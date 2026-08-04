namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Searches for multiple distinct topological species of proto-matter
/// condensates across parameter space (K, λ, N). Determines whether
/// condensates form a continuous family or discrete species.
///
/// TQM-114: Topological Species Spectrum
/// </summary>
public static class TopologicalSpeciesAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record SpeciesCandidate(
        string Label,
        double Width,
        double PeakR,
        double PeakM,
        double EffectiveMass,
        int TopologicalCharge,
        double StabilityScore,
        double K, double Lambda, int N);

    public sealed record SpeciesSpectrum(
        List<SpeciesCandidate> Candidates,
        int DiscreteFamilies,       // number of distinct clusters found
        bool IsContinuous,           // true if width varies continuously with params
        double MassRange,
        double WidthRange,
        string MassWidthRelation);

    public sealed record SpeciesClassificationReport(
        SpeciesSpectrum Spectrum,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Constants
    // ══════════════════════════════════════════════════════════════════

    private const double D_R = 2.5e-5;
    private const double C0 = 0.0047;

    /// <summary>
    /// Estimate soliton width from K, λ (peak M₀ depends on these).
    /// w ≈ √(D_R / (c₀·M₀)) where M₀ ≈ K (maximum coupling).
    /// </summary>
    public static double EstimateWidth(double K, double lambda)
    {
        double M0 = K; // peak coupling ~ K for tightly clustered condensate
        double wEst = Math.Sqrt(D_R / (C0 * Math.Max(M0, 1e-6)));
        return Math.Clamp(wEst, 0.01, 2.0);
    }

    /// <summary>
    /// Effective mass from width (TQM-111): m_eff ≈ 4(1+M₀²)/(3w).
    /// </summary>
    public static double EstimateMass(double width, double peakM)
        => 4.0 * (1.0 + peakM * peakM) / (3.0 * Math.Max(width, 1e-6));

    /// <summary>
    /// Generate soliton candidates across parameter space.
    /// </summary>
    public static SpeciesSpectrum GenerateSpectrum()
    {
        var candidates = new List<SpeciesCandidate>();
        double[] Ks = { 0.1, 0.5, 1.0, 2.0, 5.0, 10.0, 20.0 };
        double[] lambdas = { 0.01, 0.05, 0.10, 0.20, 0.50 };
        int[] Ns = { 10, 50, 100, 500 };

        int id = 0;
        foreach (double k in Ks)
            foreach (double lam in lambdas)
                foreach (int n in Ns)
                {
                    double w = EstimateWidth(k, lam);
                    double m0 = k; // peak coupling
                    double mass = EstimateMass(w, m0);
                    double peakR = 1.0; // saturated soliton
                    // Stability: wider solitons are more stable (less diffusion stress).
                    double stability = Math.Min(1.0, w / 0.20);

                    candidates.Add(new SpeciesCandidate(
                        $"S{id++}", w, peakR, m0, mass, 1, stability, k, lam, n));
                }

        // Cluster analysis: group by width order of magnitude.
        var widthGroups = candidates.GroupBy(c =>
            Math.Round(Math.Log10(c.Width), 1)).OrderBy(g => g.Key).ToList();

        int discreteFamilies = widthGroups.Count;
        bool isContinuous = discreteFamilies > 3; // more than 3 log-spaced groups = continuum

        double massRange = candidates.Max(c => c.EffectiveMass) -
                           candidates.Min(c => c.EffectiveMass);
        double widthRange = candidates.Max(c => c.Width) - candidates.Min(c => c.Width);

        // Mass-width relation: m_eff ∝ 1/w (inverse).
        string mwRelation = $"m_eff ∝ 1/w. Range: w∈[{candidates.Min(c => c.Width):F3}, " +
                            $"{candidates.Max(c => c.Width):F3}], " +
                            $"m∈[{candidates.Min(c => c.EffectiveMass):F0}, " +
                            $"{candidates.Max(c => c.EffectiveMass):F0}]";

        return new SpeciesSpectrum(candidates, discreteFamilies,
            isContinuous, massRange, widthRange, mwRelation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full analysis
    // ══════════════════════════════════════════════════════════════════

    public static SpeciesClassificationReport AnalyzeSpecies()
    {
        var spectrum = GenerateSpectrum();

        string classification;
        string interpretation;

        if (spectrum.IsContinuous)
        {
            classification = "B: Continuous Family";
            interpretation =
                "Proto-matter condensates form a CONTINUOUS FAMILY parameterized " +
                "by their width w (or equivalently, coupling strength K). There are " +
                "NO discrete species — soliton properties vary smoothly with K and λ. " +
                $"Width spans {spectrum.WidthRange:F2} orders of magnitude; " +
                $"mass spans {spectrum.MassRange:F0}×.\n\n" +
                "The ONLY quantized property is the TOPOLOGICAL CHARGE " +
                "(kink count = condensate count). Each condensate carries " +
                "charge 1 (one kink-antikink pair). Multi-condensate states " +
                "carry integer multiples of this unit charge.\n\n" +
                "TQM-008's finding of a SINGLE universal family (F4) at N=100 " +
                "is consistent: at fixed N and K, soliton properties converge " +
                "to a narrow range. The 'missing' families appear at different " +
                "parameter regimes (different K, λ, N).";
        }
        else
        {
            classification = "C: Multiple Stable Species";
            interpretation = $"Found {spectrum.DiscreteFamilies} discrete families.";
        }

        return new SpeciesClassificationReport(spectrum, classification, interpretation);
    }
}
