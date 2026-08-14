using System.Globalization;
using System.Text;
using TQM.Core.ResearchDATA;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;

/// <summary>Visibility Function Audit — finite-width recombination Doppler
/// suppression and the second acoustic peak.</summary>
public class TQM_VisibilityAudit : ResearchTestBase
{
    public TQM_VisibilityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void VisibilityAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Visibility Function Audit — finite-width recombination");

        Sec(sb, "Section A — Visibility function g(z)");
        var (sigmaEta, zPeak) = RecombinationAnalyzer.VisibilityWidth();
        double cs = RecombinationAnalyzer.SoundSpeed(PeakHeightAnalyzer.ZStar()) / RecombinationAnalyzer.C;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  z_peak   = {0:F1}   (tau = 1)", zPeak));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  sigma_eta = {0:F2} Mpc (conformal-time RMS width)", sigmaEta));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  c_s(z*)  = {0:F4}  (sound speed / c)", cs));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  c_s * sigma_eta = {0:F2} Mpc", cs * sigmaEta));
        sb.AppendLine("  g(z) = sigma_T n_e c/(H(1+z)) e^{-tau(z)}  (Peebles X_e(z)).");
        sb.AppendLine();

        Sec(sb, "Section B — Doppler visibility damping D_v(k)");
        double dM = PeakHeightAnalyzer.DM();
        foreach (double l in new[] { 220.0, 537, 814 })
        {
            double k = l / dM;
            double dv = PeakHeightAnalyzer.DopplerVisibilityDamping(k);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  l={0:F0}  k={1:F4}/Mpc  D_v={2:F4}  D_v^2={3:F4}", l, k, dv, dv * dv));
        }
        sb.AppendLine();

        Sec(sb, "Section C — Peak ratios BEFORE vs AFTER visibility");
        var before = PeakHeightAnalyzer.FindAcousticPeaks(3, dopplerWeight: 1.0 / 3.0);
        var after = PeakHeightAnalyzer.FindAcousticPeaksVisible(3);
        sb.AppendLine("  (acoustic peaks = |S| density extrema; D_l = S^2 + (1/3) D_v^2 v_b^2)");
        sb.AppendLine();
        sb.AppendLine("  BEFORE (D_v=1) : " + FormatBefore(before));
        sb.AppendLine("  AFTER  (D_v<1) : " + FormatAfter(after));
        sb.AppendLine();

        Sec(sb, "Section D — Peak ratios vs Planck");
        sb.AppendLine("  Quantity        BEFORE   AFTER    Planck");
        sb.AppendLine("  D_l2/D_l1       " + R2(before) + "   " + R2v(after) + "   0.44");
        sb.AppendLine("  D_l3/D_l1       " + R3(before) + "   " + R3v(after) + "   0.68");
        sb.AppendLine();

        Sec(sb, "Section E — Honest verdict");
        sb.AppendLine("  1. g(z) has sigma_eta ~ 21 Mpc (conformal-time); the Doppler visibility");
        sb.AppendLine("     damping D_v = exp(-k^2 c_s^2 sigma_eta^2/2) is a SMALL correction");
        sb.AppendLine("     (~1-15% in power) at the peak wavenumbers.");
        sb.AppendLine("  2. It cannot fill the rarefaction peak: at the density extrema v_b ~ 0,");
        sb.AppendLine("     so the 2nd peak is still ~0.08 vs Planck 0.44.");
        sb.AppendLine("  3. The 2nd peak lives at the Doppler-SHIFTED rarefaction (l ~ 537),");
        sb.AppendLine("     not the density extremum (l ~ 620). Resolving it requires the full");
        sb.AppendLine("     Bessel projection (Int v_b^2 j_l'^2), not the Limber quadrature.");
        sb.AppendLine("  4. Conclusion: finite-width recombination is implemented and quantified;");
        sb.AppendLine("     it is a second-order effect. The rarefaction peak is a projection,");
        sb.AppendLine("     not a visibility, effect — next module = full LOS projection.");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "VisibilityAudit_Report.txt"), sb.ToString());

        Assert.True(sigmaEta > 0 && sigmaEta < 50, "Visibility width should be ~Mpc");
        Assert.True(before.Count >= 3 && after.Count >= 3, "Need 3 acoustic peaks");
    }

    private static string FormatBefore(List<(double l, double t2, double s2, double vb2)> p)
        => string.Join("  ", p.Select(x => string.Format(CultureInfo.InvariantCulture,
            "l={0:F0}:T^2={1:F3}", x.l, x.t2)));

    private static string FormatAfter(List<(double l, double t2, double s2, double vb2, double dv)> p)
        => string.Join("  ", p.Select(x => string.Format(CultureInfo.InvariantCulture,
            "l={0:F0}:T^2={1:F3}(D_v={2:F3})", x.l, x.t2, x.dv)));

    private static string R2(List<(double l, double t2, double s2, double vb2)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[1].t2 / p[0].t2);

    private static string R3(List<(double l, double t2, double s2, double vb2)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[2].t2 / p[0].t2);

    private static string R2v(List<(double l, double t2, double s2, double vb2, double dv)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[1].t2 / p[0].t2);

    private static string R3v(List<(double l, double t2, double s2, double vb2, double dv)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[2].t2 / p[0].t2);

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
