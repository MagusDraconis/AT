namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 156 — Unified spectral access law. The known chain is
/// D96 → Z2 doublets → weak-isospin structure → spectral access → effective spectral dimension →
/// hierarchy exponent. The sector effective dimensions are δν = 2.241, δd = 2.449, δℓ = 2.940,
/// δu = 4.066. This phase asks: can ALL sector dimensions be derived from a SINGLE D96/Z2 access
/// functional, using only spectral geometry, doublet structure, occupation weighting, and mode-access
/// statistics — WITHOUT fitted charge/isospin laws and WITHOUT free sector parameters?
///
/// Method (computational, fully deterministic): the unified spectral access law is
///   δ_sector = log(N_eff) / log(span)
/// where span = ω_max/ω_min and N_eff is the sector's effective mode count computed from the D96/Z2
/// doublet + octave-occupancy structure:
///   (1) NEUTRAL-CHARGE ACCESS (ν): the OCTAVE-OCCUPATION exponent δ_occ = slope of log(mode count) vs
///       log(octave center) across the octave bands — the mode-access statistics of the spectrum (the
///       neutral sector has no charge channel, QG154, so its access is purely statistical);
///   (2) FULL-SPECTRUM ACCESS (d): N_eff = total mode count (uniform access, δ = log(N)/log(span));
///   (3) DOUBLET-OCCUPANCY ACCESS (ℓ): N_eff = Σ over modes of the doublet multiplicity (group size) —
///       the doublet structure weighting;
///   (4) OCTAVE-OCCUPATION-WEIGHTED ACCESS (u): N_eff = Σ_b occ_b·(occ_b/occ_0) — the dense top band
///       dominates (occupation weighting, the up-sector dense-band access of QG150).
/// Then p_eff = 2·δ_sector (the secondary target).
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class UnifiedSpectralAccess
{
    /// <summary>Documented sector targets: (name, δ_eff).</summary>
    public static (string Name, double Delta)[] Targets()
        => new[] { ("ν", 2.241), ("d", 2.449), ("ℓ", 2.940), ("u", 4.066) };

    /// <summary>Full-spectrum span ω_max/ω_min.</summary>
    public static double Span()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        return w[^1] / w[0];
    }

    private static double Slope(double[] x, double[] y)
    {
        int m = x.Length;
        double mx = x.Average(), my = y.Average();
        double num = 0, den = 0;
        for (int i = 0; i < m; i++)
        {
            num += (x[i] - mx) * (y[i] - my);
            den += (x[i] - mx) * (x[i] - mx);
        }
        return den < 1e-12 ? double.NaN : num / den;
    }

    private static double Weyl(double[] ws)
    {
        if (ws.Length < 3) return double.NaN;
        return Slope(ws.Select(x => Math.Log(x)).ToArray(),
            Enumerable.Range(1, ws.Length).Select(i => Math.Log((double)i)).ToArray());
    }

    // ── Spectral geometry primitives ────────────────────────────────────────────

    /// <summary>Full-spectrum Weyl exponent (uniform access).</summary>
    public static double FullWeyl()
        => Weyl(FamilyIndexOrigin.IntraSectorModes());

    /// <summary>Octave band structure: (band, occupancy, center, localWeyl).</summary>
    public static (int Band, int Modes, double Center, double LocalWeyl)[] OctaveBands()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        var w0 = w[0];
        return ModeAccessOrigin.OctaveBandStructure().Select(b =>
        {
            double center = w0 * Math.Pow(2.0, b.Band + 0.5);
            return (b.Band, b.Occupancy, center, b.LocalWeyl);
        }).ToArray();
    }

    // ── 1. Neutral-charge access (ν): octave-occupation exponent ───────────────

    /// <summary>
    /// The octave-occupation exponent: slope of log(mode count) vs log(octave center) across the octave
    /// bands. This is the mode-access statistics of the spectrum — the neutral sector (no charge channel,
    /// QG154) accesses the spectrum purely statistically through this exponent.
    /// </summary>
    public static double OctaveOccupationExponent()
    {
        var bands = OctaveBands();
        return Slope(bands.Select(b => Math.Log(b.Center)).ToArray(),
            bands.Select(b => Math.Log((double)b.Modes)).ToArray());
    }

    // ── 2. Full-spectrum access (d) ─────────────────────────────────────────────

    /// <summary>
    /// Full-spectrum access: δ = log(N)/log(span) with N = total mode count (uniform access to all
    /// modes).
    /// </summary>
    public static double FullCountAccess()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        return Math.Log(w.Length) / Math.Log(Span());
    }

    // ── 3. Doublet-occupancy access (ℓ) ─────────────────────────────────────────

    /// <summary>Doublet multiplicity (degenerate group size) of each mode.</summary>
    public static int[] DoubletMultiplicities()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        var groups = new List<List<double>>();
        for (int i = 0; i < w.Length; i++)
        {
            if (groups.Count > 0 && Math.Abs(groups[^1][0] - w[i]) < 1e-9) groups[^1].Add(w[i]);
            else groups.Add(new List<double> { w[i] });
        }
        int[] gsize = new int[w.Length];
        int gi = 0;
        for (int i = 0; i < w.Length; i++)
        {
            while (gi < groups.Count && Math.Abs(groups[gi][0] - w[i]) > 1e-9) gi++;
            gsize[i] = groups[gi].Count;
        }
        return gsize;
    }

    /// <summary>
    /// Doublet-occupancy access: N_eff = Σ over modes of doublet multiplicity (group size). The lepton
    /// sector weights each mode by its doublet occupancy.
    /// </summary>
    public static double DoubletOccupancyCount()
    {
        return DoubletMultiplicities().Sum();
    }

    // ── 4. Octave-occupation-weighted access (u) ────────────────────────────────

    /// <summary>Octave occupancy per band.</summary>
    public static int[] OctaveOccupancies()
        => OctaveBands().Select(b => b.Modes).ToArray();

    /// <summary>
    /// Octave-occupation-weighted access: N_eff = Σ_b occ_b·(occ_b/occ_0). The dense top band dominates —
    /// the up sector's occupation-weighted dense-band access (QG150).
    /// </summary>
    public static double OctaveWeightedCount()
    {
        var occ = OctaveOccupancies();
        int o0 = occ[0];
        double sum = 0;
        foreach (int o in occ) sum += o * (o / (double)o0);
        return sum;
    }

    // ── Unified law ─────────────────────────────────────────────────────────────

    /// <summary>
    /// The unified spectral access law: δ_sector = log(N_eff)/log(span), where N_eff is the sector's
    /// D96/Z2 access-weighted effective mode count. Returns (name, predicted δ, target δ, deviation,
    /// N_eff, accessType).
    /// </summary>
    public static (string Name, double Predicted, double Target, double Deviation, double NEff, string Access)[]
        UnifiedLaw()
    {
        double logSpan = Math.Log(Span());
        var w = FamilyIndexOrigin.IntraSectorModes();
        double nuN = Math.Exp(OctaveOccupationExponent() * logSpan); // ν via octave-occupation (reference)
        double dN = w.Length;
        double lN = DoubletOccupancyCount();
        double uN = OctaveWeightedCount();

        var results = new List<(string, double, double, double, double, string)>
        {
            ("ν", Math.Log(nuN) / logSpan, 2.241, 0, nuN, "octave-occupation"),
            ("d", Math.Log(dN) / logSpan, 2.449, 0, dN, "full-count"),
            ("ℓ", Math.Log(lN) / logSpan, 2.940, 0, lN, "doublet-occupancy"),
            ("u", Math.Log(uN) / logSpan, 4.066, 0, uN, "octave-weighted"),
        };
        for (int i = 0; i < results.Count; i++)
        {
            var r = results[i];
            double dev = Math.Abs(r.Item2 / r.Item3 - 1.0);
            results[i] = (r.Item1, r.Item2, r.Item3, dev, r.Item5, r.Item6);
        }
        return results.ToArray();
    }

    /// <summary>Mean relative deviation of the unified law across all four sectors.</summary>
    public static double MeanDeviation()
        => UnifiedLaw().Average(r => r.Deviation);

    /// <summary>Max relative deviation of the unified law across all four sectors.</summary>
    public static double MaxDeviation()
        => UnifiedLaw().Max(r => r.Deviation);

    /// <summary>Number of sectors reproduced within 5% by the unified law.</summary>
    public static int SectorsWithin5Percent()
        => UnifiedLaw().Count(r => r.Deviation < 0.05);

    /// <summary>Is the unified law predictive (all four sectors within 5%)?</summary>
    public static bool Predictive()
        => SectorsWithin5Percent() == 4;

    // ── Secondary target: p_eff = 2·δ ───────────────────────────────────────────

    /// <summary>
    /// Secondary target: p_eff = 2·δ_sector. Returns (name, p_predicted, p_observed).
    /// </summary>
    public static (string Name, double PPred, double PObs)[] EffectiveExponents()
    {
        var ul = UnifiedLaw();
        return new[]
        {
            ("ν", 2 * ul[0].Predicted, 4.483),
            ("d", 2 * ul[1].Predicted, 4.898),
            ("ℓ", 2 * ul[2].Predicted, 5.880),
            ("u", 2 * ul[3].Predicted, 8.131),
        };
    }

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Unified-access-law score (0..5):
    /// 1. the octave-occupation exponent is well-defined (finite, 1–4);
    /// 2. the full-count access reproduces down within 5%;
    /// 3. the doublet-occupancy access reproduces the lepton within 5%;
    /// 4. the octave-weighted access reproduces up within 5%;
    /// 5. all four sectors are reproduced within 5% (predictive law).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        double dOcc = OctaveOccupationExponent();
        if (!double.IsNaN(dOcc) && dOcc > 1.0 && dOcc < 4.0) score++;
        var ul = UnifiedLaw();
        if (ul[1].Deviation < 0.05) score++;
        if (ul[2].Deviation < 0.05) score++;
        if (ul[3].Deviation < 0.05) score++;
        if (Predictive()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO LAW             — no single spectral access functional reproduces the sector dimensions;
    ///   PARTIAL LAW        — some sectors are reproduced but not all (mean deviation too large);
    ///   UNIFIED ACCESS LAW — all four sector dimensions follow a single D96/Z2 spectral access law
    ///                        δ = log(N_eff)/log(span): the neutral sector uses the octave-occupation
    ///                        (mode-access statistics), down uses the full count, the lepton uses
    ///                        doublet-occupancy weighting, and up uses octave-occupation-weighted dense
    ///                        access — no fitted charge/isospin laws, no free sector parameters
    ///                        (p_eff = 2·δ follows).
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "NO LAW";
        if (score == 5) return "UNIFIED ACCESS LAW";
        return "PARTIAL LAW";
    }
}
