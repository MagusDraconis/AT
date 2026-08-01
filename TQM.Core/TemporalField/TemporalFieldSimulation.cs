using TQM.Core.Temporal;

namespace TQM.Core.TemporalField;

/// <summary>
/// Simulates a network of oscillators coupled exclusively through a shared temporal field.
///
/// No direct oscillator-to-oscillator coupling is used.
/// Instead:
///   1. Oscillators inject energy into the field based on their phase.
///   2. The field propagates, diffuses, and damps.
///   3. Oscillators read the local field density and adjust their frequency.
///   4. Oscillators update their phase.
///
/// This models a field-mediated interaction:
///   Oscillator A ↔ Temporal Field ↔ Oscillator B
/// </summary>
public sealed class TemporalFieldSimulation
{
    private readonly TemporalNetwork _network;
    private readonly TemporalField _field;
    private readonly int[] _oscillatorPositions; // cell index for each oscillator

    /// <summary>
    /// Coupling strength α: how strongly the local field density shifts oscillator frequency.
    /// Δω = α · ρ_local
    /// </summary>
    public double FieldCouplingAlpha { get; set; } = 0.5;

    /// <summary>
    /// Injection strength β: how much energy an oscillator deposits per unit phase amplitude.
    /// </summary>
    public double InjectionStrength { get; set; } = 1.0;

    /// <summary>
    /// Simulation time step.
    /// </summary>
    public double TimeStep { get; set; } = 1.0;

    public int CurrentIteration { get; private set; }
    public double ElapsedTime => CurrentIteration * TimeStep;
    public TemporalField Field => _field;

    public TemporalFieldSimulation(TemporalNetwork network, TemporalField field, int[] oscillatorPositions)
    {
        _network = network ?? throw new ArgumentNullException(nameof(network));
        _field = field ?? throw new ArgumentNullException(nameof(field));

        if (oscillatorPositions.Length != network.NodeCount)
            throw new ArgumentException(
                $"Position count ({oscillatorPositions.Length}) must match network node count ({network.NodeCount}).",
                nameof(oscillatorPositions));

        _oscillatorPositions = (int[])oscillatorPositions.Clone();
    }

    /// <summary>
    /// Advances the simulation by one time step.
    /// </summary>
    public void Step()
    {
        int n = _network.NodeCount;

        // ── Phase 1: Oscillators inject energy into the field ──
        for (int i = 0; i < n; i++)
        {
            double phase = _network.Nodes[i].Phase;
            // Energy injection proportional to oscillation amplitude.
            double injected = InjectionStrength * Math.Abs(Math.Sin(phase));
            _field.InjectEnergy(_oscillatorPositions[i], injected);
        }

        // ── Phase 2: Field propagates, diffuses, damps ──
        _field.Update(TimeStep);

        // ── Phase 3: Oscillators read local field density & adjust frequency ──
        for (int i = 0; i < n; i++)
        {
            double localDensity = _field.GetDensityAt(_oscillatorPositions[i]);

            // Adjusted frequency: ω' = ω₀ + α · ρ_local
            // The field density pulls the oscillator's effective frequency.
            double adjustedFreq = _network.Nodes[i].Frequency + FieldCouplingAlpha * localDensity;

            // Energy update: oscillator gains energy proportional to the coupling.
            _network.Nodes[i].Energy += FieldCouplingAlpha * localDensity * TimeStep;

            // Phase update: θ(t+Δt) = θ(t) + ω' · Δt
            double currentPhase = _network.Nodes[i].Phase;
            _network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                currentPhase + adjustedFreq * TimeStep);
        }

        CurrentIteration++;
    }

    /// <summary>
    /// Runs the simulation for the given number of iterations.
    /// </summary>
    public void Run(int iterations)
    {
        for (int i = 0; i < iterations; i++)
            Step();
    }
}
