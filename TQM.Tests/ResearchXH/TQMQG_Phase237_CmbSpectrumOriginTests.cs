using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 237 — CMB Spectrum Origin. Derive the observed CMB spectrum from Q-events: seed power
/// spectrum, octave hierarchy, critical branching, D96 topology. Targets: n_s, scale dependence, acoustic
/// structure. No new primitives, deterministic. Closes QG236's remaining gap.
/// </summary>
public class TQMQG_Phase237_CmbSpectrumOriginTests : ResearchTestBase
{
    public TQMQG_Phase237_CmbSpectrumOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2370_OctaveHierarchyTilt()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2370: the octave-hierarchy tilt 1 − n_s = ln(span)/(Σm − #d)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The seed is Poisson (scale-free, n_s = 1) from critical branching (QG227/228/231).");
        sb.AppendLine("  - The D96 spectrum is not perfectly white: finite span and Z2 doublets give a small tilt.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Span = {CmbSpectrumOrigin.Span():F4}  (QG161)");
        sb.AppendLine($"  ln(span) = {CmbSpectrumOrigin.LnSpan():F6} nats");
        sb.AppendLine($"  Σm = {CmbSpectrumOrigin.TotalModes()} modes, #d = {CmbSpectrumOrigin.DoubletCount()} doublets");
        sb.AppendLine($"  Independent modes Σm − #d = {CmbSpectrumOrigin.IndependentModes()}");
        sb.AppendLine($"  Tilt 1 − n_s = ln(span)/(Σm−#d) = {CmbSpectrumOrigin.Tilt():F6}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The scale-free Poisson base gives n_s = 1 exactly; the finite span and mode");
        sb.AppendLine("    structure of the D96 spectrum add a small octave-hierarchy tilt.");
        sb.AppendLine("  - The tilt is the octave information distributed over the independent modes.");

        Output.WriteLine(sb.ToString());

        Assert.True(CmbSpectrumOrigin.Tilt() > 0.0, "the tilt must be positive (red)");
        Assert.Equal(53, CmbSpectrumOrigin.IndependentModes());
        Assert.Equal(CmbSpectrumOrigin.TotalModes() - CmbSpectrumOrigin.DoubletCount(), CmbSpectrumOrigin.IndependentModes());
    }

    [Fact]
    public void TQMQG2371_SpectralIndexAndRunning()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2371: n_s = 0.96497 (0.007%) and running = 0");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - n_s = 1 − ln(span)/(Σm−#d); the observed value (0.9649) is a comparison anchor only.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  n_s = 1 − tilt = {CmbSpectrumOrigin.SpectralIndex():F6}");
        sb.AppendLine($"      observed 0.9649 → dev {CmbSpectrumOrigin.SpectralIndexDeviation():P3}");
        sb.AppendLine($"  Running α_s = {CmbSpectrumOrigin.Running():F1}");
        sb.AppendLine($"      observed −0.0085 ± 0.0073 → consistent? {CmbSpectrumOrigin.RunningConsistent()}");
        sb.AppendLine($"  Scale-independent (constant tilt)? {CmbSpectrumOrigin.ScaleIndependent()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - n_s = 0.96497 matches the observed 0.9649 to 0.007% — derived from the D96");
        sb.AppendLine("    octave hierarchy with no inflation parameters and no fitted spectral indices.");
        sb.AppendLine("  - The running is zero (constant tilt), consistent with Planck within 1.2σ.");

        Output.WriteLine(sb.ToString());

        Assert.True(CmbSpectrumOrigin.SpectralIndexMatches(), "n_s must match the observed value within 0.1%");
        Assert.True(CmbSpectrumOrigin.RunningConsistent(), "the running must be consistent with Planck");
        Assert.True(CmbSpectrumOrigin.ScaleIndependent(), "the tilt must be scale-independent");
    }

    [Fact]
    public void TQMQG2372_ClassificationPartialOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2372: classification — PARTIAL ORIGIN (n_s derived, acoustic structure partial)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The seed power spectrum (n_s, running) is the central CMB observable and is derived;");
        sb.AppendLine("    the acoustic peak positions require the sound-horizon/recombination sector.");
        sb.AppendLine();

        int score = CmbSpectrumOrigin.OriginScore();
        string classification = CmbSpectrumOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  n_s = {CmbSpectrumOrigin.SpectralIndex():F6}  (observed 0.9649)");
        sb.AppendLine($"  Running = {CmbSpectrumOrigin.Running():F1}  (observed −0.0085 ± 0.0073)");
        sb.AppendLine($"  Acoustic structure derived? {CmbSpectrumOrigin.AcousticStructureDerived()}");
        sb.AppendLine($"  No imports (no inflation params / fitted indices)? {CmbSpectrumOrigin.NoImports()}");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 D96 tilt well-defined ({CmbSpectrumOrigin.Tilt() > 0.0})");
        sb.AppendLine($"    +1 n_s matches ({CmbSpectrumOrigin.SpectralIndexMatches()})");
        sb.AppendLine($"    +1 running consistent + scale-independent ({CmbSpectrumOrigin.RunningConsistent()})");
        sb.AppendLine($"    +1 no imports ({CmbSpectrumOrigin.NoImports()})");
        sb.AppendLine($"  Full chain holds? {CmbSpectrumOrigin.SpectrumChainHolds()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The scalar spectral index n_s = 0.96497 is DERIVED (0.007% from Planck) with a");
        sb.AppendLine("    scale-independent tilt (running = 0, consistent with Planck within 1.2σ).");
        sb.AppendLine("  - The acoustic peak STRUCTURE (positions/heights) is PARTIAL — it requires the");
        sb.AppendLine("    sound-horizon/recombination sector, not derived from Q-events in this phase.");
        sb.AppendLine($"  ⇒ {classification} — the central CMB observable is derived without inflation; the");
        sb.AppendLine("    acoustic-peak observable-level computation remains.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", classification);
        Assert.Equal(4, score);
        Assert.True(CmbSpectrumOrigin.SpectrumChainHolds(), "the n_s + running chain must hold");
    }
}
