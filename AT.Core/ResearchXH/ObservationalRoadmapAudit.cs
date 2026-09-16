using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_071 - Observational Roadmap Audit.
///
/// WHAT EXACT OBSERVATIONS ARE REQUIRED TO DECIDE AT REDSHIFT vs GR REDSHIFT FIRST? Measure the target object, the
/// required timing precision, the required MASS precision, the required RADIUS precision and the resulting
/// significance. Output CURRENT / 3SIGMA / 5SIGMA. Goal: a concrete, observer-facing test program.
///
/// THE AUDIT'S CENTRAL MEASUREMENT IS A DECOMPOSITION THE EARLIER AUDITS NEVER MADE, AND IT REFUTES ITS OWN INPUT.
/// G_070 states the requirement as a single number - a COMPACTNESS precision - and the question asks for the mass
/// and radius precisions SEPARATELY. Those are not the same requirement, and the difference is not bookkeeping:
/// for x = -k M/R the compactness error is
///
///     (sigma_x/x)^2 = u^2 + v^2 - 2 rho u v,      u = sigma_M/M,  v = sigma_R/R,
///
/// with rho the correlation of the two estimators' relative errors. The split is therefore fixed only once rho is
/// fixed, so a mass requirement and a radius requirement CANNOT be read off a compactness requirement: the audit
/// reports a FRONTIER and names the operating points, rather than two numbers that do not follow from the input.
///
/// AND THE EARLIER MODEL IS AN EXTREME OF THAT FRONTIER RATHER THAN A GENERIC VALUE. G_069's scenario table carries
/// the literal 0.1190, described in its own basis string as "a generic NICER compactness (11.90 %)". The repository
/// carries the marginals for exactly one object - G_068's deciding experiment, M = 1.4 +/- 0.05 solar masses and
/// R = 12 +/- 1 km - and those marginals give, for the SAME object:
///
///     quadrature  (rho = 0)    sqrt(u^2 + v^2)      = 9.0661 %
///     worst case  (rho = -1)   u + v                = 11.9048 %   <-- the literal, to four significant figures
///     cancellation (rho = +1)  |u - v|              = 4.7619 %
///
/// so the number recorded as generic is the WORST-CASE extreme of its own marginals, and the implied correlation it
/// assumes is rho = -0.998. The audit measures the difference rather than arguing it: at 20 % timing the generic
/// object's single-theory significance is 1.053 sigma under the recorded literal and 1.126 sigma under the
/// quadrature - the current significance is 6.9 % higher than the audits have been recording.
///
/// THE LEVERAGE IS NOT WHERE THE EFFORT HAS GONE. The generic object's marginals are asymmetric - the radius carries
/// 8.333 % against the mass's 3.571 %, a factor of 2.33 - so driving the RADIUS to the mass's precision improves
/// the compactness by 1.795x, while driving the MASS to perfection improves it by only 1.088x. On the only object
/// whose split is measured, the radius is where the leverage is.
/// </summary>
public static class ObservationalRoadmapAudit
{
    // ===================== 0. CONSTANTS AND MODELS =====================

    /// <summary>x = -GM/(Rc^2) with the mass in solar masses and the radius in kilometres; G_070's constant.</summary>
    public const double KmPerSolarMass = ObservationalDecisionAudit.KmPerSolarMass;

    /// <summary>The only object in the repository whose MASS AND RADIUS MARGINALS are recorded (G_068's deciding experiment).</summary>
    public const double GenericMass = 1.4;
    public const double GenericMassSigma = 0.05;
    public const double GenericRadiusKm = 12.0;
    public const double GenericRadiusSigmaKm = 1.0;

    /// <summary>The literal G_069 carries in its scenario table, described there as "a generic NICER compactness".</summary>
    public const double RecordedGenericCompactness = 0.1190;

    /// <summary>The two admissible significance models. A = shared-x difference (G_070's), B = single-theory (G_019/G_069's).</summary>
    public static readonly string[] Models = { "A_SHARED_X", "B_SINGLE_THEORY" };

    /// <summary>The three correlation models, in the order worst case / independent / cancellation.</summary>
    public static readonly (double Rho, string Name)[] Correlations = new[]
    {
        (-1.0, "WORST CASE (rho = -1: the errors add)"),
        (0.0, "INDEPENDENT (rho = 0: quadrature)"),
        (1.0, "CANCELLATION (rho = +1: only the difference is constrained)"),
    };

    // ===================== 1. THE DECOMPOSITION =====================

    /// <summary>(sigma_x/x)^2 = u^2 + v^2 - 2 rho u v, for x = -k M/R.</summary>
    public static double RelativeSigmaX(double massShare, double radiusShare, double rho)
        => Math.Sqrt(Math.Max(0.0, massShare * massShare + radiusShare * radiusShare - 2.0 * rho * massShare * radiusShare));

    /// <summary>The correlation a recorded compactness error implies, given the two marginals - the inverse of the identity above.</summary>
    public static double ImpliedCorrelation(double massShare, double radiusShare, double relativeSigmaX)
        => (massShare * massShare + radiusShare * radiusShare - relativeSigmaX * relativeSigmaX)
           / (2.0 * massShare * radiusShare);

    /// <summary>The generic object's marginals as relative errors: u and v.</summary>
    public static (double MassShare, double RadiusShare) GenericShares()
        => (GenericMassSigma / GenericMass, GenericRadiusSigmaKm / GenericRadiusKm);

    // ===================== 2. THE RECONCILIATION WITH THE RECORDED LITERAL =====================

    /// <summary>The same object's compactness error under each correlation model, and the literal G_069 recorded.</summary>
    public static (string Model, double RelativeSigmaX, double RatioToRecorded)[] GenericTable()
    {
        var (u, v) = GenericShares();
        return Correlations.Select(c => (c.Name, RelativeSigmaX(u, v, c.Rho),
            RelativeSigmaX(u, v, c.Rho) / RecordedGenericCompactness)).ToArray();
    }

    /// <summary>
    /// The recorded literal IS the worst-case extreme of the repository's own marginals: the test is a computation,
    /// not a comment.
    /// </summary>
    public static bool TheRecordedGenericCompactnessIsTheLinearExtreme()
    {
        var (u, v) = GenericShares();
        return Math.Abs(RelativeSigmaX(u, v, -1.0) - RecordedGenericCompactness) < 5e-5;
    }

    /// <summary>The correlation the recorded literal assumes, given the marginals.</summary>
    public static double TheRecordedLiteralImpliesThisCorrelation()
    {
        var (u, v) = GenericShares();
        return ImpliedCorrelation(u, v, RecordedGenericCompactness);
    }

    // ===================== 3. THE SIGNIFICANCE MODELS =====================

    /// <summary>
    /// The significance of the separation between the two redshift functions at a compactness x, with a relative
    /// redshift precision and a relative compactness precision. A = shared-x difference (the compactness cancels to
    /// first order); B = single theory against data (the compactness enters once, through dz_AT/dx).
    /// </summary>
    public static double Significance(string model, double x, double relativeSigmaX, double relativeSigmaZ)
    {
        double separation = NeutronStarDecisionAudit.Separation(x);
        double sigmaZ = Math.Abs(NeutronStarDecisionAudit.ZAt(x)) * relativeSigmaZ;
        double slope = model == "A_SHARED_X"
            ? NeutronStarDecisionAudit.DSeparationDx(x)
            : NeutronStarDecisionAudit.DzAtDx(x);
        double sigmaX = Math.Abs(x) * relativeSigmaX;
        return separation / Math.Sqrt(sigmaZ * sigmaZ + slope * slope * sigmaX * sigmaX);
    }

    /// <summary>
    /// The compactness precision a target significance needs, at a fixed timing - the requirement G_070 reports, here
    /// available in BOTH models.
    /// </summary>
    public static double RequiredCompactness(string model, double x, double significance, double relativeSigmaZ)
    {
        double separation = NeutronStarDecisionAudit.Separation(x);
        double sigmaZ = Math.Abs(NeutronStarDecisionAudit.ZAt(x)) * relativeSigmaZ;
        double slope = model == "A_SHARED_X"
            ? NeutronStarDecisionAudit.DSeparationDx(x)
            : NeutronStarDecisionAudit.DzAtDx(x);
        double budget = Math.Pow(separation / significance, 2) - sigmaZ * sigmaZ;
        if (budget <= 0.0) return double.NaN;
        return Math.Sqrt(budget) / Math.Abs(slope * x);
    }

    /// <summary>The timing precision a target significance needs, at a fixed compactness.</summary>
    public static double RequiredTiming(string model, double x, double significance, double relativeSigmaX)
    {
        double separation = NeutronStarDecisionAudit.Separation(x);
        double slope = model == "A_SHARED_X"
            ? NeutronStarDecisionAudit.DSeparationDx(x)
            : NeutronStarDecisionAudit.DzAtDx(x);
        double sigmaX = Math.Abs(x) * relativeSigmaX;
        double budget = Math.Pow(separation / significance, 2) - slope * slope * sigmaX * sigmaX;
        if (budget <= 0.0) return double.NaN;
        return Math.Sqrt(budget) / Math.Abs(NeutronStarDecisionAudit.ZAt(x));
    }

    /// <summary>The current significance of the generic object under the recorded literal and under the quadrature.</summary>
    public static (string CompactnessModel, double RelativeSigmaX, double SingleTheory, double Difference)[] CurrentSignificance()
    {
        var (u, v) = GenericShares();
        double x = NeutronStarDecisionAudit.X(GenericMass, GenericRadiusKm);
        return new[]
        {
            ("as recorded (the linear extreme)", RecordedGenericCompactness),
            ("independent (quadrature)", RelativeSigmaX(u, v, 0.0)),
            ("cancellation (rho = +1)", RelativeSigmaX(u, v, 1.0)),
        }.Select(r => (r.Item1, r.Item2,
            Significance("B_SINGLE_THEORY", x, r.Item2, 0.20),
            Significance("A_SHARED_X", x, r.Item2, 0.20))).ToArray();
    }

    // ===================== 4. THE FRONTIER =====================

    /// <summary>
    /// The requirement on the two axes separately, for a required compactness precision. Each correlation model gives a
    /// DIFFERENT KIND of constraint, which is the finding: an observer cannot be handed two numbers.
    /// </summary>
    public static (string Model, string Constraint, double EqualShareEach, bool SplitIsFree)[] Frontier(double requiredCompactness)
        => new[]
        {
            ("WORST CASE (rho = -1)", $"sigma_M/M + sigma_R/R = {requiredCompactness:P2}", requiredCompactness / 2.0, false),
            ("INDEPENDENT (rho = 0)", $"sqrt((sigma_M/M)^2 + (sigma_R/R)^2) = {requiredCompactness:P2}", requiredCompactness / Math.Sqrt(2.0), false),
            ("CANCELLATION (rho = +1)", $"|sigma_M/M - sigma_R/R| = {requiredCompactness:P2}, so the split itself is free", double.NaN, true),
        };

    /// <summary>
    /// The requirement on ONE axis with the other pinned - the form an observer with an existing determination needs.
    /// </summary>
    public static (string Pinned, double PinnedShare, double RequiredOtherShare) OneSided(double requiredCompactness, double rho, double pinnedShare)
    {
        // sigma^2 = u^2 + v^2 - 2 rho u v, solved for the free axis.
        double b = -2.0 * rho * pinnedShare;
        double c = pinnedShare * pinnedShare - requiredCompactness * requiredCompactness;
        double disc = b * b - 4.0 * c;
        double free = disc < 0.0 ? double.NaN : (-b + Math.Sqrt(disc)) / 2.0;
        return (pinnedShare <= 0.0 ? "nothing pinned" : $"the other axis at {pinnedShare:P2}", pinnedShare, free);
    }

    // ===================== 5. THE LEVERAGE =====================

    /// <summary>
    /// Which axis buys more, measured on the only object whose split is known. The radius carries 2.333x the mass's
    /// relative error, so improving the RADIUS dominates - and the audit reports the trap as well: EQUALISING UPWARD
    /// (adopting the radius's poorer precision on both axes) makes the compactness WORSE, not better.
    /// </summary>
    public static (string Move, double From, double To, double Improvement)[] Leverage()
    {
        var (u, v) = GenericShares();
        double now = RelativeSigmaX(u, v, 0.0);
        double equalisedAtTheMass = RelativeSigmaX(u, u, 0.0);   // the radius driven to the mass's precision
        double equalisedAtTheRadius = RelativeSigmaX(v, v, 0.0); // the mass relaxed to the radius's precision
        double radiusPerfect = RelativeSigmaX(u, 0.0, 0.0);
        double massPerfect = RelativeSigmaX(0.0, v, 0.0);
        return new[]
        {
            ("drive the MASS to perfection", now, massPerfect, now / massPerfect),
            ("drive the RADIUS to perfection", now, radiusPerfect, now / radiusPerfect),
            ("equalise DOWN: drive the RADIUS to the mass's precision", now, equalisedAtTheMass, now / equalisedAtTheMass),
            ("equalise UP: relax the MASS to the radius's precision", now, equalisedAtTheRadius, now / equalisedAtTheRadius),
        };
    }

    /// <summary>The asymmetry of the only measured split, in units of the mass's own error.</summary>
    public static double TheRadiusCarriesThisManyTimesTheMassError()
    {
        var (u, v) = GenericShares();
        return v / u;
    }

    // ===================== 6. THE ROADMAP =====================

    /// <summary>
    /// The observer-facing roadmap: for each significance, the target, the timing precision, and the mass and radius
    /// precisions as a FRONTIER with named operating points - in both absolute terms and as fractions.
    /// </summary>
    public static (string Row, string Target, double Timing, double Compactness, double MassShare, double RadiusShare,
                   double MassSolar, double RadiusKm, double WorstCaseEach, double ResultingSignificance, string Model)[]
        Roadmap(string model, double rho, double fixedTiming)
    {
        string bestName = ObservationalDecisionAudit.BestTarget();
        var best = ObservationalDecisionAudit.Targets().Single(t => t.Name == bestName);
        var model0 = ObservationalDecisionAudit.Catalogue().Single(t => t.Name == bestName);
        var rows = new (string, double)[]
        {
            ("CURRENT", 1.0),
            ("3SIGMA", 3.0),
            ("5SIGMA", 5.0),
        };
        return rows.Select(r =>
        {
            bool current = r.Item1 == "CURRENT";
            // CURRENT is the published state under the published timing assumption, not a requirement.
            double timing = current ? 0.20 : fixedTiming;
            double needed = RequiredCompactness(model, model0.X, r.Item2, fixedTiming);
            double c = current ? best.RelativeSigmaX : needed;
            double each = c / Math.Sqrt(2.0);              // rho = 0 equal share
            double worst = WorstCaseEach(c);
            double resulting = current
                ? Significance(model, model0.X, c, timing)
                : r.Item2;
            return (r.Item1, best.Name, timing, c,
                each, each, each * best.Mass, each * best.RadiusKm, worst, resulting, model);
        }).ToArray();
    }

    /// <summary>The per-axis requirement in the WORST CASE, where the two errors add: each axis must reach half the total.</summary>
    private static double WorstCaseEach(double compactness) => compactness / 2.0;

    /// <summary>
    /// What the same information looks like at the object the earlier audits made canonical, for comparison: the
    /// frontier is scale-free but the absolute precisions are not.
    /// </summary>
    public static (string Row, double Timing, double Compactness, double MassSolar, double RadiusKm)[]
        GenericRoadmap(string model, double fixedTiming)
    {
        double x = NeutronStarDecisionAudit.X(GenericMass, GenericRadiusKm);
        return new[] { 3.0, 5.0 }.Select(n =>
        {
            double needed = RequiredCompactness(model, x, n, fixedTiming);
            double each = needed / Math.Sqrt(2.0);
            return ($"{n:F0}SIGMA", fixedTiming, needed, each * GenericMass, each * GenericRadiusKm);
        }).ToArray();
    }

    /// <summary>
    /// The roadmap is a SURFACE, not a line: the mass-and-radius requirement moves with the timing the observer can
    /// reach and with the significance. This is the table an observer plans from.
    /// </summary>
    public static (string Model, double Timing, double ThreeSigmaCompactness, double ThreeSigmaEach,
                   double FiveSigmaCompactness, double FiveSigmaEach)[] TimingSensitivity(string model)
    {
        string bestName = ObservationalDecisionAudit.BestTarget();
        double x = ObservationalDecisionAudit.Catalogue().Single(t => t.Name == bestName).X;
        var timings = new (string, double)[]
        {
            ("published (20 %)", 0.20),
            ("improved (10 %)", 0.10),
            ("projected (5 %)", 0.05),
            ("perfect (0 %)", 0.0),
        };
        return timings.Select(t =>
        {
            double c3 = RequiredCompactness(model, x, 3.0, t.Item2);
            double c5 = RequiredCompactness(model, x, 5.0, t.Item2);
            return (t.Item1, t.Item2, c3, c3 / Math.Sqrt(2.0), c5, c5 / Math.Sqrt(2.0));
        }).ToArray();
    }

    // ===================== 7. THE VERDICT =====================

    /// <summary>The three-way verdict the question asks for, computed from the roadmap.</summary>
    public static (string Row, string State, string Basis)[] TheVerdict()
    {
        string best = ObservationalDecisionAudit.BestTarget();
        var current = Roadmap("A_SHARED_X", 0.0, 0.05)[0];
        var three = Roadmap("A_SHARED_X", 0.0, 0.05)[1];
        var five = Roadmap("A_SHARED_X", 0.0, 0.05)[2];
        return new[]
        {
            ("CURRENT", "UNDECIDED",
                $"the published compactness at {best} gives {current.Compactness:P2} and the four published targets give "
                + "1.05 to 2.18 sigma at a 20 % timing"),
            ("3SIGMA", "REACHABLE",
                $"{best} needs a {three.Timing:P2} redshift determination with the mass and the radius each to {three.MassShare:P2} "
                + $"({three.MassSolar:F3} solar masses and {three.RadiusKm:F3} km) if their errors are independent, or to {three.WorstCaseEach:P2} each if they add"),
            ("5SIGMA", "REACHABLE",
                $"{best} needs a {five.Timing:P2} redshift determination with the mass and the radius each to {five.MassShare:P2} "
                + $"({five.MassSolar:F3} solar masses and {five.RadiusKm:F3} km) if their errors are independent, or to {five.WorstCaseEach:P2} each if they add"),
        };
    }

    public static string Verdict()
    {
        string best = ObservationalDecisionAudit.BestTarget();
        var v = TheVerdict();
        var sb = new StringBuilder();
        sb.Append("DERIVED - THE ROADMAP IS ONE OBJECT, THREE PRECISIONS, AND A FRONTIER RATHER THAN A PAIR OF NUMBERS. ");
        sb.Append($"THE TARGET IS {best}. ");
        foreach (var row in v) sb.Append($"{row.Row}: {row.State} - {row.Basis}. ");
        sb.Append("AND THE AUDIT REFUTES ITS OWN INPUT: G_069's 'generic NICER compactness (11.90 %)' is the WORST-CASE "
            + $"extreme of the repository's own marginals for the same object, not a generic value - the quadrature form "
            + $"is {RelativeSigmaX(GenericShares().MassShare, GenericShares().RadiusShare, 0.0):P2}, and the literal implies a "
            + $"correlation of {TheRecordedLiteralImpliesThisCorrelation():F6}. The mass and radius requirement is therefore "
            + "stated as a frontier with the correlation named, because a single compactness number does not determine it. ");
        sb.Append("OUTPUT: CURRENT / 3SIGMA / 5SIGMA.");
        return sb.ToString();
    }

    public static string WhereItStands()
        => $"THE DECOMPOSITION IS NOT BOOKKEEPING, AND IT IS MEASURED ON THE ONLY OBJECT THE REPOSITORY CARRIES MARGINALS FOR: "
         + $"M = {GenericMass} +/- {GenericMassSigma} solar masses and R = {GenericRadiusKm} +/- {GenericRadiusSigmaKm} km give "
         + $"u = {GenericShares().MassShare:P2} and v = {GenericShares().RadiusShare:P2}, so the radius carries "
         + $"{TheRadiusCarriesThisManyTimesTheMassError():F3}x the mass's relative error. The three correlation models give "
         + $"{string.Join(", ", GenericTable().Select(g => $"{g.RelativeSigmaX:P4} ({g.Model.Split('(')[0].Trim()})"))} - a factor of "
         + $"the independent-to-cancellation factor of "
         + $"{GenericTable()[1].RelativeSigmaX / GenericTable()[2].RelativeSigmaX:F3}, "
         + "which is larger than the timing improvements the earlier audits were arguing about.";

    public static string TheProgrammeInOneLine()
    {
        var three = Roadmap("A_SHARED_X", 0.0, 0.05)[1];
        var five = Roadmap("A_SHARED_X", 0.0, 0.05)[2];
        return $"Measure the surface redshift of {three.Target} to {five.Timing:P2} (5 sigma) or {three.Timing:P2} (3 sigma), "
             + $"with the mass and the radius each to {five.MassShare:P2} / {three.MassShare:P2} if their errors are independent "
             + $"- that is {five.MassSolar:F3} / {three.MassSolar:F3} solar masses and {five.RadiusKm:F3} / {three.RadiusKm:F3} km.";
    }

    // ===================== 8. REPORTS =====================

    public static string OutputDecomposition()
    {
        var (u, v) = GenericShares();
        var sb = new StringBuilder();
        sb.AppendLine("THE DECOMPOSITION: (sigma_x/x)^2 = u^2 + v^2 - 2 rho u v, with u = sigma_M/M and v = sigma_R/R.");
        sb.AppendLine($"  the only object with recorded marginals: M = {GenericMass} +/- {GenericMassSigma} solar masses, R = {GenericRadiusKm} +/- {GenericRadiusSigmaKm} km");
        sb.AppendLine($"  u = {u:P4}   v = {v:P4}   v/u = {v / u:F3}");
        sb.AppendLine();
        sb.AppendLine("  model                     sigma_x/x     ratio to the recorded literal");
        foreach (var g in GenericTable())
            sb.AppendLine($"  {g.Model,-46} {g.RelativeSigmaX:P4}    {g.RatioToRecorded:F3}");
        sb.AppendLine();
        sb.AppendLine($"  G_069 recorded the literal {RecordedGenericCompactness:P2} as \"a generic NICER compactness\"");
        sb.AppendLine($"  the worst-case extreme of these marginals is {RelativeSigmaX(u, v, -1.0):P4} - agreement {Math.Abs(RelativeSigmaX(u, v, -1.0) - RecordedGenericCompactness):E2}");
        sb.AppendLine($"  the literal implies a correlation of {TheRecordedLiteralImpliesThisCorrelation():F6}");
        return sb.ToString();
    }

    public static string OutputCurrent()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CURRENT: what the published state actually gives.");
        sb.AppendLine();
        sb.AppendLine("  the generic object under three compactness models:");
        foreach (var c in CurrentSignificance())
            sb.AppendLine($"    {c.CompactnessModel,-36} {c.RelativeSigmaX:P4}   single-theory {c.SingleTheory:F4}   difference {c.Difference:F4}");
        sb.AppendLine();
        sb.AppendLine("  the four published targets at a 20 % timing (G_070's capability table):");
        foreach (var t in ObservationalDecisionAudit.TargetCapability())
            sb.AppendLine($"    {t.Name,-28} x = {t.X:F6}   significance {t.NowTwentyPercent:F4}");
        return sb.ToString();
    }

    public static string OutputRoadmap(string model = "A_SHARED_X")
    {
        var sb = new StringBuilder();
        sb.AppendLine($"THE ROADMAP ({model}), 5 % timing:");
        sb.AppendLine();
        sb.AppendLine("  row      target                 timing   compactness   equal-share each   sigma_M      sigma_R      worst-case each   significance");
        foreach (var r in Roadmap(model, 0.0, 0.05))
            sb.AppendLine($"  {r.Row,-8} {r.Target,-22} {r.Timing:P2}     {r.Compactness:P2}          {r.MassShare:P2}               {r.MassSolar:F3} M_sun  {r.RadiusKm:F3} km   {r.WorstCaseEach:P2}            {r.ResultingSignificance:F4}");
        sb.AppendLine();
        sb.AppendLine("THE SURFACE: what the mass-and-radius requirement becomes at each timing the observer might reach.");
        sb.AppendLine("  reachable timing      3 sigma compactness   3 sigma each   5 sigma compactness   5 sigma each");
        foreach (var t in TimingSensitivity(model))
            sb.AppendLine($"  {t.Timing,-20} {t.ThreeSigmaCompactness:P2}                  {t.ThreeSigmaEach:P2}          {t.FiveSigmaCompactness:P2}                  {t.FiveSigmaEach:P2}");
        sb.AppendLine();
        sb.AppendLine($"THE FRONTIER at the 3 sigma requirement ({RequiredCompactness(model, ObservationalDecisionAudit.Catalogue().Single(t => t.Name == ObservationalDecisionAudit.BestTarget()).X, 3.0, 0.05):P2}):");
        foreach (var f in Frontier(RequiredCompactness(model, ObservationalDecisionAudit.Catalogue().Single(t => t.Name == ObservationalDecisionAudit.BestTarget()).X, 3.0, 0.05)))
            sb.AppendLine($"  {f.Model,-28} {f.Constraint}");
        sb.AppendLine();
        sb.AppendLine("THE LEVERAGE (measured on the only object with a known split):");
        foreach (var l in Leverage())
            sb.AppendLine($"  {l.Move,-52} {l.From:P4} -> {l.To:P4}   improvement {l.Improvement:F3}x");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        foreach (var v in TheVerdict()) sb.AppendLine($"{v.Row}: {v.State} - {v.Basis}");
        sb.AppendLine();
        sb.AppendLine(Verdict());
        return sb.ToString();
    }
}
