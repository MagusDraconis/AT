namespace TQM.Core.ResearchXD.Models;

/// <summary>
/// Models for Roman + Euclid Joint Forecast (ResearchXD-005).
/// </summary>
public static class ForecastModel
{
    /// <summary>CPL parameterization of TQM prediction.</summary>
    public sealed record CplPrediction(
        string Description,
        double W0, double Wa,
        double W0Uncertainty, double WaUncertainty,
        string FittingMethod);

    /// <summary>A survey-specific forecast.</summary>
    public sealed record SurveyForecast(
        string Survey, string PrimaryProbe,
        double SigmaW0, double SigmaWa,
        double TqmSignalW0, double TqmSignalWa,
        double SignificanceW0, double SignificanceWa,
        string Timeline,
        string Verdict);

    /// <summary>The combined constraint.</summary>
    public sealed record CombinedConstraint(
        string Combination,
        double SigmaW0, double SigmaWa,
        double EllipseArea,
        double DistanceFromLCDM, // in sigma
        double JointSignificance,
        string Classification);

    /// <summary>Validation thresholds.</summary>
    public sealed record ValidationThreshold(
        string Outcome, string Condition,
        string RequiredSigma, string Action,
        string Impact);

    /// <summary>The complete forecast.</summary>
    public sealed record JointForecast(
        string Title,
        CplPrediction Cpl,
        List<SurveyForecast> Surveys,
        List<CombinedConstraint> Combinations,
        List<ValidationThreshold> Thresholds,
        double BestCaseSignificance,
        string ForecastClass,
        string Verdict);
}
