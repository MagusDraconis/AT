using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_065_CouplingSymmetryPrinciple : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const double Beta = 0.5;
    private const int NPerGroup = 50;
    private const int BaseSeed = 650419273;

    public AT_065_CouplingSymmetryPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_065_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-065 Coupling Symmetry Principle");

        report.AppendLine("AT-065: Is Coupling Symmetry the Cause of Attraction?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-064 showed 7/8 coupling laws attract. Only sin(Δθ) fails.");
        report.AppendLine("  This tests whether EVEN symmetry causes attraction and");
        report.AppendLine("  ODD symmetry causes repulsion/zero net force.");
        report.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        var bag = new ConcurrentBag<CouplingSymmetryAnalyzer.SymmetryProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Named laws.
        var namedLaws = new Dictionary<string, (Func<double,double>,double,double)>
        {
            ["E1:cos"]=(d=>Math.Cos(d),1,0),["E2:cos²"]=(d=>Math.Cos(d)*Math.Cos(d),1,0),
            ["E3:exp"]=(d=>Math.Exp(-Math.Abs(d)),1,0),["E4:1/(1+|x|)"]=(d=>1.0/(1+Math.Abs(d)),0.8,0),
            ["O1:sin"]=(d=>Math.Sin(d),0,1),["O2:sin³"]=(d=>Math.Pow(Math.Sin(d),3),0,1),
            ["O3:tanh"]=(d=>Math.Tanh(d),0,0.7),
            ["M1:c+s"]=(d=>Math.Cos(d)+Math.Sin(d),0.5,0.5),
            ["M2:c-s"]=(d=>Math.Cos(d)-Math.Sin(d),0.5,0.5),
            ["M3:.5c+.5s"]=(d=>0.5*Math.Cos(d)+0.5*Math.Sin(d),0.5,0.5),
            ["M4:c*exp"]=(d=>Math.Cos(d)*Math.Exp(-Math.Abs(d)),0.8,0),
        };

        int seedIdx = 0;
        foreach (var kv in namedLaws)
            for (int s = 0; s < 2; s++)
                bag.Add(CouplingSymmetryAnalyzer.RunSymmetryTest(
                    kv.Key, kv.Value.Item2, kv.Value.Item3, kv.Value.Item1,
                    1.0, Beta, K, Lambda, NPerGroup, BaseSeed + seedIdx++ * 7919));

        // Automated sweep: A*cos + B*sin.
        double[] weights = { 0.0, 0.25, 0.5, 0.75, 1.0 };
        foreach (double a in weights)
            foreach (double b in weights)
            {
                if (a < 0.01 && b < 0.01) continue;
                double aCap = a, bCap = b;
                string name = $"Sweep_A{a:F2}_B{b:F2}";
                double ew = aCap / Math.Max(aCap + bCap, 1e-10);
                double ow = bCap / Math.Max(aCap + bCap, 1e-10);
                for (int s = 0; s < 2; s++)
                    bag.Add(CouplingSymmetryAnalyzer.RunSymmetryTest(
                        name, ew, ow, d => aCap * Math.Cos(d) + bCap * Math.Sin(d),
                        1.0, Beta, K, Lambda, NPerGroup, BaseSeed + seedIdx++ * 7919));
            }

        sw.Stop();
        var profiles = bag.ToList();
        report.AppendLine($"  Completed {profiles.Count} profiles in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        var sym = CouplingSymmetryAnalyzer.AnalyzeSymmetry(profiles);

        // ── Section 3: Named Laws ────────────────────────────────────
        AppendSection(report, "3. Named Coupling Laws");

        report.AppendLine("  Law          │ Even │ Odd  │ ΔSep     │ Converge?│ AttrScore");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var p in profiles.Where(p => !p.LawName.StartsWith("Sweep")).OrderBy(p => p.EvenWeight))
            report.AppendLine($"  {p.LawName,-12} │ {p.EvenWeight,4:F1} │ {p.OddWeight,4:F1} │ {p.SeparationChange,8:F4} │ {(p.Converges?"\u25BC YES":"\u25B2 no "),8} │ {p.AttractionScore,8:P1}");

        report.AppendLine();

        // ── Section 4: Symmetry Correlation ──────────────────────────
        AppendSection(report, "4. Symmetry-Attraction Correlation");

        report.AppendLine($"  Even-attraction r:  {sym.EvenAttractionCorrelation,8:F4}");
        report.AppendLine($"  Odd-attraction r:   {sym.OddAttractionCorrelation,8:F4}");
        report.AppendLine($"  Even always attracts:  {(sym.EvenAlwaysAttracts?"YES":"no")}");
        report.AppendLine($"  Odd always fails:      {(sym.OddAlwaysFails?"YES":"no")}");
        report.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        report.AppendLine($"  Q1: Do purely even coupling laws always attract?");
        report.AppendLine($"    {(sym.EvenAlwaysAttracts ? "YES \u2014 All even laws produce convergence" : "NO \u2014 Some even laws fail")}");
        report.AppendLine();

        report.AppendLine($"  Q2: Do purely odd coupling laws fail to attract?");
        report.AppendLine($"    {(sym.OddAlwaysFails ? "YES \u2014 All odd laws fail to attract" : "NO \u2014 Some odd laws attract")}");
        report.AppendLine();

        report.AppendLine($"  Q3: Is attraction proportional to even component?");
        report.AppendLine($"    r = {sym.EvenAttractionCorrelation:F4} — {(Math.Abs(sym.EvenAttractionCorrelation)>0.5?"YES":"NO")}");
        report.AppendLine();

        report.AppendLine($"  Q4: Can odd symmetry produce repulsion?");
        var oddProfs = profiles.Where(p => p.OddWeight > 0.8).ToList();
        bool oddRepels = oddProfs.Any(p => p.SeparationChange > 0.01);
        report.AppendLine($"    {(oddRepels ? "YES \u2014 Odd laws can produce repulsion" : "NO \u2014 Odd laws produce zero net force")}");
        report.AppendLine();

        report.AppendLine($"  Q5: Can sign of motion be predicted from symmetry?");
        report.AppendLine($"    {(sym.EvenAlwaysAttracts && sym.OddAlwaysFails ? "YES \u2014 Symmetry fully predicts attraction sign" : "PARTIALLY \u2014 Symmetry predicts but exceptions exist")}");
        report.AppendLine();

        // ── Section 5: Automated Sweep ───────────────────────────────
        AppendSection(report, "5. Automated Sweep (A*cos + B*sin)");

        var sweep = profiles.Where(p => p.LawName.StartsWith("Sweep")).ToList();
        report.AppendLine($"  {sweep.Count} sweep profiles tested");
        report.AppendLine("  EvenW │ Mean Attr │ Converge%");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (double ew in new[]{0.0,0.25,0.5,0.75,1.0})
        {
            var sub = sweep.Where(p => Math.Abs(p.EvenWeight-ew)<0.15).ToList();
            if (sub.Count==0) continue;
            report.AppendLine($"  {ew,5:F2} │ {sub.Average(p=>p.AttractionScore),8:P1} │ {(double)sub.Count(p=>p.Converges)/sub.Count,8:P0}");
        }
        report.AppendLine();

        report.AppendLine($"  Q6: Is symmetry a better predictor than other variables?");
        report.AppendLine($"    Even-attraction r = {sym.EvenAttractionCorrelation:F4}");
        report.AppendLine($"    {(Math.Abs(sym.EvenAttractionCorrelation)>0.7?"YES \u2014 Superior predictor":"Comparable")}");
        report.AppendLine();

        report.AppendLine($"  Q7: Can a universal symmetry law be identified?");
        report.AppendLine($"    {(sym.EvenAlwaysAttracts&&sym.OddAlwaysFails?"YES: Even→Attract, Odd→Repel/Neutral":"PARTIAL")}");
        report.AppendLine();

        // ── Interpretation ───────────────────────────────────────────
        AppendSection(report, "6. Interpretation");
        report.AppendLine($"  Classification: {sym.Classification}");
        report.AppendLine();

        // ── Conclusion ───────────────────────────────────────────────
        AppendSection(report, "7. Conclusion");
        report.AppendLine($"  C1. {sym.Classification}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-065 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
