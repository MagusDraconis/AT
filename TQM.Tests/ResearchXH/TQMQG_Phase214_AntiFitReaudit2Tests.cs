using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 214 — Anti-Fit Reaudit 2. Review QG140–QG213, classify each by methodology, compare against
/// QG189, and verify whether RETRO-FIT = 2 and OVERFIT = 1 still hold. Methodology audit only, no physics.
/// </summary>
public class TQMQG_Phase214_AntiFitReaudit2Tests : ResearchTestBase
{
    public TQMQG_Phase214_AntiFitReaudit2Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2140_NewPhasesClassified()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2140: the QG190–QG213 register");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Methodology audit only; classification by target visibility, fitted parameters,");
        sb.AppendLine("    hidden targets, formula selection, pre-registration, registry lock.");
        sb.AppendLine();

        var phases = AntiFitReaudit2.NewPhases();
        sb.AppendLine($"NEW PHASES (QG190–QG213, {phases.Length}):");
        foreach (var p in phases)
            sb.AppendLine($"  QG{p.Phase,3}  {p.Classification,-26} {p.RiskName,-22} {p.Check.Substring(0, Math.Min(60, p.Check.Length))}");
        sb.AppendLine();

        var counts = AntiFitReaudit2.NewCounts();
        sb.AppendLine("NEW-PHASE COUNTS:");
        foreach (var kv in counts.OrderBy(kv => kv.Key.ToString()))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG190-213: 3 PRE-REGISTERED, 1 REGISTRY LOCK, 20 PREDICTION (derivations + audits).");
        sb.AppendLine("  - Zero retro-fit, zero overfit, zero fitted parameters in the new phases.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(24, phases.Length);
        Assert.True(AntiFitReaudit2.NewPhasesFitFree(), "QG190-213 must contain no retro-fit or overfit risk");
        Assert.True(AntiFitReaudit2.NewPhasesHaveNoFittedParameters(), "QG190-213 must have no fitted parameters");
    }

    [Fact]
    public void TQMQG2141_RetroFitAndOverfitStillHold()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2141: do RETRO-FIT = 2 and OVERFIT = 1 still hold?");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG189 baseline: 36 PREDICTION, 2 BLIND, 8 DEPENDENT, 2 RETRO-FIT, 1 OVERFIT.");
        sb.AppendLine("  - The new phases QG190-213 must not add risk cases.");
        sb.AppendLine();

        var total = AntiFitReaudit2.TotalCounts();
        bool retro2 = AntiFitReaudit2.RetroFitStillTwo();
        bool over1 = AntiFitReaudit2.OverfitStillOne();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var kv in total.OrderBy(kv => kv.Key.ToString()))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");
        sb.AppendLine($"  RETRO-FIT = 2 still holds? {retro2}");
        sb.AppendLine($"  OVERFIT = 1 still holds? {over1}");
        sb.AppendLine($"  New phases fit-free? {AntiFitReaudit2.NewPhasesFitFree()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - RETRO-FIT = 2 (QG140, QG146) and OVERFIT = 1 (QG147) remain correct.");
        sb.AppendLine("  - The only overfit was caught by QG148; the only retro-fits were superseded by QG141/149.");
        sb.AppendLine("  - QG149-213 (structural era) contain no risk cases.");

        Output.WriteLine(sb.ToString());

        Assert.True(retro2, "RETRO-FIT must remain 2");
        Assert.True(over1, "OVERFIT must remain 1");
        Assert.True(AntiFitReaudit2.NewPhasesFitFree(), "new phases must be fit-free");
    }

    [Fact]
    public void TQMQG2142_StrongestEvidenceAndRiskTrend()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2142: strongest anti-fit evidence and the risk trend by era");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Strongest evidence: pre-registration (QG190-193) + blind tests (QG176/177).");
        sb.AppendLine();

        sb.AppendLine("STRONGEST ANTI-FIT EVIDENCE:");
        sb.AppendLine($"  {AntiFitReaudit2.StrongestAntiFitEvidence()}");
        sb.AppendLine();

        var (fitting, structural) = AntiFitReaudit2.RiskTrend();
        sb.AppendLine("RISK TREND BY ERA:");
        sb.AppendLine($"  Fitting era QG140-148: 2 retro-fit + 1 overfit (all risk cases)");
        sb.AppendLine($"  Structural era QG149-213: 0 risk cases");
        sb.AppendLine($"  (risk-case distribution: {fitting} vs {structural})");
        sb.AppendLine();

        sb.AppendLine("CATEGORY CHANGES vs QG189:");
        sb.AppendLine("  - Added PRE-REGISTERED (3) and REGISTRY LOCK (1) categories for QG190-193.");
        sb.AppendLine("  - 20 PREDICTION phases added (derivations QG194-197,203-210,212; audits QG198-202,205,211,213).");
        sb.AppendLine("  - No BLIND / DEPENDENT / RETRO-FIT / OVERFIT added.");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The strongest anti-fit evidence is the pre-registration program (QG190-193):");
        sb.AppendLine("    the predictions were frozen before data, with forbidden-input guards and an");
        sb.AppendLine("    immutable registry lock.");
        sb.AppendLine("  - Risk is confined to the fitting era (QG140-148); the structural era is fit-free.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, fitting);
        Assert.Equal(0, structural);
    }
}
