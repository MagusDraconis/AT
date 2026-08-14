using System.Globalization;
using System.Text;
using TQM.Core.ResearchDATA;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;

/// <summary>Recombination Audit — minimal z* solver (Saha + Peebles).</summary>
public class TQM_RecombinationAudit : ResearchTestBase
{
    public TQM_RecombinationAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void RecombinationAudit_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { Run(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Recombination Audit — minimal z* solver");

        Sec(sb, "Section 1 — Model");
        sb.AppendLine("  Saha equation   : equilibrium ionization fraction (IC at z=1800)");
        sb.AppendLine("  Peebles ODE     : dX_e/dz (non-equilibrium, case-B + 2s->1s)");
        sb.AppendLine("  Optical depth   : tau(z) = int sigma_T n_e ds");
        sb.AppendLine("  z*              : tau(z*) = 1");
        sb.AppendLine();

        Sec(sb, "Section 2 — Saha sanity (X_e at high z)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  X_e(z=1800) = {0:F6}  (expect ~1, fully ionized)", RecombinationAnalyzer.Saha(1800.0)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  X_e(z=1200) = {0:F6}  (expect ~0.1-0.5, recombination underway)", RecombinationAnalyzer.Saha(1200.0)));
        sb.AppendLine();

        Sec(sb, "Section 3 — z* from full solve");
        var res = RecombinationAnalyzer.Solve();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  z*          = {0:F1}   (Planck 2018: 1089.9)", res.ZStar));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  X_e(z*)     = {0:F4}", res.XeAtZstar));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  tau(z*)     = {0:F4}", res.TauAtZstar));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  ODE steps   = {0}", res.Steps));
        sb.AppendLine();

        Sec(sb, "Section 4 — Classification");
        sb.AppendLine("  Saha   : IMPORTED (equilibrium ionization, 1920)");
        sb.AppendLine("  Peebles: IMPORTED (non-equilibrium correction, 1968)");
        sb.AppendLine("  X_e(z) : IMPORTED (standard recombination; now IMPLEMENTED)");
        sb.AppendLine("  z*     : IMPORTED -> now COMPUTABLE (not TQM-derived)");
        sb.AppendLine();

        Sec(sb, "Section 5 — Summary");
        sb.AppendLine("  The Acoustic-Gap 'smallest missing module' is now IMPLEMENTED.");
        sb.AppendLine("  Next step toward theta*: integrate r_s = int c_s dt (H(z) + c_s already available).");

        Output.WriteLine(sb.ToString());
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        File.WriteAllText(Path.Combine(outDir, "RecombinationAudit_Report.txt"), sb.ToString());

        // ═══ ASSERTIONS ═══
        Assert.InRange(RecombinationAnalyzer.Saha(1800.0), 0.9, 1.0);
        Assert.InRange(res.ZStar, 1000.0, 1200.0);
        Assert.InRange(res.ZStar, 1060.0, 1120.0); // Planck z* = 1089.9
        Assert.InRange(res.XeAtZstar, 0.05, 0.25);
    }

    private static void Sec(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
