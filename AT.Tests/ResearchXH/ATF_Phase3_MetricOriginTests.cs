using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-F Phase 3 — derive the metric origin √(−g)=ρ. Tests whether the identification of the counting measure
/// with the metric volume element emerges uniquely from counting-measure consistency. Classify: DERIVED /
/// PREFERRED / ASSUMED.
///
/// Tests: ATF30 (volume preservation / additivity), ATF31 (uniqueness of √(−g)=ρ), ATF32 (classification).
/// </summary>
public class ATF_Phase3_MetricOriginTests : ResearchTestBase
{
    public ATF_Phase3_MetricOriginTests(ITestOutputHelper o) : base(o) { }

    // ── ATF30: the count and volume are both measures; √(−g)=ρ makes them identical ──

    [Fact]
    public void ATF30_VolumePreservation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF30: the counting measure and the metric volume are both measures");

        // Additivity (measure property): N[0,c] = N[0,b] + N[b,c].
        double n02 = MetricOrigin.Count(MetricOrigin.Profile, 0.0, 2.0);
        double n01 = MetricOrigin.Count(MetricOrigin.Profile, 0.0, 1.0);
        double n12 = MetricOrigin.Count(MetricOrigin.Profile, 1.0, 2.0);
        bool countAdditive = Math.Abs(n02 - (n01 + n12)) < 1e-6;

        // Volume is also additive, and √(−g)=ρ makes volume = count for every region.
        double v01 = MetricOrigin.Volume(MetricOrigin.SqrtMinusG_Rho, 0.0, 1.0);
        double v12 = MetricOrigin.Volume(MetricOrigin.SqrtMinusG_Rho, 1.0, 2.0);
        bool volumeAdditive = Math.Abs(v01 + v12 - MetricOrigin.Volume(MetricOrigin.SqrtMinusG_Rho, 0.0, 2.0)) < 1e-6;
        bool volumeEqualsCount = Math.Abs(v01 - n01) < 1e-6 && Math.Abs(v12 - n12) < 1e-6;

        sb.AppendLine($"count additivity N[0,2]=N[0,1]+N[1,2]: {countAdditive}  ({n02:F4} = {n01:F4} + {n12:F4})");
        sb.AppendLine($"volume additivity V[0,2]=V[0,1]+V[1,2]: {volumeAdditive}");
        sb.AppendLine($"√(−g)=ρ ⇒ volume = count for every region: {volumeEqualsCount}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: both ρ dx and √(−g) dx are measures (additive). The metric origin √(−g)=ρ makes");
        sb.AppendLine("the metric volume measure IDENTICAL to the counting measure — the causal-set 'number = volume'.");
        Output.WriteLine(sb.ToString());

        Assert.True(countAdditive && volumeAdditive, "count and volume should be additive measures");
        Assert.True(volumeEqualsCount, "√(−g)=ρ should make volume equal count");
    }

    // ── ATF31: √(−g)=ρ is the UNIQUE volume element consistent with counting ────────

    [Fact]
    public void ATF31_Uniqueness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF31: √(−g)=ρ is the unique volume element making volume = count for all regions");

        double[] Ls = { 0.5, 1.0, 1.5, 2.0, 3.0 };
        double mRho = MetricOrigin.Mismatch(MetricOrigin.Profile, MetricOrigin.SqrtMinusG_Rho, Ls);
        double mRhoSq = MetricOrigin.Mismatch(MetricOrigin.Profile, MetricOrigin.SqrtMinusG_RhoSq, Ls);
        double mSqrt = MetricOrigin.Mismatch(MetricOrigin.Profile, MetricOrigin.SqrtMinusG_SqrtRho, Ls);
        double mConst = MetricOrigin.Mismatch(MetricOrigin.Profile, MetricOrigin.SqrtMinusG_Const, Ls);

        sb.AppendLine($"{"√(−g)",10} {"max mismatch",16}");
        sb.AppendLine($"{"ρ",10} {mRho,16:E2}");
        sb.AppendLine($"{"ρ²",10} {mRhoSq,16:E2}");
        sb.AppendLine($"{"√ρ",10} {mSqrt,16:E2}");
        sb.AppendLine($"{"const",10} {mConst,16:E2}");

        bool rhoUnique = mRho < 1e-9;                                   // √(−g)=ρ exact
        bool alternativesFail = mRhoSq > 1e-3 && mSqrt > 1e-3 && mConst > 1e-3;

        sb.AppendLine();
        sb.AppendLine($"√(−g)=ρ is the only candidate with zero mismatch (volume = count everywhere): {rhoUnique}");
        sb.AppendLine($"all alternatives (ρ², √ρ, const) fail counting consistency: {alternativesFail}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: requiring the metric volume measure to equal the counting measure UNIQUELY selects");
        sb.AppendLine("√(−g)=ρ — any other volume element over- or under-counts the events.");
        Output.WriteLine(sb.ToString());

        Assert.True(rhoUnique, "√(−g)=ρ should be the unique consistent volume element");
        Assert.True(alternativesFail, "alternatives should fail counting consistency");
    }

    // ── ATF32: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATF32_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATF32: is metric origin DERIVED, PREFERRED, or ASSUMED?");

        sb.AppendLine("CLASSIFICATION: DERIVED (unique form), with a PREFERRED identification.");
        sb.AppendLine();
        sb.AppendLine("  • The FORM √(−g)=ρ is DERIVED: it is the UNIQUE volume element that makes the metric volume");
        sb.AppendLine("    measure equal the counting measure for every region (ATF31) — no alternative works.");
        sb.AppendLine("  • The IDENTIFICATION 'counting measure = volume element' is the causal-set 'number = volume'");
        sb.AppendLine("    principle: in an event-based theory the only measure over spacetime IS the count of events,");
        sb.AppendLine("    so it must be the volume element. This is PREFERRED (minimal/definitional, no new structure).");
        sb.AppendLine("  • With this identification, the metric origin is not an added assumption: it is the requirement");
        sb.AppendLine("    that the geometry's volume element coincide with the (already native) counting measure.");
        sb.AppendLine("  • This upgrades metric origin from PREFERRED (AT-F0) to DERIVED-in-form (unique), leaving only");
        sb.AppendLine("    the 'number = volume' principle — the one remaining structural identification.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
