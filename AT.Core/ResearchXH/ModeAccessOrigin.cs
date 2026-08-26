namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 150 — Origin of mode access. QG149 established that sector exponents emerge from
/// occupation-weighted mode access (down p_eff = 2×Weyl, up/down splitting = isospin-dependent access).
/// This phase asks: WHY do different particle sectors access different parts of the SAME spectrum?
///
/// Method (computational, fully deterministic): the observable sector's spectrum splits into octave bands
/// with distinct mode occupancies and local Weyl exponents. Each sector's effective dimension δ_eff = p_eff/2
/// implies a spectral accessibility (which part of the spectrum it couples to). We measure: (1) MODE-SELECTION
/// RULES — the octave band structure (occupancy and local Weyl per band) that a sector can select;
/// (2) CHARGE CONSTRAINTS — correlation of the sector effective dimension with electric charge; (3) ISOSPIN
/// CONSTRAINTS — correlation with weak isospin; (4) SPECTRAL ACCESSIBILITY — the fraction of the spectrum
/// implied by each sector's effective dimension (e.g. down δ_eff ≈ full Weyl ⇒ full-spectrum access);
/// (5) OCCUPATION MECHANISMS — the band-occupancy-weighted mode access: which sector couples to the dense
/// top band vs the sparse low bands.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class ModeAccessOrigin
{
    /// <summary>Documented sectors with effective spectral dimension δ_eff = p_eff/2 (QG147/149).</summary>
    public static (string Name, double DeltaEff, double Q, double T3)[] SectorDimensions()
        => new[]
        {
            ("leptons", 2.940, -1.0, -0.5),
            ("up", 4.066, +2.0 / 3.0, +0.5),
            ("down", 2.449, -1.0 / 3.0, -0.5),
        };

    // ── 1. Mode-selection rules ────────────────────────────────────────────────

    /// <summary>
    /// Octave band structure: (bandIndex, occupancy, localWeyl). The mode-selection rules available to a
    /// sector.
    /// </summary>
    public static (int Band, int Occupancy, double LocalWeyl)[] OctaveBandStructure()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        if (w.Length < 4) return Array.Empty<(int, int, double)>();
        double w0 = w[0];
        var result = new List<(int, int, double)>();
        for (int b = 0; b < 40; b++)
        {
            double lo = w0 * Math.Pow(2.0, b);
            double hi = w0 * Math.Pow(2.0, b + 1);
            var sel = w.Where(x => x >= lo - 1e-12 && x < hi).ToArray();
            if (sel.Length < 3) break;
            double d = LocalWeyl(sel);
            result.Add((b, sel.Length, d));
        }
        return result.ToArray();
    }

    private static double LocalWeyl(double[] sel)
    {
        if (sel.Length < 3) return double.NaN;
        var logW = sel.Select(x => Math.Log(x)).ToArray();
        var logN = Enumerable.Range(1, sel.Length).Select(i => Math.Log((double)i)).ToArray();
        double mx = logW.Average(), my = logN.Average();
        double num = 0, den = 0;
        for (int i = 0; i < sel.Length; i++)
        {
            num += (logW[i] - mx) * (logN[i] - my);
            den += (logW[i] - mx) * (logW[i] - mx);
        }
        return den < 1e-12 ? double.NaN : num / den;
    }

    /// <summary>Mode occupancy per octave band.</summary>
    public static int[] BandOccupancies()
        => OctaveBandStructure().Select(b => b.Occupancy).ToArray();

    /// <summary>Top-band occupancy fraction (dense band vs total).</summary>
    public static double TopBandFraction()
    {
        var occ = BandOccupancies();
        if (occ.Length == 0) return 0;
        return (double)occ[^1] / occ.Sum();
    }

    // ── 2. Charge constraints ──────────────────────────────────────────────────

    /// <summary>Correlation of the sector effective dimension with electric charge.</summary>
    public static double ChargeConstraint()
    {
        var d = SectorDimensions();
        return EffectiveSizeFamilies.Pearson(d.Select(s => s.Q).ToArray(),
            d.Select(s => s.DeltaEff).ToArray());
    }

    // ── 3. Isospin constraints ─────────────────────────────────────────────────

    /// <summary>Correlation of the sector effective dimension with weak isospin.</summary>
    public static double IsospinConstraint()
    {
        var d = SectorDimensions();
        return EffectiveSizeFamilies.Pearson(d.Select(s => s.T3).ToArray(),
            d.Select(s => s.DeltaEff).ToArray());
    }

    // ── 4. Spectral accessibility ──────────────────────────────────────────────

    /// <summary>
    /// Full-spectrum Weyl exponent. A sector whose δ_eff ≈ full Weyl accesses the ENTIRE spectrum.
    /// </summary>
    public static double FullWeyl()
        => PhysicalSectorExponentOrigin.FullWeyl();

    /// <summary>Deviation of the down sector's δ_eff from the full Weyl exponent.</summary>
    public static double DownFullSpectrumDeviation()
    {
        double down = SectorDimensions().First(s => s.Name == "down").DeltaEff;
        return Math.Abs(down / FullWeyl() - 1.0);
    }

    /// <summary>
    /// Spectral accessibility: does the down sector access the FULL spectrum (δ_eff ≈ full Weyl)? Returns
    /// the deviation.
    /// </summary>
    public static bool DownAccessesFullSpectrum()
        => DownFullSpectrumDeviation() < 0.05;

    // ── 5. Occupation mechanisms ───────────────────────────────────────────────

    /// <summary>
    /// Occupation mechanism: the up sector's elevated dimension (4.07) exceeds the full Weyl (2.47),
    /// implying access to the DENSE top band. Returns the up/full ratio.
    /// </summary>
    public static double UpDimensionalRatio()
    {
        double up = SectorDimensions().First(s => s.Name == "up").DeltaEff;
        return up / FullWeyl();
    }

    /// <summary>Does the up sector's dimension exceed the full-spectrum dimension (dense-band access)?</summary>
    public static bool UpAccessesDenseBand()
        => UpDimensionalRatio() > 1.3;

    // ── Origin score & classification ──────────────────────────────────────────

    /// <summary>
    /// Mode-access-origin score (0..5):
    /// 1. the octave band structure has multiple distinct occupancies (mode-selection rules exist);
    /// 2. the top band dominates the occupancy (&gt; 0.8);
    /// 3. the down sector accesses the full spectrum (δ_eff ≈ full Weyl);
    /// 4. the up sector accesses the dense band (dimension &gt; 1.3 × full);
    /// 5. the sector dimensions correlate with isospin (mode selection is quantum-number constrained).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        var occ = BandOccupancies();
        if (occ.Distinct().Count() >= 2) score++;
        if (TopBandFraction() > 0.8) score++;
        if (DownAccessesFullSpectrum()) score++;
        if (UpAccessesDenseBand()) score++;
        if (Math.Abs(IsospinConstraint()) > 0.5) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN        — no mode-selection rule explains which sector accesses which spectral region;
    ///   PARTIAL ORIGIN   — some sectors show a clear spectral-access mechanism (e.g. down = full spectrum,
    ///                      up = dense band) but the selection is not fully quantum-number constrained;
    ///   MODE-ACCESS ORIGIN — different particle sectors access different parts of the SAME spectrum because
    ///                      occupation-weighted mode access is quantum-number constrained: the down sector
    ///                      accesses the full spectrum, the up sector the dense band, and the selection
    ///                      correlates with isospin — the concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "NO ORIGIN";
        if (score == 5) return "MODE-ACCESS ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
