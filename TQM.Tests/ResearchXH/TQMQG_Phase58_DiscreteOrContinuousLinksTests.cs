using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 58 — discrete or continuous links? Determines whether links are discrete objects or continuous
/// fields. Classify: DISCRETE / CONTINUOUS / BOTH.
///
/// Tests: TQMQG580 (microscopic discreteness), TQMQG581 (continuum limit), TQMQG582 (classification).
/// </summary>
public class TQMQG_Phase58_DiscreteOrContinuousLinksTests : ResearchTestBase
{
    public TQMQG_Phase58_DiscreteOrContinuousLinksTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG580: microscopic discreteness ───────────────────────────────────────────

    [Fact]
    public void TQMQG580_MicroscopicDiscreteness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG580: links are quantized and countable at the microscopic level");

        bool adjQuantized = DiscreteOrContinuousLinks.AdjacencyQuantized();
        bool linkCount = DiscreteOrContinuousLinks.LinkCountDiscrete();
        bool weylDiscrete = DiscreteOrContinuousLinks.WeylDiscreteMicroscopically();
        bool hopDiscrete = DiscreteOrContinuousLinks.PropagationOnFiniteGraphDiscrete();

        sb.AppendLine($"adjacency matrix A_ij is 0/1 (quantized): {adjQuantized}");
        sb.AppendLine($"number of links |E| is countable:          {linkCount}");
        sb.AppendLine($"traceless (Weyl) content is discrete:      {weylDiscrete}");
        sb.AppendLine($"propagation on a finite graph is hopping:  {hopDiscrete}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: microscopically, links are discrete network objects — quantized adjacency, countable links,");
        sb.AppendLine("discrete Weyl content — exactly parallel to the discrete Q-events.");
        Output.WriteLine(sb.ToString());

        Assert.True(adjQuantized, "adjacency should be quantized");
        Assert.True(linkCount, "link count should be discrete");
        Assert.True(weylDiscrete, "Weyl content should be discrete microscopically");
        Assert.True(hopDiscrete, "finite-graph propagation should be discrete");
    }

    // ── TQMQG581: continuum limit ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG581_ContinuumLimit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG581: the continuum limit yields a smooth ψ field");

        bool continuous = DiscreteOrContinuousLinks.ContinuumLimitContinuous();

        sb.AppendLine($"continuum limit gives a smooth (continuous) ψ field: {continuous}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: as N → ∞ with fixed density, the coarse-grained adjacency becomes a smooth field, and its");
        sb.AppendLine("traceless content becomes the continuous Weyl tensor ψ — just as the discrete Q-events yield the continuous");
        sb.AppendLine("counting measure ρ.");
        Output.WriteLine(sb.ToString());

        Assert.True(continuous, "the continuum limit should be continuous");
    }

    // ── TQMQG582: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG582_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG582: DISCRETE / CONTINUOUS / BOTH?");

        sb.AppendLine($"CLASSIFICATION: {DiscreteOrContinuousLinks.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • DISCRETE microscopically: the adjacency is 0/1, links are countable, and the Weyl content is quantized —");
        sb.AppendLine("    the links are discrete network objects at the fundamental level.");
        sb.AppendLine("  • CONTINUOUS in the continuum limit: coarse-graining yields the smooth Weyl field ψ.");
        sb.AppendLine("  • BOTH: links are discrete microscopically and continuous in the continuum limit, in exact parallel to the");
        sb.AppendLine("    nodes (discrete Q-events → continuous ρ). This reconciles QG52 (ψ fundamental) with the network picture:");
        sb.AppendLine("    ψ's microscopic form is discrete, its continuum form is the smooth spin-2 field.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("BOTH", DiscreteOrContinuousLinks.Classify());
        Assert.True(DiscreteOrContinuousLinks.AdjacencyQuantized());
        Assert.True(DiscreteOrContinuousLinks.ContinuumLimitContinuous());
    }
}
