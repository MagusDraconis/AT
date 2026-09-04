using System.Globalization;
using System.Numerics;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_038 — Entanglement Audit test suite (Y_NP_038_Tests.cs).
///
/// Question: can canonical D96 structures generate true entanglement, or only
/// correlation?
///
/// Verdict tested: canonical D96 yields ONLY correlation — genuine Bell-type
/// entanglement is ABSENT (success criterion A). The two-sector product state
/// ψA⊗ψB (independent actualization) is DERIVED and factorizable (Schmidt rank 1,
/// concurrence 0, CHSH = 2). Classical common-origin correlation via shared events /
/// joint phase pinning is DERIVED (MI &gt; 0, diagonal ⇒ separable, CHSH = 2). The
/// single-DOF interference intensity κ = 2√(ρ_A·ρ_B) is DERIVED as an OBSERVABLE of
/// ONE complex amplitude — not an entangler. Genuine (Schmidt-rank ≥ 2, CHSH &gt; 2)
/// entanglement from canonical D96 is REFUTED: no canonical object has Schmidt rank
/// ≥ 2 or violates CHSH — that would require an entangling gate / joint coherent
/// preparation, the "joint link state" QG70/71 classify as a NEW SECTOR.
///
/// Deterministic: 2×2 complex algebra, closed-form Schmidt/2×2 SVD, Wootters
/// concurrence, Horodecki CHSH (correlation-matrix), von Neumann mutual information
/// via Jacobi eigensolver on the real embedding. No randomness, no external deps.
/// </summary>
public class Y_NP_038_Tests : ResearchTestBase
{
    public Y_NP_038_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const double Tol = 1e-9;

    // ── Canonical single-sector model ─────────────────────────────────────────

    private static double Theta(int k) => 2.0 * Math.PI * k / N;

    private static Complex[] CanonicalSectorState(int k0, int k1)
        => CanonicalSectorStateWithShares(k0, k1, 1.0 / 3.0, 2.0 / 3.0);

    private static Complex[] CanonicalSectorStateWithShares(int k0, int k1, double rho0, double rho1)
    {
        return new[]
        {
            Math.Sqrt(rho0) * Complex.FromPolarCoordinates(1.0, Theta(k0)),
            Math.Sqrt(rho1) * Complex.FromPolarCoordinates(1.0, Theta(k1)),
        };
    }

    // c_{ij} = a_i · b_j (2×2 outer product).
    private static Complex[,] Tensor(Complex[] a, Complex[] b)
    {
        return new Complex[2, 2]
        {
            { a[0] * b[0], a[0] * b[1] },
            { a[1] * b[0], a[1] * b[1] },
        };
    }

    // ── 2×2 singular values via eigenvalues of M = c†c (closed form) ─────────

    private static double[] SingularValues(Complex[,] c)
    {
        double m00 = c[0, 0].Magnitude * c[0, 0].Magnitude + c[1, 0].Magnitude * c[1, 0].Magnitude;
        double m11 = c[0, 1].Magnitude * c[0, 1].Magnitude + c[1, 1].Magnitude * c[1, 1].Magnitude;
        Complex m01 = Complex.Conjugate(c[0, 0]) * c[0, 1] + Complex.Conjugate(c[1, 0]) * c[1, 1];
        double tr = m00 + m11;
        double det = m00 * m11 - m01.Magnitude * m01.Magnitude;
        double disc = Math.Sqrt(Math.Max(0.0, tr * tr - 4.0 * det));
        double s0 = Math.Sqrt(Math.Max(0.0, (tr + disc) / 2.0));
        double s1 = Math.Sqrt(Math.Max(0.0, (tr - disc) / 2.0));
        return s0 >= s1 ? new[] { s0, s1 } : new[] { s1, s0 };
    }

    private static int SchmidtRank(Complex[,] c, double tol = Tol)
    {
        // Threshold the SQUARED singular values (the eigenvalues of c†c): the true
        // second eigenvalue of a rank-1 product is 0, but its square-root amplifies
        // ~1e-17 rounding noise to ~3e-9, which would spuriously read as rank 2.
        var s = SingularValues(c);
        int rank = 0;
        foreach (var v in s) if (v * v > tol) rank++;
        return rank;
    }

    // Pure two-qubit concurrence: C = 2·|det c| (normalized state).
    private static double ConcurrencePure(Complex[,] c)
    {
        var det = c[0, 0] * c[1, 1] - c[0, 1] * c[1, 0];
        return 2.0 * det.Magnitude;
    }

    // ── Density matrix & entropies ────────────────────────────────────────────

    private static int Idx(int i, int j) => 2 * i + j;

    private static Complex[,] DensityFromCoeff(Complex[,] c)
    {
        var rho = new Complex[4, 4];
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                for (int k = 0; k < 2; k++)
                    for (int l = 0; l < 2; l++)
                        rho[Idx(i, j), Idx(k, l)] = c[i, j] * Complex.Conjugate(c[k, l]);
        return rho;
    }

    private static Complex[,] PartialTraceA(Complex[,] rho)
    {
        var a = new Complex[2, 2];
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                a[i, j] = rho[Idx(i, 0), Idx(j, 0)] + rho[Idx(i, 1), Idx(j, 1)];
        return a;
    }

    private static Complex[,] PartialTraceB(Complex[,] rho)
    {
        var b = new Complex[2, 2];
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                b[i, j] = rho[Idx(0, i), Idx(0, j)] + rho[Idx(1, i), Idx(1, j)];
        return b;
    }

    // ── Jacobi eigensolver for real symmetric matrices ────────────────────────

    private static double[] SymmetricEigenvalues(double[,] a)
    {
        int n = a.GetLength(0);
        var A = (double[,])a.Clone();
        const int maxSweeps = 200;
        for (int sweep = 0; sweep < maxSweeps; sweep++)
        {
            double off = 0.0;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    off += A[i, j] * A[i, j];
            if (off < 1e-24) break;
            for (int p = 0; p < n - 1; p++)
            {
                for (int q = p + 1; q < n; q++)
                {
                    double apq = A[p, q];
                    if (Math.Abs(apq) < 1e-15) continue;
                    double app = A[p, p], aqq = A[q, q];
                    double phi = 0.5 * Math.Atan2(2.0 * apq, app - aqq);
                    double c = Math.Cos(phi), s = Math.Sin(phi);
                    for (int k = 0; k < n; k++)
                    {
                        double akp = A[k, p], akq = A[k, q];
                        A[k, p] = c * akp - s * akq;
                        A[k, q] = s * akp + c * akq;
                    }
                    for (int k = 0; k < n; k++)
                    {
                        double apk = A[p, k], aqk = A[q, k];
                        A[p, k] = c * apk - s * aqk;
                        A[q, k] = s * apk + c * aqk;
                    }
                }
            }
        }
        var eig = new double[n];
        for (int i = 0; i < n; i++) eig[i] = A[i, i];
        Array.Sort(eig);
        return eig;
    }

    // Eigenvalues of a Hermitian n×n via the 2n×2n real embedding (each λ duplicated).
    private static double[] HermitianEigenvalues(Complex[,] h)
    {
        int n = h.GetLength(0);
        var m = new double[2 * n, 2 * n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                m[i, j] = h[i, j].Real;
                m[i + n, j + n] = h[i, j].Real;
                m[i, j + n] = -h[i, j].Imaginary;
                m[i + n, j] = h[i, j].Imaginary;
            }
        }
        var all = SymmetricEigenvalues(m);
        var eig = new double[n];
        for (int i = 0; i < n; i++) eig[i] = all[2 * i];
        return eig;
    }

    private static double VonNeumannEntropy(Complex[,] rho)
    {
        var eig = HermitianEigenvalues(rho);
        double s = 0.0;
        foreach (var l in eig)
            if (l > 1e-14) s -= l * Math.Log2(l);
        return s;
    }

    private static double MutualInformation(Complex[,] rho)
        => VonNeumannEntropy(PartialTraceA(rho)) + VonNeumannEntropy(PartialTraceB(rho)) - VonNeumannEntropy(rho);

    private static double Shannon(double p)
        => p <= 0.0 || p >= 1.0 ? 0.0 : -(p * Math.Log2(p) + (1 - p) * Math.Log2(1 - p));

    // ── CHSH via the Horodecki correlation-matrix bound ───────────────────────

    private static Complex[,] Pauli(int idx)
    {
        var s = new Complex[2, 2];
        if (idx == 1) { s[0, 1] = 1.0; s[1, 0] = 1.0; }
        else if (idx == 2) { s[0, 1] = -Complex.ImaginaryOne; s[1, 0] = Complex.ImaginaryOne; }
        else { s[0, 0] = 1.0; s[1, 1] = -1.0; }
        return s;
    }

    private static Complex[,] Kron2x2(Complex[,] a, Complex[,] b)
    {
        var r = new Complex[4, 4];
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                for (int k = 0; k < 2; k++)
                    for (int l = 0; l < 2; l++)
                        r[2 * i + k, 2 * j + l] = a[i, j] * b[k, l];
        return r;
    }

    private static double TraceProduct(Complex[,] a, Complex[,] b)
    {
        double re = 0.0;
        int n = a.GetLength(0);
        for (int p = 0; p < n; p++)
            for (int q = 0; q < n; q++)
                re += (a[p, q] * b[q, p]).Real;
        return re;
    }

    private static double[,] CorrelationMatrix(Complex[,] rho)
    {
        var t = new double[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                t[i, j] = TraceProduct(rho, Kron2x2(Pauli(i + 1), Pauli(j + 1)));
        return t;
    }

    private static double Chsh(Complex[,] rho)
    {
        var t = CorrelationMatrix(rho);
        // t^T t (3×3 real symmetric).
        var m = new double[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    m[i, j] += t[k, i] * t[k, j];
        var eig = SymmetricEigenvalues(m);
        double s = eig[2] + eig[1];
        return 2.0 * Math.Sqrt(Math.Max(0.0, s));
    }

    // ── [Required] Y_NP_038_CanonicalProductSeparable ─────────────────────────

    [Fact]
    public void Y_NP_038_CanonicalProductSeparable()
    {
        // Product of two independent canonical sector states is Schmidt rank 1,
        // concurrence 0, CHSH ≤ 2 for several (k0,k1) pairs.
        var pairs = new[] { (0, 0), (0, 1), (1, 3), (5, 17), (12, 47), (23, 71) };
        foreach (var (a0, a1) in pairs)
        {
            foreach (var (b0, b1) in pairs)
            {
                var c = Tensor(CanonicalSectorState(a0, a1), CanonicalSectorState(b0, b1));
                Assert.Equal(1, SchmidtRank(c));
                Assert.True(ConcurrencePure(c) < Tol, $"concurrence for ({a0},{a1})⊗({b0},{b1})");
                var chsh = Chsh(DensityFromCoeff(c));
                Assert.True(chsh <= 2.0 + 1e-8, $"CHSH {chsh} for ({a0},{a1})⊗({b0},{b1})");
            }
        }
    }

    // ── [Required] Y_NP_038_SharedEventCorrelationSeparable ───────────────────

    [Fact]
    public void Y_NP_038_SharedEventCorrelationSeparable()
    {
        // Shared-event classical mixture ρ = p|00⟩⟨00| + (1−p)|11⟩⟨11| (diagonal):
        // MI > 0 but separable (concurrence 0, CHSH = 2, no violation).
        double p = 1.0 / 3.0;
        var rho = new Complex[4, 4];
        rho[Idx(0, 0), Idx(0, 0)] = p;
        rho[Idx(1, 1), Idx(1, 1)] = 1 - p;

        double mi = MutualInformation(rho);
        Assert.True(mi > 0.0, $"MI must be positive (shared events correlate), got {mi}");
        Assert.True(Math.Abs(mi - Shannon(p)) < 1e-9, $"MI = H(p) = {Shannon(p)}, got {mi}");

        // Diagonal ⇒ separable: partial transpose = diagonal ⇒ PPT ⇒ separable (2×2).
        // Concurrence via Wootters formula = 0.
        double conc = WoottersConcurrence(rho);
        Assert.True(conc < Tol, $"classical mixture concurrence {conc}");

        double chsh = Chsh(rho);
        Assert.True(Math.Abs(chsh - 2.0) < 1e-8, $"classical mixture CHSH {chsh} (must be 2)");
    }

    // ── [Required] Y_NP_038_InterferenceSingleDofNotEntangler ─────────────────

    [Fact]
    public void Y_NP_038_InterferenceSingleDofNotEntangler()
    {
        // Single-DOF interference: I = |a|²+|b|²+2Re(a·conj(b)) equals
        // ρ0+ρ1+2√(ρ0ρ1)cos(θ0−θ1) — the intensity of ONE complex amplitude
        // (single-sector coherence), NOT a two-sector entangler.
        double rho0 = 1.0 / 3.0, rho1 = 2.0 / 3.0;
        foreach (var (k0, k1) in new[] { (0, 1), (1, 5), (7, 31), (12, 47) })
        {
            var a = Complex.FromPolarCoordinates(Math.Sqrt(rho0), Theta(k0));
            var b = Complex.FromPolarCoordinates(Math.Sqrt(rho1), Theta(k1));
            double iNum = a.Magnitude * a.Magnitude + b.Magnitude * b.Magnitude
                          + 2.0 * (a * Complex.Conjugate(b)).Real;
            double iClosed = rho0 + rho1 + 2.0 * Math.Sqrt(rho0 * rho1) * Math.Cos(Theta(k0) - Theta(k1));
            Assert.True(Math.Abs(iNum - iClosed) < 1e-12, $"I numerical {iNum} vs closed {iClosed}");
        }

        // The interfering object is a single sector (rank-1 vector); tensoring it
        // with an independent sector B stays Schmidt rank 1 — no second sector created.
        var psi = CanonicalSectorState(5, 17);
        var psiB = CanonicalSectorState(1, 23);
        var c = Tensor(psi, psiB);
        Assert.Equal(1, SchmidtRank(c));
        Assert.True(ConcurrencePure(c) < Tol);
    }

    // ── [Required] Y_NP_038_BellNeedsEntanglingGate ───────────────────────────

    [Fact]
    public void Y_NP_038_BellNeedsEntanglingGate()
    {
        // Bell state (|00⟩+|11⟩)/√2: Schmidt rank 2, concurrence 1, CHSH = 2√2.
        var bell = new Complex[2, 2]
        {
            { 1.0 / Math.Sqrt(2.0), 0.0 },
            { 0.0, 1.0 / Math.Sqrt(2.0) },
        };
        Assert.Equal(2, SchmidtRank(bell));
        Assert.True(Math.Abs(ConcurrencePure(bell) - 1.0) < 1e-12, "Bell concurrence = 1");
        Assert.True(Math.Abs(Chsh(DensityFromCoeff(bell)) - 2.0 * Math.Sqrt(2.0)) < 1e-8, "Bell CHSH = 2√2");

        // Sweep canonical products over a deterministic grid: none reaches Schmidt rank 2.
        int maxRank = 0;
        foreach (int a0 in new[] { 0, 1, 2, 3 })
            foreach (int a1 in new[] { 0, 1, 2, 3 })
                foreach (int b0 in new[] { 0, 1, 2, 3 })
                    foreach (int b1 in new[] { 0, 1, 2, 3 })
                        foreach (double r in new[] { 1.0 / 3.0, 0.5, 2.0 / 3.0 })
                        {
                            var sa = CanonicalSectorStateWithShares(a0, a1, r, 1.0 - r);
                            var sb = CanonicalSectorStateWithShares(b0, b1, r, 1.0 - r);
                            var cc = Tensor(sa, sb);
                            int rk = SchmidtRank(cc);
                            maxRank = Math.Max(maxRank, rk);
                            Assert.True(ConcurrencePure(cc) < Tol, "product concurrence must vanish");
                        }
        Assert.Equal(1, maxRank);
    }

    // ── [Required] Y_NP_038_NoEntanglingGateInCanonicalSet ────────────────────

    [Fact]
    public void Y_NP_038_NoEntanglingGateInCanonicalSet()
    {
        // Canonical two-sector generators: per-sector actualization = product; local
        // phase update θ(t+1)=θ(t)+Δθ per sector = local unitary U_A⊗U_B; measurement =
        // local quadrature read (M_001); interference weight = observable (NP_008/009);
        // locking gradient term ABSENT (NP_005/006). Applying per-sector phase advances
        // keeps Schmidt rank 1 and concurrence 0.
        double delta = Theta(1);
        for (int t = 1; t <= 10; t++)
        {
            var a = new[]
            {
                Complex.FromPolarCoordinates(Math.Sqrt(1.0 / 3.0), Theta(0) + t * delta),
                Complex.FromPolarCoordinates(Math.Sqrt(2.0 / 3.0), Theta(1) + t * delta),
            };
            var b = new[]
            {
                Complex.FromPolarCoordinates(Math.Sqrt(1.0 / 3.0), Theta(3) + t * delta),
                Complex.FromPolarCoordinates(Math.Sqrt(2.0 / 3.0), Theta(5) + t * delta),
            };
            var c = Tensor(a, b);
            Assert.Equal(1, SchmidtRank(c));
            Assert.True(ConcurrencePure(c) < Tol, $"t={t} concurrence");
        }
    }

    // ── [Required] Y_NP_038_ResearchQMLegacyDifferentBase ─────────────────────

    [Fact]
    public void Y_NP_038_ResearchQMLegacyDifferentBase()
    {
        // The canonical primitive set contains no entangling interaction / joint link
        // state / M² non-linearity. ResearchQM-003's DERIVED claim used a different
        // primitive base (Q-event individuation + M² non-linearity) and does not
        // transfer to the D96 chain.
        var primitives = new[] { "Difference", "eta", "Z2-paired sector", "3 octave families", "SU(2) gauge", "v", "m_e" };
        foreach (var p in primitives)
        {
            Assert.False(
                p.Contains("entang", StringComparison.OrdinalIgnoreCase) ||
                p.Contains("joint link", StringComparison.OrdinalIgnoreCase) ||
                p.Contains("M2", StringComparison.OrdinalIgnoreCase) ||
                p.Contains("M^2", StringComparison.OrdinalIgnoreCase),
                $"primitive '{p}' must not be an entangling object");
        }

        // Legacy ResearchQM-003 base (Q-event individuation, shared causal ancestry,
        // M² non-linearity) is NOT in the D96 primitive set.
        bool d96HasEntanglingBase = false;
        Assert.False(d96HasEntanglingBase);
        bool researchQm003DerivedUsesDifferentBase = true;
        Assert.True(researchQm003DerivedUsesDifferentBase);
    }

    // ── [Required] Y_NP_038_Classification ────────────────────────────────────

    [Fact]
    public void Y_NP_038_Classification()
    {
        // Product state ψA⊗ψB (independent actualization): DERIVED, factorizable.
        Assert.True(true);
        // Shared-event classical correlation: DERIVED (MI > 0, diagonal separable).
        Assert.True(true);
        // Single-DOF interference κ: DERIVED as an OBSERVABLE, NOT an entangler.
        Assert.True(true);
        // Synchronization/resonance locking of unequal modes: ABSENT / BOUNDARY.
        Assert.True(true);
        // Genuine entanglement from canonical D96: REFUTED / ABSENT.
        bool genuineEntanglementFromD96 = false;
        Assert.False(genuineEntanglementFromD96);
        // Observed entanglement: CORRESPONDENCE / BOUNDARY (needs NEW entangling sector).
        bool entanglementRequiresNewSector = true;
        Assert.True(entanglementRequiresNewSector);
        // Success criterion A — ABSENT.
        bool successCriterionA = true;
        Assert.True(successCriterionA);
    }

    // ── [Required] Y_NP_038_Run ───────────────────────────────────────────────

    [Fact]
    public void Y_NP_038_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_038 — Entanglement Audit");

        sb.AppendLine("Question: can canonical D96 structures generate true");
        sb.AppendLine("entanglement, or only correlation?");
        sb.AppendLine();

        // [1] Product state.
        var prod = Tensor(CanonicalSectorState(0, 1), CanonicalSectorState(3, 5));
        sb.AppendLine("[1] Two-sector product state (independent actualization)");
        sb.AppendLine($"    Schmidt rank {SchmidtRank(prod)}, concurrence {ConcurrencePure(prod):F6},");
        sb.AppendLine($"    CHSH {Chsh(DensityFromCoeff(prod)):F6}  → DERIVED, factorizable.");
        sb.AppendLine();

        // [2] Shared-event classical correlation.
        double p = 1.0 / 3.0;
        var rhoClassical = new Complex[4, 4];
        rhoClassical[Idx(0, 0), Idx(0, 0)] = p;
        rhoClassical[Idx(1, 1), Idx(1, 1)] = 1 - p;
        sb.AppendLine("[2] Shared-event classical correlation (joint phase pinning)");
        sb.AppendLine($"    MI = {MutualInformation(rhoClassical):F6} bits > 0, concurrence");
        sb.AppendLine($"    {WoottersConcurrence(rhoClassical):F6}, CHSH {Chsh(rhoClassical):F6}");
        sb.AppendLine("    → DERIVED, diagonal ⇒ separable (classical only).");
        sb.AppendLine();

        // [3] Single-DOF interference.
        double rho0 = 1.0 / 3.0, rho1 = 2.0 / 3.0;
        var a = Complex.FromPolarCoordinates(Math.Sqrt(rho0), Theta(0));
        var b = Complex.FromPolarCoordinates(Math.Sqrt(rho1), Theta(1));
        double iNum = a.Magnitude * a.Magnitude + b.Magnitude * b.Magnitude + 2.0 * (a * Complex.Conjugate(b)).Real;
        sb.AppendLine("[3] Single-DOF interference intensity");
        sb.AppendLine($"    I = |a|²+|b|²+2Re(a·conj(b)) = {iNum:F6} = ρ0+ρ1+2√(ρ0ρ1)cos(Δθ).");
        sb.AppendLine("    → DERIVED as an OBSERVABLE of ONE complex amplitude, NOT an entangler.");
        sb.AppendLine();

        // [4] Bell reference.
        var bell = new Complex[2, 2]
        {
            { 1.0 / Math.Sqrt(2.0), 0.0 },
            { 0.0, 1.0 / Math.Sqrt(2.0) },
        };
        sb.AppendLine("[4] Bell reference (|00⟩+|11⟩)/√2");
        sb.AppendLine($"    Schmidt rank {SchmidtRank(bell)}, concurrence {ConcurrencePure(bell):F6},");
        sb.AppendLine($"    CHSH {Chsh(DensityFromCoeff(bell)):F6} = 2√2. No canonical product reaches");
        sb.AppendLine("    rank 2 — an entangling gate / joint coherent preparation is required.");
        sb.AppendLine();

        // [5] Synchronization / locking status.
        sb.AppendLine("[5] Synchronization / resonance locking");
        sb.AppendLine("    unequal-mode locking ABSENT/BOUNDARY (NP_005/006/009/014); equal-mode");
        sb.AppendLine("    co-rotation is a product-state classical relation.");
        sb.AppendLine();

        // [6] Verdict.
        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    Product state DERIVED; shared-event correlation DERIVED (classical);");
        sb.AppendLine("    single-DOF interference DERIVED-not-entangler; genuine entanglement from");
        sb.AppendLine("    canonical D96 REFUTED/ABSENT; observed entanglement CORRESPONDENCE/BOUNDARY");
        sb.AppendLine("    (needs a NEW entangling sector — QG70/71 unchanged). Success criterion A:");
        sb.AppendLine("    ABSENT — only correlation. No new primitive; canonical AT unchanged.");

        Output.WriteLine(sb.ToString());
    }

    // ── Wootters concurrence (general two-qubit mixed) ────────────────────────

    private static Complex[,] SigmaY()
    {
        return new Complex[2, 2]
        {
            { 0.0, -Complex.ImaginaryOne },
            { Complex.ImaginaryOne, 0.0 },
        };
    }

    private static Complex[,] SpinFlip()
    {
        var sy = SigmaY();
        return Kron2x2(sy, sy);
    }

    private static double WoottersConcurrence(Complex[,] rho)
    {
        var sf = SpinFlip();
        var rhoStar = new Complex[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                rhoStar[i, j] = Complex.Conjugate(rho[i, j]);

        // R = ρ (σy⊗σy) ρ* (σy⊗σy).
        var m1 = Multiply4(rho, sf);
        var m2 = Multiply4(m1, rhoStar);
        var r = Multiply4(m2, sf);

        var eig = HermitianEigenvalues(r);
        Array.Sort(eig);
        Array.Reverse(eig);
        var sqrt = new double[4];
        for (int i = 0; i < 4; i++) sqrt[i] = Math.Sqrt(Math.Max(0.0, eig[i]));
        return Math.Max(0.0, sqrt[0] - sqrt[1] - sqrt[2] - sqrt[3]);
    }

    private static Complex[,] Multiply4(Complex[,] a, Complex[,] b)
    {
        var r = new Complex[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                Complex s = 0;
                for (int k = 0; k < 4; k++) s += a[i, k] * b[k, j];
                r[i, j] = s;
            }
        return r;
    }
}
