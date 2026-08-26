namespace AT.Core.ResearchQG;

/// <summary>QG-086 coincidence hypothesis: a0 ~ 1e-10 has no physical meaning; it is a free
/// parameter that happens to fall in the cosmological/galactic acceleration band.</summary>
public static class CoincidenceModel
{
    public static string Description =>
        "a0 free parameter, accidentally near cH/c²√Λ/galactic GM/R²";

    public static int ParameterCount => 1;

    public static string EvolutionPrediction => "no prediction";

    /// <summary>Probability a0 falls in the cosmological acceleration band (log-uniform 6-decade prior).</summary>
    public static double BandCoincidenceProbability()
    {
        // Band = [1e-11, 1e-9] (2 decades), prior = [1e-13, 1e-7] (6 decades).
        double bandDex = 2.0, priorDex = 6.0;
        return bandDex / priorDex;
    }
}
