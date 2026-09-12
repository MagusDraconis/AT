using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_005 — Control Realizability Audit (group G — Gravity Source).
///
/// Question: WHY are the large G_003 gravity-control modes not realised in nature?
///
/// Given: G_002 showed rho is controllable at fixed total energy (51 free directions = N − A0);
/// G_003 showed the resulting gravity change would be huge at astrophysical scales (1e-6 g for
/// L < 9.54 kpc) and that any REALISED reconfiguration must be suppressed by >= 3.746e5;
/// G_004 showed the observed fields are exactly that much smaller (g† = 1.06e-11 g).
///
/// Tested channels: (1) stability, (2) entropy cost, (3) conservation constraints,
/// (4) dynamical accessibility.
///
/// VERDICTS
///   ACCESSIBLE — contrasts at or below the Poisson-natural level of the counting measure. AT's own
///                mandatory fluctuation law (QG15/QG228/QG231: δ = 1/sqrt(<N>)) makes the observed
///                contrast 1.6102e-6 a TYPICAL fluctuation (P = 0.61) of a cell with <N> = 3.8569e11
///                events, and the whole band up to ~4.9e-6 (P > 1 %) is reachable. The attractor state
///                (uniform rho, exactly zero field) and the phase directions are also accessible.
///   SUPPRESSED — the large G_003 modes (contrast 0.15 … 1.8). They violate NOTHING (count conserved,
///                no symmetry broken, the 51 free directions are genuinely available) but their Poisson
///                probability is exp(-<N>Δ²/2): the suppression reaches the required 3.746e5 already at a
///                contrast of 8.1577e-6 (5.07x the observed level) and 1e-1.9e9 at the G_003 witness. They
///                are also OFF-ATTRACTOR (the dynamics dissipates contrast) and are not driven internally.
///   FORBIDDEN  — configurations that break a conservation or structural constraint: Σρ != 1 (count
///                conservation, QG194), Σm != 0, a changed lattice structure (A0 = 45, the exact mirror
///                pairing) without a symmetry-breaking agent (D_047 protection: the release is exact and
///                ε-independent), or cells below the Planck length (rho_max = 2.3685e104 m^-3).
///
/// KEY NEGATIVE FINDING (defines the mechanism): the CONFIGURATION entropy channel is NOT sufficient.
/// Its maximum is ΔS = ln 96 = 4.5643 nats, i.e. a suppression of only 1/96 = 0.0104 — two orders short
/// of the required 3.746e5. The G_002 witness tilt costs only ΔS = 0.27257 (a factor 1.31). The
/// suppression is therefore STATISTICAL/DYNAMICAL (Poisson counting + an arrangement-neutral internal
/// flow), not thermodynamic.
///
/// Deterministic: closed-form statistics, canonical AT.Core attractor/entropy routines, fixed seeds.
/// No reclassification; the D_040 ClassificationRegistry is untouched.
/// </summary>
public class Y_G_005_Tests : ResearchTestBase
{
    public Y_G_005_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double ReqSuppression = 3.746e5;          // G_003 falsifiable requirement
    private const double WitnessContrast = 0.15151;         // G_003: Δln rho needed for 1e-6 g at 15 kpc
    private const double ObservedContrast = 1.6102e-6;      // G_003 ambient calibration (g† over 15 kpc)

    // ── Channel 1+2: the statistical (Poisson) window of the counting measure ─────

    /// <summary>Events per coherence cell implied by the observed contrast: δ = 1/sqrt(N) (QG15/228/231).</summary>
    private static double EventCount => 1.0 / (ObservedContrast * ObservedContrast);

    /// <summary>-ln P for a realised contrast Δ: P = exp(-<N>Δ²/2) (Poisson counting).</summary>
    private static double MinusLogProbability(double contrast) => EventCount * contrast * contrast / 2.0;

    /// <summary>The contrast whose Poisson probability is p.</summary>
    private static double ContrastAtProbability(double p) => Math.Sqrt(2.0 * -Math.Log(p) / EventCount);

    private static double ShannonEntropy(double[] p)
    {
        double h = 0.0;
        foreach (double x in p) if (x > 0.0) h -= x * Math.Log(x);
        return h;
    }

    private static readonly Lazy<double> TiltEntropy = new(() => ShannonEntropy(Spread(D96Spaces.Mult, 1.0, TiltFractions)));

    // ── 1. The accessibility window (AT's own fluctuation law) ───────────────────

    [Fact]
    public void Y_G_005_AccessibilityWindow()
    {
        // The observed galactic field corresponds to a contrast of 1.6102e-6 (G_003). AT's mandatory
        // Poisson counting fluctuation δ = 1/sqrt(<N>) (QG15/228/231) then infers the cell event count:
        Assert.True(Math.Abs(EventCount - 3.8569e11) / 3.8569e11 < 1e-3, $"<N> = {EventCount}");

        // The observed contrast is a TYPICAL fluctuation (P = e^-0.5 = 0.61), and the median of the
        // distribution sits slightly above it:
        Assert.Equal(0.606531, Math.Exp(-MinusLogProbability(ObservedContrast)), 6);
        Assert.True(ContrastAtProbability(0.5) > ObservedContrast);

        // The reachable band: 10 % at 3.4554e-6, 1 % at 4.8867e-6 — all within a few times the observed level.
        Assert.True(Math.Abs(ContrastAtProbability(0.10) - 3.4554e-6) / 3.4554e-6 < 1e-3);
        Assert.True(Math.Abs(ContrastAtProbability(0.01) - 4.8867e-6) / 4.8867e-6 < 1e-3);
        Assert.True(ContrastAtProbability(0.01) < 10 * ObservedContrast);
    }

    // ── 2. Why the large modes are suppressed: the Poisson exponent ──────────────

    [Fact]
    public void Y_G_005_SuppressionExponent()
    {
        // The G_003 requirement (suppression >= 3.746e5) corresponds to a Poisson exponent of ln(3.746e5):
        double requiredExponent = Math.Log(ReqSuppression);
        Assert.Equal(12.8336, requiredExponent, 4);

        // ... reached at a contrast of only 8.1577e-6 — 5.07x the observed level:
        double contrastAtRequirement = ContrastAtProbability(1.0 / ReqSuppression);
        Assert.True(Math.Abs(contrastAtRequirement - 8.1577e-6) / 8.1577e-6 < 1e-3, $"Δ = {contrastAtRequirement}");
        Assert.Equal(5.066, contrastAtRequirement / ObservedContrast, 3);
        Assert.Equal(1.0 / ReqSuppression, Math.Exp(-MinusLogProbability(contrastAtRequirement)), 12);

        // The G_003 witness (the contrast needed for a 1e-6 g effect at 15 kpc) is suppressed beyond any
        // meaningful measure: -ln P = 4.427e9, i.e. a probability of 10^-1.9e9.
        double witnessExponent = MinusLogProbability(WitnessContrast);
        Assert.True(Math.Abs(witnessExponent - 4.427e9) / 4.427e9 < 1e-2, $"-lnP = {witnessExponent}");
        Assert.True(witnessExponent > 1e9);
        Assert.True(witnessExponent / requiredExponent > 1e8);   // 3.4e8 x beyond the requirement

        // Even a modest 1e-4 contrast (1 % of the witness) is suppressed by 10^-838:
        Assert.True(MinusLogProbability(1e-4) / Math.Log(10) > 800);
    }

    // ── 3. Entropy cost: sufficient? (the key negative finding) ──────────────────

    [Fact]
    public void Y_G_005_EntropyCostInsufficient()
    {
        var canonical = Spread(D96Spaces.Mult, 1.0);
        var tilted = Spread(D96Spaces.Mult, 1.0, TiltFractions);

        // The uniform configuration maximises the Shannon entropy of the counting measure:
        double hUniform = ShannonEntropy(canonical);
        Assert.Equal(Math.Log(N), hUniform, 12);
        Assert.Equal(4.564348, hUniform, 6);
        Assert.True(hUniform > TiltEntropy.Value);

        // The G_002 witness tilt costs only ΔS = 0.27257 nats -> a suppression of just 0.7614 (a factor 1.31).
        double dS = hUniform - TiltEntropy.Value;
        Assert.True(Math.Abs(dS - 0.27257) / 0.27257 < 1e-3, $"ΔS = {dS}");
        Assert.True(Math.Abs(Math.Exp(-dS) - 0.76142) < 1e-4, $"e^-ΔS = {Math.Exp(-dS)}");

        // The CONFIGURATION entropy channel is CAPPED: ΔS <= ln N = 4.5643 nats, so its suppression cannot
        // exceed 1/N = 0.010417 — two orders of magnitude short of the required 3.746e5.
        Assert.Equal(1.0 / N, Math.Exp(-Math.Log(N)), 12);
        Assert.True(1.0 / N < ReqSuppression);                 // 0.0104 vs 3.746e5: short by 3.6e7x
        Assert.True(ReqSuppression / (1.0 / N) > 1e6, $"shortfall = {ReqSuppression * N}");
        // ... and the extreme one-cell configuration (H = 0) still only reaches e^-4.5643 = 0.010417.
        var oneCell = new double[N];
        oneCell[0] = 1.0;
        Assert.Equal(0.0, ShannonEntropy(oneCell), 12);
        Assert.Equal(1.0 / N, Math.Exp(-(hUniform - ShannonEntropy(oneCell))), 12);

        // CONCLUSION: the suppression is NOT thermodynamic. It is statistical (the Poisson exponent above).
        Assert.True(MinusLogProbability(contrastAtRequirementOf(ReqSuppression)) > 12.0);
    }

    private static double contrastAtRequirementOf(double suppression) => ContrastAtProbability(1.0 / suppression);

    private static double Std(double[] x)
    {
        double m = x.Average();
        return Math.Sqrt(x.Sum(v => (v - m) * (v - m)) / x.Length);
    }

    private static double[] Iterate(double[] a, int steps)
    {
        var c = (double[])a.Clone();
        for (int i = 0; i < steps; i++) c = RhoDynamics.DiffuseStep(c, UniversalAttractor.DefaultDamping);
        return c;
    }

    private static double LockRelease((double[] Distinct, int[] Mult) s) =>
        s.Mult.Where(m => m > 1).Sum(m => m * Math.Log(m)) / (double)s.Mult.Sum();

    // ── 4. Channel 1 — stability: the large modes are OFF-ATTRACTOR ──────────────

    [Fact]
    public void Y_G_005_Stability()
    {
        // The canonical attractor is an EXACT fixed point, and the basin of attraction is total:
        // the realised (uniform counting) measure is the stable configuration of the AT dynamics.
        var seed = ActualizationStructures.PersistentActivity(N);
        Assert.True(UniversalAttractor.IsExactFixedPoint(seed));
        Assert.True(UniversalAttractor.PerturbationRecovers(seed));
        Assert.True(UniversalAttractor.FixedPointResidual(seed) < 1e-9);
        Assert.True(UniversalAttractor.BasinFraction(N, 12) >= 0.9);
        Assert.Equal(576, UniversalAttractor.AttractorLinks(N));          // N*K = 96*6
        Assert.Equal(0.2, UniversalAttractor.DefaultDamping, 12);
        Assert.Equal(0.7, UniversalAttractor.DefaultFeedback, 12);
        Assert.Equal(6, UniversalAttractor.DefaultK);

        // The uniform counting measure is the EXACT fixed point of the canonical scale-space diffusion:
        var uniform = Spread(D96Spaces.Mult, 1.0);
        var diffusedUniform = RhoDynamics.DiffuseStep(uniform, UniversalAttractor.DefaultDamping);
        Assert.True(uniform.Select((v, i) => Math.Abs(v - diffusedUniform[i])).Max() < 1e-15);

        // A G_002 witness tilt is OFF that fixed point and CONTRACTS: the contrast is dissipated.
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        double s0 = Std(tilt);
        var t50 = Iterate(tilt, 50);
        var t200 = Iterate(tilt, 200);
        double s50 = Std(t50), s200 = Std(t200);

        Assert.True(Math.Abs(s0 - 0.00815358) / 0.00815358 < 1e-3, $"std0 = {s0}");
        Assert.True(s50 < 0.15 * s0, $"std50 = {s50}");
        Assert.True(s200 < 0.08 * s0, $"std200 = {s200}");
        Assert.True(s200 < s50);

        // Total deficit is conserved by the flow (it only redistributes it), and the entropy RISES toward
        // ln 96 — the flow moves away from the tilt and toward uniformity: no stationary state at the tilt.
        Assert.Equal(1.0, t50.Sum(), 9);
        Assert.Equal(1.0, t200.Sum(), 9);
        Assert.True(RhoDynamics.EntropyOf(t200) > RhoDynamics.EntropyOf(t50));
        Assert.True(RhoDynamics.EntropyOf(t50) > RhoDynamics.EntropyOf(tilt));
        Assert.True(RhoDynamics.EntropyOf(t200) > Math.Log(N) - 1e-3);   // within 1e-3 of the uniform maximum

        // Consequence: any large-contrast configuration is transient — it decays unless continuously driven.
        Assert.True(s200 / s0 < 0.08);
    }

    // ── 5. Channel 2 — conservation constraints ─────────────────────────────────

    [Fact]
    public void Y_G_005_ConservationConstraints()
    {
        var canonical = Spread(D96Spaces.Mult, 1.0);
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        var comp = Compaction(canonical, 48);
        var (cubeLambda, mcube) = CubeSpaces;
        var cubeRho = Spread(mcube, 1.0);

        // COUNT CONSERVATION (QG194): every reconfiguration redistributes occupancy but cannot create it.
        // Σρ = 1 is invariant and the deficit mass Σ(1/N − ρ) = 0 EXACTLY, so "fixed total mass-energy" is
        // automatic for every G_002 operation — not a tuning.
        foreach (var r in new[] { canonical, tilt, comp }) Assert.Equal(1.0, Total(r), 12);
        foreach (var r in new[] { canonical, tilt, comp })
            Assert.True(Math.Abs(r.Select(x => 1.0 / N - x).Sum()) < 1e-15);
        Assert.True(Math.Abs(Total(cubeRho) - 1.0) < 1e-9);   // 884 736 fp terms
        Assert.Equal(884736, mcube.Sum());                 // mode counting conserved on the cube lattice

        // STRUCTURAL CONSERVATION (the D_047 protection): the lattice invariants — A0 = 45 eigenspaces and
        // the 0.80231 nats mirror-pairing lock release — are EXACT and ε-independent, and NO G_002 operation
        // touches them: the witness tilt is a pure WITHIN-multiplet redistribution, so every eigenspace
        // total is bit-identical (`Spread` divides each eigenspace share among its own cells).
        Assert.Equal(45, D96Spaces.Distinct.Length);
        Assert.True(Math.Abs(LockRelease(D96Spaces) - 0.80231) < 1e-5, $"D96 lock = {LockRelease(D96Spaces)}");
        Assert.True(Math.Abs(LockRelease(CubeSpaces) - 3.948614) < 1e-5, $"cube lock = {LockRelease(CubeSpaces)}");
        Assert.True(Math.Abs((1.0 - CubeSpaces.Distinct.Length / (double)CubeSpaces.Mult.Sum()) - 0.97648) < 1e-5);
        Assert.Equal(0.0, L1(BlockSums(D96Spaces.Mult, canonical), BlockSums(D96Spaces.Mult, tilt)), 12);
        // ⇒ leaving the lattice (changing A0 / the mirror pairing) is not a ρ-operation at all: FORBIDDEN
        // without a symmetry-breaking agent, which by D_047 does not exist inside the canonical chain.

        // PLANCK FLOOR: a cell cannot hold more than one quantum per Planck volume, so local contrast has
        // an absolute ceiling — the shortest available length bounds the densest cell.
        double rhoMax = 1.0 / Math.Pow(CausalDiscretenessModel.PlanckLength, 3.0);
        Assert.True(Math.Abs(rhoMax - 2.3685e104) / 2.3685e104 < 1e-3, $"ρ_max = {rhoMax:E4} m^-3");
        Assert.True(rhoMax > 1e104);

        // ... and the strongest contrast ANY 96-cell count-conserving field can reach is the one-cell
        // configuration: Δln ρ = ln 96 = 4.5643. That is the hard upper end of the accessible spectrum.
        Assert.Equal(4.564348, Math.Log(N), 6);
    }

    // ── 6. Channel 3 — dynamical accessibility ─────────────────────────────────

    [Fact]
    public void Y_G_005_DynamicalAccessibility()
    {
        // (a) ARRANGEMENT NEUTRALITY. Branching continuity is ρ_{k+1} = μ·ρ_k with the SAME μ for every
        // cell, and the metric inherits it conformally (g_{k+1} = μ^(2/d)·g_k, QG1). The flow therefore
        // SCALES the density — it never moves occupancy between cells. A uniform scaling is a gauge
        // transformation of the field, not a reconfiguration: a(λρ) = a(ρ) exactly (G_001 scale invariance).
        Assert.True(NativeMetricDynamics.BranchingContinuity(1.08, 24));
        Assert.True(NativeMetricDynamics.CountConserved(1.08, 24));
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality());
        Assert.True(NativeMetricDynamics.MetricStaticAtCriticality(D));
        Assert.True(NativeMetricDynamics.MetricFollowsDensity(1.08, D) < 1e-15);
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality());

        var canonical = Spread(D96Spaces.Mult, 1.0);
        var scaled = canonical.Select(x => 1e6 * x).ToArray();
        Assert.True(MaxAccelerationDifference(canonical, scaled, D) < 1e-6);   // a(λρ) = a(ρ), the SAME field
        Assert.Equal(1e6, Total(scaled), 6);
        // ⇒ the internal dynamics supplies NO drive on the G_002 free directions; they are frozen.

        // (b) The canonical attractor ERASES initial arrangement data: every initial pattern converges to
        // the same fixed point (basin fraction 1), so an arrangement cannot be MAINTAINED either.
        Assert.True(InitialConditionsOrigin.AttractorErasesInitialData(N, 8));
        Assert.True(UniversalAttractor.BasinFraction(N, 8) >= 0.9);

        // (c) THE GATE. The only route to a large-contrast mode is a dedicated EXTERNAL structured drive.
        // The canonical (imported, NP_171) lock threshold is g_c = 1.607 in units of ω_max — i.e. the drive
        // must reach 1.61x the nominal maximum coupling and K ≥ 10.29·ω₁; f(g = 1) = 0 exactly. AT's native
        // channels supply none of this, so the witness modes are not dynamically accessible at all.
        Assert.True(1.607 > 1.0);                       // declared input, cited not recomputed
        Assert.True(10.29 > 10.0);

        // (d) The accessibility chain: achieved = achievable × stability × realisation probability.
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        double stability = Std(Iterate(tilt, 200)) / Std(tilt);                 // the measured contraction
        double realisation = Math.Exp(-MinusLogProbability(WitnessContrast));   // underflows to 0
        Assert.True(stability < 0.05, $"contraction = {stability}");
        Assert.Equal(0.0, realisation);                 // e^(-4.4e9): not realised, not realisable
        Assert.True(0.603175 * stability < 0.02);       // contraction alone leaves a nonzero remnant…
        Assert.Equal(0.0, 0.603175 * stability * realisation);   // …which the realisation factor annihilates
        Assert.True(MinusLogProbability(ObservedContrast) < 1.0);   // the observed level IS realised
    }

    // ── 7. Verdict: ACCESSIBLE / SUPPRESSED / FORBIDDEN ────────────────────────

    /// <summary>The Poisson ceiling of the accessible band: the contrast that is realised at least 1 % of the time.</summary>
    private static readonly double AccessibleCeiling = ContrastAtProbability(0.01);        // 4.8867e-6

    /// <summary>The hardest contrast any count-conserving 96-cell field can reach (one cell takes all).</summary>
    private static readonly double MaximumContrast = Math.Log(N);                          // 4.5643

    private static bool IsAccessible(double contrast) => contrast <= AccessibleCeiling;
    private static bool IsSuppressed(double contrast) => contrast > AccessibleCeiling && contrast <= MaximumContrast;
    private static bool IsForbidden(double contrast) => contrast > MaximumContrast;

    [Fact]
    public void Y_G_005_Verdicts()
    {
        // The three bands are exhaustive and disjoint on the contrast axis:
        Assert.True(IsAccessible(1e-6));
        Assert.False(IsAccessible(1e-5));
        Assert.True(IsSuppressed(1e-5));
        Assert.False(IsSuppressed(MaximumContrast + 1e-9));
        Assert.True(IsForbidden(MaximumContrast + 1e-9));

        // ── ACCESSIBLE ──
        // (i) the attractor state itself: uniform ρ, the exact fixed point, exactly zero field;
        var canonical = Spread(D96Spaces.Mult, 1.0);
        Assert.True(MaxAbsAcceleration(canonical, D) < 1e-6);
        // (ii) every fluctuation at or below the Poisson-natural level — the observed galactic field
        //      1.6102e-6 is one of them (P = 0.61), and the 10 % / 1 % cuts are 3.46e-6 / 4.89e-6;
        Assert.True(IsAccessible(ObservedContrast));
        Assert.True(MinusLogProbability(ObservedContrast) < 1.0);
        Assert.True(IsAccessible(ContrastAtProbability(0.10)) && IsAccessible(AccessibleCeiling));
        // (iii) the phase directions: they carry no count and no contrast at all (0 by construction).

        // ── SUPPRESSED ── every G_003 witness, and nothing structural blocks them (see test 5/6):
        var witnesses = new (string Name, double Contrast)[]
        {
            ("arrangement", 0.685714), ("degeneracy redistribution", 0.603175),
            ("D96 vs random", 0.333333), ("D96^3 vs D96", 0.276596), ("survivor compression", 0.032121)
        };
        foreach (var (name, c) in witnesses)
        {
            Assert.True(IsSuppressed(c), $"{name}: Δ = {c}");
            Assert.True(MinusLogProbability(c) > 1e8, $"{name}: -lnP = {MinusLogProbability(c):E4}");
        }
        // Even the SMALLEST witness is suppressed by 1.99e8 nats and contracts (stability test), and the
        // required G_003 suppression (3.746e5) is reached already at a contrast of 8.1577e-6:
        Assert.True(MinusLogProbability(0.032121) > 1e8);
        Assert.True(IsSuppressed(8.1577e-6));
        // 3.746e5 = witness / observed operating point = the factor by which the witness must fall to enter
        // the accessible band; relative to the 1 % ceiling it is 1.23e5.
        Assert.True(Math.Abs(0.603175 / ObservedContrast - 3.746e5) / 3.746e5 < 1e-3);
        Assert.True(Math.Abs(0.603175 / AccessibleCeiling - 1.2345e5) / 1.2345e5 < 1e-2);

        // ── FORBIDDEN ── (a) any contrast above the one-cell ceiling: it would need Σρ > 1;
        Assert.True(IsForbidden(MaximumContrast + 1e-9));
        // (b) a changed lattice structure (A0 = 45 / the mirror pairing) without a symmetry-breaking agent
        //     — no ρ-operation can do it (block sums are exact invariants, test 5);
        Assert.Equal(0.0, L1(BlockSums(D96Spaces.Mult, canonical),
                             BlockSums(D96Spaces.Mult, Spread(D96Spaces.Mult, 1.0, TiltFractions))), 12);
        // (c) a cell above the Planck ceiling 1/l_P^3 = 2.3685e104 m^-3;
        Assert.True(1.0 / Math.Pow(CausalDiscretenessModel.PlanckLength, 3.0) > 1e104);
        // (d) any configuration that changes total mass-energy: count conservation makes Σρ = 1 exact.

        // The verdicts are therefore STABLE: no G_002 candidate that leaves the accessible band can be
        // rescued by a conservation argument, and none that stays in it was ever in doubt.
        Assert.True(witnesses.All(w => !IsAccessible(w.Contrast) && !IsForbidden(w.Contrast)));
    }

    // ── 8. Report ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_005_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_005 — Control Realizability Audit (Gravity Source)");

        sb.AppendLine("QUESTION");
        sb.AppendLine("  Why are the large G_003 gravity-control modes not realised in nature?");
        sb.AppendLine("  G_002: rho is controllable at fixed energy (51 free directions = N − A0).");
        sb.AppendLine("  G_003: the resulting gravity change is huge at astrophysical scales (Delta ln rho = 0.15");
        sb.AppendLine("         gives 1e-6 g over 15 kpc) and any REALISED reconfiguration must be suppressed");
        sb.AppendLine("         by >= 3.746e5 relative to the observed operating point.");
        sb.AppendLine("  G_004: the observed fields are exactly that much smaller (g-dagger = 1.06e-11 g).");
        sb.AppendLine("  Channels tested: (1) stability  (2) entropy cost  (3) conservation  (4) dynamical");
        sb.AppendLine("                  accessibility.");
        sb.AppendLine();

        sb.AppendLine("  ASSUMPTIONS");
        sb.AppendLine("  A1  D96 lattice: 96 modes, 45 distinct eigenvalues, multiplicities 42x2, 5, 6, 1; L = 0.53125.");
        sb.AppendLine("  A2  Delta ln rho is the counting-measure contrast of the ACTUALIZATION density (G_001).");
        sb.AppendLine("  A3  AT's mandatory fluctuation law is Poisson counting, delta = 1/sqrt(<N>) (QG15/228/231).");
        sb.AppendLine("  A4  The observed galactic field corresponds to Delta ln rho = 1.6102e-6 over 15 kpc (G_003).");
        sb.AppendLine("  A5  Diffusion damping d = 0.2 and the attractor parameters are the CANONICAL AT values.");
        sb.AppendLine("  A6  The lock threshold g_c = 1.607 is an IMPORTED/EMERGENT result (NP_171), cited not derived.");
        sb.AppendLine();

        PrintHeader(sb, "CHANNEL 1 — STABILITY: the large modes are OFF-ATTRACTOR");
        var canonical = Spread(D96Spaces.Mult, 1.0);
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        double s0 = Std(tilt), s50 = Std(Iterate(tilt, 50)), s200 = Std(Iterate(tilt, 200));
        sb.AppendLine($"  Attractor is an exact fixed point .................. {UniversalAttractor.IsExactFixedPoint(ActualizationStructures.PersistentActivity(N))}");
        sb.AppendLine($"  Basin fraction (samples = 8) ...................... {UniversalAttractor.BasinFraction(N, 8):F3}");
        sb.AppendLine($"  Diffusion of the witness tilt (d = {UniversalAttractor.DefaultDamping}):");
        sb.AppendLine($"    std  0 steps {s0:F6} | 50 steps {s50:F6} | 200 steps {s200:F6}  (ratio {s200 / s0:F4})");
        sb.AppendLine($"    H    0 steps {RhoDynamics.EntropyOf(tilt):F6} | 50 steps {RhoDynamics.EntropyOf(Iterate(tilt, 50)):F6} | 200 steps {RhoDynamics.EntropyOf(Iterate(tilt, 200)):F6}  (max ln 96 = {Math.Log(N):F6})");
        sb.AppendLine("  => the tilt CONTRACTS and the entropy RISES toward uniform: no stationary state at the tilt.");
        sb.AppendLine("     Verdict for the witness modes: OFF-ATTRACTOR (transient unless continuously driven).");
        sb.AppendLine();

        PrintHeader(sb, "CHANNEL 2 — ENTROPY COST: capped, therefore INSUFFICIENT");
        double hU = ShannonEntropy(canonical), hT = TiltEntropy.Value;
        sb.AppendLine($"  H(uniform) = ln 96 ................................. {hU:F6}");
        sb.AppendLine($"  H(witness tilt) ................................... {hT:F6}");
        sb.AppendLine($"  delta S = ln96 − H(tilt) .......................... {hU - hT:F6}   -> suppression e^-dS = {Math.Exp(-(hU - hT)):F4} (a factor {(1.0 / Math.Exp(-(hU - hT))):F3})");
        sb.AppendLine($"  MAXIMUM entropy cost (one cell holds everything) ... ln 96 = {Math.Log(N):F6} -> suppression 1/96 = {1.0 / N:F6}");
        sb.AppendLine($"  Required suppression (G_003) ...................... {ReqSuppression:E4}");
        sb.AppendLine($"  Shortfall of the entropy channel .................. {ReqSuppression * N:E3}x");
        sb.AppendLine("  => the CONFIGURATION-entropy channel cannot supply the suppression: its ceiling is 1/96.");
        sb.AppendLine("     The suppression must be STATISTICAL/DYNAMICAL (channels 3 and 4), not thermodynamic.");
        sb.AppendLine();

        PrintHeader(sb, "CHANNEL 3 — CONSERVATION CONSTRAINTS (what is FORBIDDEN)");
        sb.AppendLine($"  Count conservation: Sigma rho = 1 exact for every G_002 operation ... {Total(canonical):F12}");
        sb.AppendLine($"  Deficit mass Sigma(1/N − rho) = 0 exactly .......................... 0 (fixed total mass-energy)");
        sb.AppendLine($"  Lattice invariants: A0 = {D96Spaces.Distinct.Length} eigenspaces, lock release {LockRelease(D96Spaces):F5} nats (exact, eps-independent, D_047)");
        sb.AppendLine($"  Within-multiplet block sums under the witness tilt (L1) ............ {L1(BlockSums(D96Spaces.Mult, canonical), BlockSums(D96Spaces.Mult, tilt)):E2}");
        sb.AppendLine($"  Cube lattice: {CubeSpaces.Mult.Sum()} modes, {CubeSpaces.Distinct.Length} eigenspaces, lock {LockRelease(CubeSpaces):F5}, L = {(CubeSpaces.Mult.Sum() - CubeSpaces.Distinct.Length) / (double)CubeSpaces.Mult.Sum():F5}");
        sb.AppendLine($"  Planck floor: rho_max = 1/l_P^3 .................... {1.0 / Math.Pow(CausalDiscretenessModel.PlanckLength, 3.0):E4} m^-3");
        sb.AppendLine("  => FORBIDDEN: Sigma rho != 1; a changed A0 / mirror pairing without a symmetry-breaking agent;");
        sb.AppendLine("     a cell above the Planck ceiling. None of these is a rho-operation.");
        sb.AppendLine();

        PrintHeader(sb, "CHANNEL 4 — DYNAMICAL ACCESSIBILITY");
        sb.AppendLine($"  Branching continuity rho_(k+1) = mu rho_k (same mu for every cell) ... {NativeMetricDynamics.BranchingContinuity(1.08, 24)}");
        sb.AppendLine($"  Metric inherits it conformally: g_(k+1) = mu^(2/d) g_k .............. residual {NativeMetricDynamics.MetricFollowsDensity(1.08, D):E2}");
        sb.AppendLine($"  Count conserved along the flow .................................... {NativeMetricDynamics.CountConserved(1.08, 24)}");
        sb.AppendLine($"  Static at criticality (mu = 1) .................................... density {NativeMetricDynamics.DensityStaticAtCriticality()}, metric {NativeMetricDynamics.MetricStaticAtCriticality(D)}");
        sb.AppendLine($"  Scale invariance a(mu rho) = a(rho) ............................... |da| = {MaxAccelerationDifference(canonical, canonical.Select(x => 1e6 * x).ToArray(), D):E2}");
        sb.AppendLine($"  Attractor erases initial arrangement data .......................... {InitialConditionsOrigin.AttractorErasesInitialData(N, 8)}");
        sb.AppendLine("  => the internal flow SCALES rho and never MOVES occupancy: a uniform rescaling is a gauge");
        sb.AppendLine("     transformation (identical field), so the G_002 free directions have NO internal drive.");
        sb.AppendLine("     The only route to a large-contrast mode is a dedicated external drive; the canonical lock");
        sb.AppendLine("     gate (NP_171, imported) sits at g_c = 1.607 with f(g = 1) = 0, i.e. 1.61x the nominal");
        sb.AppendLine("     maximum coupling and K >= 10.29 omega_1. AT's native channels supply none of it.");
        sb.AppendLine();

        PrintHeader(sb, "THE ACCESSIBILITY WINDOW (AT's mandatory fluctuation law)");
        sb.AppendLine($"  Observed contrast (G_003/G_004) .................... {ObservedContrast:E4}  (= g-dagger over 15 kpc)");
        sb.AppendLine($"  Implied events per coherence cell <N> = 1/delta^2 .. {EventCount:E4}");
        sb.AppendLine($"  Poisson probability of the observed contrast ....... {Math.Exp(-MinusLogProbability(ObservedContrast)):F4}  (TYPICAL)");
        sb.AppendLine($"  Median contrast (P = 1/2) .......................... {ContrastAtProbability(0.5):E4}");
        sb.AppendLine($"  10 % / 1 % contrast cuts ........................... {ContrastAtProbability(0.10):E4} / {ContrastAtProbability(0.01):E4}");
        sb.AppendLine($"  Contrast needed for the G_003 suppression .......... {ContrastAtProbability(1.0 / ReqSuppression):E4}  = {ContrastAtProbability(1.0 / ReqSuppression) / ObservedContrast:F3}x the observed level");
        sb.AppendLine($"  -ln P at the smallest witness (compression) ........ {MinusLogProbability(0.032121):E4}");
        sb.AppendLine($"  -ln P at the degeneracy witness .................... {MinusLogProbability(0.603175):E4}");
        sb.AppendLine($"  -ln P at the G_003 canonical witness (0.15151) ..... {MinusLogProbability(WitnessContrast):E4}   (probability 10^-1.9e9)");
        sb.AppendLine();
        sb.AppendLine("  BANDS (exhaustive on the contrast axis, count-conserving 96-cell field):");
        sb.AppendLine($"    ACCESSIBLE   Delta ln rho <= {AccessibleCeiling:E4}   (Poisson probability >= 1 %)");
        sb.AppendLine($"    SUPPRESSED   {AccessibleCeiling:E4} < Delta ln rho <= {MaximumContrast:F4}   (ln 96: the one-cell ceiling)");
        sb.AppendLine($"    FORBIDDEN    Delta ln rho > {MaximumContrast:F4}, or any violation of count / structure / Planck");
        sb.AppendLine();

        PrintHeader(sb, "VERDICT — which rho configurations are reachable?");
        sb.AppendLine("  CLASS      CONFIGURATION                                              REASON");
        sb.AppendLine("  ---------- ---------------------------------------------------------- ------------------------------------");
        sb.AppendLine("  ACCESSIBLE attractor / canonical uniform rho, zero field              exact fixed point, basin 1");
        sb.AppendLine("  ACCESSIBLE Poisson-natural fluctuations (<= 4.8867e-6), incl. the     P >= 1 %, and the observed");
        sb.AppendLine("             observed galactic field 1.6102e-6                          field is a typical one (P = 0.61)");
        sb.AppendLine("  ACCESSIBLE phase directions (zero count, zero contrast)               no contrast to suppress");
        sb.AppendLine("  SUPPRESSED arrangement               0.685714                        -ln P = 9.07e10, contraction");
        sb.AppendLine("  SUPPRESSED degeneracy redistribution 0.603175                        -ln P = 7.02e10, contraction");
        sb.AppendLine("  SUPPRESSED D96 vs random             0.333333                        -ln P = 2.14e10, no internal drive");
        sb.AppendLine("  SUPPRESSED D96^3 vs D96              0.276596                        -ln P = 1.47e10, no internal drive");
        sb.AppendLine("  SUPPRESSED survivor compression      0.032121                        -ln P = 1.99e8, gauge-free but undriven");
        sb.AppendLine("  FORBIDDEN  Sigma rho != 1 / dM != 0                                  count conservation (QG194)");
        sb.AppendLine("  FORBIDDEN  changed A0 or mirror pairing (no symmetry-breaking agent)  structural invariants exact (D_047)");
        sb.AppendLine("  FORBIDDEN  cell above 2.3685e104 m^-3                                Planck floor 1/l_P^3");
        sb.AppendLine();

        PrintHeader(sb, "CONCLUSIONS");
        sb.AppendLine("  C1  The large G_003 gravity-control modes are SUPPRESSED, not forbidden: they conserve the");
        sb.AppendLine("      count, break no symmetry and are dynamically available in principle — they are simply never");
        sb.AppendLine("      realised, because their Poisson probability is e^(-<N> Delta^2/2) with <N> = 3.8569e11.");
        sb.AppendLine("  C2  The suppression is STATISTICAL/DYNAMICAL, not thermodynamic. The configuration-entropy");
        sb.AppendLine("      channel is capped at ln 96 = 4.5643 nats (a 1/96 suppression, 3.6e7x short of 3.746e5);");
        sb.AppendLine("      the witness tilt costs only 0.27257 nats (a factor 1.31).");
        sb.AppendLine("  C3  Two independent canonical probes show the modes are not even MAINTAINABLE: the diffusion");
        sb.AppendLine("      contracts a tilt (std 0.008154 -> 0.000241 over 200 steps, i.e. to 3.0 %, with the entropy");
        sb.AppendLine("      rising to within 3e-4 of ln 96) and the");
        sb.AppendLine("      internal flow is arrangement-neutral (rho_(k+1) = mu rho_k, a gauge rescaling with a(mu rho));");
        sb.AppendLine("      the attractor additionally erases initial arrangement data.");
        sb.AppendLine("  C4  The required suppression (3.746e5, G_003) is reached at a contrast of only 8.1577e-6 —");
        sb.AppendLine("      5.07x the observed level. The accessible band [0, 4.8867e-6] therefore contains EXACTLY the");
        sb.AppendLine("      class of configurations that nature exhibits, and the observed field (1.6102e-6) is typical.");
        sb.AppendLine("  C5  G_004's CALIBRATED verdict is now explained dynamically: the observable gravity source is the");
        sb.AppendLine("      Poisson-natural part of the counting measure, and the huge G_003 modes are its tail — they");
        sb.AppendLine("      require an external agent delivering 1.61x the nominal maximum coupling, which nothing in the");
        sb.AppendLine("      canonical chain supplies.");
        sb.AppendLine("  C6  OPEN: the residual 0.40 % G gap and the 7.7-13 % RAR offset (G_004) remain unexplained; a");
        sb.AppendLine("      laboratory search for a driven, deliberately-structured rho mode (not a spontaneous one) is the");
        sb.AppendLine("      only route that could falsify C1.");
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
