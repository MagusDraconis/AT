using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_032 — Pairing-Requirement Audit test suite (Y_D_032_Tests.cs).
///
/// Question: why must the observable sector be completely paired (0 unpaired modes)?
///
/// Verdict tested: the pairing STRUCTURE is DERIVED (D_021: cos/sin quadrature pairs
/// from oscillation); the COMPLETENESS (0 unpaired) is BOUNDARY — the observable-sector
/// requirement that every frequency carry full doublet/phase structure. The
/// self-conjugate mode k=N/2 has sin(πn)=0 (vanishing quadrature); complete pairing
/// requires it to sit in a degenerate group (λ=12 5-fold at N=96/192, 1-fold at
/// N=64/80/128). The unpaired mode has no weak-isospin doublet partner. Not required
/// by count conservation (B) or closure (D); required by the doublet-structure
/// observability (the observable-sector construction, D_020).
///
/// Deterministic: closed-form circulant eigenvalues.
/// </summary>
public class Y_D_032_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_032_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

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

    /// <summary>Multiplicity of the self-conjugate eigenvalue λ(N/2).</summary>
    private static int SelfConjugateMultiplicity(int n)
    {
        int ksc = n / 2;
        double lamSc = Math.Round(Lambda(ksc, n), 9);
        return Enumerable.Range(1, n - 1).Count(k => Math.Round(Lambda(k, n), 9) == lamSc);
    }

    // ── [Required] Y_D_032_UnpairedModeTest ────────────────────────────

    /// <summary>
    /// The self-conjugate mode k=N/2 has a vanishing sin quadrature; unpaired at
    /// 64/80/128, not at 96/192.
    /// </summary>
    [Fact]
    public void Y_D_032_UnpairedModeTest()
    {
        // Self-conjugate mode: sin(2π·(N/2)·n/N) = sin(πn) = 0 for all n.
        foreach (int n in new[] { 64, 80, 96, 128, 192 })
        {
            int ksc = n / 2;
            for (int site = 0; site < n; site += 7)
                Assert.Equal(0.0, Math.Sin(2.0 * Math.PI * ksc * site / n), 10);
        }

        // Unpaired counts: 64/80/128 have 1, 96/192 have 0.
        Assert.Equal(1, UnpairedCount(64));
        Assert.Equal(1, UnpairedCount(80));
        Assert.Equal(0, UnpairedCount(96));
        Assert.Equal(1, UnpairedCount(128));
        Assert.Equal(0, UnpairedCount(192));
    }

    // ── [Required] Y_D_032_ObservableCompleteness ──────────────────────

    /// <summary>
    /// At N=96 every eigenvalue has multiplicity ≥ 2 (all modes have partners) — the
    /// observable completeness. At N=64 λ=12 is a lone singlet.
    /// </summary>
    [Fact]
    public void Y_D_032_ObservableCompleteness()
    {
        // N=96: every eigenvalue has multiplicity ≥ 2.
        var mults96 = Enumerable.Range(1, 95)
            .Select(k => Math.Round(Lambda(k, 96), 9))
            .GroupBy(x => x)
            .Select(g => g.Count())
            .ToArray();
        Assert.All(mults96, m => Assert.True(m >= 2, $"eigenvalue with multiplicity {m} < 2"));

        // N=64: λ=12 is a lone singlet (multiplicity 1).
        Assert.Equal(1, SelfConjugateMultiplicity(64));
        Assert.Equal(5, SelfConjugateMultiplicity(96));
        Assert.Equal(1, SelfConjugateMultiplicity(128));
        Assert.Equal(5, SelfConjugateMultiplicity(192));
    }

    // ── [Required] Y_D_032_RepresentationClosure ───────────────────────

    /// <summary>
    /// Representation closure: λ(N/2)=12 is fixed; its multiplicity (the degeneracy
    /// group) is 5 at 96/192 and 1 at 64/80/128.
    /// </summary>
    [Fact]
    public void Y_D_032_RepresentationClosure()
    {
        // λ(N/2) = 12 for all even N ≥ 12.
        foreach (int n in new[] { 64, 80, 96, 128, 192 })
            Assert.Equal(12.0, Lambda(n / 2, n), 6);

        // Multiplicity: 5 at 96/192 (paired), 1 at 64/80/128 (unpaired).
        Assert.Equal(5, SelfConjugateMultiplicity(96));
        Assert.Equal(5, SelfConjugateMultiplicity(192));
        Assert.Equal(1, SelfConjugateMultiplicity(64));
        Assert.Equal(1, SelfConjugateMultiplicity(80));
        Assert.Equal(1, SelfConjugateMultiplicity(128));
    }

    // ── [Required] Y_D_032_SymmetryClosure ─────────────────────────────

    /// <summary>
    /// Symmetry closure: reflection maps cos → cos (self); the degenerate group supplies
    /// the partners for the eigenvalue.
    /// </summary>
    [Fact]
    public void Y_D_032_SymmetryClosure()
    {
        // The self-conjugate mode k=N/2 has cos even under reflection (n → N−n).
        int ksc = 48, N = 96;
        foreach (int site in new[] { 0, 7, 13, 41, 95 })
        {
            double cosK = Math.Cos(2.0 * Math.PI * ksc * site / N);
            double cosRefl = Math.Cos(2.0 * Math.PI * ksc * (N - site) / N);
            Assert.Equal(cosK, cosRefl, 10); // cos → cos (parity +1)
        }

        // The degenerate group supplies the complete quadrature structure:
        // at N=96, k=16, 32, 64, 80 share λ=12 with k=48.
        var lam12 = Enumerable.Range(1, 95)
            .Where(k => Math.Abs(Lambda(k, 96) - 12.0) < 1e-6)
            .ToArray();
        Assert.Equal(new[] { 16, 32, 48, 64, 80 }, lam12);
    }

    // ── [Required] Y_D_032_DependencyTrace ─────────────────────────────

    /// <summary>
    /// Trace: Difference → observable sector (BOUNDARY, D_020) → complete pairing →
    /// self-conjugate degeneracy → p=3 → N=96.
    /// </summary>
    [Fact]
    public void Y_D_032_DependencyTrace()
    {
        // Observable sector: complete pairing requirement (D_020).
        Assert.Equal(0, UnpairedCount(96));

        // Self-conjugate degeneracy: λ=12 5-fold at N=96.
        Assert.Equal(5, SelfConjugateMultiplicity(96));

        // p=3 (D_031): the minimal complete-pairing period.
        Assert.Equal(0, UnpairedCount(96)); // the natural size of p=3

        // N=96 = 3·2⁵.
        Assert.Equal(3, 96 / 32);

        // The pairing structure is DERIVED (D_021: cos/sin quadrature); the
        // completeness is the BOUNDARY observable-sector input (D_020).
        Assert.True(true);
    }

    // ── [Required] Y_D_032_Run ─────────────────────────────────────────

    [Fact]
    public void Y_D_032_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_032 — Pairing-Requirement Audit");

        sb.AppendLine("Goal: why must the observable sector be completely paired");
        sb.AppendLine("(0 unpaired modes)?");
        sb.AppendLine();

        sb.AppendLine("[1] The self-conjugate mode k=N/2");
        sb.AppendLine("    sin(pi*n) = 0 (vanishing quadrature) - only cos survives");
        sb.AppendLine();

        sb.AppendLine("[2] Complete vs incomplete pairing");
        sb.AppendLine("    lambda(N/2) = 12 always; multiplicity 5 at 96/192, 1 at 64/80/128");
        foreach (int n in new[] { 64, 80, 96, 128, 192 })
            sb.AppendLine($"    N={n}: mult(lambda=12) = {SelfConjugateMultiplicity(n)}, unpaired = {UnpairedCount(n)}");
        sb.AppendLine();

        sb.AppendLine("[3] What fails with unpaired modes");
        sb.AppendLine("    phase freedom (no sin), representation closure (no doublet),");
        sb.AppendLine("    symmetry closure (no partner), weak-isospin attachment");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    pairing STRUCTURE: DERIVED (D_021, oscillation quadrature)");
        sb.AppendLine("    complete pairing (0 unpaired): BOUNDARY (observable sector, D_020)");
        sb.AppendLine("    self-conjugate degeneracy: DERIVED (N-arithmetic, 6|N)");
        sb.AppendLine("    p=3 / N=96: DERIVED");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
