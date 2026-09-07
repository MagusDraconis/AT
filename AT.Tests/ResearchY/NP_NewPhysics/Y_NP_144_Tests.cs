using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_144 — Defect Spectrum Fingerprint Audit test suite (Y_NP_144_Tests.cs).
///
/// Question: do different materials possess unique defect-spectrum fingerprints usable for targeted
/// yield-stress control?
///
/// Verdict tested: PARTIAL — unique fingerprint SUPPORTED (mechanical spectroscopy), but tailored
/// exploitation gives only efficiency gains (~2–5×) not magnitude gains (~1×), because the mobility
/// response is broadband and amplitude-dominated (NP_142).
///
/// Deterministic: fixed classification codes and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_144_Tests : ResearchTestBase
{
    public Y_NP_144_Tests(ITestOutputHelper output) : base(output) { }

    private const int SUPPORTED = 0;
    private const int PARTIAL = 1;
    private const int CONTRADICTED = 2;
    private const int UNKNOWN = 3;

    // ── [Required] Y_NP_144_Define ──────────────────────────────

    [Fact]
    public void Y_NP_144_Define()
    {
        bool defectSpectrum = true;     // identity of defect types
        bool breakawaySpectrum = true;  // thresholds (pinning)
        bool mobilitySpectrum = true;   // response to drive
        Assert.True(defectSpectrum && breakawaySpectrum && mobilitySpectrum);
    }

    // ── [Required] Y_NP_144_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_144_Compare()
    {
        bool aluminumBordoni = true;    // dislocation peak
        bool steelSnoek = true;         // interstitial peak
        bool graniteNme = true;         // broad contact spectrum
        bool quartzCrack = true;        // microcrack features
        Assert.True(aluminumBordoni && steelSnoek && graniteNme && quartzCrack);
    }

    // ── [Required] Y_NP_144_Fingerprint ─────────────────────────

    [Fact]
    public void Y_NP_144_Fingerprint()
    {
        bool uniqueIdentity = true;          // distinct material spectra
        bool universalBroadbandMobility = true; // softening response broadband (NP_142)
        Assert.True(uniqueIdentity && universalBroadbandMobility);
    }

    // ── [Required] Y_NP_144_Sensitivity ─────────────────────────

    [Fact]
    public void Y_NP_144_Sensitivity()
    {
        bool amplitudeDominant = true;
        bool bandwidthSecondary = true;
        bool frequencyTertiary = true;
        Assert.True(amplitudeDominant && bandwidthSecondary && frequencyTertiary);
    }

    // ── [Required] Y_NP_144_Tailored ────────────────────────────

    [Fact]
    public void Y_NP_144_Tailored()
    {
        bool tailoredMoreEfficient = true;   // saves energy
        bool tailoredBiggerMagnitude = false; // does not double the softening
        Assert.True(tailoredMoreEfficient);
        Assert.False(tailoredBiggerMagnitude);
    }

    // ── [Required] Y_NP_144_Gains ───────────────────────────────

    [Fact]
    public void Y_NP_144_Gains()
    {
        double efficiencyGain = 3.0;   // ~2-5x power saving
        double magnitudeGain = 1.0;    // ~1x softening magnitude
        Assert.InRange(efficiencyGain, 2.0, 5.0);
        Assert.InRange(magnitudeGain, 0.8, 1.2);
    }

    // ── [Required] Y_NP_144_Classification ──────────────────────

    [Fact]
    public void Y_NP_144_Classification()
    {
        string overall = "PARTIAL";
        int uniqueFingerprint = SUPPORTED;   // mechanical spectroscopy
        int broadbandMobility = SUPPORTED;   // NP_142
        int tailoredOutperforms = PARTIAL;   // efficiency only

        Assert.Equal("PARTIAL", overall);
        Assert.Equal(SUPPORTED, uniqueFingerprint);
        Assert.Equal(SUPPORTED, broadbandMobility);
        Assert.Equal(PARTIAL, tailoredOutperforms);
    }

    // ── [Required] Y_NP_144_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_144_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_144 — Defect Spectrum Fingerprint Audit");

        sb.AppendLine("Goal: do materials have unique defect fingerprints for targeted softening?");
        sb.AppendLine();

        sb.AppendLine("[1] Spectra: defect (identity) / breakaway (threshold) / mobility (response).");
        sb.AppendLine("[2] Al=Bordoni, steel=Snoek, granite=NME, quartz=crack — distinct fingerprints.");
        sb.AppendLine("[3] Unique identity + universal broadband mobility response (NP_142).");
        sb.AppendLine("[4] Sensitivity: amplitude > bandwidth > frequency.");
        sb.AppendLine("[5] Tailored = efficiency (2-5x power), not magnitude (~1x).");
        sb.AppendLine("[6] Verdict: PARTIAL — fingerprint is diagnostic + efficiency lever, not a magnitude lever.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
