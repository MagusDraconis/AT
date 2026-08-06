namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for measurement origin analysis.
///
/// TQM-154: Origin of Quantum Measurement
/// </summary>
public static class MeasurementChannel
{
    public sealed record DecoherenceTest(
        string Scenario, bool DecoherenceOccurs,
        bool PointerBasisEmerges, bool BornStatsRecovered,
        bool CollapseExplained, string Assessment);

    public sealed record MeasurementOriginReport(
        List<DecoherenceTest> Tests,
        bool DecoherenceExplained, bool CollapseExplained,
        string Classification, string Verdict);
}
