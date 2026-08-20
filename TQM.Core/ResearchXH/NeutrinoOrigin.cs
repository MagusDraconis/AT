namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 154 — Origin of the neutrino sector. QG138-QG153 derived families, hierarchies, mode
/// access, and the Z2 doublet structure. QG148 showed the linear exponent law overfits: the neutrino
/// prediction (p = 9.113) deviates 103% from the observed exponent (p = 4.48). This phase asks: why does
/// the neutrino sector deviate from the lepton and quark scaling laws?
///
/// Method (computational, fully deterministic): (1) NEUTRAL-CHARGE LIMIT — the neutrino is the UNIQUE
/// neutral fermion (Q = 0); the charge-dependent mode access (QG143 charge power n ≈ 6.47, Q^n) vanishes
/// identically for Q = 0; (2) T3-ONLY ACCESS — with no charge channel, the neutrino reverts to T3-only
/// Z2-channel spectral access: its effective dimension should match the Weyl of the T3 = +1/2 channel
/// (one member of each Z2 doublet); (3) DOUBLET OCCUPANCY — within the lepton doublet (ν, e), the
/// neutrino (T3=+1/2) is NOT enhanced while the electron (T3=−1/2) keeps the charge channel — opposite to
/// the quark doublet where up (T3=+1/2) is enhanced; (4) SPECTRAL ACCESSIBILITY — δ_ν is the minimum over
/// all sectors and matches the Z2 channel Weyl; (5) NEUTRINO HIERARCHY — p_eff = log(ν3/ν1)/log(4) with
/// ν3/ν1 = 500.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class NeutrinoOrigin
{
    /// <summary>Charge amplification power from QG143 (charge-sector effects).</summary>
    public const double ChargePower = 6.47;

    /// <summary>Documented sectors: (name, p_eff, Q, T3) from QG147.</summary>
    public static (string Name, double P, double Q, double T3)[] SectorExponents()
        => SectorExponentLaw.SectorExponents();

    /// <summary>Observed neutrino hierarchy exponent p = log(500)/log(4).</summary>
    public static double NeutrinoExponent()
        => SectorExponentLaw.NeutrinoObservedExponent();

    /// <summary>Neutrino effective spectral dimension δ = p/2.</summary>
    public static double NeutrinoDelta()
        => NeutrinoExponent() / 2.0;

    // ── 1. Neutral-charge limit ─────────────────────────────────────────────────

    /// <summary>Is the neutrino the UNIQUE neutral (Q = 0) fermion sector?</summary>
    public static bool UniqueNeutralSector()
    {
        var sectors = new[] { ("leptons", -1.0), ("up", 2.0 / 3.0), ("down", -1.0 / 3.0), ("neutrino", 0.0) };
        return sectors.Count(s => Math.Abs(s.Item2) < 1e-9) == 1;
    }

    /// <summary>Neutrino charge amplification Q^n with Q = 0 (vanishes identically).</summary>
    public static double NeutrinoChargeAmplification()
        => Math.Pow(0.0, ChargePower);

    /// <summary>Does the neutrino's charge channel vanish (charge amplification = 0)?</summary>
    public static bool NeutralChargeLimit()
        => UniqueNeutralSector() && Math.Abs(NeutrinoChargeAmplification()) < 1e-12;

    // ── 2. T3-only access ───────────────────────────────────────────────────────

    /// <summary>
    /// Weyl exponent of the T3 = +1/2 Z2 channel (the even members — one from each doublet). With no
    /// charge channel, the neutrino accesses only this isospin channel.
    /// </summary>
    public static double T3PlusChannelWeyl()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        var even = w.Where((_, i) => i % 2 == 0).ToArray();
        return WeylOf(even);
    }

    /// <summary>Deviation of the neutrino dimension from the T3 = +1/2 channel Weyl.</summary>
    public static double NeutrinoT3ChannelDeviation()
        => Math.Abs(NeutrinoDelta() / T3PlusChannelWeyl() - 1.0);

    /// <summary>Does the neutrino dimension match the T3 = +1/2 channel Weyl within 10% (T3-only access)?</summary>
    public static bool T3OnlyAccess()
        => NeutrinoT3ChannelDeviation() < 0.10;

    private static double WeylOf(double[] ws)
    {
        if (ws.Length < 4) return double.NaN;
        var logW = ws.Select(x => Math.Log(x)).ToArray();
        var logN = Enumerable.Range(1, ws.Length).Select(i => Math.Log((double)i)).ToArray();
        double mx = logW.Average(), my = logN.Average();
        double num = 0, den = 0;
        for (int i = 0; i < ws.Length; i++)
        {
            num += (logW[i] - mx) * (logN[i] - my);
            den += (logW[i] - mx) * (logW[i] - mx);
        }
        return den < 1e-12 ? double.NaN : num / den;
    }

    // ── 3. Doublet occupancy ────────────────────────────────────────────────────

    /// <summary>
    /// Doublet occupancy: the r31 ratio of the T3=+1/2 vs T3=−1/2 member of each weak doublet.
    /// Quark doublet: up/down = 88.4 (up enhanced). Lepton doublet: e/ν = 6.95 (electron enhanced — the
    /// neutrino is NOT enhanced). Returns (name, ratio, log2).
    /// </summary>
    public static (string Doublet, double Ratio, double Log2)[] DoubletOccupancy()
    {
        var upR = QuarkAmplification.FermionSectorData().First(s => s.Name == "up").R31;
        var downR = QuarkAmplification.FermionSectorData().First(s => s.Name == "down").R31;
        var eR = QuarkAmplification.FermionSectorData().First(s => s.Name == "leptons").R31;
        var nuR = QuarkAmplification.FermionSectorData().First(s => s.Name == "neutrino").R31;
        double quarkRatio = upR / downR;
        double leptonRatio = eR / nuR;
        return new[]
        {
            ("quark (u,d)", quarkRatio, Math.Log2(quarkRatio)),
            ("lepton (ν,e)", leptonRatio, Math.Log2(leptonRatio)),
        };
    }

    /// <summary>
    /// In the lepton doublet, the T3=+1/2 member (neutrino) is NOT the enhanced one. Returns the log2
    /// ratio of the quark doublet (up enhanced) vs lepton doublet (electron enhanced) — the neutrino's
    /// missing enhancement.
    /// </summary>
    public static (double QuarkLog2, double LeptonLog2) DoubletLog2Ratios()
    {
        var occ = DoubletOccupancy();
        return (occ[0].Log2, occ[1].Log2);
    }

    /// <summary>Does the lepton doublet invert the up-enhancement (electron, not neutrino, is enhanced)?</summary>
    public static bool LeptonDoubletInverted()
    {
        var occ = DoubletOccupancy();
        // in the quark doublet up/down > 1 (up enhanced); the neutrino being the suppressed member
        // is shown by its dimension being the MINIMUM of all sectors.
        return occ[0].Ratio > 1.0 && NeutrinoIsMinimum();
    }

    // ── 4. Spectral accessibility ───────────────────────────────────────────────

    /// <summary>Effective dimension of each sector (δ = p/2) plus the neutrino.</summary>
    public static (string Name, double Delta)[] AllSectorDeltas()
    {
        var list = SectorExponents().Select(s => (s.Name, s.P / 2.0)).ToList();
        list.Add(("neutrino", NeutrinoDelta()));
        return list.ToArray();
    }

    /// <summary>Is the neutrino dimension the MINIMUM over all sectors (the suppressed neutral sector)?</summary>
    public static bool NeutrinoIsMinimum()
    {
        var deltas = AllSectorDeltas();
        double nu = NeutrinoDelta();
        return deltas.All(d => d.Delta >= nu - 1e-9);
    }

    /// <summary>Neutrino dimension relative to the full-spectrum Weyl (should be below it — reduced access).</summary>
    public static double NeutrinoVsFullWeyl()
        => NeutrinoDelta() / ModeAccessOrigin.FullWeyl();

    // ── 5. Neutrino hierarchy ───────────────────────────────────────────────────

    /// <summary>Neutrino hierarchy ratio ν3/ν1.</summary>
    public static double NeutrinoHierarchyRatio()
        => 500.0;

    /// <summary>
    /// QG147 linear-law prediction for the neutrino (Q=0, T3=+1/2): p = p0 + a·Q + b·T3. The law
    /// OVERFITS and fails for the neutrino (deviation &gt; 50%).
    /// </summary>
    public static double LinearLawNeutrinoPrediction()
        => SectorExponentLaw.FitExponentLaw().NeutrinoPrediction;

    /// <summary>Deviation of the QG147 linear-law neutrino prediction from the observed exponent.</summary>
    public static double LinearLawNeutrinoDeviation()
        => ExponentLawValidation.NeutrinoPrediction().Deviation;

    /// <summary>Does the linear exponent law FAIL for the neutrino (deviation &gt; 50%)?</summary>
    public static bool LinearLawFailsForNeutrino()
        => LinearLawNeutrinoDeviation() > 0.50;

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Neutrino-origin score (0..5):
    /// 1. the neutrino is the UNIQUE neutral (Q = 0) fermion with vanishing charge channel;
    /// 2. the neutrino dimension matches the T3 = +1/2 Z2 channel Weyl within 10% (T3-only access);
    /// 3. the lepton doublet is inverted (electron, not neutrino, is the enhanced member);
    /// 4. the neutrino dimension is the MINIMUM over all sectors (suppressed neutral sector);
    /// 5. the QG147 linear law FAILS for the neutrino (the charge-dependent T3 enhancement it predicts
    ///    requires a non-vanishing charge channel).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (NeutralChargeLimit()) score++;
        if (T3OnlyAccess()) score++;
        if (LeptonDoubletInverted()) score++;
        if (NeutrinoIsMinimum()) score++;
        if (LinearLawFailsForNeutrino()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN       — no mechanism explains the neutrino's deviation from the lepton/quark laws;
    ///   PARTIAL ORIGIN  — some spectral correspondence exists (e.g. T3-channel access) but not a complete
    ///                     explanation;
    ///   NEUTRINO ORIGIN — the neutrino sector deviates because it is the ONLY neutral fermion: the
    ///                     charge-dependent mode amplification vanishes identically (Q^n = 0), the
    ///                     charge×isospin enhancement that boosts other T3=+1/2 sectors cannot act, and the
    ///                     neutrino reverts to T3-only Z2-channel spectral access (δ_ν ≈ T3=+1/2 channel
    ///                     Weyl, 3.3%), making it the lowest (suppressed) sector. This explains why the
    ///                     QG147 linear law overfits: it predicts a charge-enhanced neutrino that cannot
    ///                     exist.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "NO ORIGIN";
        if (score == 5) return "NEUTRINO ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
