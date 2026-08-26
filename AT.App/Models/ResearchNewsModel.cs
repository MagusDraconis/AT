namespace AT.App.Models;

/// <summary>A single research-news entry: headline, summaries, key result, and linked phases.</summary>
public sealed record ResearchNewsModel(
    string Id,
    string Title,
    string Kicker,
    string Summary,
    string TechnicalSummary,
    string NonTechnicalSummary,
    string KeyResult,
    string VisualTagline,
    bool IsMilestone,
    IReadOnlyList<NewsPhaseModel> Phases);

/// <summary>A phase linked from a news entry (name, classification, one-line result, report URL).</summary>
public sealed record NewsPhaseModel(
    string Phase,
    string Classification,
    string Result,
    string ReportUrl);
