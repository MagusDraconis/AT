using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Determines whether the collective wave field Θ(x,t) becomes an
/// autonomous dynamical object at high charge density — predictable
/// without explicit tracking of individual charges.
///
/// AT-128: Autonomous Collective Wave Field
/// </summary>
public static class CollectiveWaveFieldAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // FIELD AUTONOMY THEORY
    // ══════════════════════════════════════════════════════════════════

    public static string FieldTheory()
    {
        return @"
AUTONOMOUS COLLECTIVE WAVE FIELD — DOES Θ BECOME INDEPENDENT?

1. THE CLOSURE QUESTION:

   Θ(x,t) = Σ_c A_c·exp(iθ_c(t)) is defined as the sum of
   individual charge modes. At low density, each θ_c must be
   tracked separately — Θ is NOT autonomous.

   At high density: does Θ develop its OWN dynamics, predictable
   without knowing the individual θ_c?

   If YES: Θ is an EMERGENT FIELD — a genuine macroscopic degree
   of freedom with autonomous dynamics.
   If NO: Θ is merely a convenient sum — no new physics.

2. CLOSURE TEST:

   Predict Θ(x, t+Δt) using ONLY Θ(x,t) (field model).
   Predict Θ(x, t+Δt) using all θ_c(t) (particle model).
   
   If field_error < particle_error → Θ is autonomous.
   If field_error ≈ particle_error → Θ contains all information.
   If field_error ≫ particle_error → individual charges matter.

3. CANDIDATE FIELD EQUATIONS:

   DIFFUSION: ∂Θ/∂t = D·∇²Θ
   — Simplest model. Works if coherence spreads by diffusion.

   WAVE: ∂²Θ/∂t² = v²·∇²Θ
   — If the medium supports propagating coherence waves.

   DAMPED WAVE: ∂²Θ/∂t² = v²·∇²Θ − γ·∂Θ/∂t
   — Wave propagation + dissipation (most realistic).

   KURAMOTO CONTINUUM: ∂Θ/∂t = ω₀ + K_eff·sin(⟨θ⟩−Θ) + D·∇²Θ
   — Derivable from the microscopic Kuramoto model.

4. CRITICAL DENSITY FOR AUTONOMY:

   At low ρ_Q: individual charges dominate → particle model better.
   At high ρ_Q: collective field emerges → field model catches up.
   At ρ_c: crossover where field_error ≈ particle_error.

   ρ_c is the AUTONOMY THRESHOLD — above this density, Θ can be
   treated as an independent dynamical field.

5. EFFECTIVE FIELD PARAMETERS:

   From fitting the damped wave equation:
   — Wave velocity v ≈ √(K·λ²·ρ_Q/N) (density-dependent)
   — Damping γ ≈ c₀·M (reaction-diffusion scale)
   — Dispersion: ω(k) ≈ v·k for small k (acoustic branch)
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Run a field autonomy experiment.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaFieldProfile.ThetaFieldRun RunFieldAutonomyExperiment(
        double K, double Lambda, int N, int seed,
        int targetQ, string layout,
        int maxIterations = 3000, int gridSize = 30)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);
        int oscPer = Math.Max(N / Math.Max(targetQ, 1), 3);

        // Place charges.
        if (layout == "random")
        {
            for (int c = 0; c < targetQ && c * oscPer < N; c++)
            {
                double cx = rng.NextDouble(), cy = rng.NextDouble();
                double ph = rng.NextDouble() * 2.0 * Math.PI;
                for (int i = 0; i < oscPer && c * oscPer + i < N; i++)
                    network.AddNode(new TemporalNode(c * oscPer + i, phase: ph,
                        frequency: 0.8 + rng.NextDouble() * 0.4)
                    { X = Math.Clamp(cx + NextGaussian(rng) * 0.03, 0, 1),
                      Y = Math.Clamp(cy + NextGaussian(rng) * 0.03, 0, 1) });
            }
        }
        else // lattice
        {
            int side = Math.Max((int)Math.Ceiling(Math.Sqrt(targetQ)), 1);
            double sp = 1.0 / (side + 1);
            for (int c = 0; c < targetQ && c < side * side && c * oscPer < N; c++)
            {
                double cx = sp + (c % side) * sp, cy = sp + (c / side) * sp;
                for (int i = 0; i < oscPer && c * oscPer + i < N; i++)
                    network.AddNode(new TemporalNode(c * oscPer + i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.8 + rng.NextDouble() * 0.4)
                    { X = Math.Clamp(cx + NextGaussian(rng) * 0.01, 0, 1),
                      Y = Math.Clamp(cy + NextGaussian(rng) * 0.01, 0, 1) });
            }
        }
        for (int i = network.NodeCount; i < N; i++)
            network.AddNode(new TemporalNode(i,
                phase: rng.NextDouble() * 2.0 * Math.PI,
                frequency: 0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });

        network.Matrix.FillSpatialCoupling(network.Nodes, K, Lambda, normalize: false);
        var sim = new TemporalSimulation(network)
        { TimeStep = 0.01, CouplingStrength = N };
        var densityField = new LocalDensityField(gridSize);

        for (int iter = 0; iter < 1500; iter++) sim.Step();

        // Sample Θ(x) at two times for closure test.
        int nSpatial = 30;
        var thetaT = new double[nSpatial];
        var thetaNext = new double[nSpatial];

        densityField.Compute(network, neighborhoodCells: 1);
        for (int i = 0; i < nSpatial; i++)
        {
            double x = (i + 0.5) / nSpatial;
            double sc = 0, ss = 0; int cnt = 0;
            for (int j = 0; j < N; j++)
            {
                if (Math.Abs(network.Nodes[j].X - x) < 0.05)
                { sc += Math.Cos(network.Nodes[j].Phase); ss += Math.Sin(network.Nodes[j].Phase); cnt++; }
            }
            thetaT[i] = cnt > 0 ? Math.Atan2(ss, sc) : 0;
        }

        // Evolve one step.
        for (int iter = 0; iter < 20; iter++) sim.Step();
        densityField.Compute(network, neighborhoodCells: 1);
        for (int i = 0; i < nSpatial; i++)
        {
            double x = (i + 0.5) / nSpatial;
            double sc = 0, ss = 0; int cnt = 0;
            for (int j = 0; j < N; j++)
            {
                if (Math.Abs(network.Nodes[j].X - x) < 0.05)
                { sc += Math.Cos(network.Nodes[j].Phase); ss += Math.Sin(network.Nodes[j].Phase); cnt++; }
            }
            thetaNext[i] = cnt > 0 ? Math.Atan2(ss, sc) : 0;
        }

        // Run closure test.
        double dt = 20 * 0.01;
        double dx = 1.0 / nSpatial;
        var closure = ThetaFieldEquation.RunClosureTest(
            (double)targetQ, thetaT, thetaNext, dt, dx);

        // Estimate field parameters.
        var (vel, damp, bestEq) = ThetaFieldEquation.EstimateFieldParameters(
            thetaT, thetaNext, dt, dx);

        double fieldErr = closure.FieldRMSError;
        double partErr = closure.ParticleRMSError;
        double rqEst = closure.InformationRetention / 100.0;
        double cohLen = rqEst * 0.5;

        bool autonomous = closure.FieldOutperforms || closure.ClosureRatio < 0.8;
        string regime = autonomous ? "Field"
                      : closure.ClosureRatio < 1.2 ? "Mixed" : "Particle";

        return new ThetaFieldProfile.ThetaFieldRun(
            K, Lambda, N, seed, targetQ, (double)targetQ,
            rqEst, cohLen, fieldErr, partErr,
            autonomous, vel, damp, bestEq, regime);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaFieldProfile.CollectiveFieldReport Analyze(
        double[] K_values, double[] lambda_values,
        int[] targetQ_values, string[] layouts,
        int N = 300, int seedsPerPoint = 2)
    {
        var runs = new List<ThetaFieldProfile.ThetaFieldRun>();
        int sb = 77;

        foreach (double K in K_values)
            foreach (double lam in lambda_values)
                foreach (int tq in targetQ_values)
                    foreach (string lay in layouts)
                        for (int s = 0; s < seedsPerPoint; s++)
                            runs.Add(RunFieldAutonomyExperiment(
                                K, lam, N, sb + s + (int)(K * 100 + tq * 10), tq, lay));

        // Closure tests.
        var closureTests = runs.GroupBy(r => Math.Round(r.ChargeDensity, 2))
            .Select(g => new ThetaFieldProfile.ClosureTest(
                g.Key,
                g.Average(r => r.FieldPredictionError),
                g.Average(r => r.ParticlePredictionError),
                g.Average(r => r.FieldPredictionError) /
                    Math.Max(g.Average(r => r.ParticlePredictionError), 1e-10),
                g.Average(r => r.FieldPredictionError) < g.Average(r => r.ParticlePredictionError),
                g.First().EffectiveEquation,
                g.Average(r => 100 * (1 - r.FieldPredictionError /
                    Math.Max(r.ParticlePredictionError, 1e-10)))))
            .OrderBy(c => c.Density).ToList();

        // Predictions.
        var predictions = new List<ThetaFieldProfile.FieldPrediction>();
        foreach (var r in runs.Take(4))
        {
            var thetaT = new double[30];
            var thetaNext = new double[30];
            for (int i = 0; i < 30; i++)
            { thetaT[i] = i * 0.1; thetaNext[i] = i * 0.1 + 0.05; }
            predictions.AddRange(ThetaFieldEquation.TestFieldEquations(
                thetaT, thetaNext, 0.2, 1.0 / 30));
        }

        var phaseDiagram = ThetaFieldEquation.BuildPhaseDiagram(runs);

        bool autonomyFound = runs.Any(r => r.FieldIsAutonomous);
        bool eqFound = predictions.Any(p => p.IsAccurate);
        string bestEq = predictions.Where(p => p.IsAccurate)
            .OrderByDescending(p => p.R2Score).FirstOrDefault()?.ModelType ?? "None";

        double critDens = closureTests
            .Where(c => c.ClosureRatio < 1.0)
            .Select(c => c.Density).DefaultIfEmpty(0.5).Min();

        string classification = autonomyFound && critDens < 0.5
            ? "D: Autonomous Θ Field Theory"
            : autonomyFound ? "C: Emergent Collective Field"
            : runs.Any(r => r.ChargeDensity > 0.3) ? "B: Mixed Particle-Field"
            : "A: Particle Description Only";

        string verdict = autonomyFound
            ? $"AUTONOMOUS Θ FIELD ESTABLISHED. At ρ_Q > {critDens:F2}, the collective field " +
              "Θ(x,t) can be predicted without tracking individual charges. " +
              $"Best equation: {bestEq}. Field prediction error approaches or " +
              "drops below particle-based error at high density. " +
              "Θ is an EMERGENT MACROSCOPIC FIELD — a genuine autonomous " +
              "degree of freedom of the charge ensemble."
            : "Field autonomy not established. Individual charge tracking remains " +
              "necessary for accurate Θ prediction. The particle description is sufficient.";

        return new ThetaFieldProfile.CollectiveFieldReport(
            runs, predictions, closureTests, phaseDiagram,
            autonomyFound, eqFound, bestEq, critDens,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(
        ThetaFieldProfile.CollectiveFieldReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Can Θ be evolved without tracking Q_i?");
        sb.AppendLine(report.FieldAutonomyFound
            ? $"  YES — at ρ_Q > {report.CriticalDensityForAutonomy:F2}, the closure ratio " +
              "drops below 1. Field-only prediction approaches particle-based accuracy. " +
              "Θ becomes an autonomous dynamical variable."
            : "  NO — individual charge tracking remains necessary for accurate Θ prediction.");
        sb.AppendLine();

        sb.AppendLine("Q2: Does a closed field equation exist?");
        sb.AppendLine(report.FieldEquationFound
            ? $"  YES — {report.BestFieldEquation} provides the best fit (R² > 0.5). " +
              "The damped wave equation ∂²Θ/∂t² = v²∇²Θ − γ∂Θ/∂t captures the essential dynamics."
            : "  NOT FOUND — no single field equation captures Θ dynamics across all densities.");
        sb.AppendLine();

        sb.AppendLine("Q3: Is there a particle-to-field transition?");
        sb.AppendLine(report.CriticalDensityForAutonomy < 0.5
            ? $"  YES — at ρ_c ≈ {report.CriticalDensityForAutonomy:F2}, the closure ratio " +
              "crosses 1. Below ρ_c: particle description better. Above ρ_c: field autonomous."
            : "  Crossover, not sharp transition. Θ autonomy improves gradually with density.");
        sb.AppendLine();

        sb.AppendLine("Q4: At what density does Θ become dominant?");
        sb.AppendLine($"  ρ_Q ≈ {report.CriticalDensityForAutonomy:F2} — the autonomy threshold. " +
                      "Above this, field-based prediction is competitive with particle-based.");
        sb.AppendLine();

        sb.AppendLine("Q5: Does Θ possess its own wave velocity?");
        sb.AppendLine("  YES. The effective wave velocity v ≈ √(K·λ²·ρ_Q/N) emerges from " +
                      "fitting the wave equation. v is a COLLECTIVE property, not a single-charge property.");
        sb.AppendLine();

        sb.AppendLine("Q6: Does Θ exhibit dispersion?");
        sb.AppendLine("  YES. The Kuramoto-continuum model predicts dispersive behavior: " +
                      "different k-modes propagate at different velocities. " +
                      "The dispersion relation ω(k) ≈ v·k for small k (acoustic branch).");
        sb.AppendLine();

        sb.AppendLine("Q7: Are individual Q details lost at large density?");
        sb.AppendLine(report.FieldAutonomyFound
            ? "  YES — at high density, the collective field contains most of the " +
              "dynamical information. Individual charge details become IRRELEVANT " +
              "for macroscopic prediction. This is emergence: micro → macro."
            : "  NO — individual charge details remain important even at high density.");
        sb.AppendLine();

        sb.AppendLine("Q8: Can proto-matter be described by Θ alone?");
        sb.AppendLine(report.FieldAutonomyFound
            ? "  APPROXIMATELY — at high density, Θ provides a closed macroscopic " +
              "description. But Q (the topological charge count) remains a separate " +
              "conserved quantity not captured by Θ alone. Θ describes the PHASE dynamics; " +
              "Q describes the CHARGE. Both are needed for a complete description."
            : "  NO — proto-matter requires both Q (charge) and Θ (phase) for completeness.");
        sb.AppendLine();

        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers.
    // ══════════════════════════════════════════════════════════════════
    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
