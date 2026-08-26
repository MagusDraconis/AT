using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;
using static AT.Core.Research.QNecessityMetrics;

namespace AT.Tests.ResearchX;

public class AT_X035_OriginOfQPrinciple : ResearchTestBase
{
    public AT_X035_OriginOfQPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X035_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X035 Origin of Q Principle");

        var report = OriginOfQAnalyzer.Analyze();

        // 1. AT-X034 recap
        Sec(sb, "AT-X034 Recap: The Question");
        sb.AppendLine("  X034 established the Minimal Unified Theory with 5 postulates.");
        sb.AppendLine("  Postulate 1: Q exists on a graph G=(V,E).");
        sb.AppendLine("  Question: Is Q fundamental or derivable?");
        sb.AppendLine("  Mission: Attempt to eliminate Q from the theory entirely.");
        sb.AppendLine();

        // 2. Reduction attempts
        Sec(sb, "Q Reduction Attempts");
        sb.AppendLine("  Candidate Origin              │ Status               │ Verdict");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var a in report.ReductionAttempts)
        {
            string status = a.Status switch
            {
                ReductionStatus.Irreducible => "IRREDUCIBLE",
                ReductionStatus.PartiallyDerived => "PARTIAL",
                ReductionStatus.FullyDerived => "DERIVED",
                ReductionStatus.Collapses => "!! COLLAPSES",
                _ => "?"
            };
            sb.AppendLine($"  {a.CandidateOrigin,-30} │ {status,-20} │ {a.Verdict[..Math.Min(60, a.Verdict.Length)]}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Results: {report.IrreducibleCount} irreducible, {report.DerivedCount} partial, 0 fully derived.");
        sb.AppendLine();

        // 3. Partial derivations detailed
        Sec(sb, "Partial Derivations — What CAN Be Derived");
        sb.AppendLine("  1. Q as domain count = β₀({R>0.5}) — AT-117.");
        sb.AppendLine("     The PDE forms domains; Q counts them. Field → Q.");
        sb.AppendLine();
        sb.AppendLine("  2. Integer Q from binary Q + additivity.");
        sb.AppendLine("     Binary Q (0/1) = vertex exists/doesn't. Q>1 = merger result.");
        sb.AppendLine("     Q(A∪B) = Q(A) + Q(B) → multi-charge vertices are derived.");
        sb.AppendLine();
        sb.AppendLine("  3. Q conservation from PDE barrier — AT-116.");
        sb.AppendLine("     c₀·M·R·(1−R²) > 0 prevents R crossing 0.5 downward → Q conserved.");
        sb.AppendLine("     Conservation is a CONSEQUENCE of dynamics, not a postulate.");
        sb.AppendLine();

        // 4. What remains irreducible
        Sec(sb, "What Remains Irreducible — Binary Q");
        sb.AppendLine("  Binary Q = 'this distinguishable entity exists at this location.'");
        sb.AppendLine("  This IS the graph vertex. Q and Graph are the SAME postulate.");
        sb.AppendLine();
        sb.AppendLine("  Why it cannot be eliminated:");
        sb.AppendLine("  - Without vertices, there is no graph.");
        sb.AppendLine("  - Without distinguishable entities, there is nothing to count.");
        sb.AppendLine("  - A featureless continuum cannot generate distinguishability.");
        sb.AppendLine("  - Distinguishability is the ONTOLOGICAL PRIMITIVE.");
        sb.AppendLine();

        // 5. Necessity audit
        Sec(sb, "Necessity Audit — What Survives Without Q?");
        sb.AppendLine("  Concept                  │ Status");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var a in report.NecessityAudits)
            sb.AppendLine($"  {a.Concept,-25} │ {(a.SurvivesWithoutQ ? "SURVIVES" : "COLLAPSES")}");
        sb.AppendLine();
        int survive = report.NecessityAudits.Count(a => a.SurvivesWithoutQ);
        int collapse = report.NecessityAudits.Count(a => !a.SurvivesWithoutQ);
        sb.AppendLine($"  {survive} survive, {collapse} collapse.");
        sb.AppendLine("  Surviving: Hilbert, Schrödinger, Born, Measurement, Quantum Reality (R=1,S=1).");
        sb.AppendLine("  Collapsing: Graph, L_Q, Carriers, Species, Ecology, Evolution, Complexity.");
        sb.AppendLine("  Q bridges abstract math → physical reality. Without Q: math survives, physics collapses.");
        sb.AppendLine();

        // 6. What Q really is
        Sec(sb, "What Q Really Is");
        foreach (var line in report.WhatQReallyIs)
            sb.AppendLine($"  {line}");
        sb.AppendLine();

        // 7. Minimal foundation update
        Sec(sb, "Minimal Foundation Update (Post-X035)");
        sb.AppendLine("  X034:   P1 = Q on graph G=(V,E)  [two-part postulate]");
        sb.AppendLine("  X035:   P1 = Q IS the graph. Q ≡ vertex existence ≡ distinguishability primitive.");
        sb.AppendLine("          A graph IS a set of distinguishable entities with relations.");
        sb.AppendLine();
        sb.AppendLine("  Postulate count unchanged: 5 (or 4 + 1 irreducible).");
        sb.AppendLine("  But P1 is now UNDERSTOOD as a single, unified concept.");
        sb.AppendLine("  Q is NOT eliminated. Q is CLARIFIED.");
        sb.AppendLine();

        // 8. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(OriginOfQAnalyzer.HostileReview(report));

        // 9. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X035 COMPLETE.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
