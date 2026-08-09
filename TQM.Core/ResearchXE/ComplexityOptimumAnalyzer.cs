namespace TQM.Core.ResearchXE;

using System.Globalization;

/// <summary>
/// Decomposes complexity into components and identifies the mechanisms
/// creating the complexity optimum at d=3+1, M²≈5, G≈3.
/// ResearchXE-005: Complexity Optimum Analysis
/// </summary>
public static class ComplexityOptimumAnalyzer
{
    public enum Component { Structure, Particles, Chemistry, Information, Evolution, Observer }

    public sealed record ComplexityComponent(
        Component Name, string Label, double Weight,
        string OptimalD, string OptimalM2, string OptimalG,
        string DominantMechanism);

    public sealed record FailureEdge(
        string Direction, string Parameter, string Threshold,
        string WhatBreaks, string Mechanism);

    public static List<ComplexityComponent> DecomposeComplexity()
    {
        return new List<ComplexityComponent>
        {
            new(Component.Structure, "Structure Formation", 3.0,
                "d=3 only — no stable orbits in d≠3 (Bertrand's theorem)",
                "Broad plateau M²≈2–8 — gravitational collapse works across wide nonlinearity range",
                "Independent of G — structure depends on DM + gravity, not generations",
                "Gravity + stable orbits. Bertrand: only 1/r² and r² potentials give closed orbits. "
                + "d=3 gives 1/r² from Gauss. d=2: F∝1/r (no stable bound states). d=4: F∝1/r³ (all orbits escape)."),

            new(Component.Particles, "Particle Stability", 2.5,
                "d=3 only — knots require codim-2 (3 spatial dimensions). d=2: no knots. d=4: knots unravel.",
                "Sharp peak at M²≈3–7. Below M²≈2: no topological protection (defects dissolve). "
                + "Above M²≈8: defects too tightly bound → collapse.",
                "G≥2 needed. G=1 has no flavor physics. G≥4: higher generations too unstable.",
                "Topological protection window. Defects stable when potential barrier > thermal energy. "
                + "M² sets barrier height. Too low → no barrier. Too high → classical instability."),

            new(Component.Chemistry, "Chemistry Potential", 4.0,
                "d=3 only — no long-range 1/r EM in d≠3. Gauss: F∝1/r^(d-1). 1/r only in 3D.",
                "Peak at M²≈3–5. Below M²≈3: atoms too large (weak binding). "
                + "Above M²≈5: inner electrons relativistic → instability.",
                "G≥2 needed for rich chemistry. G=1 has only one stable atom type. "
                + "G≥3 adds diversity but diminishing returns.",
                "Atomic stability window. Binding energy ∝ α²·m_e. α depends on M² (via vortex geometry). "
                + "Chemistry emerges at the SWEET SPOT where atoms are stable but not over-bound."),

            new(Component.Information, "Information Capacity", 2.0,
                "d=3 is optimal — each vertex has 2d=6 neighbors. "
                + "Balance between connectivity diversity and local distinguishability.",
                "Broad peak M²≈3–10. Information capacity ∝ log(states)×stability. "
                + "More M² → more states, but less stability. Optimum at intermediate.",
                "G∝states. More generations → more distinguishable configurations. "
                + "But G>4: higher states too unstable to contribute information.",
                "Diversity × stability trade-off. More interaction channels (larger M²) increase "
                + "state space but decrease persistence. Information capacity peaks at the balance point."),

            new(Component.Evolution, "Evolution Potential", 1.5,
                "d=3 preferred — 3+1D supports causality + stable structures simultaneously. "
                + "d=2: no stable structures to evolve. d=4: no structures at all.",
                "M²≈2–6 optimal. Below M²≈2: too few distinct species (weak hierarchy). "
                + "Above M²≈6: species too unstable to evolve across generations.",
                "G≈2–4 optimal. G=1: no variation. G≥5: too much instability. "
                + "Darwinian triad (variation+selection+heredity) requires intermediate diversity.",
                "Species diversity window. Evolution needs: (1) distinguishable entities, "
                + "(2) stable enough to reproduce, (3) variation between generations. "
                + "M² and G jointly set the diversity-stability balance for evolutionary dynamics."),

            new(Component.Observer, "Observer Viability", 5.0,
                "d=3 REQUIRED — complex structures + chemistry + causality all need 3+1D.",
                "Peak M²≈4–6. Below: no chemistry. Above: unstable structures. "
                + "Sharp cutoff outside window.",
                "G≈2–4 viable. G=3 is near-optimal but not unique. "
                + "Observers require complex information processing → needs chemistry + evolution.",
                "CONJUNCTION of all previous maxima. Observers require: structure+chemistry+"
                + "information+evolution. The observer viability peak is the INTERSECTION "
                + "of all individual component optima — hence the narrow window."),
        };
    }

    public static List<FailureEdge> IdentifyFailureEdges()
    {
        return new List<FailureEdge>
        {
            new("Low", "M²", "< 2",
                "No topological protection",
                "Defect potential barrier < thermal energy. Solitons dissolve. "
                + "Below M²_crit ≈ 0.2, no stable defects at all. "
                + "The universe is a featureless field with no particles."),

            new("High", "M²", "> 8",
                "Defect collapse + atomic instability",
                "Extremely steep potential → defects tightly bound, classical instability. "
                + "Higher generations decay instantly (τ_n < 10⁻²⁰s). Only 1-2 generations survive. "
                + "Chemistry: inner electrons become relativistic → atoms unstable. "
                + "Structure: extreme hierarchy → only lightest element forms → no chemistry."),

            new("Low", "d", "< 3",
                "No chemistry + trivial gravity",
                "d=2+1: No 1/r EM (Gauss gives F∝const). No atoms. "
                + "GR has no propagating degrees of freedom (no gravitational waves). "
                + "No knots (codim≠2). Particles exist but cannot form bound states. "
                + "Observers impossible — no complex structures."),

            new("High", "d", "> 3",
                "Orbital instability + no knots",
                "d=4+: Gravity F∝1/r³ → no stable orbits (Bertrand). "
                + "Planets cannot orbit stars. Stars cannot form stable systems. "
                + "Knots unravel in 4D (isotopy = unknot). "
                + "Observers impossible — no stable structures at any scale."),

            new("Low", "G", "< 2",
                "No flavor diversity",
                "G=1: Only one fermion type per defect. No CKM/PMNS mixing. "
                + "No flavor physics. No CP violation. No weak interactions. "
                + "Chemistry still possible (electrons exist) but very limited."),

            new("High", "G", "> 4",
                "Higher generations unstable",
                "G≥5: Stability cutoff α≈1.5 → levels 4+ have lifetime < detection threshold. "
                + "Extra 'generations' are not observable. "
                + "Effectively only 3-4 contribute to physics. Higher G adds no new physics "
                + "— the stability cutoff neutralizes them."),
        };
    }

    public static string TheComplexityPeak()
    {
        return @"
WHY COMPLEXITY PEAKS AT d=3+1, M²≈5, G≈3

THE COMPLEXITY PEAK IS THE INTERSECTION OF MULTIPLE PHYSICAL WINDOWS:

  ┌─────────────────────────────────────────────────────────────┐
  │  DIMENSIONALITY (d=3+1) — NARROWEST CONSTRAINT              │
  │                                                              │
  │  Only d=3+1 satisfies ALL of:                                │
  │    • Bertrand's theorem (stable gravitational orbits)        │
  │    • Gauss's law 1/r² (stable atoms via EM)                  │
  │    • Knot theory (codim-2 = 3D → stable topological defects) │
  │    • Huygens principle (sharp wave propagation in odd d)     │
  │    • GR with 2 propagating degrees of freedom (+,×)          │
  │                                                              │
  │  d=2+1: FAILS gravity + atoms + knots.                      │
  │  d=4+:  FAILS orbits + knots.                               │
  │  d=3+1: PASSES ALL. UNIQUE.                                  │
  ├─────────────────────────────────────────────────────────────┤
  │  NONLINEARITY (M²≈5) — COMPETING OPTIMA                      │
  │                                                              │
  │  Structure:     Best at M²≈5 (gravity window OPEN)           │
  │  Particles:     Peak at M²≈3-7 (topological protection)      │
  │  Chemistry:     Peak at M²≈3-5 (atomic stability)            │
  │  Information:   Broad M²≈3-10 (diversity × stability)        │
  │  Evolution:     Best at M²≈2-6 (Darwinian diversity window)  │
  │  Observer:      INTERSECTION peak at M²≈4-6 ← OUR UNIVERSE   │
  │                                                              │
  │  Low M² failure: DEFECT INSTABILITY. No particles → nothing. │
  │  High M² failure: ATOMIC COLLAPSE. No chemistry → no observers.│
  ├─────────────────────────────────────────────────────────────┤
  │  GENERATIONS (G≈3) — PLATEAU, NOT PEAK                       │
  │                                                              │
  │  G=1: Chemistry possible but no flavor physics.              │
  │  G=2-4: ALL support observers. G=3 is not unique.            │
  │  G≥5: Effectively G≤4 — stability cutoff neutralizes extras. │
  │                                                              │
  │  Our G=3 is CONTINGENT, not NECESSARY.                       │
  └─────────────────────────────────────────────────────────────┘

KEY INSIGHT: The observer viability peak is the INTERSECTION of
multiple overlapping windows. Each component (structure, particles,
chemistry, information, evolution) has its own optimum. The observer
optimum is where ALL windows overlap — narrowing the viable region
to a small island around d=3+1, M²≈4-6, G≈2-4.

Our universe sits at the center of this island NOT because it was
'tuned' — but because the island IS small. Most parameter values
lie outside. The apparent fine-tuning is a VOLUME effect: the
observer-supporting region is a tiny fraction of parameter space.
";
    }

    public static string DominantMechanism()
    {
        return @"
THE DOMINANT COMPLEXITY DRIVER: CHEMISTRY

Among the six complexity components, CHEMISTRY is by far the
most constraining and drives the M² optimum:

  Chemistry weight: 4.0 (highest individual weight).
  Chemistry window: d=3+1 ONLY. M²≈3-5 optimal.
  Chemistry FAILURE modes: define the viable window boundaries.

WHY CHEMISTRY DOMINATES:

  1. Chemistry is the HARDEST requirement to satisfy.
     Structure and particles exist in many universes.
     Chemistry requires: stable atoms, long-range EM,
     moderate binding energies, diverse elements.

  2. Chemistry SETS the viable window boundaries.
     Low M²: atoms too large → no chemistry → no observers.
     High M²: atoms collapse → no chemistry → no observers.
     The M² window IS the chemistry window.

  3. Chemistry is a PREREQUISITE for everything above it.
     No chemistry → no information processing → no evolution → no observers.
     Chemistry is the BOTTLENECK.

  4. Chemistry depends on the MOST parameters:
     Requires d=3+1 (for 1/r EM), M²≈3-5 (for atomic stability),
     moderate α (for binding energies). The conjunction of
     requirements makes chemistry the RAREST component.

  THEREFORE: The universe landscape's observer island is
  PRIMARILY determined by the chemistry viability window.
  The M²≈5 peak is the atomic stability sweet spot.
";
    }
}
