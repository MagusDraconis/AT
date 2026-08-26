using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 54 — is ψ a connectivity primitive? Tests whether the spin-2 sector can originate from links.
/// Classify: FIELD / CONNECTIVITY / BOTH / IMPOSSIBLE.
///
/// Tests: ATQG540 (adjacency tensor decomposition), ATQG541 (Weyl content), ATQG542 (classification).
/// </summary>
public class ATQG_Phase54_PsiAsConnectivityTests : ResearchTestBase
{
    public ATQG_Phase54_PsiAsConnectivityTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG540: adjacency tensor carries 2 spin-2 polarizations ────────────────────

    [Fact]
    public void ATQG540_AdjacencyCarriesSpin2()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG540: a symmetric rank-2 adjacency tensor carries 2 spin-2 polarizations");

        int d = 3;
        double comp = PsiAsConnectivity.AdjacencyComponents(d);
        double trace = PsiAsConnectivity.TraceDof();
        double spin2 = PsiAsConnectivity.Spin2Dof(d);

        sb.AppendLine($"adjacency components (d=3): {comp}  (= trace {trace} + traceless {comp - trace})");
        sb.AppendLine($"transverse-traceless (spin-2) polarizations: {spin2}");

        bool twoPolarizations = PsiAsConnectivity.ConnectivityCarriesTwoPolarizations(d);

        sb.AppendLine();
        sb.AppendLine($"connectivity carries 2 independent polarizations: {twoPolarizations}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a symmetric rank-2 link tensor decomposes as 1 scalar (trace) + 5 traceless, and the traceless");
        sb.AppendLine("part contains exactly 2 transverse-traceless (spin-2) modes. Connectivity CAN carry spin-2.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6.0, comp);
        Assert.Equal(1.0, trace);
        Assert.Equal(2.0, spin2);
        Assert.True(twoPolarizations, "connectivity should carry two polarizations");
    }

    // ── ATQG541: ψ = Weyl content of the causal connectivity ────────────────────────

    [Fact]
    public void ATQG541_WeylContent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG541: ψ is the non-conformal (Weyl) content of the causal links");

        bool weyl = PsiAsConnectivity.PsiIsWeylContent();
        bool equivalent = PsiAsConnectivity.FieldAndConnectivityEquivalent();
        bool eliminates = PsiAsConnectivity.EliminatesNewPrimitive();

        sb.AppendLine($"ψ = Weyl (non-conformal) content of the causal connectivity: {weyl}");
        sb.AppendLine($"field and connectivity descriptions are EQUIVALENT:         {equivalent}");
        sb.AppendLine($"connectivity interpretation ELIMINATES the new primitive:  {eliminates}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the causal order fixes the conformal class (light cone); its Weyl tensor is the spin-2 content.");
        sb.AppendLine("The scalar sector froze Weyl = 0 (conformal flatness). ψ = Weyl ≠ 0 is the non-conformal link content —");
        sb.AppendLine("equivalent to a rank-2 field, but now sourced by the CONNECTIVITY, not an external field.");
        Output.WriteLine(sb.ToString());

        Assert.True(weyl, "psi should be the Weyl content");
        Assert.True(equivalent, "field and connectivity should be equivalent");
        Assert.False(eliminates, "the connectivity interpretation should not eliminate the new primitive");
    }

    // ── ATQG542: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG542_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG542: FIELD / CONNECTIVITY / BOTH / IMPOSSIBLE?");

        sb.AppendLine($"CLASSIFICATION: {PsiAsConnectivity.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT IMPOSSIBLE: a symmetric rank-2 link tensor mathematically carries exactly 2 spin-2 polarizations.");
        sb.AppendLine("  • NOT FIELD-ONLY: ψ has a genuine CONNECTIVITY origin — it is the Weyl (non-conformal) content of the");
        sb.AppendLine("    causal link structure, which the scalar sector had frozen to zero.");
        sb.AppendLine("  • BOTH: the field and connectivity descriptions are EQUIVALENT (the Weyl tensor is a rank-2 field), so ψ");
        sb.AppendLine("    can be read either as a fundamental spin-2 field or as the non-conformal connectivity of the network.");
        sb.AppendLine("  • CAVEAT: this is a re-interpretation, not an elimination — Weyl ≠ 0 remains a new degree of freedom.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("BOTH", PsiAsConnectivity.Classify());
        Assert.True(PsiAsConnectivity.ConnectivityCarriesTwoPolarizations(3));
        Assert.True(PsiAsConnectivity.PsiIsWeylContent());
    }
}
