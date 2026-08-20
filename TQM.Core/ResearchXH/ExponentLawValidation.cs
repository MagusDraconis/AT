namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 148 — Independent validation of the exponent law. QG147 constructed the linear law
/// p_eff = 6.760 − 1.473·Q + 4.706·T3 by fitting the lepton, up, and down sectors (exact 3-param / 3-point
/// reproduction). This phase asks: does the law correctly predict fermion sectors that were NOT used to
/// construct it?
///
/// Method (computational, fully deterministic): (1) NEUTRINO SECTOR — the only fully unseen fermion sector
/// (Q=0, T3=+1/2): predict its hierarchy exponent and compare with the observed value (ν3/ν1 = 500 ⇒
/// p = log(500)/log(4) ≈ 4.48); (2) UNSEEN SECTOR PREDICTIONS — the neutrino is the out-of-sample test;
/// (3) LEAVE-ONE-OUT VALIDATION — refit 2-parameter reduced models (p = p0 + k·T3 and p = p0 + k·Q) on two
/// sectors and predict the held-out third, reporting relative deviations (the 3-parameter law cannot be
/// LOO'd directly — it is a saturated 3-param/3-point interpolation); (4) OVERFITTING CHECK — 3 parameters
/// for 3 points is saturated interpolation; generalization is measured by the LOO mean deviation and the
/// neutrino deviation; (5) PREDICTIVE ACCURACY — the out-of-sample deviations.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class ExponentLawValidation
{
    /// <summary>The QG147 exponent law coefficients (p = p0 + a·Q + b·T3).</summary>
    public static (double P0, double A, double B) Law()
    {
        var fit = SectorExponentLaw.FitExponentLaw();
        return (fit.P0, fit.A, fit.B);
    }

    /// <summary>Observed neutrino hierarchy exponent (ν3/ν1 = 500).</summary>
    public static double NeutrinoObservedExponent()
        => SectorExponentLaw.NeutrinoObservedExponent();

    // ── 1. Neutrino sector ─────────────────────────────────────────────────────

    /// <summary>
    /// Neutrino prediction from the QG147 law (Q=0, T3=+1/2) and its relative deviation from the observed
    /// exponent. This is the fully out-of-sample test (neutrino was NOT used to fit the law).
    /// </summary>
    public static (double Predicted, double Observed, double Deviation) NeutrinoPrediction()
    {
        var (p0, a, b) = Law();
        double pred = p0 + a * 0.0 + b * 0.5;
        double obs = NeutrinoObservedExponent();
        return (pred, obs, Math.Abs(pred / obs - 1.0));
    }

    // ── 2. Leave-one-out validation (2-parameter reduced models) ───────────────

    /// <summary>
    /// Leave-one-out with a 2-parameter reduced model p = p0 + k·x (x = T3 or Q): refit on two sectors,
    /// predict the held-out third. Returns per-held-out relative deviations.
    /// </summary>
    public static (string HeldOut, double Deviation)[] LeaveOneOut(string predictor = "T3")
    {
        var sectors = SectorExponentLaw.SectorExponents();
        var result = new List<(string, double)>();
        foreach (var held in sectors)
        {
            var train = sectors.Where(s => s.Name != held.Name).ToArray();
            // fit p = p0 + k*x on 2 points
            double x1 = predictor == "T3" ? train[0].T3 : train[0].Q;
            double x2 = predictor == "T3" ? train[1].T3 : train[1].Q;
            double p1 = train[0].P, p2 = train[1].P;
            double denom = x1 - x2;
            double k = Math.Abs(denom) < 1e-9 ? 0 : (p1 - p2) / denom;
            double p0 = p1 - k * x1;
            double xh = predictor == "T3" ? held.T3 : held.Q;
            double pred = p0 + k * xh;
            double dev = Math.Abs(pred / held.P - 1.0);
            result.Add((held.Name, dev));
        }
        return result.ToArray();
    }

    /// <summary>Mean leave-one-out deviation for the given 2-parameter model.</summary>
    public static double MeanLooDeviation(string predictor = "T3")
        => LeaveOneOut(predictor).Average(x => x.Deviation);

    // ── 3. Overfitting check ────────────────────────────────────────────────────

    /// <summary>
    /// Overfitting indicator: the 3-parameter law is a SATURATED fit (3 params, 3 points — exact
    /// interpolation). Generalization is poor if the LOO mean deviation AND the neutrino deviation are both
    /// large.
    /// </summary>
    public static bool SaturatedFit()
        => SectorExponentLaw.LawReproducesSectors()   // exact on training
            && SectorExponentLaw.SectorExponents().Length == 3;

    // ── 4. Predictive accuracy ─────────────────────────────────────────────────

    /// <summary>
    /// Overall predictive accuracy: the mean of (neutrino deviation, best LOO mean deviation). A value &lt;
    /// 0.25 would indicate a predictive law.
    /// </summary>
    public static double OverallDeviation()
    {
        double nu = NeutrinoPrediction().Deviation;
        double looT3 = MeanLooDeviation("T3");
        double looQ = MeanLooDeviation("Q");
        return (nu + Math.Min(looT3, looQ)) / 2.0;
    }

    // ── Validation score & classification ──────────────────────────────────────

    /// <summary>
    /// Validation score (0..5):
    /// 1. the law reproduces its training sectors (sanity);
    /// 2. the neutrino prediction is within 50% of the observed exponent;
    /// 3. the T3-only leave-one-out mean deviation is &lt; 0.35;
    /// 4. the Q-only leave-one-out mean deviation is &lt; 0.35;
    /// 5. the overall deviation is &lt; 0.25 (a predictive law).
    /// </summary>
    public static int ValidationScore()
    {
        int score = 0;
        if (SectorExponentLaw.LawReproducesSectors()) score++;
        if (NeutrinoPrediction().Deviation < 0.50) score++;
        if (MeanLooDeviation("T3") < 0.35) score++;
        if (MeanLooDeviation("Q") < 0.35) score++;
        if (OverallDeviation() < 0.25) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   OVERFIT            — the law reproduces its training sectors exactly but fails out-of-sample
    ///                        (neutrino prediction and leave-one-out deviations are large);
    ///   PARTIAL VALIDATION — the law generalizes partially (some LOO cases reasonable) but the unseen
    ///                        neutrino sector is not well predicted;
    ///   PREDICTIVE LAW     — the law predicts unseen fermion sectors (neutrino and LOO) with small
    ///                        deviations — a genuine predictive law.
    /// </summary>
    public static string Classify()
    {
        int score = ValidationScore();
        if (score <= 2) return "OVERFIT";
        if (score == 5) return "PREDICTIVE LAW";
        return "PARTIAL VALIDATION";
    }
}
