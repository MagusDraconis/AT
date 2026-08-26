using System.Globalization;
using AT.Core.ResearchXB.Models;

namespace AT.Core.ResearchXB;

/// <summary>
/// Derives the origin of universal per-step volatility σ₀².
/// ResearchXB-004: Origin of Universal Per-Step Volatility
/// </summary>
public static class UniversalVolatilityAnalyzer
{
    public static string AnalyzeAll()
    {
        var sb = new System.Text.StringBuilder();

        // 1. Born rule origin
        var (sigmaBorn, explanation) = PerStepVolatilityModel.ComputeFromBornRule();
        sb.AppendLine("BORN RULE ORIGIN OF σ₀²");
        sb.AppendLine();
        sb.AppendLine($"  Computed σ₀² ≈ {sigmaBorn:F4} (observed: ~0.09)");
        sb.AppendLine();
        sb.AppendLine(explanation);
        sb.AppendLine();

        // 2. M² scan
        sb.AppendLine("M² → σ₀² SCAN");
        sb.AppendLine();
        var (m2Vals, sigmaVals, insight) = PerStepVolatilityModel.ScanM2VsVolatility();
        sb.AppendLine("  M²       σ₀²");
        sb.AppendLine("  " + new string('-', 20));
        for (int i = 0; i < m2Vals.Length; i++)
        {
            string marker = Math.Abs(m2Vals[i] - 5.0) < 0.5 ? " ← OUR UNIVERSE" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,5:F1}   {1,7:F4}{2}", m2Vals[i], sigmaVals[i], marker));
        }
        sb.AppendLine();
        sb.AppendLine(insight);
        sb.AppendLine();

        // 3. The bridge
        sb.AppendLine(PerStepVolatilityModel.TheIdentityAbundanceBridge());

        return sb.ToString();
    }

    public static string TheFinalSynthesis()
    {
        return @"
THE FINAL SYNTHESIS — IDENTITY + ABUNDANCE UNIFIED

After ResearchXB-004, AT is a UNIFIED theory of both identity and abundance:

═══════════════════════════════════════════════════════════════
                    AT — COMPLETE FRAMEWORK
═══════════════════════════════════════════════════════════════

PRIMITIVES:
  Q — individuation (ontology)
  Randomness — actualization (becoming)

CONTINUOUS PARAMETER:
  M² — nonlinearity regime (dynamics)

═══════════════════════════════════════════════════════════════
  RESEARCHX: IDENTITY PHYSICS
  'What exists?' — Topology determines identity.
═══════════════════════════════════════════════════════════════

  M² → Defect potential → Particle species + Gauge symmetries
  M² → Excitation spectrum → 3 generations + Mass hierarchy
  M² → Wavefunction overlap → CKM/PMNS mixing + Neutrino sector
  M² → Moduli space topology → U(1) + Gauge groups

  Derived: ~93% of identity questions (WHAT exists)

═══════════════════════════════════════════════════════════════
  RESEARCHXB: ABUNDANCE PHYSICS
  'How much?' — History determines abundance.
═══════════════════════════════════════════════════════════════

  M² → σ₀² (per-step volatility) → Cascade variance
  N = log(T_init/T_freeze) → Cascade depth
  σ² = N·σ₀² → Distribution width
  log(X) ~ N(μ,σ²) → All abundance quantities are log-normal

  Derived: Distribution family + parameter scaling
  Contingent: Exact values (random draws from distributions)

═══════════════════════════════════════════════════════════════

ONE PARAMETER (M²) DETERMINES BOTH:
  • WHAT exists (identity — through defect topology)
  • HOW MUCH varies (abundance — through actualization volatility)

This is the unification of ResearchX and ResearchXB.
";
    }
}
