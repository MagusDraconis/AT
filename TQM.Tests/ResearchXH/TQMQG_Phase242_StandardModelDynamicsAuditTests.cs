using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 242 — Standard Model Dynamics Audit. Determine whether SM dynamics are derived or only
/// hosted. Audit only — no new physics.
/// </summary>
public class TQMQG_Phase242_StandardModelDynamicsAuditTests : ResearchTestBase
{
    public TQMQG_Phase242_StandardModelDynamicsAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2420_SixDynamicsChecks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2420: the six SM dynamics checks");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Reviews QG60/76/78-85/149-180: gauge symmetry, SU(3)/SU(2)/U(1) origin, interactions.");
        sb.AppendLine();

        sb.AppendLine("THE SIX CHECKS:");
        foreach (var c in StandardModelDynamicsAudit.Checks())
        {
            sb.AppendLine($"  {c.Name}: {c.Status}");
            sb.AppendLine($"      {c.Evidence}");
        }
        sb.AppendLine();

        sb.AppendLine($"By status: {string.Join(", ", StandardModelDynamicsAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, StandardModelDynamicsAudit.Checks().Length);
        var sc = StandardModelDynamicsAudit.StatusCounts();
        Assert.Equal(3, sc[StandardModelDynamicsAudit.Status.Derived]);
        Assert.Equal(1, sc[StandardModelDynamicsAudit.Status.Hosted]);
        Assert.Equal(1, sc[StandardModelDynamicsAudit.Status.Partial]);
        Assert.Equal(1, sc[StandardModelDynamicsAudit.Status.Open]);
    }

    [Fact]
    public void TQMQG2421_SymmetryDerivedDynamicsHosted()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2421: the gauge SYMMETRY is derived; the DYNAMICS is hosted");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG161 derives the 1+3+8 generator structure from D96; QG60/76 host the dynamics.");
        sb.AppendLine();

        sb.AppendLine("DERIVED (the symmetry):");
        foreach (var c in StandardModelDynamicsAudit.Checks().Where(c => c.Status == StandardModelDynamicsAudit.Status.Derived))
            sb.AppendLine($"  • {c.Name}");
        sb.AppendLine();
        sb.AppendLine("HOSTED/OPEN (the dynamics):");
        foreach (var c in StandardModelDynamicsAudit.Checks().Where(c => c.Status is StandardModelDynamicsAudit.Status.Hosted or StandardModelDynamicsAudit.Status.Open))
            sb.AppendLine($"  • {c.Name} [{c.Status}]");
        sb.AppendLine();

        sb.AppendLine("EXACT MISSING DYNAMICS:");
        foreach (var m in StandardModelDynamicsAudit.MissingDynamics())
            sb.AppendLine($"  • {m}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, StandardModelDynamicsAudit.MissingDynamics().Length);
    }

    [Fact]
    public void TQMQG2422_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2422: summary — symmetry derived, dynamics hosted");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - This is the exact content of the QG241 'SM dynamics' partial criterion.");
        sb.AppendLine();

        sb.AppendLine("SUMMARY:");
        sb.AppendLine($"  {StandardModelDynamicsAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The gauge SYMMETRY (the 1+3+8 generator structure) IS DERIVED from D96 (QG161):");
        sb.AppendLine("    U(1) from the rotation subgroup, SU(2) from the doublet-restricted generators,");
        sb.AppendLine("    SU(3) from the 3 octave families (with a color-count postulate trace, QG79).");
        sb.AppendLine("  - The gauge DYNAMICS (the interaction Lagrangian, vertices, propagators) is NOT");
        sb.AppendLine("    derived — it is HOSTED (QG60/76) and the interaction vertices are OPEN.");
        sb.AppendLine("  - Therefore SM dynamics are PARTIALLY derived: the structure yes, the dynamics no.");
        sb.AppendLine("    This is the exact missing content: the interaction vertices and propagators.");

        Output.WriteLine(sb.ToString());

        Assert.Contains("DERIVED", StandardModelDynamicsAudit.Summary());
        Assert.Contains("HOSTED/OPEN", StandardModelDynamicsAudit.Summary());
    }
}
