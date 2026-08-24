using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 281 — Boundary Condition Audit. Is observable structure determined by boundary conditions
/// or energy content? Does resonance emerge from conservation plus boundary conditions?
/// </summary>
public class TQMQG_Phase281_BoundaryConditionAuditTests : ResearchTestBase
{
    public TQMQG_Phase281_BoundaryConditionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2810_SpectrumIsBoundaryDetermined()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2810: the D96 spectrum is boundary-determined");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the 95 modes ω=√λ are eigenvalues of the graph Laplacian L = D − A;");
        sb.AppendLine("  - L is determined by the ADJACENCY (the network boundary), not the activity (energy).");
        sb.AppendLine();

        sb.AppendLine("THE ANALOGY (boundary-determined resonance):");
        foreach (var (name, boundary, energy) in BoundaryConditionAudit.Analogies())
            sb.AppendLine($"  {name}: {boundary}; {energy}");
        sb.AppendLine();
        sb.AppendLine($"spectrum is a Laplacian eigenspectrum: {BoundaryConditionAudit.SpectrumIsLaplacianEigenspectrum()}");
        sb.AppendLine($"Laplacian from adjacency (not activity): {BoundaryConditionAudit.LaplacianFromAdjacencyNotActivity()}");
        sb.AppendLine($"frequencies energy-invariant: {BoundaryConditionAudit.FrequenciesEnergyInvariant()}");

        Output.WriteLine(sb.ToString());

        Assert.True(BoundaryConditionAudit.SpectrumIsLaplacianEigenspectrum());
        Assert.True(BoundaryConditionAudit.LaplacianFromAdjacencyNotActivity(),
            "L = D − A is built from the adjacency (boundary), not the activity (energy)");
        Assert.True(BoundaryConditionAudit.FrequenciesEnergyInvariant(),
            "the mode frequencies do not change under energy rescaling");
    }

    [Fact]
    public void TQMQG2811_ConservationTimesBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2811: conservation × boundary = the total");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the trace Σλ = 2E = N·d (QG266) is the CONSERVATION part;");
        sb.AppendLine("  - the N=96 degree-12 regularity is the BOUNDARY part.");
        sb.AppendLine();

        sb.AppendLine($"trace is conservation (2E, handshake lemma): {BoundaryConditionAudit.TraceIsConservation()}");
        sb.AppendLine($"N=96 degree-12 is the boundary: {BoundaryConditionAudit.N96IsBoundary()}");
        sb.AppendLine($"total is conservation × boundary (Σλ = 2E = N·d): {BoundaryConditionAudit.TotalIsConservationTimesBoundary()}");
        sb.AppendLine();
        sb.AppendLine("The TOTAL spectral weight is set by conservation × boundary conditions.");
        sb.AppendLine("The INDIVIDUAL modes (resonances) are set by the boundary (network structure).");

        Output.WriteLine(sb.ToString());

        Assert.True(BoundaryConditionAudit.TraceIsConservation());
        Assert.True(BoundaryConditionAudit.N96IsBoundary());
        Assert.True(BoundaryConditionAudit.TotalIsConservationTimesBoundary(),
            "Σλ = 2E = N·d — conservation × boundary fixes the total");
    }

    [Fact]
    public void TQMQG2812_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2812: the boundary-condition determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - ENERGY DOMINANT (score ≤ 2), BOUNDARY DOMINANT (3-4),");
        sb.AppendLine("    RESONANCE = CONSERVATION + BOUNDARY (5-6);");
        sb.AppendLine("  - the question: does resonance emerge from conservation plus boundary conditions?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {BoundaryConditionAudit.Summary()}");
        sb.AppendLine($"Boundary-role score: {BoundaryConditionAudit.BoundaryRoleScore()}/6");
        sb.AppendLine($"families from boundary (span): {BoundaryConditionAudit.FamiliesFromBoundary()}");
        sb.AppendLine($"occupancies boundary-set: {BoundaryConditionAudit.OccupanciesBoundarySet()}");
        sb.AppendLine($"N=96 attractor is the closure: {BoundaryConditionAudit.AttractorIsClosure()}");
        sb.AppendLine($"CLASSIFICATION = {BoundaryConditionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The D96 spectrum is the Laplacian eigenspectrum — set by the ADJACENCY (the");
        sb.AppendLine("    boundary), not the activity (the energy). Like a vibrating string: energy sets");
        sb.AppendLine("    the amplitude, the boundary sets the frequency.");
        sb.AppendLine("  - The TOTAL spectral weight is CONSERVATION × BOUNDARY: Σλ = 2E = N·d (QG266).");
        sb.AppendLine("  - The INDIVIDUAL modes (octave families, occupancies, ladder, peaks) are set by");
        sb.AppendLine("    the N=96 closure — the 'pot with a lid' whose walls fix the resonances.");
        sb.AppendLine("  - CONCLUSION: RESONANCE = CONSERVATION (total) + BOUNDARY (modes). Observable");
        sb.AppendLine("    structure is determined by the boundary conditions, not the energy content.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("RESONANCE = CONSERVATION + BOUNDARY", BoundaryConditionAudit.Classify());
        Assert.True(BoundaryConditionAudit.BoundaryRoleScore() >= 5);
        Assert.Contains("RESONANCE = CONSERVATION + BOUNDARY", BoundaryConditionAudit.Summary());
        Assert.Contains("boundary", BoundaryConditionAudit.Summary());
    }
}
