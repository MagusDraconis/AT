using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether effective spatial attraction is universal across
/// different phase-coupling force laws or specific to cos(Δθ).
/// </summary>
public static class CouplingUniversalityAnalyzer
{
    public sealed record LawProfile(
        string LawName,
        double SeparationLambda,
        double InitialSep, double FinalSep, double SeparationChange,
        double FinalRA, double FinalRB,
        bool Converges, bool Synchronizes,
        double AttractionScore, double SyncScore, int Seed);

    public sealed record UniversalityReport(
        List<LawProfile> Profiles,
        double ConvergenceFraction,
        double SyncFraction,
        double MeanAttractionScore,
        string Classification);

    // ── Coupling force laws (for position dynamics) ──────────────────

    private static readonly Dictionary<string, Func<double, double>> ForceLaws = new()
    {
        ["K1: cos(Δθ)"]        = d => Math.Cos(d),
        ["K2: sin(Δθ)"]        = d => Math.Sin(d),
        ["K3: cos²(Δθ)"]       = d => Math.Cos(d) * Math.Cos(d),
        ["K4: exp(-|Δθ|)"]     = d => Math.Exp(-Math.Abs(d)),
        ["K5: 1/(1+|Δθ|)"]     = d => 1.0 / (1.0 + Math.Abs(d)),
        ["K6: cos*exp(-|Δθ|)"] = d => Math.Cos(d) * Math.Exp(-Math.Abs(d)),
        ["K7: sign(cos(Δθ))"]  = d => Math.Sign(Math.Cos(d)),
        ["K8: 1-|Δθ|/π"]       = d => 1.0 - Math.Abs(d) / Math.PI,
    };

    // ── Run ──────────────────────────────────────────────────────────

    public static LawProfile RunCouplingLaw(
        string lawName, double sepLambda, double beta, double k, double lambda,
        int nPerGroup, int seed, int totalIters = 2000)
    {
        var forceFn = ForceLaws[lawName];
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        double sep = sepLambda * lambda;

        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });
        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(nPerGroup + i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = Math.Clamp(0.3 + sep + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        double initSep = GroupSeparation(network, nPerGroup);

        for (int iter = 0; iter < totalIters; iter++)
        {
            // Phase update.
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }

            // Position update using specified force law.
            double[] nx = new double[n], ny = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx = 0, fy = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double dx = network.Nodes[j].X - network.Nodes[i].X;
                    double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                    double w = network.Matrix.GetCoupling(i, j);
                    double phaseDiff = TemporalSimulation.NormalizePhase(
                        network.Nodes[j].Phase - network.Nodes[i].Phase);
                    // Map to [-π, π].
                    if (phaseDiff > Math.PI) phaseDiff -= 2 * Math.PI;
                    double force = forceFn(phaseDiff);
                    fx += w * force * dx / d;
                    fy += w * force * dy / d;
                }
                nx[i] = Math.Clamp(network.Nodes[i].X + 0.001 * fx, 0.01, 0.99);
                ny[i] = Math.Clamp(network.Nodes[i].Y + 0.001 * fy, 0.01, 0.99);
            }
            for (int i = 0; i < n; i++)
            { network.Nodes[i].X = nx[i]; network.Nodes[i].Y = ny[i]; }
        }

        double finalSep = GroupSeparation(network, nPerGroup);
        double rA = GroupR(network, 0, nPerGroup);
        double rB = GroupR(network, nPerGroup, nPerGroup);

        bool converges = finalSep < initSep * 0.95;
        bool syncs = rA > 0.8 && rB > 0.8;
        double attrScore = Math.Clamp((initSep - finalSep) / Math.Max(initSep, 1e-10), -1, 1);
        double syncScore = (rA + rB) / 2;

        return new LawProfile(lawName, sepLambda, initSep, finalSep,
            finalSep - initSep, rA, rB, converges, syncs, attrScore, syncScore, seed);
    }

    private static double GroupSeparation(TemporalNetwork net, int nPerGroup)
    {
        double cxA = 0, cyA = 0, cXB = 0, cYB = 0;
        for (int i = 0; i < nPerGroup; i++) { cxA += net.Nodes[i].X; cyA += net.Nodes[i].Y; }
        for (int i = 0; i < nPerGroup; i++) { cXB += net.Nodes[i + nPerGroup].X; cYB += net.Nodes[i + nPerGroup].Y; }
        cxA /= nPerGroup; cXB /= nPerGroup; cyA /= nPerGroup; cYB /= nPerGroup;
        return Math.Sqrt((cxA - cXB) * (cxA - cXB) + (cyA - cYB) * (cyA - cYB));
    }

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double ss = 0, sc = 0;
        for (int i = start; i < start + count; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / count;
    }

    public static UniversalityReport AnalyzeUniversality(List<LawProfile> profiles)
    {
        double convFrac = (double)profiles.Count(p => p.Converges) / profiles.Count;
        double syncFrac = (double)profiles.Count(p => p.Synchronizes) / profiles.Count;
        double meanAttr = profiles.Average(p => p.AttractionScore);

        string cls = convFrac > 0.8 ? "D: Universal Consequence of Synchronization" :
                     convFrac > 0.5 ? "C: Strongly Robust" :
                     convFrac > 0.3 ? "B: Weakly Robust" : "A: Coupling Artifact";

        return new UniversalityReport(profiles, convFrac, syncFrac, meanAttr, cls);
    }
}
