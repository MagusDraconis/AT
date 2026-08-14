using System.Globalization;
using System.Text;
using TQM.Core.ResearchDATA;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;

/// <summary>Higher Peaks Audit — second and third acoustic peaks.</summary>
public class TQM_HigherPeaksAudit : ResearchTestBase
{
    public TQM_HigherPeaksAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void HigherPeaksAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Higher Peaks Audit — second and third acoustic peaks");

        var ts = RecombinationAnalyzer.ComputeThetaStar();
        double theta = ts.ThetaStar;

        Sec(sb, "Section A — Analytical peak positions (spacing)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  theta*  = {0:F6} rad   (pi/theta* = {1:F0})", theta, Math.PI / theta));
        sb.AppendLine("  Acoustic peaks are density extrema of S = Theta0 + Phi.");
        sb.AppendLine("  Even spacing Delta_l ~ pi/theta* predicts l2, l3 from l1.");
        sb.AppendLine();

        Sec(sb, "Section B — Density extrema (|S| local maxima)");
        var peaks = PeakHeightAnalyzer.FindAcousticPeaks(count: 3, lMax: 960);
        foreach (var (l, t2, s2, vb2) in peaks)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  l = {0:F0}   S^2 = {1:F4}   v_b^2 = {2:F4}   T^2 = {3:F4}", l, s2, vb2, t2));
        sb.AppendLine();

        Sec(sb, "Section C — Peak ratios");
        if (peaks.Count >= 3)
        {
            double d1 = peaks[0].t2, d2 = peaks[1].t2, d3 = peaks[2].t2;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  D_l2/D_l1 = {0:F3}   (Planck ~ 0.44)", d2 / d1));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  D_l3/D_l1 = {0:F3}   (Planck ~ 0.68)", d3 / d1));
        }
        sb.AppendLine();

        Sec(sb, "Section D — Planck comparison");
        sb.AppendLine("  Planck : l1 ~ 220, l2 ~ 537, l3 ~ 814");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Model  : l1 ~ {0:F0}, l2 ~ {1:F0}, l3 ~ {2:F0}",
            peaks.Count > 0 ? peaks[0].l : 0, peaks.Count > 1 ? peaks[1].l : 0, peaks.Count > 2 ? peaks[2].l : 0));
        sb.AppendLine();

        Sec(sb, "Section E — Honest limitations");
        sb.AppendLine("  1. Model peak positions are SW (density) extrema, BEFORE the");
        sb.AppendLine("     Doppler projection shift (l1: 300 -> 220 in Projection Audit).");
        sb.AppendLine("  2. The Limber quadrature T^2 = S^2 + v_b^2 drops the SW-Doppler");
        sb.AppendLine("     cross term, so the 2nd (rarefaction) peak is under-filled.");
        sb.AppendLine("  3. D_l3/D_l1 is robust (density-dominated); D_l2/D_l1 needs the");
        sb.AppendLine("     full Bessel projection with cross term (out of scope here).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "HigherPeaksAudit_Report.txt"), sb.ToString());

        Assert.True(peaks.Count >= 3, "Should find at least 3 acoustic (density) peaks");
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
