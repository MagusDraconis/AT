using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 85 — Origin of Standard Model parameters. Determines whether masses, couplings, generations, and
/// color count can emerge from network information content. Classify: DERIVED / COMPATIBLE / POSTULATED.
///
/// Tests: ATQG850 (parameter counting + link capacity), ATQG851 (symmetry + family index + mass hierarchies),
/// ATQG852 (classification).
/// </summary>
public class ATQG_Phase85_SMParametersTests : ResearchTestBase
{
    public ATQG_Phase85_SMParametersTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG850: parameter counting, link information capacity ────────────────────

    [Fact]
    public void ATQG850_ParameterCounting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG850: how many parameters, and does capacity constrain them?");

        int count = SMParameters.SmParameterCount();
        int nuExtra = SMParameters.NeutrinoAdditionalParameters();
        bool capacity = SMParameters.LinkCapacitySufficient();
        bool determines = SMParameters.LinkCapacityDeterminesValues();

        sb.AppendLine($"SM free parameters = {count} (3 gauge + 2 Higgs + 9 masses + 4 CKM + 1 theta)");
        sb.AppendLine($"+{nuExtra} if neutrinos are massive (3 masses + 4 PMNS)");
        sb.AppendLine($"link information capacity SUFFICES to host them: {capacity}");
        sb.AppendLine($"link capacity DETERMINES the values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the link has ample capacity (it already carries the complex rank-2 object plus a family");
        sb.AppendLine("index), but capacity only PERMITS the parameters — it does not fix their values.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(19, count);
        Assert.Equal(7, nuExtra);
        Assert.True(capacity, "capacity suffices");
        Assert.False(determines, "capacity does not determine values");
    }

    // ── ATQG851: symmetry constraints, family index, mass hierarchies ─────────────

    [Fact]
    public void ATQG851_SymmetryAndHierarchies()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG851: do symmetries or structure fix the values?");

        bool symForm = SMParameters.SymmetriesFixFormNotValues();
        bool familyFree = SMParameters.FamilyCountFree();
        bool hierarchyDerived = SMParameters.MassHierarchiesDerived();

        sb.AppendLine($"symmetries fix the FORM but not the VALUES: {symForm}");
        sb.AppendLine($"family count (3) is free (not derived): {familyFree}");
        sb.AppendLine($"mass hierarchies (up vs top quark) DERIVED: {hierarchyDerived}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: gauge/Lorentz symmetries constrain the FORM of the parameters, not their numerical values.");
        sb.AppendLine("The family count is free, and the mass hierarchy is an empirical input, not a network output.");
        Output.WriteLine(sb.ToString());

        Assert.True(symForm, "symmetries fix form not values");
        Assert.True(familyFree, "family count is free");
        Assert.False(hierarchyDerived, "mass hierarchies are not derived");
    }

    // ── ATQG852: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG852_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG852: DERIVED / COMPATIBLE / POSTULATED?");

        sb.AppendLine($"CLASSIFICATION: {SMParameters.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the 19 parameter VALUES are not outputs of the network.");
        sb.AppendLine("  • COMPATIBLE (subordinate): the link has the CAPACITY to host them — no contradiction.");
        sb.AppendLine("  • POSTULATED: the masses, couplings, generation count, and color count are FREE empirical inputs;");
        sb.AppendLine("    the network hosts but does not derive them.");
        sb.AppendLine();
        sb.AppendLine("So the SM parameters are POSTULATED (compatible, but not derivable).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("POSTULATED", SMParameters.Classify());
        Assert.False(SMParameters.LinkCapacityDeterminesValues());
        Assert.False(SMParameters.MassHierarchiesDerived());
    }
}
