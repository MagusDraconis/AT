using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_007 - Generator Selection Audit (group QM). Why does AT use {1..6} instead of the
/// Schrodinger-compatible {1}? Measure locality, the dispersion error, the fold position, packet evolution and the
/// existing AT requirements, and identify which constraint forces the native generator.
/// </summary>
public sealed class Y_QM_007_Tests : ResearchTestBase
{
    public Y_QM_007_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private const int Singleton = 1;
    private const int Pair = 0b11;
    private const int Native = 0b111111;

    [Fact]
    public void Y_QM_007_TheTraceAloneFixesTheShellCount()
    {
        PrintHeader("QM_007 - the trace: one number already in the repository fixes the shell count");

        var sb = new StringBuilder();
        sb.AppendLine($"  a circulant Laplacian's trace is 2 |S| 96, so the trace counts the shells");
        sb.AppendLine($"  recorded trace: {GeneratorSelectionAudit.RecordedTrace():F0}");
        sb.AppendLine("  candidate   shells   trace     matches the record");
        foreach (var name in GeneratorSelectionAudit.Candidates())
        {
            int mask = GeneratorSelectionAudit.MaskOf(name);
            double trace = GeneratorSelectionAudit.TraceOf(mask);
            sb.AppendLine($"  {name,-11} {LaplacianDispersionAudit.ShellSet(mask).Length,-8} {trace,-9:F0} {Math.Abs(trace - GeneratorSelectionAudit.RecordedTrace()) < 1e-9}");
        }
        sb.AppendLine();
        sb.AppendLine($"  THE RECORDED TRACE 1152 = 2 x 6 x 96, SO THE NUMBER OF SHELLS IS SIX - and {{1}} gives 192 and {{1,2}} gives 384,");
        sb.AppendLine("  contradicting the record by factors of six and three. Only ONE of the 63 subsets matches it, the six-shell one.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1152.0, GeneratorSelectionAudit.RecordedTrace(), 9);
        Assert.Equal(192.0, GeneratorSelectionAudit.TraceOf(Singleton), 6);
        Assert.Equal(384.0, GeneratorSelectionAudit.TraceOf(Pair), 6);
        Assert.Equal(1152.0, GeneratorSelectionAudit.TraceOf(Native), 6);
        // the trace is exactly 2 |S| 96 for every subset
        foreach (var mask in LaplacianDispersionAudit.Subsets())
            Assert.Equal(192.0 * LaplacianDispersionAudit.ShellSet(mask).Length, GeneratorSelectionAudit.TraceOf(mask), 6);

        var census = GeneratorSelectionAudit.RequirementCensus()[0];
        Assert.Equal(1, census.MatchingTrace);
        Assert.Equal(1, census.SixShellSubsets);
    }

    [Fact]
    public void Y_QM_007_ExactlyOneOfTheSixtyThreeSubsetsReproducesTheRecordedSpectrum()
    {
        PrintHeader("QM_007 - the spectrum: which six shells");

        var sb = new StringBuilder();
        sb.AppendLine("  requirement                        recorded      {1}           {1,2}         {1..6}");
        foreach (var r in GeneratorSelectionAudit.RequirementTable())
            sb.AppendLine($"  {r.Requirement,-34} {r.Recorded,-13} {r.Singleton,-13} {r.Pair,-13} {r.Native}");
        sb.AppendLine();
        var census = GeneratorSelectionAudit.RequirementCensus()[0];
        sb.AppendLine($"  of the 63 subsets: {census.MatchingTrace} match the trace, {census.MatchingLevels} match the level count, and "
            + $"{census.MatchingFull} REPRODUCES THE WHOLE SPECTRUM.");
        sb.AppendLine();
        sb.AppendLine("  THE CONSTRAINT IS COMPUTED AND IT IS UNIQUE: the recorded 45 levels with a maximum of 15.837372 select the native");
        sb.AppendLine("  set and nothing else. No propagation requirement is involved anywhere in this selection.");
        Output.WriteLine(sb.ToString());

        // the level structure
        Assert.Equal(45, GeneratorSelectionAudit.RecordedLevels());
        Assert.Equal(49, GeneratorSelectionAudit.LevelCount(Singleton));
        Assert.Equal(47, GeneratorSelectionAudit.LevelCount(Pair));
        Assert.Equal(45, GeneratorSelectionAudit.LevelCount(Native));

        // the free room, which the earlier audits derived from the level count
        Assert.Equal(51, GeneratorSelectionAudit.RecordedFreeRoom());
        Assert.Equal(47, GeneratorSelectionAudit.FreeRoomOf(Singleton));
        Assert.Equal(49, GeneratorSelectionAudit.FreeRoomOf(Pair));
        Assert.Equal(51, GeneratorSelectionAudit.FreeRoomOf(Native));

        // the maximum, which discriminates {1,2} from {1..6} even though both are quadratic
        Assert.InRange(GeneratorSelectionAudit.RecordedMax(), 15.83, 15.84);
        Assert.InRange(GeneratorSelectionAudit.MaxOf(Singleton), 3.99, 4.01);
        Assert.InRange(GeneratorSelectionAudit.MaxOf(Pair), 6.24, 6.26);
        Assert.InRange(GeneratorSelectionAudit.MaxOf(Native), 15.83, 15.84);

        // the uniqueness of the full spectrum match
        Assert.False(GeneratorSelectionAudit.ReproducesTheRecordedSpectrum(Singleton));
        Assert.False(GeneratorSelectionAudit.ReproducesTheRecordedSpectrum(Pair));
        Assert.True(GeneratorSelectionAudit.ReproducesTheRecordedSpectrum(Native));
        Assert.Equal(1, GeneratorSelectionAudit.SubsetsReproducingTheRecord());
    }

    [Fact]
    public void Y_QM_007_OneRequiredStructureDoesNotDiscriminateAtAll()
    {
        PrintHeader("QM_007 - the requirement that is not a constraint");

        var table = GeneratorSelectionAudit.RequirementTable();
        var sb = new StringBuilder();
        sb.AppendLine("  requirement                        recorded      {1}           {1,2}         {1..6}        discriminates");
        foreach (var r in table)
            sb.AppendLine($"  {r.Requirement,-34} {r.Recorded,-13} {r.Singleton,-13} {r.Pair,-13} {r.Native,-13} {r.Discriminates}");
        sb.AppendLine();
        sb.AppendLine("  THE 42/53 AMPLITUDE/PHASE SPLIT IS |S|-INDEPENDENT: it follows from the ring's reflection pairing and the");
        sb.AppendLine("  canonical state's construction, so it is the SAME for every shell set and CANNOT select a generator. A");
        sb.AppendLine("  requirement that looks like a constraint here is not one, and the audit reports it rather than omitting it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(6, table.Length);
        Assert.Equal(5, table.Count(r => r.Discriminates == "yes"));
        var split = table.Single(r => r.Requirement.Contains("42/53"));
        Assert.Contains("NO", split.Discriminates);
        // and its value really is identical for all three candidates
        Assert.Equal(split.Singleton, split.Pair);
        Assert.Equal(split.Pair, split.Native);
        Assert.Equal(split.Recorded, split.Native);
    }

    [Fact]
    public void Y_QM_007_EveryPropagationMeasurePrefersTheSingleton()
    {
        PrintHeader("QM_007 - the five measures, and which generator each prefers");

        var table = GeneratorSelectionAudit.MeasureTable();
        var sb = new StringBuilder();
        sb.AppendLine("  measure                            {1}           {1,2}         {1..6}        prefers");
        foreach (var m in table)
            sb.AppendLine($"  {m.Measure,-34} {m.Singleton,-13} {m.Pair,-13} {m.Native,-13} {m.Prefers}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, table.Length);

        // locality: the native set wins, because the substrate is 12-regular
        Assert.Equal(2, GeneratorSelectionAudit.Locality(Singleton));
        Assert.Equal(4, GeneratorSelectionAudit.Locality(Pair));
        Assert.Equal(12, GeneratorSelectionAudit.Locality(Native));

        // dispersion error: the singleton wins
        Assert.InRange(GeneratorSelectionAudit.DispersionError(Singleton), 0.594, 0.595);
        Assert.InRange(GeneratorSelectionAudit.DispersionError(Pair), 0.918, 0.919);
        Assert.InRange(GeneratorSelectionAudit.DispersionError(Native), 0.986, 0.987);

        // fold position: the singleton has none
        Assert.Equal(0, GeneratorSelectionAudit.FoldPosition(Singleton));
        Assert.Equal(28, GeneratorSelectionAudit.FoldPosition(Pair));
        Assert.Equal(11, GeneratorSelectionAudit.FoldPosition(Native));

        // packet window: the singleton wins by a factor of about twenty-two
        Assert.InRange(GeneratorSelectionAudit.WindowOf(Singleton), 500.0, 530.0);
        Assert.InRange(GeneratorSelectionAudit.WindowOf(Pair), 148.0, 158.0);
        Assert.InRange(GeneratorSelectionAudit.WindowOf(Native), 20.0, 26.0);

        // exactly two rows prefer the native set, and one of them is the requirements row itself
        Assert.Equal(2, table.Count(m => m.Prefers == "the NATIVE set"));
        Assert.Equal(3, table.Count(m => m.Prefers == "the SINGLETON"));
    }

    [Fact]
    public void Y_QM_007_ThePacketEvolutionOrdersTheThreeGenerators()
    {
        PrintHeader("QM_007 - the packet, each candidate normalised by its own coefficient");

        var widths = GeneratorSelectionAudit.PacketWidths();
        var sb = new StringBuilder();
        sb.AppendLine("  time    {1}        {1,2}      {1..6}     reference");
        foreach (var w in widths)
            sb.AppendLine($"  {w.Time,-7:F0} {w.OneShell,-10:F4} {w.Pair,-10:F4} {w.Native,-10:F4} {w.Reference:F4}");
        sb.AppendLine();
        sb.AppendLine("  the ordering is monotone in the shell count at every time: the more shells, the further the dispersion is");
        sb.AppendLine("  from the Schrodinger law and the slower the packet tracks the continuum spreading.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, widths.Length);
        // at every time the singleton is closest to the reference and the native set furthest
        foreach (var w in widths.Where(w => w.Time > 0.0))
        {
            Assert.True(Math.Abs(w.OneShell - w.Reference) < Math.Abs(w.Pair - w.Reference));
            Assert.True(Math.Abs(w.Pair - w.Reference) < Math.Abs(w.Native - w.Reference));
        }

        // the distances at t = 20 put the same ordering on the state rather than the width
        Assert.InRange(GeneratorSelectionAudit.DistanceUnder(Singleton, 20.0), 0.004, 0.005);
        Assert.InRange(GeneratorSelectionAudit.DistanceUnder(Pair, 20.0), 0.013, 0.015);
        Assert.InRange(GeneratorSelectionAudit.DistanceUnder(Native, 20.0), 0.087, 0.089);
    }

    [Fact]
    public void Y_QM_007_TheNativeSetIsTheOnlyOneThatSatisfiesTheExistingRequirements()
    {
        PrintHeader("QM_007 - the existing AT requirements, and their cost in propagation");

        var table = GeneratorSelectionAudit.RequirementTable();
        var sb = new StringBuilder();
        sb.AppendLine("  THE TRADE THE QUESTION ASKS ABOUT, IN ONE PLACE:");
        sb.AppendLine($"    the native set satisfies ALL {table.Count(r => r.Discriminates == "yes")} discriminating requirements and propagates a packet");
        sb.AppendLine($"    for {GeneratorSelectionAudit.WindowOf(Native):F0} units inside 10 % of the exact Schrodinger solution;");
        sb.AppendLine($"    the singleton has the BEST propagation ({GeneratorSelectionAudit.WindowOf(Singleton):F0} units) and fails EVERY ONE of them:");
        sb.AppendLine($"    trace {GeneratorSelectionAudit.TraceOf(Singleton):F0} against {GeneratorSelectionAudit.RecordedTrace():F0}, free room {GeneratorSelectionAudit.FreeRoomOf(Singleton)} against {GeneratorSelectionAudit.RecordedFreeRoom()},");
        sb.AppendLine($"    and it does not reproduce the recorded spectrum at all.");
        Output.WriteLine(sb.ToString());

        var discriminating = table.Where(r => r.Discriminates == "yes").ToArray();
        Assert.Equal(5, discriminating.Length);
        // the native set matches the record in every discriminating requirement, and neither smaller set matches any
        foreach (var r in discriminating)
        {
            Assert.True(GeneratorSelectionAudit.MatchesTheRecord(r.Requirement, Native), r.Requirement);
            Assert.False(GeneratorSelectionAudit.MatchesTheRecord(r.Requirement, Singleton), r.Requirement);
            Assert.False(GeneratorSelectionAudit.MatchesTheRecord(r.Requirement, Pair), r.Requirement);
            // and the mark in the table agrees with the computed answer
            Assert.Contains("*", r.Native);
            Assert.DoesNotContain("*", r.Singleton);
        }
    }

    [Fact]
    public void Y_QM_007_TheVerdictNamesTheConstraint()
    {
        PrintHeader("QM_007 - the verdict");

        var identification = GeneratorSelectionAudit.TheIdentification();
        var counts = GeneratorSelectionAudit.VerdictCounts();
        var sb = new StringBuilder();
        foreach (var h in identification)
        {
            sb.AppendLine($"{h.Question}: {h.Answer}");
            sb.AppendLine($"  {h.Basis}");
        }
        sb.AppendLine();
        sb.AppendLine(GeneratorSelectionAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine(GeneratorSelectionAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, identification.Length);
        Assert.Equal("DERIVED", identification[0].Answer);       // the constraint is identified
        Assert.Equal("REFUTED", identification[1].Answer);       // it is not dynamical
        Assert.Equal("BOUNDARY", identification[2].Answer);      // it is an input, not a law
        Assert.Equal(1, counts.Derived);
        Assert.Equal(1, counts.Boundary);
        Assert.Equal(1, counts.Refuted);

        var verdict = GeneratorSelectionAudit.Verdict();
        Assert.Contains("BOUNDARY", verdict);
        Assert.Contains("SPECTRAL FINGERPRINT", verdict);
        Assert.Contains("INPUT RATHER THAN A LAW", verdict);
    }

    [Fact]
    public void Y_QM_007_TheReport()
    {
        PrintHeader("QM_007 - generator selection: the report");

        var sb = new StringBuilder();
        sb.AppendLine(GeneratorSelectionAudit.OutputRequirements());
        sb.AppendLine(GeneratorSelectionAudit.OutputMeasures());
        sb.AppendLine(GeneratorSelectionAudit.OutputPackets());
        sb.AppendLine(GeneratorSelectionAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());

        // The structural statement behind the whole audit: the recorded spectrum IS the six-shell symbol, so the
        // "selection" is not a selection at all but a restatement of the substrate.
        foreach (var k in Enumerable.Range(0, 96))
        {
            double delta = 2.0 * Math.PI * k / 96;
            double symbol = 0.0;
            for (int r = 1; r <= 6; r++) symbol += 2.0 - 2.0 * Math.Cos(r * delta);
            Assert.Equal(RhoObservableAudit.ModeEigenvalues()[k], symbol, 6);
        }
    }
}
