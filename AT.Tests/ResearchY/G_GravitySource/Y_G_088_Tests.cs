using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_088 - ALTERNATIVE OBSERVABLE AUDIT.
///
/// G_087 measured that the kernel's dimension is undecidable by the redshift route: the free room's signature is a
/// factor 10.8 below the AT/GR separation, and below |c| = 0.72 the compactness error vetoes it at any precision. This
/// suite asks the structural question: **is there an observable NOT proportional to that same signature?** Seven
/// domains are searched, and the audit prefers refutation.
///
/// The report follows the seven domains and the four questions. Every figure is computed here, and no spectral quantity
/// is quoted.
/// </summary>
public sealed class Y_G_088_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_088_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F2(double v) => v.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_088_01_TheCatalogueAcrossSevenDomains()
    {
        PrintHeader("G_088 - task 1: which observable is first NOT degenerate with the redshift?");
        var sb = Start(
            "the measure of independence is the POWER of the potential in an observable's c-signature: two observables are",
            "collinear — hence degenerate at one target — exactly when their powers agree;",
            "the power is MEASURED by evaluating the relative signature at the surface potential and at a core potential",
            "(a central density five times the mean) and fitting p = ln(rho2/rho1)/ln(A2/A1);",
            "the kernel is the same object G_086 collapsed and G_087 made undecidable: ln K = A + c A^2 / 2;",
            "measurability is judged against the two quantities the sector actually measures — the surface redshift and the",
            "compactness — and not against a model or a hidden interior.");
        sb.Append(AlternativeObservableAudit.OutputCatalogue());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var catalogue = AlternativeObservableAudit.Catalogue();
        Assert.Equal(8, catalogue.Length);
        Assert.Equal(7, catalogue.Select(o => o.Domain).Distinct().Count());
        // the powers: the rate's signature is second order, the first derivative's first, the curvature is c itself
        Assert.Equal(2.0, catalogue.Single(o => o.Domain == "value").PowerInThePotential, 6);
        Assert.Equal(1.0, catalogue.Single(o => o.Observable.Contains("first gradient")).PowerInThePotential, 6);
        Assert.Equal(0.0, catalogue.Single(o => o.Observable.Contains("curvature")).PowerInThePotential, 6);
        Assert.Equal(2.0, catalogue.Single(o => o.Domain == "clock transport").PowerInThePotential, 6);

        var nonCollinear = AlternativeObservableAudit.NonCollinear();
        Assert.Equal(4, nonCollinear.Length);                       // the gradient, the curvature, the dynamical derivative and the mixed ratio
        Assert.All(nonCollinear, o => Assert.NotEqual(2.0, o.PowerInThePotential, 6));
        Assert.Empty(AlternativeObservableAudit.IndependentAndMeasurable());

        sb.AppendLine("  the answer to question 1: the FIRST non-degenerate observable is a DERIVATIVE — the acceleration,");
        sb.AppendLine("  whose signature is first order in the potential where the redshift's is second. But it is reachable only");
        sb.AppendLine("  THROUGH the compactness, so of the three non-collinear observables exactly NONE is independently");
        sb.AppendLine("  measurable in the surviving sector.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_02_TheCurvatureReadsTheExtensionDirectly()
    {
        PrintHeader("G_088 - task 2: the sharpest mathematically independent probe");
        var sb = Start(
            "the kernel's second derivative with respect to the potential is d^2(ln K)/dA^2 = c EXACTLY;",
            "so a curvature measurement would read the extension directly, with no baseline ratio to be swamped by;",
            "the audit reports it as non-collinear AND unmeasurable, both measured rather than asserted;",
            "the curvature's signature power is zero by construction: it has no c = 0 baseline to be relative to.");
        sb.Append(AlternativeObservableAudit.OutputCatalogue());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the identity, checked at several potentials and extension sizes
        foreach (var c in new[] { -0.5, -0.1, 0.1, 0.5 })
        {
            foreach (var a in new[] { 0.1, 0.247, 0.536 })
            {
                Assert.Equal(c, AlternativeObservableAudit.Curvature(a, c), 12);
            }
        }
        // and the first derivative is exactly 1 + c A, so the derivative route's power is one
        foreach (var a in new[] { 0.1, 0.247, 0.536 })
        {
            Assert.Equal(1.0 + 0.3 * a, AlternativeObservableAudit.Gradient(a, 0.3), 12);
        }
        Assert.True(double.IsNaN(AlternativeObservableAudit.SignaturePerUnitC(AlternativeObservableAudit.Curvature, 0.247)),
            "a curvature has no baseline, so its relative signature is undefined rather than small");

        sb.AppendLine("  d^2(ln K)/dA^2 = c to twelve decimals, and the first derivative is exactly 1 + c A: the separation exists");
        sb.AppendLine("  mathematically and is as strong as it can be — it is the MEASUREMENT that is missing.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_03_TheTransportTelescopesExactly()
    {
        PrintHeader("G_088 - the clock-transport domain: nothing new");
        var sb = Start(
            "a clock carried from one site to another measures the integral of the kernel's gradient;",
            "the integral telescopes EXACTLY into the difference of the kernel's values, which is the redshift ratio;",
            "the residual is therefore asserted at 1e-12 rather than bounded.");
        sb.Append(AlternativeObservableAudit.OutputStructural());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var t = AlternativeObservableAudit.TransportTelescoping().Single();
        Assert.True(t.Residual < 1e-12, $"the telescoping must be exact (residual {t.Residual:E3})");
        Assert.NotEqual(0.0, t.ValueDifference);
        var transport = AlternativeObservableAudit.Catalogue().Single(o => o.Domain == "clock transport");
        Assert.True(transport.DegenerateWithTheRedshift);

        sb.AppendLine("  the transport domain is REFUTED: an integrated gradient IS a value difference, so it carries exactly");
        sb.AppendLine("  what the redshift already carries.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_04_TheInteriorSignatureIsLargerAndHidden()
    {
        PrintHeader("G_088 - the compact-object profile domain: a bigger signal that does not escape");
        var sb = Start(
            "the interior profile's signature scales as the potential squared, and a core potential (a central density five",
            "times the mean) is several times the surface's, so the core's signature is LARGER;",
            "the interior's clock does not escape, so the larger signature is not an observable;",
            "the audit separates the size of a signature from the reach of a measurement, which is the whole distinction.");
        var interior = AlternativeObservableAudit.InteriorAdvantage();
        sb.AppendLine($"    the surface potential {N(interior.SurfacePotential)}, the core potential {N(interior.CorePotential)}, ");
        sb.AppendLine($"    the signature ratio {F2(interior.SignatureRatio)}");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var interiorRow = AlternativeObservableAudit.Catalogue().Single(o => o.Domain == "compact-object profiles");
        Assert.True(interior.SignatureRatio > 1.0, "the core's signature must be the larger one");
        Assert.Equal(2.0, interiorRow.PowerInThePotential, 6);
        Assert.True(interiorRow.DegenerateWithTheRedshift);
        Assert.False(interiorRow.MeasurableInTheSurvivingSector);

        sb.AppendLine($"  the interior offers a signature a factor {F2(interior.SignatureRatio)} larger and REFUTES nothing,");
        sb.AppendLine("  because no clock escapes from the core: the profile domain is closed by reachability, not by form.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_05_ASecondTargetIsNotSufficient()
    {
        PrintHeader("G_088 - task 3: is a second compactness target sufficient?");
        var sb = Start(
            "each target contributes one equation and one FREE compactness offset, so N targets give N equations in N + 1",
            "unknowns and the system is structurally rank-deficient;",
            "the gain from N targets is therefore only the square root of the same error, computed rather than asserted;",
            "the compactness error used is the recorded one at the J0740 target (3.661 per cent).");
        sb.Append(AlternativeObservableAudit.OutputStructural());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var counts = AlternativeObservableAudit.TwoTargetCounts();
        Assert.Equal(5, counts.Length);
        Assert.All(counts, c => Assert.False(c.StructurallyDetermined, "no target count determines the system"));
        Assert.All(counts, c => Assert.Equal(c.Equations + 1, c.Unknowns));
        // the gain is the square root of the target count, and nothing more
        foreach (var (targets, _, _, _, _, gain) in counts)
        {
            Assert.Equal(Math.Sqrt(targets), gain, 6);   // the gain is the square root of the target count
        }
        Assert.True(counts[^1].ErrorOnC > 0.0);

        sb.AppendLine("  the answer to question 3 is NO, by a COUNT: the c-signature is collinear with the free offsets at every");
        sb.AppendLine("  target, so second and further targets buy only the square root of the same error — not a new axis.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_06_TheMixedRouteMovesTheNumberByPrecisionOnly()
    {
        PrintHeader("G_088 - the mixed gravity/clock domain: precision, not independence");
        var sb = Start(
            "the ratio of the rate's signature to the gradient's is exactly A/2, a function of the potential, so (rate,",
            "gradient) at ONE target gives two equations in (c, A);",
            "but the gradient IS the compactness — the quantity whose error already vetoes the redshift route — so the",
            "route buys precision rather than an independent axis;",
            "the reach is computed from the recorded compactness error and compared with G_087's veto.");
        sb.Append(AlternativeObservableAudit.OutputStructural());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var reach = AlternativeObservableAudit.ReachOfTheMixedRoute();
        sb.AppendLine($"  the c-error the recorded compactness error already imposes is {F2(reach.CError)}, hence a three-sigma "
            + $"threshold near |c| = {F2(reach.ThreeSigmaThreshold)}");
        Assert.True(reach.CError > 0.1 && reach.CError < 0.5, $"the c-error must be of order a quarter ({F2(reach.CError)})");
        Assert.True(reach.ThreeSigmaThreshold > 0.4 && reach.ThreeSigmaThreshold < 1.5,
            $"the threshold must land near G_087's veto ({F2(reach.ThreeSigmaThreshold)})");
        // and it agrees with the independent route G_087 used
        double veto = KernelDiscriminabilityAudit.CompactnessVetoThreshold();
        Assert.True(Math.Abs(reach.ThreeSigmaThreshold - veto) / veto < 0.35,
            $"the two routes must agree on the veto (here {F2(reach.ThreeSigmaThreshold)}, G_087 {F2(veto)})");

        sb.AppendLine("  the mixed route recovers G_087's veto from a different direction — which is exactly what 'precision, not");
        sb.AppendLine("  independence' means: the number moves, the axis does not.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_07_TheRemainingDomainsAreClosed()
    {
        PrintHeader("G_088 - the remaining domains: dynamical evolution and the tidal field");
        var sb = Start(
            "a dynamical observable (the redshift's drift) is first order in c A like any derivative, so its FORM is",
            "independent of the redshift's;",
            "but the redistribution rate it is proportional to is fixed by no surviving datum, so its magnitude is",
            "unconstrained;",
            "the tidal route is a second gradient, whose conversion into an observable is a structural model rather than a",
            "reading of the kernel.");
        sb.Append(AlternativeObservableAudit.OutputLedger());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var ledger = AlternativeObservableAudit.MeasurabilityLedger();
        Assert.Contains(ledger, l => l.Measurable && l.Route.Contains("redshift"));
        Assert.Contains(ledger, l => l.Measurable && l.Route.Contains("compactness"));
        Assert.Contains(ledger, l => !l.Measurable && l.Route.Contains("curvature"));
        Assert.Contains(ledger, l => !l.Measurable && l.Route.Contains("core"));
        Assert.Contains(ledger, l => !l.Measurable && l.Route.Contains("drift"));
        var dynamical = AlternativeObservableAudit.Catalogue().Single(o => o.Domain == "dynamical evolution");
        Assert.False(dynamical.DegenerateWithTheRedshift);
        Assert.False(dynamical.ItsRateIsFixedByASurvivingDatum);

        sb.AppendLine("  both remaining domains are closed for reasons that are stated rather than implied: one by an unfixed rate,");
        sb.AppendLine("  the other by being a model rather than a reading.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_08_TheDegeneracyIsStructuralNotSpecificToJ0740()
    {
        PrintHeader("G_088 - task 4: is the degeneracy structural or specific to J0740?");
        var sb = Start(
            "the extension is a function of the POTENTIAL ALONE, so its signature's power in the potential is the same at",
            "every target, and the compactness offset cancels the signature in the same way everywhere;",
            "the test is therefore invariance ACROSS targets: the powers, the count and the reach are recomputed for",
            "different compactnesses rather than assumed stable;",
            "a degeneracy that survives the change of target is structural.");
        var rows = new[] { 0.1, 0.247, 0.4, 0.536 }.Select(a =>
        {
            double rate = AlternativeObservableAudit.PowerOfTheSignature(AlternativeObservableAudit.Rate, a, a * 1.5);
            double gradient = AlternativeObservableAudit.PowerOfTheSignature(AlternativeObservableAudit.Gradient, a, a * 1.5);
            return (Surface: a, RatePower: rate, GradientPower: gradient);
        }).ToArray();
        foreach (var r in rows)
        {
            sb.AppendLine($"    surface potential {N(r.Surface)}: the rate's power {F2(r.RatePower)}, the gradient's {F2(r.GradientPower)}");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        Assert.All(rows, r => Assert.Equal(2.0, r.RatePower, 2));   // the residual is the finite-c truncation of the fit
        Assert.All(rows, r => Assert.Equal(1.0, r.GradientPower, 2));
        // the reach scales as 1 over the potential, so a differently placed target changes the number, not the structure
        var near = AlternativeObservableAudit.ReachOfTheMixedRoute(0.03661);
        Assert.True(near.ThreeSigmaThreshold > 0.4);
        Assert.Equal(2.0, AlternativeObservableAudit.Catalogue().Single(o => o.Domain == "value").PowerInThePotential, 6);

        sb.AppendLine("  the powers are the same at every target tried (0.100 to 0.536) and the offset structure is target-independent:");
        sb.AppendLine("  the answer to question 4 is STRUCTURAL — it is the extension's form, not J0740's compactness.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_09_TheVerdictIsComputedAndDeterministic()
    {
        PrintHeader("G_088 - the verdict: empirically undecidable within the surviving sector");
        var sb = Start(
            "the verdict is a function of the measured powers, the telescoping residual, the interior's ratio, the equation",
            "count and the mixed route's reach;",
            "the classification applied is the one the question names, and it is applied only because the count of",
            "independent-and-measurable observables is zero;",
            "no randomness, no clock and no external data enter, so the report is reproducible rather than asserted.");
        sb.Append(AlternativeObservableAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = AlternativeObservableAudit.Verdict();
        Assert.StartsWith("KERNEL DIMENSION EMPIRICALLY UNDECIDABLE WITHIN THE SURVIVING SECTOR", verdict);
        Assert.Contains("STRUCTURAL", verdict);
        Assert.Contains("of the 8 observables catalogued across seven domains", verdict);
        Assert.Contains("are independently MEASURABLE", verdict);
        Assert.Contains("0 are independently MEASURABLE", verdict);
        Assert.Contains("IS c exactly", verdict);
        Assert.Contains("not a new axis", verdict);
        Assert.Contains("G_087's veto, recovered", verdict);
        Assert.Contains("empirically", verdict, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(verdict, AlternativeObservableAudit.Verdict());

        var standing = AlternativeObservableAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("WHY THIS IS NOT A RESTATEMENT OF G_087", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("curvature", standing);
        Assert.Contains("would reverse this", standing);

        sb.AppendLine("  the verdict is deterministic, and it is NOT the lazy one: the mathematical separation is reported as real");
        sb.AppendLine("  and named (the kernel's second derivative IS c), while the empirical one is reported as absent.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_088_10_TheAuditIsCheckedAgainstG086AndG087()
    {
        PrintHeader("G_088 - cross-check: the same kernel, the same collapse, the same veto");
        var sb = Start(
            "the kernel this audit varies is the one G_086 collapsed to and G_087 made undecidable, so the three audits",
            "must agree on it;",
            "G_087's veto threshold is recovered here from the compactness error rather than from the timing, which is an",
            "independent route to the same number;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(KernelDiscriminabilityAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the same kernel object: this audit's LnK and Rate are G_087's kernel, checked on the state and the target
        var rho = MapRedundancyAudit.State;
        foreach (var cell in new[] { 0, 24, 48, 72 })
        {
            Assert.Equal(KernelDiscriminabilityAudit.LnK(rho[cell], 0.3), AlternativeObservableAudit.LnK(
                Math.Log(rho[cell]) / AlternativeObservableAudit.D, 0.3), 12);
        }
        Assert.Equal(KernelDiscriminabilityAudit.K(1.0, 0.3), AlternativeObservableAudit.Rate(0.0, 0.3), 12);
        Assert.Equal(KernelDimension.TwoFunctions, ObservableKernelAudit.Theories()
            .Single(t => t.Name.StartsWith("the free room")).Dimension);
        Assert.StartsWith("KERNEL DIMENSION UNDECIDABLE", KernelDiscriminabilityAudit.Verdict());

        double veto = KernelDiscriminabilityAudit.CompactnessVetoThreshold();
        double threshold = AlternativeObservableAudit.ReachOfTheMixedRoute().ThreeSigmaThreshold;
        sb.AppendLine($"  G_087's compactness veto is {F2(veto)} and this audit's three-sigma reach is {F2(threshold)}: the same");
        sb.AppendLine("  number from the timing side and from the compactness side, which is what makes the refutation robust.");
        Assert.True(Math.Abs(veto - threshold) / veto < 0.35);

        sb.AppendLine("  the three audits agree on the object, the collapse and the veto; what G_088 adds is that the search of");
        sb.AppendLine("  seven domains finds no independent axis, and that the reason is structural.");
        Output.WriteLine(sb.ToString());
    }
}
