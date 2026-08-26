using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 60 — Standard Model compatibility. Audits whether the network hosts the Standard Model.
/// Classify: NATURAL / COMPATIBLE / UNKNOWN.
///
/// Tests: ATQG600 (classification), ATQG601 (native spin content), ATQG602 (conclusion).
/// </summary>
public class ATQG_Phase60_StandardModelCompatibilityTests : ResearchTestBase
{
    public ATQG_Phase60_StandardModelCompatibilityTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG600: classification ─────────────────────────────────────────────────────

    [Fact]
    public void ATQG600_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG600: NATURAL / COMPATIBLE / UNKNOWN for Standard Model ingredients");

        int natural = 0, compatible = 0, unknown = 0;
        foreach (var ing in StandardModelCompatibility.Ingredients)
        {
            string c = StandardModelCompatibility.Classify(ing);
            sb.AppendLine($"{ing,-20} -> {c}");
            switch (c)
            {
                case "NATURAL": natural++; break;
                case "COMPATIBLE": compatible++; break;
                case "UNKNOWN": unknown++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"NATURAL    : {natural}");
        sb.AppendLine($"COMPATIBLE : {compatible}");
        sb.AppendLine($"UNKNOWN    : {unknown}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, natural);
        Assert.Equal(2, compatible);
        Assert.Equal(1, unknown);
    }

    // ── ATQG601: native spin content ────────────────────────────────────────────────

    [Fact]
    public void ATQG601_NativeSpinContent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG601: the network natively produces spin-0 and spin-2 only");

        double[] spins = StandardModelCompatibility.NativeSpins();
        bool gaugeOnLinks = StandardModelCompatibility.GaugeFieldsLiveOnLinks();
        bool fermionsNative = StandardModelCompatibility.FermionsNative();
        bool chargeScalar = StandardModelCompatibility.ChargeIsScalarLabel();

        sb.AppendLine($"native spins: {string.Join(", ", spins)}  (trace ρ, traceless ψ)");
        sb.AppendLine($"gauge fields live on links (connections): {gaugeOnLinks}");
        sb.AppendLine($"fermions are native to the network:       {fermionsNative}");
        sb.AppendLine($"charge is a scalar node label:             {chargeScalar}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network has spin-0 (nodes/trace) and spin-2 (links/traceless). Gauge fields (spin-1) fit on");
        sb.AppendLine("the links as connections; charge fits on the nodes as a scalar label; fermions (spin-1/2) have no native home.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, spins.Length);
        Assert.True(gaugeOnLinks, "gauge fields should live on links");
        Assert.False(fermionsNative, "fermions should not be native");
        Assert.True(chargeScalar, "charge should be a scalar label");
    }

    // ── ATQG602: conclusion ─────────────────────────────────────────────────────────

    [Fact]
    public void ATQG602_Conclusion()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG602: what the network can and cannot host");

        sb.AppendLine("CAN HOST:");
        sb.AppendLine("  • charge (NATURAL — a scalar quantum-number label on nodes)");
        sb.AppendLine("  • gauge fields and spin-1 interactions (COMPATIBLE — connections on the links)");
        sb.AppendLine();
        sb.AppendLine("CANNOT NATIVELY HOST:");
        sb.AppendLine("  • fermions (UNKNOWN — spin-1/2 spinors have no home in a scalar-node + rank-2-link network)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: AT's causal network is a theory of GRAVITY (spin-0 + spin-2). It accommodates charge and gauge");
        sb.AppendLine("fields on its nodes/links, but the Standard Model's fermionic sector would require a genuinely new primitive");
        sb.AppendLine("(spin-1/2) — consistent with AT being a gravitational/completion framework, not a full matter theory.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NATURAL", StandardModelCompatibility.Classify("charge"));
        Assert.Equal("COMPATIBLE", StandardModelCompatibility.Classify("gauge-fields"));
        Assert.Equal("UNKNOWN", StandardModelCompatibility.Classify("fermions"));
    }
}
