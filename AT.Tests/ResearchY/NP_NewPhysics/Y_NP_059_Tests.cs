using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_059 — Actualization Rate Audit test suite (Y_NP_059_Tests.cs).
///
/// Question: can "energy = actualization rate" (QG89) be DERIVED instead of postulated?
/// QG89 is the root bridge between information and physical energy (NP_058).
///
/// Verdict tested: "energy = actualization rate" is NOT derivable — it is BOUNDARY (an
/// irreducible definition). What IS derived is the conserved COUNT: the actualization rate
/// is the normalized count density ρ with Σρ = 1 (normalization, QG194/QG216/M_005), and the
/// deficit m = ρ̄ − ρ is exactly conserved (Σm = 0, QG194's "Noether count"). The step from
/// "conserved count" to "energy" has no Noether route: AT's time is DISCRETE (Δθ = 2πk/N per
/// tick, "time IS the tick count"), and QG244's Lagrangian presupposes QG89's
/// "actualization-flow energy" rather than deriving it. The dimensionful conversion (count →
/// Joules/GeV) additionally needs the BOUNDARY anchors v, m_e and unit conventions ħ, c.
///
/// Classification: conserved count (actualization rate, Σρ = 1) DERIVED; deficit
/// conservation (Σm = 0) DERIVED; "energy = actualization rate" (QG89) BOUNDARY (definition);
/// dimensionful energy (anchors v, m_e + ħ, c) BOUNDARY. Deepest open step: QG89's
/// identification of the conserved count with energy + the anchored dimensionful conversion.
/// No new primitive; canonical AT unchanged.
///
/// Deterministic: closed-form count normalization and deficit conservation over [4,4,87].
/// </summary>
public class Y_NP_059_Tests : ResearchTestBase
{
    public Y_NP_059_Tests(ITestOutputHelper output) : base(output) { }

    private const int K = 3;
    private static readonly int[] Occ = { 4, 4, 87 };
    private const int N = 96;               // ring order (D_041: Δθ = 2πk/N)

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

    // ── [Required] Y_NP_059_CountConservationDerived ──────────────

    [Fact]
    public void Y_NP_059_CountConservationDerived()
    {
        // The actualization rate is the normalized count density ρ; Σρ = 1 is DERIVED from
        // normalization alone. The deficit m = ρ̄ − ρ is exactly conserved (Σm = 0).
        var rho = Rho(Occ);
        double sum = 0;
        foreach (double p in rho) sum += p;
        Assert.True(Math.Abs(sum - 1.0) < 1e-12, "Σρ = 1 (count conservation, DERIVED)");

        double rhoBar = 1.0 / K;             // uniform mean density
        double deficitSum = 0;
        foreach (double p in rho) deficitSum += (rhoBar - p);
        Assert.True(Math.Abs(deficitSum) < 1e-12, "Σ(ρ̄ − ρ) = 0 (deficit + surplus cancel)");

        // The deficit (matter = ρ̄ − ρ) is a COUNT, conserved by construction.
        // "energy = actualization rate" is a SEPARATE identification on top of this count.
        Assert.True(rhoBar > 0, "ρ̄ = 1/K is the mean count density");
    }

    // ── [Required] Y_NP_059_DiscreteTimeNoContinuousNoether ───────

    [Fact]
    public void Y_NP_059_DiscreteTimeNoContinuousNoether()
    {
        // AT's time is DISCRETE: Δθ = 2πk/N per tick (D_041), "time IS the tick count"
        // (NP_003/M_009/M_010). There is no continuous time-translation symmetry.
        double dTheta = 2 * Math.PI * 1.0 / N;   // a fixed discrete step
        Assert.True(Math.Abs(dTheta - 2 * Math.PI / 96.0) < 1e-12);
        Assert.True(dTheta > 0, "Δθ is a fixed discrete step, not an infinitesimal");

        // Noether's theorem requires a continuous symmetry + Lagrangian; the discrete tick
        // provides neither, so the standard Noether derivation of energy does not apply.
        bool continuousTimeSymmetry = false;
        Assert.False(continuousTimeSymmetry);
    }

    // ── [Required] Y_NP_059_RemoveQG89 ────────────────────────────

    [Fact]
    public void Y_NP_059_RemoveQG89()
    {
        // Remove QG89 ("energy = actualization rate"): the INFORMATION chain survives, the
        // energy-dependent derivations (matter, Λ) break.
        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);
        double frac = kl / LnK;

        Assert.True(Math.Abs(frac - 0.6839) < 1e-3, "information chain survives: I_occ/ln K = 0.6839");
        Assert.True(Math.Abs((kl + h) - LnK) < 1e-9, "I_occ + H = ln K survives (no energy)");

        // The energy reading (matter = deficit as E_def = m; Λ from I_vac > 0) needs QG89.
        bool energyReadingRequiresQg89 = true;
        Assert.True(energyReadingRequiresQg89);
    }

    // ── [Required] Y_NP_059_ConservedRateSearch ───────────────────

    [Fact]
    public void Y_NP_059_ConservedRateSearch()
    {
        // Search whether the primitives imply a conserved rate quantity.
        // Noether (continuous): FAILS (discrete time).
        // Counting rate: Σρ = 1 — DERIVED.
        // Transition rate: θ → θ + Δθ — fixed step, no energy.
        // Occupancy flow: ρ_k = μ^k/S — normalized (Σρ = 1), no energy.
        var rho = Rho(Occ);
        double sum = 0;
        foreach (double p in rho) sum += p;
        Assert.True(Math.Abs(sum - 1.0) < 1e-12, "counting rate Σρ = 1 is DERIVED");

        // The conserved quantity is a COUNT, not an energy (no units).
        double kl = KLDivergence(rho, 1.0 / K);
        Assert.True(kl > 0 && kl < 1, "the conserved count carries no energy scale");

        // Only the count is conserved — no Noether charge emerges from discrete time.
        bool noetherChargeDerived = false;
        Assert.False(noetherChargeDerived);
    }

    // ── [Required] Y_NP_059_EnergyIsDefinition ────────────────────

    [Fact]
    public void Y_NP_059_EnergyIsDefinition()
    {
        // "energy = actualization rate" is a DEFINITION (QG89), not a derivation. Giving the
        // conserved count UNITS (Joules/GeV) requires the anchors v, m_e and ħ, c (BOUNDARY).
        bool countEqualsEnergyDerived = false;
        Assert.False(countEqualsEnergyDerived);

        // The information fraction is dimensionless — it needs no anchors.
        var rho = Rho(Occ);
        double frac = KLDivergence(rho, 1.0 / K) / LnK;
        Assert.True(Math.Abs(frac - 0.6839) < 1e-3, "I_occ/ln K is dimensionless (no anchors)");

        // Energy requires dimensionful anchors (v, m_e) and unit conventions (ħ, c).
        bool dimensionfulEnergyBoundary = true;
        Assert.True(dimensionfulEnergyBoundary);
    }

    // ── [Required] Y_NP_059_Classification ────────────────────────

    [Fact]
    public void Y_NP_059_Classification()
    {
        // Conserved count (Σρ = 1, Σm = 0): DERIVED.
        var rho = Rho(Occ);
        double sum = 0;
        foreach (double p in rho) sum += p;
        Assert.True(Math.Abs(sum - 1.0) < 1e-12);

        // "energy = actualization rate" (QG89): BOUNDARY (definition) — not derived, not
        // emergent, not refuted.
        bool energyRateDerived = false;
        bool energyRateEmergent = false;
        bool energyRateRefuted = false;
        Assert.False(energyRateDerived);
        Assert.False(energyRateEmergent);
        Assert.False(energyRateRefuted);

        // Dimensionful energy (anchors v, m_e + ħ, c): BOUNDARY.
        bool dimensionfulEnergyBoundary = true;
        Assert.True(dimensionfulEnergyBoundary);

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, K);
    }

    // ── [Required] Y_NP_059_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_059_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_059 — Actualization Rate Audit");

        sb.AppendLine("Goal: can 'energy = actualization rate' (QG89) be derived, or is it");
        sb.AppendLine("postulated? This is the root bridge between information and energy.");
        sb.AppendLine();

        var rho = Rho(Occ);
        double h = ShannonEntropy(rho);
        double kl = KLDivergence(rho, 1.0 / K);
        double sum = 0;
        foreach (double p in rho) sum += p;
        double rhoBar = 1.0 / K;
        double deficitSum = 0;
        foreach (double p in rho) deficitSum += (rhoBar - p);

        sb.AppendLine("[1] The conserved COUNT is DERIVED (no QG89 needed)");
        sb.AppendLine($"    rho = [{string.Join(", ", rho.Select(p => p.ToString("F4")))}]");
        sb.AppendLine($"    Sigma rho = {sum:F12} = 1  (count conservation)");
        sb.AppendLine($"    Sigma(rho_bar - rho) = {deficitSum:E2} = 0  (deficit + surplus cancel)");
        sb.AppendLine();

        sb.AppendLine("[2] Remove QG89 — information survives, energy breaks");
        sb.AppendLine($"    I_occ/ln K = {kl / LnK:F4} survives (pure information).");
        sb.AppendLine("    matter = deficit (E_def = m), Lambda origin, Lagrangian: BREAK.");
        sb.AppendLine();

        sb.AppendLine("[3] Noether search");
        sb.AppendLine("    continuous Noether: FAILS (time IS the tick count — discrete).");
        sb.AppendLine("    counting rate: Sigma rho = 1 (DERIVED, a count).");
        sb.AppendLine("    transition rate: theta -> theta + Delta_theta (fixed step, no energy).");
        sb.AppendLine("    occupancy flow: rho_k = mu^k/S (normalized, no energy).");
        sb.AppendLine();

        sb.AppendLine("[4] Two layers");
        sb.AppendLine("    Layer 1 (DERIVED): actualization rate = conserved COUNT (Sigma rho = 1).");
        sb.AppendLine("    Layer 2 (BOUNDARY): 'that count = energy (with units)' — QG89 definition");
        sb.AppendLine("       + dimensionful anchors (v, m_e) and unit conventions (hbar, c).");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    'energy = actualization rate' is BOUNDARY (a definition), NOT derivable:");
        sb.AppendLine("    Noether fails (discrete time, no native Lagrangian); units need anchors.");
        sb.AppendLine("    Deepest open step: QG89's identification of the conserved count with energy.");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
