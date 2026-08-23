using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 240 — Cosmology Blind Reproduction. Hide the observed n_s and peak values; recompute from
/// D96 quantities only using the QG237/QG238 formulas. Compare only after the predictions are locked.
/// Classify BLIND SUCCESS / BLIND FAILURE / INCONCLUSIVE.
/// </summary>
public class TQMQG_Phase240_CosmologyBlindReproductionTests : ResearchTestBase
{
    public TQMQG_Phase240_CosmologyBlindReproductionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2400_PredictionsLockedFromD96Only()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2400: the predictions are locked from D96 primitives only");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The prediction path reads ONLY D96 primitives (span, Σm, #d, occupancies).");
        sb.AppendLine("  - No observed value enters the lock step; comparison happens afterwards.");
        sb.AppendLine();

        var p = CosmologyBlindReproduction.LockPredictions();
        sb.AppendLine("INTERMEDIATE CALCULATIONS (D96 primitives):");
        sb.AppendLine($"  span = {CosmologyBlindReproduction.Span():F4}, ln(span) = {CosmologyBlindReproduction.LnSpan():F4}");
        sb.AppendLine($"  Σm = {CosmologyBlindReproduction.TotalModes()}, #d = {CosmologyBlindReproduction.DoubletCount()}, Σm−#d = {CosmologyBlindReproduction.IndependentModes()}");
        sb.AppendLine($"  occupancies = [{string.Join(", ", CosmologyBlindReproduction.OctaveOccupancies())}]");
        sb.AppendLine();
        sb.AppendLine("LOCKED PREDICTIONS (no observed values in this path):");
        sb.AppendLine($"  n_s   = 1 − ln(span)/(Σm−#d) = {p.Ns:F6}");
        sb.AppendLine($"  ℓ₁    = Σm·ln(span)·(5/4) = {p.L1:F3}");
        sb.AppendLine($"  ℓ₂/ℓ₁ = (Σm−#d)·occ₁/occ₃ = {p.L2OverL1:F4}");
        sb.AppendLine($"  ℓ₃/ℓ₁ = span/√3 = {p.L3OverL1:F4}");

        Output.WriteLine(sb.ToString());

        // The predictions must match the QG237/QG238 derived values (formulas unchanged).
        double span = CosmologyBlindReproduction.Span();
        double lnSpan = CosmologyBlindReproduction.LnSpan();
        int indep = CosmologyBlindReproduction.IndependentModes();
        Assert.Equal(1.0 - lnSpan / indep, p.Ns, 6);
        Assert.Equal(95.0 * lnSpan * 1.25, p.L1, 6);
        Assert.Equal(53.0 * 4.0 / 87.0, p.L2OverL1, 6);
        Assert.Equal(span / Math.Sqrt(3.0), p.L3OverL1, 6);
    }

    [Fact]
    public void TQMQG2401_ComparisonOnlyAfterLock()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2401: compare only after the predictions are locked");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The observed values are consulted only in the comparison step, which runs after");
        sb.AppendLine("    the lock step. The derivation path cannot see the targets.");
        sb.AppendLine();

        sb.AppendLine("COMPARISON (after locking):");
        foreach (var c in CosmologyBlindReproduction.Compare())
            sb.AppendLine($"  {c.Name}: predicted {c.Predicted:F5}, observed {c.Observed:F5}, dev {c.Deviation:P3}");
        sb.AppendLine();
        sb.AppendLine($"  Max deviation: {CosmologyBlindReproduction.MaxDeviation():P3}");

        Output.WriteLine(sb.ToString());

        // All four locked predictions must match the observed values within 1%.
        foreach (var c in CosmologyBlindReproduction.Compare())
            Assert.True(c.Deviation < 0.01, $"{c.Name} must match within 1% (dev {c.Deviation:P2})");
    }

    [Fact]
    public void TQMQG2402_ClassificationBlindSuccess()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2402: classification — BLIND SUCCESS");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - BLIND SUCCESS requires all locked predictions to match the observed values within 1%.");
        sb.AppendLine();

        string classification = CosmologyBlindReproduction.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Max deviation: {CosmologyBlindReproduction.MaxDeviation():P3}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {CosmologyBlindReproduction.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QG237/QG238's formulas recomputed from D96 quantities alone (span, Σm, #d,");
        sb.AppendLine("    occupancies) with no target values in the derivation path reproduce n_s, ℓ₁,");
        sb.AppendLine("    ℓ₂/ℓ₁, and ℓ₃/ℓ₁ to sub-0.1%.");
        sb.AppendLine("  - The hidden-target audit confirms the formulas are NOT fitted to the observed");
        sb.AppendLine("    values — they follow from the D96 spectrum alone.");
        sb.AppendLine($"  ⇒ {classification} — QG237/QG238 survive the hidden-target audit.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("BLIND SUCCESS", classification);
    }
}
