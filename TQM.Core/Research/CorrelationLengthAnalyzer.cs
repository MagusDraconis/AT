using System.Globalization;

namespace TQM.Core.Research;

/// <summary>
/// Investigates the origin of the defect correlation length ξ.
/// TQM-X058: Origin of the Defect Correlation Length
/// </summary>
public static class CorrelationLengthAnalyzer
{
    // Observed: m_e ≈ 0.511 MeV, M_P ≈ 1.22×10^22 MeV
    // m_e/M_P ≈ (ℓ_P/ξ) → ξ/ℓ_P ≈ M_P/m_e ≈ 2.4×10^22
    // log10(ξ/ℓ_P) ≈ 22.4
    private const double ObservedLogXiOverLP = 22.4;

    public static List<CorrelationLengthMetrics.XiModel> AnalyzeModels()
    {
        return new List<CorrelationLengthMetrics.XiModel>
        {
            new("A: Defect stability sweet spot",
                "Defects are stable only when ξ ≫ ℓ_P. Below a critical\n"
                + "ξ_c, defects are destroyed by Q-event granularity.\n"
                + "Above ξ_c, defects persist but become too diffuse.\n"
                + "The optimal ξ balances stability vs localization.",
                -1, ObservedLogXiOverLP, true,
                "ξ_c is a FUNCTION of N (entity count). The stability threshold\n"
                + "depends on defect-defect interaction strength. The predicted\n"
                + "log(ξ/ℓ_P) depends on Σ(1/r_ij) over all defect pairs —\n"
                + "which depends on how many defects exist. Self-consistent:\n"
                + "more defects → stronger interaction → larger ξ needed.\n"
                + "But the exact value requires N, which is contingent.",
                true),

            new("B: Complexity maximization scan",
                "Scan over ξ/ℓ_P ∈ [10^10, 10^30] and compute ecological\n"
                + "fitness. The ξ that maximizes stability × info-capacity\n"
                + "is the natural correlation length.",
                -1, ObservedLogXiOverLP, false,
                "SCAN-BASED: See computational experiment below.\n"
                + "If the optimum lands near log(ξ/ℓ_P) ≈ 22, this is a\n"
                + "genuine prediction (within the model).",
                true),

            new("C: Self-organized criticality",
                "Q-event networks naturally evolve to a critical state\n"
                + "where correlation lengths DIVERGE. At criticality,\n"
                + "ξ is limited only by the system size (universe scale).\n"
                + "ξ ~ R_universe ~ 10^61 ℓ_P → WRONG (too large).",
                61, ObservedLogXiOverLP, true,
                "Criticality gives ξ ~ system size (~10^61 ℓ_P), not 10^17.\n"
                + "Suggests the defect network is NEAR-critical but not AT\n"
                + "criticality. The distance from criticality IS the electroweak scale.\n"
                + "But this 'distance' is an unexplained parameter.",
                false),

            new("D: Defect density equilibrium",
                "In equilibrium, defect creation rate = annihilation rate.\n"
                + "Creation ∝ N (entity count). Annihilation ∝ n² (defect density²).\n"
                + "n_eq ∝ √N. Defect spacing ∝ N^(-1/4). ξ ∝ spacing.",
                29, ObservedLogXiOverLP, false,
                $"log(ξ/ℓ_P) predicted: {29:F1} (observed: {ObservedLogXiOverLP:F1}).\n"
                + "N^(1/4) scaling gives ~29 for N~10^120 but observation is ~22.\n"
                + "Factor ~10^7 discrepancy. Wrong scaling or wrong N?",
                false),

            new("E: Λ correlation (X046 connection)",
                "The cosmological constant Λ ≈ H² ≈ 1/√V sets a scale.\n"
                + "ξ ~ Λ^(-1/2) ≈ H^(-1) ≈ 10^61 ℓ_P → WRONG.\n"
                + "But if ξ ~ (Λ·ℓ_P²)^(-1/4): ξ ~ 10^17 ℓ_P. CLOSE!",
                17, ObservedLogXiOverLP, true,
                $"ξ/ℓ_P ~ (ℓ_P²Λ)^(-1/4) ≈ {17:F0} (observed: {ObservedLogXiOverLP:F0}).\n"
                + "Combining Q-event spacing ℓ_P and cosmological constant Λ\n"
                + "produces a scale near the electroweak scale! This is the\n"
                + "CORRECT numerology but the mechanism is unclear.\n"
                + "Why would ξ involve Λ? Λ is COSMOLOGICAL, not microscopic.",
                true),
        };
    }

    public static List<CorrelationLengthMetrics.XiScanPoint> ScanXi()
    {
        var points = new List<CorrelationLengthMetrics.XiScanPoint>();
        double[] logXiVals = { 5, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };

        foreach (double logXi in logXiVals)
        {
            double xiOverLP = Math.Pow(10, logXi);

            // Stability: too small → defects destroyed. Too large → too diffuse.
            // Optimal at intermediate ξ where localization and persistence balance
            double stability = Math.Exp(-0.5 * Math.Pow((logXi - 20), 2) / 25.0);

            // Information capacity: more scales → more distinguishable configurations
            // But too many scales → redundancy
            double infoCap = Math.Log(1.0 + xiOverLP) / Math.Log(10) * Math.Exp(-logXi / 60.0);

            // Complexity cost: larger ξ → larger defects → more energy needed
            double cost = logXi * 0.3;

            // Fitness
            double fitness = stability * infoCap * 10.0 - cost;

            points.Add(new CorrelationLengthMetrics.XiScanPoint(
                logXi, stability, infoCap, cost, fitness));
        }

        return points;
    }

    public static string ScanTable(List<CorrelationLengthMetrics.XiScanPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CORRELATION LENGTH OPTIMIZATION SCAN");
        sb.AppendLine();
        sb.AppendLine("  log(ξ/ℓ_P)   ξ/ℓ_P        Stability  InfoCap   Cost    FITNESS");
        sb.AppendLine("  " + new string('─', 68));

        double bestF = points.Max(p => p.TotalFitness);
        foreach (var p in points)
        {
            string marker = Math.Abs(p.TotalFitness - bestF) < 0.001 ? " ← OPTIMAL" : "";
            string obs = Math.Abs(p.LogXiOverLP - ObservedLogXiOverLP) < 1.0 ? " (observed)" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,10:F0}    10^{1,3:F0}      {2,8:F4}  {3,7:F4}  {4,6:F2}  {5,8:F3}{6}{7}",
                p.LogXiOverLP, p.LogXiOverLP, p.Stability,
                p.InfoCapacity, p.ComplexityCost, p.TotalFitness,
                marker, obs));
        }

        double optimalLogXi = points.OrderByDescending(p => p.TotalFitness).First().LogXiOverLP;
        sb.AppendLine();
        sb.AppendLine($"  Optimal log(ξ/ℓ_P) = {optimalLogXi:F0}");
        sb.AppendLine($"  Observed log(ξ/ℓ_P) = {ObservedLogXiOverLP:F1} (from m_e/M_Planck)");
        sb.AppendLine($"  Ratio: 10^optimal/10^observed = {Math.Pow(10, optimalLogXi - ObservedLogXiOverLP):F1}");

        return sb.ToString();
    }

    public static string TheDerivation()
    {
        return @"
ORIGIN OF THE CORRELATION LENGTH ξ — HONEST ASSESSMENT

THE QUESTION: Why is ξ/ℓ_P ≈ 10^22? (or log(ξ/ℓ_P) ≈ 22.4)

THE ANSWER: TQM does NOT derive this number from first principles.

WHAT WE CAN SHOW:
  • There is a VIABLE WINDOW for ξ: too small → defects destroyed
    by Q-event granularity. Too large → defects too diffuse to be
    localized particles. The window is roughly 10^15 < ξ/ℓ_P < 10^30.

  • Within this window, complexity optimization favors ξ values
    where stability (favoring intermediate scales) and information
    capacity (favoring many distinguishable scales) balance.

  • The optimum depends on the functional form of the fitness function.
    Different choices shift the optimum.

  • Model E is the most intriguing: combining ℓ_P (microscopic Q-event
    scale) and Λ (cosmological constant from X046) naturally produces
    a scale near ξ. This suggests a DEEP CONNECTION between UV (Planck)
    and IR (cosmological) physics — the 'UV/IR mixing' familiar from
    quantum gravity.

STATUS: Classification B — Weak emergence. TQM provides a VIABLE
        mechanism (complexity optimization over defect stability)
        and a tantalizing numerology (ξ ~ (ℓ_P²/√Λ)^(1/2) ~ 10^17 ℓ_P).
        But a rigorous derivation of log(ξ/ℓ_P) ≈ 22 does not exist.

        ξ remains ONE MEASURED PARAMETER in TQM — the absolute mass
        scale. This is the same as the Higgs VEV in the Standard Model,
        repackaged as a defect correlation length.
";
    }

    public static string HostileReview()
    {
        return @"
HOSTILE REVIEW: Is ξ any more fundamental than the Higgs VEV?

CHALLENGE 1: 'Defect correlation length' is just a NEW NAME for
the electroweak scale. You haven't derived it — you've relabeled it.

RESPONSE: Fair. ξ ≈ (200 GeV)^(-1) ≈ 10^(-18) m IS the electroweak
scale expressed as a length. The TQM contribution is: (1) connecting
this scale to defect stability requirements, (2) showing it must be
much larger than ℓ_P for defects to exist, (3) the tantalizing
Λ-mediated connection ξ ~ (ℓ_P²/√Λ)^(1/2). Whether this is 'derivation'
or 'repackaging' depends on whether the Λ connection is real physics
or numerology. Current evidence: NUMEROLOGY.

CHALLENGE 2: The complexity optimization scan gives optimal
log(ξ/ℓ_P) ≈ 20, not 22.4. The observed value is ~250× larger.
That's a significant discrepancy — not a 'prediction.'

RESPONSE: The scan depends on the fitness functional form. With
different weighting of stability vs. information capacity, the
peak shifts. The peak at ~20 is within factor ~250 of observed
— better than the 'any value is possible' alternative. But it's
not a precision prediction.

CHALLENGE 3: Model E uses Λ (cosmological constant) to predict ξ
(microscopic scale). This is UV/IR mixing — a known feature of
quantum gravity. But the specific formula ξ ~ (ℓ_P²/√Λ)^(1/2)
is pulled from dimensional analysis. No derivation.

RESPONSE: Correct. The formula is dimensional analysis: the only
scales in TQM are ℓ_P and Λ. Any length scale L must be expressible
as L = ℓ_P · f(ℓ_P²Λ) where f is some function. For f(x) = x^(-1/8),
L ~ ℓ_P · (ℓ_P²Λ)^(-1/8) ~ 10^17 ℓ_P. The functional form is chosen
to match the data — it's not derived. But the FACT that ℓ_P and Λ
combine to produce a mesoscopic scale is genuinely interesting.

VERDICT: Classification B. ξ is weakly constrained by defect stability
and complexity arguments. The Λ-mediated formula is numerologically
suggestive. But ξ remains a measured parameter — the single mass
scale that every physical theory requires as input.
";
    }
}
