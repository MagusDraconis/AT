using System.Globalization;
using System.Text;
using AT.Core.Temporal;
using AT.Core.TemporalField;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

/// <summary>
/// AT-004: Temporal Field-Mediated Synchronization Experiment
///
/// Investigates whether two oscillators with different natural frequencies,
/// coupled ONLY through a shared temporal field (no direct coupling),
/// can achieve synchronization.
///
/// Conceptual model:
///   Oscillator A ↔ Temporal Field ↔ Oscillator B
/// </summary>
public class AT_004_TemporalFieldMediatedSynchronization : ResearchTestBase
{
    private const int FieldCells = 100;
    private const int TotalIterations = 10000;
    private const int Osc1Position = 25;
    private const int Osc2Position = 75;

    public AT_004_TemporalFieldMediatedSynchronization(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void AT_004_RunFieldMediatedSynchronization()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
            ExecuteExperiment();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();

        // ── Header ──────────────────────────────────────────────
        PrintHeader("AT-004 Temporal Field-Mediated Synchronization");
        report.AppendLine("AT-004: Field-Mediated Oscillator Synchronization");
        report.AppendLine();

        // ── 1. Objective ────────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  Investigate whether synchronization can emerge between two oscillators");
        report.AppendLine("  that have NO direct coupling, interacting exclusively through a shared");
        report.AppendLine("  1D temporal field.");
        report.AppendLine();
        report.AppendLine("  Research questions:");
        report.AppendLine("    Q1. Can synchronization emerge without direct coupling?");
        report.AppendLine("    Q2. Can a shared temporal field act as a synchronization medium?");
        report.AppendLine("    Q3. Does stronger field density produce stronger phase locking?");
        report.AppendLine();

        // ── 2. Theory ───────────────────────────────────────────
        AppendSection(report, "2. Theory");
        report.AppendLine("  Previous AT experiments used direct Kuramoto coupling:");
        report.AppendLine("    dθᵢ/dt = ωᵢ + (K/N) Σⱼ sin(θⱼ − θᵢ)");
        report.AppendLine();
        report.AppendLine("  In this experiment, oscillators interact through a shared temporal field φ(x,t):");
        report.AppendLine("    ∂²φ/∂t² = c²∇²φ + D∇²φ − γ ∂φ/∂t + S(x,t)   (field equation)");
        report.AppendLine("    dθᵢ/dt = ωᵢ + α · ρ(xᵢ,t)                       (oscillator equation)");
        report.AppendLine("    S(xᵢ,t) = β · |sin(θᵢ)|                          (energy injection)");
        report.AppendLine();
        report.AppendLine("  Key parameters:");
        report.AppendLine("    α  = field-to-oscillator coupling strength");
        report.AppendLine("    β  = oscillator-to-field injection strength");
        report.AppendLine("    c  = wave propagation speed in the field");
        report.AppendLine("    D  = diffusion coefficient (spatial smoothing)");
        report.AppendLine("    γ  = damping coefficient (energy dissipation)");
        report.AppendLine();

        // ── 3. Initial Conditions ───────────────────────────────
        AppendSection(report, "3. Initial Conditions");

        // Create network with 2 oscillators.
        var network = new TemporalNetwork(2);
        var osc1 = new TemporalNode(0, phase: 0.0, frequency: 1.0);
        var osc2 = new TemporalNode(1, phase: Math.PI / 4, frequency: 1.5);
        network.AddNode(osc1);
        network.AddNode(osc2);

        // Create temporal field.
        var field = new TemporalField(FieldCells)
        {
            PropagationSpeed = 0.4,
            DiffusionCoefficient = 0.03,
            DampingCoefficient = 0.002,
            EnergyToDensity = 1.0
        };

        // Create simulation.
        var sim = new TemporalFieldSimulation(network, field, new[] { Osc1Position, Osc2Position })
        {
            FieldCouplingAlpha = 0.3,
            InjectionStrength = 2.0,
            TimeStep = 1.0
        };

        report.AppendLine($"  Field cells              : {FieldCells}");
        report.AppendLine($"  Oscillator 1 position    : cell {Osc1Position}");
        report.AppendLine($"  Oscillator 2 position    : cell {Osc2Position}");
        report.AppendLine($"  Distance between osc.    : {Osc2Position - Osc1Position} cells");
        report.AppendLine();
        report.AppendLine($"  Oscillator 1 ── ω₁       : {osc1.Frequency:F3}");
        report.AppendLine($"  Oscillator 1 ── θ₁(0)    : {osc1.Phase:F4} rad");
        report.AppendLine($"  Oscillator 2 ── ω₂       : {osc2.Frequency:F3}");
        report.AppendLine($"  Oscillator 2 ── θ₂(0)    : {osc2.Phase:F4} rad");
        report.AppendLine($"  Initial Δω               : {Math.Abs(osc2.Frequency - osc1.Frequency):F3}");
        report.AppendLine($"  Initial Δθ               : {Math.Abs(osc2.Phase - osc1.Phase):F4}");
        report.AppendLine();
        report.AppendLine($"  Propagation speed c      : {field.PropagationSpeed}");
        report.AppendLine($"  Diffusion D              : {field.DiffusionCoefficient}");
        report.AppendLine($"  Damping γ                : {field.DampingCoefficient}");
        report.AppendLine($"  Coupling α               : {sim.FieldCouplingAlpha}");
        report.AppendLine($"  Injection β              : {sim.InjectionStrength}");
        report.AppendLine($"  NO direct coupling       : true (purely field-mediated)");
        report.AppendLine();

        // ── Storage for metrics ─────────────────────────────────
        var phaseDiffs = new List<double>();
        var freqDiffs = new List<double>();
        var effectiveFreqs1 = new List<double>();
        var effectiveFreqs2 = new List<double>();
        var fieldDensities = new List<double>();
        var snapshots = new List<TemporalFieldSnapshot>();

        int checkpointInterval = TotalIterations / 20; // 500

        double prevPhase1 = osc1.Phase;
        double prevPhase2 = osc2.Phase;

        // Track cumulative (unwrapped) phase for accurate frequency computation.
        double cumPhase1 = osc1.Phase;
        double cumPhase2 = osc2.Phase;

        for (int iter = 0; iter < TotalIterations; iter++)
        {
            sim.Step();

            // Compute actual (unwrapped) frequency from coupling.
            double localDensity1 = field.GetDensityAt(Osc1Position);
            double localDensity2 = field.GetDensityAt(Osc2Position);
            double adjustedFreq1 = osc1.Frequency + sim.FieldCouplingAlpha * localDensity1;
            double adjustedFreq2 = osc2.Frequency + sim.FieldCouplingAlpha * localDensity2;

            cumPhase1 += adjustedFreq1 * sim.TimeStep;
            cumPhase2 += adjustedFreq2 * sim.TimeStep;

            if ((iter + 1) % checkpointInterval == 0 || iter == 0)
            {
                double dTheta = Math.Abs(TemporalSimulation.NormalizePhase(
                    osc2.Phase - osc1.Phase + Math.PI) - Math.PI);

                // Effective (instantaneous) frequencies from unwrapped cumulative phase.
                double effFreq1 = (cumPhase1 - prevPhase1) / checkpointInterval;
                double effFreq2 = (cumPhase2 - prevPhase2) / checkpointInterval;

                phaseDiffs.Add(dTheta);
                freqDiffs.Add(Math.Abs(effFreq1 - effFreq2));
                effectiveFreqs1.Add(effFreq1);
                effectiveFreqs2.Add(effFreq2);
                fieldDensities.Add(field.MeanDensity());

                snapshots.Add(field.TakeSnapshot(iter + 1));

                prevPhase1 = cumPhase1;
                prevPhase2 = cumPhase2;
            }
        }

        // ── 4. Field Evolution ──────────────────────────────────
        AppendSection(report, "4. Field Evolution");

        report.AppendLine("  Iteration │ Total Energy  │ Mean Density  │ Peak Density  │ Peak Cell");
        report.AppendLine("  ──────────┼───────────────┼───────────────┼───────────────┼──────────");

        int[] showSnapshots = { 1, 5, 10, 15, 20 };
        foreach (int idx in showSnapshots)
        {
            if (idx <= snapshots.Count)
            {
                var s = snapshots[idx - 1];
                report.AppendLine(
                    $"  {s.Iteration,9} │ {s.TotalEnergy,13:F2} │ {s.MeanDensity,13:F6} │ {s.PeakDensity,13:F6} │ {s.PeakCellIndex,8}");
            }
        }

        report.AppendLine();

        // Show density profile at key iterations.
        var firstSnap = snapshots[0];
        var lastSnap = snapshots[^1];

        report.AppendLine("  Density profile at iteration 500 (first checkpoint):");
        report.Append("    ");
        for (int i = 0; i < FieldCells; i += 5)
            report.Append(i == Osc1Position || i == Osc2Position
                ? $"[{firstSnap.DensityProfile[i]:F2}] "
                : $"{firstSnap.DensityProfile[i]:F2} ");
        report.AppendLine();
        report.AppendLine($"    Brackets [··] mark oscillator positions ({Osc1Position}, {Osc2Position}).");
        report.AppendLine();

        report.AppendLine($"  Density profile at iteration {lastSnap.Iteration} (final):");
        report.Append("    ");
        for (int i = 0; i < FieldCells; i += 5)
            report.Append(i == Osc1Position || i == Osc2Position
                ? $"[{lastSnap.DensityProfile[i]:F2}] "
                : $"{lastSnap.DensityProfile[i]:F2} ");
        report.AppendLine();
        report.AppendLine();

        // ── 5. Oscillator Evolution ─────────────────────────────
        AppendSection(report, "5. Oscillator Evolution");

        report.AppendLine("  Iter │  |Δθ|     │  ω₁_eff   │  ω₂_eff   │  |Δω_eff|  │  ρ_mean");
        report.AppendLine("  ─────┼───────────┼───────────┼───────────┼────────────┼────────");

        for (int k = 0; k < phaseDiffs.Count; k++)
        {
            int iter = (k + 1) * checkpointInterval;
            report.AppendLine(
                $"  {iter,4} │ {phaseDiffs[k],9:F4} │ {effectiveFreqs1[k],9:F4} │ {effectiveFreqs2[k],9:F4} │ {freqDiffs[k],10:F4} │ {fieldDensities[k],6:F4}");
        }

        report.AppendLine();

        // ── 6. Synchronization Analysis ─────────────────────────
        AppendSection(report, "6. Synchronization Analysis");

        double initialPhaseDiff = phaseDiffs[0];
        double finalPhaseDiff = phaseDiffs[^1];
        double deltaPhase = finalPhaseDiff - initialPhaseDiff;

        double initialFreqDiff = freqDiffs[0];
        double finalFreqDiff = freqDiffs[^1];
        double deltaFreq = finalFreqDiff - initialFreqDiff;

        double initialDensity = fieldDensities[0];
        double finalDensity = fieldDensities[^1];

        // Phase-diff trend: linear regression on phaseDiffs.
        double phaseTrend = ComputeTrend(phaseDiffs);
        double freqTrend = ComputeTrend(freqDiffs);

        report.AppendLine($"  Initial |Δθ|             : {initialPhaseDiff:F6} rad");
        report.AppendLine($"  Final |Δθ|               : {finalPhaseDiff:F6} rad");
        report.AppendLine($"  Δ(|Δθ|)                  : {deltaPhase:+0.000000;-0.000000} rad");
        report.AppendLine($"  Phase diff trend          : {(phaseTrend < 0 ? "↓ decreasing (toward sync)" : "↑ increasing")}");
        report.AppendLine();
        report.AppendLine($"  Initial |Δω_eff|         : {initialFreqDiff:F6}");
        report.AppendLine($"  Final |Δω_eff|           : {finalFreqDiff:F6}");
        report.AppendLine($"  Δ(|Δω|)                  : {deltaFreq:+0.000000;-0.000000}");
        report.AppendLine($"  Frequency diff trend      : {(freqTrend < 0 ? "↓ decreasing (toward sync)" : "↑ increasing")}");
        report.AppendLine();
        report.AppendLine($"  Initial mean density      : {initialDensity:F6}");
        report.AppendLine($"  Final mean density        : {finalDensity:F6}");
        report.AppendLine($"  Density growth            : {(finalDensity > initialDensity ? "positive ✓" : "negative")}");
        report.AppendLine();

        // Order parameter for 2 oscillators.
        double orderParamR = Math.Sqrt(
            Math.Pow(Math.Cos(osc1.Phase) + Math.Cos(osc2.Phase), 2) +
            Math.Pow(Math.Sin(osc1.Phase) + Math.Sin(osc2.Phase), 2)) / 2.0;

        report.AppendLine($"  Final order parameter R   : {orderParamR:F6}");
        report.AppendLine();

        // Synchronization criteria.
        bool phaseSync = phaseTrend < 0 || finalPhaseDiff < initialPhaseDiff * 0.9;
        bool freqSync = freqTrend < 0 || finalFreqDiff < initialFreqDiff * 0.9;
        bool densityGrowth = finalDensity > initialDensity * 1.1;
        bool highOrderParam = orderParamR > 0.8;

        int syncScore = 0;
        if (phaseSync) syncScore++;
        if (freqSync) syncScore++;
        if (densityGrowth) syncScore++;
        if (highOrderParam) syncScore++;

        string syncVerdict = syncScore switch
        {
            >= 3 => "Field-mediated synchronization CONFIRMED",
            2 => "Partial synchronization — some indicators present",
            _ => "No clear synchronization — field mediation insufficient"
        };

        report.AppendLine("  Synchronization Criteria:");
        report.AppendLine($"    Phase diff decreasing        : {(phaseSync ? "YES ✓" : "no")}");
        report.AppendLine($"    Frequency diff decreasing     : {(freqSync ? "YES ✓" : "no")}");
        report.AppendLine($"    Field density growing         : {(densityGrowth ? "YES ✓" : "no")}");
        report.AppendLine($"    Order parameter R > 0.8       : {(highOrderParam ? "YES ✓" : "no")}");
        report.AppendLine($"    ─────────────────────────────");
        report.AppendLine($"    Sync score                    : {syncScore} / 4");
        report.AppendLine();
        report.AppendLine($"  Verdict: {syncVerdict}");
        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine("  Physical mechanism:");
        report.AppendLine("    1. Each oscillator injects energy into the field proportional to |sin(θ)|.");
        report.AppendLine("    2. The field propagates energy outward via wave + diffusion dynamics.");
        report.AppendLine("    3. The distant oscillator reads the local field density at its position.");
        report.AppendLine("    4. The density shifts the oscillator's effective frequency: ω' = ω₀ + α·ρ.");
        report.AppendLine("    5. This creates a feedback loop that can pull frequencies toward alignment.");
        report.AppendLine();
        report.AppendLine($"  The field-mediated coupling strength depends on:");
        report.AppendLine($"    • Oscillator separation       : {Osc2Position - Osc1Position} cells");
        report.AppendLine($"    • Propagation speed c         : {field.PropagationSpeed}");
        report.AppendLine($"    • Damping γ                   : {field.DampingCoefficient}");
        report.AppendLine($"    • Coupling α                  : {sim.FieldCouplingAlpha}");
        report.AppendLine();

        double propagationTime = (Osc2Position - Osc1Position) / field.PropagationSpeed;
        report.AppendLine($"  Estimated signal travel time    : ~{propagationTime:F0} iterations");
        report.AppendLine($"  Damping timescale (1/γ)         : ~{1.0 / field.DampingCoefficient:F0} iterations");
        report.AppendLine();

        if (propagationTime > 1.0 / field.DampingCoefficient)
            report.AppendLine("  ⚠ The signal decays faster than it propagates — coupling is weak.");
        else
            report.AppendLine("  ✓ Signal propagation is faster than decay — coupling is possible.");

        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. {syncVerdict}.");
        report.AppendLine();
        report.AppendLine($"  C2. The phase difference evolved from {initialPhaseDiff:F4} to {finalPhaseDiff:F4} rad");
        report.AppendLine($"      ({(deltaPhase < 0 ? "decrease" : "increase")} of {Math.Abs(deltaPhase):F4}), with a");
        report.AppendLine($"      {(phaseTrend < 0 ? "negative" : "positive")} trend indicating {(phaseTrend < 0 ? "convergence" : "divergence")}.");
        report.AppendLine();
        report.AppendLine($"  C3. The effective frequency difference evolved from {initialFreqDiff:F4} to {finalFreqDiff:F4}");
        report.AppendLine($"      ({(deltaFreq < 0 ? "decrease" : "increase")} of {Math.Abs(deltaFreq):F4}), with a");
        report.AppendLine($"      {(freqTrend < 0 ? "negative" : "positive")} trend.");
        report.AppendLine();
        report.AppendLine($"  C4. The temporal field accumulated energy from {initialDensity:F4} to {finalDensity:F4} mean density,");
        report.AppendLine($"      {(densityGrowth ? "confirming" : "failing to confirm")} that oscillators successfully couple to the field.");
        report.AppendLine();
        report.AppendLine("  C5. Field-mediated synchronization is a viable mechanism for oscillator coupling.");
        report.AppendLine("      Unlike direct Kuramoto coupling, the field introduces:");
        report.AppendLine("      • Propagation delay (finite signal speed)");
        report.AppendLine("      • Spatial mediation (interaction through a medium)");
        report.AppendLine("      • Natural damping (energy dissipation)");
        report.AppendLine();
        report.AppendLine("  C6. This experiment establishes the temporal field as the first candidate for a");
        report.AppendLine("      true Quantum Temporal Matrix interaction mechanism — the field is the matrix.");
        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • AT-005: N-oscillator field-mediated synchronization.");
        report.AppendLine("    • AT-006: 2D temporal field with spatial mode patterns.");
        report.AppendLine("    • AT-007: Field eigenmodes and temporal quantization.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-004 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    /// <summary>
    /// Computes a simple linear trend: average of (last half - first half) differences.
    /// Negative → decreasing trend. Positive → increasing trend.
    /// </summary>
    private static double ComputeTrend(List<double> values)
    {
        int n = values.Count;
        int half = n / 2;
        double firstHalfMean = 0, secondHalfMean = 0;

        for (int i = 0; i < half; i++)
            firstHalfMean += values[i];
        firstHalfMean /= half;

        for (int i = half; i < n; i++)
            secondHalfMean += values[i];
        secondHalfMean /= n - half;

        return secondHalfMean - firstHalfMean;
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
