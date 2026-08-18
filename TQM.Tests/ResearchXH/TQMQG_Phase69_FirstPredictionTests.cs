using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 69 — first unique prediction of the unified network theory.
/// Classify: UNIQUE / TESTABLE / FALSIFIABLE.
///
/// Tests: TQMQG690 (signature census), TQMQG691 (the unique prediction), TQMQG692 (classification).
/// </summary>
public class TQMQG_Phase69_FirstPredictionTests : ResearchTestBase
{
    public TQMQG_Phase69_FirstPredictionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG690: signature census ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG690_SignatureCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG690: which signatures are unique to the network?");

        int unique = 0, notUnique = 0;
        foreach (var s in FirstPrediction.Signatures)
        {
            bool u = FirstPrediction.Unique(s);
            sb.AppendLine($"{s,-22} -> UNIQUE: {u}");
            if (u) unique++; else notUnique++;
        }

        sb.AppendLine();
        sb.AppendLine($"unique: {unique}   not-unique: {notUnique}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: GWs, lensing, regular cores, and quantum coherence all reproduce GR/SM results. Only NETWORK");
        sb.AppendLine("DISCRETENESS (spacetime granularity) is absent from GR and the Standard Model.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, unique);
        Assert.Equal(4, notUnique);
        Assert.True(FirstPrediction.Unique("network-discreteness"));
        Assert.False(FirstPrediction.Unique("gw"));
    }

    // ── TQMQG691: the unique prediction ─────────────────────────────────────────────

    [Fact]
    public void TQMQG691_UniquePrediction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG691: the unique prediction is a common discreteness scale");

        bool common = FirstPrediction.CommonDiscretenessScale();
        bool fixedScale = FirstPrediction.DiscretenessScaleFixed();

        sb.AppendLine($"all four sectors share a COMMON discreteness scale: {common}");
        sb.AppendLine($"the discreteness scale is FIXED by the theory:      {fixedScale}");
        sb.AppendLine();
        sb.AppendLine("PREDICTION: spacetime — and all four sectors (ρ, ψ, θ, S) — is granular at a single common scale, because");
        sb.AppendLine("the link is a discrete object carrying all four. Neither GR nor the SM predicts this common granularity.");
        sb.AppendLine("CAVEAT: the scale is a free parameter (QG14/QG38), so the prediction is qualitative (there IS a scale), not");
        sb.AppendLine("quantitative (its value is unfixed).");
        Output.WriteLine(sb.ToString());

        Assert.True(common, "all sectors should share a common discreteness scale");
        Assert.False(fixedScale, "the scale should not be fixed");
    }

    // ── TQMQG692: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG692_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG692: UNIQUE / TESTABLE / FALSIFIABLE?");

        bool testable = FirstPrediction.Testable();
        bool falsifiable = FirstPrediction.Falsifiable();

        sb.AppendLine($"TESTABLE (in principle):    {testable}");
        sb.AppendLine($"FALSIFIABLE (in principle): {falsifiable}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {FirstPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • UNIQUE: the network discreteness (common granularity of all sectors) is absent from GR + SM.");
        sb.AppendLine("  • TESTABLE: in principle, via high-energy/lattice dispersion or Planck-scale granularity effects.");
        sb.AppendLine("  • FALSIFIABLE: in principle — but the free scale (QG14/QG38) makes falsification challenging, since the");
        sb.AppendLine("    discreteness can always be pushed below current resolution.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIQUE", FirstPrediction.Classify());
        Assert.True(testable);
        Assert.True(falsifiable);
    }
}
