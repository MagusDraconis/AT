namespace AT.Core.ResearchQG;

/// <summary>QG-085 horizon mechanism (Unruh + Gibbons–Hawking): the de Sitter horizon
/// temperature is T = ħH/2πk, and the Unruh acceleration is a = 2πkT/ħ, so the 2π's
/// CANCEL and a = cH (no retained 2π). This is the 'cH class' excluded in QG-084.</summary>
public static class HorizonMechanismModel
{
    public static string Description =>
        "Unruh + Gibbons-Hawking: T=ħH/2πk → a=2πkT/ħ = cH (2π cancels)";

    public static double Predict() => LocalCosmicCoupling.CH;

    public static bool RetainsTwoPi => false;

    public static double RatioToObserved() => Predict() / LocalCosmicCoupling.A0_Mond;
}
