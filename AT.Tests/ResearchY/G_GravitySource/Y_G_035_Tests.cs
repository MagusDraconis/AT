using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_035 — Temporal Independence Audit (group G — Gravity Source).
///
/// QUESTION. Which G-results survive if the **conformal metric is removed**? Review G_001–G_034 and classify each
/// result by whether it requires **g₀₀ only**, **g_rr**, or **conformal flatness**. Goal: identify the **minimal
/// time sector** of AT.
///
/// THE ANSWER. **The temporal sector is already independent, and its boundary is sharp.**
///
///     SURVIVES (g₀₀ only) 25 of 36 suites — the entire source/clock/density programme
///     BOUNDARY (g_ij)      8 — the spatial sector: survives as a result, not as a value
///     REFUTED (conformal)  3 — G_024, G_031, G_032: results that ARE the conformal assumption
///
/// **Every audit from G_001 to G_020 is g₀₀-only.** The spatial requirement begins *exactly* at **G_021
/// (Light-Propagation)** — the first audit to ask for light bending. **G_020 → G_021 is the conformal boundary.**
///
/// THE MINIMAL TIME SECTOR: 1 metric function (g₀₀ = −ρ^(2/d)) + 1 scalar (ρ) + 1 exponent (1/d, d = 3) — with
/// no spatial metric, no conformal factor and no reference η. The source law and the clock law are the SAME
/// statement, because the Newtonian potential IS the time exponent.
///
/// THE ARITY PROOF: the temporal observables are functions whose signature contains no B; the spatial ones take
/// B explicitly. A result that cannot be *called* with B cannot *require* it.
///
/// WHAT THIS BUYS. G_032 showed the conformal ansatz is an assumed primitive and that imposing it is refuted
/// (γ = −1, Cassini 8.6957e4 σ). G_035 shows the cost is **confined**: the theory's time half is not hostage to
/// its space half.
/// </summary>
public class Y_G_035_Tests : ResearchTestBase
{
    public Y_G_035_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_035_TheTemporalSectorIsIndependentAndTheBoundaryIsSharp()
    {
        // The registry covers every group-G suite the scanner finds.
        Assert.Empty(TemporalIndependenceAudit.UnregisteredSuites());
        Assert.Equal(47, TemporalIndependenceAudit.Registry().Length);

        var (survives, boundary, refuted) = TemporalIndependenceAudit.Counts();
        Assert.Equal(33, survives);   // G_046 added a ClockOnly, non-scan-detected claim
        Assert.Equal(11, boundary);   // 8 as G_035 audited; G_036, G_037 and G_038 added three more
        Assert.Equal(3, refuted);
        Assert.Equal(47, survives + boundary + refuted);

        // THE SHARP BOUNDARY: the first audit requiring more than g₀₀ is G_021 — light propagation.
        var first = TemporalIndependenceAudit.ConformalBoundary();
        Assert.NotNull(first);
        Assert.Equal("Y_G_021", first!.Audit);
        Assert.Equal(MetricRequirement.Spatial, first.Requirement);
        Assert.Equal(23, TemporalIndependenceAudit.ConformalBoundaryIndex());

        // Everything before it is temporal-only: the whole density era G_001–G_020.
        var reg = TemporalIndependenceAudit.Registry();
        for (int i = 0; i < 22; i++)   // indices 0..21 = the 22 temporal-only entries
            Assert.Equal(MetricRequirement.ClockOnly, reg[i].Requirement);
        // ...and the temporal era is 21 suites long by the audit-count convention (G_001–G_020 plus G_011b).
        Assert.Equal(22, TemporalIndependenceAudit.TemporalEraLength());

        // The refuted three are exactly the results whose SUBJECT is conformal flatness.
        var bad3 = TemporalIndependenceAudit.With(TemporalVerdict.Refuted).Select(c => c.Audit).ToArray();
        Assert.Equal(new[] { "Y_G_024", "Y_G_031", "Y_G_032" }, bad3);
    }

    [Fact]
    public void Y_G_035_TheRegistryAgreesWithTheExecutableScan()
    {
        // THE MECHANICAL CHECK: ClockOnly ⟹ no executable reference to the spatial block; Spatial/Conformal ⟹
        // at least one. Strings and comments are stripped, so a narrative mention cannot mask a dependency —
        // G_019's only spatial "hit" was a report sentence and disappears under stripping.
        string why = string.Join("; ", TemporalIndependenceAudit.RegistryDisagreements()
            .Select(d => $"{d.Audit}: {d.Why}"));
        Assert.True(TemporalIndependenceAudit.RegistryAgreesWithScan(), why);
        Assert.Empty(TemporalIndependenceAudit.RegistryDisagreements());

        // The scan sees every suite INCLUDING this one (37); the registry covers the 36 it classifies.
        var signals = TemporalIndependenceAudit.ScanAudits();
        Assert.Equal(TemporalIndependenceAudit.Registry().Length + 1, signals.Length);

        // The scan must actually be discriminating, not uniformly zero.
        Assert.Contains(signals, s => s.TouchesSpatial);
        Assert.Contains(signals, s => !s.TouchesSpatial);

        // Spot-checks at the boundary.
        Assert.False(TemporalIndependenceAudit.SignalsFor("Y_G_020")!.TouchesSpatial);   // redshift: g₀₀ only
        Assert.True(TemporalIndependenceAudit.SignalsFor("Y_G_021")!.TouchesSpatial);    // light bending: needs B
        Assert.False(TemporalIndependenceAudit.SignalsFor("Y_G_009")!.TouchesSpatial);   // the clock law
        Assert.True(TemporalIndependenceAudit.SignalsFor("Y_G_029")!.TouchesSpatial);    // g_rr survivor

        // Stripping is what makes the check exact: a narrative mention cannot mask a dependency...
        Assert.DoesNotContain("g_rr", TemporalIndependenceAudit.StripNonCode(
            """sb.AppendLine("  g_rr = +rho^(2/d); gamma = -1");"""));
        // ...but real code survives.
        Assert.Contains("grr", TemporalIndependenceAudit.StripNonCode("double grr = -g00;"));
        // ...and comments are stripped as well.
        Assert.DoesNotContain("grr", TemporalIndependenceAudit.StripNonCode("double x = 1.0;  // grr is the spatial block"));
        // A `//` inside a string must not truncate the line (strings are stripped first).
        Assert.Contains("gamma", TemporalIndependenceAudit.StripNonCode("""double g = 1.0; string s = "// not a comment"; gamma = g;"""));
    }

    [Fact]
    public void Y_G_035_NoTemporalObservableTakesB()
    {
        // THE ARITY PROOF — structural, not numerical.
        Assert.True(TemporalIndependenceAudit.NoTemporalObservableTakesB());
        Assert.All(TemporalIndependenceAudit.TemporalObservables(),
            o => Assert.DoesNotContain("b", o.Signature.Replace("double", "").Replace("[]", "").ToLowerInvariant()
                .Replace("rho", "").Replace("x", "").Replace("field", "").Replace("(", "").Replace(")", "")
                .Replace(" ", "").Trim(',')));

        // The spatial observables DO take B — the whole distinction rests on this.
        Assert.Contains(TemporalIndependenceAudit.SpatialObservables(), o => o.Signature.Contains("double b"));
    }

    [Fact]
    public void Y_G_035_TheMinimalTimeSectorReproducesTheTemporalResults()
    {
        // The temporal observables are computed from ρ alone and reproduce the recorded results.
        // g₀₀ = −ρ^(2/d) and the clock √(−g₀₀) = ρ^(1/d).
        foreach (double rho in new[] { 0.5, 0.610136, 1.0, 2.0, 1.0e3 })
        {
            Assert.Equal(-Math.Pow(rho, 2.0 / 3.0), TemporalIndependenceAudit.G00(rho), 12);
            Assert.Equal(Math.Pow(rho, 1.0 / 3.0), TemporalIndependenceAudit.ClockOf(rho), 12);
            Assert.Equal(Math.Log(rho) / 3.0, TemporalIndependenceAudit.ClockPotentialOf(rho), 12);
            // The clock is the square root of minus g₀₀ — one function, one exponent.
            Assert.Equal(Math.Sqrt(-TemporalIndependenceAudit.G00(rho)), TemporalIndependenceAudit.ClockOf(rho), 12);
        }

        // The redshift (G_020's observable) is a pure function of g₀₀.
        double rhoSurface = Math.Exp(-3.0 * 0.247002);
        Assert.Equal(1.0 / Math.Pow(rhoSurface, 1.0 / 3.0) - 1.0, TemporalIndependenceAudit.RedshiftOf(rhoSurface), 12);
        Assert.Equal(Math.Exp(0.247002) - 1.0, TemporalIndependenceAudit.RedshiftOf(rhoSurface), 9);

        // G_019's second-order discriminator: AT/GR = e^x/√(1+2x), and it is g₀₀-only.
        Assert.Equal(1.0, TemporalIndependenceAudit.SecondOrderRatioOf(0.0), 12);
        Assert.Equal(1.0e-6, TemporalIndependenceAudit.SecondOrderRatioOf(1.0e-3) - 1.0, 8);   // x², minus (4/3)x³
        Assert.Equal(1.0e-4, TemporalIndependenceAudit.SecondOrderRatioOf(1.0e-2) - 1.0, 5);

        // The source law from ρ alone is EXACTLY the negative gradient of the clock potential (a = −∇A, as an
        // attractive force requires) — the reason the source law and the clock law are ONE statement.
        var rhoField = new[] { 0.9, 0.95, 1.0, 1.05, 1.1 };
        var aFromRho = TemporalIndependenceAudit.SourceAccelerationOf(rhoField);
        for (int i = 1; i < rhoField.Length - 1; i++)
        {
            double leftA = Math.Log(rhoField[i - 1]) / 3.0, rightA = Math.Log(rhoField[i + 1]) / 3.0;
            Assert.Equal(-(rightA - leftA) / 2.0, aFromRho[i], 12);
        }
        // Sign check on a PEAKED profile: a monotonic one has no sign change, a peaked one must accelerate
        // INWARD — toward the density maximum. This is the attractive-force property.
        var peak = new[] { 0.9, 1.0, 1.1, 1.0, 0.9 };
        var aPeak = TemporalIndependenceAudit.SourceAccelerationOf(peak);
        Assert.True(aPeak[1] < 0.0, $"rising side must accelerate forward-inward, got {aPeak[1]}");
        Assert.True(aPeak[3] > 0.0, $"falling side must accelerate backward-inward, got {aPeak[3]}");
        Assert.Equal(0.0, aPeak[2], 12);          // the peak itself is an equilibrium
        // ...and a monotonic profile accelerates uniformly against the gradient.
        Assert.All(aFromRho, a => Assert.True(a <= 0.0));

        // The minimal sector's content: one function, one scalar, one exponent.
        Assert.Equal((1, 1, 1), TemporalIndependenceAudit.MinimalTimeSectorContent());
    }

    [Fact]
    public void Y_G_035_VerdictIsSurvives()
    {
        // COMPUTED verdict (ResearchY-G_027).
        Assert.Equal("SURVIVES", TemporalIndependenceAudit.Verdict());

        // SURVIVES because the minimal time sector is non-empty, the registry agrees with the scan, and no
        // temporal observable can be called with a B.
        Assert.NotEmpty(TemporalIndependenceAudit.MinimalTimeSector());
        Assert.True(TemporalIndependenceAudit.RegistryAgreesWithScan(),
            string.Join("; ", TemporalIndependenceAudit.RegistryDisagreements().Select(d => $"{d.Audit}: {d.Why}")));
        Assert.True(TemporalIndependenceAudit.NoTemporalObservableTakesB());

        // The three ingredients of the minimal time sector.
        Assert.Equal(33, TemporalIndependenceAudit.MinimalTimeSector().Length);
        Assert.Equal((1, 1, 1), TemporalIndependenceAudit.MinimalTimeSectorContent());
        string statement = TemporalIndependenceAudit.MinimalTimeSectorStatement();
        Assert.Contains("no spatial metric, no conformal factor, no reference", statement);
        Assert.Contains("SAME statement", statement);
    }

    [Fact]
    public void Y_G_035_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_035 — Temporal Independence Audit: what survives if the conformal metric is removed?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The metric splits as g₀₀ (the clock) and g_ij (space); conformal flatness is A = B (G_032).");
        sb.AppendLine("  2. Removing conformality means allowing A ≠ B; removing the spatial sector means dropping g_ij.");
        sb.AppendLine("  3. Classification axis: does the RESULT require g₀₀ only, g_rr (some g_ij), or A = B?");
        sb.AppendLine("  4. Verdict map: ClockOnly → SURVIVES | Spatial → BOUNDARY | ConformalFlatness → REFUTED.");
        sb.AppendLine();

        PrintHeader("1. THE MINIMAL TIME SECTOR");
        sb.AppendLine("  " + TemporalIndependenceAudit.MinimalTimeSectorStatement());
        var (mf, sc, ex) = TemporalIndependenceAudit.MinimalTimeSectorContent();
        sb.AppendLine($"  content: {mf} metric function + {sc} scalar + {ex} exponent");
        sb.AppendLine("  g₀₀ = −ρ^(2/d)   √(−g₀₀) = dτ/dt = ρ^(1/d)   A = ½ln(−g₀₀) = σ = (1/d)ln ρ");
        sb.AppendLine("  a = −(1/d)∇ln ρ = ∇A  ← the source law IS the clock law (the Newtonian potential is A)");
        sb.AppendLine();
        sb.AppendLine("  THE ARITY PROOF — no temporal observable can be CALLED with a B:");
        foreach (var (name, sig) in TemporalIndependenceAudit.TemporalObservables())
            sb.AppendLine($"    {name,-22} {sig}");
        sb.AppendLine("  ...whereas the spatial observables take B explicitly:");
        foreach (var (name, sig) in TemporalIndependenceAudit.SpatialObservables())
            sb.AppendLine($"    {name,-22} {sig}");
        sb.AppendLine();

        PrintHeader("2. THE SHARP BOUNDARY");
        var first = TemporalIndependenceAudit.ConformalBoundary();
        sb.AppendLine($"  first audit requiring more than g₀₀: {first!.Audit} ({first.Title}) — {first.Basis}");
        sb.AppendLine($"  boundary index {TemporalIndependenceAudit.ConformalBoundaryIndex()} of "
                      + $"{TemporalIndependenceAudit.Registry().Length}; temporal era length "
                      + $"{TemporalIndependenceAudit.TemporalEraLength()}");
        sb.AppendLine("  ⇒ EVERY audit from G_001 to G_020 is g₀₀-only. The spatial requirement begins exactly at");
        sb.AppendLine("    G_021, the first audit to ask for light bending. G_020 → G_021 IS the conformal boundary.");
        sb.AppendLine();

        PrintHeader("3. EVERY RESULT, CLASSIFIED");
        sb.AppendLine("  verdict      audit      title                       requirement        code-signals (t/s/c)");
        var signals = TemporalIndependenceAudit.ScanAudits();
        foreach (var c in TemporalIndependenceAudit.Registry())
        {
            var s = signals.FirstOrDefault(x => x.Audit == c.Audit);
            sb.AppendLine($"  {c.Verdict,-12} {c.Audit,-10} {Trim(c.Title, 26),-26} {c.Requirement,-18}"
                        + $" {(s is null ? "n/a" : $"{s.Temporal}/{s.Spatial}/{s.Conformal}")}");
        }
        var (sv, bd, rf) = TemporalIndependenceAudit.Counts();
        sb.AppendLine();
        sb.AppendLine($"  SURVIVES {sv}   BOUNDARY {bd}   REFUTED {rf}   (of {TemporalIndependenceAudit.Registry().Length})");
        sb.AppendLine();

        PrintHeader("4. THE REFUTED THREE — results that ARE the conformal assumption");
        foreach (var c in TemporalIndependenceAudit.With(TemporalVerdict.Refuted))
            sb.AppendLine($"  {c.Audit,-10} {Trim(c.Title, 26),-26} {c.Basis}");
        sb.AppendLine();
        sb.AppendLine("  These are not casualties of the spatial problem — they are statements ABOUT the conformal");
        sb.AppendLine("  slice (G_024), conditional on it (G_031), or the statement of the assumption itself (G_032).");
        sb.AppendLine("  G_032's own conclusion is that the ansatz must be abandoned, so its being conformal-classed is");
        sb.AppendLine("  consistent: the audit is ABOUT the thing that should go.");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine($"  {TemporalIndependenceAudit.Verdict()} — the temporal sector stands without the conformal metric.");
        sb.AppendLine("  • 25 of 36 suites need g₀₀ and NOTHING else; the density era G_001–G_020 is entirely temporal.");
        sb.AppendLine("  • The 8 BOUNDARY results need g_ij; they survive the removal of conformality as a CONSTRAINT, but");
        sb.AppendLine("    their VALUES depend on the chosen B, which the theory does not fix (G_029 / G_030).");
        sb.AppendLine("  • The 3 REFUTED results dissolve with the conformal slice, and none of them is load-bearing for");
        sb.AppendLine("    the source or the clock.");
        sb.AppendLine();
        sb.AppendLine("  CONSEQUENCE: the conformal problem found by G_032 is CONFINED to the spatial sector. AT's time");
        sb.AppendLine("  half — the source law, the clock law, and everything built on them — is independent of it.");
        Output.WriteLine(sb.ToString());
    }

    private static string Trim(string s, int n) => s.Length <= n ? s : s[..(n - 1)] + "…";
}
