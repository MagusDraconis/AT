namespace AT.Core.ResearchDATA;

/// <summary>
/// AT-derived RAR prediction.
/// Based on: defect-DM isothermal profile + exponential baryonic disk
/// → g_obs = g_bar * sqrt(1 + (g†/g_bar))
/// with g† = cH₀/(2π) derived from cosmological boundary conditions.
/// </summary>
public sealed record AtRarPrediction(
    double DerivedGDagger,
    double DerivedGDagger_1e10,
    string DerivationSteps,
    string FunctionalForm,
    double[] PredictedLogGobs,
    double[] InputLogGbar,
    double ChiSqVsData,
    double RmsScatter,
    bool MatchesData,
    string Verdict);

/// <summary>
/// Step-by-step derivation documentation.
/// </summary>
public sealed record DerivationStep(
    int StepNumber,
    string Description,
    string Equation,
    string PhysicalJustification);
