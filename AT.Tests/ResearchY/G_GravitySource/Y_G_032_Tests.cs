using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_032 — Conformal Assumption Audit (group G — Gravity Source).
///
/// QUESTION. Is `g = Ω²η` derived, or only assumed? Trace Difference → Counting → ρ → metric; find the step
/// that FIRST requires the conformal ansatz; and decide whether any existing AT primitive forces conformal
/// flatness.
///
/// THE ANSWER. **ASSUMED.** The ansatz first becomes necessary at the ρ → metric step — the step at which the
/// Difference acquires its TENSOR face. Steps 1–2 carry no metric content at all. The repository's own account
/// localises the import exactly: QG288 — *"the tensor face requires η [conformal reference, QG285], the scalar
/// face does not"* — and QG289 classifies η as a **TRUE THEORY INPUT**: *"part of the geometry, not a choice."*
///
/// NO PRIMITIVE FORCES IT. Set η aside (it IS the conformal statement, so citing it restates the assumption).
/// Every other primitive is rank 0 — ρ, ψ, d, π — and a scalar can only be read into a conformal factor.
/// Component bookkeeping: a symmetric 4×4 metric has 10 components, a conformal metric has 1 function, so the
/// ansatz discards 9, and the primitives supply 0 of the 5 independent traceless (Weyl) components.
///
/// THE CRITERION, PROVED. In the G-group's gauge, `g = Ω²η ⟺ A = B`. (⇐) is immediate; (⇒) is two lines of
/// chain rule in area gauge, `e^(B_area) = 1 − R·A_R`, which holds iff B′ = A′. Confirmed independently by the
/// Weyl invariant: W ≈ 1e−30 for A = B, W = 0.25–15.4 for A ≠ B. **The G_029 survivor is NOT conformally
/// flat** — its deficit is −1.5x² + …, conformal to first order and never to second.
///
/// THE DECISIVE CONSEQUENCE. With A = σ fixed by G_028, `A = B` forces B = σ — the counting measure (G_031) —
/// giving γ = −1 EXACTLY, excluded by Cassini at **8.6957e4 σ** with zero deflection. So the ansatz is not
/// merely unforced: imposing it is refuted. AT's own metric form `g = ρ^(2/d)η` IS the conformal member, and so
/// predicts γ = −1.
///
/// VERDICT: **BOUNDARY** — assumed; no primitive entails it. Computed, never typed (ResearchY-G_027).
/// </summary>
public class Y_G_032_Tests : ResearchTestBase
{
    public Y_G_032_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_032_TheAnsatzFirstBecomesRequiredAtTheTensorFace()
    {
        // THE TRACE ANSWER: the first step that requires the conformal ansatz.
        var first = ConformalAssumptionAudit.FirstStepRequiringConformalAnsatz();
        Assert.Contains("TENSOR", first.Step.ToUpperInvariant());
        Assert.Contains("metric", first.Step);
        Assert.Contains("rank-2", first.What);
        Assert.Equal(3, ConformalAssumptionAudit.FirstStepRequiringConformalAnsatzIndex());

        // The prefix is metric-free: Difference → Counting and Counting → ρ need no ansatz at all.
        Assert.Equal(2, ConformalAssumptionAudit.MetricFreePrefixLength());
        var chain = ConformalAssumptionAudit.Chain();
        Assert.False(chain[0].RequiresConformalAnsatz);   // Difference → Counting: bookkeeping only
        Assert.False(chain[1].RequiresConformalAnsatz);   // Counting → ρ: the SCALAR face needs no η
        Assert.True(chain[2].RequiresConformalAnsatz);    // ρ → metric: the TENSOR face requires η

        // The step is ASSUMED — not derived. That is the whole finding.
        Assert.Equal(MeasureProvenance.Assumed, first.Provenance);
        Assert.Equal(MeasureProvenance.Derived, chain[0].Provenance);
        Assert.Equal(MeasureProvenance.Derived, chain[1].Provenance);
        // ...and every step cites its basis (G_027's rule).
        Assert.All(chain, s => Assert.False(string.IsNullOrWhiteSpace(s.Basis)));
        // The steps AFTER the import inherit it, so they are derived only WITHIN the assumption.
        Assert.True(chain[3].RequiresConformalAnsatz);
        Assert.Equal(MeasureProvenance.Derived, chain[3].Provenance);
    }

    [Fact]
    public void Y_G_032_TheCriterionIsEqualityOfTheTwoExponents()
    {
        // g = Ω²η ⟺ A = B, in the G-group's gauge.
        Assert.True(ConformalAssumptionAudit.IsConformalFlat(0.0, 0.0));
        Assert.True(ConformalAssumptionAudit.IsConformalFlat(-0.25, -0.25));
        Assert.False(ConformalAssumptionAudit.IsConformalFlat(-0.25, -0.125));

        // The area-gauge form agrees EXACTLY with the exponent-equality form (401 × 401 sweep, 0 mismatches).
        Assert.Equal(0, ConformalAssumptionAudit.CriterionEquivalenceMismatches());
        Assert.Equal(0.0, ConformalAssumptionAudit.AreaGaugeCriterionResidual(1.0, 0.3, 0.3), 12);
        Assert.NotEqual(0.0, ConformalAssumptionAudit.AreaGaugeCriterionResidual(1.0, 0.3, 0.4), 6);

        // THE G_029 SURVIVOR IS NOT CONFORMALLY FLAT: its deficit expands as −1.5x² + (13/6)x³ − …
        Assert.True(ConformalAssumptionAudit.SurvivorB(0.247002)
                    != ConformalAssumptionAudit.ConformalMemberB(0.247002));
        Assert.Equal(-1.5, ConformalAssumptionAudit.LeadingDeficitCoefficient(), 4);
        foreach (double x in new[] { 1.0e-6, 4.0e-6, 1.0e-3 })
        {
            double deficit = ConformalAssumptionAudit.ConformityDeficit(x);
            double expansion = -1.5 * x * x + (13.0 / 6.0) * x * x * x;
            Assert.True(Math.Abs(deficit - expansion) < 1.0e-5 * Math.Abs(expansion),
                $"x = {x}: deficit {deficit:E9} must match the expansion {expansion:E9}");
            Assert.True(deficit < 0.0, $"x = {x}: the survivor lies outside the conformal class");
        }
        // Conformal to FIRST order, violated at SECOND: the ratio deficit/x² tends to −1.5, not 0.
        Assert.Equal(-1.5, ConformalAssumptionAudit.ConformityDeficit(1.0e-6) / 1.0e-12, 5);
        // At x = 1e-3 the cubic has entered: deficit/x² = −1.5 + (13/6)x − (29/8)x² = −1.4978340.
        Assert.Equal(-1.5, ConformalAssumptionAudit.ConformityDeficit(1.0e-3) / 1.0e-6, 2);
        Assert.Equal(13.0 / 6.0,
            (ConformalAssumptionAudit.ConformityDeficit(1.0e-5) / 1.0e-10 + 1.5) / 1.0e-5, 3);
    }

    [Fact]
    public void Y_G_032_TheThreeNamesAreOneCondition()
    {
        // With A = σ fixed, conformality / the counting measure / B = σ are ONE condition.
        Assert.Equal(0, ConformalAssumptionAudit.ThreeNameMismatches());

        // Deterministic spot-confirmations: the B solving the counting measure IS the B conformality demands.
        foreach (double x in new[] { 1.0e-6, 4.0e-6, 0.01, 0.247002, 1.0, 3.0 })
        {
            double a = ConformalAssumptionAudit.AOf(x);
            double bCounting = Math.Log(ConformalAssumptionAudit.RhoOf(x)) / 3.0;
            Assert.Equal(a, bCounting, 12);
            Assert.Equal(ConformalAssumptionAudit.Sigma(x), bCounting, 12);
            Assert.Equal(ConformalAssumptionAudit.RhoOf(x),
                ConformalAssumptionAudit.CountingMeasure(bCounting), 9);
        }
    }

    [Fact]
    public void Y_G_032_CountingIsADeterminantConditionNotAShapeCondition()
    {
        // √det g_ij = ρ fixes ONE combination (the product of the eigenvalues); the SHAPE is separate.
        // Two 3-metrics with identical √det = ρ but different conformal status:
        //   isotropic diag(ρ^(2/3), ρ^(2/3), ρ^(2/3))  — conformal to flat at every ρ
        //   anisotropic diag(ρ², 1, 1)                 — same determinant, conformal ONLY at ρ = 1
        foreach (double rho in new[] { 1.0e-3, 0.5, 0.610136, 2.0, 1.0e3 })
        {
            var c = ConformalAssumptionAudit.ShapeCounterexampleFor(rho);
            Assert.Equal(rho, c.SqrtDetIso, 9);                  // same counting measure…
            Assert.Equal(rho, c.SqrtDetAnisotropic, 9);
            Assert.True(c.IsoIsConformal);                        // …conformal
            Assert.False(c.AnisotropicIsConformal);               // …but NOT conformal
            Assert.NotEqual(0.0, c.AnisotropySpread, 12);
        }

        // The single degenerate point: ρ = 1 is flat, so both are conformal and the counterexample dissolves.
        var flat = ConformalAssumptionAudit.ShapeCounterexampleFor(1.0);
        Assert.True(flat.AnisotropicIsConformal);
        Assert.Equal(0.0, flat.AnisotropySpread, 12);

        // Therefore the counting measure CANNOT entail conformal flatness: same determinant, different status.
        Assert.Equal(5, ConformalAssumptionAudit.ShapeCounterexamples()
            .Count(c => c.Rho != 1.0 && !c.AnisotropicIsConformal));
    }

    [Fact]
    public void Y_G_032_NoPrimitiveForcesConformalFlatness()
    {
        // NO primitive entails it. η is excluded: it IS the conformal statement, so citing it restates the
        // assumption rather than deriving it.
        Assert.Empty(ConformalAssumptionAudit.ForcingPrimitives());
        Assert.False(ConformalAssumptionAudit.AnyPrimitiveForcesConformalFlatness());

        // ...but η IS in the inventory, and it is the conformal reference (QG77/QG289: a TRUE THEORY INPUT).
        Assert.True(ConformalAssumptionAudit.ConformalityIsItselfAPrimitive());
        var eta = ConformalAssumptionAudit.Primitives().Single(p => p.IsConformalReference);
        Assert.Equal(2, eta.Rank);
        Assert.Contains("conformal reference", eta.Primitive.ToLowerInvariant());

        // Every OTHER primitive is rank 0 — scalar content can only enter a conformal factor.
        Assert.All(ConformalAssumptionAudit.Primitives().Where(p => !p.IsConformalReference),
            p => Assert.Equal(0, p.Rank));

        // Component accounting: 10 → 1, discarding 9; the primitives supply 0 of the 5 traceless components.
        var (metric, conformal, discarded, scalars, supplied, independent) =
            ConformalAssumptionAudit.ComponentAccounting();
        Assert.Equal(10, metric);
        Assert.Equal(1, conformal);
        Assert.Equal(9, discarded);
        Assert.Equal(2, scalars);          // ρ and ψ — both SCALARS
        Assert.Equal(0, supplied);         // nothing in AT supplies a traceless part
        Assert.Equal(5, independent);      // the 5 Weyl components that would be needed
    }

    [Fact]
    public void Y_G_032_ImposingTheAnsatzGivesGammaMinusOneAndCassiniExcludesIt()
    {
        // THE DECISIVE CONSEQUENCE. Conformality + A = σ ⟹ B = σ ⟹ γ = −1 EXACTLY, at EVERY body.
        foreach (var (body, x) in ConformalAssumptionAudit.Bodies)
        {
            Assert.Equal(-1.0, ConformalAssumptionAudit.ConformalMemberGamma(x), 12);
            Assert.Equal(86957.4348, ConformalAssumptionAudit.CassiniSeparation(-1.0), 4);
            Assert.False(ConformalAssumptionAudit.ConformalValueIsAdmitted(x),
                $"{body}: γ = −1 (the conformal value) must NOT be inside Cassini's band");
        }
        Assert.False(ConformalAssumptionAudit.ConformalValueIsAdmittedAnywhere());

        // The admitted band is exact (closed-form inversion of γ), and it sits at ≈ −σ while conformality
        // demands +σ: OPPOSITE SIDES OF ZERO. So conformality must be violated by ≈ a FACTOR OF 2.
        foreach (double x in new[] { 4.0e-6, 2.1225e-6 })
        {
            var (lo, hi) = ConformalAssumptionAudit.AdmittedBand(x);
            Assert.True(lo < hi && hi > 0.0, $"x = {x}: the band must lie at positive B (≈ −σ)");
            Assert.True(ConformalAssumptionAudit.BandCentre(x) > 0.0);
            Assert.Equal(2.0, ConformalAssumptionAudit.RequiredConformalityViolation(x), 3);
        }
        // At the densest measured star the ratio is |A − B|/|A| = 1.6663535622 — still O(1), not a correction.
        Assert.Equal(1.666, ConformalAssumptionAudit.RequiredConformalityViolation(0.247002), 3);
        Assert.True(ConformalAssumptionAudit.RequiredConformalityViolation(0.247002) > 1.0);

        // Exact figures recorded by the audit (band width compared by relative agreement to avoid
        // over-constraining the last digits).
        Assert.True(Math.Abs(ConformalAssumptionAudit.BandWidth(4.0e-6) / 1.8399778826483446e-10 - 1.0) < 1.0e-12);
        Assert.True(Math.Abs(ConformalAssumptionAudit.BandWidth(2.1225e-6) / 9.7634374676449081e-11 - 1.0) < 1.0e-12);
        Assert.Equal(4.00005199926229485e-06, ConformalAssumptionAudit.BandCentre(4.0e-6), 12);
        Assert.Equal(8.000051999262295e-06,
            Math.Abs(ConformalAssumptionAudit.Sigma(4.0e-6) - ConformalAssumptionAudit.BandCentre(4.0e-6)), 11);

        // So the counterfactual is unavoidable: if the ansatz WERE entailed, AT would be refuted.
        Assert.True(ConformalAssumptionAudit.WouldBeRefutedIfEntailed());
    }

    [Fact]
    public void Y_G_032_VerdictIsBoundary()
    {
        // COMPUTED verdict, not a literal (ResearchY-G_027).
        Assert.Equal("BOUNDARY", ConformalAssumptionAudit.Verdict());

        // BOUNDARY because nothing entails it: the entailing set is empty, and the assumption is not redundant.
        Assert.False(ConformalAssumptionAudit.AnyPrimitiveForcesConformalFlatness());
        Assert.True(ConformalAssumptionAudit.ConformalityIsItselfAPrimitive());

        // The one primitive that WOULD have to do the entailing cannot: it is the assumption itself.
        string why = ConformalAssumptionAudit.PrimitiveThatWouldHaveToEntailIt();
        Assert.Contains("η", why);
        Assert.Contains("restates the assumption", why);
    }

    [Fact]
    public void Y_G_032_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_032 — Conformal Assumption Audit: is g = Ω²η derived, or only assumed?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The G-group's gauge: ds² = −e^(2A)dt² + e^(2B)·(flat 3-space).");
        sb.AppendLine("  2. A is FIXED by G_028's clock closure: A = σ = −x (so the clock sector is not in question).");
        sb.AppendLine("  3. d = 3; σ = (1/d)ln ρ; ρ = e^(−dx).");
        sb.AppendLine("  4. Cassini 2003: γ = 1.0000210 ± 2.3e−5.");
        sb.AppendLine();

        PrintHeader("1. THE TRACE — Difference → Counting → ρ → metric");
        sb.AppendLine("  step                              ansatz?   provenance        basis");
        foreach (var s in ConformalAssumptionAudit.Chain())
            sb.AppendLine($"  {s.Step,-33}{(s.RequiresConformalAnsatz ? "YES" : "no"),-10}{s.Provenance,-18}{s.Basis}");
        sb.AppendLine();
        sb.AppendLine($"  FIRST STEP REQUIRING THE ANSATZ: #{ConformalAssumptionAudit.FirstStepRequiringConformalAnsatzIndex()}"
                      + $" — {ConformalAssumptionAudit.FirstStepRequiringConformalAnsatz().Step}");
        sb.AppendLine($"  metric-free prefix: {ConformalAssumptionAudit.MetricFreePrefixLength()} steps "
                      + "(Difference → Counting and Counting → ρ need no ansatz at all)");
        sb.AppendLine();

        PrintHeader("2. THE CRITERION — g = Ω²η ⟺ A = B");
        sb.AppendLine("  (⇐) A = B ⟹ g = e^(2A)(−dt² + δ) = e^(2A)η.");
        sb.AppendLine("  (⇒) in area gauge: e^(B_area) = 1/(1 + rB′) and R·A_R = rA′/(1 + rB′),");
        sb.AppendLine("      so the criterion holds iff B′ = A′, i.e. B = A by asymptotic flatness.");
        sb.AppendLine($"  executed equivalence sweep: mismatches = {ConformalAssumptionAudit.CriterionEquivalenceMismatches()}");
        sb.AppendLine("  independent Weyl confirmation (W = C_abcd C^abcd):");
        sb.AppendLine("      A = B = −c/r        →  W ≈ 1.0e−30   (conformal)");
        sb.AppendLine("      A = B = −c/r²       →  W ≈ 1.6e−29   (conformal)");
        sb.AppendLine("      A = −c/r, B = A/2   →  W = 0.6937    (NOT conformal)");
        sb.AppendLine("      A = 0,    B = −c/r  →  W = 15.39     (NOT conformal)");
        sb.AppendLine($"  G_029 survivor deficit at x = 1e−6: {ConformalAssumptionAudit.ConformityDeficit(1.0e-6):E6}"
                      + $"  → leading coefficient {ConformalAssumptionAudit.LeadingDeficitCoefficient():F6}");
        sb.AppendLine("  THE SURVIVOR IS CONFORMAL TO FIRST ORDER ONLY (−1.5x² + …).");
        sb.AppendLine();

        PrintHeader("3. DOES ANY PRIMITIVE FORCE IT?");
        sb.AppendLine("  primitive                                rank   entails shape?   conformal reference?");
        foreach (var p in ConformalAssumptionAudit.Primitives())
            sb.AppendLine($"  {p.Primitive,-41}{p.Rank,-7}{(p.EntailsConformalShape ? "yes" : "no"),-17}{(p.IsConformalReference ? "YES" : "no")}");
        sb.AppendLine($"  forcing primitives (η excluded as self-referential): {ConformalAssumptionAudit.ForcingPrimitives().Length} → ANSWER: NO.");
        sb.AppendLine();
        var (metric, conformal, discarded, scalars, supplied, independent) = ConformalAssumptionAudit.ComponentAccounting();
        sb.AppendLine($"  COMPONENT ACCOUNTING: symmetric 4×4 metric {metric} components; conformal metric {conformal} function;");
        sb.AppendLine($"      the ansatz DISCARDS {discarded}; AT's scalar content {scalars}; traceless components supplied {supplied} of {independent}.");
        sb.AppendLine();

        PrintHeader("4. COUNTING IS A DETERMINANT CONDITION, NOT A SHAPE CONDITION");
        sb.AppendLine("  ρ          √det(iso)     √det(aniso)   iso conformal?  aniso conformal?");
        foreach (var c in ConformalAssumptionAudit.ShapeCounterexamples())
            sb.AppendLine($"  {c.Rho,-10:F6} {c.SqrtDetIso,-14:F9} {c.SqrtDetAnisotropic,-14:F9} "
                        + $"{(c.IsoIsConformal ? "yes" : "no"),-15}{(c.AnisotropicIsConformal ? "yes" : "no")}");
        sb.AppendLine("  SAME determinant, DIFFERENT conformal status — so the counting measure cannot entail conformality.");
        sb.AppendLine($"  three-name equivalence (conformal / counting / B = σ): mismatches = {ConformalAssumptionAudit.ThreeNameMismatches()}");
        sb.AppendLine();

        PrintHeader("5. THE DECISIVE CONSEQUENCE — imposing the ansatz is refuted");
        sb.AppendLine("  body              x            γ(conformal)   Cassini sep (σ)      band centre        |A−B|/|A|");
        foreach (var (body, x) in ConformalAssumptionAudit.Bodies)
            sb.AppendLine($"  {body,-17}{x,-13:G6}{ConformalAssumptionAudit.ConformalMemberGamma(x),-15:F12}"
                        + $"{ConformalAssumptionAudit.CassiniSeparation(-1.0),17:F4}   "
                        + $"{ConformalAssumptionAudit.BandCentre(x),17:E6}   "
                        + $"{ConformalAssumptionAudit.RequiredConformalityViolation(x),9:F6}");
        sb.AppendLine($"  is γ = −1 (the conformal value) ever admitted? {ConformalAssumptionAudit.ConformalValueIsAdmittedAnywhere()} — NO, at every body.");
        sb.AppendLine("  The band sits at ≈ −σ while conformality demands +σ: OPPOSITE SIDES OF ZERO, violated by a");
        sb.AppendLine("  factor of two — not a small correction to be finessed.");
        sb.AppendLine("  AT's own metric form g = ρ^(2/d)η IS the conformal member, so it predicts γ = −1: zero light");
        sb.AppendLine("  deflection and zero Shapiro delay. This is G_030's dilemma traced to its primitive root.");
        sb.AppendLine();

        PrintHeader("6. VERDICT");
        sb.AppendLine($"  {ConformalAssumptionAudit.Verdict()} — assumed; no existing AT primitive entails it.");
        sb.AppendLine("  • It enters at the ρ → metric step (the TENSOR face), per QG285/QG288, and the imported object");
        sb.AppendLine("    is η — which QG289's anchor inventory already calls a TRUE THEORY INPUT.");
        sb.AppendLine("  • Nothing entails it from outside: every other primitive is rank 0 (ρ, ψ, d, π).");
        sb.AppendLine("  • Imposing it forces B = σ, hence γ = −1, excluded at 8.6957e4 σ. So the ansatz is not merely");
        sb.AppendLine("    unforced — it is untenable at face value.");
        sb.AppendLine();
        sb.AppendLine("  HONEST NOTE: the repository describes η as 'part of the geometry, not a choice'. This audit");
        sb.AppendLine("  agrees it is not EMPIRICAL and not fitted — but 'input' and 'derived' are opposites, and the");
        sb.AppendLine("  entailing set is empty. That is what BOUNDARY records.");
        Output.WriteLine(sb.ToString());
    }
}
