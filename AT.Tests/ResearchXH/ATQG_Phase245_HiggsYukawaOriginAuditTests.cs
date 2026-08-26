using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 245 — Higgs Yukawa Origin Audit. Determine the exact status of the four Higgs-sector
/// components. Audit only — no new physics.
/// </summary>
public class ATQG_Phase245_HiggsYukawaOriginAuditTests : ResearchTestBase
{
    public ATQG_Phase245_HiggsYukawaOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2450_FourComponents()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2450: the four Higgs/Yukawa components");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Reviews QG84/140-180/203-210/244: Higgs field, Yukawa interaction, mass generation,");
        sb.AppendLine("    Higgs potential.");
        sb.AppendLine();

        sb.AppendLine("THE FOUR COMPONENTS:");
        foreach (var c in HiggsYukawaAudit.Components())
        {
            sb.AppendLine($"  {c.Name}: {c.Status}");
            sb.AppendLine($"      {c.Evidence}");
        }
        sb.AppendLine();
        sb.AppendLine($"By status: {string.Join(", ", HiggsYukawaAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, HiggsYukawaAudit.Components().Length);
        var sc = HiggsYukawaAudit.StatusCounts();
        Assert.Equal(0, sc[HiggsYukawaAudit.Status.Derived]);
        Assert.Equal(2, sc[HiggsYukawaAudit.Status.Partial]);
        Assert.Equal(0, sc[HiggsYukawaAudit.Status.Hosted]);
        Assert.Equal(2, sc[HiggsYukawaAudit.Status.Open]);
    }

    [Fact]
    public void ATQG2451_MissingComponents()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2451: the exact missing SM dynamics components");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The gauge dynamics is derived (QG243/244); the Higgs/Yukawa sector is the remainder.");
        sb.AppendLine();

        sb.AppendLine("MISSING COMPONENTS:");
        foreach (var m in HiggsYukawaAudit.MissingComponents())
            sb.AppendLine($"  • {m}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, HiggsYukawaAudit.MissingComponents().Length);
    }

    [Fact]
    public void ATQG2452_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2452: summary — SM DYNAMICS NOT COMPLETE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - SM dynamics is complete iff no Higgs/Yukawa component is OPEN or PARTIAL.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {HiggsYukawaAudit.Summary()}");
        sb.AppendLine();
        sb.AppendLine($"SM dynamics complete? {HiggsYukawaAudit.SmDynamicsComplete()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The Higgs FIELD is identified (the collective occupation-density scalar, QG84/161/169);");
        sb.AppendLine("    the fermion mass VALUES are derived spectrally (QG140-210).");
        sb.AppendLine("  - The YUKAWA interaction and the HIGGS POTENTIAL are OPEN (not derived from D96); the");
        sb.AppendLine("    mass-generation MECHANISM (m_f = y_f·v) is PARTIAL.");
        sb.AppendLine("  - These are the exact remaining Standard Model dynamics components after QG244.");

        Output.WriteLine(sb.ToString());

        Assert.False(HiggsYukawaAudit.SmDynamicsComplete(), "SM dynamics is not complete (Yukawa + potential open)");
        Assert.Contains("NOT COMPLETE", HiggsYukawaAudit.Summary());
    }
}
