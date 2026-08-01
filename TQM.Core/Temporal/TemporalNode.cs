using System.Globalization;

namespace TQM.Core.Temporal;

/// <summary>
/// A single temporal oscillator node with intrinsic phase, frequency, and energy.
/// </summary>
public sealed class TemporalNode
{
    public int Id { get; }
    public double Phase { get; set; }
    public double Frequency { get; set; }
    public double Energy { get; set; }

    public TemporalNode(int id, double phase = 0.0, double frequency = 1.0, double energy = 0.0)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be non-negative.");

        Id = id;
        Phase = phase;
        Frequency = frequency;
        Energy = energy;
    }

    public override string ToString() =>
        string.Format(CultureInfo.InvariantCulture,
            "Node[{0:D3}] φ={1,10:F6} ω={2,10:F6} E={3,10:F6}",
            Id, Phase, Frequency, Energy);
}
