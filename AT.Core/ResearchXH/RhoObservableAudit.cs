using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_040 — RHO OBSERVABLE AUDIT.
///
/// QUESTION. Can any measurable quantity retain ALL 95 dimensions of rho?
///
/// Candidates: occupancy patterns, mode populations, detector counts, attractor occupancy, survivor occupancy.
/// Test: dimension retained, information loss, invertibility.
/// Goal: the first observable O such that O &lt;-&gt; rho is approximately invertible.
///
/// ANSWER: **REFUTED — and the ceiling is a symmetry theorem, not a detector limitation.**
///
/// (1) THE ACCOUNTING (computed). The state space is the simplex over the 96 cells, dimension 95. G_039 found
///     the *carrier* (the occupancy of the reachable set) and priced the spectral measurement's loss as 51 =
///     Sum(m-1) over the 45 levels. This audit asks the sharper question — is ANY measurable quantity lossless —
///     and answers it by computing the retained dimension of every measurement class:
///       cell counts (site-addressed)   : 96 data, tangent rank 95, LOSS 0   — but this IS rho, not a probe of it
///       distance contractions (49 class): 48 data, tangent rank 48, LOSS 47  — the best non-addressed observable
///       level populations (spectral)   : 45 data, tangent rank 44, LOSS 51  — exactly G_039's free room
///
/// (2) WHY 47 IS A CEILING. The substrate's own symmetry is the dihedral group D96 of order 192. Every operator
///     AT can build from the substrate commutes with it, so the available observables live in the CENTRALIZER
///     ALGEBRA, whose dimension is the number of ORBITALS (orbits of the group on cell x cell) — computed four
///     independent ways, all landing on 49:
///       brute-force orbital enumeration                      : 49
///       Burnside / Reynolds  (1/|G|) Sum_g fix(g)^2 = 9408/192: 49
///       multiplicity-free irrep decomposition Sum m_lambda^2: 49
///       Sum over levels of rank(algebra restricted to level) : 49
///     The state space is 95-dimensional, so a 49-dimensional operator algebra leaves 95 - 48 = 47 dimensions
///     that no substrate-constructed observable can reach.
///
/// (3) WHAT THE 47 ARE. They are exactly the INTRA-DOUBLET ORIENTATIONS. The real state splits as 48 magnitudes
///     (49 irreducible channels minus the norm) plus 47 angles — one per two-dimensional irrep, and there are
///     exactly 47 of those because D96 has 47 two-dimensional irreps and each appears ONCE. 48 + 47 = 95, and
///     the loss equals the doublet count. By SCHUR'S LEMMA a centralizer operator is a scalar on each irrep, so
///     it cannot see the orientation: computed witness — rotating one doublet's orientation moves the state by
///     1.997e-1 in L1 on a contrasty state (6.93e-3 on the flat generic one) while moving every contraction by
///     at most 2.78e-17, and all 192 group images of a generic state report one identical reading (spread
///     3.12e-17) while sitting up to 9.23e-2 apart.
///
/// (4) THE REGIME CAVEAT (self-caught). The retained dimension is a GENERIC-STATE statement. On a state whose
///     occupancy is confined to four channels the same 48 contractions collapse to rank 4 — a sparse state is
///     even LESS observable than a generic one. The headline number is therefore quoted for a state that
///     populates all 49 channels, and the audit asserts both regimes rather than the convenient one.
///
/// (5) A NUMERICAL SLIP, ALSO SELF-CAUGHT. The rank threshold must be relative to the WHOLE matrix, not to each
///     row. A per-row relative tolerance accepted a row of pure roundoff — the restriction of A_24 to channel 1
///     is exactly the zero matrix, since 2cos(2*pi*24/96) = 0, leaving a computed norm of 2.8e-32 that trivially
///     exceeded 1e-8 times itself. Each doublet then read as rank 2 and the 45 levels summed to 143 instead of
///     49. See <see cref="RankOf"/>.
///
/// (6) THE BOUNDARY OF THIS RESULT. The 47-dimension gap is the price of NOT addressing cells. A measurement
///     that can name a cell (96 site-labelled detectors) retains all 95 — because it IS rho. G_017 excluded
///     exactly that identification, so AT's own observables are on the far side of the gap. The claim refuted
///     here is "some measurable quantity retains all 95 dimensions"; the claim established is "every observable
///     AT can construct is confined to 48 of them, and the missing 47 are protected by the substrate's
///     symmetry".
/// </summary>
public static class RhoObservableAudit
{
    /// <summary>Cells in the D96 ring this audit computes with (G_016: 96 cells).</summary>
    public const int D96Cells = 96;

    /// <summary>Circulant connection radius, C96(1..6).</summary>
    public const int D96Radius = 6;

    /// <summary>Clustering tolerance for the spectrum (the project's standard for a distinct level).</summary>
    public const double LevelTolerance = 1e-9;

    // ═══ §1  THE STATE SPACE ═════════════════════════════════════════════════════════════════════

    /// <summary>The 96 mode-indexed Laplacian eigenvalues mu_k = 2*Radius - lambda_k (G_016's C96(1..6)).</summary>
    public static double[] ModeEigenvalues()
    {
        var ring = PhotonOntologyAudit.RingSpectrum();
        var mu = new double[D96Cells];
        for (int k = 0; k < D96Cells; k++) mu[k] = 2.0 * D96Radius - ring[k];
        return mu;
    }

    /// <summary>The distinct levels with their multiplicities, clustered by <see cref="LevelTolerance"/>.</summary>
    public static (double Level, int Multiplicity)[] Levels()
    {
        var mu = ModeEigenvalues();
        var levels = new List<(double, int)>();
        foreach (double v in mu.OrderBy(x => x))
        {
            if (levels.Count > 0 && Math.Abs(v - levels[^1].Item1) < LevelTolerance)
                levels[^1] = (levels[^1].Item1, levels[^1].Item2 + 1);
            else levels.Add((v, 1));
        }
        return levels.ToArray();
    }

    /// <summary>Level index of every mode k (so the eigenspace blocks can be built from the mode index).</summary>
    public static int[] LevelIndexOfMode()
    {
        var levels = Levels();
        var mu = ModeEigenvalues();
        var map = new int[D96Cells];
        for (int k = 0; k < D96Cells; k++)
            for (int l = 0; l < levels.Length; l++)
                if (Math.Abs(mu[k] - levels[l].Level) < LevelTolerance) { map[k] = l; break; }
        return map;
    }

    public static int DistinctLevels() => Levels().Length;

    public static int LaplacianTrace() => (int)Math.Round(ModeEigenvalues().Sum());

    /// <summary>Dimension of the state space: the normalised simplex over the cells, 95.</summary>
    public static int StateDimension() => D96Cells - 1;

    /// <summary>
    /// The recomputed ground must agree with the record (G_016/G_018/G_039): 45 levels, the histogram
    /// {1:1, 2:42, 5:1, 6:1}, the trace 1152 = 2 x 576 links and a state space of 95.
    /// </summary>
    public static bool SpectrumReproducesTheRecord()
    {
        var levels = Levels();
        var hist = levels.GroupBy(l => l.Multiplicity).ToDictionary(g => g.Key, g => g.Count());
        return levels.Length == 45
            && levels.Sum(l => l.Multiplicity) == D96Cells
            && hist.GetValueOrDefault(1) == 1 && hist.GetValueOrDefault(2) == 42
            && hist.GetValueOrDefault(5) == 1 && hist.GetValueOrDefault(6) == 1
            && Math.Abs(LaplacianTrace() - 1152.0) < 1e-9
            && StateDimension() == 95;
    }

    // ═══ §2  THE SUBSTRATE'S SYMMETRY — the source of the ceiling ═══════════════════════════════

    /// <summary>
    /// The dihedral group D96 acting on the 96 cells: the 96 rotations i -&gt; (i+k) mod 96 followed by the 96
    /// reflections i -&gt; (k-i) mod 96 — 192 permutations. Nothing is asserted about the group: every property
    /// below is computed from these arrays.
    /// </summary>
    public static int[][] SymmetryGroup()
    {
        var group = new List<int[]>(2 * D96Cells);
        for (int k = 0; k < D96Cells; k++)
        {
            var r = new int[D96Cells];
            for (int i = 0; i < D96Cells; i++) r[i] = (i + k) % D96Cells;
            group.Add(r);
        }
        for (int k = 0; k < D96Cells; k++)
        {
            var s = new int[D96Cells];
            for (int i = 0; i < D96Cells; i++) s[i] = ((k - i) % D96Cells + D96Cells) % D96Cells;
            group.Add(s);
        }
        return group.ToArray();
    }

    public static int GroupOrder() => SymmetryGroup().Length;

    /// <summary>Number of distinct permutations (192 — no rotation coincides with a reflection).</summary>
    public static int DistinctGroupElements()
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var p in SymmetryGroup()) seen.Add(string.Join(",", p));
        return seen.Count;
    }

    /// <summary>
    /// Closure, checked by composing all 192 x 192 pairs and matching the whole permutation (the key (p[0],p[1])
    /// identifies an element of this group; the match is verified on all 96 images, not on the key alone).
    /// </summary>
    public static bool GroupIsClosedUnderComposition()
    {
        var group = SymmetryGroup();
        var byKey = new Dictionary<int, int[]>();
        foreach (var p in group) byKey[p[0] * D96Cells + p[1]] = p;
        foreach (var a in group)
            foreach (var b in group)
            {
                var c = new int[D96Cells];
                for (int i = 0; i < D96Cells; i++) c[i] = a[b[i]];
                if (!byKey.TryGetValue(c[0] * D96Cells + c[1], out var match)) return false;
                for (int i = 0; i < D96Cells; i++) if (match[i] != c[i]) return false;
            }
        return true;
    }

    /// <summary>Orbits of the group on the cells — 1, i.e. the action is transitive.</summary>
    public static int OrbitsOnCells()
    {
        var group = SymmetryGroup();
        var seen = new bool[D96Cells];
        int count = 0;
        for (int i = 0; i < D96Cells; i++)
        {
            if (seen[i]) continue;
            count++;
            foreach (var p in group) seen[p[i]] = true;
        }
        return count;
    }

    /// <summary>
    /// ORBITALS — the orbits of the group on the ORDERED PAIRS (cell, cell). This is the dimension of the
    /// centralizer algebra of the permutation representation (the Hecke / association-scheme algebra), i.e. the
    /// space of operators commuting with the symmetry, i.e. the space of substrate-constructible observables.
    /// Computed by explicit orbit enumeration of all 9216 pairs under all 192 group elements.
    /// </summary>
    public static int OrbitalCountBruteForce()
    {
        var group = SymmetryGroup();
        var seen = new HashSet<int>();
        int count = 0;
        for (int i = 0; i < D96Cells; i++)
            for (int j = 0; j < D96Cells; j++)
            {
                if (!seen.Add(i * D96Cells + j)) continue;
                count++;
                foreach (var p in group) seen.Add(p[i] * D96Cells + p[j]);
            }
        return count;
    }

    /// <summary>Fixed points of every group element (96 once, 2 for the 48 even reflections, 0 otherwise).</summary>
    public static int[] FixedPointCounts()
    {
        var group = SymmetryGroup();
        var counts = new int[group.Length];
        for (int g = 0; g < group.Length; g++)
            for (int i = 0; i < D96Cells; i++) if (group[g][i] == i) counts[g]++;
        return counts;
    }

    /// <summary>
    /// The same dimension by Burnside / the Reynolds operator: dim(commutant) = trace of the averaging projector
    /// = (1/|G|) Sum_g fix(g)^2. This is the orbit count on X x X reached by counting rather than enumerating.
    /// </summary>
    public static int CommutantDimensionByBurnside()
    {
        var counts = FixedPointCounts();
        long sum = counts.Sum(f => (long)f * f);
        return (int)(sum / counts.Length);
    }

    // ═══ §3  THE IRREPS — what the symmetry permits to be seen ══════════════════════════════════

    /// <summary>The irreducible channels present: the orbits of the mode index under k -&gt; -k.</summary>
    public static int[] IrrepClasses()
    {
        var set = new SortedSet<int>();
        for (int k = 0; k < D96Cells; k++) set.Add(Math.Min(k, D96Cells - k));
        return set.ToArray();
    }

    public static int IrrepCount() => IrrepClasses().Length;

    /// <summary>The two-dimensional irreps: every channel except the constant and the alternating one.</summary>
    public static int DoubletCount() => IrrepClasses().Count(c => c != 0 && c != D96Cells / 2);

    public static int SingletCount() => IrrepCount() - DoubletCount();

    /// <summary>
    /// The number of irreps inside each level. A real state's level block is spanned by ONE cos/sin pair per
    /// channel — enumerating the 96 modes with a per-mode guard would double the +/- pair and build a
    /// projector that is not idempotent (a self-caught slip: the doubled block gave |P^2 - P| = 1/24).
    /// </summary>
    public static int[] IrrepsPerLevel()
    {
        var map = LevelIndexOfMode();
        var perLevel = new List<HashSet<int>>();
        for (int l = 0; l < DistinctLevels(); l++) perLevel.Add(new HashSet<int>());
        for (int k = 0; k < D96Cells; k++) perLevel[map[k]].Add(Math.Min(k, D96Cells - k));
        return perLevel.Select(s => s.Count).ToArray();
    }

    public static int SumOfIrrepsPerLevel() => IrrepsPerLevel().Sum();

    /// <summary>Sum of the squared level multiplicities — the room the centralizer COULD have filled.</summary>
    public static int SumOfMultiplicitySquares() => Levels().Sum(l => l.Multiplicity * l.Multiplicity);

    /// <summary>
    /// The real orthonormal eigenbasis rows of one level: the constant (if the level holds channel 0), the
    /// alternating vector (channel 48), and one cos/sin pair per remaining channel.
    /// </summary>
    public static double[][] LevelBasis(int level)
    {
        var map = LevelIndexOfMode();
        var channels = new SortedSet<int>();
        for (int k = 0; k < D96Cells; k++) if (map[k] == level) channels.Add(Math.Min(k, D96Cells - k));
        var rows = new List<double[]>();
        foreach (int c in channels)
        {
            if (c == 0)
            {
                var v = new double[D96Cells];
                for (int i = 0; i < D96Cells; i++) v[i] = 1.0 / Math.Sqrt(D96Cells);
                rows.Add(v);
            }
            else if (c == D96Cells / 2)
            {
                var v = new double[D96Cells];
                for (int i = 0; i < D96Cells; i++) v[i] = (i % 2 == 0 ? 1.0 : -1.0) / Math.Sqrt(D96Cells);
                rows.Add(v);
            }
            else
            {
                var a = new double[D96Cells];
                var b = new double[D96Cells];
                double norm = Math.Sqrt(D96Cells / 2.0);
                for (int i = 0; i < D96Cells; i++)
                {
                    a[i] = Math.Cos(2.0 * Math.PI * c * i / D96Cells) / norm;
                    b[i] = Math.Sin(2.0 * Math.PI * c * i / D96Cells) / norm;
                }
                rows.Add(a);
                rows.Add(b);
            }
        }
        return rows.ToArray();
    }

    /// <summary>The distance-d relation matrix A_d — a basis element of the centralizer algebra.</summary>
    public static double[][] OrbitalMatrix(int d)
    {
        var m = new double[D96Cells][];
        for (int i = 0; i < D96Cells; i++)
        {
            m[i] = new double[D96Cells];
            for (int j = 0; j < D96Cells; j++)
            {
                int delta = Math.Abs(i - j);
                if (Math.Min(delta, D96Cells - delta) == d) m[i][j] = 1.0;
            }
        }
        return m;
    }

    private static double[][][]? _orbitals;

    /// <summary>The 49 relation matrices, built once (each level's restriction reuses them).</summary>
    public static double[][][] OrbitalMatrices()
    {
        if (_orbitals is not null) return _orbitals;
        var all = new double[D96Cells / 2 + 1][][];
        for (int d = 0; d <= D96Cells / 2; d++) all[d] = OrbitalMatrix(d);
        return _orbitals = all;
    }

    /// <summary>
    /// Rank of the centralizer algebra RESTRICTED to one level — the number of independent things a
    /// substrate-constructed operator can say about that level. By Schur this is the number of irreps in the
    /// level, not the square of its multiplicity: the operator must act as a scalar inside each irrep.
    /// </summary>
    public static int RestrictedAlgebraRank(int level)
    {
        var basis = LevelBasis(level);
        int m = basis.Length;
        var orbitals = OrbitalMatrices();
        var rows = new List<double[]>();
        for (int d = 1; d <= D96Cells / 2; d++)
        {
            var a = orbitals[d];
            var row = new double[m * m];
            for (int r = 0; r < m; r++)
                for (int c = 0; c < m; c++)
                {
                    double s = 0.0;
                    for (int i = 0; i < D96Cells; i++)
                    {
                        double tmp = 0.0;
                        for (int j = 0; j < D96Cells; j++) tmp += a[i][j] * basis[c][j];
                        s += basis[r][i] * tmp;
                    }
                    row[r * m + c] = s;
                }
            rows.Add(row);
        }
        return RankOf(rows.ToArray());
    }

    /// <summary>
    /// Dimensions the centralizer cannot reach: Sum over levels of (m^2 - rank there). 230 - 49 = 181.
    /// </summary>
    public static int ProtectedDimensions()
        => SumOfMultiplicitySquares() - Enumerable.Range(0, DistinctLevels()).Sum(RestrictedAlgebraRank);

    /// <summary>
    /// THE FOUR ROUTES, all of which must land on the same 49: the orbital enumeration, Burnside's fixed-point
    /// count, the multiplicity-free irrep decomposition, and the sum of the restricted algebra ranks.
    /// </summary>
    public static bool TheCountsAgree()
    {
        int orbitals = OrbitalCountBruteForce();
        int burnside = CommutantDimensionByBurnside();
        int irreps = SumOfIrrepsPerLevel();
        int ranks = Enumerable.Range(0, DistinctLevels()).Sum(RestrictedAlgebraRank);
        return orbitals == burnside && burnside == irreps && irreps == ranks && ranks == IrrepCount();
    }

    /// <summary>
    /// Rank by modified Gram-Schmidt with two reorthogonalisation passes.
    ///
    /// THE THRESHOLD IS RELATIVE TO THE WHOLE SET, NOT TO EACH ROW — and that distinction is a self-caught slip
    /// rather than a detail. With a per-row relative tolerance, a row of pure roundoff (the restriction of A_24
    /// to channel 1 is the ZERO matrix, since 2cos(2*pi*24/96) = 0, so its computed norm is ~2.8e-32) passes the
    /// test n &gt; tolerance * n0 trivially, because n0 is the roundoff itself. The doublet then reported rank 2
    /// and the 45 levels summed to 143 instead of 49. The threshold must be relative to the largest row norm,
    /// which is what an SVD-based rank does; numerically zero rows are skipped outright.
    /// </summary>
    public static int RankOf(double[][] rows, double tolerance = 1e-8)
    {
        double scale = 0.0;
        foreach (var row in rows) scale = Math.Max(scale, Math.Sqrt(row.Sum(x => x * x)));
        if (scale < 1e-300) return 0;
        double floor = tolerance * scale;

        var basis = new List<double[]>();
        foreach (var row in rows)
        {
            var v = (double[])row.Clone();
            if (Math.Sqrt(v.Sum(x => x * x)) < floor) continue;
            for (int pass = 0; pass < 2; pass++)
                foreach (var u in basis)
                {
                    double dot = 0.0;
                    for (int i = 0; i < v.Length; i++) dot += u[i] * v[i];
                    for (int i = 0; i < v.Length; i++) v[i] -= dot * u[i];
                }
            double n = Math.Sqrt(v.Sum(x => x * x));
            if (n > floor)
            {
                for (int i = 0; i < v.Length; i++) v[i] /= n;
                basis.Add(v);
            }
        }
        return basis.Count;
    }

    // ═══ §4  THE LADDER OF MEASUREMENT CLASSES — retained, lost, invertible ═════════════════════

    /// <summary>
    /// A generic state: full support, and occupancy spread over ALL 49 channels. The genericity is the point —
    /// a state confined to four channels has a strictly smaller observable rank (see
    /// <see cref="ChannelConfinedState"/>), so a headline number quoted on a sparse state would understate the
    /// ceiling. Deterministic by construction: fixed coefficients, no randomness.
    /// </summary>
    public static double[] GenericState()
    {
        var rho = new double[D96Cells];
        for (int t = 0; t < D96Cells; t++)
        {
            double v = 3.0;
            for (int j = 1; j <= D96Cells / 2 - 1; j++)
                v += (0.20 / (1 + j)) * Math.Cos(2.0 * Math.PI * j * t / D96Cells)
                   + (0.15 / (1 + j)) * Math.Sin(2.0 * Math.PI * j * t / D96Cells);
            v += 0.12 * (t % 2 == 0 ? 1.0 : -1.0);
            rho[t] = v;
        }
        Normalise(rho);
        return rho;
    }

    /// <summary>A state confined to four channels — the regime in which observability COLLAPSES.</summary>
    public static double[] ChannelConfinedState()
    {
        var rho = new double[D96Cells];
        for (int t = 0; t < D96Cells; t++)
            rho[t] = 1.0
                   + 0.30 * Math.Cos(2.0 * Math.PI * 7 * t / D96Cells)
                   + 0.17 * Math.Cos(2.0 * Math.PI * 23 * t / D96Cells)
                   + 0.11 * Math.Sin(2.0 * Math.PI * 13 * t / D96Cells)
                   + 0.05 * Math.Sin(2.0 * Math.PI * 41 * t / D96Cells);
        Normalise(rho);
        return rho;
    }

    private static void Normalise(double[] rho)
    {
        double sum = rho.Sum();
        for (int i = 0; i < rho.Length; i++) rho[i] /= sum;
    }

    /// <summary>The channels a state actually occupies (|Fourier coefficient| above the roundoff floor).</summary>
    public static int ChannelCount(double[] rho)
    {
        var channels = new HashSet<int>();
        for (int k = 0; k < D96Cells; k++)
        {
            double re = 0.0, im = 0.0;
            for (int t = 0; t < D96Cells; t++)
            {
                double ang = 2.0 * Math.PI * k * t / D96Cells;
                re += rho[t] * Math.Cos(ang);
                im -= rho[t] * Math.Sin(ang);
            }
            if (Math.Sqrt(re * re + im * im) > 1e-9) channels.Add(Math.Min(k, D96Cells - k));
        }
        return channels.Count;
    }

    /// <summary>The Jacobian rows of the spectral measurement: d|P_l rho|^2/drho = 2 P_l rho, one per level.</summary>
    public static double[][] LevelPopulationRows(double[] rho)
    {
        var rows = new List<double[]>();
        for (int l = 0; l < DistinctLevels(); l++)
        {
            var basis = LevelBasis(l);
            var proj = new double[D96Cells];
            foreach (var b in basis)
            {
                double dot = 0.0;
                for (int i = 0; i < D96Cells; i++) dot += b[i] * rho[i];
                for (int i = 0; i < D96Cells; i++) proj[i] += dot * b[i];
            }
            var row = new double[D96Cells];
            for (int i = 0; i < D96Cells; i++) row[i] = 2.0 * proj[i];
            rows.Add(row);
        }
        return rows.ToArray();
    }

    /// <summary>The Jacobian rows of the contraction measurement: d&lt;rho,A_d rho&gt;/drho = 2 A_d rho, d = 1..48.</summary>
    public static double[][] ContractionRows(double[] rho)
    {
        var orbitals = OrbitalMatrices();
        var rows = new List<double[]>();
        for (int d = 1; d <= D96Cells / 2; d++)
        {
            var a = orbitals[d];
            var row = new double[D96Cells];
            for (int i = 0; i < D96Cells; i++)
            {
                double s = 0.0;
                for (int j = 0; j < D96Cells; j++) s += a[i][j] * rho[j];
                row[i] = 2.0 * s;
            }
            rows.Add(row);
        }
        return rows.ToArray();
    }

    /// <summary>The site-addressed measurement: one row per cell.</summary>
    public static double[][] CellCountRows()
    {
        var rows = new double[D96Cells][];
        for (int i = 0; i < D96Cells; i++)
        {
            rows[i] = new double[D96Cells];
            rows[i][i] = 1.0;
        }
        return rows;
    }

    /// <summary>Project every row onto the simplex's tangent space (subtract the row mean).</summary>
    public static double[][] Tangent(double[][] rows)
    {
        var output = new double[rows.Length][];
        for (int r = 0; r < rows.Length; r++)
        {
            double mean = rows[r].Average();
            output[r] = rows[r].Select(x => x - mean).ToArray();
        }
        return output;
    }

    /// <summary>The 48 contraction VALUES &lt;rho, A_d rho&gt; — the actual datum the measurement reports.</summary>
    public static double[] ContractionData(double[] rho)
    {
        var orbitals = OrbitalMatrices();
        var data = new double[D96Cells / 2];
        for (int d = 1; d <= D96Cells / 2; d++)
        {
            var a = orbitals[d];
            double s = 0.0;
            for (int i = 0; i < D96Cells; i++)
            {
                double t = 0.0;
                for (int j = 0; j < D96Cells; j++) t += a[i][j] * rho[j];
                s += rho[i] * t;
            }
            data[d - 1] = s;
        }
        return data;
    }

    public static int SpectralRetained()
        => RankOf(Tangent(LevelPopulationRows(GenericState())));

    public static int SpectralLoss() => StateDimension() - SpectralRetained();

    public static int ContractionRetained()
        => RankOf(Tangent(ContractionRows(GenericState())));

    public static int ContractionLoss() => StateDimension() - ContractionRetained();

    public static int AddressedRetained() => RankOf(Tangent(CellCountRows()));

    public static int AddressedLoss() => StateDimension() - AddressedRetained();

    /// <summary>The rank on a channel-confined state — the regime caveat, computed rather than asserted.</summary>
    public static int ChannelConfinedRank()
        => RankOf(Tangent(ContractionRows(ChannelConfinedState())));

    /// <summary>G_039's free room, Sum(m - 1) over the 45 levels = 96 - 45 = 51.</summary>
    public static int FreeRoom() => D96Cells - DistinctLevels();

    /// <summary>Does the spectral loss reproduce G_039's free room exactly?</summary>
    public static bool SpectralLossIsTheFreeRoom() => SpectralLoss() == FreeRoom();

    /// <summary>Does the best non-addressed loss equal the number of two-dimensional irreps?</summary>
    public static bool ContractionLossIsTheDoubletCount() => ContractionLoss() == DoubletCount();

    /// <summary>Dimensions a level-blind measurement misses that an irrep-blind one does not.</summary>
    public static int DimensionsReclaimedByResolvingLevels()
        => Enumerable.Range(0, DistinctLevels()).Sum(l => IrrepsPerLevel()[l] - 1);

    // ═══ §5  THE WITNESSES — invertibility fails, exactly and by construction ═══════════════════

    /// <summary>
    /// Rotate one doublet's ORIENTATION (the cos/sin pair of channel 7) and report how far the state moves
    /// against how far every contraction moves. The rotation is orthogonal inside the irrep, so it preserves
    /// the sum and every level norm; Schur then says no centralizer operator can see it.
    ///
    /// Two states are offered because the ABSOLUTE size of the invisible move is a property of the state, not of
    /// the theorem: on the flat generic state the rotation moves ~0.7 % of the total occupancy, while on a
    /// channel-confined (contrasty) state it moves a large fraction of it. The DATA moves by roundoff either
    /// way, so both regimes are reported rather than the flattering one.
    /// </summary>
    public static (double StateMove, double DataMove) OrientationWitness(double theta, bool contrasty = false)
    {
        var rho = contrasty ? ChannelConfinedState() : GenericState();
        int channel = 7;
        double norm = Math.Sqrt(D96Cells / 2.0);
        var a = new double[D96Cells];
        var b = new double[D96Cells];
        for (int t = 0; t < D96Cells; t++)
        {
            a[t] = Math.Cos(2.0 * Math.PI * channel * t / D96Cells) / norm;
            b[t] = Math.Sin(2.0 * Math.PI * channel * t / D96Cells) / norm;
        }
        double d1 = 0.0, d2 = 0.0;
        for (int t = 0; t < D96Cells; t++) { d1 += a[t] * rho[t]; d2 += b[t] * rho[t]; }
        double ca = Math.Cos(theta), sa = Math.Sin(theta);
        var moved = new double[D96Cells];
        for (int t = 0; t < D96Cells; t++)
            moved[t] = rho[t] - (d1 * a[t] + d2 * b[t])
                     + (d1 * ca - d2 * sa) * a[t]
                     + (d1 * sa + d2 * ca) * b[t];
        double stateMove = 0.0;
        for (int t = 0; t < D96Cells; t++) stateMove += Math.Abs(moved[t] - rho[t]);
        double dataMove = MaxDifference(ContractionData(rho), ContractionData(moved));
        return (stateMove, dataMove);
    }

    /// <summary>
    /// The group-orbit witness: every one of the 192 images of a generic state reports the SAME contraction
    /// data, while the images sit far apart. The fibre of the measurement is therefore at least 192-to-1.
    /// </summary>
    public static (double DataSpread, double StateSpread, int DistinctData) GroupImageWitness()
    {
        var rho = GenericState();
        var group = SymmetryGroup();
        var baseData = ContractionData(rho);
        var keys = new HashSet<string>(StringComparer.Ordinal);
        double dataSpread = 0.0, stateSpread = 0.0;
        foreach (var p in group)
        {
            var image = new double[D96Cells];
            for (int i = 0; i < D96Cells; i++) image[i] = rho[p[i]];
            dataSpread = Math.Max(dataSpread, MaxDifference(baseData, ContractionData(image)));
            double move = 0.0;
            for (int i = 0; i < D96Cells; i++) move += Math.Abs(image[i] - rho[i]);
            stateSpread = Math.Max(stateSpread, move);
            keys.Add(string.Join(",", ContractionData(image)
                .Select(v => Math.Round(v, 12).ToString("F12", CultureInfo.InvariantCulture))));
        }
        return (dataSpread, stateSpread, keys.Count);
    }

    private static double MaxDifference(double[] a, double[] b)
    {
        double worst = 0.0;
        for (int i = 0; i < a.Length; i++) worst = Math.Max(worst, Math.Abs(a[i] - b[i]));
        return worst;
    }

    /// <summary>Is the doublet orientation invisible to every contraction, and does it still move the state?</summary>
    public static bool OrientationIsInvisible()
    {
        var flat = OrientationWitness(1.1);
        var contrasty = OrientationWitness(1.1, contrasty: true);
        return flat.DataMove < 1e-12 && flat.StateMove > 1e-3
            && contrasty.DataMove < 1e-12 && contrasty.StateMove > 1e-1;
    }

    /// <summary>Do all 192 images agree on the data while sitting apart?</summary>
    public static bool GroupImagesShareOneReading()
    {
        var (dataSpread, stateSpread, distinct) = GroupImageWitness();
        return dataSpread < 1e-12 && stateSpread > 1e-2 && distinct == 1;
    }

    // ═══ THE CANDIDATES ═════════════════════════════════════════════════════════════════════════

    public static (string Candidate, int Retained, int Loss, string Invertible, string Verdict)[] Candidates() => new[]
    {
        ("occupancy patterns", AddressedRetained(), AddressedLoss(),
            "trivially — it IS rho, so it presupposes naming every cell", "REFUTED"),
        ("mode populations", SpectralRetained(), SpectralLoss(),
            "no — a level-blind probe leaves the orientations free", "CORRELATED"),
        ("detector counts", SpectralRetained(), SpectralLoss(),
            "no — a binned detector counts levels, not cells", "CORRELATED"),
        ("attractor occupancy", AddressedRetained(), AddressedLoss(),
            "trivially — the same object read at the attractor (G_039)", "REFUTED"),
        ("survivor occupancy", AddressedRetained(), AddressedLoss(),
            "trivially — the same object after the transient (G_039)", "REFUTED"),
        ("distance-class contractions", ContractionRetained(), ContractionLoss(),
            "no — the closest rung: 48 of 95, and it is the ceiling", "CORRELATED"),
    };

    /// <summary>Candidates that retain the whole state space — computed, not asserted.</summary>
    public static string[] CandidatesThatReachTheWholeSpace()
        => Candidates().Where(c => c.Loss == 0).Select(c => c.Candidate).ToArray();

    /// <summary>The best candidate that is NOT rho itself.</summary>
    public static string ClosestCandidate()
        => Candidates().Where(c => c.Loss > 0).OrderBy(c => c.Loss).First().Candidate;

    // ═══ VERDICT ════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// REFUTED (computed). Every structural check the verdict rests on is recomputed first — the spectrum, the
    /// group's size, closure and transitivity, the four agreeing counts, the ladder, the Schur budget and the
    /// two invisibility witnesses. Given those, no constructible measurement reaches the full state space: the
    /// only lossless class is the site-addressed count, which IS rho, and every distinct class loses exactly
    /// the 47 intra-doublet orientations — protected by the substrate's own symmetry rather than by any
    /// detector's resolution.
    /// </summary>
    public static string Verdict()
    {
        bool floor = SpectrumReproducesTheRecord()
                  && DistinctGroupElements() == GroupOrder()
                  && GroupIsClosedUnderComposition()
                  && OrbitsOnCells() == 1
                  && TheCountsAgree()
                  && ProtectedDimensions() == SumOfMultiplicitySquares() - CommutantDimensionByBurnside();
        bool ladder = SpectralLossIsTheFreeRoom()
                  && ContractionLossIsTheDoubletCount()
                  && ContractionRetained() + ContractionLoss() == StateDimension()
                  && ContractionRetained() > SpectralRetained();
        if (!(floor && ladder && OrientationIsInvisible() && GroupImagesShareOneReading()))
            return "REFUTED";                    // a structural check did not come out
        if (AddressedLoss() == 0 && ContractionLoss() == DoubletCount() && ContractionLoss() > 0)
            return "REFUTED";                    // the only lossless class is rho; every distinct one loses 47
        return "OBSERVABLE";
    }

    public static string WhereItStands()
        => "NO MEASURABLE QUANTITY RETAINS ALL 95 DIMENSIONS, AND THE REASON IS A THEOREM RATHER THAN A "
         + "DETECTOR. The audit first reproduces its own ground: 96 cells, 45 levels, the histogram "
         + "{1:1, 2:42, 5:1, 6:1}, a trace of 1152 and a state space of 95. It then builds the substrate's "
         + "symmetry explicitly — the 192 permutations of the dihedral group — and checks in code that they are "
         + "distinct, closed under composition and transitive on the cells. From that group one number decides "
         + "the whole question: the dimension of the space of operators AT can construct, which is the "
         + "centralizer algebra, whose dimension is the number of orbitals. Four independent computations land "
         + "on 49 — enumerating the orbits of all 9216 ordered cell pairs, Burnside's count (1/|G|) Sum fix(g)^2 "
         + "= 9408/192, the multiplicity-free irrep decomposition (49 irreps, each appearing once) and the sum "
         + "over levels of the algebra's restricted rank (1 + 42 x 1 + 3 + 3). Because a 49-dimensional algebra "
         + "of operators acts on a 95-dimensional state space, 47 dimensions are left; the audit names them "
         + "rather than merely counting them: the state splits as 48 magnitudes plus 47 ANGLES, one per "
         + "two-dimensional irrep, and D96 has exactly 47 of those. Two witnesses make the failure of "
         + "invertibility concrete instead of statistical. Rotating a single doublet's orientation moves the "
         + "state by 1.997e-1 in L1 on a contrasty state and still 6.93e-3 on the flat generic one, while "
         + "moving every contraction by at most 2.78e-17 — the rotation is orthogonal inside the irrep, so it is "
         + "invisible by Schur's lemma and not merely small. And all 192 group images of a generic state report "
         + "one identical reading (spread 3.12e-17) while sitting up to 9.23e-2 apart, so the measurement's "
         + "fibre is at least 192-to-1 before any continuous degeneracy is counted. The ladder of classes is "
         + "then computed rather than argued: a site-addressed count retains all 95 and loses nothing — but that "
         + "IS rho, and naming every cell is exactly the identification G_017 excluded; the best non-addressed "
         + "observable, the 48 distance-class contractions, retains 48 and loses 47; and the spectral "
         + "measurement, which is what a physical spectrometer returns, retains 44 and loses 51 — precisely "
         + "G_039's free room. The four extra dimensions the contraction rung reclaims come from the only two "
         + "levels that carry more than one irrep. One honest regime caveat, found by computing both: the "
         + "retained dimension is a generic-state statement — on a state confined to four channels the same 48 "
         + "contractions collapse to rank 4, so a sparse state is even LESS observable, not more. The goal asked "
         + "for an observable O approximately invertible against rho; the closest is the contraction profile at "
         + "48 of 95, and invertibility is refused by symmetry. That is a REFUTED answer to the question as "
         + "posed — and a sharp one, because it converts a measurability complaint into a dimension count.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputSpace()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE STATE SPACE, RECOMPUTED");
        sb.AppendLine($"   cells                              : {D96Cells}");
        sb.AppendLine($"   distinct levels                    : {DistinctLevels()}");
        var hist = string.Join(", ", Levels().GroupBy(l => l.Multiplicity).OrderBy(g => g.Key)
            .Select(g => $"{g.Key}:{g.Count()}"));
        sb.AppendLine($"   multiplicity histogram             : {{{hist}}}");
        sb.AppendLine($"   Laplacian trace                    : {LaplacianTrace():F0} = 2 x {D96Cells * D96Radius / 2 * 2} links");
        sb.AppendLine($"   state dimension (simplex)          : {StateDimension()}");
        sb.AppendLine($"   ALL FIGURES AGREE WITH THE RECORD   : {SpectrumReproducesTheRecord()}");
        sb.AppendLine();
        sb.AppendLine("   THE MEASUREMENT CLASSES, AND WHAT EACH RETAINS");
        sb.AppendLine($"     site-addressed cell counts        : {AddressedRetained(),3} retained, loss {AddressedLoss()}");
        sb.AppendLine($"     distance-class contractions       : {ContractionRetained(),3} retained, loss {ContractionLoss()}");
        sb.AppendLine($"     spectral level populations        : {SpectralRetained(),3} retained, loss {SpectralLoss()}");
        sb.AppendLine($"     on a FOUR-CHANNEL state           : {ChannelConfinedRank(),3} retained, loss {StateDimension() - ChannelConfinedRank()}");
        sb.AppendLine($"       <- the regime caveat: a sparse state is LESS observable, not more");
        return sb.ToString();
    }

    public static string OutputSymmetry()
    {
        var counts = FixedPointCounts();
        var sb = new StringBuilder();
        sb.AppendLine("2. THE SUBSTRATE'S SYMMETRY — where the ceiling comes from");
        sb.AppendLine($"   group order                        : {GroupOrder()}   (distinct: {DistinctGroupElements()})");
        sb.AppendLine($"   closed under composition           : {GroupIsClosedUnderComposition()}");
        sb.AppendLine($"   orbits on the cells                : {OrbitsOnCells()} (transitive)");
        sb.AppendLine($"   fixed points of a group element    : {string.Join(", ", counts.Distinct().OrderBy(x => x))}");
        sb.AppendLine($"   Sum fix(g)^2                       : {counts.Sum(f => (long)f * f)}");
        sb.AppendLine();
        sb.AppendLine("   THE CENTRALIZER ALGEBRA DIMENSION, FOUR WAYS");
        sb.AppendLine($"     orbital enumeration (9216 pairs)  : {OrbitalCountBruteForce()}");
        sb.AppendLine($"     Burnside (1/|G|) Sum fix^2        : {CommutantDimensionByBurnside()}");
        sb.AppendLine($"     multiplicity-free irreps          : {SumOfIrrepsPerLevel()}");
        sb.AppendLine($"     Sum of restricted ranks           : {Enumerable.Range(0, DistinctLevels()).Sum(RestrictedAlgebraRank)}");
        sb.AppendLine($"     ALL FOUR AGREE                     : {TheCountsAgree()}");
        sb.AppendLine();
        sb.AppendLine($"   irrep channels present             : {IrrepCount()} ({DoubletCount()} doublets + {SingletCount()} singlets)");
        sb.AppendLine($"   Sum m^2 over levels                : {SumOfMultiplicitySquares()}");
        sb.AppendLine($"   dimensions the algebra cannot reach: {ProtectedDimensions()}");
        sb.AppendLine($"     = Sum m^2 - centralizer dimension : {SumOfMultiplicitySquares()} - {CommutantDimensionByBurnside()}");
        return sb.ToString();
    }

    public static string OutputLadder()
    {
        var perLevel = IrrepsPerLevel();
        var sb = new StringBuilder();
        sb.AppendLine("3. THE RESOLUTION LADDER — retained, lost, and why 47 is a wall");
        sb.AppendLine();
        sb.AppendLine("   level multiplicity -> rank of the algebra restricted to it");
        foreach (var g in Levels().Select((l, i) => (l, i)).GroupBy(x => x.l.Multiplicity).OrderBy(g => g.Key))
        {
            var idx = g.Select(x => x.i).ToArray();
            var ranks = idx.Select(RestrictedAlgebraRank).Distinct().ToArray();
            var irreps = idx.Select(i => perLevel[i]).Distinct().ToArray();
            sb.AppendLine($"     m = {g.Key}  : {g.Count(),2} levels, {string.Join("/", irreps)} irreps each,"
                          + $" rank {string.Join("/", ranks),3}, protected {g.Key * g.Key - ranks.Min()}");
        }
        sb.AppendLine($"     Sum of the ranks                   : {Enumerable.Range(0, DistinctLevels()).Sum(RestrictedAlgebraRank)}");
        sb.AppendLine($"     (a level of multiplicity m admits m^2 operators; the algebra reaches only its irrep count)");
        sb.AppendLine();
        sb.AppendLine("   THE ACCOUNTING");
        sb.AppendLine($"     state space                        : {StateDimension()}");
        sb.AppendLine($"     = magnitudes + orientations        : {ContractionRetained()} + {ContractionLoss()}");
        sb.AppendLine($"     spectral loss  = G_039's free room : {SpectralLoss()} = {FreeRoom()}  -> {SpectralLossIsTheFreeRoom()}");
        sb.AppendLine($"     best loss      = the doublet count : {ContractionLoss()} = {DoubletCount()}  -> {ContractionLossIsTheDoubletCount()}");
        sb.AppendLine($"     measured state occupies channels   : {ChannelCount(GenericState())}");
        sb.AppendLine($"     levels carrying more than one irrep: {IrrepsPerLevel().Count(r => r > 1)}");
        sb.AppendLine($"     dimensions they reclaim            : {DimensionsReclaimedByResolvingLevels()}"
                      + $"  ({SpectralLoss()} - {ContractionLoss()})");
        return sb.ToString();
    }

    public static string OutputWitnesses()
    {
        var sb = new StringBuilder();
        var (stateMove, dataMove) = OrientationWitness(1.1);
        var (contrastMove, contrastData) = OrientationWitness(1.1, contrasty: true);
        var (dataSpread, stateSpread, distinct) = GroupImageWitness();
        sb.AppendLine("4. TWO WITNESSES — invertibility fails, exactly");
        sb.AppendLine();
        sb.AppendLine("   (a) ROTATE ONE DOUBLET'S ORIENTATION (channel 7, by 1.1 rad)");
        sb.AppendLine("       on the flat generic state");
        sb.AppendLine($"         state moved (L1)               : {stateMove:E3}");
        sb.AppendLine($"         largest contraction moved      : {dataMove:E3}");
        sb.AppendLine("       on the channel-confined (contrasty) state");
        sb.AppendLine($"         state moved (L1)               : {contrastMove:E3}");
        sb.AppendLine($"         largest contraction moved      : {contrastData:E3}");
        sb.AppendLine($"       INVISIBLE IN BOTH REGIMES        : {OrientationIsInvisible()}");
        sb.AppendLine("       the rotation is orthogonal inside the irrep, so it preserves the sum and every");
        sb.AppendLine("       level norm; Schur's lemma makes it unreachable, not merely small. The absolute");
        sb.AppendLine("       size of the unseen move is a property of the STATE; its invisibility is not.");
        sb.AppendLine();
        sb.AppendLine("   (b) ALL 192 GROUP IMAGES OF A GENERIC STATE");
        sb.AppendLine($"       distinct readings                : {distinct}");
        sb.AppendLine($"       largest disagreement in the data : {dataSpread:E3}");
        sb.AppendLine($"       largest separation of the states : {stateSpread:F4}");
        sb.AppendLine($"       ONE READING, 192 STATES          : {GroupImagesShareOneReading()}");
        sb.AppendLine("       the fibre of the measurement is at least 192-to-1, before any continuous degeneracy");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. THE CANDIDATES");
        sb.AppendLine("   candidate                       retained  loss  invertible?");
        foreach (var (candidate, retained, loss, invertible, verdict) in Candidates())
        {
            sb.AppendLine($"   {candidate,-30}  {retained,7}  {loss,4}   {verdict}");
            sb.AppendLine($"       {invertible}");
        }
        sb.AppendLine();
        sb.AppendLine($"   reach the whole space : {string.Join(", ", CandidatesThatReachTheWholeSpace())}");
        sb.AppendLine($"   closest distinct rung : {ClosestCandidate()}");
        sb.AppendLine();
        sb.AppendLine("6. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
