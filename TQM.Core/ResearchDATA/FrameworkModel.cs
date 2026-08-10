namespace TQM.Core.ResearchDATA;

/// <summary>A cosmological framework being compared.</summary>
public sealed record FrameworkModel(string Name, string CoreIdea, string KeyEquation, int FreeParams, int DerivedParams, int Assumptions, string Category);

/// <summary>A single assumption counted in the audit.</summary>
public sealed record AssumptionEntry(string Framework, string Assumption, bool IsFundamental, bool IsTestable, string Notes);

/// <summary>Prediction classification for a RAR aspect.</summary>
public sealed record PredictionEntry(string Framework, string Aspect, string Status, bool Predicted, bool Constrained, bool Fitted, string Details);

/// <summary>Explanatory compression = observables explained / independent assumptions.</summary>
public sealed record CompressionResult(string Framework, int ObservablesExplained, int IndependentAssumptions, double CompressionRatio, string Assessment);

/// <summary>What would falsify each framework?</summary>
public sealed record FailureMode(string Framework, string FalsificationCondition, bool TestableNow, bool TestableFuture, string Severity);

public sealed record RankingEntry(string Framework, int Score, string Rationale);

/// <summary>Aggregate result.</summary>
public sealed record ExplanatoryPowerResult(string SectionA, string SectionB, string SectionC, string SectionD, string SectionE, string SectionF, string SectionG, string SectionH, string SectionI, FrameworkModel[] Models, AssumptionEntry[] Assumptions, PredictionEntry[] Predictions, CompressionResult[] Compressions, FailureMode[] Failures, RankingEntry[] Rankings);