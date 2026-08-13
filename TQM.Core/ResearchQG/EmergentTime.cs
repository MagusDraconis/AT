namespace TQM.Core.ResearchQG;

/// <summary>QG-089 emergent time: t = ∫ dN/R, where N = ∫ R dt is the accumulated "phase".
/// This is a TAUTOLOGY: it inverts the definition N = ∫ R dt. Time does not 'emerge' from the
/// rate — it is the parameter the rate is a function of. Reconstructing t from R recovers t
/// exactly because R(t) already requires t.</summary>
public static class EmergentTime
{
    /// <summary>Accumulated phase N(t) = ∫ R dt (the 'tick count').</summary>
    public static double Phase(double z, int n = 20000)
    {
        // ∫ H dt from z to 0 = ∫ dz/((1+z)H) — the same integral as conformal/age time.
        double result = 0, dz = z / n;
        for (int i = 0; i < n; i++)
        {
            double z1 = i * dz, z2 = (i + 1) * dz;
            result += 0.5 * (1.0 / ((1 + z1) * CosmicRateModel.Rate(z1)) + 1.0 / ((1 + z2) * CosmicRateModel.Rate(z2))) * dz;
        }
        return result; // seconds (per Hubble normalization units)
    }

    /// <summary>Reconstructing time from the rate: t = ∫ dN/R. Returns the age in Gyr.</summary>
    public static double ReconstructedTimeGyr(double z)
        => CosmicRateModel.AgeGyr(z); // identical by construction (the same integral)

    /// <summary>True iff reconstruction is exact (it always is — a tautology).</summary>
    public static bool ReconstructionIsExact(double z, double tol = 1e-6)
        => Math.Abs(ReconstructedTimeGyr(z) - CosmicRateModel.AgeGyr(z)) < tol;
}
