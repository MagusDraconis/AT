using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 119 — Local vs Global Attractor Classes. QG118 showed the octave-family count of each
/// attractor geometry class scales with the network (3→4→5 as N=48→96→192). This phase asks: do LOCAL
/// observers — sampling a finite subregion (horizon) — see only a subset of the network's attractor classes?
/// Investigates local attractor accessibility, the global attractor spectrum, hidden stable classes,
/// suppression of higher classes, and observable vs total families. Classify: EXACT MATCH / LOCAL SUBSET /
/// HIDDEN CLASSES.
///
/// Tests: ATQG1190 (local accessibility + global spectrum), ATQG1191 (hidden classes + suppression),
/// ATQG1192 (observable vs total families + classification).
/// </summary>
public class ATQG_Phase119_LocalVsGlobalAttractorsTests : ResearchTestBase
{
    public ATQG_Phase119_LocalVsGlobalAttractorsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1190: local accessibility + global spectrum ───────────────────────────

    [Fact]
    public void ATQG1190_LocalAccessibilityAndGlobalSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1190: local attractor accessibility vs the global attractor spectrum");

        sb.AppendLine("GLOBAL ATTRACTOR SPECTRUM (saturated radii over the parameter plane):");
        foreach (int n in LocalVsGlobalAttractors.GlobalSizes)
            sb.AppendLine($"  N={n,3}: [{string.Join(", ", LocalVsGlobalAttractors.GlobalRadii(n).Select(r => r.ToString("F1")))}]");
        sb.AppendLine();
        sb.AppendLine("LOCAL ATTRACTOR ACCESSIBILITY (radius ladder reachable at each horizon):");
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
            sb.AppendLine($"  horizon {h,2}: [{string.Join(", ", LocalVsGlobalAttractors.LocalReachableRadii(h).Select(r => r.ToString("F2")))}]");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the geometry-class ladder ({2, 6} for K=6) is IDENTICAL at every global size and");
        sb.AppendLine("is FULLY ACCESSIBLE to every local horizon (16/24/32) — local observers can reach every");
        sb.AppendLine("global geometry class.");
        Output.WriteLine(sb.ToString());

        // global spectrum stable across sizes
        double[] g48 = LocalVsGlobalAttractors.GlobalRadii(48);
        double[] g192 = LocalVsGlobalAttractors.GlobalRadii(192);
        Assert.Equal(g48.Length, g192.Length);
        for (int i = 0; i < g48.Length; i++)
            Assert.True(Math.Abs(g48[i] - g192[i]) < 0.01, "global spectrum is size-invariant");
        // every horizon reaches every global rung
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
        {
            var local = LocalVsGlobalAttractors.LocalReachableRadii(h);
            foreach (double g in g48)
                Assert.True(local.Any(l => Math.Abs(l - g) < 0.5),
                    $"horizon {h} can reach global rung {g:F1}");
        }
    }

    // ── ATQG1191: hidden classes + suppression ────────────────────────────────────

    [Fact]
    public void ATQG1191_HiddenClassesAndSuppression()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1191: are any global classes hidden, and are higher families suppressed?");

        sb.AppendLine("HIDDEN STABLE CLASSES (global rungs unreachable at each horizon):");
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
            sb.AppendLine($"  horizon {h,2}: hidden=[{string.Join(", ", LocalVsGlobalAttractors.HiddenClasses(h).Select(r => r.ToString("F1")))}] "
                + $"(hasHidden={LocalVsGlobalAttractors.HasHiddenClasses(h)})");
        sb.AppendLine();
        sb.AppendLine("SUPPRESSION OF HIGHER FAMILIES (does local family count saturate below total?):");
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
            sb.AppendLine($"  horizon {h,2}: suppressed={LocalVsGlobalAttractors.HigherFamiliesSuppressed(h)}, "
                + $"strictSubset={LocalVsGlobalAttractors.LocalIsStrictSubset(h)}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: NO geometry class is hidden (all rungs accessible at every horizon), but the");
        sb.AppendLine("locally observable FAMILY COUNT is suppressed — higher octave families grow beyond the");
        sb.AppendLine("local horizon, so local observers see only a subset of the total family content.");
        Output.WriteLine(sb.ToString());

        // no hidden classes at any horizon
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
            Assert.False(LocalVsGlobalAttractors.HasHiddenClasses(h), $"no hidden classes at horizon {h}");
        // higher families ARE suppressed at every horizon
        foreach (int h in LocalVsGlobalAttractors.LocalHorizons)
            Assert.True(LocalVsGlobalAttractors.HigherFamiliesSuppressed(h),
                $"higher families suppressed at horizon {h}");
    }

    // ── ATQG1192: observable vs total families + classification ──────────────────

    [Fact]
    public void ATQG1192_ObservableVsTotalAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1192: observable vs total families → EXACT MATCH / LOCAL SUBSET / HIDDEN CLASSES");

        var data = LocalVsGlobalAttractors.ObservableVsTotal(24);
        string cls = LocalVsGlobalAttractors.Classify(24);

        sb.AppendLine("OBSERVABLE vs TOTAL FAMILIES (fixed horizon 24 embedded in growing networks):");
        foreach (var (n, total, local) in data)
            sb.AppendLine($"  N={n,3}: total families={total}, local window families={local}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT HIDDEN CLASSES: the geometry-class ladder is fully locally accessible (no hidden");
        sb.AppendLine("    stable classes — all radius rungs reachable at every horizon).");
        sb.AppendLine("  • NOT EXACT MATCH: the total family count GROWS with the network (QG118 scaling: 2→3→4)");
        sb.AppendLine("    while the local window's family count saturates — local observers see fewer families.");
        sb.AppendLine("  • LOCAL SUBSET: local observers sample a strict subset of the network's family spectrum —");
        sb.AppendLine("    the higher octave families are suppressed beyond the local horizon.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("LOCAL SUBSET", cls);
        // total grows, local stays constant
        Assert.True(data[^1].TotalFamilies > data[0].TotalFamilies, "total family count grows with network size");
        Assert.Equal(data.Min(d => d.LocalFamilies), data.Max(d => d.LocalFamilies));
        // local is a strict subset at large N
        Assert.True(data.Any(d => d.LocalFamilies < d.TotalFamilies));
    }
}
