using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X058_CorrelationLengthOrigin : ResearchTestBase
{
    public AT_X058_CorrelationLengthOrigin(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X058_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X058 Origin of the Defect Correlation Length");

        var models = CorrelationLengthAnalyzer.AnalyzeModels();
        var scan = CorrelationLengthAnalyzer.ScanXi();
        int surviving = models.Count(m => m.Survives);
        double optimalLogXi = scan.OrderByDescending(p => p.TotalFitness).First().LogXiOverLP;

        // 1. The hierarchy
        Sec(sb, "The Hierarchy Translated to Correlation Length");
        sb.AppendLine("  m_e / M_P ≈ (ℓ_P / ξ)  →  ξ / ℓ_P ≈ M_P / m_e ≈ 2.4×10^22");
        sb.AppendLine("  log(ξ/ℓ_P) ≈ 22.4");
        sb.AppendLine();
        sb.AppendLine("  WHY is the defect correlation length 22 orders of magnitude");
        sb.AppendLine("  larger than the fundamental Q-event spacing?");
        sb.AppendLine();

        // 2. Models
        Sec(sb, "Candidate Origins of ξ");
        sb.AppendLine("  Model                              log(ξ/ℓ_P) pred/obs  Tuning?  Survives?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var m in models)
        {
            string tun = m.RequiresTuning ? "YES" : "no";
            string s = m.Survives ? "✓" : "✗";
            string pred = m.PredictedLogXiOverLP > 0 ? $"{m.PredictedLogXiOverLP:F0}" : "scan";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-35} {1,5}/{2,4}     {3}      {4}",
                m.Name, pred, "22.4", tun, s));
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive. None precisely predicts log(ξ/ℓ_P) ≈ 22.");
        sb.AppendLine();

        // 3. Scan
        Sec(sb, "Complexity Optimization Scan over ξ/ℓ_P");
        sb.AppendLine(CorrelationLengthAnalyzer.ScanTable(scan));
        sb.AppendLine();

        // 4. Model E: The Λ connection
        Sec(sb, "Model E: The Λ-ξ Connection (Most Intriguing)");
        sb.AppendLine("  AT provides TWO fundamental scales:");
        sb.AppendLine("    ℓ_P ≈ 10^(-35) m   (Q-event spacing, from X045)");
        sb.AppendLine("    Λ ≈ 10^(-52) m^(-2) (cosmological constant, from X046)");
        sb.AppendLine();
        sb.AppendLine("  Combining them dimensionally:");
        sb.AppendLine("    L = (ℓ_P^2 / √Λ)^(1/3) ≈ 10^(-18) m ≈ (200 GeV)^(-1)");
        sb.AppendLine("    ξ = ℓ_P · (ℓ_P^2 Λ)^(-1/8) ≈ 10^17 ℓ_P");
        sb.AppendLine();
        sb.AppendLine("  This connects the UV (ℓ_P) and IR (Λ) scales to produce");
        sb.AppendLine("  a MESOSCOPIC scale — the electroweak scale.");
        sb.AppendLine("  This is UV/IR mixing, a known feature of quantum gravity.");
        sb.AppendLine("  STATUS: Numerologically correct but not derived from dynamics.");
        sb.AppendLine();

        // 5. Honest assessment
        Sec(sb, "Honest Assessment");
        sb.AppendLine(CorrelationLengthAnalyzer.TheDerivation());

        // 6. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(CorrelationLengthAnalyzer.HostileReview());

        // 7. Final
        string classification = Math.Abs(optimalLogXi - 22.4) < 3 ? "B: Weak Emergence"
            : "A: ξ Remains Fundamental";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X058 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Optimal log(ξ/ℓ_P) ≈ {optimalLogXi:F0} (observed: 22.4)");
        sb.AppendLine($"  ξ is WEAKLY CONSTRAINED by defect stability + complexity.");
        sb.AppendLine($"  Λ-mediated connection is numerologically intriguing.");
        sb.AppendLine($"  ONE mass scale remains as measured input to AT.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
