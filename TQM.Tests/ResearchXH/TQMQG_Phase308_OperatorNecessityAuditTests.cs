using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 308 — Operator Necessity Audit. Why exactly these four operators? Can any be derived
/// from the others? Remove each and determine INDISPENSABLE / DERIVABLE / REDUNDANT. No observables,
/// no target values, D96 only, deterministic.
/// </summary>
public class TQMQG_Phase308_OperatorNecessityAuditTests : ResearchTestBase
{
    public TQMQG_Phase308_OperatorNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3080_GroupingsDiffer()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3080: CROWDING and COMPRESSION are different groupings of the same 95 modes");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - CROWDING reads the multiplicity multiset [42×2, 5, 6];");
        sb.AppendLine("  - COMPRESSION reads the octave occupancies [4,4,87];");
        sb.AppendLine("  - only the trivial first moment Σm = 95 is shared.");
        sb.AppendLine();

        sb.AppendLine($"multiplicity sum: {EffectiveAccessCounts.DoubletMultiplicities().Sum()}");
        sb.AppendLine($"occupancy sum: {EffectiveAccessCounts.OctaveOccupancies().Sum()}");
        sb.AppendLine($"multiplicity groups: {EffectiveAccessCounts.DoubletMultiplicities().Length} vs octave bands: {EffectiveAccessCounts.OctaveOccupancies().Length}");
        sb.AppendLine($"groupings differ: {OperatorNecessityAudit.GroupingsDiffer()}");
        sb.AppendLine($"only the first moment is shared: {OperatorNecessityAudit.OnlyFirstMomentShared()}");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorNecessityAudit.GroupingsDiffer(),
            "CROWDING and COMPRESSION must be different groupings of the same modes");
        Assert.True(OperatorNecessityAudit.OnlyFirstMomentShared(),
            "only the trivial first moment must be shared");
    }

    [Fact]
    public void TQMQG3081_NoOperatorDerivable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3081: no operator's outputs are derivable from the others");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - occMom ≠ any multiplicity-moment combination;");
        sb.AppendLine("  - span ≠ any grouping statistic; λ₂ ≠ any moment ratio.");
        sb.AppendLine();

        sb.AppendLine($"occMom not derivable from crowding moments: {OperatorNecessityAudit.OccMomNotDerivableFromCrowding()}");
        sb.AppendLine($"span not derivable from the grouping: {OperatorNecessityAudit.SpanNotDerivableFromGrouping()}");
        sb.AppendLine($"λ₂ not derivable from the moments: {OperatorNecessityAudit.LockingNotDerivable()}");
        sb.AppendLine();
        sb.AppendLine("REMOVAL TESTS:");
        foreach (var o in OperatorNecessityAudit.Operators())
        {
            sb.AppendLine($"  {o.Name} — reads {o.Reads}");
            sb.AppendLine($"      outputs lost: {o.OutputsLost}; {o.ReconstructionCheck}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorNecessityAudit.OccMomNotDerivableFromCrowding(),
            "occMom must not be derivable from the CROWDING moments");
        Assert.True(OperatorNecessityAudit.SpanNotDerivableFromGrouping(),
            "span must not be derivable from the grouping");
        Assert.True(OperatorNecessityAudit.LockingNotDerivable(),
            "λ₂ must not be derivable from the moments");
    }

    [Fact]
    public void TQMQG3082_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3082: the operator-necessity determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - INEVITABLE FOUR: all four operators are mutually independent and indispensable;");
        sb.AppendLine("  - they are exactly the four independent spectral projections.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OperatorNecessityAudit.Summary()}");
        sb.AppendLine($"Necessity score: {OperatorNecessityAudit.NecessityScore()}/5");
        sb.AppendLine($"indispensable={OperatorNecessityAudit.IndispensableCount()} derivable={OperatorNecessityAudit.DerivableCount()} redundant={OperatorNecessityAudit.RedundantCount()}");
        sb.AppendLine($"all four indispensable: {OperatorNecessityAudit.AllFourIndispensable()}");
        sb.AppendLine($"CLASSIFICATION = {OperatorNecessityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("MINIMUM OPERATOR BASIS:");
        foreach (var b in OperatorNecessityAudit.MinimumBasis())
            sb.AppendLine($"  - {b}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - CROWDING reads the degeneracy grouping [multiplicities 42×2,5,6];");
        sb.AppendLine("  - COMPRESSION reads the octave grouping [occupancies 4,4,87];");
        sb.AppendLine("  - BEAT reads the extent [span = 6.4025];");
        sb.AppendLine("  - LOCKING reads the gap [λ₂ = 0.3864];");
        sb.AppendLine("  - no operator's outputs can be reconstructed from the others — each is");
        sb.AppendLine("    INDISPENSABLE. The four-operator basis is the MINIMUM and INEVITABLE basis:");
        sb.AppendLine("    the four independent spectral projections any spectrum carries, read by the");
        sb.AppendLine("    universal MOMENT functional.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INEVITABLE FOUR", OperatorNecessityAudit.Classify());
        Assert.True(OperatorNecessityAudit.NecessityScore() >= 5);
        Assert.True(OperatorNecessityAudit.AllFourIndispensable());
        Assert.Contains("INEVITABLE FOUR", OperatorNecessityAudit.Summary());
    }
}
