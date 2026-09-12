namespace AT.Core.ResearchDATA;

/// <summary>
/// Assessment of whether AT simultaneously explains RAR scale, shape, and width.
/// </summary>
public sealed record CompletionScore(
    string Aspect,
    string Question,
    bool Derived,
    bool EmpiricallyConsistent,
    int FreeParams,
    string Status,
    string Notes,
    CompletionEvidence Evidence = CompletionEvidence.Authored);

/// <summary>
/// How a completion-audit row's <c>Derived</c> verdict was obtained (ResearchY-G_026).
/// <see cref="Computed"/> = produced by an executed check in this codebase;
/// <see cref="Authored"/> = a recorded research judgement with no executable check behind it.
/// Carried explicitly so that a typed verdict cannot be reported as a computed one — the table previously
/// labelled seven hand-typed <c>true</c> rows "DERIVED ✓" and then counted them.
/// </summary>
public enum CompletionEvidence
{
    Computed,
    Authored,
}

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
