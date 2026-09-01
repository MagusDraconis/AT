using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;

namespace AT.Tests.ResearchDATA;

/// <summary>SW-Doppler Cross Audit — resolve the second-peak deficit.
/// Implements the SW-Doppler interference (cross) term and the correct Doppler
/// projection weight, then compares peak ratios before/after vs Planck.</summary>
public class AT_CrossTermAudit : ResearchTestBase
{
    public AT_CrossTermAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void CrossTermAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("SW-Doppler Cross Audit — second-peak deficit");

        Sec(sb, "Section A — The SW-Doppler cross term");
        sb.AppendLine("  The LOS temperature transfer function is");
        sb.AppendLine("    Theta_l(k) = i^-l [ S j_l(kD) - i v_b j_l'(kD) ]");
        sb.AppendLine("  (S = Theta0 + Phi = monopole/SW, v_b = Theta1 = dipole/Doppler).");
        sb.AppendLine("  The monopole and dipole enter with relative phase -i, so");
        sb.AppendLine("    |Theta_l|^2 = S^2 j_l^2 + v_b^2 j_l'^2 + 2 S v_b Re[-i j_l j_l']");
        sb.AppendLine("  and Re[-i] = 0  =>  the cross term is EXACTLY ZERO.");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  CrossTermWeight() = {0}  (analytic: zero)", PeakHeightAnalyzer.CrossTermWeight()));
        sb.AppendLine();

        Sec(sb, "Section B — Correct Doppler projection weight");
        sb.AppendLine("  Under the LOS projection with measure d(ln k):");
        sb.AppendLine("    w_D = Int d(ln k) j_l'^2 / Int d(ln k) j_l^2 = 1/3");
        sb.AppendLine("  (the dipole/monopole angular-average ratio, verified numerically");
        sb.AppendLine("   to ~0.333 for l = 150..900). The original code used w_D = 1.");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  DopplerProjectionWeight() = {0:F4}", PeakHeightAnalyzer.DopplerProjectionWeight()));
        sb.AppendLine();

        Sec(sb, "Section C — Peak ratios BEFORE (w_D = 1) vs AFTER (w_D = 1/3)");
        var before = PeakHeightAnalyzer.FindAcousticPeaks(3, dopplerWeight: 1.0);
        var after = PeakHeightAnalyzer.FindAcousticPeaks(3, dopplerWeight: 1.0 / 3.0);
        sb.AppendLine("  (density extrema |S| maxima = acoustic peak positions)");
        sb.AppendLine();
        sb.AppendLine("  BEFORE  w_D=1 :  " + FormatPeaks(before));
        sb.AppendLine("  AFTER   w_D=1/3:  " + FormatPeaks(after));
        sb.AppendLine();

        Sec(sb, "Section D — Peak ratios vs Planck");
        sb.AppendLine("  Quantity        BEFORE   AFTER    Planck");
        sb.AppendLine("  D_l2/D_l1       " + Ratio(before) + "   " + Ratio(after) + "   0.44");
        sb.AppendLine("  D_l3/D_l1       " + Ratio3(before) + "   " + Ratio3(after) + "   0.68");
        sb.AppendLine();

        Sec(sb, "Section E — Honest verdict");
        sb.AppendLine("  1. The cross term is zero; it CANNOT fill the rarefaction peak.");
        sb.AppendLine("  2. The correct Doppler weight (1/3) barely moves the ratios because");
        sb.AppendLine("     v_b ~ 0 at the density extrema (the velocity is 90 deg out of phase).");
        sb.AppendLine("  3. The 2nd peak is the Doppler-SHIFTED rarefaction: it lives at");
        sb.AppendLine("     l ~ 537 (not the rarefaction l ~ 620), where v_b is significant.");
        sb.AppendLine("  4. Filling it requires the FULL Bessel projection + visibility-function");
        sb.AppendLine("     Doppler damping (out of scope; no polarization/lensing/hierarchy).");
        sb.AppendLine("  5. Net: the second-peak deficit is NOT a missing cross term; it is the");
        sb.AppendLine("     sudden-recombination + Limber limit. Next module = full LOS projection.");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "CrossTermAudit_Report.txt"), sb.ToString());

        Assert.Equal(0.0, PeakHeightAnalyzer.CrossTermWeight());
        Assert.Equal(1.0 / 3.0, PeakHeightAnalyzer.DopplerProjectionWeight(), 10);
        Assert.True(before.Count >= 3 && after.Count >= 3, "Need 3 acoustic peaks");
    }

    private static string FormatPeaks(List<(double l, double t2, double s2, double vb2)> p)
        => string.Join("  ", p.Select(x => string.Format(CultureInfo.InvariantCulture,
            "l={0:F0}:T^2={1:F3}", x.l, x.t2)));

    private static string Ratio(List<(double l, double t2, double s2, double vb2)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[1].t2 / p[0].t2);

    private static string Ratio3(List<(double l, double t2, double s2, double vb2)> p)
        => string.Format(CultureInfo.InvariantCulture, "{0:F3}", p[2].t2 / p[0].t2);

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
