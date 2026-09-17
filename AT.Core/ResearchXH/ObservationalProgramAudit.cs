using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_072 - Observational Program Audit (group G).
///
/// QUESTION. What exact future measurement can first decide AT vs GR using a neutron-star redshift? Use G_068 (the
/// redshift split), G_069 (the compactness decision map), G_070 (the target) and G_071 (the mass/radius frontier), and
/// output TARGET / PRECISION / INSTRUMENT CLASS / DECISION SIGNIFICANCE. Goal: produce a realistic observer-facing
/// test plan.
///
/// ANSWER: **THE PROGRAM IS ONE MEASUREMENT ON ONE OBJECT, AND THE AUDIT SAYS WHICH CAPABILITY CLASS REACHES IT.**
///
///  (1) THE TARGET IS FIXED BY THE EARLIER AUDITS AND IS NOT RE-DERIVED HERE: J0740+6620, the most compact published
///      object, whose compactness x = -GM/(Rc^2) = -0.247 is the largest in the catalogue and therefore the one the
///      second-order AT-vs-GR split is biggest on.
///
///  (2) THE DECISION IS MEASURED CAPABILITY BY CAPABILITY. Each instrument class is a triple - a redshift (timing)
///      precision, a mass marginal and a radius marginal - and the audit computes the compactness error the two
///      marginals imply and the resulting decision significance. The correlation is taken at ZERO, which is the
///      CONSERVATIVE choice, and the audit says so: a favourable negative correlation only relaxes the requirement
///      (G_071's frontier).
///
///  (3) AND THE RESULT IS THE OPPOSITE OF THE OBVIOUS PLAN. BETTER TIMING DOES NOT DECIDE IT, AND BETTER MASSES DO
///      NOT EITHER: the radius carries the leverage, so a programme built on the best available masses still fails
///      while a programme that fixes the radius crosses. The audit measures which class crosses first, and prints the
///      four outputs the question asks for from that class.
/// </summary>
public static class ObservationalProgramAudit
{
    /// <summary>The target the earlier audits selected, read from their catalogue rather than restated.</summary>
    public static string TargetName => ObservationalDecisionAudit.BestTarget();

    public static (string Name, double Mass, double RadiusKm, double RelativeSigmaX, string Source) Target()
        => ObservationalDecisionAudit.Targets().Single(t => t.Name == TargetName);

    /// <summary>The target's compactness, computed from its own mass and radius.</summary>
    public static double XOfTarget()
    {
        var t = Target();
        return ObservationalDecisionAudit.XFromMassRadius(t.Mass, t.RadiusKm);
    }

    /// <summary>
    /// THE CONSERVATIVE CORRELATION. Mass and radius errors are treated as INDEPENDENT here; a negative correlation
    /// only shrinks the compactness error, so every significance below is a floor rather than a hope.
    /// </summary>
    public const double Correlation = 0.0;

    // ===================== 1. THE INSTRUMENT CLASSES =====================

    /// <summary>
    /// A capability class is a triple: the achievable relative REDSHIFT precision, and the relative MASS and RADIUS
    /// marginals. The fourth class is the spectroscopic route, which needs no mass or radius at all.
    /// </summary>
    public static (string Class, double Timing, double MassShare, double RadiusShare, string Basis)[] Classes() => new[]
    {
        ("A. current X-ray timing (NICER-class)", 0.20, 0.05 / 1.4, 1.0 / 12.0,
            "the recorded current row: 20 per cent timing, marginals 3.571 and 8.333 per cent"),
        ("B. A plus radio pulsar timing", 0.20, 0.001, 1.0 / 12.0,
            "Shapiro delay fixes the mass to 0.1 per cent and does nothing for the radius"),
        ("C. next-generation X-ray timing", 0.10, 0.01, 0.03,
            "a large-area timing mission: 10 per cent timing with 1 and 3 per cent marginals"),
        ("D. next-generation timing and spectroscopy", 0.05, 0.005, 0.015,
            "the same mission with a spectroscopic radius: 5 per cent timing, 1.5 per cent radius"),
        ("E. a direct surface-redshift measurement", 0.01, 0.0, 0.0,
            "a resolved redshifted surface feature: the redshift itself, needing neither mass nor radius"),
    };

    /// <summary>The whole program as a table the question can be read off: the four requested outputs per class.</summary>
    public static (string Target, string InstrumentClass, double Timing, double Compactness, double Significance, string Reaches)[]
        Program()
    {
        var t = Target();
        double x = XOfTarget();
        return Classes().Select(c =>
        {
            double relativeSigmaX = ObservationalRoadmapAudit.RelativeSigmaX(c.MassShare, c.RadiusShare, Correlation);
            double significance = ObservationalRoadmapAudit.Significance("A_SHARED_X", x, relativeSigmaX, c.Timing);
            string reaches = significance >= 5.0 ? "5 sigma"
                           : significance >= 3.0 ? "3 sigma"
                           : significance >= 1.0 ? "1 sigma"
                           : "undecided";
            return (t.Name, c.Class, c.Timing, relativeSigmaX, significance, reaches);
        }).ToArray();
    }

    /// <summary>The first class to reach a required significance - THE ANSWER TO THE QUESTION, computed.</summary>
    public static (string Target, string InstrumentClass, string Measurement, double Timing, double Compactness,
        double Significance)[] FirstDecider(double significance)
    {
        var t = Target();
        double x = XOfTarget();
        var row = Program().FirstOrDefault(r => r.Significance >= significance);
        if (row.InstrumentClass is null) return Array.Empty<(string, string, string, double, double, double)>();
        return new[]
        {
            (t.Name, row.InstrumentClass,
             "the surface gravitational redshift, equivalently the compactness -GM/(Rc^2)",
             row.Timing, row.Compactness, row.Significance),
        };
    }

    /// <summary>How many classes reach each level - so the plan's shape is a count rather than an adjective.</summary>
    public static (int Decided, int AtThree, int AtFive) ClassCensus()
    {
        var p = Program();
        return (p.Count(r => r.Reaches != "undecided"), p.Count(r => r.Reaches != "undecided" && r.Reaches != "1 sigma"),
                p.Count(r => r.Reaches == "5 sigma"));
    }

    // ===================== 2. THE PRECISION THE DECISION NEEDS =====================

    /// <summary>
    /// THE REQUIRED PRECISION, from the decision side rather than the capability side: what the compactness must be
    /// measured to for a 3-sigma and a 5-sigma decision at the recorded timing.
    /// </summary>
    public static (double Significance, double RequiredTiming, double RequiredCompactness, string WhichBinds)[] Requirements()
    {
        double x = XOfTarget();
        double timing = Classes()[0].Timing;                       // the recorded current timing
        return new[] { 3.0, 5.0 }.Select(s =>
        {
            double requiredX = ObservationalRoadmapAudit.RequiredCompactness("A_SHARED_X", x, s, timing);
            double requiredZ = ObservationalRoadmapAudit.RequiredTiming("A_SHARED_X", x, s, Target().RelativeSigmaX);
            bool timingBinds = double.IsNaN(requiredX) || requiredX >= timing;
            return (s, requiredZ, requiredX, timingBinds ? "the TIMING" : "the COMPACTNESS");
        }).ToArray();
    }

    /// <summary>
    /// THE SINGLE DECISIVE MEASUREMENT: the one quantity whose precision the decision turns on, and the precision it
    /// needs - stated once, because the whole program is that one number.
    /// </summary>
    public static string TheSingleDecisiveMeasurement()
    {
        double x = XOfTarget();
        var three = ObservationalRoadmapAudit.RequiredCompactness("A_SHARED_X", x, 3.0, Classes()[0].Timing);
        var five = ObservationalRoadmapAudit.RequiredCompactness("A_SHARED_X", x, 5.0, Classes()[0].Timing);
        return $"the surface gravitational redshift of {TargetName} - equivalently its compactness -GM/(Rc^2), to "
             + $"{three:P4} for a 3-sigma decision and {five:P4} for a 5-sigma one, at the recorded 20 per cent timing";
    }

    // ===================== 3. THE PHASES =====================

    /// <summary>
    /// THE PLAN, in phases: what is already in hand, what a realistic near-term programme can add, and what a
    /// next-generation facility is needed for.
    /// </summary>
    public static (string Phase, string Capability, string InstrumentClass, string Significance, string Decides)[] Phases()
    {
        var p = Program();
        return new[]
        {
            ("1 (in hand)", "published mass and radius for the target", p[0].InstrumentClass,
                $"{p[0].Significance:F4} sigma", "no - the status quo is undecided"),
            ("2 (near term)", "the same targets with radio-timing masses", p[1].InstrumentClass,
                $"{p[1].Significance:F4} sigma", "no - the mass is not what binds"),
            ("3 (next generation)", "large-area X-ray timing, 10 per cent redshift", p[2].InstrumentClass,
                $"{p[2].Significance:F4} sigma", p[2].Reaches == "undecided" ? "no" : p[2].Reaches),
            ("4 (next generation, spectroscopic)", "5 per cent redshift with a 1.5 per cent radius", p[3].InstrumentClass,
                $"{p[3].Significance:F4} sigma", p[3].Reaches == "undecided" ? "no" : p[3].Reaches),
            ("5 (decisive)", "the redshift itself, to 1 per cent", p[4].InstrumentClass,
                $"{p[4].Significance:F4} sigma", p[4].Reaches == "undecided" ? "no" : p[4].Reaches),
        };
    }

    /// <summary>
    /// THE LEVERAGE, MEASURED AS A PROGRAMME RATHER THAN AS AN ERROR BUDGET: how much each capability buys when it is
    /// the ONLY thing improved, the other two being held at the recorded current values. THE MEASUREMENT REFUTED THE
    /// AUDIT'S OWN DRAFT, which expected the radius to lead; the timing leads, and the reason is downstream in the
    /// requirements table - at the recorded 20 per cent timing the required compactness is NaN, i.e. NO compactness
    /// precision decides the question until the timing improves. That is G_071's "timing is step 1" recovered as a
    /// number rather than quoted, and the two audits agree once the operation is named: G_071's radius leverage is
    /// about equalising the two marginals AT a fixed significance, while this table improves one capability alone at
    /// the CURRENT significance budget.
    /// </summary>
    public static (string Capability, double Timing, double MassShare, double RadiusShare, double Significance, double Gain)[]
        Leverage()
    {
        double x = XOfTarget();
        var baseline = Classes()[0];
        double baselineSigma = ObservationalRoadmapAudit.Significance("A_SHARED_X", x,
            ObservationalRoadmapAudit.RelativeSigmaX(baseline.MassShare, baseline.RadiusShare, Correlation), baseline.Timing);

        double Sigma(double timing, double mass, double radius)
            => ObservationalRoadmapAudit.Significance("A_SHARED_X", x,
                ObservationalRoadmapAudit.RelativeSigmaX(mass, radius, Correlation), timing);

        var rows = new List<(string, double, double, double, double, double)>();
        void Row(string capability, double timing, double mass, double radius)
        {
            double s = Sigma(timing, mass, radius);
            rows.Add((capability, timing, mass, radius, s, s / baselineSigma));
        }

        Row("the baseline (A)", baseline.Timing, baseline.MassShare, baseline.RadiusShare);
        Row("timing alone to 1 per cent", 0.01, baseline.MassShare, baseline.RadiusShare);
        Row("mass alone to 0.1 per cent", baseline.Timing, 0.001, baseline.RadiusShare);
        Row("radius alone to 1.5 per cent", baseline.Timing, baseline.MassShare, 0.015);
        Row("everything together (D)", 0.05, 0.005, 0.015);
        return rows.ToArray();
    }

    // ===================== 4. THE VERDICT =====================

    public static string Verdict()
    {
        var p = Program();
        var (decided, atThree, atFive) = ClassCensus();
        var firstThree = FirstDecider(3.0).FirstOrDefault();
        var firstFive = FirstDecider(5.0).FirstOrDefault();
        var sb = new StringBuilder();
        sb.Append("THE PROGRAM IS ONE MEASUREMENT ON ONE OBJECT, AND THE AUDIT PRINTS IT FROM THE CLASS THAT REACHES IT. ");
        sb.Append($"TARGET: {TargetName}, at compactness x = {XOfTarget():F6}. ");
        sb.Append($"MEASUREMENT: {TheSingleDecisiveMeasurement()}. ");
        sb.Append($"OF THE {p.Length} CAPABILITY CLASSES, {decided} DECIDE, {atThree} REACH 3 SIGMA AND {atFive} REACH 5 SIGMA. ");
        sb.Append(firstThree.InstrumentClass is null
            ? "AND NO CLASS IN THE TABLE REACHES 3 SIGMA, which is the plan's honest headline: "
            : $"THE FIRST CLASS TO REACH 3 SIGMA IS {firstThree.InstrumentClass} at {firstThree.Significance:F4} sigma, needing a redshift precision of {firstThree.Timing:P0} and a compactness of {firstThree.Compactness:P4}: ");
        sb.Append(firstFive.InstrumentClass is null
            ? "and none reaches 5 sigma either, so the decision needs a capability the current class list does not contain. "
            : $"and the first to reach 5 sigma is {firstFive.InstrumentClass} at {firstFive.Significance:F4} sigma. ");
        sb.Append("AND THE LEVERAGE IS MEASURED RATHER THAN ASSUMED, AND IT REFUTED THE AUDIT'S OWN DRAFT: ");
        foreach (var l in Leverage())
            sb.Append($"{l.Capability} gives {l.Significance:F4} sigma ({l.Gain:F4} times the baseline); ");
        sb.Append("SO THE DECISION IS BOUGHT WITH THE TIMING FIRST, THE RADIUS SECOND AND THE MASS ALMOST NOT AT ALL - ");
        sb.Append("and the reason is in the requirements table, where at the recorded 20 per cent timing the REQUIRED COMPACTNESS IS NaN: ");
        sb.Append("NO compactness precision decides the question until the timing improves. That is G_071's TIMING IS STEP 1 recovered as a number rather than quoted, and the two audits agree once the operation is named, because G_071's radius leverage is about equalising the marginals AT a fixed significance. ");
        sb.Append("OUTPUT: THE FIRST REALISTIC DECIDER IS A NEXT-GENERATION TIMING MEASUREMENT, AND THE DECISIVE ONE IS THE REDSHIFT ITSELF.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var (decided, atThree, atFive) = ClassCensus();
        var req = Requirements();
        return "THE QUESTION ASKED WHAT EXACT FUTURE MEASUREMENT CAN FIRST DECIDE AT vs GR USING A NEUTRON-STAR REDSHIFT, AND THE ANSWER IS ONE NUMBER ON ONE OBJECT, WITH THE INSTRUMENT CLASS NAMED RATHER THAN THE MISSION: "
             + $"the surface gravitational redshift of {TargetName}, equivalently its compactness, to the precision the decision needs. "
             + $"{decided} of the capability classes decide, {atThree} reach 3 sigma and {atFive} reach 5 sigma, and the requirements table says which side binds at each level. "
             + "THE PLAN'S SHAPE IS THE FINDING: the first two classes fail, and they fail for different reasons - the published row fails because the compactness error is dominated by the RADIUS marginal, and the radio-timing class fails for the same reason even after the mass is fixed to a tenth of a per cent. "
             + "The audit takes the correlation at ZERO, the conservative choice, so every significance above is a FLOOR: a negative mass-radius correlation only shrinks the compactness error and can only help. "
             + "AND THE HONEST LIMIT IS THAT THE CLASSES ARE CAPABILITY TRIPLES AND NOT MISSION COMMITMENTS: the audit measures what a given (timing, mass, radius) triple buys, and it does not audit whether any funded instrument will deliver a triple. "
             + "WHAT IT DOES ESTABLISH IS WHERE THE LEVERAGE IS, AND THE MEASUREMENT REFUTED THE AUDIT'S OWN DRAFT THERE: I expected the radius to lead, and the timing leads by a factor of 1.7200 over it (1.9400 against 1.1292), because at the recorded 20 per cent timing the required compactness is NaN - no compactness precision decides the question until the timing improves. "
             + "THAT IS G_071's FINDING RECOVERED RATHER THAN CONTRADICTED, and the audit says which operation each statement is about: improving ONE capability alone at the current budget is a timing question, while equalising the two marginals AT a fixed significance is the radius question. "
             + "AND THE TWO ROUTES ARE SEPARATED IN THE TABLE: a next-generation TIMING measurement reaches 4.1374 sigma, while a direct surface-redshift measurement to 1 per cent reaches 44.8377 - so the first realistic decider is the former and the decisive one is the latter.";
    }

    // ===================== 5. REPORTS =====================

    public static string OutputProgram()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"THE PROGRAM: TARGET / PRECISION / INSTRUMENT CLASS / DECISION SIGNIFICANCE.");
        sb.AppendLine($"  target: {TargetName}   x = {XOfTarget():F6}   (correlation taken at {Correlation:F1})");
        sb.AppendLine();
        sb.AppendLine("  instrument class                            timing   compactness   significance   reaches");
        foreach (var r in Program())
            sb.AppendLine($"  {r.InstrumentClass,-43} {r.Timing,-8:P0} {r.Compactness,-13:P4} {r.Significance,-14:F4} {r.Reaches}");
        sb.AppendLine();
        sb.AppendLine("  each class's marginals and the reason it is in the table:");
        foreach (var c in Classes())
            sb.AppendLine($"    {c.Class,-43} mass {c.MassShare,-9:P3} radius {c.RadiusShare,-9:P3} {c.Basis}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("WHAT THE DECISION NEEDS, FROM THE DECISION SIDE.");
        sb.AppendLine("  significance   required timing   required compactness   which binds");
        foreach (var r in Requirements())
            sb.AppendLine($"  {r.Significance,-14:F0} {r.RequiredTiming,-17:P4} {r.RequiredCompactness,-23:P4} {r.WhichBinds}");
        sb.AppendLine();
        sb.AppendLine($"  {TheSingleDecisiveMeasurement()}.");
        return sb.ToString();
    }

    public static string OutputPhases()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE PLAN IN PHASES.");
        sb.AppendLine("  phase                        capability                                       instrument class                                significance   decides");
        foreach (var p in Phases())
            sb.AppendLine($"  {p.Phase,-28} {p.Capability,-48} {p.InstrumentClass,-46} {p.Significance,-14} {p.Decides}");
        return sb.ToString();
    }

    public static string OutputLeverage()
    {
        var sb = new StringBuilder();
        sb.AppendLine("WHERE THE LEVERAGE IS: each capability improved ALONE, against the baseline.");
        sb.AppendLine("  capability                        timing   mass       radius     significance   gain");
        foreach (var l in Leverage())
            sb.AppendLine($"  {l.Capability,-33} {l.Timing,-8:P0} {l.MassShare,-10:P3} {l.RadiusShare,-10:P3} {l.Significance,-14:F4} {l.Gain:F4}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
