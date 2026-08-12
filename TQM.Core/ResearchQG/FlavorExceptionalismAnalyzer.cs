using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class FlavorExceptionalismAnalyzer
{
    public static FEResult RunFullAnalysis()
    {
        var relations = BuildRelations();
        var hypotheses = BuildHypotheses();
        return new FEResult(BuildA(),BuildB(relations),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),relations,hypotheses);
    }

    static FlavorRelation[] BuildRelations()
    {
        return new FlavorRelation[]
        {
            new FlavorRelation("Koide (charged leptons)","10^-5 (EXACT)","e, mu, tau","REAL, unexplained (QG-057)","EXCEPTIONAL"),
            new FlavorRelation("Georgi-Jarlskog (GUT)","~10-30% (approximate)","quarks vs leptons","m_b=m_tau, m_s=m_mu/3, m_d=3m_e at GUT","GENERIC (GUT texture)"),
            new FlavorRelation("Wolfenstein (CKM)","~10% (parametric)","quarks","|V_us|~lambda, |V_cb|~lambda^2, lambda~0.22","GENERIC (parametrization)"),
            new FlavorRelation("Tribimaximal (PMNS)","~10% (approximate)","leptons","theta_12~35deg, theta_23~45deg, theta_13~8deg","PARTIAL (approximate texture)"),
            new FlavorRelation("Quark Koide","FAILS (10-20% off)","quarks","Q_up~0.78, Q_down~0.91 (not 2/3)","ABSENT (QG-048)"),
            new FlavorRelation("Neutrino Koide","UNKNOWN","neutrinos","masses too small to test","UNTESTED (QG-050)"),
        };
    }

    static Exceptionalism[] BuildHypotheses()
    {
        return new Exceptionalism[]
        {
            new Exceptionalism("Fundamental law","Koide is a universal flavor law","FAILS: quark Koide does NOT hold (QG-048). Not universal.","REJECTED: lepton-specific, not universal."),
            new Exceptionalism("Emergent structure","Koide emerges from a deeper flavor architecture","PARTIAL: no other precise relation found. No architecture revealed.","B: coherent but unsupported by other relations."),
            new Exceptionalism("Residual (broken symmetry)","Koide is the remnant of a broken lepton-sector symmetry","PARTIAL: the S3 texture (QG-046) suggests a broken symmetry, but the breaking pattern (45 deg) is not derived.","B: plausible remnant, but the value unexplained."),
            new Exceptionalism("Bare-flavor fingerprint","Koide = the bare (unconfined) flavor geometry, S¹ + U(1) charge","COHERENT: leptons = S¹ winding + charge (QG-050); quarks confined (lose it).","B: explains lepton-specificity, not the value 2/3."),
            new Exceptionalism("Coincidence","Koide is an accident","DISFAVORED: ~10^-4 + 1981 prediction (QG-047).","A→B: unlikely but not impossible."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE FLAVOR EXCEPTIONALISM PROBLEM");
        sb.AppendLine();
        sb.AppendLine("  One precise relation stands out in flavor physics:");
        sb.AppendLine("    Koide: Q = 2/3 to 10^-5 (charged leptons).");
        sb.AppendLine("  Everything else in flavor is approximate (10-30%).");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION:");
        sb.AppendLine("    Is Koide the FINAL unexplained number, or the FIRST");
        sb.AppendLine("    crack revealing a deeper flavor architecture?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    Koide is EXCEPTIONAL (the only precise flavor relation).");
        sb.AppendLine("    But it is ISOLATED (no quark/neutrino analog found).");
        sb.AppendLine("    The 'deeper architecture' hypothesis is NOT supported —");
        sb.AppendLine("    no other crack is visible. Koide is a single crack.");
        return sb.ToString();
    }

    static string BuildB(FlavorRelation[] relations)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INVENTORY OF FLAVOR RELATIONS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-16} {2,-16} {3}", "Relation", "Precision", "Type", "Status"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var r in relations)
        {
            string st = r.Status.Length > 30 ? r.Status[..27]+"..." : r.Status;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-16} {2,-16} {3}", r.Name, r.Precision, r.Type, st));
        }
        sb.AppendLine();
        sb.AppendLine("  THE PATTERN:");
        sb.AppendLine("    - EXACTLY ONE relation (Koide) holds to 10^-5.");
        sb.AppendLine("    - All others are APPROXIMATE (10-30%): GUT textures,");
        sb.AppendLine("      Wolfenstein parametrization, tribimaximal.");
        sb.AppendLine("    - Quark Koide FAILS. Neutrino Koide untested.");
        sb.AppendLine();
        sb.AppendLine("  THE EXCEPTIONALISM:");
        sb.AppendLine("    Koide is UNIQUE: the only precise dimensionless flavor");
        sb.AppendLine("    relation. It is an OUTLIER in a sea of approximations.");
        sb.AppendLine("    This exceptionalism is itself a clue (or a warning).");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EXCEPTIONAL VS GENERIC STRUCTURE");
        sb.AppendLine();
        sb.AppendLine("  GENERIC STRUCTURE (approximate, understood):");
        sb.AppendLine("    - Wolfenstein: CKM hierarchy |V_us|~λ, |V_cb|~λ². A");
        sb.AppendLine("      PARAMETRIZATION (λ~0.22), not a derivation. It works");
        sb.AppendLine("      because mixing is hierarchical. Generic.");
        sb.AppendLine("    - Georgi-Jarlskog: GUT-scale mass relations. Approximate");
        sb.AppendLine("      (10-30%). Texture-based, not precise. Generic.");
        sb.AppendLine("    - Tribimaximal PMNS: approximate (10%). Texture-based.");
        sb.AppendLine("    These are APPROXIMATE and have plausible origins (textures,");
        sb.AppendLine("    hierarchies, parametrizations).");
        sb.AppendLine();
        sb.AppendLine("  EXCEPTIONAL STRUCTURE (precise, unexplained):");
        sb.AppendLine("    - Koide: Q = 2/3 to 10^-5. NO approximation, NO texture");
        sb.AppendLine("      derivation, NO parametrization. It is EXACT and");
        sb.AppendLine("      UNEXPLAINED.");
        sb.AppendLine();
        sb.AppendLine("  THE CONTRAST:");
        sb.AppendLine("    Generic relations are APPROXIMATE (arise from hierarchies");
        sb.AppendLine("    and textures). Koide is EXACT (requires a symmetry or");
        sb.AppendLine("    principle, not a texture). This is the exceptionalism.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is EXCEPTIONAL (exact, no known origin),");
        sb.AppendLine("  distinct from the generic (approximate, texture-based)");
        sb.AppendLine("  flavor relations.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HIDDEN SYMMETRY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Could Koide be the remnant of a broken flavor symmetry?");
        sb.AppendLine();
        sb.AppendLine("  CANDIDATE: S3 (permutation of generations, QG-046).");
        sb.AppendLine("    - Exact S3 → democratic masses (Q=1/3, not 2/3).");
        sb.AppendLine("    - Broken S3 'halfway' → Q=2/3 (the 45° balance).");
        sb.AppendLine("    - The S3 TEXTURE reproduces Q=2/3, but with FITTED");
        sb.AppendLine("      breaking parameters (QG-057). Not derived.");
        sb.AppendLine();
        sb.AppendLine("  THE PROBLEM: WHY 'HALFWAY' BREAKING?");
        sb.AppendLine("    A generic S3 breaking gives ANY angle (any Q in [1/3,1]).");
        sb.AppendLine("    The specific 45° (Q=2/3) is NON-GENERIC. No S3 mechanism");
        sb.AppendLine("    forces the 'halfway' point.");
        sb.AppendLine();
        sb.AppendLine("  SO THE 'HIDDEN SYMMETRY' IS ONLY PARTIAL:");
        sb.AppendLine("    S3 provides the FRAMEWORK (singlet/doublet), but the");
        sb.AppendLine("    specific breaking (45°) is unexplained. The hidden");
        sb.AppendLine("    symmetry would need to be LARGER than S3 (something that");
        sb.AppendLine("    forces the balance), but no such symmetry is identified.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S3 is a NECESSARY but INSUFFICIENT hidden symmetry.");
        sb.AppendLine("  It gives the geometry, not the balance. The true hidden");
        sb.AppendLine("  symmetry (if any) remains unidentified.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LOW-ENERGY REMNANT ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Could Koide be a low-energy projection of GUT-scale structure?");
        sb.AppendLine();
        sb.AppendLine("  THE GUT CONNECTION:");
        sb.AppendLine("    At the GUT scale (~10^16 GeV), quarks and leptons unify.");
        sb.AppendLine("    The Georgi-Jarlskog relations (m_b=m_tau, m_s=m_mu/3, m_d=3m_e)");
        sb.AppendLine("    are GUT-scale mass relations (approximate).");
        sb.AppendLine();
        sb.AppendLine("  IS KOIDE A GUT REMNANT?");
        sb.AppendLine("    - Koide is LEPTON-ONLY (no quark analog). If it were a");
        sb.AppendLine("      GUT remnant, it would involve quarks too. It doesn't.");
        sb.AppendLine("    - Koide is EXACT (10^-5); GUT relations are approximate.");
        sb.AppendLine("      RG running would SPOIL an exact relation (unless");
        sb.AppendLine("      scale-invariant). The exactness argues AGAINST a");
        sb.AppendLine("      high-scale origin with running.");
        sb.AppendLine("    - Koide is DIMENSIONLESS (VEV-independent, QG-039a).");
        sb.AppendLine("      It is not tied to the GUT scale.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is UNLIKELY to be a GUT remnant. It is");
        sb.AppendLine("  lepton-only, exact (scale-invariant), and dimensionless.");
        sb.AppendLine("  It appears to be an INTRINSIC lepton-sector structure,");
        sb.AppendLine("  not a projection from higher scale.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION-SPACE IMPLICATIONS");
        sb.AppendLine();
        sb.AppendLine("  QG-055: G ≈ C^3 (complex, U(3)). QG-056: Yukawa = operator on G.");
        sb.AppendLine();
        sb.AppendLine("  DOES G ADMIT HIDDEN STRUCTURE BEYOND S3/U(3)?");
        sb.AppendLine("    - S3: permutation (discrete). U(3): rotations (continuous).");
        sb.AppendLine("    - The 45° (Koide) is a specific direction in the REAL");
        sb.AppendLine("      mass subsector R^3_+ of G.");
        sb.AppendLine("    - No additional structure of G is known that would");
        sb.AppendLine("      SELECT the 45° direction.");
        sb.AppendLine();
        sb.AppendLine("  THE KEY INSIGHT (QG-050):");
        sb.AppendLine("    Koide lives in the CHARGED-LEPTON sector, which is the");
        sb.AppendLine("    CLEANEST realization of S¹ winding + U(1) charge. The");
        sb.AppendLine("    45° is the fingerprint of this BARE (unconfined) geometry.");
        sb.AppendLine("    Quarks (confined, fractional charge) lose the clean");
        sb.AppendLine("    relation. Neutrinos (uncharged) lose the hierarchy.");
        sb.AppendLine();
        sb.AppendLine("  SO THE 'EXCEPTIONALISM' IS LOCATED:");
        sb.AppendLine("    Koide = the bare S¹+U(1) flavor geometry, realized only");
        sb.AppendLine("    in charged leptons. This EXPLAINS the exceptionalism");
        sb.AppendLine("    (only leptons are bare) but NOT the value (2/3).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G's structure (C^3, S3, U(3)) LOCATES Koide in the");
        sb.AppendLine("  charged-lepton sector but does NOT derive the 45° value.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE COINCIDENCE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  Assume Koide is merely accidental. Compare models.");
        sb.AppendLine();
        sb.AppendLine("  COINCIDENCE MODEL:");
        sb.AppendLine("    - Probability ~10^-4 (look-elsewhere, QG-047).");
        sb.AppendLine("    - FAILS to explain the 1981 PREDICTION (m_tau predicted");
        sb.AppendLine("      before measurement). A coincidence doesn't predict.");
        sb.AppendLine("    - EXPLANATORY POWER: low (just 'it happened').");
        sb.AppendLine();
        sb.AppendLine("  STRUCTURE MODELS (symmetry/geometry/remnant):");
        sb.AppendLine("    - Explain the lepton-specificity (S¹+U(1), QG-050).");
        sb.AppendLine("    - Explain the GEOMETRY (45°, balanced S3).");
        sb.AppendLine("    - But do NOT derive the VALUE (2/3) — all are restatements.");
        sb.AppendLine("    - EXPLANATORY POWER: medium (locate, don't derive).");
        sb.AppendLine();
        sb.AppendLine("  THE COMPARISON:");
        sb.AppendLine("    - Coincidence: simple but fails the prediction test.");
        sb.AppendLine("    - Structure: locates but doesn't derive.");
        sb.AppendLine("    Neither is fully satisfactory. Koide sits between");
        sb.AppendLine("    'not coincidence' and 'not derived'.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is UNLIKELY coincidence (1981 prediction),");
        sb.AppendLine("  but NOT derived (no mechanism). It is a REAL, ISOLATED,");
        sb.AppendLine("  LEPTON-SPECIFIC constraint of unknown origin.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. KOIDE IS EXCEPTIONAL BUT ISOLATED:");
        sb.AppendLine("    It is the ONLY precise flavor relation. No quark/neutrino");
        sb.AppendLine("    analog exists. The 'deeper architecture' is NOT revealed");
        sb.AppendLine("    by other relations — Koide is a single crack.");
        sb.AppendLine();
        sb.AppendLine("  2. THE EXCEPTIONALISM IS LOCATED (not explained):");
        sb.AppendLine("    Koide = the bare S¹+U(1) flavor geometry (charged leptons).");
        sb.AppendLine("    This explains WHY it's exceptional (only leptons are bare)");
        sb.AppendLine("    but NOT the value (2/3). The 'where' is solved; the 'what'");
        sb.AppendLine("    is not.");
        sb.AppendLine();
        sb.AppendLine("  3. THREE LAYERS OF FLAVOR (a clean summary):");
        sb.AppendLine("    - GENERIC: hierarchies, textures (Wolfenstein, GUT, PMNS).");
        sb.AppendLine("    - STRUCTURAL: G=C^3, S3, U(3), mixing (QG-055/056).");
        sb.AppendLine("    - EXCEPTIONAL: Koide Q=2/3 (one precise number).");
        sb.AppendLine("    Only the EXCEPTIONAL layer is unexplained.");
        sb.AppendLine();
        sb.AppendLine("  4. THE FALSIFIABLE TEST (unchanged):");
        sb.AppendLine("    Neutrino-Koide: if neutrinos satisfy Q=2/3, the structure");
        sb.AppendLine("    is LEPTON-wide; if not, it is CHARGED-lepton-specific.");
        sb.AppendLine("    This is the key future measurement.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    Koide is a real, precise, isolated, lepton-specific");
        sb.AppendLine("    constraint. It is NOT explained, and no larger flavor");
        sb.AppendLine("    architecture is revealed. It remains the single");
        sb.AppendLine("    unexplained number in flavor physics.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  KOIDE IS EXCEPTIONAL BUT ISOLATED (NO DEEPER ARCHITECTURE)");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Exactly one precise flavor relation exists (Koide) because");
        sb.AppendLine("      it is the BARE S¹+U(1) geometry, realized only in leptons.");
        sb.AppendLine("  Q2: Koide is the ONLY precise member; quark-Koide FAILS,");
        sb.AppendLine("      neutrino-Koide untested. No larger family is visible.");
        sb.AppendLine("  Q3: Possible additional relations could be hidden, but none");
        sb.AppendLine("      have been found (quark-Koide absent, QG-048).");
        sb.AppendLine("  Q4: If fundamental, it is LEPTON-SPECIFIC (not universal).");
        sb.AppendLine("      This argues against 'fundamental flavor law'.");
        sb.AppendLine("  Q5: If emergent, the deeper structure is NOT identified");
        sb.AppendLine("      (S3 gives geometry, not the balance).");
        sb.AppendLine("  Q6: YES — flavor = generic (hierarchies) + structural (G=C^3)");
        sb.AppendLine("      + exceptional (Koide). Only the last is unexplained.");
        sb.AppendLine("  Q7: No hidden symmetry of G selects 45° (QG-047/057).");
        sb.AppendLine("  Q8: Low-energy fixed point: POSSIBLE but unproven (no RG");
        sb.AppendLine("      mechanism; exactness argues against running).");
        sb.AppendLine("  Q9: YES — neutrino-Koide is the KEY falsifiable test.");
        sb.AppendLine("  Q10: The exceptionalism LOCATES Koide (bare lepton sector)");
        sb.AppendLine("      but does not DERIVE it.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK RESIDUAL STRUCTURE");
        sb.AppendLine();
        sb.AppendLine("    Koide is EXCEPTIONAL (the only precise flavor relation,");
        sb.AppendLine("    10^-5) but ISOLATED (no quark/neutrino analog).");
        sb.AppendLine();
        sb.AppendLine("    It is NOT coincidence (1981 prediction), NOT fundamental");
        sb.AppendLine("    (lepton-specific), and NOT a GUT remnant (exact, scale-");
        sb.AppendLine("    invariant, dimensionless).");
        sb.AppendLine();
        sb.AppendLine("    The best characterization: Koide = the BARE S¹+U(1) flavor");
        sb.AppendLine("    geometry (QG-050), a RESIDUAL of the unconfined lepton");
        sb.AppendLine("    sector. This LOCATES it but does NOT DERIVE the value 2/3.");
        sb.AppendLine();
        sb.AppendLine("    THE CENTRAL QUESTION ANSWERED:");
        sb.AppendLine("    Koide is the FINAL unexplained number, NOT a crack");
        sb.AppendLine("    revealing a deeper architecture. No other crack exists.");
        sb.AppendLine("    It is a single, precise, isolated mystery.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 59 experiments.");
        return sb.ToString();
    }
}
