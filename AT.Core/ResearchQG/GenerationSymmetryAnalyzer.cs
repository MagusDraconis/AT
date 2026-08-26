using System.Globalization;

namespace AT.Core.ResearchQG;

public static class GenerationSymmetryAnalyzer
{
    public static GSAResult RunFullAnalysis()
    {
        var counts = BuildGenCounts();
        var decomp = ComputeS3Decomp();
        return new GSAResult(BuildA(),BuildB(counts),BuildC(),BuildD(decomp),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),counts,decomp);
    }

    static S3Decomp ComputeS3Decomp()
    {
        double me = 0.51099895000, mmu = 105.6583755, mtau = 1776.86;
        double se = Math.Sqrt(me), su = Math.Sqrt(mmu), st = Math.Sqrt(mtau);
        double sum = se + su + st;
        double totalMag = Math.Sqrt(me + mmu + mtau);
        double singletMag = sum / Math.Sqrt(3.0);   // projection onto (1,1,1)/sqrt3
        double doubletMag = Math.Sqrt(totalMag*totalMag - singletMag*singletMag);
        double cosTheta = singletMag / totalMag;
        double angle = Math.Acos(cosTheta) * 180.0 / Math.PI;
        string balanced = Math.Abs(singletMag - doubletMag)/totalMag < 0.001 ? "BALANCED (singlet=doublet exactly)" : "unbalanced";
        return new S3Decomp(singletMag, doubletMag, totalMag, cosTheta, angle, balanced);
    }

    static GenCount[] BuildGenCounts()
    {
        return new GenCount[]
        {
            new GenCount(1,"0","NO","NO","Possible but empty","FAILS: No mixing, no CP violation. No baryogenesis. Matter-antimatter symmetric annihilation -> empty universe."),
            new GenCount(2,"0","NO","NO","Possible but empty","FAILS: CKM 2x2 is REAL (no complex phase). No CP violation. Same annihilation problem. No matter."),
            new GenCount(3,"1","YES","YES","OBSERVED","WORKS: CKM 3x3 has 1 complex phase. CP violation possible. Baryogenesis possible. Matter survives. This IS our universe."),
            new GenCount(4,"3","YES (more)","YES","EXCLUDED","EXCLUDED: Z-width N_nu=3 (light 4th nu). Higgs production ~9x enhanced (heavy 4th). Not observed."),
            new GenCount(5,"6","YES (more)","YES","EXCLUDED","EXCLUDED: Even more excluded. No evidence."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE GENERATION PROBLEM");
        sb.AppendLine();
        sb.AppendLine("  OBSERVED: exactly 3 fermion generations (e, mu, tau; u, c, t; d, s, b).");
        sb.AppendLine("  Each repeats identical quantum numbers, differing only in mass.");
        sb.AppendLine();
        sb.AppendLine("  QG-039: generations = excitation levels of the SAME n=1 topology.");
        sb.AppendLine("  QG-039a: Koide 45° = geometric constraint on the 3 masses.");
        sb.AppendLine("  QG-045: Koide = symmetry-generated (S3), not stability.");
        sb.AppendLine();
        sb.AppendLine("  QG-046's QUESTION:");
        sb.AppendLine("    Why EXACTLY 3 generations, and why does S3 act on them?");
        sb.AppendLine("    Is S3 accidental, emergent, required, or fundamental?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    'Why 3' is NOT derived by AT (nor by the SM).");
        sb.AppendLine("    S3 is the AUTOMATIC permutation symmetry of 3 objects.");
        sb.AppendLine("    Koide's 45° IS an S3 representation geometry.");
        sb.AppendLine("    But the '3-ness' itself remains selection (anthropic).");
        return sb.ToString();
    }

    static string BuildB(GenCount[] counts)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THREE-GENERATION NECESSITY: WHAT FAILS AT N≠3");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,10} {2,10} {3,12} {4}", "N", "CKM phases", "CP viol?", "Status", "Verdict"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var c in counts)
        {
            string v = c.Verdict.Length > 60 ? c.Verdict[..57]+"..." : c.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,10} {2,10} {3,12} {4}", c.N, c.CKMPhases, c.CPViolation, c.ObservationStatus, v));
        }
        sb.AppendLine();
        sb.AppendLine("  THE CP-VIOLATION ARGUMENT:");
        sb.AppendLine("    - N=1,2: CKM matrix is real (no complex phase). No CP");
        sb.AppendLine("      violation. No baryogenesis (Sakharov conditions fail).");
        sb.AppendLine("      Matter-antimatter symmetry → annihilation → empty universe.");
        sb.AppendLine("    - N=3: CKM 3x3 has exactly 1 irreducible complex phase.");
        sb.AppendLine("      CP violation possible. Baryogenesis possible. Matter.");
        sb.AppendLine("    - N≥4: CP violation works, but EMPIRICALLY EXCLUDED");
        sb.AppendLine("      (Z-width, Higgs production).");
        sb.AppendLine();
        sb.AppendLine("  THE CONCLUSION: 3 = MINIMUM N with CP violation.");
        sb.AppendLine("    This is a SELECTION argument (anthropic), not a DERIVATION.");
        sb.AppendLine("    3 is the smallest count that permits matter to exist.");
        sb.AppendLine("    It does NOT follow from topology, oscillation, or Q.");
        sb.AppendLine();
        sb.AppendLine("  WHY AT CANNOT DERIVE '3':");
        sb.AppendLine("    The number 3 enters via the SU(3) color structure (QG-038,");
        sb.AppendLine("    itself unexplained) and via empirical Z-width. There is no");
        sb.AppendLine("    AT mechanism that forces exactly 3 excitation levels.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("S3 SYMMETRY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  S3 = the PERMUTATION GROUP of 3 objects. 6 elements.");
        sb.AppendLine();
        sb.AppendLine("  S3 IS AUTOMATIC FOR ANY 3-FOLD STRUCTURE:");
        sb.AppendLine("    If a system has 3 indistinguishable components, its");
        sb.AppendLine("    permutation symmetry group is S3. This is a MATHEMATICAL");
        sb.AppendLine("    AUTOMATISM — not an 'emergence', not an 'assumption'.");
        sb.AppendLine("    ANY 3-object system has S3 symmetry available.");
        sb.AppendLine();
        sb.AppendLine("  S3 REPRESENTATION THEORY (key to Koide):");
        sb.AppendLine("    S3 has irreducible representations:");
        sb.AppendLine("      - 1 singlet (trivial): the direction (1,1,1).");
        sb.AppendLine("      - 1 sign representation (antisymmetric).");
        sb.AppendLine("      - 1 DOUBLET (2-dimensional).");
        sb.AppendLine("    Any 3-vector decomposes into singlet + doublet.");
        sb.AppendLine();
        sb.AppendLine("  WHAT S3 ACTS ON (Q5):");
        sb.AppendLine("    S3 acts on the GENERATION INDEX (the 3 excitation levels).");
        sb.AppendLine("    It permutes e, mu, tau. The MASSES are the S3-orbits.");
        sb.AppendLine("    But S3 does NOT force the masses to be DIFFERENT — that");
        sb.AppendLine("    requires S3-BREAKING (the mass hierarchy breaks S3).");
        sb.AppendLine();
        sb.AppendLine("  KEY POINT: S3 is SYMMETRY, but the MASS HIERARCHY breaks it.");
        sb.AppendLine("    Full S3 symmetry → m_e = m_mu = m_tau (democratic, degenerate).");
        sb.AppendLine("    Observed: m_e << m_mu << m_tau → S3 is BROKEN.");
        sb.AppendLine("    The Koide 45° is the specific 'halfway' breaking pattern.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S3 is AUTOMATIC (any 3-fold structure), not derived.");
        sb.AppendLine("  The interesting physics is the S3-BREAKING pattern (Koide).");
        return sb.ToString();
    }

    static string BuildD(S3Decomp d)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE GEOMETRY = S3 DECOMPOSITION (45° = BALANCED)");
        sb.AppendLine();
        sb.AppendLine("  DECOMPOSE the amplitude vector A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau))");
        sb.AppendLine("  into S3 singlet (democratic) and S3 doublet (orthogonal) parts:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Total amplitude magnitude  |A| = {0:F3} sqrt(MeV)", d.TotalMag));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    S3 singlet component       = {0:F3} sqrt(MeV)", d.SingletMag));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    S3 doublet component       = {0:F3} sqrt(MeV)", d.DoubletMag));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    cos(theta)                 = {0:F6}", d.CosTheta));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    theta                      = {0:F3} degrees", d.AngleDeg));
        sb.AppendLine("    " + d.Balanced);
        sb.AppendLine();
        sb.AppendLine("  THE REMARKABLE RESULT:");
        sb.AppendLine("    The S3 singlet and doublet components are EXACTLY EQUAL.");
        sb.AppendLine("    |singlet| = |doublet| = 30.68 sqrt(MeV).");
        sb.AppendLine("    This equality IS the 45° angle (cos²θ = 1/2).");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL MEANING:");
        sb.AppendLine("    The lepton amplitude vector sits EXACTLY HALFWAY between:");
        sb.AppendLine("      - the S3-democratic direction (1,1,1): m_e=m_mu=m_tau.");
        sb.AppendLine("      - the S3-doublet plane: maximal hierarchy.");
        sb.AppendLine("    The observed hierarchy is 'balanced' — S3 broken by");
        sb.AppendLine("    exactly the amount that puts the vector at 45°.");
        sb.AppendLine();
        sb.AppendLine("  WHY 45° IS SPECIAL (Q6):");
        sb.AppendLine("    45° = the angle where democratic (singlet) and hierarchical");
        sb.AppendLine("    (doublet) contributions are EQUAL. It is the 'midpoint' of");
        sb.AppendLine("    the S3 representation. Why the midpoint? Not derived.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide = the S3 decomposition is balanced (45°).");
        sb.AppendLine("  This is a CLEAN geometric statement, but the 'balance'");
        sb.AppendLine("  (why equal singlet/doublet) is NOT derived.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ARCHITECTURE EXCITATION MODEL");
        sb.AppendLine();
        sb.AppendLine("  QG-039: e, mu, tau = excitation levels of the n=1 vortex.");
        sb.AppendLine();
        sb.AppendLine("  WHAT KIND OF EXCITATIONS?");
        sb.AppendLine("    1. HARMONIC MODES: e, mu, tau as 1st, 2nd, 3rd harmonics.");
        sb.AppendLine("       But harmonic masses would be m, 2m, 3m (or m, 4m, 9m) —");
        sb.AppendLine("       NOT observed (m_e : m_mu : m_tau = 1 : 207 : 3478). REJECTED.");
        sb.AppendLine("    2. STANDING MODES: discrete resonances of the vortex core.");
        sb.AppendLine("       Frequencies from a boundary-value problem. PLAUSIBLE but");
        sb.AppendLine("       the specific frequencies (207, 3478) not derived.");
        sb.AppendLine("    3. RESONANT BANDS: frequency bands where the architecture");
        sb.AppendLine("       self-stabilizes. Each band = one generation. SPECULATIVE.");
        sb.AppendLine("    4. ATTRACTOR BRANCHES: multiple stable attractors of the");
        sb.AppendLine("       same n=1 architecture. Each branch = one generation.");
        sb.AppendLine("       This is the MOST AT-CONSISTENT: generations = multiple");
        sb.AppendLine("       attractor branches of one topological object.");
        sb.AppendLine();
        sb.AppendLine("  THE ATTRACTOR-BRANCH PICTURE (preferred):");
        sb.AppendLine("    The n=1 vortex architecture has (at least) 3 stable");
        sb.AppendLine("    attractor branches in frequency space (QG-020). Each");
        sb.AppendLine("    branch is a generation. The electron sits in the lowest");
        sb.AppendLine("    branch; muon in the next; tau in the next.");
        sb.AppendLine("    WHY 3 branches? NOT derived. Could be more (excluded)");
        sb.AppendLine("    or fewer (no CP violation).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Generations as attractor branches is CONSISTENT");
        sb.AppendLine("  with AT but the NUMBER of branches (3) is not derived.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NEUTRINO CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("  Do nu_e, nu_mu, nu_tau inherit the SAME symmetry structure?");
        sb.AppendLine();
        sb.AppendLine("  SIMILARITIES:");
        sb.AppendLine("    - Same 3-generation count (LEP: N_nu = 3).");
        sb.AppendLine("    - Same S3 permutation structure (3 flavors).");
        sb.AppendLine("    - Oscillations require 3 mixing angles + 1 CP phase");
        sb.AppendLine("      (PMNS matrix, same CKM-like structure).");
        sb.AppendLine();
        sb.AppendLine("  DIFFERENCES:");
        sb.AppendLine("    - Masses: neutrinos ~10^6 lighter than electrons.");
        sb.AppendLine("    - Hierarchy: neutrino mass splittings are TINY (meV),");
        sb.AppendLine("      NOT hierarchical like charged leptons (10^6 range).");
        sb.AppendLine("    - Ordering: normal vs inverted ordering still unknown.");
        sb.AppendLine();
        sb.AppendLine("  DOES A NEUTRINO KOIDE HOLD?");
        sb.AppendLine("    If nu_e, nu_mu, nu_tau obeyed the SAME 45° geometry,");
        sb.AppendLine("    their amplitude vector would also sit at 45° to (1,1,1).");
        sb.AppendLine("    Current mass bounds: this is UNTESTED (masses too small).");
        sb.AppendLine("    If the ordering is INVERTED, a Koide-like relation could");
        sb.AppendLine("    still hold (the relation is permutation-invariant).");
        sb.AppendLine("    Status: UNKNOWN. Requires neutrino mass measurement.");
        sb.AppendLine();
        sb.AppendLine("  A FALSIFIABLE PREDICTION:");
        sb.AppendLine("    If S3 governs ALL fermion generations, then the NEUTRINO");
        sb.AppendLine("    amplitudes should ALSO satisfy a Koide-like relation");
        sb.AppendLine("    (Q = 2/3), once masses are measured. This is TESTABLE");
        sb.AppendLine("    with future neutrino experiments (KATRIN, cosmology).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Neutrinos likely inherit S3 (same 3-fold structure),");
        sb.AppendLine("  but the Koide relation for neutrinos is UNVERIFIED. This is");
        sb.AppendLine("  a concrete testable prediction of the S3 hypothesis.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FOURTH GENERATION STRESS TEST");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: Add a 4th generation. What survives?");
        sb.AppendLine();
        sb.AppendLine("  1. KOIDE GEOMETRY:");
        sb.AppendLine("     The 45° is specific to 3 generations (QG-039a: any 4th");
        sb.AppendLine("     mass breaks Q = 2/3). A 4th generation would need a");
        sb.AppendLine("     DIFFERENT (4-mass) relation. The 3-generation Koide DIES.");
        sb.AppendLine();
        sb.AppendLine("  2. S3 SYMMETRY:");
        sb.AppendLine("     S3 = permutation of 3. With 4 generations, the symmetry");
        sb.AppendLine("     group would be S4 (24 elements), not S3. The 45° geometry");
        sb.AppendLine("     (S3 singlet/doublet) has NO 4-generation analogue.");
        sb.AppendLine("     S3 DIES (replaced by S4, which has different geometry).");
        sb.AppendLine();
        sb.AppendLine("  3. STABILITY / PHENOMENOLOGY:");
        sb.AppendLine("     Z-width: N_nu = 2.984 ± 0.008 → 4th LIGHT neutrino EXCLUDED.");
        sb.AppendLine("     Higgs production: gg→H enhanced ~9x with 4th gen → EXCLUDED.");
        sb.AppendLine("     A 4th generation with HEAVY neutrino survives these, but");
        sb.AppendLine("     is strongly disfavored by global EW fits.");
        sb.AppendLine();
        sb.AppendLine("  4. OBSERVATIONS:");
        sb.AppendLine("     NO 4th generation observed. LHC, LEP, precision EW all");
        sb.AppendLine("     consistent with exactly 3.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: A 4th generation DESTROYS the Koide geometry,");
        sb.AppendLine("  the S3 symmetry, and is excluded by observation.");
        sb.AppendLine("  The 3-generation structure (Koide + S3 + phenomenology)");
        sb.AppendLine("  is COHERENT and mutually reinforcing.");
        sb.AppendLine();
        sb.AppendLine("  BUT: this does NOT DERIVE 3. It shows 3 is CONSISTENT,");
        sb.AppendLine("  not NECESSARY. (A universe with 4 generations and no");
        sb.AppendLine("  Koide is conceivable — just not observed.)");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'S3 IS TRIVIAL, NOT EMERGENT':");
        sb.AppendLine("     CORRECT. S3 is the permutation group of 3 objects — it is");
        sb.AppendLine("     AUTOMATIC for any 3-fold structure. Claiming S3 'emerges'");
        sb.AppendLine("     from AT is misleading. It's just math. The REAL content");
        sb.AppendLine("     is the S3-BREAKING pattern (Koide 45°), which IS specific.");
        sb.AppendLine();
        sb.AppendLine("  2. 'THREE GENERATIONS IS NOT DERIVED':");
        sb.AppendLine("     CORRECT. The '3' comes from CP violation (selection) +");
        sb.AppendLine("     empirical exclusion (LEP, LHC). AT does NOT derive '3'.");
        sb.AppendLine("     Claiming 'mathematically required' would be FALSE.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE 45° IS UNEXPLAINED':");
        sb.AppendLine("     CORRECT. The balanced S3 decomposition (singlet = doublet)");
        sb.AppendLine("     is a clean geometric RESTATEMENT of Koide, but WHY balanced");
        sb.AppendLine("     (why 45° not 30° or 60°) is NOT derived. Restating ≠ deriving.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ACHIEVED':");
        sb.AppendLine("     - Generations = excitation levels / attractor branches");
        sb.AppendLine("       (QG-039, consistent).");
        sb.AppendLine("     - Koide 45° = balanced S3 decomposition (concrete, new).");
        sb.AppendLine("     - The falsifiable neutrino-Koide prediction (new, testable).");
        sb.AppendLine("     - 3 = minimum for CP violation (selection, not derivation).");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     S3 is RELEVANT (it restates Koide geometrically) but");
        sb.AppendLine("     does NOT derive 3 generations or the 45° angle.");
        sb.AppendLine("     Honest classification: B (weak correspondence).");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  S3 RESTATES KOIDE; DOES NOT DERIVE 3 GENERATIONS");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Why 3? NOT derived. Selection: 3 = minimum for CP violation");
        sb.AppendLine("      (baryogenesis); 4+ excluded (Z-width, Higgs).");
        sb.AppendLine("  Q2: The generation index is carried by the EXCITATION LEVEL");
        sb.AppendLine("      (frequency band) of the n=1 vortex (QG-039).");
        sb.AppendLine("  Q3: YES — generations arise from excitation bands / attractor");
        sb.AppendLine("      branches of one architecture. Number of branches not derived.");
        sb.AppendLine("  Q4: Koide lives in 3D because there are 3 generation amplitudes");
        sb.AppendLine("      (the 3D mass space is the generation space).");
        sb.AppendLine("  Q5: S3 acts on the generation INDEX (permutes e/mu/tau),");
        sb.AppendLine("      breaking to produce the mass hierarchy.");
        sb.AppendLine("  Q6: 45° = balanced S3 singlet/doublet decomposition. Why balanced?");
        sb.AppendLine("      Not derived.");
        sb.AppendLine("  Q7: S3 emerges from phase-permutation symmetry: ANY 3-fold");
        sb.AppendLine("      structure has S3. Automatic, not derived.");
        sb.AppendLine("  Q8: A 4th generation BREAKS Koide, replaces S3 with S4, and");
        sb.AppendLine("      is excluded by observation.");
        sb.AppendLine("  Q9: Neutrinos likely inherit S3 (same 3 flavors), but a");
        sb.AppendLine("      neutrino-Koide is UNVERIFIED (testable prediction).");
        sb.AppendLine("  Q10: S3 NOT derived from deeper AT structures. It is the");
        sb.AppendLine("      automatic permutation symmetry of 3 objects.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("    S3 is the AUTOMATIC symmetry of 3 generations (trivial).");
        sb.AppendLine("    The Koide 45° IS the S3-balanced-decomposition geometry");
        sb.AppendLine("    (a genuine, concrete result — QG-039a restated).");
        sb.AppendLine("    BUT: '3 generations' is NOT derived (selection), and");
        sb.AppendLine("    '45° balance' is NOT derived (restatement).");
        sb.AppendLine();
        sb.AppendLine("    GENUINE NEW PREDICTION (testable):");
        sb.AppendLine("    If S3 governs generations, the NEUTRINO amplitudes should");
        sb.AppendLine("    ALSO satisfy Q = 2/3 (a neutrino-Koide). Falsifiable with");
        sb.AppendLine("    future neutrino mass measurements.");
        sb.AppendLine();
        sb.AppendLine("    The generation structure is COHERENT (Koide + S3 + CP");
        sb.AppendLine("    violation + phenomenology mutually reinforce), but the");
        sb.AppendLine("    number 3 remains an EMPIRICAL INPUT.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 46 experiments.");
        return sb.ToString();
    }
}
