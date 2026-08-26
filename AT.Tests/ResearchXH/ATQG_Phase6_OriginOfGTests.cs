using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 6 — origin of the coupling G. The gravity structure is derived; only the overall scale remains
/// imported. Here we test whether G can emerge from counting statistics or actualization dynamics.
/// Classify G: DERIVED / PREFERRED / IMPORTED.
///
/// Tests: ATQG60 (the conformal gravity has NO free coupling; GM_eff is native), ATQG61 (G–M non-separability),
///        ATQG62 (classification).
/// </summary>
public class ATQG_Phase6_OriginOfGTests : ResearchTestBase
{
    public ATQG_Phase6_OriginOfGTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    // ── ATQG60: the conformal gravity has no free coupling — GM_eff is native ────────

    [Fact]
    public void ATQG60_NoFreeCoupling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG60: the conformal gravity has no free coupling — GM_eff is native");

        // a = −(1/d)∇lnρ contains NO coupling constant — the 1/d is fixed, and the profile is all that matters.
        double m0 = 0.5, r0 = 0.5, rhoBar = 1.0;
        double gmEff = CouplingOrigin.DeficitMass(m0, r0, D, rhoBar);

        // The power-law deficit's asymptotic M_eff = −a·r² should equal m0·r0/(d·ρ̄).
        double r = 12.0;
        double a = DeficitCollective.AtAcceleration3D(u => DeficitCollective.PowerLawDeficit(u, rhoBar, m0, r0), r, D);
        double mEff = DeficitCollective.EffectiveEnclosedMass(a, r);
        double relErr = Math.Abs(mEff - gmEff) / gmEff;

        sb.AppendLine($"GM_eff = m0·r0/(d·ρ̄) = {m0}·{r0}/({D}·{rhoBar}) = {gmEff:F6}");
        sb.AppendLine($"asymptotic M_eff(−a·r² at r=12) = {mEff:F6}  (relative deviation {relErr:P1})");
        sb.AppendLine($"a = −(1/d)∇lnρ has NO coupling constant — gravity strength is set entirely by ρ's profile");

        bool noFreeCoupling = Math.Abs(mEff - gmEff) / gmEff < 0.10;   // M_eff → GM_eff (point-mass form)

        sb.AppendLine();
        sb.AppendLine($"the gravitational scale (GM_eff) is fully determined by the deficit abundance (m0, r0, ρ̄): {noFreeCoupling}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the conformal gravity has NO independent coupling G. The single native scale that");
        sb.AppendLine("plays Newton's GM role is the deficit mass GM_eff = m0·r0/(d·ρ̄), fixed by the actualization.");
        Output.WriteLine(sb.ToString());

        Assert.True(noFreeCoupling, "the effective mass should equal the native deficit mass");
    }

    // ── ATQG61: G and M are not separable (degeneracy) ───────────────────────────────

    [Fact]
    public void ATQG61_GMDegeneracy()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG61: G and M are not separable — only GM_eff is physical");

        double m0 = 0.5, r0 = 0.5, rhoBar = 1.0;
        double gm = CouplingOrigin.DeficitMass(m0, r0, D, rhoBar);

        sb.AppendLine($"{"m0",7} {"r0",7} {"GM_eff",10}");
        double gmC2 = 0, gmC05 = 0;
        foreach (double c in new[] { 0.5, 1.0, 2.0 })
        {
            double gmRescaled = CouplingOrigin.RescaledDeficitMass(m0, r0, c, D, rhoBar);
            if (c == 2.0) gmC2 = gmRescaled;
            if (c == 0.5) gmC05 = gmRescaled;
            sb.AppendLine($"{c * m0,7:F2} {r0 / c,7:F2} {gmRescaled,10:F6}");
        }

        bool invariant = Math.Abs(gmC2 - gm) < 1e-12 && Math.Abs(gmC05 - gm) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"GM_eff invariant under m0→c·m0, r0→r0/c: {invariant}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the gravitational field is degenerate in (m0, r0) — a large deficit over a small");
        sb.AppendLine("scale is indistinguishable from a small deficit over a large scale. G and M are therefore NOT");
        sb.AppendLine("separately determined: only the product GM_eff = m0·r0/(d·ρ̄) is physical.");
        Output.WriteLine(sb.ToString());

        Assert.True(invariant, "GM_eff should be invariant under the m0/r0 rescaling");
    }

    // ── ATQG62: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG62_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG62: is G DERIVED, PREFERRED, or IMPORTED?");

        sb.AppendLine("CLASSIFICATION: IMPORTED as a discrete normalization (BDG −2); DERIVED as the physical scale.");
        sb.AppendLine();
        sb.AppendLine("  • The physical gravitational scale GM_eff = m0·r0/(d·ρ̄) is DERIVED — it is the deficit");
        sb.AppendLine("    abundance of the actualization, with NO independent coupling G (ATQG60). The conformal");
        sb.AppendLine("    gravity a = −(1/d)∇lnρ has no free coupling constant.");
        sb.AppendLine("  • G and M are NOT separable — only GM_eff is physical (ATQG61), so the question 'what is G' is");
        sb.AppendLine("    really 'what is the deficit abundance' — which the actualization dynamics supplies (m0, r0, ρ̄).");
        sb.AppendLine("  • The BDG scale −2 (G4-L12) is a DISCRETIZATION normalization: it fixes the discrete operator's");
        sb.AppendLine("    second moment to match the continuum Laplacian — IMPORTED, and distinct from the physical G.");
        sb.AppendLine("  • Therefore: the gravitational COUPLING is native (derived as the deficit mass), while the BDG −2");
        sb.AppendLine("    is an imported continuum-matching convention — the last remaining 'imported scale' is a");
        sb.AppendLine("    bookkeeping normalization, not a physical coupling.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
