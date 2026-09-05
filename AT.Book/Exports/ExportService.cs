using System.Text;
using System.Text.Json;
using AT.Book.Data;
using AT.Book.Services;

namespace AT.Book.Exports;

/// <summary>Exports the theory book as Markdown or JSON (canonical, data-driven).</summary>
public sealed class ExportService
{
    private readonly TheoryRegistry _registry;
    private readonly TheoryGraphService _graph;

    public ExportService(TheoryRegistry registry, TheoryGraphService graph)
    {
        _registry = registry;
        _graph = graph;
    }

    public string ToMarkdown()
    {
        var sb = new StringBuilder();
        sb.AppendLine("# AT.Book — The Actualization Theory");
        sb.AppendLine();
        foreach (var layer in _graph.Layers)
        {
            sb.AppendLine($"## Layer {(int)layer.Layer} — {layer.Name}");
            sb.AppendLine();
            foreach (var o in layer.Objects.OrderBy(x => x.Title))
            {
                sb.AppendLine($"### {o.Title}  `{o.Id}`");
                sb.AppendLine();
                sb.AppendLine($"- **Classification:** {o.Classification}");
                sb.AppendLine($"- **Kind:** {o.Kind}");
                sb.AppendLine($"- **Dependencies:** {(o.Dependencies.Count == 0 ? "—" : string.Join(", ", o.Dependencies))}");
                if (o.Formula is not null) sb.AppendLine($"- **Formula:** `{o.Formula}`");
                sb.AppendLine();
                sb.AppendLine(o.Summary);
                if (o.Narrative is not null) { sb.AppendLine(); sb.AppendLine(o.Narrative); }
                sb.AppendLine();
            }
        }
        return sb.ToString();
    }

    public string ToJson() => JsonSerializer.Serialize(
        new { objects = _registry.Objects, audits = _registry.Audits },
        new JsonSerializerOptions { WriteIndented = true });
}
