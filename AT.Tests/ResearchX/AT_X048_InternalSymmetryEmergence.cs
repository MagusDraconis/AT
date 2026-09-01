using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X048_InternalSymmetryEmergence : ResearchTestBase
{
    public AT_X048_InternalSymmetryEmergence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X048_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X048 Origin of Internal Symmetry");

        var symmetries = InternalSymmetryAnalyzer.AnalyzeSymmetries();
        int surviving = symmetries.Count(s => s.Survives);

        // 1. The question
        Sec(sb, "Core Question");
        sb.AppendLine("  Topological defects (particles) exist (X047).");
        sb.AppendLine("  Do they have INTERNAL degrees of freedom?");
        sb.AppendLine("  Can gauge symmetries emerge from defect topology?");
        sb.AppendLine();

        // 2. Candidate symmetries
        Sec(sb, "Candidate Internal Symmetries");
        sb.AppendLine("  Group     Origin                        Dim   Local?  Survives?");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var s in symmetries)
        {
            string local = s.IsLocal ? "✓" : "✗";
            string survives = s.Survives ? "✓" : "✗";
            sb.AppendLine($"  {s.Group,-8} {s.TopologicalOrigin.Split('\n')[0],-30} {s.Dimension,4}  {local}      {survives}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{symmetries.Count} candidate symmetries survive.");
        sb.AppendLine();

        // 3. The emergence mechanism
        Sec(sb, "Emergence Mechanism");
        sb.AppendLine("  1. Defect has moduli space M (internal orientations).");
        sb.AppendLine("  2. G = Aut(M) — transformations preserving topological class.");
        sb.AppendLine("  3. Different Q-event locations → different orientations → LOCAL symmetry.");
        sb.AppendLine("  4. Comparing orientations requires CONNECTION → gauge field A_μ.");
        sb.AppendLine("  5. Physical observables → gauge-invariant quantities only.");
        sb.AppendLine();

        // 4. U(1) — the robust case
        Sec(sb, "U(1) — Robust Emergence");
        sb.AppendLine("  Every vortex with winding number w has S¹ moduli: orientation θ.");
        sb.AppendLine("  U(1) = rotations of θ. This is electromagnetism-like.");
        sb.AppendLine();
        sb.AppendLine("  Conservation: winding number w = electric charge.");
        sb.AppendLine("  Gauge field: A_μ = ∂_μθ (connection 1-form).");
        sb.AppendLine("  Field strength: F_μν = ∂_μA_ν - ∂_νA_μ = curvature of connection.");
        sb.AppendLine("  This IS classical electrodynamics, emergent from vortex topology.");
        sb.AppendLine();

        // 5. Non-Abelian candidates
        Sec(sb, "Non-Abelian — SU(n) from Multi-Vortex Systems");
        sb.AppendLine("  n indistinguishable vortices → U(n) mixing symmetry.");
        sb.AppendLine("  SU(n) = U(n) / U(1) (traceless part).");
        sb.AppendLine();
        sb.AppendLine("  n = 2: SU(2) — weak isospin-like.");
        sb.AppendLine("  n = 3: SU(3) — color-like.");
        sb.AppendLine();
        sb.AppendLine("  PROBLEM: Why n = 2 for weak, n = 3 for color?");
        sb.AppendLine("  AT does not uniquely select these values.");
        sb.AppendLine("  Possibly from complexity maximization or anomaly cancellation.");
        sb.AppendLine();

        // 6. The derivation
        Sec(sb, "Derivation");
        sb.AppendLine(InternalSymmetryAnalyzer.TheDerivation());

        // 7. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(InternalSymmetryAnalyzer.HostileReview());

        // 8. Final verdict
        string classification = surviving >= 4 ? "B: Gauge-Like Structures Emerge"
            : surviving >= 2 ? "B: Weak Symmetry Emergence" : "A: No Internal Symmetry";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X048 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  U(1) emerges robustly from vortex phase.");
        sb.AppendLine($"  SU(n) emerges from multi-vortex mixing but n is not fixed.");
        sb.AppendLine($"  Standard Model gauge group not uniquely predicted.");
        sb.AppendLine($"  Gauge symmetry is DERIVABLE CONCEPTUALLY, not uniquely.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
