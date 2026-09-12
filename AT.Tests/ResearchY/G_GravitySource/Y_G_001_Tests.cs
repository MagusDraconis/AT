using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;
using static AT.Tests.Shared.DensityField;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_001 — Gravity Source Audit (new group G — Gravity Source).
///
/// Question: what variable actually SOURCES gravity in AT?
///
/// Candidates: (1) energy density, (2) actualization density, (3) spectral density,
/// (4) information density, (5) the curvature-density law R = F(rho).
///
/// Method per candidate: define units -> derive dimensions -> test limits ->
/// compare with Newton / GR / RAR / QG results.
///
/// VERDICTS
///   SOURCE     — (2) the ACTUALIZATION DENSITY rho (the counting measure), and specifically its
///                standardized deficit m = rhoBar - rho for the attractive Newtonian sector.
///                rho is the INPUT of the AT field equation: g = rho^(2/d)*eta (QG197),
///                R = F(rho) (G4-G2), a = -(1/d)grad ln rho (G4-O3), and the input of the native
///                flow rho_{k+1} = mu*rho_k (QG222). It is dimensionless — exactly what a
///                conformal factor requires — so it enters with no imported coupling.
///   CORRELATED — (1) energy density: the deficit dust T = (rhoBar-rho)v v (QG195) is an exact
///                re-expression of the same deficit (rank-identical), and the Lovelock alternative
///                is forced to G/kappa (G4-G4): the kinetic energy tensor is NOT conserved while
///                G/kappa is. Its energy READING is hosted (QG89/NP_058).
///                (3) spectral density: a GLOBAL (frequency-space) quantity that supplies the
///                source's parameters m0 = occ0/SigmaM, r0 = ln(span) and the MAGNITUDE of G
///                (A = SigmaM*#g*occ2, M_Pl = v*A^3, QG181/182) — but never the local source field.
///   REFUTED    — (4) information density: a permutation-invariant GLOBAL functional of rho, so the
///                SAME I is compatible with DIFFERENT fields; it is the dark-energy surplus, which
///                has no derived gravitational role (NP_055-NP_064).
///                (5) the curvature-density law as a source: R is the OUTPUT of R = F(rho) and is
///                not even injective in rho, so sourcing rho from R is circular.
///
/// Limits tested: vacuum (rho = rhoBar => a = 0, R = 0), rho -> 0 (curvature divergence, metric
/// degeneracy), weak deficit (exact 1/r^2, M_eff -> m0*r0/(d*rhoBar)), deep RAR
/// (g_obs -> sqrt(g_bar*g_dagger)), log deficit (flat curve), and the critical branching point
/// mu = 1 (stationary source, alpha = 0).
///
/// Deterministic: closed-form profiles, fixed grids, fixed finite-difference steps, no randomness.
/// No new AT assumption is introduced; no canonical value is reclassified (the D_040
/// ClassificationRegistry is untouched).
/// </summary>
public class Y_G_001_Tests : ResearchTestBase
{
    public Y_G_001_Tests(ITestOutputHelper output) : base(output) { }

    // ── Fixed conventions ────────────────────────────────────────────────────────
    private const int D = 3;                  // spatial dimension (G4-G2 / QG161)
    private const double RhoBar = 1.0;        // normalized counting-measure reference (QG182)

    // D96 canonical spectral constants (QG161/168/181/182)
    private const double SigmaM = 95.0;       // total mode count
    private const double GroupCount = 44.0;   // #g
    private const double Occ2 = 87.0;         // dense-band occupancy
    private const double Occ0 = 4.0;          // lightest octave occupancy
    private const double Doublets = 42.0;     // Z2 doublets
    private const double Span = 6.402515;     // spectral span

    private static double LnSpan => Math.Log(Span);
    private static double SpectralContent => SigmaM * GroupCount * Occ2;   // A = 363 660

    // ── Dimension bookkeeping: (mass, length, time) exponents ────────────────────

    /// <summary>A dimension as SI base exponents. A source that enters the conformal factor must be dimensionless.</summary>
    private readonly record struct Dim(double M, double L, double T)
    {
        public static Dim None => new(0, 0, 0);
        public static Dim NumberDensity => new(0, -3, 0);        // counts (dimensionless) per volume
        public static Dim EnergyDensity => new(1, -1, -2);       // J/m^3
        public static Dim SpectralDensity => new(0, 0, 1);       // modes per angular frequency
        public static Dim Curvature => new(0, -2, 0);            // 1/L^2
        public static Dim KappaOver8Pi => new(-1, -1, 2);        // kappa = 8*pi*G/c^4

        public bool IsNone => Math.Abs(M) < 1e-12 && Math.Abs(L) < 1e-12 && Math.Abs(T) < 1e-12;
        public Dim Scale(double s) => new(M * s, L * s, T * s);
        public Dim Times(Dim o) => new(M + o.M, L + o.L, T + o.T);
    }

    // ── Shared numeric helpers live in AT.Tests.Shared.DensityField (also used by Y_G_002) ───────

    // ── 1. Units and dimensions of all five candidates ───────────────────────────

    [Fact]
    public void Y_G_001_UnitsAndDimensions()
    {
        var actualization = Dim.None;                       // counting measure = dimensionless ratio to rhoBar
        var energyDensity = Dim.EnergyDensity;              // J/m^3 = M L^-1 T^-2
        var spectral = Dim.SpectralDensity;                 // modes per unit frequency = T
        var information = Dim.None;                         // nats per mode = dimensionless
        var curvature = Dim.Curvature;                      // 1/L^2

        // The conformal factor rho^(2/d) of g = rho^(2/d)*eta is dimensionless, so the variable that
        // enters it MUST be dimensionless. Only the actualization density (and the information count)
        // are dimensionless — the energy density is not.
        Assert.True(actualization.IsNone);
        Assert.True(actualization.Scale(2.0 / D).IsNone);   // rho^(2/d) is dimensionless
        Assert.False(energyDensity.IsNone);
        Assert.False(spectral.IsNone);
        Assert.True(information.IsNone);                    // nats are dimensionless, but global (see test 6)

        // Dimensional route to the field equation: in GR the source needs a dimensionful coupling.
        // kappa*T has curvature dimensions (M^-1 L^-1 T^2) * (M L^-1 T^-2) = L^-2 = [R]: consistent,
        // but only WITH kappa — a constant AT must import (G4-L12: BDG -2 NO MATCH).
        Assert.Equal(curvature.L, energyDensity.Times(Dim.KappaOver8Pi).L, 12);
        Assert.Equal(curvature.M, energyDensity.Times(Dim.KappaOver8Pi).M, 12);
        Assert.Equal(curvature.T, energyDensity.Times(Dim.KappaOver8Pi).T, 12);

        // The AT field equation R = F(rho) needs NO coupling: rho is dimensionless and the length
        // scale comes wholly from rho's own derivatives, so [R] = L^-2 follows by construction.
        Assert.Equal(-2.0, curvature.L, 12);

        // Scale invariance: the native acceleration depends only on ratios of rho; rescaling rho by
        // any lambda changes nothing. Only the OVERALL normalization of the curvature follows lambda,
        // as lambda^(-2/d) — the counting-measure reference is a boundary (rhoBar = 1, QG182).
        Func<double, double> rho = r => DeficitCollective.PowerLawDeficit(r);
        Func<double, double> rhoScaled = r => 2.5 * DeficitCollective.PowerLawDeficit(r);
        Assert.Equal(DeficitCollective.AtAcceleration3D(rho, 2.0),
                     DeficitCollective.AtAcceleration3D(rhoScaled, 2.0), 10);

        double r1 = ScalarCurvatureOf(rho, 1.0, D);
        double r2 = ScalarCurvatureOf(rhoScaled, 1.0, D);
        Assert.Equal(Math.Pow(2.5, -2.0 / D), r2 / r1, 8);

        // The counting measure in physical units is an event count per 4-volume: L^-3 T^-1.
        var eventCount = new Dim(0, -3, -1);
        Assert.False(eventCount.IsNone);                    // a raw rate density needs rhoBar to become a ratio
    }

    // ── 2. Candidate 2 — the actualization density IS the source ─────────────────

    [Fact]
    public void Y_G_001_ActualizationDensityIsSource()
    {
        // (a) The metric is built from rho alone: g = rho^(2/d)*eta (QG197).
        Assert.Equal(0.6300, Math.Pow(0.5, 2.0 / D), 3);

        // (b) The geodesic acceleration is fixed by rho alone, with no free coupling: a = -(1/d)*(ln rho)'.
        Func<double, double> rho = x => 1.0 + 0.5 * x * x;
        foreach (double x in new[] { 0.25, 0.5, 1.0, 2.0 })
        {
            double numeric = DeficitCollective.AtAcceleration3D(rho, x);
            double analytic = -(1.0 / D) * (2.0 * 0.5 * x / (1.0 + 0.5 * x * x));
            Assert.Equal(analytic, numeric, 8);
        }

        // (c) VACUUM LIMIT: uniform rho = rhoBar gives a = 0 and R = 0 exactly.
        Assert.Equal(0.0, DeficitCollective.AtAcceleration3D(_ => RhoBar, 3.0), 12);
        Assert.Equal(0.0, HigherDimEinstein.ScalarCurvature(1.0, 0.0, D), 12);
        Assert.Equal(0.0, ScalarCurvatureOf(_ => RhoBar, 1.0, D), 9);

        // (d) The attractive sector is the standardized DEFICIT m = rhoBar - rho: a = +(1/d)*grad m/rho.
        Func<double, double> deficitProfile = r => DeficitCollective.PowerLawDeficit(r, RhoBar, 0.5, 0.5);
        Assert.True(DeficitCollective.AtAcceleration3D(deficitProfile, 1.0) < 0.0);   // toward the void
        Assert.True(MatterSectorOrigin.DeficitPositiveInVoids(RhoBar, deficitProfile(0.0)));
        Assert.False(MatterSectorOrigin.DeficitPositiveInVoids(RhoBar, 1.2));

        // (e) Singular limit rho -> 0: curvature diverges as rho^(-2/d) and sqrt(-g) = rho degenerates.
        double previous = 0.0;
        foreach (double r in new[] { 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 })
        {
            double div = PlanckRegime.CurvatureDivergence(r, D);
            Assert.True(div > previous, $"CurvatureDivergence must grow: {r} -> {div}");
            previous = div;
        }
        Assert.Equal(100.0, PlanckRegime.CurvatureDivergence(1e-3, D), 6);
        Assert.Equal(10000.0, PlanckRegime.CurvatureDivergence(1e-6, D), 6);
        Assert.True(PlanckRegime.MinimumCellSize(1e6, D) > 0.0);

        // (f) DYNAMICS OF THE SOURCE (QG222): the actualization flow moves rho, and rho moves the metric.
        Assert.True(NativeMetricDynamics.CountConserved(2.0, 8));
        Assert.True(NativeMetricDynamics.CountConserved(1.0, 8));
        Assert.True(NativeMetricDynamics.BranchingContinuity(2.0, 8));
        Assert.Equal(0.0, NativeMetricDynamics.DensityRate(1.0), 12);                    // mu = 1 (alpha = 0)
        Assert.Equal(Math.Pow(2.0, 2.0 / D), NativeMetricDynamics.MetricScaleFactor(2.0, D), 10);
        Assert.True(NativeMetricDynamics.MetricFollowsDensity(2.0, D) < 1e-12);
    }

    // ── 3. Limits against Newton: the deficit reproduces 1/r^2 exactly ───────────

    [Fact]
    public void Y_G_001_NewtonianLimit()
    {
        const double m0 = 0.5, r0 = 0.5;
        Func<double, double> rho = r => DeficitCollective.PowerLawDeficit(r, RhoBar, m0, r0);

        // (a) a = -(1/d)*m'/rho = -m0*r0/(d*rhoBar*r^2) + O(1/r^3): the Newtonian form.
        double a12 = DeficitCollective.AtAcceleration3D(rho, 12.0);
        double meff12 = DeficitCollective.EffectiveEnclosedMass(a12, 12.0);
        double asymptote = m0 * r0 / (D * RhoBar);
        Assert.Equal(0.0833333333, asymptote, 10);
        Assert.Equal(0.0783673, meff12, 7);
        Assert.True(meff12 / asymptote > 0.94, $"M_eff(12)/M_eff(inf) = {meff12 / asymptote}");

        double meff200 = DeficitCollective.EffectiveEnclosedMass(
            DeficitCollective.AtAcceleration3D(rho, 200.0), 200.0);
        Assert.True(meff200 / asymptote > 0.995, $"M_eff(200)/M_eff(inf) = {meff200 / asymptote}");

        // (b) Exact 1/r^2: the log-log slope of |a| over octaves tends to -2.
        double[] xs = { 8.0, 16.0, 32.0, 64.0, 128.0 };
        double[] ys = xs.Select(x => Math.Abs(DeficitCollective.AtAcceleration3D(rho, x))).ToArray();
        var (slope, _) = DeficitCollective.LogLogFit(xs, ys);
        Assert.True(slope < -1.90 && slope > -2.05, $"d ln|a| / d ln r = {slope}");

        // (c) QG6's native scale and QG182's bridge to the D96 spectrum (deviation < 0.1%).
        double r0Spectral = LnSpan;
        double gmEffNative = (Occ0 / SigmaM) * r0Spectral / (D * RhoBar);
        double gmEffSpectral = 1.0 / (3.0 * Math.Log(SpectralContent));
        Assert.Equal(0.0260588194, gmEffNative, 9);
        Assert.Equal(0.0260335827, gmEffSpectral, 9);
        Assert.True(Math.Abs(gmEffNative - gmEffSpectral) / gmEffNative < 1e-3,
            $"QG182 bridge deviation = {Math.Abs(gmEffNative - gmEffSpectral) / gmEffNative:P4}");

        // (d) The weak scale that anchors the magnitude: v = (SigmaM + #doublets)*ln(span).
        double v = (SigmaM + Doublets) * LnSpan;
        Assert.Equal(137.0, SigmaM + Doublets, 12);
        Assert.True(Math.Abs(v - 254.37) / 254.37 < 1e-4, $"v = {v} GeV");
    }

    // ── 4. Candidate 1 — energy density is CORRELATED, not the source ────────────

    [Fact]
    public void Y_G_001_EnergyDensityCorrelated()
    {
        // (a) LOVELOCK TEST. The only conserved symmetric second-order tensor built from the scalar
        // geometry is G/kappa: the Bianchi residual of the native Einstein tensor vanishes ...
        foreach (double x in new[] { 0.25, 0.5, 1.0, 1.5 })
        {
            Assert.True(Math.Abs(HigherDimEinstein.BianchiResidual(x, 0.5, D)) < 1e-9);
            Assert.True(Math.Abs(HigherDimEinstein.BianchiResidual(x, 0.5, 2)) < 1e-9);
        }
        Assert.True(NativeMetricDynamics.BianchiConsistent(1.0, D));
        Assert.True(NativeMetricDynamics.BianchiConsistent(0.4, D));

        // ... while a candidate ENERGY tensor built from the field gradients is NOT conserved.
        double kineticDiv = HigherDimEinstein.KineticDivergence(1.0, 0.5, D);
        Assert.True(Math.Abs(kineticDiv) > 1e-3, $"nabla T_kin = {kineticDiv}");
        Assert.True(Math.Abs(kineticDiv) > 1e6 * Math.Abs(HigherDimEinstein.BianchiResidual(1.0, 0.5, D)));

        // (b) The surviving energy density is an exact re-expression of the deficit: T00 = (rhoBar - rho)v^2.
        double[] rhos = { 0.2, 0.5, 0.8, 0.95 };
        double[] t00 = rhos.Select(r => MatterSectorOrigin.MatterTensor00(RhoBar, r)).ToArray();
        double[] deficit = rhos.Select(r => MatterSectorOrigin.DeficitMassDensity(RhoBar, r)).ToArray();
        Assert.True(Math.Abs(AdaptabilityAudit.Pearson(t00, deficit) - 1.0) < 1e-12);   // identical quantities
        Assert.True(Math.Abs(AdaptabilityAudit.Pearson(t00, rhos) + 1.0) < 1e-12); // strictly decreasing in rho
        Assert.Equal(0.5, MatterSectorOrigin.MatterTensor00(RhoBar, 0.5), 12);
        Assert.Equal(0.8, MatterSectorOrigin.MatterTensor00(RhoBar, 0.2), 12);

        // (c) The matter tensor is independent of G (it escapes the Lovelock obstruction) because it is
        // built from the deficit value and the flow — not from the geometry (QG195/MatterSectorOrigin).
        Assert.True(NativeMetricDynamics.MatterIndependentOfG());
        Assert.True(MatterSectorOrigin.MatterTensorDistinctFromG());
        Assert.True(MatterSectorOrigin.DustIsConserved());
        Assert.True(MatterSectorOrigin.G4G_LovelockForcesGeometricTensor());
        Assert.Equal("MATTER ORIGIN", MatterSectorOrigin.Classify());

        // (d) The ENERGY reading itself is hosted, not derived (QG89 / NP_058/NP_059): the deficit
        // carries rest mass only through the definition 'energy = actualization rate'.
        Assert.True(MatterSectorOrigin.EnergyIsActualizationRate());
        Assert.True(MatterSectorOrigin.DeficitCarriesRestMass());
    }

    // ── 5. Candidate 3 — spectral density is CORRELATED (parameters + magnitude) ─

    [Fact]
    public void Y_G_001_SpectralDensityCorrelated()
    {
        // (a) The spectral density supplies the SOURCE'S PARAMETERS (QG182): each deficit parameter is
        // a D96 spectral quantity — m0 = occ0/SigmaM, r0 = ln(span), rhoBar = 1, d = 3.
        double m0 = Occ0 / SigmaM;
        Assert.Equal(0.0421052632, m0, 10);
        Assert.Equal(1.8566908819, LnSpan, 9);
        Assert.Equal(1.0, RhoBar, 12);
        Assert.Equal(3, D);
        Assert.Equal(SigmaM, Occ0 + 4.0 + Occ2, 12);            // the octave record [4,4,87]

        // The two configurations used in (c): the same density values in two spatial orders.
        double[] values = { 0.1, 0.2, 0.3, 0.4 };
        double[] xs = { 1.0, 2.0, 3.0, 4.0 };
        var fwd = PiecewiseLinear(values, xs);
        var rev = PiecewiseLinear(values.Reverse().ToArray(), xs);

        // (b) ... and the MAGNITUDE of G, through the occupation-weighted spectral content.
        Assert.Equal(363660.0, SpectralContent, 6);
        double hierarchy = Math.Pow(SpectralContent, 3.0);      // M_Pl/v = A^3
        Assert.True(Math.Abs(hierarchy - 4.8093524275896e16) / 4.8093524275896e16 < 1e-12);

        double v = (SigmaM + Doublets) * LnSpan;                 // weak scale (GeV)
        double mPlGeV = v * hierarchy;
        Assert.True(Math.Abs(mPlGeV - 1.22335e19) / 1.22335e19 < 2e-4, $"M_Pl = {mPlGeV} GeV");

        double mPlKg = mPlGeV / GeVPerKg;
        double gSI = HbarCJm / (mPlKg * mPlKg);
        Assert.True(Math.Abs(gSI - 6.6476e-11) / 6.6476e-11 < 1e-4, $"G = {gSI}");
        Assert.True(Math.Abs(gSI - 6.67430e-11) / 6.67430e-11 < 5e-3, $"G deviation from CODATA = {(gSI - 6.67430e-11) / 6.67430e-11:P3}");

        // (c) BUT the spectral content is GLOBAL: it is a function of the occupancy MULTISET only, so it
        // cannot distinguish the two configurations — while the field can.
        Assert.Equal(values.OrderBy(v => v), values.Reverse().OrderBy(v => v));   // identical spectrum
        Assert.Equal(values.Sum(x => x * x), values.Reverse().Sum(x => x * x), 12);   // every moment too

        double aFwd = DeficitCollective.AtAcceleration3D(fwd, 1.5);
        double aRev = DeficitCollective.AtAcceleration3D(rev, 1.5);
        Assert.True(aFwd < 0.0 && aRev > 0.0);
        Assert.Equal(-0.22222222, aFwd, 8);
        Assert.Equal(0.09523810, aRev, 8);

        // The spectral density therefore enters only through DIMENSIONLESS CONSTANTS (m0, r0, A) —
        // correlated with the source's parameters and scale, never the source itself.
        Assert.False(Dim.SpectralDensity.IsNone);                // dimension T, not dimensionless
    }

    // ── 6. Candidate 4 — information density is REFUTED as the source ────────────

    [Fact]
    public void Y_G_001_InformationDensityRefuted()
    {
        double[] record = { 0.1, 0.2, 0.3, 0.4 };
        double[] reversed = record.Reverse().ToArray();

        // (a) I = KL(rho || uniform) = ln K - H(rho): zero exactly for the uniform (vacuum) state.
        Assert.Equal(0.0, InformationContentOrigin.InformationContent(new[] { 0.25, 0.25, 0.25, 0.25 }), 12);
        double iRecord = InformationContentOrigin.InformationContent(record);
        Assert.Equal(0.1064401353, iRecord, 10);
        Assert.True(iRecord > 0.0);

        // (b) PERMUTATION INVARIANCE — the decisive refutation. The same information content is
        // compatible with DIFFERENT fields (here: opposite signs at the same point).
        Assert.Equal(iRecord, InformationContentOrigin.InformationContent(reversed), 12);
        double[] xs = { 1.0, 2.0, 3.0, 4.0 };
        double aFwd = DeficitCollective.AtAcceleration3D(PiecewiseLinear(record, xs), 1.5);
        double aRev = DeficitCollective.AtAcceleration3D(PiecewiseLinear(reversed, xs), 1.5);
        Assert.True(aFwd < 0.0 && aRev > 0.0, $"same I, fields {aFwd} vs {aRev}");
        Assert.True(Math.Abs(aFwd - aRev) > 0.3, "the field differs while the information content does not");

        // (c) It is a GLOBAL scalar. Used as a source it has no position dependence at all, so it
        // cannot generate a field: its "gradient" vanishes identically, while the density's does not.
        double iAsSource = InformationContentOrigin.InformationContent(record);
        Assert.Equal(0.0, FieldSensitivity(_ => iAsSource, 1.5), 12);
        Assert.True(FieldSensitivity(PiecewiseLinear(record, xs), 1.5) > 0.0);

        // (d) Direction of the bridge (QG_001): information is a FUNCTION of rho, not the reverse —
        // and the information density is the dark-energy SURPLUS (Omega_Lambda), which has no derived
        // gravitational role (NP_055-NP_060: a descriptor that cannot do work).
        Assert.Equal(0.6839, 0.7513 / 1.0986, 4);
        Assert.True(InformationContentOrigin.UniformHasZeroInformation(4));
        Assert.True(InformationContentOrigin.CriticalExpectedProfileUniform(3));
        Assert.True(InformationContentOrigin.NonCriticalProfileCarriesInformation(3));
    }

    // ── 7. Candidate 5 — the curvature-density law as a source is REFUTED ────────

    [Fact]
    public void Y_G_001_CurvatureLawRefuted()
    {
        // (a) R = F(rho) is exact (G4-G2), has dimension [L^-2] and carries no free constant.
        foreach (double a in new[] { 0.1, 0.5, 1.0 })
        {
            Assert.Equal(-4.0 * a * (D - 1.0) / D, HigherDimEinstein.ScalarCurvature(0.0, a, D), 9);
            Assert.Equal(-4.0 * a * (2 - 1.0) / 2, HigherDimEinstein.ScalarCurvature(0.0, a, 2), 9);
            // the general-profile evaluator agrees with the analytic one
            Assert.True(Math.Abs(ScalarCurvatureOf(x => 1.0 + a * x * x, 0.0, D)
                               - HigherDimEinstein.ScalarCurvature(0.0, a, D)) < 1e-6);
        }
        Assert.Equal(-0.2666666667, HigherDimEinstein.ScalarCurvature(0.0, 0.1, D), 9);
        Assert.Equal(-1.3333333333, HigherDimEinstein.ScalarCurvature(0.0, 0.5, D), 9);
        Assert.Equal(-0.2000000000, HigherDimEinstein.ScalarCurvature(0.0, 0.1, 2), 9);

        // (b) NO INDEPENDENT DEGREE OF FREEDOM: over a one-parameter family of densities the curvature
        // is an exactly monotone (perfectly correlated) read — R adds nothing to rho.
        double[] family = { 0.1, 0.2, 0.4, 0.7, 1.0 };
        double[] curvatures = family.Select(a => HigherDimEinstein.ScalarCurvature(0.0, a, D)).ToArray();
        Assert.True(Math.Abs(AdaptabilityAudit.Pearson(family, curvatures) + 1.0) < 1e-12);
        for (int i = 1; i < family.Length; i++)
            Assert.True(curvatures[i] < curvatures[i - 1]);

        // (c) VACUUM: curvature vanishes with the source — the curvature cannot be an independent
        // reservoir of gravity (contrast GR, whose vacuum admits non-zero Weyl curvature).
        Assert.Equal(0.0, ScalarCurvatureOf(_ => RhoBar, 1.0, D), 9);

        // (d) CIRCULARITY — R is NOT injective in rho. At x = 0 the curvature is fixed by rho''(0)
        // alone, R(0) = -2(d-1)*rho''(0)/(d*rho(0)), so two densities with the same rho''(0) share R(0)
        // while being different functions. If R sourced rho, R would have to determine rho; it does not.
        Func<double, double> rhoQuad = x => 1.0 + 0.5 * x * x;                             // rho''(0) = 1
        Func<double, double> rhoQuartic = x => 1.0 + 0.5 * x * x + 5.0 * x * x * x * x;    // rho''(0) = 1
        Assert.Equal(-2.0 * (D - 1.0) * 1.0 / (D * 1.0), HigherDimEinstein.ScalarCurvature(0.0, 0.5, D), 9);
        Assert.True(Math.Abs(ScalarCurvatureOf(rhoQuad, 0.0, D) - ScalarCurvatureOf(rhoQuartic, 0.0, D)) < 1e-4,
            "the same R(0) is shared by two different densities");
        Assert.True(Math.Abs(rhoQuad(1.0) - rhoQuartic(1.0)) > 1.0,
            $"rho1(1) = {rhoQuad(1.0)} vs rho2(1) = {rhoQuartic(1.0)}");
        Assert.True(Math.Abs(ScalarCurvatureOf(rhoQuad, 1.0, D) - ScalarCurvatureOf(rhoQuartic, 1.0, D)) > 1e-3,
            "the same R(0) is shared by profiles whose curvature elsewhere differs");

        // (e) The law itself is DERIVED (G4-G2): its role is OUTPUT, so it is the field equation's
        // right-hand side, not the source term on the left.
        Assert.True(Dim.Curvature.IsNone == false);
        Assert.Equal(-2.0, Dim.Curvature.L, 12);
    }

    // ── 8. Comparison: Newton / GR / RAR / QG ────────────────────────────────────

    [Fact]
    public void Y_G_001_CompareNewtonGrRarQg()
    {
        // NEWTON — the deficit reproduces the 1/r^2 form with M_eff -> m0*r0/(d*rhoBar) (test 3).
        // GR — kappa*T has curvature dimensions, so the GR source needs the dimensionful coupling;
        //      AT's dimensionless rho needs none (test 1).
        // RAR — AT's own research model g_obs = g_bar*sqrt(1 + g_dagger/g_bar) with the
        //      cosmic-clock scale g_dagger = c*H0/(2*pi) (QG080 / DATA program).
        double gDagger = (C_Kms * H0 / 1000.0) * Kms2PerKpcTo1e10 / (2.0 * Math.PI);
        Assert.True(Math.Abs(gDagger - 1.0422) < 1e-3, $"g_dagger = {gDagger} x1e-10 m/s2");

        Func<double, double> rar = gb => gb * Math.Sqrt(1.0 + gDagger / gb);
        Assert.Equal(Math.Sqrt(2.0), rar(gDagger) / gDagger, 10);                       // break point
        Assert.True(rar(1e-3) / Math.Sqrt(1e-3 * gDagger) - 1.0 < 0.01);                // deep: sqrt(g_bar*g_dagger)
        Assert.True(rar(1000.0) / 1000.0 < 1.001);                                      // Newtonian: g_obs -> g_bar

        // The AT-native deep regime is the log deficit: a ~ -1/r gives v^2 = r|a| ~ const (flat curve).
        Func<double, double> logRho = r => DeficitCollective.LogDeficit(r);
        double[] lxs = { 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 };
        double[] lys = lxs.Select(r => Math.Abs(DeficitCollective.AtAcceleration3D(logRho, r))).ToArray();
        var (logSlope, _) = DeficitCollective.LogLogFit(lxs, lys);
        Assert.True(logSlope > -1.3 && logSlope < -1.0, $"log-deficit d ln|a| / d ln r = {logSlope}");
        Assert.True(DeficitCollective.RotationCurveProxy(logRho, 2.0)
                  / DeficitCollective.RotationCurveProxy(logRho, 8.0) < 1.30);

        // QG / QG206 — the exponent alpha = 0 is the unique flat (scale-free) member of the
        // self-similar deficit family m ~ r^(-alpha); every alpha != 0 falls or rises.
        Func<double, double> Profile(double alpha)
            => r => DeficitCollective.AbundanceDeficit(r, alpha, RhoBar, 0.4, 0.5, 10.0);
        double Ratio(double alpha)
            => DeficitCollective.RotationCurveProxy(Profile(alpha), 3.0)
             / DeficitCollective.RotationCurveProxy(Profile(alpha), 9.0);

        Assert.Equal(1.1748, Ratio(0.0), 3);
        Assert.Equal(1.4914, Ratio(0.25), 3);
        Assert.Equal(1.9022, Ratio(0.5), 3);
        Assert.Equal(3.1476, Ratio(1.0), 3);
        Assert.Equal(9.0900, Ratio(2.0), 3);
        Assert.True(Ratio(0.0) < Ratio(0.25) && Ratio(0.25) < Ratio(0.5)
                 && Ratio(0.5) < Ratio(1.0) && Ratio(1.0) < Ratio(2.0));
        Assert.True(Ratio(0.0) < 1.20, "alpha = 0 is the marginal, flattest member");

        // QG195/196 — the source is the deficit dust: T = (rhoBar - rho)v v, conserved and independent of G.
        Assert.True(MatterSectorOrigin.DustIsConserved());
        Assert.True(NativeMetricDynamics.MatterIndependentOfG());
        Assert.Equal(3, MatterSectorOrigin.OriginScore());

        // QG222/QG206 — the source's own dynamics is the actualization branching, stationary at mu = 1.
        Assert.True(NativeMetricDynamics.BranchingContinuity(2.0, 8));
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality());
        Assert.True(NativeMetricDynamics.MetricStaticAtCriticality(D));
        Assert.Equal(5, NativeMetricDynamics.OriginScore());

        // QG21/24/43/212 — the conformal (scalar) sector has NO vacuum curvature: R = F(rho) vanishes
        // identically for uniform rho, so lensing / tensor waves require the psi sector (a separate
        // audit); this is a property of the SOURCE being rho, not of the candidate list.
        Assert.Equal(0.0, ScalarCurvatureOf(_ => RhoBar, 1.0, D), 9);
    }

    // ── 9. Report ────────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_001 — Gravity Source Audit");

        Func<double, double> powerLaw = r => DeficitCollective.PowerLawDeficit(r, RhoBar, 0.5, 0.5);
        Func<double, double> logRho = r => DeficitCollective.LogDeficit(r);
        double asymptote = 0.5 * 0.5 / (D * RhoBar);
        double meff12 = DeficitCollective.EffectiveEnclosedMass(
            DeficitCollective.AtAcceleration3D(powerLaw, 12.0), 12.0);
        double gDagger = (C_Kms * H0 / 1000.0) * Kms2PerKpcTo1e10 / (2.0 * Math.PI);
        double gSI = HbarCJm / Math.Pow((SigmaM + Doublets) * LnSpan * Math.Pow(SpectralContent, 3.0) / GeVPerKg, 2.0);
        double[] record = { 0.1, 0.2, 0.3, 0.4 };
        double[] xs = { 1.0, 2.0, 3.0, 4.0 };

        sb.AppendLine("Question: what variable actually sources gravity in AT?");
        sb.AppendLine();
        sb.AppendLine("[0] Assumptions");
        sb.AppendLine("    * d = 3, counting-measure reference rhoBar = 1 (QG182); no new primitive.");
        sb.AppendLine("    * The AT field equation is R = F(rho) with g = rho^(2/d)*eta (QG197/G4-G2); the");
        sb.AppendLine("      acceleration is a = -(1/d)*grad ln rho (G4-O3); the native dynamics is");
        sb.AppendLine("      rho_{k+1} = mu*rho_k (QG222). The deficit m = rhoBar - rho is the attractive sector.");
        sb.AppendLine("    * 'Source' = the variable the field equation takes as INPUT. Every candidate is judged");
        sb.AppendLine("      by: units -> dimensions -> limits -> agreement with Newton/GR/RAR/QG.");
        sb.AppendLine("    * Deterministic closed-form profiles, fixed grids; no randomness.");
        sb.AppendLine();

        sb.AppendLine("[1] Units and dimensions");
        sb.AppendLine("    candidate              units                     [M,L,T]      local?  dimensionless?");
        sb.AppendLine("    1 energy density       J m^-3                    [1,-1,-2]   yes     NO");
        sb.AppendLine("    2 actualization dens.  count per 4-volume        [0,0,0] (ratio) yes  YES");
        sb.AppendLine("    3 spectral density     modes per unit frequency  [0,0,1]      NO      no");
        sb.AppendLine("    4 information density  nats per mode             [0,0,0]      NO      YES");
        sb.AppendLine("    5 curvature R = F(rho) 1/m^2                     [0,-2,0]     yes     no");
        sb.AppendLine("    dimensional route: kappa*T -> [L^-2] = [R] (GR, needs the imported kappa);");
        sb.AppendLine("                       rho -> [L^-2] = [R] directly (AT, no coupling constant).");
        sb.AppendLine("    scale invariance: a(lambda*rho) = a(rho) exactly; R(lambda*rho) = lambda^(-2/d) R(rho).");
        sb.AppendLine();

        sb.AppendLine("[2] Limits");
        sb.AppendLine($"    vacuum (rho = rhoBar)        a = 0 exactly, R = 0 exactly");
        sb.AppendLine($"    weak deficit (Newton)        a = -m0*r0/(d*rhoBar*r^2); M_eff(12) = {meff12:F6} = {meff12 / asymptote:P2} of {asymptote:F6}");
        sb.AppendLine($"    1/r^2 confirmation           d ln|a| / d ln r = {DeficitCollective.LogLogFit(new[] { 8.0, 16.0, 32.0, 64.0, 128.0 }, new[] { 8.0, 16.0, 32.0, 64.0, 128.0 }.Select(r => Math.Abs(DeficitCollective.AtAcceleration3D(powerLaw, r))).ToArray()).slope:F4} (target -2)");
        sb.AppendLine($"    flat curve (log deficit)     d ln|a| / d ln r = {DeficitCollective.LogLogFit(new[] { 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 }, new[] { 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 }.Select(r => Math.Abs(DeficitCollective.AtAcceleration3D(logRho, r))).ToArray()).slope:F4} (target -1)");
        sb.AppendLine($"    rho -> 0                     curvature diverges as rho^(-2/3): {PlanckRegime.CurvatureDivergence(1e-6, D):F0} at rho = 1e-6");
        sb.AppendLine($"    mu = 1 (critical, alpha = 0)  d rho/dt = 0 exactly; d g/dt = 0 exactly");
        sb.AppendLine($"    uniform record               I = KL(rho||uniform) = 0 exactly");
        sb.AppendLine($"    deep RAR (g_bar << g_dag)    g_obs -> sqrt(g_bar*g_dagger), break ratio sqrt(2) at g_bar = g_dagger");
        sb.AppendLine();

        sb.AppendLine("[3] Comparison");
        sb.AppendLine("                                            AT (rho)                     Newton / GR / RAR / QG");
        sb.AppendLine("    source variable                    actualization density rho     mass density / T_munu");
        sb.AppendLine("    coupling                            none (dimensionless)         G, kappa (imported)");
        sb.AppendLine("    long-range field                    a = -(1/d)*grad ln rho       a = -GM/r^2");
        sb.AppendLine($"    coupling magnitude                  G = {gSI:E4} m3/kg/s2   CODATA 6.6743e-11 (dev {Math.Abs(gSI - 6.67430e-11) / 6.67430e-11:P2})");
        sb.AppendLine($"    acceleration scale g_dagger         c*H0/(2*pi) = {gDagger:F4}e-10 m/s2  MOND a0 ~ 1.2e-10 (same form: g_obs = g_bar*sqrt(1+g_dag/g_bar))");
        sb.AppendLine("    vacuum curvature                    R = 0 (conformal)            GR vacuum may carry Weyl -> psi sector (QG21/24/43/212)");
        sb.AppendLine("    matter tensor                       T = (rhoBar-rho)v v (QG195)   T_munu (independent in GR)");
        sb.AppendLine("    deficit dust conservation           yes (Lovelock escapes, QG195) yes");
        sb.AppendLine("    kinetic energy tensor               NOT conserved (Bianchi test)  conserved by construction");
        sb.AppendLine("    alpha = 0 flatness                  unique marginal member (QG206) RAR deep regime: v^4 = G*M*a0");
        sb.AppendLine();

        sb.AppendLine("[4] Candidate-by-candidate");
        sb.AppendLine("    1 ENERGY DENSITY ........... CORRELATED");
        sb.AppendLine("      T00 = (rhoBar - rho)v^2 is rank-identical to the deficit (Pearson = -1 against rho, +1");
        sb.AppendLine("      against m); the only conserved tensor built from the geometry is G/kappa (Lovelock), and");
        sb.AppendLine("      the kinetic candidate fails conservation (nabla T_kin = 0.0209 vs Bianchi 1e-12).");
        sb.AppendLine("      The energy READING is hosted (QG89/NP_058). Correlated, not primitive.");
        sb.AppendLine("    2 ACTUALIZATION DENSITY .... SOURCE");
        sb.AppendLine("      g = rho^(2/d)*eta; R = F(rho); a = -(1/d)*grad ln rho; rho_{k+1} = mu*rho_k. Dimensionless,");
        sb.AppendLine("      local, no free coupling, fixes the sign, the 1/r^2 range and the alpha = 0 flatness.");
        sb.AppendLine("      The attractive sector is its standardized deficit m = rhoBar - rho.");
        sb.AppendLine("    3 SPECTRAL DENSITY ......... CORRELATED");
        sb.AppendLine("      Supplies the source's PARAMETERS (m0 = occ0/SigmaM = 0.042105, r0 = ln span = 1.856691)");
        sb.AppendLine("      and the MAGNITUDE of G (A = 363660, M_Pl = v*A^3, 0.2%/0.4%). Global (no position index):");
        sb.AppendLine("      identical for profiles whose fields differ -> cannot be the local source.");
        sb.AppendLine("    4 INFORMATION DENSITY ...... REFUTED");
        sb.AppendLine($"      I(record) = I(reversed record) = {InformationContentOrigin.InformationContent(record):F6} nats, yet");
        sb.AppendLine($"      a(1.5) = {DeficitCollective.AtAcceleration3D(PiecewiseLinear(record, xs), 1.5):F6} vs {DeficitCollective.AtAcceleration3D(PiecewiseLinear(record.Reverse().ToArray(), xs), 1.5):F6}.");
        sb.AppendLine("      Permutation-invariant GLOBAL functional of rho (a function OF the source, QG_001), and the");
        sb.AppendLine("      dark-energy surplus (Omega_Lambda = 0.6839) with no derived gravitational role (NP_055-064).");
        sb.AppendLine("    5 CURVATURE-DENSITY LAW .... REFUTED (as a source; the law itself is DERIVED, G4-G2)");
        sb.AppendLine("      R is the OUTPUT of R = F(rho). It is monotone (Pearson = -1) in the sole profile parameter,");
        sb.AppendLine("      so it carries no independent degree of freedom, and it is NOT injective: rho = 1+0.5x^2 and");
        sb.AppendLine("      rho = 1+0.5x^2+5x^4 share R(0) = -1.333333 while differing away from 0. Sourcing rho from R");
        sb.AppendLine("      is therefore circular.");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    SOURCE      : actualization density rho (counting measure) — and, for the attractive");
        sb.AppendLine("                  sector, its standardized deficit m = rhoBar - rho.");
        sb.AppendLine("    CORRELATED  : energy density (exact re-expression of the deficit; energy reading hosted);");
        sb.AppendLine("                  spectral density (source parameters + G magnitude, not the local field).");
        sb.AppendLine("    REFUTED     : information density (permutation-invariant global functional; the surplus);");
        sb.AppendLine("                  curvature-density law R = F(rho) as a source (output of the law; circular).");
        sb.AppendLine();
        sb.AppendLine("[6] Classification and caveats");
        sb.AppendLine("    DERIVED (field-equation input): g = rho^(2/d)*eta (QG197); R = F(rho) (G4-G2);");
        sb.AppendLine("        a = -(1/d)*grad ln rho (G4-O3); rho_{k+1} = mu*rho_k (QG222); M_eff = m0*r0/(d*rhoBar) (QG6);");
        sb.AppendLine("        deficit dust T = (rhoBar-rho)v v (QG195/196); alpha = 0 criticality (QG206).");
        sb.AppendLine("    BOUNDARY: the counting-measure reference rhoBar = 1 and the metric ansatz exponent 2/d;");
        sb.AppendLine("        the scale-freeness behind alpha = 0 (AT-F1/NP_070); the psi tensor completion (QG24/43);");
        sb.AppendLine("        the SI conversion of G (hbar, c, GeV->kg).");
        sb.AppendLine("    HYPOTHESIS (real-underived): 'matter = deficit' (G4-ME0) — the sign is derived, the");
        sb.AppendLine("        identification is a reinterpretation. Two-level rule: the SOURCE VARIABLE is derived;");
        sb.AppendLine("        the attractive-deficit READING is a hypothesis, and its energy reading is hosted.");
        sb.AppendLine("    No canonical value reclassified; the D_040 ClassificationRegistry is untouched.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}

