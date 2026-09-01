using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 84 — Origin of the Higgs sector. Determines whether mass generation can emerge from network
/// structure. Classify: DERIVED / COMPATIBLE / NEW SECTOR.
///
/// Tests: ATQG840 (node occupancy + link condensates), ATQG841 (symmetry breaking + vacuum + Higgs analog),
/// ATQG842 (classification).
/// </summary>
public class ATQG_Phase84_HiggsOriginTests : ResearchTestBase
{
    public ATQG_Phase84_HiggsOriginTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG840: node occupancy, link condensates ─────────────────────────────────

    [Fact]
    public void ATQG840_OccupancyAndCondensate()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG840: does the scalar sector / condensate exist?");

        bool scalar = HiggsOrigin.ScalarSectorExists();
        bool condensate = HiggsOrigin.LinkCondensateRepresentable();

        sb.AppendLine($"scalar representation ρ (node occupancy, spin-0) exists: {scalar}");
        sb.AppendLine($"link/node condensate can serve as a VEV: {condensate}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the scalar backbone ρ (derived QG23-24) provides the spin-0 representation, and a");
        sb.AppendLine("condensate of link content can play the role of the non-zero vacuum expectation value.");
        Output.WriteLine(sb.ToString());

        Assert.True(scalar, "scalar sector exists");
        Assert.True(condensate, "condensate is representable");
    }

    // ── ATQG841: symmetry breaking, vacuum, Higgs analog ──────────────────────────

    [Fact]
    public void ATQG841_SymmetryBreakingAndVacuum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG841: is the Higgs MECHANISM native?");

        bool native = HiggsOrigin.SymmetryBreakingNative();
        bool representable = HiggsOrigin.SymmetryBreakingRepresentable();
        bool vacuum = HiggsOrigin.VacuumAsCondensate();
        bool analog = HiggsOrigin.HiggsAnalogRepresentable();
        bool derived = HiggsOrigin.MassGenerationDerived();

        sb.AppendLine($"symmetry-breaking potential (VEV != 0) NATIVE: {native}");
        sb.AppendLine($"symmetry breaking REPRESENTABLE (postulated potential): {representable}");
        sb.AppendLine($"effective vacuum is a condensate: {vacuum}");
        sb.AppendLine($"ρ serves as the Higgs-field analog: {analog}");
        sb.AppendLine($"mass generation DERIVED from (V,E) alone: {derived}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the Higgs analog is representable within the existing scalar sector (ρ condensate → VEV), but");
        sb.AppendLine("the symmetry-breaking potential and the Yukawa/gauge couplings are ADDITIONAL (postulated) content.");
        Output.WriteLine(sb.ToString());

        Assert.False(native, "SSB potential is not native");
        Assert.True(representable, "SSB is representable");
        Assert.True(vacuum, "vacuum is a condensate");
        Assert.True(analog, "ρ is the Higgs analog");
        Assert.False(derived, "mass generation is not derived");
    }

    // ── ATQG842: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG842_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG842: DERIVED / COMPATIBLE / NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {HiggsOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the symmetry-breaking potential and mass couplings are not outputs of (V,E).");
        sb.AppendLine("  • COMPATIBLE: the scalar ρ sector already exists, so the Higgs analog (a ρ condensate with a VEV) is");
        sb.AppendLine("    representable without a new representation.");
        sb.AppendLine("  • NOT NEW SECTOR: no new representation is required — the spin-0 sector already exists.");
        sb.AppendLine();
        sb.AppendLine("So mass generation is COMPATIBLE (representable via a ρ condensate), but not DERIVED from the network.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("COMPATIBLE", HiggsOrigin.Classify());
        Assert.True(HiggsOrigin.ScalarSectorExists());
        Assert.False(HiggsOrigin.MassGenerationDerived());
    }
}
