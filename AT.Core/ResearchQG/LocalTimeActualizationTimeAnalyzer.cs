using System.Globalization;

namespace AT.Core.ResearchQG;

public static class LocalTimeActualizationTimeAnalyzer
{
    public static T18Result RunFullAnalysis()
    {
        var tl = new[]{new TimeLayer("Q-event actualization",0,"tau = 5.39e-44 s — UNIVERSAL actualization interval.","Q succession primitive.","FUNDAMENTAL — pre-geometric, pre-metric, invariant."),
            new TimeLayer("Causal order",1,"tau defines before/after. Causal succession.","Q-event order.","EMERGENT — from actualization ordering."),
            new TimeLayer("Causal set",2,"Partial order on discrete events.","Causal structure.","EMERGENT — mathematics of partial orders."),
            new TimeLayer("Coordinate time",3,"t = N*tau — clock time from Q-event count.","Q-event counting.","EMERGENT — from counting actualizations."),
            new TimeLayer("Metric/proper time",4,"ds^2 = g_uv dx^u dx^v. Proper time from metric.","Metric (continuum limit).","EMERGENT — from causal set -> manifold."),
            new TimeLayer("GR time dilation",5,"dtau_proper = sqrt(1-2GM/rc^2)*dt.","Curvature (Einstein equations).","EMERGENT — from GR (L6)."),
        };

        var tc = new[]{new TimeComp("Deep space (flat)","tau universal (5.39e-44 s).","dtau = N*tau. Clock time = Q-event count * tau.","c = l/tau = 299792458 m/s.","IDENTICAL — in flat space, proper time = N*tau."),
            new TimeComp("Near Earth","tau universal (unchanged).","dtau_Earth = N*tau * sqrt(1-2GM/Rc^2). Clock SLOWER.","c = l/tau = 299792458 m/s.","DIVERGE — proper time dilates. tau does NOT change."),
            new TimeComp("Near BH horizon","tau universal (unchanged).","dtau_BH -> 0 as r -> 2GM/c^2. Clock STOPS.","c = l/tau = 299792458 m/s.","EXTREME — proper time freezes. tau continues universally."),
        };

        var lc = new[]{new LocalC("Flat space","l universal.","tau universal.","c = l/tau = constant.","INVARIANT — ℓ and τ are universal."),
            new LocalC("In gravity well","l universal.","tau universal. But proper time slower.","Local measurement of c always l/tau.","INVARIANT — local physics uses local proper time. l/tau constant."),
            new LocalC("Resolution","Local c = l/tau_local where tau_local = tau (universal).","Proper time = gamma*tau_local (metric scaling).","c measured locally = l/tau = invariant.","DUAL TIME: tau (universal) vs proper time (metric-scaled)."),
        };

        var dt = new[]{new DualTime("Fundamental interval","tau — universal actualization grain.","N/A","tau is INVARIANT across all of spacetime.","BELOW GEOMETRY — pre-metric."),
            new DualTime("Proper time","N/A","dtau = sqrt(-g_uv dx^u dx^v).","dtau = gamma*tau where gamma = metric factor.","METRIC-DEPENDENT — dilates with gravity."),
            new DualTime("c invariance","c = l/tau — universal ratio.","c = l/tau — same ratio measured locally.","Local measurements use local proper time. Ratio preserved.","LOCAL LORENTZ INVARIANCE — always holds."),
            new DualTime("Gravity coupling","tau unchanged.","Proper time changes with metric.","Gravity warps proper time, not tau.","TIME DILATION IS EMERGENT — from geometry, not actualization."),
        };

        string A=BuildA(tl),B=BuildB(),C=BuildC(tc),D=BuildD(lc),E=BuildE(dt),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI();
        return new T18Result(A,B,C,D,E,F,G,H,I,tl,tc,lc,dt);
    }

    static string BuildA(TimeLayer[] t){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("THE LAYERS OF TIME IN AT");sb.AppendLine();
        sb.AppendLine("  Level  Time Concept              Status");
        sb.AppendLine("  -----  ------------------------  ----------------------------------------");
        foreach(var x in t) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]    {1,-25} {2}",x.Level,x.Time,x.Status));
        return sb.ToString();
    }

    static string BuildB()=>"TIME ONTOLOGY\n\n  TWO NOTIONS OF TIME IN AT:\n\n  1. TAU (fundamental actualization interval):\n     - Universal. Invariant. Pre-geometric.\n     - Exists at Level 0 (before spacetime).\n     - All Q-events everywhere actualize with this interval.\n     - Does NOT change near massive bodies.\n\n  2. PROPER TIME (emergent clock time):\n     - Metric-dependent. Varies with gravity.\n     - Emerges at Level 4 (continuum limit of causal set).\n     - What clocks measure. What observers experience.\n     - Dilates near massive bodies (GR).\n\n  THE KEY INSIGHT:\n    tau is NOT proper time. tau is MORE FUNDAMENTAL.\n    Proper time = tau * (metric factor) * (Q-event count).\n    Gravity warps the metric factor, not tau.";

    static string BuildC(TimeComp[] t){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("TIME IN DIFFERENT ENVIRONMENTS");sb.AppendLine();
        sb.AppendLine("  Scenario          tau                  Proper Time              c local");
        sb.AppendLine("  ----------------- -------------------- ------------------------  ------");
        foreach(var x in t) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-18} {1,-21} {2,-25} {3}",x.Scenario,x.Tau,x.ProperT,x.CT));
        return sb.ToString();
    }

    static string BuildD(LocalC[] l){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("LOCAL c CONSISTENCY");sb.AppendLine();
        sb.AppendLine("  Condition            l             tau           c             Invariant?");
        sb.AppendLine("  -------------------- ------------  ------------  ------------  ---------");
        foreach(var x in l) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-21} {1,-13} {2,-13} {3,-13} {4}",x.Condition,x.L,x.TauVal,x.Cval,x.Invariant));
        sb.AppendLine();sb.AppendLine("  RESOLUTION: c = l/tau is FUNDAMENTAL and UNIVERSAL.");
        sb.AppendLine("  Local measurements use proper time, which dilates.");
        sb.AppendLine("  But l/tau in proper-time units = c. Always.");
        return sb.ToString();
    }

    static string BuildE(DualTime[] d){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("DUAL-TIME STRUCTURE");sb.AppendLine();
        sb.AppendLine("  Aspect               Fundamental                    Emergent                      Relationship");
        sb.AppendLine("  -------------------- -----------------------------  ----------------------------  ------------");
        foreach(var x in d) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-21} {1,-30} {2,-29} {3}",x.Aspect,x.Fundamental,x.Emergent,x.Relationship));
        return sb.ToString();
    }

    static string BuildF()=>"GRAVITY-TIME INTERACTION\n\n  HOW DOES GRAVITY DILATE TIME IN AT?\n\n  1. Mass-energy curves causal structure (QG-001, Level 5-6).\n  2. Curved causal structure -> different Q-event density per volume.\n  3. Near a mass: MORE Q-events per unit volume.\n  4. More Q-events per unit volume -> more actualizations per proper time.\n  5. BUT tau (actualization interval) is unchanged.\n  6. RESULT: proper time = N_events * tau, where N_events depends on metric.\n     Near a mass: same tau, different N_events per proper second.\n     Clocks tick with Q-events. More Q-events per second -> slower clock.\n\n  THIS IS NOT A NEW MECHANISM:\n    It's just GR expressed in AT language.\n    Gravity curves spacetime -> proper time dilates.\n    AT adds: tau is the grain, proper time is the emergent continuum.";

    static string BuildG()=>"HOSTILE REVIEW\n\n1. TAU AS 'UNIVERSAL' IS UNOBSERVABLE:\n   We only ever measure proper time. tau = 5.39e-44 s is\n   computed from G, c, hbar — which we measure in OUR proper time.\n   Claiming tau is 'universal' is an interpretation, not a measurement.\n\n2. THE DUAL-TIME STRUCTURE IS NECESSARY BUT NOT NEW:\n   Any theory with discrete time at the Planck scale must explain\n   how it relates to GR proper time. AT's answer (tau universal,\n   proper time emergent) is the standard answer for discrete QG.\n\n3. NO EXPERIMENTAL TEST:\n   tau is below the Planck scale — untestable. The dual-time\n   interpretation cannot be falsified by current experiments.\n\n4. THE KEY ACHIEVEMENT:\n   AT reconciles:\n   - tau > 0 (discrete actualization, QG-011)\n   - c = l/tau (causal speed, QG-010)\n   - Local Lorentz invariance (c always measured as constant)\n   - GR time dilation (proper time varies with gravity)\n   All four coexist without contradiction.";

    static string BuildH()=>"REMAINING AMBIGUITIES\n\n  1. How EXACTLY does the metric emerge from the causal set?\n     This is external mathematics (Sorkin+). AT inherits it.\n\n  2. Does the Q-event density near a mass change?\n     If yes: proper time dilation = more events per proper second.\n     If no: how does proper time change?\n     ANSWER (from causal set): density increases due to curvature.\n\n  3. Can we OBSERVE tau?\n     No — below Planck scale. tau is a theoretical construct.\n     But its EFFECTS (G, c, hbar) are observable.\n\n  4. Is the dual-time structure unique to AT?\n     No — LQG, causal sets, and other discrete QG approaches\n     also have fundamental discreteness + emergent continuum.\n     AT's contribution: grounding in Q-event actualization.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q3: tau = universal actualization interval (5.39e-44 s).");
        sb.AppendLine("         NOT proper time. Pre-geometric (Level 0).");
        sb.AppendLine("  Q4-Q6: Clocks measure proper time, NOT tau.");
        sb.AppendLine("         Gravity dilates proper time. tau is UNCHANGED.");
        sb.AppendLine("  Q7-Q8: c = l/tau is invariant because l and tau are universal.");
        sb.AppendLine("         Local measurements use proper time. Ratio preserved.");
        sb.AppendLine("  Q9-Q10: Time dilation emerges at Level 4-6 (metric -> GR).");
        sb.AppendLine("         AT HAS a dual-time structure. This is a FEATURE.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  AT HAS A DUAL-TIME STRUCTURE:");
        sb.AppendLine();
        sb.AppendLine("    TAU (fundamental):");
        sb.AppendLine("      - Universal actualization interval.");
        sb.AppendLine("      - Pre-geometric (Level 0).");
        sb.AppendLine("      - INVARIANT — does not change with gravity.");
        sb.AppendLine("      - Defines c = l/tau (universal causal speed).");
        sb.AppendLine();
        sb.AppendLine("    PROPER TIME (emergent):");
        sb.AppendLine("      - Metric-dependent clock time.");
        sb.AppendLine("      - Emerges at Level 4 (causal set -> manifold).");
        sb.AppendLine("      - DILATES with gravity (GR).");
        sb.AppendLine("      - What observers measure.");
        sb.AppendLine();
        sb.AppendLine("  THIS RECONCILES:");
        sb.AppendLine("    [1] c = l/tau constant (tau universal).");
        sb.AppendLine("    [2] Local Lorentz invariance (c always measured the same).");
        sb.AppendLine("    [3] GR time dilation (proper time varies with gravity).");
        sb.AppendLine("    [4] tau > 0 (discrete actualization).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: D — DUAL-TIME STRUCTURE REQUIRED");
        sb.AppendLine("  QG program (QG-001->018, 19 experiments) continues.");
        return sb.ToString();
    }
}
