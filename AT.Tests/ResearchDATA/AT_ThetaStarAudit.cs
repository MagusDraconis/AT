using System.Globalization;
using System.Text;
using AT.Core.ResearchDATA;
using AT.Tests.Shared;

namespace AT.Tests.ResearchDATA;

/// <summary>Theta* Audit — compute r_s and theta* from background observables.</summary>
public class AT_ThetaStarAudit : ResearchTestBase
{
    public AT_ThetaStarAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ThetaStarAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Theta* Audit — r_s and theta* from background observables");

        Sec(sb, "Section 1 — Inputs");
        sb.AppendLine("  z*           : RecombinationAnalyzer.Solve() (Saha + Peebles)");
        sb.AppendLine("  R(z)         : 3 rho_b / 4 rho_gamma");
        sb.AppendLine("  c_s(z)       : c / sqrt(3(1+R))");
        sb.AppendLine("  H(z)         : EofZ (FLRW, matter + radiation + Lambda)");
        sb.AppendLine();

        var res = RecombinationAnalyzer.ComputeThetaStar();

        Sec(sb, "Section 2 — Sound speed & R");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  R(z=0)   = {0:F1}   (3 Omega_b / 4 Omega_gamma)", 3.0 * 0.0493 / (4.0 * 5.44e-5)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  c_s(z*)  = {0:F4} c", RecombinationAnalyzer.SoundSpeed(res.ZStar) / RecombinationAnalyzer.C));
        sb.AppendLine();

        Sec(sb, "Section 3 — Results vs Planck");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  z*        = {0:F1}", res.ZStar));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  r_s       = {0:F2} Mpc  (Planck r_d ~ 147.1 Mpc)", res.RsMpc));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  D_M(z*)   = {0:F1} Mpc", res.DmMpc));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  theta*    = {0:E6} rad", res.ThetaStar));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  100 theta* = {0:F5}   (Planck 100 theta_MC = 1.04092)", res.ThetaStar100));
        double relErr = (res.ThetaStar100 - 1.04092) / 1.04092;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  rel. error = {0:P1}", relErr));
        sb.AppendLine();

        Sec(sb, "Section 4 — Classification");
        sb.AppendLine("  R, c_s, r_s, D_M, theta*  : IMPORTED (standard FLRW background)");
        sb.AppendLine("  Recombination z*          : IMPORTED (Saha + Peebles), now IMPLEMENTED");
        sb.AppendLine("  No perturbation theory; no C_l spectrum.");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "ThetaStarAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(res.RsMpc, 120.0, 180.0);
        Assert.InRange(res.DmMpc, 13000.0, 15000.0);
        Assert.InRange(res.ThetaStar100, 0.95, 1.15);
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
