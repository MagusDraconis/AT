namespace AT.Core.ResearchXH;

/// <summary>
/// AT-MONO001 — Quantum Gravity Monograph Assembly. Assembles the complete monograph structure from
/// QG0-QG225 (226 phases). Each chapter maps to its source QG phases. Assembly only — no new physics,
/// no new derivations. Deterministic.
///
/// The 18-chapter structure (with the source phases that carry each chapter's content):
///   1.  EXECUTIVE SUMMARY      — the QG closure chain and readiness verdicts.
///   2.  PRIMITIVE ONTOLOGY     — the two primitives (Q-events, ψ), the counting measure, dependency audit.
///   3.  Q-EVENTS               — actualization, branching, causal order, meaning, correlations.
///   4.  EMERGENT DENSITY ρ     — the counting measure, energy origin, octave organization.
///   5.  QUANTUM MECHANICS      — amplitudes, phase, complex structure, interference, spin, entanglement,
///                               measurement (the QM pillar, closed by QG216/218/220).
///   6.  SPACETIME EMERGENCE    — dimension, effective dimension, metric ansatz, 2D→3D bridge, dynamics.
///   7.  GRAVITY                — Newton constant, Einstein structure, Hawking, frame dragging, GPS, optics,
///                               mass-radius, Bekenstein boundary, perihelion.
///   8.  MATTER                 — energy origin, matter = deficit, the deficit dust T_μν, α=0.
///   9.  STANDARD MODEL         — color/SU(3), generations, families, gauge sector, CKM/PMNS, weak/Higgs,
///                               precision EW, neutrino masses, quark running, lepton/family exact laws.
///  10.  TENSOR SECTOR ψ        — the ψ origin arc, spin-2, capacity/excitation, observables, the boundary.
///  11.  VALIDATION PROGRAM     — spectrum, completeness, coverage, robustness, readiness.
///  12.  BLIND TESTS            — Higgs blind reconstruction, leave-one-out validation.
///  13.  ANTI-FIT AUDITS        — overfit detection, anti-fit audits, reaudits.
///  14.  PREDICTION REGISTRY    — pre-registration (P1/P2/P3), the immutable registry lock.
///  15.  PREDICTION OUTCOMES    — evidence audits, statistics, the outcome dashboard.
///  16.  DISCUSSION             — frontier, theory completion, QG status, paper readiness.
///  17.  LIMITATIONS            — Bekenstein 1/4 impossibility, cosmology out of scope, partial items, ψ.
///  18.  FALSIFICATION PATHS    — explicit falsification conditions and forward outcome paths.
///
/// The source phases are listed in dependency order where possible (root → derived), matching the acyclic
/// derivation graph verified by QG225. No new physics — assembly of existing phase results only.
/// </summary>
public static class MonographAssembly
{
    /// <summary>A monograph chapter: title, one-line scope, and the source QG phases that carry it.</summary>
    public sealed record Chapter(
        int Index,
        string Title,
        string Scope,
        string[] SourcePhases);

    /// <summary>The complete 18-chapter monograph structure.</summary>
    public static Chapter[] Chapters() => new[]
    {
        new Chapter(1, "Executive Summary",
            "The complete AT quantum-gravity program at a glance: two primitives, both pillars derived, COMPLETE QG, MONOGRAPH READY.",
            new[] { "QG0", "QG51", "QG215", "QG219", "QG221", "QG223", "QG224", "QG225" }),

        new Chapter(2, "Primitive Ontology",
            "The exact two-primitive structure (Q-events, ψ), the counting measure, the final boundary, and the dependency audit.",
            new[] { "QG1", "QG11", "QG23", "QG24", "QG40", "QG50", "QG51", "QG53", "QG55", "QG68" }),

        new Chapter(3, "Q-Events",
            "The actualization process: branching, causal order, physical meaning, criticality, and correlations.",
            new[] { "QG1", "QG7", "QG11", "QG29", "QG30", "QG34", "QG104" }),

        new Chapter(4, "Emergent Density ρ",
            "The counting measure as microscopic actualization density; energy origin; octave organization; the base chain.",
            new[] { "QG0", "QG1", "QG4", "QG89", "QG116", "QG155" }),

        new Chapter(5, "Quantum Mechanics",
            "The QM pillar from Q-events: amplitude magnitude |ψ|²=ρ (QG216), phase θ=2πk/N (QG220), complex structure (QG218), interference, spin, entanglement, measurement.",
            new[] { "QG61", "QG62", "QG63", "QG65", "QG66", "QG67", "QG70", "QG71", "QG72", "QG73", "QG74",
                    "QG216", "QG218", "QG220" }),

        new Chapter(6, "Spacetime Emergence",
            "Dimension from network structure; the metric ansatz g=ρ^(2/d)η; the 2D→3D bridge; Planck regime and fluctuations; native dynamics.",
            new[] { "QG2", "QG3", "QG5", "QG10", "QG14", "QG15", "QG197", "QG207", "QG222" }),

        new Chapter(7, "Gravity",
            "All gravity observables from ρ: Newton constant, Einstein structure, Hawking temperature, frame dragging, GPS, conformal optics, mass-radius, Bekenstein boundary, perihelion.",
            new[] { "QG0", "QG6", "QG12", "QG13", "QG103", "QG181", "QG182", "QG183", "QG184", "QG185", "QG186",
                    "QG187", "QG196", "QG198", "QG209", "QG213", "QG222" }),

        new Chapter(8, "Matter",
            "Energy as actualization rate; matter = the deficit ρ̄−ρ; the conserved deficit dust T_μν=(ρ̄−ρ)v_μv_ν; α=0 flat rotation.",
            new[] { "QG89", "QG194", "QG195", "QG196", "QG206" }),

        new Chapter(9, "Standard Model",
            "The full SM from D96: color/SU(3), generations, families, gauge sector, CKM/PMNS, weak/Higgs, precision EW, neutrino masses, quark running, exact lepton/family laws.",
            new[] { "QG60", "QG78", "QG79", "QG80", "QG81", "QG82", "QG84", "QG85", "QG118", "QG134", "QG138",
                    "QG140", "QG149", "QG150", "QG151", "QG153", "QG154", "QG155", "QG156", "QG157", "QG158",
                    "QG159", "QG160", "QG161", "QG162", "QG163", "QG164", "QG165", "QG166", "QG167", "QG168",
                    "QG169", "QG171", "QG172", "QG173", "QG174", "QG175", "QG178", "QG179", "QG180",
                    "QG203", "QG204", "QG205", "QG209", "QG210", "QG211" }),

        new Chapter(10, "Tensor Sector ψ",
            "The second primitive: tensor-sector necessity, spin-2 origin, capacity forced/excitation derived, connectivity interpretation, observables, the ontological boundary.",
            new[] { "QG16", "QG17", "QG18", "QG19", "QG22", "QG23", "QG24", "QG25", "QG43", "QG44", "QG45",
                    "QG46", "QG47", "QG48", "QG49", "QG50", "QG52", "QG54", "QG56", "QG57", "QG58", "QG59",
                    "QG103", "QG186", "QG208", "QG213", "QG223" }),

        new Chapter(11, "Validation Program",
            "The measurement/validation machinery: network spectrum, completeness, coverage, robustness, spectral classes, and the readiness verdict.",
            new[] { "QG76", "QG104", "QG105", "QG106", "QG107", "QG108", "QG109", "QG110", "QG111", "QG112",
                    "QG113", "QG114", "QG115", "QG117", "QG119", "QG170", "QG224", "QG225" }),

        new Chapter(12, "Blind Tests",
            "Pre-registered-before-data reconstructions: the Higgs blind reconstruction and the 12-observable leave-one-out validation.",
            new[] { "QG176", "QG177" }),

        new Chapter(13, "Anti-Fit Audits",
            "The methodology defense: overfit detection, the first anti-fit audit, the reaudits, and the clean structural era.",
            new[] { "QG147", "QG148", "QG189", "QG190", "QG215" }),

        new Chapter(14, "Prediction Registry",
            "The three pre-registered predictions and the immutable registry lock: P1 106 GeV, P2 0νββ m_ββ, P3 sector ladder.",
            new[] { "QG132", "QG188", "QG190", "QG191", "QG192", "QG193", "QG194" }),

        new Chapter(15, "Prediction Outcomes",
            "External validation: the evidence audits (P1, sector ladder), ladder statistics, and the single-source outcome dashboard.",
            new[] { "QG199", "QG200", "QG201", "QG202", "QG203" }),

        new Chapter(16, "Discussion",
            "Frontier analysis, theory completion (~95%), the QG closure chain, and the MONOGRAPH READY verdict.",
            new[] { "QG212", "QG214", "QG223", "QG224" }),

        new Chapter(17, "Limitations",
            "Stated boundaries: Bekenstein 1/4 impossibility (no π), cosmology out of scope, partial SM items, the ψ primitive as boundary.",
            new[] { "QG76", "QG77", "QG85", "QG135", "QG136", "QG139", "QG142", "QG143", "QG144", "QG146",
                    "QG152", "QG185", "QG196", "QG223" }),

        new Chapter(18, "Falsification Paths",
            "The explicit falsification conditions of every registered prediction and the forward outcome paths (PENDING→SUPPORTED→CONFIRMED / DISFAVORED→FALSIFIED).",
            new[] { "QG132", "QG190", "QG191", "QG192", "QG193", "QG202", "QG203" }),
    };

    // ── Structure checks ──────────────────────────────────────────────────────

    /// <summary>Number of chapters.</summary>
    public static int ChapterCount() => Chapters().Length;

    /// <summary>The chapters are numbered 1..18 in order.</summary>
    public static bool ChaptersSequential()
    {
        var c = Chapters();
        for (int i = 0; i < c.Length; i++)
            if (c[i].Index != i + 1) return false;
        return true;
    }

    /// <summary>Every chapter has a non-empty source-phase list.</summary>
    public static bool AllChaptersHaveSources()
        => Chapters().All(ch => ch.SourcePhases.Length > 0);

    /// <summary>Total distinct phases referenced across all chapters.</summary>
    public static int DistinctPhaseCount()
        => Chapters().SelectMany(ch => ch.SourcePhases).Distinct().Count();

    /// <summary>Total phase references (with chapter repeats).</summary>
    public static int TotalPhaseReferences()
        => Chapters().Sum(ch => ch.SourcePhases.Length);

    /// <summary>
    /// Coverage of the register: fraction of the 226 phases (QG0-QG225) referenced by at least one chapter.
    /// The unlisted phases are support/audit phases folded into the validation chapters.
    /// </summary>
    public static double RegisterCoverageFraction()
    {
        int total = 226;   // QG0 .. QG225 (the 116.5 sub-phase is not a chapter source)
        return (double)DistinctPhaseCount() / total;
    }

    /// <summary>The monograph title and subtitle.</summary>
    public static (string Title, string Subtitle) MonographTitle() =>
        ("Quantum Gravity from a Counting Measure",
         "A two-primitive theory of Q-events, emergent spacetime, and the standard model — with pre-registered predictions");

    /// <summary>Suggested reading order is the chapter order 1..18.</summary>
    public static string[] ReadingOrder()
        => Chapters().Select(c => $"{c.Index}. {c.Title}").ToArray();
}
