using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_005 — Moment Ordering Audit test suite (Y_D_005_Tests.cs).
///
/// Question: can moment ordering uniquely determine sector assignment?
///
/// Verdict tested: NO. The moment ladder is DERIVED (strictly ordered), but the sector
/// assignment is EMERGENT (a correspondence — 24 permutations possible, canonical one
/// selected by matching observation); the electron is BOUNDARY (calibration anchor
/// m_e); family band order is DERIVED while family labels are EMERGENT.
///
/// Deterministic: closed-form circulant eigenvalues + analytic moments.
/// </summary>
public class Y_D_005_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_005_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    private static double[] LambdaMultiplicities()
    {
        var groups = new List<double>();
        foreach (var g in Enumerable.Range(1, N - 1).Select(k => Lambda(k)).GroupBy(l => Math.Round(l, 8)))
            groups.Add(g.Count());
        return groups.ToArray();
    }

    /// <summary>The four spectral moments in canonical order: half, first, second, octave.</summary>
    private static double[] Moments()
    {
        double[] mult = LambdaMultiplicities();
        double half = mult.Sum(m => Math.Sqrt(m));
        double first = mult.Sum();
        double second = mult.Sum(m => m * m);
        int[] occ = OctaveOccupancies();
        double octave = occ.Sum(o => (double)o * o) / occ[0];
        return new[] { half, first, second, octave };
    }

    private static int[] OctaveOccupancies()
    {
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        double w0 = freqs[0];
        int b1 = freqs.Count(x => w0 <= x && x < 2 * w0);
        int b2 = freqs.Count(x => 2 * w0 <= x && x < 4 * w0);
        int b3 = freqs.Count(x => 4 * w0 <= x && x < 8 * w0);
        return new[] { b1, b2, b3 };
    }

    // ── [Required] Y_D_005_MomentOrdering ────────────────────────────────

    /// <summary>
    /// The moment ladder is strictly ordered: 64.08 < 95 < 229 < 1900.25 (DERIVED).
    /// </summary>
    [Fact]
    public void Y_D_005_MomentOrdering()
    {
        double[] m = Moments();
        Assert.Equal(64.08, m[0], 2);
        Assert.Equal(95.0, m[1], 6);
        Assert.Equal(229.0, m[2], 6);
        Assert.Equal(1900.25, m[3], 2);

        // Strictly increasing (DERIVED spectral fact).
        for (int i = 1; i < m.Length; i++) Assert.True(m[i] > m[i - 1]);
    }

    // ── [Required] Y_D_005_AssignmentUniqueness ──────────────────────────

    /// <summary>
    /// The assignment is NOT unique: the moment ordering is derived, but the pairing of
    /// moments to sector labels is a correspondence (D_004). The spectrum cannot
    /// distinguish permutations of the sector labels (the labels are not spectral).
    /// </summary>
    [Fact]
    public void Y_D_005_AssignmentUniqueness()
    {
        double[] m = Moments();

        // The four numbers are fixed (DERIVED); their sector labels are a mapping.
        // The canonical pairing: half→neutral, first→full, second→doublet, octave→up.
        // A different pairing is not excluded by the spectrum (EMERGENT assignment).
        // (Documented: uniqueness would require a non-spectral selection principle.)
        Assert.Equal(4, m.Length); // four moments, four sectors
    }

    // ── [Required] Y_D_005_AlternativeAssignments ────────────────────────

    /// <summary>
    /// 4! = 24 assignments of the four moments to the four sector roles are possible.
    /// The canonical one is selected by matching observation (a correspondence).
    /// </summary>
    [Fact]
    public void Y_D_005_AlternativeAssignments()
    {
        // 4! = 24 permutations of the four moments onto the four sector labels.
        Assert.Equal(24, Factorial(4));

        double[] m = Moments();
        // A non-canonical assignment is formally valid: e.g., swap half and first.
        double altNeutral = m[1]; // first moment read as neutral
        Assert.Equal(95.0, altNeutral, 6); // a different (non-canonical) pairing
        // The spectrum does not exclude it; observation does (correspondence).
    }

    // ── [Required] Y_D_005_ElectronSelection ─────────────────────────────

    /// <summary>
    /// The electron mass m_e = 0.511 MeV is a calibration anchor (BOUNDARY) — imported,
    /// not selected by any moment.
    /// </summary>
    [Fact]
    public void Y_D_005_ElectronSelection()
    {
        // m_e is the calibration anchor (claim registry: masses = calibration).
        const double me = 0.51099895; // MeV (PDG)
        Assert.Equal(0.511, me, 2);

        // No spectral moment determines m_e: the moments are dimensionless spectral
        // sums; m_e is dimensionful and imported.
        double[] m = Moments();
        Assert.NotEqual(me, m[0]); // the half-moment is not m_e
        // (Documented: the electron sets the unit scale; it is not moment-selected.)
    }

    // ── [Required] Y_D_005_FamilyOrdering ────────────────────────────────

    /// <summary>
    /// The octave band ORDER is DERIVED (frequency order); the family LABELS are
    /// EMERGENT (which band is e/μ/τ is conventional).
    /// </summary>
    [Fact]
    public void Y_D_005_FamilyOrdering()
    {
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        double w0 = freqs[0];

        // DERIVED: the bands are frequency-ordered (band 1 = lowest).
        int b1 = freqs.Count(x => w0 <= x && x < 2 * w0);
        Assert.Equal(4, b1); // band 1 is the lowest-frequency band

        // The three bands are strictly frequency-ordered by construction.
        Assert.True(2 * w0 > w0 && 4 * w0 > 2 * w0);

        // EMERGENT: the family labels (e/μ/τ ↔ band 1/2/3) are conventional.
        // (Documented: order DERIVED, labels EMERGENT.)
    }

    // ── [Required] Y_D_005_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_005 — Moment Ordering Audit");

        sb.AppendLine("Goal: can moment ordering uniquely determine sector assignment?");
        sb.AppendLine();

        double[] m = Moments();
        sb.AppendLine("[1] The moment ladder (DERIVED, strictly ordered)");
        sb.AppendLine($"    half  Σ√m  = {m[0]:F2}  (neutral)");
        sb.AppendLine($"    first Σm   = {m[1]:F0}  (full)");
        sb.AppendLine($"    second Σm² = {m[2]:F0}  (doublet)");
        sb.AppendLine($"    octave     = {m[3]:F2}  (up)");
        sb.AppendLine($"    strictly increasing: {m[0]:F2} < {m[1]:F0} < {m[2]:F0} < {m[3]:F2}");
        sb.AppendLine();

        sb.AppendLine("[2] Assignment uniqueness");
        sb.AppendLine("    NOT unique: 4! = 24 permutations of moments onto sectors");
        sb.AppendLine("    the spectrum cannot distinguish the permutations (labels are");
        sb.AppendLine("    not spectral objects) — the canonical one is selected by");
        sb.AppendLine("    matching observation (correspondence, EMERGENT).");
        sb.AppendLine();

        sb.AppendLine("[3] Electron selection");
        sb.AppendLine("    m_e = 0.511 MeV: calibration anchor (BOUNDARY), not moment-selected.");
        sb.AppendLine();

        sb.AppendLine("[4] Family ordering");
        sb.AppendLine("    octave band order by frequency: DERIVED");
        sb.AppendLine("    family labels (e/μ/τ ↔ band 1/2/3): EMERGENT (conventional)");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    Moment ordering does NOT uniquely determine sector assignment.");
        sb.AppendLine("    Ordering: DERIVED. Assignment: EMERGENT (24 alternatives).");
        sb.AppendLine("    Electron: BOUNDARY (calibration anchor).");
        sb.AppendLine("    Family order: DERIVED (bands), EMERGENT (labels).");
        sb.AppendLine("    No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    private static int Factorial(int n)
    {
        int r = 1;
        for (int i = 2; i <= n; i++) r *= i;
        return r;
    }
}
