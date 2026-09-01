using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 215 — Quantum Gravity Closure Audit. Determine whether AT constitutes a complete quantum
/// gravity theory. Audit only — no new physics. Reviews QM (QG61-74), Gravity (QG0-26/103/181-213), and
/// Foundation (QG1-11/51-59). Deterministic.
/// </summary>
public class ATQG_Phase215_QuantumGravityClosureAuditTests : ResearchTestBase
{
    public ATQG_Phase215_QuantumGravityClosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2150_TheSixCriteria()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2150: the six quantum-gravity completeness criteria");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Audit only; classification from the completed phase record.");
        sb.AppendLine();

        sb.AppendLine("THE SIX CRITERIA:");
        sb.AppendLine($"  1. Is quantum mechanics derived?  {QuantumGravityClosureAudit.IsQuantumMechanicsDerived()}");
        foreach (var s in QuantumGravityClosureAudit.QuantumMechanicsStatus())
            sb.AppendLine($"     {s}");
        sb.AppendLine($"  2. Is gravity derived?  {QuantumGravityClosureAudit.IsGravityDerived()}");
        foreach (var s in QuantumGravityClosureAudit.GravityStatus())
            sb.AppendLine($"     {s}");
        sb.AppendLine($"  3. Both from the same primitive?  {QuantumGravityClosureAudit.SamePrimitiveForBoth()}");
        sb.AppendLine($"  4. Is spacetime emergent?  {QuantumGravityClosureAudit.IsSpacetimeEmergent()}  (metric yes, BDG dynamics imported QG6)");
        sb.AppendLine($"  5. Is matter emergent?  {QuantumGravityClosureAudit.IsMatterEmergent()}");
        sb.AppendLine($"  6. Essential components open?  {QuantumGravityClosureAudit.EssentialComponentsOpen()}");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());

        Assert.False(QuantumGravityClosureAudit.IsQuantumMechanicsDerived(), "QM is not derived (QG62 needs a new primitive)");
        Assert.True(QuantumGravityClosureAudit.IsGravityDerived(), "gravity is derived");
        Assert.False(QuantumGravityClosureAudit.SamePrimitiveForBoth(), "QM and gravity are not from the same primitive");
        Assert.True(QuantumGravityClosureAudit.IsMatterEmergent(), "matter is emergent");
    }

    [Fact]
    public void ATQG2151_ClassificationPartialQg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2151: classification — PARTIAL QG");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score 0-1 NOT QG, 2-3 PARTIAL QG, 4-5 EFFECTIVE QG, 6 COMPLETE QG.");
        sb.AppendLine();

        int score = QuantumGravityClosureAudit.QgScore();
        string classification = QuantumGravityClosureAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  QG score (max 6) = {score}");
        sb.AppendLine($"    +1 QM derived ({QuantumGravityClosureAudit.IsQuantumMechanicsDerived()})");
        sb.AppendLine($"    +1 gravity derived ({QuantumGravityClosureAudit.IsGravityDerived()})");
        sb.AppendLine($"    +1 same primitive ({QuantumGravityClosureAudit.SamePrimitiveForBoth()})");
        sb.AppendLine($"    +1 spacetime emergent ({QuantumGravityClosureAudit.IsSpacetimeEmergent()})");
        sb.AppendLine($"    +1 matter emergent ({QuantumGravityClosureAudit.IsMatterEmergent()})");
        sb.AppendLine($"    +1 no components open ({!QuantumGravityClosureAudit.EssentialComponentsOpen()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Gravity is derived from ρ; matter and spacetime (metric) emerge.");
        sb.AppendLine("  - QM is NOT derived: the amplitude/phase requires a new primitive (QG62).");
        sb.AppendLine("  - The two pillars are not based on the same primitive.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL QG", classification);
        Assert.Equal(2, score);
    }

    [Fact]
    public void ATQG2152_MissingPiecesForPublishablePaper()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2152: required missing pieces for a publishable QG paper");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The audit identifies only what is missing; no new physics is proposed.");
        sb.AppendLine();

        sb.AppendLine("REQUIRED MISSING PIECES:");
        foreach (var m in QuantumGravityClosureAudit.MissingPieces())
            sb.AppendLine($"  {m}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The decisive gap is the QM amplitude/phase origin: QG62 shows it is compatible");
        sb.AppendLine("    but not emergent — it requires a new primitive. Until that is derived (or shown");
        sb.AppendLine("    necessary), AT is a derived-gravity program, not a complete quantum gravity theory.");
        sb.AppendLine("  - The imported BDG dynamics and the binary measurement basis are secondary gaps.");
        sb.AppendLine("  - The Bekenstein 1/4 is a proven boundary (QG196), not a resolvable gap.");

        Output.WriteLine(sb.ToString());

        var missing = QuantumGravityClosureAudit.MissingPieces();
        Assert.Equal(5, missing.Length);
        Assert.Contains("amplitude", missing[0], StringComparison.OrdinalIgnoreCase);
    }
}
