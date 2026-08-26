namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 11 — origin of causal order. Tests whether the partial (causal) order emerges from the
/// actualization/generation relation (branching) rather than being a separate primitive. Models actualization as
/// a deterministic branching tree; the causal order is the transitive closure (ancestor relation) of the
/// parent→child generation relation. No new primitives.
/// </summary>
public static class CausalOrder
{
    /// <summary>Parent of event i in a complete b-ary tree (breadth-first numbering, root = 0); −1 for the root.</summary>
    public static int Parent(int i, int branching) => i <= 0 ? -1 : (i - 1) / branching;

    /// <summary>Number of events in a complete b-ary tree of depth K (generations 0..K).</summary>
    public static int EventCount(int branching, int K)
    {
        int total = 0, layer = 1;
        for (int g = 0; g <= K; g++) { total += layer; layer *= branching; }
        return total;
    }

    /// <summary>Generation (layer) of event i in a complete b-ary tree.</summary>
    public static int Generation(int i, int branching)
    {
        int g = 0, count = 0, layer = 1;
        while (true)
        {
            if (i < count + layer) return g;
            count += layer;
            layer *= branching;
            g++;
        }
    }

    /// <summary>Is a a strict ancestor of b (a lies on the root→b path, with a ≠ b)? This is the causal order.</summary>
    public static bool IsAncestor(int a, int b, int branching)
    {
        if (a == b) return false;                       // irreflexive
        int cur = b;
        while (cur > 0)
        {
            cur = Parent(cur, branching);
            if (cur == a) return true;
        }
        return false;
    }

    /// <summary>Irreflexivity: no event is its own ancestor.</summary>
    public static bool Irreflexive(int branching, int n)
    {
        for (int i = 0; i < n; i++) if (IsAncestor(i, i, branching)) return false;
        return true;
    }

    /// <summary>Antisymmetry: no two distinct events are mutual ancestors.</summary>
    public static bool Antisymmetric(int branching, int n)
    {
        for (int a = 0; a < n; a++)
            for (int b = 0; b < n; b++)
                if (a != b && IsAncestor(a, b, branching) && IsAncestor(b, a, branching)) return false;
        return true;
    }

    /// <summary>Transitivity: ancestor of ancestor is ancestor.</summary>
    public static bool Transitive(int branching, int n)
    {
        for (int a = 0; a < n; a++)
            for (int b = 0; b < n; b++)
                for (int c = 0; c < n; c++)
                    if (IsAncestor(a, b, branching) && IsAncestor(b, c, branching) && !IsAncestor(a, c, branching))
                        return false;
        return true;
    }

    /// <summary>The generation order is a linear extension: ancestor ⟹ lower generation (a "temporal" ordering).</summary>
    public static bool GenerationIsLinearExtension(int branching, int n)
    {
        for (int a = 0; a < n; a++)
            for (int b = 0; b < n; b++)
                if (IsAncestor(a, b, branching) && Generation(a, branching) >= Generation(b, branching))
                    return false;
        return true;
    }
}
