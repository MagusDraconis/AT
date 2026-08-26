using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 219 — Quantum Gravity Reclosure Audit. Re-evaluate the QG status using QG215 (baseline),
/// QG216 (amplitude magnitude), and QG218 (complex structure). Audit only — no new physics.
/// </summary>
public class ATQG_Phase219_QuantumGravityReclosureAuditTests : ResearchTestBase
{
    public ATQG_Phase219_QuantumGravityReclosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2190_ReclosureDeltas()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2190: the reclosure deltas since QG215");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG215 baseline: PARTIAL QG (QM not derived, needs the amplitude/phase primitive).");
        sb.AppendLine("  - Reclosure: QG216 (magnitude) + QG218 (complex structure).");
        sb.AppendLine();

        sb.AppendLine("DELTAS SINCE QG215:");
        foreach (var d in QuantumGravityReclosureAudit.ReclosureDeltas())
            sb.AppendLine($"  {d}");
        sb.AppendLine();

        sb.AppendLine("RE-EVALUATED CRITERIA:");
        sb.AppendLine($"  1. QM derived?    {QuantumGravityReclosureAudit.IsQuantumMechanicsDerived()}");
        sb.AppendLine($"  2. Gravity derived? {QuantumGravityReclosureAudit.IsGravityDerived()}");
        sb.AppendLine($"  3. Same primitive? {QuantumGravityReclosureAudit.SamePrimitiveForBoth()}");
        sb.AppendLine($"  4. Spacetime emergent? {QuantumGravityReclosureAudit.IsSpacetimeEmergent()}");
        sb.AppendLine($"  5. Matter emergent? {QuantumGravityReclosureAudit.IsMatterEmergent()}");
        sb.AppendLine($"  6. Components open? {QuantumGravityReclosureAudit.EssentialComponentsOpen()}");

        Output.WriteLine(sb.ToString());

        Assert.True(QuantumGravityReclosureAudit.IsQuantumMechanicsDerived(), "QM is now substantially derived (QG216+218)");
        Assert.True(QuantumGravityReclosureAudit.IsGravityDerived(), "gravity is derived");
        Assert.True(QuantumGravityReclosureAudit.SamePrimitiveForBoth(), "both pillars share ρ as the core source");
        Assert.True(QuantumGravityReclosureAudit.IsMatterEmergent(), "matter is emergent");
    }

    [Fact]
    public void ATQG2191_RemainingAndResolvedGaps()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2191: which QG215 gaps remain unresolved");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Compare the QG215 missing-pieces list against the QG216/218 results.");
        sb.AppendLine();

        sb.AppendLine("REMAINING GAPS:");
        foreach (var g in QuantumGravityReclosureAudit.RemainingGaps())
            sb.AppendLine($"  {g}");
        sb.AppendLine();

        sb.AppendLine("RESOLVED GAPS:");
        foreach (var g in QuantumGravityReclosureAudit.ResolvedGaps())
            sb.AppendLine($"  {g}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The amplitude magnitude and complex structure (QG216/218) resolve the central QM gap.");
        sb.AppendLine("  - The measurement basis was already resolved (QG74 MATCH).");
        sb.AppendLine("  - Remaining: the phase origin (hosted but not derived), native metric dynamics (BDG");
        sb.AppendLine("    imported), ψ origin status.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, QuantumGravityReclosureAudit.RemainingGaps().Length);
        Assert.Equal(2, QuantumGravityReclosureAudit.ResolvedGaps().Length);
    }

    [Fact]
    public void ATQG2192_ClassificationEffectiveQg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2192: classification — EFFECTIVE QG");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score 0-1 NOT QG, 2-3 PARTIAL QG, 4-5 EFFECTIVE QG, 6 COMPLETE QG.");
        sb.AppendLine();

        int score = QuantumGravityReclosureAudit.QgScore();
        string classification = QuantumGravityReclosureAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  QG score (max 6) = {score}");
        sb.AppendLine($"    +1 QM derived ({QuantumGravityReclosureAudit.IsQuantumMechanicsDerived()})");
        sb.AppendLine($"    +1 gravity derived ({QuantumGravityReclosureAudit.IsGravityDerived()})");
        sb.AppendLine($"    +1 same primitive ({QuantumGravityReclosureAudit.SamePrimitiveForBoth()})");
        sb.AppendLine($"    +1 spacetime emergent ({QuantumGravityReclosureAudit.IsSpacetimeEmergent()})");
        sb.AppendLine($"    +1 matter emergent ({QuantumGravityReclosureAudit.IsMatterEmergent()})");
        sb.AppendLine($"    +1 no components open ({!QuantumGravityReclosureAudit.EssentialComponentsOpen()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG215: PARTIAL QG → QG219: EFFECTIVE QG.");
        sb.AppendLine("  - Both pillars are now derived from the common primitive ρ (gravity + amplitude");
        sb.AppendLine("    magnitude); the complex structure is forced; matter emerges; the metric is derived.");
        sb.AppendLine("  - Remaining primitive/closure origins (phase value, BDG dynamics, ψ) make it");
        sb.AppendLine("    EFFECTIVE rather than COMPLETE.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("EFFECTIVE QG", classification);
        Assert.Equal(4, score);
    }
}
