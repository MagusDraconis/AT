using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 50 — necessity of two sectors. Determines whether the two-sector structure is arbitrary or minimal.
/// Classify: FORCED / PREFERRED / CONTINGENT.
///
/// Tests: TQMQG500 (division of roles), TQMQG501 (minimality), TQMQG502 (classification).
/// </summary>
public class TQMQG_Phase50_TwoSectorNecessityTests : ResearchTestBase
{
    public TQMQG_Phase50_TwoSectorNecessityTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG500: division of roles ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG500_DivisionOfRoles()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG500: scalar = source/actualization; tensor = propagation/geometry");

        sb.AppendLine("SCALAR SECTOR (Q-events → ρ):");
        sb.AppendLine("  • ACTUALIZATION — the discrete counting measure (how many events happened)");
        sb.AppendLine("  • SOURCE — the matter/deficit, redshift, attraction, regular cores");
        sb.AppendLine("  • intrinsically spin-0: counting is a scalar operation");
        sb.AppendLine();
        sb.AppendLine("TENSOR SECTOR (ψ):");
        sb.AppendLine("  • PROPAGATION — the spin-2 field carrying dynamical metric fluctuations (GWs)");
        sb.AppendLine("  • GEOMETRY — lensing, horizons, the quadrupole (+/×) pattern");
        sb.AppendLine();
        sb.AppendLine("The two roles are IRREDUCIBLE: information/counting vs geometry/propagation.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, TwoSectorNecessity.Sectors.Length);
        Assert.Contains("scalar-actualization", TwoSectorNecessity.Sectors);
        Assert.Contains("tensor-propagation", TwoSectorNecessity.Sectors);
    }

    // ── TQMQG501: minimality ─────────────────────────────────────────────────────────

    [Fact]
    public void TQMQG501_Minimality()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG501: neither sector alone suffices; two is minimal");

        bool singleScalar = TwoSectorNecessity.SingleScalarSufficient();
        bool singleTensor = TwoSectorNecessity.SingleTensorSufficient();
        bool minimal = TwoSectorNecessity.Minimal();
        bool arbitrary = TwoSectorNecessity.Arbitrary();

        sb.AppendLine($"single scalar suffices: {singleScalar}  (no spin-2, QG23/37/49)");
        sb.AppendLine($"single tensor suffices: {singleTensor}  (does not count events)");
        sb.AppendLine($"two-sector structure is MINIMAL: {minimal}");
        sb.AppendLine($"two-sector structure is ARBITRARY: {arbitrary}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a scalar alone cannot propagate spin-2; a bare tensor alone cannot count discrete events. The");
        sb.AppendLine("smallest complete structure is exactly two sectors — scalar actualization + tensor propagation.");
        Output.WriteLine(sb.ToString());

        Assert.False(singleScalar, "a single scalar should not suffice");
        Assert.False(singleTensor, "a single tensor should not suffice");
        Assert.True(minimal, "the two-sector structure should be minimal");
        Assert.False(arbitrary, "the structure should not be arbitrary");
    }

    // ── TQMQG502: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG502_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG502: FORCED / PREFERRED / CONTINGENT?");

        bool scalarForced = TwoSectorNecessity.ScalarSectorForced();
        bool tensorContingent = TwoSectorNecessity.TensorSectorContingent();

        sb.AppendLine($"scalar sector FORCED (intrinsic):    {scalarForced}");
        sb.AppendLine($"tensor sector CONTINGENT (observational): {tensorContingent}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {TwoSectorNecessity.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • FORCED (minimal): the two roles are irreducible — actualization is intrinsically scalar (counting), and");
        sb.AppendLine("    spin-2 propagation is intrinsically tensor; no single sector can do both, so two is the minimum.");
        sb.AppendLine("  • TIERED: the scalar half is FORCED by the nature of actualization; the tensor half is CONTINGENT on the");
        sb.AppendLine("    spin-2 GW observation (itself model-dependent, QG48).");
        sb.AppendLine("  • NOT ARBITRARY: exactly two sectors is the minimal complete structure, not a free choice.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("FORCED", TwoSectorNecessity.Classify());
        Assert.True(scalarForced, "the scalar sector should be forced");
        Assert.True(tensorContingent, "the tensor sector should be contingent");
    }
}
