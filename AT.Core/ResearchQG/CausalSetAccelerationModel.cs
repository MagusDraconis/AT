namespace AT.Core.ResearchQG;

/// <summary>QG-096 all acceleration scales constructible from {c, l_P, N, Λ, H} without new
/// parameters. The candidates are: Planck a_P = c²/l_P (wrong by ~61 decades), cH, cH/2π,
/// c²√Λ (~cH), and c/t_universe (~cH). All the cosmological candidates are ~cH; the 2π is the
/// only free dimensionless factor.</summary>
public sealed record CausalAccelerationScale(string Name, string Construction, double Value_m_s2, bool HasTwoPi);

public static class CausalSetAccelerationModel
{
    public const double C = 299792458.0;
    public const double PlanckLength = 1.616255e-35;
    public const double H0 = 67.4;
    public const double Lambda = 1.1e-52;
    public const double A0 = 1.2e-10;
    public const double Kpc_m = 3.0857e19;

    public static double H0PerS => H0 / Kpc_m;
    public static double CH => C * H0PerS;                          // 6.55e-10
    public static double Gdagger => CH / (2.0 * Math.PI);           // 1.04e-10
    public static double C2SqrtLambda => C * C * Math.Sqrt(Lambda);  // 9.4e-10
    public static double COverAge => C / (13.8e9 * 3.15576e7);      // 6.88e-10
    public static double PlanckAcceleration => C * C / PlanckLength; // 5.6e51

    /// <summary>All constructible acceleration scales.</summary>
    public static CausalAccelerationScale[] Scales() => new[]
    {
        new CausalAccelerationScale("Planck a_P = c²/l_P", "c, l_P", PlanckAcceleration, false),
        new CausalAccelerationScale("cH (causal-depth growth)", "c, H", CH, false),
        new CausalAccelerationScale("c²√Λ (ever-present Λ)", "c, Λ", C2SqrtLambda, false),
        new CausalAccelerationScale("c/t_universe", "c, age", COverAge, false),
        new CausalAccelerationScale("cH/2π (g†)", "c, H, 2π", Gdagger, true),
    };
}
