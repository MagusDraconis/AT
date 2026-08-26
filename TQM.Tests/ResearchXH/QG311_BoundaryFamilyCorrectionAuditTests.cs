using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

public class QG311_BoundaryFamilyCorrectionAuditTests : ResearchTestBase
{
    public QG311_BoundaryFamilyCorrectionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG311_BoundaryFamilyCorrectionAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("QG311: Boundary Family Correction Audit");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  D96 only; no target values; correction map is derived from boundary families.");
        sb.AppendLine();
        foreach (var c in BoundaryFamilyCorrectionAudit.Corrections())
            sb.AppendLine($"  {c.Observable}: family={c.BoundaryFamily}, read={c.BoundaryRead:F6}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {BoundaryFamilyCorrectionAudit.Classify()}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, BoundaryFamilyCorrectionAudit.Corrections().Length);
        Assert.True(BoundaryFamilyCorrectionAudit.AllAssigned());
    }
}
