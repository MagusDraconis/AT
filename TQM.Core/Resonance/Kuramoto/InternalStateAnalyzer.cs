using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes internal phase structure of resonance condensates:
/// winding number, circulation direction, and state persistence.
/// </summary>
public sealed class InternalStateAnalyzer
{
    /// <summary>
    /// Computes the topological winding number of oscillator phases
    /// around the condensate center (cx, cy).
    /// Winding = Σ Δθᵢ / 2π where Δθᵢ are phase differences between
    /// consecutive oscillators sorted by angle around the center.
    /// </summary>
    public static double ComputeWindingNumber(TemporalNetwork network, double cx, double cy)
    {
        var nodes = network.Nodes;

        // Get oscillators sorted by angle around center.
        var sorted = nodes
            .Select(n => (Node: n, Angle: Math.Atan2(n.Y - cy, n.X - cx)))
            .OrderBy(x => x.Angle)
            .ToList();

        if (sorted.Count < 3) return 0;

        double totalDelta = 0;
        for (int i = 0; i < sorted.Count; i++)
        {
            int j = (i + 1) % sorted.Count;
            double delta = TemporalSimulation.NormalizePhase(
                sorted[j].Node.Phase - sorted[i].Node.Phase + Math.PI) - Math.PI;
            totalDelta += delta;
        }

        return totalDelta / (2.0 * Math.PI);
    }

    /// <summary>
    /// Classifies the internal state based on winding number and phase gradient.
    /// </summary>
    public static InternalResonanceStateType ClassifyState(double windingNumber, double phaseGradient)
    {
        double absWinding = Math.Abs(windingNumber);

        if (absWinding < 0.15)
            return InternalResonanceStateType.Uniform;

        if (absWinding > 0.85 && absWinding < 1.15)
        {
            if (windingNumber > 0)
                return phaseGradient > 0.1
                    ? InternalResonanceStateType.WindingPositive
                    : InternalResonanceStateType.Clockwise;
            else
                return phaseGradient > 0.1
                    ? InternalResonanceStateType.WindingNegative
                    : InternalResonanceStateType.CounterClockwise;
        }

        if (phaseGradient < 0.05)
            return InternalResonanceStateType.Defect;

        return InternalResonanceStateType.Unknown;
    }

    /// <summary>
    /// Computes the mean phase gradient magnitude across all oscillator pairs in the network.
    /// </summary>
    public static double ComputePhaseGradient(TemporalNetwork network)
    {
        var nodes = network.Nodes;
        int n = nodes.Count;
        if (n < 2) return 0;

        double totalGrad = 0;
        int pairs = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dx = nodes[i].X - nodes[j].X;
                double dy = nodes[i].Y - nodes[j].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist < 1e-10) continue;

                double dPhase = Math.Abs(TemporalSimulation.NormalizePhase(
                    nodes[i].Phase - nodes[j].Phase + Math.PI) - Math.PI);

                totalGrad += dPhase / dist;
                pairs++;
            }
        }

        return pairs > 0 ? totalGrad / pairs : 0;
    }

    /// <summary>
    /// Runs an internal state persistence experiment:
    /// 1. Initialize oscillators with a specific phase pattern.
    /// 2. Run simulation and track whether the state survives.
    /// </summary>
    public static InternalStateResult Analyze(
        TemporalNetwork network, TemporalSimulation sim,
        InternalResonanceStateType initialState,
        int totalIterations, int checkpointInterval = 250)
    {
        double cx = 0.5, cy = 0.5; // assume centered

        double initialWinding = ComputeWindingNumber(network, cx, cy);
        double initialGradient = ComputePhaseGradient(network);

        int decayIteration = -1;
        InternalResonanceStateType currentState = initialState;
        double finalWinding = initialWinding;
        double finalCoherence = 0;

        for (int iter = 0; iter < totalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == totalIterations - 1)
            {
                finalWinding = ComputeWindingNumber(network, cx, cy);
                double gradient = ComputePhaseGradient(network);
                currentState = ClassifyState(finalWinding, gradient);

                var metrics = SynchronizationMetrics.FromNetwork(network, iter + 1);
                finalCoherence = metrics.OrderParameterR;

                if (decayIteration < 0 && currentState != initialState)
                    decayIteration = iter + 1;
            }
        }

        bool preserved = currentState == initialState;

        return new InternalStateResult(
            initialState, currentState, finalWinding,
            preserved, decayIteration, finalCoherence, initialGradient);
    }

    /// <summary>
    /// Initializes oscillator phases with a clockwise phase circulation pattern.
    /// Phase(θ) = θ (angle around center).
    /// </summary>
    public static void InitializeClockwise(TemporalNetwork network, double cx, double cy)
    {
        foreach (var node in network.Nodes)
        {
            double angle = Math.Atan2(node.Y - cy, node.X - cx);
            node.Phase = TemporalSimulation.NormalizePhase(angle);
        }
    }

    /// <summary>
    /// Initializes oscillator phases with counter-clockwise circulation.
    /// Phase(θ) = -θ.
    /// </summary>
    public static void InitializeCounterClockwise(TemporalNetwork network, double cx, double cy)
    {
        foreach (var node in network.Nodes)
        {
            double angle = Math.Atan2(node.Y - cy, node.X - cx);
            node.Phase = TemporalSimulation.NormalizePhase(-angle);
        }
    }

    /// <summary>
    /// Initializes with a phase winding of ±2π around the center.
    /// </summary>
    public static void InitializeWinding(TemporalNetwork network, double cx, double cy, int winding)
    {
        foreach (var node in network.Nodes)
        {
            double angle = Math.Atan2(node.Y - cy, node.X - cx);
            node.Phase = TemporalSimulation.NormalizePhase(winding * angle);
        }
    }

    /// <summary>
    /// Initializes with uniform random phases (baseline).
    /// </summary>
    public static void InitializeUniform(TemporalNetwork network, Random rng)
    {
        double phase = rng.NextDouble() * 2.0 * Math.PI;
        foreach (var node in network.Nodes)
            node.Phase = phase;
    }
}
