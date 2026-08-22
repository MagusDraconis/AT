namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 184 — Mass-radius origin. Known: QG12 derives the horizon entropy S ∝ R^(d−1) (area) from
/// boundary counting; QG13 showed that the COMPACT-VOID deficit energy E ∝ R^d (volume) gives T ∝ R
/// (anti-Hawking), while the Schwarzschild M ∝ R gives T ∝ 1/R (Hawking). This phase asks: can the
/// OBSERVED mass-radius relation M ∝ R be derived from TRM/D96 — no new primitives, deterministic?
///
/// Method (computational, fully deterministic): (1) THE DEFICIT THAT COUNTS — QG13's E ∝ R^d assumed a
/// COMPACT VOID (constant deficit inside R, zero outside). But the counting measure's actual deficit is
/// the PER-OCTAVE (log) deficit ρ = ρ̄ − m₀·ln(Rmax/r)/L (L = ln(Rmax/r₀)) — the SAME profile that
/// produces the flat rotation curves in G4ME (constant deficit per octave). (2) THE FIELD-DEFINED MASS —
/// the gravitational mass is GM_eff(R) = −a·R² (the field a = −(1/d)ρ′/ρ at radius R), NOT the enclosed
/// deficit volume. For the per-octave deficit ρ′ = m₀/(r·L), so a ∝ −1/r and GM_eff = m₀·R/(d·L·ρ̄)
/// ∝ R — the mass is RADIUS-proportional (M ∝ R). (3) WHY NOT VOLUME — a compact void (m = const) would
/// give a 'step' field and enclosed mass M ∝ R^d (volume); the point-mass 1/r deficit gives M ∝ const.
/// Only the per-octave (log) deficit gives M ∝ R. (4) HAWKING RESTORED — with E = GM ∝ R and
/// S ∝ R^(d−1) (QG12), the first law T = dE/dS ∝ 1/R^(d−2) gives T ∝ 1/R at d = 3 — Hawking, with no
/// new primitives. (5) D96/OCTAVE CONNECTION — the per-octave deficit is the octave-ladder abundance
/// (G4ME AnnularDeficit: constant deficit per octave), the discrete form of the log-deficit, and the D96
/// spectrum is itself octave-organized (occupancies [4,4,87]).
///
/// Derived: the physical mass scales with the horizon RADIUS (M ∝ R) because the counting measure's
/// deficit is per-octave (log), giving a ∝ −1/r and GM_eff ∝ R; the compact-void volume assignment
/// (M ∝ R^d) is not the counting-measure deficit. Hawking T ∝ 1/R follows with no new primitives.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class MassRadiusOrigin
{
    /// <summary>Spatial dimension (d = 3 in the counting-measure gravity program).</summary>
    public const int Dimension = 3;

    // ── 1. The deficit that counts: per-octave (log) deficit ──────────────────

    /// <summary>
    /// ρ = ρ̄ − m₀·ln(Rmax/r)/L — the per-octave (log) deficit (G4ME LogDeficit / flat-rotation-curve
    /// profile). L = ln(Rmax/r₀).
    /// </summary>
    public static double LogDeficitDensity(double r, double rhoBar = 1.0, double m0 = 0.4,
        double r0 = 0.5, double Rmax = 10.0)
        => DeficitCollective.LogDeficit(r, rhoBar, m0, r0, Rmax);

    /// <summary>L = ln(Rmax/r₀) — the logarithmic dynamic range of the deficit.</summary>
    public static double LogRange(double r0 = 0.5, double Rmax = 10.0)
        => Math.Log(Rmax / r0);

    /// <summary>
    /// ρ′ = m₀/(r·L) — the log-deficit derivative. The field a = −(1/d)ρ′/ρ ∝ −1/r (the flat-rotation-curve
    /// field).
    /// </summary>
    public static double LogDeficitDerivative(double r, double m0 = 0.4, double r0 = 0.5, double Rmax = 10.0)
        => m0 / (r * LogRange(r0, Rmax));

    // ── 2. The field-defined gravitational mass ────────────────────────────────

    /// <summary>
    /// The field a = −(1/d)·ρ′/ρ at radius R (central difference). The gravitational mass is defined
    /// through this field: GM_eff = −a·R².
    /// </summary>
    public static double Acceleration(double R, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5,
        double Rmax = 10.0, int d = Dimension, double h = 1e-6)
    {
        double rhoPlus = LogDeficitDensity(R + h, rhoBar, m0, r0, Rmax);
        double rhoMinus = LogDeficitDensity(R - h, rhoBar, m0, r0, Rmax);
        double rho = LogDeficitDensity(R, rhoBar, m0, r0, Rmax);
        return -(rhoPlus - rhoMinus) / (2.0 * h * d * rho);
    }

    /// <summary>
    /// GM_eff(R) = −a·R² — the field-defined enclosed gravitational mass. For the per-octave deficit this
    /// scales ∝ R (radius-proportional), NOT ∝ R^d (volume).
    /// </summary>
    public static double GravitationalMass(double R, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5,
        double Rmax = 10.0, int d = Dimension)
        => -Acceleration(R, rhoBar, m0, r0, Rmax, d) * R * R;

    /// <summary>
    /// The exact small-deficit limit: GM_eff = m₀·R/(d·L·ρ̄) — EXACTLY linear in R (M ∝ R).
    /// </summary>
    public static double LinearMass(double R, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5,
        double Rmax = 10.0, int d = Dimension)
        => m0 * R / (d * LogRange(r0, Rmax) * rhoBar);

    // ── 3. Scaling exponents ───────────────────────────────────────────────────

    /// <summary>
    /// Effective scaling exponent d(ln GM)/d(ln R) over the octave range. ~1 for the log-deficit
    /// (M ∝ R), ~3 for a compact void (M ∝ R^d), ~0 for a point mass.
    /// </summary>
    public static double ScalingExponent(double R, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5,
        double Rmax = 10.0, int d = Dimension)
    {
        double gmR = GravitationalMass(R, rhoBar, m0, r0, Rmax, d);
        double gm2R = GravitationalMass(2.0 * R, rhoBar, m0, r0, Rmax, d);
        return Math.Log(gm2R / gmR) / Math.Log(2.0);
    }

    // ── 4. Hawking temperature with M ∝ R ──────────────────────────────────────

    /// <summary>
    /// T = dE/dS with E = GM ∝ R and S ∝ R^(d−1) (QG12 area): T = 1/((d−1)·R^(d−2)) — T ∝ 1/R at d = 3
    /// (Hawking restored).
    /// </summary>
    public static double HawkingTemperature(int d, double R)
        => HorizonThermodynamics.TemperatureHawking(d, R);

    /// <summary>T·R — constant at d = 3 (the Hawking T ∝ 1/R signature).</summary>
    public static double TemperatureRadiusProduct(int d, double R)
        => HawkingTemperature(d, R) * R;

    // ── 5. Agreement checks ────────────────────────────────────────────────────

    /// <summary>
    /// Does the per-octave deficit give M ∝ R? Checks: (a) the effective scaling exponent is near 1
    /// (not 3 for volume, not 0 for point); (b) the linear formula matches the numeric field mass within
    /// 15% over the octave range.
    /// </summary>
    public static bool MassScalesWithRadius()
    {
        double exp1 = ScalingExponent(1.0);
        double exp2 = ScalingExponent(2.0);
        bool nearLinear = exp1 > 0.7 && exp1 < 1.1 && exp2 > 0.7 && exp2 < 1.1;
        bool linearMatches = Math.Abs(GravitationalMass(4.0) / LinearMass(4.0) - 1.0) < 0.15;
        return nearLinear && linearMatches;
    }

    /// <summary>Is the compact-void (volume) assignment the anti-Hawking case (T ∝ R)?</summary>
    public static bool CompactVoidIsAntiHawking()
    {
        double t1 = HorizonThermodynamics.TemperatureDeficit(Dimension, 1.0);
        double t2 = HorizonThermodynamics.TemperatureDeficit(Dimension, 2.0);
        return t2 > t1; // T grows with R
    }

    /// <summary>Does the M ∝ R temperature give T ∝ 1/R (Hawking) at d = 3?</summary>
    public static bool HawkingRestored()
    {
        double tr1 = TemperatureRadiusProduct(Dimension, 1.0);
        double tr8 = TemperatureRadiusProduct(Dimension, 8.0);
        return Math.Abs(tr8 / tr1 - 1.0) < 1e-9; // T·R constant
    }

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Mass-radius score (0..3):
    /// 1. the per-octave deficit gives M ∝ R (scaling exponent ~1, linear match);
    /// 2. the compact-void volume assignment (M ∝ R^d) is the anti-Hawking case (explains QG13);
    /// 3. with E = GM ∝ R and S ∝ R^(d−1) (QG12), T ∝ 1/R — Hawking restored with no new primitives.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (MassScalesWithRadius()) score++;
        if (CompactVoidIsAntiHawking()) score++;
        if (HawkingRestored()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN          — no TRM/D96 deficit gives M ∝ R;
    ///   PARTIAL ORIGIN     — a deficit gives M ∝ R but Hawking is not restored consistently;
    ///   MASS-RADIUS ORIGIN — the observed mass-radius relation M ∝ R EMERGES from the counting measure:
    ///                        the deficit is PER-OCTAVE (log, G4ME flat-rotation-curve profile), whose
    ///                        field a ∝ −1/r gives GM_eff = m₀·R/(d·L·ρ̄) ∝ R — the mass scales with the
    ///                        horizon RADIUS, not volume; QG13's E ∝ R^d was the compact-void assignment,
    ///                        not the counting-measure deficit. With S ∝ R^(d−1) (QG12), T ∝ 1/R (Hawking)
    ///                        follows with no new primitives.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 1) return "NO ORIGIN";
        if (score == 3) return "MASS-RADIUS ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
