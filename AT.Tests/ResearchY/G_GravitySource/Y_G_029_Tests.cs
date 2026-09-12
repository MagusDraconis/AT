using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_029 — Spatial Sector Closure Audit (group G — Gravity Source).
///
/// QUESTION. Can any spatial metric B(r) survive all existing constraints without introducing a new primitive?
///
/// KNOWN. A = σ = (1/d)ln ρ, FIXED by G_028 (clock sector CLOSED). The only remaining freedom is B(r).
///
/// WHAT THE EIGHT REQUIREMENTS ACTUALLY CONSTRAIN. Requirements 1–4 (Newton limit, Earth clock, GPS, the
/// neutron-star audit) depend ONLY on A — preserved by every candidate. Requirements 5–7 (Cassini γ, light
/// deflection, Shapiro delay) depend ONLY on γ. So the requirements pin **one number**, not a function; the
/// entire nonlinear completion of B is free.
///
/// THE DECISIVE RESULT. γ is a functional of B alone, and the condition γ = +1 at ALL orders has exactly ONE
/// solution: e^(2B) = 2 − e^(2A), i.e. **g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|** — the spatial metric is the
/// REFLECTION of the temporal one about 1, where the conformal (γ = −1) member has g_rr = |g₀₀|.
///
/// AND THE SECTOR'S HONEST STATE. AT derives no B. Its only equation for B — the counting measure
/// √det g_ij = ρ — yields B = σ, the member Cassini excludes at 8.7e4 σ. So the survivor is selected by
/// OBSERVATION, not by the theory: the spatial sector closes on a POSTULATE.
/// </summary>
public class Y_G_029_Tests : ResearchTestBase
{
    public Y_G_029_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_029_ConformalMemberIsRefutedByCassini()
    {
        // B = σ is the ONLY member the theory derives (the counting measure), and it is refuted.
        var c = SpatialSectorClosure.CaseFor(SpatialFamily.Conformal, 4.0e-6);
        Assert.Equal(-1.0, c.Gamma, 6);                                   // γ = −1
        Assert.Equal(0.0, c.DeflectionOverGr, 12);                        // no light deflection
        Assert.Equal(0.0, c.ShapiroOverGr, 12);                           // no Shapiro delay
        Assert.Equal(1.0, c.CountingMeasureRatio, 9);                     // ...but it preserves the count
        Assert.True(SpatialSectorClosure.CassiniSeparation(c.Gamma) > 8.0e4,
            "the conformal member must be excluded by Cassini at ~8.7e4 sigma");
        Assert.Contains("REFUTED", SpatialSectorClosure.VerdictOf(SpatialFamily.Conformal));

        // Requirements 1–4 are preserved by EVERY candidate, because they depend only on A.
        foreach (var f in Enum.GetValues<SpatialFamily>())
        {
            var k = SpatialSectorClosure.CaseFor(f, 4.0e-6);
            Assert.Equal(SpatialSectorClosure.Sigma(4.0e-6), k.A, 15);
        }
    }

    [Fact]
    public void Y_G_029_ExactlyOneMemberGivesExactGrOptics()
    {
        // γ = +1 at ALL orders ⟺ e^(2B) = 2 − e^(2A), i.e. g_rr = 2 − ρ^(2/d). UNIQUE.
        foreach (var (body, x) in SpatialSectorClosure.Bodies)
        {
            var c = SpatialSectorClosure.CaseFor(SpatialFamily.ExactGr, x);
            // The DEFINING relation is exact by construction; γ then follows to round-trip precision.
            Assert.Equal(2.0 - Math.Exp(2.0 * SpatialSectorClosure.AOf(x)), Math.Exp(2.0 * c.B), 10);
            Assert.True(Math.Abs(c.Gamma - 1.0) < 1.0e-6, $"{body}: γ must be +1 at all orders, got {c.Gamma:F12}");
            Assert.True(Math.Abs(c.DeflectionOverGr - 1.0) < 1.0e-6);
            Assert.True(Math.Abs(c.ShapiroOverGr - 1.0) < 1.0e-6);

            // The closed form must agree with the exponent form.
            double rho = Math.Exp(-3.0 * x);
            Assert.Equal(SpatialSectorClosure.GrrOfExactMember(rho), Math.Exp(2.0 * c.B), 9);

            // Uniqueness: any other B giving exact γ = +1 would have to satisfy the same equation.
            double bOther = 2.0 * SpatialSectorClosure.Sigma(x);      // a wrong member
            Assert.NotEqual(1.0, SpatialSectorClosure.GammaOf(SpatialSectorClosure.AOf(x), bOther), 6);
        }

        // The linear member reaches +1 only to FIRST order — γ = 1 + 2x.
        double xSolar = 4.0e-6;
        double gLinear = SpatialSectorClosure.CaseFor(SpatialFamily.NegSigma, xSolar).Gamma;
        Assert.Equal(1.0 + 2.0 * xSolar, gLinear, 9);
        Assert.True(gLinear > 1.0, "the linear member overshoots +1 at first order");
    }

    [Fact]
    public void Y_G_029_TheExactMemberIsNonDegenerateEverywherePhysical()
    {
        // e^(2B) = 2 − e^(2A) vanishes only at A = +½ln2, i.e. x = −½ln2 — a NEGATIVE compactness
        // (a repulsive / negative-mass object). So the exact member has NO degeneracy for any physical x > 0.
        Assert.Equal(-0.3465735903, SpatialSectorClosure.DegenerateX, 10);

        foreach (double x in new[] { 6.957e-10, 2.1225e-6, 0.1, 0.172317, 0.247002, 0.35, 0.5, 1.0, 10.0 })
        {
            Assert.True(SpatialSectorClosure.NonDegenerate(SpatialFamily.ExactGr, x),
                $"x = {x}: the exact member must be non-degenerate");
            double grr = Math.Exp(2.0 * SpatialSectorClosure.BExactGr(x));
            Assert.True(grr > SpatialSectorClosure.GrrLowerBound && grr < SpatialSectorClosure.GrrUpperBound,
                $"x = {x}: g_rr = {grr:F9} must lie strictly inside (1, 2)");
        }

        // Including a black-hole compactness, which the conformal member also handles — but the exact member
        // stays regular, with space STRETCHED (g_rr > 1) at every radius.
        Assert.True(SpatialSectorClosure.NonDegenerate(SpatialFamily.ExactGr, 0.5));
        Assert.True(Math.Exp(2.0 * SpatialSectorClosure.BExactGr(10.0)) > 1.9,
            "as x grows, g_rr tends to 2");
    }

    [Fact]
    public void Y_G_029_TheSectorClosesToOneNumberNotAFunction()
    {
        // Cassini constrains only γ, so the quadratic coefficient c₂ is nearly free.
        double allowed = SpatialSectorClosure.CassiniAllowedC2Deviation();
        Assert.True(allowed > 20.0, $"Cassini must leave c₂ wide open, got |c₂+2| <= {allowed:F1}");

        // Confirm: a wide sweep of c₂ stays inside Cassini.
        int inside = 0, swept = 0;
        for (double c2 = -20.0; c2 <= 18.0; c2 += 1.0)
        {
            swept++;
            double g = SpatialSectorClosure.GammaOf(
                SpatialSectorClosure.AOf(4.0e-6),
                SpatialSectorClosure.BPolynomial(4.0e-6, c2));
            if (SpatialSectorClosure.CassiniSeparation(g) <= 3.0) inside++;
        }
        Assert.True(inside > swept / 2, $"most of the swept c₂ range must be Cassini-allowed ({inside}/{swept})");

        // Polynomial and rational/exponential families therefore SURVIVE observationally without being selected.
        Assert.Contains("SURVIVES", SpatialSectorClosure.VerdictOf(SpatialFamily.Polynomial));
        Assert.Contains("SURVIVES", SpatialSectorClosure.VerdictOf(SpatialFamily.RationalExponential));
    }

    [Fact]
    public void Y_G_029_TheTheoryDerivesNoB()
    {
        // AT's ONLY equation for B is the counting measure, and it yields the refuted member.
        Assert.False(SpatialSectorClosure.AnyDerivableBSurvives(),
            "the only derivable B (= sigma) must NOT survive");

        // The exact-γ member's price is the volume measure — and it reproduces G_023's recorded cost.
        var c = SpatialSectorClosure.CaseFor(SpatialFamily.ExactGr, 0.247002);
        Assert.Equal(3.437584871, c.CountingMeasureRatio, 8);
        Assert.True(c.CountingMeasureRatio > 1.0, "the volume element must exceed the count");

        // The conformal member is the only one that preserves the count exactly.
        Assert.Equal(1.0, SpatialSectorClosure.CaseFor(SpatialFamily.Conformal, 0.247002).CountingMeasureRatio, 9);

        // So: the survivor trades the counting measure for GR optics. No new PRIMITIVE is added (the freedom
        // is the traceless face ψ, established at G_024 as not a new primitive), but the theory loses its
        // only EQUATION for B — which is why the survivor is a POSTULATE, not a derivation.
        Assert.True(Math.Abs(c.CountingMeasureRatio - 1.0) > 1.0,
            "the survivor must NOT preserve the counting measure — that is the whole point");
    }

    [Fact]
    public void Y_G_029_ExactlyOneSurvivesAndIsIdentified()
    {
        // Identify the survivor: g_rr = 2 − ρ^(2/d) = 2 − |g₀₀| — the reflection of the temporal metric about 1.
        foreach (double x in new[] { 2.1225e-6, 1.0e-4, 0.247002 })
        {
            double rho = Math.Exp(-3.0 * x);
            double grr = SpatialSectorClosure.GrrOfExactMember(rho);
            double g00Abs = Math.Pow(rho, 2.0 / 3.0);
            Assert.Equal(2.0 - g00Abs, grr, 12);                    // g_rr = 2 − |g₀₀|
            Assert.Equal(2.0 - Math.Exp(2.0 * SpatialSectorClosure.Sigma(x)), grr, 12);
            // First order (small x only): g_rr = 1 + 2x = 1 − 2γΦ with Φ = −x, γ = +1 — the GR value.
            if (x <= 1.0e-4) Assert.Equal(1.0 + 2.0 * x, grr, 6);
            else Assert.True(grr < 1.0 + 2.0 * x, "at compactness the exact form falls below its linear term");
        }
        // The conformal member is the mirror image: g_rr = ρ^(2/d) = |g₀₀|.
        double r = Math.Exp(-3.0 * 0.247002);
        Assert.Equal(Math.Pow(r, 2.0 / 3.0), SpatialSectorClosure.GrrOfConformalMember(r), 12);
    }

    [Fact]
    public void Y_G_029_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_029 — Spatial Sector Closure Audit: can any B(r) survive without a new primitive?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A = σ is FIXED by G_028 (clock sector CLOSED); only B(r) remains free.");
        sb.AppendLine("  2. ds² = −e^(2A)dt² + e^(2B)(dr² + r²dΩ²); γ = −(e^(2B)−1)/(e^(2A)−1).");
        sb.AppendLine("  3. Requirements 1–4 depend only on A (preserved by every candidate); 5–7 depend only on γ.");
        sb.AppendLine("  4. Counting measure √det g_ij = ρ ⟺ e^(3B) = ρ ⟺ B = σ.");
        sb.AppendLine();

        PrintHeader("1. THE CANDIDATE TABLE (J0740+6620, x = 0.247002)");
        sb.AppendLine("  family                    B            gamma       defl/GR    shapiro/GR  vol/rho");
        foreach (var c in SpatialSectorClosure.Table().Where(c => c.X == 0.247002))
            sb.AppendLine($"  {c.Family,-22} {c.B,12:E4} {c.Gamma,10:F6} {c.DeflectionOverGr,10:F6} "
                        + $"{c.ShapiroOverGr,11:F6} {c.CountingMeasureRatio,9:F6}");
        sb.AppendLine();

        PrintHeader("2. THE VERDICTS (COMPUTED, never typed)");
        foreach (var f in Enum.GetValues<SpatialFamily>())
            sb.AppendLine($"  {f,-22} {SpatialSectorClosure.VerdictOf(f)}");
        sb.AppendLine();

        PrintHeader("3. WHAT THE EIGHT REQUIREMENTS CONSTRAIN");
        sb.AppendLine("  1 Newton limit    } depend only on A = σ — PASS for EVERY candidate");
        sb.AppendLine("  2 Earth clock     }");
        sb.AppendLine("  3 GPS             }");
        sb.AppendLine("  4 neutron-star    }");
        sb.AppendLine("  5 Cassini gamma   }");
        sb.AppendLine("  6 deflection      } depend only on γ — ONE number");
        sb.AppendLine("  7 Shapiro         }");
        sb.AppendLine("  8 no new primitive — the B freedom is ψ's traceless face (G_024: not a new primitive)");
        sb.AppendLine("  ⇒ the requirements pin ONE NUMBER, not a function: the nonlinear completion stays free.");
        sb.AppendLine();

        PrintHeader("4. THE UNIQUE MEMBER WITH EXACT GR OPTICS");
        sb.AppendLine("  e^(2B) = 2 − e^(2A)  ⟺  g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|");
        sb.AppendLine("  the REFLECTION of the temporal metric about 1 (conformal member: g_rr = |g₀₀|)");
        sb.AppendLine($"  e^(2B) = 2 − e^(2A) vanishes only at x = −½ln2 = {SpatialSectorClosure.DegenerateX:F10}");
        sb.AppendLine("  i.e. a NEGATIVE compactness (a repulsive object): the exact member has NO degeneracy");
        sb.AppendLine("  anywhere physical, and g_rr = 2 − rho^(2/d) is bounded in (1, 2) — space is STRETCHED.");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine("  SURVIVES  — the γ = +1 family, and EXACTLY ONE member gives γ = +1 at ALL orders:");
        sb.AppendLine("              IDENTIFIED: g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|  (B = ½ln(2 − e^(2σ)))");
        sb.AppendLine("  BOUNDARY  — (a) the requirements pin γ, not B: Cassini leaves |c₂+2| ≤ "
                      + $"{SpatialSectorClosure.CassiniAllowedC2Deviation():F1};");
        sb.AppendLine("              (b) B is fixed only through γ: the nonlinear completion is unselected;");
        sb.AppendLine("              (c) AT DERIVES NO B — the survivor is selected by observation, not by the theory.");
        sb.AppendLine("  REFUTED   — B = σ: γ = −1, deflection 0, Shapiro 0, Cassini separation "
                      + $"{SpatialSectorClosure.CassiniSeparation(-1.0):E3} σ;");
        sb.AppendLine("              and with it the counting measure AS THE EQUATION FOR B.");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL: all DERIVABLE B fail — the only derivable member is B = σ, and it is excluded.");
        sb.AppendLine("  Exactly one member survives the strengthened requirement (exact γ = +1) and it is");
        sb.AppendLine("  identified above. THE SPATIAL SECTOR IS CLOSED — on a POSTULATE: the counting measure is");
        sb.AppendLine("  traded for GR optics, at a volume cost of 3.437585× the count at J0740+6620.");
        Output.WriteLine(sb.ToString());
    }
}
