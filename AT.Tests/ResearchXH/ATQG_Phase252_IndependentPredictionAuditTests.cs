using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 252 — Independent Prediction Audit. Measure how much of AT's validation comes from
/// genuine prediction vs reconstruction.
/// </summary>
public class ATQG_Phase252_IndependentPredictionAuditTests : ResearchTestBase
{
    public ATQG_Phase252_IndependentPredictionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2520_Inventory()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2520: the validation inventory (QG176/177/190-193/199-202/240)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - POSTDICTION = target known when the formula was built and compared;");
        sb.AppendLine("  - BLIND RECONSTRUCTION = target hidden from the derivation machinery (methodological);");
        sb.AppendLine("  - PRE-REGISTERED = frozen before measurement (temporal);");
        sb.AppendLine("  - EXTERNAL SUPPORT = an independent experiment matched a frozen value.");
        sb.AppendLine();

        sb.AppendLine("THE INVENTORY:");
        foreach (var r in IndependentPredictionAudit.Results())
        {
            sb.AppendLine($"  [{r.Category,-23}] {r.Phase} · {r.Name} ({r.Units} units)");
            sb.AppendLine($"      {r.Note}");
        }
        sb.AppendLine();
        sb.AppendLine($"By category (units): {string.Join(", ", IndependentPredictionAudit.UnitCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"Total units: {IndependentPredictionAudit.TotalUnits()}");

        Output.WriteLine(sb.ToString());

        var u = IndependentPredictionAudit.UnitCounts();
        Assert.Equal(35, u[IndependentPredictionAudit.Category.Postdiction]);
        Assert.Equal(21, u[IndependentPredictionAudit.Category.BlindReconstruction]);
        Assert.Equal(3, u[IndependentPredictionAudit.Category.PreRegisteredPrediction]);
        Assert.Equal(1, u[IndependentPredictionAudit.Category.ExternalSupport]);
        Assert.Equal(60, IndependentPredictionAudit.TotalUnits());
    }

    [Fact]
    public void ATQG2521_EvidenceFractions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2521: the independent-evidence fractions");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Methodological independence = the derivation machinery never sees the target;");
        sb.AppendLine("  - Temporal independence (strictest) = the target did not exist at derivation time.");
        sb.AppendLine();

        sb.AppendLine($"Methodological independence (blind + pre-registered + external):");
        sb.AppendLine($"  {IndependentPredictionAudit.MethodologicalFraction():P1} of {IndependentPredictionAudit.TotalUnits()} units");
        sb.AppendLine($"Temporal independence (pre-registered + external):");
        sb.AppendLine($"  {IndependentPredictionAudit.TemporalFraction():P1} of {IndependentPredictionAudit.TotalUnits()} units");
        sb.AppendLine($"F2 fully mitigated (≥ 50% methodological)? {IndependentPredictionAudit.F2FullyMitigated()}");

        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(IndependentPredictionAudit.MethodologicalFraction() - 25.0 / 60.0) < 1e-9);
        Assert.True(Math.Abs(IndependentPredictionAudit.TemporalFraction() - 4.0 / 60.0) < 1e-9);
        Assert.False(IndependentPredictionAudit.F2FullyMitigated(), "58% of units remain postdictions");
    }

    [Fact]
    public void ATQG2522_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2522: the independent-evidence strength");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - LOW < 20%, MEDIUM 20-60%, HIGH > 60% (methodological criterion).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {IndependentPredictionAudit.Summary()}");
        sb.AppendLine($"CLASSIFICATION = {IndependentPredictionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - 42% of validation units are produced with the target hidden from the derivation");
        sb.AppendLine("    machinery (methodological blindness); the temporally-predictive core (pre-registered");
        sb.AppendLine("    + externally supported) is 6.7% of units.");
        sb.AppendLine("  - The bulk of numerical validation (58%) is POSTDICTION against known targets.");
        sb.AppendLine("  - QG250's F2 claim is only PARTIALLY mitigated: the genuinely temporal prediction");
        sb.AppendLine("    content is small but nonzero and externally supported (P3, 2.80σ).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MEDIUM", IndependentPredictionAudit.Classify());
        Assert.Contains("MEDIUM", IndependentPredictionAudit.Summary());
    }
}
