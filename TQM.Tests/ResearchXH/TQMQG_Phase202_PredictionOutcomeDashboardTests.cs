using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 202 — Prediction Outcome Dashboard. Single source of truth for the external validation of
/// P1/P2/P3: frozen value, current evidence, support level, last audit, next experiment, and state
/// (PENDING / SUPPORTED / CONFIRMED / DISFAVORED / FALSIFIED). Deterministic projection of the registry
/// and the evidence audits.
/// </summary>
public class TQMQG_Phase202_PredictionOutcomeDashboardTests : ResearchTestBase
{
    public TQMQG_Phase202_PredictionOutcomeDashboardTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2020_DashboardCompleteForAllPredictions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2020: the complete prediction outcome dashboard");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Inputs: Docs/TQMQG_Predictions.json (immutable registry) and the evidence audits");
        sb.AppendLine("    (QG188A/199/200/201). Deterministic projection, no new physics.");
        sb.AppendLine();

        var all = PredictionOutcomeDashboard.All();
        sb.AppendLine("DASHBOARD (single source of truth):");
        foreach (var o in all)
        {
            sb.AppendLine($"  {o.Id} [{o.State}]  {o.Name}");
            sb.AppendLine($"      frozen value : {o.FrozenValue}");
            sb.AppendLine($"      evidence     : {o.CurrentEvidence}");
            sb.AppendLine($"      support      : {o.SupportLevel}");
            sb.AppendLine($"      last audit   : {o.LastAudit}");
            sb.AppendLine($"      next exp     : {o.NextExperiment}");
        }
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Predictions: {all.Length}");
        sb.AppendLine($"  All frozen values present? {PredictionOutcomeDashboard.AllFrozenValuesPresent()}");
        sb.AppendLine($"  Summary: {PredictionOutcomeDashboard.Summary()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, all.Length);
        Assert.Equal(new[] { "P1", "P2", "P3" }, all.Select(o => o.Id).ToArray());
        Assert.True(PredictionOutcomeDashboard.AllFrozenValuesPresent(), "every frozen value must be present");
    }

    [Fact]
    public void TQMQG2021_StatesAndEvidenceConsistent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2021: states consistent with the evidence audits");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - P1: QG199 PENDING (window open, not excluded).");
        sb.AppendLine("  - P2: no experiment at 2.02 meV → PENDING.");
        sb.AppendLine("  - P3: 151.98 rung = ~152 GeV excess (MODERATE SUPPORT, QG201) → SUPPORTED.");
        sb.AppendLine();

        var counts = PredictionOutcomeDashboard.StateCounts();
        var noneExcluded = PredictionOutcomeDashboard.NoneExcluded();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var kv in counts.OrderBy(kv => kv.Key))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");
        sb.AppendLine($"  None excluded (PENDING or SUPPORTED only)? {noneExcluded}");
        sb.AppendLine($"  P1 state: {PredictionOutcomeDashboard.State("P1")}");
        sb.AppendLine($"  P2 state: {PredictionOutcomeDashboard.State("P2")}");
        sb.AppendLine($"  P3 state: {PredictionOutcomeDashboard.State("P3")}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - P1 and P2 are PENDING (no decisive data).");
        sb.AppendLine("  - P3 is SUPPORTED by the 152 GeV excess alignment (MODERATE SUPPORT).");
        sb.AppendLine("  - No prediction is excluded.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PENDING", PredictionOutcomeDashboard.State("P1"));
        Assert.Equal("PENDING", PredictionOutcomeDashboard.State("P2"));
        Assert.Equal("SUPPORTED", PredictionOutcomeDashboard.State("P3"));
        Assert.True(noneExcluded, "no prediction may be DISFAVORED or FALSIFIED");
    }

    [Fact]
    public void TQMQG2022_RegistryCompliance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2022: registry compliance and forward-only transitions");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG193 registry rule: frozen values immutable; only CONFIRMED / DISFAVORED / FALSIFIED");
        sb.AppendLine("    may be added later; states advance forward only (PENDING → SUPPORTED → CONFIRMED).");
        sb.AppendLine();

        bool valid = PredictionOutcomeDashboard.StateTransitionsValid();
        bool noneExcluded = PredictionOutcomeDashboard.NoneExcluded();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine("  State transitions valid (all in PENDING/SUPPORTED/CONFIRMED/DISFAVORED/FALSIFIED)? " + valid);
        sb.AppendLine($"  None excluded? {noneExcluded}");
        sb.AppendLine($"  Summary: {PredictionOutcomeDashboard.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Every dashboard state is a valid registry state.");
        sb.AppendLine("  - No frozen value is modified; the dashboard is a read-only projection.");
        sb.AppendLine("  - P3 is the only prediction advanced beyond PENDING (SUPPORTED).");

        Output.WriteLine(sb.ToString());

        Assert.True(valid, "all states must be valid registry states");
        Assert.True(noneExcluded, "no prediction may be excluded yet");
    }
}
