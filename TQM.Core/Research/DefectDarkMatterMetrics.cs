namespace TQM.Core.Research;

/// <summary>
/// Data types for X064 Defect Dark Matter.
/// </summary>
public static class DefectDarkMatterMetrics
{
    public enum DefectDMStatus { CannotExplain, WeakCandidate, StrongCandidate, FullyDerived }

    public sealed record DefectDMCandidate(
        string Name, string DefectType,
        double MassGeV, bool IsNeutral,
        bool IsStable, double RelicDensity,
        string Notes);

    public sealed record DMRequirement(
        string Requirement, string LCDMStatus,
        bool SatisfiedByDefect, string TQMExplanation);

    public sealed record DefectDMReport(
        List<DefectDMCandidate> Candidates,
        List<DMRequirement> Requirements,
        int SatisfiedCount, DefectDMStatus Status,
        string Verdict);
}
