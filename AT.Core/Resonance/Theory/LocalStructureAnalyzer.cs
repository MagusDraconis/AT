using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Tests whether local condensate structures survive when the
/// mean-field theory predicts inevitable global synchronization.
/// Compares mean-field predictions against full spatial simulations.
///
/// AT-107: Local Structure Survival
/// </summary>
public static class LocalStructureAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record CondensateInfo(
        double CenterX, double CenterY,
        double LocalR,
        double LocalM,
        int OscillatorCount,
        bool IsAlive);  // local R > 0.7

    public sealed record SpatialSnapshot(
        int Iteration,
        double GlobalR,
        double GlobalM,
        double MeanFieldPredictedDR,
        double ActualDR,
        List<CondensateInfo> Condensates,
        int CondensateCount);

    public sealed record SpatialFieldProfile(
        string Scenario,
        int N,
        double K, double Lam,
        List<SpatialSnapshot> History,
        double MeanFieldError,     // mean |predicted dR - actual dR|
        double FinalGlobalR,
        int FinalCondensateCount,
        bool MeanFieldFailed);     // condensates survive while global R < 0.9

    public sealed record MeanFieldBreakdownReport(
        List<SpatialFieldProfile> Profiles,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Mean-field prediction (from AT-104)
    // ══════════════════════════════════════════════════════════════════

    private const double C0 = 0.0047;
    private static double PredictDR(double R, double M) =>
        C0 * M * R * (1.0 - R * R);

    // ══════════════════════════════════════════════════════════════════
    // Run spatial simulation with condensate detection
    // ══════════════════════════════════════════════════════════════════

    public static SpatialFieldProfile SimulateWithCondensates(
        string scenario, int n, double k, double lam, int seed,
        int totalSteps = 5000, int snapshotInterval = 200)
    {
        var net = new TemporalNetwork(n);
        var rng = new Random(seed);

        // Place oscillators based on scenario.
        switch (scenario)
        {
            case "single":
                // One tight cluster at center.
                for (int i = 0; i < n; i++)
                    net.AddNode(new TemporalNode(i,
                        rng.NextDouble() * 2 * Math.PI, 1.0)
                    { X = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
                      Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
                break;

            case "two":
                // Two separated clusters.
                for (int i = 0; i < n / 2; i++)
                    net.AddNode(new TemporalNode(i,
                        rng.NextDouble() * 2 * Math.PI, 1.0)
                    { X = Math.Clamp(0.2 + (rng.NextDouble() * 2 - 1) * 0.04, 0.01, 0.99),
                      Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.04, 0.01, 0.99) });
                for (int i = n / 2; i < n; i++)
                    net.AddNode(new TemporalNode(i,
                        rng.NextDouble() * 2 * Math.PI, 1.0)
                    { X = Math.Clamp(0.8 + (rng.NextDouble() * 2 - 1) * 0.04, 0.01, 0.99),
                      Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.04, 0.01, 0.99) });
                break;

            case "multi":
                // 5 small clusters.
                var centers = new[] { (0.2, 0.3), (0.5, 0.2), (0.8, 0.4), (0.3, 0.7), (0.7, 0.8) };
                for (int i = 0; i < n; i++)
                {
                    var (cx, cy) = centers[i % 5];
                    net.AddNode(new TemporalNode(i,
                        rng.NextDouble() * 2 * Math.PI, 1.0)
                    { X = Math.Clamp(cx + (rng.NextDouble() * 2 - 1) * 0.03, 0.01, 0.99),
                      Y = Math.Clamp(cy + (rng.NextDouble() * 2 - 1) * 0.03, 0.01, 0.99) });
                }
                break;

            default: // random
                for (int i = 0; i < n; i++)
                    net.AddNode(new TemporalNode(i,
                        rng.NextDouble() * 2 * Math.PI, 1.0)
                    { X = rng.NextDouble(), Y = rng.NextDouble() });
                break;
        }

        net.Matrix.FillSpatialCoupling(net.Nodes, k, lam, normalize: false);

        var history = new List<SpatialSnapshot>();
        double prevR = ComputeGlobalR(net);

        for (int step = 0; step <= totalSteps; step++)
        {
            if (step % snapshotInterval == 0)
            {
                double R = ComputeGlobalR(net);
                double M = ComputeGlobalM(net);
                double actualDR = step > 0 ? (R - prevR) / snapshotInterval : 0;
                double predDR = PredictDR(R, M);

                var condensates = DetectCondensates(net, k, lam);
                int aliveCount = condensates.Count(c => c.IsAlive);

                history.Add(new SpatialSnapshot(step, R, M, predDR, actualDR,
                    condensates, aliveCount));
                prevR = R;
            }

            if (step == totalSteps) break;

            // Phase step.
            PhaseStep(net);

            // Position step (slow coupling-energy gradient).
            PositionStep(net, 0.0005);

            // Recompute coupling.
            net.Matrix.FillSpatialCoupling(net.Nodes, k, lam, normalize: false);
        }

        double mfError = 0;
        int count = 0;
        for (int i = 1; i < history.Count; i++)
        {
            mfError += Math.Abs(history[i].MeanFieldPredictedDR - history[i].ActualDR);
            count++;
        }
        mfError /= Math.Max(count, 1);

        double finalR = history[^1].GlobalR;
        int finalCond = history[^1].CondensateCount;
        bool mfFailed = finalCond > 0 && finalR < 0.9;

        return new SpatialFieldProfile(scenario, n, k, lam, history,
            mfError, finalR, finalCond, mfFailed);
    }

    // ══════════════════════════════════════════════════════════════════
    // Condensate detection via spatial clustering
    // ══════════════════════════════════════════════════════════════════

    private static List<CondensateInfo> DetectCondensates(
        TemporalNetwork net, double k, double lam)
    {
        int n = net.NodeCount;
        double couplingRange = lam * 3.0; // effective coupling range
        var visited = new bool[n];
        var condensates = new List<CondensateInfo>();

        for (int i = 0; i < n; i++)
        {
            if (visited[i]) continue;

            // BFS to find connected cluster (within coupling range).
            var cluster = new List<int>();
            var queue = new Queue<int>();
            queue.Enqueue(i);
            visited[i] = true;

            while (queue.Count > 0)
            {
                int cur = queue.Dequeue();
                cluster.Add(cur);

                for (int j = 0; j < n; j++)
                {
                    if (visited[j]) continue;
                    double dx = net.Nodes[cur].X - net.Nodes[j].X;
                    double dy = net.Nodes[cur].Y - net.Nodes[j].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy);
                    double c = net.Matrix.GetCoupling(cur, j);

                    if (d < couplingRange && c > 0.01 * k)
                    {
                        visited[j] = true;
                        queue.Enqueue(j);
                    }
                }
            }

            // Compute local R within cluster.
            double ss = 0, sc = 0;
            foreach (int idx in cluster)
            { ss += Math.Sin(net.Nodes[idx].Phase); sc += Math.Cos(net.Nodes[idx].Phase); }
            double localR = Math.Sqrt(ss * ss + sc * sc) / cluster.Count;

            // Compute local M within cluster.
            double mSum = 0;
            int mPairs = 0;
            foreach (int a in cluster)
                foreach (int b in cluster)
                    if (a < b) { mSum += net.Matrix.GetCoupling(a, b); mPairs++; }
            double localM = mPairs > 0 ? mSum / mPairs : 0;

            double cx = cluster.Average(idx => net.Nodes[idx].X);
            double cy = cluster.Average(idx => net.Nodes[idx].Y);

            condensates.Add(new CondensateInfo(cx, cy, localR, localM,
                cluster.Count, localR > 0.7));
        }

        return condensates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis
    // ══════════════════════════════════════════════════════════════════

    public static MeanFieldBreakdownReport RunBreakdownAnalysis(int baseSeed = 107_000_001)
    {
        var profiles = new List<SpatialFieldProfile>();

        profiles.Add(SimulateWithCondensates("single", 100, 2.0, 0.05, baseSeed));
        profiles.Add(SimulateWithCondensates("two", 100, 2.0, 0.05, baseSeed + 1));
        profiles.Add(SimulateWithCondensates("multi", 100, 2.0, 0.05, baseSeed + 2));
        profiles.Add(SimulateWithCondensates("random", 100, 2.0, 0.05, baseSeed + 3));

        int mfFailures = profiles.Count(p => p.MeanFieldFailed);
        double avgError = profiles.Average(p => p.MeanFieldError);

        string classification;
        string interpretation;

        if (mfFailures >= 3)
        {
            classification = "D: Mean-Field Breakdown — Spatial Field Theory Required";
            interpretation =
                $"MEAN-FIELD THEORY BREAKS DOWN. {mfFailures}/{profiles.Count} scenarios show " +
                "persistent condensates despite mean-field prediction of synchronization. " +
                "The global variables {R, M} fail to capture spatial structure. " +
                "LOCAL condensates survive because:\n" +
                "  1. Spatial separation prevents inter-condensate coupling.\n" +
                "  2. Each condensate internally synchronizes (local R→1) but\n" +
                "     the global R remains low because condensates are phase-incoherent.\n" +
                "  3. The mean-field average over all oscillators LOSES the spatial\n" +
                "     information that allows structure to persist.\n\n" +
                $"Mean |predicted − actual dR/dt| = {avgError:F5}. " +
                "The mean-field equation does not describe multi-condensate systems.";
        }
        else if (mfFailures >= 1)
        {
            classification = "C: Strong Spatial Effects";
            interpretation =
                "Spatial effects are significant in some scenarios. " +
                "The mean-field theory works for single-condensate systems " +
                "but breaks down when multiple spatially-separated structures exist.";
        }
        else
        {
            classification = "B: Weak Spatial Corrections";
            interpretation =
                "The mean-field theory approximately captures the dynamics. " +
                "Spatial corrections are minor at N=100 scale.";
        }

        return new MeanFieldBreakdownReport(profiles, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulation helpers
    // ══════════════════════════════════════════════════════════════════

    private static void PhaseStep(TemporalNetwork net)
    {
        int n = net.NodeCount; double[] np = new double[n];
        for (int i = 0; i < n; i++)
        { double sum = 0; for (int j = 0; j < n; j++) if (i != j) sum += net.Matrix.GetCoupling(i, j) * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); np[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum)); }
        for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
    }

    private static void PositionStep(TemporalNetwork net, double step)
    {
        int n = net.NodeCount; double[] nx = new double[n], ny = new double[n];
        for (int i = 0; i < n; i++)
        { double fx = 0, fy = 0; for (int j = 0; j < n; j++) { if (i == j) continue; double dx = net.Nodes[j].X - net.Nodes[i].X, dy = net.Nodes[j].Y - net.Nodes[i].Y; double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10; double c = net.Matrix.GetCoupling(i, j) * Math.Cos(net.Nodes[j].Phase - net.Nodes[i].Phase) / d; fx += c * dx; fy += c * dy; } nx[i] = Math.Clamp(net.Nodes[i].X + step * fx, 0.01, 0.99); ny[i] = Math.Clamp(net.Nodes[i].Y + step * fy, 0.01, 0.99); }
        for (int i = 0; i < n; i++) { net.Nodes[i].X = nx[i]; net.Nodes[i].Y = ny[i]; }
    }

    private static double ComputeGlobalR(TemporalNetwork net)
    { double ss = 0, sc = 0; int n = net.NodeCount; for (int i = 0; i < n; i++) { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); } return Math.Sqrt(ss * ss + sc * sc) / n; }

    private static double ComputeGlobalM(TemporalNetwork net)
    { int n = net.NodeCount; double s = 0; int p = 0; for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++) { s += net.Matrix.GetCoupling(i, j); p++; } return s / p; }
}
