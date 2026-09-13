using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_038 — Measure-Decomposition Audit (group G — Gravity Source).
///
/// QUESTION. Curvature can be read as a change in MEASURE — distance bending a little. Time is a measure too, so
/// "time should be able to do the same". Can the CLOCK function alone supply the observed light deflection,
/// making no spatial postulate necessary?
///
/// ANSWER: **YES FOR HALF — AND THE OTHER HALF IS FORBIDDEN BY THE REDSHIFT.** Plus a refinement of G_029/G_030:
/// the first-order spatial coefficient is **DERIVED**, not postulated.
///
///  (1) LIGHT RESPONDS ONLY TO THE DIFFERENCE: `n − 1 = B − A` = (+B distance) + (−A clock). The clock contributes
///      exactly **1/(1+γ)** — **50 %** at γ = 1, **100 %** at γ = 0, and at γ = −1 the halves are equal and
///      opposite so the net is **zero**. The intuition is correct.
///  (2) BUT A CONFORMAL CHANGE CANNOT BEND LIGHT AT ALL: B = A for *any* conformal factor ⟹ n = 1 exactly,
///      verified across x = 1e−12 … 0.3. A conformal factor maps null geodesics to null geodesics. Making the
///      clock bend harder is automatically matched by space, and the two cancel — which IS AT's position.
///  (3) THE COMPENSATION ROUTE WORKS: A = −2x with flat space gives the **full** deflection (a = 2) — at **twice
///      the redshift**. The solar-limb redshift is measured as 2.12e−6 to ~1 %, so **A is pinned**. The redshift
///      is what forbids compensating via time.
///  (4) THEREFORE B = +x IS FORCED: with A measured, B's first-order coefficient is **DERIVED** from two
///      measurements. Only the O(x²) completion is a postulate — the two-level rule already used in D_028/D_040.
///  (5) THE MEDIUM ESCAPE IS UNAVAILABLE IN AT ANYWAY: no graviton exists (the tensor sector is the massless
///      spin-2 ψ field), and any AT-natural index is built from ρ, which sources the ψ sector too — so it IS a
///      metric.
/// </summary>
public class Y_G_038_Tests : ResearchTestBase
{
    public Y_G_038_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_038_LightRespondsOnlyToTheDifference()
    {
        double x = MeasureDecompositionAudit.SolarX;

        // The clock contributes −A and space contributes +B; together they must reproduce n − 1 exactly.
        var (clock, space) = MeasureDecompositionAudit.Contributions(x, MeasureDecompositionAudit.RequiredB());
        Assert.Equal(x, clock, 12);                     // −A = +x
        Assert.Equal(x, space, 12);                     //  B = +x
        // n − 1 = e^(2x) − 1 = 2x + O(x²): the decomposition is first-order exact, so compare as a ratio.
        Assert.Equal(1.0, MeasureDecompositionAudit.IndexMinusOne(x, MeasureDecompositionAudit.RequiredB())
                          / (clock + space), 5);

        // THE CLOCK'S SHARE IS EXACTLY 1/(1+γ): 50 % in GR, 100 % when only time acts.
        Assert.Equal(0.5, MeasureDecompositionAudit.ClockShare(1.0), 12);
        Assert.Equal(1.0, MeasureDecompositionAudit.ClockShare(0.0), 12);
        Assert.Equal(1.0 / 3.0, MeasureDecompositionAudit.ClockShare(2.0), 12);
        // At γ = −1 (AT's case) the halves are equal and opposite: the share is undefined because the net is zero.
        Assert.True(double.IsNaN(MeasureDecompositionAudit.ClockShare(-1.0)));

        // Time does bend light: with flat space the clock alone supplies HALF the deflection (a = 1 + O(x)).
        var routes = MeasureDecompositionAudit.Routes();
        Assert.Equal(1.0, routes.Single(r => r.Route.Contains("time only")).DeflectionCoefficient, 4);
    }

    [Fact]
    public void Y_G_038_AConformalChangeCannotBendLightAtAll()
    {
        // B = A for ANY conformal factor ⟹ n − 1 = 0 exactly, at every compactness from lab to compact object.
        foreach (double x in MeasureDecompositionAudit.ConformalProbe())
            Assert.Equal(0.0, MeasureDecompositionAudit.ConformalIndexMinusOne(x));

        // …and the reason is structural, not a numerical accident.
        Assert.Contains("identically", MeasureDecompositionAudit.ConformalIsInvisible());
        Assert.Contains("null geodesics", MeasureDecompositionAudit.ConformalIsInvisible());

        // AT's conformal route therefore gives ZERO bending, while the required space function gives all of it.
        var routes = MeasureDecompositionAudit.Routes();
        Assert.Equal(0.0, routes.Single(r => r.Route.Contains("conformal")).DeflectionCoefficient, 9);
        Assert.Equal(2.0, routes.Single(r => r.Route.Contains("required")).DeflectionCoefficient, 4);
    }

    [Fact]
    public void Y_G_038_TheCompensationRouteIsExcludedByTheRedshift()
    {
        var routes = MeasureDecompositionAudit.Routes();

        // The clock-compensated route DOES deliver the full deflection…
        var comp = routes.Single(r => r.Route.Contains("clock-compensated"));
        Assert.Equal(2.0, comp.DeflectionCoefficient, 4);
        Assert.Equal(-2.0 * MeasureDecompositionAudit.SolarX, comp.A, 15);

        // …but it pays with TWICE the redshift, while every other route is at 1×.
        Assert.Equal(2.0, comp.RedshiftOverMeasured, 9);
        Assert.All(routes.Where(r => !r.Route.Contains("clock-compensated")),
            r => Assert.Equal(1.0, r.RedshiftOverMeasured, 9));

        // And the redshift is measured, so A is not a free knob.
        Assert.Contains("2.12e−6", MeasureDecompositionAudit.TheClockIsMeasured());
        Assert.Contains("Pound", MeasureDecompositionAudit.TheClockIsMeasured());
        Assert.Contains("not a free knob", MeasureDecompositionAudit.TheClockIsMeasured());
    }

    [Fact]
    public void Y_G_038_TheFirstOrderSpatialCoefficientIsDerived()
    {
        // The refinement: with A measured, B = +x is FORCED — DERIVED at first order, BOUNDARY only at O(x²).
        var levels = MeasureDecompositionAudit.Refinement();
        Assert.Equal(2, levels.Length);
        Assert.Equal("DERIVED", levels[0].Status);
        Assert.Equal("BOUNDARY", levels[1].Status);
        Assert.Contains("No freedom remains at O(x)", levels[0].Why);

        // G_029's survivor and GR agree at first order — B = +x to four decimals in B/x.
        double x = MeasureDecompositionAudit.SolarX;
        Assert.Equal(1.0, MeasureDecompositionAudit.SurvivorB()(x) / x, 4);
        Assert.Equal(1.0, MeasureDecompositionAudit.GrB()(x) / x, 4);

        // …and differ afterwards, which is the whole of the remaining freedom.
        Assert.Equal(-2.697e-11, MeasureDecompositionAudit.SurvivorVsGrRelative(2.12e-6), 14);
        Assert.Equal(-0.2968, MeasureDecompositionAudit.SurvivorVsGrRelative(0.247002), 4);
    }

    [Fact]
    public void Y_G_038_TheMediumEscapeIsUnavailableInAt()
    {
        string why = MeasureDecompositionAudit.WhyNoMediumEscape();
        // AT has no graviton — the tensor sector is a field.
        Assert.Contains("no graviton", why);
        Assert.Contains("spin-2 ψ field", why);
        // And the index cannot be EM-specific, because ρ sources the metric including that sector.
        Assert.Contains("it IS a metric", why);
        Assert.Contains("ρ sources the metric", why);
    }

    [Fact]
    public void Y_G_038_VerdictIsRefutedWithADerivedByproduct()
    {
        Assert.Equal("REFUTED", MeasureDecompositionAudit.Verdict());

        string where = MeasureDecompositionAudit.WhereItStands();
        Assert.Contains("Time DOES bend light", where);
        Assert.Contains("cancel identically", where);
        Assert.Contains("doubles", where);
        Assert.Contains("DERIVED, NOT POSTULATED", where);
        Assert.Contains("wrong by a sign and by a factor of two", where);
    }

    [Fact]
    public void Y_G_038_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_038 — Measure-Decomposition Audit: can the clock bend light on its own?");

        sb.AppendLine("QUESTION. Curvature can be read as a change in MEASURE — distance bending a little. Time is");
        sb.AppendLine("a measure too, so time 'should be able to do the same'. Can the clock function alone supply");
        sb.AppendLine("the observed deflection, so that no spatial postulate is needed?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Weak field, first order in x = GM/(Rc²) (the PPN regime).");
        sb.AppendLine("  2. n = e^(B−A), so light responds ONLY to the difference B − A.");
        sb.AppendLine("  3. The redshift measures A independently: z ≈ −A.");
        sb.AppendLine();

        PrintHeader("1. LIGHT RESPONDS ONLY TO THE DIFFERENCE  n − 1 = B − A");
        sb.AppendLine("  the DISTANCE measure contributes  +B");
        sb.AppendLine("  the CLOCK  measure contributes  −A");
        sb.AppendLine();
        double x = MeasureDecompositionAudit.SolarX;
        sb.AppendLine("  rule                                  A           B         n−1        a     bending");
        foreach (var r in MeasureDecompositionAudit.Routes())
        {
            double n1 = Math.Exp(r.B - r.A) - 1.0;
            sb.AppendLine($"  {r.Route,-36}{r.A,12:E3}{r.B,12:E3}{n1,12:E3}{r.DeflectionCoefficient,9:F2}"
                        + $"{r.DeflectionCoefficient / 2 * 100,10:F1}%");
        }
        sb.AppendLine();
        sb.AppendLine("  ⇒ THE INTUITION IS RIGHT: the clock DOES bend light. Its share is exactly 1/(1+γ):");
        foreach (double g in new[] { 0.0, 1.0, 2.0 })
            sb.AppendLine($"      γ = {g:F0}  →  clock share {MeasureDecompositionAudit.ClockShare(g):P1}");
        sb.AppendLine("      γ = −1  →  the halves are equal and opposite: the NET IS ZERO  ← AT's case");
        sb.AppendLine();

        PrintHeader("2. BUT A CONFORMAL CHANGE CANNOT BEND LIGHT AT ALL");
        sb.AppendLine("  B = A for ANY conformal factor ⟹ n = e^(B−A) = 1 EXACTLY:");
        foreach (double xp in MeasureDecompositionAudit.ConformalProbe())
            sb.AppendLine($"      x = {xp,-10:E3}  A = B = {-xp,-12:F8}  n − 1 = {MeasureDecompositionAudit.ConformalIndexMinusOne(xp):E3}");
        sb.AppendLine();
        sb.AppendLine("  " + MeasureDecompositionAudit.ConformalIsInvisible());
        sb.AppendLine("  ⇒ 'time and space bending equally' is INVISIBLE TO LIGHT — and that is precisely AT's");
        sb.AppendLine("    position (A = B = σ). Making the clock bend harder makes space bend equally harder.");
        sb.AppendLine();

        PrintHeader("3. THE COMPENSATION ROUTE WORKS — AND IS EXCLUDED BY THE REDSHIFT");
        sb.AppendLine("  keep space FLAT (B = 0) and double the clock (A = −2x): n − 1 = 2x  ⇒  a = 2  ⇒  FULL bending");
        sb.AppendLine("  …at TWICE the redshift:");
        sb.AppendLine();
        sb.AppendLine("  route                                  A           z = −A        a      redshift");
        foreach (var r in MeasureDecompositionAudit.Routes())
            sb.AppendLine($"  {r.Route,-36}{r.A,12:E3}{MeasureDecompositionAudit.Redshift(r.A),12:E3}"
                        + $"{r.DeflectionCoefficient,9:F2}{r.RedshiftOverMeasured,13:F2}x");
        sb.AppendLine();
        sb.AppendLine("  " + MeasureDecompositionAudit.TheClockIsMeasured());
        sb.AppendLine();

        PrintHeader("4. THEREFORE B = +x IS FORCED — THE FIRST ORDER IS DERIVED, NOT POSTULATED");
        foreach (var l in MeasureDecompositionAudit.Refinement())
        {
            sb.AppendLine($"  [{l.Status}]  {l.Level}");
            sb.AppendLine($"      {l.Why}");
        }
        sb.AppendLine();
        sb.AppendLine("  the remaining freedom, measured:");
        sb.AppendLine("      g_rr: survivor vs GR, solar x = 2.12e−6   →  "
                      + $"{MeasureDecompositionAudit.SurvivorVsGrRelative(2.12e-6):E3} relative");
        sb.AppendLine("      g_rr: survivor vs GR, neutron star 0.247002 →  "
                      + $"{MeasureDecompositionAudit.SurvivorVsGrRelative(0.247002):P1}");
        sb.AppendLine("  ⇒ the project's TWO-LEVEL RULE (as for the 3-family window in D_028/D_040): the");
        sb.AppendLine("    first-order VALUE is DERIVED; the higher-order WINDOW is BOUNDARY.");
        sb.AppendLine();

        PrintHeader("5. AND THE MEDIUM ESCAPE IS UNAVAILABLE IN AT");
        sb.AppendLine("  " + MeasureDecompositionAudit.WhyNoMediumEscape());
        sb.AppendLine();

        PrintHeader("VERDICT");
        sb.AppendLine($"  {MeasureDecompositionAudit.Verdict()}");
        sb.AppendLine("  " + MeasureDecompositionAudit.WhereItStands());

        Output.WriteLine(sb.ToString());
    }
}
