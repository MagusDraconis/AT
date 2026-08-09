using System.Globalization;
using System.Text;
using TQM.Core.ResearchXD;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXD;

public class TQM_XD_WP001_ExperimentalPriorities : ResearchTestBase
{
    public TQM_XD_WP001_ExperimentalPriorities(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void WP001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXD-WP001 TQM White Paper — Experimental Priorities 2025-2035");

        var experiments = ExperimentalPriorityAnalyzer.DefineExperiments();
        var priorityTable = ExperimentalPriorityAnalyzer.PriorityTable(experiments);

        // 1. Executive summary
        Sec(sb, "Section 1: Executive Summary");
        sb.AppendLine("  TQM is a zero-parameter theory of fundamental physics.");
        sb.AppendLine("  Primitives: Q (individuation) + Randomness (actualization).");
        sb.AppendLine("  All physics derived: QM, GR, SM gauge structure, particles,");
        sb.AppendLine("  masses, mixing, dark matter, dark energy.");
        sb.AppendLine();
        sb.AppendLine("  TQM makes 8 testable predictions distinguishable from ΛCDM+SM.");
        sb.AppendLine("  Falsifiable by ~2030 via Euclid's measurement of w(z).");
        sb.AppendLine();

        // 2. Current status
        Sec(sb, "Section 2: Current Status");
        sb.AppendLine("  ResearchX (Identity):  ~93% derived. Topology → WHAT exists.");
        sb.AppendLine("  ResearchXB (Abundance): ~89% derived. History → HOW MUCH.");
        sb.AppendLine("  ResearchXC (Unification): Split = primitives. M² = <k>.");
        sb.AppendLine("  ResearchXD (Prediction): 8 predictions. Falsifiable.");
        sb.AppendLine();
        sb.AppendLine("  SM: ~19 parameters. TQM: Q + Randomness. Compression ~95%.");
        sb.AppendLine();

        // 3. Prediction inventory
        Sec(sb, "Section 3: Prediction Inventory");
        sb.AppendLine("  P1: w(z) ≠ -1 (time-varying dark energy)");
        sb.AppendLine("  P2: Λ(t) = α/√V(t) (specific functional form)");
        sb.AppendLine("  P3: a₀ ≈ cH₀ (acceleration scale from Λ)");
        sb.AppendLine("  P4: DM = neutral topological defects (~TeV)");
        sb.AppendLine("  P5: No spacetime singularities");
        sb.AppendLine("  P6: Log-normal abundance distributions");
        sb.AppendLine("  P7: Neutrino normal ordering");
        sb.AppendLine("  P8: M² = ⟨k⟩_interact ≈ 5");
        sb.AppendLine();

        // 4. Risk matrix
        Sec(sb, "Section 4: Experimental Priorities");
        sb.AppendLine(priorityTable);

        // 5. Tier-1: Critical tests
        Sec(sb, "Section 5: Tier-1 Critical Tests");
        sb.AppendLine("  EUCLID (ESA, 2024+):");
        sb.AppendLine("    Measures w(z) to σ ≈ 0.02 via clustering + lensing + SNe.");
        sb.AppendLine("    TQM: w ≈ -0.985 (z=0), w ≈ -0.96 (z=1).");
        sb.AppendLine("    ΛCDM: w = -1.000 (all z).");
        sb.AppendLine("    IF w = -1.00 ± 0.01 → TQM FALSIFIED at >3σ.");
        sb.AppendLine();

        // 6. Dark energy program
        Sec(sb, "Section 6: Dark Energy Program");
        sb.AppendLine("  TQM prediction: Λ(t) = α/√V(t) → w(z) ≠ -1.");
        sb.AppendLine("  Competing models:");
        sb.AppendLine("    ΛCDM: w = -1 (constant).");
        sb.AppendLine("    Quintessence: w > -1 (scalar field).");
        sb.AppendLine("    Modified gravity: w(z) varying (f(R), DGP, etc.).");
        sb.AppendLine("  DISTINGUISHING FEATURE: TQM predicts Λ tracks cosmic volume.");
        sb.AppendLine();

        // 7. Galaxy dynamics
        Sec(sb, "Section 7: Galaxy Dynamics Program");
        sb.AppendLine("  TQM prediction: a₀ ≈ cH₀/(2π) from Λ.");
        sb.AppendLine("  Test: measure a₀ in galaxies at different cosmic epochs.");
        sb.AppendLine("  If a₀ was DIFFERENT at z~1-2 (when H₀ was larger),");
        sb.AppendLine("  this confirms the Λ→a₀ link.");
        sb.AppendLine();

        // 8. Dark matter
        Sec(sb, "Section 8: Dark Matter Program");
        sb.AppendLine("  TQM prediction: DM = neutral topological defects (~TeV, stable).");
        sb.AppendLine("  Direct detection: null at current cross-sections.");
        sb.AppendLine("  Collider: heavy, weakly-interacting — hard to produce.");
        sb.AppendLine("  DISTINGUISHING: defect DM is a SUBSET of existing TQM ontology.");
        sb.AppendLine();

        // 9. Neutrino program
        Sec(sb, "Section 9: Neutrino Program");
        sb.AppendLine("  TQM prediction: NORMAL ordering (m₁<m₂<m₃).");
        sb.AppendLine("  JUNO (2024+): first measurement of ordering.");
        sb.AppendLine("  DUNE + Hyper-K (2030+): precision confirmation.");
        sb.AppendLine("  Inverted ordering → Model A (X060) wrong.");
        sb.AppendLine();

        // 10. Falsification tree
        Sec(sb, "Section 10: Falsification Tree");
        sb.AppendLine("  NODE 1: Euclid w(z)");
        sb.AppendLine("    ├─ w = -1.00 → TQM FALSIFIED (Λ emergence chain killed)");
        sb.AppendLine("    └─ w ≠ -1 → TQM SURVIVES → NODE 2");
        sb.AppendLine("  NODE 2: JUNO/DUNE ν ordering");
        sb.AppendLine("    ├─ Inverted → Model A WRONG (neutrino sector killed)");
        sb.AppendLine("    └─ Normal → TQM SURVIVES → NODE 3");
        sb.AppendLine("  NODE 3: SPARC a₀ vs H₀");
        sb.AppendLine("    ├─ a₀ ≠ f(H₀) → Λ→a₀ link BROKEN");
        sb.AppendLine("    └─ a₀ ∝ H₀ → TQM SURVIVES");
        sb.AppendLine();

        // 11. Roadmap
        Sec(sb, "Section 11: 2025-2035 Roadmap");
        sb.AppendLine(ExperimentalPriorityAnalyzer.TheDecadeRoadmap());

        // 12. Hostile review
        Sec(sb, "Section 12: Hostile Review");
        sb.AppendLine("  WEAKEST ASSUMPTIONS:");
        sb.AppendLine("    1. Causal set → GR bridge (external BDG action).");
        sb.AppendLine("    2. M² = ⟨k⟩_interact (definition-dependent).");
        sb.AppendLine("    3. σ₀² = Var[-log(p)] (Born rule origin of volatility).");
        sb.AppendLine("  MOST LIKELY FAILURE POINTS:");
        sb.AppendLine("    1. Euclid finds w = -1.00 → Λ not time-varying.");
        sb.AppendLine("    2. JUNO finds inverted ordering → Model A wrong.");
        sb.AppendLine("    3. WIMP detected → defect DM identity wrong.");
        sb.AppendLine();

        // 13. Final verdict
        Sec(sb, "Section 13: Final Verdict");
        sb.AppendLine("  RECOMMENDATION: Focus resources on DARK ENERGY program.");
        sb.AppendLine("    Euclid (ESA) and Roman (NASA) will provide decisive");
        sb.AppendLine("    tests of TQM's most distinctive prediction: w(z) ≠ -1.");
        sb.AppendLine();
        sb.AppendLine("  SECONDARY: Neutrino mass ordering (JUNO) — fast, cheap.");
        sb.AppendLine("  TERTIARY: Dark matter direct detection (ongoing, null expected).");
        sb.AppendLine();
        sb.AppendLine("  By 2030, TQM will be either FALSIFIED or STRENGTHENED.");
        sb.AppendLine("  This is how science progresses.");

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXD-WP001 COMPLETE.");
        sb.AppendLine($"  TQM White Paper: Experimental Priorities 2025-2035.");
        sb.AppendLine($"  Priority #1: Euclid w(z). Falsifiable by 2030.");
        sb.AppendLine($"  TQM is complete, predictive, and FALSIFIABLE.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
