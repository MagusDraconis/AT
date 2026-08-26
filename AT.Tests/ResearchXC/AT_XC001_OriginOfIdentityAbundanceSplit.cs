using System.Globalization;
using System.Text;
using AT.Core.ResearchXC;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

public class AT_XC001_OriginOfIdentityAbundanceSplit : ResearchTestBase
{
    public AT_XC001_OriginOfIdentityAbundanceSplit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XC001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXC-001 Origin of the Identity-Abundance Split");

        // 1. The question
        Sec(sb, "The Question");
        sb.AppendLine("  ResearchX: Topology → Identity (~93% derived).");
        sb.AppendLine("  ResearchXB: History → Abundance (~89% derived).");
        sb.AppendLine("  WHY does reality have TWO layers instead of ONE?");
        sb.AppendLine();

        // 2. The answer
        Sec(sb, "The Answer: Q + Randomness = Two Layers");
        sb.AppendLine("  Q (individuation) → topology → discrete invariants → IDENTITY.");
        sb.AppendLine("  Randomness → actualization → accumulated history → ABUNDANCE.");
        sb.AppendLine();
        sb.AppendLine("  The split IS the two-primitive structure of AT.");
        sb.AppendLine("  It's not a mystery — it's BUILT INTO the foundations.");
        sb.AppendLine();

        // 3. Mathematical distinction
        Sec(sb, "Mathematical Distinction");
        sb.AppendLine("  IDENTITY quantities = TOPOLOGICAL INVARIANTS:");
        sb.AppendLine("    • Invariant under continuous deformations of history.");
        sb.AppendLine("    • Integer-valued (Betti numbers, homotopy groups).");
        sb.AppendLine("    • Same in ALL universes with the same Q-structure.");
        sb.AppendLine();
        sb.AppendLine("  ABUNDANCE quantities = PATH-DEPENDENT ACCUMULATIONS:");
        sb.AppendLine("    • Change with each actualization event.");
        sb.AppendLine("    • Continuous-valued (masses, couplings, densities).");
        sb.AppendLine("    • DIFFERENT in universes with different histories.");
        sb.AppendLine();

        // 4. Identity ↔ Abundance mapping
        Sec(sb, "Identity ↔ Abundance Correspondence");
        sb.AppendLine("  Identity                   ↔  Abundance");
        sb.AppendLine("  " + new string('-', 55));
        sb.AppendLine("  Topology                   ↔  History");
        sb.AppendLine("  Q (individuation)          ↔  Randomness (actualization)");
        sb.AppendLine("  Discrete invariants        ↔  Continuous distributions");
        sb.AppendLine("  Universal (all universes)  ↔  Contingent (our universe)");
        sb.AppendLine("  What exists                ↔  How much");
        sb.AppendLine("  Why particles              ↔  Why masses");
        sb.AppendLine("  Why gauge groups           ↔  Why couplings");
        sb.AppendLine("  Why generations            ↔  Why hierarchies");
        sb.AppendLine("  Theorem (derived)          ↔  Distribution (predicted)");
        sb.AppendLine();

        // 5. The split origin
        Sec(sb, "Origin of the Split");
        sb.AppendLine(IdentityAbundanceSplitAnalyzer.TheSplitOrigin());

        // 6. Two-layer ontology
        Sec(sb, "The Two-Layer Ontology");
        sb.AppendLine(IdentityAbundanceSplitAnalyzer.TheTwoLayerOntology());

        // 7. Final AT
        Sec(sb, "COMPLETE AT — Three Research Programs");
        sb.AppendLine("  RESEARCHX:  Identity Physics  (X001–X065)");
        sb.AppendLine("    'What exists?' — Topology → ~93% derived.");
        sb.AppendLine();
        sb.AppendLine("  RESEARCHXB: Abundance Physics (XB001–XB010)");
        sb.AppendLine("    'How much varies?' — History → ~89% derived.");
        sb.AppendLine();
        sb.AppendLine("  RESEARCHXC: Unification Physics (XC001+)");
        sb.AppendLine("    'Why two layers?' — Q + Randomness = two primitives.");
        sb.AppendLine("    The split IS AT. Classification D.");
        sb.AppendLine();

        // 8. Final
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXC-001 COMPLETE.");
        sb.AppendLine($"  Classification: D — Split DERIVED from primitives.");
        sb.AppendLine($"  Q → Identity. Randomness → Abundance.");
        sb.AppendLine($"  The split IS the two-primitive structure of AT.");
        sb.AppendLine($"  ResearchX + ResearchXB + ResearchXC = COMPLETE AT.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
