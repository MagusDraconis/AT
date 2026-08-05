namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether information stored in the Θ field can influence
/// future topological charge creation — information-to-matter coupling.
///
/// TQM-131: Information Back-Reaction on Proto-Matter Genesis
/// </summary>
public static class ThetaBackReactionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // BACK-REACTION THEORY
    // ══════════════════════════════════════════════════════════════════

    public static string BackReactionTheory()
    {
        return @"
INFORMATION BACK-REACTION ON PROTO-MATTER GENESIS

1. THE QUESTION:

   TQM-118: Q created when c₀·M > D_R/w².
   TQM-130: Θ stores information in metastable states.

   Can stored Θ information modify Q creation?

   If YES: information → matter coupling (feedback loop).
   If NO: information is epiphenomenal (passive record).

2. PROPOSED MECHANISM:

   Θ(x) represents local phase coherence. Regions of high |Θ|
   have oscillators already partially aligned → lower nucleation
   barrier → enhanced Q creation probability.

   Modified nucleation condition:
   c₀·M·(1 + β·|Θ|²) > D_R/w²

   where β is the information-matter coupling constant.
   β > 0: memory ENHANCES nucleation (self-amplification).
   β < 0: memory SUPPRESSES nucleation (saturation).
   β = 0: no back-reaction.

3. MEMORY SURVIVAL:

   When new charges nucleate, they inject energy into Θ.
   This may DISRUPT or REINFORCE stored memory patterns.

   Survival probability: P_survive = exp(−ν·δ/γ)
   where ν = nucleation rate, δ = disruption per event,
   γ = damping (memory decay rate).

   At high density: memory more robust (collective protection).
   At low density: memory fragile (easily disrupted).

4. SELF-TEMPLATING:

   If memory enhances nucleation at memory peaks, new charges
   form WHERE the memory is strongest → the memory pattern
   REPRODUCES ITSELF. This is primitive self-templating —
   information guiding its own physical instantiation.

5. INFORMATION INHERITANCE:

   If memory survives through a generation of Q creation,
   and the new Q distribution reflects the old memory pattern,
   then INFORMATION IS INHERITED across generations of
   proto-matter. This is the precursor to replication.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis.
    // ══════════════════════════════════════════════════════════════════

    public static GenesisMemoryProfile.InformationGenesisReport Analyze(
        double[] densities = null, int nMemoryPatterns = 3,
        double biasStrength = 0.3)
    {
        densities ??= new[] { 0.1, 0.3, 0.5, 0.7, 0.9 };
        var runs = new List<GenesisMemoryProfile.MemoryGenesisRun>();
        var biases = new List<GenesisMemoryProfile.NucleationBias>();
        var rng = new Random(42);

        // Memory patterns: phase, standing wave, anti-phase.
        var memTypes = new[] { "PhasePattern", "StandingWave", "AntiPhase", "None" };
        int nBins = 20;

        foreach (double density in densities)
        {
            foreach (string memType in memTypes)
            {
                // Generate memory pattern.
                var memPattern = new double[nBins];
                for (int i = 0; i < nBins; i++)
                {
                    double x = (i + 0.5) / nBins;
                    memPattern[i] = memType switch
                    {
                        "PhasePattern" => Math.Sin(2 * Math.PI * x),
                        "StandingWave" => Math.Sin(4 * Math.PI * x) * Math.Exp(-x),
                        "AntiPhase" => x < 0.5 ? 1.0 : -1.0,
                        _ => 0
                    };
                }

                double effectiveBias = memType == "None" ? 0 : biasStrength * density;
                var (sitesMem, sitesCtrl) = ChargeCreationBias.SimulateNucleationWithMemory(
                    memPattern, 50, effectiveBias);

                var bias = ChargeCreationBias.EstimateBias(
                    memPattern, sitesMem, sitesCtrl);
                biases.Add(bias);

                int qBefore = (int)(density * 10);
                double nucRate = density * 0.5;
                int qAfter = qBefore + (int)(nucRate * (1.0 + bias.BiasFactor * 0.3));
                qAfter = Math.Min(qAfter, 100);

                double overlapBefore = density > 0.3 ? 0.8 : 0.4;
                double survProb = ChargeCreationBias.MemorySurvivalProbability(
                    density, nucRate);
                double overlapAfter = overlapBefore * survProb;

                runs.Add(new GenesisMemoryProfile.MemoryGenesisRun(
                    density, 5.0, 0.10, 200, memType,
                    qBefore, qAfter, nucRate,
                    sitesMem,
                    overlapBefore, overlapAfter,
                    overlapAfter > 0.3,
                    bias.SignificantBias,
                    bias.SignificantBias
                        ? $"Nucleation {(bias.BiasDirection == "Enhance" ? "ENHANCED" : "SUPPRESSED")} " +
                          $"by factor {bias.BiasFactor:F2} (bias). " +
                          $"Spatial correlation: {bias.SpatialCorrelation:F2}."
                        : "No significant bias detected."));
            }
        }

        bool backReactionFound = biases.Any(b => b.SignificantBias);
        bool memorySurvives = runs.Any(r => r.MemorySurvived);
        double maxBias = biases.Max(b => b.BiasFactor);
        double mi = biases.Max(b => b.MutualInfo);

        string modifiedCond = ChargeCreationBias.DeriveModifiedNucleationCondition(
            backReactionFound ? maxBias : 1.0);

        string classification = backReactionFound && memorySurvives
            ? "D: Information-Matter Feedback Theory"
            : backReactionFound ? "C: Information-Driven Genesis"
            : runs.Any(r => r.BiasDetected) ? "B: Weak Bias"
            : "A: No Back-Reaction";

        string verdict = backReactionFound
            ? $"INFORMATION-MATTER COUPLING DETECTED. Stored Θ memory " +
              $"{(maxBias > 1.1 ? "ENHANCES" : "SUPPRESSES")} subsequent charge " +
              $"nucleation with bias factor {maxBias:F2}. " +
              $"{(memorySurvives ? "Memory patterns SURVIVE re-nucleation" : "Memory is DISRUPTED by re-nucleation")}. " +
              $"Modified nucleation: c₀·M·(1+β·|Θ|²) > D_R/w² " +
              $"with β≈{maxBias - 1.0:F3}. " +
              "Information STORED in Θ feeds back onto the physical process " +
              "that creates Θ — a CLOSED INFORMATION-MATTER LOOP. " +
              "Proto-matter can self-template: memory guides its own reproduction."
            : "No significant back-reaction detected. Θ memory does not " +
              "influence future Q creation at tested parameters. " +
              "Information is a passive record, not an active agent.";

        return new GenesisMemoryProfile.InformationGenesisReport(
            runs, biases,
            backReactionFound, memorySurvives,
            maxBias, mi, modifiedCond,
            classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Research questions.
    // ══════════════════════════════════════════════════════════════════

    public static string ResearchQuestions(
        GenesisMemoryProfile.InformationGenesisReport report)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Q1: Does Θ memory bias charge creation?");
        sb.AppendLine(report.BackReactionFound
            ? $"  YES — bias factor {report.MaxBiasFactor:F2}. " +
              "Nucleation probability is MODULATED by stored Θ patterns."
            : "  NO — nucleation statistics are indistinguishable from no-memory controls.");
        sb.AppendLine();

        sb.AppendLine("Q2: Do nucleation sites correlate with stored information?");
        double maxCorr = report.Biases.Max(b => b.SpatialCorrelation);
        sb.AppendLine(Math.Abs(maxCorr) > 0.15
            ? $"  YES — spatial correlation r={maxCorr:F2} between memory peaks and nucleation sites. " +
              "New charges form preferentially where |Θ| is large."
            : "  NO — nucleation sites are spatially uncorrelated with memory patterns.");
        sb.AppendLine();

        sb.AppendLine("Q3: Can memory increase nucleation probability?");
        sb.AppendLine(report.Biases.Any(b => b.BiasDirection == "Enhance")
            ? "  YES — memory ENHANCES nucleation. Stored coherence reduces the " +
              "nucleation barrier → easier charge creation."
            : "  NOT OBSERVED — memory does not significantly enhance nucleation.");
        sb.AppendLine();

        sb.AppendLine("Q4: Can memory suppress nucleation probability?");
        sb.AppendLine(report.Biases.Any(b => b.BiasDirection == "Suppress")
            ? "  YES — memory SUPPRESSES nucleation. Saturated coherence may " +
              "inhibit further charge creation (exclusion effect)."
            : "  NOT OBSERVED — memory does not significantly suppress nucleation.");
        sb.AppendLine();

        sb.AppendLine("Q5: Does stored information survive through a generation of Q creation?");
        sb.AppendLine(report.MemorySurvivesRenucleation
            ? "  YES — memory patterns persist after new charges nucleate. " +
              "Information is ROBUST against re-nucleation disruption."
            : "  NO — new charge creation disrupts stored memory patterns.");
        sb.AppendLine();

        sb.AppendLine("Q6: Can a memory imprint reproduce itself?");
        sb.AppendLine(report.BackReactionFound && report.MemorySurvivesRenucleation
            ? "  POTENTIALLY. If memory enhances nucleation at memory peaks, and " +
              "memory survives re-nucleation, the pattern can SELF-TEMPLATE. " +
              "This is primitive reproduction — information guiding its own physical copy."
            : "  NO — either no bias or memory doesn't survive the process.");
        sb.AppendLine();

        sb.AppendLine("Q7: Is there information inheritance?");
        sb.AppendLine(report.MutualInformation > 0.1
            ? $"  YES — I(memory; future_Q) ≈ {report.MutualInformation:F2} bits. " +
              "Information about past Θ states is MUTUALLY INFORMATIVE about future Q states. " +
              "This is INFORMATION INHERITANCE across proto-matter generations."
            : "  NO — no detectable information transfer between generations.");
        sb.AppendLine();

        sb.AppendLine("Q8: Can proto-matter exhibit primitive self-templating?");
        sb.AppendLine(report.BackReactionFound && report.MemorySurvivesRenucleation
            ? "  YES. The combination of memory persistence and nucleation bias " +
              "creates a SELF-TEMPLATING LOOP: memory → enhanced nucleation at " +
              "memory sites → new charges reinforce memory → pattern persists. " +
              "This is primitive self-reproduction without genetics."
            : "  NO — missing either memory persistence or nucleation bias.");
        sb.AppendLine();

        return sb.ToString();
    }
}
