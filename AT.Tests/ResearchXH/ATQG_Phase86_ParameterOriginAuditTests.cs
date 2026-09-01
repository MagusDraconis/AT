using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 86 — Parameter Origin Audit. Determines whether any network mechanism can constrain the free SM
/// parameters. Classify: CONSTRAINED / PARTIAL / FULLY FREE.
///
/// Tests: ATQG860 (capacity + symmetry), ATQG861 (entropy + counting + minimal description), ATQG862 (classification).
/// </summary>
public class ATQG_Phase86_ParameterOriginAuditTests : ResearchTestBase
{
    public ATQG_Phase86_ParameterOriginAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG860: information capacity, symmetry constraints ───────────────────────

    [Fact]
    public void ATQG860_CapacityAndSymmetry()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG860: do capacity and symmetry constrain the values?");

        bool capacity = ParameterOriginAudit.CapacityDeterminesValues();
        bool symForm = ParameterOriginAudit.SymmetriesFixForm();
        bool symValues = ParameterOriginAudit.SymmetriesFixValues();

        sb.AppendLine($"information capacity DETERMINES values: {capacity}");
        sb.AppendLine($"symmetries fix FORM: {symForm}");
        sb.AppendLine($"symmetries fix VALUES: {symValues}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: capacity only permits values; symmetry fixes which terms EXIST (form) but not their");
        sb.AppendLine("numerical magnitudes. Neither mechanism pins down the values.");
        Output.WriteLine(sb.ToString());

        Assert.False(capacity, "capacity does not determine values");
        Assert.True(symForm, "symmetries fix form");
        Assert.False(symValues, "symmetries do not fix values");
    }

    // ── ATQG861: entropy, parameter counting, minimal description ─────────────────

    [Fact]
    public void ATQG861_EntropyCountingDescription()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG861: is the count/form constrained? entropy / minimal description native?");

        bool entropyNative = ParameterOriginAudit.EntropySelectionNative();
        bool countDetermined = ParameterOriginAudit.ParameterCountDetermined();
        bool minDescNative = ParameterOriginAudit.MinimalDescriptionNative();
        bool countOrForm = ParameterOriginAudit.ConstrainsCountOrForm();
        bool values = ParameterOriginAudit.ConstrainsValues();

        sb.AppendLine($"native entropy-selection principle: {entropyNative}");
        sb.AppendLine($"parameter COUNT structurally determined: {countDetermined}");
        sb.AppendLine($"minimal-description principle NATIVE: {minDescNative}");
        sb.AppendLine($"network constrains count OR form: {countOrForm}");
        sb.AppendLine($"network constrains VALUES: {values}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the COUNT (19) is structurally fixed (gauge dims + reps + family index), and symmetry fixes");
        sb.AppendLine("the FORM. But entropy/minimal-description selection is NOT native — it would be an additional");
        sb.AppendLine("postulate. So the network constrains count+form, not values.");
        Output.WriteLine(sb.ToString());

        Assert.False(entropyNative, "no native entropy selection");
        Assert.True(countDetermined, "count is structurally determined");
        Assert.False(minDescNative, "minimal description is not native");
        Assert.True(countOrForm, "count/form constrained");
        Assert.False(values, "values not constrained");
    }

    // ── ATQG862: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG862_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG862: CONSTRAINED / PARTIAL / FULLY FREE?");

        sb.AppendLine($"CLASSIFICATION: {ParameterOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT CONSTRAINED: the VALUES are not determined.");
        sb.AppendLine("  • NOT FULLY FREE: the network DOES constrain the COUNT (19) and the FORM (symmetry).");
        sb.AppendLine("  • PARTIAL: count + form are constrained; values remain free.");
        sb.AppendLine();
        sb.AppendLine("So the network PARTIALLY constrains the SM parameters (count + form), while the values stay free.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL", ParameterOriginAudit.Classify());
        Assert.True(ParameterOriginAudit.ConstrainsCountOrForm());
        Assert.False(ParameterOriginAudit.ConstrainsValues());
    }
}
