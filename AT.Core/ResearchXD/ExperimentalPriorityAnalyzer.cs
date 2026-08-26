namespace AT.Core.ResearchXD;

/// <summary>
/// Computes experimental priorities for AT validation 2025-2035.
/// ResearchXD-WP001: White Paper
/// </summary>
public static class ExperimentalPriorityAnalyzer
{
    public sealed record Experiment(
        string Name, string Facility,
        string Measurement, string ATPrediction,
        string LcdmPrediction, int TimescaleYears,
        double InformationGain, double CostFactor,
        double FalsificationPower, string VerdictIfAT,
        string VerdictIfLCDM);

    public static List<Experiment> DefineExperiments()
    {
        return new List<Experiment>
        {
            new("Dark energy equation of state w(z)",
                "Euclid (ESA, 2024+)", "w(z) from clustering + lensing + SNe",
                "w(z) ≈ -1 + 0.015(1+z)^(3/2)", "w = -1.00 (exact constant)",
                5, 10.0, 1.0, 10.0,
                "AT SURVIVES — time-varying Λ confirmed.",
                "AT FALSIFIED — Λ must be constant."),

            new("Expansion history H(z)",
                "DESI (2021+), Euclid, Roman", "H(z) from BAO",
                "H(z) 1-3% higher at z>0.5", "H(z) from ΛCDM",
                4, 8.0, 0.8, 7.0,
                "AT consistent — less dark energy at high z.",
                "AT constrained — H(z) deviation not observed."),

            new("Neutrino mass ordering",
                "JUNO (2024+), DUNE (2030+), Hyper-K", "Δm² sign",
                "NORMAL (m₁<m₂<m₃)", "Either (free parameter)",
                6, 6.0, 1.5, 4.0,
                "Model A (X060) confirmed — attractive self-interaction.",
                "Inverted at >5σ → Model A WRONG."),

            new("Dark matter direct detection",
                "XENONnT, LZ, PandaX", "WIMP-nucleon cross-section",
                "Null at ~10⁻⁴⁸ cm² (defect DM)", "WIMP signal at ~10⁻⁴⁷ cm²",
                5, 7.0, 2.0, 5.0,
                "AT consistent — defect DM too heavy/weak.",
                "WIMP detected → AT DM identity wrong."),

            new("Galaxy rotation curves (a₀ test)",
                "SPARC (current), Rubin/LSST", "a₀ vs H₀ correlation",
                "a₀ ≈ cH₀/(2π) ≈ 10⁻¹⁰ m/s²", "a₀ is empirical (coincidence)",
                3, 6.0, 0.3, 3.0,
                "a₀ tracks H₀ → Λ→a₀ link confirmed.",
                "a₀ constant while H₀ evolves → link broken."),

            new("Gravitational wave speed",
                "LIGO, Virgo, KAGRA", "c_g / c",
                "c_g = c (exact)", "c_g = c (exact in GR)",
                0, 3.0, 0.5, 2.0,
                "AT passes (no deviation predicted).",
                "AT passes (same prediction). Not distinguishing."),

            new("CMB spectral distortions",
                "PIXIE, PRISM (proposed)", "μ, y distortions from Λ(t)",
                "μ ~ 10⁻⁸ (Λ was larger in past)", "μ ~ 0 (no early DE)",
                15, 5.0, 3.0, 3.0,
                "AT supported — early dark energy signature.",
                "No distortion → AT early DE constrained."),

            new("Strong lensing time delays",
                "Rubin/LSST (2025+)", "H₀ from time-delay cosmography",
                "H₀ ≈ 73 km/s/Mpc (resolves tension)", "H₀ tension: 67 vs 73",
                5, 7.0, 0.5, 3.0,
                "AT's Λ(t) naturally gives higher H₀ from late-time.",
                "H₀=67 confirmed → AT's H₀ resolution wrong."),
        };
    }

    public static string PriorityTable(List<Experiment> experiments)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EXPERIMENTAL PRIORITIES 2025-2035 — RANKED BY FALSIFICATION POWER");
        sb.AppendLine();
        sb.AppendLine("  Rank  Experiment                  Facility     Yrs  Info  Cost  Power  PRIORITY");
        sb.AppendLine("  " + new string('-', 90));

        var ranked = experiments
            .OrderByDescending(e => e.FalsificationPower * e.InformationGain / (e.CostFactor + 0.1))
            .ToList();

        for (int i = 0; i < ranked.Count; i++)
        {
            var e = ranked[i];
            double priority = e.FalsificationPower * e.InformationGain / (e.CostFactor + 0.1);
            sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "  {0,3}. {1,-28} {2,-11} {3,3}  {4,4:F0}  {5,4:F1}  {6,5:F0}   {7,6:F0}",
                i + 1, e.Name, e.Facility, e.TimescaleYears,
                e.InformationGain, e.CostFactor, e.FalsificationPower, priority));
        }

        sb.AppendLine();
        sb.AppendLine("  TIER 1 (CRITICAL):    Euclid w(z) — decisive by 2030.");
        sb.AppendLine("  TIER 2 (HIGH):        DESI H(z) + DM direct detection + H₀ lensing.");
        sb.AppendLine("  TIER 3 (MEDIUM):      Neutrino ordering (JUNO/DUNE) + a₀ test.");
        sb.AppendLine("  TIER 4 (LONG-TERM):   CMB spectral distortions (PIXIE/PRISM).");
        return sb.ToString();
    }

    public static string TheDecadeRoadmap()
    {
        return @"
AT EXPERIMENTAL ROADMAP 2025-2035

═══════════════════════════════════════════════════════════════
  2025-2027: IMMEDIATE PRIORITIES
═══════════════════════════════════════════════════════════════
  • DESI BAO: First H(z) constraints on Λ(t) model.
  • SPARC: Existing rotation curve data → a₀ test.
  • Euclid first light: Survey begins.
  • Direct DM detection: XENONnT/LZ continue.

═══════════════════════════════════════════════════════════════
  2027-2030: DECISIVE TESTS
═══════════════════════════════════════════════════════════════
  • EUCLID w(z): THE KILL SHOT — σ(w) ≈ 0.02.
    This will confirm or falsify time-varying Λ at ~3σ.
  • JUNO: Neutrino mass ordering (normal vs inverted).
  • Rubin/LSST: H₀ from strong lensing + a₀ from galaxy survey.

═══════════════════════════════════════════════════════════════
  2030-2035: PRECISION ERA
═══════════════════════════════════════════════════════════════
  • Roman Space Telescope: σ(w) ≈ 0.01 → decisive test.
  • DUNE + Hyper-K: Precision neutrino physics.
  • PIXIE/PRISM (if funded): CMB spectral distortions.
  • LISA (if launched): GW stochastic background.

═══════════════════════════════════════════════════════════════

BY 2030: AT will be either FALSIFIED or STRENGTHENED at >3σ.
The single most decisive experiment is Euclid's w(z) measurement.
";
    }
}
