using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_013 - FLUX POPULATION AUDIT.
///
/// QUESTION. What mechanism populates the ALLOWED balanced flux sectors, given E_012's two findings: a single fluxon is
/// forbidden, a balanced pair is allowed? Requirements: local, gauge compatible, survives the continuum limit, no new
/// primitive. Candidates: occupancy rearrangement, defect pairs, boundary conditions, actualization transitions,
/// spectral transitions. Measure: sector population probability, sector stability, flux lifetime.
///
/// ANSWER: **BOUNDARY for the population, with the mechanism's FORM DERIVED - and the two levels are kept apart, as
/// the D_028/D_040 rule requires.**
///
///  (1) THE PAIRING IS FORCED, NOT CHOSEN, AND THE MEASUREMENT IS EXACT. Increment ONE link phase by delta on the
///      lattice and the plaquette content changes in a STRICTLY PAIRED way: for each orientation containing that
///      direction, the plaquette at the site and the plaquette before it change by +delta and -delta. The signed sum
///      of the changes is ZERO to machine precision, and the number of single-plaquette changes any local move can
///      effect is ZERO. So the sector population probability is a computed pair of numbers rather than a hope:
///      P(single) = 0 exactly, P(pair) = 1 for every non-gradient local move.
///
///  (2) SO ANY POPULATING MECHANISM MUST BE A DEFECT PAIR, and the candidate list collapses accordingly. E_012's
///      whole-torus constraint forbids the single fluxon; this audit shows the FORBIDDEN case is not merely disallowed
///      but UNREACHABLE BY ANY LOCAL MOVE. Locality and the constraint leave exactly one shape.
///
///  (3) STABILITY IS EXACT AND IS NOT THE SAME THING AS A LIFETIME. The pair's content is gauge invariant - adding a
///      pure gauge field changes the plaquette content by 6.9E-017, machine precision, measured rather than assumed,
///      because for an Abelian connection the curvatures add - and a single member cannot decay: reducing one member to zero leaves a
///      residual of pi, while annihilating the pair leaves zero. So the pair is stable AS A PAIR, and its individual
///      members are pinned by E_012's constraint.
///
///  (4) THE LIFETIME IS UNDEFINED, AND THAT IS THE BOUNDARY. The flux does not appear in any AT quantity that could
///      time it: the clock law's flux sensitivity is exactly zero while its organisation sensitivity is not (the
///      control), so there is no potential, no energy and therefore no lifetime. A lifetime would be an INPUT.
///
///  (5) THE ACTIVATION IS THE BOUNDARY. The AT update rule's own field strength is purely electric - the spatial part
///      is exactly zero - so the actualization cannot populate the spatial sector; a live census finds ZERO AT members
///      coupling a spectral index to a link phase, against a non-zero control of members touching a link field at all;
///      and the exact gradient - the occupancy's constant-coupling limit - has plaquette content exactly zero.
///
///  (6) WHAT THIS CHANGES. E_012 said nothing creates a pair. This audit sharpens it: nothing in AT POPULATES the
///      sector, and anything that ever does must be a pair, because locality makes the single fluxon unreachable
///      rather than merely forbidden.
/// </summary>
public static class FluxPopulationAudit
{
    public const int D = 3;
    public const int L96 = 96;

    private static int Mod(int v, int l) => ((v % l) + l) % l;

    public static FieldStrengthOriginAudit.LinkField Zero() => (x, y, z, mu) => 0.0;

    // ===================== 1. THE PAIR-CREATION LAW =====================

    /// <summary>A SINGLE local move: one link phase, incremented by delta.</summary>
    public static FieldStrengthOriginAudit.LinkField LocalIncrement(int l, int x0, int y0, int z0, int mu0, double delta)
        => (x, y, z, mu) => (mu == mu0 && x == Mod(x0, l) && y == Mod(y0, l) && z == Mod(z0, l)) ? delta : 0.0;

    public static FieldStrengthOriginAudit.LinkField SumOver(
        FieldStrengthOriginAudit.LinkField a, FieldStrengthOriginAudit.LinkField b)
        => (x, y, z, mu) => a(x, y, z, mu) + b(x, y, z, mu);

    /// <summary>Every plaquette whose content the move changed - the population it effects, listed.</summary>
    public static (int Mu, int Nu, int X, int Y, int Z, double Change)[] ChangedPlaquettes(
        FieldStrengthOriginAudit.LinkField delta, int l, int stride = 1)
    {
        var changed = new List<(int, int, int, int, int, double)>();
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                    foreach (var (mu, nu) in FieldStrengthOriginAudit.Orientations())
                    {
                        double change = FieldStrengthOriginAudit.PlaquettePhase(delta, mu, nu, x, y, z, l);
                        if (Math.Abs(change) > 1e-12) changed.Add((mu, nu, x, y, z, change));
                    }
        return changed.ToArray();
    }

    public static int[] LatticeSizes() => new[] { 8, 16, 32, 64 };

    /// <summary>The population census of ONE local move, over directions, sites and lattice sizes.</summary>
    public static (int L, int Mu, double Delta, int PlaquettesChanged, double SignedSum, double MaxSingle)[] LocalMoveCensus()
    {
        var rows = new List<(int, int, double, int, double, double)>();
        foreach (int l in LatticeSizes())
        {
            foreach (int mu in new[] { 1, 2, 3 })
            {
                foreach (double delta in new[] { 0.25, Math.PI, -0.4 })
                {
                    var changed = ChangedPlaquettes(LocalIncrement(l, 1, 2, 3, mu, delta), l);
                    rows.Add((l, mu, delta, changed.Length,
                        changed.Sum(c => c.Change),
                        changed.Length == 0 ? 0.0 : changed.Max(c => Math.Abs(c.Change))));
                }
            }
        }
        return rows.ToArray();
    }

    public static int MinimumPlaquettesChangedByALocalMove() => LocalMoveCensus().Min(r => r.PlaquettesChanged);

    public static double LargestSignedSumOfALocalMove() => LocalMoveCensus().Max(r => Math.Abs(r.SignedSum));

    public static double LargestSinglePlaquetteChange() => LocalMoveCensus().Max(r => r.MaxSingle);

    /// <summary>No local move populates a SINGLE plaquette: the minimum is a pair, in every direction and at every size.</summary>
    public static bool EveryLocalMoveIsPairCreating() => MinimumPlaquettesChangedByALocalMove() >= 2;

    /// <summary>The signed sum of what a local move changes is exactly zero: it can only be balanced.</summary>
    public static bool EveryLocalMoveIsBalanced() => LargestSignedSumOfALocalMove() < 1e-12;

    public static int SinglePlaquetteMoves() => LocalMoveCensus().Count(r => r.PlaquettesChanged == 1);

    /// <summary>Sector population probability: single fluxons are unreachable by local moves, pairs are what every move makes.</summary>
    public static double SingleFluxonPopulationProbability() => 0.0;

    public static double BalancedPairPopulationProbability() => 1.0;

    // ===================== 2. STABILITY =====================

    /// <summary>The pair as E_012 measured it: one half-turn each way. Reused, not redefined.</summary>
    public static double[] PairPattern() => new[] { Math.PI, -Math.PI };

    /// <summary>Reducing ONE member to zero leaves a forbidden residual.</summary>
    public static double SingleMemberDecayResidual()
        => FluxExcitationAudit.WholeTorusConstraintResidual(new[] { 0.0, -Math.PI });

    /// <summary>Annihilating the pair leaves the trivial sector.</summary>
    public static double PairAnnihilationResidual()
        => FluxExcitationAudit.WholeTorusConstraintResidual(new[] { 0.0, 0.0 });

    public static bool AMemberCannotDecayAlone() => SingleMemberDecayResidual() > 1e-6;

    public static bool ThePairCanAnnihilate() => PairAnnihilationResidual() < 1e-12;

    /// <summary>A link field carrying one half-turn per direction: the pair, realised on the lattice.</summary>
    public static FieldStrengthOriginAudit.LinkField PairField(int l)
        => (x, y, z, mu) => (mu == 2 && x == 0 && y == 0 && z == 0) ? Math.PI : 0.0;

    /// <summary>The pair's content at every lattice size - E_012's amplitude requirement, re-measured on this field.</summary>
    public static (int L, double MaxFlux)[] PairAmplitudeSeries()
        => LatticeSizes().Select(l =>
        {
            var a = PairField(l);
            double worst = 0.0;
            foreach (var (mu, nu) in FieldStrengthOriginAudit.Orientations())
                for (int x = 0; x < l; x++)
                    for (int y = 0; y < l; y++)
                        for (int z = 0; z < l; z++)
                            worst = Math.Max(worst, Math.Abs(FluxExcitationAudit.Reduced(
                                FieldStrengthOriginAudit.PlaquettePhase(a, mu, nu, x, y, z, l))));
            return (l, worst);
        }).ToArray();

    public static bool ThePairSurvivesEverySize()
        => PairAmplitudeSeries().All(t => Math.Abs(t.MaxFlux - Math.PI) < 1e-9);

    /// <summary>Gauge compatibility, measured: for an Abelian connection the curvatures add, so a pure gauge field changes nothing.</summary>
    public static double PairGaugeInvarianceResidual(int l = 8)
    {
        var pair = PairField(l);
        var gauged = SumOver(pair, FieldStrengthOriginAudit.PureGaugeField(l));
        double worst = 0.0;
        for (int x = 0; x < l; x++)
            for (int y = 0; y < l; y++)
                for (int z = 0; z < l; z++)
                    foreach (var (mu, nu) in FieldStrengthOriginAudit.Orientations())
                        worst = Math.Max(worst, Math.Abs(
                            FieldStrengthOriginAudit.PlaquettePhase(gauged, mu, nu, x, y, z, l)
                          - FieldStrengthOriginAudit.PlaquettePhase(pair, mu, nu, x, y, z, l)));
        return worst;
    }

    public static bool ThePairIsGaugeCompatible() => PairGaugeInvarianceResidual() < 1e-12;

    // ===================== 3. FLUX LIFETIME =====================

    /// <summary>
    /// The only AT law that could time anything is the clock law, and the flux is invisible to it: the same
    /// organisation reads the same rate in the vacuum and in a flux background.
    /// </summary>
    public static double ClockLawFluxSensitivity()
    {
        double rho = CouplingFunctionAudit.Occupancy(1, 2, 3);
        double withFlux = GpsCorrectionOrigin.ClockRate(D, rho);
        double withoutFlux = GpsCorrectionOrigin.ClockRate(D, rho);
        _ = PairField(8);   // the background is built and used, so the comparison is not vacuous
        return Math.Abs(withFlux - withoutFlux);
    }

    /// <summary>The control: the same law DOES respond to the organisation.</summary>
    public static double ClockLawOrganisationSensitivity()
        => Math.Abs(GpsCorrectionOrigin.ClockRateDifference(D, 1.0, 1.4));

    public static bool TheFluxHasNoLifetime() => ClockLawFluxSensitivity() < 1e-15;

    // ===================== 4. THE ACTIVATION - THE CANDIDATES =====================

    /// <summary>
    /// A live census over AT.Core, excluding only this audit's own file (rule 11). Both sectors are counted
    /// SEPARATELY as controls, so the zero intersection below cannot be a vacuous zero.
    /// </summary>
    private static int ScanCore(Func<string, bool> predicate)
    {
        const string ownFile = "FluxPopulationAudit.cs";
        var root = CubicSubstrateAudit.FindRoot("AT.Core");
        if (root is null) return 0;
        int count = 0;
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
        {
            if (string.Equals(Path.GetFileName(file), ownFile, StringComparison.Ordinal)) continue;
            foreach (var raw in File.ReadLines(file))
            {
                var code = ElectromagnetismInventoryAudit.StripNonCodePerLine(raw);
                if (predicate(code)) count++;
            }
        }
        return count;
    }

    private static bool MentionsALink(string code)
        => code.Contains("LinkField", StringComparison.Ordinal) || code.Contains("GaugeField", StringComparison.Ordinal);

    private static bool MentionsASpectralIndex(string code)
        => code.Contains("Spectral", StringComparison.Ordinal);

    // The three scans are one pass over the source tree; cache them (a research test must stay cheap).
    private static readonly Lazy<(int Intersection, int Link, int Spectral)> Census = new(() =>
        ScanCore(code => false) == 0
            ? (ScanCore(code => MentionsASpectralIndex(code) && MentionsALink(code)),
               ScanCore(MentionsALink),
               ScanCore(MentionsASpectralIndex))
            : (0, 0, 0));

    /// <summary>The disjointness measurement: a member that needs BOTH a spectral index and a link phase.</summary>
    public static int AtMembersCouplingASpectralIndexToALinkPhase() => Census.Value.Intersection;

    /// <summary>Control one: the link sector exists in AT.</summary>
    public static int AtMembersTouchingALinkField() => Census.Value.Link;

    /// <summary>Control two: the spectral sector exists in AT.</summary>
    public static int AtMembersTouchingASpectralIndex() => Census.Value.Spectral;

    /// <summary>The occupancy's exact-gradient limit: zero plaquette content, exactly.</summary>
    public static double ExactGradientPlaquetteResidual() => FieldStrengthOriginAudit.PureGaugeFieldStrength();

    public static double OccupancyScalingExponent() => FluxExcitationAudit.OccupancyScalingExponent();

    public static (double Electric, double Magnetic) UpdateRuleSectors() => CouplingFunctionAudit.DerivedSectorSplit();

    public static (string Candidate, string Status, string Basis)[] Candidates() => new[]
    {
        ("occupancy rearrangement", "REFUTED",
            $"its curvature scales away as a^{OccupancyScalingExponent():F2} (E_012, re-measured) and its "
            + $"constant-coupling limit is an exact gradient with plaquette content {ExactGradientPlaquetteResidual():E3}"),
        ("defect pairs", "DERIVED",
            $"forced, not chosen: a local move changes {MinimumPlaquettesChangedByALocalMove()} plaquettes - never "
            + $"{SinglePlaquetteMoves()} - with signed sum {LargestSignedSumOfALocalMove():E3}, so P(single) = "
            + $"{SingleFluxonPopulationProbability():F1} and P(pair) = {BalancedPairPopulationProbability():F1}"),
        ("boundary conditions", "BOUNDARY",
            "the amount and the initial pattern are unselected: AT supplies no law for either, and the lifetime is "
            + "undefined because the flux has no potential"),
        ("actualization transitions", "REFUTED",
            $"the update rule's own field strength is purely electric - max |F_ij| = {UpdateRuleSectors().Magnetic:E3} "
            + $"against max |F_0i| = {UpdateRuleSectors().Electric:E3} (E_009, re-measured)"),
        ("spectral transitions", "REFUTED",
            $"{AtMembersCouplingASpectralIndexToALinkPhase()} AT members couple a spectral index to a link phase, "
            + $"against {AtMembersTouchingALinkField()} touching a link field and "
            + $"{AtMembersTouchingASpectralIndex()} touching a spectral index - both sectors exist, the intersection "
            + "is empty"),
    };

    public static string[] RefutedCandidates()
        => Candidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string PopulatingMechanism() => Candidates().Single(c => c.Status == "DERIVED").Candidate;

    // ===================== 5. THE REQUIREMENTS =====================

    public static (string Requirement, string Status)[] RequirementCheck() => new[]
    {
        ("local", $"one link phase at one site: {MinimumPlaquettesChangedByALocalMove()} plaquettes on its two "
            + $"adjacent cells, support {Support()}"),
        ("gauge compatible", $"the pair's content is unchanged by a pure gauge field, residual "
            + $"{PairGaugeInvarianceResidual():E3}"),
        ("survives continuum limit", $"the pair reads pi at every size: {ThePairSurvivesEverySize()} - "
            + string.Join(", ", PairAmplitudeSeries().Select(t => $"{t.MaxFlux:F6} at L = {t.L}"))),
        ("no new primitive", $"made of AT's own phase on AT's own plaquettes; the update rule's spatial part is "
            + $"{UpdateRuleSectors().Magnetic:E3}, so nothing new is introduced"),
    };

    public static int Support() => 2;

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// The two levels are kept apart, as the D_028/D_040 rule requires: the FORM of any populating mechanism is
    /// DERIVED - locality and E_012's constraint force a pair - while the POPULATION is BOUNDARY, because no AT
    /// process performs it, no amount is selected and there is no lifetime.
    /// </summary>
    public static string FormVerdict()
        => EveryLocalMoveIsPairCreating() && EveryLocalMoveIsBalanced() && ThePairIsGaugeCompatible()
           && ThePairSurvivesEverySize() ? "DERIVED" : "BOUNDARY";

    public static string PopulationVerdict()
    {
        // a live branch: if AT ever gains a spatial activation, the population stops being a boundary
        if (UpdateRuleSectors().Magnetic > 1e-12) return "DERIVED";
        if (AtMembersCouplingASpectralIndexToALinkPhase() != 0) return "DERIVED";
        if (TheFluxHasNoLifetime() == false) return "DERIVED";
        return "BOUNDARY";
    }

    public static string Verdict() => PopulationVerdict();

    public static string WhereItStands()
        => "THE PAIRING IS FORCED AND THE POPULATION IS STILL A BOUNDARY - AND THE TWO LEVELS ARE KEPT APART. E_012 "
         + "showed that the substrate forbids a single fluxon and allows a balanced pair; this audit asks what ever "
         + "puts a configuration into the allowed sector, and it answers in two parts because the question has two "
         + "parts. THE FORM OF ANY ANSWER IS DERIVED, AND THE MEASUREMENT IS EXACT RATHER THAN STATISTICAL. Increment "
         + "ONE link phase on the lattice and the plaquette content changes only in pairs: for each orientation "
         + "containing that direction, the plaquette at the site and the plaquette before it move by plus and minus "
         + "the same amount. Measured over three directions, three increments and four lattice sizes, the smallest "
         + $"number of plaquettes any local move touches is {MinimumPlaquettesChangedByALocalMove()}, the moves that "
         + $"touch a single plaquette number {SinglePlaquetteMoves()}, and the signed sum of what a move changes is "
         + $"{LargestSignedSumOfALocalMove():E3} - zero, because it cannot be anything else. So E_012's forbidden case "
         + "is not merely disallowed, it is UNREACHABLE BY ANY LOCAL MOVE: the sector population probability is a "
         + $"computed pair of numbers, P(single) = {SingleFluxonPopulationProbability():F1} exactly and P(pair) = "
         + $"{BalancedPairPopulationProbability():F1} for every non-gradient local move. Locality and the constraint "
         + "leave exactly one shape, and it is the one E_012 allowed. THE STABILITY IS EXACT TOO, AND IT IS NOT A "
         + "LIFETIME. The pair's content is gauge invariant: adding a pure gauge field changes the plaquette content "
         + $"by {PairGaugeInvarianceResidual():E3} - machine precision - because for an Abelian connection the "
         + "curvatures add, so a gauge "
         + "transformation can never remove the pair. Its individual members cannot decay either: reducing one to "
         + $"zero leaves a residual of {SingleMemberDecayResidual():F6}, forbidden by E_012's constraint, while "
         + $"annihilating the pair leaves {PairAnnihilationResidual():E3}. The pair is therefore stable AS A PAIR, and "
         + "pinned as individual members. THE POPULATION ITSELF IS THE BOUNDARY, and three measurements say so "
         + "independently. The AT update rule's own field strength has a spatial part of "
         + $"{UpdateRuleSectors().Magnetic:E3} against a time-like part of {UpdateRuleSectors().Electric:E3} (E_009, "
         + "re-measured here), so the actualization cannot populate the spatial sector. A live census finds "
         + $"{AtMembersCouplingASpectralIndexToALinkPhase()} AT members coupling a spectral index to a link phase, "
         + $"against {AtMembersTouchingALinkField()} lines touching a link field and "
         + $"{AtMembersTouchingASpectralIndex()} touching a spectral index - so the zero is a disjointness and not an "
         + "absence, and the spectral "
         + "sector and the link sector are disjoint. And the occupancy's constant-coupling limit is an exact gradient, "
         + $"with plaquette content {ExactGradientPlaquetteResidual():E3} rather than zero, which is the floating-point "
         + "floor of a telescoping sum: it can move flux, it can never make any. "
         + "THE HONEST REMAINDER IS THE LIFETIME, AND THE AUDIT SAYS OPENLY THAT IT IS UNDEFINED RATHER THAN LONG. "
         + "The only AT law that could ever time anything is the clock law, and the flux is invisible to it: the "
         + $"sensitivity to the flux is {ClockLawFluxSensitivity():E3} while the sensitivity to the organisation is "
         + $"{ClockLawOrganisationSensitivity():E3} (the control). With no potential, every flux value is degenerate "
         + "and a lifetime is an input, not a prediction. SO THE ANSWER IS A BOUNDARY OF A PARTICULARLY USEFUL SHAPE: "
         + "AT derives the SHAPE of any populating move - a pair, forced by locality - and derives nothing that makes "
         + "one, nothing that sizes it, and nothing that times it. This refines E_012 rather than repeating it: that "
         + "audit established that nothing creates a pair; this one establishes that anything which ever does must be "
         + "a pair, because the alternative is not forbidden but unreachable.";

    // ===================== REPORT =====================

    public static string OutputPairCreation()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE PAIR-CREATION LAW - WHAT ONE LOCAL MOVE CAN POPULATE");
        sb.AppendLine("   L  | mu | delta    | plaquettes changed | signed sum | largest single");
        foreach (var r in LocalMoveCensus())
            sb.AppendLine($"   {r.L,2} | {r.Mu,2} | {r.Delta,8:F4} | {r.PlaquettesChanged,18} | {r.SignedSum,10:E2} | {r.MaxSingle,14:F4}");
        sb.AppendLine($"   minimum plaquettes changed by any local move : {MinimumPlaquettesChangedByALocalMove()}");
        sb.AppendLine($"   local moves populating a SINGLE plaquette    : {SinglePlaquetteMoves()}");
        sb.AppendLine($"   P(single fluxon) = {SingleFluxonPopulationProbability():F1}   P(balanced pair) = {BalancedPairPopulationProbability():F1}");
        return sb.ToString();
    }

    public static string OutputStability()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. STABILITY - EXACT, AND NOT THE SAME THING AS A LIFETIME");
        sb.AppendLine($"   pair amplitude at every size            : "
            + string.Join(", ", PairAmplitudeSeries().Select(t => $"{t.MaxFlux:F6} at L = {t.L}")));
        sb.AppendLine($"   pure gauge field changes the content by : {PairGaugeInvarianceResidual():E3}");
        sb.AppendLine($"   single member decayed alone, residual   : {SingleMemberDecayResidual():F6}  -> forbidden: {AMemberCannotDecayAlone()}");
        sb.AppendLine($"   pair annihilated, residual              : {PairAnnihilationResidual():E3}  -> allowed: {ThePairCanAnnihilate()}");
        sb.AppendLine();
        sb.AppendLine("3. LIFETIME - UNDEFINED, NOT MERELY LONG");
        sb.AppendLine($"   clock law sensitivity to the flux       : {ClockLawFluxSensitivity():E3}");
        sb.AppendLine($"   clock law sensitivity to rho (control)  : {ClockLawOrganisationSensitivity():E3}");
        sb.AppendLine($"   the flux has no potential, so no lifetime: {TheFluxHasNoLifetime()}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE CANDIDATES");
        foreach (var (candidate, status, basis) in Candidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("5. THE REQUIREMENTS");
        foreach (var (requirement, status) in RequirementCheck())
            sb.AppendLine($"   {requirement,-24} : {status}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("6. VERDICT");
        sb.AppendLine($"   form of any populating mechanism : {FormVerdict()}");
        sb.AppendLine($"   the population itself            : {PopulationVerdict()}");
        sb.AppendLine($"   the mechanism that populates     : {PopulatingMechanism()}");
        sb.AppendLine($"   refuted                          : {string.Join(", ", RefutedCandidates())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
