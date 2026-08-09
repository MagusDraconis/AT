namespace TQM.Core.ResearchXD;

/// <summary>
/// Quantitative risk ranking of TQM predictions.
/// ResearchXD-002: Prediction Risk Matrix
/// </summary>
public static class PredictionRiskAnalyzer
{
    public sealed record ScoredPrediction(
        string Name, int Uniqueness, int Falsifiability,
        int KillPower, int TimeScore, int InfoGain,
        double RiskScore, string KillShot, string Priority);

    public static List<ScoredPrediction> ScorePredictions()
    {
        return new List<ScoredPrediction>
        {
            new("Time-varying DE w(z) ≠ -1",
                10, 9, 10, 10, 9,
                90.0,
                "Euclid w = -1.00 → Λ not time-varying → X046, X062, entire Λ emergence chain collapses.",
                "CRITICAL — TEST FIRST"),

            new("Λ(t) = α/√V(t) (specific form)",
                10, 8, 9, 9, 8,
                80.0,
                "w(z) functional form wrong → specific Λ(t) model wrong. Core idea may survive.",
                "HIGH — tied to w(z)"),

            new("a₀ ≈ cH₀ (acceleration scale)",
                9, 7, 7, 8, 8,
                44.1,
                "a₀ doesn't track H₀ → Λ→a₀ link broken. MOND-scale coincidence returns.",
                "HIGH — testable now"),

            new("DM = neutral defects (~TeV)",
                6, 5, 6, 4, 7,
                45.0,
                "WIMP detected at LHC/direct → TQM DM identity wrong. But DM still needed.",
                "MEDIUM — shared prediction"),

            new("Log-normal abundance law",
                10, 4, 5, 3, 10,
                66.7,
                "α shown constant at higher precision than log-normal allows → statistical nature challenged.",
                "HIGH uniqueness, hard test"),

            new("Neutrino normal ordering",
                5, 7, 4, 5, 6,
                28.0,
                "Inverted at >5σ → Model A (X060) wrong. Core TQM survives.",
                "MEDIUM — partial kill"),

            new("No spacetime singularities",
                8, 1, 3, 1, 5,
                120.0,
                "Untestable. Only kills strong-field TQM prediction.",
                "LOW — untestable"),

            new("M² = ⟨k⟩_interact ≈ 5",
                10, 1, 2, 1, 8,
                160.0,
                "M² found to NOT equal connectivity → final parameter not derived. But M² existence unchanged.",
                "LOW — indirect only"),
        };
    }

    public static string RiskMatrix(List<ScoredPrediction> predictions)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PREDICTION RISK MATRIX — HIGHEST TO LOWEST KILL POTENTIAL");
        sb.AppendLine();
        sb.AppendLine("  Rank  Prediction                   Uniq  Fals  Kill  Time  Info  RISK   Priority");
        sb.AppendLine("  " + new string('-', 90));

        var ranked = predictions.OrderByDescending(p => p.KillPower * p.Falsifiability * p.Uniqueness / Math.Max(p.TimeScore, 1)).ToList();

        for (int i = 0; i < ranked.Count; i++)
        {
            var p = ranked[i];
            double risk = p.KillPower * p.Falsifiability * p.Uniqueness / Math.Max(p.TimeScore, 1.0);
            sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "  {0,3}. {1,-30} {2,4}  {3,4}  {4,4}  {5,4}  {6,4}  {7,5:F0}  {8}",
                i + 1, p.Name, p.Uniqueness, p.Falsifiability,
                p.KillPower, p.TimeScore, p.InfoGain, risk, p.Priority));
        }

        sb.AppendLine();
        sb.AppendLine("  SCORING (0-10): Uniqueness to TQM | Falsifiability | Kill Power |");
        sb.AppendLine("  Time-to-result (inverse) | Information gain.");
        sb.AppendLine();
        sb.AppendLine("  #1 KILL SHOT: Time-varying DE w(z).");
        sb.AppendLine("  Falsifying this KILLS the entire Λ emergence chain (X046-X062).");
        return sb.ToString();
    }

    public static string TheKillShot()
    {
        return @"
THE SINGLE CRITICAL KILL SHOT

PREDICTION:  w(z) ≠ -1  (time-varying dark energy).

EXPERIMENT:  Euclid (ESA, launch 2023, first cosmology 2025+).
             Measures w via clustering + lensing + SNe.

TARGET:      σ(w) ≈ 0.02 (from combined probes).

TQM:         w(z) ≈ -1 + 0.015·(1+z)^(3/2).
             Deviation ~1.5% at z=0, ~4% at z=1.

SCENARIOS:
  A: Euclid measures w = -1.00 ± 0.02.
     TQM is INCONSISTENT at ~1.5σ.
     Wait for Roman (2027+) for definitive test.

  B: Euclid + Roman measure w = -1.00 ± 0.01.
     TQM is FALSIFIED at >3σ.
     Λ(t) model is WRONG. X046 and X062 collapse.

  C: Euclid measures w = -0.98 ± 0.02.
     TQM is CONSISTENT. Not uniquely confirmed.
     Other models also predict w ≠ -1.

IF TQM IS RIGHT: We will see w ≠ -1 at ~3σ by ~2030.
IF TQM IS WRONG: Euclid+Roman will kill it by ~2030.

THIS IS THE RISK. THIS IS THE EXPERIMENT.
";
    }
}
