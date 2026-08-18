using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 85 — Origin of Standard Model parameters. Determines whether masses, couplings, generations, and
/// color count can emerge from network information content. Classify: DERIVED / COMPATIBLE / POSTULATED.
///
/// Tests: TQMQG850 (parameter counting + link capacity), TQMQG851 (symmetry + family index + mass hierarchies),
/// TQMQG852 (classification).
/// </summary>
public class TQMQG_Phase85_SMParametersTests : ResearchTestBase
{
    public TQMQG_Phase85_SMParametersTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG850: parameter counting, link information capacity ────────────────────

    [Fact]
    public void TQMQG850_ParameterCounting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG850: how many parameters, and does capacity constrain them?");

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

    // ── TQMQG851: symmetry constraints, family index, mass hierarchies ─────────────

    [Fact]
    public void TQMQG851_SymmetryAndHierarchies()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG851: do symmetries or structure fix the values?");

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

    // ── TQMQG852: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG852_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG852: DERIVED / COMPATIBLE / POSTULATED?");

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
