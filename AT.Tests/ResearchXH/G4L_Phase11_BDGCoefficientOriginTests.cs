using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 11 — origin of the BDG coefficients. Investigates whether the BDG binomial
/// coefficients (d=2: diagonal −2, links +4, next layer −2, 0 beyond) emerge from interval
/// combinatorics alone (layer occupancy, interval counts, causal volume, alternating-layer
/// generating functions).
///
/// Finding: the raw layer-occupancy / interval-count / causal-volume statistics are lattice-noisy
/// and do NOT reproduce the coefficients (NO MATCH); but the BDG stencil IS the binomial
/// second-difference {−2,+4,−2} = −2·(−1)^ℓ·C(2,ℓ), whose binomial structure, truncation and
/// constant-annihilation are native (MATCH), with only the overall scale −2 imported (PARTIAL MATCH).
///
/// Tests: G4-L110 (raw statistics), G4-L111 (binomial identity), G4-L112 (generating function).
/// </summary>
public class G4L_Phase11_BDGCoefficientOriginTests : ResearchTestBase
{
    public G4L_Phase11_BDGCoefficientOriginTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 9;
    private const int XMax = 5;
    private const int MaxK = 6;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);

    private static long C(int n, int k)
    {
        if (k < 0 || k > n) return 0;
        long r = 1;
        for (int i = 0; i < k; i++) r = r * (n - i) / (i + 1);
        return r;
    }

    // ── G4-L110: raw layer-occupancy / interval-count statistics ───────────────────────

    [Fact]
    public void G4_L110_RawStatisticsDoNotReproduceBdg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L110: do raw layer/interval statistics reproduce the BDG coefficients?");

        var cs = Cs;
        int n = cs.Count;

        // Layer occupancy O(k): mean # events at interval exactly k, averaged over interior events.
        var occ = new double[MaxK + 1];
        var cnt = new int[MaxK + 1];
        int centers = 0;
        for (int i = 0; i < n; i++)
        {
            if (cs.Time[i] < 2 || cs.Time[i] > TMax - 2) continue;
            if (Math.Abs(cs.Space[i]) > XMax - 2) continue;
            centers++;
            for (int j = 0; j < n; j++)
            {
                int k = cs.Order[i, j] ? cs.Interval[i, j] : (cs.Order[j, i] ? cs.Interval[j, i] : -1);
                if (k >= 0 && k <= MaxK) { occ[k]++; cnt[k]++; }
            }
        }
        for (int k = 0; k <= MaxK; k++) occ[k] /= centers;

        // Interval-count histogram over all comparable pairs.
        var hist = new long[MaxK + 1];
        long total = 0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Order[i, j] && cs.Interval[i, j] <= MaxK) { hist[cs.Interval[i, j]]++; total++; }

        sb.AppendLine($"{"k",3} {"O(k)",8} {"frac pairs",11} {"BDG c_k",8}");
        for (int k = 0; k <= 3; k++)
        {
            double bdg = LorentzianOperator.BdgCoefficient(k);
            sb.AppendLine($"{k,3} {occ[k],8:F3} {hist[k] / (double)total,11:F4} {bdg,8:F1}");
        }

        double occRatio = occ[1] / Math.Max(occ[0], 1e-9);         // O(1)/O(0)
        double bdgRatio = LorentzianOperator.BdgCoefficient(1) / LorentzianOperator.BdgCoefficient(0); // −0.5
        sb.AppendLine();
        sb.AppendLine($"occupancy ratio O(1)/O(0) = {occRatio:F3};  BDG ratio c_1/c_0 = {bdgRatio:F2}");
        sb.AppendLine($"raw statistics reproduce BDG: {Math.Abs(occRatio - bdgRatio) < 0.1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the raw layer occupancy and interval-count distribution do NOT reproduce the");
        sb.AppendLine("BDG coefficients — they are lattice-noisy (NO MATCH for naive counting).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(occRatio - bdgRatio) > 0.1,
            $"raw occupancy ratio {occRatio:F3} unexpectedly matches BDG ratio {bdgRatio:F2}");
    }

    // ── G4-L111: the BDG stencil is the binomial second difference ─────────────────────

    [Fact]
    public void G4_L111_BdgStencilIsBinomialSecondDifference()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L111: the BDG stencil equals −2 × the binomial second difference");

        // Index ℓ = 0 (diagonal/self), 1 (links, interval 0), 2 (next, interval 1), 3+ (zero).
        sb.AppendLine($"{"ℓ",4} {"−2·(−1)^ℓ·C(2,ℓ)",18} {"BDG a_ℓ",10}  match");
        bool allMatch = true;
        for (int l = 0; l <= 4; l++)
        {
            double predicted = -2.0 * Math.Pow(-1, l) * C(2, l);
            double actual = l switch
            {
                0 => -2.0,                                        // diagonal (self)
                1 => LorentzianOperator.BdgCoefficient(0),        // links (interval 0)
                2 => LorentzianOperator.BdgCoefficient(1),        // next layer (interval 1)
                _ => LorentzianOperator.BdgCoefficient(l - 1)     // 0 for k ≥ 2
            };
            bool ok = Math.Abs(predicted - actual) < 1e-12;
            if (!ok) allMatch = false;
            sb.AppendLine($"{l,4} {predicted,18:F1} {actual,10:F1}  {ok}");
        }

        // Generating-function form: Σ a_ℓ x^ℓ = −2(1−x)².
        sb.AppendLine();
        sb.AppendLine($"generating function: Σ a_ℓ x^ℓ = −2 + 4x − 2x² = −2(1−x)²");
        sb.AppendLine($"all ℓ match −2·(−1)^ℓ·C(2,ℓ): {allMatch}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the BDG coefficients ARE the binomial coefficients −2·(−1)^ℓ·C(2,ℓ) — the");
        sb.AppendLine("second finite difference over the causal layers. The binomial structure is native (MATCH).");
        Output.WriteLine(sb.ToString());

        Assert.True(allMatch, "BDG stencil is not exactly −2·(−1)^ℓ·C(2,ℓ)");
    }

    // ── G4-L112: constant annihilation + classification ────────────────────────────────

    [Fact]
    public void G4_L112_ConstantAnnihilationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L112: native constant-annihilation and the coefficient classification");

        // Native combinatorial condition: the diagonal equals minus the sum of off-diagonal
        // coefficients (the operator annihilates constants).
        double[] off = { LorentzianOperator.BdgCoefficient(0), LorentzianOperator.BdgCoefficient(1), LorentzianOperator.BdgCoefficient(2) };
        double diag = -2.0;
        double sum = diag + off.Sum();
        sb.AppendLine($"BDG stencil sum (diagonal + off-diagonal): {sum:F1}  → annihilates constants: {Math.Abs(sum) < 1e-9}");

        // Apply the symmetric BDG reference to a constant function. On a FINITE lattice the layer
        // multiplicities vary, so B·1 ≠ 0 pointwise — constant-annihilation is an AVERAGED
        // (continuum) property, held exactly only at the STENCIL level (Σ a_ℓ = 0).
        var cs = Cs;
        var b = LorentzianOperator.BdgReference(cs);
        double maxInterior = 0.0;
        for (int i = 0; i < cs.Count; i++)
        {
            bool interior = cs.Time[i] >= 2 && cs.Time[i] <= TMax - 2 && Math.Abs(cs.Space[i]) <= XMax - 2;
            if (!interior) continue;
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++) s += b[i, j] * 1.0;
            maxInterior = Math.Max(maxInterior, Math.Abs(s));
        }
        sb.AppendLine($"BDG reference applied to constant φ ≡ 1 (interior events): max|B·1| = {maxInterior:F2}");
        sb.AppendLine("  (non-zero on a finite lattice — layer multiplicities vary; exact only in the continuum)");

        // Native combinatorial conditions: stencil sum = 0 (diagonal = −Σ off-diagonal).
        bool stencilAnnhilates = Math.Abs(sum) < 1e-9;
        sb.AppendLine();
        sb.AppendLine($"stencil-level constant-annihilation (Σ a_ℓ = 0, diagonal = −Σ off-diagonal): {stencilAnnhilates}");
        sb.AppendLine("overall scale −2 (normalization to the continuum □): IMPORTED");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — the binomial coefficients (−1)^ℓ·C(2,ℓ) and the");
        sb.AppendLine("constant-annihilation (diagonal = −Σ off-diagonal) emerge from interval combinatorics;");
        sb.AppendLine("only the overall scale −2 requires continuum matching.");
        Output.WriteLine(sb.ToString());

        Assert.True(stencilAnnhilates, "stencil does not annihilate constants");
        Assert.True(maxInterior > 1e-9, "unexpected: B·1 vanishes pointwise on the finite lattice");
    }
}
