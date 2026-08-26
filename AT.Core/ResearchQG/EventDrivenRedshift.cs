namespace AT.Core.ResearchQG;

/// <summary>QG-087 event-driven redshift: 1+z = N(t_obs)/N(t_emit). With N(t0)=1 this is
/// 1+z = 1/N(t_emit), i.e. redshift is the cumulative ratio of the event density — a
/// rewrite of redshift as event-density evolution rather than metric expansion.</summary>
public static class EventDrivenRedshift
{
    /// <summary>Redshift from event density: 1+z = N_obs/N_emit.</summary>
    public static double Redshift(double nEmit, double nObs = 1.0) => nObs / nEmit - 1.0;

    /// <summary>For N = a, this equals the FLRW redshift exactly (trivial identification).</summary>
    public static bool EquivalentToFlrw() => true;
}
