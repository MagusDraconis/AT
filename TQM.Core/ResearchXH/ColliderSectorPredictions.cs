namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 130 — Collider-accessible sector prediction. QG124-129 established that higher-energy
/// attractor sectors exist, are metastable, and generate a predictive discrete spectrum; QG129 showed the
/// ladder calibrates PARTIALLY (the top transition quantum reproduces the SM H/Z ratio within ~3%, but the
/// ladder span cannot host the lepton generation hierarchy). This phase asks: which sector transitions are
/// ACCESSIBLE within current (LHC) and next-generation (FCC) collider energies?
///
/// Method (computational, fully deterministic): (1) SECTOR THRESHOLDS — the 8 discrete energy thresholds of
/// the sector hierarchy (QG127); (2) LADDER ACCESSIBILITY — the 12-rung decay ladder (radii 6.0..17.333,
/// QG128) calibrated to physical masses under the QG129-supported ELECTROWEAK calibration family (the heavy
/// SM states whose ratios the ladder reproduces: W, Z, H, t as anchors for the observable radius-6 sector);
/// (3) DECAY SPECTRA — the emitted quanta (unit quantum + top quantum) under calibration; (4) OBSERVABLE
/// SIGNATURES — whether accessible sectors appear as metastable decay signatures (QG125/127); (5) LHC/FCC
/// REACH — compare sector masses against the documented collider center-of-mass energies (LEP 0.209 TeV,
/// LHC 13 TeV, HL-LHC 14 TeV, FCC-ee 0.365 TeV, FCC-hh 100 TeV).
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class ColliderSectorPredictions
{
    /// <summary>The 12-rung decay ladder radii (QG128), descending high→observable.</summary>
    public static readonly double[] LadderRadii =
        { 17.333, 16.0, 15.0, 14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0 };

    /// <summary>Observable (radius-6) sector anchor masses (GeV) — the plausible calibration family.</summary>
    public static readonly (string Name, double MassGeV)[] ElectroweakAnchors =
    {
        ("W", PhysicalCalibration.MWGeV),
        ("Z", PhysicalCalibration.MZGeV),
        ("H", PhysicalCalibration.MHGeV),
        ("t", PhysicalCalibration.MTopGeV),
    };

    /// <summary>Documented collider center-of-mass energies (TeV).</summary>
    public static readonly (string Name, double EnergyTeV)[] Colliders =
    {
        ("LEP", 0.209),
        ("LHC13", 13.0),
        ("HL-LHC", 14.0),
        ("FCC-ee", 0.365),
        ("FCC-hh", 100.0),
    };

    /// <summary>The 8 discrete sector energy thresholds (dimensionless ceiling units, QG127).</summary>
    public static double[] SectorThresholds() => HighEnergySectorSignatures.EnergyThresholds().Thresholds;

    // ── Calibrated sector masses ────────────────────────────────────────────────

    /// <summary>
    /// Rung masses (GeV) under a linear radius→mass calibration anchored at the observable sector (radius 6)
    /// on the given electroweak anchor mass. Returns (rungIndex, radius, massGeV) descending.
    /// </summary>
    public static (int Rung, double Radius, double MassGeV)[] RungMasses(string anchorName = "Z")
    {
        var anchor = ElectroweakAnchors.First(a => a.Name == anchorName);
        double scale = anchor.MassGeV / LadderRadii[^1];   // GeV per radius unit
        return LadderRadii.Select((r, i) => (i, r, r * scale)).ToArray();
    }

    /// <summary>Highest-sector (top rung) mass in GeV for the given anchor.</summary>
    public static double TopRungMassGeV(string anchorName = "Z")
        => RungMasses(anchorName)[0].MassGeV;

    /// <summary>Lowest-sector (observable rung) mass in GeV for the given anchor.</summary>
    public static double ObservableRungMassGeV(string anchorName = "Z")
        => RungMasses(anchorName)[^1].MassGeV;

    // ── Ladder accessibility ────────────────────────────────────────────────────

    /// <summary>
    /// Number of ladder rungs with mass below a given collider c.o.m. energy (TeV). A rung is "producible"
    /// if its mass is below the collider energy (kinematic accessibility).
    /// </summary>
    public static int AccessibleRungCount(string anchorName, double colliderEnergyTeV)
        => RungMasses(anchorName).Count(m => m.MassGeV < colliderEnergyTeV * 1000.0);

    /// <summary>Fraction (0..1) of ladder rungs accessible at a collider for the anchor.</summary>
    public static double AccessibleFraction(string anchorName, double colliderEnergyTeV)
        => (double)AccessibleRungCount(anchorName, colliderEnergyTeV) / LadderRadii.Length;

    /// <summary>Is the highest-energy sector (top rung) accessible at the given collider for the anchor?</summary>
    public static bool TopSectorAccessible(string anchorName, double colliderEnergyTeV)
        => TopRungMassGeV(anchorName) < colliderEnergyTeV * 1000.0;

    // ── Decay spectra (emitted quanta) under calibration ───────────────────────

    /// <summary>Emitted-quantum energies (GeV) under the anchor calibration (unit quantum × scale, top quantum × scale).</summary>
    public static (string Quantum, double RadiusDrop, double EnergyGeV)[] DecaySpectrum(string anchorName = "Z")
    {
        double scale = RungMasses(anchorName)[0].MassGeV / LadderRadii[0];
        return new[]
        {
            ("unit", PhysicalCalibration.UnitQuantum, PhysicalCalibration.UnitQuantum * scale),
            ("top", PhysicalCalibration.TopQuantum, PhysicalCalibration.TopQuantum * scale),
        };
    }

    // ── Observable signatures ───────────────────────────────────────────────────

    /// <summary>
    /// Since QG125 showed the high-energy sectors are METASTABLE (decay into the observable sector when their
    /// energy regime is removed), an accessible high sector would appear as a decay cascade/transient rather
    /// than a stable particle. Signature: the top sector's emitted quantum is within the collider's
    /// accessible range (the decay would be observable).
    /// </summary>
    public static bool DecaySignatureObservable(string anchorName, double colliderEnergyTeV)
        => TopSectorAccessible(anchorName, colliderEnergyTeV);

    // ── Reach summary & classification ──────────────────────────────────────────

    /// <summary>
    /// For each anchor: (anchor, topMassGeV, lhc13Accessible, fcc100Accessible, accessibleFractionAtLhc).
    /// </summary>
    public static (string Anchor, double TopMassGeV, bool Lhc13, bool Fcchh, double FractionAtLhc)[]
        ReachSummary()
        => ElectroweakAnchors.Select(a =>
        {
            double lhcTeV = Colliders.First(c => c.Name == "LHC13").EnergyTeV;
            double fccTeV = Colliders.First(c => c.Name == "FCC-hh").EnergyTeV;
            return (a.Name, TopRungMassGeV(a.Name),
                TopSectorAccessible(a.Name, lhcTeV), TopSectorAccessible(a.Name, fccTeV),
                AccessibleFraction(a.Name, lhcTeV));
        }).ToArray();

    /// <summary>
    /// Accessibility score (0..5):
    /// 1. at least 3 discrete sector thresholds exist;
    /// 2. the top sector is accessible at FCC-hh for the Z anchor;
    /// 3. the top sector is accessible at LHC13 for the Z anchor;
    /// 4. the top sector is accessible at LHC13 for ALL electroweak anchors;
    /// 5. the decay signature (top quantum) is observable at LHC13 for the Z anchor.
    /// </summary>
    public static int AccessibilityScore()
    {
        double lhc = Colliders.First(c => c.Name == "LHC13").EnergyTeV;
        double fcc = Colliders.First(c => c.Name == "FCC-hh").EnergyTeV;
        int score = 0;
        if (SectorThresholds().Length >= 3) score++;
        if (TopSectorAccessible("Z", fcc)) score++;
        if (TopSectorAccessible("Z", lhc)) score++;
        if (ElectroweakAnchors.All(a => TopSectorAccessible(a.Name, lhc))) score++;
        if (DecaySignatureObservable("Z", lhc)) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NOT ACCESSIBLE       — the highest-energy sectors lie above next-generation collider reach for the
    ///                          plausible calibration family;
    ///   PARTIALLY ACCESSIBLE — some sectors are within FCC reach but not all, or reach depends strongly on
    ///                          the calibration anchor;
    ///   ACCESSIBLE           — the highest-energy sectors lie within LHC/FCC reach for the whole plausible
    ///                          electroweak calibration family, appearing as metastable decay signatures
    ///                          (QG125) — the concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = AccessibilityScore();
        if (score <= 2) return "NOT ACCESSIBLE";
        if (score == 5) return "ACCESSIBLE";
        return "PARTIALLY ACCESSIBLE";
    }
}
