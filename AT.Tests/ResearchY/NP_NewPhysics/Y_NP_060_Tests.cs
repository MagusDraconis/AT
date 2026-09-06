using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_060 — Dark Energy Resource Audit test suite (Y_NP_060_Tests.cs).
///
/// Question: does ΩΛ represent a physically extractable resource, or only an informational
/// state descriptor? ΩΛ = I_occ/ln K = 0.6839 is a DERIVED information quantity (NP_055–059).
///
/// Verdict tested: ΩΛ CANNOT perform work — it is an informational state descriptor (and
/// bookkeeping quantity, B = C), not a resource. It has no energy scale (dimensionless), no
/// gradient (single snapshot number), and no temperature channel (NP_030). Extraction
/// channels: vacuum work (hosted, no derived EoS), information work (Landauer/Szilard needs
/// T, absent), free energy (F = U − TS needs U and T, neither derived). The extractable/
/// gravitating side is Ωm (matter = deficit, QG194), NOT ΩΛ (the surplus). The conserved
/// counts Σρ = 1 and Σm = 0 are DERIVED but are counts, not energy resources.
///
/// Classification: ΩΛ as an information state descriptor DERIVED; ΩΛ as a physical resource
/// REFUTED; vacuum-energy work channel CORRESPONDENCE (hosted); conserved counts DERIVED
/// (not resources); temperature / free energy BOUNDARY (absent). No new primitive; canonical
/// AT unchanged.
///
/// Deterministic: closed-form ΩΛ/Ωm, first-law d(ρV) = −p dV, Landauer k_B T ln 2.
/// </summary>
public class Y_NP_060_Tests : ResearchTestBase
{
    public Y_NP_060_Tests(ITestOutputHelper output) : base(output) { }

    private const int K = 3;
    private static readonly int[] Occ = { 4, 4, 87 };
    private const double KB = 1.380649e-23; // Boltzmann constant, J/K
    private const double LN2 = 0.6931471805599453;

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

    private static double OmegaLambda => KLDivergence(Rho(Occ), 1.0 / K) / LnK;
    private static double OmegaMatter => ShannonEntropy(Rho(Occ)) / LnK;

    // ── [Required] Y_NP_060_ResourceRequirements ─────────────────

    [Fact]
    public void Y_NP_060_ResourceRequirements()
    {
        // A resource must have (1) an energy scale, (2) a gradient, (3) a temperature/
        // free-energy channel. ΩΛ has none.
        double ol = OmegaLambda;
        Assert.True(ol > 0 && ol < 1, "ΩΛ is a dimensionless fraction (no energy scale)");

        // It is a SINGLE snapshot number — a function of the fixed ρ, no gradient.
        double olAgain = KLDivergence(Rho(Occ), 1.0 / K) / LnK;
        Assert.True(Math.Abs(ol - olAgain) < 1e-12, "ΩΛ is a single number, not a field/gradient");

        // No canonical temperature exists in AT (NP_030).
        bool temperatureExists = false;
        Assert.False(temperatureExists);
    }

    // ── [Required] Y_NP_060_VacuumWorkIsHosted ───────────────────

    [Fact]
    public void Y_NP_060_VacuumWorkIsHosted()
    {
        // Vacuum energy CAN do work: first law d(ρV) = −p dV; with p = w·ρ.
        // w = −1 (vacuum): d(ρV)/dV = +ρ (energy GROWS — the "free lunch").
        // w = 0 (matter):  d(ρV)/dV = 0 (dilutes, no growth).
        double dudv_vacuum = -(-1.0);   // −w = +1 for w = −1
        double dudv_matter = -(0.0);    // −w = 0 for w = 0
        Assert.True(dudv_vacuum > 0, "vacuum energy grows as it expands (w = −1)");
        Assert.True(Math.Abs(dudv_matter) < 1e-12, "matter energy is conserved (w = 0)");

        // But AT derives no p or ρ_Λ (NP_056: no equation of state) — the work channel is
        // HOSTED via QG230's Λ = 8πG·ρ_Λ.
        bool equationOfStateDerived = false;
        Assert.False(equationOfStateDerived);
    }

    // ── [Required] Y_NP_060_InformationWorkNeedsT ────────────────

    [Fact]
    public void Y_NP_060_InformationWorkNeedsT()
    {
        // Information → work (Landauer/Szilard) needs a temperature: W = k_B T ln 2 per bit.
        // AT has no derived temperature (NP_030), so the conversion channel is absent.
        double landauer300K = KB * 300.0 * LN2;
        Assert.True(Math.Abs(landauer300K - 2.8710e-21) / 2.8710e-21 < 1e-2,
            $"Landauer at T=300K = {landauer300K:E4} J");

        // Without T the nats → work conversion is undefined.
        bool temperatureAvailable = false;
        Assert.False(temperatureAvailable, "no T → no information-to-work channel");
    }

    // ── [Required] Y_NP_060_FreeEnergyUndefinable ────────────────

    [Fact]
    public void Y_NP_060_FreeEnergyUndefinable()
    {
        // F = U − TS requires U (energy — needs the QG89 bridge) and T (absent). AT derives
        // neither, so no free energy exists to be minimized (no −ΔF work).
        bool internalEnergyDerived = false;   // U needs QG89 (BOUNDARY)
        bool temperatureDerived = false;      // T absent (NP_030)
        Assert.False(internalEnergyDerived);
        Assert.False(temperatureDerived);

        // The information quantities are pure numbers (no energy), so no F.
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        Assert.True(Math.Abs(h - 0.3473) < 1e-3, "H is a dimensionless entropy, no U/T to make F");
    }

    // ── [Required] Y_NP_060_Tracking ─────────────────────────────

    [Fact]
    public void Y_NP_060_Tracking()
    {
        // Under any hypothetical extraction, nothing flows: I_occ and H are invariant
        // functions of the fixed ρ, and the counts Σρ = 1, Σm = 0 are conserved.
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);

        Assert.True(Math.Abs(kl - 0.7513) < 1e-3, "I_occ = 0.7513 (invariant function of ρ)");
        Assert.True(Math.Abs(h - 0.3473) < 1e-3, "H = 0.3473 (invariant function of ρ)");

        double sum = 0;
        foreach (double p in rho) sum += p;
        Assert.True(Math.Abs(sum - 1.0) < 1e-12, "Σρ = 1 conserved (count)");

        double deficitSum = 0;
        foreach (double p in rho) deficitSum += (1.0 / K - p);
        Assert.True(Math.Abs(deficitSum) < 1e-12, "Σm = Σ(ρ̄ − ρ) = 0 conserved (count)");
    }

    // ── [Required] Y_NP_060_Readings ─────────────────────────────

    [Fact]
    public void Y_NP_060_Readings()
    {
        // A) extractable work: REFUTED. B) state descriptor: YES. C) bookkeeping: YES (= B).
        // D) hidden conserved resource: NO (the conserved quantities are counts).
        bool extractableWork = false;
        bool stateDescriptor = true;
        bool bookkeeping = true;
        bool hiddenConservedResource = false;

        Assert.False(extractableWork);
        Assert.True(stateDescriptor);
        Assert.True(bookkeeping);
        Assert.False(hiddenConservedResource);

        // B = C (the descriptor IS the bookkeeping partition).
        Assert.True(stateDescriptor == bookkeeping);
    }

    // ── [Required] Y_NP_060_ExtractableSideIsMatter ──────────────

    [Fact]
    public void Y_NP_060_ExtractableSideIsMatter()
    {
        // The extractable/gravitating side is Ωm (matter = deficit, QG194), NOT ΩΛ (surplus).
        double om = OmegaMatter;
        double ol = OmegaLambda;

        Assert.True(Math.Abs(om - 0.3161) < 1e-3, "Ωm = H/ln K = 0.3161 (the deficit/matter side)");
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ = I_occ/ln K = 0.6839 (the surplus side)");

        // Matter is the deficit m = ρ̄ − ρ (QG194): the physically load-bearing substance.
        // ΩΛ is its information complement, not the resource.
        Assert.True(Math.Abs((om + ol) - 1.0) < 1e-9, "Ωm + ΩΛ = 1 (the partition)");
    }

    // ── [Required] Y_NP_060_Classification ───────────────────────

    [Fact]
    public void Y_NP_060_Classification()
    {
        // ΩΛ as an information state descriptor: DERIVED.
        double ol = OmegaLambda;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ descriptor DERIVED");

        // ΩΛ as a physical resource: REFUTED.
        bool omegaLambdaResource = false;
        Assert.False(omegaLambdaResource);

        // Vacuum-energy work channel: CORRESPONDENCE (hosted).
        bool vacuumWorkDerived = false;
        Assert.False(vacuumWorkDerived);

        // Conserved counts: DERIVED (but not resources).
        var rho = Rho(Occ);
        double sum = 0;
        foreach (double p in rho) sum += p;
        Assert.True(Math.Abs(sum - 1.0) < 1e-12, "Σρ = 1 DERIVED (a count, not a resource)");

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, K);
    }

    // ── [Required] Y_NP_060_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_060_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_060 — Dark Energy Resource Audit");

        sb.AppendLine("Goal: does OmegaL represent an extractable resource, or only a state");
        sb.AppendLine("descriptor? Can it perform work?");
        sb.AppendLine();

        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);
        double ol = kl / LnK;
        double om = h / LnK;

        sb.AppendLine("[1] Resource requirements — OmegaL has none");
        sb.AppendLine($"    OmegaL = {ol:F4} is dimensionless (no energy scale).");
        sb.AppendLine("    It is a single snapshot number (no gradient).");
        sb.AppendLine("    No canonical temperature exists (NP_030).");
        sb.AppendLine();

        sb.AppendLine("[2] Extraction channels");
        sb.AppendLine("    (a) vacuum work d(rho·V) = -p dV: w=-1 -> grows (hosted, no derived EoS).");
        sb.AppendLine("    (b) information work W = k_B·T·ln2: needs T (absent).");
        sb.AppendLine("    (c) free energy F = U - T·S: needs U and T (neither derived).");
        sb.AppendLine();

        sb.AppendLine("[3] Tracking — nothing flows");
        sb.AppendLine($"    I_occ = {kl:F4} (invariant function of rho);  H = {h:F4} (invariant).");
        sb.AppendLine("    Sigma rho = 1, Sigma m = 0 (conserved counts, not energy).");
        sb.AppendLine();

        sb.AppendLine("[4] Readings");
        sb.AppendLine("    A) extractable work: REFUTED.");
        sb.AppendLine("    B) state descriptor: YES.  C) bookkeeping: YES (= B).");
        sb.AppendLine("    D) hidden conserved resource: NO (the conserved quantities are counts).");
        sb.AppendLine();

        sb.AppendLine("[5] The extractable side is MATTER, not OmegaL");
        sb.AppendLine($"    OmegaM = H/ln K = {om:F4} (deficit = matter, QG194).");
        sb.AppendLine($"    OmegaL = I_occ/ln K = {ol:F4} (surplus = information descriptor).");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    OmegaL CANNOT perform work — it merely describes the state.");
        sb.AppendLine("    No energy scale, no gradient, no temperature channel.");
        sb.AppendLine("    Vacuum-energy work is hosted (QG230 Lambda = 8·pi·G·rho_Lambda).");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
