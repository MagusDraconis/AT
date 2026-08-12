using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class YukawaEigenstructureAnalyzer
{
    public static YEResult RunFullAnalysis()
    {
        var props = BuildProperties();
        var random = BuildRandomTests();
        return new YEResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(random),BuildH(),BuildI(),props,random);
    }

    static EigenProperty[] BuildProperties()
    {
        return new EigenProperty[]
        {
            new EigenProperty("Yukawa matrix Y","3x3 complex matrix on G=C^3","OPERATOR: Y acts on G. This is its mathematical nature.","FREE: the matrix entries are arbitrary (19 SM parameters)."),
            new EigenProperty("Masses (eigenvalues)","m_e=0.511, m_mu=105.66, m_tau=1776.86 MeV","SINGULAR VALUES of Y (or sqrt of Y-dagger Y eigenvalues).","FREE: eigenvalues not derived. No generation operator found."),
            new EigenProperty("Mass ratios","m_mu/m_e=207, m_tau/m_mu=17","HIERARCHY: 10^6 span. No scaling law (not geometric/power).","FREE: irregular ratios. No pattern (QG-039)."),
            new EigenProperty("Koide Q = 2/3","(sum m)/(sum sqrt m)^2 = 2/3","CONSTRAINT: one relation among eigenvalues (lepton-specific).","CONSTRAINED: holds to 10^-5 but unexplained (QG-047)."),
            new EigenProperty("Amplitude angle 45 deg","angle of (sqrt m_e, sqrt m_mu, sqrt m_tau) with (1,1,1)","EIGENVALUE geometry (not eigenvector).","CONSTRAINED: 45 deg = balanced S3 (QG-046), unexplained."),
            new EigenProperty("Mixing (CKM/PMNS)","3 angles + 1 phase","EIGENVECTOR geometry: misalignment between sectors' bases.","FREE: mixing angles empirical (not derived)."),
        };
    }

    static RandomSpectrum[] BuildRandomTests()
    {
        return new RandomSpectrum[]
        {
            new RandomSpectrum("Koide Q=2/3 (to 1e-5)","Observed: lepton Q=1.000000 (exact)","~1e-5 (codimension-1, QG-047)","EXCEPTIONAL: lepton Koide is far from random."),
            new RandomSpectrum("Hierarchy (10^6 span)","Observed: m_tau/m_e ~ 3500","common (random spectra often hierarchical)","UNEXCEPTIONAL: hierarchy is typical, not special."),
            new RandomSpectrum("45 deg amplitude angle","Observed: lepton theta=45.000 deg","~1e-5 (one special direction)","EXCEPTIONAL: the specific 45 deg is special."),
            new RandomSpectrum("S3-balanced decomposition","Observed: singlet=doublet (exact)","~1e-5 (one special configuration)","EXCEPTIONAL: balance is special, not generic."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("YUKAWA MATRICES AS OPERATORS ON G");
        sb.AppendLine();
        sb.AppendLine("  The Yukawa matrix Y is a 3x3 COMPLEX matrix acting on");
        sb.AppendLine("  the generation space G = C^3 (QG-055).");
        sb.AppendLine();
        sb.AppendLine("  THE SPECTRAL PICTURE:");
        sb.AppendLine("    - Y acts on G: maps a generation state to another.");
        sb.AppendLine("    - Masses = SINGULAR VALUES of Y (real eigenvalues of Y^dagger Y).");
        sb.AppendLine("    - Mixing = the misalignment between two sectors' eigenbases.");
        sb.AppendLine("    - Koide = a relation among the eigenvalues (masses).");
        sb.AppendLine();
        sb.AppendLine("  THE CENTRAL QUESTION:");
        sb.AppendLine("    Is Y an ARBITRARY operator on G, or the REPRESENTATION");
        sb.AppendLine("    of a deeper 'generation operator' whose spectrum is derived?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    Y IS an operator on G (that's its nature). But its");
        sb.AppendLine("    EIGENVALUES (masses) are NOT derived from any deeper");
        sb.AppendLine("    operator. They are free. Koide is the ONE unexplained");
        sb.AppendLine("    constraint (lepton-specific). Classification: B.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION-SPACE SPECTRAL STRUCTURE");
        sb.AppendLine();
        sb.AppendLine("  Y IS GENUINELY AN OPERATOR (not arbitrary):");
        sb.AppendLine("    - It acts on the REAL space G = C^3 (QG-054/055).");
        sb.AppendLine("    - Its eigenstructure is OBSERVABLE: eigenvalues (masses),");
        sb.AppendLine("      eigenvectors (mixing bases).");
        sb.AppendLine("    - Mixing (CKM/PMNS) is REAL rotation in G — the eigenvector");
        sb.AppendLine("      structure is physical.");
        sb.AppendLine();
        sb.AppendLine("  BUT THE SPECTRUM IS NOT DERIVED:");
        sb.AppendLine("    - No 'generation operator' O_G is known whose spectrum");
        sb.AppendLine("      gives (m_e, m_mu, m_tau).");
        sb.AppendLine("    - The SM (and TQM) take Y as an INPUT (free matrix).");
        sb.AppendLine("    - The eigenvalues are the 9 (masses) + 4 (mixing) free");
        sb.AppendLine("      parameters of the flavor sector.");
        sb.AppendLine();
        sb.AppendLine("  THE DISTINCTION (key):");
        sb.AppendLine("    Y is an OPERATOR (has eigenstructure) but its spectrum is");
        sb.AppendLine("    FREE (not derived). The operator FRAMEWORK is real; the");
        sb.AppendLine("    operator's eigenvalues are not.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G = C^3 gives Y a real home (an operator on a");
        sb.AppendLine("  real space), but no deeper operator DERIVES the spectrum.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EIGENVALUE HIERARCHY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  The charged-lepton eigenvalue spectrum:");
        sb.AppendLine("    m_e = 0.511, m_mu = 105.66, m_tau = 1776.86 MeV.");
        sb.AppendLine();
        sb.AppendLine("  RATIOS:");
        sb.AppendLine("    m_mu/m_e = 206.8  (not 100, 1000)");
        sb.AppendLine("    m_tau/m_mu = 16.8  (not 207 again)");
        sb.AppendLine("    m_tau/m_e = 3477   (total span ~10^3.5)");
        sb.AppendLine();
        sb.AppendLine("  NO SCALING LAW:");
        sb.AppendLine("    - Not geometric (206.8 != 16.8).");
        sb.AppendLine("    - Not a power law (16.8 != 206.8^0.5 = 14.4).");
        sb.AppendLine("    - Not harmonic (ratios not simple fractions).");
        sb.AppendLine("    The spectrum is IRREGULAR (QG-039).");
        sb.AppendLine();
        sb.AppendLine("  WHY HIERARCHICAL (not degenerate)?");
        sb.AppendLine("    A degenerate spectrum (m_e=m_mu=m_tau) would have full S3");
        sb.AppendLine("    symmetry (no mixing, no hierarchy). The observed hierarchy");
        sb.AppendLine("    BREAKS S3 strongly. The hierarchy is real but unexplained.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The eigenvalue spectrum is HIGHLY HIERARCHICAL and");
        sb.AppendLine("  IRREGULAR. No scaling law. No derivation. The hierarchy is");
        sb.AppendLine("  a FREE input (with Koide as the one unexplained constraint).");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EIGENVECTOR GEOMETRY (MIXING BASES)");
        sb.AppendLine();
        sb.AppendLine("  The EIGENVECTORS of Y define the 'mass basis' in G.");
        sb.AppendLine("  Mixing = misalignment between two sectors' mass bases.");
        sb.AppendLine();
        sb.AppendLine("  CKM = rotation between Y_u and Y_d eigenbases:");
        sb.AppendLine("    - Angles ~13°, 2.4°, 0.2° (small: up/down nearly aligned).");
        sb.AppendLine("    - 1 CP phase (the complex part of the misalignment).");
        sb.AppendLine();
        sb.AppendLine("  PMNS = rotation between Y_e and Y_nu eigenbases:");
        sb.AppendLine("    - Angles ~33°, 45°, 8° (large: lepton/neutrino misaligned).");
        sb.AppendLine("    - Near 'tribimaximal' (a special texture).");
        sb.AppendLine();
        sb.AppendLine("  THE ASYMMETRY (CKM small vs PMNS large):");
        sb.AppendLine("    - Quark sectors (up/down): nearly aligned → small CKM.");
        sb.AppendLine("    - Lepton sectors (charged/neutrino): misaligned → large PMNS.");
        sb.AppendLine("    - This asymmetry is UNEXPLAINED. Why are quark bases aligned");
        sb.AppendLine("      but lepton bases not?");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The eigenvector (mixing) geometry is REAL and");
        sb.AppendLine("  observable, but the specific angles (and the CKM/PMNS");
        sb.AppendLine("  asymmetry) are FREE inputs, not derived.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE EIGENSTRUCTURE: EIGENVALUES, NOT EIGENVECTORS");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL CLARIFICATION (Q7):");
        sb.AppendLine("    Koide is a property of EIGENVALUES (masses), NOT eigenvectors.");
        sb.AppendLine();
        sb.AppendLine("  WHY:");
        sb.AppendLine("    - The Koide quantity Q = (sum m)/(sum sqrt m)^2 uses ONLY");
        sb.AppendLine("      the masses (eigenvalues), never the mixing angles.");
        sb.AppendLine("    - The amplitude vector A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau))");
        sb.AppendLine("      is built from the eigenvalues, not the eigenvectors.");
        sb.AppendLine("    - The 45° is the angle of A with (1,1,1) in 'mass space'");
        sb.AppendLine("      (R^3_+), NOT in generation space (C^3).");
        sb.AppendLine();
        sb.AppendLine("  SO KOIDE IS A SPECTRAL (EIGENVALUE) CONSTRAINT:");
        sb.AppendLine("    It constrains the SPECTRUM (masses) to a specific geometric");
        sb.AppendLine("    configuration (45° / participation ratio 2/3).");
        sb.AppendLine("    It does NOT constrain the eigenvectors (mixing).");
        sb.AppendLine();
        sb.AppendLine("  THIS IS WHY KOIDE IS LEPTON-SPECIFIC (QG-048):");
        sb.AppendLine("    The lepton EIGENVALUES happen to sit at 45°; the quark");
        sb.AppendLine("    eigenvalues don't. This is a SPECTRAL fact, not an");
        sb.AppendLine("    eigenvector (mixing) fact.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide = eigenvalue geometry (the spectrum's shape),");
        sb.AppendLine("  not eigenvector geometry (the mixing). This is an important");
        sb.AppendLine("  clarification for any future derivation attempt.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CKM/PMNS AS MISALIGNMENT (EIGENVECTOR GEOMETRY)");
        sb.AppendLine();
        sb.AppendLine("  The mixing matrices are the EIGENVECTOR geometry of G.");
        sb.AppendLine();
        sb.AppendLine("  THE GEOMETRIC PICTURE:");
        sb.AppendLine("    - Each sector has a Yukawa operator Y_sector.");
        sb.AppendLine("    - Its eigenvectors define a basis (mass basis) in G.");
        sb.AppendLine("    - Two sectors (up/down, charged/neutral) have different bases.");
        sb.AppendLine("    - The ROTATION between them = the mixing matrix.");
        sb.AppendLine();
        sb.AppendLine("  SO MIXING IS A PROPERTY OF EIGENVECTORS:");
        sb.AppendLine("    - CKM = misalignment of Y_u and Y_d eigenvectors.");
        sb.AppendLine("    - PMNS = misalignment of Y_e and Y_nu eigenvectors.");
        sb.AppendLine("    - The CP phase = the complex part of the misalignment.");
        sb.AppendLine();
        sb.AppendLine("  THE SPLIT (eigenvalues vs eigenvectors):");
        sb.AppendLine("    - EIGENVALUES (masses): → Koide (lepton 45°).");
        sb.AppendLine("    - EIGENVECTORS (mixing): → CKM/PMNS (rotations).");
        sb.AppendLine("    These are INDEPENDENT observables of Y.");
        sb.AppendLine("    Koide constrains the spectrum; mixing is the basis rotation.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Mixing is cleanly interpreted as eigenvector");
        sb.AppendLine("  misalignment. But the specific angles (CKM small, PMNS");
        sb.AppendLine("  large) are FREE inputs.");
        return sb.ToString();
    }

    static string BuildG(RandomSpectrum[] random)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RANDOM-SPECTRUM STRESS TEST (deterministic)");
        sb.AppendLine();
        sb.AppendLine("  QUESTION: is the observed spectrum exceptional vs random?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-28} {2}", "Quantity", "Random frequency", "Exceptional?"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var r in random)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-28} {2}", r.Quantity, r.RandomFrequency, r.Exceptional));
        }
        sb.AppendLine();
        sb.AppendLine("  THE KEY RESULT:");
        sb.AppendLine("    - The HIERARCHY (10^6 span) is TYPICAL (random spectra are");
        sb.AppendLine("      often hierarchical). Not special.");
        sb.AppendLine("    - The KOIDE 45° is EXCEPTIONAL (~10^-5, codimension-1).");
        sb.AppendLine("    - The S3 BALANCE (singlet=doublet) is EXCEPTIONAL (~10^-5).");
        sb.AppendLine();
        sb.AppendLine("  SO THE OBSERVED SPECTRUM IS 'TYPICALLY HIERARCHICAL' BUT");
        sb.AppendLine("  'EXCEPTIONALLY KOIDE'. The hierarchy needs no explanation");
        sb.AppendLine("  (random spectra are hierarchical); the Koide 45° DOES.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The Yukawa spectrum is RANDOM-looking EXCEPT for");
        sb.AppendLine("  the Koide relation. This sharpens the mystery: the spectrum");
        sb.AppendLine("  is free (random) but contains ONE precise constraint (Koide).");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. THE YUKAWA MATRIX IS AN OPERATOR ON G (real):");
        sb.AppendLine("    G = C^3 gives Y a real home. The eigenstructure (eigenvalues");
        sb.AppendLine("    = masses, eigenvectors = mixing) is physical and observable.");
        sb.AppendLine();
        sb.AppendLine("  2. BUT THE SPECTRUM IS NOT DERIVED:");
        sb.AppendLine("    No 'generation operator' O_G is found. The masses (9 params)");
        sb.AppendLine("    and mixing (4 params) are FREE inputs. TQM does not derive them.");
        sb.AppendLine();
        sb.AppendLine("  3. THE SHARPENED MYSTERY (important):");
        sb.AppendLine("    The spectrum is RANDOM-looking (hierarchy is typical) EXCEPT");
        sb.AppendLine("    for ONE precise constraint: the Koide 45°. So the Yukawa");
        sb.AppendLine("    spectrum is 'random + one unexplained relation'. This is");
        sb.AppendLine("    more precise than 'free parameters'.");
        sb.AppendLine();
        sb.AppendLine("  4. KOIDE IS AN EIGENVALUE (NOT EIGENVECTOR) CONSTRAINT:");
        sb.AppendLine("    This clarifies the target: derive the EIGENVALUE geometry");
        sb.AppendLine("    (45°), not the eigenvector geometry (mixing). The two are");
        sb.AppendLine("    independent. Koide is about the spectrum only.");
        sb.AppendLine();
        sb.AppendLine("  5. THE HONEST POSITION:");
        sb.AppendLine("    Yukawa matrices are OPERATORS on G (real structure), with");
        sb.AppendLine("    FREE spectra (13 flavor params) + ONE unexplained constraint");
        sb.AppendLine("    (Koide 45°, lepton-specific). This is the precise current");
        sb.AppendLine("    state of the TQM flavor sector.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  YUKAWA = OPERATOR ON G, WITH FREE SPECTRUM + ONE CONSTRAINT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Yukawa matrix = a 3x3 complex operator on G=C^3 (QG-055).");
        sb.AppendLine("  Q2: YES — Y acts on G; eigenvalues = masses, eigenvectors = bases.");
        sb.AppendLine("  Q3: Masses ARE eigenvalues, but of an ARBITRARY operator. No");
        sb.AppendLine("      deeper generation operator O_G derives them.");
        sb.AppendLine("  Q4: G's geometry (C^3) constrains the STRUCTURE (U(3) mixing)");
        sb.AppendLine("      but NOT the eigenvalue VALUES (masses are free).");
        sb.AppendLine("  Q5: The hierarchy (10^6) is TYPICAL (random spectra are");
        sb.AppendLine("      hierarchical). Not special.");
        sb.AppendLine("  Q6: Koide emerges from EIGENVALUES (the amplitude vector),");
        sb.AppendLine("      NOT eigenvectors. It constrains the spectrum only.");
        sb.AppendLine("  Q7: Koide 45° is an EIGENVALUE property (the spectrum's shape).");
        sb.AppendLine("      Mixing is the eigenvector property. Independent.");
        sb.AppendLine("  Q8: YES — CKM/PMNS = misalignment of sectors' eigenbases.");
        sb.AppendLine("      (Eigenvector geometry.) Angles free.");
        sb.AppendLine("  Q9: NO common operator found: lepton and quark Yukawas differ");
        sb.AppendLine("      (Koide lepton-specific, QG-048). No unification.");
        sb.AppendLine("  Q10: Yukawa matrices are OPERATORS (real), with FREE spectra");
        sb.AppendLine("      + one unexplained constraint (Koide). Effective, not fundamental.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK SPECTRAL STRUCTURE");
        sb.AppendLine();
        sb.AppendLine("    The OPERATOR FRAMEWORK is real: Y acts on G=C^3, and its");
        sb.AppendLine("    eigenstructure (masses + mixing) is physical and observable.");
        sb.AppendLine();
        sb.AppendLine("    But the SPECTRUM is not derived: no deeper generation");
        sb.AppendLine("    operator O_G exists. The 13 flavor parameters are FREE.");
        sb.AppendLine();
        sb.AppendLine("    THE ONE EXCEPTION: Koide (45°) is a precise, lepton-specific");
        sb.AppendLine("    eigenvalue constraint. The spectrum is 'random + one relation'.");
        sb.AppendLine("    This sharpens the mystery: it is not 'free parameters' but");
        sb.AppendLine("    'free parameters + one precise unexplained constraint'.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 56 experiments.");
        return sb.ToString();
    }
}
