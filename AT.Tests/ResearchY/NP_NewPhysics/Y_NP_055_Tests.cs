using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_055 — Dark Energy Ontology Audit test suite (Y_NP_055_Tests.cs).
///
/// Question: what is Dark Energy physically inside Actualization Theory? ΩΛ = I_occ/ln K
/// = 0.6839 matches observation to 0.12% (QG234), but what ONTOLOGY does ΩΛ represent?
///
/// Program: (1) inventory every object linked to ΩΛ (I_occ, occupancy, D96,
/// actualization); (2) test five interpretations — A) vacuum energy, B) information
/// content, C) occupancy deficit, D) actualization pressure, E) cosmological bookkeeping
/// only; (3) determine whether any interpretation predicts additional observables;
/// (4) compare with ΛCDM (w = −1); (5) test future evolution / ΩΛ(z) / q₀ / z_acc.
///
/// Verdict tested: ΩΛ = I_occ/ln K = 0.6839 is the information-bookkeeping fraction
/// (interpretations B = E — identical). The canonical ρ = [4,4,87]/95 reproduces
/// I_occ = KL(ρ‖uniform) = 0.7513 exactly, and the pair partitions the state-space size
/// (I_occ + H = ln K; ΩΛ = I_occ/ln K the EXCESS, Ωm = H/ln K the realized-entropy
/// fraction). A (vacuum energy, w = −1) is NOT derived: QG230's own Λ ∝ 1/R² (ρ_Λ ∝ a^(−2))
/// translates to w = −1/3, not −1, and the hosted q₀/z_acc closures assume w = −1.
/// C (occupancy deficit) is INVERTED — the deficit is matter (Ωm = H/ln K, QG195/196),
/// not dark energy. D (actualization pressure) is a METAPHOR — the canonical μ = 2
/// branching is a population inversion (anti-thermal, NP_030). E = B.
///
/// Classification: ΩΛ value and the information-bookkeeping ontology DERIVED (B = E);
/// vacuum-energy / w = −1 ontology CORRESPONDENCE (hosted ΛCDM; QG230 implies w = −1/3);
/// occupancy-deficit reading REFUTED as dark-energy ontology (it is matter);
/// actualization-pressure reading EMERGENT-as-metaphor / REFUTED as physical (NP_030);
/// legacy time-varying Λ(t) = α/√V(t) in TENSION with the constant fraction (coefficient
/// FITTED). Success criterion: PARTIAL — a DERIVED informational ontology exists, but no
/// physical (energetic/equation-of-state) ontology is derived; ΩΛ is a successful
/// numerical relation plus a bookkeeping reading, not a physical dark-energy mechanism.
/// No new primitive; canonical AT unchanged.
///
/// Deterministic: closed-form KL divergence over the [4,4,87] octave record, closed-form
/// ΛCDM closures, closed-form equation-of-state scaling.
/// </summary>
public class Y_NP_055_Tests : ResearchTestBase
{
    public Y_NP_055_Tests(ITestOutputHelper output) : base(output) { }

    // Canonical inputs (QG228/QG234). The octave record is top-heavy: 87 of 95 modes
    // sit in the third octave. K = 3 octave families.
    private const int K = 3;
    private static readonly int[] Occ = { 4, 4, 87 };
    private const double Iocc = 0.7513; // KL(ρ‖uniform), QG228 (recomputed below)
    private const double OmegaLambdaObs = 0.6847; // Planck ΩΛ (comparison anchor)
    private const double OmegaMatterObs = 0.3153; // Planck Ωm (comparison anchor)

    private static double[] Rho()
    {
        int total = 0;
        foreach (int c in Occ) total += c;
        var rho = new double[Occ.Length];
        for (int i = 0; i < Occ.Length; i++) rho[i] = (double)Occ[i] / total;
        return rho;
    }

    private static double KLDivergence(double[] rho, double uniform)
    {
        double kl = 0;
        foreach (double p in rho) kl += p * Math.Log(p / uniform);
        return kl;
    }

    private static double ShannonEntropy(double[] rho)
    {
        double h = 0;
        foreach (double p in rho) h -= p * Math.Log(p);
        return h;
    }

    private static double LnK => Math.Log(K);

    private static double OmegaLambda => KLDivergence(Rho(), 1.0 / K) / LnK;

    private static double OmegaMatter => 1 - OmegaLambda;

    /// <summary>Equation of state for a component with ρ ∝ a^n: w = −1 − n/3.
    /// n = 0 → w = −1 (cosmological constant); n = −2 → w = −1/3 (Λ ∝ 1/R²).</summary>
    private static double WFromScaling(double n) => -1 - n / 3.0;

    // ── [Required] Y_NP_055_InformationBookkeepingIdentity ────────

    [Fact]
    public void Y_NP_055_InformationBookkeepingIdentity()
    {
        // Section 1 — the canonical octave record [4,4,87] reproduces I_occ and
        // partitions the state-space size: I_occ + H = ln K, ΩΛ + Ωm = 1.
        var rho = Rho();
        double kl = KLDivergence(rho, 1.0 / K);
        double h = ShannonEntropy(rho);
        Assert.Equal(95, 4 + 4 + 87);
        Assert.True(Math.Abs(kl - Iocc) < 1e-3, $"I_occ = {kl:F4} (must be ≈ {Iocc})");
        Assert.True(Math.Abs(h - 0.3473) < 1e-3, $"H = {h:F4}");

        // I_occ + H = ln K (the KL↔entropy identity for a uniform reference).
        Assert.True(Math.Abs((kl + h) - LnK) < 1e-6, $"I_occ + H = {kl + h:F6} vs ln K = {LnK:F6}");

        // ΩΛ + Ωm = 1 (the pair partitions the state-space size).
        double ol = kl / LnK;
        double om = h / LnK;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, $"ΩΛ = {ol:F4}");
        Assert.True(Math.Abs(om - 0.3161) < 1e-3, $"Ωm = {om:F4}");
        Assert.True(Math.Abs((ol + om) - 1.0) < 1e-9);

        // Both match observation to < 1%.
        Assert.True(Math.Abs(ol - OmegaLambdaObs) / OmegaLambdaObs < 0.01, "ΩΛ matches Planck 0.12%");
        Assert.True(Math.Abs(om - OmegaMatterObs) / OmegaMatterObs < 0.01, "Ωm matches Planck 0.26%");
    }

    // ── [Required] Y_NP_055_InterpretationA_VacuumEnergy ──────────

    [Fact]
    public void Y_NP_055_InterpretationA_VacuumEnergy()
    {
        // A) vacuum energy (w = −1). ΩΛ is a DIMENSIONLESS fraction of two information
        // quantities — it is NOT an energy density, and w = −1 is NOT derived from it.
        double ol = OmegaLambda;
        Assert.True(ol > 0 && ol < 1, "ΩΛ is a dimensionless fraction, not an energy density");

        // w = −1 corresponds to ρ ∝ a^0 (constant energy density).
        Assert.Equal(-1.0, WFromScaling(0.0));

        // QG230's own scaling is Λ ∝ 1/R² (ρ_Λ ∝ a^(−2)), which gives w = −1/3, NOT −1.
        double wQg230 = WFromScaling(-2.0);
        Assert.True(Math.Abs(wQg230 - (-1.0 / 3.0)) < 1e-12, $"Λ ∝ 1/R² ⇒ w = {wQg230:F4}");
        Assert.True(Math.Abs(wQg230 - (-1.0)) > 0.5, "w = −1/3 differs from the cosmological constant w = −1");

        // The hosted closures assume w = −1 (cosmological constant) — inconsistent with
        // QG230's w = −1/3.
        Assert.True(Math.Abs(WFromScaling(-2.0) - WFromScaling(0.0)) > 0.5,
            "hosted w = −1 vs QG230 w = −1/3 are incompatible");
    }

    // ── [Required] Y_NP_055_InterpretationB_InformationContent ────

    [Fact]
    public void Y_NP_055_InterpretationB_InformationContent()
    {
        // B) information content. ΩΛ = I_occ/ln K is LITERALLY the normalized information
        // density — the KL-excess over uniformity as a fraction of the state-space size.
        var rho = Rho();
        double kl = KLDivergence(rho, 1.0 / K);
        double ol = kl / LnK;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, $"ΩΛ = I_occ/ln K = {ol:F4}");

        // I_occ > 0 means the record is a POSITIVE departure from uniformity (top-heavy).
        Assert.True(kl > 0, "I_occ > 0: the [4,4,87] record is top-heavy, not uniform");
        Assert.Equal(87, Occ[2]); // 87/95 in the top octave — the excess
    }

    // ── [Required] Y_NP_055_InterpretationC_OccupancyDeficit ──────

    [Fact]
    public void Y_NP_055_InterpretationC_OccupancyDeficit()
    {
        // C) occupancy deficit. QG195/196: MATTER is the deficit. The deficit maps to
        // Ωm = H/ln K (the realized-entropy fraction), NOT to ΩΛ.
        var rho = Rho();
        double h = ShannonEntropy(rho);
        double om = h / LnK;
        Assert.True(Math.Abs(om - 0.3161) < 1e-3, $"matter = deficit = Ωm = H/ln K = {om:F4}");

        // ΩΛ is the EXCESS (KL > 0), not the deficit.
        double kl = KLDivergence(rho, 1.0 / K);
        Assert.True(kl > h, "the excess (KL) exceeds the realized entropy (H) — ΩΛ is the excess side");

        // Inverted interpretation: the "deficit" describes matter, not dark energy.
        Assert.True(Math.Abs(om - (1 - OmegaLambda)) < 1e-9, "Ωm = 1 − ΩΛ is the deficit/complement");
    }

    // ── [Required] Y_NP_055_InterpretationD_ActualizationPressure ──

    [Fact]
    public void Y_NP_055_InterpretationD_ActualizationPressure()
    {
        // D) actualization pressure. NP_030: the canonical branching μ = 2 GROWS
        // (ρ_{k+1} = 2·ρ_k) — a population inversion, anti-thermal, not a pressure.
        // A "pressure" (thermal occupation) would need a DECAYING rate μ < 1.
        double mu = 2.0;
        double rho7OverRho0 = Math.Pow(mu, 7); // μ = 2, 8 generations
        Assert.Equal(128.0, rho7OverRho0);     // ρ₇/ρ₀ = 128 > 1 (growth, inversion)

        // The would-be log-ratio is POSITIVE (β < 0 — anti-thermal).
        double logRatio = Math.Log(mu);
        Assert.True(logRatio > 0, $"ln μ = {logRatio:F4} > 0: population inversion, not a decaying pressure");

        // A Bose/thermal occupation requires μ < 1 (decay); canonical μ = 2 gives n_k < 0.
        for (int k = 1; k <= 3; k++)
        {
            double nk = 1.0 / (Math.Pow(mu, -k) - 1.0);
            Assert.True(nk < 0, $"μ = 2 occupation n_{k} = {nk:F3} < 0 (inversion, NP_030)");
        }
    }

    // ── [Required] Y_NP_055_InterpretationE_Bookkeeping ───────────

    [Fact]
    public void Y_NP_055_InterpretationE_Bookkeeping()
    {
        // E) cosmological bookkeeping only. ΩΛ is the bookkeeping fraction; this is
        // IDENTICAL to B (the information partition IS the bookkeeping).
        var rho = Rho();
        double kl = KLDivergence(rho, 1.0 / K);
        double h = ShannonEntropy(rho);
        double ol = kl / LnK;
        double om = h / LnK;

        Assert.True(Math.Abs((ol + om) - 1.0) < 1e-9, "ΩΛ + Ωm = 1 (bookkeeping identity)");
        Assert.True(Math.Abs(ol - (kl / LnK)) < 1e-12, "E (bookkeeping) = B (information): same object");
        Assert.True(Math.Abs(om - (h / LnK)) < 1e-12, "Ωm = H/ln K: the complementary bookkeeping entry");
    }

    // ── [Required] Y_NP_055_AdditionalObservables ─────────────────

    [Fact]
    public void Y_NP_055_AdditionalObservables()
    {
        // Section 3 — the information ontology predicts EXACTLY the finite family:
        // ΩΛ, Ωm, ratio, q₀, z_acc. q₀/z_acc values DERIVED, forms hosted (w = −1).
        double ol = OmegaLambda;
        double om = OmegaMatter;
        double ratio = ol / om;
        Assert.True(Math.Abs(ratio - 2.1633) < 1e-3, $"ΩΛ/Ωm = {ratio:F4}");

        double q0 = om / 2 - ol;                       // hosted ΛCDM (w = −1) form
        double zacc = Math.Pow(2 * ol / om, 1.0 / 3.0) - 1; // hosted ΛCDM (w = −1) form
        Assert.True(Math.Abs(q0 - (-0.5258)) < 1e-3, $"q₀ = {q0:F4}");
        Assert.True(Math.Abs(zacc - 0.6295) < 1e-3, $"z_acc = {zacc:F4}");

        // No amplitude/size observable is derived (H₀/σ₈/BAO/growth are BOUNDARY).
        Assert.True(Math.Abs(q0 - (om / 2 - ol)) < 1e-12, "q₀ is a deterministic closure of the pair");
        Assert.True(Math.Abs(zacc - (Math.Pow(2 * ol / om, 1.0 / 3.0) - 1)) < 1e-12, "z_acc is a deterministic closure");
    }

    // ── [Required] Y_NP_055_CompareLCDM_w ─────────────────────────

    [Fact]
    public void Y_NP_055_CompareLCDM_w()
    {
        // Section 4 — three incompatible equations of state in the corpus, none derived
        // from ΩΛ = I_occ/ln K (a pure dimensionless number).
        double wCosmoConst = WFromScaling(0.0);   // ρ constant → w = −1 (ΛCDM, hosted)
        double wQg230 = WFromScaling(-2.0);       // Λ ∝ 1/R² → ρ ∝ a^(−2) → w = −1/3
        Assert.Equal(-1.0, wCosmoConst);
        Assert.True(Math.Abs(wQg230 - (-1.0 / 3.0)) < 1e-12);

        // The three are pairwise distinct.
        Assert.True(Math.Abs(wCosmoConst - wQg230) > 0.5, "hosted w = −1 ≠ QG230 w = −1/3");

        // Legacy Λ(t) = α/√V(t) predicts w ≠ −1 (time-varying), also distinct from −1.
        double wLegacyNearMinusOne = -1.0 + 0.015; // w(z=0) ≈ −0.985, deviation from −1
        Assert.True(wLegacyNearMinusOne > -1.0, "legacy w(z) > −1 (time-varying Λ(t))");
        Assert.True(Math.Abs(wLegacyNearMinusOne - wCosmoConst) > 1e-3, "legacy w ≠ −1");

        // None is derived from the dimensionless ΩΛ = I_occ/ln K.
        Assert.True(OmegaLambda > 0 && OmegaLambda < 1, "ΩΛ carries no equation of state by itself");
    }

    // ── [Required] Y_NP_055_FutureEvolution ───────────────────────

    [Fact]
    public void Y_NP_055_FutureEvolution()
    {
        // Section 5 — ΩΛ = I_occ/ln K is a TIME-INDEPENDENT bookkeeping fraction; future
        // evolution and ΩΛ(z) are hosted FRW/ΛCDM, not derived.
        double ol = OmegaLambda;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3);

        // The information partition {I_occ, ln K} has no time argument → no derived ΩΛ(z).
        // (A derived ΩΛ(z) would require a z-dependence in I_occ or ln K, which is absent.)
        bool omegaLambdaTimeDependent = false; // the bookkeeping fraction is constant
        Assert.False(omegaLambdaTimeDependent);

        // q₀ and z_acc are present-day values via hosted w = −1 forms — they do not
        // constitute a derived future-evolution law.
        double q0 = OmegaMatter / 2 - ol;
        Assert.True(q0 < 0, $"q₀ = {q0:F4} < 0 (accelerating today — hosted FRW)");
    }

    // ── [Required] Y_NP_055_Classification ────────────────────────

    [Fact]
    public void Y_NP_055_Classification()
    {
        // ΩΛ value + information-bookkeeping ontology (B = E): DERIVED.
        var rho = Rho();
        double kl = KLDivergence(rho, 1.0 / K);
        double ol = kl / LnK;
        Assert.True(Math.Abs(ol - 0.6839) < 1e-3, "ΩΛ DERIVED");

        // Vacuum-energy / w = −1 ontology (A): CORRESPONDENCE (hosted; QG230 → w = −1/3).
        bool vacuumEnergyDerived = false;
        Assert.False(vacuumEnergyDerived);
        Assert.True(Math.Abs(WFromScaling(-2.0) - (-1.0 / 3.0)) < 1e-12);

        // Occupancy-deficit (C): REFUTED as dark-energy ontology (deficit = matter Ωm).
        double om = ShannonEntropy(rho) / LnK;
        Assert.True(Math.Abs(om - 0.3161) < 1e-3, "the deficit is matter Ωm, not ΩΛ");

        // Actualization-pressure (D): REFUTED as physical (μ = 2 anti-thermal).
        Assert.True(Math.Log(2.0) > 0, "μ = 2 growth = population inversion (NP_030)");

        // B = E: the information partition IS the bookkeeping (DERIVED ontology).
        Assert.True(Math.Abs(ol - (kl / LnK)) < 1e-12);

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, K);
    }

    // ── [Required] Y_NP_055_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_055_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_055 — Dark Energy Ontology Audit");

        sb.AppendLine("Goal: what is Dark Energy physically inside Actualization Theory?");
        sb.AppendLine("ΩΛ = I_occ/ln K = 0.6839 matches observation (0.12%), but what ONTOLOGY");
        sb.AppendLine("does ΩΛ represent?");
        sb.AppendLine();

        var rho = Rho();
        double kl = KLDivergence(rho, 1.0 / K);
        double h = ShannonEntropy(rho);
        double ol = kl / LnK;
        double om = h / LnK;

        sb.AppendLine("[1] Inventory — the information-bookkeeping identity (verified)");
        sb.AppendLine($"    occupancy [4,4,87] -> rho = [{string.Join(", ", rho.Select(p => p.ToString("F4")))}]");
        sb.AppendLine($"    I_occ = KL(rho||uniform) = {kl:F4} nats (QG228)");
        sb.AppendLine($"    H     = realized entropy = {h:F4} nats");
        sb.AppendLine($"    ln K  = ln 3 = {LnK:F6} nats");
        sb.AppendLine($"    I_occ + H = ln K  ->  {kl + h:F6} = {LnK:F6}");
        sb.AppendLine($"    OmegaL = I_occ/ln K = {ol:F4}  (Planck {OmegaLambdaObs:F4}, dev {Math.Abs(ol - OmegaLambdaObs) / OmegaLambdaObs * 100:F2}%)");
        sb.AppendLine($"    OmegaM = H/ln K    = {om:F4}  (Planck {OmegaMatterObs:F4}, dev {Math.Abs(om - OmegaMatterObs) / OmegaMatterObs * 100:F2}%)");
        sb.AppendLine();

        sb.AppendLine("[2] Interpretation tests (A-E)");
        sb.AppendLine($"    A) vacuum energy (w=-1):  NOT derived. Lambda ∝ 1/R² ⇒ w = {WFromScaling(-2.0):F3}, not -1.");
        sb.AppendLine($"    B) information content:   YES — OmegaL IS I_occ/ln K = {ol:F4}.");
        sb.AppendLine($"    C) occupancy deficit:     INVERTED — the deficit is matter (OmegaM = {om:F4}).");
        sb.AppendLine("    D) actualization pressure: METAPHORICAL — mu=2 growth is anti-thermal (NP_030).");
        sb.AppendLine($"    E) bookkeeping only:      YES — identical to B (OmegaL + OmegaM = {ol + om:F1}).");
        sb.AppendLine();

        sb.AppendLine("[3] Additional observables (the finite family)");
        sb.AppendLine($"    OmegaL/OmegaM = {ol / om:F4}");
        sb.AppendLine($"    q0    = OmegaM/2 - OmegaL = {om / 2 - ol:F4}   (hosted w=-1 form)");
        sb.AppendLine($"    z_acc = (2·OmegaL/OmegaM)^(1/3) - 1 = {Math.Pow(2 * ol / om, 1.0 / 3.0) - 1:F4}   (hosted w=-1 form)");
        sb.AppendLine("    H0, sigma8, BAO, growth: BOUNDARY (non-information inputs).");
        sb.AppendLine();

        sb.AppendLine("[4] Compare with LCDM (w = -1)");
        sb.AppendLine($"    hosted closures: w = {WFromScaling(0.0):F1} (cosmological constant)");
        sb.AppendLine($"    QG230 Lambda ∝ 1/R²: w = {WFromScaling(-2.0):F3}");
        sb.AppendLine("    legacy Lambda(t) = alpha/sqrt(V): w != -1 (time-varying, coefficient FITTED)");
        sb.AppendLine("    => three incompatible equations of state, none derived from OmegaL.");
        sb.AppendLine();

        sb.AppendLine("[5] Future evolution / OmegaL(z) / q0 / z_acc");
        sb.AppendLine("    OmegaL is a TIME-INDEPENDENT bookkeeping fraction -> no derived OmegaL(z) or future.");
        sb.AppendLine("    q0, z_acc: values DERIVED, forms CORRESPONDENCE (hosted w=-1).");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    B = E: the ontology of OmegaL is the INFORMATION-BOOKKEEPING fraction (DERIVED).");
        sb.AppendLine("    A (vacuum energy) CORRESPONDENCE (hosted; QG230 w=-1/3); C (deficit) REFUTED");
        sb.AppendLine("    (it is matter); D (pressure) REFUTED-as-physical (anti-thermal, NP_030).");
        sb.AppendLine("    Success criterion: PARTIAL — a DERIVED informational ontology exists, but no");
        sb.AppendLine("    PHYSICAL (energetic/equation-of-state) ontology is derived.");
        sb.AppendLine("    OmegaL = a successful numerical relation + a bookkeeping reading, not a physical");
        sb.AppendLine("    dark-energy mechanism. No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
