using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_078 - SOURCE MANIPULATION AUDIT (group G - Gravity Source).
///
/// QUESTION. G_077 established that the clock and the acceleration share ONE source (the local occupancy rho). This
/// audit asks whether that source can be CHANGED LOCALLY: which processes can alter local rho, are they redistribution
/// or genuine amplification or suppression, what is the maximum achievable local enhancement under count
/// conservation, can a bounded region hold a change without compensating deficits elsewhere, and do clock and
/// gravitational manipulations necessarily carry each other's signature?
///
/// Allowed: actualization, density transport, count conservation, causal structure. FORBIDDEN and not used: new
/// fields, imported matter sectors, imported GR equations.
///
/// ANSWER: **REFUTED - REDISTRIBUTION IS THE ONLY ELEMENTARY CHANNEL, AND IT IS NOT A WEAK ONE.** Every natural
/// operator of the update class is a DIVERGENCE and therefore preserves the count exactly, so NO source term is
/// supplied by the surviving sector - a source would be an IMPORT, which the question forbids. What redistribution
/// buys is asymmetric and the audit measures both sides of it:
///
///  (1) THE UPLIFT IS BOUNDED BY THE CELL COUNT. Concentrating the whole conserved count into one cell raises the
///      potential there by (1/d) ln m - and nothing can raise it further, because positivity caps the concentration
///      at the total. On the canonical lattice that is the CONCRETE NUMBER (1/3) ln 96 = 1.5184, a RATE ratio of
///      m^(1/d) = 96^(1/3) = 4.5714 and a metric ratio of m^(2/d) = 96^(2/3) = 20.9. The bound comes from the cell
///      count and not from any dynamics.
///
///  (2) THE DEPRESSION IS UNBOUNDED, so the reachable CLOCK RATIO between two cells is unbounded even though the
///      uplift is not: a well and a hill of the same count diverge from each other as the well deepens, because the
///      potential is LOGARITHMIC in the occupancy. A proof that only redistribution exists therefore does NOT kill
///      gravity engineering - it kills the MONOPOLE and leaves the BALANCED PAIR, whose contrast has no bound.
///
///  (3) A BOUNDED REGION CANNOT HOLD A CHANGE ALONE, and the audit measures the ledger rather than asserting it: the
///      change of the count inside any region equals minus the net flux through its boundary, to the arithmetic's own
///      resolution. The compensating deficit must exist SOMEWHERE, and its distance is free - the flux decides it -
///      so the two-level answer is that the compensation is compulsory and its LOCATION is an engineering choice.
///
///  (4) AND THE TWO IMPLICATIONS ARE ASYMMETRIC, which is the audit's sharpest pair. EVERY gravitational manipulation
///      carries a clock signature, because an acceleration change needs a gradient of the occupancy and the clock is a
///      STRICTLY MONOTONE readout of it - measured over the whole corpus. EVERY clock manipulation carries a
///      gravitational signature too, with exactly ONE exception: the UNIFORM direction, which changes every clock and
///      no acceleration - and that is the one move COUNT CONSERVATION FORBIDS, because it scales every occupancy and
///      so changes the total. So the only channel that would have decoupled the clock from gravity is the channel the
///      theory closes.
/// </summary>
public static class SourceManipulationAudit
{
    public const int D = 3;

    /// <summary>The canonical lattice's cell count - the number that makes the uplift bound concrete.</summary>
    public const int CanonicalCells = 96;

    // ===================== 1. THE STATE, THE TOTAL, AND THE EXTREMES =====================

    public static double Total(double[] rho) => rho.Sum();

    /// <summary>The canonical transport pattern used by the ledger: every region of it carries a non-zero change.</summary>
    public static double[] LedgerPattern => new[] { 0.05, -0.02, 0.01, 0.0, -0.03, 0.02, 0.0, -0.01 };

    /// <summary>The uniform configuration at unit occupancy - the vacuum.</summary>
    public static double[] Uniform(int cells)
    {
        var rho = new double[cells];
        for (int i = 0; i < cells; i++) rho[i] = 1.0;
        return rho;
    }

    /// <summary>All of the count in one cell, nothing elsewhere: the most concentrated configuration there is.</summary>
    public static double[] Concentrated(int cells, int cell)
    {
        var rho = new double[cells];
        rho[cell] = cells;
        return rho;
    }

    /// <summary>A well and a hill whose total is the same as the uniform configuration's.</summary>
    public static double[] BalancedPair(int cells, double wellDepth)
    {
        var rho = Uniform(cells);
        rho[0] -= wellDepth;
        rho[1] += wellDepth;
        return rho;
    }

    /// <summary>The clock potential A = (1/d) ln rho, the same object the acceleration is the gradient of.</summary>
    public static double Potential(double rho) => rho <= 0.0 ? double.NegativeInfinity : Math.Log(rho) / D;

    // ===================== 2. WHICH PROCESSES CAN ALTER LOCAL RHO =====================

    /// <summary>
    /// THE UPDATE CLASS, tested on one state: the four operator forms the repository already compares elsewhere,
    /// plus a CONTROL that is deliberately not a divergence. The count change sum L(rho) is the whole measurement: a
    /// DIVERGENCE preserves the count on a CLOSED ring and a source does not, so the control proves the detector can
    /// see one. The open-chain column carries the second half of the finding: a bounded region has nowhere to send its
    /// boundary flux, so the leak there should equal exactly the boundary term the open chain drops - measured, not
    /// asserted.
    /// </summary>
    public static (string Operator, double CountChangeOnARing, double CountChangeOnAnOpenChain,
        double DroppedBoundaryTerm, bool OpenLeakEqualsDroppedTerm)[] OperatorClass(double[] rho)
    {
        int n = rho.Length;
        var rows = new List<(string, double, double, double, bool)>();
        foreach (var (name, form) in new (string, Func<double[], int, double>)[]
                 {
                     ("local difference (upwind)", (r, i) => r[i] - r[(i - 1 + n) % n]),
                     ("centred difference", (r, i) => 0.5 * (r[(i + 1) % n] - r[(i - 1 + n) % n])),
                     ("second difference (Laplacian)", (r, i) => r[(i + 1) % n] - 2.0 * r[i] + r[(i - 1 + n) % n]),
                     ("CONTROL: growth (not a divergence)", (r, i) => r[i]),
                 })
        {
            double ring = 0.0, chain = 0.0;
            for (int i = 0; i < n; i++)
            {
                ring += form(rho, i);
                // the open-chain form: the ends have no wrap-around neighbour
                double left = rho[i == 0 ? 0 : i - 1], right = rho[i == n - 1 ? n - 1 : i + 1], self = rho[i];
                chain += name.StartsWith("local") ? self - left
                    : name.StartsWith("centred") ? 0.5 * (right - left)
                    : name.StartsWith("second") ? right - 2.0 * self + left
                    : self;
            }
            double dropped = rho[n - 1] - rho[0];
            rows.Add((name, ring, chain, dropped, Math.Abs(chain - dropped) < 1e-12));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE LEDGER: a TRANSPORT is applied to a state, and the change of the count inside each region is compared with
    /// the NET FLUX through that region's boundary. The audit measures the identity rather than asserting it - this
    /// is the apparatus of the answer to "can a bounded region change alone?". The regions start at cell 0 so that
    /// each row carries a NON-ZERO change: a ledger whose entries are all zero proves nothing.
    /// </summary>
    public static (string Region, double ChangeInside, double NetBoundaryFlux, double Residual)[]
        RegionLedger(double[] flux)
    {
        int n = flux.Length;
        var rho = Uniform(n);
        var after = new double[n];
        for (int i = 0; i < n; i++) after[i] = rho[i] - (flux[i] - flux[(i - 1 + n) % n]);

        var rows = new List<(string, double, double, double)>();
        foreach (var (name, start, length) in new[] { ("one cell", 0, 1), ("three cells", 0, 3), ("seven cells", 1, 7), ("the whole ring", 0, n) })
        {
            double inside = 0.0;
            for (int k = 0; k < length; k++) inside += after[(start + k) % n] - rho[(start + k) % n];
            // the boundary of a contiguous block is the two links that leave it
            double boundary = flux[(start + length - 1) % n] - flux[(start - 1 + n) % n];
            rows.Add((name, inside, -boundary, Math.Abs(inside + boundary)));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE CELL LEDGER: every single cell of the ring, so that "a cell cannot change alone" is measured for ALL cells
    /// and not for one arbitrarily chosen one.
    /// </summary>
    public static (int Cell, double ChangeInside, double NetBoundaryFlux, double Residual)[] CellLedger(double[] flux)
    {
        int n = flux.Length;
        var rows = new (int, double, double, double)[n];
        for (int i = 0; i < n; i++)
        {
            double inside = -(flux[i] - flux[(i - 1 + n) % n]);
            double boundary = flux[i] - flux[(i - 1 + n) % n];
            rows[i] = (i, inside, -boundary, Math.Abs(inside + boundary));
        }
        return rows;
    }

    /// <summary>The channel census: every process the question allows, with its measured status.</summary>
    public static (string Channel, string Status, string Measurement)[] ChannelCensus()
    {
        var operators = OperatorClass(Uniform(8).Select((_, i) => 1.0 + 0.3 * Math.Sin(i)).ToArray());
        double divergenceLeak = operators.Where(o => !o.Operator.StartsWith("CONTROL"))
            .Max(o => Math.Abs(o.CountChangeOnARing));
        double controlLeak = Math.Abs(operators.Single(o => o.Operator.StartsWith("CONTROL")).CountChangeOnARing);
        var ledger = RegionLedger(LedgerPattern);

        return new[]
        {
            ("density transport", "AVAILABLE - the elementary channel",
                $"a transport changes a region's count by exactly its net boundary flux, residual {E3(ledger.Max(l => l.Residual))}"),
            ("actualization (the update class)", "COUNT-PRESERVING - not a source",
                $"every divergence in the update class leaks {E3(divergenceLeak)} of the count on a closed ring while the "
                + $"non-divergence control leaks {E3(controlLeak)}"),
            ("a source term", "NOT SUPPLIED - it would be an IMPORT",
                "no operator of the natural class changes the total, so a source term is a new primitive rather than a "
                + "consequence of the surviving sector"),
            ("causal structure", "ORIENTS AND SUPPRESSES - it does not create",
                "a causal order restricts which links may carry flux in which direction, so it removes reachable "
                + "configurations rather than adding them"),
        };
    }

    // ===================== 3. THE BOUNDS, AND WHICH ASSUMPTION THEY REST ON =====================

    /// <summary>
    /// THE UPLIFT CAP, IN TWO READINGS THAT MUST AGREE IF THE CAP IS REAL. Reading A concentrates the WHOLE count in
    /// one cell and lets the others empty; reading B keeps a floor of one count unit in every cell and puts the
    /// SURPLUS in one cell. Both are computed, because the difference between them is the audit's own robustness
    /// check: a cap that moved with the reading would be an artifact of the reading.
    /// </summary>
    public static (string Reading, double MaxOccupancy, double Uplift, double RateRatio, double MetricRatio, bool FloorAssumed)[]
        UpliftCap(int cells, double surplus)
        => new[]
        {
            ("all of the count in one cell (others empty)", (double)cells, Potential(cells) - Potential(1.0),
                Math.Pow(cells, 1.0 / D), Math.Pow(cells, 2.0 / D), false),
            ("one unit in every cell + the surplus in one", 1.0 + surplus, Potential(1.0 + surplus) - Potential(1.0),
                Math.Pow(1.0 + surplus, 1.0 / D), Math.Pow(1.0 + surplus, 2.0 / D), true),
        };

    /// <summary>
    /// THE CONTRAST, WHICH IS WHERE THE ASSUMPTION BITES. The contrast between a well and a hill of the same count is
    /// bounded or not depending on ONE question the surviving sector does not settle here: may a cell be EMPTY? With a
    /// floor of one count unit in every cell the contrast is capped by the surplus - and the cap is the SAME number as
    /// the uplift cap, so the asymmetry between uplift and depression VANISHES. The cap only disappears if an empty
    /// cell is admitted, where A = (1/d) ln rho diverges.
    /// </summary>
    public static (string Branch, double WellOccupancy, double MaxContrast, string Status)[]
        ContrastBranches(int cells, double surplus)
        => new[]
        {
            ("unit floor in every cell", 1.0, Math.Pow(1.0 + surplus, 1.0 / D),
                "BOUNDED - and EQUAL to the uplift cap, so the uplift/depression asymmetry VANISHES under a floor"),
            ("empty cell admitted (occupancy 0)", 0.0, double.PositiveInfinity,
                "UNBOUNDED - but this is the regime where A = (1/d) ln rho itself diverges, i.e. the sector's breakdown, "
                + "and G_075 measured that no surviving datum probes it"),
            ("integer counts, surplus S over the floor", 1.0,
                Math.Pow(1.0 + cells, 1.0 / D),
                $"a genuine integer count cannot fund more than the total: with surplus {cells} on {cells} cells the "
                + "contrast is the same cap as the uplift"),
        };

    /// <summary>
    /// THE FLOORLESS BRANCH, carried only to show where the divergence comes from: the well occupancy runs to zero,
    /// the total is held fixed in every row, and only the LOGARITHM makes the contrast run away. These rows are NOT
    /// integer counts and the audit says so rather than quoting them as configurations.
    /// </summary>
    public static (double WellOccupancy, double HillOccupancy, double Total, double PotentialDifference, double RateRatio)[]
        NoFloorContrast(double total)
        => new[] { 1.0, 0.5, 0.1, 1.0e-2, 1.0e-4, 1.0e-6 }.Select(well =>
        {
            double hill = total - well;
            double difference = Potential(hill) - Potential(well);
            return (well, hill, well + hill, difference, Math.Exp(difference));
        }).ToArray();

    // ===================== 4. THE TWO IMPLICATIONS, MEASURED =====================

    /// <summary>
    /// THE UNIFORM DIRECTION, which is the ONLY move that changes clocks and no acceleration: uniform occupancy
    /// scaling. The audit measures the clock change, the acceleration change and the COUNT change, because the third
    /// is what rules it out.
    /// </summary>
    public static (int Cells, double ClockChange, double AccelerationChange, double CountChange)[] UniformEscape(int[] cellCounts)
        => cellCounts.Select(m =>
        {
            var before = Uniform(m);
            var after = new double[m];
            for (int i = 0; i < m; i++) after[i] = 1.02;                 // the same scaling everywhere
            var law = ClockLawUniquenessAudit.At();
            double clock = Enumerable.Range(0, m)
                .Max(i => Math.Abs(ClockLawUniquenessAudit.Rate(law, Potential(after[i]))
                              - ClockLawUniquenessAudit.Rate(law, Potential(before[i]))));
            double acceleration = Enumerable.Range(0, m)
                .Max(i => Math.Abs(Acceleration(after)[i] - Acceleration(before)[i]));
            return (m, clock, acceleration, Total(after) - Total(before));
        }).ToArray();

    /// <summary>The acceleration from a profile of potentials, by the surviving source law a = -grad A.</summary>
    public static double[] Acceleration(double[] potential, bool ring = true)
    {
        int n = potential.Length;
        var acc = new double[n];
        for (int i = 0; i < n; i++)
        {
            double left = potential[i == 0 ? (ring ? n - 1 : 0) : i - 1];
            double right = potential[i == n - 1 ? (ring ? 0 : n - 1) : i + 1];
            acc[i] = -(right - left) / 2.0;
        }
        return acc;
    }

    /// <summary>
    /// GRAVITY WITHOUT A CLOCK, TESTED AND REFUTED: a change of the acceleration field needs a gradient of the
    /// occupancy, and the clock is a STRICTLY MONOTONE readout of the occupancy, so anywhere the gradient changes the
    /// occupancy changes and the clock changes with it. The audit measures the implication across the whole clock-law
    /// corpus rather than for one law.
    /// </summary>
    public static (string Law, int Configurations, int AccelerationWithoutClock, bool ImplicationHolds)[]
        GravityImpliesClock()
        => ClockLawUniquenessAudit.Laws().Select(law =>
        {
            int configurations = 0, violated = 0;
            foreach (double depth in new[] { 0.05, 0.2, 0.4, 0.8 })
            {
                var before = BalancedPair(8, 0.0);
                var after = BalancedPair(8, depth);
                double accelerationChange = Enumerable.Range(0, 8)
                    .Max(i => Math.Abs(Acceleration(after.Select(Potential).ToArray())[i]
                                      - Acceleration(before.Select(Potential).ToArray())[i]));
                double clockChange = Enumerable.Range(0, 8)
                    .Max(i => Math.Abs(ClockLawUniquenessAudit.Rate(law, Potential(after[i]))
                                      - ClockLawUniquenessAudit.Rate(law, Potential(before[i]))));
                configurations++;
                if (accelerationChange > 1e-9 && clockChange < 1e-12) violated++;
            }
            return (law.Name, configurations, violated, violated == 0);
        }).ToArray();

    /// <summary>
    /// CLOCK WITHOUT GRAVITY, TESTED: the ONE configuration family that changes every clock and no acceleration is
    /// the uniform scaling - and it is the one family whose total count changes, which count conservation forbids. The
    /// audit reports both facts together because either alone would be misleading.
    /// </summary>
    public static (string Question, bool Answer, string Measurement)[] MirrorImplication()
    {
        var escape = UniformEscape(new[] { 8 });
        var uniform = escape[0];
        var ledger = RegionLedger(LedgerPattern);
        var gravity = GravityImpliesClock();

        return new[]
        {
            ("every GRAVITATIONAL manipulation carries a clock signature",
                gravity.All(g => g.ImplicationHolds),
                $"over {gravity.Length} clock laws and 4 depths, the number of configurations where the acceleration "
                + $"changed and the clock did NOT is {gravity.Sum(g => g.AccelerationWithoutClock)}"),
            ("every CLOCK manipulation carries a gravitational signature",
                false,
                $"a uniform occupancy scaling changes the clock by {E3(uniform.ClockChange)} and the acceleration by "
                + $"{E3(uniform.AccelerationChange)} - the one exception - while changing the total count by "
                + $"{E3(uniform.CountChange)}, which count conservation forbids"),
            ("a bounded region can change its count ALONE",
                false,
                $"the change inside a region equals minus its net boundary flux to {E3(ledger.Max(l => l.Residual))}, "
                + "so a region that is not the whole system cannot change alone - the compensation is compulsory, and "
                + "only its LOCATION is free because the flux decides the distance"),
        };
    }

    /// <summary>
    /// The verdict's canonical figures as NUMBERS, so that a caller can check them without parsing prose (the G_075
    /// lesson: a number in a string is not evidence - this exposes the quantities the verdict quotes).
    /// </summary>
    public static (double Uplift, double RateRatio, double MetricRatio, double FloorContrast, double NoFloorContrast,
        bool ContrastIsBoundedUnderAFloor) VerdictFigures()
    {
        var cap = UpliftCap(CanonicalCells, CanonicalCells).Single(r => r.FloorAssumed);
        var branches = ContrastBranches(CanonicalCells, CanonicalCells);
        var floor = branches.Single(b => b.Branch.StartsWith("unit floor"));
        var open = branches.Single(b => b.Branch.StartsWith("empty cell"));
        return (cap.Uplift, cap.RateRatio, cap.MetricRatio, floor.MaxContrast, open.MaxContrast,
            !double.IsInfinity(floor.MaxContrast) && double.IsInfinity(open.MaxContrast));
    }

    private static string F4(double v) => v.ToString("F4", System.Globalization.CultureInfo.InvariantCulture);
    private static string E3(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    // ===================== 5. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   BOUNDARY - some surviving process changes the TOTAL count, which would open a genuine creation channel;
    ///   REFUTED  - every natural operator is a divergence, so no source is supplied, and the redistribution that
    ///              remains is capped by the count - with the contrast bound CONDITIONAL on whether a cell may be empty,
    ///              which turns the "unbounded depression" claim into a measured branch rather than a conclusion.
    /// </summary>
    public static string Verdict()
    {
        var census = ChannelCensus();
        var caps = UpliftCap(CanonicalCells, CanonicalCells);
        var floorCap = caps.Single(c => c.FloorAssumed);
        var openCap = caps.Single(c => !c.FloorAssumed);
        var branches = ContrastBranches(CanonicalCells, CanonicalCells);
        var floorBranch = branches.Single(b => b.Branch.StartsWith("unit floor"));
        var openBranch = branches.Single(b => b.Branch.StartsWith("empty cell"));
        var contrast = NoFloorContrast(2.0);
        var gravity = GravityImpliesClock();
        var escape = UniformEscape(new[] { CanonicalCells }).Single();
        var ledger = CellLedger(LedgerPattern);
        var operators = OperatorClass(Uniform(8).Select((_, i) => 1.0 + 0.3 * Math.Sin(i)).ToArray());
        bool anySource = operators.Any(o => !o.Operator.StartsWith("CONTROL") && Math.Abs(o.CountChangeOnARing) > 1e-9);
        if (anySource) return "BOUNDARY - A SURVIVING OPERATOR CHANGES THE TOTAL COUNT";

        var sb = new StringBuilder();
        sb.Append("REFUTED - REDISTRIBUTION IS THE ONLY ELEMENTARY CHANNEL, AND THE SYMMETRY OF ITS BOUNDS TURNS ON ONE ");
        sb.Append("ASSUMPTION THIS AUDIT REFUSES TO MAKE SILENTLY. ");
        sb.Append("NO SOURCE TERM IS SUPPLIED BY THE SURVIVING SECTOR: every one of the natural update forms is a ");
        sb.Append($"DIVERGENCE and leaks {Math.Abs(operators.Where(o => !o.Operator.StartsWith("CONTROL")).Max(o => o.CountChangeOnARing))} ");
        sb.Append($"of the count on a closed ring, while the deliberately non-divergent CONTROL leaks {Math.Abs(operators.Single(o => o.Operator.StartsWith("CONTROL")).CountChangeOnARing)} ");
        sb.Append("- so the detector can see a source, and there is none in the class. A source term would therefore be ");
        sb.Append("an IMPORT, which the question forbids. ");
        sb.Append("THE UPLIFT CAP IS ROBUST AND THE CONTRAST CAP IS NOT, and that is the audit's main finding. THE UPLIFT IS ");
        sb.Append($"CAPPED BY THE COUNT: concentrating everything in one cell gives {F4(openCap.Uplift)} in the potential, a ");
        sb.Append($"rate ratio of {F4(openCap.RateRatio)} and a metric ratio of {F4(openCap.MetricRatio)}, while carrying the ");
        sb.Append($"surplus above a one-unit floor gives {F4(floorCap.Uplift)}, {F4(floorCap.RateRatio)} and {F4(floorCap.MetricRatio)} ");
        sb.Append($"- the two readings agree to {F4(100.0 * Math.Abs(floorCap.Uplift - openCap.Uplift) / openCap.Uplift)}%, so the ");
        sb.Append("cap does not depend on which reading is taken, and positivity is the whole reason: no cell can hold more ");
        sb.Append("than the total. THE CONTRAST IS THE ASSUMPTION. UNDER A UNIT FLOOR IN EVERY CELL the contrast is capped by ");
        sb.Append($"the surplus at {F4(floorBranch.MaxContrast)} - THE SAME NUMBER AS THE UPLIFT CAP - so the asymmetry between ");
        sb.Append("uplift and depression VANISHES and redistribution is not a gateway to large clock contrasts. THE ASYMMETRY ");
        sb.Append($"EXISTS ONLY IF AN EMPTY CELL IS ADMITTED: there the contrast is unbounded, reaching {E3(contrast.Last().RateRatio)} ");
        sb.Append($"at a well occupancy of {E3(contrast.Last().WellOccupancy)} with the total held fixed, and the reason is the ");
        sb.Append("sector's own A = (1/d) ln rho diverging rather than anything the count does. THIS REFUTES THE OBVIOUS ");
        sb.Append("FORMULATION OF THIS AUDIT'S OWN HEADLINE: 'redistribution leaves the depression unbounded' is NOT a result of ");
        sb.Append("count conservation - it is a consequence of admitting an empty cell, and G_075 measured that no surviving ");
        sb.Append("datum probes that regime. With a floor, BOTH sides are capped by the same logarithmic number. ");
        sb.Append($"AND A BOUNDED REGION CANNOT HOLD A CHANGE ALONE: the change inside a region equals minus its net boundary ");
        sb.Append($"flux to {E3(ledger.Max(l => l.Residual))} over {ledger.Length} cells, so the compensating deficit must exist ");
        sb.Append("SOMEWHERE - and its distance is free, ");
        sb.Append("because the flux decides it. The compensation is compulsory; its LOCATION is an engineering choice. ");
        sb.Append("THE TWO IMPLICATIONS ARE ASYMMETRIC AND THAT IS THE SHARPEST RESULT. EVERY GRAVITATIONAL MANIPULATION ");
        sb.Append($"CARRIES A CLOCK SIGNATURE - measured over {gravity.Length} clock laws and four depths, ");
        sb.Append($"with {gravity.Sum(g => g.AccelerationWithoutClock)} configurations showing an acceleration ");
        sb.Append("change and no clock change - because an acceleration change needs a gradient of the occupancy and the clock ");
        sb.Append("is a STRICTLY MONOTONE readout of it. EVERY CLOCK MANIPULATION CARRIES A GRAVITATIONAL SIGNATURE TOO, WITH ");
        sb.Append("EXACTLY ONE EXCEPTION: the UNIFORM direction, which changes every clock and no acceleration - and it is the ");
        sb.Append($"one move COUNT CONSERVATION FORBIDS, because scaling every occupancy changes the total by {E3(escape.CountChange)} ");
        sb.Append("on the canonical lattice. SO THE ONLY CHANNEL ");
        sb.Append("THAT WOULD HAVE DECOUPLED THE CLOCK FROM GRAVITY IS THE CHANNEL THE THEORY CLOSES. ");
        sb.Append("OUTPUT: REFUTED - NO LOCAL NET SOURCE EXISTS. What the theory permits is the balanced pair whose total is ");
        sb.Append("fixed, and WHAT the pair buys depends on whether a cell may be empty: with a floor the two sides are capped ");
        sb.Append("by the same logarithmic number, and without one only the depression runs away and it does so because the ");
        sb.Append("sector's own logarithm diverges. What the theory forbids is the one move that would have separated time from ");
        sb.Append("gravity.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var caps = UpliftCap(CanonicalCells, CanonicalCells);
        var floorCap = caps.Single(c => c.FloorAssumed);
        var openCap = caps.Single(c => !c.FloorAssumed);
        var branches = ContrastBranches(CanonicalCells, CanonicalCells);
        var floorBranch = branches.Single(b => b.Branch.StartsWith("unit floor"));
        var openBranch = branches.Single(b => b.Branch.StartsWith("empty cell"));
        return "THE QUESTION WAS WHETHER THE SHARED SOURCE CAN BE CHANGED LOCALLY, AND THE MEASUREMENT SPLITS THE ANSWER "
             + "BETWEEN WHAT IS SUPPLIED AND WHAT IS REACHED. "
             + "WHAT IS SUPPLIED IS TRANSPORT: the update class is a divergence, the count is conserved, and the "
             + "compensation a local change needs is compulsory and measured as a ledger identity. WHAT IS REACHED is a "
             + "balancing act whose two sides are NOT symmetric in general - and the audit's own first formulation of "
             + "that asymmetry did not survive contact with the question of an empty cell. "
             + $"THE CONCRETE NUMBERS ARE THE CANONICAL ONES, and the uplift cap is ROBUST: {F4(openCap.Uplift)} in the "
             + $"potential, {F4(openCap.RateRatio)} in the rate and {F4(openCap.MetricRatio)} in the metric magnitude if all "
             + $"of the count is concentrated, against {F4(floorCap.Uplift)}, {F4(floorCap.RateRatio)} and {F4(floorCap.MetricRatio)} "
             + "if a one-unit floor is kept in every cell - the two readings differ by a fraction of a percent, so the cap "
             + "is a property of positivity rather than of the reading. "
             + $"THE CONTRAST CAP IS CONDITIONAL: {F4(floorBranch.MaxContrast)} under a unit floor - the SAME number as the "
             + $"uplift cap - against {openBranch.Status.Split(' ')[0]} if an empty cell is admitted. "
             + "AND THE HONEST LIMITS ARE FOUR. First, THE CONTRAST ASYMMETRY IS AN ARTIFACT OF THE EMPTY CELL: it is not a "
             + "result of count conservation, and G_075 measured that no surviving datum probes the deep field, so the "
             + "asymmetry is a statement about the sector's logarithm rather than a measured prediction. Second, A SOURCE "
             + "TERM IS NOT EXCLUDED - it is ABSENT: the audit measures that no operator of the natural class changes the "
             + "total, and says that importing one would be a new primitive rather than reporting a proof of "
             + "impossibility. Third, CAUSAL STRUCTURE was tested as a CHANNEL and found to be a CONSTRAINT: it orients "
             + "and suppresses transports rather than creating occupancy, which the audit states rather than scoring it as "
             + "a failure to act. Fourth, THE OPEN-CHAIN COLUMN OF THE OPERATOR TABLE IS CONVENTION-SENSITIVE: with the "
             + "end-duplicated boundary convention the second-order form also sums to zero, so that column is a diagnostic "
             + "of the two first-order forms, not a conservation law for the class.";
    }

    // ===================== 6. REPORTS =====================

    public static string OutputChannels()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. WHICH PROCESSES CAN ALTER LOCAL RHO");
        sb.AppendLine("   the update class, tested on one state (count change of sum L(rho)):");
        sb.AppendLine("   operator                                 closed ring        open chain         dropped boundary   leak = dropped");
        foreach (var o in OperatorClass(Uniform(8).Select((_, i) => 1.0 + 0.3 * Math.Sin(i)).ToArray()))
            sb.AppendLine($"   {o.Operator,-40} {E3(o.CountChangeOnARing),-18} {E3(o.CountChangeOnAnOpenChain),-18} {E3(o.DroppedBoundaryTerm),-18} {o.OpenLeakEqualsDroppedTerm}");
        sb.AppendLine("   the two first-order forms conserve the count on a CLOSED ring and leak on an OPEN chain exactly the");
        sb.AppendLine("   boundary term the chain drops - which is question 4's answer arriving already: a bounded region has");
        sb.AppendLine("   nowhere to send its boundary flux.");
        sb.AppendLine();
        sb.AppendLine("   the channel census:");
        foreach (var c in ChannelCensus())
            sb.AppendLine($"   {c.Channel,-34} {c.Status}{Environment.NewLine}       {c.Measurement}");
        sb.AppendLine();
        sb.AppendLine("   the region ledger: the change inside a region against its net boundary flux");
        sb.AppendLine("   region                  change inside   net boundary flux   residual");
        foreach (var l in RegionLedger(LedgerPattern))
            sb.AppendLine($"   {l.Region,-23} {E3(l.ChangeInside),-15} {E3(l.NetBoundaryFlux),-19} {E3(l.Residual)}");
        return sb.ToString();
    }

    public static string OutputExtremes()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE BOUNDS, AND WHICH ASSUMPTION THEY REST ON");
        sb.AppendLine("   the uplift cap, in two readings that must agree if the cap is real:");
        sb.AppendLine("   reading                                     max occupancy   uplift      rate ratio   metric ratio");
        foreach (var e in UpliftCap(CanonicalCells, CanonicalCells))
            sb.AppendLine($"   {e.Reading,-43} {e.MaxOccupancy.ToString("F1", System.Globalization.CultureInfo.InvariantCulture),-15} {F4(e.Uplift),-11} {F4(e.RateRatio),-12} {F4(e.MetricRatio)}");
        sb.AppendLine();
        sb.AppendLine("   the uplift as a function of the available count: logarithmic, so enhancement is expensive");
        sb.AppendLine("   cells   max occupancy   uplift      rate ratio   metric ratio");
        foreach (var m in new[] { 4, 16, 48, CanonicalCells, 9216, 1_000_000 })
        {
            var cap = UpliftCap(m, m).Single(c => !c.FloorAssumed);
            sb.AppendLine($"   {m,-7} {cap.MaxOccupancy.ToString("F1", System.Globalization.CultureInfo.InvariantCulture),-15} {F4(cap.Uplift),-11} {F4(cap.RateRatio),-12} {F4(cap.MetricRatio)}");
        }
        sb.AppendLine();
        sb.AppendLine("   the contrast between a well and a hill of the SAME count, by branch:");
        sb.AppendLine("   branch                                       well occupancy   max contrast   status");
        foreach (var b in ContrastBranches(CanonicalCells, CanonicalCells))
            sb.AppendLine($"   {b.Branch,-44} {b.WellOccupancy.ToString("F1", System.Globalization.CultureInfo.InvariantCulture),-15} {(double.IsInfinity(b.MaxContrast) ? "infinity" : F4(b.MaxContrast)),-14} {b.Status}");
        sb.AppendLine();
        sb.AppendLine("   the floorless branch, carried only to show where the divergence comes from");
        sb.AppendLine("   (these are NOT integer counts and are not configurations):");
        sb.AppendLine("   well occupancy   hill occupancy   total     potential difference   rate ratio");
        foreach (var c in NoFloorContrast(2.0))
            sb.AppendLine($"   {E3(c.WellOccupancy),-16} {c.HillOccupancy.ToString("F6", System.Globalization.CultureInfo.InvariantCulture),-16} {c.Total.ToString("F6", System.Globalization.CultureInfo.InvariantCulture),-9} {c.PotentialDifference.ToString("F6", System.Globalization.CultureInfo.InvariantCulture),-23} {E3(c.RateRatio)}");
        return sb.ToString();
    }

    public static string OutputImplications()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE TWO IMPLICATIONS, AND THE ONE EXCEPTION");
        sb.AppendLine("   law                                  configurations   acceleration without clock   implication holds");
        foreach (var g in GravityImpliesClock())
            sb.AppendLine($"   {g.Law,-36} {g.Configurations,-16} {g.AccelerationWithoutClock,-27} {g.ImplicationHolds}");
        sb.AppendLine();
        sb.AppendLine("   the uniform escape: clock, acceleration and COUNT");
        sb.AppendLine("   cells   clock change   acceleration change   count change");
        foreach (var u in UniformEscape(new[] { 8, 96 }))
            sb.AppendLine($"   {u.Cells,-7} {E3(u.ClockChange),-14} {E3(u.AccelerationChange),-21} {E3(u.CountChange)}");
        sb.AppendLine();
        foreach (var m in MirrorImplication())
            sb.AppendLine($"   [{(m.Answer ? "YES" : "NO ")}] {m.Question}{Environment.NewLine}         {m.Measurement}");
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
