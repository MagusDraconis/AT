namespace TQM.Core.ResearchDATA;

/// <summary>
/// Assessment of TQM compatibility with the observed galaxy dynamics.
/// Evaluates whether the empirical acceleration scale and mass discrepancy
/// pattern can plausibly support TQM's defect-DM and identity-abundance structure.
/// </summary>
public sealed record TqmCompatibilityAssessment(
    double EmpiricalA0,
    double TqmExpectedA0,
    double Ratio,
    bool AccelerationScaleConsistent,
    bool MassDiscrepancyPatternConsistent,
    bool LowSurfaceBrightnessConsistent,
    bool TransitionSharpnessConsistent,
    int ConsistencyScore,
    int TotalChecks,
    string ConsistencyLevel,
    string DefectDM_Assessment,
    string IdentityAbundance_Assessment,
    string DynamicalStructure_Assessment,
    string OverallAssessment,
    string Summary);

/// <summary>
/// Aggregate result for the full Lelli mass model analysis.
/// </summary>
public sealed record LelliAnalysisResult(
    string SectionA_DatasetStructure,
    string SectionB_GalaxyStatistics,
    string SectionC_MassDecomposition,
    string SectionD_MassDiscrepancy,
    string SectionE_Acceleration,
    string SectionF_A0Audit,
    string SectionG_TqmImplications,
    string SectionH_HostileReview,
    string SectionI_FinalVerdict,
    GalaxySummary[] GalaxySummaries,
    VelocityStatistics VelocityStats,
    MassDiscrepancyAnalysis DiscrepancyAnalysis,
    AccelerationAnalysis AccelerationAnalysisResult,
    A0Analysis A0AnalysisResult,
    TqmCompatibilityAssessment TqmAssessment);
