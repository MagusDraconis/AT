namespace AT.Core.ResearchQG;

/// <summary>QG-086 emergent-gravity (MOND) hypothesis: a0 is a new fundamental constant
/// that modifies gravity at low acceleration. One free parameter, no explanation for its
/// value, and predicts a CONSTANT a0 (no redshift evolution).</summary>
public static class EmergentScaleModel
{
    public static string Description =>
        "a0 = 1.2e-10 m/s² is a new fundamental constant; gravity modified below a0";

    public static double Predict() => 1.2e-10;

    public static int ParameterCount => 1;

    public static string EvolutionPrediction => "a0 CONSTANT (no evolution)";
}
