using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 236 — Inflation Necessity Audit. Determine whether inflation is actually required by
/// checking the five motive problems against the TQM derivations (QG227-231). Audit only.
/// </summary>
public class TQMQG_Phase236_InflationNecessityAuditTests : ResearchTestBase
{
    public TQMQG_Phase236_InflationNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2360_FiveProblemsChecked()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2360: the five inflation motive problems, checked against TQM");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each problem is checked: solved by inflation / solved by TQM / unresolved.");
        sb.AppendLine();

        sb.AppendLine("THE FIVE CHECKS:");
        foreach (var c in InflationNecessityAudit.Checks())
        {
            sb.AppendLine($"  {c.Index}. {c.Name}: {c.Resolved}");
            sb.AppendLine($"      inflation: {c.InflationSolution}");
            sb.AppendLine($"      TQM:       {c.TqmSolution}");
        }
        sb.AppendLine();
        sb.AppendLine($"  TQM-solved: {InflationNecessityAudit.TqmSolvedCount()}");
        sb.AppendLine($"  Inflation-solved: {InflationNecessityAudit.InflationSolvedCount()}");
        sb.AppendLine($"  Unresolved: {InflationNecessityAudit.UnresolvedCount()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, InflationNecessityAudit.Checks().Length);
        Assert.Equal(5, InflationNecessityAudit.TqmSolvedCount());
        Assert.Equal(0, InflationNecessityAudit.InflationSolvedCount());
        Assert.Equal(0, InflationNecessityAudit.UnresolvedCount());
    }

    [Fact]
    public void TQMQG2361_AllFiveSolvedByTqm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2361: all five motive problems are solved by TQM");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The TQM solutions draw on QG227 (uniform initial state), QG228 (information),");
        sb.AppendLine("    QG230 (Λ ~ ρ̄), QG231 (seeds + growth), QG234 (Ω_Λ + Ω_m = 1).");
        sb.AppendLine();

        sb.AppendLine("THE TQM SOLUTIONS:");
        foreach (var c in InflationNecessityAudit.Checks())
            sb.AppendLine($"  {c.Index}. {c.Name}: {c.TqmSolution}");
        sb.AppendLine();

        sb.AppendLine($"All five solved by TQM? {InflationNecessityAudit.AllFiveSolvedByTqm()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The horizon problem: the uniform critical initial state (QG227) is globally");
        sb.AppendLine("    uniform — isotropy is inherited, no epoch needed.");
        sb.AppendLine("  - The flatness problem: Ω_Λ + Ω_m = 1 is an exact structural identity (QG234).");
        sb.AppendLine("  - Initial perturbations: the Poisson seeds δ_i = 1/√⟨N⟩ (QG231).");
        sb.AppendLine("  - CMB isotropy: uniform initial state by construction (QG227/QG77).");
        sb.AppendLine("  - Structure formation: linear growth δ(a) = δ_i·a/a_i (QG231).");

        Output.WriteLine(sb.ToString());

        Assert.True(InflationNecessityAudit.AllFiveSolvedByTqm(), "all five motive problems must be solved by TQM");
    }

    [Fact]
    public void TQMQG2362_ClassificationPartialInflation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2362: classification — PARTIAL INFLATION");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - INFLATION REPLACED requires all 5 problems solved by TQM AND the observable");
        sb.AppendLine("    spectrum matched; PARTIAL INFLATION if the spectrum content is not reproduced.");
        sb.AppendLine();

        string classification = InflationNecessityAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  TQM-solved: {InflationNecessityAudit.TqmSolvedCount()}/5");
        sb.AppendLine($"  CMB anisotropy spectrum numerically derived? {!InflationNecessityAudit.CmbSpectrumNotDerived()}");
        sb.AppendLine($"  Seed spectrum is white (Poisson), not near-scale-invariant? {InflationNecessityAudit.SeedSpectrumIsWhite()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {InflationNecessityAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The inflation EPOCH is not required: all five motive problems (horizon, flatness,");
        sb.AppendLine("    perturbations, isotropy, structure formation) are solved by TQM.");
        sb.AppendLine("  - The replacement is PARTIAL because the inflationary perturbation SPECTRUM (tilt");
        sb.AppendLine("    n_s ≈ 0.96, acoustic-peak structure) is not numerically matched — the Poisson seed");
        sb.AppendLine("    is white/scale-free and the CMB anisotropy spectrum is not derived (QG235 PARTIAL).");
        sb.AppendLine($"  ⇒ {classification} — inflation is not needed as an epoch, but its spectrum content");
        sb.AppendLine("    is a remaining (observational) gap.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL INFLATION", classification);
        Assert.True(InflationNecessityAudit.AllFiveSolvedByTqm(), "all motive problems must be TQM-solved for the epoch to be replaced");
        Assert.True(InflationNecessityAudit.CmbSpectrumNotDerived(), "the CMB spectrum is the remaining gap");
    }
}
