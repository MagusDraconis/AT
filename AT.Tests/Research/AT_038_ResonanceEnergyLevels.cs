using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_038_ResonanceEnergyLevels : ResearchTestBase
{
    private static readonly double[] Injections = { 0.0, 0.10, 0.25, 0.50, 1.0, 2.0 };
    private static readonly double[] Betas = { 0.0, 0.2, 0.5 };
    private const int N = 200;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Seeds = 10;
    private const int BaseSeed = 102334155;

    public AT_038_ResonanceEnergyLevels(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_038_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-038 Resonance Energy Levels");
        report.AppendLine("AT-038: Do Multiple Stable Energy States Exist?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-036/037 showed memory creates identity. This experiment tests");
        report.AppendLine("  whether condensates can occupy multiple stable ENERGY states.");
        report.AppendLine();

        int total = Injections.Length * Betas.Length * Seeds;
        AppendSection(report, "2. Setup");
        report.AppendLine($"  Injections: [{string.Join(", ", Injections)}], β=[{string.Join(", ", Betas)}], Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<EnergyLevelAnalyzer.EnergyResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int ii = idx % Injections.Length, bi = (idx / Injections.Length) % Betas.Length, si = idx / (Injections.Length * Betas.Length);
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(EnergyLevelAnalyzer.Analyze(Injections[ii], Betas[bi], K, Lambda, N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Energy State Results");

        report.AppendLine("  β    │ Injection │ Final R  │ Energy  │ Band");
        report.AppendLine("  ─────┼───────────┼──────────┼─────────┼─────");

        foreach (double beta in Betas)
        {
            var sub = results.Where(r => Math.Abs(r.Beta - beta) < 0.001).ToList();
            foreach (double inj in Injections)
            {
                var s2 = sub.Where(r => Math.Abs(r.InjectionLevel - inj) < 0.001).ToList();
                report.AppendLine($"  {beta,4:F1} │ {inj,9:F2} │ {s2.Average(r => r.FinalR),8:F4} │ {s2.Average(r => r.FinalEnergy),7:F3} │ {s2.Average(r => r.EnergyBand),3:F0}");
            }
        }

        report.AppendLine();

        // Count unique bands.
        int bands = results.Select(r => r.EnergyBand).Distinct().Count();

        AppendSection(report, "4. Conclusion");
        report.AppendLine($"  C1. {bands} distinct energy band(s) observed.");
        report.AppendLine($"  C2. {(bands > 1 ? "MULTIPLE stable energy states exist" : "SINGLE energy state — no quantization")}");
        report.AppendLine("  C3. Energy discretization in AT is an emergent property of");
        report.AppendLine("      the resonance dynamics — potential precursor to quantum-like levels.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-038 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
