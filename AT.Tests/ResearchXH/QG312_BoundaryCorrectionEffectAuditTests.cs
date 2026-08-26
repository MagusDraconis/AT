using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

public class QG312_BoundaryCorrectionEffectAuditTests : ResearchTestBase
{
    public QG312_BoundaryCorrectionEffectAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG312_BoundaryCorrectionEffectAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("QG312: Boundary Correction Effect Audit");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  D96 only; no targets; boundary corrections are structural reads from prior audits.");
        sb.AppendLine();
        foreach (var e in BoundaryCorrectionEffectAudit.Effects())
            sb.AppendLine($"  {e.Observable}: family={e.BoundaryFamily}, baseline={e.BaselineResidual:F6}, corrected={e.CorrectedResidual:F6}, effect={e.Effect}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {BoundaryCorrectionEffectAudit.Classify()}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, BoundaryCorrectionEffectAudit.Effects().Length);
    }
}
