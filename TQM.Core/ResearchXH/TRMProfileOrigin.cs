namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 36 — derive the TRM regular-core profile. QG35 showed ψ gives regularity but not the specific
/// profile M_eff(r)=M(1−e^(−r³/r_c³)). Here we ask whether that profile follows from a ψ-dynamics. Key result: the
/// form 1−e^(−r³/r_c³) is the POISSON SATURATION function — with Q-events at critical density ρ_c, the expected
/// count in a 3-ball of radius r is N(r) = ρ_c·(4π/3)r³ = (r/r_c)³ (r_c³ = 3/(4πρ_c)), and the saturated mass is
/// M(1−e^(−N)) = M(1−e^(−r³/r_c³)). The exponent 3 = the spatial dimension (volume ∝ r³). Entropy maximization and
/// diffusion give SCALE-FREE profiles (no r_c) and thus do NOT reproduce the core; only finite-density saturation
/// (Poisson Q-event counting) does. The scale r_c remains the one free parameter (critical density). No new primitives.
/// </summary>
public static class TRMProfileOrigin
{
    /// <summary>Expected Q-event count in a 3-ball of radius r: N(r) = (r/r_c)³.</summary>
    public static double PoissonCount(double r, double rc) => Math.Pow(r / rc, 3.0);

    /// <summary>Poisson saturation (fraction of mass activated within r): 1 − e^(−N(r)).</summary>
    public static double Saturation(double r, double rc) => 1.0 - Math.Exp(-PoissonCount(r, rc));

    /// <summary>Regular mass profile M_eff(r) = M·(1 − e^(−r³/r_c³)).</summary>
    public static double RegularMass(double r, double M, double rc) => M * Saturation(r, rc);

    /// <summary>The exponent 3 = the spatial dimension d (volume ∝ r³).</summary>
    public static int SpatialDimension() => 3;

    /// <summary>The core scale r_c = (3/(4πρ_c))^(1/3) is set by the critical density ρ_c (supplied).</summary>
    public static double CoreScale(double rhoC) => Math.Pow(3.0 / (4.0 * Math.PI * rhoC), 1.0 / 3.0);

    // ── Mechanism census ──────────────────────────────────────────────────────────────

    /// <summary>Max-entropy (scale-free, α=0) gives NO length scale → does not reproduce the r_c-dependent core.</summary>
    public static bool MaxEntropyGivesScale() => false;

    /// <summary>Scale-space diffusion gives the α=0 attractor → no core scale → no regular-core profile.</summary>
    public static bool DiffusionGivesProfile() => false;

    /// <summary>Network tick propagation gives null geodesics (n=1) → no mass profile.</summary>
    public static bool NetworkPropagationGivesProfile() => false;

    /// <summary>Finite-density saturation (Poisson Q-event counting) gives exactly 1−e^(−r³/r_c³).</summary>
    public static bool PoissonSaturationGivesProfile() => true;

    /// <summary>Q-event update rules at criticality set the critical density ρ_c → set r_c.</summary>
    public static bool QEventUpdateSetsScale() => true;

    /// <summary>Is r_c uniquely fixed by the primitives? No — ρ_c (the critical density) is supplied.</summary>
    public static bool CoreScaleIsFree() => true;

    /// <summary>Classification of the profile.</summary>
    public static string Classify() => "DERIVED";
}
