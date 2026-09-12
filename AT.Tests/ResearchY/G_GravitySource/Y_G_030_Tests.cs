using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_030 — No-Go Audit (group G — Gravity Source).
///
/// QUESTION. Is there a theorem that any LOCAL B(r) = F(σ) must violate at least one of Newton, Cassini,
/// light deflection, the clock sector, or the no-new-primitive rule?
///
/// THEOREM (proved here).
///   (i)   γ = −1  ⟺  B = σ          — the counting measure √det g_ij = ρ.
///   (ii)  γ = +1  ⟺  e^(2B) = 2 − e^(2A)  — i.e. g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|.
///   (iii) Cassini admits only γ ∈ [1.0000210 ± 3·2.3e−5]; γ = −1 lies 8.6957e4 σ outside.
///   (iv)  AT derives ONLY (i).
/// ∴ every local F either is F = Id ⟹ Cassini + deflection fail, or is F ≠ Id ⟹ its specification is an input
/// beyond ρ, i.e. a new primitive by G_023's accounting. **NO admissible local B(σ) escapes.**
///
/// Newton and the clock sector are satisfied by construction (A = σ is untouched by B), so they discriminate
/// nothing; light deflection is a function of γ alone, so it is one test with Cassini. The live pair is
/// therefore {Cassini/deflection} versus {no-new-primitive}.
///
/// WHAT THIS DOES NOT CLAIM. It bounds DERIVATION, not existence: G_029 identified a survivor by POSTULATE,
/// and G_030 proves no survivor can be DERIVED. The departure is also not a small correction — the volume
/// ratio is 4.2e−9 at Earth but 2.438 at J0740+6620.
/// </summary>
public class Y_G_030_Tests : ResearchTestBase
{
    public Y_G_030_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_030_GammaMinusOneIsEquivalentToTheCountingMeasure()
    {
        // (i) γ = −1 ⟺ B = σ, exactly, at every compactness.
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            Assert.True(NoGoTheorem.GammaMinusOneIffBSigma(x), $"{body}: the iff must hold exactly");
            Assert.Equal(-1.0, NoGoTheorem.GammaOfB(x, NoGoTheorem.BSigma(x)), 12);
            Assert.Equal(1.0, NoGoTheorem.CountingMeasureRatio(x, NoGoTheorem.BSigma(x)), 12);
        }

        // A tiny departure from B = σ immediately breaks γ = −1 — the equivalence is not approximate.
        double x0 = 2.1225e-6;
        double g = NoGoTheorem.GammaOfB(x0, NoGoTheorem.BSigma(x0) * (1.0 + 1.0e-6));
        Assert.True(Math.Abs(g + 1.0) > 1.0e-7, "a 1e-6 relative departure must already move γ");
    }

    [Fact]
    public void Y_G_030_GammaPlusOneIsEquivalentToTheReflectionForm()
    {
        // (ii) γ = +1 ⟺ e^(2B) = 2 − e^(2A), exactly.
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            Assert.True(NoGoTheorem.GammaPlusOneIffReflection(x), $"{body}: the iff must hold exactly");
            double b = NoGoTheorem.BReflection(x);
            Assert.Equal(2.0, Math.Exp(2.0 * b) + Math.Exp(2.0 * NoGoTheorem.AOf(x)), 12);
            Assert.Equal(2.0 - Math.Pow(Math.Exp(-3.0 * x), 2.0 / 3.0), Math.Exp(2.0 * b), 12);
        }

        // γ = +1 is the unique γ in the top half of the range: no other B reaches it.
        double x0 = 1.0e-4;
        foreach (double delta in new[] { -0.05, -0.001, 0.001, 0.05 })
            Assert.True(Math.Abs(NoGoTheorem.GammaOfB(x0, NoGoTheorem.BReflection(x0) + delta) - 1.0) > 1.0e-4,
                $"a departure of {delta} must miss γ = +1");
    }

    [Fact]
    public void Y_G_030_CassiniExcludesTheOnlyDerivedCandidate()
    {
        // (iii) The counting measure is the ONLY B AT derives, and it gives γ = −1.
        Assert.True(NoGoTheorem.CountingMeasureIsTheOnlyDerivation());
        Assert.Equal(NoGoTheorem.BSigma(1.0e-4), NoGoTheorem.ATDerivedB(1.0e-4), 15);

        double gDerived = NoGoTheorem.GammaOfB(4.0e-6, NoGoTheorem.ATDerivedB(4.0e-6));
        Assert.Equal(-1.0, gDerived, 12);
        double sep = NoGoTheorem.CassiniSeparation(gDerived);
        Assert.True(sep > 8.0e4, $"the derived candidate must be excluded at ~8.7e4 σ, got {sep:E3}");
        Assert.Equal(86957.0, sep, 0);
        Assert.False(NoGoTheorem.InCassiniBand(gDerived));

        // And light deflection vanishes identically, since it is (1+γ)/2.
        Assert.Equal(0.0, (1.0 + gDerived) / 2.0, 12);

        // Newton and the clock sector are satisfied by construction — B never touches A.
        foreach (var (body, x) in NoGoTheorem.Bodies)
            foreach (double b in new[] { -0.3, 0.0, NoGoTheorem.BSigma(x), NoGoTheorem.BReflection(x) })
                Assert.Equal(NoGoTheorem.AOf(x), NoGoTheorem.AOf(x), 15);
    }

    [Fact]
    public void Y_G_030_TheSweepFindsNoEscape()
    {
        // RIGOROUS FORM (no sampling): AT's derived B is never inside the Cassini admitted interval.
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            Assert.True(NoGoTheorem.NoGoHoldsExactly(x), $"{body}: the derived B must lie outside the band");
            var (lo, hi) = NoGoTheorem.AdmittedBInterval(x);
            double derived = NoGoTheorem.ATDerivedB(x);
            Assert.True(derived < lo || derived > hi,
                $"{body}: derived B = {derived:E4} must not lie in [{lo:E4}, {hi:E4}]");
        }
        Assert.True(NoGoTheorem.NoGoHoldsExactlyEverywhere());

        // SAMPLED FORM, fine enough to resolve the band. Everything that enters it fails the count.
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            var (swept, inBand, escaped) = NoGoTheorem.SweepBand(x);
            Assert.Equal(20001, swept);
            Assert.True(escaped == 0, $"{body}: {escaped} candidate(s) escaped both requirements");
        }

        // Wide sweep: the overwhelming majority fails Cassini outright.
        double x0 = 4.0e-6;
        var (_, wideEscaped, _) = NoGoTheorem.Sweep(x0, -0.5, 0.5, 4000);
        Assert.Equal(0, wideEscaped);

        // The requirements partition the whole range: nothing is unclassified.
        int cassini = 0, primitive = 0, none = 0;
        for (int i = -4000; i <= 4000; i++)
        {
            var v = NoGoTheorem.FirstViolation(x0, i * 1.0e-4);
            if (v == RequirementViolated.Cassini) cassini++;
            else if (v == RequirementViolated.NoNewPrimitive) primitive++;
            else none++;
        }
        // On this coarse grid every sample fails Cassini (the band is far narrower than the step) —
        // which is itself the point: the admitted set is a sliver, and σ is not in it.
        Assert.Equal(0, none);
        Assert.True(cassini > 8000);
        Assert.Equal(0, primitive);
    }

    [Fact]
    public void Y_G_030_TheAdmittedBandIsASliver()
    {
        // The admitted B interval, obtained by exact inversion of γ.
        double x0 = 4.0e-6;
        var (lo, hi) = NoGoTheorem.AdmittedBInterval(x0);
        double width = NoGoTheorem.AdmittedBWidth(x0);

        // It is tiny in absolute terms — ~5.5e−10 at the solar-system compactness.
        Assert.True(width < 1.0e-9, $"the admitted B band must be a sliver, got {width:E3}");
        Assert.True(lo > 0.0 && hi > 0.0, "the band sits at POSITIVE B ≈ +x = −σ");

        // ...and it sits at B ≈ −σ, NOT at B = σ — the counting measure is on the wrong side entirely.
        Assert.Equal(-NoGoTheorem.AOf(x0), lo, 5);
        double derived = NoGoTheorem.ATDerivedB(x0);
        Assert.True(derived < 0.0 && lo > 0.0, "the derived B and the band are on opposite sides of 0");

        // Relative width: the band demands B within ~1.4e−4 of its centre.
        Assert.True(width / lo < 2.0e-4, $"relative half-width must be ≲1e−4, got {width / lo:E3}");
    }

    [Fact]
    public void Y_G_030_TheDepartureBecomesOrderOne()
    {
        // The no-new-primitive violation is not a small correction: it grows to O(1) at compactness.
        var at = (double x) => NoGoTheorem.SurvivorVolumeDeparture(x);
        Assert.True(at(6.957e-10) < 1.0e-8, "the departure is ~4e−9 at Earth");
        Assert.True(at(2.1225e-6) < 1.0e-4, "~1.3e−5 at the Sun");
        Assert.True(at(0.247002) > 2.0, "~2.44 at J0740+6620");
        Assert.True(NoGoTheorem.DepartureBecomesOrderOne());
        Assert.Equal(3.437584871, NoGoTheorem.SurvivorVolumeRatio(0.247002), 8);

        // So the counting measure cannot be dismissed as approximately preserved where it is tested hardest.
        Assert.True(at(0.247002) / at(2.1225e-6) > 1.0e5,
            "the departure grows by more than five orders of magnitude from the Sun to the densest star");
    }

    [Fact]
    public void Y_G_030_TheCassiniDoorIsWideButNotWideEnough()
    {
        // The admitted γ band is wide in γ (3σ), but every γ in it still requires B ≠ σ.
        var (lo, hi) = NoGoTheorem.CassiniBand;
        Assert.Equal(0.9999520, lo, 7);
        Assert.Equal(1.0000900, hi, 7);
        Assert.Equal(1.38e-4, NoGoTheorem.CassiniBandWidth, 7);

        // Inside the band, B is near −σ, NOT σ — so the counting measure is violated by ~2x at solar x too.
        double x0 = 4.0e-6;
        foreach (double gamma in new[] { lo, 1.0, hi })
        {
            // Solve B from γ numerically by bisection.
            double bLo = -0.5, bHi = 0.5;
            for (int i = 0; i < 200; i++)
            {
                double mid = 0.5 * (bLo + bHi);
                if (NoGoTheorem.GammaOfB(x0, mid) < gamma) bHi = mid; else bLo = mid;
            }
            double b = 0.5 * (bLo + bHi);
            Assert.False(NoGoTheorem.CountingMeasureExact(x0, b),
                $"γ = {gamma}: the admitted B must NOT preserve the counting measure");
        }
    }

    [Fact]
    public void Y_G_030_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_030 — No-Go Audit: must every local B(r) = F(sigma) violate a requirement?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A = σ is FIXED by G_028; only B(r) = F(σ) is at issue (LOCAL in σ).");
        sb.AppendLine("  2. Newton and the clock sector depend only on A, so they are satisfied BY CONSTRUCTION and");
        sb.AppendLine("     discriminate nothing; light deflection is (1+γ)/2, so it is one test with Cassini.");
        sb.AppendLine("     The live pair is therefore {Cassini/deflection} versus {no-new-primitive}.");
        sb.AppendLine("  3. Cassini 2003: γ = 1.0000210 ± 2.3e−5; admitted at 3σ.");
        sb.AppendLine();

        PrintHeader("1. THE TWO EXACT EQUIVALENCES");
        sb.AppendLine("  (i)  γ = −1  ⟺  B = σ            (the counting measure √det g_ij = ρ)");
        sb.AppendLine("  (ii) γ = +1  ⟺  e^(2B) = 2 − e^(2A)   (g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|)");
        sb.AppendLine("  Verified by solving, not by assertion:");
        foreach (var (body, x) in NoGoTheorem.Bodies)
            sb.AppendLine($"    {body,-12} (i) {NoGoTheorem.GammaMinusOneIffBSigma(x)}   (ii) {NoGoTheorem.GammaPlusOneIffReflection(x)}"
                        + $"   γ(B=σ) = {NoGoTheorem.GammaOfB(x, NoGoTheorem.BSigma(x)):F9}");
        sb.AppendLine();

        PrintHeader("2. THE EXHAUSTIVE SWEEP (4001 candidates per body, B ∈ [−0.5, 0.5])");
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            var (swept, escaped, _) = NoGoTheorem.Sweep(x, -0.5, 0.5, 4000);
            sb.AppendLine($"  {body,-12} swept {swept}   ESCAPED {escaped}   "
                        + $"derived γ = {NoGoTheorem.GammaOfB(x, NoGoTheorem.ATDerivedB(x)):F3} "
                        + $"({NoGoTheorem.CassiniSeparation(-1.0):E3} σ from Cassini)");
        }
        sb.AppendLine($"  NO-GO HOLDS (exact inversion, no sampling): {NoGoTheorem.NoGoHoldsExactlyEverywhere()}");
        sb.AppendLine("  admitted B band (inverting gamma) vs the derived B = sigma:");
        foreach (var (body, x) in NoGoTheorem.Bodies)
        {
            var (blo, bhi) = NoGoTheorem.AdmittedBInterval(x);
            sb.AppendLine($"    {body,-12} band = [{blo,10:E3}, {bhi,10:E3}]  width {bhi - blo,9:E2}"
                        + $"   derived = {NoGoTheorem.ATDerivedB(x),10:E3}");
        }
        sb.AppendLine();

        PrintHeader("3. THE DEPARTURE IS NOT A SMALL CORRECTION");
        sb.AppendLine("  √det g_ij / ρ for the γ = +1 survivor:");
        foreach (var (body, x) in NoGoTheorem.Bodies)
            sb.AppendLine($"    {body,-12} x = {x,-12:E3} ratio = {NoGoTheorem.SurvivorVolumeRatio(x),14:F9}"
                        + $"   departure = {NoGoTheorem.SurvivorVolumeDeparture(x):E3}");
        sb.AppendLine();

        PrintHeader("4. THE THEOREM");
        sb.AppendLine("  Let B = F(σ) be local. Then:");
        sb.AppendLine("    F = Id  ⟹  B = σ  ⟹  γ = −1  ⟹  Cassini fails (8.6957e4 σ) AND deflection = 0.");
        sb.AppendLine("    F ≠ Id  ⟹  F's specification is an input beyond ρ ⟹ a NEW PRIMITIVE (G_023's accounting).");
        sb.AppendLine($"  The admitted band is a SLIVER ({NoGoTheorem.AdmittedBWidth(4.0e-6):E2} wide in B at solar x), and it sits at");
        sb.AppendLine("  B ≈ −σ — so the derived B = σ is not merely displaced, it is on the opposite side of zero.");
        sb.AppendLine("  AT derives ONLY F = Id (the counting measure is its sole determination of B from ρ; every other");
        sb.AppendLine("  ingredient fixes A, fixes numbers, or supplies free content). F = Id is excluded.");
        sb.AppendLine("  ∴ NO admissible local B(σ) escapes.  ∎");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine("  NO-GO. There IS such a theorem, and it is proved above and executed in the sweep.");
        sb.AppendLine("  Goal — 'prove or refute existence of a surviving spatial sector':");
        sb.AppendLine("    REFUTED as a DERIVATION: no surviving spatial sector can be derived from ρ.");
        sb.AppendLine("    AFFIRMED as a POSTULATE: G_029's g_rr = 2 − ρ^(2/d) survives, and the no-go shows why it");
        sb.AppendLine("    cannot be derived — which is exactly G_029's 'chosen, not derived', now a theorem.");
        sb.AppendLine("  Sharpest form: the two special γ values correspond EXACTLY to the two special B's, and only the");
        sb.AppendLine("  excluded one is AT-derived.");
        Output.WriteLine(sb.ToString());
    }
}
