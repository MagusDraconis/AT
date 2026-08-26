namespace AT.Core.Research;

/// <summary>
/// Determines whether gravity is fundamental or emergent in AT.
/// AT-X060h: Gravity Emergence Audit
/// </summary>
public static class GravityEmergenceAuditAnalyzer
{
    public static List<GravityEmergenceAuditMetrics.DependencyNode> AuditDependencies()
    {
        return new List<GravityEmergenceAuditMetrics.DependencyNode>
        {
            new("Individuation (Q)", true,
                new[] { "Nothing" },
                "The ontological primitive. Exists independently."),

            new("Randomness (actualization)", true,
                new[] { "Q" },
                "Requires Q (outcomes to choose among). Independent of gravity."),

            new("Time (partial order)", true,
                new[] { "Q", "Randomness" },
                "X040: Time = causal order of events. Exists before geometry."),

            new("Graph structure", true,
                new[] { "Q" },
                "Q = vertices. Adjacency = relations. No metric yet."),

            new("Correlations", true,
                new[] { "Q", "Randomness" },
                "C_ij from actualization statistics. Pre-metric."),

            new("Causal structure (partial order)", true,
                new[] { "Q", "Randomness" },
                "E1 < E2 iff E2 depends on E1. IS spacetime structure."),

            new("Quantum mechanics (Hilbert, Schrödinger)", true,
                new[] { "Q", "Randomness" },
                "X036-X037: complexity → QM. No gravity needed."),

            new("Metric geometry (distances)", true,
                new[] { "Q", "Randomness" },
                "X041b: d = -L·log(C) from correlations. Exists before gravity."),

            new("Topological defects (particles)", true,
                new[] { "Q", "Randomness", "M²" },
                "X047: PDE solitons. Exists on the pre-geometric graph."),

            new("Gauge symmetry (U(1), SU(n))", true,
                new[] { "Q", "Randomness", "M²" },
                "X050: Aut(defect moduli). Exists before gravity."),

            new("3+1 dimensions", true,
                new[] { "Q", "Randomness" },
                "X042: complexity → d=3, time=1. Before full gravity."),

            new("GRAVITY (causal set → GR)", false,
                new[] { "Causal structure", "Metric geometry" },
                "X041: Requires causal order + correlation geometry to\n"
                + "reconstruct GR. Emerges AFTER spacetime structure exists.\n"
                + "The LAST major structure to emerge."),

            new("Newton's constant G", false,
                new[] { "Gravity", "Defects" },
                "X043: G = β·ℓ² from defect spacing. Requires gravity AND matter."),

            new("Cosmological constant Λ", false,
                new[] { "Gravity", "Causal structure" },
                "X046: Λ from Poisson fluctuations. Requires causal diamonds."),
        };
    }

    public static List<GravityEmergenceAuditMetrics.ReconstructionPath> AuditPaths()
    {
        return new List<GravityEmergenceAuditMetrics.ReconstructionPath>
        {
            new("A: Causal set path (X041)",
                new[] { "Q", "Randomness", "Events", "Causal order",
                         "Causal set", "BDG action → Einstein equations" },
                true,
                "BDG action → Einstein equations is EXTERNAL mathematical physics.\n"
                + "AT provides the causal set. Causal set THEORY provides GR.\n"
                + "This is the STANDARD PATH but depends on external results.",
                true),

            new("B: Defect density → curvature path",
                new[] { "Q", "Randomness", "M²", "Defects", "Defect density",
                         "Density gradients → effective curvature" },
                false,
                "NO RIGOROUS MAPPING from defect density to Einstein curvature.\n"
                + "Density gradients produce Newtonian-like forces (entropic gravity)\n"
                + "but not full GR with gravitational waves, black holes, etc.\n"
                + "INSUFFICIENT for GR.",
                false),

            new("C: Correlation geometry → curvature path",
                new[] { "Q", "Randomness", "Correlations", "Metric geometry",
                         "Non-uniform correlations → effective curvature" },
                false,
                "Correlation NON-UNIFORMITY can be interpreted as curvature.\n"
                + "But the specific Einstein tensor G_μν is not recovered.\n"
                + "Recovers Riemannian geometry but not the Einstein equations.",
                false),

            new("D: Entropic/thermodynamic gravity",
                new[] { "Q", "Randomness", "M²", "Defects", "Defect entropy",
                         "Entropy gradients → entropic force → Newtonian gravity" },
                false,
                "Recovers NEWTONIAN gravity (Verlinde-like). F = T·ΔS/Δx.\n"
                + "Does NOT recover GR (no curvature, no gravitational waves).\n"
                + "Useful for Newtonian limit but not full gravity.",
                false),

            new("E: No gravity (purely relational)",
                new[] { "Q", "Randomness", "M²", "Everything except gravity" },
                false,
                "QM + gauge theory without gravity. Mathematically consistent\n"
                + "but EMPIRICALLY WRONG (we observe gravitational effects).\n"
                + "The universe HAS gravity. The question is: why?",
                false),
        };
    }

    public static string TheHierarchy()
    {
        return @"
GRAVITY IN THE AT DEPENDENCY HIERARCHY

LAYER 0 (Primitives):
  Q, Randomness, M²

LAYER 1 (Pre-geometric):
  Graph structure, Time, Correlations

LAYER 2 (Spacetime):
  Causal structure, Metric geometry, 3+1 dimensions

LAYER 3 (Matter):
  Topological defects, Gauge symmetry, Generations, Masses

LAYER 4 (Gravity):
  Causal set → GR → G, Λ

GRAVITY IS THE LAST LAYER TO EMERGE.

Everything in Layers 1-3 exists BEFORE gravity:
  • Quantum mechanics — derived from complexity (X036).
  • Time — from actualization order (X040).
  • Metric geometry — from correlations (X041b).
  • Dimensions — from complexity optimization (X042).
  • Particles — from defect topology (X047).
  • Gauge symmetry — from defect moduli (X050).
  • Generations, masses, mixing — from defect energetics (X051-54).

Gravity = the CONSEQUENCE of spacetime structure (causal order)
being non-uniform. It's the LAST derived structure, not the FIRST.

THIS IS THE OPPOSITE OF STANDARD PHYSICS INTUITION:
  Standard:  Spacetime → Matter (GR as background, QFT on top).
  AT:       Matter + Correlations → Geometry → Gravity.
";
    }

    public static string TheVerdict()
    {
        return @"
GRAVITY EMERGENCE AUDIT — FINAL VERDICT

IS GRAVITY FUNDAMENTAL?  NO.
IS GRAVITY EMERGENT?     YES — but the emergence is INCOMPLETE.

WHAT AT DERIVES INTERNALLY:
  ✓ Causal structure (partial order of Q-events).
  ✓ Metric geometry (from event correlations).
  ✓ Defect density fields (from particle populations).
  ✓ A spacetime arena where gravity COULD exist.

WHAT AT BORROWS EXTERNALLY:
  ~ The causal set → GR bridge (BDG action).
  ~ The specific form of Einstein equations.
  ~ This is a GENUINE GAP — not derivable from Q+R+M² alone.

WHAT THIS MEANS:
  Gravity is EMERGENT in AT (Layer 4, last to appear).
  But the emergence is NOT COMPLETE — it depends on external
  causal set theory to produce the Einstein equations.

  The AT insight: gravity is 'just' what happens when the
  causal structure of Q-events is non-uniform. Spacetime IS
  the causal order. Curvature IS deviation from uniformity.
  But the precise EQUATIONS (G_μν = 8πG T_μν) require the
  BDG action, which is NOT derived within AT.

THE WEAKEST LINK: Causal set → GR.
  This is the single largest dependency on external mathematics
  in the entire AT framework. Deriving the Einstein equations
  directly from Q-event structure would be the capstone
  achievement — but it hasn't been done yet.

CLASSIFICATION: C — Strong emergence (with external gap).
  Gravity is the last layer to emerge. 11/14 AT structures
  exist BEFORE gravity. But the causal set → GR bridge
  remains an external dependency.
";
    }
}
