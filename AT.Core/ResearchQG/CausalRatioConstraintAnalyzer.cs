using System.Globalization;

namespace AT.Core.ResearchQG;

public static class CausalRatioConstraintAnalyzer
{
    public static RResult RunFullAnalysis()
    {
        var rv = new[]{new RatioVar("l/tau << c_obs","c slow. Causal propagation slow.","Physics exists. All time scales stretch. Observers measure their c as 'c'.","C IS DEFINED BY THE RATIO — observers always measure l/tau as c."),
            new RatioVar("l/tau = c_obs","c = 299792458 m/s.","Standard physics.","NOMINAL — our universe."),
            new RatioVar("l/tau >> c_obs","c fast. Causal propagation fast.","Physics exists. All time scales compress. Observers measure their c as 'c'.","C IS DEFINED BY THE RATIO — observers always measure l/tau as c."),
        };

        var fc = new[]{new FixedC("c = l/tau (definition)","(l,tau): 2 DOF. c fixes ratio -> 1 DOF.","(l->k*l, tau->k*tau) preserves c but changes G.","DEFINITION — not a hidden constraint."),
            new FixedC("c measured (SI)","(l,tau): determined by c AND G measurement.","No degeneracy — c AND G measured.","MEASUREMENT — fixes both l and tau."),
            new FixedC("c + G + hbar measured","l, tau uniquely fixed.","ZERO degeneracy.","ALL THREE MEASURED — l, tau, hbar uniquely determined."),
        };

        var oe = new[]{new ObsEffect("c (causal speed)","l/tau","c IS the ratio. Measuring c IS measuring l/tau.","TAUTOLOGY — c = l/tau by definition."),
            new ObsEffect("G (gravity)","l^5/(tau^3*hbar)","G changes if ratio changes. G is measured -> constrains ratio.","G MEASUREMENT + c = l/tau -> l and tau fixed."),
            new ObsEffect("Planck mass","sqrt(hbar*c/G)","m_P changes if ratio changes. Measured.","m_P BREAKS any remaining degeneracy."),
            new ObsEffect("RAR scale g†","c*H0/(2*pi)","g† depends on c -> depends on ratio.","OBSERVABLE — constrains l/tau via cosmology."),
        };

        var hr = new[]{new HiddenRatio("Causal consistency","No — any ratio works. c = l/tau by definition.","NO — definition, not constraint.","NOT HIDDEN — c IS the definition of the ratio."),
            new HiddenRatio("Information propagation","No — information speed IS l/tau.","NO — tautology.","NOT HIDDEN — information propagates AT the causal speed."),
            new HiddenRatio("Horizon entropy","BH entropy constrains l, not l/tau.","NO — S = A/4l^2. l appears, not l/tau.","NOT HIDDEN — entropy constrains l, not the ratio."),
            new HiddenRatio("HONEST: No hidden ratio constraint","c = l/tau IS the definition. The VALUE is empirical.","l and tau are independent, but their ratio is fixed by measuring c.","c IS NOT A CONSTRAINT ON l AND tau — it IS their ratio."),
        };

        string A=BuildA(),B=BuildB(rv),C=BuildC(fc),D=BuildD(oe),E=BuildE(hr),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI();
        return new RResult(A,B,C,D,E,F,G,H,I,rv,fc,oe,hr);
    }

    static string BuildA()=>"THE RATIO PROBLEM\n\n  QG-015: l and tau are INDEPENDENT parameters.\n  BUT: c = l/tau is observed to be CONSTANT.\n  TENSION: If l and tau are independent, why is their ratio fixed?\n\n  RESOLUTION:\n    c IS the definition of the ratio l/tau.\n    'c is constant' means 'l/tau is constant' — the same statement.\n    c is fixed because we MEASURE it, not because of a hidden constraint.\n\n  KEY INSIGHT:\n    l/tau = c is a DEFINITION, not a constraint.\n    c = 299792458 m/s because that's the measured ratio.\n    If l or tau were different, c would be different.\n    There is NO hidden mechanism forcing l/tau to a specific value.";

    static string BuildB(RatioVar[] r){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("RATIO VARIATION");sb.AppendLine();
        sb.AppendLine("  l/tau ratio        Physics                                         Status");
        sb.AppendLine("  ------------------  ----------------------------------------------  ------");
        foreach(var x in r) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-19} {1,-47} {2}",x.Ratio,x.Physics,x.Status));
        return sb.ToString();
    }

    static string BuildC(FixedC[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FIXED-c ANALYSIS");sb.AppendLine();
        sb.AppendLine("  Constraint           Free params    Degeneracy                Status");
        sb.AppendLine("  -------------------  -------------  ------------------------  ------");
        foreach(var x in f) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-20} {1,-14} {2,-26} {3}",x.Constraint,x.FreeParams,x.Degeneracy,x.Status));
        return sb.ToString();
    }

    static string BuildD(ObsEffect[] o){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("OBSERVABLE EFFECTS");sb.AppendLine();
        sb.AppendLine("  Observable          Depends on      Ratio change effect          Status");
        sb.AppendLine("  ------------------  --------------  ---------------------------  ------");
        foreach(var x in o) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-19} {1,-15} {2,-28} {3}",x.Observable,x.DependsOn,x.RatioChange,x.Status));
        return sb.ToString();
    }

    static string BuildE(HiddenRatio[] h){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("HIDDEN RATIO SEARCH");sb.AppendLine();
        sb.AppendLine("  Candidate                  Status");
        sb.AppendLine("  -------------------------  ------");
        foreach(var x in h) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-26} {1}",x.Candidate,x.Status));
        return sb.ToString();
    }

    static string BuildF()=>"CONSISTENCY ANALYSIS\n\n  l/tau = c is a DEFINITION, not a constraint.\n\n  WHY:\n    1. c IS the causal speed — defined as l/tau.\n    2. Measuring c IS measuring l/tau.\n    3. There is no deeper mechanism.\n\n  INDEPENDENCE CLARIFIED:\n    l and tau ARE independent parameters.\n    Their RATIO is observed (c).\n    Their PRODUCT (or other combination) is observed (G via l^5/tau^3*hbar).\n    Two observations (c, G) fix two unknowns (l, tau).\n    Three observations (c, G, hbar) fix the third (if not already known).\n\n  THIS IS STANDARD DIMENSIONAL ANALYSIS.\n  No new physics. No hidden constraints.\n  Just the relationship between definitions and measurements.";

    static string BuildG()=>"HOSTILE REVIEW\n\n1. THIS AUDIT IS MORE HONEST THAN PROFOUND:\n   'c = l/tau is a definition, not a constraint' — this is true\n   but it doesn't advance our understanding. It just clarifies\n   what we already knew.\n\n2. THE 'TENSION' WAS ILLUSORY:\n   QG-015 said 'l and tau are independent.' This audit asked\n   'then why is l/tau fixed?' The answer: because we MEASURED it.\n   This was never a real tension — just a semantic confusion.\n\n3. NO NEW PHYSICS:\n   This audit does not discover new constraints. It clarifies\n   existing ones. Useful, but limited.\n\n4. THE VALUE OF c REMAINS EMPIRICAL:\n   c = 299792458 m/s because l and tau have the values they do.\n   And l and tau have those values because G and hbar have theirs.\n   The chain of 'why' always terminates at measurement.";

    static string BuildH()=>"REMAINING AMBIGUITIES\n\n  NONE. The ratio question is fully clarified.\n\n  l/tau = c by DEFINITION.\n  c is measured.\n  Therefore l/tau is measured.\n  No hidden constraint. No ambiguity.\n\n  The only remaining question is the VALUE:\n    Why does l/tau = 299792458 m/s?\n  Answer: Because l = 1.616e-35 m and tau = 5.391e-44 s.\n  Why those values? Answered in QG-012: EMPIRICAL.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q2: l and tau ARE independent. Their ratio is measured (c).");
        sb.AppendLine("         Joint scaling preserves c but changes G — broken by measurement.");
        sb.AppendLine("  Q3-Q6: c, G, m_P, g† ALL depend on l/tau — but these ARE the");
        sb.AppendLine("         measurements that fix l/tau. No hidden constraint.");
        sb.AppendLine("  Q7-Q10: No hidden ratio law. No deeper mechanism. No invariance missed.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  c = l/tau IS A DEFINITION, NOT A HIDDEN CONSTRAINT.");
        sb.AppendLine();
        sb.AppendLine("  THE 'TENSION' BETWEEN QG-015 AND OBSERVATION IS RESOLVED:");
        sb.AppendLine("    QG-015: l and tau ARE independent (2 DOF).");
        sb.AppendLine("    Observation: c = l/tau is fixed (1 DOF reduction).");
        sb.AppendLine("    Resolution: Measuring c reduces DOF from 2 to 1.");
        sb.AppendLine("    Measuring G reduces DOF from 1 to 0 (fixes both l and tau).");
        sb.AppendLine();
        sb.AppendLine("  THIS IS STANDARD DIMENSIONAL ANALYSIS.");
        sb.AppendLine("  AT adds no new mathematics — it adds ONTOLOGICAL MEANING:");
        sb.AppendLine("    l = spatial grain of reality.");
        sb.AppendLine("    tau = temporal grain of becoming.");
        sb.AppendLine("    c = l/tau = causal update speed.");
        sb.AppendLine("    G = l^5/(tau^3*hbar) = emergent gravity.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: A — FULLY INDEPENDENT");
        sb.AppendLine("  l and tau are independent. No hidden ratio constraint exists.");
        sb.AppendLine("  QG program (QG-001->016, 17 experiments) continues.");
        return sb.ToString();
    }
}
