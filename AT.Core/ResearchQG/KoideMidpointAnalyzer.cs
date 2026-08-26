using System.Globalization;

namespace AT.Core.ResearchQG;

public static class KoideMidpointAnalyzer
{
    public static KMPResult RunFullAnalysis()
    {
        var facts = BuildFacts();
        var analogies = BuildAnalogies();
        return new KMPResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(analogies),BuildF(),BuildG(),BuildH(),BuildI(),facts,analogies);
    }

    static MidpointFact[] BuildFacts()
    {
        return new MidpointFact[]
        {
            new MidpointFact("Q = sum p_i^2 at Koide","2/3 = 0.6667","The participation ratio is EXACTLY 2/3 (QG-057)."),
            new MidpointFact("Midpoint of [1/3, 1]","(1/3 + 1)/2 = 2/3","2/3 is the ARITHMETIC midpoint of the allowed range."),
            new MidpointFact("N_eff = 1/Q","3/2 = 1.5","Effective generations = 1.5 (halfway between 1 and 2)."),
            new MidpointFact("Equivalence to 45 deg","cos^2(theta) = 1/2","Q=2/3 ⟺ theta=45 deg ⟺ singlet=doublet. SAME fact."),
            new MidpointFact("N-dependence","midpoint(N)=(N+1)/(2N)","2/3 is the midpoint FOR N=3. For N=2: 3/4; N=4: 5/8. NOT universal."),
        };
    }

    static CrossSystem[] BuildAnalogies()
    {
        return new CrossSystem[]
        {
            new CrossSystem("Anderson localization","Inverse participation ratio (IPR)","Q = sum p_i^2 over sites","IPR midpoint = delocalized/localized boundary","REAL: same formula, but the 'midpoint' is N-dependent, not 2/3."),
            new CrossSystem("Ecology (Simpson index)","Simpson diversity = sum p_i^2","p_i = species abundances","Diversity midpoint","REAL: same formula. No universal '2/3' — depends on species count."),
            new CrossSystem("Political science (effective parties)","Laakso-Taagepera: N = 1/sum p_i^2","p_i = party vote shares","Effective parties midpoint","REAL: same formula. N=1.5 would mean '1.5 effective parties'."),
            new CrossSystem("Quantum entanglement","Schmidt number = 1/sum lambda_i^2","lambda_i = Schmidt coefficients","Halfway between separable and maximally entangled","REAL: same formula. Schmidt number 1.5 = '1.5 entangled modes'."),
            new CrossSystem("AT generation space (Koide)","Q = sum p_i^2 = 2/3","p_i = amplitude fractions","N_eff = 1.5 effective generations","The SAME participation-ratio structure. 2/3 is N=3-specific."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE MIDPOINT OBSERVATION");
        sb.AppendLine();
        sb.AppendLine("  QG-057: Q = sum p_i^2 = 2/3, where p_i = sqrt(m_i)/sum sqrt(m).");
        sb.AppendLine();
        sb.AppendLine("  THE STRIKING FACT:");
        sb.AppendLine("    - Uniform (democratic) state: Q = 1/3 (p_i = 1/3 each).");
        sb.AppendLine("    - Concentrated (hierarchical): Q = 1 (one p_i = 1).");
        sb.AppendLine("    - Koide: Q = 2/3 = EXACTLY the arithmetic midpoint:");
        sb.AppendLine("      2/3 = (1/3 + 1)/2.");
        sb.AppendLine();
        sb.AppendLine("  EQUIVALENT STATEMENTS (all the SAME fact):");
        sb.AppendLine("    Q = 2/3  ⟺  N_eff = 3/2  ⟺  cos^2(theta) = 1/2");
        sb.AppendLine("    ⟺  theta = 45°  ⟺  singlet = doublet (balanced S3).");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION:");
        sb.AppendLine("    Is the midpoint a PRINCIPLE or a NUMEROLOGY?");
        sb.AppendLine("    (Spoiler: it is the SAME balance as the 45° — a");
        sb.AppendLine("    restatement, not a new mechanism.)");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PARTICIPATION-RATIO ANALYSIS: THE N-DEPENDENCE");
        sb.AppendLine();
        sb.AppendLine("  For N generations, Q ∈ [1/N (uniform), 1 (concentrated)].");
        sb.AppendLine("  The midpoint is (1/N + 1)/2 = (N+1)/(2N).");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL OBSERVATION:");
        sb.AppendLine("    - N=2: midpoint = 3/4.");
        sb.AppendLine("    - N=3: midpoint = 2/3 (Koide).");
        sb.AppendLine("    - N=4: midpoint = 5/8.");
        sb.AppendLine("    - N=5: midpoint = 3/5.");
        sb.AppendLine("    The '2/3' is the midpoint FOR N=3 SPECIFICALLY.");
        sb.AppendLine("    It is NOT a universal constant — it depends on N.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS:");
        sb.AppendLine("    If '2/3' were a fundamental principle, it would be");
        sb.AppendLine("    N-INDEPENDENT. It is NOT — it is (N+1)/(2N) evaluated");
        sb.AppendLine("    at N=3. So the 'midpoint' is DERIVATIVE of N=3,");
        sb.AppendLine("    which is itself SELECTED (QG-053), not derived.");
        sb.AppendLine();
        sb.AppendLine("  SO THE MIDPOINT IS NOT A NEW PRINCIPLE:");
        sb.AppendLine("    It is the combination of (a) the N=3 selection and");
        sb.AppendLine("    (b) the balance condition (45°). Both are already known.");
        sb.AppendLine("    The midpoint adds NO new information.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION-THEORETIC ANALYSIS: IS THE MIDPOINT EXTREMAL?");
        sb.AppendLine();
        sb.AppendLine("  At Q = 2/3 (Koide), the normalized entropy S/S_max ~ 0.51");
        sb.AppendLine("  (QG-057). This is CLOSE to 1/2 but not EXACTLY 1/2.");
        sb.AppendLine();
        sb.AppendLine("  WHY 'CLOSE BUT NOT EXACT' IS IMPORTANT:");
        sb.AppendLine("    - The participation ratio Q = 2/3 is an EXACT midpoint");
        sb.AppendLine("      (of the Q range).");
        sb.AppendLine("    - But the entropy S is NOT exactly half of S_max.");
        sb.AppendLine("    - The 'midpoint' is exact only in the Q-coordinate, not");
        sb.AppendLine("      in the entropy coordinate. The two measures differ.");
        sb.AppendLine();
        sb.AppendLine("  THIS DISTINGUISHES 'GEOMETRIC MIDPOINT' FROM 'INFORMATION':");
        sb.AppendLine("    - Q = 2/3 is a GEOMETRIC fact (midpoint in Q-space).");
        sb.AppendLine("    - It is NOT an INFORMATION-theoretic extremum.");
        sb.AppendLine("    - The geometry (45°, balanced S3) is the real content;");
        sb.AppendLine("      the information (entropy ~half) is approximate.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The midpoint is GEOMETRIC (exact in Q, 45°), not");
        sb.AppendLine("  informational (entropy ~half, not exact). No information");
        sb.AppendLine("  principle selects Q = 2/3.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ATTRACTOR ANALYSIS: CAN MIDPOINT BE SELECTED?");
        sb.AppendLine();
        sb.AppendLine("  Could attractor dynamics drive the spectrum to the midpoint?");
        sb.AppendLine();
        sb.AppendLine("  CANDIDATE: two competing attractors.");
        sb.AppendLine("    - Democratic attractor: pulls toward Q = 1/3 (S3-symmetric).");
        sb.AppendLine("    - Hierarchical attractor: pulls toward Q = 1 (S3-broken).");
        sb.AppendLine("    - Equal-strength competition → equilibrium at Q = 2/3.");
        sb.AppendLine("    This is PLAUSIBLE but SPECULATIVE:");
        sb.AppendLine("      - No AT generation-sector dynamics is specified.");
        sb.AppendLine("      - 'Equal strength' is ASSUMED, not derived.");
        sb.AppendLine("      - The midpoint would be an ATTRACTOR, but unproven.");
        sb.AppendLine();
        sb.AppendLine("  THE BALANCE = THE 45° (already known):");
        sb.AppendLine("    The 'midpoint' attractor IS the 'balanced S3' (45°).");
        sb.AppendLine("    It is the SAME object. The attractor picture does NOT");
        sb.AppendLine("    add a new mechanism — it re-describes the balance as");
        sb.AppendLine("    a dynamical equilibrium.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The attractor interpretation is COHERENT (balance");
        sb.AppendLine("  = equilibrium of two attractors) but SPECULATIVE and adds");
        sb.AppendLine("  no derivation. It is the 45° balance in dynamical clothing.");
        return sb.ToString();
    }

    static string BuildE(CrossSystem[] analogies)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CROSS-SYSTEM ANALOGIES");
        sb.AppendLine();
        sb.AppendLine("  The participation ratio N_eff = 1/sum p_i^2 appears across");
        sb.AppendLine("  physics and beyond:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-26} {2}", "System", "Quantity", "Relevance"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var a in analogies)
        {
            string r = a.Relevance.Length > 45 ? a.Relevance[..42]+"..." : a.Relevance;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-26} {2}", a.System, a.Quantity, r));
        }
        sb.AppendLine();
        sb.AppendLine("  THE UNIVERSAL PATTERN:");
        sb.AppendLine("    The participation ratio (N_eff = 1/sum p_i^2) is a UNIVERSAL");
        sb.AppendLine("    measure of 'effective number of components' — used in");
        sb.AppendLine("    localization, ecology, politics, entanglement, and flavor.");
        sb.AppendLine("    Koide N_eff = 1.5 is one instance.");
        sb.AppendLine();
        sb.AppendLine("  BUT NO 'MIDPOINT PRINCIPLE' EXISTS ANYWHERE:");
        sb.AppendLine("    - No field has a principle that effective number = 1.5.");
        sb.AppendLine("    - The 'midpoint' value (2/3) is N-DEPENDENT (specific to 3).");
        sb.AppendLine("    - The analogies share the FORMULA, not the VALUE 2/3.");
        sb.AppendLine("    So the analogies show Koide uses a UNIVERSAL tool, but");
        sb.AppendLine("    they do NOT explain why Q = 2/3.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The participation ratio is a UNIVERSAL mathematical");
        sb.AppendLine("  tool (shared across fields), but the specific value 2/3 is");
        sb.AppendLine("  N=3-specific and unexplained. No cross-field 'midpoint law'.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: IS THE MIDPOINT NUMEROLOGY?");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE MIDPOINT IS A RESTATEMENT, NOT A DISCOVERY':");
        sb.AppendLine("     CORRECT. Q=2/3 = midpoint is EXACTLY equivalent to");
        sb.AppendLine("     cos^2(theta)=1/2 = singlet=doublet (QG-046/047). The");
        sb.AppendLine("     'midpoint' is the SAME balance in participation-ratio");
        sb.AppendLine("     coordinates. It adds NO new information.");
        sb.AppendLine();
        sb.AppendLine("  2. 'THE MIDPOINT IS N-DEPENDENT, NOT FUNDAMENTAL':");
        sb.AppendLine("     CORRECT. 2/3 = (3+1)/(2·3) is specific to N=3. For N=4 it");
        sb.AppendLine("     would be 5/8. So '2/3' is NOT a universal principle —");
        sb.AppendLine("     it is (N+1)/(2N) at N=3. Any claim of 'fundamental 2/3'");
        sb.AppendLine("     fails the N-independence test.");
        sb.AppendLine();
        sb.AppendLine("  3. 'NO CROSS-FIELD MIDPOINT LAW EXISTS':");
        sb.AppendLine("     CORRECT. The participation ratio is universal, but no");
        sb.AppendLine("     field has a '1.5 effective components' law. The analogies");
        sb.AppendLine("     share the tool, not the value.");
        sb.AppendLine();
        sb.AppendLine("  4. 'THE ATTRACTOR PICTURE IS UNFALSIFIABLE':");
        sb.AppendLine("     PARTIALLY CORRECT. 'Two equal attractors → midpoint' is");
        sb.AppendLine("     coherent but unfalsifiable without a generation-sector");
        sb.AppendLine("     dynamics. It is speculation, not derivation.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     The midpoint observation is VALID but NON-REVELATORY.");
        sb.AppendLine("     It is the 45° balance in new coordinates. Classification:");
        sb.AppendLine("     B (weak preference) at best; the 'midpoint principle'");
        sb.AppendLine("     does not exist as an independent mechanism.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR FLAVOR PHYSICS");
        sb.AppendLine();
        sb.AppendLine("  1. THE MIDPOINT IS THE BALANCE, REPACKAGED:");
        sb.AppendLine("    Q=2/3 (midpoint) = 45° (angle) = singlet=doublet (S3).");
        sb.AppendLine("    Three coordinates, ONE unexplained fact. The midpoint");
        sb.AppendLine("    observation does NOT deepen the mystery — it CONFIRMS it.");
        sb.AppendLine();
        sb.AppendLine("  2. THE N-DEPENDENCE IS THE REAL CONTENT:");
        sb.AppendLine("    The midpoint value (N+1)/(2N) shows that Q=2/3 is tied");
        sb.AppendLine("    to N=3. So the '2/3' is TWO facts: (a) N=3 (selected,");
        sb.AppendLine("    QG-053), and (b) the balance (45°). Neither is new.");
        sb.AppendLine();
        sb.AppendLine("  3. THE UNIVERSAL TOOL, NOT A UNIVERSAL VALUE:");
        sb.AppendLine("    The participation ratio (1/sum p_i^2) is a UNIVERSAL tool");
        sb.AppendLine("    (localization, ecology, entanglement, flavor). But the");
        sb.AppendLine("    VALUE 2/3 is flavor-specific. Koide uses a universal tool");
        sb.AppendLine("    to express a specific unexplained relation.");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT WOULD CONSTITUTE PROGRESS:");
        sb.AppendLine("    - A mechanism that derives the BALANCE (45°) from S¹ +");
        sb.AppendLine("      U(1) charge (QG-050's lepton-specific chain).");
        sb.AppendLine("    - The midpoint would then follow (it's the same balance).");
        sb.AppendLine("    - No such mechanism exists. The balance remains the core.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    The 'midpoint principle' does NOT exist. Q=2/3 is the");
        sb.AppendLine("    balance (45°) in participation-ratio coordinates. This");
        sb.AppendLine("    audit CLOSES the midpoint question: it is real but not");
        sb.AppendLine("    a new principle.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("REMAINING OPEN PROBLEMS");
        sb.AppendLine();
        sb.AppendLine("  After QG-058, the flavor program's open problems are:");
        sb.AppendLine();
        sb.AppendLine("  1. THE BALANCE (45°): why singlet = doublet. The core.");
        sb.AppendLine("     (Equivalently: Q=2/3, N_eff=3/2, the midpoint.)");
        sb.AppendLine();
        sb.AppendLine("  2. THE MASS SCALE: why m_e = 0.511 MeV (the absolute scale).");
        sb.AppendLine("     Koide constrains ratios, not the overall scale.");
        sb.AppendLine();
        sb.AppendLine("  3. THE LEPTON SPECIFICITY: why charged leptons only (QG-050).");
        sb.AppendLine("     The S¹ + U(1) chain locates but doesn't derive.");
        sb.AppendLine();
        sb.AppendLine("  4. THE QUARK SECTOR: why quarks DON'T follow Koide (QG-048).");
        sb.AppendLine("     Confinement scrambling FALSIFIED (QG-049). Georgi-Jarlskog");
        sb.AppendLine("     relations are a separate, unexplained structure.");
        sb.AppendLine();
        sb.AppendLine("  5. THE NEUTRINO SECTOR: neutrino-Koide untested (QG-050).");
        sb.AppendLine("     A falsifiable prediction: Q = 2/3 or NOT for neutrinos.");
        sb.AppendLine();
        sb.AppendLine("  THE CORE REMAINS: derive the balance (45° / Q=2/3) from");
        sb.AppendLine("  the S¹-winding + U(1)-charge architecture. Unreached.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  MIDPOINT = THE 45° BALANCE, REPACKAGED (NO NEW PRINCIPLE)");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Q=2/3 is at the midpoint because it is the SAME balance");
        sb.AppendLine("      as the 45° (singlet=doublet). No new mechanism.");
        sb.AppendLine("  Q2: YES — Q=2/3 = balance between symmetry (1/3) and");
        sb.AppendLine("      differentiation (1). This IS the 45° balance.");
        sb.AppendLine("  Q3: 'Maximum coexistence' = the balance. Same fact.");
        sb.AppendLine("  Q4: N_eff = 1.5 because N=3 and the balance (3/2 = 3 gen / 2).");
        sb.AppendLine("  Q5: 'Halfway between 1 and 3 modes' = the balance. Same.");
        sb.AppendLine("  Q6: Attractor midpoint: SPECULATIVE (no dynamics specified).");
        sb.AppendLine("  Q7: Actualization → midpoint: NO mechanism shown.");
        sb.AppendLine("  Q8: Midpoint is NOT extremal in information geometry.");
        sb.AppendLine("  Q9: No cross-field 'midpoint law' (participation ratio is");
        sb.AppendLine("      universal, but the value 2/3 is N=3-specific).");
        sb.AppendLine("  Q10: Q=2/3 = the N=3 midpoint = the balance. Not a new invariant.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: A — PURE NUMEROLOGY (as a 'principle')");
        sb.AppendLine("  / B — WEAK STRUCTURAL PREFERENCE (as a restatement)");
        sb.AppendLine();
        sb.AppendLine("    The 'midpoint principle' does NOT exist as an independent");
        sb.AppendLine("    mechanism. Q=2/3 is EXACTLY the 45° balance (QG-046/047),");
        sb.AppendLine("    expressed in participation-ratio coordinates.");
        sb.AppendLine();
        sb.AppendLine("    KEY FINDINGS:");
        sb.AppendLine("      1. N-dependence: 2/3 = (N+1)/(2N) at N=3. NOT universal.");
        sb.AppendLine("      2. Equivalence: Q=2/3 = 45° = singlet=doublet. One fact.");
        sb.AppendLine("      3. No cross-field midpoint law. No information extremum.");
        sb.AppendLine();
        sb.AppendLine("    THE MIDPOINT ADDS NO NEW INFORMATION:");
        sb.AppendLine("    It CONFIRMS (in a new coordinate) that the charged-lepton");
        sb.AppendLine("    spectrum sits at the balance point. The balance itself");
        sb.AppendLine("    (45°) remains the unexplained core.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 58 experiments.");
        return sb.ToString();
    }
}
