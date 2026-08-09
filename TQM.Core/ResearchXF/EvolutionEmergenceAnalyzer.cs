namespace TQM.Core.ResearchXF;

using System.Globalization;

/// <summary>
/// Derives the Evolution Emergence Principle from Q + Randomness.
/// ResearchXF-003: Evolution Emergence Principle
/// </summary>
public static class EvolutionEmergenceAnalyzer
{
    public enum EvoRegime { Dead, Static, RandomWalk, Adaptive, Optimizing }

    public sealed record EvoPoint(
        double Q, double R,
        double Variation, double Retention,
        double SelectionPressure, double EvolutionRate,
        int GenerationsToAdapt, EvoRegime Regime,
        string Description);

    /// <summary>
    /// Evolution rate E = Variation × Retention × Selection.
    /// Variation ∝ R (randomness drives diversity).
    /// Retention ∝ Q·exp(-R) (structure preserves; chaos erases).
    /// Selection ∝ diversity of persistence times.
    /// </summary>
    public static List<EvoPoint> ScanEvoSpace()
    {
        var points = new List<EvoPoint>();
        double[] qVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] rVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in qVals)
        {
            foreach (double r in rVals)
            {
                // VARIATION: how much novelty per generation? ∝ R
                double variation = r * q; // needs structure to vary

                // RETENTION: what fraction of traits survive? ∝ Q, limited by R
                double retention = q * Math.Exp(-2.0 * r);

                // SELECTION: differential persistence among variants
                // Requires: multiple variants + different persistence times
                double selection = variation > 0.01 && retention > 0.01
                    ? variation * retention * 5.0
                    : 0;

                // EVOLUTION RATE: cumulative adaptive change per generation
                double evoRate = variation * retention * selection;

                // Generations to adapt: 1/evoRate (normalized)
                double gensToAdapt = evoRate > 0.001 ? 50.0 / evoRate : double.PositiveInfinity;

                var regime = (q, r) switch
                {
                    (0, _) => EvoRegime.Dead,
                    (_, 0) => EvoRegime.Static,
                    ( < 0.3, _) => EvoRegime.RandomWalk,
                    (_, > 0.7) => EvoRegime.RandomWalk,
                    ( > 0.5, _) when r > 0.1 && r < 0.7 && evoRate > 0.05 => EvoRegime.Optimizing,
                    ( > 0.5, _) when evoRate > 0.01 => EvoRegime.Adaptive,
                    _ => EvoRegime.RandomWalk
                };

                string desc = regime switch
                {
                    EvoRegime.Dead => "DEAD: No entities. Nothing to evolve.",
                    EvoRegime.Static => "STATIC: Perfect retention, zero variation. Frozen species.",
                    EvoRegime.RandomWalk => "RANDOM WALK: Variation exists but no cumulative selection.",
                    EvoRegime.Adaptive => "ADAPTIVE: Variation + selection → directional change.",
                    EvoRegime.Optimizing => "OPTIMIZING: Strong selection on diverse variants. Peak evolution.",
                    _ => "—"
                };

                points.Add(new EvoPoint(q, r, variation, retention,
                    selection, evoRate, (int)Math.Min(gensToAdapt, 99999),
                    regime, desc));
            }
        }

        return points;
    }

    public static string EvoPhaseDiagram(List<EvoPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EVOLUTION PHASE DIAGRAM — (Q, R) PLANE");
        sb.AppendLine();
        sb.AppendLine("  Evolution = Variation × Retention × Selection");
        sb.AppendLine("  OPTIMIZING regime: Q > 0.5, R ≈ 0.3–0.5");
        sb.AppendLine();
        sb.AppendLine("  Q\\R   R=0.0   R=0.1   R=0.3   R=0.5   R=0.7   R=0.9   R=1.0");
        sb.AppendLine("  " + new string('-', 70));

        double[] rVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] qVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in qVals)
        {
            sb.Append(string.Format(CultureInfo.InvariantCulture, "  {0,4:F1}  ", q));
            foreach (double r in rVals)
            {
                var p = points.First(pt => Math.Abs(pt.Q - q) < 0.01 && Math.Abs(pt.R - r) < 0.01);
                string symbol = p.Regime switch
                {
                    EvoRegime.Dead => " · ",
                    EvoRegime.Static => " □ ",
                    EvoRegime.RandomWalk => " ~ ",
                    EvoRegime.Adaptive => " → ",
                    EvoRegime.Optimizing => " ⇒ ",
                    _ => " ? "
                };
                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.AppendLine("  · = DEAD   □ = STATIC   ~ = RANDOM   → = ADAPTIVE   ⇒ = OPTIMIZING");
        sb.AppendLine($"  Our universe (Q≈1, R≈0.5): ⇒ OPTIMIZING. Evolution is MANDATORY.");
        return sb.ToString();
    }

    public static string TheDarwinianTriad()
    {
        return @"
THE DARWINIAN TRIAD — ALL FROM Q + RANDOMNESS

Evolution requires three components. ALL THREE emerge inevitably
from the TQM primitives:

  ┌──────────────────────────────────────────────────────────────┐
  │  VARIATION  ←  RANDOMNESS + Q                                │
  │                                                               │
  │  Actualization produces NOVEL outcomes among the              │
  │  distinguishable states provided by Q.                        │
  │  Each generation: new actualizations → new configurations.   │
  │  Without Randomness: no variation. Without Q: nothing varies. │
  ├──────────────────────────────────────────────────────────────┤
  │  RETENTION  ←  Q (IDENTITY)                                   │
  │                                                               │
  │  Topological invariants guarantee PERSISTENCE of identity.    │
  │  Entities persist across actualizations → memory.             │
  │  Without Q: nothing persists. Without retention: no heredity. │
  ├──────────────────────────────────────────────────────────────┤
  │  SELECTION  ←  DIFFERENTIAL PERSISTENCE                       │
  │                                                               │
  │  Different configurations have DIFFERENT persistence times.   │
  │  Those that last longer → more actualizations → more copies.  │
  │  Selection is NOT an added principle — it's the STATISTICAL   │
  │  CONSEQUENCE of variation + differential retention.           │
  └──────────────────────────────────────────────────────────────┘

  EVOLUTION = VARIATION × RETENTION × SELECTION

  All three = f(Q, Randomness).
  Evolution is DERIVED, not postulated.
";
    }

    public static string ThePrinciple()
    {
        return @"
THE EVOLUTION EMERGENCE PRINCIPLE

═══════════════════════════════════════════════════════════════
  THE EVOLUTION EMERGENCE PRINCIPLE
═══════════════════════════════════════════════════════════════

  Evolution emerges whenever:
    (1) Entities exist and persist (Q > 0)
    (2) Variation occurs (Randomness > 0)
    (3) Persistence varies across variants (inevitable for Q > 0)

  E = V(Q,R) · R(Q,R) · S(Q,R)

  V = Variation     ∝ R · Q (novelty × states)
  R = Retention     ∝ Q · e^(-R) (persistence limited by chaos)
  S = Selection     ∝ V · R (differential persistence)

  OPTIMAL EVOLUTION at: Q ≫ 0.5, R ≈ 0.3–0.5
  Our universe: Q ≈ 1, R ≈ 0.5 → PEAK EVOLUTION.

  THREE REGIMES WHERE EVOLUTION FAILS:

    R = 0: STATIC. No variation. Nothing changes.
    R ≫ 0.7: RANDOM WALK. No cumulative adaptation.
    Q ≈ 0: DEAD. Nothing to evolve.

  EVOLUTION IS NOT A CONTINGENT FEATURE OF OUR UNIVERSE.
  EVOLUTION IS THE INEVITABLE CONSEQUENCE OF Q + RANDOMNESS.

  Any universe with distinguishable entities and genuine novelty
  WILL evolve. It cannot avoid it.

═══════════════════════════════════════════════════════════════
";
    }

    public static string TheTripleChain()
    {
        return @"
THE TRIPLE CHAIN — COMPLEXITY, INFORMATION, EVOLUTION

ResearchXF has now derived three interconnected principles:

  XF001: COMPLEXITY EMERGENCE
    C = States × Persistence × Novelty
    Maximum at Q ≈ 1, R ≈ 0.5.

  XF002: INFORMATION GENERATION
    dI/dt = Creation − Decay
    Growing at Q > 0.5, R ≈ 0.3–0.7.

  XF003: EVOLUTION EMERGENCE
    E = Variation × Retention × Selection
    Optimizing at Q > 0.5, R ≈ 0.3–0.5.

  ALL THREE share the SAME optimum: Q ≈ 1, R ≈ 0.3–0.5.
  ALL THREE derive from the SAME primitives.
  ALL THREE are INEVITABLE — not accidents, not tuning.

  THE TRIPLE EMERGENCE:
    Q + Randomness
        → Complexity
        → Information Growth
        → Evolution

  This is the CORE of ResearchXF: COMPLEXITY PHYSICS.
";
    }
}
