using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 295 — Spectrum Necessity Audit. Is the spectrum fundamental (primitive input), derived
/// (contingent consequence), or the inevitable fixed point of actualization? No observables, no target
/// values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase295_SpectrumNecessityAuditTests : ResearchTestBase
{
    public ATQG_Phase295_SpectrumNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2950_ActualizationDynamicsConverge()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2950: the actualization dynamics converge to a unique fixed point");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the topology converges to 0% residual link growth (QG116);");
        sb.AppendLine("  - the attractor is UNIQUE — every initial pattern converges to the same geometry;");
        sb.AppendLine("  - N=96 is the attractor, NOT a chosen input (QG159/160).");
        sb.AppendLine();

        sb.AppendLine($"dynamics converge: {SpectrumNecessityAudit.DynamicsConverge()}");
        sb.AppendLine($"attractor unique (content-independent): {SpectrumNecessityAudit.AttractorUnique()}");
        sb.AppendLine($"N=96 is the attractor, not a choice: {SpectrumNecessityAudit.N96IsAttractorNotChoice()}");
        sb.AppendLine($"symmetries are attractor properties: {SpectrumNecessityAudit.SymmetriesAreAttractorProperties()}");
        sb.AppendLine();
        sb.AppendLine("The link creation is self-reinforcing and bounded — the process saturates at a");
        sb.AppendLine("stable topology. Every initial pattern → the SAME N=96 geometry.");

        Output.WriteLine(sb.ToString());

        Assert.True(SpectrumNecessityAudit.DynamicsConverge(),
            "the actualization dynamics must converge");
        Assert.True(SpectrumNecessityAudit.AttractorUnique(),
            "the attractor must be unique (content-independent)");
        Assert.True(SpectrumNecessityAudit.N96IsAttractorNotChoice(),
            "N=96 must be the attractor, not a choice");
        Assert.True(SpectrumNecessityAudit.SymmetriesAreAttractorProperties(),
            "the symmetries must be attractor properties");
    }

    [Fact]
    public void ATQG2951_BoundaryAndSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2951: the boundary is the closure; the spectrum is its eigenspectrum");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - N=96 is the stable fixed point of the actualization flow (QG282 CLOSURE PRINCIPLE);");
        sb.AppendLine("  - the spectrum is the Laplacian eigenspectrum of the converged network.");
        sb.AppendLine();

        sb.AppendLine($"boundary is stable fixed point + closure: {SpectrumNecessityAudit.BoundaryIsStableFixedPointAndClosure()}");
        sb.AppendLine($"spectrum is the network eigenspectrum (95 positive modes): {SpectrumNecessityAudit.SpectrumIsNetworkEigenspectrum()}");
        sb.AppendLine();
        sb.AppendLine("Actualization → (unique fixed point N=96) → (Laplacian eigenspectrum) → the D96 spectrum.");
        sb.AppendLine("The spectrum is a DETERMINISTIC function of the converged geometry — no choice enters.");

        Output.WriteLine(sb.ToString());

        Assert.True(SpectrumNecessityAudit.BoundaryIsStableFixedPointAndClosure(),
            "the boundary must be the stable fixed point and the closure");
        Assert.True(SpectrumNecessityAudit.SpectrumIsNetworkEigenspectrum(),
            "the spectrum must be the Laplacian eigenspectrum of the converged network");
    }

    [Fact]
    public void ATQG2952_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2952: the spectrum origin determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - SPECTRUM PRIMITIVE: the spectrum is an assumed input (a choice);");
        sb.AppendLine("  - SPECTRUM DERIVED: the spectrum follows contingently from the dynamics;");
        sb.AppendLine("  - INEVITABLE SPECTRUM: the spectrum is the FORCED output of the unique attractor.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {SpectrumNecessityAudit.Summary()}");
        sb.AppendLine($"Spectrum score: {SpectrumNecessityAudit.SpectrumScore()}/5");
        sb.AppendLine($"primitive: {SpectrumNecessityAudit.SpectrumIsPrimitive()}");
        sb.AppendLine($"derived (contingent): {SpectrumNecessityAudit.SpectrumIsDerivedContingent()}");
        sb.AppendLine($"inevitable: {SpectrumNecessityAudit.SpectrumIsInevitable()}");
        sb.AppendLine($"CLASSIFICATION = {SpectrumNecessityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - PRIMITIVE? NO — the spectrum carries no choice; it is an OUTPUT of the");
        sb.AppendLine("    converged network, not an input.");
        sb.AppendLine("  - DERIVED? NO — the attractor is content-independent (QG116): the same spectrum");
        sb.AppendLine("    from every initial pattern — not contingent.");
        sb.AppendLine("  - INEVITABLE — YES: actualization converges to the UNIQUE N=96 fixed point");
        sb.AppendLine("    (0% residual link growth; N=96 is the attractor, not a choice, QG159/160;");
        sb.AppendLine("    the boundary IS the closure, QG282), and the spectrum is the Laplacian");
        sb.AppendLine("    eigenspectrum of that converged network — forced, unique, stable.");
        sb.AppendLine("  - The spectrum is the inevitable spectral fingerprint of the actualization");
        sb.AppendLine("    attractor: it sits in the minimal hierarchy as the unique spectral output of");
        sb.AppendLine("    Difference → Actualization, not as an independent input.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INEVITABLE SPECTRUM", SpectrumNecessityAudit.Classify());
        Assert.True(SpectrumNecessityAudit.SpectrumScore() >= 5);
        Assert.True(SpectrumNecessityAudit.SpectrumIsInevitable());
        Assert.True(!SpectrumNecessityAudit.SpectrumIsPrimitive() && !SpectrumNecessityAudit.SpectrumIsDerivedContingent());
        Assert.Contains("INEVITABLE SPECTRUM", SpectrumNecessityAudit.Summary());
    }
}
