namespace TQM.Core.ResearchQG;

/// <summary>Model-comparison summary for a g†(z) hypothesis (TQM / MOND / null).</summary>
public sealed record TheoryComparison(
    string Model,
    double Chi2,
    double AIC,
    double BIC,
    int Nparams,
    int Ndata,
    double LogLikelihood);
