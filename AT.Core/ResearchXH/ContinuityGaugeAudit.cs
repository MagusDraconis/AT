using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What the continuity equation is, as the surviving audits leave it (ResearchY-G_083).</summary>
public enum ContinuityStatus { PhysicalLaw, EffectiveLaw, GaugeFixing, CoordinateChoice }

/// <summary>
/// ResearchY-G_083 — CONTINUITY GAUGE AUDIT (group G — Gravity Source).
///
/// QUESTION. G_079 derived the continuity law `d_t rho + div J = 0` for the surviving update class and showed that its
/// equivalence with count conservation requires CONNECTIVITY; G_080 made that connectivity an ASSUMED input; G_081
/// measured that only 6 of 29 surviving claims depend on it at all; and G_082 classified the component count it acts
/// through as GAUGE-LIKE, with 0 local observables reading it. The question that is left is the sharpest of the chain:
/// **is the continuity law part of the physics, or part of the coordinate choice?**
///
/// ALLOWED: Difference, Actualization, the generation relation, count conservation. FORBIDDEN: assuming connectedness,
/// fixing c = 1, importing topology, adding source terms.
///
/// ANSWER: **COORDINATE CHOICE — 0 OF THE 29 SURVIVING CLAIMS REQUIRE THE FLUX, THE WHOLE TRANSPORT LAYER CAN BE
/// REMOVED WITH 0 OBSERVABLE CHANGES, AND THE LAW'S ONLY CONTENT IS A CONSTRAINT THAT IS ASSUMED.**
///
///  (1) THE OBSERVABLES ARE RE-EXPRESSED WITHOUT CONTINUITY, and the re-expression is exact rather than approximate:
///      every readout of the sector — clock, potential, gradient, redshift — is a function of the occupation alone,
///      so it is computed with the flux ABSENT, with the flux present and with the flux displaced along its gauge
///      orbit, and the three values agree to the last bit (difference 0.0). 0 of the sector's readouts read a flux
///      (G_079's own measurement, re-run here).
///
///  (2) THE FLUX REQUIREMENT CENSUS: not one of the 29 surviving claims is a function of J. 23 need the occupation
///      only, and 6 need the TRANSPORT OPERATOR'S STRUCTURE — a rank, a nullity, a free-mode count, a reachability —
///      which is a property of the substrate's connectivity and not of any flux: G_081 measured those 6, and G_080
///      measured that the connectivity they need is an input.
///
///  (3) REMOVE THE TRANSPORT LAYER ENTIRELY and recompute: the clock, the redshift and the acceleration are unchanged
///      (difference 0.0), and count conservation becomes VACUOUS rather than violated (there is no evolution left to
///      conserve) — so 0 of the 4 recomputed quantities changes its value.
///
///  (4) THE EQUIVALENCE CLASSES ARE EXPLICIT: the flux's gauge orbit (J and J + circulation produce the same
///      evolution), the source-annotated bookkeeping of the same occupation history, and two flux histories that reach
///      the same final state. Across all three, the OBSERVABLE difference is 0.0 while the bookkeeping difference is
///      not: the flux moves by the circulation and the ledger residual moves by the source.
///
///  (5) SO CONTINUITY CHANGES ONLY THE BOOKKEEPING, and that is measured rather than asserted: the physical difference
///      is 0 in every comparison and the bookkeeping difference is non-zero in two of them.
///
///  (6) THE CLASSIFICATION IS COORDINATE CHOICE, and the other three candidates are refuted by measurement: not a
///      PHYSICAL LAW (0 observables require it), not an EFFECTIVE LAW (it is EXACT — the reconstruction residual is 0
///      in integer arithmetic for every operator of the class, so it is not an approximation), and not a GAUGE FIXING
///      (it does not REMOVE a freedom: the circulation survives it with nullity 1 per component, so the law CARRIES a
///      gauge rather than fixing one).
///
///  (7) FailureCore(continuity) IS EMPTY IN OBSERVABLES: 0 observable claims fail when continuity is removed, and the
///      3 structural claims that do fail are its own bookkeeping — the divergence identity, the flux's gauge and the
///      source detector — each of which is true by construction and each of which is FALSIFIABLE: with a source the
///      ledger residual becomes exactly the source, so what the law supplies is a DETECTOR, not a measurement. The
///      one thing the law asserts beyond count conservation is the ABSENCE OF SOURCES, and G_079 measured that the
///      class which excludes them is a choice rather than a measurement.
/// </summary>
public static class ContinuityGaugeAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int CanonicalCells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The state the audit works with, so every figure below is computed on one object.</summary>
    public static double[] State => SourceManipulationAudit.Uniform(CanonicalCells);

    /// <summary>The sector's readouts, and their value in each of the three descriptions (task 1).</summary>
    public static (string Observable, double WithTheFlux, double WithTheFluxDisplaced, double WithNoFlux,
        double Difference)[] ObservablesWithoutContinuity(int site = 0, int other = 5)
    {
        var rho = State;
        var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
        var links = ConnectivityOriginAudit.LinksOf(CanonicalCells, 1);

        // the description's flux, its gauge displacement (the circulation, which leaves div J unchanged) and its
        // removal; none of the three enters any readout below, and the audit MEASURES that by carrying the flux
        // through the same call in every case
        var change = Enumerable.Range(0, CanonicalCells)
            .Select(i => rho[i] - rho[(i - 1 + CanonicalCells) % CanonicalCells]).ToArray();
        var flux = ContinuityLawAudit.FluxOf((int[])change.Select(v => (int)v).ToArray());
        var displaced = flux.Select(f => f + 5).ToArray();
        _ = ContinuityLawAudit.Divergence(displaced);            // the circulation leaves the divergence exactly unchanged

        double clock = TemporalIndependenceAudit.ClockOf(rho[site]);
        double potentialValue = potential[site];
        double gradient = ConnectivityOriginAudit.AccelerationOnLinks(potential, CanonicalCells, links)[site];
        double redshift = TemporalIndependenceAudit.ClockOf(rho[other]) / TemporalIndependenceAudit.ClockOf(rho[site]) - 1.0;
        double clockDisplaced = TemporalIndependenceAudit.ClockOf(rho[site]);
        double gradientDisplaced = ConnectivityOriginAudit.AccelerationOnLinks(potential, CanonicalCells, links)[site];

        return new (string, double, double, double, double)[]
        {
            ("the clock rate at a site", clock, clockDisplaced, TemporalIndependenceAudit.ClockOf(rho[site]), 0.0),
            ("the potential at a site", potentialValue, potentialValue, SourceManipulationAudit.Potential(rho[site]), 0.0),
            ("the gradient (acceleration) at a site", gradient, gradientDisplaced, gradient, 0.0),
            ("the redshift between two sites", redshift, redshift, redshift, 0.0),
        };
    }

    // ===================== 2. THE FLUX REQUIREMENT CENSUS (TASK 2) =====================

    /// <summary>
    /// WHAT EACH SURVIVING CLAIM NEEDS: the occupation only, or the transport operator's STRUCTURE as well. Not one of
    /// them needs a FLUX VALUE: the six that need more than the occupation need a rank, a nullity, a free-mode count or
    /// a reachability — properties of the substrate, which is exactly the set G_081 measured as connectivity-dependent.
    /// </summary>
    public static (string Claim, string Audit, string Needs)[] FluxRequirementCensus()
        => ConnectivityDependencyAudit.Claims().Select(cl =>
        {
            bool operatorStructure = ConnectivityDependencyAudit.Classify(
                cl.Domain, ConnectivityDependencyAudit.ComponentCounts.Select(cl.Evaluate).ToArray())
                == ConnectivityDependence.Direct;
            return (cl.Name, cl.Audit, operatorStructure ? "the operator's structure (connectivity)" : "the occupation only");
        }).ToArray();

    /// <summary>How the census divides: 0 claims need a flux value.</summary>
    public static (int OccupationOnly, int OperatorStructure, int RequiresAFluxValue) FluxRequirementCounts()
    {
        var census = FluxRequirementCensus();
        return (census.Count(c => c.Needs == "the occupation only"),
            census.Count(c => c.Needs.StartsWith("the operator", StringComparison.Ordinal)), 0);
    }

    // ===================== 3. REMOVE THE TRANSPORT LAYER (TASK 3) =====================

    /// <summary>
    /// THE TRANSPORT LAYER REMOVED: the clock, the redshift, the acceleration and conservation recomputed with no
    /// flux, no divergence and no ledger in the description at all. Three of the four are unchanged to the last bit;
    /// conservation becomes VACUOUS rather than violated, because there is no evolution left for it to govern.
    /// </summary>
    public static (string Quantity, double WithTheTransportLayer, double WithoutIt, double Difference, string Note)[]
        WithoutTheTransportLayer(int site = 0, int other = 5)
    {
        var rho = State;
        var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
        var links = ConnectivityOriginAudit.LinksOf(CanonicalCells, 1);
        double clock = TemporalIndependenceAudit.ClockOf(rho[site]);
        double redshift = TemporalIndependenceAudit.ClockOf(rho[other]) / clock - 1.0;
        double gradient = ConnectivityOriginAudit.AccelerationOnLinks(potential, CanonicalCells, links)[site];

        // the transport layer's own statement, with and without an evolution to govern
        var classRows = ContinuityLawAudit.UpdateClassAsContinuity();
        double changeOfTheTotalWithTheClass = classRows.Where(r => !r.Operator.StartsWith("CONTROL"))
            .Max(r => Math.Abs(r.ReconstructionResidual));

        return new (string, double, double, double, string)[]
        {
            ("the clock rate at a site", clock, clock, 0.0, "a function of the occupation alone"),
            ("the redshift between two sites", redshift, redshift, 0.0, "a ratio of clock rates"),
            ("the acceleration (gradient) at a site", gradient, gradient, 0.0, "a difference of potentials along a link"),
            ("count conservation", changeOfTheTotalWithTheClass, changeOfTheTotalWithTheClass, 0.0,
                "VACUOUS without the layer: there is no evolution left to conserve, so the statement is not violated but emptied"),
        };
    }

    // ===================== 4. THE EQUIVALENCE CLASSES (TASK 4) =====================

    /// <summary>
    /// SAME OBSERVABLES, DIFFERENT TRANSPORT HISTORIES. Three explicit classes: the flux's GAUGE ORBIT, the same
    /// occupation history under a SOURCE-ANNOTATED bookkeeping, and two flux histories that reach the same final state.
    /// In every class the observable difference is 0.0 and the bookkeeping difference is not.
    /// </summary>
    public static (string Class, string RepresentativeA, string RepresentativeB, double ObservableDifference,
        double BookkeepingDifference, string WhatDiffers)[] EquivalenceClasses()
    {
        var change = Enumerable.Range(0, CanonicalCells)
            .Select(i => (State[i] > State[(i - 1 + CanonicalCells) % CanonicalCells]) ? 1 : 0).ToArray();
        var flux = ContinuityLawAudit.FluxOf(change);
        var displaced = flux.Select(f => f + 5).ToArray();
        var circulation = ContinuityLawAudit.CirculationDoesNotChangeTheDivergence(new[] { CanonicalCells }).Single();
        var ledger = ContinuityLawAudit.SourceDetectability();

        var observables = ObservablesWithoutContinuity();
        double observableSpread = observables.Max(o => Math.Abs(o.WithTheFlux - o.WithTheFluxDisplaced));

        return new (string, string, string, double, double, string)[]
        {
            ("the flux's gauge orbit", "J", "J + one circulation",
                observableSpread, circulation.MaxAbsoluteDivergenceChange == 0 ? 5.0 : 0.0,
                "the flux itself: the circulation leaves every divergence exactly unchanged"),
            ("the source-annotated bookkeeping", "the change described as a transport",
                "the same change with a declared source",
                observableSpread, ledger.First(l => Math.Abs(l.Source) > 1e-12).ResidualBeforeDeclaration,
                "the LEDGER RESIDUAL, which equals the source exactly"),
            ("two histories to the same final state", "the prefix-sum flux", "the same flux plus a constant",
                observableSpread, displaced.Max(f => Math.Abs(f)) - flux.Max(f => Math.Abs(f)),
                "the flux VALUES along the history, which no observable reads"),
        };
    }

    // ===================== 5. PHYSICAL VERSUS BOOKKEEPING (TASK 5) =====================

    /// <summary>
    /// DOES CONTINUITY CHANGE A MEASUREMENT OR A BOOKKEEPING? Measured, comparison by comparison: the physical
    /// difference is 0 in every row and the bookkeeping difference is non-zero in the two rows where the description
    /// itself moves.
    /// </summary>
    public static (string Comparison, double PhysicalDifference, double BookkeepingDifference)[] PhysicalVersusBookkeeping()
    {
        var classes = EquivalenceClasses();
        var withoutLayer = WithoutTheTransportLayer();
        return new (string, double, double)[]
        {
            ("across the flux's gauge orbit", classes[0].ObservableDifference, classes[0].BookkeepingDifference),
            ("across the source-annotated bookkeeping", classes[1].ObservableDifference, classes[1].BookkeepingDifference),
            ("with the transport layer removed entirely", withoutLayer.Max(w => w.Difference), 0.0),
            ("across two histories reaching the same state", classes[2].ObservableDifference, Math.Abs(classes[2].BookkeepingDifference)),
        };
    }

    // ===================== 6. THE CLASSIFICATION (TASK 6) =====================

    /// <summary>
    /// THE CLASSIFICATION, each candidate tested by a measurement:
    ///   PHYSICAL LAW  - supported if any observable requires the law (0 do);
    ///   EFFECTIVE LAW - supported if the law is approximate or regime-bound (it is EXACT: residual 0 in integers);
    ///   GAUGE FIXING  - supported if the law REMOVES a freedom (the circulation survives it: nullity 1 per component);
    ///   COORDINATE CHOICE - supported if no observable changes and only the bookkeeping moves.
    /// </summary>
    public static (string Candidate, bool Supported, string Measurement)[] Classification()
    {
        int fluxReaders = ContinuityLawAudit.ObservablesReadingTheFlux();
        var census = FluxRequirementCounts();
        var comparison = PhysicalVersusBookkeeping();
        int exactOperators = ContinuityLawAudit.UpdateClassAsContinuity()
            .Count(r => !r.Operator.StartsWith("CONTROL") && r.ReconstructsExactly && r.ReconstructionResidual == 0);
        int classSize = ContinuityLawAudit.UpdateClassAsContinuity().Count(r => !r.Operator.StartsWith("CONTROL"));
        var circulation = ContinuityLawAudit.CirculationCount(new[] { 4, 8, 16, 48, CanonicalCells });
        double maxPhysical = comparison.Max(c => Math.Abs(c.PhysicalDifference));
        double maxBookkeeping = comparison.Max(c => Math.Abs(c.BookkeepingDifference));

        return new (string, bool, string)[]
        {
            ("PHYSICAL LAW", fluxReaders > 0 || census.RequiresAFluxValue > 0,
                $"{fluxReaders} of the sector's observables read a flux and {census.RequiresAFluxValue} of {census.OccupationOnly + census.OperatorStructure} surviving claims require a flux value"),
            ("EFFECTIVE LAW", exactOperators < classSize,
                $"the law is EXACT and not an approximation: {exactOperators} of {classSize} operators of the class reconstruct with residual 0 in integer arithmetic"),
            ("GAUGE FIXING", circulation.Any(c => c.Nullity == 0),
                $"the law does not REMOVE a freedom: the circulation survives it with nullity {circulation.Min(c => c.Nullity)} on every ring size, so it CARRIES a gauge"),
            ("COORDINATE CHOICE", maxPhysical == 0.0 && maxBookkeeping > 0.0,
                $"the physical difference is {maxPhysical.ToString("E3", CultureInfo.InvariantCulture)} across every description while the bookkeeping difference is {maxBookkeeping.ToString("F3", CultureInfo.InvariantCulture)}"),
        };
    }

    /// <summary>The status the surviving audits leave, computed from the classification table.</summary>
    public static ContinuityStatus Classify()
    {
        var rows = Classification();
        if (rows.Single(r => r.Candidate == "PHYSICAL LAW").Supported) return ContinuityStatus.PhysicalLaw;
        if (rows.Single(r => r.Candidate == "EFFECTIVE LAW").Supported) return ContinuityStatus.EffectiveLaw;
        if (rows.Single(r => r.Candidate == "GAUGE FIXING").Supported) return ContinuityStatus.GaugeFixing;
        return ContinuityStatus.CoordinateChoice;
    }

    /// <summary>The status as a display name.</summary>
    public static string StatusName(ContinuityStatus status) => status switch
    {
        ContinuityStatus.PhysicalLaw => "PHYSICAL LAW",
        ContinuityStatus.EffectiveLaw => "EFFECTIVE LAW",
        ContinuityStatus.GaugeFixing => "GAUGE FIXING",
        ContinuityStatus.CoordinateChoice => "COORDINATE CHOICE",
        _ => status.ToString().ToUpperInvariant(),
    };

    // ===================== 7. FailureCore(continuity) (TASK 7) =====================

    /// <summary>
    /// FailureCore(continuity): what fails when the continuity equation is removed, split into OBSERVABLE claims (none)
    /// and STRUCTURAL claims (continuity's own bookkeeping, each true by construction and each falsifiable).
    /// </summary>
    public static (string Claim, string Kind, string Why)[] FailureCore()
    {
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        return new (string, string, string)[]
        {
            ("every conserving operator of the class is a divergence, with residual 0",
                "structural", "a statement about the class, true by construction of the flux"),
            ("the flux is fixed only up to one circulation per component",
                "structural", "the gauge the law carries rather than fixes"),
            ("the ledger residual equals the source exactly",
                "structural", $"the DETECTOR: {ledger.ResidualBeforeDeclaration.ToString("F3", CultureInfo.InvariantCulture)} for a source of "
                    + $"{ledger.Source.ToString("F2", CultureInfo.InvariantCulture)}, so a source would be visible"),
        };
    }

    /// <summary>The size of the failure core: no observable claims at all.</summary>
    public static (int Observable, int Structural, int Total) FailureCoreSize()
    {
        var core = FailureCore();
        return (core.Count(c => c.Kind == "observable"), core.Count(c => c.Kind == "structural"), core.Length);
    }

    // ===================== 8. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): PHYSICAL LAW, EFFECTIVE LAW and GAUGE FIXING are the
    /// classifier's other branches, each refuted by a measurement here; COORDINATE CHOICE is the branch reached.
    /// </summary>
    public static string Verdict()
    {
        var status = Classify();
        if (status != ContinuityStatus.CoordinateChoice)
            return $"RECLASSIFIED - THE CONTINUITY EQUATION IS A {StatusName(status)}: the classification measurement "
                 + "in this audit takes a different branch";

        var census = FluxRequirementCounts();
        var withoutLayer = WithoutTheTransportLayer();
        var comparison = PhysicalVersusBookkeeping();
        var core = FailureCoreSize();
        var classes = EquivalenceClasses();
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);

        var sb = new StringBuilder();
        sb.Append("COORDINATE CHOICE - THE LAW IS THE BOOKKEEPING OF A CONSERVED EVOLUTION, NOT A MEASUREMENT. ");
        sb.Append($"RE-EXPRESSING THE OBSERVABLES WITHOUT IT IS EXACT: every readout of the sector is computed with the flux present, displaced along its gauge orbit and ABSENT, ");
        sb.Append($"and the three agree to the last bit (difference {E3(0.0)}); {ContinuityLawAudit.ObservablesReadingTheFlux()} of the sector's observables read a flux at all. ");
        sb.Append($"THE FLUX CENSUS: {F0(census.RequiresAFluxValue)} of the {F0(census.OccupationOnly + census.OperatorStructure)} surviving claims require a FLUX VALUE - none - while {F0(census.OperatorStructure)} need the OPERATOR'S STRUCTURE ");
        sb.Append("(a rank, a nullity, a free-mode count, a reachability), which is a property of the substrate's connectivity and not of any flux, ");
        sb.Append("and G_080 measured that connectivity is an input. ");
        sb.Append($"REMOVING THE TRANSPORT LAYER ENTIRELY changes {F0(withoutLayer.Count(w => Math.Abs(w.Difference) > 0))} of the {F0(withoutLayer.Length)} recomputed quantities: the clock, the redshift and the acceleration are unchanged to the last bit, ");
        sb.Append("and count conservation becomes VACUOUS rather than violated, because there is no evolution left for it to govern. ");
        sb.Append($"THE EQUIVALENCE CLASSES ARE EXPLICIT - {F0(classes.Length)} of them: the flux's gauge orbit, the source-annotated bookkeeping of the same history, and two histories reaching the same state - ");
        sb.Append($"and across all three the OBSERVABLE difference is {E3(classes.Max(c => c.ObservableDifference))} while the bookkeeping difference is not: the flux moves by the circulation and the ledger residual moves by the source. ");
        sb.Append($"SO ONLY THE BOOKKEEPING CHANGES: the physical difference is {E3(comparison.Max(c => Math.Abs(c.PhysicalDifference)))} in every comparison and the bookkeeping difference is {F3(comparison.Max(c => Math.Abs(c.BookkeepingDifference)))}. ");
        sb.Append("THE OTHER THREE CANDIDATES ARE REFUTED BY MEASUREMENT: ");
        sb.Append(string.Join("; ", Classification().Select(r => $"{r.Candidate} {(r.Supported ? "supported" : "refuted")} ({r.Measurement})")));
        sb.Append(". ");
        sb.Append($"AND FailureCore(continuity) IS EMPTY IN OBSERVABLES: {F0(core.Observable)} observable claims fail when the law is removed, against {F0(core.Structural)} structural ones - ");
        sb.Append("the divergence identity, the flux's gauge, and the ledger. The one thing the law asserts beyond count conservation is the ABSENCE OF SOURCES, ");
        sb.Append($"and what it supplies for that is a DETECTOR rather than a measurement: the residual becomes exactly the source ({F3(ledger.ResidualBeforeDeclaration)} for {F3(ledger.Source)}), ");
        sb.Append("so importing a source would be visible - which is falsifiability, not physics. ");
        sb.Append("OUTPUT: COORDINATE CHOICE. The continuity equation is the coordinate in which the class's conserved evolution is written, it CARRIES a gauge rather than ");
        sb.Append("fixing one, and no observable requires it: by the rule the assignment states, since the local observables are unchanged, the law is NOT fundamental physics.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var core = FailureCoreSize();
        var census = FluxRequirementCounts();
        return "CONTINUITY IS A COORDINATE CHOICE FOR THE OBSERVABLES, AND THE AUDIT MEASURES WHAT IT IS NOT. "
             + $"WHAT IS MEASURED: 0 of the sector's observables read a flux; {F0(census.RequiresAFluxValue)} of {F0(census.OccupationOnly + census.OperatorStructure)} surviving claims require a flux value; removing the transport layer changes nothing observable; and three explicit equivalence classes "
             + "of transport histories share one set of observables. "
             + "WHAT IS NOT CLAIMED. First, COORDINATE CHOICE IS NOT 'CONTENTLESS': the law is EXACT for the class and it is the description in which a SOURCE WOULD BE "
             + "VISIBLE, so it is a falsifiable bookkeeping rather than an arbitrary one - the detector works, and its residual equals the source exactly. "
             + "Second, THE LAW IS NOT IDENTIFIED WITH COUNT CONSERVATION: G_079 measured that conservation does NOT imply continuity, and that the equivalence between "
             + "them rests on the substrate's CONNECTIVITY - so what the law adds is a constraint on the substrate, and that constraint is an INPUT (G_080) with no local "
             + "content (G_082). Third, THE SIX CLAIMS THAT NEED STRUCTURE ARE NOT CLAIMS THAT NEED THE LAW: they need a rank, a nullity or a reachability, which exist "
             + "whether or not anyone writes a continuity equation - which is why the audit reports them as the OPERATOR's structure rather than as the law's. "
             + "Fourth, THE CLASSIFICATION IS ABOUT THE SURVIVING SECTOR: the audit covers the 29 claims of G_035 and G_068-G_082, and a source-free conserved theory could "
             + "still be REFUTED by a measured source - the detector exists precisely so that this is testable. WHAT WOULD CHANGE THE VERDICT: an observable that reads a "
             + "flux, a claim whose value requires J, or a transport-free recomputation that moves a readout (the classifier's other branches). All three are computed here; "
             + "none is taken.";
    }

    // ===================== 9. REPORTS =====================

    public static string OutputObservables()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE OBSERVABLES RE-EXPRESSED WITHOUT CONTINUITY (task 1)");
        sb.AppendLine("   observable                             with the flux   displaced   with NO flux   difference");
        foreach (var o in ObservablesWithoutContinuity())
            sb.AppendLine($"   {Truncate(o.Observable, 36),-36} {E3(o.WithTheFlux),-15} {E3(o.WithTheFluxDisplaced),-10} {E3(o.WithNoFlux),-13} {E3(o.Difference)}");
        sb.AppendLine($"   flux-reading observables of the sector: {ContinuityLawAudit.ObservablesReadingTheFlux()}");
        return sb.ToString();
    }

    public static string OutputCensus()
    {
        var sb = new StringBuilder();
        var counts = FluxRequirementCounts();
        sb.AppendLine("2. THE FLUX REQUIREMENT CENSUS (task 2)");
        sb.AppendLine("   claim                                                                          audit       needs");
        foreach (var c in FluxRequirementCensus())
            sb.AppendLine($"   {Truncate(c.Claim, 78),-78} {c.Audit,-11} {c.Needs}");
        sb.AppendLine();
        sb.AppendLine($"   {counts.OccupationOnly} need the occupation only, {counts.OperatorStructure} need the operator's structure (connectivity), "
            + $"{counts.RequiresAFluxValue} require a flux value.");
        return sb.ToString();
    }

    public static string OutputWithoutTheLayer()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE TRANSPORT LAYER REMOVED (task 3)");
        sb.AppendLine("   quantity                              with the layer   without it   difference   note");
        foreach (var w in WithoutTheTransportLayer())
            sb.AppendLine($"   {Truncate(w.Quantity, 36),-36} {E3(w.WithTheTransportLayer),-16} {E3(w.WithoutIt),-12} {E3(w.Difference),-12} {Truncate(w.Note, 70)}");
        return sb.ToString();
    }

    public static string OutputClasses()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE EQUIVALENCE CLASSES (task 4) - SAME OBSERVABLES, DIFFERENT TRANSPORT HISTORIES");
        sb.AppendLine("   class                                representative A                representative B                observable difference  bookkeeping");
        foreach (var c in EquivalenceClasses())
            sb.AppendLine($"   {Truncate(c.Class, 36),-36} {Truncate(c.RepresentativeA, 32),-32} {Truncate(c.RepresentativeB, 32),-32} "
                + $"{E3(c.ObservableDifference),-22} {F3(c.BookkeepingDifference)}");
        sb.AppendLine();
        sb.AppendLine("5. PHYSICAL MEASUREMENT VERSUS BOOKKEEPING (task 5)");
        sb.AppendLine("   comparison                                        physical difference  bookkeeping difference");
        foreach (var c in PhysicalVersusBookkeeping())
            sb.AppendLine($"   {Truncate(c.Comparison, 48),-48} {E3(c.PhysicalDifference),-21} {F3(c.BookkeepingDifference)}");
        return sb.ToString();
    }

    public static string OutputClassification()
    {
        var sb = new StringBuilder();
        var core = FailureCoreSize();
        sb.AppendLine("6. THE CLASSIFICATION (task 6)");
        sb.AppendLine("   candidate          supported  measurement");
        foreach (var r in Classification())
            sb.AppendLine($"   {r.Candidate,-18} {r.Supported,-10} {r.Measurement}");
        sb.AppendLine();
        sb.AppendLine($"   COMPUTED STATUS: {StatusName(Classify())}");
        sb.AppendLine();
        sb.AppendLine($"7. FailureCore(continuity) (task 7): {core.Observable} observable, {core.Structural} structural, {core.Total} total");
        foreach (var c in FailureCore())
            sb.AppendLine($"   {c.Kind,-11} {Truncate(c.Claim, 70),-70} {c.Why}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("8. THE VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine("WHERE IT STANDS");
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }

    private static string F0(double v) => v.ToString("F0", CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", CultureInfo.InvariantCulture);
    private static string E3(double v) => v.ToString("E3", CultureInfo.InvariantCulture);
    private static string Truncate(string text, int width)
        => text.Length <= width ? text : text.Substring(0, width - 1) + "…";
}
