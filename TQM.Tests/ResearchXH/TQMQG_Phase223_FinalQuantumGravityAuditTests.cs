using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 223 — Final Quantum Gravity Audit. Re-evaluate the QG status after QG222 (native metric
/// dynamics) and adjudicate the ψ origin status. Audit only — no new derivations, no new physics.
/// </summary>
public class TQMQG_Phase223_FinalQuantumGravityAuditTests : ResearchTestBase
{
    public TQMQG_Phase223_FinalQuantumGravityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2230_SixCriteriaComplete()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2230: the six criteria — all hold after QG222");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Review QG215 → QG219 → QG221 → QG222; QG222 makes the metric dynamics native.");
        sb.AppendLine();

        sb.AppendLine("RE-EVALUATED CRITERIA:");
        sb.AppendLine($"  1. QM derived?         {FinalQuantumGravityAudit.IsQuantumMechanicsDerived()}  (magnitude QG216 + phase QG220 + structure QG218 + measurement QG74)");
        sb.AppendLine($"  2. Gravity derived?    {FinalQuantumGravityAudit.IsGravityDerived()}  (structure QG197/207 + observables QG181-213 + native dynamics QG222)");
        sb.AppendLine($"  3. Common primitive?   {FinalQuantumGravityAudit.CommonPrimitive()}  (both from ρ + the actualization circulation)");
        sb.AppendLine($"  4. Spacetime emergent? {FinalQuantumGravityAudit.IsSpacetimeEmergent()}  (structure QG207 + dynamics QG222 — no longer partial)");
        sb.AppendLine($"  5. Matter emergent?    {FinalQuantumGravityAudit.IsMatterEmergent()}  (QG195/196/203-210)");
        sb.AppendLine($"  6. Remaining blockers? {FinalQuantumGravityAudit.HasRemainingBlockers()}  (ψ is a boundary, not a blocker)");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Criterion 4 upgraded from PARTIAL (QG221) to YES: QG222 derived the metric dynamics,");
        sb.AppendLine("    so spacetime is fully emergent from the counting measure.");
        sb.AppendLine("  - Criterion 6: the only nominally-open item (ψ origin) is adjudicated as a boundary,");
        sb.AppendLine("    not a blocker — no blocker remains in the derived program.");

        Output.WriteLine(sb.ToString());

        Assert.True(FinalQuantumGravityAudit.IsQuantumMechanicsDerived(), "QM is fully derived");
        Assert.True(FinalQuantumGravityAudit.IsGravityDerived(), "gravity is derived");
        Assert.True(FinalQuantumGravityAudit.CommonPrimitive(), "both pillars share the network primitive");
        Assert.True(FinalQuantumGravityAudit.IsSpacetimeEmergent(), "spacetime is now fully emergent (QG222)");
        Assert.True(FinalQuantumGravityAudit.IsMatterEmergent(), "matter is emergent");
        Assert.False(FinalQuantumGravityAudit.HasRemainingBlockers(), "no QG blocker remains");
    }

    [Fact]
    public void TQMQG2231_PsiStatusAdjudication()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2231: the ψ origin status — boundary and tensor-sector question, NOT a blocker");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Capacity forced (QG56), excitation derived (QG57), observables derived (QG103/186/212).");
        sb.AppendLine("  - ψ is the second of exactly two primitives (QG51/40); existence observational (QG47).");
        sb.AppendLine();

        sb.AppendLine("ψ ADJUDICATION:");
        foreach (var (q, v) in FinalQuantumGravityAudit.PsiAdjudication())
            sb.AppendLine($"  {q}  {v}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - NOT a QG blocker: every functional layer (capacity, excitation, observables) is resolved.");
        sb.AppendLine("  - IS an ontological boundary: ψ is the second of two primitives; its existence is");
        sb.AppendLine("    observationally demanded (GW spin-2), not derivable from the scalar sector.");
        sb.AppendLine("  - IS a separate tensor-sector question: distinct spin, role, and equation (QG50/44).");

        Output.WriteLine(sb.ToString());

        Assert.False(FinalQuantumGravityAudit.PsiIsQgBlocker(), "ψ is not a QG blocker");
        Assert.True(FinalQuantumGravityAudit.PsiIsOntologicalBoundary(), "ψ is an ontological boundary");
        Assert.True(FinalQuantumGravityAudit.PsiIsSeparateTensorSectorQuestion(), "ψ is a tensor-sector question");
        Assert.True(FinalQuantumGravityAudit.PsiCapacityForced(), "the Weyl capacity is forced (QG56)");
        Assert.True(FinalQuantumGravityAudit.PsiExcitationDerived(), "the excitation mechanism is derived (QG57)");
        Assert.True(FinalQuantumGravityAudit.PsiIsNewPrimitive(), "ψ is a new fundamental primitive");
        Assert.True(FinalQuantumGravityAudit.PsiExistenceObservational(), "ψ's existence is observational");
    }

    [Fact]
    public void TQMQG2232_ClassificationCompleteQg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2232: classification — COMPLETE QG");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score < 3.0 PARTIAL QG; 3.0-4.5 EFFECTIVE QG; 5.0-5.5 NEAR-COMPLETE QG; 6.0 COMPLETE QG.");
        sb.AppendLine();

        double score = FinalQuantumGravityAudit.TotalScore();
        string classification = FinalQuantumGravityAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var (criterion, s) in FinalQuantumGravityAudit.SubScores())
            sb.AppendLine($"  {criterion}: {s:F1}/1.0");
        sb.AppendLine($"  TOTAL = {score:F1}/6");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("PROGRESSION:");
        foreach (var p in FinalQuantumGravityAudit.Progression())
            sb.AppendLine($"  {p.Phase}: {p.Status} ({p.Score:F1}/6)");

        sb.AppendLine();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG215 PARTIAL QG (2/6) → QG219 EFFECTIVE QG (4/6) → QG221 NEAR-COMPLETE QG (5/6)");
        sb.AppendLine("    → QG223 COMPLETE QG (6/6).");
        sb.AppendLine("  - All six criteria fully hold: QM derived, gravity derived, common primitive, spacetime");
        sb.AppendLine("    emergent, matter emergent, no blockers. The derived program is complete within its");
        sb.AppendLine("    stated primitives (Q-events → ρ and ψ).");
        sb.AppendLine("  - ψ is an explicit ontological boundary (the second of exactly two primitives), not a");
        sb.AppendLine("    blocker — its capacity is forced (QG56) and its excitation mechanism is derived (QG57).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("COMPLETE QG", classification);
        Assert.Equal(6.0, score, 6);
    }
}
