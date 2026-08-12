using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class GenerationDimensionAnalyzer
{
    public static GDRResult RunFullAnalysis()
    {
        var dims = BuildDimensions();
        var mechs = BuildMechanisms();
        return new GDRResult(BuildA(),BuildB(dims),BuildC(),BuildD(dims),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),dims,mechs);
    }

    static GenDimension[] BuildDimensions()
    {
        return new GenDimension[]
        {
            new GenDimension(1,0,0,"Trivial","NO","Possible","FAILS: 1 generation, no mixing, no CP, no atoms."),
            new GenDimension(2,1,0,"S2 = Z2","NO","Possible","FAILS: CKM 2x2 REAL (0 phases). No CP violation. Empty universe."),
            new GenDimension(3,3,1,"S3 (non-abelian)","YES","OBSERVED","WORKS: minimum N with 1 CP phase. Matter survives."),
            new GenDimension(4,6,3,"S4","YES (more)","EXCLUDED","EXCLUDED: Z-width N_nu=3 (light 4th nu); Higgs production ~9x (heavy 4th)."),
            new GenDimension(5,10,6,"S5","YES (more)","EXCLUDED","EXCLUDED: no evidence. More phases than needed."),
        };
    }

    static DimMechanism[] BuildMechanisms()
    {
        return new DimMechanism[]
        {
            new DimMechanism("CP-violation minimality","PARTIAL (gives N>=3, not N=3)","LOWER BOUND: N>=3","3 = minimum N with a complex CKM phase ((N-1)(N-2)/2 >= 1 needs N>=3)."),
            new DimMechanism("Baryogenesis (Sakharov)","PARTIAL (gives N>=3)","LOWER BOUND: N>=3","Matter-antimatter asymmetry needs CP violation, which needs N>=3."),
            new DimMechanism("Z-width (LEP)","PARTIAL (gives N<=3 light nu)","UPPER BOUND: N_light_nu <= 3","N_nu = 2.984±0.008. 4th LIGHT neutrino excluded."),
            new DimMechanism("Higgs production (LHC)","PARTIAL (excludes N>=4)","UPPER BOUND: N <= 3","gg->H enhanced ~9x with 4th heavy generation. Consistent with 3."),
            new DimMechanism("Anthropic + empirical COMBINED","YES (unique value)","N=3 uniquely","N>=3 (anthropic) AND N<=3 (empirical) -> N=3. Selection, not derivation."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE DIMENSION PROBLEM");
        sb.AppendLine();
        sb.AppendLine("  QG-052 established: a generation space G is REQUIRED.");
        sb.AppendLine("  But WHY is dim(G) = 3 (not 1, 2, 4, N)?");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION:");
        sb.AppendLine("    Is dim(G)=3 selected, emergent, required, or contingent?");
        sb.AppendLine();
        sb.AppendLine("  THE CANDIDATE ANSWERS:");
        sb.AppendLine("    1. CP violation minimality: 3 = minimum for a complex CKM phase.");
        sb.AppendLine("    2. Baryogenesis: matter-antimatter asymmetry needs CP (N>=3).");
        sb.AppendLine("    3. Empirical bounds: N<=3 (Z-width, Higgs production).");
        sb.AppendLine("    4. A combination of all three → N=3 uniquely.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    N=3 is the UNIQUE intersection of a lower bound (N>=3 for");
        sb.AppendLine("    CP violation) and an upper bound (N<=3 by observation).");
        sb.AppendLine("    This is SELECTION, not DERIVATION. No single principle");
        sb.AppendLine("    forces N=3 from first principles.");
        return sb.ToString();
    }

    static string BuildB(GenDimension[] dims)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DIMENSION COUNTING: CP PHASES AND MIXING ANGLES");
        sb.AppendLine();
        sb.AppendLine("  For N generations, the NxN unitary mixing matrix has:");
        sb.AppendLine("    - N(N-1)/2 mixing angles.");
        sb.AppendLine("    - (N-1)(N-2)/2 irreducible CP-violating phases.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,10} {2,10} {3,-14} {4}", "N", "Angles", "CP phases", "Symmetry", "Verdict"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var d in dims)
        {
            string v = d.Verdict.Length > 50 ? d.Verdict[..47]+"..." : d.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,10} {2,10} {3,-14} {4}", d.N, d.MixingAngles, d.CPPhases, d.SymmetryGroup, v));
        }
        sb.AppendLine();
        sb.AppendLine("  THE KEY NUMBER: CP PHASES = (N-1)(N-2)/2.");
        sb.AppendLine("    - N=1,2: 0 phases (mixing matrix is REAL). No CP violation.");
        sb.AppendLine("    - N=3: 1 phase (the MINIMAL non-trivial CP violation).");
        sb.AppendLine("    - N=4,5: 3, 6 phases (MORE CP violation than needed).");
        sb.AppendLine();
        sb.AppendLine("  SO N=3 IS THE MINIMAL CP-VIOLATING DIMENSION.");
        sb.AppendLine("    This is a MATHEMATICAL fact, not a TQM derivation.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CP VIOLATION MINIMALITY: THE LOWER BOUND");
        sb.AppendLine();
        sb.AppendLine("  WHY CP VIOLATION IS NEEDED (baryogenesis):");
        sb.AppendLine("    Sakharov (1967): matter-antimatter asymmetry requires");
        sb.AppendLine("    (1) baryon number violation,");
        sb.AppendLine("    (2) C and CP violation,");
        sb.AppendLine("    (3) departure from thermal equilibrium.");
        sb.AppendLine("    Without CP violation, matter and antimatter are produced");
        sb.AppendLine("    equally and annihilate → empty universe.");
        sb.AppendLine();
        sb.AppendLine("  CP VIOLATION REQUIRES N >= 3:");
        sb.AppendLine("    - N=1,2: mixing matrix is real (no complex phase).");
        sb.AppendLine("      No CP violation. No baryogenesis. No matter.");
        sb.AppendLine("    - N=3: one complex phase (Kobayashi-Maskawa, 1973).");
        sb.AppendLine("      CP violation possible. Matter possible.");
        sb.AppendLine();
        sb.AppendLine("  THE KOBayashi-Maskawa INSIGHT (1973, Nobel 2008):");
        sb.AppendLine("    Kobayashi and Maskawa predicted a THIRD generation");
        sb.AppendLine("    (before it was observed) BECAUSE 3 is the minimum for");
        sb.AppendLine("    CP violation. This is a genuine prediction that N>=3.");
        sb.AppendLine("    But it predicts N>=3, NOT N=3.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: CP violation gives a LOWER BOUND (N>=3),");
        sb.AppendLine("  not the exact value (N=3). It is NECESSARY but not");
        sb.AppendLine("  SUFFICIENT to pin the dimension.");
        return sb.ToString();
    }

    static string BuildD(GenDimension[] dims)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HIGHER-DIMENSION STRESS TEST: WHY NOT N=4, 5?");
        sb.AppendLine();
        sb.AppendLine("  N=4 (4 generations):");
        sb.AppendLine("    - CP violation: YES (3 phases). Baryogenesis: possible.");
        sb.AppendLine("    - But the 4th neutrino: must be HEAVY (>45 GeV) to evade");
        sb.AppendLine("      Z-width (N_light_nu = 2.984 ± 0.008).");
        sb.AppendLine("    - Higgs production: gg→H loop amplitude enhanced ~9x with");
        sb.AppendLine("      a 4th heavy quark generation. LHC (2012+) measured the");
        sb.AppendLine("      Higgs rate consistent with 3 generations. EXCLUDED.");
        sb.AppendLine("    - Electroweak precision (S, T parameters): disfavored.");
        sb.AppendLine();
        sb.AppendLine("  N=5: even more excluded (no evidence, more phases than needed).");
        sb.AppendLine();
        sb.AppendLine("  THE KEY DISTINCTION:");
        sb.AppendLine("    N=4,5 are EXCLUDED EMPIRICALLY (Z-width, Higgs), NOT");
        sb.AppendLine("    THEORETICALLY. There is no first-principles reason");
        sb.AppendLine("    forbidding 4+ generations — they are just not observed.");
        sb.AppendLine("    (A 4th generation with a very heavy neutrino and quarks");
        sb.AppendLine("    is not LOGICALLY impossible, only phenomenologically");
        sb.AppendLine("    ruled out by current data.)");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The upper bound (N<=3) is EMPIRICAL, not derived.");
        sb.AppendLine("  No TQM principle forbids N>3. It is observation, not ontology.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION GEOMETRY: WHAT CHANGES WITH N");
        sb.AppendLine();
        sb.AppendLine("  The generation space G's geometry depends on its dimension:");
        sb.AppendLine();
        sb.AppendLine("    N=2: S2 (trivial). 2D space. No Koide-3 (that needs 3).");
        sb.AppendLine("    N=3: S3. 3D space. Koide 45° (balanced singlet/doublet).");
        sb.AppendLine("    N=4: S4. 4D space. No 45° (different decomposition).");
        sb.AppendLine("    N=5: S5. 5D space. No 45°.");
        sb.AppendLine();
        sb.AppendLine("  THE KOIDE 45° IS SPECIFIC TO N=3:");
        sb.AppendLine("    The balanced S3 decomposition (singlet = doublet, 45°)");
        sb.AppendLine("    exists ONLY in 3D. It is a 3-dimensional geometric fact.");
        sb.AppendLine("    But this does NOT select N=3 — it just says that IF N=3,");
        sb.AppendLine("    then the 45° geometry is available.");
        sb.AppendLine();
        sb.AppendLine("  IS THERE A PREFERRED DIMENSION FROM GEOMETRY ALONE?");
        sb.AppendLine("    NO. S2, S3, S4, S5 are all valid symmetry groups. None is");
        sb.AppendLine("    'more natural' than the others from pure geometry.");
        sb.AppendLine("    The preference for N=3 comes from CP violation (physics),");
        sb.AppendLine("    not from geometry (mathematics).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Generation geometry gives NO dimensional preference.");
        sb.AppendLine("  N=3 is physics-selected (CP violation), not geometry-selected.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE DIMENSION AUDIT: ASSUME ARBITRARY N");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: reproduce observations for ANY dimension N.");
        sb.AppendLine();
        sb.AppendLine("  RESULT: THE OBSERVATIONS CONSTRAIN N TO A UNIQUE VALUE.");
        sb.AppendLine();
        sb.AppendLine("  LOWER BOUND (anthropic, physics):");
        sb.AppendLine("    N < 3 → no CP violation → no baryogenesis → no matter.");
        sb.AppendLine("    A universe with N=1 or 2 generations has no observers.");
        sb.AppendLine("    (This is the Kobayashi-Maskawa anthropic argument.)");
        sb.AppendLine();
        sb.AppendLine("  UPPER BOUND (empirical, observation):");
        sb.AppendLine("    N > 3 → excluded by Z-width (light neutrinos) and");
        sb.AppendLine("    Higgs production (heavy generations).");
        sb.AppendLine("    (This is LEP + LHC measurement.)");
        sb.AppendLine();
        sb.AppendLine("  THE UNIQUE INTERSECTION: N = 3.");
        sb.AppendLine("    No other dimension satisfies both bounds.");
        sb.AppendLine();
        sb.AppendLine("  BUT THIS IS SELECTION, NOT DERIVATION:");
        sb.AppendLine("    - The lower bound is ANTHROPIC (N<3 has no observers).");
        sb.AppendLine("    - The upper bound is EMPIRICAL (we measure N=3).");
        sb.AppendLine("    - Neither is a first-principles derivation of N=3.");
        sb.AppendLine("    A universe with N=4 (heavy 4th generation) is conceivable");
        sb.AppendLine("    in principle — it just isn't ours.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: N=3 is STRONGLY SELECTED (unique), but the");
        sb.AppendLine("  selection is anthropic + empirical, not ontological.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE ANTHROPIC LOWER BOUND IS UNFALSIFIABLE':");
        sb.AppendLine("     PARTIALLY CORRECT. 'N<3 has no observers' cannot be");
        sb.AppendLine("     directly tested. But the Kobayashi-Maskawa argument");
        sb.AppendLine("     (N>=3 for CP violation) is a MATHEMATICAL fact, not");
        sb.AppendLine("     just anthropic speculation. The CP-violation minimality");
        sb.AppendLine("     is solid; the 'no observers' is the (unfalsifiable)");
        sb.AppendLine("     anthropic wrapper.");
        sb.AppendLine();
        sb.AppendLine("  2. 'THE UPPER BOUND IS EMPIRICAL, NOT THEORETICAL':");
        sb.AppendLine("     CORRECT. Z-width and Higgs production EXCLUDE N>3, but");
        sb.AppendLine("     they don't DERIVE N=3. A 4th generation is not logically");
        sb.AppendLine("     impossible — just not observed.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE COMBINATION IS SELECTION, NOT DERIVATION':");
        sb.AppendLine("     CORRECT. N>=3 (anthropic) + N<=3 (empirical) = N=3. This");
        sb.AppendLine("     is a UNIQUE intersection, but it's SELECTION. No single");
        sb.AppendLine("     principle derives N=3 from first principles.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ESTABLISHED':");
        sb.AppendLine("     - CP phases = (N-1)(N-2)/2: N=3 is minimal CP dimension.");
        sb.AppendLine("     - N=3 is the unique intersection of bounds.");
        sb.AppendLine("     - No geometric/theological principle prefers N=3.");
        sb.AppendLine("     - The Koide 45° is specific to N=3 (S3), but doesn't select it.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     dim(G)=3 is STRONGLY SELECTED (unique value) but NOT");
        sb.AppendLine("     DERIVED. Classification: C (strong selection), not D");
        sb.AppendLine("     (derivation). The 'why 3' is answered by selection,");
        sb.AppendLine("     not by ontology.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. dim(G)=3 IS SELECTED, NOT DERIVED:");
        sb.AppendLine("    TQM cannot derive N=3 from Q, oscillation, or phase.");
        sb.AppendLine("    The dimension is fixed by the combination:");
        sb.AppendLine("      N>=3 (CP violation, anthropic) + N<=3 (observation).");
        sb.AppendLine("    This is SELECTION, not ontology.");
        sb.AppendLine();
        sb.AppendLine("  2. THE SELECTION IS STRONG (UNIQUE VALUE):");
        sb.AppendLine("    Unlike the coupling constants (which have a WIDE allowed");
        sb.AppendLine("    range, QG-041), the generation dimension has a UNIQUE");
        sb.AppendLine("    allowed value (N=3). This is a STRONGER selection.");
        sb.AppendLine();
        sb.AppendLine("  3. THE KOBAYASHI-MASKAWA PRECEDENT IS REASSURING:");
        sb.AppendLine("    KM (1973) PREDICTED a 3rd generation (N>=3) from CP");
        sb.AppendLine("    violation BEFORE it was observed. This shows that");
        sb.AppendLine("    selection arguments CAN be predictive. The TQM/selection");
        sb.AppendLine("    explanation of 'why 3' is of the SAME type.");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT REMAINS OPEN:");
        sb.AppendLine("    - WHY the specific Yukawa eigenvalues (masses).");
        sb.AppendLine("    - WHY the Koide 45° (within N=3, the balance is unexplained).");
        sb.AppendLine("    - Whether N=3 has a deeper (non-anthropic) origin.");
        sb.AppendLine();
        sb.AppendLine("  5. THE HONEST POSITION:");
        sb.AppendLine("    dim(G)=3 is the one quantity where the 'selection' answer");
        sb.AppendLine("    is UNIQUELY satisfactory (only N=3 works). For couplings");
        sb.AppendLine("    (QG-041) the selection is weak (wide band); for dimension");
        sb.AppendLine("    it is strong (unique). But it is still selection.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  dim(G)=3 IS STRONGLY SELECTED, NOT DERIVED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: dim(G) is fixed by CP-violation minimality (lower) and");
        sb.AppendLine("      observation (upper). Nothing derives it from ontology.");
        sb.AppendLine("  Q2: dim(G)=3 does NOT emerge from topology/symmetry/stability/");
        sb.AppendLine("      actualization. It is selected (see Q1).");
        sb.AppendLine("  Q3: 1D/2D insufficient: no CP violation → no baryogenesis");
        sb.AppendLine("      → no matter (empty universe).");
        sb.AppendLine("  Q4: 4D/5D not realized: empirically excluded (Z-width, Higgs).");
        sb.AppendLine("      Not theoretically impossible.");
        sb.AppendLine("  Q5: The CKM CP phase is the REASON N>=3 is required, but it");
        sb.AppendLine("      does not select N=3 (N=4 also has a phase).");
        sb.AppendLine("  Q6: Baryogenesis selects N>=3 (lower bound), not N=3 exactly.");
        sb.AppendLine("  Q7: YES — G has geometry (S3, Koide 45°), but geometry gives");
        sb.AppendLine("      no dimensional preference.");
        sb.AppendLine("  Q8: S3 exists only for N=3. For N=4 it is S4, etc. S3 is");
        sb.AppendLine("      specific to 3 dimensions.");
        sb.AppendLine("  Q9: No stability penalty for N>3 (only phenomenological exclusion).");
        sb.AppendLine("  Q10: Random Actualization could select among dimensions, but");
        sb.AppendLine("      the anthropic + empirical bounds already pin N=3.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C — STRONG SELECTION");
        sb.AppendLine();
        sb.AppendLine("    N=3 is the UNIQUE value satisfying:");
        sb.AppendLine("      N >= 3  (CP violation, baryogenesis, anthropic)");
        sb.AppendLine("      N <= 3  (Z-width, Higgs production, empirical)");
        sb.AppendLine("    No other dimension works. This is STRONG selection.");
        sb.AppendLine();
        sb.AppendLine("    BUT it is SELECTION, not DERIVATION (D would require a");
        sb.AppendLine("    first-principles mechanism forcing N=3, which does not");
        sb.AppendLine("    exist). The 'why 3' is answered by selection,");
        sb.AppendLine("    not by ontology.");
        sb.AppendLine();
        sb.AppendLine("    THE DEEPER STORY:");
        sb.AppendLine("    - dim(G)=3: STRONG selection (unique value).");
        sb.AppendLine("    - Couplings (QG-041): WEAK selection (wide band).");
        sb.AppendLine("    - 45° (QG-047): NO selection found yet.");
        sb.AppendLine("    Three different kinds of 'unexplained number' — three");
        sb.AppendLine("    different depths of mystery.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 53 experiments.");
        return sb.ToString();
    }
}
