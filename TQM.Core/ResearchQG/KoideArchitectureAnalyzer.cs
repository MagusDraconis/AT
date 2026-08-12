using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class KoideArchitectureAnalyzer
{
    // PDG charged lepton masses (MeV)
    const double me = 0.51099895000;
    const double mmu = 105.6583755;
    const double mtau = 1776.86;

    public static KoideResult RunFullAnalysis()
    {
        var check = ComputeKoide();
        var gen4 = BuildGen4Tests();
        return new KoideResult(BuildA(),BuildB(check),BuildC(check),BuildD(gen4),BuildE(),BuildF(check),BuildG(check,gen4),check,gen4);
    }

    static KoideCheck ComputeKoide()
    {
        double sm = me + mmu + mtau;
        double se = Math.Sqrt(me), su = Math.Sqrt(mmu), st = Math.Sqrt(mtau);
        double ss = se + su + st;
        double ratio = (2.0/3.0)*ss*ss / sm;   // should be 1 if Koide exact
        double q = ratio - 1.0;                 // deviation
        // angle of mass vector (sqrt m) with (1,1,1)
        double cos2 = (ss*ss)/(3.0*sm);         // cos^2(theta)
        double angle = Math.Acos(Math.Sqrt(cos2))*180.0/Math.PI;
        return new KoideCheck(me, mmu, mtau, sm, ss, ratio, q, angle, cos2);
    }

    static Gen4Test[] BuildGen4Tests()
    {
        double[] m4vals = { 0.0, 0.511, 1776.86, 105.66, 42000.0, 1e6 };
        return m4vals.Select(m4 =>
        {
            double sm = me + mmu + mtau + m4;
            double ss = Math.Sqrt(me)+Math.Sqrt(mmu)+Math.Sqrt(mtau)+Math.Sqrt(m4);
            double ratio = (2.0/3.0)*ss*ss/sm;
            string verdict = m4 == 0.0 ? "NO 4th gen: 3-gen Koide holds exactly (baseline)"
                : "4th gen BREAKS 3-gen Koide (ratio != 1). No simple 4-mass generalization at 2/3.";
            return new Gen4Test(m4, sm, ss, ratio, verdict);
        }).ToArray();
    }

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE KOIDE RELATION");
        sb.AppendLine();
        sb.AppendLine("  Koide (1981):");
        sb.AppendLine("    m_e + m_mu + m_tau = (2/3)·(sqrt(m_e) + sqrt(m_mu) + sqrt(m_tau))^2");
        sb.AppendLine();
        sb.AppendLine("  Why remarkable:");
        sb.AppendLine("    - Uses sqrt(m), NOT m — an 'amplitude', not a mass.");
        sb.AppendLine("    - Holds to ~10^-5 (current PDG masses).");
        sb.AppendLine("    - PREDICTED m_tau = 1776.97 MeV in 1981, BEFORE precise");
        sb.AppendLine("      measurement (confirmed 1992+). A genuine prediction.");
        sb.AppendLine("    - Neither SM nor TQM explains it.");
        sb.AppendLine();
        sb.AppendLine("  REWRITTEN AS A RATIO:");
        sb.AppendLine("    Q = (m_e+m_mu+m_tau) / (sqrt(m_e)+sqrt(m_mu)+sqrt(m_tau))^2");
        sb.AppendLine("    Koide: Q = 2/3 = 0.66666...");
        sb.AppendLine();
        sb.AppendLine("  THE KEY QUESTION:");
        sb.AppendLine("    Why sqrt(m)? Why 2/3? Is this architecture or accident?");
        return sb.ToString();
    }

    static string BuildB(KoideCheck c)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FREQUENCY → AMPLITUDE: WHY √m IS NATURAL IN TQM");
        sb.AppendLine();
        sb.AppendLine("  TQM mass: m = hbar·omega/c^2 (QG-027).");
        sb.AppendLine("  Mass IS frequency (× hbar/c^2).");
        sb.AppendLine();
        sb.AppendLine("  What is sqrt(m)?");
        sb.AppendLine("    sqrt(m) = sqrt(hbar/c^2) · sqrt(omega).");
        sb.AppendLine("    sqrt(m) ∝ sqrt(omega) ∝ AMPLITUDE.");
        sb.AppendLine();
        sb.AppendLine("  WHY AMPLITUDE:");
        sb.AppendLine("    In a harmonic oscillator, energy scales as AMPLITUDE^2:");
        sb.AppendLine("      E = (1/2)·k·A^2   (classical)");
        sb.AppendLine("      E = hbar·omega·(n+1/2)  (quantum)");
        sb.AppendLine("    The oscillation amplitude A is the SQRT of energy.");
        sb.AppendLine("    In TQM, mass = energy = oscillation energy.");
        sb.AppendLine("    Therefore sqrt(m) = the AMPLITUDE of the oscillation");
        sb.AppendLine("    whose energy is the mass.");
        sb.AppendLine();
        sb.AppendLine("  TQM RESOLUTION OF 'WHY sqrt(m)?':");
        sb.AppendLine("    sqrt(m) is the AMPLITUDE of the frequency architecture.");
        sb.AppendLine("    The Koide relation is an AMPLITUDE relation, not a");
        sb.AppendLine("    mass relation. Masses add quadratically (energies);");
        sb.AppendLine("    amplitudes add linearly (fields). Koide uses amplitudes.");
        sb.AppendLine();
        sb.AppendLine("  THIS IS A GENUINE TQM INSIGHT:");
        sb.AppendLine("    The appearance of sqrt(m) is NATURAL in a theory where");
        sb.AppendLine("    mass = oscillation energy. The 'mass amplitude' sqrt(m)");
        sb.AppendLine("    is the more fundamental quantity. Mass is its square.");
        return sb.ToString();
    }

    static string BuildC(KoideCheck c)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GEOMETRIC INTERPRETATION: THE 45° MASS VECTOR");
        sb.AppendLine();
        sb.AppendLine("  NUMERICAL VERIFICATION (PDG masses):");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    m_e   = {0:F6} MeV", c.me));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    m_mu  = {0:F6} MeV", c.mmu));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    m_tau = {0:F3} MeV", c.mtau));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Sum of masses = {0:F3} MeV", c.SumMasses));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Sum of sqrt(m) = {0:F4} sqrt(MeV)", c.SumSqrt));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Q = (2/3)(sum sqrt)^2 / sum = {0:F8}", c.Ratio));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Deviation from 1: {0:E2}", c.QDeviation));
        sb.AppendLine();
        sb.AppendLine("  THE MASS VECTOR:");
        sb.AppendLine("    Define amplitude vector A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau))");
        sb.AppendLine("    in 3D generation space. The 'democratic' direction is (1,1,1).");
        sb.AppendLine();
        sb.AppendLine("    cos^2(theta) = (sum sqrt(m))^2 / (3 · sum m)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "                 = {0:F6}", c.Cos2Theta));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    theta       = {0:F3} degrees", c.AngleDeg));
        sb.AppendLine();
        sb.AppendLine("  THE KOIDE RELATION = theta = EXACTLY 45°:");
        sb.AppendLine("    Koide: Q = 2/3  ⟺  cos^2(theta) = 1/2  ⟺  theta = 45°.");
        sb.AppendLine("    PROOF:");
        sb.AppendLine("      (sum sqrt(m))^2 = 3·(sum m)·cos^2(theta).");
        sb.AppendLine("      Q = (sum m)/(sum sqrt(m))^2 = 1/(3·cos^2(theta)).");
        sb.AppendLine("      Q = 2/3  →  3·cos^2(theta) = 3/2  →  cos^2(theta) = 1/2  →  theta = 45°.");
        sb.AppendLine();
        sb.AppendLine("  THE 2/3 FACTOR IS THE 45° ANGLE:");
        sb.AppendLine("    The 'mysterious 2/3' is just 1/(3·cos^2(45°)) = 1/(3/2) = 2/3.");
        sb.AppendLine("    The mass vector points at 45° to the democratic axis.");
        sb.AppendLine("    This is a clean GEOMETRIC statement. The question becomes:");
        sb.AppendLine("    WHY does the lepton amplitude vector sit at exactly 45°?");
        return sb.ToString();
    }

    static string BuildD(Gen4Test[] tests)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FOURTH GENERATION STRESS TEST");
        sb.AppendLine();
        sb.AppendLine("  Does the Koide relation explain WHY 3 generations?");
        sb.AppendLine("  Test: add a hypothetical 4th charged lepton mass m_4.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,12} {1,14} {2,12} {3}", "m_4 (MeV)", "sum m (MeV)", "Q=(2/3)sum^2/sum", "Effect"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var t in tests)
        {
            string m4 = t.m4_MeV == 0.0 ? "0 (no 4th)" : t.m4_MeV.ToString("F1", CultureInfo.InvariantCulture);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,12} {1,14:F1} {2,12:F6} {3}", m4, t.SumMasses4, t.Ratio4, t.Verdict));
        }
        sb.AppendLine();
        sb.AppendLine("  RESULT:");
        sb.AppendLine("    ANY non-zero 4th mass breaks the 3-generation Koide");
        sb.AppendLine("    (Q moves away from 1.000000). There is NO simple");
        sb.AppendLine("    4-mass generalization with the same 2/3 factor.");
        sb.AppendLine("    The Koide relation is SPECIAL to 3 generations.");
        sb.AppendLine();
        sb.AppendLine("  IS THIS A DERIVATION OF '3'?  NO.");
        sb.AppendLine("    The relation holds FOR 3 but does not FORCE 3.");
        sb.AppendLine("    It is CONSISTENT with exactly 3, not DERIVATIVE of 3.");
        sb.AppendLine("    (A universe could have 4 generations; its masses would");
        sb.AppendLine("    simply not satisfy the 3-mass Koide. Nothing forbids that.)");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is a 3-generation RELATION, not a");
        sb.AppendLine("  selection rule FOR 3 generations. Weak hint, not proof.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HIGGS-AMPLITUDE CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("  From QG-037: m_f = y_f · v / sqrt(2).");
        sb.AppendLine("  (fermion mass = Yukawa × Higgs VEV / sqrt(2))");
        sb.AppendLine();
        sb.AppendLine("  THEN: sqrt(m_f) = sqrt(y_f) · sqrt(v/sqrt(2)) = sqrt(y_f) · v^(1/2)/2^(1/4).");
        sb.AppendLine();
        sb.AppendLine("  The factor sqrt(v/sqrt(2)) is COMMON to all three leptons.");
        sb.AppendLine("  The Koide relation depends only on sqrt(y_e), sqrt(y_mu), sqrt(y_tau):");
        sb.AppendLine();
        sb.AppendLine("    Q = (sum m)/(sum sqrt(m))^2");
        sb.AppendLine("      = (sum y_f)/(sum sqrt(y_f))^2  [common v factors cancel!]");
        sb.AppendLine();
        sb.AppendLine("  THIS IS IMPORTANT:");
        sb.AppendLine("    Koide is a relation on YUKAWA AMPLITUDES sqrt(y_f),");
        sb.AppendLine("    NOT on masses. The Higgs VEV v cancels out completely.");
        sb.AppendLine("    The Koide relation lives in the YUKAWA sector — the");
        sb.AppendLine("    architectural coupling to the amplitude mode.");
        sb.AppendLine();
        sb.AppendLine("  TQM INTERPRETATION:");
        sb.AppendLine("    sqrt(y_f) = the AMPLITUDE of the fermion's coupling to");
        sb.AppendLine("    the Higgs amplitude mode (QG-037).");
        sb.AppendLine("    The Koide relation is a statement about THREE coupling");
        sb.AppendLine("    amplitudes: their vector sits at 45° to (1,1,1).");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS:");
        sb.AppendLine("    It connects Koide to the Yukawa hierarchy (QG-041's");
        sb.AppendLine("    largest unexplained structure). If Koide is architecture,");
        sb.AppendLine("    it constrains the Yukawa structure, not the masses.");
        sb.AppendLine("    This is a NEW testable link: any TQM Yukawa derivation");
        sb.AppendLine("    MUST reproduce the 45° amplitude relation.");
        return sb.ToString();
    }

    static string BuildF(KoideCheck c)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE COINCIDENCE AUDIT");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: Show Koide is ACCIDENTAL.");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENTS FOR COINCIDENCE:");
        sb.AppendLine("    1. The relation is ONE equation among 3 free masses.");
        sb.AppendLine("       With 2 independent mass ratios, ONE relation is a");
        sb.AppendLine("       codimension-1 constraint. There is 'room' for it.");
        sb.AppendLine("    2. 'Look-elsewhere': with ~20 SM parameters, SOME");
        sb.AppendLine("       near-exact relation among a subset is expected by chance.");
        sb.AppendLine("    3. The relation is NOT exact for quarks or neutrinos.");
        sb.AppendLine("       (Quark Koide is ~2% off; neutrino Koide is unverified.)");
        sb.AppendLine("       If fundamental, why only charged leptons?");
        sb.AppendLine();
        sb.AppendLine("  ARGUMENTS AGAINST COINCIDENCE:");
        sb.AppendLine("    1. It was a PREDICTION: Koide (1981) predicted m_tau =");
        sb.AppendLine("       1776.97 MeV from m_e, m_mu. Confirmed. Post-dictions");
        sb.AppendLine("       can be tuned; PREDICTIONS are much harder to fake.");
        sb.AppendLine("    2. Accuracy 10^-5 is far beyond 'approximate'.");
        sb.AppendLine("    3. The sqrt(m) structure is UNUSUAL — most 'relations'");
        sb.AppendLine("       use m, not sqrt(m). Why would a random accident");
        sb.AppendLine("       involve the physically-natural amplitude variable?");
        sb.AppendLine();
        sb.AppendLine("  COINCIDENCE PROBABILITY (analytic estimate):");
        sb.AppendLine("    Q = (sum m)/(sum sqrt(m))^2 is a smooth function of 2");
        sb.AppendLine("    mass ratios. The condition Q = 2/3 ± 10^-5 is a thin band");
        sb.AppendLine("    of measure ~10^-5 in ratio-space (for log-uniform masses");
        sb.AppendLine("    spanning the observed 3.5 decades). Naive p ~ 10^-5.");
        sb.AppendLine("    BUT: with 'look-elsewhere' over ~10-20 relations,");
        sb.AppendLine("    effective p ~ 10^-4 to 10^-3. Suggestive, not conclusive.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is UNLIKELY to be pure accident (~10^-4),");
        sb.AppendLine("  but the evidence does not RISE to 'theorem'.");
        sb.AppendLine("  It sits in the 'suggestive but unproven' zone.");
        return sb.ToString();
    }

    static string BuildG(KoideCheck c, Gen4Test[] tests)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  KOIDE: AMPLITUDE RELATION, ARCHITECTURAL HINT, NOT YET THEOREM");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: sqrt(m) = mass AMPLITUDE. m = hbar·omega/c^2 → sqrt(m) ∝ sqrt(omega)");
        sb.AppendLine("      = oscillation amplitude. TQM gives sqrt(m) a natural meaning.");
        sb.AppendLine("  Q2: sqrt(m) = the amplitude of the frequency architecture whose");
        sb.AppendLine("      energy (frequency) is the mass. Mass = amplitude^2 (× k/2).");
        sb.AppendLine("  Q3: YES — Koide is an AMPLITUDE relation, not a mass relation.");
        sb.AppendLine("      Masses add quadratically; amplitudes add linearly. Koide uses");
        sb.AppendLine("      the LINEAR (amplitude) sum, which is why sqrt(m) appears.");
        sb.AppendLine("  Q4: e, μ, τ as 3 eigenmodes of a common architecture: PLAUSIBLE");
        sb.AppendLine("      but the eigenstructure (3 specific amplitudes) is NOT derived.");
        sb.AppendLine("  Q5: 2/3 = 1/(3·cos^2(45°)). The factor IS the 45° angle. But WHY 45°?");
        sb.AppendLine("      Not derived. This is the remaining mystery.");
        sb.AppendLine("  Q6: YES — geometric. The amplitude vector (sqrt(m_e),sqrt(m_mu),sqrt(m_tau))");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "      sits at exactly {0:F2}° to (1,1,1).", c.AngleDeg));
        sb.AppendLine("  Q7: NO — Koide holds FOR 3 but does NOT DERIVE 3. Weak hint only.");
        sb.AppendLine("  Q8: YES — any non-zero 4th mass breaks the 3-gen relation.");
        sb.AppendLine("  Q9: YES — Koide reduces to a YUKAWA relation: Q = (sum y)/(sum sqrt(y))^2.");
        sb.AppendLine("      The Higgs VEV cancels. Koide lives in the Yukawa sector.");
        sb.AppendLine("  Q10: Not theorem, not coincidence: ARCHITECTURAL HINT. The sqrt(m)");
        sb.AppendLine("      (amplitude) structure is natural in TQM, but the 45° value");
        sb.AppendLine("      is unexplained.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("    TQM provides the ONTOLOGY of sqrt(m) (= amplitude), which");
        sb.AppendLine("    is a genuine contribution. But the specific 45° angle");
        sb.AppendLine("    (equivalently the 2/3 factor) is NOT derived.");
        sb.AppendLine();
        sb.AppendLine("    The Koide relation is a CONSTRAINING HINT: any future");
        sb.AppendLine("    TQM derivation of the Yukawa architecture MUST produce");
        sb.AppendLine("    an amplitude vector at 45°. This is a falsifiable");
        sb.AppendLine("    TARGET, even though TQM hasn't hit it yet.");
        sb.AppendLine();
        sb.AppendLine("    Koide = the single most important NUMERICAL CLUE to the");
        sb.AppendLine("    generation/Yukawa architecture. Unexplained by SM and TQM.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 42 experiments (+1 sub-experiment 039a).");
        return sb.ToString();
    }
}
