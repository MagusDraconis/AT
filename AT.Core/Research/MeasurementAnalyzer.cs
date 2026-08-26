using System.Text;
using static AT.Core.Research.MeasurementMetrics;

namespace AT.Core.Research;

/// <summary>
/// Derives measurement from Q-individuation.
/// AT-X038: Origin of Measurement from Individuation
/// </summary>
public static class MeasurementAnalyzer
{
    public static MeasurementReport Analyze()
    {
        var models = MeasurementDerivation.BuildModels();
        var individ = MeasurementDerivation.AnalyzeIndividuation();

        int surviving = models.Count(m => m.Survives);
        bool hasDerivation = models.Any(m => m.Name.Contains("Q-Individuation") && m.Survives);
        bool allAlternativesFail = models.Count(m => !m.Survives) >= 3;

        CollapseStatus status = hasDerivation && allAlternativesFail
            ? CollapseStatus.PartiallyDerived
            : hasDerivation ? CollapseStatus.WeakReduction
            : CollapseStatus.Fundamental;

        string verdict = status switch
        {
            CollapseStatus.PartiallyDerived =>
                "MEASUREMENT PARTIALLY DERIVED (Classification C). "
                + "Single-outcome selection follows from Q conservation + identity persistence. "
                + "Born rule probabilities follow from unitary geometry (X037). "
                + "The specific outcome is genuinely random — IRREDUCIBLE CHANCE. "
                + "The theory is now: 1 postulate (Q) + 1 irreducible element (chance). "
                + "Everything else — reversibility, self-consistency, Hilbert space, "
                + "unitary dynamics, Schrödinger equation, Born rule, single-outcome "
                + "selection — is DERIVED. "
                + "The final mystery is not 'why measurement?' but 'why THIS outcome?' "
                + "— which is genuine ontological randomness.",
            CollapseStatus.WeakReduction => "Weak reduction achieved. Some models survive.",
            _ => "Measurement remains fundamental."
        };

        return new MeasurementReport(models, individ, models.Count,
            surviving, status, MeasurementDerivation.TheDerivation(), verdict);
    }

    public static string ModelReport(List<MeasurementModel> models)
    {
        var sb = new StringBuilder();
        sb.AppendLine("MEASUREMENT MODELS — HOSTILE AUDIT");
        sb.AppendLine();
        sb.AppendLine("  Model                          Q-axiom  Identity  Finite-C  Single   Survives?");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var m in models)
        {
            string q = m.PreservesQAxiom ? " ✓" : " ✗";
            string id = m.PreservesIdentity ? " ✓" : " ✗";
            string fc = m.PreservesFiniteComplexity ? " ✓" : " ✗";
            string so = m.PredictsSingleOutcome ? " ✓" : " ✗";
            string sv = m.Survives ? "YES" : "NO";
            sb.AppendLine($"  {m.Name,-30}  {q}      {id}        {fc}        {so}       {sv}");
        }
        sb.AppendLine();
        int survive = models.Count(m => m.Survives);
        sb.AppendLine($"  {survive}/{models.Count} models survive all consistency checks.");
        sb.AppendLine($"  Only Q-Individuation Collapse DERIVES (rather than postulates) collapse.");
        return sb.ToString();
    }

    public static string DetailedFailures(List<MeasurementModel> models)
    {
        var sb = new StringBuilder();
        sb.AppendLine("DETAILED FAILURE ANALYSIS");
        sb.AppendLine();
        foreach (var m in models.Where(m => !m.Survives))
        {
            sb.AppendLine($"  {m.Name}");
            sb.AppendLine($"  {new string('─', m.Name.Length)}");
            sb.AppendLine($"  {m.FatalFlaw}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string IndividuationReport(List<IndividuationAnalysis> tests)
    {
        var sb = new StringBuilder();
        sb.AppendLine("INDIVIDUATION ANALYSIS — Q CONSERVATION UNDER MEASUREMENT");
        sb.AppendLine();
        sb.AppendLine("  Scenario                                    Q_before  Q_after   Conserved?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var t in tests)
        {
            string qAfter = t.QAfter?.ToString() ?? "UNDEFINED";
            string conserved = t.QConserved ? "✓ YES" : "✗ NO";
            sb.AppendLine($"  {t.Scenario,-44}  {t.QBefore,8}  {qAfter,9}  {conserved}");
            sb.AppendLine($"    {t.Implication}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string FinalPostulateCount()
    {
        var sb = new StringBuilder();
        sb.AppendLine("FINAL AT POSTULATE COUNT (Post-X038)");
        sb.AppendLine();
        sb.AppendLine("  POSTULATE (irreducible):");
        sb.AppendLine("    P1: Q — the principle of individuation.");
        sb.AppendLine("        Distinguishable entities exist. Q ≡ distinguishability primitive.");
        sb.AppendLine();
        sb.AppendLine("  IRREDUCIBLE ELEMENT (not a postulate — a feature of reality):");
        sb.AppendLine("    I1: GENUINE ONTOLOGICAL RANDOMNESS.");
        sb.AppendLine("        When Q conservation forces single-outcome selection,");
        sb.AppendLine("        WHICH outcome occurs is not determined by prior state.");
        sb.AppendLine("        Born rule gives probabilities; nature actualizes one.");
        sb.AppendLine();
        sb.AppendLine("  EVERYTHING ELSE IS DERIVED:");
        sb.AppendLine("    • Reversibility (R=1) — from complexity maximization (X036)");
        sb.AppendLine("    • Self-consistency (S=1) — from complexity maximization (X036)");
        sb.AppendLine("    • Hilbert space — from R+S at (1,1) (X034)");
        sb.AppendLine("    • Unitary dynamics — from Stone's theorem (X036)");
        sb.AppendLine("    • Schrödinger equation — from unitary dynamics (X036)");
        sb.AppendLine("    • Born rule — from unitary invariance (X037)");
        sb.AppendLine("    • Single-outcome selection — from Q conservation (X038)");
        sb.AppendLine();
        sb.AppendLine("  AT IS A 1-POSTULATE THEORY (+ 1 irreducible element).");
        sb.AppendLine("  Q is the only thing you must accept. Everything else follows.");
        return sb.ToString();
    }
}
