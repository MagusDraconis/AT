using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 283 — Assignment Frontier Closure. Can the Question → Physics Role mapping be closed by
/// a D96-native role assignment law? D96 only, no observables.
/// </summary>
public class ATQG_Phase283_AssignmentFrontierClosureTests : ResearchTestBase
{
    public ATQG_Phase283_AssignmentFrontierClosureTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2830_UniqueAxisPositions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2830: the full axis positions are unique");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - every question has a UNIQUE full 4-axis position (level × nature × kind × domain);");
        sb.AppendLine("  - the mapping question → role is bijective.");
        sb.AppendLine();

        foreach (var a in AssignmentFrontierClosure.Assignments())
            sb.AppendLine($"  {a.Question,-14} = ({a.Level,-7} {a.Nature,-11} {a.Kind,-12} {a.Domain,-14}) → {a.Role,-9} [{a.Law}]");
        sb.AppendLine();
        sb.AppendLine($"full axis positions unique: {AssignmentFrontierClosure.FullAxisPositionsUnique()}");
        sb.AppendLine($"mapping bijective: {AssignmentFrontierClosure.MappingBijective()}");
        sb.AppendLine();
        sb.AppendLine("The KIND axis separates strength (interaction) from orientation (angle);");
        sb.AppendLine("the LEVEL axis separates global (whole) from arena (spacetime).");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, AssignmentFrontierClosure.Assignments().Length);
        Assert.True(AssignmentFrontierClosure.FullAxisPositionsUnique(),
            "each question has a unique full 4-axis position");
        Assert.True(AssignmentFrontierClosure.MappingBijective(),
            "each question maps to a distinct role");
    }

    [Fact]
    public void ATQG2831_RelationalSubclassResolved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2831: the relational subclass resolution");

        sb.AppendLine("HYPOTHESIS: the QG273/275 residual (a strength read can be coupling, mixing, or");
        sb.AppendLine("mass-ratio) is resolved by the CONSERVATION structure of the equation.");
        sb.AppendLine();

        sb.AppendLine($"m_τ/m_μ (mass-ratio role)  = √occMom·λ₂ = {AssignmentFrontierClosure.TauMuonMassRatio():F4}");
        sb.AppendLine($"y_τ/y_μ (coupling role)    = √occMom·λ₂ = {AssignmentFrontierClosure.TauMuonYukawaRatio():F4}");
        sb.AppendLine($"Vus     (mixing role)      = #d/(2Σm) = {AssignmentFrontierClosure.Vus():F6}");
        sb.AppendLine($"CKM unitary (V†V=I): {AssignmentFrontierClosure.CKMUnitary()}");
        sb.AppendLine($"relational subclass resolved: {AssignmentFrontierClosure.RelationalSubclassResolved()}");
        sb.AppendLine();
        sb.AppendLine("The role is set by the conservation structure: norm-preserving (unitary) → mixing;");
        sb.AppendLine("me-anchored → mass; plain dimensionless ratio → coupling. This is structural");
        sb.AppendLine("context (unitarity = norm conservation, QG267), not a target value.");

        Output.WriteLine(sb.ToString());

        Assert.True(AssignmentFrontierClosure.CKMUnitary(), "the CKM matrix is unitary (the mixing discriminator)");
        Assert.True(AssignmentFrontierClosure.RelationalSubclassResolved(),
            "the relational subclass is resolved by the conservation structure");
    }

    [Fact]
    public void ATQG2832_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2832: the assignment closure determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO ASSIGNMENT (score ≤ 2), PARTIAL ASSIGNMENT (3-4),");
        sb.AppendLine("    ASSIGNMENT CLOSED (5-6);");
        sb.AppendLine("  - the goal: close the Question → Physics Role mapping with a D96-native law.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AssignmentFrontierClosure.Summary()}");
        sb.AppendLine($"Closure score: {AssignmentFrontierClosure.ClosureScore()}/6");
        sb.AppendLine($"Role assignment law: {AssignmentFrontierClosure.RoleAssignmentLaw()}");
        sb.AppendLine($"all closure conditions hold: {AssignmentFrontierClosure.ClosureConditionsHold()}");
        sb.AppendLine($"CLASSIFICATION = {AssignmentFrontierClosure.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - Every question has a UNIQUE full 4-axis position (level × nature × kind ×");
        sb.AppendLine("    domain) mapping bijectively to a role: how much?→mass, how strong?→coupling,");
        sb.AppendLine("    how oriented?→mixing, how global?→cosmology, what shape?→gravity.");
        sb.AppendLine("  - The role assignment law is structural (L1-L5): dimensional→mass, unitary→mixing,");
        sb.AppendLine("    log→cosmology, power≥2→gravity, ratio→coupling.");
        sb.AppendLine("  - The relational subclass (the QG273/275 residual) is RESOLVED by the conservation");
        sb.AppendLine("    structure: norm-preserving (V†V=I) → mixing; me-anchored → mass; plain ratio →");
        sb.AppendLine("    coupling. This is structural context, not a target value.");
        sb.AppendLine("  - CONCLUSION: the assignment frontier (QG271) is CLOSED.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("ASSIGNMENT CLOSED", AssignmentFrontierClosure.Classify());
        Assert.True(AssignmentFrontierClosure.ClosureScore() >= 5);
        Assert.Contains("ASSIGNMENT CLOSED", AssignmentFrontierClosure.Summary());
        Assert.Contains("BIJECTIVE", AssignmentFrontierClosure.Summary());
    }
}
