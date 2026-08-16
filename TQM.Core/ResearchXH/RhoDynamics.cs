namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-RHO Phase 0 — dynamical origin of ρ. Tests whether scale-free actualization, flux conservation, and
/// attractor dynamics determine ρ(x), and whether the α=0 (log-deficit) abundance law arises naturally.
/// No new primitives.
/// </summary>
public static class RhoDynamics
{
    /// <summary>Scale-free (self-similar) density ρ = ρ₀(r/r₀)^s.</summary>
    public static double ScaleFreeDensity(double r, double s, double rho0 = 1.0, double r0 = 1.0)
        => rho0 * Math.Pow(r / r0, s);

    /// <summary>Log density ρ = ρ̄ + c·ln(r/r₀) (the α=0 log-deficit density, rising outward).</summary>
    public static double LogDensity(double r, double c = 0.4, double rhoBar = 1.0, double r0 = 1.0)
        => rhoBar + c * Math.Log(r / r0);

    /// <summary>TQM acceleration a = −(1/d) d(ln ρ)/dr (central difference).</summary>
    public static double Acceleration3D(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => -(rho(r + h) - rho(r - h)) / (2.0 * h * d * rho(r));

    /// <summary>Rotation-curve proxy v² = r·|a|.</summary>
    public static double RotationCurve(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => r * Math.Abs(Acceleration3D(rho, r, d, h));

    /// <summary>
    /// Actualization flux F = ρ·v·r^(d−1) through radius r, with scale-free velocity v = v₀ r^β.
    /// Conservation (steady state, no sources) means F(r) = const.
    /// </summary>
    public static double Flux(Func<double, double> rho, double r, double beta = 0.0, double v0 = 1.0, int d = 3)
        => rho(r) * v0 * Math.Pow(r, beta + d - 1.0);

    // ── G4-RHO Phase 1: α-selection (entropy, RG fixed points) ───────────────────────

    /// <summary>Per-octave deficit fractions p_k ∝ λ^(−αk), normalized (Σ p_k = 1).</summary>
    public static double[] DeficitFractions(double alpha, int K = 8, double lambda = 1.5)
    {
        var p = new double[K];
        double sum = 0.0;
        for (int k = 0; k < K; k++) { p[k] = Math.Pow(lambda, -alpha * k); sum += p[k]; }
        for (int k = 0; k < K; k++) p[k] /= sum;
        return p;
    }

    /// <summary>Shannon entropy H(α) = −Σ p_k ln p_k of the per-octave deficit allocation.</summary>
    public static double Entropy(double alpha, int K = 8, double lambda = 1.5)
    {
        var p = DeficitFractions(alpha, K, lambda);
        double h = 0.0;
        foreach (double pk in p) if (pk > 0.0) h -= pk * Math.Log(pk);
        return h;
    }

    /// <summary>Per-octave deficit increments A_k ∝ λ^(−αk), normalized to total m₀.</summary>
    public static double[] Increments(double alpha, int K = 8, double lambda = 1.5, double m0 = 1.0)
    {
        var a = new double[K];
        double sum = 0.0;
        for (int k = 0; k < K; k++) { a[k] = Math.Pow(lambda, -alpha * k); sum += a[k]; }
        for (int k = 0; k < K; k++) a[k] *= m0 / sum;
        return a;
    }

    /// <summary>
    /// Effective α after block-spin coarse-graining (merging adjacent octaves). For increments
    /// A_k ∝ λ^(−αk), the merged increments have ratio λ^(−2α), so α is INVARIANT (all α are RG fixed points).
    /// </summary>
    public static double CoarseGrainedAlpha(double alpha, int K = 8, double lambda = 1.5)
    {
        var a = Increments(alpha, K, lambda);
        int k2 = K / 2;
        var ap = new double[k2];
        for (int k = 0; k < k2; k++) ap[k] = a[2 * k] + a[2 * k + 1];
        double ratio = ap[1] / ap[0];
        return -Math.Log(ratio) / (2.0 * Math.Log(lambda));
    }

    // ── G4-RHO Phase 2: evolution equation (entropy gradient flow, scale-space diffusion) ──

    /// <summary>dH/dα (central difference) — the entropy gradient driving the abundance-law evolution.</summary>
    public static double EntropyDerivative(double alpha, int K = 8, double lambda = 1.5, double h = 1e-4)
        => (Entropy(alpha + h, K, lambda) - Entropy(alpha - h, K, lambda)) / (2.0 * h);

    /// <summary>d²H/dα² (central difference) — stability of the entropy fixed point.</summary>
    public static double EntropySecondDerivative(double alpha, int K = 8, double lambda = 1.5, double h = 1e-4)
        => (Entropy(alpha + h, K, lambda) - 2.0 * Entropy(alpha, K, lambda) + Entropy(alpha - h, K, lambda)) / (h * h);

    /// <summary>
    /// One Euler step of scale-space diffusion ∂_t A_k = D·(A_{k+1} − 2A_k + A_{k−1}) (reflecting boundaries),
    /// conserving total deficit while equilibrating the per-octave increments toward uniformity (α=0).
    /// </summary>
    public static double[] DiffuseStep(double[] a, double d)
    {
        int k = a.Length;
        var b = new double[k];
        for (int i = 0; i < k; i++)
        {
            double left = (i == 0) ? a[i] : a[i - 1];        // Neumann ghost A[−1] = A[0]
            double right = (i == k - 1) ? a[i] : a[i + 1];   // Neumann ghost A[K] = A[K−1]
            b[i] = a[i] + d * (left - 2.0 * a[i] + right);
        }
        return b;
    }

    // ── G4-RHO Phase 3: microscopic origin (counting statistics / maximum likelihood) ──

    /// <summary>Log number of microstates ln W = N·H(α) (Stirling), for N deficit quanta over K octaves.</summary>
    public static double LogMicrostates(double alpha, double n = 1000.0, int k = 8, double lambda = 1.5)
        => n * Entropy(alpha, k, lambda);

    /// <summary>Shannon entropy of an increment vector (normalized to fractions).</summary>
    public static double EntropyOf(double[] a)
    {
        double sum = a.Sum();
        double h = 0.0;
        foreach (double x in a)
        {
            double p = x / sum;
            if (p > 0.0) h -= p * Math.Log(p);
        }
        return h;
    }

    // ── TQM-F Phase 1: indifference / scale-freeness (renormalization invariance) ─────

    /// <summary>Block-spin coarse-graining: merge adjacent octaves (pairs).</summary>
    public static double[] CoarseGrain(double[] a)
    {
        int k2 = a.Length / 2;
        var b = new double[k2];
        for (int k = 0; k < k2; k++) b[k] = a[2 * k] + a[2 * k + 1];
        return b;
    }

    /// <summary>Scale-setting (non-scale-free) abundance: a Gaussian bump at octave center.</summary>
    public static double[] GaussianAbundance(int K, int center, double sigma)
    {
        var a = new double[K];
        for (int k = 0; k < K; k++)
        {
            double z = (k - center) / sigma;
            a[k] = Math.Exp(-z * z);
        }
        return a;
    }

    /// <summary>Successive-increment ratio A_{k+1}/A_k of an abundance vector (constant for a power law).</summary>
    public static double[] SuccessiveRatios(double[] a)
    {
        var r = new double[a.Length - 1];
        for (int k = 0; k < r.Length; k++) r[k] = a[k + 1] / a[k];
        return r;
    }
}
