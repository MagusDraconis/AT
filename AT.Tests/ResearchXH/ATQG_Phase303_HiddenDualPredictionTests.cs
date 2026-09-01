using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 303 — Hidden Dual Prediction. Review QG286/301; find scalar results lacking tensor
/// partners and tensor results lacking scalar partners; predict the missing duals. No observables,
/// no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase303_HiddenDualPredictionTests : ResearchTestBase
{
    public ATQG_Phase303_HiddenDualPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3030_ScalarHiddenDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3030: scalar results lacking tensor partners → predicted tensor duals");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the masses/couplings had only WEAK tensor duals in QG301;");
        sb.AppendLine("  - their true tensor face is the rank-2 tensor whose trace they read.");
        sb.AppendLine();

        sb.AppendLine("SCALAR → HIDDEN TENSOR DUAL:");
        foreach (var d in HiddenDualPrediction.ScalarHiddenDuals())
        {
            sb.AppendLine($"  {d.ScalarResult} → {d.HiddenTensorDual}");
            sb.AppendLine($"      {d.Decomposition}");
            sb.AppendLine($"      prediction: {d.Prediction}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(HiddenDualPrediction.ScalarHiddenCount() >= 3,
            "at least 3 scalar→tensor hidden duals must be predicted");
        Assert.True(HiddenDualPrediction.TraceTracelessAppliesToRank2(),
            "the trace/traceless decomposition must apply to every rank-2 object");
    }

    [Fact]
    public void ATQG3031_TensorHiddenDuals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3031: tensor results lacking scalar partners → predicted scalar duals");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - S ∝ A, κ, and the Weyl tensor had weak/no scalar duals in QG301;");
        sb.AppendLine("  - their hidden scalar face is the trace/count they carry.");
        sb.AppendLine();

        sb.AppendLine("TENSOR → HIDDEN SCALAR DUAL:");
        foreach (var d in HiddenDualPrediction.TensorHiddenDuals())
        {
            sb.AppendLine($"  {d.TensorResult} → {d.HiddenScalarDual}");
            sb.AppendLine($"      {d.Decomposition}");
            sb.AppendLine($"      prediction: {d.Prediction}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(HiddenDualPrediction.TensorHiddenCount() >= 3,
            "at least 3 tensor→scalar hidden duals must be predicted");
    }

    [Fact]
    public void ATQG3032_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3032: the hidden-dual determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - NEW DUALS: the QG301 weak duals are completed by predicted hidden duals;");
        sb.AppendLine("  - the {ρ, ψ} duality extends to every rank-2 physical object.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {HiddenDualPrediction.Summary()}");
        sb.AppendLine($"Prediction score: {HiddenDualPrediction.PredictionScore()}/5");
        sb.AppendLine($"scalar→tensor: {HiddenDualPrediction.ScalarHiddenCount()}  tensor→scalar: {HiddenDualPrediction.TensorHiddenCount()}  total: {HiddenDualPrediction.TotalHiddenDuals()}");
        sb.AppendLine($"all weak duals completed: {HiddenDualPrediction.AllWeakDualsCompleted()}");
        sb.AppendLine($"CLASSIFICATION = {HiddenDualPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the QG301 weak duals are completed by predicted hidden duals:");
        sb.AppendLine("    · masses → T_μν (m = Tr(T_μν)/d — the mass is the trace);");
        sb.AppendLine("    · couplings → F_μν (α = the contraction strength of the interaction tensor);");
        sb.AppendLine("    · S ∝ A → N_def (A/cell — the count face of the area);");
        sb.AppendLine("    · κ → M_Pl (the scalar read of the same spectral constants);");
        sb.AppendLine("    · Weyl → Ricci trace R;");
        sb.AppendLine("  - the {ρ, ψ} decomposition extends to EVERY rank-2 physical object via the");
        sb.AppendLine("    trace/traceless decomposition — the duality is complete.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW DUALS", HiddenDualPrediction.Classify());
        Assert.True(HiddenDualPrediction.PredictionScore() >= 3);
        Assert.True(HiddenDualPrediction.AllWeakDualsCompleted());
        Assert.Contains("NEW DUALS", HiddenDualPrediction.Summary());
    }
}
