using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 104 — Compute network spectrum. For a concrete causal network (deterministic 1+1D causal-set
/// grid) this phase computes the eigenvalues of the native network operators (adjacency A, graph Laplacian
/// L = D − A, actualization operator Lc = ρ⁻¹Lρ⁻¹ with ρ = causal counting density), extracts stable-mode
/// frequencies and spectral ratios, and compares them against the known SM mass hierarchies.
/// Classify: NO MATCH / PARTIAL MATCH / NUMERICAL CORRESPONDENCE.
///
/// Tests: TQMQG1040 (adjacency + Laplacian spectra), TQMQG1041 (actualization operator + stable modes +
/// spectral ratios), TQMQG1042 (SM comparison + classification).
/// </summary>
public class TQMQG_Phase104_NetworkSpectrumTests : ResearchTestBase
{
    public TQMQG_Phase104_NetworkSpectrumTests(ITestOutputHelper o) : base(o) { }

    private static CausalSetData Cs() => NetworkSpectrum.BuildConcreteCausalNetwork(6, 6);

    // ── TQMQG1040: adjacency spectrum + graph Laplacian spectrum ──────────────────

    [Fact]
    public void TQMQG1040_AdjacencyAndLaplacianSpectra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1040: adjacency spectrum + graph Laplacian spectrum of the concrete causal network");

        var cs = Cs();
        double[] adj = NetworkSpectrum.AdjacencySpectrum(cs);
        double[] lap = NetworkSpectrum.LaplacianSpectrum(cs);

        int n = cs.Count;
        bool adjSymmetric = Math.Abs(adj[0] + adj[^1]) < 1e-9;          // bipartite Hasse graph: spectrum symmetric about 0
        double zeroModes = lap.Count(x => Math.Abs(x) < 1e-8);           // # connected components
        double gap = SpectralCurvature.SpectralGap(lap);                  // first positive Laplacian eigenvalue
        bool allLapNonNeg = lap.All(x => x >= -1e-8);
        double adjRadius = adj.Max(x => Math.Abs(x));
        int[] deg = new int[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (cs.Link[i, j] || cs.Link[j, i]) deg[i]++;
        double maxDegree = deg.Max();

        sb.AppendLine($"causal network      : 1+1D Minkowski grid t∈[0,6], x∈[−6,6]  N = {n} events");
        sb.AppendLine($"Hasse-link edges    : {(adj.Count(x => Math.Abs(x) > 1e-12)) / 2.0:F0} undirected links");
        sb.AppendLine($"max degree          : {maxDegree}");
        sb.AppendLine();
        sb.AppendLine($"ADJACENCY spectrum  : λ_min = {adj[0]:F4}  λ_max = {adj[^1]:F4}");
        sb.AppendLine($"  spectral radius   : {adjRadius:F4}  (≤ max degree {maxDegree}: {adjRadius <= maxDegree + 1e-9})");
        sb.AppendLine($"  bipartite symmetry: λ_min = −λ_max: {adjSymmetric}");
        sb.AppendLine();
        sb.AppendLine($"LAPLACIAN spectrum  : λ_1 = {lap[0]:E3} (zero mode)  λ_2 = {gap:F4} (spectral gap)  λ_max = {lap[^1]:F4}");
        sb.AppendLine($"  # zero modes      : {zeroModes:F0}  (connected network ⇒ 1)");
        sb.AppendLine($"  all λ ≥ 0         : {allLapNonNeg}  (PSD Laplacian)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the concrete causal network POSSESSES a real adjacency spectrum (bipartite-symmetric,");
        sb.AppendLine("spectral radius ≤ max degree) and a positive-semidefinite graph-Laplacian spectrum with a single");
        sb.AppendLine($"zero mode (connected) and a nonzero spectral gap λ_2 = {gap:F4}. The native network operators have");
        sb.AppendLine("genuine, computable spectra.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(n, adj.Length);
        Assert.Equal(n, lap.Length);
        Assert.True(allLapNonNeg, "Laplacian must be PSD");
        Assert.Equal(1.0, zeroModes, 6);                       // connected ⇒ exactly one zero mode
        Assert.True(gap > 0.0, "connected network has a spectral gap");
        Assert.True(adjRadius <= maxDegree + 1e-9, "spectral radius ≤ max degree");
        Assert.True(adjSymmetric, "bipartite Hasse adjacency spectrum symmetric about 0");
    }

    // ── TQMQG1041: actualization operator + stable modes + spectral ratios ─────────

    [Fact]
    public void TQMQG1041_ActualizationOperatorModesAndRatios()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1041: actualization operator spectrum, stable-mode frequencies, spectral ratios");

        var cs = Cs();
        double[] lc = NetworkSpectrum.ActualizationSpectrum(cs);
        double[] freqs = NetworkSpectrum.StableModeFrequencies(cs);
        double[] ratios = NetworkSpectrum.SuccessiveSpectralRatios(freqs);
        double span = NetworkSpectrum.SpectralHierarchySpan(freqs);

        double lcMin = lc.Min();
        bool lcSemiDef = lcMin > -1e-8;
        int lcZero = lc.Count(x => Math.Abs(x) < 1e-8);

        sb.AppendLine($"ACTUALIZATION OP (ρ⁻¹Lρ⁻¹): λ_min = {lcMin:E3}  λ_max = {lc[^1]:F4}  PSD: {lcSemiDef}");
        sb.AppendLine($"  zero modes            : {lcZero}  (same connected component count as L)");
        sb.AppendLine();
        sb.AppendLine($"STABLE MODE FREQUENCIES ω=√λ : {freqs.Length} positive modes");
        sb.AppendLine($"  ω_1 = {freqs[0]:F4}  ...  ω_max = {freqs[^1]:F4}");
        sb.AppendLine($"  monotone increasing   : {(freqs.SequenceEqual(freqs.OrderBy(x => x)))}");
        sb.AppendLine();
        sb.AppendLine("SPECTRAL RATIOS ω_k+1/ω_k (first 8):");
        for (int i = 0; i < Math.Min(8, ratios.Length); i++)
            sb.AppendLine($"  r[{i}] = ω_{i + 2}/ω_{i + 1} = {ratios[i]:F4}");
        sb.AppendLine($"  spectral-hierarchy span ω_max/ω_min = {span:F2}");
        sb.AppendLine($"  hierarchy present (span > 10): {NetworkSpectrum.IsHierarchical(freqs)}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the actualization operator has a real (PSD) spectrum with the same connectivity");
        sb.AppendLine("structure as L; the network possesses STABLE normal-mode eigenfrequencies ω = √λ (all real,");
        sb.AppendLine("positive, monotone); and the successive spectral ratios form a DISCRETE, hierarchical");
        sb.AppendLine($"fingerprint with span {span:F2} — a genuine native spectrum.");
        Output.WriteLine(sb.ToString());

        Assert.True(lcSemiDef, "actualization operator PSD");
        Assert.True(lcZero >= 1, "actualization operator retains zero modes");
        Assert.True(freqs.Length > 10, "enough stable modes");
        Assert.True(freqs.SequenceEqual(freqs.OrderBy(x => x)), "frequencies monotone");
        Assert.True(NetworkSpectrum.IsHierarchical(freqs), "spectrum is hierarchical (span > 10)");
        Assert.True(ratios.Length == freqs.Length - 1, "ratios count");
    }

    // ── TQMQG1042: SM hierarchies vs network spectra — classification ──────────────

    [Fact]
    public void TQMQG1042_SMHierarchyComparisonAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1042: SM mass hierarchies vs network spectra → NO MATCH / PARTIAL MATCH / NUMERICAL CORRESPONDENCE");

        var cs = Cs();
        double[] freqs = NetworkSpectrum.StableModeFrequencies(cs);
        double[] netRatios = NetworkSpectrum.SuccessiveSpectralRatios(freqs);

        double[] lep = NetworkSpectrum.LeptonMassRatios();
        double[] qua = NetworkSpectrum.QuarkSuccessiveMassRatios();

        var (lepRel, lepNet, lepSm) = NetworkSpectrum.BestRatioMatch(netRatios, lep);
        var (quaRel, quaNet, quaSm) = NetworkSpectrum.BestRatioMatch(netRatios, qua);

        bool lepNum = NetworkSpectrum.AnyNumericalCorrespondence(netRatios, lep);
        bool quaNum = NetworkSpectrum.AnyNumericalCorrespondence(netRatios, qua);
        string cls = NetworkSpectrum.Classify(cs);

        sb.AppendLine("SM MASS HIERARCHIES (scale-free ratios):");
        sb.AppendLine($"  charged leptons m_e/m_μ={lep[0]:F6}, m_μ/m_τ={lep[1]:F6}, m_e/m_τ={lep[2]:E4}");
        sb.AppendLine($"  quarks (successive) {qua[0]:F3}, {qua[1]:F3}, {qua[2]:F3}, {qua[3]:F3}, {qua[4]:F3}");
        sb.AppendLine($"  Koide Q (leptons) = {NetworkSpectrum.KoideQ():F6}  (known hidden structure ≈ 2/3)");
        sb.AppendLine();
        sb.AppendLine("NETWORK SPECTRA (computed):");
        sb.AppendLine($"  stable-mode frequencies : {freqs.Length} modes, span ω_max/ω_min = {NetworkSpectrum.SpectralHierarchySpan(freqs):F2}");
        sb.AppendLine($"  best match vs leptons   : net ratio {lepNet:F4} ↔ SM {lepSm:F6}  rel.err {lepRel:E3}");
        sb.AppendLine($"  best match vs quarks    : net ratio {quaNet:F4} ↔ SM {quaSm:F4}  rel.err {quaRel:E3}");
        sb.AppendLine($"  numerical correspondence (< 1%): leptons {lepNum}, quarks {quaNum}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO MATCH: the network genuinely possesses discrete hierarchical spectra (span > 10,");
        sb.AppendLine("    stable modes, spectral gaps) — a structural analogy to the SM hierarchy exists.");
        sb.AppendLine("  • NOT NUMERICAL CORRESPONDENCE: no specific network spectral ratio equals a SM mass ratio");
        sb.AppendLine("    (m_e/m_μ ≈ 4.8e-3, m_μ/m_τ ≈ 5.9e-2) within 1% — the concrete un-tuned network does not");
        sb.AppendLine("    reproduce the SM numbers.");
        sb.AppendLine("  • PARTIAL MATCH: hierarchical discrete spectrum + quantization (structural analogy), without");
        sb.AppendLine("    numerical value determination (consistent with QG94/95: spectra exist, mapping speculative).");
        Output.WriteLine(sb.ToString());

        Assert.True(NetworkSpectrum.IsHierarchical(freqs), "network spectrum is hierarchical");
        Assert.False(lepNum, "no numerical correspondence with lepton ratios");
        Assert.False(quaNum, "no numerical correspondence with quark ratios");
        Assert.Equal("PARTIAL MATCH", cls);
        Assert.True(Math.Abs(NetworkSpectrum.KoideQ() - 2.0 / 3.0) < 1e-4, "Koide Q ≈ 2/3 sanity check");
    }
}
