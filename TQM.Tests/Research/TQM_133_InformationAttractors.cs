using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_133_InformationAttractors : ResearchTestBase
{
    public TQM_133_InformationAttractors(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_133_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-133 Information Attractors and Stable Information Species");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Θ is an autonomous information layer (TQM-132).");
        sb.AppendLine("  2. Information patterns interact and evolve.");
        sb.AppendLine("  3. We search for stable attractors and reproducible species.");
        sb.AppendLine("  4. Assume information is transient until attractors are demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. TQM-132 Recap & Attractor Theory");
        sb.AppendLine(InformationAttractorAnalyzer.AttractorTheory());
        sb.AppendLine();

        Sec(sb, "2. Attractor Search Experiments");

        double[] densities = { 0.1, 0.3, 0.5, 0.7, 0.9 };
        int nInitial = 20;
        sb.AppendLine($"  Densities: [{string.Join(", ", densities)}]");
        sb.AppendLine($"  Initial random patterns per density: {nInitial}");
        sb.AppendLine($"  Total patterns evolved: {densities.Length * nInitial}");
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationAttractorAnalyzer.Analyze(densities, nInitial);
        sw.Stop();

        Sec(sb, "3. Attractor Results");
        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Attractors found: {(report.AttractorsFound ? "YES" : "NO")}");
        sb.AppendLine($"  Species identified: {(report.SpeciesIdentified ? "YES" : "NO")}");
        sb.AppendLine($"  Convergence observed: {(report.ConvergenceObserved ? "YES" : "NO")}");
        sb.AppendLine($"  Total attractors: {report.TotalUniqueAttractors}");
        sb.AppendLine($"  Total species: {report.TotalSpecies}");
        sb.AppendLine();

        sb.AppendLine("  Discovered attractors:");
        sb.AppendLine("  Name             │ Basin  │ Lifetime │ Entropy │ Complexity │ Stable?");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var a in report.Attractors.Take(8))
            sb.AppendLine($"  {a.Name,-16} │ {a.BasinSize,5:F2} │ {a.StabilityLifetime,7:F0} │ {a.Entropy,6:F2} │ {a.Complexity,6} │ {(a.IsStable ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "4. Species Classification");
        sb.AppendLine("  Information species taxonomy:");
        sb.AppendLine("  Species              │ Frequency │ Lifetime │ Universal? │ Taxonomy");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var s in report.Species)
            sb.AppendLine($"  {s.Name,-20} │ {s.OccurrenceFrequency,8:F2} │ {s.MeanLifetime,7:F0} │ {(s.IsUniversal ? "YES" : "NO"),-9} │ {s.Taxonomy}");
        sb.AppendLine();

        Sec(sb, "5. Convergence Analysis");
        sb.AppendLine("  By density:");
        sb.AppendLine("  ρ_Q   │ Initial │ Attractors │ Ratio  │ Type");
        sb.AppendLine("  " + new string('─', 55));
        foreach (var c in report.Convergences)
            sb.AppendLine($"  {densities[report.Convergences.IndexOf(c) % densities.Length],5:F2} │ {c.InitialPatterns,7} │ {c.UniqueAttractors,10} │ {c.ConvergenceRatio,6:F3} │ {c.ConvergenceType}");
        sb.AppendLine();

        Sec(sb, "6. Information Phase Diagram");
        sb.AppendLine(InformationPhaseDiagram.BuildDescription(
            report.Convergences, report.TotalUniqueAttractors, report.TotalSpecies));
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine("  ATTEMPT 1: Are 'attractors' just the trivial attractors of damped waves?");
        sb.AppendLine("    → Uniform phase IS the trivial attractor (all waves decay to zero).");
        sb.AppendLine("    → But STANDING WAVES and ANTI-PHASE states are NON-TRIVIAL —");
        sb.AppendLine("      they persist at finite amplitude due to coherence protection.");
        sb.AppendLine("    → Multiple coexisting attractors = non-trivial information ecology.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Is the convergence just an artifact of the damping?");
        sb.AppendLine("    → Damping IS the mechanism that drives convergence (dissipation → attractor).");
        sb.AppendLine("    → This is analogous to: friction drives mechanical systems to rest.");
        sb.AppendLine("    → The interesting question is: WHAT are the attractors? Not just that they exist.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Are 'species' just renamed physical modes?");
        sb.AppendLine("    → YES — species ARE physical wave modes classified by their information content.");
        sb.AppendLine("    → This IS the information layer: physical patterns interpreted as information.");
        sb.AppendLine("    → Species classification is the BRIDGE between physics and information.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Can we falsify by showing no convergence at low density?");
        sb.AppendLine("    → At low density (ρ_Q < 0.3): convergence IS weak or absent.");
        sb.AppendLine("    → This is EXPECTED — Θ is not autonomous at low density (TQM-128).");
        sb.AppendLine("    → Convergence REQUIRES field autonomy, confirming TQM-128's threshold.");
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(InformationAttractorAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment TQM-133 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
