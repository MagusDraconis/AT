namespace AT.Core.Resonance.Theory;

/// <summary>
/// Orchestrates the charge quantization analysis: evaluates candidate
/// mechanisms, attempts fractional charge construction, constructs the
/// mathematical proof, and produces the final classification.
///
/// AT-121: Charge Quantization Mechanism
/// </summary>
public static class ChargeQuantizationAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Full analysis
    // ══════════════════════════════════════════════════════════════════

    public static ChargeSectorProfile.QuantizationReport Analyze()
    {
        return QuantizationMechanism.BuildReport();
    }

    // ══════════════════════════════════════════════════════════════════
    // Mechanism evaluation
    // ══════════════════════════════════════════════════════════════════

    public static string EvaluateMechanisms()
    {
        var mechanisms = ChargeSectorProfile.GetAllMechanisms();
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("CANDIDATE QUANTIZATION MECHANISMS — EVALUATION");
        sb.AppendLine();
        sb.AppendLine("  Mechanism │ Sufficient? │ Necessary? │ Verdict");
        sb.AppendLine("  " + new string('─', 70));

        foreach (var m in mechanisms)
        {
            string verdict = m.IsSufficient && m.IsNecessary ? "COMPLETE"
                           : m.IsSufficient ? "SUFFICIENT ONLY"
                           : m.IsNecessary ? "NECESSARY ONLY"
                           : "INSUFFICIENT";

            sb.AppendLine(
                $"  {m.Name,-42} │ {(m.IsSufficient ? "YES" : "NO"),-10} │ {(m.IsNecessary ? "YES" : "NO"),-10} │ {verdict}");
        }
        sb.AppendLine();

        sb.AppendLine("  CONCLUSION:");
        sb.AppendLine("  Mechanism A (topology/β₀) provides integer nature.");
        sb.AppendLine("  Mechanism C (reaction barrier) provides conservation.");
        sb.AppendLine("  Mechanism G (combined A+C) is the COMPLETE mechanism.");
        sb.AppendLine("  Mechanisms B, D, E, F are consequences or descriptions,");
        sb.AppendLine("  not independent causes of quantization.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Mechanism comparison table
    // ══════════════════════════════════════════════════════════════════

    public static string MechanismComparisonTable()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("  MECHANISM COMPARISON:");
        sb.AppendLine();
        sb.AppendLine("  ID │ Name                    │ Provides        │ Missing");
        sb.AppendLine("  " + new string('─', 75));
        sb.AppendLine("  A  │ Topology (β₀)           │ Q ∈ ℕ           │ dQ/dt=0");
        sb.AppendLine("  B  │ Kink-Antikink Pairs     │ Q ∈ ℕ           │ dQ/dt=0, physical basis");
        sb.AppendLine("  C  │ Reaction-Diff Barrier   │ dQ/dt=0         │ Q ∈ ℕ");
        sb.AppendLine("  D  │ Homotopy Classes        │ Discrete Q      │ dQ/dt=0 (structural)");
        sb.AppendLine("  E  │ Morse Topology          │ Q = #max(R>0.5)│ dQ/dt=0");
        sb.AppendLine("  F  │ Persistent Homology     │ Clean separation │ Q ∈ ℕ, dQ/dt=0");
        sb.AppendLine("  G  │ COMBINED (A+C)          │ Q ∈ ℕ, dQ/dt=0 │ — COMPLETE —");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Delegates for test clarity
    // ══════════════════════════════════════════════════════════════════

    public static string GetQuantizationTheory() => QuantizationMechanism.QuantizationTheory();

    public static string GetResearchQuestions(ChargeSectorProfile.QuantizationReport report)
        => QuantizationMechanism.ResearchQuestions(report);

    public static Dictionary<string, string> GetValidation()
        => QuantizationMechanism.ValidateAgainstPriorExperiments();
}
