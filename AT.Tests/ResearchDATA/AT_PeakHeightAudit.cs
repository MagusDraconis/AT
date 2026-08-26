using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchDATA;

/// <summary>CMB Peak Height Audit — first peak amplitude.</summary>
public class AT_PeakHeightAudit : ResearchTestBase
{
    public AT_PeakHeightAudit(ITestOutputHelper o) : base(o) { }

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
        sb.AppendLine("  Radiation driving : 5-ODE (no nu) / 7-ODE (with nu) system");
        sb.AppendLine("  Neutrino driving  : free-streaming neutrino fluid (delta_nu, v_nu)");
        sb.AppendLine("  Silk damping      : exp(-k^2/k_D^2), k_D from diffusion integral");
        sb.AppendLine("  Amplitude         : D_l1 = (9/25) A_s T_cmb^2 (S^2 + v_b^2) x Silk");
        sb.AppendLine();

        var rNo = PeakHeightAnalyzer.FirstPeakAmplitude();
        var rNu = PeakHeightAnalyzer.FirstPeakAmplitudeNu();
        double planck = 5700.0;

        Sec(sb, "Section 2 — Without vs With neutrinos");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Without nu : l1 = {0:F0}, D_l1 = {1:F0}, Phi = {2:F3}",
            rNo.lPeak, rNo.dPeak, rNo.phi));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  With nu    : l1 = {0:F0}, D_l1 = {1:F0}, Phi = {2:F3}",
            rNu.lPeak, rNu.dPeak, rNu.phi));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Shift      : D_l1 {0:+F0;-F0} micro-K^2 ({1:P0})",
            rNu.dPeak - rNo.dPeak, (rNu.dPeak - rNo.dPeak) / rNo.dPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Residual recovered (of {0:F0} deficit): {1:P0}",
            planck - rNo.dPeak, (rNu.dPeak - rNo.dPeak) / (planck - rNo.dPeak)));
        sb.AppendLine();

        Sec(sb, "Section 3 — Error budget (vs Planck ~5700 micro-K^2)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  With nu : rel. error = {0:P1}", (rNu.dPeak - planck) / planck));
        sb.AppendLine("  Remaining: ISW / full C_l integral: ~10-15%");
        sb.AppendLine("  Tight-coupling approx:             ~5%");
        sb.AppendLine("  Hydrogen-only recombination:       ~1%");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "PeakHeightAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(rNo.lPeak, 150.0, 300.0);
        Assert.InRange(rNo.dPeak, 2000.0, 12000.0);
        Assert.InRange(rNu.lPeak, 150.0, 300.0);
        Assert.InRange(rNu.dPeak, 2000.0, 12000.0);
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
