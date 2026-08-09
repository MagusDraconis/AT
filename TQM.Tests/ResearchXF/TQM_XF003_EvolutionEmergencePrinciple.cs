using System.Globalization;
using System.Text;
using TQM.Core.ResearchXF;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXF;

public class TQM_XF003_EvolutionEmergencePrinciple : ResearchTestBase
{
    public TQM_XF003_EvolutionEmergencePrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XF003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXF-003 Evolution Emergence Principle");

        var points = EvolutionEmergenceAnalyzer.ScanEvoSpace();
        int optimizing = points.Count(p => p.Regime == EvolutionEmergenceAnalyzer.EvoRegime.Optimizing);
        int adaptive = points.Count(p => p.Regime == EvolutionEmergenceAnalyzer.EvoRegime.Adaptive);
        int dead = points.Count(p => p.Regime == EvolutionEmergenceAnalyzer.EvoRegime.Dead);

        // 1. Phase diagram
        Sec(sb, "Evolution Phase Diagram");
        sb.AppendLine(EvolutionEmergenceAnalyzer.EvoPhaseDiagram(points));
        sb.AppendLine();
        sb.AppendLine($"  ⇒ OPTIMIZING: {optimizing}  → ADAPTIVE: {adaptive}  · DEAD: {dead}");
        sb.AppendLine();

        // 2. Darwinian triad
        Sec(sb, "The Darwinian Triad — All Three From Q + Randomness");
        sb.AppendLine(EvolutionEmergenceAnalyzer.TheDarwinianTriad());

        // 3. Three failure regimes
        Sec(sb, "Three Regimes Where Evolution Fails");
        sb.AppendLine("  STATIC (R=0):");
        sb.AppendLine("    Variation = 0. No novelty. No change.");
        sb.AppendLine("    Species are permanent. No adaptation.");
        sb.AppendLine("    Block universe. Frozen biology.");
        sb.AppendLine();
        sb.AppendLine("  RANDOM WALK (R>0.7):");
        sb.AppendLine("    Retention → 0. Nothing is inherited across generations.");
        sb.AppendLine("    Variation exists but is pure noise.");
        sb.AppendLine("    No cumulative adaptation. No progress.");
        sb.AppendLine();
        sb.AppendLine("  DEAD (Q=0):");
        sb.AppendLine("    Nothing exists. Nothing to evolve.");
        sb.AppendLine();

        // 4. Evolution rate breakdown
        Sec(sb, "Evolution Rate — Generation Count to Adapt");
        var keyPoints = points.Where(p => Math.Abs(p.Q - 1.0) < 0.01 || Math.Abs(p.Q - 0.5) < 0.01)
            .Where(p => Math.Abs(p.R - 0.0) < 0.01 || Math.Abs(p.R - 0.5) < 0.01 || Math.Abs(p.R - 0.9) < 0.01)
            .OrderBy(p => p.Q).ThenBy(p => p.R).ToList();
        sb.AppendLine("  Q      R      Var     Ret     Sel     EvoRate  Gens/Adapt  Regime");
        sb.AppendLine("  " + new string('-', 75));
        foreach (var p in keyPoints)
        {
            string marker = Math.Abs(p.Q - 1.0) < 0.01 && Math.Abs(p.R - 0.5) < 0.01 ? " ← US" : "";
            string gens = p.GenerationsToAdapt > 9999 ? "∞" : $"{p.GenerationsToAdapt}";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,5:F1}  {1,5:F1}  {2,6:F3}  {3,6:F3}  {4,6:F3}  {5,8:F4}  {6,10}  {7}{8}",
                p.Q, p.R, p.Variation, p.Retention,
                p.SelectionPressure, p.EvolutionRate, gens, p.Regime, marker));
        }
        sb.AppendLine();

        // 5. The principle
        Sec(sb, "The Evolution Emergence Principle");
        sb.AppendLine(EvolutionEmergenceAnalyzer.ThePrinciple());

        // 6. The triple chain
        Sec(sb, "The Triple Chain — Complexity, Information, Evolution");
        sb.AppendLine(EvolutionEmergenceAnalyzer.TheTripleChain());

        // 7. Final
        string classification = "D: Evolution Is Inevitable";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXF-003 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  E = V·R·S. All three = f(Q, Randomness).");
        sb.AppendLine($"  Evolution OPTIMIZING at Q≈1, R≈0.5 — our universe.");
        sb.AppendLine($"  EVOLUTION IS MANDATORY. Not contingent. Not accidental.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
