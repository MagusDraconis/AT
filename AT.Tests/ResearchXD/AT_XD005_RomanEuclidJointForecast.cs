using System.Globalization;
using System.Text;
using AT.Core.ResearchXD;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXD;

public class AT_XD005_RomanEuclidJointForecast : ResearchTestBase
{
    public AT_XD005_RomanEuclidJointForecast(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XD005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXD-005 Roman + Euclid Joint Forecast");

        var forecast = RomanEuclidForecastAnalyzer.FullForecast();

        // ═══ SECTION A: AT prediction ═══
        Sec(sb, "Section A — AT Prediction Recap");
        sb.AppendLine(RomanEuclidForecastAnalyzer.PredictionRecap());

        // ═══ SECTION B: CPL conversion ═══
        Sec(sb, "Section B — CPL Conversion");
        var cpl = forecast.Cpl;
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0}", cpl.Description));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  w0 = {0:F4} +/- {1:F4}", cpl.W0, cpl.W0Uncertainty));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  wa = {0:F4} +/- {1:F4}", cpl.Wa, cpl.WaUncertainty));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  CPL: w(a) = {0:F4} + {1:F4}·(1-a)", cpl.W0, cpl.Wa));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  LCDM: w(a) = -1.000 + 0.000·(1-a)"));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Difference: Delta_w0 = {0:F4}, Delta_wa = {1:F4}", cpl.W0 + 1.0, cpl.Wa));
        sb.AppendLine();
        sb.AppendLine(cpl.FittingMethod);

        // ═══ SECTION C: Euclid forecast ═══
        Sec(sb, "Section C — Survey-Specific Forecasts");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-30} {1,7} {2,7} {3,7} {4,7} {5,-10}",
            "Survey", "sigma_w0", "sigma_wa", "S/N w0", "S/N wa", "Timeline"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var s in forecast.Surveys)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-30} {1,7:F3} {2,7:F3} {3,7:F1} {4,7:F1} {5,-10}",
                s.Survey, s.SigmaW0, s.SigmaWa,
                s.SignificanceW0, s.SignificanceWa, s.Timeline));
        }
        sb.AppendLine();
        foreach (var s in forecast.Surveys)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  [{0}] {1}", s.Survey, s.Verdict));
        }

        // ═══ SECTION D: Combined constraint ═══
        Sec(sb, "Section D — Combined Constraint Ellipse");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-25} {1,7} {2,7} {3,10} {4,8}",
            "Combination", "sigma_w0", "sigma_wa", "Dist(LCDM)", "Signif."));
        sb.AppendLine("  " + new string('-', 70));
        foreach (var c in forecast.Combinations)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-25} {1,7:F3} {2,7:F3} {3,10:F1}sigma {4,8:F1}sigma",
                c.Combination, c.SigmaW0, c.SigmaWa,
                c.DistanceFromLCDM, c.JointSignificance));
        }
        sb.AppendLine();
        foreach (var c in forecast.Combinations)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  [{0}] {1}", c.Combination, c.Classification));
        }

        // ═══ SECTION E: AT signature ═══
        Sec(sb, "Section E — The AT Signature");
        sb.AppendLine(RomanEuclidForecastAnalyzer.TheAtSignature());

        // ═══ SECTION F: Validation matrix ═══
        Sec(sb, "Section F — Validation/Failure Matrix");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-30} {1,-12} {2}", "Outcome", "Threshold", "Impact"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var v in forecast.Thresholds)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-30} {1,-12} {2}", v.Outcome, v.RequiredSigma, v.Impact));
        }

        // ═══ SECTION G: Final verdict ═══
        Sec(sb, "Section G — Final Verdict");
        sb.AppendLine(forecast.Verdict);

        // ═══ SUMMARY ═══
        Sec(sb, "Summary — Joint Forecast");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  CPL prediction:  w0 = {0:F4}, wa = {1:F4}", cpl.W0, cpl.Wa));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Best significance: {0:F1}sigma (all three combined)", forecast.BestCaseSignificance));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Forecast class:    {0}", forecast.ForecastClass));
        sb.AppendLine();
        sb.AppendLine("  SURVEY SENSITIVITIES:");
        sb.AppendLine("    Euclid alone:     ~1.0sigma (hint)");
        sb.AppendLine("    Roman alone:      ~1.25sigma (hint)");
        sb.AppendLine("    Euclid+Roman:     ~1.9sigma (suggestive)");
        sb.AppendLine("    All three:        ~2.5sigma (w0), ~3.0sigma (wa) DECISIVE");
        sb.AppendLine();
        sb.AppendLine("  THE SMOKING GUN: wa > 0 at >3sigma in 3-survey joint fit.");
        sb.AppendLine("    wa > 0 = dark energy DECREASES with time.");
        sb.AppendLine("    This is the signature of Lambda(t) = alpha/sqrt(V(t)).");
        sb.AppendLine("    Unique among dark energy models (most predict wa < 0).");
        sb.AppendLine();
        sb.AppendLine("  AT makes a CONCRETE, FALSIFIABLE, TESTABLE prediction.");
        sb.AppendLine("  If (w0, wa) = (-1, 0) at high significance: AT is WRONG.");
        sb.AppendLine("  If (w0, wa) = (-0.985, +0.06): AT is RIGHT.");
        sb.AppendLine();
        sb.AppendLine("  The experimental community just needs to do the measurement.");

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  ResearchXD-005 COMPLETE.");
        sb.AppendLine("  Final pre-observation forecast. 2026-2031 roadmap defined.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(); sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
