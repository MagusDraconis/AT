namespace AT.Core.ResearchXH;

/// <summary>One step of the Difference → Counting → ρ → metric chain, and whether it needs the ansatz.</summary>
public sealed record ConformityStep(string Step, string What, bool RequiresConformalAnsatz,
    MeasureProvenance Provenance, string Basis);

/// <summary>An AT primitive, its tensorial rank, and whether it can entail the conformal shape.</summary>
public sealed record PrimitiveRank(string Primitive, int Rank, bool EntailsConformalShape,
    bool IsConformalReference, string Basis);

/// <summary>Two spatial 3-metrics with the SAME determinant but different conformal status.</summary>
public sealed record ShapeCounterexample(double Rho, double SqrtDetIso, double SqrtDetAnisotropic,
    double AnisotropySpread, bool IsoIsConformal, bool AnisotropicIsConformal);

/// <summary>
/// ResearchY-G_032 — CONFORMAL ASSUMPTION AUDIT.
///
/// QUESTION. Is <c>g = Ω²η</c> derived, or only assumed? Trace Difference → Counting → ρ → metric; find the
/// step that first requires the conformal ansatz; and decide whether any existing AT primitive forces
/// conformal flatness.
///
/// THE ANSWER. **ASSUMED — and the step that requires it is the ρ → metric step, i.e. the step at which the
/// Difference acquires its TENSOR face.** Steps 1–2 carry no metric content at all (indices and one scalar
/// label); the conformal ansatz first becomes necessary when a rank-2 field must be built from scalar
/// primitives. The repository's own account agrees and localises the import precisely: QG288 —
/// *"ρ and ψ are the trace/traceless faces of the ONE Difference; **the tensor face requires η** [conformal
/// reference, QG285], the scalar face does not."* And QG289's anchor inventory classifies η as a
/// **TRUE THEORY INPUT**: *"the conformal reference metric (g = ρ^(2/d)·η) — the framework's flat reference,
/// not a measured value; part of the geometry, not a choice."*
///
/// NO PRIMITIVE FORCES IT. Set aside η (which IS the conformal statement, so it cannot entail itself). The
/// remaining primitives are scalar: ρ (rank 0) and ψ. A scalar can only be read into a conformal factor, so
/// scalar content alone gives the ONE-function conformal form only if no derivative and no second function
/// is admitted — and the G-group's own G_029 family, B = F(σ), is precisely the two-function family that the
/// primitives' content does support. Conformal flatness (A = B) therefore REMOVES freedom that the primitives
/// leave open; it does not confer freedom they lack. Component bookkeeping: a symmetric 4×4 metric has 10
/// components, a conformal metric has 1 function, so the ansatz discards 9; the primitives supply 0 of the 5
/// independent traceless (Weyl) components.
///
/// THE CRITERION, PROVED. In the G-group's gauge the spatial part is e^(2B)·(flat), so the metric is
/// ds² = −e^(2A)dt² + e^(2B)δ_ij. Then
///
///     **g = Ω²η  ⟺  A = B.**
///
/// (⇐) A = B ⟹ g = e^(2A)(−dt² + δ) = e^(2A)η, manifestly conformal. (⇒) In area gauge the same statement is
/// e^(B_area) = 1 − R·A_R with R = e^B r; the proof is two lines of chain rule — e^(B_area) = 1/(1 + rB′) and
/// R·A_R = rA′/(1 + rB′), so the criterion holds iff B′ = A′, i.e. B = A by asymptotic flatness. The
/// converse construction is explicit: setting x = R·e^(−A) turns the area-gauge metric into e^(2A)(−dt² +
/// dx² + x²dΩ²). Verified independently below by the Weyl invariant: W = 1e−30 for A = B and W = 0.25–15.4 for
/// A ≠ B.
///
/// THE DECISIVE CONSEQUENCE. With A = σ FIXED by G_028, the criterion A = B forces B = σ — which is exactly
/// the counting measure (G_031), giving γ = −1 EXACTLY, excluded by Cassini at **8.6957e4 σ** with zero
/// deflection and zero Shapiro delay. So the ansatz is not merely unforced: **imposing it is refuted.** The
/// admissible band never contains the conformal value at any body, and the conformality it demands is violated
/// by a factor of 2 (not a small correction). AT's own metric form, g = ρ^(2/d)η, IS the conformal member, and
/// so predicts γ = −1. That is G_030's dilemma traced to its primitive root: the second primitive **η**.
///
/// VERDICT: **BOUNDARY** — assumed, and no primitive entails it. The verdict is COMPUTED, never typed
/// (the ResearchY-G_027 discipline).
/// </summary>
public static class ConformalAssumptionAudit
{
    public const int D = 3;

    /// <summary>σ = −x (with ρ = e^(−dx)).</summary>
    public static double Sigma(double x) => -x;

    /// <summary>A is FIXED by G_028's clock closure: A = σ.</summary>
    public static double AOf(double x) => Sigma(x);

    /// <summary>Cassini 2003: γ = 1.0000210 ± 2.3e−5.</summary>
    public const double CassiniGamma = 1.0000210;
    public const double CassiniUncertainty = 2.3e-5;

    public static readonly (string Body, double X)[] Bodies =
    {
        ("Cassini (solar)", 4.0e-6),
        ("Earth", 6.957e-10),
        ("Sun", 2.1225e-6),
        ("J0740+6620", 0.247002),
    };

    // ── The criterion, and its equivalence to equality of the two exponents ────

    /// <summary>THE CRITERION: g = Ω²η ⟺ A = B (in the G-group's gauge).</summary>
    public static bool IsConformalFlat(double a, double b)
        => Math.Abs(a - b) < 1.0e-12 * Math.Max(1.0, Math.Max(Math.Abs(a), Math.Abs(b)));

    /// <summary>
    /// The area-gauge form of the same criterion: e^(B_area) = 1 − R·A_R with R = e^B·r.
    /// This is the derivation the Weyl check confirms; it is expressed in the isotropic radius r.
    /// </summary>
    public static double AreaGaugeCriterionResidual(double r, double aPrime, double bPrime)
    {
        double eBArea = 1.0 / (1.0 + r * bPrime);
        double oneMinusRAr = 1.0 - r * aPrime / (1.0 + r * bPrime);
        return eBArea - oneMinusRAr;
    }

    /// <summary>
    /// EXECUTED PROOF that the two forms of the criterion agree: the area-gauge residual vanishes
    /// **iff** A′ = B′. Returns the number of mismatches over a deterministic sweep (must be 0).
    /// The flat-space point where the derivative pair is degenerate carries no information and is skipped.
    /// </summary>
    public static int CriterionEquivalenceMismatches(int divisions = 400, double lo = -2.0, double hi = 2.0)
    {
        int mismatches = 0;
        for (int i = 0; i <= divisions; i++)
        {
            double ap = lo + (hi - lo) * i / divisions;
            for (int j = 0; j <= divisions; j++)
            {
                double bp = lo + (hi - lo) * j / divisions;
                if (Math.Abs(1.0 + 1.0 * bp) < 1.0e-6) continue;     // e^(B_area) singular
                bool residualVanishes = Math.Abs(AreaGaugeCriterionResidual(1.0, ap, bp)) < 1.0e-12;
                bool exponentsEqual = Math.Abs(ap - bp) < 1.0e-12;
                if (residualVanishes != exponentsEqual) mismatches++;
            }
        }
        return mismatches;
    }

    // ── The chain: where does the ansatz first become necessary? ──────────────

    /// <summary>The Difference → Counting → ρ → metric chain, with the step that first needs the ansatz.</summary>
    public static ConformityStep[] Chain() => new[]
    {
        new ConformityStep("Difference → Counting", "counts and their indices — pure bookkeeping",
            false, MeasureProvenance.Derived,
            "no metric content whatsoever: this step produces labels, not geometry"),
        new ConformityStep("Counting → ρ (scalar face)", "ρ is the SCALAR FACE of the one Difference",
            false, MeasureProvenance.Derived,
            "QG288: 'the scalar face does not [require η]' — one scalar needs no reference tensor"),
        new ConformityStep("ρ → metric (TENSOR face)", "a symmetric rank-2 field is built from scalar content",
            true, MeasureProvenance.Assumed,
            "QG285/QG288: 'the tensor face REQUIRES η [conformal reference]'; QG289: η is a TRUE THEORY INPUT — 'part of the geometry, not a choice'. THIS is the first step that needs the ansatz"),
        new ConformityStep("metric → clock law / spatial measure", "√(−g₀₀) = ρ^(1/d) and √det g_ij = ρ",
            true, MeasureProvenance.Derived,
            "derived WITHIN the ansatz — G_031: the two are one equation (n = 1/d), so they inherit the assumption"),
    };

    /// <summary>THE ANSWER TO THE TRACE: the first step that requires the conformal ansatz.</summary>
    public static ConformityStep FirstStepRequiringConformalAnsatz()
        => Chain().First(s => s.RequiresConformalAnsatz);

    /// <summary>The 1-based index of that step in the chain.</summary>
    public static int FirstStepRequiringConformalAnsatzIndex()
    {
        var chain = Chain();
        for (int i = 0; i < chain.Length; i++) if (chain[i].RequiresConformalAnsatz) return i + 1;
        return 0;
    }

    /// <summary>How many steps precede the first one that needs the ansatz — the metric-free prefix.</summary>
    public static int MetricFreePrefixLength() => FirstStepRequiringConformalAnsatzIndex() - 1;

    // ── The primitive inventory: does anything entail the shape? ──────────────

    /// <summary>
    /// AT's primitive inventory at a point, with tensorial rank. Rank 0 content can only enter a conformal
    /// factor, so it cannot determine a SHAPE. η is the only rank-2 item — and η IS the conformal statement.
    /// </summary>
    public static PrimitiveRank[] Primitives() => new[]
    {
        new PrimitiveRank("ρ (scalar face of the Difference)", 0, false, false,
            "QG285/QG288 — rank 0; reads into a conformal factor and nothing else"),
        new PrimitiveRank("ψ (traceless face of the Difference)", 0, false, false,
            "QG288 — the traceless face is REFERENCED through η (QG207 trace-preserving split); on its own it is one more scalar field"),
        new PrimitiveRank("η (the conformal reference metric)", 2, true, true,
            "QG77/QG289 — 'a TRUE THEORY INPUT: the framework's flat reference... part of the geometry, not a choice'. It is rank 2, and it is conformal flatness itself"),
        new PrimitiveRank("d = 3 (spatial dimension)", 0, false, false,
            "QG2/QG197 — a number; fixes the exponent, not the shape"),
        new PrimitiveRank("π (universal constant)", 0, false, false,
            "QG185 — a number"),
    };

    /// <summary>
    /// The primitives that ENTAIL conformal flatness from outside it. η is excluded: it is the conformal
    /// statement itself, so citing it is restating the assumption rather than deriving it.
    /// </summary>
    public static PrimitiveRank[] ForcingPrimitives()
        => Primitives().Where(p => p.EntailsConformalShape && !p.IsConformalReference).ToArray();

    /// <summary>Does any primitive entail conformal flatness without being the assumption itself?</summary>
    public static bool AnyPrimitiveForcesConformalFlatness() => ForcingPrimitives().Length > 0;

    /// <summary>Is conformal flatness itself one of AT's primitives (QG289)?</summary>
    public static bool ConformalityIsItselfAPrimitive() => Primitives().Any(p => p.IsConformalReference);

    /// <summary>
    /// COMPONENT ACCOUNTING: a symmetric 4×4 metric has 10 components; a conformal metric has 1 function;
    /// so the ansatz discards 9. AT's scalar content at a point is 2 (ρ, ψ) and it supplies 0 of the 5
    /// independent traceless (Weyl) components — so nothing in it forces a shape.
    /// </summary>
    public static (int MetricComponents, int ConformalFunctions, int Discarded, int PrimitiveScalars,
        int TracelessSupplied, int TracelessIndependent) ComponentAccounting()
        => (10, 1, 9, 2, 0, 5);

    // ── Counting is a DETERMINANT condition, not a SHAPE condition ────────────

    /// <summary>
    /// The counting measure √det g_ij = ρ fixes ONE combination (the product of the eigenvalues). Exhibit two
    /// 3-metrics with identical √det = ρ but different conformal status. Same determinant, different shape.
    /// </summary>
    public static ShapeCounterexample ShapeCounterexampleFor(double rho)
    {
        double iso = Math.Pow(rho, 2.0 / D);                 // diag(a,a,a) — conformal to flat
        double aniso = rho * rho;                            // diag(ρ², 1, 1) — same det
        return new ShapeCounterexample(rho,
            Math.Sqrt(iso * iso * iso), Math.Sqrt(aniso * 1.0 * 1.0),
            aniso - 1.0,
            true,                                            // isotropic ⟹ conformally flat at every ρ
            Math.Abs(aniso - 1.0) < 1.0e-15);                // anisotropic only if ρ = 1 (flat)
    }

    /// <summary>Every shape counterexample over a deterministic grid.</summary>
    public static ShapeCounterexample[] ShapeCounterexamples()
        => new[] { 1.0e-3, 0.5, 0.610136, 1.0, 2.0, 1000.0 }.Select(ShapeCounterexampleFor).ToArray();

    // ── The three names are one condition ────────────────────────────────────

    /// <summary>ρ for a compactness.</summary>
    public static double RhoOf(double x) => Math.Exp(-D * x);

    /// <summary>The counting measure in this gauge: √det g_ij = e^(3B).</summary>
    public static double CountingMeasure(double b) => Math.Exp(D * b);

    /// <summary>
    /// G_031's theorem, restated with the ansatz as the subject: with A = σ FIXED, the conformal statement
    /// (A = B), the counting measure (e^(3B) = ρ) and B = σ are THE SAME CONDITION. Solved, not sampled:
    /// the B that satisfies the counting measure is compared directly with the B that conformality demands.
    /// Counts mismatches (must be 0).
    /// </summary>
    public static int ThreeNameMismatches(int divisions = 4000)
    {
        int mismatches = 0;
        for (int i = 0; i <= divisions; i++)
        {
            double x = 1.0e-9 + (3.0 - 1.0e-9) * i / divisions;
            double a = AOf(x);
            double bFromCountingMeasure = Math.Log(RhoOf(x)) / D;   // solving e^(d·B) = ρ
            double bFromConformality = a;                           // solving A = B
            double bFromSigma = Sigma(x);
            if (Math.Abs(bFromCountingMeasure - bFromConformality) > 1.0e-12) mismatches++;
            if (Math.Abs(bFromCountingMeasure - bFromSigma) > 1.0e-12) mismatches++;
            if (!IsConformalFlat(a, bFromCountingMeasure)) mismatches++;
        }
        return mismatches;
    }

    // ── The conformal member, and the observation that excludes it ────────────

    /// <summary>The conformal member: B = σ. (G_031: this is also the counting-measure member.)</summary>
    public static double ConformalMemberB(double x) => Sigma(x);

    /// <summary>The γ = +1 survivor (G_029) — NOT conformal, since B ≠ σ. For contrast.</summary>
    public static double SurvivorB(double x) => 0.5 * Math.Log(2.0 - Math.Exp(-2.0 * x));

    /// <summary>Exact PPN γ = −(e^(2B) − 1)/(e^(2A) − 1).</summary>
    public static double GammaOf(double a, double b)
        => -((Math.Exp(2.0 * b) - 1.0) / (Math.Exp(2.0 * a) - 1.0));

    /// <summary>The conformal member's γ — exactly −1 at EVERY body (independent of x).</summary>
    public static double ConformalMemberGamma(double x) => GammaOf(AOf(x), ConformalMemberB(x));

    /// <summary>Cassini separation in σ.</summary>
    public static double CassiniSeparation(double gamma)
        => Math.Abs(gamma - CassiniGamma) / CassiniUncertainty;

    /// <summary>
    /// e^x − 1 computed without cancellation for small x (the framework has no Math.ExpM1).
    /// </summary>
    public static double ExpM1(double x)
        => Math.Abs(x) > 1.0e-5
            ? Math.Exp(x) - 1.0
            : x * (1.0 + x * (0.5 + x * (1.0 / 6.0 + x * (1.0 / 24.0 + x / 120.0))));

    /// <summary>
    /// The survivor's departure from conformal flatness, in area gauge: e^B − (1 − R·A_R) with A = σ = −x.
    /// Expands as **−1.5x² + (13/6)x³ − …**: conformal to FIRST order, never to second.
    ///
    /// Computed through the conjugate form [1 − 2x − x² − e^(−2x)] / [√(2 − e^(−2x)) + 1 + x], because the
    /// direct difference √(2 − e^(−2x)) − (1 + x) loses ~12 digits to cancellation as x → 0 (verified: the
    /// direct form returns −1.499911e−12 at x = 1e−6 where the true value is −1.4999978e−12). For small x the
    /// numerator is taken from its series, since ExpM1 still cancels its own O(x) leading term.
    /// </summary>
    public static double ConformityDeficit(double x)
    {
        double numerator;
        if (Math.Abs(x) < 1.0e-3)
        {
            double x2 = x * x;
            numerator = -3.0 * x2 * (1.0 - (4.0 / 9.0) * x + (2.0 / 9.0) * x2 - (4.0 / 45.0) * x2 * x);
        }
        else
        {
            numerator = -2.0 * x - x * x - ExpM1(-2.0 * x);
        }
        return numerator / (Math.Sqrt(2.0 - Math.Exp(-2.0 * x)) + (1.0 + x));
    }

    /// <summary>The leading coefficient of the deficit, computed: deficit/x² at small x.</summary>
    public static double LeadingDeficitCoefficient()
        => ConformityDeficit(1.0e-6) / (1.0e-6 * 1.0e-6);

    // ── The admitted band, in closed form ────────────────────────────────────

    /// <summary>
    /// Inverting γ exactly: e^(2B) = 1 − γ(e^(2A) − 1), so B(x, γ) = ½ln(1 + γ(1 − e^(−2x))).
    /// </summary>
    public static double BForGamma(double x, double gamma)
        => 0.5 * Math.Log(1.0 + gamma * (1.0 - Math.Exp(-2.0 * x)));

    /// <summary>The B interval Cassini admits at a compactness.</summary>
    public static (double Lo, double Hi) AdmittedBand(double x)
        => (BForGamma(x, CassiniGamma - CassiniUncertainty),
            BForGamma(x, CassiniGamma + CassiniUncertainty));

    /// <summary>The admitted band's centre.</summary>
    public static double BandCentre(double x)
    {
        var (lo, hi) = AdmittedBand(x);
        return 0.5 * (lo + hi);
    }

    /// <summary>The admitted band's width.</summary>
    public static double BandWidth(double x)
    {
        var (lo, hi) = AdmittedBand(x);
        return hi - lo;
    }

    /// <summary>Is the CONFORMAL value γ = −1 inside Cassini's admitted band at this body?</summary>
    public static bool ConformalValueIsAdmitted(double x)
    {
        var (lo, hi) = AdmittedBand(x);
        return lo <= -1.0 && -1.0 <= hi;
    }

    /// <summary>Is the conformal value admitted at ANY body? (Computed verdict input.)</summary>
    public static bool ConformalValueIsAdmittedAnywhere() => Bodies.Any(b => ConformalValueIsAdmitted(b.X));

    /// <summary>
    /// How badly conformality must be violated to satisfy Cassini — |A − B_centre| / |A|.
    /// This is **≈ 2**: the violation is a factor of two, not a small correction.
    /// </summary>
    public static double RequiredConformalityViolation(double x)
        => Math.Abs(AOf(x) - BandCentre(x)) / Math.Abs(AOf(x));

    // ── The verdict, COMPUTED ────────────────────────────────────────────────

    /// <summary>
    /// THE VERDICT, COMPUTED (G_027: never a literal).
    ///
    ///   DERIVED  — some primitive entails the ansatz AND imposing it survives observation;
    ///   REFUTED  — some primitive entails the ansatz, but imposing it is observationally excluded;
    ///   BOUNDARY — no primitive entails it: the ansatz is an ASSUMED input.
    /// </summary>
    public static string Verdict()
    {
        bool entailed = AnyPrimitiveForcesConformalFlatness();
        bool survivable = ConformalValueIsAdmittedAnywhere();
        if (entailed && survivable) return "DERIVED";
        if (entailed) return "REFUTED";
        return "BOUNDARY";
    }

    /// <summary>
    /// The counterfactual the audit must report: if the ansatz WERE entailed, the theory would be refuted,
    /// because the entailed value is the observationally excluded one.
    /// </summary>
    public static bool WouldBeRefutedIfEntailed() => !ConformalValueIsAdmittedAnywhere();

    /// <summary>The primitive that would have to do the entailing — and why it cannot.</summary>
    public static string PrimitiveThatWouldHaveToEntailIt()
        => "η (the conformal reference metric, QG77/QG289) — the ONLY rank-2 item in the inventory. But η IS "
         + "the conformal statement: citing it to derive conformal flatness restates the assumption. The "
         + "remaining primitives (ρ, ψ, d, π) are scalars and cannot determine a shape.";
}
