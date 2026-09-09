using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_010 — D96 Survivor Compression Audit.
///
/// Question: why does D96 saturate near 5 survivors while a random sparse landscape saturates
/// near 17? Can S∞ be predicted from spectral structure alone?
///
/// Measures (per case): survivor count S∞, spectral tail shape (near-gap mode density), fitness
/// entropy, multiplicity structure (degeneracy), and dominant-mode concentration. Deterministic.
/// </summary>
public class Y_T_010_Tests : ResearchTestBase
{
    public Y_T_010_Tests(ITestOutputHelper output) : base(output) { }

    // ── Structural measures ──────────────────────────────────────────────────

    private static double[] Normalized(double[] w)
    {
        double s = w.Sum();
        return w.Select(x => x / s).ToArray();
    }

    /// <summary>Shannon entropy (nats) of the normalized fitness distribution.</summary>
    private static double FitnessEntropy(double[] w)
    {
        double h = 0.0;
        foreach (double p in Normalized(w))
            if (p > 1e-300) h -= p * Math.Log(p);
        return h;
    }

    /// <summary>Dominant-mode concentration: the fittest mode's share of total fitness w_max/Σw.</summary>
    private static double Concentration(double[] w) => w.Max() / w.Sum();

    /// <summary>Herfindahl–Hirschman index Σ p_k² (1 = monopoly).</summary>
    private static double Herfindahl(double[] w) => Normalized(w).Sum(p => p * p);

    /// <summary>Spectral gap λ₂ (smallest positive eigenvalue).</summary>
    private static double Gap(double[] distinct)
    {
        double g = double.PositiveInfinity;
        foreach (double d in distinct) if (d > 1e-9) g = Math.Min(g, d);
        return g;
    }

    /// <summary>Number of MODES (with multiplicity) with eigenvalue ≤ factor·λ₂ — near-gap density.</summary>
    private static int NearGapModes(SpectralCase c, double factor = 2.0)
    {
        double g = Gap(c.Distinct);
        int count = 0;
        for (int i = 0; i < c.Distinct.Length; i++)
            if (c.Distinct[i] > 1e-9 && c.Distinct[i] <= g * factor) count += c.Multiplicities[i];
        return count;
    }

    /// <summary>Degeneracy fraction (N−A)/N — the share of modes sharing an eigenvalue.</summary>
    private static double DegeneracyFraction(SpectralCase c) => (double)(c.N - c.A) / c.N;

    /// <summary>Largest eigenvalue multiplicity.</summary>
    private static int MaxMultiplicity(SpectralCase c) => c.Multiplicities.Max();

    private static int S(SpectralCase c, double mu, double beta) => SpectralCaseCatalog.SInfinity(c, mu, beta);

    // ── 1. D96 compresses: peaked, concentrated, sparse ──────────────────────

    [Fact]
    public void Y_T_010_D96Compresses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();
        var random = SpectralCaseCatalog.Random();

        // D96's fitness field is more peaked: higher dominant-mode concentration, lower entropy.
        Assert.True(Concentration(d96.Fitness) > Concentration(random.Fitness),
            "D96 concentration must exceed random (peaked vs flat)");
        Assert.True(Herfindahl(d96.Fitness) > Herfindahl(random.Fitness),
            "D96 HHI must exceed random");
        Assert.True(FitnessEntropy(d96.Fitness) < FitnessEntropy(random.Fitness),
            "D96 fitness entropy must be below random");

        // …so D96 keeps FEWER species at equilibrium.
        Assert.True(S(d96, 0.01, 1.0) < S(random, 0.01, 1.0),
            "D96 must have fewer survivors than random");
    }

    // ── 2. The structural cause: sparse vs dense spectrum ────────────────────

    [Fact]
    public void Y_T_010_SpectralCause()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();
        var random = SpectralCaseCatalog.Random();

        // D96's circulant symmetry collapses its spectrum into few distinct eigenvalues
        // (high degeneracy), so near λ₂ there are only the two modes of the doublet.
        Assert.True(DegeneracyFraction(d96) > DegeneracyFraction(random),
            "D96 must be more degenerate than random");
        Assert.True(MaxMultiplicity(d96) > MaxMultiplicity(random),
            "D96 must have a higher eigenvalue multiplicity than random (all singletons)");

        // The random graph has ~95 distinct eigenvalues, so many modes sit within 2·λ₂ of the
        // gap; D96 has only the doublet. This near-gap density sets the fitness tail length.
        int d96Near = NearGapModes(d96);
        int randNear = NearGapModes(random);
        Assert.True(d96Near < randNear,
            $"D96 near-gap modes ({d96Near}) must be fewer than random ({randNear})");
    }

    // ── 3. S∞ is predictable from spectral structure alone (exact β=0) ───────

    [Fact]
    public void Y_T_010_PredictableFromSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        const double mu = 0.01;

        // At β = 0 the equilibrium is the Perron vector of M·diag(w) — a pure function of the
        // fitness spectrum {w} (hence of the spectral structure alone). It reproduces S∞ EXACTLY.
        foreach (var c in SpectralCaseCatalog.All())
        {
            int predicted = FitnessReachLaw.PredictedLinearEquilibrium(c.Fitness, mu);
            int simulated = S(c, mu, 0.0);
            Assert.Equal(predicted, simulated);
        }
    }

    // ── 4. No single scalar determines S∞ ────────────────────────────────────

    [Fact]
    public void Y_T_010_SingleScalarRefuted()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();
        var physical = SpectralCaseCatalog.Physical();

        // D96 and physical BOTH saturate at S∞ = 5, yet differ in landscape size A and in
        // fitness entropy/concentration — so no single scalar (A, entropy, concentration, λ₂)
        // is a bijection onto S∞. Only the full spectrum (Perron/threshold) determines it.
        Assert.Equal(S(d96, 0.01, 1.0), S(physical, 0.01, 1.0));
        Assert.NotEqual(d96.A, physical.A);
        Assert.NotEqual(FitnessEntropy(d96.Fitness), FitnessEntropy(physical.Fitness));
    }

    // ── 5. Classification ────────────────────────────────────────────────────

    [Fact]
    public void Y_T_010_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var d96 = SpectralCaseCatalog.D96();
        var random = SpectralCaseCatalog.Random();

        // DERIVED: the split (D96 ≈ 5, random ≈ 17) is a deterministic consequence of the
        // spectral tail — D96 peaked (sparse circulant spectrum) vs random flat (dense
        // near-degenerate spectrum) — via the exact Perron/threshold laws.
        Assert.True(S(d96, 0.01, 1.0) < S(random, 0.01, 1.0));
        Assert.True(Concentration(d96.Fitness) > Concentration(random.Fitness));

        // REFUTED: "a single spectral scalar predicts S∞" — D96 and physical share S∞ = 5
        // while differing in every single scalar (A, entropy, concentration).
        var physical = SpectralCaseCatalog.Physical();
        Assert.Equal(S(d96, 0.01, 1.0), S(physical, 0.01, 1.0));
    }

    // ── 6. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_T_010_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_010 — D96 Survivor Compression Audit");

        sb.AppendLine("Question: why does D96 saturate near 5 survivors while random near 17?");
        sb.AppendLine("Can S∞ be predicted from spectral structure alone?");
        sb.AppendLine();

        sb.AppendLine("[1] Structural measures (μ=0.01, β=1)");
        sb.AppendLine("     case             A    S∞    conc    HHI    ent    near-gap  degen  maxmult");
        foreach (var c in SpectralCaseCatalog.All())
        {
            sb.AppendLine(
                $"     {c.Name,-14} {c.A,4} {S(c, 0.01, 1.0),4} " +
                $"{Concentration(c.Fitness),6:F3} {Herfindahl(c.Fitness),6:F3} " +
                $"{FitnessEntropy(c.Fitness),6:F3} {NearGapModes(c),7} {DegeneracyFraction(c),8:F3} {MaxMultiplicity(c),7}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] The mechanism");
        sb.AppendLine("     · D96's circulant symmetry collapses 95 non-zero modes into 44 distinct");
        sb.AppendLine("       eigenvalues (degeneracy 0.542), so the spectrum near λ₂ is SPARSE — only");
        sb.AppendLine($"       {NearGapModes(SpectralCaseCatalog.D96())} modes within 2·λ₂.");
        sb.AppendLine("     · The random graph has ~95 distinct eigenvalues (degeneracy 0.010), so many");
        sb.AppendLine($"       modes ({NearGapModes(SpectralCaseCatalog.Random())}) sit within 2·λ₂.");
        sb.AppendLine("     · Fitness w = m/λ is therefore PEAKED for D96 (few high-fitness modes with");
        sb.AppendLine("       big gaps) and FLAT for random (many near-equal modes) → D96 keeps ~5,");
        sb.AppendLine("       random keeps ~17.");
        sb.AppendLine();

        sb.AppendLine("[3] Predictability");
        sb.AppendLine("     · β=0 Perron equilibrium S∞ = #{Perron(M·diag(w)) > ε} reproduces S∞ EXACTLY");
        sb.AppendLine("       for every case → S∞ IS a function of spectral structure alone (the full {w}).");
        sb.AppendLine("     · μ=0 threshold S∞ = #{w_k > Z*(β)} (T_009) brackets the interior value.");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusions");
        sb.AppendLine("  DERIVED:  the D96→~5 vs random→~17 split is a deterministic consequence of the");
        sb.AppendLine("            spectral tail (sparse/peaked vs dense/flat), via the exact reach laws.");
        sb.AppendLine("  EMERGENT: the precise integer (5 vs 17) at finite (μ, β) — no closed interior form.");
        sb.AppendLine("  REFUTED:  'a single spectral scalar predicts S∞' (D96 and physical both give 5 yet");
        sb.AppendLine("            differ in A, entropy, and concentration).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
