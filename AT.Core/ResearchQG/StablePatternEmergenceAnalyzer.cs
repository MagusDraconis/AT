using System.Globalization;

namespace AT.Core.ResearchQG;

public static class StablePatternEmergenceAnalyzer
{
    public static SPResult RunFullAnalysis()
    {
        var ns = new[]{new NoiseVsStruct("Randomness ONLY (no Q, no M^2)","WHITE NOISE — no patterns, no persistence.","Randomness alone = uncorrelated events. No structure.","MATTER CANNOT EXIST."),
            new NoiseVsStruct("Q + Randomness (no M^2)","CAUSAL STRUCTURE but linear dynamics.","Q provides individuation + causal order. But linear → no attractors.","PARTICLES CANNOT FORM — need nonlinearity."),
            new NoiseVsStruct("Q + Randomness + M^2","STABLE PATTERNS EMERGE.","M^2 (nonlinearity) creates attractors. Q provides structure.","THIS IS AT — the minimal set for matter."),
        };

        var am = new[]{new AttractorMech("Q individuation","Distinct Q-events with causal relations.","Q primitive.","FOUNDATION — without Q, no structure."),
            new AttractorMech("M^2 (nonlinearity)","Solitons, defects, stable coherent structures.","Q-event field dynamics.","ATTRACTOR ENGINE — nonlinearity creates stable configurations."),
            new AttractorMech("Topological protection","Defects with non-zero winding number are topologically stable.","M^2 + Q-event field topology.","PARTICLE STABILITY — winding number conserved (AT-113-122)."),
            new AttractorMech("Energy minimization","Bound states are energy minima. Once formed, need energy to break.","Emergent Hamiltonian (QM-002).","ATOMIC STABILITY — Coulomb potential creates minima."),
            new AttractorMech("Entanglement locking","Correlated Q-event states resist disruption.","Q-event entanglement (QM-003).","COHERENCE — entangled structures maintain correlations."),
        };

        var pw = new[]{new PersistWhy("Electron","Lepton number conserved. Topological charge protected.","> 10^28 years (stable).","Topological winding number. U(1) gauge protection.","STABLE — no decay channel observed."),
            new PersistWhy("Proton","Baryon number conserved (or extremely long-lived).","> 10^34 years.","Topological defect with conserved baryon number.","STABLE — or effectively stable."),
            new PersistWhy("Hydrogen atom","Bound state is energy minimum. 13.6 eV to ionize.","Infinite (in vacuum).","Coulomb binding + energy minimization.","STABLE — ground state is eternal."),
            new PersistWhy("Neutron (free)","Weak decay: n -> p + e + anti-nu.","~880 s (15 min).","Not topologically protected alone — needs nucleus.","UNSTABLE — decays outside nucleus."),
            new PersistWhy("Uranium-238","Alpha decay through quantum tunneling.","4.5 x 10^9 years.","Energy barrier + tunneling probability.","METASTABLE — decays slowly."),
        };

        var so = new[]{new SelfOrg("Q-event field","Coherent field modes","Linear superposition attractor (QM-002).","QUANTUM COHERENCE — from linearity."),
            new SelfOrg("Topological","Stable defects","Topological attractor (winding number fixed).","PARTICLES — from M^2 + topology."),
            new SelfOrg("Atomic","Bound states (e + nucleus)","Energy minimum attractor (Coulomb potential).","ATOMS — from gauge interactions."),
            new SelfOrg("Molecular","Chemical bonds","Electron sharing attractor (exchange interaction).","MOLECULES — from quantum chemistry."),
            new SelfOrg("Condensed","Crystal lattices, liquids","Free energy minimum attractor (thermodynamics).","MATTER — from many-body physics."),
            new SelfOrg("Living","Self-replicating patterns","Far-from-equilibrium attractor (metabolism).","LIFE — from information + energy flow."),
        };

        string A=BuildA(ns),B=BuildB(am),C=BuildC(pw),D=BuildD(so),E=BuildE(),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI();
        return new SPResult(A,B,C,D,E,F,G,H,I,ns,am,pw,so);
    }

    static string BuildA(NoiseVsStruct[] n){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("WHY NOT JUST NOISE?");sb.AppendLine();
        sb.AppendLine("  Scenario                                    Outcome");
        sb.AppendLine("  ------------------------------------------  ----------------------------------------");
        foreach(var x in n) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-43} {1}",x.Scenario,x.Outcome));
        sb.AppendLine();sb.AppendLine("  CONCLUSION: Q + Randomness + M^2 is the MINIMAL SET for matter.");
        return sb.ToString();
    }

    static string BuildB(AttractorMech[] a){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("ATTRACTOR MECHANISMS");sb.AppendLine();
        sb.AppendLine("  Mechanism                  Creates                          Status");
        sb.AppendLine("  -------------------------  -------------------------------  ------");
        foreach(var x in a) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-26} {1,-32} {2}",x.Mechanism,x.Creates,x.Status));
        return sb.ToString();
    }

    static string BuildC(PersistWhy[] p){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("PARTICLE & ATOM PERSISTENCE");sb.AppendLine();
        sb.AppendLine("  Entity          Why Stable                        Lifetime         Status");
        sb.AppendLine("  --------------  --------------------------------  ---------------  ------");
        foreach(var x in p) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-15} {1,-33} {2,-16} {3}",x.Entity,x.WhyStable,x.Lifetime,x.Status));
        return sb.ToString();
    }

    static string BuildD(SelfOrg[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SELF-ORGANIZATION HIERARCHY");sb.AppendLine();
        sb.AppendLine("  Level          Structure                   Attractor Type");
        sb.AppendLine("  --------------  --------------------------  ------------------------------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-15} {1,-27} {2}",x.Level,x.Structure,x.AttractorType));
        return sb.ToString();
    }

    static string BuildE()=>"EMERGENCE HIERARCHY\n\n  Q + Randomness + M^2 (3 primitives)\n      |\n  Level 1: Q-event field modes (linear superposition)\n      |  M^2\n  Level 2: Topological defects = PARTICLES (stable attractors)\n      |  Gauge interactions (U(1), SU(2), SU(3))\n  Level 3: Bound states = ATOMS (energy minima)\n      |  Exchange interactions\n  Level 4: Chemical bonds = MOLECULES (electron sharing)\n      |  Van der Waals, hydrogen bonds\n  Level 5: Condensed matter = BULK MATERIAL (thermodynamic minima)\n      |  Information + energy flow\n  Level 6: Self-replicating patterns = LIFE (far-from-equilibrium)\n\n  EACH LEVEL IS AN ATTRACTOR OF THE LEVEL BELOW.\n  Stability cascades upward from Q-event grains to galaxies.";

    static string BuildF()=>"COMPARISON TO COMPLEXITY THEORY\n\n  AT vs known self-organization frameworks:\n\n  CELLULAR AUTOMATA:\n    Simple rules -> complex patterns. AT:\n    Q-events are like CA cells. Actualization = update rule.\n    M^2 provides the nonlinear update rule.\n    SIMILAR — but AT has physical content (Q, hbar, G).\n\n  DISSIPATIVE STRUCTURES (Prigogine):\n    Energy flow -> order. AT:\n    Actualization IS the energy flow.\n    Energy = hbar * omega (from Q-event oscillation).\n    AT grounds thermodynamics in Q-event dynamics.\n\n  NETWORK DYNAMICS:\n    Graph topology -> emergent properties. AT:\n    Q-event causal set IS the network.\n    Causal connectivity defines geometry (QG-001).\n    ATTRACTORS = stable graph configurations.\n\n  KEY DIFFERENCE:\n    AT has physical constants (l, tau, hbar).\n    Complexity theory has abstract rules.\n    AT bridges abstract structure -> physical reality.";

    static string BuildG()=>"HOSTILE REVIEW\n\n1. 'ATTRACTORS' IS A METAPHOR, NOT A MECHANISM:\n   Saying 'M^2 creates attractors' explains nothing unless\n   you specify the ATTRACTOR DYNAMICS. What is the Lyapunov\n   function? What are the basins of attraction?\n\n2. TOPOLOGICAL STABILITY IS INHERITED:\n   Winding numbers, conserved charges — these come from\n   standard QFT. AT claims them but doesn't derive them.\n\n3. THE HIERARCHY IS STANDARD PHYSICS:\n   Particles -> atoms -> molecules -> matter is the\n   standard reductionist picture. AT just adds 'from Q-events'\n   at the bottom — which is ontological, not computational.\n\n4. M^2 IS UNKNOWN:\n   The 'nonlinearity that creates attractors' has no specified\n   value. Without M^2, the attractor claim is unfalsifiable.\n\n5. LIFE EMERGENCE IS UNSUPPORTED:\n   Self-replicating patterns from Q-events is a CLAIM,\n   not a result. No AT experiment demonstrates this.";

    static string BuildH()=>"REMAINING GAPS\n\n  1. M^2 — the attractor engine. Unknown value. Unknown dynamics.\n  2. Gauge symmetries — U(1), SU(2), SU(3) not derived from Q-events.\n  3. Particle masses — not predicted. Standard model inputs.\n  4. Binding energies — from QED/QCD. Not from AT.\n  5. Life emergence — speculative. No AT mechanism.\n\n  THE BOTTOM LINE:\n    AT provides a COHERENT ONTOLOGICAL FRAMEWORK for why\n    matter exists. It identifies the MECHANISMS (Q, M^2, topology).\n    But it does not COMPUTE anything — all quantitative\n    predictions come from standard physics.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q3: Stability = Q (structure) + M^2 (nonlinear attractors).");
        sb.AppendLine("         Without M^2: linear dynamics, no stable patterns.");
        sb.AppendLine("         Without Q: no individuation, pure noise.");
        sb.AppendLine("  Q4-Q6: Particles = topologically protected defects.");
        sb.AppendLine("         Atoms = energy minima (Coulomb binding).");
        sb.AppendLine("         Coherence = entanglement locking (QM-003).");
        sb.AppendLine("  Q7-Q10: Matter = stable Q-event pattern at large N.");
        sb.AppendLine("         Hierarchy: modes -> particles -> atoms -> molecules -> matter.");
        sb.AppendLine("         Each level is an ATTRACTOR of the level below.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  REALITY FORMS STABLE PATTERNS BECAUSE OF THREE INGREDIENTS:");
        sb.AppendLine();
        sb.AppendLine("    1. Q (individuation) — events are DISTINCT.");
        sb.AppendLine("       Creates causal structure. Prevents chaos.");
        sb.AppendLine();
        sb.AppendLine("    2. M^2 (nonlinearity) — interactions are NONLINEAR.");
        sb.AppendLine("       Creates attractors. Prevents dissolution.");
        sb.AppendLine();
        sb.AppendLine("    3. Topology (winding numbers) — defects are PROTECTED.");
        sb.AppendLine("       Creates particle stability. Prevents decay.");
        sb.AppendLine();
        sb.AppendLine("  WITH THESE THREE, MATTER IS INEVITABLE.");
        sb.AppendLine("  WITHOUT ANY ONE, MATTER CANNOT EXIST.");
        sb.AppendLine();
        sb.AppendLine("  AT identifies WHY matter exists but does not COMPUTE");
        sb.AppendLine("  specific particle properties — those come from standard physics.");
        sb.AppendLine("  AT provides the ONTOLOGICAL FOUNDATION, not the computational");
        sb.AppendLine("  replacement, for the standard model.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B/C — PARTIAL to STRONG EMERGENCE");
        sb.AppendLine("  QG program (QG-001->020, 20 experiments) COMPLETE.");
        return sb.ToString();
    }
}
