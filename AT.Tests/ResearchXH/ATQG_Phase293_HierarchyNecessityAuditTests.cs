using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 293 — Hierarchy Necessity Audit. Remove each intermediate layer of the hierarchy
/// Difference → Actualization → Closure → Conservation → Resonance → Spectrum → Question → Measurement
/// → Physics and determine which is INDISPENSABLE / COMPRESSIBLE / REDUNDANT. Goal: the minimal hierarchy.
/// No observables, no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase293_HierarchyNecessityAuditTests : ResearchTestBase
{
    public ATQG_Phase293_HierarchyNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2930_ActualizationAndSpectrumIndispensable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2930: Actualization and Spectrum are INDISPENSABLE");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Actualization is the count-producing process (a Q-event IS a unit, QG268);");
        sb.AppendLine("  - Spectrum is the D96 spectral constants every physics read-out consumes.");
        sb.AppendLine();

        sb.AppendLine($"actualization produces the count: {HierarchyNecessityAudit.ActualizationProducesCount()}");
        sb.AppendLine($"spectrum indispensable (all structural physics is spectral reads): {HierarchyNecessityAudit.SpectrumIndispensable()}");
        sb.AppendLine();
        sb.AppendLine("REMOVAL TESTS:");
        foreach (var l in HierarchyNecessityAudit.Layers())
        {
            if (l.Necessity == HierarchyNecessityAudit.LayerNecessity.Indispensable)
                sb.AppendLine($"  [{l.Necessity}] {l.Name} — removing it breaks the chain");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(HierarchyNecessityAudit.ActualizationProducesCount(),
            "actualization must produce the count");
        Assert.True(HierarchyNecessityAudit.SpectrumIndispensable(),
            "the spectrum must be indispensable");
        Assert.True(HierarchyNecessityAudit.IndispensableCount() == 2,
            "exactly Actualization and Spectrum are indispensable");
    }

    [Fact]
    public void ATQG2931_ClosureConservationResonanceCompressible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2931: Closure, Conservation, Resonance are COMPRESSIBLE");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Closure is the fixed point of actualization (QG282 CLOSURE PRINCIPLE);");
        sb.AppendLine("  - Conservation is the universal graph identity (handshake lemma) + the definitional");
        sb.AppendLine("    identity of the primitive (QG266/268);");
        sb.AppendLine("  - Resonance is a dual read of the ONE spectrum (QG264).");
        sb.AppendLine();

        sb.AppendLine($"closure is the actualization fixed point: {HierarchyNecessityAudit.ClosureIsFixedPointOfActualization()}");
        sb.AppendLine($"conservation is a universal graph identity: {HierarchyNecessityAudit.ConservationIsUniversalGraphIdentity()}");
        sb.AppendLine($"conservation is the definitional identity: {HierarchyNecessityAudit.ConservationIsDefinitional()}");
        sb.AppendLine($"resonance is a dual read of the spectrum: {HierarchyNecessityAudit.ResonanceIsDualReadOfSpectrum()}");
        sb.AppendLine();
        sb.AppendLine("COMPRESSIBLE LAYERS:");
        foreach (var l in HierarchyNecessityAudit.Layers())
        {
            if (l.Necessity == HierarchyNecessityAudit.LayerNecessity.Compressible)
                sb.AppendLine($"  [{l.Necessity}] {l.Name} → compressed into {l.CompressedInto}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(HierarchyNecessityAudit.ClosureIsFixedPointOfActualization(),
            "closure must be the actualization fixed point");
        Assert.True(HierarchyNecessityAudit.ConservationIsUniversalGraphIdentity(),
            "conservation must be a universal graph identity");
        Assert.True(HierarchyNecessityAudit.ConservationIsDefinitional(),
            "conservation must be the definitional identity of the primitive");
        Assert.True(HierarchyNecessityAudit.ResonanceIsDualReadOfSpectrum(),
            "resonance must be a dual read of the spectrum");
        Assert.True(HierarchyNecessityAudit.CompressibleCount() == 5,
            "Closure, Conservation, Resonance, Question, Measurement are compressible");
    }

    [Fact]
    public void ATQG2932_MinimalHierarchy()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2932: the minimal hierarchy");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Question is derivable from Difference (QG278);");
        sb.AppendLine("  - Measurement is a structural read of the same operator basis (QG262/274);");
        sb.AppendLine("  - the hierarchy reduces to Difference → Actualization → Spectrum → Physics.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {HierarchyNecessityAudit.Summary()}");
        sb.AppendLine($"Necessity score: {HierarchyNecessityAudit.NecessityScore()}/5");
        sb.AppendLine($"indispensable={HierarchyNecessityAudit.IndispensableCount()} compressible={HierarchyNecessityAudit.CompressibleCount()} redundant={HierarchyNecessityAudit.RedundantCount()}");
        sb.AppendLine($"hierarchy reduces: {HierarchyNecessityAudit.HierarchyReduces()}");
        sb.AppendLine($"CLASSIFICATION = {HierarchyNecessityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("MINIMAL HIERARCHY:");
        foreach (var m in HierarchyNecessityAudit.MinimalHierarchy())
            sb.AppendLine($"  - {m}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the 9-layer hierarchy reduces to 4 layers: Difference → Actualization →");
        sb.AppendLine("    Spectrum → Physics;");
        sb.AppendLine("  - INDISPENSABLE: Actualization (the count-producing process) and Spectrum (the");
        sb.AppendLine("    D96 spectral constants every read-out consumes);");
        sb.AppendLine("  - COMPRESSIBLE: Closure (fixed point of actualization, QG282), Conservation");
        sb.AppendLine("    (universal graph identity + definitional identity, QG266/268), Resonance (dual");
        sb.AppendLine("    read of the one spectrum, QG264), Question (derivable from Difference, QG278),");
        sb.AppendLine("    and Measurement (structural reads of the same operator basis, QG262/274);");
        sb.AppendLine("  - REDUNDANT: none.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("REDUCIBLE", HierarchyNecessityAudit.Classify());
        Assert.True(HierarchyNecessityAudit.NecessityScore() >= 5);
        Assert.True(HierarchyNecessityAudit.HierarchyReduces());
        Assert.Contains("REDUCIBLE", HierarchyNecessityAudit.Summary());
        Assert.Equal(4, HierarchyNecessityAudit.MinimalHierarchy().Length);
    }
}
