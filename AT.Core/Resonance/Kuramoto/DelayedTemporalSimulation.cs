using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Kuramoto simulation with finite propagation speed.
/// Each oscillator pair (i,j) has a delay τᵢⱼ = distance / propagationSpeed.
/// The coupling term uses phaseⱼ(t − τᵢⱼ) instead of phaseⱼ(t).
/// </summary>
public sealed class DelayedTemporalSimulation
{
    private readonly TemporalNetwork _network;
    private readonly double[,] _delays;
    private readonly double[][] _phaseHistory;
    private readonly int _historySize;
    private int _historyWriteIndex;
    private int _totalSteps;

    public double TimeStep { get; set; } = 0.01;
    public double CouplingStrength { get; set; }
    public int CurrentIteration => _totalSteps;
    public double ElapsedTime => _totalSteps * TimeStep;
    public double PropagationSpeed { get; }

    public DelayedTemporalSimulation(TemporalNetwork network, double propagationSpeed)
    {
        _network = network ?? throw new ArgumentNullException(nameof(network));
        PropagationSpeed = propagationSpeed;
        CouplingStrength = network.NodeCount;

        int n = network.NodeCount;
        _delays = new double[n, n];

        // Precompute delays.
        double maxDelay = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double dx = network.Nodes[i].X - network.Nodes[j].X;
                double dy = network.Nodes[i].Y - network.Nodes[j].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                double delay = double.IsInfinity(propagationSpeed) ? 0 : dist / propagationSpeed;
                _delays[i, j] = delay;
                if (delay > maxDelay) maxDelay = delay;
            }
        }

        // Initialize history buffer: enough for max delay + 1.
        _historySize = (int)(maxDelay / TimeStep) + 2;
        if (_historySize < 2) _historySize = 2;

        _phaseHistory = new double[n][];
        for (int i = 0; i < n; i++)
        {
            _phaseHistory[i] = new double[_historySize];
            _phaseHistory[i][0] = network.Nodes[i].Phase;
        }
    }

    /// <summary>
    /// Advances one time step using delayed coupling.
    /// </summary>
    public void Step()
    {
        int n = _network.NodeCount;

        // Store current phases in history.
        int writeSlot = (_historyWriteIndex + 1) % _historySize;
        for (int i = 0; i < n; i++)
            _phaseHistory[i][writeSlot] = _network.Nodes[i].Phase;

        double[] newPhases = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0.0;
            double thetaI = _network.Nodes[i].Phase;

            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;

                double delay = _delays[i, j];
                int stepsBack = (int)(delay / TimeStep);
                if (stepsBack > _totalSteps) stepsBack = _totalSteps;
                if (stepsBack >= _historySize - 1) stepsBack = _historySize - 2;

                int readSlot = (writeSlot - stepsBack + _historySize) % _historySize;
                double thetaJdelayed = _phaseHistory[j][readSlot];

                double coupling = _network.Matrix.GetCoupling(i, j);
                sum += coupling * Math.Sin(thetaJdelayed - thetaI);
            }

            double dTheta = _network.Nodes[i].Frequency + (CouplingStrength / n) * sum;
            newPhases[i] = TemporalSimulation.NormalizePhase(thetaI + TimeStep * dTheta);
        }

        for (int i = 0; i < n; i++)
            _network.Nodes[i].Phase = newPhases[i];

        _historyWriteIndex = writeSlot;
        _totalSteps++;
    }

    /// <summary>
    /// Runs the simulation for the specified number of iterations.
    /// </summary>
    public void Run(int iterations)
    {
        for (int i = 0; i < iterations; i++)
            Step();
    }
}
