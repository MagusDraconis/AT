using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class KoideAngleSelectionAnalyzer
{
    public static KAResult RunFullAnalysis()
    {
        var scan = BuildAngleScan();
        var coinc = ComputeCoincidence();
        return new KAResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(),BuildF(scan),BuildG(coinc),BuildH(),BuildI(),scan,coinc);
    }

    static AngleScan[] BuildAngleScan()
    {
        // Scan theta from 0 to 54.74 deg (max hierarchical angle). Q = 1/(3 cos^2).
        // singlet fraction = cos(theta), doublet fraction = sin(theta).
        double[] thetas = { 0.0, 10.0, 20.0, 30.0, 35.264, 45.0, 50.0, 54.736 };
        return thetas.Select(t =>
        {
            double rad = t*Math.PI/180.0;
            double q = 1.0/(3.0*Math.Cos(rad)*Math.Cos(rad));
            double sing = Math.Cos(rad), doub = Math.Sin(rad);
            string note;
            if (t == 0.0) note = "Democratic (m_e=m_mu=m_tau). Q=1/3, not Koide.";
            else if (Math.Abs(t-35.264)<0.01) note = "Magic angle (tetrahedral, 1/sqrt(3)).";
            else if (Math.Abs(t-45.0)<0.01) note = "KOIDE ANGLE: balanced singlet=doublet. Q=2/3.";
            else if (Math.Abs(t-54.736)<0.01) note = "Max hierarchical (one mass dominates). Q=1.";
            else note = "Intermediate.";
            return new AngleScan(t, q, sing, doub, note);
        }).ToArray();
    }

    static CoincidenceEstimate ComputeCoincidence()
    {
        // Deterministic solid-angle computation (no randomness).
        // Amplitude vector on unit sphere, first octant (x,y,z > 0).
        // Koide: Q = 2/3  <=>  x+y+z = sqrt(3/2) = 1.224745.
        // Uniform grid over (alpha, beta) with Jacobian sin(alpha).
        // Count fraction with |x+y+z - sqrt(3/2)| < tol (tol = 0.01, resolvable).
        int n = 800;
        double target = Math.Sqrt(3.0/2.0);
        double tol = 0.01;
        long inBand = 0;
        double totalWeight = 0, bandWeight = 0;
        for (int i = 0; i < n; i++)
        {
            double alpha = (i + 0.5)/n * (Math.PI/2.0);
            for (int j = 0; j < n; j++)
            {
                double beta = (j + 0.5)/n * (Math.PI/2.0);
                double x = Math.Sin(alpha)*Math.Cos(beta);
                double y = Math.Sin(alpha)*Math.Sin(beta);
                double z = Math.Cos(alpha);
                double w = Math.Sin(alpha);   // Jacobian
                double s = x + y + z;
                totalWeight += w;
                if (Math.Abs(s - target) < tol) bandWeight += w;
            }
        }
        double fracAt1eMinus2 = bandWeight/totalWeight;
        // Linear scaling to 1e-5 precision (thin band, smooth density)
        double fracAt1eMinus5 = fracAt1eMinus2 * (1e-5/1e-2);
        double lookElsewhere = fracAt1eMinus5 * 20.0; // ~20 candidate relations
        string assess = fracAt1eMinus5 < 1e-3 ? "COINCIDENCE UNLIKELY (but not impossible after look-elsewhere)"
            : "COINCIDENCE POSSIBLE";
        return new CoincidenceEstimate(fracAt1eMinus2, fracAt1eMinus5, lookElsewhere, assess);
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE 45° PROBLEM");
        sb.AppendLine();
        sb.AppendLine("  Koide (QG-039a): Q = (sum m)/(sum sqrt m)^2 = 2/3 to 10^-5.");
        sb.AppendLine("  QG-046: this is equivalent to a BALANCED S3 decomposition");
        sb.AppendLine("  (|singlet| = |doublet|), which is equivalent to theta = 45°.");
        sb.AppendLine();
        sb.AppendLine("  THE REMAINING MYSTERY: WHY 45°?");
        sb.AppendLine("    45° = arccos(1/sqrt(2)) = the 'balanced' angle.");
        sb.AppendLine("    cos^2(45°) = sin^2(45°) = 1/2.");
        sb.AppendLine("    It is the angle where the democratic (singlet) and");
        sb.AppendLine("    hierarchical (doublet) contributions are EQUAL.");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION:");
        sb.AppendLine("    Is 45° accidental, geometric, symmetry-selected,");
        sb.AppendLine("    stability-selected, or ontologically required?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The angle is GEOMETRICALLY MEANINGFUL (balanced),");
        sb.AppendLine("    but NO selection mechanism that forces 45° is found.");
        sb.AppendLine("    The coincidence probability is ~10^-5 (naive),");
        sb.AppendLine("    ~10^-4 (look-elsewhere). Significant but not decisive.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GEOMETRIC INTERPRETATION");
        sb.AppendLine();
        sb.AppendLine("  THE AMPLITUDE VECTOR AND THE ANGLE RANGE:");
        sb.AppendLine("    A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau)) in the first octant.");
        sb.AppendLine("    Normalize to unit sphere: x^2 + y^2 + z^2 = 1.");
        sb.AppendLine();
        sb.AppendLine("    The angle theta with the democratic axis (1,1,1)/sqrt(3):");
        sb.AppendLine("      theta = 0°:    fully democratic (m_e = m_mu = m_tau).");
        sb.AppendLine("      theta = 54.74°: maximally hierarchical (one mass >> rest).");
        sb.AppendLine("      (54.74° = arccos(1/sqrt(3)) = the 'magic angle').");
        sb.AppendLine();
        sb.AppendLine("  KOIDE AS A SURFACE CONDITION:");
        sb.AppendLine("    On the unit sphere, x^2+y^2+z^2 = 1, so");
        sb.AppendLine("      Q = (sum m)/(sum sqrt m)^2 = 1/(x+y+z)^2.");
        sb.AppendLine("    Koide Q = 2/3  ⟺  1/(x+y+z)^2 = 2/3");
        sb.AppendLine("                ⟺  x+y+z = sqrt(3/2) = 1.224745.");
        sb.AppendLine();
        sb.AppendLine("  THE LEVEL SET x+y+z = 1.2247:");
        sb.AppendLine("    This is a CURVE on the octant sphere (a 'latitude' at 45°).");
        sb.AppendLine("    The leptons sit ON this curve. The curve is where");
        sb.AppendLine("    cos(theta) = sin(theta), i.e., theta = 45°.");
        sb.AppendLine();
        sb.AppendLine("  KEY OBSERVATION:");
        sb.AppendLine("    45° is NOT the midpoint of the allowed range [0°, 54.74°].");
        sb.AppendLine("    (The midpoint is 27.37°.) 45° is at 82% of the way to");
        sb.AppendLine("    maximal hierarchy. So 45° is a SPECIFIC, non-symmetric value.");
        sb.AppendLine("    It is 'balanced' in the S3 sense (singlet=doublet), NOT");
        sb.AppendLine("    in the angle-range sense.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("S3 SYMMETRY-BREAKING ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  EXACT S3 (unbroken):");
        sb.AppendLine("    Full permutation symmetry → m_e = m_mu = m_tau.");
        sb.AppendLine("    Amplitude vector along (1,1,1). theta = 0°.");
        sb.AppendLine("    Q = 1/3 (NOT 2/3). This is the DEMOCRATIC limit.");
        sb.AppendLine();
        sb.AppendLine("  BROKEN S3 (observed):");
        sb.AppendLine("    The mass hierarchy m_e << m_mu << m_tau breaks S3.");
        sb.AppendLine("    The amplitude vector tilts away from (1,1,1).");
        sb.AppendLine("    At theta = 45°, the breaking is 'halfway' — the vector");
        sb.AppendLine("    has equal democratic and hierarchical content.");
        sb.AppendLine();
        sb.AppendLine("  THE 'HALFWAY' PATTERN:");
        sb.AppendLine("    The Koide angle corresponds to S3 broken by EXACTLY the");
        sb.AppendLine("    amount that equalizes singlet and doublet weights.");
        sb.AppendLine("    This is a SPECIFIC breaking pattern, not a generic one.");
        sb.AppendLine("    A generic breaking would land at ANY angle in [0, 54.74°].");
        sb.AppendLine();
        sb.AppendLine("  IS 'HALFWAY' SELECTED?");
        sb.AppendLine("    Candidate: a Z2 symmetry between the singlet and doublet");
        sb.AppendLine("    sectors (swapping democratic ↔ hierarchical) would force");
        sb.AppendLine("    the balanced point (45°). But no such Z2 is evident in TQM.");
        sb.AppendLine("    Candidate: RG flow toward a fixed point at 45°. Unproven.");
        sb.AppendLine("    Neither mechanism is established.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 45° is a SPECIFIC S3-breaking pattern, but the");
        sb.AppendLine("  mechanism that selects 'halfway' (vs 30° or 60°) is UNKNOWN.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ATTRACTOR-BALANCE ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  HYPOTHESIS: 45° = balance point of two competing attractors.");
        sb.AppendLine();
        sb.AppendLine("  SINGLET SECTOR (democratic attractor):");
        sb.AppendLine("    Pulls the amplitude vector toward (1,1,1) — all generations");
        sb.AppendLine("    equal. This is the 'S3-symmetric' attractor.");
        sb.AppendLine();
        sb.AppendLine("  DOUBLET SECTOR (hierarchical attractor):");
        sb.AppendLine("    Pulls toward maximal hierarchy — one generation dominates.");
        sb.AppendLine("    This is the 'S3-broken' attractor.");
        sb.AppendLine();
        sb.AppendLine("  BALANCE AT 45°:");
        sb.AppendLine("    If the two sectors are EQUALLY strong attractors, the");
        sb.AppendLine("    equilibrium would sit at the balanced point (45°), where");
        sb.AppendLine("    neither dominates. This is a PLAUSIBLE mechanism.");
        sb.AppendLine();
        sb.AppendLine("  BUT IT IS SPECULATIVE:");
        sb.AppendLine("    - No TQM dynamics is specified for the generation sector.");
        sb.AppendLine("    - No derivation that singlet and doublet are equal attractors.");
        sb.AppendLine("    - The 'balance' is ASSUMED, not derived.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The attractor-balance mechanism is COHERENT and");
        sb.AppendLine("  could explain 45°, but it is a HYPOTHESIS, not a result.");
        sb.AppendLine("  It requires a TQM generation-sector dynamics that does not");
        sb.AppendLine("  yet exist.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("YUKAWA-SPACE INTERPRETATION");
        sb.AppendLine();
        sb.AppendLine("  From QG-037: m_f = y_f · v / sqrt(2).");
        sb.AppendLine();
        sb.AppendLine("  In YUKAWA space: sqrt(m_f) = sqrt(y_f) · sqrt(v/sqrt(2)).");
        sb.AppendLine("  The factor sqrt(v/sqrt(2)) is COMMON to all 3 leptons.");
        sb.AppendLine("  Koide depends only on sqrt(y_e), sqrt(y_mu), sqrt(y_tau):");
        sb.AppendLine("    Q = (sum m)/(sum sqrt m)^2 = (sum y)/(sum sqrt y)^2.");
        sb.AppendLine();
        sb.AppendLine("  SO KOIDE IS A YUKAWA-SPACE CONSTRAINT:");
        sb.AppendLine("    The Yukawa amplitude vector (sqrt(y_e), sqrt(y_mu), sqrt(y_tau))");
        sb.AppendLine("    also sits at 45° to (1,1,1). The Higgs VEV cancels.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THIS MEANS:");
        sb.AppendLine("    The 45° angle is a property of the YUKAWA ARCHITECTURE");
        sb.AppendLine("    (the couplings to the amplitude mode), NOT of the mass");
        sb.AppendLine("    values directly. The angle encodes the GENERATION MIXING");
        sb.AppendLine("    STRUCTURE (how the 3 generations couple to the Higgs).");
        sb.AppendLine();
        sb.AppendLine("  NUMERICALLY:");
        sb.AppendLine("    sqrt(y_e) = 0.0017, sqrt(y_mu) = 0.0247, sqrt(y_tau) = 0.1011.");
        sb.AppendLine("    (These are proportional to sqrt(m_e), sqrt(m_mu), sqrt(m_tau),");
        sb.AppendLine("    so the angle is IDENTICAL — 45° — by construction.)");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The 45° angle lives in YUKAWA space. It constrains");
        sb.AppendLine("  the GENERATION COUPLING ARCHITECTURE, not the masses per se.");
        sb.AppendLine("  This connects Koide to the Yukawa hierarchy (QG-041) — the");
        sb.AppendLine("  largest unexplained structure. Same mystery, deeper location.");
        return sb.ToString();
    }

    static string BuildF(AngleScan[] scan)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ROBUSTNESS SCAN: Q(θ) AND BALANCE");
        sb.AppendLine();
        sb.AppendLine("  Scan theta over the allowed range [0°, 54.74°]:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,8} {1,10} {2,12} {3,12} {4}", "theta", "Q", "singlet", "doublet", "Note"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var s in scan)
        {
            string note = s.Note.Length > 45 ? s.Note[..42]+"..." : s.Note;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F2} {1,10:F4} {2,12:F4} {3,12:F4} {4}",
                s.ThetaDeg, s.Q, s.SingletFrac, s.DoubletFrac, note));
        }
        sb.AppendLine();
        sb.AppendLine("  OBSERVATIONS:");
        sb.AppendLine("    1. Q(theta) = 1/(3·cos^2(theta)) is a MONOTONIC function:");
        sb.AppendLine("       Q goes from 1/3 (democratic) to 1 (hierarchical).");
        sb.AppendLine("    2. Q = 2/3 occurs at EXACTLY theta = 45°.");
        sb.AppendLine("    3. At 45°: singlet = doublet = 0.7071 (balanced).");
        sb.AppendLine("    4. 45° is the UNIQUE angle where Q = 2/3.");
        sb.AppendLine();
        sb.AppendLine("  IS 45° AN ATTRACTOR OR OPTIMUM OF ANY MEASURE?");
        sb.AppendLine("    - 'Balance' (singlet = doublet) is maximized... no, it's");
        sb.AppendLine("      a specific equality, not a maximum.");
        sb.AppendLine("    - No natural robustness measure has a maximum at 45°.");
        sb.AppendLine("    - The scan shows 45° is SPECIAL (Q=2/3, balanced) but");
        sb.AppendLine("      does NOT identify a mechanism that PREFERS 45°.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 45° is geometrically SPECIAL (balanced), but the");
        sb.AppendLine("  scan finds NO stability/robustness mechanism selecting it.");
        return sb.ToString();
    }

    static string BuildG(CoincidenceEstimate c)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE COINCIDENCE REVIEW (deterministic)");
        sb.AppendLine();
        sb.AppendLine("  QUESTION: how likely is a near-perfect Koide by CHANCE?");
        sb.AppendLine();
        sb.AppendLine("  METHOD: deterministic solid-angle scan (no randomness).");
        sb.AppendLine("    Amplitude vector on the unit sphere (first octant).");
        sb.AppendLine("    Koide condition: x+y+z = sqrt(3/2) = 1.224745.");
        sb.AppendLine("    Measure the fraction of directions within tolerance.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Fraction within delta=0.01:  {0:F5}", c.FractionAt1eMinus2));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Scaled to delta=1e-5:       {0:E2}", c.ScaledTo1eMinus5));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Look-elsewhere (x20):       {0:E2}", c.LookElsewhere));
        sb.AppendLine("    " + c.Assessment);
        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION STATUS (decisive):");
        sb.AppendLine("    Koide (1981) PREDICTED m_tau = 1776.97 MeV from m_e, m_mu,");
        sb.AppendLine("    BEFORE precise measurement. Confirmed (1992+).");
        sb.AppendLine("    A post-diction can be tuned; a PREDICTION cannot.");
        sb.AppendLine("    This dramatically reduces the coincidence probability —");
        sb.AppendLine("    the relation was derived and THEN verified.");
        sb.AppendLine();
        sb.AppendLine("  ASSESSMENT:");
        sb.AppendLine("    Naive coincidence: ~10^-5. Look-elsewhere: ~10^-4.");
        sb.AppendLine("    Prediction status: much more significant (hard to quantify,");
        sb.AppendLine("    but a successful 1981 prediction is strong evidence).");
        sb.AppendLine("    Verdict: UNLIKELY to be pure accident, but NOT a theorem.");
        sb.AppendLine("    The 45° angle is 'suggestive but unproven'.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. KOIDE IS THE SHARPEST UNEXPLAINED NUMBER IN TQM:");
        sb.AppendLine("    More precise (10^-5) than any other un-derived relation.");
        sb.AppendLine("    It is a YUKAWA-SPACE constraint (generation coupling).");
        sb.AppendLine();
        sb.AppendLine("  2. THE RESEARCH TARGET IS NOW PRECISE:");
        sb.AppendLine("    To derive Koide, TQM must derive WHY the generation");
        sb.AppendLine("    Yukawa amplitudes sit at exactly 45° (balanced S3).");
        sb.AppendLine("    Candidate mechanisms (all unproven):");
        sb.AppendLine("      (a) Z2 symmetry between singlet and doublet sectors.");
        sb.AppendLine("      (b) RG fixed point at the balanced configuration.");
        sb.AppendLine("      (c) Attractor balance of two competing sectors.");
        sb.AppendLine();
        sb.AppendLine("  3. THE DEEPER CONNECTION:");
        sb.AppendLine("    Koide (Yukawa) + generations (excitation levels) + S3");
        sb.AppendLine("    (permutation) + 45° (balance) form a COHERENT cluster.");
        sb.AppendLine("    They all point to a generation ARCHITECTURE that TQM");
        sb.AppendLine("    has not yet specified. This is the next frontier.");
        sb.AppendLine();
        sb.AppendLine("  4. FALSIFIABILITY IS STRONG:");
        sb.AppendLine("    - Neutrino-Koide (QG-046): testable prediction.");
        sb.AppendLine("    - Quark Koide: holds only ~2% — a WEAKENED version.");
        sb.AppendLine("    - If the mechanism is S3-symmetry, quarks should follow");
        sb.AppendLine("      a RELATED (but different) relation. The 2% deviation");
        sb.AppendLine("      is itself a constraint on the mechanism.");
        sb.AppendLine();
        sb.AppendLine("  5. THE HONEST POSITION:");
        sb.AppendLine("    45° is GEOMETRICALLY meaningful (balanced S3) but");
        sb.AppendLine("    MECHANISTICALLY unexplained. TQM has restated the");
        sb.AppendLine("    mystery precisely; it has not solved it.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  45°: GEOMETRICALLY MEANINGFUL, MECHANISTICALLY UNEXPLAINED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: The amplitude vector sits at 45° because the S3 singlet");
        sb.AppendLine("      and doublet are equal. This IS the 45° (cos^2=1/2).");
        sb.AppendLine("  Q2: Singlet = doublet is OBSERVED (exact to 10^-5). WHY they");
        sb.AppendLine("      are equal is UNEXPLAINED.");
        sb.AppendLine("  Q3: 45° from symmetry breaking: PLAUSIBLE (S3 broken halfway),");
        sb.AppendLine("      but 'halfway' is not derived.");
        sb.AppendLine("  Q4: 45° from stability: NO stability mechanism found.");
        sb.AppendLine("  Q5: 45° as attractor fixed point: HYPOTHESIS, unproven.");
        sb.AppendLine("  Q6: 45° maximizes persistence/robustness: NO evidence.");
        sb.AppendLine("  Q7: 45° from topology: NO topological derivation found.");
        sb.AppendLine("  Q8: Other angles (30°, 35.26°, 54.74°, 60°) would give");
        sb.AppendLine("      DIFFERENT Q values and could still support matter");
        sb.AppendLine("      (no obvious instability). 45° is not uniquely required.");
        sb.AppendLine("  Q9: 45° IS a Yukawa-space constraint (generation coupling),");
        sb.AppendLine("      connected to the Yukawa hierarchy. Same mystery, deeper.");
        sb.AppendLine("  Q10: 45° CANNOT be derived without inserting Koide by hand.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK PREFERENCE");
        sb.AppendLine();
        sb.AppendLine("    The 45° angle is GEOMETRICALLY meaningful (balanced S3");
        sb.AppendLine("    decomposition, the unique angle where Q = 2/3).");
        sb.AppendLine("    Coincidence probability: ~10^-5 (naive), ~10^-4 (look-");
        sb.AppendLine("    elsewhere), and the 1981 PREDICTION status makes it");
        sb.AppendLine("    unlikely to be pure accident.");
        sb.AppendLine();
        sb.AppendLine("    BUT: NO selection mechanism (symmetry, stability, attractor)");
        sb.AppendLine("    is established. The angle is RESTATED precisely, not DERIVED.");
        sb.AppendLine();
        sb.AppendLine("    THE DEEPEST UNRESOLVED NUMBER IN TQM:");
        sb.AppendLine("    Koide 45° = the sharpest unexplained relation in physics.");
        sb.AppendLine("    It is the next frontier: a generation architecture that");
        sb.AppendLine("    would explain the balanced S3 coupling. Unreached.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 47 experiments.");
        return sb.ToString();
    }
}
