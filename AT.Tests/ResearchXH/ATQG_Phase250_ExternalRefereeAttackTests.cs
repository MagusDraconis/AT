using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 250 — External Referee Audit. A hostile referee attacks QG0-QG249: the strongest
/// remaining reasons AT could still fail. Attack only — no defense.
/// </summary>
public class ATQG_Phase250_ExternalRefereeAttackTests : ResearchTestBase
{
    public ATQG_Phase250_ExternalRefereeAttackTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2500_Top25Attacks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2500: the top-25 strongest remaining attacks");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A HOSTILE referee reviews QG0-QG249; attack only, no defense.");
        sb.AppendLine("  - Severity: FATAL / MAJOR / MINOR / EDITORIAL (the referee's judgment of the damage");
        sb.AppendLine("    if the attack is right).");
        sb.AppendLine();

        sb.AppendLine("THE TOP-25 ATTACKS:");
        foreach (var a in ExternalRefereeAttack.Top25())
        {
            sb.AppendLine($"  {a.Rank,2}. [{a.Severity} / {a.Focus}]");
            sb.AppendLine($"      {a.AttackText}");
            sb.AppendLine($"      Hits: {a.TargetPhases}");
        }
        sb.AppendLine();
        sb.AppendLine($"By severity: {string.Join(", ", ExternalRefereeAttack.SeverityCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(25, ExternalRefereeAttack.Top25().Length);
        var sc = ExternalRefereeAttack.SeverityCounts();
        Assert.Equal(2, sc[ExternalRefereeAttack.Severity.Fatal]);
        Assert.Equal(14, sc[ExternalRefereeAttack.Severity.Major]);
        Assert.Equal(8, sc[ExternalRefereeAttack.Severity.Minor]);
        Assert.Equal(1, sc[ExternalRefereeAttack.Severity.Editorial]);
    }

    [Fact]
    public void ATQG2501_FatalAttacks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2501: the two FATAL attacks");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - FATAL = the attack, if right, destroys the derivation claim.");
        sb.AppendLine();

        sb.AppendLine("FATAL ATTACKS:");
        foreach (var a in ExternalRefereeAttack.Top25().Where(x => x.Severity == ExternalRefereeAttack.Severity.Fatal))
        {
            sb.AppendLine($"  [{a.Focus}]");
            sb.AppendLine($"      {a.AttackText}");
        }

        Output.WriteLine(sb.ToString());

        var fatal = ExternalRefereeAttack.Top25().Where(x => x.Severity == ExternalRefereeAttack.Severity.Fatal).ToList();
        Assert.Equal(2, fatal.Count);
        Assert.Contains("Parameter leakage", fatal.Select(f => f.Focus));
        Assert.Contains(fatal, f => f.Focus.StartsWith("Effective vs fundamental"));
    }

    [Fact]
    public void ATQG2502_Verdict()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2502: the hostile referee's verdict");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The verdict is the referee's; no defense is offered anywhere.");
        sb.AppendLine();

        sb.AppendLine($"VERDICT: {ExternalRefereeAttack.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("The referee would NOT accept, as evidence:");
        sb.AppendLine("  - the coverage register (self-maintained);");
        sb.AppendLine("  - the closure/referee audits (self-authored);");
        sb.AppendLine("  - the BOUNDARY labels (self-assigned to every hard gap);");
        sb.AppendLine("  - the passing test suite (validates the formulas it encodes).");

        Output.WriteLine(sb.ToString());

        Assert.Contains("2 FATAL", ExternalRefereeAttack.Verdict());
        Assert.Contains("attack surface", ExternalRefereeAttack.Verdict());
    }
}
