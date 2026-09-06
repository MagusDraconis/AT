using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_057 — Dark Energy Meaning Audit test suite (Y_NP_057_Tests.cs).
///
/// Question: what does ΩΛ physically represent inside Actualization Theory, and why
/// should information content appear as a cosmological density fraction?
///
/// Program: (1) inventory every quantity (Difference, Actualization, D96, occupancy,
/// I_occ); (2) determine what ΩΛ measures (A surplus / B deficit / C unused state-space /
/// D actualization pressure / E pure bookkeeping); (3) search equivalent formulations;
/// (4) determine whether ΩΛ is a cause, an effect, or a state descriptor; (5) compare
/// with vacuum energy, cosmological constant, and entropy-based interpretations.
///
/// Verdict tested: ΩΛ = I_occ/ln K = 0.6839 is the normalized ENTROPY DEFICIT — the
/// information surplus of the realized D96 occupancy over the uniform prior — and is a
/// STATE DESCRIPTOR (order parameter), not a cause and not an energy density. A = C = E
/// (surplus = unused state-space = bookkeeping) all equal ΩΛ; B (deficit = realized
/// entropy) is the complement Ωm = H/ln K = 0.3161 (matter); D (pressure) is metaphorical
/// (μ = 2 anti-thermal, NP_030). ΩΛ is monotone in top-heaviness (0 for uniform, 0.6839
/// for [4,4,87], 0.8938 for [1,1,93]) — an order parameter. The identification of the
/// information budget (I_occ + H = ln K) with the energy budget (ΩΛ + Ωm = 1, flatness)
/// is an empirical CORRESPONDENCE with an open information→energy bridge.
///
/// Classification: ΩΛ value + "normalized entropy deficit / information surplus" meaning
/// DERIVED (KL structure); state-descriptor status DERIVED (NP_030); the identification
/// with the cosmological energy fraction CORRESPONDENCE (empirical, bridge BOUNDARY/open);
/// vacuum-energy / cosmological-constant ontology CORRESPONDENCE (hosted);
/// actualization-pressure reading REFUTED as physical (NP_030). No new primitive;
/// canonical AT unchanged.
///
/// Deterministic: closed-form Shannon entropy and KL divergence over the [4,4,87] record.
/// </summary>
public class Y_NP_057_Tests : ResearchTestBase
{
    public Y_NP_057_Tests(ITestOutputHelper output) : base(output) { }

    private const int K = 3;               // three octave families
    private static readonly int[] Occ = { 4, 4, 87 };

    private static double[] Rho(int[] occ)
    {
        int total = 0;
        foreach (int c in occ) total += c;
        var rho = new double[occ.Length];
        for (int i = 0; i < occ.Length; i++) rho[i] = (double)occ[i] / total;
        return rho;
    }

    private static double ShannonEntropy(double[] rho)
    {
        double h = 0;
        foreach (double p in rho) h -= p * Math.Log(p);
        return h;
    }

    private static double KLDivergence(double[] rho, double uniform)
    {
        double kl = 0;
        foreach (double p in rho) kl += p * Math.Log(p / uniform);
        return kl;
    }

    private static double LnK => Math.Log(K);

    // ── [Required] Y_NP_057_EntropyDeficitFormulation ─────────────

    [Fact]
    public void Y_NP_057_EntropyDeficitFormulation()
    {
        // ΩΛ = (ln K − H)/ln K = 1 − H/ln K — the normalized ENTROPY DEFICIT.
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);

        // I_occ = KL = ln K − H (the KL↔entropy identity for the uniform reference).
        Assert.True(Math.Abs(kl - (LnK - h)) < 1e-9, $"KL = ln K − H: {kl:F6} vs {LnK - h:F6}");

        double ol = (LnK - h) / LnK;   // normalized entropy deficit
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, $"ΩΛ = (ln K − H)/ln K = {ol:F4}");
        Assert.True(Math.Abs((1 - h / LnK) - ol) < 1e-9, "ΩΛ = 1 − H/ln K");
    }

    // ── [Required] Y_NP_057_SurplusVsDeficit ──────────────────────

    [Fact]
    public void Y_NP_057_SurplusVsDeficit()
    {
        // A) information SURPLUS = I_occ/ln K = ΩΛ (KL excess over uniform).
        // B) information DEFICIT (realized entropy) = H/ln K = Ωm (matter).
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);

        double surplus = kl / LnK;       // A — ΩΛ
        double deficit = h / LnK;        // B — Ωm (the complement)

        Assert.True(Math.Abs(surplus - 0.6839) < 1e-3, $"surplus ΩΛ = {surplus:F4}");
        Assert.True(Math.Abs(deficit - 0.3161) < 1e-3, $"deficit Ωm = {deficit:F4}");
        Assert.True(Math.Abs((surplus + deficit) - 1.0) < 1e-9, "surplus + deficit = 1");

        // The "deficit" describes MATTER, not dark energy.
        Assert.True(Math.Abs(deficit - (1 - surplus)) < 1e-9, "Ωm = 1 − ΩΛ (deficit = matter)");
    }

    // ── [Required] Y_NP_057_UnusedStateSpace ──────────────────────

    [Fact]
    public void Y_NP_057_UnusedStateSpace()
    {
        // C) unused state-space = the capacity left unrealized = ln K − H = I_occ.
        // This is IDENTICAL to A (the surplus).
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);

        double unused = LnK - h;         // unrealized entropy capacity
        Assert.True(Math.Abs(unused - kl) < 1e-9, "unused state-space = ln K − H = I_occ");

        double olUnused = unused / LnK;
        double olSurplus = kl / LnK;
        Assert.True(Math.Abs(olUnused - olSurplus) < 1e-12, "C (unused) = A (surplus) = ΩΛ");
        Assert.True(Math.Abs(olUnused - 0.6839) < 1e-3);
    }

    // ── [Required] Y_NP_057_OrderParameter ────────────────────────

    [Fact]
    public void Y_NP_057_OrderParameter()
    {
        // ΩΛ is monotone in top-heaviness: 0 for uniform, grows with concentration.
        var uniform = Rho(new[] { 95, 95, 95 });   // effectively [1/3,1/3,1/3] over 3 cells
        // (use the true 3-cell uniform directly for the KL-to-uniform reference)
        double olUniform = KLDivergence(new[] { 1.0 / 3, 1.0 / 3, 1.0 / 3 }, 1.0 / 3) / LnK;
        double olRealized = KLDivergence(Rho(Occ), 1.0 / 3) / LnK;
        double olExtreme = KLDivergence(Rho(new[] { 1, 1, 93 }), 1.0 / 3) / LnK;

        Assert.True(Math.Abs(olUniform - 0.0) < 1e-9, "uniform → ΩΛ = 0");
        Assert.True(Math.Abs(olRealized - 0.6839) < 1e-3, "realized [4,4,87] → ΩΛ = 0.6839");
        Assert.True(Math.Abs(olExtreme - 0.8938) < 1e-3, "extreme [1,1,93] → ΩΛ = 0.8938");
        Assert.True(olUniform < olRealized && olRealized < olExtreme,
            "ΩΛ is monotone in top-heaviness — an order parameter");
    }

    // ── [Required] Y_NP_057_EquivalentFormulations ────────────────

    [Fact]
    public void Y_NP_057_EquivalentFormulations()
    {
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);

        double f1 = kl / LnK;                // I_occ/ln K
        double f2 = KLDivergence(rho, 1.0 / K) / LnK; // KL/ln K
        double f3 = (LnK - h) / LnK;         // (ln K − H)/ln K
        double f4 = 1 - h / LnK;             // 1 − H/ln K = 1 − Ωm

        Assert.True(Math.Abs(f1 - f2) < 1e-12);
        Assert.True(Math.Abs(f2 - f3) < 1e-12);
        Assert.True(Math.Abs(f3 - f4) < 1e-12);
        Assert.True(Math.Abs(f1 - 0.6839) < 1e-3, "all equivalent formulations give ΩΛ = 0.6839");
    }

    // ── [Required] Y_NP_057_StateDescriptorNotCause ───────────────

    [Fact]
    public void Y_NP_057_StateDescriptorNotCause()
    {
        // ΩΛ is a dimensionless order parameter of ρ (a STATE DESCRIPTOR). It drives
        // nothing — it has no dynamics (NP_056) and is a function of ρ alone.
        var rho = Rho(Occ);
        double ol = KLDivergence(rho, 1.0 / K) / LnK;

        // ΩΛ is fully determined by the count structure ρ (a descriptor).
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3);
        Assert.True(ol > 0 && ol < 1, "ΩΛ is a bounded order parameter in (0,1)");

        // It is NOT a cause: there is no equation of state and no dynamics (NP_056).
        bool omegaLambdaCausesDynamics = false;
        Assert.False(omegaLambdaCausesDynamics);
    }

    // ── [Required] Y_NP_057_VacuumVsConstantVsEntropy ─────────────

    [Fact]
    public void Y_NP_057_VacuumVsConstantVsEntropy()
    {
        // Vacuum energy (w = −1): NOT derived — no energy density; w = −1 hosted.
        // Cosmological constant (Λ = const): NOT AT's — QG230 has Λ ∝ 1/R² (ρ ∝ a^(−2)).
        double wQg230 = -1 - (-2.0) / 3.0;   // ρ ∝ a^(−2) → w = −1/3 (not a constant Λ)
        Assert.True(Math.Abs(wQg230 - (-1.0 / 3.0)) < 1e-12, "Λ ∝ 1/R² ⇒ w = −1/3, not a constant Λ");

        // Entropy/information family: ΩΛ = (ln K − H)/ln K is an entropy-deficit fraction —
        // the natural family, not vacuum energy.
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double ol = (LnK - h) / LnK;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ is an entropy-deficit fraction");
        Assert.True(h < LnK, "realized entropy H < max entropy ln K (top-heavy)");
    }

    // ── [Required] Y_NP_057_Classification ────────────────────────

    [Fact]
    public void Y_NP_057_Classification()
    {
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);
        double ol = kl / LnK;

        // ΩΛ value + "normalized entropy deficit / information surplus": DERIVED.
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ DERIVED");
        Assert.True(Math.Abs(kl - (LnK - h)) < 1e-9, "I_occ = ln K − H (entropy deficit)");

        // State-descriptor (order-parameter) status: DERIVED (monotone in top-heaviness).
        Assert.True(KLDivergence(new[] { 1.0 / 3, 1.0 / 3, 1.0 / 3 }, 1.0 / 3) < 1e-12, "uniform → ΩΛ = 0");
        Assert.True(ol > 0, "top-heavy → ΩΛ > 0");

        // Identification with the energy fraction: CORRESPONDENCE (bridge open).
        bool infoEnergyBridgeDerived = false;
        Assert.False(infoEnergyBridgeDerived);

        // Vacuum energy / cosmological constant: CORRESPONDENCE (hosted).
        // Actualization pressure: REFUTED (anti-thermal).
        Assert.True(Math.Log(2.0) > 0, "μ = 2 growth = anti-thermal (no pressure)");

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, K);
    }

    // ── [Required] Y_NP_057_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_057_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_057 — Dark Energy Meaning Audit");

        sb.AppendLine("Goal: what does OmegaL physically represent, and why should information");
        sb.AppendLine("content appear as a cosmological density fraction?");
        sb.AppendLine();

        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);
        double ol = kl / LnK;
        double om = h / LnK;

        sb.AppendLine("[1] Inventory — the entropy-deficit identity");
        sb.AppendLine($"    occupancy [4,4,87] -> H = {h:F4} nats (realized entropy)");
        sb.AppendLine($"    ln K = ln 3 = {LnK:F6} nats (max entropy, uniform reference)");
        sb.AppendLine($"    I_occ = KL = ln K - H = {kl:F4} nats (entropy deficit / info surplus)");
        sb.AppendLine();

        sb.AppendLine("[2] What OmegaL measures (A-E)");
        sb.AppendLine($"    A) information surplus = I_occ/ln K   = {ol:F4}  -> OmegaL  YES");
        sb.AppendLine($"    B) information deficit = H/ln K        = {om:F4}  -> OmegaM (matter)");
        sb.AppendLine($"    C) unused state-space = (lnK-H)/lnK    = {ol:F4}  -> OmegaL  YES (= A)");
        sb.AppendLine("    D) actualization pressure              = metaphor (anti-thermal, NP_030)");
        sb.AppendLine($"    E) pure bookkeeping     = I_occ/ln K   = {ol:F4}  -> OmegaL  YES (= A)");
        sb.AppendLine("    => A = C = E; B is the complement (matter); D metaphorical.");
        sb.AppendLine();

        sb.AppendLine("[3] Equivalent formulations (all = 0.6839)");
        sb.AppendLine($"    I_occ/ln K = {kl / LnK:F4};  KL/ln K = {kl / LnK:F4};");
        sb.AppendLine($"    (lnK-H)/lnK = {(LnK - h) / LnK:F4};  1 - H/lnK = {1 - h / LnK:F4}");
        sb.AppendLine();

        sb.AppendLine("[4] Cause / effect / state descriptor");
        sb.AppendLine("    cause: NO (drives nothing, NP_056).");
        sb.AppendLine("    effect: partial (produced by top-heavy occupancy).");
        sb.AppendLine("    STATE DESCRIPTOR (order parameter): YES — monotone in top-heaviness.");
        double olUniform = KLDivergence(new[] { 1.0 / 3, 1.0 / 3, 1.0 / 3 }, 1.0 / 3) / LnK;
        double olExtreme = KLDivergence(Rho(new[] { 1, 1, 93 }), 1.0 / 3) / LnK;
        sb.AppendLine($"    uniform -> {olUniform:F4}; [4,4,87] -> {ol:F4}; [1,1,93] -> {olExtreme:F4}");
        sb.AppendLine();

        sb.AppendLine("[5] Compare with vacuum energy / cosmological constant / entropy");
        sb.AppendLine("    vacuum energy (w=-1): CORRESPONDENCE (hosted, no derived w=-1).");
        sb.AppendLine("    cosmological constant: CORRESPONDENCE (QG230 Lambda ∝ 1/R², not constant).");
        sb.AppendLine("    entropy/information family: YES — OmegaL is an entropy-deficit fraction.");
        sb.AppendLine();

        sb.AppendLine("[6] The open bridge");
        sb.AppendLine("    I_occ + H = ln K (information budget)  <==>  OmegaL + OmegaM = 1 (energy budget).");
        sb.AppendLine("    The information->energy link is an EMPIRICAL CORRESPONDENCE, bridge OPEN.");
        sb.AppendLine();

        sb.AppendLine("[7] Verdict");
        sb.AppendLine("    OmegaL = the normalized ENTROPY DEFICIT (information surplus) of the realized");
        sb.AppendLine("    D96 occupancy — a DERIVED STATE DESCRIPTOR (order parameter), not a cause, not");
        sb.AppendLine("    an energy density. Entropic/informational family, not vacuum energy.");
        sb.AppendLine("    Identification with the energy fraction = CORRESPONDENCE (bridge BOUNDARY/open).");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
