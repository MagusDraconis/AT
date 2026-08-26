using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

/// <summary>Can the 3 log-normal classes be projections of one cascade?</summary>
public class AT_CascadeUnificationAudit : ResearchTestBase
{
    public AT_CascadeUnificationAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void CascadeUnification_Audit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("One Cascade or Three? — Universality-Class Unification Audit");

        S(sb, "Section A — The three classes and their spans"); sb.AppendLine(SectionA());
        S(sb, "Section B — Shared cascade or independent?"); sb.AppendLine(SectionB());
        S(sb, "Section C — The underdetermination argument"); sb.AppendLine(SectionC());
        S(sb, "Section D — Rejected fallacies"); sb.AppendLine(SectionD());
        S(sb, "Section E — Outputs"); sb.AppendLine(SectionE());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  DEFAULT: independent cascades (distinct mechanisms; one universe cannot distinguish)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "CascadeUnification_Report.txt"), sb.ToString());

        Assert.Equal(3, CascadeUnificationAnalyzer.ClassSpans().Length);
        Assert.Equal(3, CascadeUnificationAnalyzer.GenerationMechanisms().Length);
        // The three classes have widely different spans (couplings ~1 dex, Yukawas ~6 dex, Ω_DM ~0).
        var spans = CascadeUnificationAnalyzer.ClassSpans();
        Assert.True(spans[1].SpanDex > 3 * spans[0].SpanDex); // mass scale ≫ coupling span
        Assert.False(CascadeUnificationAnalyzer.SingleUniverseCanDistinguish);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("The three universality classes and their observed log10 span:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,8}", "class", "span [dex]"));
        foreach (var c in CascadeUnificationAnalyzer.ClassSpans())
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,8:F1}", c.Class, c.SpanDex));
        sb.AppendLine();
        sb.AppendLine("  The spans differ by ORDERS OF MAGNITUDE: couplings ~1 dex, mass scale ~6 dex, relic");
        sb.AppendLine("  density a single value. A single multiplicative cascade produces ONE σ; three classes");
        sb.AppendLine("  with three different σ require either three cascades or one cascade with channel gains");
        sb.AppendLine("  (a new primitive).");
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Distinct physical generation mechanisms:");
        sb.AppendLine();
        foreach (var m in CascadeUnificationAnalyzer.GenerationMechanisms())
            sb.AppendLine($"  - {m}");
        sb.AppendLine();
        sb.AppendLine("  These are three DIFFERENT actualization processes (RG running, architecture overlap,");
        sb.AppendLine("  freezeout), not three channels of one process. There is no known mechanism tying them");
        sb.AppendLine("  to a single cascade, and no evidence of shared (μ,σ).");
        sb.AppendLine();
        sb.AppendLine("  ⇒ DEFAULT (parsimonious): INDEPENDENT cascades.");
        return sb.ToString();
    }

    private static string SectionC()
    {
        return
            "THE UNDERDETERMINATION ARGUMENT (decisive):\n" +
            "\n" +
            "  We observe exactly ONE universe (one realized draw from each class). A single\n" +
            "  realization CANNOT distinguish 'one cascade with 3 channels' from '3 independent\n" +
            "  cascades' — both can fit the single observed value (μ,σ) of each class. Testing\n" +
            "  shared-cascade vs independent would require MULTI-UNIVERSE statistics (correlations\n" +
            "  across realizations), which are unavailable.\n" +
            "\n" +
            "  What we CAN say: the three classes have widely different σ (1 vs 6 vs 0 dex), and\n" +
            "  three distinct generation mechanisms. A single cascade would need channel gains to\n" +
            "  reproduce these σ — i.e. a new primitive (forbidden). Hence, under the constraints," +
            "\n" +
            "  the shared-cascade hypothesis is UNTESTABLE and UNMOTIVATED, and the parsimonious\n" +
            "  conclusion is independent cascades.";
    }

    private static string SectionD()
    {
        return
            "REJECTED:\n" +
            "  - NEW PRIMITIVES: 'channel gains' to make one cascade fit 3 σ is a hidden parameter. REJECTED.\n" +
            "  - ANTHROPICS: 'the observed classes are those compatible with life' does not unify the cascade. REJECTED.\n" +
            "  - NUMEROLOGY: '3 classes = 3 generations = 3 spatial dims' lacks a mechanism. REJECTED.";
    }

    private static string SectionE()
    {
        return
            "SHARED CASCADE?  NO (untestable + unmotivated; needs channel gains = new primitive).\n" +
            "INDEPENDENT CASCADES?  YES (parsimonious default: 3 distinct mechanisms, 3 distinct σ).\n" +
            "\n" +
            "STRONGEST NO-GO THEOREM:\n" +
            "  A single universe's realization cannot distinguish one cascade from three (underdetermined),\n" +
            "  and the three classes have σ differing by orders of magnitude (1 vs 6 vs 0 dex) with three\n" +
            "  distinct generation mechanisms (RG running, architecture overlap, freezeout). Reproducing\n" +
            "  these σ from one cascade requires channel gains — a new primitive, forbidden. Therefore the\n" +
            "  shared-cascade hypothesis is irreducible-UNRESOLVABLE within AT: it cannot be tested and\n" +
            "  cannot be derived without violating the no-new-primitives constraint.\n" +
            "\n" +
            "STRONGEST DERIVATION PATH:\n" +
            "  Show that the architecture-overlap mechanism (which generates the mass scale) ALSO generates\n" +
            "  the gauge couplings and Ω_DM as its low/high moments. This would unify the 3 classes as one\n" +
            "  cascade without a new primitive — but it requires the overlap operator (Y) to simultaneously\n" +
            "  determine α, α_s, θ_W and Ω_DM, for which there is currently no derivation (the couplings run\n" +
            "  by RG, not by overlap). Blocked.\n" +
            "\n" +
            "SUCCESS PROBABILITY (proving one cascade): ≈ 0.05.\n" +
            "  It would need (a) a mechanism linking 3 distinct processes, and (b) multi-universe statistics\n" +
            "  to test — neither is available without new primitives or a multiverse assumption.";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
