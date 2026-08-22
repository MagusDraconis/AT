using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 196 — Quarter Coefficient Origin. Can the exact 1/4 in S = A/4 be derived — no fitting,
/// no imported Hawking factor — or is it impossible within D96/TRM? Answer: the structure (S ∝ A, M ∝ R,
/// T ∝ 1/R) is derived, but the exact 1/4 is PROVEN IMPOSSIBLE: the required bits-per-cell is π, and
/// 1/occ₀ = 1/4 is a numerical coincidence in the wrong units. PARTIAL ORIGIN. Deterministic.
/// </summary>
public class TQMQG_Phase196_QuarterCoefficientOriginTests : ResearchTestBase
{
    public TQMQG_Phase196_QuarterCoefficientOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1960_StructureDerivedCoefficientsDefinite()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1960: structure derived; QG12 and deficit first-law give definite coefficients");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - S ∝ A (QG12 boundary counting), M ∝ R and T ∝ 1/R (QG184) are established.");
        sb.AppendLine("  - Physical area A = 4πR²; Bekenstein target S/A = 1/4.");
        sb.AppendLine();

        bool structure = QuarterCoefficientOrigin.StructureDerived();
        double qg12 = QuarterCoefficientOrigin.Qg12Coefficient();
        double def = QuarterCoefficientOrigin.DeficitFirstLawCoefficient();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  structure (S∝A, M∝R, T∝1/R) derived?  {structure}");
        sb.AppendLine($"  QG12 boundary counting: S/A = ln2/(4π) = {qg12:F6}   (≠ 1/4)");
        sb.AppendLine($"  deficit first-law:      S/A = 1/(8π) = {def:F6}   (≠ 1/4, off by 2π)");
        sb.AppendLine($"  QG12 reproduces 1/4?  {QuarterCoefficientOrigin.Qg12ReproducesQuarter()}");
        sb.AppendLine($"  deficit reproduces 1/4? {QuarterCoefficientOrigin.DeficitReproducesQuarter()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The area-law structure is fully derived (QG12/QG184).");
        sb.AppendLine("  - Both natural coefficients are definite but NOT 1/4: QG12 gives ln2/(4π) ≈ 0.055,");
        sb.AppendLine("    the deficit first law gives 1/(8π) ≈ 0.040.");

        Output.WriteLine(sb.ToString());

        Assert.True(structure, "the structure must be derived");
        Assert.True(Math.Abs(qg12 - 0.0552) < 1e-3, "QG12 coefficient ≈ ln2/(4π)");
        Assert.True(Math.Abs(def - 1.0 / (8.0 * Math.PI)) < 1e-12, "deficit first-law ≈ 1/(8π)");
        Assert.False(QuarterCoefficientOrigin.Qg12ReproducesQuarter(), "QG12 does not give 1/4");
        Assert.False(QuarterCoefficientOrigin.DeficitReproducesQuarter(), "deficit first-law does not give 1/4");
    }

    [Fact]
    public void TQMQG1961_QuarterRequiresImportedPi()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1961: the bit-per-cell constraint — 1/4 requires imported π");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Boundary counting: S = b·R² (b bits per horizon cell).");
        sb.AppendLine("  - S/A_phys = 1/4 requires b = π — an imported constant, not derivable from D96/TRM.");
        sb.AppendLine();

        double bReq = QuarterCoefficientOrigin.RequiredBitsPerCell();
        double qg12b = Math.Log(2.0);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  S = b·R², A_phys = 4πR²  ⇒  S/A_phys = b/(4π)");
        sb.AppendLine($"  target S/A_phys = 1/4  ⇒  b = π = {bReq:F6} bits/cell");
        sb.AppendLine($"  QG12 natural count:  b = ln2 = {qg12b:F6} bits/cell  (S/A = ln2/(4π))");
        sb.AppendLine($"  requires imported π? {QuarterCoefficientOrigin.RequiresImportedPi()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The ONLY bits-per-cell that gives S = A_phys/4 is b = π.");
        sb.AppendLine("  - π is not a D96/TRM quantity; setting b = π would be an imported normalization,");
        sb.AppendLine("    which is forbidden in this phase (no fitting, no imported Hawking factor).");
        sb.AppendLine("  - The exact 1/4 is therefore IMPOSSIBLE to derive from D96/TRM alone.");

        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(bReq - Math.PI) < 1e-12, "required bits/cell must be π");
        Assert.True(QuarterCoefficientOrigin.RequiresImportedPi(), "the required b = π is imported");
        Assert.True(Math.Abs(qg12b - Math.PI) > 0.1, "ln2 ≠ π (QG12 count is not the required one)");
    }

    [Fact]
    public void TQMQG1962_ClassificationPartialOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1962: quarter-coefficient classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-196 impossibility proof.");
        sb.AppendLine("  - Structure derived + exact 1/4 impossible without π ⇒ PARTIAL ORIGIN.");
        sb.AppendLine();

        int score = QuarterCoefficientOrigin.OriginScore();
        string classification = QuarterCoefficientOrigin.Classify();
        double occ0 = QuarterCoefficientOrigin.LightestOctaveOccupancy();
        double occCell = QuarterCoefficientOrigin.InverseOctaveCellCoefficient();
        double occPhys = QuarterCoefficientOrigin.InverseOctavePhysicalCoefficient();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3) = {score}");
        sb.AppendLine($"    +1 structure derived (S∝A, M∝R, T∝1/R)");
        sb.AppendLine($"    +1 definite coefficients (QG12 ln2/(4π), deficit 1/(8π))");
        sb.AppendLine($"    +1 exact 1/4 proven impossible without imported π");
        sb.AppendLine($"  occ₀ = {occ0:F0}; 1/occ₀ = {occCell:F4} (cell)");
        sb.AppendLine($"  1/occ₀ S/A_phys = {occPhys:F6} = 1/(16π)  (target 0.25, ratio {occPhys / 0.25:F4} = 1/(4π))");
        sb.AppendLine($"  1/occ₀ reproduces the physical 1/4? {QuarterCoefficientOrigin.InverseOctaveReproducesQuarter()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The structure (S ∝ A, M ∝ R, T ∝ 1/R) is fully derived, but the exact 1/4 in");
        sb.AppendLine("  S = A/4 is PROVEN IMPOSSIBLE to derive from D96/TRM without fitting and without");
        sb.AppendLine("  importing π (the 2π quantum factor):");
        sb.AppendLine("    • boundary counting (QG12) gives S/A = ln2/(4π) ≈ 0.055;");
        sb.AppendLine("    • the deficit first-law (QG185) gives S/A = 1/(8π) ≈ 0.040;");
        sb.AppendLine("    • S/A = 1/4 forces b = π bits per cell — an imported constant;");
        sb.AppendLine("    • 1/occ₀ = 1/4 is a numerical coincidence in the WRONG units (π/4 physical,");
        sb.AppendLine("      it would require π = 1).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", classification);
        Assert.True(score == 3, "all three evidence channels (structure, coefficients, impossibility)");
        Assert.False(QuarterCoefficientOrigin.InverseOctaveReproducesQuarter(),
            "1/occ₀ must NOT reproduce the physical 1/4 (it gives π/4)");
    }
}
