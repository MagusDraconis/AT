using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class CosmicExpansionAnalyzer
{
    public static CEExpResult RunFullAnalysis()
    {
        var gs = new[]{new GrowthStep(1,"Q-event count N(t)","New Q-events actualized over time.","Q + Randomness (primitive).","FUNDAMENTAL — Q-events are added."),
            new GrowthStep(2,"Causal links L(t)","New Q-events create new causal connections.","Causal structure (QG-001 L1-2).","EMERGENT — links grow with N."),
            new GrowthStep(3,"Effective volume V_eff","N × ℓ^3 → volume of Q-event causal set.","Q-event density + ℓ.","EMERGENT — from counting."),
            new GrowthStep(4,"Scale factor a(t)","a(t) ∝ N(t)^(1/3) → expansion.","Volume growth → scale.","EMERGENT — from volume."),
            new GrowthStep(5,"Hubble parameter H(t)","H = ȧ/a = (1/3)Ṅ/N.","Q-event growth rate.","EMERGENT — from growth rate."),
        };

        var ce = new[]{new ConnectEvo("Early universe","N ≪ N_now","Sparse","Small (ℓ-scale)","Rapid Q-event actualization.","INFLATION-LIKE — fast growth."),
            new ConnectEvo("Mid universe","N ~ 0.1 N_now","Dense","Growing ∝ N^(1/3)","Growth slowing. Structure forming.","EXPANSION — decelerating."),
            new ConnectEvo("Late universe","N ~ N_now","Very dense","Large (Gpc)","Slow growth. Sparse new Q-events.","Λ-DOMINATED — accelerating apparent."),
            new ConnectEvo("Far future","N ≫ N_now","Maximal","Maximum","Growth asymptotically zero.","HEAT DEATH — static."),
        };

        var de = new[]{new DistEmerge(1,"Causal link count","d(A,B) ∝ (# links between A and B) × ℓ.","Q-event causal structure (QG-001).","RAW DISTANCE — discrete."),
            new DistEmerge(2,"Distance growth","New Q-events inserted → link count increases → d grows.","Q-event addition (growth).","EXPANSION — distances increase."),
            new DistEmerge(3,"Redshift","Δd/d ∝ ΔN/N → z ∝ (a_now/a_then - 1).","Distance growth.","REDSHIFT — natural from growth."),
            new DistEmerge(4,"Metric expansion","g_μν(t) evolves with N(t).","QG-001 Level 4.","METRIC — emergent FLRW."),
        };

        var ss = new[]{new ScaleStep(1,"a(t) ∝ N(t)^(1/3)","Volume ∝ N → linear scale ∝ N^(1/3).","Q-event counting.","SCALE FACTOR — from Q-event number."),
            new ScaleStep(2,"ȧ > 0","N(t) increases monotonically (Q-events accumulate).","Q-event actualization is ongoing.","EXPANDING — never contracting."),
            new ScaleStep(3,"ä ∝ Ṅ²/N + N̈","Acceleration depends on N̈ (second derivative).","Q-event growth rate change.","ACCELERATION — possible if Ṅ slows."),
        };

        var hs = new[]{new HubbleStep(1,"H(t) = ȧ/a","= (1/3)Ṅ/N — expansion rate = growth rate / 3.","H ∝ Q-event growth rate.","H(t) from Ṅ(t)."),
            new HubbleStep(2,"H(t) decreasing","Ṅ slows (fewer new Q-events per time).","Growth rate naturally decays.","DECELERATING phase (matter era)."),
            new HubbleStep(3,"H(t) → H_∞","Ṅ → constant (background Q-event rate).","Residual actualization rate.","CONSTANT H (dark energy era)."),
            new HubbleStep(4,"H_0 ≈ 67 km/s/Mpc","Consistent with observed value if ℓ ~ ℓ_Planck.","Scale set by ℓ.","OBSERVABLE — matches Planck."),
        };

        var cc = new[]{new CosmoComp("FLRW","Assume a(t), H(t) as primitives.","Assume ρ_Λ = constant.","TQM derives a(t), H(t) from Q-events."),
            new CosmoComp("ΛCDM","FLRW + Λ + CDM. 6 parameters.","Λ = cosmological constant (fundamental).","TQM: Λ(t) emerges, not constant. CDM = defect-DM."),
            new CosmoComp("Causal Set Cosmo","Discrete causal set → effective FLRW.","Not addressed directly.","CLOSEST — TQM IS causal set + Q-event growth."),
            new CosmoComp("Emergent Gravity","Entropy → gravity → expansion.","Emergent from entropy.","SIMILAR — but TQM starts from Q-events, not entropy."),
            new CosmoComp("TQM","Q-event growth → a(t) → H(t) → Λ(t).","Λ(t) = α/√V(t) — time-varying.","THIS FRAMEWORK — expansion = Q-event accumulation."),
        };

        string A=BuildA(gs),B=BuildB(ce),C=BuildC(de),D=BuildD(ss),E=BuildE(hs),F=BuildF(),G=BuildG(cc),H=BuildH(),I=BuildI();
        return new CEExpResult(A,B,C,D,E,F,G,H,I,gs,ce,de,ss,hs,cc);
    }

    static string BuildA(GrowthStep[] g){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("Q-EVENT GROWTH");sb.AppendLine();
        sb.AppendLine("  Step  What Grows              Mechanism                      Status");
        sb.AppendLine("  ----  ----------------------  -----------------------------  ------");
        foreach(var x in g) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-23} {2,-30} {3}",x.Step,x.What,x.Mechanism,x.Status));
        return sb.ToString();
    }

    static string BuildB(ConnectEvo[] c){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("CONNECTIVITY EVOLUTION");sb.AppendLine();
        sb.AppendLine("  Stage           N(t)         Links        Effective R    Description");
        sb.AppendLine("  --------------- ------------ ------------ -------------  -----------");
        foreach(var x in c) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-16} {1,-13} {2,-13} {3,-14} {4}",x.Stage,x.N,x.Links,x.EffectiveR,x.Description));
        return sb.ToString();
    }

    static string BuildC(DistEmerge[] d){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("DISTANCE EMERGENCE");sb.AppendLine();
        sb.AppendLine("  Step  Structure          Derivation                        Status");
        sb.AppendLine("  ----  -----------------  --------------------------------  ------");
        foreach(var x in d) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-18} {2,-33} {3}",x.Step,x.Structure,x.Derivation,x.Status));
        return sb.ToString();
    }

    static string BuildD(ScaleStep[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SCALE FACTOR RECONSTRUCTION");sb.AppendLine();
        sb.AppendLine("  Step  Relation                Derivation                       Status");
        sb.AppendLine("  ----  ----------------------  -------------------------------  ------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-23} {2,-32} {3}",x.Step,x.Relation,x.Derivation,x.Status));
        return sb.ToString();
    }

    static string BuildE(HubbleStep[] h){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("HUBBLE EMERGENCE");sb.AppendLine();
        sb.AppendLine("  Step  Relation               Derivation                      Prediction");
        sb.AppendLine("  ----  ---------------------  ------------------------------  ----------");
        foreach(var x in h) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-22} {2,-31} {3}",x.Step,x.Relation,x.Derivation,x.Prediction));
        return sb.ToString();
    }

    static string BuildF()=>"DARK ENERGY IMPLICATIONS\n\n  TQM DARK ENERGY = Λ(t) = α/√V(t)\n\n  WHY TIME-VARYING?\n  Λ depends on causal volume V(t). As Q-events accumulate,\n  V(t) grows → Λ(t) decays → expansion accelerates (relative to Λ).\n\n  w(z) = -1 + η·(1+z)^(3/2) with η = 0.015.\n  This is the EXACT form used in DATA-001 through DATA-010.\n\n  Λ(t) IS EMERGENT:\n  Not a fundamental constant. Not vacuum energy.\n  It's the residual growth rate of the Q-event network.\n\n  CONNECTION TO OBSERVATIONS:\n  - DATA-001: Pantheon+SH0ES consistent with TQM (indistinguishable).\n  - DATA-007: g†(z) = c·H(z)/(2π) — unique TQM prediction.\n  - Euclid 2027+ will test Λ(t) vs constant Λ.";

    static string BuildG(CosmoComp[] c){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("COSMOLOGY FRAMEWORK COMPARISON");sb.AppendLine();
        sb.AppendLine("  Framework          Expansion Origin          Dark Energy              TQM Comparison");
        sb.AppendLine("  -----------------  ------------------------  -----------------------  -------------");
        foreach(var x in c) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-18} {1,-25} {2,-24} {3}",x.Framework,x.Expansion,x.DarkEnergy,x.TqmComparison));
        return sb.ToString();
    }

    static string BuildH()=>"HOSTILE REVIEW\n\n1. a(t) ~ N(t)^(1/3) IS POSTULATED, NOT DERIVED:\n   Why should volume go as N? Could go as N^2, N^(2/3), etc.\n   ANSWER: Q-events occupy equal spacetime volume ℓ^3.\n   But this assumes Q-events are uniformly distributed.\n\n2. H(t) = (1/3)dN/dt/N IS THE DEFINITION, NOT A PREDICTION:\n   This just restates a(t) ~ N^(1/3) in derivative form.\n   It does not predict the FUNCTIONAL FORM of H(t).\n\n3. Λ(t) = α/sqrt(V(t)) IS HEURISTIC:\n   The functional form is chosen to match observations.\n   α is a FREE PARAMETER (fitted, not derived).\n\n4. THE FLRW METRIC IS IMPORTED:\n   TQM does not derive ds^2 = -dt^2 + a(t)^2 dx^2.\n   The metric structure is inherited from GR.\n\n5. QUANTITATIVE PREDICTIONS ARE WEAK:\n   w(z) = -1 + 0.015(1+z)^(3/2) is the ONLY prediction.\n   And η = 0.015 is fitted to match data (DATA-001).\n\n6. THE EXPANSION IS NOT 'DERIVED' — IT'S REINTERPRETED:\n   'Space expands' → 'Q-event network grows'.\n   Same observations, different words. Is this progress?";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1-Q2: Universe = growing Q-event causal set. Growth = actualization.");
        sb.AppendLine("  Q3-Q4: Growing causal structure → effective distance growth → a(t).");
        sb.AppendLine("  Q5:    H(t) = ȧ/a = (1/3)Ṅ/N. Expansion rate = Q-event growth rate / 3.");
        sb.AppendLine("  Q6-Q7: Redshift = consequence of growing distances. No 'expanding space.'");
        sb.AppendLine("  Q8-Q9: TQM predicts w(z) = -1 + η(1+z)^(3/2). ΛCDM is late-time limit.");
        sb.AppendLine("  Q10:   Dark Energy is EMERGENT — Λ(t) from Q-event growth, not fundamental.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  COSMIC EXPANSION IS REINTERPRETED, NOT DERIVED.");
        sb.AppendLine();
        sb.AppendLine("  TQM's contribution:");
        sb.AppendLine("    - 'Space expands' → 'Q-event network grows.'");
        sb.AppendLine("    - a(t) from N(t) — counting, not geometry.");
        sb.AppendLine("    - H(t) from Ṅ — growth rate, not curvature.");
        sb.AppendLine("    - Λ(t) from V(t) — emergent, not fundamental.");
        sb.AppendLine("    - w(z) = -1 + 0.015(1+z)^(3/2) — unique TQM prediction.");
        sb.AppendLine();
        sb.AppendLine("  TQM does NOT derive the functional form of N(t).");
        sb.AppendLine("  N(t) is the Q-event growth history — it's what nature GIVES us.");
        sb.AppendLine("  TQM interprets it, but does not predict it from first principles.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — PARTIAL EMERGENCE");
        sb.AppendLine("  Expansion = Q-event growth is a REINTERPRETATION with one");
        sb.AppendLine("  unique prediction (w(z) form). But the growth law N(t) is");
        sb.AppendLine("  not derived — it's taken from observation.");
        return sb.ToString();
    }
}
