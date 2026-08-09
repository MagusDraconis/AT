namespace TQM.Core.Research;

/// <summary>
/// Data types for X065b Abundance vs Identity Audit.
/// </summary>
public static class AbundanceIdentityMetrics
{
    public enum SplitStatus { NoDistinction, WeakDistinction, StrongDistinction, FundamentalSplit }

    public enum Category { Identity, Abundance, Mixed }

    public sealed record TQMResult(
        string Experiment, string Result,
        Category Category, string DerivationStatus,
        string Why);

    public sealed record SplitAnalysis(
        int IdentityCount, int AbundanceCount,
        int IdentityDerived, int AbundanceDerived,
        double IdentitySuccessRate, double AbundanceSuccessRate,
        string Pattern, SplitStatus Status, string Verdict);
}
