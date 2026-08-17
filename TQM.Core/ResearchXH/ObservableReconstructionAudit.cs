namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 25 — observable reconstruction audit. The prior "failures" (lensing, horizon thermodynamics, GW
/// detector outputs) were all identified using GR's observable mappings. Here we separate the OBSERVED EFFECT
/// (what is literally measured — its spin) from the GR EXPLANATION (spin-2 metric perturbation). A scalar
/// observable (deflection angle, time delay, magnification, shadow size, temperature) only needs a non-conformal
/// metric, which a scalar ψ can supply; only the GW strain is intrinsically spin-2. No new primitives.
/// </summary>
public static class ObservableReconstructionAudit
{
    /// <summary>The observables under audit (the five scalar observables + the GW strain).</summary>
    public static readonly string[] Observables =
    {
        "lensing-deflection",
        "time-delay",
        "magnification",
        "horizon-shadow",
        "hawking-temperature",
        "gw-strain",
    };

    /// <summary>
    /// Spin of the OBSERVED EFFECT itself: deflection/time-delay/magnification/shadow/temperature are each a single
    /// scalar (spin 0); the GW strain is a quadrupole with two helicities (spin 2). This is what the detector
    /// literally measures, independent of any gravity theory.
    /// </summary>
    public static double ObservedEffectSpin(string observable) => observable switch
    {
        "lensing-deflection" => 0.0,   // a single deflection angle
        "time-delay" => 0.0,           // a single time shift
        "magnification" => 0.0,        // a single magnification factor
        "horizon-shadow" => 0.0,       // a single angular size
        "hawking-temperature" => 0.0,  // a single temperature
        "gw-strain" => 2.0,            // h_+ and h_x, two helicities (quadrupole)
        _ => throw new ArgumentOutOfRangeException(nameof(observable))
    };

    /// <summary>A tensor (spin-2) is REQUIRED only if the observed effect itself is spin-2.</summary>
    public static bool RequiresTensor(string observable) => ObservedEffectSpin(observable) >= 2.0;

    /// <summary>An observable is scalar-capable iff its observed effect is spin 0 (a scalar ψ suffices).</summary>
    public static bool ScalarCapable(string observable) => ObservedEffectSpin(observable) == 0.0;

    /// <summary>
    /// Classification. GW strain is TENSOR REQUIRED (its polarization content is spin-2). Hawking temperature is
    /// UNDECIDED (scalar-tensor theories DO recover T ∝ 1/M, but TQM's ψ-extension horizon thermodynamics has not
    /// yet been re-derived). All other scalar observables are OBSERVABLE AMBIGUITY (the GR tensor mapping is one
    /// explanation, but a scalar non-conformal metric also reproduces them).
    /// </summary>
    public static string Classify(string observable) => observable switch
    {
        "gw-strain" => "TENSOR REQUIRED",
        "hawking-temperature" => "UNDECIDED",
        _ => "OBSERVABLE AMBIGUITY",
    };

    /// <summary>Minimal d.o.f. for the scalar-capable observables (lensing + shadow): one scalar ψ.</summary>
    public static double ScalarCapableMinimalDof() => 1.0;

    /// <summary>Minimal d.o.f. for the full set (including the GW strain): the 2 graviton polarizations.</summary>
    public static double FullSetMinimalDof() => 2.0;
}
