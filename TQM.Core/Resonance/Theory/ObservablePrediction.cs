namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Computes physical observables from Q interaction graphs at various sizes,
/// fits scaling laws, and checks universality across geometries.
///
/// TQM-145: Physical Observables from Topological Charge
/// </summary>
public static class ObservablePrediction
{
    // ══════════════════════════════════════════════════════════════════
    // Compute observables for a 1D chain at varying Q.
    // ══════════════════════════════════════════════════════════════════

    public static List<ChargeObservable.PhysicalObservable> Compute1DChainObservables(int[] qSizes)
    {
        int nQ = qSizes.Length;
        var spectralGap = new double[nQ];
        var effectiveMass = new double[nQ];
        var totalEnergy = new double[nQ];
        var modeDensity = new double[nQ];
        var transportCoeff = new double[nQ];
        var infoCapacity = new double[nQ];
        var correlationLength = new double[nQ];

        for (int idx = 0; idx < nQ; idx++)
        {
            int Q = qSizes[idx];

            // Build 1D chain graph Laplacian.
            // Exact eigenvalues: λ_k = 2 - 2·cos(πk/(Q+1)), k=1..Q
            double lambda1 = 2.0 - 2.0 * Math.Cos(Math.PI / (Q + 1));
            double lambda2 = 2.0 - 2.0 * Math.Cos(2.0 * Math.PI / (Q + 1));

            // Spectral gap = λ_2 - λ_1.
            spectralGap[idx] = lambda2 - lambda1;

            // Effective mass: m_eff ∝ 1/gap (from dispersion E = p²/2m_eff).
            effectiveMass[idx] = spectralGap[idx] > 1e-10 ? 1.0 / spectralGap[idx] : double.MaxValue;

            // Total energy: sum of all eigenvalues = trace(L) = Σ deg(i) = 2(Q-1).
            totalEnergy[idx] = 2.0 * (Q - 1);

            // Mode density: Q modes per Q charges = 1 (always).
            modeDensity[idx] = 1.0;

            // Transport coefficient: λ_1 (slowest diffusion mode).
            transportCoeff[idx] = lambda1;

            // Information capacity: log₂(Q) eigenmodes.
            infoCapacity[idx] = Math.Log(Q) / Math.Log(2);

            // Correlation length: ξ ∝ 1/√(λ_1).
            correlationLength[idx] = lambda1 > 1e-10 ? 1.0 / Math.Sqrt(lambda1) : double.MaxValue;
        }

        var observables = new List<ChargeObservable.PhysicalObservable>();
        double[] qDouble = qSizes.Select(q => (double)q).ToArray();

        observables.Add(FitAndCreate("Spectral Gap Δ", qDouble, spectralGap,
            "Energy difference between ground and 1st excited state"));
        observables.Add(FitAndCreate("Effective Mass m_eff", qDouble, effectiveMass,
            "Inverse spectral gap, from E = p²/2m"));
        observables.Add(FitAndCreate("Total Energy E", qDouble, totalEnergy,
            "Sum of all coupling energies = 2(Q-1)"));
        observables.Add(FitAndCreate("Mode Density ρ", qDouble, modeDensity,
            "Eigenmodes per charge = 1 (constant)"));
        observables.Add(FitAndCreate("Transport Coefficient D", qDouble, transportCoeff,
            "Smallest eigenvalue = diffusion constant"));
        observables.Add(FitAndCreate("Information Capacity C", qDouble, infoCapacity,
            "log₂(Q) bits of information"));
        observables.Add(FitAndCreate("Correlation Length ξ", qDouble, correlationLength,
            "Inverse sqrt of spectral gap"));

        return observables;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute observables for a 2D square lattice.
    // ══════════════════════════════════════════════════════════════════

    public static List<ChargeObservable.PhysicalObservable> Compute2DObservables(int[] qSizes)
    {
        int nQ = qSizes.Length;
        var spectralGap = new double[nQ];
        var effectiveMass = new double[nQ];
        var totalEnergy = new double[nQ];
        var modeDensity = new double[nQ];

        for (int idx = 0; idx < nQ; idx++)
        {
            int Q = qSizes[idx];
            int n = (int)Math.Sqrt(Q); // approximate square

            // 2D Laplacian: λ_{kx,ky} = 4 - 2·cos(πkx/(n+1)) - 2·cos(πky/(n+1))
            double lambda1 = 4.0 - 2.0 * Math.Cos(Math.PI / (n + 1))
                                 - 2.0 * Math.Cos(Math.PI / (n + 1));
            double lambda2 = 4.0 - 2.0 * Math.Cos(2.0 * Math.PI / (n + 1))
                                 - 2.0 * Math.Cos(Math.PI / (n + 1));

            spectralGap[idx] = lambda2 - lambda1;
            effectiveMass[idx] = spectralGap[idx] > 1e-10 ? 1.0 / spectralGap[idx] : double.MaxValue;
            totalEnergy[idx] = 4.0 * Q; // approximate
            modeDensity[idx] = 1.0;
        }

        var observables = new List<ChargeObservable.PhysicalObservable>();
        double[] qDouble = qSizes.Select(q => (double)q).ToArray();

        observables.Add(FitAndCreate("Spectral Gap Δ (2D)", qDouble, spectralGap, "2D square lattice gap"));
        observables.Add(FitAndCreate("Effective Mass m_eff (2D)", qDouble, effectiveMass, "2D effective mass"));
        observables.Add(FitAndCreate("Total Energy E (2D)", qDouble, totalEnergy, "Total coupling energy"));
        observables.Add(FitAndCreate("Mode Density ρ (2D)", qDouble, modeDensity, "Constant mode density"));

        return observables;
    }

    // ══════════════════════════════════════════════════════════════════
    // Fit power law O = a·Q^b and create observable record.
    // ══════════════════════════════════════════════════════════════════

    private static ChargeObservable.PhysicalObservable FitAndCreate(
        string name, double[] qVals, double[] obsVals, string interpretation)
    {
        var (a, b, r2, scalingType) = FitPowerLaw(qVals, obsVals);

        bool isUniversal = r2 > 0.95;

        return new ChargeObservable.PhysicalObservable(
            name, qVals, obsVals,
            scalingType, b, r2, isUniversal, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Fit power law: O = a·Q^b using log-log linear regression.
    // ══════════════════════════════════════════════════════════════════

    private static (double a, double b, double r2, string type) FitPowerLaw(
        double[] q, double[] obs)
    {
        int n = Math.Min(q.Length, obs.Length);
        if (n < 2) return (0, 0, 0, "Undefined");

        // Check if constant.
        double obsMean = obs.Take(n).Average();
        double obsStd = Math.Sqrt(obs.Take(n).Average(x => (x - obsMean) * (x - obsMean)));
        if (obsStd < 1e-10) return (obsMean, 0, 1.0, "Constant");

        // Log-log regression: log(O) = b·log(Q) + log(a).
        double meanLogQ = 0, meanLogO = 0;
        for (int i = 0; i < n; i++)
        {
            meanLogQ += Math.Log(Math.Max(q[i], 1));
            meanLogO += Math.Log(Math.Max(obs[i], 1e-10));
        }
        meanLogQ /= n; meanLogO /= n;

        double cov = 0, varQ = 0;
        for (int i = 0; i < n; i++)
        {
            double dq = Math.Log(Math.Max(q[i], 1)) - meanLogQ;
            double dO = Math.Log(Math.Max(obs[i], 1e-10)) - meanLogO;
            cov += dq * dO;
            varQ += dq * dq;
        }

        double b = varQ > 1e-10 ? cov / varQ : 0;
        double logA = meanLogO - b * meanLogQ;
        double a = Math.Exp(logA);

        // R² of log-log fit.
        double ssRes = 0, ssTot = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = a * Math.Pow(q[i], b);
            ssRes += (obs[i] - pred) * (obs[i] - pred);
            ssTot += (obs[i] - obsMean) * (obs[i] - obsMean);
        }
        double r2 = ssTot > 1e-10 ? 1.0 - ssRes / ssTot : 0;

        string type = Math.Abs(b) < 0.01 ? "Constant"
                    : Math.Abs(b - 1.0) < 0.05 ? "Linear"
                    : Math.Abs(b + 1.0) < 0.05 ? "Inverse"
                    : b > 0 ? "Power-Law" : "Inverse-Power-Law";

        return (a, b, r2, type);
    }

    // ══════════════════════════════════════════════════════════════════
    // Build scaling law records.
    // ══════════════════════════════════════════════════════════════════

    public static List<ChargeObservable.ScalingLaw> BuildScalingLaws(
        List<ChargeObservable.PhysicalObservable> observables)
    {
        return observables.Select(o => new ChargeObservable.ScalingLaw(
            o.Name, 1.0, o.ScalingExponent, o.R2,
            $"{o.Name} ∝ Q^{o.ScalingExponent:F2}")).ToList();
    }
}
