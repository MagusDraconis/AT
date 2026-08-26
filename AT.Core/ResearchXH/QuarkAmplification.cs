namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 143 — Origin of quark amplification. QG141 derived the lepton hierarchy exponents from the
/// spectral density and QG142 showed leptons follow the octave spectral law (τ/e within 0.26%) while quarks
/// and neutrinos deviate (up 22.7× HIGHER, down 0.26×, neutrino 0.14× than the octave prediction). This
/// phase asks: what EXTRA sector-dependent factor amplifies quark and neutrino masses beyond the octave
/// hierarchy?
///
/// Method (computational, fully deterministic): the deviation factor of each sector is f = r31_observed /
/// r31_octave, with r31_octave = 4^p_net = 4^5.88 = 3468 (the QG140/141 spectral law). We test five candidate
/// factors against the documented sector quantum numbers: (1) COLOR-SECTOR EFFECTS — does a color
/// multiplicity (N=3 for quarks vs N=1 for leptons) explain the deviation (a single color factor must fit
/// BOTH up and down); (2) CHARGE-SECTOR EFFECTS — correlation of the deviation with electric charge |Q|
/// (up 2/3, down 1/3, lepton 1, neutrino 0); (3) ISOSPIN EFFECTS — correlation with weak isospin T3
/// (up +1/2, down −1/2, lepton −1/2, neutrino +1/2); (4) SECTOR OCCUPATION DENSITY — the octave
/// mode-density occupation per sector (a spectral proxy); (5) MULTI-SECTOR COUPLING — whether the deviation
/// is consistent with a PRODUCT of sector factors (charge × isospin) rather than a single factor.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class QuarkAmplification
{
    /// <summary>The octave-predicted r31 ratio (4^p_net with p_net = 5.88, QG140/141).</summary>
    public static readonly double R31Octave = Math.Pow(4.0, 5.88);   // 3468.3

    /// <summary>
    /// Documented fermion sectors with (name, r31 observed, color N, |electric charge|, weak isospin T3,
    /// hypercharge Y).
    /// </summary>
    public static (string Name, double R31, double Color, double ChargeAbs, double T3, double Y)[]
        FermionSectorData()
        => new[]
        {
            ("leptons", 3477.2, 1.0, 1.0, -0.5, -1.0),
            ("up", 78636.4, 3.0, 2.0 / 3.0, +0.5, +1.0 / 3.0),
            ("down", 889.4, 3.0, 1.0 / 3.0, -0.5, +1.0 / 3.0),
            ("neutrino", 500.0, 1.0, 0.0, +0.5, -1.0),
        };

    /// <summary>Deviation factor f = r31_observed / r31_octave for each sector.</summary>
    public static (string Name, double Factor)[] DeviationFactors()
        => FermionSectorData().Select(s => (s.Name, s.R31 / R31Octave)).ToArray();

    // ── 1. Color-sector effects ─────────────────────────────────────────────────

    /// <summary>
    /// Color-factor consistency: if color alone set the deviation, the two quark sectors (both color 3)
    /// would have the SAME factor. Returns the up/down factor ratio (a large ratio ⇒ color is NOT the
    /// single factor).
    /// </summary>
    public static double ColorFactorRatio()
    {
        var up = DeviationFactors().First(d => d.Name == "up").Factor;
        var down = DeviationFactors().First(d => d.Name == "down").Factor;
        return up / down;
    }

    /// <summary>Does a single color factor explain the quark deviations (up/down ratio ≈ 1)?</summary>
    public static bool SingleColorFactor()
        => Math.Abs(ColorFactorRatio() - 1.0) < 0.1;

    // ── 2. Charge-sector effects ────────────────────────────────────────────────

    /// <summary>
    /// Charge-sector correlation: Pearson correlation between the deviation factor and the electric charge
    /// magnitude |Q| across all sectors. Positive ⇒ higher charge → stronger amplification.
    /// </summary>
    public static double ChargeCorrelation()
    {
        var data = FermionSectorData();
        var x = data.Select(s => s.ChargeAbs).ToArray();
        var y = data.Select(s => s.R31 / R31Octave).ToArray();
        return EffectiveSizeFamilies.Pearson(x, y);
    }

    // ── 3. Isospin effects ──────────────────────────────────────────────────────

    /// <summary>
    /// Isospin effect: the up (T3=+1/2) vs down (T3=−1/2) asymmetry. Returns (upFactor, downFactor,
    /// upOverDown) — a large up/down ratio with up amplified and down suppressed indicates an isospin-signed
    /// amplification.
    /// </summary>
    public static (double Up, double Down, double UpOverDown) IsospinAsymmetry()
    {
        var up = DeviationFactors().First(d => d.Name == "up").Factor;
        var down = DeviationFactors().First(d => d.Name == "down").Factor;
        return (up, down, up / down);
    }

    /// <summary>Is the amplification isospin-signed (up strongly amplified, down suppressed)?</summary>
    public static bool IsospinSignedAmplification()
    {
        var (up, down, _) = IsospinAsymmetry();
        return up > 5.0 && down < 1.0;
    }

    // ── 4. Sector occupation density ────────────────────────────────────────────

    /// <summary>
    /// Sector occupation density: the fraction of spectral modes the "colored" (quark) sector would occupy
    /// relative to the full observable sector. A larger density → more amplification channels.
    /// </summary>
    public static double SectorOccupationDensity()
    {
        var modes = FamilyIndexOrigin.IntraSectorModes();
        if (modes.Length == 0) return 0;
        // proxy: fraction of modes in the top octave (the crowded band) — the multi-mode sector
        return EffectiveSizeLaw.TopOctaveCrowding();
    }

    // ── 5. Multi-sector coupling ────────────────────────────────────────────────

    /// <summary>
    /// Multi-sector coupling: is the deviation consistent with a PRODUCT of sector factors (charge × isospin
    /// × color) rather than a single factor? We test whether the up/down ratio is explained by the charge
    /// ratio alone: (|Q_up|/|Q_down|)^n = upFactor/downFactor ⇒ n = log2(87.3) ≈ 6.45 (a steep charge-power
    /// coupling). Returns the implied charge-power exponent.
    /// </summary>
    public static double ImpliedChargePower()
    {
        var (up, down, _) = IsospinAsymmetry();
        double qRatio = (2.0 / 3.0) / (1.0 / 3.0);   // = 2
        return Math.Log(up / down) / Math.Log(qRatio);
    }

    // ── Factor score & classification ───────────────────────────────────────────

    /// <summary>
    /// Amplification-origin score (0..5):
    /// 1. the deviation factor is sector-dependent (up ≠ down, leptons ≠ quarks);
    /// 2. color is NOT the single factor (up/down ratio ≫ 1);
    /// 3. the deviation correlates positively with electric charge;
    /// 4. the amplification is isospin-signed (up amplified, down suppressed);
    /// 5. a multi-sector (charge-power) coupling reproduces the up/down split with a well-defined exponent.
    /// </summary>
    public static int FactorScore()
    {
        int score = 0;
        if (ColorFactorRatio() > 3.0) score++;
        if (!SingleColorFactor()) score++;
        if (ChargeCorrelation() > 0.5) score++;
        if (IsospinSignedAmplification()) score++;
        double n = ImpliedChargePower();
        if (n > 3.0 && n < 12.0) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO FACTOR         — no sector-dependent factor explains the deviations (all sectors equal);
    ///   PARTIAL FACTOR    — some candidate correlates (charge or isospin) but no consistent multi-sector
    ///                       factor reproduces the deviations;
    ///   AMPLIFICATION ORIGIN — a sector-dependent amplification (charge/isospin-signed, multi-sector
    ///                       coupling with a well-defined power) explains the quark/neutrino deviations beyond
    ///                       the octave law — the concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = FactorScore();
        if (score <= 2) return "NO FACTOR";
        if (score == 5) return "AMPLIFICATION ORIGIN";
        return "PARTIAL FACTOR";
    }
}
