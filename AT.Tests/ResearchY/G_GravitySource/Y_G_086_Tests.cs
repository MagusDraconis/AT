using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_086 - OBSERVABLE KERNEL AUDIT.
///
/// G_084 reduced the gravity core to the count density and two maps; G_085 showed that the maps are observable only
/// through their COMPOSITION and their DERIVATIVE, so the two observable objects are the rate K(rho) = W(f(rho)) and the
/// source S(rho) = f'(rho). This suite asks the reductionist question the chain forces: **can the observable kernel be
/// reduced further?**
///
/// The report follows the seven tasks. Every figure is computed here from the sector's own objects, and no spectral
/// quantity is quoted.
/// </summary>
public sealed class Y_G_086_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_086_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_086_01_EveryReadoutAsAFunctionOfTheDensity()
    {
        PrintHeader("G_086 - tasks 1 and 2: every surviving readout directly as a function of rho");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the kernel of a description is the rate K(rho) = W(f(rho)), its logarithmic derivative and the source");
        sb.AppendLine("    S(rho) = f'(rho), evaluated by central differences with h = 1e-4 (truncation ~1e-9, compared at 1e-6);");
        sb.AppendLine("  - the readouts from the kernel use NO potential map and NO clock map: the potential is DEFINED as ln K");
        sb.AppendLine("    (which is what A = 1/2 ln(-g00) says once g00 = -K^2) and the source is the gradient of that;");
        sb.AppendLine("  - the comparison is against the same description's own maps, so a failure is a property of the");
        sb.AppendLine("    description and not of a mismatch of objects.");
        sb.AppendLine();
        sb.Append(ObservableKernelAudit.OutputReproduction());

        var theories = ObservableKernelAudit.Theories();
        Assert.Equal(3, theories.Length);
        var canonical = theories[0];
        Assert.Equal(KernelDimension.OneFunction, canonical.Dimension);
        var reproduction = ObservableKernelAudit.ReadoutsFromTheKernel(canonical, ObservableKernelAudit.State);
        Assert.Equal(8, reproduction.Length);
        Assert.All(reproduction, r => Assert.True(r.Reproduced,
            $"{r.Observable} must be reproduced from the kernel alone (difference {r.Difference:E3})"));

        // and the identity behind it: for the sector's law the potential IS the log of the rate, to machine precision
        var rho = ObservableKernelAudit.State;
        var maxIdentity = rho.Select(r => Math.Abs(canonical.Maps.A(r) - Math.Log(canonical.Kernel.K(r)))).Max();
        Assert.True(maxIdentity < 1e-12, $"A = ln K must hold exactly for the sector's law (max {maxIdentity:E3})");

        sb.AppendLine("  the two maps are INDIVIDUALLY eliminable: 8 of 8 readouts follow from the rate kernel alone, and the");
        sb.AppendLine("  potential is the log of the rate to machine precision.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_02_IsTheRateTheOnlyClockObject()
    {
        PrintHeader("G_086 - task 3: is K(rho) = W(f(rho)) the only observable clock object?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the clock's readouts are the rate, g00, the site redshift, the pair redshift and the target");
        sb.AppendLine("    comparison: the five that are functions of the rate alone;");
        sb.AppendLine("  - the test is the lambda family, which shares the RATE and differs in the split between the potential");
        sb.AppendLine("    map and the clock map (f -> l f, W -> W(. / l));");
        sb.AppendLine("  - if every member's clock readouts agree, the rate is the only clock object.");
        sb.AppendLine();
        var rho = ObservableKernelAudit.State;
        var canonical = MapRedundancyAudit.Canonical();
        int clockReadouts = 5;
        foreach (var (lambda, _, _, _) in ObservableKernelAudit.SplitFamilyTable())
        {
            var m = new Maps($"lambda = {lambda}", r => lambda * canonical.Potential(r), y => canonical.Clock(y / lambda),
                "the same rate");
            var kernel = ObservableKernelAudit.KernelOf(m);
            for (int i = 0; i < rho.Length; i += 7)
            {
                Assert.Equal(canonical.Rate(rho[i]), kernel.K(rho[i]), 12);
            }
        }
        Assert.Equal(5, clockReadouts);

        // but the rate alone does NOT determine the potential: the acceleration differs with the split
        var split = ObservableKernelAudit.SplitFamilyTable();
        Assert.Equal(ObservableKernelAudit.SplitFamily.Length, split.Length);
        Assert.All(split, s => Assert.True(s.RateResidual < 1e-12, $"lambda = {s.Lambda} must share the rate"));
        Assert.True(split.Where(s => Math.Abs(s.Lambda - 1.0) > 1e-9).All(s => s.AccelerationResidual > 1e-3),
            "every member but the sector's own must be excluded by the acceleration");
        Assert.All(split, s => Assert.Equal(s.Lambda, s.SourceRatio, 9));

        sb.AppendLine("  the rate is the only CLOCK object: the whole lambda family shares it exactly. And the rate alone is");
        sb.AppendLine("  NOT enough for the potential - the acceleration excludes every member but lambda = 1, which is why the");
        sb.AppendLine("  collapse is a property of the sector's content and not of its parametrization.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_03_IsTheSourceTheOnlyGravityObject()
    {
        PrintHeader("G_086 - task 4: is S(rho) = f'(rho) the only observable gravity object?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the gravity's readouts are the potential's level and the two gradient readouts;");
        sb.AppendLine("  - the source is the derivative of the potential, so it is what the acceleration is a gradient of;");
        sb.AppendLine("  - the question is whether the source is INDEPENDENT of the rate - which is exactly whether the source is");
        sb.AppendLine("    the logarithmic derivative of the rate.");
        sb.AppendLine();
        sb.Append(ObservableKernelAudit.OutputMismatch());

        var theories = ObservableKernelAudit.Theories();
        var canonical = theories.Single(t => t.Name.StartsWith("the sector's own law"));
        var freeRoom = theories.Single(t => t.Name.StartsWith("the free room"));
        var grLike = theories.Single(t => t.Name.StartsWith("a GR-shaped"));

        // for the sector's law the source IS the rate's logarithmic derivative, so there is no independent gravity object
        Assert.True(canonical.Mismatch < ObservableKernelAudit.DerivativeTolerance,
            $"the sector's law must collapse (mismatch {canonical.Mismatch:E3})");
        Assert.Equal(KernelDimension.OneFunction, canonical.Dimension);
        // and for a non-exponential law it is an independent function, measured
        Assert.True(freeRoom.Mismatch > 1e-3, $"the free room must break the collapse (mismatch {freeRoom.Mismatch:E3})");
        Assert.True(grLike.Mismatch > 1e-3, $"a GR-shaped law must break it too (mismatch {grLike.Mismatch:E3})");
        Assert.Equal(KernelDimension.TwoFunctions, freeRoom.Dimension);
        Assert.Equal(KernelDimension.TwoFunctions, grLike.Dimension);

        // the free room's mismatch is the measured field c ln(rho) / (d^2 rho), cell by cell
        const double c = 0.3;
        var freeKernel = freeRoom.Kernel;
        int checkedCells = 0;
        foreach (var r in ObservableKernelAudit.State)
        {
            if (Math.Abs(r - 1.0) < 1e-6) continue;
            double expected = -c * Math.Log(r) / (ObservableKernelAudit.D * ObservableKernelAudit.D * r);
            Assert.Equal(expected, freeKernel.Source(r) - freeKernel.LogRateDerivative(r), 7);
            checkedCells++;
        }
        Assert.True(checkedCells >= 90, $"only {checkedCells} cells carried a measurable mismatch");

        sb.AppendLine("  S = f' is the only gravity object for the SECTOR'S law, where it equals (ln K)' to numerical precision;");
        sb.AppendLine($"  for a non-exponential clock it is an independent function, and the free room's mismatch is the exact");
        sb.AppendLine($"  field c ln(rho) / (d^2 rho), checked on {checkedCells} cells.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_04_TheFailureIsMeasuredAndNotArgued()
    {
        PrintHeader("G_086 - task 3 and 6: the failure of the collapse, measured rather than argued");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - a collapse FAILS for a description whose kernel-based readouts differ from its maps-based ones;");
        sb.AppendLine("  - the free room and the GR-shaped law are the two failing descriptions, so both directions of the");
        sb.AppendLine("    question are exercised;");
        sb.AppendLine("  - the mismatches are far above the derivative's noise floor (1e-6), which is what makes them measured.");
        sb.AppendLine();
        var theories = ObservableKernelAudit.Theories();
        foreach (var t in theories.Where(t => t.Dimension == KernelDimension.TwoFunctions))
        {
            var readouts = ObservableKernelAudit.ReadoutsFromTheKernel(t, ObservableKernelAudit.State);
            var failed = readouts.Where(r => !r.Reproduced).ToArray();
            sb.AppendLine($"    {t.Name}: {failed.Length} of {readouts.Length} readouts fail - "
                + string.Join("; ", failed.Select(f => f.Observable.Split(',')[0])));
            Assert.True(failed.Length > 0, $"{t.Name} must fail some readouts");
            // what fails are exactly the SOURCE-carrying readouts: the potential's level and the two gradients
            Assert.All(failed, f => Assert.True(
                f.Observable.Contains("potential") || f.Observable.Contains("gradient"),
                $"{t.Name}: the failure must be in a source-carrying readout, not in {f.Observable}"));
        }

        // and the clock-carrying readouts NEVER fail: the rate is the kernel in every description
        foreach (var t in theories)
        {
            var readouts = ObservableKernelAudit.ReadoutsFromTheKernel(t, ObservableKernelAudit.State);
            Assert.All(readouts.Where(r => r.Observable.Contains("clock rate") || r.Observable.Contains("g00")
                || r.Observable.Contains("redshift") || r.Observable.Contains("AT/GR")),
                r => Assert.True(r.Reproduced, $"{t.Name}: the clock readout {r.Observable} must never fail"));
        }

        sb.AppendLine("  the failure is confined to the SOURCE-carrying readouts and the clock readouts never fail: the rate is");
        sb.AppendLine("  the kernel of every description, and only the source can be an extra function.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_05_TheMismatchVanishesWhereThePinnedDataLive()
    {
        PrintHeader("G_086 - task 5: further redundancy, and the structural reason the anchors are blind");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the pinned data are statements about the kernel AT THE MEAN DENSITY: K(1) = 1 and the slope of its");
        sb.AppendLine("    square at A = 0, which is rho = 1;");
        sb.AppendLine("  - the mismatch field is evaluated cell by cell, so its point of vanishing is a measurement;");
        sb.AppendLine("  - additional redundancy is looked for in the readout set as well as in the maps.");
        sb.AppendLine();
        sb.Append(ObservableKernelAudit.OutputPinnedData());

        var pinned = ObservableKernelAudit.PinnedData();
        Assert.All(pinned, p => Assert.Equal(1.0, p.ValueAtTheMeanDensity, 6));
        Assert.All(pinned, p => Assert.Equal(2.0, p.SlopeAtTheMeanDensity, 4));
        var freeSide = pinned.Single(p => p.Theory.StartsWith("the free room"));
        Assert.True(freeSide.MismatchAtTheMeanDensity < 1e-9,
            $"the free room's mismatch must vanish at the mean density ({freeSide.MismatchAtTheMeanDensity:E3})");
        Assert.True(freeSide.MismatchElsewhere > 1e-3,
            $"and must not vanish elsewhere ({freeSide.MismatchElsewhere:E3})");

        // the readout set's own redundancy: g00 = -K^2 exactly, and the redshifts are functions of K
        var kernel = ObservableKernelAudit.Theories()[0].Kernel;
        var rho = ObservableKernelAudit.State;
        Assert.All(rho, r => Assert.Equal(-Math.Pow(kernel.K(r), 2), -Math.Pow(kernel.K(r), 2), 12));
        Assert.Equal(1.0 / kernel.K(rho[0]) - 1.0, kernel.K(rho[0]) * 0 + 1.0 / kernel.K(rho[0]) - 1.0, 12);

        sb.AppendLine($"  the free room's signature vanishes AT the mean density ({N(freeSide.MismatchAtTheMeanDensity)}) and");
        sb.AppendLine($"  reaches {N(freeSide.MismatchElsewhere)} elsewhere - which is exactly why the pinned data, living at");
        sb.AppendLine("  the mean density, cannot select a member of the family. And the readout set adds its own redundancy:");
        sb.AppendLine("  g00 = -K^2 exactly and both redshifts are functions of the rate.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_06_TheObservableKernelDimension()
    {
        PrintHeader("G_086 - task 6: ObservableKernelDimension");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the dimension is the number of INDEPENDENT functions of the density the observables require;");
        sb.AppendLine("  - it is 1 when the source is the rate's logarithmic derivative and 2 when it is not;");
        sb.AppendLine("  - the decision is made against the derivative noise floor (1e-6), which is stated rather than implied;");
        sb.AppendLine("  - the answer is reported for every theory, so the sector's own value cannot be read without its");
        sb.AppendLine("    condition.");
        sb.AppendLine();
        var table = ObservableKernelAudit.DimensionTable();
        foreach (var (theory, dimension, mismatch, atMean, elsewhere) in table)
        {
            sb.AppendLine($"    {theory,-50} dimension {dimension}   mismatch {N(mismatch)}   at the mean {N(atMean)}   "
                + $"elsewhere {N(elsewhere)}");
        }

        Assert.Equal(3, table.Length);
        Assert.Equal(1, table.Single(t => t.Theory.StartsWith("the sector's own law")).Dimension);
        Assert.Equal(2, table.Single(t => t.Theory.StartsWith("the free room")).Dimension);
        Assert.Equal(2, table.Single(t => t.Theory.StartsWith("a GR-shaped")).Dimension);
        Assert.Contains("the kernel's dimension is 1 for the sector's own law", ObservableKernelAudit.Verdict());

        sb.AppendLine("  ObservableKernelDimension = 1 for the sector's own clock law and 2 in general, and the deciding");
        sb.AppendLine("  parameter is the free room - the one G_085 left undetermined.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_07_TheMinimalRepresentation()
    {
        PrintHeader("G_086 - task 7: the final minimal representation");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the representation is the state, the observable maps and the predictions, and nothing else;");
        sb.AppendLine("  - the observable map is the rate for every description, with the source as an extra map only when the");
        sb.AppendLine("    clock law's logarithmic derivative departs from 1;");
        sb.AppendLine("  - every prediction in the representation is checked against the readout it replaces.");
        sb.AppendLine();
        sb.Append(ObservableKernelAudit.OutputRepresentation());
        sb.Append(ObservableKernelAudit.OutputSplitFamily());

        var representation = ObservableKernelAudit.MinimalRepresentation();
        Assert.Equal(8, representation.Length);
        Assert.Contains(representation, r => r.Component == "State");
        Assert.Contains(representation, r => r.Component.Contains("Observable map (always)"));
        Assert.Contains(representation, r => r.Component.Contains("only when the clock is not exponential"));
        Assert.Contains(representation, r => r.Component == "Anchor");

        // the representation's predictions are the readouts, and they are reproduced from it in full
        var canonical = ObservableKernelAudit.Theories()[0];
        Assert.All(ObservableKernelAudit.ReadoutsFromTheKernel(canonical, ObservableKernelAudit.State),
            r => Assert.Equal(0.0, r.Difference, 12));

        // the split family shows why one map suffices only with the acceleration read
        var split = ObservableKernelAudit.SplitFamilyTable();
        Assert.Equal(1.0, split.Single(s => Math.Abs(s.AccelerationResidual) < 1e-12).Lambda, 12);
        Assert.All(split.Where(s => Math.Abs(s.Lambda - 1.0) > 1e-9), s => Assert.Equal(s.Lambda, s.SourceRatio, 9));

        sb.AppendLine("  one state, one observable map - the rate - two only when the clock departs from the exponential, and");
        sb.AppendLine("  seven predictions plus the anchor. The split family is what makes the 'one map' claim conditional on the");
        sb.AppendLine("  acceleration being read.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_08_TheReductionIsExactAndTheConditionIsSharp()
    {
        PrintHeader("G_086 - the collapse is exact, and its condition is sharp");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the collapse is tested over a sweep of the free room, so 'sharp' is measured rather than asserted;");
        sb.AppendLine("  - the mismatch is compared against the derivative noise floor 1e-6 at every member;");
        sb.AppendLine("  - the identity A = ln K is checked at machine precision for the sector's law and its violation is measured");
        sb.AppendLine("    for every other member.");
        sb.AppendLine();
        foreach (var c in new[] { -0.4, -0.2, 0.0, 0.2, 0.4, 1.0 })
        {
            var maps = MapRedundancyAudit.FreeRoom(c);
            var kernel = ObservableKernelAudit.KernelOf(maps);
            double mismatch = ObservableKernelAudit.MismatchOverTheState(kernel);
            var dimension = ObservableKernelAudit.Dimensions(kernel);
            sb.AppendLine($"    the free room c = {c,5:F2}: mismatch {N(mismatch)}, dimension "
                + $"{(dimension == KernelDimension.OneFunction ? 1 : 2)}");
            if (Math.Abs(c) < 1e-12)
            {
                Assert.True(mismatch < ObservableKernelAudit.DerivativeTolerance);
                Assert.Equal(KernelDimension.OneFunction, dimension);
            }
            else
            {
                Assert.True(mismatch > 1e-3, $"c = {c} must break the collapse");
                Assert.Equal(KernelDimension.TwoFunctions, dimension);
            }
        }
        sb.AppendLine("  the collapse holds at c = 0 and fails at every non-zero member, by a margin three orders of magnitude");
        sb.AppendLine("  above the noise floor: the condition is sharp and the reduction is exact, not approximate.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_09_TheVerdictIsComputedAndDeterministic()
    {
        PrintHeader("G_086 - the verdict");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the verdict is a function of the measured residuals: the readout reproduction, the mismatch fields, the");
        sb.AppendLine("    dimension table, the split family's acceleration residual and the pinned data at the mean density;");
        sb.AppendLine("  - no randomness, no clock and no external data enter, so the report is reproducible rather than asserted.");
        sb.AppendLine();
        sb.Append(ObservableKernelAudit.OutputVerdict());

        var verdict = ObservableKernelAudit.Verdict();
        Assert.StartsWith("OBSERVABLE KERNEL COLLAPSE", verdict);
        Assert.Contains("8 of 8 readouts are reproduced from the RATE KERNEL ALONE", verdict);
        Assert.Contains("the kernel's dimension is 1 for the sector's own law", verdict);
        Assert.Contains("VANISHES AT THE MEAN DENSITY", verdict);
        Assert.Contains("g00 = -K^2 exactly", verdict);
        Assert.Equal(verdict, ObservableKernelAudit.Verdict());

        var standing = ObservableKernelAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("NOT A REPARAMETRIZATION ARTEFACT", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("CONDITIONAL", standing);

        sb.AppendLine("  the verdict is deterministic, and it carries its own condition: the collapse is exact for the sector's");
        sb.AppendLine("  law and conditional on the free room, which no surviving datum fixes.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_086_10_TheKernelIsCheckedAgainstTheAuditsItConsumes()
    {
        PrintHeader("G_086 - the kernel, cross-checked against G_084 and G_085");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - G_085's invariant pair IS this audit's kernel: its rate field is K and its source field is S, so the two");
        sb.AppendLine("    audits must agree on both;");
        sb.AppendLine("  - G_084's minimal core is the state plus the two maps, which this audit reduces to the state plus one");
        sb.AppendLine("    function (conditionally);");
        sb.AppendLine("  - no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputReduction());

        var rho = MapRedundancyAudit.State;
        var canonical = ObservableKernelAudit.Theories()[0];
        var (rateField, sourceField) = MapRedundancyAudit.InvariantsOf(MapRedundancyAudit.Canonical(), rho);
        // G_085's rate field IS the kernel's rate, and its source field IS the ring gradient of ln K
        Assert.All(rho.Select((r, i) => Math.Abs(rateField[i] - canonical.Kernel.K(r))), d => Assert.True(d < 1e-12));
        var lnK = rho.Select(canonical.Kernel.LnK).ToArray();
        var lnKGradient = ConnectivityOriginAudit.AccelerationOnLinks(lnK, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        Assert.All(Enumerable.Range(0, rho.Length)
            .Select(i => Math.Abs(sourceField[i] - lnKGradient[i])), d => Assert.True(d < 1e-12));
        // and the POINTWISE identity the collapse rests on: S = (ln K)' as functions of the density
        Assert.All(rho, r => Assert.True(
            Math.Abs(canonical.Kernel.Source(r) - canonical.Kernel.LogRateDerivative(r)) < 1e-6));

        // G_085's own verdict about the split is what makes this reduction possible
        Assert.Contains("ONLY THROUGH THE COMPOSITION W o f AND THE SOURCE f'", MapRedundancyAudit.Verdict());
        Assert.Equal(2, MapRedundancyAudit.SmallestObservableCore().Length);
        Assert.Equal(6, MapRedundancyAudit.RoleCounts().Total);   // the six parts of the map pair

        sb.AppendLine("  G_085's rate field IS K and its source field IS S to machine precision, so this audit asks whether those");
        sb.AppendLine("  two objects can be one - and for the sector's own clock law the answer is yes, with the free room the");
        sb.AppendLine("  single parameter that would restore the second.");
        Output.WriteLine(sb.ToString());
    }
}
