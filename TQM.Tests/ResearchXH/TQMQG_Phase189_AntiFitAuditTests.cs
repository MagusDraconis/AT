using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 189 — Anti-Fit Audit. Reviews QG140–QG188 derivation methodology: inputs, hidden targets,
/// free choices, candidate formulas, and whether the known target influenced formula selection. Audit only —
/// no physics derived. Classification: PREDICTION / BLIND RECONSTRUCTION / DEPENDENT DERIVATION / RETRO-FIT RISK /
/// OVERFIT RISK.
/// </summary>
public class TQMQG_Phase189_AntiFitAuditTests : ResearchTestBase
{
    public TQMQG_Phase189_AntiFitAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1890_CompleteRegister()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1890: complete anti-fit register (QG140–QG188)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Audit reads each phase report: inputs, target, free choices, candidate formulas.");
        sb.AppendLine("  - Classification: PREDICTION / BLIND / DEPENDENT / RETRO-FIT / OVERFIT.");
        sb.AppendLine();

        var reg = AntiFitAudit.Register();
        sb.AppendLine("PHASE | TARGET | INPUTS | RISK | LEVEL | REASON");
        foreach (var p in reg)
            sb.AppendLine($"  QG{p.Phase} | {p.Target} | {p.Inputs} | {p.RiskName} | {p.Level} | {p.Reason}");
        sb.AppendLine();
        sb.AppendLine($"  total phases audited: {reg.Length}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(49, reg.Length);  // QG140..QG188
        Assert.True(AntiFitAudit.HighRiskCount() >= 2, "QG140/146/147 should be high-risk (fitting era)");
        Assert.True(AntiFitAudit.BlindCount() == 2, "QG176 and QG177 are the blind reconstructions");
        Assert.Contains(reg, p => p.Phase == 147 && p.Risk == AntiFitAudit.RiskClass.OverfitRisk);
        Assert.Contains(reg, p => p.Phase == 148 && p.Risk == AntiFitAudit.RiskClass.Prediction);
    }

    [Fact]
    public void TQMQG1891_EraSplitAndDistribution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1891: fitting era vs structural era, risk distribution");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG140–148 is the fitting era (amplification laws with fitted exponents).");
        sb.AppendLine("  - QG149+ is the structural era (D96 primitives, no fitted parameters).");
        sb.AppendLine();

        var counts = AntiFitAudit.CountByClass();
        var (fitEra, structEra) = AntiFitAudit.EraSplit();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  total phases: {AntiFitAudit.Register().Length}");
        sb.AppendLine($"  PREDICTION:            {counts.GetValueOrDefault(AntiFitAudit.RiskClass.Prediction)}");
        sb.AppendLine($"  BLIND RECONSTRUCTION:   {counts.GetValueOrDefault(AntiFitAudit.RiskClass.BlindReconstruction)}");
        sb.AppendLine($"  DEPENDENT DERIVATION:   {counts.GetValueOrDefault(AntiFitAudit.RiskClass.DependentDerivation)}");
        sb.AppendLine($"  RETRO-FIT RISK:         {counts.GetValueOrDefault(AntiFitAudit.RiskClass.RetroFitRisk)}");
        sb.AppendLine($"  OVERFIT RISK:           {counts.GetValueOrDefault(AntiFitAudit.RiskClass.OverfitRisk)}");
        sb.AppendLine($"  HIGH-risk phases:       {AntiFitAudit.HighRiskCount()}");
        sb.AppendLine($"  fitting era (≤148):     {fitEra}   structural era (≥149): {structEra}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The only CONFIRMED overfit is QG147, caught by QG148's out-of-sample validation.");
        sb.AppendLine("  - Retro-fit risk is confined to the fitting era (QG140, QG146); those results were");
        sb.AppendLine("    superseded by QG141 (derived exponents) and QG149 (physical occupation-weighted origin).");
        sb.AppendLine("  - The structural era (149+) contains no fitted parameters.");

        Output.WriteLine(sb.ToString());

        Assert.True(AntiFitAudit.PredictionCount() > 25, "most phases are genuine predictions");
        Assert.True(fitEra <= 9, "fitting era is QG140–148 (9 phases)");
        Assert.True(structEra >= 40, "structural era is QG149–188 (40 phases)");
        Assert.True(AntiFitAudit.HighRiskCount() >= 2, "high-risk phases exist in the fitting era");
    }

    [Fact]
    public void TQMQG1892_ConclusionsAndGoldStandard()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1892: audit conclusions and gold-standard blind tests");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The gold standard for anti-fit safety is a blind reconstruction (target hidden).");
        sb.AppendLine();

        sb.AppendLine("AUDIT CONCLUSIONS:");
        foreach (var c in AntiFitAudit.Conclusions())
            sb.AppendLine($"  • {c}");
        sb.AppendLine();
        sb.AppendLine("GOLD-STANDARD BLIND TESTS:");
        sb.AppendLine("  QG176 — Higgs blind reconstruction: MH, ΓH, MH/MW, MH/MZ, λ_H hidden;");
        sb.AppendLine("           rebuilt from pre-Higgs D96 only → 125.49/125.25 GeV.");
        sb.AppendLine("  QG177 — Leave-one-out: 12 observables each hidden and rebuilt from the primitive");
        sb.AppendLine("           base; mean dev 0.58%, max 1.89% → INDEPENDENT.");
        sb.AppendLine();
        sb.AppendLine($"  ⇒ {AntiFitAudit.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PREDICTION AUDIT", AntiFitAudit.Classify());
        Assert.Equal(2, AntiFitAudit.BlindCount());
        Assert.Contains(AntiFitAudit.Conclusions(), c => c.Contains("QG147"));
        Assert.Contains(AntiFitAudit.Conclusions(), c => c.Contains("QG176"));
    }
}
