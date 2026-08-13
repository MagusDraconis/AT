namespace TQM.Core.ResearchQG;

/// <summary>QG-084 Mach-like coupling: local inertia is set by the global matter
/// distribution, giving an acceleration scale ~ c²/R_H = cH. This yields the right
/// ORDER but lacks the 2π factor (predicts ~6× the observed a0).</summary>
public static class MachCouplingModel
{
    public static string Description =>
        "local inertia from global matter (Sciama); a_Mach ~ c²/R_H = cH";

    public static double Predict() => LocalCosmicCoupling.CH;

    public static bool HasExactTwoPi => false;

    public static double RatioToObserved() => Predict() / LocalCosmicCoupling.A0_Mond;
}
