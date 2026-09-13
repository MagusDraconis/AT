using System.Text;
using AT.Core.ResearchXH;
using AT.Core;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_036 — Temporal Core Test Audit (group G — Gravity Source).
///
/// QUESTION. Can the surviving temporal core — ρ → g₀₀ → clock rate → acceleration — be tested **without any
/// spatial metric**, using only g₀₀, the clock law and the source law? Search clock gradients, redshift-only
/// observables, compact-object timing and pulsar timing. Goal: find the FIRST experiment that probes the
/// temporal core alone.
///
/// G_035 proved the core is **independent**; G_036 asks whether it is **testable**. Answer: a sharp
/// **BOUNDARY**, and the reason is **not the signal**.
///
///  (1) THE OBSERVABLE IS PURE. `z = 1/√(−g₀₀) − 1` needs **g₀₀ alone** — no γ, no lensing, no Shapiro delay,
///      no spatial metric. AT and GR agree at O(x) and split at **O(x²)**.
///  (2) THE SIGNAL IS LARGE AT COMPACT OBJECTS. Δz = **0.125628** at J0740+6620 (45 % of z_AT), reproducing
///      G_020's published figures exactly.
///  (3) THE ARENA IS NEUTRON STARS ONLY. Terrestrial clocks, the Sun and binary pulsars would need σ_z between
///      1e−18 and 1e−12 — nine to twelve orders of magnitude out of reach.
///  (4) **THE BOTTLENECK IS THE COMPACTNESS, NOT THE REDSHIFT.** σ_x/x = 13.73 % against 3.661 % needed — and
///      for a neutron star the compactness comes from **pulse-profile modelling, which fits light bending**,
///      i.e. the spatial sector G_032 showed to be an assumed primitive. The observable is pure; the number
///      required to read it is not.
///  (5) A PURE ROUTE EXISTS AND IS SHORT: `R_∞ = R/√(1−2x)` from thermal flux plus parallax distance is a
///      g₀₀ effect (photon energy and arrival rate, not bending), so {z, R_∞} solves {M, R} with no bending.
///      Distance and atmosphere systematics keep σ_x/x above the ~3.7 % needed — about a factor of three short.
///  (6) THE FIRST EXPERIMENT is **Pound & Rebka (1960)**, x = 2.45e−15, purely g₀₀ — but it probes only the
///      first-order term, the one AT shares with GR. **No experiment has yet probed the core's distinctive
///      content.**
/// </summary>
public class Y_G_036_Tests : ResearchTestBase
{
    public Y_G_036_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_036_TheObservableIsPureAndTheSplitIsSecondOrder()
    {
        // THE ARITY PROOF: every temporal-core observable takes only the compactness x — no B, no gamma, no psi.
        Assert.True(TemporalCoreTestAudit.AllObservablesArePure());
        Assert.All(TemporalCoreTestAudit.PureObservables(), o => Assert.Single(o.Signature.Split(',')));

        // Both laws begin at x, so first order they agree; the split is at O(x²).
        Assert.Equal(2, TemporalCoreTestAudit.FirstDiscriminatingOrder());
        Assert.Equal((0.5, 1.5), TemporalCoreTestAudit.SecondOrderCoefficients());

        // Verify the expansion numerically: the O(x) terms agree, and the O(x²) coefficients are ½ (AT) and 3⁄2 (GR).
        // Tested at 1e−6…1e−4: above the cancellation floor, and small enough that the x³ terms stay below 1e−3.
        foreach (double x in new[] { 1.0e-6, 1.0e-5, 1.0e-4 })
        {
            Assert.Equal(1.0, TemporalCoreTestAudit.RedshiftAT(x) / x, 3);
            Assert.Equal(1.0, TemporalCoreTestAudit.RedshiftGR(x) / x, 3);
            Assert.Equal(0.5, (TemporalCoreTestAudit.RedshiftAT(x) / x - 1.0) / x, 3);
            Assert.Equal(1.5, (TemporalCoreTestAudit.RedshiftGR(x) / x - 1.0) / x, 3);
            Assert.Equal(1.0, TemporalCoreTestAudit.Discriminator(x) / (x * x), 3);
        }

        // THE WEAK-FIELD SPLIT IS NOT MERELY UNMEASURABLE — IT IS UNREPRESENTABLE. Each law passes through an
        // intermediate ≈ 1 and so carries ~1 ulp = 2.2e−16 of absolute error, while the true split at the
        // Pound–Rebka compactness is 6.0e−30: fourteen orders below the noise floor.
        double tiny = 2.45e-15;
        Assert.Equal(1.0, TemporalCoreTestAudit.Discriminator(tiny) / (tiny * tiny), 6);   // the series: Δz = x²
        Assert.True(TemporalCoreTestAudit.Discriminator(tiny) < TemporalCoreTestAudit.WeakFieldRoundingFloor() * 1.0e-10);

        // …and the direct subtraction cannot see it: it returns ~7.5e−18 of rounding noise (wrong SIGN as well as
        // wrong magnitude) where the truth is 6.0e−30.
        Assert.True(Math.Abs(TemporalCoreTestAudit.DirectDiscriminator(tiny)) > 1.0e-19,
            "the direct difference must be dominated by rounding noise");
        Assert.True(Math.Abs(TemporalCoreTestAudit.DirectDiscriminator(tiny) - TemporalCoreTestAudit.Discriminator(tiny)) > 1.0e-19,
            "the direct difference must not reproduce the series");

        // At x = 1.1e−18 the GR redshift is annihilated outright: 1 − 2x rounds to 1.0 and z_GR returns exactly 0.
        Assert.Equal(0.0, TemporalCoreTestAudit.RedshiftGR(1.1e-18));
        Assert.True(TemporalCoreTestAudit.RedshiftAT(1.1e-18) > 0.0);

        // RULE 4 IN ACTION. The naive `exp(x) - 1` carries ~1 ulp of 1.0 of ABSOLUTE error, so its relative
        // error is ~1.1e−16/x: ~0.3 % at the Pound–Rebka compactness, and total (it returns exactly 0.0) at
        // optical-clock compactness — precisely where the laboratory regime is most precisely probed.
        Assert.Equal(0.0, Math.Exp(1.0e-18) - 1.0);                       // annihilation
        Assert.Equal(1.0, AtNumerics.ExpM1(1.0e-18) / 1.0e-18, 6);        // the guarded form survives
        Assert.True(Math.Abs((Math.Exp(2.45e-15) - 1.0) - 2.45e-15) / 2.45e-15 > 1.0e-4,
            "the naive form must lose at least 0.01 % at the Pound–Rebka compactness");
        Assert.Equal(1.0, TemporalCoreTestAudit.RedshiftAT(2.45e-15) / 2.45e-15, 12);

        // G_020's published figures, reproduced exactly.
        Assert.Equal(0.1256, TemporalCoreTestAudit.Discriminator(0.247002), 4);
        Assert.Equal(0.206812, TemporalCoreTestAudit.RedshiftAT(0.187982), 6);
        Assert.Equal(0.265888, TemporalCoreTestAudit.RedshiftGR(0.187982), 6);
    }

    [Fact]
    public void Y_G_036_TheArenaIsNeutronStarsOnly()
    {
        // The arena threshold climbs with precision: |Δz| > σ_z requires x above.
        Assert.Equal(0.030494, TemporalCoreTestAudit.RequiredCompactness(1.0e-3), 5);
        Assert.Equal(0.115094, TemporalCoreTestAudit.RequiredCompactness(0.017766), 5);
        Assert.Equal(0.133350, TemporalCoreTestAudit.RequiredCompactness(0.025125), 5);

        // Every weak-field arena: the split is exactly x², so the FRACTIONAL split is exactly x — 1e−15 to 1e−4.
        foreach (double x in new[] { 2.45e-15, 1.1e-18, 2.12e-6, 1.0e-6, 1.0e-4 })
        {
            Assert.Equal(1.0, TemporalCoreTestAudit.Discriminator(x) / (x * x), 3);
            Assert.True(TemporalCoreTestAudit.SignalFraction(x) < 1.5 * x,
                $"x = {x}: the fractional split must be ≈ x");
        }

        // ...and the strong field clears it by a wide margin.
        Assert.True(Math.Abs(TemporalCoreTestAudit.Discriminator(0.247002)) > 0.1);
        Assert.Equal(0.4484, TemporalCoreTestAudit.SignalFraction(0.247002), 4);

        // Only the compact-object candidates discriminate at all.
        var disc = TemporalCoreTestAudit.DiscriminatingCandidates();
        Assert.All(disc, c => Assert.True(c.Compactness > 0.1, $"{c.Example} must be a compact object"));
        Assert.All(TemporalCoreTestAudit.Candidates().Where(c => c.Compactness < 0.01),
            c => Assert.False(c.Discriminating, $"{c.Example} must not discriminate"));
    }

    [Fact]
    public void Y_G_036_TheBottleneckIsTheCompactnessNotTheRedshift()
    {
        // G_020's budget: 13.73 % against 3.661 % needed — a factor of ~3.75×.
        Assert.Equal(0.03661, TemporalCoreTestAudit.RequiredSigmaXOverX(), 5);
        Assert.Equal(0.1373, TemporalCoreTestAudit.CurrentSigmaXOverX(), 4);
        Assert.Equal(3.75034, TemporalCoreTestAudit.SigmaXImprovementFactor(), 4);
        Assert.Equal(1.334, TemporalCoreTestAudit.CurrentSignificance(), 3);

        // THE CENTRAL FINDING: the compactness side splits.
        //  - exactly one candidate is impure outright (pulse-profile modelling fits light bending);
        //  - exactly one is PURE as an observable but IMPORTS its compactness (the NS redshift);
        //  - exactly one is pure in BOTH senses and reaches the strong field (the R_inf route).
        var impure = TemporalCoreTestAudit.ImpureCandidates();
        Assert.Single(impure);
        Assert.Contains("LIGHT BENDING", impure[0].Impurity);
        Assert.Contains("pulse", impure[0].Example);

        var imported = TemporalCoreTestAudit.PureObservableImpureCompactness();
        Assert.Single(imported);
        Assert.Contains("IMPORTED", imported[0].Impurity);
        Assert.Contains("J0740", imported[0].Example);

        var fullyPure = TemporalCoreTestAudit.FullyPureCandidates();
        Assert.Contains(fullyPure, c => c.Compactness == 0.247002 && c.Example.Contains("R_∞"));

        // The pure strong-field route exists: {z, R_inf} needs no bending.
        var pureStrong = fullyPure.Where(c => c.Compactness > 0.1).ToArray();
        Assert.Single(pureStrong);
        Assert.Contains("R_∞", pureStrong[0].Observable);
        Assert.Contains("g₀₀ effect", pureStrong[0].Note);

        // R_inf = R/sqrt(1-2x) is a g00 relation: invertible from a measured z with no bending.
        double r = 11.0, x = 0.187982;
        double apparent = TemporalCoreTestAudit.ApparentRadius(r, x);
        double z = TemporalCoreTestAudit.RedshiftGR(x);
        Assert.Equal(r, TemporalCoreTestAudit.TrueRadiusFrom(apparent, z), 9);
    }

    [Fact]
    public void Y_G_036_TheFirstProbeAndTheFrontier()
    {
        // THE FIRST EXPERIMENT to probe the temporal core at all — and it is pure g₀₀ with no light bending.
        var (experiment, year, x, what, verdict) = TemporalCoreTestAudit.FirstProbe();
        Assert.Equal(1960, year);
        Assert.Contains("Pound", experiment);
        Assert.Equal(1.0, x / 2.45e-15, 12);
        Assert.Contains("FIRST-order", what);
        // ...but it reaches only the term AT shares with GR: x² is 1e−30.
        Assert.True(x * x < 1.0e-29);
        Assert.Contains("distinctive content is untouched", verdict);

        // THE FRONTIER: a pure test needs x ~ 0.115, and it has not been achieved.
        var (needed, achieved, frontier) = TemporalCoreTestAudit.Frontier();
        Assert.False(achieved);
        Assert.Equal(0.115094, needed, 5);
        Assert.Contains("R_∞", frontier);
        Assert.True(needed > 0.1, "the frontier is a compact object, not a laboratory");
    }

    [Fact]
    public void Y_G_036_VerdictIsBoundary()
    {
        // COMPUTED verdict (ResearchY-G_027).
        Assert.Equal("BOUNDARY", TemporalCoreTestAudit.Verdict());

        // BOUNDARY because: the observable is pure, a discriminating regime exists, and a pure route to that
        // regime exists — but the precision currently supplied is not pure.
        Assert.True(TemporalCoreTestAudit.AllObservablesArePure());
        Assert.NotEmpty(TemporalCoreTestAudit.DiscriminatingCandidates());
        Assert.Contains(TemporalCoreTestAudit.DiscriminatingCandidates(), c => c.IsPureClock);
        Assert.Single(TemporalCoreTestAudit.ImpureCandidates());

        string where = TemporalCoreTestAudit.WhereItStands();
        Assert.Contains("0.125628", where);
        Assert.Contains("light bending", where);
        Assert.Contains("factor of three", where);
    }

    [Fact]
    public void Y_G_036_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_036 — Temporal Core Test Audit: can ρ → g₀₀ → clock → acceleration be tested alone?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The temporal core is G_035's surviving set: ρ, g₀₀ = −ρ^(2/d), the clock, the source law.");
        sb.AppendLine("  2. ALLOWED inputs: g₀₀, the clock law, the source law.");
        sb.AppendLine("  3. REJECTED: γ, lensing, the Shapiro delay, and the spatial metric g_ij in any form.");
        sb.AppendLine("  4. A test DISCRIMINATES when |Δz| = |z_GR − z_AT| exceeds the redshift uncertainty σ_z.");
        sb.AppendLine();

        PrintHeader("1. THE OBSERVABLE IS PURE — THE TWO CLOCK LAWS");
        sb.AppendLine("  AT:  z = e^x − 1            from g₀₀ = −e^(−2x)");
        sb.AppendLine("  GR:  z = 1/√(1 − 2x) − 1    from g₀₀ = −(1 − 2x)");
        sb.AppendLine("  series:  AT  x + x²/2 + x³/6   |   GR  x + 3x²/2 + 5x³/2");
        sb.AppendLine("  ⇒ they AGREE at O(x) and first differ at O(x²) — the split is ½x² against 1½x²");
        sb.AppendLine("  ⇒ z depends on g₀₀ ALONE: no γ, no lensing, no Shapiro, no g_ij anywhere in the measurement");
        sb.AppendLine();
        sb.AppendLine("  the observables (signature proves purity — none can be handed a B):");
        foreach (var (name, sig) in TemporalCoreTestAudit.PureObservables())
            sb.AppendLine($"    {sig}");
        sb.AppendLine();

        PrintHeader("2. THE ARENA — WHERE THE SPLIT BECOMES VISIBLE");
        sb.AppendLine("  candidate                                          x            x²        |Δz|      pure?");
        foreach (var c in TemporalCoreTestAudit.Candidates())
            sb.AppendLine($"  {Trim(c.Example, 44),-44}{c.Compactness,11:E4}{c.Signal,12:E4}"
                        + $"{Math.Abs(TemporalCoreTestAudit.Discriminator(c.Compactness)),12:E4}"
                        + $"   {(c.IsPureClock ? "yes" : "NO — " + Trim(c.Impurity, 34))}");
        sb.AppendLine();
        sb.AppendLine("  compactness needed for |Δz| > σ_z:");
        foreach (double sz in new[] { 1.0e-3, 0.01, 0.017766, 0.025125 })
            sb.AppendLine($"    σ_z = {sz,-10:F6} → x ≥ {TemporalCoreTestAudit.RequiredCompactness(sz):F6}");
        sb.AppendLine("  ⇒ the arena is NEUTRON STARS ONLY. Terrestrial clocks (x = 2.45e−15) would need σ_z ≤ 1e−30;");
        sb.AppendLine("    binary pulsars (1e−6) would need σ_z ≤ 1e−12. Nine to twelve orders out of reach.");
        sb.AppendLine();
        sb.AppendLine("  IN FACT THE WEAK-FIELD SPLIT IS NOT MERELY UNMEASURABLE — IT IS UNREPRESENTABLE.");
        sb.AppendLine($"    each law passes through an intermediate ≈ 1, carrying ~1 ulp = {TemporalCoreTestAudit.WeakFieldRoundingFloor():E2}");
        sb.AppendLine($"    true split at x = 2.45e−15 : {TemporalCoreTestAudit.Discriminator(2.45e-15):E3}  (series)");
        sb.AppendLine($"    direct subtraction returns : {TemporalCoreTestAudit.DirectDiscriminator(2.45e-15):E3}  ← noise, wrong SIGN");
        sb.AppendLine($"    at x = 1.1e−18, z_GR        : {TemporalCoreTestAudit.RedshiftGR(1.1e-18):E3}  ← annihilated outright");
        sb.AppendLine("  ⇒ in the weak field the split can be STATED but not COMPUTED BY SUBTRACTION. This is why the");
        sb.AppendLine("    audit uses the series Δz = x²(1 + 7x/3 + 13x²/3 + …) below x = 1e−3.");
        sb.AppendLine();

        PrintHeader("3. THE SEARCH AREAS");
        foreach (var area in new[] { "clock gradient", "redshift-only", "compact-object timing", "pulsar timing" })
        {
            var group = TemporalCoreTestAudit.Candidates().Where(c => c.Area == area).ToArray();
            sb.AppendLine($"  ── {area} ──");
            foreach (var c in group)
                sb.AppendLine($"    {Trim(c.Example, 40),-40} probe={c.Discriminating,-6} pure={c.IsPureClock,-6} {c.Note}");
        }
        sb.AppendLine();

        PrintHeader("4. THE BOTTLENECK — IT IS NOT THE SIGNAL");
        sb.AppendLine($"  σ_x/x needed for 5σ          : {TemporalCoreTestAudit.RequiredSigmaXOverX():P3}  (G_020)");
        sb.AppendLine($"  σ_x/x achieved today         : {TemporalCoreTestAudit.CurrentSigmaXOverX():P2}  (G_020)");
        sb.AppendLine($"  improvement still required   : {TemporalCoreTestAudit.SigmaXImprovementFactor():F2}×");
        sb.AppendLine($"  best significance today      : {TemporalCoreTestAudit.CurrentSignificance():F3}σ");
        sb.AppendLine($"  the signal itself at J0740   : Δz = {TemporalCoreTestAudit.Discriminator(0.247002):F6}"
                      + $" = {TemporalCoreTestAudit.SignalFraction(0.247002):P2} of z_AT");
        sb.AppendLine();
        sb.AppendLine("  AND THE COMPACTNESS COMES FROM THE SPATIAL SECTOR:");
        foreach (var c in TemporalCoreTestAudit.ImpureCandidates())
            sb.AppendLine($"    {c.Example}: {c.Impurity}");
        sb.AppendLine("  ⇒ the temporal core's ONLY discriminating test is currently read with a number obtained from");
        sb.AppendLine("    light bending — the spatial sector that G_032 showed is an assumed primitive.");
        sb.AppendLine();

        PrintHeader("5. A PURE ROUTE EXISTS — AND IS ABOUT THREE TIMES SHORT");
        sb.AppendLine("  R_∞ = R/√(1 − 2x) from a thermal flux plus a parallax distance.");
        sb.AppendLine("  This is a g₀₀ effect — photon energy and arrival rate, NOT bending — so {z, R_∞} solves");
        sb.AppendLine("  {M, R} with no light bending at all. Its systematics (distance, atmosphere models) keep");
        sb.AppendLine("  σ_x/x above the ≈3.7 % the test needs.");
        foreach (var c in TemporalCoreTestAudit.PureCandidates().Where(c => c.Compactness > 0.1))
            sb.AppendLine($"    {c.Example}: {c.Note}");
        sb.AppendLine();

        PrintHeader("6. THE FIRST EXPERIMENT");
        var (experiment, year, fx, what, verdict) = TemporalCoreTestAudit.FirstProbe();
        sb.AppendLine($"  {experiment}, {year} — x = {fx:E2}");
        sb.AppendLine($"    probed:    {what}");
        sb.AppendLine($"    verdict:   {verdict}");
        sb.AppendLine("  ⇒ It is PURE (no light bending in the measurement) but reaches only the first-order term —");
        sb.AppendLine("    x² = 6.0e−30 — the term AT SHARES WITH GR.");
        var (need, got, frontier) = TemporalCoreTestAudit.Frontier();
        sb.AppendLine($"  FRONTIER: {frontier}  (needs x ≥ {need:F6}; achieved = {got})");
        sb.AppendLine("  ⇒ NO EXPERIMENT HAS YET PROBED THE CORE'S DISTINCTIVE CONTENT.");
        sb.AppendLine();

        PrintHeader("7. VERDICT");
        sb.AppendLine($"  {TemporalCoreTestAudit.Verdict()} — testable in principle, not yet isolated in practice.");
        sb.AppendLine("  " + TemporalCoreTestAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  The missing ingredient is COMPACTNESS PRECISION, not signal. The temporal core is the one part");
        sb.AppendLine("  of the theory that is both independent (G_035) and, in principle, cleanly testable (G_036) —");
        sb.AppendLine("  which is exactly why closing the σ_x gap matters more than adding another weak-field clock.");
        Output.WriteLine(sb.ToString());
    }

    private static string Trim(string s, int n) => s.Length <= n ? s : s[..(n - 1)] + "…";
}
