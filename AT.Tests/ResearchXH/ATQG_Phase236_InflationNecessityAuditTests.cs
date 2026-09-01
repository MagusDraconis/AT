using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 236 — Inflation Necessity Audit. Determine whether inflation is actually required by
/// checking the five motive problems against the AT derivations (QG227-231). Audit only.
/// </summary>
public class ATQG_Phase236_InflationNecessityAuditTests : ResearchTestBase
{
    public ATQG_Phase236_InflationNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2360_FiveProblemsChecked()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2360: the five inflation motive problems, checked against AT");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each problem is checked: solved by inflation / solved by AT / unresolved.");
        sb.AppendLine();

        sb.AppendLine("THE FIVE CHECKS:");
        foreach (var c in InflationNecessityAudit.Checks())
        {
            sb.AppendLine($"  {c.Index}. {c.Name}: {c.Resolved}");
            sb.AppendLine($"      inflation: {c.InflationSolution}");
            sb.AppendLine($"      AT:       {c.AtSolution}");
        }
        sb.AppendLine();
        sb.AppendLine($"  AT-solved: {InflationNecessityAudit.AtSolvedCount()}");
        sb.AppendLine($"  Inflation-solved: {InflationNecessityAudit.InflationSolvedCount()}");
        sb.AppendLine($"  Unresolved: {InflationNecessityAudit.UnresolvedCount()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, InflationNecessityAudit.Checks().Length);
        Assert.Equal(5, InflationNecessityAudit.AtSolvedCount());
        Assert.Equal(0, InflationNecessityAudit.InflationSolvedCount());
        Assert.Equal(0, InflationNecessityAudit.UnresolvedCount());
    }

    [Fact]
    public void ATQG2361_AllFiveSolvedByAt()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2361: all five motive problems are solved by AT");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The AT solutions draw on QG227 (uniform initial state), QG228 (information),");
        sb.AppendLine("    QG230 (Λ ~ ρ̄), QG231 (seeds + growth), QG234 (Ω_Λ + Ω_m = 1).");
        sb.AppendLine();

        sb.AppendLine("THE AT SOLUTIONS:");
        foreach (var c in InflationNecessityAudit.Checks())
            sb.AppendLine($"  {c.Index}. {c.Name}: {c.AtSolution}");
        sb.AppendLine();

        sb.AppendLine($"All five solved by AT? {InflationNecessityAudit.AllFiveSolvedByAt()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The horizon problem: the uniform critical initial state (QG227) is globally");
        sb.AppendLine("    uniform — isotropy is inherited, no epoch needed.");
        sb.AppendLine("  - The flatness problem: Ω_Λ + Ω_m = 1 is an exact structural identity (QG234).");
        sb.AppendLine("  - Initial perturbations: the Poisson seeds δ_i = 1/√⟨N⟩ (QG231).");
        sb.AppendLine("  - CMB isotropy: uniform initial state by construction (QG227/QG77).");
        sb.AppendLine("  - Structure formation: linear growth δ(a) = δ_i·a/a_i (QG231).");

        Output.WriteLine(sb.ToString());

        Assert.True(InflationNecessityAudit.AllFiveSolvedByAt(), "all five motive problems must be solved by AT");
    }

    [Fact]
    public void ATQG2362_ClassificationPartialInflation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2362: classification — PARTIAL INFLATION");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - INFLATION REPLACED requires all 5 problems solved by AT AND the observable");
        sb.AppendLine("    spectrum matched; PARTIAL INFLATION if the spectrum content is not reproduced.");
        sb.AppendLine();

        string classification = InflationNecessityAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  AT-solved: {InflationNecessityAudit.AtSolvedCount()}/5");
        sb.AppendLine($"  CMB anisotropy spectrum numerically derived? {!InflationNecessityAudit.CmbSpectrumNotDerived()}");
        sb.AppendLine($"  Seed spectrum is white (Poisson), not near-scale-invariant? {InflationNecessityAudit.SeedSpectrumIsWhite()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {InflationNecessityAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The inflation EPOCH is not required: all five motive problems (horizon, flatness,");
        sb.AppendLine("    perturbations, isotropy, structure formation) are solved by AT.");
        sb.AppendLine("  - The replacement is PARTIAL because the inflationary perturbation SPECTRUM (tilt");
        sb.AppendLine("    n_s ≈ 0.96, acoustic-peak structure) is not numerically matched — the Poisson seed");
        sb.AppendLine("    is white/scale-free and the CMB anisotropy spectrum is not derived (QG235 PARTIAL).");
        sb.AppendLine($"  ⇒ {classification} — inflation is not needed as an epoch, but its spectrum content");
        sb.AppendLine("    is a remaining (observational) gap.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL INFLATION", classification);
        Assert.True(InflationNecessityAudit.AllFiveSolvedByAt(), "all motive problems must be AT-solved for the epoch to be replaced");
        Assert.True(InflationNecessityAudit.CmbSpectrumNotDerived(), "the CMB spectrum is the remaining gap");
    }
}
