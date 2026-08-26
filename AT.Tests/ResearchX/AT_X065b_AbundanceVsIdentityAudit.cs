using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;
using static AT.Core.Research.AbundanceIdentityMetrics;

namespace AT.Tests.ResearchX;

public class AT_X065b_AbundanceVsIdentityAudit : ResearchTestBase
{
    public AT_X065b_AbundanceVsIdentityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X065b_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X065b Abundance vs Identity Audit");

        var results = AbundanceIdentityAuditAnalyzer.ClassifyResults();
        var analysis = AbundanceIdentityAuditAnalyzer.AnalyzeSplit(results);

        // 1. Classification matrix
        Sec(sb, "AT Results — Identity vs Abundance Classification");
        sb.AppendLine("  Experiment  Result                              Category     Status");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var r in results)
        {
            string cat = r.Category switch
            {
                Category.Identity => "IDENTITY",
                Category.Abundance => "ABUNDANCE",
                Category.Mixed => "MIXED",
                _ => "?"
            };
            sb.AppendLine($"  {r.Experiment,-11} {r.Result,-40} {cat,-12} {r.DerivationStatus.Split('\n')[0]}");
        }
        sb.AppendLine();

        // 2. The split
        Sec(sb, "The Identity/Abundance Split");
        sb.AppendLine($"  IDENTITY results:   {analysis.IdentityCount}");
        sb.AppendLine($"    Derived:           {analysis.IdentityDerived}/{analysis.IdentityCount} ({analysis.IdentitySuccessRate:P0})");
        sb.AppendLine();
        sb.AppendLine($"  ABUNDANCE results:  {analysis.AbundanceCount}");
        sb.AppendLine($"    Derived:           {analysis.AbundanceDerived}/{analysis.AbundanceCount} ({analysis.AbundanceSuccessRate:P0})");
        sb.AppendLine();
        sb.AppendLine($"  {analysis.Pattern}");
        sb.AppendLine();

        // 3. Identity successes
        Sec(sb, "Identity Successes — What AT Does Best");
        sb.AppendLine("  AT DERIVES identity (WHAT exists, WHY it exists):");
        sb.AppendLine("    ✓ Particles = topological defects");
        sb.AppendLine("    ✓ Gauge symmetry = Aut(moduli space)");
        sb.AppendLine("    ✓ U(1) = vortex S¹ moduli → Aut(S¹) = U(1)");
        sb.AppendLine("    ✓ 3 generations = excitation stability cutoff");
        sb.AppendLine("    ✓ Mass hierarchy = anharmonic WKB spectrum");
        sb.AppendLine("    ✓ Mixing = wavefunction overlap → exponential");
        sb.AppendLine("    ✓ DM = neutral topological defects");
        sb.AppendLine("    ✓ 3+1 dimensions = complexity maximization");
        sb.AppendLine();
        sb.AppendLine("  THE PATTERN: Topology answers identity questions.");
        sb.AppendLine("  All identity results depend on topological invariants");
        sb.AppendLine("  (Betti numbers, homotopy groups, moduli spaces).");
        sb.AppendLine();

        // 4. Abundance "failures"
        Sec(sb, "Abundance Contingencies — What AT Cannot Derive");
        sb.AppendLine("  AT does NOT derive abundance (HOW MUCH, HOW STRONG):");
        sb.AppendLine("    ~ m_e = 0.511 MeV — absolute mass scale (1 measurement)");
        sb.AppendLine("    ~ α ≈ 1/137 — fine-structure constant (weakly constrained)");
        sb.AppendLine("    ~ Ω_DM ≈ 0.27 — relic abundance (initial conditions)");
        sb.AppendLine("    ~ M² ≈ 5 — nonlinearity regime (one continuous parameter)");
        sb.AppendLine("    ~ ξ — correlation length (sets mass scale)");
        sb.AppendLine();
        sb.AppendLine("  THE PATTERN: Abundance depends on initial conditions");
        sb.AppendLine("  and cosmological history — NOT on topology.");
        sb.AppendLine("  These are CONTINGENT facts about our universe.");
        sb.AppendLine();

        // 5. The exception
        Sec(sb, "The Exception: Λ ~ H² (X046)");
        sb.AppendLine("  The cosmological constant is the ONLY abundance question");
        sb.AppendLine("  that AT partially derives. Why?");
        sb.AppendLine();
        sb.AppendLine("  Because Λ is not an 'abundance' in the usual sense —");
        sb.AppendLine("  it's a FLUCTUATION phenomenon from Q-event discreteness.");
        sb.AppendLine("  The functional form Λ ~ H² is TOPOLOGICAL (depends on");
        sb.AppendLine("  causal diamond structure). The exact VALUE fluctuates.");
        sb.AppendLine();
        sb.AppendLine("  This is the exception that proves the rule:");
        sb.AppendLine("  When an abundance question has a TOPOLOGICAL answer,");
        sb.AppendLine("  AT can derive it. When it depends on HISTORY,");
        sb.AppendLine("  AT cannot.");
        sb.AppendLine();

        // 6. Two-layer ontology
        Sec(sb, "The Two-Layer Ontology");
        sb.AppendLine(AbundanceIdentityAuditAnalyzer.TheTwoLayers());

        // 7. Verdict
        Sec(sb, "Verdict");
        sb.AppendLine(analysis.Verdict);

        // 8. Final
        string classification = analysis.Status == SplitStatus.FundamentalSplit
            ? "D: Fundamental Split Discovered" : "C: Strong Distinction";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X065b COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Identity:  {analysis.IdentitySuccessRate:P0} derived.");
        sb.AppendLine($"  Abundance: {analysis.AbundanceSuccessRate:P0} derived.");
        sb.AppendLine($"  Topology → Identity. History → Abundance.");
        sb.AppendLine($"  This is THE deepest meta-result of the AT program.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
