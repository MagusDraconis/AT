using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 266 — Invariant Origin Audit. Is the invariant Σλ = 12×96 fundamental or the projection
/// of a deeper conservation law? D96 only, no observables.
/// </summary>
public class ATQG_Phase266_InvariantOriginAuditTests : ResearchTestBase
{
    public ATQG_Phase266_InvariantOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2660_TraceIdentity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2660: the trace of the Laplacian — a universal graph identity");

        sb.AppendLine("HYPOTHESIS: Σλ = trace(L) = Σ degrees = 2·(number of edges) — the HANDSHAKE LEMMA,");
        sb.AppendLine("a universal identity of every Laplacian L = D − A.");
        sb.AppendLine();

        sb.AppendLine($"N = {InvariantOriginAudit.NodeCount()} nodes");
        sb.AppendLine($"edges = {InvariantOriginAudit.EdgeCount()}");
        sb.AppendLine($"trace(L) = Σ degrees = {InvariantOriginAudit.TraceFromDegrees():F0}");
        sb.AppendLine($"2·edges = {InvariantOriginAudit.TwiceEdges():F0}");
        sb.AppendLine($"trace = 2E (handshake lemma)? {InvariantOriginAudit.TraceEqualsTwiceEdges()}");
        sb.AppendLine($"Σλ (eigenvalues) = {InvariantOriginAudit.EigenvalueTrace():F8}");
        sb.AppendLine($"Σλ = trace(L)? {InvariantOriginAudit.EigenvalueTraceEqualsMatrixTrace()}");

        Output.WriteLine(sb.ToString());

        Assert.True(InvariantOriginAudit.TraceEqualsTwiceEdges(), "trace(L) = 2·edges (handshake lemma)");
        Assert.True(InvariantOriginAudit.EigenvalueTraceEqualsMatrixTrace(), "Σλ = trace(L)");
    }

    [Fact]
    public void ATQG2661_RegularNetwork()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2661: the network is regular — why the value is 12×96");

        sb.AppendLine("HYPOTHESIS: the observable sector is a REGULAR graph (all degrees equal), so");
        sb.AppendLine("trace(L) = N·d = 96×12 — the factorization is the degree structure, not fitting.");
        sb.AppendLine();

        var degs = InvariantOriginAudit.Degrees();
        sb.AppendLine($"degree distribution: min={degs.Min()}, max={degs.Max()}, distinct={string.Join(",", degs.Distinct().OrderBy(d=>d))}");
        sb.AppendLine($"regular graph: {InvariantOriginAudit.IsRegular()}");
        sb.AppendLine($"common degree d = {InvariantOriginAudit.CommonDegree()} (the gauge sector 1+3+8)");
        sb.AppendLine($"trace = N·d = {InvariantOriginAudit.NodeCount()}×{InvariantOriginAudit.CommonDegree()} = {InvariantOriginAudit.NodeCount() * InvariantOriginAudit.CommonDegree()}");
        sb.AppendLine($"trace = N·d? {InvariantOriginAudit.TraceEqualsNodesTimesDegree()}");
        sb.AppendLine($"Σλ = 1152 = 12×96 (QG265) — now explained as N·d of a regular graph.");

        Output.WriteLine(sb.ToString());

        Assert.True(InvariantOriginAudit.IsRegular(), "the observable sector must be a regular graph");
        Assert.Equal(12, InvariantOriginAudit.CommonDegree());
        Assert.True(InvariantOriginAudit.TraceEqualsNodesTimesDegree(), "trace = N·d = 96×12");
    }

    [Fact]
    public void ATQG2662_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2662: the origin determination — why Σλ is conserved");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - FUNDAMENTAL INVARIANT (score ≤ 2), DERIVED INVARIANT (3-4),");
        sb.AppendLine("    UNIVERSAL CONSERVATION LAW (5-6);");
        sb.AppendLine("  - the conservation structure: trace identity (2E), kernel conservation,");
        sb.AppendLine("    and the conserved N=96 actualization attractor.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {InvariantOriginAudit.Summary()}");
        sb.AppendLine($"Origin score: {InvariantOriginAudit.OriginScore()}/6");
        sb.AppendLine($"Constant vector in kernel (total-mass conservation): {InvariantOriginAudit.ConstantVectorInKernel()}");
        sb.AppendLine($"CLASSIFICATION = {InvariantOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - Σλ = trace(L) = Σ degrees = 2·edges is the HANDSHAKE LEMMA — a universal");
        sb.AppendLine("    identity of every Laplacian, not a fitted constant.");
        sb.AppendLine("  - The value 1152 = 96×12 = N·d follows from the network being REGULAR of degree");
        sb.AppendLine("    12 (the gauge sector 1+3+8) — the factorization is the degree structure.");
        sb.AppendLine("  - Every Laplacian has the constant vector in its kernel (row sums = 0), so the");
        sb.AppendLine("    total actualization amplitude is conserved by the dynamics ẋ = −Lx — the");
        sb.AppendLine("    ACTUALIZATION CONSERVATION; the trace identity is its scalar projection.");
        sb.AppendLine("  - The N=96 network is the CONVERGED ATTRACTOR of the actualization dynamics");
        sb.AppendLine("    (QG159/160): the dynamics conserves its attractor → N, E, and the degree");
        sb.AppendLine("    sequence are fixed → trace = 2E is fixed.");
        sb.AppendLine("  - CONCLUSION: Σλ is NOT fundamental. It is the projection of a UNIVERSAL");
        sb.AppendLine("    conservation law (Laplacian trace identity + kernel conservation) onto the");
        sb.AppendLine("    conserved N=96 actualization attractor.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL CONSERVATION LAW", InvariantOriginAudit.Classify());
        Assert.True(InvariantOriginAudit.OriginScore() >= 5);
        Assert.True(InvariantOriginAudit.ConstantVectorInKernel());
        Assert.Contains("UNIVERSAL CONSERVATION LAW", InvariantOriginAudit.Summary());
    }
}
