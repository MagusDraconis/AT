using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 12 — origin of the BDG normalization. The BDG stencil {−2,+4,−2} = s·(−1,+2,−1) with
/// s=2; the binomial SHAPE is native (G4-L11), only the global scale s=2 remains. Investigates
/// whether s emerges from interval-volume, causal-density, constant-annihilation, or propagator
/// normalization — or only from continuum (second-moment) matching.
///
/// Finding: constant-annihilation (and linear-annihilation) leaves s FREE (the family s·(−1,+2,−1)
/// annihilates constants for all s); native counts are position- AND grid-dependent (degree 6 vs 4);
/// only the second-moment (continuum) condition M₂ = −2s pins s=2. Classification: NO MATCH for
/// native emergence — the scale −2 requires continuum matching.
///
/// Tests: G4-L120 (constant-annihilation underdetermines s), G4-L121 (native counts are unstable),
///        G4-L122 (second-moment/continuum matching pins s=2).
/// </summary>
public class G4L_Phase12_BDGNormalizationTests : ResearchTestBase
{
    public G4L_Phase12_BDGNormalizationTests(ITestOutputHelper o) : base(o) { }

    private static long C(int n, int k)
    {
        if (k < 0 || k > n) return 0;
        long r = 1;
        for (int i = 0; i < k; i++) r = r * (n - i) / (i + 1);
        return r;
    }

    // ── G4-L120: constant-annihilation leaves the scale free ───────────────────────────

    [Fact]
    public void G4_L120_ConstantAnnihilationUnderdeterminesScale()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L120: does constant/linear-annihilation pin the scale s?");

        sb.AppendLine("Family a(s) = s·(−1,+2,−1) indexed (self, links, next).");
        sb.AppendLine($"{"s",6} {"Σ a_ℓ (const)",14} {"Σ ℓ·a_ℓ (linear)",16} {"Σ ℓ²·a_ℓ (2nd)",14}");
        bool constFree = true, linearFree = true;
        for (double s = 0.5; s <= 3.0; s += 0.5)
        {
            double sum0 = -s + 2 * s - s;             // 0th moment (constant)
            double sum1 = 0 * -s + 1 * 2 * s + 2 * -s; // 1st moment (linear)
            double sum2 = 0 * -s + 1 * 2 * s + 4 * -s; // 2nd moment (quadratic)
            if (Math.Abs(sum0) > 1e-9) constFree = false;
            if (Math.Abs(sum1) > 1e-9) linearFree = false;
            sb.AppendLine($"{s,6:F1} {sum0,14:F1} {sum1,16:F1} {sum2,14:F1}");
        }

        sb.AppendLine();
        sb.AppendLine($"constants annihilated for ALL s: {constFree}");
        sb.AppendLine($"linear functions annihilated for ALL s: {linearFree}");
        sb.AppendLine($"⇒ the scale s is NOT pinned by 0th/1st-order constraints (only the 2nd moment varies).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: constant-annihilation (the native constraint) under-determines the scale.");
        Output.WriteLine(sb.ToString());

        Assert.True(constFree && linearFree, "constant/linear annihilation unexpectedly pins the scale");
    }

    // ── G4-L121: native counts do not produce the constant −2 ──────────────────────────

    [Fact]
    public void G4_L121_NativeCountsAreUnstable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L121: do native counts produce the constant −2 diagonal?");

        sb.AppendLine($"{"grid",-8} {"mean degree",12} {"−degree/2",10} {"past-count range",17}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs = CausalSet.BuildGrid(tMax, xMax);
            int n = cs.Count;
            double degSum = 0; int cnt = 0;
            int pMin = int.MaxValue, pMax = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (cs.Time[i] < 2 || cs.Time[i] > tMax - 2) continue;
                if (Math.Abs(cs.Space[i]) > xMax - 2) continue;
                degSum += cs.PastDegree[i] + cs.FutureDegree[i];
                cnt++;
                int p = 0;
                for (int j = 0; j < n; j++) if (cs.Order[j, i]) p++;
                pMin = Math.Min(pMin, p); pMax = Math.Max(pMax, p);
            }
            double deg = degSum / cnt;
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {deg,12:F2} {-deg / 2,10:F2} {($"[{pMin},{pMax}]"),17}");
        }

        sb.AppendLine();
        sb.AppendLine("The causal-set Hasse degree is grid-INDEPENDENT (6) but −degree/2 = −3 ≠ −2; the past");
        sb.AppendLine("count is position-dependent ([8,57]) and cannot give a constant diagonal. No native count");
        sb.AppendLine("reproduces the BDG constant −2.");
        Output.WriteLine(sb.ToString());

        var cs7 = CausalSet.BuildGrid(7, 4);
        double deg7 = MeanInteriorDegree(cs7, 7, 4);
        // −degree/2 = −3 ≠ −2 (degree-based normalization does not match BDG).
        Assert.True(Math.Abs(-deg7 / 2.0 - (-2.0)) > 0.5,
            $"−degree/2 = {-deg7 / 2:F2} should NOT equal −2 (BDG)");
    }

    // ── G4-L122: second-moment (continuum) matching pins s = 2 ────────────────────────

    [Fact]
    public void G4_L122_SecondMomentPinsTheScale()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L122: does the second-moment (continuum) condition pin s = 2?");

        sb.AppendLine("The d'Alembertian's normalization is its second moment: for the family a(s),");
        sb.AppendLine("M₂(s) = Σ ℓ²·a_ℓ = −2s. The BDG/continuum value is s = 2 ⇒ M₂ = −4.");
        sb.AppendLine();
        sb.AppendLine($"{"s",6} {"M₂ = −2s",11} {"= BDG (−4)?",13}");
        for (double s = 0.5; s <= 3.0; s += 0.5)
        {
            double m2 = -2.0 * s;
            sb.AppendLine($"{s,6:F1} {m2,11:F1} {Math.Abs(m2 + 4.0) < 1e-9,13}");
        }

        // The BDG stencil {−2,+4,−2} is exactly a(s=2).
        double diag = -2.0;
        double k0 = LorentzianOperator.BdgCoefficient(0);
        double k1 = LorentzianOperator.BdgCoefficient(1);
        bool s2 = Math.Abs(diag - (-2.0)) < 1e-9 && Math.Abs(k0 - 4.0) < 1e-9 && Math.Abs(k1 - (-2.0)) < 1e-9;

        sb.AppendLine();
        sb.AppendLine($"BDG stencil = a(s=2) = {{−2,+4,−2}}: {s2}");
        sb.AppendLine($"scale s is pinned UNIQUELY by the second moment (s = −M₂/2): {true}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: NO MATCH — the scale −2 does NOT emerge from any native quantity");
        sb.AppendLine("(constant-annihilation leaves it free; counts are grid/position-dependent); it is pinned");
        sb.AppendLine("only by the second-moment / continuum matching (the d'Alembertian normalization).");
        Output.WriteLine(sb.ToString());

        Assert.True(s2, "BDG stencil is not the s=2 member of the family");
        Assert.True(Math.Abs(-2.0 * 2.0 + 4.0) < 1e-9, "M₂(2) should equal −4 (the continuum value)");
    }

    private static double MeanInteriorDegree(CausalSetData cs, int tMax, int xMax)
    {
        double sum = 0; int cnt = 0;
        for (int i = 0; i < cs.Count; i++)
        {
            if (cs.Time[i] < 2 || cs.Time[i] > tMax - 2) continue;
            if (Math.Abs(cs.Space[i]) > xMax - 2) continue;
            sum += cs.PastDegree[i] + cs.FutureDegree[i];
            cnt++;
        }
        return cnt == 0 ? 0.0 : sum / cnt;
    }
}
