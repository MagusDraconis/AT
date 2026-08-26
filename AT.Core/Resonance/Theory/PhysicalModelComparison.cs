namespace AT.Core.Resonance.Theory;

/// <summary>
/// Builds Theta graph Laplacian spectra for multiple geometries and compares
/// them against known physical spectra (tight-binding, phonons, vibrating strings).
///
/// AT-144: Physical Spectrum Correspondence
/// </summary>
public static class PhysicalModelComparison
{
    private const int N = 20;

    // ══════════════════════════════════════════════════════════════════
    // Build physical model spectra.
    // ══════════════════════════════════════════════════════════════════

    public static List<SpectrumCorrespondence.PhysicalModel> BuildPhysicalModels()
    {
        return new List<SpectrumCorrespondence.PhysicalModel>
        {
            new("1D Tight-Binding", "Electrons on a 1D chain",
                "λ_k = -2t·cos(ka), k=πn/(N+1)",
                1, Build1DTightBinding(N)),

            new("1D Vibrating String", "Fixed-end string harmonics",
                "λ_n = n², n=1,2,...,N",
                1, BuildVibratingString(N)),

            new("1D Phonon (Debye)", "Acoustic phonons on a chain",
                "ω_k ∝ |sin(ka/2)|, k=πn/(N+1)",
                1, Build1DPhonon(N)),

            new("2D Square Tight-Binding", "Electrons on a 2D square lattice",
                "λ_{nx,ny} = -2t·[cos(kx·a)+cos(ky·a)]",
                2, Build2DSquareTB(5, 4)),

            new("2D Graphene-like", "Dirac fermions on honeycomb",
                "E = ±t√(3+2cos(√3kya)+4cos(√3kxa/2)cos(kya/2))",
                2, BuildGrapheneLike(5, 4)),

            new("3D Cubic Tight-Binding", "Electrons on a 3D cubic lattice",
                "λ_{kx,ky,kz} = -2t·[cos(kx)+cos(ky)+cos(kz)]",
                3, Build3DCubicTB(3, 3, 2)),

            new("Coupled Oscillator Chain", "Classical coupled masses",
                "ω_k = 2√(K/m)·|sin(ka/2)|",
                1, BuildCoupledOscillators(N)),

            new("Spin-Wave (1D Heisenberg)", "Magnons on ferromagnetic chain",
                "ω_k = 2JS·(1-cos(ka))",
                1, BuildSpinWave(N)),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Analytic spectrum builders.
    // ══════════════════════════════════════════════════════════════════

    private static double[] Build1DTightBinding(int n)
    {
        var evals = new double[n];
        for (int k = 0; k < n; k++)
            evals[k] = -2.0 * Math.Cos(Math.PI * (k + 1) / (n + 1));
        return evals;
    }

    private static double[] BuildVibratingString(int n)
    {
        var evals = new double[n];
        for (int i = 0; i < n; i++)
            evals[i] = (i + 1.0) * (i + 1.0);
        return evals;
    }

    private static double[] Build1DPhonon(int n)
    {
        var evals = new double[n];
        for (int k = 0; k < n; k++)
            evals[k] = Math.Abs(Math.Sin(Math.PI * (k + 1) / (2.0 * (n + 1))));
        return evals;
    }

    private static double[] Build2DSquareTB(int nx, int ny)
    {
        int n = nx * ny;
        var evals = new List<double>();
        for (int kx = 0; kx < nx; kx++)
        for (int ky = 0; ky < ny; ky++)
            evals.Add(-2.0 * (Math.Cos(Math.PI * (kx + 1) / (nx + 1))
                             + Math.Cos(Math.PI * (ky + 1) / (ny + 1))));
        return evals.OrderBy(x => x).Take(n).ToArray();
    }

    private static double[] BuildGrapheneLike(int nx, int ny)
    {
        int n = Math.Min(nx * ny, 20);
        var evals = new double[n];
        // Approximate graphene dispersion with Dirac cones.
        for (int i = 0; i < n; i++)
        {
            double k = Math.PI * (i + 1) / (n + 1);
            evals[i] = Math.Sqrt(1 + 4 * Math.Cos(Math.Sqrt(3) * k) * Math.Cos(k)
                                + 4 * Math.Cos(k) * Math.Cos(k));
        }
        Array.Sort(evals);
        return evals;
    }

    private static double[] Build3DCubicTB(int nx, int ny, int nz)
    {
        int n = Math.Min(nx * ny * nz, 20);
        var evals = new List<double>();
        for (int kx = 0; kx < nx; kx++)
        for (int ky = 0; ky < ny; ky++)
        for (int kz = 0; kz < nz; kz++)
            evals.Add(-2.0 * (Math.Cos(Math.PI * (kx + 1) / (nx + 1))
                             + Math.Cos(Math.PI * (ky + 1) / (ny + 1))
                             + Math.Cos(Math.PI * (kz + 1) / (nz + 1))));
        return evals.OrderBy(x => x).Take(n).ToArray();
    }

    private static double[] BuildCoupledOscillators(int n)
    {
        var evals = new double[n];
        for (int k = 0; k < n; k++)
            evals[k] = Math.Abs(Math.Sin(Math.PI * (k + 1) / (2.0 * (n + 1))));
        return evals;
    }

    private static double[] BuildSpinWave(int n)
    {
        var evals = new double[n];
        for (int k = 0; k < n; k++)
            evals[k] = 2.0 * (1.0 - Math.Cos(Math.PI * (k + 1) / (n + 1)));
        return evals;
    }

    // ══════════════════════════════════════════════════════════════════
    // Build Theta graph Laplacian spectra.
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, double[]> BuildThetaSpectra()
    {
        var spectra = new Dictionary<string, double[]>();

        // 1D Chain: λ_k = 2 - 2·cos(πk/(N+1))
        var chainEvals = new double[N];
        for (int k = 0; k < N; k++)
            chainEvals[k] = 2.0 - 2.0 * Math.Cos(Math.PI * (k + 1) / (N + 1));
        spectra["1D Chain"] = chainEvals;

        // 1D Ring: λ_k = 2 - 2·cos(2πk/N)
        var ringEvals = new double[N];
        for (int k = 0; k < N; k++)
            ringEvals[k] = 2.0 - 2.0 * Math.Cos(2.0 * Math.PI * k / N);
        spectra["1D Ring"] = ringEvals;

        // 2D Square: λ_{kx,ky} = 4 - 2·cos(πkx/(Nx+1)) - 2·cos(πky/(Ny+1))
        int nx = 5, ny = 4;
        var sq = new List<double>();
        for (int kx = 0; kx < nx; kx++)
        for (int ky = 0; ky < ny; ky++)
            sq.Add(4.0 - 2.0 * Math.Cos(Math.PI * (kx + 1) / (nx + 1))
                       - 2.0 * Math.Cos(Math.PI * (ky + 1) / (ny + 1)));
        spectra["2D Square Lattice"] = sq.OrderBy(x => x).Take(N).ToArray();

        // 2D Hexagonal (triangular): λ_{kx,ky} = 6 - 2·Σcos(k·δ_i)
        var hex = new List<double>();
        for (int kx = 0; kx < nx; kx++)
        for (int ky = 0; ky < ny; ky++)
        {
            double kxa = Math.PI * (kx + 1) / (nx + 1);
            double kya = Math.PI * (ky + 1) / (ny + 1);
            double val = 6.0 - 2.0 * (Math.Cos(kxa) + Math.Cos(kya)
                         + Math.Cos(kxa + kya));
            hex.Add(val);
        }
        spectra["2D Hexagonal"] = hex.OrderBy(x => x).Take(N).ToArray();

        // 3D Cubic: λ = 6 - 2·[cos(kx)+cos(ky)+cos(kz)]
        int nnx = 3, nny = 3, nnz = 2;
        var cubic = new List<double>();
        for (int kx = 0; kx < nnx; kx++)
        for (int ky = 0; ky < nny; ky++)
        for (int kz = 0; kz < nnz; kz++)
            cubic.Add(6.0 - 2.0 * (Math.Cos(Math.PI * (kx + 1) / (nnx + 1))
                                  + Math.Cos(Math.PI * (ky + 1) / (nny + 1))
                                  + Math.Cos(Math.PI * (kz + 1) / (nnz + 1))));
        spectra["3D Cubic Lattice"] = cubic.OrderBy(x => x).Take(N).ToArray();

        return spectra;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compare Theta spectrum to physical model spectrum.
    // ══════════════════════════════════════════════════════════════════

    public static SpectrumCorrespondence.SpectrumComparison Compare(
        string geometryName, double[] thetaEvals,
        SpectrumCorrespondence.PhysicalModel model)
    {
        int n = Math.Min(thetaEvals.Length, model.Spectrum.Length);
        var t = thetaEvals.Take(n).ToArray();
        var p = model.Spectrum.Take(n).ToArray();

        // Correlation.
        double mt = t.Average(), mp = p.Average();
        double cov = 0, vt = 0, vp = 0;
        for (int i = 0; i < n; i++)
        {
            cov += (t[i] - mt) * (p[i] - mp);
            vt += (t[i] - mt) * (t[i] - mt);
            vp += (p[i] - mp) * (p[i] - mp);
        }
        double pearson = (vt > 1e-10 && vp > 1e-10) ? cov / Math.Sqrt(vt * vp) : 0;

        // Spearman rank correlation.
        var rankT = GetRanks(t);
        var rankP = GetRanks(p);
        double mrt = rankT.Average(), mrp = rankP.Average();
        double covR = 0, vrT = 0, vrP = 0;
        for (int i = 0; i < n; i++)
        {
            covR += (rankT[i] - mrt) * (rankP[i] - mrp);
            vrT += (rankT[i] - mrt) * (rankT[i] - mrt);
            vrP += (rankP[i] - mrp) * (rankP[i] - mrp);
        }
        double spearman = (vrT > 1e-10 && vrP > 1e-10) ? covR / Math.Sqrt(vrT * vrP) : 0;

        // RMSE (after normalizing both to [0,1]).
        double tMin = t.Min(), tMax = t.Max();
        double pMin = p.Min(), pMax = p.Max();
        double tRange = tMax - tMin, pRange = pMax - pMin;
        double rmse = 0;
        for (int i = 0; i < n; i++)
        {
            double tn = tRange > 1e-10 ? (t[i] - tMin) / tRange : 0;
            double pn = pRange > 1e-10 ? (p[i] - pMin) / pRange : 0;
            rmse += (tn - pn) * (tn - pn);
        }
        rmse = Math.Sqrt(rmse / n);

        // Spectral overlap (cosine similarity).
        double dot = 0, nt = 0, np2 = 0;
        for (int i = 0; i < n; i++) { dot += t[i] * p[i]; nt += t[i] * t[i]; np2 += p[i] * p[i]; }
        double overlap = (nt > 1e-10 && np2 > 1e-10) ? dot / Math.Sqrt(nt * np2) : 0;

        // Exact matches (within 1% after normalization).
        int exactMatches = 0;
        for (int i = 0; i < n; i++)
        {
            double tn = tRange > 1e-10 ? (t[i] - tMin) / tRange : 0;
            double pn = pRange > 1e-10 ? (p[i] - pMin) / pRange : 0;
            if (Math.Abs(tn - pn) < 0.01) exactMatches++;
        }

        // Is it a mathematical identity? Check if t = a·p + b.
        bool isIdentity = pearson > 0.999 && rmse < 0.02;

        string correspondence = isIdentity ? "Identity"
                              : pearson > 0.95 ? "Strong"
                              : pearson > 0.7 ? "Moderate"
                              : pearson > 0.3 ? "Weak" : "None";

        return new SpectrumCorrespondence.SpectrumComparison(
            geometryName, model.Name, t, p,
            pearson, spearman, rmse, overlap,
            exactMatches, isIdentity, correspondence);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run all comparisons.
    // ══════════════════════════════════════════════════════════════════

    public static List<SpectrumCorrespondence.SpectrumComparison> RunAllComparisons()
    {
        var thetaSpectra = BuildThetaSpectra();
        var physicalModels = BuildPhysicalModels();
        var comparisons = new List<SpectrumCorrespondence.SpectrumComparison>();

        foreach (var (geoName, thetaEvals) in thetaSpectra)
        foreach (var model in physicalModels)
        {
            comparisons.Add(Compare(geoName, thetaEvals, model));
        }

        return comparisons;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers.
    // ══════════════════════════════════════════════════════════════════

    private static double[] GetRanks(double[] values)
    {
        int n = values.Length;
        var indexed = values.Select((v, i) => (v, i)).OrderBy(x => x.v).ToArray();
        var ranks = new double[n];
        for (int i = 0; i < n; i++)
            ranks[indexed[i].i] = i + 1;
        return ranks;
    }
}
