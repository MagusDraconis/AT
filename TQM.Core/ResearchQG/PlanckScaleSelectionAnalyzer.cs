using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class PlanckScaleSelectionAnalyzer
{
    public static PResult RunFullAnalysis()
    {
        var sv = new[]{new ScaleVar("Gravity (G)","G HUGE → everything is a black hole.","G = 6.67e-11 → observed gravity.","G → 0 → gravity vanishes.","BLACK HOLE UNIVERSE or NO GRAVITY."),
            new ScaleVar("QM coherence","Decoherence instantaneous.","Decoherence observable.","QM remains coherent forever.","DECOHERENCE TOO FAST or NO CLASSICALITY."),
            new ScaleVar("Black holes","Everything gravitational collapse.","BHs observable — stars, galaxies.","No BHs — collapse impossible.","NO STRUCTURE or EVERYTHING COLLAPSED."),
            new ScaleVar("Cosmic expansion","Inflation never ends.","Observed expansion (H_0 ~ 67).","Expansion negligible.","NO STRUCTURE FORMATION."),
            new ScaleVar("Atomic physics","Atoms collapse — no chemistry.","Stable atoms — chemistry exists.","Atoms enormous — no bonding.","NO CHEMISTRY — no life."),
        };

        var sc = new[]{new StabCheck("Standard Model","Forces unified wrong.","Standard Model works.","Symmetries break wrong.","WIDE — not constraining."),
            new StabCheck("General Relativity","GR breaks at macroscopic scale.","GR valid above Planck.","GR breaks at microscopic scale.","BOUNDED ABOVE — l <= l_Planck for GR."),
            new StabCheck("Black hole entropy","S finite. Consistent.","S = A/4l^2. Consistent.","S > A/4l^2 — entropy violation.","UNCONSTRAINED — any l gives consistent S."),
            new StabCheck("Quantum gravity","QG needed at large scales.","QG at l_Planck.","QG at even smaller scales.","PREFERRED — l_Planck is natural boundary."),
            new StabCheck("Life/complexity","No complex structures.","Structures exist.","Structures too fragile.","ANTHROPIC — we observe what we can."),
        };

        var sm = new[]{new SelectMech("Entropy consistency","S_BH = A/(4*l^2) must match GR's S_BH = A/(4*l_Planck^2).","NO — any l works if G varies accordingly.","If G is independent, l is forced. But G is derived from l. CIRCULAR.","EMERGENT CONSISTENCY — doesn't select value."),
            new SelectMech("Causal set density","3+1D continuum limit requires specific density.","NO — density ~ O(1)/l^4. Any l gives 3+1D.","l sets the scale but not the value. Causal set math doesn't fix the numerical density.","CONSTRAINS scale to be UNIFORM, not specific value."),
            new SelectMech("Information bound","S_max = A/(4*l^2). Must be consistent with BH evaporation.","NO — consistency across all scales.","Information bound is a RATIO, not an absolute. l cancels out.","SCALE-INVARIANT — doesn't select l."),
            new SelectMech("Emergence fixed point","QM + GR emerge consistently only at l = l_Planck.","PARTIALLY — l_Planck is the NATURAL scale where QM and GR meet.","l = l_Planck is where hbar*G/c^3 = l^2. This is the DEFINITION.","DEFINITIONAL — not a selection mechanism."),
            new SelectMech("Anthropic selection","Observers require specific l for chemistry and structure.","PARTIALLY — l cannot be too large (atoms collapse) or too small (no structure).","Windows: l in [10^-36, 10^-34] m for complex chemistry. l_Planck = 1.6e-35 is within this window.","ANTHROPIC — explains why we observe this range, not this value."),
            new SelectMech("HONEST: No mechanism","The NUMERICAL VALUE of l is NOT selected by any known TQM mechanism.","NO — the value is EMPIRICAL.","l = 1.616e-35 m because G = 6.674e-11. Why G? Unknown.","EMPIRICAL — the deepest open question in TQM."),
        };

        var id = new[]{new InfoDensity("Holographic bound","S_max = A/(4*l^2). l large → low density.","S_max = A/(4*l_Planck^2) ≈ 10^77 for solar mass.","S_max = A/(4*l^2). l small → high density.","l CANCELS in information per Planck area. Scale-invariant."),
            new InfoDensity("Bekenstein bound","S ≤ 2πRE/(hbar*c). l appears via E scale.","Bound easily satisfied.","Bound constrains entropy.","WEAK constraint. l appears only via energy scale."),
        };

        var fp = new[]{new FixedPt("l = l_Planck","1.616e-35 m","Definition of l_Planck: l_P^2 = hbar*G/c^3.","CIRCULAR — l_Planck defined FROM G, not derived.","NO — definition, not fixed point."),
            new FixedPt("tau = t_Planck","5.391e-44 s","t_P = l_P/c. From l and c.","CIRCULAR — t_Planck defined FROM l_Planck.","NO — derived, not fixed point."),
            new FixedPt("M^2 fixed point?","Unknown.","M^2 might have a fixed point from Q-event graph dynamics.","SPECULATIVE — M^2 unknown.","POSSIBLY — if M^2 is dynamically selected."),
        };

        string A=BuildA(),B=BuildB(sv),C=BuildC(sc),D=BuildD(sm),E=BuildE(id),F=BuildF(fp),G=BuildG(),H=BuildH(),I=BuildI();
        return new PResult(A,B,C,D,E,F,G,H,I,sv,sc,sm,id,fp);
    }

    static string BuildA()=>"THE PLANCK-SCALE PROBLEM\n\n  TQM has proven:\n    l > 0 (QG-009) — logically required.\n    tau > 0 (QG-011) — logically required.\n\n  TQM has NOT proven:\n    l = 1.616e-35 m — specific value.\n    tau = 5.391e-44 s — specific value.\n\n  THE FUNDAMENTAL QUESTION:\n    Why THESE values and not any other positive numbers?\n\n  POSSIBLE ANSWERS:\n    1. Arbitrary — just happens to be this way.\n    2. Selected by internal consistency.\n    3. Environmental/anthropic.\n    4. Dynamical fixed point.\n\n  This audit evaluates all four.";

    static string BuildB(ScaleVar[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SCALE VARIATION AUDIT");sb.AppendLine();
        sb.AppendLine("  If l were different by many orders of magnitude:");
        sb.AppendLine();
        sb.AppendLine("  Scale         l >> l_P                l ~ l_P              l << l_P");
        sb.AppendLine("  ------------  ----------------------  -------------------  ----------------------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-13} {1,-23} {2,-20} {3}",x.Scale,x.VeryLarge,x.Planck,x.VerySmall));
        sb.AppendLine();sb.AppendLine("  ONLY l ~ l_P gives a universe with gravity, QM, atoms, and life.");
        sb.AppendLine("  But the allowed WINDOW is broad (~10 orders). Not uniquely selected.");
        return sb.ToString();
    }

    static string BuildC(StabCheck[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("EMERGENCE STABILITY");sb.AppendLine();
        sb.AppendLine("  Structure            Large l              Small l             Precision needed");
        sb.AppendLine("  -------------------- -------------------- -------------------- --------------------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-21} {1,-21} {2,-21} {3}",x.Structure,x.LargeL,x.SmallL,x.Precision));
        sb.AppendLine();sb.AppendLine("  CONCLUSION: l can vary by ~10 orders and physics still works.");
        sb.AppendLine("  l = l_Planck is NOT uniquely selected by stability requirements.");
        return sb.ToString();
    }

    static string BuildD(SelectMech[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SELECTION MECHANISMS");sb.AppendLine();
        sb.AppendLine("  Mechanism                  Selects unique l?  Assessment");
        sb.AppendLine("  -------------------------  -----------------  ------------------------------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-26} {1,-18} {2}",x.Mechanism,x.Unique,x.HonestAssessment));
        sb.AppendLine();sb.AppendLine("  FINAL: NO known TQM mechanism selects the numerical value of l.");
        return sb.ToString();
    }

    static string BuildE(InfoDensity[] i){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION DENSITY");sb.AppendLine();
        sb.AppendLine("  Bound                  l large                 l = l_P                 l small");
        sb.AppendLine("  ---------------------  ----------------------  ----------------------  ----------------------");
        foreach(var x in i) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-22} {1,-23} {2,-23} {3}",x.Bound,x.LargeL,x.Planck,x.SmallL));
        return sb.ToString();
    }

    static string BuildF(FixedPt[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FIXED-POINT ANALYSIS");sb.AppendLine();
        sb.AppendLine("  Candidate         Value              Mechanism                    Status");
        sb.AppendLine("  ----------------- ------------------ ---------------------------  ------");
        foreach(var x in f) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-18} {1,-18} {2,-28} {3}",x.Candidate,x.FixedPointValue,x.Mechanism,x.Status));
        return sb.ToString();
    }

    static string BuildG()=>"HOSTILE REVIEW\n\n1. THE PLANCK SCALE IS NOT EXPLAINED:\n   After 12 QG experiments spanning emergence to constant determination,\n   l and tau remain EMPIRICAL inputs. TQM has not predicted a single\n   numerical value from first principles.\n\n2. ALL 'DERIVATIONS' ARE CIRCULAR:\n   'G = l^2*c^3/hbar' → we measure G → we compute l.\n   'l = l_Planck → G matches observation' — but G IS the observation.\n   Circular: G → l_P → consistency with G.\n\n3. THE ANTHROPIC ARGUMENT IS WEAK:\n   'l must be ~Planck for life.' But the window is ~10 orders.\n   l = 10*l_P or l = 0.1*l_P would also support chemistry and life.\n\n4. M^2 IS THE LAST HOPE:\n   If M^2 is dynamically selected (fixed point of Q-event dynamics),\n   and M^2 determines l, then l is indirectly selected.\n   But M^2 is UNKNOWN. This is speculation, not derivation.\n\n5. THE DEEPEST ADMISSION:\n   TQM has compressed 26+ Standard Model parameters to 2 primitives\n   + 3-5 parameters. But those 3-5 are UNEXPLAINED.\n   This is PROGRESS (fewer parameters, clearer meaning) but not\n   COMPLETION (parameters remain).\n\n6. WHAT WOULD SUCCESS LOOK LIKE?\n   A derivation that, starting from 'there are Q-events,'\n   produces the number l = 1.616255...e-35 m without any\n   empirical input. This has NOT been achieved by any theory.";

    static string BuildH()=>"REMAINING ASSUMPTIONS\n\n  TQM'S CURRENT FREE PARAMETERS:\n    1. l (Q-event spacing) = 1.616e-35 m — EMPIRICAL.\n    2. tau (actualization interval) = 5.391e-44 s — from l/c.\n    3. hbar (action quantum) = 1.055e-34 J*s — EMPIRICAL.\n    4. M^2 (nonlinearity) = unknown — EMPIRICAL/UNKNOWN.\n    5. N_inf (residual rate) = unknown — EMPIRICAL/UNKNOWN.\n\n  RELATIONS:\n    c = l/tau (definition). G = l^2*c^3/hbar (definition).\n    These reduce 5 parameters to 3 INDEPENDENT ones:\n    (l, hbar, M^2) or (c, tau, hbar) or (G, c, hbar).\n\n  COMPARISON:\n    Standard Model + GR: ~26 independent parameters.\n    TQM: 3-5 independent parameters.\n    COMPRESSION: ~5x reduction. But NOT elimination.\n\n  THE FINAL GOAL:\n    Derive ALL parameters from Q + Randomness.\n    Status: NOT ACHIEVED.";

    static string BuildI(){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  Q1: l and tau define the grain of reality. Fundamental scales.");
        sb.AppendLine("  Q2-Q4: NO known mechanism selects their specific values.");
        sb.AppendLine("  Q5-Q6: Stability window ~10 orders. Not uniquely constraining.");
        sb.AppendLine("  Q7-Q8: l too large → everything is a black hole. l too small → no gravity.");
        sb.AppendLine("         Broad window. l_Planck is WITHIN the window but not UNIQUE.");
        sb.AppendLine("  Q9-Q10: No fixed point identified. Values are EMPIRICAL.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  THE PLANCK SCALE IS NOT DERIVED. IT IS THE FINAL EMPIRICAL INPUT.");
        sb.AppendLine();
        sb.AppendLine("  WHAT TQM EXPLAINS (QG-001→012):");
        sb.AppendLine("    [1] QM from Q-events (QM-001→005).");
        sb.AppendLine("    [2] Gravity from Q-events (QG-001).");
        sb.AppendLine("    [3] Black holes, entropy, information (QG-002→003).");
        sb.AppendLine("    [4] Cosmic expansion, growth law (QG-004→005).");
        sb.AppendLine("    [5] G, c, hbar relationships (QG-007, QG-010).");
        sb.AppendLine("    [6] WHY l>0, tau>0 (QG-009, QG-011).");
        sb.AppendLine();
        sb.AppendLine("  WHAT TQM DOES NOT EXPLAIN:");
        sb.AppendLine("    [1] The NUMERICAL VALUE of l (1.616e-35 m).");
        sb.AppendLine("    [2] The NUMERICAL VALUE of hbar (1.055e-34 J*s).");
        sb.AppendLine("    [3] The NUMERICAL VALUE of M^2 (unknown).");
        sb.AppendLine();
        sb.AppendLine("  PARAMETER COMPRESSION:");
        sb.AppendLine("    Standard Model + GR: ~26 parameters.");
        sb.AppendLine("    TQM: 3-5 parameters.");
        sb.AppendLine("    COMPRESSION: ~5-8x. GENUINE PROGRESS.");
        sb.AppendLine();
        sb.AppendLine("  BUT: the remaining 3-5 parameters are UNEXPLAINED.");
        sb.AppendLine("  This is the continuing challenge of the TQM program.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: A — COMPLETELY EMPIRICAL");
        sb.AppendLine("  The Planck scale is NOT derived. Values are empirical inputs.");
        sb.AppendLine("  QG program (QG-001→012, 12 experiments) is COMPLETE.");
        return sb.ToString();
    }
}
