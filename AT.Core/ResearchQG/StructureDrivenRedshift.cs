namespace AT.Core.ResearchQG;

/// <summary>QG-088 structure-driven redshift: 1+z = S_obs/S_emit = a_obs^p/a_emit^p = (1+z)^p.
/// This equals the observed 1+z only for p = 1 (S = a, the reparametrization); for p = 3
/// (node count) it gives (1+z)³, excluded; for p = 0 (degree/dimension) there is no redshift.</summary>
public static class StructureDrivenRedshift
{
    /// <summary>Redshift from a structural variable with power p: 1+z_struct = (1+z)^p.</summary>
    public static double RedshiftFromStructure(double zObserved, double powerP)
        => Math.Pow(1 + zObserved, powerP) - 1.0;

    /// <summary>True iff the structural variable reproduces the observed redshift (p = 1).</summary>
    public static bool ReproducesRedshift(double powerP, double tolerance = 1e-3)
        => Math.Abs(powerP - 1.0) < tolerance;
}
