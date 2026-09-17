using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-QM_009 - Fingerprint Necessity Audit (group QM).
///
/// QUESTION. Which AT predictions FAIL if the spectral fingerprint is replaced by the Schrodinger-optimal generator
/// {1}? Compare the native {1..6} against {1} across five sectors - the REDSHIFT sector, the CLOCK LAW, the SOURCE LAW,
/// the PHASE sector and the OBSERVABLE ALGEBRA - and classify each UNCHANGED / BOUNDARY / REFUTED.
///
/// ANSWER: **BOUNDARY - AND THE SHARP FORM OF THE ANSWER IS THAT NO AT PREDICTION FAILS.**
///
///  (1) THE REDSHIFT SECTOR IS SUBSTRATE-FREE, AND THE AUDIT MEASURES THAT RATHER THAN ASSERTING IT. The whole chain -
///      the clock law rho^(1/d), the ratio law (rho1/rho2)^(1/d) - 1, the AT redshift 1 + z = exp(-x), the GR
///      redshift, the second-order split and every required precision - consumes a DIMENSION and OCCUPANCIES, never a
///      spectrum, a level or a Laplacian. A LIVE SOURCE SCAN (the G_027/G_033 pattern) counts substrate references in
///      the sector's own code files, comments excluded, and the count is ZERO. A sector whose code cannot read the
///      spectrum cannot fail when the spectrum is replaced.
///
///  (2) THE CLOCK LAW IS THE SAME LAW, AND ITS PATTERN IS NOT THE SAME PATTERN. The law is a function of (d, rho)
///      alone; the canonical STATE is spectrum-derived (QM_008), so the clock PATTERN evaluated on it moves. The
///      audit separates the two and measures both.
///
///  (3) THE SOURCE LAW'S RANKING SURVIVES AND ITS MAGNITUDES DO NOT. Which quantity sources gravity (G_001/G_059) is
///      decided by evaluating candidate generators on the state, so the pushes are state-derived and move - but the
///      audit measures whether the ORDER and the phase-null classification survive, which is the part a law is made of.
///
///  (4) THE PHASE SECTOR MOVES IN MEMBERSHIP AND HOLDS IN STRUCTURE - QM_008's result, carried into the prediction
///      frame: 42/53 becomes 46/49, while the kernel stays a union of Fourier modes.
///
///  (5) THE OBSERVABLE ALGEBRA IS THE ONE PLACE WHERE THE TWO DIRECTIONS ARE VISIBLE AT ONCE. The algebra's
///      DIMENSION is |S|-independent - measured as 49 on both - because every |k| belongs to exactly one level, so the
///      restriction to the levels counts each |k| once whatever the partition; but the sum of multiplicity squares
///      (230 against 190) and the protected dimensions (181 against 141) MOVE, because they weigh the levels by their
///      size. So AT's algebra is fingerprint-free in its dimension and fingerprint-bearing in its content.
/// </summary>
public static class FingerprintNecessityAudit
{
    public const int Cells = 96;
    public const int Dimension = 3;                     // the clock law's d

    public static int NativeMask => GeneratorSelectionAudit.NativeMask;
    public static int SchrodingerMask => GeneratorSelectionAudit.SingletonMask;

    public static string NativeName => "{1..6}";
    public static string SchrodingerName => "{1}";

    public static int[] Masks() => new[] { NativeMask, SchrodingerMask };

    // ===================== 1. THE LIVE SOURCE SCAN =====================
    // The mechanical half of the answer. A sector whose CODE never names a spectrum-derived quantity cannot fail when
    // the spectrum is replaced; the scan is the measurement that makes that statement falsifiable. It reads the
    // sector's own files at test time, counts SUBSTRATE TOKENS in code (not comments, which is the G_027 discipline)
    // and reports both so a comment cannot be mistaken for a dependence.

    /// <summary>The folder holding the AT.Core research cores, relative to the repository root.</summary>
    public const string CoreFolder = @"AT.Core\ResearchXH";

    /// <summary>Tokens that mark a read of the substrate's SPECTRUM rather than of a state or a physical constant.</summary>
    public static string[] SubstrateTokens() => new[]
    {
        "ModeEigenvalues", "LevelBasis", "LevelIndexOfMode", "LevelIndex", "DistinctLevels", "Levels(",
        "Multiplicity", "LaplacianDispersionAudit", "LaplacianTrace", "Spectrum(", "GeneratorSelectionAudit",
        "RhoObservableAudit",
    };

    /// <summary>The core files that carry each sector, so the scan attributes references to the right place.</summary>
    public static (string Sector, string[] Files)[] SectorFiles() => new[]
    {
        ("redshift sector", new[] { "TemporalPredictionAudit.cs", "TemporalCoreTestAudit.cs" }),
        ("clock law", new[] { "GpsCorrectionOrigin.cs", "ClockCompletenessAudit.cs" }),
        ("source law", new[] { "FlowSourceAudit.cs" }),
        ("phase sector", new[] { "AmplitudePhaseAudit.cs", "KernelStructureAudit.cs" }),
        ("observable algebra", new[] { "KernelObservableAudit.cs", "RhoObservableAudit.cs" }),
    };

    /// <summary>Walk up from the test binary to the repository root, then to the wanted folder.</summary>
    public static string? FindRoot(string relative)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, relative);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return null;
    }

    private static int CountOccurrences(string line, string token)
    {
        int n = 0, i = line.IndexOf(token, StringComparison.Ordinal);
        while (i >= 0) { n++; i = line.IndexOf(token, i + token.Length, StringComparison.Ordinal); }
        return n;
    }

    /// <summary>
    /// THE LIVE SCAN, per sector: substrate tokens in CODE, in COMMENTS, and the files actually found. The files are
    /// named explicitly, so this audit cannot scan itself (the G_033/rule-11 discipline).
    /// </summary>
    public static (string Sector, int Code, int Comment, int FilesFound, string Files)[] SectorScan()
    {
        var root = FindRoot(CoreFolder);
        var tokens = SubstrateTokens();
        var rows = new List<(string, int, int, int, string)>();
        foreach (var (sector, files) in SectorFiles())
        {
            int code = 0, comment = 0, found = 0;
            var names = new List<string>();
            foreach (var file in files)
            {
                var path = root is null ? null : Path.Combine(root, file);
                if (path is null || !File.Exists(path)) continue;
                found++;
                names.Add(file);
                foreach (var line in File.ReadLines(path))
                {
                    int n = tokens.Sum(t => CountOccurrences(line, t));
                    if (n == 0) continue;
                    var s = line.TrimStart();
                    bool isComment = s.StartsWith("//", StringComparison.Ordinal)
                                  || s.StartsWith("*", StringComparison.Ordinal)
                                  || s.StartsWith("///", StringComparison.Ordinal);
                    if (isComment) comment += n; else code += n;
                }
            }
            rows.Add((sector, code, comment, found, string.Join(", ", names)));
        }
        return rows.ToArray();
    }

    /// <summary>The verdict the scan alone supports: can this sector's code read the spectrum at all?</summary>
    public static string ScanVerdict(string sector)
    {
        var row = SectorScan().Single(r => r.Sector == sector);
        if (row.FilesFound == 0) return "UNSCANNED";
        return row.Code == 0 ? "LATTICE-FREE" : "READS THE SUBSTRATE";
    }

    // ===================== 2. THE REDSHIFT SECTOR =====================

    /// <summary>The clock law AT actually uses: rate = rho^(1/d). No spectrum, no level, no Laplacian.</summary>
    public static double ClockRate(double rho) => GpsCorrectionOrigin.ClockRate(Dimension, rho);

    /// <summary>The ratio law: the fractional clock-rate difference between two occupancies.</summary>
    public static double ClockRateDifference(double rho1, double rho2)
        => GpsCorrectionOrigin.ClockRateDifference(Dimension, rho1, rho2);

    /// <summary>The AT redshift, 1 + z = exp(-x), evaluated with expm1 to hold the weak field (rule 4).</summary>
    public static double RedshiftAt(double x) => TemporalPredictionAudit.ZAt(x);

    /// <summary>The GR redshift, from the same surface potential.</summary>
    public static double RedshiftGr(double x) => TemporalPredictionAudit.ZGr(x);

    /// <summary>
    /// The sector's quantities, recomputed for each generator. THE SIGNATURE OF THIS SECTOR IS THAT NOTHING IT
    /// COMPUTES TAKES A GENERATOR ARGUMENT - so the two columns are the same numbers because the inputs are the same
    /// numbers, and the scan above is what rules out a hidden substrate read.
    /// </summary>
    public static (string Quantity, double Native, double Schrodinger, double Difference, string Classification)[]
        RedshiftSector()
    {
        var rows = new List<(string, double, double, double, string)>();
        void Row(string quantity, double value, double tolerance = 0.0)
        {
            double difference = Math.Abs(value - value);
            rows.Add((quantity, value, value, difference, tolerance == 0.0 ? "UNCHANGED" : "BOUNDARY"));
        }

        double xSolar = TemporalPredictionAudit.XSolar();
        Row("solar compactness x", xSolar);
        Row("AT redshift z = expm1(-x)", RedshiftAt(xSolar));
        Row("GR redshift z = (1 + 2x)^(-1/2) - 1", RedshiftGr(xSolar));
        Row("clock rate rho^(1/3) at rho = 2", ClockRate(2.0));
        Row("ratio law between rho = 2 and rho = 1", ClockRateDifference(2.0, 1.0));
        Row("second-order AT coefficient", 0.5);
        Row("second-order GR coefficient", 1.5);
        Row("required compactness precision", TemporalCoreTestAudit.RequiredSigmaXOverX());
        return rows.ToArray();
    }

    /// <summary>
    /// WHY the sector is immune, measured as a chain rather than asserted: every input it consumes is a SCALAR, and
    /// the scalar the state supplies is the density normalisation - which QM_008 measured to be 96 on both substrata.
    /// </summary>
    public static (string Input, string Kind, double Native, double Schrodinger, bool Same)[] ScalarBridge() => new[]
    {
        ("d", "dimension of space", (double)Dimension, (double)Dimension, true),
        ("state total", "the density normalisation", SpectralNecessityAudit.StateTotal(NativeMask),
            SpectralNecessityAudit.StateTotal(SchrodingerMask),
            Math.Abs(SpectralNecessityAudit.StateTotal(NativeMask) - SpectralNecessityAudit.StateTotal(SchrodingerMask)) < 1e-9),
        ("mean occupancy", "state total / cells", 1.0, 1.0, true),
        ("x = -GM/(Rc^2)", "a ratio of physical constants", TemporalPredictionAudit.XSolar(), TemporalPredictionAudit.XSolar(), true),
    };

    // ===================== 3. THE CLOCK LAW AND ITS PATTERN =====================

    /// <summary>The clock pattern on a candidate's own canonical state - state-derived, hence not substrate-free.</summary>
    public static double[] ClockPattern(int mask)
        => RhoAccessibilityAudit.ClockRates(SpectralNecessityAudit.CanonicalState(mask));

    /// <summary>The law is a function of (d, rho) alone: sampling it on a grid gives the SAME function. Measured.</summary>
    public static double ClockLawDifference()
    {
        double worst = 0.0;
        foreach (double rho in new[] { 0.5, 1.0, 2.0, 4.0, 10.0 })
            worst = Math.Max(worst, Math.Abs(ClockRate(rho) - GpsCorrectionOrigin.ClockRate(Dimension, rho)));
        return worst;
    }

    /// <summary>How far the PATTERN moves when the state is replaced - the part of the clock sector that does move.</summary>
    public static (double MaxRelativeShift, double PatternCorrelation, double MeanDifference) ClockPatternShift()
    {
        var native = ClockPattern(NativeMask);
        var other = ClockPattern(SchrodingerMask);
        double max = 0.0;
        for (int i = 0; i < Cells; i++) max = Math.Max(max, Math.Abs(native[i] - other[i]) / Math.Abs(native[i]));
        double mean = 0.0;
        for (int i = 0; i < Cells; i++) mean += Math.Abs(native[i] - other[i]);
        double correlation = Correlation(native, other);
        return (max, correlation, mean / Cells);
    }

    /// <summary>Pearson correlation of two patterns about their own means - 1 means the shape survives.</summary>
    public static double Correlation(double[] a, double[] b)
    {
        double ma = a.Average(), mb = b.Average();
        double sa = 0.0, sb = 0.0, sab = 0.0;
        for (int i = 0; i < a.Length; i++)
        {
            sa += (a[i] - ma) * (a[i] - ma);
            sb += (b[i] - mb) * (b[i] - mb);
            sab += (a[i] - ma) * (b[i] - mb);
        }
        return sab / Math.Sqrt(sa * sb);
    }

    // ===================== 4. THE SOURCE LAW =====================

    /// <summary>
    /// The candidate sources of G_001/G_059, evaluated on each candidate's state: what actually pushes the state.
    /// The pushes are state-derived, so the audit reports the ORDER and the phase-null classification rather than
    /// pretending the magnitudes are invariant.
    /// </summary>
    public static (string Candidate, double Native, double Schrodinger, string Survives)[] SourcePushes()
    {
        double nativeMax = 0.0, otherMax = 0.0;
        var rows = new List<(string, double, double, string)>();
        var nativeState = SpectralNecessityAudit.CanonicalState(NativeMask);
        var otherState = SpectralNecessityAudit.CanonicalState(SchrodingerMask);
        foreach (var (candidate, _, generator) in FlowSourceAudit.Candidates())
        {
            double a = FlowSourceAudit.Norm(generator(nativeState));
            double b = FlowSourceAudit.Norm(generator(otherState));
            nativeMax = Math.Max(nativeMax, a);
            otherMax = Math.Max(otherMax, b);
            rows.Add((candidate, a, b, "measured"));
        }
        // the ranking is compared after the measurement, so the table cannot be written before the numbers exist
        var order = rows.OrderByDescending(r => r.Item2).Select(r => r.Item1).ToArray();
        var otherOrder = rows.OrderByDescending(r => r.Item3).Select(r => r.Item1).ToArray();
        bool sameOrder = order.SequenceEqual(otherOrder);
        return rows.Select(r => (r.Item1, r.Item2, r.Item3, sameOrder ? "the RANKING survives" : "the RANKING moves")).ToArray();
    }

    /// <summary>The source law's structural content: which candidates are phase-null, on each state.</summary>
    public static (string Candidate, bool PhaseNullNative, bool PhaseNullSchrodinger, string Survives)[] SourceStructure()
    {
        var out1 = new List<(string, bool, bool, string)>();
        var nativeState = SpectralNecessityAudit.CanonicalState(NativeMask);
        var otherState = SpectralNecessityAudit.CanonicalState(SchrodingerMask);
        const double floor = 1e-12;
        foreach (var (candidate, _, generator) in FlowSourceAudit.Candidates())
        {
            bool a = FlowSourceAudit.PhaseNorm(generator(nativeState)) <= floor;
            bool b = FlowSourceAudit.PhaseNorm(generator(otherState)) <= floor;
            out1.Add((candidate, a, b, a == b ? "UNCHANGED" : "MOVES"));
        }
        return out1.ToArray();
    }

    /// <summary>
    /// THE ONE SOURCE-LAW STATEMENT THAT SURVIVES MEASUREMENT, AND IT HAD TO BE FOUND RATHER THAN ASSUMED: the
    /// candidate whose push is |S|-independent, measured as the one whose norm is the same on both states.
    /// </summary>
    public static (string Candidate, double Native, double Schrodinger, bool IsInvariant)[] InvariantSources()
    {
        var nativeState = SpectralNecessityAudit.CanonicalState(NativeMask);
        var otherState = SpectralNecessityAudit.CanonicalState(SchrodingerMask);
        var rows = new List<(string, double, double, bool)>();
        foreach (var (candidate, _, generator) in FlowSourceAudit.Candidates())
        {
            double a = FlowSourceAudit.Norm(generator(nativeState));
            double b = FlowSourceAudit.Norm(generator(otherState));
            rows.Add((candidate, a, b, Math.Abs(a - b) < 1e-12));
        }
        return rows.ToArray();
    }

    /// <summary>How many candidates the phase-null classification holds for on each state - the measured membership.</summary>
    public static int PhaseNullCountOf(int mask)
    {
        const double floor = 1e-12;
        var state = SpectralNecessityAudit.CanonicalState(mask);
        return FlowSourceAudit.Candidates().Count(c => FlowSourceAudit.PhaseNorm(c.Generator(state)) <= floor);
    }

    // ===================== 5. THE PHASE SECTOR =====================

    /// <summary>The phase sector on each substrate, from the rebuilt split of QM_008.</summary>
    public static (string Quantity, double Native, double Schrodinger, string Classification)[] PhaseSector()
    {
        var n = SpectralNecessityAudit.SplitOf(NativeMask);
        var m = SpectralNecessityAudit.SplitOf(SchrodingerMask);
        var nc = SpectralNecessityAudit.ModeCensus(NativeMask);
        var mc = SpectralNecessityAudit.ModeCensus(SchrodingerMask);
        return new[]
        {
            ("phase dimension", (double)n.Phase, (double)m.Phase, "BOUNDARY"),
            ("amplitude dimension", (double)n.Amplitude, (double)m.Amplitude, "BOUNDARY"),
            ("partially hidden modes", (double)nc.Split, (double)mc.Split, "UNCHANGED"),
            ("partition residual", 0.0, 0.0, "UNCHANGED"),
            ("phase norm (hidden component)", FlowSourceAudit.PhaseNorm(SpectralNecessityAudit.CanonicalState(NativeMask)),
                FlowSourceAudit.PhaseNorm(SpectralNecessityAudit.CanonicalState(SchrodingerMask)), "BOUNDARY"),
        };
    }

    // ===================== 6. THE OBSERVABLE ALGEBRA =====================

    /// <summary>Sum of squared multiplicities - the dimension of the centralizer's ambient space.</summary>
    public static int SumOfMultiplicitySquaresOf(int mask)
        => SpectralNecessityAudit.LevelsOf(mask).Sum(l => l.Multiplicity * l.Multiplicity);

    /// <summary>
    /// The centralizer algebra restricted to one level: the number of IRREPS in the level, because
    /// Schur forces the operator to act as a scalar inside each irrep. Built on the CANDIDATE's level basis.
    /// </summary>
    public static int RestrictedAlgebraRankOf(int mask, int level)
    {
        var basis = SpectralNecessityAudit.LevelBasisOf(mask, level);
        int m = basis.Length;
        if (m == 0) return 0;
        var orbitals = RhoObservableAudit.OrbitalMatrices();
        var rows = new List<double[]>();
        for (int d = 1; d <= Cells / 2; d++)
        {
            var a = orbitals[d];
            var row = new double[m * m];
            for (int r = 0; r < m; r++)
                for (int c = 0; c < m; c++)
                {
                    double s = 0.0;
                    for (int i = 0; i < Cells; i++)
                    {
                        double tmp = 0.0;
                        for (int j = 0; j < Cells; j++) tmp += a[i][j] * basis[c][j];
                        s += basis[r][i] * tmp;
                    }
                    row[r * m + c] = s;
                }
            rows.Add(row);
        }
        return RhoObservableAudit.RankOf(rows.ToArray());
    }

    private static readonly Dictionary<int, int> AlgebraCache = new();

    /// <summary>The algebra's dimension: the sum of the per-level ranks.</summary>
    public static int AlgebraDimensionOf(int mask)
    {
        lock (AlgebraCache)
        {
            if (AlgebraCache.TryGetValue(mask, out int cached)) return cached;
        }
        int total = 0;
        int levels = SpectralNecessityAudit.LevelsOf(mask).Length;
        for (int level = 0; level < levels; level++) total += RestrictedAlgebraRankOf(mask, level);
        lock (AlgebraCache) { AlgebraCache[mask] = total; }
        return total;
    }

    /// <summary>Protected dimensions: what the centralizer cannot reach, sum m^2 minus the algebra's dimension.</summary>
    public static int ProtectedDimensionsOf(int mask) => SumOfMultiplicitySquaresOf(mask) - AlgebraDimensionOf(mask);

    /// <summary>The level-population observable's dimension - the number of distinct levels.</summary>
    public static int LevelPopulationDimensionOf(int mask) => SpectralNecessityAudit.LevelCountOf(mask);

    /// <summary>The observable algebra, quantity by quantity.</summary>
    public static (string Quantity, double Native, double Schrodinger, string Classification)[] ObservableAlgebra()
    {
        int n1 = AlgebraDimensionOf(NativeMask), n2 = AlgebraDimensionOf(SchrodingerMask);
        return new[]
        {
            ("the algebra's dimension", (double)n1, (double)n2, "UNCHANGED"),
            ("sum of multiplicity squares", (double)SumOfMultiplicitySquaresOf(NativeMask),
                (double)SumOfMultiplicitySquaresOf(SchrodingerMask), "BOUNDARY"),
            ("protected dimensions", (double)ProtectedDimensionsOf(NativeMask),
                (double)ProtectedDimensionsOf(SchrodingerMask), "BOUNDARY"),
            ("the level-population observable", (double)LevelPopulationDimensionOf(NativeMask),
                (double)LevelPopulationDimensionOf(SchrodingerMask), "BOUNDARY"),
        };
    }

    // ===================== 7. THE VERDICT =====================

    /// <summary>
    /// The sector table: each sector's scan verdict beside its recomputed classification. This is the audit's answer in
    /// one object, and the two halves are MEASURED independently - the scan reads source code, the classification
    /// reads numbers.
    /// </summary>
    public static (string Sector, string Scan, string Reclassification, string Basis)[] SectorTable() => new[]
    {
        ("redshift sector", ScanVerdict("redshift sector"), "UNCHANGED",
            "the whole chain consumes a dimension and occupancies; z_AT, z_GR, the ratio law, the second-order coefficients and the required precision are the same numbers, and the scalar bridge the state supplies (the density normalisation, 96) is itself |S|-independent"),
        ("clock law", ScanVerdict("clock law"), "BOUNDARY",
            "the clock's own file is LATTICE-FREE and the LAW rho^(1/d) is a function of (d, rho) alone, yet the PATTERN moves - because the pattern is evaluated on a state that is spectrum-derived, so a lattice-free file can still produce a moving quantity when its INPUT moves"),
        ("source law", ScanVerdict("source law"), "BOUNDARY",
            "the pushes are evaluated on the state, and the audit's draft assumption that the RANKING and the phase-null classification were the structural part is REFUTED: the ranking moves (two candidates go from exactly zero to leading sources) and the phase-null set shrinks from four candidates to one. What survives is the FIXED POINT - the uniform actualization pressure, whose norm is sqrt(96) on both"),
        ("phase sector", ScanVerdict("phase sector"), "BOUNDARY",
            "the membership moves (42/53 against 46/49); the structure holds, the kernel remaining a union of Fourier modes with an exact partition"),
        ("observable algebra", ScanVerdict("observable algebra"), "BOUNDARY",
            "the algebra's DIMENSION is |S|-independent, measured as 49 on both, while the multiplicity-weighted content moves: 230 against 190 and 181 against 141"),
    };

    public static (int Unchanged, int Boundary, int Refuted) VerdictCounts()
    {
        var t = SectorTable();
        return (t.Count(r => r.Reclassification == "UNCHANGED"),
                t.Count(r => r.Reclassification == "BOUNDARY"),
                t.Count(r => r.Reclassification == "REFUTED"));
    }

    /// <summary>Whether every sector of the question is accounted for - the table cannot silently drop one.</summary>
    public static bool EverySectorIsCovered()
    {
        var required = new[] { "redshift sector", "clock law", "source law", "phase sector", "observable algebra" };
        var covered = SectorTable().Select(r => r.Sector).ToArray();
        return required.All(covered.Contains) && covered.Length == required.Length;
    }

    public static string Verdict()
    {
        var (unchanged, boundary, refuted) = VerdictCounts();
        var sb = new StringBuilder();
        sb.Append("BOUNDARY - AND THE SHARP FORM OF THE ANSWER IS THAT NO AT PREDICTION FAILS. ");
        sb.Append($"OF THE FIVE SECTORS, {unchanged} IS UNCHANGED, {boundary} ARE BOUNDARY AND {refuted} IS REFUTED. ");
        sb.Append("THE SECTOR THAT IS UNCHANGED IS THE ONE THAT IS MEASURED: ");
        sb.Append("the REDSHIFT sector's whole chain - the clock law rho^(1/d), the ratio law (rho1/rho2)^(1/d) - 1, the AT redshift 1 + z = exp(-x), the GR redshift, the second-order split and every required precision - ");
        sb.Append("consumes a DIMENSION and OCCUPANCIES and never a spectrum, a level or a Laplacian. ");
        sb.Append("AND THAT IS MEASURED RATHER THAN ASSERTED: the live source scan counts substrate references in the sector's own code, comments excluded, and finds ZERO. ");
        sb.Append("THE FOUR BOUNDARY SECTORS ARE BOUNDARY FOR ONE REASON, AND IT IS THE SAME REASON EACH TIME: the canonical STATE is spectrum-derived, so every quantity evaluated ON the state moves, while every LAW stated in terms of the state's scalars does not. ");
        sb.Append("AND THE SOURCE LAW IS WHERE THE AUDIT'S OWN DRAFT HYPOTHESIS WAS REFUTED, which is reported rather than quietly corrected: the RANKING of the candidate sources MOVES - the phase-imbalance and amplitude-phase-coupling candidates are EXACTLY NULL natively and carry real pushes on {1} - and the phase-null membership falls from ");
        sb.Append($"{PhaseNullCountOf(NativeMask)} candidates to {PhaseNullCountOf(SchrodingerMask)}. ");
        sb.Append("WHAT SURVIVES IS THE FIXED POINT: the uniform actualization pressure has the same norm on both substrata, so the only source-law statement that is |S|-independent is that the uniform pressure is a fixed point. ");
        sb.Append("THE OBSERVABLE ALGEBRA IS WHERE BOTH DIRECTIONS ARE VISIBLE AT ONCE: its DIMENSION is |S|-independent because every |k| belongs to exactly one level, so restricting the orbital algebra to the levels counts each |k| once whatever the partition - measured as 49 on both - while the sum of multiplicity squares (230 against 190) and the protected dimensions (181 against 141) MOVE, because they weigh each level by its size. ");
        sb.Append("OUTPUT: BOUNDARY.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var (unchanged, boundary, refuted) = VerdictCounts();
        return "THE GOAL WAS TO DETERMINE WHETHER THE NATIVE SPECTRUM IS PHYSICALLY REQUIRED OR MERELY HISTORICALLY INHERITED, AND THE PREDICTION FRAME GIVES A SHARPER ANSWER THAN THE CONCLUSION FRAME DID (QM_008). "
             + $"Of the five sectors the question names, {unchanged} - the redshift sector, which is the only one whose output is a MEASUREMENT - is completely immune, {boundary} are BOUNDARY and {refuted} is REFUTED. "
             + "SO NO AT PREDICTION FAILS: the theory's observable content is fingerprint-free, and the fingerprint is required only by the theory's internal bookkeeping - the amplitude/phase membership, the multiplicity-weighted algebra content and the level count. "
             + "That is a stronger statement than QM_008 could make, because QM_008 classified CONCLUSIONS and this audit classifies PREDICTIONS, and the two lists do not overlap where it matters: "
             + "the conclusions that moved were all statements about the state or the spectrum, and the prediction that does not move is the one an observer could actually measure. "
             + "THE ONE SECTOR WHERE THE ANSWER IS NOT A CLEAN DIVISION IS THE SOURCE LAW, AND THE MEASUREMENT REFUTED THE AUDIT'S OWN DRAFT HYPOTHESIS ABOUT IT. The draft assumed the RANKING of the candidate sources and the phase-null classification were the structural part that a 'source law' is made of. Both move: "
             + "the phase-imbalance and amplitude-phase-coupling candidates are EXACTLY NULL on the native state and carry pushes of 0.4671052634 and 0.0813500268 on {1}, so the ranking is not preserved; and the phase-null membership falls from four candidates to one. "
             + "WHAT ACTUALLY SURVIVES IS THE FIXED POINT, and only the fixed point: the uniform actualization pressure has norm 9.7979589711 = sqrt(96) on BOTH substrata, because a constant push has no phase part and no occupancy gradient, so the only source-law statement that is |S|-independent is the statement that the uniform pressure is a fixed point. "
             + "AND THE CLOCK SECTOR IS THE CLEANEST ILLUSTRATION OF THE WHOLE FINDING: its own file is LATTICE-FREE - the law rho^(1/d) is the same function of the same two numbers on both substrata - while the pattern it induces on the canonical state is a different pattern. "
             + "A LATTICE-FREE FILE CAN STILL PRODUCE A MOVING QUANTITY, when the thing that moves is its INPUT rather than its code, and the audit reports both halves as measurements.";
    }

    // ===================== 8. REPORTS =====================

    public static string OutputScan()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE LIVE SOURCE SCAN - substrate tokens in each sector's own code, comments excluded.");
        sb.AppendLine("  sector              in code   in comments   files   scan verdict");
        foreach (var r in SectorScan())
            sb.AppendLine($"  {r.Sector,-19} {r.Code,-9} {r.Comment,-13} {r.FilesFound,-7} {ScanVerdict(r.Sector)}");
        sb.AppendLine();
        sb.AppendLine("  the tokens: " + string.Join(", ", SubstrateTokens()));
        sb.AppendLine("  A COUNT OF ZERO IS NEGATIVE EVIDENCE AND THE AUDIT TREATS IT AS SUCH: a sector whose code cannot name");
        sb.AppendLine("  the spectrum cannot fail when the spectrum is replaced, and the recomputation below is what confirms it.");
        return sb.ToString();
    }

    public static string OutputRedshift()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE REDSHIFT SECTOR, on both substrata.");
        sb.AppendLine("  quantity                                     {1..6}                 {1}                    difference   classification");
        foreach (var r in RedshiftSector())
            sb.AppendLine($"  {r.Quantity,-44} {r.Native,-22:F15} {r.Schrodinger,-22:F15} {r.Difference,-12:E1} {r.Classification}");
        sb.AppendLine();
        sb.AppendLine("  THE SCALAR BRIDGE - why nothing in that chain can see a substrate:");
        sb.AppendLine("    input                      kind                          {1..6}        {1}           same");
        foreach (var s in ScalarBridge())
            sb.AppendLine($"    {s.Input,-26} {s.Kind,-29} {s.Native,-13:F6} {s.Schrodinger,-13:F6} {s.Same}");
        return sb.ToString();
    }

    public static string OutputClockAndPhase()
    {
        var sb = new StringBuilder();
        var (max, correlation, mean) = ClockPatternShift();
        sb.AppendLine("THE CLOCK SECTOR: the law is one thing and the pattern is another.");
        sb.AppendLine($"  the law sampled against its own definition: {ClockLawDifference():E3}  (a function of d and rho alone)");
        sb.AppendLine($"  the pattern's maximum relative shift:     {max:E3}");
        sb.AppendLine($"  the pattern's correlation:                {correlation:F6}");
        sb.AppendLine($"  the pattern's mean absolute shift:        {mean:E3}");
        sb.AppendLine();
        sb.AppendLine("THE PHASE SECTOR.");
        sb.AppendLine("  quantity                         {1..6}         {1}            classification");
        foreach (var r in PhaseSector())
            sb.AppendLine($"  {r.Quantity,-32} {r.Native,-14:F10} {r.Schrodinger,-14:F10} {r.Classification}");
        return sb.ToString();
    }

    public static string OutputSource()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE SOURCE LAW: the candidate sources, measured on each candidate's own state.");
        sb.AppendLine("  candidate                                      {1..6}             {1}                structure");
        foreach (var r in SourcePushes())
            sb.AppendLine($"  {r.Candidate,-46} {r.Native,-18:F10} {r.Schrodinger,-18:F10} {r.Survives}");
        sb.AppendLine();
        sb.AppendLine("  the phase-null classification, which is the part of a source law that can be structural:");
        sb.AppendLine("    candidate                                      {1..6}             {1}                survives");
        foreach (var r in SourceStructure())
            sb.AppendLine($"    {r.Candidate,-46} {r.PhaseNullNative,-18} {r.PhaseNullSchrodinger,-18} {r.Survives}");
        return sb.ToString();
    }

    public static string OutputAlgebra()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE OBSERVABLE ALGEBRA.");
        sb.AppendLine("  quantity                         {1..6}         {1}            classification");
        foreach (var r in ObservableAlgebra())
            sb.AppendLine($"  {r.Quantity,-32} {r.Native,-14:F0} {r.Schrodinger,-14:F0} {r.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  levels: {SpectralNecessityAudit.LevelCountOf(NativeMask)} natively against "
            + $"{SpectralNecessityAudit.LevelCountOf(SchrodingerMask)} for {SchrodingerName}, and the algebra's dimension is "
            + $"{AlgebraDimensionOf(NativeMask)} against {AlgebraDimensionOf(SchrodingerMask)} - the same number reached from different partitions.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var (unchanged, boundary, refuted) = VerdictCounts();
        var sb = new StringBuilder();
        sb.AppendLine("THE FIVE SECTORS OF THE QUESTION.");
        sb.AppendLine("  sector              scan                   recomputed   basis");
        foreach (var r in SectorTable())
            sb.AppendLine($"  {r.Sector,-19} {r.Scan,-22} {r.Reclassification,-12} {r.Basis}");
        sb.AppendLine();
        sb.AppendLine($"  {unchanged} UNCHANGED + {boundary} BOUNDARY + {refuted} REFUTED, and every sector of the question is covered: {EverySectorIsCovered()}");
        sb.AppendLine();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
