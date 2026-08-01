namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Represents a detected internal resonance state of a condensate.
/// </summary>
public enum InternalResonanceStateType
{
    Uniform,           // all phases equal (standard sync)
    Clockwise,         // phase increases clockwise around center
    CounterClockwise,  // phase decreases clockwise
    WindingPositive,   // total winding = +1 (phase wraps +2π)
    WindingNegative,   // total winding = -1
    Defect,            // phase singularity / undefined
    Unknown
}

/// <summary>
/// Result of internal state analysis for a condensate.
/// </summary>
public sealed class InternalStateResult
{
    public InternalResonanceStateType InitialState { get; }
    public InternalResonanceStateType FinalState { get; }
    public double WindingNumber { get; } // final winding number
    public bool StatePreserved { get; }  // did the initial state survive?
    public int DecayIteration { get; }   // when state changed, -1 if preserved
    public double MeanCoherence { get; } // mean local R across condensate
    public double PhaseGradient { get; } // mean |∇θ| across oscillators

    public InternalStateResult(
        InternalResonanceStateType initialState,
        InternalResonanceStateType finalState,
        double windingNumber,
        bool statePreserved,
        int decayIteration,
        double meanCoherence,
        double phaseGradient)
    {
        InitialState = initialState;
        FinalState = finalState;
        WindingNumber = windingNumber;
        StatePreserved = statePreserved;
        DecayIteration = decayIteration;
        MeanCoherence = meanCoherence;
        PhaseGradient = phaseGradient;
    }

    public override string ToString() =>
        $"{InitialState} → {FinalState} ({(StatePreserved ? "PRESERVED" : "DECAYED")} " +
        $"winding={WindingNumber:F2}, coherence={MeanCoherence:F3})";
}
