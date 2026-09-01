using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 79 — Why SU(3)? Determines whether SU(3) is the MINIMAL non-Abelian extension of the link.
/// Classify: DERIVED / PREFERRED / NEW POSTULATE.
///
/// Tests: ATQG790 (SU(2) vs SU(3)), ATQG791 (color triplets + generator counting + confinement), ATQG792 (classification).
/// </summary>
public class ATQG_Phase79_WhySU3Tests : ResearchTestBase
{
    public ATQG_Phase79_WhySU3Tests(ITestOutputHelper o) : base(o) { }

    // ── ATQG790: SU(2) vs SU(3) — minimality ──────────────────────────────────────

    [Fact]
    public void ATQG790_Su2VsSu3()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG790: is SU(3) the MINIMAL non-Abelian extension?");

        bool su2Present = WhySU3.Su2AlreadyPresentAsSpin();
        bool minimalIsSu2 = WhySU3.MinimalNonAbelianIsSu2();
        bool su3Minimal = WhySU3.Su3IsMinimalNonAbelian();

        int dimSu2 = WhySU3.GeneratorCount(2);   // 3
        int dimSu3 = WhySU3.GeneratorCount(3);   // 8

        sb.AppendLine($"SU(2) already present as the spin structure S: {su2Present}");
        sb.AppendLine($"dim SU(2) = 2^2-1 = {dimSu2}  (smallest non-Abelian Lie group)");
        sb.AppendLine($"dim SU(3) = 3^2-1 = {dimSu3}");
        sb.AppendLine($"minimal non-Abelian is SU(2): {minimalIsSu2}");
        sb.AppendLine($"SU(3) is the minimal non-Abelian extension: {su3Minimal}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: SU(3) is NOT the minimal non-Abelian extension in the abstract — SU(2) (dim 3) is");
        sb.AppendLine("smaller and is already present. Minimality alone does not select SU(3).");
        Output.WriteLine(sb.ToString());

        Assert.True(su2Present, "SU(2) should already be present as spin");
        Assert.True(minimalIsSu2, "SU(2) is the minimal non-Abelian group");
        Assert.False(su3Minimal, "SU(3) is NOT the minimal non-Abelian group");
        Assert.Equal(3, dimSu2);
        Assert.Equal(8, dimSu3);
    }

    // ── ATQG791: color triplets, generator counting, confinement ──────────────────

    [Fact]
    public void ATQG791_TripletsAndGenerators()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG791: given 3 colors, SU(3) is the unique gauge group");

        int colors = WhySU3.ColorCount();
        int gluons = WhySU3.GluonCount();
        int generators = WhySU3.GeneratorCount(colors);
        bool groupIsSu3 = WhySU3.GroupGivenColorsIsSu3();
        bool colorsDerived = WhySU3.ColorCountIsDerived();
        bool confining = WhySU3.ConfinementIsNonPerturbative();
        bool capacity = WhySU3.LinkCapacitySuffices();

        sb.AppendLine($"color count N = {colors} (triplets — empirical, from baryon statistics)");
        sb.AppendLine($"generator count dim SU(3) = N^2-1 = {generators} = {gluons} gluons");
        sb.AppendLine($"given N=3 colors the maximal unitary det=1 group is SU(3): {groupIsSu3}");
        sb.AppendLine($"color count N=3 is DERIVED from the network: {colorsDerived}");
        sb.AppendLine($"confinement is non-perturbative (dynamical): {confining}");
        sb.AppendLine($"link information capacity suffices for 8 real parameters: {capacity}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the number 3 (color triplets) is an empirical input, not a network output. GIVEN N = 3,");
        sb.AppendLine("the gauge group is uniquely SU(3) with 8 gluons. The link has ample capacity to host it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, colors);
        Assert.Equal(8, gluons);
        Assert.Equal(8, generators);
        Assert.True(groupIsSu3, "given 3 colors the group is SU(3)");
        Assert.False(colorsDerived, "color count is not derived");
        Assert.True(confining, "confinement is non-perturbative");
        Assert.True(capacity, "link capacity suffices");
    }

    // ── ATQG792: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG792_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG792: DERIVED / PREFERRED / NEW POSTULATE?");

        sb.AppendLine($"CLASSIFICATION: {WhySU3.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the color count N = 3 is not a network output (fermion statistics force 3 colors,");
        sb.AppendLine("    but that is an empirical fact, not derivable from U(1)/SU(2)/link structure).");
        sb.AppendLine("  • PREFERRED (conditional): GIVEN N = 3 colors, SU(3) is the unique maximal unitary det=1 group with");
        sb.AppendLine("    N^2-1 = 8 generators — it is the preferred/only choice for that 3-dimensional color space.");
        sb.AppendLine("  • NEW POSTULATE: the existence of exactly 3 colors (color triplets) is itself a new postulate; SU(3)");
        sb.AppendLine("    follows uniquely once that postulate is accepted.");
        sb.AppendLine();
        sb.AppendLine("So Why SU(3)? Because 3 colors are postulated (from baryon statistics), and given 3 colors SU(3) is forced.");
        sb.AppendLine("The 3-color count — not the group — is the new postulate.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW POSTULATE", WhySU3.Classify());
        Assert.False(WhySU3.ColorCountIsDerived());
        Assert.True(WhySU3.GroupGivenColorsIsSu3());
    }
}
