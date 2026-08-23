using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 260 — Resonance Layer Audit. Determine whether a hidden resonance layer exists between
/// D96 and the observables, or whether it was collapsed into the D96 moment set.
/// </summary>
public class TQMQG_Phase260_ResonanceLayerAuditTests : ResearchTestBase
{
    public TQMQG_Phase260_ResonanceLayerAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2600_SpectralStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2600: the D96 spectral structure (the raw material)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - D96 spectrum: 95 stable modes ω = √λ (Laplacian of the observable sector);");
        sb.AppendLine("  - octave families = frequency-doubling bands (harmonic resonance structure);");
        sb.AppendLine("  - the quantities Σm, span, λ₂, occMom, Σ√m are MOMENTS of this spectrum (QG155/157).");
        sb.AppendLine();

        var modes = FamilyIndexOrigin.IntraSectorModes();
        sb.AppendLine($"Mode count: {modes.Length}");
        sb.AppendLine($"Spectral span: {ResonanceLayerAudit.Span():F6}");
        sb.AppendLine($"log2(span): {Math.Log(ResonanceLayerAudit.Span()) / Math.Log(2.0):F6}");
        sb.AppendLine($"Family count (floor(log2 span)+1): {ResonanceLayerAudit.FamilyCount()}");
        sb.AppendLine($"Octave occupancies: [{string.Join(",", ResonanceLayerAudit.OctaveOccupancies())}]");
        sb.AppendLine($"Top-band crowding: {ResonanceLayerAudit.TopBandCrowding():P1}");
        sb.AppendLine($"Mode-locking fraction (near-degenerate ratios): {ResonanceLayerAudit.ModeLockingFraction():P1}");
        sb.AppendLine($"Ladder comb max deviation: {ResonanceLayerAudit.LadderCombMaxDeviation():P1}");
        sb.AppendLine();
        sb.AppendLine("THE LADDER IS A FIXED-SPACING BEAT COMB (MZ/6 = 15.198 GeV) — a resonance structure.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, ResonanceLayerAudit.FamilyCount());
        Assert.True(ResonanceLayerAudit.TopBandDominant(), "the top octave band dominates (crowding)");
        Assert.True(ResonanceLayerAudit.ModeLockingFraction() > 0.5, "modes are locked into near-degenerate clusters");
        Assert.True(ResonanceLayerAudit.LadderIsBeatComb(), "the sector ladder is a linear beat comb");
    }

    [Fact]
    public void TQMQG2601_BeatIdentities()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2601: the collapsed beat identities among the D96 moments");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - if the D96 moments are the COLLAPSED PRODUCT of a resonance layer, they should");
        sb.AppendLine("    satisfy near-integer (beat/locking) ratios — the signature of a beat structure.");
        sb.AppendLine();

        foreach (var b in ResonanceLayerAudit.BeatIdentities())
            sb.AppendLine($"  {b}");
        sb.AppendLine();
        sb.AppendLine($"Near-integer beat identities: {ResonanceLayerAudit.NearIntegerBeatCount()}");
        sb.AppendLine($"Within 2% of integer/rational target: {ResonanceLayerAudit.BeatIdentitiesWithin2Percent()}");

        Output.WriteLine(sb.ToString());

        Assert.True(ResonanceLayerAudit.NearIntegerBeatCount() >= 2, "at least two near-integer beat identities");
        Assert.True(ResonanceLayerAudit.BeatIdentitiesWithin2Percent() >= 2, "at least two within 2%");
        // The flagship identity: Σ√m/span ≈ 10 (0.09%).
        Assert.True(Math.Abs(ResonanceLayerAudit.SigmaSqrtM() / ResonanceLayerAudit.Span() - 10.0) < 0.5);
    }

    [Fact]
    public void TQMQG2602_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2602: the layer determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - NO LAYER (score ≤ 2), PARTIAL LAYER (3-4), RESONANCE LAYER (5-6);");
        sb.AppendLine("  - the QG165-258 formulas use the collapsed moments (Σm, span, λ₂, occMom, Σ√m, Σm²)");
        sb.AppendLine("    DIRECTLY — no explicit beat/locking/crowding operator sits between D96 and the");
        sb.AppendLine("    observables in the published formulas.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ResonanceLayerAudit.Summary()}");
        sb.AppendLine($"Layer score: {ResonanceLayerAudit.LayerScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {ResonanceLayerAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - A resonance layer EXISTS inside D96: the octave families (frequency-doubling,");
        sb.AppendLine("    family count 3), the mode crowding (92% in the top band; 94% of successive");
        sb.AppendLine("    ratios near-degenerate), the sector ladder (a fixed-spacing MZ/6 beat comb), and");
        sb.AppendLine("    the integer beat identities (Σ√m/span ≈ 10, Σm²/Σm ≈ 12/5, occMom/Σm² ≈ 25/3).");
        sb.AppendLine("  - This layer is DIRECTLY USED in the family-index (QG210), sector-ladder (QG192)");
        sb.AppendLine("    and CMB acoustic-peak (QG238) derivations — the octave hierarchy, the beat comb");
        sb.AppendLine("    and the occupancy ratios ARE the resonance operators in those formulas.");
        sb.AppendLine("  - For the mass/coupling sector (QG165-247) the layer was COLLAPSED into the moment");
        sb.AppendLine("    set {Σm, span, λ₂, occMom, Σ√m, Σm²} — the formulas use the collapsed moments");
        sb.AppendLine("    directly rather than re-exposing the beat/locking operators.");
        sb.AppendLine("  - The layer is therefore REAL and partially re-exposed, partially collapsed — the");
        sb.AppendLine("    derivations did not lose a needed resonance step; they encoded it into the");
        sb.AppendLine("    moments in the mass sector while using it explicitly in the resonance sectors.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("RESONANCE LAYER", ResonanceLayerAudit.Classify());
        Assert.True(ResonanceLayerAudit.LayerScore() >= 5);
        Assert.Contains("RESONANCE LAYER", ResonanceLayerAudit.Summary());
    }
}
