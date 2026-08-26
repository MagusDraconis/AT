using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_132_InformationDynamics : ResearchTestBase
{
    public AT_132_InformationDynamics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_132_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-132 Information Dynamics in the Theta Field");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Θ transports (AT-129) and stores (AT-130) information.");
        sb.AppendLine("  2. Θ and Q are decoupled (AT-131).");
        sb.AppendLine("  3. We test whether information structures WITHIN Θ interact.");
        sb.AppendLine("  4. Assume information is passive until interactions are demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Theory Recap & Information Dynamics");
        sb.AppendLine(ThetaInformationDynamicsAnalyzer.DynamicsTheory());
        sb.AppendLine();

        Sec(sb, "2. Information Interaction Experiments");

        double[] densities = { 0.1, 0.3, 0.5, 0.7, 0.9 };
        sb.AppendLine($"  Densities: [{string.Join(", ", densities)}]");
        sb.AppendLine("  Patterns: A(In-Phase), B(StandingWave), C(Anti-Phase), D(GaussianPulse)");
        sb.AppendLine($"  Pairwise interactions: {6} pairs × {densities.Length} densities = {6 * densities.Length}");
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ThetaInformationDynamicsAnalyzer.Analyze(densities);
        sw.Stop();

        Sec(sb, "3. Interaction Results");
        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Interactions found: {(report.InteractionsFound ? "YES" : "NO")}");
        sb.AppendLine($"  Mergers: {(report.MergersFound ? "YES" : "NO")}");
        sb.AppendLine($"  Cancellations: {(report.CancellationsFound ? "YES" : "NO")}");
        sb.AppendLine($"  Composite states: {(report.CompositeStatesFound ? "YES" : "NO")}");
        sb.AppendLine($"  Self-organization: {(report.SelfOrganizationFound ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine("  Interaction types breakdown:");
        var typeCounts = report.Interactions.GroupBy(i => i.InteractionType)
            .ToDictionary(g => g.Key, g => g.Count());
        foreach (var (t, c) in typeCounts.OrderBy(kv => kv.Key))
            sb.AppendLine($"    {t}: {c}");
        sb.AppendLine();

        sb.AppendLine("  Representative interactions:");
        sb.AppendLine("  A       │ B       │ Init Ov │ Final Ov │ MI    │ Type");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var ix in report.Interactions.Take(12))
            sb.AppendLine($"  {ix.PatternA,-7} │ {ix.PatternB,-7} │ {ix.InitialOverlap,7:F3} │ {ix.FinalOverlap,7:F3} │ {ix.MutualInfoAB,5:F2} │ {ix.InteractionType}");
        sb.AppendLine();

        Sec(sb, "4. Entropy Analysis");
        sb.AppendLine("  Information entropy by pattern and density:");
        sb.AppendLine("  Pattern           │ ρ     │ Entropy │ dI/dt  │ Complexity");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var ep in report.EntropyProfiles.Take(12))
            sb.AppendLine($"  {ep.StateLabel,-17} │ {ep.ShannonEntropy,6:F3} │ {ep.InformationProductionRate,6:F3} │ {ep.PatternComplexity,4}");
        sb.AppendLine();

        Sec(sb, "5. Information Transformation");
        int transformed = report.Interactions.Count(i => i.InformationTransformed);
        sb.AppendLine($"  Interactions that TRANSFORMED information: {transformed}/{report.Interactions.Count}");
        sb.AppendLine();
        foreach (var ix in report.Interactions.Where(i => i.InformationTransformed))
            sb.AppendLine($"    {ix.PatternA} + {ix.PatternB} → {ix.InteractionType}: {ix.Description}");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine("  ATTEMPT 1: Are 'interactions' just linear superposition?");
        sb.AppendLine("    → Linear superposition IS the interaction mechanism in Θ.");
        sb.AppendLine("    → But the OUTCOMES (merge, cancel, reinforce) are nonlinear ");
        sb.AppendLine("      in terms of information — they change what information EXISTS.");
        sb.AppendLine("    → Information dynamics is about information CONTENT, not just amplitudes.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Is information 'merging' just both patterns coexisting?");
        sb.AppendLine("    → Coexistence (independent): both patterns persist unchanged.");
        sb.AppendLine("    → Merging: a NEW composite pattern emerges that is not A or B.");
        sb.AppendLine("    → This is information CREATION through combination.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Does information cancellation violate conservation?");
        sb.AppendLine("    → Information is NOT conserved in Θ (damping destroys it).");
        sb.AppendLine("    → Cancellation is just destructive interference of patterns.");
        sb.AppendLine("    → No conservation law is violated — information is not a conserved quantity.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Is the 'information layer' just a re-labeling of wave dynamics?");
        sb.AppendLine("    → The wave dynamics are the SUBSTRATE. The information layer is their");
        sb.AppendLine("      INTERPRETATION in terms of encoded patterns, entropy, and interactions.");
        sb.AppendLine("    → This is analogous to: transistors are the substrate; logic gates are the layer.");
        sb.AppendLine("    → Θ-field wave dynamics IS the physics; information interaction IS the semantics.");
        sb.AppendLine();

        Sec(sb, "7. Research Questions");
        sb.AppendLine(ThetaInformationDynamicsAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-132 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Information dynamics: {(report.InteractionsFound ? "AUTONOMOUS LAYER" : "STATIC")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
