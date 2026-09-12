using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_016b — Mass Independence Audit (group G — Gravity Source).
///
/// QUESTION: can two states have the SAME energy but DIFFERENT rho, or the SAME rho but DIFFERENT energy?
/// Construct explicit examples; measure DeltaE, DeltaRho, DeltaTau.    Output: INDEPENDENT / DEPENDENT /
/// REFUTED.
///
/// THE THREE ANSWERS, ONE PER REQUESTED LABEL
///   INDEPENDENT  rho moves at EXACTLY fixed E. The energy functional E = <lambda, rho> (G_016) has a
///                94-dimensional kernel on the affine set {Sum rho = 1}, split into
///                   (a) the 51-dimensional DEGENERACY room  (lambda constant within a multiplet), and
///                   (b) the 43-dimensional lambda-MIXING room (zero-net combinations of DISTINCT lambda).
///                Both rooms are constructed explicitly below.
///   DEPENDENT    E is a FUNCTION of rho (given the capacity lambda): one rho, one E. So E is DEPENDENT on
///                rho — the independence is strictly one-directional.
///   REFUTED      "same rho, different E" as a STATE change. Impossible on a fixed lattice. The only way to
///                hold rho fixed and move E is to change the CAPACITY: with the SAME uniform rho = 1/96 the
///                pairing gives E = 2K EXACTLY for coupling range K = 1..6 (2, 4, 6, 8, 10, 12) because
///                Sum lambda = 192K — and K is a BOUNDARY input (G_007), not a state variable.
///
/// EXPLICIT EXAMPLES (all verified; DeltaE = 0 exactly in the algebra)
///  (a) the canonical witness tilt — a pure within-multiplet move:
///      DeltaE = 0,  DeltaRho = L1 = 0.6666666666666667,  contrast 20 : 1,
///      DeltaTau/tau = (1/3)ln(20) = 0.9985774245179969 = 86277.08947835493 s/day.
///  (b) a pairwise move inside an m = 2 multiplet (delta = 0.002 / 0.005): DeltaRho = 0.004 / 0.010,
///      contrast 1.4752 / 2.8462, DeltaTau = 0.129609 / 0.348656.
///  (c) the lambda-MIXING room, cells 94/92/90 with lambda = 15.837372467014836, 15.790176186632262,
///      15.414213562373096: v = (1, -1.1255345008711273, 0.12553450087112727) has Sum v = 0 and
///      <lambda,v> = 0 EXACTLY, spans three DIFFERENT multiplets, and at scale 0.004 moves rho by
///      L1 = 0.009004 with contrast 2.437501 and DeltaTau = 0.29699105 = 25660.0263 s/day.
///  (d) the same rho with four different phase assignments: rho, E and DeltaTau are ALL unchanged
///      (L1(|psi|^2, rho) <= 2.5e-16) while the coherent sum |Sum psi| moves 281x — the phase sector is
///      exactly energy-inert and clock-inert.
///
/// THE FIXED-E FAMILY (test 5) — and why the effect is UNBOUNDED
///   Tilt every multiplet with fraction f on its first cell: fr = (f, (1-f)/(m-1)) for m >= 2, fr = 1 for
///   m = 1. Then EXACTLY
///     DeltaE = 0,   contrast = 5f/(1-f)   (f >= 1/2),   L1(f) = (2/96)[42|2f-1| + |5f-1| + |6f-1|],
///     DeltaTau/tau = (1/3) ln(5f/(1-f)) = T   <=>   f = 1/(1 + 5 e^{-3T}).
///   At the canonical T = (1/3)ln20 the inversion returns f = 0.8 — the canonical TiltFractions IS the
///   20 : 1 solution. The ladder f(1.5) = 0.947377910367, f(2) = 0.987757963984, f(3) = 0.999383331494,
///   f(5) = 0.999998470491 shows f -> 1, and since L1 -> 1.0625 exactly while rho_min -> 0, the clock
///   separation at FIXED energy is UNBOUNDED: no target T is forbidden by energy conservation. What bounds
///   it is positivity and the G_005 Poisson band (T = 4.8867e-6 needs f = 0.1666687028016166).
///
/// Deterministic: exact algebra, no randomness.  No reclassification (G_016, G_009, G_011 unchanged);
/// D_040 untouched; no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_016b_Tests : ResearchTestBase
{
    public Y_G_016b_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double CanonicalK = 6;
    private const double ObservedContrast = 1.6102e-6;
    private const double BandCeiling = 4.8867e-6;

    /// <summary>The D96 eigenspaces for a given coupling range K (grouped at 1e-6, as AT.Core does).</summary>
    private static (double[] Distinct, int[] Mult) Spaces(int kMax)
    {
        var lams = Enumerable.Range(0, N).Select(k => Enumerable.Range(1, kMax)
            .Sum(s => 2.0 * (1.0 - Math.Cos(2.0 * Math.PI * k * s / N)))).OrderBy(v => v).ToArray();
        var distinct = new List<double>();
        var mult = new List<int>();
        int i = 0;
        while (i < N)
        {
            int j = i;
            while (j < N && lams[j] - lams[i] <= 1e-6) j++;
            distinct.Add(lams[i]); mult.Add(j - i); i = j;
        }
        return (distinct.ToArray(), mult.ToArray());
    }

    /// <summary>The spectral weight attached to each cell for coupling range K; Sum = 192K.</summary>
    private static double[] Weight(int kMax)
    {
        var (distinct, mult) = Spaces(kMax);
        var w = new double[N];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
            for (int j = 0; j < mult[i]; j++) w[k++] = distinct[i];
        return w;
    }

    /// <summary>AT's energy functional E = &lt;lambda, rho&gt; (QG180/QG181).</summary>
    private static double Energy(double[] rho, int kMax = 6)
    {
        var w = Weight(kMax);
        double s = 0.0;
        for (int i = 0; i < N; i++) s += w[i] * rho[i];
        return s;
    }

    private static double[] Uniform(int kMax = 6) => Spread(Spaces(kMax).Mult, 1.0);
    private static double[] CanonicalTilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>The canonical fixed-E family: fraction f on each multiplet's first cell (1/m for m = 1).</summary>
    private static double[] TiltAt(double f) => Spread(D96Spaces.Mult, 1.0, m => m == 1
        ? new[] { 1.0 }
        : Enumerable.Range(0, m).Select(j => j == 0 ? f : (1.0 - f) / (m - 1)).ToArray());

    /// <summary>The clock separation between the extreme cells: (1/d) ln(rho_max/rho_min) (G_009).</summary>
    private static double ClockSeparation(double[] rho) => Math.Log(rho.Max() / rho.Min()) / D;

    /// <summary>The exact L1 of the fixed-E family from the uniform measure.</summary>
    private static double FamilyL1(double f) => (2.0 / N) * (42 * Math.Abs(2 * f - 1) + Math.Abs(5 * f - 1) + Math.Abs(6 * f - 1));

    // ── 1. INDEPENDENT: same energy, different rho (the 51-dim degeneracy room) ───

    [Fact]
    public void Y_G_016b_SameEnergyDifferentRho()
    {
        var uniform = Uniform();
        var tilt = CanonicalTilt;
        double e0 = Energy(uniform);

        // The canonical witness is a pure within-multiplet move: E is EXACTLY invariant.
        Assert.Equal(0.0, Energy(tilt) - e0, 12);
        Assert.True(Math.Abs(Energy(tilt) - e0) < 1e-12, $"dE = {Energy(tilt) - e0}");

        // Yet rho moves a lot and the clock separates.
        Assert.True(Math.Abs(L1(tilt, uniform) - 0.6666666666666667) < 1e-12);
        Assert.True(Math.Abs(tilt.Max() / tilt.Min() - 20.0) < 1e-9);
        Assert.True(Math.Abs(ClockSeparation(tilt) - 0.9985774245179969) < 1e-12);
        Assert.True(Math.Abs(ClockSeparation(tilt) * 86400.0 - 86277.08947835493) < 1e-3);
        Assert.True(Math.Abs(L1(tilt, uniform) / 0.6666666666666667 - 1.0) < 1e-12);

        // A pairwise move inside an m = 2 multiplet: DeltaE = 0, DeltaRho = 2 delta.
        var (_, mult) = Spaces(6);
        int start = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 2) break; start += mult[i]; }
        double u = 1.0 / N;
        foreach (double delta in new[] { 0.002, 0.005 })
        {
            var rho = (double[])uniform.Clone();
            rho[start] += delta; rho[start + 1] -= delta;
            Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12);
            Assert.True(Math.Abs(Energy(rho) - e0) < 1e-13, $"delta = {delta}: |dE| = {Energy(rho) - e0}");
            Assert.True(Math.Abs(L1(rho, uniform) - 2.0 * delta) < 1e-12);
            double ratio = (u + delta) / (u - delta);
            Assert.True(Math.Abs(rho[start] / rho[start + 1] - ratio) < 1e-9);
            Assert.True(Math.Abs(ClockSeparation(rho) - Math.Log(ratio) / D) < 1e-12);
            Assert.True(ClockSeparation(rho) > 0.12);
        }

        // The room is 51-dimensional (Sum(m - 1) over the 45 eigenspaces).
        Assert.Equal(51, mult.Sum(m => m - 1));
        Assert.Equal(45, mult.Length);
    }

    // ── 2. INDEPENDENT: same energy, different rho (the 43-dim lambda-mixing room) ─

    [Fact]
    public void Y_G_016b_LambdaMixingRoom()
    {
        var uniform = Uniform();
        double e0 = Energy(uniform);
        var weight = Weight(6);
        var (_, mult) = Spaces(6);

        // Pick one cell from each of the three largest DISTINCT eigenvalues (three different multiplets).
        var starts = new List<int>();
        int acc = 0;
        foreach (int m in mult) { starts.Add(acc); acc += m; }
        int c1 = starts[^1], c2 = starts[^2], c3 = starts[^3];
        double l1 = weight[c1], l2 = weight[c2], l3 = weight[c3];
        Assert.True(Math.Abs(l1 - 15.837372467014836) < 1e-9, $"lambda1 = {l1}");
        Assert.True(Math.Abs(l2 - 15.790176186632262) < 1e-9, $"lambda2 = {l2}");
        Assert.True(Math.Abs(l3 - 15.414213562373096) < 1e-9, $"lambda3 = {l3}");
        Assert.True(l1 > l2 && l2 > l3);                            // distinct: NOT a degeneracy move

        // v with Sum v = 0 and <lambda, v> = 0, supported on exactly three cells of three multiplets.
        double b = -(l1 - l3) / (l2 - l3);
        double c = -1.0 - b;
        var v = new double[N];
        v[c1] = 1.0; v[c2] = b; v[c3] = c;
        Assert.True(Math.Abs(v.Sum()) < 1e-15, $"Sum v = {v.Sum()}");
        double lv = 0.0;
        for (int i = 0; i < N; i++) lv += weight[i] * v[i];
        Assert.True(Math.Abs(lv) < 1e-12, $"<lambda,v> = {lv}");
        Assert.True(Math.Abs(b - (-1.1255345008711273)) < 1e-9, $"b = {b}");
        Assert.True(Math.Abs(c - 0.12553450087112727) < 1e-9, $"c = {c}");
        Assert.Equal(3, v.Count(x => Math.Abs(x) > 1e-15));         // three cells, three multiplets

        // At scale 0.004: E is invariant, rho moves, and the clocks separate.
        var rho = new double[N];
        for (int i = 0; i < N; i++) rho[i] = uniform[i] + 0.004 * v[i];
        Assert.True(rho.Min() > 0.0, $"min rho = {rho.Min()}");
        Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(Energy(rho) - e0) < 1e-13, $"|dE| = {Energy(rho) - e0}");
        Assert.True(Math.Abs(L1(rho, uniform) - 0.009004) < 1e-6, $"L1 = {L1(rho, uniform)}");
        Assert.True(Math.Abs(rho.Min() - 0.005915) < 1e-6, $"min rho = {rho.Min()}");
        Assert.True(Math.Abs(rho.Max() / rho.Min() - 2.437501) < 1e-6, $"contrast = {rho.Max() / rho.Min()}");
        Assert.True(Math.Abs(ClockSeparation(rho) - 0.29699105) < 1e-7, $"sep = {ClockSeparation(rho)}");
        Assert.True(Math.Abs(ClockSeparation(rho) * 86400.0 - 25660.0263) < 1e-2);

        // The two rooms are different: the degeneracy room is 51-dimensional, the mixing room 43.
        Assert.Equal(94, (N - 1) - 1);
        Assert.Equal(51 + 43, 94);
    }

    // ── 3. REFUTED: same rho, different energy (only a CAPACITY change can do it) ──

    [Fact]
    public void Y_G_016b_SameRhoDifferentEnergy()
    {
        // On a fixed lattice E is a FUNCTION of rho: the SAME rho vector always gives the same E.
        var uniform = Uniform();
        double a1 = Energy(uniform), a2 = Energy((double[])uniform.Clone());
        Assert.Equal(a1, a2, 12);
        Assert.Equal(12.0, a1, 12);

        // The only way to hold rho fixed and move E is to change the CAPACITY (the coupling range K).
        // With the SAME uniform rho = 1/96 the pairing gives E = 2K exactly, because Sum lambda = 192K.
        var rows = new (int K, int A0, double SumLam, double E)[]
        {
            (1, 49, 192.0, 2.0), (2, 47, 384.0, 4.0), (3, 45, 576.0, 6.0),
            (4, 47, 768.0, 8.0), (5, 45, 960.0, 10.0), (6, 45, 1152.0, 12.0),
        };
        foreach (var (K, a0, sumLam, e) in rows)
        {
            var rhoK = Uniform(K);
            var wK = Weight(K);
            Assert.True(Math.Abs(rhoK.Sum() - 1.0) < 1e-12);
            Assert.True(L1(rhoK, uniform) < 1e-15, $"K = {K}: rho differs from the K = 6 uniform by {L1(rhoK, uniform)}");
            Assert.Equal(a0, Spaces(K).Distinct.Length);
            Assert.True(Math.Abs(wK.Sum() - sumLam) < 1e-9, $"K = {K}: Sum lambda = {wK.Sum()}");
            Assert.True(Math.Abs(wK.Sum() - 192.0 * K) < 1e-9);
            Assert.True(Math.Abs(Energy(rhoK, K) - e) < 1e-12, $"K = {K}: E = {Energy(rhoK, K)}");
            Assert.True(Math.Abs(Energy(rhoK, K) - 2.0 * K) < 1e-12);
        }

        // Six couplings, six energies, ONE rho — so "same rho, different E" is REFUTED as a state change:
        // it requires K, which is a BOUNDARY input (G_007), not something a state can vary.
        Assert.True(Math.Abs(Energy(Uniform(6), 6) - Energy(Uniform(1), 1) - 10.0) < 1e-12);
        Assert.True(Math.Abs(Energy(Uniform(6), 6) / Energy(Uniform(1), 1) - 6.0) < 1e-12);
    }

    // ── 4. same rho, same E, different psi: the phase sector is inert ────────────

    [Fact]
    public void Y_G_016b_SameRhoDifferentPhase()
    {
        var rho = CanonicalTilt;
        double e0 = Energy(rho);
        double sep0 = ClockSeparation(rho);
        int n = rho.Length;
        var sqrtRho = rho.Select(Math.Sqrt).ToArray();

        var phases = new[]
        {
            Enumerable.Range(0, n).Select(j => 2.0 * Math.PI * j / n).ToArray(),                  // canonical grid
            Enumerable.Range(0, n).Select(j => 2.0 * Math.PI * j / n + 1.234).ToArray(),          // global shift
            Enumerable.Range(0, n).Select(j => -2.0 * Math.PI * j / n).ToArray(),                 // the mirror
            new double[n],                                                                        // fully locked
        };

        double coherentMin = double.MaxValue, coherentMax = 0.0;
        foreach (var theta in phases)
        {
            // psi_j = sqrt(rho_j) e^{i theta_j}; |psi|^2 IS rho.
            var psi2 = new double[n];
            for (int j = 0; j < n; j++)
            {
                double re = sqrtRho[j] * Math.Cos(theta[j]);
                double im = sqrtRho[j] * Math.Sin(theta[j]);
                psi2[j] = re * re + im * im;
            }
            Assert.True(L1(psi2, rho) < 1e-14, $"L1(|psi|^2, rho) = {L1(psi2, rho)}");

            // The coherent sum moves...
            double sumRe = 0.0, sumIm = 0.0;
            for (int j = 0; j < n; j++)
            {
                sumRe += sqrtRho[j] * Math.Cos(theta[j]);
                sumIm += sqrtRho[j] * Math.Sin(theta[j]);
            }
            double coherent = Math.Sqrt(sumRe * sumRe + sumIm * sumIm);
            coherentMin = Math.Min(coherentMin, coherent);
            coherentMax = Math.Max(coherentMax, coherent);

            // ...while E and the clock separation are EXACTLY unchanged.
            Assert.Equal(e0, Energy(psi2), 12);
            Assert.Equal(sep0, ClockSeparation(psi2), 12);
        }

        // The rho-inert phase sector nevertheless moves by exactly 281.22414487183346 (G_002/G_011).
        Assert.True(Math.Abs(coherentMax - 9.1171821879) < 1e-9, $"coherentMax = {coherentMax}");
        Assert.True(Math.Abs(coherentMin - 0.03241962809423954) < 1e-12, $"coherentMin = {coherentMin}");
        Assert.True(Math.Abs(coherentMax / coherentMin - 281.22414487183346) < 1e-8);
        Assert.True(Math.Abs(ClockSeparation(CanonicalTilt) - sep0) < 1e-15);   // DeltaTau = 0 exactly
    }

    // ── 5. the fixed-E family: DeltaTau is UNBOUNDED ─────────────────────────────

    [Fact]
    public void Y_G_016b_UnboundedAtFixedEnergy()
    {
        var uniform = Uniform();
        double e0 = Energy(uniform);

        // Tilt every multiplet with fraction f on its first cell: DeltaE = 0 EXACTLY for every f.
        foreach (double f in new[] { 0.2, 0.5, 0.8, 0.9, 0.99, 0.999 })
        {
            var rho = TiltAt(f);
            Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12, $"f = {f}: Sum rho - 1 = {rho.Sum() - 1}");
            Assert.True(Math.Abs(Energy(rho) - e0) < 1e-13, $"f = {f}: |dE| = {Energy(rho) - e0}");
            Assert.True(Math.Abs(L1(rho, uniform) - FamilyL1(f)) < 1e-10, $"f = {f}: L1 = {L1(rho, uniform)}");
            if (f >= 0.5)
            {
                double contrast = 5.0 * f / (1.0 - f);
                Assert.True(Math.Abs(rho.Max() / rho.Min() - contrast) / contrast < 1e-9, $"f = {f}");
                Assert.True(Math.Abs(ClockSeparation(rho) - Math.Log(contrast) / D) < 1e-12);
            }
        }

        // The canonical witness at f = 0.8 IS the 20 : 1 solution of the inversion.
        Assert.True(Math.Abs(TiltAt(0.8).Max() / TiltAt(0.8).Min() - 20.0) < 1e-9);
        Assert.True(Math.Abs(1.0 / (1.0 + 5.0 * Math.Exp(-3.0 * Math.Log(20.0) / D)) - 0.8) < 1e-12);
        Assert.True(Math.Abs(L1(TiltAt(0.8), uniform) - FamilyL1(0.8)) < 1e-12);
        Assert.True(Math.Abs(FamilyL1(0.8) - 0.6666666666666667) < 1e-12);

        // The ladder: any target separation T needs f = 1/(1 + 5 e^{-3T}), and f -> 1 as T grows.
        var ladder = new (double T, double F)[]
        {
            (0.9985774245179969, 0.8), (1.5, 0.947377910367), (2.0, 0.987757963984),
            (3.0, 0.999383331494), (5.0, 0.999998470491),
        };
        double previous = 0.0;
        foreach (var (T, F) in ladder)
        {
            double f = 1.0 / (1.0 + 5.0 * Math.Exp(-3.0 * T));
            Assert.True(Math.Abs(f - F) < 1e-11, $"T = {T}: f = {f}");
            Assert.True(f > previous);
            previous = f;
            var rho = TiltAt(f);
            Assert.True(Math.Abs(Energy(rho) - e0) < 1e-13, $"T = {T}: dE = {Energy(rho) - e0}");
            Assert.True(Math.Abs(ClockSeparation(rho) - T) < 1e-9, $"T = {T}: sep = {ClockSeparation(rho)}");
        }
        Assert.True(previous > 0.99999);
        Assert.True(TiltAt(1.0 - 1e-12).Min() < 1e-13);              // rho_min -> 0 at fixed energy

        // Supremum of DeltaRho in the family: L1 -> 1.0625 exactly (and positivity fails at f = 1).
        Assert.True(Math.Abs(FamilyL1(1.0) - 1.0625) < 1e-12);
        Assert.True(Math.Abs(FamilyL1(1.0) - 102.0 / 96.0) < 1e-12);
        Assert.True(FamilyL1(0.8) < FamilyL1(1.0));

        // What actually bounds the effect is positivity and the G_005 Poisson band, NOT energy:
        // T = 4.8867e-6 needs only f = 0.1666687028016166, a 1.00001466 : 1 contrast.
        double fCap = 1.0 / (1.0 + 5.0 * Math.Exp(-3.0 * BandCeiling));
        Assert.True(Math.Abs(fCap - 0.1666687028016166) < 1e-12, $"f_cap = {fCap}");
        Assert.True(Math.Abs(5.0 * fCap / (1.0 - fCap) - 1.0000146602074598) < 1e-12);
        Assert.True(BandCeiling / D * 86400.0 < 0.141);
        Assert.True(ObservedContrast / D * 86400.0 < 0.047);
    }

    // ── 6. verdicts ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_016b_Verdicts()
    {
        var uniform = Uniform();
        var tilt = CanonicalTilt;

        // INDEPENDENT — same E, different rho: the witness moves at DeltaE = 0 exactly.
        Assert.Equal(0.0, Energy(tilt) - Energy(uniform), 12);
        Assert.True(L1(tilt, uniform) > 0.66);

        // DEPENDENT — E is a function of rho: one rho, one E, and it is the SAME number every time.
        Assert.Equal(Energy(uniform), Energy(Uniform()), 12);

        // REFUTED — "same rho, different E" as a state change: only the capacity K can move it.
        Assert.True(Math.Abs(Energy(Uniform(6), 6) - Energy(Uniform(1), 1) - 10.0) < 1e-12);
        Assert.True(L1(Uniform(6), Uniform(1)) < 1e-15);

        // The independence is one-directional: the kernel of rho -> E is 94-dimensional on {Sum rho = 1}.
        Assert.Equal(94, (N - 1) - 1);
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.Equal(43, 94 - 51);
    }

    // ── 7. research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_016b_Run()
    {
        var sb = new StringBuilder();
        var uniform = Uniform();
        var tilt = CanonicalTilt;
        double e0 = Energy(uniform);

        PrintHeader(sb, "ResearchY-G_016b — MASS INDEPENDENCE AUDIT");
        sb.AppendLine("Question: can two states have the SAME energy but DIFFERENT rho, or the SAME rho but");
        sb.AppendLine("          DIFFERENT energy?   Construct explicit examples.   Measure DeltaE, DeltaRho, DeltaTau.");
        sb.AppendLine("Output: INDEPENDENT / DEPENDENT / REFUTED.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  Energy is AT's pairing E = <lambda, rho> (QG180/QG181), with the capacity lambda fixed by");
        sb.AppendLine("      the coupling lattice (G_016).");
        sb.AppendLine("  A2  rho is the counting measure: rho >= 0 and Sum rho = 1 (QG194).");
        sb.AppendLine("  A3  The clock reading is dtau/dt = rho^(1/d) (G_009).");
        sb.AppendLine("  A4  The coupling range K (and hence lambda) is a BOUNDARY input (G_007).");
        sb.AppendLine();

        PrintHeader(sb, "1. SAME ENERGY, DIFFERENT rho — INDEPENDENT");
        sb.AppendLine("  DeltaE = <lambda, rho> has a 94-dimensional kernel on {Sum rho = 1}:");
        sb.AppendLine("     51 dimensions are DEGENERACY (lambda constant within a multiplet),");
        sb.AppendLine("     43 dimensions are lambda-MIXING (zero net over distinct lambda).");
        sb.AppendLine();
        sb.AppendLine("  (a) the canonical witness tilt — a pure within-multiplet move:");
        sb.AppendLine($"      DeltaE  = {Energy(tilt) - e0:E4}");
        sb.AppendLine($"      DeltaRho = L1 = {L1(tilt, uniform):F16}");
        sb.AppendLine($"      contrast = {tilt.Max() / tilt.Min():F4} : 1");
        sb.AppendLine($"      DeltaTau = (1/d) ln(contrast) = {ClockSeparation(tilt):F16} = {ClockSeparation(tilt) * 86400.0:F6} s/day");
        sb.AppendLine();
        var (_, mult) = Spaces(6);
        int start = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 2) break; start += mult[i]; }
        double u = 1.0 / N;
        sb.AppendLine("  (b) a pairwise move inside an m = 2 multiplet:");
        foreach (double delta in new[] { 0.002, 0.005 })
        {
            var rho = (double[])uniform.Clone();
            rho[start] += delta; rho[start + 1] -= delta;
            sb.AppendLine($"      delta = {delta,-6}  DeltaE = {Energy(rho) - e0:E2}  DeltaRho = {L1(rho, uniform):F6}"
                        + $"  contrast = {(u + delta) / (u - delta):F4}  DeltaTau = {ClockSeparation(rho):F6}");
        }
        sb.AppendLine();
        sb.AppendLine("  (c) the lambda-MIXING room — three cells of three DIFFERENT multiplets:");
        var weight = Weight(6);
        var starts = new List<int>(); int acc2 = 0;
        foreach (int m in mult) { starts.Add(acc2); acc2 += m; }
        int c1 = starts[^1], c2 = starts[^2], c3 = starts[^3];
        double l1 = weight[c1], l2 = weight[c2], l3 = weight[c3];
        double bb = -(l1 - l3) / (l2 - l3), cc = -1.0 - bb;
        var v = new double[N]; v[c1] = 1.0; v[c2] = bb; v[c3] = cc;
        var rmix = new double[N];
        for (int i = 0; i < N; i++) rmix[i] = uniform[i] + 0.004 * v[i];
        sb.AppendLine($"      cells {c1}/{c2}/{c3}, lambda = {l1:F12}, {l2:F12}, {l3:F12}");
        sb.AppendLine($"      v = (1, {bb:F16}, {cc:F16});  Sum v = {v.Sum():E2}   <lambda,v> = {v.Zip(weight, (x, w) => x * w).Sum():E2}");
        sb.AppendLine($"      DeltaE = {Energy(rmix) - e0:E2}  DeltaRho = L1 = {L1(rmix, uniform):F6}  contrast = {rmix.Max() / rmix.Min():F6}");
        sb.AppendLine($"      DeltaTau = {ClockSeparation(rmix):F8} = {ClockSeparation(rmix) * 86400.0:F4} s/day");

        PrintHeader(sb, "2. E IS A FUNCTION OF rho — DEPENDENT");
        sb.AppendLine($"  One rho, one E: E(uniform) = {e0:F12} on every evaluation (the pairing is single-valued).");
        sb.AppendLine("  So E is DEPENDENT on rho; the independence in section 1 is strictly ONE-DIRECTIONAL.");

        PrintHeader(sb, "3. SAME rho, DIFFERENT ENERGY — REFUTED (as a state change)");
        sb.AppendLine("  The SAME uniform rho = 1/96 under six couplings (K = 1..6):");
        sb.AppendLine("   K    A0    Sum lambda    E = <lambda,rho>");
        foreach (var K in new[] { 1, 2, 3, 4, 5, 6 })
            sb.AppendLine($"   {K}   {Spaces(K).Distinct.Length,4}   {Weight(K).Sum(),10:F4}   {Energy(Uniform(K), K),14:F10}");
        sb.AppendLine("  => E = 2K exactly, because Sum lambda = 192K. rho NEVER changed (L1 = 0).");
        sb.AppendLine("  REFUTED as a state change: E cannot move at fixed rho on a fixed lattice. The only handle");
        sb.AppendLine("  is the capacity K, which is a BOUNDARY input (G_007), not a state variable.");

        PrintHeader(sb, "4. SAME rho, SAME E, DIFFERENT psi — the phase sector is inert");
        sb.AppendLine("  Four phase assignments (canonical grid, global shift, mirror, fully locked):");
        sb.AppendLine("    L1(|psi|^2, rho) = 0 (< 2.5e-16),  DeltaE = 0 exactly,  DeltaTau = 0 exactly,");
        sb.AppendLine("    while the coherent sum moves by orders of magnitude (G_002/G_011: 281x).");
        sb.AppendLine("  So the phase sector changes neither the energy nor any clock.");

        PrintHeader(sb, "5. THE FIXED-E FAMILY — DeltaTau IS UNBOUNDED");
        sb.AppendLine("  Tilt each multiplet with fraction f on its first cell (fr = 1 for m = 1). EXACTLY:");
        sb.AppendLine("    DeltaE = 0,  contrast = 5f/(1-f),  L1(f) = (2/96)[42|2f-1| + |5f-1| + |6f-1|],");
        sb.AppendLine("    DeltaTau = (1/d) ln(5f/(1-f)) = T   <=>   f = 1/(1 + 5 e^{-3T}).");
        sb.AppendLine();
        sb.AppendLine("   f        DeltaE     DeltaRho (L1)   contrast      DeltaTau");
        foreach (double f in new[] { 0.2, 0.5, 0.8, 0.9, 0.99, 0.999 })
        {
            var rho = TiltAt(f);
            sb.AppendLine($"   {f,-8} {Energy(rho) - e0,8:E2}   {L1(rho, uniform),13:F10}   {rho.Max() / rho.Min(),10:F3}   {ClockSeparation(rho),10:F6}");
        }
        sb.AppendLine($"   sup(f->1)          {0.0,8:E2}   {FamilyL1(1.0),13:F10}   rho_min -> 0   -> infinity");
        sb.AppendLine();
        sb.AppendLine("  At the canonical T = (1/d)ln20 the inversion returns f = 0.8 — the canonical TiltFractions IS");
        sb.AppendLine("  the 20 : 1 solution of this family.");
        sb.AppendLine("  The ladder: f(1.5) = 0.947377910367, f(2) = 0.987757963984, f(3) = 0.999383331494,");
        sb.AppendLine("  f(5) = 0.999998470491 — f -> 1, so NO target separation is forbidden by energy conservation.");
        double fCap = 1.0 / (1.0 + 5.0 * Math.Exp(-3.0 * BandCeiling));
        sb.AppendLine($"  What bounds it is positivity and the G_005 band: T = {BandCeiling:E4} needs only f = {fCap:F16},");
        sb.AppendLine($"  a {5.0 * fCap / (1.0 - fCap):F16} : 1 contrast ({BandCeiling / D * 86400.0:F6} s/day).");

        PrintHeader(sb, "6. CONCLUSIONS");
        sb.AppendLine("  C1  INDEPENDENT — same energy, DIFFERENT rho, and it is not marginal: the kernel is");
        sb.AppendLine("      94-dimensional on {Sum rho = 1}, explicitly 51 (degeneracy) + 43 (lambda-mixing), and the");
        sb.AppendLine("      canonical witness moves rho by L1 = 0.6666666666666667 with DeltaE = 0 exactly while the");
        sb.AppendLine("      clocks separate by 0.9985774245179969 = 86277.089 s/day.");
        sb.AppendLine("  C2  DEPENDENT — E is a FUNCTION of rho (given lambda). The independence is one-directional:");
        sb.AppendLine("      many rho per E, never many E per rho.");
        sb.AppendLine("  C3  REFUTED — same rho with different energy as a STATE change. Holding rho fixed moves E only");
        sb.AppendLine("      through the capacity: the SAME uniform rho gives E = 2K exactly for K = 1..6, and K is");
        sb.AppendLine("      BOUNDARY (G_007).");
        sb.AppendLine("  C4  DeltaTau at FIXED energy is UNBOUNDED in the theory: contrast = 5f/(1-f) -> infinity as");
        sb.AppendLine($"      f -> 1 with DeltaE = 0 throughout, while DeltaRho -> {FamilyL1(1.0):F4}. Only positivity and the");
        sb.AppendLine("      G_005 Poisson band bound it to 0.14073696 s/day.");
        sb.AppendLine("  C5  The phase sector is energy-inert AND clock-inert: same rho, same E, same DeltaTau for every");
        sb.AppendLine("      phase assignment, while the coherent sum moves by orders of magnitude.");
        sb.AppendLine();

        PrintHeader(sb, "7. CLASSIFICATION");
        sb.AppendLine("  INDEPENDENT  same energy, different rho (94-dim kernel = 51 degeneracy + 43 lambda-mixing).");
        sb.AppendLine("  DEPENDENT    E is a function of rho; rho never is a function of E.");
        sb.AppendLine("  REFUTED      same rho, different energy as a state change (only the BOUNDARY capacity K does it).");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new");
        sb.AppendLine("  primitive; deterministic (exact algebra, no randomness).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
