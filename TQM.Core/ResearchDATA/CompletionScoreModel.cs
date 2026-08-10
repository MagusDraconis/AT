namespace TQM.Core.ResearchDATA;

/// <summary>
/// Assessment of whether TQM simultaneously explains RAR scale, shape, and width.
/// </summary>
public sealed record CompletionScore(
    string Aspect,
    string Question,
    bool Derived,
    bool EmpiricallyConsistent,
    int FreeParams,
    string Status,
    string Notes);

/// <summary>
/// Aggregate explanatory completion audit.
/// </summary>
public sealed record ExplanatoryCompletion(
    CompletionScore[] Scores,
    int DerivedCount,
    int TotalCount,
    double CompletionFraction,
    int TotalFreeParams,
    string Classification,
    string Summary);

/// <summary>
/// Aggregate DATA-005 result.
/// </summary>
public sealed record RarScatterResult(
    string SectionA_PiOrigin,
    string SectionB_ScaleComparison,
    string SectionC_ScatterSources,
    string SectionD_VariancePropagation,
    string SectionE_GalaxyScatter,
    string SectionF_CompletionAudit,
    string SectionG_HostileReview,
    string SectionH_RemainingWeaknesses,
    string SectionI_FinalVerdict,
    PiFactorAudit PiAudit,
    ScaleComparison ScaleComp,
    ScatterSourceCatalog ScatterCatalog,
    VarianceModel Variance,
    GalaxyScatterMatrix GalaxyScatter,
    ExplanatoryCompletion Completion);
