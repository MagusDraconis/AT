using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 246 — Higgs Potential Origin. Derive the Higgs potential V(φ) and its vacuum
/// minimum from D96 — no new primitives, deterministic, rejects the imported Higgs potential.
/// </summary>
public class ATQG_Phase246_HiggsPotentialOriginTests : ResearchTestBase
{
    public ATQG_Phase246_HiggsPotentialOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2460_PotentialFormAndCoefficients()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2460: the potential form and its D96 coefficients");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The Higgs is the collective occupation-density deviation φ = ρ − ρ̄ (QG84/161/169);");
        sb.AppendLine("  - Energy is the actualization rate (QG89) — the potential is the self-energy of");
        sb.AppendLine("    occupation-density fluctuations;");
        sb.AppendLine("  - The D96 reflection symmetry (QG155) maps φ → −φ, forcing an even potential.");
        sb.AppendLine();

        sb.AppendLine($"FORM: {HiggsPotentialOrigin.PotentialForm()}");
        sb.AppendLine($"Reflection symmetry present (QG155)? {HiggsPotentialOrigin.ReflectionSymmetry()}");
        sb.AppendLine($"λ_H = λ₂·g₂/2 = {HiggsPotentialOrigin.QuarticCoefficient():F6}  (QG169, positive: {HiggsPotentialOrigin.QuarticPositive()})");
        sb.AppendLine($"μ² = −λ_H·v² = {HiggsPotentialOrigin.QuadraticCoefficient():F2} GeV²  (negative: {HiggsPotentialOrigin.QuadraticNegative()})");
        sb.AppendLine($"|μ| = M_H/√2 = {HiggsPotentialOrigin.TachyonicMass():F3} GeV");

        Output.WriteLine(sb.ToString());

        Assert.Contains("μ²|φ|² + λ|φ|⁴", HiggsPotentialOrigin.PotentialForm());
        Assert.True(HiggsPotentialOrigin.ReflectionSymmetry(), "the D96 reflection symmetry must be present");
        Assert.True(HiggsPotentialOrigin.QuarticPositive(), "the quartic must be positive (saturation)");
        Assert.True(HiggsPotentialOrigin.QuadraticNegative(), "μ² must be negative (vacuum instability)");
        Assert.True(HiggsPotentialOrigin.TachyonicConsistency(), "|μ| = M_H/√2");
    }

    [Fact]
    public void ATQG2461_VacuumMinimumAndSymmetryBreaking()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2461: the vacuum minimum and spontaneous symmetry breaking");

        double v = HiggsPotentialOrigin.WeakScaleGeV();
        double phiMin = HiggsPotentialOrigin.VacuumFieldValue();
        double vMin = HiggsPotentialOrigin.PotentialAtMinimum();

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The weak scale v = (Σm + #d)·ln(span) = 254.37 GeV is derived (QG168);");
        sb.AppendLine("  - The stationary point of V solves μ² + 2λ|φ|² = 0 → |φ|² = −μ²/(2λ) = v²/2.");
        sb.AppendLine();

        sb.AppendLine($"v (weak scale, QG168) = {v:F3} GeV");
        sb.AppendLine($"|φ|_min = v/√2 = {phiMin:F3} GeV");
        sb.AppendLine($"V(0) = {HiggsPotentialOrigin.PotentialAtOrigin():F1} GeV⁴  (the symmetric state)");
        sb.AppendLine($"V(±|φ|_min) = {vMin:F1} GeV⁴  (the condensate minimum)");
        sb.AppendLine($"Degenerate minima V(+)=V(−)? {HiggsPotentialOrigin.DegenerateMinima()}");
        sb.AppendLine($"Symmetry breaking occurs (V_min < V(0))? {HiggsPotentialOrigin.SymmetryBreakingOccurs()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(v, phiMin * Math.Sqrt(2.0), 6);
        Assert.True(HiggsPotentialOrigin.SymmetryBreakingOccurs(), "SSB: the condensate minimum lies below the origin");
        Assert.True(HiggsPotentialOrigin.DegenerateMinima(), "the two minima V(±v/√2) must be degenerate");
        Assert.True(vMin < 0.0, "V_min must be negative (below the symmetric origin)");
    }

    [Fact]
    public void ATQG2462_RadialModeAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2462: the radial mode and the classification");

        double mh = HiggsPotentialOrigin.HiggsMassGeV();

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The radial-mode mass is the potential curvature at the minimum (doublet");
        sb.AppendLine("    normalization): M_H² = 2λ_H·v² → M_H = v·√(λ₂·g₂).");
        sb.AppendLine();

        sb.AppendLine("KEY QUANTITIES:");
        foreach (var (name, derived, note) in HiggsPotentialOrigin.Quantities())
            sb.AppendLine($"  {name,-24} = {derived,10:F4}   {note}");

        sb.AppendLine();
        sb.AppendLine($"M_H (radial)      = {mh:F3} GeV   (physical 125.25 GeV, dev {Math.Abs(mh / 125.25 - 1.0):P3})");
        sb.AppendLine($"Origin score      = {HiggsPotentialOrigin.OriginScore()}/5");
        sb.AppendLine($"CLASSIFICATION   = {HiggsPotentialOrigin.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.True(HiggsPotentialOrigin.HiggsMatchesPhysical(), "M_H must match 125.25 GeV within 1%");
        Assert.True(HiggsPotentialOrigin.TachyonicConsistency());
        Assert.Equal(5, HiggsPotentialOrigin.OriginScore());
        Assert.Equal("POTENTIAL ORIGIN", HiggsPotentialOrigin.Classify());
    }
}
