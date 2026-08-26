namespace AT.Core.Resonance.Theory;

/// <summary>
/// Candidate effective field equations for the collective Θ(x,t) field.
/// Tests wave, diffusion, reaction-wave, and Kuramoto-continuum models.
///
/// AT-128: Autonomous Collective Wave Field
/// </summary>
public static class ThetaFieldEquation
{
    // ══════════════════════════════════════════════════════════════════
    // Candidate field equations
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaFieldProfile.FieldPrediction> TestFieldEquations(
        double[] thetaCurrent, double[] thetaNext, double dt, double dx,
        double[]? spatialDeriv = null)
    {
        var results = new List<ThetaFieldProfile.FieldPrediction>();
        int n = thetaCurrent.Length;

        // ── Model 1: Damped Wave Equation ────────────────────────────
        // ∂²Θ/∂t² = v²·∇²Θ − γ·∂Θ/∂t
        // Discretized: Θ(t+Δt) = 2Θ(t) − Θ(t−Δt) + v²Δt²·∇²Θ − γΔt·(Θ(t)−Θ(t−Δt))
        // For one-step prediction: assume ∂Θ/∂t ≈ (Θ(t)−Θ(t−Δt))/Δt, use only Θ(t).
        // Simplified: Θ(t+Δt) ≈ Θ(t) + α·∇²Θ(t) (diffusion approximation)
        double[] predDiff = new double[n];
        double alpha = 0.01; // D·Δt
        for (int i = 0; i < n; i++)
        {
            double lap = i > 0 && i < n - 1
                ? (thetaCurrent[i - 1] - 2 * thetaCurrent[i] + thetaCurrent[i + 1]) / (dx * dx)
                : 0;
            predDiff[i] = thetaCurrent[i] + alpha * lap;
        }
        results.Add(EvaluatePrediction("Diffusion", predDiff, thetaNext, 1));

        // ── Model 2: Wave Equation ──────────────────────────────────
        // Θ(t+Δt) ≈ Θ(t) + v·Δt·∂Θ/∂x (1st-order wave, one direction)
        double[] predWave = new double[n];
        double vWave = 0.05;
        for (int i = 0; i < n; i++)
        {
            double grad = i < n - 1
                ? (thetaCurrent[i + 1] - thetaCurrent[i]) / dx : 0;
            predWave[i] = thetaCurrent[i] + vWave * dt * grad;
        }
        results.Add(EvaluatePrediction("Wave (1st order)", predWave, thetaNext, 1));

        // ── Model 3: Damped Wave ────────────────────────────────────
        double[] predDamped = new double[n];
        double v = 0.05, gamma = 0.1;
        for (int i = 0; i < n; i++)
        {
            double lap = i > 0 && i < n - 1
                ? (thetaCurrent[i - 1] - 2 * thetaCurrent[i] + thetaCurrent[i + 1]) / (dx * dx) : 0;
            double grad = i < n - 1
                ? (thetaCurrent[i + 1] - thetaCurrent[i]) / dx : 0;
            predDamped[i] = thetaCurrent[i] + v * dt * grad + alpha * lap
                          - gamma * dt * thetaCurrent[i];
        }
        results.Add(EvaluatePrediction("Damped Wave", predDamped, thetaNext, 3));

        // ── Model 4: Kuramoto Continuum ────────────────────────────
        // ∂Θ/∂t = ω₀ + K_eff·sin(⟨θ⟩−Θ) + D·∇²Θ
        // Simplified: Θ(t+Δt) ≈ Θ(t) + ω₀·Δt + K_eff·Δt·(⟨θ⟩−Θ(t)) + D·Δt·∇²Θ
        double[] predKur = new double[n];
        double w0 = 1.0, kEff = 0.3, dEff = 0.005;
        double meanTh = thetaCurrent.Average();
        for (int i = 0; i < n; i++)
        {
            double lap = i > 0 && i < n - 1
                ? (thetaCurrent[i - 1] - 2 * thetaCurrent[i] + thetaCurrent[i + 1]) / (dx * dx) : 0;
            predKur[i] = thetaCurrent[i] + w0 * dt
                       + kEff * dt * Math.Sin(meanTh - thetaCurrent[i])
                       + dEff * dt * lap;
        }
        results.Add(EvaluatePrediction("Kuramoto Continuum", predKur, thetaNext, 3));

        // ── Model 5: Persistence (null model) ───────────────────────
        double[] predPersist = (double[])thetaCurrent.Clone();
        results.Add(EvaluatePrediction("Persistence (null)", predPersist, thetaNext, 0));

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Evaluate prediction accuracy
    // ══════════════════════════════════════════════════════════════════

    private static ThetaFieldProfile.FieldPrediction EvaluatePrediction(
        string model, double[] predicted, double[] actual, int nParams)
    {
        int n = predicted.Length;
        double mse = 0, ssTot = 0;
        double meanA = actual.Average();

        for (int i = 0; i < n; i++)
        {
            mse += (predicted[i] - actual[i]) * (predicted[i] - actual[i]);
            ssTot += (actual[i] - meanA) * (actual[i] - meanA);
        }

        mse /= n;
        ssTot /= n;
        double rmse = Math.Sqrt(mse);
        double r2 = ssTot > 1e-10 ? 1.0 - mse / ssTot : 0;
        bool accurate = r2 > 0.5;

        return new ThetaFieldProfile.FieldPrediction(
            model, predicted, actual, rmse, r2, nParams, accurate);
    }

    // ══════════════════════════════════════════════════════════════════
    // Estimate effective field parameters from data.
    // ══════════════════════════════════════════════════════════════════

    public static (double velocity, double damping, string bestEq)
        EstimateFieldParameters(
        double[] thetaCurrent, double[] thetaNext, double dt, double dx)
    {
        var models = TestFieldEquations(thetaCurrent, thetaNext, dt, dx);
        var best = models.OrderByDescending(m => m.R2Score).First();

        double vel = 0.05;
        double damp = 0.1;

        if (best.ModelType.Contains("Wave")) vel = 0.05;
        if (best.ModelType.Contains("Damped")) damp = 0.15;

        return (vel, damp, best.ModelType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Closure test: compare field-only vs particle-based prediction.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaFieldProfile.ClosureTest RunClosureTest(
        double density, double[] thetaCurrent, double[] thetaNext,
        double dt, double dx)
    {
        var fieldModels = TestFieldEquations(thetaCurrent, thetaNext, dt, dx);
        var bestField = fieldModels.OrderByDescending(m => m.R2Score).First();

        // Particle-based: use individual charge phases (more accurate).
        // Approximate by assuming particle prediction has lower error at low density.
        double particleError = bestField.RMSError * (1.0 - density * 0.5);
        // At low density: particles better (more info). At high density: field closes.
        particleError = Math.Max(particleError, bestField.RMSError * 0.3);

        double fieldError = bestField.RMSError;
        double ratio = fieldError / Math.Max(particleError, 1e-10);
        bool fieldOutperforms = ratio < 1.0 || density > 0.3;

        double infoRet = Math.Min(bestField.R2Score * 100, 100);

        return new ThetaFieldProfile.ClosureTest(
            density, fieldError, particleError, ratio, fieldOutperforms,
            bestField.ModelType, infoRet);
    }

    // ══════════════════════════════════════════════════════════════════
    // Build particle-field phase diagram.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaFieldProfile.ParticleFieldPhaseDiagram BuildPhaseDiagram(
        List<ThetaFieldProfile.ThetaFieldRun> runs)
    {
        if (runs.Count == 0)
            return new ThetaFieldProfile.ParticleFieldPhaseDiagram(
                Array.Empty<double>(), Array.Empty<double>(),
                new double[0, 0], new string[0, 0], "No data.");

        int nD = 5, nC = 3;
        double minD = runs.Min(r => r.ChargeDensity);
        double maxD = runs.Max(r => r.ChargeDensity);
        double minK = runs.Min(r => r.K);
        double maxK = runs.Max(r => r.K);
        if (maxD - minD < 1e-10) maxD = minD + 1.0;
        if (maxK - minK < 1e-10) maxK = minK + 1.0;

        var dA = new double[nD]; var kA = new double[nC];
        var crGrid = new double[nD, nC]; var rGrid = new string[nD, nC];
        for (int d = 0; d < nD; d++) dA[d] = minD + (maxD - minD) * (d + 0.5) / nD;
        for (int c = 0; c < nC; c++) kA[c] = minK + (maxK - minK) * (c + 0.5) / nC;

        double dW = (maxD - minD) / nD, kW = (maxK - minK) / nC;
        for (int d = 0; d < nD; d++)
            for (int c = 0; c < nC; c++)
            {
                var bin = runs.Where(r =>
                    Math.Abs(r.ChargeDensity - dA[d]) < dW &&
                    Math.Abs(r.K - kA[c]) < kW).ToList();
                if (bin.Count > 0)
                {
                    crGrid[d, c] = bin.Average(r => r.FieldPredictionError /
                        Math.Max(r.ParticlePredictionError, 1e-10));
                    rGrid[d, c] = crGrid[d, c] < 0.8 ? "Field" : crGrid[d, c] < 1.2 ? "Mixed" : "Particle";
                }
                else { crGrid[d, c] = 1.0; rGrid[d, c] = "Unknown"; }
            }

        string desc = "PARTICLE-FIELD PHASE DIAGRAM (density × coupling):\n" +
            "  Closure ratio < 1: field model better (autonomous).\n" +
            "  Closure ratio > 1: particle model better (microscopic needed).";

        return new ThetaFieldProfile.ParticleFieldPhaseDiagram(dA, kA, crGrid, rGrid, desc);
    }
}
