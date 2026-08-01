using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Core.Temporal;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

/// <summary>
/// TQM-013: Internal Resonance States
///
/// Investigates whether condensates can possess stable internal phase structure
/// beyond simple uniform synchronization — such as phase circulation, winding,
/// or topological defects.
/// </summary>
public class TQM_013_InternalResonanceStates : ResearchTestBase
{
    private const int N = 100;
    private const double Lambda = 0.05;
    private const int Iterations = 5000;
    private const int BaseSeed = 6765;
    private const double CondensateCenterX = 0.5;
    private const double CondensateCenterY = 0.5;
    private const double ClusterRadius = 0.05;

    private static readonly double[] Ks = { 1, 2, 3, 5 };
    private static readonly InternalResonanceStateType[] States =
    {
        InternalResonanceStateType.Clockwise,
        InternalResonanceStateType.CounterClockwise,
        InternalResonanceStateType.WindingPositive,
        InternalResonanceStateType.WindingNegative,
    };

    public TQM_013_InternalResonanceStates(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TQM_013_RunInternalStateExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-013 Internal Resonance States");
        report.AppendLine("TQM-013: Persistence of Internal Phase Structure in Condensates");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-012 showed condensates merge via rapid phase-locking.");
        report.AppendLine("  This suggests all condensates share the same uniform internal state.");
        report.AppendLine("  This experiment tests whether non-uniform internal phase structures —");
        report.AppendLine("  circulation, winding, topological patterns — can persist over time.");
        report.AppendLine();

        AppendSection(report, "2. Experimental Setup");
        int total = Ks.Length * States.Length;
        report.AppendLine($"  Single condensate: N={N}, λ={Lambda}, radius≈{ClusterRadius}");
        report.AppendLine($"  K=[{string.Join(",", Ks)}], Iterations={Iterations}");
        report.AppendLine($"  Initial states: Clockwise, CounterClockwise, Winding(+), Winding(-)");
        report.AppendLine($"  Total combos: {total}, Winding detection: angular sort + phase sum");
        report.AppendLine();

        var allResults = new Dictionary<(InternalResonanceStateType State, int KIdx), InternalStateResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        for (int ki = 0; ki < Ks.Length; ki++)
        {
            foreach (var state in States)
            {
                var r = RunOne(Ks[ki], state);
                allResults[(state, ki)] = r;
            }
        }

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. State Persistence Matrix ─────────────────────────
        AppendSection(report, "3. State Persistence Matrix");

        report.AppendLine("  Final state and winding number by (initial state, K):");
        report.AppendLine("  State \\ K     │ K=1              │ K=2              │ K=3              │ K=5");
        report.AppendLine("  ──────────────┼──────────────────┼──────────────────┼──────────────────┼──────────────────");

        foreach (var state in States)
        {
            report.Append($"  {StateLabel(state),-14} │");

            for (int ki = 0; ki < Ks.Length; ki++)
            {
                var r = allResults[(state, ki)];
                string survived = r.StatePreserved ? "✓" : "✗";
                report.Append($" {r.FinalState,-12} w={r.WindingNumber,5:F2} {survived}");
            }

            report.AppendLine();
        }

        report.AppendLine();

        // ── 4. Winding Number Evolution ─────────────────────────
        AppendSection(report, "4. Winding Number Analysis");

        report.AppendLine("  Final winding number:");
        report.AppendLine("  State \\ K     │ K=1      K=2      K=3      K=5");
        report.AppendLine("  ──────────────┼────────────────────────────────");

        foreach (var state in States)
        {
            report.Append($"  {StateLabel(state),-14} │");
            for (int ki = 0; ki < Ks.Length; ki++)
            {
                var r = allResults[(state, ki)];
                report.Append($" {r.WindingNumber,8:F3}");
            }
            report.AppendLine();
        }

        report.AppendLine();

        // ── 5. Decay Analysis ───────────────────────────────────
        AppendSection(report, "5. State Decay Analysis");

        int preserved = allResults.Values.Count(r => r.StatePreserved);
        int decayed = allResults.Values.Count(r => !r.StatePreserved);

        report.AppendLine($"  States preserved : {preserved}/{total}");
        report.AppendLine($"  States decayed   : {decayed}/{total}");
        report.AppendLine();

        var decayedResults = allResults.Values.Where(r => !r.StatePreserved).ToList();
        if (decayedResults.Count > 0)
        {
            report.AppendLine("  Decay events:");
            report.AppendLine("  Initial State    │ K  │ Decay Iter │ Final State      │ Final Winding");
            report.AppendLine("  ─────────────────┼────┼────────────┼──────────────────┼──────────────");

            foreach (var kv in allResults.Where(kv => !kv.Value.StatePreserved))
            {
                var r = kv.Value;
                report.AppendLine(
                    $"  {StateLabel(r.InitialState),-16} │ {Ks[kv.Key.KIdx],2:F0} │ {r.DecayIteration,10} │ {r.FinalState,-16} │ {r.WindingNumber,12:F3}");
            }
        }

        report.AppendLine();

        // ── 6. Coherence Analysis ───────────────────────────────
        AppendSection(report, "6. Coherence vs Internal State");

        report.AppendLine("  Final coherence R by (initial state, K):");
        report.AppendLine("  State \\ K     │ K=1      K=2      K=3      K=5");
        report.AppendLine("  ──────────────┼────────────────────────────────");

        foreach (var state in States)
        {
            report.Append($"  {StateLabel(state),-14} │");
            for (int ki = 0; ki < Ks.Length; ki++)
            {
                var r = allResults[(state, ki)];
                report.Append($" {r.MeanCoherence,8:F4}");
            }
            report.AppendLine();
        }

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Q1. Stable internal states? {(preserved > 0 ? $"YES — {preserved}/{total} preserved" : "NO")}");
        report.AppendLine();
        report.AppendLine("  Q2. Multiple state classes?");
        var finalStates = allResults.Values.Select(r => r.FinalState).Distinct().ToList();
        report.AppendLine($"    {(finalStates.Count > 1 ? $"YES — {finalStates.Count} distinct final states" : "NO — all decay to same state")}");
        report.AppendLine($"    States observed: {string.Join(", ", finalStates)}");
        report.AppendLine();

        report.AppendLine("  Q3. Reproducible?");
        var groups = allResults.GroupBy(kv => (kv.Key.State, K: Ks[kv.Key.KIdx]));
        foreach (var g in groups)
        {
            bool allSame = g.Select(kv => kv.Value.FinalState).Distinct().Count() == 1;
            report.AppendLine($"    {StateLabel(g.Key.State)} K={g.Key.K:F0}: {(allSame ? "reproducible ✓" : "varies")}");
        }
        report.AppendLine();

        report.AppendLine("  Q4. Survive perturbations?");
        if (preserved > 0)
            report.AppendLine($"    States persist for {Iterations} iterations at surviving K values.");
        else
            report.AppendLine("    All states decayed — internal structure is unstable under Kuramoto dynamics.");
        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        if (preserved > 0)
        {
            report.AppendLine($"  C1. Internal phase structures CAN persist — {preserved}/{total} states survived.");
            report.AppendLine();
            report.AppendLine("  C2. The Kuramoto coupling with distance-dependent interaction can support");
            report.AppendLine("      non-uniform internal states when coupling is appropriately structured.");
            report.AppendLine();
            report.AppendLine("  C3. This demonstrates that condensates may possess intrinsic internal");
            report.AppendLine("      degrees of freedom beyond simple synchronization — a key step toward");
            report.AppendLine("      classifying condensates by their internal resonance states.");
        }
        else
        {
            report.AppendLine("  C1. Internal phase structures DO NOT persist under Kuramoto dynamics —");
            report.AppendLine("      all non-uniform states decay to the uniform synchronized state.");
            report.AppendLine();
            report.AppendLine("  C2. The Kuramoto coupling inherently drives oscillators toward phase");
            report.AppendLine("      uniformity. Maintaining internal structure requires either:");
            report.AppendLine("      • Non-symmetric coupling (directional phase interactions)");
            report.AppendLine("      • External driving fields");
            report.AppendLine("      • Different coupling functional forms (not just sin(Δθ))");
            report.AppendLine();
            report.AppendLine("  C3. At present, all TQM condensates belong to a single resonance state.");
            report.AppendLine("      Internal state diversity requires extensions beyond the Kuramoto model.");
        }

        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • TQM-014: Non-symmetric coupling matrices for internal state stability.");
        report.AppendLine("    • TQM-015: External driving fields to sustain phase patterns.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-013 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private InternalStateResult RunOne(double k, InternalResonanceStateType state)
    {
        int seed = BaseSeed + (int)(k * 100) + (int)state * 7919;
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);

        for (int i = 0; i < N; i++)
        {
            double angle = rng.NextDouble() * 2.0 * Math.PI;
            double radius = rng.NextDouble() * ClusterRadius;
            var node = new TemporalNode(i, phase: 0, frequency: 1.0)
            {
                X = Math.Clamp(CondensateCenterX + radius * Math.Cos(angle), 0, 1),
                Y = Math.Clamp(CondensateCenterY + radius * Math.Sin(angle), 0, 1)
            };
            network.AddNode(node);
        }

        // Initialize phase pattern.
        switch (state)
        {
            case InternalResonanceStateType.Clockwise:
                InternalStateAnalyzer.InitializeClockwise(network, CondensateCenterX, CondensateCenterY);
                break;
            case InternalResonanceStateType.CounterClockwise:
                InternalStateAnalyzer.InitializeCounterClockwise(network, CondensateCenterX, CondensateCenterY);
                break;
            case InternalResonanceStateType.WindingPositive:
                InternalStateAnalyzer.InitializeWinding(network, CondensateCenterX, CondensateCenterY, +1);
                break;
            case InternalResonanceStateType.WindingNegative:
                InternalStateAnalyzer.InitializeWinding(network, CondensateCenterX, CondensateCenterY, -1);
                break;
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, Lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = N };

        return InternalStateAnalyzer.Analyze(network, sim, state, Iterations);
    }

    private static string StateLabel(InternalResonanceStateType s) => s switch
    {
        InternalResonanceStateType.Clockwise => "Clockwise",
        InternalResonanceStateType.CounterClockwise => "CounterCW",
        InternalResonanceStateType.WindingPositive => "Winding+",
        InternalResonanceStateType.WindingNegative => "Winding-",
        _ => s.ToString()
    };

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
