using System.Globalization;

namespace AT.Core.ResearchQG;

public static class CausalitySpeedAnalyzer
{
    const double c = 299792458;
    const double hbar = 1.054571817e-34;
    const double lPlanck = 1.616255e-35;
    const double tPlanck = 5.391247e-44;
    const double G_si = 6.67430e-11;

    public static CSResult RunFullAnalysis()
    {
        var cm = new[]{new Cmeaning("Speed of light","Maximum speed of EM radiation.","Maximum causal update rate of Q-event network.","REDEFINED — c is causal, not optical."),
            new Cmeaning("Causal propagation","Not separately defined.","Speed at which Q-event actualization propagates.","FUNDAMENTAL — defines causal structure."),
            new Cmeaning("Maximum signal speed","Consequence of SR.","Consequence of finite Q-event update rate.","DERIVED — from actualization rate."),
            new Cmeaning("Light travels at c","Photon is massless → travels at max speed.","Photon = Q-event field excitation. Max speed = c.","CONSISTENT — light is emergent."),
        };

        var ci = new[]{new Cinfinite("Causal order","All Q-events connected to all others instantaneously. No partial order.","FATAL — causal order destroyed.","c < ∞ IS LOGICALLY REQUIRED."),
            new Cinfinite("Q-event individuation","Events cannot be temporally distinguished. All simultaneous.","FATAL — Q primitive violated.","c < ∞ required for temporal distinction."),
            new Cinfinite("Actualization","All Q-events actualize at once. No becoming.","FATAL — actualization meaningless.","c < ∞ required for sequential actualization."),
            new Cinfinite("Geometry","No causal structure → no causal set → no manifold.","FATAL — spacetime destroyed.","c < ∞ required for geometry."),
            new Cinfinite("Entanglement","All Q-events maximally entangled. Bell bound trivial.","MODERATE — entanglement still exists.","Entanglement survives c→∞."),
            new Cinfinite("Entropy","No entropy growth. Second law meaningless.","FATAL — thermodynamics destroyed.","c < ∞ required for entropy."),
        };

        var ar = new[]{new ActRate("Sequential actualization","Q-events actualize one at a time. Requires temporal order.","Finite rate = 1/τ per Q-event.","RATE EXISTS — from Q-event succession."),
            new ActRate("Causal propagation","Actualization propagates through causal links.","Speed = Δx/Δt = ℓ/τ = c.","PROPAGATION SPEED — from ℓ and τ."),
            new ActRate("Maximum rate","Causal links saturate. Maximum update speed reached.","No Q-event can actualize faster than c.","MAXIMUM SPEED — from causal saturation."),
        };

        var mt = new[]{new MinTime("Q-event spacing in time","Q-events are discrete → minimum temporal separation.","τ = ℓ/c = t_Planck.","CANDIDATE — τ from ℓ and c."),
            new MinTime("Actualization granularity","Actualization is discrete → finite rate.","τ = 1/(actualization rate).","PLAUSIBLE — depends on rate."),
            new MinTime("Causal set element interval","Causal set elements have minimum time separation.","τ consistent with causal set density.","MATHEMATICAL — from causal set."),
        };

        var le = new[]{new LengthEmerge("1. c is fundamental","c = maximum causal speed. From causal structure.","Q-event causal order.","DERIVED — from causality."),
            new LengthEmerge("2. τ emerges","τ = minimum actualization interval. From actualization discreteness.","Q-event succession.","DERIVED — from actualization."),
            new LengthEmerge("3. ℓ emerges","ℓ = c·τ. Length from speed × time.","c and τ.","DERIVED — ℓ is secondary."),
            new LengthEmerge("4. G emerges","G = ℓ²·c³/ħ. Gravity constant from ℓ, c, ħ.","ℓ, c, ħ.","DERIVED — G is tertiary."),
        };

        var pc = new[]{new PlanckChain("0. Q + Randomness","Individuation + actualization.","Logical primitives.","BEDROCK — irreducible (QG-006)."),
            new PlanckChain("1. c","Maximum causal speed.","Causal order + finite propagation.","DERIVED — from causal structure."),
            new PlanckChain("2. tau","Minimum actualization interval.","Discreteness of actualization.","PARTIALLY — from actualization."),
            new PlanckChain("3. l = c*tau","Minimum length.","c x tau.","DERIVED — composite of c and tau."),
            new PlanckChain("4. G","Gravity constant.","l, c, hbar.","DERIVED — composite of l, c, hbar."),
            new PlanckChain("5. Planck scale","l_P, t_P, m_P, T_P.","l, c, hbar, G.","DERIVED — all from l, c, hbar."),
        };

        string A=BuildA(cm),B=BuildB(ci),C=BuildC(ar),D=BuildD(mt),E=BuildE(le),F=BuildF(pc),G=BuildG(),H=BuildH(),I=BuildI();
        return new CSResult(A,B,C,D,E,F,G,H,I,cm,ci,ar,mt,le,pc);
    }

    static string BuildA(Cmeaning[] c){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("WHAT IS c IN AT?");sb.AppendLine();
        sb.AppendLine("  Aspect               Standard View              AT View");
        sb.AppendLine("  -------------------  -------------------------  -------------------------");
        foreach(var x in c) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-20} {1,-26} {2}",x.Aspect,x.StandardView,x.AtView));
        sb.AppendLine();sb.AppendLine("  c = maximum causal update rate of the Q-event network.");
        sb.AppendLine("  Light travels at c because photons are massless Q-event field excitations.");
        sb.AppendLine("  c is NOT about light — light is about c.");
        return sb.ToString();
    }

    static string BuildB(Cinfinite[] c){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("c → ∞ LIMIT ANALYSIS");sb.AppendLine();
        sb.AppendLine("  Aspect                 c = ∞ consequence                    Severity");
        sb.AppendLine("  ---------------------  -----------------------------------  --------");
        foreach(var x in c) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-22} {1,-36} {2}",x.Aspect,x.Consequence,x.Severity));
        sb.AppendLine();sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  FATAL: {0}/{1}. c < ∞ IS LOGICALLY REQUIRED by causal structure.",c.Count(x=>x.Severity=="FATAL"),c.Length));
        sb.AppendLine("  c is NOT arbitrary — it's FORCED by Q-event causal order.");
        return sb.ToString();
    }

    static string BuildC(ActRate[] a){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("ACTUALIZATION RATE");sb.AppendLine();
        sb.AppendLine("  Mechanism                Constraint                    Gives");
        sb.AppendLine("  -----------------------  ----------------------------  --------");
        foreach(var x in a) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-24} {1,-29} {2}",x.Mechanism,x.Constraint,x.Gives));
        return sb.ToString();
    }

    static string BuildD(MinTime[] m){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("MINIMUM TIME");sb.AppendLine();
        sb.AppendLine("  Candidate                      Mechanism                      Status");
        sb.AppendLine("  -----------------------------  -----------------------------  ------");
        foreach(var x in m) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-30} {1,-30} {2}",x.Candidate,x.Mechanism,x.Status));
        return sb.ToString();
    }

    static string BuildE(LengthEmerge[] l){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("LENGTH EMERGENCE CHAIN");sb.AppendLine();
        sb.AppendLine("  Step  Relation          From                      Status");
        sb.AppendLine("  ----  ----------------  ------------------------  ------");
        foreach(var x in l) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-17} {2,-25} {3}",x.Step[0],x.Relation,x.From,x.Status));
        sb.AppendLine();sb.AppendLine("  KEY INSIGHT: c is MORE fundamental than l.");
        sb.AppendLine("  l = c*tau — length is DERIVED from speed + time.");
        sb.AppendLine("  This INVERTS the QG-008 conclusion (l as final parameter).");
        return sb.ToString();
    }

    static string BuildF(PlanckChain[] p){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("PLANCK SCALE RECONSTRUCTION");sb.AppendLine();
        sb.AppendLine("  Level  Quantity      Expression          From              Status");
        sb.AppendLine("  -----  ------------  ------------------  ----------------  ------");
        foreach(var x in p) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]    {1,-12} {2,-19} {3,-17}",x.Level[0],x.Quantity,x.Expression,x.From));
        sb.AppendLine();sb.AppendLine("  THE COMPLETE CHAIN:");
        sb.AppendLine("    Q + Randomness → c → tau → l = c*tau → G = l^2*c^3/hbar → Planck scale.");
        sb.AppendLine("    All of fundamental physics from 2 primitives + c + tau + hbar.");
        return sb.ToString();
    }

    static string BuildG()=>"HOSTILE REVIEW\n\n1. c IS STILL ASSUMED, NOT DERIVED:\n   c < ∞ is logically forced (causal order). But c = 299792458 m/s\n   is NOT forced. Any finite c would satisfy the logic.\n   The NUMERICAL VALUE remains empirical.\n\n2. τ IS EVEN LESS CONSTRAINED:\n   l = c*tau. If l is inferred from G, and G is inferred from l,\n   the chain is CIRCULAR. We need an independent determination\n   of (c, tau) or (l, c).\n\n3. THE FUNDAMENTAL PAIR:\n   (c, tau) and (l, hbar) are DUAL descriptions.\n   (c, tau) → l = c*tau.\n   (l, hbar) → c from QM + QG consistency.\n   Neither pair is uniquely determined.\n\n4. THE REDUCTION IS REAL BUT INCOMPLETE:\n   Standard: G, c, hbar (3 fundamental constants).\n   AT: c, tau, hbar (3 fundamental constants).\n   Wait — still 3! l is eliminated but tau replaces it.\n\n5. THE NUMBER OF FREE PARAMETERS IS UNCHANGED:\n   Standard: (G, c, hbar) = 3.\n   AT: (c, tau, hbar) = 3.\n   l = c*tau is a DEFINITION, not a reduction.\n\n6. WHAT AT ACTUALLY ACHIEVES:\n   It clarifies WHAT the constants MEAN:\n   - c = causal speed (not 'speed of light').\n   - tau = actualization interval (new concept).\n   - l = derived length (not fundamental).\n   This is ONTOLOGICAL progress, not parametric progress.";

    static string BuildH()=>"REMAINING GAPS\n\n  GAP 1: c's numerical value — NOT derived.\n    c < ∞ is logically forced. c = 299792458 m/s is NOT.\n\n  GAP 2: tau — NOT derived.\n    tau = l/c = 5.39e-44 s. But l is from G.\n    Circular: tau = sqrt(hbar*G/c^5). G is assumed.\n\n  GAP 3: hbar — NOT derived.\n    QM-002 gives Hilbert space but not numerical hbar.\n\n  GAP 4: The triple (c, tau, hbar) replaces (G, c, hbar).\n    Number of free parameters is UNCHANGED (3 → 3).\n    l = c*tau is a definition, not an elimination.\n\n  GAP 5: REAL reduction requires deriving at least ONE of\n    (c, tau, hbar) from the other two + Q-event structure.\n    This has NOT been achieved.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q2: c = maximum causal update rate of Q-event network.");
        sb.AppendLine("         NOT the 'speed of light' — light travels at c BECAUSE");
        sb.AppendLine("         it's a massless Q-event field excitation.");
        sb.AppendLine("  Q3-Q5: c → ∞ destroys causal order, individuation, actualization.");
        sb.AppendLine("         c < ∞ IS LOGICALLY REQUIRED. Value remains empirical.");
        sb.AppendLine("  Q6-Q8: τ = ℓ/c = t_Planck. ℓ = cτ. ℓ is DERIVED from c + τ.");
        sb.AppendLine("         c is MORE fundamental than ℓ.");
        sb.AppendLine("  Q9-Q10: Planck scale = c + τ + ħ → ℓ → G → all scales.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  c IS LOGICALLY FORCED (c < ∞). THE VALUE IS EMPIRICAL.");
        sb.AppendLine();
        sb.AppendLine("  THE FUNDAMENTAL CHAIN:");
        sb.AppendLine("    Q + Randomness → causal order → finite propagation speed (c)");
        sb.AppendLine("    → discrete actualization → minimum time (τ)");
        sb.AppendLine("    → ℓ = cτ (derived length)");
        sb.AppendLine("    → G = ℓ²c³/ħ (derived gravity constant)");
        sb.AppendLine("    → Planck scale (all quantum gravity scales)");
        sb.AppendLine();
        sb.AppendLine("  PARAMETER COUNT:");
        sb.AppendLine("    Standard physics: G, c, ħ (3 fundamental constants).");
        sb.AppendLine("    AT: c, τ, ħ (3 fundamental constants). UNCHANGED.");
        sb.AppendLine("    l = cτ eliminates l but introduces τ. Net: 0 reduction.");
        sb.AppendLine();
        sb.AppendLine("  PROGRESS:");
        sb.AppendLine("    - c is REINTERPRETED as causal update rate.");
        sb.AppendLine("    - τ is a NEW physical concept (actualization interval).");
        sb.AppendLine("    - ℓ is DEMOTED to derived (ℓ = cτ).");
        sb.AppendLine("    - The chain is LOGICALLY COHERENT from Q to Planck scale.");
        sb.AppendLine("    - But NUMBER of free parameters is unchanged (3 → 3).");
        sb.AppendLine();
        sb.AppendLine("  THE REAL GOAL:");
        sb.AppendLine("    Derive ONE of (c, τ, ħ) from the others + Q-event structure.");
        sb.AppendLine("    Then AT would have FEWER parameters than standard physics.");
        sb.AppendLine("    This is the continuing challenge of the QG program.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B/C — CONSTRAINED to STRONGLY EMERGENT");
        sb.AppendLine("  c < ∞ is LOGICALLY REQUIRED. Value is empirical.");
        sb.AppendLine("  QG program (QG-001→010, 10 experiments) continues.");
        return sb.ToString();
    }
}
