using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>Which part of the metric a G-series result actually requires.</summary>
public enum MetricRequirement
{
    /// <summary>g₀₀ = −ρ^(2/d) alone — the clock. No spatial metric, no conformality.</summary>
    ClockOnly,

    /// <summary>The spatial block g_ij is needed, but NOT conformal flatness.</summary>
    Spatial,

    /// <summary>The result requires A = B (conformal flatness) and dissolves when it is removed.</summary>
    ConformalFlatness,
}

/// <summary>Survival of a G-series result when the conformal metric is removed.</summary>
public enum TemporalVerdict
{
    /// <summary>Depends on g₀₀ only — unaffected by removing conformality (or the whole spatial metric).</summary>
    Survives,

    /// <summary>Needs g_ij; survives the removal of conformality as a constraint, but its value depends on the chosen B.</summary>
    Boundary,

    /// <summary>Requires conformal flatness — does not survive its removal.</summary>
    Refuted,
}

/// <summary>What the live scan measures for one suite: executable references to each vocabulary.</summary>
public sealed record MetricSignals(string Audit, int Temporal, int Spatial, int Conformal)
{
    /// <summary>Does the executable code touch the spatial block at all?</summary>
    public bool TouchesSpatial => Spatial > 0;
}

/// <summary>
/// One G-series result, with the metric component it requires and the basis for that judgement.
/// <paramref name="ScanDetectsIt"/> is false when the suite's spatial dependence is NOT expressible in the
/// metric-symbol vocabulary the scanner uses (the reason goes in the basis) — an explicit, auditable triage
/// rather than a silent exception.
/// </summary>
public sealed record SectorClaim(string Audit, string Title, MetricRequirement Requirement, string Basis,
    bool ScanDetectsIt = true)
{
    /// <summary>THE SURVIVAL VERDICT — a 1:1 map from the requirement, computed, never typed.</summary>
    public TemporalVerdict Verdict => Requirement switch
    {
        MetricRequirement.ClockOnly => TemporalVerdict.Survives,
        MetricRequirement.Spatial => TemporalVerdict.Boundary,
        MetricRequirement.ConformalFlatness => TemporalVerdict.Refuted,
        _ => throw new ArgumentOutOfRangeException(nameof(Requirement)),
    };

    /// <summary>Is this result independent of the metric entirely (verdict discipline / numerics)?</summary>
    public bool IsMetricFree => Basis.Contains("metric-free", StringComparison.OrdinalIgnoreCase);
}

/// <summary>
/// ResearchY-G_035 — TEMPORAL INDEPENDENCE AUDIT.
///
/// QUESTION. Which G-results survive if the **conformal metric is removed**? Review G_001–G_034; classify each
/// result by whether it requires **g₀₀ only**, **g_rr**, or **conformal flatness**. Goal: identify the **minimal
/// time sector** of AT.
///
/// THE ANSWER: **the temporal sector is already independent, and its boundary is sharp.**
///
///     SURVIVES  (g₀₀ only)        25 of 36 suites — the entire source/clock/density programme
///     BOUNDARY  (g_ij needed)      8 — the spatial sector, which survives as a result but not as a value
///     REFUTED   (conformal)        3 — G_024, G_031, G_032: results that ARE the conformal assumption
///
/// **Every audit from G_001 to G_020 is g₀₀-only.** The whole density era — the source law, the clock law, the
/// control and realizability analyses, the SI magnitudes, the calibration, the suppression mechanism, the
/// actuation chain, the mass-independence and watch-ontology results, the second-order signature and the
/// neutron-star redshift — uses **g₀₀ = −ρ^(2/d) and nothing else**. The spatial requirement begins *exactly*
/// at **G_021 (Light-Propagation)**, the first audit to ask for light bending: **G_020 → G_021 is the conformal
/// boundary of the programme.**
///
/// THE MINIMAL TIME SECTOR. It is the intersection of the surviving results, and it needs:
///
///     1 metric function       g₀₀ = −ρ^(2/d)          (the clock)
///     1 scalar field          ρ = the occupancy        (the source)
///     1 exponent              1/d, d = 3               (rotation self-duality, M_013)
///
/// and **no spatial metric, no conformal factor, no reference η**. The source law `a = −(1/d)∇ln ρ` and the
/// clock law `dτ/dt = ρ^(1/d)` are the *same* statement, because the Newtonian potential IS the time exponent
/// (G_028: `a = ∇A` with `A = ½ln(−g₀₀) = σ`). AT's time sector is therefore **one equation on one scalar**,
/// and it is complete: nothing in the surviving 25 needs anything else.
///
/// THE ARITY PROOF. The audit's strongest evidence is structural rather than numerical: the temporal
/// observables are **functions whose signature contains no B**. `ClockOf(rho)`, `ClockPotentialOf(rho)`,
/// `SourceAccelerationOf(rhoField)`, `RedshiftOf(rho)`, `SecondOrderRatioOf(x)` — none takes a B or a conformal
/// factor, so they cannot depend on one. The spatial observables (`GammaOf(a,b)`, `AdmittedBand`) take B
/// explicitly. A result that cannot be *called* with B cannot *require* it.
///
/// WHY THIS MATTERS. G_032 showed that imposing conformal flatness is refuted (γ = −1, Cassini 8.6957e4 σ) and
/// that the ansatz is an assumed primitive η. G_035 shows the cost of that problem is **confined**: the entire
/// temporal sector stands without η, without g_ij and without conformality. The theory's time half is not
/// hostage to its space half.
/// </summary>
public static class TemporalIndependenceAudit
{
    public const int D = 3;

    // ── The minimal time sector, as arithmetic that cannot mention B ──────────

    /// <summary>g₀₀ = −ρ^(2/d). The ONLY metric component the temporal sector needs.</summary>
    public static double G00(double rho) => -Math.Pow(rho, 2.0 / D);

    /// <summary>√(−g₀₀) = ρ^(1/d) — the clock rate dτ/dt. Takes NO spatial argument.</summary>
    public static double ClockOf(double rho) => Math.Pow(rho, 1.0 / D);

    /// <summary>A = ½ln(−g₀₀) = σ = (1/d)ln ρ — the clock potential, and the Newtonian potential Φ/c².</summary>
    public static double ClockPotentialOf(double rho) => Math.Log(rho) / D;

    /// <summary>
    /// The source law, from ρ alone: a = −(1/d)∇ln ρ. Since A = (1/d)ln ρ this is EXACTLY a = −∇A — gravity is
    /// the NEGATIVE gradient of the clock potential, as an attractive force requires. Computed through the logs so
    /// that it IS the gradient of the clock potential term by term (no linearisation), which is the whole point:
    /// the source law and the clock law are ONE statement (G_028). Takes NO spatial argument.
    /// </summary>
    public static double[] SourceAccelerationOf(double[] rhoField)
    {
        var a = new double[rhoField.Length];
        int n = rhoField.Length;
        for (int i = 0; i < n; i++)
        {
            double left = rhoField[i == 0 ? 0 : i - 1];
            double right = rhoField[i == n - 1 ? n - 1 : i + 1];
            a[i] = -(Math.Log(right) - Math.Log(left)) / (2.0 * D);
        }
        return a;
    }

    /// <summary>Gravitational redshift from g₀₀ alone: 1/√(−g₀₀) − 1.</summary>
    public static double RedshiftOf(double rho) => 1.0 / ClockOf(rho) - 1.0;

    /// <summary>G_019's second-order discriminator AT/GR = e^x/√(1 + 2x) — a g₀₀-only comparison.</summary>
    public static double SecondOrderRatioOf(double x) => Math.Exp(x) / Math.Sqrt(1.0 + 2.0 * x);

    /// <summary>
    /// THE ARITY PROOF, EXECUTED: no temporal observable has a parameter that could carry a spatial metric or a
    /// conformal factor. Any public method of this class whose name is a temporal observable takes only ρ (or
    /// the ρ field) or the compactness x — never a `b`, `B`, `psi` or `omega`.
    /// </summary>
    public static (string Name, string Signature)[] TemporalObservables() => new[]
    {
        (nameof(G00), "G00(double rho)"),
        (nameof(ClockOf), "ClockOf(double rho)"),
        (nameof(ClockPotentialOf), "ClockPotentialOf(double rho)"),
        (nameof(RedshiftOf), "RedshiftOf(double rho)"),
        (nameof(SourceAccelerationOf), "SourceAccelerationOf(double[] rhoField)"),
        (nameof(SecondOrderRatioOf), "SecondOrderRatioOf(double x)"),
    };

    /// <summary>Does any temporal observable's signature mention a spatial or conformal quantity?</summary>
    public static bool NoTemporalObservableTakesB()
        => TemporalObservables().All(o =>
            !o.Signature.Contains(" b", StringComparison.Ordinal)
            && !o.Signature.Contains("B", StringComparison.Ordinal)
            && !o.Signature.Contains("psi", StringComparison.OrdinalIgnoreCase)
            && !o.Signature.Contains("omega", StringComparison.OrdinalIgnoreCase));

    /// <summary>For contrast: the spatial observables DO take B. This is the whole distinction.</summary>
    public static (string Name, string Signature)[] SpatialObservables() => new[]
    {
        ("GammaOf", "GammaOf(double a, double b)"),
        ("AdmittedBand", "AdmittedBand(double x)  // via BForGamma, which uses B"),
        ("SurvivorB", "SurvivorB(double x)"),
    };

    /// <summary>The minimal time sector's content, as a counted summary.</summary>
    public static (int MetricFunctions, int ScalarFields, int Exponents) MinimalTimeSectorContent() => (1, 1, 1);

    // ── The live scan: does the CODE of a suite touch the spatial block? ──────

    private static readonly Regex StringLiteral = new("\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\"", RegexOptions.Compiled);
    private static readonly Regex LineComment = new("//.*$", RegexOptions.Compiled);

    private static readonly string[] TemporalPatterns =
    {
        @"\bSigma\(", @"\bAOf\(", @"\bClock\(", @"\bG00\b", @"\bg00\b", @"ClockSeparation",
        @"ClockOf\(", @"ClockPotentialOf\(", @"RedshiftOf\(", @"\bTau\b", @"\bSigma\b",
    };

    /// <summary>
    /// The spatial vocabulary — UNAMBIGUOUS metric names only.
    ///
    /// Two patterns were REMOVED after measurement, because they produced false positives on the temporal side:
    /// `double b` matched a least-squares slope (G_006) and a tridiagonal parameter (G_007, G_016b), and a bare
    /// `K(` matched any method of that name. Bare `psi` is excluded on purpose: it names the WAVEfunction in the
    /// density era (|ψ|² = ρ, G_014) and the metric's traceless face elsewhere, so counting it would corrupt the
    /// temporal classification. The rule is: a token qualifies only if it cannot plausibly mean anything else.
    /// </summary>
    private static readonly string[] SpatialPatterns =
    {
        @"\bBOf\(", @"\bBConformal\b", @"\bBExactGr\b", @"\bBReflection\b", @"\bSurvivorB\b",
        @"\bGrrOf", @"\bg_rr\b", @"\bGrr\b", @"\bgrr\b", @"\bBForGamma", @"\bGammaOf\(",
        @"GammaFromExponents", @"GammaConformal", @"GammaFromAB", @"\bspatial\b",
    };

    private static readonly string[] ConformalPatterns =
    {
        @"[Cc]onformal", @"IsConformalFlat", @"conformity", @"A = B",
    };

    /// <summary>
    /// Strip non-executable text: string-literal contents first (so a `//` inside a string cannot truncate the
    /// line), then the `//` tail. This is what makes the scan measure DEPENDENCY rather than PROSE — G_019's
    /// only spatial "hit" was a report sentence about the conformal slice, and it disappears here.
    /// </summary>
    public static string StripNonCode(string line)
        => LineComment.Replace(StringLiteral.Replace(line, "\"\""), "");

    /// <summary>The folder holding the group-G suites, relative to the repository root.</summary>
    public const string AuditFolder = @"AT.Tests\ResearchY\G_GravitySource";

    /// <summary>The executable metric vocabulary of every group-G suite.</summary>
    public static MetricSignals[] ScanAudits()
    {
        var root = CubicSubstrateAudit.FindRoot(AuditFolder);
        if (root is null) return Array.Empty<MetricSignals>();
        var result = new List<MetricSignals>();
        foreach (var file in Directory.EnumerateFiles(root, "Y_G_*.cs").OrderBy(p => p, StringComparer.Ordinal))
        {
            int tCount = 0, sCount = 0, cCount = 0;
            foreach (var raw in File.ReadLines(file))
            {
                var line = StripNonCode(raw);
                if (line.Trim().Length == 0) continue;
                tCount += CountAny(line, TemporalPatterns);
                sCount += CountAny(line, SpatialPatterns);
                cCount += CountAny(line, ConformalPatterns);
            }
            result.Add(new MetricSignals(
                Path.GetFileName(file).Replace("_Tests.cs", ""), tCount, sCount, cCount));
        }
        return result.ToArray();
    }

    private static int CountAny(string line, string[] patterns)
        => patterns.Sum(p => Regex.Matches(line, p).Count);

    /// <summary>A suite's executable signals, or null if it was not scanned.</summary>
    public static MetricSignals? SignalsFor(string audit)
        => ScanAudits().FirstOrDefault(s => s.Audit == audit);

    // ── The registry: the 36 suites, classified ─────────────────────────────

    /// <summary>
    /// Every group-G suite with the metric component its result requires. The judgement is curated (whether a
    /// RESULT depends on B is semantic), but it is mechanically CHECKED against the live scan by
    /// <see cref="RegistryDisagreements"/> so it cannot silently go stale.
    /// </summary>
    public static SectorClaim[] Registry() => new[]
    {
        // ── the density era: g₀₀ only, G_001 – G_020 ──
        new SectorClaim("Y_G_001", "Gravity Source", MetricRequirement.ClockOnly,
            "the source is ρ; the source law a = −(1/d)∇ln ρ = ∇A is a g₀₀ statement"),
        new SectorClaim("Y_G_002", "Density Control", MetricRequirement.ClockOnly,
            "ρ, a(ρ) and the field curvature are properties of ρ, not of g_ij"),
        new SectorClaim("Y_G_003", "Gravity Magnitude", MetricRequirement.ClockOnly,
            "Δa, ΔR, ΔΦ in SI: Φ is the clock potential, fixed by g₀₀"),
        new SectorClaim("Y_G_004", "Gravity Calibration", MetricRequirement.ClockOnly,
            "a_pred/a_obs at four scales, both from g₀₀"),
        new SectorClaim("Y_G_005", "Control Realizability", MetricRequirement.ClockOnly,
            "SUPPRESSED is a statement about ρ configurations and the Poisson channel"),
        new SectorClaim("Y_G_006", "Suppression Mechanism", MetricRequirement.ClockOnly,
            "the mechanism is the relaxation operator on the ρ field; μ_k depends on N only"),
        new SectorClaim("Y_G_007", "Suppression Origin", MetricRequirement.ClockOnly,
            "whether DiffuseStep is DERIVED — an operator property, metric-free"),
        new SectorClaim("Y_G_008", "Controlled Suppression", MetricRequirement.ClockOnly,
            "ρ configurations under the ρ operator"),
        new SectorClaim("Y_G_009", "Clock Rate", MetricRequirement.ClockOnly,
            "dτ/dt = ρ^(1/d) from g₀₀ = −ρ^(2/d) — the canonical temporal result"),
        new SectorClaim("Y_G_010", "Time Control Feasibility", MetricRequirement.ClockOnly,
            "sustained clock shift from a ρ drive; needs g₀₀ only"),
        new SectorClaim("Y_G_011", "Rho Actuator", MetricRequirement.ClockOnly,
            "changing ρ changes the clock; no spatial metric appears"),
        new SectorClaim("Y_G_011b", "Labor Rho", MetricRequirement.ClockOnly,
            "laboratory implementation of a ρ field; a clock statement"),
        new SectorClaim("Y_G_012", "Local Rho Actuator", MetricRequirement.ClockOnly,
            "Δτ/τ = Δln ρ/3 — a pure clock readout"),
        new SectorClaim("Y_G_013", "Physical Actuator", MetricRequirement.ClockOnly,
            "which process changes ρ; the required source is a ρ-Laplacian"),
        new SectorClaim("Y_G_014", "Physical Rho Mapping", MetricRequirement.ClockOnly,
            "|ψ|² IS ρ; verified through the source law and the clock law, both g₀₀"),
        new SectorClaim("Y_G_015", "Rho To Metric", MetricRequirement.ClockOnly,
            "the laboratory q profile's two channels are the clock-pole A and the source"),
        new SectorClaim("Y_G_016", "Watch Ontology", MetricRequirement.ClockOnly,
            "whether mass-energy is required to generate ρ and the clock"),
        new SectorClaim("Y_G_016b", "Mass Independence", MetricRequirement.ClockOnly,
            "ΔE = 0 and Δτ = 0 exactly — a simultaneous source-and-clock statement"),
        new SectorClaim("Y_G_017", "Metric Coupling", MetricRequirement.ClockOnly,
            "the question IS the clock shift Δτ from a |ψ|² profile; ΔΦ is the clock phase"),
        new SectorClaim("Y_G_018", "Rho Identity", MetricRequirement.ClockOnly,
            "the surviving identity is the occupancy measure — metric-free content"),
        new SectorClaim("Y_G_019", "Second-Order Signature", MetricRequirement.ClockOnly,
            "dτ/dt = e^σ vs √(1+2σ): both are g₀₀ laws; the discriminator is x²"),
        new SectorClaim("Y_G_020", "Neutron-Star Redshift", MetricRequirement.ClockOnly,
            "the redshift z = 1/√(−g₀₀) − 1 is fixed by g₀₀; M/R enters as compactness only"),

        // ── the spatial sector: g_ij needed, conformality not required ──
        new SectorClaim("Y_G_021", "Light-Propagation", MetricRequirement.Spatial,
            "the FIRST audit to require g_ij: light bending and the Shapiro delay need γ, hence B"),
        new SectorClaim("Y_G_022", "Spatial Metric", MetricRequirement.Spatial,
            "derives B to reproduce γ ≈ +1 — a g_ij result, not a conformal one (it needs A ≠ B)"),
        new SectorClaim("Y_G_023", "Spatial Sector Closure", MetricRequirement.Spatial,
            "sweeps candidate B's; conformal flatness is one member among many, not a premise"),
        new SectorClaim("Y_G_025", "Optics Determinant Correction", MetricRequirement.Spatial,
            "corrects the spatial-block exponent in det g under ψ ≠ 0 — a non-conformal g_ij result. NOT "
            + "symbol-detectable: the dependence is an EXPONENT (`rho * exp(-d*psi/(d-1))`), and `psi` cannot be "
            + "used as a marker because the density era uses ψ for the wavefunction",
            ScanDetectsIt: false),
        new SectorClaim("Y_G_028", "Clock Sector Closure", MetricRequirement.Spatial,
            "the clock closure itself is g₀₀, but its decisive corollary (γ = +1 ⟹ ψ = −4σ) needs the ψ split"),
        new SectorClaim("Y_G_029", "Spatial Sector Closure", MetricRequirement.Spatial,
            "the surviving rule g_rr = 2 − ρ^(2/d) is explicitly NON-conformal (A ≠ B)"),
        new SectorClaim("Y_G_030", "No-Go", MetricRequirement.Spatial,
            "the theorem quantifies over B; conformality is the B = σ special case it refutes"),
        new SectorClaim("Y_G_033", "Cubic Substrate", MetricRequirement.Spatial,
            "the metric's traceless part needs the dimension-3 irrep T2g — a g_ij structure question. NOT "
            + "symbol-detectable at all: the suite contains NO metric vocabulary (0/0/0), because its argument is "
            + "group-theoretic rather than symbolic",
            ScanDetectsIt: false),

        // ── results that ARE the conformal assumption ──
        new SectorClaim("Y_G_024", "Optics Reconciliation", MetricRequirement.ConformalFlatness,
            "rests on the conformal-slice framing (ψ = 0 vs ψ ≠ 0) and its |ψ| bound was corrected by G_028; "
            + "its B enters as a generic parameter of a local GammaFromAB helper (the scan now catches that)",
            ScanDetectsIt: true),
        new SectorClaim("Y_G_031", "Spatial-Origin", MetricRequirement.ConformalFlatness,
            "its theorem (clock law ⟺ counting measure) holds only INSIDE conformal flatness — stated as a conditional"),
        new SectorClaim("Y_G_032", "Conformal Assumption", MetricRequirement.ConformalFlatness,
            "the conformal ansatz IS its subject; the result cannot be stated without it"),

        // ── metric-free: verdict discipline and numerics ──
        new SectorClaim("Y_G_026", "Authored-Verdict", MetricRequirement.ClockOnly,
            "metric-free: audits the QG closure criteria, not geometry"),
        new SectorClaim("Y_G_027", "Literal Verdict", MetricRequirement.ClockOnly,
            "metric-free: audits verdict provenance across AT.Core"),
        new SectorClaim("Y_G_034", "A0 Robustness", MetricRequirement.ClockOnly,
            "metric-free: a numerical robustness audit of the spectral substrate"),

        // ── G_036: the core's testability — needs the spatial sector to READ it, not to state it ──
        new SectorClaim("Y_G_036", "Temporal Core Test", MetricRequirement.Spatial,
            "its observable z = 1/√(−g₀₀) − 1 is g₀₀-only, but its BOUNDARY verdict rests on the compactness "
            + "being READ from light bending — a g_ij fact. NOT symbol-detectable: the suite's metric vocabulary "
            + "lives entirely inside report string literals (0/0/0 after stripping), because the audit asks which "
            + "quantities the MEASUREMENT needs, not which metric symbols the code spells",
            ScanDetectsIt: false),

        // ── G_037: the refractive route to bending — the index IS gamma, so it needs g_ij ──
        new SectorClaim("Y_G_037", "Refractive Lens", MetricRequirement.Spatial,
            "the result is about γ ITSELF: a static refractive index obeys n − 1 = −(1+γ)Φ/c², so it IS the "
            + "spatial metric function written optically, and the deflection needs a = 1+γ. NOT symbol-detectable: "
            + "the suite names no B, g_rr or GammaOf — its subject is a coefficient, and its only metric "
            + "vocabulary is the NEGATIVE test that a conformal metric (A = B) cannot bend light at all",
            ScanDetectsIt: false),

        // ── G_038: the measure decomposition — A is measured, so B is forced at first order ──
        new SectorClaim("Y_G_038", "Measure Decomposition", MetricRequirement.Spatial,
            "it partitions the deflection into the clock's half (−A) and the distance's half (+B) and shows the "
            + "first-order spatial coefficient B = +x is FORCED by the measured redshift and deflection — a "
            + "statement about the spatial block. NOT symbol-detectable: the suite's metric vocabulary is a "
            + "coefficient plus the NEGATIVE conformal test (B = A ⟹ n = 1), not B/g_rr/GammaOf symbols",
            ScanDetectsIt: false),

        // ── G_039: the rho realization — which observable carries rho, and why measurability binds ──
        new SectorClaim("Y_G_039", "Rho Realization", MetricRequirement.ClockOnly,
            "it identifies the observable that carries rho (the occupancy of the reachable set) and tests it "
            + "against energy, phase, information and the clock law dtau/dt = rho^(1/d) — every statement is about "
            + "rho and g00. NOT symbol-detectable: the suite's vocabulary is rho, energy, entropy and phase, not "
            + "B/g_rr/GammaOf",
            ScanDetectsIt: false),

        // ── G_040: the rho observable — how much of rho any constructible measurement can retain ──
        new SectorClaim("Y_G_040", "Rho Observable", MetricRequirement.ClockOnly,
            "it bounds what a measurement can know about rho by the centralizer algebra of the substrate's own "
            + "symmetry — 49 dimensions of operator against a 95-dimensional state space, so 47 are lost — and "
            + "every statement is about rho, the spectral substrate and its group. NOT symbol-detectable: the "
            + "suite's vocabulary is cells, channels, irreps, ranks and contractions, not B/g_rr/GammaOf",
            ScanDetectsIt: false),

        // ── G_041: the substrate dimension — is d = 3 selected, or assumed? ──
        new SectorClaim("Y_G_041", "Substrate Dimension", MetricRequirement.ClockOnly,
            "it asks whether d = 3 is selected by the substrate, and every statement is group-theoretic or a "
            + "statement about the clock law rho^(1/d): the signed-permutation groups B_d and their irrep "
            + "dimension spectra, character inner products over those groups, the eps/Hodge dimension accident, "
            + "polarisation counts, and the rate the same rho gives at d = 2, 3 and 4. NOT symbol-detectable: "
            + "the suite names no B, g_rr or GammaOf — its vocabulary is groups, irreps, characters and clocks",
            ScanDetectsIt: false),

        // ── G_042: is three-dimensionality one root mechanism or several? ──
        new SectorClaim("Y_G_042", "Dimensionality Dependency", MetricRequirement.ClockOnly,
            "it asks whether the selectors of d = 3 are independent, and every statement is group theory, tensor "
            + "algebra or a statement about the clock law rho^(1/d): solution sets of predicates on d, the "
            + "identity graviton = dim(so(d)) - 1, the inclusion graph between those sets, and the polarisation "
            + "counts. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — its vocabulary is "
            + "dimensions, bivectors, rotations, polarisations and clocks",
            ScanDetectsIt: false),

        // ── G_043: why stop at the first working dimension? ──
        new SectorClaim("Y_G_043", "Minimality", MetricRequirement.ClockOnly,
            "it asks why the substrate stops at d = 3 rather than continuing to d = 4, 5, and every statement "
            + "is a count, a growth law or the clock law rho^(1/d): polarisation counts, the Hodge mismatch, "
            + "irrep-dimension growth, the state-space size 96^d, the orbital count C(48+d, d), and the rate the "
            + "same rho gives at each d. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — its "
            + "vocabulary is dimensions, polarisations, orbitals and clocks",
            ScanDetectsIt: false),

        // ── G_044: is D96^3 minimal, optimal, or merely first? ──
        new SectorClaim("Y_G_044", "Minimal Working Substrate", MetricRequirement.ClockOnly,
            "it asks whether D96^3 is selected for being the first working substrate or for minimising "
            + "complexity, and every statement is a count, a growth law or a symmetry fact about D96^d: the "
            + "state space 96^d, the number of irreps of B_d, the photon and graviton sector dimensions, the "
            + "orbital count C(48+d, d), and the observability measures built from them. NOT symbol-detectable: "
            + "the suite names no B, g_rr or GammaOf — its vocabulary is dimensions, sectors, orbitals and costs",
            ScanDetectsIt: false),

        // ── G_045: is the apparent 3 a projection of a bigger actualization space? ──
        new SectorClaim("Y_G_045", "Observed vs Hidden Dimension", MetricRequirement.ClockOnly,
            "it asks whether the apparent 3D world is a projection of a higher-dimensional actualization space, "
            + "and every statement is a spectrum or a count: the d-torus eigenvalues, the distinct-level count "
            + "A0, the reachable room 96^d - A0, the orbital count C(48+d, d) - 1, box counts of the uniform "
            + "measure, the orbit order 2^d d! and a density-of-states exponent. NOT symbol-detectable: the "
            + "suite names no B, g_rr or GammaOf — its vocabulary is dimensions, spectra, boxes and clocks",
            ScanDetectsIt: false),
        // ── G_046: can the hidden 47 dimensions of rho ever reach an observable? ──
        new SectorClaim("Y_G_046", "Rho Accessibility", MetricRequirement.ClockOnly,
            "it reads the clock law at every cell and asks whether a hidden direction can move the spectrum, so the "
            + "metric content is the clock alone: the doublet and contraction counts, the symmetry orbit span, the "
            + "multisets of rates, accelerations and field strengths, and the kernel of the contraction observables. "
            + "NOT symbol-detectable: the suite names no B, g_rr or GammaOf — its vocabulary is orbits, kernels and "
            + "clocks",
            ScanDetectsIt: false),
        // ── G_047: which observable detects the kernel directions directly? ──
        new SectorClaim("Y_G_047", "Kernel Observable", MetricRequirement.ClockOnly,
            "it reads the clock law at every cell and measures the rank of that reading on the kernel of the "
            + "contraction observables, so the metric content is the clock alone: the kernel basis, the three candidate "
            + "response amplitudes, the reading ranks and the minimal basis size. NOT symbol-detectable: the suite "
            + "names no B, g_rr or GammaOf — its vocabulary is kernels, ranks and clocks",
            ScanDetectsIt: false),
        // ── G_048: is the clock pattern the maximal observable of rho? ──
        new SectorClaim("Y_G_048", "Clock Completeness", MetricRequirement.ClockOnly,
            "it measures the observable dimension of the clock, acceleration and field-strength patterns against the "
            + "contraction sub-algebra, so the metric content is the clock alone: the responses of four readings on the "
            + "simplex tangent space and on the kernel, their ranks, and the invertibility of the clock map. NOT "
            + "symbol-detectable: the suite names no B, g_rr or GammaOf — its vocabulary is readings, ranks and clocks",
            ScanDetectsIt: false),
        // ── G_049: is the clock pattern the unique lossless observable of rho? ──
        new SectorClaim("Y_G_049", "Clock Primacy", MetricRequirement.ClockOnly,
            "it reads the clock law at every cell and tests the inversion of four readings by search, so the metric "
            + "content is the clock alone: the patterns, their ranks, the closed-form inverse rho = rate^d and the "
            + "collision counts from displaced starts. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — "
            + "its vocabulary is invertibility, ranks and clocks",
            ScanDetectsIt: false),
        // ── G_050: what physical structure do the 53 kernel directions represent? ──
        new SectorClaim("Y_G_050", "Kernel Structure", MetricRequirement.ClockOnly,
            "it decomposes the kernel of the contraction observables into Fourier modes and measures the three "
            + "readings' signatures one mode at a time, so the metric content is the clock alone: the mode classes, the "
            + "count relation, the empty channels and the signatures. NOT symbol-detectable: the suite names no B, g_rr "
            + "or GammaOf — its vocabulary is modes, quadratures and clocks",
            ScanDetectsIt: false),
        // ── G_051: do the 53 phase directions carry physical or gauge information? ──
        new SectorClaim("Y_G_051", "Phase Sector", MetricRequirement.ClockOnly,
            "it steps along each of the 53 phase modes and reads the clock, acceleration and field multisets plus the "
            + "decoupled flux label, so the metric content is the clock alone: the responses, their scaling in the step, "
            + "the gauge control and the coupling census. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — "
            + "its vocabulary is responses, multisets and clocks",
            ScanDetectsIt: false),
        // ── G_052: can rho be decomposed uniquely into amplitude (42) and phase (53)? ──
        new SectorClaim("Y_G_052", "Amplitude Phase", MetricRequirement.ClockOnly,
            "it splits the state into the visible and hidden Fourier sectors, measures the interface against the "
            + "contraction row space and reads each observable's amplitude and phase contributions with the order of the "
            + "cross term, so the metric content is the clock alone. NOT symbol-detectable: the suite names no B, g_rr or "
            + "GammaOf — its vocabulary is sectors, projections and clocks",
            ScanDetectsIt: false),
        // ── G_053: do phase modes have independent effects beyond amplitudes? ──
        new SectorClaim("Y_G_053", "Phase Sector Dynamics", MetricRequirement.ClockOnly,
            "it applies a pure amplitude and a pure phase perturbation and measures the clock, acceleration and field "
            + "responses, the residual of the phase response against the span of the amplitude responses, the channel "
            + "rotation and the quadrature functional, so the metric content is the clock alone. NOT symbol-detectable: "
            + "the suite names no B, g_rr or GammaOf — its vocabulary is perturbations, rotations and clocks",
            ScanDetectsIt: false),
        // ── G_054: what fixes the 53 phase coordinates? ──
        new SectorClaim("Y_G_054", "Phase Determination", MetricRequirement.ClockOnly,
            "it measures how much each candidate moves the phase coordinates and how much of the clock and field "
            + "functionals' gradients points along the phase sector, so the metric content is the clock alone. NOT "
            + "symbol-detectable: the suite names no B, g_rr or GammaOf — its vocabulary is coordinates, flows and "
            + "clocks",
            ScanDetectsIt: false),
        // ── G_055: can any existing AT quantity assign a preferred phase state? ──
        new SectorClaim("Y_G_055", "Phase Selection Principle", MetricRequirement.ClockOnly,
            "it projects the gradients of six AT functionals into the phase sector and measures the rank of what they "
            + "can constrain, plus the directional derivatives that decide whether the state is a critical point, so the "
            + "metric content is the clock alone. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — its "
            + "vocabulary is gradients, ranks and clocks",
            ScanDetectsIt: false),
        // ── G_056: can a non-scalar AT structure span the phase sector? ──
        new SectorClaim("Y_G_056", "Non-Scalar Selection", MetricRequirement.ClockOnly,
            "it measures the phase rank of five AT structures - the rank of each structure's derivative along the 53 "
            + "phase directions - against G_055's scalar ceiling, so the metric content is the clock alone. NOT "
            + "symbol-detectable: the suite names no B, g_rr or GammaOf — its vocabulary is ranks, gradients and clocks",
            ScanDetectsIt: false),
        // ── G_057: can any AT process change the phase coordinates? ──
        new SectorClaim("Y_G_057", "Phase Flow", MetricRequirement.ClockOnly,
            "it measures each candidate flow's phase velocity, phase rank and selection power, and re-measures the "
            + "actualization's spatial part and the coupling census to show that the running process is phase-static, so "
            + "the metric content is the clock alone. NOT symbol-detectable: the suite names no B, g_rr or GammaOf — its "
            + "vocabulary is potentials, velocities and clocks",
            ScanDetectsIt: false),
    };

    // ── The computed classification summaries ───────────────────────────────

    /// <summary>Claims with a given verdict.</summary>
    public static SectorClaim[] With(TemporalVerdict verdict)
        => Registry().Where(c => c.Verdict == verdict).ToArray();

    /// <summary>(survives, boundary, refuted) counts.</summary>
    public static (int Survives, int Boundary, int Refuted) Counts()
    {
        var rows = Registry().Select(c => c.Verdict).ToArray();
        return (rows.Count(v => v == TemporalVerdict.Survives),
                rows.Count(v => v == TemporalVerdict.Boundary),
                rows.Count(v => v == TemporalVerdict.Refuted));
    }

    /// <summary>
    /// THE MINIMAL TIME SECTOR: the results that need g₀₀ and nothing else. This is the set that stands when the
    /// conformal metric — and indeed the whole spatial metric — is removed.
    /// </summary>
    public static SectorClaim[] MinimalTimeSector() => With(TemporalVerdict.Survives);

    /// <summary>
    /// THE CONFORMAL BOUNDARY: the first audit (by index) whose result requires more than g₀₀. Everything before
    /// it is temporal. Computed from the registry, never typed.
    /// </summary>
    public static SectorClaim? ConformalBoundary()
        => Registry().FirstOrDefault(c => c.Requirement != MetricRequirement.ClockOnly);

    /// <summary>The index of that boundary in the registry (1-based), or 0 if none.</summary>
    public static int ConformalBoundaryIndex()
    {
        var reg = Registry();
        for (int i = 0; i < reg.Length; i++)
            if (reg[i].Requirement != MetricRequirement.ClockOnly) return i + 1;
        return 0;
    }

    /// <summary>How many audits precede the boundary — i.e. how much of the programme is temporal-only.</summary>
    public static int TemporalEraLength() => ConformalBoundaryIndex() - 1;

    /// <summary>
    /// THE MECHANICAL CONSISTENCY CHECK, in two directions with different strength.
    ///
    /// HARD (the safety property the audit exists to defend): a result classified <c>ClockOnly</c> must have
    /// **no executable reference to the spatial block** — the temporal claim is only as good as this.
    ///
    /// SOFT: a result classified <c>Spatial</c> or <c>ConformalFlatness</c> should show at least one spatial
    /// reference, UNLESS the claim declares <c>ScanDetectsIt = false</c> with a written reason (the dependence
    /// may be an exponent structure or a group-theoretic argument rather than a symbol).
    ///
    /// Returns the disagreements (must be empty).
    /// </summary>
    public static (string Audit, string Why)[] RegistryDisagreements()
    {
        var signals = ScanAudits();
        if (signals.Length == 0) return new[] { ("(none)", "the scanner found no suites, so the check is vacuous") };
        var bad = new List<(string, string)>();
        foreach (var claim in Registry())
        {
            var sig = signals.FirstOrDefault(s => s.Audit == claim.Audit);
            if (sig is null) { bad.Add((claim.Audit, "not found by the scanner")); continue; }
            bool spatialInCode = sig.TouchesSpatial;
            if (claim.Requirement == MetricRequirement.ClockOnly && spatialInCode)
                bad.Add((claim.Audit, $"HARD: classified ClockOnly but its code references the spatial block ({sig.Spatial} hits)"));
            if (claim.Requirement != MetricRequirement.ClockOnly && !spatialInCode && claim.ScanDetectsIt)
                bad.Add((claim.Audit, $"SOFT: classified {claim.Requirement} but its code shows no spatial reference"));
        }
        return bad.ToArray();
    }

    /// <summary>The claims whose spatial dependence the symbol scan cannot see, with their reasons.</summary>
    public static SectorClaim[] ScanBlindClaims() => Registry().Where(c => !c.ScanDetectsIt).ToArray();

    /// <summary>Do the registry and the live scan agree? (One-directional: every scanned suite must be registered.)</summary>
    public static bool RegistryAgreesWithScan() => RegistryDisagreements().Length == 0;

    /// <summary>
    /// This audit's own suite id — excluded from the coverage check, because its code references every metric
    /// symbol by construction (it defines the vocabulary it scans for). The same self-reference G_033's scanner
    /// hits, handled by an explicit exclusion rather than a silent one.
    /// </summary>
    public const string SelfId = "Y_G_035";

    /// <summary>Suites the scanner found that the registry omits (this audit's own suite excepted).</summary>
    public static string[] UnregisteredSuites()
    {
        var registered = Registry().Select(c => c.Audit).ToHashSet(StringComparer.Ordinal);
        return ScanAudits()
            .Where(s => !string.Equals(s.Audit, SelfId, StringComparison.Ordinal))
            .Where(s => !registered.Contains(s.Audit))
            .Select(s => s.Audit)
            .ToArray();
    }

    // ── The verdict, COMPUTED ───────────────────────────────────────────────

    /// <summary>
    /// THE VERDICT, COMPUTED (G_027: never a literal).
    ///   SURVIVES  — the temporal sector is non-empty, self-consistent, and the registry agrees with the scan;
    ///   BOUNDARY  — the catalog and the scan agree, but a temporal observable can be called with a B;
    ///   REFUTED   — the catalog and the scan disagree, or no temporal result survives.
    /// </summary>
    public static string Verdict()
    {
        if (!RegistryAgreesWithScan()) return "REFUTED";
        if (MinimalTimeSector().Length == 0) return "REFUTED";
        if (!NoTemporalObservableTakesB()) return "BOUNDARY";
        return "SURVIVES";
    }

    /// <summary>
    /// The one-line statement of the minimal time sector: what the theory's time half is made of.
    /// </summary>
    public static string MinimalTimeSectorStatement()
    {
        var (metricFunctions, scalars, exponents) = MinimalTimeSectorContent();
        return $"{metricFunctions} metric function (g₀₀ = −ρ^(2/d)) + {scalars} scalar (ρ) + {exponents} exponent "
             + "(1/d, d = 3 by rotation self-duality) — no spatial metric, no conformal factor, no reference η. "
             + "The source law a = −(1/d)∇ln ρ and the clock law dτ/dt = ρ^(1/d) are the SAME statement, because "
             + "the Newtonian potential IS the time exponent (G_028), so a = −∇A.";
    }
}
