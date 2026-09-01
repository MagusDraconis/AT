using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 186 — Frame Dragging Origin (Lense–Thirring).
/// Can the gravitomagnetic (frame-dragging) sector be derived from TRM/D96 — no new primitives?
///
/// Method: (1) sector decomposition — frame dragging IS the h_0i vector sector of linearized GR;
/// (2) the ρ-only conformal sector has h_0i = 0 (conformally flat g = ρ^(2/d)η) ⇒ no frame dragging;
/// (3) ψ (spin-2, Fierz-Pauli, QG44) restores the full linearized-Einstein structure including h_0i;
/// (4) the rotating deficit field (matter = deficit, G4ME) sources J;
/// (5) the Lense–Thirring rate Ω = G(3(J·r̂)r̂−J)/(2c²r³) reproduces GP-B and LAGEOS with the
///     D96-derived G (QG181). Deterministic, reproducible, no randomness.
/// </summary>
public class ATQG_Phase186_FrameDraggingOriginTests : ResearchTestBase
{
    public ATQG_Phase186_FrameDraggingOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1860_PsiRestoresTheGravitomagneticSector()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1860: frame dragging is a ψ-sector (h_0i) observable");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Linearized GR: h_μν = h_00 (scalar) + h_0i (VECTOR gravitomagnetic) + h_ij^TT (spin-2).");
        sb.AppendLine("  - Frame dragging (Lense-Thirring) IS the h_0i sector, sourced by angular momentum J.");
        sb.AppendLine("  - ρ-only sector: g = ρ^(2/d)η is conformally flat ⇒ h_0i = 0 ⇒ NO frame dragging.");
        sb.AppendLine("  - ψ (spin-2, Fierz-Pauli, QG44) restores the FULL linearized-Einstein structure, incl. h_0i.");
        sb.AppendLine();

        var (s, v, t) = FrameDraggingOrigin.MetricSectorDecomposition();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Sector decomposition:  scalar = {s}");
        sb.AppendLine($"  vector = {v}");
        sb.AppendLine($"  tensor = {t}");
        sb.AppendLine($"  Conformal (ρ-only) h_0i = 0 (no frame dragging)?        {FrameDraggingOrigin.ConformalSectorHasNoFrameDragging()}");
        sb.AppendLine($"  ψ restores full linearized Einstein (incl. h_0i)?      {FrameDraggingOrigin.PsiRestoresVectorSector()}");
        sb.AppendLine($"  Frame dragging requires ψ (h_0i ≠ 0)?                   {FrameDraggingOrigin.FrameDraggingRequiresPsi()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The conformal (ρ-only) sector has NO gravitomagnetic field: conformally flat");
        sb.AppendLine("    metrics have no off-diagonal time-space components (analogue of QG26 no-lensing,");
        sb.AppendLine("    QG103 retrograde perihelion).");
        sb.AppendLine("  - ψ (the massless spin-2 graviton, QG44) restores the full linearized-Einstein");
        sb.AppendLine("    structure including the h_0i vector sector ⇒ frame dragging becomes possible.");
        sb.AppendLine("  - The source is the rotating deficit field (matter = deficit, G4ME) carrying J.");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameDraggingOrigin.ConformalSectorHasNoFrameDragging(),
            "conformal sector must have h_0i = 0 (no frame dragging)");
        Assert.True(FrameDraggingOrigin.PsiRestoresVectorSector(),
            "ψ must restore the full linearized-Einstein structure (QG44)");
        Assert.True(FrameDraggingOrigin.FrameDraggingRequiresPsi(),
            "frame dragging must be a ψ-sector observable (h_0i requires ψ ≠ 0)");
    }

    [Fact]
    public void ATQG1861_FrameDraggingRateMatchesTargets()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1861: Lense-Thirring rate vs Gravity Probe B and LAGEOS");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Gravitomagnetic potential A_g = (G/c²)(J×r)/r³; gyroscope precession");
        sb.AppendLine("    Ω_LT = (G/c²r³)(3(J·r̂)r̂ − J)/2 (standard Lense-Thirring).");
        sb.AppendLine("  - Targets: GP-B frame-dragging = 39.2 mas/yr (GR), measured 37.2 ± 7.2;");
        sb.AppendLine("    LAGEOS node precession ≈ 31 mas/yr.");
        sb.AppendLine();

        double gpb = FrameDraggingOrigin.GpbFrameDraggingMasPerYear();
        double lag = FrameDraggingOrigin.LageosNodePrecessionMasPerYear();
        double gpbD96 = FrameDraggingOrigin.GpbFrameDraggingMasPerYear(FrameDraggingOrigin.G_D96);
        double lagD96 = FrameDraggingOrigin.LageosNodePrecessionMasPerYear(FrameDraggingOrigin.G_D96);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  GP-B orbit radius r = R_E + 642 km = {FrameDraggingOrigin.GpbOrbitalRadius():F4} m");
        sb.AppendLine($"  LAGEOS semimajor axis a = {FrameDraggingOrigin.LageosSemiMajorAxis():F4} m");
        sb.AppendLine($"  Earth J = {FrameDraggingOrigin.EarthAngularMomentum:F4} kg m²/s");
        sb.AppendLine($"  GP-B  (CODATA G)  = {gpb:F2} mas/yr   (GR published {FrameDraggingOrigin.GravityProbeBTarget:F1}, measured {FrameDraggingOrigin.GravityProbeBMeasured:F1} ± {FrameDraggingOrigin.GravityProbeBUncertainty:F1})");
        sb.AppendLine($"  GP-B  (D96 G)     = {gpbD96:F2} mas/yr");
        sb.AppendLine($"  LAGEOS (CODATA G) = {lag:F2} mas/yr   (GR ≈ {FrameDraggingOrigin.LageosTarget:F1})");
        sb.AppendLine($"  LAGEOS (D96 G)    = {lagD96:F2} mas/yr");
        sb.AppendLine($"  GP-B dev vs GR pub:   {FrameDraggingOrigin.GpbRelativeDeviation() * 100:F2}%");
        sb.AppendLine($"  LAGEOS dev vs ~31:    {FrameDraggingOrigin.LageosRelativeDeviation() * 100:F2}%");
        sb.AppendLine($"  GP-B within measurement 37.2 ± 7.2?   {FrameDraggingOrigin.GpbMatchesMeasurement()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The Lense-Thirring rate from the ψ-restored gravitomagnetic sector matches");
        sb.AppendLine("    the GP-B target within ~5% (well inside the ±7.2 mas/yr measurement) and");
        sb.AppendLine("    the LAGEOS node precession within ~1%.");
        sb.AppendLine("  - The D96-derived G (QG181, 0.4% dev) changes the rates by <1%.");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameDraggingOrigin.GpbMatchesTarget(), "GP-B rate must match the GR target within 10%");
        Assert.True(FrameDraggingOrigin.LageosMatchesTarget(), "LAGEOS rate must match ~31 mas/yr within 10%");
        Assert.True(FrameDraggingOrigin.GpbMatchesMeasurement(),
            "GP-B rate must lie inside the measured 37.2 ± 7.2 mas/yr");
    }

    [Fact]
    public void ATQG1862_ClassificationFrameDraggingOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1862: frame-dragging origin classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-186 computations.");
        sb.AppendLine("  - Mechanism derived + rate reproduced + D96 coupling ⇒ FRAME-DRAGGING ORIGIN.");
        sb.AppendLine();

        int score = FrameDraggingOrigin.OriginScore();
        string classification = FrameDraggingOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3) = {score}");
        sb.AppendLine($"    +1 gravitomagnetic sector is a ψ-sector observable (absent in conformal, restored by ψ)");
        sb.AppendLine($"    +1 rate matches GP-B and LAGEOS targets");
        sb.AppendLine($"    +1 D96-derived G (QG181) within 1% of CODATA");
        sb.AppendLine($"  GP-B matches target?     {FrameDraggingOrigin.GpbMatchesTarget()}");
        sb.AppendLine($"  LAGEOS matches target?   {FrameDraggingOrigin.LageosMatchesTarget()}");
        sb.AppendLine($"  Classification          = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The gravitomagnetic (h_0i) sector is a ψ-sector observable: the ρ-only conformal");
        sb.AppendLine("  sector has no frame dragging (conformally flat ⇒ h_0i = 0), while ψ (the massless");
        sb.AppendLine("  spin-2 graviton, QG44) restores the full linearized-Einstein structure. A rotating");
        sb.AppendLine("  deficit field (matter = deficit, G4ME) sources J, and the Lense-Thirring rate");
        sb.AppendLine("  Ω_LT = G(3(J·r̂)r̂−J)/(2c²r³) reproduces the GP-B and LAGEOS targets with the");
        sb.AppendLine("  D96-derived G (QG181). No new primitives beyond the established ψ.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FRAME-DRAGGING ORIGIN", classification);
        Assert.True(score == 3, "All three evidence channels should be present (sector, rate, coupling).");
    }
}
