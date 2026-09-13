using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_002 — FIELD EQUATION DERIVATION AUDIT.
///
/// QUESTION. E_001 found that AT's electromagnetic dynamics is DECLARED, never COMPUTED: the Lagrangian, the
/// field strength, the covariant derivative and the conserved currents all exist in `LagrangianOrigin.cs` as
/// members returning STRINGS, and the sourced Maxwell equation `d_mu F^mu_nu = J_nu` is recorded MISSING. So:
/// can AT derive an actual field equation — and specifically
/// <code>dF = 0</code> and <code>partial F = J</code> — or is that impossible?
///
/// ANSWER: **BOUNDARY. Both equations ARE derivable, and this audit derives them by computation rather than
/// asserting them — but the premises the derivation needs are not AT's.**
///
/// (1) `dF = 0` IS AN IDENTITY, AND IT IS FREE. With F = dA, the cyclic sum vanishes for ANY A because partials
///     commute — and on a uniform grid it vanishes EXACTLY: the cross second-differences cancel term by term, so
///     the computed residual is ROUNDOFF (worst 5.6e-15 across h = 0.4 to 0.01) and FLAT in h, not a converging
///     discretisation error. **This is not a dynamical achievement** — it costs nothing and is available to
///     anyone who writes F = dA. Its CONTENT is the statement "no magnetic charge", which the audit demonstrates
///     by exhibiting fields that FAIL it: a generic antisymmetric F (residual 2.0, constant in h) and the radial
///     field B = r_hat, whose divergence is the non-zero `2/r` (computed 4.64-4.78). So `dF = 0` is DERIVED and
///     VACUOUS.
///
/// (2) `partial F = J` IS DERIVED HERE — BY ACTUALLY DOING THE VARIATION. The audit builds the action
///     `S = sum h^4 [ -1/4 F_mu_nu F^mu_nu - J_mu A_mu ]` on a periodic 4-D lattice, computes its functional
///     derivative by BRUTE-FORCE central differencing of S link by link, and shows it equals the discrete
///     divergence of F minus J to roundoff (agreement 1e-17 relative, residual 1.3e-15 on an N = 6 lattice).
///     **Stationarity of the action IS `d_mu F^mu_nu = J_nu`.** This is computed dynamics, not a declared string
///     — the thing E_001 found missing now exists.
///
/// (3) BUT THE DERIVATION IMPORTS ITS PREMISES. The variation returns `partial F = J` only because the action
///     already had the form `-1/4 F^2 - J A`. The kinetic coefficient `-1/4` is conventional; the coupling to
///     J is a choice; and the sign/normalisation come from the assumed action principle. AT supplies none of
///     these as a theorem — E_001 established that the form is selected by a **minimality** argument and that
///     the coupling disagrees with itself (1/alpha = 137 from QG162 against ~100 from `FineStructureAnalyzer`).
///     The audit measures exactly this: the variation reproduces `partial F = e J` with the coupling e carried
///     through unchanged, and nothing in AT fixes e.
///
/// (4) THE CONTINUITY EQUATION IS DERIVED, TWICE AND INDEPENDENTLY. (a) Antisymmetry: the double divergence
///     `d_nu d_mu F^mu_nu` vanishes identically because F^mu_nu = -F^nu_mu — computed to 1.6e-17 for a generic
///     antisymmetric F. (b) Gauge invariance: the coupling term's variation under A -> A + dchi is EXACTLY the
///     divergence term, `-sum h^4 chi d_mu J^mu`, verified on the lattice (agreement 3.2e-13); so invariance
///     for every chi forces `d_mu J^mu = 0`. A non-conserved current breaks it, as computed for J = (1,0,0,0).
///
/// (5) THE PHOTON SECTOR IS DERIVED-FROM-GIVEN, AND ABSENT-AS-DERIVED. The photon is exactly massless BECAUSE
///     gauge invariance forbids the mass term: the Maxwell Lagrangian is invariant to 8.9e-17 while the Proca
///     term moves by O(m^2) (2.0e0 relative at m = 1). The dispersion follows from the field equation: for
///     A_nu = eps_nu cos(k.x) in Lorenz gauge the residual is `eps_nu k^2 cos(k.x)`, so the EOM holds iff
///     k^2 = 0, i.e. omega = c|k| — computed at 3.6e-9 for a null wave vector and O(1) for a massive one. But
///     that is the field equation TALKING, not AT producing the field: nothing in AT derives the spin-1 field.
///     AT's only computable massless wave equation is the spin-2 Fierz-Pauli `Box psi_mu_nu = 0`.
///
/// (6) SO WHY BOUNDARY RATHER THAN DERIVED? Because the success criterion asks AT to derive the equations, and
///     the derivation is performed here using the **action principle + locality + 4-D dimensional analysis +
///     Lorentz invariance** — premises AT does not supply. What the derivation DOES establish is that E_001's
///     defect is one of METHOD, NOT of impossibility: AT's declared Lagrangian is the standard one, its field
///     equation is the standard one, and both are correct. Nobody had done the variation. The gap is that AT
///     never earned the Lagrangian it wrote down.
///
/// VERDICT: **BOUNDARY** — `dF = 0` DERIVED (an identity, hence vacuous); `partial F = J` DERIVED-CONDITIONAL
/// (computed here in full, but only from a selected action with an undetermined coupling); continuity DERIVED
/// twice; photon masslessness DERIVED given gauge invariance; the photon SECTOR itself REFUTED-absent.
///
/// NOTE ON SIGNATURE. The lattice work uses a EUCLIDEAN signature (all partials `+d_mu`) because the variational
/// identity `g/h^4 = sum_mu grad_mu F_mu_nu - J_nu` is signature-independent — the Lorentzian signs only place
/// metric factors. The plane-wave DISPERSION check is run separately in MINKOWSKI signature `(+,-,-,-)`, carrying
/// the `eta` factors explicitly, because in Euclidean signature `k^2 = omega^2 + kappa^2` has no real null
/// vector: the condition `omega = |k|` appears only with Lorentzian signs. Running it Euclidean first produced a
/// residual of 1.62 where zero was expected — an error of this audit, caught by its own test rather than shipped.
/// </summary>
public static class FieldEquationDerivationAudit
{
    /// <summary>Index count (3+1).</summary>
    public const int Dim = 4;

    /// <summary>
    /// AT's own scan root (same convention as E_001) — used to check whether AT ever *computes* the dynamics.
    /// </summary>
    public const string ScanRelative = "AT.Core";

    // ═══ (1) dF = 0 — THE BIANCHI IDENTITY, COMPUTED ═══════════════════════════════════════════

    /// <summary>Central difference d_mu f at x — the whole basis of section (1).</summary>
    public static double Partial(Func<double[], double> f, double[] x, int mu, double h)
    {
        var xp = (double[])x.Clone();
        var xm = (double[])x.Clone();
        xp[mu] += h;
        xm[mu] -= h;
        return (f(xp) - f(xm)) / (2.0 * h);
    }

    /// <summary>F_mu_nu = d_mu A_nu - d_nu A_mu, returned as functions of x.</summary>
    public static Func<double[], double>[] FieldStrength(Func<double[], double>[] a, double h)
    {
        var f = new Func<double[], double>[Dim * Dim];
        for (int mu = 0; mu < Dim; mu++)
            for (int nu = 0; nu < Dim; nu++)
            {
                int m = mu, n = nu;
                f[mu * Dim + nu] = x => Partial(a[n], x, m, h) - Partial(a[m], x, n, h);
            }
        return f;
    }

    /// <summary>max |d_l F_mn + d_m F_nl + d_n F_lm| over a sample cloud.</summary>
    public static double BianchiResidual(Func<double[], double>[] f, double[][] samples, double h)
    {
        double worst = 0.0;
        foreach (var x in samples)
            for (int l = 0; l < Dim; l++)
                for (int m = 0; m < Dim; m++)
                    for (int n = 0; n < Dim; n++)
                    {
                        double b = Partial(f[m * Dim + n], x, l, h)
                                  + Partial(f[n * Dim + l], x, m, h)
                                  + Partial(f[l * Dim + m], x, n, h);
                        worst = Math.Max(worst, Math.Abs(b));
                    }
        return worst;
    }

    /// <summary>A smooth gauge potential, A_0..A_3.</summary>
    public static Func<double[], double>[] SmoothPotential()
        => new Func<double[], double>[]
        {
            x => Math.Sin(0.7 * x[0]) * Math.Cos(0.4 * x[1]) + 0.3 * x[2] * x[2],
            x => Math.Cos(0.5 * x[1]) * Math.Exp(-0.2 * x[3]) + 0.1 * x[0] * x[1],
            x => Math.Sin(0.6 * x[2] + 0.3 * x[0]) + 0.2 * x[1] * x[3],
            x => Math.Exp(-0.3 * x[1]) * Math.Sin(0.8 * x[3]) + 0.4 * x[0] * x[2],
        };

    /// <summary>F = dA from <see cref="SmoothPotential"/> — the case where Bianchi must hold.</summary>
    public static Func<double[], double>[] ExactField(double h) => FieldStrength(SmoothPotential(), h);

    /// <summary>
    /// A smooth antisymmetric F built DIRECTLY, not from any A — the control. Checked by hand at the sample
    /// point: with x = (0,0,0,0) the component (l,m,n) = (0,1,2) gives d_0 F_12 + d_1 F_20 + d_2 F_01
    /// = d_0(x_0 + x_1) + d_1(-x_3) + d_2(x_2) = 1, so the residual is O(1) and must NOT fall with h.
    /// </summary>
    public static Func<double[], double>[] NonExactField()
    {
        var f = new Func<double[], double>[Dim * Dim];
        for (int i = 0; i < Dim * Dim; i++) f[i] = _ => 0.0;

        void Set(int mu, int nu, Func<double[], double> g)
        {
            f[mu * Dim + nu] = g;
            f[nu * Dim + mu] = x => -g(x);
        }

        Set(0, 1, x => x[2]);
        Set(0, 2, x => x[3]);
        Set(0, 3, x => x[0]);
        Set(1, 2, x => x[0] + x[1]);
        Set(1, 3, x => x[1] * x[2]);
        Set(2, 3, x => x[0] * x[3]);
        return f;
    }

    /// <summary>
    /// The magnetic reading of dF = 0: with B_i = eps_ijk F_jk the spatial components say div B = 0. Here
    /// B = r_hat, which is NOT a curl, so div B = 2/r and the identity fails — computed, not asserted.
    /// </summary>
    public static Func<double[], double>[] RadialBField()
    {
        var f = new Func<double[], double>[Dim * Dim];
        for (int i = 0; i < Dim * Dim; i++) f[i] = _ => 0.0;

        // F_ij = eps_ijk B_k for spatial i,j (indices 1,2,3), B = r_hat in the same coordinates.
        Func<double[], double> R = x => Math.Sqrt(x[1] * x[1] + x[2] * x[2] + x[3] * x[3]);
        void Set(int i, int j, Func<double[], double> g)
        {
            f[i * Dim + j] = g;
            f[j * Dim + i] = x => -g(x);
        }
        // F_12 = B_3, F_13 = -B_2, F_23 = B_1
        Set(1, 2, x => x[3] / R(x));
        Set(1, 3, x => -x[2] / R(x));
        Set(2, 3, x => x[1] / R(x));
        return f;
    }

    /// <summary>Sample points away from the origin, so the radial control stays finite.</summary>
    public static double[][] SampleCloud()
    {
        var pts = new List<double[]>();
        double[] vals = { -1.1, -0.7, -0.3, 0.3, 0.7, 1.1, 1.6 };
        foreach (double a in vals)
            foreach (double b in vals)
                foreach (double c in vals)
                    pts.Add(new[] { a, b, c, 0.45 * a - 0.2 * b });
        return pts.ToArray();
    }

    /// <summary>Bianchi residual of F = dA at spacing h — the identity, measured.</summary>
    public static double BianchiExactResidual(double h)
        => BianchiResidual(ExactField(h), SampleCloud(), h);

    /// <summary>Bianchi residual of the directly-built control F — must NOT vanish.</summary>
    public static double BianchiNonExactResidual(double h)
        => BianchiResidual(NonExactField(), SampleCloud(), h);

    /// <summary>Bianchi residual of B = r_hat — the magnetic-charge violation, must NOT vanish.</summary>
    public static double BianchiRadialResidual(double h)
        => BianchiResidual(RadialBField(), SampleCloud(), h);

    /// <summary>The convergence table for the identity: the residual must fall as O(h^2).</summary>
    public static (double H, double Residual)[] BianchiConvergence()
        => new[] { 0.4, 0.2, 0.1, 0.05, 0.025, 0.01 }
            .Select(h => (h, BianchiExactResidual(h)))
            .ToArray();

    /// <summary>
    /// The discrete Bianchi sum is EXACT for F = dA, not merely convergent: the cross second-differences of A
    /// cancel identically on a uniform grid, term by term (d_l d_m A_n cancels -d_m d_l A_n, and so on), so the
    /// residual is ROUNDOFF and does not fall with h. Measured: flat at ~1e-15 from h = 0.4 down to h = 0.01.
    /// </summary>
    public static bool BianchiIsExactToRoundoff() => BianchiConvergence().All(t => t.Residual < 1e-13);

    /// <summary>Worst residual over the convergence table — reported to show the flatness.</summary>
    public static double BianchiWorstResidual() => BianchiConvergence().Max(t => t.Residual);

    /// <summary>Worst residual of the non-exact control over the table — must NOT fall either.</summary>
    public static double BianchiNonExactWorstResidual()
        => BianchiConvergence().Max(t => BianchiNonExactResidual(t.H));

    // ═══ (2)/(3) THE ACTION AND ITS VARIATION ═══════════════════════════════════════════════════

    /// <summary>
    /// A periodic 4-D lattice with N^4 sites and spacing h. Fields are stored flat as `mu * Sites + site`, with
    /// the LAST coordinate fastest, so `site = ((i0*N + i1)*N + i2)*N + i3`.
    /// </summary>
    public sealed class PeriodicLattice
    {
        public int N { get; }
        public double H { get; }
        public int Sites { get; }
        public double[] A { get; }
        public double[] J { get; }

        public PeriodicLattice(int n, double h)
        {
            N = n;
            H = h;
            Sites = n * n * n * n;
            A = new double[Dim * Sites];
            J = new double[Dim * Sites];
        }

        public int At(int mu, int site) => mu * Sites + site;

        private int Stride(int mu)
        {
            int stride = 1;
            for (int k = Dim - 1; k > mu; k--) stride *= N;
            return stride;
        }

        /// <summary>Site shifted by +1 in direction mu, with periodic wrap.</summary>
        public int Shift(int site, int mu)
        {
            int stride = Stride(mu);
            int idx = (site / stride) % N;
            return site + ((idx + 1) % N - idx) * stride;
        }

        /// <summary>Site shifted by -1 in direction mu, with periodic wrap.</summary>
        public int ShiftBack(int site, int mu)
        {
            int stride = Stride(mu);
            int idx = (site / stride) % N;
            return site + ((idx - 1 + N) % N - idx) * stride;
        }
    }

    /// <summary>The real coordinate x_mu of a site.</summary>
    public static double[] Coordinates(PeriodicLattice l, int site)
    {
        var x = new double[Dim];
        int s = site;
        for (int k = Dim - 1; k >= 0; k--)
        {
            x[k] = (s % l.N) * l.H;
            s /= l.N;
        }
        return x;
    }

    /// <summary>F_mu_nu at a site, forward differences (the standard lattice plaquette).</summary>
    public static double Plaquette(PeriodicLattice l, int mu, int nu, int site)
        => (l.A[l.At(nu, l.Shift(site, mu))] - l.A[l.At(nu, site)]) / l.H
         - (l.A[l.At(mu, l.Shift(site, nu))] - l.A[l.At(mu, site)]) / l.H;

    /// <summary>S = sum_x h^4 [ -1/4 F_mu_nu F_mu_nu - J_mu A_mu ] — Euclidean signature.</summary>
    public static double Action(PeriodicLattice l)
    {
        double vol = Math.Pow(l.H, Dim);
        double s = 0.0;
        for (int site = 0; site < l.Sites; site++)
        {
            double f2 = 0.0, ja = 0.0;
            for (int mu = 0; mu < Dim; mu++)
            {
                for (int nu = 0; nu < Dim; nu++)
                {
                    double f = Plaquette(l, mu, nu, site);
                    f2 += f * f;
                }
                int ix = l.At(mu, site);
                ja += l.J[ix] * l.A[ix];
            }
            s += (-0.25 * f2 - ja) * vol;
        }
        return s;
    }

    /// <summary>
    /// dS/dA_nu(y) by BRUTE-FORCE central differencing: perturb one link, recompute the whole action twice.
    /// This is the "computed dynamics" the audit requires — no declared string is involved.
    ///
    /// The step is LARGE (1e-2) deliberately: S is exactly quadratic in A, so the central difference is EXACT
    /// for any step, and the only error is roundoff. Measured: the agreement with the closed form is a factor
    /// 1e-4 better at eps = 1e-2 than at 1e-6, which is exactly the roundoff law (S ~ 3e3, so the differencing
    /// noise is S*eps_machine/(2*eps)).
    /// </summary>
    public static double VariationBruteForce(PeriodicLattice l, int nu, int site, double eps = 1e-2)
    {
        int ix = l.At(nu, site);
        double keep = l.A[ix];
        l.A[ix] = keep + eps;
        double sp = Action(l);
        l.A[ix] = keep - eps;
        double sm = Action(l);
        l.A[ix] = keep;
        return (sp - sm) / (2.0 * eps);
    }

    /// <summary>sum_mu (F_mu_nu(y) - F_mu_nu(y - mu)), the backward-difference divergence before dividing by h.</summary>
    public static double DivergenceSum(PeriodicLattice l, int nu, int site)
    {
        double sum = 0.0;
        for (int mu = 0; mu < Dim; mu++)
            sum += Plaquette(l, mu, nu, site) - Plaquette(l, mu, nu, l.ShiftBack(site, mu));
        return sum;
    }

    /// <summary>sum_mu grad_mu F_mu_nu — the left-hand side of the discrete field equation.</summary>
    public static double DiscreteDivergenceF(PeriodicLattice l, int nu, int site)
        => DivergenceSum(l, nu, site) / l.H;

    /// <summary>
    /// The same derivative in closed form: `g_nu(y) = h^3 sum_mu [F_mu_nu(y) - F_mu_nu(y-mu)] - h^4 J_nu(y)`.
    /// The audit's central claim is that this equals <see cref="VariationBruteForce"/>, and therefore that the
    /// action's stationarity IS the discrete Maxwell equation.
    /// </summary>
    public static double VariationFormula(PeriodicLattice l, int nu, int site)
        => Math.Pow(l.H, 3) * DivergenceSum(l, nu, site)
         - Math.Pow(l.H, 4) * l.J[l.At(nu, site)];

    /// <summary>Deterministic, explicit wave numbers — no RNG, so the audit reproduces bit-for-bit anywhere.</summary>
    private static readonly double[,] WaveNumbers =
    {
        { 0.9, 0.4, 0.3, 0.2 },
        { 0.5, 0.8, 0.1, 0.6 },
        { 0.7, 0.2, 0.9, 0.3 },
        { 0.3, 0.6, 0.5, 0.8 },
    };

    /// <summary>Fill A with a smooth deterministic potential (no randomness — reproducibility).</summary>
    public static void FillSmooth(PeriodicLattice l)
    {
        for (int site = 0; site < l.Sites; site++)
        {
            var x = Coordinates(l, site);
            for (int mu = 0; mu < Dim; mu++)
            {
                double v = 0.0;
                for (int k = 0; k < Dim; k++) v += WaveNumbers[mu, k] * x[k];
                l.A[l.At(mu, site)] = Math.Sin(v) + 0.25 * Math.Cos(WaveNumbers[mu, 0] * x[0] - WaveNumbers[mu, 2] * x[2]);
            }
        }
    }

    /// <summary>Fill J with a smooth deterministic external current (the generic case — NOT conserved).</summary>
    public static void FillDeterministicCurrent(PeriodicLattice l)
    {
        for (int site = 0; site < l.Sites; site++)
        {
            var x = Coordinates(l, site);
            for (int mu = 0; mu < Dim; mu++)
                l.J[l.At(mu, site)] = 0.3 * Math.Cos(0.5 * x[0] + 0.2 * mu + 0.1 * x[3]) + 0.05 * x[1];
        }
    }

    /// <summary>
    /// Set J := sum_mu grad_mu F_mu_nu computed from the current A. By the variational identity this makes A a
    /// STATIONARY point of the action, so the audit can verify the field equation is solved — numerically.
    /// </summary>
    public static void SetCurrentFromA(PeriodicLattice l)
    {
        for (int nu = 0; nu < Dim; nu++)
            for (int site = 0; site < l.Sites; site++)
                l.J[l.At(nu, site)] = DiscreteDivergenceF(l, nu, site);
    }

    /// <summary>Sampling stride so the brute-force check stays fast while still covering the lattice.</summary>
    private static int Stride(int sites, int want) => Math.Max(1, sites / want);

    /// <summary>
    /// Worst RELATIVE disagreement between the brute-force derivative and the closed form, over a spread of
    /// links. This is the audit's proof that the variation was actually performed and came out right.
    /// </summary>
    public static double VariationAgreement(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        FillDeterministicCurrent(l);
        int step = Stride(l.Sites, 40);
        double scale = 0.0, worst = 0.0;
        for (int nu = 0; nu < Dim; nu++)
            for (int site = 0; site < l.Sites; site += step)
            {
                double brute = VariationBruteForce(l, nu, site);
                double form = VariationFormula(l, nu, site);
                scale = Math.Max(scale, Math.Abs(brute));
                worst = Math.Max(worst, Math.Abs(brute - form));
            }
        return worst / Math.Max(scale, 1e-300);
    }

    /// <summary>Absolute worst disagreement, for reporting beside the relative figure.</summary>
    public static double VariationAgreementAbsolute(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        FillDeterministicCurrent(l);
        int step = Stride(l.Sites, 40);
        double worst = 0.0;
        for (int nu = 0; nu < Dim; nu++)
            for (int site = 0; site < l.Sites; site += step)
                worst = Math.Max(worst, Math.Abs(VariationBruteForce(l, nu, site) - VariationFormula(l, nu, site)));
        return worst;
    }

    /// <summary>
    /// THE FIELD EQUATION IS SOLVED: with J built from A, max |g_nu| / max |h^4 J_nu| must be ~0 — i.e. A is a
    /// stationary point of S, and the stationarity condition is exactly `grad_mu F_mu_nu = J_nu`.
    /// </summary>
    public static double StationarityResidualWhenSolved(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        SetCurrentFromA(l);
        int step = Stride(l.Sites, 40);
        double scale = 0.0, worst = 0.0;
        for (int nu = 0; nu < Dim; nu++)
            for (int site = 0; site < l.Sites; site += step)
            {
                scale = Math.Max(scale, Math.Abs(Math.Pow(l.H, 4) * l.J[l.At(nu, site)]));
                worst = Math.Max(worst, Math.Abs(VariationBruteForce(l, nu, site)));
            }
        return worst / Math.Max(scale, 1e-300);
    }

    /// <summary>
    /// The SAME residual when J is NOT built from A (a generic external current). This must be O(1): it shows
    /// the identity above is a real statement about the action, not a triviality.
    /// </summary>
    public static double StationarityResidualWhenNotSolved(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        FillDeterministicCurrent(l);
        int step = Stride(l.Sites, 40);
        double scale = 0.0, worst = 0.0;
        for (int nu = 0; nu < Dim; nu++)
            for (int site = 0; site < l.Sites; site += step)
            {
                scale = Math.Max(scale, Math.Abs(Math.Pow(l.H, 4) * l.J[l.At(nu, site)]));
                worst = Math.Max(worst, Math.Abs(VariationBruteForce(l, nu, site)));
            }
        return worst / Math.Max(scale, 1e-300);
    }

    // ═══ (4) CONTINUITY — DERIVED TWICE, INDEPENDENTLY ═════════════════════════════════════════

    /// <summary>
    /// (a) BY ANTISYMMETRY. `d_nu d_mu F^mu_nu` vanishes identically because F^mu_nu = -F^nu_mu: a symmetric
    /// double derivative meeting an antisymmetric tensor has nothing left. Computed from the same A as above.
    /// </summary>
    public static double DoubleDivergenceResidual(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        double worst = 0.0;
        for (int site = 0; site < l.Sites; site++)
        {
            double d = 0.0;
            for (int nu = 0; nu < Dim; nu++)
                d += (DiscreteDivergenceF(l, nu, site) - DiscreteDivergenceF(l, nu, l.ShiftBack(site, nu))) / l.H;
            worst = Math.Max(worst, Math.Abs(d));
        }
        return worst;
    }

    /// <summary>
    /// (b) BY GAUGE INVARIANCE. Under A -> A + dchi the coupling term changes by exactly minus the divergence
    /// term: `dS_c = - sum_x h^4 chi(x) grad_mu J^mu`. Both sides are computed independently here; if they agree,
    /// then invariance for EVERY chi forces `grad_mu J^mu = 0` — the continuity equation.
    /// </summary>
    public static (double Direct, double DivergenceForm) GaugeVariationOfCoupling(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillDeterministicCurrent(l);

        var chi = new double[l.Sites];
        for (int site = 0; site < l.Sites; site++)
        {
            var x = Coordinates(l, site);
            chi[site] = Math.Sin(0.6 * x[0] + 0.4 * x[2]) + 0.3 * x[1] * x[3];
        }

        double vol = Math.Pow(l.H, Dim);
        double before = 0.0, after = 0.0;
        for (int site = 0; site < l.Sites; site++)
            for (int mu = 0; mu < Dim; mu++)
            {
                int ix = l.At(mu, site);
                double dchi = (chi[l.Shift(site, mu)] - chi[site]) / l.H;
                before += l.J[ix] * l.A[ix] * vol;
                after += l.J[ix] * (l.A[ix] + dchi) * vol;
            }

        double divForm = 0.0;
        for (int site = 0; site < l.Sites; site++)
        {
            double dj = 0.0;
            for (int mu = 0; mu < Dim; mu++)
                dj += (l.J[l.At(mu, site)] - l.J[l.At(mu, l.ShiftBack(site, mu))]) / l.H;
            divForm += -chi[site] * dj * vol;
        }

        return (after - before, divForm);
    }

    /// <summary>
    /// A deliberately NON-conserved current — the control for (4b): with it the coupling term stops being gauge
    /// invariant, as it must. It is chosen LATTICE-PERIODIC, J_0 = cos(2*pi*x_0/L) with L = N*h, so the forward
    /// difference is the honest derivative everywhere and no wrap artefact inflates the value (a non-periodic
    /// choice such as J_0 = x_0 reports the wrap jump, N-1, instead of the derivative).
    /// </summary>
    public static double NonConservedCurrentDivergence(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        double period = n * h;
        for (int site = 0; site < l.Sites; site++)
        {
            var x = Coordinates(l, site);
            l.J[l.At(0, site)] = Math.Cos(2.0 * Math.PI * x[0] / period);
        }
        double worst = 0.0;
        for (int site = 0; site < l.Sites; site++)
        {
            double dj = 0.0;
            for (int mu = 0; mu < Dim; mu++)
                dj += (l.J[l.At(mu, l.Shift(site, mu))] - l.J[l.At(mu, site)]) / l.H;
            worst = Math.Max(worst, Math.Abs(dj));
        }
        return worst;
    }

    /// <summary>The analytic maximum of that control's divergence, 2*pi/L, for comparison.</summary>
    public static double NonConservedCurrentDivergenceExpected(int n = 6, double h = 0.4)
        => 2.0 * Math.PI / (n * h);

    /// <summary>
    /// The current SOURCED by the field, J := grad_mu F_mu_nu, is automatically conserved. This is the physical
    /// content of (4a), computed: its relative divergence is roundoff.
    /// </summary>
    public static double SourcedCurrentDivergence(int n = 6, double h = 0.4)
    {
        var l = new PeriodicLattice(n, h);
        FillSmooth(l);
        SetCurrentFromA(l);
        double scale = 0.0, worst = 0.0;
        for (int site = 0; site < l.Sites; site++)
        {
            double dj = 0.0;
            for (int mu = 0; mu < Dim; mu++)
            {
                double here = l.J[l.At(mu, site)];
                double back = l.J[l.At(mu, l.ShiftBack(site, mu))];
                dj += (here - back) / l.H;
                scale = Math.Max(scale, Math.Abs(here) / l.H);
            }
            worst = Math.Max(worst, Math.Abs(dj));
        }
        return worst / Math.Max(scale, 1e-300);
    }

    // ═══ (5) THE PHOTON SECTOR ══════════════════════════════════════════════════════════════════

    /// <summary>L = -1/4 F_mu_nu F_mu_nu at a point.</summary>
    public static double MaxwellDensity(Func<double[], double>[] a, double[] x, double h)
    {
        var f = FieldStrength(a, h);
        double s = 0.0;
        for (int mu = 0; mu < Dim; mu++)
            for (int nu = 0; nu < Dim; nu++)
                s += f[mu * Dim + nu](x) * f[mu * Dim + nu](x);
        return -0.25 * s;
    }

    /// <summary>L = -1/2 m^2 A_mu A_mu at a point (Proca) — NOT gauge invariant.</summary>
    public static double ProcaDensity(Func<double[], double>[] a, double[] x, double m)
    {
        double s = 0.0;
        for (int mu = 0; mu < Dim; mu++) s += a[mu](x) * a[mu](x);
        return -0.5 * m * m * s;
    }

    /// <summary>
    /// GAUGE INVARIANCE FORBIDS THE MASS. Pointwise change of the two candidate densities under A -> A + dchi:
    /// the Maxwell density moves by roundoff (F is gauge invariant), the Proca term by O(m^2).
    /// </summary>
    public static (double Maxwell, double Proca) MassTermViolation(double m = 1.0, double probeH = 1e-4)
    {
        var a = SmoothPotential();
        Func<double[], double> chi = x => Math.Sin(0.7 * x[0] - 0.3 * x[2]) + 0.2 * x[1] * x[3];
        var shifted = new Func<double[], double>[Dim];
        for (int mu = 0; mu < Dim; mu++)
        {
            int mm = mu;
            shifted[mu] = x => a[mm](x) + Partial(chi, x, mm, probeH);
        }

        double maxw = 0.0, proca = 0.0;
        for (int trial = 0; trial < 12; trial++)
        {
            var x = new[] { 0.41 + 0.13 * trial, -0.63 - 0.07 * trial, 0.27 + 0.11 * trial, 0.55 + 0.05 * trial };
            maxw = Math.Max(maxw, Math.Abs(MaxwellDensity(a, x, probeH) - MaxwellDensity(shifted, x, probeH)));
            proca = Math.Max(proca, Math.Abs(ProcaDensity(a, x, m) - ProcaDensity(shifted, x, m)));
        }
        return (maxw, proca);
    }

    /// <summary>
    /// THE DISPERSION, FROM THE FIELD EQUATION. For A_nu = eps_nu cos(k.x), k = (omega, kappa, 0, 0) and eps.k = 0,
    /// the vacuum equation gives `d_mu F_mu_nu = eps_nu k^2 cos(k.x)` — so it holds iff k^2 = 0, i.e. omega = |k|.
    /// </summary>
    public static (double NullResidual, double MassiveResidual) DispersionViolation(double spatialK = 0.9, double h = 1e-4)
        => (VacuumEomResidual(spatialK, spatialK, h),           // omega = |k|: null, must vanish
            VacuumEomResidual(0.9 * spatialK, spatialK, h));    // omega < |k|: not null, must not

    /// <summary>Minkowski metric, signature (+,-,-,-) — used by the dispersion check only.</summary>
    public static double Eta(int mu) => mu == 0 ? 1.0 : -1.0;

    /// <summary>
    /// max |d_mu F^mu_nu| in MINKOWSKI signature, for A_nu = eps_nu cos(k.x) with k = (omega, kappa, 0, 0) and
    /// eps.k = 0. Working it through: F_02 = -omega sin, F_12 = -kappa sin, so with eta = (+,-,-,-) the
    /// divergence is `eps_nu (omega^2 - kappa^2) cos(k.x)`. The field equation therefore holds iff
    /// `omega = |k|` — the MASSLESS dispersion, obtained from the equation rather than assumed.
    ///
    /// The Euclidean lattice work above is deliberately not reused here: in Euclidean signature k^2 = omega^2 +
    /// kappa^2 has no real null vector, so the null condition only appears with the Lorentzian signs.
    /// </summary>
    public static double VacuumEomResidual(double omega, double kappa, double h)
    {
        var eps = new[] { 0.0, 0.0, 1.0, 0.0 };   // eps.k = 0, since k has no third component
        var a = new Func<double[], double>[Dim];
        for (int nu = 0; nu < Dim; nu++)
        {
            int nn = nu;
            a[nu] = x => eps[nn] * Math.Cos(omega * x[0] + kappa * x[1]);
        }
        var f = FieldStrength(a, h);
        double worst = 0.0;
        for (int trial = 0; trial < 12; trial++)
        {
            var x = new[] { 0.13 * trial, 0.21 * trial, -0.17 * trial, 0.31 };
            for (int nu = 0; nu < Dim; nu++)
            {
                double d = 0.0;
                for (int mu = 0; mu < Dim; mu++)
                    d += Partial(f[mu * Dim + nu], x, mu, h) * Eta(mu) * Eta(nu);
                worst = Math.Max(worst, Math.Abs(d));
            }
        }
        return worst;
    }

    /// <summary>The analytic prediction for <see cref="VacuumEomResidual"/>: |omega^2 - kappa^2| at |cos| = 1.</summary>
    public static double VacuumEomResidualExpected(double omega, double kappa)
        => Math.Abs(omega * omega - kappa * kappa);

    // ═══ (6) WHAT AT SUPPLIES — AND WHAT THE DERIVATION IMPORTS ═════════════════════════════════

    /// <summary>This audit's own file, excluded from the scans below so it cannot certify itself.</summary>
    private static readonly string[] SelfFiles = { "FieldEquationDerivationAudit.cs" };

    /// <summary>
    /// Rule 11, GENERALISED. This scanner counts AT's physics, so the audits that measure AT are not part of what it
    /// counts - they are the apparatus. Three separate audits have now perturbed the count by naming a member after
    /// the thing being counted: E_006's first helper was called MembersThatComputeAFieldStrength, and E_007 carries
    /// PureGaugeFieldStrength, both of which the signature regex below matches, so the reading became 1 instead of 0.
    /// Excluding every file whose name ends in "Audit.cs" makes the count immune to the apparatus, which is what the
    /// rule is for.
    /// </summary>
    private static bool IsAuditScaffolding(string fileName)
        => fileName.EndsWith("Audit.cs", StringComparison.Ordinal)
        || SelfFiles.Contains(fileName, StringComparer.Ordinal);

    /// <summary>E_001's mechanical proof, reused: AT's EM dynamics is returned as strings, not computed.</summary>
    public static string[] AtDeclaresRatherThanComputes() => ElectromagnetismInventoryAudit.EmDynamicsIsStringReturning();

    /// <summary>
    /// Does AT ever COMPUTE a field strength, or the divergence of one? Counts numeric-returning members whose
    /// names carry the vocabulary. E_001 found the F^a_mu_nu member returns a string; this is the count that
    /// must be zero for AT to have "computed dynamics".
    /// </summary>
    public static int ComputableFieldStrengthMethods()
    {
        var root = CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return 0;
        var pat = new Regex(@"public\s+static\s+(double|double\[\]|float|int)\s+\w*(FieldStrength|DivergenceF|Fmunu)\w*\s*\(");
        return CountAcross(root, pat);
    }

    /// <summary>Does AT carry any numeric massless SPIN-1 wave equation? (E_001 says no.)</summary>
    public static bool NoSpin1WaveEquation()
    {
        var root = CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return true;
        var pat = new Regex(@"public\s+static\s+(double|double\[\])\s+\w*(Spin1|PhotonWave|MasslessSpin1|MaxwellWave)\w*\s*\(");
        return CountAcross(root, pat) == 0;
    }

    private static int CountAcross(string root, Regex pat)
    {
        int n = 0;
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")
                || file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")) continue;
            if (SelfFiles.Contains(Path.GetFileName(file), StringComparer.Ordinal)) continue;
            if (IsAuditScaffolding(Path.GetFileName(file))) continue;
            n += pat.Matches(AtSourceScan.StripLiteralsAndComments(File.ReadAllText(file))).Count;
        }
        return n;
    }

    /// <summary>The massless wave operator AT actually carries.</summary>
    public static string AtMasslessWaveOperator() => "Box psi_mu_nu = 0  (spin-2, Fierz-Pauli; QG/AT-X)";

    /// <summary>
    /// E_001's coupling contradiction, reused: the derivation's normalisation cannot come from AT if AT does not
    /// know its own coupling.
    /// </summary>
    public static ElectromagnetismInventoryAudit.Contradiction CouplingContradiction()
        => ElectromagnetismInventoryAudit.AlphaDisagreement();

    // ═══ VERDICT ════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// BOUNDARY (computed). Both equations ARE derived here — dF = 0 as an identity, partial F = J by actually
    /// performing the variation — but the premises the derivation needs are the action principle, locality, 4-D
    /// dimensional analysis and Lorentz invariance, none of which AT supplies; and the coupling that fixes the
    /// normalisation is contradicted inside AT itself. So the field equation is earned by standard means, not by
    /// AT. The audit also shows E_001's defect is one of METHOD, not impossibility.
    /// </summary>
    public static string Verdict()
    {
        // The Bianchi residual is a DISCRETISATION quantity (O(h^2)), so it is judged by CONVERGENCE rather than
        // by an absolute floor: the content of the identity is that it falls to zero as h -> 0, for arbitrary A,
        // while a non-exact F does not. Roundoff-level floors are used only where the cancellation is exact.
        bool bianchiIsIdentity = BianchiIsExactToRoundoff()
                              && BianchiNonExactResidual(0.01) > 0.5
                              && BianchiRadialResidual(0.01) > 0.5;
        bool variationPerformed = VariationAgreement() < 1e-8;
        bool solvedIsStationary = StationarityResidualWhenSolved() < 1e-7;
        bool unsolvedIsNot = StationarityResidualWhenNotSolved() > 1e-3;
        bool continuityHolds = DoubleDivergenceResidual() < 1e-9 && SourcedCurrentDivergence() < 1e-9;
        bool massForbidden = MassTermViolation().Maxwell < 1e-9 && MassTermViolation().Proca > 1e-3;

        if (!(bianchiIsIdentity && variationPerformed && solvedIsStationary && unsolvedIsNot
              && continuityHolds && massForbidden))
            return "REFUTED";                       // the derivation did not come out
        if (ComputableFieldStrengthMethods() > 0 && NoSpin1WaveEquation() && !AtDeclaresRatherThanComputes().Any())
            return "DERIVED";                        // AT computes it itself — not the case today
        return "BOUNDARY";                           // derived, but by imported premises
    }

    public static string WhereItStands()
        => "AT CAN DERIVE THE FIELD EQUATION — BY STANDARD MEANS, WHICH IS NOT THE SAME AS DERIVING IT. "
         + "`dF = 0` is an identity: with F = dA the cyclic sum vanishes for any A because partials commute, and "
         + "on a uniform grid it vanishes EXACTLY — the cross second-differences cancel term by term, so the "
         + "measured residual is roundoff (worst "
         + BianchiWorstResidual().ToString("E1", CultureInfo.InvariantCulture)
         + " across h = 0.4 to 0.01) rather than a discretisation error. Its only content is 'no magnetic "
         + "charge', shown by exhibiting fields that break it — a directly-built antisymmetric F (residual "
         + BianchiNonExactWorstResidual().ToString("F1", CultureInfo.InvariantCulture)
         + ") and the radial B = r_hat. `partial F = J` is likewise "
         + "derivable, and this audit DERIVES it rather than asserting it: the action on a periodic lattice is "
         + "varied link by link by brute force, and the result equals the discrete divergence of F minus J to "
         + "roundoff — so stationarity of the action IS the sourced Maxwell equation. Continuity follows twice "
         + "over, from antisymmetry and from gauge invariance, and gauge invariance also forbids the photon a "
         + "mass. BUT NONE OF THAT IS AT'S: the derivation runs on the action principle, locality, four-"
         + "dimensional counting and Lorentz invariance — premises AT does not supply — and the coupling that "
         + "would fix the normalisation contradicts itself inside AT (1/alpha = 137 against ~100). What the audit "
         + "establishes is that E_001's defect is procedural: the declared Lagrangian is the standard one, its "
         + "field equation is the standard one and both are correct, but nobody performed the variation, so AT "
         + "never earned what it wrote down. The photon is exactly massless if U(1) is a gauge symmetry — and "
         + "that, AT does derive.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputBianchi()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. dF = 0 — THE BIANCHI IDENTITY, COMPUTED");
        sb.AppendLine("   With F = dA the cyclic sum d_l F_mn + d_m F_nl + d_n F_lm must vanish for ANY A.");
        sb.AppendLine();
        sb.AppendLine("   h          F = dA (identity)    directly-built F    B = r_hat (div B = 2/r)");
        foreach (var (hh, rr) in BianchiConvergence())
            sb.AppendLine($"   {hh,-10:F3} {rr,18:E3} {BianchiNonExactResidual(hh),18:E3} {BianchiRadialResidual(hh),22:E3}");
        sb.AppendLine();
        sb.AppendLine($"   worst residual for F = dA      : {BianchiWorstResidual():E3}");
        sb.AppendLine("     ROUNDOFF, and FLAT in h — the discrete identity is EXACT, because the cross");
        sb.AppendLine("     second-differences of A cancel term by term. It is not a converging approximation,");
        sb.AppendLine("     it is an algebraic cancellation.");
        sb.AppendLine($"   worst residual of the control  : {BianchiNonExactWorstResidual():F3} — does NOT vanish at any h.");
        sb.AppendLine("   ⇒ dF = 0 costs nothing and is free to anyone who writes F = dA. Its content is exactly");
        sb.AppendLine("     'no magnetic charge', and the fields that break it are exhibited above.");
        return sb.ToString();
    }

    public static string OutputVariation()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. partial F = J — THE VARIATION, ACTUALLY PERFORMED");
        sb.AppendLine("   S = sum h^4 [ -1/4 F_mu_nu F_mu_nu - J_mu A_mu ] on a periodic lattice;");
        sb.AppendLine("   dS/dA_nu(y) computed by perturbing ONE link and recomputing S twice.");
        sb.AppendLine();
        sb.AppendLine($"   brute force vs closed form, relative disagreement : {VariationAgreement():E3}");
        sb.AppendLine($"   brute force vs closed form, absolute disagreement : {VariationAgreementAbsolute():E3}");
        sb.AppendLine($"   stationarity residual when the EOM IS satisfied   : {StationarityResidualWhenSolved():E3}");
        sb.AppendLine($"   stationarity residual when it is NOT              : {StationarityResidualWhenNotSolved():E3}");
        sb.AppendLine();
        sb.AppendLine("   ⇒ dS/dA_nu = h^3 sum_mu [F_mu_nu(y) - F_mu_nu(y-mu)] - h^4 J_nu(y), i.e.");
        sb.AppendLine("     STATIONARITY OF THE ACTION **IS** grad_mu F_mu_nu = J_nu. Computed, not declared.");
        return sb.ToString();
    }

    public static string OutputContinuity()
    {
        var (direct, divForm) = GaugeVariationOfCoupling();
        var sb = new StringBuilder();
        sb.AppendLine("3. CONTINUITY — DERIVED TWICE, INDEPENDENTLY");
        sb.AppendLine("   (a) antisymmetry: d_nu d_mu F^mu_nu = 0 because F^mu_nu = -F^nu_mu");
        sb.AppendLine($"       residual                                      : {DoubleDivergenceResidual():E3}");
        sb.AppendLine($"       sourced current J := grad F, relative divergence: {SourcedCurrentDivergence():E3}");
        sb.AppendLine("   (b) gauge invariance: dS_c[A + dchi] = - sum h^4 chi grad_mu J^mu");
        sb.AppendLine($"       direct change of the coupling term            : {direct,18:E6}");
        sb.AppendLine($"       divergence form - sum h^4 chi grad J          : {divForm,18:E6}");
        sb.AppendLine($"       agreement                                     : {Math.Abs(direct - divForm),18:E3}");
        sb.AppendLine($"   control: a NON-conserved J = cos(2*pi*x_0/L) has |grad J| = {NonConservedCurrentDivergence():F4}");
        sb.AppendLine($"     against the analytic maximum 2*pi/L = {NonConservedCurrentDivergenceExpected():F4}, so");
        sb.AppendLine("     invariance for every chi forces grad_mu J^mu = 0. DERIVED.");
        return sb.ToString();
    }

    public static string OutputPhoton()
    {
        var (maxwell, proca) = MassTermViolation();
        var (nullR, massiveR) = DispersionViolation();
        var sb = new StringBuilder();
        sb.AppendLine("4. THE PHOTON SECTOR — DERIVED FROM GIVEN, ABSENT AS DERIVED");
        sb.AppendLine("   gauge invariance forbids the mass:");
        sb.AppendLine($"     change of the Maxwell density under A -> A + dchi : {maxwell,16:E3}");
        sb.AppendLine($"     change of the Proca  density under the same shift : {proca,16:E3}");
        sb.AppendLine("   the dispersion comes from the field equation (Minkowski signature, eta = +---),");
        sb.AppendLine("   A_nu = eps_nu cos(k.x), k = (omega, kappa, 0, 0), eps.k = 0:");
        sb.AppendLine($"     null wave vector (omega = |k|)  : residual {nullR,16:E3}   (analytic 0, since k^2 = 0)");
        sb.AppendLine($"     non-null       (omega = 0.9|k|) : residual {massiveR,16:E3}   (analytic "
                      + $"{VacuumEomResidualExpected(0.9 * 0.9, 0.9):F4})");
        sb.AppendLine("   ⇒ k^2 = 0, i.e. omega = c|k|, and the photon is exactly massless — IF U(1) is a gauge");
        sb.AppendLine("     symmetry. Both the masslessness and the transversality eps.k = 0 come from that; the");
        sb.AppendLine("     dispersion comes from the field equation. Neither comes from AT.");
        sb.AppendLine($"   but AT's only computable massless wave equation is {AtMasslessWaveOperator()}");
        sb.AppendLine($"     numeric massless spin-1 wave equations in AT.Core : {(NoSpin1WaveEquation() ? 0 : 1)}");
        return sb.ToString();
    }

    public static string OutputWhatAtSupplies()
    {
        var contra = CouplingContradiction();
        var sb = new StringBuilder();
        sb.AppendLine("5. WHAT AT SUPPLIES — AND WHAT THE DERIVATION IMPORTS");
        sb.AppendLine($"   AT members that COMPUTE a field strength or its divergence : {ComputableFieldStrengthMethods()}");
        sb.AppendLine($"   AT EM dynamics members that return STRINGS                 : {AtDeclaresRatherThanComputes().Length}");
        sb.AppendLine($"     names: {string.Join(", ", AtDeclaresRatherThanComputes())}");
        sb.AppendLine($"   AT's coupling disagrees with itself: {contra.First}  vs  {contra.Second}");
        sb.AppendLine($"     {contra.Why}");
        sb.AppendLine();
        sb.AppendLine("   IMPORTED (not AT's): action principle, locality, 4-D dimensional counting, Lorentz invariance,");
        sb.AppendLine("   the normalisation 1/4 and the minimal coupling.");
        sb.AppendLine("   AT'S OWN: the gauge symmetry's existence, and the dimension in which Maxwell has the right form.");
        return sb.ToString();
    }
}
