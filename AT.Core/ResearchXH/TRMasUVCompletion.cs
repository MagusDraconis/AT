namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 33 — interpret TRM as a UV completion. The TRM kernel is the ψ (non-conformal) sector, which QG32
/// showed is a clean extension (only the Einstein sector changes). Here we test whether TRM is purely a
/// high-density/UV extension of AT, a separate theory, or a partial extension. Using the ψ-perturbation profile
/// ψ = b·x (MetricAnsatzAudit): the g_00 correction is e^{2ψ}, which → 1 in the weak-field limit (x→0) — so TRM
/// reduces to AT in the IR — and departs from 1 in the strong-field (high-density) regime. But the graviton
/// (spin-2) degree of freedom is NOT UV-confined (GWs propagate at all scales), so it is a PARTIAL EXTENSION.
/// No new primitives.
/// </summary>
public static class TRMasUVCompletion
{
    /// <summary>The ψ-perturbation profile ψ = b·x (linear, as in MetricAnsatzAudit.PerturbedG00).</summary>
    public static double Psi(double x, double b = 0.3) => b * x;

    /// <summary>g_00 correction factor e^{2ψ} = TRM g_00 / AT conformal g_00.</summary>
    public static double G00Correction(double x, double b = 0.3) => Math.Exp(2.0 * Psi(x, b));

    /// <summary>Weak-field (x→0) correction = e^{2ψ(0)} = 1 (TRM reduces to AT).</summary>
    public static double WeakFieldCorrection(double b = 0.3) => G00Correction(0.0, b);

    /// <summary>Strong-field correction e^{2ψ(x)} at a finite x (departs from 1).</summary>
    public static double StrongFieldCorrection(double x, double b = 0.3) => G00Correction(x, b);

    /// <summary>TRM reduces to AT iff the correction ≈ 1 (ψ → 0).</summary>
    public static bool ReducesToAt(double correction, double tol = 1e-6) => Math.Abs(correction - 1.0) < tol;

    /// <summary>Departure magnitude |e^{2ψ} − 1| — grows monotonically with field strength |x|.</summary>
    public static double Departure(double x, double b = 0.3) => Math.Abs(G00Correction(x, b) - 1.0);

    /// <summary>Core density ρ(0) for the AT conformal metric — finite (regular core).</summary>
    public static double CoreDensity() => MetricAnsatzAudit.Profile(0.0);

    /// <summary>√(−g)(0) for the ψ-perturbed metric — same ρ(0) (volume-preserving → regular core unchanged).</summary>
    public static double TrmCoreVolumeElement(int d) => MetricAnsatzAudit.PerturbedVolumeElement(0.0, d);

    /// <summary>Is the spin-2 (graviton) degree of freedom confined to the UV? No — GWs propagate at all scales.</summary>
    public static bool TensorDofUvConfined() => false;

    /// <summary>Does the extension reduce EXACTLY to AT in the weak-field/IR limit? Yes.</summary>
    public static bool IrReductionHolds() => ReducesToAt(WeakFieldCorrection());
}
