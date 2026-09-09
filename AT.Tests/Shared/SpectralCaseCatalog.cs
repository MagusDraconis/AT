using AT.Core.ResearchT;

namespace AT.Tests.Shared;

/// <summary>A spectral-landscape test case for the ResearchY-T program.</summary>
public sealed record SpectralCase(
    string Name,
    int N,
    int A,
    double[] Fitness,
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
        return new SpectralCase("D96", 96, NonZeroCount(d), FitnessOf(d, m),
            () => AttractorDominanceAnalyzer.D96Ring(), null);
    }

    public static SpectralCase D963D()
    {
        var (d, m) = AttractorDominanceAnalyzer.Eigenspaces(AttractorDominanceAnalyzer.D963D(4, 4, 6));
        return new SpectralCase("D96-3D", 96, NonZeroCount(d), FitnessOf(d, m),
            () => AttractorDominanceAnalyzer.D963D(4, 4, 6), null);
    }

    public static SpectralCase Random()
    {
        var (d, m) = AttractorDominanceAnalyzer.Eigenspaces(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42));
        return new SpectralCase("random", 96, NonZeroCount(d), FitnessOf(d, m),
            () => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42), null);
    }

    public static SpectralCase Physical()
    {
        var spec = SpectralBlueprint.BuildSymmetric(96, x => (double)x);
        var (d, m) = AttractorDominanceAnalyzer.GroupSpectrum(spec.OrderBy(x => x).ToArray());
        return new SpectralCase("physical", 96, NonZeroCount(d), FitnessOf(d, m),
            null, () => SpectralBlueprint.BuildSymmetric(96, x => (double)x));
    }

    public static SpectralCase Unphysical()
    {
        var spec = SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0);
        var (d, m) = AttractorDominanceAnalyzer.GroupSpectrum(spec.OrderBy(x => x).ToArray());
        return new SpectralCase("unphysical", 96, NonZeroCount(d), FitnessOf(d, m),
            null, () => SpectralBlueprint.BuildSymmetric(96, x => x <= 16 ? 5.0 : x <= 32 ? 25.0 : 60.0));
    }

    public static SpectralCase[] All() => [D96(), D963D(), Random(), Physical(), Unphysical()];

    public static int SInfinity(SpectralCase c, double mu, double beta, int steps = BoundedInnovationAnalyzer.DefaultSteps)
        => c.Adjacency != null
            ? BoundedInnovationAnalyzer.Run(c.Name, c.Adjacency(), steps, mutationRate: mu, crowding: beta).FinalSpecies
            : BoundedInnovationAnalyzer.RunSpectrum(c.Name, c.Spectrum(), steps, mutationRate: mu, crowding: beta).FinalSpecies;
}
