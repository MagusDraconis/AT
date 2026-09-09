using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_015 — Spectral Robustness Audit.
///
/// Question: is spectral robustness controlled by the near-gap density ρ(k) = N_gap(k)/N rather
/// than by the gap size λ₂?
///
/// For D96 / D96-3D / physical / unphysical / random / complete: compute ρ(k) for k = 1.5..4,
/// apply edge-removal / edge-addition / weight-noise perturbations, measure the λ₂ shift,
/// attractor-count shift, and spectral reordering, and correlate robustness with λ₂, m(λ₂), ρ(k).
/// Quantitative only (no AT assumptions); deterministic.
///
/// Verdict found: robustness is controlled by the DEGENERACY structure (number of degenerate
/// eigenvalues), not ρ or λ₂. H1 (gap size insufficient) SUPPORTED; H2 (low ρ predicts
/// robustness) REFUTED; H3 (D96 outperforms random) REFUTED.
/// </summary>
public class Y_T_015_Tests : ResearchTestBase
{
    public Y_T_015_Tests(ITestOutputHelper output) : base(output) { }

    private static readonly string[] Models = ["D96", "D96-3D", "physical", "unphysical", "random", "complete"];

    // ── Model adjacencies ────────────────────────────────────────────────────

    private static double[,] CirculantFromSpectrum(double[] spectrum, int n)
    {
        double[] w = SpectralBlueprint.ReconstructWeights(spectrum, n);
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int d = 1; d < n; d++)
            {
                int j = (i + d) % n;
                adj[i, j] += w[d];
            }
        return adj;
    }

    private static double[,] Adjacency(string model) => model switch
    {
        "D96" => AttractorDominanceAnalyzer.D96Ring(),
        "D96-3D" => AttractorDominanceAnalyzer.D963D(4, 4, 6),
        "physical" => CirculantFromSpectrum(SpectralBlueprint.BuildSymmetric(96, m => (double)m), 96),
        "unphysical" => CirculantFromSpectrum(SpectralBlueprint.BuildSymmetric(96, m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0), 96),
        "random" => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42),
        "complete" => GeneralInverseSpectrumAnalyzer.CompleteGraph(96),
        _ => throw new ArgumentOutOfRangeException(nameof(model)),
    };

    private static double[,] Clone(double[,] a)
    {
        var c = new double[a.GetLength(0), a.GetLength(1)];
        Array.Copy(a, c, a.Length);
        return c;
    }

    // ── Perturbations ────────────────────────────────────────────────────────

    private static double[,] RemoveEdge(double[,] adj)
    {
        var c = Clone(adj);
        int n = c.GetLength(0);
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (c[i, j] != 0.0) { c[i, j] = c[j, i] = 0.0; return c; }
        return c;
    }

    private static double[,] AddEdge(double[,] adj)
    {
        var c = Clone(adj);
        int n = c.GetLength(0);
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (c[i, j] == 0.0) { c[i, j] = c[j, i] = 1.0; return c; }
        return c;   // complete graph: no non-edge exists
    }

    private static double[,] WeightNoise(double[,] adj)
    {
        var c = Clone(adj);
        int n = c.GetLength(0);
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (c[i, j] != 0.0)
                {
                    double s = ((i + j) % 2 == 0) ? 1.0 : -1.0;
                    double w = c[i, j] * (1.0 + 0.1 * s);
                    c[i, j] = c[j, i] = w;
                }
        return c;
    }

    // ── Spectral quantities ──────────────────────────────────────────────────

    private static double[] Spec(double[,] adj)
        => GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(adj));

    private static double Gap(double[] evals)
    {
        double g = double.PositiveInfinity;
        foreach (double l in evals) if (l > 1e-9) g = Math.Min(g, l);
        return g;
    }

    private static int GapMult(double[] evals)
    {
        double g = Gap(evals);
        return evals.Count(l => Math.Abs(l - g) < 1e-6);
    }

    private static int DistinctCount(double[] evals)
    {
        var sorted = evals.OrderBy(x => x).ToArray();
        int c = 0;
        for (int i = 0; i < sorted.Length; i++)
            if (i == 0 || Math.Abs(sorted[i] - sorted[i - 1]) > 1e-6) c++;
        return c;
    }

    private static double Rho(double[] evals, double k)
    {
        double g = Gap(evals);
        int count = 0;
        foreach (double l in evals) if (l > 1e-9 && l <= k * g) count++;
        return (double)count / evals.Length;
    }

    /// <summary>Number of distinct non-zero eigenvalues with multiplicity &gt; 1 (degenerate levels).</summary>
    private static int DegenerateCount(double[] evals)
    {
        var sorted = evals.OrderBy(x => x).ToArray();
        int count = 0;
        for (int i = 0; i < sorted.Length;)
        {
            int j = i;
            while (j < sorted.Length && Math.Abs(sorted[j] - sorted[i]) < 1e-6) j++;
            if (sorted[i] > 1e-9 && j - i > 1) count++;
            i = j;
        }
        return count;
    }

    // ── Robustness measures ──────────────────────────────────────────────────

    private static (double GapShift, double AttractorShift, double SpectralShift) PerturbShift(
        double[] baseSpec, double[,] perturbed)
    {
        double[] p = Spec(perturbed);
        double bGap = Gap(baseSpec);
        double pGap = Gap(p);
        double gapShift = bGap > 0 ? Math.Abs(pGap - bGap) / bGap : 0.0;
        double attrShift = Math.Abs(DistinctCount(p) - DistinctCount(baseSpec));

        double normB = Math.Sqrt(baseSpec.Sum(x => x * x));
        double normP = Math.Sqrt(p.Sum(x => x * x));
        double spectralShift = normB > 0
            ? Math.Sqrt(baseSpec.Zip(p, (a, b) => (a - b) * (a - b)).Sum()) / normB
            : 0.0;

        return (gapShift, attrShift, spectralShift);
    }

    private sealed record Robustness(
        string Model, double Lambda2, int GapMult, double Rho2, double Rho4,
        double GapShift, double AttractorShift, double SpectralShift, int DegenerateCount);

    private static Robustness Analyze(string model)
    {
        double[,] adj = Adjacency(model);
        double[] spec = Spec(adj);

        var shifts = new List<(double g, double a, double s)>
        {
            PerturbShift(spec, RemoveEdge(adj)),
            PerturbShift(spec, AddEdge(adj)),
            PerturbShift(spec, WeightNoise(adj)),
        };

        return new Robustness(
            model, Gap(spec), GapMult(spec), Rho(spec, 2.0), Rho(spec, 4.0),
            shifts.Average(x => x.g), shifts.Average(x => x.a), shifts.Average(x => x.s),
            DegenerateCount(spec));
    }

    private static Robustness[] All() => Models.Select(Analyze).ToArray();

    // ── H1: gap size alone is insufficient ───────────────────────────────────

    [Fact]
    public void Y_T_015_H1_GapSizeInsufficient()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = All().Single(r => r.Model == "D96");
        var complete = All().Single(r => r.Model == "complete");
        var random = All().Single(r => r.Model == "random");

        // SUPPORTED: gap size λ₂ does not rank robustness. Two counterexamples:
        //  · complete has a LARGER gap (96) AND a LARGER gap shift (0.040) than D96 (0.386, 0.008)
        //    → a larger gap does NOT protect.
        //  · random has a LARGER gap (17.2) than D96 (0.386) yet a SMALLER gap shift (0.0003)
        //    → a larger gap does NOT necessarily hurt.
        Assert.True(complete.Lambda2 > d96.Lambda2 && complete.GapShift > d96.GapShift,
            "complete: larger gap yet MORE fragile (contradicts gap-size protection)");
        Assert.True(random.Lambda2 > d96.Lambda2 && random.GapShift < d96.GapShift,
            "random: larger gap yet MORE robust (contradicts gap-size fragility)");
    }

    // ── H2: low ρ does NOT predict robustness ────────────────────────────────

    [Fact]
    public void Y_T_015_H2_LowRhoRefuted()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = All().Single(r => r.Model == "D96");
        var complete = All().Single(r => r.Model == "complete");
        var random = All().Single(r => r.Model == "random");

        // REFUTED: near-gap density ρ is ANTI-correlated with attractor robustness. D96 has the
        // LOWEST ρ (0.021, isolated gap) yet the LARGEST attractor shift (30 — its doublets split
        // under a symmetry-breaking edge removal); complete has the HIGHEST ρ (0.99) yet a tiny
        // attractor shift (1 — a rank-1 perturbation splits only one level).
        Assert.True(d96.Rho2 < complete.Rho2, "D96 has lower ρ than complete");
        Assert.True(d96.AttractorShift > complete.AttractorShift,
            $"low-ρ D96 (ΔA={d96.AttractorShift}) is MORE fragile than high-ρ complete (ΔA={complete.AttractorShift})");

        // The actual controlling factor is the DEGENERACY structure: attractor shift tracks the
        // number of degenerate (multiplicity>1) eigenvalues, not ρ.
        Assert.True(d96.DegenerateCount > complete.DegenerateCount,
            "D96 has more degenerate eigenvalues than complete");
        Assert.Equal(0, random.DegenerateCount);   // random is all-singleton → no splitting
        Assert.Equal(0, random.AttractorShift);
    }

    // ── H3: D96 does NOT outperform random (even normalized) ─────────────────

    [Fact]
    public void Y_T_015_H3_D96NotBetterThanRandom()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = All().Single(r => r.Model == "D96");
        var random = All().Single(r => r.Model == "random");

        // REFUTED: random (all-singleton spectrum) is MORE robust than D96 (doublets split).
        // Even normalizing the spectral shift by the gap size, random stays ahead — D96's rigid
        // circulant symmetry is itself a fragility: one edge removal breaks it and splits ~30 modes.
        double d96Norm = d96.SpectralShift / (d96.Lambda2 + 1e-300);
        double randomNorm = random.SpectralShift / (random.Lambda2 + 1e-300);
        Assert.True(random.SpectralShift < d96.SpectralShift, "random has lower spectral shift");
        Assert.True(random.AttractorShift < d96.AttractorShift, "random has lower attractor shift");
        Assert.True(randomNorm < d96Norm, "random is more robust even after gap normalization");
    }

    // ── 4. Report ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_T_015_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_015 — Spectral Robustness Audit");

        sb.AppendLine("Question: is robustness controlled by near-gap density ρ(k) rather than gap size?");
        sb.AppendLine();

        sb.AppendLine("[1] Robustness table");
        sb.AppendLine("     model         λ₂       m(λ₂)   ρ(2)    ρ(4)    Δλ₂/λ₂   ΔA     spectral  degen");
        foreach (var r in All().OrderBy(r => r.Rho2))
        {
            sb.AppendLine($"     {r.Model,-12} {r.Lambda2,9:F4} {r.GapMult,5} {r.Rho2,6:F3} {r.Rho4,6:F3} " +
                          $"{r.GapShift,9:F4} {r.AttractorShift,5:F0} {r.SpectralShift,10:F4} {r.DegenerateCount,5}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] Hypotheses");
        var rows = All();
        var d96 = rows.Single(r => r.Model == "D96");
        var complete = rows.Single(r => r.Model == "complete");
        var random = rows.Single(r => r.Model == "random");
        sb.AppendLine($"  H1 gap size alone insufficient → SUPPORTED");
        sb.AppendLine($"     complete λ₂={complete.Lambda2:F1} MORE fragile (Δλ₂/λ₂={complete.GapShift:F3}) than");
        sb.AppendLine($"     D96 λ₂={d96.Lambda2:F3} (Δλ₂/λ₂={d96.GapShift:F3}); random λ₂={random.Lambda2:F1} MORE");
        sb.AppendLine($"     robust (Δλ₂/λ₂={random.GapShift:F4}) — λ₂ does not rank robustness.");
        sb.AppendLine($"  H2 low ρ predicts robustness → REFUTED");
        sb.AppendLine($"     D96 ρ(2)={d96.Rho2:F3} (low) yet ΔA={d96.AttractorShift} (doublets split);");
        sb.AppendLine($"     complete ρ(2)={complete.Rho2:F3} (high) yet ΔA={complete.AttractorShift}. The");
        sb.AppendLine($"     controlling factor is the DEGENERACY count (D96={d96.DegenerateCount}, random=0).");
        sb.AppendLine($"  H3 D96 outperforms random (normalized) → REFUTED");
        sb.AppendLine($"     random spectral {random.SpectralShift:F4} < D96 {d96.SpectralShift:F4} — D96's rigid");
        sb.AppendLine($"     circulant symmetry is itself a fragility (one edge breaks it).");
        sb.AppendLine();

        sb.AppendLine("[3] Verdict");
        sb.AppendLine("  DERIVED:  robustness (attractor shift) is set by the DEGENERACY structure — the number of");
        sb.AppendLine("            degenerate (multiplicity>1) eigenvalues that split under symmetry-breaking — a");
        sb.AppendLine("            deterministic function of the spectrum.");
        sb.AppendLine("  EMERGENT: the specific shift magnitudes.");
        sb.AppendLine("  REFUTED:  'gap size λ₂ predicts robustness' (H1); 'near-gap density ρ predicts robustness'");
        sb.AppendLine("            (H2); 'D96 outperforms random' (H3).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
