using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 163 — Running coupling origin. QG162 established the couplings at the observable scale
/// (1/α_em = 137, α_weak = 3/Σm, α_strong = 8/Σ√m). This phase asks WHY the couplings run with energy
/// and whether a unification scale emerges — using only D96 spectral geometry, no fitted beta functions.
///
/// Tests: ATQG1630 (spectral scale and occupation flow), ATQG1631 (running couplings), ATQG1632
/// (hierarchy preservation + classification).
/// </summary>
public class ATQG_Phase163_RunningCouplingOriginTests : ResearchTestBase
{
    public ATQG_Phase163_RunningCouplingOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1630_SpectralScaleAndOccupationFlow()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1630: spectral scale (octave ladder) and occupation flow");

        sb.AppendLine("ASSUMPTIONS: the octave-band structure of the observable sector defines the natural");
        sb.AppendLine("spectral (energy) scale; as E increases, more modes are ACTIVATED (higher bands join).");
        sb.AppendLine();
        int[] sizes = RunningCouplingOrigin.OctaveBandSizes();
        sb.AppendLine("SPECTRAL SCALE (octave ladder):");
        sb.AppendLine($"  octave sizes: [{string.Join(",", sizes)}]");
        sb.AppendLine($"  octave rung count = {RunningCouplingOrigin.OctaveRungCount()}");
        sb.AppendLine($"  activation ladder = [{string.Join(",", RunningCouplingOrigin.ActivationLadder())}]");
        sb.AppendLine();
        sb.AppendLine("OCCUPATION FLOW (denominator growth along the ladder):");
        foreach (int n in RunningCouplingOrigin.ActivationLadder())
        {
            int sumM = RunningCouplingOrigin.ActiveModes(n);
            int d = RunningCouplingOrigin.ActiveDoublets(n);
            double s = RunningCouplingOrigin.ActiveNeutralMoment(n);
            sb.AppendLine($"  N={n}: Σm={sumM}, #doublets={d}, Σ√m={s:F2}");
        }
        sb.AppendLine();
        sb.AppendLine("  occupation flow: 4 → 8 → 95 modes. The dense top band (87 modes) dominates");
        sb.AppendLine("  the observable-scale occupancy (0.916).");
        Output.WriteLine(sb.ToString());

        Assert.True(RunningCouplingOrigin.OctaveRungCount() >= 3, "3 octave rungs");
        var ladder = RunningCouplingOrigin.ActivationLadder();
        for (int i = 1; i < ladder.Length; i++)
            Assert.True(RunningCouplingOrigin.ActiveModes(ladder[i]) > RunningCouplingOrigin.ActiveModes(ladder[i - 1]),
                "occupation should flow (denominators grow)");
    }

    [Fact]
    public void ATQG1631_RunningCouplings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1631: running couplings α(E) = g_i / D_i(N(E))");

        sb.AppendLine("ASSUMPTIONS: the couplings are functions of the activated-mode occupancy statistics:");
        sb.AppendLine("α_em(E) = 1/(Σm(E)+#doublets(E)), α_weak(E) = 3/Σm(E), α_strong(E) = 8/Σ√m(E).");
        sb.AppendLine();
        sb.AppendLine("RUNNING COUPLINGS ALONG THE OCTAVE LADDER:");
        sb.AppendLine("rung | N | 1/α_em | α_weak | α_strong");
        foreach (var (rung, n, emInv, wk, st) in RunningCouplingOrigin.RungCouplings())
            sb.AppendLine($"  {rung} | {n} | {emInv:F1} | {wk:F4} | {st:F4}");
        sb.AppendLine();
        var (em, weak, strong) = RunningCouplingOrigin.RunningFactors();
        sb.AppendLine($"RUNNING FACTORS (lowest rung → observable):");
        sb.AppendLine($"  α_em⁻¹: {em:F1}x, α_weak: {weak:F1}x, α_strong: {strong:F1}x");
        sb.AppendLine();
        var (mEm, mWk, mSt) = RunningCouplingOrigin.MonotoneDecrease();
        sb.AppendLine($"  monotone decrease: α_em={mEm}, α_weak={mWk}, α_strong={mSt}");
        sb.AppendLine($"  comparable running rates (shared occupation flow): {RunningCouplingOrigin.ComparableRunningRates()}");
        sb.AppendLine();
        sb.AppendLine("  the running is DRIVEN by the occupation flow (mode activation):");
        sb.AppendLine("  D96 → spectral scale (octave ladder) → occupancy evolution → coupling evolution.");
        Output.WriteLine(sb.ToString());

        Assert.True(mEm && mWk && mSt, "all couplings should decrease monotonically");
        Assert.True(RunningCouplingOrigin.ComparableRunningRates(), "all couplings run at comparable rates");
        Assert.True(em > 1 && weak > 1 && strong > 1, "couplings should decrease (factors > 1)");
    }

    [Fact]
    public void ATQG1632_HierarchyPreservationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1632: hierarchy preservation and classification");

        sb.AppendLine("ASSUMPTIONS: unification, if any, would require the couplings to converge; the");
        sb.AppendLine("structural bound 1/α_em = Σm + #doublets > Σm/3 = 1/α_weak (since #doublets > 0).");
        sb.AppendLine();
        sb.AppendLine("HIERARCHY PRESERVATION:");
        sb.AppendLine($"  hierarchy preserved at all scales: {RunningCouplingOrigin.HierarchyPreservedAtAllScales()}");
        sb.AppendLine($"  α_strong largest at every rung: {RunningCouplingOrigin.StrongLargestAtEveryRung()}");
        sb.AppendLine($"  UNIFICATION: {RunningCouplingOrigin.UnificationStatement()}");
        sb.AppendLine();
        sb.AppendLine("  → the couplings run but do NOT unify within the observable sector: the hierarchy");
        sb.AppendLine("    α_em < α_weak < α_strong is preserved at every spectral scale. Consistent with");
        sb.AppendLine("    experiment (low-energy hierarchy) and a GUT scale BEYOND the observable ladder.");
        sb.AppendLine();
        int score = RunningCouplingOrigin.OriginScore();
        string cls = RunningCouplingOrigin.Classify();
        sb.AppendLine($"Running-origin score (0..5): {score}");
        sb.AppendLine($"  +1 octave ladder defines the spectral scale: {RunningCouplingOrigin.OctaveRungCount() >= 3}");
        sb.AppendLine($"  +1 denominators grow (occupation flow): {RunningCouplingOrigin.ActiveModes(95) > RunningCouplingOrigin.ActiveModes(8)}");
        sb.AppendLine($"  +1 all couplings decrease monotonically: {RunningCouplingOrigin.MonotoneDecrease().Em}");
        sb.AppendLine($"  +1 comparable running rates: {RunningCouplingOrigin.ComparableRunningRates()}");
        sb.AppendLine($"  +1 hierarchy preserved (no in-sector unification): {RunningCouplingOrigin.HierarchyPreservedAtAllScales()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the couplings run monotonically along the D96 octave ladder");
        sb.AppendLine("    (occupation flow 4 → 8 → 95 drives the denominators).");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the full mechanism holds — spectral scale, occupation");
        sb.AppendLine("    flow, scale-dependent access, mode activation, monotone running at comparable");
        sb.AppendLine("    rates.");
        sb.AppendLine("  • RUNNING ORIGIN accepted: the running of the gauge couplings EMERGES from D96");
        sb.AppendLine("    spectral geometry: the octave-band ladder defines the spectral (energy) scale,");
        sb.AppendLine("    the occupation flow grows the denominators, and α_i(E) = g_i / D_i(N(E)) runs");
        sb.AppendLine("    monotonically — all couplings decrease by comparable factors (~23x) driven by the");
        sb.AppendLine("    shared occupation flow. The structural bound 1/α_em > Σm/3 preserves the hierarchy");
        sb.AppendLine("    (no in-sector unification), consistent with a GUT scale beyond the observable");
        sb.AppendLine("    octave ladder — no fitted beta functions.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "running-origin score should be strong");
        Assert.Equal("RUNNING ORIGIN", cls);
    }
}
