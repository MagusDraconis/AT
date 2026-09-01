using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 294 — Minimal Theory Audit. Verify that every compressed layer (Closure, Conservation,
/// Resonance, Question, Measurement) is DERIVABLE using only the minimal hierarchy Difference →
/// Actualization → Spectrum → Physics (QG293). No observables, no new assumptions, D96 only,
/// deterministic. Output: MINIMAL THEORY or MISSING LAYER.
/// </summary>
public class ATQG_Phase294_MinimalTheoryAuditTests : ResearchTestBase
{
    public ATQG_Phase294_MinimalTheoryAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2940_ClosureConservationResonanceDerivable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2940: Closure, Conservation, Resonance are DERIVABLE");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Closure ← Actualization: N=96 is the fixed point of the actualization dynamics (QG282);");
        sb.AppendLine("  - Conservation ← Difference + network: definitional identity + handshake lemma (QG266/268);");
        sb.AppendLine("  - Resonance ← Spectrum: dual reads of the one 95-mode spectrum (QG264).");
        sb.AppendLine();

        sb.AppendLine($"closure derivable from actualization: {MinimalTheoryAudit.ClosureDerivableFromActualization()}");
        sb.AppendLine($"conservation derivable: {MinimalTheoryAudit.ConservationDerivable()}");
        sb.AppendLine($"resonance derivable from spectrum: {MinimalTheoryAudit.ResonanceDerivableFromSpectrum()}");
        sb.AppendLine();
        sb.AppendLine("REWRITTEN (using only the minimal hierarchy):");
        sb.AppendLine("  Closure:        Actualization → (converged dynamics) → N=96");
        sb.AppendLine("  Conservation:   Difference → (definitional identity) + Network → (handshake lemma) → Σλ = 2E = N·d");
        sb.AppendLine("  Resonance:      Spectrum → (density/frequency projections) → resonance structure");

        Output.WriteLine(sb.ToString());

        Assert.True(MinimalTheoryAudit.ClosureDerivableFromActualization(),
            "closure must be derivable from actualization (the fixed point)");
        Assert.True(MinimalTheoryAudit.ConservationDerivable(),
            "conservation must be derivable (handshake lemma + definitional identity)");
        Assert.True(MinimalTheoryAudit.ResonanceDerivableFromSpectrum(),
            "resonance must be derivable from the spectrum (dual reads)");
    }

    [Fact]
    public void ATQG2941_QuestionMeasurementDerivable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2941: Question and Measurement are DERIVABLE");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - Question ← Difference: a question is a gap — the known/unknown difference (QG278);");
        sb.AppendLine("  - Measurement ← Spectrum + Physics: structural reads of the same operator basis (QG262/274).");
        sb.AppendLine();

        sb.AppendLine($"question derivable from difference: {MinimalTheoryAudit.QuestionDerivableFromDifference()}");
        sb.AppendLine($"measurement derivable from spectrum/physics: {MinimalTheoryAudit.MeasurementDerivableFromSpectrumPhysics()}");
        sb.AppendLine();
        sb.AppendLine("REWRITTEN (using only the minimal hierarchy):");
        sb.AppendLine("  Question:    Difference → (known/unknown gap) → question classes");
        sb.AppendLine("  Measurement: Spectrum → (operator projections) → measurement classes");

        Output.WriteLine(sb.ToString());

        Assert.True(MinimalTheoryAudit.QuestionDerivableFromDifference(),
            "question must be derivable from Difference (the known/unknown gap)");
        Assert.True(MinimalTheoryAudit.MeasurementDerivableFromSpectrumPhysics(),
            "measurement must be derivable from the spectrum/physics (structural reads)");
    }

    [Fact]
    public void ATQG2942_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2942: the minimal theory determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - DERIVABLE: the compressed layer is rewritten using only the minimal hierarchy;");
        sb.AppendLine("  - ACTUALLY REQUIRED: the compressed layer cannot be rewritten — a MISSING LAYER;");
        sb.AppendLine("  - MINIMAL THEORY: all five compressed layers are derivable — the 4-layer");
        sb.AppendLine("    hierarchy is complete.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {MinimalTheoryAudit.Summary()}");
        sb.AppendLine($"Completeness score: {MinimalTheoryAudit.CompletenessScore()}/5");
        sb.AppendLine($"derivable={MinimalTheoryAudit.DerivableCount()} actually required={MinimalTheoryAudit.ActuallyRequiredCount()}");
        sb.AppendLine($"minimal hierarchy complete: {MinimalTheoryAudit.MinimalHierarchyComplete()}");
        sb.AppendLine($"CLASSIFICATION = {MinimalTheoryAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("RE-DERIVATION MAP:");
        foreach (var l in MinimalTheoryAudit.Layers())
        {
            sb.AppendLine($"  [{l.Derivable.ToString().ToUpperInvariant().PadRight(16)}] {l.Name} ← {l.From}");
            sb.AppendLine($"       {l.RewrittenAs}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal("MINIMAL THEORY", MinimalTheoryAudit.Classify());
        Assert.True(MinimalTheoryAudit.CompletenessScore() >= 5);
        Assert.True(MinimalTheoryAudit.MinimalHierarchyComplete());
        Assert.Contains("MINIMAL THEORY", MinimalTheoryAudit.Summary());
    }
}
