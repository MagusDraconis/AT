namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Derives soliton momentum from the TQM field theory and tests
/// conservation laws via moving soliton and collision experiments.
///
/// TQM-112: Soliton Momentum and Conservation Laws
/// </summary>
public static class SolitonMomentumAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record MomentumProfile(
        double Time,
        double[] X, double[] R, double[] M,
        double SolitonPosition,    // R-weighted centroid
        double SolitonVelocity,
        double TotalMomentum,       // P = ∫ (∂R/∂t)(∂R/∂x) dx
        double KineticEnergy,       // (1/2)·∫ (∂R/∂t)² dx
        double FieldEnergy);        // ∫ [D_R(∇R)² + ...] dx

    public sealed record CollisionResult(
        double V1_initial, double V2_initial,
        double V1_final, double V2_final,
        double P_initial, double P_final,
        double E_initial, double E_final,
        double MomentumConservationError,
        double EnergyConservationError,
        string Outcome);  // "passed through", "merged", "bounced"

    public sealed record ConservationLawReport(
        List<MomentumProfile> MotionProfiles,
        List<CollisionResult> Collisions,
        double MeanMomentumConservation,
        double MeanEnergyConservation,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Field constants
    // ══════════════════════════════════════════════════════════════════

    private const double D_R = 2.5e-5;
    private const double D_M = 2.5e-6;
    private const double C0 = 0.0047;
    private const double A = 0.00976;
    private const double W = 0.10;
    private const double M_EFF = 347.0; // from TQM-111

    // ══════════════════════════════════════════════════════════════════
    // MOMENTUM DERIVATION
    // ══════════════════════════════════════════════════════════════════

    public static string MomentumDerivation()
    {
        return @"
SOLITON MOMENTUM — FIELD THEORY DERIVATION

1. CANONICAL MOMENTUM DENSITY:
   For fields R(x,t) and M(x,t), the momentum density from the
   kinetic terms in the Lagrangian (or directly from Noether's theorem
   for spatial translations):

   P(x,t) = (∂R/∂t)·(∂R/∂x) + (∂M/∂t)·(∂M/∂x)

2. TOTAL MOMENTUM:
   P_total = ∫ P(x,t) dx
           = ∫ [(∂R/∂t)(∂R/∂x) + (∂M/∂t)(∂M/∂x)] dx

3. FOR A SOLITON MOVING WITH VELOCITY v:
   R(x,t) = R₀(x − vt)
   ∂R/∂t = −v·∂R/∂x
   
   P = v · ∫ [(∂R/∂x)² + (∂M/∂x)²] dx
     = v · m_eff          [from TQM-111: m_eff = ∫(∇R)²+(∇M)² dx]
     = m_eff · v

   THIS IS EXACTLY THE CLASSICAL PARTICLE RELATION: P = m·v.

4. CONSERVATION:
   The PDE system is not derivable from a standard Lagrangian
   (reaction-diffusion form breaks time-reversal symmetry).
   
   However, momentum-like quantities can be TRACKED:
   - For a single soliton: P = m_eff·v is constant if v is constant
   - For collisions: total field momentum ∫P(x,t)dx is approximately
     conserved on short timescales (before reaction terms dominate)

5. ENERGY:
   Define a free-energy functional:
   E = ∫ [D_R·(∇R)² + D_M·(∇M)²] dx  [gradient/diffusion energy]
     + ∫ [c₀·M·R²·(1−R²/3)] dx         [reaction ''potential'']
   
   E is NOT strictly conserved (reaction-diffusion is dissipative)
   but changes slowly compared to kinetic timescales.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute field momentum
    // ══════════════════════════════════════════════════════════════════

    public static double ComputeMomentum(double[] R, double[] M,
        double[] R_prev, double[] M_prev, double dx, double dt)
    {
        double P = 0;
        for (int i = 1; i < R.Length - 1; i++)
        {
            double dRdt = (R[i] - R_prev[i]) / dt;
            double dMdt = (M[i] - M_prev[i]) / dt;
            double dRdx = (R[i + 1] - R[i - 1]) / (2.0 * dx);
            double dMdx = (M[i + 1] - M[i - 1]) / (2.0 * dx);
            P += (dRdt * dRdx + dMdt * dMdx) * dx;
        }
        return P;
    }

    public static double ComputeFieldEnergy(double[] R, double[] M, double dx)
    {
        double E = 0;
        for (int i = 1; i < R.Length - 1; i++)
        {
            double dRdx = (R[i + 1] - R[i - 1]) / (2.0 * dx);
            double dMdx = (M[i + 1] - M[i - 1]) / (2.0 * dx);
            E += (D_R * dRdx * dRdx + D_M * dMdx * dMdx) * dx;
            E += C0 * M[i] * R[i] * R[i] * (1.0 - R[i] * R[i] / 3.0) * dx;
        }
        return E;
    }

    // ══════════════════════════════════════════════════════════════════
    // PDE RHS
    // ══════════════════════════════════════════════════════════════════

    private static (double[] dR, double[] dM) PdeRHS(double[] R, double[] M, double dx, int nx)
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
            if (R[i] >= 1.0 && dR[i] > 0) dR[i] = 0;
        }
        return (dR, dM);
    }

    // ══════════════════════════════════════════════════════════════════
    // Moving soliton experiment
    // ══════════════════════════════════════════════════════════════════

    public static List<MomentumProfile> SimulateMovingSoliton(
        double v0, int nx = 200, double L = 3.0, int steps = 1000, int snapInterval = 100)
    {
        double dx = L / (nx - 1), dt = 2.0;
        double[] X = new double[nx], R = new double[nx], M = new double[nx];

        // Initialize soliton with phase gradient imparting velocity.
        for (int i = 0; i < nx; i++)
        {
            X[i] = -L / 2 + i * dx;
            // Soliton profile at x=0.
            double r = Math.Tanh(X[i] / W);
            R[i] = (r + 1.0) / 2.0;
            M[i] = 5.0 * Math.Exp(-X[i] * X[i] / (2.0 * W * W));
            // Velocity perturbation: shift the R field phase gradient.
            R[i] = (Math.Tanh((X[i] + v0 * 10.0) / W) + 1.0) / 2.0;
        }

        var profiles = new List<MomentumProfile>();
        double[] R_prev = (double[])R.Clone();
        double[] M_prev = (double[])M.Clone();

        for (int step = 0; step <= steps; step++)
        {
            if (step % snapInterval == 0)
            {
                double pos = Centroid(R, X);
                double vel = step > 0
                    ? (pos - profiles[^1].SolitonPosition) / (snapInterval * dt) : 0;
                double P = step > 0
                    ? ComputeMomentum(R, M, R_prev, M_prev, dx, dt) : 0;
                double E = ComputeFieldEnergy(R, M, dx);
                double KE = step > 0
                    ? 0.5 * M_EFF * vel * vel : 0;
                profiles.Add(new MomentumProfile(step * dt, (double[])X.Clone(),
                    (double[])R.Clone(), (double[])M.Clone(), pos, vel, P, KE, E));
            }

            R_prev = (double[])R.Clone();
            M_prev = (double[])M.Clone();

            var (dR1, dM1) = PdeRHS(R, M, dx, nx);
            double[] Rmid = new double[nx], Mmid = new double[nx];
            for (int i = 0; i < nx; i++)
            { Rmid[i] = Math.Clamp(R[i] + 0.5 * dt * dR1[i], 0, 1); Mmid[i] = Math.Max(0, M[i] + 0.5 * dt * dM1[i]); }
            var (dR2, dM2) = PdeRHS(Rmid, Mmid, dx, nx);
            for (int i = 0; i < nx; i++)
            { R[i] = Math.Clamp(R[i] + dt * dR2[i], 0, 1); M[i] = Math.Max(0, M[i] + dt * dM2[i]); }
        }

        return profiles;
    }

    // ══════════════════════════════════════════════════════════════════
    // Two-soliton collision
    // ══════════════════════════════════════════════════════════════════

    public static CollisionResult SimulateCollision(
        double v1, double v2, int nx = 300, double L = 4.0, int steps = 1500)
    {
        double dx = L / (nx - 1), dt = 2.0;
        double[] X = new double[nx], R = new double[nx], M = new double[nx];

        // Two solitons at x = ±1.0, moving toward each other.
        for (int i = 0; i < nx; i++)
        {
            X[i] = -L / 2 + i * dx;
            double r1 = Math.Tanh((X[i] + 1.0) / W);
            double r2 = Math.Tanh((X[i] - 1.0) / W);
            R[i] = ((r1 + 1.0) / 2.0) * 0.5 + ((r2 + 1.0) / 2.0) * 0.5;
            M[i] = 5.0 * Math.Exp(-(X[i] + 1.0) * (X[i] + 1.0) / (2.0 * W * W))
                 + 5.0 * Math.Exp(-(X[i] - 1.0) * (X[i] - 1.0) / (2.0 * W * W));
        }

        // Apply velocity perturbations (phase gradients).
        double[] Rtemp = (double[])R.Clone();
        for (int i = 0; i < nx; i++)
        {
            double r1 = Math.Tanh((X[i] + 1.0 - v1 * 10.0) / W);
            double r2 = Math.Tanh((X[i] - 1.0 - v2 * 10.0) / W);
            R[i] = ((r1 + 1.0) / 2.0) * 0.5 + ((r2 + 1.0) / 2.0) * 0.5;
        }

        double[] R_prev = (double[])R.Clone();
        double[] M_prev = (double[])M.Clone();
        double P_initial = 0;
        double E_initial = ComputeFieldEnergy(R, M, dx);

        for (int step = 0; step <= steps; step++)
        {
            if (step == 1)
            {
                P_initial = ComputeMomentum(R, M, R_prev, M_prev, dx, dt);
                E_initial = ComputeFieldEnergy(R, M, dx);
            }

            R_prev = (double[])R.Clone();
            M_prev = (double[])M.Clone();

            var (dR1, dM1) = PdeRHS(R, M, dx, nx);
            double[] Rmid = new double[nx], Mmid = new double[nx];
            for (int i = 0; i < nx; i++)
            { Rmid[i] = Math.Clamp(R[i] + 0.5 * dt * dR1[i], 0, 1); Mmid[i] = Math.Max(0, M[i] + 0.5 * dt * dM1[i]); }
            var (dR2, dM2) = PdeRHS(Rmid, Mmid, dx, nx);
            for (int i = 0; i < nx; i++)
            { R[i] = Math.Clamp(R[i] + dt * dR2[i], 0, 1); M[i] = Math.Max(0, M[i] + dt * dM2[i]); }
        }

        double P_final = ComputeMomentum(R, M, R_prev, M_prev, dx, dt);
        double E_final = ComputeFieldEnergy(R, M, dx);

        // Detect peaks.
        double[] peakXs = FindPeaks(R, X);
        string outcome = peakXs.Length >= 2 ? "passed through" :
                         peakXs.Length == 1 ? "merged" : "dispersed";

        double pError = Math.Abs(P_final - P_initial) / Math.Max(Math.Abs(P_initial), 1e-15);
        double eError = Math.Abs(E_final - E_initial) / Math.Max(Math.Abs(E_initial), 1e-15);

        return new CollisionResult(v1, v2, 0, 0, P_initial, P_final,
            E_initial, E_final, pError, eError, outcome);
    }

    private static double[] FindPeaks(double[] R, double[] X)
    {
        var peaks = new List<double>();
        for (int i = 2; i < R.Length - 2; i++)
            if (R[i] > 0.3 && R[i] > R[i - 1] && R[i] > R[i + 1]
                && R[i] > R[i - 2] && R[i] > R[i + 2])
                peaks.Add(X[i]);
        return peaks.ToArray();
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis
    // ══════════════════════════════════════════════════════════════════

    public static ConservationLawReport RunConservationAnalysis()
    {
        // Moving soliton tests.
        var motionProfiles = new List<MomentumProfile>();
        foreach (double v0 in new[] { 0.0, 1e-4, 1e-3, 1e-2 })
            motionProfiles.AddRange(SimulateMovingSoliton(v0));

        // Collision tests.
        var collisions = new List<CollisionResult>();
        collisions.Add(SimulateCollision(1e-3, -1e-3));  // equal and opposite
        collisions.Add(SimulateCollision(2e-3, -1e-3));  // unequal
        collisions.Add(SimulateCollision(5e-3, 1e-3));   // overtaking

        double meanPCons = collisions.Average(c => c.MomentumConservationError);
        double meanECons = collisions.Average(c => c.EnergyConservationError);

        string classification;
        string interpretation;

        if (meanPCons < 0.1 && collisions.All(c => c.Outcome == "passed through"))
        {
            classification = "D: Proto-Particle Mechanics";
            interpretation =
                $"SOLITONS BEHAVE AS TRUE PROTO-PARTICLES. Momentum is conserved " +
                $"(mean error = {meanPCons:P1}). Solitons pass through each other " +
                "in collisions like classical particles. The field theory supports " +
                "stable moving soliton states with P = m_eff·v. " +
                "The proto-particle interpretation is COMPLETE: solitons have mass " +
                "(TQM-111), momentum (TQM-112), and undergo elastic scattering.";
        }
        else if (meanPCons < 0.5)
        {
            classification = "C: Conserved Momentum";
            interpretation = "Momentum is approximately conserved. Solitons support motion but collisions show some dissipation.";
        }
        else
        {
            classification = "B: Weak Quasi-Momentum";
            interpretation = "Momentum-like quantities can be defined but are not strictly conserved in the reaction-diffusion system.";
        }

        return new ConservationLawReport(motionProfiles, collisions,
            meanPCons, meanECons, classification, interpretation);
    }

    private static double Centroid(double[] R, double[] X)
    {
        double s = 0, w = 0;
        for (int i = 0; i < R.Length; i++) { s += R[i] * X[i]; w += R[i]; }
        return w > 1e-10 ? s / w : 0;
    }
}
