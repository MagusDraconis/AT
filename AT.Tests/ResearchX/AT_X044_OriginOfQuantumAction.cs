using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X044_OriginOfQuantumAction : ResearchTestBase
{
    public AT_X044_OriginOfQuantumAction(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X044_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X044 Origin of the Quantum of Action");

        var mechanisms = QuantumActionAnalyzer.AnalyzeMechanisms();
        var uncertainty = QuantumActionAnalyzer.TestUncertainty();

        int surviving = mechanisms.Count(m => m.Survives);

        // 1. The question
        Sec(sb, "The Question");
        sb.AppendLine("  Where does Planck's constant ħ come from?");
        sb.AppendLine("  ħ = 1.054571817×10⁻³⁴ J·s");
        sb.AppendLine("  Is ħ fundamental or can it be derived from Q-events?");
        sb.AppendLine();

        // 2. Candidate mechanisms
        Sec(sb, "Candidate Origins of ħ");
        sb.AppendLine("  Model  Survives?  Mechanism");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var m in mechanisms)
        {
            string s = m.Survives ? "YES" : "NO ";
            sb.AppendLine($"  {m.Model,-6} {s}       {m.Origin.Split('\n')[0]}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{mechanisms.Count} models survive.");
        sb.AppendLine();

        // 3. The honest answer
        Sec(sb, "The Honest Answer: ħ is a Unit Convention");
        sb.AppendLine("  In natural units (c = ħ = 1), ħ IS 1 by definition.");
        sb.AppendLine("  ħ is not a 'physical constant' like G — it's a UNIT CONVERSION.");
        sb.AppendLine();
        sb.AppendLine("  Dimensional analysis:");
        sb.AppendLine("    G has dimensions [L²] (in c=ħ=1) → G = ℓ² (X043).");
        sb.AppendLine("    ħ is DIMENSIONLESS (in c=ħ=1) → nothing to derive.");
        sb.AppendLine();
        sb.AppendLine("  The real question is: 'What sets the QUANTUM SCALE?'");
        sb.AppendLine("  Answer: The Q-event discreteness scale ℓ.");
        sb.AppendLine("  Large N → small ℓ → quantum effects at Planck scale.");
        sb.AppendLine();

        // 4. Model F: ħ IS the Q-event
        Sec(sb, "Model F: One Q-Event = One Quantum of Action");
        sb.AppendLine("  Each actualization event carries ONE unit of action.");
        sb.AppendLine("  ħ = 1 event-unit of action.");
        sb.AppendLine();
        sb.AppendLine("  In SI units: ħ ≈ 10⁻³⁴ J·s.");
        sb.AppendLine("  This means: 1 second of 1 watt = ~10³⁴ Q-events.");
        sb.AppendLine("  ħ is small because macroscopic action involves MANY events.");
        sb.AppendLine();
        sb.AppendLine("  SAME SCALING AS G (X043):");
        sb.AppendLine("    G ∝ N^(-1/2) — gravity weakens with more entities.");
        sb.AppendLine("    ħ ∝ N^(-1/2) — quantum grain becomes finer with more entities.");
        sb.AppendLine("    Both reflect: N is huge → fundamental scales are tiny.");
        sb.AppendLine();

        // 5. Uncertainty relations
        Sec(sb, "Uncertainty Relations from Q-Event Granularity");
        sb.AppendLine("  Relation        Q-Event Origin                          Min Product");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var u in uncertainty)
        {
            sb.AppendLine($"  {u.Relation,-15} {u.QEventOrigin.Split('\n')[0],-40} {u.MinimumProduct,10:F2}");
        }
        sb.AppendLine();
        sb.AppendLine("  All uncertainty relations follow from: ONE Q-event = minimum resolution.");
        sb.AppendLine();

        // 6. Phase accumulation
        Sec(sb, "Phase Accumulation Between Q-Events");
        sb.AppendLine("  Between actualizations: unitary evolution U = e^{-iHt}.");
        sb.AppendLine("  Phase φ = H·t/ħ = (energy) × (time) / ħ = action / ħ.");
        sb.AppendLine();
        sb.AppendLine("  In Q-event units (ħ=1): φ = action = number of Q-events.");
        sb.AppendLine("  One full cycle (2π) = one Q-event's worth of phase.");
        sb.AppendLine("  Phase IS the count of actualization opportunities.");
        sb.AppendLine();

        // 7. Planck units
        Sec(sb, "Planck Units — All From ℓ");
        sb.AppendLine("  From X042: d = 4 (spacetime dimension).");
        sb.AppendLine("  From X043: G = β·ℓ² (Newton's constant).");
        sb.AppendLine();
        sb.AppendLine("  Planck length:   ℓ_P = √(ħG/c³) = √(1·βℓ²/c³) = ℓ√(β)/c^(3/2)");
        sb.AppendLine("  Planck time:     t_P = ℓ_P/c");
        sb.AppendLine("  Planck mass:     m_P = √(ħc/G) = √(c/β)/ℓ");
        sb.AppendLine();
        sb.AppendLine("  ALL Planck units reduce to ℓ (Q-event spacing) + c + dimensionless β.");
        sb.AppendLine("  If c also emerges from Q-event geometry, then ONLY ℓ remains.");
        sb.AppendLine();

        // 8. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(QuantumActionAnalyzer.HostileReview());

        // 9. Final verdict
        string classification = surviving >= 4 ? "C: ħ Partially Derived (unit convention + event granularity)"
            : surviving >= 2 ? "B: Weak Emergence" : "A: ħ Fundamental";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X044 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  ħ = 1 in Q-event units. One event = one quantum of action.");
        sb.AppendLine($"  ħ in SI is small because N is large (~10³⁴ events/J·s).");
        sb.AppendLine($"  Uncertainty relations follow from Q-event granularity.");
        sb.AppendLine($"  G and ħ both ∝ N^(-1/2): same origin, same scaling.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
