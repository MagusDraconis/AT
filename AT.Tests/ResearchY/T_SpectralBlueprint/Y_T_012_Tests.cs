using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_012 — Origin of 19 Audit.
///
/// Question: can the historical ~19-species count emerge from D96^3 (the cubic tensor product
/// D96 ⊗ D96 ⊗ D96, NP_037/NP_088) rather than from the 1D D96 ring?
///
/// The cubic lattice has octahedral (O_h) symmetry: its modes decompose into axis-count
/// SECTORS (1-axis / 2-axis / 3-axis) and permutation-orbit IRREP classes (A = {a,a,a},
/// T = two-equal, G = all-distinct). Run the replicator–mutator on the full 3D spectrum and on
/// each sector/irrep sub-spectrum; measure survivors and cumulative discovery; test whether
/// ~19 emerges without parameter tuning. Deterministic throughout.
/// </summary>
public class Y_T_012_Tests : ResearchTestBase
{
    public Y_T_012_Tests(ITestOutputHelper output) : base(output) { }

    // ── 1D reduced spectrum and the 3D tensor-product breakdown ──────────────

    /// <summary>1D D96 eigenvalue λ_r = 2Σ_{d=1..6}(1−cos 2πdr/96) for reduced index r ∈ 0..48.</summary>
    private static double Lambda1D(int r)
    {
        double s = 0.0;
        for (int d = 1; d <= 6; d++) s += 1.0 - Math.Cos(2.0 * Math.PI * d * r / 96.0);
        return 2.0 * s;
    }

    /// <summary>1D multiplicity of reduced index r (1 for r=0,48; 2 otherwise).</summary>
    private static int Mult1D(int r) => (r == 0 || r == 48) ? 1 : 2;

    /// <summary>Axis-count sector (1/2/3) of a triple of reduced indices (0 = zero mode).</summary>
    private static int Sector(int r1, int r2, int r3)
        => (r1 > 0 ? 1 : 0) + (r2 > 0 ? 1 : 0) + (r3 > 0 ? 1 : 0);

    /// <summary>Octahedral permutation-orbit class: 0=A ({a,a,a}), 1=T (two equal), 2=G (distinct).</summary>
    private static int IrrepClass(int r1, int r2, int r3)
    {
        var nz = new List<int>(3);
        if (r1 > 0) nz.Add(r1);
        if (r2 > 0) nz.Add(r2);
        if (r3 > 0) nz.Add(r3);
        nz.Sort();
        if (nz.Count <= 1) return 1;                       // single axis → T-orbit (size 3)
        if (nz.Count == 2) return nz[0] == nz[1] ? 1 : 2;  // {a,a,0}→T ; {a,b,0}→G
        // three non-zero
        if (nz[0] == nz[1] && nz[1] == nz[2]) return 0;    // {a,a,a} → A
        if (nz[0] == nz[1] || nz[1] == nz[2]) return 1;    // two equal → T
        return 2;                                          // all distinct → G
    }

    /// <summary>
    /// Full D96⊗D96⊗D96 decomposition: distinct eigenvalues ascending, with total multiplicity
    /// and per-sector (n1,n2,n3) and per-irrep (nA,nT,nG) multiplicities.
    /// </summary>
    private static (double[] Distinct, int[] Total, int[] N1, int[] N2, int[] N3, int[] NA, int[] NT, int[] NG)
        Breakdown()
    {
        var d = new Dictionary<double, int[]>();   // [total, n1, n2, n3, nA, nT, nG]
        for (int r1 = 0; r1 <= 48; r1++)
            for (int r2 = 0; r2 <= 48; r2++)
                for (int r3 = 0; r3 <= 48; r3++)
                {
                    double e = Lambda1D(r1) + Lambda1D(r2) + Lambda1D(r3);
                    int m = Mult1D(r1) * Mult1D(r2) * Mult1D(r3);
                    int sec = Sector(r1, r2, r3);
                    int irr = IrrepClass(r1, r2, r3);
                    if (!d.TryGetValue(e, out var b))
                    {
                        b = new int[7];
                        d[e] = b;
                    }
                    b[0] += m;
                    if (sec == 1) b[1] += m;
                    else if (sec == 2) b[2] += m;
                    else if (sec == 3) b[3] += m;
                    if (irr == 0) b[4] += m;
                    else if (irr == 1) b[5] += m;
                    else b[6] += m;
                }

        var order = d.Keys.OrderBy(x => x).ToArray();
        double[] distinct = order.ToArray();
        int[] total = order.Select(e => d[e][0]).ToArray();
        int[] n1 = order.Select(e => d[e][1]).ToArray();
        int[] n2 = order.Select(e => d[e][2]).ToArray();
        int[] n3 = order.Select(e => d[e][3]).ToArray();
        int[] nA = order.Select(e => d[e][4]).ToArray();
        int[] nT = order.Select(e => d[e][5]).ToArray();
        int[] nG = order.Select(e => d[e][6]).ToArray();
        return (distinct, total, n1, n2, n3, nA, nT, nG);
    }

    /// <summary>Drop zero-multiplicity entries and zero eigenvalues; keep the non-zero sub-spectrum.</summary>
    private static (double[] Distinct, int[] Mult) SubSpectrum(double[] distinct, int[] subMult)
    {
        var d = new List<double>();
        var m = new List<int>();
        for (int i = 0; i < distinct.Length; i++)
            if (subMult[i] > 0 && distinct[i] > 1e-9)
            {
                d.Add(distinct[i]);
                m.Add(subMult[i]);
            }
        return (d.ToArray(), m.ToArray());
    }

    private static int SRun(double[] distinct, int[] mult, double mu, double beta)
        => BoundedInnovationAnalyzer.RunDistinct("D96^3", distinct, mult,
            mutationRate: mu, crowding: beta).FinalSpecies;

    private static int SOf(string model, Func<SpectralCase> c, double mu, double beta)
        => SpectralCaseCatalog.SInfinity(c(), mu, beta);

    // ── 1. D96^3 equilibrium survivors ──────────────────────────────────────

    [Fact]
    public void Y_T_012_D96CubedSurvivors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var (distinct, total, _, _, _, _, _, _) = Breakdown();

        // The 3D tensor product is a genuine cubic lattice: 96³ modes over A distinct
        // eigenvalues, with octahedral degeneracies.
        Assert.Equal(96 * 96 * 96, total.Sum());
        Assert.True(distinct.Length > 44, "D96^3 has more distinct eigenvalues than D96 (44)");

        // Natural survivors (μ=0.01, β=1) — the key quantity under test.
        int s = SRun(distinct, total, 0.01, 1.0);
        Assert.True(s > 0);
        // D96^3 must exceed the 1D D96 survivor count (5): a denser spectrum keeps more species.
        Assert.True(s > SOf("D96", SpectralCaseCatalog.D96, 0.01, 1.0),
            $"D96^3 survivors ({s}) must exceed D96 (5)");
    }

    // ── 2. Sector- and irrep-specific survivors ─────────────────────────────

    [Fact]
    public void Y_T_012_SectorIrrepDecomposition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var (distinct, _, n1, n2, n3, nA, nT, nG) = Breakdown();

        var (d1, m1) = SubSpectrum(distinct, n1);
        var (d2, m2) = SubSpectrum(distinct, n2);
        var (d3, m3) = SubSpectrum(distinct, n3);
        var (dA, mA) = SubSpectrum(distinct, nA);
        var (dT, mT) = SubSpectrum(distinct, nT);
        var (dG, mG) = SubSpectrum(distinct, nG);

        int s1 = SRun(d1, m1, 0.01, 1.0);
        int s2 = SRun(d2, m2, 0.01, 1.0);
        int s3 = SRun(d3, m3, 0.01, 1.0);
        int sA = SRun(dA, mA, 0.01, 1.0);
        int sT = SRun(dT, mT, 0.01, 1.0);
        int sG = SRun(dG, mG, 0.01, 1.0);

        // Each sector/irrep sub-landscape is non-empty and saturates to a positive count.
        Assert.True(d1.Length > 0 && d2.Length > 0 && d3.Length > 0);
        Assert.True(dA.Length > 0 && dT.Length > 0 && dG.Length > 0);
        Assert.All(new[] { s1, s2, s3, sA, sT, sG }, x => Assert.True(x > 0));

        // The 3-axis (cubic) sector dominates the survivor set — it is the densest sub-spectrum
        // and yields the count nearest 19 (S∞=21, distance 2).
        Assert.True(d3.Length > d1.Length && d3.Length > d2.Length, "3-axis sector is the densest");
        Assert.InRange(Math.Abs(s3 - 19), 0, 3);
    }

    // ── 3. Cumulative discovery (fittest-init) ──────────────────────────────

    [Fact]
    public void Y_T_012_Discovery()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var (distinct, total, _, _, _, _, _, _) = Breakdown();
        var r = BoundedInnovationAnalyzer.RunDistinct("D96^3", distinct, total,
            mutationRate: 0.01, crowding: 1.0, startFromFittest: true);

        Assert.True(r.CumulativeSpecies >= 1);
        Assert.True(r.CumulativeSpecies <= distinct.Length);
    }

    // ── 4. Does 19 emerge naturally? ────────────────────────────────────────

    [Fact]
    public void Y_T_012_DoesNineteenEmerge()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var (distinct, total, _, _, _, _, _, _) = Breakdown();
        int s3d = SRun(distinct, total, 0.01, 1.0);
        int s1d = SOf("D96", SpectralCaseCatalog.D96, 0.01, 1.0);
        int sRandom = SOf("random", SpectralCaseCatalog.Random, 0.01, 1.0);

        // D96^3 (S∞=16) moves substantially toward 19 from D96 (5): |16−19|=3 &lt;&lt; |5−19|=14.
        Assert.True(Math.Abs(s3d - 19) < Math.Abs(s1d - 19),
            $"D96^3 ({s3d}) must be nearer 19 than D96 ({s1d})");

        // But 19 is NOT hit exactly without tuning: the nearest natural value is still the
        // random control (17, distance 2).
        Assert.True(Math.Abs(sRandom - 19) <= Math.Abs(s3d - 19),
            $"random ({sRandom}) is at least as near 19 as D96^3 ({s3d})");
        Assert.True(Math.Abs(s3d - 19) > 0, "D96^3 does not exactly reproduce 19");
    }

    // ── 5. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_012_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var (distinct, total, _, _, _, _, _, _) = Breakdown();
        int s3d = SRun(distinct, total, 0.01, 1.0);
        int s1d = SOf("D96", SpectralCaseCatalog.D96, 0.01, 1.0);

        // DERIVED: the D96^3 spectrum (20,811 distinct eigenvalues) and its S∞=16 are a
        // deterministic function of the tensor-product structure (T_008/T_009); the raise
        // 5 → 16 is a derived 3D-density-of-states effect.
        Assert.True(distinct.Length - 1 > 10000, "D96^3 has a large distinct spectrum");
        Assert.True(s3d > s1d, "D96^3 raises the survivor count above D96");

        // CORRESPONDENCE: 16 is within the legacy ±5 consistency tolerance (AT-139) of 19,
        // so D96^3 order-of-magnitude MATCHES the historical value; D96 (5) does not.
        Assert.InRange(Math.Abs(s3d - 19), 0, 5);
        Assert.True(Math.Abs(s1d - 19) > 5, "D96 (5) is outside ±5 of 19");

        // REFUTED: "D96^3 exactly derives 19 without tuning" — it gives 16 (or 21 in the
        // cubic 3-axis sector), and random (17) remains the nearest natural value.
        Assert.NotEqual(19, s3d);
    }

    // ── 6. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_012_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_012 — Origin of 19 Audit");

        sb.AppendLine("Question: can the historical ~19-species count emerge from D96^3 rather than D96?");
        sb.AppendLine("Models: D96 (1D), D96^3 (cubic ⊗), D96^3 with Oh irreps, D96^3 with sector");
        sb.AppendLine("splitting, random control. Deterministic (μ=0.01, β=1).");
        sb.AppendLine();

        var (distinct, total, n1, n2, n3, nA, nT, nG) = Breakdown();

        sb.AppendLine("[1] Landscape inventory");
        sb.AppendLine($"  D96 (1D):            A = {SpectralCaseCatalog.D96().A} distinct non-zero eigenvalues");
        sb.AppendLine($"  D96^3 (cubic ⊗):     {total.Sum():N0} modes over {distinct.Length - 1} non-zero distinct eigenvalues");
        sb.AppendLine($"  random control:      A = {SpectralCaseCatalog.Random().A}");
        sb.AppendLine();

        sb.AppendLine("[2] Equilibrium survivors (μ=0.01, β=1)");
        int s1d = SOf("D96", SpectralCaseCatalog.D96, 0.01, 1.0);
        int s3d = SRun(distinct, total, 0.01, 1.0);
        int sRandom = SOf("random", SpectralCaseCatalog.Random, 0.01, 1.0);
        sb.AppendLine($"  D96 (1D)    → S∞ = {s1d}");
        sb.AppendLine($"  D96^3       → S∞ = {s3d}");
        sb.AppendLine($"  random      → S∞ = {sRandom}");
        sb.AppendLine($"  target      → 19");
        sb.AppendLine();

        sb.AppendLine("[3] Sector splitting (axis-count) survivors");
        var (d1, m1) = SubSpectrum(distinct, n1);
        var (d2, m2) = SubSpectrum(distinct, n2);
        var (d3, m3) = SubSpectrum(distinct, n3);
        sb.AppendLine($"  1-axis (axial)   A={d1.Length,4}  S∞ = {SRun(d1, m1, 0.01, 1.0)}");
        sb.AppendLine($"  2-axis (planar)  A={d2.Length,4}  S∞ = {SRun(d2, m2, 0.01, 1.0)}");
        sb.AppendLine($"  3-axis (cubic)   A={d3.Length,4}  S∞ = {SRun(d3, m3, 0.01, 1.0)}");
        sb.AppendLine();

        sb.AppendLine("[4] Oh irrep (permutation-orbit) survivors");
        var (dA, mA) = SubSpectrum(distinct, nA);
        var (dT, mT) = SubSpectrum(distinct, nT);
        var (dG, mG) = SubSpectrum(distinct, nG);
        sb.AppendLine($"  A ({{a,a,a}})  A={dA.Length,4}  S∞ = {SRun(dA, mA, 0.01, 1.0)}");
        sb.AppendLine($"  T (two equal)  A={dT.Length,4}  S∞ = {SRun(dT, mT, 0.01, 1.0)}");
        sb.AppendLine($"  G (distinct)   A={dG.Length,4}  S∞ = {SRun(dG, mG, 0.01, 1.0)}");
        sb.AppendLine();

        sb.AppendLine("[5] Cumulative discovery (fittest-init, D96^3)");
        var disc = BoundedInnovationAnalyzer.RunDistinct("D96^3", distinct, total,
            mutationRate: 0.01, crowding: 1.0, startFromFittest: true);
        sb.AppendLine($"  cumulative = {disc.CumulativeSpecies},  survivor = {disc.FinalSpecies}");
        sb.AppendLine();

        sb.AppendLine("[6] Distance from 19 (no parameter tuning)");
        sb.AppendLine($"  |D96    − 19| = {Math.Abs(s1d - 19)}");
        sb.AppendLine($"  |D96^3  − 19| = {Math.Abs(s3d - 19)}");
        sb.AppendLine($"  |random − 19| = {Math.Abs(sRandom - 19)}");
        sb.AppendLine();

        sb.AppendLine("[7] Verdict");
        sb.AppendLine($"  D96^3 raises the survivor count from D96's 5 to {s3d} (distance {Math.Abs(s3d - 19)} from 19),");
        sb.AppendLine($"  but does NOT hit 19 without tuning; random ({sRandom}, distance {Math.Abs(sRandom - 19)}) is");
        sb.AppendLine($"  the nearest natural value. The cubic 3-axis sector gives 21 (distance 2).");
        sb.AppendLine($"  Verdict: D96^3 → 16 is DERIVED (3D DOS raise) + CORRESPONDENCE (within ±5 of 19,");
        sb.AppendLine($"  the AT-139 tolerance); 'D96^3 exactly derives 19' is REFUTED.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
