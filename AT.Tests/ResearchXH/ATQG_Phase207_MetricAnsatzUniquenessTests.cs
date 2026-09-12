using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 207 — Metric Ansatz Uniqueness. Test all admissible conformal powers ρ^a·η and alternative
/// counting-preserving forms on measure preservation, Bianchi consistency, Einstein recovery, and observable
/// consistency. Determine whether g = ρ^(2/d)η is uniquely selected. Deterministic.
/// </summary>
public class ATQG_Phase207_MetricAnsatzUniquenessTests : ResearchTestBase
{
    public ATQG_Phase207_MetricAnsatzUniquenessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2070_MeasurePreservationUniquelySelectsMetricPower()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2070: measure preservation uniquely selects k = 2/d");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The volume element of g = ρ^k·η is √(−g) = ρ^(kd/2).");
        sb.AppendLine("  - Measure preservation requires √(−g) = ρ (the counting measure).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        double k0 = 2.0 / MetricAnsatzUniqueness.Dimension;
        sb.AppendLine($"  √(−g) = ρ^(kd/2); measure preservation ⇒ k·d/2 = 1 ⇒ k = 2/d = {k0:F4}");
        foreach (double k in new[] { 1.0 / 3.0, 1.5 / 3.0, 2.0 / 3.0, 3.0 / 3.0 })
            sb.AppendLine($"  k = {k:F4}: volume error = {MetricAnsatzUniqueness.VolumeError(1.0, k):E3}");
        sb.AppendLine($"  Only k = 2/d preserves the measure? {MetricAnsatzUniqueness.OnlyMetricPowerPreservesMeasure()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Among conformal powers, k = 2/d is the UNIQUE exponent preserving √(−g) = ρ.");
        sb.AppendLine("  - Every other power breaks the counting-measure identification.");

        Output.WriteLine(sb.ToString());

        Assert.True(MetricAnsatzUniqueness.OnlyMetricPowerPreservesMeasure(), "only k=2/d must preserve the measure");
    }

    [Fact]
    public void ATQG2071_AccelerationAndEinsteinRecovery()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2071: geodesic acceleration and Einstein recovery select k = 2/d");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Derived geodesic acceleration (QG20/21): a = −(1/d)·d(ln ρ)/dx.");
        sb.AppendLine("  - Ansatz acceleration: a = −(k/2)·d(ln ρ)/dx.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        double target = MetricAnsatzUniqueness.DerivedAccelerationCoefficient();
        sb.AppendLine($"  Derived coefficient 1/d = {target:F4}");
        foreach (double k in new[] { 1.0 / 3.0, 2.0 / 3.0, 1.0 })
            sb.AppendLine($"  k = {k:F4}: ansatz coefficient k/2 = {MetricAnsatzUniqueness.AnsatzAccelerationCoefficient(k):F4}  match? {Math.Abs(MetricAnsatzUniqueness.AnsatzAccelerationCoefficient(k) - target) < 1e-9}");
        sb.AppendLine($"  Only k = 2/d matches? {MetricAnsatzUniqueness.OnlyMetricPowerMatchesAcceleration()}");
        sb.AppendLine($"  Einstein recovery at k = 2/d (QG197 Bianchi-conserved)? {MetricAnsatzUniqueness.EinsteinRecoveredAtMetricPower()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Only k = 2/d reproduces the derived geodesic acceleration.");
        sb.AppendLine("  - At k = 2/d the Einstein tensor is the QG197 Bianchi-conserved structure.");

        Output.WriteLine(sb.ToString());

        Assert.True(MetricAnsatzUniqueness.OnlyMetricPowerMatchesAcceleration(), "only k=2/d must match the acceleration");
        Assert.True(MetricAnsatzUniqueness.EinsteinRecoveredAtMetricPower(), "Einstein must be recovered at k=2/d");
    }

    [Fact]
    public void ATQG2072_ClassificationPartialUnique()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2072: classification — PARTIAL UNIQUE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Within the conformal class ρ^a·η, k = 2/d is uniquely selected.");
        sb.AppendLine("  - CORRECTED (G_025): the ψ tensor sector (QG44/186) gives alternative metrics that are NOT");
        sb.AppendLine("    counting-preserving — √(det g_ij) = ρ·e^(−dψ/(d−1)). ψ=0 is the only measure-preserving");
        sb.AppendLine("    member of that family.");
        sb.AppendLine();

        int score = MetricAnsatzUniqueness.OriginScore();
        string classification = MetricAnsatzUniqueness.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 measure preservation unique ({MetricAnsatzUniqueness.OnlyMetricPowerPreservesMeasure()})");
        sb.AppendLine($"    +1 acceleration unique ({MetricAnsatzUniqueness.OnlyMetricPowerMatchesAcceleration()})");
        sb.AppendLine($"    +1 Einstein recovery ({MetricAnsatzUniqueness.EinsteinRecoveredAtMetricPower()})");
        sb.AppendLine($"    +1 ψ sector changes observables ({MetricAnsatzUniqueness.PsiSectorChangesObservables()})");
        sb.AppendLine($"  ψ-perturbed √(det g_ij) = ρ preserved? {MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure()}  (CORRECTED: false)");
        sb.AppendLine($"  ψ-perturbation BREAKS the counting measure? {MetricAnsatzUniqueness.PsiPerturbationBreaksMeasure()}");
        sb.AppendLine($"  ψ=0 is the unique measure-preserving member? {MetricAnsatzUniqueness.ConformalIsTheMeasurePreservingMember()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - k = 2/d is UNIQUELY selected within the conformal-flat class (three independent");
        sb.AppendLine("    selection arguments: measure, acceleration, Einstein recovery).");
        sb.AppendLine("  - The ansatz is not the unique metric with different observables: the ψ tensor sector");
        sb.AppendLine("    (QG44/186) provides alternatives (frame dragging, lensing) — but CORRECTED (G_025) they");
        sb.AppendLine("    do NOT preserve the counting measure. The conformal ansatz is the ψ = 0 isotropic,");
        sb.AppendLine("    measure-preserving member.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL UNIQUE", classification);
        Assert.False(MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure(), "CORRECTED (G_025): ψ does NOT preserve the counting measure");
        Assert.True(MetricAnsatzUniqueness.PsiPerturbationBreaksMeasure(), "the ψ perturbation must break the measure for ψ≠0");
        Assert.True(MetricAnsatzUniqueness.ConformalIsTheMeasurePreservingMember(), "ψ=0 must be the unique measure-preserving member");
        Assert.True(MetricAnsatzUniqueness.PsiSectorChangesObservables(), "ψ sector must change the observables");
    }
}
