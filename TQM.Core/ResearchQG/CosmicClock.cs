namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-080 CosmicClock: the physical/emergent clock. Physical time τ advances at rate
/// γ(t) = dτ/dt = a(t), i.e. the FLRW scale factor is reinterpreted as the cosmic clock
/// rate. The fractional drift of the clock is the Hubble parameter: d(ln γ)/dt = H.
/// </summary>
public static class CosmicClock
{
    /// <summary>Clock rate γ(t) = dτ/dt, identified with the scale factor a.</summary>
    public static double ClockRate(double z) => Cosmology.ScaleFactor(z);

    /// <summary>Physical time τ (conformal time) at redshift z.</summary>
    public static double PhysicalTime(double z) =>
        Cosmology.ConformalTimeAtScaleFactor(Cosmology.ScaleFactor(z));

    /// <summary>Fractional clock drift d(ln γ)/dt (≡ H, km/s/Mpc).</summary>
    public static double ClockDrift(double z) => Cosmology.H(z);

    /// <summary>Clock acceleration τ̈/τ̇ = d(ln γ)/dt = H (the log-derivative of the clock rate).</summary>
    public static double ClockAccelerationRatio(double z) => Cosmology.H(z);
}
