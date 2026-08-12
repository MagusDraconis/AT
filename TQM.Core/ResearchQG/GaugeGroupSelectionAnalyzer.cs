using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class GaugeGroupSelectionAnalyzer
{
    public static GGSResult RunFullAnalysis()
    {
        var candidates = BuildCandidates();
        var derivations = BuildDerivations();
        return new GGSResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(candidates),BuildF(),BuildG(),BuildH(),BuildI(),candidates,derivations);
    }

    static GaugeDeriv[] BuildDerivations()
    {
        return new GaugeDeriv[]
        {
            new GaugeDeriv("U(1) EM",1,
                "Phase winding on S¹. ∮∇θ·dl = 2πn (QG-033/034). Integer topological charge n → conserved charge → U(1) gauge symmetry. Gauge connection A_μ = ∂_μθ.",
                "DIRECT from S¹ phase topology. Phase angle θ IS the U(1) parameter. Gauge transformation θ→θ+α(x) is a local phase redefinition. Photon = massless phase wave (n=0).",
                "C: COMPLETE. U(1) IS S¹. The circle's topology generates the symmetry. No additional assumption needed."),
            new GaugeDeriv("SU(2) weak",2,
                "Binary topological sectors: n=+1 and n=-1 winding (matter/anti-matter, QG-035). Spinor double cover of SO(3) → SU(2). Isospin doublets (e,νe).",
                "PARTIAL: n↔-n symmetry suggests Z₂, lifted to SU(2) via spinor structure. Weak doublets map to binary winding pairs. But the LIFT from Z₂ to full SU(2) is not derived.",
                "B: PARTIAL. Binary structure (n=±1) is real in TQM. But SU(2)'s full continuous rotations need an independent justification. WEAK CORRESPONDENCE."),
            new GaugeDeriv("SU(3) color",3,
                "Tri-winding confinement: proton = confined n=3 (QG-034). Three n=1 vortices bound by QCD-like confinement. Color = which of 3 winding substructures.",
                "PARTIAL: The number 3 matches confined n=3. Color triplets map to 3 bound vortex substructures. But WHY 3 (not 2 or 4) is not derived. Confinement mechanism is external (borrowed QCD).",
                "B: PARTIAL. Tri-structure (n=3 confinement) is suggestive. But SU(3) gauge dynamics is NOT derived. WEAK-TO-MODERATE CORRESPONDENCE."),
            new GaugeDeriv("Pattern: 1-2-3",0,
                "Winding sectors n=1,2,3 map to U(1),SU(2),SU(3) ranks 1,2,3. The SM gauge group ranks ARE the first three winding numbers.",
                "HINT: rank(U(1))=1, rank(SU(2))=2, rank(SU(3))=3. Coincidence with n=1,2,3 winding sectors. But this is a NUMERICAL coincidence, not a derivation. No mechanism links them.",
                "A/B: INTRIGUING PATTERN but NOT a derivation. Could be coincidence. Requires a mechanism connecting winding number to gauge rank."),
        };
    }

    static GaugeCandidate[] BuildCandidates()
    {
        return new GaugeCandidate[]
        {
            new GaugeCandidate("U(1) only","Single phase circle. One winding sector (n=±1).","Derived: S¹ topology.","HIGH: simplest stable","ONE charge. No atoms (no bound states). Only Coulomb.","INSUFFICIENT: No confinement, no weak decay, no nuclear force. No stable matter."),
            new GaugeCandidate("U(1)×SU(2)","Phase + spinor structure. EM + weak.","U(1) derived; SU(2) partial.","HIGH","Electroweak. Atoms OK. But no quarks/hadrons. No protons.","INSUFFICIENT: No strong force. No nuclei. No stable matter."),
            new GaugeCandidate("SU(3)×SU(2)×U(1)","Tri-winding + spinor + phase. The SM.","U(1) C; SU(2) B; SU(3) B.","HIGH (observed)","FULL: Atoms, chemistry, nuclei, stable matter, life.","WORKS: The observed universe. But NOT derived from TQM — largely empirical."),
            new GaugeCandidate("SU(5) GUT","Unified. 5-plet (quarks+leptons).","NOT derived from TQM. External GUT hypothesis.","MEDIUM: proton decay unobserved","Unified at 10^16 GeV. Proton decay rate problem (not observed, tau>10^34 yr).","DISFAVORED: Predicts proton decay at observable rate. NOT observed. Minimal SU(5) ruled out."),
            new GaugeCandidate("SO(10) GUT","Spinor 16 = one generation.","NOT derived from TQM.","MEDIUM","Right-handed neutrino naturally. But proton decay still.","UNPROVEN: Elegant (16-plet) but no experimental support. Proton decay still required."),
            new GaugeCandidate("E6","Exceptional group. 27-plet.","NOT derived from TQM.","LOW: too many states","78 generators, 27-plet. Many exotic particles.","DISFAVORED: Excessive particle content. No observational motivation."),
            new GaugeCandidate("E8","Largest exceptional group. 248-dim.","NOT derived from TQM. LQG/string proposals external.","LOW: enormous","248-dim adjoint. String-theory motivated (heterotic). No particle physics evidence.","SPECULATIVE: No empirical support. TQM gives no reason to prefer E8."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE PROBLEM: WHY SU(3) × SU(2) × U(1)?");
        sb.AppendLine();
        sb.AppendLine("  The Standard Model gauge group is:");
        sb.AppendLine("    SU(3)_color × SU(2)_weak × U(1)_hypercharge.");
        sb.AppendLine("    8 gluons + 3 weak bosons + 1 photon = 12 gauge bosons.");
        sb.AppendLine("    Rank 4. 8+3+1 = 12 generators.");
        sb.AppendLine();
        sb.AppendLine("  The Standard Model DOES NOT explain this choice.");
        sb.AppendLine("    It is ASSUMED. 'It's what we observe.'");
        sb.AppendLine("    There are infinitely many other Lie groups.");
        sb.AppendLine("    Why these three, in this combination?");
        sb.AppendLine();
        sb.AppendLine("  TQM'S CURRENT STATE:");
        sb.AppendLine("    TQM explains: topology, phase, oscillation, architecture.");
        sb.AppendLine("    TQM does NOT yet explain: the gauge group selection.");
        sb.AppendLine("    This experiment attempts the derivation.");
        sb.AppendLine();
        sb.AppendLine("  THE CENTRAL QUESTION:");
        sb.AppendLine("    Is SU(3)×SU(2)×U(1) mathematically inevitable,");
        sb.AppendLine("    stability-selected, complexity-optimal,");
        sb.AppendLine("    or merely accidental?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    U(1) has a NATURAL TQM derivation (S¹ topology).");
        sb.AppendLine("    SU(2) and SU(3) have SUGGESTIVE correspondences");
        sb.AppendLine("    (binary and tri-winding structures),");
        sb.AppendLine("    but NO complete derivation yet.");
        sb.AppendLine("    The full gauge group remains PARTIALLY external.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("U(1) DERIVATION: THE COMPLETE SUCCESS");
        sb.AppendLine();
        sb.AppendLine("  THE DERIVATION CHAIN:");
        sb.AppendLine("    1. Oscillation creates phase: θ(x,t) = ωt - kx (QG-021/026).");
        sb.AppendLine("    2. Phase lives on S¹: θ ≡ θ + 2π (QG-033).");
        sb.AppendLine("    3. S¹ has topological sectors: ∮∇θ·dl = 2πn, n ∈ Z (QG-034).");
        sb.AppendLine("    4. n is conserved (cannot change continuously).");
        sb.AppendLine("    5. Conserved integer charge → Noether symmetry.");
        sb.AppendLine("    6. The symmetry group of S¹ is U(1).");
        sb.AppendLine();
        sb.AppendLine("  THE U(1) GAUGE CONNECTION:");
        sb.AppendLine("    Local phase redefinition: θ → θ + α(x).");
        sb.AppendLine("    This is EXACTLY a U(1) gauge transformation.");
        sb.AppendLine("    Gauge connection: A_μ = ∂_μθ (phase gradient).");
        sb.AppendLine("    Photon = massless excitation of θ (n=0 phase wave).");
        sb.AppendLine();
        sb.AppendLine("  CHARGE FROM WINDING (QG-035):");
        sb.AppendLine("    Q = g·n where n = winding number, g = coupling.");
        sb.AppendLine("    Electron (n=-1): Q = -1. Positron (n=+1): Q = +1.");
        sb.AppendLine("    Anti-matter = opposite winding (QG-034).");
        sb.AppendLine();
        sb.AppendLine("  WHY U(1) IS INEVITABLE:");
        sb.AppendLine("    Phase is a CIRCLE (S¹). The circle's isometry group is U(1).");
        sb.AppendLine("    You cannot have phase without U(1). You cannot have U(1)");
        sb.AppendLine("    without a circle. They are the SAME object.");
        sb.AppendLine("    U(1) is not 'chosen' — it's the symmetry of phase itself.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: U(1) FULLY DERIVED. Classification C — COMPLETE.");
        sb.AppendLine("  This is the cleanest result of the entire gauge program.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SU(2) ANALYSIS: BINARY TOPOLOGICAL SECTORS");
        sb.AppendLine();
        sb.AppendLine("  WHAT SU(2) DOES:");
        sb.AppendLine("    Weak isospin. Doublets: (e,νe), (u,d), (μ,νμ), ...");
        sb.AppendLine("    W± and Z bosons. Parity violation. Flavor change.");
        sb.AppendLine();
        sb.AppendLine("  TQM CANDIDATE MECHANISMS:");
        sb.AppendLine();
        sb.AppendLine("  1. BINARY WINDING (n ↔ -n):");
        sb.AppendLine("     Matter/anti-matter = opposite winding (QG-034/035).");
        sb.AppendLine("     Z₂ symmetry: n → -n. The smallest non-trivial discrete group.");
        sb.AppendLine("     Isospin doublets map to binary winding PAIRS.");
        sb.AppendLine();
        sb.AppendLine("  2. SPINOR STRUCTURE (DOUBLE COVER):");
        sb.AppendLine("     SU(2) is the double cover of SO(3).");
        sb.AppendLine("     A 2π rotation gives -1 (spinors). 4π gives +1.");
        sb.AppendLine("     TQM: phase θ → θ+2πn. For spin-½, θ → θ+2π gives -1.");
        sb.AppendLine("     Spinor = double-valued phase representation.");
        sb.AppendLine();
        sb.AppendLine("  3. ORIENTATION SYMMETRY:");
        sb.AppendLine("     SU(2) ≅ S³ (unit quaternions). Rotation symmetry of 3D.");
        sb.AppendLine("     TQM: 3+1 dimensions → SO(3) rotations → SU(2) cover.");
        sb.AppendLine();
        sb.AppendLine("  WHAT'S DERIVED vs WHAT'S NOT:");
        sb.AppendLine("    DERIVED: Z₂ (n↔-n) is real. Spinor double-cover is real.");
        sb.AppendLine("    NOT DERIVED: The LIFT from Z₂ to full continuous SU(2).");
        sb.AppendLine("    Why do discrete binary sectors become continuous rotations?");
        sb.AppendLine("    This requires an independent justification.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: SU(2) has SUGGESTIVE TQM correspondences but");
        sb.AppendLine("  is NOT fully derived. Classification B — PARTIAL.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SU(3) ANALYSIS: TRI-WINDING CONFINEMENT");
        sb.AppendLine();
        sb.AppendLine("  WHAT SU(3) DOES:");
        sb.AppendLine("    Color. Triplets: (r,g,b) for quarks. Gluons (8).");
        sb.AppendLine("    Confinement: color-neutral bound states only.");
        sb.AppendLine("    Asymptotic freedom: quarks free at short distance.");
        sb.AppendLine();
        sb.AppendLine("  TQM CANDIDATE MECHANISMS:");
        sb.AppendLine();
        sb.AppendLine("  1. TRI-WINDING (n=3 CONFINEMENT):");
        sb.AppendLine("     Proton = confined n=3 winding (QG-034).");
        sb.AppendLine("     Three n=1 vortices bound by confinement.");
        sb.AppendLine("     Color = which of 3 substructures (r/g/b).");
        sb.AppendLine("     The NUMBER 3 matches: 3 colors, 3 quarks, n=3 winding.");
        sb.AppendLine();
        sb.AppendLine("  2. STABLE THREE-STATE SYSTEMS:");
        sb.AppendLine("     Three n=1 vortices in a bound state = minimum stable");
        sb.AppendLine("     multi-vortex configuration (n=2 unstable, n=3 confined).");
        sb.AppendLine("     This suggests: 3 is the MINIMAL non-trivial stable");
        sb.AppendLine("     multi-particle structure.");
        sb.AppendLine();
        sb.AppendLine("  3. COLOR SINGLET CONDITION:");
        sb.AppendLine("     Physical states must be color-neutral (singlet).");
        sb.AppendLine("     TQM: net winding number conserved → hadrons have");
        sb.AppendLine("     integer net winding. q q q = n=3 (baryon). q qbar = n=0 (meson).");
        sb.AppendLine();
        sb.AppendLine("  WHAT'S DERIVED vs WHAT'S NOT:");
        sb.AppendLine("    DERIVED: The number 3 appears in winding (n=3).");
        sb.AppendLine("    NOT DERIVED: Why SU(3) specifically (not U(3), SO(3), etc.).");
        sb.AppendLine("    NOT DERIVED: Confinement mechanism (borrowed from QCD).");
        sb.AppendLine("    NOT DERIVED: 8 gluons (from SU(3) adjoint, not from winding).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: SU(3) has the STRONGEST NUMERICAL HINT (3-ness)");
        sb.AppendLine("  but the WEAKEST derivation. The tri-winding connection is");
        sb.AppendLine("  suggestive but incomplete. Classification B — PARTIAL.");
        return sb.ToString();
    }

    static string BuildE(GaugeCandidate[] candidates)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ALTERNATIVE GROUP COMPARISON");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,-12} {2,-22} {3}","Group","Stability","Matter support","Verdict"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var c in candidates)
        {
            string v = c.Verdict.Length > 50 ? c.Verdict[..47]+"..." : c.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,-12} {2,-22} {3}", c.Group, c.StabilityRank, c.MatterSupport, v));
        }
        sb.AppendLine();
        sb.AppendLine("  KEY OBSERVATIONS:");
        sb.AppendLine();
        sb.AppendLine("  1. TOO SIMPLE FAILS:");
        sb.AppendLine("     U(1) alone: no bound states, no atoms, no matter.");
        sb.AppendLine("     U(1)×SU(2): no strong force, no nuclei.");
        sb.AppendLine("     The SM group is the MINIMUM needed for stable matter.");
        sb.AppendLine();
        sb.AppendLine("  2. TOO COMPLEX FAILS (EMPIRICALLY):");
        sb.AppendLine("     SU(5), SO(10): predict proton decay — NOT OBSERVED.");
        sb.AppendLine("     E6, E8: predict exotic particles — NOT OBSERVED.");
        sb.AppendLine("     Larger groups introduce unobserved phenomena.");
        sb.AppendLine();
        sb.AppendLine("  3. THE SM GROUP IS 'JUST RIGHT':");
        sb.AppendLine("     Minimal set that supports: confinement + weak decay + EM.");
        sb.AppendLine("     Any smaller: no stable matter.");
        sb.AppendLine("     Any larger: unobserved proton decay / exotic particles.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: SU(3)×SU(2)×U(1) is EMPIRICALLY SELECTED");
        sb.AppendLine("  (it's the smallest group that works), but this is a");
        sb.AppendLine("  SELECTION argument, not a DERIVATION.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COMPLEXITY AND STABILITY SELECTION");
        sb.AppendLine();
        sb.AppendLine("  IS SU(3)×SU(2)×U(1) COMPLEXITY-OPTIMAL?");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR 'YES':");
        sb.AppendLine("    - Smallest group that supports stable matter.");
        sb.AppendLine("    - Anthropic: only such universes produce observers.");
        sb.AppendLine("    - Rank 4 is the minimal rank for 3+1 dimensions");
        sb.AppendLine("      to support full particle physics.");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENT FOR 'NO' (HOSTILE):");
        sb.AppendLine("    - 'Smallest that works' is NOT the same as 'derived'.");
        sb.AppendLine("    - Other groups might ALSO support life (untested).");
        sb.AppendLine("    - Anthropic arguments are UNFALSIFIABLE (we can't");
        sb.AppendLine("      visit other universes to check).");
        sb.AppendLine("    - The SM group has unexplained structure:");
        sb.AppendLine("      WHY 3 generations? WHY this specific embedding?");
        sb.AppendLine();
        sb.AppendLine("  THE 1-2-3 PATTERN (MOST INTRIGUING):");
        sb.AppendLine("    rank(U(1)) = 1, rank(SU(2)) = 2, rank(SU(3)) = 3.");
        sb.AppendLine("    This maps to winding sectors n=1, 2, 3.");
        sb.AppendLine("    U(1) ↔ n=1 (single phase circle).");
        sb.AppendLine("    SU(2) ↔ n=2 (binary winding, unstable → n=±1 pair).");
        sb.AppendLine("    SU(3) ↔ n=3 (tri-winding, confined stable).");
        sb.AppendLine("    Is this a coincidence or a deep connection?");
        sb.AppendLine();
        sb.AppendLine("    HONESTLY: UNKNOWN. No mechanism links winding number");
        sb.AppendLine("    to gauge rank. The pattern could be accidental.");
        sb.AppendLine("    But it is SUGGESTIVE and deserves further study.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Selection arguments favor the SM group,");
        sb.AppendLine("  but they are SELECTION, not DERIVATION.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'U(1) DERIVATION IS TRIVIAL / TAUTOLOGICAL':");
        sb.AppendLine("     Accusation: Phase IS a circle. Circle symmetry IS U(1).");
        sb.AppendLine("     So 'deriving' U(1) from phase is just restating definitions.");
        sb.AppendLine("     Response: PARTIALLY CORRECT. The derivation shows U(1) is");
        sb.AppendLine("     NECESSARY (not optional) in any phase-based theory.");
        sb.AppendLine("     That IS progress — it removes U(1) from the 'unexplained");
        sb.AppendLine("     assumptions' list. But it doesn't derive the COUPLING g.");
        sb.AppendLine();
        sb.AppendLine("  2. 'SU(2) AND SU(3) ARE NOT DERIVED AT ALL':");
        sb.AppendLine("     CORRECT. The binary and tri-winding correspondences are");
        sb.AppendLine("     HINTS, not derivations. TQM has NOT derived SU(2) or SU(3).");
        sb.AppendLine("     This experiment FAILS to derive the full gauge group.");
        sb.AppendLine("     Honesty requires saying so clearly.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE 1-2-3 PATTERN IS NUMEROLOGY':");
        sb.AppendLine("     The fact that ranks are 1,2,3 matching winding numbers");
        sb.AppendLine("     is PROBABLY a coincidence. Many numerical coincidences");
        sb.AppendLine("     in physics are accidents. Without a mechanism, this is");
        sb.AppendLine("     numerology. CORRECT assessment.");
        sb.AppendLine();
        sb.AppendLine("  4. 'ANTHROPIC ARGUMENTS ARE UNFALSIFIABLE':");
        sb.AppendLine("     Saying 'the SM group is what it is because otherwise");
        sb.AppendLine("     we wouldn't be here' explains NOTHING testable.");
        sb.AppendLine("     It is a SELECTION effect, not a physical law.");
        sb.AppendLine("     CORRECT. TQM should not lean on this.");
        sb.AppendLine();
        sb.AppendLine("  5. THE REAL ACHIEVEMENT (MODEST):");
        sb.AppendLine("     - U(1): genuinely derived from S¹ topology.");
        sb.AppendLine("     - SU(2): binary structure identified (n↔-n).");
        sb.AppendLine("     - SU(3): tri-structure identified (n=3 confinement).");
        sb.AppendLine("     - Alternative groups: constrained by stability/matter.");
        sb.AppendLine("     - FULL derivation: NOT ACHIEVED. Remains open.");
        sb.AppendLine("     This is an HONEST partial success.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("REMAINING GAPS");
        sb.AppendLine();
        sb.AppendLine("  1. SU(2) DERIVATION INCOMPLETE:");
        sb.AppendLine("     The lift from Z₂ (n↔-n) to continuous SU(2) is unexplained.");
        sb.AppendLine("     Why continuous weak isospin rotations? Unknown.");
        sb.AppendLine();
        sb.AppendLine("  2. SU(3) DERIVATION INCOMPLETE:");
        sb.AppendLine("     The number 3 is suggested by n=3 confinement but the");
        sb.AppendLine("     full SU(3) algebra (8 gluons) is not derived.");
        sb.AppendLine("     Confinement mechanism borrowed from QCD.");
        sb.AppendLine();
        sb.AppendLine("  3. THE COMBINATION NOT DERIVED:");
        sb.AppendLine("     Why SU(3)×SU(2)×U(1) TOGETHER? Why not separately?");
        sb.AppendLine("     The tensor product structure is assumed.");
        sb.AppendLine();
        sb.AppendLine("  4. THE HYPERCHARGE EMBEDDING NOT DERIVED:");
        sb.AppendLine("     U(1)_Y has specific hypercharge assignments (Y = 2(Q-T₃)).");
        sb.AppendLine("     These specific values are NOT derived from TQM.");
        sb.AppendLine();
        sb.AppendLine("  5. COUPLING CONSTANTS NOT DERIVED:");
        sb.AppendLine("     g_s, g, g' (or equivalently α_s, α_EM, sin²θ_W) are");
        sb.AppendLine("     measured, not derived. No TQM prediction yet.");
        sb.AppendLine();
        sb.AppendLine("  6. THREE GENERATIONS NOT EXPLAINED:");
        sb.AppendLine("     The gauge group is repeated 3 times (generations).");
        sb.AppendLine("     WHY 3? Architecture (QG-028) but no derivation.");
        sb.AppendLine();
        sb.AppendLine("  BOTTOM LINE:");
        sb.AppendLine("    The gauge group selection is the LARGEST remaining gap");
        sb.AppendLine("    in the TQM program's particle physics coverage.");
        sb.AppendLine("    U(1) is done. SU(2), SU(3) are open problems.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  PARTIAL SUCCESS: U(1) DERIVED, SU(2)/SU(3) OPEN");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Gauge symmetry in TQM = symmetry of the phase structure.");
        sb.AppendLine("  Q2: U(1) DERIVED from S¹. COMPLETE. The circle IS U(1).");
        sb.AppendLine("  Q3: SU(2) — binary winding (n↔-n) and spinor structure are");
        sb.AppendLine("      real, but full SU(2) not derived. PARTIAL.");
        sb.AppendLine("  Q4: SU(3) — tri-winding (n=3) suggestive, but full SU(3) not");
        sb.AppendLine("      derived. PARTIAL.");
        sb.AppendLine("  Q5: 1-2-3 pattern (U(1),SU(2),SU(3) ranks = 1,2,3) is");
        sb.AppendLine("      INTRIGUING but possibly coincidental.");
        sb.AppendLine("  Q6: Full gauge group NOT derived from topology alone.");
        sb.AppendLine("  Q7: Larger groups (SU(5),SO(10),E6,E8) DISFAVORED by proton");
        sb.AppendLine("      decay non-observation and exotic particle absence.");
        sb.AppendLine("  Q8: Symmetry breaking NOT yet predicted before Higgs.");
        sb.AppendLine("  Q9: Selection by STABILITY (smallest group supporting matter),");
        sb.AppendLine("      not by complexity optimization.");
        sb.AppendLine("  Q10: Universes with different groups likely fail to support");
        sb.AppendLine("      stable matter (but this is anthropic, not derived).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK CORRESPONDENCE");
        sb.AppendLine("    U(1): C (complete derivation).");
        sb.AppendLine("    SU(2): B (binary structure identified, full group not derived).");
        sb.AppendLine("    SU(3): B (tri-structure identified, full group not derived).");
        sb.AppendLine("    FULL GROUP: A/B — still largely EXTERNAL.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST CONCLUSION:");
        sb.AppendLine("    TQM has NOT yet derived SU(3)×SU(2)×U(1).");
        sb.AppendLine("    It has made GENUINE PROGRESS: U(1) is fully derived,");
        sb.AppendLine("    and the 1-2-3 pattern suggests a direction.");
        sb.AppendLine("    But claiming 'logical inevitability' would be UNSUPPORTED.");
        sb.AppendLine("    The gauge group selection remains an OPEN PROBLEM.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 38 experiments.");
        return sb.ToString();
    }
}
