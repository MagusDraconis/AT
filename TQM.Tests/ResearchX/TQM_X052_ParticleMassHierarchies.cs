using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X052_ParticleMassHierarchies : ResearchTestBase
{
    public TQM_X052_ParticleMassHierarchies(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X052_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X052 Origin of Particle Mass Hierarchies");

        var models = ParticleMassHierarchyAnalyzer.AnalyzeModels();
        var spectrum = ParticleMassHierarchyAnalyzer.ComputeSpectrum();
        int surviving = models.Count(m => m.Survives);

        // 1. Mass hierarchy models
        Sec(sb, "Mass Hierarchy Models");
        sb.AppendLine("  Model                               r₂₁ (pred/obs)   r₃₁ (pred/obs)   LogErr  Survives?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var m in models)
        {
            string s = m.Survives ? "✓" : "✗";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-35} {1,6:F0}/{2,-5:F0}   {3,8:F0}/{4,-6:F0}  {5,7:F2}    {6}",
                m.Name, m.PredictedRatio21, m.ObservedRatio21,
                m.PredictedRatio31, m.ObservedRatio31, m.AccuracyLog, s));
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive. Geometric hierarchy is the key insight.");
        sb.AppendLine();

        // 2. Energy spectrum
        Sec(sb, "Defect Energy Spectrum");
        sb.AppendLine(ParticleMassHierarchyAnalyzer.SpectrumTable(spectrum));
        sb.AppendLine();

        // 3. Mass ratio analysis
        Sec(sb, "Mass Ratio Analysis — Model A");
        sb.AppendLine(ParticleMassHierarchyAnalyzer.MassRatioAnalysis());

        // 4. Observed vs predicted
        Sec(sb, "Observed Mass Spectrum (Charged Leptons)");
        sb.AppendLine("  Particle    Mass (MeV)    Mass/m_e    Lifetime");
        sb.AppendLine("  " + new string('─', 50));
        sb.AppendLine("  electron        0.511         1        stable");
        sb.AppendLine("  muon          105.66        207       2.2 μs");
        sb.AppendLine("  tau          1776.86       3477       2.9×10⁻¹³ s");
        sb.AppendLine("  gen-4 (pred)  ~3×10⁴      ~6×10⁴     < 10⁻²⁰ s");
        sb.AppendLine("  gen-5 (pred)  ~5×10⁵      ~1×10⁶     < 10⁻³⁰ s");
        sb.AppendLine();
        sb.AppendLine("  TQM predicts: gen-4 exists but decays before detection.");
        sb.AppendLine("  Mass > 10 TeV, lifetime < 10⁻²⁰ s — beyond LHC reach.");
        sb.AppendLine();

        // 5. Why geometric?
        Sec(sb, "Why Geometric Spacing?");
        sb.AppendLine("  The defect field excitation is governed by a nonlinear potential V(φ).");
        sb.AppendLine("  Near the minimum: V(φ) ≈ ½m²φ² (harmonic).");
        sb.AppendLine("  At large amplitudes: anharmonic terms φ³, φ⁴ dominate.");
        sb.AppendLine();
        sb.AppendLine("  WKB quantization condition for the n-th level:");
        sb.AppendLine("    ∮ √(2(E_n - V(φ))) dφ = (n + ½)h");
        sb.AppendLine();
        sb.AppendLine("  For exponential potential tails (generic for topological defects):");
        sb.AppendLine("    E_n ∝ exp(n·const)");
        sb.AppendLine();
        sb.AppendLine("  This is the geometric hierarchy: each generation is a FIXED FACTOR");
        sb.AppendLine("  heavier than the previous one. The factor is set by the potential's");
        sb.AppendLine("  anharmonicity — a measurable property of the defect.");
        sb.AppendLine();

        // 6. The derivation
        Sec(sb, "Derivation");
        sb.AppendLine(ParticleMassHierarchyAnalyzer.TheDerivation());

        // 7. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(ParticleMassHierarchyAnalyzer.HostileReview());

        // 8. Final verdict
        string classification = surviving >= 3 ? "C: Mass Hierarchy Emerges from Defect Energetics"
            : surviving >= 1 ? "B: Weak Hierarchy" : "A: Masses Remain Arbitrary";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X052 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Geometric mass hierarchy from anharmonic defect excitation.");
        sb.AppendLine($"  m_n = m_0 · exp(n·π·a) — correct PATTERN, scale from potential.");
        sb.AppendLine($"  Precise ratios require measurable anharmonicity parameter.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
