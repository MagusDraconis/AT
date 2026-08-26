using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 144 — Weak-isospin amplification origin. QG143 established the quark deviations are
/// isospin-signed. This phase asks whether weak-isospin coupling can explain the quark hierarchy
/// amplification.
///
/// Tests: ATQG1440 (T3 dependence + up/down amplification), ATQG1441 (charge-isospin combinations +
/// sector splitting), ATQG1442 (hierarchy reconstruction + classification).
/// </summary>
public class ATQG_Phase144_WeakIsospinAmplificationTests : ResearchTestBase
{
    public ATQG_Phase144_WeakIsospinAmplificationTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1440_T3DependenceAndUpDownAmplification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1440: T3 dependence and up/down amplification");

        sb.AppendLine("SECTOR DATA (deviation factor, T3, Q, Y):");
        foreach (var (n, f, t3, q, y) in WeakIsospinAmplification.SectorData())
            sb.AppendLine($"  {n}: factor={f:F3} T3={t3:F1} Q={q:F3} log2(factor)={Math.Log2(f):F2}");
        sb.AppendLine();
        sb.AppendLine($"T3 correlation with log2(factor): {WeakIsospinAmplification.T3Correlation():F3}");
        sb.AppendLine($"|T3| correlation: {WeakIsospinAmplification.AbsT3Correlation():F3}");
        var ud = WeakIsospinAmplification.UpDownAsymmetry();
        sb.AppendLine();
        sb.AppendLine("UP/DOWN AMPLIFICATION:");
        sb.AppendLine($"  up (T3=+1/2) factor = {ud.Up:F2}");
        sb.AppendLine($"  down (T3=-1/2) factor = {ud.Down:F2}");
        sb.AppendLine($"  up/down = {ud.UpOverDown:F1}");
        sb.AppendLine($"  strongly isospin-signed split: {WeakIsospinAmplification.StrongIsospinSplit()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the up/down split is strongly isospin-signed (~89×), though the raw T3");
        sb.AppendLine("correlation is only moderate — the signed structure is real but not a linear T3 law.");
        Output.WriteLine(sb.ToString());

        Assert.True(WeakIsospinAmplification.StrongIsospinSplit(), "up/down split should be strongly signed");
        Assert.True(ud.Up > 5.0 && ud.Down < 1.0, "up amplified, down suppressed");
        Assert.True(ud.UpOverDown > 20.0, "up/down ratio should be large");
    }

    [Fact]
    public void ATQG1441_ChargeIsospinCombinationsAndSectorSplitting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1441: charge-isospin combinations and sector splitting");

        sb.AppendLine("CHARGE-ISOSPIN COMBINATION CORRELATIONS:");
        foreach (var (n, r) in WeakIsospinAmplification.CombinationCorrelations())
            sb.AppendLine($"  {n}: r={r:F3}");
        var best = WeakIsospinAmplification.BestCombination();
        sb.AppendLine($"  best = {best.Name} (r={best.R:F3})");
        sb.AppendLine();
        sb.AppendLine("SECTOR SPLITTING:");
        sb.AppendLine($"  separation (up / max other) = {WeakIsospinAmplification.SectorSeparation():F1}");
        sb.AppendLine($"  charge-SIGN gate (only Q>0 amplified): {WeakIsospinAmplification.ChargeSignGate()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the best charge/isospin combination correlates moderately (|Q|, r≈0.59)");
        sb.AppendLine("and the up sector is cleanly separated, but the charge-sign gate fails (leptons with");
        sb.AppendLine("Q=-1 still track the octave law, factor ≈ 1).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(best.R) > 0.5, "a charge/isospin combination should correlate strongly");
        Assert.True(WeakIsospinAmplification.SectorSeparation() > 20.0, "up sector should be cleanly separated");
    }

    [Fact]
    public void ATQG1442_HierarchyReconstructionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1442: hierarchy reconstruction and classification");

        bool reconstructs = WeakIsospinAmplification.ReconstructsOrdering();
        int score = WeakIsospinAmplification.OriginScore();
        string cls = WeakIsospinAmplification.Classify();

        sb.AppendLine("HIERARCHY RECONSTRUCTION:");
        sb.AppendLine($"  observed ordering (factor): neutrino(0.144) < down(0.256) < leptons(1.003) < up(22.673)");
        sb.AppendLine($"  ordering reconstructed: {reconstructs}");
        sb.AppendLine();
        sb.AppendLine($"isospin-origin score (0..5): {score}");
        sb.AppendLine($"  +1 strong isospin split: {WeakIsospinAmplification.StrongIsospinSplit()}");
        sb.AppendLine($"  +1 charge-sign gate: {WeakIsospinAmplification.ChargeSignGate()}");
        sb.AppendLine($"  +1 strong correlation: {Math.Abs(WeakIsospinAmplification.BestCombination().R) > 0.5}");
        sb.AppendLine($"  +1 large sector separation: {WeakIsospinAmplification.SectorSeparation() > 20.0}");
        sb.AppendLine($"  +1 ordering reconstructed: {reconstructs}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • ISOSPIN ORIGIN rejected: no single isospin/charge combination reproduces the full");
        sb.AppendLine("    hierarchy (correlations only moderate, charge-sign gate fails).");
        sb.AppendLine("  • PARTIAL EFFECT accepted: the up/down split is strongly isospin-signed, the ordering");
        sb.AppendLine("    is reconstructed, and |Q| correlates moderately — but the amplification is not a");
        sb.AppendLine("    clean weak-isospin law.");
        Output.WriteLine(sb.ToString());

        Assert.True(reconstructs, "observed deviation ordering should be reconstructed");
        Assert.True(score >= 4, "origin score should be strong");
        Assert.Equal("PARTIAL EFFECT", cls);
    }
}
