using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class RAREvolutionPrecisionAnalyzer
{
    // Planck 2018 flat LambdaCDM
    const double H0_kmsMpc = 67.4;      // km/s/Mpc
    const double OmM = 0.315;
    const double OmL = 0.685;
    const double c_mps = 2.99792458e8;  // m/s
    const double Mpc_m = 3.08567758e22; // meters per Mpc
    const double twoPi = 6.283185307179586;

    public static RPEAResult RunFullAnalysis()
    {
        double H0_s = H0_kmsMpc * 1000.0 / Mpc_m;   // H0 in s^-1
        double g0 = c_mps * H0_s / twoPi;           // g-dagger(0) in m/s^2
        double[] zs = { 0.0, 0.25, 0.5, 0.75, 1.0, 1.5, 2.0, 3.0, 4.0 };
        var pts = zs.Select(z =>
        {
            double Hz = H0_kmsMpc * Math.Sqrt(OmM*Math.Pow(1+z,3) + OmL);
            double gz = c_mps * (Hz*1000.0/Mpc_m) / twoPi;
            return new RARPoint(z, Hz, gz, gz/g0);
        }).ToArray();
        double maxSep = pts.Max(p => p.RatioToG0) - 1.0;
        return new RPEAResult(BuildA(),BuildB(),BuildC(pts),BuildD(),BuildE(),BuildF(),BuildG(pts,maxSep),BuildH(),BuildI(),pts,g0,maxSep);
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RAR PREDICTION RECAP");
        sb.AppendLine();
        sb.AppendLine("  TQM (DATA-004): g† = c·H₀/(2π) — the RAR acceleration scale.");
        sb.AppendLine("  QG-069: the EVOLVING form g†(z) = c·H(z)/(2π) is the #1 test.");
        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION (to quantify):");
        sb.AppendLine("    g†(z) = (c/2π)·H₀·sqrt(Ωm·(1+z)³ + ΩΛ)  [flat ΛCDM H(z)]");
        sb.AppendLine("    = g†(0) · sqrt(Ωm·(1+z)³ + ΩΛ).");
        sb.AppendLine();
        sb.AppendLine("  THE PHYSICAL CLAIM:");
        sb.AppendLine("    The RAR scale g† is NOT constant — it grows with z because");
        sb.AppendLine("    the Hubble parameter H(z) grows. MOND predicts a CONSTANT");
        sb.AppendLine("    a₀ ≈ 1.2e-10 m/s² (no evolution). This is the discriminator.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    g† grows ~(1+z)^(3/2) at high z (matter-dominated). At z=3,");
        sb.AppendLine("    g† ≈ 2.8× its z=0 value. This is a LARGE, observable");
        sb.AppendLine("    signature — IF the RAR can be measured at high z.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COSMOLOGICAL EVOLUTION MODEL (H(z))");
        sb.AppendLine();
        sb.AppendLine("  Parameters (Planck 2018, flat ΛCDM):");
        sb.AppendLine("    H₀ = 67.4 km/s/Mpc,  Ωm = 0.315,  ΩΛ = 0.685.");
        sb.AppendLine();
        sb.AppendLine("  H(z) = H₀ · sqrt(Ωm·(1+z)³ + ΩΛ).");
        sb.AppendLine("    - z=0: H = H₀ (dominated by Λ, 0.685).");
        sb.AppendLine("    - z>>1: H → H₀·sqrt(Ωm)·(1+z)^(3/2) (matter-dominated).");
        sb.AppendLine("    - So g†(z) ∝ (1+z)^(3/2) at high z — a clean power-law growth.");
        sb.AppendLine();
        sb.AppendLine("  g†(z) = (c/2π)·H(z):");
        sb.AppendLine("    - g†(0) = c·H₀/(2π).");
        sb.AppendLine("    - g†(z) / g†(0) = sqrt(Ωm·(1+z)³ + ΩΛ).");
        sb.AppendLine("    - This RATIO is independent of H₀ and c — it is a pure");
        sb.AppendLine("      function of z and the density parameters. Clean and");
        sb.AppendLine("      robust (no distance-ladder dependence).");
        sb.AppendLine();
        sb.AppendLine("  KEY ADVANTAGE (Q8): the RATIO g†(z)/g†(0) depends ONLY on");
        sb.AppendLine("  Ωm (and ΩΛ via flatness). It does NOT depend on H₀ (the");
        sb.AppendLine("  Hubble tension does NOT affect the evolution prediction).");
        return sb.ToString();
    }

    static string BuildC(RARPoint[] pts)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PREDICTED g†(z) TABLE (NUMERICAL)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,6} {1,12} {2,14} {3,12}", "z", "H (km/s/Mpc)", "g† (m/s²)", "g†/g†(0)"));
        sb.AppendLine("  " + new string('-', 55));
        foreach (var p in pts)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,6:F2} {1,12:F2} {2,14:E3} {3,12:F3}",
                p.Redshift, p.H_kmsMpc, p.Gdagger_mps2, p.RatioToG0));
        }
        sb.AppendLine();
        sb.AppendLine("  THE OBSERVABLE SIGNATURE:");
        sb.AppendLine("    g† grows monotonically: ×1.00 (z=0) → ×1.32 (z=0.5) →");
        sb.AppendLine("    ×1.79 (z=1) → ×3.03 (z=2) → ×4.57 (z=3).");
        sb.AppendLine("    At z=3, g† is 4.6× its local value — a HUGE, unmistakable");
        sb.AppendLine("    signal vs a constant (MOND) a₀.");
        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION TO TEST (Q1, Q10):");
        sb.AppendLine("    Astronomers should measure g†(z)/g†(0) = sqrt(0.315·(1+z)³");
        sb.AppendLine("    + 0.685). ANY flat/constant g†(z) KILLS TQM (favors MOND).");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MOND COMPARISON: CONSTANT a₀ vs EVOLVING g†");
        sb.AppendLine();
        sb.AppendLine("  MOND: a₀ = CONSTANT ≈ 1.2e-10 m/s² (no evolution).");
        sb.AppendLine("    - a₀(z) = a₀(0) for ALL z. Flat line.");
        sb.AppendLine("  TQM: g†(z) = c·H(z)/2π (grows with z).");
        sb.AppendLine("    - g†(z)/g†(0) = sqrt(0.315(1+z)³ + 0.685). Rising curve.");
        sb.AppendLine();
        sb.AppendLine("  THE SEPARATION (Q3):");
        sb.AppendLine("    - At z=1: TQM g† = 1.79× local; MOND = 1.00×. (79% difference).");
        sb.AppendLine("    - At z=2: TQM g† = 3.03× local; MOND = 1.00×. (203% difference).");
        sb.AppendLine("    - At z=3: TQM g† = 4.57× local; MOND = 1.00×. (357% difference).");
        sb.AppendLine("    The separation is O(1) at z≥1 — LARGE, not a subtle effect.");
        sb.AppendLine();
        sb.AppendLine("  REQUIRED PRECISION (Q3):");
        sb.AppendLine("    To distinguish 79% (z=1) at 3σ, need ~26% precision on g†.");
        sb.AppendLine("    To distinguish 203% (z=2) at 3σ, need ~68% precision.");
        sb.AppendLine("    Even MODEST precision (~30%) suffices at z≥1. This is FEASIBLE.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The MOND/TQM separation is O(1) at z≥1 — easily");
        sb.AppendLine("  resolvable with near-term data. The evolution is NOT subtle.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ΛCDM COMPARISON: NO RAR AT ALL");
        sb.AppendLine();
        sb.AppendLine("  ΛCDM makes NO prediction of a universal RAR acceleration scale.");
        sb.AppendLine("    - The RAR (Milgrom) is an EMPIRICAL relation, not a ΛCDM");
        sb.AppendLine("      prediction. ΛCDM explains rotation curves via dark matter");
        sb.AppendLine("      halos (no g† needed).");
        sb.AppendLine("    - So ΛCDM predicts: NO universal g†, NO g† evolution.");
        sb.AppendLine("    - The RAR itself is a PHENOMENOLOGICAL observation that ΛCDM");
        sb.AppendLine("      must REPRODUCE via halo physics (not a fundamental scale).");
        sb.AppendLine();
        sb.AppendLine("  THE THREE-WAY CONTRAST:");
        sb.AppendLine("    - MOND: g† = constant a₀ (a fundamental scale, no evolution).");
        sb.AppendLine("    - TQM: g† = c·H(z)/2π (a fundamental scale, EVOLVING).");
        sb.AppendLine("    - ΛCDM: no fundamental g† (RAR is emergent halo physics).");
        sb.AppendLine();
        sb.AppendLine("  WHY THE EVOLUTION TEST IS DECISIVE:");
        sb.AppendLine("    - If g† evolves as c·H(z)/2π → TQM wins (ΛCDM has no");
        sb.AppendLine("      mechanism, MOND is constant).");
        sb.AppendLine("    - If g† is constant → MOND wins (TQM's evolution fails).");
        sb.AppendLine("    - If no universal g† at all → ΛCDM wins (RAR is not fundamental).");
        sb.AppendLine("    Three distinct outcomes, one measurement. Clean science.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The g†(z) test is a THREE-WAY discriminator.");
        sb.AppendLine("  It is the sharpest single test in the TQM program.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OBSERVATIONAL FEASIBILITY");
        sb.AppendLine();
        sb.AppendLine("  The RAR requires galaxy rotation curves + baryonic mass:");
        sb.AppendLine("    g_obs (total) vs g_bar (baryonic), and the acceleration");
        sb.AppendLine("    scale g† where the relation turns over.");
        sb.AppendLine();
        sb.AppendLine("  EXISTING / NEAR-TERM DATASETS:");
        sb.AppendLine("    - SPARC (z≈0): 175 galaxies. CONFIRMED g† locally.");
        sb.AppendLine("    - KMOS3D (z≈0.7-2.7): ~700 galaxies, Hα rotation curves.");
        sb.AppendLine("      ARCHIVAL. Can measure g† at z~1-2. THE KEY DATASET.");
        sb.AppendLine("    - Euclid / Rubin (future): huge samples, but galaxies are");
        sb.AppendLine("      more disturbed at high z (mergers) — harder RAR extraction.");
        sb.AppendLine("    - DESI: spectroscopic, but no resolved rotation curves.");
        sb.AppendLine();
        sb.AppendLine("  THE DOMINANT UNCERTAINTIES (Q9):");
        sb.AppendLine("    - Stellar mass-to-light ratio (M*/L): the largest systematic.");
        sb.AppendLine("    - Baryonic mass modeling (gas, stars).");
        sb.AppendLine("    - Inclination, beam-smearing (spatial resolution).");
        sb.AppendLine("    - High-z galaxies are clumpy/merging → disturbed dynamics.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: KMOS3D archival data is the BEST near-term opportunity.");
        sb.AppendLine("  High-z RAR is harder (disturbed galaxies) but the O(1) signal");
        sb.AppendLine("  (g† ×2-3) outweighs the increased scatter.");
        return sb.ToString();
    }

    static string BuildG(RARPoint[] pts, double maxSep)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FALSIFICATION THRESHOLD ANALYSIS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    Maximum separation from constant: {0:F0}% at z≈3", maxSep*100));
        sb.AppendLine();
        sb.AppendLine("  WHAT WOULD FALSIFY TQM (the smallest rejection):");
        sb.AppendLine("    Measure g† at z≈1 with ~25% precision.");
        sb.AppendLine("    - TQM predicts g†(1) = 1.79× g†(0).");
        sb.AppendLine("    - If g†(1) = 1.00× g†(0) ± 0.25 (no evolution at 3σ),");
        sb.AppendLine("      TQM's evolving RAR is FALSIFIED (MOND constant wins).");
        sb.AppendLine();
        sb.AppendLine("  THE DECISIVE MEASUREMENT:");
        sb.AppendLine("    The RATIO R(z) = g†(z)/g†(0) = sqrt(0.315(1+z)³+0.685).");
        sb.AppendLine("    - R(1) = 1.79, R(2) = 3.03, R(3) = 4.57.");
        sb.AppendLine("    - Measure R at TWO redshifts (e.g., z≈1 and z≈2).");
        sb.AppendLine("    - If R(z) grows as predicted → TQM confirmed.");
        sb.AppendLine("    - If R(z) = 1 (flat) → TQM falsified, MOND favored.");
        sb.AppendLine();
        sb.AppendLine("  ROBUSTNESS (Q8): the RATIO R(z) is independent of H₀ (the");
        sb.AppendLine("  Hubble tension does NOT affect it). It depends only on Ωm.");
        sb.AppendLine("  A measurement of R(z) is therefore a CLEAN test, immune to");
        sb.AppendLine("  the H₀ controversy.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The falsification threshold is LOW (need ~15% precision");
        sb.AppendLine("  at z≈1, or ~30% at z≈2). This is achievable with KMOS3D.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EXPERIMENTAL ROADMAP (PRECISION)");
        sb.AppendLine();
        sb.AppendLine("  STEP 1 (now, archival): KMOS3D high-z rotation curves.");
        sb.AppendLine("    - Extract g† at z≈1-2. Target precision ~20-30%.");
        sb.AppendLine("    - This ALONE can distinguish evolving vs constant g† (O(1)");
        sb.AppendLine("      signal at z≥1).");
        sb.AppendLine();
        sb.AppendLine("  STEP 2 (near-term): improve M*/L systematics.");
        sb.AppendLine("    - The dominant uncertainty is stellar mass. Better SED fitting");
        sb.AppendLine("      and gas-mass modeling reduce the g† error.");
        sb.AppendLine();
        sb.AppendLine("  STEP 3 (Euclid/Rubin, future): larger high-z samples.");
        sb.AppendLine("    - Improve precision to ~10%, test the (1+z)^(3/2) power law");
        sb.AppendLine("      shape, not just the monotonic growth.");
        sb.AppendLine();
        sb.AppendLine("  THE KILL-SHOT MEASUREMENT:");
        sb.AppendLine("    R(z) = g†(z)/g†(0) at z≈1 and z≈2, to ~15% precision.");
        sb.AppendLine("    This is the single most decisive test: it distinguishes");
        sb.AppendLine("    TQM (rising) from MOND (flat) from ΛCDM (no scale).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The roadmap is FEASIBLE now (archival) and decisive");
        sb.AppendLine("  with near-term data. g†(z) is immediately testable.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  g†(z) IS IMMEDIATELY TESTABLE (C→D) — A DECISIVE THREE-WAY TEST");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: g†(z) = (c/2π)·H(z); g†(1)=1.79×g†(0), g†(2)=3.03×, g†(3)=4.57×.");
        sb.AppendLine("  Q2: STRONG departure — grows (1+z)^(3/2) at high z (×4.6 at z=3).");
        sb.AppendLine("  Q3: ~25% precision at z=1 suffices (3σ). ~60% at z=2.");
        sb.AppendLine("  Q4: z≈1-2 gives maximum discrimination per unit precision.");
        sb.AppendLine("  Q5: YES — KMOS3D archival data can test it NOW.");
        sb.AppendLine("  Q6: TQM (rising) vs MOND (flat) vs ΛCDM (no scale). Three-way.");
        sb.AppendLine("  Q7: Residuals = g†(z) - constant = (c/2π)(H(z)-H₀) — grows with z.");
        sb.AppendLine("  Q8: The RATIO R(z) is H₀-INDEPENDENT (depends only on Ωm). Robust.");
        sb.AppendLine("  Q9: M*/L (stellar mass) dominates the uncertainty.");
        sb.AppendLine("  Q10: g†(z)=constant at z≈1 (flat, no evolution) FALSIFIES TQM.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C — STRONGLY TESTABLE (bordering D)");
        sb.AppendLine();
        sb.AppendLine("    The g†(z) prediction is QUANTIFIED and IMMEDIATELY TESTABLE:");
        sb.AppendLine("      g†(z)/g†(0) = sqrt(0.315(1+z)³ + 0.685).");
        sb.AppendLine("    - O(1) signal (×1.8 at z=1, ×4.6 at z=3).");
        sb.AppendLine("    - H₀-independent (robust to the Hubble tension).");
        sb.AppendLine("    - Archival data (KMOS3D) can test it NOW.");
        sb.AppendLine("    - Three-way discriminator (TQM vs MOND vs ΛCDM).");
        sb.AppendLine();
        sb.AppendLine("    THE KILL-SHOT: measure R(z)=g†(z)/g†(0) at z≈1-2 to ~25%.");
        sb.AppendLine("    - Rising → TQM confirmed.");
        sb.AppendLine("    - Flat → TQM falsified, MOND favored.");
        sb.AppendLine("    This is the single most decisive experiment in the TQM");
        sb.AppendLine("    program, and it is immediately feasible.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 70 experiments.");
        return sb.ToString();
    }
}
