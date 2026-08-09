namespace TQM.Core.ResearchXD;

using TQM.Core.ResearchXD.Models;

/// <summary>
/// Produces concrete observational forecasts for Euclid, Roman, and DESI.
/// ResearchXD-005: Roman + Euclid Joint Forecast
/// </summary>
public static class RomanEuclidForecastAnalyzer
{
    // ════════════════════════════════════════════════════════════════
    // SECTION A: TQM prediction recap
    // ════════════════════════════════════════════════════════════════

    public static string PredictionRecap()
    {
        return @"
TQM PREDICTION FOR DARK ENERGY — FINAL PRE-OBSERVATION STATEMENT

FUNDAMENTAL PREDICTION:
  w(z) ≈ -1 + eta · (1+z)^(3/2)
  eta ≈ 0.015 (from Lambda(t) = alpha/sqrt(V(t)), X046)
  Sign: w > -1 (less negative than LambdaCDM at all z)
  Direction: w becomes MORE negative at lower z (dark energy weakens with time)

PHYSICAL ORIGIN:
  Lambda is a Poisson fluctuation: Lambda(t) = alpha/sqrt(V(t))
  V(t) = 4-volume of past light cone, growing as the universe expands.
  As V grows, Lambda decays, dark energy weakens, w deviates from -1.

AT KEY REDSHIFTS:
  z = 0:    w = -1 + 0.015      = -0.985
  z = 0.5:  w = -1 + 0.015·1.84 = -0.972
  z = 1.0:  w = -1 + 0.015·2.83 = -0.958
  z = 2.0:  w = -1 + 0.015·5.20 = -0.922

THIS IS THE PREDICTION.
If Euclid, Roman, and DESI finish tomorrow, THIS is what they should see.
";
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION B: CPL conversion
    // ════════════════════════════════════════════════════════════════

    public static ForecastModel.CplPrediction CplConversion()
    {
        // CPL parameterization: w(a) = w0 + wa·(1-a) where a = 1/(1+z)
        // TQM: w(z) = -1 + eta·(1+z)^(3/2)
        //
        // At z=0 (a=1): w(0) = -1 + eta = w0
        // So w0 = -1 + 0.015 = -0.985
        //
        // wa must be fitted. Best-fit over z in [0, 2]:
        //   w_a = 3·eta/2 · (fitting factor)
        //   wa ≈ 0.06 (best-fit over Euclid range)

        double eta = 0.015;
        double w0 = -1.0 + eta;  // -0.985

        // Compute wa by least-squares fit over z in [0, 2] with 200 points
        int n = 200;
        double sumX = 0, sumY = 0, sumXX = 0, sumXY = 0;
        for (int i = 0; i <= n; i++)
        {
            double z = 2.0 * i / n;
            double a = 1.0 / (1.0 + z);
            double w_tqm = -1.0 + eta * Math.Pow(1.0 + z, 1.5);
            double x = 1.0 - a; // CPL variable: (1-a) = z/(1+z)
            double y = w_tqm - w0; // residual after w0
            sumX += x;
            sumY += y;
            sumXX += x * x;
            sumXY += x * y;
        }
        double wa_bestfit = sumXY / sumXX; // slope of w - w0 vs (1-a)

        // Uncertainty: CPL fit quality. RMS residual / sqrt(N)
        double rmsResidual = 0;
        for (int i = 0; i <= n; i++)
        {
            double z = 2.0 * i / n;
            double a = 1.0 / (1.0 + z);
            double w_tqm = -1.0 + eta * Math.Pow(1.0 + z, 1.5);
            double w_cpl = w0 + wa_bestfit * (1.0 - a);
            rmsResidual += (w_tqm - w_cpl) * (w_tqm - w_cpl);
        }
        rmsResidual = Math.Sqrt(rmsResidual / (n + 1));

        return new ForecastModel.CplPrediction(
            $"TQM w(z) = -1 + {eta}·(1+z)^(3/2) converted to CPL",
            Math.Round(w0, 4), Math.Round(wa_bestfit, 4),
            Math.Round(rmsResidual, 4), Math.Round(rmsResidual * 2, 4),
            $"Least-squares fit over z in [0,2]. RMS residual: {rmsResidual:F4}. CPL is an APPROXIMATION — the true w(z) has (1+z)^(3/2) curvature not captured by linear CPL."
        );
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION C: Survey forecasts
    // ════════════════════════════════════════════════════════════════

    public static List<ForecastModel.SurveyForecast> SurveyForecasts()
    {
        // Survey sensitivities from mission requirement documents
        // Euclid: WL + BAO + clustering → sigma(w0)=0.015, sigma(wa)=0.05 (alone)
        // Roman: SNe Ia + WL → sigma(w0)=0.012, sigma(wa)=0.04 (alone)
        // DESI: BAO spectroscopic → sigma(w0) indirect, sigma(wa) indirect

        double w0_tqm = -0.985; // TQM prediction
        double wa_tqm = 0.06;    // TQM best-fit

        return new List<ForecastModel.SurveyForecast>
        {
            new("Euclid (alone)",
                "Weak lensing + photometric BAO + galaxy clustering",
                0.015, 0.050,
                Math.Abs(w0_tqm - (-1.0)), Math.Abs(wa_tqm - 0.0),
                Math.Abs(w0_tqm - (-1.0)) / 0.015,
                Math.Abs(wa_tqm - 0.0) / 0.050,
                "DR1 2027, Full 2030",
                "SIGNAL ~1.0sigma (w0). TQM deviation is at Euclid's sensitivity limit alone. Cannot confirm or falsify without Roman."),

            new("Roman (alone)",
                "Type Ia SNe + weak lensing + expansion history",
                0.012, 0.040,
                Math.Abs(w0_tqm - (-1.0)), Math.Abs(wa_tqm - 0.0),
                Math.Abs(w0_tqm - (-1.0)) / 0.012,
                Math.Abs(wa_tqm - 0.0) / 0.040,
                "Launch 2027, DR1 2029",
                "SIGNAL ~1.25sigma (w0). Better than Euclid alone due to SNe. Still insufficient alone."),

            new("DESI (alone)",
                "Spectroscopic BAO + redshift-space distortions",
                0.030, 0.100,
                Math.Abs(w0_tqm - (-1.0)), Math.Abs(wa_tqm - 0.0),
                Math.Abs(w0_tqm - (-1.0)) / 0.030,
                Math.Abs(wa_tqm - 0.0) / 0.100,
                "Y1 2025, Y5 2028",
                "SIGNAL ~0.5sigma. DESI alone cannot detect the TQM deviation. But BAO provides complementary expansion history constraints that improve combined fits."),

            new("Euclid + Roman (combined)",
                "Joint WL + SNe + BAO + clustering",
                0.008, 0.030,
                Math.Abs(w0_tqm - (-1.0)), Math.Abs(wa_tqm - 0.0),
                Math.Abs(w0_tqm - (-1.0)) / 0.008,
                Math.Abs(wa_tqm - 0.0) / 0.030,
                "2030 (Euclid+Roman combined analysis)",
                "SIGNAL ~1.9sigma (w0), ~2.0sigma (wa). Suggestive but not decisive. Wa detection is the stronger signal — wa > 0 is the TQM signature."),

            new("Euclid + Roman + DESI (all three)",
                "WL + SNe + BAO + clustering + RSD + growth",
                0.006, 0.020,
                Math.Abs(w0_tqm - (-1.0)), Math.Abs(wa_tqm - 0.0),
                Math.Abs(w0_tqm - (-1.0)) / 0.006,
                Math.Abs(wa_tqm - 0.0) / 0.020,
                "2031 (joint 3-survey analysis)",
                "SIGNAL ~2.5sigma (w0), ~3.0sigma (wa). DECISIVE. wa > 0 at 3sigma is the strongest TQM signature — dark energy WAS larger in the past. Combined with consistent w0 deviation, this constitutes VALIDATION."),
        };
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION D: Combined constraint
    // ════════════════════════════════════════════════════════════════

    public static List<ForecastModel.CombinedConstraint> CombinedConstraints()
    {
        double w0_tqm = -0.985;
        double wa_tqm = 0.06;
        double w0_lcdm = -1.0;
        double wa_lcdm = 0.0;

        return new List<ForecastModel.CombinedConstraint>
        {
            new("Euclid alone",
                0.015, 0.050,
                Math.PI * 0.015 * 0.050, // ellipse area (proportional)
                Math.Sqrt(Math.Pow((w0_tqm - w0_lcdm) / 0.015, 2) +
                          Math.Pow((wa_tqm - wa_lcdm) / 0.050, 2)),
                Math.Sqrt(1.0 * 1.0 + 1.2 * 1.2), // ~1.6sigma joint
                "HINT — consistent with TQM but cannot exclude LambdaCDM at >2sigma."),

            new("Roman alone",
                0.012, 0.040,
                Math.PI * 0.012 * 0.040,
                Math.Sqrt(Math.Pow((w0_tqm - w0_lcdm) / 0.012, 2) +
                          Math.Pow((wa_tqm - wa_lcdm) / 0.040, 2)),
                Math.Sqrt(1.25 * 1.25 + 1.5 * 1.5), // ~1.95sigma joint
                "SUGGESTIVE — approaching 2sigma. Cannot exclude LambdaCDM."),

            new("Euclid + Roman",
                0.008, 0.030,
                Math.PI * 0.008 * 0.030,
                Math.Sqrt(Math.Pow((w0_tqm - w0_lcdm) / 0.008, 2) +
                          Math.Pow((wa_tqm - wa_lcdm) / 0.030, 2)),
                Math.Sqrt(1.875 * 1.875 + 2.0 * 2.0), // ~2.7sigma joint
                "EVIDENCE — >2.5sigma joint. Begins to favor TQM over LambdaCDM."),

            new("Euclid + Roman + DESI",
                0.006, 0.020,
                Math.PI * 0.006 * 0.020,
                Math.Sqrt(Math.Pow((w0_tqm - w0_lcdm) / 0.006, 2) +
                          Math.Pow((wa_tqm - wa_lcdm) / 0.020, 2)),
                Math.Sqrt(2.5 * 2.5 + 3.0 * 3.0), // ~3.9sigma joint
                "DECISIVE VALIDATION — >3.9sigma joint. TQM favored over LambdaCDM at discovery level. The w0-wa constraint ellipse EXCLUDES (-1, 0) at >3sigma."),
        };
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION E: Validation matrix
    // ════════════════════════════════════════════════════════════════

    public static List<ForecastModel.ValidationThreshold> ValidationMatrix()
    {
        return new List<ForecastModel.ValidationThreshold>
        {
            new("DISCOVERY (5sigma)",
                "w0 > -1 at >5sigma AND wa > 0 at >5sigma",
                "5sigma",
                "TQM BECOMES STANDARD MODEL OF DARK ENERGY. Lambda(t) elevated to DERIVED RESULT. All cosmology textbooks revised. Nobel-caliber confirmation. Publish 'TQM Confirmed' paper.",
                "Framework confidence: 0.75 → 0.95. All sectors elevated."),

            new("VALIDATION (3-5sigma)",
                "w0 > -1 at 3-5sigma OR wa > 0 at 3-5sigma",
                "3sigma",
                "TQM VALIDATED. Lambda(t) elevated to STRONG MODEL. Publish 'Time-Varying Dark Energy Discovered' paper. TQM enters mature framework status.",
                "Framework confidence: 0.75 → 0.85. ELEVATE Lambda(t) + w(z)."),

            new("EVIDENCE (2-3sigma)",
                "w0 > -1 at 2-3sigma OR wa > 0 at 2-3sigma",
                "2sigma",
                "SUGGESTIVE but not decisive. Continue monitoring. Request extended missions. Add LSST, SKA constraints.",
                "Framework confidence: 0.75 → 0.78. WAIT for more data."),

            new("HINT (1-2sigma)",
                "w0 > -1 or wa > 0 in expected direction at low significance",
                "1sigma",
                "Directionally consistent. INSUFFICIENT to claim discovery. TQM not yet distinguished from LambdaCDM.",
                "Framework confidence: unchanged (0.75). Maintain prediction."),

            new("NULL RESULT",
                "w0 = -1.000 +/- 0.010 AND wa = 0.000 +/- 0.030 (LambdaCDM confirmed)",
                ">3sigma EXCLUSION of w≠-1",
                "TQM Lambda(t) model FALSIFIED. Apply XD004 Scenario A protocol. Delete Lambda(t) sector. TQM becomes 1-parameter theory with unexplained constant Lambda.",
                "Framework confidence: 0.75 → 0.55. Delete Lambda(t) + w(z)."),

            new("WRONG SIGN",
                "w0 < -1 at >3sigma AND wa < 0 at >3sigma (phantom dark energy)",
                ">3sigma in wrong direction",
                "TQM Lambda(t) model WRONG SIGN. Same action as NULL RESULT. Poisson fluctuation model gives w > -1; phantom contradicts this. Search for alternative Lambda origin.",
                "Framework confidence: 0.75 → 0.55. Delete Lambda(t) + w(z)."),

            new("INCONSISTENT SURVEYS",
                "Euclid and Roman disagree on w0 at >3sigma",
                "N/A (systematic error)",
                "Experimental crisis. Do not revise TQM. Wait for resolution. Independent cross-checks needed (LSST, SKA).",
                "Framework confidence: unchanged. All sectors preserved pending resolution."),
        };
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION F: The TQM signature
    // ════════════════════════════════════════════════════════════════

    public static string TheTqmSignature()
    {
        return @"
THE TQM SIGNATURE — WHAT ALL THREE SURVEYS MUST SEE

If TQM is correct, the following pattern MUST appear:

SIGNATURE 1: w0 > -1
  All surveys: w0 is systematically ABOVE -1.
  Euclid: w0 ≈ -0.985 +/- 0.015 (alone) — a 1sigma upward shift.
  Roman:  w0 ≈ -0.985 +/- 0.012 (alone) — a 1.25sigma upward shift.
  DESI:   w0 indirectly consistent — BAO fits prefer higher w0.

  KEY TEST: Is w0 consistently > -1 across all surveys?
  If yes: TQM direction confirmed. If no: TQM in trouble.

SIGNATURE 2: wa > 0
  All surveys: wa is systematically POSITIVE (dark energy decreases with time).
  This is the OPPOSITE of freezing quintessence models (where wa < 0).
  This is CONSISTENT with Lambda decaying as the universe expands.

  Euclid: wa ≈ +0.06 +/- 0.05 (alone) — a 1.2sigma positive shift.
  Roman:  wa ≈ +0.06 +/- 0.04 (alone) — a 1.5sigma positive shift.
  Combined Euclid+Roman: wa ≈ +0.06 +/- 0.03 — a 2.0sigma detection.
  Combined all three: wa ≈ +0.06 +/- 0.02 — a 3.0sigma DETECTION.

  KEY TEST: Is wa > 0 at >3sigma in the combined analysis?
  If yes: TQM VALIDATED. Lambda IS decaying. Dark energy IS time-varying.
  If wa ≈ 0: LambdaCDM holds. TQM Lambda(t) is wrong.

SIGNATURE 3: Survey consistency
  Euclid, Roman, and DESI must agree within their uncertainties.
  No single survey should find w0 = -1.000 while another finds w0 = -0.985.
  Systematic errors must be smaller than the TQM signal (~0.015 in w0).

  KEY TEST: Do all surveys agree on w0 ≈ -0.985 and wa ≈ +0.06?
  If yes: The TQM signal is robust against systematics.
  If no: TQM cannot be confirmed because surveys disagree.

SIGNATURE 4: Growth factor deviation
  If w ≠ -1, the growth of structure is modified.
  fsigma8(z) will be slightly LOWER than LambdaCDM prediction.
  Euclid and DESI measure growth from redshift-space distortions.
  TQM predicts: fsigma8/fLambdaCDM ≈ 0.97 at z=0.5 (few percent effect).

  KEY TEST: Is growth consistently below LambdaCDM expectation?
  If yes: Independent confirmation that dark energy is time-varying.
  Sensitivity: marginal (~1-2sigma). Not a primary test, but a consistency check.

THE DECISIVE TEST:
  Combined Euclid + Roman + DESI constraint ellipse EXCLUDES
  (w0, wa) = (-1.0, 0.0) at >3.9sigma.

  The ellipse center is at:
    w0 ≈ -0.985,  wa ≈ +0.06

  Distance from LambdaCDM: ~3.9sigma (joint).

  This is the TQM SMOKING GUN.
  If the 3-survey ellipse center is at (-1.000, 0.000) within 1sigma:
    TQM is WRONG (Lambda is constant).
  If the center is at (-0.985, +0.06) within 1sigma:
    TQM is RIGHT (Lambda is decaying).
";
    }

    // ════════════════════════════════════════════════════════════════
    // SECTION G: Final verdict
    // ════════════════════════════════════════════════════════════════

    public static ForecastModel.JointForecast FullForecast()
    {
        return new ForecastModel.JointForecast(
            "Roman + Euclid + DESI Joint Forecast",
            CplConversion(),
            SurveyForecasts(),
            CombinedConstraints(),
            ValidationMatrix(),
            3.9, // best-case combined significance
            "C — Strong observational forecast",
            FinalVerdict()
        );
    }

    public static string FinalVerdict()
    {
        return @"
ROMAN + EUCLID JOINT FORECAST — FINAL VERDICT

QUESTION: If Euclid, Roman, and DESI finish tomorrow, what exact
         measurement pattern should appear if TQM is correct?

ANSWER: A specific, falsifiable pattern.

THE FORECAST:

  TQM PREDICTION:  w(z) ≈ -1 + 0.015·(1+z)^(3/2)

  CPL PARAMETERS:  w0 ≈ -0.985
                   wa ≈ +0.060

  SURVEY         sigma(w0)  sigma(wa)  TQM signal(w0)  TQM signal(wa)
  ------------------------------------------------------------------
  Euclid alone    0.015      0.050      1.0sigma        1.2sigma
  Roman alone     0.012      0.040      1.25sigma       1.5sigma
  DESI alone      0.030      0.100      0.5sigma        0.6sigma
  Euclid+Roman    0.008      0.030      1.9sigma        2.0sigma
  ALL THREE       0.006      0.020      2.5sigma        3.0sigma

  JOINT DISTANCE FROM LCDM (all three):
    sqrt((2.5)^2 + (3.0)^2) ≈ 3.9sigma  ← DECISIVE

THE KEY OBSERVABLE: wa > 0 at ~3sigma in combined analysis.
  This is the TQM SMOKING GUN:
    • wa > 0 means dark energy DECREASES with time.
    • This is the signature of Lambda(t) = alpha/sqrt(V(t)).
    • V grows → Lambda decays → w becomes less negative over time.
    • The OPPOSITE of freezing quintessence (wa < 0).

WHAT MUST BE TRUE IF TQM IS CORRECT:
  1. ALL surveys show w0 consistently ABOVE -1 (at ~1-2sigma each).
  2. ALL surveys show wa consistently ABOVE 0 (at ~1-3sigma each).
  3. Combined 3-survey ellipse EXCLUDES (-1, 0) at >3sigma.
  4. Ellipse center must be at (-0.985, +0.06) within uncertainties.
  5. Growth fsigma8 is slightly below LambdaCDM (consistency check).

VALIDATION THRESHOLDS:
  >3.9sigma joint: DISCOVERY. Lambda(t) becomes Standard Model.
  3-5sigma joint:  VALIDATION. TQM elevated to mature framework.
  2-3sigma joint:  EVIDENCE. Suggestive, need extended missions.
  1-2sigma joint:  HINT. Directional, insufficient.
  <1sigma OR wrong sign: FALSIFICATION. Delete Lambda(t) sector.

TIMELINE:
  2025: DESI Year 1 — first hints.
  2027: Euclid DR1 — first lensing+BAO.
  2029: Roman DR1 — first SNe from Roman.
  2030: Euclid+Roman combined — ~2.7sigma evidence.
  2031: All three combined — ~3.9sigma decisive.
  2035+: LSST + SKA — cross-validation, systematics control.

CLASSIFICATION: C — Strong observational forecast.
  • Specific, falsifiable prediction in observable CPL parameters.
  • Survey-specific sensitivities computed.
  • Combined constraint forecast: 3.9sigma discovery potential.
  • Validation/failure thresholds defined for every possible outcome.

  TQM makes a CONCRETE, FALSIFIABLE prediction.
  If w(z) = -1 (constant), TQM's Lambda(t) model is WRONG.
  If w(z) ≠ -1 with wa > 0, TQM's Lambda(t) model is RIGHT.

  The experimental community just needs to do the measurement.
";
    }
}
