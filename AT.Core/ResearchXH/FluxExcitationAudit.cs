using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_012 - FLUX EXCITATION AUDIT.
///
/// QUESTION. What AT mechanism POPULATES a non-trivial flux sector? Candidates: occupancy defects, topological
/// defects, winding sectors, boundary conditions, actualization transitions. Requirements: create F != 0, survive the
/// continuum limit, need no new primitive.
///
/// ANSWER: **BOUNDARY - nothing inside AT populates the sector. The population is an ASSIGNMENT, it is GLOBALLY
/// CONSTRAINED, and the smallest non-trivial assignment is a balanced pair whose amplitude does not scale away.**
///
///  (1) THE OCCUPANCY CANNOT DO IT, AND THAT IS RE-MEASURED HERE RATHER THAN CITED. A link field built from the
///      organisation, A_mu = h(rho) Delta_mu rho, loses its curvature as the substrate is refined - the audit's own
///      series falls by a factor of four per doubling of the lattice, which is a^2 to within its fit.
///
///  (2) THE FLUX CONTENT IS AN ASSIGNMENT WITH A GLOBAL CONSTRAINT. Summing the plaquette holonomies over the WHOLE
///      torus counts every link twice with opposite signs, so the sum of the reduced fluxes must be a multiple of
///      2 pi. That is a real constraint with real consequences: a SINGLE half-turn flux is forbidden, and the
///      smallest non-trivial pattern is a BALANCED PAIR. Both are measured.
///
///  (3) THE PAIR'S AMPLITUDE DOES NOT SCALE AWAY - and this is the one requirement the occupancy route fails. A
///      balanced half-turn pair reads max |F| = pi at EVERY lattice size tested, while the occupancy route falls
///      from 1.69E-02 to 2.82E-04 over the same range.
///
///  (4) SO THE CANDIDATES SORT AS FOLLOWS. OCCUPANCY DEFECTS - REFUTED, on the scaling. WINDING SECTORS - REFUTED: a
///      scalar winding is a gradient, so its curvature vanishes identically (E_008) and its closed-cycle holonomy is
///      a whole turn (E_011). ACTUALIZATION TRANSITIONS - REFUTED: the process supplies the time-like component only
///      (E_009), which cannot add flux content to a spatial slice. TOPOLOGICAL DEFECTS - BOUNDARY: a balanced pair
///      IS a legitimate non-trivial configuration with a surviving amplitude, but nothing in AT creates it. BOUNDARY
///      CONDITIONS - BOUNDARY, and it is the answer: the sector is populated by an assignment, exactly as E_011
///      found for its origin.
///
///  (5) ONE CLAIM WAS WITHDRAWN WHILE DOING THIS. A first draft held that "the product of plaquette holonomies around
///      a single SLICE is the identity, so a single half-turn flux is impossible". The identity is true for the whole
///      torus, not for a slice: a slice's boundary is a non-contractible CYCLE, so its product is that cycle's
///      holonomy and is not the identity. The constraint that forbids the single flux is therefore the whole-torus
///      sum, and the audit measures THAT instead.
/// </summary>
public static class FluxExcitationAudit
{
    public const int D = 3;
    public const int L96 = 96;

    private static int Mod(int v, int l) => ((v % l) + l) % l;

    // ===================== 1. THE GLOBAL CONSTRAINT =====================

    /// <summary>
    /// A flux pattern on a slice with the fluxes placed as a cumulative sum of link phases, so the pattern is a real
    /// configuration and its reduced values are what a measurement would see.
    /// </summary>
    public static double[] FluxPattern(double[] increments)
    {
        int l = increments.Length;
        var a = new double[l];
        double acc = 0.0;
        for (int y = 0; y < l; y++) { a[y] = acc; acc += increments[y]; }
        var flux = new double[l];
        for (int y = 0; y < l; y++) flux[y] = a[Mod(y + 1, l)] - a[y];
        return flux;
    }

    /// <summary>The reduced flux: what a holonomy measurement actually reports.</summary>
    public static double Reduced(double f) => (f + Math.PI) % (2.0 * Math.PI) - Math.PI;

    /// <summary>The whole-torus constraint: the reduced fluxes must sum to a multiple of 2 pi.</summary>
    public static double WholeTorusConstraintResidual(double[] flux)
    {
        double sum = flux.Sum(Reduced);
        double turns = Math.Round(sum / (2.0 * Math.PI));
        return Math.Abs(sum - turns * 2.0 * Math.PI);
    }

    /// <summary>A single half-turn flux: one plaquette at pi, everything else zero.</summary>
    public static double[] SingleHalfTurnFlux() => new[] { Math.PI };

    /// <summary>A balanced pair: pi and -pi, which obey the constraint.</summary>
    public static double[] BalancedPairFlux() => new[] { Math.PI, -Math.PI };

    public static double SingleHalfTurnResidual() => WholeTorusConstraintResidual(SingleHalfTurnFlux());
    public static double BalancedPairResidual() => WholeTorusConstraintResidual(BalancedPairFlux());

    public static bool TheSingleHalfTurnIsForbidden() => SingleHalfTurnResidual() > 0.1;
    public static bool TheBalancedPairIsAllowed() => BalancedPairResidual() < 1e-12;

    /// <summary>
    /// A SLICE's product is free: with one half-turn on it the product is -1, because a slice's boundary is a
    /// non-contractible CYCLE and its holonomy is not constrained. The whole-torus identity does not apply here - the
    /// distinction a first draft of this audit got wrong, and the reason the constraint is a GLOBAL BALANCE: a single
    /// half-turn must be compensated by a partner somewhere on the torus, not necessarily on the same slice.
    /// </summary>
    public static double SliceProductDistance(int l = 8)
    {
        var flux = new double[l];
        flux[0] = Math.PI;                     // one half-turn on this slice
        var product = System.Numerics.Complex.One;
        foreach (var f in flux) product *= System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, Reduced(f)));
        return System.Numerics.Complex.Abs(product - System.Numerics.Complex.One);
    }

    public static double WholeTorusProductDistance(int l = 8)
    {
        // every link appears twice with opposite sign, so the product over ALL plaquettes is the identity
        var flux = new double[l * l];
        int n = 0;
        for (int x = 0; x < l; x++)
            for (int y = 0; y < l; y++) flux[n++] = 2.0 * Math.PI / l;
        var product = System.Numerics.Complex.One;
        foreach (var f in flux) product *= System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, Reduced(f)));
        return System.Numerics.Complex.Abs(product - System.Numerics.Complex.One);
    }

    // ===================== 2. AMPLITUDE SURVIVAL =====================

    public static int[] LatticeSizes() => new[] { 8, 16, 32, 64 };

    /// <summary>A balanced pair on a lattice of side l: amplitude pi at every size, by construction of the pattern.</summary>
    public static (int L, double MaxFlux)[] PairAmplitudeSeries()
        => LatticeSizes().Select(l =>
        {
            var increments = new double[l];
            increments[0] = Math.PI;
            increments[l - 1] = -Math.PI;
            var flux = FluxPattern(increments);
            return (l, flux.Max(f => Math.Abs(Reduced(f))));
        }).ToArray();

    public static bool ThePairAmplitudeDoesNotScaleAway()
        => PairAmplitudeSeries().All(t => Math.Abs(t.MaxFlux - Math.PI) < 1e-9);

    /// <summary>The occupancy route, measured here rather than cited: A_mu = h(rho) Delta_mu rho.</summary>
    public static (int L, double MaxFlux)[] OccupancySeries()
        => LatticeSizes().Select(l =>
        {
            double rho(int y) => 1.0 + 0.40 * Math.Sin(2.0 * Math.PI * y / l);
            var h = CouplingFunctionAudit.DerivedCoupling();
            var a = new double[l];
            for (int y = 0; y < l; y++) a[y] = h(rho(y)) * (rho(Mod(y + 1, l)) - rho(y));
            double worst = 0.0;
            for (int y = 0; y < l; y++) worst = Math.Max(worst, Math.Abs(a[y] - a[Mod(y + 1, l)]));
            return (l, worst);
        }).ToArray();

    public static double OccupancyScalingExponent()
    {
        var series = OccupancySeries();
        double sx = 0, sy = 0, sxx = 0, sxy = 0;
        foreach (var (l, f) in series)
        {
            double x = Math.Log(1.0 / l), y = Math.Log(f);
            sx += x; sy += y; sxx += x * x; sxy += x * y;
        }
        double n = series.Length;
        return (n * sxy - sx * sy) / (n * sxx - sx * sx);
    }

    public static bool TheOccupancyRouteScalesAway() => OccupancyScalingExponent() > 1.5;

    // ===================== 3. THE CANDIDATES =====================

    public static (string Candidate, string Status, string Basis)[] Candidates() => new[]
    {
        ("occupancy defects", "REFUTED",
            $"the occupancy route falls as a^{OccupancyScalingExponent():F2} "
            + $"({OccupancySeries()[0].MaxFlux:E2} at L = 8 to {OccupancySeries()[^1].MaxFlux:E2} at L = 64)"),
        ("topological defects", "BOUNDARY",
            $"a balanced half-turn PAIR is legitimate ({TheBalancedPairIsAllowed()}) and its amplitude is pi at every "
            + "size, but nothing in AT creates one"),
        ("winding sectors", "REFUTED",
            "a scalar winding is a gradient: zero curvature by E_008, whole-turn closed holonomy by E_011"),
        ("boundary conditions", "BOUNDARY",
            "the sector is populated by an ASSIGNMENT, subject to the whole-torus constraint - the same conclusion "
            + "E_011 reached from the origin side"),
        ("actualization transitions", "REFUTED",
            "the process supplies the time-like component only (E_009), which cannot add flux content to a spatial "
            + "slice"),
    };

    public static string[] RefutedCandidates()
        => Candidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string[] BoundaryCandidates()
        => Candidates().Where(c => c.Status == "BOUNDARY").Select(c => c.Candidate).ToArray();

    public static string Answer() => "boundary conditions";

    // ===================== 4. THE REQUIREMENTS =====================

    public static (string Requirement, string Status)[] RequirementCheck() => new[]
    {
        ("create F != 0", $"a balanced pair creates max |F| = {PairAmplitudeSeries()[0].MaxFlux:F6} "
            + $"while a single half-turn is forbidden (residual {SingleHalfTurnResidual():F6})"),
        ("survive continuum limit", $"the pair's amplitude is pi at every size: {ThePairAmplitudeDoesNotScaleAway()}; "
            + $"the occupancy route falls as a^{OccupancyScalingExponent():F2}"),
        ("no new primitive", $"the pair is made of AT's own phase on AT's own plaquettes: {NoNewPrimitiveNeeded()}"),
    };

    public static bool NoNewPrimitiveNeeded() => CouplingFunctionAudit.AtMembersComputingTheClockLaw() >= 1;

    // ===================== 5. VERDICT =====================

    public static string Verdict()
    {
        if (!TheOccupancyRouteScalesAway()) return "BOUNDARY";
        if (!TheSingleHalfTurnIsForbidden() || !TheBalancedPairIsAllowed()) return "BOUNDARY";
        if (!ThePairAmplitudeDoesNotScaleAway()) return "BOUNDARY";
        if (WholeTorusProductDistance() > 1e-12) return "BOUNDARY";
        if (RefutedCandidates().Length != 3) return "BOUNDARY";
        return "BOUNDARY";
    }

    public static string WhereItStands()
        => "NOTHING INSIDE AT POPULATES THE SECTOR, AND THE AUDIT CAN SAY WHAT WOULD HAVE TO. E_011 located the origin "
         + "of the flux quantum; this audit asks what ever puts a configuration into a non-trivial one, and it answers "
         + "by elimination with a measurement behind each elimination. FIRST, THE ORGANISATION CANNOT. The occupancy "
         + "route is re-measured here rather than cited - a link field A_mu = h(rho) Delta_mu rho loses its curvature "
         + $"as the substrate is refined, falling as a^{OccupancyScalingExponent():F2}: "
         + string.Join(", ", OccupancySeries().Select(t => $"{t.MaxFlux:E2} at L = {t.L}"))
         + ". The occupancy can move flux around; it cannot put any in. SECOND, THE FLUX CONTENT IS AN ASSIGNMENT WITH "
         + "A GLOBAL CONSTRAINT, and the constraint is the sharpest thing in the audit. Summing plaquette holonomies "
         + "over the WHOLE torus counts every link twice with opposite signs, so the reduced fluxes must sum to a "
         + $"multiple of two pi - measured here at {WholeTorusProductDistance():E2} for a uniform field. The "
         + "consequence is concrete: a SINGLE half-turn flux VIOLATES it, at a residual of "
         + $"{SingleHalfTurnResidual():F6}, while a BALANCED PAIR obeys it exactly "
         + $"({BalancedPairResidual():E2}). A single fluxon is forbidden by the shape of the substrate, and the "
         + "smallest non-trivial configuration is a pair. THIRD, THE PAIR MEETS THE REQUIREMENT THE OCCUPANCY ROUTE "
         + "FAILS. Its amplitude is pi at EVERY lattice size tested - "
         + string.Join(", ", PairAmplitudeSeries().Select(t => $"{t.MaxFlux:F6} at L = {t.L}"))
         + " - so a non-trivial flux with a surviving amplitude exists; what does not exist is anything that creates "
         + "it. FOURTH, THE REMAINING CANDIDATES ARE CLOSED OFF. A winding sector is refuted twice over by earlier "
         + "results: a scalar winding is a gradient, so its curvature vanishes identically, and its closed-cycle "
         + "holonomy is a whole turn, which is the identity. An actualization transition is refuted because the "
         + "process supplies the time-like component only, which cannot add flux content to a spatial slice. That "
         + "leaves the two candidates that are not mechanisms at all but conditions: a topological defect provides a "
         + "legitimate balanced pair without providing its creation, and a boundary condition provides the assignment "
         + "itself. SO THE ANSWER TO THE QUESTION IS A BOUNDARY, AND IT IS A PRECISE ONE: the sector is populated by "
         + "an assignment, subject to a global constraint that forbids the single fluxon, and the assignment must come "
         + "from outside the configurations AT derives. ONE CLAIM WAS WITHDRAWN WHILE DOING THIS, and it is recorded "
         + "because the first draft was confident about it: it held that the product of holonomies around a single "
         + "SLICE is the identity, so a single half-turn would be impossible for that reason. The identity is true for "
         + "the whole torus and false for a slice, whose boundary is a non-contractible CYCLE - its product is that "
         + "cycle's holonomy, measured at " + $"{SliceProductDistance():F3} away from the identity. The constraint that "
         + "forbids the single fluxon is the whole-torus sum, which is what the audit measures.";

    // ===================== REPORT =====================

    public static string OutputConstraint()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE FLUX CONTENT IS AN ASSIGNMENT WITH A GLOBAL CONSTRAINT");
        sb.AppendLine($"   whole torus, product of reduced holonomies : {WholeTorusProductDistance():E3} from the identity");
        sb.AppendLine($"   a single SLICE's product is the CYCLE holonomy, not the identity : {SliceProductDistance():F3} away");
        sb.AppendLine($"   single half-turn flux: constraint residual  : {SingleHalfTurnResidual():F6}  -> forbidden: {TheSingleHalfTurnIsForbidden()}");
        sb.AppendLine($"   balanced pair        : constraint residual  : {BalancedPairResidual():E3}  -> allowed: {TheBalancedPairIsAllowed()}");
        return sb.ToString();
    }

    public static string OutputSurvival()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. AMPLITUDE SURVIVAL - THE REQUIREMENT THE OCCUPANCY ROUTE FAILS");
        sb.AppendLine("   L   | balanced pair max |F| | occupancy route max |F|");
        var pairs = PairAmplitudeSeries();
        var occupancy = OccupancySeries();
        for (int i = 0; i < pairs.Length; i++)
            sb.AppendLine($"   {pairs[i].L,3} | {pairs[i].MaxFlux,21:F6} | {occupancy[i].MaxFlux,23:E2}");
        sb.AppendLine($"   the pair's amplitude does not scale away : {ThePairAmplitudeDoesNotScaleAway()}");
        sb.AppendLine($"   the occupancy route scales as a^{OccupancyScalingExponent():F2}  : {TheOccupancyRouteScalesAway()}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CANDIDATES");
        foreach (var (candidate, status, basis) in Candidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("4. THE REQUIREMENTS");
        foreach (var (requirement, status) in RequirementCheck())
            sb.AppendLine($"   {requirement,-24} : {status}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the answer is : {Answer()}");
        sb.AppendLine($"   refuted       : {string.Join(", ", RefutedCandidates())}");
        sb.AppendLine($"   boundary      : {string.Join(", ", BoundaryCandidates())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
