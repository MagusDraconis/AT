using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 73 — measurement from actualization. Determines whether measurement = Q-event actualization.
/// Classify: MATCH / PARTIAL MATCH / NO MATCH.
///
/// Tests: ATQG730 (Born-weighted projection), ATQG731 (binary limitation), ATQG732 (classification).
/// </summary>
public class ATQG_Phase73_MeasurementFromActualizationTests : ResearchTestBase
{
    public ATQG_Phase73_MeasurementFromActualizationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG730: actualization is a Born-weighted projection ───────────────────────

    [Fact]
    public void ATQG730_BornWeightedProjection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG730: a Q-event is a discrete, Born-weighted projection");

        bool projection = MeasurementFromActualization.ActualizationIsProjection();
        bool bornWeighted = MeasurementFromActualization.ActualizationBornWeighted();
        bool beyondDecoherence = MeasurementFromActualization.ActualizationBeyondDecoherence();

        sb.AppendLine($"a Q-event is a PROJECTION (collapse to a definite state): {projection}");
        sb.AppendLine($"actualization is BORN-WEIGHTED (P = |amplitude|²):        {bornWeighted}");
        sb.AppendLine($"actualization goes BEYOND decoherence (the collapse):    {beyondDecoherence}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: actualization IS the measurement collapse — a discrete, Born-weighted projection onto a definite");
        sb.AppendLine("outcome, beyond the unitary (no-collapse) process of decoherence.");
        Output.WriteLine(sb.ToString());

        Assert.True(projection, "actualization should be a projection");
        Assert.True(bornWeighted, "actualization should be Born-weighted");
        Assert.True(beyondDecoherence, "actualization should go beyond decoherence");
    }

    // ── ATQG731: the binary limitation ─────────────────────────────────────────────

    [Fact]
    public void ATQG731_BinaryLimitation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG731: the projection is binary (tick/no-tick), not general");

        bool binary = MeasurementFromActualization.ProjectionIsBinary();

        sb.AppendLine($"the actualization projection is BINARY (tick/no-tick): {binary}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: actualization projects onto a specific (binary) basis — whether a node ticks or not. It is a");
        sb.AppendLine("collapse, but not a GENERAL quantum measurement (arbitrary observable basis).");
        Output.WriteLine(sb.ToString());

        Assert.True(binary, "the projection should be binary");
    }

    // ── ATQG732: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG732_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG732: MATCH / PARTIAL MATCH / NO MATCH?");

        sb.AppendLine($"CLASSIFICATION: {MeasurementFromActualization.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • MATCH (the collapse): a Q-event IS a Born-weighted projection — the measurement collapse is identified");
        sb.AppendLine("    with actualization, resolving the missing piece of QG72.");
        sb.AppendLine("  • PARTIAL (the basis): the projection is binary (tick/no-tick), not a general measurement basis.");
        sb.AppendLine("  • So measurement = actualization is a PARTIAL MATCH: the collapse is recovered, but as a binary projection.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL MATCH", MeasurementFromActualization.Classify());
        Assert.True(MeasurementFromActualization.ActualizationIsProjection());
        Assert.True(MeasurementFromActualization.ProjectionIsBinary());
    }
}
