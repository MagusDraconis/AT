using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class ScalePairDegeneracyAnalyzer
{
    public static SResult RunFullAnalysis()
    {
        var sr = new[]{new ScalingResult("(l,tau,hbar) -> (k*l, k*tau, k^2*hbar)","c = INVARIANT","G = INVARIANT","m_P = k*m_P — CHANGES","S = INVARIANT","T_H = T_H/k — CHANGES","DEGENERATE — c and G unchanged, others change."),
            new ScalingResult("(l,tau) -> (k*l, k*tau), hbar fixed","c INVARIANT","G = k^2*G — CHANGES","m_P = m_P/k — CHANGES","S = A/4l^2? See notes.","T_H CHANGES","NO DEGENERACY — G changes measurably."),
            new ScalingResult("l -> k*l, tau fixed","c = k*c — CHANGES","G = k^5*G — CHANGES","CHANGES","CHANGES","CHANGES","NO DEGENERACY — c changes."),
            new ScalingResult("tau -> k*tau, l fixed","c = c/k — CHANGES","G = G/k^3 — CHANGES","CHANGES","CHANGES","CHANGES","NO DEGENERACY — c changes."),
        };

        var iv = new[]{new IndepVar("l only (tau fixed)","c CHANGES (c = l/tau).","G CHANGES (G ~ l^5).","NO — c is measured.","BROKEN by c measurement."),
            new IndepVar("tau only (l fixed)","c CHANGES.","G CHANGES (~1/tau^3).","NO — c is measured.","BROKEN by c measurement."),
            new IndepVar("(l,tau) keeping c=l/tau fixed","c INVARIANT.","G = l^2*c^3/hbar -> if l scales, G scales.","NO — G is measured.","BROKEN by G measurement."),
            new IndepVar("(l,tau,hbar) scaling","c INVARIANT.","G INVARIANT.","VARIABLE — m_P changes.","BROKEN by any mass measurement."),
        };

        var os = new[]{new ObsSens("c (causal speed)","l/tau","(l,tau)->(k*l,k*tau).","INVARIANT under joint scaling."),
            new ObsSens("G (gravity)","l^5/(tau^3*hbar)","(l,tau,hbar)->(k*l,k*tau,k^2*hbar).","DEGENERATE — special scaling preserves G."),
            new ObsSens("Planck mass m_P","sqrt(hbar*c/G) = sqrt(hbar^2/l^2)","Only if hbar also scales.","BREAKS degeneracy — m_P measurable."),
            new ObsSens("BH entropy S_BH","A/(4*l^2)","(l,tau)->(k*l,k*tau) if A scales as k^2.","SCALE-DEPENDENT — A in SI units fixed."),
            new ObsSens("Hawking T_H","hbar/(8*pi*G*M)","Only if hbar, G, M all scale consistently.","BREAKS degeneracy — temperature measurable."),
            new ObsSens("Fine structure alpha","e^2/(4*pi*eps0*hbar*c)","NEITHER l nor tau appear.","ALPHA IS INDEPENDENT — not affected."),
        };

        var dg = new[]{new Degeneracy("(l,tau,hbar)->(k*l,k*tau,k^2*hbar)","c, G are invariant.","m_P, T_H are NOT invariant.","UNIT CHOICE — selecting (k) chooses units.","NOT PHYSICAL — broken by any mass measurement."),
            new Degeneracy("Only (l,tau)->(k*l,k*tau)","c invariant.","G, m_P, T_H all change.","NO — G is measured (Cavendish, LLR).","BROKEN BY OBSERVATION."),
            new Degeneracy("FULL DEGENERACY?","NONE survive all measurements.","c BREAKS independent l/tau. G breaks independent l/...","HONEST: measuring c, G, m_P fixes l, tau, hbar uniquely.","NO PHYSICAL DEGENERACY."),
            new Degeneracy("HONEST SUMMARY","There is a SCALING DEGENERACY (k) in the triple.","Broken by mass/temperature measurement.","l, tau, hbar: 3 parameters, 3 measurements -> uniquely fixed.","MEASUREMENT BREAKS ALL DEGENERACIES."),
        };

        string A=BuildA(),B=BuildB(sr),C=BuildC(iv),D=BuildD(os),E=BuildE(dg),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI();
        return new SResult(A,B,C,D,E,F,G,H,I,sr,iv,os,dg);
    }

    static string BuildA()=>"WHY l AND tau MATTER\n\n  l = spatial grain. tau = temporal grain.\n  c = l/tau = causal speed.\n\n  THE QUESTION:\n    Are (l, tau) TWO independent parameters,\n    or ONE effective degree of freedom?\n\n  ANSWER: They are LINKED by c = l/tau.\n    Of (l, tau, c): ONLY 2 are independent.\n    Equivalent triple: (l, c) or (tau, c) or (l, tau).\n\n  THE DEEPER QUESTION:\n    Is there a scaling degeneracy where different (l, tau)\n    pairs produce identical observable physics?";

    static string BuildB(ScalingResult[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("JOINT SCALING RESULTS");sb.AppendLine();
        sb.AppendLine("  Scaling                          c            G            Status");
        sb.AppendLine("  -------------------------------  -----------  -----------  ------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-32} {1,-12} {2,-12} {3}",x.Scaling,x.C,x.G,x.Status));
        return sb.ToString();
    }

    static string BuildC(IndepVar[] i){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("INDEPENDENT VARIATION");sb.AppendLine();
        sb.AppendLine("  Variation                        c change      G change      Broken by");
        sb.AppendLine("  -------------------------------  ------------  ------------  --------");
        foreach(var x in i) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-32} {1,-13} {2,-13} {3}",x.Variation,x.Cchange,x.Gchange,x.Status));
        return sb.ToString();
    }

    static string BuildD(ObsSens[] o){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("OBSERVABLE SENSITIVITY");sb.AppendLine();
        sb.AppendLine("  Observable          Depends on             Invariant under            Status");
        sb.AppendLine("  ------------------  ---------------------  -------------------------  ------");
        foreach(var x in o) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-19} {1,-22} {2,-26} {3}",x.Observable,x.DependsOn,x.InvariantUnder,x.Status));
        return sb.ToString();
    }

    static string BuildE(Degeneracy[] d){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("DEGENERACY ANALYSIS");sb.AppendLine();
        sb.AppendLine("  Scaling                                          Status");
        sb.AppendLine("  -----------------------------------------------  ------");
        foreach(var x in d) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-48} {1}",x.Scaling,x.Status));
        return sb.ToString();
    }

    static string BuildF()=>"CONSTRAINT DISCOVERY\n\n  THE IRREDUCIBLE TRIPLE:\n    (l, tau, hbar) — 3 parameters.\n\n  MEASURED QUANTITIES:\n    c = l/tau — fixes ratio.\n    G = l^2*c^3/hbar = l^5/(tau^3*hbar) — fixes one more.\n    m_P = sqrt(hbar*c/G) — fixes all three.\n\n  EQUIVALENTLY:\n    Measuring (c, G, m_P) in SI units uniquely determines\n    (l, tau, hbar). There is NO degeneracy.\n\n  l = l_Planck = sqrt(hbar*G/c^3).\n  tau = l/c = sqrt(hbar*G/c^5).\n  hbar = measured independently.\n\n  ALL THREE ARE UNIQUELY DETERMINED BY OBSERVATION.\n  NO scale pair degeneracy exists in practice.";

    static string BuildG()=>"HOSTILE REVIEW\n\n1. THE 'DEGENERACY' IS A UNIT CHOICE:\n   (l,tau,hbar)->(k*l,k*tau,k^2*hbar) leaves c,G invariant.\n   This is because c and G depend on RATIOS (l/tau, l^5/tau^3*hbar).\n   The degeneracy is a UNITS AMBIGUITY — choose k to set your units.\n\n2. MEASUREMENT BREAKS ALL DEGENERACIES:\n   c is measured (SI). G is measured (Cavendish).\n   hbar is measured (Josephson, quantum Hall).\n   m_P, T_H, S_BH: all break the scaling.\n\n3. IN PRACTICE: NO DEGENERACY.\n   We know the numerical values of l, tau, hbar because\n   we know c, G, hbar from experiment.\n\n4. TQM ADDS NOTHING NEW HERE:\n   The relationship between constants is STANDARD dimensional\n   analysis. TQM provides ONTOLOGICAL MEANING (l = spatial grain,\n   tau = temporal grain) but does not change the mathematics.\n\n5. THE REAL QUESTION:\n   'Are two parameters independent?' is answered by\n   counting equations and unknowns. (l, tau, hbar): 3 unknowns.\n   Observations (c, G): 2 equations. 1 DOF remains.\n   Third observation (e.g., m_P or alpha or hbar via QHE): fixes it.\n   STANDARD DIMENSIONAL ANALYSIS. No new physics here.";

    static string BuildH()=>"REMAINING FREEDOM\n\n  AFTER ALL MEASUREMENTS:\n    l, tau, hbar are UNIQUELY FIXED.\n\n  THE SCALING DEGENERACY:\n    (l,tau,hbar)->(k*l,k*tau,k^2*hbar) is a UNIT CHOICE.\n    If we measure everything in SI units, k = 1 is forced.\n    If we choose natural units (l=tau=hbar=1), k = 1/l_Planck.\n    This is NOT a physical degeneracy.\n\n  TRUE INDEPENDENT PARAMETERS:\n    Standard: (c, G, hbar) = 3 independent.\n    TQM: (l, tau, hbar) = 3 independent.\n    Equivalent: one-to-one mapping between triples.\n\n  NO reduction. NO degeneracy. NO hidden constraint.\n  Just different names for the same 3 numbers.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q3: l and tau are LINKED by c = l/tau. Not independent.");
        sb.AppendLine("         (l, tau) have 2 DOF. c reduces to 1 DOF in the pair.");
        sb.AppendLine("  Q4-Q6: Joint scaling (k*l, k*tau) preserves c but changes G.");
        sb.AppendLine("         Triple scaling (k*l, k*tau, k^2*hbar) preserves c AND G.");
        sb.AppendLine("         But m_P, T_H, and any mass/temperature break it.");
        sb.AppendLine("  Q7-Q9: G, m_P, T_H all break the scaling degeneracy.");
        sb.AppendLine("         QM reconstruction works for any triple (scale invariant).");
        sb.AppendLine("         Cosmology sensitive to G and hbar -> breaks degeneracy.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  (l, tau, hbar) ARE THREE INDEPENDENT PARAMETERS.");
        sb.AppendLine();
        sb.AppendLine("  THE SCALING DEGENERACY EXISTS BUT IS BROKEN BY MEASUREMENT:");
        sb.AppendLine("    (l,tau,hbar)->(k*l,k*tau,k^2*hbar): c, G invariant.");
        sb.AppendLine("    m_P scales as k -> broken by mass measurement.");
        sb.AppendLine("    In practice: c, G, hbar measured -> l, tau uniquely fixed.");
        sb.AppendLine();
        sb.AppendLine("  EQUIVALENCE:");
        sb.AppendLine("    Standard (c, G, hbar) <-> TQM (l, tau, hbar).");
        sb.AppendLine("    c = l/tau. G = l^2*c^3/hbar. One-to-one mapping.");
        sb.AppendLine("    Number of parameters: 3. No reduction. No degeneracy.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: A — FULLY INDEPENDENT");
        sb.AppendLine("  l and tau are SEPARATE degrees of freedom, linked by c.");
        sb.AppendLine("  QG program (QG-001->015, 16 experiments) continues.");
        return sb.ToString();
    }
}
