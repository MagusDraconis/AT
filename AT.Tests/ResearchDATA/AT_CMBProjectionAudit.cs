using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;

namespace AT.Tests.ResearchDATA;

/// <summary>CMB Projection Audit — LOS projection to the first peak.</summary>
public class AT_CMBProjectionAudit : ResearchTestBase
{
    public AT_CMBProjectionAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void CMBProjectionAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("CMB Projection Audit — LOS projection to first peak");

        Sec(sb, "Section 1 — Model");
        sb.AppendLine("  S(k)    = Theta0 + Psi   (SW source)");
        sb.AppendLine("  v_b(k)  = Theta1         (Doppler source, tight-coupling velocity)");
        sb.AppendLine("  Limber  : l = k D_M;  T^2 = S^2 + v_b^2");
        sb.AppendLine("  D_l     ~ l(l+1) T^2 / 2pi");
        sb.AppendLine("  No polarization, no lensing, no ISW, no Silk damping.");
        sb.AppendLine();

        Sec(sb, "Section 2 — First peak location");
        var sw = CmbProjectionAnalyzer.FirstPeak(includeDoppler: false);
        var full = CmbProjectionAnalyzer.FirstPeak(includeDoppler: true);

        double planck = 220.0;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  SW only      l1 = {0:F1}", sw.lPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  SW + Doppler l1 = {0:F1}   (Planck l1 ~ 220)", full.lPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  rel. error (full vs Planck) = {0:P1}", (full.lPeak - planck) / planck));
        sb.AppendLine();

        Sec(sb, "Section 3 — Shift & contribution");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Doppler shift = {0:F1} -> {1:F1}  (delta = {2:F1})",
            sw.lPeak, full.lPeak, sw.lPeak - full.lPeak));
        sb.AppendLine("  SW contribution: dominant (cos(k r_s) oscillation)");
        sb.AppendLine("  Doppler contribution: quadrature (sin(k r_s), shifts peak down)");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "CMBProjectionAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(sw.lPeak, 250.0, 400.0);      // SW first peak ~ pi/theta* ~ 306
        Assert.InRange(full.lPeak, 160.0, 280.0);    // SW+Doppler ~ 220
        Assert.True(full.lPeak < sw.lPeak, "Doppler term should shift the first peak down");
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
