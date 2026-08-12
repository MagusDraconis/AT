using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class KoideConstraintOriginAnalyzer
{
    public static KCOResult RunFullAnalysis()
    {
        var metrics = ComputeMetrics();
        var origins = BuildOrigins();
        return new KCOResult(BuildA(),BuildB(),BuildC(metrics),BuildD(metrics),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),metrics,origins);
    }

    static InfoMetric[] ComputeMetrics()
    {
        double me = 0.51099895000, mmu = 105.6583755, mtau = 1776.86;
        double se = Math.Sqrt(me), su = Math.Sqrt(mmu), st = Math.Sqrt(mtau);
        double sum = se + su + st;
        double pe = se/sum, pu = su/sum, pt = st/sum;
        double Q = pe*pe + pu*pu + pt*pt;
        double N_eff = 1.0/Q;
        double S = -(pe*Math.Log(pe) + pu*Math.Log(pu) + pt*Math.Log(pt));
        double S_max = Math.Log(3.0);
        return new InfoMetric[]
        {
            new InfoMetric("Participation ratio Q = sum p_i^2", Q, "Koide: Q = 2/3. Midpoint of [1/3 (uniform), 1 (concentrated)]."),
            new InfoMetric("Effective generations N_eff = 1/Q", N_eff, "3/2 = 1.5. Halfway between 1 (dominated) and 2 (binary)."),
            new InfoMetric("Shannon entropy S", S, "Not extremal. Between 0 and ln3 = 1.0986."),
            new InfoMetric("S / S_max (normalized entropy)", S/S_max, "~0.51. Half of maximum entropy. Not special."),
        };
    }

    static KoideOrigin[] BuildOrigins()
    {
        return new KoideOrigin[]
        {
            new KoideOrigin("Spectral sum rule","Q = (sum m)/(sum sqrt m)^2 as a trace-like invariant","NO: Koide is NON-POLYNOMIAL (square roots). Trace/det are polynomial. No standard spectral invariant gives 2/3.","B: Not a standard sum rule. Square roots are unusual."),
            new KoideOrigin("Participation ratio","Q = sum p_i^2 (p_i = sqrt m_i / sum sqrt m)","PARTIAL: Q=2/3 = the MIDPOINT of [1/3,1]. But 'midpoint' is not a mechanism.","B: elegant restatement, not derivation."),
            new KoideOrigin("Information geometry","p_i as a probability; entropy, effective dimension","NO: Q=2/3 is not extremal (entropy S~0.56 is not max or min). No optimization principle.","B: information picture exists, but no extremization."),
            new KoideOrigin("S3 texture (democratic + breaking)","Mass matrix M = a*I + b*D (democratic) + breaking","PARTIAL: specific textures give Q=2/3, but the texture parameters are fitted, not derived.","B: texture reproduces Koide, but parameters are inputs."),
            new KoideOrigin("Lepton specificity (QG-050)","Charged leptons = S¹ winding + U(1) charge","PARTIAL: explains WHY leptons (integer charge) but not WHY 2/3.","B: locates the sector, not the value."),
            new KoideOrigin("Coincidence","Random eigenvalues","NO: ~1e-5 (QG-047), and a 1981 PREDICTION. Unlikely accident.","A→B: coincidence disfavored but not impossible."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE REMAINING FLAVOR MYSTERY");
        sb.AppendLine();
        sb.AppendLine("  The flavor problem has been reduced to its smallest core:");
        sb.AppendLine("    Q = (m_e+m_mu+m_tau) / (sqrt(m_e)+sqrt(m_mu)+sqrt(m_tau))^2 = 2/3");
        sb.AppendLine("  to 10^-5 precision.");
        sb.AppendLine();
        sb.AppendLine("  WHAT IS ESTABLISHED (the chain):");
        sb.AppendLine("    - Koide is an EIGENVALUE relation (not mixing), QG-056.");
        sb.AppendLine("    - Lepton-specific (quarks fail), QG-048/050.");
        sb.AppendLine("    - Geometric (45° = balanced S3), QG-046/047.");
        sb.AppendLine("    - Lives in Yukawa space (VEV cancels), QG-039a.");
        sb.AppendLine();
        sb.AppendLine("  WHAT REMAINS UNKNOWN: WHY Q = 2/3 (not 1/2, 3/4, 0.71)?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    Multiple INTERPRETATIONS exist (participation ratio,");
        sb.AppendLine("    information geometry, S3 texture), but NONE derives Q=2/3");
        sb.AppendLine("    from first principles. The relation is real but unexplained.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SPECTRAL ANALYSIS: IS KOIDE A SUM RULE?");
        sb.AppendLine();
        sb.AppendLine("  Standard spectral invariants of Y (3x3):");
        sb.AppendLine("    - Trace: tr(Y) = m_e + m_mu + m_tau (sum of eigenvalues).");
        sb.AppendLine("    - Determinant: det(Y) = m_e * m_mu * m_tau.");
        sb.AppendLine("    - Characteristic polynomial coefficients (symmetric sums).");
        sb.AppendLine();
        sb.AppendLine("  THE KOIDE RELATION IS NON-STANDARD:");
        sb.AppendLine("    Q = (sum m) / (sum sqrt m)^2 involves SQUARE ROOTS of the");
        sb.AppendLine("    eigenvalues. This is NON-POLYNOMIAL — not expressible in");
        sb.AppendLine("    terms of trace, determinant, or any standard spectral");
        sb.AppendLine("    invariant (which are all polynomial in the eigenvalues).");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS:");
        sb.AppendLine("    - Standard spectral theory (polynomial invariants) CANNOT");
        sb.AppendLine("      produce a sqrt(m) relation.");
        sb.AppendLine("    - The sqrt(m) is the TQM 'amplitude' (QG-039a): m = hbar*omega/c^2");
        sb.AppendLine("      -> sqrt(m) ∝ amplitude. The amplitude is the NATURAL variable.");
        sb.AppendLine("    - So Koide is a constraint on AMPLITUDES, not on masses —");
        sb.AppendLine("      a spectral relation in the AMPLITUDE representation.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is NOT a standard spectral sum rule. It is a");
        sb.AppendLine("  constraint on the SQUARE-ROOT (amplitude) spectrum, which is");
        sb.AppendLine("  non-polynomial and non-standard. No known spectral principle");
        sb.AppendLine("  generates it.");
        return sb.ToString();
    }

    static string BuildC(InfoMetric[] metrics)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PARTICIPATION-RATIO ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Define p_i = sqrt(m_i) / (sum sqrt m). Then sum p_i = 1.");
        sb.AppendLine("  Q = sum p_i^2 = the PARTICIPATION RATIO (Simpson index).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    p_e  = {0:F4}", 0.7148/53.147));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    p_mu = {0:F4}", 10.279/53.147));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    p_tau = {0:F4}", 42.153/53.147));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Q = sum p_i^2 = {0:F4}  (Koide: 2/3 = 0.6667)", metrics[0].Value));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    N_eff = 1/Q = {0:F3}  (effective generations)", metrics[1].Value));
        sb.AppendLine();
        sb.AppendLine("  THE MIDPOINT OBSERVATION:");
        sb.AppendLine("    Q ranges over [1/3 (uniform, 3 equal) ... 1 (concentrated)].");
        sb.AppendLine("    Q = 2/3 is the EXACT MIDPOINT of [1/3, 1]:");
        sb.AppendLine("    (1/3 + 1)/2 = 2/3.");
        sb.AppendLine("    So Koide = the generation spectrum sits at the participation");
        sb.AppendLine("    ratio MIDPOINT. Equivalent to the 45° balance (QG-047).");
        sb.AppendLine();
        sb.AppendLine("  THE EFFECTIVE GENERATIONS = 3/2 = 1.5:");
        sb.AppendLine("    N_eff = 1/Q = 3/2. The 'effective number of generations'");
        sb.AppendLine("    is 1.5 — halfway between 1 (one dominates) and 2 (binary).");
        sb.AppendLine("    The 3/2 = 3 generations / 2 (the balance factor).");
        sb.AppendLine("    This is SUGGESTIVE but not a derivation.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The participation-ratio picture is ELEGANT (Q=2/3 is");
        sb.AppendLine("  the midpoint, N_eff=3/2), but 'midpoint' is a RESTATEMENT,");
        sb.AppendLine("  not a MECHANISM.");
        return sb.ToString();
    }

    static string BuildD(InfoMetric[] metrics)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION-GEOMETRIC ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Interpret p_i as a PROBABILITY distribution over generations.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Participation ratio Q = {0:F4} (Koide 2/3)", metrics[0].Value));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Effective dimension N_eff = {0:F3}", metrics[1].Value));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Shannon entropy S = {0:F4}", metrics[2].Value));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    S/S_max = {0:F4} (max = ln 3)", metrics[3].Value));
        sb.AppendLine();
        sb.AppendLine("  IS Q = 2/3 AN EXTREMUM OF ANY INFORMATION MEASURE?");
        sb.AppendLine("    - Shannon entropy S: NOT extremal (S ~ 0.56, between 0 and ln3).");
        sb.AppendLine("    - Participation ratio: Q = 2/3 is the midpoint, not a max/min.");
        sb.AppendLine("    - No known information principle (max entropy, min concentration)");
        sb.AppendLine("      selects Q = 2/3.");
        sb.AppendLine();
        sb.AppendLine("  THE NORMALIZED ENTROPY S/S_max ~ 0.51:");
        sb.AppendLine("    The generation distribution has ~half the maximum entropy.");
        sb.AppendLine("    This is consistent with 'halfway between democratic (max)");
        sb.AppendLine("    and hierarchical (min)' — the same 45° balance.");
        sb.AppendLine("    But no optimization principle forces S/S_max = 1/2 exactly.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The information-geometric picture is DESCRIPTIVE");
        sb.AppendLine("  (Q = participation ratio, N_eff = 3/2, S ~ 0.56) but finds NO");
        sb.AppendLine("  extremization that selects Q = 2/3. Not a derivation.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("S3 TEXTURE ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Can S3 symmetry generate Q = 2/3 without fitting?");
        sb.AppendLine();
        sb.AppendLine("  EXACT S3 (democratic):");
        sb.AppendLine("    M = m0 * [[1,1,1],[1,1,1],[1,1,1]].");
        sb.AppendLine("    Eigenvalues: (3m0, 0, 0). Two massless + one massive.");
        sb.AppendLine("    Q = (3m0)/(sqrt(3m0)+0+0)^2 = 3m0/3m0 = 1. NOT 2/3.");
        sb.AppendLine("    FAILS: exact S3 gives Q=1, not 2/3.");
        sb.AppendLine();
        sb.AppendLine("  BROKEN S3 (specific textures):");
        sb.AppendLine("    Certain S3-BREAKING textures (e.g., circulant M = a*I + b*C + c*C^2)");
        sb.AppendLine("    CAN reproduce Q = 2/3 for specific (a,b,c). But the (a,b,c)");
        sb.AppendLine("    are FITTED parameters — they are chosen to give Q=2/3, not");
        sb.AppendLine("    derived. This is parameter tuning, not derivation.");
        sb.AppendLine();
        sb.AppendLine("  THE 'HALFWAY' BREAKING (45°):");
        sb.AppendLine("    Q = 2/3 corresponds to S3 broken 'halfway' (singlet = doublet).");
        sb.AppendLine("    But 'halfway' is a specific, non-generic breaking pattern.");
        sb.AppendLine("    No S3 mechanism forces the 'halfway' point.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S3 gives the FRAMEWORK (singlet/doublet), but the");
        sb.AppendLine("  specific 'balanced' breaking (Q=2/3, 45°) is NOT derived from");
        sb.AppendLine("  S3 alone. Texture models FIT Q=2/3; they don't DERIVE it.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LEPTON-SPECIFICITY: WHY CHARGED LEPTONS ONLY?");
        sb.AppendLine();
        sb.AppendLine("  QG-050: Koide is charged-lepton-specific.");
        sb.AppendLine("    - Charged leptons: integer charge, S¹ winding, colorless.");
        sb.AppendLine("    - Quarks: fractional charge, SU(3) color, confined.");
        sb.AppendLine("    - Neutrinos: colorless but U(1)-decoupled (no charge).");
        sb.AppendLine();
        sb.AppendLine("  THE PREREQUISITE CHAIN (QG-050):");
        sb.AppendLine("    colorlessness → integer charge → S¹ winding → clean S3.");
        sb.AppendLine("    Only CHARGED LEPTONS satisfy the full chain.");
        sb.AppendLine("    (Neutrinos are colorless but charge-less → no hierarchy.)");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS LOCATES BUT DOESN'T DERIVE:");
        sb.AppendLine("    The chain explains WHY Koide is charged-lepton-specific");
        sb.AppendLine("    (only they have clean S¹ winding + charge). But it does");
        sb.AppendLine("    NOT explain WHY Q = 2/3 (the specific value).");
        sb.AppendLine("    The 'where' is explained; the 'what' (2/3) is not.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Lepton-specificity is LOCATED (S¹ winding + charge),");
        sb.AppendLine("  but the value Q=2/3 remains the unexplained core.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE COINCIDENCE REVIEW (deterministic)");
        sb.AppendLine();
        sb.AppendLine("  Could Q = 2/3 be a numerical accident?");
        sb.AppendLine();
        sb.AppendLine("  THE QUANTITATIVE ESTIMATE (QG-047):");
        sb.AppendLine("    - Q = 2/3 is a codimension-1 constraint (one relation).");
        sb.AppendLine("    - Precision 10^-5: the relation holds to 1 part in 10^5.");
        sb.AppendLine("    - Naive coincidence probability: ~10^-5.");
        sb.AppendLine("    - Look-elsewhere (quarks, neutrinos also tested): ~10^-4.");
        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION STATUS (decisive):");
        sb.AppendLine("    Koide (1981) PREDICTED m_tau = 1776.97 MeV from m_e, m_mu");
        sb.AppendLine("    BEFORE precise measurement. Confirmed (1992+).");
        sb.AppendLine("    A post-hoc relation can be tuned; a PREDICTION cannot.");
        sb.AppendLine("    This strongly disfavors coincidence.");
        sb.AppendLine();
        sb.AppendLine("  BUT 'NOT COINCIDENCE' ≠ 'DERIVED':");
        sb.AppendLine("    The relation is real (prediction confirmed), but no");
        sb.AppendLine("    mechanism derives Q=2/3. It sits in the 'real but");
        sb.AppendLine("    unexplained' zone.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Coincidence is DISFAVORED (~10^-4 after look-elsewhere,");
        sb.AppendLine("  plus the 1981 prediction). But the alternative (a hidden");
        sb.AppendLine("  mechanism) has NOT been found. Koide is 'suggestive but unproven'.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. KOIDE IS THE FINAL UNSOLVED CORE OF FLAVOR:");
        sb.AppendLine("    Everything else (G, dim=3, S3, mixing, CP) is characterized.");
        sb.AppendLine("    Koide Q=2/3 is the ONE precise number that resists.");
        sb.AppendLine();
        sb.AppendLine("  2. THE MULTIPLE INTERPRETATIONS ARE CONSISTENT:");
        sb.AppendLine("    - Geometric: 45° = balanced S3 (QG-046).");
        sb.AppendLine("    - Informational: Q = participation ratio = midpoint.");
        sb.AppendLine("    - Amplitude: sqrt(m) = the natural TQM variable.");
        sb.AppendLine("    All agree: Koide is a balance/midpoint. But no mechanism");
        sb.AppendLine("    forces the balance.");
        sb.AppendLine();
        sb.AppendLine("  3. THE NON-POLYNOMIAL NATURE IS KEY:");
        sb.AppendLine("    Koide involves sqrt(m), which is NON-POLYNOMIAL. Standard");
        sb.AppendLine("    spectral theory (trace/det) cannot produce it. The sqrt(m)");
        sb.AppendLine("    is the TQM AMPLITUDE. This suggests the relation lives in");
        sb.AppendLine("    the AMPLITUDE representation, not the mass representation.");
        sb.AppendLine();
        sb.AppendLine("  4. THE RESEARCH TARGET IS NOW ULTRA-PRECISE:");
        sb.AppendLine("    Derive Q = 2/3 from the S¹-winding + U(1)-charge amplitude");
        sb.AppendLine("    architecture. Any candidate must produce sqrt(m) (amplitude)");
        sb.AppendLine("    and the balance (2/3). No known mechanism does both.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    Koide is real (prediction confirmed, ~10^-5), but unexplained.");
        sb.AppendLine("    It is the sharpest single number in the TQM program — and");
        sb.AppendLine("    now, after 57 QG experiments, the cleanest statement of");
        sb.AppendLine("    what remains unknown.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  KOIDE IS REAL BUT UNEXPLAINED — THE SHARPEST OPEN NUMBER");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: WHY Q=2/3: UNKNOWN. No mechanism found.");
        sb.AppendLine("  Q2: Koide is an eigenvalue relation because it uses only");
        sb.AppendLine("      masses (sqrt(m) amplitudes), not mixing angles (QG-056).");
        sb.AppendLine("  Q3: Not a standard spectral sum rule — it is NON-POLYNOMIAL");
        sb.AppendLine("      (sqrt m), beyond trace/det invariants.");
        sb.AppendLine("  Q4: sqrt(m) appears because m = hbar*omega/c^2 -> sqrt(m) ∝");
        sb.AppendLine("      AMPLITUDE (the natural TQM variable, QG-039a).");
        sb.AppendLine("  Q5: YES — Q = sum p_i^2 = participation ratio = 2/3 = the");
        sb.AppendLine("      MIDPOINT of [1/3, 1]. Elegant restatement.");
        sb.AppendLine("  Q6: N_eff = 1/Q = 3/2 (effective generations). Entropy ~0.56,");
        sb.AppendLine("      not extremal. No information optimization.");
        sb.AppendLine("  Q7: No spectral sum rule forces Q=2/3 (non-polynomial).");
        sb.AppendLine("  Q8: S3 gives the FRAMEWORK (singlet/doublet) but not the");
        sb.AppendLine("      balanced value. Textures FIT, not DERIVE.");
        sb.AppendLine("  Q9: Charged leptons only because only they have S¹ winding");
        sb.AppendLine("      + U(1) charge (QG-050). 'Where' explained, 'what' not.");
        sb.AppendLine("  Q10: Not accident (~10^-4 + 1981 prediction), not derived.");
        sb.AppendLine("      REAL but UNEXPLAINED.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK STRUCTURAL CONSTRAINT");
        sb.AppendLine();
        sb.AppendLine("    Koide Q=2/3 is a REAL structural constraint (prediction");
        sb.AppendLine("    confirmed, 10^-5 precision, lepton-specific). It is NOT");
        sb.AppendLine("    coincidence (QG-047).");
        sb.AppendLine();
        sb.AppendLine("    But NO mechanism derives it. Every interpretation");
        sb.AppendLine("    (geometric 45°, informational participation ratio 2/3,");
        sb.AppendLine("    S3 balanced texture) is a RESTATEMENT, not a derivation.");
        sb.AppendLine("    The sqrt(m) (amplitude) structure is natural in TQM, but");
        sb.AppendLine("    the value 2/3 is not.");
        sb.AppendLine();
        sb.AppendLine("    AFTER 57 QG EXPERIMENTS, KOIDE IS THE CLEANEST STATEMENT");
        sb.AppendLine("    OF WHAT TQM DOES NOT YET EXPLAIN: a single dimensionless");
        sb.AppendLine("    number (2/3) at the heart of flavor, real but underived.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 57 experiments.");
        return sb.ToString();
    }
}
