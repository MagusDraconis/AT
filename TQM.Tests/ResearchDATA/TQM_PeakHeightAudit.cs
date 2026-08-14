using System.Globalization;
using System.Text;
using TQM.Core.ResearchDATA;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;

/// <summary>CMB Peak Height Audit — first peak amplitude.</summary>
public class TQM_PeakHeightAudit : ResearchTestBase
{
    public TQM_PeakHeightAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void PeakHeightAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("CMB Peak Height Audit — first peak amplitude");

        Sec(sb, "Section 1 — Model");
        sb.AppendLine("  Radiation driving : 5-ODE system (Theta0, Theta1, delta_m, v_m, Phi)");
        sb.AppendLine("  Silk damping      : exp(-k^2/k_D^2), k_D from diffusion integral");
        sb.AppendLine("  Amplitude         : D_l1 = (9/25) A_s T_cmb^2 (S^2 + v_b^2) x Silk");
        sb.AppendLine();

        var r = PeakHeightAnalyzer.FirstPeakAmplitude();
        double planck = 5700.0;   // D_l at first peak ~ 5700 micro-K^2

        Sec(sb, "Section 2 — Result");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  l1 (first peak)      = {0:F0}", r.lPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  S^2 (SW)             = {0:F3}", r.s2));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  v_b^2 (Doppler)      = {0:F3}", r.vb2));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Phi (at peak)        = {0:F3}  (initial 1.0 -> decay = {1:P0})", r.phi, 1.0 - r.phi));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  k_D (Silk)           = {0:F3} Mpc^-1", r.kD));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Silk factor          = {0:F4}", r.silk));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D_l1 (this audit)    = {0:F0} micro-K^2", r.dPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Planck first peak    ~ {0:F0} micro-K^2", planck));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  rel. error           = {0:P1}", (r.dPeak - planck) / planck));
        sb.AppendLine();

        Sec(sb, "Section 3 — Error budget (vs Planck ~5700 micro-K^2)");
        sb.AppendLine("  No neutrino driving:        ~10-15%");
        sb.AppendLine("  No ISW / full C_l integral: ~15-25%");
        sb.AppendLine("  Hydrogen-only recombination: ~1%");
        sb.AppendLine("  Tight-coupling approx:       ~5%");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "PeakHeightAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(r.lPeak, 150.0, 300.0);
        Assert.InRange(r.dPeak, 2000.0, 12000.0);   // order-of-magnitude agreement
        Assert.InRange(r.kD, 0.05, 0.5);            // Silk scale ~ 0.1 Mpc^-1
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
