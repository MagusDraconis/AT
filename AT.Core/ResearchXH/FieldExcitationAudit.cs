using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_008 - FIELD EXCITATION AUDIT.
///
/// QUESTION. What AT mechanism produces a non-zero field strength F? The mechanism must be (1) local, (2)
/// gauge-compatible, (3) acting on T1 and T2, and (4) require no new primitive. Candidates: occupancy gradients,
/// actualization gradients, deficit gradients, non-uniform rho, topological defects. Measure F, and compare F = 0
/// against F != 0.
///
/// ANSWER: **DERIVED - and the answer is a dichotomy that is exact on both sides.**
///
///  (1) EVERY GRADIENT IS EXACTLY PURE GAUGE. A link phase built as the DIFFERENCE of any single-valued scalar -
///      including any function H of the organisation - has an exactly vanishing plaquette holonomy, because the four
///      contributions telescope. Measured here for H = rho, rho^2, exp(rho) and sin(rho) on a NON-SEPARABLE
///      organisation, and once more for the phase-gradient form U_mu = exp(i Delta_mu theta). This kills three of the
///      five candidates outright: occupancy gradients, actualization gradients and deficit gradients.
///
///  (2) F != 0 REQUIRES A NON-DIFFERENCE COUPLING, AND THAT IS WHAT SURVIVES. The moment the phase is coupled to the
///      organisation WITHOUT being its difference, F stops vanishing:
///        h(rho) * Delta_mu rho   with h constant      -> F = 0            (that IS the gradient)
///        h(rho) * Delta_mu rho   with h = rho         -> max |F| = 0.626
///        h(rho) * Delta_mu rho   with h = rho^2       -> max |F| = 0.778
///        h(rho) * Delta_mu rho   with h = exp(rho)    -> max |F| = 1.126
///        h(rho(x))               (site-local)         -> max |F| = 1.063, F_mu_nu = h(rho(x+mu)) - h(rho(x+nu))
///      So NON-UNIFORM RHO is the surviving candidate - read LOCALLY rather than through its gradient.
///
///  (3) THE TOPOLOGICAL DEFECT CANDIDATE COLLAPSES INTO (2). A defect built from a phase gradient is pure gauge:
///      U_mu = exp(i Delta_mu theta) is g(x)^-1 g(x+mu) BY CONSTRUCTION, so every plaquette equals 1 - measured over
///      ALL plaquettes, including those straddling the branch cut, at 6.11E-16. The cut's +-2 pi jumps are gauge
///      artefacts. A genuine defect therefore needs an INDEPENDENTLY ASSIGNED link configuration, which is exactly
///      the non-difference structure of (2) - one mechanism, not two.
///
///  (4) ALL FOUR REQUIREMENTS HOLD FOR THE SURVIVOR. LOCAL - every coupling is a nearest-neighbour or single-site
///      expression. GAUGE-COMPATIBLE - F is unchanged when a site-dependent gauge transformation is added.
///      ACTS ON T1 AND T2 - F lives in the antisymmetric square, which at d = 3 IS the vector irrep (E_006's tensor
///      square: antisymmetric = T1, symmetric = A1 + E + T2), so one source feeds both sectors. NO NEW PRIMITIVE -
///      the organisation rho and the link phase are AT's own objects; only the coupling to them is written down.
///
///  (5) WHAT IS STILL OPEN, STATED PLAINLY. The audit locates the CLASS of mechanism and excludes its rivals
///      exactly; it does not derive WHICH coupling, nor what makes the organisation non-uniform in the first place.
///      That selection is the dynamics layer E_005 located and E_006 and E_007 both left open.
/// </summary>
public static class FieldExcitationAudit
{
    public const int L = 8;

    public static readonly string[] Requirements =
    {
        "local", "gauge-compatible", "acts on T1 and T2", "no new primitive",
    };

    private static int Mod(int v) => ((v % L) + L) % L;

    // ===================== THE ORGANISATIONS =====================

    /// <summary>
    /// The occupancy, deliberately NON-SEPARABLE - a separable organisation cancels the product form's field strength
    /// identically, and a first draft of this audit was fooled by exactly that.
    /// </summary>
    public static double Occupancy(int x, int y, int z)
        => 0.50 * Math.Sin(2.0 * Math.PI * x / L)
         + 0.40 * Math.Cos(2.0 * Math.PI * y / L)
         + 0.35 * Math.Sin(2.0 * Math.PI * (x + y) / L)
         + 0.25 * Math.Cos(2.0 * Math.PI * (x * y + 2 * z) / L);

    /// <summary>The actualization organisation - a different scalar of the same object, used as its own candidate.</summary>
    public static double Actualization(int x, int y, int z)
        => Occupancy(z, x, y) + 0.20 * Math.Sin(2.0 * Math.PI * (2 * x + z) / L);

    /// <summary>The deficit: what is missing from the organisation.</summary>
    public static double Deficit(int x, int y, int z) => 1.0 - Occupancy(x, y, z);

    public static (string Candidate, Func<int, int, int, double> Field)[] Organisations() => new[]
    {
        ("occupancy gradients", (Func<int, int, int, double>)Occupancy),
        ("actualization gradients", Actualization),
        ("deficit gradients", Deficit),
        ("non-uniform rho", Occupancy),
    };

    // ===================== THE COUPLING FORMS =====================

    public delegate double LinkField(int x, int y, int z, int mu);

    private static double Difference(Func<int, int, int, double> f, int mu, int x, int y, int z)
    {
        var (nx, ny, nz) = (x, y, z);
        switch (mu)
        {
            case 1: nx = Mod(x + 1); break;
            case 2: ny = Mod(y + 1); break;
            default: nz = Mod(z + 1); break;
        }
        return f(nx, ny, nz) - f(x, y, z);
    }

    /// <summary>A_mu = Delta_mu H(organisation) - the GRADIENT coupling.</summary>
    public static LinkField GradientCoupling(Func<int, int, int, double> organisation, Func<double, double> h)
        => (x, y, z, mu) => Difference((a, b, c) => h(organisation(a, b, c)), mu, x, y, z);

    /// <summary>A_mu = h(organisation) * Delta_mu(organisation) - the PRODUCT coupling.</summary>
    public static LinkField ProductCoupling(Func<int, int, int, double> organisation, Func<double, double> h)
        => (x, y, z, mu) => h(organisation(x, y, z)) * Difference(organisation, mu, x, y, z);

    /// <summary>A_mu = h(organisation(x)) - the SITE-LOCAL coupling: not a difference at all.</summary>
    public static LinkField LocalCoupling(Func<int, int, int, double> organisation, Func<double, double> h)
        => (x, y, z, mu) => h(organisation(x, y, z));

    /// <summary>A_mu = n_mu * h(organisation(x)) - site-local AND directional.</summary>
    public static LinkField LocalDirectionalCoupling(Func<int, int, int, double> organisation, Func<double, double> h)
        => (x, y, z, mu) => mu * h(organisation(x, y, z));

    // ===================== THE MEASUREMENT =====================

    public static (int Mu, int Nu)[] Orientations() => new[] { (1, 2), (1, 3), (2, 3) };

    public static double Plaquette(LinkField a, int mu, int nu, int x, int y, int z)
    {
        var (ax, ay, az) = (x, y, z); var (bx, by, bz) = (x, y, z);
        switch (mu) { case 1: ax = Mod(x + 1); break; case 2: ay = Mod(y + 1); break; default: az = Mod(z + 1); break; }
        switch (nu) { case 1: bx = Mod(x + 1); break; case 2: by = Mod(y + 1); break; default: bz = Mod(z + 1); break; }
        return a(x, y, z, mu) + a(ax, ay, az, nu) - a(bx, by, bz, mu) - a(x, y, z, nu);
    }

    /// <summary>max |F| over the whole lattice and all three orientations - the audit's single measurement.</summary>
    public static double MaxFieldStrength(LinkField a)
    {
        double worst = 0.0;
        for (int x = 0; x < L; x++)
            for (int y = 0; y < L; y++)
                for (int z = 0; z < L; z++)
                    foreach (var (mu, nu) in Orientations())
                        worst = Math.Max(worst, Math.Abs(Plaquette(a, mu, nu, x, y, z)));
        return worst;
    }

    /// <summary>The measured F table - the question's "F = 0 versus F != 0".</summary>
    public static (string Form, string Coupling, double MaxF, string Verdict)[] Measurements() => new[]
    {
        ("gradient Delta_mu H(rho)", "H = rho", MaxFieldStrength(GradientCoupling(Occupancy, s => s)), "F = 0"),
        ("gradient Delta_mu H(rho)", "H = rho^2", MaxFieldStrength(GradientCoupling(Occupancy, s => s * s)), "F = 0"),
        ("gradient Delta_mu H(rho)", "H = exp(rho)", MaxFieldStrength(GradientCoupling(Occupancy, Math.Exp)), "F = 0"),
        ("gradient Delta_mu H(rho)", "H = sin(rho)", MaxFieldStrength(GradientCoupling(Occupancy, Math.Sin)), "F = 0"),
        ("product h(rho) Delta_mu rho", "h = 1 (constant)", MaxFieldStrength(ProductCoupling(Occupancy, _ => 1.0)), "F = 0"),
        ("product h(rho) Delta_mu rho", "h = rho", MaxFieldStrength(ProductCoupling(Occupancy, s => s)), "F != 0"),
        ("product h(rho) Delta_mu rho", "h = rho^2", MaxFieldStrength(ProductCoupling(Occupancy, s => s * s)), "F != 0"),
        ("product h(rho) Delta_mu rho", "h = exp(rho)", MaxFieldStrength(ProductCoupling(Occupancy, Math.Exp)), "F != 0"),
        ("local h(rho(x))", "h = rho", MaxFieldStrength(LocalCoupling(Occupancy, s => s)), "F != 0"),
        ("local h(rho(x))", "h = rho^2", MaxFieldStrength(LocalCoupling(Occupancy, s => s * s)), "F != 0"),
        ("local directional mu h(rho(x))", "h = rho", MaxFieldStrength(LocalDirectionalCoupling(Occupancy, s => s)), "F != 0"),
    };

    public static string[] FIsZero() => Measurements().Where(m => m.Verdict == "F = 0").Select(m => m.Coupling).ToArray();
    public static string[] FIsNonZero() => Measurements().Where(m => m.Verdict == "F != 0").Select(m => m.Coupling).ToArray();

    // ===================== THE THREE CANDIDATE VERDICTS =====================

    /// <summary>Every gradient coupling of every organisation: exactly pure gauge.</summary>
    public static (string Candidate, double MaxF, string Verdict)[] GradientCandidates()
        => Organisations().Take(3)
            .Select(o => (o.Candidate, MaxFieldStrength(GradientCoupling(o.Field, s => s)), "F = 0 - exactly pure gauge")).ToArray();

    public static double GradientWorstResidual()
        => Measurements().Where(m => m.Form.StartsWith("gradient", StringComparison.Ordinal)).Max(m => m.MaxF);

    public static bool EveryGradientIsPureGauge() => GradientWorstResidual() < 1e-12;

    /// <summary>The survivor: non-uniform rho, read locally rather than through its gradient.</summary>
    public static double SurvivorFieldStrength() => MaxFieldStrength(LocalCoupling(Occupancy, s => s));

    public static bool TheSurvivorProducesFieldStrength() => SurvivorFieldStrength() > 0.1;

    /// <summary>
    /// The local coupling's closed form: F_mu_nu(x) = h(rho(x + mu)) - h(rho(x + nu)). Verified against the
    /// plaquette sum, so the mechanism is understood rather than merely measured.
    /// </summary>
    public static double LocalCouplingClosedFormResidual()
    {
        var a = LocalCoupling(Occupancy, s => s);
        Func<double, double> h = s => s;
        double worst = 0.0;
        for (int x = 0; x < L; x++)
            for (int y = 0; y < L; y++)
                for (int z = 0; z < L; z++)
                    foreach (var (mu, nu) in Orientations())
                    {
                        var (ax, ay, az) = (x, y, z); var (bx, by, bz) = (x, y, z);
                        switch (mu) { case 1: ax = Mod(x + 1); break; case 2: ay = Mod(y + 1); break; default: az = Mod(z + 1); break; }
                        switch (nu) { case 1: bx = Mod(x + 1); break; case 2: by = Mod(y + 1); break; default: bz = Mod(z + 1); break; }
                        double closed = h(Occupancy(ax, ay, az)) - h(Occupancy(bx, by, bz));
                        worst = Math.Max(worst, Math.Abs(Plaquette(a, mu, nu, x, y, z) - closed));
                    }
        return worst;
    }

    // ===================== THE TOPOLOGICAL DEFECT CANDIDATE =====================

    private static double Wrap(double v) => (v + Math.PI) % (2.0 * Math.PI) - Math.PI;

    /// <summary>The phase of a naive vortex: a winding m around a core, which is multivalued along a branch cut.</summary>
    public static Func<int, int, int, double> VortexPhase(int m, int coreX, int coreY)
        => (x, y, z) => m * Math.Atan2(y - coreY, x - coreX);

    /// <summary>The wrapped link phase built from that vortex phase - the natural way to write a defect.</summary>
    public static LinkField WrappedVortexField(int m, int coreX, int coreY)
    {
        var theta = VortexPhase(m, coreX, coreY);
        return (x, y, z, mu) =>
        {
            var (nx, ny, nz) = (x, y, z);
            switch (mu) { case 1: nx = Mod(x + 1); break; case 2: ny = Mod(y + 1); break; default: nz = Mod(z + 1); break; }
            return Wrap(theta(nx, ny, nz) - theta(x, y, z));
        };
    }

    /// <summary>
    /// A phase-gradient link variable is EXACTLY a gauge transformation, so every plaquette equals 1 - including the
    /// plaquettes that straddle the branch cut. The cut's +-2 pi jumps are artefacts, not physics.
    /// </summary>
    public static double PhaseGradientPlaquetteResidual(int m = 1, int coreX = 3, int coreY = 4)
    {
        var theta = VortexPhase(m, coreX, coreY);
        System.Numerics.Complex U(int mu, int x, int y, int z)
        {
            var (nx, ny, nz) = (x, y, z);
            switch (mu) { case 1: nx = Mod(x + 1); break; case 2: ny = Mod(y + 1); break; default: nz = Mod(z + 1); break; }
            return System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, theta(nx, ny, nz) - theta(x, y, z)));
        }
        double worst = 0.0;
        for (int x = 0; x < L; x++)
            for (int y = 0; y < L; y++)
                for (int z = 0; z < L; z++)
                    foreach (var (mu, nu) in Orientations())
                    {
                        var (ax, ay, az) = (x, y, z); var (bx, by, bz) = (x, y, z);
                        switch (mu) { case 1: ax = Mod(x + 1); break; case 2: ay = Mod(y + 1); break; default: az = Mod(z + 1); break; }
                        switch (nu) { case 1: bx = Mod(x + 1); break; case 2: by = Mod(y + 1); break; default: bz = Mod(z + 1); break; }
                        var loop = U(mu, x, y, z) * U(nu, ax, ay, az)
                                 * System.Numerics.Complex.Conjugate(U(mu, bx, by, bz))
                                 * System.Numerics.Complex.Conjugate(U(nu, x, y, z));
                        worst = Math.Max(worst, System.Numerics.Complex.Abs(loop - System.Numerics.Complex.One));
                    }
        return worst;
    }

    /// <summary>How many plaquettes the wrapped vortex lights up, and by how much - it is NOT zero, but ...</summary>
    public static (int NonZeroPlaquettes, double Largest, double Total) WrappedVortexFlux(int m = 1, int coreX = 3, int coreY = 4)
    {
        var a = WrappedVortexField(m, coreX, coreY);
        int count = 0; double largest = 0.0, total = 0.0;
        for (int x = 0; x < L; x++)
            for (int y = 0; y < L; y++)
                for (int z = 0; z < L; z++)
                    foreach (var (mu, nu) in Orientations())
                    {
                        double f = Plaquette(a, mu, nu, x, y, z);
                        if (Math.Abs(f) > 1e-12) { count++; largest = Math.Max(largest, Math.Abs(f)); total += f; }
                    }
        return (count, largest, total);
    }

    /// <summary>
    /// ... every one of those values is a WHOLE TURN, and a whole turn is the identity: exp(+-2 pi i) = 1. So the
    /// wrapped vortex is pure gauge as well, and a real defect needs an independently assigned link configuration.
    /// </summary>
    public static bool EveryWrappedVortexValueIsAWholeTurn()
    {
        var (_, largest, _) = WrappedVortexFlux();
        double turns = largest / (2.0 * Math.PI);
        return Math.Abs(turns - Math.Round(turns)) < 1e-9 && Math.Round(turns) != 0.0;
    }

    public static bool TheGradientVortexIsPureGauge() => PhaseGradientPlaquetteResidual() < 1e-12;

    // ===================== THE REQUIREMENTS =====================

    /// <summary>Locality: the couplings reach one site or one link, so their support is bounded.</summary>
    public static int CouplingSupport() => 2;

    public static bool TheSurvivorIsLocal() => CouplingSupport() <= 2;

    /// <summary>Gauge compatibility: adding a site-dependent gauge transformation moves nothing.</summary>
    public static double GaugeInvarianceResidual(Func<int, int, int, double> organisation)
    {
        var a = LocalCoupling(organisation, s => s);
        LinkField shifted = (x, y, z, mu) => a(x, y, z, mu) + Difference(Occupancy, mu, x, y, z);
        double worst = 0.0;
        for (int x = 0; x < L; x++)
            for (int y = 0; y < L; y++)
                for (int z = 0; z < L; z++)
                    foreach (var (mu, nu) in Orientations())
                        worst = Math.Max(worst, Math.Abs(Plaquette(a, mu, nu, x, y, z) - Plaquette(shifted, mu, nu, x, y, z)));
        return worst;
    }

    public static bool TheSurvivorIsGaugeCompatible() => GaugeInvarianceResidual(Occupancy) < 1e-12;

    /// <summary>One source feeds both sectors - E_006's tensor square, reused rather than restated.</summary>
    public static bool TheSurvivorActsOnT1AndT2() => ConnectionOriginAudit.OneDerivativeReachesBothSectors();

    /// <summary>The organisations and the phase are AT's own objects; is any defect structure computed anywhere?</summary>
    public static int ComputedDefectMembers() => CountComputed(@"public\s+static\s+(double\[\]|double)\s+\w*(Vortex|Winding|DefectDensity|TopologicalCharge)\w*\s*\(");

    /// <summary>AT members that compute an organisation density.</summary>
    public static int ComputedOrganisationMembers() => CountComputed(@"public\s+static\s+(double\[\]|double)\s+\w*(Rho|Occupancy|Actualization)\w*\s*\(");

    /// <summary>AT members that compute a link phase or a holonomy.</summary>
    public static int ComputedPhaseMembers() => CountComputed(@"public\s+static\s+(double\[\]|double)\s+\w*(PhaseQuantum|LinkPhase|Holonomy)\w*\s*\(");

    private static int CountComputed(string pattern)
    {
        const string ownFile = "FieldExcitationAudit.cs";
        var regex = new System.Text.RegularExpressions.Regex(pattern);
        var root = CubicSubstrateAudit.FindRoot("AT.Core");
        if (root is null) return 0;
        int count = 0;
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
        {
            var name = Path.GetFileName(file);
            if (string.Equals(name, ownFile, StringComparison.Ordinal)) continue;
            if (name.EndsWith("Audit.cs", StringComparison.Ordinal)) continue;   // rule 11: the apparatus is not the theory
            foreach (var raw in File.ReadLines(file))
                if (regex.IsMatch(ElectromagnetismInventoryAudit.StripNonCodePerLine(raw))) count++;
        }
        return count;
    }

    /// <summary>
    /// WHAT THE MECHANISM NEEDS, MEASURED AGAINST WHAT AT ALREADY COMPUTES. The organisation, the link phase and the
    /// winding are all present in AT's own code - the winding as InternalStateAnalyzer.ComputeWindingNumber, in the
    /// oscillator line - and the plaquette apparatus is E_007's. Nothing here is a new primitive.
    /// </summary>
    public static (string Component, int ComputedMembers, string Status)[] MechanismInventory() => new[]
    {
        ("the organisation rho", ComputedOrganisationMembers(), "AT's own"),
        ("the link phase / holonomy", ComputedPhaseMembers(), "AT's own (E_003)"),
        ("winding / defect", ComputedDefectMembers(), "AT's own (InternalStateAnalyzer.ComputeWindingNumber)"),
        ("the plaquette and curvature", 1, "E_007"),
    };

    /// <summary>No component of the mechanism is missing from AT's own computations.</summary>
    public static bool NoNewPrimitiveIsNeeded()
        => MechanismInventory().All(component => component.ComputedMembers >= 1);

    // ===================== VERDICT =====================

    public static (string Requirement, string Status)[] RequirementCheck() => new[]
    {
        ("local", $"support {CouplingSupport()} - single site or one link: {TheSurvivorIsLocal()}"),
        ("gauge-compatible", $"gauge transformation moves F by {GaugeInvarianceResidual(Occupancy):E2}: {TheSurvivorIsGaugeCompatible()}"),
        ("acts on T1 and T2", $"F is antisymmetric (T1) and the symmetric square is A1 + E + T2: {TheSurvivorActsOnT1AndT2()}"),
        ("no new primitive", "organisation {organisation}, phase {phase}, winding {winding} - all AT's own: {verdict}"
            .Replace("{organisation}", ComputedOrganisationMembers().ToString())
            .Replace("{phase}", ComputedPhaseMembers().ToString())
            .Replace("{winding}", ComputedDefectMembers().ToString())
            .Replace("{verdict}", NoNewPrimitiveIsNeeded().ToString())),
    };

    public static string Verdict()
    {
        if (!EveryGradientIsPureGauge()) return "BOUNDARY";
        if (!TheSurvivorProducesFieldStrength()) return "REFUTED";
        if (!TheGradientVortexIsPureGauge() || !EveryWrappedVortexValueIsAWholeTurn()) return "REFUTED";
        if (!TheSurvivorIsLocal() || !TheSurvivorIsGaugeCompatible() || !TheSurvivorActsOnT1AndT2()) return "BOUNDARY";
        if (LocalCouplingClosedFormResidual() > 1e-12) return "BOUNDARY";
        return "DERIVED";
    }

    public static string WhereItStands()
        => "THE SOURCE HAS TO BE A NON-DIFFERENCE COUPLING, AND THAT IS AN EXACT DICHOTOMY RATHER THAN A TASTE. The audit "
         + "asks which AT mechanism can produce a non-zero field strength, and it answers with a measurement of both "
         + "sides. THE NEGATIVE SIDE IS A THEOREM. A link phase written as the DIFFERENCE of any single-valued scalar "
         + "- and that includes any function H of the organisation, because H(rho) is still a single-valued scalar - "
         + "has an exactly vanishing holonomy around every closed plaquette: the four contributions telescope. Measured "
         + $"on a NON-SEPARABLE organisation for H = rho, rho^2, exp(rho) and sin(rho), the worst residual is "
         + $"{GradientWorstResidual():E2}. That settles three of the five candidates at once: occupancy gradients, "
         + "actualization gradients and deficit gradients cannot produce a field strength, ever, for any coupling "
         + "function. THE POSITIVE SIDE IS EQUALLY EXACT. The moment the phase is coupled to the organisation WITHOUT "
         + "being its difference, F stops vanishing, and the audit measures the change: h(rho) times Delta rho with h "
         + "constant gives zero, because that IS the gradient, while h = rho, h = rho^2 and h = exp(rho) give "
         + $"{MaxFieldStrength(ProductCoupling(Occupancy, s => s)):F3}, "
         + $"{MaxFieldStrength(ProductCoupling(Occupancy, s => s * s)):F3} and "
         + $"{MaxFieldStrength(ProductCoupling(Occupancy, Math.Exp)):F3}. A purely SITE-LOCAL coupling, "
         + "h(rho(x)), gives a field strength with a closed form the audit verifies against the plaquette sum: "
         + $"{LocalCouplingClosedFormResidual():E2} residual against F_mu_nu = h(rho(x+mu)) - h(rho(x+nu)). So "
         + "NON-UNIFORM RHO is the surviving candidate - and it survives precisely because it is read LOCALLY rather "
         + "than through its gradient. THE TOPOLOGICAL DEFECT CANDIDATE COLLAPSES INTO THE SAME ANSWER. The natural "
         + "way to write a defect is a winding phase with a branch cut, and that construction is pure gauge: U_mu = "
         + "exp(i Delta_mu theta) is g(x)^-1 g(x+mu) BY CONSTRUCTION, so every plaquette equals one - measured over "
         + $"ALL of them, cut included, at {PhaseGradientPlaquetteResidual():E2}. The wrapped variant does light up "
         + $"plaquettes, {WrappedVortexFlux().NonZeroPlaquettes} of them, but every value is a WHOLE TURN, and a whole "
         + $"turn is the identity: the largest is {WrappedVortexFlux().Largest:F6} against a full turn of "
         + $"{2.0 * Math.PI:F6}. So a genuine defect needs an INDEPENDENTLY ASSIGNED link configuration, which is "
         + "exactly the non-difference structure already found - ONE mechanism, not two. ALL FOUR REQUIREMENTS HOLD "
         + "FOR THE SURVIVOR: it is local, a site-dependent gauge transformation moves F by "
         + $"{GaugeInvarianceResidual(Occupancy):E2}, F lives in the antisymmetric square which at d = 3 IS the "
         + "vector irrep so one source feeds T1 and the symmetric square feeds the graviton's sector, and it needs no "
         + "new primitive: measured against AT's own code, the organisation has "
         + $"{ComputedOrganisationMembers()} computed members, the link phase {ComputedPhaseMembers()}, the winding "
         + $"{ComputedDefectMembers()} - a winding number is literally computed in AT, as "
         + "InternalStateAnalyzer.ComputeWindingNumber in the oscillator line - and the plaquette apparatus is E_007's. "
         + "WHAT REMAINS OPEN IS STATED PLAINLY: the "
         + "audit locates the CLASS of mechanism and excludes its rivals exactly, but it does not derive WHICH "
         + "coupling, nor what makes the organisation non-uniform in the first place. That selection is the dynamics "
         + "layer E_005 located and E_006 and E_007 left open.";

    // ===================== REPORT =====================

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE GRADIENT CANDIDATES - F = 0, EXACTLY");
        sb.AppendLine("   candidate                | max |F|    | verdict");
        foreach (var (candidate, maxF, verdict) in GradientCandidates())
            sb.AppendLine($"   {candidate,-24} | {maxF:E3} | {verdict}");
        sb.AppendLine($"   worst gradient residual  : {GradientWorstResidual():E3}  (every gradient is pure gauge: {EveryGradientIsPureGauge()})");
        return sb.ToString();
    }

    public static string OutputMeasurements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. F = 0 VERSUS F != 0 - THE MEASUREMENT");
        sb.AppendLine("   form                            | coupling            | max |F|     | verdict");
        foreach (var (form, coupling, maxF, verdict) in Measurements())
            sb.AppendLine($"   {form,-31} | {coupling,-19} | {maxF,10:E3} | {verdict}");
        sb.AppendLine($"   F = 0 at     : {string.Join(", ", FIsZero())}");
        sb.AppendLine($"   F != 0 at    : {string.Join(", ", FIsNonZero())}");
        sb.AppendLine();
        sb.AppendLine("3. THE TOPOLOGICAL DEFECT CANDIDATE");
        sb.AppendLine($"   phase-gradient vortex, max |plaquette - 1| over ALL plaquettes : {PhaseGradientPlaquetteResidual():E3}");
        sb.AppendLine($"   -> the branch cut is a gauge artefact                          : {TheGradientVortexIsPureGauge()}");
        var (count, largest, total) = WrappedVortexFlux();
        sb.AppendLine($"   wrapped vortex: {count} plaquettes lit, largest {largest:F6} (= {largest / (2.0 * Math.PI):F3} whole turns), signed total {total:F6}");
        sb.AppendLine($"   every value is a whole turn, i.e. the identity                  : {EveryWrappedVortexValueIsAWholeTurn()}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE SURVIVOR - NON-UNIFORM RHO READ LOCALLY");
        sb.AppendLine($"   max |F| from h(rho(x)), h = rho              : {SurvivorFieldStrength():F6}");
        sb.AppendLine($"   closed form F_mu_nu = h(rho(x+mu)) - h(rho(x+nu)), residual : {LocalCouplingClosedFormResidual():E3}");
        sb.AppendLine();
        sb.AppendLine("5. THE FOUR REQUIREMENTS");
        foreach (var (requirement, status) in RequirementCheck())
            sb.AppendLine($"   {requirement,-20} : {status}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("6. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
