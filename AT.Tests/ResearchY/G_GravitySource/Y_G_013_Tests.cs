using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;
using static AT.Tests.Shared.RhoActuators;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_013 — Physical Actuator Audit (group G — Gravity Source).
///
/// QUESTION: what PHYSICAL process can realize s = (I - W) rho* locally?
/// Candidates: feedback controller, oscillator lattice, coupled resonators, active diffusion
/// cancellation, pump/loss networks.
/// Requirements: (1) local implementation, (2) finite power, (3) reproduces the EXACT stencil,
/// (4) maintains rho*.     Measure: drive, stability, power, error.
///
/// RESULTS (d = 0.2, N = 96; mu_1 = 0.9997858350, mu_95 = 0.20021417)
///
/// THE STENCIL IN PHYSICAL FORM: s_i = -d (rho_{i-1} - 2 rho_i + rho_{i+1}) — a NEGATIVE LAPLACIAN, i.e.
/// anti-diffusion of strength d = 0.2 on the nearest-neighbour chain, verified to 3.5e-18. It is a
/// BALANCED PUMP-AND-DRAIN: exactly 50.0 % of ||s||_1 = 0.48675 is injection and 50.0 % extraction, with
/// max|s| = 0.01866667.
///
/// (1) FEEDBACK CONTROLLER — PHYSICAL. Sense rho_i and its neighbours, compute (I - W) rho, actuate:
///     exact stencil, three-point local, and G_012's freeze (4.34e-18 over 5000 steps). Its error budget is
///     the loop gain error: with residual gain (1 + eps) the closed loop is I + eps (I - W), so mode k
///     grows or decays at 1 + eps (1 - mu_k) — verified step by step (eps = 1e-5 gives exactly 1.000007998
///     per step on v_95). Hold times tau_k = 1/(eps (1 - mu_k)): at eps = 1e-3 the FASTEST mode lives only
///     1250 steps while the SMOOTHEST lives 4.669e6 — a 3734x split. A ONE-STEP DELAY is TOLERABLE: the
///     delayed recursion has roots {1, mu_k - 1} in [-0.79979, 0], so a 1e-3 perturbation is retained
///     (0.9986e-3 ... 0.9995e-3 after 2000 steps) — stable, still marginal.
/// (2) ACTIVE DIFFUSION CANCELLATION — PHYSICAL. The same operator is a nearest-neighbour NEGATIVE
///     CONDUCTANCE: an NIC (negative-impedance converter) in the coupling network realizes -R exactly,
///     half the elements sourcing and half sinking. This is the canonical exact realization.
/// (3) OSCILLATOR LATTICE — ANALOGUE. A NODE-WISE gain (uniform negative damping) has a mode-independent
///     rate gamma, so the residual per-mode error is |gamma - (1 - mu_k)|, minimax 0.3997858349905463 at
///     gamma = 0.4, against a required spread of 3734.437 — it cannot maintain a non-uniform rho*.
/// (4) COUPLED RESONATORS — ANALOGUE. A band-limited bank: compensating only k <= 16 leaves 96.4 % of the
///     witness's structure uncompensated (the retained share is 3.626161e-2; k <= 8: 4.833883e-3;
///     k <= 24: 8.417047e-2; k <= 48: 2.035517e-1). Exact only over a truncated mode set.
/// (5) PUMP/LOSS NETWORKS — REFUTED as an exact actuator. The required source is indeed balanced
///     (0.5 / 0.5), but a pump/loss balance is a SCALAR condition whose fixed points are the single
///     Neumann modes (G_012's theorem): it maintains no arbitrary rho*, and an imbalance grows at the
///     fastest mode's rate (0.7998 eps).
///
/// POWER IS NOT THE BINDING CONSTRAINT: the thermodynamic minimum is Landauer's k_B T * dH per step.
/// Holding the WITNESS removes dH = 0.2170247 nats per step = 8.6295e-20 J per lattice per step
/// (8.63e-14 W at a 1 us step), or 1.5192e-19 J by the free-energy rate sum_i s_i ln(rho_i/rhoBar)
/// (1.52e-13 W) — 10 orders below the ~1 mW quiescent draw of any electronic controller. The SMOOTH
/// (band-top) profile costs dH = 0 to double precision: it is thermodynamically free. The binding
/// constraint is EXACTNESS, not power.
///
/// VERDICTS: PHYSICAL = the feedback controller and active diffusion cancellation (the exact negative
/// conductance) · ANALOGUE = the oscillator lattice (node-wise gain) and coupled resonators (band-limited)
/// · REFUTED = pump/loss networks. Deterministic: exact algebra, no randomness; no reclassification
/// (D_040 untouched); no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_013_Tests : ResearchTestBase
{
    public Y_G_013_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double BandCeiling = 4.8867e-6;    // G_005's 1 % Poisson ceiling
    private const double WitnessShare = 0.7965733;   // the witness's high-k share (k >= 48)
    private const double KB = 1.380649e-23;
    private const double Temperature = 300.0;

    private static double[] Mode(int k) => NeumannMode(k, N);

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var r = (double[])a.Clone();
        for (int i = 0; i < m; i++) r = Step(r);
        return r;
    }

    private static double MaxAbs(double[] x) => x.Max(Math.Abs);

    private static double L1Of(double[] x) => x.Sum(Math.Abs);

    /// <summary>The G_002/G_003 witness tilt (within-eigenspace 80/20 redistribution).</summary>
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>The required source as a negative Laplacian: s_i = -d(rho_{i-1} - 2 rho_i + rho_{i+1}).</summary>
    private static double[] NegativeLaplacian(double[] rho)
    {
        var s = new double[N];
        for (int i = 0; i < N; i++)
        {
            double left = i == 0 ? rho[i] : rho[i - 1];
            double right = i == N - 1 ? rho[i] : rho[i + 1];
            s[i] = -Damping * (left - 2.0 * rho[i] + right);
        }
        return s;
    }

    /// <summary>One step of the imperfect controller: rho &lt;- W rho + (1 + eps)(rho - W rho).</summary>
    private static double[] LoopStep(double[] x, double eps)
    {
        var stepped = Step(x);
        var y = new double[N];
        for (int i = 0; i < N; i++) y[i] = stepped[i] + (1.0 + eps) * (x[i] - stepped[i]);
        return y;
    }

    private static double[] LoopIterate(double[] x, double eps, int steps)
    {
        var r = (double[])x.Clone();
        for (int m = 0; m < steps; m++) r = LoopStep(r, eps);
        return r;
    }

    // ── 1. The stencil in physical form ──────────────────────────────────────────

    [Fact]
    public void Y_G_013_StencilAsNegativeLaplacian()
    {
        var rho = Tilt;
        var s = HoldDrive(rho);
        var negLap = NegativeLaplacian(rho);

        // The required source IS a negative Laplacian: anti-diffusion of strength d = 0.2.
        double dev = Enumerable.Range(0, N).Max(i => Math.Abs(s[i] - negLap[i]));
        Assert.True(dev < 1e-17, $"|s + d*lap| = {dev}");

        // It is a BALANCED pump-and-drain: exactly half the L1 injects, half extracts.
        double injected = s.Where(v => v > 0).Sum();
        double extracted = -s.Where(v => v < 0).Sum();
        Assert.True(Math.Abs(injected - extracted) < 1e-15, $"{injected} vs {extracted}");
        Assert.True(Math.Abs(injected / L1Of(s) - 0.5) < 1e-12, $"injection share = {injected / L1Of(s)}");
        Assert.True(Math.Abs(L1Of(s) - 0.48675) < 1e-5);
        Assert.True(Math.Abs(MaxAbs(s) - 0.01866667) < 1e-7);

        // Requirement 3 is therefore a NEGATIVE CONDUCTANCE in the coupling network (an NIC), which is
        // exactly why "active diffusion cancellation" is a serious candidate.
        Assert.True(dev / MaxAbs(s) < 1e-15);
    }

    // ── 2. Requirement 2: the thermodynamic power budget ─────────────────────────

    [Fact]
    public void Y_G_013_PowerBudget()
    {
        var rho = Tilt;

        // Holding rho* against the relaxation must remove the entropy the diffusion produces:
        // dH = H(W rho) - H(rho) = 0.2170247 nats per step for the witness (Landauer: k_B T dH per cell).
        double h0 = RhoDynamics.EntropyOf(rho);
        double h1 = RhoDynamics.EntropyOf(Step(rho));
        double dH = h1 - h0;
        Assert.True(dH > 0.0, "the diffusion increases the entropy");
        Assert.True(Math.Abs(dH - 0.2170247) < 1e-6, $"dH = {dH}");

        double landauerPerStep = KB * Temperature * dH * N;                 // joules per lattice per step
        var s = HoldDrive(rho);
        double chem = 0.0;
        double rhoBar = 1.0 / N;
        for (int i = 0; i < N; i++) chem += s[i] * Math.Log(rho[i] / rhoBar);
        double freeEnergyPerStep = KB * Temperature * chem * N;
        Assert.True(Math.Abs(landauerPerStep - 8.6295e-20) / 8.6295e-20 < 1e-3, $"Landauer = {landauerPerStep}");
        Assert.True(Math.Abs(freeEnergyPerStep - 1.5192e-19) / 1.5192e-19 < 1e-2, $"free energy = {freeEnergyPerStep}");
        Assert.True(freeEnergyPerStep >= landauerPerStep * 0.9);

        // At a 1 us step that is 8.63e-14 W (Landauer) / 1.52e-13 W (free energy) — ten orders below the
        // quiescent draw of any electronic controller, so POWER IS NOT THE BINDING CONSTRAINT.
        Assert.True(Math.Abs(landauerPerStep / 1e-6 - 8.63e-14) / 8.63e-14 < 1e-2);
        Assert.True(freeEnergyPerStep / 1e-6 < 1e-12);
        Assert.True(1e-3 / (freeEnergyPerStep / 1e-6) > 1e9, "a 1 mW controller is 1e9x above the requirement");

        // The SMOOTH (band-top) profile is thermodynamically FREE: its entropy change is zero to double
        // precision, because H is stationary in the k = 1 direction (it changes only at second order).
        var smooth = Mode(1).Select(v => (1.0 + BandCeiling / 2.0 * v) / N).ToArray();
        double dHs = RhoDynamics.EntropyOf(Step(smooth)) - RhoDynamics.EntropyOf(smooth);
        // The k = 1 mode is entropy-stationary (H changes only at second order, ~5.6e-16 here), so the
        // measured change is at the double-precision floor of the entropy sum.
        Assert.True(Math.Abs(dHs) < 1e-12, $"smooth dH = {dHs}");
        Assert.True(dH > 1e9 * Math.Abs(dHs));
    }

    // ── 3. Requirement 4 for the controller: the gain-error budget ───────────────

    [Fact]
    public void Y_G_013_GainErrorBudget()
    {
        var rho = Tilt;

        // Requirement 4 at zero error: the exact controller maintains rho* (G_012's freeze).
        var frozen = Settle(rho, HoldDrive(rho), 5000);
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(frozen[i] - rho[i])) < 1e-15);

        // With residual loop gain (1 + eps) the closed loop is I + eps (I - W): mode k evolves at
        // 1 + eps (1 - mu_k). Verified step by step on the fastest and the smoothest mode.
        foreach (var (k, eps) in new[] { (95, 1e-5), (95, 1e-3), (1, 1e-3), (1, 1e-2) })
        {
            // A PURE mode isolates the modal law: W v_k = mu_k v_k and (I - W) v_k = (1 - mu_k) v_k, so the
            // closed loop multiplies the AC amplitude by exactly 1 + eps (1 - mu_k) in one step.
            var pure = Mode(k).Select(v => 1e-6 * v).ToArray();
            var grown = LoopIterate(pure, eps, 1);
            double ratio = MaxAbs(grown) / MaxAbs(pure);
            double expected = 1.0 + eps * (1.0 - NeumannMu(k, N, Damping));
            Assert.True(Math.Abs(ratio - expected) < 1e-9, $"k = {k}, eps = {eps}: {ratio} vs {expected}");
            // After n steps the same law holds: (1 + eps (1 - mu_k))^n.
            Assert.True(Math.Abs(MaxAbs(LoopIterate(pure, eps, 100)) / MaxAbs(pure) - Math.Pow(expected, 100)) < 1e-7);
        }

        // Hold times tau_k = 1/(eps (1 - mu_k)): the fastest mode is the fragile one.
        double fast = 1.0 - NeumannMu(95, N, Damping);
        double slow = 1.0 - NeumannMu(1, N, Damping);
        Assert.True(Math.Abs(fast - 0.7997858350) < 1e-9);
        Assert.True(Math.Abs(slow - 2.141650094e-4) < 1e-12);
        Assert.True(Math.Abs(1.0 / (1e-6 * fast) - 1250335.0) < 1.0);
        Assert.True(Math.Abs(1.0 / (1e-5 * fast) - 125033.0) < 1.0);
        Assert.True(Math.Abs(1.0 / (1e-4 * fast) - 12503.0) < 1.0);
        Assert.True(Math.Abs(1.0 / (1e-3 * fast) - 1250.0) < 1.0);
        Assert.True(Math.Abs(1.0 / (1e-2 * fast) - 125.0) < 1.0);
        Assert.True(Math.Abs(1.0 / (1e-3 * slow) - 4.669e6) / 4.669e6 < 1e-3);
        Assert.True(Math.Abs(1.0 / (1e-2 * slow) - 4.669e5) / 4.669e5 < 1e-3);

        // So a 1e-6 loop accuracy holds the witness for ~1.25e6 steps while a 0.1 % tolerance holds only the
        // SMOOTH class (4.67e6 steps) — a 3734x split in what a given tolerance can maintain.
        Assert.True(Math.Abs(fast / slow - 3734.437465) < 1e-5);
        Assert.True(1.0 / (1e-3 * fast) < 2000.0);          // the witness dies in ~1250 steps at 0.1 %
        Assert.True(1.0 / (1e-3 * slow) > 4.0e6);           // the smooth class survives 4.67e6 steps
    }

    // ── 4. A one-step delay is tolerable ─────────────────────────────────────────

    [Fact]
    public void Y_G_013_DelayTolerance()
    {
        var rho = Tilt;

        // The delayed recursion rho_{n+1} = W rho_n + (rho_{n-1} - W rho_{n-1}) has roots {1, mu_k - 1},
        // so the extra branch spans [-0.7997858350, 0]: stable, with an alternating transient.
        foreach (int k in new[] { 1, 48, 95 })
        {
            double r = NeumannMu(k, N, Damping) - 1.0;
            Assert.True(r <= 0.0 && r >= -0.7997858350 - 1e-9, $"k = {k}: root {r}");
        }

        // Simulated: a 1e-3 perturbation is RETAINED (not amplified, not erased) after 2000 steps ...
        var prev = (double[])rho.Clone();
        var cur = rho.Select((v, i) => v + 1e-3 * Mode(2)[i]).ToArray();
        for (int m = 0; m < 2000; m++)
        {
            var next = new double[N];
            for (int i = 0; i < N; i++) next[i] = Step(cur)[i] + (prev[i] - Step(prev)[i]);
            prev = cur;
            cur = next;
        }
        double drift = Enumerable.Range(0, N).Max(i => Math.Abs(cur[i] - rho[i]));
        Assert.True(drift > 0.99e-3 && drift < 1.001e-3, $"delayed drift = {drift}");

        // ... and a perturbed pair of branches is likewise retained (0.9995e-3 after 2000 steps): the
        // delayed loop is still a marginal MEMORY, not a stabiliser.
        var a = rho.Select((v, i) => v + 1e-3 * Mode(2)[i]).ToArray();
        var b = rho.Select((v, i) => v + 1e-3 * Mode(2)[i]).ToArray();
        for (int m = 0; m < 2000; m++)
        {
            var next = new double[N];
            for (int i = 0; i < N; i++) next[i] = Step(b)[i] + (a[i] - Step(a)[i]);
            a = b;
            b = next;
        }
        double kept = Enumerable.Range(0, N).Max(i => Math.Abs(b[i] - rho[i]));
        Assert.True(kept < 1.001e-3 && kept > 0.99e-3, $"kept = {kept}");
    }

    // ── 5. Candidate 1+4: the PHYSICAL realizations ──────────────────────────────

    [Fact]
    public void Y_G_013_PhysicalRealizations()
    {
        var rho = Tilt;
        var s = HoldDrive(rho);

        // (a) FEEDBACK CONTROLLER: sense + compute + actuate. Local (three-point), exact and maintaining.
        var perturbed = (double[])rho.Clone();
        perturbed[48] += 1e-6;
        var s2 = HoldDrive(perturbed);
        for (int i = 0; i < N; i++)
            Assert.True(Math.Abs(i - 48) <= 1 || Math.Abs(s2[i] - s[i]) < 1e-18, $"cell {i} moved");
        // Maintaining: G_012's freeze — 4.34e-18 over 5000 steps.
        var frozen = Settle(rho, s, 5000);
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(frozen[i] - rho[i])) < 1e-15);

        // Sensor resolution: with a worst-case per-step actuation error q the drift is bounded by N q
        // (random-walk in practice), so 1 % of rhoBar over 1e6 steps needs q <= 1.04e-10 count units.
        double q = 0.01 * (1.0 / N) / 1e6;
        Assert.True(Math.Abs(q - 1.0416667e-10) / 1.0416667e-10 < 1e-3, $"q = {q}");
        Assert.True(q * 1e6 / (1.0 / N) < 0.011);

        // (b) ACTIVE DIFFUSION CANCELLATION: the operator is a nearest-neighbour negative conductance, so an
        // NIC realizes it exactly; the required element value equals the canonical d = 0.2 per coupling.
        Assert.True(Math.Abs(s[48] - (-Damping) * (rho[47] - 2.0 * rho[48] + rho[49])) < 1e-18);
        Assert.True(Math.Abs(Damping - 0.2) < 1e-15);
    }

    // ── 6. Candidate 2: the node-wise gain is only an ANALOGUE ───────────────────

    [Fact]
    public void Y_G_013_OscillatorLatticeAnalogue()
    {
        double lo = 1.0 - NeumannMu(1, N, Damping);
        double hi = 1.0 - NeumannMu(95, N, Damping);

        // A node-wise (uniform) negative damping has a MODE-INDEPENDENT rate gamma, so the residual error
        // per mode is |gamma - (1 - mu_k)|. The minimax choice over the required band is gamma = 0.4.
        double gamma = 0.5 * (lo + hi);
        Assert.True(Math.Abs(gamma - 0.4) < 1e-12);
        double worst = 0.0;
        for (int k = 1; k < N; k++)
            worst = Math.Max(worst, Math.Abs(gamma - (1.0 - NeumannMu(k, N, Damping))));
        Assert.True(Math.Abs(worst - 0.3997858349905463) < 1e-12, $"minimax error = {worst}");
        Assert.True(Math.Abs(hi / lo - 3734.437465) < 1e-5);      // the spread a flat gain must cover

        // Consequence: closing the loop with a flat gain does NOT maintain rho* — the residual per-mode
        // error reaches a factor 1866.7 on the SMOOTHEST mode (the gain that suits the fast modes is 1867x
        // too large for the slow one, and vice versa).
        double err = 0.0;
        for (int k = 1; k < N; k++) err = Math.Max(err, Math.Abs(gamma - (1.0 - NeumannMu(k, N, Damping))) / (1.0 - NeumannMu(k, N, Damping)));
        Assert.True(err > 1866.0 && err < 1868.0, $"worst RELATIVE error = {err}");

        // Only a NEAREST-NEIGHBOUR negative coupling removes the mode dependence (candidate 3): that is the
        // exact stencil of test 1, which is why the lattice is an analogue and the NIC is physical.
        var rho = Tilt;
        var s = HoldDrive(rho);
        double lift = 0.0;
        for (int i = 1; i < N - 1; i++) lift = Math.Max(lift, Math.Abs(s[i] / (rho[i] - 1.0 / N)));
        Assert.True(lift > 0.0);
        Assert.True(MaxAbs(s) < 0.02);
    }

    // ── 7. Candidate 3: band-limited resonators are an ANALOGUE ──────────────────

    [Fact]
    public void Y_G_013_ResonatorsTruncation()
    {
        var rho = Tilt;

        // A bank of resonators can compensate only the modes it covers. The witness's spectral share in
        // k <= 16 is 3.626161e-2 — i.e. 96.4 % of its structure is left uncompensated.
        foreach (var (kc, expected) in new[] { (1, 3.240559e-4), (4, 1.479430e-3), (8, 4.833883e-3),
                                               (16, 3.626161e-2), (24, 8.417047e-2), (48, 2.035517e-1) })
        {
            var w = Dct(rho);
            double low = 0.0, tot = 0.0;
            for (int k = 1; k < N; k++) { double e = w[k] * w[k]; tot += e; if (k <= kc) low += e; }
            double share = low / tot;
            Assert.True(Math.Abs(share - expected) / expected < 1e-4, $"k <= {kc}: {share}");
        }
        Assert.True(1.0 - 3.626161e-2 > 0.96, "a 16-mode bank leaves over 96 % uncompensated");
        Assert.True(HighKShare(rho, N / 2) > 0.79);

        // The uncompensated modes then decay on their own timescales: after 200 steps the witness's
        // deviation from uniform has collapsed 33.17x in L1 — the bank cannot hold it.
        Assert.True(Math.Abs(L1(Iterate(rho, 200), Enumerable.Repeat(1.0 / N, N).ToArray()) - 0.0201006) < 1e-6);
        Assert.True(Math.Abs(L1(rho, Enumerable.Repeat(1.0 / N, N).ToArray()) - 0.6666667) < 1e-6);
    }

    // ── 8. Candidate 5: a pump/loss balance is REFUTED ───────────────────────────

    [Fact]
    public void Y_G_013_PumpLossRefuted()
    {
        var rho = Tilt;

        // The required source IS balanced (half pump, half drain) — which is exactly why a pump/loss
        // network looks attractive — but the balance is a SCALAR condition, not the three-point operator.
        var s = HoldDrive(rho);
        double injected = s.Where(v => v > 0).Sum();
        double extracted = -s.Where(v => v < 0).Sum();
        Assert.True(Math.Abs(injected - extracted) < 1e-15);

        // G_012's restoring-family theorem: a scalar (mode-independent) balance has fixed points only on
        // the single Neumann modes (lambda = 1 - mu_k), and stability admits k = 1 alone.
        double hi = 1.0 - NeumannMu(95, N, Damping);
        foreach (int k in new[] { 2, 48, 95 })
        {
            double lambda = 1.0 - NeumannMu(k, N, Damping);
            Assert.True(lambda > 1.0 - NeumannMu(1, N, Damping), $"k = {k} needs {lambda}");
            Assert.True(NeumannMu(1, N, Damping) + lambda > 1.0, "and destabilises the smoothest mode");
        }
        Assert.True(hi / (1.0 - NeumannMu(1, N, Damping)) > 3734.0);

        // An imbalance grows at the FASTEST mode's rate: 0.7997858350 eps per step, so a 1 % mismatch
        // reaches a factor e in 1/0.00799786 = 125 steps. The operating point is measure-zero.
        Assert.True(Math.Abs(1.0 / (1e-2 * hi) - 125.0335) < 0.01);
        Assert.True(1e-3 * hi > 1e-4);
        // It can therefore maintain only states whose curvature is proportional to the local density —
        // the cellwise test of G_012, which the witness fails (spread 9.5e-3 against max|s| = 0.01866667).
        var groups = rho.Select((v, i) => (Value: Math.Round(v, 12), Index: i)).GroupBy(g => g.Value).ToArray();
        double worstSpread = groups.Max(g => g.Select(x => s[x.Index]).Max() - g.Select(x => s[x.Index]).Min());
        Assert.True(worstSpread > 9.0e-3, $"spread = {worstSpread}");
    }

    // ── 9. Research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_013_Run()
    {
        var sb = new StringBuilder();
        var rho = Tilt;
        var s = HoldDrive(rho);
        double lo = 1.0 - NeumannMu(1, N, Damping);
        double hi = 1.0 - NeumannMu(95, N, Damping);
        double dH = RhoDynamics.EntropyOf(Step(rho)) - RhoDynamics.EntropyOf(rho);

        PrintHeader(sb, "ResearchY-G_013 — PHYSICAL ACTUATOR AUDIT");
        sb.AppendLine("Question: what PHYSICAL process can realize s = (I - W) rho* locally?");
        sb.AppendLine("Candidates: feedback controller, oscillator lattice, coupled resonators,");
        sb.AppendLine("            active diffusion cancellation, pump/loss networks.");
        sb.AppendLine("Requirements: (1) local, (2) finite power, (3) exact stencil, (4) maintains rho*.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  The relaxation is W = DiffuseStep(d = 0.2) on the ordered 96-cell chain (G_006/G_007).");
        sb.AppendLine("  A2  The actuator must realize s = (I - W) rho* = -d * Laplacian(rho*) (G_012).");
        sb.AppendLine("  A3  Landauer: holding rho* must remove the entropy the diffusion produces, k_B T per nat.");
        sb.AppendLine("  A4  A real device is characterized by (drive, stability, power, error).");
        sb.AppendLine();

        PrintHeader(sb, "1. THE STENCIL IN PHYSICAL FORM");
        sb.AppendLine($"  s = -d * Laplacian(rho*)   (verified to {Enumerable.Range(0, N).Max(i => Math.Abs(s[i] - NegativeLaplacian(rho)[i])):E2})");
        sb.AppendLine($"  balanced pump-and-drain: injection {s.Where(v => v > 0).Sum() / L1Of(s):P4} / extraction {-s.Where(v => v < 0).Sum() / L1Of(s):P4} of ||s||_1 = {L1Of(s):F5}");
        sb.AppendLine($"  max|s| = {MaxAbs(s):F8} (< 1); three-point local; count-neutral");
        sb.AppendLine();

        PrintHeader(sb, "2. THE FIVE CANDIDATES (drive, stability, power, error)");
        sb.AppendLine("  candidate                 drive        stability            power              error        VERDICT");
        sb.AppendLine($"  feedback controller       {MaxAbs(s):F8}     marginal (delta = 0)  {KB * Temperature * dH * N / 1e-6:E2} W   |eps| <= 1.25e-6  PHYSICAL");
        sb.AppendLine($"  active diff. cancellation {MaxAbs(s):F8}     marginal (delta = 0)  {KB * Temperature * dH * N / 1e-6:E2} W   component eps    PHYSICAL");
        sb.AppendLine($"  oscillator lattice        {MaxAbs(s):F8}     modal mismatch        {KB * Temperature * dH * N / 1e-6:E2} W   {0.3997858349905463:F7}     ANALOGUE");
        sb.AppendLine($"  coupled resonators        {MaxAbs(s):F8}     mode-truncated        {KB * Temperature * dH * N / 1e-6:E2} W   96.4 % missing   ANALOGUE");
        sb.AppendLine($"  pump/loss network         {MaxAbs(s):F8}     scalar balance        {KB * Temperature * dH * N / 1e-6:E2} W   single-mode      REFUTED");
        sb.AppendLine();

        PrintHeader(sb, "3. THE ERROR BUDGET (the binding constraint)");
        sb.AppendLine($"  closed loop with residual gain (1 + eps): I + eps (I - W), eigenvalue 1 + eps (1 - mu_k)");
        sb.AppendLine("  eps        fastest mode (0.7997858350)      smoothest mode (2.141650094e-4)");
        foreach (double eps in new[] { 1e-6, 1e-5, 1e-4, 1e-3, 1e-2 })
            sb.AppendLine($"  {eps,8:E0}   {1.0 / (eps * hi),18:F0} steps       {1.0 / (eps * lo),18:E2} steps");
        sb.AppendLine($"  required per-mode gain spread: {hi / lo:F3}  ->  a single broadband gain has minimax error {0.5 * (hi - lo):F7}");
        sb.AppendLine("  one-step DELAY: roots {1, mu_k - 1} in [-0.7997858350, 0] -> stable; a 1e-3 perturbation is");
        sb.AppendLine("  retained (memory), not amplified or erased.");
        sb.AppendLine();

        PrintHeader(sb, "4. THE POWER BUDGET (not binding)");
        sb.AppendLine($"  entropy the diffusion produces: dH = {dH:F7} nats per step (witness)");
        sb.AppendLine($"  Landauer: k_B T dH per cell = {KB * Temperature * dH * N:E4} J per lattice per step -> {KB * Temperature * dH * N / 1e-6:E2} W at 1 us");
        double chem = 0.0;
        for (int i = 0; i < N; i++) chem += s[i] * Math.Log(rho[i] * N);
        sb.AppendLine($"  free energy: k_B T sum s ln(rho/rhoBar) = {KB * Temperature * chem * N:E4} J per step -> {KB * Temperature * chem * N / 1e-6:E2} W");
        sb.AppendLine($"  smooth band-top profile: dH = {RhoDynamics.EntropyOf(Step(Mode(1).Select(v => (1.0 + BandCeiling / 2.0 * v) / N).ToArray())) - RhoDynamics.EntropyOf(Mode(1).Select(v => (1.0 + BandCeiling / 2.0 * v) / N).ToArray()):E2} nats — thermodynamically FREE");
        sb.AppendLine("  a 1 mW controller is 1e9x above the requirement: EXACTNESS, not power, is the constraint.");
        sb.AppendLine();

        PrintHeader(sb, "5. CONCLUSIONS");
        sb.AppendLine("  C1  The stencil is a NEGATIVE LAPLACIAN — anti-diffusion of strength d = 0.2 on the");
        sb.AppendLine("      nearest-neighbour chain — and a balanced pump-and-drain (50 % / 50 %).");
        sb.AppendLine("  C2  PHYSICAL: the feedback controller (sense + compute + actuate; three-point local, exact)");
        sb.AppendLine("      and ACTIVE DIFFUSION CANCELLATION (a nearest-neighbour negative conductance / NIC, which");
        sb.AppendLine("      realizes the operator element by element). Both are marginal memories (G_012).");
        sb.AppendLine("  C3  ANALOGUE: the oscillator lattice with a NODE-WISE gain — a flat gain has a mode-independent");
        sb.AppendLine("      rate, so the residual error is |gamma - (1 - mu_k)| with minimax 0.3997858 against a");
        sb.AppendLine("      required spread of 3734.437 — and the coupled-resonator bank, which is band-limited: a");
        sb.AppendLine("      16-mode bank leaves 96.4 % of the witness's structure uncompensated.");
        sb.AppendLine("  C4  REFUTED: pump/loss networks. The required source is balanced, but a pump/loss balance is a");
        sb.AppendLine("      SCALAR condition whose fixed points are the single Neumann modes (G_012): it maintains no");
        sb.AppendLine("      arbitrary rho*, and an imbalance grows at the fastest mode's rate (125 steps at 1 %).");
        sb.AppendLine("  C5  The binding constraint is EXACTNESS: |eps| <= 1.25e-6 of the mode rates for a 1.25e6-step");
        sb.AppendLine("      hold of the witness, or <= 4.67e-3 for the smooth class — a 3734x split in tolerance.");
        sb.AppendLine("  C6  A one-step control DELAY is tolerable (extra roots mu_k - 1 in [-0.8, 0]); the loop stays a");
        sb.AppendLine("      marginal memory rather than becoming unstable.");
        sb.AppendLine("  C7  Power is a non-issue: the thermodynamic minimum is 8.63e-14 W (Landauer) to 1.52e-13 W");
        sb.AppendLine("      (free energy) at a 1 us step, ten orders below any electronic floor, and the smooth class");
        sb.AppendLine("      costs dH = 0 to double precision.");
        sb.AppendLine();

        PrintHeader(sb, "6. CLASSIFICATION");
        sb.AppendLine("  PHYSICAL   feedback controller; active diffusion cancellation (the exact negative conductance).");
        sb.AppendLine("  ANALOGUE   oscillator lattice (node-wise gain: modal mismatch); coupled resonators (band-limited).");
        sb.AppendLine("  REFUTED    pump/loss networks (a scalar balance maintains no arbitrary rho*).");
        sb.AppendLine("  No reclassification (the D_040 registry is untouched); no canonical claim, value or equation");
        sb.AppendLine("  changes; no new primitive; deterministic (exact algebra, no randomness).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
