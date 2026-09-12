namespace AT.Core.ResearchXH;

/// <summary>The two candidate physical clock laws, plus the spatial route that preserves the clock.</summary>
public enum ClockCandidate
{
    /// <summary>dτ/dt = ρ^(1/d) — the ψ = 0 conformal slice.</summary>
    RhoOnly,

    /// <summary>dτ/dt = ρ^(1/d)·e^ψ — the QG207 completion, evaluated at the ψ that gives γ = +1.</summary>
    Qg207Psi,

    /// <summary>A = σ held fixed (so the clock is untouched) with B set to make γ = +1.</summary>
    SpatialRoute,
}

/// <summary>One candidate's derived clock-sector quantities at a given compactness (ResearchY-G_028).</summary>
public sealed record ClockSectorCase(
    ClockCandidate Candidate,
    string ClockLaw,
    double X,
    double A,
    double B,
    double Z,
    double GammaExact,
    double ClockOverInfinity,
    bool RedshiftPositive);

/// <summary>
/// ResearchY-G_028 — CLOCK SECTOR CLOSURE AUDIT.
///
/// QUESTION. Is the physical clock law <c>dτ/dt = ρ^(1/d)</c> or <c>dτ/dt = ρ^(1/d)·e^ψ</c>?
///
/// THE ONE FACT THAT DECIDES IT. The Newtonian potential IS the time exponent:
///
///     Φ/c² = A ,  where g₀₀ = −e^(2A) ,  A = σ + ψ ,  σ = (1/d)·ln ρ
///
/// so the acceleration is
///
///     a = −∇Φ = −(1/d)∇ln ρ − ∇ψ
///
/// The stated source law is therefore preserved **iff ∇ψ = 0** — a constant ψ, which is nothing but a
/// global choice of time unit. Any ψ that varies in space changes the source law.
///
/// WHY THAT MATTERS FOR THE OPTICS FIX. In the trace-preserving QG207 direction the two exponents move
/// together, A − σ = ψ and B − σ = −ψ/(d−1). γ = +1 requires A + B = 0, which within this family forces
/// ψ = −4σ **uniquely** (at d = 3). Then A = σ + ψ = −3σ = +3x &gt; 0 — the potential becomes POSITIVE, the
/// surface clock runs FAST, and every gravitating body is blueshifted. The clock is not merely shifted: its
/// SIGN is inverted.
///
/// WHAT G_028 CORRECTS. G_024 concluded that the QG207 completion creates "NO solar-system conflict",
/// comparing the shift e^(4x) − 1 = 2.78e−9 against a |ψ| ≲ 2e−3 bound and calling it 7.2e5× loose. That
/// comparison treated the shift as an additive perturbation on a clock of 1. But the observable is the
/// redshift **relative to infinity**, and the conformal law already predicts z(x) = e^x − 1 ≈ +x. A shift of
/// 2.78e−9 is not small next to x = 6.957e−10 — it is 4x, and it changes the sign.
/// </summary>
public static class ClockSectorClosure
{
    /// <summary>Compactness x = GM/(Rc²). Sign convention: ρ = e^(−dx), so σ = −x.</summary>
    public static readonly (string Body, double X)[] Bodies =
    {
        ("Earth", 6.957e-10),
        ("Sun", 2.1225e-6),
        ("x = 1e-4", 1.0e-4),
        ("J0740+6620", 0.247002),
    };

    /// <summary>GPS gravitational-redshift precision as a fraction of z_AT (G_009: +38.5 vs +38.6 μs/day).</summary>
    public const double GpsRelativePrecision = 2.0e-3;

    /// <summary>σ = (1/d)·ln ρ. With ρ = e^(−dx) this is simply −x.</summary>
    public static double Sigma(double x) => -x;

    /// <summary>The ψ that makes A + B = 0 inside the trace-preserving QG207 family, at d = 3: −4σ.</summary>
    public static double PsiForGammaPlusOne(double x) => -4.0 * Sigma(x);

    /// <summary>A = σ + ψ.</summary>
    public static double AOf(double x, double psi) => Sigma(x) + psi;

    /// <summary>B = σ − ψ/(d−1) at d = 3.</summary>
    public static double BOf(double x, double psi) => Sigma(x) - psi / 2.0;

    /// <summary>B for the spatial route: A = σ held fixed and A + B = 0 ⟹ B = −σ.</summary>
    public static double BSpatialRouteExact(double x) => 0.5 * Math.Log(2.0 - Math.Exp(2.0 * Sigma(x)));

    /// <summary>dτ/dt at the surface, relative to a clock at infinity.</summary>
    public static double ClockRatio(double a) => Math.Exp(a);

    /// <summary>Gravitational redshift 1 + z = e^(−A).</summary>
    public static double ZOf(double a) => Math.Exp(-a) - 1.0;

    /// <summary>Exact PPN γ = −(e^(2B) − 1)/(e^(2A) − 1) in the isotropic form.</summary>
    public static double GammaExact(double a, double b)
        => -((Math.Exp(2.0 * b) - 1.0) / (Math.Exp(2.0 * a) - 1.0));

    /// <summary>The derived case for a candidate at a given compactness.</summary>
    public static ClockSectorCase CaseFor(ClockCandidate candidate, double x)
    {
        double psi, a, b;
        string law;
        switch (candidate)
        {
            case ClockCandidate.RhoOnly:
                psi = 0.0; a = AOf(x, psi); b = BOf(x, psi);
                law = "rho^(1/d)";
                break;
            case ClockCandidate.Qg207Psi:
                psi = PsiForGammaPlusOne(x); a = AOf(x, psi); b = BOf(x, psi);
                law = "rho^(1/d)*exp(psi),  psi = -4 sigma";
                break;
            default:
                a = Sigma(x); b = BSpatialRouteExact(x);
                law = "rho^(1/d) (A held at sigma) with B = 0.5*ln(2 - e^(2 sigma))";
                break;
        }
        double z = ZOf(a);
        return new ClockSectorCase(candidate, law, x, a, b, z, GammaExact(a, b), ClockRatio(a), z > 0);
    }

    /// <summary>Every candidate at every body.</summary>
    public static ClockSectorCase[] Table()
        => (from c in Enum.GetValues<ClockCandidate>()
            from b in Bodies
            select CaseFor(c, b.X)).ToArray();

    // ── The source law ─────────────────────────────────────────────────────────

    /// <summary>
    /// Is the source law a = −(1/d)∇ln ρ preserved? It holds exactly when ψ is spatially CONSTANT, because
    /// Φ = σ + ψ makes a = −(1/d)∇ln ρ − ∇ψ. A constant ψ is a global time-unit choice, not physics.
    /// </summary>
    public static bool SourceLawPreserved(double psi) => Math.Abs(psi) == 0.0;

    /// <summary>The fractional size of the ψ contribution to the acceleration, relative to the ρ contribution,
    /// at the ψ that gives γ = +1: |∇ψ| / |(1/d)∇ln ρ| = 4.</summary>
    public static double PsiAccelerationContribution(double x) => Math.Abs(PsiForGammaPlusOne(x) / Sigma(x));

    // ── The two decisive separations ───────────────────────────────────────────

    /// <summary>The Earth separation of a candidate from the observed gravitational redshift, in σ of GPS.</summary>
    public static double EarthSeparationSigma(ClockCandidate candidate)
    {
        double x = Bodies[0].X;
        double predicted = CaseFor(candidate, x).Z;
        double observed = ZOf(Sigma(x));                 // the calibrated law: z = e^x − 1
        double precision = GpsRelativePrecision * Math.Abs(observed);
        return Math.Abs(predicted - observed) / precision;
    }

    /// <summary>
    /// The bound on |ψ| implied by the Earth redshift measurement. G_024 quoted 2e−3; the correct bound is
    /// the GPS precision itself, because a nonzero ψ shifts the redshift ABSOLUTELY, not fractionally.
    /// </summary>
    public static double PsiBoundFromGps()
    {
        double x = Bodies[0].X;
        return GpsRelativePrecision * Math.Abs(ZOf(Sigma(x)));
    }

    /// <summary>How much tighter the corrected GPS bound is than the 2e−3 quoted by G_024.</summary>
    public static double GpsBoundCorrectionFactor() => 2.0e-3 / PsiBoundFromGps();

    /// <summary>The volume cost of the spatial route: √(det g_ij)/ρ at a compactness. With ρ = e^(−dx) this is
    /// e^(3B)·e^(3x) at d = 3, which is G_023's 3.437585 at J0740+6620.</summary>
    public static double SpatialRouteVolumeCost(double x)
        => Math.Exp(3.0 * BSpatialRouteExact(x)) / Math.Exp(-3.0 * x);
}
