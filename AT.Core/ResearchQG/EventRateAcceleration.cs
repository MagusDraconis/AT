namespace AT.Core.ResearchQG;

/// <summary>QG-087 event-rate acceleration: a₀ = c × d ln(N)/dt = c × H_event. For the
/// physically motivated event models this gives a₀ ~ cH (the 'cH class', no 2π) — the same
/// order as observed a₀ = 1.2e-10, but missing the 1/(2π) factor (QG-084/085).</summary>
public static class EventRateAcceleration
{
    /// <summary>a₀ from the event rate at z=0.</summary>
    public static double A0(double hEvent0) => EventCosmologyConstants.C * hEvent0;

    /// <summary>a₀ for each model at z=0.</summary>
    public static (string Model, double A0_m_s2)[] A0ForEach(EventGrowthModel[] models)
        => models.Select(m => (m.Name, A0(m.HEventOfZ(0.0)))).ToArray();
}
