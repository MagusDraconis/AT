using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

public class QG308_BoundaryReadNecessityTests : ResearchTestBase
{
    public QG308_BoundaryReadNecessityTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG308_BoundaryReadNecessity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("QG308: Boundary Read Necessity");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  D96 only; no targets; no fitted coefficients; boundary reads are derived from the spectrum.");
        sb.AppendLine();
        sb.AppendLine("BREADCRUMB READS:");
        foreach (var r in BoundaryReadNecessity.Reads())
            sb.AppendLine($"  {r.Name}: {r.Value:F6} ({r.Definition})");
        sb.AppendLine();
        sb.AppendLine("REVIEW:");
        foreach (var r in BoundaryReadNecessity.Review())
            sb.AppendLine($"  {r.Name}: operator-only={r.OperatorOnlyResidual:F6}, operator+boundary={r.OperatorPlusBoundaryResidual:F6}, boundary={r.BoundaryRead}, improves={r.Improves}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {BoundaryReadNecessity.Classify()}");
        Output.WriteLine(sb.ToString());

        Assert.NotEmpty(BoundaryReadNecessity.Reads());
        Assert.Equal(5, BoundaryReadNecessity.Review().Length);
    }
}
