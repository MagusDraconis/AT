using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 39 — separate TRM into derived and non-derived sectors. Audits six observables into SATURATION /
/// PSI / BOTH.
///
/// Tests: ATQG390 (sector census), ATQG391 (the BOTH case), ATQG392 (summary).
/// </summary>
public class ATQG_Phase39_TRMSectorAuditTests : ResearchTestBase
{
    public ATQG_Phase39_TRMSectorAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG390: sector census ────────────────────────────────────────────────────────

    [Fact]
    public void ATQG390_SectorCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG390: SATURATION / PSI / BOTH for six TRM observables");

        int saturation = 0, psi = 0, both = 0;
        foreach (var ob in TRMSectorAudit.Observables)
        {
            string c = TRMSectorAudit.Classify(ob);
            sb.AppendLine($"{ob,-24} -> {c}");
            switch (c)
            {
                case "SATURATION": saturation++; break;
                case "PSI": psi++; break;
                case "BOTH": both++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"SATURATION (derived scalar): {saturation}");
        sb.AppendLine($"PSI (new tensor primitive):  {psi}");
        sb.AppendLine($"BOTH:                        {both}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: only redshift is pure scalar; lensing, PPN, horizon thermodynamics, and GWs are all PSI;");
        sb.AppendLine("regular black holes need both (scalar core + tensor horizon).");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, saturation);
        Assert.Equal(4, psi);
        Assert.Equal(1, both);
    }

    // ── ATQG391: the BOTH case ────────────────────────────────────────────────────────

    [Fact]
    public void ATQG391_BothCase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG391: regular black hole = scalar core (saturation) + tensor horizon (ψ)");

        bool coreSaturation = TRMSectorAudit.RegularCoreNeedsSaturation();
        bool horizonPsi = TRMSectorAudit.HorizonNeedsPsi();

        sb.AppendLine($"regular CORE  from saturation (Poisson profile, QG36): {coreSaturation}");
        sb.AppendLine($"HORIZON       from ψ (non-conformal, QG33/35):         {horizonPsi}");

        bool both = coreSaturation && horizonPsi;

        sb.AppendLine();
        sb.AppendLine($"regular black hole = BOTH sectors: {both}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a regular black hole is genuinely composite — the finite-curvature core is derived from Q-event");
        sb.AppendLine("saturation, but the horizon (and hence the black hole itself) requires the non-conformal ψ.");
        Output.WriteLine(sb.ToString());

        Assert.True(both, "regular black hole should require both sectors");
        Assert.Equal("BOTH", TRMSectorAudit.Classify("regular-black-hole"));
    }

    // ── ATQG392: summary ──────────────────────────────────────────────────────────────

    [Fact]
    public void ATQG392_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG392: what TRM contributes from each sector");

        sb.AppendLine("DERIVED SCALAR SECTOR (saturation physics):");
        sb.AppendLine("  • redshift (g_00 = −ρ^(2/d), no ψ)");
        sb.AppendLine("  • the regular CORE of a black hole (Poisson profile)");
        sb.AppendLine();
        sb.AppendLine("NEW TENSOR PRIMITIVE (ψ physics):");
        sb.AppendLine("  • lensing, PPN recovery, horizon thermodynamics, GW phenomenology");
        sb.AppendLine("  • the HORIZON of a regular black hole");
        sb.AppendLine();
        sb.AppendLine("SUMMARY: TRM's payload is overwhelmingly ψ/tensor (4 of 6 observables + the horizon). Only redshift and the");
        sb.AppendLine("regular core are derived scalar physics. This is the final sector separation: AT derives the scalar backbone");
        sb.AppendLine("and the regular core; the tensor sector (ψ) is the single non-derived ingredient carrying lensing/GWs/horizons.");

        bool redshiftScalar = !TRMSectorAudit.RedshiftNeedsPsi();
        bool lensingPsi = TRMSectorAudit.LensingNeedsPsi();
        bool ppnPsi = TRMSectorAudit.PpnNeedsPsi();
        bool gwPsi = TRMSectorAudit.GwNeedsPsi();

        sb.AppendLine();
        sb.AppendLine($"redshift scalar (no ψ): {redshiftScalar}   lensing/PPN/GW need ψ: {lensingPsi && ppnPsi && gwPsi}");
        Output.WriteLine(sb.ToString());

        Assert.True(redshiftScalar, "redshift should not need psi");
        Assert.True(lensingPsi && ppnPsi && gwPsi, "lensing/PPN/GW should need psi");
    }
}
