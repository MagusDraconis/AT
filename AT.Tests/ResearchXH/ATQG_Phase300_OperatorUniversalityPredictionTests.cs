using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 300 — Operator Universality Prediction. Search observables NOT used during QG0-QG299
/// and determine whether they also reduce to MOMENT / COMPRESSION / BEAT / LOCKING. No observables,
/// no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase300_OperatorUniversalityPredictionTests : ResearchTestBase
{
    public ATQG_Phase300_OperatorUniversalityPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3000_PrecisionEwReduces()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3000: the precision-EW observables reduce to the operator basis");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the precision-EW observables (ΓZ, ΓW, ΓH, R_b, A_FB^b, A_FB^ℓ) were NOT in the");
        sb.AppendLine("    QG262 map, yet they reduce to MOMENT/COMPRESSION/BEAT/LOCKING.");
        sb.AppendLine();

        sb.AppendLine("PRECISION EW (QG175):");
        foreach (var o in OperatorUniversalityPrediction.NewObservables().Where(o => o.Phase == "QG175"))
        {
            sb.AppendLine($"  {o.Name}: {o.Formula} → [{o.OperatorsUsed}]  reduces={o.ReducesToBasis}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorUniversalityPrediction.NewObservables()
                .Where(o => o.Phase == "QG175").All(o => o.ReducesToBasis),
            "the precision-EW observables must reduce to the operator basis");
    }

    [Fact]
    public void ATQG3001_RunningPredictionsNewtonReduce()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3001: running couplings, predictions, and Newton constant reduce");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the running couplings (α_em, α_W, α_s), quark running, P1/P2/P3, and the");
        sb.AppendLine("    Newton constant were NOT in the QG262 map, yet they reduce to the basis.");
        sb.AppendLine();

        sb.AppendLine("RUNNING COUPLINGS (QG204/224):");
        foreach (var o in OperatorUniversalityPrediction.NewObservables().Where(o => o.Phase is "QG204" or "QG224"))
            sb.AppendLine($"  {o.Name}: {o.Formula} → [{o.OperatorsUsed}]  reduces={o.ReducesToBasis}");
        sb.AppendLine("PREDICTIONS + NEWTON (QG181/190-192):");
        foreach (var o in OperatorUniversalityPrediction.NewObservables().Where(o => o.Phase is "QG181" or "QG190" or "QG191" or "QG192"))
            sb.AppendLine($"  {o.Name}: {o.Formula} → [{o.OperatorsUsed}]  reduces={o.ReducesToBasis}");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorUniversalityPrediction.NewObservables()
                .Where(o => o.Phase is "QG204" or "QG224").All(o => o.ReducesToBasis),
            "the running couplings must reduce to the basis");
        Assert.True(OperatorUniversalityPrediction.NewObservables()
                .Where(o => o.Phase is "QG181" or "QG190" or "QG191" or "QG192").All(o => o.ReducesToBasis),
            "the predictions and Newton constant must reduce to the basis");
    }

    [Fact]
    public void ATQG3002_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3002: the operator universality determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - UNIVERSAL: every new observable reduces to the four-operator basis;");
        sb.AppendLine("  - the only non-reducible new observable is the documented Bekenstein 1/4 boundary.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OperatorUniversalityPrediction.Summary()}");
        sb.AppendLine($"Universality score: {OperatorUniversalityPrediction.UniversalityScore()}/5");
        sb.AppendLine($"reducible={OperatorUniversalityPrediction.ReducibleCount()} non-reducible={OperatorUniversalityPrediction.NonReducibleCount()}");
        sb.AppendLine($"all new reduce: {OperatorUniversalityPrediction.AllNewReduce()}");
        sb.AppendLine($"CLASSIFICATION = {OperatorUniversalityPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("OPERATOR BASIS:");
        foreach (var b in OperatorUniversalityPrediction.OperatorBasis())
            sb.AppendLine($"  - {b}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the operator universality is a PREDICTION: observables NOT used during");
        sb.AppendLine("    QG0-QG299 — precision-EW widths/asymmetries, running couplings, quark running,");
        sb.AppendLine("    P1/P2/P3, Newton constant — ALL reduce to MOMENT/COMPRESSION/BEAT/LOCKING;");
        sb.AppendLine("  - the only non-reducible new observable is the documented Bekenstein 1/4");
        sb.AppendLine("    boundary (needs the imported 2π quantum factor, QG185/259);");
        sb.AppendLine("  - the four operators are UNIVERSAL across the observable sector.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL", OperatorUniversalityPrediction.Classify());
        Assert.True(OperatorUniversalityPrediction.UniversalityScore() >= 5);
        Assert.True(OperatorUniversalityPrediction.AllNewReduce());
        Assert.Contains("UNIVERSAL", OperatorUniversalityPrediction.Summary());
    }
}
