namespace AT.Core.ResearchDATA;

/// <summary>
/// Assessment of whether AT EXPLAINS or merely ACCOMMODATES the RAR.
/// Key distinction:
/// - Accommodation: free parameters tuned to match data
/// - Explanation: functional form and scale DERIVED from theory without tuning
/// </summary>
public sealed record ExplanatoryPowerAssessment(
    string Category,
    bool ScaleDerived,
    bool FunctionalFormDerived,
    bool ScatterExplained,
    bool SlopePredicted,
    int FreeParametersUsed,
    int FreeParametersInModel,
    double TuningPenalty,
    string Verdict,
    string DetailedAssessment);

/// <summary>
/// Aggregate RAR origin audit result.
/// </summary>
public sealed record RarOriginResult(
    string SectionA_EmpiricalRar,
    string SectionB_TransitionScale,
    string SectionC_MondFit,
    string SectionD_LcdmFit,
    string SectionE_AtDerivation,
    string SectionF_ModelComparison,
    string SectionG_ExplanatoryPower,
    string SectionH_HostileReview,
    string SectionI_FinalVerdict,
    RarFitCollection Fits,
    TransitionScaleAnalysis ScaleAnalysis,
    AtRarPrediction AtPrediction,
    ModelComparison Comparison,
    ExplanatoryPowerAssessment PowerAssessment);
