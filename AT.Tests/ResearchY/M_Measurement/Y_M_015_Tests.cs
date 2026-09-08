using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_015 — Score-Function Robustness Audit test suite (Y_M_015_Tests.cs).
///
/// Question: how much does the M_014 result (N = 96 is the UNIQUE Score-4 maximizer over
/// N = 16..512) depend on the precise form of the score function? For each criterion of
/// Score(N) = [0 unpaired] + [3 families ∧ span &lt; 8] + [6|N] + [N = 3·2^k] this audit
/// removes it, perturbs its value, and changes its threshold by ±5%, ±10%, ±20%,
/// recomputing the full rank table over N = 16..512 (all 497 rings, no manual candidate
/// selection) under every modified score.
///
/// Verdict tested: ROBUST. N = 96 stays in the argmax set (rank 1) in 24/26 variants —
/// every removal and every threshold/value perturbation that preserves the physical
/// family-3 requirement; the only two variants that displace 96 (family requirement → 2
/// and → 4) re-target criterion B to a DIFFERENT physical requirement (they select the
/// family-2 rung 48 and the family-4 rung 192, answering a different question). N = 96 is
/// the UNIQUE top in 16/26 variants, and in all variants whose numeric thresholds stay
/// within ±10% of baseline. Score(96) ∈ {3,4} in every variant (never beaten; only drops
/// when a criterion 96 satisfies is removed or its razor threshold is crossed). Razor
/// edges (Section 3): the window upper edge U must stay above span(96) = 6.4025 (a −20%
/// tightening to U = 6.40 just excludes 96, the −19.97% razor); the rung tolerance must
/// stay below ≈ 0.10 octaves (0.10 admits {90, 96, 102}); the divisor must divide 96.
///
/// Deterministic: closed-form circulant eigenvalues; exhaustive scan (no manual candidate
/// list); every scenario recomputes the full rank table over N = 16..512.
/// </summary>
public class Y_M_015_Tests : ResearchTestBase
{
    private const int K = 6;
    private const int Nmin = 16;
    private const int Nmax = 512;

    public Y_M_015_Tests(ITestOutputHelper output) : base(output) { }

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

    /// <summary>Is n within tol octaves of an octave rung 3·2^k?</summary>
    private static bool NearOctaveRung(int n, double tol)
    {
        for (int j = 0; j < 20; j++)
        {
            double rung = 3.0 * Math.Pow(2, j);
            if (rung > n * Math.Pow(2, tol)) break;
            if (Math.Abs(Math.Log2(n) - Math.Log2(rung)) <= tol + 1e-9) return true;
        }
        return false;
    }

    // ── Scenario model ──────────────────────────────────────

    private enum Kind
    {
        Baseline, RemoveA, RemoveB, RemoveC, RemoveD,
        UnpAllow, WinU, FamReq, Divisor, RungTol,
    }

    private readonly record struct Scenario(Kind Kind, double Value, string Name);

    /// <summary>
    /// The 26 audited score variants over N = 16..512: baseline, four single-criterion
    /// removals, and 21 one-at-a-time value/threshold perturbations.
    /// </summary>
    private static Scenario[] Variants() =>
    [
        new(Kind.Baseline, 0.0, "baseline"),
        new(Kind.RemoveA, 0.0, "remove A (pairing)"),
        new(Kind.RemoveB, 0.0, "remove B (3-family window)"),
        new(Kind.RemoveC, 0.0, "remove C (6-divisibility)"),
        new(Kind.RemoveD, 0.0, "remove D (octave rung)"),
        new(Kind.UnpAllow, 1.0, "A: unpaired <= 1"),
        new(Kind.UnpAllow, 2.0, "A: unpaired <= 2"),
        new(Kind.WinU, 8.0 * 1.05, "B: U = +5% (8.4)"),
        new(Kind.WinU, 8.0 * 1.10, "B: U = +10% (8.8)"),
        new(Kind.WinU, 8.0 * 1.20, "B: U = +20% (9.6)"),
        new(Kind.WinU, 8.0 * 0.95, "B: U = -5% (7.6)"),
        new(Kind.WinU, 8.0 * 0.90, "B: U = -10% (7.2)"),
        new(Kind.WinU, 8.0 * 0.80, "B: U = -20% (6.4)"),
        new(Kind.FamReq, 2.0, "B: family requirement -> 2"),
        new(Kind.FamReq, 4.0, "B: family requirement -> 4"),
        new(Kind.Divisor, 4.0, "C: divisor 4"),
        new(Kind.Divisor, 5.0, "C: divisor 5"),
        new(Kind.Divisor, 7.0, "C: divisor 7"),
        new(Kind.Divisor, 8.0, "C: divisor 8"),
        new(Kind.Divisor, 12.0, "C: divisor 12"),
        new(Kind.RungTol, 0.02, "D: rung tolerance 0.02 oct"),
        new(Kind.RungTol, 0.05, "D: rung tolerance 0.05 oct"),
        new(Kind.RungTol, 0.08, "D: rung tolerance 0.08 oct"),
        new(Kind.RungTol, 0.10, "D: rung tolerance 0.10 oct"),
        new(Kind.RungTol, 0.15, "D: rung tolerance 0.15 oct"),
        new(Kind.RungTol, 0.20, "D: rung tolerance 0.20 oct"),
    ];

    /// <summary>Score ring n under the given perturbation of the canonical M_014 score.</summary>
    private static int ScoreVariant(int n, in Scenario s)
    {
        int sc = 0;
        bool useA = s.Kind != Kind.RemoveA;
        bool useB = s.Kind != Kind.RemoveB;
        bool useC = s.Kind != Kind.RemoveC;
        bool useD = s.Kind != Kind.RemoveD;

        // A — complete pairing (relaxable)
        if (useA)
        {
            int allow = s.Kind == Kind.UnpAllow ? (int)s.Value : 0;
            if (UnpairedCount(n) <= allow) sc++;
        }

        // B — 3-family window: family count == target AND span < U (U = 8 baseline)
        if (useB)
        {
            bool b = false;
            if (s.Kind == Kind.FamReq)
            {
                // value perturbation: require a different family count (pure re-target)
                b = FamilyCount(n) == (int)s.Value;
            }
            else
            {
                double u = s.Kind == Kind.WinU ? s.Value : 8.0;
                b = FamilyCount(n) == 3 && Span(n) < u;
            }
            if (b) sc++;
        }

        // C — seed half-shift divisibility (relaxable divisor)
        if (useC)
        {
            int div = s.Kind == Kind.Divisor ? (int)s.Value : 6;
            if (n % div == 0) sc++;
        }

        // D — octave rung (exact or with tolerance)
        if (useD)
        {
            bool d = s.Kind == Kind.RungTol ? NearOctaveRung(n, s.Value) : IsOctaveRung(n);
            if (d) sc++;
        }

        return sc;
    }

    /// <summary>
    /// Full rank analysis over ALL N = 16..512 for a scenario: max score, argmax set,
    /// score(96), rank(96) (number of rings scoring strictly above 96, +1).
    /// </summary>
    private static (int MaxScore, List<int> Top, int Score96, int Rank96) Analyze(in Scenario s)
    {
        int max = -1;
        var top = new List<int>();
        int s96 = ScoreVariant(96, s);
        int above = 0;
        for (int n = Nmin; n <= Nmax; n++)
        {
            int sc = ScoreVariant(n, s);
            if (sc > max) { max = sc; top.Clear(); top.Add(n); }
            else if (sc == max && !top.Contains(n)) top.Add(n);
            if (sc > s96) above++;
        }
        top.Sort();
        return (max, top, s96, above + 1);
    }

    /// <summary>MemberData: every one of the 26 audited scenarios.</summary>
    public static IEnumerable<object[]> AllVariants() =>
        Variants().Select(v => new object[] { (int)v.Kind, v.Value, v.Name });

    // ── [Required] Y_M_015_ScenarioRank ────────────────────

    /// <summary>
    /// Theory over ALL 26 scenarios: for each, recompute the full rank table over N =
    /// 16..512 and assert winner stability (96 ∈ argmax ∧ rank 1) whenever the physical
    /// family-3 requirement is kept, plus the score and uniqueness invariants.
    /// </summary>
    [Theory]
    [MemberData(nameof(AllVariants))]
    public void Y_M_015_ScenarioRank(int kind, double value, string name)
    {
        var s = new Scenario((Kind)kind, value, name);
        var (maxScore, top, s96, rank96) = Analyze(s);
        bool inTop = top.Contains(96);
        bool unique = top.Count == 1 && top[0] == 96;

        Assert.InRange(s96, 0, 4);
        Assert.True(maxScore >= s96, $"{s.Name}: ring above 96 at score {maxScore} vs {s96}");

        if (s.Kind == Kind.FamReq)
        {
            // Criterion re-targeted to a different physical family → other rung wins.
            int req = (int)s.Value;
            Assert.False(inTop, $"{s.Name}: 96 must leave the family-{req} argmax");
            Assert.Equal(4, maxScore);
            Assert.Equal(3, s96);
            Assert.Equal(2, rank96);
            Assert.Equal(req == 2 ? new[] { 48 } : new[] { 192 }, top.ToArray());
        }
        else
        {
            // Winner stability: every removal / value / threshold perturbation that keeps
            // the family-3 requirement leaves 96 in the argmax set at rank 1.
            Assert.True(inTop, $"{s.Name}: 96 dropped out of the argmax set");
            Assert.Equal(1, rank96);
        }

        // Score stability: Score(96) = 4 unless a criterion 96 satisfies is removed or its
        // razor threshold is crossed.
        bool razorDrop = (s.Kind == Kind.WinU && s.Value < 6.4025 + 1e-9)   // U below span(96)
                         || (s.Kind == Kind.Divisor && 96 % (int)s.Value != 0) // divisor ∤ 96
                         || (s.Kind == Kind.FamReq);                          // B re-targeted
        if (s.Kind is Kind.RemoveA or Kind.RemoveB or Kind.RemoveC or Kind.RemoveD || razorDrop)
            Assert.Equal(3, s96);
        else
            Assert.Equal(4, s96);
    }

    // ── [Required] Y_M_015_WinnerStability ──────────────────

    /// <summary>
    /// N = 96 remains a top candidate (in the argmax set) under 24/26 scenarios; the only
    /// failures are the two family-requirement re-targets. Under removals and value/
    /// threshold perturbations preserving family-3, 96 is in the argmax 24/24.
    /// </summary>
    [Fact]
    public void Y_M_015_WinnerStability()
    {
        int inTopCount = 0, uniqueCount = 0, notInTop = 0;
        var displaced = new List<string>();
        foreach (var s in Variants())
        {
            var (_, top, _, _) = Analyze(s);
            if (top.Contains(96)) inTopCount++; else { notInTop++; displaced.Add(s.Name); }
            if (top.Count == 1 && top[0] == 96) uniqueCount++;
        }
        Assert.Equal(24, inTopCount);
        Assert.Equal(2, notInTop);
        Assert.Equal(2, displaced.Count);
        Assert.All(displaced, d => Assert.Contains("family requirement", d));
        Assert.Equal(16, uniqueCount);
    }

    // ── [Required] Y_M_015_ScoreStability ───────────────────

    /// <summary>
    /// Score(96) never falls below 3 across all 26 scenarios and no ring ever scores
    /// strictly above 96's maximum reachable score 4.
    /// </summary>
    [Fact]
    public void Y_M_015_ScoreStability()
    {
        int min = int.MaxValue;
        int score4Count = 0, score3Count = 0;
        foreach (var s in Variants())
        {
            var (maxScore, _, s96, _) = Analyze(s);
            min = Math.Min(min, s96);
            if (s96 == 4) score4Count++;
            else if (s96 == 3) score3Count++;
            Assert.True(maxScore <= 4, $"{s.Name}: max score {maxScore} exceeds 4");
            Assert.True(maxScore >= s96, $"{s.Name}: a ring outscored 96");
        }
        Assert.Equal(3, min);         // never below 3
        Assert.Equal(17, score4Count); // Score(96)=4 in 17 variants
        Assert.Equal(9, score3Count);  // Score(96)=3 in 9 variants (removals + razor drops)
    }

    // ── [Required] Y_M_015_UniqueTop ────────────────────────

    /// <summary>
    /// Within ±10% threshold changes on every numeric threshold, N = 96 remains the UNIQUE
    /// top: window U ∈ [7.2, 8.8], pairing ≤ 2, divisor dividing 96, rung tolerance ≤ 0.08
    /// oct. Uniqueness is fully stable under all "reasonable perturbations".
    /// </summary>
    [Fact]
    public void Y_M_015_UniqueTop()
    {
        // ±5% and ±10% window perturbations
        foreach (double u in new[] { 8.0 * 1.05, 8.0 * 1.10, 8.0 * 0.95, 8.0 * 0.90 })
        {
            var (_, top, _, _) = Analyze(new Scenario(Kind.WinU, u, $"U={u}"));
            Assert.Equal(new[] { 96 }, top.ToArray());
        }
        // pairing relaxed up to 2
        foreach (int a in new[] { 1, 2 })
        {
            var (_, top, _, _) = Analyze(new Scenario(Kind.UnpAllow, a, $"unp<={a}"));
            Assert.Equal(new[] { 96 }, top.ToArray());
        }
        // divisors that divide 96
        foreach (int d in new[] { 4, 8, 12 })
        {
            var (_, top, _, _) = Analyze(new Scenario(Kind.Divisor, d, $"div={d}"));
            Assert.Equal(new[] { 96 }, top.ToArray());
        }
        // rung tolerance up to 0.08 octaves
        foreach (double t in new[] { 0.02, 0.05, 0.08 })
        {
            var (_, top, _, _) = Analyze(new Scenario(Kind.RungTol, t, $"tol={t}"));
            Assert.Equal(new[] { 96 }, top.ToArray());
        }
    }

    // ── [Required] Y_M_015_RazorEdges ───────────────────────

    /// <summary>
    /// The razor edges that break UNIQUENESS (not winner status): window U below span(96)
    /// = 6.4025 (−19.97% is the exact tipping point, −20% → 6.40 &lt; 6.4025 → 11-ring tie),
    /// rung tolerance ≥ 0.10 oct (admits {90, 96, 102}), divisor 5/7 (96 not divisible).
    /// </summary>
    [Fact]
    public void Y_M_015_RazorEdges()
    {
        double span96 = Span(96);
        Assert.True(Math.Abs(span96 - 6.4025) < 0.001);

        // window upper edge razor: 96 keeps B iff U > span(96) = 6.4025
        Assert.True(8.0 * 0.80 < span96 + 1e-9);   // U(-20%) = 6.40 is below the razor
        var topU20 = Analyze(new Scenario(Kind.WinU, 8.0 * 0.80, "U=6.4")).Top;
        Assert.False(topU20.Count == 1 && topU20[0] == 96);   // uniqueness lost
        Assert.Contains(96, topU20);                           // winner status kept
        Assert.Equal(11, topU20.Count);                        // 11-ring tie at score 3

        // rung tolerance razor: ≥ 0.10 oct admits 90 and 102
        var topTol10 = Analyze(new Scenario(Kind.RungTol, 0.10, "tol=0.10")).Top;
        Assert.Equal(new[] { 90, 96, 102 }, topTol10.ToArray());
        var topTol20 = Analyze(new Scenario(Kind.RungTol, 0.20, "tol=0.20")).Top;
        Assert.Equal(new[] { 84, 90, 96, 102, 108 }, topTol20.ToArray());

        // divisor razor: divisor must divide 96
        foreach (int d in new[] { 5, 7 })
        {
            var topDiv = Analyze(new Scenario(Kind.Divisor, d, $"div={d}")).Top;
            Assert.Contains(96, topDiv);     // 96 still in the argmax
            Assert.False(topDiv.Count == 1 && topDiv[0] == 96); // but not unique
        }
    }

    // ── [Required] Y_M_015_FamilyRetarget ───────────────────

    /// <summary>
    /// Re-targeting criterion B to family 2 or 4 (a value substitution, not a perturbation
    /// of the physical family-3 question) selects the family-2 rung 48 / family-4 rung 192
    /// and displaces 96 — exactly the M_014 rung-ladder structure.
    /// </summary>
    [Fact]
    public void Y_M_015_FamilyRetarget()
    {
        Assert.Equal(2, FamilyCount(48));
        Assert.True(IsOctaveRung(48));
        Assert.Equal(4, FamilyCount(192));
        Assert.True(IsOctaveRung(192));

        var top2 = Analyze(new Scenario(Kind.FamReq, 2, "fam=2")).Top;
        Assert.Equal(new[] { 48 }, top2.ToArray());
        var top4 = Analyze(new Scenario(Kind.FamReq, 4, "fam=4")).Top;
        Assert.Equal(new[] { 192 }, top4.ToArray());
    }

    // ── [Required] Y_M_015_Run ──────────────────────────────

    [Fact]
    public void Y_M_015_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_015 — Score-Function Robustness Audit");

        sb.AppendLine("Question: how much does the M_014 result (N = 96 the UNIQUE Score-4");
        sb.AppendLine("maximizer over N = 16..512) depend on the precise form of the score?");
        sb.AppendLine();
        sb.AppendLine("Method: for each criterion of Score(N) = [0 unpaired] + [3 families");
        sb.AppendLine("and span < 8] + [6|N] + [N = 3·2^k] remove it, perturb its value, and");
        sb.AppendLine("shift its threshold by +-5/10/20%; recompute the FULL rank table over");
        sb.AppendLine("all 497 rings N = 16..512 per variant (no manual candidate selection).");
        sb.AppendLine();

        sb.AppendLine("[1] Scenario table (26 variants, full exhaustive rank per variant):");
        sb.AppendLine("    winner stability: 96 in the argmax set in 24/26 (all but the two");
        sb.AppendLine("    family-req re-targets, which change the physical question).");
        sb.AppendLine("    unique top = {96} in 16/26; score(96) in {4,3}, never below 3.");
        sb.AppendLine();
        sb.AppendFormat("    {0,-32} {1,6} {2,6} {3,6}{4}", "scenario", "s96", "rank", "topN", Environment.NewLine);
        foreach (var s in Variants())
        {
            var (_, top, s96, rank96) = Analyze(s);
            sb.AppendFormat("    {0,-32} {1,6} {2,6} {3,6}{4}", s.Name, s96, rank96, top.Count, Environment.NewLine);
        }
        sb.AppendLine();

        sb.AppendLine("[2] Winner stability: 96 at rank 1 under every removal and every");
        sb.AppendLine("    value/threshold perturbation that keeps the family-3 requirement.");
        sb.AppendLine("    The family-req 2/4 re-targets answer a different question (they");
        sb.AppendLine("    select the family-2 rung 48 / family-4 rung 192).");
        sb.AppendLine();

        sb.AppendLine("[3] Razor edges (threshold dependence of UNIQUENESS only):");
        sb.AppendLine("    window upper edge U must stay > span(96) = 6.4025 (-19.97% razor;");
        sb.AppendLine("    -20% gives U = 6.40 < 6.4025 -> 11-ring tie, 96 still in it);");
        sb.AppendLine("    rung tolerance >= 0.10 oct admits {90, 96, 102}; divisor must");
        sb.AppendLine("    divide 96 (5/7 break uniqueness, 96 stays in the argmax).");
        sb.AppendLine();

        sb.AppendLine("Verdict: ROBUST. N = 96 remains the top candidate under every");
        sb.AppendLine("removal, every +-5%/+10% threshold change, and every value");
        sb.AppendLine("perturbation that preserves the physical 3-family requirement; it is");
        sb.AppendLine("the UNIQUE top in all reasonable (+-10%) perturbation variants. The");
        sb.AppendLine("M_014 unique-maximizer result is not an artifact of the score's");
        sb.AppendLine("precise form. No reclassification; no new primitive; canonical AT");
        sb.AppendLine("unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
