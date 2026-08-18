namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 41 — derive the TRM acceleration law. TRM predicts g_TRM = g_N + √(g_N·a0)/λ. We test whether the
/// √(g_N·a0) term can emerge from Q-event saturation. Key result: saturation gives the regular-core acceleration
/// g_sat = g_N·(1 − e^(−r³/r_c³)), whose correction factor (1 − e^(−r³/r_c³)) ∈ [0,1] — saturation ALWAYS
/// SUPPRESSES gravity (g_sat ≤ g_N, a core at small r) and returns to Newtonian at large r. The MOND term
/// √(g_N·a0) is instead an ENHANCEMENT that grows at large r (√(g_N·a0)/g_N = √(a0/g_N) → ∞ as g_N → 0). The two
/// act in OPPOSITE regimes with OPPOSITE sign, so the √(g_N·a0) term cannot emerge from saturation: it is
/// IMPORTED (a separate MOND ansatz with scale a0). TQM's flat rotation curves come from the log-deficit (α=0)
/// profile, a DIFFERENT derived mechanism — not saturation and not the exact √ form. No new primitives.
/// </summary>
public static class TRMAccelerationOrigin
{
    /// <summary>Newtonian acceleration g_N = GM/r².</summary>
    public static double Newtonian(double G, double M, double r) => G * M / (r * r);

    /// <summary>Saturation correction factor 1 − e^(−r³/r_c³) ∈ [0,1].</summary>
    public static double SaturationFactor(double r, double rc) => 1.0 - Math.Exp(-Math.Pow(r / rc, 3.0));

    /// <summary>Saturation (regular-core) acceleration g_sat = g_N·(1 − e^(−r³/r_c³)).</summary>
    public static double SaturationAcceleration(double G, double M, double r, double rc)
        => Newtonian(G, M, r) * SaturationFactor(r, rc);

    /// <summary>The MOND term √(g_N·a0).</summary>
    public static double MondTerm(double gN, double a0) => Math.Sqrt(gN * a0);

    /// <summary>The full TRM law g_TRM = g_N + √(g_N·a0)/λ.</summary>
    public static double TrmAcceleration(double gN, double a0, double lambda) => gN + MondTerm(gN, a0) / lambda;

    /// <summary>Does saturation reproduce the MOND √ term? No — saturation suppresses (≤ g_N), MOND enhances (≥ g_N).</summary>
    public static bool SaturationReproducesMond() => false;

    /// <summary>Saturation is a SUPPRESSION (core): its correction factor is always ≤ 1.</summary>
    public static bool SaturationIsSuppression(double r, double rc) => SaturationFactor(r, rc) <= 1.0;

    /// <summary>MOND is an ENHANCEMENT: g_TRM is always ≥ g_N.</summary>
    public static bool MondIsEnhancement(double gN, double a0, double lambda) => TrmAcceleration(gN, a0, lambda) >= gN;
}
