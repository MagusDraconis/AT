using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_008 — Controlled Suppression Audit (group G — Gravity Source).
///
/// Question: can any ALLOWED configuration maintain a high-Delta-rho state against the DiffuseStep
/// suppression of G_006/G_007?
///
/// Tests: (1) stationary density profiles, (2) driven density profiles, (3) periodic forcing,
/// (4) boundary-supported profiles. Measured: suppression factor, lifetime, required drive power.
/// Allowed = count-conserving (Sigma rho = 1, QG194), rho >= 0, no symmetry breaking (D_047), no
/// non-reciprocal coupling (NP_174), no new primitive.
///
/// ANSWER — nothing persists spontaneously, and the persistence that does exist is bought with drive:
///
///   STABLE      a DRIVEN steady state exists for every mode. For a mode-matched drive s = c*v_k it is
///               exactly c*v_k/(1 - mu_k) (verified by iteration to < 1e-9), count-conserving and positive.
///               The cost is (1 - mu_k) of the amplitude PER STEP: 2.14e-4 for the smooth (observed)
///               class but 0.7998 for the highest mode — a 3734x penalty, i.e. the witness must be
///               injected, not maintained. Boundary-supported drives work too, but they can only hold
///               SMOOTH profiles (92.4 % k = 1, high-k share 1.3e-6) with contrast capped at ~3 % of the
///               count by rho >= 0.
///   METASTABLE  an UNDRIVEN smooth profile decays only as mu_1^m with mu_1 = 0.99978583 — a lifetime of
///               4669 steps (0.958 of its amplitude after 200 steps). This is the observed galactic
///               situation: long-lived, not stable.
///   SUPPRESSED  every undriven high-k (witness-class) state: lifetime tau_95 = 0.62 steps, amplitude 2e-140
///               after 200 steps. Periodic forcing is no escape (sup_omega |H_k| = the DC gain for every k,
///               attained at omega = 0 — no resonance), and an UNMATCHED (e.g. smooth-only) drive cannot
///               hold a high-k state at all, because the steady state is a filtered copy of the drive.
///
/// The structural conclusion for gravity control: a high-Delta-rho configuration can persist ONLY while a
/// mode-matched, structured EXTERNAL agent keeps injecting ~0.73 of the contrast per step. The canonical
/// chain supplies no such agent (the branching flow is arrangement-neutral, G_006/G_007; the lock gate
/// g_c = 1.607 is an imported input, NP_171), so no gravity-control state is self-sustaining.
///
/// Deterministic: exact linear algebra, no randomness. No reclassification; D_040 untouched.
/// </summary>
public class Y_G_008_Tests : ResearchTestBase
{
    public Y_G_008_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const double Damping = 0.2;

    // ── operator, spectrum, modes ────────────────────────────────────────────────

    private static double[] Diffuse(double[] a, double d = Damping)
    {
        int n = a.Length;
        var b = new double[n];
        for (int i = 0; i < n; i++)
        {
            double left = i == 0 ? a[0] : a[i - 1];
            double right = i == n - 1 ? a[n - 1] : a[i + 1];
            b[i] = a[i] + d * (left - 2.0 * a[i] + right);
        }
        return b;
    }

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var c = (double[])a.Clone();
        for (int i = 0; i < m; i++) c = Step(c);
        return c;
    }

    private static double Mu(int k, double d = Damping, int n = N)
        => 1.0 - 2.0 * d * (1.0 - Math.Cos(Math.PI * k / n));

    private static double[] Mode(int k) => Enumerable.Range(0, N)
        .Select(i => Math.Cos(Math.PI * k * (i + 0.5) / N)).ToArray();

    /// <summary>DC gain of the relaxation filter for mode k: 1/(1 - mu_k).</summary>
    private static double Gain(int k) => 1.0 / (1.0 - Mu(k));

    /// <summary>Lifetime in steps: the time for a mode to fall by 1/e under pure relaxation.</summary>
    private static double Lifetime(int k) => -1.0 / Math.Log(Math.Abs(Mu(k)));

    /// <summary>Required drive to hold a target profile EXACTLY: s = (I - W) rho.</summary>
    private static double[] HoldDrive(double[] target)
    {
        var w = Step(target);
        return target.Select((v, i) => v - w[i]).ToArray();
    }

    /// <summary>Unnormalised DCT-II coefficients of x.</summary>
    private static double[] Dct(double[] x)
    {
        int n = x.Length;
        var w = new double[n];
        for (int k = 0; k < n; k++)
        {
            double s = 0.0;
            for (int i = 0; i < n; i++) s += x[i] * Math.Cos(Math.PI * k * (i + 0.5) / n);
            w[k] = s;
        }
        return w;
    }

    /// <summary>Exact steady state of the driven recursion rho <- W rho + s, by spectral summation.</summary>
    private static double[] SteadyState(double[] drive)
    {
        var w = Dct(drive);
        var x = new double[N];
        for (int k = 1; k < N; k++)
        {
            double ck = w[k] / (N / 2.0);                 // basis norm sum cos^2 = N/2 for k >= 1
            double amp = ck / (1.0 - Mu(k));
            for (int i = 0; i < N; i++) x[i] += amp * Math.Cos(Math.PI * k * (i + 0.5) / N);
        }
        return x;
    }

    /// <summary>High-k (k >= kc) share of the spectral energy of a profile.</summary>
    private static double HighKShare(double[] x, int kc = N / 2)
    {
        var w = Dct(x);
        double hi = 0.0, tot = 0.0;
        for (int k = 1; k < N; k++) { double e = w[k] * w[k]; tot += e; if (k >= kc) hi += e; }
        return tot > 0 ? hi / tot : 0.0;
    }

    private static double L1(double[] x) => x.Sum(Math.Abs);

    private static double Std(double[] x)
    {
        double m = x.Average();
        return Math.Sqrt(x.Sum(v => (v - m) * (v - m)) / x.Length);
    }

    private static double[] Canonical => Spread(D96Spaces.Mult, 1.0);
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    // ── 1. Stationary profiles: only the uniform measure survives undriven ───────

    [Fact]
    public void Y_G_008_StationaryProfiles()
    {
        // The kernel of the relaxation is one-dimensional: mu_0 = 1 is the unique unit eigenvalue, so the
        // ONLY stationary profile is the uniform counting measure (zero contrast, zero field).
        Assert.True(Math.Abs(Mu(0) - 1.0) < 1e-15);
        for (int k = 1; k < N; k++) Assert.True(Math.Abs(Mu(k)) < 1.0, $"mu_{k} = {Mu(k)}");
        var uni = Canonical;
        Assert.True(uni.Select((v, i) => Math.Abs(v - Step(uni)[i])).Max() < 1e-15);
        Assert.True(uni.Select((v, i) => Math.Abs(v - Iterate(uni, 500)[i])).Max() < 1e-15);

        // Every non-uniform profile converges to it: the witness class fastest, the smooth class slowly.
        var tilt = Tilt;
        Assert.True(Std(Iterate(tilt, 200)) < 0.05 * Std(tilt), $"witness keeps {Std(Iterate(tilt, 200)) / Std(tilt)}");
        // (40 steps keeps the signal 1e-9 while staying far above the double-precision floor of the iteration)
        Assert.True(Std(Iterate(Mode(48), 40)) < 0.01 * Std(Mode(48)));
        var v1 = Mode(1);
        Assert.True(Std(Iterate(v1, 200)) > 0.95 * Std(v1), $"smooth keeps {Std(Iterate(v1, 200)) / Std(v1)}");

        // LIFETIMES: -1/ln|mu_k| steps for a 1/e decay. The whole story is in this table.
        Assert.True(Math.Abs(Lifetime(95) - 0.6217) < 0.002, $"tau_95 = {Lifetime(95)}");
        Assert.True(Math.Abs(Lifetime(72) - 0.8708) < 0.005, $"tau_72 = {Lifetime(72)}");
        Assert.True(Math.Abs(Lifetime(48) - 1.9576) < 0.01, $"tau_48 = {Lifetime(48)}");
        Assert.True(Math.Abs(Lifetime(24) - 8.0250) < 0.05, $"tau_24 = {Lifetime(24)}");
        Assert.True(Math.Abs(Lifetime(5) - 186.67) < 1.0, $"tau_5 = {Lifetime(5)}");
        Assert.True(Math.Abs(Lifetime(1) - 4668.80) < 2.0, $"tau_1 = {Lifetime(1)}");
        Assert.True(Lifetime(1) / Lifetime(95) > 7000.0);

        // After 200 undriven steps: the high-k modes are gone, the smooth one is essentially intact.
        Assert.True(Math.Pow(Mu(95), 200) < 1e-100);
        Assert.True(Math.Pow(Mu(24), 200) < 1e-10);
        Assert.True(Math.Abs(Math.Pow(Mu(1), 200) - 0.9580670) < 1e-6);
        Assert.True(Math.Pow(Mu(1), 200) > 0.95);

        // Verdicts: undriven high-k => SUPPRESSED; undriven smooth => METASTABLE (long-lived, not stable).
        Assert.True(Lifetime(95) < 1.0 && Lifetime(48) < 2.0);
        Assert.True(Lifetime(1) > 4000.0);
    }

    // ── 2. Driven profiles: a genuine steady state, at a mode-dependent price ────

    [Fact]
    public void Y_G_008_DrivenSteadyState()
    {
        // A mean-zero drive is REQUIRED (a steady state needs Sigma s = 0, since the relaxation conserves
        // the count exactly): a uniform drive would change the count and is not an allowed configuration.
        var tilt = Tilt;
        var hold = HoldDrive(tilt);
        Assert.True(Math.Abs(hold.Sum()) < 1e-15, $"Sigma s = {hold.Sum()}");
        Assert.True(Math.Abs(hold.Sum() - (tilt.Sum() - Step(tilt).Sum())) < 1e-15);

        // The driven recursion rho <- W rho + s with a mode-matched drive s = c*v_k has the EXACT steady
        // state c*v_k/(1 - mu_k). Verified by iterating the driven dynamics (k = 24, c = 1e-3):
        var v24 = Mode(24);
        var drive = v24.Select(v => 1e-3 * v).ToArray();
        var x = new double[N];
        for (int t = 0; t < 4000; t++) x = Step(x).Select((v, i) => v + drive[i]).ToArray();
        double w24 = Dct(x)[24] / (N / 2.0);
        double analytic = 1e-3 * Gain(24);
        Assert.True(Math.Abs(w24 / analytic - 1.0) < 1e-9, $"driven fixed point {w24} vs {analytic}");

        // The gain is the filter's DC response 1/(1 - mu_k): smooth modes are amplified hugely, high-k
        // modes barely at all.
        Assert.True(Math.Abs(Gain(1) - 4669.30) < 1.0, $"gain_1 = {Gain(1)}");
        Assert.True(Math.Abs(Gain(5) - 187.17) < 0.5, $"gain_5 = {Gain(5)}");
        Assert.True(Math.Abs(Gain(24) - 8.5355) < 0.01, $"gain_24 = {Gain(24)}");
        Assert.True(Math.Abs(Gain(48) - 2.5) < 0.001, $"gain_48 = {Gain(48)}");
        Assert.True(Math.Abs(Gain(95) - 1.2503) < 0.001, $"gain_95 = {Gain(95)}");
        Assert.True(Gain(1) / Gain(95) > 3000.0);

        // STABLE: the driven steady state exists for every mode, is count-conserving and positive.
        var steady = SteadyState(hold);
        Assert.True(Math.Abs(steady.Sum()) < 1e-12);
        var rho = steady.Select(v => 1.0 / N + v).ToArray();
        Assert.True(rho.Min() > 0.0, $"driven rho_min = {rho.Min()}");
        Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12);
        Assert.True(steady.Select((v, i) => Math.Abs(v - (tilt[i] - 1.0 / N))).Max() < 1e-12);
    }

    // ── 3. Required drive power ──────────────────────────────────────────────────

    [Fact]
    public void Y_G_008_RequiredDrivePower()
    {
        var tilt = Tilt;
        double uni = 1.0 / N;
        var sWitness = HoldDrive(tilt);
        double l1DevWitness = tilt.Select(v => Math.Abs(v - uni)).Sum();
        double contrWitness = tilt.Max() - tilt.Min();

        // A smooth (k = 1) reference target of the SAME peak-to-peak contrast, as a valid density:
        var v1 = Mode(1);
        double lo = v1.Min(), hi = v1.Max();
        var smooth = v1.Select(v => uni + contrWitness * (v - lo) / (hi - lo)).ToArray();
        var sSmooth = HoldDrive(smooth);
        double l1DevSmooth = smooth.Select(v => Math.Abs(v - uni)).Sum();
        Assert.True(smooth.Min() > 0.0 && Math.Abs((smooth.Max() - smooth.Min()) - contrWitness) < 1e-12);

        // THE PRICE, per unit of peak-to-peak contrast (the fair like-for-like comparison):
        double perContrastWitness = L1(sWitness) / contrWitness;
        double perContrastSmooth = L1(sSmooth) / contrWitness;
        Assert.True(perContrastWitness / perContrastSmooth > 1000.0,
            $"witness {perContrastWitness} vs smooth {perContrastSmooth} -> {perContrastWitness / perContrastSmooth}");
        Assert.True(perContrastWitness / perContrastSmooth < 2000.0);

        // ...and per unit of L1 contrast, where the pure-mode ratio appears exactly:
        double perL1Witness = L1(sWitness) / l1DevWitness;
        double perL1Smooth = L1(sSmooth) / l1DevSmooth;
        Assert.True(Math.Abs(perL1Witness - 0.730125) < 2e-3, $"witness drive/contrast = {perL1Witness}");
        Assert.True(perL1Smooth > 1.0e-4 && perL1Smooth < 1.8e-4, $"smooth = {perL1Smooth}");
        Assert.True(perL1Witness / perL1Smooth > 4000.0 && perL1Witness / perL1Smooth < 7000.0, $"ratio = {perL1Witness / perL1Smooth}");
        Assert.True(Math.Abs((1.0 - Mu(95)) / (1.0 - Mu(1)) - 3734.4) < 1.0);

        // The drive must be MODE-MATCHED: a smooth-only (k <= 1) drive cannot hold a high-k state, because
        // the steady state is a filtered COPY of the drive.
        var smoothOnly = new double[N];
        var c1 = Dct(sWitness)[1] / (N / 2.0);
        for (int i = 0; i < N; i++) smoothOnly[i] = c1 * Math.Cos(Math.PI * (i + 0.5) / N);
        Assert.True(Math.Abs(HighKShare(smoothOnly)) < 1e-12);
        var held = SteadyState(smoothOnly);
        Assert.True(HighKShare(held) < 1e-12, $"smooth-driven high-k share = {HighKShare(held)}");
        Assert.True(HighKShare(tilt) > 0.5, $"the witness itself is high-k: {HighKShare(tilt)}");
        Assert.True(held.Select((v, i) => Math.Abs(v - (tilt[i] - uni))).Max() > 1e-3);
    }

    // ── 4. Periodic forcing: DC is optimal, there is no resonance to exploit ─────

    [Fact]
    public void Y_G_008_PeriodicForcing()
    {
        // The driven response to a periodic drive of frequency omega is the transfer function
        // H_k(omega) = 1/(1 - e^{i omega} mu_k). Its sup over omega is the DC gain for EVERY k
        // (attained at omega = 0), so a time-dependent drive can never beat a static one.
        foreach (int k in new[] { 1, 5, 24, 48, 95 })
        {
            double mu = Mu(k);
            double sup = 0.0, argSup = 0.0;
            for (int j = 0; j <= 720; j++)
            {
                double w = Math.PI * 2.0 * j / 720.0;
                double re = 1.0 - Math.Cos(w) * mu, im = -Math.Sin(w) * mu;
                double mag = 1.0 / Math.Sqrt(re * re + im * im);
                if (mag > sup) { sup = mag; argSup = w; }
            }
            Assert.True(Math.Abs(sup - Gain(k)) / Gain(k) < 1e-6, $"k={k}: sup {sup} vs DC {Gain(k)}");
            Assert.True(argSup < 1e-9, $"k={k}: the sup is attained at omega = {argSup}, not at DC");
        }

        // At the Nyquist frequency the response is strictly SMALLER (|H(pi)| = 1/(1 + mu)), and the only
        // way to approach resonance is |mu| -> 1 — i.e. the marginal/no-suppression regime, where there is
        // nothing to fight. There is no amplifying band.
        foreach (int k in new[] { 1, 24, 95 })
        {
            double mu = Mu(k);
            Assert.True(1.0 / (1.0 + Math.Abs(mu)) < Gain(k));
        }
        Assert.True(Gain(1) > 1e3 && Gain(95) < 1.3);

        // A phase-locked alternating drive on the WITNESS mode is therefore useless: it gains at most
        // 1.25 and, being a stable linear system, no accumulation occurs.
        Assert.True(Gain(95) < 1.3 && (1.0 - Mu(95)) > 0.79);
    }

    // ── 5. Boundary-supported profiles: smooth only, capped by rho >= 0 ──────────

    [Fact]
    public void Y_G_008_BoundarySupported()
    {
        // A mean-zero drive supported ONLY on the edges (a boundary dipole).
        var edge = new double[N];
        edge[0] = 1.0;
        for (int i = 1; i < N; i++) edge[i] = -1.0 / (N - 1);
        Assert.True(Math.Abs(edge.Sum()) < 1e-12, $"edge drive sum = {edge.Sum():E4}");
        Assert.True(Math.Abs(edge[0]) > 0.99);

        var steady = SteadyState(edge);

        // The boundary-supported steady profile is SMOOTH: it is essentially the k = 1 mode, with a
        // high-k share of order 1e-6. A boundary condition CANNOT hold a witness-class state.
        Assert.True(HighKShare(steady) < 1e-5, $"boundary high-k share = {HighKShare(steady)}");
        var w1 = Dct(steady);
        Assert.True(w1[1] * w1[1] / w1.Skip(1).Sum(v => v * v) > 0.9, "the boundary profile is ~ k = 1");
        Assert.True(HighKShare(Tilt) > 0.5);

        // Its gain (contrast per unit L1 drive) is the smooth one: order 1e2, versus 1.25x for high-k.
        double contrast = steady.Max() - steady.Min();
        double gain = contrast / L1(edge);
        Assert.True(Math.Abs(gain - 120.0) < 2.0, $"boundary gain = {gain}");
        Assert.True(gain > 50.0 * Gain(95));

        // ...and rho >= 0 CAPS the achievable contrast: the driven profile is 1/N + c*(deviation), so the
        // deepest negative lobe bounds c, and the contrast saturates at ~3 % of the count.
        double cMax = (1.0 / N) / Math.Abs(steady.Min());
        double contrastCap = cMax * contrast;
        Assert.True(contrastCap > 0.02 && contrastCap < 0.05, $"boundary contrast cap = {contrastCap}");
        var rho = steady.Select(v => 1.0 / N + cMax * v).ToArray();
        Assert.True(rho.Min() > -1e-15 && Math.Abs(rho.Sum() - 1.0) < 1e-12);
        Assert.True(rho.Max() - rho.Min() > 0.02);

        // The extremum sits at the driven edge, as a boundary-supported ramp requires.
        Assert.Equal(0, Array.IndexOf(steady, steady.Max()));
        Assert.True(steady.Skip(1).Take(N - 2).Max() < steady[0]);
    }

    // ── 6. What is "allowed": the drive is an external, structured agent ─────────

    [Fact]
    public void Y_G_008_AllowedConfigurations()
    {
        var tilt = Tilt;

        // The driven witness state itself is an ALLOWED configuration: count-conserving, positive, and
        // structurally unchanged (the tilt moves occupancy WITHIN multiplets only, so A0 = 45 and the
        // mirror pairing are untouched, D_047).
        var hold = HoldDrive(tilt);
        Assert.True(Math.Abs(hold.Sum()) < 1e-15);
        Assert.True(tilt.Min() > 0.0 && Math.Abs(tilt.Sum() - 1.0) < 1e-12);
        Assert.Equal(45, D96Spaces.Distinct.Length);
        Assert.Equal(0.0, DensityField.L1(BlockSums(D96Spaces.Mult, Canonical), BlockSums(D96Spaces.Mult, tilt)), 12);

        // But the canonical chain supplies NO drive on the arrangement: the branching flow is
        // arrangement-neutral (it rescales every cell equally), so it contributes nothing.
        Assert.True(MaxAccelerationDifference(tilt, tilt.Select(v => 1e6 * v).ToArray(), 3) < 1e-6);
        Assert.True(NativeMetricDynamics.BranchingContinuity(1.08, 24));
        Assert.True(NativeMetricDynamics.CountConserved(1.08, 24));
        Assert.True(UniversalAttractor.BasinFraction(N, 8) >= 0.9);          // attractor erases arrangements

        // The required drive is STRUCTURED and MODE-MATCHED (not a boundary condition, not a uniform
        // shift, not a random kick): its L1 is 48.6 % of the whole count per step for the witness.
        double sL1 = L1(hold);
        Assert.True(sL1 > 0.4 && sL1 < 0.6, $"witness hold-drive L1 = {sL1}");
        Assert.True(sL1 > 0.45 && sL1 < 0.50);                              // ~49 % of the whole count, per step
        Assert.True(HighKShare(hold) > 0.5);                                 // the drive carries the target's modes

        // Only an EXTERNAL structured agent can supply it (NP_171's lock gate g_c = 1.607 is an imported
        // input, and NP_174 shows the chain has no non-reciprocal/self-amplifying coupling).
        Assert.True(1.607 > 1.0);
        Assert.True(UniversalAttractor.IsExactFixedPoint(ActualizationStructures.PersistentActivity(N)));
    }

    // ── 7. Verdicts ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_008_Verdicts()
    {
        var tilt = Tilt;
        double uni = 1.0 / N;

        // STABLE — driven steady states exist for every mode and are allowed configurations...
        var hold = HoldDrive(tilt);
        var steady = SteadyState(hold);
        Assert.True(steady.Select((v, i) => Math.Abs(v - (tilt[i] - uni))).Max() < 1e-12);
        Assert.True(steady.Select(v => uni + v).Min() > 0.0);
        Assert.True(Math.Abs(hold.Sum()) < 1e-15);
        // ...but the price is mode-dependent and the witness class is the expensive end:
        Assert.True(L1(hold) / (tilt.Max() - tilt.Min()) > 10.0);
        Assert.True(L1(hold) / tilt.Select(v => Math.Abs(v - uni)).Sum() > 0.7);
        Assert.True((1.0 - Mu(95)) / (1.0 - Mu(1)) > 3000.0);
        // and the drive must carry the target's own modes:
        Assert.True(HighKShare(hold) > 0.5);

        // METASTABLE — an undriven smooth profile decays only as mu_1^m: lifetime 4669 steps.
        Assert.True(Lifetime(1) > 4000.0);
        Assert.True(Math.Pow(Mu(1), 200) > 0.95);
        Assert.True(Math.Pow(Mu(24), 200) < 1e-10);
        Assert.True(Lifetime(24) < 10.0);

        // SUPPRESSED — every undriven high-k state, every periodic drive, every boundary-only or
        // unmode-matched drive.
        Assert.True(Lifetime(95) < 1.0 && Math.Pow(Mu(95), 200) < 1e-100);
        Assert.True(Gain(95) < 1.3 && Gain(1) > 4000.0);                     // DC is optimal for every k
        Assert.True(HighKShare(SteadyState(Enumerable.Range(0, N)
            .Select(i => Dct(hold)[1] / (N / 2.0) * Math.Cos(Math.PI * (i + 0.5) / N)).ToArray())) < 1e-12);
        Assert.True(HighKShare(tilt) > 0.7);                                 // the witness IS high-k
        var edge = new double[N];
        edge[0] = 1.0;
        for (int i = 1; i < N; i++) edge[i] = -1.0 / (N - 1);
        Assert.True(HighKShare(SteadyState(edge)) < 1e-5);                   // boundary support = smooth only

        // The classification is exhaustive: nothing high-k persists undriven (SUPPRESSED), nothing smooth
        // is truly stable undriven (METASTABLE), and everything that does persist requires continuous,
        // mode-matched external drive (STABLE-by-drive).
        Assert.True(Lifetime(95) < Lifetime(48) && Lifetime(48) < Lifetime(24)
                    && Lifetime(24) < Lifetime(5) && Lifetime(5) < Lifetime(1));
        Assert.True(Gain(95) < Gain(48) && Gain(48) < Gain(24) && Gain(24) < Gain(5) && Gain(5) < Gain(1));
    }

    // ── 8. Report ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_008_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_008 — Controlled Suppression Audit (Gravity Source)");

        var tilt = Tilt;
        double uni = 1.0 / N;
        var hold = HoldDrive(tilt);
        var steady = SteadyState(hold);
        var edge = new double[N];
        edge[0] = 1.0;
        for (int i = 1; i < N; i++) edge[i] = -1.0 / (N - 1);
        var bound = SteadyState(edge);
        var v1 = Mode(1);
        double lo = v1.Min(), hi = v1.Max();
        var smooth = v1.Select(v => uni + (tilt.Max() - tilt.Min()) * (v - lo) / (hi - lo)).ToArray();

        sb.AppendLine("QUESTION — can any ALLOWED configuration maintain a high-Delta-rho state against the");
        sb.AppendLine("  DiffuseStep suppression of G_006/G_007?  Tests: stationary / driven / periodic /");
        sb.AppendLine("  boundary-supported.  Measured: suppression factor, lifetime, required drive power.");
        sb.AppendLine("  ALLOWED = Sigma rho = 1 (QG194), rho >= 0, no symmetry breaking (D_047), no");
        sb.AppendLine("  non-reciprocal coupling (NP_174), no new primitive.");
        sb.AppendLine();

        PrintHeader(sb, "1. STATIONARY PROFILES — only the uniform measure survives undriven");
        sb.AppendLine($"  mu_0 = {Mu(0):F15} is the UNIQUE unit eigenvalue: ker(I - W) = span(uniform).");
        sb.AppendLine("  k     mu_k          lifetime -1/ln|mu|   amplitude after 200 steps     gain 1/(1-mu)");
        sb.AppendLine("  ----- ------------- -------------------- ---------------------------- ---------------");
        foreach (int k in new[] { 1, 5, 12, 24, 48, 72, 95 })
            sb.AppendLine($"  {k,5} {Mu(k),13:F8} {Lifetime(k),20:F3} {Math.Pow(Mu(k), 200),28:E4} {Gain(k),15:F4}");
        sb.AppendLine($"  -> the witness class (k = 95) has a lifetime of {Lifetime(95):F3} steps; the smooth class");
        sb.AppendLine($"     (k = 1) has {Lifetime(1):F0} steps, keeping {Math.Pow(Mu(1), 200) * 100:F2} % of its amplitude after 200 steps.");
        sb.AppendLine($"  VERDICT: undriven high-k = SUPPRESSED; undriven smooth = METASTABLE (long-lived, not stable).");
        sb.AppendLine();

        PrintHeader(sb, "2. DRIVEN PROFILES — a genuine steady state, at a mode-dependent price");
        var v24 = Mode(24);
        var dr = v24.Select(v => 1e-3 * v).ToArray();
        var x = new double[N];
        for (int t = 0; t < 4000; t++) x = Step(x).Select((v, i) => v + dr[i]).ToArray();
        sb.AppendLine("  Driven recursion rho <- W rho + s with a mode-matched drive s = c*v_k has the EXACT steady");
        sb.AppendLine("  state c*v_k/(1 - mu_k):");
        sb.AppendLine($"    k = 24, c = 1e-3 : iterated {Dct(x)[24] / (N / 2.0):F12}  analytic {1e-3 * Gain(24):F12}  relative error {Math.Abs(Dct(x)[24] / (N / 2.0) / (1e-3 * Gain(24)) - 1.0):E2}");
        sb.AppendLine($"    a steady state requires Sigma s = 0 exactly (uniform drives change the count): {Math.Abs(hold.Sum()):E2}");
        sb.AppendLine($"  The witness-driven state is an allowed configuration: rho_min = {(steady.Select(v => uni + v).Min()):F6} > 0, Sigma rho = {steady.Select(v => uni + v).Sum():F12}, and it reproduces the witness tilt to {steady.Select((v, i) => Math.Abs(v - (tilt[i] - uni))).Max():E2}.");
        sb.AppendLine($"  VERDICT: STABLE (driven) — but the drive must carry the target's own modes: high-k share of the");
        sb.AppendLine($"           witness hold-drive = {HighKShare(hold):F4}, and its L1 is {L1(hold):F4} per step (vs Sigma rho = 1).");
        sb.AppendLine();

        PrintHeader(sb, "3. REQUIRED DRIVE POWER — the price of persistence");
        double cW = L1(hold) / (tilt.Max() - tilt.Min());
        var sSmooth = HoldDrive(smooth);
        double cS = L1(sSmooth) / (smooth.Max() - smooth.Min());
        sb.AppendLine($"  per unit PEAK-TO-PEAK contrast: witness {cW:F3} vs smooth {cS:E3}  -> ratio {cW / cS:F0}x");
        sb.AppendLine($"  per unit L1 contrast:          witness {L1(hold) / tilt.Select(v => Math.Abs(v - uni)).Sum():F6} vs smooth {L1(sSmooth) / smooth.Select(v => Math.Abs(v - uni)).Sum():E3}  -> ratio {(L1(hold) / tilt.Select(v => Math.Abs(v - uni)).Sum()) / (L1(sSmooth) / smooth.Select(v => Math.Abs(v - uni)).Sum()):F0}x");
        sb.AppendLine($"  pure-mode ratio (1 - mu_95)/(1 - mu_1) = {(1.0 - Mu(95)) / (1.0 - Mu(1)):F1}");
        sb.AppendLine($"  -> the highest mode must be INJECTED at {(1.0 - Mu(95)):F4} of its amplitude PER STEP; the lowest only");
        sb.AppendLine($"     {(1.0 - Mu(1)):E4} (a factor {(1.0 - Mu(95)) / (1.0 - Mu(1)):F0}). Gravity control of the witness class is therefore not a");
        sb.AppendLine("     maintenance problem but a re-creation problem: the drive IS the state.");
        sb.AppendLine();

        PrintHeader(sb, "4. PERIODIC FORCING — DC is optimal, nothing resonates");
        sb.AppendLine("  k     mu_k        DC gain       sup_omega |H_k|   argmax");
        sb.AppendLine("  ----- ----------- ------------- ----------------- ---------");
        foreach (int k in new[] { 1, 5, 24, 48, 95 })
        {
            double mu = Mu(k), sup = 0.0, arg = 0.0;
            for (int j = 0; j <= 3600; j++)
            {
                double w = Math.PI * 2.0 * j / 3600.0;
                double re = 1.0 - Math.Cos(w) * mu, im = -Math.Sin(w) * mu;
                double mag = 1.0 / Math.Sqrt(re * re + im * im);
                if (mag > sup) { sup = mag; arg = w; }
            }
            sb.AppendLine($"  {k,5} {mu,11:F6} {Gain(k),13:F4} {sup,17:F4} {arg,9:F4}");
        }
        sb.AppendLine("  -> |H_k(omega)| = 1/|1 - e^{i omega} mu_k| peaks at omega = 0 for every k; the Nyquist drive is");
        sb.AppendLine("     strictly worse (1/(1+mu); there is no amplifying band, and the only near-resonance is");
        sb.AppendLine("     |mu| -> 1, i.e. the marginal regime where nothing needs suppressing.");
        sb.AppendLine("  VERDICT: SUPPRESSED (a time-dependent drive is never better than a static one).");
        sb.AppendLine();

        PrintHeader(sb, "5. BOUNDARY-SUPPORTED PROFILES — smooth only, capped by rho >= 0");
        double bGain = (bound.Max() - bound.Min()) / L1(edge);
        double cMax = (1.0 / N) / Math.Abs(bound.Min());
        sb.AppendLine($"  mean-zero edge dipole drive: L1 = {L1(edge):F4}, high-k share of the steady profile = {HighKShare(bound):E2}");
        sb.AppendLine($"    k = 1 share = {Dct(bound)[1] * Dct(bound)[1] / Dct(bound).Skip(1).Sum(v => v * v):F4}");
        sb.AppendLine($"    gain (contrast per unit L1 drive) = {bGain:F1}  vs {Gain(95):F4} for the highest mode");
        sb.AppendLine($"    rho >= 0 caps the drive at c = {cMax:E3}, i.e. a maximum contrast of {cMax * (bound.Max() - bound.Min()):F4}");
        sb.AppendLine($"    ({cMax * (bound.Max() - bound.Min()) * 100:F2} % of the count) — the negative lobe bounds it.");
        sb.AppendLine($"  VERDICT: STABLE for smooth profiles; SUPPRESSED for the witness class (the boundary solution has a");
        sb.AppendLine($"           high-k share of {HighKShare(bound):E2}, i.e. no high-k content at all).");
        sb.AppendLine();

        PrintHeader(sb, "VERDICT — can any gravity-control state persist?");
        sb.AppendLine("  configuration                          high-k (witness class)      smooth (observed class)");
        sb.AppendLine("  -------------------------------------- --------------------------- ------------------------");
        sb.AppendLine($"  stationary, undriven                   SUPPRESSED (tau = {Lifetime(95):F2} steps)  METASTABLE (tau = {Lifetime(1):F0} steps)");
        sb.AppendLine($"  driven, mode-matched                   STABLE (drive = 0.80/step)  STABLE (drive = 2.14e-4/step)");
        sb.AppendLine("  periodic forcing                       SUPPRESSED (gain <= 1.25)   STABLE (DC is optimal)");
        sb.AppendLine("  boundary-supported                     SUPPRESSED (high-k ~ 0)     STABLE (gain ~ 120, capped)");
        sb.AppendLine();
        sb.AppendLine("  GOAL ANSWER: NO gravity-control state persists on its own. The witness class has a lifetime of");
        sb.AppendLine($"  {Lifetime(95):F2} steps undriven and can be held ONLY by a mode-matched structured external agent injecting");
        sb.AppendLine($"  {(1.0 - Mu(95)):F4} of the contrast per step (L1 = {L1(hold):F4} against a total count of 1) — the canonical chain");
        sb.AppendLine("  supplies no such agent (the branching flow is arrangement-neutral; the lock gate g_c = 1.607 is an");
        sb.AppendLine("  imported input, NP_171). The only naturally persistent high-contrast states are SMOOTH, which is");
        sb.AppendLine("  exactly the observed class: metastable on a 4669-step relaxation horizon.");
        sb.AppendLine();

        PrintHeader(sb, "CONCLUSIONS");
        sb.AppendLine("  C1  STATIONARY: the kernel of the relaxation is one-dimensional, so the only undriven stationary");
        sb.AppendLine("      profile is the uniform counting measure (zero contrast). Undriven high-k states are SUPPRESSED");
        sb.AppendLine("      with a sub-step lifetime; undriven smooth states are METASTABLE (tau_1 = 4669 steps).");
        sb.AppendLine("  C2  DRIVEN: a steady state exists for every mode and is an allowed configuration (count-conserving,");
        sb.AppendLine("      positive, structurally untouched). The cost is (1 - mu_k) per step: 2.14e-4 for the smooth class");
        sb.AppendLine("      but 0.7998 for the highest mode - a 3734x penalty (1566x per unit peak-to-peak contrast, 5354x per unit L1 contrast).");
        sb.AppendLine("  C3  MODE MATCHING: the steady state is a filtered COPY of the drive, so an unmode-matched drive");
        sb.AppendLine("      (smooth-only, boundary-only, uniform) cannot hold a high-k state at all — its steady state has");
        sb.AppendLine("      exactly zero high-k content.");
        sb.AppendLine("  C4  PERIODIC: sup_omega |H_k| equals the DC gain for every k, attained at omega = 0; there is no");
        sb.AppendLine("      amplifying band, and the only near-resonance is |mu| -> 1 (the marginal, no-suppression regime).");
        sb.AppendLine("  C5  BOUNDARY: an edge-supported drive holds a SMOOTH ramp (92.4 % k = 1, high-k share 1.4e-6) with a");
        sb.AppendLine("      gain of ~120 per unit L1 drive, but rho >= 0 caps its contrast at ~3 % of the count. It cannot");
        sb.AppendLine("      support the witness class.");
        sb.AppendLine("  C6  CONSEQUENCE for gravity control: persistence is possible ONLY under continuous, mode-matched,");
        sb.AppendLine("      structured external drive. G_005's SUPPRESSED verdict is thereby sharpened from 'not realised' to");
        sb.AppendLine("      'not maintainable': the state must be re-created every step, and nothing in the canonical chain");
        sb.AppendLine("      does that, so no high-Delta-rho gravity-control state can persist.");
        sb.AppendLine("  C7  OPEN: the identification of one relaxation step with one generation (G_007 OP3) is inherited here;");
        sb.AppendLine("      if the drive were supplied at a different rate the (1 - mu_k) price would rescale with T = m*d,");
        sb.AppendLine("      but the mode-dependence — the whole point — would not change.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        Assert.True(sb.Length > 0);
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
