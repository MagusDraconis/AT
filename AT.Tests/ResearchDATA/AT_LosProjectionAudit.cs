using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchDATA;

/// <summary>LOS Projection Audit — exact j_l / j_l' projection and peak locations.</summary>
public class AT_LosProjectionAudit : ResearchTestBase
{
    public AT_LosProjectionAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void LosProjectionAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("LOS Projection Audit — exact j_l / j_l' projection");

        Sec(sb, "Section A — Exact LOS projection");
        sb.AppendLine("  Theta_l(k) = S(k) j_l(kD) - i v_b(k) j_l'(kD)   (SW + Doppler)");
        sb.AppendLine("  D_l = l(l+1) Int d(ln k) [S^2 j_l^2 + v_b^2 j_l'^2] e^{-k^2/kD^2} D_v^2");
        sb.AppendLine("  (cross term = 0). Sources: S = A cos(k r_s) - R Phi, v_b = B sin(k r_s).");
        sb.AppendLine();

        Sec(sb, "Section B — Peak scan (local maxima of D_l)");
        var peaks = LosProjectionAnalyzer.FindPeaks(4);
        foreach (var (l, dl, sw, dop) in peaks)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  l={0:F0}   D_l={1:F4}   SW={2:F4}   Dop={3:F4}", l, dl, sw, dop));
        sb.AppendLine();

        Sec(sb, "Section C — Peak positions vs Planck");
        sb.AppendLine("  peak           model     Planck   error");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  l1 (compression)  {0:F0}     220      {1:+0.0;-0.0}%", peaks[0].l, (peaks[0].l - 220) / 220 * 100));
        sb.AppendLine("  l2 (rarefaction)  MISSING   537      ---  (dip, not a peak)");
        if (peaks.Count >= 2)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  l3 (compression)  {0:F0}     814      {1:+0.0;-0.0}%", peaks[1].l, (peaks[1].l - 814) / 814 * 100));
        sb.AppendLine();

        Sec(sb, "Section D — Peak ratios vs Planck");
        double d1 = peaks[0].dl;
        // rarefaction = density extremum between the two compressions
        double lRare = 0.5 * (peaks[0].l + peaks[1].l);
        double kRare = lRare / LosProjectionAnalyzer.DM();
        double sRare = LosProjectionAnalyzer.S(kRare);
        double dRare = sRare * sRare;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D_l2/D_l1 (rarefaction, SW) = {0:F3}   (Planck 0.44)", dRare / d1));
        if (peaks.Count >= 2)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  D_l3/D_l1 (compression)    = {0:F3}   (Planck 0.68)", peaks[1].dl / d1));
        sb.AppendLine();

        Sec(sb, "Section E — Honest verdict");
        sb.AppendLine("  1. The exact j_l/j_l' projection reproduces the COMPRESSION peaks only");
        sb.AppendLine("     (l1, l3); it produces NO rarefaction peak (l ~ 537 is a dip).");
        sb.AppendLine("  2. The compression peaks sit at k r_s = n*pi (no acoustic phase shift),");
        sb.AppendLine("     so l1 ~ 304 vs Planck 220 (+38%) — the Doppler phase shift");
        sb.AppendLine("     phi ~ 0.88 rad is not captured by the sudden-recombination model.");
        sb.AppendLine("  3. D_l = S^2 + v_b^2/3 is monotonic between compressions: the Doppler");
        sb.AppendLine("     fills the density zero-crossing (sin^2=1) more than the rarefaction");
        sb.AppendLine("     (sin=0), so the rarefaction is a minimum, not a peak.");
        sb.AppendLine("  4. The observed 2nd peak (l~537, 0.44) therefore requires physics beyond");
        sb.AppendLine("     sudden recombination (finite-width velocity weighting, baryon-photon");
        sb.AppendLine("     decoupling, or the ISW) — out of scope. Honest negative result.");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "LosProjectionAudit_Report.txt"), sb.ToString());

        Assert.True(peaks.Count >= 2, "Need >=2 peaks from the LOS projection");
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
