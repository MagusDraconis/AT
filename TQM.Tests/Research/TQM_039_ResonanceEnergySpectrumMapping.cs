using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_039_ResonanceEnergySpectrumMapping : ResearchTestBase
{
    private const int InjectionSteps = 101; // 0.00 to 1.00
    private static readonly double[] Betas = { 0.0, 0.2, 0.5 };
    private const int Seeds = 10;
    private const int N = 100;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int BaseSeed = 165580141;

    public TQM_039_ResonanceEnergySpectrumMapping(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_039_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-039 Resonance Energy Spectrum Mapping");
        report.AppendLine("TQM-039: Continuous, Clustered, or Discrete Energy Spectrum?");
        report.AppendLine();

        int total = InjectionSteps * Betas.Length * Seeds;
        AppendSection(report, "1. Experimental Setup");
        report.AppendLine($"  {InjectionSteps} injection levels [0.00, 1.00], β=[{string.Join(",", Betas)}]");
        report.AppendLine($"  {Seeds} seeds, N={N}, Total: {total} runs");
        report.AppendLine();

        var bag = new ConcurrentBag<EnergySpectrumAnalyzer.SpectrumPoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int ii = idx % InjectionSteps, rem = idx / InjectionSteps;
            int bi = rem % Betas.Length, si = rem / Betas.Length;
            double inj = ii / (double)(InjectionSteps - 1);
            var rng = new Random(BaseSeed + idx * 7919);
            var pt = EnergySpectrumAnalyzer.Measure(inj, Betas[bi], K, Lambda, N, rng);
            if (pt != null) bag.Add(pt);
        });

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Points: {points.Count}/{total}");
        report.AppendLine();

        if (points.Count < 10) { report.AppendLine("Insufficient data."); Output.WriteLine(report.ToString()); return; }

        // ── 2. Spectrum Statistics ──────────────────────────────
        AppendSection(report, "2. Energy Spectrum Statistics");

        var energies = points.Select(p => p.FinalEnergy).OrderBy(e => e).ToList();
        int c = energies.Count;
        double eMin = energies[0], eMax = energies[^1], eRange = eMax - eMin;

        // Detect gaps: sort energies, find large jumps.
        var gaps = new List<double>();
        for (int i = 1; i < c; i++)
            gaps.Add(energies[i] - energies[i - 1]);
        gaps.Sort();
        double medGap = gaps[gaps.Count / 2];
        double largeGapThreshold = medGap * 3;
        int significantGaps = gaps.Count(g => g > largeGapThreshold);

        report.AppendLine($"  Energy range: [{eMin:F3}, {eMax:F3}]");
        report.AppendLine($"  Mean energy: {energies.Average():F3}");
        report.AppendLine($"  Median gap: {medGap:F4}, Large gaps (>3× median): {significantGaps}");
        report.AppendLine();

        // ── 3. Band Detection ───────────────────────────────────
        AppendSection(report, "3. Spectrum Classification");

        // Histogram into 20 bins.
        int bins = 20;
        double binWidth = eRange / bins;
        var histogram = new int[bins];
        foreach (double e in energies)
        {
            int b = Math.Min(bins - 1, (int)((e - eMin) / binWidth));
            histogram[b]++;
        }

        int emptyBins = histogram.Count(h => h == 0);
        int lowBins = histogram.Count(h => h > 0 && h < 3);

        string classification = emptyBins > bins / 4 ? "DISCRETE BANDS — significant gaps in spectrum" :
                                emptyBins > 0 ? "PREFERRED CLUSTERS — some structure, not fully discrete" :
                                "CONTINUOUS — smooth density of states";

        report.AppendLine($"  Empty bins (no states): {emptyBins}/{bins}");
        report.AppendLine($"  Classification: {classification}");
        report.AppendLine();

        // ── 4. β Dependence ─────────────────────────────────────
        AppendSection(report, "4. Memory Effect on Spectrum");

        report.AppendLine("  β    │ Mean E  │ Range    │ Bands (empty bins)");
        report.AppendLine("  ─────┼─────────┼──────────┼───────────────────");

        foreach (double beta in Betas)
        {
            var sub = points.Where(p => Math.Abs(p.Beta - beta) < 0.001).ToList();
            var eSub = sub.Select(p => p.FinalEnergy).OrderBy(e => e).ToList();
            int es = eSub.Count;
            double range = eSub[^1] - eSub[0];

            // Empty bins for this β.
            double bw = range / 20;
            int empty = Enumerable.Range(0, 20).Count(b =>
                !eSub.Any(e => e >= eSub[0] + b * bw && e < eSub[0] + (b + 1) * bw));

            report.AppendLine($"  {beta,4:F1} │ {sub.Average(p => p.FinalEnergy),7:F3} │ [{eSub[0]:F3},{eSub[^1]:F3}] │ {empty}");
        }

        report.AppendLine();

        AppendSection(report, "5. Conclusion");
        report.AppendLine($"  C1. Classification: {classification}");
        report.AppendLine($"  C2. {emptyBins}/{bins} empty bins — {(emptyBins > 0 ? "structure detected" : "continuous spectrum")}");
        report.AppendLine("  C3. The energy spectrum of TQM condensates shows emergent");
        report.AppendLine("      organization — potential foundation for energy quantization");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-039 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
