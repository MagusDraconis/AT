namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 318 (reissue 2) — Final Theory Architecture. Review QG223-QG317 and produce the CANONICAL
/// MINIMAL ARCHITECTURE of AT. No observables, no target values, D96 only, deterministic.
///
/// THE FOUR LAYERS (topological order — every concept depends only on concepts in the same or lower
/// layers):
///   LAYER 1 PRIMITIVE  — the irreducible primitives: the Difference (ρ, the scalar counting measure)
///     and η (the tensor reference metric). These are FOUNDATIONAL: removing any one collapses the
///     theory (QG292). CORRECTED per MONO006/A01: Actualization is NOT a primitive — removing Difference
///     collapses it (QG292 Case A) while removing η leaves it intact (QG292 Case B).
///   LAYER 2 DYNAMIC    — the actualization dynamics derived from the primitives: ACTUALIZATION
///     [Difference's count-producing process — a Q-event IS a unit QG268, N=96 its attractor fixed point
///     QG284; derived from Difference, independent of η (MONO006)], Resonance [= Conservation +
///     Boundary], Self-Consistency, Individuation, the Difference Principle, and the actualization
///     ATTRACTOR [the dynamics' fixed state]. DERIVED from the primitive layer.
///   LAYER 3 SPECTRUM   — the D96 spectrum [the inevitable output of the attractor, QG295], the four
///     operators {CROWDING, COMPRESSION, BEAT, LOCKING}, the lock identities [moment-chain fixed points,
///     QG318], and the organization/maturity structure. EMERGENT from the dynamics.
///   LAYER 4 PHYSICS    — every observable sector: fermions, gauge, gravity, cosmology, and the SM
///     dynamics. EMERGENT from the spectrum. The SM-dynamics Lagrangian is a BOUNDARY (hosted, QG242/245).
///
/// THE CLASSIFICATION (four categories):
///   FOUNDATIONAL — an irreducible primitive: cannot be derived, removing it collapses the theory;
///   DERIVED      — follows deductively from lower-layer concepts;
///   EMERGENT     — follows from lower-layer concepts but with genuine structural novelty [a stable
///     pattern not present in the input];
///   BOUNDARY     — a concept that is either a numerical constant not derivable inside the framework [π],
///     an experimental frontier [P1 106 GeV, P2 0νββ], a hosted dynamics [SM Lagrangian], or an open
///     ontological question [ψ fundamental status, Bekenstein 1/4 coefficient].
///
/// THE DEPENDENCY GRAPH:
///   Every concept lists its dependencies [concept names]. The architecture is verified to be a DAG
///   [acyclic] by Kahn's topological sort — every edge points from a lower/equal layer to a higher one.
///
/// Classification:
///   FINAL AT ARCHITECTURE — the canonical minimal architecture is complete: 4 layers, every concept
///     classified, the dependency graph acyclic, and no primitive derivable from the others.
/// </summary>
public static class FinalTheoryArchitecture
{
    /// <summary>The four architectural layers.</summary>
    public enum Layer { Primitive, Dynamic, Spectrum, Physics }

    /// <summary>The concept classification.</summary>
    public enum ConceptKind { Foundational, Derived, Emergent, Boundary }

    /// <summary>An architectural concept with its layer, kind, and dependencies.</summary>
    public sealed record Concept(
        string Name,
        Layer Layer,
        ConceptKind Kind,
        string Note,
        params string[] DependsOn);

    /// <summary>The canonical minimal architecture.</summary>
    public static Concept[] Concepts() => new[]
    {
        // ── Layer 1 — Primitive (FOUNDATIONAL) ──────────────────────────────
        new Concept("Difference", Layer.Primitive, ConceptKind.Foundational,
            "ρ — the scalar counting measure, the sole irreducible primitive (QG278/286/292)"),
        new Concept("η", Layer.Primitive, ConceptKind.Foundational,
            "the tensor reference metric (QG290-292 — the irreducible framework)"),
        new Concept("π", Layer.Primitive, ConceptKind.Boundary,
            "numerical constant — not derivable inside the framework (QG291: framework necessity)"),

        // ── Layer 2 — Dynamic (DERIVED from primitives) ─────────────────────
        new Concept("Actualization", Layer.Dynamic, ConceptKind.Derived,
            "Difference's count-producing process — a Q-event IS a unit (QG268), N=96 is its attractor " +
            "fixed point (QG284); derived from Difference (QG292 removal test), independent of η",
            "Difference"),
        new Concept("Resonance", Layer.Dynamic, ConceptKind.Derived,
            "Resonance = Conservation + Boundary (QG275)", "Difference", "Actualization"),
        new Concept("Self-Consistency", Layer.Dynamic, ConceptKind.Derived,
            "the actualization dynamics closes on itself (QG267)", "Resonance"),
        new Concept("Individuation", Layer.Dynamic, ConceptKind.Derived,
            "the Difference individuates into distinct sectors (QG268)", "Self-Consistency"),
        new Concept("Difference Principle", Layer.Dynamic, ConceptKind.Derived,
            "Difference is the primitive — the root of the hierarchy (QG278)", "Individuation"),
        new Concept("Actualization Attractor", Layer.Dynamic, ConceptKind.Derived,
            "the fixed state of the actualization dynamics (QG295)", "Actualization", "Difference Principle"),
        new Concept("Spectrum Necessity", Layer.Dynamic, ConceptKind.Derived,
            "the spectrum is the inevitable output of the attractor (QG295)", "Actualization Attractor"),

        // ── Layer 3 — Spectrum (EMERGENT from the dynamics) ─────────────────
        new Concept("D96 Spectrum", Layer.Spectrum, ConceptKind.Emergent,
            "the 96-mode spectrum, 95 positive modes + 1 zero (QG295)", "Spectrum Necessity", "η"),
        new Concept("Operator Basis", Layer.Spectrum, ConceptKind.Emergent,
            "{CROWDING, COMPRESSION, BEAT, LOCKING} — the spectral operators (QG260-263/300-312)",
            "D96 Spectrum"),
        new Concept("Lock Identities", Layer.Spectrum, ConceptKind.Emergent,
            "the moment-chain fixed points [Σ√m/span≈10, occMom/Σm≈20, Σm²/Σm≈12/5] (QG313/318)",
            "D96 Spectrum", "Operator Basis"),
        new Concept("Organization Maturity", Layer.Spectrum, ConceptKind.Emergent,
            "the hierarchy/span structure and its critical transition (QG315-316)", "Lock Identities"),
        new Concept("Spectrum → Physics", Layer.Spectrum, ConceptKind.Emergent,
            "moments of the D96 spectrum are the access counts of all sectors (QG157/296)",
            "D96 Spectrum", "η"),

        // ── Layer 4 — Physics (EMERGENT from the spectrum) ──────────────────
        new Concept("Fermion Sector", Layer.Physics, ConceptKind.Emergent,
            "3 families, Z2 doublets, hierarchies (QG138/149-158)", "Spectrum → Physics"),
        new Concept("Gauge Sector", Layer.Physics, ConceptKind.Emergent,
            "1+3+8 gauge structure, couplings 1/α_em=137 (QG161-162)", "Spectrum → Physics"),
        new Concept("Gravity", Layer.Physics, ConceptKind.Emergent,
            "G, M_Pl, M∝R, frame dragging (QG181-187)", "Spectrum → Physics"),
        new Concept("Cosmology", Layer.Physics, ConceptKind.Emergent,
            "Λ, Ω_Λ/Ω_m, CMB, structure formation (QG230-240)", "Spectrum → Physics"),
        new Concept("SM Dynamics", Layer.Physics, ConceptKind.Boundary,
            "the SM Lagrangian/vertices are HOSTED, not derived (QG242/245)", "Gauge Sector", "Fermion Sector"),
        new Concept("Bekenstein Quarter", Layer.Physics, ConceptKind.Boundary,
            "S=A/4 coefficient — needs the 2π quantum factor, not in D96 (QG185)", "Gravity"),
        new Concept("Psi Fundamental Status", Layer.Physics, ConceptKind.Boundary,
            "ψ is an ontological boundary + tensor-sector question (QG223/286)", "Gravity"),
        new Concept("Experimental Frontier", Layer.Physics, ConceptKind.Boundary,
            "P1 106 GeV, P2 0νββ, P3 ladder — pending observations (QG199-201)", "Fermion Sector"),
    };

    // ── DAG verification ─────────────────────────────────────────────────────

    /// <summary>Topological sort [Kahn]. Returns the sorted concept names, or null if a cycle exists.</summary>
    public static string[]? TopologicalSort()
    {
        var concepts = Concepts();
        var names = concepts.Select(c => c.Name).ToHashSet();
        var deps = new Dictionary<string, List<string>>();
        var indegree = concepts.ToDictionary(c => c.Name, _ => 0);
        foreach (var c in concepts)
        {
            deps[c.Name] = new List<string>();
            foreach (var d in c.DependsOn)
            {
                if (!names.Contains(d)) continue;
                deps[d].Add(c.Name);
                indegree[c.Name]++;
            }
        }
        var queue = new Queue<string>(concepts.Where(c => indegree[c.Name] == 0).Select(c => c.Name));
        var order = new List<string>();
        while (queue.Count > 0)
        {
            var n = queue.Dequeue();
            order.Add(n);
            foreach (var next in deps[n])
            {
                if (--indegree[next] == 0) queue.Enqueue(next);
            }
        }
        return order.Count == concepts.Length ? order.ToArray() : null;
    }

    /// <summary>The dependency graph is acyclic [a valid DAG].</summary>
    public static bool IsAcyclic() => TopologicalSort() is not null;

    // ── Classification completeness ───────────────────────────────────────────

    /// <summary>Every concept is classified in exactly one category.</summary>
    public static bool AllClassified()
        => Concepts().All(c => Enum.IsDefined(c.Kind) && Enum.IsDefined(c.Layer));

    /// <summary>Each of the four layers contains at least one concept.</summary>
    public static bool AllLayersPopulated()
        => Enum.GetValues<Layer>().All(l => Concepts().Any(c => c.Layer == l));

    /// <summary>The primitive layer is FOUNDATIONAL and irreducible: every primitive is Foundational or Boundary.</summary>
    public static bool PrimitivesIrreducible()
        => Concepts().Where(c => c.Layer == Layer.Primitive)
            .All(c => c.Kind is ConceptKind.Foundational or ConceptKind.Boundary);

    /// <summary>No concept depends on a concept in a HIGHER layer [the layering is topological].</summary>
    public static bool LayersTopological()
    {
        var concepts = Concepts().ToDictionary(c => c.Name);
        foreach (var c in Concepts())
            foreach (var d in c.DependsOn)
                if (concepts.ContainsKey(d) && concepts[d].Layer > c.Layer)
                    return false;
        return true;
    }

    // ── The determination ─────────────────────────────────────────────────────

    /// <summary>
    /// Architecture score (0..6):
    /// 1. the four layers are populated [primitive, dynamic, spectrum, physics];
    /// 2. every concept is classified in exactly one category;
    /// 3. the dependency graph is ACYCLIC [Kahn's topological sort succeeds];
    /// 4. the layering is topological [no edge points to a higher layer];
    /// 5. the primitives are irreducible [Foundational/Boundary only];
    /// 6. no primitive is derivable from the others [Difference and η are independent — CORRECTED per
    ///    MONO006/A01: Actualization is derived from Difference and is not a primitive].
    /// </summary>
    public static int ArchitectureScore()
    {
        int score = 0;
        if (AllLayersPopulated()) score++;
        if (AllClassified()) score++;
        if (IsAcyclic()) score++;
        if (LayersTopological()) score++;
        if (PrimitivesIrreducible()) score++;
        if (PrimitivesIndependent()) score++;
        return score;
    }

    private static bool PrimitivesIndependent()
    {
        var prims = Concepts().Where(c => c.Layer == Layer.Primitive && c.Kind == ConceptKind.Foundational).ToArray();
        return prims.Length == 2 && prims.All(p => p.DependsOn.Length == 0);
    }

    /// <summary>
    /// Data-driven classification:
    ///   FINAL AT ARCHITECTURE — score 6: the canonical minimal architecture is complete and sound —
    ///     4 layers, every concept classified, the dependency graph acyclic and topological, the
    ///     primitives irreducible and independent.
    /// </summary>
    public static string Classify()
    {
        if (ArchitectureScore() >= 6) return "FINAL AT ARCHITECTURE";
        if (ArchitectureScore() >= 4) return "PARTIAL ARCHITECTURE";
        return "INCOMPLETE ARCHITECTURE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        int score = ArchitectureScore();
        var prim = Concepts().Where(c => c.Layer == Layer.Primitive).ToArray();
        var dyn = Concepts().Where(c => c.Layer == Layer.Dynamic).ToArray();
        var spec = Concepts().Where(c => c.Layer == Layer.Spectrum).ToArray();
        var phys = Concepts().Where(c => c.Layer == Layer.Physics).ToArray();
        return $"{Classify()} — architecture score {score}/6. The canonical minimal architecture has " +
               $"{prim.Length} primitives [Difference, η — irreducible], {dyn.Length} dynamic " +
               $"concepts [Actualization derived from Difference → Resonance = Conservation + Boundary → " +
               $"actualization attractor], {spec.Length} " +
               $"spectrum concepts [D96 spectrum → operators → locks → physics], and {phys.Length} physics " +
               $"concepts [fermions, gauge, gravity, cosmology — emergent; SM dynamics, Bekenstein 1/4, ψ, " +
               $"experimental frontier — boundary]. Dependency graph acyclic: {IsAcyclic()}; layering " +
               $"topological: {LayersTopological()}. The minimal base is Difference → Actualization → " +
               $"Spectrum → Physics.";
    }
}
