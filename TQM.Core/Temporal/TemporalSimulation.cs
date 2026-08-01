using System.Diagnostics;

namespace TQM.Core.Temporal;

/// <summary>
/// Executes Kuramoto-style temporal simulations over a network of oscillators.
/// </summary>
public sealed class TemporalSimulation
{
    private readonly TemporalNetwork _network;

    public double TimeStep { get; set; } = 0.01;
    public double CouplingStrength { get; set; } = 1.0;
    public int CurrentIteration { get; private set; }
    public double ElapsedTime => CurrentIteration * TimeStep;

    public TemporalSimulation(TemporalNetwork network)
    {
        _network = network ?? throw new ArgumentNullException(nameof(network));
    }

    /// <summary>
    /// Performs a single Kuramoto update step for all nodes.
    /// dθᵢ/dt = ωᵢ + (K/N) · Σⱼ Kᵢⱼ · sin(θⱼ − θᵢ)
    /// </summary>
    public void Step()
    {
        int n = _network.NodeCount;
        if (n == 0) return;

        double[] newPhases = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0.0;
            double thetaI = _network.Nodes[i].Phase;

            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double coupling = _network.Matrix.GetCoupling(i, j);
                double thetaJ = _network.Nodes[j].Phase;
                sum += coupling * Math.Sin(thetaJ - thetaI);
            }

            double dTheta = _network.Nodes[i].Frequency + (CouplingStrength / n) * sum;
            newPhases[i] = thetaI + TimeStep * dTheta;
        }

        for (int i = 0; i < n; i++)
        {
            _network.Nodes[i].Phase = NormalizePhase(newPhases[i]);
        }

        CurrentIteration++;
    }

    /// <summary>
    /// Runs the simulation for a specified number of iterations.
    /// Returns execution time in milliseconds.
    /// </summary>
    public long Run(int iterations)
    {
        if (iterations < 0)
            throw new ArgumentOutOfRangeException(nameof(iterations), "Iterations must be non-negative.");

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
            Step();

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    /// <summary>
    /// Runs the simulation and collects snapshot metrics at each iteration.
    /// </summary>
    public List<SynchronizationMetrics> RunWithMetrics(int iterations)
    {
        var metrics = new List<SynchronizationMetrics>(iterations + 1);

        metrics.Add(SynchronizationMetrics.FromNetwork(_network, 0));

        for (int i = 0; i < iterations; i++)
        {
            Step();
            metrics.Add(SynchronizationMetrics.FromNetwork(_network, i + 1));
        }

        return metrics;
    }

    /// <summary>
    /// Normalizes a phase angle to the range [0, 2π).
    /// </summary>
    public static double NormalizePhase(double phase)
    {
        double twoPi = 2.0 * Math.PI;
        phase %= twoPi;
        if (phase < 0)
            phase += twoPi;
        return phase;
    }
}
