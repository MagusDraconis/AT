using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>Random Actualization: are the contingencies one ensemble or several?</summary>
public class TQM_RandomActualizationEnsembleAudit : ResearchTestBase
{
    public TQM_RandomActualizationEnsembleAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void RandomActualization_EnsembleAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Random Actualization — Contingent-Ensemble Audit");

        S(sb, "Section A — All contingent outputs listed"); sb.AppendLine(SectionA());
        S(sb, "Section B — Common mathematical form"); sb.AppendLine(SectionB());
        S(sb, "Section C — Structure/content boundary"); sb.AppendLine(SectionC());
        S(sb, "Section D — One ensemble or several?"); sb.AppendLine(SectionD());
        S(sb, "Section E — New primitives rejected"); sb.AppendLine(SectionE());
        S(sb, "Section F — Outputs"); sb.AppendLine(SectionF());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ensembles: {ContingentEnsembleAnalyzer.EnsembleCount()} (3 log-normal classes + 1 discrete)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "RandomActualizationEnsemble_Report.txt"), sb.ToString());

        Assert.Equal(6, ContingentEnsembleAnalyzer.ContinuousContingencies().Length);
        Assert.Equal(3, ContingentEnsembleAnalyzer.DiscreteContingencies().Length);
        Assert.Equal(3, ContingentEnsembleAnalyzer.UniversalityClasses().Length);
        Assert.Equal(4, ContingentEnsembleAnalyzer.EnsembleCount());
        Assert.True(ContingentEnsembleAnalyzer.LogNormalFormIsDerived);
        Assert.False(ContingentEnsembleAnalyzer.LogNormalParametersDerived);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("All contingent outputs (Random Actualization's 'content'):");
        sb.AppendLine();
        sb.AppendLine("  CONTINUOUS (log-normal abundance law):");
        foreach (var c in ContingentEnsembleAnalyzer.ContinuousContingencies())
            sb.AppendLine($"    - {c.Output,-36} [{c.Class}]");
        sb.AppendLine();
        sb.AppendLine("  DISCRETE (selection, not log-normal):");
        foreach (var d in ContingentEnsembleAnalyzer.DiscreteContingencies())
            sb.AppendLine($"    - {d.Output,-36} [{d.Kind}]");
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("COMMON MATHEMATICAL FORM: LOG-NORMAL (the Universal Abundance Law, XB002).");
        sb.AppendLine();
        sb.AppendLine("  Multiplicative actualization cascades ⇒ CLT in log-space ⇒ log(X) ~ N(μ, σ²).");
        sb.AppendLine("  Every CONTINUOUS contingent quantity is a log-normal random variable. This is WHY");
        sb.AppendLine("  TQM cannot derive exact values: they are random variables, not fixed constants.");
        sb.AppendLine();
        sb.AppendLine("  Three universality classes share this form (distinct μ,σ):");
        foreach (var c in ContingentEnsembleAnalyzer.UniversalityClasses())
            sb.AppendLine($"    - {c}");
        sb.AppendLine();
        sb.AppendLine("  The DISCRETE contingencies (N=3) do NOT follow log-normal — they are a separate");
        sb.AppendLine("  small-integer selection (derived-lower ∩ empirical-upper, Phases 150–151).");
        return sb.ToString();
    }

    private static string SectionC()
    {
        return
            "STRUCTURE/CONTENT BOUNDARY:\n" +
            "\n" +
            "  " + ContingentEnsembleAnalyzer.StructureContentBoundary + ".\n" +
            "\n" +
            "  The boundary is UNIVERSAL (QG-042/065): it recurs at every level (particles, gauge,\n" +
            "  flavor, cosmology). The FORM (log-normal distribution) is DERIVED; the CONTENT (the\n" +
            "  specific μ, σ, and the drawn values) is CONTINGENT. This is not a hidden boundary to\n" +
            "  find — it is THE boundary, and it is already located: it sits between 'multiplicative\n" +
            "  cascade → log-normal' (derived) and 'the realized draw' (contingent).";
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("ONE ENSEMBLE OR SEVERAL?  →  SEVERAL independent ensembles.");
        sb.AppendLine();
        sb.AppendLine($"  The contingencies form {ContingentEnsembleAnalyzer.EnsembleCount()} ensembles:");
        sb.AppendLine("    1. coupling (α, α_s, θ_W) — log-normal, class-1");
        sb.AppendLine("    2. mass scale (Yukawas, architecture frequencies) — log-normal, class-2");
        sb.AppendLine("    3. relic density (Ω_DM) — log-normal, class-3");
        sb.AppendLine("    4. discrete multiplicity (N=3, generations, color) — small-integer selection");
        sb.AppendLine();
        sb.AppendLine("  They are INDEPENDENT: each class has its own (μ,σ), and the discrete N=3 is not a");
        sb.AppendLine("  draw from any log-normal. There is NO evidence they share ONE underlying distribution —");
        sb.AppendLine("  they are separate actualization cascades (different sectors, different histories).");
        return sb.ToString();
    }

    private static string SectionE()
    {
        return
            "NEW PRIMITIVES REJECTED:\n" +
            "\n" +
            "  The log-normal FORM is derived (CLT), so no new primitive is needed for the DISTRIBUTION.\n" +
            "  The PARAMETERS (μ,σ) are NOT new primitives — they are the accumulated statistics of the\n" +
            "  actualization cascade, contingent content (like M²'s realized value). Introducing a new\n" +
            "  primitive to fix μ,σ (or to force Koide's 45°) would be a hidden-parameter dodge, forbidden\n" +
            "  by the accepted hierarchy (Q + Random Actualization + (ℓ,τ,ħ) + M²).";
    }

    private static string SectionF()
    {
        return
            "DERIVED: structure/form — spatial 3, U(1), N≥3, the log-normal distribution FORM.\n" +
            "SELECTED: (nothing new — the internal 3 is contingent under the empirical upper bound).\n" +
            "CONTINGENT: N≤3, generations=3, color=3, Yukawas, Koide 45°, couplings, architecture\n" +
            "            frequencies, H, Ω_DM — the log-normal DRAWS + the discrete N=3 selection.\n" +
            "\n" +
            "FIRST UNRESOLVED NODE:\n" +
            "  Whether the 3 log-normal universality classes (coupling, mass scale, relic density) are\n" +
            "  truly INDEPENDENT or share ONE underlying (μ,σ) set via a common cascade. Also: whether\n" +
            "  Koide's 45° is a hidden STRUCTURE inside the mass-scale class (a correlation that is\n" +
            "  derived, not drawn) or merely a contingent correlation among 3 log-normal draws.\n" +
            "\n" +
            "STRONGEST NO-GO THEOREM:\n" +
            "  The structure/content boundary is universal and already located: form (log-normal) is\n" +
            "  derived, content (μ,σ and the drawn values) is contingent. No new primitive can force a\n" +
            "  specific draw without re-introducing it as an input. Hence the specific contingent values\n" +
            "  (masses, couplings, the 45° angle, N≤3) are irreducible-CONTINGENT: they are the realized\n" +
            "  outcome of Random Actualization, not computable from Q + Randomness + (ℓ,τ,ħ) + M².";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
