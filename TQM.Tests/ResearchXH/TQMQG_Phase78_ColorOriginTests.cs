using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 78 — origin of SU(3) color. Determines whether color charge emerges from link structure.
/// Classify: DERIVED / COMPATIBLE / NEW SECTOR.
///
/// Tests: TQMQG780 (different Lie algebra), TQMQG781 (link carries SU(3)), TQMQG782 (classification).
/// </summary>
public class TQMQG_Phase78_ColorOriginTests : ResearchTestBase
{
    public TQMQG_Phase78_ColorOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG780: SU(3) is a different Lie algebra ─────────────────────────────────

    [Fact]
    public void TQMQG780_DifferentLieAlgebra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG780: SU(3) does not emerge from U(1) or SU(2)");

        bool existing = ColorOrigin.ExistingGaugeIsU1AndSu2();
        bool different = ColorOrigin.Su3DifferentLieAlgebra();

        sb.AppendLine($"existing gauge content is U(1) (θ) + SU(2) (S): {existing}");
        sb.AppendLine($"SU(3) is a DIFFERENT Lie algebra (3 colors, 8 generators): {different}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: U(1) (1 phase), SU(2) (2 spin states), and SU(3) (3 colors) are different Lie groups. SU(3)");
        sb.AppendLine("does NOT emerge from the U(1)/SU(2) content of the network.");
        Output.WriteLine(sb.ToString());

        Assert.True(existing, "existing gauge should be U(1) + SU(2)");
        Assert.True(different, "SU(3) should be a different Lie algebra");
    }

    // ── TQMQG781: the link can carry SU(3) ─────────────────────────────────────────

    [Fact]
    public void TQMQG781_LinkCarriesSu3()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG781: the link carries an SU(3) connection, like it carries U(1)");

        bool linkCarries = ColorOrigin.LinkCanCarrySu3();
        bool dynamical = ColorOrigin.ConfinementIsDynamical();

        sb.AppendLine($"the link CAN carry an SU(3) connection (lattice QCD): {linkCarries}");
        sb.AppendLine($"confinement is DYNAMICAL (not structural):          {dynamical}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a link variable is a group element of the gauge group G — for U(1) it is a phase, for SU(3) it is");
        sb.AppendLine("a 3×3 unitary matrix. Wilson loops and gluons are the SU(3) analogues of the U(1) holonomy/photon. Confinement");
        sb.AppendLine("is a non-perturbative dynamical property, not a structural link feature.");
        Output.WriteLine(sb.ToString());

        Assert.True(linkCarries, "the link should carry SU(3)");
        Assert.True(dynamical, "confinement should be dynamical");
    }

    // ── TQMQG782: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG782_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG782: DERIVED / COMPATIBLE / NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {ColorOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: SU(3) (3 colors, non-Abelian) does not emerge from the U(1) θ or SU(2) S content.");
        sb.AppendLine("  • COMPATIBLE: the link CAN carry an SU(3) connection, exactly as it carries the U(1) phase.");
        sb.AppendLine("  • NEW SECTOR: the SU(3) color connection (3 colors, 8 gluons) is a new gauge sector.");
        sb.AppendLine();
        sb.AppendLine("So SU(3) color requires a NEW SECTOR, compatible with the link structure but not derivable from it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW SECTOR", ColorOrigin.Classify());
        Assert.True(ColorOrigin.NewSector());
        Assert.True(ColorOrigin.LinkCanCarrySu3());
    }
}
