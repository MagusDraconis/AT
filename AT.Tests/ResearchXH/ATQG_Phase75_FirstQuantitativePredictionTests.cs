using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 75 — first quantitative prediction of the unified network theory.
/// Classify: UNIQUE / TESTABLE / FALSIFIABLE.
///
/// Tests: ATQG750 (the profile), ATQG751 (uniqueness), ATQG752 (classification).
/// </summary>
public class ATQG_Phase75_FirstQuantitativePredictionTests : ResearchTestBase
{
    public ATQG_Phase75_FirstQuantitativePredictionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG750: the predicted profile ────────────────────────────────────────────

    [Fact]
    public void ATQG750_PredictedProfile()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG750: M_eff(r) = M(1 - e^(-r^3/rc^3))");

        double M = 1.0, rc = 1.0;
        double core = FirstQuantitativePrediction.RegularCore(0.0, M, rc);
        double half = FirstQuantitativePrediction.RegularCore(0.5, M, rc);
        double atRc = FirstQuantitativePrediction.RegularCore(rc, M, rc);
        double far = FirstQuantitativePrediction.RegularCore(5.0, M, rc);
        int exponent = FirstQuantitativePrediction.CoreExponent();

        sb.AppendLine($"M_eff(0)      = {core:F6}   (regular core, no singularity)");
        sb.AppendLine($"M_eff(0.5 rc) = {half:F6}");
        sb.AppendLine($"M_eff(rc)     = {atRc:F6}  (= M(1-1/e))");
        sb.AppendLine($"M_eff(5 rc)   = {far:F6}   (→ M)");
        sb.AppendLine($"core exponent = {exponent}   (= spatial dimension)");

        bool regular = core == 0.0 && far > 0.99 * M;

        sb.AppendLine();
        sb.AppendLine($"regular core + asymptotic M: {regular}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the predicted curve is the Poisson-saturation profile with exponent 3 — a specific quantitative");
        sb.AppendLine("fingerprint.");
        Output.WriteLine(sb.ToString());

        Assert.True(regular, "the core should be regular and asymptote to M");
        Assert.Equal(3, exponent);
    }

    // ── ATQG751: uniqueness ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG751_Uniqueness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG751: the specific form differs from GR and from Hayward/Bardeen");

        bool differsGr = FirstQuantitativePrediction.DiffersFromGr();
        bool differsRegular = FirstQuantitativePrediction.DiffersFromRegularBhModels();

        sb.AppendLine($"differs from GR (singular core):        {differsGr}");
        sb.AppendLine($"differs from Hayward/Bardeen forms:     {differsRegular}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: GR has M_eff = M (singular at r=0); Hayward has M r³/(r³+2Mℓ²); Bardeen has M r³/(r²+r_g²)^(3/2).");
        sb.AppendLine("The network's M(1-e^(-r³/rc³)) is a DIFFERENT, unique functional form — a testable fingerprint.");
        Output.WriteLine(sb.ToString());

        Assert.True(differsGr, "should differ from GR");
        Assert.True(differsRegular, "should differ from Hayward/Bardeen");
    }

    // ── ATQG752: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG752_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG752: UNIQUE / TESTABLE / FALSIFIABLE?");

        bool testable = FirstQuantitativePrediction.Testable();
        bool falsifiable = FirstQuantitativePrediction.Falsifiable();

        sb.AppendLine($"TESTABLE:    {testable}  (shadow, ISCO, lensing, ringdown)");
        sb.AppendLine($"FALSIFIABLE: {falsifiable}  (if the observed core does not match)");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {FirstQuantitativePrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • UNIQUE: the specific 1-e^(-r³/rc³) curve (exponent 3) is absent from GR and from Hayward/Bardeen.");
        sb.AppendLine("  • TESTABLE: via black-hole shadow, ISCO, lensing, and gravitational-wave ringdown.");
        sb.AppendLine("  • FALSIFIABLE: in principle — with the caveat of the free core scale r_c.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIQUE", FirstQuantitativePrediction.Classify());
        Assert.True(testable);
        Assert.True(falsifiable);
    }
}
