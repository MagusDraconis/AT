using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_079 - CONTINUITY LAW AUDIT.
///
/// G_077 made rho the sole surviving clock source; G_078 found transport only, with no source and no sink in the
/// surviving class. This suite asks whether `d_t rho + div J = 0` is FORCED - and, if it is, what the forcing rests on.
/// A proof of continuity elevates transport from an observation to a law; any surviving source term refutes the
/// transport-only conclusion.
///
/// The report follows the six questions of the audit assignment. Every number quoted is computed here.
/// </summary>
public sealed class Y_G_079_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_079_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_079_01_EverySurvivingUpdate_RewrittenAsAContinuityEquation()
    {
        PrintHeader("G_079 - Q1: can every surviving update rule be rewritten as a continuity equation?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the source is the local occupancy rho of the actualization count (G_077);");
        sb.AppendLine("  - the surviving update class is the four operators of G_078 plus an explicit transport;");
        sb.AppendLine("  - the substrate is the canonical CLOSED ring, so the total has nowhere to leave;");
        sb.AppendLine("  - the decomposition is checked in EXACT INTEGER ARITHMETIC, because a continuity law that held only");
        sb.AppendLine("    to a tolerance would be a weaker claim than one that holds exactly.");
        sb.AppendLine();
        sb.Append(ContinuityLawAudit.OutputContinuity());

        var rows = ContinuityLawAudit.UpdateClassAsContinuity();
        var classRows = rows.Where(r => !r.Operator.StartsWith("CONTROL")).ToArray();
        var control = rows.Single(r => r.Operator.StartsWith("CONTROL"));

        // every conserving operator of the class admits a flux, reconstructed exactly
        Assert.All(classRows, r =>
        {
            Assert.True(r.Conserves, $"{r.Operator}: the class must conserve the count");
            Assert.True(r.IsATransport, $"{r.Operator}: must lie in the image of the divergence");
            Assert.Equal(0, r.ReconstructionResidual);
            Assert.True(r.ReconstructsExactly);
        });

        // and the criterion is LIVE: the control must fail it, otherwise success means nothing
        Assert.False(control.IsATransport, "the non-divergent control must fail the membership test");
        Assert.False(control.Conserves);

        // the flux is ambiguous by exactly one circulation on a ring
        Assert.All(classRows, r => Assert.Equal(1, r.FluxAmbiguity));
        var circulation = ContinuityLawAudit.CirculationDoesNotChangeTheDivergence(new[] { 4, 8, 16, 96 });
        Assert.All(circulation, c => Assert.Equal(0, c.MaxAbsoluteDivergenceChange));
        Assert.All(circulation, c => Assert.Equal(5, c.Circulation));

        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_02_Conservation_IsNotContinuity_ConnectivityIsTheMissingPremise()
    {
        PrintHeader("G_079 - Q2: is count conservation equivalent to continuity?");
        var sb = new StringBuilder();
        sb.AppendLine("THE CUT TEST, WHICH IS THE AUDIT'S CENTRAL REFUTATION");
        sb.AppendLine("  the image of the divergence requires each component's sum to vanish. A transfer across a cut has one");
        sb.AppendLine("  component gaining and one losing: it conserves the TOTAL and lies outside the image, so it is a");
        sb.AppendLine("  source/sink pair - exactly what the law forbids - and it is count-conserving.");
        sb.AppendLine();
        sb.Append(ContinuityLawAudit.OutputConservationVersusContinuity());

        var cuts = ContinuityLawAudit.CutTest();
        var ring = cuts.Single(c => c.Substrate.StartsWith("one connected ring"));
        Assert.True(ring.ConservesTheTotal && ring.IsATransport);

        var split = cuts.Single(c => c.Substrate.StartsWith("two rings"));
        Assert.True(split.ConservesTheTotal, "the transfer conserves the total");
        Assert.False(split.IsATransport, "and is nevertheless not any flux: conservation is strictly weaker");

        var swept = ContinuityLawAudit.ConservationVersusContinuity();
        Assert.Equal(0, swept.Single(c => c.Substrate.StartsWith("one connected ring")).ConservingAndNotTransport);
        Assert.True(swept.Single(c => c.Substrate.StartsWith("two rings")).ConservingAndNotTransport > 0);
        Assert.True(swept.Single(c => c.Substrate.StartsWith("four rings")).ConservingAndNotTransport > 0);

        // the rank test must be a rank test: the failing witness has zero sum, so a SUM check would have passed it
        var transfer = new[] { 1.0, 0, 0, 0, -1, 0, 0, 0 };
        Assert.Equal(0, transfer.Sum(), 12);
        Assert.False(ContinuityLawAudit.IsATransport(transfer, 8, 2));

        sb.AppendLine($"  and the check must be a RANK check and not a sum: the failing witness has total {transfer.Sum():F0}, "
            + "so a sum test would have accepted it.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_03_NoSurvivingUpdateCreatesAnInteriorSource()
    {
        PrintHeader("G_079 - Q3: does any surviving update create local source terms?");
        var sb = new StringBuilder();
        sb.AppendLine("INTERIOR VERSUS BOUNDARY, WHICH IS THE WHOLE DISTINCTION");
        sb.AppendLine("  an interior source is a change outside the image of the divergence. The class supplies none (Q1).");
        sb.AppendLine("  A BOUNDARY FLUX is the one genuinely source-like term the framework can express, and it is exactly the");
        sb.AppendLine("  leak G_078 measured on an open chain: a region with an edge has somewhere to send count, and sending it");
        sb.AppendLine("  is a source or a sink as far as that region is concerned.");
        sb.AppendLine();

        var operators = SourceManipulationAudit.OperatorClass(
            SourceManipulationAudit.Uniform(8).Select((_, i) => 1.0 + 0.3 * Math.Sin(i)).ToArray());
        double leak = operators.Where(o => !o.Operator.StartsWith("CONTROL")).Max(o => Math.Abs(o.CountChangeOnARing));
        double boundary = operators.First(o => o.Operator.StartsWith("local")).CountChangeOnAnOpenChain;

        sb.AppendLine($"  the largest interior leak over the class on a CLOSED ring: {N(leak)}");
        sb.AppendLine($"  the boundary term an OPEN chain is forced to carry:        {N(boundary)}");
        sb.AppendLine("  so the interior source is zero and the boundary source is available to any substrate with an edge.");

        // on the closed substrate the class leaks nothing; the boundary term is non-zero and equals the dropped flux
        Assert.Equal(0.0, leak, 12);
        Assert.True(Math.Abs(boundary) > 1e-9);
        Assert.True(operators.First(o => o.Operator.StartsWith("local")).OpenLeakEqualsDroppedTerm);

        // and the closed substrate is the canonical one: every entry in the census that is not a multi-component
        // substrate has nullity one, i.e. no boundary can be opened by the operator alone
        var ringRows = ContinuityLawAudit.OperatorCensus().Where(c => c.Components == 1).ToArray();        Assert.All(ringRows, c => Assert.Equal(1.0, c.Nullity, 12));
        Assert.All(ContinuityLawAudit.OperatorCensus(), c => Assert.Equal(c.Cells - c.Components, c.Rank, 12));

        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_04_ASourceCanBeAddedWithoutBreakingAnySurvivingMeasurement()
    {
        PrintHeader("G_079 - Q4: can source terms be added without breaking the previous audits?");
        var sb = new StringBuilder();
        sb.AppendLine("THE LEDGER IS A LIVE DETECTOR, NOT A CONVENTION");
        sb.AppendLine("  G_078 asserted a residual of zero. This audit inserts a source and watches the residual become the");
        sb.AppendLine("  source EXACTLY, and then declares the source and watches the identity return. An identity that can be");
        sb.AppendLine("  broken by a source and repaired by naming it is falsifiable rather than decorative.");
        sb.AppendLine();
        sb.Append(ContinuityLawAudit.OutputSource());

        var ledger = ContinuityLawAudit.SourceDetectability();
        var zero = ledger.Single(l => Math.Abs(l.Source) < 1e-12);
        Assert.Equal(0.0, zero.ResidualBeforeDeclaration, 12);
        var sourced = ledger.Where(l => Math.Abs(l.Source) > 1e-12).ToArray();
        Assert.Equal(2, sourced.Length);
        Assert.All(sourced, l =>
        {
            Assert.Equal(Math.Abs(l.Source), l.ResidualBeforeDeclaration, 12);
            Assert.True(l.ResidualAfterDeclaration < 1e-14);
            Assert.True(l.TheLedgerIsLive);
        });

        var breaks = ContinuityLawAudit.DoesASourceBreakAnything();
        // the only question that answers YES is detectability; every breakage question answers NO
        Assert.All(breaks.Where(b => b.Question.StartsWith("does a source contradict")),
            b => Assert.False(b.Answer, $"{b.Question}: a source must not break any surviving measurement"));
        Assert.True(breaks.Single(b => b.Question.StartsWith("is a source DETECTABLE")).Answer);

        sb.AppendLine("  SO: no surviving measurement excludes a source. What a source breaks is the CLOSURE, which is a");
        sb.AppendLine("  structural premise rather than a measured fact - and the transport-only conclusion is therefore forced");
        sb.AppendLine("  GIVEN the class, not by the data.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_05_ObservablesNeedOneModeTheFluxCannotSupply()
    {
        PrintHeader("G_079 - Q5: are the clock and gravity observables uniquely determined once J is specified?");
        var sb = new StringBuilder();
        sb.AppendLine("NO - AND EXACTLY ONE MODE IS MISSING, WHICH THE CONSERVED COUNT SUPPLIES ON THE CANONICAL SUBSTRATE");
        sb.AppendLine("  a divergence determines a state only up to its KERNEL. On a connected ring the kernel is the uniform");
        sb.AppendLine("  mode, so ratios and differences are determined and the absolute clock rate is not; the conserved total");
        sb.AppendLine("  fixes exactly that mode. On a substrate of c components the kernel has one dimension per component, so");
        sb.AppendLine("  the total fixes the SUM of the levels and leaves c - 1 RELATIVE levels free - levels that no observable");
        sb.AppendLine("  in the sector reads, because the sector reads occupancy and potentials and never a flux.");
        sb.AppendLine();
        sb.Append(ContinuityLawAudit.OutputDeterminacy());

        var det = ContinuityLawAudit.DeterminacyCensus();
        var ring = det.Single(d => d.Substrate.StartsWith("one connected ring") && d.Cells == 8);
        Assert.Equal(1, ring.KernelDimension);
        Assert.Equal(0, ring.FreeModesAfterTheTotalIsFixed);
        Assert.True(ring.ObservablesUniquelyDetermined);

        var split = det.Single(d => d.Substrate.StartsWith("two rings"));
        Assert.Equal(2, split.KernelDimension);
        Assert.Equal(1, split.FreeModesAfterTheTotalIsFixed);
        Assert.False(split.ObservablesUniquelyDetermined);

        var four = det.Single(d => d.Substrate.StartsWith("four rings"));
        Assert.Equal(4, four.KernelDimension);
        Assert.Equal(3, four.FreeModesAfterTheTotalIsFixed);

        // the ambiguity is a GAUGE, not a measurement waiting to be made: nothing in the sector reads a flux
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());
        Assert.True(TemporalIndependenceAudit.TemporalObservables().Length >= 6);

        // the circulation count is an invariant of the ring and not a fact about one n
        var circulation = ContinuityLawAudit.CirculationCount(new[] { 4, 8, 16, 48, 96 });
        Assert.All(circulation, c => Assert.Equal(1, c.Nullity));
        Assert.All(circulation, c => Assert.Equal(c.Cells - 1.0, c.Rank, 12));

        sb.AppendLine($"  the circulation invariant, swept over the ring size: nullity 1 for every n in "
            + $"{string.Join(", ", circulation.Select(c => F(c.Cells)))}.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_06_SourceFreeImpliesRedistributionOnly_Conditionally()
    {
        PrintHeader("G_079 - Q6: does a source-free theory limit gravity engineering to redistribution?");
        var sb = new StringBuilder();
        sb.AppendLine("YES CONDITIONALLY - AND THE CONDITION IS THE ONE HAVING NO SOURCE, WHICH NO MEASUREMENT SUPPLIES");
        sb.AppendLine();

        var breaks = ContinuityLawAudit.DoesASourceBreakAnything();
        var cuts = ContinuityLawAudit.ConservationVersusContinuity();
        var split = cuts.Single(c => c.Substrate.StartsWith("two rings"));

        // the conditional structure, assembled from the measurements rather than asserted
        bool noSourceMeasured = breaks.Where(b => b.Question.StartsWith("does a source contradict")).All(b => !b.Answer);
        bool transportIsForced = ContinuityLawAudit.UpdateClassAsContinuity()
            .Where(r => !r.Operator.StartsWith("CONTROL")).All(r => r.IsATransport);
        bool connectivityIsLoadBearing = split.ConservingAndNotTransport > 0;

        Assert.True(transportIsForced, "transport must be forced on the canonical substrate");
        Assert.True(noSourceMeasured, "no surviving measurement may exclude a source");
        Assert.True(connectivityIsLoadBearing, "connectivity must be measurable as load-bearing");

        sb.AppendLine("  the chain of the argument, each link measured:");
        sb.AppendLine($"    (a) transport IS forced for the class ................ {transportIsForced}");
        sb.AppendLine($"    (b) no measurement excludes a source ................. {noSourceMeasured}");
        sb.AppendLine($"    (c) connectivity is load-bearing .................... {connectivityIsLoadBearing} "
            + $"({split.ConservingAndNotTransport} conserving changes that are not fluxes)");
        sb.AppendLine();
        sb.AppendLine("  so: IF the update class is the surviving class AND the substrate stays connected and closed AND no");
        sb.AppendLine("  source is added, then every change of the source is a redistribution, and G_078's cap applies - the");
        sb.AppendLine("  attainable uplift is (1/3) ln of the count and the attainable contrast a factor of a few under a floor.");
        sb.AppendLine("  Any ONE of those three premises failing opens a channel: a source changes the total, a boundary");
        sb.AppendLine("  supplies a boundary flux, a cut makes a transfer a source/sink pair. The audit therefore answers YES");
        sb.AppendLine("  to Q6 and refuses to answer it unconditionally.");

        // the ceiling from G_078 is the ceiling that survives, so quote it from the operator rather than by hand
        var cap = SourceManipulationAudit.UpliftCap(SourceManipulationAudit.CanonicalCells, SourceManipulationAudit.CanonicalCells)
            .Single(c => c.FloorAssumed);
        sb.AppendLine($"  and the ceiling is G_078's: uplift {cap.Uplift:F4}, rate ratio {cap.RateRatio:F4} on the canonical lattice.");
        Assert.True(cap.RateRatio > 1.0);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_07_Verdict_IsComputed_AndIsBoundary()
    {
        PrintHeader("G_079 - verdict");
        var sb = new StringBuilder();
        sb.Append(ContinuityLawAudit.OutputVerdict());

        string verdict = ContinuityLawAudit.Verdict();
        Assert.StartsWith("BOUNDARY", verdict);
        Assert.Contains("THE CONTINUITY LAW IS DERIVED FOR THE SURVIVING CLASS", verdict);
        // the verdict must carry its three premises and its three counterexamples, not only the positive claim
        Assert.Contains("CONNECTIVITY", verdict);
        Assert.Contains("UNOBSERVABLE", verdict);
        Assert.Contains("LIVE", verdict);
        Assert.Contains("conservation does NOT imply continuity", verdict);
        Assert.Contains("BOUNDARY", verdict);

        // the verdict's live branch: no conserving operator of the class may fail the membership test
        var rows = ContinuityLawAudit.UpdateClassAsContinuity();
        Assert.All(rows.Where(r => !r.Operator.StartsWith("CONTROL")), r => Assert.True(r.IsATransport));
        Assert.False(rows.Single(r => r.Operator.StartsWith("CONTROL")).IsATransport);

        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_08_WhereItStands_CarriesTheFourHonestLimits()
    {
        PrintHeader("G_079 - boundary analysis: what the audit does and does not prove");
        var sb = new StringBuilder();
        string standing = ContinuityLawAudit.WhereItStands();
        sb.Append(standing);

        // the four limits must travel with the verdict rather than living only in prose elsewhere
        Assert.Contains("A SOURCE IS NOT EXCLUDED", standing);
        Assert.Contains("DISCRETISATION", standing);
        Assert.Contains("CONNECTIVITY IS A PROPERTY OF THE CANONICAL SUBSTRATE", standing);
        Assert.Contains("BOUNDARY TERM", standing);

        // and the audit must not present the law as unconditional
        Assert.Contains("ELEVATED", standing);
        Assert.Contains("PREMISES", standing);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_079_09_Reproducible()
    {
        PrintHeader("G_079 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite comes from two objects and no data:");
        sb.AppendLine("      the discrete divergence div J at cell i = J[i] - J[i-1] on a ring of integer counts;");
        sb.AppendLine("      the membership test `rank([M|v]) == rank(M)` on the divergence matrix M.");
        sb.AppendLine("  The decomposition is verified in INTEGER arithmetic, so there is no tolerance and no floating-point");
        sb.AppendLine("  ambiguity in the central claim; the rank tests are on matrices whose entries are 0 and +/-1, where a");
        sb.AppendLine("  tolerance of 1e-9 is far below any possible rounding.");
        Assert.Equal(ContinuityLawAudit.Verdict(), ContinuityLawAudit.Verdict());
        Assert.Equal(ContinuityLawAudit.WhereItStands(), ContinuityLawAudit.WhereItStands());
        sb.AppendLine("  The audit is deterministic: fixed enumerations, no randomness and no external data.");
        Output.WriteLine(sb.ToString());
    }
}
