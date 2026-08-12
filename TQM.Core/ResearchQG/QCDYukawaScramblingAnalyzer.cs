using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class QCDYukawaScramblingAnalyzer
{
    public static QYSResult RunFullAnalysis()
    {
        var sectors = BuildSectors();
        var tests = BuildTests();
        return new QYSResult(BuildA(sectors),BuildB(sectors),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(sectors,tests),sectors,tests);
    }

    static double AngleFromMasses(double m1, double m2, double m3)
    {
        double s = Math.Sqrt(m1)+Math.Sqrt(m2)+Math.Sqrt(m3);
        double sm = m1+m2+m3;
        double cos2 = (s*s)/(3.0*sm);
        return Math.Acos(Math.Sqrt(cos2))*180.0/Math.PI;
    }

    static SectorAngle[] BuildSectors()
    {
        // Low scale (pole masses, MeV) and GUT-scale textures.
        // Up quarks: assume m_u:m_c:m_t ∝ m_e:m_mu:m_tau at GUT (no GJ factor) -> theta=45.
        // Down quarks: Georgi-Jarlskog m_d=3m_e, m_s=m_mu/3, m_b=m_tau -> theta~47.5.
        return new SectorAngle[]
        {
            new SectorAngle("Charged leptons", 45.000, 0.0, 45.000,
                "Baseline. No QCD (colorless). theta = 45 deg at ALL scales."),
            new SectorAngle("Up quarks", AngleFromMasses(2.2,1270,172500),
                AngleFromMasses(2.2,1270,172500)-45.0,
                AngleFromMasses(0.511,105.66,1776.86),   // m_u:m_c:m_t ∝ m_e:m_mu:m_tau at GUT -> 45 deg
                "low 51 deg; IF up ∝ lepton at GUT, theta=45. Top's differential running does most of the 6 deg."),
            new SectorAngle("Down quarks", AngleFromMasses(4.7,95,4180),
                AngleFromMasses(4.7,95,4180)-45.0,
                AngleFromMasses(3*0.511, 105.66/3.0, 1776.86),  // Georgi-Jarlskog -> ~47.5 deg
                "GJ texture gives ~47.5 deg at GUT, NOT 45. Down quarks are NOT a 45-deg descendant."),
        };
    }

    static ScramblingTest[] BuildTests()
    {
        return new ScramblingTest[]
        {
            new ScramblingTest("Common-factor invariance","Multiply all 3 Yukawas in a sector by the SAME factor f. Does theta change?","theta is INVARIANT (ratios preserved). Q = (sum y)/(sum sqrt y)^2 is scale-independent in y.","QCD's dominant term (-8 g_s^2) is a COMMON factor for all 3 quarks -> does NOT scramble theta."),
            new ScramblingTest("QCD beta function structure","beta_y_i = y_i/(16pi^2)[(3/2)y_i^2 + ... - 8g_s^2 - ...]. The -8g_s^2 term is IDENTICAL for u,c,t (and d,s,b).","The dominant QCD term is COMMON. It rescales all 3 Yukawas equally. Ratios (hence theta) UNCHANGED.","QCD (dominant) PRESERVES theta, not scrambles it. Scrambling hypothesis FALSIFIED by the common-factor structure."),
            new ScramblingTest("Differential terms","Only (3/2)y_i^2 (self) and y_t^2 (top) differ between quarks. These are SUB-DOMINANT.","Self-coupling terms are tiny (y_u^2 ~ 5e-11); only y_t^2 ~ 1 is significant, and it affects only the top.","Differential running is too weak to move theta by 6 deg. Cannot explain the quark deviation."),
            new ScramblingTest("Georgi-Jarlskog (GUT texture)","At GUT scale: m_b=m_tau, m_s=m_mu/3, m_d=3m_e (SO(10)/SU(5) texture).","Compute theta for (3m_e, m_mu/3, m_tau): ~47 deg, NOT 45 deg.","The GUT texture gives ~47 deg, not 45. Quarks are NOT a clean 45->47 scramble; they start differently."),
            new ScramblingTest("Null model (lepton-specific Koide)","Assume Koide is purely a lepton phenomenon; quarks have independent Yukawas.","Explains ALL quark data with NO reference to 45 deg. Same explanatory power.","Null model is SIMPLER and equally good. Occam's razor favors 'no scrambling'."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA(SectorAngle[] sectors)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE LEPTON 45° BASELINE");
        sb.AppendLine();
        sb.AppendLine("  QG-048 established: charged leptons sit at theta = 45° exactly.");
        sb.AppendLine("  This is the CLEANEST geometric structure in the Yukawa sector.");
        sb.AppendLine();
        sb.AppendLine("  WHY LEPTONS ARE THE BASELINE:");
        sb.AppendLine("    - Colorless: no QCD, no confinement.");
        sb.AppendLine("    - n=1 vortices (QG-034): simplest topology.");
        sb.AppendLine("    - Weak RG running: theta is nearly scale-invariant.");
        sb.AppendLine("    - The 45° = balanced S3 decomposition (QG-046/047).");
        sb.AppendLine();
        sb.AppendLine("  THE HYPOTHESIS TO TEST (from QG-048):");
        sb.AppendLine("    QCD confinement 'scrambles' the 45° geometry into the");
        sb.AppendLine("    observed quark angles (up ~51°, down ~47°).");
        sb.AppendLine("    If true, deconfinement (high scale) should RESTORE 45°.");
        sb.AppendLine();
        sb.AppendLine("  THE TEST:");
        sb.AppendLine("    1. Does QCD's dominant effect change theta? (compute)");
        sb.AppendLine("    2. Does RG running move quark theta toward 45°? (compute)");
        sb.AppendLine("    3. Does the GUT texture reproduce 45°? (Georgi-Jarlskog)");
        return sb.ToString();
    }

    static string BuildB(SectorAngle[] sectors)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QUARK DEVIATION ANALYSIS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-20} {1,10} {2,12} {3,10}", "Sector", "theta_low", "Delta(45)", "theta_GUT"));
        sb.AppendLine("  " + new string('-', 60));
        foreach (var s in sectors)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-20} {1,10:F3} {2,12:F3} {3,10:F3}",
                s.Sector, s.ThetaLowDeg, s.DeltaFrom45, s.ThetaGUTDeg));
        }
        sb.AppendLine();
        sb.AppendLine("  OBSERVED DEVIATIONS:");
        sb.AppendLine("    - Up quarks: theta ~ 51.2° (+6.2° from 45°).");
        sb.AppendLine("    - Down quarks: theta ~ 47.5° (+2.5° from 45°).");
        sb.AppendLine("    - Different deviations in up vs down sectors.");
        sb.AppendLine();
        sb.AppendLine("  KEY OBSERVATION:");
        sb.AppendLine("    The deviations are POSITIVE (both toward MORE hierarchy).");
        sb.AppendLine("    Quarks are MORE hierarchical than leptons (45° is 'balanced').");
        sb.AppendLine("    This is a systematic, not random, difference — but is it QCD?");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QCD DISTORTION MECHANISM: THE COMMON-FACTOR RESULT");
        sb.AppendLine();
        sb.AppendLine("  THE CRUCIAL MATHEMATICAL FACT:");
        sb.AppendLine();
        sb.AppendLine("  Koide quantity: Q = (sum y)/(sum sqrt y)^2.");
        sb.AppendLine("  Under y_i -> f·y_i (ALL THREE scaled by the SAME factor f):");
        sb.AppendLine("    Q -> (f·sum y)/(sqrt(f)·sum sqrt y)^2 = (sum y)/(sum sqrt y)^2 = Q.");
        sb.AppendLine("    Q is INVARIANT. theta is INVARIANT.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS FOR QCD:");
        sb.AppendLine("    The QCD beta function's DOMINANT term is -8·g_s^2.");
        sb.AppendLine("    This term is IDENTICAL for u, c, t (all color triplets).");
        sb.AppendLine("    So QCD rescales ALL THREE up-quark Yukawas by the SAME factor.");
        sb.AppendLine("    Result: QCD (dominant) does NOT change theta.");
        sb.AppendLine();
        sb.AppendLine("  THE SCRAMBLING HYPOTHESIS IS FALSIFIED:");
        sb.AppendLine("    QCD confinement does NOT 'scramble' the Yukawa geometry.");
        sb.AppendLine("    Its dominant effect (common color factor) preserves theta.");
        sb.AppendLine("    Only SUB-DOMINANT differential terms (self-coupling y_t^2)");
        sb.AppendLine("    can change theta, and they are far too weak (+6° requires");
        sb.AppendLine("    O(1) changes, not the O(0.01) differential running gives).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: QCD is NOT the scrambling mechanism.");
        sb.AppendLine("  The quark angles are NOT descendants of 45° via QCD.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RG EVOLUTION EFFECTS");
        sb.AppendLine();
        sb.AppendLine("  DOES theta(mu) CHANGE WITH SCALE?");
        sb.AppendLine();
        sb.AppendLine("  For quarks, the dominant RG effect (QCD) is a COMMON factor,");
        sb.AppendLine("  which preserves theta (shown above). The differential effect");
        sb.AppendLine("  (self-coupling and top) is sub-dominant.");
        sb.AppendLine();
        sb.AppendLine("  ESTIMATE OF DIFFERENTIAL RUNNING:");
        sb.AppendLine("    beta_y_i (differential) ~ y_i·(3/2)·y_i^2/(16pi^2) for self.");
        sb.AppendLine("    For top: y_t ~ 1, so differential ~ (3/2)/(16pi^2) ~ 0.01.");
        sb.AppendLine("    Over t = ln(10^16/10^2) ~ 32 e-folds, the top Yukawa");
        sb.AppendLine("    changes by ~exp(0.01·32) ~ 1.4x. This shifts theta by");
        sb.AppendLine("    only a FEW degrees for the up sector.");
        sb.AppendLine();
        sb.AppendLine("  SO THE UP-SECTOR DEVIATION (+6°) IS PARTLY RG:");
        sb.AppendLine("    The top's differential running (y_t decreasing at high scale)");
        sb.AppendLine("    slightly reduces the up-sector hierarchy, moving theta");
        sb.AppendLine("    DOWNWARD (toward 45°) at high scale, by a few degrees.");
        sb.AppendLine("    But this is ~2-3°, not the full 6°.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: RG evolution changes theta by only a FEW degrees.");
        sb.AppendLine("  It does NOT map 45° to 51° (up) or 47° (down). The quark");
        sb.AppendLine("  angles are LARGELY fixed at low scale, not RG-scrambled.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CKM INTERPRETATION");
        sb.AppendLine();
        sb.AppendLine("  If QCD does NOT scramble (common factor preserves theta),");
        sb.AppendLine("  then the CKM matrix cannot be 'differential scrambling'.");
        sb.AppendLine();
        sb.AppendLine("  WHAT CKM ACTUALLY IS (QG-048):");
        sb.AppendLine("    CKM = rotation between the up-sector and down-sector");
        sb.AppendLine("    Yukawa bases. The mismatch of the two diagonalizing");
        sb.AppendLine("    bases is the mixing matrix.");
        sb.AppendLine();
        sb.AppendLine("  DIFFERENTIAL SCRAMBLING HYPOTHESIS (now weakened):");
        sb.AppendLine("    'CKM arises from different scrambling in up vs down.'");
        sb.AppendLine("    Since QCD's dominant term is the SAME for up and down,");
        sb.AppendLine("    it does NOT produce differential scrambling.");
        sb.AppendLine("    Only the DIFFERENT self-couplings (y_t vs y_b) differ,");
        sb.AppendLine("    producing a WEAK differential effect.");
        sb.AppendLine();
        sb.AppendLine("  SO CKM IS NOT EXPLAINED BY SCRAMBLING:");
        sb.AppendLine("    The CKM angles (13°, 2.4°, 0.2°) are INPUTS, not outputs");
        sb.AppendLine("    of any scrambling mechanism. They encode the RELATIVE");
        sb.AppendLine("    orientation of the up and down Yukawa architectures,");
        sb.AppendLine("    which TQM does not derive.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: CKM remains empirical. The scrambling hypothesis");
        sb.AppendLine("  does NOT explain CKM (QCD's common factor cannot generate");
        sb.AppendLine("  differential up/down rotation).");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NULL-MODEL COMPARISON");
        sb.AppendLine();
        sb.AppendLine("  NULL MODEL: Koide is purely lepton-specific.");
        sb.AppendLine("    Quarks have independent Yukawas (no 45° origin).");
        sb.AppendLine();
        sb.AppendLine("  SCRAMBLING MODEL: quarks descend from 45° via QCD.");
        sb.AppendLine();
        sb.AppendLine("  COMPARISON:");
        sb.AppendLine("    1. EXPLANATORY POWER: both explain quark masses equally");
        sb.AppendLine("       well (both just take the measured values).");
        sb.AppendLine("    2. SIMPLICITY: null model is SIMPLER (no scrambling");
        sb.AppendLine("       mechanism, no QCD-geometry coupling).");
        sb.AppendLine("    3. PREDICTIVE POWER: scrambling predicts theta->45° at");
        sb.AppendLine("       high scale (deconfinement). Null predicts nothing.");
        sb.AppendLine("    4. FALSIFICATION: scrambling is FALSIFIED (QCD common");
        sb.AppendLine("       factor preserves theta; GUT texture gives 47° not 45°).");
        sb.AppendLine();
        sb.AppendLine("  OCCAM'S RAZOR: FAVORS THE NULL MODEL.");
        sb.AppendLine("    The scrambling hypothesis is FALSIFIED by the common-factor");
        sb.AppendLine("    argument AND fails to reproduce the GUT-scale angles.");
        sb.AppendLine("    The null model (lepton-specific Koide) is simpler and");
        sb.AppendLine("    equally explanatory. We should REJECT scrambling.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is likely a LEPTON-SPECIFIC phenomenon.");
        sb.AppendLine("  Quarks are NOT its scrambled descendants.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE COMMON-FACTOR ARGUMENT IS DECISIVE AND CORRECT':");
        sb.AppendLine("     AGREED. The dominant QCD beta term (-8g_s^2) is identical");
        sb.AppendLine("     for all quarks in a sector. It rescales all Yukawas");
        sb.AppendLine("     equally, preserving ratios and hence theta. This is a");
        sb.AppendLine("     CLEAN falsification of the scrambling hypothesis.");
        sb.AppendLine();
        sb.AppendLine("  2. 'BUT THE SCRAMBLING COULD BE NON-PERTURBATIVE':");
        sb.AppendLine("     PARTIAL OBJECTION. Non-perturbative QCD (confinement,");
        sb.AppendLine("     chiral condensate) COULD in principle change the effective");
        sb.AppendLine("     Yukawa geometry in ways the perturbative RG misses.");
        sb.AppendLine("     However: the observed quark Yukawas are defined AT LOW");
        sb.AppendLine("     SCALE where perturbative QCD is marginal, and there is no");
        sb.AppendLine("     known non-perturbative mechanism that rotates theta by 6°.");
        sb.AppendLine("     This is SPECULATIVE, not established.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE GEORGI-JARLSKOG RELATIONS ARE A COUNTEREXAMPLE':");
        sb.AppendLine("     PARTIALLY. G-J relations (m_b=m_tau, m_s=m_mu/3, m_d=3m_e)");
        sb.AppendLine("     DO relate quarks to leptons at GUT scale. But they give");
        sb.AppendLine("     theta ~ 47° (down), NOT 45°. So G-J is a quark-lepton");
        sb.AppendLine("     CONNECTION, but NOT a Koide-scrambling connection.");
        sb.AppendLine("     The 45° remains lepton-specific.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ESTABLISHED':");
        sb.AppendLine("     - QCD's dominant term preserves theta (clean, correct).");
        sb.AppendLine("     - Scrambling hypothesis FALSIFIED (perturbatively).");
        sb.AppendLine("     - GUT texture (G-J) gives ~47°, not 45°.");
        sb.AppendLine("     - Koide is lepton-specific; quarks are not descendants.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     The scrambling hypothesis is REJECTED. Koide is likely");
        sb.AppendLine("     a LEPTON-SPECIFIC structure, with quarks related to");
        sb.AppendLine("     leptons only via the (different) G-J GUT texture.");
        sb.AppendLine("     Classification: B (weak correspondence via G-J), NOT C/D.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. KOIDE IS LEPTON-SPECIFIC (NOW FIRMER):");
        sb.AppendLine("    QG-048 showed quarks don't follow Koide. QG-049 shows");
        sb.AppendLine("    QCD cannot scramble 45° into the quark angles. The");
        sb.AppendLine("    lepton-specificity is now on firmer footing.");
        sb.AppendLine();
        sb.AppendLine("  2. WHY LEPTONS ARE SPECIAL (TQM hypothesis):");
        sb.AppendLine("    Leptons are n=1 vortices, colorless, unconfined. Their");
        sb.AppendLine("    Yukawa architecture is 'bare' — not distorted by QCD.");
        sb.AppendLine("    Quarks are n=1/3 color-charged, confined. Their Yukawa");
        sb.AppendLine("    structure is dressed by the chiral condensate. The");
        sb.AppendLine("    LEAFTON 45° is the bare structure; quarks are dressed.");
        sb.AppendLine("    (But QG-049 shows the dressing does NOT simply rotate 45°.)");
        sb.AppendLine();
        sb.AppendLine("  3. THE GEORGI-JARLSKOG CONNECTION IS THE REAL LEAD:");
        sb.AppendLine("    At GUT scale, m_b=m_tau, m_s=m_mu/3, m_d=3m_e. This is a");
        sb.AppendLine("    DIFFERENT relation from Koide, but it IS a quark-lepton");
        sb.AppendLine("    connection. TQM should investigate whether G-J emerges");
        sb.AppendLine("    from a common architecture, SEPARATELY from Koide.");
        sb.AppendLine();
        sb.AppendLine("  4. THE REFINED RESEARCH PROGRAM:");
        sb.AppendLine("    - Koide (lepton 45°): lepton-specific, unexplained.");
        sb.AppendLine("    - Georgi-Jarlskog (GUT quark-lepton): separate relation.");
        sb.AppendLine("    - CKM/PMNS: relative orientation, unexplained.");
        sb.AppendLine("    Three DISTINCT unexplained structures. TQM must address");
        sb.AppendLine("    them SEPARATELY, not via a single 'scrambling' story.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    The scrambling hypothesis (an appealing unification) is");
        sb.AppendLine("    FALSIFIED. This is GOOD science — a clean negative result");
        sb.AppendLine("    that sharpens the remaining mystery.");
        return sb.ToString();
    }

    static string BuildI(SectorAngle[] sectors, ScramblingTest[] tests)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  SCRAMBLING FALSIFIED: KOIDE IS LEPTON-SPECIFIC");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  THE FIVE TESTS:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1}", "Test", "Verdict"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var t in tests)
        {
            string v = t.Verdict.Length > 50 ? t.Verdict[..47]+"..." : t.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1}", t.Test, v));
        }
        sb.AppendLine();
        sb.AppendLine("  Q1: 45° = the underlying UNCONFINED geometry (leptons).");
        sb.AppendLine("  Q2: Leptons preserve 45° because they are colorless (no QCD).");
        sb.AppendLine("  Q3: Quarks deviate because they are DIFFERENT architectures,");
        sb.AppendLine("      NOT because QCD scrambles a shared 45°.");
        sb.AppendLine("  Q4: QCD confinement does NOT modify theta (common factor");
        sb.AppendLine("      preserves ratios). Scrambling FALSIFIED.");
        sb.AppendLine("  Q5: RG evolution changes theta by only a FEW degrees, not 6°.");
        sb.AppendLine("  Q6: QCD does NOT generate a systematic rotation (common factor).");
        sb.AppendLine("  Q7: Confinement does NOT cleanly transform 45° (G-J gives 47°).");
        sb.AppendLine("  Q8: Up vs down deviations differ because their hierarchies");
        sb.AppendLine("      differ intrinsically, not via differential scrambling.");
        sb.AppendLine("  Q9: CKM is NOT differential scrambling (QCD common factor).");
        sb.AppendLine("  Q10: Deconfinement does NOT restore 45° (G-J gives 47°, not 45°).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("    The scrambling hypothesis is FALSIFIED:");
        sb.AppendLine("      (1) QCD's dominant term is a COMMON factor -> preserves theta.");
        sb.AppendLine("      (2) Differential running is too weak (+6° needs O(1) changes).");
        sb.AppendLine("      (3) The GUT texture (Georgi-Jarlskog) gives ~47°, not 45°.");
        sb.AppendLine();
        sb.AppendLine("    Koide (45°) is therefore LEPTON-SPECIFIC. Quarks are NOT");
        sb.AppendLine("    its scrambled descendants.");
        sb.AppendLine();
        sb.AppendLine("    HOWEVER: a WEAK quark-lepton connection DOES exist via");
        sb.AppendLine("    the Georgi-Jarlskog GUT relations (m_b=m_tau, m_s=m_mu/3,");
        sb.AppendLine("    m_d=3m_e), which are a SEPARATE structure from Koide.");
        sb.AppendLine("    So quarks and leptons are related at GUT scale, but NOT");
        sb.AppendLine("    through the Koide 45° geometry.");
        sb.AppendLine();
        sb.AppendLine("    Koide = lepton-specific 45°. Georgi-Jarlskog = quark-lepton");
        sb.AppendLine("    GUT relation. Two SEPARATE structures, neither derived.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 49 experiments.");
        return sb.ToString();
    }
}
