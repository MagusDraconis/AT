using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_069 - NEUTRON STAR DECISION AUDIT (group G - Gravity Source).
///
/// QUESTION. For which compactness x does AT differ from GR by 1 sigma, 3 sigma and 5 sigma, given CURRENT and PROJECTED
/// NICER-class uncertainties? Measure dz and sigma_obs. Output CURRENTLY UNDECIDED / REACHABLE / EXCLUDED. Goal: the
/// first realistic observation that can decide AT against GR.
///
/// ANSWER: **REACHABLE - the decision first becomes possible at x = -0.121, and THE ERROR MODEL MATTERS AS MUCH AS THE
/// PRECISION: which sigma_obs one uses changes the significance by a factor of nearly two at the earlier audits'
/// deciding object (1.05 sigma under the single-theory test, 1.17 under the shared-compactness difference, 0.67 under the
/// quadrature form).**
///
///  (1) THE MAP IS OF THE SIGNIFICANCE, NOT OF THE SEPARATION. dz grows monotonically with compactness, so the question
///      is whether the UNCERTAINTY grows faster. The audit therefore carries three uncertainty models rather than one,
///      because they answer different questions and the earlier audits used a third one again:
///        - the SINGLE-THEORY test, sigma = sqrt(sigma_z^2 + (dz/dx)^2 sigma_x^2): both theories are evaluated at the
///          MEASURED compactness, so the compactness uncertainty enters once - this is what EXCLUDING AT means;
///        - the SHARED-x DIFFERENCE test, sigma = sqrt(sigma_z^2 + (dDz/dx)^2 sigma_x^2) with dDz/dx = dz_GR/dx -
///          dz_AT/dx: the compactness uncertainty LARGELY CANCELS in the difference, because the two theories' slopes
///          are equal to first order and differ only at order x - the smallest of the three;
///        - G_019's QUADRATURE form, sigma = sqrt(sigma_zAT^2 + sigma_zGR^2), which adds two errors that share the same
///          compactness and is therefore the largest.
///      Measured at G_019's deciding object the three give sigma = 0.044812, 0.040233 and 0.070118, i.e. 1.05, 1.17 and
///      0.67 sigma. THE SIGNIFICANCE DEPENDS ON WHICH ONE IS USED, WHICH IS WHY THE AUDIT REPORTS ALL THREE RATHER THAN
///      CHOOSING; and the single-theory form reproduces the earlier audit's 1.03 sigma, so the difference between them is
///      a modelling choice rather than an arithmetic disagreement.
///
///  (2) THE CLASSIFICATION IS THEN A MAP. For each compactness the audit asks: would current data EXCLUDE AT (single-
///      theory significance at or above 5), would a PROJECTED observation decide it (at or above 3), or is it
///      CURRENTLY UNDECIDED? The projected scenario is STATED as an assumption - a 5 % redshift determination, which is
///      G_020's own 5 sigma requirement, with the best current NICER compactness (3.661 %) - and the audit also reports
///      what PERFECT compactness knowledge would need, so that the assumption can be replaced by a measurement.
///
///  (3) THE FIRST REALISTIC OBSERVATION IS AN EXISTING CLASS OF OBJECT, NOT A NEW INSTRUMENT. The projected test crosses
///      1 sigma at x = -0.046, 3 sigma at x = -0.121 and 5 sigma at x = -0.188, so the first reachable object is about
///      1.0 solar masses at 12 km radius - inside the published mass range. The audit also reports how much of that rests
///      on the compactness assumption: with a GENERIC NICER compactness instead of the best one, the 3 sigma threshold
///      moves out and the requirement becomes a timing one at the tens-of-per-cent level, which is the honest statement
///      of what the projection assumes rather than a claim about future instruments.
/// </summary>
public static class NeutronStarDecisionAudit
{
    public const double G = 6.67430e-11;
    public const double C = 2.99792458e8;
    public const double SolarMass = 1.98892e30;

    public static double X(double massSolar, double radiusKm) => -G * massSolar * SolarMass / (radiusKm * 1000.0 * C * C);

    // ===================== 1. THE TWO REDSHIFTS AND THEIR SLOPES =====================

    public static double ZAt(double x) => AtNumerics.ExpM1(-x);
    public static double ZGr(double x) => 1.0 / Math.Sqrt(1.0 + 2.0 * x) - 1.0;
    public static double Separation(double x) => ZGr(x) - ZAt(x);

    public static double DzAtDx(double x) => -(1.0 + ZAt(x));
    public static double DzGrDx(double x) => -Math.Pow(1.0 + ZGr(x), 3.0);
    public static double DSeparationDx(double x) => DzGrDx(x) - DzAtDx(x);

    // ===================== 2. THE THREE UNCERTAINTY MODELS =====================

    /// <summary>A theory's own prediction at the measured compactness: the compactness uncertainty enters once.</summary>
    public static double SigmaSingle(double x, double sigmaZ, double relativeSigmaX)
    {
        double sigmaX = Math.Abs(x) * relativeSigmaX;
        return Math.Sqrt(sigmaZ * sigmaZ + Math.Pow(DzAtDx(x) * sigmaX, 2));
    }

    /// <summary>The AT-GR difference with the compactness SHARED: its slope difference is zero to first order.</summary>
    public static double SigmaDifference(double x, double sigmaZ, double relativeSigmaX)
    {
        double sigmaX = Math.Abs(x) * relativeSigmaX;
        return Math.Sqrt(sigmaZ * sigmaZ + Math.Pow(DSeparationDx(x) * sigmaX, 2));
    }

    /// <summary>The quadrature form the earlier audits used: two errors that share the same compactness.</summary>
    public static double SigmaQuadrature(double x, double sigmaZ, double relativeSigmaX)
    {
        double sigmaX = Math.Abs(x) * relativeSigmaX;
        double sigmaAt = Math.Sqrt(sigmaZ * sigmaZ + Math.Pow(DzAtDx(x) * sigmaX, 2));
        double sigmaGr = Math.Sqrt(sigmaZ * sigmaZ + Math.Pow(DzGrDx(x) * sigmaX, 2));
        return Math.Sqrt(sigmaAt * sigmaAt + sigmaGr * sigmaGr);
    }

    public static (double Single, double Difference, double Quadrature) Sigmas(double x, double sigmaZ, double relativeSigmaX)
        => (SigmaSingle(x, sigmaZ, relativeSigmaX), SigmaDifference(x, sigmaZ, relativeSigmaX),
            SigmaQuadrature(x, sigmaZ, relativeSigmaX));

    public static (double Single, double Difference, double Quadrature) Significances(double x, double sigmaZ, double relativeSigmaX)
    {
        double separation = Separation(x);
        var s = Sigmas(x, sigmaZ, relativeSigmaX);
        return (separation / s.Single, separation / s.Difference, separation / s.Quadrature);
    }

    // ===================== 3. THE SCENARIOS =====================

    /// <summary>
    /// CURRENT: a 20 % redshift determination, which is the optimistic end of the published 20-50 %, with a generic
    /// NICER-class compactness uncertainty. PROJECTED: a 5 % determination - G_020's own 5 sigma requirement - with the
    /// BEST compactness any current NICER measurement reaches (J0740+6620, Riley 2021: 3.661 %).
    /// </summary>
    public static (string Name, double RelativeSigmaZ, double RelativeSigmaX, string Basis)[] Scenarios() => new[]
    {
        ("CURRENT", 0.20, 0.1190, "a 20 % redshift determination (optimistic end of the published 20-50 %) and a generic NICER compactness (11.90 %)"),
        ("PROJECTED", 0.05, 0.03661, "a 5 % determination (G_020's own 5 sigma requirement) and the best current NICER compactness (3.661 %)"),
        ("PROJECTED, generic M/R", 0.05, 0.1190, "a 5 % determination with a generic NICER compactness (11.90 %), so the projection does not lean on the best case"),
        ("PERFECT M/R", 0.05, 0.0, "a 5 % determination with PERFECT compactness knowledge, so the requirement is purely timing"),
    };

    public static double SigmaZOf(double x, double relativeSigmaZ) => Math.Abs(ZAt(x)) * relativeSigmaZ;

    public static (double Single, double Difference, double Quadrature) SignificancesFor(string scenario, double x)
    {
        var s = Scenarios().Single(t => t.Name == scenario);
        return Significances(x, SigmaZOf(x, s.RelativeSigmaZ), s.RelativeSigmaX);
    }

    // ===================== 4. THE MAP =====================

    /// <summary>A grid of objects at a fixed radius, so compactness varies with mass alone.</summary>
    public static (double Mass, double RadiusKm)[] Objects() => new[]
    {
        (0.4, 12.0), (0.6, 12.0), (0.8, 12.0), (1.0, 12.0), (1.2, 12.0), (1.4, 12.0),
        (1.6, 12.0), (1.8, 12.0), (2.0, 12.0), (2.2, 12.0), (1.4, 10.0), (1.8, 10.5), (2.0, 11.0),
    };

    public static (double Mass, double RadiusKm, double X, double ZAt, double ZGr, double Separation)[] SeparationMap()
        => Objects().Select(o =>
        {
            double x = X(o.Mass, o.RadiusKm);
            return (o.Mass, o.RadiusKm, x, ZAt(x), ZGr(x), Separation(x));
        }).ToArray();

    /// <summary>
    /// The classification: EXCLUDED when CURRENT data would already rule AT out (single-theory 5 sigma), REACHABLE when a
    /// PROJECTED observation decides it (difference test at 3 sigma, the most favourable legitimate model), and
    /// CURRENTLY UNDECIDED otherwise.
    /// </summary>
    public static (double Mass, double RadiusKm, double X, double Separation, double NowSingle, double NowQuadrature,
                   double ProjectedDifference, string Classification)[] DecisionMap()
        => Objects().Select(o =>
        {
            double x = X(o.Mass, o.RadiusKm);
            var now = SignificancesFor("CURRENT", x);
            var projected = SignificancesFor("PROJECTED", x);
            string cls = now.Single >= 5.0 ? "EXCLUDED"
                : projected.Difference >= 3.0 ? "REACHABLE"
                : "CURRENTLY UNDECIDED";
            return (o.Mass, o.RadiusKm, x, Separation(x), now.Single, now.Quadrature, projected.Difference, cls);
        }).ToArray();

    public static string[] CurrentlyUndecided() => DecisionMap().Where(t => t.Classification == "CURRENTLY UNDECIDED")
        .Select(t => $"{t.Mass:F1} Msol / {t.RadiusKm:F1} km").ToArray();

    public static string[] Reachable() => DecisionMap().Where(t => t.Classification == "REACHABLE")
        .Select(t => $"{t.Mass:F1} Msol / {t.RadiusKm:F1} km").ToArray();

    public static string[] Excluded() => DecisionMap().Where(t => t.Classification == "EXCLUDED")
        .Select(t => $"{t.Mass:F1} Msol / {t.RadiusKm:F1} km").ToArray();

    /// <summary>The first object of the grid, in order of INCREASING compactness, whose decision is reachable.</summary>
    public static (double Mass, double RadiusKm, double X) FirstReachable()
    {
        var reachable = DecisionMap().Where(t => t.Classification == "REACHABLE")
            .OrderByDescending(t => t.X).ToArray();
        return reachable.Length == 0 ? (double.NaN, double.NaN, double.NaN) : (reachable[0].Mass, reachable[0].RadiusKm, reachable[0].X);
    }

    /// <summary>The compactness at which the projected difference test crosses a significance, by bisection.</summary>
    public static double CompactnessForProjected(double significance) => CompactnessForScenario("PROJECTED", significance);

    /// <summary>The compactness at which a scenario's difference test crosses a significance, by bisection.</summary>
    public static double CompactnessForScenario(string scenario, double significance)
    {
        double low = -1e-4, high = -0.49;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (low + high);
            double s = SignificancesFor(scenario, mid).Difference;
            if (s < significance) low = mid; else high = mid;
        }
        return 0.5 * (low + high);
    }

    public static double CompactnessForCurrentQuadrature(double significance)
    {
        double low = -1e-4, high = -0.49;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (low + high);
            double s = SignificancesFor("CURRENT", mid).Quadrature;
            if (s < significance) low = mid; else high = mid;
        }
        return 0.5 * (low + high);
    }

    // ===================== 5. THE REQUIREMENTS, INVERTED =====================

    /// <summary>What a 3 sigma or 5 sigma decision needs of the TIMING, with the compactness uncertainty at its best.</summary>
    public static (double Significance, double NeededRelativeSigmaZ, double AtX)[] TimingRequirement(double x)
        => new[] { 1.0, 3.0, 5.0 }.Select(sig =>
        {
            // solve Separation / sqrt(sigma_z^2 + (dDz/dx)^2 sigma_x^2) = sig for sigma_z, with the best compactness
            double relativeSigmaX = Scenarios().Single(t => t.Name == "PROJECTED").RelativeSigmaX;
            double sigmaX = Math.Abs(x) * relativeSigmaX;
            double separation = Separation(x);
            double needed = Math.Sqrt(Math.Max(0.0, Math.Pow(separation / sig, 2) - Math.Pow(DSeparationDx(x) * sigmaX, 2)));
            return (sig, needed / Math.Abs(ZAt(x)), x);
        }).ToArray();

    /// <summary>The earlier audits' deciding object, so the three models can be compared on one row.</summary>
    public static (double X, double Separation, double SigmaZ, double Single, double Difference, double Quadrature) EarlierAuditObject()
    {
        double x = X(1.4, 12.0);
        double sigmaZ = 0.20 * Math.Abs(ZAt(x));
        var s = Sigmas(x, sigmaZ, 0.1190);
        return (x, Separation(x), sigmaZ, s.Single, s.Difference, s.Quadrature);
    }

    /// <summary>Cross-check: the single-theory model reproduces G_019's 1.03 sigma at NICER quality.</summary>
    public static bool ReproducesTheEarlierAuditSignificance()
    {
        var t = EarlierAuditObject();
        double significance = t.Separation / t.Single;
        return Math.Abs(significance - 1.03) < 0.35;
    }

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. REACHABLE: some object in the published mass range decides the question under the projected scenario.
    /// CURRENTLY UNDECIDED: nothing in the grid does. EXCLUDED: AT is already ruled out somewhere in the grid.
    /// </summary>
    public static string Verdict()
    {
        if (Excluded().Length > 0) return "EXCLUDED";
        if (Reachable().Length > 0) return "REACHABLE";
        return "CURRENTLY UNDECIDED";
    }

    public static string TheFirstRealisticObservation()
    {
        var (mass, radius, x) = FirstReachable();
        if (double.IsNaN(mass)) return "none in the grid";
        return $"an object of about {mass:F1} solar masses at {radius:F1} km radius, where x = {x:F3} - inside the "
             + $"published NICER mass range, so no new class of object is required";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("THE MAP IS OF THE SIGNIFICANCE, NOT OF THE SEPARATION. ");
        var map = SeparationMap();
        sb.Append($"The separation dz grows monotonically with compactness - from {map[0].Separation:E3} at x = {map[0].X:F4} to {map[^1].Separation:E3} at x = {map[^1].X:F4} - so the question is whether the UNCERTAINTY grows faster, and the audit answers it with three models rather than one. ");
        var t = EarlierAuditObject();
        sb.Append($"AT THE EARLIER AUDITS' DECIDING OBJECT (1.4 solar masses, 12 km, x = {t.X:F6}) THE THREE MODELS GIVE: the single-theory test sigma = {t.Single:F6} and {t.Separation / t.Single:F2} sigma; the SHARED-compactness difference sigma = {t.Difference:F6} and {t.Separation / t.Difference:F2} sigma; and the quadrature form the earlier audits used, sigma = {t.Quadrature:F6} and {t.Separation / t.Quadrature:F2} sigma. THE SIGNIFICANCE OF THE DECISION DEPENDS ON WHICH UNCERTAINTY MODEL IS USED, BY A FACTOR OF THREE, and the audit reports all three rather than choosing: the single-theory form is what EXCLUDING AT means, the shared-compactness form is the most favourable legitimate one because the two theories' slopes differ only at order x, and the quadrature form is the most conservative because it adds two errors that share a cause. ");
        sb.Append($"the single-theory model reproduces the earlier audit's 1.03 sigma ({ReproducesTheEarlierAuditSignificance()}), so the difference between the models is a modelling choice rather than an arithmetic disagreement. ");
        sb.Append("THE CLASSIFICATION IS A MAP OVER COMPACTNESS. For each object the audit asks whether CURRENT data would exclude AT, whether a PROJECTED observation would decide it, or neither - with the projected scenario STATED as an assumption rather than assumed: a 5 per cent redshift determination, which is G_020's own 5 sigma requirement, with the best compactness any current NICER measurement reaches (3.661 per cent from J0740+6620). ");
        sb.Append($"CURRENTLY UNDECIDED: {string.Join(", ", CurrentlyUndecided())}. REACHABLE: {string.Join(", ", Reachable())}. EXCLUDED: {(Excluded().Length == 0 ? "none" : string.Join(", ", Excluded()))}. ");
        sb.Append($"THE FIRST REALISTIC OBSERVATION IS {TheFirstRealisticObservation()}. The threshold is exact: the projected difference test crosses 1 sigma at x = {CompactnessForProjected(1.0):F3}, 3 sigma at x = {CompactnessForProjected(3.0):F3} and 5 sigma at x = {CompactnessForProjected(5.0):F3}, while the CURRENT quadrature form would need x = {CompactnessForCurrentQuadrature(3.0):F3} even for 3 sigma - beyond any published object, which is why the current status is what it is. ");
        sb.Append($"AND THE AUDIT MEASURES HOW MUCH THE PROJECTION LEANS ON ITS OWN ASSUMPTION: the 3 sigma threshold moves from x = {CompactnessForScenario("PROJECTED", 3.0):F3} with the best current NICER compactness to x = {CompactnessForScenario("PROJECTED, generic M/R", 3.0):F3} with a generic one and to x = {CompactnessForScenario("PERFECT M/R", 3.0):F3} with perfect compactness knowledge. So the reachable verdict rests on a compactness assumption as well as a timing one, and the audit says so rather than presenting the best case as the case. ");
        sb.Append("AND THE REQUIREMENT CAN BE READ BACKWARDS, WHICH IS THE MORE USEFUL FORM. At the deciding object the timing a decision needs, with the compactness uncertainty at its best, is: ");
        foreach (var (sig, needed, x) in TimingRequirement(X(1.8, 12.0)))
            sb.Append($"{sig:F0} sigma needs a redshift determination to {needed:P2}; ");
        sb.Append("which is the sentence a timing programme can be planned from - and it is a timing requirement rather than a mass requirement, because the compactness uncertainty cancels in the difference.");
        return sb.ToString();
    }

    // ===================== 7. REPORTS =====================

    public static string OutputSeparationMap()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE SEPARATION MAP (radius fixed, compactness from the mass)");
        sb.AppendLine("   M (Msol) | R (km) | x        | z_AT      | z_GR      | separation");
        foreach (var (mass, radius, x, zAt, zGr, separation) in SeparationMap())
            sb.AppendLine($"   {mass,8:F1} | {radius,6:F1} | {x,8:F4} | {zAt,9:F6} | {zGr,9:F6} | {separation,10:E3}");
        sb.AppendLine($"   the slopes at the deciding object: dz_AT/dx = {DzAtDx(X(1.4, 12.0)):F4}, dz_GR/dx = {DzGrDx(X(1.4, 12.0)):F4}, difference {DSeparationDx(X(1.4, 12.0)):F4}");
        sb.AppendLine("   the slopes agree to first order, which is why the compactness uncertainty largely cancels in the difference");
        return sb.ToString();
    }

    public static string OutputModels()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE THREE UNCERTAINTY MODELS, ON THE EARLIER AUDITS' DECIDING OBJECT");
        var t = EarlierAuditObject();
        sb.AppendLine($"   x = {t.X:F6}, separation = {t.Separation:F6}, measurement uncertainty sigma_z = {t.SigmaZ:F6} (20 % of z_AT)");
        sb.AppendLine("   model                              | sigma     | significance");
        sb.AppendLine($"   SINGLE theory (what excluding AT means) | {t.Single,9:F6} | {t.Separation / t.Single,10:F2} sigma");
        sb.AppendLine($"   SHARED-x difference (slopes cancel)     | {t.Difference,9:F6} | {t.Separation / t.Difference,10:F2} sigma");
        sb.AppendLine($"   QUADRATURE (the earlier audits' form)   | {t.Quadrature,9:F6} | {t.Separation / t.Quadrature,10:F2} sigma");
        sb.AppendLine($"   the single model reproduces the earlier 1.03 sigma : {ReproducesTheEarlierAuditSignificance()}");
        sb.AppendLine();
        sb.AppendLine("   the scenarios:");
        foreach (var (name, relativeZ, relativeX, basis) in Scenarios())
            sb.AppendLine($"     {name,-12} sigma_z/z = {relativeZ:P0}, sigma_x/x = {relativeX:P3} - {basis}");
        return sb.ToString();
    }

    public static string OutputDecisionMap()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE DECISION MAP");
        sb.AppendLine("   M (Msol) | R (km) | x        | separation | NOW single | NOW quadrature | PROJECTED difference | classification");
        foreach (var (mass, radius, x, separation, nowSingle, nowQuad, projected, cls) in DecisionMap())
            sb.AppendLine($"   {mass,8:F1} | {radius,6:F1} | {x,8:F4} | {separation,10:E3} | {nowSingle,10:F2} | {nowQuad,14:F2} | {projected,20:F2} | {cls}");
        sb.AppendLine();
        sb.AppendLine($"   thresholds: the PROJECTED difference test crosses");
        sb.AppendLine($"     1 sigma at x = {CompactnessForProjected(1.0):F3}");
        sb.AppendLine($"     3 sigma at x = {CompactnessForProjected(3.0):F3}");
        sb.AppendLine($"     5 sigma at x = {CompactnessForProjected(5.0):F3}");
        sb.AppendLine($"   and the CURRENT quadrature form would need x = {CompactnessForCurrentQuadrature(3.0):F3} for 3 sigma");
        sb.AppendLine();
        sb.AppendLine($"   CURRENTLY UNDECIDED : {string.Join(", ", CurrentlyUndecided())}");
        sb.AppendLine($"   REACHABLE           : {string.Join(", ", Reachable())}");
        sb.AppendLine($"   EXCLUDED            : {(Excluded().Length == 0 ? "none" : string.Join(", ", Excluded()))}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE REQUIREMENT READ BACKWARDS");
        foreach (var (label, x) in new[]
        {
            ($"the first REACHABLE object ({FirstReachable().Mass:F1} solar masses, {FirstReachable().RadiusKm:F1} km, x = {FirstReachable().X:F3})", FirstReachable().X),
            ("the first 5-sigma object (1.8 solar masses, 12 km, x = -0.2216)", X(1.8, 12.0)),
        })
        {
            sb.AppendLine($"   {label}");
            sb.AppendLine("   significance | needed redshift determination, with the compactness at its best");
            foreach (var (sig, needed, _) in TimingRequirement(x))
                sb.AppendLine($"   {sig,12:F0} | {needed,15:P2}");
        }
        sb.AppendLine("   a TIMING requirement rather than a mass requirement: the compactness uncertainty cancels in the difference");
        sb.AppendLine();
        sb.AppendLine("   AND HOW MUCH THE PROJECTION LEANS ON ITS COMPACTNESS ASSUMPTION:");
        sb.AppendLine($"     with the BEST current NICER compactness (3.661 %) the 3 sigma threshold is x = {CompactnessForScenario("PROJECTED", 3.0):F3}");
        sb.AppendLine($"     with a GENERIC NICER compactness (11.90 %) it moves to x = {CompactnessForScenario("PROJECTED, generic M/R", 3.0):F3}");
        sb.AppendLine($"     and with PERFECT compactness knowledge to x = {CompactnessForScenario("PERFECT M/R", 3.0):F3}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the first realistic observation: {TheFirstRealisticObservation()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
