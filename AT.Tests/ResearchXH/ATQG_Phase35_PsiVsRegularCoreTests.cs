using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 35 — does ψ alone reproduce the regular-core structure M_eff(r)=M(1−e^(−r³/r_c³))? Classify:
/// FULL MATCH / PARTIAL MATCH / NO MATCH.
///
/// Tests: ATQG350 (target profile properties), ATQG351 (ψ qualitative vs exact), ATQG352 (classification).
/// </summary>
public class ATQG_Phase35_PsiVsRegularCoreTests : ResearchTestBase
{
    public ATQG_Phase35_PsiVsRegularCoreTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG350: target regular-mass profile properties ──────────────────────────────

    [Fact]
    public void ATQG350_RegularMassProfile()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG350: the target regular mass M(1-e^(-r^3/rc^3))");

        double M = 1.0, rc = 1.0;
        double core = PsiVsRegularCore.RegularCoreValue(M, rc);
        double atRc = PsiVsRegularCore.RegularMass(rc, M, rc);
        double asymptote = PsiVsRegularCore.RegularAsymptote(M, rc);

        sb.AppendLine($"M_eff(0)      = {core:F6}   (finite core, no divergence)");
        sb.AppendLine($"M_eff(r_c)    = {atRc:F6}   (= M(1-1/e))");
        sb.AppendLine($"M_eff(r→∞)    = {asymptote:F6}   (asymptote → M)");

        bool regularCore = core == 0.0;
        bool correctAsymptote = Math.Abs(asymptote - M) < 1e-6;

        sb.AppendLine();
        sb.AppendLine($"finite core (M_eff(0)=0): {regularCore}");
        sb.AppendLine($"correct asymptote (→M):   {correctAsymptote}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the target profile is regular at the core and asymptotes to the Schwarzschild mass M — the");
        sb.AppendLine("defining features of a regular-core mass function.");
        Output.WriteLine(sb.ToString());

        Assert.True(regularCore, "the core should be finite (M_eff(0)=0)");
        Assert.True(correctAsymptote, "the profile should asymptote to M");
    }

    // ── ATQG351: ψ qualitative match vs exact form ───────────────────────────────────

    [Fact]
    public void ATQG351_PsiQualitativeVsExact()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG351: can ψ generate the specific form without additional assumptions?");

        bool qualitative = PsiVsRegularCore.RegularCoreQualitatively();
        bool exactFormFree = !PsiVsRegularCore.SpecificFormRequiresAssumption();
        bool needsScale = PsiVsRegularCore.NeedsCoreScale();
        int assumptions = PsiVsRegularCore.AdditionalAssumptions();

        sb.AppendLine($"smooth ψ(0)=0 gives a regular core (qualitative): {qualitative}");
        sb.AppendLine($"specific r^3/rc^3 form follows with NO assumptions: {exactFormFree}");
        sb.AppendLine($"specific form needs a core scale rc: {needsScale}");
        sb.AppendLine($"additional assumptions required:      {assumptions}  (a chosen ψ(r) AND a core scale rc)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: ψ is a FREE field — it reproduces the QUALITATIVE regular core (finite M_eff, finite curvature),");
        sb.AppendLine("but the SPECIFIC profile M(1-e^(-r^3/rc^3)) is an ansatz imposed ON ψ, requiring two further inputs:");
        sb.AppendLine("the functional form and a new length scale rc. It is not derivable from ψ alone.");
        Output.WriteLine(sb.ToString());

        Assert.True(qualitative, "smooth psi should give a regular core qualitatively");
        Assert.False(exactFormFree, "the exact form should require an assumption");
        Assert.True(needsScale, "the exact form should need a core scale");
        Assert.Equal(2, assumptions);
    }

    // ── ATQG352: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG352_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG352: FULL MATCH / PARTIAL MATCH / NO MATCH");

        string[] aspects = { "core-regularity", "curvature-finiteness", "horizon-structure", "mass-profile" };
        int full = 0, partial = 0, noMatch = 0;
        foreach (var a in aspects)
        {
            string c = PsiVsRegularCore.ClassifyAspect(a);
            sb.AppendLine($"{a,-22} -> {c}");
            switch (c)
            {
                case "FULL MATCH": full++; break;
                case "PARTIAL MATCH": partial++; break;
                case "NO MATCH": noMatch++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"FULL MATCH    : {full}   (core regularity, curvature finiteness)");
        sb.AppendLine($"PARTIAL MATCH : {partial}   (horizon structure)");
        sb.AppendLine($"NO MATCH      : {noMatch}   (exact mass profile)");
        sb.AppendLine();
        sb.AppendLine($"OVERALL: {PsiVsRegularCore.OverallClassification()}");
        sb.AppendLine();
        sb.AppendLine("ψ reproduces the regular-core STRUCTURE (finite core + finite curvature) for free, but NOT the exact");
        sb.AppendLine("M(1-e^(-r^3/rc^3)) mass function — that specific form is an additional ansatz + a new scale rc.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, full);
        Assert.Equal(1, partial);
        Assert.Equal(1, noMatch);
        Assert.Equal("PARTIAL MATCH", PsiVsRegularCore.OverallClassification());
    }
}
