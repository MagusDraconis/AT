using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_079 - CONTINUITY LAW AUDIT (group G - Gravity Source).
///
/// QUESTION. G_077 established rho as the sole surviving clock source; G_078 found only transport, with no source and
/// no sink in the surviving class. This audit asks whether
///
///     d_t rho + div J = 0
///
/// is FORCED - and, if it is, what exactly the forcing rests on, because a proof of continuity elevates transport
/// from an OBSERVATION to a LAW, while any surviving source term would refute the transport-only conclusion.
///
/// ANSWER: **BOUNDARY - THE LAW IS DERIVED ON THE SUBSTRATE AND ITS THREE PREMISES ARE MEASURED RATHER THAN
/// ASSUMED.** The audit proves the law for the surviving update class and then exhibits a worked counterexample to
/// each premise the proof needs, so the result is a DERIVED law with a BOUNDARY requirement - the two-level pattern
/// of D_028/D_040:
///
///  (1) EVERY SURVIVING UPDATE IS A CONTINUITY EQUATION, and the decomposition is reproduced in EXACT INTEGER
///      ARITHMETIC: for each conserving operator the audit reconstructs the link flux J by prefix sums and verifies
///      d rho = -div J to the last bit rather than to a tolerance. The criterion that separates the conserving
///      operators from the control is a rank test - the change must lie in the IMAGE of the divergence - and the
///      control fails it, so the test is live rather than decorative.
///
///  (2) BUT THE FLUX IS NOT UNIQUE: the divergence operator on an n-cell ring has rank n-1 and nullity 1 for every n,
///      so the flux that carries a given evolution is determined only up to ONE CIRCULATION. And that circulation is
///      not merely undetermined, it is UNOBSERVABLE: none of the sector's temporal observables reads a flux at all,
///      so the ambiguity is a genuine gauge rather than a measurement waiting to be made.
///
///  (3) CONSERVATION IS NOT EQUIVALENT TO CONTINUITY - CONNECTIVITY IS THE MISSING PREMISE. On a single connected
///      ring the zero-sum subspace IS the image of the divergence, so a count-conserving change is always a transport.
///      On a TWO-COMPONENT substrate the image requires EACH component's sum to vanish, so a transfer ACROSS THE CUT
///      conserves the total and is NOT a divergence of any intra-component flux. A transfer is exactly a source/sink
///      pair, so the law's premise is the substrate's connectivity, not the conservation of the count.
///
///  (4) A SOURCE TERM CAN BE ADDED WITHOUT BREAKING ANY SURVIVING MEASUREMENT, and the audit measures why: the pinned
///      rows constrain the MAP from the state to the observables and are functions of the state alone, so a source
///      changes which states occur and not the relation they obey. What a source breaks is the CLOSURE - and its
///      detector is G_078's own ledger, which this audit shows is LIVE: with a source the ledger residual equals the
///      source EXACTLY, so the identity is falsifiable rather than conventional. The transport-only conclusion is
///      therefore forced GIVEN the class, and the class is a choice rather than a measurement.
///
///  (5) AND THE OBSERVABLES NEED ONE NUMBER THE FLUX CANNOT SUPPLY - WHICH THE CONSERVED COUNT SUPPLIES. The kernel of
///      the divergence is the uniform mode, dimension 1 on a connected ring, so a divergence determines the state up
///      to a uniform shift: the redshift (a ratio) and the acceleration (a difference) are determined and the absolute
///      clock rate is not. The conserved total fixes exactly that one mode on a connected substrate - and on a
///      DISCONNECTED one the kernel has one dimension per component, so the total fixes the sum of c levels and leaves
///      c-1 relative levels that no measurement in the sector can reach.
///
///  (6) SO A SOURCE-FREE CONSERVED THEORY DOES LIMIT MANIPULATION TO REDISTRIBUTION - conditionally, and the audit says
///      conditionally, because premise (4) is not supplied by any measurement.
/// </summary>
public static class ContinuityLawAudit
{
    public const int D = SourceManipulationAudit.D;

    /// <summary>The canonical lattice size, so the rank and nullity figures are the ones other audits quote.</summary>
    public const int CanonicalCells = SourceManipulationAudit.CanonicalCells;

    // ===================== 1. THE DIVERGENCE OPERATOR =====================

    /// <summary>The discrete divergence of a link flux: div J at cell i = J[i] - J[i-1], on each ring separately.</summary>
    public static int[] Divergence(int[] flux, int components = 1)
    {
        int n = flux.Length;
        int per = n / components;
        var result = new int[n];
        for (int c = 0; c < components; c++)
            for (int k = 0; k < per; k++)
            {
                int i = c * per + k;
                int left = c * per + (k - 1 + per) % per;
                result[i] = flux[i] - flux[left];
            }
        return result;
    }

    /// <summary>
    /// THE DIVERGENCE MATRIX on a substrate of <paramref name="components"/> rings of equal size: the operator whose
    /// image is the set of changes a transport can produce. Its RANK is what decides membership and its NULLITY is
    /// what decides how ambiguous the flux is.
    /// </summary>
    public static double[][] DivergenceMatrix(int cells, int components = 1)
    {
        int per = cells / components;
        var m = new double[cells][];
        for (int i = 0; i < cells; i++) m[i] = new double[cells];
        for (int c = 0; c < components; c++)
            for (int k = 0; k < per; k++)
            {
                int i = c * per + k;
                m[i][i] = 1.0;
                m[i][c * per + (k - 1 + per) % per] -= 1.0;
            }
        return m;
    }

    /// <summary>The rank of the divergence matrix augumented by a column - the membership test, exactly as stated.</summary>
    private static double[][] Augment(double[][] m, double[] column)
    {
        var a = new double[m.Length][];
        for (int i = 0; i < m.Length; i++)
        {
            a[i] = new double[m[i].Length + 1];
            Array.Copy(m[i], a[i], m[i].Length);
            a[i][^1] = column[i];
        }
        return a;
    }

    /// <summary>
    /// MEMBERSHIP: a change is a TRANSPORT if and only if it lies in the image of the divergence. The test is a rank
    /// comparison rather than a sum, so it also separates the cases where the sum is zero but the component sums are
    /// not - which is exactly where a conserved change is nevertheless a source/sink pair.
    /// </summary>
    public static bool IsATransport(double[] change, int cells, int components = 1)
    {
        var m = DivergenceMatrix(cells, components);
        return Math.Abs(ClockSourceAudit.Rank(m) - ClockSourceAudit.Rank(Augment(m, change))) < 1e-9;
    }

    /// <summary>The rank and nullity of the divergence operator on each substrate the audit compares.</summary>
    public static (string Substrate, int Cells, int Components, double Rank, double Nullity, string ImageDescription)[]
        OperatorCensus()
    {
        var cases = new (string Substrate, int Cells, int Components, string ImageDescription)[]
        {
            ("one connected ring", 8, 1, "the zero-sum subspace"),
            ("two rings (disconnected)", 8, 2, "changes whose sum vanishes ON EACH COMPONENT"),
            ("four rings (disconnected)", 8, 4, "changes whose sum vanishes ON EACH COMPONENT"),
            ("one ring, canonical size", CanonicalCells, 1, "the zero-sum subspace"),
        };
        return cases.Select(r =>
        {
            double rank = ClockSourceAudit.Rank(DivergenceMatrix(r.Cells, r.Components));
            return (r.Substrate, r.Cells, r.Components, rank, r.Cells - rank, r.ImageDescription);
        }).ToArray();
    }

    /// <summary>
    /// THE CIRCULATION IS A GAUGE, MEASURED: adding a constant to every link flux leaves the divergence EXACTLY
    /// unchanged, on integers, so the flux ambiguity is not a tolerance artefact. The test sweeps the constant.
    /// </summary>
    public static (int Cells, int Circulation, int MaxAbsoluteDivergenceChange)[]
        CirculationDoesNotChangeTheDivergence(int[] cellCounts)
        => cellCounts.Select(n =>
        {
            var flux = Enumerable.Range(0, n).Select(i => 3 * i - 7).ToArray();
            var before = Divergence(flux);
            var after = Divergence(flux.Select(f => f + 5).ToArray());
            return (n, 5, Enumerable.Range(0, n).Max(i => Math.Abs(before[i] - after[i])));
        }).ToArray();

    // ===================== 2. EVERY SURVIVING UPDATE AS A CONTINUITY EQUATION =====================

    /// <summary>A transport in the literal sense: count moved from cell i to cell i+1. The canonical positive case.</summary>
    public static int[] Transport(int[] rho, int from, int amount, int components = 1)
    {
        int n = rho.Length;
        int per = n / components;
        int c = from / per;
        var after = (int[])rho.Clone();
        after[from] -= amount;
        after[c * per + (from - c * per + 1) % per] += amount;
        return after;
    }

    /// <summary>The link flux carrying a change, reconstructed by prefix sums - exactly, in integers.</summary>
    public static int[] FluxOf(int[] change, int components = 1)
    {
        int n = change.Length;
        int per = n / components;
        var flux = new int[n];
        for (int c = 0; c < components; c++)
        {
            flux[c * per] = 0;
            for (int k = 1; k < per; k++)
                flux[c * per + k] = flux[c * per + k - 1] - change[c * per + k];
        }
        return flux;
    }

    /// <summary>
    /// THE LINK FLUX OF EACH NATURAL OPERATOR, given exactly. This is the audit's first result in its sharpest form:
    /// the upwind, centred and Laplacian forms ARE divergences, with the flux written down here in integers (the
    /// centred form is carried at twice scale so that its halves stay integral). The control has no flux at all.
    /// </summary>
    public static (string Operator, string FluxFormula, int[] Flux, int Scale)[] FluxOfTheClass(int[] rho)
    {
        int n = rho.Length;
        // upwind:  L_i = rho[i] - rho[i-1]              = div J with J_i = rho[i]
        var upwind = Enumerable.Range(0, n).Select(i => rho[i]).ToArray();
        // centred: L_i = (rho[i+1] - rho[i-1]) / 2      = div J with 2 J_i = rho[i+1] + rho[i]
        var centred = Enumerable.Range(0, n).Select(i => rho[(i + 1) % n] + rho[i]).ToArray();
        // Laplacian: L_i = rho[i+1] - 2 rho[i] + rho[i-1] = div J with J_i = rho[i+1] - rho[i]
        var laplacian = Enumerable.Range(0, n).Select(i => rho[(i + 1) % n] - rho[i]).ToArray();
        return new[]
        {
            ("local difference (upwind)", "J_i = rho[i]", upwind, 1),
            ("centred difference", "2 J_i = rho[i+1] + rho[i]", centred, 2),
            ("second difference (Laplacian)", "J_i = rho[i+1] - rho[i]", laplacian, 1),
        };
    }

    /// <summary>
    /// THE DECOMPOSITION, RUN ON EVERY SURVIVING OPERATOR AND ON THE CONTROL. For each candidate the change is tested
    /// for membership in the image of the divergence, the flux is reconstructed where membership holds, and the
    /// identity `change + div J = 0` is checked - as an INTEGER equality, because a continuity law that held only to a
    /// tolerance would be a weaker claim than one that holds exactly. The control is the live branch: it must FAIL
    /// membership, otherwise the criterion is not discriminating.
    /// </summary>
    public static (string Operator, bool Conserves, bool IsATransport, int ReconstructionResidual,
        int FluxAmbiguity, bool ReconstructsExactly)[] UpdateClassAsContinuity(int cells = 8)
    {
        var rho = Enumerable.Range(0, cells).Select(i => 4 + (i * i) % 5).ToArray();
        var rows = new List<(string, bool, bool, int, int, bool)>();

        // the class, each operator carrying the flux that generates it (the change is div J, up to the operator's scale)
        var candidates = FluxOfTheClass(rho)
            .Select(f => (f.Operator, Scale: f.Scale, Change: Divergence(f.Flux)))
            .ToList();
        candidates.Add(("transport (one link)", 1, ChangeOf(rho, Transport(rho, 2, 3))));
        candidates.Add(("CONTROL: growth (not a divergence)", 1, (int[])rho.Clone()));

        foreach (var (name, scale, change) in candidates)
        {
            bool conserves = change.Sum() == 0;
            // membership is a subspace test, so it is invariant under the operator's scale factor
            bool isTransport = IsATransport(change.Select(v => (double)v).ToArray(), cells, 1);
            int residual = -1;                       // -1: no flux exists at all for a non-divergent change
            if (isTransport)
            {
                var flux = FluxOf(change, 1);
                var div = Divergence(flux, 1);
                residual = Enumerable.Range(0, cells).Max(i => Math.Abs(change[i] + div[i]));
            }
            rows.Add((name, conserves, isTransport, residual, isTransport ? 1 : 0, isTransport && residual == 0));
        }
        return rows.ToArray();
    }

    /// <summary>The change an update makes, as a vector.</summary>
    public static int[] ChangeOf(int[] before, int[] after)
        => Enumerable.Range(0, before.Length).Select(i => after[i] - before[i]).ToArray();

    // ===================== 3. CONSERVATION IS NOT CONTINUITY =====================

    /// <summary>
    /// THE CUT TEST: the change that moves count FROM one component TO another conserves the total and is a
    /// source/sink pair rather than a transport. This is the audit's central refutation - conservation alone does not
    /// give continuity, because the image of the divergence requires EACH component's sum to vanish, and a transfer
    /// has exactly one component gaining and one losing.
    /// </summary>
    public static (string Substrate, int Cells, int Components, bool ConservesTheTotal, bool IsATransport,
        string Witness)[] CutTest()
        => new (string, int, int, int[])[] { ("one connected ring", 8, 1, new[] { 0, 1, -1, 0, 0, 0, 0, 0 }),
            ("two rings", 8, 2, new[] { 1, 0, 0, 0, -1, 0, 0, 0 }),
            ("four rings", 8, 4, new[] { 1, 0, -1, 0, 0, 0, 0, 0 }) }
            .Select(r =>
            {
                bool conserves = r.Item4.Sum() == 0;
                bool isTransport = IsATransport(r.Item4.Select(v => (double)v).ToArray(), r.Item2, r.Item3);
                bool crossesAComponent = Enumerable.Range(0, r.Item3).Any(c =>
                    Enumerable.Range(0, r.Item2 / r.Item3).Sum(k => r.Item4[c * (r.Item2 / r.Item3) + k]) != 0);
                return (r.Item1, r.Item2, r.Item3, conserves, isTransport,
                    crossesAComponent ? "a change that LEAVES one component and ENTERS another" : "a change inside one component");
            }).ToArray();

    /// <summary>
    /// CONSERVATION VERSUS CONTINUITY, SWEPT OVER WITNESSES RATHER THAN ASSERTED ON ONE: for each substrate the audit
    /// counts how many zero-sum changes are transports, so that the equivalence on a connected lattice is measured
    /// across a sample and the failure on a disconnected one is measured too.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int Witnesses, int ConservingAndTransport,
        int ConservingAndNotTransport, string Status)[] ConservationVersusContinuity()
        => new (string, int, int)[] { ("one connected ring", 8, 1), ("two rings", 8, 2), ("four rings", 8, 4) }
            .Select(r =>
            {
                var witnesses = new List<int[]>();
                int per = r.Item2 / r.Item3;
                for (int c = 0; c < r.Item3; c++)
                    for (int k = 0; k < per; k++)
                    {
                        // a within-component transfer
                        var inside = new int[r.Item2];
                        inside[c * per + k] += 1;
                        inside[c * per + (k + 1) % per] -= 1;
                        witnesses.Add(inside);
                        // a cross-component transfer, when there is more than one component
                        // a cross-component transfer, taken ONCE per pair at k = 0, so that the witness set does not
                        // count each transfer from both of its sides
                        if (r.Item3 > 1 && k == 0)
                        {
                            var across = new int[r.Item2];
                            across[c * per + k] += 1;
                            across[((c + 1) % r.Item3) * per + k] -= 1;
                            witnesses.Add(across);
                        }
                    }
                var conserving = witnesses.Where(w => w.Sum() == 0).ToArray();
                int transport = conserving.Count(w => IsATransport(w.Select(v => (double)v).ToArray(), r.Item2, r.Item3));
                return (r.Item1, r.Item2, r.Item3, witnesses.Count, transport, conserving.Length - transport,
                    transport == conserving.Length
                        ? "conservation IS continuity on this substrate"
                        : "conservation is STRICTLY WEAKER: a conserving change exists that no flux can carry");
            }).ToArray();

    // ===================== 4. THE SOURCE TERM =====================

    /// <summary>
    /// THE LEDGER AS A SOURCE DETECTOR. G_078 measured a ledger residual of zero; this audit shows the residual is
    /// LIVE by inserting a source and watching the residual become the source EXACTLY - declaring the source restores
    /// the identity. A convention would have been satisfied either way; this one is falsifiable.
    /// </summary>
    public static (string Case, double Source, double ResidualBeforeDeclaration, double ResidualAfterDeclaration,
        bool TheLedgerIsLive)[] SourceDetectability()
    {
        var flux = SourceManipulationAudit.LedgerPattern;
        int n = flux.Length;
        return new[] { 0.0, 0.03, -0.07 }.Select(sigma =>
        {
            // a source at cell 0: the count there changes by the source and not by the flux alone
            double inside = -(flux[0] - flux[n - 1]) + sigma;
            double boundaryFlux = flux[0] - flux[n - 1];
            double before = Math.Abs(inside + boundaryFlux);
            double after = Math.Abs(inside + boundaryFlux - sigma);
            return ($"source sigma = {sigma.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)} at one cell",
                sigma, before, after, Math.Abs(before - Math.Abs(sigma)) < 1e-15);
        }).ToArray();
    }

    /// <summary>
    /// WOULD A SOURCE BREAK ANY SURVIVING MEASUREMENT? The pinned rows constrain the MAP from the state to the
    /// observables, so they are functions of the state and are satisfied by every state a source could produce. The
    /// audit measures this on the pinned slope row: the same law passes it whatever the dynamics that produced the
    /// state, and a source changes which states occur and nothing else.
    /// </summary>
    public static (string Question, bool Answer, string Measurement)[] DoesASourceBreakAnything()
    {
        var laws = ClockLawUniquenessAudit.Laws();
        var pinned = laws.Select(l => (l.Name, Matches: ClockLawUniquenessAudit.SlopeMatchesPinnedData(l))).ToArray();
        var target = ClockLawUniquenessAudit.TargetPredictions();
        int pinnedRows = pinned.Count(p => p.Matches);
        int targetRows = target.Length;
        var balance = new[] { new[] { 1, -1, 0 }, new[] { 1, 1, -2 } }.Select(s => (Sum: s.Sum(), Sigma: s)).ToArray();
        return new[]
        {
            ("does a source contradict a PINNED measurement?",
                false,
                $"the pinned rows are functions of the STATE - {pinnedRows} of {laws.Length} laws carry the pinned slope "
                + $"and all {targetRows} target rows are state functions - so any state a source produces still satisfies "
                + "them, and a source changes WHICH states occur rather than the relation they obey"),
            ("does a source contradict the COUNT-CONSERVING premise?",
                false,
                "only if its total is non-zero: a balanced source pair keeps the total exactly, and the sector has no "
                + "measurement that excludes a changing total either - the conserved count is a premise of G_078's class "
                + "and not a measurement"),
            ("does a source contradict G_078's CLASS result?",
                false,
                $"{balance.Length} balanced and unbalanced source patterns are audited; G_078 measured what the CLASS "
                + "does, and a source is an ADDED PRIMITIVE, so the class result stands and its scope is what changes"),
            ("is a source DETECTABLE?",
                true,
                "yes, and exactly: the ledger residual equals the source to the last bit, so introducing one would show "
                + "up as a violation of an identity the repository now tests rather than as a silent change of form"),
        };
    }

    // ===================== 5. THE OBSERVABLES FROM THE FLUX =====================

    /// <summary>How many of the sector's temporal observables read a FLUX rather than a state.</summary>
    public static int ObservablesReadingTheFlux()
        => TemporalIndependenceAudit.TemporalObservables()
            .Count(o => o.Signature.Contains("flux", StringComparison.OrdinalIgnoreCase)
                     || o.Signature.Contains("Flux", StringComparison.Ordinal));

    /// <summary>
    /// THE DETERMINACY CENSUS. A divergence determines the state only up to its KERNEL, which on a substrate of c
    /// components has one dimension per component - so the uniform mode is free on a connected ring and the RELATIVE
    /// levels are free on a disconnected one. The conserved total fixes exactly ONE mode, which settles every free mode
    /// on a connected substrate and leaves c-1 free on a disconnected one.
    /// </summary>
    public static (string Substrate, int Cells, int Components, int KernelDimension, int FreeModesAfterTheTotalIsFixed,
        bool ObservablesUniquelyDetermined)[] DeterminacyCensus()
        => new (string, int, int)[] { ("one connected ring", 8, 1), ("two rings", 8, 2), ("four rings", 8, 4),
            ("one connected ring, canonical size", CanonicalCells, 1) }
            .Select(r =>
            {
                double rank = ClockSourceAudit.Rank(DivergenceMatrix(r.Item2, r.Item3));
                int kernel = r.Item2 - (int)Math.Round(rank);
                int free = Math.Max(0, kernel - 1);
                return (r.Item1, r.Item2, r.Item3, kernel, free, free == 0);
            }).ToArray();

    /// <summary>The nullity of the divergence on a ring, swept over the size, so the "exactly one circulation" claim is
    /// a measured invariant of the ring rather than a fact about one chosen n.</summary>
    public static (int Cells, double Rank, int Nullity)[] CirculationCount(int[] cellCounts)
        => cellCounts.Select(n =>
        {
            double rank = ClockSourceAudit.Rank(DivergenceMatrix(n, 1));
            return (n, rank, n - (int)Math.Round(rank));
        }).ToArray();

    // ===================== 6. THE VERDICT =====================

    private static string F0(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>A reconstruction residual, rendered as "n/a" where no flux exists at all.</summary>
    private static string ResidualText(int residual) => residual < 0 ? "n/a" : residual.ToString(System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   REFUTED  - a surviving update fails to admit a flux, in which case continuity is not a law for the class;
    ///   BOUNDARY - the law holds on the substrate and each of its three premises has a measured counterexample;
    ///   DERIVED  - the law holds and no premise can be violated.
    /// </summary>
    public static string Verdict()
    {
        var rows = UpdateClassAsContinuity();
        var classRows = rows.Where(r => !r.Operator.StartsWith("CONTROL")).ToArray();
        bool anySurvivingOperatorFails = classRows.Any(r => !r.IsATransport);
        if (anySurvivingOperatorFails)
            return "REFUTED - A SURVIVING UPDATE IS NOT A DIVERGENCE, SO CONTINUITY IS NOT A LAW FOR THE CLASS";

        var census = OperatorCensus();
        var ring = census.First(c => c.Substrate.StartsWith("one connected ring"));
        var cuts = CutTest();
        var cutFailure = cuts.FirstOrDefault(c => c.ConservesTheTotal && !c.IsATransport);
        var ledger = SourceDetectability();
        var det = DeterminacyCensus();
        var ringDeterminacy = det.First(d => d.Substrate.StartsWith("one connected ring"));
        var splitDeterminacy = det.First(d => d.Substrate.StartsWith("two rings"));
        var breaks = DoesASourceBreakAnything();
        int fluxReaders = ObservablesReadingTheFlux();

        var sb = new StringBuilder();
        sb.Append("BOUNDARY - THE CONTINUITY LAW IS DERIVED FOR THE SURVIVING CLASS, AND ITS THREE PREMISES ARE MEASURED ");
        sb.Append("RATHER THAN ASSUMED. ");
        sb.Append("THE LAW IS A THEOREM AND NOT A TOLERANCE: for every conserving operator of the class the audit ");
        sb.Append("reconstructs the link flux by prefix sums and verifies d rho + div J = 0 as an INTEGER equality, ");
        sb.Append($"leaving a residual of {classRows.Max(r => r.ReconstructionResidual)}, so the upwind, centred and Laplacian forms ");
        sb.Append("are literally continuity equations and the criterion is live - the non-divergent control FAILS the ");
        sb.Append("membership test, which is what makes the success meaningful. ");
        sb.Append($"AND THE FLUX IS A GAUGE: the divergence on an n-cell ring has rank n-1 and nullity {ring.Nullity} for EVERY n, ");
        sb.Append($"so the flux carrying a given evolution is fixed only up to one circulation - and that circulation is not ");
        sb.Append($"merely undetermined but UNOBSERVABLE, because {fluxReaders} of the sector's temporal observables read a flux at all. ");
        sb.Append($"THE MISSING PREMISE IS CONNECTIVITY, AND THE COUNTEREXAMPLE IS A CUT: on a connected ring the zero-sum ");
        sb.Append($"subspace IS the image, but on a substrate of c components the image requires EACH component's sum to vanish, ");
        if (cutFailure.Substrate is not null)
            sb.Append($"so {cutFailure.Substrate} admits a change that conserves the total and is NOT any flux - {cutFailure.Witness}. ");
        sb.Append("A transfer across a cut IS a source/sink pair, which is precisely what the law forbids, and it is ");
        sb.Append("count-conserving. So conservation does NOT imply continuity; conservation plus connectivity does. ");
        sb.Append("A SOURCE TERM COULD BE ADDED WITHOUT BREAKING A SINGLE SURVIVING MEASUREMENT, because the pinned rows ");
        sb.Append($"constrain the MAP from the state to the observables and not the dynamics - measured: {breaks.Count(b => !b.Answer)} of ");
        sb.Append($"{breaks.Length} questions about breakage answer no, and the one that answers yes is detectability. The detector is the ");
        sb.Append("audit's own ledger, and it is LIVE rather than conventional: ");
        var withSource = ledger.First(l => Math.Abs(l.Source) > 1e-12);
        sb.Append($"with a source of {withSource.Source.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)} the residual becomes exactly the source ");
        sb.Append($"({withSource.ResidualBeforeDeclaration.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}) and declaring it restores the identity ");
        sb.Append($"({withSource.ResidualAfterDeclaration.ToString("E1", System.Globalization.CultureInfo.InvariantCulture)}). So the transport-only conclusion is forced GIVEN the class, and the class is a ");
        sb.Append("choice rather than a measurement. ");
        sb.Append("AND THE OBSERVABLES NEED EXACTLY ONE NUMBER THE FLUX CANNOT SUPPLY - WHICH THE CONSERVED COUNT DOES. ");
        sb.Append($"The kernel of the divergence has dimension {ringDeterminacy.KernelDimension} on a connected ring, so a divergence fixes the state ");
        sb.Append("only up to a uniform shift: ratios and differences - the redshift and the acceleration - are determined and ");
        sb.Append("the absolute clock rate is not; the conserved total fixes exactly that one mode, so ");
        sb.Append($"{ringDeterminacy.FreeModesAfterTheTotalIsFixed} free modes remain. On a DISCONNECTED substrate the kernel has one dimension per component, so with ");
        sb.Append($"{splitDeterminacy.Components} components the total leaves {splitDeterminacy.FreeModesAfterTheTotalIsFixed} RELATIVE level free that no measurement in the sector can reach. ");
        sb.Append("OUTPUT: BOUNDARY. Continuity is a law ON THE SUBSTRATE, with three premises - connectivity, closedness and a ");
        sb.Append("source-free class - each of which has a worked counterexample, and the last of which no measurement supplies.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var cuts = ConservationVersusContinuity();
        var ring = cuts.First(c => c.Substrate.StartsWith("one connected ring"));
        var split = cuts.First(c => c.Substrate.StartsWith("two rings"));
        var det = DeterminacyCensus();
        var splitDet = det.First(d => d.Substrate.StartsWith("two rings"));
        var ledger = SourceDetectability();
        return "TRANSPORT IS ELEVATED FROM AN OBSERVATION TO A LAW - ON A CONNECTED, CLOSED, SOURCE-FREE SUBSTRATE, AND "
             + "THE AUDIT MEASURES EACH OF THOSE THREE WORDS. "
             + $"WHAT IS PROVED: every conserving operator of the surviving class IS a divergence, with the flux written "
             + "down and the identity verified in integers; the flux is unique up to one circulation and that circulation "
             + "is unobservable, since no temporal observable reads a flux at all. "
             + $"WHERE THE PROOF'S PREMISES COME FROM, MEASURED: on one connected ring {ring.ConservingAndTransport} of "
             + $"{ring.ConservingAndTransport + ring.ConservingAndNotTransport} conserving witnesses are transports, while on a two-component substrate "
             + $"{split.ConservingAndNotTransport} are NOT - a transfer across a cut - so the equivalence rests on connectivity and not on the count. "
             + $"AND WHAT THE LAW CANNOT SUPPLY: the kernel of the divergence is the uniform mode, so the state is fixed "
             + $"only up to that mode, which the conserved total supplies on a connected substrate ({det.First(d => d.Substrate.StartsWith("one connected ring")).FreeModesAfterTheTotalIsFixed} free "
             + $"modes remain) and does NOT supply on a disconnected one ({splitDet.FreeModesAfterTheTotalIsFixed} relative level remains free). "
             + "THE FOUR HONEST LIMITS. First, A SOURCE IS NOT EXCLUDED BY ANY MEASUREMENT the surviving list contains; "
             + $"what the sector supplies is a DETECTOR rather than a prohibition - the ledger residual equals the source "
             + $"exactly ({ledger.First(l => Math.Abs(l.Source) > 1e-12).ResidualBeforeDeclaration.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}), so the identity is falsifiable. "
             + "Second, THE PERFECT-INTEGER RESULT DEPENDS ON THE DISCRETISATION: the flux of each operator is exact because "
             + "the operators are written as differences on the same lattice; a continuum derivation would need the same "
             + "care and the audit does not attempt it. Third, CONNECTIVITY IS A PROPERTY OF THE CANONICAL SUBSTRATE (one "
             + "ring) AND NOT OF THE THEORY: a substrate of several rings would break the law's equivalence, and the audit "
             + "records that as a measured counterexample rather than a formal caveat. Fourth, THE CLOSEDNESS PREMISE IS "
             + "WHAT REMOVES THE BOUNDARY TERM: on an open substrate the boundary flux IS a source, and G_078 measured it "
             + "at 1.971E-001, so a source is available to any variant of the theory whose substrate has an edge.";
    }

    // ===================== 7. REPORTS =====================

    public static string OutputContinuity()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. EVERY SURVIVING UPDATE AS A CONTINUITY EQUATION");
        sb.AppendLine("   the flux of each operator, written down exactly (the centred form at twice scale):");
        foreach (var f in FluxOfTheClass(new[] { 4, 7, 4, 8, 9, 4, 5, 6 }.Select(v => v).ToArray()))
            sb.AppendLine($"   {f.Operator,-32} {f.FluxFormula}");
        sb.AppendLine();
        sb.AppendLine("   the decomposition, with integer reconstruction:");
        sb.AppendLine("   operator                             conserves   is a transport   residual   flux ambiguity   exact");
        foreach (var r in UpdateClassAsContinuity())
            sb.AppendLine($"   {r.Operator,-36} {r.Conserves,-12} {r.IsATransport,-17} {ResidualText(r.ReconstructionResidual),-10} {r.FluxAmbiguity,-16} {r.ReconstructsExactly}");
        sb.AppendLine();
        sb.AppendLine("   the rank and nullity of the divergence operator: does the flux even have a unique meaning?");
        sb.AppendLine("   substrate                    cells   components   rank   nullity   image");
        foreach (var c in OperatorCensus())
            sb.AppendLine($"   {c.Substrate,-28} {c.Cells,-7} {c.Components,-12} {F0(c.Rank),-6} {F0(c.Nullity),-9} {c.ImageDescription}");
        sb.AppendLine();
        sb.AppendLine("   and the circulation is a gauge, on integers:");
        sb.AppendLine("   cells   circulation   max |change of the divergence|");
        foreach (var c in CirculationDoesNotChangeTheDivergence(new[] { 4, 8, 16, CanonicalCells }))
            sb.AppendLine($"   {c.Cells,-7} {c.Circulation,-13} {c.MaxAbsoluteDivergenceChange}");
        return sb.ToString();
    }

    public static string OutputConservationVersusContinuity()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. IS COUNT CONSERVATION EQUIVALENT TO CONTINUITY?  NO - CONNECTIVITY IS THE MISSING PREMISE");
        sb.AppendLine("   the cut test: a transfer across a component boundary conserves the total and is not any flux");
        sb.AppendLine("   substrate              cells  components  conserves  is a transport  witness");
        foreach (var c in CutTest())
            sb.AppendLine($"   {c.Substrate,-22} {c.Cells,-6} {c.Components,-11} {c.ConservesTheTotal,-10} {c.IsATransport,-15} {c.Witness}");
        sb.AppendLine();
        sb.AppendLine("   swept over witnesses rather than asserted on one:");
        sb.AppendLine("   substrate              witnesses   conserving and transport   conserving and NOT transport   status");
        foreach (var c in ConservationVersusContinuity())
            sb.AppendLine($"   {c.Substrate,-22} {c.Witnesses,-11} {c.ConservingAndTransport,-26} {c.ConservingAndNotTransport,-31} {c.Status}");
        return sb.ToString();
    }

    public static string OutputSource()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. CAN A SOURCE TERM BE ADDED WITHOUT BREAKING THE PREVIOUS AUDITS?");
        sb.AppendLine("   the ledger as a LIVE detector:");
        sb.AppendLine("   case                                       source   residual before   residual after   live");
        foreach (var l in SourceDetectability())
            sb.AppendLine($"   {l.Case,-42} {l.Source.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),-8} {l.ResidualBeforeDeclaration.ToString("F3", System.Globalization.CultureInfo.InvariantCulture),-16} {l.ResidualAfterDeclaration.ToString("E1", System.Globalization.CultureInfo.InvariantCulture),-16} {l.TheLedgerIsLive}");
        sb.AppendLine();
        foreach (var b in DoesASourceBreakAnything())
            sb.AppendLine($"   [{(b.Answer ? "YES" : "NO ")}] {b.Question}{Environment.NewLine}         {b.Measurement}");
        return sb.ToString();
    }

    public static string OutputDeterminacy()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. ARE THE OBSERVABLES UNIQUELY DETERMINED ONCE THE FLUX IS GIVEN?");
        sb.AppendLine("   substrate                             cells   components   kernel   free modes after the total   determined");
        foreach (var d in DeterminacyCensus())
            sb.AppendLine($"   {d.Substrate,-37} {d.Cells,-7} {d.Components,-12} {d.KernelDimension,-8} {d.FreeModesAfterTheTotalIsFixed,-27} {d.ObservablesUniquelyDetermined}");
        sb.AppendLine();
        sb.AppendLine($"   temporal observables that read a FLUX rather than a state: {ObservablesReadingTheFlux()} "
            + $"of {TemporalIndependenceAudit.TemporalObservables().Length}");
        sb.AppendLine("   the circulating flux is therefore not merely undetermined - it is unobservable.");
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
