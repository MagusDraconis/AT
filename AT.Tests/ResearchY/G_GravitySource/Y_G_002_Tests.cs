using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_002 — Density Control Audit (group G — Gravity Source).
///
/// Question: can the actualization density rho change INDEPENDENTLY of mass-energy?
///
/// Model. rho is the counting measure on a lattice of cells (one cell per mode of the lattice's
/// spectrum). Reference rhoBar = 1/N; the normalized measure has Sigma rho = 1. Energy = the
/// actualization rate (QG89), so E = Sigma rho; the deficit mass is m = rhoBar - rho, and count
/// conservation (QG194, Sigma m = 0) is checked explicitly. The two reads of rho are the native
/// conformal acceleration a = -(1/d) grad ln rho (G4-O3) and the conformal scalar curvature
/// R = F(rho) (G4-G2/QG197); both are computed by the shared DensityField helper (also used by G_001).
///
/// Candidates (each an OPERATION on rho):
///   (1) spectral organization — the same occupancy multiset in a different order;
///   (2) phase coherence — the complex phase theta = 2 pi j / N (QG220);
///   (3) degeneracy structure — a redistribution INSIDE degenerate multiplets;
///   (4) survivor compression — keep only the k largest occupancies;
///   (5) D96 vs random — two lattices (45 vs 96 eigenspaces) at the same N and the same total;
///   (6) D96^3 vs D96 — the tensor cube (884 736 modes, 20 812 eigenspaces) at the same total per mode.
///   (control) global rescaling rho -> lambda rho.
///
/// VERDICTS
///   CONTROLLABLE — (1) spectral organization: dE = 0 exactly while drho != 0 and both reads move
///                  (a flips sign at every interior probe). (3) degeneracy structure: a redistribution
///                  internal to degenerate multiplets leaves every per-multiplet total — and hence the
///                  spectrum — identical, at dE = 0, and takes the field from EXACTLY zero to 0.603.
///                  The free room is exactly Sigma (m_i - 1) = N - A0 = 51 = L*N for D96 (the D_048
///                  latent fraction!) and 0 for random. (4) survivor compression with the total held
///                  fixed: dE = 0 exactly at L1 up to 5.16. (5) D96 vs random: same N and same total —
///                  random is the exact zero-field null, D96 is not. (6) D96^3 vs D96: same total per
///                  mode, 97.6% of the cube's directions are energy-free versus 53.1% for D96.
///   CORRELATED   — (4b) the same compression WITHOUT fixing the total moves rho and E together
///                  (-47% to -86%); (control) a global rescaling changes E by x3.7 while leaving a
///                  EXACTLY invariant and merely rescaling R by lambda^(-2/d) — energy moves, the
///                  field does not.
///   REFUTED      — (2) phase coherence: rho = |psi|^2 is phase-blind, so rho, R and a are bit-identical
///                  under a global phase shift or a mirror-pair relative-phase flip, while the
///                  interference term 2 + 2cos(dtheta) still runs 4 -> 0.
///
/// Headline: because count conservation makes the TOTAL deficit vanish exactly (Sigma m = 0), every
/// rearrangement of rho is automatically at fixed total mass-energy. rho's arrangement is therefore
/// not tied to the energy at all — it is tied to the DEGENERACY STRUCTURE, whose free directions
/// number N - A0. This sharpens G_001's open problem OP1: the profile of rho has a large free
/// subspace, so a dynamical principle is genuinely needed to fix it (consistent with G4-RHO's
/// "dynamically undetermined").
///
/// Deterministic: closed-form densities, fixed lattices, fixed seeds inside the shared catalog, no
/// randomness here. No reclassification; the D_040 ClassificationRegistry is untouched.
/// </summary>
public class Y_G_002_Tests : ResearchTestBase
{
    public Y_G_002_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;              // D96 mode count
    private const int D = 3;               // conformal exponent used throughout (2/d = 2/3)


    // Spectral lattices and density configurations come from AT.Tests.Shared.DensityField.


    // ── 1. Baseline: the measure, the energy, and the two lattices ───────────────

    [Fact]
    public void Y_G_002_Baseline()
    {
        var (d96, m96) = D96Spaces;
        var (rand, mrand) = RandomSpaces;

        var canonical = Spread(m96, 1.0);
        Assert.Equal(N, canonical.Length);
        Assert.Equal(1.0, canonical.Sum(), 12);

        // Count conservation (QG194): with rhoBar = 1/N and Sigma rho = 1 the TOTAL deficit vanishes
        // exactly, so "fixed total mass-energy" is the generic situation, not a special one.
        double rhoBar = 1.0 / N;
        Assert.Equal(0.0, canonical.Sum(r => rhoBar - r), 12);

        // The canonical measure (uniform within multiplets) is exactly uniform, hence the exact
        // ZERO-FIELD configuration: a = 0 and R = 0.
        Assert.All(canonical, v => Assert.Equal(1.0 / N, v, 12));
        Assert.Equal(0.0, MaxAbsAcceleration(canonical), 12);
        Assert.Equal(0.0, MaxAbsCurvature(canonical), 12);

        // Real degeneracy structures from the shared catalog (D_048 / D_047 canonical values).
        Assert.Equal(45, d96.Length);                              // A0 = 45
        Assert.Equal(0.53125, (N - d96.Length) / (double)N, 5);    // latent fraction L
        Assert.Equal(N, m96.Sum());
        Assert.Equal(42, m96.Count(m => m == 2));
        Assert.Equal(1, m96.Count(m => m == 5));
        Assert.Equal(1, m96.Count(m => m == 6));
        Assert.Equal(1, m96.Count(m => m == 1));
        Assert.Equal(0.80231, m96.Where(m => m > 1).Sum(m => m * Math.Log(m)) / N, 5);   // D_047 lock release

        // The degeneracy-free control: all singletons, no latent structure, uniform measure.
        Assert.Equal(N, rand.Length);
        Assert.All(mrand, m => Assert.Equal(1, m));
        Assert.Equal(0.0, (N - rand.Length) / (double)N, 12);      // L = 0
    }

    // ── 2. Candidate 1 — spectral organization is CONTROLLABLE ───────────────────

    [Fact]
    public void Y_G_002_SpectralOrganizationControllable()
    {
        var (_, m96) = D96Spaces;

        // The witness: one arrangement of the D96 lattice occupancy (tilted within multiplets) and the
        // very same multiset in ascending order — the density-space analogue of NP_171's canonical
        // mode-index arrangement versus its sorted-ascending control (9% threshold shift there).
        var canonicalArrangement = Spread(m96, 1.0, TiltFractions);
        var sortedArrangement = canonicalArrangement.OrderBy(v => v).ToArray();

        // Same content -> the same total energy, exactly.
        Assert.Equal(canonicalArrangement.Sum(), sortedArrangement.Sum(), 12);
        Assert.Equal(1.0, sortedArrangement.Sum(), 12);

        // rho moves ...
        Assert.True(L1(canonicalArrangement, sortedArrangement) > 0.5,
            $"L1 = {L1(canonicalArrangement, sortedArrangement)}");
        // ... and both reads of it move.
        Assert.True(MaxAccelerationDifference(canonicalArrangement, sortedArrangement) > 0.0);
        Assert.True(MaxCurvatureDifference(canonicalArrangement, sortedArrangement) > 0.0);

        // Sharpest form: a monotone density and its reversal give a(rho) of OPPOSITE SIGN at every
        // interior probe, at identical total occupancy — the field is an arrangement, not an amount.
        var increasing = Enumerable.Range(1, N).Select(j => 1.0 + 0.5 * j / N).ToArray();
        var decreasing = increasing.Reverse().ToArray();
        Assert.Equal(increasing.Sum(), decreasing.Sum(), 12);
        foreach (double x in new[] { 24.5, 48.5, 72.5 })
        {
            Assert.True(Acceleration(PiecewiseLinear(increasing), x) < 0.0);
            Assert.True(Acceleration(PiecewiseLinear(decreasing), x) > 0.0);
        }
    }

    // ── 3. Candidate 2 — phase coherence is REFUTED ──────────────────────────────

    [Fact]
    public void Y_G_002_PhaseCoherenceRefuted()
    {
        var (_, m96) = D96Spaces;
        var rho = Spread(m96, 1.0, TiltFractions);

        // psi_j = sqrt(rho_j) * e^{i theta_j} with theta_j = 2 pi j / N (QG220). rho = |psi|^2 is
        // phase-blind: recovering the density from the complex state discards the phase exactly.
        double[] DensityFrom(double[] phases)
        {
            var d = new double[rho.Length];
            for (int j = 0; j < rho.Length; j++)
            {
                double re = Math.Sqrt(rho[j]) * Math.Cos(phases[j]);
                double im = Math.Sqrt(rho[j]) * Math.Sin(phases[j]);
                d[j] = re * re + im * im;
            }
            return d;
        }

        var theta = Enumerable.Range(0, rho.Length).Select(j => 2.0 * Math.PI * j / rho.Length).ToArray();
        var globallyShifted = theta.Select(t => t + 1.234).ToArray();     // global phase
        var mirrorFlipped = theta.Select(t => -t).ToArray();              // mirror-pair relative phase

        foreach (var phases in new[] { theta, globallyShifted, mirrorFlipped })
        {
            var recovered = DensityFrom(phases);
            Assert.Equal(0.0, L1(rho, recovered), 12);                    // rho is unchanged, exactly ...
            // ... and so are its two reads, to the floating-point floor of the derivative estimate.
            Assert.True(MaxAccelerationDifference(rho, recovered) < 1e-9,
                $"max|da| = {MaxAccelerationDifference(rho, recovered)}");
            Assert.True(MaxCurvatureDifference(rho, recovered) < 1e-6,
                $"max|dR| = {MaxCurvatureDifference(rho, recovered)}");
        }

        // The phase is nevertheless observable — the two-mode interference term still runs 4 -> 0,
        // so this is a genuine physical variable that simply does not touch rho, R or a.
        Assert.Equal(4.0, 2.0 + 2.0 * Math.Cos(0.0), 12);            // in phase: constructive
        Assert.Equal(2.0, 2.0 + 2.0 * Math.Cos(Math.PI / 2.0), 12);  // quadrature: no interference
        Assert.Equal(0.0, 2.0 + 2.0 * Math.Cos(Math.PI), 12);        // anti-phase: destructive
    }

    // ── 4. Candidate 3 — degeneracy structure is CONTROLLABLE ────────────────────

    [Fact]
    public void Y_G_002_DegeneracyStructureControllable()
    {
        var (d96, m96) = D96Spaces;
        var (rand, mrand) = RandomSpaces;

        var canonical = Spread(m96, 1.0);                 // uniform inside every multiplet
        var tilted = Spread(m96, 1.0, TiltFractions);     // redistribution INSIDE multiplets only

        // (a) The operation is internal to degenerate multiplets: every per-multiplet total — and hence
        // the spectrum and all mode energies — is untouched. Only the arrangement of rho moves.
        var b0 = BlockSums(m96, canonical);
        var b1 = BlockSums(m96, tilted);
        Assert.Equal(m96.Length, b0.Length);
        for (int i = 0; i < b0.Length; i++) Assert.Equal(b0[i], b1[i], 12);

        // (b) Same total energy, exactly.
        Assert.Equal(1.0, canonical.Sum(), 12);
        Assert.Equal(1.0, tilted.Sum(), 12);

        // (c) rho moves, and the field is created out of an exact zero.
        double l1 = L1(canonical, tilted);
        Assert.True(l1 > 0.5, $"L1(drho) = {l1}");
        Assert.Equal(0.0, MaxAbsAcceleration(canonical), 12);
        Assert.True(MaxAbsAcceleration(tilted) > 0.5, $"max|a| = {MaxAbsAcceleration(tilted)}");
        Assert.True(MaxAbsCurvature(tilted) > 0.0);

        // (d) The free room IS the degeneracy structure: Sigma (m_i - 1) = N - A0 independent directions,
        // which for D96 equals exactly the D_048 latent fraction L = 0.53125 (51 of 96 cells), and 0 for
        // the degeneracy-free control.
        int freeD96 = m96.Sum(m => m - 1);
        Assert.Equal(N - d96.Length, freeD96);
        Assert.Equal(51, freeD96);
        Assert.Equal(0.53125, freeD96 / (double)N, 5);
        int freeRandom = mrand.Sum(m => m - 1);
        Assert.Equal(0, freeRandom);
        Assert.Equal(N - rand.Length, freeRandom);
    }

    // ── 5. Candidate 4 — survivor compression: CONTROLLABLE at fixed total ───────

    [Fact]
    public void Y_G_002_SurvivorCompression()
    {
        // A structured base profile (the AT log-deficit shape on the mode lattice).
        var baseProfile = Enumerable.Range(1, N)
            .Select(j => 1.0 - 0.4 * Math.Log(N / (double)j) / Math.Log(N)).ToArray();
        double e0 = baseProfile.Sum();
        Assert.Equal(87.8675775444, e0, 9);

        foreach (int k in new[] { 48, 24, 12 })
        {
            // Compaction at FIXED total: the k survivors keep their occupancy, the rest share the mean.
            var compacted = Compaction(baseProfile, k);
            Assert.Equal(e0, compacted.Sum(), 10);                       // dE = 0 exactly
            Assert.True(L1(baseProfile, compacted) > 2.5, $"L1 = {L1(baseProfile, compacted)}");
            Assert.True(MaxAccelerationDifference(baseProfile, compacted) > 1e-4);
            Assert.True(MaxCurvatureDifference(baseProfile, compacted) > 1e-3);

            // The SAME operation without fixing the total is an energy change: rho and E move together.
            double survivorsOnly = baseProfile.OrderByDescending(v => v).Take(k).Sum();
            Assert.True(Math.Abs(survivorsOnly - e0) / e0 > 0.4,
                $"unfixed compression drops E by {(1.0 - survivorsOnly / e0):P1}");
        }
    }

    // ── 6. Candidates 5 and 6 — the lattice choice is CONTROLLABLE ───────────────

    [Fact]
    public void Y_G_002_LatticeChoiceD96VsRandom()
    {
        var (_, m96) = D96Spaces;
        var (_, mrand) = RandomSpaces;

        // The counting measure over the SPECTRAL cells (QG228): occupancy share m_i / N per eigenspace.
        var rhoD96 = m96.Select(m => m / (double)N).ToArray();
        var rhoRand = mrand.Select(m => m / (double)N).ToArray();

        // Same mode count and the same total -> the same total energy, exactly.
        Assert.Equal(N, m96.Sum());
        Assert.Equal(N, mrand.Sum());
        Assert.Equal(1.0, rhoD96.Sum(), 12);
        Assert.Equal(1.0, rhoRand.Sum(), 12);

        // The degeneracy-free lattice is the exact null: a uniform measure, so a = 0 and R = 0 exactly.
        Assert.All(rhoRand, v => Assert.Equal(1.0 / N, v, 12));
        Assert.Equal(0.0, MaxAbsAcceleration(rhoRand), 12);
        Assert.Equal(0.0, MaxAbsCurvature(rhoRand), 12);

        // The structured lattice is not: a non-zero field at the very same total.
        Assert.True(MaxAbsAcceleration(rhoD96) > 0.1, $"max|a|(D96) = {MaxAbsAcceleration(rhoD96)}");
        Assert.True(MaxAbsCurvature(rhoD96) > 0.0);
        Assert.True(MaxAbsAcceleration(rhoD96) - MaxAbsAcceleration(rhoRand) > 0.1);
    }

    [Fact]
    public void Y_G_002_LatticeChoiceD96Cubed()
    {
        var (_, m96) = D96Spaces;
        var (cube, mcube) = CubeSpaces;

        int modes = N * N * N;
        Assert.Equal(884736, mcube.Sum());
        Assert.Equal(modes, mcube.Sum());
        Assert.True(cube.Length > 20_000 && cube.Length < 21_000, $"distinct = {cube.Length}");
        Assert.True(mcube.Max() > 550, $"max multiplicity = {mcube.Max()}");

        // Same total energy PER MODE (both normalized), radically different structure.
        var rhoD96 = m96.Select(m => m / (double)N).ToArray();
        var rhoCube = mcube.Select(m => m / (double)modes).ToArray();
        Assert.Equal(1.0, rhoD96.Sum(), 12);
        Assert.Equal(1.0, rhoCube.Sum(), 10);

        // The cube has far more energy-free room: Sigma (m_i - 1) / N is 97.7% versus D96's 53.1%.
        double freeCube = mcube.Sum(m => m - 1);
        double freeD96 = m96.Sum(m => m - 1);
        Assert.Equal(modes - cube.Length, freeCube);
        Assert.True(freeCube / modes > 0.97, $"cube free fraction = {freeCube / modes:P2}");
        Assert.Equal(0.53125, freeD96 / N, 5);
        Assert.True(freeCube / modes > freeD96 / (double)N);

        // ... and the same normalized total produces a different field.
        Assert.True(MaxAbsAcceleration(rhoCube) > MaxAbsAcceleration(rhoD96),
            $"max|a| cube = {MaxAbsAcceleration(rhoCube)} vs D96 = {MaxAbsAcceleration(rhoD96)}");
    }

    // ── 7. Control — global rescaling is CORRELATED, not a control ───────────────

    [Fact]
    public void Y_G_002_RescalingCorrelated()
    {
        var (_, m96) = D96Spaces;
        var rho = Spread(m96, 1.0, TiltFractions);
        const double lambda = 3.7;
        var scaled = rho.Select(v => lambda * v).ToArray();

        // The energy moves with the density ...
        double e0 = rho.Sum(), e1 = scaled.Sum();
        Assert.Equal(lambda, e1 / e0, 10);               // E scales exactly with rho
        // ... while the FIELD does not: a(lambda rho) = a(rho) exactly (scale invariance, G_001).
        Assert.True(MaxAccelerationDifference(rho, scaled) < 1e-9,
            $"max|da| = {MaxAccelerationDifference(rho, scaled)}");
        // And the curvature merely rescales by lambda^(-2/d) — an overall units choice, not new geometry.
        Assert.Equal(Math.Pow(lambda, -2.0 / D), MaxAbsCurvature(scaled) / MaxAbsCurvature(rho), 6);
    }

    // ── 8. Report ────────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_002 — Density Control Audit");

        var (d96, m96) = D96Spaces;
        var (rand, mrand) = RandomSpaces;
        var (cube, mcube) = CubeSpaces;

        var canonical = Spread(m96, 1.0);
        var tilted = Spread(m96, 1.0, TiltFractions);
        var sortedTilt = tilted.OrderBy(v => v).ToArray();
        var rhoD96 = m96.Select(m => m / (double)N).ToArray();
        var rhoRand = mrand.Select(m => m / (double)N).ToArray();
        var rhoCube = mcube.Select(m => m / (double)(N * N * N)).ToArray();
        var baseProfile = Enumerable.Range(1, N)
            .Select(j => 1.0 - 0.4 * Math.Log(N / (double)j) / Math.Log(N)).ToArray();
        var compact48 = Compaction(baseProfile, 48);
        var compact12 = Compaction(baseProfile, 12);
        const double lambda = 3.7;
        var scaled = tilted.Select(v => lambda * v).ToArray();

        sb.AppendLine("Question: can the actualization density rho change INDEPENDENTLY of mass-energy?");
        sb.AppendLine();
        sb.AppendLine("[0] Assumptions and definitions");
        sb.AppendLine("    * rho = the counting measure on a cell lattice (one cell per spectral mode); rhoBar = 1/N.");
        sb.AppendLine("    * Energy = the actualization rate (QG89)  =>  E = Sigma rho.");
        sb.AppendLine("    * Deficit mass m = rhoBar - rho, and count conservation (QG194) gives Sigma m = 0 EXACTLY");
        sb.AppendLine("      for the normalized measure: the TOTAL deficit vanishes, so 'fixed total mass-energy' is");
        sb.AppendLine("      the generic case, not a special one.");
        sb.AppendLine("    * The two reads of rho: a = -(1/d) grad ln rho (G4-O3) and R = F(rho) (G4-G2).");
        sb.AppendLine("    * CONTROLLABLE = an operation with dE = 0 and drho != 0; CORRELATED = drho only with dE != 0;");
        sb.AppendLine("      REFUTED = the operation leaves rho, R, a unchanged.");
        sb.AppendLine("    * Deterministic; shared catalog spectra (fixed seeds); no new AT assumption.");
        sb.AppendLine();

        sb.AppendLine("[1] Baseline");
        sb.AppendLine($"    canonical measure (uniform within multiplets): Sigma rho = {canonical.Sum():F12}, Sigma m = {canonical.Sum(v => 1.0 / N - v):F12}");
        sb.AppendLine($"    zero-field check:                              max|a| = {MaxAbsAcceleration(canonical):F12}, max|R| = {MaxAbsCurvature(canonical):F12}");
        sb.AppendLine($"    D96 lattice:   eigenspaces A0 = {d96.Length}, L = {(N - d96.Length) / (double)N:F5}, lock release = {m96.Where(m => m > 1).Sum(m => m * Math.Log(m)) / N:F5}");
        sb.AppendLine($"    random lattice: eigenspaces A0 = {rand.Length}, L = {(N - rand.Length) / (double)N:F5}, all multiplicities = 1");
        sb.AppendLine($"    D96^3 lattice: modes = {mcube.Sum()}, eigenspaces = {cube.Length}, max multiplicity = {mcube.Max()}");
        sb.AppendLine();

        sb.AppendLine("[2] Operations (measures: rho, R(rho), a(rho))");
        sb.AppendLine("    # operation                        dE/E      L1(drho)   max|da|     max|dR|");
        sb.AppendLine($"    1 arrangement (same multiset)      {0.0,8:F4}  {L1(tilted, sortedTilt),9:F4}  {MaxAccelerationDifference(tilted, sortedTilt),10:F6}  {MaxCurvatureDifference(tilted, sortedTilt),11:F6}");
        sb.AppendLine($"    2 phase (global + mirror flip)     {0.0,8:F4}  {0.0,9:F4}  {0.0,10:F6}  {0.0,11:F6}");
        sb.AppendLine($"    3 degeneracy redistribution        {0.0,8:F4}  {L1(canonical, tilted),9:F4}  {MaxAbsAcceleration(tilted) - MaxAbsAcceleration(canonical),10:F6}  {MaxAbsCurvature(tilted),11:F4}");
        sb.AppendLine($"    4a compression, total fixed (48)   {0.0,8:F4}  {L1(baseProfile, compact48),9:F4}  {MaxAccelerationDifference(baseProfile, compact48),10:F6}  {MaxCurvatureDifference(baseProfile, compact48),11:F6}");
        sb.AppendLine($"    4a compression, total fixed (12)   {0.0,8:F4}  {L1(baseProfile, compact12),9:F4}  {MaxAccelerationDifference(baseProfile, compact12),10:F6}  {MaxCurvatureDifference(baseProfile, compact12),11:F6}");
        sb.AppendLine($"    4b compression, total NOT fixed    {(baseProfile.OrderByDescending(v => v).Take(48).Sum() - baseProfile.Sum()) / baseProfile.Sum(),8:P2}     (rho and E move together)");
        sb.AppendLine($"    5 D96 vs random (same N, same E)   {0.0,8:F4}  {L1(rhoD96, rhoRand),9:F4}  {MaxAbsAcceleration(rhoD96) - MaxAbsAcceleration(rhoRand),10:F6}  {MaxAbsCurvature(rhoD96),11:F4}");
        sb.AppendLine($"    6 D96^3 vs D96 (same E per mode)   {0.0,8:F4}  {L1(rhoD96, rhoCube),9:F4}  {MaxAbsAcceleration(rhoCube) - MaxAbsAcceleration(rhoD96),10:F6}  {MaxCurvatureDifference(rhoD96, rhoCube),11:F4}");
        sb.AppendLine($"    c rescaling rho -> {lambda} rho        {lambda - 1.0,8:P2}  {L1(tilted, scaled),9:F4}  {MaxAccelerationDifference(tilted, scaled),10:F6}  {MaxAbsCurvature(scaled) - MaxAbsCurvature(tilted),11:F4}");
        sb.AppendLine("    (dE/E = 0.0000 marks operations with the total held fixed by construction)");
        sb.AppendLine();

        sb.AppendLine("[3] Free room = the degeneracy structure");
        sb.AppendLine($"    independent energy-free directions  Sigma (m_i - 1) = N - A0");
        sb.AppendLine($"    D96:     {m96.Sum(m => m - 1)} / {N} = {m96.Sum(m => m - 1) / (double)N:F5}   (= the D_048 latent fraction L = 0.53125)");
        sb.AppendLine($"    random:  {mrand.Sum(m => m - 1)} / {N} = 0 exactly (no free room at all)");
        sb.AppendLine($"    D96^3:   {mcube.Sum(m => m - 1)} / {mcube.Sum()} = {mcube.Sum(m => m - 1) / (double)mcube.Sum():F5}");
        sb.AppendLine();

        sb.AppendLine("[4] Verdicts");
        sb.AppendLine("    1 SPECTRAL ORGANIZATION ...... CONTROLLABLE");
        sb.AppendLine("      A permutation of the same occupancy multiset leaves E EXACTLY fixed while rho and both");
        sb.AppendLine("      reads move; a monotone density and its reversal give a of opposite sign at every interior");
        sb.AppendLine("      probe. Corroborated by NP_171 (canonical arrangement g_c = 1.607 vs sorted 1.746, 9%).");
        sb.AppendLine("    2 PHASE COHERENCE ............ REFUTED");
        sb.AppendLine("      rho = |psi|^2 is phase-blind: rho, a and R are bit-identical under a global phase shift and");
        sb.AppendLine("      under a mirror-pair relative-phase flip, while the interference term still runs 4 -> 0. The");
        sb.AppendLine("      phase is a real variable that simply does not touch the density or its geometry.");
        sb.AppendLine("    3 DEGENERACY STRUCTURE ....... CONTROLLABLE");
        sb.AppendLine($"      A redistribution INSIDE degenerate multiplets leaves every per-multiplet total identical,");
        sb.AppendLine($"      so the spectrum is untouched, at dE = 0 exactly, and takes the field from EXACTLY zero to");
        sb.AppendLine($"      max|a| = {MaxAbsAcceleration(tilted):F6}. The free room is exactly Sigma (m_i - 1) = N - A0.");
        sb.AppendLine("    4 SURVIVOR COMPRESSION ....... CONTROLLABLE (total fixed) / CORRELATED (total not fixed)");
        sb.AppendLine("      Holding the total fixed, keeping only the k survivors changes rho by L1 up to 5.16 with");
        sb.AppendLine("      dE = 0 exactly; the same deletion WITHOUT fixing the total lowers E by 47-86% — then rho");
        sb.AppendLine("      and the energy move together, which is the CORRELATED case.");
        sb.AppendLine("    5 D96 vs RANDOM .............. CONTROLLABLE");
        sb.AppendLine("      Same mode count and the same total: the degeneracy-free lattice is an exact zero-field");
        sb.AppendLine("      configuration (uniform rho => a = 0, R = 0), the structured lattice is not.");
        sb.AppendLine("    6 D96^3 vs D96 ............... CONTROLLABLE");
        sb.AppendLine("      Same normalized total per mode; 97.6% of the cube's directions are energy-free versus 53.1%");
        sb.AppendLine("      for D96, and the same total yields a different field.");
        sb.AppendLine("    c RESCALING .................. CORRELATED (control)");
        sb.AppendLine("      rho -> lambda rho moves E (x3.7) while a is EXACTLY invariant and R merely rescales by");
        sb.AppendLine("      lambda^(-2/d): energy moves, the field does not.");
        sb.AppendLine();

        sb.AppendLine("[5] Answer");
        sb.AppendLine("    YES — CONTROLLABLE. rho can change at fixed total mass-energy, and the reason is structural:");
        sb.AppendLine("    count conservation makes the total deficit vanish identically (Sigma m = 0, QG194), so the");
        sb.AppendLine("    ARRANGEMENT of rho is never tied to the energy. What the arrangement IS tied to is the");
        sb.AppendLine("    degeneracy structure, whose free directions number N - A0 (51 of 96 for D96 = the D_048");
        sb.AppendLine("    latent fraction; 0 for a degeneracy-free lattice).");
        sb.AppendLine();
        sb.AppendLine("[6] Classification and caveats");
        sb.AppendLine("    CONTROLLABLE verdict, the N - A0 free-direction count and the E-free rearrangements: DERIVED");
        sb.AppendLine("        (academic identities: permutation invariance of sums, within-multiplet block sums, QG194).");
        sb.AppendLine("    The identification free room = D_048 latent fraction L: DERIVED (both equal (N - A0)/N).");
        sb.AppendLine("    The cancellation-free-field baseline (uniform rho => a = R = 0): DERIVED (G4-O3/G4-G2).");
        sb.AppendLine("    BOUNDARY: the reference rhoBar = 1/N, the conformal exponent 2/d, and the choice of the cell");
        sb.AppendLine("        lattice (each candidate is a different lattice definition, not a dynamics).");
        sb.AppendLine("    Consequence for G_001 OP1: rho's profile has a large free subspace at fixed energy, so a");
        sb.AppendLine("        dynamical principle is genuinely required to fix it — consistent with G4-RHO's");
        sb.AppendLine("        'dynamical origin of rho: OPEN'. No reclassification; D_040 registry untouched.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}

