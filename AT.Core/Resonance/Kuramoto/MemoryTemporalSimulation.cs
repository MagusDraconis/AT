using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Kuramoto simulation with historical memory — coupling includes
/// an exponential moving average of past phase differences.
/// </summary>
public sealed class MemoryTemporalSimulation
{
    private readonly TemporalNetwork _network;
    private readonly double[,] _memory; // Mᵢⱼ memory term
    private readonly double _alpha;     // memory decay rate
    private readonly double _beta;      // memory strength

    public double TimeStep { get; set; } = 0.01;
    public double CouplingStrength { get; set; }
    public int CurrentIteration { get; private set; }

    public MemoryTemporalSimulation(TemporalNetwork network, double beta, double alpha = 0.9)
    {
        _network = network;
        _beta = beta;
        _alpha = alpha;
        CouplingStrength = network.NodeCount;
        _memory = new double[network.NodeCount, network.NodeCount];
    }

    public void Step()
    {
        int n = _network.NodeCount;
        double[] newPhases = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0.0;
            double thetaI = _network.Nodes[i].Phase;

            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double coupling = _network.Matrix.GetCoupling(i, j);
                double sinTerm = Math.Sin(_network.Nodes[j].Phase - thetaI);
                sum += coupling * (sinTerm + _beta * _memory[i, j]);
            }

            double dTheta = _network.Nodes[i].Frequency + (CouplingStrength / n) * sum;
            newPhases[i] = TemporalSimulation.NormalizePhase(thetaI + TimeStep * dTheta);
        }

        // Update memory: exponential moving average of sin(Δθ).
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double sinTerm = Math.Sin(_network.Nodes[j].Phase - _network.Nodes[i].Phase);
                _memory[i, j] = _alpha * _memory[i, j] + (1 - _alpha) * sinTerm;
            }
        }

        for (int i = 0; i < n; i++)
            _network.Nodes[i].Phase = newPhases[i];

        CurrentIteration++;
    }

    public void Run(int iterations)
    {
        for (int i = 0; i < iterations; i++) Step();
    }

    /// <summary>
    /// Zeros the memory between two oscillators.
    /// </summary>
    public void ZeroMemory(int i, int j)
    {
        _memory[i, j] = 0;
    }
}
