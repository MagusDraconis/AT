namespace TQM.Core.TemporalField;

/// <summary>
/// A single cell in the temporal field, carrying local phase (wave amplitude),
/// accumulated energy, and derived density.
/// </summary>
public sealed class TemporalFieldCell
{
    /// <summary>
    /// Oscillatory wave amplitude φ at this cell.
    /// Evolves via the wave equation with sources from coupled oscillators.
    /// </summary>
    public double TemporalPhase { get; set; }

    /// <summary>
    /// Accumulated energy deposited by oscillator injections and wave propagation.
    /// </summary>
    public double TemporalEnergy { get; set; }

    /// <summary>
    /// Local temporal density, derived from energy concentration.
    /// This is the quantity that oscillators read to adjust their frequency.
    /// </summary>
    public double TemporalDensity { get; set; }

    public TemporalFieldCell(double phase = 0.0, double energy = 0.0, double density = 0.0)
    {
        TemporalPhase = phase;
        TemporalEnergy = energy;
        TemporalDensity = density;
    }

    public override string ToString() =>
        $"φ={TemporalPhase,8:F4} E={TemporalEnergy,8:F4} ρ={TemporalDensity,8:F4}";
}
