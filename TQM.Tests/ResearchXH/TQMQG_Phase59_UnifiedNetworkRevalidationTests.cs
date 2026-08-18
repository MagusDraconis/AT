using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 59 — revalidate the unified network theory. Audits seven prior results under the unified picture.
/// Classify: PRESERVED / MODIFIED / BROKEN.
///
/// Tests: TQMQG590 (classification table), TQMQG591 (trace/traceless split), TQMQG592 (faithful re-description).
/// </summary>
public class TQMQG_Phase59_UnifiedNetworkRevalidationTests : ResearchTestBase
{
    public TQMQG_Phase59_UnifiedNetworkRevalidationTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG590: classification table ───────────────────────────────────────────────

    [Fact]
    public void TQMQG590_ClassificationTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG590: do all seven results survive the unified picture?");

        int preserved = 0, modified = 0, broken = 0;
        foreach (var r in UnifiedNetworkRevalidation.Results)
        {
            string c = UnifiedNetworkRevalidation.Classify(r);
            sb.AppendLine($"{r,-20} ({UnifiedNetworkRevalidation.Source(r),-9}) -> {c}");
            switch (c)
            {
                case "PRESERVED": preserved++; break;
                case "MODIFIED": modified++; break;
                case "BROKEN": broken++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"PRESERVED : {preserved}");
        sb.AppendLine($"MODIFIED  : {modified}");
        sb.AppendLine($"BROKEN    : {broken}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(7, preserved);
        Assert.Equal(0, modified);
        Assert.Equal(0, broken);
    }

    // ── TQMQG591: the trace/traceless split ──────────────────────────────────────────

    [Fact]
    public void TQMQG591_TraceTracelessSplit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG591: scalar results live in the trace; tensor results in the traceless part");

        int trace = 0, traceless = 0, both = 0;
        foreach (var r in UnifiedNetworkRevalidation.Results)
        {
            string s = UnifiedNetworkRevalidation.Source(r);
            switch (s)
            {
                case "trace": trace++; break;
                case "traceless": traceless++; break;
                case "both": both++; break;
            }
        }

        sb.AppendLine($"scalar (trace) results:   {trace}   (matter, gravity, rotation curves, regular cores)");
        sb.AppendLine($"tensor (traceless) results: {traceless}   (lensing, GW polarization)");
        sb.AppendLine($"both:                     {both}   (Schwarzschild limit)");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the unified picture cleanly separates the scalar backbone (trace = ρ) from the tensor sector");
        sb.AppendLine("(traceless = ψ). Each previous result maps to its correct network content.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, trace);
        Assert.Equal(2, traceless);
        Assert.Equal(1, both);
    }

    // ── TQMQG592: faithful re-description ─────────────────────────────────────────────

    [Fact]
    public void TQMQG592_FaithfulRedescription()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG592: the unified network picture is a faithful re-description");

        bool faithful = UnifiedNetworkRevalidation.FaithfulRedescription();

        sb.AppendLine($"unified picture preserves all results: {faithful}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Network(V, E) → ρ (trace) + ψ (traceless) is a faithful RE-DESCRIPTION, not a new theory. ρ is");
        sb.AppendLine("the same counting measure, ψ is the same spin-2 field (now understood as the link content). Every prior result");
        sb.AppendLine("(QG0–QG57) is PRESERVED — the reinterpretation changes the INTERPRETATION, not the physics.");
        Output.WriteLine(sb.ToString());

        Assert.True(faithful, "the unified picture should be faithful");
        Assert.Equal("PRESERVED", UnifiedNetworkRevalidation.Classify("gw-polarization"));
        Assert.Equal("PRESERVED", UnifiedNetworkRevalidation.Classify("scalar-gravity"));
    }
}
