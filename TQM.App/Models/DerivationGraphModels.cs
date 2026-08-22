namespace TQM.App.Models;

/// <summary>A node in the full TQM-QG derivation graph (one phase).</summary>
public sealed record DerivationGraphNode(
    string Id,                  // "QG140" (mermaid-safe)
    double Phase,               // 140
    string Title,               // short node label shown in the graph
    string Domain,
    string Classification,
    string Validation,          // tested | partial | untested
    string Formula,             // key_result formula text (full)
    string FormulaShort,        // truncated for tooltip
    string File);               // report file name

/// <summary>A directed derivation edge: From ---> To.</summary>
public sealed record DerivationGraphEdge(
    string From,                // source node id
    string To,                  // target node id
    string? Label = null);      // optional edge label (formula note)

/// <summary>The complete derivation graph definition.</summary>
public sealed record DerivationGraphData(
    IReadOnlyList<DerivationGraphNode> Nodes,
    IReadOnlyList<DerivationGraphEdge> Edges,
    IReadOnlyList<string> Domains,
    Dictionary<string, int> DomainCounts);
