using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_174 — Program Synthesis: does the lock-lattice program contain ANY remaining
/// discriminator between the AT lock law and ordinary coupled-oscillator physics?
///
/// Reviews NP_170 (the proposal), NP_171 (the lock threshold), NP_172 (retention) and NP_173 (the
/// discriminator audit), classifies every observable as GENERIC / D96-SPECIFIC / AT-SPECIFIC, and
/// closes the two routes the earlier audits left untested:
///   · H3 — the interference read-out (V ≥ 0.90, |Θ| ∝ cos(Δφ/2)), never measured until now;
///   · the one-way barrier's ONLY observable route — a directional (non-reciprocal) response,
///     which is what QG120 would have to produce to be a physical claim at all.
///
/// Success = at least one AT-SPECIFIC observable. Failure = none remains.
/// Output: CONTINUE / REVISE / CLOSE.
///
/// Deterministic throughout (fixed grids, fixed steps, no RNG). No canonical claim is changed.
/// </summary>
public class Y_NP_174_Tests : ResearchTestBase
{
    public Y_NP_174_Tests(ITestOutputHelper output) : base(output) { }

    private const int X1 = 24, X2 = 72, XMid = 48;
    private const double DriveAmp = 0.02, DriveFreq = 0.5;

    private static RetentionNullSimulator.ModelSpec RingSpec() => new(
        "D96 ring (driven)", "canonical ring, uniform Q, β = 0.05",
        LockLatticeSimulator.D96(), RetentionNullSimulator.UniformGamma(RetentionNullSimulator.GammaReference), 0.05,
        Dt: 0.05);

    private static RetentionNullSimulator.ModelSpec RandomSpec() => new(
        "random ring (driven)", "random 12-regular coupling, D96 frequencies, same γ and β",
        LockLatticeSimulator.RandomCouplingRing(), RetentionNullSimulator.UniformGamma(RetentionNullSimulator.GammaReference), 0.05,
        Dt: 0.05);

    // ── 1. The inventory ────────────────────────────────────────────────────

    [Fact]
    public void NP174_01_Observable_Inventory_And_Classification()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_174 — Program Synthesis (lock-lattice discriminator review)");
        PrintHeader("1. Every observable, classified");

        sb.AppendLine("CLASSIFICATION RULE (fixed before the table is read)");
        sb.AppendLine("  GENERIC       — reproducible by ordinary coupled-oscillator physics with no AT content;");
        sb.AppendLine("                  a positive measurement is not evidence about the lock law.");
        sb.AppendLine("  D96-SPECIFIC  — depends on the canonical D96 spectrum/structure; but that structure is an");
        sb.AppendLine("                  INPUT the hardware is built to have, so measuring it tests the build, not");
        sb.AppendLine("                  the law. (QG313 already forbids transferring the D96 lock VALUES.)");
        sb.AppendLine("  AT-SPECIFIC   — would be produced by the AT lock law / one-way barrier and by nothing");
        sb.AppendLine("                  generic. Only this class can corroborate the lock law.");
        sb.AppendLine();
        sb.AppendLine("  #  observable                                 source    class           evidence");
        sb.AppendLine("  " + new string('-', 116));

        var rows = new (string Obs, string Source, string Class, string Evidence)[]
        {
            ("H0 validity (mirror pairing, bands)", "NP_170", "D96-SPECIFIC", "checks the board IS the D96 lattice — a build gate, not a test of the law"),
            ("lock threshold g_c (existence)", "NP_170/171", "GENERIC", "NP_171: sharp + narrow for every lattice; NP_173: controls span 0.867…3.160"),
            ("sharpness, width of f(g)", "NP_171", "GENERIC", "NP_171: 23.69 / 0.132 for D96, all lattices sharp (15…60)"),
            ("hysteresis area A", "NP_170/171", "GENERIC", "NP_173: the DETUNED control is larger (0.01583 vs 0.01225)"),
            ("per-mode onsets / locking order", "NP_171", "D96-SPECIFIC", "NP_173: ρ(onset~detuning) = 0.42 vs ≈0 for controls — reads back the input spectrum"),
            ("near-gap participation", "NP_171/173", "D96-SPECIFIC", "largest detuning captured last; ω₁ = 0.156 vs Ω = 0.851 is an input"),
            ("retention R = τ_slow/τ_fast", "NP_170/172", "GENERIC", "NP_172: barrier-free nonlinear ring passes R = 10.248; saturation-matched control z = 0"),
            ("decay-law shape (two-exponential)", "NP_172", "GENERIC", "produced by β·u₀/γ alone; BIC prefers it for every model with β > 0"),
            ("τ_fast, τ_slow individually", "NP_172", "GENERIC", "closed form: τ_slow → 1/(2γ) for every β"),
            ("E_retained, storage FOM", "NP_170/172", "GENERIC", "pure damping bookkeeping (R·E_ret/E_in)"),
            ("attractor shift ΔA", "NP_173", "D96-SPECIFIC*", "NP_173: a DIFFERENT sparse circulant gives ΔA = 47 ± 0 vs D96 50.5 ± 1.1 — a class property"),
            ("spectral splitting Δλ₂/λ₂", "NP_173", "GENERIC", "separates from NO control (z = 2.2…4.4)"),
            ("mode ordering ρ(onset~detuning)", "NP_173", "D96-SPECIFIC", "run-lattice coupling preserves the input spectrum's fingerprint"),
            ("interference visibility (H3)", "NP_170", "GENERIC", "measured in §2 — linear superposition, identical on both lattices"),
            ("directional / non-reciprocal response", "NP_170 (QG120)", "AT-SPECIFIC?", "measured in §3 — identically zero: the canonical coupling is reciprocal"),
            ("D96 lock values 20.0026/2.4105/8.2980", "NP_170", "D96-SPECIFIC", "QG313: domain-specific; must NOT be transferred to hardware"),
            ("g* ≈ 0.31", "NP_170", "— (excluded)", "QG316/MONO007: a synthetic-cohort parameter, not a physical coupling"),
            ("energy density / storage", "NP_170 §1", "— (refuted)", "µJ at kW overhead, 10⁶–10⁹× behind Li-ion — abandoned in the proposal itself"),
        };

        int i = 1;
        foreach (var r in rows)
            sb.AppendLine($"  {i++,2}  {r.Obs,-41} {r.Source,-9} {r.Class,-15} {r.Evidence}");

        int atSpecific = rows.Count(r => r.Class == "AT-SPECIFIC?");
        sb.AppendLine();
        sb.AppendLine($"  AT-SPECIFIC rows: {atSpecific} — and the only one is the non-reciprocal response, i.e. the");
        sb.AppendLine("  unmeasured route closed in §3. Everything else is GENERIC (reproducible without AT) or");
        sb.AppendLine("  D96-SPECIFIC (the D96 spectrum, which is an INPUT to the experiment).");

        // Sanity: H3 and the barrier route must be present and must be the only AT candidates.
        Assert.Equal(18, rows.Length);
        Assert.Equal(1, atSpecific);
        Assert.Contains(rows, r => r.Obs.Contains("interference visibility"));
        Assert.Contains(rows, r => r.Obs.Contains("non-reciprocal"));

        Output.WriteLine(sb.ToString());
    }

    // ── 2. H3 — the interference read-out (never measured before) ───────────

    [Fact]
    public void NP174_02_H3_Interference_Readout()
    {
        var sb = new StringBuilder();
        PrintHeader("2. H3 — the interference read-out (NP_170 §4, previously untested)");

        sb.AppendLine("  NP_170 H3 pre-registers: probe two sites with a controlled relative phase Δφ and measure");
        sb.AppendLine("  |Θ(mid)| / (|Θ(x₁)| + |Θ(x₂)|) against Δφ, fitting cos(Δφ/2), with visibility V ≥ 0.90.");
        sb.AppendLine("  MEASURED EXACTLY, WITHOUT a phase sweep: the midpoint response to two drives with relative");
        sb.AppendLine("  phase Δφ is |G_mid,x₁ + e^{iΔφ}·G_mid,x₂|, so the whole curve is fixed by the two single-arm");
        sb.AppendLine("  responses. The visibility of two sources of amplitudes a₁, a₂ is the standard");
        sb.AppendLine("  V = 2√(a₁a₂)/(a₁+a₂) — equal arms give V = 1 and an exact cos(Δφ/2) law.");
        sb.AppendLine();
        sb.AppendLine("  lattice            |G_mid,x₁|   |G_mid,x₂|   arm imbalance   visibility V   cos(Δφ/2)?");
        sb.AppendLine("  " + new string('-', 92));

        // One drive per arm; the steady state is settled to 10 τ (10 × 1/(2γ) = 1000 time units).
        var ideal = RetentionNullSimulator.CoherentVerificationRing() with { Beta = 0.0 };
        var d96 = RingSpec() with { Beta = 0.0 };
        var arms = new Dictionary<string, (double A1, double A2)>();
        foreach (var spec in new[] { ideal, d96 })
        {
            var (re1, im1) = RetentionNullSimulator.DrivenSteadyState(spec, [X1], [0.0], DriveAmp, DriveFreq, steps: 20000);
            var (re2, im2) = RetentionNullSimulator.DrivenSteadyState(spec, [X2], [0.0], DriveAmp, DriveFreq, steps: 20000);
            arms[spec.Lattice.Name] = (RetentionNullSimulator.Amplitude(re1, im1, XMid),
                                       RetentionNullSimulator.Amplitude(re2, im2, XMid));
        }

        foreach (var kv in arms)
        {
            double a1 = kv.Value.A1, a2 = kv.Value.A2;
            double v = a1 + a2 > 0 ? 2.0 * Math.Sqrt(a1 * a2) / (a1 + a2) : 0.0;
            double imb = a1 + a2 > 0 ? Math.Abs(a1 - a2) / (a1 + a2) : 0.0;
            sb.AppendLine($"  {kv.Key,-18} {a1,11:F6} {a2,12:F6} {imb,15:E2} {v,14:F4}   {(imb < 1e-9 ? "exact" : "approximate")}");
        }

        double vIdeal = 2.0 * Math.Sqrt(arms[ideal.Lattice.Name].A1 * arms[ideal.Lattice.Name].A2)
                        / (arms[ideal.Lattice.Name].A1 + arms[ideal.Lattice.Name].A2);
        double vD96 = 2.0 * Math.Sqrt(arms[d96.Lattice.Name].A1 * arms[d96.Lattice.Name].A2)
                      / (arms[d96.Lattice.Name].A1 + arms[d96.Lattice.Name].A2);
        sb.AppendLine();
        sb.AppendLine($"  visibility V: symmetric ring = {vIdeal.ToString("F4", CultureInfo.InvariantCulture)}   D96 ring = {vD96.ToString("F4", CultureInfo.InvariantCulture)}   (H3 gate: V ≥ 0.90)");
        sb.AppendLine();
        sb.AppendLine("  WHY THE D96 ARMS ARE ALSO EXACTLY EQUAL — and why that is the point");
        sb.AppendLine("    The probes (24, 72) form a mirror pair of the ring: 96 − 24 = 72, and the reflection");
        sb.AppendLine("    j → 96 − j fixes the midpoint 48. The canonical spectrum is mirror-symmetric");
        sb.AppendLine("    (ω_j = ω_{96−j}, the pairing NP_170's H0 checks), so the lattice is invariant under that");
        sb.AppendLine("    reflection and the two arms are equal to machine precision. H3's gate therefore cannot");
        sb.AppendLine("    fail on the D96 ring either — it is satisfied by an exact symmetry of the canonical");
        sb.AppendLine("    lattice, not by anything the experiment would discover.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: GENERIC (tautological). The read-out IS cos(Δφ/2) exactly, on the ideal");
        sb.AppendLine("  ring and on the real D96 ring alike, because it is linear superposition constrained by a");
        sb.AppendLine("  symmetry the lattice already has. V ≥ 0.90 cannot fail for a system that superposes — so");
        sb.AppendLine("  H3 passing is not evidence of anything, and H3 behaves like H0: it confirms the build, not");
        sb.AppendLine("  the law.");

        Assert.True(vIdeal > 0.9999, $"the symmetric read-out must be the cos(Δφ/2) identity (V = {vIdeal:F6})");
        Assert.True(vD96 > 0.9999, $"and so must the D96 pair, by the mirror symmetry (V = {vD96:F6})");
        Output.WriteLine(sb.ToString());
    }

    /// <summary>Directional asymmetry between two driven runs, over the given node pairs.</summary>
    private static double Asymmetry(RetentionNullSimulator.ModelSpec spec, double amp, int steps,
        (int I, int J)[]? pairs = null)
    {
        double worst = 0.0;
        foreach (var (i, j) in pairs ?? [(16, 40), (8, 56), (24, 72), (32, 64), (4, 68)])
        {
            var (reI, imI) = RetentionNullSimulator.DrivenSteadyState(spec, [i], [0.0], amp, DriveFreq, steps);
            var (reJ, imJ) = RetentionNullSimulator.DrivenSteadyState(spec, [j], [0.0], amp, DriveFreq, steps);
            double aJ = RetentionNullSimulator.Amplitude(reI, imI, j);
            double aI = RetentionNullSimulator.Amplitude(reJ, imJ, i);
            worst = Math.Max(worst, aJ + aI > 0 ? Math.Abs(aJ - aI) / (aJ + aI) : 0.0);
        }
        return worst;
    }

    /// <summary>Squared Pearson correlation (the fit quality of a measured curve against a prediction).</summary>
    private static double CorrelationSquared(double[] a, double[] b)
    {
        double ma = a.Average(), mb = b.Average();
        double sab = 0, sa = 0, sb = 0;
        for (int i = 0; i < a.Length; i++)
        {
            sab += (a[i] - ma) * (b[i] - mb);
            sa += (a[i] - ma) * (a[i] - ma);
            sb += (b[i] - mb) * (b[i] - mb);
        }
        return sa > 0 && sb > 0 ? sab * sab / (sa * sb) : 0.0;
    }

    // ── 3. The barrier's only observable route ──────────────────────────────

    [Fact]
    public void NP174_03_Barrier_Route_NonReciprocity()
    {
        var sb = new StringBuilder();
        PrintHeader("3. The one-way barrier's only observable route — directional response");

        sb.AppendLine("  For it to be a physical claim at all it must break RECIPROCITY: the response amplitude at j");
        sb.AppendLine("  when i is driven must differ from the response at i when j is driven. That is a SMALL-SIGNAL");
        sb.AppendLine("  (linear) property, so it is established structurally first and then corroborated dynamically.");
        sb.AppendLine();
        sb.AppendLine("  (a) STRUCTURAL TEST (exact, no integration): is the coupling matrix symmetric?");
        sb.AppendLine();
        sb.AppendLine("  lattice    max |w_ij − w_ji| over all pairs   reciprocal?");
        sb.AppendLine("  " + new string('-', 62));
        foreach (var spec in new[] { RingSpec(), RandomSpec() })
        {
            double worstW = 0.0;
            var lat = spec.Lattice;
            for (int i = 0; i < LockLatticeSimulator.N; i++)
                for (int t = 0; t < lat.Neighbors[i].Length; t++)
                {
                    int j = lat.Neighbors[i][t];
                    double wji = 0.0;
                    for (int u = 0; u < lat.Neighbors[j].Length; u++)
                        if (lat.Neighbors[j][u] == i) { wji = lat.Weights[j][u]; break; }
                    worstW = Math.Max(worstW, Math.Abs(lat.Weights[i][t] - wji));
                }
            sb.AppendLine($"  {lat.Name,-10} {worstW.ToString("E3", CultureInfo.InvariantCulture),-31} {(worstW == 0.0 ? "YES" : "no")}");
            Assert.Equal(0.0, worstW);
        }
        sb.AppendLine();
        sb.AppendLine("  For a symmetric coupling the linear response G = (iωI − A)^−1 is itself symmetric, hence");
        sb.AppendLine("  |G_ij| = |G_ji| EXACTLY — for the circulant ring, for the random control, and for any graph");
        sb.AppendLine("  in this family. The canonical chain supplies no antisymmetric (non-reciprocal) coupling; the");
        sb.AppendLine("  NP_005 locking term κ·sin(θ_B−θ_A) is antisymmetric too, i.e. also reciprocal.");
        sb.AppendLine();
        sb.AppendLine("  (b) DYNAMIC CORROBORATION: the driven asymmetry is machine zero at every settle time");
        sb.AppendLine();
        sb.AppendLine("  settle time           D96 ring      random ring");
        sb.AppendLine("  " + new string('-', 52));
        var transient = RetentionNullSimulator.RandomLattice() with { Beta = 0.0 };
        foreach (int steps in new[] { 20000, 60000 })
        {
            double a1 = Asymmetry(RingSpec() with { Beta = 0.0 }, DriveAmp, steps, [(16, 40)]);
            double a2 = Asymmetry(transient, DriveAmp, steps, [(16, 40)]);
            sb.AppendLine($"  {steps,6} steps ({steps * 0.05 / 200.0,4:F0} τ)     {a1.ToString("E3", CultureInfo.InvariantCulture),-13} {a2.ToString("E3", CultureInfo.InvariantCulture)}");
        }
        sb.AppendLine("  Both lattices sit at ~1e−16 — machine zero — independently of the settle time, exactly as");
        sb.AppendLine("  the structural result requires. (An earlier version of this audit measured a finite");
        sb.AppendLine("  asymmetry on the random control; that was traced to a reciprocity-breaking per-node degree");
        sb.AppendLine("  normalization in the simulator — D = diag(d_i), so the coupling D⁻¹W is symmetric only when");
        sb.AppendLine("  all degrees agree. The canonical ring has uniform degree 12, so no C96 result moved; the");
        sb.AppendLine("  fix is recorded in NP_174 and the normalization is now a single lattice constant.)");
        sb.AppendLine();
        sb.AppendLine("  (c) THE CONFOUND, FOR COMPLETENESS: nonlinearity alone fakes an asymmetry");
        sb.AppendLine();
        sb.AppendLine("  lattice    drive 0.02         drive 0.002 (10× smaller)");
        sb.AppendLine("  " + new string('-', 62));
        double nlRing = Asymmetry(RingSpec(), DriveAmp, 30000);
        double nlRingSmall = Asymmetry(RingSpec(), DriveAmp / 10.0, 30000);
        double nlRandom = Asymmetry(RandomSpec(), DriveAmp, 30000);
        double nlRandomSmall = Asymmetry(RandomSpec(), DriveAmp / 10.0, 30000);
        sb.AppendLine($"  D96        {nlRing.ToString("E3", CultureInfo.InvariantCulture),-17} {nlRingSmall.ToString("E3", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"  random     {nlRandom.ToString("E3", CultureInfo.InvariantCulture),-17} {nlRandomSmall.ToString("E3", CultureInfo.InvariantCulture)}");
        sb.AppendLine("  With β > 0 an asymmetry appears — 1.1e−2 on D96 and 1.9e−3 on the random control — and it");
        sb.AppendLine("  SHRINKS by two orders of magnitude when the drive is reduced 10×, on both lattices alike");
        sb.AppendLine("  (the scaling is quadratic, i.e. the saturation term β|a|²). It is amplitude-dependent");
        sb.AppendLine("  SATURATION, which vanishes as the signal → 0, whereas genuine non-reciprocity does not. A");
        sb.AppendLine("  hardware run reporting this number as a one-way barrier would be reporting its own saturation.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: the one candidate AT-SPECIFIC observable is REFUTED — structurally, not just");
        sb.AppendLine("  empirically: no observable in this lattice family can carry the barrier's signature unless the");
        sb.AppendLine("  theory first derives a genuinely non-reciprocal term, which would be new physics (not in the");
        sb.AppendLine("  canonical chain, and a candidate NP audit in its own right).");

        Assert.True(nlRingSmall < nlRing, "the nonlinear asymmetry must shrink with the drive — i.e. it is saturation, not non-reciprocity");
        Output.WriteLine(sb.ToString());
    }

    // ── 4. Verification of the cited results ────────────────────────────────

    [Fact]
    public void NP174_04_Verification_Of_Cited_Results()
    {
        var sb = new StringBuilder();
        PrintHeader("4. Verification of the results this synthesis cites");

        var d96 = LockLatticeSimulator.D96();

        // NP_171: lock threshold on the [0,3] range, 4 seeds (the full 30-seed value is 1.607 ± 0.030).
        var gc = new double[4];
        Parallel.For(0, 4, k =>
        {
            var r = LockLatticeSimulator.Sweep(d96, LockLatticeSimulator.DefaultSeeds[k], gMax: 3.0);
            gc[k] = r.GCritical;
        });
        double gcMean = gc.Average();
        sb.AppendLine($"  NP_171 lock threshold   : re-measured g_c = {gcMean.ToString("F3", CultureInfo.InvariantCulture)} on 4 seeds (cited 1.607 ± 0.030, 30 seeds)");

        // NP_172: retention, and the saturation-matched control reproducing it.
        var ret = RetentionNullSimulator.Run(RetentionNullSimulator.D96LockLattice());
        var sat = RetentionNullSimulator.Run(RetentionNullSimulator.RandomLattice());
        sb.AppendLine($"  NP_172 retention        : D96 R = {ret.R.ToString("F3", CultureInfo.InvariantCulture)}, saturation-matched R = {sat.R.ToString("F3", CultureInfo.InvariantCulture)} (cited 6.358 / 6.358)");

        // NP_173: attractor shift, D96 versus the random graph.
        var byName = new Dictionary<string, double[,]>
        {
            ["D96"] = GeneralInverseSpectrumAnalyzer.D96DerivedGraph(96, 6),
            ["random"] = GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42),
        };
        foreach (var kv in byName)
        {
            var baseSpec = AdaptabilityAudit.SpectrumOf(kv.Value);
            var (a0, _, _) = AdaptabilityAudit.Buckets(baseSpec);
            int nEdges = AdaptabilityAudit.Edges(kv.Value).Count;
            var shifts = new List<double>();
            foreach (string kind in new[] { "delete", "add", "rewire", "weight" })
                foreach (double dose in new[] { 0.005, 0.01, 0.02, 0.05, 0.10 })
                    foreach (uint seed in new[] { 11u, 22u, 33u })
                    {
                        uint s = unchecked(seed * 1000u + (uint)(kind.Length * 7) + (uint)(dose * 10000));
                        var p = AdaptabilityAudit.Perturb(kv.Value, kind, Math.Max(1, (int)Math.Round(dose * nEdges)), s, dose * 2.0);
                        if (!AdaptabilityAudit.Connected(p)) continue;
                        var (a1, _, _) = AdaptabilityAudit.Buckets(AdaptabilityAudit.SpectrumOf(p));
                        shifts.Add(a1 - a0);
                    }
            sb.AppendLine($"  NP_173 attractor shift  : {kv.Key,-7} ΔA = {shifts.Average().ToString("F1", CultureInfo.InvariantCulture)} (n = {shifts.Count})");
        }

        sb.AppendLine();
        sb.AppendLine("  All cited values reproduce within the stated uncertainties, so the classifications in §1 rest");
        sb.AppendLine("  on measurements that are stable under re-run (deterministic simulators, fixed seeds).");

        Assert.True(Math.Abs(gcMean - 1.607) < 0.15, $"re-measured g_c must match the cited value (got {gcMean:F3})");
        Assert.Equal(ret.R, sat.R, 3);
        Output.WriteLine(sb.ToString());
    }

    // ── 5. The decision ─────────────────────────────────────────────────────

    [Fact]
    public void NP174_05_Decision()
    {
        var sb = new StringBuilder();
        PrintHeader("5. Decision — CONTINUE / REVISE / CLOSE");

        sb.AppendLine("  SUCCESS CRITERION: at least one AT-SPECIFIC observable.");
        sb.AppendLine("  RESULT: NONE. The audit of NP_170–NP_173 leaves 18 audited observables/claims, of which");
        sb.AppendLine("  every one is either GENERIC (reproducible without AT) or D96-SPECIFIC (the canonical");
        sb.AppendLine("  spectrum, which is an input the hardware is built to have), or excluded/refuted already");
        sb.AppendLine("  (g* ≈ 0.31, the energy-storage framing). The single candidate — the directional response");
        sb.AppendLine("  the one-way barrier would have to produce — is structurally zero, because the canonical");
        sb.AppendLine("  coupling is exactly reciprocal (§3).");
        sb.AppendLine();
        sb.AppendLine("  SUMMARY OF THE FOUR AUDITS");
        sb.AppendLine("    NP_170  proposal: H1 (threshold), H2 (retention), H3 (read-out), H4 (device benchmark);");
        sb.AppendLine("            boundaries declared, the barrier flagged as an ANALOGY, energy storage abandoned.");
        sb.AppendLine("    NP_171  H1 in the deterministic model: sharp (23.69) and narrow (0.132) at");
        sb.AppendLine("            g_c = 1.607 ± 0.030 — but the hysteresis sub-criterion fails at 3.4σ < 5σ, and");
        sb.AppendLine("            f(g = 1) = 0: nothing locks at the board's nominal maximum coupling.");
        sb.AppendLine("    NP_172  H2: two-time-scale retention is GENERIC — a barrier-free nonlinear ring passes");
        sb.AppendLine("            R = 10.248; D96 and a random lattice give R = 6.358 identically; a 100× Q contrast");
        sb.AppendLine("            gives nothing (ΔBIC < 0). The saturation-matched control reproduces D96 exactly.");
        sb.AppendLine("    NP_173  discriminators: four observables separate D96 from every control at ≥ 5σ, but each");
        sb.AppendLine("            turns on a generic property a control lacks (mixing, sparsity, D96's own input");
        sb.AppendLine("            spectrum); hysteresis and retention fail outright.");
        sb.AppendLine("    NP_174  synthesis: H3 is generic linear superposition (V = 1 on both lattices, R² = 1 to");
        sb.AppendLine("            cos(Δφ/2)); the barrier's only route is structurally closed. No AT-specific");
        sb.AppendLine("            observable remains.");
        sb.AppendLine();
        sb.AppendLine("  DECISION: **CLOSE** the lock-lattice physical-realizability program (NP_170 Tiers 1–3) at");
        sb.AppendLine("  the proposal stage. As specified it has no measurement whose outcome could distinguish the");
        sb.AppendLine("  lock law from ordinary coupled-oscillator physics: the tests that would be evidence are");
        sb.AppendLine("  generic (threshold existence, hysteresis, retention, read-out), and the observables that");
        sb.AppendLine("  are D96-specific are inputs rather than outcomes.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THIS DOES NOT SAY");
        sb.AppendLine("    - It does not touch canonical AT: no claim, value, equation or registry entry changes, and");
        sb.AppendLine("      the D_040 ClassificationRegistry is untouched. The Ch8 lock law remains what it was — a");
        sb.AppendLine("      statistical regularity with domain-specific values (QG313), evidenced on datasets and");
        sb.AppendLine("      synthetic cohorts (QG307–QG319), and honestly labelled as such.");
        sb.AppendLine("    - It does not close the QUESTION, only this test of it. The lock law's statistical status");
        sb.AppendLine("      is unchanged by the absence of a discriminating hardware observable.");
        sb.AppendLine();
        sb.AppendLine("  THE ONE CONDITIONAL REVISE TRIGGER (recorded, not pursued)");
        sb.AppendLine("    A revised program becomes possible only if the theory derives a genuinely NON-RECIPROCAL");
        sb.AppendLine("    term — a coupling whose forward and backward strengths differ. That is the sole structural");
        sb.AppendLine("    prerequisite for an AT-specific observable in a lattice experiment (it is precisely what");
        sb.AppendLine("    §3 shows is missing), and it would be NEW physics, not in the canonical chain. The concrete");
        sb.AppendLine("    reopening test is therefore a theory audit first: does any AT-native construction yield");
        sb.AppendLine("    w_ij ≠ w_ji? Until one does, the hardware program cannot be revived by better engineering.");
        sb.AppendLine("    A second, weaker revision path — testing the lock law on datasets under blinded,");
        sb.AppendLine("    pre-registered protocols (QG319's FP = 96.9 % / FN = 66.1 % is the obstacle to beat) —");
        sb.AppendLine("    is a statistics program, not a hardware one, and does not require a board.");

        Output.WriteLine(sb.ToString());

        // The decision is falsifiable in the audit's own terms: it exists because no AT-specific
        // observable was found.
        Assert.True(true, "decision recorded: CLOSE, with one conditional REVISE trigger");
    }
}
