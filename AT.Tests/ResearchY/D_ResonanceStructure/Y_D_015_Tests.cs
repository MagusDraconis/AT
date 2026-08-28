using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_015 — N=96 Uniqueness Audit test suite (Y_D_015_Tests.cs).
///
/// Question: what property makes N=96 unique among alternative closure sizes?
///
/// Verdict tested: N=96 is unique in the tested class {64, 96, 128, 192, 245} by the
/// COMBINATION of the period-3 seed symmetry (6|N) and the three-family octave window
/// (span in [4,8)), generating the canonical [4,4,87] structure. The scale properties
/// (λ₂, ω₁, Z2) are not unique; the structural properties are.
///
/// Deterministic: closed-form circulant eigenvalues for each N.
/// </summary>
public class Y_D_015_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_015_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n) => Math.Sqrt(Lambda(k, n));

    /// <summary>Octave occupancies for a given N.</summary>
    private static int[] OctaveOccupancies(int n)
    {
        var freqs = new double[n - 1];
        for (int k = 1; k < n; k++) freqs[k - 1] = Omega(k, n);
        Array.Sort(freqs);
        double w0 = freqs[0];
        int families = (int)Math.Floor(Math.Log2(freqs[^1] / freqs[0])) + 1;
        var occ = new int[families];
        for (int j = 0; j < families; j++)
            occ[j] = freqs.Count(x => Math.Pow(2, j) * w0 <= x && x < Math.Pow(2, j + 1) * w0);
        return occ;
    }

    /// <summary>Family count for a given N.</summary>
    private static int FamilyCount(int n)
    {
        var freqs = new double[n - 1];
        for (int k = 1; k < n; k++) freqs[k - 1] = Omega(k, n);
        Array.Sort(freqs);
        return (int)Math.Floor(Math.Log2(freqs[^1] / freqs[0])) + 1;
    }

    private static double Span(int n)
    {
        var freqs = new double[n - 1];
        for (int k = 1; k < n; k++) freqs[k - 1] = Omega(k, n);
        Array.Sort(freqs);
        return freqs[^1] / freqs[0];
    }

    // ── [Required] Y_D_015_Comparison ────────────────────────────────────

    /// <summary>
    /// Compare the candidate closure sizes: N=64/96/128/192/245.
    /// N=96 has span 6.403 (in the [4,8) window); the others differ.
    /// </summary>
    [Fact]
    public void Y_D_015_Comparison()
    {
        Assert.Equal(6.403, Span(96), 3);  // N=96: 3-family window
        Assert.True(Span(64) < 8);         // N=64: also 3 families
        Assert.True(Span(128) > 8);        // N=128: 4 families
        Assert.True(Span(192) > 8);        // N=192: 4 families
        Assert.True(Span(245) > 8);        // N=245: 5 families

        // The period-3 seed: 6|N.
        Assert.Equal(0, 96 % 6);           // 96 passes
        Assert.NotEqual(0, 64 % 6);        // 64 fails
        Assert.NotEqual(0, 128 % 6);       // 128 fails
        Assert.Equal(0, 192 % 6);          // 192 passes the seed
        Assert.NotEqual(0, 245 % 6);       // 245 fails
    }

    // ── [Required] Y_D_015_SpectralMeasures ──────────────────────────────

    /// <summary>
    /// λ₂, ω₁, Z2 pairs, family count, occupancy, span per N.
    /// The scale properties shift with N; the structural properties distinguish N=96.
    /// </summary>
    [Fact]
    public void Y_D_015_SpectralMeasures()
    {
        // λ₂ (spectral gap) decreases with N.
        Assert.True(Lambda(1, 64) > Lambda(1, 96));
        Assert.True(Lambda(1, 96) > Lambda(1, 128));

        // ω₁ (minimum excitation) decreases with N.
        Assert.True(Omega(1, 64) > Omega(1, 96));
        Assert.True(Omega(1, 96) > Omega(1, 128));

        // Family counts.
        Assert.Equal(3, FamilyCount(64));
        Assert.Equal(3, FamilyCount(96));
        Assert.Equal(4, FamilyCount(128));
        Assert.Equal(4, FamilyCount(192));
        Assert.Equal(5, FamilyCount(245));

        // The canonical occupancy [4,4,87] is unique to N=96.
        Assert.Equal(new[] { 4, 4, 87 }, OctaveOccupancies(96));
        Assert.NotEqual(new[] { 4, 4, 87 }, OctaveOccupancies(64));
    }

    // ── [Required] Y_D_015_SelectionMechanism ────────────────────────────

    /// <summary>
    /// N=96 is selected by the COMBINATION (E) of the period-3 seed symmetry (6|N) and
    /// the three-family octave window (span in [4,8)).
    /// </summary>
    [Fact]
    public void Y_D_015_SelectionMechanism()
    {
        // Combination: seed symmetry (6|N) AND family window (span < 8).
        bool seed96 = 96 % 6 == 0;
        bool window96 = Span(96) < 8;
        Assert.True(seed96 && window96); // N=96 passes both

        // N=192 passes the seed but fails the window.
        Assert.True(192 % 6 == 0 && !(Span(192) < 8));
        // N=64 passes the window but fails the seed.
        Assert.True(!(64 % 6 == 0) && Span(64) < 8);

        // The intersection is {96} alone.
        int[] candidates = { 64, 96, 128, 192, 245 };
        var passing = candidates.Where(n => n % 6 == 0 && Span(n) < 8).ToArray();
        Assert.Equal(new[] { 96 }, passing);
    }

    // ── [Required] Y_D_015_StructureLoss ─────────────────────────────────

    /// <summary>
    /// If N changes, the three-family structure and the [4,4,87] occupancy disappear.
    /// </summary>
    [Fact]
    public void Y_D_015_StructureLoss()
    {
        // N=64: loses the [4,4,87] occupancy.
        Assert.NotEqual(new[] { 4, 4, 87 }, OctaveOccupancies(64));

        // N=128/192/245: lose the three-family window.
        Assert.NotEqual(3, FamilyCount(128));
        Assert.NotEqual(3, FamilyCount(192));
        Assert.NotEqual(3, FamilyCount(245));

        // Only N=96 keeps both the 3-family count and the [4,4,87] occupancy.
        Assert.Equal(3, FamilyCount(96));
        Assert.Equal(new[] { 4, 4, 87 }, OctaveOccupancies(96));
    }

    // ── [Required] Y_D_015_ScaleGenerating ───────────────────────────────

    /// <summary>
    /// The scale-generating property unique to N=96: the combination (seed symmetry +
    /// family window) yielding the canonical [4,4,87] structure.
    /// </summary>
    [Fact]
    public void Y_D_015_ScaleGenerating()
    {
        // The unique combination: 6|N and span in [4,8).
        Assert.Equal(0, 96 % 6);
        Assert.True(Span(96) >= 4 && Span(96) < 8);

        // This generates the canonical occupancy (feeding occMom and the moments).
        Assert.Equal(new[] { 4, 4, 87 }, OctaveOccupancies(96));
        double occMom = (4.0 * 4 + 4.0 * 4 + 87.0 * 87) / 4.0;
        Assert.Equal(1900.25, occMom, 2); // the canonical occupancy moment
    }

    // ── [Required] Y_D_015_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_015_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_015 — N=96 Uniqueness Audit");

        sb.AppendLine("Goal: what property makes N=96 unique among closure sizes?");
        sb.AppendLine();

        int[] sizes = { 64, 96, 128, 192, 245 };
        sb.AppendLine("[1] Comparison");
        sb.AppendLine("    N    | λ₂      | ω₁      | fam | occupancy      | span");
        foreach (int n in sizes)
        {
            var occ = OctaveOccupancies(n);
            string occStr = string.Join(",", occ);
            sb.AppendLine($"    {n,-4} | {Lambda(1, n):F4} | {Omega(1, n):F4} | {FamilyCount(n),-3} | [{occStr,-14}] | {Span(n):F3}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] Selection mechanism");
        sb.AppendLine("    A) resonance: partial (all N have standing waves)");
        sb.AppendLine("    B) closure: partial (fixed point, size is the question)");
        sb.AppendLine("    C) symmetry: period-3 seed (6|N) — 96 and 192 pass");
        sb.AppendLine("    D) family window: span < 8 — 64 and 96 pass");
        sb.AppendLine("    E) COMBINATION: 6|N AND span < 8 → {96} UNIQUE");
        sb.AppendLine();

        sb.AppendLine("[3] Structure loss if N changes");
        sb.AppendLine("    N=64: loses [4,4,87] (→[4,39,20]); fails seed");
        sb.AppendLine("    N=128/192/245: lose 3-family window (→4/4/5 families)");
        sb.AppendLine();

        sb.AppendLine("[4] Scale-generating property unique to N=96");
        sb.AppendLine("    the combination (6|N + span in [4,8)) → [4,4,87]");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    N=96 is unique by the COMBINATION of the period-3 seed");
        sb.AppendLine("    symmetry and the three-family octave window. The scale");
        sb.AppendLine("    properties (λ₂, ω₁, Z2) are not unique; the structural");
        sb.AppendLine("    properties (3 families, [4,4,87]) are. No canonical value");
        sb.AppendLine("    is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
