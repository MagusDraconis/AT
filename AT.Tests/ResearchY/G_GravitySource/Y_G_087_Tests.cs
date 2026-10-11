using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_087 - KERNEL DISCRIMINABILITY AUDIT.
///
/// G_086 collapsed the observable kernel to the rate K(rho) alone, conditional on the clock law's logarithmic
/// derivative being 1 - the one-function kernel - and showed that the smallest extension restoring a second function is
/// the free room, ln K = A + c A^2 / 2. This suite asks the measurement question: **can any surviving measurement
/// distinguish the one-function kernel from the two-function one?**
///
/// The report follows the six tasks, prefers refutation, and computes every threshold by bisection rather than
/// estimating it. No spectral quantity is quoted.
/// </summary>
public sealed class Y_G_087_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_087_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string P(double v) => v.ToString("P4", System.Globalization.CultureInfo.InvariantCulture);
    private static string F1(double v) => v.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);

    private static StringBuilder Start(string task, params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_087_01_EveryObservableFromTheKernelAlone()
    {
        PrintHeader("G_087 - task 1: every surviving observable from K alone, and what the extension does to it");
        var sb = Start("task 1",
            "the kernel is the rate K(rho) = exp(A + c A^2 / 2) with A = ln rho / d, so c = 0 IS the one-function kernel",
            "G_086 collapsed the sector to;",
            "the readouts are taken at the state's CONTRAST sites (the deepest well and the highest hill, found by search",
            "rather than assumed) and at the target's compactness for the J0740 comparison;",
            "the two SOURCE-carrying readouts are carried as CONTROLS, because the extension changes the clock law and not",
            "the potential map;",
            "the signature at the target is computed with ExpM1, because it is a difference of nearly equal exponentials.");
        sb.Append(KernelDiscriminabilityAudit.OutputObservables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = KernelDiscriminabilityAudit.Observables();
        Assert.Equal(9, rows.Length);
        Assert.Equal(4, rows.Count(r => r.SourceCarrying));
        var (well, hill) = KernelDiscriminabilityAudit.ContrastSites();
        var rho = KernelDiscriminabilityAudit.State;
        Assert.True(rho[well] < 1.0 && rho[hill] > 1.0, "the contrast sites must straddle the mean density");
        // the MAP-HELD source readouts are exactly insensitive; the kernel re-cut ones move
        Assert.All(rows.Where(r => r.Observable.Contains("map held")), r => Assert.Equal(0.0, r.Signal, 12));
        Assert.All(rows.Where(r => r.Observable.Contains("kernel re-cut")), r => Assert.True(Math.Abs(r.Signal) > 0.0));
        Assert.All(rows.Where(r => !r.SourceCarrying), r => Assert.True(Math.Abs(r.Signal) > 0.0));

        sb.AppendLine("  every rate-based readout moves with the extension; the source-carrying readouts move when the kernel is");
        sb.AppendLine("  RE-CUT (the potential IS ln K in that reading) and are EXACTLY insensitive when the map is held — which is");
        sb.AppendLine("  why nothing already measured excludes the extension, and why the redshift is the robust discriminator.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_02_TheSmallestExtensionThatBreaksTheCollapse()
    {
        PrintHeader("G_087 - task 2: the smallest extension that breaks the collapse");
        var sb = Start("task 2",
            "the extension is ln K = A + c A^2 / 2: ONE parameter, second order in the potential;",
            "it is the SMALLEST extension because the constant and linear terms are pinned by the sector's own normalization",
            "and by G_086's collapse, so this is the first surviving coefficient;",
            "the test that it is an extension of the right kind is that it preserves the pinned data and scales with c.");
        var x = ObservationalProgramAudit.XOfTarget();
        foreach (var c in new[] { -1.0, -0.3, 0.3, 1.0 })
        {
            sb.AppendLine($"    c = {c,5:F1}: K(1) = {KernelDiscriminabilityAudit.K(1.0, c):F6}, the signature at the target "
                + $"{KernelDiscriminabilityAudit.Signature(x, c):E3}");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        foreach (var c in new[] { -1.0, -0.3, 0.3, 1.0 })
        {
            Assert.Equal(1.0, KernelDiscriminabilityAudit.K(1.0, c), 6);
        }
        Assert.Equal(0.0, KernelDiscriminabilityAudit.Signature(x, 0.0), 12);
        Assert.True(Math.Abs(KernelDiscriminabilityAudit.Signature(x, 1.0)) > 1e-3);

        // the extension is the SAME object G_086 identified: its effect is exactly proportional to c
        var rho = KernelDiscriminabilityAudit.State;
        double one = Math.Abs(KernelDiscriminabilityAudit.LnK(rho[0], 0.3) - KernelDiscriminabilityAudit.LnK(rho[0], 0.0));
        double two = Math.Abs(KernelDiscriminabilityAudit.LnK(rho[0], 0.6) - KernelDiscriminabilityAudit.LnK(rho[0], 0.0));
        Assert.Equal(2.0, two / one, 9);

        sb.AppendLine("  one parameter, the pinned data preserved at every member, and the effect exactly proportional to c: the");
        sb.AppendLine("  smallest extension is the second-order coefficient of the kernel, and nothing else.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_03_TheFirstSensitiveObservable()
    {
        PrintHeader("G_087 - task 3: the first observable sensitive to the extension");
        var sb = Start("task 3",
            "sensitivity is measured as the RELATIVE change of the readout per unit c, by a central difference in c with",
            "h = 1e-6;",
            "the ranking runs over every observable, with the source-carrying ones reported at exactly zero because they",
            "are the controls;",
            "the largest relative sensitivity is the observable a measurement should use FIRST.");
        sb.Append(KernelDiscriminabilityAudit.OutputRanking());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var ranking = KernelDiscriminabilityAudit.SensitivityRanking();
        Assert.Equal(9, ranking.Length);
        // the largest relative signature is the AT/GR comparison itself, whose own value is already second order
        Assert.StartsWith("the AT/GR comparison at the target", ranking[0].Observable);
        Assert.True(Math.Abs(ranking[0].RelativeSensitivityPerUnitC) > 1e-1,
            $"the AT/GR comparison must be sensitive at the 10-per-cent level ({ranking[0].RelativeSensitivityPerUnitC:E3})");
        // then the kernel re-cut source readouts, then the rate-based ones
        Assert.StartsWith("the acceleration at the well (kernel re-cut)", ranking[1].Observable);
        for (int i = 1; i < ranking.Length; i++)
        {
            Assert.True(Math.Abs(ranking[i].RelativeSensitivityPerUnitC)
                <= Math.Abs(ranking[i - 1].RelativeSensitivityPerUnitC));
        }
        Assert.All(ranking.TakeLast(2), r => Assert.Contains("map held", r.Observable));
        Assert.All(ranking.TakeLast(2), r => Assert.Equal(0.0, Math.Abs(r.RelativeSensitivityPerUnitC), 12));
        Assert.Contains(ranking, r => r.Observable.StartsWith("the redshift between the hill and the well"));

        sb.AppendLine($"  the first sensitive observable is the AT/GR COMPARISON at the target "
            + $"({N(ranking[0].RelativeSensitivityPerUnitC)} per unit c), then the kernel re-cut acceleration; the");
        sb.AppendLine("  rate-based readouts follow, and the map-held source readouts are exactly ZERO — so the redshift is the");
        sb.AppendLine("  discriminator that survives both readings of the extension.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_04_TheMinimumPrecision()
    {
        PrintHeader("G_087 - task 4: the minimum precision required");
        var sb = Start("task 4",
            "the shared-compactness model compares two kernels at ONE measured compactness, so it cancels to first order -",
            "the structure the project's own AT/GR comparison uses;",
            "the one-prediction model compares a prediction with data whose compactness carries its own error, so it must",
            "be at least as demanding;",
            "the error budget follows the project's own significance formula, so the numbers are comparable with the AT/GR",
            "programme's requirements.");
        sb.Append(KernelDiscriminabilityAudit.OutputPrecision());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var atOne = KernelDiscriminabilityAudit.MinimumPrecision(1.0);
        Assert.Equal(2, atOne.Length);
        double shared = atOne.Single(r => r.Model == SigmaModel.SharedX).RequiredRelativeSigmaZ;
        double single = atOne.Single(r => r.Model == SigmaModel.OnePrediction).RequiredRelativeSigmaZ;
        Assert.True(shared > 0.0 && shared < 0.2, $"the shared model must need a few per cent ({P(shared)})");
        Assert.True(double.IsNaN(single) || single <= shared,
            "the conservative model can never need LESS precision than the favourable one");

        var small = KernelDiscriminabilityAudit.MinimumPrecision(0.1).Single(r => r.Model == SigmaModel.SharedX);
        Assert.True(small.RequiredRelativeSigmaZ < shared * 0.5,
            "a ten-times smaller extension must need a proportionally FINER measurement");

        sb.AppendLine($"  |c| = 1 needs {P(shared)} (shared compactness) or {P(single)} (one prediction) at three sigma, and the");
        sb.AppendLine("  requirement scales inversely with the extension's size.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_05_J0740ReEvaluated()
    {
        PrintHeader("G_087 - task 5: J0740 re-evaluated against every recorded instrument class");
        var sb = Start("task 5",
            "the classes, their timing precisions and their mass and radius marginals are the programme's own recorded",
            "capabilities, read through its own API rather than retyped;",
            "the compactness error follows the programme's conservative independent-error rule;",
            "the smallest detectable extension is solved by bisection, not estimated, and the binding error is reported");
        sb.Append(KernelDiscriminabilityAudit.OutputJ0740());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var thresholds = KernelDiscriminabilityAudit.ThresholdsByClass();
        Assert.Equal(ObservationalProgramAudit.Classes().Length, thresholds.Length);
        var current = thresholds[0];
        var best = thresholds.OrderBy(t => t.SmallestDetectableC).First();
        sb.AppendLine($"  the current class reaches |c| = {F1(current.SmallestDetectableC)} and the best recorded one "
            + $"{F1(best.SmallestDetectableC)} ('{best.InstrumentClass}')");
        Assert.True(current.SmallestDetectableC > 2.0,
            $"the current class must offer no useful constraint ({F1(current.SmallestDetectableC)})");
        Assert.All(thresholds, t => Assert.True(t.SmallestDetectableC > 0.1,
            $"{t.InstrumentClass} must still have a finite threshold ({F1(t.SmallestDetectableC)})"));
        Assert.True(best.SmallestDetectableC < current.SmallestDetectableC);

        sb.AppendLine("  a natural extension of order one is out of reach for the current and near-term classes, and the only");
        sb.AppendLine("  class that comes close is the direct spectroscopic redshift - which is stated rather than glossed.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_06_TheExtensionIsSecondOrderAndDegenerateWithTheCompactness()
    {
        PrintHeader("G_087 - task 5 and 6: the extension is second order, and degenerate with the compactness error");
        var sb = Start("task 5 and 6",
            "the extension's signature at the target is second order in the compactness, so it competes directly with the",
            "AT/GR separation the same programme is chasing;",
            "the degeneracy is measured: the compactness shift that mimics |c| = 1 is compared with the target's own",
            "compactness error;",
            "the decisive comparison is the ratio of the two signatures, which says how much finer a measurement the SAME");
        var against = KernelDiscriminabilityAudit.AgainstTheSeparation(0.3);
        var x = ObservationalProgramAudit.XOfTarget();
        double shift = KernelDiscriminabilityAudit.EquivalentCompactnessShift(x, 1.0);
        var target = ObservationalProgramAudit.Target();
        sb.AppendLine($"    at c = {against.C:F2}: the extension's signature {N(against.Signature)} against the AT/GR separation "
            + $"{N(against.AtGrSeparation)} — a factor {F1(against.Ratio)}");
        sb.AppendLine($"    |c| = 1 mimics a compactness shift of {N(shift)} = {P(Math.Abs(shift / x))} of the compactness "
            + $"(magnitudes: the compactness is negative in the project's convention), against the recorded compactness "
            + $"error {P(target.RelativeSigmaX)}");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        Assert.Equal(0.3, against.C, 12);
        Assert.True(against.Signature > 0.0 && against.AtGrSeparation > against.Signature,
            "the AT/GR separation the programme chases must be LARGER than the extension's signature");
        Assert.True(against.Ratio > 3.0, $"the kernel decision needs a finer measurement by {F1(against.Ratio)}");
        Assert.True(Math.Abs(shift / x) > target.RelativeSigmaX,
            $"a unit extension mimics more than the compactness error, so the TIMING is the binding constraint "
            + $"(shift {N(shift)}, |x| {Math.Abs(x):E6}, ratio {P(Math.Abs(shift / x))}, "
            + $"compactness error {P(target.RelativeSigmaX)})");

        // and there is a VETO independent of the instrument: below it the compactness error forbids the decision
        double veto = KernelDiscriminabilityAudit.CompactnessVetoThreshold();
        Assert.True(veto > 0.1 && veto < 2.0, $"the compactness veto must land in a natural range ({veto:F2})");
        Assert.True(double.IsNaN(KernelDiscriminabilityAudit.MinimumPrecision(0.1)
            .Single(r => r.Model == SigmaModel.OnePrediction).RequiredRelativeSigmaZ),
            "below the veto the conservative model must be unreachable, not merely demanding");

        sb.AppendLine($"  the kernel's dimension sits a factor {F1(against.Ratio)} below the effect the programme is already");
        sb.AppendLine($"  chasing, and a unit extension mimics {P(Math.Abs(shift) / x)} of compactness — more than the recorded "
            + $"{P(target.RelativeSigmaX)} — so the TIMING binds; below |c| = {veto:F2} the compactness error VETOES");
        sb.AppendLine("  a three-sigma decision at any redshift precision.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_07_NoCurrentOrPlannedExperimentCanDecideIt()
    {
        PrintHeader("G_087 - task 6: can any current or planned experiment decide the kernel's dimension?");
        var sb = Start("task 6",
            "the answer is REFUTED when no recorded class reaches a natural size of the extension;",
            "'natural' is taken as |c| of order one, the size at which the extension is a 100 per cent correction to the",
            "second-order term of the potential's logarithm;",
            "the decisive number is stated in the same form the programme uses for its own decision.");
        sb.Append(KernelDiscriminabilityAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = KernelDiscriminabilityAudit.Verdict();
        Assert.StartsWith("KERNEL DIMENSION", verdict);
        Assert.Contains("exactly", verdict);
        Assert.Contains("ZERO", verdict);
        Assert.Contains("VETO", verdict);
        Assert.Contains("second the kernel re-cut acceleration", verdict);
        Assert.Contains("ExpM1", verdict);
        Assert.Equal(verdict, KernelDiscriminabilityAudit.Verdict());

        var standing = KernelDiscriminabilityAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("AND THE DECISION IS NOT THE ONE THE PROGRAMME IS CHASING", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("DEGENERATE", standing);

        // the refutation holds for the current and near-term classes; the most advanced is named with its number
        var thresholds = KernelDiscriminabilityAudit.ThresholdsByClass();
        Assert.True(thresholds.Take(4).All(t => t.SmallestDetectableC > 1.0),
            "the current and near-term classes must all sit above a natural extension");
        var best = thresholds.OrderBy(t => t.SmallestDetectableC).First();
        sb.AppendLine($"  REFUTED for the recorded and near-term classes (all above |c| = 1); the best recorded class "
            + $"('{best.InstrumentClass}') would reach |c| ≈ {F1(best.SmallestDetectableC)}, which a natural "
            + "extension of order one is not excluded by either.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_08_TheSignatureIsComputedTheStableWay()
    {
        PrintHeader("G_087 - the numerics: the signature is a difference of nearly equal exponentials");
        var sb = Start("the numerics",
            "the signature is z(c) - z(0) with z = exp(-x - c x^2 / 2) - 1, a difference of nearly equal terms at small c,",
            "which the numerical-reproducibility rules name as a hazard;",
            "it is therefore written as exp(-x) * ExpM1(-c x^2 / 2), which is exact and stable;",
            "the naive subtraction is CARRIED and MEASURED, so the failure scale is reported rather than assumed.");
        sb.Append(KernelDiscriminabilityAudit.OutputNumerics());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var stability = KernelDiscriminabilityAudit.StabilityOfTheSignature(ObservationalProgramAudit.XOfTarget());
        Assert.Equal(7, stability.Length);
        Assert.True(stability.Single(s => s.C == 1e-2).RelativeError < 1e-10,
            "at c = 1e-2 the naive form must still be exact");
        // the error grows as the extension shrinks, and it eventually swallows the signature
        var errors = stability.Select(s => s.RelativeError).ToArray();
        for (int i = 1; i < errors.Length; i++) Assert.True(errors[i] >= errors[i - 1]);
        Assert.True(errors[^1] > 1e-2, $"at the smallest c the naive form must fail clearly ({errors[^1]:E3})");
        Assert.All(stability, s => Assert.True(s.Stable != 0.0, "the stable form must never return zero"));

        sb.AppendLine($"  the naive subtraction is exact down to c ~ 1e-6 and fails below it (relative error "
            + $"{errors[^1]:E3} at c = {stability[^1].C:E0}); the audit uses ExpM1, so no threshold rests on a");
        sb.AppendLine("  cancellation-degraded number.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_09_TheModelComparisonIsJudgedBothWays()
    {
        PrintHeader("G_087 - task 4, continued: both error models, and which one binds");
        var sb = Start("task 4, continued",
            "the programme's own AT/GR decision is judged under two models (a shared compactness and a single theory",
            "against data), so the kernel decision is judged the same way;",
            "the shared-compactness model is the favourable one and the single-prediction model the conservative one, and",
            "both are reported;",
            "the binding error is measured, not chosen.");
        var rows = new[] { 0.1, 1.0, 10.0 }.SelectMany(c => KernelDiscriminabilityAudit.MinimumPrecision(c)).ToArray();
        foreach (var r in rows)
        {
            sb.AppendLine($"    |c| = {r.C,5:F1}  {KernelDiscriminabilityAudit.ModelName(r.Model),-48} 3 sigma needs "
                + $"{P(r.RequiredRelativeSigmaZ)}  ({r.WhichBinds})");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        Assert.All(rows, r => Assert.True(double.IsNaN(r.RequiredRelativeSigmaZ) || r.RequiredRelativeSigmaZ > 0.0));
        Assert.Contains(rows, r => r.Model == SigmaModel.SharedX);
        Assert.Contains(rows, r => r.Model == SigmaModel.OnePrediction);
        var atOne = KernelDiscriminabilityAudit.MinimumPrecision(1.0);
        double shared = atOne.Single(r => r.Model == SigmaModel.SharedX).RequiredRelativeSigmaZ;
        double single = atOne.Single(r => r.Model == SigmaModel.OnePrediction).RequiredRelativeSigmaZ;
        Assert.True(double.IsNaN(single) || single <= shared,
            "the conservative model can never be looser than the favourable one");
        foreach (var c in new[] { 0.1, 1.0, 10.0 })
        {
            var row = KernelDiscriminabilityAudit.MinimumPrecision(c).Single(r => r.Model == SigmaModel.SharedX);
            Assert.True(row.RequiredRelativeSigmaZ > 0.0 && row.RequiredRelativeSigmaZ < 1.0);
        }

        sb.AppendLine("  both models are computed, the conservative one is never looser, and the binding error is named in every");
        sb.AppendLine("  row - including the case where the compactness error alone vetoes the decision.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_087_10_TheExtensionIsTheOneG085AndG086Identified()
    {
        PrintHeader("G_087 - cross-check: the extension is the free room G_085 found and G_086 collapsed");
        var sb = Start("the cross-check",
            "the free room is ONE parameter, and this audit's c IS that parameter;",
            "G_086 measured its mismatch field as the exact field c ln(rho) / (d^2 rho) - the audit must reproduce that",
            "field from its own kernel;",
            "G_085's free-room family is the same object, so the thresholds computed here apply to the parameter that",
            "audit left undetermined.");
        sb.Append(ObservableKernelAudit.OutputMismatch());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rho = KernelDiscriminabilityAudit.State;
        const double c = 0.3;
        int checkedCells = 0;
        foreach (var r in rho)
        {
            if (Math.Abs(r - 1.0) < 1e-6) continue;
            double source = 1.0 / (KernelDiscriminabilityAudit.D * r);
            double logDerivative = (1.0 + c * Math.Log(r) / KernelDiscriminabilityAudit.D)
                / (KernelDiscriminabilityAudit.D * r);
            double expected = -c * Math.Log(r)
                / (KernelDiscriminabilityAudit.D * KernelDiscriminabilityAudit.D * r);
            Assert.Equal(expected, source - logDerivative, 9);
            checkedCells++;
        }
        Assert.True(checkedCells >= 90, $"only {checkedCells} cells carried a measurable mismatch");

        var freeRoomRow = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room"));
        Assert.Contains("free room", freeRoomRow.Variation);
        Assert.Equal(KernelDimension.TwoFunctions, ObservableKernelAudit.Theories()
            .Single(t => t.Name.StartsWith("the free room")).Dimension);
        Assert.True(freeRoomRow.PredictionsMoved > 0, "G_085 measured the free room moving a prediction");

        sb.AppendLine($"  the mismatch field is the exact field c ln(rho) / (d^2 rho), checked on {checkedCells} cells, and");
        sb.AppendLine("  G_085's free-room row is the same object: the thresholds computed here apply to the single parameter");
        sb.AppendLine("  that audit left undetermined and G_086 showed would restore a second function.");
        Output.WriteLine(sb.ToString());
    }
}
