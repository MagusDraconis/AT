namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for physical spectrum correspondence analysis.
/// Compares Theta graph Laplacian spectra against known physical systems.
///
/// TQM-144: Physical Spectrum Correspondence
/// </summary>
public static class SpectrumCorrespondence
{
    /// <summary>
    /// A known physical model with analytic spectrum.
    /// </summary>
    public sealed record PhysicalModel(
        string Name,                        // "1D Tight-Binding", "2D Phonon", etc.
        string System,                      // "Electrons on a chain", "Lattice vibrations"
        string AnalyticFormula,             // "λ_k = -2t·cos(ka)"
        int Dimension,                      // 1, 2, 3
        double[] Spectrum);                 // computed eigenvalues for comparison

    /// <summary>
    /// Quantitative comparison between Theta and physical spectra.
    /// </summary>
    public sealed record SpectrumComparison(
        string ThetaGeometry,               // "1D Chain", "2D Square", etc.
        string PhysicalModel,               // which physical model compared
        double[] ThetaEigenvalues,          // from graph Laplacian
        double[] PhysicalEigenvalues,       // from physical model
        double PearsonR,                    // correlation coefficient
        double SpearmanRho,                 // rank correlation
        double RMSE,                        // root mean square error
        double SpectralOverlap,             // cosine similarity
        int ExactMatchCount,                // eigenvalues matching within 1%
        bool IsMathematicalIdentity,        // are they identical up to scaling?
        string Correspondence);             // "Identity", "Strong", "Moderate", "Weak", "None"

    /// <summary>
    /// Complete physical spectrum report.
    /// </summary>
    public sealed record PhysicalSpectrumReport(
        List<PhysicalModel> PhysicalModels,
        List<SpectrumComparison> Comparisons,
        int GeometriesTested,
        int PhysicalModelsTested,
        int IdentityMatches,                // how many are mathematical identities?
        int StrongMatches,                  // how many have strong correspondence?
        double MeanSpectralOverlap,
        bool PhysicalCorrespondenceExists,  // any significant match?
        bool NovelPredictionMade,           // does TQM predict anything new?
        string Classification,              // "A: No Physical Correspondence" ... "D: New Physical Prediction"
        string Verdict);
}
