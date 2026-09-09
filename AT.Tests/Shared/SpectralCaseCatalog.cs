using AT.Core.ResearchT;

namespace AT.Tests.Shared;

/// <summary>A spectral-landscape test case for the ResearchY-T program.</summary>
public sealed record SpectralCase(
    string Name,
    int N,
    int A,
    double[] Fitness,
    double[] Distinct,
    int[] Multiplicities,
    Func<double[,]> Adjacency,
    Func<double[]> Spectrum);

/// <summary>
/// Shared catalog of T-program spectral landscapes (D96, D96-3D, random, physical, unphysical)
/// with their fitness distributions w = m/λ and the saturated species count S∞ of the
/// replicator–mutator (BoundedInnovationAnalyzer). Used by T_008/T_009 to avoid duplication.
/// Fitness is in eigenvalue-ascending order (the mutation-ring order of the model).
/// </summary>
public static class SpectralCaseCatalog
{
    public static double[] FitnessOf(double[] distinct, int[] mult)
    {
        var w = new List<double>();
        for (int i = 0; i < distinct.Length; i++)
            if (distinct[i] > 1e-9) w.Add(mult[i] / distinct[i]);
        return w.ToArray();
    }

    public static int NonZeroCount(double[] distinct) => distinct.Count(d => d > 1e-9);

    public static SpectralCase D96()
    {
        var (d, m) = AttractorDominanceAnalyzer.Eigenspaces(AttractorDominanceAnalyzer.D96Ring());
        return new SpectralCase("D96", 96, NonZeroCount(d), FitnessOf(d, m), d, m,
            () => AttractorDominanceAnalyzer.D96Ring(), null);
    }

    public static SpectralCase D963D()
    {
        var (d, m) = AttractorDominanceAnalyzer.Eigenspaces(AttractorDominanceAnalyzer.D963D(4, 4, 6));
        return new SpectralCase("D96-3D", 96, NonZeroCount(d), FitnessOf(d, m), d, m,
            () => AttractorDominanceAnalyzer.D963D(4, 4, 6), null);
    }

    public static SpectralCase Random()
    {
        var (d, m) = AttractorDominanceAnalyzer.Eigenspaces(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42));
        return new SpectralCase("random", 96, NonZeroCount(d), FitnessOf(d, m), d, m,
            () => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), null);
    }

    public static SpectralCase Physical()
    {
        var spec = SpectralBlueprint.BuildSymmetric(96, x => (double)x);
        var (d, m) = AttractorDominanceAnalyzer.GroupSpectrum(spec.OrderBy(x => x).ToArray());
        return new SpectralCase("physical", 96, NonZeroCount(d), FitnessOf(d, m), d, m,
            null, () => SpectralBlueprint.BuildSymmetric(96, x => (double)x));
    }

    public static SpectralCase Unphysical()
    {
        var spec = SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0);
        var (d, m) = AttractorDominanceAnalyzer.GroupSpectrum(spec.OrderBy(x => x).ToArray());
        return new SpectralCase("unphysical", 96, NonZeroCount(d), FitnessOf(d, m), d, m,
            null, () => SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0));
    }

    public static SpectralCase[] All() => [D96(), D963D(), Random(), Physical(), Unphysical()];

    /// <summary>
    /// The D96 ⊗ D96 ⊗ D96 tensor-product (Cartesian) cubic lattice: spectrum is the Minkowski
    /// sum Λ = λ_i + λ_j + λ_k of three 1D D96 spectra, multiplicity the product of the 1D
    /// multiplicities (NP_037/NP_088). Fitness w = m/λ. Deterministic; no matrix materialized.
    /// </summary>
    public static SpectralCase D96Cubed()
    {
        var (distinct, mult) = TensorProductSpectrum96();
        return new SpectralCase("D96^3", 96 * 96 * 96, NonZeroCount(distinct), FitnessOf(distinct, mult),
            distinct, mult, null, null);
    }

    /// <summary>1D D96 spectrum λ_j (j = 0..95), λ_0 = 0, λ_j = λ_{96−j}.</summary>
    public static double[] D96Spectrum1D()
        => SpectralBlueprint.CirculantSpectrum(96, 6);

    /// <summary>
    /// Full D96⊗D96⊗D96 spectrum: distinct eigenvalues (ascending) and their multiplicities.
    /// Built by a single deterministic pass over the 96³ triples of 1D mode indices.
    /// </summary>
    public static (double[] Distinct, int[] Multiplicities) TensorProductSpectrum96()
    {
        double[] lam = D96Spectrum1D();
        var hist = new Dictionary<double, int>();
        for (int i = 0; i < 96; i++)
            for (int j = 0; j < 96; j++)
                for (int k = 0; k < 96; k++)
                {
                    double e = lam[i] + lam[j] + lam[k];
                    hist[e] = hist.TryGetValue(e, out int v) ? v + 1 : 1;
                }
        var pairs = hist.OrderBy(p => p.Key).ToArray();
        double[] distinct = pairs.Select(p => p.Key).ToArray();
        int[] mult = pairs.Select(p => p.Value).ToArray();
        return (distinct, mult);
    }

    public static int SInfinity(SpectralCase c, double mu, double beta, int steps = BoundedInnovationAnalyzer.DefaultSteps)
        => RunCase(c, mu, beta, steps, false).FinalSpecies;

    /// <summary>Full replicator–mutator result for a case (with optional discovery-mode init).</summary>
    public static InnovationResult RunCase(
        SpectralCase c,
        double mu,
        double beta,
        int steps = BoundedInnovationAnalyzer.DefaultSteps,
        bool startFromFittest = false)
        => c.Adjacency != null
            ? BoundedInnovationAnalyzer.Run(c.Name, c.Adjacency(), steps, mutationRate: mu, crowding: beta, startFromFittest: startFromFittest)
            : c.Spectrum != null
                ? BoundedInnovationAnalyzer.RunSpectrum(c.Name, c.Spectrum(), steps, mutationRate: mu, crowding: beta, startFromFittest: startFromFittest)
                : BoundedInnovationAnalyzer.RunDistinct(c.Name, c.Distinct, c.Multiplicities, steps, mutationRate: mu, crowding: beta, startFromFittest: startFromFittest);
}
