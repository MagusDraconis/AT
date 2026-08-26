namespace AT.App.Models;

/// <summary>Coverage classification of a measured SM quantity against the AT-QG record.</summary>
public enum CoverageStatus
{
    Tested,
    Partial,
    Untested,
}

/// <summary>One audited SM quantity with its AT-QG status.</summary>
public sealed record AuditItemModel(
    string Name,
    CoverageStatus Status,
    string Phase,
    string Result,
    string Physical,
    double? DeviationPercent,
    string Note);

/// <summary>An observable category grouping audited quantities.</summary>
public sealed record AuditCategoryModel(
    string Name,
    string Description,
    IReadOnlyList<AuditItemModel> Items);

/// <summary>A remaining test on the roadmap (ranked).</summary>
public sealed record AuditRoadmapItemModel(
    int Rank,
    string Name,
    string Status,
    string Why);
