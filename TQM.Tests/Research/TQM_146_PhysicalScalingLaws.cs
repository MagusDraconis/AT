using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_146_PhysicalScalingLaws : ResearchTestBase
{
    public TQM_146_PhysicalScalingLaws(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_146_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-146 Physical Scaling Laws from Topological Charge");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Q → L_Q → observables (TQM-145): m_eff∝Q², E∝Q, Δ∝1/Q², ...");
        sb.AppendLine("  2. These may be generic graph effects, not real physics.");
        sb.AppendLine("  3. Assume NO physical correspondence until exact matches are shown.");
        sb.AppendLine();

        Sec(sb, "1. Scaling Theory");
        sb.AppendLine(PhysicalScalingAnalyzer.ScalingTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = PhysicalScalingAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Q Scaling Laws vs Physical Systems");
        sb.AppendLine("  Observable       │ Q Scaling │ Exponent │ Exact Matches               │ Exact?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var c in report.Candidates)
            sb.AppendLine($"  {c.ObservableName,-16} │ {c.QScaling,-9} │ {c.QExponent,8:F1} │ {c.ExactMatches.FirstOrDefault() ?? "—",-28} │ {(c.HasExactCorrespondence ? "YES" : "no")}");
        sb.AppendLine();

        Sec(sb, "3. Exact Correspondences");
        sb.AppendLine($"  λ₁ = 2-2cos(π/(Q+1)) ≈ π²/Q² ≡ E₁ = π²ℏ²/(2mL²) [Particle-in-box]");
        sb.AppendLine($"  E = 2(Q-1) = trace(L) ≡ Extensive energy [Thermodynamics]");
        sb.AppendLine($"  ρ = 1 ≡ Weyl's law ρ = 1 in 1D [Spectral theory]");
        sb.AppendLine($"  C = log₂(Q) ≡ S = k_B·ln(W) [Boltzmann entropy]");
        sb.AppendLine();
        sb.AppendLine($"  {report.ExactCorrespondences}/7 exact correspondences to known physics.");
        sb.AppendLine($"  Q ↔ L (system size). Graph Laplacian ≡ kinetic energy operator.");
        sb.AppendLine();

        Sec(sb, "4. Hostile Review");
        sb.AppendLine(PhysicalScalingAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "5. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-146 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
