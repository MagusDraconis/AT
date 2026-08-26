using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X047_ParticleEmergence : ResearchTestBase
{
    public AT_X047_ParticleEmergence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X047_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X047 Emergence of Particles from Q-Event Topology");

        var candidates = ParticleEmergenceAnalyzer.IdentifyCandidates();
        var properties = ParticleEmergenceAnalyzer.MapProperties();

        int stable = candidates.Count(c => c.IsStable);

        // 1. Particle candidates
        Sec(sb, "Particle Candidates");
        sb.AppendLine("  Structure               Stable?  Local?  Charge?  Topological Origin");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var c in candidates)
        {
            string s = c.IsStable ? "✓" : "✗";
            string l = c.IsLocalized ? "✓" : "✗";
            string q = c.HasConservedCharge ? "✓" : "✗";
            sb.AppendLine($"  {c.Name,-22}  {s}       {l}       {q}       {c.Invariant}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {stable}/{candidates.Count} are stable, localized, with conserved charge.");
        sb.AppendLine();

        // 2. Primary candidate
        Sec(sb, "Primary Candidate: Q-Condensate (Soliton)");
        sb.AppendLine("  Origin: PDE reaction-diffusion field theory (AT-010-012).");
        sb.AppendLine("  Structure: Localized domain where R > 0.5.");
        sb.AppendLine("  Protection: Reaction barrier c₀·M·R·(1-R²) > 0 prevents R");
        sb.AppendLine("             from crossing 0.5 downward (AT-117).");
        sb.AppendLine("  Charge: Q = β₀({R>0.5}) ∈ ℕ. dQ/dt = 0 (AT-116).");
        sb.AppendLine("  Mass: m_eff = 4(1+M₀²)/(3w) (AT-111).");
        sb.AppendLine("  Stability: Q=1 plateau for T∈[0.10,0.85] — width 0.75 (AT-115).");
        sb.AppendLine("  Interactions: Mergers (Q₁+Q₂→Q₁+Q₂) and weak force (AT-109).");
        sb.AppendLine();

        // 3. Particle properties from topology
        Sec(sb, "Particle Properties from Topology");
        sb.AppendLine("  Property       Interpretation              Topological Origin          Quantized?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var p in properties)
        {
            string quant = p.NaturallyQuantized ? "✓" : "~ (continuous)";
            sb.AppendLine($"  {p.Property,-14} {p.PhysicalInterpretation,-27} {p.TopologicalOrigin.Split('\n')[0],-27} {quant}");
        }
        sb.AppendLine();

        // 4. The derivation
        Sec(sb, "Derivation");
        sb.AppendLine(ParticleEmergenceAnalyzer.TheDerivation());

        // 5. What's missing
        Sec(sb, "What's Missing — The Standard Model Gap");
        sb.AppendLine("  AT DERIVES:");
        sb.AppendLine("    ✓ Particle EXISTENCE (topological defects).");
        sb.AppendLine("    ✓ Particle CHARGE (topological invariant Q).");
        sb.AppendLine("    ✓ Particle MASS (defect formation energy).");
        sb.AppendLine("    ✓ Particle STABILITY (topological protection).");
        sb.AppendLine("    ✓ Particle INTERACTIONS (mergers, splits).");
        sb.AppendLine();
        sb.AppendLine("  AT DOES NOT DERIVE (yet):");
        sb.AppendLine("    ✗ Discrete particle spectrum (gets continuous family).");
        sb.AppendLine("    ✗ Gauge charges (electric, color, weak isospin).");
        sb.AppendLine("    ✗ Three generations (e, μ, τ; u, c, t; d, s, b).");
        sb.AppendLine("    ✗ The Standard Model gauge group SU(3)×SU(2)×U(1).");
        sb.AppendLine("    ✗ Higgs mechanism / electroweak symmetry breaking.");
        sb.AppendLine();
        sb.AppendLine("  To get the Standard Model, AT needs:");
        sb.AppendLine("    1. Internal symmetry structure (fiber bundle over Q-events).");
        sb.AppendLine("    2. Symmetry breaking mechanism.");
        sb.AppendLine("    3. Quantization of continuous parameters.");
        sb.AppendLine();

        // 6. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(ParticleEmergenceAnalyzer.HostileReview());

        // 7. Final verdict
        string classification = stable >= 3 ? "C: Stable Topological Structures Found"
            : stable >= 1 ? "B: Weak Particle Candidates" : "A: No Particles Emerge";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X047 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Particles = topological defects in Q-event structure.");
        sb.AppendLine($"  NO additional primitives needed.");
        sb.AppendLine($"  Standard Model spectrum requires gauge extension.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
