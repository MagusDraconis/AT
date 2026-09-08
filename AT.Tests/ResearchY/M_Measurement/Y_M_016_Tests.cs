using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_016 — Criterion-Independence Audit test suite (Y_M_016_Tests.cs).
///
/// Question: are the four M_014 score criteria independent over N = 16..512?
///   A = 0 unpaired modes   B = 3 families ∧ span &lt; 8   C = 6|N   D = N = 3·2^k
/// Metrics over the 497 rings: marginal entropy, joint entropy, total correlation,
/// pairwise mutual information, phi (Pearson-on-bits) correlation matrix, PCA on the
/// standardized 4-bit criteria, conditional entropy H(X_i | others) and its unique
/// fraction, and per-criterion predictability from the other three (mode within the
/// other-3 pattern vs global majority baseline).
///
/// Verdict tested: PARTIALLY REDUNDANT. The four criteria are NOT independent — there is
/// a deterministic inclusion chain D ⊆ C ⊆ A (every rung 3·2^k is 6-divisible and
/// zero-unpaired; every 6-divisible ring in [16,512] is zero-unpaired), giving pairwise
/// phi up to 0.285 (A–C) and a total correlation of 0.118 bit (5.5% of ΣH). Yet B is
/// statistically near-independent (I(B;others) ≈ 0.0004 bit; unique fraction 0.999), and
/// no criterion is a deterministic function of the others (all unique fractions &gt; 0.67).
/// The criterion that actually selects N = 96 is the PAIR {B, D}: B is true exactly on
/// [60,120] (61 rings) and D is true on the seed-3 rung ladder {24,48,96,192,384};
/// their conjunction is the singleton {96}. A and C add no selection power for 96 once
/// {B,D} is imposed (they are implied at rungs). Confirms M_014/M_015 (B, D load-bearing;
/// A, C redundant for the 96 selection).
///
/// Deterministic: closed-form circulant eigenvalues; exhaustive scan over N = 16..512.
/// </summary>
public class Y_M_016_Tests : ResearchTestBase
{
    private const int K = 6;
    private const int Nmin = 16;
    private const int Nmax = 512;

    public Y_M_016_Tests(ITestOutputHelper output) : base(output) { }

    // ── ring-spectrum primitives (identical to M_014/M_015) ──

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

    private static bool IsOctaveRung(int n)
    {
        if (n % 3 != 0) return false;
        int m = n / 3;
        while (m > 1 && m % 2 == 0) m /= 2;
        return m == 1;
    }

    // ── criteria bits over the full population ──

    /// <summary>Bit of criterion i for ring n. i: 0=A,1=B,2=C,3=D.</summary>
    private static bool Bit(int i, int n) => i switch
    {
        0 => UnpairedCount(n) == 0,
        1 => FamilyCount(n) == 3 && Span(n) < 8.0,
        2 => n % 6 == 0,
        _ => IsOctaveRung(n),
    };

    private static List<int> RingList() => Enumerable.Range(Nmin, Nmax - Nmin + 1).ToList();

    private static void Close(double expected, double actual, double tol)
        => Assert.True(Math.Abs(expected - actual) <= tol, $"expected {expected}, got {actual}");

    private static double Log2(double x) => Math.Log2(x);

    private static double H(double p)
    {
        if (p <= 0 || p >= 1) return 0;
        return -p * Log2(p) - (1 - p) * Log2(1 - p);
    }

    /// <summary>Binary-entropy of criterion i over the population.</summary>
    private static double MargEntropy(int i, IReadOnlyList<int> rings)
    {
        int ones = rings.Count(n => Bit(i, n));
        return H((double)ones / rings.Count);
    }

    /// <summary>Joint entropy over the 4-bit signature.</summary>
    private static double JointEntropy(IReadOnlyList<int> rings)
    {
        var hist = new Dictionary<(bool, bool, bool, bool), int>();
        foreach (int n in rings)
        {
            var key = (Bit(0, n), Bit(1, n), Bit(2, n), Bit(3, n));
            hist[key] = hist.GetValueOrDefault(key) + 1;
        }
        double h = 0;
        foreach (int c in hist.Values)
        {
            double p = (double)c / rings.Count;
            h -= p * Log2(p);
        }
        return h;
    }

    /// <summary>Pairwise mutual information I(X_i ; X_j) in bits.</summary>
    private static double MutualInfo(int i, int j, IReadOnlyList<int> rings)
    {
        double mi = 0;
        var hist = new Dictionary<(bool, bool), int>();
        foreach (int n in rings) hist[(Bit(i, n), Bit(j, n))] = hist.GetValueOrDefault((Bit(i, n), Bit(j, n))) + 1;
        foreach (var ((a, b), c) in hist)
        {
            double p = (double)c / rings.Count;
            double pa = rings.Count(n => Bit(i, n) == a) / (double)rings.Count;
            double pb = rings.Count(n => Bit(j, n) == b) / (double)rings.Count;
            if (p > 0) mi += p * Log2(p / (pa * pb));
        }
        return mi;
    }

    /// <summary>Phi (Pearson on bits) for a criterion pair.</summary>
    private static double Phi(int i, int j, IReadOnlyList<int> rings)
    {
        int n11 = rings.Count(n => Bit(i, n) && Bit(j, n));
        double p1 = (double)rings.Count(n => Bit(i, n)) / rings.Count;
        double q1 = (double)rings.Count(n => Bit(j, n)) / rings.Count;
        double p00 = 1 - p1 - q1 + (double)n11 / rings.Count;
        double num = (double)n11 / rings.Count - p1 * q1;
        double den = Math.Sqrt(p1 * (1 - p1) * q1 * (1 - q1));
        return den == 0 ? 0 : num / den;
    }

    /// <summary>Conditional entropy H(X_i | X_others) in bits.</summary>
    private static double CondEntropy(int i, IReadOnlyList<int> rings)
    {
        int[] others = Enumerable.Range(0, 4).Where(x => x != i).ToArray();
        var groups = new Dictionary<(bool, bool, bool), List<int>>();
        foreach (int n in rings)
        {
            var key = (Bit(others[0], n), Bit(others[1], n), Bit(others[2], n));
            if (!groups.TryGetValue(key, out var list)) groups[key] = list = new List<int>();
            list.Add(n);
        }
        double h = 0;
        foreach (var (_, list) in groups)
        {
            double p1 = list.Count(n => Bit(i, n)) / (double)list.Count;
            h += (double)list.Count / rings.Count * H(p1);
        }
        return h;
    }

    /// <summary>Predict criterion i from the other three (mode within other-3 pattern).</summary>
    private static double PredictAccuracy(int i, IReadOnlyList<int> rings)
    {
        int[] others = Enumerable.Range(0, 4).Where(x => x != i).ToArray();
        var groups = new Dictionary<(bool, bool, bool), List<int>>();
        foreach (int n in rings)
        {
            var key = (Bit(others[0], n), Bit(others[1], n), Bit(others[2], n));
            if (!groups.TryGetValue(key, out var list)) groups[key] = list = new List<int>();
            list.Add(n);
        }
        int correct = 0;
        foreach (var (_, list) in groups)
        {
            int ones = list.Count(n => Bit(i, n));
            bool majority = ones >= list.Count - ones; // tie → true
            correct += list.Count(n => Bit(i, n) == majority);
        }
        return (double)correct / rings.Count;
    }

    /// <summary>Global-majority baseline accuracy for criterion i.</summary>
    private static double BaselineAccuracy(int i, IReadOnlyList<int> rings)
    {
        int ones = rings.Count(n => Bit(i, n));
        return Math.Max(ones, rings.Count - ones) / (double)rings.Count;
    }

    // ── PCA: eigen decomposition of the 4×4 symmetric correlation matrix ──

    private static (double[] eigenvalues, double[] explained) PcaOnCorrelation(IReadOnlyList<int> rings)
    {
        var m = new double[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                m[i, j] = i == j ? 1.0 : Phi(i, j, rings);
        double[] eigs = JacobiEigenvalues(m);
        double total = eigs.Sum();
        return (eigs, eigs.Select(e => e / total).ToArray());
    }

    private static double[] JacobiEigenvalues(double[,] a)
    {
        int n = a.GetLength(0);
        double[] v = new double[n];
        var mat = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                mat[i, j] = a[i, j];
        const int sweeps = 100;
        for (int s = 0; s < sweeps; s++)
        {
            double off = 0;
            for (int p = 0; p < n; p++)
                for (int q = p + 1; q < n; q++)
                    off += mat[p, q] * mat[p, q];
            if (off < 1e-18) break;
            for (int p = 0; p < n; p++)
            {
                for (int q = p + 1; q < n; q++)
                {
                    double apq = mat[p, q];
                    if (Math.Abs(apq) < 1e-15) continue;
                    double app = mat[p, p], aqq = mat[q, q];
                    double theta = (aqq - app) / (2 * apq);
                    double t = Math.Abs(theta) < 1e-300
                        ? 1.0
                        : Math.Sign(theta) / (Math.Abs(theta) + Math.Sqrt(theta * theta + 1));
                    double c = 1 / Math.Sqrt(t * t + 1);
                    double sRot = t * c;
                    for (int k = 0; k < n; k++)
                    {
                        if (k == p || k == q) continue;
                        double akp = mat[k, p], akq = mat[k, q];
                        mat[k, p] = c * akp - sRot * akq;
                        mat[p, k] = mat[k, p];
                        mat[k, q] = sRot * akp + c * akq;
                        mat[q, k] = mat[k, q];
                    }
                    mat[p, p] = c * c * app - 2 * sRot * c * apq + sRot * sRot * aqq;
                    mat[q, q] = sRot * sRot * app + 2 * sRot * c * apq + c * c * aqq;
                    mat[p, q] = 0; mat[q, p] = 0;
                }
            }
        }
        for (int i = 0; i < n; i++) v[i] = mat[i, i];
        Array.Sort(v);
        Array.Reverse(v);
        return v;
    }

    // ── [Required] Y_M_016_CriterionBits ───────────────────

    /// <summary>
    /// Over the full population N = 16..512 (497 rings), the four criteria hold on:
    /// A = 354 rings, B = 61 rings, C = 83 rings, D = 5 rings.
    /// </summary>
    [Fact]
    public void Y_M_016_CriterionBits()
    {
        var rings = RingList();
        Assert.Equal(497, rings.Count);
        Assert.Equal(354, rings.Count(n => Bit(0, n)));
        Assert.Equal(61, rings.Count(n => Bit(1, n)));
        Assert.Equal(83, rings.Count(n => Bit(2, n)));
        Assert.Equal(5, rings.Count(n => Bit(3, n)));
        Assert.True(Bit(0, 96) && Bit(1, 96) && Bit(2, 96) && Bit(3, 96));
    }

    // ── [Required] Y_M_016_Entropies ───────────────────────

    /// <summary>
    /// Marginal entropies, joint entropy, and total correlation over N = 16..512.
    /// ΣH = 2.135 bit, H_joint = 2.017 bit, total correlation = 0.118 bit = 5.5% of ΣH.
    /// </summary>
    [Fact]
    public void Y_M_016_Entropies()
    {
        var rings = RingList();
        double hA = MargEntropy(0, rings), hB = MargEntropy(1, rings);
        double hC = MargEntropy(2, rings), hD = MargEntropy(3, rings);
        double hJoint = JointEntropy(rings);
        double sum = hA + hB + hC + hD;
        double tc = sum - hJoint;

        Close(0.8658, hA, 5e-4);
        Close(0.5372, hB, 5e-4);
        Close(0.6508, hC, 5e-4);
        Close(0.0812, hD, 5e-4);
        Close(2.1349, sum, 5e-4);
        Close(2.017, hJoint, 5e-4);
        Close(0.1179, tc, 5e-4);
        Assert.True(tc / sum < 0.10, "total correlation must be a small fraction of ΣH");
    }

    // ── [Required] Y_M_016_MutualInformation ───────────────

    /// <summary>
    /// Pairwise mutual information (bits) over N = 16..512: the only non-negligible pairs
    /// are A–C (0.0912) and C–D (0.0263); B is nearly independent of every other criterion.
    /// </summary>
    [Fact]
    public void Y_M_016_MutualInformation()
    {
        var rings = RingList();
        Close(0.0000, MutualInfo(0, 1, rings), 5e-4);
        Close(0.0912, MutualInfo(0, 2, rings), 1e-3);
        Close(0.0050, MutualInfo(0, 3, rings), 1e-3);
        Close(0.0001, MutualInfo(1, 2, rings), 5e-4);
        Close(0.0004, MutualInfo(1, 3, rings), 1e-3);
        Close(0.0263, MutualInfo(2, 3, rings), 1e-3);
    }

    // ── [Required] Y_M_016_CorrelationMatrix ───────────────

    /// <summary>
    /// Phi (Pearson-on-bits) correlation matrix over N = 16..512. Largest: A–C = 0.285,
    /// C–D = 0.225. B is weakly correlated with all others (≤ 0.024).
    /// </summary>
    [Fact]
    public void Y_M_016_CorrelationMatrix()
    {
        var rings = RingList();
        Close(0.007, Phi(0, 1, rings), 5e-3);
        Close(0.285, Phi(0, 2, rings), 5e-3);
        Close(0.064, Phi(0, 3, rings), 5e-3);
        Close(0.013, Phi(1, 2, rings), 5e-3);
        Close(0.024, Phi(1, 3, rings), 5e-3);
        Close(0.225, Phi(2, 3, rings), 5e-3);
    }

    // ── [Required] Y_M_016_NestingChain ────────────────────

    /// <summary>
    /// Deterministic inclusion chain D ⊆ C ⊆ A over N = 16..512: every ring with D=1 has
    /// C=1 and A=1; every ring with C=1 has A=1. (Conjunction sizes equal the subset size.)
    /// </summary>
    [Fact]
    public void Y_M_016_NestingChain()
    {
        var rings = RingList();
        // D ⊆ A : every rung has zero unpaired modes
        Assert.Equal(5, rings.Count(n => Bit(3, n) && Bit(0, n)));
        // D ⊆ C : every rung is 6-divisible
        Assert.Equal(5, rings.Count(n => Bit(3, n) && Bit(2, n)));
        // C ⊆ A : every 6-divisible ring in [16,512] is zero-unpaired
        Assert.Equal(83, rings.Count(n => Bit(2, n) && Bit(0, n)));
        // B ∧ D = {96}
        var bd = rings.Where(n => Bit(1, n) && Bit(3, n)).ToList();
        Assert.Equal(new[] { 96 }, bd);
    }

    // ── [Required] Y_M_016_UniqueInformation ───────────────

    /// <summary>
    /// Unique information content of each criterion = H(X_i | others). B carries ~100% of
    /// its entropy uniquely; A 89.5%; C 82.7%; D 67.2% (D is most shared, but no criterion
    /// is a deterministic function of the others).
    /// </summary>
    [Fact]
    public void Y_M_016_UniqueInformation()
    {
        var rings = RingList();
        double[] hMarg = { MargEntropy(0, rings), MargEntropy(1, rings), MargEntropy(2, rings), MargEntropy(3, rings) };
        double[] hCond = { CondEntropy(0, rings), CondEntropy(1, rings), CondEntropy(2, rings), CondEntropy(3, rings) };
        double[] uniqueFrac = hCond.Select((hc, i) => hc / hMarg[i]).ToArray();

        Close(0.7746, hCond[0], 1e-3);
        Close(0.5368, hCond[1], 1e-3);
        Close(0.5382, hCond[2], 1e-3);
        Close(0.0546, hCond[3], 1e-3);

        Close(0.895, uniqueFrac[0], 5e-3);
        Close(0.999, uniqueFrac[1], 5e-3);
        Close(0.827, uniqueFrac[2], 5e-3);
        Close(0.672, uniqueFrac[3], 5e-3);

        Assert.All(uniqueFrac, u => Assert.True(u > 0.65, "every criterion retains unique information"));
    }

    // ── [Required] Y_M_016_Predictability ──────────────────

    /// <summary>
    /// Predicting each criterion from the other three (mode within the other-3 pattern):
    /// A, B, D give zero gain over the global-majority baseline (B is unpredictable from
    /// the others); C gains +1.0% (0.8330 → 0.8431), the only criterion the others help on.
    /// </summary>
    [Fact]
    public void Y_M_016_Predictability()
    {
        var rings = RingList();
        for (int i = 0; i < 4; i++)
        {
            double pred = PredictAccuracy(i, rings);
            double baseAcc = BaselineAccuracy(i, rings);
            Assert.True(pred >= baseAcc - 1e-9, "pattern predictor cannot underperform the baseline");
            if (i == 2) // C is the only criterion the others help predict
                Assert.True(pred - baseAcc > 0.005, "C must gain from the other criteria");
            else
                Assert.True(pred - baseAcc < 0.005, $"criterion {i} must not gain from the others");
        }
    }

    // ── [Required] Y_M_016_Pca ─────────────────────────────

    /// <summary>
    /// PCA on the standardized 4-bit criteria (correlation matrix): eigenvalues
    /// [1.397, 1.001, 0.935, 0.667] — no dominant factor; participation ratio ≈ 3.74/4
    /// confirms near-full effective dimensionality (criteria are far from redundant as a set).
    /// </summary>
    [Fact]
    public void Y_M_016_Pca()
    {
        var rings = RingList();
        var (eigs, explained) = PcaOnCorrelation(rings);
        Assert.Equal(4, eigs.Length);
        Close(1.397, eigs[0], 5e-3);
        Close(1.001, eigs[1], 5e-3);
        Close(0.935, eigs[2], 5e-3);
        Close(0.667, eigs[3], 5e-3);
        // participation ratio = (Σλ)² / Σλ²
        double pr = Math.Pow(eigs.Sum(), 2) / eigs.Sum(e => e * e);
        Close(3.744, pr, 2e-2);
        Assert.True(pr > 3.5, "effective dimensionality must be near 4");
        Assert.True(explained[0] < 0.5, "no single principal component dominates");
    }

    // ── [Required] Y_M_016_SelectorOf96 ────────────────────

    /// <summary>
    /// Which criterion actually selects N = 96? The PAIR {B, D}: B is true exactly on the
    /// 3-family window [60,120] (61 rings) and D on the seed-3 rung ladder {24,48,96,192,
    /// 384}; their conjunction is the singleton {96}. No single criterion selects 96, and
    /// A or C adds nothing once {B,D} is imposed.
    /// </summary>
    [Fact]
    public void Y_M_016_SelectorOf96()
    {
        var rings = RingList();

        // single criteria: none isolates 96
        for (int i = 0; i < 4; i++)
            Assert.True(rings.Count(n => Bit(i, n)) > 1, "no single criterion isolates 96");

        // the B window is [60,120]
        var bSet = rings.Where(n => Bit(1, n)).ToList();
        Assert.Equal(61, bSet.Count);
        Assert.Equal(60, bSet.Min());
        Assert.Equal(120, bSet.Max());
        Assert.Equal(61, bSet.Count(n => n >= 60 && n <= 120));

        // the D ladder
        var dSet = rings.Where(n => Bit(3, n)).ToList();
        Assert.Equal(new[] { 24, 48, 96, 192, 384 }, dSet);

        // B ∧ D = {96}
        var bd = rings.Where(n => Bit(1, n) && Bit(3, n)).ToList();
        Assert.Equal(new[] { 96 }, bd);

        // adding A or C to {B,D} does not change the selection
        var bda = rings.Where(n => Bit(1, n) && Bit(3, n) && Bit(0, n)).ToList();
        var bdc = rings.Where(n => Bit(1, n) && Bit(3, n) && Bit(2, n)).ToList();
        Assert.Equal(new[] { 96 }, bda);
        Assert.Equal(new[] { 96 }, bdc);
    }

    // ── [Required] Y_M_016_Run ─────────────────────────────

    [Fact]
    public void Y_M_016_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var rings = RingList();
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_016 — Criterion-Independence Audit");

        sb.AppendLine("Question: are the four M_014 score criteria independent over N = 16..512?");
        sb.AppendLine("  A = 0 unpaired modes   B = 3 families and span < 8   C = 6|N   D = N = 3·2^k");
        sb.AppendLine();

        sb.AppendLine("[1] Population: all 497 rings N = 16..512. Criteria hold on:");
        sb.AppendLine($"    A on 354 rings, B on 61, C on 83, D on 5.  96 satisfies all four.");
        sb.AppendLine();

        sb.AppendLine("[2] Entropies (bits): H(A)=0.866 H(B)=0.537 H(C)=0.651 H(D)=0.081;");
        sb.AppendLine("    sum H = 2.135; joint H = 2.017; total correlation = 0.118 bit");
        sb.AppendLine("    (5.5% of the entropy budget).");
        sb.AppendLine();

        sb.AppendLine("[3] Pairwise mutual information (bits): A-C = 0.0912 (largest);");
        sb.AppendLine("    C-D = 0.0263; A-D = 0.0050; B-* <= 0.0004 (B near-independent).");
        sb.AppendLine();

        sb.AppendLine("[4] Phi correlation matrix: A-C = 0.285, C-D = 0.225, A-D = 0.064,");
        sb.AppendLine("    B-* <= 0.024. Deterministic inclusion chain: D subset C subset A");
        sb.AppendLine("    (every rung is 6-divisible and zero-unpaired; every 6-divisible ring");
        sb.AppendLine("    in [16,512] is zero-unpaired).");
        sb.AppendLine();

        sb.AppendLine("[5] Unique information H(X_i | others) / H(X_i):");
        sb.AppendLine("    A: 0.775 / 0.866 = 0.895    B: 0.537 / 0.537 = 0.999");
        sb.AppendLine("    C: 0.538 / 0.651 = 0.827    D: 0.055 / 0.081 = 0.672");
        sb.AppendLine("    No criterion is a deterministic function of the others.");
        sb.AppendLine();

        sb.AppendLine("[6] Predictability from the other three (mode within other-3 pattern):");
        sb.AppendLine("    A: 0.7123 (baseline 0.7123, gain 0)   B: 0.8773 (0.8773, 0)");
        sb.AppendLine("    C: 0.8431 (0.8330, +1.0%)             D: 0.9899 (0.9899, 0)");
        sb.AppendLine("    C is the only criterion the others help predict.");
        sb.AppendLine();

        sb.AppendLine("[7] PCA on the standardized criteria (correlation matrix): eigenvalues");
        sb.AppendLine("    [1.397, 1.001, 0.935, 0.667]; participation ratio 3.744/4; no");
        sb.AppendLine("    dominant factor -> the four criteria span ~4 effective dimensions.");
        sb.AppendLine();

        sb.AppendLine("[8] Which criterion selects N = 96? The PAIR {B, D}.");
        sb.AppendLine("    B is true exactly on the 3-family window [60,120] (61 rings); D on");
        sb.AppendLine("    the seed-3 rung ladder {24,48,96,192,384}. Their conjunction is the");
        sb.AppendLine("    singleton {96}. No single criterion isolates 96; A and C add nothing");
        sb.AppendLine("    once {B,D} is imposed (they are implied at rungs) - confirming the");
        sb.AppendLine("    M_014/M_015 finding that B and D are load-bearing, A and C redundant");
        sb.AppendLine("    for the 96 selection.");
        sb.AppendLine();

        sb.AppendLine("Verdict: PARTIALLY REDUNDANT - the criteria are not independent (the");
        sb.AppendLine("D subset C subset A inclusion chain is deterministic and pairwise");
        sb.AppendLine("correlations reach phi = 0.285), yet no criterion is redundant given the");
        sb.AppendLine("others (all unique fractions > 0.67, PCA effective dims 3.74/4, B nearly");
        sb.AppendLine("independent). N = 96 is selected by {B,D}; A and C are redundant for that");
        sb.AppendLine("selection. No reclassification of M_014/M_015; no new primitive; canonical");
        sb.AppendLine("AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
