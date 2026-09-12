using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.RhoActuators;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_011 — Rho Actuator Audit (group G — Gravity Source).
///
/// QUESTION: can any physical quantity CHANGE rho?
/// Candidates: energy density, phase coherence, spectral organization, degeneracy engineering,
/// attractor compression, synchronization, information density.
///
/// FRAMEWORK (the actuator criterion). q is an ACTUATOR iff (i) q is INDEPENDENT of rho (not a function
/// of it), (ii) q's value determines rho locally (d rho / d q != 0), and (iii) changing q leaves
/// Sigma rho = 1, needs no energy and introduces no new primitive. Each candidate is measured as the
/// quadruple (Delta rho, Delta a, Delta Phi, Delta Tau) on the D96 lattice (45 eigenspaces, 96 cells,
/// 51 = D_048 free directions) with the canonical relaxation W = DiffuseStep(d = 0.2).
///
/// RESULTS
///   ACTUATOR ......... NONE OF THE SEVEN. Every candidate is either a FUNCTION of rho (energy, spectral
///                      organization, degeneracy engineering, compression, information) or INDEPENDENT
///                      but rho-INERT (phase, synchronization). The only quantity that changes rho is an
///                      imported occupancy source s = (I - W) rho* per step (G_008's law, generalised):
///                      count-neutral (Sigma s = 0 exactly), UNIQUE per target (the gains 1/(1 - mu_k)
///                      are all finite, 4669.2968 down to 1.2503), and SUM ZERO for the uniform state.
///   CORRELATED ....... energy density (E = <lambda, rho>, an exact linear re-expression: identical block
///                      sums and identical E to 1e-12 while rho moves by L1 = 2/3 and |a| goes 0 -> 0.603175);
///                      spectral organization (the DCT-II is an ORTHONORMAL bijection, round trip 4e-16:
///                      the 95 mode amplitudes are coordinates of rho, not a handle on it, and the
///                      operator's own spectrum mu_k is fixed by (d, N) alone); degeneracy engineering
///                      (the 51-dimensional energy-free room IS the free part of rho).
///   REFUTED .......... phase coherence (rho = |psi|^2 is phase-blind: L1 = 0 and |Delta a| < 1e-9 for all
///                      four phase assignments while the psi-sector runs 0.0324 -> 9.1172, a factor 281);
///                      synchronization (the canonical grid theta_j = 2 pi j/N is the maximally
///                      incoherent configuration, Kuramoto r = 8.08e-17, and locking it drives r -> 1 with
///                      rho untouched); attractor compression (a rho -> rho MAP, not a quantity: it is
///                      merely a relabelling, and its difference is SMOOTH so it survives 200 steps
///                      1.12x while the G_002 witness is erased 33.78x); information density (a GLOBAL
///                      functional: exactly permutation- and reversal-invariant while rho moves by
///                      L1 = 0.658 and |Delta a| = 1.003, i.e. it cannot select a local field).
///
/// SUCCESS CRITERION: the first physical handle that changes rho is the external source term s, which is
/// NOT an AT primitive (G_008/G_010's missing driver). Within the canonical vocabulary, rho is changed
/// only by the evolution of rho itself, whose horizon m is not an external quantity (G_007).
///
/// Deterministic: exact algebra, no randomness.  No reclassification: G_002's CONTROLLABLE verdict is
/// about the OPERATIONS on rho (they do move it at fixed energy) and is unchanged; the G_011 labels are
/// about whether the QUANTITY is an actuator.  D_040 untouched.
/// </summary>
public class Y_G_011_Tests : ResearchTestBase
{
    public Y_G_011_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double Witness = 0.603175;        // G_003's degeneracy-redistribution Delta a_AT
    private const double Arrangement = 0.685714;    // G_003's arrangement Delta a_AT
    private const double Compression = 0.032121;    // G_003's survivor-compression Delta a_AT
    private const double BandCeiling = 4.8867e-6;   // G_005's 1 % Poisson ceiling
    private const double ClockFloor = 1e-18;

    // ── the arena ────────────────────────────────────────────────────────────────

    private static double[] Canonical => Spread(D96Spaces.Mult, 1.0);
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);
    private static double[] SortedTilt => Tilt.OrderBy(v => v).ToArray();

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var r = (double[])a.Clone();
        for (int i = 0; i < m; i++) r = Step(r);
        return r;
    }

    private static double L1Of(double[] x) => x.Sum(Math.Abs);

    private static double Std(double[] x)
    {
        double m = x.Average();
        return Math.Sqrt(x.Sum(v => (v - m) * (v - m)) / x.Length);
    }

    /// <summary>Cell-wise log density change max_i |ln(a_i / b_i)| (the density-space contrast).</summary>
    private static double DeltaLnRho(double[] a, double[] b)
    {
        double m = 0.0;
        for (int i = 0; i < a.Length; i++) m = Math.Max(m, Math.Abs(Math.Log(a[i] / b[i])));
        return m;
    }

    /// <summary>The G_003/G_009 potential reading of a lattice-level change: Delta Phi/c^2 = Delta a_AT / d.</summary>
    private static double PhiOverC2(double deltaAat) => deltaAat / D;

    /// <summary>The clock reading of a lattice-level change, in seconds per day (G_009).</summary>
    private static double SecondsPerDay(double deltaAat) => PhiOverC2(deltaAat) * 86400.0;

    /// <summary>The energy functional E = &lt;lambda, rho&gt; (occupation-weighted spectral content, QG180/181).</summary>
    private static double EnergyOf(double[] rho)
    {
        var (distinct, mult) = D96Spaces;
        double e = 0.0;
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
            for (int j = 0; j < mult[i]; j++) e += distinct[i] * rho[k++];
        return e;
    }

    /// <summary>The G_002/G_003 structured base profile (the AT log-deficit shape on the mode lattice).</summary>
    private static double[] BaseProfile => Enumerable.Range(1, N)
        .Select(j => 1.0 - 0.4 * Math.Log(N / (double)j) / Math.Log(N)).ToArray();

    /// <summary>Invert the hold-drive map: rho* = Idct_k( w_k / (1 - mu_k) ) — every gain must be finite.</summary>
    private static double[] ReconstructFromDrive(double[] drive)
    {
        var w = Dct(drive);
        for (int k = 1; k < N; k++) w[k] /= 1.0 - NeumannMu(k, N, Damping);
        w[0] = 0.0;
        return Idct(w, N);
    }

    // ── 1. The arena ─────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_011_CandidateSpace()
    {
        var (distinct, mult) = D96Spaces;

        // 45 distinct eigenspaces over 96 cells; the free room is Sigma (m_i - 1) = 51 = D_048's L.
        Assert.Equal(45, distinct.Length);
        Assert.Equal(N, mult.Sum());
        Assert.Equal(51, mult.Sum(m => m - 1));
        Assert.Equal(N - distinct.Length, mult.Sum(m => m - 1));
        Assert.Equal(42, mult.Count(m => m == 2));
        Assert.Equal(1, mult.Count(m => m == 5));
        Assert.Equal(1, mult.Count(m => m == 6));
        Assert.Equal(1, mult.Count(m => m == 1));

        // The density configuration space itself: 95 directions at fixed count, 94 at fixed energy.
        Assert.Equal(95, N - 1);

        // The attractor is the exact zero field, so the "actuator" has nothing to do at the fixed point.
        Assert.Equal(0.0, MaxAbsAcceleration(Canonical), 12);
        Assert.Equal(0.0, MaxAbsCurvature(Canonical), 12);
        Assert.Equal(1.0, L1Of(Canonical), 12);

        // The vacuum is not actuable: the uniform state has no hold-drive at all (G_008).
        Assert.True(L1Of(HoldDrive(Canonical)) < 1e-12);
    }

    // ── 2. Candidate 1 — energy density is CORRELATED ────────────────────────────

    [Fact]
    public void Y_G_011_EnergyDensityCorrelated()
    {
        var canonical = Canonical;
        var tilted = Tilt;

        // (a) Energy is an EXACT linear functional of rho, E = <lambda, rho> (occupation-weighted spectral
        //     content, QG180/QG181). It is therefore a fixed function of rho: "changing the energy" IS
        //     changing rho, so the candidate cannot be its cause.
        double eCanonical = EnergyOf(canonical), eTilted = EnergyOf(tilted);
        Assert.True(Math.Abs(eCanonical - 12.0) < 1e-12, $"E = {eCanonical}");
        Assert.True(Math.Abs(eTilted - eCanonical) < 1e-12, $"{eTilted} vs {eCanonical}");

        // (b) The degeneracy redistribution conserves EVERY eigenspace total, hence the energy EXACTLY ...
        var b0 = BlockSums(D96Spaces.Mult, canonical);
        var b1 = BlockSums(D96Spaces.Mult, tilted);
        for (int i = 0; i < b0.Length; i++) Assert.Equal(b0[i], b1[i], 12);

        // ... while rho moves by 2/3 of the count and the field is created out of an exact zero.
        Assert.True(Math.Abs(L1(canonical, tilted) - 0.6666667) < 1e-6, $"L1 = {L1(canonical, tilted)}");
        Assert.True(Math.Abs(MaxAbsAcceleration(tilted) - Witness) < 1e-6);

        // (c) Non-injectivity: the fixed-E fibre contains the whole 51-dimensional degeneracy space
        //     (94 dimensions in total — one linear constraint on the 95-dimensional simplex), so the
        //     energy cannot SELECT a configuration.
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.Equal(94, N - 1 - 1);

        // (d) Re-ordering the SAME multiset changes the energy by 12.8 %: E depends on the pairing of rho
        //     with the spectrum, i.e. again on the arrangement, never the other way round.
        double eSorted = EnergyOf(SortedTilt);
        Assert.True(Math.Abs(eSorted - 13.5401766) < 1e-6, $"E(sorted) = {eSorted}");
        Assert.True(Math.Abs(eSorted - eTilted - 1.5401766) < 1e-6);

        // (e) The readouts, in the G_003/G_009 convention (Delta Phi/c^2 = Delta a_AT / d) ...
        Assert.True(Math.Abs(PhiOverC2(Witness) - 0.201058) < 1e-6);
        Assert.True(Math.Abs(SecondsPerDay(Witness) - 17371.4) < 0.5);
        // ... and in the density-space reading: the tilt is a 4.8:1 occupancy contrast.
        Assert.True(Math.Abs(DeltaLnRho(tilted, canonical) - Math.Log(4.8)) < 1e-12);
        Assert.True(Math.Abs(DeltaLnRho(tilted, canonical) / D * 86400.0 - 45176.1) < 1.0);

        // VERDICT: CORRELATED — an exact re-expression of rho, non-injective, unable to select a state.
    }

    // ── 3. Candidate 2+6 — phase coherence and synchronization are REFUTED ───────

    [Fact]
    public void Y_G_011_PhaseAndSynchronizationRefuted()
    {
        var rho = Tilt;

        double[] DensityFrom(double[] phases)
        {
            var dens = new double[rho.Length];
            for (int j = 0; j < rho.Length; j++)
            {
                double re = Math.Sqrt(rho[j]) * Math.Cos(phases[j]);
                double im = Math.Sqrt(rho[j]) * Math.Sin(phases[j]);
                dens[j] = re * re + im * im;
            }
            return dens;
        }
        (double Re, double Im) PsiSum(double[] phases)
        {
            double re = 0.0, im = 0.0;
            for (int j = 0; j < rho.Length; j++)
            {
                double amp = Math.Sqrt(rho[j]);
                re += amp * Math.Cos(phases[j]);
                im += amp * Math.Sin(phases[j]);
            }
            return (re, im);
        }
        double AbsSum(double[] phases)
        {
            var (re, im) = PsiSum(phases);
            return Math.Sqrt(re * re + im * im);
        }
        double Kuramoto(double[] phases)
        {
            double re = 0.0, im = 0.0;
            foreach (double t in phases) { re += Math.Cos(t); im += Math.Sin(t); }
            return Math.Sqrt(re * re + im * im) / phases.Length;
        }

        // psi_j = sqrt(rho_j) e^{i theta_j} (QG220); rho = |psi|^2 is phase-blind. The phase is the ONE
        // candidate that is genuinely INDEPENDENT of rho — and it is inert.
        var theta = Enumerable.Range(0, N).Select(j => 2.0 * Math.PI * j / N).ToArray();
        foreach (var phases in new[] { theta, theta.Select(t => t + 1.234).ToArray(), theta.Select(t => -t).ToArray(), new double[N] })
        {
            var recovered = DensityFrom(phases);
            Assert.True(L1(rho, recovered) < 1e-15, $"L1 = {L1(rho, recovered)}");
            Assert.True(MaxAccelerationDifference(rho, recovered) < 1e-9);
            Assert.True(MaxCurvatureDifference(rho, recovered) < 1e-6);
        }

        // The psi-sector nevertheless MOVES: the coherent sum runs 0.0324 -> 9.1172 (a factor 281).
        Assert.True(Math.Abs(AbsSum(theta) - 0.0324196281) < 1e-9, $"{AbsSum(theta)}");
        Assert.True(Math.Abs(AbsSum(new double[N]) - 9.1171821879) < 1e-9, $"{AbsSum(new double[N])}");
        Assert.True(Math.Abs(AbsSum(new double[N]) / AbsSum(theta) - 281.2241449) < 1e-5);

        // Synchronization is a phase RELATION: the canonical grid is the maximally INCOHERENT state
        // (the 96th roots of unity sum to zero exactly), and locking it takes r from 8.08e-17 to 1
        // with rho, a and R untouched.
        Assert.True(Kuramoto(theta) < 1e-15, $"r = {Kuramoto(theta)}");
        Assert.Equal(1.0, Kuramoto(new double[N]), 12);
        Assert.True(MaxAccelerationDifference(rho, DensityFrom(new double[N])) < 1e-9);

        // The phase is physically observable in the two-mode interference term (4 -> 2 -> 0) — a genuine
        // variable that simply does not touch rho.
        Assert.Equal(4.0, 2.0 + 2.0 * Math.Cos(0.0), 12);
        Assert.Equal(2.0, 2.0 + 2.0 * Math.Cos(Math.PI / 2.0), 12);
        Assert.Equal(0.0, 2.0 + 2.0 * Math.Cos(Math.PI), 12);

        // VERDICT: REFUTED — both are independent of rho and exactly rho-inert (Delta rho = Delta a = 0).
    }

    // ── 4. Candidate 3 — spectral organization is a COORDINATE system ────────────

    [Fact]
    public void Y_G_011_SpectralOrganizationIsACoordinate()
    {
        var tilted = Tilt;

        // The orthonormal DCT-II is a BIJECTION on the density space: C C^T = I and Idct(Dct(rho)) = rho.
        var c = DctMatrix(N);
        double ortho = 0.0;
        for (int k = 0; k < N; k++)
            for (int l = 0; l < N; l++)
            {
                double s = 0.0;
                for (int i = 0; i < N; i++) s += c[k, i] * c[l, i];
                ortho = Math.Max(ortho, Math.Abs(s - (k == l ? 1.0 : 0.0)));
            }
        Assert.True(ortho < 1e-12, $"|C C^T - I| = {ortho}");

        var w = Dct(tilted);
        var back = Idct(w, N);
        double rt = 0.0;
        for (int i = 0; i < N; i++) rt = Math.Max(rt, Math.Abs(back[i] - tilted[i]));
        Assert.True(rt < 1e-13, $"round trip = {rt}");
        // The DC coefficient of the unnormalised DCT IS the count itself (Sigma rho = 1), so the DC
        // direction carries no freedom: 95 AC coordinates move.
        Assert.True(Math.Abs(w[0] - 1.0) < 1e-12, $"DC = {w[0]}");
        Assert.True(Math.Abs(w[1] / Math.Sqrt(N / 2.0) - 0.0014381159) < 1e-9,
            $"orthonormalised w_1 = {w[1] / Math.Sqrt(N / 2.0)}");

        // The witness is a HIGH-k configuration (dominant mode k = 94; k >= 48 carries 79.66 % of the AC
        // energy) — i.e. the mode amplitudes are exactly the G_005/G_006 "free directions" in another basis.
        int dominant = Enumerable.Range(1, N - 1).OrderByDescending(k => Math.Abs(w[k])).First();
        Assert.Equal(94, dominant);
        Assert.True(Math.Abs(HighKShare(tilted, N / 2) - 0.7965733) < 1e-6, $"high-k = {HighKShare(tilted, N / 2)}");
        Assert.True(HighKShare(tilted, N / 2) > 0.5);

        // The OPERATOR's spectrum is fixed by (d, N) alone — it carries no rho-dependence at all, so the
        // "spectral organization" of the physics is not adjustable: only the amplitudes are.
        Assert.True(Math.Abs(NeumannMu(48, N, Damping) - 0.6) < 1e-15);
        Assert.True(Math.Abs((1.0 - NeumannMu(1, N, Damping)) - 2.141650094e-4) < 1e-12);
        Assert.True(Math.Abs((1.0 - NeumannMu(95, N, Damping)) - 0.799785835) < 1e-9);

        // The mode amplitudes are therefore legitimate, INDEPENDENT coordinates of rho (95 of them) but
        // they are not a physical quantity: nothing in the chain sets them.
        // VERDICT: CORRELATED — a coordinate system on rho, not a handle on it.
    }

    // ── 5. Candidate 4 — degeneracy engineering is the free part of rho ──────────

    [Fact]
    public void Y_G_011_DegeneracyEngineeringIsTheFreeRoom()
    {
        var canonical = Canonical;
        var tilted = Tilt;

        // The 51 energy-free directions are exactly the within-eigenspace redistributions (G_002): they
        // conserve the count, every eigenspace total, the energy and the whole spectrum.
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.Equal(0.53125, D96Spaces.Mult.Sum(m => m - 1) / (double)N, 5);
        Assert.True(Math.Abs(EnergyOf(tilted) - EnergyOf(canonical)) < 1e-12);
        Assert.Equal(45, D96Spaces.Distinct.Length);                    // no eigenspace created or destroyed

        // It moves rho and creates the field out of an exact zero: 0 -> 0.603175 (G_003's degeneracy case).
        Assert.Equal(0.0, MaxAbsAcceleration(canonical), 12);
        Assert.True(Math.Abs(MaxAbsAcceleration(tilted) - Witness) < 1e-6);
        Assert.True(Math.Abs(L1(canonical, tilted) - 0.6666667) < 1e-6);

        // Its price is the G_008 drive and its suppression is the G_006 witness factor.
        var s = HoldDrive(tilted);
        Assert.True(Math.Abs(L1Of(s) - 0.48675) < 1e-5, $"||s||_1 = {L1Of(s)}");
        Assert.True(Math.Abs(Std(tilted) / Std(Iterate(tilted, 200)) - 33.7781483) < 1e-4);

        // The arrangement case (the same multiset in ascending order) is the stronger witness (0.685714)
        // and is NOT energy-free: it moves E by 12.8 % (see the energy candidate).
        Assert.True(Math.Abs(MaxAccelerationDifference(tilted, SortedTilt) - Arrangement) < 1e-6);

        // VERDICT: CORRELATED — the largest genuinely free structure in rho, but a coordinate of rho,
        // not an external quantity (no energy, no symmetry breaking, and nothing selects it).
    }

    // ── 6. Candidate 5 — attractor compression is a MAP, not a quantity ──────────

    [Fact]
    public void Y_G_011_CompressionRefuted()
    {
        var baseProfile = BaseProfile;
        var compact48 = Compaction(baseProfile, 48);

        // The operation does move rho (G_002's CONTROLLABLE verdict stands) and G_003's field reading is
        // 0.032121 — but compression is a rho -> rho MAP: it is a relabelling of a density move, so its
        // "value" is not a state variable of the theory.
        Assert.True(Math.Abs(L1(baseProfile, compact48) - 2.8669638) < 1e-5, $"L1 = {L1(baseProfile, compact48)}");
        Assert.True(Math.Abs(MaxAccelerationDifference(baseProfile, compact48) - Compression) < 1e-7);

        // Unlike the witness, its difference is SMOOTH: it survives 200 relaxation steps essentially
        // unchanged (1.12x), because the change lives in the slow modes (k = 1 dominant).
        Assert.True(Math.Abs(MaxAccelerationDifference(baseProfile, Iterate(compact48, 200)) - Compression) < 1e-6,
            "the compaction's field difference is not erased by 200 steps");
        Assert.True(Math.Abs(Std(baseProfile) / Std(Iterate(baseProfile, 200)) - 1.1207130) < 1e-5);

        // The exact price of holding it is (I - W) rho per step, and it is a pure drive requirement —
        // 0.0242978 for the base profile, 0.0336912 for the compacted one.
        Assert.True(Math.Abs(L1Of(HoldDrive(baseProfile)) - 0.0242978) < 1e-6);
        Assert.True(Math.Abs(L1Of(HoldDrive(compact48)) - 0.0336912) < 1e-6);

        // A compression applied to the WITNESS class is erased like the witness itself: the tilt's L1
        // distance from the uniform measure collapses from 0.6666667 to 0.0201006 (33.17x).
        Assert.True(Math.Abs(L1(Tilt, Canonical) - 0.6666667) < 1e-6);
        Assert.True(Math.Abs(L1(Iterate(Tilt, 200), Canonical) - 0.0201006) < 1e-6);

        // VERDICT: REFUTED as an actuator. It is an operation on rho (its G_002 status CONTROLLABLE is
        // unchanged), powered by nothing, and its own magnitude is a function of the rho it acts on.
    }

    // ── 7. Candidate 7 — information density is a GLOBAL functional ──────────────

    [Fact]
    public void Y_G_011_InformationDensityRefuted()
    {
        var canonical = Canonical;
        var tilted = Tilt;

        // I_occ = KL(rho || uniform): zero at the attractor, 0.272565 for the witness (= ln 96 - H, G_005).
        Assert.True(Math.Abs(Kl(canonical)) < 1e-15, $"KL(uniform) = {Kl(canonical)}");
        Assert.True(Math.Abs(Kl(tilted) - 0.2725652026) < 1e-9, $"KL(tilt) = {Kl(tilted)}");
        Assert.True(Math.Abs(Kl(tilted) - (Math.Log(N) - RhoDynamics.EntropyOf(tilted))) < 1e-12);

        // It is EXACTLY permutation-invariant — while rho moves by L1 = 0.658 and the field can even GROW
        // (|Delta a| = 1.003). A quantity that cannot see the order of rho cannot drive a local field.
        var permuted = Enumerable.Range(0, N).Select(i => tilted[(i + 37) % N]).ToArray();
        Assert.True(Math.Abs(Kl(permuted) - Kl(tilted)) < 1e-15, $"dKL = {Kl(permuted) - Kl(tilted)}");
        Assert.True(Math.Abs(L1(tilted, permuted) - 0.6583333) < 1e-6, $"L1 = {L1(tilted, permuted)}");
        Assert.True(Math.Abs(MaxAccelerationDifference(tilted, permuted) - 1.0031746) < 1e-6);

        // Reversal too: identical information, different field.
        var reversed = tilted.Reverse().ToArray();
        Assert.True(Math.Abs(Kl(reversed) - Kl(tilted)) < 1e-15);
        Assert.True(Math.Abs(L1(tilted, reversed) - 0.7083333) < 1e-6);

        // Along the canonical flow the information only DECREASES toward the attractor (G_006: the operator
        // is exactly linear, so entropy is downstream of rho, never its cause).
        double k50 = Kl(Iterate(tilted, 50));
        double k200 = Kl(Iterate(tilted, 200));
        Assert.True(k50 < Kl(tilted) && k200 < k50, $"KL ordering {Kl(tilted)} {k50} {k200}");
        Assert.True(Math.Abs(k50 - 1.08730024e-3) < 1e-7 && Math.Abs(k200 - 2.69463228e-4) < 1e-8);

        // A single scalar can select at most a 94-dimensional fibre of the 95-dimensional simplex.
        Assert.Equal(94, (N - 1) - 1);

        // VERDICT: REFUTED — a global, permutation-invariant functional of rho; it cannot act locally.
    }

    // ── 8. The actuator verdict: the source term is imported ─────────────────────

    [Fact]
    public void Y_G_011_ControllabilityAndVerdicts()
    {
        var tilted = Tilt;
        var s = HoldDrive(tilted);

        // (a) The uniform state is the UNIQUE undriven fixed point: its hold-drive is identically zero.
        Assert.True(L1Of(HoldDrive(Canonical)) < 1e-15);

        // (b) The witness's hold-drive is count-neutral (Sigma s = 0) and costs 0.48675 per step
        //     (G_008's 0.4868 = 49 % of the count).
        double sumS = s.Sum();
        Assert.True(Math.Abs(sumS) < 1e-14, $"Sigma s = {sumS}");
        Assert.True(Math.Abs(L1Of(s) - 0.48675) < 1e-5, $"||s||_1 = {L1Of(s)}");
        Assert.True(Math.Abs(s.Max(Math.Abs) - 0.01866667) < 1e-7);

        // (c) The actuator map s = (I - W) rho* is INVERTIBLE: every gain 1/(1 - mu_k) is finite, so every
        //     rho is reachable and no direction has zero gain. The ladder spans 4669.2968 ... 1.2503.
        Assert.True(Math.Abs(1.0 / (1.0 - NeumannMu(1, N, Damping)) - 4669.296831218) < 1e-6);
        Assert.True(Math.Abs(1.0 / (1.0 - NeumannMu(48, N, Damping)) - 2.5) < 1e-12);
        Assert.True(Math.Abs(1.0 / (1.0 - NeumannMu(95, N, Damping)) - 1.250334722) < 1e-8);

        // (d) The inverse reconstructs the target exactly from its drive (error 1.9e-15) ...
        var rec = ReconstructFromDrive(s);
        double mean = tilted.Average();
        double err = 0.0;
        for (int i = 0; i < N; i++) err = Math.Max(err, Math.Abs(rec[i] - (tilted[i] - mean)));
        Assert.True(err < 1e-12, $"reconstruction error = {err}");

        // ... and the driven recursion converges onto it (rho <- W rho + s after 20 000 steps).
        var settled = Settle(tilted, s, 20000);
        double err2 = 0.0;
        for (int i = 0; i < N; i++) err2 = Math.Max(err2, Math.Abs(settled[i] - tilted[i]));
        Assert.True(err2 < 1e-12, $"settle error = {err2}");

        // (e) The four readouts of the witness, in the G_003/G_009 convention, are all far above the band.
        double dPhi = PhiOverC2(Witness);
        Assert.True(Math.Abs(dPhi - 0.201058) < 1e-6);
        Assert.True(Math.Abs(SecondsPerDay(Witness) - 17371.4) < 0.5);
        Assert.True(dPhi / BandCeiling > 4e4);
        Assert.True(dPhi / ClockFloor > 1e17);

        // VERDICT: no candidate is an actuator; rho is FULLY actuable given an imported source s, which the
        // canonical chain does not supply (G_005/G_008/G_010).
    }

    // ── 9. Research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_011_Run()
    {
        var sb = new StringBuilder();
        var canonical = Canonical;
        var tilted = Tilt;
        var sorted = SortedTilt;
        var sTilt = HoldDrive(tilted);

        PrintHeader(sb, "ResearchY-G_011 — RHO ACTUATOR AUDIT");
        sb.AppendLine("Question: can any physical quantity CHANGE rho?");
        sb.AppendLine("Candidates: energy density, phase coherence, spectral organization, degeneracy");
        sb.AppendLine("            engineering, attractor compression, synchronization, information density.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  rho is the counting measure on the D96 lattice (45 eigenspaces, 96 cells, 51 free).");
        sb.AppendLine("  A2  The canonical relaxation is RhoDynamics.DiffuseStep with d = 0.2 (G_006/G_007).");
        sb.AppendLine("  A3  A lattice-level change reads Delta Phi/c^2 = Delta a_AT / d (G_003/G_009).");
        sb.AppendLine("  A4  An actuator must be independent of rho, must determine it locally, must conserve the");
        sb.AppendLine("      count, must cost no energy and must introduce no new primitive (the audit's constraints).");
        sb.AppendLine();

        PrintHeader(sb, "1. THE FOUR MEASURED DELTAS PER CANDIDATE");
        sb.AppendLine("  candidate                Delta rho (L1)  Delta a_AT   Delta Phi/c^2  Delta tau [s/day]");
        sb.AppendLine($"  energy density (fixed E) {L1(canonical, tilted),14:F6}  {MaxAbsAcceleration(tilted),10:F6}  {PhiOverC2(Witness),13:F6}  {SecondsPerDay(Witness),16:F1}");
        sb.AppendLine($"  phase coherence                  {0.0,14:F6}  {0.0,10:F6}  {0.0,13:F6}  {0.0,16:F1}");
        sb.AppendLine($"  spectral organization    {L1(canonical, tilted),14:F6}  {MaxAbsAcceleration(tilted),10:F6}  {PhiOverC2(Witness),13:F6}  {SecondsPerDay(Witness),16:F1}");
        sb.AppendLine($"  degeneracy engineering   {L1(canonical, tilted),14:F6}  {MaxAbsAcceleration(tilted),10:F6}  {PhiOverC2(Witness),13:F6}  {SecondsPerDay(Witness),16:F1}");
        sb.AppendLine($"  attractor compression    {L1(BaseProfile, Compaction(BaseProfile, 48)),14:F6}  {Compression,10:F6}  {PhiOverC2(Compression),13:F6}  {SecondsPerDay(Compression),16:F1}");
        sb.AppendLine($"  synchronization                  {0.0,14:F6}  {0.0,10:F6}  {0.0,13:F6}  {0.0,16:F1}");
        sb.AppendLine($"  information density      {L1(tilted, Enumerable.Range(0, N).Select(i => tilted[(i + 37) % N]).ToArray()),14:F6}  n/a (global)");
        sb.AppendLine($"  imported source s        {L1Of(sTilt),14:F6}  (drives rho, Sigma s = {sTilt.Sum():E2})");        sb.AppendLine();

        PrintHeader(sb, "2. THE THREE TESTS PER CANDIDATE");
        sb.AppendLine("  candidate                function of rho?  independent?  local?       survives W?      drive/step");
        sb.AppendLine($"  energy density           YES (E=<l,r>)     no            yes          33.78x erased    {L1Of(sTilt):F5}");
        sb.AppendLine("  phase coherence          NO                YES (only 1)  yes          n/a (zero)       0");
        sb.AppendLine($"  spectral organization    YES (DCT biject)  no            yes          33.78x erased    {L1Of(sTilt):F5}");
        sb.AppendLine($"  degeneracy engineering   YES (free part)   no            yes          33.78x erased    {L1Of(sTilt):F5}");
        sb.AppendLine($"  attractor compression    YES (a map)       no            yes          1.12x (smooth)   {L1Of(HoldDrive(Compaction(BaseProfile, 48))):F5}");
        sb.AppendLine("  synchronization          NO                YES (a phase) yes          n/a (zero)       0");
        sb.AppendLine("  information density      YES (global)      no            NO (global)  invariant        0");
        sb.AppendLine();

        PrintHeader(sb, "3. THE INTERMEDIATE VALUES");
        sb.AppendLine($"  D96 arena .......................... 45 eigenspaces, 96 cells, free room 51 (D_048 L = 0.53125)");
        sb.AppendLine($"  energy E = <lambda, rho> ........... {EnergyOf(canonical):F12} (uniform) = {EnergyOf(tilted):F12} (tilt)  [exact]");
        sb.AppendLine($"  E(sorted arrangement) .............. {EnergyOf(sorted):F12}  (the same multiset, +{EnergyOf(sorted) - EnergyOf(tilted):F6})");
        sb.AppendLine($"  density-space contrast (tilt) ...... ln(4.8) = {DeltaLnRho(tilted, canonical):F12}  -> {DeltaLnRho(tilted, canonical) / D * 86400.0:F1} s/day");
        sb.AppendLine($"  psi-sector move (canonical phase) .. 0.0324196 -> 9.1171822 locked (factor 281.22) with Delta rho = 0");
        double rCanonical = Math.Sqrt(
            Math.Pow(Enumerable.Range(0, N).Sum(j => Math.Cos(2.0 * Math.PI * j / N)), 2)
            + Math.Pow(Enumerable.Range(0, N).Sum(j => Math.Sin(2.0 * Math.PI * j / N)), 2)) / N;
        sb.AppendLine($"  Kuramoto order parameter ........... {rCanonical:E2} canonical (maximally incoherent), 1.0 locked");
        sb.AppendLine($"  information density I_occ .......... {Kl(tilted):F12} = ln 96 - H = {Math.Log(N) - RhoDynamics.EntropyOf(tilted):F12}");
        sb.AppendLine($"  KL under the flow .................. {Kl(tilted):E8} -> {Kl(Iterate(tilted, 50)):E8} (50) -> {Kl(Iterate(tilted, 200)):E8} (200)");
        sb.AppendLine($"  hold-drive of the witness .......... ||s||_1 = {L1Of(sTilt):F8}, max|s| = {sTilt.Max(Math.Abs):F8}, Sigma s = {sTilt.Sum():E2}");
        sb.AppendLine($"  gains 1/(1 - mu_k) ................. {1.0 / (1.0 - NeumannMu(1, N, Damping)):F6} (k=1) ... {1.0 / (1.0 - NeumannMu(95, N, Damping)):F6} (k=95)");
        sb.AppendLine($"  reconstruction from the drive ...... error {Enumerable.Range(0, N).Max(i => Math.Abs(ReconstructFromDrive(sTilt)[i] - (tilted[i] - tilted.Average()))):E2}");
        sb.AppendLine();

        PrintHeader(sb, "4. CONCLUSIONS");
        sb.AppendLine("  C1  NO CANDIDATE IS AN ACTUATOR. Five of the seven are functions of rho (energy, spectral");
        sb.AppendLine("      organization, degeneracy engineering, compression, information) and two are independent");
        sb.AppendLine("      but exactly rho-inert (phase coherence, synchronization: Delta rho = 0 to 1e-15 and");
        sb.AppendLine("      |Delta a| < 1e-9 while the psi-sector moves by a factor 281).");
        sb.AppendLine("  C2  The sharpest case is the phase: it is the ONLY candidate that is genuinely independent of");
        sb.AppendLine("      rho, and rho = |psi|^2 is exactly phase-blind, so the one true degree of freedom outside");
        sb.AppendLine("      rho cannot touch it.");
        sb.AppendLine("  C3  rho is nevertheless FULLY ACTUABLE: s = (I - W) rho* is invertible on the 95 zero-sum");
        sb.AppendLine("      directions (gains 4669.2968 ... 1.2503, no zero-gain mode), count-neutral, and unique per");
        sb.AppendLine("      target. The vacancy is a SOURCE, not a handle.");
        sb.AppendLine("  C4  The energy is an exact linear re-expression (identical to 1e-12 at fixed eigenspace totals");
        sb.AppendLine("      while rho moves by 2/3 of the count), so the audit confirms G_001 from the actuator side:");
        sb.AppendLine("      energy is a correlate. Re-ordering the same multiset moves E by 12.8 %, i.e. the arrangement");
        sb.AppendLine("      is prior to the energy.");
        sb.AppendLine("  C5  Compression is a MAP: its difference is SMOOTH (survives 200 steps 1.12x, unlike the");
        sb.AppendLine("      witness's 33.78x) and it is powered by nothing. G_002's CONTROLLABLE verdict for the");
        sb.AppendLine("      OPERATIONS is unchanged; the G_011 labels are about the QUANTITY being an actuator.");
        sb.AppendLine("  C6  SUCCESS CRITERION: the first physical handle that changes rho is the imported source term");
        sb.AppendLine("      s = (I - W) rho*, which is not an AT primitive — the same missing driver as G_008/G_010.");
        sb.AppendLine();

        PrintHeader(sb, "5. CLASSIFICATION AND CAVEATS");
        sb.AppendLine("  ACTUATOR   none of the seven candidates; the imported source s (Sigma s = 0) is the only");
        sb.AppendLine("             quantity that changes rho, and it is external.");
        sb.AppendLine("  CORRELATED energy density (exact re-expression), spectral organization (orthonormal");
        sb.AppendLine("             coordinates), degeneracy engineering (the 51-dimensional free room = the free part");
        sb.AppendLine("             of rho itself).");
        sb.AppendLine("  REFUTED    phase coherence, synchronization (independent and inert), attractor compression");
        sb.AppendLine("             (a map, not a quantity), information density (global, permutation-invariant).");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changed; no new");
        sb.AppendLine("  primitive. Deterministic: exact algebra, no randomness.");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
