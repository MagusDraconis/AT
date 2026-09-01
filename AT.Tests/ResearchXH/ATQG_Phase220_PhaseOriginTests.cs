using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 220 — Phase Origin. Derive θ (the U(1) angle) from network structure — no new primitives,
/// Q-events only, deterministic. Investigates: causal ordering, actualization timing, branch depth, network
/// cycles, link orientation, connectivity phase.
/// </summary>
public class ATQG_Phase220_PhaseOriginTests : ResearchTestBase
{
    public ATQG_Phase220_PhaseOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2200_CausalCirculationPhase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2200: the U(1) angle from causal circulation — cycle closure fixes the phase quantum");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Q-events actualize in a causal order (QG1/QG11); each event has a branch depth k.");
        sb.AppendLine("  - The observable attractor is a circulant ring C_N (N=96, QG155/159); the cycle closes");
        sb.AppendLine("    after N ticks, fixing the phase quantum Δθ = 2π/N per tick.");
        sb.AppendLine("  - The phase of an event at position k is θ_k = 2π·k/N (fraction of the cycle completed).");
        sb.AppendLine();

        int N = 96;
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Cycle closure N·Δθ = 2π?  {PhaseOrigin.CycleCloses(N)}");
        sb.AppendLine($"  Phase quantum Δθ = 2π/{N} = {PhaseOrigin.PhaseQuantum(N):F6} rad = {PhaseOrigin.PhaseQuantum(N) * 180 / Math.PI:F4}°");
        sb.AppendLine($"  θ at branch depth k=0  = {PhaseOrigin.PhaseFromPosition(0, N):F4}");
        sb.AppendLine($"  θ at branch depth k=16 = {PhaseOrigin.PhaseFromPosition(16, N):F4}  (= 60° = π/3: quarter-family spacing)");
        sb.AppendLine($"  θ at branch depth k=24 = {PhaseOrigin.PhaseFromPosition(24, N):F4}  (= 90° = π/2: quarter cycle)");
        sb.AppendLine($"  θ at branch depth k=48 = {PhaseOrigin.PhaseFromPosition(48, N):F4}  (= 180° = π: half cycle)");
        sb.AppendLine($"  θ periodic (θ_{{k+96}} = θ_k)?  {PhaseOrigin.PhasePeriodic(5, N)}");
        sb.AppendLine($"  Phase difference Δθ(24,0) = {PhaseOrigin.PhaseDifference(24, 0, N):F4}  (= 2π·d/N, d = graph distance)");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The causal order fixes the position k; the cycle period N fixes the quantum 2π/N;");
        sb.AppendLine("    θ_k = 2πk/N is a deterministic U(1) angle from Q-event structure alone.");
        sb.AppendLine("  - The phase difference between events is the connectivity (graph distance) converted by");
        sb.AppendLine("    2π/N — phase differences are derived from the network, not imported.");

        Output.WriteLine(sb.ToString());

        Assert.True(PhaseOrigin.CycleCloses(N), "cycle closure must fix the phase quantum");
        Assert.Equal(0.0, PhaseOrigin.PhaseFromPosition(0, N), 6);
        Assert.Equal(Math.PI / 2.0, PhaseOrigin.PhaseFromPosition(24, N), 6);
        Assert.True(PhaseOrigin.PhasePeriodic(5, N), "phase must be 2π-periodic in the causal position");
    }

    [Fact]
    public void ATQG2201_LinkOrientationCyclesConnectivity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2201: link orientation, network cycles, and the connectivity phase");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Links carry the circulation phase: forward (i→i+1) adds +2π/N, backward subtracts.");
        sb.AppendLine("  - A path of L oriented links accumulates Σ θ_links = 2πL/N (QG65 path phase).");
        sb.AppendLine("  - A loop of length L has holonomy 2πL/N; the full cycle (L=N) is trivial (gauge).");
        sb.AppendLine();

        int N = 96;
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Link phase forward  = {PhaseOrigin.LinkPhaseForward(N):F6}  (+2π/N)");
        sb.AppendLine($"  Link phase backward = {PhaseOrigin.LinkPhaseBackward(N):F6}  (−2π/N)");
        sb.AppendLine($"  Path phase L=7      = {PhaseOrigin.PathPhase(7, N):F6}");
        sb.AppendLine($"  Path accumulates Σ θ_links?  {PhaseOrigin.PathAccumulates(7, N)}");
        sb.AppendLine($"  Loop holonomy L=48  = {PhaseOrigin.LoopHolonomy(48, N):F4}  (= π, half cycle)");
        sb.AppendLine($"  Full cycle L=96     = {PhaseOrigin.LoopHolonomy(N, N):F4}  (= 0 mod 2π, trivial)");
        sb.AppendLine($"  Full cycle trivial? {PhaseOrigin.FullCycleTrivial(N)}");
        sb.AppendLine($"  Interference P(k₁=3,k₂=7) = {PhaseOrigin.InterferenceFromPositions(3, 7, N):F4}");
        sb.AppendLine($"  Interference connectivity-determined? {PhaseOrigin.InterferenceConnectivityDetermined(3, 7, N)}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Link orientation assigns a signed phase; path phases telescope to Σ θ_links (QG65).");
        sb.AppendLine("  - Loop holonomies are DERIVED (non-trivial for L ≠ N, trivial for the full cycle).");
        sb.AppendLine("  - The interference pattern is determined by the causal distance k₁−k₂ — connectivity phase.");

        Output.WriteLine(sb.ToString());

        Assert.True(PhaseOrigin.PathAccumulates(7, N), "path phase must equal the sum of link phases");
        Assert.True(PhaseOrigin.FullCycleTrivial(N), "the full-cycle holonomy must be trivial (gauge)");
        Assert.True(PhaseOrigin.InterferenceConnectivityDetermined(3, 7, N), "interference depends only on the causal distance");
        Assert.Equal(Math.PI, PhaseOrigin.LoopHolonomy(48, N), 6);
    }

    [Fact]
    public void ATQG2202_ClassificationPhaseOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2202: classification — PHASE ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The complete amplitude is ψ_k = √ρ_k · e^(iθ_k) with ρ_k = μ^k/S (QG216) and θ_k = 2πk/N.");
        sb.AppendLine("  - A single global phase is gauge; the observable content is the phase difference, which");
        sb.AppendLine("    is fully determined by the causal positions (connectivity).");
        sb.AppendLine();

        int score = PhaseOrigin.OriginScore();
        string classification = PhaseOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 cycle closure fixes the quantum ({PhaseOrigin.CycleCloses(96)})");
        sb.AppendLine($"    +1 deterministic periodic phase from branch depth ({PhaseOrigin.PhasePeriodic(5, 96)})");
        sb.AppendLine($"    +1 link orientation → path phase = Σ θ_links ({PhaseOrigin.PathAccumulates(7, 96)})");
        sb.AppendLine($"    +1 loop holonomies derived, full cycle trivial ({PhaseOrigin.FullCycleTrivial(96)})");
        sb.AppendLine($"    +1 Born rule preserved + interference connectivity-determined ({PhaseOrigin.BornRuleWithPhase(2.0, 8, 96)} / {PhaseOrigin.InterferenceConnectivityDetermined(3, 7, 96)})");
        sb.AppendLine($"  Complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N), k=3, μ=2, K=8, N=96:");
        var amp = PhaseOrigin.Amplitude(2.0, 3, 8, 96);
        double mag = QuantumAmplitudeOrigin.AmplitudeMagnitude(2.0, 3, 8);
        sb.AppendLine($"    |ψ₃| = √ρ₃ = {mag:F6}, θ₃ = {PhaseOrigin.PhaseFromPosition(3, 96):F4} → ψ₃ = {amp.Re:F6} + {amp.Im:F6}i");
        sb.AppendLine($"  Born rule Σ|ψ|² = 1 with phase? {PhaseOrigin.BornRuleWithPhase(2.0, 8, 96)}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - θ_k = 2πk/N is DERIVED from the network: causal order → position k, cycle period →");
        sb.AppendLine("    quantum 2π/N, link orientation → sign, connectivity → phase differences/interference.");
        sb.AppendLine("  - No new primitives: the phase is the circulation of the actualization cycle, the same");
        sb.AppendLine("    rotational structure that generates the Z2 doublets (QG155) and the CP phase (QG166).");
        sb.AppendLine("  - The magnitude (QG216) + the phase (this phase) give the complete amplitude from Q-events.");
        sb.AppendLine($"  ⇒ {classification} — this closes the QG219 gap (a) 'the phase origin'.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PHASE ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(PhaseOrigin.BornRuleWithPhase(2.0, 8, 96), "the Born rule must be preserved with the derived phase");
    }
}
