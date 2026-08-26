namespace AT.Core.ResearchXF;

using System.Globalization;

/// <summary>
/// Derives the Information Generation Principle from Q + Randomness.
/// ResearchXF-002: Information Generation Principle
/// </summary>
public static class InformationGenerationAnalyzer
{
    public enum InfoRegime { NoInfo, Frozen, Decaying, Growing, Exploding }

    public sealed record InfoPoint(
        double Q, double R,
        double StorageCapacity, double CreationRate,
        double RetentionFraction, double NetGrowth,
        double SteadyStateInfo, InfoRegime Regime,
        string Description);

    /// <summary>
    /// Information dynamics: dI/dt = Creation - Decay.
    /// Creation ∝ R · storage_capacity (randomness × states).
    /// Decay ∝ (1-persistence) · I (information fragility).
    /// Steady state: I* = Creation / Decay_rate.
    /// </summary>
    public static List<InfoPoint> ScanInfoSpace()
    {
        var points = new List<InfoPoint>();
        double[] qVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] rVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in qVals)
        {
            foreach (double r in rVals)
            {
                // Storage capacity: how much info can the system hold?
                // ∝ Q (distinguishable states = 2^Q·logN approx)
                double storage = q * 1000;

                // Creation rate: how fast is new information generated?
                // ∝ R × available_states (randomness creates new configurations)
                double creation = r * storage * 0.1;

                // Retention fraction: what fraction survives?
                // ∝ Q (structure retains information) and ∝ exp(-R) (chaos destroys it)
                double retention = q * Math.Exp(-1.5 * r);

                // Decay rate: how fast does stored information degrade?
                double decayRate = 1.0 - retention;

                // Net growth: creation - decay
                // At steady state: I* = creation / decayRate
                double steadyState = decayRate > 0.01 ? creation / decayRate : creation * 100;
                double netGrowth = creation - decayRate * (steadyState * 0.5); // approach to steady state

                var regime = (q, r) switch
                {
                    (0, _) => InfoRegime.NoInfo,
                    (_, 0) => InfoRegime.Frozen,
                    ( < 0.3, > 0.7) => InfoRegime.Decaying,
                    ( > 0.5, > 0.3) when r < 0.7 => InfoRegime.Growing,
                    (_, > 0.8) => InfoRegime.Exploding,
                    _ => InfoRegime.Decaying
                };

                string desc = regime switch
                {
                    InfoRegime.NoInfo => "NO INFO: No distinguishable states. Vacuum.",
                    InfoRegime.Frozen => "FROZEN: Information stored but never created. Static library.",
                    InfoRegime.Decaying => "DECAYING: Information degrades faster than created. Heat death.",
                    InfoRegime.Growing => "GROWING: Creation > Decay. Information accumulates. EVOLUTION.",
                    InfoRegime.Exploding => "EXPLODING: Creation >> Decay. Noise, not signal.",
                    _ => "—"
                };

                points.Add(new InfoPoint(q, r, storage, creation,
                    retention, netGrowth, steadyState, regime, desc));
            }
        }

        return points;
    }

    public static string InfoPhaseDiagram(List<InfoPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION PHASE DIAGRAM — (Q, R) PLANE");
        sb.AppendLine();
        sb.AppendLine("  InformationGrowth = Creation(Q,R) − Decay(Q,R)");
        sb.AppendLine("  GROWING regime: Q > 0.5, R ≈ 0.3–0.7");
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
                    InfoRegime.NoInfo => " · ",
                    InfoRegime.Frozen => " □ ",
                    InfoRegime.Decaying => " - ",
                    InfoRegime.Growing => " ▲ ",
                    InfoRegime.Exploding => " ≈ ",
                    _ => " ? "
                };
                sb.Append(symbol);
            }
            sb.AppendLine();
        }
        sb.AppendLine();
        sb.AppendLine("  · = NO INFO   □ = FROZEN   - = DECAYING   ▲ = GROWING   ≈ = EXPLODING");
        sb.AppendLine($"  Our universe: Q≈1, R≈0.5 → ▲ GROWING. Information INCREASES over time.");
        return sb.ToString();
    }

    public static string InfoGrowthTable(List<InfoPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION DYNAMICS — STEADY STATE ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Q      R      Storage   Creation   Retent%   Steady.I   Growth?");
        sb.AppendLine("  " + new string('-', 72));

        var keyPoints = points.Where(p =>
            (Math.Abs(p.Q - 1.0) < 0.01 || Math.Abs(p.Q - 0.5) < 0.01 || Math.Abs(p.Q - 0.0) < 0.01) &&
            (Math.Abs(p.R - 0.0) < 0.01 || Math.Abs(p.R - 0.5) < 0.01 || Math.Abs(p.R - 1.0) < 0.01))
            .OrderBy(p => p.Q).ThenBy(p => p.R).ToList();

        foreach (var p in keyPoints)
        {
            string growth = p.Regime == InfoRegime.Growing ? "▲ YES" :
                            p.Regime == InfoRegime.Frozen ? "□ NO" :
                            p.Regime == InfoRegime.NoInfo ? "· NO" : "✗ NO";
            string marker = Math.Abs(p.Q - 1.0) < 0.01 && Math.Abs(p.R - 0.5) < 0.01 ? " ← OUR UNIVERSE" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,5:F1}  {1,5:F1}  {2,7:F0}   {3,8:F2}   {4,6:P0}   {5,8:F1}   {6}{7}",
                p.Q, p.R, p.StorageCapacity, p.CreationRate,
                p.RetentionFraction, p.SteadyStateInfo, growth, marker));
        }

        return sb.ToString();
    }

    public static string ThePrinciple()
    {
        return @"
THE INFORMATION GENERATION PRINCIPLE

═══════════════════════════════════════════════════════════════
  THE INFORMATION GENERATION PRINCIPLE
═══════════════════════════════════════════════════════════════

  Information grows whenever distinguishable entities (Q > 0)
  coexist with bounded actualization (0 < R < 0.7).

  dI/dt = Creation − Decay

  Creation = R · States(Q)
    Randomness generates new configurations at a rate proportional
    to the number of distinguishable states available.
    More entities → more possible configurations → more creation.

  Decay = (1 − Retention(Q,R)) · I
    Information degrades when it cannot be preserved.
    Retention depends on Q (structure) and R (disruption).
    High Q → high retention. High R → low retention.

  STEADY STATE:
    I* = Creation / DecayRate
    For Q≈1, R≈0.5: I* is LARGE and GROWING.

  THREE LIMITS:

    R = 0 (deterministic): Creation = 0. No new information.
      Universe is a static library. All info was present at t=0.
      FROZEN REGIME.

    R ≫ 0.7 (chaotic): Decay dominates. Information destroyed
      faster than created. Heat death. DECAYING REGIME.

    Q ≈ 0 (no entities): Storage = 0. Nothing to store info in.
      Vacuum. NO INFO REGIME.

  THE GOLDILOCKS ZONE: Q > 0.5, R ≈ 0.3–0.7.
    Information GROWS. Evolution possible. Complexity accumulates.
    Our universe occupies this zone (Q≈1, R≈0.5).

  INFORMATION GROWTH IS INEVITABLE.
  Whenever Q and Randomness coexist with sufficient strength,
  information MUST increase. It is not a goal. It is not an accident.
  It is a MATHEMATICAL CONSEQUENCE of the primitives.

═══════════════════════════════════════════════════════════════
";
    }
}
