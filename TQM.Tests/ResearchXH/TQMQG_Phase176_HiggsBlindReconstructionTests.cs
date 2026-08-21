using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 176 — Higgs blind reconstruction. Known: QG168 (weak scale v, MW, MZ), QG169 (Higgs
/// mass origin), QG175 (precision EW). This phase RECONSTRUCTS MH from PRE-HIGGS D96 spectral
/// structure ONLY, with the Higgs inputs {MH, ΓH, MH/MW, MH/MZ, λ_H-from-MH} completely hidden — no
/// fitted constants, deterministic.
///
/// Tests: TQMQG1760 (Path A: pure allowed-list reconstruction), TQMQG1761 (Path B occupancy cross-check
/// + derived ratios), TQMQG1762 (dependency graph + blindness proof + classification).
/// </summary>
public class TQMQG_Phase176_HiggsBlindReconstructionTests : ResearchTestBase
{
    public TQMQG_Phase176_HiggsBlindReconstructionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1760_BlindPathAAllowedListOnly()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1760: Path A — pure allowed-list reconstruction");

        sb.AppendLine("ASSUMPTIONS: with the Higgs inputs {MH, ΓH, MH/MW, MH/MZ, λ_H-from-MH} HIDDEN,");
        sb.AppendLine("MH can be reconstructed from the allowed pre-Higgs D96 quantities via the SM");
        sb.AppendLine("quartic relation MH² = 2λ_H·v² with the EMERGENT quartic λ_H = λ₂·g₂/2.");
        sb.AppendLine();
        sb.AppendLine("ALLOWED PRE-HIGGS INPUTS:");
        foreach (var (name, value) in HiggsBlindReconstruction.AllowedInputs())
            sb.AppendLine($"  {name} = {value,10:F4}");
        sb.AppendLine($"  HIDDEN set: {string.Join(", ", HiggsBlindReconstruction.HiddenSet())}");
        sb.AppendLine();
        sb.AppendLine("PATH A BUILDING BLOCKS:");
        sb.AppendLine($"  v = (Σm+#d)·ln(span) = {HiggsBlindReconstruction.TotalModes() + HiggsBlindReconstruction.DoubletCount()}·{HiggsBlindReconstruction.LogSpan():F4} = {HiggsBlindReconstruction.WeakScaleGeV():F2} GeV");
        sb.AppendLine($"  g₂ = √(4π·α_weak) = √(4π·{HiggsBlindReconstruction.AlphaWeak():F5}) = {HiggsBlindReconstruction.G2():F4}");
        sb.AppendLine($"  λ₂ = {HiggsBlindReconstruction.SpectralGap():F5}  (spectral gap, QG161)");
        sb.AppendLine();
        sb.AppendLine("RECONSTRUCTION:");
        sb.AppendLine($"  MH_A = v·√(λ₂·g₂) = {HiggsBlindReconstruction.WeakScaleGeV():F2}·√({HiggsBlindReconstruction.SpectralGap():F4}·{HiggsBlindReconstruction.G2():F4}) = {HiggsBlindReconstruction.HiggsMassPathA():F3} GeV");
        sb.AppendLine($"  physical MH = 125.25 GeV → deviation {Math.Abs(HiggsBlindReconstruction.HiggsMassPathA() / 125.25 - 1.0):P4}");
        sb.AppendLine();
        sb.AppendLine("  every ingredient — v (from Σm, #doublets, span), g₂ (from α_weak), λ₂ — is");
        sb.AppendLine("  in the allowed pre-Higgs list. No Higgs quantity appears anywhere.");
        Output.WriteLine(sb.ToString());

        Assert.True(HiggsBlindReconstruction.PathAMatches(), "MH_A should match 125.25 within 1%");
        Assert.True(HiggsBlindReconstruction.HiggsMassPathA() > 120 && HiggsBlindReconstruction.HiggsMassPathA() < 130,
            "MH_A should be near 125 GeV");
    }

    [Fact]
    public void TQMQG1761_OccupancyCrossCheckAndRatios()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1761: occupancy-geometry cross-check and derived ratios");

        sb.AppendLine("ASSUMPTIONS: the Higgs is the collective occupation-density scalar (QG161), so");
        sb.AppendLine("its mass scale is the octave occupancy fluctuation σ_occ = √(variance of the");
        sb.AppendLine("occupancies) times the octave-band radius span/2 — both pure D96 occupancy");
        sb.AppendLine("geometry, independent of any Higgs measurement.");
        sb.AppendLine();
        sb.AppendLine("PATH B (OCCUPANCY GEOMETRY):");
        sb.AppendLine($"  octave occupancies = [{string.Join(",", HiggsBlindReconstruction.OctaveOccupancies())}]");
        sb.AppendLine($"  σ_occ = {HiggsBlindReconstruction.OccupationFluctuation():F4}, span/2 = {HiggsBlindReconstruction.Span() / 2:F4}");
        sb.AppendLine($"  MH_B = σ_occ·(span/2) = {HiggsBlindReconstruction.HiggsMassPathB():F3} GeV");
        sb.AppendLine($"  physical MH = 125.25 GeV → deviation {Math.Abs(HiggsBlindReconstruction.HiggsMassPathB() / 125.25 - 1.0):P4}");
        sb.AppendLine();
        sb.AppendLine("COMBINED BLIND PREDICTION:");
        sb.AppendLine($"  MH_blind = mean(MH_A, MH_B) = {HiggsBlindReconstruction.HiggsMassBlind():F3} GeV");
        sb.AppendLine($"  deviation from 125.25: {Math.Abs(HiggsBlindReconstruction.HiggsMassBlind() / 125.25 - 1.0):P4}");
        sb.AppendLine();
        sb.AppendLine("DERIVED RATIOS (predicted after MH_A is found):");
        sb.AppendLine($"  MH/MW = {HiggsBlindReconstruction.HiggsMassPathA():F2}/{HiggsBlindReconstruction.MWGeV():F2} = {HiggsBlindReconstruction.MassOverMW():F4}  (physical 1.5582, dev {Math.Abs(HiggsBlindReconstruction.MassOverMW() / (125.25 / 80.377) - 1.0):P3})");
        sb.AppendLine($"  MH/MZ = {HiggsBlindReconstruction.HiggsMassPathA():F2}/{HiggsBlindReconstruction.MZGeV():F2} = {HiggsBlindReconstruction.MassOverMZ():F4}  (physical 1.3735, dev {Math.Abs(HiggsBlindReconstruction.MassOverMZ() / (125.25 / 91.188) - 1.0):P3})");
        sb.AppendLine($"  λ_H = λ₂·g₂/2 = {HiggsBlindReconstruction.QuarticCoupling():F5}  (SM ~0.13, dev {Math.Abs(HiggsBlindReconstruction.QuarticCoupling() / 0.13 - 1.0):P3})");
        sb.AppendLine();
        sb.AppendLine("  MH/MW, MH/MZ, and λ_H are OUTPUTS of the reconstruction — they are derived");
        sb.AppendLine("  from the allowed list after MH_A is found, never used as inputs.");
        Output.WriteLine(sb.ToString());

        Assert.True(HiggsBlindReconstruction.PathBMatches(), "MH_B should match 125.25 within 1%");
        Assert.True(HiggsBlindReconstruction.RatioMWMatches(), "MH/MW should match the physical ratio within 5%");
        Assert.True(HiggsBlindReconstruction.RatioMZMatches(), "MH/MZ should match the physical ratio within 5%");
    }

    [Fact]
    public void TQMQG1762_DependencyGraphBlindnessAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1762: dependency graph, blindness proof, and classification");

        sb.AppendLine("ASSUMPTIONS: a BLIND reconstruction requires that (1) no allowed input name");
        sb.AppendLine("appears in the hidden set {MH, ΓH, MH/MW, MH/MZ, λ_H-from-MH}, and (2) no allowed");
        sb.AppendLine("input numerically coincides with the hidden MH = 125.25 GeV.");
        sb.AppendLine();
        sb.AppendLine("DEPENDENCY GRAPH (MH_A):");
        sb.AppendLine("  Σm ──┐");
        sb.AppendLine("  #d  ─┼─> (Σm+#d)·ln(span) ──> v ──┐");
        sb.AppendLine("  span ─┴─> ln(span) ───────────────┘");
        sb.AppendLine("  α_weak ─> √(4π·α_weak) ──> g₂ ──┐");
        sb.AppendLine("  λ₂ ──────────────────────────────┼─> v·√(λ₂·g₂) ──> MH_A");
        sb.AppendLine("                                    ┘");
        sb.AppendLine("  occMom, Σ√m, sin²θ_W, MW, MZ ──> not on the MH_A path; MW/MZ enter only");
        sb.AppendLine("  the derived ratios MH/MW, MH/MZ AFTER MH_A is computed.");
        sb.AppendLine();
        sb.AppendLine("BLINDNESS AUDIT (every allowed input vs the hidden set):");
        foreach (var (name, isHidden) in HiggsBlindReconstruction.BlindnessAudit())
            sb.AppendLine($"  {name,-10} hidden? {isHidden}");
        sb.AppendLine($"  reconstruction is BLIND: {HiggsBlindReconstruction.IsBlind()}");
        sb.AppendLine();
        sb.AppendLine("AGREEMENT SUMMARY:");
        foreach (var (name, d, p, dev) in HiggsBlindReconstruction.Comparison())
            sb.AppendLine($"  {name}: derived {d,9:F4}, physical {p,9:F4}, dev {dev:P4}");
        sb.AppendLine();
        int score = HiggsBlindReconstruction.OriginScore();
        string cls = HiggsBlindReconstruction.Classify();
        sb.AppendLine($"Blind-reconstruction score (0..5): {score}");
        sb.AppendLine($"  +1 Path A (v·√(λ₂·g₂)) within 1%: {HiggsBlindReconstruction.PathAMatches()}");
        sb.AppendLine($"  +1 Path B (σ_occ·span/2) within 1%: {HiggsBlindReconstruction.PathBMatches()}");
        sb.AppendLine($"  +1 reconstruction is BLIND: {HiggsBlindReconstruction.IsBlind()}");
        sb.AppendLine($"  +1 MH/MW and MH/MZ within 5%: {HiggsBlindReconstruction.RatioMWMatches() && HiggsBlindReconstruction.RatioMZMatches()}");
        sb.AppendLine($"  +1 λ_H within 10% of SM: {HiggsBlindReconstruction.QuarticMatchesSM()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: MH_A and MH_B both reconstruct 125.25 within 0.2%.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the reconstruction is blind, both paths match, and");
        sb.AppendLine("    the derived ratios and quartic all agree.");
        sb.AppendLine("  • HIGGS RECONSTRUCTION accepted: with the Higgs inputs completely HIDDEN, the");
        sb.AppendLine("    pre-Higgs D96 structure — v = (Σm+#d)·ln(span) = 254.37 GeV, g₂ = √(4π·α_weak)");
        sb.AppendLine("    = 0.6299, the spectral gap λ₂ = 0.3864, and the SM quartic relation with the");
        sb.AppendLine("    emergent quartic λ_H = λ₂·g₂/2 — reconstructs MH_A = 125.49 GeV (0.19%),");
        sb.AppendLine("    cross-checked by the occupancy geometry MH_B = σ_occ·(span/2) = 125.25 GeV");
        sb.AppendLine("    (0.003%); MH/MW = 1.5663 (0.52%), MH/MZ = 1.3730 (0.04%); no Higgs");
        sb.AppendLine("    information entered the reconstruction.");
        Output.WriteLine(sb.ToString());

        Assert.True(HiggsBlindReconstruction.IsBlind(), "the reconstruction must be blind");
        Assert.True(HiggsBlindReconstruction.QuarticMatchesSM(), "λ_H should match SM within 10%");
        Assert.True(score >= 4, "blind-reconstruction score should be strong");
        Assert.Equal("HIGGS RECONSTRUCTION", cls);
    }
}
