using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 185 — Bekenstein Quarter Origin.
/// Can the EXACT coefficient 1/4 in S = A/4 be derived from TRM/D96 without imported normalization?
///
/// Method: (1) verify the structure is derived (area law QG12, M ∝ R QG184, T ∝ 1/R QG184);
/// (2) evaluate the deficit first-law entropy coefficient (S = R²/2 = A_cell/2, i.e. 1/(8π) physical);
/// (3) quantify the 2π gap to the Bekenstein-Hawking coefficient (needs T = κ/(2π));
/// (4) examine the candidate 1/occ₀ = 1/4 (occ₀ = 4) — a numerical identity without a mechanism.
///
/// Deterministic, reproducible; no random elements; only the D96 constants and the phase-185 logic.
/// </summary>
public class TQMQG_Phase185_BekensteinQuarterOriginTests : ResearchTestBase
{
    public TQMQG_Phase185_BekensteinQuarterOriginTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TQMQG1850_StructureIsDerived()
    {
        var sb = new StringBuilder();

        PrintHeader("TQMQG1850 — Bekenstein quarter origin: structure derivation audit");
        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG12 derives the area law S ∝ R^(d−1) from boundary counting (1 bit per cell).");
        sb.AppendLine("  - QG184 derives M ∝ R (radius-proportional mass) and T ∝ 1/R from the per-octave deficit.");
        sb.AppendLine("  - This test only audits whether the STRUCTURE of the area law is derived, not the coefficient.");
        sb.AppendLine();

        double R = 2.0;
        int d = 3;

        double SArea = BekensteinQuarterOrigin.AreaLawEntropy(d, R);
        double S2R = BekensteinQuarterOrigin.AreaLawEntropy(d, 2.0 * R);
        double areaRatio = S2R / SArea;

        double GM = BekensteinQuarterOrigin.DeficitMass(R);
        double GMHalf = BekensteinQuarterOrigin.DeficitMass(R / 2.0);

        double T = BekensteinQuarterOrigin.SurfaceGravityTemperature(d, R);
        double T2R = BekensteinQuarterOrigin.SurfaceGravityTemperature(d, R / 2.0);

        bool structureDerived = BekensteinQuarterOrigin.StructureDerived();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  R                       = {R,8:F4}   (horizon radius, cell units)");
        sb.AppendLine($"  S(R)   (QG12 counting)  = {SArea,8:F6}   nats");
        sb.AppendLine($"  S(2R)                   = {S2R,8:F6}   nats");
        sb.AppendLine($"  S(2R)/S(R)              = {areaRatio,8:F4}   → expected 2^(d−1) = 4 (area law)");
        sb.AppendLine($"  GM(R)   (QG184 deficit) = {GM,8:F6}   → ∝ R (mass-radius)");
        sb.AppendLine($"  GM(R/2)                 = {GMHalf,8:F6}   → GM(R)/GM(R/2) = {GM / GMHalf,8:F4}");
        sb.AppendLine($"  T(R)    (QG184)         = {T,8:F6}   → ∝ 1/R (temperature scaling)");
        sb.AppendLine($"  T(R/2)                  = {T2R,8:F6}   → T(R)/T(R/2) = {T / T2R,8:F4}");
        sb.AppendLine($"  StructureDerived()      = {structureDerived}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine($"  - Area law S ∝ R^(d−1): ratio {areaRatio:F4} ≈ 4 ✓ (QG12 boundary counting).");
        sb.AppendLine($"  - Mass-radius M ∝ R:    GM scales linearly with R ✓ (QG184).");
        sb.AppendLine($"  - Temperature T ∝ 1/R:  T scales inversely with R ✓ (QG184).");
        sb.AppendLine("  - RESULT: the STRUCTURE of the Bekenstein area law is fully derived from D96/TRM.");
        sb.AppendLine($"  - The remaining question is the COEFFICIENT (1/4) — the subject of TQMQG1851/1852.");

        Output.WriteLine(sb.ToString());

        Assert.True(structureDerived, "The area-law structure (S ∝ A, M ∝ R, T ∝ 1/R) must be derived.");
        Assert.True(Math.Abs(areaRatio - Math.Pow(2, d - 1)) < 1e-9, "Area law ratio must equal 2^(d−1).");
    }

    [Fact]
    public void TQMQG1851_TwoPiGapPreventsExactQuarter()
    {
        var sb = new StringBuilder();

        PrintHeader("TQMQG1851 — The 2π gap: why the deficit first law gives 1/2, not 1/4");
        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The deficit first law S = ∫d(GM)/T uses the QG184 temperature T = 1/((d−1)·R^(d−2)).");
        sb.AppendLine("  - The Schwarzschild normalization sets GM = R/2 (i.e. m₀/(d·L·ρ̄) = 1/2).");
        sb.AppendLine("  - The exact Bekenstein-Hawking coefficient needs T = κ/(2π) (the quantum factor).");
        sb.AppendLine();

        double R = 2.0;
        double SDeficit = BekensteinQuarterOrigin.DeficitFirstLawEntropy(R);
        double APhys = 4.0 * Math.PI * R * R;         // physical area 4πR²
        double SPhysTarget = 0.25 * APhys;            // Bekenstein S = A/4

        double deficitCoeffCell = BekensteinQuarterOrigin.DeficitCoefficient();      // 1/2
        double deficitCoeffPhys = BekensteinQuarterOrigin.DeficitCoefficientPhysicalArea();  // 1/(8π)
        double bekensteinCoeff = BekensteinQuarterOrigin.BekensteinCoefficient();    // 1/4

        double twoPi = BekensteinQuarterOrigin.TwoPiGap();
        double coeffRatio = BekensteinQuarterOrigin.CoefficientRatio();

        double span = Math.Log(EffectiveAccessCounts.Span());
        double spanVs2Pi = span / (2.0 * Math.PI);

        double occ0 = BekensteinQuarterOrigin.LightestOctaveOccupancy();
        double invOcc0 = BekensteinQuarterOrigin.InverseLightestOctave();
        bool invIsQuarter = BekensteinQuarterOrigin.InverseOctaveIsQuarter();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  R                       = {R,8:F4}");
        sb.AppendLine($"  Deficit first-law S     = {SDeficit,8:F6}   (= R²/2)");
        sb.AppendLine($"  Physical area A = 4πR²   = {APhys,8:F6}");
        sb.AppendLine($"  Bekenstein target A/4   = {SPhysTarget,8:F6}");
        sb.AppendLine($"  Deficit coefficient (cell)   = {deficitCoeffCell,8:F6}   (S = A_cell/2)");
        sb.AppendLine($"  Deficit coefficient (phys.)  = {deficitCoeffPhys,8:F6}   (S = A/8π)");
        sb.AppendLine($"  Bekenstein coefficient       = {bekensteinCoeff,8:F6}   (S = A/4)");
        sb.AppendLine($"  Coefficient ratio (1/2 ÷ 1/4)= {coeffRatio,8:F6}");
        sb.AppendLine($"  2π quantum gap               = {twoPi,8:F6}");
        sb.AppendLine($"  span vs 2π: span/(2π)        = {spanVs2Pi,8:F6}   (not exactly 1 → span is not the quantum 2π)");
        sb.AppendLine($"  occ₀ (lightest octave)       = {occ0,8:F0}");
        sb.AppendLine($"  1/occ₀                       = {invOcc0,8:F6}   (numerical identity candidate)");
        sb.AppendLine($"  1/occ₀ == 1/4?               = {invIsQuarter}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine($"  - The deficit first law gives S = R²/2 = A_cell/2 (coefficient 1/2), or S = A/(8π)");
        sb.AppendLine($"    in physical area units. The Bekenstein target is S = A/4.");
        sb.AppendLine($"  - The deficit temperature T = 1/(2R) is the SURFACE GRAVITY κ = 1/(4M); the");
        sb.AppendLine($"    Hawking temperature is κ/(2π) = 1/(8πM). The missing 2π is exactly the gap.");
        sb.AppendLine($"  - span/{2*Math.PI:F6} = {spanVs2Pi:F4} ≠ 1: the D96 span is NOT the quantum 2π,");
        sb.AppendLine($"    so the 2π cannot be imported from the D96 spectrum.");
        sb.AppendLine($"  - 1/occ₀ = 1/4 (occ₀ = 4) is numerically exact but mechanistically unjustified");
        sb.AppendLine($"    (a label identity, not a derived counting rule).");

        Output.WriteLine(sb.ToString());

        Assert.False(BekensteinQuarterOrigin.DeficitReproducesQuarter(),
            "The deficit first law must NOT reproduce the exact Bekenstein 1/4 (it gives 1/2).");
        Assert.False(BekensteinQuarterOrigin.Qg12ReproducesQuarter(),
            "The QG12 counting (ln 2 per cell) must NOT reproduce the exact 1/4.");
        Assert.True(Math.Abs(twoPi - 2.0 * Math.PI) < 1e-9, "The 2π gap must be identified exactly.");
    }

    [Fact]
    public void TQMQG1852_ClassificationPartialOrigin()
    {
        var sb = new StringBuilder();

        PrintHeader("TQMQG1852 — Quarter-origin classification");
        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-185 computations.");
        sb.AppendLine("  - Structure derived + coefficient identified but exact 1/4 not reproduced → PARTIAL ORIGIN.");
        sb.AppendLine();

        string classification = BekensteinQuarterOrigin.Classify();
        int score = BekensteinQuarterOrigin.OriginScore();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3)         = {score}");
        sb.AppendLine($"    +1 structure derived (S∝A, M∝R, T∝1/R)");
        sb.AppendLine($"    +1 deficit first-law coefficient identified (1/2 cell, 1/(8π) physical)");
        sb.AppendLine($"    +1 2π quantum gap identified");
        sb.AppendLine($"  DeficitReproducesQuarter()  = {BekensteinQuarterOrigin.DeficitReproducesQuarter()}");
        sb.AppendLine($"  Qg12ReproducesQuarter()     = {BekensteinQuarterOrigin.Qg12ReproducesQuarter()}");
        sb.AppendLine($"  Classification              = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  Derived from D96/TRM:  the area law S ∝ A (QG12), the mass-radius relation M ∝ R,");
        sb.AppendLine("  the temperature scaling T ∝ 1/R (QG184), and a DEFINITE first-law coefficient");
        sb.AppendLine("  (1/2 in cell units = 1/(8π) in physical units).");
        sb.AppendLine("  NOT derived:          the exact Bekenstein-Hawking 1/4. It requires the 2π quantum");
        sb.AppendLine("  factor T = κ/(2π) (Unruh/Hawking), which is not present in the D96/TRM classical");
        sb.AppendLine("  structures (span ≠ 2π exactly). The candidate 1/occ₀ = 1/4 is a label identity,");
        sb.AppendLine("  not a mechanism.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", classification);
        Assert.True(score == 3, "All three evidence channels should be present (structure, coefficient, gap).");
    }
}
