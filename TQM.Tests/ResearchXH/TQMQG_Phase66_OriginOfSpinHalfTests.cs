using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 66 — origin of spin-1/2. Determines whether fermions emerge from the network.
/// Classify: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE.
///
/// Tests: TQMQG660 (integer vs half-integer), TQMQG661 (double cover), TQMQG662 (classification).
/// </summary>
public class TQMQG_Phase66_OriginOfSpinHalfTests : ResearchTestBase
{
    public TQMQG_Phase66_OriginOfSpinHalfTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG660: integer vs half-integer spin ──────────────────────────────────────

    [Fact]
    public void TQMQG660_IntegerVsHalfInteger()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG660: the network hosts integer spins; fermions are half-integer");

        double[] native = OriginOfSpinHalf.NativeSpins();
        sb.AppendLine($"native spins: {string.Join(", ", native)}  (nodes 0, links 2, gauge 1)");
        sb.AppendLine($"fermion spin: 1/2  (half-integer, a spinor)");

        bool orientationSpinor = OriginOfSpinHalf.OrientationGivesSpinor();

        sb.AppendLine($"link orientation gives a spinor: {orientationSpinor}  (only a Z2 sign)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network's native content is INTEGER spin (tensors). Spin-1/2 is a HALF-INTEGER spinor — a");
        sb.AppendLine("fundamentally different representation (SU(2), double cover) that integer-spin content cannot produce.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, native.Length);
        Assert.False(orientationSpinor, "orientation should not give a spinor");
    }

    // ── TQMQG661: double cover required ─────────────────────────────────────────────

    [Fact]
    public void TQMQG661_DoubleCoverRequired()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG661: spinors require a double cover (spin structure)");

        bool doubleCover = OriginOfSpinHalf.RequiresDoubleCover();
        bool derived = OriginOfSpinHalf.Derived();

        sb.AppendLine($"spin-1/2 requires a DOUBLE COVER (SU(2) spin structure): {doubleCover}");
        sb.AppendLine($"spin-1/2 is DERIVED from the integer-spin content:      {derived}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a spinor is a section of a spin bundle (a double cover of the frame bundle). It is NOT derivable");
        sb.AppendLine("from scalar nodes + rank-2 links — a spin structure is a new piece of data on the network.");
        Output.WriteLine(sb.ToString());

        Assert.True(doubleCover, "spin-1/2 should require a double cover");
        Assert.False(derived, "spin-1/2 should not be derivable");
    }

    // ── TQMQG662: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG662_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG662: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE?");

        bool compatible = OriginOfSpinHalf.Compatible();
        bool requires = OriginOfSpinHalf.RequiresNewPrimitive();

        sb.AppendLine($"COMPATIBLE (spin structure can be added): {compatible}");
        sb.AppendLine($"REQUIRES NEW PRIMITIVE (spinor):           {requires}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {OriginOfSpinHalf.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: half-integer spin cannot emerge from integer-spin (scalar + rank-2 + U(1)) content.");
        sb.AppendLine("  • COMPATIBLE: a spin structure (double cover) can be added to the network to host fermions.");
        sb.AppendLine("  • REQUIRES NEW PRIMITIVE: the spinor/double-cover is a new degree of freedom — fermions need it.");
        sb.AppendLine();
        sb.AppendLine("So fermions require a new spin-1/2 (spinor) primitive, compatible with the network but not derivable from it —");
        sb.AppendLine("completing the matter picture: gravity (spin-0+2) and gauge (spin-1) are hosted, fermions (spin-1/2) are not.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("REQUIRES NEW PRIMITIVE", OriginOfSpinHalf.Classify());
        Assert.True(compatible);
        Assert.True(requires);
    }
}
