using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 53 — dependency audit. Maps which conclusions depend on which assumptions.
/// Classify: ASSUMPTION-FREE / DERIVED / OBSERVATION-DEPENDENT / MODEL-DEPENDENT.
///
/// Tests: ATQG530 (dependency graph), ATQG531 (the derived chain), ATQG532 (weakest links).
/// </summary>
public class ATQG_Phase53_DependencyAuditTests : ResearchTestBase
{
    public ATQG_Phase53_DependencyAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG530: dependency graph classification ────────────────────────────────────

    [Fact]
    public void ATQG530_DependencyGraph()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG530: classify every node of the dependency graph");

        int assumptionFree = 0, derived = 0, observation = 0, model = 0;
        foreach (var n in DependencyAudit.Nodes)
        {
            string c = DependencyAudit.Classify(n);
            sb.AppendLine($"{n,-18} <- {DependencyAudit.DependsOn(n),-24} -> {c}");
            switch (c)
            {
                case "ASSUMPTION-FREE": assumptionFree++; break;
                case "DERIVED": derived++; break;
                case "OBSERVATION-DEPENDENT": observation++; break;
                case "MODEL-DEPENDENT": model++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"ASSUMPTION-FREE       : {assumptionFree}");
        sb.AppendLine($"DERIVED               : {derived}");
        sb.AppendLine($"OBSERVATION-DEPENDENT : {observation}");
        sb.AppendLine($"MODEL-DEPENDENT       : {model}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, assumptionFree);
        Assert.Equal(5, derived);
        Assert.Equal(0, observation);
        Assert.Equal(2, model);
    }

    // ── ATQG531: the derived chain ───────────────────────────────────────────────────

    [Fact]
    public void ATQG531_DerivedChain()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG531: the scalar backbone is fully derived");

        sb.AppendLine("DERIVED CHAIN (all from Q-events + principles):");
        sb.AppendLine("  Q-events ──→ ρ ──→ geometry ──→ gravity");
        sb.AppendLine("       │          └──→ matter");
        sb.AppendLine("       └──→ saturation");
        sb.AppendLine();
        bool allDerived = true;
        foreach (var n in new[] { "rho", "geometry", "matter", "gravity", "saturation" })
            allDerived &= DependencyAudit.Classify(n) == "DERIVED";

        sb.AppendLine($"all five scalar nodes DERIVED: {allDerived}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: counting measure, geometry, matter, gravity, and saturation all follow from the single");
        sb.AppendLine("Q-events primitive — the scalar backbone has NO free assumptions beyond Q-events (and the preferred η).");
        Output.WriteLine(sb.ToString());

        Assert.True(allDerived, "the scalar chain should be fully derived");
        Assert.Equal("DERIVED", DependencyAudit.Classify("gravity"));
    }

    // ── ATQG532: weakest links ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG532_WeakestLinks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG532: the weakest remaining links");

        foreach (var w in DependencyAudit.WeakestLinks)
            sb.AppendLine($"  • {w} -> {DependencyAudit.Classify(w)}");

        sb.AppendLine();
        sb.AppendLine("WEAKEST LINKS:");
        sb.AppendLine("  • ψ — its necessity rests entirely on the spin-2 reading of the GW strain, which is itself model-dependent.");
        sb.AppendLine("  • GW interpretation — spin-2 is RECONSTRUCTED, not directly measured (QG48).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the scalar backbone is robust (derived from Q-events alone), but the ENTIRE tensor sector hangs");
        sb.AppendLine("on a single model-dependent link: the spin-2 interpretation of the gravitational-wave strain.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, DependencyAudit.WeakestLinks.Length);
        Assert.Equal("MODEL-DEPENDENT", DependencyAudit.Classify("psi"));
        Assert.Equal("MODEL-DEPENDENT", DependencyAudit.Classify("gw-interpretation"));
    }
}
