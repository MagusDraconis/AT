using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether memory (path dependence) can emerge spontaneously
/// from repeated resonance dynamics at beta=0.
/// </summary>
public static class SpontaneousMemoryAnalyzer
{
    public sealed record EmergenceProfile(
        double Beta, int Cycles, double PathDependenceDistance,
        double IdentityPersistence, double Curvature, double MemScore, int Seed);

    public sealed record EmergenceReport(
        List<EmergenceProfile> Profiles, double MeanPathDependence,
        double MeanCurvature, double MeanMemoryScore,
        string EmergenceClass, string Description);

    private static double Mem(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++)
            { double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); sum += Math.Abs(s); sumSq += s * s; c++; }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    private static (double r, double f, double v) Fingerprint(TemporalNetwork net)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        return (m.OrderParameterR, net.Nodes.Average(n => n.Frequency), m.PhaseVariance);
    }

    private static double IdDist((double r, double f, double v) a, (double r, double f, double v) b)
    {
        double dr = (a.r - b.r), df = (a.f - b.f) / 3.0, dv = (a.v - b.v);
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    private static TemporalNetwork BuildNet(int n, double k, double lambda, int seed)
    {
        var rng = new Random(seed);
        var net = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
            net.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5) { X = rng.NextDouble(), Y = rng.NextDouble() });
        net.Matrix.FillSpatialCoupling(net.Nodes, k, lambda, normalize: false);
        return net;
    }

    public static EmergenceProfile TestEmergence(
        double beta, int cycles, double k, double lambda, int n, int seed)
    {
        // Path dependence: AB vs BA.
        var netAB = BuildNet(n, k, lambda, seed);
        var simAB = new MemoryTemporalSimulation(netAB, beta);
        simAB.Run(1500);
        for (int c = 0; c < cycles; c++)
        {
            foreach (var node in netAB.Nodes) node.Phase += 0.4;
            simAB.Run(100);
            foreach (var node in netAB.Nodes) node.Phase -= 0.4;
            simAB.Run(100);
        }
        var fpAB = Fingerprint(netAB);
        double memAB = Mem(netAB);

        var netBA = BuildNet(n, k, lambda, seed);
        var simBA = new MemoryTemporalSimulation(netBA, beta);
        simBA.Run(1500);
        for (int c = 0; c < cycles; c++)
        {
            foreach (var node in netBA.Nodes) node.Phase -= 0.4;
            simBA.Run(100);
            foreach (var node in netBA.Nodes) node.Phase += 0.4;
            simBA.Run(100);
        }
        var fpBA = Fingerprint(netBA);
        double memBA = Mem(netBA);

        double pathDep = IdDist(fpAB, fpBA);
        double idPersist = 1.0 / (1.0 + pathDep * 5);
        double curv = pathDep;
        double memScore = (memAB + memBA) / 2;

        return new EmergenceProfile(beta, cycles, pathDep, idPersist, curv, memScore, seed);
    }

    public static EmergenceReport AnalyzeEmergence(List<EmergenceProfile> profiles)
    {
        double meanPD = profiles.Average(p => p.PathDependenceDistance);
        double meanCurv = profiles.Average(p => p.Curvature);
        double meanMem = profiles.Average(p => p.MemScore);

        var beta0 = profiles.Where(p => p.Beta < 0.01).ToList();
        var betaPos = profiles.Where(p => p.Beta > 0.01).ToList();

        double pdRatio = betaPos.Count > 0 && beta0.Count > 0
            ? beta0.Average(p => p.PathDependenceDistance) / Math.Max(betaPos.Average(p => p.PathDependenceDistance), 1e-10) : 0;

        string emergClass = pdRatio > 0.5 ? "C: Strongly Emergent" :
                            pdRatio > 0.2 ? "B: Weakly Emergent" : "A: Purely External";
        string desc = pdRatio > 0.3
            ? $"Path dependence at beta=0 is {pdRatio:P0} of beta>0 level"
            : $"Path dependence at beta=0 is only {pdRatio:P0} of beta>0 level";

        return new EmergenceReport(profiles, meanPD, meanCurv, meanMem, emergClass, desc);
    }
}
