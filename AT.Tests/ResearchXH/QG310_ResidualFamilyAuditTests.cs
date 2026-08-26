using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

public class QG310_ResidualFamilyAuditTests : ResearchTestBase
{
    public QG310_ResidualFamilyAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG310_ResidualFamilyAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("QG310: Residual Family Audit");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  D96 only; no target values; residuals are grouped by derived boundary families.");
        sb.AppendLine();
        foreach (var f in ResidualFamilyAudit.Families())
            sb.AppendLine($"  {f.Name}: family={f.BoundaryFamily}, read={f.BoundaryRead:F6}, proxy={f.ResidualProxy:F6}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {ResidualFamilyAudit.Classify()}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, ResidualFamilyAudit.Families().Length);
        Assert.True(ResidualFamilyAudit.FamilyCount() >= 3);
    }
}
