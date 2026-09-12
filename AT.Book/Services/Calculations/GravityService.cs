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

            new("suppression-origin",
                "Suppression Origin — is the smoothing derived or imported?",
                "W = I − d·L;  0 ≤ d ≤ 1/2 (from rho >= 0);  selectivity ~ |1 − 4d|;  factor = f(T = m·d)",
                [
                    new("trace", "Difference → measure → coarse-graining → Laplacian flow", "every link canonical; RG invariance exact"),
                    new("unique up to one rate", "W(b) = b·left + (1−2b)·a + b·right", "W(0.2) ≡ DiffuseStep"),
                    new("isotropy is forced", "anisotropic weights leak at the reflecting edge", "no antisymmetric coupling (NP_174)"),
                    new("admissible range (DERIVED)", "0 <= d <= 1/2", "convex combination; d = 0.6 gives min rho < 0"),
                    new("selectivity |mu_95|/|mu_1|", $"{Selectivity(0.2):F4} (d = 0.2) / {Selectivity(0.25):F6} (d = 1/4) / {Selectivity(0.5):F6} (d = 1/2)", "maximal at 1/4, ZERO at 1/2"),
                    new("factor at m = 200", string.Join("  ", new[] { 0.1, 0.2, 0.3, 0.4, 0.5 }.Select(d => $"d={d:F1}: {TiltRatioAt(d, 200):F2}")), "collapses at d = 1/2"),
                    new("factor at fixed T = 40", string.Join("  ", new[] { (0.05, 800), (0.1, 400), (0.2, 200), (0.4, 100) }.Select(p => $"{TiltRatioAt(p.Item1, p.Item2):F2}")), "0.17 % spread over a 20x range of d"),
                    new("witness dichotomy", "32.4 (canonical) / 1.81 (d = 1/2) / 7.1 (biharmonic) / 1.0 (identity)", "holds for every 0 < d < 1/2"),
                    new("time", "branching is diagonal: |da| at 2^1000 rho = 0", "TIME REFUTED as the cause"),
                    new("horizon for 3.746e5", "≈ 729 relaxation steps", "a horizon, not a duration"),
                ],
                "The suppressing operator's FORM is DERIVED (the infinitesimal form of the canonical coarse-graining, with exact RG invariance) and it is UNIQUE up to one "
                + "scalar rate among local, linear, isotropic, count-conserving and scale-free maps; the admissible range 0 ≤ d ≤ ½ is DERIVED from ρ ≥ 0; the values "
                + "d = 0.2 and m = 200 — hence the 34× — are BOUNDARY (the same flow at fixed T = m·d = 40); the nearest-neighbour average IS d = ½ and destroys the "
                + "mechanism, the spectral cutoff is non-local and non-positive, the biharmonic is non-positive and unstable beyond κ = 1/16, and the identity suppresses "
                + "nothing; and TIME is REFUTED as the cause (ResearchY-G_007)."),

            new("controlled-suppression",
                "Controlled Suppression — can anything hold a high-Δρ state?",
                "rho* = c·v_k/(1 − mu_k);  tau_k = −1/ln|mu_k|;  required drive = (1 − mu_k) of the amplitude per step",
                [
                    new("lifetime tau_k (steps)", $"k=1: {Lifetime(1):F1}  k=24: {Lifetime(24):F2}  k=48: {Lifetime(48):F2}  k=95: {Lifetime(95):F3}", "4668.8 down to 0.622"),
                    new("amplitude after 200 steps", $"{Math.Pow(Mu(1), 200):F4} (k=1)  →  {Math.Pow(Mu(95), 200):E2} (k=95)", "smooth survives, witness annihilated"),
                    new("gain 1/(1 − mu_k)", $"k=1: {GainOf(1):F2}  k=48: {GainOf(48):F4}  k=95: {GainOf(95):F4}", "the DC response of the filter"),
                    new("drive per step (pure modes)", $"{1 - Mu(95):F4} (k=95) vs {1 - Mu(1):E4} (k=1)", $"ratio {(1 - Mu(95)) / (1 - Mu(1)):F1}"),
                    new("peak-to-peak / L1 contrast ratios", "1566x / 5354x", "witness vs an equal-contrast smooth target"),
                    new("sup_omega |H_k|", "= the DC gain at omega = 0 for every k", "no amplifying band"),
                    new("boundary-supported", "smooth ramp, gain 120.0, contrast capped at 3.09 %", "ρ ≥ 0 caps the drive"),
                    new("verdict", "undriven high-k SUPPRESSED · smooth METASTABLE · driven STABLE (re-created)", "no gravity-control state persists on its own"),
                ],
                "The kernel of the relaxation is one-dimensional, so the only undriven stationary profile is uniform; a driven steady state exists for every mode and is an allowed "
                + "configuration, but the price is (1 − μ_k) per step — 2.14e-4 for the smooth class and 0.7998 for the highest mode (a 3734× penalty). The steady state is a filtered copy "
                + "of the drive, so an unmode-matched drive holds no high-k content; periodic forcing never beats DC; boundary-only support holds a smooth ramp whose contrast ρ ≥ 0 caps at "
                + "3.09 % of the count. G_005's SUPPRESSED verdict becomes NOT MAINTAINABLE (ResearchY-G_008)."),

            new("clock-rate",
                "Clock Rate — does ρ move clocks?",
                "dtau/dt = rho^(1/d);  (1/d)Delta ln rho = Delta Phi/c^2;  AT: exp(x)  vs  GR: sqrt(1 + 2x)",
                [
                    new("Earth surface", $"{UsPerDayOf(-EarthDepth):F3} us/day", "AT == GR to double precision"),
                    new("GPS gravitational", $"{UsPerDayOf(GpsDepth):F3} us/day", "QG187: 45.7"),
                    new("GPS total (with SR)", $"{UsPerDayOf(GpsDepth - SrTerm):F3} us/day", "observed 38.6 (ratio 0.9984)"),
                    new("Galactic field (AT)", $"{GalacticAt * 86400 * 1e3:F3} ms/day", "from the G_003 ambient contrast"),
                    new("Galactic field (v^2/c^2)", $"{GalacticKin * 86400 * 1e3:F3} ms/day", "220 km/s; ratio 0.99668"),
                    new("equivalent rotation velocity", $"{Math.Sqrt(GalacticAt) * C / 1e3:F2} km/s", "0.33 % agreement, nothing fitted"),
                    new("arrangement redistribution", $"{UsPerDayOf(0.685714 / 3.0) / 1e6:F1} s/day", "22.86 % at fixed total mass-energy"),
                    new("realised band (G_005 ceiling)", $"{4.8867e-6 / 3.0 * 86400:F4} s/day", "3.03x the observed level"),
                    new("AT vs GR second order", "±1/2 x^2", "4.846e-19 (Earth), below the 1e-18 clock floor"),
                    new("scale invariance", "ρ → λρ: RELATIVE change exactly 0", "only ratios of ρ are physical"),
                ],
                "The clock law is DERIVED from g₀₀ = −ρ^(2/d): dτ/dt = ρ^(1/d) with (1/d)Δlnρ = ΔΦ/c². AT and GR agree to first order exactly (Earth −60.145 μs/day; GPS gravitational +45.740 vs QG187's "
                + "45.7) and split at second order by ±½(Φ/c²)² (4.8e-19 at the Earth's surface — 2–4× below the 1e-18 optical-clock floor). The GPS total needs the imported SR term, so it is CORRELATED. "
                + "The galactic field is a NEW parameter-free cross-check: the G_003 ambient contrast gives 46.374 ms/day against the rotation curve's 46.528 (0.33 %, equivalent v = 219.63 km/s). The G_002 "
                + "redistributions move clocks by up to 22.86 % at fixed total mass-energy, but nothing realised or maintainable delivers it — what is realised is exactly GR's galactic depth "
                + "(ResearchY-G_009)."),

            new("time-control",
                "Time Control — what clock shift can be sustained?",
                "Delta ln rho = 3f;  drive/step = (1 - mu_k) * Delta ln rho;  P ~ f * (k/N)^2",
                [
                    new("1 ns/day", $"Delta ln rho = {3 * 1e-9 / 86400.0:E3}, drive {G10Drive(1e-9, 1):E3}/step", "well 32.25 m/s — PRACTICAL (numbers)"),
                    new("1 us/day", $"Delta ln rho = {3 * 1e-6 / 86400.0:E3}, drive {G10Drive(1e-6, 1):E3}/step", "well 1019.91 m/s — PRACTICAL"),
                    new("1 ms/day", $"Delta ln rho = {3 * 1e-3 / 86400.0:E3}, drive {G10Drive(1e-3, 1):E3}/step", "well 32.25 km/s — PRACTICAL"),
                    new("1 s/day", $"Delta ln rho = {3 * 1.0 / 86400.0:E3}, drive {G10Drive(1.0, 1):E3}/step", $"7.1x the band — REFUTED (well 1019.91 km/s)"),
                    new("band top (G_005)", "0.140737 s/day", "Delta ln rho = 4.8867e-6; a 382.62 km/s well"),
                    new("observed galactic field", "0.046373 s/day", "33 % of the band; a 219.63 km/s well"),
                    new("positivity", "min rho = 1.041667e-2, excursion <= 1.7e-5", "positivity and count NEVER bind"),
                    new("power scaling", "6.4250e-4 (k=1) vs 2.3994 (k=95) per unit shift", "P ~ f*(k/N)^2; a 1024-site lattice is 113.7x cheaper"),
                    new("the driver", "mode-matched to 1e-18; nothing in the chain supplies it", "time control is arithmetic-feasible, physics-refuted"),
                ],
                "Inverting the clock law gives Δlnρ = 3f and G_008 prices the drive at (1 − μ_k) of the amplitude per step. 1 ns/day, 1 µs/day and 1 ms/day need contrasts of 3.472222e-14, "
                + "3.472222e-11 and 3.472222e-8 (drives 7.436285e-18 … 7.436285e-12 per step) to sustain wells of 32 m/s, 1.02 km/s and 32.3 km/s — all ≤7.11e-3 of the accessible band, with "
                + "positivity never binding. The band's top is 0.140737 s/day and the observed galactic field already uses 33 % of it; 1 s/day (Δlnρ = 3.472222e-5, a 1020 km/s well) is REFUTED "
                + "spontaneously. Every target still needs an external mode-matched driver, which the canonical chain does not supply (ResearchY-G_010)."),

            new("rho-actuator",
                "Rho Actuator — can any physical quantity change rho?",
                "E = <lambda, rho>;  s = (I - W) rho*;  gains 1/(1 - mu_k)",
                [
                    new("energy density (fixed E)", "E = 12.000000000000 both ways; rho moves L1 = 0.6666667", "the field goes 0 -> 0.603175 at zero energy cost"),
                    new("energy is not injective", "fixed-E fibre contains 51 dims (94 in total)", "re-ordering the multiset moves E by 1.5401766 (12.8 %)"),
                    new("spectral organization", "DCT-II ||CC^T - I|| = 1.37e-14, round trip 4.2e-16", "95 free coordinates; DC = Sigma rho = 1 is fixed"),
                    new("phase coherence", "L1(rho, |psi|^2) <= 2.5e-16, |da| < 1e-9", "while |Sigma psi| runs 0.0324196 -> 9.1171822 (281.22x)"),
                    new("synchronization", "Kuramoto r 8.08e-17 -> 1.0", "the canonical grid is maximally incoherent; rho untouched"),
                    new("attractor compression", "L1 = 2.8669638, da = 0.032121", "a rho -> rho map; its smooth difference survives 200 steps 1.1207x"),
                    new("information density", "Delta KL = 0 exactly under permutation", "while rho moves L1 = 0.6583333 and |da| grows to 1.0031746"),
                    new("rho is fully actuable", $"gains {1.0 / (1.0 - Mu(1)):F6} (k=1) ... {1.0 / (1.0 - Mu(95)):F6} (k=95)", "s = (I - W) rho* is unique, count-neutral, and holds any target"),
                ],
                "Five of the seven candidates are FUNCTIONS of rho (energy, spectral organization, degeneracy engineering, compression, information) and two are INDEPENDENT of rho but exactly "
                + "rho-INERT (phase coherence, synchronization: L1 = 0 to 1e-15 while the psi-sector moves by a factor 281). So no candidate is an actuator — yet rho is completely controllable: the "
                + "hold-drive s = (I - W) rho* is count-neutral, unique per target and finite in every direction, so the vacancy is a SOURCE (the missing driver of G_008/G_010), not a handle "
                + "(ResearchY-G_011)."),

            new("labor-rho",
                "Labor Rho — can a bench system hold a controlled rho profile?",
                "0 <= d <= 1/2 (CFL);  drive/step = (1 - mu_k) Delta ln rho;  M/r = f c^2/G",
                [
                    new("oscillator lattice / D96 controls", $"chain lambda_max = 3.998929, d_max = {2.0 / 3.998929:F6}", "d = 0.2 sits at 0.3999 of the bound; rate spread 3734.44"),
                    new("resonator network (ring)", "lambda_max = 4.0, d_max = 0.5", "d = 0.2 at 0.4000; rate spread 934.11"),
                    new("graph diffusion (D96 circulant)", "lambda_max = 15.837372, d_max = 0.126284", "d = 0.2 is 1.5837x OVER the bound"),
                    new("hub / star (control)", "lambda_max = 96, d_max = 0.020833", "d = 0.2 is 9.6000x over"),
                    new("drive for 1 ns/day", $"{(1.0 - Mu(1)) * 3e-9 / 86400.0:E3} per step (k=1)", "the G_002 witness class costs 0.48675 per step (49 % of the count)"),
                    new("steady state", "Sigma rho = 1, excursion <= 1.74e-5 of rhoBar", "positivity never binds; reproduced to < 1e-12 after 20 000 steps"),
                    new("clock floor (1e-18)", $"M/r = {MassOverRadius(1e-18):E3} kg/m", "1.35 million tonnes per metre"),
                    new("band top (0.1407 s/day)", $"M/r = {MassOverRadius(4.8867e-6 / 3.0):E3} kg/m", "2340 Earths per metre"),
                    new("1 kg at 1 m", $"{6.6743e-11 * 1.0 / (2.99792458e8 * 2.99792458e8):E3}", "1.35e9x below the clock floor; exactly Newtonian anyway"),
                ],
                "The rho dynamics is PRACTICAL (a 96-site chain with d = 0.2 IS RhoDynamics.DiffuseStep, and the derived 0 <= d <= 1/2 IS the CFL stability bound), every real clock or gravity readout "
                + "is ASTROPHYSICAL (the 1e-18 floor needs M/r = 1.35e6 t/m), and a bench-side metric effect from a rho rearrangement is REFUTED (a kilogram moved a metre gives 3.7e-28, 2.7e9x below the "
                + "clock floor, and AT's prediction is exactly Newton's — ResearchY-G_011b)."),

            new("local-actuator",
                "Local Rho Actuator — is there a local handle on the density?",
                "s = rho - W rho (identity closed loop);  c_k = mu_k(1 + beta) - beta;  HighKShare <= 2.8666657e-7 D_high/w_1^2",
                [
                    new("(1) local", "s = (I - W) rho* is a three-point stencil", "perturbing one cell moves exactly three source entries"),
                    new("(2) finite", $"||s||_1 = 0.48675 (witness); max|s| = 0.01866667", "3.331453e-10 for the band-top profile"),
                    new("(3) survives W", "held to < 1e-12 over 20 000 steps", "freeze: 4.336809e-18 over 5000 steps"),
                    new("(4) no primitive", "s = rho - W rho is linear in the local state", "the closed loop is the IDENTITY (a MEMORY, no restoring force)"),
                    new("three-point theorem", "c_k = mu_k(1+beta) - beta; c_k = 1 for all k only at beta = -1", "95/95 modes neutral against 0/95 at beta = 0"),
                    new("restoring family", $"stability lambda <= {1.0 - Mu(1):E3} = 1 - mu_1", "only k = 1 holdable; k = 2 decays 6.422657e-4/step; k = 48 at 0.4 is unstable"),
                    new("runaway above threshold", $"growth {Mu(1) + 2.0 * (1.0 - Mu(1)):F9}/step", "saturation from a Poisson seed in 64 516 steps"),
                    new("compact masks", "block w = 2: 6.630273e-3 ... w = 64: 6.368861e-9", "best structured mask 0.6117676 = 77 % of the witness (prescribed data)"),
                    new("sync / lattice", "locked patch L1 = 2.45e-16; responses 2.5 ... 120.0", "synchronization REFUTED; the lattice and a NESS are the MEDIUM"),
                ],
                "Five local candidates tested against four requirements. The hold-drive is a local three-point stencil; the incremental feedback s = rho - W rho freezes ANY configuration exactly (the witness to 4.3e-18, a "
                + "perturbation retained 100.000 %) because its closed loop is the identity, and it is the UNIQUE non-trivial member of its three-point family (c_k = 1 for all k only at beta = -1). It is marginal — a memory, not "
                + "a creator. The restoring family holds only the smoothest mode (lambda <= 1 - mu_1), mode injection and synchronization are REFUTED, and the lattice/NESS framing is the medium rather than the source "
                + "(ResearchY-G_012)."),

            new("physical-actuator",
                "Physical Actuator — what device builds the density handle?",
                "s = -d * Laplacian(rho*);  loop eigenvalue 1 + eps (1 - mu_k);  P_min = k_B T dH / tau",
                [
                    new("the stencil", "s_i = -d(rho_{i-1} - 2 rho_i + rho_{i+1})", "a NEGATIVE LAPLACIAN: anti-diffusion, verified to 3.5e-18"),
                    new("balance", "injection 50.0000 % / extraction 50.0000 % of 0.48675", "max|s| = 0.01866667; count-neutral; three-point local"),
                    new("PHYSICAL", "feedback controller; active diffusion cancellation (an NIC)", "exact stencil; marginal hold (< 1e-15 over 5000 steps)"),
                    new("error budget", "loop I + eps(I - W) -> 1 + eps(1 - mu_k)", "eps = 1e-3: 1250 steps (fastest) vs 4.669e6 (smoothest)"),
                    new("ANALOGUE", "node-wise gain: minimax error 0.3997858 vs a 3734.437 spread", "band-limited: a 16-mode bank leaves 96.4 % uncompensated"),
                    new("REFUTED", "pump/loss networks", "a scalar balance fixes single modes; imbalance e-folds in 125 steps at 1 %"),
                    new("delay", "roots {1, mu_k - 1} in [-0.7997858350, 0]", "one-step delay is tolerable; a 1e-3 perturbation is retained"),
                    new("power", "Landauer 8.6295e-20 J/step = 8.63e-14 W at 1 us", "the smooth profile costs dH = 0.0: thermodynamically free"),
                ],
                "The stencil is a negative Laplacian (anti-diffusion) and a balanced pump-and-drain, so a feedback controller or a nearest-neighbour negative conductance (NIC) realizes it exactly (PHYSICAL). A node-wise gain "
                + "and a band-limited resonator bank are only ANALOGUE, and a pump/loss balance is REFUTED. Exactness is the binding constraint, while power is ten orders below any electronic floor (ResearchY-G_013)."),

            new("rho-mapping",
                "Rho Mapping — what do you actually measure?",
                "kappa = d ln q / d ln rho = 1;  delta = 1/sqrt(<N>) = 1.6102e-6;  P = exp(-<N> Delta^2/2)",
                [
                    new("criterion", "q = F(rho) must be positive, normalised, AFFINE and cellwise", "W and the G_013 stencil are LINEAR, so only kappa = 1 commutes"),
                    new("affine test", "flow and actuator errors are EXACTLY 0 at kappa = 1", "kappa = 0.5: 3.229213; kappa = 2: 1.047221e-2; kappa = 3: 1.724505e-2"),
                    new("PHYSICAL", "probability density: |psi|^2 IS rho (2.484991379e-16)", "source law, clock law, actuator and 33.7781483x suppression hold identically"),
                    new("PHYSICAL", "occupation density: its shot noise IS the band", "<N> = 3.856917553651e11, delta = 1.610200e-6, ceiling 4.886722e-6, P = 0.6065"),
                    new("CORRELATED", "mode population: reversal-invariant power spectrum (4.1598669e-15)", "while max|Delta a| = 0.8864864874 (correlation 0.10): spectrally blind"),
                    new("CORRELATED", "energy density: a zero weight mode makes eps = 0", "commutation defect 1.81 %; clock factor spread [0.5689248, 1.1378497]"),
                    new("REFUTED", "information density (global), coherence density (psi-sector)", "Delta KL = 0 under a roll; a 281.2241449x phase change at Delta rho = 0"),
                    new("critical answer", "the diagonal occupation (probability) density", "floor 1.6102e-6 = 46.374 ms/day of clock depth; only the identification premise remains"),
                ],
                "Six candidates against four requirements reduce to four structural conditions (positive, normalised, AFFINE, cellwise). The diagonal occupation (probability) density is the first measurable rho analogue: kappa = 1 exactly, its shot noise IS "
                + "G_005's Poisson band, and its readable floor is 46.374 ms/day of clock depth. The mode population and energy density are CORRELATED, the information and coherence densities REFUTED (ResearchY-G_014)."),

            new("rho-metric",
                "Rho To Metric — can a laboratory q profile move a clock?",
                "Delta Phi/c^2 = G m/(R c^2);  M/r = f c^2 / G;  u_floor = 2.889272332033454e25 J/m^3",
                [
                    new("two channels", "the map is the IDENTITY (kappa = 1, G_014), so contrast vs metric separate", "ANALOGUE: Delta tau/tau = Delta ln q/d is a measurable RATIO; METRIC: mass-energy only (G_011b)"),
                    new("clock ladder", "floor 1e-18 needs M/r = 1.3466e9 kg/m = 1.3466e7 kg in 1 cm", "13 466 tonnes in a centimetre; galactic 7.2276e20; band top 2.1935e21 kg/m"),
                    new("photon occupation", "1 J (5.034116567542709e18 photons at 1 um) in 5 mm", "Delta Phi/c^2 = 1.6525e-42; a = 2.9705e-23 m/s^2; 1.65e-24x the floor"),
                    new("cavity modes", "1 kJ in 6 cm (SRF, Q = 1e10)", "1.3771e-40; a = 2.0628e-22 m/s^2; 1.38e-22x the floor"),
                    new("resonator lattice", "1 mJ on 1 cm", "8.2627e-46; a = 7.4262e-27 m/s^2; 8.26e-28x the floor"),
                    new("oscillator lattice", "1 nJ on 1 cm (the G_011b chain)", "8.2627e-52; a = 7.4262e-33 m/s^2; 8.26e-34x the floor"),
                    new("strongest lab case", "an energy density in a 1 m ball (V = 4.189 m^3)", "nuclear 1e18 J/m^3 gives 3.4611e-26 = 3.46e-8x the floor, still 2.889e7x SHORT"),
                    new("what the floor needs", "u = 2.889272332033454e25 J/m^3 (2.889e7x nuclear)", "the band top needs 4.706335701649293e37 J/m^3 (4706.3x a neutron-star core)"),
                    new("the measurable channel", "count floor 1.6102e-6 = 0.046374 s/day; 20:1 = 86 277.089 s/day", "but the equivalent mass at 1 cm is 7.2276e18 kg (floor) to 1.3447e25 kg (20:1, 2.25 Earth masses)"),
                    new("critical answer", "NO in the METRIC sense, YES in the ANALOGUE sense", "a neutron-star core (3.4611e-10) would finally be measurable: ASTROPHYSICAL ONLY"),
                ],
                "The audit only becomes well-posed once G_014's identity map is used, splitting the question in two. The ANALOGUE contrast channel is MEASURABLE for all four cases (kappa = 1: 0.046374 to 86 277.089 s/day), while the METRIC channel has mass-energy as its only "
                + "laboratory handle (G_011b) and is REFUTED: the best laboratory case is 3.4611e-26, 3.46e-8 of the 1e-18 floor, and the floor itself needs 13 466 tonnes inside a centimetre (2.889e7x nuclear density). Every metric effect that exists is ASTROPHYSICAL ONLY (ResearchY-G_015)."),

            new("watch-ontology",
                "Watch Ontology — is mass-energy required to generate rho?",
                "E = <lambda, rho>;  rank 1 of 95;  kernel 94 = 51 (energy-free by degeneracy) + 43;  free room 51",
                [
                    new("the AT-native chain", "Difference -> distinguishability -> lattice -> {capacity lambda, occupancy rho} -> E -> clocks", "rho and lambda are SIBLINGS; E = <lambda, rho> is their PAIRING"),
                    new("rho from primitives", "positivity and Sigma rho = 1 need ONLY distinguishability (QG194)", "A0 = 45; histogram {1:1, 2:42, 5:1, 6:1}; free room 51; Sigma lambda = 1152 (rho-blind)"),
                    new("E is a rank-1 pairing", "E = <lambda, rho>; E(uniform) = Sigma lambda / N = 12.0 exactly", "on {Sigma rho = 1} rank 1, kernel 94, so E sees 1.0526 % of rho"),
                    new("rearrangement does move E", "comonotone 13.540176608029563 (+12.835 %)", "anticomonotone 10.015359929915876; spread 3.5248166781136874"),
                    new("REFUTED: rho needs mass", "the 51-dim free room: 51 directions in which rho moves and E does not", "m = 6 multiplet, delta = 0.005/0.01: |dE| <= 1.776e-15, ratios 2.8462 / 49.0000"),
                    new("the witness is energy-free", "the canonical tilt is a PURE within-multiplet move", "L1 = 0.6666666666666667, dE = 0 exactly, max|a| = 0.6031746"),
                    new("the minimal carrier is cellwise", "dimension ladder 95 -> 44 (loses the 51-dim room) -> 1 (loses 94)", "so any coarser carrier destroys exactly the energy-free room"),
                    new("degeneracy distribution = CORRELATED", "the CAPACITY side: it sizes the room but carries no rho", "invariant under any within-room move (block-sum L1 = 0 to 1e-12)"),
                    new("survivor compression = CORRELATED", "a FUNCTIONAL of rho, and not even energy-free", "+4.248925e-3 at 48 kept; -2.959751e-1 at 24 kept"),
                    new("critical answer", "YES: clock rate changes at fixed Sigma m", "clock ratio 20^(1/3) = 2.7144176165949063 = 86 277.089 s/day separation with dE = 0"),
                ],
                "rho is MORE PRIMITIVE than mass-energy: rho and the spectrum lambda are siblings off the coupling lattice (capacity and occupancy) and E = <lambda, rho> is their rank-1 pairing on a 95-dimensional object, keeping only 1.0526 %. "
                + "51 of the 94 discarded dimensions are energy-free BY DEGENERACY, so the canonical witness rearranges density with dE = 0 exactly while changing the clock ratio between its extreme cells by 2.7144176165949063. SOURCE = rho; CARRIER = the cellwise counting density (kappa = 1); "
                + "BOOKKEEPING = E = <lambda, rho> (ResearchY-G_016)."),

            new("mass-independence",
                "Mass Independence — is rho free of E, or the reverse?",
                "kernel of rho -> E on {Sigma rho = 1}: 94 = 51 (degeneracy) + 43 (lambda-mixing);  E = 2K",
                [
                    new("both directions", "same E different rho = INDEPENDENT; same rho different E = REFUTED", "E is a FUNCTION of rho: one rho, one E — independence is not symmetry"),
                    new("51-dim degeneracy room", "lambda constant within a multiplet, so E is invariant by symmetry", "DeltaE = 0 exactly; the witness moves rho by L1 = 0.6666666666666667"),
                    new("the witness", "contrast 20 : 1 at DeltaE = 0", "DeltaTau/tau = (1/d) ln 20 = 0.9985774245179969 = 86 277.089 s/day"),
                    new("pairwise m = 2 moves", "DeltaRho = 2 delta exactly, DeltaE = 0", "delta = 0.002: contrast 1.4752, DeltaTau 0.129609; delta = 0.005: contrast 2.8462, DeltaTau 0.348656"),
                    new("43-dim lambda-mixing room", "cells 94/92/90 with distinct lambda, <lambda,v> = 0 EXACTLY", "v = (1, -1.1255345008711273, 0.12553450087112727); at 0.004: DeltaRho = 0.009004, DeltaTau = 25 660.0263 s/day"),
                    new("REFUTED: same rho, new E", "the SAME uniform rho = 1/96 gives E = 2K exactly for K = 1..6", "Sigma lambda = 192K; A0 = 49/47/45/47/45/45; L1(rho_K, rho_6) = 0; K is BOUNDARY (G_007)"),
                    new("the phase sector is inert", "four phase assignments: DeltaE = 0 and DeltaTau = 0 exactly", "while |Sigma psi| moves 0.03241962809423954 -> 9.117182187865382 (281.22414487183346x)"),
                    new("DeltaTau at fixed E is UNBOUNDED", "contrast = 5f/(1 - f) with DeltaE = 0; f = 1/(1 + 5 e^{-3T})", "L1 -> 1.0625, rho_min -> 0: no target separation is forbidden by energy conservation"),
                    new("what bound it", "positivity and the G_005 Poisson band", "T = 4.8867e-6 needs f = 0.1666687028016166, contrast 1.0000146602074598"),
                ],
                "The two directions are not symmetric. rho is independent OF E (a 94-dimensional kernel: 51 by degeneracy plus 43 by lambda-mixing, both built explicitly) while E is fully determined BY rho. Holding rho fixed moves E only through the "
                + "capacity: the same uniform rho gives E = 2K exactly for K = 1..6, and K is BOUNDARY. At fixed energy the clock separation a pattern can demand is UNBOUNDED (contrast = 5f/(1 - f) with DeltaE = 0 throughout); the canonical 20 : 1 tilt is exactly the f = 0.8 member. "
                + "INDEPENDENT / DEPENDENT / REFUTED (ResearchY-G_016b)."),

            new("metric-coupling",
                "Metric Coupling — can a real |psi|^2 move a clock?",
                "DeltaTau/tau = (1/d) Delta ln I;  AT-naive 0.7675 .. 3.0701 vs GR 1e-47 .. 1e-58;  exclusion 6.667e17 .. 3.070e18",
                [
                    new("three readings", "AT-substrate (rho = actualization density) / AT-naive (rho ~ lab intensity I) / GR (m = U/c^2)", "all share DeltaTau/tau = Delta Phi/c^2 = (1/d) Delta ln rho"),
                    new("the observable", "a FRACTIONAL FREQUENCY RATIO", "so the ceiling is a clock's fractional resolution: 1e-18 (best), 1e-12 (crude systematic)"),
                    new("DERIVED: substrate reading", "Earth 2.320443e-10 = 2.320e8 x floor; galactic 5.367333e-7 = 0.046374 s/day = 5.367e11 x floor", "G_004's 0.99600 and G_009's 0.99668 -- NOT refuted"),
                    new("optical cavity", "F = 1e6, 1 W, 0.3 m; U = 6.370605e-4 J; Gaussian contrast 1e2", "AT-naive 0.6666666667 vs GR 3.509234e-47 -> 1.900e46; exclusion 6.667e17"),
                    new("resonator array", "Q = 1e7, 1 mW, 1550 nm; U = 8.228698e-12 J; u = 2.209714e6 J/m^3; on/off 1e4", "AT-naive 3.0701134573 vs GR 7.071071e-50 -> 4.342e49; exclusion 3.070e18"),
                    new("photon lattice", "Sr clock: E_rec = 2.272842e-30 J, 100 E_rec, 300 a.u. -> I = 2.439413e8 W/m^2; node/antinode 1e3", "AT-naive 2.3025850930; exclusion 2.303e18 -- a 230.26 % shift across its own lattice"),
                    new("oscillator network", "Q = 1e6, 1 pW, 6 GHz; U = 2.652582e-17 J; 10:1", "AT-naive 0.7675283643 vs GR 3.533090e-58 -> 2.17e57; exclusion 7.675e17"),
                    new("exclusion vs every ceiling", "1e-12 / 1e-15 / 1e-18 / 1e-19 leave 11 to 19 orders of exclusion", "no ceiling exists at which any realizable profile survives"),
                    new("why it is structural", "the law is LOGARITHMIC: DeltaTau/tau = (1/3) ln(contrast) is O(1) for any contrast > 10", "so the identification premise is EXPERIMENTALLY EXCLUDED at bench scale"),
                ],
                "The clock law is derived and its SUBSTRATE reading is consistent (0.99600 / 0.99668). But taking |psi|^2 literally for a real optical field gives O(1) predictions (77-307 %) where GR gives 1e-47 .. 1e-58 -- a "
                + "mismatch of 1.900e46 to 4.342e49, with the observation agreeing with GR. The sharpest case is a Sr lattice clock inside its own standing wave (2.439413e8 W/m^2, 1e-18 readability), which excludes the naive reading by 2.303e18. So the identification premise is BOUNDARY and its "
                + "naive form is REFUTED -- and the failure is structural, because the law is logarithmic. DERIVED / BOUNDARY / REFUTED (ResearchY-G_017)."),

            new("rho-identity",
                "Rho Identity — what remains after the lab reading is gone?",
                "I1..I4;  the occupancy measure;  sizes are scale-free;  51/95 = 53.6842 % pure arrangement;  zero imported constants",
                [
                    new("the criterion", "Q is the identity of rho iff (I1) counting-defined, (I2) dimensionless/normalised, (I3) survived G_017, (I4) determines rho", "no external physics enters the criterion"),
                    new("the ledger", "this audit imports NO constant", "contrast G_015/G_017: G, c, finesse, Q, polarizability -- and thereby falsifiable"),
                    new("DERIVED: occupancy measure", "96 cells, A0 = 45 eigenspaces, free room 51, state dimension N - 1 = 95", "Sigma rho = 1 and rho_min > 0 for every configuration"),
                    new("I3 is the pivot", "nothing in 'rho = counting' was ever a laboratory number", "so G_017 removed an IDENTIFICATION, not the quantity"),
                    new("I4 holds uniquely", "it IS rho: zero information loss", "L1(witness, witness) = 0 while L1(witness, uniform) = 0.6666666666666667"),
                    new("REFUTED: the count is a label", "exact scale invariance: max|a| = 0.6031746016455657 and clock sep 0.9985774245179969", "identical at scales 1, 1e3, 1e-6, 96 -> every observable is a RATIO"),
                    new("DERIVED content / BOUNDARY dimension", "the degeneracy occupancy is exactly what energy cannot see (DeltaE = 0)", "51/95 = 53.6842 % pure arrangement; 94/95 = 98.9474 % energy-invisible; dimension 51 = Sigma(m - 1) is BOUNDARY"),
                    new("REFUTED: survivor distribution", "a lossy functional: per-multiplet totals keep 44 of 95, E keeps 1 of 95", "compaction MOVES E, and two different states share the same survivor data"),
                    new("BOUNDARY: state accessibility", "K = 1..6 give A0 = 49/47/45/47/45/45 (free room N - A0)", "the SAME uniform rho for every lattice: a capacity input (G_007) that bounds the reachable set"),
                    new("what remains", "rho is an ARRANGEMENT: meaning entirely in the SHAPE of the occupancy, none in its SIZE", "nothing refers to energy, mass, intensity or a Born probability"),
                    new("the honest limit", "importing no constant makes this the MOST AT-NATIVE audit -- and UNFALSIFIABLE BY ITSELF", "the PROVENANCE ASYMMETRY: boundary-identified audits are falsifiable and where testable have been EXCLUDED or shown to be a RANK-1 SHADOW"),
                ],
                "rho's identity survives as the OCCUPANCY MEASURE: a dimensionless counting measure whose content is entirely in the arrangement. The count is a label (exact scale invariance), accessibility is the lattice, the degeneracy occupancy is the DERIVED content under a BOUNDARY dimension (51/95 = 53.6842 % pure arrangement), "
                + "and the survivor distribution is a lossy readout. The audit imports NO constant -- the most AT-native of the group and correspondingly unfalsifiable by itself -- which yields the provenance asymmetry: the theory's identity content is combinatorial and becomes physics only via a boundary identification. DERIVED / BOUNDARY / REFUTED (ResearchY-G_018)."),

            new("authored-verdict-audit",
                 "Authored-Verdict Audit \u2014 were the verdicts computed, or just typed in?",
                 "SYSTEMIC: four QG audits used IDENTICAL method names and differed only in literals; their sub-scores never read the criteria; their four test suites asserted CONTRADICTORY verdicts and all passed;  the Born-rule headline classification was DEAD CODE",
                 [
                     new("THE FINGERPRINT", "a member whose NAME claims a computation or verdict, whose VALUE is a literal, which a SCORE/CLASSIFICATION/TEST consumes", "detectors: 248 literal-bodied members feeding scores (of 1221); 16 members with declared-but-UNUSED parameters (decidable); 49 evidence-boolean literals at construction sites"),
                     new("FINDING 1", "the QG verdict ladder", "QuantumGravityClosureAudit (QG215 PARTIAL), ReclosureAudit (QG219 EFFECTIVE), ReclosureAudit2 (QG221 NEAR-COMPLETE) and FinalQuantumGravityAudit (QG223 COMPLETE) share method names and differ only in literals \u2014 PARTIAL -> COMPLETE was produced by EDITING A LITERAL"),
                     new("the decoupling", "QG221/QG223 sub-scores were typed 1.0s that NEVER READ the criteria", "TotalScore() returned 6.0 and Classify() returned COMPLETE QG whatever the criteria said; in ReclosureAudit2, IsSpacetimeEmergent() => false coexisted with SpacetimeSubScore() => 0.5"),
                     new("the tests", "ATQG_Phase215 asserts QM NOT derived; ATQG_Phase219/221/223 assert QM IS derived", "ALL FOUR PASS \u2014 four passing suites asserting mutually contradictory verdicts, and nothing to detect it"),
                     new("the fix", "QgCriterion: Name + Status(None/Partial/Full) + MANDATORY cited Basis + DERIVED Score", "every accessor, sub-score, total, classification and progression is a function of the criteria table; the derived ladder REPRODUCES 2.0 / 4.0 / 5.0 / 6.0 -> PARTIAL / EFFECTIVE / NEAR-COMPLETE / COMPLETE exactly; psi corrected to canonical (G_024) with PsiWasCalledNewPrimitive() preserving the record"),
                     new("FINDING 2", "the Born rule: Survives and the pass arrays were typed, and AllRequirementsUniquelySatisfied(reqs, tests) NEVER READ tests", "and the old uniqueness test returned false UNCONDITIONALLY (the factorization/orthogonality/additivity rows pass for ALL SIX exponents), so the headline \"D: alpha = 2 UNIQUELY selected\" was DEAD CODE \u2014 X037 output C: Strong Theorem"),
                     new("the alpha screen, EXECUTED", "N(psi) = Sum|psi_i|^alpha invariant under every unitary IFF alpha = 2", "deterministic (normalized DFT + Givens at fixed angles, no RNG), dims 2-5; max relative violation 2.343702 / 1.236068 / 0.4953488 / 2.220446e-16 / 0.5527864 / 0.8000000 for alpha = 0.5 / 1 / 1.5 / 2 / 3 / 4; RequirementEvidence = 4 Computed + 3 Analytic (partial trace, complexity additivity, linearity)"),
                     new("FINDINGS 3-5", "RAR: 7 of 11 rows typed Derived=true with the literal label DERIVED", "the three LIMIT ROWS are now computed from g_obs = g_bar*sqrt(1+g_dagger/g_bar): Newtonian ratio 1.000000000, deep-MOND ratio 1.000000000; G: computed in NewtonConstantOrigin (6.64670e-11) but duplicated as unchecked literals (0.014% from G_D96, 0.414% from CODATA); IdentityHoldsAcrossGrid(feedback, damping) declared the dynamics parameters and answered for the DEFAULTS"),
                     new("ROBUSTNESS DEFECT EN ROUTE", "the alpha selection compared FORMATTED NUMBER STRINGS", "culture-dependent \u2014 a comma-decimal culture would silently collapse the classification to a lower rung; now InvariantCulture + numeric comparison"),
                     new("BOUNDARY", "the criteria STATUSES remain the authored QG223 adjudication", "restructured, not re-adjudicated; 3 of 7 Born requirements remain ANALYTIC; (feedback, damping) do not move the tested spectrum (flagged, not claimed)"),
                 ],
                 "SYSTEMIC. The G_025 defect class is not isolated: four audits asking whether QM and gravity are derived used IDENTICAL method names and differed only in typed literals, so the escalation PARTIAL -> EFFECTIVE -> NEAR-COMPLETE -> COMPLETE was produced by editing a literal; the last two audits' sub-scores never read their own criteria, so the classification could not move; and the four test suites asserted mutually contradictory verdicts and ALL PASSED. Separately, the Born-rule headline classification was unreachable DEAD CODE, 7 of 11 RAR completion rows were typed as DERIVED, the derived constant G was duplicated as an unchecked literal, and a declared parameter silently answered for the defaults. All five are now DERIVED with cited bases and Computed/Analytic disclosure; the verdicts themselves are unchanged. Every claim now cites the phase carrying its evidence, and each piece of evidence is marked as computed or merely asserted. Lesson: a test that asserts a literal against itself is not evidence. DERIVED / BOUNDARY / REFUTED (ResearchY-G_026)."),

            new("optics-determinant-correction",
                 "Optics Determinant Correction \u2014 was the optics result actually derived?",
                 "NO: the psi-perturbed determinant had a d-vs-(d\u22121) OFF-BY-ONE (error unbounded in psi: 36.24 % at b = 0.3, 8901.71 % at b = \u22123) and gamma was a HARD-CODED CONSTANT;  corrected at 12 sites / 5 AT.Core files, gamma now DERIVED (\u22121 at psi = 0, +1 at the derived psi = \u22124 sigma, exact e^(6 sigma) = rho^2)",
                 [
                     new("DEFECT 1", "det g = \u2212rho^2 for ANY psi was the stated identity", "THE SPATIAL BLOCK HAS d FACTORS, NOT d \u2212 1: correct is det g = \u2212rho^(2(d+1)/d)e^(\u22122 psi/(d\u22121)); \u221a(\u2212det g) = rho^((d+1)/d)e^(\u2212psi/(d\u22121)); \u221a(det g_ij) = rho\u00b7e^(\u2212d psi/(d\u22121))"),
                     new("the off-by-one, numerically", "PerturbedVolumeElement(x, d, b) returned Profile(x, a) = rho BY CONSTRUCTION \u2014 it never computed a determinant", "at x = 1: b = 0.0 \u2192 spatial 2.0000000000, 4-volume 2.5198420998 (25.99 %); b = 0.3 \u2192 1.2752563032 (36.24 %); b = 0.5 \u2192 0.9447331055 (52.76 %); b = \u22123.0 \u2192 180.0342626010 (8901.71 %), 4-volume 11.2931487976 (464.66 %)"),
                     new("why it was invisible", "the shipped test asserted sameVolume == true for every psi", "the error is UNBOUNDED in psi, so a hard-coded return could never disagree with itself"),
                     new("CONSEQUENCE \u2014 the choice is STRENGTHENED", "'\u221a(\u2212g) = rho is preserved for ANY psi' is FALSE", "psi = 0 is the ONLY member that preserves the counting measure: PsiPerturbationPreservesMeasure() is now FALSE and PsiPerturbationBreaksMeasure() / ConformalIsTheMeasurePreservingMember() are TRUE \u2014 STRONGER than QG207's claim"),
                     new("DEFECT 2", "GammaPsiNonZero() => NonTensorLensing.GrGamma() = +1.0, GammaPsiZero() => ConformalGamma() = \u22121.0", "no code path computed gamma from the metric, so origin-score items 2/3 were ((1+1)/2 = 1) and (Shapiro(1) = (1+1)/2\u00b72 = 2) \u2014 arithmetic on a literal, unable to fail"),
                     new("the coded psi form cannot give gamma = +1", "psi = b\u00b7x is LINEAR; gamma = +1 needs psi = \u22122 sigma (d\u22121)/(d\u22122) = \u22124 sigma, i.e. \u2212(4/3) ln(1 + a x^2) \u2014 QUADRATIC", "gamma(psi = b x) with b = 0.3: +0.3352 / +0.0022 / \u22120.0930 / \u22120.0694 at x = 0.1 / 0.5 / 1.0 / 2.0; with b = 0.5: +0.3772 / +0.1054 / +0.0112 / \u22120.0037"),
                     new("DERIVED now", "GammaFromPsiMetric = (g_ii \u2212 1)/(g_00 + 1), exact; GammaFromPsiMetricFirstOrder = (psi \u2212 2 sigma)/(2(sigma + psi))", "at rho = 1.000001 / 1.5 / 2.0: gamma(psi = 0, exact) = \u22121.0000000000 in every row; gamma(psi = \u22124 sigma, 1st order) = +1.0000000000 in every row; exact = +1.0000020001 / +2.2500000000 / +4.0000000000 = e^(6 sigma) = rho^2 at d = 3 \u2014 +1 ONLY IN THE WEAK FIELD"),
                     new("TRM matrix \u2014 one honest dent", "the \"metric-origin\" row said UNCHANGED", "it is MODIFIED (the psi sector does not preserve \u221a(\u2212g) = rho): corrected matrix is 4 UNCHANGED (counting measure, matter-deficit, alpha = 0 attractor, critical branching) / 2 MODIFIED (metric origin, Einstein structure) / 0 BROKEN"),
                     new("BOUNDARY", "at psi = \u22124 sigma the EXACT gamma drifts as e^(6 sigma) = rho^2", "the same psi shifts the CLOCK law \u221a(\u2212g_00) = rho^(1/d)e^(psi) by e^(\u22124 sigma) = rho^(\u22124/3) \u2014 2.784532e\u22129 at Earth (invisible; GPS tests 0.2 %) but O(1) at compactness, so the O(x^2) completion is LOAD-BEARING AND UNSPECIFIED; AT.Core now exposes PsiClockShiftFactor, PsiSectorShiftsClock, PsiSectorBreaksMeasure and GammaPsiNonZeroExact"),
                     new("SCORECARD", "1 of QG212\u2019s 4 origin-score points was substantive before the fix", "only gamma = \u22121 for the conformal slice (verified independently three times); OPTICS RESOLVED (4/4) is RESTORED on the corrected basis, with the boundary above; Record: Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md (AT-QG phase 320)"),
                 ],
                 "NO \u2014 the optics result was restored by G_024 on the strength of its own documentation, and independent verification (ResearchY-G_025) finds that its arithmetic did not hold up. "
                 + "TWO REAL DEFECTS. (1) The psi-perturbed determinant was stated as det g = \u2212rho^2 for ANY psi, and PerturbedVolumeElement returned rho BY CONSTRUCTION; the spatial block has d factors, not d \u2212 1, so the correct identities are det g = \u2212rho^(2(d+1)/d)e^(\u22122 psi/(d\u22121)), \u221a(\u2212det g) = rho^((d+1)/d)e^(\u2212psi/(d\u22121)) and \u221a(det g_ij) = rho\u00b7e^(\u2212d psi/(d\u22121)). The error is unbounded in psi \u2014 0.00 / 36.24 / 52.76 / 8901.71 % at b = 0.0 / 0.3 / 0.5 / \u22123.0 at x = 1 \u2014 which is exactly why the shipped test (asserting sameVolume == true) could never catch it. (2) gamma was a HARD-CODED CONSTANT: GammaPsiNonZero() returned GrGamma() = 1.0 and GammaPsiZero() returned ConformalGamma() = \u22121.0, so no path computed gamma from the metric, origin-score items 2 and 3 were arithmetic on a literal, and nothing detected that the coded psi = b x has gamma = +0.3352 / +0.0022 / \u22120.0930 / \u22120.0694 at x = 0.1 / 0.5 / 1.0 / 2.0 for b = 0.3. "
                 + "CORRECTED at 12 sites across 5 AT.Core files: the determinant computes; the measure premise is fixed (psi = 0 is now the UNIQUE measure-preserving member \u2014 STRONGER than QG207 claimed); gamma is DERIVED (\u22121 at psi = 0 and +1 at the derived psi = \u22124 sigma to first order, exact e^(6 sigma) = rho^2 at d = 3, so +1 only in the weak field); ConformalIsRestrictedSector is rebased on the corrected member test; and TRMCompatibilityAudit\u2019s \"metric-origin\" row moves UNCHANGED \u2192 MODIFIED (matrix 4 / 2 / 0). "
                 + "The optics CONCLUSION is RESTORED on derived grounds. What is withdrawn is the measure-preservation premise, the determinant identity, the metric-origin row, and the hard-coded gamma. The BOUNDARY stands: at psi = \u22124 sigma the clock shifts by e^(\u22124 sigma) = rho^(\u22124/3) (2.784532e\u22129 at Earth, O(1) at compactness), so the O(x^2) completion is load-bearing and unspecified. "
                 + "Scorecard: 1 of QG212\u2019s 4 origin-score points was substantive before the fix. A document marked RESOLVED with green tests is not the same as a resolved derivation, and a test that cannot fail is not a test. DERIVED / BOUNDARY / REFUTED (ResearchY-G_025; AT-QG phase 320)."),

            new("optics-reconciliation",
                 "Optics Reconciliation \u2014 was light bending already solved?",
                 "YES: QG212 (Status COMPLETE / OPTICS RESOLVED) gives psi = 0 -> gamma = \u22121 (lensing = Shapiro = frame dragging = 0) and psi != 0 -> gamma = +1 (full GR optics), with NO new primitives;  gamma = \u2212B/A so psi = 0 <=> A = B and gamma = +1 <=> A + B = 0",
                 [
                     new("the canonical result", "QG212: Status COMPLETE \u2014 OPTICS RESOLVED, tests ATQG2120/2121/2122, core class AT.Core/ResearchXH/ConformalOpticsResolution.cs", "two sectors: psi = 0 -> gamma = \u22121, lensing 0, Shapiro 0, frame dragging 0, redshift yes; psi != 0 (QG207) -> gamma = +1, lensing GR, Shapiro GR, frame dragging restored"),
                     new("verdict as stated", "conformal no-lensing is a RESTRICTED SECTOR", "'not a numerical artifact' (gamma = \u22121 is exact in the slice), 'not physical GR' (the slice is an isotropic assumption), 'the physical sector is psi != 0'; method line: 'no new primitives'"),
                     new("rebuilt executably", "QG207: g00 = \u2212\u03c1^(2/d)e^(2\u03c8), g_ii = \u03c1^(2/d)e^(\u22122\u03c8/(d\u22121)); gamma = h_ii/h00", "psi = 0 -> gamma = \u22121 EXACTLY ((1+gamma)/2 = 0); psi = \u22124 sigma -> gamma = +1 TO FIRST ORDER ((1+gamma)/2 = 1), exact value e^(\u22126x)"),
                     new("RETRACTION 1", "psi is NOT a new primitive", "trace/traceless of the one Difference at d = 3: 6 = 1 trace (\u03c1) + 5 traceless, 2 TT (\u03c8, spin-2); minimal set {Difference, \u03b7} (QG285/QG286/QG292) \u2014 the G_021/G_022/G_023 'MINIMAL NEW PRIMITIVE (QG24)' wording is WITHDRAWN"),
                     new("RETRACTION 2", "the psi route is NOT empty", "G_022 \u00a76 derived psi = 0 by assuming the \u03c1-only clock law is non-negotiable \u2014 but that IS the psi = 0 slice; the physical sector has \u221a(\u2212g00) = \u03c1^(1/d)e^(\u03c8)"),
                     new("RETRACTION 3", "G_023's verdict reason is void", "it rested on psi being a new primitive (RETRACTION 1). Its technical content SURVIVES: gamma = \u2212B/A, so psi = 0 <=> A = B <=> gamma = \u22121 and gamma = +1 <=> A + B = 0"),
                     new("what survives", "the psi = 0 slice's measured exclusion", "Cassini 8.6957e4 \u03c3, VLBA 6.6660e3 \u03c3, Gaia 1.2481e2 \u03c3 \u2014 QG26 gives the bare gamma = \u22121; G_021 gives the number"),
                     new("two realisations of A + B = 0", "keep the clock law (A = \u03c3, B = \u2212\u03c3 = +x) gives G_023's k = B \u2212 A = 2x", "the QG207 completion at psi = \u22124 sigma gives A = \u22123\u03c3, B = +3\u03c3, k = \u22126x \u2014 so G_023's identity is the CLOCK-LAW-PRESERVING form"),
                     new("the open item added", "\u03b3 = +1 requires psi = \u22124 sigma, so the completion is NOT redshift-neutral: e^(4x) \u2212 1 = 2.784532e\u22129 at Earth, 8.490012e\u22126 at the Sun", "far below GPS (0.2 %) and Cassini (2.3e\u22125) so NO solar-system conflict; the GPS bound |psi| <= 2e\u22123 is 7.2e5x looser than required \u2014 but at compactness the leading-order z = e^(\u22123x) \u2212 1 turns NEGATIVE (\u22120.523366 at x = 0.247002), so the O(x\u00b2) completion is LOAD-BEARING AND UNCOMPUTED: a BOUNDARY flag, not a refutation"),
                 ],
                 "YES \u2014 light bending was already solved, by the AT-QG optics resolution QG212 (Status COMPLETE \u2014 OPTICS RESOLVED, tests ATQG2120/2121/2122). The psi = 0 conformal slice gives gamma = \u22121 with lensing = Shapiro = frame dragging = 0; the psi != 0 sector (QG207) gives gamma = +1 with full GR optics and NO new primitives \u2014 psi is the TRACELESS FACE of the one Difference read against \u03b7, so the minimal primitive set is {Difference, \u03b7}. "
                 + "The G-chain mis-stated this and ResearchY-G_024 retracts three claims (psi as a 'new primitive'; G_022 \u00a76 'the psi route is empty'; G_023's verdict reason). What survives is the quantitative part: the psi = 0 slice is Cassini-excluded at 8.6957e4 sigma, and the invariant split is psi = 0 <=> A = B <=> gamma = \u22121 with gamma = +1 <=> A + B = 0. "
                 + "One genuine open item is added: the QG207 completion is not redshift-neutral (shift e^(4x) \u2212 1, invisible in the solar system but turning the leading-order compactness redshift negative), so the O(x\u00b2) form is load-bearing and uncomputed. DERIVED / BOUNDARY / REFUTED (ResearchY-G_024)."),

            new("spatial-closure",
                 "Spatial Sector Closure \u2014 can any spatial metric give gamma ~ +1 without new primitives?",
                 "gamma = \u22121 + k/x with k := B \u2212 A (isotropic form);  k is the conformal invariant;  AT pins k = 0 twice (A = \u03c3 from the clock law, B = \u03c3 from the counting measure) so gamma = \u22121 identically;  gamma = +1 needs k = 2x at a volume cost of 3.437585",
                 [
                     new("isotropic form is where PPN gamma lives", "ds\u00b2 = \u2212e^(2A)dt\u00b2 + e^(2B)(dR\u00b2 + R\u00b2d\u03a9\u00b2), \u03a6 = (e^(2A)\u22121)/2", "gamma = \u2212(e^(2B)\u22121)/(e^(2A)\u22121) \u2248 \u22121 + k/x with k := B \u2212 A: exactly TWO functions, ONE controlling number"),
                     new("k is the conformal invariant", "g \u2192 \u03a9\u00b2g shifts A and B by the same \u03c9, so k = B \u2212 A is unchanged", "k is exactly the class data of the causal-order \u2192 conformal-class step; conformal flatness \u21d4 A = B \u21d4 g_rr = \u2212g00 \u21d4 k = 0 \u21d4 gamma = \u22121 for EVERY A"),
                     new("given: the clock law", "\u221a(\u2212g00) = \u03c1^(1/d) = e^\u03c3", "A = \u03c3 \u2014 the first pin"),
                     new("given: the counting measure", "the spatial volume element IS the count: \u221a(det g_ij) = \u03c1 \u21d2 e^(3B) = \u03c1", "B = \u03c3 \u2014 the second pin, from the SAME primitive, at the SAME scalar"),
                     new("so k = 0 identically", "Earth \u22126.96133e\u221210, Sun \u22122.122503e\u22126, 1e\u22124, J0740+6620 \u22120.247002", "volume/\u03c1 = 1.000000000 and gamma = \u22121 at EVERY compactness \u2014 a theorem about AT's construction, not a coincidence"),
                     new("what gamma = +1 costs", "keep A = \u03c3 (g00 physics unchanged) and demand gamma = +1: e^(2B) = 2 \u2212 e^(2A) \u21d2 B = \u00bdln(2 \u2212 e^(\u22122x))", "k = 2x to first order; the spatial volume becomes e^(3k) = 1.000013 at the Sun but 3.437585 at J0740+6620 (B = 0.1645877, k = 0.4115897 vs k_lin = 0.494004)"),
                     new("the primitive inventory", "Q-event counts, \u03c1, \u03c3, DiffuseStep \u039b = I \u2212 W, the D96 lattice, spectral \u03bb, information", "ALL SCALARS: each fixes ONE function; any \u03b4 = B \u2212 \u03c3 breaks the volume by e^(3\u03b4) and moves gamma to \u22121 + \u03b4/x; none supplies k = 2x"),
                     new("the one non-scalar ingredient", "the causal ORDER \u2014 which supplies the conformal CLASS", "AT\u2019s order is the FLAT D96 ring order: 96 cells \u2192 45 eigenvalues (histogram {1:1, 2:42, 5:1, 6:1}, free room 51) vs the degeneracy-free random control\u2019s 96 distinct, so its class is [\u03b7] and k = 0"),
                     new("the complement candidates are constants", "51/95 = 0.536842, the 95 \u2192 44 \u2192 1 chain = 2.159091, possible/accessible = 3.746e5, spectral total 1152", "NONE matches 1/\u03c1 at any compactness, so none can supply a field-valued reciprocal volume"),
                     new("the success criterion, split", "half one: A = \u03c3 kept exactly \u2014 the three laws and z_AT = 0.2801817 untouched", "half two: gamma = \u22121 is forced and excluded (Cassini 8.6957e4 / VLBA 6.6660e3 / Gaia 124.8125 \u03c3) while gamma = +1 (0.9130 / 0.6667 / 0.1875 \u03c3) needs k = 2x; flat space is REFUTED too \u2014 so the criterion is met only by POSTULATE"),
                 ],
                 "NO. In isotropic form (where PPN gamma is defined) gamma = \u22121 + k/x with k := B \u2212 A, and k is the CONFORMAL INVARIANT \u2014 unchanged by any rescaling, hence exactly the class data the causal-order step supplies; conformal flatness \u21d4 k = 0 \u21d4 gamma = \u22121 for every A. "
                 + "AT pins k = 0 from TWO independent directions: the clock law gives A = \u03c3 and the counting measure (the spatial volume IS the count) gives B = \u03c3 \u2014 the same primitive, the same scalar \u2014 so k = 0 identically at every compactness. "
                 + "gamma = +1 would need e^(2B) = 2 \u2212 e^(2A), i.e. k = 2x, at the price of a spatial volume 3.437585x the counting measure at J0740+6620 \u2014 which is changing a primitive. The inventory closes it: every existing ingredient is a scalar, and the one non-scalar ingredient (the causal order) is the flat D96 ring order. "
                 + "The success criterion is met only by postulate: the g00 half is satisfiable exactly, the gamma half is not. DERIVED / BOUNDARY / REFUTED (ResearchY-G_023). CORRECTED BY RESEARCHY-G_024 (Optics Reconciliation Audit): the claim that psi is a 'MINIMAL NEW PRIMITIVE' and that gamma = +1 is out of derivational reach is SUPERSEDED and WITHDRAWN. The AT-QG optics resolution QG212 (Status COMPLETE - OPTICS RESOLVED, tests ATQG2120/2121/2122) classes the psi = 0 conformal slice as a RESTRICTED SECTOR; the physical sector is psi != 0, giving gamma = +1 with lensing, Shapiro and frame dragging at full GR, with NO new primitives, since psi is the TRACELESS FACE of the one Difference read against eta (minimal set {Difference, eta}; QG285/QG286/QG292). What STANDS is the quantitative part: the psi = 0 slice is Cassini-excluded at 8.6957e4 sigma, and the invariant split is psi = 0 <=> A = B <=> gamma = -1, gamma = +1 <=> A + B = 0."),

            new("spatial-metric",
                 "Spatial Metric \u2014 can a derived spatial metric give gamma ~ +1?",
                 "The three laws (clock, source, acceleration) are gamma-BLIND;  conformal flatness forces g_rr = \u2212g00 hence gamma = \u22121 for ANY factor;  gamma = +1 needs TWO factors (g_rr = rho^(\u22122/d)) and is therefore a POSTULATE",
                 [
                     new("the three laws are gamma-blind", "clock d\u03c4/dt = \u221a(\u2212g00) = \u03c1^(1/d) \u00b7 source a = \u2212(1/d)\u2207ln \u03c1 \u00b7 slow-particle acceleration \u2212A\u2032", "depends on g00 and rho only \u2014 the spatial exponent B never appears, so the question as posed constrains NOTHING about gamma"),
                     new("only B-dependent quantity", "the proper acceleration of a HELD observer: a\u0302 = A\u2032e^(\u2212B)", "conformal-vs-reciprocal deviation e^(2x) \u2212 1 = 1.392e\u22129 at the Earth's surface \u2014 a measure convention, below every stated precision"),
                     new("conformal flatness forces gamma = \u22121", "g = \u03a9\u00b2\u03b7 \u21d2 g00 = \u2212\u03a9\u00b2, g_rr = +\u03a9\u00b2 \u21d2 g_rr = \u2212g00 ALWAYS", "\u03b3 = \u2212(g_rr \u2212 1)/(2\u03a6) = \u2212(\u03a9\u00b2 \u2212 1)/(\u03a9\u00b2 \u2212 1) = \u22121 for EVERY \u03a9\u00b2 \u2260 1"),
                     new("verified for five factors", "\u03a9\u00b2 = \u03c1^(2/d) 0.999998000002 \u00b7 \u03c1^(\u22122/d) 1.000002000002 \u00b7 e^(\u22120.6x) 0.999999400000 \u00b7 1.5 \u00b7 0.9", "ALL give exactly \u22121 \u2014 so gamma = \u22121 is a THEOREM, not an artefact of the counting-measure factor"),
                     new("the beta-family", "g_rr = e^(2\u03b2\u03c3) \u21d2 gamma = \u2212\u03b2", "\u03b2 = +1 conformal \u2192 \u22121; \u03b2 = 0 flat space \u2192 0; \u03b2 = \u22121 reciprocal \u2192 +1"),
                     new("the exclusion table", "gamma = \u22121: 8.6957e4 / 6.6660e3 / 124.8125 sigma", "gamma = 0: 4.3479e4 / 3.3327e3 / 62.3125 sigma (REFUTED too); gamma = +1: 0.9130 / 0.6667 / 0.1875 sigma (ALLOWED)"),
                     new("gamma = +1 passes every requirement", "g00 = \u2212\u03c1^(2/d) kept, g_rr = +\u03c1^(\u22122/d): clock exact, source exact, acceleration exact, z_AT = 0.2801817 identical to G_020", "and NOT conformally flat: g_rr + g00 = 2 sinh(2x) = 1.028687 at x = 0.247002 \u2014 so the causal-order \u2192 conformal-class step does not produce it: a POSTULATE"),
                     new("the price", "native \u03a9 = \u03c1^(1/d) gives volume measure \u03a9\u00b3 = \u03c1 EXACTLY", "gamma = +1 needs \u03a9 = \u03c1^(\u22121/d) \u21d2 \u03a9\u00b3 = 1/\u03c1 \u2014 the RECIPROCAL of the counting measure"),
                     new("the psi route is EMPTY", "A = \u03c3 + \u03c8: \u2212A\u2032 = \u2212\u03c3\u2032 \u21d2 \u03c8\u2032 = 0; then e^A = e^\u03c3 \u21d2 \u03c8 = 0", "FORCED BY BOTH LAWS \u2014 the psi completion gives gamma = \u22121, not +1 (refines G_021)"),
                     new("and at psi = \u22124\u03c3 the redshift turns BLUE", "z = e^(\u22123x) \u2212 1 = \u22122.0e\u22129 at Earth, \u22126.4e\u22126 at the Sun, \u22120.523366 at J0740+6620", "a NEGATIVE surface shift at a bound object \u2014 refuted by any positive measured neutron-star redshift; clock violation 2.785e\u22129 at Earth vs 1.685879 (169 %) at a neutron star"),
                     new("nothing earlier is damaged", "every G_001\u2013G_020 agreement with GR is gamma-blind", "0.99600, the clock rates and \u0394\u03c4 = 86 277.089 s/day, the x\u00b2 signature, z_AT = 0.2801817 and the source-law/MOND phenomenology are ALL invariant under any spatial sector"),
                 ],
                 "NO - structurally. The three laws the question names are exactly the three that never look at the spatial sector, so they cannot decide gamma; the condition that does decide it is the conformal closure, and conformal flatness forces gamma = \u22121 for ANY factor (a theorem, verified for five factors including the reciprocal). "
                 + "gamma = +1 needs TWO DIFFERENT FACTORS \u2014 g00 = \u2212\u03c1^(2/d) kept and g_rr = +\u03c1^(\u22122/d) \u2014 which preserves all three laws exactly, keeps the redshift identical to G_020 and passes Cassini at 0.913 sigma, but is NOT conformally flat and is therefore a POSTULATE whose cost is a spatial volume measure of 1/\u03c1 instead of \u03c1. "
                 + "The psi route is EMPTY: psi' = 0 then psi = 0 are forced by both laws, and at psi = \u22124 sigma the surface redshift is \u22120.523366 at J0740+6620 (a blueshift at a bound object). Nothing earlier is damaged. DERIVED / BOUNDARY / REFUTED (ResearchY-G_022). CORRECTED BY RESEARCHY-G_024 (Optics Reconciliation Audit): the claim that psi is a 'MINIMAL NEW PRIMITIVE' and that gamma = +1 is out of derivational reach is SUPERSEDED and WITHDRAWN. The AT-QG optics resolution QG212 (Status COMPLETE - OPTICS RESOLVED, tests ATQG2120/2121/2122) classes the psi = 0 conformal slice as a RESTRICTED SECTOR; the physical sector is psi != 0, giving gamma = +1 with lensing, Shapiro and frame dragging at full GR, with NO new primitives, since psi is the TRACELESS FACE of the one Difference read against eta (minimal set {Difference, eta}; QG285/QG286/QG292). What STANDS is the quantitative part: the psi = 0 slice is Cassini-excluded at 8.6957e4 sigma, and the invariant split is psi = 0 <=> A = B <=> gamma = -1, gamma = +1 <=> A + B = 0."),

            new("light-propagation",
                 "Light Propagation \u2014 does the theory bend light? (corrects G_019)",
                 "g_uv = \u03c1^(2/d)\u03b7_uv is conformally flat;  g_rr = +(1 + 2\u03c3) = 1 \u2212 2\u03b3\u03a6 with \u03a6 = \u03c3  \u21d2  \u03b3 = \u22121 EXACTLY;  \u03b3 = +1 \u21d4 \u03c8 = \u22124\u03c3 (d = 3)",
                 [
                     new("the metric is closed, not missing", "g_uv = \u03c1^(2/d)\u03b7_uv, so g00 = \u2212\u03c1^(2/d) AND g_rr = +\u03c1^(2/d)", "MetricOriginClosure.md: class \u00d7 factor = full g_uv, CLOSED; conformal class IMPORTED (Malament 1977, proven), factor NATIVE (the counting measure)"),
                     new("the source-law identification", "a = \u2212(1/d)\u2207ln \u03c1 = \u2212\u2207\u03c3 with \u03c3 = (1/d) ln \u03c1", "fixes \u03a6 = \u03c3 - the same identification that gives G_004's 0.99600"),
                     new("gamma = \u22121 EXACTLY", "g_rr = +(1 + 2\u03c3) = 1 \u2212 2\u03b3\u03a6 \u21d2 \u03b3 = \u22121", "exact, not first order: \u03a6 = (e^(2\u03c3) \u2212 1)/2 gives \u03b3 = \u2212(e^(2\u03c3) \u2212 1)/(e^(2\u03c3) \u2212 1) = \u22121 for every \u03c3 \u2260 0"),
                     new("no optics at all", "every lensing observable \u221d (1 + \u03b3)/2 (QG26) = 0", "deflection = \u03ba = shear = \u03bc\u22121 = Shapiro delay = 0, while the REDSHIFT survives (g00 alone): redshift WITHOUT lensing"),
                     new("REFUTED - the exclusion", "Cassini (2003) \u03b3 = 1.0000210 +- 2.3e\u22125", "8.6957e4 sigma; VLBA (2009) 6.6660e3 sigma; Gaia (2022) 1.2481e2 sigma; plus thousands of observed lensing systems"),
                     new("the escape - and its price", "g00 = \u2212\u03c1^(2/d)e^(2\u03c8), g_ii = \u03c1^(2/d)e^(\u22122\u03c8/(d\u22121)) gives \u03b3 = +1 (QG207/QG212)", "\u03b3(\u03c8) = \u2212[\u03c3 \u2212 \u03c8/(d\u22121)]/(\u03c3 + \u03c8); \u03b3 = +1 \u21d4 \u03c8 = \u22124\u03c3 in d = 3 (\u22123\u03c3 in d = 4; NO solution in d = 2, G_uv = 0)"),
                     new("the requirement is NOT free", "\u03a6 = \u03c3 + \u03c8 = \u22123\u03c3, not \u03c3", "contradicts a = \u2212(1/d)\u2207ln \u03c1, so restoring \u03b3 = +1 CHANGES THE SOURCING RELATION; \u03c8 is a MINIMAL NEW PRIMITIVE (QG24; QG43: a 1-d.o.f. scalar suffices)"),
                     new("the clock carries psi at first order", "with \u03c8 \u2260 0, d\u03c4/dt = \u03c1^(1/d)e^(\u03c8) and e^\u03c8 = 1 + \u03c8 + \u2026", "at \u03c8 = 1e\u22123 the contamination is 1000.5\u00d7 G_019's x\u00b2 term, so any nonzero \u03c8 SWAMPS the second-order signature"),
                     new("why nothing earlier is damaged", "every audit G_001\u2013G_020 used g00 ONLY", "the source law, the clock law, 0.99600, 0.99668, the x\u00b2 signature and z_NICER all STAND; the chain is SILENT on optics, not wrong"),
                 ],
                 "CORRECTION: G_019 \u00a75 said AT supplies no spatial metric, so no light bending, Shapiro delay or shadow size follows - the most distinctive consequence being the least derivable one. BOTH PARTS ARE FALSE. "
                 + "AT DOES supply the full metric and the chain is CLOSED: g_uv = rho^(2/d) eta_uv is CONFORMALLY FLAT, so g00 = -rho^(2/d) AND g_rr = +rho^(2/d); the conformal class is IMPORTED (Malament 1977) and the factor rho^(2/d) NATIVE (the counting measure). "
                 + "THE OPTICS: with sigma = (1/d) ln rho, the source law fixes Phi = sigma, so g_rr = +(1 + 2 sigma) = 1 - 2 gamma Phi gives gamma = -1 EXACTLY; every lensing observable is proportional to (1 + gamma)/2, so deflection, kappa, shear and the Shapiro delay all vanish while the REDSHIFT survives - AT predicts redshift WITHOUT lensing. "
                 + "That is a DERIVED FALSIFICATION, not a missing derivation: Cassini separates at 8.6957e4 sigma, VLBA at 6.6660e3 sigma, Gaia at 1.2481e2 sigma. "
                 + "THE ESCAPE AND ITS PRICE: the psi-completed metric gives gamma = +1, and this audit DERIVES that it requires psi = -2 sigma (d-1)/(d-2) = -4 sigma in d = 3 (no d = 2 solution); along that direction the exact gamma is e^(6 sigma). But Phi = sigma + psi = -3 sigma then contradicts the native source law, so the psi sector necessarily CHANGES THE SOURCING RELATION - it is a new primitive, not a free patch. CORRECTED BY RESEARCHY-G_024 (Optics Reconciliation Audit): the claim that psi is a 'MINIMAL NEW PRIMITIVE' and that gamma = +1 is out of derivational reach is SUPERSEDED and WITHDRAWN. The AT-QG optics resolution QG212 (Status COMPLETE - OPTICS RESOLVED, tests ATQG2120/2121/2122) classes the psi = 0 conformal slice as a RESTRICTED SECTOR; the physical sector is psi != 0, giving gamma = +1 with lensing, Shapiro and frame dragging at full GR, with NO new primitives, since psi is the TRACELESS FACE of the one Difference read against eta (minimal set {Difference, eta}; QG285/QG286/QG292). What STANDS is the quantitative part: the psi = 0 slice is Cassini-excluded at 8.6957e4 sigma, and the invariant split is psi = 0 <=> A = B <=> gamma = -1, gamma = +1 <=> A + B = 0."
                 + "Every G_001-G_020 result is g00-only and stands. DERIVED / BOUNDARY / REFUTED (ResearchY-G_021)."),

            new("second-order-signature",
                 "Second-Order Signature — is there a test that can decide it?",
                 "AT: e^x vs GR: sqrt(1+2x);  ratio = 1 + x^2 - (4/3)x^3;  z gap 17-31 %;  1.033 sigma now",
                 [
                     new("the signature", "AT dtau/dt = e^x against GR sqrt(1 + 2x)", "first order IDENTICAL, second order OPPOSITE SIGN: ratio = 1 + x^2 - (4/3)x^3 + ..."),
                     new("the discriminator is x squared", "which decides the whole strategy", "no solar-system or white-dwarf measurement can reach x^2"),
                     new("sign and series", "AT clocks run FASTER, so AT's redshift is always SMALLER", "(ratio-1)/x^2 = 1.0000889006 at -1e-6, 1.0135879522 at -1e-2, 1.2774576758 at -0.15"),
                     new("REFUTED: the weak-field route", "Earth 4.845934e-19 = 0.4846x the 1e-18 floor (2.1x short)", "ground-vs-GPS 4.567944e-19; Sun 4.505017e-12 vs 1e-5 (2.2e6x short); Sirius B 6.619702e-8 vs 2 % (3.3e6x short)"),
                     new("unrepresentable", "at x = 1e-9 the difference 1e-18 is below one ulp of 1.0 (1.11e-16)", "the arithmetic cannot express the signature at solar-system depths"),
                     new("the live arena", "NICER neutron stars: J0030+0451 -17.367 %, J0740+6620 (Riley) -30.957 %, (Miller) -27.463 %", "3-10 % in the rate and 17-31 % in the redshift; the J0740 z gap is 0.1256 absolute"),
                     new("the inverse map", "for a measured z, AT needs a smaller radius: z = 0.35 gives 6.890 km against GR's 9.164 km", "so a NICER-compatible object (R ~ 11-14 km) cannot have z = 0.35 under AT"),
                     new("AT SURVIVES", "M = 1.4 +- 0.05 Msun, R = 12 +- 1 km: z_AT = 0.188055 +- 0.024372 vs z_GR = 0.235259 +- 0.038665", "separation 0.047205 against sigma 0.045706 = 1.033 sigma; 3 sigma needs sigma_z <= 8.37 % of z_AT, and current NS redshifts are 20-50 %, short by 2.4x-6.0x"),
                     new("why it survives", "the signal is QUADRATIC, not small", "so only the compact-object test is live, and the deciding measurement is now specified exactly"),
                     new("the horizon corollary", "AT's g00 = -e^(2x) NEVER VANISHES: at y = 1/2 AT gives z = 0.6487213 where GR's DIVERGES", "for y > 1/2 GR has no real surface while AT gives 0.8221188 / 1.718282 / 147.4132"),
                     new("but it is g00-ONLY", "every number here uses g00 only - the audit is silent on optics", "CORRECTED by G_021: AT DOES have the spatial metric (g = rho^(2/d) eta, g_rr = +rho^(2/d)); its derived optics are gamma = -1, so all lensing and the Shapiro delay vanish and Cassini excludes that at 8.6957e4 sigma"),
                 ],
                 "OP1 answer: YES - an audit that imports measured constants and SURVIVES. The signature AT/GR = 1 + x^2 - (4/3)x^3 makes the weak-field route useless (2.1x to 3.3e6x short at four imported depths) and makes the neutron-star redshift the only live test, where "
                 + "the divergence is 17-31 % but the current significance is only 1.033 sigma; 3 sigma needs sigma_z = 8.37 % of z_AT, i.e. 2.4x-6.0x beyond current measurements. The largest discrepancy (no horizon, where GR's z diverges) is g00-only. CORRECTION (G_021): AT DOES supply the spatial metric g = rho^(2/d) eta, and its derived optics are gamma = -1 EXACTLY - all lensing observables and the Shapiro delay vanish while the redshift survives - which Cassini EXCLUDES at 8.6957e4 sigma; gamma = +1 requires the psi sector with psi = -4 sigma, which also moves Phi to -3 sigma and so changes the source law. Every number in G_019 is g00-only and stands. "
                 + "DERIVED / BOUNDARY / REFUTED (ResearchY-G_019)."),

            new("neutron-star-redshift",
                 "Neutron-Star Redshift — can any measured value decide it?",
                 "z_AT = e^x - 1 vs z_GR = (1-2x)^(-1/2) - 1;  max S with perfect z = 1.334;  5 sigma: sigx/x <= 2.589 % and sz <= 0.017766",
                 [
                     new("the framework", "x = GM/(Rc^2); S = Dz / sqrt(sz^2 + (dz/dx)^2 sx^2), dz/dx = (1-2x)^(-3/2)", "AT always predicts the SMALLER redshift; the midpoint decides which law is closer"),
                     new("the error budget is binding", "max significance with a PERFECT redshift (sz = 0)", "J0030 (Riley) 0.614; J0030 (Miller) 0.752; J0740 (Riley) 1.334; J0740 (Miller) 0.778; generic 1.221"),
                     new("no object reaches 2 sigma", "(dz/dx) sx exceeds Dz/2, Dz/3 and Dz/5 for every object", "so no redshift precision whatsoever can reach 3 sigma, let alone 5 sigma"),
                     new("ALLOWED", "a representative z = 0.30 with a 20 % systematic (sz = 0.06)", "AT inside 2.5 sigma everywhere, 0.33 sigma at 2.072 Msun/12.39 km; GR also allowed, so no discrimination"),
                     new("PREFERRED - NOT supported", "ln LR(AT/GR) at sz = 0.05 runs 1.718 down to 0.068 across the grid", "only 1 of 9 grid points favours AT; data lean toward GR for R >= 11 km"),
                     new("why the apparent preference dissolves", "it borrows one object's redshift for another object's compactness", "the z values are not measured on the NICER objects at all"),
                     new("the radius inversion", "a measured z demands a SMALLER radius under AT (M = 1.4 Msun)", "z = 0.35 gives 6.890 km vs GR's 9.164 km (-24.81 %); z = 0.20 gives 11.342 vs 13.535 km"),
                     new("the dangerous case", "EXO 0748-676 z = 0.35 (Cottam 2002, UNCONFIRMED)", "at M = 1.4, R = 11 km: z_AT = 0.206812 vs z_GR = 0.265888, so 0.35 excludes BOTH laws"),
                     new("CRITICAL - 5 sigma", "J0740+6620 (Riley): sx/x <= 3.661 % (perfect z) OR sz <= 0.025125 = 8.97 % of z_AT", "or the equal split sx/x <= 2.589 % AND sz <= 0.017766 = 6.34 % of z_AT"),
                     new("the improvement needed", "against today's 13.73 % M/R", "3.75x to 5.30x better in sigma_x/x PLUS 3.2x to 7.9x better in sigma_z, WON JOINTLY"),
                     new("the structural lesson", "the deficit is in the ERROR BUDGET, not the signal", "Dz = 0.1256 is large in absolute terms; it is sigma_x that keeps the test out of reach"),
                 ],
                 "ALLOWED: no current neutron-star redshift excludes AT (inside 2.5 sigma everywhere, 0.33 sigma for the most compact). EXCLUDED is not achievable - the maximum significance with a PERFECT redshift is 1.334 sigma, so no z precision reaches even 3 sigma on today's M/R. "
                 + "PREFERRED is not supported - the at-favoured ratio 4.483 flips sign across the plausible grid (down to 0.068) and only 1 of 9 grid points favours AT. 5 sigma needs sigma_x/x <= 2.589 % AND sigma_z <= 0.017766 (6.34 % of z_AT) jointly: 3.75-5.30x and 3.2-7.9x better. "
                 + "The bottleneck is the weighing, not the spectroscopy. EXCLUDED / ALLOWED / PREFERRED (ResearchY-G_020)."),
        ];
    }

    // ── G_011b: the gravity ladder M/r = f c^2 / G ───────────────────────────────

    private static double MassOverRadius(double f) => f * 2.99792458e8 * 2.99792458e8 / 6.67430e-11;

    // ── G_010: the feasibility arithmetic ───────────────────────────────────────

    private static double G10Drive(double secondsPerDay, int k)
        => (1.0 - Mu(k)) * (3.0 * secondsPerDay / 86400.0);

    // ── G_009: the clock law ─────────────────────────────────────────────────────

    private static double EarthDepth => GM_Earth / (R_Earth * C * C);
    private static double GpsDepth => GM_Earth / (C * C) * (1.0 / R_Earth - 1.0 / R_GpsOrbit);
    private static double GpsSpeed => Math.Sqrt(GM_Earth / R_GpsOrbit);
    private static double SrTerm => GpsSpeed * GpsSpeed / (2.0 * C * C);
    private static double GalacticAt => (1.6102e-6) / 3.0;
    private static double GalacticKin => Math.Pow(220e3 / C, 2.0);
    private const double R_GpsOrbit = 2.66e7;

    /// <summary>Fractional rate deviation in microseconds per day.</summary>
    private static double UsPerDayOf(double fractional) => fractional * 86400.0 * 1e6;

    // ── G_008: lifetimes and gains ───────────────────────────────────────────────

    private static double Lifetime(int k) => -1.0 / Math.Log(Math.Abs(Mu(k)));
    private static double GainOf(int k) => 1.0 / (1.0 - Mu(k));

    // ── G_007: the operator family's rate dependence ─────────────────────────────

    /// <summary>Closed-form suppression factor of the canonical D96 witness tilt at rate d after m steps.</summary>
    private static double TiltRatioAt(double d, int m)
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
        for (int k = 1; k < n; k++) { num += w[k] * w[k] * Math.Pow(Mu(k, d, n), 2.0 * m); den += w[k] * w[k]; }
        return Math.Sqrt(den / num);
    }

    /// <summary>Selectivity of the relaxation filter: |mu at the fastest mode| / |mu_1|.</summary>
    private static double Selectivity(double d) => Math.Abs(Mu(95, d)) / Math.Abs(Mu(1, d));

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
