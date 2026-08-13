namespace TQM.Core.ResearchQG;

/// <summary>QG-087 event-density model: candidate event variables N(t) (event density,
/// causal-event count, information content, entropy, state-transition count) and their
/// event Hubble rate H_event = d ln N/dt.</summary>
public sealed record EventGrowthModel(
    string Name,
    string NOfT,
    Func<double, double> HEventOfZ); // H_event(z) in s^-1

/// <summary>Shared constants for the event cosmology.</summary>
public static class EventCosmologyConstants
{
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double C = 299792458.0;
    public const double Kpc_m = 3.0857e19;
    public static double H0PerS => H0 / 3.0857e19;       // 2.184e-18 s^-1
    public static double T0Seconds => 13.8e9 * 3.15576e7; // 4.355e17 s

    public static double HLambda(double z) => H0PerS * Math.Sqrt(OmM * Math.Pow(1 + z, 3) + OmL);
}
