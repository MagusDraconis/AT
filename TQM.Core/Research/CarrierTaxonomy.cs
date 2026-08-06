namespace TQM.Core.Research;

/// <summary>
/// Data types for information carrier taxonomy.
/// TQM-X008: Information Carrier Taxonomy
/// </summary>
public static class CarrierTaxonomy
{
    public enum CarrierRegime { Linear, WeaklyNonlinear, StronglyNonlinear, Topological, Hybrid }

    public sealed record CarrierClass(
        string Name, string Morphology, CarrierRegime Regime,
        bool IsPersistent, bool IsLocalized, bool IsTopological,
        bool CarriesInformation, bool InteractsElastically,
        string ParentClass, int DiversityScore);

    public sealed record TaxonomyReport(
        List<CarrierClass> Classes,
        int TotalClasses, int LinearClasses,
        int NonlinearClasses, int TopologicalClasses,
        string RichestRegime, bool TaxonomyComplete,
        string Classification, string Verdict);
}
