using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_003 — Gravity Magnitude Audit (group G — Gravity Source).
///
/// Given: G_001 established that gravity is sourced by the counting measure rho, and G_002 that rho can
/// be reorganised at fixed total energy (Σm = 0 exactly, QG194). This audit answers the quantitative
/// follow-up: WHAT PHYSICAL GRAVITATIONAL CHANGE corresponds to a measured Δrho?
///
/// THE CONVERSION CHAIN (all canonical; no fitted quantity):
///   metric      g_00 = -rho^(2/d)                      (QG197)
///   potential   ΔPhi/c^2 = (1/d)·Δln rho               (redshift law, QG21/QG187)
///   acceleration Δa = -grad ΔPhi = -(c^2/d)·grad ln rho (G4-O3)
///   curvature   ΔR_phys = ΔR_AT / L^2                   (G4-G2, per physical length)
///   Since the audit inverts a = -(1/d) grad ln rho over ONE cell, its Δ measures satisfy
///   Δln rho = d·Δa_AT exactly, so the three physical reads of a lattice-level change are:
///     ΔPhi/c^2 = Δa_AT                        [dimensionless — NO length scale needed]
///     Δa_phys  = c^2·Δa_AT / L                [m/s^2 — needs the physical scale L]
///     ΔR_phys  = ΔR_AT / L^2                  [1/m^2 — needs L]
///     ΔM_eq    = Δa_phys·L^2/G = c^2·Δa_AT·L/G [kg — the equivalent Newtonian mass]
///
/// CALIBRATION ASSUMPTION (BOUNDARY): a lattice-level Δa_AT is realised as a gradient across one
/// physical coherence length L. AT's rho is a large-scale field, so L is astrophysical/cosmological;
/// the potential channel is the only one free of L.
///
/// VERDICTS
///   MEASURABLE        — the potential / redshift / clock channel: ΔPhi/c^2 = Δa_AT is a pure number,
///                       so a G_002-class reconfiguration (Δa_AT = 0.032 … 0.686) gives a 3-69 %
///                       clock/redshift change, ~1e17x above an optical clock's 1e-18 floor.
///   ASTROPHYSICAL ONLY — the acceleration / curvature channel: it needs a physical region of size L,
///                       and the thresholds are met only for L below 9.5-204 kpc (1e-6 g),
///                       9.5-204 Mpc (1e-9 g) and 9.5-204 Gpc (1e-12 g). No laboratory realisation
///                       exists: rho is a large-scale field and G_002 operations are lattice
///                       reorganisations.
///   PRACTICALLY ZERO  — phase coherence (all channels exactly zero) and global rescaling (Δa = 0
///                       exactly; the curvature changes only by the overall factor lambda^(-2/d),
///                       i.e. the observable geometry is untouched). Within the observable universe
///                       NO non-zero G_002 configuration is practically zero.
///
/// CRITICAL QUESTION. Can any G_002 configuration produce a gravity change above 1e-12 g, 1e-9 g,
/// 1e-6 g WITHOUT changing total energy? YES to all three — the binding constraint is the physical
/// scale L, not the energy budget:
///     1e-12 g  for  L < 9.5 … 204 Gpc      (every case; the observable universe is ~14 Gpc)
///     1e-9  g  for  L < 9.5 … 204 Mpc      (every case)
///     1e-6  g  for  L < 9.5 … 204 kpc      (every case)
/// Conversely the observed galactic field g† = cH0/(2π) = 1.0422e-10 m/s^2 corresponds to an ambient
/// Δln rho = 1.61e-6 over 15 kpc, so any REALISED reconfiguration must be suppressed by >= 3.7e5 —
/// the audit's falsifiable requirement (G_002 OP2: no physical process is known to realise it).
///
/// Deterministic: closed-form configurations, shared catalog spectra, no randomness. No reclassification.
/// </summary>
public class Y_G_003_Tests : ResearchTestBase
{
    public Y_G_003_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Lambda = 3.7;

    // ── The conversion laws ──────────────────────────────────────────────────────

    /// <summary>ΔPhi/c^2 for a lattice-level change characterised by Δa_AT (pure number; no length scale).</summary>
    private static double PotentialFraction(double deltaAat) => deltaAat;

    /// <summary>Δa in m/s^2 for a change of Δa_AT realised across a physical length L (metres).</summary>
    private static double PhysicalAcceleration(double deltaAat, double length) => C * C * deltaAat / length;

    /// <summary>The physical length below which Δa exceeds a given acceleration (the threshold length).</summary>
    private static double ThresholdLength(double deltaAat, double acceleration)
        => deltaAat <= 0.0 ? 0.0 : C * C * deltaAat / acceleration;

    /// <summary>ΔR in 1/m^2 for a lattice-level curvature change ΔR_AT realised over a physical length L.</summary>
    private static double PhysicalCurvature(double deltaRat, double length) => deltaRat / (length * length);

    /// <summary>Deviation of R(λρ)/R(ρ) from λ^(-2/d) over the profile's significant probes.</summary>
    private static double CurvatureScalingDeviation(double[] a, double[] b, double lambda)
    {
        var fa = PiecewiseLinear(a);
        var fb = PiecewiseLinear(b);
        double maxA = 0.0;
        for (int i = 1; i < a.Length; i++) maxA = Math.Max(maxA, Math.Abs(ScalarCurvatureOf(fa, i + 0.5)));
        double target = Math.Pow(lambda, -2.0 / D), dev = 0.0;
        for (int i = 1; i < a.Length; i++)
        {
            double ra = ScalarCurvatureOf(fa, i + 0.5);
            if (Math.Abs(ra) < 0.01 * maxA) continue;
            dev = Math.Max(dev, Math.Abs(ScalarCurvatureOf(fb, i + 0.5) / ra - target));
        }
        return dev;
    }

    // ── The G_002 configurations, with their measured lattice-level changes ───────

    private sealed record Case(string Name, double DeltaAat, double DeltaRat);

    private static Case[] ConfiguredCases()
    {
        var (_, m96) = D96Spaces;
        var (_, mrand) = RandomSpaces;
        var (_, mcube) = CubeSpaces;

        var canonical = Spread(m96, 1.0);
        var tilted = Spread(m96, 1.0, TiltFractions);
        var sortedTilt = tilted.OrderBy(v => v).ToArray();
        var rhoD96 = m96.Select(m => m / (double)N).ToArray();
        var rhoRand = mrand.Select(m => m / (double)N).ToArray();
        var rhoCube = mcube.Select(m => m / (double)(N * N * N)).ToArray();
        var scaled = tilted.Select(v => Lambda * v).ToArray();
        var baseProfile = Enumerable.Range(1, N)
            .Select(j => 1.0 - 0.4 * Math.Log(N / (double)j) / Math.Log(N)).ToArray();
        var compact48 = Compaction(baseProfile, 48);

        return new[]
        {
            new Case("arrangement", MaxAccelerationDifference(tilted, sortedTilt),
                                   MaxCurvatureDifference(tilted, sortedTilt)),
            new Case("phase", 0.0, 0.0),
            new Case("degeneracy redistribution", MaxAbsAcceleration(tilted) - MaxAbsAcceleration(canonical),
                                   MaxAbsCurvature(tilted)),
            new Case("survivor compression (k=48)", MaxAccelerationDifference(baseProfile, compact48),
                                   MaxCurvatureDifference(baseProfile, compact48)),
            new Case("D96 vs random", Math.Abs(MaxAbsAcceleration(rhoD96) - MaxAbsAcceleration(rhoRand)),
                                   MaxCurvatureDifference(rhoD96, rhoRand)),
            // The two cross-lattice cases compare the same-lattice maximum reads (the densities live on
            // different cell counts, so a shared probe axis is not defined).
            new Case("D96^3 vs D96", Math.Abs(MaxAbsAcceleration(rhoCube) - MaxAbsAcceleration(rhoD96)),
                                   Math.Abs(MaxAbsCurvature(rhoCube) - MaxAbsCurvature(rhoD96))),
            new Case("rescaling (control)", MaxAccelerationDifference(tilted, scaled),
                                   MaxAbsCurvature(scaled) - MaxAbsCurvature(tilted)),
        };
    }

    // ── 1. Calibration: the conversion chain and its canonical anchor ─────────────

    [Fact]
    public void Y_G_003_Calibration()
    {
        // (a) The AT acceleration scale g† = cH0/(2π) (QG080) — the one physical acceleration scale AT has.
        Assert.True(Math.Abs(GDagger - 1.0422e-10) / 1.0422e-10 < 1e-3, $"g† = {GDagger}");
        // It sits BETWEEN the 1e-12 g and 1e-9 g thresholds: 0.094 g† and 94.2 g† respectively.
        Assert.Equal(0.0942, (1e-12 * G_N) / GDagger, 3);
        double x9c = (1e-9 * G_N) / GDagger;
        Assert.True(x9c > 9.4e1 && x9c < 9.5e1, $"1e-9 g = {x9c} g†");

        // (b) The potential law ΔPhi/c^2 = (1/d)Δln rho, cross-checked on the canonical GPS anchor
        // (QG187): the Earth-surface vs GPS-orbit potential, Δtau/tau = ΔPhi/c^2, is 45.7 μs/day.
        // GM_Earth, R_Earth and the GPS orbital radius from the shared PhysicalUnits anchors.
        double muEarth = GM_Earth, rEarth = R_Earth, rGps = R_Gps;
        double phiOverC2 = muEarth / (C * C) * (1.0 / rEarth - 1.0 / rGps);
        double microSecondsPerDay = phiOverC2 * 86400.0 * 1e6;
        Assert.Equal(45.74, microSecondsPerDay, 1);            // QG187: +45.7 μs/day
        // ... and in AT the same potential IS a counting-measure contrast Δln rho = d·ΔPhi/c^2, so the
        // Earth-GPS potential difference corresponds to Δln rho = 1.588e-9.
        Assert.True(Math.Abs(D * phiOverC2 - 1.58805e-9) / 1.58805e-9 < 1e-4, $"Δln rho = {D * phiOverC2}");

        // (c) STRUCTURAL: the potential channel carries NO length scale, the acceleration is exactly 1/L.
        Assert.Equal(0.603175, PotentialFraction(0.603175), 12);
        double at1kpc = PhysicalAcceleration(0.603175, Kpc);
        double at1Mpc = PhysicalAcceleration(0.603175, Mpc);
        Assert.Equal(Mpc / Kpc, at1kpc / at1Mpc, 12);          // a ∝ 1/L exactly
        Assert.Equal(1.0e3, Mpc / Kpc, 12);

        // (d) The equivalent Newtonian mass and the curvature read come with L and L^2.
        double a = PhysicalAcceleration(0.603175, 15 * Kpc);
        Assert.True(Math.Abs(EquivalentMass(a, 15 * Kpc) - a * Math.Pow(15 * Kpc, 2) / G_SI)
                    / EquivalentMass(a, 15 * Kpc) < 1e-12);
        Assert.Equal(4.0, PhysicalCurvature(1.0, 2.0) / PhysicalCurvature(1.0, 4.0), 12);   // R ∝ 1/L²
    }

    // ── 2. The AT-native deltas per case ─────────────────────────────────────────

    [Fact]
    public void Y_G_003_ATDelta()
    {
        var cases = ConfiguredCases();
        Case Get(string name) => cases.Single(c => c.Name == name);

        // The four witnesses all move rho at fixed total energy (G_002), with these lattice-level deltas.
        Assert.Equal(0.685714, Get("arrangement").DeltaAat, 6);
        Assert.Equal(0.603175, Get("degeneracy redistribution").DeltaAat, 6);
        Assert.Equal(0.333333, Get("D96 vs random").DeltaAat, 6);
        Assert.Equal(0.276596, Get("D96^3 vs D96").DeltaAat, 6);
        Assert.Equal(0.032121, Get("survivor compression (k=48)").DeltaAat, 6);

        // Ordering: arrangement > degeneracy > lattice averages > survivor compression.
        Assert.True(Get("arrangement").DeltaAat > Get("degeneracy redistribution").DeltaAat);
        Assert.True(Get("degeneracy redistribution").DeltaAat > Get("D96 vs random").DeltaAat);
        Assert.True(Get("D96 vs random").DeltaAat > Get("D96^3 vs D96").DeltaAat);
        Assert.True(Get("D96^3 vs D96").DeltaAat > Get("survivor compression (k=48)").DeltaAat);

        // The two ZERO cases: phase is exactly inert; rescaling leaves the ACCELERATION invariant to
        // floating-point (scale invariance, G_001) — its curvature changes only by the overall factor
        // λ^(-2/d), i.e. the observable geometry is untouched.
        Assert.Equal(0.0, Get("phase").DeltaAat, 12);
        Assert.Equal(0.0, Get("phase").DeltaRat, 12);
        Assert.True(Get("rescaling (control)").DeltaAat < 1e-9, $"{Get("rescaling (control)").DeltaAat}");

        var (_, m96) = D96Spaces;
        var tilted = Spread(m96, 1.0, TiltFractions);
        var scaled = tilted.Select(v => Lambda * v).ToArray();
        Assert.True(CurvatureScalingDeviation(tilted, scaled, Lambda) < 1e-6,
            $"deviation = {CurvatureScalingDeviation(tilted, scaled, Lambda)}");
    }

    // ── 3. Physical units: Δa, ΔR, ΔPhi on the canonical scale ladder ────────────

    [Fact]
    public void Y_G_003_PhysicalField()
    {
        var strongest = ConfiguredCases().Single(c => c.Name == "arrangement");
        double da = strongest.DeltaAat;

        // The potential (clock/redshift) channel: a pure number, ΔPhi/c^2 = Δa_AT.
        Assert.Equal(0.685714, PotentialFraction(da), 6);
        // ΔPhi = c^2·(ΔPhi/c^2) = 6.1629e16 m^2/s^2 — i.e. 68.6% of c^2 (a strong-field change).
        Assert.True(Math.Abs(C * C * da - 6.1629e16) / 6.1629e16 < 1e-4, $"ΔPhi = {C * C * da}");

        // The acceleration channel on the canonical ladder (L, Δa, Δa/g, ΔM_eq).
        var ladder = new (string Name, double L)[]
        {
            ("1 pc", Pc), ("1 kpc", Kpc), ("15 kpc", 15 * Kpc), ("1 Mpc", Mpc), ("1 Gpc", Gpc),
            ("c/H0", HubbleLength),
        };
        double A(double L) => PhysicalAcceleration(da, L);

        Assert.Equal(1.9973, A(Pc), 4);                       // 2.04e-1 g
        Assert.Equal(1.9973e-3, A(Kpc), 7);                   // 2.04e-4 g
        Assert.Equal(1.3315e-4, A(15 * Kpc), 8);              // 1.36e-5 g
        Assert.Equal(1.9973e-6, A(Mpc), 10);                  // 2.04e-7 g
        Assert.Equal(1.9973e-9, A(Gpc), 13);                  // 2.04e-10 g
        Assert.Equal(4.490e-10, A(HubbleLength), 12);         // 4.58e-11 g — still above 1e-12 g
        Assert.True(InG(A(HubbleLength)) > 1e-12);
        Assert.True(InG(A(Gpc)) > 1e-12 && InG(A(Gpc)) < 1e-9);

        // The equivalent Newtonian mass at the galactic scale is cluster-scale — 3.6e6 Milky Ways.
        double mEq15 = EquivalentMass(A(15 * Kpc), 15 * Kpc);
        Assert.True(Math.Abs(mEq15 / MSun - 2.157e17) / 2.157e17 < 1e-3, $"ΔM_eq = {mEq15 / MSun} Msun");
        Assert.True(Math.Abs(mEq15 / MSun / 6.0e10 - 3.6e6) / 3.6e6 < 1e-2);   // vs the Milky Way (6e10 Msun)

        // The curvature channel: ΔR in 1/m^2 on the same ladder.
        double r15 = PhysicalCurvature(strongest.DeltaRat, 15 * Kpc);
        Assert.True(Math.Abs(r15 - 2.512e-40) / 2.512e-40 < 1e-3, $"ΔR(15 kpc) = {r15}");
        double rG = PhysicalCurvature(strongest.DeltaRat, Gpc);
        Assert.True(Math.Abs(rG - 5.652e-50) / 5.652e-50 < 1e-3, $"ΔR(1 Gpc) = {rG}");
    }

    // ── 4. THE CRITICAL QUESTION: threshold lengths at fixed total energy ────────

    [Fact]
    public void Y_G_003_Thresholds()
    {
        var cases = ConfiguredCases();
        double t12 = 1e-12 * G_N, t9 = 1e-9 * G_N, t6 = 1e-6 * G_N;
        // "live" = a real force-channel change; below 1e-6 is floating-point residue (the rescaling case).
        var live = cases.Where(c => c.DeltaAat > 1e-6).ToArray();

        foreach (var c in live)
        {
            double l12 = ThresholdLength(c.DeltaAat, t12);
            double l9 = ThresholdLength(c.DeltaAat, t9);
            double l6 = ThresholdLength(c.DeltaAat, t6);

            // The three windows nest, and at the threshold length the change is exactly at threshold.
            Assert.True(l12 > l9 && l9 > l6, $"{c.Name}: {l12} {l9} {l6}");
            Assert.True(Math.Abs(InG(PhysicalAcceleration(c.DeltaAat, l12)) / 1e-12 - 1.0) < 1e-9);
            Assert.True(Math.Abs(InG(PhysicalAcceleration(c.DeltaAat, l9)) / 1e-9 - 1.0) < 1e-9);
            Assert.True(Math.Abs(InG(PhysicalAcceleration(c.DeltaAat, l6)) / 1e-6 - 1.0) < 1e-9);

            // Just inside each window the change is ABOVE the threshold (at fixed total energy).
            Assert.True(InG(PhysicalAcceleration(c.DeltaAat, 0.5 * l12)) > 1e-12);
            Assert.True(InG(PhysicalAcceleration(c.DeltaAat, 0.5 * l9)) > 1e-9);
            Assert.True(InG(PhysicalAcceleration(c.DeltaAat, 0.5 * l6)) > 1e-6);
        }

        // The GUARANTEED window is set by the weakest witness (survivor compression).
        double min12 = live.Min(c => ThresholdLength(c.DeltaAat, t12));
        double min9 = live.Min(c => ThresholdLength(c.DeltaAat, t9));
        double min6 = live.Min(c => ThresholdLength(c.DeltaAat, t6));
        Assert.Equal(9.54, min12 / Gpc, 2);
        Assert.Equal(9.54, min9 / Mpc, 2);
        Assert.Equal(9.54, min6 / Kpc, 2);

        // The strongest witness (arrangement).
        double max12 = live.Max(c => ThresholdLength(c.DeltaAat, t12));
        Assert.Equal(203.66, max12 / Gpc, 2);

        // ANSWER: yes to all three thresholds — above 1e-6 g for any L below ~9.5 kpc, above 1e-9 g
        // below ~9.5 Mpc, above 1e-12 g below ~9.5 Gpc — with the total energy held fixed.
        Assert.True(min6 > 9.0 * Kpc && min6 < 10.0 * Kpc);
        Assert.True(min9 > 9.0 * Mpc && min9 < 10.0 * Mpc);
        Assert.True(min12 > 9.0 * Gpc && min12 < 10.0 * Gpc);

        // Phase coherence and rescaling never pass: their force channel is zero at any L.
        foreach (var c in cases.Where(c => c.DeltaAat <= 1e-6))
        {
            Assert.True(c.DeltaAat < 1e-9, $"{c.Name}: {c.DeltaAat}");
            Assert.True(ThresholdLength(c.DeltaAat, t12) < 1e-6 * Gpc);
            Assert.True(PhysicalAcceleration(c.DeltaAat, Pc) < 1e-6);
        }
    }

    // ── 5. Detectability thresholds ──────────────────────────────────────────────

    [Fact]
    public void Y_G_003_Detectability()
    {
        var cases = ConfiguredCases();
        double witness = cases.Single(c => c.Name == "degeneracy redistribution").DeltaAat;

        // (a) The three thresholds measured in the AT field scale g† = 1.0422e-10 m/s^2 (QG080):
        //     1e-12 g = 0.094 g† (about a tenth of the galactic AT field),
        //     1e-9  g = 94 g†,   1e-6 g = 9.4e4 g†  — utterly unmissable if realised.
        Assert.Equal(0.0942, (1e-12 * G_N) / GDagger, 3);
        double x9 = (1e-9 * G_N) / GDagger;
        Assert.True(x9 > 9.4e1 && x9 < 9.5e1, $"1e-9 g = {x9} g†");
        double x6 = (1e-6 * G_N) / GDagger;
        Assert.True(x6 > 9.4e4 && x6 < 9.5e4, $"1e-6 g = {x6} g†");
        Assert.True(InG(GDagger) > 1e-12 && InG(GDagger) < 1e-9);      // the RAR scale sits between them

        // (b) The ambient calibration: the whole observed galactic AT field corresponds to a counting-
        // measure contrast of only 1.61e-6 over 15 kpc (a = (c^2/d) dln rho / L = g†).
        double ambientDln = D * GDagger * (15 * Kpc) / (C * C);
        Assert.Equal(1.610e-6, ambientDln, 9);
        Assert.True(Math.Abs(PhysicalAcceleration(ambientDln / D, 15 * Kpc) / GDagger - 1.0) < 1e-9);

        // (c) THE SUPPRESSION REQUIREMENT (the audit's falsifiable number): a G_002-class witness is
        // 3.7e5x the observed ambient contrast, so any REALISED reconfiguration must be suppressed by
        // at least 3.7e5 to leave the observed rotation curves intact.
        Assert.True(Math.Abs(witness / ambientDln - 3.746e5) / 3.746e5 < 1e-3);
        Assert.True(witness > 1e5 * ambientDln);

        // (d) The potential channel is 1e17x above an optical clock's 1e-18 fractional floor.
        Assert.True(witness / 1e-18 > 1e17);
        Assert.True(witness > 1e4 * 1e-11);        // ~1e-11 is the strongest current astrophysical bound

        // (e) The equivalent Newtonian mass at the galactic scale is cluster/supercluster-scale.
        double mEq = EquivalentMass(PhysicalAcceleration(witness, 15 * Kpc), 15 * Kpc);
        Assert.True(mEq / MSun > 1e16, $"dM_eq = {mEq / MSun} Msun");
        Assert.True(mEq / MSun / 6.0e10 > 1e5);    // >> the Milky Way's 6e10 Msun
    }

    // ── 6. Verdicts ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_003_Verdicts()
    {
        var cases = ConfiguredCases();
        var live = cases.Where(c => c.DeltaAat > 1e-6).ToArray();

        // MEASURABLE — the potential / clock / redshift channel is a pure number with no length scale.
        Assert.Equal(live.Length, 5);
        foreach (var c in live) Assert.True(c.DeltaAat >= 0.03, $"{c.Name} = {c.DeltaAat}");
        Assert.True(live.Min(c => c.DeltaAat) > 1e16 * 1e-18);      // far above the clock floor

        // ASTROPHYSICAL ONLY — the acceleration / curvature channel needs a region of size L, and its
        // three windows are kpc-, Mpc- and Gpc-scale respectively.
        double min12 = live.Min(c => ThresholdLength(c.DeltaAat, 1e-12 * G_N));
        double min6 = live.Min(c => ThresholdLength(c.DeltaAat, 1e-6 * G_N));
        Assert.True(min12 > 9.0 * Gpc && min12 < 14.3 * Gpc);        // inside the horizon, not beyond
        Assert.True(min6 > 9.0 * Kpc && min6 < 10.0 * Kpc);          // galactic scale
        Assert.True(min6 > 100.0 * Pc, $"min6 = {min6 / Pc} pc");     // never a laboratory/solar scale

        // PRACTICALLY ZERO — exactly: phase coherence and global rescaling.
        Assert.Equal(0.0, cases.Single(c => c.Name == "phase").DeltaAat, 12);
        Assert.Equal(0.0, cases.Single(c => c.Name == "phase").DeltaRat, 12);
        Assert.True(cases.Single(c => c.Name == "rescaling (control)").DeltaAat < 1e-9);
        // ... and the "practically zero" REGIME of the acceleration channel lies at/beyond the horizon:
        // below 1e-12 g requires L > 9.5-204 Gpc, i.e. larger than the observable universe (14.3 Gpc).
        Assert.True(min12 > 9.0 * Gpc);
    }

    // ── 7. Report ────────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_003 — Gravity Magnitude Audit");

        var cases = ConfiguredCases();
        var live = cases.Where(c => c.DeltaAat > 1e-6).ToArray();
        double t12 = 1e-12 * G_N, t9 = 1e-9 * G_N, t6 = 1e-6 * G_N;
        double ambientDln = D * GDagger * (15 * Kpc) / (C * C);

        sb.AppendLine("Given: rho sources gravity (G_001) and rho is controllable at FIXED total energy (G_002).");
        sb.AppendLine("Question: what physical gravitational change corresponds to a measured Δrho?");
        sb.AppendLine();
        sb.AppendLine("[0] Assumptions — the conversion chain (all canonical)");
        sb.AppendLine("    g_00 = -rho^(2/d)                            (QG197)");
        sb.AppendLine("    ΔPhi/c^2 = (1/d)·Δln rho                     (redshift law, QG21/QG187)");
        sb.AppendLine("    Δa = -grad ΔPhi = -(c^2/d)·grad ln rho       (G4-O3)");
        sb.AppendLine("    ΔR_phys = ΔR_AT / L^2                        (G4-G2, per physical length)");
        sb.AppendLine("    Inverting a = -(1/d) grad ln rho over ONE cell gives Δln rho = d·Δa_AT, so");
        sb.AppendLine("      ΔPhi/c^2 = Δa_AT                (pure number — NO length scale)");
        sb.AppendLine("      Δa_phys  = c^2·Δa_AT / L        (m/s^2 — needs the physical scale L)");
        sb.AppendLine("      ΔR_phys  = ΔR_AT / L^2          (1/m^2)");
        sb.AppendLine("      ΔM_eq    = Δa_phys·L^2/G        (kg — equivalent Newtonian mass)");
        sb.AppendLine("    CALIBRATION ASSUMPTION (BOUNDARY): a lattice-level Δa_AT is realised as a gradient");
        sb.AppendLine("    across ONE physical coherence length L. AT's rho is a large-scale field, so L is");
        sb.AppendLine("    astrophysical or cosmological; only the potential channel is free of L.");
        sb.AppendLine($"    Anchors: c, g = 9.80665 m/s^2, G = {G_SI:E4} (QG181), g† = {GDagger:F4} m/s^2 (QG080).");
        sb.AppendLine();

        sb.AppendLine("[1] Calibration anchor — the GPS/redshift cross-check");
        double muE = 3.986004418e14, rE = 6.371e6, rG = 2.66e7;
        double phi = muE / (C * C) * (1.0 / rE - 1.0 / rG);
        sb.AppendLine($"    ΔPhi/c^2 (Earth surface vs GPS orbit) = {phi:E4}  ->  {phi * 86400 * 1e6:F2} us/day (QG187: 45.7)");
        sb.AppendLine($"    equivalently AT's counting-measure contrast over the same potential: Δln rho = {D * phi:E4}");
        sb.AppendLine($"    g† in units of g: {InG(GDagger):E4}  (between the 1e-12 g and 1e-9 g thresholds)");
        sb.AppendLine();

        sb.AppendLine("[2] Theoretical AT delta, physical field, equivalent mass (per case)");
        sb.AppendLine("    case                        Δa_AT     ΔPhi/c^2    Δa @15kpc [m/s2]   [g]        ΔM_eq @15kpc [Msun]");
        foreach (var c in cases)
        {
            double a15 = PhysicalAcceleration(c.DeltaAat, 15 * Kpc);
            double mEq = EquivalentMass(a15, 15 * Kpc) / MSun;
            sb.AppendLine($"    {c.Name,-26} {c.DeltaAat,8:F6}  {PotentialFraction(c.DeltaAat),10:F6}  {a15,17:E4}  {InG(a15),9:E3}  {mEq,17:E3}");
        }
        sb.AppendLine();

        sb.AppendLine("[3] Scale ladder (strongest witness: arrangement, Δa_AT = 0.685714, ΔPhi/c^2 = 0.6857)");
        sb.AppendLine("    L              Δa [m/s2]      Δa / g        ΔR [1/m2]      ΔM_eq [Msun]");
        double das = cases.Single(c => c.Name == "arrangement").DeltaAat;
        double drs = cases.Single(c => c.Name == "arrangement").DeltaRat;
        foreach (var (name, L) in new (string, double)[]
                 { ("1 pc", Pc), ("1 kpc", Kpc), ("15 kpc", 15 * Kpc), ("1 Mpc", Mpc), ("1 Gpc", Gpc), ("c/H0", HubbleLength) })
        {
            double a = PhysicalAcceleration(das, L);
            sb.AppendLine($"    {name,-10} {a,13:E4}  {InG(a),12:E3}  {PhysicalCurvature(drs, L),13:E4}  {EquivalentMass(a, L) / MSun,13:E3}");
        }
        sb.AppendLine();

        sb.AppendLine("[4] THE CRITICAL QUESTION — threshold lengths at FIXED total energy");
        sb.AppendLine("    case                        >1e-12 g  for L <      >1e-9 g  for L <       >1e-6 g  for L <");
        foreach (var c in live)
            sb.AppendLine($"    {c.Name,-26} {ThresholdLength(c.DeltaAat, t12) / Gpc,9:F2} Gpc   {ThresholdLength(c.DeltaAat, t9) / Mpc,9:F2} Mpc   {ThresholdLength(c.DeltaAat, t6) / Kpc,9:F2} kpc");
        sb.AppendLine($"    {"phase, rescaling",-26} {0.0,9:F2} Gpc   {0.0,9:F2} Mpc   {0.0,9:F2} kpc   (force channel identically zero)");
        sb.AppendLine($"    GUARANTEED window (weakest witness): 1e-12 g for L < {live.Min(c => ThresholdLength(c.DeltaAat, t12)) / Gpc:F2} Gpc,");
        sb.AppendLine($"                                          1e-9 g for L < {live.Min(c => ThresholdLength(c.DeltaAat, t9)) / Mpc:F2} Mpc,");
        sb.AppendLine($"                                          1e-6 g for L < {live.Min(c => ThresholdLength(c.DeltaAat, t6)) / Kpc:F2} kpc.");
        sb.AppendLine($"    ANSWER: YES to all three thresholds, at fixed total energy. The binding constraint is the");
        sb.AppendLine($"    physical scale L, not the energy budget. The observable-universe radius (~14.3 Gpc) exceeds");
        sb.AppendLine($"    the 1e-12 g window of every case, so the 'practically zero' regime lies beyond the horizon.");
        sb.AppendLine();

        sb.AppendLine("[5] Detectability");
        sb.AppendLine($"    1e-12 g = {(1e-12 * G_N) / GDagger:F4} g†   1e-9 g = {(1e-9 * G_N) / GDagger:F1} g†   1e-6 g = {(1e-6 * G_N) / GDagger:E2} g†");
        sb.AppendLine($"    ambient galactic calibration: g† over 15 kpc  <=>  Δln rho = {ambientDln:E4}");
        sb.AppendLine($"    suppression requirement: the G_002 witnesses are {cases.Single(c => c.Name == "degeneracy redistribution").DeltaAat / ambientDln:E4}x");
        sb.AppendLine($"    the observed ambient contrast, so any REALISED reconfiguration must be suppressed by >= 3.7e5");
        sb.AppendLine($"    (G_002 OP2: no physical process is known to realise one).");
        sb.AppendLine($"    clock floor: Δa_AT = 1e-18 (optical clock) is still {cases.Single(c => c.Name == "degeneracy redistribution").DeltaAat / 1e-18:E2}x below the witness.");
        sb.AppendLine();

        sb.AppendLine("[6] Verdicts");
        sb.AppendLine("    MEASURABLE ......... the potential / clock / redshift channel: ΔPhi/c^2 = Δa_AT is a pure");
        sb.AppendLine($"                         number, so the witnesses give 3-69 % clock and redshift changes —");
        sb.AppendLine("                         ~1e17x above an optical clock's 1e-18 floor. No length scale enters.");
        sb.AppendLine("    ASTROPHYSICAL ONLY . the acceleration / curvature channel: it requires a region of size L; the");
        sb.AppendLine("                         thresholds are met only at kpc (1e-6 g), Mpc (1e-9 g) and Gpc (1e-12 g)");
        sb.AppendLine("                         scales. No laboratory realisation exists — rho is a large-scale field");
        sb.AppendLine("                         and the G_002 operations are lattice reorganisations.");
        sb.AppendLine("    PRACTICALLY ZERO ... phase coherence (every channel exactly zero) and global rescaling");
        sb.AppendLine("                         (Δa = 0 exactly; the curvature changes only by the overall factor");
        sb.AppendLine("                         λ^(-2/d), so the observable geometry is untouched).");
        sb.AppendLine();
        sb.AppendLine("[7] Classification and caveats");
        sb.AppendLine("    DERIVED : the conversion laws and their canonical anchors (QG197/QG21/QG187/G4-G2/G4-O3);");
        sb.AppendLine("              the threshold lengths and the nesting of the three windows; the exact zeros of the");
        sb.AppendLine("              phase and rescaling channels; the ambient calibration 1.61e-6 and the 3.7e5");
        sb.AppendLine("              suppression requirement.");
        sb.AppendLine("    BOUNDARY: the realisation of a lattice-level Δa_AT as a gradient across ONE physical length");
        sb.AppendLine("              L; the value of G (QG181, 0.40% from CODATA) and of g† = cH0/(2π); the reference");
        sb.AppendLine("              normalisation rhoBar = 1/N and the exponent 2/d.");
        sb.AppendLine("    EMERGENT: the specific per-case magnitudes (they follow from the G_002 witnesses).");
        sb.AppendLine("    No reclassification; the D_040 ClassificationRegistry is untouched.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}

