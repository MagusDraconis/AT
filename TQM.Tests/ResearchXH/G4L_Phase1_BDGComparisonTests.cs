using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 1 — rank native Lorentzian operators against the BDG d'Alembertian (d=2).
/// Measures spectrum, eigenmodes, layer/interval response, and propagation; compares to a
/// symmetric BDG reference (−2·I + 4·link − 2·next-layer) and a retarded (past-only) BDG.
///
/// Tests: G4-L10 (spectrum + eigenmodes), G4-L11 (layer/interval response + alternation),
///        G4-L12 (propagation + ranking).
/// </summary>
public class G4L_Phase1_BDGComparisonTests : ResearchTestBase
{
    public G4L_Phase1_BDGComparisonTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;   // 9

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);

    private static readonly (string Name, Func<CausalSetData, double[,]> Build)[] Operators =
    {
        ("L1 causal-link",      LorentzianOperator.LinkOperator),
        ("L2 interval",         LorentzianOperator.IntervalOperator),
        ("L3 layer",            LorentzianOperator.LayerOperator),
        ("L4 density-weighted", LorentzianOperator.DensityWeightedCausal),
    };

    private static int CenterIndex => (TMax / 2) * Nx + XMax; // t = 3, x = 0

    // ── G4-L10: spectrum + eigenmodes vs symmetric BDG ──────────────────────────────────

    [Fact]
    public void G4_L10_SpectrumAndEigenmodesComparison()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L10: spectrum and eigenmodes vs the symmetric BDG reference");

        var cs = Cs;
        double[] bdgEvals = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));
        var bdgSig = LorentzianOperator.Signature(bdgEvals);

        sb.AppendLine($"Symmetric BDG reference (d=2): −2·I + 4·link − 2·next-layer.");
        sb.AppendLine($"  signature (n+, n−, n0) = ({bdgSig.pos}, {bdgSig.neg}, {bdgSig.zero}); min λ = {bdgEvals[0]:F3}, max λ = {bdgEvals[^1]:F3}.");
        sb.AppendLine();
        sb.AppendLine($"{"operator",-20} {"signature",14} {"min λ",9} {"max λ",9} {"KS to BDG",10}");
        foreach (var (name, build) in Operators)
        {
            var evals = LorentzianOperator.Eigenvalues(build(cs));
            var s = LorentzianOperator.Signature(evals);
            double ks = SpectralCurvature.KolmogorovSmirnov(evals, bdgEvals);
            sb.AppendLine($"{name,-20} {"(" + s.pos + "," + s.neg + "," + s.zero + ")",14} {evals[0],9:F3} {evals[^1],9:F3} {ks,10:F4}");
        }

        sb.AppendLine();
        sb.AppendLine("KS distance is a global spectral shape measure (scale-sensitive); the STRUCTURAL");
        sb.AppendLine("ranking is settled in G4-L11 via the layer response.");
        Output.WriteLine(sb.ToString());

        Assert.True(bdgSig.pos > 0 && bdgSig.neg > 0, "symmetric BDG reference is not indefinite");
        foreach (var (name, build) in Operators)
        {
            var s = LorentzianOperator.Signature(LorentzianOperator.Eigenvalues(build(cs)));
            Assert.True(s.pos > 0 && s.neg > 0, $"{name} is not indefinite");
        }
    }

    // ── G4-L11: layer/interval response + alternation ───────────────────────────────────

    [Fact]
    public void G4_L11_LayerResponseAndAlternation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L11: layer/interval response and sign alternation");

        var cs = Cs;
        double[] bdgProfile = LorentzianOperator.LayerProfile(cs, LorentzianOperator.BdgReference(cs));
        bool bdgAlternates = LorentzianOperator.Alternates(bdgProfile);

        sb.AppendLine($"BDG layer profile (mean entry per interval k = 0..3):");
        sb.AppendLine($"  k=0 (links) {bdgProfile[0],6:F2}   k=1 {bdgProfile[1],6:F2}   k=2 {bdgProfile[2],6:F2}   k=3 {bdgProfile[3],6:F2}");
        sb.AppendLine($"  alternates (sign flips between k=0 and k=1): {bdgAlternates}");
        sb.AppendLine();

        sb.AppendLine($"{"operator",-20} {"k=0",7} {"k=1",7} {"k=2",7} {"k=3",7} {"alternates",11}");
        foreach (var (name, build) in Operators)
        {
            var p = LorentzianOperator.LayerProfile(cs, build(cs));
            bool alt = LorentzianOperator.Alternates(p);
            sb.AppendLine($"{name,-20} {p[0],7:F2} {p[1],7:F2} {p[2],7:F2} {p[3],7:F2} {alt,11}");
        }

        sb.AppendLine();
        sb.AppendLine("BDG's defining feature is the ALTERNATING layer sign (links vs next layer opposite).");
        sb.AppendLine("Only L3 (layer operator) shares this; L1/L4 are links-only, L2 is interval-monotonic.");
        Output.WriteLine(sb.ToString());

        // BDG alternates; L3 alternates (same structure); L2 does not (monotonic).
        var l3p = LorentzianOperator.LayerProfile(cs, LorentzianOperator.LayerOperator(cs));
        var l2p = LorentzianOperator.LayerProfile(cs, LorentzianOperator.IntervalOperator(cs));
        Assert.True(bdgAlternates, "BDG reference does not alternate");
        Assert.True(LorentzianOperator.Alternates(l3p), "L3 does not alternate like BDG");
        Assert.False(LorentzianOperator.Alternates(l2p), "L2 should NOT alternate (monotonic)");
    }

    // ── G4-L12: propagation + ranking ───────────────────────────────────────────────────

    [Fact]
    public void G4_L12_PropagationAndRanking()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L12: propagation behavior and BDG-similarity ranking");

        var cs = Cs;
        int c = CenterIndex;
        int tc = cs.Time[c];

        sb.AppendLine($"Apply each operator to a δ-source at (t={tc}, x=0), index {c}.");
        sb.AppendLine($"{"operator",-20} {"past-response",14} {"future-response",15} {"direction",12}");
        double PastResponse(double[,] m)
        {
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++)
                if (cs.Time[j] < tc) s += Math.Abs(m[c, j]);
            return s;
        }
        double FutureResponse(double[,] m)
        {
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++)
                if (cs.Time[j] > tc) s += Math.Abs(m[c, j]);
            return s;
        }

        double bdgPast = PastResponse(LorentzianOperator.RetardedBdg(cs));
        double bdgFuture = FutureResponse(LorentzianOperator.RetardedBdg(cs));
        sb.AppendLine($"{"BDG (retarded)",-20} {bdgPast,14:F2} {bdgFuture,15:F2} {"forward-only",12}");

        var classes = new List<(string Name, string Class)>();
        foreach (var (name, build) in Operators)
        {
            var m = build(cs);
            double past = PastResponse(m), future = FutureResponse(m);
            string dir = past > 1e-9 && future > 1e-9 ? "symmetric" : "?";
            sb.AppendLine($"{name,-20} {past,14:F2} {future,15:F2} {dir,12}");

            var p = LorentzianOperator.LayerProfile(cs, m);
            string cls = Classify(name, p);
            classes.Add((name, cls));
        }

        sb.AppendLine();
        sb.AppendLine("Ranking by BDG-similarity (alternation = defining Lorentzian structure):");
        foreach (var (name, cls) in classes)
            sb.AppendLine($"  {name,-20} → {cls}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: L3 (layer operator) is the closest to BDG — the only candidate with the");
        sb.AppendLine("alternating layer structure. L1/L4 are links-only (truncated first layer); L2 is rejected.");
        sb.AppendLine("Note: all candidates are time-SYMMETRIC (Feynman-like), whereas BDG is retarded — the");
        sb.AppendLine("remaining causality gap.");
        Output.WriteLine(sb.ToString());

        // Retarded BDG propagates forward-only; symmetric candidates propagate both ways.
        Assert.True(bdgPast < 1e-9 && bdgFuture > 1e-9, "retarded BDG must be forward-only");
        foreach (var (name, build) in Operators)
        {
            var m = build(cs);
            Assert.True(PastResponse(m) > 1e-9 && FutureResponse(m) > 1e-9, $"{name} is not time-symmetric");
        }

        // L3 is the best match (only alternator); L2 is rejected (monotonic).
        Assert.True(classes.Any(x => x.Name == "L3 layer" && x.Class == "BEST MATCH"), "L3 not ranked best");
        Assert.True(classes.Any(x => x.Name == "L2 interval" && x.Class == "REJECT"), "L2 not rejected");
    }

    private static string Classify(string name, double[] profile)
    {
        bool alt = LorentzianOperator.Alternates(profile);
        bool link = Math.Abs(profile[0]) > 1e-9;
        if (alt) return "BEST MATCH";
        if (!link) return "REJECT";                       // no link component, monotonic interval
        return name.Contains("density") ? "WEAK" : "PROMISING";
    }
}
