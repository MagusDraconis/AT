using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// The G-program (ResearchY-G_001 … G_004): the counting measure ρ as the source of gravity, its
/// controllability at fixed total energy, the SI magnitude of a Δρ, and the four-scale calibration.
/// Executable — recomputes the free-room count from the D96 spectrum and the threshold lengths from c, g.
/// </summary>
public sealed class GravityService : ICalculationService
{
    // ── Canonical physical anchors (QG181/QG182, QG080) and measured astronomical inputs ──
    public const double C = 2.99792458e8;            // m/s
    public const double G_N = 9.80665;               // m/s^2 (standard gravity)
    public const double G_AT = 6.6476e-11;           // m^3/kg/s^2 — DERIVED (QG181/QG182)
    public const double G_CODATA = 6.67430e-11;
    public const double H0 = 67.4;                   // km/s/Mpc (Planck 2018)
    public const double Kpc = 3.0856775814913673e19;
    public const double Mpc = 1e3 * Kpc;
    public const double Gpc = 1e6 * Kpc;
    public const double MSun = 1.98892e30;
    public const double GM_Sun = 1.32712440018e20;
    public const double GM_Earth = 3.986004418e14;
    public const double R_Earth = 6.371e6;
    public const double AU = 1.495978707e11;
    public const double A0_MOND = 1.200e-10;         // literature RAR scale
    public const double A0OverCH_Combined = 0.1725;  // project combined determination
    public const double WitnessDeltaAat = 0.685714;  // G_002 arrangement witness (max lattice Δa_AT)
    public const double ComaM500Msun = 1.0e15;
    public const double ComaR500Mpc = 1.48;
    public const double ComaBaryonFraction = 0.15;

    public string Name => "Gravity";

    public IReadOnlyList<CalculationResult> Results { get; }

    /// <summary>AT's acceleration scale g† = c·H0/(2π) (QG080) in m/s^2.</summary>
    public static double GDagger => C / (H0 * 1000.0 / Mpc) / (2.0 * Math.PI);

    public GravityService()
    {
        double c2 = C * C;
        double gdag = GDagger;
        double d = 3.0;

        // Free room of the counting measure = Σ(m_i − 1) = N − A₀ over the D96 eigenspaces.
        var modes = SpectrumService.Modes(SpectrumService.N);
        var mult = new List<int>();
        int i = 0;
        while (i < modes.Length)
        {
            int j = i;
            while (j < modes.Length && Math.Abs(modes[j] - modes[i]) <= 1e-6) j++;
            mult.Add(j - i);
            i = j;
        }
        int a0 = mult.Count;                       // eigenspaces (A₀ = 45)
        int n = SpectrumService.N;
        int free = mult.Sum(m => m - 1);            // 51 = N − A₀
        double latent = free / (double)n;           // 0.53125 = the D_048 latent fraction

        double ratioG = G_AT / G_CODATA;
        double earthObs = GM_Earth / (R_Earth * R_Earth);
        double sunObs = GM_Sun / (AU * AU);
        double rarRatio = gdag / A0_MOND;
        double comaObs = G_CODATA * ComaM500Msun * MSun / Math.Pow(ComaR500Mpc * Mpc, 2);
        double comaBar = comaObs * ComaBaryonFraction;
        double comaAt = comaBar * Math.Sqrt(1.0 + gdag / comaBar);

        double threshold(double target) => WitnessDeltaAat * c2 / target;   // L below which Δa > target
        double ambientDln = d * gdag * 15 * Kpc / c2;                        // the observed galactic field

        Results =
        [
            new("gravity-source",
                "Gravity Source — the counting measure ρ",
                "g = ρ^(2/d)·η,  R = F(ρ),  a = −(1/d)·∇ln ρ,  ρ_{k+1} = μ·ρ_k",
                [
                    new("d", d.ToString("0", CultureInfo.InvariantCulture), "spatial dimension"),
                    new("2/d", (2.0 / d).ToString("0.0000", CultureInfo.InvariantCulture), "conformal exponent"),
                    new("ρ^(2/d) at ρ=0.5", Math.Pow(0.5, 2.0 / d).ToString("0.0000", CultureInfo.InvariantCulture), "dimensionless conformal factor"),
                    new("dimension of ρ", "0 (pure ratio)", "the source needs no coupling constant, unlike GR's κ·T"),
                    new("a(λρ)/a(ρ)", "1 exactly", "scale invariance — only ratios of ρ are physical"),
                ],
                "SOURCE = the actualization density ρ (counting measure), and for the attractive sector its standardised deficit m = ρ̄ − ρ. "
                + "Energy density is CORRELATED (T00 = (ρ̄−ρ)v² is rank-identical to the deficit; the energy reading is hosted, QG89); "
                + "spectral density is CORRELATED (it supplies m₀ = occ₀/Σm, r₀ = ln span and the magnitude of G, but has no position index); "
                + "information density and the curvature–density law used as a source are REFUTED (ResearchY-G_001)."),

            new("density-control",
                "Density Control — ρ moves at fixed total energy",
                "Σm = 0 (QG194)  ⇒  free room = Σ(m_i − 1) = N − A₀",
                [
                    new("N", n.ToString(CultureInfo.InvariantCulture), "D96 modes"),
                    new("A₀", a0.ToString(CultureInfo.InvariantCulture), "distinct eigenspaces (D_048)"),
                    new("free directions", free.ToString(CultureInfo.InvariantCulture), "= N − A₀"),
                    new("latent fraction L", latent.ToString("0.00000", CultureInfo.InvariantCulture), "= the D_048 adaptability ceiling"),
                    new("lock release", "0.80231", "D_047 (nats)"),
                    new("uniform ρ field", "a = 0, R = 0 exactly", "the canonical measure is the zero-field configuration"),
                ],
                "Count conservation makes the total deficit vanish identically, so ρ's ARRANGEMENT is never tied to the energy. "
                + "Spectral organisation, a redistribution inside degenerate multiplets, fixed-total survivor compression and the lattice "
                + "choice all change ρ with ΔE = 0 exactly (CONTROLLABLE); phase coherence is REFUTED (ρ, R, a unchanged) and rescaling "
                + "is CORRELATED (Δa = 0 exactly while E scales) (ResearchY-G_002)."),

            new("gravity-magnitude",
                "Gravity Magnitude — the SI size of a Δρ",
                "ΔΦ/c² = Δa_AT  (no length scale);  Δa = c²·Δa_AT / L;  ΔR = ΔR_AT / L²;  ΔM = Δa·L²/G",
                [
                    new("witness Δa_AT", WitnessDeltaAat.ToString("0.000000", CultureInfo.InvariantCulture), "G_002 arrangement"),
                    new("ΔΦ/c²", WitnessDeltaAat.ToString("0.0000", CultureInfo.InvariantCulture), "68.6 % potential change"),
                    new("Δa at 15 kpc", (c2 * WitnessDeltaAat / (15 * Kpc)).ToString("0.000e0", CultureInfo.InvariantCulture), "m/s²"),
                    new("Δa/g at 15 kpc", (c2 * WitnessDeltaAat / (15 * Kpc) / G_N).ToString("0.000e0", CultureInfo.InvariantCulture), "g"),
                    new("L for > 1e-6 g", (threshold(1e-6 * G_N) / Kpc).ToString("0.00", CultureInfo.InvariantCulture), "kpc"),
                    new("L for > 1e-9 g", (threshold(1e-9 * G_N) / Mpc).ToString("0.00", CultureInfo.InvariantCulture), "Mpc"),
                    new("L for > 1e-12 g", (threshold(1e-12 * G_N) / Gpc).ToString("0.00", CultureInfo.InvariantCulture), "Gpc"),
                    new("ambient Δln ρ", ambientDln.ToString("0.000e0", CultureInfo.InvariantCulture), "g† over 15 kpc"),
                    new("suppression required", (WitnessDeltaAat / ambientDln).ToString("0.00e0", CultureInfo.InvariantCulture), "≥ 3.7e5"),
                ],
                "The potential/clock channel is MEASURABLE (a pure number); the acceleration/curvature channel is ASTROPHYSICAL ONLY "
                + "(threshold lengths 9.5 kpc / 9.5 Mpc / 9.5 Gpc); phase and rescaling are PRACTICALLY ZERO. All three thresholds are "
                + "passable at fixed total energy — the binding constraint is the physical scale L, not the energy (ResearchY-G_003)."),

            new("gravity-calibration",
                "Gravity Calibration — a_pred/a_obs at four scales",
                "Earth GM_⊕/R_⊕² · Sun–Earth GM_☉/AU² · RAR g† = c·H0/(2π) · cluster AT/RAR from baryons",
                [
                    new("Earth (a_obs)", earthObs.ToString("0.000000", CultureInfo.InvariantCulture), "m/s² = GM_⊕/R_⊕²"),
                    new("Earth a_pred/a_obs", ratioG.ToString("0.00000", CultureInfo.InvariantCulture), "= G_AT/G_CODATA"),
                    new("Sun–Earth (a_obs)", sunObs.ToString("0.000e0", CultureInfo.InvariantCulture), "m/s² at 1 AU"),
                    new("Sun–Earth ratio", ratioG.ToString("0.00000", CultureInfo.InvariantCulture), "same derived-G offset"),
                    new("g† = c·H0/(2π)", gdag.ToString("0.000e0", CultureInfo.InvariantCulture), "m/s², ZERO fitted parameters"),
                    new("RAR a_pred/a_obs", rarRatio.ToString("0.00000", CultureInfo.InvariantCulture), "vs the literature mean a₀"),
                    new("RAR vs combined", (gdag / (A0OverCH_Combined * C / (H0 * 1000.0 / Mpc))).ToString("0.0000", CultureInfo.InvariantCulture), "vs the project's combined a₀/cH₀"),
                    new("Coma a_obs", comaObs.ToString("0.000e0", CultureInfo.InvariantCulture), "m/s² at R_500 = 1.48 Mpc"),
                    new("Coma a_pred/a_obs", (comaAt / comaObs).ToString("0.00000", CultureInfo.InvariantCulture), "AT/RAR from baryons — REFUTED channel"),
                ],
                "CALIBRATED at three of four scales with no free parameters (Earth and Sun–Earth 0.99600 — the residual IS the derived-G offset; "
                + "the galaxy RAR 0.86850 with ZERO fitted parameters). CORRELATED: the RAR interpolating function. REFUTED: the cluster "
                + "modified-gravity channel (1.93× short) and a uniform cosmic AT gradient (104× above the local bound). The 1e-6 g figure "
                + "is a counterfactual — the realised field is g† = 1.06e-11 g and locally the prediction is exactly Newton with the derived G "
                + "(ResearchY-G_004)."),

            new("gravity-control",
                "Gravity Control — why the large modes are never realised",
                "P(Δ) = exp(−⟨N⟩·Δ²/2);  ⟨N⟩ = 1/δ²;  max ΔS = ln 96  ⇒  suppression ≤ 1/96",
                [
                    new("observed Δln ρ", ObservedContrast.ToString("0.000e0", CultureInfo.InvariantCulture), "G_003 ambient (g† over 15 kpc)"),
                    new("⟨N⟩ = 1/δ²", (1.0 / (ObservedContrast * ObservedContrast)).ToString("0.000e0", CultureInfo.InvariantCulture), "events per coherence cell"),
                    new("P(observed)", Math.Exp(-0.5).ToString("0.0000", CultureInfo.InvariantCulture), "the observed field is TYPICAL"),
                    new("1 % contrast cut", ContrastAtProbability(0.01).ToString("0.000e0", CultureInfo.InvariantCulture), "the accessibility ceiling"),
                    new("Δ at the 3.746e5 requirement", ContrastAtProbability(1.0 / 3.746e5).ToString("0.000e0", CultureInfo.InvariantCulture), "5.07× the observed level"),
                    new("−ln P at the witness 0.15151", MinusLogProbability(0.15151).ToString("0.000e0", CultureInfo.InvariantCulture), "probability 10^−1.9e9"),
                    new("max entropy suppression", (1.0 / n).ToString("0.000000", CultureInfo.InvariantCulture), "max ΔS = ln 96 ⇒ 1/96"),
                    new("witness tilt ΔS", "0.272565", "a factor 1.31 only — 3.6e7× short"),
                    new("tilt contraction, 200 steps", "0.0296", "std 0.008154 → 0.000241"),
                ],
                "SUPPRESSED, not forbidden: the large G_003 modes conserve the count exactly (Σρ = 1), break no symmetry (the D_047 lattice "
                + "invariants are untouched) and occupy genuinely free directions (51 of 96) — they are simply never counted. The entropy "
                + "channel is CAPPED (max ΔS = ln 96 ⇒ 1/96 = 0.010417, 3.6e7× short of the required 3.746e5), so the mechanism is AT's own "
                + "mandatory Poisson law δ = 1/√⟨N⟩ read forward from the observed contrast. The internal flow is arrangement-neutral "
                + "(ρ_(k+1) = μρ_k with the same μ per cell and a(λρ) = a(ρ)), and the attractor erases arrangements, so the modes need an "
                + "external drive (NP_171 gate g_c = 1.607). ACCESSIBLE = the attractor, the phase directions and Δ ≤ 4.8867e-6 including the "
                + "observed galactic field; FORBIDDEN = Σρ ≠ 1, a changed A₀ or mirror pairing, a cell above 1/l_P³, or Δ > ln 96 "
                + "(ResearchY-G_005)."),

            new("suppression-mechanism",
                "Suppression Mechanism — the term, derived in closed form",
                "mu_k = 1 − 2d(1 − cos(pi k/N));  r(m) = sqrt(Σ_{k≥1} w_k²mu_k^2m / Σ_{k≥1} w_k²);  1/r(200) = 33.78",
                [
                    new("mu_1 (slow)", Mu(1).ToString("0.000000000000", CultureInfo.InvariantCulture), "1/e after 4669 steps"),
                    new("mu_48", Mu(48).ToString("0.000000000000", CultureInfo.InvariantCulture), "= 0.6 exactly (lambda_48 = 12)"),
                    new("mu_95 (fast)", Mu(95).ToString("0.000000000000", CultureInfo.InvariantCulture), "1.99e-140 after 200 steps"),
                    new("mu_1^200", Math.Pow(Mu(1), 200).ToString("0.000000", CultureInfo.InvariantCulture), "the slow mode survives"),
                    new("1/r(200)", (1.0 / TiltRatio(200)).ToString("0.00", CultureInfo.InvariantCulture), "the G_005 34x (audited: 33.78)"),
                    new("exp(200·rate(200))", Math.Exp(-200.0 * Math.Log(TiltRatio(200))).ToString("0.00", CultureInfo.InvariantCulture), "= exp(3.5198 nats)"),
                    new("1/r(m) at m = 1 / 50 / 500 / 1000", $"{1.0 / TiltRatio(1):F2} / {1.0 / TiltRatio(50):F2} / {1.0 / TiltRatio(500):F2} / {1.0 / TiltRatio(1000):F2}", "the factor is NOT universal"),
                    new("rate(m) = −ln r/m: 200 / 20 000 / 50 000", $"{Rate(TiltRatio(200), 200):E4} / {Rate(TiltRatio(20000), 20000):E4} / {Rate(TiltRatio(50000), 50000):E4}", "converges to |ln mu_1| = 2.1419e-4"),
                    new("|ln mu_1|", (-Math.Log(Mu(1))).ToString("0.000000E+00", CultureInfo.InvariantCulture), "the geometric floor"),
                    new("power-law R² over 1..200", "0.9945", "vs 0.7579 for one exponential — the illusion"),
                    new("tail slope over 5000..50000", "-2.1418813131e-4", "= ln mu_1 to 1.85e-10 (R² = 1.0)"),
                    new("cube mu_1 (N = 884 736)", Mu(1, Damping, 884736).ToString("0.00000000000000", CultureInfo.InvariantCulture), "a slow mode that never decays"),
                ],
                "The suppressing term is the RELAXATION operator (RhoDynamics.DiffuseStep): a LINEAR low-pass filter on the eigenspace-occupancy "
                + "index. Every Neumann mode decays geometrically, so r(m) is exact and the 34× is 1/r(200) = 33.78 = exp(3.5198 nats). "
                + "Power-law decay is REFUTED as the law (the tail is a single exponential at slope ln μ₁, 1.85e-10) although its finite-window "
                + "appearance is real; entropy-driven is REFUTED (the operator is exactly linear and H is slaved to the Dirichlet energy); "
                + "branching-driven is REFUTED (arrangement-neutral). Cases at m = 200: D96 33.78, D96³ 7.33, Random witness class EMPTY. "
                + "The mechanism is arrangement-selective, not lattice-selective (ResearchY-G_006)."),
        ];
    }

    // ── G_006: the relaxation operator's exact spectrum and closed-form contraction ────────────────

    private const double Damping = 0.2;

    /// <summary>Exact Neumann eigenvalue of the relaxation step: mu_k = 1 - 2d(1 - cos(pi k/N)).</summary>
    private static double Mu(int k, double d = Damping, int n = 96)
        => 1.0 - 2.0 * d * (1.0 - Math.Cos(Math.PI * k / n));

    private static double[] NeumannMode(int k, int n = 96)
        => Enumerable.Range(0, n).Select(i => Math.Cos(Math.PI * k * (i + 0.5) / n)).ToArray();

    /// <summary>Exact closed-form std ratio of the canonical D96 witness tilt after m relaxation steps.</summary>
    private static double TiltRatio(int m)
    {
        var tilt = D96Tilt();
        int n = tilt.Length;
        var w = new double[n];
        for (int k = 0; k < n; k++)
        {
            double s = 0.0;
            for (int i = 0; i < n; i++) s += tilt[i] * Math.Cos(Math.PI * k * (i + 0.5) / n);
            w[k] = s;
        }
        w[0] = 0.0;
        double num = 0.0, den = 0.0;
        for (int k = 1; k < n; k++) { num += w[k] * w[k] * Math.Pow(Mu(k), 2.0 * m); den += w[k] * w[k]; }
        return Math.Sqrt(num / den);
    }

    private static double Rate(double ratio, int m) => -Math.Log(ratio) / m;

    /// <summary>The canonical D96 counting measure carrying the G_002 within-multiplet 80/20 witness tilt.</summary>
    private static double[] D96Tilt()
    {
        var modes = SpectrumService.Modes(SpectrumService.N);
        int n = modes.Length;
        var mult = new List<int>();
        int i = 0;
        while (i < n)
        {
            int j = i;
            while (j < n && Math.Abs(modes[j] - modes[i]) <= 1e-6) j++;
            mult.Add(j - i);
            i = j;
        }
        var rho = new double[n];
        int p = 0;
        foreach (int mm in mult)
        {
            double share = mm / (double)n;
            rho[p++] = share * 0.8;
            for (int q = 1; q < mm; q++) rho[p++] = share * 0.2 / (mm - 1);
        }
        return rho;
    }

    // The G_003 ambient calibration: the observed galactic field as a counting-measure contrast.
    private const double ObservedContrast = 1.6102e-6;

    private static double MinusLogProbability(double contrast)
        => contrast * contrast / (2.0 * ObservedContrast * ObservedContrast);

    private static double ContrastAtProbability(double p)
        => ObservedContrast * Math.Sqrt(2.0 * -Math.Log(p));
}
