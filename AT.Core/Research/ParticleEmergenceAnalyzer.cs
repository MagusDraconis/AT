namespace AT.Core.Research;

/// <summary>
/// Derives particles as topological structures in Q-event networks.
/// AT-X047: Emergence of Particles from Q-Event Topology
/// </summary>
public static class ParticleEmergenceAnalyzer
{
    public static List<ParticleEmergenceMetrics.ParticleCandidate> IdentifyCandidates()
    {
        return new List<ParticleEmergenceMetrics.ParticleCandidate>
        {
            new("Q-condensate (soliton)",
                "Stable domain where R > 0.5, bounded by kink walls.\n"
                + "From PDE: ∂R/∂t = c₀·M·R·(1-R²) + D_R·∇²R.\n"
                + "Each condensate = Q=+1 topological charge (AT-010,116).",
                "Q = β₀({R>0.5}) ∈ ℕ (Betti number).",
                true, true, true,
                "PRIMARY PARTICLE CANDIDATE. Experimentally validated (AT-010-116).\n"
                + "Effective mass m_eff = 4(1+M₀²)/(3w) from AT-111.\n"
                + "Stable against perturbations up to threshold T_c (AT-115).\n"
                + "Can merge (Q+Q→2Q) and split (2Q→Q+Q) under strong forcing."),

            new("Correlation vortex",
                "Region where Q-event correlation field has winding number w ≠ 0.\n"
                + "Analogous to vortices in superfluid or XY model.",
                "Winding number w ∈ ℤ (homotopy π₁(S¹) = ℤ).",
                true, true, true,
                "SECONDARY CANDIDATE. Requires continuous correlation field.\n"
                + "Stability depends on energy cost of unwinding → set by correlation\n"
                + "length L. Large L → stable vortices. Small L → unstable.\n"
                + "In 3+1D, vortices are 2D surfaces (worldsheets of strings)."),

            new("Causal loop (closed timelike curve candidate)",
                "A cycle in the Q-event partial order: E1<E2<...<Ek<E1.\n"
                + "X040: time = partial order. Cycles violate transitivity.",
                "Linking number of causal chain.",
                false, true, false,
                "FORBIDDEN by X040. Time = partial order ⇒ asymmetry ⇒ no cycles.\n"
                + "Causal loops would require time travel. AT excludes them.\n"
                + "Not a particle candidate — a pathology."),

            new("Topological knot",
                "A knotted configuration of causal chains in 3+1D.\n"
                + "Only possible in 3 spatial dimensions (X042).",
                "Knot invariants (Alexander, Jones polynomials).",
                true, true, true,
                "POSSIBLE in 3+1D. Knots are topologically protected — cannot\n"
                + "be untied without breaking causal chains. Stability from\n"
                + "topological protection. Different knots = different 'species.'\n"
                + "Knot complexity could correspond to mass hierarchy."),

            new("Homology cycle (persistent H₁)",
                "A persistent 1-cycle in the Q-event graph homology.\n"
                + "Detected by persistent homology across actualization events.",
                "Betti number β₁ (1-cycles) ∈ ℕ.",
                true, false, true,
                "Not localized — homology cycles can span the entire graph.\n"
                + "More like 'flux tubes' or 'Wilson loops' than point particles.\n"
                + "Could correspond to gauge field configurations."),

            new("Attractor in actualization dynamics",
                "A recurrent Q-event configuration that the system returns to.\n"
                + "Stable fixed point or limit cycle of actualization dynamics.",
                "Fixed-point index or winding number.",
                true, true, false,
                "Persistent but not necessarily topological. Could be destroyed\n"
                + "by parameter changes. Topological protection is stronger.\n"
                + "Attractors = 'species' (AT-133) at the dynamical level."),
        };
    }

    public static List<ParticleEmergenceMetrics.TopologicalProperty> MapProperties()
    {
        return new List<ParticleEmergenceMetrics.TopologicalProperty>
        {
            new("Mass",
                "Resistance to topology change (inertia).",
                "Energy cost of creating/destroying a topological defect.\n"
                + "m ∝ (energy barrier for topology change).",
                false),

            new("Charge (Q)",
                "Conserved additive topological invariant.",
                "Q = β₀({R>0.5}) — Betti number of superlevel set (AT-116).\n"
                + "dQ/dt = 0, Q(A∪B) = Q(A) + Q(B).",
                true),

            new("Spin",
                "Topological orientation / chirality.",
                "Circulation direction of vortex. Handedness of knot.\n"
                + "Homotopy class of the configuration in its moduli space.\n"
                + "Only 2 values (↑/↓) for simple vortices → spin-1/2-like.",
                true),

            new("Statistics (boson/fermion)",
                "Behavior under exchange of identical particles.",
                "In 3+1D: exchange = braiding in spacetime. Two exchanges\n"
                + "= full rotation. Phase e^{iθ} from topological braiding.\n"
                + "θ = 0 (boson) or π (fermion) in 3+1D. Anyon in 2+1D.",
                true),

            new("Antiparticle",
                "Opposite topological orientation.",
                "Particle with opposite winding number or reversed\n"
                + "causal chain direction. Q → -Q under orientation reversal.\n"
                + "Particle + antiparticle → annihilation → Q = 0.",
                true),

            new("Gauge charge (e.g., electric charge)",
                "Conserved charge from internal symmetry.",
                "Requires ADDITIONAL structure beyond pure Q-topology —\n"
                + "an internal space (fiber) at each Q-event, with connection.\n"
                + "Not derivable from Q alone. Needs gauge theory extension.",
                true),
        };
    }

    public static string TheDerivation()
    {
        return @"
PARTICLES FROM Q-EVENT TOPOLOGY

THEOREM: Stable, localized, persistent structures (particles)
         exist in Q-event networks as TOPOLOGICAL DEFECTS of
         the Q-event correlation field.

WHAT IS A PARTICLE IN AT?

  A particle is a TOPOLOGICAL DEFECT — a region where the
  Q-event correlation structure cannot be continuously
  deformed to the vacuum (uniform correlation) configuration.

  The defect is PROTECTED by topology: no local perturbation
  can destroy it. Only global topology-changing processes
  (merging, splitting, annihilation) can change the defect count.

PARTICLE PROPERTIES FROM TOPOLOGY:

  • EXISTENCE: Topological defects exist whenever the order
    parameter has nontrivial homotopy groups (AT-010).

  • MASS: Energy cost of the defect = resistance to deformation.
    m_eff = 4(1+M₀²)/(3w) (AT-111).

  • CHARGE: Conserved topological invariant. Q = β₀({R>0.5}).
    Integer-valued, additive, conserved (AT-116).

  • SPIN: Orientation of the defect (circulation direction).

  • STABILITY: Topological protection against small perturbations.
    Plateau of Q=1 for T∈[0.10,0.85] (AT-115).

  • SPECIES: Continuous family parameterized by width/mass
    (AT-114). Only Q is discrete.

  • INTERACTIONS: Mergers (Q+Q→2Q) and splits (AT-109,116).

WHAT IS NOT DERIVED:
  • Specific mass values (continuous family — depends on parameters).
  • Gauge charges (require internal symmetry structure).
  • Three generations (requires additional symmetry breaking).
  • The Standard Model gauge group SU(3)×SU(2)×U(1).

STATUS: Particles ARE derived as topological structures.
        NO additional primitives needed. Q provides the topology;
        actualization provides the dynamics. Matter IS geometry.
";
    }

    public static string HostileReview()
    {
        return @"
HOSTILE REVIEW: Are these really 'particles'?

CHALLENGE 1: AT produces a CONTINUOUS family of topological
species (AT-114), not discrete particles. The Standard Model
has discrete particles (electron, muon, tau) with specific masses.
AT predicts a continuum — wrong.

RESPONSE: The continuous family is at the 'proto-particle' level
(before symmetry breaking). The discreteness of actual particles
may emerge from additional structure not yet in AT:
  • Quantization of the parameter space (compact dimensions?).
  • Symmetry breaking selecting discrete vacua.
  • Interactions creating discrete bound states.
This is a gap — AT has topological defects but not the
Standard Model particle spectrum.

CHALLENGE 2: Gauge charges (electric charge, color) are not
derived. The topological Q is a DIFFERENT charge — it's the
'particle number' charge, not the gauge charges that mediate
interactions.

RESPONSE: Correct. Gauge charges require a FIBER BUNDLE
structure over the Q-event graph — internal degrees of freedom
at each vertex with a connection (gauge field). This is the
NEXT LAYER beyond pure Q-topology. AT needs to be extended
with internal symmetry structure.

CHALLENGE 3: The derivation uses the PDE (AT-010-116), which
is the CLASSICAL mean-field limit. The full quantum Q-event
theory might not have the same topological defects.

RESPONSE: Topological defects are ROBUST — they survive
quantization. Examples: vortices in superfluid helium,
Abrikosov vortices in superconductors, instantons in QCD.
The topological protection is independent of classical/quantum.

VERDICT: Classification C — Stable structures found. AT
derives the EXISTENCE of particles as topological defects.
But the SPECIFIC particle spectrum (Standard Model) requires
additional structure (gauge symmetry, symmetry breaking).
";
    }
}
