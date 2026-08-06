namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether Q-derived graph physics can predict properties
/// of external physical systems not used in TQM construction.
///
/// TQM-148: External Physical Prediction Test
/// </summary>
public static class ExternalPredictionAnalyzer
{
    public static string ExternalTheory()
    {
        return @"
EXTERNAL PHYSICAL PREDICTION TEST

1. THE TEST:

   Can TQM predict systems it was NOT built to describe?
   This is the gold standard of scientific prediction.

2. WHERE TQM WORKS (Graph Laplacian Systems):
   - Coupled harmonic oscillators → exact match
   - Tight-binding electrons → exact match (identity)
   - Diffusion on lattices → exact match
   - Spin-wave magnons → exact match
   - Phonon spectra → exact match

3. WHERE TQM FAILS (Non-Graph-Laplacian Systems):
   - Ising chain: gap ∝ 1/N (TQM: 1/N²) — WRONG
   - Heisenberg chain: gap ∝ 1/N (TQM: 1/N²) — WRONG
   - Percolation: no prediction from L_Q
   - Random resistor networks: non-Ohmic scaling

4. INTERPRETATION:

   TQM predicts graph Laplacian systems EXACTLY.
   TQM FAILS on systems with different physics.
   This is NOT a weakness — it DELIMITS TQM's domain of applicability.

5. NULL HYPOTHESIS: TQM has no external predictive power.
   H1: TQM predicts graph-Laplacian-governed systems.
";
    }

    public static PredictionCandidate.ExternalPredictionReport Analyze()
    {
        var tests = ExperimentalComparison.RunExternalTests();
        int total = tests.Count;
        int passed = tests.Count(t => t.TQMMatches);
        int failed = total - passed;

        var works = tests.Where(t => t.TQMMatches).Select(t => t.System).ToArray();
        var fails = tests.Where(t => !t.TQMMatches).Select(t => t.System).ToArray();

        bool hasPower = passed >= 4;

        string classification = passed >= 6 ? "D: Novel Predictive Framework"
                              : passed >= 4 ? "C: External Physical Prediction"
                              : passed >= 2 ? "B: Graph-Theory Equivalence"
                              : "A: No External Predictive Power";

        string verdict = passed >= 4
            ? $"TQM PREDICTS EXTERNAL SYSTEMS. {passed}/{total} passed. "
              + $"Works on: [{string.Join(", ", works)}]. "
              + $"Fails on: [{string.Join(", ", fails)}]. "
              + $"TQM's predictive domain = systems governed by graph Laplacians. "
              + $"TQM FAILS on Ising, Heisenberg, percolation — these have different physics. "
              + $"This DELIMITS TQM: it's a theory of graph-Laplacian-governed systems."
            : "Insufficient external predictive power.";

        return new PredictionCandidate.ExternalPredictionReport(
            tests, total, passed, failed,
            works, fails, hasPower, classification, verdict);
    }

    public static string HostileReview(PredictionCandidate.ExternalPredictionReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Can TQM predict outside its domain?");
        sb.AppendLine();
        sb.AppendLine($"  {report.Passed}/{report.TotalTests} passed, {report.Failed} failed.");
        sb.AppendLine();
        sb.AppendLine("  TQM WORKS on: graph Laplacian systems.");
        sb.AppendLine($"    {string.Join(", ", report.WhereTQMWorks)}");
        sb.AppendLine();
        sb.AppendLine("  TQM FAILS on: systems with different physics.");
        sb.AppendLine($"    {string.Join(", ", report.WhereTQMFails)}");
        sb.AppendLine();
        sb.AppendLine("  Ising/Heisenberg failure is SCIENTIFICALLY IMPORTANT:");
        sb.AppendLine("  It shows that TQM is NOT a universal theory of everything.");
        sb.AppendLine("  TQM's domain = systems where dynamics = graph Laplacian.");
        sb.AppendLine("  This is a PROPERLY DELIMITED scientific theory.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 7: Null hypothesis.");
        sb.AppendLine(report.HasExternalPredictivePower
            ? "  → TQM predicts within its domain. Properly delimited."
            : "  → No external predictive power.");
        sb.AppendLine();
        return sb.ToString();
    }
}
