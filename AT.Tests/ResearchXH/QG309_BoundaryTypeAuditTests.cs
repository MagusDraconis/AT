using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

public class QG309_BoundaryTypeAuditTests : ResearchTestBase
{
    public QG309_BoundaryTypeAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG309_BoundaryTypeAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("QG309: Boundary Type Audit");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  D96 only; no observables; no target values; boundary types are derived from spectrum reads.");
        sb.AppendLine();
        sb.AppendLine("BOUNDARY TYPES:");
        foreach (var t in BoundaryTypeAudit.Types())
            sb.AppendLine($"  {t.Name}: {t.Read:F6} ({t.Definition})");
        sb.AppendLine();
        sb.AppendLine("RESIDUAL FAMILIES:");
        foreach (var r in BoundaryTypeAudit.ResidualFamilies())
            sb.AppendLine($"  {r.Name}: boundary={r.BoundaryType}, read={r.BoundaryRead:F6}, proxy={r.ResidualProxy:F6}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {BoundaryTypeAudit.Classify()}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, BoundaryTypeAudit.ResidualFamilies().Length);
        Assert.True(BoundaryTypeAudit.BoundaryTypeCount() >= 3);
    }
}
