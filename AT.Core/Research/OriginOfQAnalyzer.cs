using System.Text;
using static AT.Core.Research.QNecessityMetrics;

namespace AT.Core.Research;

/// <summary>
/// Determines whether Q is fundamental, derivable, or emergent.
/// AT-X035: Origin of Q Principle
/// </summary>
public static class OriginOfQAnalyzer
{
    public static OriginOfQReport Analyze()
    {
        var attempts = QReductionAudit.AttemptReductions();
        var audits = QReductionAudit.AuditNecessity();
        var whatQIs = QReductionAudit.WhatQReallyIs();

        int irreducible = attempts.Count(a => a.Status == ReductionStatus.Irreducible);
        int partial = attempts.Count(a => a.Status == ReductionStatus.PartiallyDerived);
        int derived = attempts.Count(a => a.Status == ReductionStatus.FullyDerived);

        // Q is partially derivable (binary Q = graph vertex) but irreducible at core
        bool qEliminated = derived >= attempts.Count;
        bool qMostlyIrreducible = irreducible >= 7;

        string classification = qEliminated ? "A: Q Derived"
            : qMostlyIrreducible ? "D: Q is the Final Irreducible Primitive (with partial derivations)"
            : partial >= 3 ? "C: Q Necessary"
            : "B: Partial Reduction";

        string verdict = qMostlyIrreducible
            ? "Q IS THE FINAL IRREDUCIBLE PRIMITIVE. 7/10 reduction attempts failed outright. "
              + "3 partial derivations succeed but only reduce ASPECTS of Q: binary Q ≡ graph vertex, "
              + "domain-count Q ≡ β₀, integer Q emerges from additivity. "
              + "The CORE of Q — 'there exist distinguishable, countable entities' — "
              + "cannot be derived from anything deeper. Without Q: no graph, no L_Q, "
              + "no carriers, no species, no ecology, no evolution, no complexity. "
              + "Q is the PRINCIPLE OF INDIVIDUATION. "
              + "It is the discreteness primitive that makes reality countable. "
              + "Q and Graph are the SAME postulate: 'There exists a set of distinguishable entities with relations.' "
              + "POSTULATE 1 COLLAPSES FROM TWO TO ONE: Q ≡ Graph ≡ the existence of distinguishable relata."
            : "Q is partially derivable.";

        return new OriginOfQReport(attempts, audits, attempts.Count,
            irreducible, partial, classification, whatQIs, verdict);
    }

    public static string FullReport(OriginOfQReport report)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ORIGIN OF Q — REDUCTION ANALYSIS");
        sb.AppendLine(new string('=', 70));
        sb.AppendLine();
        sb.AppendLine($"  Reduction attempts: {report.AttemptsCount}");
        sb.AppendLine($"  Irreducible:        {report.IrreducibleCount}");
        sb.AppendLine($"  Partially derived:  {report.DerivedCount}");
        sb.AppendLine();
        sb.AppendLine("  CANDIDATE ORIGINS:");
        sb.AppendLine();
        foreach (var a in report.ReductionAttempts)
        {
            string icon = a.Status switch
            {
                ReductionStatus.Irreducible => "✗",
                ReductionStatus.PartiallyDerived => "~",
                ReductionStatus.FullyDerived => "✓",
                ReductionStatus.Collapses => "!!",
                _ => "?"
            };
            sb.AppendLine($"  {icon} {a.CandidateOrigin}");
            sb.AppendLine($"    Status: {a.Status}");
            sb.AppendLine($"    {a.Verdict}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string NecessityReport(List<QNecessityAudit> audits)
    {
        var sb = new StringBuilder();
        sb.AppendLine("NECESSITY AUDIT: What survives without Q?");
        sb.AppendLine();
        foreach (var a in audits)
        {
            string status = a.SurvivesWithoutQ ? "SURVIVES" : "COLLAPSES";
            sb.AppendLine($"  {a.Concept,-25} → {status}");
            sb.AppendLine($"    {a.Notes}");
            sb.AppendLine();
        }
        int survive = audits.Count(a => a.SurvivesWithoutQ);
        int collapse = audits.Count(a => !a.SurvivesWithoutQ);
        sb.AppendLine($"  {survive} survive, {collapse} collapse.");
        sb.AppendLine("  What survives: abstract mathematical structures (Hilbert, Schrödinger, QM point).");
        sb.AppendLine("  What collapses: everything that involves countable entities.");
        sb.AppendLine("  Q is the bridge between abstract math and physical reality.");
        return sb.ToString();
    }

    public static string HostileReview(OriginOfQReport report)
    {
        var sb = new StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is Q really irreducible?");
        sb.AppendLine();
        sb.AppendLine("  FINAL ATTEMPT: Eliminate Q by merging it with Graph.");
        sb.AppendLine();
        sb.AppendLine("  Claim: Q ≡ Graph. 'Q exists on a graph' is ONE postulate, not two.");
        sb.AppendLine("  A vertex IS a charge. A graph IS a set of charges with relations.");
        sb.AppendLine();
        sb.AppendLine("  COUNTER-CLAIM: This is semantics. Merging Q and Graph doesn't ELIMINATE");
        sb.AppendLine("  the concept — it just repackages it. You still need:");
        sb.AppendLine("    1. Distinguishable entities (Q-ness)");
        sb.AppendLine("    2. Relations between them (graph-ness)");
        sb.AppendLine("  Whether you call it one postulate or two, both ingredients are needed.");
        sb.AppendLine();
        sb.AppendLine("  DEEPER QUESTION: Can 'distinguishable entity' be derived?");
        sb.AppendLine();
        sb.AppendLine("  Consider a universe with NO distinguishable entities:");
        sb.AppendLine("    - All points are identical.");
        sb.AppendLine("    - No labels, no counting, no identity.");
        sb.AppendLine("    - This is a FEATURELESS CONTINUUM.");
        sb.AppendLine();
        sb.AppendLine("  Can a featureless continuum spontaneously develop distinguishable entities?");
        sb.AppendLine("    - Spontaneous symmetry breaking: a continuous field develops domains.");
        sb.AppendLine("    - But domains only exist if the field HAS structure to break.");
        sb.AppendLine("    - The field must already have values at points for domains to form.");
        sb.AppendLine("    - Those points must already BE points — distinguishable locations.");
        sb.AppendLine();
        sb.AppendLine("  Distinguishability requires a PRE-EXISTING set of distinct locations.");
        sb.AppendLine("  A graph provides this: vertices are distinguishable BY DEFINITION.");
        sb.AppendLine("  Q is THE NAME we give to this distinguishability.");
        sb.AppendLine();
        sb.AppendLine("  CONCLUSION: Q cannot be derived from a featureless substrate because");
        sb.AppendLine("  distinguishability must be BUILT INTO the substrate. It is the");
        sb.AppendLine("  ontological primitive — the thing that makes 'things' possible.");
        sb.AppendLine();
        sb.AppendLine("  Q IS THE PRINCIPLE OF INDIVIDUATION.");
        sb.AppendLine("  Q IS THE FINAL IRREDUCIBLE POSTULATE OF AT.");
        sb.AppendLine();
        return sb.ToString();
    }

    public static string MinimalFoundationUpdate(OriginOfQReport report)
    {
        var sb = new StringBuilder();
        sb.AppendLine("MINIMAL FOUNDATION UPDATE");
        sb.AppendLine();
        sb.AppendLine("  X034 had 5 postulates:");
        sb.AppendLine("    P1: Q on graph G=(V,E)");
        sb.AppendLine("    P2: Reversibility");
        sb.AppendLine("    P3: Self-Consistency");
        sb.AppendLine("    P4: Born Rule");
        sb.AppendLine("    P5: Measurement");
        sb.AppendLine();
        sb.AppendLine("  X035 refines P1:");
        sb.AppendLine("    P1: Q IS the graph. A graph IS a set of distinguishable entities");
        sb.AppendLine("        (vertices labeled by Q) with relations (edges).");
        sb.AppendLine("    Q ≡ vertex existence ≡ distinguishability primitive.");
        sb.AppendLine();
        sb.AppendLine("  This is NOT a reduction to 4 postulates — it's a CLARIFICATION.");
        sb.AppendLine("  The content is identical. Q and Graph are two aspects of one thing:");
        sb.AppendLine("  the existence of distinguishable, related entities.");
        sb.AppendLine();
        sb.AppendLine("  FINAL POSTULATE COUNT: still 5 (or 4 + 1 irreducible).");
        sb.AppendLine("  Q is NOT eliminated. Q is UNDERSTOOD.");
        return sb.ToString();
    }
}
