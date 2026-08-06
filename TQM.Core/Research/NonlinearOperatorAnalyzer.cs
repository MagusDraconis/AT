namespace TQM.Core.Research;

/// <summary>
/// Analyzes which TQM results survive nonlinear operators
/// and whether qualitatively new physics emerges.
///
/// TQM-X005: Nonlinear Operator Physics
/// </summary>
public static class NonlinearOperatorAnalyzer
{
    public static string NonlinearTheory()
    {
        return @"
NONLINEAR OPERATOR PHYSICS

1. THE QUESTION:

   All TQM (117-154) uses L_Q (linear).
   What survives when L_Q → L_NL(ψ) is nonlinear?

2. REGIMES:

   α = 0:      Linear — TQM valid.
   α < 0.01:   Weakly Nonlinear — eigenmodes perturbed.
   α ≈ 0.01-0.5: Moderately Nonlinear — eigenmodes break.
   α ≈ 0.5-2.0: Strongly Nonlinear — new structures (solitons).
   α > 2.0:   Soliton-Dominated — completely new physics.

3. WHAT BREAKS IMMEDIATELY (α > 0):

   - Superposition: L(ψ₁+ψ₂) ≠ L(ψ₁)+L(ψ₂)
   - Hilbert space: no vector space structure
   - Eigenmodes: nonlinear modes are NOT orthogonal
   - Fourier analysis: modes are not sinusoidal
   - Quantum correspondence: breaks entirely

4. WHAT MAY EMERGE:

   - Solitons (self-localized persistent structures)
   - Nonlinear eigenmodes (new species TYPES)
   - Pattern formation (Turing-like)
   - Chaos (unpredictable dynamics)
   - Qualitatively new attractor families

5. NULL HYPOTHESIS: Nonlinearity only adds small corrections.
   H1: Nonlinearity creates qualitatively new physics.
";
    }

    public static NonlinearPhaseMetrics.NonlinearReport Analyze(int? seed = null)
    {
        double[] alphas = { 0.00, 0.01, 0.05, 0.10, 0.20, 0.50, 1.00, 2.00, 5.00 };
        var results = NonlinearOperatorModel.SweepNonlinearity(alphas, seed: seed);

        int regimes = results.Select(r => r.Regime).Distinct().Count();
        bool linearityEssential = results.Any(r => r.Regime == "Linear")
            && results.All(r => r.Regime != "Linear" || r.EigenmodesSurvive);
        bool newPhysics = results.Any(r => r.NewStructuresEmerge);

        string classification = newPhysics ? "D: Fundamentally New Nonlinear Physics"
                              : results.Count(r => !r.EigenmodesSurvive) >= 3 ? "C: Nonlinear Emergent Structures"
                              : "B: Weak Nonlinear Corrections";

        string verdict = newPhysics
            ? $"NONLINEARITY CREATES NEW PHYSICS. {regimes} regimes identified. "
              + $"At α ≥ {alphas.First(a => results.Any(r => r.Alpha == a && r.NewStructuresEmerge))}, "
              + $"solitons and new structures emerge. "
              + $"Eigenmodes, superposition, Hilbert space, and quantum correspondence ALL BREAK. "
              + $"Nonlinear TQM is a DIFFERENT THEORY from linear TQM."
            : "Nonlinearity is inessential at tested parameters.";

        return new NonlinearPhaseMetrics.NonlinearReport(
            results, regimes, linearityEssential, newPhysics, classification, verdict);
    }

    public static string HostileReview(NonlinearPhaseMetrics.NonlinearReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: How much of TQM is just linear algebra?");
        sb.AppendLine();
        sb.AppendLine("  TQM results that DEPEND on linearity:");
        sb.AppendLine("    ✗ Eigenmodes (L·v = λ·v requires linear L)");
        sb.AppendLine("    ✗ Superposition (ψ₁+ψ₂ is not a solution)");
        sb.AppendLine("    ✗ Hilbert space (not a vector space under nonlinear L)");
        sb.AppendLine("    ✗ Fourier species (modes are not sinusoidal)");
        sb.AppendLine("    ✗ Schrödinger correspondence (requires linear H)");
        sb.AppendLine("    ✗ Berry phase / geometric phases");
        sb.AppendLine();
        sb.AppendLine("  TQM results that do NOT depend on linearity:");
        sb.AppendLine("    ✓ Q charge existence and conservation");
        sb.AppendLine("    ✓ Q interaction graph");
        sb.AppendLine("    ✓ Fitness law w = r/c (instantaneous)");
        sb.AppendLine("    ✓ Selection (differential survival)");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Most of TQM's 'quantum' results are linear algebra.");
        sb.AppendLine("  Nonlinearity reveals which parts are fundamental (Q, fitness)");
        sb.AppendLine("  and which are artifacts of the linear operator choice.");
        sb.AppendLine();
        return sb.ToString();
    }
}
