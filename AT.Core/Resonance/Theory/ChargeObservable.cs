namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for physical observables derived from topological charge Q.
///
/// AT-145: Physical Observables from Topological Charge
/// </summary>
public static class ChargeObservable
{
    /// <summary>
    /// A physical observable computed from a Q interaction graph.
    /// </summary>
    public sealed record PhysicalObservable(
        string Name,                        // "Effective Mass", "Spectral Gap", etc.
        double[] QValues,                   // Q values tested
        double[] ObservableValues,          // computed values
        string ScalingType,                 // "Power-Law", "Linear", "Logarithmic", "Constant"
        double ScalingExponent,             // b in O ∝ Q^b
        double R2,                          // fit quality
        bool IsUniversal,                   // consistent across geometries?
        string PhysicalInterpretation);     // "m_eff ∝ 1/λ_1 ∝ Q² in 1D"

    /// <summary>
    /// A scaling law fit: O(Q) = a·Q^b.
    /// </summary>
    public sealed record ScalingLaw(
        string ObservableName,
        double Prefactor,                   // a
        double Exponent,                    // b
        double R2,
        string Law);                        // "O = a·Q^b"

    /// <summary>
    /// Complete physical observables report.
    /// </summary>
    public sealed record ObservableReport(
        List<PhysicalObservable> Observables,
        List<ScalingLaw> ScalingLaws,
        int GeometriesTested,
        int ObservablesFound,               // count of observables with R² > 0.8
        int UniversalObservables,           // count consistent across geometries
        double MeanR2,
        bool DirectObservablesExist,        // can Q directly predict any observable?
        bool UniversalScalingFound,         // is there a geometry-independent scaling law?
        string Classification,              // "A: Purely Topological" ... "D: Direct Physical Observable Theory"
        string Verdict);
}
