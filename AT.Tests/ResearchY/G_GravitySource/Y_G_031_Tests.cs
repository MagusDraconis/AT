using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_031 — Spatial-Origin Audit (group G — Gravity Source).
///
/// QUESTION. Exactly where does `B = σ` enter the theory?
///
/// THE ANSWER. **It does not enter anywhere as a separate assumption.** Write the conformal ansatz with an
/// arbitrary exponent n:
///     g_μν = ρ^(2n)η  ⟹  A = B = n·ln ρ = n·d·σ ;  √(−g₀₀) = ρ^n ;  √det g_ij = ρ^(n·d)
/// Then in d = 3:
///     **n = 1/d  ⟺  √(−g₀₀) = ρ^(1/d)  ⟺  √det g_ij = ρ**
/// **The clock law and the counting measure are THE SAME EQUATION** — two readings of one exponent choice,
/// read off two slots that conformal flatness (A = B) has already made equal. There is no "spatial measure"
/// step in the chain to be found or removed.
///
/// THIS RENARRATES G_023, which said AT "pins k = 0 twice (the clock law forces A = σ; the counting measure
/// forces B = σ)". There are not two pins: conformal flatness supplies A = B, and ONE exponent choice supplies
/// both values.
///
/// PROVENANCE: ρ is DERIVED (scalar face of Difference); the conformal ansatz and its exponent are ASSUMED
/// (the exponent IS the clock law, anchored by G_004/G_009); the counting measure is DERIVED; the reading of ρ
/// as a geometric volume is a CORRESPONDENCE.
///
/// VERDICT: REQUIRED — it cannot be removed alone, and five places consume it.
/// </summary>
public class Y_G_031_Tests : ResearchTestBase
{
    public Y_G_031_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_031_TheClockLawAndTheCountingMeasureAreOneEquation()
    {
        // THE PROVENANCE THEOREM: n = 1/d ⟺ clock law ⟺ counting measure. Exhaustive sweep, deterministic.
        Assert.Equal(0, SpatialOriginAudit.EquivalenceMismatches());
        Assert.True(SpatialOriginAudit.CountingMeasureIsEntailed());

        // The excluded point: at ρ = 1 the metric is flat and EVERY exponent agrees — a degeneracy that
        // carries no information about the equivalence (which is why the sweep skips it).
        Assert.True(SpatialOriginAudit.FlatSpaceIsDegenerate());
        Assert.True(SpatialOriginAudit.ClockLawHolds(1.0, 1.0), "flat space satisfies the clock law for any exponent");
        Assert.True(SpatialOriginAudit.CountingMeasureHolds(1.0, 1.0));

        // Spot-confirm at the exponent AT uses, across the whole density range.
        double n = SpatialOriginAudit.RequiredExponent;
        foreach (double rho in new[] { 1.0e-3, 0.5, 0.610136, 1.0, 2.0, 1.0e3 })
        {
            Assert.True(SpatialOriginAudit.ClockLawHolds(rho, n), $"ρ = {rho}: clock law must hold");
            Assert.True(SpatialOriginAudit.CountingMeasureHolds(rho, n), $"ρ = {rho}: counting measure must hold");
        }

        // ...and any OTHER exponent breaks BOTH at once — they are not separable.
        foreach (double wrong in new[] { 0.5, 1.0, 0.2, 0.4 })
        {
            if (Math.Abs(wrong - n) < 1.0e-12) continue;
            Assert.False(SpatialOriginAudit.ClockLawHolds(1.5, wrong), $"n = {wrong}: clock law must fail");
            Assert.False(SpatialOriginAudit.CountingMeasureHolds(1.5, wrong), $"n = {wrong}: measure must fail");
        }
    }

    [Fact]
    public void Y_G_031_BCannotBeRemovedWithoutLeavingConformalFlatness()
    {
        // No exponent other than 1/d keeps the clock law — so B = σ cannot be dropped on its own.
        Assert.False(SpatialOriginAudit.RemovableWithoutLeavingConformalFlatness());

        // The two exponents that matter, side by side.
        double rho = 0.610136;
        Assert.True(SpatialOriginAudit.CountingMeasureHolds(rho, 1.0 / 3.0));
        Assert.Equal(rho, SpatialOriginAudit.SpatialMeasure(rho, 1.0 / 3.0), 12);
        Assert.Equal(Math.Pow(rho, 1.0 / 3.0), SpatialOriginAudit.Clock(rho, 1.0 / 3.0), 12);

        // With n = 1 the volume exponent is n·d = 3, not 1: the measure would be ρ³.
        Assert.Equal(3.0, SpatialOriginAudit.CaseFor(rho, 1.0).VolumeExponent, 12);
        Assert.False(SpatialOriginAudit.CountingMeasureHolds(rho, 1.0));
    }

    [Fact]
    public void Y_G_031_ProvenanceOfEveryStepIsRecorded()
    {
        var chain = SpatialOriginAudit.Chain();
        Assert.Equal(5, chain.Length);

        // ρ is DERIVED; the ansatz and the exponent are ASSUMED; the measure is DERIVED; the volume reading is
        // a CORRESPONDENCE.
        Assert.Equal(MeasureProvenance.Derived, chain[0].Provenance);          // Difference → Density
        Assert.Equal(MeasureProvenance.Assumed, chain[1].Provenance);          // Density → Metric
        Assert.Equal(MeasureProvenance.Assumed, chain[2].Provenance);          // the exponent
        Assert.Equal(MeasureProvenance.Derived, chain[3].Provenance);          // Metric → Measure
        Assert.Equal(MeasureProvenance.Correspondence, chain[4].Provenance);   // volume reading

        // Every step must cite its basis (ResearchY-G_027's rule).
        Assert.All(chain, s => Assert.False(string.IsNullOrWhiteSpace(s.Basis), $"{s.Step} must cite a basis"));

        // THE HEADLINE: the spatial measure is DERIVED, not assumed — there is no separate postulate to find.
        Assert.Equal(MeasureProvenance.Derived, chain.Single(s => s.Step.Contains("Spatial measure")).Provenance);
    }

    [Fact]
    public void Y_G_031_TheCountingMeasureIsLoadBearing()
    {
        // Five places consume it; four of them break if √det g_ij ≠ ρ.
        Assert.True(SpatialOriginAudit.AnyLoadBearingUse());
        Assert.Equal(5, SpatialOriginAudit.LoadBearingUseCount());

        var uses = SpatialOriginAudit.Uses();
        Assert.Contains(uses, u => u.Where.Contains("count conservation") && u.Usage == MeasureUsage.LoadBearing);
        Assert.Contains(uses, u => u.Where.Contains("entropy") && u.Usage == MeasureUsage.LoadBearing);
        Assert.Contains(uses, u => u.Where.Contains("density parameters") && u.Usage == MeasureUsage.LoadBearing);
        // ...and two do NOT (they read ρ without its volume reading).
        Assert.Equal(2, uses.Count(u => u.Usage == MeasureUsage.Neutral));
        Assert.All(uses, u => Assert.False(string.IsNullOrWhiteSpace(u.Basis)));
    }

    [Fact]
    public void Y_G_031_WhatBreaksIsQuantified()
    {
        // The geometric-to-count ratio for the surviving alternative measure (G_029's γ = +1 member).
        Assert.Equal(1.000012735, SpatialOriginAudit.GeometricOverCountRatio(2.1225e-6), 9);
        Assert.Equal(1.000600120, SpatialOriginAudit.GeometricOverCountRatio(1.0e-4), 9);
        Assert.Equal(1.733052388, SpatialOriginAudit.GeometricOverCountRatio(0.1), 9);
        Assert.Equal(3.437584871, SpatialOriginAudit.GeometricOverCountRatio(0.247002), 9);
        Assert.Equal(51.142808724, SpatialOriginAudit.GeometricOverCountRatio(1.0), 9);

        // Every listed breakage carries that magnitude — the count would be off by 3.44× at J0740+6620.
        var brk = SpatialOriginAudit.Breakage();
        Assert.Equal(4, brk.Length);
        Assert.All(brk, b => Assert.Equal(3.437584871 - 1.0, b.Magnitude, 8));
        Assert.All(brk, b => Assert.False(string.IsNullOrWhiteSpace(b.What)));
    }

    [Fact]
    public void Y_G_031_VerdictIsRequired()
    {
        // COMPUTED verdict, not a literal (ResearchY-G_027).
        Assert.Equal("REQUIRED", SpatialOriginAudit.Verdict());

        // It is REQUIRED because it is entailed (not removable alone) AND consumed.
        Assert.True(SpatialOriginAudit.CountingMeasureIsEntailed());
        Assert.True(SpatialOriginAudit.AnyLoadBearingUse());

        // The alternative exists but is NOT removable within the ansatz: it requires B ≠ σ, i.e. A ≠ B,
        // i.e. leaving conformal flatness — the traceless face ψ (not a new primitive TYPE, but an
        // independent degree of freedom the theory does not constrain).
        double x = 0.247002;
        double bSurvivor = SpatialOriginAudit.SurvivorB(x);
        Assert.True(bSurvivor > 0.0);
        Assert.NotEqual(-x, bSurvivor, 6);                       // B ≠ σ
        Assert.Equal(2.0 - Math.Exp(-2.0 * x), Math.Exp(2.0 * bSurvivor), 12);
    }

    [Fact]
    public void Y_G_031_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_031 — Spatial-Origin Audit: exactly where does B = σ enter?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The conformal ansatz with an arbitrary exponent: g_μν = ρ^(2n)η.");
        sb.AppendLine("  2. Then A = B = n·ln ρ = n·d·σ, so conformal flatness (A = B) is built in and only n is free.");
        sb.AppendLine("  3. d = 3; σ = (1/d)ln ρ.");
        sb.AppendLine();

        PrintHeader("1. THE FIRST DERIVATION (Difference → Density → Metric → Measure)");
        sb.AppendLine("  step                        provenance        basis");
        foreach (var s in SpatialOriginAudit.Chain())
            sb.AppendLine($"  {s.Step,-27} {s.Provenance,-16} {s.Basis}");
        sb.AppendLine();

        PrintHeader("2. THE PROVENANCE THEOREM (exhaustive, 401 × 401 exponent × density pairs)");
        sb.AppendLine("  g_μν = ρ^(2n)η   ⟹   A = B = n·ln ρ ;  √(−g₀₀) = ρ^n ;  √det g_ij = ρ^(n·d)");
        sb.AppendLine("  ∴  n = 1/d   ⟺   √(−g₀₀) = ρ^(1/d)   ⟺   √det g_ij = ρ");
        sb.AppendLine($"  mismatches: {SpatialOriginAudit.EquivalenceMismatches()}   "
                      + $"counting measure entailed: {SpatialOriginAudit.CountingMeasureIsEntailed()}");
        sb.AppendLine("  THE CLOCK LAW AND THE COUNTING MEASURE ARE THE SAME EQUATION.");
        sb.AppendLine();

        PrintHeader("3. WHERE IT IS ACTUALLY CONSUMED");
        foreach (var u in SpatialOriginAudit.Uses())
            sb.AppendLine($"  {u.Usage,-12} {u.Where,-46} {u.Basis}");
        sb.AppendLine($"  load-bearing: {SpatialOriginAudit.LoadBearingUseCount()}   "
                      + $"verdict: {SpatialOriginAudit.Verdict()}");
        sb.AppendLine();

        PrintHeader("4. WHAT BREAKS IF B ≠ σ (the geometric-to-count ratio)");
        foreach (var (body, x) in new[] { ("Sun", 2.1225e-6), ("x=1e-4", 1.0e-4), ("x=0.1", 0.1), ("J0740+6620", 0.247002), ("x=1", 1.0) })
            sb.AppendLine($"  {body,-12} √det g_ij / ρ = {SpatialOriginAudit.GeometricOverCountRatio(x),14:F9}"
                        + $"   count error = {SpatialOriginAudit.CountError(x):E3}");
        sb.AppendLine();
        foreach (var (what, mag) in SpatialOriginAudit.Breakage())
            sb.AppendLine($"  BREAKS: {what}  [{mag:P1} at J0740+6620]");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine("  REQUIRED — and B = σ was never a separate assumption to remove.");
        sb.AppendLine("  • It is ENTAILED: B = σ follows from conformal flatness (A = B) plus the clock law, and the");
        sb.AppendLine("    clock law IS the same exponent choice. Removing it alone is impossible.");
        sb.AppendLine("  • Removal requires leaving conformal flatness (A ≠ B) — the traceless face ψ. ψ is not a new");
        sb.AppendLine("    primitive TYPE (G_024), but it is an independent degree of freedom the theory does not");
        sb.AppendLine("    constrain, so the metric becomes underdetermined (G_030).");
        sb.AppendLine("  • It is LOAD-BEARING in five places; four break by the ratios above.");
        sb.AppendLine();
        sb.AppendLine("  G_030's dilemma is therefore SHARPER than stated: not 'counting measure versus optics' but");
        sb.AppendLine("  'CONFORMAL FLATNESS PLUS THE CLOCK LAW versus optics'.");
        Output.WriteLine(sb.ToString());
    }
}
