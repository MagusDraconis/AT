namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether information structures within the Θ field
/// interact — merging, canceling, reinforcing, or transforming
/// each other — forming an autonomous information dynamics layer.
///
/// TQM-132: Information Dynamics in the Θ Field
/// </summary>
public static class ThetaInformationDynamicsAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // INFORMATION DYNAMICS THEORY
    // ══════════════════════════════════════════════════════════════════

    public static string DynamicsTheory()
    {
        return @"
INFORMATION DYNAMICS IN THE Θ FIELD

1. THE QUESTION:

   TQM-131: Information (Θ) and matter (Q) are DECOUPLED.
   But within Θ itself: can information interact with information?

   Can two stored memory patterns A and B:
   — Merge into composite AB?
   — Cancel if anti-correlated?
   — Reinforce if overlapping?
   — Remain independent if orthogonal?
   — Transform into new pattern C?

2. INTERACTION MECHANISM:

   Θ satisfies damped wave equation: ∂²Θ/∂t² = v²∇²Θ − γ∂Θ/∂t.

   Two patterns A(x) and B(x) co-evolve:
   Θ(x,t) = A(x)·exp(−γt/2)·cos(ω_A·t) + B(x)·exp(−γt/2)·cos(ω_B·t)

   INTERACTION: if ω_A ≈ ω_B and patterns overlap spatially,
   the amplitudes ADD (constructive/destructive interference).

   The interaction is MEDIATED BY THE WAVE DYNAMICS — information
   patterns interact through the same Θ field that carries them.

3. INTERACTION TYPES:

   REINFORCE: overlap > 0.8 → amplitudes add constructively.
   Patterns amplify each other. Information is PRESERVED and
   strengthened.

   CANCEL: overlap < −0.5 → anti-correlated patterns partially
   cancel. Information is DESTROYED. This is information
   ERASURE through destructive interference.

   MERGE: orthogonal patterns (overlap ≈ 0) at high density →
   form composite state. New information is CREATED
   (emergence of composite pattern).

   INDEPENDENT: low overlap + low density → patterns evolve
   separately. No interaction.

4. ENTROPY DYNAMICS:

   Information entropy H(Θ) = −Σ p_i·log(p_i) over pattern bins.
   
   Reinforcement: ΔH < 0 (order increases).
   Cancellation: ΔH > 0 (disorder increases).
   Independence: ΔH ≈ 0.

   Information production rate:
   dI/dt = −γ·I + ρ_Q·⟨|interaction|⟩
   
   Damping destroys information (first term).
   Interactions can create or destroy information (second term).
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaInformationPattern.InfoDynamicsReport Analyze(
        double[] densities = null)
    {
        densities ??= new[] { 0.1, 0.3, 0.5, 0.7, 0.9 };
        var patterns = InformationInteractionProfile.GenerateTestPatterns();
        var interactions = new List<ThetaInformationPattern.PatternInteraction>();
        var entropyProfiles = new List<ThetaInformationPattern.InfoEntropyProfile>();

        // All pairwise interactions.
        var pairs = new[] { (0, 1), (0, 2), (0, 3), (1, 2), (1, 3), (2, 3) };

        foreach (double density in densities)
        {
            foreach (var (i, j) in pairs)
            {
                var interaction = InformationInteractionProfile.SimulateInteraction(
                    patterns[i], patterns[j], density);
                interactions.Add(interaction);
            }

            // Entropy profiles.
            foreach (var p in patterns)
            {
                entropyProfiles.Add(InformationInteractionProfile.ComputeEntropy(
                    p.Pattern, $"{p.Name} (ρ={density:F1})", density));
            }
        }

        bool interactionsFound = interactions.Any(x =>
            x.InteractionType != "Independent");
        bool mergersFound = interactions.Any(x => x.InteractionType == "Merge");
        bool cancelsFound = interactions.Any(x => x.InteractionType == "Cancel");
        bool compositesFound = interactions.Any(x =>
            x.InteractionType == "Merge" || x.InteractionType == "Reinforce");
        bool selfOrg = entropyProfiles.Any(e => e.InformationProductionRate > 0);

        string classification = interactionsFound && compositesFound
            ? "D: Autonomous Information Layer"
            : interactionsFound ? "C: Emergent Information Dynamics"
            : "A: Independent Information";

        string verdict = interactionsFound
            ? $"INFORMATION-INFORMATION INTERACTIONS DETECTED. " +
              $"Patterns in Θ {(mergersFound ? "MERGE" : "interact")}, " +
              $"{(cancelsFound ? "CANCEL, " : "")}" +
              $"and {(compositesFound ? "form COMPOSITE states" : "interact weakly")}. " +
              $"Interaction types: {string.Join(", ", interactions.Select(x => x.InteractionType).Distinct())}. " +
              "This establishes Θ as an AUTONOMOUS INFORMATION DYNAMICS LAYER — " +
              "information within Θ has its own physics (interaction rules) " +
              "independent of the matter layer (Q). " +
              "Information is not passive: it can merge, cancel, reinforce, " +
              "and transform — a genuine dynamical system of information."
            : "No significant information-information interactions detected. " +
              "Patterns in Θ evolve independently.";

        return new ThetaInformationPattern.InfoDynamicsReport(
            patterns.ToList(), interactions, entropyProfiles,
            interactionsFound, mergersFound, cancelsFound,
            compositesFound, selfOrg,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(
        ThetaInformationPattern.InfoDynamicsReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Can two information structures interact?");
        sb.AppendLine(report.InteractionsFound
            ? $"  YES — {report.Interactions.Count(i => i.InteractionType != "Independent")} " +
              "interactions detected. Information patterns merge, cancel, and reinforce " +
              "through Θ-field wave dynamics."
            : "  NO — information patterns evolve independently.");
        sb.AppendLine();

        sb.AppendLine("Q2: Do memories merge?");
        sb.AppendLine(report.MergersFound
            ? "  YES — orthogonal patterns at high density merge into composite states. " +
              "New information is CREATED through pattern combination."
            : "  NO — patterns remain distinct even when co-located.");
        sb.AppendLine();

        sb.AppendLine("Q3: Can memories cancel each other?");
        sb.AppendLine(report.CancellationsFound
            ? "  YES — anti-correlated patterns partially cancel through " +
              "destructive interference. This is information ERASURE."
            : "  NO — anti-correlation does not cause cancellation at tested parameters.");
        sb.AppendLine();

        sb.AppendLine("Q4: Are composite memory states possible?");
        sb.AppendLine(report.CompositeStatesFound
            ? "  YES — multiple patterns can coexist in composite states " +
              "through superposition. The Θ field supports multi-bit memories."
            : "  NOT OBSERVED — composite states require specific density/interaction.");
        sb.AppendLine();

        sb.AppendLine("Q5: Can information be transformed into new information?");
        sb.AppendLine(report.Interactions.Any(i => i.InformationTransformed)
            ? "  YES — mergers and cancellations TRANSFORM information. " +
              "Output differs from input. Information is not static."
            : "  NO — information is preserved or decays, but not transformed.");
        sb.AppendLine();

        sb.AppendLine("Q6: Does Θ possess information attractors?");
        sb.AppendLine("  Potentially. Reinforcement (overlap→1) and independence " +
                      "(overlap→0) may be attractors of the information dynamics. " +
                      "Composite states may form metastable attractors.");
        sb.AppendLine();

        sb.AppendLine("Q7: Can information self-organize?");
        sb.AppendLine(report.SelfOrganizationFound
            ? "  YES — positive information production rate detected. " +
              "Interactions can CREATE order from pattern coexistence."
            : "  NO — information always decays (dI/dt < 0).");
        sb.AppendLine();

        sb.AppendLine("Q8: Does Θ support a genuine information dynamics layer?");
        sb.AppendLine(report.InteractionsFound
            ? "  YES. The Θ field is an AUTONOMOUS INFORMATION LAYER with its own " +
              "interaction rules (merge, cancel, reinforce). Information in Θ " +
              "has dynamics INDEPENDENT of the matter layer — a genuine " +
              "information physics built on topological charge dynamics."
            : "  NO — information is static. No autonomous dynamics.");
        sb.AppendLine();

        return sb.ToString();
    }
}
