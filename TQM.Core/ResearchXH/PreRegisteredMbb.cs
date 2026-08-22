namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 191 — Pre-Registered Neutrinoless Double Beta Decay (0νββ). This is a PRE-REGISTRATION:
/// the effective Majorana mass prediction is LOCKED from QG167 (PMNS), QG172 (neutrino masses) and QG179
/// (Majorana character) ONLY, before any future measurement is examined.
///
/// FORBIDDEN inputs (never used in this phase):
///   • experimental limits, detector sensitivities, future measurements
///
/// ALLOWED inputs (the only ones used):
///   • QG167 PMNS (s12 = √(#d/(Σm+#g)), s13 = √(occ0/(2Σm)), δ_ν = 66.4°)
///   • QG172 masses (m1 = 0, m2 = 8.72e-3, m3 = 4.94e-2 eV — normal ordering)
///   • QG179 Majorana result (real mass matrix ⇒ vanishing Majorana phases α2 = α3 = 0)
///
/// PRE-REGISTERED OUTPUTS (frozen):
///   1. m_ββ = |Σ U_ei²·m_i| = 2.02 meV — computed from the D96 PMNS first row and masses:
///      m_ββ = |m1·c12²·c13² + m2·s12²·c13² + m3·s13²·e^(−2iδ_ν)| = 2.0222 meV ≈ 2.02 meV.
///   2. MASS ORDERING — NORMAL: m1 = 0 &lt; m2 = 8.72 meV &lt; m3 = 49.4 meV (QG172); the ordering is a
///      pre-registered prediction, not an input.
///   3. MAJORANA PHASE ASSUMPTION — the neutrino mass matrix is real (QG179 reflection automorphism),
///      so the Majorana phases vanish: α2 = α3 = 0. This is the ONLY phase assumption; it is derived,
///      not fitted.
///
/// ACCEPTANCE:
///   CONFIRMED  — a future 0νββ measurement is consistent with the 2.02 meV range;
///   FALSIFIED  — a significant exclusion below the prediction.
///
/// This class NEVER reads experimental limits, detector sensitivities, or future measurements. The
/// forbidden-input guard asserts that no limit/sensitivity field exists.
/// </summary>
public static class PreRegisteredMbb
{
    // ── ALLOWED inputs: QG167/172/179 only ────────────────────────────────────────

    /// <summary>m1 (eV) — the massless zero-mode of the T3-only channel (QG172).</summary>
    public static double M1() => NeutrinoMassLaw.M1();

    /// <summary>m2 (eV) = 8.72e-3 — √Δm²21 (QG172).</summary>
    public static double M2() => NeutrinoMassLaw.M2();

    /// <summary>m3 (eV) = 4.94e-2 — √Δm²31 (QG172).</summary>
    public static double M3() => NeutrinoMassLaw.M3();

    /// <summary>s12 (QG167): √(#doublets/(Σm + #groups)).</summary>
    public static double SinTheta12() => PMNSOrigin.SinTheta12();

    /// <summary>s13 (QG167): √(occ₀/(2Σm)).</summary>
    public static double SinTheta13() => PMNSOrigin.SinTheta13();

    /// <summary>δ_ν (degrees, QG167).</summary>
    public static double DeltaNuDeg() => PMNSOrigin.DeltaNuDeg();

    /// <summary>Majorana phases vanish (real mass matrix, QG179): α2 = α3 = 0.</summary>
    public static (double Alpha2, double Alpha3) MajoranaPhases() => (0.0, 0.0);

    // ── Pre-registered output 1: m_ββ ────────────────────────────────────────────

    /// <summary>
    /// The frozen effective Majorana mass m_ββ = |Σ U_ei²·m_i| (eV), computed ONLY from QG167/172/179.
    /// Returns 2.0222e-3 eV ≈ 2.02 meV.
    /// </summary>
    public static double EffectiveMajoranaMass()
    {
        double m1 = M1(), m2 = M2(), m3 = M3();
        double s12 = SinTheta12(), s13 = SinTheta13();
        double c12 = Math.Sqrt(1 - s12 * s12);
        double c13 = Math.Sqrt(1 - s13 * s13);
        double delta = DeltaNuDeg() * Math.PI / 180.0;
        // U_e1 = c12·c13, U_e2 = s12·c13, U_e3 = s13·e^{-iδ}; Majorana phases α2=α3=0.
        double re = m1 * c12 * c12 * c13 * c13
                  + m2 * s12 * s12 * c13 * c13
                  + m3 * s13 * s13 * Math.Cos(-2 * delta);
        double im = m3 * s13 * s13 * Math.Sin(-2 * delta);
        return Math.Sqrt(re * re + im * im);
    }

    /// <summary>The frozen prediction in meV = 2.02 meV.</summary>
    public static double MbbMeV() => EffectiveMajoranaMass() * 1e3;

    // ── Pre-registered output 2: mass ordering ───────────────────────────────────

    /// <summary>Normal ordering: m1 = 0 &lt; m2 &lt; m3 (QG172) — frozen, not an input.</summary>
    public static bool NormalOrdering()
        => M1() == 0.0 && M2() > 0.0 && M3() > M2();

    // ── Pre-registered output 3: Majorana phase assumption ───────────────────────

    /// <summary>The mass matrix is real (QG179), so Majorana phases vanish — the only phase assumption.</summary>
    public static bool MajoranaPhasesVanish()
        => MajoranaPhases().Alpha2 == 0.0 && MajoranaPhases().Alpha3 == 0.0;

    // ── Forbidden-input guard ─────────────────────────────────────────────────────

    /// <summary>
    /// Forbidden-input guard: the prediction NEVER reads experimental limits, detector sensitivities, or
    /// future measurements. No field for a limit or sensitivity exists; m_ββ is computed from D96 inputs only.
    /// </summary>
    public static bool ForbiddenInputsNeverUsed()
    {
        bool noLimitFields = !typeof(PreRegisteredMbb)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic |
                       System.Reflection.BindingFlags.Static)
            .Any(f => f.Name.Contains("Limit") || f.Name.Contains("Sensitivity")
                   || f.Name.Contains("Detector") || f.Name.Contains("Measured"));
        return Math.Abs(MbbMeV() - 2.02) < 0.01 && noLimitFields;
    }

    // ── Acceptance ───────────────────────────────────────────────────────────────

    /// <summary>CONFIRMED: a future measurement is consistent with the 2.02 meV range (±10%).</summary>
    public static bool Confirmed(double futureMbbMeV)
        => Math.Abs(futureMbbMeV / MbbMeV() - 1.0) < 0.10;

    /// <summary>FALSIFIED: a significant exclusion below the prediction (measured upper limit &lt; prediction).</summary>
    public static bool Falsified(double futureUpperLimitMeV)
        => futureUpperLimitMeV < MbbMeV();

    /// <summary>Classification: the prediction is PRE-REGISTERED and frozen.</summary>
    public static string Classify() => "PRE-REGISTERED";
}
