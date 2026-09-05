using AT.Book.Domain;

namespace AT.Book.ViewModels;

/// <summary>A graph node (one theory object or audit).</summary>
public sealed record GraphNode(
    string Id,
    string Label,
    TheoryLayer Layer,
    TheoryClassification Classification,
    string Kind);

/// <summary>A directed dependency edge (source → target means source depends on target).</summary>
public sealed record GraphEdge(string Source, string Target);

/// <summary>A layer group for the book navigation and timeline.</summary>
public sealed record LayerGroup(TheoryLayer Layer, string Name, IReadOnlyList<TheoryObject> Objects);
