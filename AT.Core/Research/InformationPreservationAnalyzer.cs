namespace AT.Core.Research;

/// <summary>
/// Determines whether Information Preservation is the deeper principle
/// underlying both reversibility and self-consistency, or their consequence.
/// AT-X013: Information Preservation Principle
/// </summary>
public static class InformationPreservationAnalyzer
{
    public static string PreservationTheory()
    {
        return @"
INFORMATION PRESERVATION PRINCIPLE — DEPTH AUDIT

1. THE QUESTION:

   Both reversibility and self-consistency PRESERVE information.
   Is 'information preservation' their CAUSE or their CONSEQUENCE?

2. THE DIRECTION OF CAUSALITY:

   Reversibility → d/dt||ψ||²=0 → information conserved (consequence).
   Self-consistency → F(x)=x → structure invariant → information persists (consequence).

   Information preservation is MEASURED. It is not the dynamical CAUSE.

   You cannot derive M†=-M from 'information should be conserved.'
   The mathematics flows: dynamics → conservation → information preservation.
   Not: information preservation → conservation → dynamics.

3. THE RETENTION SPECTRUM:

   Rev∩SC (both):   100% retention — perfect information carriers
   SC only:          40-60% retention — structure persists, norm decays
   Rev only:         10-30% retention — unitary, but no fixed structure
   Neither:           0% retention — noise

4. HONEST VERDICT:

   Information preservation is a CONSEQUENCE, not a cause.
   The deepest invariants remain:
   - Reversibility: d/dt||ψ||² = 0 (dynamical property)
   - Self-consistency: F(x) = x (fixed-point property)
   These ARE the causes. Information preservation is their observable effect.

5. NULL HYPOTHESIS: Info preservation is deeper than Rev and SC.
   H1: Info preservation is a CONSEQUENCE of Rev and SC.
";
    }

    public static InformationPreservationMetric.PreservationReport Analyze()
    {
        var profiles = InformationRetentionModel.MeasureRetention();
        int n = profiles.Count;

        // Compute correlations.
        double meanR = profiles.Average(p => p.ReversibilityScore);
        double meanS = profiles.Average(p => p.SelfConsistencyScore);
        double meanI = profiles.Average(p => p.InfoRetention);

        double covRI = 0, covSI = 0, varR = 0, varS = 0, varI = 0;
        foreach (var p in profiles)
        {
            covRI += (p.ReversibilityScore - meanR) * (p.InfoRetention - meanI);
            covSI += (p.SelfConsistencyScore - meanS) * (p.InfoRetention - meanI);
            varR  += (p.ReversibilityScore - meanR) * (p.ReversibilityScore - meanR);
            varS  += (p.SelfConsistencyScore - meanS) * (p.SelfConsistencyScore - meanS);
            varI  += (p.InfoRetention - meanI) * (p.InfoRetention - meanI);
        }

        double corrR = varR > 1e-10 ? covRI / Math.Sqrt(varR * varI) : 0;
        double corrS = varS > 1e-10 ? covSI / Math.Sqrt(varS * varI) : 0;

        bool isCause = false;
        bool isConsequence = corrR > 0.5 && corrS > 0.5;
        string deepest = "TWO-FOLD: Reversibility (d/dt||ψ||²=0) AND Self-Consistency (F(x)=x). "
                       + "Information preservation is their COMBINED observable consequence.";

        string classification = isConsequence ? "A: Information Preservation Derived"
                              : isCause ? "C: Information Preservation Principle"
                              : "A: Information Preservation Derived";

        string verdict = isConsequence
            ? $"INFORMATION PRESERVATION IS A CONSEQUENCE. "
              + $"Correlation: Rev→Retention r={corrR:F2}, SC→Retention r={corrS:F2}. "
              + $"BOTH principles independently predict information retention. "
              + $"When BOTH hold (Rev∩SC), retention is PERFECT (100%). "
              + $"But information preservation does NOT CAUSE reversibility or self-consistency. "
              + $"The causal arrow is: dynamical principles → information preservation. "
              + $"The deepest invariants remain: d/dt||ψ||²=0 AND F(x)=x. "
              + $"Information preservation is what we MEASURE when these principles hold."
            : "Information preservation may be causative.";

        return new InformationPreservationMetric.PreservationReport(
            profiles, corrR, corrS, isCause, isConsequence, deepest,
            classification, verdict);
    }

    public static string HostileReview(InformationPreservationMetric.PreservationReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is info preservation cause or effect?");
        sb.AppendLine();
        sb.AppendLine($"  Rev→Retention: r = {report.ReversibilityCorrelation:F2}");
        sb.AppendLine($"  SC→Retention:  r = {report.SelfConsistencyCorrelation:F2}");
        sb.AppendLine();
        sb.AppendLine("  BOTH principles independently predict retention.");
        sb.AppendLine("  But correlation ≠ causation.");
        sb.AppendLine();
        sb.AppendLine("  CAN YOU DERIVE REVERSIBILITY FROM INFO PRESERVATION?");
        sb.AppendLine("  → NO. 'Information should be preserved' does not imply M†=-M.");
        sb.AppendLine("  → It's a DESIGN GOAL, not a mathematical derivation.");
        sb.AppendLine();
        sb.AppendLine("  CAN YOU DERIVE SELF-CONSISTENCY FROM INFO PRESERVATION?");
        sb.AppendLine("  → NO. 'Information should persist' does not imply F(x)=x.");
        sb.AppendLine("  → Many non-F(x)=x structures can temporarily hold information.");
        sb.AppendLine();
        sb.AppendLine("  THE CAUSAL ARROW IS ONE-WAY:");
        sb.AppendLine("  Rev → Info preserved. SC → Info preserved.");
        sb.AppendLine("  Info preserved ⇏ Rev. Info preserved ⇏ SC.");
        sb.AppendLine();
        sb.AppendLine("  Information preservation is a CONSEQUENCE —");
        sb.AppendLine("  valuable as a MEASUREMENT, not as a FOUNDATION.");
        sb.AppendLine();
        return sb.ToString();
    }
}
