using System.Globalization;

namespace AT.Core.ResearchQG;

public static class LeptonSpecificSymmetryAnalyzer
{
    public static LSSResult RunFullAnalysis()
    {
        var comp = BuildComparison();
        var sym = BuildSymmetries();
        return new LSSResult(BuildA(),BuildB(comp),BuildC(),BuildD(),BuildE(),BuildF(sym),BuildG(),BuildH(),BuildI(),comp,sym);
    }

    static LeptonQuark[] BuildComparison()
    {
        return new LeptonQuark[]
        {
            new LeptonQuark("Color charge","None (colorless, SU(3) singlet)","SU(3) triplet (r,g,b)","Quarks carry color; leptons do not. Color enables confinement."),
            new LeptonQuark("U(1) charge","Integer (e = g·n, n=±1)","FRACTIONAL (+2/3, -1/3)","Fractional charge is IMPOSSIBLE for S¹ winding (n∈Z). Quarks are NOT simple S¹ vortices."),
            new LeptonQuark("Winding number","n = ±1 (clean S¹ topology)","n = 1/3, 2/3 (NOT S¹ — needs SU(3))","QG-034: winding is integer. Quark fractional charge requires SU(3), not U(1) S¹."),
            new LeptonQuark("Confinement","Unconfined (free)","Confined (no free quarks)","Quarks are permanently bound; leptons propagate freely."),
            new LeptonQuark("Topological structure","Elementary n=1 vortex","Composite (3 vortices in color singlet)","Leptons are the minimal winding; baryons are bound n=3 states (QG-034)."),
            new LeptonQuark("Generation structure","Clean S3 (3 excitation levels)","Entangled with color","Lepton generations are pure; quark generations mix with color DOF."),
        };
    }

    static HiddenSymmetry[] BuildSymmetries()
    {
        return new HiddenSymmetry[]
        {
            new HiddenSymmetry("S3 permutation (QG-046)","Permutation of 3 generations","YES: 45° = balanced S3 singlet/doublet","PARTIAL: S3 acts on any 3-fold structure, not just leptons","B: S3 explains the GEOMETRY but not lepton-specificity."),
            new HiddenSymmetry("S¹ winding (QG-034)","Integer winding n=±1 → charge","PARTIAL: winding gives integer charge, not 45°","YES: only leptons have integer winding (quarks fractional)","B: explains WHY leptons differ from quarks (integer vs fractional)."),
            new HiddenSymmetry("U(1) phase symmetry","Charge coupling to gauge field","PARTIAL: charge is integer (from S¹)","PARTIAL: charged leptons couple; neutrinos decouple","B: distinguishes charged leptons from neutrinos."),
            new HiddenSymmetry("Lepton purity (n=1, colorless, unconfined)","Leptons = bare S¹ vortices, no color dressing","HYPOTHESIS: the 45° is the BARE generation geometry","YES: quarks are dressed (color), leptons bare","B→C: coherent but the 45° value still not derived."),
            new HiddenSymmetry("NULL (coincidence)","Koide is accidental, lepton-specific","NO (assumes accident)","YES (trivially: any accident is specific)","A: simpler but unexplained. Falsifiable via neutrino-Koide."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE LEPTON-SPECIFIC MYSTERY");
        sb.AppendLine();
        sb.AppendLine("  ESTABLISHED CHAIN:");
        sb.AppendLine("    QG-039a: Koide exact (charged leptons, 10^-5).");
        sb.AppendLine("    QG-046: 45° = balanced S3 decomposition.");
        sb.AppendLine("    QG-048: Koide NOT universal (quarks fail).");
        sb.AppendLine("    QG-049: QCD does NOT scramble (common factor preserves theta).");
        sb.AppendLine();
        sb.AppendLine("  THE SHARPENED QUESTION:");
        sb.AppendLine("    WHY does the charged-lepton sector show a near-perfect");
        sb.AppendLine("    45° generation geometry, while quarks do not?");
        sb.AppendLine("    What makes leptons special?");
        sb.AppendLine();
        sb.AppendLine("  THE CANDIDATE ANSWERS:");
        sb.AppendLine("    1. Colorlessness (no confinement).");
        sb.AppendLine("    2. Integer charge (clean S¹ winding).");
        sb.AppendLine("    3. Elementary (n=1) vs composite (n=3) structure.");
        sb.AppendLine("    4. A lepton-specific hidden symmetry.");
        sb.AppendLine("    5. Pure coincidence.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The STRUCTURAL lepton-quark difference is clear (integer");
        sb.AppendLine("    vs fractional charge, S¹ vs SU(3) topology). This EXPLAINS");
        sb.AppendLine("    WHY Koide is lepton-specific, but NOT the 45° value itself.");
        return sb.ToString();
    }

    static string BuildB(LeptonQuark[] comp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LEPTON-QUARK COMPARISON");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-20} {1,-30} {2,-32}", "Property", "Leptons", "Quarks"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var c in comp)
        {
            string l = c.Leptons.Length > 30 ? c.Leptons[..27]+"..." : c.Leptons;
            string q = c.Quarks.Length > 32 ? c.Quarks[..29]+"..." : c.Quarks;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-20} {1,-30} {2,-32}", c.Property, l, q));
        }
        sb.AppendLine();
        sb.AppendLine("  THE SMOKING GUN: FRACTIONAL CHARGE.");
        sb.AppendLine("    - Leptons: charge e = g·n with n = ±1 (INTEGER).");
        sb.AppendLine("      This is PURE S¹ winding — the circle's integer topology.");
        sb.AppendLine("    - Quarks: charge +2/3, -1/3 (FRACTIONAL).");
        sb.AppendLine("      n = 1/3 is IMPOSSIBLE for S¹ winding (n ∈ Z).");
        sb.AppendLine("      Quarks are NOT simple S¹ vortices — they need SU(3) color.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEP RESULT:");
        sb.AppendLine("    Leptons ARE the S¹-winding fermions. Quarks are NOT.");
        sb.AppendLine("    The S3 generation symmetry (→ Koide 45°) acts on the");
        sb.AppendLine("    excitation levels of the S¹ vortex. It is therefore");
        sb.AppendLine("    STRUCTURALLY LEPTON-SPECIFIC: only leptons are pure S¹.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COLOR VERSUS NO-COLOR ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  QG-049 RESULT: QCD's dominant effect is a COMMON factor,");
        sb.AppendLine("  which PRESERVES theta (does not scramble). So the COLOR");
        sb.AppendLine("  RUNNING is not what makes quarks differ.");
        sb.AppendLine();
        sb.AppendLine("  BUT COLOR IS STILL THE KEY STRUCTURAL DIFFERENCE:");
        sb.AppendLine("    - Color enables FRACTIONAL charge (SU(3) singlet condition");
        sb.AppendLine("      forces quark charges to be ±1/3, ±2/3).");
        sb.AppendLine("    - Fractional charge means quarks are NOT S¹ vortices.");
        sb.AppendLine("    - Therefore the S¹-winding generation geometry (S3/Koide)");
        sb.AppendLine("      does NOT apply to quarks in the same clean way.");
        sb.AppendLine();
        sb.AppendLine("  SO 'ABSENCE OF COLOR' IS SUFFICIENT IN A DEEP SENSE:");
        sb.AppendLine("    It is not the color RUNNING that matters (QG-049).");
        sb.AppendLine("    It is that color PERMITS fractional charge, which");
        sb.AppendLine("    EXCLUDES quarks from the S¹-winding category.");
        sb.AppendLine("    Leptons (no color) are forced to integer charge = S¹.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: colorlessness → integer charge → S¹ winding →");
        sb.AppendLine("  clean S3 generation geometry. This is a CHAIN, not a");
        sb.AppendLine("  single fact. The chain explains lepton-specificity.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION SYMMETRY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Q6: Does S3 act primarily on the lepton sector?");
        sb.AppendLine();
        sb.AppendLine("  S3 = permutation of 3 excitation levels of an S¹ vortex.");
        sb.AppendLine("    - Leptons: n=1 vortex → 3 excitation levels (e, mu, tau).");
        sb.AppendLine("      S3 permutes them cleanly. The 45° is the balanced");
        sb.AppendLine("      S3 decomposition of these 3 levels.");
        sb.AppendLine("    - Quarks: NOT S¹ vortices (fractional charge). Their");
        sb.AppendLine("      3 generations are 3 excitation levels of an SU(3)-color");
        sb.AppendLine("      structure, entangled with color DOF. S3 acts, but is");
        sb.AppendLine("      entangled with SU(3) → no clean 45°.");
        sb.AppendLine();
        sb.AppendLine("  WHY S3 IS 'CLEAN' FOR LEPTONS:");
        sb.AppendLine("    The generation index (excitation level) and the topology");
        sb.AppendLine("    (S¹ winding) are INDEPENDENT for leptons. S3 acts only");
        sb.AppendLine("    on the generation index.");
        sb.AppendLine("    For quarks, color (SU(3)) and generation are entangled,");
        sb.AppendLine("    so the permutation symmetry is 'polluted' by color.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S3 acts on all fermions, but it is CLEAN only");
        sb.AppendLine("  where the topology is pure S¹ (leptons). Quark S3 is");
        sb.AppendLine("  entangled with SU(3) → no clean 45° geometry.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NEUTRINO CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("  Neutrinos ARE leptons (colorless, n=1, unconfined).");
        sb.AppendLine("  If the S3 generation symmetry is LEPTON-specific,");
        sb.AppendLine("  neutrinos should ALSO follow it (a neutrino-Koide).");
        sb.AppendLine();
        sb.AppendLine("  BUT NEUTRINOS ARE NEARLY DEGENERATE:");
        sb.AppendLine("    - Charged leptons: m_e : m_mu : m_tau = 1 : 207 : 3478");
        sb.AppendLine("      (strong hierarchy, clear 45°).");
        sb.AppendLine("    - Neutrinos: sum of masses < 0.12 eV, splittings ~meV.");
        sb.AppendLine("      (nearly degenerate or mildly hierarchical).");
        sb.AppendLine("    - Neutrino masses ~10^6 lighter than electrons.");
        sb.AppendLine();
        sb.AppendLine("  THE TENSION:");
        sb.AppendLine("    If neutrinos share the S3 45° geometry, their masses");
        sb.AppendLine("    would be hierarchical like charged leptons. They are NOT.");
        sb.AppendLine("    So either:");
        sb.AppendLine("      (a) Neutrinos do NOT share the S3 structure.");
        sb.AppendLine("      (b) They share it but with DIFFERENT mass scale/pattern.");
        sb.AppendLine("      (c) The near-degeneracy hides a Koide that is untestable");
        sb.AppendLine("          at current precision.");
        sb.AppendLine();
        sb.AppendLine("  THE CHARGE ASYMMETRY (QG-048):");
        sb.AppendLine("    Charged leptons couple to U(1) (phase winding → charge).");
        sb.AppendLine("    Neutrinos do NOT couple to U(1) (no charge).");
        sb.AppendLine("    This suggests: the 45° geometry requires the U(1) CHARGE");
        sb.AppendLine("    coupling (phase winding), which neutrinos lack.");
        sb.AppendLine("    So the symmetry is CHARGED-LEPTON-specific, not lepton-specific.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Neutrino correspondence is UNTESTED (masses too");
        sb.AppendLine("  small). The charge asymmetry suggests the 45° may be tied to");
        sb.AppendLine("  the U(1) winding (charge), making it CHARGED-lepton-specific.");
        sb.AppendLine("  Falsifiable prediction: a neutrino-Koide would TEST this.");
        return sb.ToString();
    }

    static string BuildF(HiddenSymmetry[] sym)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HIDDEN ARCHITECTURE INVESTIGATION");
        sb.AppendLine();
        sb.AppendLine("  CANDIDATE HIDDEN SYMMETRIES (evaluated):");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-22} {2}", "Candidate", "Status", "Assessment"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var s in sym)
        {
            string a = s.Status.Length > 55 ? s.Status[..52]+"..." : s.Status;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-22} {2}", s.Candidate, s.ExplainsLeptonSpecificity, a));
        }
        sb.AppendLine();
        sb.AppendLine("  THE MOST COHERENT PICTURE (lepton purity):");
        sb.AppendLine("    Leptons = BARE S¹ vortices (integer winding, no color).");
        sb.AppendLine("    The 45° is the BARE generation geometry (S3 balanced).");
        sb.AppendLine("    Quarks = DRESSED (color, fractional charge) → no clean 45°.");
        sb.AppendLine("    Neutrinos = BARE but U(1)-DECOUPLED → tiny masses, no");
        sb.AppendLine("    hierarchy. The 45° needs BOTH S¹ winding AND U(1) charge.");
        sb.AppendLine();
        sb.AppendLine("  THIS IS COHERENT BUT NOT DERIVED:");
        sb.AppendLine("    The lepton-purity picture EXPLAINS lepton-specificity");
        sb.AppendLine("    (integer winding + charge = clean S3). But the 45° value");
        sb.AppendLine("    itself remains unexplained (QG-047).");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'INTEGER VS FRACTIONAL CHARGE IS A RESTATEMENT, NOT AN EXPLANATION':");
        sb.AppendLine("     PARTIALLY CORRECT. Saying 'leptons have integer charge, quarks");
        sb.AppendLine("     fractional' is TRUE but does not DERIVE the 45°. It explains");
        sb.AppendLine("     WHY the S3 geometry might apply to leptons (S¹ winding) but");
        sb.AppendLine("     not quarks (SU(3)). That IS progress — it locates the");
        sb.AppendLine("     lepton-specificity in the topology — but it is not a derivation.");
        sb.AppendLine();
        sb.AppendLine("  2. 'THE NEUTRINO COUNTEREXAMPLE IS SERIOUS':");
        sb.AppendLine("     Neutrinos are leptons (colorless, n=1) but do NOT show the");
        sb.AppendLine("     45° hierarchy (they are nearly degenerate). This WEAKENS the");
        sb.AppendLine("     'lepton-specific' hypothesis and STRENGTHENS the 'charged-");
        sb.AppendLine("     lepton-specific' refinement. The charge asymmetry is real.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE NULL MODEL (COINCIDENCE) IS STILL VIABLE':");
        sb.AppendLine("     CORRECT. Nothing here RULES OUT 'Koide is a lepton-specific");
        sb.AppendLine("     accident'. The structural story is COHERENT but not DECISIVE.");
        sb.AppendLine("     Only a neutrino-Koide (or a derivation of 45°) would decide.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ESTABLISHED':");
        sb.AppendLine("     - Leptons = integer winding (S¹); quarks = fractional (SU(3)).");
        sb.AppendLine("       This is a REAL, DERIVED structural difference (QG-034/038).");
        sb.AppendLine("     - The S3 generation symmetry is CLEAN only for S¹ fermions.");
        sb.AppendLine("     - The charge asymmetry (charged vs neutral leptons) is real.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     AT EXPLAINS the lepton/quark structural difference but");
        sb.AppendLine("     does NOT DERIVE the 45°. Classification: B (weak lepton-");
        sb.AppendLine("     specific effect). The 45° remains the open core.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. LEPTON-SPECIFICITY IS LOCATED IN THE TOPOLOGY:");
        sb.AppendLine("    The Koide 45° is a property of PURE S¹ WINDING fermions.");
        sb.AppendLine("    Leptons (integer charge) have it; quarks (fractional,");
        sb.AppendLine("    SU(3)) do not. This is a genuine structural result.");
        sb.AppendLine();
        sb.AppendLine("  2. THE REFINED RESEARCH TARGET:");
        sb.AppendLine("    Not 'derive Koide' but 'derive the S3-balanced geometry");
        sb.AppendLine("    OF THE S¹ VORTEX excitation spectrum'. This is more");
        sb.AppendLine("    specific: it ties the 45° to the S¹ topology.");
        sb.AppendLine();
        sb.AppendLine("  3. THE CHARGE ASYMMETRY IS A NEW CLUE:");
        sb.AppendLine("    Charged leptons (U(1)-coupled): 45° hierarchy.");
        sb.AppendLine("    Neutrinos (U(1)-decoupled): near-degenerate.");
        sb.AppendLine("    This suggests the 45° requires the U(1) charge coupling");
        sb.AppendLine("    (phase winding). The hierarchy is tied to CHARGE.");
        sb.AppendLine();
        sb.AppendLine("  4. FALSIFIABLE PREDICTIONS (two):");
        sb.AppendLine("    (a) Neutrino-Koide: if neutrinos follow S3, Q=2/3 for");
        sb.AppendLine("        neutrino masses (testable with future data).");
        sb.AppendLine("    (b) Charged-lepton-only: if the 45° needs charge, the");
        sb.AppendLine("        neutrino Koide will FAIL (also testable).");
        sb.AppendLine("    These are MUTUALLY EXCLUSIVE — one must be wrong.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    AT explains WHY leptons differ from quarks (topology)");
        sb.AppendLine("    and WHY charged differ from neutral (charge). But the");
        sb.AppendLine("    45° value remains the unexplained core.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  LEPTON-SPECIFICITY = S¹-WINDING STRUCTURE (45° STILL OPEN)");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: The fundamental lepton/quark difference = TOPOLOGY:");
        sb.AppendLine("      leptons are integer-winding (S¹), quarks fractional (SU(3)).");
        sb.AppendLine("  Q2: Koide appears only among e/mu/tau because ONLY they are");
        sb.AppendLine("      pure S¹ vortices with U(1) charge.");
        sb.AppendLine("  Q3: Absence of color is necessary (forces integer charge)");
        sb.AppendLine("      but not sufficient (neutrinos are colorless yet degenerate).");
        sb.AppendLine("  Q4: YES — leptons are the more primitive (n=1, elementary).");
        sb.AppendLine("  Q5: YES — quarks are composite (confined n=3), leptons elementary.");
        sb.AppendLine("  Q6: S3 acts cleanly only on UNCONFINED INTEGER-winding fermions.");
        sb.AppendLine("  Q7: The 45° emerges from S3 ONLY for the S¹ (integer) sector.");
        sb.AppendLine("  Q8: Neutrinos share the winding but NOT the charge; their");
        sb.AppendLine("      near-degeneracy suggests the 45° needs CHARGE.");
        sb.AppendLine("  Q9: Koide likely originates from the CHARGED-lepton (U(1)");
        sb.AppendLine("      winding) architecture, not the full lepton sector.");
        sb.AppendLine("  Q10: YES — the charged-lepton sector reveals the S¹ generation");
        sb.AppendLine("      geometry in its purest form.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK LEPTON-SPECIFIC EFFECT");
        sb.AppendLine();
        sb.AppendLine("    AT EXPLAINS the lepton-specificity (integer vs fractional");
        sb.AppendLine("    charge → S¹ vs SU(3) topology), which is a GENUINE");
        sb.AppendLine("    structural result (QG-034/038).");
        sb.AppendLine();
        sb.AppendLine("    But the 45° VALUE itself remains unexplained. The lepton-");
        sb.AppendLine("    purity picture is COHERENT but not DERIVED.");
        sb.AppendLine();
        sb.AppendLine("    THE REFINED MYSTERY:");
        sb.AppendLine("    Koide 45° = the balanced S3 geometry of the S¹ vortex");
        sb.AppendLine("    excitation spectrum, realized ONLY in charged leptons.");
        sb.AppendLine("    This is the purest window into generation architecture —");
        sb.AppendLine("    and its core (the 45°) remains unopened.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 50 experiments.");
        return sb.ToString();
    }
}
