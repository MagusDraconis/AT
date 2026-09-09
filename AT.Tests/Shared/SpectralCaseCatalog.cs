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

    /// <summary>1D D96 eigenvalue λ_r for reduced index r ∈ 0..48 (the doublet λ_j = λ_{96−j} folded exactly).</summary>
    public static double[] D96Spectrum1DReduced()
    {
        var f = new double[49];
        for (int r = 0; r <= 48; r++)
        {
            double s = 0.0;
            for (int d = 1; d <= 6; d++) s += 1.0 - Math.Cos(2.0 * Math.PI * d * r / 96.0);
            f[r] = 2.0 * s;
        }
        return f;
    }

    /// <summary>1D multiplicity of reduced index r (1 for r = 0, 48; 2 otherwise).</summary>
    public static int Mult1DReduced(int r) => (r == 0 || r == 48) ? 1 : 2;

    /// <summary>Axis-count sector (1/2/3) of a triple of reduced indices (0 = zero mode).</summary>
    public static int D96CubedSector(int r1, int r2, int r3)
        => (r1 > 0 ? 1 : 0) + (r2 > 0 ? 1 : 0) + (r3 > 0 ? 1 : 0);

    /// <summary>Octahedral permutation-orbit class: 0=A ({a,a,a}), 1=T (two equal), 2=G (distinct).</summary>
    public static int D96CubedIrrep(int r1, int r2, int r3)
    {
        var nz = new List<int>(3);
        if (r1 > 0) nz.Add(r1);
        if (r2 > 0) nz.Add(r2);
        if (r3 > 0) nz.Add(r3);
        nz.Sort();
        if (nz.Count <= 1) return 1;
        if (nz.Count == 2) return nz[0] == nz[1] ? 1 : 2;
        if (nz[0] == nz[1] && nz[1] == nz[2]) return 0;
        if (nz[0] == nz[1] || nz[1] == nz[2]) return 1;
        return 2;
    }

    /// <summary>
    /// Full D96⊗D96⊗D96 decomposition: distinct eigenvalues ascending with total multiplicity
    /// and per-sector (n1,n2,n3) and per-irrep (nA,nT,nG) multiplicities. Built over the 49³
    /// reduced indices with the doublet degeneracy folded exactly (avoids floating-point
    /// splitting of the λ_j = λ_{96−j} pairs).
    /// </summary>
    public static (double[] Distinct, int[] Total, int[] N1, int[] N2, int[] N3, int[] NA, int[] NT, int[] NG)
        D96CubedBreakdown()
    {
        double[] f = D96Spectrum1DReduced();
        var d = new Dictionary<double, int[]>();   // [total, n1, n2, n3, nA, nT, nG]
        for (int r1 = 0; r1 <= 48; r1++)
            for (int r2 = 0; r2 <= 48; r2++)
                for (int r3 = 0; r3 <= 48; r3++)
                {
                    double e = f[r1] + f[r2] + f[r3];
                    int m = Mult1DReduced(r1) * Mult1DReduced(r2) * Mult1DReduced(r3);
                    int sec = D96CubedSector(r1, r2, r3);
                    int irr = D96CubedIrrep(r1, r2, r3);
                    if (!d.TryGetValue(e, out var b)) { b = new int[7]; d[e] = b; }
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

    /// <summary>Distinct eigenvalues and their multiplicities of D96⊗D96⊗D96 (plain spectrum).</summary>
    public static (double[] Distinct, int[] Multiplicities) TensorProductSpectrum96()
    {
        var (distinct, total, _, _, _, _, _, _) = D96CubedBreakdown();
        return (distinct, total);
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
