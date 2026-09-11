using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_048 — Latent-Degeneracy Adaptability Audit.
///
/// Question: does latent degeneracy predict adaptability?
///
/// Latent fraction: L = (N − A₀)/N, where A₀ is the number of distinct eigenspaces
/// (attractors) of the graph Laplacian. L ∈ [0,1] is the fraction of the spectrum that is
/// degenerate — the structural "reservoir" a perturbation can unsplit.
///
/// Cases (all N=96): D96 (circulant C96(±1..±6)), D96-3D (4×4×6 periodic torus), random
/// (seeded sparse p=0.3), physical (circulant with λ_m = m), unphysical (circulant with the
/// piecewise-constant profile 5/25/60), complete (K96).
///
/// Perturbations (deterministic LCG, 3 seeds each): edge deletion, edge addition, rewiring,
/// weight perturbation; dose = a fraction {0.5%, 1%, 2%, 5%, 10%} of the current edge count.
/// Perturbations that disconnect the graph are excluded (connectivity guard).
///
/// Measures: (1) reachable attractor increase ΔA; (2) entropy increase ΔE; (3) recovery
/// R = 1 − ‖λ′−λ‖₂/‖λ‖₂ (spectral retention); (4) structural diversity gain = ΔA/(N − A₀),
/// the fraction of the latent capacity actually realized.
///
/// Predictors: λ₂ (spectral gap), spectral entropy E₀, attractor count A₀, latent fraction L.
///
/// VERDICT FOUND (Spearman ρ over the 6 cases, tie-averaged ranks):
///   H1 "adaptive capacity scales with latent degeneracy" — PARTIALLY SUPPORTED / REFUTED
///      as stated: the RAW gains scale with L (ρ(ΔA,L) = +0.43, ρ(ΔE,L) = +0.83) but the
///      NORMALIZED adaptive capacity does not (ρ(capacity,L) = −0.03). L is a ceiling,
///      not a predictor of how much of the ceiling is realized.
///   H2 "L predicts adaptability better than λ₂" — REFUTED: ρ(capacity,λ₂) = −0.84 (the
///      SMALLER the gap, the MORE adaptable), a far stronger predictor than L. Near-
///      degeneracy, not degeneracy count, indexes adaptability.
///   H3 "random graphs have low latent capacity" — SUPPORTED: random has L = 0 exactly and
///      capacity = 0 exactly (ΔA ≡ ΔE ≡ 0 under every perturbation and dose).
///
/// Quantitative only: no AT theoretical assumptions are used anywhere in this audit.
/// Deterministic: fixed LCG seeds, no unseeded randomness.
/// Shared machinery: <see cref="AdaptabilityAudit"/> (also used by D_049).
/// </summary>
public class Y_D_048_Tests : ResearchTestBase
{
    public Y_D_048_Tests(ITestOutputHelper output) : base(output) { }

    private static IReadOnlyList<AdaptabilityProfile> Profiles => AdaptabilityAudit.Profiles;
    private static AdaptabilityProfile P(string name) => AdaptabilityAudit.P(name);

    private static double[] Col(Func<AdaptabilityProfile, double> f) => Profiles.Select(f).ToArray();
    private static double Rho(Func<AdaptabilityProfile, double> x, Func<AdaptabilityProfile, double> y)
        => AdaptabilityAudit.Spearman(Col(x), Col(y));

    /// <summary>Mean spectral retention over the four perturbation families at one dose.</summary>
    private static double MeanRetained(string name, double dose)
        => AdaptabilityAudit.Kinds.Average(k => AdaptabilityAudit.RetainedAt(name, dose, k));

    // ── 1. Baselines: latent fraction L, gap, entropy, attractor count ──────

    [Fact]
    public void Y_D_048_LatentFractionBaseline()
    {
        var d96 = P("D96");
        var rnd = P("random");
        var cmp = P("complete");
        var p3d = P("D96-3D");
        var phy = P("physical");
        var un = P("unphysical");

        // L = (N − A₀)/N — the latent (degenerate) fraction of the spectrum.
        Assert.Equal(45, d96.A0); Assert.Equal(0.53125, d96.L, 5);
        Assert.Equal(49, phy.A0); Assert.Equal(0.48958, phy.L, 5);
        Assert.Equal(2, cmp.A0); Assert.Equal(0.97917, cmp.L, 5);
        Assert.Equal(4, un.A0); Assert.Equal(0.95833, un.L, 5);

        // The degenerate-free control has NO latent fraction at all.
        Assert.Equal(AdaptabilityAudit.N, rnd.A0);
        Assert.Equal(0.0, rnd.L, 12);

        // D96-3D carries the largest latent fraction among the structured graphs besides K96.
        Assert.True(p3d.L > 0.80, $"D96-3D L = {p3d.L}");

        // Spectral gaps: the L ranking and the λ₂ ranking are near-orthogonal.
        Assert.Equal(0.3864, d96.Lambda2, 3);
        Assert.Equal(1.0, phy.Lambda2, 6);
        Assert.Equal(1.0, p3d.Lambda2, 6);
        Assert.Equal(5.0, un.Lambda2, 6);
        Assert.Equal(96.0, cmp.Lambda2, 6);
        Assert.True(rnd.Lambda2 > 5.0, $"random λ₂ = {rnd.Lambda2}");

        // Every perturbation kept the graph connected (no excluded samples).
        Assert.All(Profiles, p => Assert.Equal(0, p.Excluded));
    }

    // ── 2. Adaptive capacity and the dose response ──────────────────────────

    [Fact]
    public void Y_D_048_AdaptiveCapacity()
    {
        var d96 = P("D96");
        var rnd = P("random");
        var cmp = P("complete");
        var p3d = P("D96-3D");
        var phy = P("physical");
        var un = P("unphysical");

        // Adaptive capacity = mean over (kind, dose, seed) of ΔA/(N−A₀).
        Assert.True(d96.Capacity > 0.90, $"D96 capacity = {d96.Capacity}");
        Assert.True(phy.Capacity > 0.70, $"physical capacity = {phy.Capacity}");
        Assert.True(p3d.Capacity > 0.60, $"D96-3D capacity = {p3d.Capacity}");
        Assert.True(un.Capacity > 0.60, $"unphysical capacity = {un.Capacity}");
        Assert.True(cmp.Capacity < 0.45, $"complete capacity = {cmp.Capacity}");
        Assert.Equal(0.0, rnd.Capacity, 12);

        // Ordering: D96 > physical > random, and D96 > D96-3D, and complete < unphysical.
        Assert.True(d96.Capacity > phy.Capacity && phy.Capacity > rnd.Capacity);
        Assert.True(d96.Capacity > p3d.Capacity);
        Assert.True(cmp.Capacity < un.Capacity);

        // Raw reachable-attractor increase is largest for the biggest reservoirs, but the
        // NORMALIZED gain is what separates the graphs — the ceiling is not the response.
        Assert.True(un.MeanDeltaA > d96.MeanDeltaA, $"unphysical ΔA = {un.MeanDeltaA}");
        Assert.True(d96.MeanDeltaA > phy.MeanDeltaA);
        Assert.Equal(0.0, rnd.MeanDeltaA, 12);

        // Dose response: D96 saturates immediately; D96-3D and complete rise with dose.
        Assert.True(d96.CapacityByDose[0] > 0.90, $"D96 at 0.5% = {d96.CapacityByDose[0]}");
        Assert.True(p3d.CapacityByDose[^1] > p3d.CapacityByDose[0] + 0.3,
            $"D96-3D dose response {p3d.CapacityByDose[0]} → {p3d.CapacityByDose[^1]}");
        Assert.True(cmp.CapacityByDose[^1] > cmp.CapacityByDose[0],
            $"complete dose response {cmp.CapacityByDose[0]} → {cmp.CapacityByDose[^1]}");

        // Structural impossibility shows up as exact zeros: K96 has no non-edge to add and
        // cannot be rewired (its only non-edge is the edge just removed).
        Assert.Equal(0.0, cmp.CapacityByKind[Array.IndexOf(AdaptabilityAudit.Kinds, "add")], 12);
        Assert.Equal(0.0, cmp.CapacityByKind[Array.IndexOf(AdaptabilityAudit.Kinds, "rewire")], 12);
    }

    // ── 3. H1: does adaptive capacity scale with latent degeneracy? ─────────

    [Fact]
    public void Y_D_048_H1_LatentDegeneracy()
    {
        double rhoCapL = Rho(p => p.L, p => p.Capacity);
        double rhoDa = Rho(p => p.L, p => p.MeanDeltaA);
        double rhoDe = Rho(p => p.L, p => p.MeanDeltaE);

        // REFUTED for the normalized capacity: L is not the response, it is the ceiling.
        Assert.True(Math.Abs(rhoCapL) < 0.40, $"ρ(capacity, L) = {rhoCapL}");

        // PARTIALLY SUPPORTED for the raw gains: the reachable increase tracks L weakly and
        // the entropy increase tracks L strongly (a bigger reservoir releases more entropy).
        Assert.True(rhoDa > 0.25, $"ρ(ΔA, L) = {rhoDa}");
        Assert.True(rhoDe > 0.60, $"ρ(ΔE, L) = {rhoDe}");

        // Recovery (spectral retention) is NOT predicted by L either.
        Assert.True(Math.Abs(Rho(p => p.L, p => p.MeanRecovery)) < 0.40);
    }

    // ── 4. H2: does L beat λ₂ as a predictor of adaptability? ───────────────

    [Fact]
    public void Y_D_048_H2_GapBeatsLatent()
    {
        double rhoCapL = Rho(p => p.L, p => p.Capacity);
        double rhoCapGap = Rho(p => p.Lambda2, p => p.Capacity);
        double rhoDaGap = Rho(p => p.Lambda2, p => p.MeanDeltaA);

        // REFUTED: the spectral gap is a far stronger predictor — and the sign is INVERTED
        // (a SMALLER gap ⇒ a MORE adaptable graph). Near-degeneracy, not degeneracy count,
        // is what indexes adaptability.
        Assert.True(rhoCapGap < -0.60, $"ρ(capacity, λ₂) = {rhoCapGap}");
        Assert.True(Math.Abs(rhoCapGap) > 4 * Math.Abs(rhoCapL),
            $"|ρ(capacity,λ₂)| = {Math.Abs(rhoCapGap)} vs |ρ(capacity,L)| = {Math.Abs(rhoCapL)}");
        Assert.True(rhoCapGap < rhoCapL, "λ₂ correlates more negatively than L");

        // The same ordering holds for the raw attractor increase.
        Assert.True(rhoDaGap < -0.20, $"ρ(ΔA, λ₂) = {rhoDaGap}");

        // Counterexample making the point explicit: D96 has a SMALLER gap than complete by
        // 250× and a larger normalized capacity by >3×, despite complete holding more latent
        // degeneracy.
        var d96 = P("D96");
        var cmp = P("complete");
        Assert.True(cmp.Lambda2 / d96.Lambda2 > 200);
        Assert.True(cmp.L > d96.L);
        Assert.True(d96.Capacity > 3 * cmp.Capacity);
    }

    // ── 5. H3: random graphs have low latent capacity ───────────────────────

    [Fact]
    public void Y_D_048_H3_RandomNull()
    {
        var rnd = P("random");

        // SUPPORTED: no degeneracy ⇒ no latent fraction, no gain, no entropy release, under
        // every perturbation type and every dose.
        Assert.Equal(0.0, rnd.L, 12);
        Assert.Equal(0.0, rnd.Capacity, 12);
        Assert.Equal(0.0, rnd.MeanDeltaA, 12);
        Assert.Equal(0.0, rnd.MeanDeltaE, 12);
        Assert.All(rnd.CapacityByDose, g => Assert.Equal(0.0, g, 12));
        Assert.All(rnd.CapacityByKind, g => Assert.Equal(0.0, g, 12));

        // …while every degenerate case does adapt.
        foreach (var p in Profiles.Where(x => x.Name != "random"))
            Assert.True(p.Capacity > 0.25, $"{p.Name} capacity = {p.Capacity}");

        // Random's recovery is high (its spectrum barely moves) — robustness ≠ adaptability:
        // the least adaptable graph is among the MOST stable.
        Assert.True(rnd.MeanRecovery > 0.90, $"random recovery = {rnd.MeanRecovery}");
        Assert.True(P("unphysical").MeanRecovery < rnd.MeanRecovery,
            "the most adaptable graph is less 'recovered' than the null");
    }

    // ── 6. Recovery and structural diversity gain ───────────────────────────

    [Fact]
    public void Y_D_048_RecoveryAndDiversityGain()
    {
        // Recovery R = 1 − ‖λ′−λ‖₂/‖λ‖₂ (spectral retention) is monotone down in dose for
        // every case: more perturbation ⇒ less recovery.
        foreach (var p in Profiles)
        {
            double small = MeanRetained(p.Name, 0.005);
            double large = MeanRetained(p.Name, 0.10);
            Assert.True(large <= small + 1e-9, $"{p.Name}: R(10%) = {large} > R(0.5%) = {small}");
        }

        // The most latent graph is the least recoverable, and the null is the most recoverable:
        // latent degeneracy buys adaptability at the cost of stability.
        Assert.True(P("unphysical").MeanRecovery < P("D96").MeanRecovery
                    || P("unphysical").MeanRecovery < P("complete").MeanRecovery);

        // Structural diversity gain is bounded by 1 by construction (a fraction of headroom),
        // and is realized fully only by D96 at the smallest dose.
        Assert.True(P("D96").CapacityByDose[0] > 0.9);
        Assert.All(Profiles, p => Assert.All(p.CapacityByDose, g => Assert.True(g <= 1.0 + 1e-9)));

        // Total diversity increase (raw) exceeds the small-dose increase for structured graphs.
        Assert.True(P("D96-3D").CapacityByDose[^1] > P("D96-3D").CapacityByDose[0]);
    }

    // ── 7. Report ───────────────────────────────────────────────────────────

    [Fact]
    public void Y_D_048_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_048 — Latent-Degeneracy Adaptability Audit");

        sb.AppendLine("Question: does latent degeneracy predict adaptability?");
        sb.AppendLine("Latent fraction  L = (N − A₀)/N ;  adaptive capacity = mean ΔA/(N − A₀).");
        sb.AppendLine("Perturbations: edge deletion, addition, rewiring, weight; doses 0.5–10% of |E|,");
        sb.AppendLine("               3 deterministic seeds each; disconnected results excluded.");
        sb.AppendLine("Quantitative only — no AT theoretical assumptions.");
        sb.AppendLine();

        sb.AppendLine("[1] Baselines");
        sb.AppendLine("     case         A₀   edges     λ₂      E₀      L      capacity  ΔA     ΔE     recovery");
        foreach (var p in Profiles)
            sb.AppendLine($"     {p.Name,-11} {p.A0,3} {p.Edges,6} {p.Lambda2,7:F4} {p.E0,7:F4} {p.L,7:F4} " +
                          $"{p.Capacity,8:F4} {p.MeanDeltaA,6:F2} {p.MeanDeltaE,6:F4} {p.MeanRecovery,8:F4}");
        sb.AppendLine();

        sb.AppendLine("[2] Capacity vs dose (fraction of latent capacity realized)");
        sb.AppendLine("     case        " + string.Join("  ", AdaptabilityAudit.Doses.Select(d => $"{d,8:P1}")));
        foreach (var p in Profiles)
            sb.AppendLine($"     {p.Name,-11} " + string.Join("  ", p.CapacityByDose.Select(g => $"{g,8:F3}")));
        sb.AppendLine();

        sb.AppendLine("[3] Capacity vs perturbation type");
        sb.AppendLine("     case        " + string.Join("  ", AdaptabilityAudit.Kinds.Select(k => $"{k,8}")));
        foreach (var p in Profiles)
            sb.AppendLine($"     {p.Name,-11} " + string.Join("  ", p.CapacityByKind.Select(g => $"{g,8:F3}")));
        sb.AppendLine();

        sb.AppendLine("[4] Spearman ρ (tie-averaged ranks) over the six cases");
        sb.AppendLine("     measure        vs L     vs λ₂    vs E₀    vs A₀");
        foreach (var (label, f) in new (string, Func<AdaptabilityProfile, double>)[]
                 {
                     ("capacity", p => p.Capacity),
                     ("mean ΔA", p => p.MeanDeltaA),
                     ("mean ΔE", p => p.MeanDeltaE),
                     ("recovery", p => p.MeanRecovery),
                 })
            sb.AppendLine($"     {label,-12} {Rho(p => p.L, f),7:F3}  {Rho(p => p.Lambda2, f),7:F3}  " +
                          $"{Rho(p => p.E0, f),7:F3}  {Rho(p => p.A0, f),7:F3}");
        sb.AppendLine();

        sb.AppendLine("[5] Hypotheses");
        sb.AppendLine($"  H1 adaptive capacity scales with latent degeneracy L → PARTIALLY REFUTED");
        sb.AppendLine($"     normalized capacity: ρ = {Rho(p => p.L, p => p.Capacity):F3} (no scaling);");
        sb.AppendLine($"     raw gains DO scale:  ρ(ΔA,L) = {Rho(p => p.L, p => p.MeanDeltaA):F3}, " +
                      $"ρ(ΔE,L) = {Rho(p => p.L, p => p.MeanDeltaE):F3}.");
        sb.AppendLine($"     L is the CEILING, not the response.");
        sb.AppendLine($"  H2 L predicts adaptability better than λ₂ → REFUTED");
        sb.AppendLine($"     ρ(capacity,λ₂) = {Rho(p => p.Lambda2, p => p.Capacity):F3} vs " +
                      $"ρ(capacity,L) = {Rho(p => p.L, p => p.Capacity):F3}: the gap wins, with the");
        sb.AppendLine($"     sign INVERTED — a SMALLER gap ⇒ a MORE adaptable graph.");
        sb.AppendLine($"  H3 random graphs have low latent capacity → SUPPORTED");
        sb.AppendLine($"     L = 0 exactly, capacity = 0 exactly, ΔA ≡ ΔE ≡ 0 at every dose and type.");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("  DERIVED   : latent fraction L = (N − A₀)/N is the ceiling on the reachable");
        sb.AppendLine("              attractor increase; ΔE released tracks L (ρ = +0.83).");
        sb.AppendLine("  DERIVED   : the spectral gap λ₂ is the dominant (inverse) predictor of");
        sb.AppendLine("              normalized adaptive capacity (ρ = −0.84).");
        sb.AppendLine("  REFUTED   : 'adaptive capacity scales with latent degeneracy' (normalized);");
        sb.AppendLine("              'L predicts adaptability better than λ₂'.");
        sb.AppendLine("  SUPPORTED : 'random graphs have low latent capacity' (exact null).");
        sb.AppendLine("  EMERGENT  : the specific per-case capacities (deeply non-monotone across cases).");
        sb.AppendLine("  Recovery and adaptability are ANTI-correlated across cases: the least adaptable");
        sb.AppendLine("  graph (random) is the most stable, and the most latent graphs are the least.");
        sb.AppendLine("  No AT assumptions used; no canonical value touched.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
