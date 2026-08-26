using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 264 — Projection Family Audit. Are the density and frequency projections fundamental,
/// or manifestations of a single resonance invariant? No observables, no formulas, D96 only.
/// </summary>
public class ATQG_Phase264_ProjectionFamilyAuditTests : ResearchTestBase
{
    public ATQG_Phase264_ProjectionFamilyAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2640_SharedOriginAndDuality()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2640: shared origin and the frequency → density duality");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - both projections are functions of the SAME 95-mode frequency list (shared origin);");
        sb.AppendLine("  - the octave band count is DETERMINED by the span (frequency → density duality).");
        sb.AppendLine();

        var modes = ProjectionFamilyAudit.Spectrum();
        sb.AppendLine($"spectrum: {modes.Length} modes (ω = √λ) — the single source object");
        sb.AppendLine($"span (frequency) = {ProjectionFamilyAudit.Span():F6}");
        sb.AppendLine($"log2(span) = {ProjectionFamilyAudit.Log2Span():F6}");
        sb.AppendLine($"octave band count (density) = {ProjectionFamilyAudit.OctaveBandCount()}");
        sb.AppendLine($"family count floor(log2 span)+1 = {ProjectionFamilyAudit.FamilyCount()}");
        sb.AppendLine($"→ octave bands determined by span: {ProjectionFamilyAudit.OctaveCountDeterminedBySpan()}");
        sb.AppendLine($"shared origin (both read the one list): {ProjectionFamilyAudit.SharedOrigin()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(95, modes.Length);
        Assert.True(ProjectionFamilyAudit.SharedOrigin());
        Assert.True(ProjectionFamilyAudit.OctaveCountDeterminedBySpan(),
            "the number of density bands is fixed by the frequency span");
    }

    [Fact]
    public void ATQG2641_UnifiedExponentLaw()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2641: the density ↔ frequency duality (unified exponent law)");

        sb.AppendLine("HYPOTHESIS: δ = log(N_eff)/log(span) pairs each DENSITY moment with the FREQUENCY");
        sb.AppendLine("span into ONE exponent — the two projections are not independent inputs.");
        sb.AppendLine();

        foreach (var r in ProjectionFamilyAudit.UnifiedExponents())
            sb.AppendLine($"  {r.Name}: δ = log(N_eff)/log(span) = {r.Predicted:F4} (target {r.Target:F3}, dev {r.Deviation:P2})");
        sb.AppendLine();
        sb.AppendLine($"All four sectors within 2%: {ProjectionFamilyAudit.UnifiedLawReproducesSectors()}");

        Output.WriteLine(sb.ToString());

        Assert.True(ProjectionFamilyAudit.UnifiedLawReproducesSectors(),
            "the single law δ = log(N_eff)/log(span) reproduces all four sectors within 2%");
        Assert.Equal(4, ProjectionFamilyAudit.UnifiedExponents().Length);
    }

    [Fact]
    public void ATQG2642_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2642: the projection-family determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - TWO FUNDAMENTAL PROJECTIONS (score ≤ 2), PARTIAL REDUCTION (3-4),");
        sb.AppendLine("    SINGLE RESONANCE INVARIANT (5-6);");
        sb.AppendLine("  - the common-invariant evidence: octave count from span, unified exponent law,");
        sb.AppendLine("    and the beat identity Σ√m/span ≈ 10.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ProjectionFamilyAudit.Summary()}");
        sb.AppendLine($"Projection score: {ProjectionFamilyAudit.ProjectionScore()}/6");
        sb.AppendLine($"Beat identity Σ√m/span ≈ 10: {ProjectionFamilyAudit.BeatIdentityHolds()}");
        sb.AppendLine($"CLASSIFICATION = {ProjectionFamilyAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - SHARED ORIGIN: both projections are reads of the ONE 95-mode spectrum; no");
        sb.AppendLine("    density quantity exists independently of the frequency list.");
        sb.AppendLine("  - FREQUENCY → DENSITY: the octave band count is floor(log2(span))+1 = 3 — the");
        sb.AppendLine("    frequency projection fixes how many density bands exist.");
        sb.AppendLine("  - DENSITY ↔ FREQUENCY: the unified law δ = log(N_eff)/log(span) combines a density");
        sb.AppendLine("    moment with the frequency span into one exponent (all 4 sectors within 2%).");
        sb.AppendLine("  - COMMON INVARIANT: the beat identity Σ√m/span ≈ 10 couples the two projections.");
        sb.AppendLine("  - The two projections are NOT fundamental — they are dual manifestations of the");
        sb.AppendLine("    single resonance invariant (the D96 spectrum).");
        sb.AppendLine("  - Honest caveat: the operator-to-sector assignment retains QG149-157-era target");
        sb.AppendLine("    information (QG261/262/263); this STRUCTURAL duality is D96-only.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SINGLE RESONANCE INVARIANT", ProjectionFamilyAudit.Classify());
        Assert.True(ProjectionFamilyAudit.ProjectionScore() >= 5);
        Assert.True(ProjectionFamilyAudit.BeatIdentityHolds());
        Assert.Contains("SINGLE RESONANCE INVARIANT", ProjectionFamilyAudit.Summary());
    }
}
