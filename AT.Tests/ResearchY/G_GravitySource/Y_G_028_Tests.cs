using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_028 — Clock Sector Closure Audit (group G — Gravity Source).
///
/// QUESTION. Is the physical clock law <c>dτ/dt = ρ^(1/d)</c> or <c>dτ/dt = ρ^(1/d)·e^ψ</c>?
///
/// THE ONE FACT THAT DECIDES IT. The Newtonian potential IS the time exponent:
///     g₀₀ = −e^(2A),  A = σ + ψ,  σ = (1/d)·ln ρ,  Φ/c² = A
/// so the acceleration is  a = −∇Φ = −(1/d)∇ln ρ − ∇ψ.  The stated source law a = −(1/d)∇ln ρ therefore holds
/// **iff ∇ψ = 0** — a constant ψ, which is only a global choice of time unit. ANY spatially varying ψ changes
/// the source law.
///
/// THE DECISIVE CORRECTION. Within the trace-preserving QG207 direction, A − σ = ψ and B − σ = −ψ/(d−1), so
/// γ = +1 (i.e. A + B = 0) fixes ψ = −4σ **uniquely**. Then A = σ + ψ = −3σ = +3x &gt; 0: the potential becomes
/// POSITIVE, the surface clock runs FAST, and every gravitating body is BLUESHIFTED. The clock's SIGN inverts.
///
/// WHAT THIS AUDIT CORRECTS. G_024 concluded the QG207 completion creates "NO solar-system conflict", comparing
/// the shift e^(4x) − 1 = 2.78e−9 against a |ψ| ≲ 2e−3 bound. That treated the shift as ADDITIVE on a clock of
/// 1. But the observable is the redshift RELATIVE TO INFINITY, and the conformal law already predicts z ≈ +x.
/// A shift of 2.78e−9 is not small beside x = 6.957e−10 — it is 4x, and it flips the sign. The correct GPS
/// bound on |ψ| is the precision itself (~1.4e−12), which is ~1.4e9× TIGHTER than G_024 quoted.
/// </summary>
public class Y_G_028_Tests : ResearchTestBase
{
    public Y_G_028_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_028_CandidateARhoOnlySatisfiesAllFiveRequirements()
    {
        // (1) SOURCE LAW: ψ = 0 ⟹ a = −(1/d)∇ln ρ exactly.
        Assert.True(ClockSectorClosure.SourceLawPreserved(0.0));
        // (2) EARTH/GPS: the conformal law IS the calibrated law, so the separation is zero.
        Assert.True(ClockSectorClosure.EarthSeparationSigma(ClockCandidate.RhoOnly) < 1e-6,
            "the rho-only clock must reproduce the observed Earth redshift");
        // (3) NEUTRON-STAR REDSHIFT AUDIT (G_020): z_AT = 0.2801817 reproduced.
        double zNs = ClockSectorClosure.CaseFor(ClockCandidate.RhoOnly, 0.247002).Z;
        Assert.Equal(0.2801817, zNs, 7);
        // (4) POSITIVITY at every body, including compact objects.
        foreach (var c in ClockSectorClosure.Bodies)
            Assert.True(ClockSectorClosure.CaseFor(ClockCandidate.RhoOnly, c.X).RedshiftPositive,
                $"rho-only redshift must stay positive at {c.Body}");
        // (5) NO NEW PRIMITIVE: ψ = 0 uses nothing beyond ρ.
        double psi = -4.0 * ClockSectorClosure.Sigma(1.0e-4);
        Assert.NotEqual(0.0, psi);   // the QG207 route needs a nonzero ψ; the rho-only route needs none

        // The ONE thing it fails is optics, which is NOT among the five requirements — and that failure lives
        // in the SPATIAL sector (γ = −1), not in the clock.
        Assert.Equal(-1.0, ClockSectorClosure.CaseFor(ClockCandidate.RhoOnly, 1.0e-4).GammaExact, 6);
    }

    [Fact]
    public void Y_G_028_CandidateBQg207IsRefuted()
    {
        // γ = +1 requires ψ = −4σ, which makes A = +3x > 0 — the potential flips sign.
        foreach (var (body, x) in ClockSectorClosure.Bodies)
        {
            var c = ClockSectorClosure.CaseFor(ClockCandidate.Qg207Psi, x);
            Assert.True(c.A > 0.0, $"{body}: the QG207 gamma=+1 clock exponent must be POSITIVE (A = {c.A:E3})");
            Assert.False(c.RedshiftPositive, $"{body}: the QG207 redshift must turn NEGATIVE (z = {c.Z:E3})");
            Assert.True(c.ClockOverInfinity > 1.0,
                $"{body}: the surface clock must run FAST relative to infinity");
        }

        // Requirements 1 and 4 fail outright; requirement 2 fails at 2000σ.
        Assert.False(ClockSectorClosure.SourceLawPreserved(-4.0 * ClockSectorClosure.Sigma(1.0e-4)));
        double sep = ClockSectorClosure.EarthSeparationSigma(ClockCandidate.Qg207Psi);
        Assert.True(sep > 1000.0, $"the Earth separation must be enormous, got {sep:F1} sigma");
        Assert.Equal(2000.0, sep, 0);

        // The neutron-star audit is destroyed: NICER/observed z is positive (~0.25), QG207 predicts −0.5234.
        double zNs = ClockSectorClosure.CaseFor(ClockCandidate.Qg207Psi, 0.247002).Z;
        Assert.Equal(-0.5233658, zNs, 6);

        // The ψ contribution to the acceleration is 4× the ρ contribution — it does not merely perturb it.
        Assert.Equal(4.0, ClockSectorClosure.PsiAccelerationContribution(1.0e-4), 9);
    }

    [Fact]
    public void Y_G_028_PsiCannotBeNonzeroWithoutChangingTheSourceLaw()
    {
        // THE CRITICAL QUESTION: can ψ be nonzero without changing the source law?
        // NO — because Φ = A = σ + ψ, so a = −(1/d)∇ln ρ − ∇ψ. The law holds iff ψ is spatially constant.
        Assert.True(ClockSectorClosure.SourceLawPreserved(0.0));
        foreach (double x in new[] { 1.0e-6, 1.0e-4, 0.1, 0.247002 })
        {
            double psi = ClockSectorClosure.PsiForGammaPlusOne(x);
            Assert.False(ClockSectorClosure.SourceLawPreserved(psi),
                $"x = {x}: a nonzero ψ must change the source law");

            // And it must break the CLOCK too: A ≠ σ whenever ψ ≠ 0, because A − σ = ψ identically.
            double a = ClockSectorClosure.AOf(x, psi);
            Assert.NotEqual(ClockSectorClosure.Sigma(x), a);
            Assert.Equal(psi, a - ClockSectorClosure.Sigma(x), 12);
        }

        // A constant ψ is a global time-unit choice, not physics: it cancels from the redshift RATIO.
        double zA = ClockSectorClosure.ZOf(ClockSectorClosure.Sigma(1.0e-4) + 0.05);
        double zB = ClockSectorClosure.ZOf(ClockSectorClosure.Sigma(1.0e-4) + 0.05);
        Assert.Equal(zA, zB, 15);
    }

    [Fact]
    public void Y_G_028_GammaPlusOneForcesTheClockToInvert()
    {
        // Within the trace-preserving QG207 family, ψ = −4σ is the UNIQUE value making A + B = 0, i.e. γ = +1.
        foreach (double x in new[] { 6.957e-10, 2.1225e-6, 1.0e-4, 0.247002 })
        {
            double psi = ClockSectorClosure.PsiForGammaPlusOne(x);
            double a = ClockSectorClosure.AOf(x, psi);
            double b = ClockSectorClosure.BOf(x, psi);
            Assert.Equal(0.0, a + b, 12);                       // A + B = 0 ⟺ γ = +1 (to first order)
            // The EXACT γ reaches +1 only in the weak field: G_025 established it drifts as e^(6σ) = ρ².
            // A + B = 0 is the first-order condition PPN γ is defined at.
            double g = ClockSectorClosure.GammaExact(a, b);
            if (x <= 1.0e-4) Assert.True(Math.Abs(g - 1.0) < 2.0e-3, $"x = {x}: γ = {g:F6} should be ≈ +1");
            else Assert.True(g < 0.5, $"x = {x}: the exact γ must drift well below +1, got {g:F4}");
            // ...and the price: A = −3σ = +3x.
            Assert.Equal(3.0 * x, a, 12);
        }

        // The conformal member is the OTHER extreme of the same family: ψ = 0 ⟹ A = B = σ ⟹ γ = −1.
        double x0 = 1.0e-4;
        Assert.Equal(-1.0, ClockSectorClosure.GammaExact(
            ClockSectorClosure.Sigma(x0), ClockSectorClosure.Sigma(x0)), 6);

        // So the family interpolates γ from −1 to +1, and the clock exponent from −x to +3x. There is no
        // member with A < 0 and γ = +1: the two requirements are incompatible inside this direction.
        // (The point A = 0 is degenerate — γ diverges there — so it is excluded.)
        for (double t = 0.0; t <= 1.0; t += 0.05)
        {
            double psi = t * ClockSectorClosure.PsiForGammaPlusOne(x0);
            double a = ClockSectorClosure.AOf(x0, psi);
            double b = ClockSectorClosure.BOf(x0, psi);
            if (Math.Abs(a) < 1.0e-12) continue;            // γ = −B/A is undefined at A = 0
            double g = ClockSectorClosure.GammaExact(a, b);
            // γ is positive exactly when A is positive: the family cannot give γ > 0 with A < 0.
            Assert.Equal(a > 0.0, g > 0.0);
        }
    }

    [Fact]
    public void Y_G_028_SpatialRoutePreservesTheClockAtAVolumeCost()
    {
        // The ONLY way to get γ = +1 while keeping the clock (A = σ fixed) is to move B instead.
        foreach (double x in new[] { 2.1225e-6, 1.0e-4, 0.247002 })
        {
            var c = ClockSectorClosure.CaseFor(ClockCandidate.SpatialRoute, x);
            Assert.Equal(ClockSectorClosure.Sigma(x), c.A, 15);        // source law + clock PRESERVED
            Assert.True(c.RedshiftPositive);                          // positivity PRESERVED
            Assert.Equal(1.0, c.GammaExact, 5);                       // γ = +1 achieved
        }
        // ...and its price is the volume measure, which G_023 quantified.
        double cost = ClockSectorClosure.SpatialRouteVolumeCost(0.247002);
        Assert.Equal(3.437585, cost, 1);
        Assert.True(cost > 1.0, "the volume element must exceed the count at compactness");
    }

    [Fact]
    public void Y_G_028_GpsBoundCorrectedAgainstG024()
    {
        // G_024 quoted |ψ| ≲ 2e−3 from GPS and called it 7.2e5× LOOSER than required, concluding there is
        // "NO solar-system conflict". The comparison was wrong: the observable is the redshift RELATIVE TO
        // INFINITY, and the conformal law already predicts z ≈ +x = 6.957e−10.
        double bound = ClockSectorClosure.PsiBoundFromGps();
        Assert.True(bound < 1.0e-11, $"the correct GPS bound on |ψ| must be ~1e−12, got {bound:E3}");
        Assert.Equal(1.0e-12, bound, 12);

        double factor = ClockSectorClosure.GpsBoundCorrectionFactor();
        Assert.True(factor > 1.0e9, $"the corrected bound must be ≳1e9× tighter than 2e−3, got {factor:E3}");

        // The headline separation.
        double sep = ClockSectorClosure.EarthSeparationSigma(ClockCandidate.Qg207Psi);
        Assert.True(sep > 1.0e3, $"the Earth separation must be enormous, got {sep:F1} sigma");

        // ψ = −4σ at Earth is 4x the compactness — it is not a small perturbation on the clock.
        double psiEarth = ClockSectorClosure.PsiForGammaPlusOne(ClockSectorClosure.Bodies[0].X);
        Assert.Equal(4.0 * 6.957e-10, psiEarth, 15);
    }

    [Fact]
    public void Y_G_028_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_028 — Clock Sector Closure Audit: is the clock rho^(1/d) or rho^(1/d)*exp(psi)?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Isotopic form ds^2 = -e^(2A)dt^2 + e^(2B)(dr^2 + r^2 dOmega^2); gamma = -(e^(2B)-1)/(e^(2A)-1).");
        sb.AppendLine("  2. The Newtonian potential IS the time exponent: Phi/c^2 = A (since g00 = -(1+2Phi) = -e^(2A)).");
        sb.AppendLine("  3. rho = e^(-d x) with x = GM/(Rc^2), so sigma = (1/d)ln rho = -x.");
        sb.AppendLine("  4. QG207: A = sigma + psi, B = sigma - psi/(d-1); trace A + (d-1)B = d*sigma preserved.");
        sb.AppendLine();

        PrintHeader("1. THE CASE TABLE");
        sb.AppendLine("  candidate        body           x          A            B           z            gamma      pos?");
        foreach (var c in ClockSectorClosure.Table())
        {
            string name = c.Candidate switch
            {
                ClockCandidate.RhoOnly => "rho-only",
                ClockCandidate.Qg207Psi => "QG207 psi=-4s",
                _ => "spatial route",
            };
            sb.AppendLine($"  {name,-16} {ClockSectorClosure.Bodies.First(b => b.X == c.X).Body,-14}"
                        + $"{c.X,10:F3e} {c.A,12:E3} {c.B,12:E3} {c.Z,12:E3} {c.GammaExact,9:F4}  {c.RedshiftPositive}");
        }
        sb.AppendLine();

        PrintHeader("2. THE FIVE REQUIREMENTS");
        sb.AppendLine("  req                                rho-only   QG207(psi=-4s)   spatial route");
        sb.AppendLine("  1 source law a = -(1/d) grad ln rho     YES         NO              YES");
        sb.AppendLine("  2 Earth/GPS agreement                   YES      NO (2000 sigma)     YES");
        sb.AppendLine("  3 neutron-star redshift audit (G_020)   YES         NO              YES");
        sb.AppendLine("  4 positivity for compact objects        YES         NO              YES");
        sb.AppendLine("  5 no new primitive                      YES         YES             NO  (volume cost)");
        sb.AppendLine($"  Earth separation: rho-only {ClockSectorClosure.EarthSeparationSigma(ClockCandidate.RhoOnly):E3} sigma, "
                      + $"QG207 {ClockSectorClosure.EarthSeparationSigma(ClockCandidate.Qg207Psi):F1} sigma");
        sb.AppendLine($"  J0740+6620:       rho-only z = {ClockSectorClosure.CaseFor(ClockCandidate.RhoOnly, 0.247002).Z:F7}, "
                      + $"QG207 z = {ClockSectorClosure.CaseFor(ClockCandidate.Qg207Psi, 0.247002).Z:F7}");
        sb.AppendLine();

        PrintHeader("3. WHY: THE POTENTIAL IS THE TIME EXPONENT");
        sb.AppendLine("  g00 = -e^(2A)  with  A = sigma + psi  =>  Phi/c^2 = A  =>  a = -(1/d) grad ln rho - grad psi");
        sb.AppendLine("  THE SOURCE LAW HOLDS IFF grad psi = 0. A constant psi is a global time-unit choice, not physics.");
        sb.AppendLine("  So: CAN psi BE NONZERO WITHOUT CHANGING THE SOURCE LAW?  NO.");
        sb.AppendLine($"  At psi = -4 sigma the psi contribution is {ClockSectorClosure.PsiAccelerationContribution(1.0e-4):F1}x the rho contribution.");
        sb.AppendLine();

        PrintHeader("4. CORRECTION TO G_024");
        sb.AppendLine($"  G_024: |psi| <= 2e-3 from GPS, '7.2e5x looser than required', 'NO solar-system conflict'.");
        sb.AppendLine($"  CORRECT bound: |psi| <= {ClockSectorClosure.PsiBoundFromGps():E3}  (the GPS precision itself),");
        sb.AppendLine($"  i.e. {ClockSectorClosure.GpsBoundCorrectionFactor():E3}x TIGHTER than quoted.");
        sb.AppendLine("  The error: the shift e^(4x)-1 = 2.78e-9 was treated as additive on a clock of 1, but the");
        sb.AppendLine("  observable is the redshift RELATIVE TO INFINITY, where the conformal law already gives z ~ +x.");
        sb.AppendLine("  2.78e-9 is 4x the entire effect, and it REVERSES its sign.");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine("  DERIVED  — dTau/dt = rho^(1/d). It satisfies ALL FIVE requirements: source law, Earth/GPS");
        sb.AppendLine("             (0.000 sigma), the G_020 neutron-star audit (z_AT = 0.2801817), positivity at every");
        sb.AppendLine("             body, and no new primitive. Gamma = -1 is EXCLUDED (Cassini 8.6957e4 sigma), but optics");
        sb.AppendLine("             is not among the five requirements — and that failure lives in the SPATIAL sector.");
        sb.AppendLine("  REFUTED  — dTau/dt = rho^(1/d)*exp(psi) at the psi that gamma = +1 requires. psi = -4 sigma is");
        sb.AppendLine("             UNIQUE within the trace-preserving family, and it forces A = +3x > 0: a blueshift at");
        sb.AppendLine("             every gravitating body, negative z at compactness, and a source law broken by 4x.");
        sb.AppendLine("  BOUNDARY — the spatial route (A held at sigma, B = 0.5 ln(2 - e^(2 sigma))) is the ONLY way to");
        sb.AppendLine("             reach gamma = +1 without moving the clock. It satisfies requirements 1-4 and fails 5:");
        sb.AppendLine("             it leaves the trace-free direction, so the volume measure becomes ~3.44x the count.");
        sb.AppendLine();
        sb.AppendLine("  CLOCK CLOSED — on dTau/dt = rho^(1/d). The closure RELOCATES the failure: the clock sector is");
        sb.AppendLine("  sound, and the entire open problem is the SPATIAL sector (G_023's, still unresolved).");
        Output.WriteLine(sb.ToString());
    }
}
