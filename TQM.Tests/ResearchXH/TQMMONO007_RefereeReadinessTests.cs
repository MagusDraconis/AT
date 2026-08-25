using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-MONO007 — Referee-Readiness Resolution (A02-A09). Resolve the hostile-referee findings of MONO005
/// in the canonical monograph structure without modifying physics: Actualization is derived [A01/MONO006],
/// Difference remains primitive [A02], completeness claims are referee-safe [A03/A05], the Bekenstein
/// boundary is removed from the Emergent gravity chapter [A04], operator sources move to the spectrum
/// chapter [A06], η/π are separated [A07], the synthetic-cohort basis is disclosed [A08], and the
/// superseded MONO001 citation is noted [A09]. VALID001 predictions are separated from boundaries.
/// </summary>
public class TQMMONO007_RefereeReadinessTests : ResearchTestBase
{
    public TQMMONO007_RefereeReadinessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMMONO0070_ActualizationDerivedDifferencePrimitive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0070: Actualization is derived; Difference remains primitive [A01/A02]");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - MONO006 established Actualization is DERIVED FROM DIFFERENCE;");
        sb.AppendLine("  - Difference (Ch1) is the fundamental boundary — not 'derived from a principle'.");
        sb.AppendLine();

        var ch1 = CanonicalMonograph.Chapters().First(c => c.Index == 1);
        var ch3 = CanonicalMonograph.Chapters().First(c => c.Index == 3);
        sb.AppendLine($"Ch1 '{ch1.Title}': kind = {ch1.Kind}");
        sb.AppendLine($"Ch3 '{ch3.Title}': kind = {ch3.Kind}, sources = {string.Join(", ", ch3.Sources)}");
        sb.AppendLine($"Difference is boundary: {CanonicalMonograph.DifferenceIsBoundary()}");
        sb.AppendLine($"Actualization is derived: {CanonicalMonograph.ActualizationIsDerived()}");

        Output.WriteLine(sb.ToString());

        Assert.True(CanonicalMonograph.DifferenceIsBoundary(),
            "Difference must remain the fundamental boundary [not 'Derived']");
        Assert.True(CanonicalMonograph.ActualizationIsDerived(),
            "Actualization must be a Derived chapter and must not cite the operator-layer sources");
        Assert.Equal("Difference", CanonicalMonograph.Primitives[0]);
        Assert.Equal("η", CanonicalMonograph.Primitives[1]);
        Assert.Equal(2, CanonicalMonograph.Primitives.Length);
    }

    [Fact]
    public void TQMMONO0071_BoundarySeparationAndScope()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0071: boundaries are separated from emergent content [A04/A07/VALID001]");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the Bekenstein 1/4 boundary is NOT inside the Emergent gravity chapter [Ch10];");
        sb.AppendLine("  - Ch2 separates the η primitive from the π boundary constant;");
        sb.AppendLine("  - derived-but-unvalidated predictions [S,T,U, a_e, 0νββ] are in Ch17 [validation],");
        sb.AppendLine("    not mixed with the true boundaries in Ch16.");
        sb.AppendLine();

        sb.AppendLine($"Bekenstein not in gravity chapter: {CanonicalMonograph.BekensteinNotInGravity()}");
        sb.AppendLine($"validation separated from boundaries: {CanonicalMonograph.ValidationSeparatedFromBoundaries()}");
        var ch16 = CanonicalMonograph.Chapters().First(c => c.Index == 16);
        var ch17 = CanonicalMonograph.Chapters().First(c => c.Index == 17);
        sb.AppendLine($"Ch16 '{ch16.Title}' scope: {ch16.Scope}");
        sb.AppendLine($"Ch17 '{ch17.Title}' scope: {ch17.Scope}");

        Output.WriteLine(sb.ToString());

        Assert.True(CanonicalMonograph.BekensteinNotInGravity(),
            "the Bekenstein 1/4 boundary must not be inside the Emergent gravity chapter");
        Assert.True(CanonicalMonograph.ValidationSeparatedFromBoundaries(),
            "derived-but-unvalidated predictions must be in Ch17, separated from the Ch16 boundaries");
        Assert.Contains("Bekenstein 2π", ch16.Scope);
        Assert.Contains("EXPERIMENTAL VALIDATION", ch17.Scope);
    }

    [Fact]
    public void TQMMONO0072_CompletenessRefereeSafe()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMMONO0072: completeness claims are referee-safe [A03/A05/A08/A09]");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - 'no fifth operator' is search-scoped [absence-of-evidence, not existence proof];");
        sb.AppendLine("  - the completeness claim carries the hosted-SM-Lagrangian boundary qualifier;");
        sb.AppendLine("  - the lock/phase-transition claims disclose the synthetic-cohort basis.");
        sb.AppendLine();

        sb.AppendLine($"fifth operator search-scoped: {CanonicalMonograph.FifthOperatorSearchScoped()}");
        sb.AppendLine($"completeness referee-safe: {CanonicalMonograph.CompletenessRefereeSafe()}");
        sb.AppendLine($"synthetic cohort disclosed: {CanonicalMonograph.SyntheticCohortDisclosed()}");
        sb.AppendLine($"all referee findings resolved: {CanonicalMonograph.AllRefereeFindingsResolved()}");
        sb.AppendLine($"SUMMARY: {CanonicalMonograph.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.True(CanonicalMonograph.FifthOperatorSearchScoped(),
            "'no fifth operator' must be search-scoped");
        Assert.True(CanonicalMonograph.CompletenessRefereeSafe(),
            "the completeness claim must carry the hosted-SM boundary qualifier");
        Assert.True(CanonicalMonograph.SyntheticCohortDisclosed(),
            "the lock/phase-transition claims must disclose the synthetic-cohort basis");
        Assert.True(CanonicalMonograph.AllRefereeFindingsResolved(),
            "all MONO005 findings A02-A09 must be resolved");
        Assert.Equal("FINAL CANONICAL MONOGRAPH", CanonicalMonograph.Classify());
        Assert.True(CanonicalMonograph.MonographScore() >= 6);
    }
}
