using AT.Core.Temporal;
using AT.Core.Resonance.Kuramoto;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Autonomous theory compression engine.
/// Searches for the minimal set of state variables and equations
/// that explain all major AT findings (AT-044 through AT-082).
///
/// AT-083: Minimal AT Physics
/// </summary>
public static class TheoryCompressionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A fitted equation within a theory candidate.
    /// </summary>
    public sealed record EquationFit(
        string Target,
        string[] Predictors,
        double R2,
        double AdjustedR2,
        double[] Coefficients);

    /// <summary>
    /// A candidate physical theory with state variables and equations.
    /// </summary>
    public sealed record PhysicsCandidate(
        string Name,
        string[] StateVariables,
        string[] DerivedQuantities,
        string[] FixedParameters,
        List<EquationFit> Equations,
        string Description);

    /// <summary>
    /// Score for a theory candidate.
    /// </summary>
    public sealed record TheoryScore(
        string Name,
        double MeanAdjR2,
        int NumStateVars,
        int NumFittedParams,
        double ComplexityPenalty,
        double TotalScore,
        string Rank);

    /// <summary>
    /// Full comparison of all candidates.
    /// </summary>
    public sealed record TheoryComparison(
        List<PhysicsCandidate> AllCandidates,
        List<TheoryScore> AllScores,
        PhysicsCandidate BestTheory,
        TheoryScore BestScore,
        string SearchPath,
        string[] DiscardedVariables,
        string[] RetainedVariables,
        double InformationLoss,
        string Classification);

    // ══════════════════════════════════════════════════════════════════
    // Data point for theory fitting
    // ══════════════════════════════════════════════════════════════════

    public sealed record TheoryDataPoint(
        double R,
        double dRdt,
        double M,
        double dMdt,
        double Variance,
        double Entropy,
        double SpectralGap,
        double MeanDegree,
        double SpatialClustering);

    // ══════════════════════════════════════════════════════════════════
    // Known facts and causal chains from AT-044 to AT-082
    // ══════════════════════════════════════════════════════════════════

    public static readonly string[] KnownCausalChains =
    {
        "Memory(β) → Curvature (AT-059, r=0.932) [Curvature does NOT drive motion, AT-068]",
        "Memory(β) is EXTERNAL, not emergent (AT-061)",
        "No memory-curvature feedback (AT-060)",
        "Identity ⟂ Energy (AT-047, r=0.06) — independent dimensions",
        "Identity survives ±25% energy band (AT-048), fully recoverable (AT-049)",
        "Identity does NOT transfer (AT-050) — identity exclusion",
        "Coherence IS conserved (AT-052), emergent consequence not causal root (AT-053)",
        "Single continuous attractor landscape (AT-056)",
        "Near-geodesic recovery trajectories (AT-057, 89.4% repeatability)",
        "Curvature exists (AT-058), memory-generated (AT-059)",
        "F_net = Alignment × ⟨f⟩ (AT-074, R²=0.989) — universal force law",
        "Alignment ≈ R² (AT-075, R²=0.942) — zero-parameter model",
        "dR/dt = f(R, M) (AT-081, R²=0.758) — M is dominant topology descriptor",
        "M compresses 97.7% of topology info (AT-081)",
        "dM/dt = f(M, R, M², R², MR) (AT-082, Adj R²=0.299) — M is effective field",
        "ASYMMETRIC: M→R strong (R²=0.758), R→M weak (Adj R²=0.299)",
    };

    // ══════════════════════════════════════════════════════════════════
    // Generate data for theory fitting
    // ══════════════════════════════════════════════════════════════════

    public static List<TheoryDataPoint> GenerateTheoryData(
        int numConfigs = 180, int baseSeed = 830491627,
        double k = 2.0, double lambda = 0.05, int n = 100)
    {
        // Generate topology ensemble (reuse AT-081 approach).
        var states = TopologyEvolutionAnalyzer.GenerateTopologyEnsemble(
            k, lambda, n, numConfigs, baseSeed);

        // Also generate temporal profiles for dM/dt data.
        var dMdtPoints = GenerateDMdtPoints(numConfigs / 6, baseSeed + 1, k, lambda, n);

        var points = new List<TheoryDataPoint>();

        // Merge static snapshots with dM/dt from temporal profiles.
        // For static snapshots, dM/dt is unknown (use temporal profile average).
        double meanDMdt = dMdtPoints.Count > 0 ? dMdtPoints.Average(p => p.dMdt) : 0;

        foreach (var s in states)
        {
            // Find matching temporal profile by topology type.
            var match = dMdtPoints
                .Where(p => Math.Abs(p.M - s.MeanCoupling) < 0.1 * Math.Max(s.MeanCoupling, 1e-6))
                .ToList();

            double dMdtVal = match.Count > 0 ? match.Average(p => p.dMdt) : meanDMdt;

            points.Add(new TheoryDataPoint(
                s.R, s.dRdt,
                s.MeanCoupling, dMdtVal,
                s.CouplingVariance, s.CouplingEntropy,
                s.SpectralGap, s.MeanDegree, s.SpatialClustering));
        }

        return points;
    }

    private static List<(double M, double dMdt)> GenerateDMdtPoints(
        int profiles, int baseSeed, double k, double lambda, int n)
    {
        var results = new List<(double M, double dMdt)>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        for (int p = 0; p < profiles; p++)
        {
            int seed = baseSeed + p * 7919;
            string type = types[p % types.Length];
            var profile = MeanCouplingFieldAnalyzer.SimulateProfile(
                type, k, lambda, n, seed, totalSteps: 200, snapshotInterval: 10);

            for (int i = 1; i < profile.M.Length; i++)
                results.Add((profile.M[i], profile.dMdt[i]));
        }

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Define candidate theories
    // ══════════════════════════════════════════════════════════════════

    public static List<PhysicsCandidate> GenerateCandidates()
    {
        var candidates = new List<PhysicsCandidate>();

        // Theory A: R only — coherence alone.
        candidates.Add(new PhysicsCandidate(
            "A", new[] { "R" }, new[] { "Alignment≈R²", "Force≈A·⟨f⟩" },
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "Coherence R as sole state variable. Force and alignment are derived. " +
            "Topology is external (not state). Memory is external parameter."));

        // Theory B: M only — mean coupling alone.
        candidates.Add(new PhysicsCandidate(
            "B", new[] { "M" }, Array.Empty<string>(),
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "MeanCoupling M as sole state variable. " +
            "R is emergent from M. Minimal topology-first theory."));

        // Theory C: {R, M} — the two-variable theory.
        candidates.Add(new PhysicsCandidate(
            "C", new[] { "R", "M" }, new[] { "Alignment≈R²", "Force≈A·⟨f⟩" },
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "TWO-VARIABLE THEORY: State = {R, M}. " +
            "R captures phase coherence. M captures effective topology. " +
            "Two-way coupling: M→R strong, R→M weak."));

        // Theory D: {R, M, Alignment} — redundant by construction (A≈R²).
        candidates.Add(new PhysicsCandidate(
            "D", new[] { "R", "M", "A" }, Array.Empty<string>(),
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "Three-variable: adds explicit Alignment A. " +
            "Hypothesis: A carries info beyond R. Expected: REDUNDANT (A≈R²)."));

        // Theory E: {R, M, Variance} — does coupling heterogeneity matter?
        candidates.Add(new PhysicsCandidate(
            "E", new[] { "R", "M", "V" }, Array.Empty<string>(),
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "Three-variable: adds CouplingVariance V. " +
            "Hypothesis: coupling heterogeneity carries independent info. " +
            "Expected: WEAK (V≈M², redundant)."));

        // Theory F: {R, M, Entropy} — coupling information.
        candidates.Add(new PhysicsCandidate(
            "F", new[] { "R", "M", "S" }, Array.Empty<string>(),
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "Three-variable: adds CouplingEntropy S. " +
            "Hypothesis: coupling diversity matters. " +
            "Expected: REDUNDANT (S ∝ M, AT-081 corr>0.99)."));

        // Theory G: {R, M, SpectralGap} — spectral topology.
        candidates.Add(new PhysicsCandidate(
            "G", new[] { "R", "M", "G" }, Array.Empty<string>(),
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "Three-variable: adds SpectralGap G. " +
            "Hypothesis: spectral properties capture network structure. " +
            "Expected: WEAK (G poorly correlated with dR/dt)."));

        // Theory H: {R, M} + Memory as state — is β a state variable?
        candidates.Add(new PhysicsCandidate(
            "H", new[] { "R", "M", "β" }, new[] { "Curvature∝β" },
            new[] { "K", "λ", "N" },
            new List<EquationFit>(),
            "Three-variable: promotes Memory β to state variable. " +
            "Hypothesis: β variation governs curvature and recovery. " +
            "Expected: β is EXTERNAL parameter, not state (AT-061)."));

        // Theory I: Full — all non-redundant variables.
        candidates.Add(new PhysicsCandidate(
            "I", new[] { "R", "M", "V", "S", "G" }, new[] { "Alignment≈R²", "Force≈A·⟨f⟩" },
            new[] { "β", "K", "λ", "N" },
            new List<EquationFit>(),
            "FULL MODEL: all measured topology variables. " +
            "Reference for information loss computation."));

        return candidates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Fit equations for a candidate theory
    // ══════════════════════════════════════════════════════════════════

    public static PhysicsCandidate FitTheory(
        PhysicsCandidate candidate, List<TheoryDataPoint> data)
    {
        var equations = new List<EquationFit>();
        int n = data.Count;

        // Map variable names to data arrays.
        var varMap = new Dictionary<string, double[]>
        {
            ["R"] = data.Select(d => d.R).ToArray(),
            ["M"] = data.Select(d => d.M).ToArray(),
            ["A"] = data.Select(d => d.R * d.R).ToArray(),  // A ≈ R²
            ["V"] = data.Select(d => d.Variance).ToArray(),
            ["S"] = data.Select(d => d.Entropy).ToArray(),
            ["G"] = data.Select(d => d.SpectralGap).ToArray(),
            ["D"] = data.Select(d => d.MeanDegree).ToArray(),
            ["C"] = data.Select(d => d.SpatialClustering).ToArray(),
        };

        var stateVars = candidate.StateVariables.ToHashSet();

        // Equation 1: dR/dt = f(state_vars)
        // Target: dR/dt. Predictors: all state variables.
        {
            var predictors = candidate.StateVariables
                .Where(v => varMap.ContainsKey(v))
                .Select(v => varMap[v])
                .ToArray();

            if (predictors.Length > 0)
            {
                double[] target = data.Select(d => d.dRdt).ToArray();
                var fit = FitLinearModel("dR/dt", candidate.StateVariables, predictors, target);
                equations.Add(fit);
            }
        }

        // Equation 2: dM/dt = f(state_vars) — if M is a state variable.
        if (stateVars.Contains("M"))
        {
            var predictors = candidate.StateVariables
                .Where(v => varMap.ContainsKey(v))
                .Select(v => varMap[v])
                .ToArray();

            if (predictors.Length > 0)
            {
                double[] target = data.Select(d => d.dMdt).ToArray();
                var fit = FitLinearModel("dM/dt", candidate.StateVariables, predictors, target);
                equations.Add(fit);
            }
        }

        // Equation 3: If Alignment A is a state variable, verify A ≈ R².
        if (stateVars.Contains("A"))
        {
            double[] aVals = data.Select(d => d.R * d.R).ToArray();
            double[] aTarget = data.Select(d => d.R * d.R).ToArray(); // self-prediction
            var fitA = FitLinearModel("A(R)", new[] { "R²" },
                new[] { data.Select(d => d.R * d.R).ToArray() }, aTarget);
            equations.Add(new EquationFit("A≈R²", new[] { "R²" },
                fitA.R2, fitA.AdjustedR2, new[] { 0.0, 1.0 }));
        }

        return candidate with { Equations = equations };
    }

    private static EquationFit FitLinearModel(
        string target, string[] predNames, double[][] predictors, double[] Y)
    {
        int n = Y.Length;
        int p = predictors.Length;
        int m = p + 1;

        double[,] XTX = new double[m, m];
        double[] XTY = new double[m];
        for (int i = 0; i < n; i++)
        {
            double[] f = new double[m];
            f[0] = 1.0;
            for (int j = 0; j < p; j++)
                f[j + 1] = predictors[j][i];
            for (int a = 0; a < m; a++)
            {
                XTY[a] += f[a] * Y[i];
                for (int b = 0; b < m; b++)
                    XTX[a, b] += f[a] * f[b];
            }
        }

        double[] beta = SolveGauss(XTX, XTY, m);

        double ssRes = 0, ssTot = 0, meanY = Y.Average();
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            for (int j = 0; j < p; j++)
                pred += beta[j + 1] * predictors[j][i];
            double err = Y[i] - pred;
            ssRes += err * err;
            ssTot += (Y[i] - meanY) * (Y[i] - meanY);
        }

        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0.0;
        double adjR2 = n > m ? 1.0 - (1.0 - r2) * (n - 1) / (n - m) : r2;

        return new EquationFit(target, predNames, r2, adjR2, beta);
    }

    // ══════════════════════════════════════════════════════════════════
    // Score a theory
    // ══════════════════════════════════════════════════════════════════

    public static TheoryScore ScoreTheory(PhysicsCandidate theory)
    {
        if (theory.Equations.Count == 0)
            return new TheoryScore(theory.Name, 0, theory.StateVariables.Length,
                0, 0, 0, "No equations fitted");

        double meanAdjR2 = theory.Equations.Average(e => e.AdjustedR2);
        int numVars = theory.StateVariables.Length;
        int numParams = theory.Equations.Sum(e => e.Coefficients.Length);

        // Complexity penalty: 0.02 per state variable + 0.005 per fitted parameter.
        double penalty = 0.02 * numVars + 0.005 * numParams;
        double score = Math.Max(0, meanAdjR2 - penalty);

        string rank = score >= 0.50 ? "A: Excellent" :
                      score >= 0.30 ? "B: Good" :
                      score >= 0.15 ? "C: Moderate" :
                      score >= 0.05 ? "D: Weak" : "E: Poor";

        return new TheoryScore(theory.Name, meanAdjR2, numVars, numParams,
            penalty, score, rank);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full theory comparison
    // ══════════════════════════════════════════════════════════════════

    public static TheoryComparison CompareTheories(
        List<PhysicsCandidate> candidates, List<TheoryDataPoint> data)
    {
        // Fit all candidates.
        var fitted = new List<PhysicsCandidate>();
        foreach (var c in candidates)
            fitted.Add(FitTheory(c, data));

        // Score all.
        var scores = fitted.Select(ScoreTheory)
            .OrderByDescending(s => s.TotalScore)
            .ToList();

        var best = fitted.First(c => c.Name == scores[0].Name);
        var bestScore = scores[0];

        // Redundancy analysis.
        var fullTheory = fitted.First(c => c.Name == "I");
        double fullR2 = ScoreTheory(fullTheory).MeanAdjR2;
        double bestR2 = bestScore.MeanAdjR2;
        double infoLoss = fullR2 > 1e-10 ? 1.0 - bestR2 / fullR2 : 0;

        // Which variables were discarded?
        var fullVars = fullTheory.StateVariables.ToHashSet();
        var bestVars = best.StateVariables.ToHashSet();
        var discarded = fullVars.Except(bestVars).ToArray();
        var retained = bestVars.Intersect(fullVars).ToArray();

        // Search path description.
        var path = new System.Text.StringBuilder();
        path.AppendLine($"Started with {candidates.Count} candidates across {fullVars.Count} candidate variables.");
        path.AppendLine($"Best theory: {best.Name} ({best.Description.Split('.')[0]})");
        path.AppendLine($"Score: {bestScore.TotalScore:F3} (Adj R²={bestScore.MeanAdjR2:F3}, penalty={bestScore.ComplexityPenalty:F3})");
        path.AppendLine($"Variables: {string.Join(", ", best.StateVariables)} → {best.Equations.Count} equations");
        foreach (var eq in best.Equations)
            path.AppendLine($"  {eq.Target} = f({string.Join(", ", eq.Predictors)}), Adj R² = {eq.AdjustedR2:F4}");

        // Classification.
        string classification;
        if (best.StateVariables.Length <= 2 && bestScore.TotalScore >= 0.30)
            classification = "D: Candidate Emergent Physics";
        else if (best.StateVariables.Length <= 3 && bestScore.TotalScore >= 0.20)
            classification = "C: Unified Reduced Theory";
        else if (bestScore.TotalScore >= 0.10)
            classification = "B: Partial Theory";
        else
            classification = "A: No Coherent Theory";

        return new TheoryComparison(fitted, scores, best, bestScore,
            path.ToString(), discarded, retained, infoLoss, classification);
    }

    // ══════════════════════════════════════════════════════════════════
    // Gaussian elimination
    // ══════════════════════════════════════════════════════════════════

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = A[i, j];
            M[i, n] = b[i];
        }

        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col]))
                    maxRow = row;
            for (int j = col; j <= n; j++)
                (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            {
                double f = M[row, col] / M[col, col];
                for (int j = col; j <= n; j++)
                    M[row, j] -= f * M[col, j];
            }
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double s = M[i, n];
            for (int j = i + 1; j < n; j++)
                s -= M[i, j] * x[j];
            x[i] = Math.Abs(M[i, i]) > 1e-15 ? s / M[i, i] : 0;
        }
        return x;
    }
}
