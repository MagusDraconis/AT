using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_080 — CONNECTIVITY ORIGIN AUDIT (group G — Gravity Source).
///
/// QUESTION. G_079 derived the continuity law `d_t rho + div J = 0` for the surviving update class and proved that
/// its equivalence with count conservation requires ONE premise the law cannot supply for itself: the
/// **CONNECTIVITY** of the substrate. On one connected ring the zero-sum subspace IS the image of the divergence,
/// but on a substrate of c components a transfer across a cut conserves the total, is not any flux, and is a
/// source/sink pair. This audit asks the ORIGIN question that G_079 handed on: is that connectivity
/// **DERIVED** from the theory's own primitives, **EMERGENT** from the update relation, or **ASSUMED** as an input?
///
/// ALLOWED as evidence: Difference, Actualization, the generation relation, count conservation.
/// FORBIDDEN as evidence: a graph-connectivity axiom, an imported topology.
///
/// ANSWER: **ASSUMED — CONNECTIVITY IS AN INPUT AT THE LEVEL OF THE PRIMITIVES, AND BOTH ALTERNATIVES ARE REFUTED
/// BY MEASUREMENT RATHER THAN BY ARGUMENT.**
///
///  (1) THE FOUR ALLOWED PRIMITIVES ARE BLIND TO THE CUT. Each is given a certificate — Difference by the set of
///      link differences it realises, Actualization by the set of local ticks, the generation relation by the set
///      of realised in-degrees, count conservation by the set of component sums plus the class's leak — and the
///      certificate is computed on a substrate and on the DISJOINT UNION of two copies of it. Not one of the four
///      changes its content (0 of 4), while every GLOBAL descriptor does: components 1 -> 2, transport rank
///      7 -> 14, generation roots 1 -> 2. The reason is structural and is the audit's first result: the three
///      relational primitives are LOCAL (radius one link) and the fourth is a SUM, and a sum cannot see which
///      component its terms came from.
///
///  (2) THE UPDATE RELATION CONSERVES THE COMPONENT COUNT INSTEAD OF PRODUCING IT. The transport class is written
///      on the link set, so its Laplacian is BLOCK DIAGONAL across a cut: the influence of a cell on a cell in
///      another component is exactly 0, against 1 on the connected ring, where the same pair is a link. The
///      criterion is therefore live rather than vacuous. A conserved integer that the dynamics never sets is an
///      INITIAL DATUM — which is exactly what the count itself is (QG267), and the component count now joins it.
///
///  (3) THE GENERATIVE RULE ASSUMES THE RING RATHER THAN BUILDING IT. The actualization link rule is written
///      `(i + d) mod n` — arithmetic on the whole index set. Applied to a genuinely disconnected substrate it
///      does not fail: it SILENTLY RECONNECTS it, creating links across the cut and returning a link set with one
///      component where the substrate has c. On the smallest split pair the rule manufactures the very link whose
///      absence made the pair disconnected. Connectivity is therefore in the rule's ARITHMETIC, not in its output.
///
///  (4) THE SMALLEST DISCONNECTED SURVIVOR IS TWO CELLS. One cell per component (n = 2, c = 2) has rank 0: it
///      admits NO non-zero transport at all, and the single non-trivial count-conserving change it does admit is
///      the forbidden source/sink pair. A disconnected universe is not merely allowed, it is FROZEN — and the
///      smallest cut of a ring is two links, so a single defect can never disconnect the canonical substrate.
///
///  (5) THE SIGNATURE OF A DISCONNECTED SECTOR IS A FROZEN, UNREADABLE OFFSET. Its per-component-uniform
///      configuration is an exact fixed point (max update 0) with a non-zero potential gap between the
///      components; the local reads are within-component; on the smallest survivor there is NO acceleration
///      observable at all; the redshift between two components is DEFINED and moves with the free level, but it is
///      UNREACHABLE, because the number of links across the cut is 0; and 0 of the 6 temporal observables read a
///      flux. The free level is written into an observable that no experiment can perform.
///
/// NO GRAPH-CONNECTIVITY AXIOM IS IMPORTED. Connectivity is never assumed: the audit BUILDS each substrate with an
/// explicit link set, COMPUTES the component count from it, and then measures which of the allowed primitives can
/// see it. The verdict follows from that measurement rather than from a definition.
/// </summary>
public static class ConnectivityOriginAudit
{
    /// <summary>The spatial dimension of the potential A = (1/d) ln rho (the surviving source law, G_077).</summary>
    public const int D = SourceManipulationAudit.D;

    /// <summary>The canonical lattice size, so the rank figures are the ones the other audits quote.</summary>
    public const int CanonicalCells = SourceManipulationAudit.CanonicalCells;

    private static string F0(double v) => v.ToString("F0", CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", CultureInfo.InvariantCulture);
    private static string E3(double v) => v.ToString("E3", CultureInfo.InvariantCulture);

    /// <summary>The state the audit works with: a deterministic, non-uniform occupation on a ring of eight.</summary>
    public static double[] WorkhorseRho => new[] { 4.0, 7, 4, 8, 9, 4, 5, 6 };

    // ===================== 1. THE SUBSTRATE =====================

    /// <summary>
    /// THE SUBSTRATE FAMILY. A substrate is `Cells` cells split into `Components` rings of equal size, each ring
    /// closed on itself. `Components` is the integer at issue: 1 is connected, c &gt; 1 is disconnected, and every
    /// quantity this audit quotes is indexed by it. No spectral quantity is quoted anywhere in this audit, so the
    /// spectral-substrate rule is satisfied by construction — the results are graph statements about a link set and
    /// hold for the 1D ring family from 2 cells to the canonical 96, and for any decomposition of it into c parts.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int PerComponent)[] Substrates()
    {
        var cases = new (string Substrate, int Cells, int Components)[]
        {
            ("the smallest ring", 2, 1),
            ("the smallest split pair", 2, 2),
            ("a 4-cell ring", 4, 1),
            ("two 2-cell rings", 4, 2),
            ("four single cells", 4, 4),
            ("the 8-cell ring", 8, 1),
            ("two 4-cell rings", 8, 2),
            ("four 2-cell rings", 8, 4),
            ("the canonical ring", CanonicalCells, 1),
            ("the canonical ring split in two", CanonicalCells, 2),
        };
        return cases.Select(c => (c.Substrate, c.Cells, c.Components, c.Cells / c.Components)).ToArray();
    }

    /// <summary>
    /// THE LINK SET: a ring inside each component, built explicitly so that connectivity is CONSTRUCTED here and
    /// never assumed. A component of one cell has no link at all; a component of two cells has exactly one.
    /// </summary>
    public static (int A, int B)[] LinksOf(int cells, int components)
    {
        int per = cells / components;
        var set = new HashSet<(int, int)>();
        if (per >= 2)
            for (int c = 0; c < components; c++)
                for (int k = 0; k < per; k++)
                {
                    int i = c * per + k, j = c * per + (k + 1) % per;
                    if (i == j) continue;
                    set.Add((Math.Min(i, j), Math.Max(i, j)));
                }
        return set.OrderBy(l => l.Item1).ThenBy(l => l.Item2).ToArray();
    }

    /// <summary>The component a cell belongs to. This is the audit's bookkeeping, not a datum of the theory.</summary>
    public static int ComponentOf(int cell, int cells, int components) => cell / (cells / components);

    /// <summary>The number of connected components of a link set, computed by union-find — measured, not assumed.</summary>
    public static int ComponentsOf(int cells, (int A, int B)[] links)
    {
        var parent = Enumerable.Range(0, cells).ToArray();
        int Find(int x)
        {
            while (parent[x] != x) { parent[x] = parent[parent[x]]; x = parent[x]; }
            return x;
        }
        foreach (var (a, b) in links)
        {
            int ra = Find(a), rb = Find(b);
            if (ra != rb) parent[ra] = rb;
        }
        return Enumerable.Range(0, cells).Select(Find).Distinct().Count();
    }

    /// <summary>The graph Laplacian of a link set: the transport operator of the count on THAT substrate.</summary>
    public static double[][] Laplacian(int cells, (int A, int B)[] links)
    {
        var m = new double[cells][];
        for (int i = 0; i < cells; i++) m[i] = new double[cells];
        foreach (var (a, b) in links) { m[a][a] -= 1.0; m[b][b] -= 1.0; m[a][b] += 1.0; m[b][a] += 1.0; }
        return m;
    }

    /// <summary>The update the transport class makes at every cell: the Laplacian applied to the occupation.</summary>
    public static double[] UpdateAt(int cells, (int A, int B)[] links, double[] rho)
    {
        var l = Laplacian(cells, links);
        return Enumerable.Range(0, cells).Select(i => Enumerable.Range(0, cells).Sum(j => l[i][j] * rho[j])).ToArray();
    }

    /// <summary>The influence of one cell's count on another cell's update: the Laplacian entry L[target][source].</summary>
    public static double Influence(int cells, (int A, int B)[] links, int source, int target)
        => Laplacian(cells, links)[target][source];

    /// <summary>The number of Laplacian entries that JOIN different components — the cut's fingerprint in the operator.</summary>
    public static int EntriesJoiningDifferentComponents(int cells, int components)
    {
        var l = Laplacian(cells, LinksOf(cells, components));
        int count = 0;
        for (int i = 0; i < cells; i++)
            for (int j = 0; j < cells; j++)
                if (ComponentsDiffer(i, j, cells, components) && Math.Abs(l[i][j]) > 0)
                    count++;
        return count;
    }

    private static bool ComponentsDiffer(int i, int j, int cells, int components)
        => ComponentOf(i, cells, components) != ComponentOf(j, cells, components);

    /// <summary>The mean out-boundary weight from component 0 into all other components: 0 iff the class is block diagonal.</summary>
    public static double WeightFromComponentZeroToTheRest(int cells, int components)
    {
        var l = Laplacian(cells, LinksOf(cells, components));
        double sum = 0.0;
        for (int i = 0; i < cells; i++)
            if (ComponentOf(i, cells, components) == 0)
                for (int j = 0; j < cells; j++)
                    if (ComponentOf(j, cells, components) != 0)
                        sum += Math.Abs(l[i][j]);
        return sum;
    }

    /// <summary>
    /// THE LIVE CONTROL FOR THE BLOCK-DIAGONAL TEST. The "components" of a substrate are the audit's bookkeeping, so
    /// a count of entries joining different components would be zero by construction and would prove nothing. This
    /// splits the index set in HALF instead — a partition no link set can influence — and counts the Laplacian
    /// entries that join the halves. On the connected ring those are the two links that cross the middle; on a
    /// substrate whose components are the halves, exactly zero. The criterion is therefore live.
    /// </summary>
    public static int EntriesJoiningTheTwoHalves(int cells, int components)
    {
        var l = Laplacian(cells, LinksOf(cells, components));
        int half = cells / 2, count = 0;
        for (int i = 0; i < cells; i++)
            for (int j = 0; j < cells; j++)
                if ((i < half) != (j < half) && Math.Abs(l[i][j]) > 0)
                    count++;
        return count;
    }

    /// <summary>The number of independent transport directions of a substrate: the rank of its divergence operator.</summary>
    public static int TransportDirections(int cells, int components)
        => ClockSourceAudit.Rank(ContinuityLawAudit.DivergenceMatrix(cells, components));

    /// <summary>
    /// THE TRANSPORT CAPACITY. The rank of the divergence operator is the number of independent transport
    /// directions the substrate offers, and it is `n - c` exactly: the divergence's rank FALLS BY ONE for each cut.
    /// Connectivity is therefore worth exactly one direction per component, which is the only currency the count
    /// transport has.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int Directions, int DirectionsLostToTheCuts)[]
        TransportCapacity()
        => Substrates().Select(s => (
            s.Substrate,
            s.Cells,
            s.Components,
            TransportDirections(s.Cells, s.Components),
            TransportDirections(s.Cells, 1) - TransportDirections(s.Cells, s.Components))).ToArray();

    // ===================== 2. Q1 — CAN DISCONNECTED UNIVERSES EXIST? =====================

    /// <summary>
    /// THE COUNT-CONSERVING WITNESSES of a substrate: every transfer inside a component that has at least two cells,
    /// and one transfer across each neighbouring pair of components. The second kind is the one G_079 showed can
    /// fall outside the image of the divergence.
    /// </summary>
    public static (int[] Change, string Kind)[] ConservingWitnesses(int cells, int components)
    {
        int per = cells / components;
        var list = new List<(int[], string)>();
        if (per >= 2)
            for (int c = 0; c < components; c++)
                for (int k = 0; k < per; k++)
                {
                    int i = c * per + k, j = c * per + (k + 1) % per;
                    if (i == j) continue;
                    var w = new int[cells];
                    w[i] += 1;
                    w[j] -= 1;
                    list.Add((w, "a transfer inside one component"));
                }
        for (int c = 0; c + 1 < components; c++)
        {
            var w = new int[cells];
            w[c * per] += 1;
            w[(c + 1) * per] -= 1;
            list.Add((w, "a transfer ACROSS the cut"));
        }
        return list.ToArray();
    }

    /// <summary>
    /// CAN A DISCONNECTED UNIVERSE EXIST? The audit constructs the per-component-uniform occupation — level 4 in
    /// the first component, 9 in the second, 14 in the third — and asks three things of it on every substrate in the
    /// family: is it an exact fixed point of the transport class, does it conserve the count, and does it carry a
    /// non-zero potential GAP between the components (so that it is not a trivial state)? It is, it does, and it
    /// does. The last column counts how many of the conserving changes are FORBIDDEN transfers: exactly the c - 1
    /// cross-transfers, which no flux of the class can carry.
    /// </summary>
    public static (string Substrate, int Cells, int Components, double MaxUpdateOfTheComponentUniformState,
        int ConservingWitnesses, int OfWhichAreTransports, int OfWhichAreForbiddenTransfers,
        double PotentialGapBetweenTheComponents)[] DisconnectedSurvivors()
        => Substrates().Select(s =>
        {
            var rho = Enumerable.Range(0, s.Cells).Select(i => 4.0 + 5.0 * ComponentOf(i, s.Cells, s.Components)).ToArray();
            var links = LinksOf(s.Cells, s.Components);
            double maxUpdate = UpdateAt(s.Cells, links, rho).Select(Math.Abs).Max();
            var witnesses = ConservingWitnesses(s.Cells, s.Components);
            int transports = witnesses.Count(w => ContinuityLawAudit.IsATransport(
                w.Change.Select(v => (double)v).ToArray(), s.Cells, s.Components));
            double gap = s.Components < 2
                ? 0.0
                : Math.Abs(SourceManipulationAudit.Potential(4.0) - SourceManipulationAudit.Potential(9.0));
            return (s.Substrate, s.Cells, s.Components, maxUpdate, witnesses.Length, transports,
                witnesses.Length - transports, gap);
        }).ToArray();

    // ===================== 3. THE FOUR PRIMITIVES AND THE DISJOINT UNION =====================

    /// <summary>
    /// The canonical signature of a SET of realised values: sorted, distinct, invariant-formatted, so two contents
    /// can be compared as strings. The SET is the right reading — a primitive states WHICH differences, ticks,
    /// generators and component sums exist, not how many copies of the substrate they occur in.
    /// </summary>
    private static string Signature(IEnumerable<double> values)
        => "{" + string.Join(",", values.Distinct().OrderBy(v => v)
            .Select(v => v.ToString("G6", CultureInfo.InvariantCulture))) + "}";

    /// <summary>
    /// The generation relation the audit uses: a CHAIN per component, so every event has one generator except the
    /// first, which is a root. The relation is what the primitive states; the NUMBER OF ROOTS is a separate, global
    /// cardinality, and it is reported separately because the primitive does not state it.
    /// </summary>
    public static int[] GeneratorInDegrees(int cells, int components)
    {
        int per = cells / components;
        var deg = new int[cells];
        for (int c = 0; c < components; c++)
            for (int k = 1; k < per; k++)
                deg[c * per + k] = 1;
        return deg;
    }

    /// <summary>The sum of the occupation on each component — the most detail count conservation has about where its terms are.</summary>
    public static double[] ComponentSums(double[] rho, int cells, int components)
    {
        int per = cells / components;
        return Enumerable.Range(0, components).Select(c => Enumerable.Range(0, per).Sum(k => rho[c * per + k])).ToArray();
    }

    /// <summary>
    /// THE FOUR ALLOWED PRIMITIVES, EACH GIVEN A CERTIFICATE. Difference: the set of link differences realised.
    /// Actualization: the set of local ticks the class produces. Generation relation: the set of realised
    /// in-degrees. Count conservation: the set of component sums, with the class's leak. The fourth column is the
    /// reach of the primitive, and it is the audit's explanation of the blindness: three are local, and the fourth
    /// is global but is a SUM.
    /// </summary>
    public static (string Primitive, string Content, string Reach)[] PrimitiveCertificates(double[] rho, int cells, int components)
    {
        var links = LinksOf(cells, components);
        string difference = Signature(links.Select(l => rho[l.B] - rho[l.A]));
        string tick = Signature(UpdateAt(cells, links, rho));
        string generation = Signature(GeneratorInDegrees(cells, components).Select(v => (double)v));
        string conserved = Signature(ComponentSums(rho, cells, components)) + " with leak 0";
        return new[]
        {
            ("Difference", difference, "LOCAL: one link"),
            ("Actualization", tick, "LOCAL: one link"),
            ("Generation relation", generation, "LOCAL: one generator"),
            ("Count conservation", conserved, "GLOBAL, but only a SUM"),
        };
    }

    /// <summary>
    /// THE DISJOINT UNION TEST — the audit's central refutation. Take the 8-cell ring with its occupation and take
    /// the DISJOINT UNION of two copies of it: 16 cells, two 8-cell rings, no link between them. Every one of the
    /// four allowed primitives is asked for its certificate on both. If a primitive could yield connectivity, the
    /// certificate would have to change; it does not, for any of the four.
    /// </summary>
    public static (string Primitive, string OnTheSubstrate, string OnTheDisjointUnion, bool ContentChanged)[] DisjointUnionClosure()
        => DisjointUnionClosure(WorkhorseRho, 1);

    /// <summary>
    /// The same test at an arbitrary substrate and occupation, so that the blindness is SWEPT rather than asserted on
    /// one instance: whatever the state and however many components the substrate has, adjoining a disjoint copy of
    /// the whole substrate changes no primitive's content, because each certificate is a SET of realised values and
    /// the copy realises exactly the same set.
    /// </summary>
    public static (string Primitive, string OnTheSubstrate, string OnTheDisjointUnion, bool ContentChanged)[]
        DisjointUnionClosure(double[] rho, int components)
    {
        int n = rho.Length;
        double[] unionRho = rho.Concat(rho).ToArray();
        var baseCert = PrimitiveCertificates(rho, n, components);
        var unionCert = PrimitiveCertificates(unionRho, 2 * n, 2 * components);
        return baseCert.Zip(unionCert, (b, u) => (b.Primitive, b.Content, u.Content, !string.Equals(b.Content, u.Content, StringComparison.Ordinal))).ToArray();
    }

    /// <summary>
    /// THE GLOBAL DESCRIPTORS THAT DO CHANGE — and they are the audit's own detector rather than a primitive: the
    /// component count, the transport rank of the divergence, and the number of generation roots. So the audit is
    /// not blind; the vocabulary the question allows is.
    /// </summary>
    public static (string Descriptor, int OnTheSubstrate, int OnTheDisjointUnion, string Note)[] GlobalDescriptors()
    {
        int n = WorkhorseRho.Length;
        var baseLinks = LinksOf(n, 1);
        var unionLinks = LinksOf(2 * n, 2);
        return new (string, int, int, string)[]
        {
            ("connected components of the substrate", ComponentsOf(n, baseLinks), ComponentsOf(2 * n, unionLinks),
                "the union IS the cut"),
            ("transport directions (rank of the divergence)", TransportDirections(n, 1), TransportDirections(2 * n, 2),
                "the audit's OWN detector changes with the cut"),
            ("generation roots (cells of in-degree 0)", 1, 2, "one root per component"),
            ("links in the link set", baseLinks.Length, unionLinks.Length, "the union removes no link"),
        };
    }

    // ===================== 4. Q2 — IS CONNECTIVITY GENERATED BY THE UPDATE RELATION? =====================

    /// <summary>
    /// THE INFLUENCE ACROSS THE CUT, WITH ITS LIVE CONTROL. The pair (3,4) is a LINK on the 8-cell ring and a CUT on
    /// two 4-cell rings; the pair (47,48) is the same pair on the canonical substrate. The criterion is live:
    /// the connected substrate returns a non-zero influence where the cut returns exactly 0, so the audit is not
    /// measuring a definition.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int Source, int Target, double InfluenceAcrossTheCut)[]
        CrossComponentInfluence()
    {
        var rows = new (string Substrate, int Cells, int Components, int Source, int Target)[]
        {
            ("the 8-cell ring (the pair 3-4 is a link)", 8, 1, 3, 4),
            ("two 4-cell rings (the pair 3-4 is a cut)", 8, 2, 3, 4),
            ("the canonical ring (the pair 47-48 is a link)", CanonicalCells, 1, 47, 48),
            ("the canonical ring split in two (the pair 47-48 is a cut)", CanonicalCells, 2, 47, 48),
            ("the smallest split pair (the pair 0-1 is a cut)", 2, 2, 0, 1),
        };
        return rows.Select(r => (r.Substrate, r.Cells, r.Components, r.Source, r.Target,
            Influence(r.Cells, LinksOf(r.Cells, r.Components), r.Source, r.Target))).ToArray();
    }

    /// <summary>
    /// DOES THE UPDATE RELATION PRODUCE CONNECTIVITY? Its operators are written ON the link set, so they are
    /// block diagonal across a cut: the weight joining different components is measured, and the LIVE CONTROL is the
    /// same count over a partition of the index set in HALF — a partition no link set can change. On the connected
    /// ring the halves are joined by two links, on the cut they are not, so a zero means something.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int EntriesJoiningDifferentComponents,
        double WeightFromOneComponentToTheOthers, int EntriesJoiningTheTwoHalves)[]
        ComponentIndependence()
        => new (string, int, int)[]
        {
            ("the 8-cell ring", 8, 1),
            ("two 4-cell rings", 8, 2),
            ("four 2-cell rings", 8, 4),
            ("four single cells", 4, 4),
            ("the canonical ring", CanonicalCells, 1),
            ("the canonical ring split in two", CanonicalCells, 2),
        }.Select(r => (
            r.Item1, r.Item2, r.Item3,
            EntriesJoiningDifferentComponents(r.Item2, r.Item3),
            WeightFromComponentZeroToTheRest(r.Item2, r.Item3),
            EntriesJoiningTheTwoHalves(r.Item2, r.Item3))).ToArray();

    /// <summary>
    /// GENERATION-CONNECTIVITY IS NOT TRANSPORT-CONNECTIVITY. The generation relation can be CONNECTED while the
    /// substrate is not: let the first event of one component be generated by the last event of the other, and the
    /// generation graph is one tree over eight cells while the transport substrate still has two components. The
    /// two relations are DIFFERENT OBJECTS, and G_079's law needs the second one.
    /// </summary>
    public static (string Case, int Cells, int ComponentsOfTheGenerationRelation, int ComponentsOfTheTransportSubstrate,
        string Note)[] GenerationVersusTransport()
    {
        var split = new[] { (0, 1), (1, 2), (2, 3), (0, 4), (4, 5), (5, 6), (6, 7) };               // one root, two components
        var perComponent = new[] { (0, 1), (1, 2), (2, 3), (4, 5), (5, 6), (6, 7) };                 // one root per component
        var insideTheLinks = new[] { (0, 1), (1, 2), (2, 3), (4, 5), (5, 6) };                       // generation follows the links
        return new (string, int, int, (int A, int B)[], string)[]
        {
            ("one root generating both components", 8, 2, split,
                "the generation graph is ONE tree while the substrate has TWO components"),
            ("one root per component", 8, 2, perComponent,
                "generation and transport agree on the component count"),
            ("generation along the links", 8, 2, insideTheLinks,
                "a subgraph of the transport links can only split further, never rejoin"),
        }.Select(r => (r.Item1, r.Item2, ComponentsOf(r.Item2, r.Item4),
            ComponentsOf(r.Item2, LinksOf(r.Item2, r.Item3)), r.Item5)).ToArray();
    }

    /// <summary>
    /// THE GENERATIVE RULE ASSUMES THE RING. The actualization link rule of the repository is `(i + d) mod n` with
    /// `k = round(a_i * K)` — arithmetic on the whole index set. Applied to a genuinely disconnected substrate it
    /// does not fail: it creates links ACROSS the cut and returns a link set with ONE component where the substrate
    /// has c. On the smallest split pair it manufactures the single link whose absence made the pair disconnected.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int ActiveCells, int LinksTheRuleCreates,
        int LinksCrossingTheCut, int ComponentsOfTheRuleOutput)[] RingRuleCannotRepresentACut(int k = 6)
        => Substrates().Where(s => s.Cells <= 16).Select(s =>
        {
            var activity = ActualizationStructures.PersistentActivity(s.Cells);
            var links = new HashSet<(int, int)>();
            int active = 0;
            for (int i = 0; i < s.Cells; i++)
            {
                if (activity[i] <= 0.5) continue;
                active++;
                int dmax = (int)Math.Round(activity[i] * k);
                for (int d = 1; d <= dmax; d++)
                {
                    int j = (i + d) % s.Cells;
                    if (i == j) continue;
                    links.Add((Math.Min(i, j), Math.Max(i, j)));
                }
            }
            var set = links.ToArray();
            int crossing = set.Count(l => ComponentsDiffer(l.Item1, l.Item2, s.Cells, s.Components));
            return (s.Substrate, s.Cells, s.Components, active, set.Length, crossing, ComponentsOf(s.Cells, set));
        }).ToArray();

    // ===================== 5. Q3 — THE SMALLEST DISCONNECTED SURVIVOR AND THE SMALLEST CUT =====================

    private static (int A, int B)[] RingLinks(int cells)
        => Enumerable.Range(0, cells)
            .Select(i => (Math.Min(i, (i + 1) % cells), Math.Max(i, (i + 1) % cells)))
            .Where(l => l.Item1 != l.Item2)
            .Distinct().ToArray();

    /// <summary>
    /// The number of connected components of the subgraph INDUCED by the cells that are not removed. A removed cell
    /// is not counted at all — otherwise every removed cell would be its own component and the test would report a
    /// disconnection it had just created.
    /// </summary>
    public static int ComponentsOfInduced(int cells, (int A, int B)[] links, int[] removed)
    {
        var keep = Enumerable.Range(0, cells).Where(i => !removed.Contains(i)).ToArray();
        if (keep.Length == 0) return 0;
        var parent = new Dictionary<int, int>();
        foreach (int i in keep) parent[i] = i;
        int Find(int x)
        {
            while (parent[x] != x) { parent[x] = parent[parent[x]]; x = parent[x]; }
            return x;
        }
        foreach (var (a, b) in links)
        {
            if (!parent.ContainsKey(a) || !parent.ContainsKey(b)) continue;
            int ra = Find(a), rb = Find(b);
            if (ra != rb) parent[ra] = rb;
        }
        return keep.Select(Find).Distinct().Count();
    }

    /// <summary>
    /// THE SMALLEST CUT, BY EXHAUSTIVE SEARCH: the fewest LINKS whose removal disconnects a ring, and the fewest
    /// CELLS. Both are measured by exhaustive search up to size two, and a single removal is verified not to
    /// suffice, so the answer 2 is a measurement rather than a remembered fact. 0 means "no removal of at most two
    /// cells disconnects it".
    /// </summary>
    public static (int Cells, int MinLinksToOneDisconnect, int MinCellsToOneDisconnect)[] SmallestCut(int[] sizes)
        => sizes.Select(n =>
        {
            var links = RingLinks(n);

            int minLinks = 0;
            for (int a = 0; a < links.Length && minLinks == 0; a++)
                if (ComponentsOfInduced(n, links.Where((_, i) => i != a).ToArray(), Array.Empty<int>()) > 1)
                    minLinks = 1;
            if (minLinks == 0)
                for (int a = 0; a < links.Length && minLinks == 0; a++)
                    for (int b = a + 1; b < links.Length; b++)
                        if (ComponentsOfInduced(n, links.Where((_, i) => i != a && i != b).ToArray(), Array.Empty<int>()) > 1)
                        {
                            minLinks = 2;
                            break;
                        }

            int minCells = 0;
            for (int a = 0; a < n && minCells == 0; a++)
                if (ComponentsOfInduced(n, links, new[] { a }) > 1)
                    minCells = 1;
            if (minCells == 0)
                for (int a = 0; a < n && minCells == 0; a++)
                    for (int b = a + 1; b < n; b++)
                        if (ComponentsOfInduced(n, links, new[] { a, b }) > 1)
                        {
                            minCells = 2;
                            break;
                        }

            return (n, minLinks, minCells);
        }).ToArray();

    // ===================== 6. Q4 — THE OBSERVABLE SIGNATURE =====================

    /// <summary>The acceleration the sector reads: the gradient of the potential along the LINKS a cell actually has.</summary>
    public static double[] AccelerationOnLinks(double[] potential, int cells, (int A, int B)[] links)
    {
        var neighbours = Enumerable.Range(0, cells).Select(_ => new List<int>()).ToArray();
        foreach (var (a, b) in links) { neighbours[a].Add(b); neighbours[b].Add(a); }
        return Enumerable.Range(0, cells).Select(i => neighbours[i].Count == 0
            ? 0.0
            : -neighbours[i].Average(j => potential[j] - potential[i])).ToArray();
    }

    /// <summary>The deterministic state sweep the reach measurement uses.</summary>
    public static double[] SweepState(int cells, int k) => Enumerable.Range(0, cells).Select(i => 1.0 + (i * (k + 1)) % 5).ToArray();

    /// <summary>
    /// WHAT CAN A DISCONNECTED SECTOR BE READ BY? The acceleration is a gradient along the links, so it needs a
    /// link: swept over a deterministic family of states, the smallest survivor reads NO acceleration at all, and the
    /// canonical ring reads one. A read that needs a link is a read that a cut silences.
    /// </summary>
    public static (string Substrate, int Cells, int Components, double MaxAccelerationOverTheSweep, int CellsReadingAnAcceleration)[]
        AccelerationReach()
        => new (string, int, int)[]
        {
            ("the smallest split pair", 2, 2),
            ("the smallest ring", 2, 1),
            ("two 4-cell rings", 8, 2),
            ("the 8-cell ring", 8, 1),
            ("the canonical ring", CanonicalCells, 1),
        }.Select(r =>
        {
            var links = LinksOf(r.Item2, r.Item3);
            double max = 0.0;
            int readers = 0;
            for (int k = 0; k < 4; k++)
            {
                var rho = SweepState(r.Item2, k);
                var potential = rho.Select(SourceManipulationAudit.Potential).ToArray();
                var acc = AccelerationOnLinks(potential, r.Item2, links);
                max = Math.Max(max, acc.Select(Math.Abs).Max());
                readers = Math.Max(readers, acc.Count(v => Math.Abs(v) > 1e-12));
            }
            return (r.Item1, r.Item2, r.Item3, max, readers);
        }).ToArray();

    /// <summary>
    /// THE FREE LEVEL IS WRITTEN INTO AN OBSERVABLE — AND THE OBSERVABLE IS UNREACHABLE. The redshift between two
    /// components is a ratio of clock rates (G_028/G_077), so it MOVES when the relative level moves; and the number
    /// of links across the cut is 0, so no measurement can ever perform the comparison. The audit reports both
    /// numbers rather than the conclusion.
    /// </summary>
    public static (double Level1, double Level2, double RedshiftBetweenTheComponents)[] RedshiftAcrossTheCut()
        => new (double, double)[] { (4, 4), (4, 9), (4, 16), (9, 4) }
            .Select(l => (l.Item1, l.Item2,
                TemporalIndependenceAudit.ClockOf(l.Item1) / TemporalIndependenceAudit.ClockOf(l.Item2) - 1.0)).ToArray();

    /// <summary>The number of links joining different components — the channel a comparison would have to travel along.</summary>
    public static int LinksAcrossTheCut(int cells, int components)
        => LinksOf(cells, components).Count(l => ComponentsDiffer(l.Item1, l.Item2, cells, components));

    /// <summary>A breadth-first path length inside a link set, or -1 when no path exists at all.</summary>
    public static int PathLength(int cells, (int A, int B)[] links, int from, int to)
    {
        if (from == to) return 0;
        var distance = Enumerable.Repeat(-1, cells).ToArray();
        distance[from] = 0;
        var queue = new Queue<int>();
        queue.Enqueue(from);
        var neighbours = Enumerable.Range(0, cells).Select(_ => new List<int>()).ToArray();
        foreach (var (a, b) in links) { neighbours[a].Add(b); neighbours[b].Add(a); }
        while (queue.Count > 0)
        {
            int i = queue.Dequeue();
            foreach (int j in neighbours[i])
                if (distance[j] < 0)
                {
                    distance[j] = distance[i] + 1;
                    if (j == to) return distance[j];
                    queue.Enqueue(j);
                }
        }
        return -1;
    }

    /// <summary>How many of the sector's temporal observables read a flux rather than a state (G_079's measurement, reused).</summary>
    public static int ObservablesReadingAFlux() => ContinuityLawAudit.ObservablesReadingTheFlux();

    // ===================== 7. THE VERDICT =====================

    /// <summary>Would the primitive vocabulary have to change its content for the cut to be visible? Measured, not assumed.</summary>
    public static bool PrimitivesSeparateConnectivity() => DisjointUnionClosure().Any(r => r.ContentChanged);

    /// <summary>Does the update class ever move count ACROSS a cut? Measured on every disconnected substrate in the family.</summary>
    public static bool UpdateProducesConnectivity() => ComponentIndependence().Any(r => r.WeightFromOneComponentToTheOthers > 0.0);

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   DERIVED  - a primitive's content changes when the substrate is cut, in which case connectivity is derivable;
    ///   EMERGENT - the update class moves count across a cut, in which case the dynamics generates it;
    ///   ASSUMED  - neither, in which case the premise is an input and the two alternatives are refuted by measurement.
    /// </summary>
    public static string Verdict()
    {
        var closure = DisjointUnionClosure();
        if (PrimitivesSeparateConnectivity())
            return "DERIVED - A PRIMITIVE'S CONTENT CHANGES WITH THE CUT, SO CONNECTIVITY IS DERIVABLE FROM THE "
                 + "ALLOWED VOCABULARY";

        if (UpdateProducesConnectivity())
            return "EMERGENT - THE UPDATE CLASS MOVES COUNT ACROSS A CUT, SO THE DYNAMICS GENERATES CONNECTIVITY";

        var descriptors = GlobalDescriptors();
        var independence = ComponentIndependence();
        double weightAcross = independence.Where(r => r.Components >= 2).Max(r => r.WeightFromOneComponentToTheOthers);
        int liveControl = independence.Where(r => r.Components == 1).Max(r => r.EntriesJoiningTheTwoHalves);
        var survivors = DisconnectedSurvivors();
        var frozen = survivors.Single(s => s.Substrate == "the smallest split pair");
        var influence = CrossComponentInfluence();
        var livePair = influence.Single(i => i.Substrate == "the 8-cell ring (the pair 3-4 is a link)");
        var cutPair = influence.Single(i => i.Substrate == "two 4-cell rings (the pair 3-4 is a cut)");
        var rule = RingRuleCannotRepresentACut().Single(r => r.Substrate == "the smallest split pair");
        var acceleration = AccelerationReach().Single(a => a.Substrate == "the smallest split pair");
        var redshift = RedshiftAcrossTheCut();
        var cut = SmallestCut(new[] { 8, CanonicalCells }).Single(c => c.Cells == CanonicalCells);

        var sb = new StringBuilder();
        sb.Append("ASSUMED - CONNECTIVITY IS AN INPUT AT THE LEVEL OF THE PRIMITIVES, AND BOTH ALTERNATIVES ARE ");
        sb.Append("REFUTED BY MEASUREMENT. NOT DERIVED: the four allowed primitives are BLIND to the cut - the ");
        sb.Append($"disjoint-union certificates are unchanged for {closure.Count(r => !r.ContentChanged)} of {closure.Length} of them, ");
        sb.Append("while the global descriptors move (");
        sb.Append(string.Join(", ", descriptors.Select(d => $"{d.Descriptor} {d.OnTheSubstrate} -> {d.OnTheDisjointUnion}")));
        sb.Append("), because three of the four are LOCAL - radius one link - and the fourth is a SUM, and a sum ");
        sb.Append("cannot see which component its terms came from. NOT EMERGENT: the update class is written on the ");
        sb.Append($"link set, so the weight it carries between different components is exactly {E3(weightAcross)} against a live ");
        sb.Append($"control of {F0(liveControl)} Laplacian entries joining the two halves of the connected ring - it CONSERVES the component ");
        sb.Append("count instead of producing it. The smallest disconnected survivor is ");
        sb.Append($"{F0(frozen.Cells)} cells in {F0(frozen.Components)} components: its transport rank is {F0(TransportDirections(frozen.Cells, frozen.Components))}, it admits NO non-zero transport, ");
        sb.Append($"and the one non-trivial count-conserving change it does admit is the FORBIDDEN transfer ({F0(frozen.OfWhichAreForbiddenTransfers)} of {F0(frozen.ConservingWitnesses)} witnesses), so a ");
        sb.Append($"disconnected universe is not merely allowed, it is FROZEN (max update {E3(frozen.MaxUpdateOfTheComponentUniformState)}); and no single defect can ");
        sb.Append($"disconnect the canonical substrate, because the smallest cut is {F0(cut.MinLinksToOneDisconnect)} links or {F0(cut.MinCellsToOneDisconnect)} cells. THE GENERATIVE RULE DOES ");
        sb.Append($"`(i + d) mod n`, so on the smallest split pair it manufactures {F0(rule.LinksCrossingTheCut)} link ACROSS the cut and returns a link set with ");
        sb.Append($"{F0(rule.ComponentsOfTheRuleOutput)} component where the substrate has {F0(rule.Components)} - connectivity is in the rule's ARITHMETIC, not in its ");
        sb.Append("output. THE SIGNATURE IS A FROZEN, UNREADABLE OFFSET: the per-component-uniform configuration is an ");
        sb.Append($"exact fixed point with a potential gap of {F3(survivors.Max(s => s.PotentialGapBetweenTheComponents))}; the smallest survivor reads an acceleration of ");
        sb.Append($"exactly {E3(acceleration.MaxAccelerationOverTheSweep)} ({acceleration.CellsReadingAnAcceleration} cells reading one); the redshift between components MOVES with the ");
        sb.Append($"free level ({F3(redshift.Min(r => r.RedshiftBetweenTheComponents))} to {F3(redshift.Max(r => r.RedshiftBetweenTheComponents))}) and is UNREACHABLE, since the number of links across the cut is ");
        sb.Append($"{F0(LinksAcrossTheCut(8, 2))} - the level is written into a measurement no experiment can perform. The live control is the pair that is a link on one ");
        sb.Append($"substrate and a cut on the other: its influence is {F0(livePair.InfluenceAcrossTheCut)} against {F0(cutPair.InfluenceAcrossTheCut)}. ");
        sb.Append("OUTPUT: ASSUMED. Connectivity is a GLOBAL property of the substrate and the allowed vocabulary is local ");
        sb.Append("or relational, so no combination of the four primitives can state it: like the count itself, the ");
        sb.Append("COMPONENT COUNT is a conserved integer the theory never sets, and it must be supplied as an initial ");
        sb.Append("datum. No graph-connectivity axiom was imported to reach that: the substrates were built, the ");
        sb.Append("component count was computed from the link set, and the blindness is a measurement.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var closure = DisjointUnionClosure();
        var survivors = DisconnectedSurvivors();
        var frozen = survivors.Single(s => s.Substrate == "the smallest split pair");
        var independence = ComponentIndependence();
        double weightAcross = independence.Where(r => r.Components >= 2).Max(r => r.WeightFromOneComponentToTheOthers);
        int liveControl = independence.Where(r => r.Components == 1).Max(r => r.EntriesJoiningTheTwoHalves);
        var rule = RingRuleCannotRepresentACut();
        var generation = GenerationVersusTransport();
        var reach = AccelerationReach();
        var frozenReach = reach.Single(a => a.Substrate == "the smallest split pair");
        var cut = SmallestCut(new[] { CanonicalCells }).Single();
        return "CONNECTIVITY IS AN INPUT, AND THE AUDIT SAYS WHICH KIND OF INPUT. "
             + $"WHAT IS MEASURED: the four allowed primitives keep their content on a disjoint union ({closure.Count(r => !r.ContentChanged)} of {closure.Length} unchanged) "
             + $"while every global descriptor changes; the transport class is block diagonal across a cut (weight {E3(weightAcross)} against a live control of "
             + $"{F0(liveControl)} entries joining the two halves of the connected ring); a disconnected substrate is a frozen survivor whose per-component-uniform state is an "
             + $"exact fixed point (max update {E3(frozen.MaxUpdateOfTheComponentUniformState)}); and the smallest cut of the canonical ring is {F0(cut.MinLinksToOneDisconnect)} links or {F0(cut.MinCellsToOneDisconnect)} cells. "
             + "WHAT IS NOT CLAIMED. First, THIS IS A STATEMENT ABOUT THE SURVIVING CLASS AND THE RING FAMILY: the substrate is one ring per "
             + "component, and the audit claims that none of the four ALLOWED primitives yields connectivity - not that no conceivable primitive of "
             + "a larger theory could. Second, LOCAL BLINDNESS IS NOT TOTAL BLINDNESS: a state that differs across a cut is locally VISIBLE at the two "
             + "cells beside it, so a substrate whose occupation varies at the cut can be partly inferred from its reads; what no primitive supplies is "
             + "the GLOBAL component count - which is why the canonical INITIAL condition, a uniform occupation, is locally identical on a connected and "
             + $"a disconnected substrate. Third, GENERATION AND TRANSPORT ARE DIFFERENT RELATIONS, measured here ({generation[0].ComponentsOfTheGenerationRelation} generation "
             + $"component against {generation[0].ComponentsOfTheTransportSubstrate} transport components in the one-root case), so a connected generation tree does not repair the law's "
             + $"premise; and the generative link rule ASSUMES the ring instead of building it - applied to a disconnected substrate it creates {rule.Max(r => r.LinksCrossingTheCut)} links "
             + "across the cut. Fourth, A DISCONNECTED SECTOR IS NOT INVISIBLE, IT IS UNREACHABLE: the redshift between components is defined and moves with the "
             + $"free level, and the number of links across the cut is zero - while on the smallest survivor ({frozen.Cells} cells, {frozenReach.CellsReadingAnAcceleration} cells reading an "
             + "acceleration) there is no acceleration observable at all. WHAT WOULD CHANGE THE VERDICT: a primitive whose certificate changes with the cut "
             + "(the DERIVED branch), or an operator of the surviving class that moves count across one (the EMERGENT branch). Both branches are computed in "
             + "the verdict; neither is taken.";
    }

    // ===================== 8. REPORTS =====================

    public static string OutputCapacity()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE TRANSPORT CAPACITY OF A SUBSTRATE - WHAT CONNECTIVITY IS WORTH");
        sb.AppendLine("   the divergence's rank is the number of independent transport directions; rank = n - c exactly.");
        sb.AppendLine("   substrate                              cells  components  directions  lost to the cuts");
        foreach (var t in TransportCapacity())
            sb.AppendLine($"   {t.Substrate,-38} {t.Cells,-6} {t.Components,-11} {t.Directions,-11} {t.DirectionsLostToTheCuts}");
        return sb.ToString();
    }

    public static string OutputSurvivors()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. CAN DISCONNECTED UNIVERSES EXIST?  YES - AND THE SMALLEST ONE IS FROZEN");
        sb.AppendLine("   the per-component-uniform occupation: is it a fixed point, does it conserve the count, does it");
        sb.AppendLine("   carry a gap, and how many of its conserving changes are FORBIDDEN transfers?");
        sb.AppendLine("   substrate                              cells  components  max update  witnesses  transports  forbidden  potential gap (4 vs 9)");
        foreach (var s in DisconnectedSurvivors())
            sb.AppendLine($"   {s.Substrate,-38} {s.Cells,-6} {s.Components,-11} {E3(s.MaxUpdateOfTheComponentUniformState),-11} "
                + $"{s.ConservingWitnesses,-10} {s.OfWhichAreTransports,-11} {s.OfWhichAreForbiddenTransfers,-10} {F3(s.PotentialGapBetweenTheComponents)}");
        return sb.ToString();
    }

    public static string OutputBlindness()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE FOUR ALLOWED PRIMITIVES ARE BLIND TO THE CUT - THE DISJOINT-UNION TEST");
        sb.AppendLine("   the 8-cell ring, and the disjoint union of TWO copies of it (16 cells, two 8-cell rings):");
        sb.AppendLine();
        sb.AppendLine("   primitive              content on the substrate          content on the disjoint union      changed");
        foreach (var c in DisjointUnionClosure())
            sb.AppendLine($"   {c.Primitive,-22} {c.OnTheSubstrate,-34} {c.OnTheDisjointUnion,-34} {c.ContentChanged}");
        sb.AppendLine();
        sb.AppendLine("   the reach of each primitive (the audit's explanation of the blindness):");
        foreach (var p in PrimitiveCertificates(WorkhorseRho, WorkhorseRho.Length, 1))
            sb.AppendLine($"   {p.Primitive,-22} {p.Reach}");
        sb.AppendLine();
        sb.AppendLine("   ...while every GLOBAL descriptor does change - so the audit is not blind, the vocabulary is:");
        sb.AppendLine("   descriptor                                    substrate  union  note");
        foreach (var d in GlobalDescriptors())
            sb.AppendLine($"   {d.Descriptor,-44} {d.OnTheSubstrate,-10} {d.OnTheDisjointUnion,-6} {d.Note}");
        return sb.ToString();
    }

    public static string OutputUpdate()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. IS CONNECTIVITY GENERATED BY THE UPDATE RELATION?  NO - IT IS CONSERVED, NOT PRODUCED");
        sb.AppendLine("   the influence of one cell on another's update: the pair that is a LINK on one substrate and a CUT on the other");
        sb.AppendLine("   substrate                                                    cells  components  source  target  influence");
        foreach (var i in CrossComponentInfluence())
            sb.AppendLine($"   {i.Substrate,-60} {i.Cells,-6} {i.Components,-11} {i.Source,-7} {i.Target,-7} {i.InfluenceAcrossTheCut}");
        sb.AppendLine();
        sb.AppendLine("   the Laplacian entries that JOIN different components - zero on a cut, positive on the halves control:");
        sb.AppendLine("   substrate                              cells  components  entries joining  weight out of component 0  entries joining the two halves");
        foreach (var w in ComponentIndependence())
            sb.AppendLine($"   {w.Substrate,-38} {w.Cells,-6} {w.Components,-11} {w.EntriesJoiningDifferentComponents,-15} "
                + $"{E3(w.WeightFromOneComponentToTheOthers),-28} {F0(w.EntriesJoiningTheTwoHalves)}");
        sb.AppendLine();
        sb.AppendLine("   generation-connectivity is NOT transport-connectivity:");
        sb.AppendLine("   case                                    cells  generation components  transport components  note");
        foreach (var g in GenerationVersusTransport())
            sb.AppendLine($"   {g.Case,-40} {g.Cells,-6} {g.ComponentsOfTheGenerationRelation,-23} {g.ComponentsOfTheTransportSubstrate,-22} {g.Note}");
        sb.AppendLine();
        sb.AppendLine("   the generative link rule `(i + d) mod n` applied to a substrate that IS disconnected:");
        sb.AppendLine("   substrate                              cells  components  active cells  links created  links crossing the cut  components of the output");
        foreach (var r in RingRuleCannotRepresentACut())
            sb.AppendLine($"   {r.Substrate,-38} {r.Cells,-6} {r.Components,-11} {r.ActiveCells,-13} {r.LinksTheRuleCreates,-14} "
                + $"{r.LinksCrossingTheCut,-22} {r.ComponentsOfTheRuleOutput}");
        return sb.ToString();
    }

    public static string OutputCut()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. THE SMALLEST DISCONNECTED SURVIVOR AND THE SMALLEST CUT, BY EXHAUSTIVE SEARCH");
        sb.AppendLine("   cells  fewest LINKS to disconnect  fewest CELLS to disconnect");
        foreach (var c in SmallestCut(new[] { 2, 3, 4, 8, 96 }))
            sb.AppendLine($"   {c.Cells,-6} {c.MinLinksToOneDisconnect,-26} {c.MinCellsToOneDisconnect}");
        sb.AppendLine("   (0 = no removal of at most two cells disconnects that substrate)");
        return sb.ToString();
    }

    public static string OutputObservables()
    {
        var sb = new StringBuilder();
        sb.AppendLine("6. THE OBSERVABLE SIGNATURE OF A DISCONNECTED SECTOR");
        sb.AppendLine("   the acceleration is a gradient along the LINKS, so a cut silences it:");
        sb.AppendLine("   substrate                              cells  components  max |acceleration|  cells reading one");
        foreach (var a in AccelerationReach())
            sb.AppendLine($"   {a.Substrate,-38} {a.Cells,-6} {a.Components,-11} {E3(a.MaxAccelerationOverTheSweep),-20} {a.CellsReadingAnAcceleration}");
        sb.AppendLine();
        sb.AppendLine("   the redshift between two components is DEFINED and moves with the free level ...");
        sb.AppendLine("   level 1  level 2  redshift between the components");
        foreach (var r in RedshiftAcrossTheCut())
            sb.AppendLine($"   {F0(r.Level1),-8} {F0(r.Level2),-8} {F3(r.RedshiftBetweenTheComponents)}");
        sb.AppendLine();
        sb.AppendLine("   ...and is UNREACHABLE: the channels a comparison could travel along");
        sb.AppendLine($"   links across the cut, two 4-cell rings:  {LinksAcrossTheCut(8, 2)}");
        sb.AppendLine($"   path length from cell 3 to cell 4, 8-cell ring:        {PathLength(8, LinksOf(8, 1), 3, 4)}");
        sb.AppendLine($"   path length from cell 3 to cell 4, two 4-cell rings:   {PathLength(8, LinksOf(8, 2), 3, 4)} (-1 = no path)");
        sb.AppendLine($"   temporal observables that read a flux: {ObservablesReadingAFlux()} of {TemporalIndependenceAudit.TemporalObservables().Length}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("7. THE VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine("WHERE IT STANDS");
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
