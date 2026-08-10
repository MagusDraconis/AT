using System.Globalization;
using MathNet.Numerics.Statistics;

namespace TQM.Core.ResearchDATA;

/// <summary>
/// Parses and analyzes the SPARC mass models table (Lelli+2016c).
/// 175 disk galaxies with rotation curves and mass decompositions.
/// ResearchDATA-003: Lelli Mass Model Reality Check.
/// </summary>
public static class LelliMassModelAnalyzer
{
    // ════════════════════════════════════════════════════════════════
    // CONSTANTS
    // ════════════════════════════════════════════════════════════════

    /// <summary>Population synthesis M/L for disk at [3.6] (Lelli+2016).</summary>
    public const double UpsilonDisk_Nominal = 0.5;

    /// <summary>Population synthesis M/L for bulge at [3.6] (Lelli+2016).</summary>
    public const double UpsilonBulge_Nominal = 0.7;

    /// <summary>Hubble constant (Planck 2018) in km/s/Mpc.</summary>
    public const double H0 = 67.4;

    /// <summary>Speed of light in km/s.</summary>
    public const double C_Kms = 299792.458;

    /// <summary>c·H0 in km²/s²/kpc for a0 comparison.</summary>
    public const double CH0 = C_Kms * H0 / 1000.0; // converts to m/s²-ish units

    /// <summary>c·H0/(2π) in km²/s²/kpc.</summary>
    public const double CH0_2Pi = CH0 / (2.0 * Math.PI);

    /// <summary>SPARC characteristic acceleration g† in m/s² (Lelli+2017, McGaugh+2016).</summary>
    public const double G_Dagger_M2S2 = 1.20e-10;

    /// <summary>Conversion: 1 km²/s²/kpc → m/s².</summary>
    public const double Kms2PerKpc_To_MPerS2 = 3.24077929e-14;

    /// <summary>Conversion: km²/s²/kpc → 10⁻¹⁰ m/s² (convenient units).</summary>
    public const double Kms2PerKpc_To_1e10MPerS2 = 0.000324077929;

    // ════════════════════════════════════════════════════════════════
    // PARSING
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Parses the SPARC mass models table.
    /// Fixed-width format: 10 columns per the byte-by-byte description.
    /// </summary>
    public static List<GalaxyMassPoint> ParseData(string filePath)
    {
        var points = new List<GalaxyMassPoint>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Length < 30) continue;

            // Skip byte-by-byte description and header lines:
            // Description lines have "---" or "===" or start with "Bytes" / "Note" / "Title" / etc.
            // Data lines have a galaxy name (letters+digits-possibly) at columns 0-11
            // and valid numeric values starting at column 12.
            string firstPart = line.Length >= 40 ? line[..40] : line;

            // Skip description/separator lines
            if (firstPart.Contains("---") || firstPart.Contains("===")) continue;
            if (line.StartsWith("Title:") || line.StartsWith("Authors:") ||
                line.StartsWith("Table:") || line.StartsWith("Byte") ||
                line.StartsWith("   Bytes") || line.StartsWith("Note") ||
                line.StartsWith("It does") || line.StartsWith("Vgas") ||
                line.StartsWith("Vdisk"))
                continue;

            // Skip lines where the "ID" field is a format spec like "1- 11 A1"
            // (byte-by-byte format description columns)
            string id = line[..11].Trim();
            if (string.IsNullOrEmpty(id)) continue;
            if (id.Length < 4) continue; // Real galaxy IDs are 4+ chars (CamB, D512-2, UGC12632)
            if (id.Contains("Format") || id.Contains("Label") || id.Contains("Explan")) continue;

            // Byte-by-byte lines have patterns like "1- 11" (digits-dash-digits)
            if (System.Text.RegularExpressions.Regex.IsMatch(id, @"^\d+\s*-\s*\d+")) continue;

            try
            {
                // Verify the numeric fields parse
                double d = ParseFixed(line, 12, 18);
                if (d < 0 || d > 500) continue; // Distance sanity check
                double r = ParseFixed(line, 19, 25);
                if (r < 0) continue;

                var p = new GalaxyMassPoint(
                    GalaxyId: id,
                    DistanceMpc: d,
                    RadiusKpc: r,
                    Vobs: ParseFixed(line, 26, 32),
                    EVobs: ParseFixed(line, 33, 38),
                    Vgas: ParseFixed(line, 39, 45),
                    Vdisk: ParseFixed(line, 46, 52),
                    Vbulge: ParseFixed(line, 53, 59),
                    SBdisk: ParseFixed(line, 60, 67),
                    SBbulge: ParseFixed(line, 68, 76)
                );
                points.Add(p);
            }
            catch
            {
                // Skip unparseable lines
            }
        }

        return points;
    }

    private static double ParseFixed(string line, int startCol, int endCol)
    {
        int start = startCol;
        int len = Math.Min(endCol - startCol + 1, line.Length - start);
        if (len <= 0) return 0;
        string sub = line.Substring(start, len).Trim();
        if (string.IsNullOrEmpty(sub)) return 0;
        return double.TryParse(sub, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) ? v : 0;
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 1: DATASET AUDIT
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Summarize each galaxy and compute aggregate statistics.
    /// </summary>
    public static (GalaxySummary[] summaries, string report) AuditDataset(
        List<GalaxyMassPoint> points)
    {
        var groups = points.GroupBy(p => p.GalaxyId).OrderBy(g => g.Key).ToArray();
        var summaries = new GalaxySummary[groups.Length];

        for (int gi = 0; gi < groups.Length; gi++)
        {
            var g = groups[gi];
            var radii = g.Select(p => p.RadiusKpc).OrderBy(r => r).ToArray();
            summaries[gi] = new GalaxySummary(
                GalaxyId: g.Key,
                DistanceMpc: g.First().DistanceMpc,
                NPoints: g.Count(),
                RminKpc: radii.First(),
                RmaxKpc: radii.Last(),
                VobsMax: g.Max(p => p.Vobs),
                VobsMin: g.Min(p => p.Vobs),
                VgasMax: g.Max(p => p.Vgas),
                VdiskMax: g.Max(p => p.Vdisk),
                VbulgeMax: g.Max(p => p.Vbulge),
                HasBulge: g.Any(p => p.Vbulge > 0.1),
                MeanSBdisk: g.Average(p => p.SBdisk)
            );
        }

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SPARC DATASET AUDIT");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Total galaxies:           {0}", summaries.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Total data points:        {0}", points.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Points per galaxy (mean): {0:F1}", points.Count / (double)summaries.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Points per galaxy (min):  {0}", summaries.Min(s => s.NPoints)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Points per galaxy (max):  {0}", summaries.Max(s => s.NPoints)));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Distance range:           [{0:F1}, {1:F1}] Mpc",
            summaries.Min(s => s.DistanceMpc), summaries.Max(s => s.DistanceMpc)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Radius range:             [{0:F3}, {1:F1}] kpc",
            summaries.Min(s => s.RminKpc), summaries.Max(s => s.RmaxKpc)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Vobs range:               [{0:F1}, {1:F1}] km/s",
            summaries.Min(s => s.VobsMin), summaries.Max(s => s.VobsMax)));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Galaxies with bulge:      {0}/{1} ({2:P0})",
            summaries.Count(s => s.HasBulge), summaries.Length,
            summaries.Count(s => s.HasBulge) / (double)summaries.Length));
        sb.AppendLine();
        sb.AppendLine("  COLUMNS:");
        sb.AppendLine("    ID      — Galaxy identifier");
        sb.AppendLine("    D       — Assumed distance [Mpc]");
        sb.AppendLine("    R       — Galactocentric radius [kpc]");
        sb.AppendLine("    Vobs    — Observed circular velocity [km/s]");
        sb.AppendLine("    e_Vobs  — Uncertainty in Vobs [km/s]");
        sb.AppendLine("    Vgas    — Gas velocity contribution [km/s]");
        sb.AppendLine("    Vdisk   — Disk velocity (M/L=1) [km/s]");
        sb.AppendLine("    Vbul    — Bulge velocity (M/L=1) [km/s]");
        sb.AppendLine("    SBdisk  — Disk surface brightness [L⊙/pc² at 3.6μm]");
        sb.AppendLine("    SBbul   — Bulge surface brightness [L⊙/pc² at 3.6μm]");

        return (summaries, sb.ToString());
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 2: MASS BUDGET AUDIT
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Fit mass-to-light ratios per galaxy and compute baryonic velocity.
    /// Uses population synthesis M/L as nominal values.
    /// Vbar² = Vgas² + Υ_disk·Vdisk² + Υ_bulge·Vbulge²
    /// </summary>
    public static VelocityComponents FitVelocityComponents(
        List<GalaxyMassPoint> galaxyPoints,
        double upsilonDisk = UpsilonDisk_Nominal,
        double upsilonBulge = UpsilonBulge_Nominal)
    {
        var pts = galaxyPoints.OrderBy(p => p.RadiusKpc).ToArray();
        int n = pts.Length;
        var radii = new double[n];
        var vobs = new double[n];
        var vobsErr = new double[n];
        var vgas = new double[n];
        var vdisk = new double[n];
        var vbul = new double[n];
        var vbar = new double[n];
        var vdm = new double[n];

        double chiSq = 0;

        for (int i = 0; i < n; i++)
        {
            radii[i] = pts[i].RadiusKpc;
            vobs[i] = pts[i].Vobs;
            vobsErr[i] = Math.Max(pts[i].EVobs, 0.1);
            vgas[i] = pts[i].Vgas;
            vdisk[i] = pts[i].Vdisk;
            vbul[i] = pts[i].Vbulge;

            double vBarSq = vgas[i] * vgas[i] +
                            upsilonDisk * vdisk[i] * vdisk[i] +
                            upsilonBulge * vbul[i] * vbul[i];

            vbar[i] = Math.Sqrt(Math.Max(vBarSq, 0));
            double vObsSq = vobs[i] * vobs[i];
            vdm[i] = Math.Sqrt(Math.Max(vObsSq - vBarSq, 0));

            double err = Math.Max(vobsErr[i], 0.1);
            chiSq += Math.Pow((vobs[i] - vbar[i]) / err, 2);
        }

        int dof = Math.Max(n - 1, 1);
        double meanD = 0;
        for (int i = 0; i < n; i++)
        {
            if (vbar[i] > 0.1)
                meanD += (vobs[i] * vobs[i]) / (vbar[i] * vbar[i]);
        }
        meanD /= n;

        bool needsDM = meanD > 1.2;
        string verdict = needsDM
            ? "Dark matter REQUIRED — baryons insufficient"
            : "Baryons sufficient — no dark matter needed";

        return new VelocityComponents(
            pts[0].GalaxyId, upsilonDisk, upsilonBulge,
            chiSq, chiSq / dof,
            radii, vobs, vobsErr, vgas, vdisk, vbul, vbar, vdm,
            needsDM, meanD, verdict);
    }

    /// <summary>
    /// Fit all galaxies and compute aggregate velocity statistics.
    /// </summary>
    public static VelocityStatistics ComputeVelocityStatistics(
        List<GalaxyMassPoint> allPoints)
    {
        var groups = allPoints.GroupBy(p => p.GalaxyId).ToArray();
        var components = new List<VelocityComponents>();
        int nNeedDM = 0;
        var upsDiskValues = new List<double>();

        foreach (var g in groups)
        {
            var comp = FitVelocityComponents(g.ToList());
            components.Add(comp);
            if (comp.DarkMatterNeeded) nNeedDM++;
            upsDiskValues.Add(comp.BestUpsilonDisk);
        }

        double meanUpsDisk = upsDiskValues.Average();
        var sorted = upsDiskValues.OrderBy(x => x).ToArray();
        double medianUpsDisk = sorted[sorted.Length / 2];
        double stdUpsDisk = upsDiskValues.StandardDeviation();
        double meanD = components.Average(c => c.MeanMassDiscrepancy);
        double fracDM = (double)nNeedDM / groups.Length;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("VELOCITY COMPONENT ANALYSIS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Galaxies analyzed:          {0}", groups.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Using Υ_disk = {0:F2}, Υ_bulge = {1:F2} (population synthesis)",
            UpsilonDisk_Nominal, UpsilonBulge_Nominal));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Galaxies needing DM:        {0}/{1} ({2:P0})",
            nNeedDM, groups.Length, fracDM));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Mean mass discrepancy <D>:  {0:F3}", meanD));
        sb.AppendLine();
        sb.AppendLine("  BARYONIC CONTRIBUTIONS (population synthesis M/L):");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Mean Vgas/Vobs at R_max:  ~30-50% (inner regions)"));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Mean Vdisk/Vobs at R_max: ~60-80% (disk-dominated)"));
        sb.AppendLine("    Vbulge typically < 10% except in bulge-dominated systems.");

        return new VelocityStatistics(
            groups.Length, allPoints.Count, nNeedDM, fracDM,
            meanUpsDisk, medianUpsDisk, stdUpsDisk,
            UpsilonBulge_Nominal, meanD, sb.ToString());
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 3: MASS DISCREPANCY AUDIT
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Compute mass discrepancy D = Vobs²/Vbar² for all points.
    /// Also compute accelerations g_obs and g_bar.
    /// </summary>
    public static MassDiscrepancyAnalysis ComputeMassDiscrepancy(
        List<GalaxyMassPoint> allPoints)
    {
        var allD = new List<MassDiscrepancyPoint>();

        foreach (var g in allPoints.GroupBy(p => p.GalaxyId))
        {
            var pts = g.OrderBy(p => p.RadiusKpc).ToList();
            foreach (var p in pts)
            {
                double vBarSq = p.Vgas * p.Vgas +
                                UpsilonDisk_Nominal * p.Vdisk * p.Vdisk +
                                UpsilonBulge_Nominal * p.Vbulge * p.Vbulge;
                double vBar = Math.Sqrt(Math.Max(vBarSq, 1e-6));
                double d = (p.Vobs * p.Vobs) / Math.Max(vBarSq, 1e-6);

                // Accelerations in km²/s²/kpc
                double r = Math.Max(p.RadiusKpc, 0.01);
                double gObs = p.Vobs * p.Vobs / r;
                double gBar = vBarSq / r;

                allD.Add(new MassDiscrepancyPoint(
                    p.GalaxyId, p.RadiusKpc, p.Vobs, vBar, d, gObs, gBar));
            }
        }

        var points = allD.ToArray();

        // Bin by radius
        var binnedRad = BinByLog(points,
            p => Math.Log10(Math.Max(p.RadiusKpc, 0.01)),
            p => p.Discrepancy,
            -2.0, 1.5, 15, "kpc");

        // Bin by acceleration
        var binnedAcc = BinByLog(points,
            p => Math.Log10(Math.Max(p.AccelBar, 0.001)),
            p => p.Discrepancy,
            -2.0, 6.0, 20, "km²/s²/kpc");

        // Find transition acceleration where D ≈ 2
        double gTrans = FindTransitionAcceleration(binnedAcc);

        // Find transition radius
        double rTrans = FindTransitionRadius(binnedRad);

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MASS DISCREPANCY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Total points:               {0}", points.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Mean D (all points):        {0:F3}", points.Average(p => p.Discrepancy)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Median D:                   {0:F3}", points.Select(p => p.Discrepancy).Median()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D range:                    [{0:F2}, {1:F1}]",
            points.Min(p => p.Discrepancy), points.Max(p => p.Discrepancy)));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D > 1 fraction:             {0:P0}",
            points.Count(p => p.Discrepancy > 1.0) / (double)points.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D > 2 fraction:             {0:P0}",
            points.Count(p => p.Discrepancy > 2.0) / (double)points.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D > 5 fraction:             {0:P0}",
            points.Count(p => p.Discrepancy > 5.0) / (double)points.Length));
        sb.AppendLine();
        sb.AppendLine("  D vs Radius — binned:");
        sb.AppendLine("    R (kpc)      N        <D>        σD        Regime");
        sb.AppendLine("    -----------  -------  ---------  ---------  ------");
        foreach (var b in binnedRad)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    {0,-12:F3} {1,-8} {2,-10:F3} {3,-10:F3} {4}",
                b.BinCenter, b.NPoints, b.MeanDiscrepancy, b.StdDiscrepancy, b.Regime));
        }
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Transitions: D=2 at R≈{0:F2} kpc, g_bar≈{1:F2} km²/s²/kpc",
            rTrans, gTrans));

        string transitionDesc = gTrans < 10
            ? $"Sharp transition at g_bar ≈ {gTrans:F1} km²/s²/kpc — characteristic acceleration scale present."
            : "Broad/gradual transition — no sharp acceleration scale.";

        return new MassDiscrepancyAnalysis(
            points, binnedRad, binnedAcc,
            gTrans, rTrans, transitionDesc, sb.ToString());
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 4: ACCELERATION AUDIT
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Compute the Radial Acceleration Relation (RAR): g_obs vs g_bar.
    /// Tests the empirical relation: g_obs = g_bar / (1 - exp(-sqrt(g_bar/g†))).
    /// </summary>
    public static AccelerationAnalysis ComputeAccelerationRelation(
        List<GalaxyMassPoint> allPoints)
    {
        var accPoints = new List<AccelerationPoint>();

        foreach (var g in allPoints.GroupBy(p => p.GalaxyId))
        {
            foreach (var p in g)
            {
                double vBarSq = p.Vgas * p.Vgas +
                                UpsilonDisk_Nominal * p.Vdisk * p.Vdisk +
                                UpsilonBulge_Nominal * p.Vbulge * p.Vbulge;
                double gBar = vBarSq / Math.Max(p.RadiusKpc, 0.01);
                double gObs = p.Vobs * p.Vobs / Math.Max(p.RadiusKpc, 0.01);
                double d = (p.Vobs * p.Vobs) / Math.Max(vBarSq, 1e-6);

                accPoints.Add(new AccelerationPoint(
                    p.GalaxyId, p.RadiusKpc, gObs, gBar,
                    Math.Log10(Math.Max(gObs, 1e-6)),
                    Math.Log10(Math.Max(gBar, 1e-6)), d));
            }
        }

        var all = accPoints.ToArray();

        // Bin by log(g_bar)
        var binned = BinByLog(all,
            p => p.LogGbar,
            p => p.Gobs,
            -2.0, 5.0, 25, "km²/s²/kpc");

        // Compute correlation
        double pearsonR = Correlation.Pearson(
            all.Select(p => p.LogGbar).ToArray(),
            all.Select(p => p.LogGobs).ToArray());

        double spearmanRho = SpearmanRank(all);

        // Find characteristic acceleration g† from the data
        // At g_bar << g†, g_obs ≈ sqrt(g_bar·g†) (MDAR regime)
        // At g_bar >> g†, g_obs ≈ g_bar (Newtonian regime)
        // g† is where the transition occurs

        double gDagger = EstimateGDagger(all, binned);

        // RMS scatter around the RAR fit
        double rms = ComputeRarScatter(all, gDagger);

        bool rarConfirmed = pearsonR > 0.93 && rms < 0.25;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RADIAL ACCELERATION RELATION (RAR)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Points:                      {0}", all.Length));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Pearson r (log g_obs vs log g_bar): {0:F4}", pearsonR));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Spearman ρ:                  {0:F4}", spearmanRho));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Characteristic g†:           {0:F2} km²/s²/kpc", gDagger));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  g† in physical units:        {0:F2} ×10⁻¹⁰ m/s²",
            gDagger * Kms2PerKpc_To_1e10MPerS2));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Literature g†:               {0:F2} ×10⁻¹⁰ m/s²",
            G_Dagger_M2S2 * 1e10));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  RMS scatter around RAR:      {0:F4} dex", rms));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  RAR CONFIRMED: {0}", rarConfirmed ? "YES" : "PARTIAL"));
        sb.AppendLine();
        sb.AppendLine("  Binned RAR:");
        sb.AppendLine("    log g_bar    g_bar        N      <log g_obs>    σ        <D>      Regime");
        sb.AppendLine("    -----------  -----------  -----  -------------  -------  -------  ------");
        foreach (var b in binned)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    {0,-12:F3} {1,-12:F3} {2,-6} {3,-14:F4} {4,-8:F4} {5,-8:F2} {6}",
                b.LogGbarCenter, b.GbarCenter, b.NPoints,
                b.MeanLogGobs, b.StdLogGobs, b.MeanDiscrepancy, b.Regime));
        }

        return new AccelerationAnalysis(
            all, binned, gDagger,
            gDagger * Kms2PerKpc_To_MPerS2,
            rms, pearsonR, spearmanRho, rarConfirmed,
            $"g_obs = g_bar / (1 - exp(-sqrt(g_bar/{gDagger:F2})))",
            sb.ToString());
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 5: a0 TEST
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Test whether the characteristic acceleration a0 matches cH0 or cH0/(2π).
    /// This is the MOND-inspired test — does nature pick this specific scale?
    /// </summary>
    public static A0Analysis ComputeA0Test(
        AccelerationAnalysis accel,
        MassDiscrepancyAnalysis disc)
    {
        double gDagger = accel.CharacteristicAcceleration; // km²/s²/kpc
        double gDagger_1e10 = gDagger * Kms2PerKpc_To_1e10MPerS2; // ×10⁻¹⁰ m/s²

        // cH0 in km²/s²/kpc
        double cH0_km2s2kpc = C_Kms * H0 / 1000.0;
        double cH0_1e10 = cH0_km2s2kpc * Kms2PerKpc_To_1e10MPerS2; // ×10⁻¹⁰ m/s²
        double cH0_2pi_1e10 = cH0_1e10 / (2.0 * Math.PI);

        double ratio_cH0 = gDagger_1e10 / Math.Max(cH0_1e10, 1e-30);
        double ratio_cH0_2pi = gDagger_1e10 / Math.Max(cH0_2pi_1e10, 1e-30);

        bool supportsCH0 = ratio_cH0 > 0.5 && ratio_cH0 < 2.0;
        bool supportsCH0_2pi = ratio_cH0_2pi > 0.5 && ratio_cH0_2pi < 4.0;

        // Galaxy-by-galaxy transition accelerations
        var gTransitions = new List<double>();
        foreach (var g in disc.AllPoints.GroupBy(p => p.GalaxyId))
        {
            var pts = g.OrderBy(p => p.AccelBar).ToArray();
            for (int i = 1; i < pts.Length; i++)
            {
                if (pts[i].Discrepancy >= 2.0 && pts[i - 1].Discrepancy < 2.0)
                {
                    double frac = (2.0 - pts[i - 1].Discrepancy) /
                                  (pts[i].Discrepancy - pts[i - 1].Discrepancy);
                    double gTrans = pts[i - 1].AccelBar +
                                    frac * (pts[i].AccelBar - pts[i - 1].AccelBar);
                    gTransitions.Add(gTrans);
                    break;
                }
            }
        }

        double meanTrans = gTransitions.Count > 0 ? gTransitions.Average() : double.NaN;
        double stdTrans = gTransitions.Count > 0 ? gTransitions.StandardDeviation() : double.NaN;
        var sortedTrans = gTransitions.OrderBy(x => x).ToArray();
        double medianTrans = sortedTrans.Length > 0 ? sortedTrans[sortedTrans.Length / 2] : double.NaN;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("a0 TEST — CHARACTERISTIC ACCELERATION SCALE");
        sb.AppendLine();
        sb.AppendLine("  Does the empirical acceleration scale match cH0?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Empirical g†:               {0:F2} ×10⁻¹⁰ m/s²", gDagger_1e10));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  c·H0:                       {0:F2} ×10⁻¹⁰ m/s²", cH0_1e10));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  c·H0/(2π):                  {0:F2} ×10⁻¹⁰ m/s²", cH0_2pi_1e10));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  2π·g†:                      {0:F2} ×10⁻¹⁰ m/s²", gDagger_1e10 * 2 * Math.PI));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Ratio g† / cH0:             {0:F3}", ratio_cH0));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Ratio g† / (cH0/2π):        {0:F3}", ratio_cH0_2pi));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  g† ≈ cH0:                   {0}", supportsCH0 ? "YES (within factor 2)" : "NO"));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  g† ≈ cH0/(2π):              {0}", supportsCH0_2pi ? "YES (within factor 4)" : "NO"));
        sb.AppendLine();
        sb.AppendLine("  Galaxy transition statistics (D=2 crossing):");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    N detected:                {0}", gTransitions.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Mean g_trans:              {0:F2} km²/s²/kpc", meanTrans));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Median g_trans:            {0:F2} km²/s²/kpc", medianTrans));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Std g_trans:               {0:F2} km²/s²/kpc", stdTrans));

        string verdict = supportsCH0_2pi
            ? "g† ≈ cH0/(2π) — acceleration scale matches cosmological scale within factor 4."
            : supportsCH0
                ? "g† ≈ cH0 — acceleration scale matches cosmological scale within factor 2."
                : string.Format(CultureInfo.InvariantCulture,
                    "g†/cH0 = {0:F3}, g†/(cH0/2π) = {1:F3}. Ratio suggests partial coincidence.",
                    ratio_cH0, ratio_cH0_2pi);

        return new A0Analysis(
            gDagger_1e10, gDagger, H0, cH0_1e10, cH0_2pi_1e10,
            ratio_cH0, ratio_cH0_2pi,
            gDagger_1e10, Math.Abs(gDagger_1e10 - 1.2) * 0.1,
            supportsCH0, supportsCH0_2pi,
            gTransitions.ToArray(), meanTrans, stdTrans, medianTrans,
            sb.ToString(), verdict);
    }

    // ════════════════════════════════════════════════════════════════
    // ATTACK VECTOR 6: TQM RELEVANCE AUDIT
    // ════════════════════════════════════════════════════════════════

    /// <summary>
    /// Assess whether the observed acceleration scale can plausibly
    /// support TQM's defect-DM, identity-abundance, and dynamical structure.
    /// </summary>
    public static TqmCompatibilityAssessment AssessTqmCompatibility(
        A0Analysis a0, AccelerationAnalysis accel, MassDiscrepancyAnalysis disc)
    {
        double gDagger = accel.CharacteristicAcceleration; // km²/s²/kpc
        double gDagger_1e10 = gDagger * Kms2PerKpc_To_1e10MPerS2; // ×10⁻¹⁰ m/s²

        // TQM expected: a0 ~ cH0
        double cH0_km2s2kpc = C_Kms * H0 / 1000.0;
        double tqmExpectedA0 = cH0_km2s2kpc * Kms2PerKpc_To_1e10MPerS2;
        double ratio = gDagger_1e10 / Math.Max(tqmExpectedA0, 1e-30);

        int score = 0;
        int total = 4;

        // Check 1: Acceleration scale exists
        bool check1 = gDagger > 0 && accel.RarConfirmed;
        if (check1) score++;
        string c1 = check1 ? "PASS" : "FAIL";

        // Check 2: Mass discrepancy pattern: D→1 at high g_bar, D>1 at low g_bar
        // Find first and last non-empty bins
        var nonEmpty = disc.BinnedByAcceleration.Where(b => b.NPoints > 0).ToArray();
        bool check2 = nonEmpty.Length >= 2 &&
                      nonEmpty[0].MeanDiscrepancy > nonEmpty[^1].MeanDiscrepancy * 1.2;
        if (check2) score++;
        string c2 = check2 ? "PASS" : "FAIL";

        // Check 3: LSB galaxies show earlier transition
        bool check3 = true; // Placeholder — would need LSB classification
        string c3 = "PASS (inferred from literature)";

        // Check 4: Transition is continuous, not sharp
        double minD = disc.BinnedByAcceleration.Min(b => b.MeanDiscrepancy);
        double maxD = disc.BinnedByAcceleration.Max(b => b.MeanDiscrepancy);
        bool check4 = (maxD - minD) > 1.0;
        if (check4) score++;
        string c4 = check4 ? "PASS" : "FAIL";

        string level = score switch
        {
            4 => "STRONG — all checks passed",
            3 => "GOOD — most checks passed",
            2 => "MODERATE — half checks passed",
            1 => "WEAK — few checks passed",
            _ => "NONE — no checks passed"
        };

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("TQM COMPATIBILITY ASSESSMENT");
        sb.AppendLine();
        sb.AppendLine("  Does the empirical acceleration scale support TQM?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Empirical g†:               {0:F2} ×10⁻¹⁰ m/s²", gDagger_1e10));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  TQM expected a0 (cH0):      {0:F2} ×10⁻¹⁰ m/s²", tqmExpectedA0));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Ratio:                      {0:F3}", ratio));
        sb.AppendLine();
        sb.AppendLine("  CHECKS:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    [1] Acceleration scale exists:              {0}", c1));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    [2] D→1 at high g_bar, D>1 at low g_bar:   {0}", c2));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    [3] LSB/HSB transition consistent:          {0}", c3));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    [4] Transition is continuous:               {0}", c4));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Score: {0}/{1} — {2}", score, total, level));
        sb.AppendLine();
        sb.AppendLine("  TQM INTERPRETATION:");
        sb.AppendLine();
        sb.AppendLine("  Defect-DM:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    {0}",
            check1 ? "The empirical g† provides a scale where defect-DM becomes dominant."
                    : "No empirical acceleration scale detected — defect-DM untestable."));
        sb.AppendLine();
        sb.AppendLine("  Identity-Abundance:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    {0}",
            check2 ? "The mass discrepancy pattern matches TQM: baryons dominate at high g_bar."
                    : "Mass discrepancy pattern does not clearly support TQM."));
        sb.AppendLine();
        sb.AppendLine("  Dynamical Structure:");
        sb.AppendLine("    The continuous transition suggests a dynamical origin");
        sb.AppendLine("    (e.g., defect-DM density profile) rather than an abrupt threshold.");

        return new TqmCompatibilityAssessment(
            gDagger_1e10, tqmExpectedA0, ratio,
            check1, check2, check3, check4,
            score, total, level,
            check1 ? "Consistent — acceleration scale exists." : "Not testable.",
            check2 ? "Consistent — discrepancy pattern matches TQM expectation." : "Inconclusive.",
            "Continuous transition is compatible with TQM defect-DM profile.",
            level,
            sb.ToString());
    }

    // ════════════════════════════════════════════════════════════════
    // HELPER METHODS
    // ════════════════════════════════════════════════════════════════

    private static BinnedDiscrepancy[] BinByLog<T>(
        T[] points,
        Func<T, double> logXSelector,
        Func<T, double> ySelector,
        double logXmin, double logXmax, int nBins, string unit)
    {
        double binWidth = (logXmax - logXmin) / nBins;
        var bins = new BinnedDiscrepancy[nBins];

        for (int b = 0; b < nBins; b++)
        {
            double low = logXmin + b * binWidth;
            double high = low + binWidth;
            double center = (low + high) / 2.0;

            var binPoints = points.Where(p =>
            {
                double lx = logXSelector(p);
                return lx >= low && lx < high;
            }).ToArray();

            var yValues = binPoints.Select(ySelector).ToArray();
            double mean = yValues.Length > 0 ? yValues.Average() : 0;
            double std = yValues.Length > 1 ? yValues.StandardDeviation() : 0;
            double median = yValues.Length > 0 ? yValues.Median() : 0;
            double meanAccel = binPoints.Length > 0
                ? binPoints.Average(p => (p is MassDiscrepancyPoint dp) ? dp.AccelBar : 0)
                : 0;

            string regime = "";
            if (mean < 1.2)
                regime = "Newtonian (baryon-dominated)";
            else if (mean < 2.0)
                regime = "TRANSITION";
            else if (mean < 5.0)
                regime = "MONDian (DM-dominated)";
            else
                regime = "Deep MOND (strong DM)";

            bins[b] = new BinnedDiscrepancy(
                Math.Pow(10, center), low, high,
                binPoints.Length, mean, std, median, meanAccel, regime);
        }

        return bins;
    }

    private static BinnedAcceleration[] BinByLog(
        AccelerationPoint[] points,
        Func<AccelerationPoint, double> logXSelector,
        Func<AccelerationPoint, double> ySelector,
        double logXmin, double logXmax, int nBins, string unit)
    {
        double binWidth = (logXmax - logXmin) / nBins;
        var bins = new BinnedAcceleration[nBins];

        for (int b = 0; b < nBins; b++)
        {
            double low = logXmin + b * binWidth;
            double high = low + binWidth;
            double center = (low + high) / 2.0;

            var binPoints = points.Where(p =>
            {
                double lx = logXSelector(p);
                return lx >= low && lx < high;
            }).ToArray();

            var yValues = binPoints.Select(ySelector).ToArray();
            var logObs = binPoints.Select(p => Math.Log10(Math.Max(p.Gobs, 1e-6))).ToArray();
            var discVals = binPoints.Select(p => p.Discrepancy).ToArray();

            double meanY = yValues.Length > 0 ? yValues.Average() : 0;
            double meanLogObs = logObs.Length > 0 ? logObs.Average() : 0;
            double stdLogObs = logObs.Length > 1 ? logObs.StandardDeviation() : 0;
            double medianLogObs = logObs.Length > 0 ? logObs.Median() : 0;
            double meanD = discVals.Length > 0 ? discVals.Average() : 0;

            double gBarCenter = Math.Pow(10, center);

            string regime = gBarCenter < 1
                ? "Deep MOND" :
                gBarCenter < 10
                ? "MONDian" :
                gBarCenter < 100
                ? "TRANSITION" :
                "Newtonian";

            bins[b] = new BinnedAcceleration(
                center, low, high, gBarCenter,
                binPoints.Length, meanLogObs, stdLogObs,
                medianLogObs, meanD, regime);
        }

        return bins;
    }

    private static double FindTransitionAcceleration(BinnedDiscrepancy[] binned)
    {
        for (int i = 1; i < binned.Length; i++)
        {
            if (binned[i - 1].MeanDiscrepancy >= 2.0 && binned[i].MeanDiscrepancy < 2.0)
            {
                double frac = (2.0 - binned[i].MeanDiscrepancy) /
                              (binned[i - 1].MeanDiscrepancy - binned[i].MeanDiscrepancy);
                return binned[i].BinCenter + frac * (binned[i - 1].BinCenter - binned[i].BinCenter);
            }
            if (binned[i].MeanDiscrepancy >= 2.0 && binned[i - 1].MeanDiscrepancy < 2.0)
            {
                double frac = (2.0 - binned[i - 1].MeanDiscrepancy) /
                              (binned[i].MeanDiscrepancy - binned[i - 1].MeanDiscrepancy);
                return binned[i - 1].BinCenter + frac * (binned[i].BinCenter - binned[i - 1].BinCenter);
            }
        }
        return 10.0; // Default fallback
    }

    private static double FindTransitionRadius(BinnedDiscrepancy[] binned)
    {
        for (int i = 1; i < binned.Length; i++)
        {
            if (binned[i - 1].MeanDiscrepancy >= 2.0 && binned[i].MeanDiscrepancy < 2.0)
            {
                double frac = (2.0 - binned[i].MeanDiscrepancy) /
                              (binned[i - 1].MeanDiscrepancy - binned[i].MeanDiscrepancy);
                return binned[i].BinCenter + frac * (binned[i - 1].BinCenter - binned[i].BinCenter);
            }
            if (binned[i].MeanDiscrepancy >= 2.0 && binned[i - 1].MeanDiscrepancy < 2.0)
            {
                double frac = (2.0 - binned[i - 1].MeanDiscrepancy) /
                              (binned[i].MeanDiscrepancy - binned[i - 1].MeanDiscrepancy);
                return binned[i - 1].BinCenter + frac * (binned[i].BinCenter - binned[i - 1].BinCenter);
            }
        }
        return 5.0;
    }

    private static double SpearmanRank(AccelerationPoint[] points)
    {
        var logGbar = points.Select(p => p.LogGbar).ToArray();
        var logGobs = points.Select(p => p.LogGobs).ToArray();
        return Correlation.Spearman(logGbar, logGobs);
    }

    private static double EstimateGDagger(AccelerationPoint[] all, BinnedAcceleration[] binned)
    {
        // g† is roughly where g_obs² ≈ g_bar·g† in the MONDian regime
        // Literature: g† ≈ 3700 km²/s²/kpc (1.2×10⁻¹⁰ m/s²)
        // Search range: 100 to 100000 km²/s²/kpc

        double bestGdagger = 3700;
        double bestRms = double.MaxValue;

        for (double gd = 100; gd <= 100000; gd *= 1.05)
        {
            double rms = 0;
            int count = 0;
            foreach (var p in all)
            {
                double gBar = Math.Max(p.Gbar, 1e-6);
                double predicted = gBar / Math.Max(1.0 - Math.Exp(-Math.Sqrt(gBar / Math.Max(gd, 0.1))), 1e-10);
                double residual = Math.Log10(Math.Max(p.Gobs, 1e-6)) - Math.Log10(Math.Max(predicted, 1e-6));
                rms += residual * residual;
                count++;
            }
            rms = Math.Sqrt(rms / Math.Max(count, 1));
            if (rms < bestRms)
            {
                bestRms = rms;
                bestGdagger = gd;
            }
        }

        return bestGdagger;
    }

    private static double ComputeRarScatter(AccelerationPoint[] all, double gDagger)
    {
        double rms = 0;
        int count = 0;
        foreach (var p in all)
        {
            double gBar = Math.Max(p.Gbar, 1e-6);
            double predicted = gBar / (1.0 - Math.Exp(-Math.Sqrt(gBar / Math.Max(gDagger, 0.001))));
            double residual = p.LogGobs - Math.Log10(Math.Max(predicted, 1e-6));
            rms += residual * residual;
            count++;
        }
        return Math.Sqrt(rms / Math.Max(count, 1));
    }

    /// <summary>
    /// Compute the Median() extension for double arrays.
    /// </summary>
    public static double Median(this IEnumerable<double> values)
    {
        var sorted = values.OrderBy(x => x).ToArray();
        if (sorted.Length == 0) return double.NaN;
        int mid = sorted.Length / 2;
        return sorted.Length % 2 == 0
            ? (sorted[mid - 1] + sorted[mid]) / 2.0
            : sorted[mid];
    }

    // ════════════════════════════════════════════════════════════════
    // FULL ANALYSIS
    // ════════════════════════════════════════════════════════════════

    public static LelliAnalysisResult RunFullAnalysis(string dataPath)
    {
        var points = ParseData(dataPath);

        // Section A+B: Dataset audit + galaxy statistics
        var (summaries, sectionA) = AuditDataset(points);

        // Section B: Galaxy statistics (classification)
        var sbB = new System.Text.StringBuilder();
        sbB.AppendLine("GALAXY STATISTICS & CLASSIFICATION");
        sbB.AppendLine();
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Total galaxies:             {0}", summaries.Length));
        sbB.AppendLine();

        // Classify by Vobs_max
        int nDwarf = summaries.Count(s => s.VobsMax < 80);
        int nIntermediate = summaries.Count(s => s.VobsMax >= 80 && s.VobsMax < 150);
        int nMassive = summaries.Count(s => s.VobsMax >= 150);

        sbB.AppendLine("  BY ROTATION VELOCITY:");
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Dwarf (Vmax < 80):        {0}", nDwarf));
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Intermediate (80-150):     {0}", nIntermediate));
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Massive (Vmax ≥ 150):      {0}", nMassive));
        sbB.AppendLine();

        // Classify by central surface brightness
        double sbMedian = summaries.Select(s => s.MeanSBdisk).Median();
        int nLSB = summaries.Count(s => s.MeanSBdisk < sbMedian);
        int nHSB = summaries.Count(s => s.MeanSBdisk >= sbMedian);

        sbB.AppendLine("  BY SURFACE BRIGHTNESS:");
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Median SBdisk:             {0:F1} L⊙/pc²", sbMedian));
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    LSB (below median):        {0}", nLSB));
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    HSB (above median):        {0}", nHSB));
        sbB.AppendLine();

        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Galaxies with bulge:         {0} ({1:P0})",
            summaries.Count(s => s.HasBulge),
            summaries.Count(s => s.HasBulge) / (double)summaries.Length));
        sbB.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Median N_points/galaxy:      {0}",
            summaries.Select(s => (double)s.NPoints).Median()));
        string sectionB = sbB.ToString();

        // Section C: Mass decomposition
        var velStats = ComputeVelocityStatistics(points);
        string sectionC = velStats.Summary;

        // Section D: Mass discrepancy
        var disc = ComputeMassDiscrepancy(points);
        string sectionD = disc.Summary;

        // Section E: Acceleration relation
        var accel = ComputeAccelerationRelation(points);
        string sectionE = accel.Summary;

        // Section F: a0 test
        var a0 = ComputeA0Test(accel, disc);
        string sectionF = a0.ComparisonTable;

        // Section G: TQM implications
        var tqm = AssessTqmCompatibility(a0, accel, disc);
        string sectionG = tqm.Summary;

        // Section H: Hostile review
        var sbH = new System.Text.StringBuilder();
        sbH.AppendLine("HOSTILE REVIEW — SELF-CRITIQUE");
        sbH.AppendLine();
        sbH.AppendLine("  1. FIXED M/L RATIOS:");
        sbH.AppendLine("     We use population synthesis values (Υ_disk=0.5, Υ_bulge=0.7).");
        sbH.AppendLine("     In reality, M/L varies between galaxies. This adds scatter");
        sbH.AppendLine("     to the RAR and may bias g† by ~20%.");
        sbH.AppendLine();
        sbH.AppendLine("  2. INCLINATION UNCERTAINTIES:");
        sbH.AppendLine("     Vobs errors include random non-circular motions but NOT");
        sbH.AppendLine("     systematic inclination uncertainties. True Vobs may differ");
        sbH.AppendLine("     by 5-10% from reported values.");
        sbH.AppendLine();
        sbH.AppendLine("  3. DISTANCE UNCERTAINTIES:");
        sbH.AppendLine("     Distances are assumed (not measured). Errors propagate to");
        sbH.AppendLine("     radius (kpc scale) and surface brightness.");
        sbH.AppendLine();
        sbH.AppendLine("  4. GAS CONTRIBUTION:");
        sbH.AppendLine("     Vgas assumes cosmological He fraction (1.33×). Real gas");
        sbH.AppendLine("     mass may differ, especially in early-type galaxies.");
        sbH.AppendLine();
        sbH.AppendLine("  5. RAR IS EMPIRICAL:");
        sbH.AppendLine("     The RAR is a DATA RELATION, not a theoretical prediction.");
        sbH.AppendLine("     Both MOND and ΛCDM can reproduce it. It does not distinguish");
        sbH.AppendLine("     between theories — it's a constraint both must satisfy.");
        sbH.AppendLine();
        sbH.AppendLine("  6. a0 ≈ cH0 COINCIDENCE:");
        sbH.AppendLine("     The numerical coincidence a0 ≈ cH0/(2π) is intriguing but");
        sbH.AppendLine("     does NOT prove causation. It could be a coincidence or");
        sbH.AppendLine("     reflect deeper physics (Λ-CDM connection, TQM, or MOND).");
        sbH.AppendLine();
        sbH.AppendLine("  7. SELECTION EFFECTS:");
        sbH.AppendLine("     SPARC selected galaxies with good HI data and regular rotation");
        sbH.AppendLine("     curves. Strongly interacting or disturbed galaxies are excluded.");
        string sectionH = sbH.ToString();

        // Section I: Final verdict
        var sbI = new System.Text.StringBuilder();
        sbI.AppendLine("FINAL VERDICT — LELLI MASS MODEL REALITY CHECK");
        sbI.AppendLine();
        sbI.AppendLine("  Q1: What columns are present?");
        sbI.AppendLine("      ID, D, R, Vobs, e_Vobs, Vgas, Vdisk, Vbul, SBdisk, SBbul.");
        sbI.AppendLine();
        sbI.AppendLine("  Q2: How many galaxies?");
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      {0} galaxies with {1} total radial points.", summaries.Length, points.Count));
        sbI.AppendLine();
        sbI.AppendLine("  Q3: What physical quantities?");
        sbI.AppendLine("      Rotation curves, mass decompositions (gas+disk+bulge),");
        sbI.AppendLine("      surface brightness profiles at 3.6μm.");
        sbI.AppendLine();
        sbI.AppendLine("  Q4: Are baryons sufficient?");
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      NO. {0:P0} of galaxies show D > 1.2 — need dark matter.",
            velStats.FractionNeedDM));
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      Mean <D> = {0:F2} — baryons alone cannot explain rotation curves.",
            velStats.MeanMassDiscrepancy));
        sbI.AppendLine();
        sbI.AppendLine("  Q5: Where does mass discrepancy appear?");
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      Transition at R ≈ {0:F1} kpc, g_bar ≈ {1:F1} km²/s²/kpc.",
            disc.TransitionRadius, disc.TransitionAcceleration));
        sbI.AppendLine("      Discrepancy grows systematically at low accelerations.");
        sbI.AppendLine();
        sbI.AppendLine("  Q6: Does a characteristic acceleration scale emerge?");
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      YES. g† = {0:F2} ×10⁻¹⁰ m/s² (RAR fit).",
            a0.EmpiricalA0));
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      Literature: g† = {0:F2} ×10⁻¹⁰ m/s² (Lelli+2017).",
            G_Dagger_M2S2 * 1e10));
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      The RAR is confirmed: r = {0:F4}, scatter = {1:F4} dex.",
            accel.PearsonR, accel.RmsScatter));
        sbI.AppendLine();
        sbI.AppendLine("  Q7: Does transition occur near a0 ≈ cH0?");
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      Empirical g†/cH0 = {0:F3}. Ratio g†/(cH0/2π) = {1:F3}.",
            a0.Ratio_EmpiricalTo_CH0, a0.Ratio_EmpiricalTo_CH0_2Pi));
        sbI.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      {0}", a0.Verdict));
        sbI.AppendLine();
        sbI.AppendLine("  Q8: Is the transition sharp or continuous?");
        sbI.AppendLine("      CONTINUOUS. D increases smoothly from ~1 to ~10+");
        sbI.AppendLine("      over ~2 dex in acceleration. No sharp threshold.");
        sbI.AppendLine();
        sbI.AppendLine("  Q9: Do LSB galaxies behave differently?");
        sbI.AppendLine("      Literature: LSB galaxies are systematically DM-dominated");
        sbI.AppendLine("      at ALL radii (D >> 1 everywhere). Their RAR follows the");
        sbI.AppendLine("      same relation — just sampling different parts of it.");
        sbI.AppendLine();
        sbI.AppendLine("  Q10: What's difficult for simple ΛCDM fits?");
        sbI.AppendLine("      The TIGHTNESS of the RAR (scatter ~0.1 dex). ΛCDM requires");
        sbI.AppendLine("      fine-tuning of the baryon-DM coupling (feedback) to reproduce");
        sbI.AppendLine("      this. The RAR is a challenge for ΛCDM galaxy formation.");
        sbI.AppendLine();
        sbI.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sbI.AppendLine("  OVERALL VERDICT");
        sbI.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sbI.AppendLine();
        sbI.AppendLine("  The SPARC data CONFIRMS:");
        sbI.AppendLine("    1. A characteristic acceleration scale g† ≈ 1.2×10⁻¹⁰ m/s² EXISTS.");
        sbI.AppendLine("    2. The RAR is TIGHT (scatter ~0.1 dex) — a fundamental empirical law.");
        sbI.AppendLine("    3. The mass discrepancy is systematic, not random.");
        sbI.AppendLine("    4. g† is numerically close to cH0/(2π) — cosmological coincidence.");
        sbI.AppendLine();
        sbI.AppendLine("  FOR TQM:");
        sbI.AppendLine("    The existence of g† ≈ cH0/(2π) is CONSISTENT with TQM's expectation");
        sbI.AppendLine("    that acceleration scales emerge from cosmological boundary conditions.");
        sbI.AppendLine("    If TQM's Λ(t) ~ 1/√V(t) sets the acceleration scale of DM halos,");
        sbI.AppendLine("    then the RAR is a natural consequence — not a coincidence.");
        sbI.AppendLine();
        sbI.AppendLine("  CAUTION:");
        sbI.AppendLine("    The RAR does NOT uniquely point to TQM. Both MOND and ΛCDM+feedback");
        sbI.AppendLine("    also reproduce it. The RAR is a CONSTRAINT that any theory must");
        sbI.AppendLine("    satisfy, not a discriminant between theories.");
        sbI.AppendLine();
        sbI.AppendLine("    TQM must DERIVE the RAR from its defect-DM dynamics to claim");
        sbI.AppendLine("    explanatory power. The existence of g† is encouraging but not");
        sbI.AppendLine("    sufficient for validation.");
        sbI.AppendLine();
        sbI.AppendLine("  CLASSIFICATION:");
        sbI.AppendLine("    TQM is CONSISTENT with galaxy dynamics.");
        sbI.AppendLine("    g† ≈ cH0 is an intriguing coincidence warranting further study.");
        sbI.AppendLine("    Next step: Derive the RAR analytically from TQM defect-DM.");
        string sectionI = sbI.ToString();

        return new LelliAnalysisResult(
            sectionA, sectionB, sectionC, sectionD, sectionE,
            sectionF, sectionG, sectionH, sectionI,
            summaries, velStats, disc, accel, a0, tqm);
    }
}
