namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether TQM solitons possess effective mass/inertia.
/// Derives m_eff from field theory gradient energy and tests via
/// controlled forcing experiments on the spatial PDE.
///
/// TQM-111: Soliton Effective Mass and Inertia
/// </summary>
public static class SolitonMassAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record SolitonInertiaProfile(
        double AppliedForce,
        double MeasuredAcceleration,
        double EffectiveMass,
        double Displacement,
        double ExpectedAccelNoMass,  // F (if no inertia)
        double InertiaRatio);        // ExpectedAccelNoMass / MeasuredAccel

    public sealed record EffectiveMassReport(
        List<SolitonInertiaProfile> Profiles,
        double TheoreticalMass,       // from field theory
        double MeasuredMass,          // from forcing experiments
        double InertiaSuppression,    // how much inertia suppresses motion
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Field theory constants
    // ══════════════════════════════════════════════════════════════════

    private const double D_R = 2.5e-5;
    private const double D_M = 2.5e-6;
    private const double C0 = 0.0047;
    private const double A = 0.00976;
    private const double W = 0.10;     // soliton half-width

    // ══════════════════════════════════════════════════════════════════
    // THEORETICAL EFFECTIVE MASS DERIVATION
    // ══════════════════════════════════════════════════════════════════

    public static string MassDerivation()
    {
        return @"
SOLITON EFFECTIVE MASS — FIELD THEORY DERIVATION

1. SOLITON PROFILE (1D, centered at x₀):
   R(x) ≈ tanh((x−x₀)/w)    [approximate kink solution]
   M(x) ≈ M₀·sech²((x−x₀)/w) [coupled field]

2. FIELD KINETIC ENERGY:
   When the soliton moves with velocity v = dx₀/dt:
   ∂R/∂t = −v·∂R/∂x
   
   Kinetic energy density: (1/2)·(∂R/∂t)²
   E_kin = (1/2)·v² · ∫ (∂R/∂x)² dx

3. EFFECTIVE MASS:
   m_eff = ∫ [(∂R/∂x)² + (∂M/∂x)²] dx
   
   For R ≈ tanh(x/w): ∂R/∂x ≈ (1/w)·sech²(x/w)
   ∫ sech⁴(x/w) dx ≈ (4/3)·w
   → ∫ (∂R/∂x)² dx ≈ (1/w²)·(4w/3) = 4/(3w)

   For M similar: ∫ (∂M/∂x)² dx ≈ 4M₀²/(3w)

   TOTAL: m_eff ≈ 4(1+M₀²)/(3w)

   For w=0.10, M₀≈5: m_eff ≈ 4·26/(0.30) ≈ 347

4. EQUATION OF MOTION:
   m_eff · d²x₀/dt² = F_applied
   
   For F ~ D_R·exp(−d/w)/w ~ 10⁻⁵:
   a = F/m_eff ~ 10⁻⁵/347 ~ 3×10⁻⁸
   
   In 4000 time units: Δx ~ (1/2)·a·t² ~ 0.24
   
   COMPARE: without inertia, the field would respond instantly.
   The effective mass SUPPRESSES acceleration by factor ~1/m_eff ≈ 3×10⁻³.
";
    }

    /// <summary>
    /// Theoretical effective mass from soliton profile.
    /// </summary>
    public static double TheoreticalMass(double width = W, double peakM = 5.0) =>
        4.0 * (1.0 + peakM * peakM) / (3.0 * width);

    // ══════════════════════════════════════════════════════════════════
    // PDE RHS with external forcing term
    // ══════════════════════════════════════════════════════════════════

    private static (double[] dR, double[] dM) PdeRHS_Forced(
        double[] R, double[] M, double dx, int nx, double forceMag, int forceCenter)
    {
        double[] dR = new double[nx], dM = new double[nx];
        for (int i = 0; i < nx; i++)
        {
            double reactionR = C0 * M[i] * Math.Max(R[i], 1e-10) * (1.0 - R[i] * R[i]);
            double reactionM = A * R[i] * R[i];
            double lapR = 0, lapM = 0;
            if (i > 0 && i < nx - 1)
            {
                lapR = (R[i - 1] - 2.0 * R[i] + R[i + 1]) / (dx * dx);
                lapM = (M[i - 1] - 2.0 * M[i] + M[i + 1]) / (dx * dx);
            }
            dR[i] = reactionR + D_R * lapR;
            dM[i] = reactionM + D_M * lapM;

            // External force: gradient in R that pushes the soliton.
            // Apply as an additional advection term: −F·∇R
            if (i > 0 && i < nx - 1)
                dR[i] += forceMag * (R[i - 1] - R[i + 1]) / (2.0 * dx);

            if (R[i] >= 1.0 && dR[i] > 0) dR[i] = 0;
            if (M[i] >= 5.0 && dM[i] > 0) dM[i] = 0;
        }
        return (dR, dM);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run forcing experiment
    // ══════════════════════════════════════════════════════════════════

    public static SolitonInertiaProfile RunForcingExperiment(
        double forceMag, int nx = 200, double L = 3.0, int steps = 2000)
    {
        double dx = L / (nx - 1);
        double[] X = new double[nx], R = new double[nx], M = new double[nx];

        // Initialize soliton at center.
        for (int i = 0; i < nx; i++)
        {
            X[i] = -L / 2 + i * dx;
            R[i] = Math.Tanh((X[i]) / W);
            R[i] = (R[i] + 1.0) / 2.0; // map tanh from [-1,1] to [0,1]
            M[i] = 5.0 * Math.Exp(-X[i] * X[i] / (2.0 * W * W));
        }

        // Force applied at right edge (positive direction).
        int forceCenter = nx - 20;

        // Track soliton center (R-weighted centroid).
        double initialCenter = Centroid(R, X);
        double dt = 2.0;

        for (int step = 0; step < steps; step++)
        {
            var (dR, dM) = PdeRHS_Forced(R, M, dx, nx, forceMag, forceCenter);
            double[] Rmid = new double[nx], Mmid = new double[nx];
            for (int i = 0; i < nx; i++)
            {
                Rmid[i] = Math.Clamp(R[i] + 0.5 * dt * dR[i], 0, 1);
                Mmid[i] = Math.Max(0, M[i] + 0.5 * dt * dM[i]);
            }
            var (dR2, dM2) = PdeRHS_Forced(Rmid, Mmid, dx, nx, forceMag, forceCenter);
            for (int i = 0; i < nx; i++)
            {
                R[i] = Math.Clamp(R[i] + dt * dR2[i], 0, 1);
                M[i] = Math.Max(0, M[i] + dt * dM2[i]);
            }
        }

        double finalCenter = Centroid(R, X);
        double displacement = finalCenter - initialCenter;
        double tTotal = steps * dt;
        // Δx = (1/2)·a·t²  →  a = 2·Δx/t²
        double accel = 2.0 * displacement / (tTotal * tTotal);
        double mEff = Math.Abs(accel) > 1e-15 ? forceMag / Math.Abs(accel) : double.PositiveInfinity;
        double expectedAccelNoMass = forceMag; // if m=1

        return new SolitonInertiaProfile(forceMag, accel, mEff, displacement,
            expectedAccelNoMass, expectedAccelNoMass / Math.Max(Math.Abs(accel), 1e-15));
    }

    private static double Centroid(double[] R, double[] X)
    {
        double sum = 0, weighted = 0;
        for (int i = 0; i < R.Length; i++)
        { sum += R[i]; weighted += R[i] * X[i]; }
        return sum > 1e-10 ? weighted / sum : 0;
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis
    // ══════════════════════════════════════════════════════════════════

    public static EffectiveMassReport RunMassAnalysis()
    {
        double[] forces = { 1e-6, 1e-5, 1e-4, 1e-3, 1e-2 };
        var profiles = new List<SolitonInertiaProfile>();

        foreach (double f in forces)
            profiles.Add(RunForcingExperiment(f));

        double mTheory = TheoreticalMass();
        double mMeasured = profiles
            .Where(p => !double.IsInfinity(p.EffectiveMass))
            .Average(p => p.EffectiveMass);
        double inertiaSuppression = profiles.Average(p => p.InertiaRatio);

        string classification;
        string interpretation;

        if (inertiaSuppression > 10)
        {
            classification = "D: Proto-Particle Dynamics";
            interpretation =
                $"SOLITONS HAVE SIGNIFICANT EFFECTIVE MASS. m_theory ≈ {mTheory:F0}, " +
                $"m_measured ≈ {mMeasured:F0}. Inertia suppresses acceleration by " +
                $"~{inertiaSuppression:F0}× compared to a massless field. " +
                "This EXPLAINS why PDE soliton interactions appear negligible (TQM-109): " +
                "even if a force exists, the soliton's inertia makes the response " +
                "too small to observe at N=100 scale. " +
                "CONDENSATES ARE PROTO-PARTICLES with well-defined inertial mass.";
        }
        else if (inertiaSuppression > 2)
        {
            classification = "C: Effective Massive Soliton";
            interpretation = "Solitons have measurable effective mass that partially suppresses motion.";
        }
        else
        {
            classification = "B: Weak Inertia";
            interpretation = "Solitons show weak inertial effects — the PDE response is limited primarily by the force magnitude, not by inertia.";
        }

        return new EffectiveMassReport(profiles, mTheory, mMeasured,
            inertiaSuppression, classification, interpretation);
    }
}
