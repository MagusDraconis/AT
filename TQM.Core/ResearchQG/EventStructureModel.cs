namespace TQM.Core.ResearchQG;

/// <summary>QG-088 event-structure model: structural variables S(t) (connectivity, complexity,
/// causal density, information, network dimension) and their scaling with the scale factor a,
/// expressed as S ∝ a^p, so the structural evolution rate is d ln(S)/dt = p·H.</summary>
public sealed record StructuralVariable(
    string Name,
    string ScalingWithA,
    double PowerP,          // S ∝ a^p, so d ln S/dt = p·H
    string SourceNetwork);

/// <summary>Shared constants for structure cosmology.</summary>
public static class StructureConstants
{
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double C = 299792458.0;
    public static double H0PerS => H0 / 3.0857e19; // 2.184e-18 s^-1
}
