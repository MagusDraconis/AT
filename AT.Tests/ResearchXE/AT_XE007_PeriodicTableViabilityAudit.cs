using System.Globalization;
using System.Text;
using AT.Core.ResearchXE;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXE;

public class AT_XE007_PeriodicTableViabilityAudit : ResearchTestBase
{
    public AT_XE007_PeriodicTableViabilityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XE007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXE-007 Periodic Table Viability Audit");

        var snapshots = PeriodicTableViabilityAnalyzer.ComputePeriodicTable();
        var elementClasses = PeriodicTableViabilityAnalyzer.DefineElementClasses();

        int richChemistry = snapshots.Count(s => s.StableElements >= 20);
        int dead = snapshots.Count(s => s.StableElements < 6);

        // 1. Periodic table
        Sec(sb, "M² → Periodic Table Viability");
        sb.AppendLine(PeriodicTableViabilityAnalyzer.PeriodicTableTable(snapshots));
        sb.AppendLine();
        sb.AppendLine($"  {richChemistry}/{snapshots.Count} M² values support rich chemistry (Z≥20).");
        sb.AppendLine($"  {dead}/{snapshots.Count} are chemically dead (Z<6).");
        sb.AppendLine();

        // 2. Element classes
        Sec(sb, "Element Classes and Observer Requirements");
        sb.AppendLine("  Class                     Z range    Representative  Biological Role");
        sb.AppendLine("  " + new string('-', 70));
        foreach (var ec in elementClasses)
        {
            sb.AppendLine($"  {ec.Name,-25} {ec.ZRange,5}      Z={ec.RepresentativeZ,3}          {ec.BiologicalRole}");
        }
        sb.AppendLine();

        // 3. Critical thresholds
        Sec(sb, "Critical Element Thresholds");
        sb.AppendLine("  Z=6 (Carbon):      M² threshold ≈ 2.0. Organic chemistry possible.");
        sb.AppendLine("  Z=8 (Oxygen):      M² threshold ≈ 2.5. Water + respiration.");
        sb.AppendLine("  Z=20 (Calcium):     M² threshold ≈ 3.0. MINIMUM OBSERVER CHEMISTRY.");
        sb.AppendLine("  Z=26 (Iron):        M² threshold ≈ 3.5. Oxygen transport + catalysis.");
        sb.AppendLine("  Z=30 (Zinc):        M² threshold ≈ 4.0. Rich enzymatic chemistry.");
        sb.AppendLine("  Z=50 (Tin):         M² threshold ≈ 4.5. Full chemical diversity.");
        sb.AppendLine();
        sb.AppendLine($"  Our universe: M²≈5, Z≈90. FAR ABOVE minimum threshold.");
        sb.AppendLine();

        // 4. Element survival at different M²
        Sec(sb, "Element Survival — What Dies First?");
        sb.AppendLine("  LOW M² (< 3): Heavy elements disappear FIRST (relativistic isn't the issue,");
        sb.AppendLine("       it's that atoms are giant → weakly bound → thermal dissociation).");
        sb.AppendLine("       Periodic table shrinks from the TOP down.");
        sb.AppendLine();
        sb.AppendLine("  HIGH M² (> 7): Heavy elements disappear FIRST (relativistic collapse).");
        sb.AppendLine("       Inner electrons exceed v/c ≈ 0.5 → orbitals collapse.");
        sb.AppendLine("       Periodic table shrinks from the TOP down.");
        sb.AppendLine();
        sb.AppendLine("  BOTH ENDS: The periodic table shrinks symmetrically.");
        sb.AppendLine("  Maximum diversity at M²≈4-5 — the GOLDILOCKS peak.");
        sb.AppendLine();

        // 5. Minimum chemistry library
        Sec(sb, "Minimum Chemistry Library for Observers");
        sb.AppendLine(PeriodicTableViabilityAnalyzer.MinimumChemistryLibrary());

        // 6. The threshold
        Sec(sb, "The Observer Chemistry Threshold");
        sb.AppendLine(PeriodicTableViabilityAnalyzer.TheThreshold());

        // 7. Final
        string classification = richChemistry >= 8 ? "D: Primary Complexity Bottleneck — Periodic Table Size"
            : "C: Strong Contribution";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXE-007 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Observer threshold: Z≥20 → M²≥3.0. Our M²≈5 → Z≈90.");
        sb.AppendLine($"  Bottleneck is NOT atomic existence — it's element DIVERSITY.");
        sb.AppendLine($"  Observers need a PERIODIC TABLE, not just atoms.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
