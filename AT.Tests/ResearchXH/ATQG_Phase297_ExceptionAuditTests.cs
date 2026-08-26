using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 297 — Exception Audit. Focus: the 5/4 constant. Determine whether 5/4 is derived,
/// structural, an artifact, a fit, or a boundary — and whether every occurrence traces to the same
/// source. No observables, no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase297_ExceptionAuditTests : ResearchTestBase
{
    public ATQG_Phase297_ExceptionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2970_NotDerivedNotStructural()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2970: 5/4 is not derived and not structural");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - no D96 beat identity equals 5/4 (the identities are ≈10, ≈20, ≈12/5, ≈25/3);");
        sb.AppendLine("  - 5/4 = (occ₀+1)/occ₀ is a LABEL IDENTITY without a mechanism — the same standard");
        sb.AppendLine("    QG185 used to reject Bekenstein 1/occ₀ = 1/4.");
        sb.AppendLine();

        sb.AppendLine($"5/4 = {ExceptionAudit.FiveFourths():F2}");
        sb.AppendLine($"not derived from beat identities: {ExceptionAudit.NotDerivedFromBeatIdentities()}");
        sb.AppendLine($"is a label identity ((occ₀+1)/occ₀): {ExceptionAudit.IsLabelIdentity()}");
        sb.AppendLine($"label identity like Bekenstein 1/occ₀: {ExceptionAudit.LabelIdentityLikeBekenstein()}");
        sb.AppendLine($"beat identities: Σ√m/span≈10, occMom/Σm≈20, Σm²/Σm≈12/5, occMom/Σm²≈25/3");
        sb.AppendLine();
        sb.AppendLine("The 'lightest-octave-relative multiplicity' claim (QG238) is the label identity");
        sb.AppendLine("(occ₀+1)/occ₀ = 5/4 — a numerical coincidence without a mechanism, exactly like");
        sb.AppendLine("Bekenstein's 1/occ₀ = 1/4 (QG185: 'a numerical identity without a mechanism').");

        Output.WriteLine(sb.ToString());

        Assert.True(ExceptionAudit.NotDerivedFromBeatIdentities(),
            "no beat identity may equal 5/4");
        Assert.True(ExceptionAudit.IsLabelIdentity(),
            "5/4 must be the label identity (occ₀+1)/occ₀");
        Assert.True(ExceptionAudit.LabelIdentityLikeBekenstein(),
            "the label identity must match the Bekenstein 1/occ₀ standard");
    }

    [Fact]
    public void ATQG2971_FitNotBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2971: 5/4 is a FIT, not a boundary");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the observed ℓ₁ = 220.5 requires the fit factor ℓ₁/(Σm·ln span) ≈ 5/4;");
        sb.AppendLine("  - the QG255/238 inconsistency is an ARTIFACT of the rule's calibration;");
        sb.AppendLine("  - 5/4 is documented as REMOVABLE (QG289), not an irreducible boundary.");
        sb.AppendLine();

        sb.AppendLine($"is a fit factor (ℓ₁/(Σm·ln span) ≈ 5/4): {ExceptionAudit.IsFitFactor()}");
        sb.AppendLine($"rule inconsistency is an artifact: {ExceptionAudit.RuleInconsistencyIsArtifact()}");
        sb.AppendLine($"documented removable (QG289): {ExceptionAudit.DocumentedRemovable()}");
        sb.AppendLine();
        sb.AppendLine("ℓ₁ = Σm·ln(span)·(5/4) = 220.48 matches the observed 220.5 (dev 0.008%)");
        sb.AppendLine("— the 5/4 is precisely the multiplicative factor needed to match observation.");

        Output.WriteLine(sb.ToString());

        Assert.True(ExceptionAudit.IsFitFactor(),
            "5/4 must be the fit factor for ℓ₁");
        Assert.True(ExceptionAudit.RuleInconsistencyIsArtifact(),
            "the QG255/238 inconsistency must be an artifact of the rule's calibration");
        Assert.True(ExceptionAudit.DocumentedRemovable(),
            "5/4 must be documented as removable (QG289)");
    }

    [Fact]
    public void ATQG2972_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2972: the exception determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - EXCEPTION REMAINS: 5/4 is a fit with no D96 origin, occurrences independent;");
        sb.AppendLine("  - the question: can every occurrence of 5/4 be traced to the same source?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ExceptionAudit.Summary()}");
        sb.AppendLine($"Exception score: {ExceptionAudit.ExceptionScore()}/5");
        sb.AppendLine($"5/4 classification: {ExceptionAudit.ClassifyFiveFourths()}");
        sb.AppendLine($"all occurrences same source: {ExceptionAudit.AllOccurrencesSameSource()}");
        sb.AppendLine($"CLASSIFICATION = {ExceptionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("OCCURRENCES OF 5/4:");
        foreach (var o in ExceptionAudit.Occurrences())
        {
            sb.AppendLine($"  {o.Phase} — {o.Context}: {o.Formula}  (same source as ℓ₁: {o.SameSourceAsL1})");
        }
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - 5/4 = FIT: not derived (no beat identity), not structural (label identity),");
        sb.AppendLine("    not a boundary (removable, QG289). It is the fitted factor for ℓ₁.");
        sb.AppendLine("  - NOT every occurrence traces to the same source: the QG238 5/4 (acoustic-peak");
        sb.AppendLine("    fit) and the QG253/255 5/4 (a standard tournament multiplier) are the same");
        sb.AppendLine("    value in different fitting contexts — no single D96 origin.");
        sb.AppendLine("  - The QG280 R4 meta-inconsistency (QG238 uses 5/4, QG255 rejects free constants)");
        sb.AppendLine("    stands — the exception is characterized but not resolved.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("EXCEPTION REMAINS", ExceptionAudit.Classify());
        Assert.True(ExceptionAudit.ExceptionScore() >= 5);
        Assert.Equal(ExceptionAudit.FiveFourthsClass.Fit, ExceptionAudit.ClassifyFiveFourths());
        Assert.True(!ExceptionAudit.AllOccurrencesSameSource());
        Assert.Contains("EXCEPTION REMAINS", ExceptionAudit.Summary());
    }
}
