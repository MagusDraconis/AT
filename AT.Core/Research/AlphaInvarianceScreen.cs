using System.Numerics;

namespace AT.Core.Research;

/// <summary>
/// The unitary-invariance screen behind the Born-rule derivation, EXECUTED rather than assumed
/// (ResearchY-G_026).
///
/// THE CLAIM (AT-X037 / QG216): for the generalized family P_i ∝ |ψ_i|^α, the normalization
/// N(ψ) = Σ_i |ψ_i|^α is invariant under every unitary — N(Uψ) = N(ψ) — IF AND ONLY IF α = 2.
///
/// WHY THIS CLASS EXISTS. The derivation previously recorded the outcome as hand-typed data:
/// <c>TestAllAlphas()</c> passed a literal <c>Survives</c> per exponent and <c>BuildRequirements()</c>
/// passed literal <c>bool[]</c> arrays such as <c>new[] { false, false, false, true, false, false }</c>.
/// The classification "α = 2 is UNIQUELY selected" therefore rested on typed booleans, and
/// <c>AllRequirementsUniquelySatisfied(reqs, tests)</c> accepted the executed tests and never read them.
/// The screen below actually performs the invariance test, so the α = 2 result is computed.
///
/// DETERMINISM (required for a research test): no randomness is used. The unitary set is the normalized
/// discrete Fourier transform plus Givens rotations at fixed angles, and the state set is fixed. The
/// screen is therefore bit-reproducible.
/// </summary>
public static class AlphaInvarianceScreen
{
    /// <summary>The exponents tested by the Born-rule derivation.</summary>
    public static readonly double[] TestAlphas = { 0.5, 1.0, 1.5, 2.0, 3.0, 4.0 };

    /// <summary>The dimensions the screen runs over.</summary>
    public static readonly int[] TestDimensions = { 2, 3, 4, 5 };

    /// <summary>Relative tolerance for the invariance test.</summary>
    public const double Tolerance = 1e-9;

    /// <summary>
    /// The normalized DFT matrix of order <paramref name="n"/> — a genuinely mixing unitary
    /// (it maps the computational basis onto the uniform superposition).
    /// </summary>
    public static Complex[,] Dft(int n)
    {
        double h = 1.0 / Math.Sqrt(n);
        var m = new Complex[n, n];
        for (int i = 0; i < n; i++)
            for (int k = 0; k < n; k++)
                m[i, k] = h * Complex.Exp(new Complex(0.0, 2.0 * Math.PI * i * k / n));
        return m;
    }

    /// <summary>A Givens rotation in the (i,j) plane at angle <paramref name="theta"/>.</summary>
    public static Complex[,] Givens(int n, int i, int j, double theta)
    {
        var m = new Complex[n, n];
        for (int a = 0; a < n; a++) m[a, a] = Complex.One;
        double c = Math.Cos(theta), s = Math.Sin(theta);
        m[i, i] = c; m[i, j] = -s; m[j, i] = s; m[j, j] = c;
        return m;
    }

    /// <summary>The fixed unitary set: the DFT plus Givens rotations on adjacent planes.</summary>
    public static List<Complex[,]> Unitaries(int n)
    {
        var set = new List<Complex[,]> { Dft(n) };
        foreach (double th in new[] { Math.PI / 7.0, Math.PI / 5.0, Math.PI / 3.0 })
        {
            set.Add(Givens(n, 0, 1, th));
            if (n >= 3) set.Add(Givens(n, 1, 2, th));
        }
        return set;
    }

    /// <summary>The fixed state set: a basis vector, the uniform state, and a fixed generic complex state.</summary>
    public static List<Complex[]> States(int n)
    {
        var basis = new Complex[n];
        basis[0] = Complex.One;

        var uniform = new Complex[n];
        for (int k = 0; k < n; k++) uniform[k] = new Complex(1.0 / Math.Sqrt(n), 0.0);

        var generic = new Complex[n];
        for (int k = 0; k < n; k++)
            generic[k] = Complex.Exp(new Complex(0.0, k * 1.7 + n * 0.3)) * (1.0 + 0.35 * k);
        double norm = Math.Sqrt(generic.Sum(z => z.Magnitude * z.Magnitude));
        for (int k = 0; k < n; k++) generic[k] /= norm;

        return new List<Complex[]> { basis, uniform, generic };
    }

    /// <summary>Apply a matrix to a vector.</summary>
    public static Complex[] Apply(Complex[,] m, Complex[] v)
    {
        int n = v.Length;
        var r = new Complex[n];
        for (int i = 0; i < n; i++)
        {
            Complex acc = Complex.Zero;
            for (int k = 0; k < n; k++) acc += m[i, k] * v[k];
            r[i] = acc;
        }
        return r;
    }

    /// <summary>N(ψ) = Σ_i |ψ_i|^α.</summary>
    public static double N(Complex[] psi, double alpha)
        => psi.Sum(z => Math.Pow(z.Magnitude, alpha));

    /// <summary>Is N(·, α) invariant under the fixed unitary set in dimension <paramref name="n"/>?</summary>
    public static bool IsInvariantIn(double alpha, int n)
    {
        foreach (var psi in States(n))
        {
            double before = N(psi, alpha);
            double scale = Math.Max(1.0, Math.Abs(before));
            foreach (var u in Unitaries(n))
            {
                double after = N(Apply(u, psi), alpha);
                if (Math.Abs(after - before) > Tolerance * scale) return false;
            }
        }
        return true;
    }

    /// <summary>Is N(·, α) invariant under the fixed unitary set in EVERY tested dimension?</summary>
    public static bool IsInvariantUnderUnitaries(double alpha)
        => TestDimensions.All(n => IsInvariantIn(alpha, n));

    /// <summary>
    /// The exponents that PASS the executed invariance screen. Expected to be exactly {2.0}.
    /// </summary>
    public static double[] SurvivingAlphas()
        => TestAlphas.Where(IsInvariantUnderUnitaries).ToArray();

    /// <summary>Was the α = 2 uniqueness computed (rather than asserted)? True iff the screen selects 2.0 only.</summary>
    public static bool AlphaTwoIsUniquelySelected()
    {
        var s = SurvivingAlphas();
        return s.Length == 1 && Math.Abs(s[0] - 2.0) < 1e-12;
    }

    /// <summary>
    /// The maximum relative violation of invariance over the screen, for α ≠ 2 — the magnitude by which
    /// the screen excludes the exponent. Used in reports so the exclusion is quantified, not asserted.
    /// </summary>
    public static double MaxRelativeViolation(double alpha)
    {
        double worst = 0.0;
        foreach (int n in TestDimensions)
            foreach (var psi in States(n))
            {
                double before = N(psi, alpha);
                double scale = Math.Max(1.0, Math.Abs(before));
                foreach (var u in Unitaries(n))
                {
                    double after = N(Apply(u, psi), alpha);
                    worst = Math.Max(worst, Math.Abs(after - before) / scale);
                }
            }
        return worst;
    }

    /// <summary>The screen's results as a report table.</summary>
    public static (double Alpha, string Outcome, double MaxViolation)[] Screen()
        => TestAlphas.Select(a => (
            a,
            IsInvariantUnderUnitaries(a) ? "INVARIANT (survives)" : "BREAKS",
            MaxRelativeViolation(a))).ToArray();
}
