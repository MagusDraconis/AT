using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_029 — Closure-Defect Audit test suite (Y_D_029_Tests.cs).
///
/// Question: what structure must be closed so that no inconsistency remains?
///
/// Verdict tested: closure removes structural defects (unpaired modes, broken seed
/// half-shift, wrong family count, span ≥ 8) — producing the zero-defect set
/// {60, 66, …, 120} (11 rings with 6|N + 3 families). But closure does NOT select
/// N=96: N=60/90/120 are zero-defect too. The octave-rung structure n = 3·2^k is the
/// discriminator — N=96 = 3·2⁵ is the UNIQUE zero-defect octave rung in [32,300]
/// (48 has 2 families, 192 has 4). Closure removes inconsistency (EMERGENT zero-defect
/// set); the specific N=96 is BOUNDARY (octave-rung selection, D_020).
///
/// Deterministic: closed-form circulant eigenvalues.
/// </summary>
public class Y_D_029_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_029_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n) => Math.Sqrt(Lambda(k, n));

    private static double Span(int n)
    {
        var freqs = new double[n - 1];
        for (int k = 1; k < n; k++) freqs[k - 1] = Omega(k, n);
        Array.Sort(freqs);
        return freqs[^1] / freqs[0];
    }

    private static int FamilyCount(int n) => (int)Math.Floor(Math.Log2(Span(n))) + 1;

    private static int UnpairedCount(int n)
    {
        var evals = new List<double>();
        for (int k = 1; k < n; k++) evals.Add(Math.Round(Lambda(k, n), 9));
        var mult = evals.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        int unpaired = 0;
        for (int k = 1; k < n; k++)
            if (k == n - k && mult[Math.Round(Lambda(k, n), 9)] == 1) unpaired++;
        return unpaired;
    }

    /// <summary>Inconsistency count: [unpaired>0] + [6∤N] + [families≠3] + [span≥8].</summary>
    private static int DefectCount(int n)
    {
        int d = 0;
        if (UnpairedCount(n) > 0) d++;
        if (n % 6 != 0) d++;
        if (FamilyCount(n) != 3) d++;
        if (Span(n) >= 8.0) d++;
        return d;
    }

    /// <summary>Is n an octave rung of period 3 (n = 3·2^k)?</summary>
    private static bool IsOctaveRung(int n)
    {
        if (n % 3 != 0) return false;
        int m = n / 3;
        while (m > 1 && m % 2 == 0) m /= 2;
        return m == 1;
    }

    // ── [Required] Y_D_029_UnpairedModes ───────────────────────────────

    /// <summary>
    /// Incomplete Z2: unpaired(64/80/128) = 1, unpaired(96/192) = 0.
    /// </summary>
    [Fact]
    public void Y_D_029_UnpairedModes()
    {
        Assert.Equal(1, UnpairedCount(64));
        Assert.Equal(1, UnpairedCount(80));
        Assert.Equal(1, UnpairedCount(128));
        Assert.Equal(0, UnpairedCount(96));
        Assert.Equal(0, UnpairedCount(192));
    }

    // ── [Required] Y_D_029_BrokenSymmetry ──────────────────────────────

    /// <summary>
    /// Broken seed half-shift: 6|N holds at 96; broken at 64/80/128/245.
    /// </summary>
    [Fact]
    public void Y_D_029_BrokenSymmetry()
    {
        Assert.True(96 % 6 == 0);
        Assert.True(192 % 6 == 0);
        Assert.False(64 % 6 == 0);
        Assert.False(80 % 6 == 0);
        Assert.False(128 % 6 == 0);
        Assert.False(245 % 6 == 0);
    }

    // ── [Required] Y_D_029_CycleClosure ────────────────────────────────

    /// <summary>
    /// Cycle closure (zero-defect set): {60, 66, …, 120} — 11 rings with 6|N + 3
    /// families and 0 defects.
    /// </summary>
    [Fact]
    public void Y_D_029_CycleClosure()
    {
        var zeros = new List<int>();
        for (int n = 32; n <= 300; n++)
            if (DefectCount(n) == 0) zeros.Add(n);
        Assert.Equal(11, zeros.Count);
        Assert.Equal(new[] { 60, 66, 72, 78, 84, 90, 96, 102, 108, 114, 120 }, zeros);
    }

    // ── [Required] Y_D_029_RepresentationClosure ───────────────────────

    /// <summary>
    /// Representation closure: N=96 is the UNIQUE zero-defect octave rung in [32,300].
    /// </summary>
    [Fact]
    public void Y_D_029_RepresentationClosure()
    {
        var zeroRungs = new List<int>();
        for (int n = 32; n <= 300; n++)
            if (DefectCount(n) == 0 && IsOctaveRung(n)) zeroRungs.Add(n);
        Assert.Equal(new[] { 96 }, zeroRungs);

        // The octave-rung chain: 48 (2 fam), 96 (3 fam), 192 (4 fam).
        Assert.True(IsOctaveRung(48));
        Assert.True(IsOctaveRung(96));
        Assert.True(IsOctaveRung(192));
        Assert.Equal(2, FamilyCount(48));
        Assert.Equal(3, FamilyCount(96));
        Assert.Equal(4, FamilyCount(192));
    }

    // ── [Required] Y_D_029_InconsistencyCount ──────────────────────────

    /// <summary>
    /// Inconsistency counts: 64=2, 80=2, 128=4, 192=2, 245=3, 96=0.
    /// </summary>
    [Fact]
    public void Y_D_029_InconsistencyCount()
    {
        Assert.Equal(2, DefectCount(64));
        Assert.Equal(2, DefectCount(80));
        Assert.Equal(4, DefectCount(128));
        Assert.Equal(2, DefectCount(192));
        Assert.Equal(3, DefectCount(245));
        Assert.Equal(0, DefectCount(96));
        Assert.Equal(1, DefectCount(48));
    }

    // ── [Required] Y_D_029_Run ──────────────────────────────────────────

    [Fact]
    public void Y_D_029_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_029 — Closure-Defect Audit");

        sb.AppendLine("Goal: what structure must be closed so that no inconsistency");
        sb.AppendLine("remains?");
        sb.AppendLine();

        sb.AppendLine("[1] The inconsistency hierarchy");
        sb.AppendLine("    L1 incomplete Z2 (unpaired): N=64, 80, 128");
        sb.AppendLine("    L2 broken seed half-shift (6|N): N=64, 80, 128, 245");
        sb.AppendLine("    L3 wrong family count: N=48, 128, 192, 245");
        sb.AppendLine("    L4 span >= 8: N=128, 192, 245");
        sb.AppendLine();

        sb.AppendLine("[2] Inconsistency count vs N");
        foreach (int n in new[] { 48, 64, 80, 96, 128, 192, 245 })
            sb.AppendLine($"    N={n}: defect count = {DefectCount(n)}");
        sb.AppendLine();

        sb.AppendLine("[3] The zero-defect set");
        var zeros = new List<int>();
        for (int n = 32; n <= 300; n++)
            if (DefectCount(n) == 0) zeros.Add(n);
        sb.AppendLine($"    zero-defect N in [32,300]: {string.Join(",", zeros)}");
        sb.AppendLine("    closure removes the defects -> this set (11 rings)");
        sb.AppendLine();

        sb.AppendLine("[4] The discriminator");
        var rungs = zeros.Where(IsOctaveRung).ToArray();
        sb.AppendLine($"    zero-defect octave rungs: {string.Join(",", rungs)} (only 96)");
        sb.AppendLine("    (48 has 2 families; 192 has 4)");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    closure removes inconsistency (EMERGENT zero-defect set);");
        sb.AppendLine("    the specific N=96 is BOUNDARY (octave-rung selection, D_020).");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
