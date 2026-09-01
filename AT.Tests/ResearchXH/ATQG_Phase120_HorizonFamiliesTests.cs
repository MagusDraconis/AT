using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 120 — Horizon suppression of families. QG119 showed local observers see fewer octave
/// families than exist globally. This phase asks: does a FINITE HORIZON NATURALLY suppress higher-family
/// modes? Investigates horizon size, mode localization, family visibility, spectral suppression, and the
/// observable family count. Classify: NO SUPPRESSION / PARTIAL SUPPRESSION / HORIZON ORIGIN.
///
/// Tests: ATQG1200 (horizon size + family visibility), ATQG1201 (mode localization + spectral
/// suppression), ATQG1202 (observable family count + classification).
/// </summary>
public class ATQG_Phase120_HorizonFamiliesTests : ResearchTestBase
{
    public ATQG_Phase120_HorizonFamiliesTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1200: horizon size + family visibility ────────────────────────────────

    [Fact]
    public void ATQG1200_HorizonSizeAndFamilyVisibility()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1200: horizon size → observable family count; family visibility");

        var curve = HorizonFamilies.ObservableFamiliesVsHorizon();
        int total = HorizonFamilies.TotalFamilies();

        sb.AppendLine("OBSERVABLE FAMILIES VS HORIZON (global N=192, total=" + total + "):");
        foreach (var (h, f) in curve)
            sb.AppendLine($"  horizon {h,3}: {f} families");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a smaller horizon sees FEWER families — the observable family count is");
        sb.AppendLine("suppressed at small scales (1 family at h=8 vs 4 at h=64). The finite horizon genuinely");
        sb.AppendLine("limits family visibility.");
        Output.WriteLine(sb.ToString());

        // smaller horizon sees fewer families than larger horizon
        var h8 = HorizonFamilies.ObservableFamilyCount(8);
        var h64 = HorizonFamilies.ObservableFamilyCount(64);
        Assert.True(h64 > h8, "larger horizon reveals more families");
        // small horizon sees strictly fewer than the total
        Assert.True(h8 < total, "small horizon sees fewer families than the global total");
        Assert.True(h64 <= total, "large horizon family count does not exceed the total");
    }

    // ── ATQG1201: mode localization + spectral suppression ───────────────────────

    [Fact]
    public void ATQG1201_ModeLocalizationAndSpectralSuppression()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1201: mode localization by family; spectral suppression profile");

        var localization = HorizonFamilies.ModeLocalizationByFamily();
        var profile = HorizonFamilies.SuppressionProfile();

        sb.AppendLine("MODE LOCALIZATION (mean IPR of global eigenmodes per octave family):");
        foreach (var (fam, ipr) in localization)
            sb.AppendLine($"  family {fam}: mean IPR {ipr:F4}");
        sb.AppendLine();
        sb.AppendLine("SUPPRESSION PROFILE (total=" + profile[0].Total + "):");
        foreach (var (h, t, v, s) in profile)
            sb.AppendLine($"  horizon {h,3}: visible={v} suppressed={s}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all family modes are DELOCALIZED (IPR ≈ 1/N — plane-wave modes on the");
        sb.AppendLine("ring), so suppression is NOT a localization effect. Suppression is SPECTRAL: the");
        sb.AppendLine("window truncates the resolvable frequency range. But the suppression profile is not");
        sb.AppendLine("perfectly monotone — the open-path window boundary can ADD spectral span (the h=128");
        sb.AppendLine("patch shows 5 families, exceeding the closed total 4) — so suppression is partial.");
        Output.WriteLine(sb.ToString());

        // modes are delocalized (plane waves): IPR ≈ 1/N, all small
        foreach (var (_, ipr) in localization)
            Assert.True(ipr < 0.05, "family modes are delocalized (small IPR)");
        // suppression is real at small horizons
        Assert.True(profile[0].Suppressed > 0, "small horizon suppresses at least one family");
        // suppression not perfectly monotone: an intermediate window patch can exceed the closed total
        Assert.True(profile.Any(p => p.Visible > p.Total) || profile[^1].Visible < profile[0].Total,
            "suppression is not perfectly monotone (window boundary adds spectral structure)");
    }

    // ── ATQG1202: observable family count + classification ───────────────────────

    [Fact]
    public void ATQG1202_ObservableCountAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1202: observable family count → NO SUPPRESSION / PARTIAL SUPPRESSION / HORIZON ORIGIN");

        bool grows = HorizonFamilies.ObservableCountGrowsWithHorizon();
        bool saturates = HorizonFamilies.SaturationAtFullHorizon();
        bool monotone = HorizonFamilies.SuppressionIsMonotone();
        string cls = HorizonFamilies.Classify();

        sb.AppendLine("SPECTRAL SUPPRESSION CHECKS:");
        sb.AppendLine($"  observable count grows (monotone) with horizon: {grows}");
        sb.AppendLine($"  observable count saturates to total at full horizon: {saturates}");
        sb.AppendLine($"  suppression is strictly monotone: {monotone}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO SUPPRESSION: a smaller horizon genuinely sees fewer families (1 at h=8 vs 4");
        sb.AppendLine("    at h=64) — the finite horizon suppresses higher-family modes.");
        sb.AppendLine("  • NOT HORIZON ORIGIN: the suppression is NOT a clean monotone function of the horizon —");
        sb.AppendLine("    the open-path window boundary adds its own spectral span (h=128 patch shows 5");
        sb.AppendLine("    families, exceeding the closed total 4), so the observable count does not follow a");
        sb.AppendLine("    pure spectral-resolution law.");
        sb.AppendLine("  • PARTIAL SUPPRESSION: a finite horizon DOES suppress higher families at small scales,");
        sb.AppendLine("    but the window-boundary structure perturbs the count — suppression exists but is not");
        sb.AppendLine("    perfectly systematic in the horizon size.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL SUPPRESSION", cls);
        Assert.True(HorizonFamilies.ObservableFamilyCount(8) < HorizonFamilies.TotalFamilies(),
            "suppression is real (small horizon sees fewer than total)");
        Assert.False(monotone, "suppression is not strictly monotone in the horizon");
    }
}
