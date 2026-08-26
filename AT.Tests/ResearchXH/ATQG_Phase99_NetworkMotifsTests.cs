using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 99 — Network motifs as parameter origin. Determines whether SM parameters can correspond to
/// invariant local network motifs. Classify: NO RELATION / PARTIAL RELATION / MOTIF ORIGIN.
///
/// Tests: ATQG990 (triangle + loop motifs), ATQG991 (branching + spectra + stability + derived), ATQG992 (classification).
/// </summary>
public class ATQG_Phase99_NetworkMotifsTests : ResearchTestBase
{
    public ATQG_Phase99_NetworkMotifsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG990: triangle motifs, loop motifs ─────────────────────────────────────

    [Fact]
    public void ATQG990_TriangleAndLoopMotifs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG990: do triangle/loop motifs exist?");

        bool triangle = NetworkMotifs.TriangleMotifsExist();
        bool loop = NetworkMotifs.LoopMotifsExist();

        sb.AppendLine($"triangle motifs exist: {triangle}");
        sb.AppendLine($"loop motifs exist: {loop}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: triangle and loop motifs are recurring subgraph patterns with their own invariants (area,");
        sb.AppendLine("holonomy) — richer than individual lengths/angles.");
        Output.WriteLine(sb.ToString());

        Assert.True(triangle, "triangle motifs exist");
        Assert.True(loop, "loop motifs exist");
    }

    // ── ATQG991: branching motifs, spectra, stability classes, derived composites ─

    [Fact]
    public void ATQG991_BranchingSpectraStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG991: branching motifs, motif spectra, stability classes");

        bool branching = NetworkMotifs.BranchingMotifsExist();
        bool spectra = NetworkMotifs.MotifSpectraExist();
        bool stability = NetworkMotifs.MotifStabilityClassesExist();
        bool derived = NetworkMotifs.MotifsAreDerivedComposites();
        bool determines = NetworkMotifs.MotifsDetermineValues();

        sb.AppendLine($"branching motifs exist: {branching}");
        sb.AppendLine($"network has a MOTIF SPECTRUM: {spectra}");
        sb.AppendLine($"motifs have stability classes: {stability}");
        sb.AppendLine($"motifs are DERIVED composites (no independent dof): {derived}");
        sb.AppendLine($"motifs DETERMINE specific SM parameter values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: motifs provide a structural organizing principle (spectrum + stability classes), but they are");
        sb.AppendLine("derived composites whose invariants reduce to link content; no native mapping selects specific values.");
        Output.WriteLine(sb.ToString());

        Assert.True(branching, "branching motifs exist");
        Assert.True(spectra, "motif spectrum exists");
        Assert.True(stability, "stability classes exist");
        Assert.True(derived, "motifs are derived");
        Assert.False(determines, "motifs do not determine values");
    }

    // ── ATQG992: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG992_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG992: NO RELATION / PARTIAL RELATION / MOTIF ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {NetworkMotifs.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: motifs and motif spectra are real network structure and provide an organizing principle.");
        sb.AppendLine("  • NOT MOTIF ORIGIN: motifs are derived composites with no independent dof, and no native mapping selects");
        sb.AppendLine("    which motif/invariant corresponds to which SM parameter.");
        sb.AppendLine("  • PARTIAL RELATION: structural organizing principle (motif spectra) without value determination.");
        sb.AppendLine();
        sb.AppendLine("So network motifs give a PARTIAL RELATION to parameters (organizing structure, not motif origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", NetworkMotifs.Classify());
        Assert.True(NetworkMotifs.MotifSpectraExist());
        Assert.False(NetworkMotifs.MotifsDetermineValues());
    }
}
