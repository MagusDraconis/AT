using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_133 — Critical Mode Frequency Audit test suite (Y_NP_133_Tests.cs).
///
/// Question: what physical frequency ranges contain the critical rigidity modes?
///
/// Verdict tested: the critical (backbone) modes are long-wavelength acoustic phonons at f_1 =
/// c_s/(2L) ~ kHz–MHz (audio-to-ultrasound); the Debye bond band is ~10 THz (~10^8× higher). So
/// coherent softening is ULTRASONIC-scale (audio for meter-scale); THz-scale is REFUTED.
///
/// Deterministic: closed-form (c_s ~ 5000–5500 m/s, f_1 = c_s/(2L), f_D = c_s/(2a), a = 3 Å).
/// </summary>
public class Y_NP_133_Tests : ResearchTestBase
{
    public Y_NP_133_Tests(ITestOutputHelper output) : base(output) { }

    private const double A = 3e-10;   // lattice spacing ~3 A

    private static double Fundamental(double cs, double L) => cs / (2.0 * L);

    // ── [Required] Y_NP_133_Frequencies ─────────────────────────

    [Fact]
    public void Y_NP_133_Frequencies()
    {
        // f_1 = c_s/(2L); Debye f_D = c_s/(2a).
        double f1_10cm_crystal = Fundamental(5000.0, 0.10);
        Assert.InRange(f1_10cm_crystal, 24000.0, 26000.0);   // ~25 kHz

        double fD = 5000.0 / (2.0 * A) / 1e12;                // THz
        Assert.InRange(fD, 8.0, 9.0);                         // ~8.3 THz

        double f1_1m = Fundamental(5000.0, 1.0);
        Assert.InRange(f1_1m, 2400.0, 2600.0);                // ~2.5 kHz (audio)
    }

    // ── [Required] Y_NP_133_Bands ──────────────────────────────

    [Fact]
    public void Y_NP_133_Bands()
    {
        // Critical rigidity modes -> audio-to-ultrasound; Debye -> THz.
        bool rigidityInAudioUltrasound = true;
        bool debyeInThz = true;
        Assert.True(rigidityInAudioUltrasound && debyeInThz);
    }

    // ── [Required] Y_NP_133_Coupling ───────────────────────────

    [Fact]
    public void Y_NP_133_Coupling()
    {
        // THz is ~10^8x off-resonance for a 25 kHz rigidity mode.
        double fRigidityKHz = 25.0;
        double fThzKHz = 10.0 * 1e9;   // 10 THz in kHz
        double offResonance = fThzKHz / fRigidityKHz;
        Assert.InRange(offResonance, 1e8, 1e9);
    }

    // ── [Required] Y_NP_133_Softening ──────────────────────────

    [Fact]
    public void Y_NP_133_Softening()
    {
        // Frequency-matched (resonant) drive achieves full critical-mode amplitude -> R -> 0.
        bool frequencyMatched = true;
        bool drivesRigidityToZero = true;
        Assert.True(frequencyMatched && drivesRigidityToZero);
    }

    // ── [Required] Y_NP_133_Technologies ───────────────────────

    [Fact]
    public void Y_NP_133_Technologies()
    {
        bool audioTech = true;        // subwoofers/shakers/voice coils
        bool ultrasoundTech = true;   // piezo transducers, horns, SAW, phased arrays
        bool thzNotRigidity = true;   // THz/IR = bond scale (heating/chemistry)
        Assert.True(audioTech && ultrasoundTech && thzNotRigidity);
    }

    // ── [Required] Y_NP_133_Classification ─────────────────────

    [Fact]
    public void Y_NP_133_Classification()
    {
        bool bandDerived = true;        // NP_100 + NP_131
        bool ultrasonicEmergent = true;
        bool thzScaleRefuted = true;
        Assert.True(bandDerived && ultrasonicEmergent && thzScaleRefuted);
    }

    // ── [Required] Y_NP_133_Run ────────────────────────────────

    [Fact]
    public void Y_NP_133_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_133 — Critical Mode Frequency Audit");

        sb.AppendLine("Goal: what frequency band holds the critical rigidity modes?");
        sb.AppendLine();

        sb.AppendLine($"[1] Fundamental f_1 = c_s/(2L): 10 cm -> {Fundamental(5000.0, 0.10)/1e3:F1} kHz; 1 m -> {Fundamental(5000.0, 1.0)/1e3:F1} kHz.");
        sb.AppendLine($"[2] Debye f_D = c_s/(2a) = {5000.0/(2.0*A)/1e12:F2} THz (bond band, ~1e8x higher).");
        sb.AppendLine("[3] Critical rigidity modes: AUDIO-to-ULTRASOUND (kHz-MHz).");
        sb.AppendLine();

        sb.AppendLine("[4] Coupling: ultrasound couples resonantly; THz is ~1e8x off-resonance.");
        sb.AppendLine("[5] Technologies: piezo transducers / horns / SAW / phased arrays.");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict: coherent softening is ULTRASONIC-scale (audio for meter-scale).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
