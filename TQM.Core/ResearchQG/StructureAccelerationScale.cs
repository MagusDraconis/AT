namespace TQM.Core.ResearchQG;

/// <summary>QG-088 structure-driven acceleration scale: a₀ = c × d ln(S)/dt = c × p·H.
/// For p = 1 (S=a) this is cH; for p = 3 (node count) it is 3cH; for p = 0 it is 0. All give
/// the 'cH class' order (no 1/(2π)), consistent with QG-084/087.</summary>
public static class StructureAccelerationScale
{
    public static double A0(double powerP, double h = 0) 
        => StructureConstants.C * powerP * (h == 0 ? StructureConstants.H0PerS : h);

    public static (string Name, double A0_m_s2)[] A0ForEach(StructuralVariable[] vars)
        => vars.Select(v => (v.Name, A0(v.PowerP))).ToArray();
}
