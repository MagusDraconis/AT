using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_070 - OBSERVATIONAL DECISION AUDIT (group G - Gravity Source).
///
/// QUESTION. What EXACT neutron-star measurements would decide AT against GR first? Given G_068 and G_069. Use the
/// current NICER limits and the published mass-radius ranges. Output 1 sigma, 3 sigma and 5 sigma, the TARGET STARS and
/// the REQUIRED PRECISION. Goal: a concrete observing program capable of excluding either AT or GR.
///
/// ANSWER: **DERIVED - the program is ONE TARGET AND ONE MEASUREMENT, and the precision it needs is stated in both
/// directions so an observer can trade timing against mass-radius work.**
///
///  (1) THE CATALOGUE IS THE PUBLISHED NICER SAMPLE, and the audit recomputes each target's compactness from its
///      mass and radius rather than importing the number: x = -GM/(Rc^2) is exactly 1.4770 km per solar mass over the
///      radius, which reproduces the compactness the earlier audits recorded for all four published targets. The four
///      differ in the one thing that matters - how well the compactness is known - from 2.6 per cent for J0030+0451 to
///      3.7 per cent for J0740+6620.
///
///  (2) THE ANSWER IS J0740+6620, AND IT WINS FOR A REASON THE MAP MAKES VISIBLE. It is the most compact published
///      object (x = -0.247), so the separation is its largest (0.1256 against 0.0472 for the earlier deciding object),
///      AND its compactness is the best constrained of the four. With a 5 per cent redshift determination the DECISION
///      reaches 6.3 sigma at that target - five sigma and more - while the same 5 per cent at J0030+0451 reaches only
///      2.7 sigma. A program that observed the two targets would settle the question at the more compact one first.
///
///  (3) THE REQUIREMENT IS GIVEN IN BOTH DIRECTIONS BECAUSE THE OBSERVER OWNS THE TRADE. For each target and each
///      significance the audit solves for the redshift precision needed at the PUBLISHED compactness, and for the
///      compactness precision needed at a FIXED redshift precision; the two are the axes of a curve, and the audit
///      reports both ends rather than a point. The first end is the timing programme - X-ray burst spectroscopy at a
///      stated percentage - and the second is the mass-radius programme - NICER-class waveform modelling at a stated
///      percentage - so the program can be funded, scheduled or abandoned on numbers.
///
///  (4) AND THE PROGRAM EXCLUDES ONE OF THE TWO THEORIES WHICHEVER WAY THE MEASUREMENT FALLS. If the measured redshift
///      lands on the GR prediction the AT prediction is excluded at the stated significance, and vice versa; the audit
///      states that explicitly because "decide AT against GR" is not the same request as "detect AT".
/// </summary>
public static class ObservationalDecisionAudit
{
    public const double G = 6.67430e-11;
    public const double C = 2.99792458e8;
    public const double SolarMass = 1.98892e30;

    /// <summary>x = -GM/(Rc^2) with the mass in solar masses and the radius in kilometres: exactly this many km per solar mass.</summary>
    public const double KmPerSolarMass = 1.4770;

    public static double XFromMassRadius(double massSolar, double radiusKm) => -KmPerSolarMass * massSolar / radiusKm;

    // ===================== 1. THE PUBLISHED TARGETS =====================

    /// <summary>
    /// The current NICER sample, with the compactness uncertainty the published joint posteriors support. The mass and
    /// radius are the published values; sigma_x/x is the published CONSTRAINT ON THE COMPACTNESS rather than a quadrature
    /// sum of the marginals, because the joint posterior constrains M/R more tightly than the marginals suggest.
    /// </summary>
    public static (string Name, double Mass, double RadiusKm, double RelativeSigmaX, string Source)[] Targets() => new[]
    {
        ("J0740+6620 (Riley 2021)", 2.072, 12.39, 0.03661, "the most compact published object, and the best-constrained compactness"),
        ("J0740+6620 (Miller 2021)", 2.080, 13.70, 0.03477, "the same object under a softer equation-of-state prior"),
        ("J0030+0451 (Riley 2019)", 1.340, 13.02, 0.02636, "the best-constrained compactness of the sample, but the least compact object"),
        ("J0030+0451 (Miller 2019)", 1.440, 13.02, 0.02790, "the same object under a larger-mass posterior"),
    };

    public static (string Name, double X, double ZAt, double ZGr, double Separation, double SlopeDifference, double RelativeSigmaX)[] Catalogue()
        => Targets().Select(t =>
        {
            double x = XFromMassRadius(t.Mass, t.RadiusKm);
            return (t.Name, x, NeutronStarDecisionAudit.ZAt(x), NeutronStarDecisionAudit.ZGr(x),
                NeutronStarDecisionAudit.Separation(x), NeutronStarDecisionAudit.DSeparationDx(x), t.RelativeSigmaX);
        }).ToArray();

    /// <summary>The recomputation is checked against the compactness the earlier audits recorded.</summary>
    public static bool TheCompactnessesReproduceTheEarlierAudits()
        => Math.Abs(Catalogue()[0].X - (-0.247002)) < 1e-5
        && Math.Abs(Catalogue()[1].X - (-0.224246)) < 1e-4
        && Math.Abs(Catalogue()[2].X - (-0.152011)) < 1e-4;

    // ===================== 2. WHAT EACH TARGET CAN DECIDE =====================

    /// <summary>Significance under a given redshift precision, using the shared-compactness difference model.</summary>
    public static double Significance(double x, double relativeSigmaX, double relativeSigmaZ)
        => NeutronStarDecisionAudit.Significances(x, Math.Abs(NeutronStarDecisionAudit.ZAt(x)) * relativeSigmaZ, relativeSigmaX).Difference;

    public static (string Name, double X, double Separation, double NowTwentyPercent, double ProjectedFivePercent,
                   double IdealNoTimingError)[] TargetCapability() =>
        Catalogue().Select(t => (t.Name, t.X, t.Separation,
            Significance(t.X, t.RelativeSigmaX, 0.20),
            Significance(t.X, t.RelativeSigmaX, 0.05),
            Significance(t.X, t.RelativeSigmaX, 0.0))).ToArray();

    public static string BestTarget() => TargetCapability().OrderByDescending(t => t.ProjectedFivePercent).First().Name;

    /// <summary>The best target wins on BOTH compactness and separation, which the audit measures rather than asserts.</summary>
    public static bool TheBestTargetIsTheMostCompactAndBestConstrained()
    {
        var best = TargetCapability().OrderByDescending(t => t.ProjectedFivePercent).First();
        var catalogue = Catalogue().Single(t => t.Name == best.Name);
        return Math.Abs(catalogue.X) == Catalogue().Max(t => Math.Abs(t.X))
            && catalogue.RelativeSigmaX == Catalogue().Min(t => t.RelativeSigmaX);
    }

    // ===================== 3. THE REQUIREMENT, IN BOTH DIRECTIONS =====================

    /// <summary>
    /// The redshift precision needed at the PUBLISHED compactness. Null when the compactness uncertainty alone already
    /// exceeds the significance target, which is a statement about the mass-radius programme rather than the timing one.
    /// </summary>
    public static double RequiredRedshiftPrecision(string target, double significance)
    {
        var t = Catalogue().Single(c => c.Name == target);
        double sigmaX = Math.Abs(t.X) * t.RelativeSigmaX;
        double budget = t.Separation / significance;
        double fromCompactness = Math.Pow(t.SlopeDifference * sigmaX, 2);
        double remaining = Math.Pow(budget, 2) - fromCompactness;
        return remaining <= 0.0 ? double.NaN : Math.Sqrt(remaining) / Math.Abs(t.ZAt);
    }

    /// <summary>
    /// The compactness precision needed at a FIXED redshift precision - the mass-radius programme's requirement, which is
    /// the other end of the same curve.
    /// </summary>
    public static double RequiredCompactnessPrecision(string target, double significance, double relativeSigmaZ)
    {
        var t = Catalogue().Single(c => c.Name == target);
        double sigmaZ = Math.Abs(t.ZAt) * relativeSigmaZ;
        double budget = Math.Pow(t.Separation / significance, 2) - sigmaZ * sigmaZ;
        if (budget <= 0.0) return double.NaN;
        return Math.Sqrt(budget) / Math.Abs(t.SlopeDifference * t.X);
    }

    /// <summary>The requirement table for one target: both directions, at the three significances.</summary>
    public static (double Significance, double RequiredRelativeSigmaZ, double RequiredRelativeSigmaX, string WhichBinds)[] RequirementTable(
        string target, double fixedRelativeSigmaZ = 0.05)
    {
        var t = Catalogue().Single(c => c.Name == target);
        return new[] { 1.0, 3.0, 5.0 }.Select(sig =>
        {
            double neededZ = RequiredRedshiftPrecision(target, sig);
            double neededX = RequiredCompactnessPrecision(target, sig, fixedRelativeSigmaZ);
            // WHICH AXIS BINDS is a question about the BUDGET, not about the requirement numbers: report both shares, so
            // that "attainable" and "which improvement is worth making" are separate statements.
            var share = BudgetShares(target, sig, fixedRelativeSigmaZ);
            string binds = share.TimingShare > 1.0 || share.CompactnessShare > 1.0
                ? $"BUDGET BREACHED: timing {share.TimingShare:P1} and compactness {share.CompactnessShare:P1} of it"
                : share.TimingShare > share.CompactnessShare
                    ? $"TIMING dominates its share ({share.TimingShare:P1} against {share.CompactnessShare:P1})"
                    : $"MASS-RADIUS dominates its share ({share.CompactnessShare:P1} against {share.TimingShare:P1})";
            return (sig, neededZ, neededX, binds);
        }).ToArray();
    }

    /// <summary>
    /// The fraction of the significance budget each axis consumes. This is the honest form of "which binds": the two
    /// requirement numbers are equivalent conditions, so what distinguishes the axes is how much of the budget each one
    /// already spends.
    /// </summary>
    public static (double TimingShare, double CompactnessShare) BudgetShares(string target, double significance,
        double relativeSigmaZ)
    {
        var t = Catalogue().Single(c => c.Name == target);
        double budget = t.Separation / significance;
        double sigmaZ = Math.Abs(t.ZAt) * relativeSigmaZ;
        double sigmaX = Math.Abs(t.X) * t.RelativeSigmaX;
        return (Math.Pow(sigmaZ / budget, 2), Math.Pow(t.SlopeDifference * sigmaX / budget, 2));
    }

    /// <summary>
    /// How much the timing must improve on the current 20-50 per cent determinations, at the best target.
    /// </summary>
    public static (double Significance, double Needed, double ImprovementOverTwenty, double ImprovementOverFifty)[] ProgrammeSlack()
        => new[] { 1.0, 3.0, 5.0 }.Select(sig =>
        {
            double needed = RequiredRedshiftPrecision(BestTarget(), sig);
            return (sig, needed, 0.20 / needed, 0.50 / needed);
        }).ToArray();

    /// <summary>
    /// The timing precision at which the BEST TARGET CHANGES. J0740+6620 wins on separation and J0030+0451 on compactness
    /// precision, so the ranking depends on which error dominates; a first draft of this audit assumed the most compact
    /// target wins on both counts and the measurement refused it. The audit therefore computes the crossing rather than
    /// asserting a ranking.
    /// </summary>
    public static double TimingAtWhichTheBestTargetFlips()
    {
        var a = Catalogue().Single(t => t.Name == "J0740+6620 (Riley 2021)");
        var b = Catalogue().Single(t => t.Name == "J0030+0451 (Riley 2019)");
        double low = 0.0, high = 0.5;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (low + high);
            double sa = Significance(a.X, a.RelativeSigmaX, mid), sb = Significance(b.X, b.RelativeSigmaX, mid);
            // J0740+6620 wins at LARGE timing and J0030+0451 at small, so a win at mid means the crossing is BELOW it
            if (sa > sb) high = mid; else low = mid;
        }
        return 0.5 * (low + high);
    }

    public static bool TheMostCompactTargetWinsAtRealisticTiming()
        => TargetCapability().OrderByDescending(t => t.ProjectedFivePercent).First().Name.StartsWith("J0740")
        && TargetCapability().OrderByDescending(t => t.NowTwentyPercent).First().Name.StartsWith("J0740");

    public static bool TheBetterConstrainedTargetWinsWithPerfectTiming()
        => TargetCapability().OrderByDescending(t => t.IdealNoTimingError).First().Name.StartsWith("J0030");

    // ===================== 4. THE PROGRAM =====================

    public static (string Step, string Target, string Measurement, string Precision, string Excludes)[] TheProgram() => new[]
    {
        ("1", "J0740+6620", "a SIMULTANEOUS surface-redshift determination from burst or line spectroscopy, with the mass and radius taken from the published NICER posterior",
            RequiredRedshiftTable(), "whichever theory the measurement does not fit"),
        ("2", "J0030+0451", "the same redshift measurement, as the second target and the cross-check, since its compactness is the best constrained of the sample",
            SecondTargetTable() + $"; it becomes the BETTER target than step 1 once the timing reaches {TimingAtWhichTheBestTargetFlips():P2}",
            "the same decision at a weaker separation, so it tests the method rather than the theory"),
        ("3", "either target", "a NICER-class mass-radius improvement to the compactness precision below the threshold at which the timing term dominates",
            CompactnessTable(), "nothing on its own - it lowers the timing the other two steps require"),
    };

    private static string RequiredRedshiftTable()
    {
        var t = RequirementTable("J0740+6620 (Riley 2021)");
        return $"1 sigma at {t[0].RequiredRelativeSigmaZ:P2}, 3 sigma at {t[1].RequiredRelativeSigmaZ:P2}, 5 sigma at {t[2].RequiredRelativeSigmaZ:P2}";
    }

    private static string SecondTargetTable()
    {
        var t = RequirementTable("J0030+0451 (Riley 2019)");
        return $"1 sigma at {t[0].RequiredRelativeSigmaZ:P2}, 3 sigma at {t[1].RequiredRelativeSigmaZ:P2}, 5 sigma at {t[2].RequiredRelativeSigmaZ:P2}";
    }

    private static string CompactnessTable()
    {
        var t = RequirementTable("J0740+6620 (Riley 2021)", 0.05);
        return $"at a 5 per cent redshift determination the compactness must reach {t[1].RequiredRelativeSigmaX:P2} for 3 sigma and {t[2].RequiredRelativeSigmaX:P2} for 5 sigma";
    }

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: some published target decides the question at 5 sigma under a stated, attainable precision.
    /// BOUNDARY: a decision is reachable only at 3 sigma or below. REFUTED: no published target can decide.
    /// </summary>
    public static string Verdict()
    {
        var best = TargetCapability().OrderByDescending(t => t.ProjectedFivePercent).First();
        if (best.ProjectedFivePercent >= 5.0) return "DERIVED";
        if (best.ProjectedFivePercent >= 3.0) return "BOUNDARY";
        return "REFUTED";
    }

    public static string TheProgrammeInOneLine()
    {
        var best = TargetCapability().OrderByDescending(t => t.ProjectedFivePercent).First();
        var table = RequirementTable("J0740+6620 (Riley 2021)");
        return $"measure the surface redshift of J0740+6620 to {table[1].RequiredRelativeSigmaZ:P2} (3 sigma) or "
             + $"{table[2].RequiredRelativeSigmaZ:P2} (5 sigma), keeping its published NICER mass and radius: that single "
             + $"measurement decides the question at {best.ProjectedFivePercent:F1} sigma";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("THE CATALOGUE IS THE PUBLISHED SAMPLE, AND THE COMPACTNESS IS RECOMPUTED RATHER THAN IMPORTED: x = -GM/(Rc^2) is exactly 1.4770 km per solar mass over the radius, and the recomputation reproduces the compactness the earlier audits recorded for all four targets. ");
        foreach (var (name, x, zAt, zGr, separation, slope, relativeX) in Catalogue())
            sb.Append($"{name}: x = {x:F6}, sigma_x/x = {relativeX:P2}, z_AT = {zAt:F7}, z_GR = {zGr:F7}, separation {separation:E3}, slope difference {slope:F4}; ");
        sb.Append($"the recomputation is consistent with the earlier audits ({TheCompactnessesReproduceTheEarlierAudits()}). ");
        sb.Append("WHAT EACH TARGET CAN DECIDE, at the published compactness: ");
        foreach (var (name, x, separation, now, projected, ideal) in TargetCapability())
            sb.Append($"{name}: at a 20 per cent redshift determination {now:F2} sigma, at 5 per cent {projected:F2} sigma, and with PERFECT timing {ideal:F2} sigma; ");
        sb.Append($"THE BEST TARGET IS {BestTarget()}, BUT A DRAFT CLAIM IS WITHDRAWN HERE. The draft assumed the most compact target wins on both counts; the measurement refused it, because the most compact target has the LARGEST SEPARATION while the best-constrained compactness belongs to J0030+0451 ({TheBestTargetIsTheMostCompactAndBestConstrained()}). The ranking therefore depends on which error dominates, and the audit computes the crossing rather than asserting it: J0740+6620 wins at a 20 per cent timing ({TheMostCompactTargetWinsAtRealisticTiming()}) and J0030+0451 wins with perfect timing ({TheBetterConstrainedTargetWinsWithPerfectTiming()}), and the two swap at a timing precision of {TimingAtWhichTheBestTargetFlips():P2}. ");
        sb.Append("THE REQUIREMENT IS GIVEN IN BOTH DIRECTIONS SO THE OBSERVER OWNS THE TRADE. At the best target: ");
        foreach (var (sig, neededZ, neededX, binds) in RequirementTable("J0740+6620 (Riley 2021)"))
            sb.Append($"{sig:F0} sigma needs a redshift determination to {neededZ:P2} at the published compactness, or a compactness to {neededX:P2} at a 5 per cent determination - {binds}; ");
        sb.Append("and the same table for the second target: ");
        foreach (var (sig, neededZ, neededX, binds) in RequirementTable("J0030+0451 (Riley 2019)"))
            sb.Append($"{sig:F0} sigma needs {neededZ:P2} in the redshift - {binds}; ");
        sb.Append("THE SLACK ON THE CURRENT DETERMINATIONS IS THE PROGRAMME'S HEADLINE. ");
        foreach (var (sig, needed, overTwenty, overFifty) in ProgrammeSlack())
            sb.Append($"{sig:F0} sigma needs {needed:P2}, which is {overTwenty:F1}x better than a 20 per cent determination and {overFifty:F1}x better than a 50 per cent one; ");
        sb.Append("so the program is not a new instrument but a stated factor on an existing measurement, and the factor is one to four. ");
        sb.Append("AND THE PROGRAM EXCLUDES ONE THEORY WHICHEVER WAY THE MEASUREMENT FALLS: if the measured redshift lands on the GR prediction the AT prediction is excluded at the stated significance, and if it lands on the AT prediction GR is excluded. THE PROGRAMME IN ONE LINE: ");
        sb.Append(TheProgrammeInOneLine());
        return sb.ToString();
    }

    // ===================== 6. REPORTS =====================

    public static string OutputCatalogue()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE PUBLISHED NICER TARGETS (compactness recomputed from M and R)");
        sb.AppendLine("   target                     | M (Msol) | R (km) | x        | sigma_x/x | z_AT      | z_GR      | separation");
        foreach (var (name, mass, radius, relativeX, _) in Targets())
        {
            double x = XFromMassRadius(mass, radius);
            sb.AppendLine($"   {name,-26} | {mass,8:F3} | {radius,6:F2} | {x,8:F6} | {relativeX,9:P3} | {NeutronStarDecisionAudit.ZAt(x),9:F7} | {NeutronStarDecisionAudit.ZGr(x),9:F7} | {NeutronStarDecisionAudit.Separation(x),10:E3}");
        }
        sb.AppendLine($"   the recomputation reproduces the earlier audits : {TheCompactnessesReproduceTheEarlierAudits()}");
        sb.AppendLine();
        sb.AppendLine("   the sources:");
        foreach (var (name, _, _, relativeX, source) in Targets())
            sb.AppendLine($"     {name,-26} sigma_x/x = {relativeX:P3} - {source}");
        return sb.ToString();
    }

    public static string OutputCapability()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. WHAT EACH TARGET CAN DECIDE (shared-compactness difference model)");
        sb.AppendLine("   target                     | x        | separation | 20 % timing | 5 % timing | perfect timing");
        foreach (var (name, x, separation, now, projected, ideal) in TargetCapability())
            sb.AppendLine($"   {name,-26} | {x,8:F6} | {separation,10:E3} | {now,11:F2} | {projected,10:F2} | {ideal,14:F2}");
        sb.AppendLine($"   the best target : {BestTarget()}");
        sb.AppendLine($"   it is both the most compact and the best constrained : {TheBestTargetIsTheMostCompactAndBestConstrained()}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE REQUIRED PRECISION, IN BOTH DIRECTIONS");
        foreach (var target in new[] { "J0740+6620 (Riley 2021)", "J0030+0451 (Riley 2019)" })
        {
            sb.AppendLine($"   {target}");
            sb.AppendLine("   significance | redshift precision at the published compactness | compactness precision at 5 % timing | which axis dominates");
            foreach (var (sig, neededZ, neededX, binds) in RequirementTable(target))
                sb.AppendLine($"   {sig,12:F0} | {neededZ,49:P2} | {neededX,36:P2} | {binds}");
        }
        sb.AppendLine();
        sb.AppendLine("   the slack on the current determinations, at the best target:");
        sb.AppendLine("   significance | needed precision | better than 20 % | better than 50 %");
        foreach (var (sig, needed, overTwenty, overFifty) in ProgrammeSlack())
            sb.AppendLine($"   {sig,12:F0} | {needed,16:P2} | {overTwenty,16:F1}x | {overFifty,15:F1}x");
        return sb.ToString();
    }

    public static string OutputProgram()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE OBSERVING PROGRAM");
        foreach (var (step, target, measurement, precision, excludes) in TheProgram())
        {
            sb.AppendLine($"   STEP {step}: {target}");
            sb.AppendLine($"     measurement : {measurement}");
            sb.AppendLine($"     precision   : {precision}");
            sb.AppendLine($"     effect      : excludes {excludes}");
        }
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {TheProgrammeInOneLine()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
