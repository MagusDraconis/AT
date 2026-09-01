using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-VALID001 — Remaining Untested Items Audit. Analyze the S,T,U oblique parameters, the electron
/// g-2 (a_e), and the Majorana character (0νββ) against the accepted AT derivations. No new physics,
/// no parameter fitting, no speculation. For each item determine A) derived prediction, B) partial
/// derivation, C) no derivation.
/// </summary>
public class ATVALID001_UntestedItemsAuditTests : ResearchTestBase
{
    public ATVALID001_UntestedItemsAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATVALID0010_StatusClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATVALID0010: the status classification of the three items");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - use only accepted AT derivations [QG162/167/171/172/174/178/179/180];");
        sb.AppendLine("  - no new physics, no parameter fitting, no speculation.");
        sb.AppendLine();

        foreach (var item in Valid001UntestedItemsAudit.Items())
        {
            sb.AppendLine($"  {item.Name.PadRight(30)} [{item.Phase}] status={item.Status} " +
                          $"difficulty={item.Difficulty} priority={item.Priority} category={item.Category}");
        }
        sb.AppendLine();
        sb.AppendLine($"all derived: {Valid001UntestedItemsAudit.AllDerived()}");
        sb.AppendLine($"no missing derivation steps: {Valid001UntestedItemsAudit.NoMissingDerivationSteps()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, Valid001UntestedItemsAudit.Items().Length);
        Assert.True(Valid001UntestedItemsAudit.AllDerived(),
            "all three items must have complete derived predictions [category A]");
        Assert.True(Valid001UntestedItemsAudit.NoMissingDerivationSteps(),
            "no item may have a missing derivation step");
        Assert.All(Valid001UntestedItemsAudit.Items(), i => Assert.InRange(i.Difficulty, 1, 5));
    }

    [Fact]
    public void ATVALID0011_DependencyChains()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATVALID0011: the dependency chains");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - each chain runs from the D96 spectrum to the derived prediction;");
        sb.AppendLine("  - only accepted phases [QG155-180] are referenced.");
        sb.AppendLine();

        foreach (var item in Valid001UntestedItemsAudit.Items())
        {
            sb.AppendLine($"  {item.Name}:");
            foreach (var step in item.DependencyChain)
                sb.AppendLine($"    → {step}");
            sb.AppendLine($"    missing steps: {(item.MissingSteps.Length == 0 ? "NONE" : string.Join("; ", item.MissingSteps))}");
        }

        Output.WriteLine(sb.ToString());

        Assert.All(Valid001UntestedItemsAudit.Items(), i =>
            Assert.True(i.DependencyChain.Length >= 3, "every chain must be traced to the D96 spectrum"));
        Assert.All(Valid001UntestedItemsAudit.Items(), i =>
            Assert.Empty(i.MissingSteps));
    }

    [Fact]
    public void ATVALID0012_ClassificationAndPriority()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATVALID0012: the category and priority determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - each item belongs to Physics derivation / Experimental validation / Boundary;");
        sb.AppendLine("  - the open item, if any, is experimental — not a derivation gap.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {Valid001UntestedItemsAudit.Summary()}");
        sb.AppendLine($"Validation score: {Valid001UntestedItemsAudit.ValidationScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {Valid001UntestedItemsAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("DEPENDENCY SUMMARY:");
        foreach (var item in Valid001UntestedItemsAudit.Items())
        {
            sb.AppendLine($"  {item.Name}: {item.DerivationSummary}");
            sb.AppendLine($"    validation: {item.ValidationSummary}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal("ALL DERIVED — EXPERIMENTAL VALIDATION", Valid001UntestedItemsAudit.Classify());
        Assert.True(Valid001UntestedItemsAudit.ValidationScore() >= 6);
        Assert.True(Valid001UntestedItemsAudit.AllExperimentalValidation(),
            "all three items must belong to the experimental-validation category");
        Assert.All(Valid001UntestedItemsAudit.Items(), i => Assert.Equal("Experimental validation", i.Category));
    }
}
