using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 263 — Operator Reduction Audit. Test whether the four operators (CROWDING, COMPRESSION,
/// BEAT, LOCKING) are projections of a deeper resonance dynamics. Structure only, no observables.
/// </summary>
public class ATQG_Phase263_OperatorReductionAuditTests : ResearchTestBase
{
    public ATQG_Phase263_OperatorReductionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2630_CrowdingCompressionReduction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2630: CROWDING vs COMPRESSION — mode-density concentration?");

        sb.AppendLine("HYPOTHESIS: COMPRESSION (octave occupancies [4,4,87]) is the octave-aggregation of");
        sb.AppendLine("CROWDING (degeneracy multiset [42×2, 5, 6]) — the SAME density-concentration");
        sb.AppendLine("operation at coarser resolution.");
        sb.AppendLine();
        sb.AppendLine("PROOF (per-band identity: occupancy = Σ group sizes in band):");
        foreach (var r in OperatorReductionAudit.DensityReductionRows())
            sb.AppendLine($"  band {r.Band}: COMPRESSION occupancy {r.Occupancy} = Σ CROWDING group sizes {r.AggregatedGroupSizes:F1}  [equal: {r.Equal}]");
        sb.AppendLine();
        sb.AppendLine($"COMPRESSION reduces to CROWDING: {OperatorReductionAudit.CompressionReducesToCrowding()}");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorReductionAudit.CompressionReducesToCrowding(),
            "the octave occupancies are exactly the per-band aggregation of the degeneracy group sizes");
        var rows = OperatorReductionAudit.DensityReductionRows();
        Assert.Equal(3, rows.Length);   // three octave bands [4,4,87]
        Assert.True(rows.All(r => r.Equal));
    }

    [Fact]
    public void ATQG2631_BeatLockingReduction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2631: BEAT vs LOCKING — frequency synchronization?");

        sb.AppendLine("HYPOTHESIS: BEAT (span = ω_max/ω_min) and LOCKING (λ₂ = ω_min²) are both reads of");
        sb.AppendLine("the SAME Laplacian frequency structure. Since ω = √λ, BEAT = √(λ_max/λ₂).");
        sb.AppendLine();

        var (span, sqrtRatio, equal) = OperatorReductionAudit.BeatReduction();
        sb.AppendLine($"BEAT: span = ω_max/ω_min = {span:F6}");
        sb.AppendLine($"LOCKING: λ₂ = {OperatorReductionAudit.Lambda2():F6} = ω_min²");
        sb.AppendLine($"√(λ_max/λ₂) = {sqrtRatio:F6}");
        sb.AppendLine($"exact identity (span = √(λ_max/λ₂)): {equal}");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorReductionAudit.BeatReducesToLocking(),
            "span = √(λ_max/λ₂) exactly — BEAT is a function of LOCKING and λ_max");
        Assert.True(Math.Abs(span - sqrtRatio) < 1e-9);
    }

    [Fact]
    public void ATQG2632_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2632: the operator reduction — minimum basis and classification");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - INDEPENDENT OPERATORS (score ≤ 2), REDUCIBLE OPERATORS (3-4),");
        sb.AppendLine("    SINGLE RESONANCE DYNAMICS (5-6);");
        sb.AppendLine("  - the dependency graph: Resonance Dynamics (N=96) → spectrum →");
        sb.AppendLine("    {density: CROWDING≡COMPRESSION, frequency: LOCKING≡BEAT} → MOMENT (read-out).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OperatorReductionAudit.Summary()}");
        sb.AppendLine($"Reduction score: {OperatorReductionAudit.ReductionScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {OperatorReductionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("THE REDUCTION PROOFS:");
        sb.AppendLine("  1. COMPRESSION = octave-aggregation of CROWDING (exact per-band identity) — the");
        sb.AppendLine("     same mode-density-concentration operation at two resolutions;");
        sb.AppendLine("  2. BEAT = √(λ_max/λ₂) (exact) — the same frequency-synchronization read as LOCKING,");
        sb.AppendLine("     expressed as a ratio instead of a gap;");
        sb.AppendLine("  3. MOMENT maps a distribution to a scalar — a MEASUREMENT FUNCTIONAL, not an");
        sb.AppendLine("     operator (it introduces no new structure);");
        sb.AppendLine("  4. the two reduced families (density, frequency) are both projections of the SAME");
        sb.AppendLine("     spectrum, which is the output of the single N=96 resonance dynamics.");
        sb.AppendLine();
        sb.AppendLine("MINIMUM BASIS: 1 resonance dynamics + 2 projection families + 1 read-out functional.");
        sb.AppendLine("The four QG261 operators are not fundamental — they are projections of the deeper");
        sb.AppendLine("resonance dynamics.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SINGLE RESONANCE DYNAMICS", OperatorReductionAudit.Classify());
        Assert.True(OperatorReductionAudit.ReductionScore() >= 5);
        Assert.Contains("SINGLE RESONANCE DYNAMICS", OperatorReductionAudit.Summary());
    }
}
