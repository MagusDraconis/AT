using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>Hostile audit of the upper bound N≤3.</summary>
public class TQM_UpperBoundThreeAudit : ResearchTestBase
{
    public TQM_UpperBoundThreeAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void UpperBoundThree_HostileAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Why N≤3? — Upper-Bound Hostile Audit");

        S(sb, "Section A — Empirical facts"); sb.AppendLine(SectionA());
        S(sb, "Section B — Five derivation routes"); sb.AppendLine(SectionB());
        S(sb, "Section C — Classification of N≥4"); sb.AppendLine(SectionC());
        S(sb, "Section D — No-go theorem + remaining path"); sb.AppendLine(SectionD());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  N≥4: CONTINGENT (not impossible, not derived, not selected — empirically unobserved)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "UpperBoundThree_Report.txt"), sb.ToString());

        // Asymptotic freedom permits up to 8 generations (so it does NOT bound N≤3).
        Assert.Equal(8, UpperBoundThreeAnalyzer.MaxGenerationsAsymptoticFreedom());
        Assert.True(UpperBoundThreeAnalyzer.MaxGenerationsAsymptoticFreedom() > 3);
        Assert.True(UpperBoundThreeAnalyzer.Routes().Length == 5);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("What actually excludes N≥4:");
        sb.AppendLine();
        sb.AppendLine(UpperBoundThreeAnalyzer.EmpiricalFacts());
        sb.AppendLine();
        sb.AppendLine("  KEY: the upper bound N≤3 is EMPIRICAL (Z-width for light neutrinos + Higgs production");
        sb.AppendLine("  for ~TeV masses). Asymptotic freedom alone permits up to 8 generations — so it does NOT");
        sb.AppendLine("  bound N≤3.");
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Five derivation routes for the upper bound N≤3:");
        sb.AppendLine();
        foreach (var r in UpperBoundThreeAnalyzer.Routes())
        {
            sb.AppendLine($"  {r.Route}:");
            sb.AppendLine($"      {r.Argument}");
            sb.AppendLine($"      → {r.Verdict}");
            sb.AppendLine();
        }
        sb.AppendLine("  RESULT: NO route derives N≤3. Anomaly cancellation is per-generation (bounds the");
        sb.AppendLine("  representation, not the multiplicity); asymptotic freedom permits 8 generations;");
        sb.AppendLine("  representation theory allows any multiplicity; the only 'stability' hint is");
        sb.AppendLine("  model-dependent (X051, 5/6 models).");
        return sb.ToString();
    }

    private static string SectionC()
    {
        return
            "N≥4 is: CONTINGENT (empirically excluded up to ~TeV; theoretically possible).\n" +
            "\n" +
            "  - NOT Derived: no stability/anomaly/representation/defect/info theorem forbids N≥4.\n" +
            "  - NOT Selected: no selection mechanism removes N≥4 (a heavy 4th generation is not\n" +
            "    anthropically forbidden — it would not prevent observers).\n" +
            "  - NOT Impossible: a 4th generation with small Yukawas or a heavy neutrino is consistent\n" +
            "    with all gauge-theory constraints (asymptotic freedom OK up to 8; anomalies cancel\n" +
            "    per-generation; Z-width only bounds LIGHT neutrinos).\n" +
            "  - CONTINGENT: N≥4 is simply not observed — an empirical (contingent) fact, not a law.\n" +
            "\n" +
            "  So the upper bound N≤3 is an EMPIRICAL boundary condition, of the same character as\n" +
            "  'H ≈ 2.2e-18 s⁻¹' (QG-100): a given, not a derived, fact.";
    }

    private static string SectionD()
    {
        return
            "STRONGEST NO-GO THEOREM:\n" +
            "  No stability, anomaly-cancellation, representation-theoretic, defect-saturation, or\n" +
            "  information-capacity principle bounds N≤3. Anomaly cancellation is per-generation (each\n" +
            "  generation self-cancels); asymptotic freedom permits N_gen ≤ 8 (N_f < 33/2); representation\n" +
            "  theory allows any multiplicity; no codim-1 catastrophe gives exactly 3 stable branches;\n" +
            "  the only stability bound (defect excitation cutoff, X051) is model-dependent (5/6 models).\n" +
            "  Therefore N≤3 is an EMPIRICAL (contingent) upper bound, not a derived one: N≥4 is not\n" +
            "  impossible, merely unobserved. Under the no-new-primitives constraint, N≤3 is irreducible-\n" +
            "  CONTINGENT.\n" +
            "\n" +
            "STRONGEST REMAINING PATH:\n" +
            "  The Higgs-vacuum-stability bound: a heavy (≳TeV) 4th generation with O(1) Yukawa couplings\n" +
            "  drives the Higgs quartic λ negative below the Planck scale (vacuum instability). This is a\n" +
            "  GENUINE stability bound, but it is (a) model-dependent (depends on the 4th-gen Yukawa),\n" +
            "  (b) not absolute (small-Yukawa 4th generations survive), and (c) a quantitative (not\n" +
            "  categorical) constraint. Promoting it to a clean N≤3 theorem would be the strongest\n" +
            "  remaining route — but no such promotion currently exists. A defect-moduli argument that\n" +
            "  the n≥4 excitation level is topologically unstable (not merely energetically) would also\n" +
            "  qualify, but is absent.";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
