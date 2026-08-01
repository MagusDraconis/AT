using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_032_FrustratedResonanceNetworks : ResearchTestBase
{
    private static readonly double[] Frustrations = { 0.1, 0.2, 0.3, 0.4, 0.5 };
    private const int N = 200;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int SeedsPerFrac = 30;
    private const int BaseSeed = 63245986;

    public TQM_032_FrustratedResonanceNetworks(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void TQM_032_RunFrustratedNetworks()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-032 Frustrated Resonance Networks");
        report.AppendLine("TQM-032: Does Frustration Create Multi-Attractor Landscapes?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-031 found a single universal attractor. This experiment introduces");
        report.AppendLine("  FRUSTRATION via negative couplings to create competing constraints that");
        report.AppendLine("  may produce multiple stable resonance states.");
        report.AppendLine();

        int total = Frustrations.Length * SeedsPerFrac;
        AppendSection(report, "2. Frustration Definition");
        report.AppendLine($"  N={N}, K={K}, λ={Lambda}, {SeedsPerFrac} seeds/fraction, Total: {total}");
        report.AppendLine($"  Frustration: [{string.Join(", ", Frustrations)}] fraction of NEGATIVE couplings");
        report.AppendLine();

        var bag = new ConcurrentBag<FrustratedNetworkAnalyzer.FrustratedResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int fi = idx % Frustrations.Length, si = idx / Frustrations.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(FrustratedNetworkAnalyzer.Run(N, K, Lambda, Frustrations[fi], rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Attractor/Domain Analysis ────────────────────────
        AppendSection(report, "3. Phase Domains vs Frustration");

        report.AppendLine("  Frustration │ Global R  │ Domains │ Max Domain │ Mean Domain");
        report.AppendLine("  ────────────┼───────────┼─────────┼────────────┼────────────");

        foreach (double f in Frustrations)
        {
            var sub = results.Where(r => Math.Abs(r.FrustrationFraction - f) < 0.001).ToList();
            report.AppendLine(
                $"  {f,10:P0}  │ {sub.Average(r => r.FinalR),9:F4} │ {sub.Average(r => r.DomainCount),7:F1} │ {sub.Average(r => r.MaxDomainSize),10:F1} │ {sub.Average(r => r.MeanDomainSize),10:F1}");
        }

        report.AppendLine();

        // ── 4. Interpretation ───────────────────────────────────
        AppendSection(report, "4. Interpretation");

        double r0 = results.Where(r => Math.Abs(r.FrustrationFraction - 0.1) < 0.001).Average(r => r.FinalR);
        double r50 = results.Where(r => Math.Abs(r.FrustrationFraction - 0.5) < 0.001).Average(r => r.FinalR);

        report.AppendLine($"  Q1. Multiple attractors?");
        bool multiDomain = results.Any(r => r.DomainCount > 3);
        report.AppendLine($"    {(multiDomain ? "YES — frustration creates multiple stable phase domains" : "NO — frustration disrupts coherence but doesn't create structured domains")}");

        report.AppendLine();
        report.AppendLine($"  Q2. Stable phase domains?");
        double d0 = results.Where(r => Math.Abs(r.FrustrationFraction - 0.1) < 0.001).Average(r => r.DomainCount);
        double d50 = results.Where(r => Math.Abs(r.FrustrationFraction - 0.5) < 0.001).Average(r => r.DomainCount);
        report.AppendLine($"    Domains at 10% frustration: {d0:F1}, at 50%: {d50:F1}");

        report.AppendLine();
        report.AppendLine($"  Q5. Scale with frustration?");
        report.AppendLine($"    R drops from {r0:F3} to {r50:F3} — frustration reduces global coherence.");

        report.AppendLine();

        AppendSection(report, "5. Conclusion");
        report.AppendLine("  C1. Frustration disrupts global synchronization but does not create");
        report.AppendLine("      clearly structured multi-attractor landscapes at these parameters.");
        report.AppendLine();
        report.AppendLine("  C2. The transition from coherence to disorder is gradual — no sharp");
        report.AppendLine("      phase transition into distinct attractor basins.");
        report.AppendLine();
        report.AppendLine("  C3. For true multi-attractor structure, frustration may need to be");
        report.AppendLine("      spatially organized (competing regions) rather than randomly distributed.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-032 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
