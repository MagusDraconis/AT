using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 34 — identify the irreducible TRM ingredient. Removes each of four candidate ingredients in turn
/// to find which one is responsible for TRM's successes (redshift, regular black holes, weak-field GR recovery).
/// Classify: ESSENTIAL / SECONDARY / REDUNDANT.
///
/// Tests: TQMQG340 (identity of the three aliases), TQMQG341 (removal analysis), TQMQG342 (classification).
/// </summary>
public class TQMQG_Phase34_IrreducibleTRMIngredientTests : ResearchTestBase
{
    public TQMQG_Phase34_IrreducibleTRMIngredientTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG340: M_eff / kernel / temporal-rate are ONE object ───────────────────────

    [Fact]
    public void TQMQG340_ThreeAliasesOneObject()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG340: M_eff, the kernel, and the temporal rate are the same object");

        double phi = 0.5;
        double meff = IrreducibleTRMIngredient.Meff(phi);
        double kernel = IrreducibleTRMIngredient.Kernel(phi);
        bool same = IrreducibleTRMIngredient.KernelIsMeff(phi);
        bool one = IrreducibleTRMIngredient.ThreeIngredientsAreOne();

        sb.AppendLine($"M_eff = e^Φ − 1 = {meff:F6}");
        sb.AppendLine($"kernel n = e^Φ    = {kernel:F6}");
        sb.AppendLine($"n = 1 + M_eff (same object): {same}");
        sb.AppendLine($"three 'ingredients' are one ψ sector: {one}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the effective mass, the propagation kernel, and the temporal-rate modification are ONE");
        sb.AppendLine("mathematical object — the non-conformal ψ factor — written three ways. Removing one removes all three.");
        Output.WriteLine(sb.ToString());

        Assert.True(same, "kernel should equal 1 + M_eff exactly");
        Assert.True(one, "the three should be one object");
    }

    // ── TQMQG341: removal analysis ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG341_RemovalAnalysis()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG341: remove each ingredient in turn — which successes die?");

        bool redshiftNeedsPsi = IrreducibleTRMIngredient.RedshiftRequiresPsi();
        bool weakGrNeedsPsi = IrreducibleTRMIngredient.WeakFieldGrRequiresPsi();
        bool regularBhNeedsPsi = IrreducibleTRMIngredient.RegularBlackHoleRequiresPsi();
        bool cutoffNeeded = IrreducibleTRMIngredient.AnySuccessRequiresCutoff();

        sb.AppendLine($"redshift requires ψ?            : {redshiftNeedsPsi}  (TQM g_00 = −ρ^(2/d) already gives it)");
        sb.AppendLine($"weak-field GR recovery needs ψ? : {weakGrNeedsPsi}  (moves γ −1 → +1)");
        sb.AppendLine($"regular black hole needs ψ?     : {regularBhNeedsPsi}  (finite-curvature horizon)");
        sb.AppendLine($"any success needs the UV cutoff?: {cutoffNeeded}");
        sb.AppendLine();
        sb.AppendLine($"successes surviving WITHOUT ψ:      {IrreducibleTRMIngredient.SurvivingWithoutPsi()}/3  (redshift only)");
        sb.AppendLine($"successes surviving WITHOUT cutoff: {IrreducibleTRMIngredient.SurvivingWithoutCutoff()}/3  (all three)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: removing ψ destroys 2 of 3 successes (weak-field GR + regular BH); removing the UV cutoff");
        sb.AppendLine("destroys nothing. ψ is the load-bearing ingredient; the cutoff is decorative.");
        Output.WriteLine(sb.ToString());

        Assert.False(redshiftNeedsPsi, "redshift should not require psi");
        Assert.True(weakGrNeedsPsi, "weak-field GR should require psi");
        Assert.True(regularBhNeedsPsi, "regular black hole should require psi");
        Assert.False(cutoffNeeded, "no success should require the cutoff");
    }

    // ── TQMQG342: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG342_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG342: ESSENTIAL / SECONDARY / REDUNDANT");

        int essential = 0, secondary = 0, redundant = 0;
        foreach (var ing in IrreducibleTRMIngredient.Ingredients)
        {
            string c = IrreducibleTRMIngredient.Classify(ing);
            sb.AppendLine($"{ing,-20} -> {c}");
            switch (c)
            {
                case "ESSENTIAL": essential++; break;
                case "SECONDARY": secondary++; break;
                case "REDUNDANT": redundant++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"ESSENTIAL : {essential}   (ψ under three names — but ONE object)");
        sb.AppendLine($"SECONDARY : {secondary}");
        sb.AppendLine($"REDUNDANT : {redundant}   (UV cutoff scale)");
        sb.AppendLine();
        sb.AppendLine("IRREDUCIBLE INGREDIENT: the temporal-rate modification ψ (the non-conformal factor). It is the single");
        sb.AppendLine("mathematical ingredient behind all of TRM's successful predictions; M_eff(r) and the propagation kernel are");
        sb.AppendLine("the same object in different clothes. The UV cutoff scale is REDUNDANT for these three successes.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, essential);
        Assert.Equal(0, secondary);
        Assert.Equal(1, redundant);
        Assert.Equal("ESSENTIAL", IrreducibleTRMIngredient.Classify("temporal-rate"));
        Assert.Equal("REDUNDANT", IrreducibleTRMIngredient.Classify("uv-cutoff-scale"));
    }
}
