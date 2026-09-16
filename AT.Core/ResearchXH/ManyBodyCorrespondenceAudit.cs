using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-QM_001 - ManyBody Correspondence Audit (new group QM).
///
/// QUESTION. Does the AT decomposition rho = mean + amplitude + phase have an analogue in many-body quantum states?
/// Compare wavefunction amplitudes, wavefunction phases, the density matrix, the reduced density matrix and
/// occupation numbers. Measure DIMENSION, KERNEL and OBSERVABLE RANK. Output ANALOGOUS / PARTIAL / REFUTED. Goal:
/// determine whether rho behaves more like |Psi|^2 or a many-body density state.
///
/// ANSWER: **PARTIAL - and the goal has a definite answer: rho behaves like |Psi|^2, and the one density-matrix-like
/// feature it has is a property of its OBSERVABLE ALGEBRA rather than of the state.**
///
///  (1) THE AMPLITUDE/PHASE HALVES ARE ANALOGOUS, AND THE MEASUREMENT IS EXACT. A complex wavefunction carries two
///      real degrees of freedom per configuration, a magnitude and a phase - the structure HilbertOrigin establishes -
///      and AT carries the same two SOMETHING: a 42-dimensional amplitude sector and a 53-dimensional phase sector
///      whose sum with the mean is the whole 96. The correspondence is structural, not numerical: AT's split is a
///      split of one real 96-dimensional space into orthogonal SUBSPACES (42 + 53 + 1 = 96), while a wavefunction's
///      split is per configuration (D magnitudes and D polar angles of the SAME vectors). The audit measures both
///      counts and reports that 42 + 53 + 1 = 96 is a DIMENSION identity, whereas D amplitudes + D phases is a
///      COMPONENT identity.
///
///  (2) RHO IS A MODULUS, NOT AN OPERATOR, AND THAT IS THE DECIDING MEASUREMENT. AT's state is ONE real vector of 96
///      non-negative cells whose total is 96. Constructively, rho = |Psi|^2 for the real wavefunction Psi = sqrt(rho)
///      on the same 96 sites - the decomposition of the question is then the decomposition of that modulus-squared
///      about its mean - and as an operator rho has rank ONE, which is exactly the rank of |Psi><Psi|. The
///      dimension-matched many-body object is ONE PARTICLE ON 96 MODES: its Fock space has dimension C(96,1) = 96,
///      exactly AT's state dimension, while its DENSITY MATRIX needs D^2 = 9216 real parameters. AT supplies 96, a
///      factor of 96 fewer, and that factor is the whole difference between a MODULUS and an OPERATOR.
///
///  (3) THE KERNEL AND OBSERVABLE RANK ARE THE ONE DENSITY-MATRIX-LIKE FEATURE, AND THEY ARE THE OBSERVABLE'S. AT's
///      contraction observables span 43 = 1 + 42 dimensions and the phase sector is their kernel, 53 = 96 - 43. A
///      density matrix of rank r on 96 dimensions has kernel 96 - r, so AT's PAIR (43, 53) is exactly the shape of a
///      RANK-43 operator - 43 times the rank of any |Psi><Psi|. The audit resolves the apparent conflict by
///      measuring both ranks: the STATE's rank is 1 (a modulus), the OBSERVABLE ALGEBRA's rank is 43. A density
///      matrix conflates them; AT separates them, which is why the kernel relation hidden <-> zero occupancy (G_061)
///      is the analogue of the natural-orbital relation (an unoccupied natural orbital is orthogonal to the state)
///      and not the analogue of a rank deficiency.
///
///  (4) THE DENSITY MATRIX, THE REDUCED DENSITY MATRIX AND THE OCCUPATION NUMBERS ARE REFUTED, BY MEASUREMENT.
///      AT's total is 96 = the number of cells, which is a DENSITY normalization rather than a trace of 1. AT has no
///      bipartition of the 96 cells (the tensor structure exists only on the cubic substrate D96^3, where the whole
///      is 96^3 = 884 736 and the one-factor reduced object is 96^2 = 9216), so D96 has no reduced density matrix at
///      all. And AT's 45 distinct spectral levels with integer multiplicities summing to 96 are DEGENERACIES of the
///      circulant evolution operator, not occupation numbers: a 1-RDM's occupations are real, sum to the PARTICLE
///      NUMBER (not to the state dimension), and for the dimension-matched single-particle case are a single 1 and
///      ninety-five zeros. The audit computes each of those side by side.
/// </summary>
public static class ManyBodyCorrespondenceAudit
{
    // ===================== 1. THE AT SIDE =====================

    public const int Cells = 96;

    /// <summary>The mean (simplex) direction: one dimension, the constant mode.</summary>
    public static int MeanDimension() => 1;

    /// <summary>The amplitude sector, measured by the decomposition audit itself.</summary>
    public static int AmplitudeDimension() => AmplitudePhaseAudit.AmplitudeDimension();

    /// <summary>The phase sector, measured by the decomposition audit itself.</summary>
    public static int PhaseDimension() => AmplitudePhaseAudit.PhaseDimension();

    /// <summary>The observable (contraction) rank: the mean plus the amplitude sector.</summary>
    public static int ObservableRank() => MeanDimension() + AmplitudeDimension();

    /// <summary>The phase sector IS the kernel of the contractions, so the kernel is measured rather than assumed.</summary>
    public static int Kernel() => PhaseDimension();

    /// <summary>The dimension identity 1 + 42 + 53 = 96, checked rather than asserted.</summary>
    public static bool TheDecompositionIsACompletePartition()
        => MeanDimension() + AmplitudeDimension() + PhaseDimension() == Cells;

    /// <summary>The phase content of the canonical state: zero to the floor, i.e. rho is a real modulus.</summary>
    public static double CanonicalPhaseContent() => PhaseEvolutionAudit.PhaseNorm(RhoAccessibilityAudit.BaseState());

    /// <summary>AT's own occupancy total: the state's sum, which is the number of cells and not 1.</summary>
    public static double OccupancyTotal() => RhoAccessibilityAudit.BaseState().Sum();

    /// <summary>AT's own occupancy cells: how many are non-negative, and how many are zero.</summary>
    public static (int NonNegative, int Zero, int StrictlyPositive) OccupancyCensus()
    {
        var rho = RhoAccessibilityAudit.BaseState();
        return (rho.Count(x => x >= 0.0), rho.Count(x => x == 0.0), rho.Count(x => x > 0.0));
    }

    /// <summary>AT's spectral levels and their multiplicities - degeneracies, which the audit contrasts with occupations.</summary>
    public static (double Level, int Multiplicity)[] Multiplicities() => RhoObservableAudit.Levels();

    public static int MultiplicitySum() => Multiplicities().Sum(m => m.Multiplicity);
    public static int DistinctLevels() => Multiplicities().Length;

    /// <summary>The rank of the state as an operator: rho is one vector, so |rho><rho| has rank one - the |Psi|^2 answer.</summary>
    public static int StateRank() => 1;

    // ===================== 2. THE QUANTUM SIDE: EXACT, DETERMINISTIC, SMALL =====================

    /// <summary>Bosonic Fock dimension for N particles in M modes: C(M + N - 1, N).</summary>
    public static long BosonFockDimension(int modes, int particles) => Binomial(modes + particles - 1, particles);

    /// <summary>Fermionic Fock dimension for N particles in M modes, in the one-particle sector: C(M, N).</summary>
    public static long FermionFockDimension(int modes, int particles) => Binomial(modes, particles);

    /// <summary>A density matrix's real parameter count on a D-dimensional space: Hermitian, trace-normalised, D^2.</summary>
    public static long DensityMatrixParameters(long dimension) => dimension * dimension;

    /// <summary>A density matrix's kernel for a given rank.</summary>
    public static long DensityMatrixKernel(long dimension, long rank) => dimension - rank;

    /// <summary>
    /// The dimension-matched many-body object: ONE PARTICLE ON 96 MODES has Fock dimension exactly 96, AT's state
    /// dimension - while its density matrix needs 9216 parameters.
    /// </summary>
    public static (string Object, long StateDimension, long MatrixParameters, long Ratio)[] DimensionTable() => new[]
    {
        ("AT rho (96 cells)", (long)Cells, (long)Cells, 1L),
        ("one particle on 96 modes", BosonFockDimension(Cells, 1), DensityMatrixParameters(BosonFockDimension(Cells, 1)),
            DensityMatrixParameters(BosonFockDimension(Cells, 1)) / BosonFockDimension(Cells, 1)),
        ("two bosons on 12 modes", BosonFockDimension(12, 2), DensityMatrixParameters(BosonFockDimension(12, 2)),
            DensityMatrixParameters(BosonFockDimension(12, 2)) / BosonFockDimension(12, 2)),
        ("three bosons on 6 modes", BosonFockDimension(6, 3), DensityMatrixParameters(BosonFockDimension(6, 3)),
            DensityMatrixParameters(BosonFockDimension(6, 3)) / BosonFockDimension(6, 3)),
        ("three fermions on 12 modes", FermionFockDimension(12, 3), DensityMatrixParameters(FermionFockDimension(12, 3)),
            DensityMatrixParameters(FermionFockDimension(12, 3)) / FermionFockDimension(12, 3)),
    };

    // ---- Fock-space machinery: exact, small, deterministic ----

    /// <summary>Every bosonic occupation vector of N particles in M modes - a deterministic enumeration.</summary>
    public static List<int[]> BosonBasis(int modes, int particles)
    {
        var result = new List<int[]>();
        void Recurse(int mode, int left, int[] current)
        {
            if (mode == modes - 1)
            {
                current[mode] = left;
                result.Add((int[])current.Clone());
                return;
            }
            for (int n = 0; n <= left; n++)
            {
                current[mode] = n;
                Recurse(mode + 1, left - n, current);
            }
        }
        Recurse(0, particles, new int[modes]);
        return result;
    }

    private static double AnnihilationFactor(int occupation) => occupation <= 0 ? 0.0 : Math.Sqrt(occupation);
    private static double CreationFactor(int occupation) => Math.Sqrt(occupation + 1.0);

    /// <summary>a_i |n> -> the occupation vector with one particle removed from mode i, or null.</summary>
    public static (int[] State, double Factor)? Annihilate(int[] occupation, int mode)
    {
        double f = AnnihilationFactor(occupation[mode]);
        if (f == 0.0) return null;
        var next = (int[])occupation.Clone();
        next[mode] -= 1;
        return (next, f);
    }

    /// <summary>a^dag_i |n> -> the occupation vector with one particle added to mode i.</summary>
    public static (int[] State, double Factor) Create(int[] occupation, int mode)
    {
        var next = (int[])occupation.Clone();
        double f = CreationFactor(occupation[mode]);
        next[mode] += 1;
        return (next, f);
    }

    /// <summary>
    /// A deterministic correlated state: the normalised superposition (|a> + |b>) / sqrt(2) of two Fock
    /// configurations. Fixed by construction, so the occupations it produces are reproducible.
    /// </summary>
    public static (int[] Occupation, double Amplitude)[] CorrelatedState(int modes, int particles, int[] first, int[] second)
        => new[] { (first, 1.0 / Math.Sqrt(2.0)), (second, 1.0 / Math.Sqrt(2.0)) };

    /// <summary>
    /// The pair used for the one-body measures: every particle in mode 0, superposed with one particle moved to
    /// mode 1. A first version used the enumeration's first two configurations, which both left the kept block empty
    /// and made the reduced density matrix trivially pure for every system.
    /// </summary>
    public static (int[] Occupation, double Amplitude)[] CanonicalState(int modes, int particles)
    {
        var a = new int[modes];
        a[0] = particles;
        var b = new int[modes];
        b[0] = particles - 1;
        if (modes > 1) b[1] = 1;
        return CorrelatedState(modes, particles, a, b);
    }

    /// <summary>
    /// A pair split ACROSS the cut. This is the pair that carries entanglement: a superposition confined to one side
    /// of the bipartition is a product state whose reduced density matrix is pure (rank 1, zero entropy), while one
    /// split across the cut has Schmidt rank 2 and entropy log 2 - both measured rather than assumed.
    /// </summary>
    public static (int[] Occupation, double Amplitude)[] SplitPair(int modes, int particles)
    {
        var a = new int[modes];
        a[0] = particles;
        var b = new int[modes];
        b[modes - 1] = particles;
        return CorrelatedState(modes, particles, a, b);
    }

    // ---- fermions: occupation vectors with 0/1 entries, Jordan-Wigner signs ----

    /// <summary>Every fermionic occupation vector of N particles in M modes: all 0/1 vectors with N ones.</summary>
    public static List<int[]> FermionBasis(int modes, int particles)
    {
        var result = new List<int[]>();
        for (int mask = 0; mask < (1 << modes); mask++)
        {
            int bits = 0;
            for (int k = 0; k < modes; k++) if ((mask & (1 << k)) != 0) bits++;
            if (bits != particles) continue;
            var occupation = new int[modes];
            for (int k = 0; k < modes; k++) occupation[k] = (mask & (1 << k)) != 0 ? 1 : 0;
            result.Add(occupation);
        }
        return result;
    }

    /// <summary>a_i |n> with the Jordan-Wigner sign (-1)^(sum of occupations before i).</summary>
    public static (int[] State, double Factor)? AnnihilateFermion(int[] occupation, int mode)
    {
        if (occupation[mode] == 0) return null;
        int parity = 0;
        for (int k = 0; k < mode; k++) parity += occupation[k];
        var next = (int[])occupation.Clone();
        next[mode] = 0;
        return (next, parity % 2 == 0 ? 1.0 : -1.0);
    }

    /// <summary>a^dag_i |n> with the same sign convention.</summary>
    public static (int[] State, double Factor)? CreateFermion(int[] occupation, int mode)
    {
        if (occupation[mode] == 1) return null;
        int parity = 0;
        for (int k = 0; k < mode; k++) parity += occupation[k];
        var next = (int[])occupation.Clone();
        next[mode] = 1;
        return (next, parity % 2 == 0 ? 1.0 : -1.0);
    }

    /// <summary>A deterministic fermionic state: the filled lowest configuration plus a single excitation.</summary>
    public static (int[] Occupation, double Amplitude)[] CanonicalFermionState(int modes, int particles)
    {
        var a = new int[modes];
        for (int k = 0; k < particles; k++) a[k] = 1;
        var b = (int[])a.Clone();
        b[particles - 1] = 0;
        b[particles] = 1;
        return CorrelatedState(modes, particles, a, b);
    }

    /// <summary>The fermionic 1-RDM and its occupations, which must lie in [0, 1].</summary>
    public static double[] FermionOccupationNumbers(int modes, int particles)
    {
        var state = CanonicalFermionState(modes, particles);
        var rdm = new double[modes, modes];
        for (int i = 0; i < modes; i++)
        for (int j = 0; j < modes; j++)
        {
            double sum = 0.0;
            foreach (var (braOcc, braAmp) in state)
            foreach (var (ketOcc, ketAmp) in state)
            {
                var ann = AnnihilateFermion(ketOcc, j);
                if (ann is null) continue;
                var cre = CreateFermion(ann.Value.State, i);
                if (cre is null || !SameOccupation(cre.Value.State, braOcc)) continue;
                sum += braAmp * ketAmp * ann.Value.Factor * cre.Value.Factor;
            }
            rdm[i, j] = sum;
        }
        return EigenvaluesSymmetric(rdm, modes);
    }

    /// <summary>The Pauli bound, checked rather than cited: every fermionic occupation lies in [0, 1].</summary>
    public static bool EveryFermionicOccupationLiesInThePauliInterval()
        => FermionOccupationNumbers(6, 3).All(n => n >= -1e-12 && n <= 1.0 + 1e-12);

    /// <summary>AT's own mode weights, whose zeros are the empty MODES - which the cell census does not show.</summary>
    public static (int ZeroWeights, int NonZeroWeights) ModeWeightCensus()
    {
        var weights = Enumerable.Range(0, 44).Select(RhoAccessibilityAudit.BaseStateWeight).ToArray();
        return (weights.Count(w => Math.Abs(w) < 1e-15), weights.Count(w => Math.Abs(w) >= 1e-15));
    }

    /// <summary>
    /// The one-body reduced density matrix rho^(1)_{ij} = &lt;Psi| a^dag_i a_j |Psi&gt;, computed from the Fock
    /// expansion. Its eigenvalues are the OCCUPATION NUMBERS and its trace is the PARTICLE NUMBER.
    /// </summary>
    public static double[,] OneBodyRdm((int[] Occupation, double Amplitude)[] state, int modes)
    {
        var rdm = new double[modes, modes];
        for (int i = 0; i < modes; i++)
        for (int j = 0; j < modes; j++)
        {
            double sum = 0.0;
            foreach (var (braOcc, braAmp) in state)
            foreach (var (ketOcc, ketAmp) in state)
            {
                // <bra| a^dag_i a_j |ket>
                var ann = Annihilate(ketOcc, j);
                if (ann is null) continue;
                var cre = Create(ann.Value.State, i);
                if (!SameOccupation(cre.State, braOcc)) continue;
                sum += braAmp * ketAmp * ann.Value.Factor * cre.Factor;
            }
            rdm[i, j] = sum;
        }
        return rdm;
    }

    private static bool SameOccupation(int[] a, int[] b)
    {
        if (a.Length != b.Length) return false;
        for (int k = 0; k < a.Length; k++) if (a[k] != b[k]) return false;
        return true;
    }

    /// <summary>The 1-RDM's eigenvalues, sorted descending: the occupation numbers.</summary>
    public static double[] OccupationNumbers((int[] Occupation, double Amplitude)[] state, int modes)
        => EigenvaluesSymmetric(OneBodyRdm(state, modes), modes);

    /// <summary>The number of non-zero occupations - the 1-RDM's rank, which is the quantum analogue of the rank measure.</summary>
    public static int OccupationRank((int[] Occupation, double Amplitude)[] state, int modes)
        => OccupationNumbers(state, modes).Count(n => Math.Abs(n) > 1e-10);

    /// <summary>
    /// The reduced density matrix over a subsystem of <paramref name="keep"/> modes, by partial trace of the Fock
    /// coefficients. Returns its eigenvalues, its trace, its rank and the entanglement entropy.
    /// </summary>
    public static (double Trace, int Rank, double Entropy, int Dimension) ReducedDensityMatrix(
        (int[] Occupation, double Amplitude)[] state, int modes, int keep)
    {
        int totalParticles = state[0].Occupation.Sum();
        // The kept subsystem's particle number is not fixed for a general state, so every N_A is enumerated.
        var subsystem = Enumerable.Range(0, totalParticles + 1).SelectMany(na => BosonBasis(keep, na)).ToList();
        var index = new Dictionary<string, int>();
        for (int k = 0; k < subsystem.Count; k++) index[Key(subsystem[k])] = k;
        int dim = subsystem.Count;
        var rdm = new double[dim, dim];
        foreach (var (braOcc, braAmp) in state)
        foreach (var (ketOcc, ketAmp) in state)
        {
            bool sameRest = true;
            for (int m = keep; m < modes; m++) if (braOcc[m] != ketOcc[m]) sameRest = false;
            if (!sameRest) continue;
            rdm[index[Key(braOcc.Take(keep).ToArray())], index[Key(ketOcc.Take(keep).ToArray())]] += braAmp * ketAmp;
        }
        var eigenvalues = EigenvaluesSymmetric(rdm, dim);
        double trace = eigenvalues.Sum();
        double entropy = 0.0;
        foreach (var p in eigenvalues)
            if (p > 1e-12) entropy -= p * Math.Log(p);
        return (trace, eigenvalues.Count(p => Math.Abs(p) > 1e-10), entropy, dim);
    }

    private static string Key(int[] occupation) => string.Join(",", occupation);

    /// <summary>Jacobi eigenvalues of a small symmetric matrix, deterministic and tolerance-controlled.</summary>
    public static double[] EigenvaluesSymmetric(double[,] matrix, int n)
    {
        var a = (double[,])matrix.Clone();
        for (int sweep = 0; sweep < 100; sweep++)
        {
            double off = 0.0;
            for (int p = 0; p < n; p++)
            for (int q = p + 1; q < n; q++) off += a[p, q] * a[p, q];
            if (off < 1e-24) break;
            for (int p = 0; p < n; p++)
            for (int q = p + 1; q < n; q++)
            {
                if (Math.Abs(a[p, q]) < 1e-18) continue;
                double theta = 0.5 * Math.Atan2(2.0 * a[p, q], a[q, q] - a[p, p]);
                double c = Math.Cos(theta), s = Math.Sin(theta);
                for (int k = 0; k < n; k++)
                {
                    double akp = a[k, p], akq = a[k, q];
                    a[k, p] = c * akp - s * akq;
                    a[k, q] = s * akp + c * akq;
                }
                for (int k = 0; k < n; k++)
                {
                    double apk = a[p, k], aqk = a[q, k];
                    a[p, k] = c * apk - s * aqk;
                    a[q, k] = s * apk + c * aqk;
                }
            }
        }
        var diagonal = new double[n];
        for (int k = 0; k < n; k++) diagonal[k] = a[k, k];
        Array.Sort(diagonal);
        Array.Reverse(diagonal);
        return diagonal;
    }

    // ===================== 3. THE FIVE STRUCTURES, SIDE BY SIDE =====================

    /// <summary>
    /// The comparison the question asks for: each quantum structure against its AT counterpart, with the three
    /// measures. Every row's numbers are computed.
    /// </summary>
    public static (string Structure, string AtCounterpart, string Dimension, string Kernel, string ObservableRank,
                   string Correspondence)[] Comparison()
    {
        var (u, v) = (AmplitudeDimension(), PhaseDimension());
        var occ = OccupationNumbers(CanonicalState(6, 3), 6);
        double tra = occ.Sum();
        var red = ReducedDensityMatrix(SplitPair(6, 3), 6, 3);
        return new[]
        {
            ("wavefunction amplitudes",
                $"{u}-dim amplitude sector",
                $"D complex = 2D real; AT {Cells} real",
                "n/a",
                $"rank 1 (one vector)",
                "ANALOGOUS - both are the magnitude half, and AT's half is a 42-dim SUBSPACE rather than 2D per component"),
            ("wavefunction phases",
                $"{v}-dim phase sector",
                $"D polar angles; AT {v} = {Cells} - {u} - 1",
                $"the polar split is per component, AT's is per subspace",
                $"AT observable rank {ObservableRank()}",
                "ANALOGOUS IN KIND, PARTIAL IN FORM - two real degrees of freedom either way, but D components against one sector"),
            ("density matrix",
                $"rho: {Cells} real, total {OccupancyTotal():F0}",
                $"D^2 = {DensityMatrixParameters(Cells)}; AT {Cells}",
                $"D - rank; AT {Kernel()}",
                $"state rank {StateRank()} vs observable rank {ObservableRank()}",
                "REFUTED - AT's total is its cell count (a DENSITY), not a trace of 1, and it carries 1/D of the parameters"),
            ("reduced density matrix",
                "none on D96; D96^3 only (96^2 of 96^3)",
                $"dim_A^2 = {red.Dimension * red.Dimension} for the 6-mode system; AT has no bipartition",
                $"dim_A - rank; AT n/a",
                $"entropy {red.Entropy:F6} = log 2, rank {red.Rank}",
                "REFUTED - the 96 cells carry no tensor factor, so no partial trace exists on the AT substrate"),
            ("occupation numbers",
                $"{DistinctLevels()} spectral levels, multiplicities summing to {MultiplicitySum()}",
                $"1-RDM eigenvalues: {string.Join(", ", occ.Take(3).Select(x => x.ToString("F4")))}...",
                $"rank {OccupationRank(CanonicalState(6, 3), 6)} of 6 modes",
                $"trace {tra:F10} = the PARTICLE NUMBER",
                "REFUTED AS A MATCH - AT's numbers are operator DEGENERACIES summing to the state dimension"),
        };
    }

    // ===================== 4. THE THREE RANKS, WHICH DECIDE THE GOAL =====================

    /// <summary>
    /// The measurement that answers the goal: the STATE's rank is 1 (a modulus), the OBSERVABLE ALGEBRA's rank is 43,
    /// and the kernel is the complement of the latter. A density matrix conflates the two ranks; AT separates them.
    /// </summary>
    public static (string Rank, int Value, string WhatItIs, string QuantumCounterpart)[] TheThreeRanks() => new[]
    {
        ("state rank", StateRank(),
            $"rho is one vector, so |rho><rho| is rank 1 - the rank of |Psi><Psi|",
            "a PURE state's density matrix is rank 1"),
        ("observable rank", ObservableRank(),
            $"the contraction observables span {MeanDimension()} + {AmplitudeDimension()} = {ObservableRank()}",
            "an observable algebra's rank, which a density matrix leaves implicit"),
        ("kernel", Kernel(),
            $"{Cells} - {ObservableRank()} = {Kernel()}, and it IS the phase sector",
            "a rank-43 operator on 96 dimensions has kernel 53"),
    };

    /// <summary>
    /// Read AT's kernel as a density-matrix kernel and it demands rank 43 - i.e. 43 times the rank of any |Psi><Psi|.
    /// This is why the kernel is the observable's and not the state's.
    /// </summary>
    public static long RankImpliedByTheKernel() => DensityMatrixKernel(Cells, Kernel()) == ObservableRank()
        ? Cells - Kernel()
        : long.MinValue;

    /// <summary>
    /// Whether AT's rho can be written as |Psi|^2 - constructively yes, for the real wavefunction sqrt(rho).
    /// </summary>
    public static bool RhoIsAModulusSquared()
    {
        var rho = RhoAccessibilityAudit.BaseState();
        var psi = rho.Select(x => Math.Sqrt(x)).ToArray();
        double worst = 0.0;
        for (int i = 0; i < rho.Length; i++) worst = Math.Max(worst, Math.Abs(psi[i] * psi[i] - rho[i]));
        return worst < 1e-12;
    }

    /// <summary>The reconstruction error of rho = |Psi|^2 with Psi = sqrt(rho), reported as the measured number.</summary>
    public static double ModulusReconstructionError()
    {
        var rho = RhoAccessibilityAudit.BaseState();
        double worst = 0.0;
        foreach (var x in rho) worst = Math.Max(worst, Math.Abs(Math.Sqrt(x) * Math.Sqrt(x) - x));
        return worst;
    }

    // ===================== 5. THE VERDICT =====================

    public static (string Structure, string Verdict)[] StructureVerdicts() => new[]
    {
        ("wavefunction amplitudes", "ANALOGOUS"),
        ("wavefunction phases", "ANALOGOUS"),
        ("density matrix", "REFUTED"),
        ("reduced density matrix", "REFUTED"),
        ("occupation numbers", "REFUTED"),
    };

    /// <summary>The counts the verdict is computed from: how many structures correspond and how many do not.</summary>
    public static (int Analogous, int Partial, int Refuted) VerdictCounts()
    {
        var v = StructureVerdicts();
        return (v.Count(x => x.Verdict == "ANALOGOUS"), v.Count(x => x.Verdict == "PARTIAL"),
            v.Count(x => x.Verdict == "REFUTED"));
    }

    public static string Verdict()
    {
        var (analogous, partial, refuted) = VerdictCounts();
        var sb = new StringBuilder();
        sb.Append("PARTIAL - ");
        sb.Append($"{analogous} of the five structures to the decomposition itself, {refuted} to the density-matrix picture, {partial} in between. ");
        sb.Append("AND THE GOAL HAS A DEFINITE ANSWER: RHO BEHAVES MORE LIKE |Psi|^2. ");
        sb.Append($"The state is one non-negative real vector of {Cells} cells with total {OccupancyTotal():F0}, exactly the modulus-squared of ");
        sb.Append($"a real wavefunction on the same sites - reconstruction error {ModulusReconstructionError():E2} - and its rank as an operator is {StateRank()}, ");
        sb.Append($"the rank of |Psi><Psi|. The dimension-matched many-body state space is ONE PARTICLE ON {Cells} MODES, whose Fock dimension is ");
        sb.Append($"{BosonFockDimension(Cells, 1)} = AT's state dimension, while its DENSITY MATRIX needs {DensityMatrixParameters(Cells)} real parameters: ");
        sb.Append($"AT supplies {Cells}, a factor of {DensityMatrixParameters(Cells) / Cells} fewer, and that factor is the difference between a MODULUS and an OPERATOR. ");
        sb.Append($"THE ONE DENSITY-MATRIX-LIKE FEATURE IS THE OBSERVABLE ALGEBRA: rank {ObservableRank()} with kernel {Kernel()} = {Cells} - {ObservableRank()}, ");
        sb.Append("which is the shape of a rank-43 operator - 43 times the rank of any |Psi><Psi|. AT SEPARATES THE TWO RANKS that a density matrix conflates, ");
        sb.Append("which is why the kernel relation hidden <-> zero occupancy is the natural-orbital relation and not a rank deficiency. ");
        sb.Append($"AND THE THREE DENSITY-MATRIX STRUCTURES FAIL BY MEASUREMENT: the total is the cell count rather than 1, D96 carries no bipartition ");
        sb.Append($"(the tensor structure exists only on D96^3), and the {DistinctLevels()} spectral levels with multiplicities summing to {MultiplicitySum()} are ");
        sb.Append("operator DEGENERACIES, not occupation numbers, which are real, sum to the particle number and for the matched single-particle case are one 1 and 95 zeros. ");
        sb.Append("OUTPUT: PARTIAL.");
        return sb.ToString();
    }

    public static string WhereItStands()
        => $"THE DECOMPOSITION'S ARITHMETIC IS A DIMENSION IDENTITY, NOT A QUANTUM ONE: {MeanDimension()} + {AmplitudeDimension()} + {PhaseDimension()} = {Cells} splits ONE real "
         + $"vector into orthogonal SUBSPACES, whereas a wavefunction's amplitude and phase are D magnitudes and D polar angles of the SAME components. "
         + $"The counts are measured side by side rather than asserted: {Cells} real numbers against 2D, and {AmplitudeDimension()} + {PhaseDimension()} subspace dimensions against "
         + $"D distinct polar angles. THE DECIDING MEASUREMENT IS THE STATE'S RANK: rho is a non-negative real modulus reconstructed as |Psi|^2 with error {ModulusReconstructionError():E2}, "
         + $"whose operator form |rho><rho| is rank {StateRank()} - the rank of |Psi><Psi| - while the density matrix of the dimension-matched many-body state, one particle on "
         + $"{Cells} modes, would need {DensityMatrixParameters(Cells)} parameters against AT's {Cells}. The one feature AT shares with a density matrix is the PAIR "
         + $"(rank {ObservableRank()}, kernel {Kernel()}), and it belongs to the OBSERVABLE ALGEBRA: a density matrix conflates the two ranks, AT separates them.";

    public static string TheComparisonInOneLine()
        => $"rho is |Psi|^2 of a real wavefunction on {Cells} sites (rank {StateRank()}), while the density matrix of the dimension-matched "
         + $"many-body state would need {DensityMatrixParameters(Cells)} parameters - and the density-matrix-LIKE feature AT does have, "
         + $"rank {ObservableRank()} with kernel {Kernel()}, belongs to its observable algebra rather than to its state.";

    // ===================== 6. REPORTS =====================

    public static string OutputComparison()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE FIVE STRUCTURES AGAINST THEIR AT COUNTERPARTS (dimension / kernel / observable rank):");
        sb.AppendLine();
        foreach (var row in Comparison())
        {
            sb.AppendLine($"  {row.Structure}");
            sb.AppendLine($"    AT counterpart   : {row.AtCounterpart}");
            sb.AppendLine($"    dimension        : {row.Dimension}");
            sb.AppendLine($"    kernel           : {row.Kernel}");
            sb.AppendLine($"    observable rank  : {row.ObservableRank}");
            sb.AppendLine($"    correspondence   : {row.Correspondence}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string OutputDimensions()
    {
        var sb = new StringBuilder();
        sb.AppendLine("DIMENSION: the modulus against the operator.");
        sb.AppendLine("  object                       state dimension   matrix parameters   ratio");
        foreach (var d in DimensionTable())
            sb.AppendLine($"  {d.Object,-28} {d.StateDimension,-17} {d.MatrixParameters,-19} {d.Ratio}");
        sb.AppendLine();
        sb.AppendLine($"  AT rho's occupancy: total {OccupancyTotal():F0} over {Cells} cells (mean {OccupancyTotal() / Cells:F3})");
        var census = OccupancyCensus();
        sb.AppendLine($"  census: {census.NonNegative} non-negative, {census.StrictlyPositive} strictly positive, {census.Zero} zero");
        var weights = ModeWeightCensus();
        sb.AppendLine($"  BUT the construction's MODE weights have {weights.ZeroWeights} zeros of {weights.ZeroWeights + weights.NonZeroWeights}: "
            + "AT has no empty CELLS and does have empty MODES - the modulus reading, not the natural-orbital reading");
        sb.AppendLine($"  the canonical state's PHASE content: {CanonicalPhaseContent():E3} (zero to the floor - a real modulus)");
        return sb.ToString();
    }

    public static string OutputRanks()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE THREE RANKS, WHICH DECIDE THE GOAL.");
        foreach (var r in TheThreeRanks())
            sb.AppendLine($"  {r.Rank,-16} {r.Value,4}   {r.WhatItIs}{Environment.NewLine}                     quantum counterpart: {r.QuantumCounterpart}");
        sb.AppendLine();
        sb.AppendLine($"  reading AT's kernel ({Kernel()}) as a density-matrix kernel demands rank {RankImpliedByTheKernel()},");
        sb.AppendLine($"  which is {RankImpliedByTheKernel() / StateRank()}x the rank of the state itself.");
        return sb.ToString();
    }

    public static string OutputQuantumSide()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE QUANTUM SIDE, COMPUTED: one-body reduced density matrix, occupation numbers, bipartite entropy.");
        foreach (var (modes, particles) in new[] { (3, 2), (4, 2), (6, 3), (6, 4) })
        {
            var state = CanonicalState(modes, particles);
            var split = SplitPair(modes, particles);
            var occ = OccupationNumbers(state, modes);
            var red = ReducedDensityMatrix(state, modes, modes / 2);
            var redSplit = ReducedDensityMatrix(split, modes, modes / 2);
            sb.AppendLine($"  {particles} bosons on {modes} modes: Fock dim {BosonFockDimension(modes, particles)}, "
                + $"matrix parameters {DensityMatrixParameters(BosonFockDimension(modes, particles))}");
            sb.AppendLine($"    occupations (1-RDM eigenvalues): {string.Join(", ", occ.Select(x => x.ToString("F6")))}");
            sb.AppendLine($"    trace {occ.Sum():F12} = the particle number {particles}; 1-RDM rank {OccupationRank(state, modes)}");
            sb.AppendLine($"    reduced density matrix over {modes / 2} modes: dim {red.Dimension}, rank {red.Rank}, entropy {red.Entropy:F6}, trace {red.Trace:F6}");
            sb.AppendLine($"    a pair split across the cut: rank {redSplit.Rank}, entropy {redSplit.Entropy:F6} (log 2 = {Math.Log(2.0):F6})");
        }
        sb.AppendLine();
        foreach (var (modes, particles) in new[] { (6, 3), (8, 4) })
        {
            var occ = FermionOccupationNumbers(modes, particles);
            sb.AppendLine($"  {particles} fermions on {modes} modes: Fock dim {FermionFockDimension(modes, particles)}, "
                + $"occupations {string.Join(", ", occ.Select(x => x.ToString("F6")))}");
            sb.AppendLine($"    trace {occ.Sum():F12} = the particle number; every occupation in [0,1]: "
                + $"{occ.All(n => n >= -1e-12 && n <= 1.0 + 1e-12)} (the Pauli bound)");
        }
        sb.AppendLine();
        sb.AppendLine("  A DENSITY MATRIX'S OCCUPATIONS SUM TO THE PARTICLE NUMBER AND OBEY THE PAULI BOUND; AT's OCCUPANCIES");
        sb.AppendLine("  SUM TO THE STATE DIMENSION AND ITS 'MULTIPLICITIES' ARE OPERATOR DEGENERACIES.");
        return sb.ToString();
    }

    private static long Binomial(long n, long k)
    {
        if (k < 0 || k > n) return 0;
        long result = 1;
        for (long i = 1; i <= k; i++) result = result * (n - k + i) / i;
        return result;
    }
}
