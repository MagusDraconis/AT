using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class ArchitectureShapeOriginAnalyzer
{
    public static ASOResult RunFullAnalysis()
    {
        var props = BuildProperties();
        var mechs = BuildMechanisms();
        return new ASOResult(BuildA(),BuildB(props),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),props,mechs);
    }

    static ArchProperty[] BuildProperties()
    {
        return new ArchProperty[]
        {
            new ArchProperty("Architecture = frequency distribution","The organization of oscillation modes (QG-027/028)","DERIVED: architecture IS the frequency organization."),
            new ArchProperty("Architecture = attractor","A stable fixed point of actualization dynamics (QG-020)","DERIVED: matter persists because it is an attractor."),
            new ArchProperty("Architecture = topological structure","Winding structure (QG-034): n=1 vortex for leptons","DERIVED: topology fixes the STRUCTURE (winding), not the shape."),
            new ArchProperty("Specific shape (frequencies)","The frequency values (m_e : m_mu : m_tau = 1 : 207 : 3478)","UNDERIVED: the specific frequencies are not derived (QG-041)."),
        };
    }

    static ShapeMechanism[] BuildMechanisms()
    {
        return new ShapeMechanism[]
        {
            new ShapeMechanism("Attractor selection (QG-020)","Shapes = stable patterns","NO (landscape unspecified)","B: the MECHANISM is derived, the SPECIFICS are not."),
            new ShapeMechanism("Topology (QG-034)","Shapes = winding structures","NO (fixes n, not the frequency)","B: topology fixes the type, not the shape."),
            new ShapeMechanism("Frequency hierarchy (QG-027)","Shapes = frequency cascade","NO (couplings empirical)","B: hierarchy real, values underived."),
            new ShapeMechanism("Geometry of G (QG-055)","Shapes = points in G","NO (G gives the space, not the shape)","B: geometry gives the home, not the occupant."),
            new ShapeMechanism("Actualization (QG-006)","Shapes = historical draws","NO (contingent, QG-042)","B: contingency explains freedom, not the shape."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE ARCHITECTURE-SHAPE QUESTION");
        sb.AppendLine();
        sb.AppendLine("  QG-062: Y = overlap operator (QG-037).");
        sb.AppendLine("  But the ARCHITECTURE SHAPES (the inputs to the overlap)");
        sb.AppendLine("  remain unspecified.");
        sb.AppendLine();
        sb.AppendLine("  THE REMAINING MYSTERY:");
        sb.AppendLine("    Why do the frequency architectures have the SHAPES they");
        sb.AppendLine("    have (the frequency hierarchy m_e : m_mu : m_tau = 1 : 207 : 3478)?");
        sb.AppendLine();
        sb.AppendLine("  WHAT IS KNOWN (the chain):");
        sb.AppendLine("    - Architecture = frequency organization (QG-027/028).");
        sb.AppendLine("    - Architecture = attractor (QG-020): stable pattern.");
        sb.AppendLine("    - Architecture = topological structure (QG-034): winding.");
        sb.AppendLine("    - But the SPECIFIC shape (frequencies) is underived.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The architecture SHAPES are ATTRACTOR-SELECTED (stable)");
        sb.AppendLine("    and TOPOLOGY-FIXED (winding), but the SPECIFIC frequencies");
        sb.AppendLine("    are not derived. The shape is 'stable but underived'.");
        return sb.ToString();
    }

    static string BuildB(ArchProperty[] props)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ARCHITECTURE DEFINITION: WHAT IS THE SHAPE?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-36} {1}", "Property", "Status"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var p in props)
        {
            string st = p.Status.Length > 45 ? p.Status[..42]+"..." : p.Status;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-36} {1}", p.Property, st));
        }
        sb.AppendLine();
        sb.AppendLine("  THE THREE LAYERS OF 'ARCHITECTURE':");
        sb.AppendLine("    1. STRUCTURE (derived): the architecture IS a frequency");
        sb.AppendLine("       organization, a stable attractor, a winding structure.");
        sb.AppendLine("    2. TYPE (derived): the winding number n fixes the TYPE");
        sb.AppendLine("       (n=1 lepton, n=3 baryon).");
        sb.AppendLine("    3. SHAPE (underived): the specific frequency values");
        sb.AppendLine("       (the hierarchy) are not derived.");
        sb.AppendLine();
        sb.AppendLine("  SO 'ARCHITECTURE' IS PARTIALLY DERIVED:");
        sb.AppendLine("    The STRUCTURE and TYPE are derived (topology, attractor).");
        sb.AppendLine("    The SHAPE (frequencies) is the underived part. This is");
        sb.AppendLine("    exactly the flavor hierarchy (QG-041's free couplings).");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ATTRACTOR SHAPE: STABILITY DETERMINES FORM, NOT FREQUENCIES");
        sb.AppendLine();
        sb.AppendLine("  QG-020: matter persists because it is a STABLE ATTRACTOR.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THE ATTRACTOR PICTURE DETERMINES:");
        sb.AppendLine("    - That stable architectures EXIST (attractor basins).");
        sb.AppendLine("    - That they PERSIST (stability = persistence).");
        sb.AppendLine("    - That they are DISCRETE (separate basins = separate particles).");
        sb.AppendLine();
        sb.AppendLine("  WHAT IT DOES NOT DETERMINE:");
        sb.AppendLine("    - WHICH frequencies (the shape) each attractor has.");
        sb.AppendLine("    - HOW MANY attractors (3 generations, QG-052/053).");
        sb.AppendLine("    - The MASS RATIOS (207, 17 — the shape's scale).");
        sb.AppendLine();
        sb.AppendLine("  THE LANDSCAPE IS UNSPECIFIED:");
        sb.AppendLine("    The attractor picture gives the MECHANISM (stability), but");
        sb.AppendLine("    the LANDSCAPE (the effective potential whose minima are the");
        sb.AppendLine("    architectures) is NOT specified. Without the landscape,");
        sb.AppendLine("    the specific shapes (frequencies) cannot be derived.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Attractor selection gives the FORM (stable patterns),");
        sb.AppendLine("  not the FREQUENCIES (the shape). The shape is the landscape's");
        sb.AppendLine("  content, which is underived.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OVERLAP FORMATION: HOW SHAPES PRODUCE HIERARCHY");
        sb.AppendLine();
        sb.AppendLine("  Y_ij = <arch_i | amplitude_mode | arch_j> (QG-037).");
        sb.AppendLine();
        sb.AppendLine("  WHY THE HIERARCHY IS NATURAL:");
        sb.AppendLine("    - The amplitude mode (Higgs) couples to the architecture's");
        sb.AppendLine("      frequency SCALE. A high-frequency architecture (tau)");
        sb.AppendLine("      overlaps MORE with the amplitude than a low-frequency");
        sb.AppendLine("      one (electron).");
        sb.AppendLine("    - So Y_tau >> Y_mu >> Y_e naturally follows from the");
        sb.AppendLine("      frequency hierarchy (omega_tau >> omega_mu >> omega_e).");
        sb.AppendLine("    - The hierarchy in Y is a PROJECTION of the frequency");
        sb.AppendLine("      hierarchy (QG-027).");
        sb.AppendLine();
        sb.AppendLine("  BUT THE FREQUENCY HIERARCHY ITSELF IS UNDERIVED:");
        sb.AppendLine("    The ratio omega_mu/omega_e = 207 (mass ratio) is empirical.");
        sb.AppendLine("    The overlap picture explains WHY Y is hierarchical (the");
        sb.AppendLine("    architecture is hierarchical), but not WHY the hierarchy");
        sb.AppendLine("    is 1 : 207 : 3478 specifically.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The overlap picture explains the MECHANISM (hierarchy");
        sb.AppendLine("  in Y = hierarchy in architecture), but the hierarchy VALUES");
        sb.AppendLine("  (the shape) are underived. One more layer down, same mystery.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LEPTON ARCHITECTURE: THE CLEANEST CASE");
        sb.AppendLine();
        sb.AppendLine("  Charged leptons = n=1 vortices (QG-034), colorless (QG-050).");
        sb.AppendLine();
        sb.AppendLine("  WHY LEPTONS ARE THE 'CLEANEST' ARCHITECTURE:");
        sb.AppendLine("    - n=1: simplest winding (integer charge).");
        sb.AppendLine("    - Colorless: no confinement dressing.");
        sb.AppendLine("    - Unconfined: the bare architecture is directly observable.");
        sb.AppendLine("    - The 3 generations = 3 excitation bands of the n=1 vortex.");
        sb.AppendLine();
        sb.AppendLine("  SO THE LEPTON ARCHITECTURES ARE THE 'PURE' SHAPES:");
        sb.AppendLine("    The electron, muon, tau are the bare frequency shapes of");
        sb.AppendLine("    the n=1 vortex. Their hierarchy (1 : 207 : 3478) is the");
        sb.AppendLine("    BARE frequency hierarchy, uncontaminated by confinement.");
        sb.AppendLine();
        sb.AppendLine("  THE 45° (KOIDE) AT THE ARCHITECTURE LEVEL (Q10):");
        sb.AppendLine("    If the 45° is a property of the ARCHITECTURE SHAPES (their");
        sb.AppendLine("    mutual overlaps), then it is a DEEPER statement than the");
        sb.AppendLine("    eigenvalue-level relation (QG-056). This is UNTESTED but");
        sb.AppendLine("    plausible: the balance might live in the shapes, not just");
        sb.AppendLine("    their overlaps.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Leptons give the purest architecture shapes, but the");
        sb.AppendLine("  specific frequencies (the shape) remain underived. The 45°");
        sb.AppendLine("  might be an architecture-level property (untested).");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QUARK COMPARISON: WHY DIFFERENT SHAPES?");
        sb.AppendLine();
        sb.AppendLine("  Quarks = fractional charge, SU(3) color, confined (QG-050).");
        sb.AppendLine();
        sb.AppendLine("  THE ARCHITECTURE DIFFERENCE:");
        sb.AppendLine("    - Leptons: n=1 vortex, bare (unconfined).");
        sb.AppendLine("    - Quarks: confined within baryons, dressed by the");
        sb.AppendLine("      chiral condensate. The 'bare' quark architecture is");
        sb.AppendLine("      NOT directly observable (confinement).");
        sb.AppendLine();
        sb.AppendLine("  WHY QUARKS DON'T FOLLOW KOIDE (QG-048):");
        sb.AppendLine("    - The quark architecture SHAPES are DIFFERENT from the");
        sb.AppendLine("      lepton shapes (confined, dressed).");
        sb.AppendLine("    - QG-049: QCD does NOT scramble (common factor preserves");
        sb.AppendLine("      theta). So the quark shapes are INTRINSICALLY different,");
        sb.AppendLine("      not QCD-scrambled lepton shapes.");
        sb.AppendLine("    - The quark architectures are a DIFFERENT family (confined),");
        sb.AppendLine("      with different shapes and no 45°.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The lepton/quark shape difference is ARCHITECTURAL");
        sb.AppendLine("  (bare vs confined), not dynamical (scrambling). But the");
        sb.AppendLine("  specific shapes (frequencies) of both families are underived.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE ARBITRARY-SHAPE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  Assume architecture shapes are ARBITRARY. What survives?");
        sb.AppendLine();
        sb.AppendLine("  WHAT SURVIVES (derived regardless of shape):");
        sb.AppendLine("    - The STRUCTURE (frequency organization, QG-027).");
        sb.AppendLine("    - The TYPE (winding, QG-034).");
        sb.AppendLine("    - The ATTRACTOR NATURE (stability, QG-020).");
        sb.AppendLine("    - The OVERLAP MECHANISM (Y = <arch|amplitude>, QG-037).");
        sb.AppendLine("    - The GEOMETRY (G=C^3, S3, U(3), QG-055).");
        sb.AppendLine("  These are DERIVED and shape-INDEPENDENT.");
        sb.AppendLine();
        sb.AppendLine("  WHAT DOES NOT SURVIVE (shape-dependent):");
        sb.AppendLine("    - The specific masses (the shape = the frequencies).");
        sb.AppendLine("    - The hierarchy (1 : 207 : 3478).");
        sb.AppendLine("    - The Koide 45° (the balance of the shape).");
        sb.AppendLine("  These are UNDERIVED and shape-DEPENDENT.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The flavor STRUCTURE (overlap, G, S3, attractor) is");
        sb.AppendLine("  derived and shape-independent. The flavor VALUES (masses,");
        sb.AppendLine("  hierarchy, 45°) are shape-dependent and underived. The");
        sb.AppendLine("  architecture shapes are the FINAL underived input.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. THE ARCHITECTURE SHAPES ARE THE FINAL UNDERIVED INPUT:");
        sb.AppendLine("    TQM derives the STRUCTURE (frequency, topology, attractor)");
        sb.AppendLine("    and the MECHANISM (overlap). But the SHAPES (specific");
        sb.AppendLine("    frequencies) are underived. They are the flavor's 'initial");
        sb.AppendLine("    conditions' (contingent, QG-042).");
        sb.AppendLine();
        sb.AppendLine("  2. THE REDUCTION HAS BOTTOMED OUT:");
        sb.AppendLine("    Flavor → Y (overlap) → architecture shapes (frequency) →");
        sb.AppendLine("    ???. The chain ends at 'the architecture shapes', which");
        sb.AppendLine("    are the attractor landscape's minima (unspecified).");
        sb.AppendLine("    This is the DEEPEST layer reached.");
        sb.AppendLine();
        sb.AppendLine("  3. THE 45° MIGHT BE AT THE ARCHITECTURE LEVEL (new hypothesis):");
        sb.AppendLine("    If the Koide balance is a property of the architecture");
        sb.AppendLine("    SHAPES (their mutual overlaps), not just the Yukawa");
        sb.AppendLine("    eigenvalues, then it is DEEPER than QG-056 thought. This");
        sb.AppendLine("    is UNTESTED but would relocate the mystery.");
        sb.AppendLine();
        sb.AppendLine("  4. THE ATTRACTOR LANDSCAPE IS THE KEY UNKNOWN:");
        sb.AppendLine("    To derive the shapes, TQM needs the ATTRACTOR LANDSCAPE");
        sb.AppendLine("    (the effective potential whose minima are the architectures).");
        sb.AppendLine("    This landscape is NOT specified. It is the next frontier.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    After 63 experiments, flavor has reduced to ONE input:");
        sb.AppendLine("    the architecture shapes (frequencies). Everything above");
        sb.AppendLine("    them (overlap, Y, masses, mixing, Koide) is either derived");
        sb.AppendLine("    or characterized. The shapes remain the unexplained core.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  ARCHITECTURE SHAPES = ATTRACTOR-SELECTED BUT UNDERIVED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Architecture = a frequency distribution (organization of");
        sb.AppendLine("      oscillation modes, QG-027/028).");
        sb.AppendLine("  Q2: The shape is determined by the ATTRACTOR LANDSCAPE (the");
        sb.AppendLine("      stable minima), which is UNSPECIFIED.");
        sb.AppendLine("  Q3: Three charged-lepton architectures = 3 attractor branches");
        sb.AppendLine("      (QG-039), but the count is SELECTED (QG-053), not derived.");
        sb.AppendLine("  Q4: Shape partially emerges from attractor structure (stability),");
        sb.AppendLine("      but the specific frequencies are not derived.");
        sb.AppendLine("  Q5: Architectures = ATTRACTOR BASINS / phase configurations");
        sb.AppendLine("      (QG-020/034). Eigenmodes/standing waves are descriptions.");
        sb.AppendLine("  Q6: Hierarchy in Y = hierarchy in the frequency architecture");
        sb.AppendLine("      (QG-027/037). But the hierarchy VALUES are underived.");
        sb.AppendLine("  Q7: Architecture symmetry could generate Koide-like constraints,");
        sb.AppendLine("      but no such symmetry is identified (QG-057).");
        sb.AppendLine("  Q8: Lepton/quark difference = BARE vs CONFINED architecture");
        sb.AppendLine("      (QG-050). Shapes differ, but both underived.");
        sb.AppendLine("  Q9: Sectors = DIFFERENT architecture families (no common parent).");
        sb.AppendLine("  Q10: POSSIBLY — the 45° might be at the architecture-shape");
        sb.AppendLine("      level (untested hypothesis).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK SELECTION");
        sb.AppendLine();
        sb.AppendLine("    The architecture shapes are ATTRACTOR-SELECTED (they are");
        sb.AppendLine("    stable patterns, QG-020 — a DERIVED mechanism).");
        sb.AppendLine();
        sb.AppendLine("    But the SPECIFIC shapes (the frequency values) are UNDERIVED:");
        sb.AppendLine("    the attractor LANDSCAPE is unspecified. The shapes are the");
        sb.AppendLine("    landscape's minima, and the landscape is not derived.");
        sb.AppendLine();
        sb.AppendLine("    THE FLAVOR REDUCTION HAS BOTTOMED OUT:");
        sb.AppendLine("    Flavor → Y (overlap) → architecture shapes (frequency) →");
        sb.AppendLine("    [the attractor landscape — unspecified]. The shapes are");
        sb.AppendLine("    the FINAL underived input. Everything above them is");
        sb.AppendLine("    derived or characterized.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 63 experiments.");
        return sb.ToString();
    }
}
