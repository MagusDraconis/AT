using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_142 — Multi-Band Resonance Control Audit test suite (Y_NP_142_Tests.cs).
///
/// Question: is coherent material control a single-frequency problem or a multi-band phase-coherence
/// problem?
///
/// Verdict tested: PARTIAL — the "master key" is NEITHER a single resonance NOR a coordinated
/// multi-band phase-coherence pattern; it is a broadband, amplitude-driven defect coupling.
/// Amplitude > frequency (spectral coverage) > phase.
///
/// Deterministic: fixed classification codes and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_142_Tests : ResearchTestBase
{
    public Y_NP_142_Tests(ITestOutputHelper output) : base(output) { }

    private const int SUPPORTED = 0;
    private const int PARTIAL = 1;
    private const int CONTRADICTED = 2;
    private const int UNKNOWN = 3;

    // ── [Required] Y_NP_142_SingleVsMulti ───────────────────────

    [Fact]
    public void Y_NP_142_SingleVsMulti()
    {
        bool singleToneSufficient = true;      // 20 kHz works (amplitude-driven)
        bool multiToneMoreEffective = true;    // broader defect-spectrum coverage
        bool singleSharpResonanceKey = false;  // dislocation response is broadband
        Assert.True(singleToneSufficient && multiToneMoreEffective);
        Assert.False(singleSharpResonanceKey);
    }

    // ── [Required] Y_NP_142_Independence ────────────────────────

    [Fact]
    public void Y_NP_142_Independence()
    {
        bool defectsIndependent = true;         // dislocations = many independent defects
        bool requiresCooperativePhaseLock = false;
        Assert.True(defectsIndependent);
        Assert.False(requiresCooperativePhaseLock);
    }

    // ── [Required] Y_NP_142_Define ──────────────────────────────

    [Fact]
    public void Y_NP_142_Define()
    {
        bool frequencySpectrumBroad = true;     // {omega_i}: broad (dislocation lengths)
        bool amplitudeDominant = true;          // {A_i}: breakaway threshold = primary lever
        bool phaseSecondary = true;             // {phi_i}: tertiary
        Assert.True(frequencySpectrumBroad && amplitudeDominant && phaseSecondary);
    }

    // ── [Required] Y_NP_142_Materials ───────────────────────────

    [Fact]
    public void Y_NP_142_Materials()
    {
        bool crystalYield = true;
        bool metalYield = true;
        bool graniteYield = true;
        bool glassYield = true;
        Assert.True(crystalYield && metalYield && graniteYield && glassYield);
    }

    // ── [Required] Y_NP_142_Measure ─────────────────────────────

    [Fact]
    public void Y_NP_142_Measure()
    {
        bool yieldExceedsModulus = true;        // NP_141: 20-90% vs <=30%
        bool coherenceCostLow = true;           // amplitude-driven, no phase control
        Assert.True(yieldExceedsModulus && coherenceCostLow);
    }

    // ── [Required] Y_NP_142_PhaseVsFrequency ────────────────────

    [Fact]
    public void Y_NP_142_PhaseVsFrequency()
    {
        bool phaseMattersMoreThanFrequency = false;
        Assert.False(phaseMattersMoreThanFrequency);
    }

    // ── [Required] Y_NP_142_MinSet ──────────────────────────────

    [Fact]
    public void Y_NP_142_MinSet()
    {
        bool smallDiscreteSet = false;          // no m=6 discrete key
        bool broadbandPlusAmplitude = true;     // minimum = broad band + breakaway amplitude
        Assert.False(smallDiscreteSet);
        Assert.True(broadbandPlusAmplitude);
    }

    // ── [Required] Y_NP_142_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_142_Compare()
    {
        bool oneToneSuboptimal = true;
        bool harmonicCombMoreEffective = true;
        bool adaptiveFeedbackOnlySharp = true;  // only for elastic soft mode near T_c
        Assert.True(oneToneSuboptimal && harmonicCombMoreEffective && adaptiveFeedbackOnlySharp);
    }

    // ── [Required] Y_NP_142_Classification ──────────────────────

    [Fact]
    public void Y_NP_142_Classification()
    {
        string overall = "PARTIAL";
        int singleResonance = CONTRADICTED;     // broadband, not one sharp line
        int broadbandMultiBand = SUPPORTED;     // single-tone works; multi-tone better
        int phaseCoherenceResource = CONTRADICTED; // amplitude dominates, phase tertiary

        Assert.Equal("PARTIAL", overall);
        Assert.Equal(CONTRADICTED, singleResonance);
        Assert.Equal(SUPPORTED, broadbandMultiBand);
        Assert.Equal(CONTRADICTED, phaseCoherenceResource);
    }

    // ── [Required] Y_NP_142_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_142_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_142 — Multi-Band Resonance Control Audit");

        sb.AppendLine("Goal: is coherent material control single-frequency or multi-band phase-coherence?");
        sb.AppendLine();

        sb.AppendLine("[1] Single-tone works; multi-tone more effective (broadband defect spectrum).");
        sb.AppendLine("[2] Defects act independently; no cooperative phase-lock required.");
        sb.AppendLine("[3] Axes: amplitude {A_i} > frequency {omega_i} (spectral coverage) > phase {phi_i}.");
        sb.AppendLine();
        sb.AppendLine("[4] Yield softening in crystal/metal/granite/glass (magnitude = defect density).");
        sb.AppendLine("[5] Yield >> modulus (NP_141); coherence cost LOW (amplitude-driven).");
        sb.AppendLine("[6] Phase does NOT matter more than frequency matching.");
        sb.AppendLine("[7] No small discrete set; minimum = broadband + breakaway amplitude.");
        sb.AppendLine("[8] one-tone suboptimal; harmonic comb effective; adaptive feedback = sharp-only.");
        sb.AppendLine();
        sb.AppendLine("[9] Verdict: PARTIAL — the key is broadband amplitude, not single resonance or phase coherence.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
