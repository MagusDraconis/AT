namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-ME Phase 2 — collective deficit structures and the origin of long-range gravity.
/// The single-deficit field is a = −(1/d)∇ln ρ = +(1/d)∇m/ρ (localized ∝ ∇m). Here we ask whether a
/// COLLECTIVE (network / nested / abundance-law) deficit structure can produce an approximate 1/r² field.
/// All quantities are derived from the counting measure ρ only — no matter sector, no Einstein equations.
/// </summary>
public static class DeficitCollective
{
    /// <summary>3D radial TQM acceleration a = −(1/d) d(ln ρ)/dr = −(1/d) ρ′/ρ (central difference).</summary>
    public static double TqmAcceleration3D(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => -(rho(r + h) - rho(r - h)) / (2.0 * h * d * rho(r));

    /// <summary>Newtonian point-mass acceleration a = −M/r² (G = 1).</summary>
    public static double NewtonianPointMass(double M, double r) => -M / (r * r);

    /// <summary>Newtonian acceleration from a distributed deficit m = ρ̄−ρ: a = −M_encl(r)/r².</summary>
    public static double NewtonianAcceleration3D(Func<double, double> rho, double r, double rhoBar = 1.0, int n = 20000)
    {
        double dr = r / n;
        double M = 0.0;
        for (int i = 0; i < n; i++)
        {
            double rr = (i + 0.5) * dr;
            M += (rhoBar - rho(rr)) * 4.0 * Math.PI * rr * rr * dr;
        }
        return -M / (r * r);
    }

    /// <summary>Effective enclosed mass implied by a radial acceleration a: M_eff = −a·r².</summary>
    public static double EffectiveEnclosedMass(double a, double r) => -a * r * r;

    /// <summary>
    /// Smooth power-law deficit (continuum abundance-law limit): ρ = ρ̄ − m₀/(1 + r/r₀).
    /// The deficit m = m₀ r₀/(r₀+r) has a 1/r tail, so a_TQM ∝ −1/r² (Newtonian point-mass form).
    /// </summary>
    public static double PowerLawDeficit(double r, double rhoBar = 1.0, double m0 = 0.5, double r0 = 0.5)
        => rhoBar - m0 / (1.0 + r / r0);

    /// <summary>Localized Gaussian void ρ = 1 − A·e^(−(r/σ)²).</summary>
    public static double GaussianVoid(double r, double A = 0.5, double sigma = 0.3)
        => 1.0 - A * Math.Exp(-(r * r) / (sigma * sigma));

    /// <summary>Compact spherical void ρ = 1−A for r &lt; R, ρ = 1 outside.</summary>
    public static double CompactVoid(double r, double A = 0.3, double R = 0.5)
        => r < R ? 1.0 - A : 1.0;

    /// <summary>
    /// Self-similar nested void structure: geometric radii R_k = r₀λ^k, geometric amplitudes A_k = A₀λ^(−k),
    /// self-similar widths σ_k = σ₀λ^k. The logarithmic measure (one void per octave) makes the cumulative
    /// deficit m(r) ∝ 1/r, whose gradient gives the 1/r² field.
    /// </summary>
    public static double NestedVoidField(double r, double rhoBar = 1.0, double A0 = 0.4, double r0 = 0.5,
        double lambda = 1.5, double sigma0 = 0.2, int K = 10)
    {
        double m = 0.0;
        double Rk = r0, Ak = A0, sk = sigma0;
        for (int k = 0; k < K; k++)
        {
            double z = (r - Rk) / sk;
            m += Ak * Math.Exp(-z * z);
            Rk *= lambda;
            Ak /= lambda;
            sk *= lambda;
        }
        return rhoBar - m;
    }

    // ── G4-ME Phase 3: astrophysical profiles (rotation curves, finite cutoffs, populations) ──

    /// <summary>Rotation-curve proxy v²(r) = r·|a(r)| (circular-velocity squared for the TQM field).</summary>
    public static double RotationCurveProxy(Func<double, double> rho, double r, int d = 3, double h = 1e-6)
        => r * Math.Abs(TqmAcceleration3D(rho, r, d, h));

    /// <summary>Newtonian rotation curve v²(r) = M_encl(r)/r for a distributed deficit m = ρ̄−ρ.</summary>
    public static double NewtonianRotationCurve(Func<double, double> rho, double r, double rhoBar = 1.0, int n = 20000)
        => r * Math.Abs(NewtonianAcceleration3D(rho, r, rhoBar, n));

    /// <summary>
    /// Log-deficit profile ρ = ρ̄ − m₀·ln(Rmax/r)/ln(Rmax/r₀) for r₀ ≤ r ≤ Rmax (constant deficit per octave,
    /// finite-size cutoff at Rmax). Its field a = −m₀/(d·ρ·r·ln(Rmax/r₀)) ∝ −1/r, giving a FLAT rotation curve.
    /// </summary>
    public static double LogDeficit(double r, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5, double Rmax = 10.0)
    {
        if (r <= r0) return rhoBar - m0;
        if (r >= Rmax) return rhoBar;
        return rhoBar - m0 * Math.Log(Rmax / r) / Math.Log(Rmax / r0);
    }

    /// <summary>
    /// Discrete annular deficit hierarchy (constant amplitude per octave, finite K): a staircase deficit
    /// m(r) = m₀·(K−k)/K in octave k (R_k = r₀λ^k), the discrete form of LogDeficit m ∝ ln(Rmax/r).
    /// </summary>
    public static double AnnularDeficit(double r, double rhoBar = 1.0, double m0 = 0.4, double r0 = 0.5,
        double lambda = 1.5, int K = 8)
    {
        if (r <= r0) return rhoBar - m0;
        double Rk = r0;
        for (int k = 0; k < K; k++)
        {
            double Rnext = Rk * lambda;
            if (r >= Rk && r < Rnext)
                return rhoBar - m0 * (K - k) / (double)K;
            Rk = Rnext;
        }
        return rhoBar; // beyond Rmax = r0·λ^K
    }

    /// <summary>Least-squares linear fit of log y vs log x (power-law y ∝ x^slope); returns (slope, intercept).</summary>
    public static (double slope, double intercept) LogLogFit(double[] xs, double[] ys)
    {
        int n = xs.Length;
        double sx = 0, sy = 0, sxx = 0, sxy = 0;
        for (int i = 0; i < n; i++)
        {
            double x = Math.Log(xs[i]);
            double y = Math.Log(ys[i]);
            sx += x;
            sy += y;
            sxx += x * x;
            sxy += x * y;
        }
        double denom = n * sxx - sx * sx;
        double slope = (n * sxy - sx * sy) / denom;
        double intercept = (sy - slope * sx) / n;
        return (slope, intercept);
    }
}
