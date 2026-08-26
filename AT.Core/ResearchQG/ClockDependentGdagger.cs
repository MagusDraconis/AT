namespace AT.Core.ResearchQG;

/// <summary>
/// QG-083 clock-dependent g†: for clock family i with γ_i = a(1+ε_i),
/// g†_i = c·d(ln γ_i)/dt/2π = cH/2π·(1 + dε_i/d ln a). The drift ε_i is a constant
/// upper bound over z=0→3, so dε_i/d ln a ≈ ε_i/ln(4) (linear ramp in ln a).
/// </summary>
public static class ClockDependentGdagger
{
    static readonly double DLnA_Z03 = Math.Log(4.0); // ln(a0/a3) = ln 4 over z=0→3

    /// <summary>g† correction factor 1 + dε/d ln a for a given drift bound ε.</summary>
    public static double CorrectionFactor(double epsilon) => 1.0 + epsilon / DLnA_Z03;

    /// <summary>g†_i(z) = cH(z)/2π × correction, in m/s².</summary>
    public static double Gdagger(double z, double epsilon)
    {
        double gA = GdaggerTimeDerivation.GdaggerFromClock(Cosmology.H(z));
        return gA * CorrectionFactor(epsilon);
    }

    /// <summary>Sensitivity row for one family.</summary>
    public static GdaggerSensitivityRow SensitivityRow(ClockFamily family, double epsilon)
    {
        double dEps = epsilon / DLnA_Z03;
        double corr = CorrectionFactor(epsilon);
        return new GdaggerSensitivityRow(family.Name, epsilon, dEps, corr,
            Gdagger(0.0, epsilon), Gdagger(3.0, epsilon));
    }
}
