namespace TQM.Core.ResearchXH;

/// <summary>Curvature→density feedback models (ρ̇ driven by reconstructed curvature R̂ = F(ρ)).</summary>
public enum FeedbackModel
{
    /// <summary>ρ̇ = −k·R.</summary>
    Linear,

    /// <summary>ρ̇ = −k·sign(R).</summary>
    Sign,

    /// <summary>ρ̇ = −k·R·ρ.</summary>
    Product
}

/// <summary>Primitive-native restoring terms added to ρ̇ = −kR (G4-E Phase 2).</summary>
public enum RestoringTerm
{
    /// <summary>No restoring term (pure anti-diffusive feedback).</summary>
    None,

    /// <summary>Linear diffusion: −d·(ρ−1).</summary>
    Diffusion,

    /// <summary>Cubic (logistic) restoring: −c·(ρ−1)³.</summary>
    Logistic,

    /// <summary>Hard conservation constraint mean(ρ) = 1 (pins the mean density to flat).</summary>
    Conservation
}

/// <summary>
/// G4-E Phase 1: closed-loop curvature–density feedback. Given the native reconstruction
/// R̂ = F(ρ̄) (a monotonically decreasing map, F′(ρ) &lt; 0), evolve ρ in discrete time under
/// the feedback ρ̇ = −k·(R or sign(R) or R·ρ) and classify the resulting dynamics
/// (fixed points, stability, oscillation, runaway). Deterministic: closed-form map + arithmetic.
/// </summary>
public static class CurvatureFeedback
{
    /// <summary>
    /// Native F map (ρ̄ → R̂): sort the (mean density, score) pairs of a linear A-sweep so
    /// ρ̄ is strictly increasing and R̂ = F(ρ̄) is decreasing.
    /// </summary>
    public static (double[] rho, double[] score) BuildMap(int steps = 16, double amp = 0.8, int n = 16)
    {
        var frames = CurvatureDynamics.Evolve(CurvatureDynamics.LinearSweep(steps, -amp, +amp), n);
        var sorted = frames.Select(f => (f.MeanDensity, f.Score))
                           .OrderBy(x => x.MeanDensity)
                           .ToArray();
        return (sorted.Select(x => x.MeanDensity).ToArray(),
                sorted.Select(x => x.Score).ToArray());
    }

    /// <summary>Feedback rate ρ̇ for a given model at (ρ, R).</summary>
    public static double FeedbackRate(FeedbackModel model, double rho, double r, double k)
        => model switch
        {
            FeedbackModel.Linear => -k * r,
            FeedbackModel.Sign => -k * Math.Sign(r),
            FeedbackModel.Product => -k * r * rho,
            _ => 0.0
        };

    /// <summary>Linear interpolation on the F map, clamped (constant extrapolation) outside range.</summary>
    public static double Interpolate(double[] xs, double[] ys, double x)
    {
        if (x <= xs[0]) return ys[0];
        if (x >= xs[^1]) return ys[^1];
        int i = Array.BinarySearch(xs, x);
        if (i >= 0) return ys[i];
        i = ~i;
        double t = (x - xs[i - 1]) / (xs[i] - xs[i - 1]);
        return ys[i - 1] + t * (ys[i] - ys[i - 1]);
    }

    /// <summary>Central-difference estimate of F′(ρ̄) at the flat point ρ̄ = 1.</summary>
    public static double SlopeAtFlat(double[] rhoMap, double[] scoreMap)
    {
        int i = Array.BinarySearch(rhoMap, 1.0);
        if (i < 0) i = ~i;
        if (i == 0) i = 1;
        if (i >= rhoMap.Length - 1) i = rhoMap.Length - 2;
        double dr = rhoMap[i + 1] - rhoMap[i - 1];
        double ds = scoreMap[i + 1] - scoreMap[i - 1];
        return ds / dr;
    }

    /// <summary>Discrete-time feedback simulation: ρ_{t+1} = ρ_t + Δt·ρ̇(ρ_t, R̂=F(ρ_t)).</summary>
    public static double[] Simulate(double[] rhoMap, double[] scoreMap, FeedbackModel model,
        double k, double dt, int steps, double rho0)
    {
        var rho = new double[steps + 1];
        rho[0] = rho0;
        for (int t = 0; t < steps; t++)
        {
            double r = Interpolate(rhoMap, scoreMap, rho[t]);
            rho[t + 1] = rho[t] + dt * FeedbackRate(model, rho[t], r, k);
        }
        return rho;
    }

    /// <summary>
    /// Discrete-time feedback with a primitive-native restoring term:
    /// ρ_{t+1} = ρ_t + Δt·(−k·F(ρ_t) + restoring(ρ_t)). The Conservation term applies a hard
    /// projection ρ_{t+1} = 1 (mean pinned to flat) rather than an additive force.
    /// </summary>
    public static double[] SimulateRestoring(double[] rhoMap, double[] scoreMap,
        double k, double dt, int steps, double rho0, RestoringTerm term, double strength)
    {
        var rho = new double[steps + 1];
        rho[0] = rho0;
        for (int t = 0; t < steps; t++)
        {
            if (term == RestoringTerm.Conservation)
            {
                rho[t + 1] = 1.0;
                continue;
            }
            double r = Interpolate(rhoMap, scoreMap, rho[t]);
            double feedback = -k * r;
            double restoring = term switch
            {
                RestoringTerm.Diffusion => -strength * (rho[t] - 1.0),
                RestoringTerm.Logistic => -strength * Math.Pow(rho[t] - 1.0, 3),
                _ => 0.0
            };
            rho[t + 1] = rho[t] + dt * (feedback + restoring);
        }
        return rho;
    }

    /// <summary>Classify a trajectory: "fixed" (converged), "oscillatory", or "runaway".</summary>
    public static string Classify(double[] rho)
    {
        int signChanges = 0, prevSign = 0;
        for (int t = 0; t < rho.Length - 1; t++)
        {
            int s = Math.Sign(rho[t + 1] - rho[t]);
            if (s != 0)
            {
                if (prevSign != 0 && s != prevSign) signChanges++;
                prevSign = s;
            }
        }
        if (Math.Abs(rho[^1] - rho[^2]) < 1e-6) return "fixed";
        if (signChanges >= 2) return "oscillatory";
        return "runaway";
    }
}
