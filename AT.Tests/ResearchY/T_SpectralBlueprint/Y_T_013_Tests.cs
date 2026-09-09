using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_013 — Compression Origin Audit.
///
/// Question: why does D96^3 compress 20,811 distinct eigenvalues into 16 survivors?
///
/// Measure the fitness distribution, multiplicity hierarchy, near-gap density, entropy
/// reduction, and survivor basin volume for D96 / D96^3 / random, and derive the compression
/// ratio C = distinct eigenvalues / survivors. Deterministic throughout.
/// </summary>
public class Y_T_013_Tests : ResearchTestBase
{
    public Y_T_013_Tests(ITestOutputHelper output) : base(output) { }

    // ── Measures ─────────────────────────────────────────────────────────────

    private sealed record Compression(
        string Model,
        int A,                 // distinct non-zero eigenvalues
        int SInf,              // survivors at μ=0.01, β=1
        double C,              // compression ratio A / S∞
        double Concentration,  // w_max / Σw
        double FitnessEntropy, // H(p), p = w/Σw
        double EntropyReduction, // ln(A) − H
        int NearGap,           // modes with λ ≤ 2λ₂
        int MaxMult,           // largest multiplicity
        double BasinFraction); // survivor multiplicity / total modes

    private static Compression Measure(SpectralCase c)
    {
        var lam = new List<double>();
        var m = new List<int>();
        for (int i = 0; i < c.Distinct.Length; i++)
            if (c.Distinct[i] > 1e-9)
            {
                lam.Add(c.Distinct[i]);
                m.Add(c.Multiplicities[i]);
            }

        int A = lam.Count;
        var result = SpectralCaseCatalog.RunCase(c, 0.01, 1.0);
        int s = result.FinalSpecies;
        double ratio = (double)A / s;

        double[] w = new double[A];
        double sumW = 0.0;
        for (int i = 0; i < A; i++)
        {
            w[i] = m[i] / lam[i];
            sumW += w[i];
        }
        double conc = w.Max() / sumW;
        double H = 0.0;
        foreach (double wi in w)
        {
            double p = wi / sumW;
            if (p > 1e-300) H -= p * Math.Log(p);
        }
        double entRed = Math.Log(A) - H;

        double gap = lam.Min();
        int nearGap = 0;
        for (int i = 0; i < A; i++)
            if (lam[i] <= gap * 2.0) nearGap += m[i];

        int maxMult = m.Max();
        int totalModes = m.Sum();
        double basin = (double)result.Survivors.Sum(k => m[k]) / totalModes;

        return new Compression(c.Name, A, s, ratio, conc, H, entRed, nearGap, maxMult, basin);
    }

    private static Compression[] All()
        => [Measure(SpectralCaseCatalog.D96()), Measure(SpectralCaseCatalog.D96Cubed()), Measure(SpectralCaseCatalog.Random())];

    // ── 1. Compression ratios are DERIVED from the spectrum ──────────────────

    [Fact]
    public void Y_T_013_CompressionRatio()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var m = All();

        // C = A/S∞ is a deterministic function of the spectrum (A the distinct-sum count,
        // S∞ the mutation–selection reach), hence well-defined per landscape.
        foreach (var x in m)
        {
            Assert.True(x.A > 0 && x.SInf > 0);
            Assert.InRange(x.C, 1.0, 1e6);
            Assert.True(Math.Abs(x.C - (double)x.A / x.SInf) < 1e-9);
        }
    }

    // ── 2. D96^3 compresses most ─────────────────────────────────────────────

    [Fact]
    public void Y_T_013_D96CubedCompressesMost()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = Measure(SpectralCaseCatalog.D96());
        var d963 = Measure(SpectralCaseCatalog.D96Cubed());
        var random = Measure(SpectralCaseCatalog.Random());

        // D96^3 has by far the largest landscape (A) and the largest compression ratio C.
        Assert.True(d963.A > d96.A && d963.A > random.A, "D96^3 has the largest A");
        Assert.True(d963.C > d96.C && d963.C > random.C, "D96^3 has the largest compression ratio");
        Assert.True(d963.A > 10000, "D96^3 A is huge (3D distinct-sum count)");
        Assert.InRange(d963.SInf, 10, 25);
    }

    // ── 3. The mechanism: huge A dominates, not fitness peakedness ───────────

    [Fact]
    public void Y_T_013_Mechanism()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = Measure(SpectralCaseCatalog.D96());
        var d963 = Measure(SpectralCaseCatalog.D96Cubed());
        var random = Measure(SpectralCaseCatalog.Random());

        // D96^3's A is huge because 3-way sums of 49 1D eigenvalues yield ~20k distinct values.
        Assert.True(d963.A > 100 * random.A, "D96^3 A dwarfs random's A");

        // The survivors are essentially the near-gap modes: S∞ ≈ near-gap density.
        Assert.InRange(Math.Abs(d963.SInf - d963.NearGap), 0, 4);

        // Compression is dominated by A, NOT fitness peakedness: the 1D D96 has the HIGHEST
        // entropy reduction (0.88, most peaked fitness) yet the LOWEST compression (8.8), while
        // D96^3 has a moderate entropy reduction (0.20) but compresses 150× more (1300).
        Assert.True(d96.EntropyReduction > d963.EntropyReduction,
            "D96 has the most peaked fitness, yet compresses less");
        Assert.True(d963.C > 100 * d96.C, "D96^3 compresses far more despite flatter fitness");
    }

    // ── 4. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_013_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = Measure(SpectralCaseCatalog.D96());
        var d963 = Measure(SpectralCaseCatalog.D96Cubed());
        var random = Measure(SpectralCaseCatalog.Random());

        // DERIVED: C = A/S∞ is a deterministic function of the spectrum (A distinct-sum count,
        // S∞ via T_008/T_009 reach laws). The values differ across landscapes.
        Assert.NotEqual((int)d96.C, (int)d963.C);
        Assert.NotEqual((int)d963.C, (int)random.C);

        // REFUTED: "C is a universal constant" — it varies from ~6 (random) to ~1300 (D96^3).
        Assert.True(d963.C > 100 * random.C, "C is not universal");
    }

    // ── 5. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_013_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_013 — Compression Origin Audit");

        sb.AppendLine("Question: why does D96^3 compress 20,811 eigenvalues into 16 survivors?");
        sb.AppendLine("Measure fitness, multiplicity hierarchy, near-gap density, entropy reduction,");
        sb.AppendLine("and survivor basin volume; derive C = A/S∞.");
        sb.AppendLine();

        sb.AppendLine("[1] Compression measures (μ=0.01, β=1)");
        sb.AppendLine("     model       A      S∞    C=A/S∞   conc   ent-red  near-gap  maxmult  basin");
        foreach (var x in All())
        {
            sb.AppendLine(
                $"     {x.Model,-8} {x.A,6} {x.SInf,6} {x.C,9:F1} {x.Concentration,6:F4} {x.EntropyReduction,8:F2} {x.NearGap,8} {x.MaxMult,7} {x.BasinFraction,7:F4}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] The mechanism");
        var d963 = Measure(SpectralCaseCatalog.D96Cubed());
        var d96 = Measure(SpectralCaseCatalog.D96());
        sb.AppendLine($"  D96^3 has A = {d963.A} distinct eigenvalues (3-way sums of 49 1D values),");
        sb.AppendLine($"  and S∞ = {d963.SInf} ≈ its near-gap density ({d963.NearGap}) — the survivors are");
        sb.AppendLine("  essentially the near-gap modes; the other ~20k eigenvalues go extinct.");
        sb.AppendLine($"  Compression ratio C = A/S∞ = {d963.C:F1}.");
        sb.AppendLine($"  Compression is dominated by A, NOT fitness peakedness: the 1D D96 has the");
        sb.AppendLine($"  highest entropy reduction ({d96.EntropyReduction:F2}) yet compresses least");
        sb.AppendLine($"  (C = {d96.C:F1}); D96^3 has a flatter fitness (ent-red {d963.EntropyReduction:F2})");
        sb.AppendLine("  but compresses ~150× more because its 3D sum structure creates a huge A.");
        sb.AppendLine();

        sb.AppendLine("[3] Verdict");
        sb.AppendLine("  DERIVED: C = A/S∞ is a deterministic function of the spectrum (A = distinct-sum");
        sb.AppendLine("           count; S∞ = mutation–selection reach ≈ near-gap density, T_008/T_009).");
        sb.AppendLine("  EMERGENT: the value C ≈ 1300 for D96^3 (vs ~9 for D96, ~6 for random).");
        sb.AppendLine("  REFUTED:  'C is set by fitness peakedness' (D96 is most peaked yet compresses");
        sb.AppendLine("            least — A, the landscape size, is the dominant factor).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
