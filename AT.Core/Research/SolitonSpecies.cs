namespace AT.Core.Research;

/// <summary>
/// Data types for soliton species analysis.
/// AT-X006: Soliton Species Physics
/// </summary>
public static class SolitonSpecies
{
    public sealed record SolitonClass(
        string Name, string Morphology, double Stability,
        double Size, int NodeCount, bool IsPersistent,
        bool CollidesElastically, bool CarriesInformation);

    public sealed record SolitonEcologyReport(
        List<SolitonClass> SolitonClasses,
        int ClassCount, bool SolitonsAreSpecies,
        bool RicherThanLinearAT,
        string Classification, string Verdict);
}
