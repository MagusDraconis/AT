namespace TQM.Core.ResearchQG;

/// <summary>QG-084 cosmic-boundary model: the observable-universe radius R_H = c/H acts
/// as a boundary condition, giving a local scale ~ c²/R_H = cH. Same order as Mach,
/// again lacking the 2π factor.</summary>
public static class BoundaryConditionModel
{
    public static string Description =>
        "R_H = c/H as boundary; a_boundary ~ c²/R_H = cH";

    public static double Predict() => LocalCosmicCoupling.C2OverRH;

    public static bool HasExactTwoPi => false;

    public static double RatioToObserved() => Predict() / LocalCosmicCoupling.A0_Mond;
}
