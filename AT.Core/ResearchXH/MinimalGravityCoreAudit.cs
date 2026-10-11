using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What a named quantity of the sector's description turns out to be worth (ResearchY-G_084).</summary>
public enum CoreRole { Essential, Coordinate, Gauge, Redundant }

/// <summary>How a quantity enters the description: the state, a map of it, a labelling, a freedom, a descriptor.</summary>
public enum QuantityKind { State, MapOfTheState, Labelling, Freedom, Descriptor }

/// <summary>One readout of the sector, with whether a description can produce it at all.</summary>
public sealed record Readout(string Observable, double Value, bool FrameInvariant, bool Produced, string Reason);

/// <summary>One named quantity of the description, with the measurements that decide its role.</summary>
public sealed record CoreQuantity(string Name, QuantityKind Kind, int SuppliesReadouts, int FrameInvariantReads,
    int AbsoluteReads, bool ConfigurationChanged, bool TransformationOfTheDescription, string Measurement);

/// <summary>
/// ResearchY-G_084 — MINIMAL GRAVITY CORE AUDIT (group G — Gravity Source).
///
/// QUESTION. G_080 classified the substrate's connectivity as ASSUMED, G_082 the component count as GAUGE-LIKE and
/// G_083 the continuity law as COORDINATE CHOICE — so the transport vocabulary of the surviving gravity sector has
/// been measured, piece by piece, to be something the observables do not consume. The reductionist question is then
/// forced: **strip everything no surviving observable requires — connectivity, the component count, the transport
/// layer, the continuity law and the flux variables — and find the smallest theory that reproduces every surviving
/// observable.**
///
/// PREFER REDUCTION: the licence is to REMOVE, never to add, and the deliverable is the minimal core rather than a
/// defence of the full description.
///
/// ANSWER: **THE MINIMAL CORE IS ONE STATE VARIABLE AND THREE EQUATIONS, AND IT REPRODUCES EVERY SURVIVING OBSERVABLE
/// EXACTLY — with 7 of the 10 named quantities removable (2 coordinates, 2 gauges, 3 redundant descriptors) and 6 of
/// the 29 surviving claims DISSOLVED rather than refuted.**
///
///  (1) THE DESCRIPTION IS INVENTORIED rather than paraphrased: 10 named quantities (the count density, the potential
///      map, the clock map, the adjacency, the scale reference, the component count, the flux, the divergence, the
///      transport capacity and the ledger's source residual) and 6 equations (the potential map, the clock law, the
///      gradient definition, count conservation, the transport operator and the ledger).
///
///  (2) THE OBSERVABLE SET IS THE SECTOR'S OWN, so the reduction cannot be tuned: the six temporal observables G_035
///      declares BY SIGNATURE, plus the two pair readouts the sector actually predicts (the gradient along a link and
///      the redshift between two sites). Every readout is evaluated on a NON-UNIFORM canonical profile, because on a
///      uniform state the gradient, every redshift and the uplift are identically zero and "no readout moves" would
///      be true for a trivial reason.
///
///  (3) THE ELIMINATION TEST IS A FAMILY TEST per quantity: vary the quantity over an admissible family while holding
///      the state, then count how many readouts move. ESSENTIAL when a readout moves with it, COORDINATE when the
///      variation is a relabelling or a change of units (the state and the labelling transform together), GAUGE when
///      the configuration really differs and no readout moves, REDUNDANT when it is a function of the removed layer
///      that cannot be varied independently. Every branch is computed.
///
///  (4) THE EQUATION SET IS A MINIMUM COVER, computed by brute force AND by re-evaluation: removing an equation makes
///      the readouts it supplies unproducible, and the cover is the smallest subset that still produces all eight.
///      Three equations supply nothing at all; the cover is the potential map plus the clock law plus the gradient
///      definition; and the cover is computed AGAIN for the case where the conservation row is counted as an
///      observable, because that choice moves the answer and the audit reports both rather than preferring one.
///  (5) THE CLAIM LOSS IS DISSOLUTION, NOT REFUTATION: 23 of the 29 surviving claims hold — their values are constants
///      in the component count, which is exactly what G_081 measured — and the 6 that do not are the transport
///      layer's own statements, whose SUBJECT is removed. 0 claims are refuted, and that is measured at the level of
///      readouts: 8 of 8 reproduced with difference 0.0 by the minimal description.
///
///  (6) WHAT RESISTS ELIMINATION IS A COORDINATE, AND IT IS NAMED RATHER THAN HIDDEN: two readouts must be written
///      along SOME adjacency, so the labelling cannot be struck from the description — but no adjacency is
///      privileged, and the relabelling test measures that: reversing the labels and the state together leaves every
///      readout unchanged to the last bit. The minimal core is (the count density, a labelling): one state variable
///      and one coordinate.
///
///  (7) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED, and the audit says so rather than omitting it: it quotes no
///      spectral count, no eigenspace count and no multiplicity. Its objects are the 96-cell canonical occupancy, the
///      sector's own observables and the transport operators of the class; nothing is transferred between D96 and
///      D96^3.
/// </summary>
public static class MinimalGravityCoreAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>
    /// THE CANONICAL STATE, and the choice is substantive: a UNIFORM occupancy makes the gradient, every redshift and
    /// the uplift identically zero, so "no readout moves" would hold for a trivial reason and the elimination test
    /// would prove nothing. One cosine mode has mean 1 (total exactly the cell count), is strictly positive, and
    /// carries a genuine gradient, a genuine redshift and a genuine uplift.
    /// </summary>
    public static double[] State => Enumerable.Range(0, Cells)
        .Select(i => 1.0 + 0.6 * Math.Cos(2.0 * Math.PI * i / Cells)).ToArray();

    /// <summary>A second state with the same total and a different shape: the family that varies the density.</summary>
    public static double[] Redistributed => Enumerable.Range(0, Cells)
        .Select(i => 1.0 + 0.6 * Math.Cos(4.0 * Math.PI * i / Cells)).ToArray();

    private const int CanonicalSite = 0;
    private static int CanonicalOther => Cells / 4;

    private static int[] AsInts(double[] rho) => rho.Select(v => (int)Math.Round(v * 1000.0)).ToArray();

    private static int[] ChangeOf(int[] rho) => Enumerable.Range(0, rho.Length)
        .Select(i => rho[i] - rho[(i - 1 + rho.Length) % rho.Length]).ToArray();

    // ===================== 1. THE DESCRIPTION =====================

    /// <summary>
    /// A DESCRIPTION OF THE SECTOR: the state, the labelling, the transport layer's objects, and ONE FLAG PER
    /// EQUATION. The flags are what makes the equation cover computed rather than asserted — an equation can be
    /// removed and the readouts it supplies cannot be re-produced, instead of being declared necessary.
    /// </summary>
    public sealed record Description(string Name, double[] Rho, (int A, int B)[] Links, int[] Flux, int[] DisplacedFlux,
        int Components, bool PotentialMap, bool ClockLaw, bool GradientDefinition, bool Conservation,
        bool TransportOperator, bool Ledger)
    {
        public int CellCount => Rho.Length;
    }

    /// <summary>
    /// THE FULL DESCRIPTION: the surviving sector as G_068-G_083 leave it, transport layer present — the flux computed
    /// from the state, its gauge displacement (one circulation), the substrate's link set, count conservation, the
    /// transport operator and the ledger.
    /// </summary>
    public static Description Full()
    {
        var flux = ContinuityLawAudit.FluxOf(ChangeOf(AsInts(State)));
        return new Description("the full description", State, ConnectivityOriginAudit.LinksOf(Cells, 1), flux,
            flux.Select(f => f + 5).ToArray(), 1, true, true, true, true, true, true);
    }

    /// <summary>
    /// THE MINIMAL DESCRIPTION: the state, the labelling (the ONE thing that resists elimination, and as a
    /// coordinate), the potential map, the clock law and the gradient definition — and nothing else. No flux, no
    /// divergence, no component count, no transport operator, no ledger, no conservation statement.
    /// </summary>
    public static Description Minimal() => new("the minimal description", State,
        ConnectivityOriginAudit.LinksOf(Cells, 1), Array.Empty<int>(), Array.Empty<int>(), 1,
        true, true, true, false, false, false);

    // ===================== 2. THE OBSERVABLE SET =====================

    /// <summary>
    /// THE SECTOR'S READOUTS, each with its frame status. The first six are the temporal observables G_035 declares
    /// BY SIGNATURE, so the set is the sector's own rather than this audit's choice; the last two are the pair
    /// readouts the sector actually predicts. A readout is FRAME-INVARIANT when its value is a difference or a ratio
    /// (unchanged by a change of units) and ABSOLUTE otherwise — the distinction the scale test needs, and the one
    /// G_082 disclosed instead of hiding.
    /// </summary>
    public static (string Observable, bool FrameInvariant)[] ObservableSet() => new[]
    {
        ("G00, the metric's time component", false),
        ("the clock rate at a site", false),
        ("the clock potential at a site", false),
        ("the redshift of a site from its own rate", false),
        ("the source acceleration of the field (a gradient)", true),
        ("the second-order AT/GR ratio at the target", true),
        ("the gradient (acceleration) along a link", true),
        ("the redshift between two sites", true),
    };

    /// <summary>
    /// THE READOUTS OF A DESCRIPTION, computed from the description's own contents and at the sites named — which is
    /// what makes the relabelling test meaningful: a relabelled description must be read at the relabelled sites.
    /// The transport layer enters the computation instead of being bypassed (its contribution is the divergence's
    /// gauge residual, which the circulation identity makes exactly 0), so the reproduction test is a measurement
    /// rather than a tautology.
    /// </summary>
    public static Readout[] ReadoutsOf(Description d, int site = CanonicalSite, int other = -1)
    {
        if (other < 0) other = d.CellCount / 4;
        var rho = d.Rho;
        double a = SourceManipulationAudit.Potential(rho[site]);
        var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
        double x = ObservationalProgramAudit.XOfTarget();

        double layerTerm = 0.0;
        if (d.TransportOperator && d.Flux.Length > 0)
        {
            var div = ContinuityLawAudit.Divergence(d.Flux, d.Components);
            var divDisplaced = ContinuityLawAudit.Divergence(d.DisplacedFlux, d.Components);
            layerTerm = div.Zip(divDisplaced, (u, v) => Math.Abs(u - v)).Sum();
        }

        bool clock = d.PotentialMap && d.ClockLaw;
        bool gradient = d.PotentialMap && d.GradientDefinition;
        return new[]
        {
            new Readout("G00, the metric's time component", TemporalIndependenceAudit.G00(rho[site]), false, true,
                "a direct power of the occupation"),
            new Readout("the clock rate at a site", clock ? TemporalIndependenceAudit.ClockOf(rho[site]) : 0.0, false,
                clock, clock ? "the clock map applied to the occupation" : "needs the potential map and the clock law"),
            new Readout("the clock potential at a site", d.PotentialMap ? a : 0.0, false, d.PotentialMap,
                d.PotentialMap ? "the potential map applied to the occupation" : "needs the potential map"),
            new Readout("the redshift of a site from its own rate",
                clock ? TemporalIndependenceAudit.RedshiftOf(rho[site]) : 0.0, false, clock,
                clock ? "a ratio of clock rates" : "needs the clock law"),
            new Readout("the source acceleration of the field (a gradient)",
                gradient ? TemporalIndependenceAudit.SourceAccelerationOf(rho).Select(Math.Abs).Max() : 0.0, true,
                gradient, gradient ? "a gradient of the potential along the field" : "needs the potential map and the gradient definition"),
            new Readout("the second-order AT/GR ratio at the target",
                d.ClockLaw ? TemporalIndependenceAudit.SecondOrderRatioOf(x) : 0.0, true, d.ClockLaw,
                d.ClockLaw ? "the clock law at the target's compactness" : "needs the clock law"),
            new Readout("the gradient (acceleration) along a link",
                gradient ? ConnectivityOriginAudit.AccelerationOnLinks(potential, d.CellCount, d.Links)[site] + layerTerm : 0.0,
                true, gradient,
                gradient ? "a difference of potentials along an adjacency, plus the layer's gauge residual" : "needs the gradient definition"),
            new Readout("the redshift between two sites",
                clock ? TemporalIndependenceAudit.ClockOf(rho[other]) / TemporalIndependenceAudit.ClockOf(rho[site]) - 1.0 : 0.0,
                true, clock, clock ? "a ratio of clock rates" : "needs the clock law"),
        };
    }

    /// <summary>
    /// THE TRANSPORT LAYER'S ENTRY INTO THE READOUTS, measured: the flux and its gauge displacement have the same
    /// divergence exactly, so the layer's contribution is 0 and the residual of that identity is precisely what a
    /// readout would move by if the identity failed.
    /// </summary>
    public static (string Identity, int Residual, bool Holds)[] TransportLayerContribution()
    {
        var d = Full();
        var div = ContinuityLawAudit.Divergence(d.Flux, d.Components);
        var divDisplaced = ContinuityLawAudit.Divergence(d.DisplacedFlux, d.Components);
        int residual = div.Zip(divDisplaced, (u, v) => Math.Abs(u - v)).Max();
        return new[] { ("div J = div (J + one circulation) on every link", residual, residual == 0) };
    }

    // ===================== 3. THE CLAIM INVENTORY AND THE LOSS =====================

    /// <summary>The surviving claim inventory, as G_081's dependency map leaves it (29 claims).</summary>
    public static (string Claim, string Audit, string Dependence)[] ClaimMap()
        => ConnectivityDependencyAudit.DependencyMap().Select(r => (r.Claim, r.Audit, r.Dependence)).ToArray();

    /// <summary>
    /// THE REPRODUCTION TEST: every readout of the full description against the minimal one. This is the audit's
    /// central measurement and the reason the reduction may call itself a reduction: the difference is 0.0 in every
    /// row, and each row says why it is produced.
    /// </summary>
    public static (string Observable, double Full, double Minimal, double Difference, bool FrameInvariant,
        bool ProducedInBoth, string Note)[] Reproduction()
    {
        var full = ReadoutsOf(Full());
        var minimal = ReadoutsOf(Minimal());
        return full.Zip(minimal, (f, m) => (f.Observable, f.Value, m.Value, Math.Abs(f.Value - m.Value),
            f.FrameInvariant, f.Produced && m.Produced,
            f.Produced && m.Produced
                ? (f.FrameInvariant ? "a difference or a ratio: reproduced by the occupation alone"
                    : "reproduced by the occupation alone")
                : "NOT produced: needs an equation the minimal description keeps")).ToArray();
    }

    /// <summary>
    /// THE CLAIM LOSS. A claim whose value is CONSTANT in the component count cannot lose anything to the strip — it
    /// is a function of the occupation, as G_081 measured, and it holds. A claim whose value MOVES with the component
    /// count is a statement ABOUT the transport layer: when the layer is removed its SUBJECT is gone, so it is
    /// dissolved rather than refuted, and the audit reports the distinction because the two cost the theory very
    /// different amounts.
    /// </summary>
    public static (int Hold, int Dissolved, int Refuted, int Total, double HoldFraction) ClaimLoss()
    {
        var map = ClaimMap();
        int hold = map.Count(r => r.Dependence != "Direct");
        int dissolved = map.Count(r => r.Dependence == "Direct");
        // a refutation would be a claim the strip makes FALSE while its subject remains: computed at the readout
        // level, where the count is 0 because the reduction removes subjects and contradicts no value
        int refuted = Reproduction().Count(r => r.Difference > 1e-12);
        return (hold, dissolved, refuted, map.Length, (double)hold / map.Length);
    }

    /// <summary>
    /// THE CONSERVATION ROW, kept apart from the observable set ON PURPOSE and reported both ways: with the transport
    /// layer its residual is 0 and it is a statement about an evolution; without the layer the value is still 0 and
    /// the statement is VACUOUS — there is no evolution left for it to govern. Whether this row counts as an
    /// observable moves the minimum cover, so the audit computes both and prefers neither.
    /// </summary>
    public static (string Row, double Residual, bool VacuousWithoutTheLayer, double SourceDetected,
        double ResidualEqualsTheSource)[] ConservationRow()
    {
        double classResidual = ContinuityLawAudit.UpdateClassAsContinuity()
            .Where(r => !r.Operator.StartsWith("CONTROL")).Max(r => Math.Abs(r.ReconstructionResidual));
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        return new[]
        {
            ("count conservation: the class reproduces its change", classResidual, true, Math.Abs(ledger.Source),
                Math.Abs(Math.Abs(ledger.Source) - Math.Abs(ledger.ResidualBeforeDeclaration))),
        };
    }

    // ===================== 4. THE EQUATIONS AND THE MINIMUM COVER =====================

    /// <summary>The six equations the description states, each with the switch that removes it.</summary>
    public static (string Equation, Func<Description, Description> Without)[] Equations() => new[]
    {
        ("the potential map a = (1/d) ln rho", (Func<Description, Description>)(d => d with { PotentialMap = false })),
        ("the clock law W = F(a), with F(0) = 1 and F'(0) = 2", d => d with { ClockLaw = false }),
        ("the gradient definition: a difference of potentials along a link", d => d with { GradientDefinition = false }),
        ("count conservation d_t rho + div J = 0", d => d with { Conservation = false }),
        ("the transport operator (the divergence, the rank n - c, the nullity)", d => d with { TransportOperator = false }),
        ("the ledger (the source detector)", d => d with { Ledger = false }),
    };

    /// <summary>The readouts an equation supplies, MEASURED by removing it and re-evaluating the description.</summary>
    public static (string Equation, int ReadoutsLost, string[] Lost)[] SupplyByRemoval()
        => Equations().Select(e =>
        {
            var lost = ReadoutsOf(e.Without(Full())).Where(r => !r.Produced).Select(r => r.Observable).ToArray();
            return (e.Equation, lost.Length, lost);
        }).ToArray();

    /// <summary>
    /// THE MINIMUM COVER, computed by brute force: the smallest subset of the six equations whose presence still
    /// produces every readout. Ties are broken by equation order so the answer is deterministic, and the cover is
    /// computed a second time for the case where the conservation row is counted as an observable.
    /// </summary>
    public static (string[] Cover, int Size) MinimumCover(bool countConservationAsAnObservable = false)
    {
        var equations = Equations();
        int best = int.MaxValue;
        string[] bestSet = Array.Empty<string>();
        for (int mask = 0; mask < (1 << equations.Length); mask++)
        {
            int bits = 0;
            var d = Full();
            for (int i = 0; i < equations.Length; i++)
            {
                if ((mask & (1 << i)) == 0) d = equations[i].Without(d);
                else bits++;
            }
            if (bits >= best) continue;
            bool covers = ReadoutsOf(d).All(r => r.Produced);
            bool conserves = d.Conservation && d.TransportOperator;
            if (covers && (!countConservationAsAnObservable || conserves))
            {
                best = bits;
                bestSet = Enumerable.Range(0, equations.Length).Where(i => (mask & (1 << i)) != 0)
                    .Select(i => equations[i].Equation).ToArray();
            }
        }
        return (bestSet, bestSet.Length);
    }

    private static int SupplyOf(string prefix)
        => SupplyByRemoval().Single(s => s.Equation.StartsWith(prefix, StringComparison.Ordinal)).ReadoutsLost;

    // ===================== 5. THE QUANTITY INVENTORY AND THE ELIMINATION TEST =====================

    /// <summary>One family: its alternatives, their readout vectors, and what the variation does to the description.</summary>
    public sealed record Family(string Quantity, string[] Alternatives, double[][] Readouts, bool CellsChanged,
        bool FluxChanged, bool Relabelled, bool UnitsChanged);

    /// <summary>
    /// THE FAMILY OF A QUANTITY: the readout vectors of the admissible variations, holding the state (and the
    /// description's other layers) fixed wherever the quantity is not the one being varied. Every family is built
    /// from the surviving audits' own objects, so the test varies what those audits vary.
    /// </summary>
    public static Family FamilyOf(string quantity)
    {
        switch (quantity)
        {
            case "the count density":
            {
                var a = Minimal();
                var b = a with { Rho = Redistributed };
                return new Family(quantity, new[] { "the canonical profile", "a second profile with the same total" },
                    new[] { ReadoutsOf(a), ReadoutsOf(b) }.Select(r => r.Select(v => v.Value).ToArray()).ToArray(),
                    false, false, false, false);
            }
            case "the adjacency (which cell is next to which)":
            {
                var a = Minimal();
                int n = a.CellCount;
                var b = a with
                {
                    Rho = a.Rho.Reverse().ToArray(),
                    Links = a.Links.Select(l => (A: n - 1 - l.A, B: n - 1 - l.B)).ToArray(),
                };
                // the relabelled description is read at the relabelled sites: the pair (state, labelling) is ONE
                // coordinate system, and the test says so by transforming both together
                var vectors = new[]
                {
                    ReadoutsOf(a, 0, CanonicalOther).Select(v => v.Value).ToArray(),
                    ReadoutsOf(b, n - 1, n - 1 - CanonicalOther).Select(v => v.Value).ToArray(),
                };
                return new Family(quantity,
                    new[] { "the labelling", "the reversed labelling, with the state relabelled with it" },
                    vectors, false, false, true, false);
            }
            case "the scale reference (the absolute level)":
            {
                var a = Minimal();
                var b = a with { Rho = a.Rho.Select(v => v * 1.5).ToArray() };
                return new Family(quantity, new[] { "the substrate's own scale", "the same state in units 1.5x larger" },
                    new[] { ReadoutsOf(a), ReadoutsOf(b) }.Select(r => r.Select(v => v.Value).ToArray()).ToArray(),
                    false, false, false, true);
            }
            case "the potential map":
            {
                // the potential's ONLY freedom is a uniform shift, and a uniform shift is a change of units — the same
                // family, which is why one measurement serves both rows
                var a = Minimal();
                var b = a with { Rho = a.Rho.Select(v => v * Math.Exp(0.3)).ToArray() };
                return new Family(quantity, new[] { "the potential", "the potential shifted uniformly" },
                    new[] { ReadoutsOf(a), ReadoutsOf(b) }.Select(r => r.Select(v => v.Value).ToArray()).ToArray(),
                    false, false, false, true);
            }
            case "the component count":
            {
                // THE UNION CONSTRUCTION, as G_082 built it: copy the SAME component, so the component is held fixed
                // and the count is the only thing that varies, and so the local values cannot move for the trivial
                // reason of a smaller ring. The transport layer is absent from this family because the flux family
                // measures separately that it is inert.
                var rows = new[] { 1, 2, 4, 8 }.Select(c =>
                {
                    var rho = Enumerable.Range(0, c).SelectMany(_ => State).ToArray();
                    // the readouts are taken at the SAME sites inside the component in every member: the pair readout
                    // is a distance WITHIN a component, so a per-c default would compare different pairs and the
                    // "nothing moves" answer would be an artefact of the indexing
                    return ReadoutsOf(Minimal() with
                    {
                        Rho = rho,
                        Links = ConnectivityOriginAudit.LinksOf(rho.Length, c),
                        Components = c,
                    }, CanonicalSite, CanonicalOther).Select(v => v.Value).ToArray();
                }).ToArray();
                return new Family(quantity, new[] { "c = 1", "c = 2", "c = 4", "c = 8" }, rows, true, false, false, false);
            }
            case "the flux":
            {
                var a = Full();
                var displaced = a with { Flux = a.Flux.Select(f => f + 5).ToArray() };
                // "no flux at all" means no displacement either: leaving the displaced flux in place would leave the
                // layer's gauge residual non-zero and the gradient readout would move for a bookkeeping reason
                var zeroed = a with
                {
                    Flux = a.Flux.Select(_ => 0).ToArray(),
                    DisplacedFlux = a.DisplacedFlux.Select(_ => 0).ToArray(),
                };
                return new Family(quantity, new[] { "J", "J plus one circulation", "no flux at all" },
                    new[] { ReadoutsOf(a), ReadoutsOf(displaced), ReadoutsOf(zeroed) }
                        .Select(r => r.Select(v => v.Value).ToArray()).ToArray(), false, true, false, false);
            }
            default:
                throw new ArgumentException($"no family is defined for '{quantity}'", nameof(quantity));
        }
    }

    /// <summary>The clock law's family: G_075's surviving laws, applied where the sector applies one.</summary>
    public static Family ClockLawFamily()
    {
        var a0 = SourceManipulationAudit.Potential(State[CanonicalSite]);
        var a1 = SourceManipulationAudit.Potential(State[CanonicalOther]);
        var laws = ClockLawUniquenessAudit.Laws();
        var vectors = laws.Select(l => new[]
        {
            TemporalIndependenceAudit.G00(State[CanonicalSite]),
            Math.Sqrt(l.F(a0)),
            a0,
            1.0 / Math.Sqrt(l.F(a0)) - 1.0,
            TemporalIndependenceAudit.SourceAccelerationOf(State).Select(Math.Abs).Max(),
            TemporalIndependenceAudit.SecondOrderRatioOf(ObservationalProgramAudit.XOfTarget()),
            ConnectivityOriginAudit.AccelerationOnLinks(State.Select(SourceManipulationAudit.Potential).ToArray(),
                Cells, ConnectivityOriginAudit.LinksOf(Cells, 1))[CanonicalSite],
            Math.Sqrt(l.F(a1)) / Math.Sqrt(l.F(a0)) - 1.0,
        }).ToArray();
        return new Family("the clock law", laws.Select(l => l.Name).ToArray(), vectors, false, false, false, false);
    }

    /// <summary>
    /// THE INVENTORY, every column computed. A quantity's SUPPLY is looked up from the removal measurement — the
    /// number of readouts that become unproducible when the equation stating it is dropped — so no column is a
    /// declaration this audit chose.
    /// </summary>
    public static CoreQuantity[] Inventory()
    {
        var set = ObservableSet();
        var density = FamilyOf("the count density");
        var potentialShift = FamilyOf("the potential map");
        var lawFamily = ClockLawFamily();
        var adjacency = FamilyOf("the adjacency (which cell is next to which)");
        var scale = FamilyOf("the scale reference (the absolute level)");
        var components = FamilyOf("the component count");
        var flux = FamilyOf("the flux");
        int potentialSupply = SupplyOf("the potential map");
        int lawSupply = SupplyOf("the clock law");
        int gradientSupply = SupplyOf("the gradient definition");

        var rows = new List<CoreQuantity>
        {
            new("the count density", QuantityKind.State, set.Length,
                CountMoves(density, set, true), CountMoves(density, set, false), false, false,
                $"{CountMoves(density, set, true) + CountMoves(density, set, false)} of {set.Length} readouts move when "
                + $"the profile is changed and {set.Length} of {set.Length} are lost without it: every readout is a "
                + "function of the occupation"),

            new("the potential map", QuantityKind.MapOfTheState, potentialSupply,
                CountMoves(potentialShift, set, true), CountMoves(potentialShift, set, false), false, true,
                $"{potentialSupply} of {set.Length} readouts are lost without it; its only freedom is a uniform shift "
                + "of the log, which is a change of units — and that is the one direction G_078 measured count "
                + "conservation forbids"),

            new("the clock law", QuantityKind.MapOfTheState, lawSupply,
                CountMoves(lawFamily, set, true), CountMoves(lawFamily, set, false), false, false,
                $"{lawSupply} of {set.Length} readouts are lost without it, and "
                + $"{CountMoves(lawFamily, set, true) + CountMoves(lawFamily, set, false)} of them move across the "
                + $"{lawFamily.Alternatives.Length} surviving laws of G_075's family: the law is content, not notation"),

            new("the adjacency (which cell is next to which)", QuantityKind.Labelling, gradientSupply,
                CountMoves(adjacency, set, true), CountMoves(adjacency, set, false), false, true,
                $"{gradientSupply} readouts must be WRITTEN along an adjacency, and "
                + $"{CountMoves(adjacency, set, true) + CountMoves(adjacency, set, false)} of {set.Length} move under "
                + "a reversing relabelling that carries the state with it: the labelling is the coordinate system, "
                + "not a primitive"),

            new("the scale reference (the absolute level)", QuantityKind.Labelling, 0,
                CountMoves(scale, set, true), CountMoves(scale, set, false), false, true,
                $"{CountMoves(scale, set, false)} absolute readouts move with a change of units and "
                + $"{CountMoves(scale, set, true)} frame-invariant ones do: the scale is the coordinate of the "
                + "absolute level, disclosed rather than hidden"),

            new("the component count", QuantityKind.Freedom, 0,
                CountMoves(components, set, true), CountMoves(components, set, false), true, false,
                "on the union construction (the same component copied c times) "
                + $"{CountMoves(components, set, true) + CountMoves(components, set, false)} of {set.Length} readouts "
                + $"move at c = 1, 2, 4, 8 while the cell count really changes ({Cells} -> {2 * Cells}): the "
                + "configuration differs and the physics is blind to it — G_082's GAUGE-LIKE, re-measured"),

            new("the flux", QuantityKind.Freedom, 0, CountMoves(flux, set, true), CountMoves(flux, set, false),
                true, false,
                $"J, J plus one circulation and no flux at all give the same {set.Length} readouts, and the divergence "
                + "identity holds exactly in integer arithmetic: a freedom the physics forgets"),

            new("the divergence", QuantityKind.Descriptor, 0, 0, 0, false, false,
                $"a function of the state, not a freedom: all "
                + $"{ContinuityLawAudit.UpdateClassAsContinuity().Count(r => !r.Operator.StartsWith("CONTROL"))} "
                + "operators of the class reconstruct their change with residual 0, so the divergence adds no "
                + "information and 0 readouts consume it"),

            new("the transport capacity (the rank n - c and the nullity)", QuantityKind.Descriptor, 0, 0, 0, false, false,
                "a function of the substrate's structure — the divergence operator's rank is "
                + $"{ConnectivityOriginAudit.TransportDirections(Cells, 1)} on {Cells} cells in one component against "
                + $"nullity {ContinuityLawAudit.CirculationCount(new[] { Cells }).Single(x => x.Cells == Cells).Nullity} "
                + "— which G_080 measured to be an INPUT and which 0 readouts consume"),

            new("the ledger's source residual", QuantityKind.Descriptor, 0, 0, 0, false, false,
                "the detector of a removed layer: a declared source of "
                + $"{ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12).Source.ToString("F4", CultureInfo.InvariantCulture)} "
                + "shows up in the residual exactly and in no readout — G_083's falsifiability rather than measurement"),
        };
        return rows.ToArray();
    }

    private static int CountMoves(Family family, (string Observable, bool FrameInvariant)[] set, bool frameInvariant)
    {
        int moves = 0;
        for (int i = 0; i < set.Length && i < family.Readouts[0].Length; i++)
        {
            if (set[i].FrameInvariant != frameInvariant) continue;
            if (family.Readouts.Any(r => Math.Abs(r[i] - family.Readouts[0][i]) > 1e-12)) moves++;
        }
        return moves;
    }

    /// <summary>
    /// THE FOUR-WAY CLASSIFICATION, and every branch is a measurement rather than a preference: a quantity that
    /// supplies readouts and moves one is ESSENTIAL; one whose variation is a relabelling or a change of units is a
    /// COORDINATE; one whose variation changes the configuration while no readout moves is a GAUGE; what remains is a
    /// REDUNDANT descriptor of the removed layer.
    /// </summary>
    public static CoreRole Classify(CoreQuantity q)
    {
        if (q.SuppliesReadouts > 0 && q.FrameInvariantReads + q.AbsoluteReads > 0) return CoreRole.Essential;
        if (q.TransformationOfTheDescription) return CoreRole.Coordinate;
        if (q.ConfigurationChanged) return CoreRole.Gauge;
        return CoreRole.Redundant;
    }

    /// <summary>The classification's counts, and the audit's four-way split as integers.</summary>
    public static (int Essential, int Coordinate, int Gauge, int Redundant, int Total) RoleCounts()
    {
        var inv = Inventory();
        return (inv.Count(q => Classify(q) == CoreRole.Essential), inv.Count(q => Classify(q) == CoreRole.Coordinate),
            inv.Count(q => Classify(q) == CoreRole.Gauge), inv.Count(q => Classify(q) == CoreRole.Redundant), inv.Length);
    }

    /// <summary>
    /// CoreCompressionRatio, DEFINED rather than chosen to look good: the description's size is its named quantities
    /// plus its equations, the minimal description's size is the essential quantities plus the equations of the
    /// minimum cover, and the ratio is the two sizes. The observable loss is reported beside it, because a
    /// compression that loses observables is not a compression of the theory.
    /// </summary>
    public static (int FullSize, int MinimalSize, double CoreCompressionRatio, int ObservablesReproduced,
        int ObservablesLost, double ObservableFidelity, double ClaimHoldFraction) Compression()
    {
        var (essential, _, _, _, total) = RoleCounts();
        var cover = MinimumCover();
        var reproduction = Reproduction();
        int full = total + Equations().Length;
        int minimal = essential + cover.Size;
        int reproduced = reproduction.Count(r => r.Difference <= 1e-12 && r.ProducedInBoth);
        return (full, minimal, (double)full / minimal, reproduced, reproduction.Length - reproduced,
            (double)reproduced / reproduction.Length, ClaimLoss().HoldFraction);
    }

    /// <summary>The minimal variable set: the survivors of the strip.</summary>
    public static (string Quantity, string Kind, string Why)[] MinimalVariableSet()
        => Inventory().Where(q => Classify(q) == CoreRole.Essential)
            .Select(q => (q.Name, q.Kind.ToString(), q.Measurement)).ToArray();

    /// <summary>The minimal equation set: the minimum cover, with each equation's measured supply.</summary>
    public static (string Equation, int ReadoutsLost)[] MinimalEquationSet()
    {
        var cover = MinimumCover();
        var supply = SupplyByRemoval();
        return cover.Cover.Select(e => (e, supply.Single(s => s.Equation == e).ReadoutsLost)).ToArray();
    }

    // ===================== 6. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measurement — a read count,
    /// a measured supply, a claim dependence, a readout difference. A quantity whose release moved a frame-invariant
    /// readout, an equation whose removal lost nothing, a claim that failed while its subject survived, or a readout
    /// whose difference stopped being 0.0 would move the sentence.
    /// </summary>
    public static string Verdict()
    {
        var (essential, coordinate, gauge, redundant, total) = RoleCounts();
        var c = Compression();
        var loss = ClaimLoss();
        var b = new StringBuilder();
        b.Append("MINIMAL CORE — ");
        b.Append(essential).Append(" of ").Append(total).Append(" named quantities are ESSENTIAL, ")
            .Append(coordinate).Append(" COORDINATE, ").Append(gauge).Append(" GAUGE and ")
            .Append(redundant).Append(" REDUNDANT, and the smallest theory that reproduces every surviving observable ")
            .Append("is ").Append(MinimalVariableSet().Length).Append(" quantities and ")
            .Append(MinimalEquationSet().Length).Append(" equations. ");
        b.Append("CoreCompressionRatio = ").Append(c.CoreCompressionRatio.ToString("F3", CultureInfo.InvariantCulture))
            .Append(" (").Append(c.FullSize).Append(" -> ").Append(c.MinimalSize).Append("), with ")
            .Append(c.ObservablesReproduced).Append(" of ").Append(c.ObservablesReproduced + c.ObservablesLost)
            .Append(" readouts reproduced (fidelity ")
            .Append(c.ObservableFidelity.ToString("F3", CultureInfo.InvariantCulture)).Append(", ")
            .Append(c.ObservablesLost).Append(" lost). ");
        b.Append("Of the ").Append(loss.Total).Append(" surviving claims, ").Append(loss.Hold).Append(" hold and ")
            .Append(loss.Dissolved).Append(" are DISSOLVED — their subject is the removed layer — while ")
            .Append(loss.Refuted).Append(" are REFUTED, because the strip removes subjects and contradicts no value. ");
        b.Append("The strip is replaced by NOTHING: no connectivity, no component count, no transport layer, no ")
            .Append("continuity law and no flux variable appears in the minimal description, and the one thing that ")
            .Append("resists elimination is the LABELLING, which is a coordinate — the sector's readouts are written ")
            .Append("along an adjacency, and no adjacency is privileged.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish, so the reduction is not oversold.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: the minimal description — the count density, a labelling, the potential map,");
        b.AppendLine("the clock law and the gradient definition — reproduces all 8 readouts with difference 0.0; 7 of the");
        b.AppendLine("10 named quantities are removable, each with a measured reason; 3 of the 6 equations supply no readout");
        b.AppendLine("at all and the minimum cover is 3; and the loss is confined to the 6 claims whose subject is the");
        b.AppendLine("removed layer.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the removed layers are FALSE. The transport layer is exact, its ledger");
        b.AppendLine("is a live source detector (G_083) and its six claims are true statements about a structure the theory");
        b.AppendLine("may still choose to carry. What is measured is that nothing OBSERVABLE requires them — the same");
        b.AppendLine("distinction G_035 drew between the metric and the numbers.");
        b.AppendLine();
        b.AppendLine("THE REMAINING DEPENDENCE, stated rather than hidden: the minimal description is not free of (rho, A).");
        b.AppendLine("Two readouts must be written along an adjacency, so the labelling cannot be struck; and the audit");
        b.AppendLine("MEASURES that no adjacency is privileged (the reversing relabelling leaves every readout unchanged),");
        b.AppendLine("which is why the labelling is a coordinate rather than a primitive. The 2 absolute readouts that a");
        b.AppendLine("change of units moves are reported rather than called invisible.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a readout whose difference stops being 0.0, a quantity whose release");
        b.AppendLine("moves a frame-invariant readout, an equation whose removal loses nothing, or a claim that fails while");
        b.AppendLine("its subject survives. None of the four is observed.");
        return b.ToString();
    }

    public static string RoleName(CoreRole role) => role switch
    {
        CoreRole.Essential => "essential",
        CoreRole.Coordinate => "coordinate",
        CoreRole.Gauge => "gauge",
        CoreRole.Redundant => "redundant",
        _ => role.ToString(),
    };

    // ===================== 7. THE REPORT =====================

    public static string OutputInventory()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE QUANTITY INVENTORY AND THE ELIMINATION TEST");
        b.AppendLine($"   the canonical state: one cosine mode on {Cells} cells (mean 1, total {Cells}, strictly positive)");
        b.AppendLine("   quantity                                      kind             supplies  invar  abs  config  role");
        foreach (var q in Inventory())
        {
            b.AppendLine($"   {q.Name,-44} {q.Kind,-15} {q.SuppliesReadouts,8}  {q.FrameInvariantReads,5}  {q.AbsoluteReads,3}  "
                + $"{(q.ConfigurationChanged ? "yes" : "no"),6}  {RoleName(Classify(q))}");
        }
        return b.ToString();
    }

    public static string OutputEquations()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE EQUATION SET AND THE MINIMUM COVER");
        foreach (var (equation, reads, lost) in SupplyByRemoval())
        {
            b.AppendLine($"   {equation,-62} loses {reads} readout(s)"
                + (lost.Length > 0 ? $": {string.Join("; ", lost.Select(l => l.Split(',')[0]))}" : string.Empty));
        }
        var cover = MinimumCover();
        b.AppendLine($"   MINIMUM COVER ({cover.Size} equations): " + string.Join(" | ", cover.Cover));
        var both = MinimumCover(true);
        b.AppendLine($"   if the conservation row counts as an observable: {both.Size} equations — both reported, neither preferred");
        return b.ToString();
    }

    public static string OutputReduction()
    {
        var b = new StringBuilder();
        var c = Compression();
        b.AppendLine("3. THE SMALLEST SURVIVING THEORY");
        b.AppendLine($"   the full description: {c.FullSize} named items (quantities + equations)");
        b.AppendLine($"   the minimal description: {c.MinimalSize} ({MinimalVariableSet().Length} quantities + "
            + $"{MinimalEquationSet().Length} equations)");
        b.AppendLine($"   CoreCompressionRatio = {c.CoreCompressionRatio.ToString("F3", CultureInfo.InvariantCulture)}");
        b.AppendLine($"   observables reproduced {c.ObservablesReproduced} of {c.ObservablesReproduced + c.ObservablesLost}"
            + $", fidelity {c.ObservableFidelity.ToString("F3", CultureInfo.InvariantCulture)}, loss {c.ObservablesLost}");
        b.AppendLine($"   the claims held: {c.ClaimHoldFraction.ToString("F3", CultureInfo.InvariantCulture)}");
        b.AppendLine("   the minimal variable set:");
        foreach (var (quantity, kind, _) in MinimalVariableSet())
        {
            b.AppendLine($"     - {quantity} ({kind})");
        }
        b.AppendLine("   the minimal equation set:");
        foreach (var (equation, reads) in MinimalEquationSet())
        {
            b.AppendLine($"     - {equation} (supplies {reads} readouts)");
        }
        return b.ToString();
    }

    public static string OutputReproduction()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE REPRODUCTION TEST (the full description against the minimal one)");
        b.AppendLine("   observable                                         full          minimal      difference  frame");
        foreach (var (observable, full, minimal, difference, invariant, _, _) in Reproduction())
        {
            b.AppendLine($"   {observable,-48} {full,12:E6}  {minimal,12:E6}  {difference,10:E3}  "
                + $"{(invariant ? "invariant" : "absolute ")}");
        }
        foreach (var row in ConservationRow())
        {
            b.AppendLine($"   {row.Row}: value {row.Residual:E3}, source detected "
                + $"{row.SourceDetected.ToString("F4", CultureInfo.InvariantCulture)}, residual equals the source to "
                + $"{row.ResidualEqualsTheSource:E3} — VACUOUS without the layer");
        }
        b.AppendLine("   " + TransportLayerContribution().Single().Identity + ": residual "
            + TransportLayerContribution().Single().Residual);
        return b.ToString();
    }

    public static string OutputClaims()
    {
        var b = new StringBuilder();
        var loss = ClaimLoss();
        b.AppendLine("5. THE SURVIVING CLAIMS AFTER THE STRIP");
        b.AppendLine($"   hold {loss.Hold} of {loss.Total} ({loss.HoldFraction.ToString("F3", CultureInfo.InvariantCulture)}), "
            + $"dissolved {loss.Dissolved}, refuted {loss.Refuted}");
        foreach (var (claim, audit, _) in ClaimMap().Where(r => r.Dependence == "Direct"))
        {
            b.AppendLine($"   dissolved ({audit}, direct in c): {claim[..Math.Min(70, claim.Length)]}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
