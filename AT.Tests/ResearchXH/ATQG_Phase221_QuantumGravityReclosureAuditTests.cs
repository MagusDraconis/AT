using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 221 — Quantum Gravity Reclosure Audit (after QG220). Re-evaluate the QG status using QG215
/// (baseline), QG216 (amplitude magnitude), QG218 (complex structure), and QG220 (phase origin). Audit only.
/// </summary>
public class ATQG_Phase221_QuantumGravityReclosureAuditTests : ResearchTestBase
{
    public ATQG_Phase221_QuantumGravityReclosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2210_ReclosureDeltas()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2210: the reclosure deltas since QG215 — the phase origin is now closed");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG215 baseline: PARTIAL QG (QM not derived, needs the amplitude/phase primitive).");
        sb.AppendLine("  - Reclosure: QG216 (magnitude) + QG218 (complex structure) + QG220 (phase).");
        sb.AppendLine();

        sb.AppendLine("CLOSURE CHAIN:");
        foreach (var d in QuantumGravityReclosureAudit2.ReclosureDeltas())
            sb.AppendLine($"  {d}");
        sb.AppendLine();

        sb.AppendLine("RE-EVALUATED CRITERIA:");
        sb.AppendLine($"  1. QM derived?    {QuantumGravityReclosureAudit2.IsQuantumMechanicsDerived()}");
        sb.AppendLine($"  2. Gravity derived? {QuantumGravityReclosureAudit2.IsGravityDerived()}");
        sb.AppendLine($"  3. Same primitive? {QuantumGravityReclosureAudit2.SamePrimitiveForBoth()}");
        sb.AppendLine($"  4. Spacetime emergent? {QuantumGravityReclosureAudit2.IsSpacetimeEmergent()}");
        sb.AppendLine($"  5. Matter emergent? {QuantumGravityReclosureAudit2.IsMatterEmergent()}");
        sb.AppendLine($"  6. Components open? {QuantumGravityReclosureAudit2.EssentialComponentsOpen()}");

        Output.WriteLine(sb.ToString());

        Assert.True(QuantumGravityReclosureAudit2.IsQuantumMechanicsDerived(), "QM is now FULLY derived (QG216+218+220)");
        Assert.True(QuantumGravityReclosureAudit2.IsGravityDerived(), "gravity is derived");
        Assert.True(QuantumGravityReclosureAudit2.SamePrimitiveForBoth(), "both pillars share the network (ρ + circulation)");
        Assert.True(QuantumGravityReclosureAudit2.IsMatterEmergent(), "matter is emergent");
    }

    [Fact]
    public void ATQG2211_GapStatuses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2211: which QG219 gaps remain after the phase origin");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Compare the QG219 gap list against the QG220 phase-origin result.");
        sb.AppendLine();

        sb.AppendLine("GAP STATUSES (QG219 → QG221):");
        foreach (var g in QuantumGravityReclosureAudit2.GapStatuses())
        {
            sb.AppendLine($"  {g.Gap}");
            sb.AppendLine($"      → {g.Status}");
        }
        sb.AppendLine();
        sb.AppendLine($"Remaining (non-resolved) gaps: {QuantumGravityReclosureAudit2.RemainingGapCount()}");

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The phase origin (a) is RESOLVED by QG220 — no QM primitive remains.");
        sb.AppendLine("  - The two remaining gaps are both in the GRAVITY/METRIC sector: native metric dynamics");
        sb.AppendLine("    (BDG imported) and the ψ origin status.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, QuantumGravityReclosureAudit2.RemainingGapCount());
        Assert.Equal("RESOLVED by QG220 (PHASE ORIGIN: θ_k = 2πk/N)",
            QuantumGravityReclosureAudit2.GapStatuses()[0].Status);
    }

    [Fact]
    public void ATQG2212_ClassificationNearCompleteQg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2212: classification — NEAR-COMPLETE QG");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score < 3.0 PARTIAL QG; 3.0-4.5 EFFECTIVE QG; 5.0-5.5 NEAR-COMPLETE QG; 6.0 COMPLETE QG.");
        sb.AppendLine();

        double score = QuantumGravityReclosureAudit2.TotalScore();
        string classification = QuantumGravityReclosureAudit2.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var s in QuantumGravityReclosureAudit2.SubScores())
            sb.AppendLine($"  {s.Criterion}: {s.Score:F1}/1.0");
        sb.AppendLine($"  TOTAL = {score:F1}/6");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("PROGRESSION:");
        foreach (var p in QuantumGravityReclosureAudit2.Progression())
            sb.AppendLine($"  {p.Phase}: {p.Status} ({p.Score:F1}/6)");

        sb.AppendLine();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG215 PARTIAL QG (2/6) → QG219 EFFECTIVE QG (4/6) → QG221 NEAR-COMPLETE QG (5/6).");
        sb.AppendLine("  - QM is fully derived (magnitude QG216 + phase QG220 + complex structure QG218 +");
        sb.AppendLine("    measurement QG74); gravity derived; same primitive; matter emergent; metric derived.");
        sb.AppendLine("  - The only open items are two gravity-sector closure issues (native metric dynamics,");
        sb.AppendLine("    ψ origin status) — no QM gap remains, hence NEAR-COMPLETE rather than COMPLETE.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NEAR-COMPLETE QG", classification);
        Assert.Equal(5.0, score, 6);
    }
}
