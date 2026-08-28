using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_016 — Family-Count Origin Audit test suite (Y_D_016_Tests.cs).
///
/// Question: are N=96, 6|N, span ∈ [4,8), and family count = 3 derived necessities or
/// selection rules?
///
/// Verdict tested: family count = 3 ⟺ span ∈ [4,8) is a DERIVED mathematical
/// equivalence; the 3-family window choice, 6|N, and N=96 are SELECTION RULES. Scanning
/// finds 61 rings with 3 families (N ∈ [60,120]); N=96 is one of 11 with both 6|N and 3
/// families.
///
/// Deterministic: closed-form circulant eigenvalues across the ring scan.
/// </summary>
public class Y_D_016_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_016_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Span(int n)
    {
        double maxW = 0.0, minW = double.PositiveInfinity;
        for (int k = 1; k < n; k++)
        {
            double w = Math.Sqrt(Lambda(k, n));
            maxW = Math.Max(maxW, w);
            minW = Math.Min(minW, w);
        }
        return maxW / minW;
    }

    private static int Families(int n) => (int)Math.Floor(Math.Log2(Span(n))) + 1;

    // ── [Required] Y_D_016_RingScan ──────────────────────────────────────

    /// <summary>
    /// Scanning C_N(±1..±6) for N ∈ [13, 300] finds 61 rings with 3 families
    /// (N ∈ [60, 120]). N=96 is not unique for the family count.
    /// </summary>
    [Fact]
    public void Y_D_016_RingScan()
    {
        int threeFamily = 0;
        int minN = int.MaxValue, maxN = 0;
        for (int n = 13; n <= 300; n++)
        {
            if (Families(n) == 3)
            {
                threeFamily++;
                minN = Math.Min(minN, n);
                maxN = Math.Max(maxN, n);
            }
        }
        Assert.True(threeFamily > 10);        // many rings give 3 families
        Assert.Equal(60, minN);               // the 3-family window starts at 60
        Assert.Equal(120, maxN);              // ... and ends at 120

        // N=96 is one admissible ring, not unique.
        Assert.Equal(3, Families(96));
        Assert.True(threeFamily >= 61);
    }

    // ── [Required] Y_D_016_Mod6 ──────────────────────────────────────────

    /// <summary>
    /// 6|N is NOT necessary for 3 families: N=61..65 give 3 families without 6|N.
    /// </summary>
    [Fact]
    public void Y_D_016_Mod6()
    {
        // 3-family rings without the seed (6 ∤ N).
        Assert.Equal(3, Families(61)); // 61 mod 6 = 1
        Assert.Equal(3, Families(62)); // 62 mod 6 = 2
        Assert.Equal(3, Families(63)); // 63 mod 6 = 3
        Assert.Equal(3, Families(64)); // 64 mod 6 = 4
        Assert.Equal(3, Families(65)); // 65 mod 6 = 5

        // 6|N is not necessary for 3 families.
        Assert.False(61 % 6 == 0);
        Assert.False(64 % 6 == 0);
    }

    // ── [Required] Y_D_016_SpanScan ──────────────────────────────────────

    /// <summary>
    /// The 3-family window is span ∈ [4, 8); the span varies smoothly across the window.
    /// </summary>
    [Fact]
    public void Y_D_016_SpanScan()
    {
        // Window edges: N=60 (span ≈ 4.02) and N=120 (span ≈ 8.00).
        Assert.True(Span(60) >= 4.0 && Span(60) < 8.0);
        Assert.True(Span(120) >= 4.0 && Span(120) < 8.0);

        // N=96 is inside the window (span 6.40).
        Assert.True(Span(96) >= 4.0 && Span(96) < 8.0);
        Assert.Equal(6.40, Span(96), 2);

        // The span increases with N across the window.
        Assert.True(Span(60) < Span(96));
        Assert.True(Span(96) < Span(120));
    }

    // ── [Required] Y_D_016_ThreeFamilyCondition ──────────────────────────

    /// <summary>
    /// family count = 3 ⟺ span ∈ [4, 8) — a DERIVED mathematical identity.
    /// </summary>
    [Fact]
    public void Y_D_016_ThreeFamilyCondition()
    {
        // The identity: floor(log₂ span)+1 = 3 ⟺ 4 ≤ span < 8.
        foreach (int n in new[] { 60, 61, 64, 90, 96, 120 })
        {
            double s = Span(n);
            int f = Families(n);
            Assert.Equal(f == 3, s >= 4.0 && s < 8.0); // the equivalence holds
        }

        // At the window boundary: N=128 (span 8.53) gives 4 families.
        Assert.Equal(4, Families(128));
        Assert.True(Span(128) >= 8.0);
    }

    // ── [Required] Y_D_016_Counterexamples ───────────────────────────────

    /// <summary>
    /// Counterexamples to N=96 being the unique 3-family ring: N=64, 90, 120 all give 3
    /// families; N=128 gives 4.
    /// </summary>
    [Fact]
    public void Y_D_016_Counterexamples()
    {
        Assert.Equal(3, Families(64));  // 3 families, 6 ∤ 64
        Assert.Equal(3, Families(90));  // 3 families, 6 | 90
        Assert.Equal(3, Families(120)); // 3 families, 6 | 120 (upper edge)
        Assert.Equal(4, Families(128)); // 4 families (window boundary)

        // N=96 is one admissible ring among many.
        Assert.Equal(3, Families(96));
    }

    // ── [Required] Y_D_016_Classification ────────────────────────────────

    /// <summary>
    /// A) N=96 SELECTION RULE; B) 6|N SELECTION RULE; C) span∈[4,8) DERIVED (the
    /// equivalence with 3 families); D) family count = 3 SELECTION RULE.
    /// </summary>
    [Fact]
    public void Y_D_016_Classification()
    {
        // C is DERIVED: family count = 3 ⟺ span ∈ [4,8) (identity).
        Assert.Equal(3, Families(96));
        Assert.True(Span(96) >= 4.0 && Span(96) < 8.0);

        // A, B, D are SELECTION RULES:
        // - A: many rings give 3 families (N=96 is one of 61)
        // - B: 3-family rings exist without 6|N (N=64)
        // - D: the 3-family window is a choice (2/4-family windows exist)
        Assert.Equal(3, Families(64)); // counterexample for B
        Assert.Equal(2, Families(29)); // a 2-family window exists (span ~2.02 < 4)
        Assert.Equal(4, Families(128)); // a 4-family window exists (span ≥ 8)
    }

    // ── [Required] Y_D_016_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_016_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_016 — Family-Count Origin Audit");

        sb.AppendLine("Goal: are N=96, 6|N, span∈[4,8), family count=3 derived or selected?");
        sb.AppendLine();

        int threeFamily = 0;
        for (int n = 13; n <= 300; n++) if (Families(n) == 3) threeFamily++;

        sb.AppendLine("[1] Ring scan (C_N(±1..±6), N ∈ [13, 300])");
        sb.AppendLine($"    rings with 3 families: {threeFamily} (N ∈ [60, 120])");
        sb.AppendLine($"    rings with 6|N and 3 families: 11 (60, 66, ..., 120)");
        sb.AppendLine("    N=96 is one admissible ring — NOT unique for the family count");
        sb.AppendLine();

        sb.AppendLine("[2] Conditions");
        sb.AppendLine($"    N=96: SELECTION RULE (one of 61 three-family rings)");
        sb.AppendLine("    6|N:  SELECTION RULE (3-family rings exist without it, e.g. N=64)");
        sb.AppendLine("    span∈[4,8): DERIVED (family count = 3 ⟺ span∈[4,8), identity)");
        sb.AppendLine("    family count=3: SELECTION RULE (the 3-family window is a choice)");
        sb.AppendLine();

        sb.AppendLine("[3] Counterexamples");
        sb.AppendLine("    N=64:  3 families, 6∤64 (seed not necessary)");
        sb.AppendLine("    N=90:  3 families, 6|90 (another admissible ring)");
        sb.AppendLine("    N=120: 3 families (upper window edge)");
        sb.AppendLine("    N=128: 4 families (window boundary)");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusion");
        sb.AppendLine("    family count = 3 ⟺ span ∈ [4,8) is DERIVED (identity).");
        sb.AppendLine("    The 3-family window, 6|N, and N=96 are SELECTION RULES —");
        sb.AppendLine("    N=96 is one of 11 rings with both the seed and 3 families,");
        sb.AppendLine("    selected by the additional D96 criteria, not by the family");
        sb.AppendLine("    count alone. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
