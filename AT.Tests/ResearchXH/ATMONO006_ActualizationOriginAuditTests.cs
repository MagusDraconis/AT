using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-MONO006 — A01 Critical Consistency Check: the actualization origin. Resolve the hostile-referee
/// finding A01 using ONLY accepted derivations [QG278-QG318], no new assumptions, by tracing the
/// dependency graph. Determine whether Actualization is A) primitive, B) derived from Difference, or
/// C) derived from Difference + η.
/// </summary>
public class ATMONO006_ActualizationOriginAuditTests : ResearchTestBase
{
    public ATMONO006_ActualizationOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATMONO0060_DependencyEvidence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0060: the dependency evidence from accepted phases");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - use only accepted derivations [QG268/284/288/292/293/294/295];");
        sb.AppendLine("  - the removal tests [QG292] are the exact procedure for identifying primitives.");
        sb.AppendLine();

        sb.AppendLine($"first appearance of Actualization: {ActualizationOriginAudit.FirstAppearance()}");
        sb.AppendLine();
        foreach (var f in ActualizationOriginAudit.Facts())
        {
            sb.AppendLine($"  {f.Id} [{f.Source}]: {f.Statement}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, ActualizationOriginAudit.Facts().Length);
        Assert.True(ActualizationOriginAudit.DependsOnDifference(),
            "removing Difference must collapse Actualization [QG292 Case A]");
        Assert.True(ActualizationOriginAudit.DoesNotDependOnEta(),
            "removing eta must leave Actualization intact [QG292 Case B]");
        Assert.Contains("QG268", ActualizationOriginAudit.FirstAppearance());
    }

    [Fact]
    public void ATMONO0061_TheDependencyProof()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0061: the dependency proof — Actualization is derived from Difference");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  (a) Actualization requires Difference [E1] and not eta [E2] → NOT primitive, NOT C;");
        sb.AppendLine("  (b) Actualization is Difference's own count-producing process [E3/E4];");
        sb.AppendLine("  (c) therefore Actualization is DERIVED FROM DIFFERENCE [B].");
        sb.AppendLine();

        sb.AppendLine($"depends on Difference: {ActualizationOriginAudit.DependsOnDifference()}");
        sb.AppendLine($"does not depend on eta: {ActualizationOriginAudit.DoesNotDependOnEta()}");
        sb.AppendLine($"is Difference's own dynamics: {ActualizationOriginAudit.IsDifferencesOwnDynamics()}");
        sb.AppendLine($"determination: {ActualizationOriginAudit.Determine()}");
        sb.AppendLine();
        sb.AppendLine($"minimal primitive set: {{{string.Join(", ", ActualizationOriginAudit.MinimalPrimitiveSet())}}}");
        sb.AppendLine($"primitives minimal: {ActualizationOriginAudit.PrimitivesAreMinimal()}");
        sb.AppendLine($"canonical architecture: {string.Join(" → ", ActualizationOriginAudit.CanonicalArchitecture())}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(ActualizationOriginAudit.OriginKind.DerivedFromDifference,
            ActualizationOriginAudit.Determine());
        Assert.Equal(2, ActualizationOriginAudit.MinimalPrimitiveSet().Length);
        Assert.Contains("Difference", ActualizationOriginAudit.MinimalPrimitiveSet());
        Assert.Contains("η", ActualizationOriginAudit.MinimalPrimitiveSet());
        Assert.Equal("Difference", ActualizationOriginAudit.CanonicalArchitecture()[0]);
    }

    [Fact]
    public void ATMONO0062_ContradictionAndConfidence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATMONO0062: the contradiction, the canonical architecture, and the confidence");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the QG318(2) primitive classification of Actualization is the contradiction;");
        sb.AppendLine("  - the canonical monograph {Difference, eta} is CORRECT;");
        sb.AppendLine("  - confidence HIGH — removal tests are decisive in both directions.");
        sb.AppendLine();

        sb.AppendLine($"architecture inconsistent: {ActualizationOriginAudit.ArchitectureIsInconsistent()}");
        sb.AppendLine($"inconsistency source: {ActualizationOriginAudit.InconsistencySource()}");
        sb.AppendLine();
        sb.AppendLine($"resolution score: {ActualizationOriginAudit.ResolutionScore()}/6");
        sb.AppendLine($"VERDICT: {ActualizationOriginAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine($"confidence: {ActualizationOriginAudit.Confidence()}");
        sb.AppendLine();
        sb.AppendLine($"SUMMARY: {ActualizationOriginAudit.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.True(ActualizationOriginAudit.ArchitectureIsInconsistent(),
            "the QG318(2) primitive classification of Actualization must be flagged as inconsistent");
        Assert.Contains("QG318", ActualizationOriginAudit.InconsistencySource());
        Assert.Equal("HIGH", ActualizationOriginAudit.Confidence().Split(' ')[0]);
        Assert.True(ActualizationOriginAudit.ResolutionScore() >= 6);
    }
}
