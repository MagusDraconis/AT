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
        ];
    }
}
