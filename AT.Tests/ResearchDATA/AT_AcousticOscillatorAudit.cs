using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchDATA;

/// <summary>Acoustic Oscillator Audit — tight-coupling first peak.</summary>
public class AT_AcousticOscillatorAudit : ResearchTestBase
{
    public AT_AcousticOscillatorAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AcousticOscillatorAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Acoustic Oscillator Audit — tight-coupling first peak");

        Sec(sb, "Section 1 — Model");
        sb.AppendLine("  Tight-coupling oscillator: Theta0'' + R'/(1+R) Theta0' + k^2 c_s^2 Theta0 = -(k^2/3) Phi");
        sb.AppendLine("  Phi = const (adiabatic, matter era); Theta1 = -3 Theta0'/k");
        sb.AppendLine("  2 first-order ODEs (Theta0, Theta0'); no polarization, no lensing");
        sb.AppendLine();

        Sec(sb, "Section 2 — Inputs");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  z*       = {0:F1}", AcousticOscillatorAnalyzer.ZStar()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  R0       = {0:F1} (3 Omega_b / 4 Omega_gamma)", AcousticOscillatorAnalyzer.R0));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D_M(z*)  = {0:F1} Mpc", RecombinationAnalyzer.ComovingDistance(AcousticOscillatorAnalyzer.ZStar()) / RecombinationAnalyzer.Mpc));
        sb.AppendLine();

        Sec(sb, "Section 3 — First peak (SW compression)");
        var pk = AcousticOscillatorAnalyzer.FindFirstPeak();
        double naive = Math.PI / (pk.KPeak / (pk.LPeak / 13868.1)); // not used; placeholder
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  k_peak        = {0:F4} Mpc^-1", pk.KPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  l_peak (SW)   = {0:F1}   (pi/theta* ~ 306; Planck full l1 ~ 220)", pk.LPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  k_peak r_s    = {0:F2} rad  (pi = 3.14)", pk.KPeak * (RecombinationAnalyzer.SoundHorizon(AcousticOscillatorAnalyzer.ZStar()) / RecombinationAnalyzer.Mpc)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  SW amplitude  = {0:F3} (relative to Phi)", pk.SwPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  SW plateau    = {0:F3}", pk.SwPlateau));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  peak/plateau  = {0:F2}x", pk.Ratio));
        sb.AppendLine();

        Sec(sb, "Section 4 — Error budget (vs Planck l1 ~ 220)");
        sb.AppendLine("  Doppler term + Bessel projection (EXCLUDED): ~30-35% in l_peak (dominant)");
        sb.AppendLine("  Constant Phi (neglects Phi' evolution):      ~2-5% in l_peak");
        sb.AppendLine("  Hydrogen-only recombination (z* low):        ~0.7% in l_peak");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "AcousticOscillatorAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(pk.LPeak, 250.0, 420.0);   // SW first compression ~ pi/theta* ~ 306
        Assert.InRange(pk.SwPeak, 0.7, 1.3);      // amplitude O(1) x Phi
        Assert.True(pk.Ratio > 1.5, "First peak should clearly exceed the SW plateau");
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
