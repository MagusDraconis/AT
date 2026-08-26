using System.Globalization;

namespace AT.Core.ResearchQG;

public static class AttractiveRepulsiveGravityAnalyzer
{
    public static AR29Result RunFullAnalysis()
    {
        var ps = new[]{new PhaseSign("Positive (+dtheta/dx)","Oscillation density INCREASES outward.","POSITIVE curvature -> attraction.","STABLE — matter falls into density wells.","STANDARD GRAVITY — all normal matter."),
            new PhaseSign("Negative (-dtheta/dx)","Oscillation density DECREASES outward.","NEGATIVE curvature -> repulsion.","UNSTABLE — phase void fills from surroundings.","MATHEMATICALLY ALLOWED — physically unstable."),
            new PhaseSign("Zero (flat)","Constant oscillation density.","FLAT spacetime. No gravity.","STABLE — vacuum state.","MINKOWSKI — empty space."),
        };

        var ra = new[]{new RepulArch("Coherent attractive","Phase-aligned, positive gradient.","STANDARD ATTRACTION.","STABLE — normal matter attractor.","ALL OBSERVED GRAVITY."),
            new RepulArch("Phase void (dip)","Local oscillation density minimum.","REPULSIVE — pushes away.","UNSTABLE — fills from surroundings.","POSSIBLE but transient — not persistent."),
            new RepulArch("Anti-phase domain","Two regions: opposite phase. Boundary repulsion.","DOMAIN WALL — repulsive boundary.","METASTABLE — requires energy to maintain.","TOPOLOGICAL — could exist briefly."),
            new RepulArch("Dark Energy (Lambda)","UNIFORM negative pressure (p = -rho).","COSMIC REPULSION — acceleration.","STABLE at COSMOLOGICAL scale.","QG-004: Lambda(t) = alpha/sqrt(V(t)). OBSERVED."),
            new RepulArch("Exotic anti-phase attractor","Sustained negative phase gradient.","LOCALIZED repulsion.","UNKNOWN — no known mechanism in AT.","NO EVIDENCE — would violate energy conditions."),
        };

        string A=BuildA(),B=BuildB(ps),C=BuildC(),D=BuildD(ra),E=BuildE(),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI();
        return new AR29Result(A,B,C,D,E,F,G,H,I,ps,ra);
    }

    static string BuildA()=>"WHY GRAVITY ATTRACTS\n\n  QG-022: Gravity = phase gradient -> causal density -> curvature.\n\n  NORMAL MATTER:\n    Mass = localized oscillation density (higher than vacuum).\n    dtheta/dx > 0 (rising toward mass concentration).\n    Causal density increases toward mass.\n    Metric curves: geodesics converge -> ATTRACTION.\n\n  THIS IS THE STANDARD PICTURE:\n    Positive phase gradient -> positive curvature -> attraction.\n    All normal matter produces positive phase gradients.\n\n  THE QUESTION:\n    Can ANY architecture produce a NEGATIVE phase gradient\n    that is STABLE?";

    static string BuildB(PhaseSign[] p){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("PHASE-SIGN ANALYSIS");sb.AppendLine();
        sb.AppendLine("  Gradient        Curvature         Stability        Status");
        sb.AppendLine("  --------------  ----------------  ---------------  ------");
        foreach(var x in p) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-15} {1,-17} {2,-16} {3}",x.Sign,x.Curvature,x.Stability,x.Status));
        return sb.ToString();
    }

    static string BuildC()=>"CURVATURE RESPONSE\n\n  PHASE -> CURVATURE MAPPING (QG-022):\n    dtheta/dx > 0 -> oscillation density gradient positive -> causal density increases -> metric curves -> geodesics converge (ATTRACTION).\n    dtheta/dx < 0 -> causal density decreases -> metric curves outward -> geodesics diverge (REPULSION).\n\n  BUT: negative dtheta/dx means oscillation density BELOW vacuum.\n  Can oscillation density go below vacuum?\n  The vacuum has MINIMAL oscillation (from N_inf, QG-005).\n  Below vacuum = no Q-events = empty causal set = undefined metric.\n\n  CONSTRAINT: oscillation density cannot go below vacuum level.\n  Therefore: negative curvature from LOCAL structures is LIMITED.\n  Exceptions: COSMOLOGICAL (dark energy) and TOPOLOGICAL (domain walls).\n\n  ENERGY CONDITIONS (from GR):\n    Weak: rho >= 0. Dominant: |p| <= rho.\n    Violation -> exotic matter.\n    AT inherits these from GR at the effective level.";

    static string BuildD(RepulArch[] r){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("REPULSIVE ARCHITECTURES");sb.AppendLine();
        sb.AppendLine("  Architecture                Gravity              Stability         Status");
        sb.AppendLine("  --------------------------  -------------------  ----------------  ------");
        foreach(var x in r) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-27} {1,-20} {2,-17} {3}",x.Architecture,x.Gravity,x.Stability,x.Status));
        return sb.ToString();
    }

    static string BuildE()=>"DARK ENERGY INTERPRETATION\n\n  QG-004: Lambda(t) = alpha/sqrt(V(t)).\n    Lambda creates uniform negative pressure -> cosmic acceleration.\n\n  AT INTERPRETATION:\n    NOT a localized phase void.\n    It's the GLOBAL oscillation density changing with N(t).\n    As causal volume V grows, residual growth rate decreases.\n    Result: Lambda DECREASES with time.\n    But expansion ACCELERATES (relative to Lambda).\n\n  w(z) = -1 + 0.015*(1+z)^(3/2).\n    w ≈ -1 (near cosmological constant).\n    Small positive deviation from -1 (AT-specific, DATA-001-010).\n\n  DARK ENERGY IS REPULSIVE GRAVITY AT COSMOLOGICAL SCALE.\n  It IS stable because it's GLOBAL, not local.\n  No localized repulsive attractor needed.";

    static string BuildF()=>"REPULSIVE GRAVITY CANDIDATES — SUMMARY\n\n  1. LOCAL PHASE VOID:\n     Theoretically possible. Practically unstable.\n     Fills instantly from surroundings. No persistence.\n     STATUS: Transient only. Not observable.\n\n  2. DOMAIN WALL (ANTI-PHASE):\n     Topologically possible. Requires energy to maintain.\n     Could exist in early universe (phase transitions).\n     STATUS: Metastable. Not observed.\n\n  3. DARK ENERGY (Lambda):\n     Cosmological repulsion. Stable at large scales.\n     OBSERVED (SNe Ia, CMB, BAO).\n     STATUS: REAL — but cosmological, not local.\n\n  4. EXOTIC ATTRACTOR:\n     Sustained negative phase gradient.\n     No mechanism in AT. Would violate energy conditions.\n     STATUS: Not predicted. No evidence.\n\n  CONCLUSION:\n    Stable LOCAL repulsive gravity is NOT predicted by AT.\n    Cosmological repulsion (dark energy) IS predicted.";

    static string BuildG()=>"NO-GO ARGUMENTS\n\n  AGAINST LOCAL REPULSIVE GRAVITY:\n\n  1. STABILITY: Phase voids fill from denser surroundings.\n     No mechanism sustains them.\n\n  2. ENERGY CONDITIONS: Negative energy density violates\n     weak energy condition. No known matter does this.\n\n  3. TOPOLOGICAL CONSTRAINTS: Normal matter defects\n     (particles) have positive winding -> positive energy.\n     Anti-defects (if they exist) would annihilate.\n\n  4. CAUSAL STRUCTURE: Repulsive regions would push\n     Q-events away, creating causal disconnection.\n     This might violate global hyperbolicity.\n\n  5. OBSERVATION: No repulsive gravity detected.\n     Only dark energy (cosmological) and possibly\n     inflation (early universe).\n\n  VERDICT: Local repulsive gravity is HIGHLY CONSTRAINED.\n  No-go arguments are strong but not absolute proofs.";

    static string BuildH()=>"HOSTILE REVIEW\n\n1. THIS AUDIT FINDS NOTHING NEW:\n   'Normal matter attracts. Exotic matter might repel.'\n   This is standard GR, not AT-specific.\n\n2. DARK ENERGY AS REPULSIVE IS STANDARD:\n   Lambda causes cosmic acceleration in standard cosmology.\n   AT's Lambda(t) adds time variation but doesn't change the sign.\n\n3. AT DOES NOT PREDICT NEW REPULSIVE PHENOMENA:\n   It provides ONTOLOGICAL GROUNDING (phase gradient -> curvature)\n   but no new physical effects beyond GR.\n\n4. THE DEEPEST INSIGHT:\n   AT explains WHY gravity attracts:\n     Mass = localized oscillation density increase.\n     Higher density -> positive phase gradient -> attraction.\n   This is an EXPLANATION, not a new prediction.\n\n5. REPULSIVE GRAVITY IS THEORETICALLY POSSIBLE\n   BUT PRACTICALLY ABSENT. This is consistent with observation.\n   AT is conservative on this point.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q3: Positive phase gradient -> attraction. Negative possible but unstable.");
        sb.AppendLine("         Opposite signs -> opposite curvature (mathematically allowed).");
        sb.AppendLine("  Q4-Q7: Phase voids are UNSTABLE. Domain walls are METASTABLE.");
        sb.AppendLine("         Anti-phase architectures mathematically possible, physically transient.");
        sb.AppendLine("  Q8:    Dark Energy = cosmological repulsion (QG-004). Stable, global.");
        sb.AppendLine("         w(z) = -1 + 0.015*(1+z)^(3/2) — unique AT prediction.");
        sb.AppendLine("  Q9-Q10: AT does NOT forbid repulsive gravity mathematically.");
        sb.AppendLine("         But stable LOCAL repulsion is NOT predicted.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  ATTRACTIVE GRAVITY IS THE DOMINANT STABLE SOLUTION.");
        sb.AppendLine("  REPULSIVE GRAVITY IS MATHEMATICALLY POSSIBLE BUT UNSTABLE.");
        sb.AppendLine();
        sb.AppendLine("  WHY ATTRACTION DOMINATES:");
        sb.AppendLine("    Normal matter = localized oscillation density INCREASE.");
        sb.AppendLine("    Density increase -> positive phase gradient -> attraction.");
        sb.AppendLine("    This is the ONLY stable architecture for normal matter.");
        sb.AppendLine();
        sb.AppendLine("  REPULSIVE POSSIBILITIES:");
        sb.AppendLine("    LOCAL: Phase voids -> unstable (fill from surroundings).");
        sb.AppendLine("    TOPOLOGICAL: Domain walls -> metastable (not observed).");
        sb.AppendLine("    COSMOLOGICAL: Dark Energy -> STABLE (observed).");
        sb.AppendLine();
        sb.AppendLine("  AT PREDICTS:");
        sb.AppendLine("    [1] Normal matter -> ALWAYS attractive.");
        sb.AppendLine("    [2] Dark Energy -> cosmological repulsion.");
        sb.AppendLine("    [3] w(z) = -1 + 0.015*(1+z)^(3/2) (AT-specific).");
        sb.AppendLine("    [4] NO stable local repulsive gravity.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — REPULSION MATHEMATICALLY POSSIBLE");
        sb.AppendLine("                       BUT UNSTABLE LOCALLY.");
        sb.AppendLine("  QG program (QG-001->029, 29 experiments).");
        return sb.ToString();
    }
}
