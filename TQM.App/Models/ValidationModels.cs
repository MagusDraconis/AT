namespace TQM.App.Models;

// ── Docs/TQMQG_Predictions.json ────────────────────────────────────────────

public sealed record PredictionRegistryFile(
    string Title,
    bool Immutable,
    string Rule,
    string LockedBy,
    List<RegisteredPredictionModel> Predictions);

public sealed record RegisteredPredictionModel(
    string Id,
    string Name,
    string DerivationPhase,
    string Formula,
    string Inputs,
    string FrozenValue,
    string Uncertainty,
    string Falsification,
    string? Outcome);

// ── Docs/TQMQG_PhysicsCoverage.json ────────────────────────────────────────

public sealed record PhysicsCoverageFile(
    CoverageMeta Meta,
    CoverageStats Coverage,
    List<ContradictionModel> Contradictions,
    List<OpenQuestionModel> OpenQuestions,
    List<CoveragePredictionModel> Predictions,
    List<ObservableModel> Observables,
    List<GrTopicModel> GrTopics,
    List<PhaseModel> Phases);

public sealed record CoverageMeta(
    string File,
    string Purpose,
    string LastUpdated,
    int PhasesCount,
    string Note);

public sealed record CoverageStats(
    int TotalPhases,
    int Tested,
    int Partial,
    int Untested,
    int Audit,
    double WeightedCoverage,
    Dictionary<string, DomainStats>? Domains,
    Dictionary<string, int>? Observables);

public sealed record DomainStats(int Tested, int Partial, int Untested, int Audit, int Total);

public sealed record ContradictionModel(
    string Id,
    string Topic,
    string A,
    string B,
    string Status,
    string? Resolution,
    List<string>? Phases);

public sealed record OpenQuestionModel(
    string Question,
    string Phase,
    string Status);

public sealed record CoveragePredictionModel(
    string Prediction,
    string Phase,
    string Status);

public sealed record ObservableModel(
    string Name,
    string Status,
    string Phase,
    string Detail);

public sealed record GrTopicModel(
    string Topic,
    string Phase,
    string Status,
    string Detail);

public sealed record PhaseModel(
    double Phase,
    string File,
    string Classification,
    string Domain,
    string Validation,
    string KeyResult);
