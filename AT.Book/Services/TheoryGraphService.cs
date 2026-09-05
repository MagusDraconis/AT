using AT.Book.Data;
using AT.Book.Domain;
using AT.Book.ViewModels;

namespace AT.Book.Services;

/// <summary>
/// Builds the directed acyclic dependency graph from the theory registry and answers
/// graph queries: ancestors (dependency tracing), descendants (impact analysis), and
/// the layer-ordered book navigation.
/// </summary>
public sealed class TheoryGraphService
{
    private readonly TheoryRegistry _registry;

    public IReadOnlyList<GraphNode> Nodes { get; }
    public IReadOnlyList<GraphEdge> Edges { get; }
    public IReadOnlyList<LayerGroup> Layers { get; }

    private readonly IReadOnlyDictionary<string, GraphNode> _nodeById;

    public TheoryGraphService(TheoryRegistry registry)
    {
        _registry = registry;

        var nodes = new List<GraphNode>();
        var edges = new List<GraphEdge>();

        foreach (var o in registry.Objects)
        {
            nodes.Add(new GraphNode(o.Id, o.Title, o.Layer, o.Classification, o.Kind.ToString()));
            foreach (var d in o.Dependencies)
                edges.Add(new GraphEdge(o.Id, d));
        }
        foreach (var a in registry.Audits)
        {
            nodes.Add(new GraphNode(a.Id, a.Title, a.Layer, a.Classification, "Audit"));
            foreach (var d in a.Dependencies)
                edges.Add(new GraphEdge(a.Id, d));
        }

        Nodes = nodes;
        Edges = edges;
        _nodeById = nodes.ToDictionary(n => n.Id, StringComparer.OrdinalIgnoreCase);

        Layers = Enum.GetValues<TheoryLayer>()
            .Select(layer => new LayerGroup(
                layer,
                LayerName(layer),
                registry.Objects.Where(o => o.Layer == layer).ToArray()))
            .ToArray();
    }

    /// <summary>All ancestors of an object (its transitive dependency closure).</summary>
    public IReadOnlySet<string> Ancestors(string id)
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        CollectUpstream(id, result);
        return result;
    }

    /// <summary>All descendants of an object (its transitive impact).</summary>
    public IReadOnlySet<string> Descendants(string id)
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        CollectDownstream(id, result);
        return result;
    }

    private void CollectUpstream(string id, HashSet<string> acc)
    {
        foreach (var e in Edges)
            if (string.Equals(e.Source, id, StringComparison.OrdinalIgnoreCase))
                if (acc.Add(e.Target))
                    CollectUpstream(e.Target, acc);
    }

    private void CollectDownstream(string id, HashSet<string> acc)
    {
        foreach (var e in Edges)
            if (string.Equals(e.Target, id, StringComparison.OrdinalIgnoreCase))
                if (acc.Add(e.Source))
                    CollectDownstream(e.Source, acc);
    }

    public static string LayerName(TheoryLayer layer) => layer switch
    {
        TheoryLayer.Foundations => "Foundations",
        TheoryLayer.Structure => "Structure",
        TheoryLayer.Information => "Information",
        TheoryLayer.Cosmology => "Cosmology",
        TheoryLayer.Physics => "Physics",
        TheoryLayer.Correspondence => "Correspondence",
        _ => layer.ToString(),
    };
}
