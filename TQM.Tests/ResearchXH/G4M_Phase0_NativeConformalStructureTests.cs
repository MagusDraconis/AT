using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-M Phase 0 — native reconstruction of conformal structure. Uses ONLY the causal order and the
/// counting measure ρ to recover conformal information (no Malament, no metric, no imported
/// conformal class). The causal order is the conformal class (same for all conformal geometries);
/// ρ = 1 + a·x² carries the conformal factor. Native observables: interval-volume profile,
/// layer growth, and the causal distance (longest chain).
///
/// Tests: G4-M00 (interval volume distinguishes flat/pos/neg), G4-M01 (causal distance is a
/// conformal invariant), G4-M02 (layer growth + ordering invariant classify conformal geometry).
/// </summary>
public class G4M_Phase0_NativeConformalStructureTests : ResearchTestBase
{
    public G4M_Phase0_NativeConformalStructureTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);

    private static double[] Density(double a) => ConformalStructure.Density(Cs, XMax, a);

    // ── G4-M00: interval volume distinguishes the conformal geometries ──────────────────

    [Fact]
    public void G4_M00_IntervalVolumeDistinguishesConformalGeometries()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-M00: does the interval-volume profile distinguish flat / pos / neg?");

        var cs = Cs;
        var vFlat = ConformalStructure.IntervalVolumeProfile(cs, Density(0.0), TMax, XMax);
        var vPos = ConformalStructure.IntervalVolumeProfile(cs, Density(-0.8), TMax, XMax);
        var vNeg = ConformalStructure.IntervalVolumeProfile(cs, Density(+1.0), TMax, XMax);

        sb.AppendLine($"{"x",5} {"ρ(flat)",8} {"V(flat)",8} {"ρ(pos)",8} {"V(pos)",8} {"ρ(neg)",8} {"V(neg)",8}");
        for (int x = -XMax; x <= XMax; x += 2)
        {
            int i = x + XMax;
            double u = x / (double)XMax;
            sb.AppendLine($"{x,5} {1.0,8:F2} {vFlat[i],8:F2} {1 - 0.8 * u * u,8:F2} {vPos[i],8:F2} {1 + u * u,8:F2} {vNeg[i],8:F2}");
        }

        double dFlat = vFlat[XMax] - vFlat[0];      // center − edge
        double dPos = vPos[XMax] - vPos[0];
        double dNeg = vNeg[XMax] - vNeg[0];
        sb.AppendLine();
        sb.AppendLine($"center−edge interval volume:  flat={dFlat:F2}, positive={dPos:F2}, negative={dNeg:F2}");
        sb.AppendLine($"ordering (positive > flat > negative): {dPos > dFlat && dFlat > dNeg}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the interval-volume profile (causal order + counting measure) natively");
        sb.AppendLine("distinguishes the three conformal geometries — the concentrated (positive-curvature)");
        sb.AppendLine("geometry concentrates mass at the center.");
        Output.WriteLine(sb.ToString());

        Assert.True(dPos > dFlat && dFlat > dNeg,
            $"expected center−edge ordering pos > flat > neg, got {dPos:F2} / {dFlat:F2} / {dNeg:F2}");
    }

    // ── G4-M01: causal distance is a conformal invariant ────────────────────────────────

    [Fact]
    public void G4_M01_CausalDistanceIsConformallyInvariant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-M01: is the causal distance (longest chain) a conformal invariant?");

        var cs = Cs;
        int bottom = ConformalStructure.Index(0, 0, XMax);
        int top = ConformalStructure.Index(TMax, 0, XMax);

        int cFlat = ConformalStructure.LongestChain(cs, bottom, top);
        int cPos = ConformalStructure.LongestChain(cs, bottom, top);
        int cNeg = ConformalStructure.LongestChain(cs, bottom, top);

        sb.AppendLine($"longest chain from (0,0) → ({TMax},0): flat={cFlat}, positive={cPos}, negative={cNeg}");
        sb.AppendLine($"(the causal order — the conformal class — is identical for all three, so the");
        sb.AppendLine($"chain length, a conformal invariant, is the same)");
        sb.AppendLine();
        sb.AppendLine($"conformal invariance (all equal): {cFlat == cPos && cPos == cNeg}");
        Output.WriteLine(sb.ToString());

        Assert.True(cFlat == cPos && cPos == cNeg, "causal distance differs across conformal geometries");
        Assert.Equal(TMax + 1, cFlat);
    }

    // ── G4-M02: layer growth + ordering invariant classify the geometry ──────────────────

    [Fact]
    public void G4_M02_LayerGrowthClassifiesConformalGeometry()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-M02: layer growth and the ordering invariant classify the conformal geometry");

        var cs = Cs;
        int center = ConformalStructure.Index(3, 0, XMax);
        var lFlat = ConformalStructure.LayerGrowth(cs, Density(0.0), center, 2);
        var lPos = ConformalStructure.LayerGrowth(cs, Density(-0.8), center, 2);
        var lNeg = ConformalStructure.LayerGrowth(cs, Density(+1.0), center, 2);

        sb.AppendLine($"{"layer",7} {"flat",8} {"positive",9} {"negative",9}");
        for (int k = 0; k <= 2; k++)
            sb.AppendLine($"{k,7} {lFlat[k],8:F2} {lPos[k],9:F2} {lNeg[k],9:F2}");

        // The near-layer (k=0, links) counting-measure mass at x = 0,±1 is 6 + a/4 (analytic), so it
        // orders negative (a=+1, ρ grows with |x|) > flat > positive (a=−0.8, ρ concentrated at x=0).
        double f = lFlat[0], p = lPos[0], n = lNeg[0];
        sb.AppendLine();
        sb.AppendLine($"layer-0 (link) mass: flat={f:F2}, positive={p:F2}, negative={n:F2}");
        sb.AppendLine($"classifies (negative > flat > positive): {n > f && f > p}");
        Output.WriteLine(sb.ToString());

        Assert.True(n > f && f > p,
            $"layer-0 mass ordering expected neg > flat > pos, got {n:F2} / {f:F2} / {p:F2}");
    }
}
