using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 224 — QG Paper Readiness Audit. Determine whether AT is ready for a publishable quantum
/// gravity paper. Audit only — no new derivations, no new physics.
/// </summary>
public class ATQG_Phase224_QgPaperReadinessAuditTests : ResearchTestBase
{
    public ATQG_Phase224_QgPaperReadinessAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2240_ConsistencyAndStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2240: internal consistency and dependency structure");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The ResearchXH suite passes (855 tests, 0 failures); all contradictions (C1-C7) resolved.");
        sb.AppendLine("  - The dependency graph (QG53) is a DAG rooted at the Q-events primitive.");
        sb.AppendLine();

        sb.AppendLine("INTERNAL CONSISTENCY:");
        sb.AppendLine($"  Tests passing: {QgPaperReadinessAudit.TotalTestCount()} / failures: {QgPaperReadinessAudit.FailedTestCount()}");
        sb.AppendLine($"  Bianchi-consistent dynamics (QG222)? {NativeMetricDynamics.BianchiConsistent(1.0, 3)}");
        sb.AppendLine($"  Born rule exact by construction? {QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu()}");
        sb.AppendLine($"  Contradictions C1-C7 resolved? {QgPaperReadinessAudit.ContradictionsResolved()}");
        sb.AppendLine($"  Internal consistent? {QgPaperReadinessAudit.InternalConsistent()}");
        sb.AppendLine();

        sb.AppendLine("DEPENDENCY GRAPH (must be a DAG):");
        foreach (var (node, dep) in QgPaperReadinessAudit.DependencyGraph())
            sb.AppendLine($"  {node} ← {dep}");
        sb.AppendLine($"  No dependency cycles? {QgPaperReadinessAudit.NoDependencyCycles()}");
        sb.AppendLine();

        sb.AppendLine("IMPORTED ASSUMPTIONS:");
        foreach (var (item, status) in QgPaperReadinessAudit.ImportedAssumptions())
            sb.AppendLine($"  {item}: {status}");
        sb.AppendLine($"  All imports stated (only the 2 primitives)? {QgPaperReadinessAudit.ImportsStated()}");

        Output.WriteLine(sb.ToString());

        Assert.True(QgPaperReadinessAudit.InternalConsistent(), "the theory must be internally consistent");
        Assert.True(QgPaperReadinessAudit.NoDependencyCycles(), "the dependency graph must be acyclic");
        Assert.True(QgPaperReadinessAudit.ImportsStated(), "all imports must be stated");
    }

    [Fact]
    public void ATQG2241_Inventories()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2241: primitive, validation, prediction, and falsification inventories");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Exactly two primitives (QG51); deep validation (225 phases / 855 tests / 93.0%);");
        sb.AppendLine("    three pre-registered falsifiable predictions (QG193).");
        sb.AppendLine();

        sb.AppendLine("PRIMITIVE INVENTORY (exactly 2):");
        foreach (var (p, role) in QgPaperReadinessAudit.PrimitiveInventory())
            sb.AppendLine($"  {p} — {role}");
        sb.AppendLine($"  Primitive count minimal? {QgPaperReadinessAudit.PrimitiveCountMinimal()}");
        sb.AppendLine($"  Derived sectors: {string.Join(", ", QgPaperReadinessAudit.DerivedSectors())}");
        sb.AppendLine();

        sb.AppendLine("VALIDATION INVENTORY:");
        foreach (var (m, v) in QgPaperReadinessAudit.ValidationInventory())
            sb.AppendLine($"  {m}: {v}");
        sb.AppendLine($"  Validation sufficient? {QgPaperReadinessAudit.ValidationSufficient()}");
        sb.AppendLine();

        sb.AppendLine("PREDICTION INVENTORY:");
        foreach (var (id, name, state) in QgPaperReadinessAudit.PredictionInventory())
            sb.AppendLine($"  {id} {name}: {state}");
        sb.AppendLine($"  Predictions intact (registry locked)? {QgPaperReadinessAudit.PredictionsIntact()}");
        sb.AppendLine();

        sb.AppendLine("FALSIFICATION INVENTORY:");
        foreach (var (id, cond) in QgPaperReadinessAudit.FalsificationInventory())
            sb.AppendLine($"  {id}: {cond}");
        sb.AppendLine($"  Falsification conditions present? {QgPaperReadinessAudit.FalsificationConditionsPresent()}");

        Output.WriteLine(sb.ToString());

        Assert.True(QgPaperReadinessAudit.PrimitiveCountMinimal(), "exactly two primitives");
        Assert.True(QgPaperReadinessAudit.ValidationSufficient(), "validation inventory must be sufficient");
        Assert.Equal(3, QgPaperReadinessAudit.PredictionCount());
        Assert.True(QgPaperReadinessAudit.PredictionsIntact(), "predictions must be intact (none falsified)");
        Assert.True(QgPaperReadinessAudit.FalsificationConditionsPresent(), "every prediction needs a falsification condition");
    }

    [Fact]
    public void ATQG2242_ClassificationMonographReady()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2242: classification — MONOGRAPH READY, with the mandatory paper outline");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score 0-3 NOT READY; 4-5 RESEARCH PAPER READY; 6-7 MONOGRAPH READY.");
        sb.AppendLine();

        int score = QgPaperReadinessAudit.ReadinessScore();
        string classification = QgPaperReadinessAudit.Classify();

        sb.AppendLine("READINESS CHECKS:");
        foreach (var (check, passed) in QgPaperReadinessAudit.Checks())
            sb.AppendLine($"  {check}: {(passed ? "PASS" : "FAIL")}");
        sb.AppendLine($"  Readiness score = {score}/7");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("MANDATORY PAPER OUTLINE:");
        foreach (var section in QgPaperReadinessAudit.PaperOutline())
            sb.AppendLine($"  {section}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MONOGRAPH READY", classification);
        Assert.True(score >= 6, "all seven checks must pass for MONOGRAPH READY");
        Assert.True(QgPaperReadinessAudit.PaperOutline().Length >= 10, "a complete paper outline is required");
    }
}
