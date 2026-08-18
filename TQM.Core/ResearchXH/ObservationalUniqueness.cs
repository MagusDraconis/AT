namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 43 — observational uniqueness of ψ. Which observations require the TENSOR ψ and which can be
/// reproduced by a SCALAR (non-conformal) ψ? Per QG25, the observable's own spin decides: deflection (lensing),
/// time delay (Shapiro), and the PPN parameter γ are each a single SCALAR quantity (spin 0) — a 1-d.o.f.
/// non-conformal scalar ψ breaks conformal flatness (γ → ≠ −1) and reproduces all of them. Only the GW
/// polarization (h_+, h_×) is intrinsically spin-2 and requires the tensor ψ. Horizon physics is AMBIGUOUS: the
/// shadow and entropy are scalar, but the Hawking temperature was left UNDECIDED (QG25). No new primitives.
/// </summary>
public static class ObservationalUniqueness
{
    /// <summary>The five observables audited for ψ-uniqueness.</summary>
    public static readonly string[] Observables =
    {
        "lensing",
        "gw-polarization",
        "shapiro-delay",
        "ppn-gamma",
        "horizon-physics",
    };

    /// <summary>Classification of each observable.</summary>
    public static string Classify(string observable) => observable switch
    {
        "lensing" => "SCALAR",         // spin-0 deflection; scalar ψ breaks conformal flatness
        "gw-polarization" => "PSI",    // spin-2 (h_+, h_×)
        "shapiro-delay" => "SCALAR",   // spin-0 time delay
        "ppn-gamma" => "SCALAR",       // scalar parameter γ; scalar ψ moves it off −1
        "horizon-physics" => "AMBIGUOUS", // shadow/entropy scalar; Hawking T UNDECIDED (QG25)
        _ => throw new ArgumentOutOfRangeException(nameof(observable))
    };

    /// <summary>Spin of the OBSERVED EFFECT: scalar observables are spin 0; the GW strain is spin 2.</summary>
    public static double Spin(string observable) => observable switch
    {
        "gw-polarization" => 2.0,
        "lensing" => 0.0,
        "shapiro-delay" => 0.0,
        "ppn-gamma" => 0.0,
        "horizon-physics" => 0.0,   // shadow/entropy/temperature are all scalar; the undecided part is not a spin issue
        _ => throw new ArgumentOutOfRangeException(nameof(observable))
    };

    /// <summary>Only the GW polarization genuinely requires the spin-2 tensor ψ.</summary>
    public static bool RequiresTensorPsi(string observable) => Spin(observable) >= 2.0;

    /// <summary>A scalar (non-conformal) ψ suffices for the scalar observables (breaks γ = −1).</summary>
    public static bool ScalarPsiSuffices(string observable) => Spin(observable) == 0.0 && Classify(observable) != "AMBIGUOUS";
}
