using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_014 — Automatic Ring-Size Rank Scan Audit test suite (Y_M_014_Tests.cs).
///
/// Question: which ring size does the canonical D96 attractor occupy when every N in a
/// wide range is evaluated AUTOMATICALLY, with NO manual candidate selection?
///
/// Verdict tested: an exhaustive scan over ALL N = 16..512 (497 rings, no manual
/// preselection) with the explicit composite Score(N) = [0 unpaired] + [3 families ∧
/// span < 8] + [6|N] + [N = 3·2^k] finds N = 96 as the UNIQUE Score-4 maximizer
/// (distribution {4:1, 3:14, 2:101, 1:255, 0:126}). Score-3 = seed-3 rungs at wrong
/// family count (24, 48, 192, 384) + the D_029 zero-defect rings {60..120}\{96}. Drop
/// analysis: removing the 3-family-window admits the 5 rungs; removing the octave rung
/// admits the 11 zero-defect rings; removing pairing or 6-divisibility leaves 96 unique.
/// Confirms D_029/D_030/D_031 without manual candidate selection.
///
/// Deterministic: closed-form circulant eigenvalues; the scan is exhaustive (no manual
/// candidate list).
/// </summary>
public class Y_M_014_Tests : ResearchTestBase
{
    private const int K = 6;
    private const int Nmin = 16;
    private const int Nmax = 512;

    public Y_M_014_Tests(ITestOutputHelper output) : base(output) { }

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

    /// <summary>Is n an octave rung of seed 3 (n = 3·2^k)?</summary>
    private static bool IsOctaveRung(int n)
    {
        if (n % 3 != 0) return false;
        int m = n / 3;
        while (m > 1 && m % 2 == 0) m /= 2;
        return m == 1;
    }

    /// <summary>
    /// Canonical composite score: A = complete pairing (0 unpaired); B = 3-family window
    /// (family count 3 ∧ span &lt; 8); C = 6-divisibility; D = octave rung n = 3·2^k.
    /// </summary>
    private static int Score(int n)
    {
        int s = 0;
        if (UnpairedCount(n) == 0) s++;                  // A
        if (FamilyCount(n) == 3 && Span(n) < 8.0) s++;   // B
        if (n % 6 == 0) s++;                             // C
        if (IsOctaveRung(n)) s++;                        // D
        return s;
    }

    /// <summary>Multiplicity pattern of the distinct positive eigenvalues of C_n(±1..±6).</summary>
    private static (int distinct, SortedDictionary<int, int> hist) MultiplicityPattern(int n)
    {
        var evals = new List<double>();
        for (int k = 1; k < n; k++) evals.Add(Math.Round(Lambda(k, n), 9));
        var groups = evals.GroupBy(x => x).Select(g => g.Count()).ToList();
        var hist = new SortedDictionary<int, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (int c in groups) hist[c] = hist.GetValueOrDefault(c) + 1;
        return (groups.Count, hist);
    }

    /// <summary>MemberData: every N in [16,512] — the exhaustive, non-manual candidate set.</summary>
    public static IEnumerable<object[]> AllRingSizes()
    {
        for (int n = Nmin; n <= Nmax; n++) yield return new object[] { n };
    }

    // ── [Required] Y_M_014_ExhaustiveRange ──────────────────

    /// <summary>
    /// Theory over ALL N = 16..512: every ring is evaluated (no manual candidate
    /// selection). Score ∈ [0,4] for every N.
    /// </summary>
    [Theory]
    [MemberData(nameof(AllRingSizes))]
    public void Y_M_014_ExhaustiveRange(int n)
    {
        int score = Score(n);
        Assert.InRange(score, 0, 4);
        // every ring in the closed interval is reachable & scored
        Assert.True(n >= Nmin && n <= Nmax);
    }

    // ── [Required] Y_M_014_UniqueTop ────────────────────────

    /// <summary>
    /// N = 96 is the UNIQUE Score-4 maximizer over the full range: exhaustive scan with
    /// no manual candidate list, max score 4 attained only at N = 96.
    /// </summary>
    [Fact]
    public void Y_M_014_UniqueTop()
    {
        var best = new List<int>();
        int maxScore = -1;
        for (int n = Nmin; n <= Nmax; n++)
        {
            int s = Score(n);
            if (s > maxScore) { maxScore = s; best.Clear(); best.Add(n); }
            else if (s == maxScore) { best.Add(n); }
        }
        Assert.Equal(4, maxScore);
        Assert.Equal(new[] { 96 }, best.ToArray());
    }

    // ── [Required] Y_M_014_RankTable ────────────────────────

    /// <summary>
    /// Score distribution over ALL 497 rings: {4:1, 3:14, 2:101, 1:255, 0:126}.
    /// </summary>
    [Fact]
    public void Y_M_014_RankTable()
    {
        var dist = new Dictionary<int, int>();
        int total = 0;
        for (int n = Nmin; n <= Nmax; n++)
        {
            int s = Score(n);
            dist[s] = dist.GetValueOrDefault(s) + 1;
            total++;
        }
        Assert.Equal(497, total);                    // all N = 16..512 evaluated
        Assert.Equal(1, dist[4]);
        Assert.Equal(14, dist[3]);
        Assert.Equal(101, dist[2]);
        Assert.Equal(255, dist[1]);
        Assert.Equal(126, dist[0]);
    }

    // ── [Required] Y_M_014_DropAnalysis ────────────────────

    /// <summary>
    /// Load-bearing criteria: removing the 3-family window (B) admits the 5 seed-3 rungs;
    /// removing the octave rung (D) admits the 11 zero-defect rings; removing pairing (A)
    /// or 6-divisibility (C) leaves N = 96 unique.
    /// </summary>
    [Fact]
    public void Y_M_014_DropAnalysis()
    {
        // Drop A (pairing): score = B + C + D
        var topDropA = TopSet(g => (FamilyCount(g) == 3 && Span(g) < 8 ? 1 : 0)
                                   + (g % 6 == 0 ? 1 : 0)
                                   + (IsOctaveRung(g) ? 1 : 0));
        Assert.Equal(new[] { 96 }, topDropA.ToArray());

        // Drop B (3-family window): score = A + C + D  →  the 5 seed-3 rungs
        var topDropB = TopSet(g => (UnpairedCount(g) == 0 ? 1 : 0)
                                   + (g % 6 == 0 ? 1 : 0)
                                   + (IsOctaveRung(g) ? 1 : 0));
        Assert.Equal(new[] { 24, 48, 96, 192, 384 }, topDropB.ToArray());

        // Drop C (6-divisibility): score = A + B + D
        var topDropC = TopSet(g => (UnpairedCount(g) == 0 ? 1 : 0)
                                   + (FamilyCount(g) == 3 && Span(g) < 8 ? 1 : 0)
                                   + (IsOctaveRung(g) ? 1 : 0));
        Assert.Equal(new[] { 96 }, topDropC.ToArray());

        // Drop D (octave rung): score = A + B + C  →  the 11 zero-defect rings {60..120}
        var topDropD = TopSet(g => (UnpairedCount(g) == 0 ? 1 : 0)
                                   + (FamilyCount(g) == 3 && Span(g) < 8 ? 1 : 0)
                                   + (g % 6 == 0 ? 1 : 0));
        Assert.Equal(new[] { 60, 66, 72, 78, 84, 90, 96, 102, 108, 114, 120 }, topDropD.ToArray());
    }

    private static List<int> TopSet(Func<int, int> partial)
    {
        var top = new List<int>();
        int max = -1;
        for (int n = Nmin; n <= Nmax; n++)
        {
            int s = partial(n);
            if (s > max) { max = s; top.Clear(); top.Add(n); }
            else if (s == max) { top.Add(n); }
        }
        return top;
    }

    // ── [Required] Y_M_014_CanonicalRow ─────────────────────

    /// <summary>
    /// N = 96 canonical row: Score 4 (A B C D = 1 1 1 1); 3 families; 44 distinct
    /// eigenvalues; multiplicity pattern {2×42, 5×1, 6×1}; span 6.4025 < 8.
    /// </summary>
    [Fact]
    public void Y_M_014_CanonicalRow()
    {
        Assert.Equal(4, Score(96));
        Assert.Equal(0, UnpairedCount(96));
        Assert.Equal(3, FamilyCount(96));
        Assert.True(96 % 6 == 0);
        Assert.True(IsOctaveRung(96));
        Assert.True(Span(96) < 8.0);
        Assert.True(Math.Abs(Span(96) - 6.4025) < 0.001);

        var (distinct, hist) = MultiplicityPattern(96);
        Assert.Equal(44, distinct);
        Assert.Equal(42, hist[2]);   // 42 mirror doublets
        Assert.Equal(1, hist[5]);    // octave 5-fold λ=12
        Assert.Equal(1, hist[6]);    // octave 6-fold λ=14
        Assert.Equal(95, hist.Sum(kv => kv.Key * kv.Value)); // modes sum to N−1
    }

    // ── [Required] Y_M_014_Score3Sets ───────────────────────

    /// <summary>
    /// The Score-3 rings are exactly: seed-3 rungs at wrong family count (24, 48, 192,
    /// 384) plus the zero-defect rings {60..120} \ {96}. Total 14.
    /// </summary>
    [Fact]
    public void Y_M_014_Score3Sets()
    {
        var score3 = new List<int>();
        for (int n = Nmin; n <= Nmax; n++)
            if (Score(n) == 3) score3.Add(n);

        Assert.Equal(14, score3.Count);
        // rungs not in the 3-family window
        Assert.Equal(1, FamilyCount(24));
        Assert.Equal(2, FamilyCount(48));
        Assert.Equal(4, FamilyCount(192));
        Assert.Equal(5, FamilyCount(384));
        foreach (int n in new[] { 24, 48, 192, 384 }) Assert.Contains(n, score3);
        // zero-defect rings (A ∧ B ∧ C) except 96
        foreach (int n in new[] { 60, 66, 72, 78, 84, 90, 102, 108, 114, 120 })
            Assert.Contains(n, score3);
        Assert.DoesNotContain(96, score3); // 96 is Score-4
    }

    // ── [Required] Y_M_014_Run ───────────────────────────────

    [Fact]
    public void Y_M_014_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_014 — Automatic Ring-Size Rank Scan Audit");

        sb.AppendLine("Question: which ring size does the D96 attractor occupy when every");
        sb.AppendLine("N in [16,512] is evaluated automatically, with no manual candidate");
        sb.AppendLine("selection?");
        sb.AppendLine();

        sb.AppendLine("[1] Score(N) = [0 unpaired] + [3 families ∧ span < 8] + [6|N]");
        sb.AppendLine("    + [N = 3·2^k]; evaluated for ALL N = 16..512 (497 rings).");
        sb.AppendLine();

        sb.AppendLine("[2] Score distribution: {4:1, 3:14, 2:101, 1:255, 0:126}.");
        sb.AppendLine();

        sb.AppendLine("[3] Unique top: N = 96 is the ONLY Score-4 ring in [16,512]");
        sb.AppendLine("    (3 families, span 6.403 < 8, 0 unpaired, 6|96, 96 = 3·32).");
        sb.AppendLine();

        sb.AppendLine("[4] Score-3 sets: seed-3 rungs at wrong family count (24, 48, 192,");
        sb.AppendLine("    384) + the D_029 zero-defect rings {60..120} \\ {96}.");
        sb.AppendLine();

        sb.AppendLine("[5] Drop analysis: removing the 3-family window admits the 5 rungs;");
        sb.AppendLine("    removing the octave rung admits the 11 zero-defect rings;");
        sb.AppendLine("    removing pairing or 6-divisibility leaves 96 unique.");
        sb.AppendLine();

        sb.AppendLine("Verdict: N = 96 is the unique global maximizer of the canonical");
        sb.AppendLine("score over ALL N in [16,512], reproduced WITHOUT any manual");
        sb.AppendLine("candidate selection — confirming D_029/D_030/D_031. No new");
        sb.AppendLine("primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
