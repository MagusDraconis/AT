using System.Text.RegularExpressions;
using AT.App.Models;

namespace AT.App.Services;

/// <summary>
/// Builds the full AT-QG derivation graph from the physics-coverage single source of truth.
/// Nodes are phases; edges are the known derivation links — explicit "QG###" references in the
/// key-result text plus sequential in-domain chains. Each node carries the formula actually used.
/// </summary>
public class DerivationGraphService
{
    private readonly ValidationDataService _validation;

    public DerivationGraphService(ValidationDataService validation)
    {
        _validation = validation;
    }

    public async Task<DerivationGraphData> GetGraphAsync()
    {
        var coverage = await _validation.GetCoverageAsync();

        // 1. Nodes from phases.
        var nodes = new List<DerivationGraphNode>(coverage.Phases.Count);
        var byId = new Dictionary<string, DerivationGraphNode>(StringComparer.OrdinalIgnoreCase);
        var domainOrder = new List<string>();
        var domainSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var phase in coverage.Phases)
        {
            var id = MermaidSafeId(phase.Phase);
            var formula = phase.KeyResult.Trim();
            var node = new DerivationGraphNode(
                Id: id,
                Phase: phase.Phase,
                Title: BuildTitle(phase),
                Domain: phase.Domain,
                Classification: phase.Classification,
                Validation: phase.Validation,
                Formula: formula,
                FormulaShort: Truncate(formula, 110),
                File: phase.File);
            nodes.Add(node);
            byId[id] = node;
            if (domainSet.Add(phase.Domain))
                domainOrder.Add(phase.Domain);
        }

        // 2. Edges: explicit QG### references in key_result.
        var edges = new List<DerivationGraphEdge>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in nodes)
        {
            foreach (var m in Regex.Matches(node.Formula, @"QG(\d+(?:[,.]5)?)").Cast<Match>())
            {
                var target = QgId(m.Groups[1].Value);
                if (string.IsNullOrEmpty(target) || !byId.ContainsKey(target)) continue;
                if (target == node.Id) continue;
                var key = $"{target}|{node.Id}";
                if (!seen.Add(key)) continue;
                edges.Add(new DerivationGraphEdge(target, node.Id));
            }
        }

        // 3. Sequential in-domain chains (phase → next phase in same domain).
        foreach (var domain in domainOrder)
        {
            var chain = nodes
                .Where(n => string.Equals(n.Domain, domain, StringComparison.OrdinalIgnoreCase))
                .OrderBy(n => n.Phase)
                .ToList();
            for (int i = 0; i + 1 < chain.Count; i++)
            {
                var from = chain[i].Id;
                var to = chain[i + 1].Id;
                var key = $"{from}|{to}";
                if (seen.Contains(key)) continue;
                seen.Add(key);
                edges.Add(new DerivationGraphEdge(from, to));
            }
        }

        var domainCounts = nodes
            .GroupBy(n => n.Domain)
            .ToDictionary(g => g.Key, g => g.Count());

        return new DerivationGraphData(nodes, edges, domainOrder, domainCounts);
    }

    private static string BuildTitle(PhaseModel phase)
    {
        var n = FormatPhase(phase.Phase);
        return $"{n} · {Truncate(phase.Classification, 42)}";
    }

    public static string FormatPhase(double phase)
    {
        return phase == Math.Floor(phase)
            ? $"QG{(int)phase}"
            : $"QG{phase.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
    }

    private static string QgId(string raw)
    {
        var n = raw.Replace(',', '.');
        return $"QG{n}".Replace('.', '_');
    }

    private static string MermaidSafeId(double phase)
    {
        // Phase 116.5 -> QG116_5 (dot is not mermaid-id-safe).
        return FormatPhase(phase).Replace('.', '_');
    }

    private static string Truncate(string s, int max)
    {
        if (s.Length <= max) return s;
        return s[..(max - 1)] + "…";
    }
}
