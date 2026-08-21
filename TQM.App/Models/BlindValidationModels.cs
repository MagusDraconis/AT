namespace TQM.App.Models;

/// <summary>One leave-one-out observable: hidden, rebuilt from remaining D96 quantities, compared.</summary>
public sealed record LooObservableModel(
    string Name,
    string Symbol,
    string Formula,
    string FormulaHtml,
    double Predicted,
    double Measurement,
    double DeviationPercent,
    string Deps,
    string Chain,
    string Units,
    string Phase,
    int Precision);

/// <summary>A node in the D96 → Higgs dependency chain.</summary>
public sealed record DependencyNodeModel(
    string Label,
    string Description,
    string Kind); // primitive | derived | hidden | output

/// <summary>A formula presented on the page (symbolic + numeric + result).</summary>
public sealed record BlindFormulaModel(
    string Title,
    string Steps,
    string StepsHtml,
    double Result,
    double Physical,
    double DeviationPercent,
    string Note);
