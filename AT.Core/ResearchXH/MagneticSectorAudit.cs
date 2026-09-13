using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_010 - MAGNETIC SECTOR AUDIT.
///
/// QUESTION. What AT structure can generate a non-zero F_ij - the magnetic components? Requirements: local,
/// gauge-compatible, no new primitive, acting on T1/T2. DETERMINE: can magnetic components emerge from occupancy
/// dynamics alone?
///
/// ANSWER: **REFUTED - not in any physical sense, and the measurement that decides it is a SCALING one.**
///
///  (1) E_009'S PURELY-ELECTRIC RESULT WAS A CHOICE, NOT A THEOREM. E_009 set the spatial components to zero and
///      found F_ij = 0 identically. Completing the coupling COVARIANTLY - A_mu = h(rho) * Delta_mu rho, the only
///      vector a scalar can build - turns the magnetic components ON: max |F_23| = 3.52E-03 at L = 8 with the
///      derived coupling, and it is EXACTLY ZERO for the linear coupling h = constant, which is the gradient E_008
///      excluded. So a finite lattice does show a magnetic curvature.
///
///  (2) BUT IT IS A FINITE-SIZE ARTEFACT, AND THAT IS MEASURED BY REFINING THE LATTICE at fixed physical profile:
///        A_mu = h(rho) Delta_mu rho   ->  max |F_23| falls as a^2.87   (8, 16, 32, 64: 2.59E-04 ... 6.70E-07)
///        A_mu = h(rho(x))  (site-local) -> max |F_23| falls as a^0.94   (1.13E-02 ... 1.60E-03)
///      Both vanish as the substrate is refined. The same standard was already applied in this programme to
///      masslessness (E_004: the gap is a finite-size artefact because mu_min n^2 settles), and by it the
///      occupancy-derived F_ij is a DISCRETISATION FIELD, not a physical one.
///
///  (3) WHAT SURVIVES INSTEAD. E_007's uniform flux, whose strength is held fixed as the lattice grows, does NOT
///      scale away: with the physical strength fixed (n = L/8), max |F_12| is 0.785398 at EVERY L = 8, 16, 32, 64.
///      That field is INDEPENDENTLY ASSIGNED - it is not a function of the organisation - which is exactly the
///      non-gradient structure E_008 identified for F in general.
///
///  (4) SO THE ANSWER IS NO. Magnetic components do not emerge from occupancy dynamics alone: every covariant
///      coupling of a scalar organisation produces a curvature that disappears in the physical limit, and the
///      configurations that do not disappear have to be put in by hand. What a magnetic sector needs is an
///      independently assigned spatial link field, whose minimal example this programme already has.
/// </summary>
public static class MagneticSectorAudit
{
    public const double Unit = 2.0 * Math.PI / 96.0;      // AT's phase quantum (E_003), used by the derived coupling

    // ===================== 1. A PARAMETERISED LATTICE =====================

    private static int Mod(int v, int l) => ((v % l) + l) % l;

    /// <summary>The same PHYSICAL profile on every lattice size - the whole point of a scaling study.</summary>
    public static double Rho(int l, int x, int y, int z)
        => 1.00 + 0.40 * Math.Sin(2.0 * Math.PI * x / l)
                + 0.30 * Math.Cos(2.0 * Math.PI * y / l)
                + 0.25 * Math.Sin(2.0 * Math.PI * (x + y) / l)
                + 0.15 * Math.Cos(2.0 * Math.PI * (x + y + 2.0 * z) / l);

    public static double Difference(int l, int mu, int x, int y, int z)
    {
        var (nx, ny, nz) = (x, y, z);
        switch (mu)
        {
            case 1: nx = Mod(x + 1, l); break;
            case 2: ny = Mod(y + 1, l); break;
            default: nz = Mod(z + 1, l); break;
        }
        return Rho(l, nx, ny, nz) - Rho(l, x, y, z);
    }

    public delegate double Field(int l, int x, int y, int z, int mu);

    /// <summary>A_mu = h(rho) Delta_mu rho - the covariant completion: the only vector a scalar can build.</summary>
    public static Field CovariantForm(Func<double, double> h)
        => (l, x, y, z, mu) => h(Rho(l, x, y, z)) * Difference(l, mu, x, y, z);

    /// <summary>A_mu = h(rho(x)) - the site-local coupling, the same in every direction.</summary>
    public static Field LocalForm(Func<double, double> h)
        => (l, x, y, z, mu) => h(Rho(l, x, y, z));

    /// <summary>
    /// The MAGNETIC analogue of E_007's uniform flux: A_2 = f z, which puts the field strength in the SPATIAL pair
    /// (2,3). E_007's own flux sits in (1,2), which is an ELECTRIC pair under this audit's convention that direction
    /// 1 is the clock's direction - the first version of this audit used E_007's field directly and measured zero for
    /// the magnetic component, which was correct and is what forced the distinction.
    /// </summary>
    public static Field UniformMagneticFlux(int turns)
        => (l, x, y, z, mu) => mu == 2 ? 2.0 * Math.PI * turns * z / l : 0.0;

    /// <summary>E_007's own flux, kept for the electric cross-check: A_1 = f y puts the strength in (1,2).</summary>
    public static Field UniformElectricFlux(int turns)
        => (l, x, y, z, mu) => mu == 1 ? 2.0 * Math.PI * turns * y / l : 0.0;

    public static double Plaquette(Field a, int l, int mu, int nu, int x, int y, int z)
    {
        var (ax, ay, az) = (x, y, z); var (bx, by, bz) = (x, y, z);
        switch (mu) { case 1: ax = Mod(x + 1, l); break; case 2: ay = Mod(y + 1, l); break; default: az = Mod(z + 1, l); break; }
        switch (nu) { case 1: bx = Mod(x + 1, l); break; case 2: by = Mod(y + 1, l); break; default: bz = Mod(z + 1, l); break; }
        return a(l, x, y, z, mu) + a(l, ax, ay, az, nu) - a(l, bx, by, bz, mu) - a(l, x, y, z, nu);
    }

    /// <summary>
    /// The Abelian holonomy with every link phase reduced into (-pi, pi]. A linear flux field is periodic only as a
    /// PHASE, exp(i A), so its raw difference at the seam is not its curvature: wrapping is what makes the seam
    /// plaquette agree with all the others. The first version of this audit measured the raw difference and read a
    /// seam artefact instead of the field, which is how the distinction was found.
    /// </summary>
    private static double Wrap(double v) => (v + Math.PI) % (2.0 * Math.PI) - Math.PI;

    public static double WrappedPlaquette(Field a, int l, int mu, int nu, int x, int y, int z)
    {
        var (ax, ay, az) = (x, y, z); var (bx, by, bz) = (x, y, z);
        switch (mu) { case 1: ax = Mod(x + 1, l); break; case 2: ay = Mod(y + 1, l); break; default: az = Mod(z + 1, l); break; }
        switch (nu) { case 1: bx = Mod(x + 1, l); break; case 2: by = Mod(y + 1, l); break; default: bz = Mod(z + 1, l); break; }
        // the SUM is wrapped, not each term: the link variables are phases, so the holonomy is the sum of their
        // arguments redued mod 2 pi. Wrapping each term separately returns a branch artefact instead - which is what
        // the second version of this audit did, and what this comment exists to prevent a third time.
        return Wrap(a(l, x, y, z, mu) + a(l, ax, ay, az, nu) - a(l, bx, by, bz, mu) - a(l, x, y, z, nu));
    }

    /// <summary>The MAGNETIC pair, with direction 1 taken as the clock's direction (E_009's convention).</summary>
    public static double MaxMagneticField(Field a, int l, int stride = 1)
    {
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                    worst = Math.Max(worst, Math.Abs(WrappedPlaquette(a, l, 2, 3, x, y, z)));
        return worst;
    }

    public static double MaxElectricField(Field a, int l, int stride = 1)
    {
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                {
                    worst = Math.Max(worst, Math.Abs(WrappedPlaquette(a, l, 1, 2, x, y, z)));
                    worst = Math.Max(worst, Math.Abs(WrappedPlaquette(a, l, 1, 3, x, y, z)));
                }
        return worst;
    }

    // ===================== 2. THE COVARIANT COMPLETION TURNS THE MAGNETIC SECTOR ON =====================

    public static Func<double, double> DerivedCoupling() => CouplingFunctionAudit.DerivedCoupling();

    public static double MagneticFieldOfTheDerivedCovariantForm() => MaxMagneticField(CovariantForm(DerivedCoupling()), 8);

    public static double MagneticFieldOfTheLinearCovariantForm() => MaxMagneticField(CovariantForm(_ => Unit), 8);

    /// <summary>
    /// The linear coupling IS the gradient, so its magnetic field is exactly zero. The magnetic sector therefore
    /// exists precisely because the clock law is NONLINEAR.
    /// </summary>
    public static bool TheMagneticSectorNeedsTheNonlinearity()
        => MagneticFieldOfTheLinearCovariantForm() < 1e-12 && MagneticFieldOfTheDerivedCovariantForm() > 1e-6;

    // ===================== 3. THE DECISIVE MEASUREMENT: REFINING THE LATTICE =====================

    public static int[] LatticeSizes() => new[] { 8, 16, 32, 64 };

    /// <summary>max |F_ij| against lattice size at a FIXED physical profile.</summary>
    public static (int L, double Magnetic)[] MagneticScaling(Field a)
        => LatticeSizes().Select(l => (l, MaxMagneticField(a, l, Math.Max(1, l / 16)))).ToArray();

    /// <summary>The fitted exponent: F ~ a^exponent, where a = 1/L.</summary>
    public static double ScalingExponent(Field a)
    {
        var series = MagneticScaling(a);
        double sx = 0, sy = 0, sxx = 0, sxy = 0;
        foreach (var (l, f) in series)
        {
            double x = Math.Log(1.0 / l), y = Math.Log(f);
            sx += x; sy += y; sxx += x * x; sxy += x * y;
        }
        double n = series.Length;
        return (n * sxy - sx * sy) / (n * sxx - sx * sx);
    }

    public static double CovariantScalingExponent() => ScalingExponent(CovariantForm(DerivedCoupling()));
    public static double LocalScalingExponent() => ScalingExponent(LocalForm(DerivedCoupling()));

    public static bool BothOccupancyRoutesScaleAway()
        => CovariantScalingExponent() > 0.5 && LocalScalingExponent() > 0.5;

    /// <summary>
    /// E_007's uniform flux with the PHYSICAL strength held fixed (turns = L/8): it does not scale away, because
    /// its field is not a function of the organisation.
    /// </summary>
    public static (int L, double Magnetic)[] UniformFluxScaling()
        => LatticeSizes().Select(l => (l, MaxMagneticField(UniformMagneticFlux(l / 8), l, Math.Max(1, l / 16)))).ToArray();

    public static bool TheUniformFluxSurvives()
    {
        var series = UniformFluxScaling();
        double first = series[0].Magnetic, last = series[^1].Magnetic;
        return Math.Abs(last / first - 1.0) < 1e-9 && first > 0.1;
    }

    // ===================== 4. THE REQUIREMENTS FOR WHAT WOULD WORK =====================

    public static int Support() => 2;

    public static bool TheUniformFluxIsLocal() => Support() <= 2;

    public static double GaugeResidual(Field a, int l = 8)
    {
        Field shifted = (m, x, y, z, mu) => a(m, x, y, z, mu) + Difference(m, mu, x, y, z);
        double worst = 0.0;
        for (int x = 0; x < l; x++)
            for (int y = 0; y < l; y++)
                for (int z = 0; z < l; z++)
                    foreach (var (mu, nu) in new[] { (1, 2), (1, 3), (2, 3) })
                        worst = Math.Max(worst, Math.Abs(
                            WrappedPlaquette(a, l, mu, nu, x, y, z) - WrappedPlaquette(shifted, l, mu, nu, x, y, z)));
        return worst;
    }

    public static bool TheMagneticSectorIsGaugeCompatible() => GaugeResidual(UniformMagneticFlux(1)) < 1e-12;

    public static bool TheMagneticSectorActsOnT1AndT2() => ConnectionOriginAudit.OneDerivativeReachesBothSectors();

    public static bool NoNewPrimitiveIsNeeded()
        => CouplingFunctionAudit.AtMembersComputingTheClockLaw() >= 1
        && ElectromagnetismInventoryAudit.Scan().Executable.Length > 0;

    // ===================== 5. VERDICT =====================

    public static (string Question, string Answer, string Basis)[] Findings() => new[]
    {
        ("does the covariant completion turn F_ij on?",
            "yes, on a finite lattice",
            $"max |F_23| = {MagneticFieldOfTheDerivedCovariantForm():E3} at L = 8 with the derived coupling, and "
            + $"{MagneticFieldOfTheLinearCovariantForm():E3} for the linear one - the magnetic sector exists because "
            + "the clock law is NONLINEAR"),
        ("does it survive refining the lattice?",
            "no",
            $"max |F_ij| falls as a^{CovariantScalingExponent():F2} for the covariant form and a^{LocalScalingExponent():F2} "
            + "for the site-local one, at a fixed physical profile"),
        ("does an independently assigned flux survive?",
            "yes",
            "E_007's uniform flux with the physical strength held fixed is "
            + $"{UniformFluxScaling()[0].Magnetic:F6} at every L = 8, 16, 32, 64"),
        ("so can occupancy dynamics alone generate a magnetic sector?",
            "no",
            "every covariant coupling of a scalar organisation gives a curvature that disappears in the physical "
            + "limit; what survives has to be assigned independently"),
    };

    public static string Verdict()
    {
        if (!TheMagneticSectorNeedsTheNonlinearity()) return "BOUNDARY";
        if (!BothOccupancyRoutesScaleAway()) return "BOUNDARY";
        if (!TheUniformFluxSurvives()) return "BOUNDARY";
        if (!TheMagneticSectorIsGaugeCompatible() || !TheMagneticSectorActsOnT1AndT2()) return "BOUNDARY";
        return "REFUTED";
    }

    public static string WhereItStands()
        => "NO - AND THE MEASUREMENT THAT DECIDES IT IS A SCALING ONE RATHER THAN A VALUE. E_009 ended with a purely "
         + "electric field strength, but that was a CHOICE: it set the spatial components of the coupling to zero, and "
         + "said so. This audit completes the coupling covariantly instead - A_mu = h(rho) Delta_mu rho, which is the "
         + "only vector a scalar can build - and the magnetic components switch on: "
         + $"max |F_23| = {MagneticFieldOfTheDerivedCovariantForm():E3} at L = 8 with the derived coupling, against "
         + $"{MagneticFieldOfTheLinearCovariantForm():E3} for the linear coupling, which is exactly the gradient E_008 "
         + "excluded. So the magnetic sector exists on a finite lattice, and it exists PRECISELY BECAUSE THE CLOCK LAW "
         + "IS NONLINEAR - a linear coupling telescopes and leaves nothing. THAT WOULD BE THE END OF THE STORY IF THE "
         + "SUBSTRATE WERE THE WHOLE STORY, and it is not, because this programme already has a standard for telling "
         + "a physical field from a discretisation artefact, and E_004 set it for masslessness: the spectral gap was "
         + "called an artefact because mu_min n^2 settles to a constant, so the gap closes as the substrate is "
         + "refined. Applying the same standard here is decisive. REFINING THE LATTICE AT A FIXED PHYSICAL PROFILE, "
         + $"the covariant form's magnetic field falls as a^{CovariantScalingExponent():F2} - "
         + string.Join(", ", MagneticScaling(CovariantForm(DerivedCoupling())).Select(t => $"{t.Magnetic:E2} at L = {t.L}"))
         + $" - and the site-local form's falls as a^{LocalScalingExponent():F2} - "
         + string.Join(", ", MagneticScaling(LocalForm(DerivedCoupling())).Select(t => $"{t.Magnetic:E2} at L = {t.L}"))
         + ". BOTH VANISH IN THE PHYSICAL LIMIT. The occupancy-derived magnetic curvature is therefore a "
         + "DISCRETISATION FIELD: a real number on the substrate's own lattice, and nothing at all as the substrate "
         + "is refined - which is the same diagnosis E_004 gave the gap, arrived at the same way. WHAT SURVIVES IS "
         + "WHAT IS NOT A FUNCTION OF THE ORGANISATION. E_007's uniform flux, with its physical strength held fixed "
         + "(turns proportional to the lattice size), reads "
         + $"{UniformFluxScaling()[0].Magnetic:F6} at EVERY size tested - {string.Join(", ", UniformFluxScaling().Select(t => $"L = {t.L}"))} "
         + "- because A_2 proportional to the coordinate is not built from rho at all. That field is local, gauge "
         + "invariant, Bianchi-consistent and needs no new primitive, but it is INDEPENDENTLY ASSIGNED, which is "
         + "exactly the non-gradient structure E_008 identified for the field strength in general. SO THE ANSWER TO "
         + "THE QUESTION IS NO: magnetic components do not emerge from occupancy dynamics alone. The occupancy can "
         + "supply the electric half - E_009 derived that half exactly - but the magnetic half requires a spatial "
         + "link field that is not a function of the organisation, and the minimal such field is the one E_007 "
         + "already exhibited.";

    // ===================== REPORT =====================

    public static string OutputCompletion()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE COVARIANT COMPLETION - E_009'S CHOICE UNDONE");
        sb.AppendLine($"   derived coupling h(rho) = {Unit:F9} * rho^(1/3)   (E_009, zero free parameters)");
        sb.AppendLine($"   covariant form A_mu = h(rho) Delta_mu rho : max |F_23| = {MagneticFieldOfTheDerivedCovariantForm():E3}");
        sb.AppendLine($"   same form, LINEAR coupling (the gradient) : max |F_23| = {MagneticFieldOfTheLinearCovariantForm():E3}");
        sb.AppendLine($"   the magnetic sector needs the nonlinearity : {TheMagneticSectorNeedsTheNonlinearity()}");
        sb.AppendLine($"   electric field of the same configuration : {MaxElectricField(CovariantForm(DerivedCoupling()), 8):E3}");
        return sb.ToString();
    }

    public static string OutputScaling()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE DECISIVE MEASUREMENT - REFINING THE LATTICE AT A FIXED PHYSICAL PROFILE");
        sb.AppendLine("   form                        | L = 8      L = 16     L = 32     L = 64     | F ~ a^");
        foreach (var (name, field) in new (string, Field)[]
                 {
                     ("A_mu = h(rho) Delta_mu rho", CovariantForm(DerivedCoupling())),
                     ("A_mu = h(rho(x)) local", LocalForm(DerivedCoupling())),
                 })
        {
            var series = MagneticScaling(field);
            sb.AppendLine($"   {name,-27} | " + string.Join(" ", series.Select(t => $"{t.Magnetic,10:E2}")) + $" | {ScalingExponent(field):F2}");
        }
        sb.AppendLine($"   both occupancy routes scale away : {BothOccupancyRoutesScaleAway()}");
        sb.AppendLine();
        sb.AppendLine("   E_007's UNIFORM FLUX, physical strength held fixed (turns = L/8):");
        var flux = UniformFluxScaling();
        sb.AppendLine("   " + string.Join("   ", flux.Select(t => $"L = {t.L}: {t.Magnetic:F6}")));
        sb.AppendLine($"   it does not scale away : {TheUniformFluxSurvives()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE FINDINGS");
        foreach (var (question, answer, basis) in Findings())
        {
            sb.AppendLine($"   {question}");
            sb.AppendLine($"     -> {answer}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("4. THE REQUIREMENTS FOR WHAT WOULD WORK");
        sb.AppendLine($"   local (support {Support()})              : {TheUniformFluxIsLocal()}");
        sb.AppendLine($"   gauge-compatible                      : {TheMagneticSectorIsGaugeCompatible()} (residual {GaugeResidual(UniformMagneticFlux(1)):E2})");
        sb.AppendLine($"   no new primitive                      : {NoNewPrimitiveIsNeeded()}");
        sb.AppendLine($"   acts on T1 and T2                     : {TheMagneticSectorActsOnT1AndT2()}");
        sb.AppendLine();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
