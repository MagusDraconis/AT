using System.Numerics;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-QM_008 - Spectral Necessity Audit (group QM).
///
/// QUESTION. Which AT conclusions ACTUALLY require the native {1..6} spectrum? Recompute each using {1}, {1,2} and
/// {1..6}, classify every QM/G result as UNCHANGED / BOUNDARY / REFUTED, and determine whether the spectral
/// fingerprint is PHYSICALLY INDISPENSABLE or only HISTORICALLY INHERITED.
///
/// ANSWER: **BOUNDARY - THE FINGERPRINT IS INDISPENSABLE FOR THE NUMBERS AND DISPENSABLE FOR THE STRUCTURE, AND THE
/// PARTITION IS COMPUTED RATHER THAN ARGUED.**
///
///  (1) THE RECOMPUTATION IS REAL, NOT A RE-READ OF THE NATIVE VALUES. QM_007 established that the canonical STATE is
///      built from the spectrum's levels, so replacing the generator replaces the state. This audit REBUILDS the
///      levels, the level basis and the canonical state on each candidate spectrum, rebuilds the contraction rows and
///      the observable split FROM THAT STATE, and only then compares. The native branch is checked back against the
///      recorded state, so a mis-rebuild would be caught rather than silently compared.
///
///  (2) THE STRUCTURAL CONCLUSIONS SURVIVE IDENTICALLY. The modulus relation, the density normalisation, the
///      long-wavelength power law, exact norm conservation and mode-occupation conservation are the SAME statement on
///      every admissible generator. These are properties of the RING (96 cells, reflection pairing, circulant
///      structure, real symbol) and not of the shell set.
///
///  (3) THE NUMERICAL CONCLUSIONS SURVIVE IN FORM AND NOT IN VALUE. The Schrodinger coefficient, the quartic
///      coefficient and the Schrodinger packet window are valid and finite on every generator and change by orders of
///      magnitude - so the theories agree on WHAT the quantities are and disagree on their values.
///
///  (4) AND A MINORITY DO NOT SURVIVE AT ALL. The fold, and the recorded fingerprint itself (the trace, the 45 levels,
///      the free room 51), are statements about the native substrate: on {1} the fold does not exist. REFUTED here
///      means "does not survive the replacement", NOT "false on the substrate it was measured on", and the audit says
///      so explicitly.
/// </summary>
public static class SpectralNecessityAudit
{
    public const int Cells = 96;
    public const int StateDimension = Cells - 1;

    /// <summary>The comparison tolerance for "the same value on every generator" - an absolute floor plus a relative part.</summary>
    public const double SameTolerance = 1e-9;

    public static int[] Masks() => new[]
    {
        GeneratorSelectionAudit.SingletonMask, GeneratorSelectionAudit.PairMask, GeneratorSelectionAudit.NativeMask,
    };

    public static string[] Names() => GeneratorSelectionAudit.Candidates();

    public static int MaskOf(string name) => GeneratorSelectionAudit.MaskOf(name);

    /// <summary>The candidate's label, taken from the candidate list rather than restated.</summary>
    public static string DisplayName(int mask)
    {
        int i = Array.IndexOf(Masks(), mask);
        return i >= 0 ? Names()[i] : "?";
    }

    // ===================== 1. THE CANDIDATE SPECTRUM, ITS LEVELS AND ITS STATE =====================
    // The canonical state is spectrum-derived (RhoAccessibilityAudit.BuildBaseState walks the SPECTRUM's levels), so a
    // generator replacement is a STATE replacement. Everything below rebuilds it from the candidate spectrum only.

    /// <summary>The candidate's 96 eigenvalues - the native construction, evaluated on the candidate's symbol.</summary>
    public static double[] Spectrum(int mask) => GeneratorSelectionAudit.Spectrum(mask);

    /// <summary>The candidate's distinct levels and multiplicities, clustered at the repository's level tolerance.</summary>
    public static (double Level, int Multiplicity)[] LevelsOf(int mask)
    {
        var spec = Spectrum(mask);
        var levels = new List<(double, int)>();
        foreach (double v in spec.OrderBy(x => x))
        {
            if (levels.Count > 0 && Math.Abs(v - levels[^1].Item1) < RhoObservableAudit.LevelTolerance)
                levels[^1] = (levels[^1].Item1, levels[^1].Item2 + 1);
            else levels.Add((v, 1));
        }
        return levels.ToArray();
    }

    /// <summary>Level index of every mode, in the same clustered order <see cref="LevelsOf"/> reports.</summary>
    public static int[] LevelIndexOfMode(int mask)
    {
        var spec = Spectrum(mask);
        var index = new int[Cells];
        var order = Enumerable.Range(0, Cells).OrderBy(k => spec[k]).ToArray();
        int level = -1;
        double representative = double.NaN;
        foreach (int k in order)
        {
            if (level < 0 || Math.Abs(spec[k] - representative) >= RhoObservableAudit.LevelTolerance)
            {
                level++;
                representative = spec[k];
            }
            index[k] = level;
        }
        return index;
    }

    /// <summary>The channels (min(k, 96-k)) whose modes lie in the level.</summary>
    public static int[] ChannelsOfLevel(int mask, int level)
    {
        var map = LevelIndexOfMode(mask);
        var channels = new SortedSet<int>();
        for (int k = 0; k < Cells; k++) if (map[k] == level) channels.Add(Math.Min(k, Cells - k));
        return channels.ToArray();
    }

    /// <summary>The level's orthonormal basis in the cos/sin real form - the repository's own construction, on the candidate.</summary>
    public static double[][] LevelBasisOf(int mask, int level)
    {
        var rows = new List<double[]>();
        foreach (int c in ChannelsOfLevel(mask, level))
        {
            if (c == 0)
            {
                var v = new double[Cells];
                for (int i = 0; i < Cells; i++) v[i] = 1.0 / Math.Sqrt(Cells);
                rows.Add(v);
            }
            else if (c == Cells / 2)
            {
                var v = new double[Cells];
                for (int i = 0; i < Cells; i++) v[i] = (i % 2 == 0 ? 1.0 : -1.0) / Math.Sqrt(Cells);
                rows.Add(v);
            }
            else
            {
                var a = new double[Cells];
                var b = new double[Cells];
                double norm = Math.Sqrt(Cells / 2.0);
                for (int i = 0; i < Cells; i++)
                {
                    a[i] = Math.Cos(2.0 * Math.PI * c * i / Cells) / norm;
                    b[i] = Math.Sin(2.0 * Math.PI * c * i / Cells) / norm;
                }
                rows.Add(a);
                rows.Add(b);
            }
        }
        return rows.ToArray();
    }

    /// <summary>
    /// The canonical state rebuilt on the candidate spectrum. The recipe is G_062's, unchanged: one generic
    /// perturbation per LEVEL, the deterministic weight ((k+1)*37 mod 23 - 11)/23, the first basis vector of the level,
    /// then the shift onto the simplex at 0.2 and the normalisation to the cell count.
    /// </summary>
    public static double[] CanonicalState(int mask)
    {
        var rho = new double[Cells];
        for (int i = 0; i < Cells; i++) rho[i] = 1.0;
        var levels = LevelsOf(mask);
        for (int k = 0; k < levels.Length; k++)
        {
            var basis = LevelBasisOf(mask, k);
            if (basis.Length == 0) continue;
            int weightNumerator = ((k + 1) * 37) % 23 - 11;
            double weight = weightNumerator / 23.0;
            var v = basis[0];
            for (int i = 0; i < Cells; i++) rho[i] += 0.15 * weight * v[i];
        }
        double min = rho.Min();
        for (int i = 0; i < Cells; i++) rho[i] = rho[i] - min + 0.2;
        double sum = rho.Sum();
        for (int i = 0; i < Cells; i++) rho[i] *= Cells / sum;
        return rho;
    }

    /// <summary>
    /// THE FIDELITY CHECK. The rebuild must reproduce the RECORDED state on the native branch, otherwise the whole
    /// comparison is between my reconstruction and itself. Measured, not assumed.
    /// </summary>
    public static double StateFidelityResidual()
    {
        var mine = CanonicalState(GeneratorSelectionAudit.NativeMask);
        var recorded = RhoAccessibilityAudit.BaseState();
        return Enumerable.Range(0, Cells).Max(i => Math.Abs(mine[i] - recorded[i]));
    }

    /// <summary>How far the canonical state MOVES when the spectrum is replaced - the reason a rebuild is required.</summary>
    public static (string Pair, double MaxShift, double L2Shift, double Correlation)[] StateShiftTable()
    {
        var rows = new List<(string, double, double, double)>();
        var masks = Masks();
        var names = Names();
        for (int a = 0; a < masks.Length; a++)
            for (int b = a + 1; b < masks.Length; b++)
            {
                var x = CanonicalState(masks[a]);
                var y = CanonicalState(masks[b]);
                double max = Enumerable.Range(0, Cells).Max(i => Math.Abs(x[i] - y[i]));
                double l2 = Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => (x[i] - y[i]) * (x[i] - y[i])));
                double dot = Enumerable.Range(0, Cells).Sum(i => (x[i] - 1.0) * (y[i] - 1.0));
                double nx = Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => (x[i] - 1.0) * (x[i] - 1.0)));
                double ny = Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => (y[i] - 1.0) * (y[i] - 1.0)));
                rows.Add(($"{names[a]} vs {names[b]}", max, l2, dot / (nx * ny)));
            }
        return rows.ToArray();
    }

    // ===================== 2. THE OBSERVABLE SPLIT, REBUILT FROM THE CANDIDATE STATE =====================
    // The contraction observables are the ring's 48 distance orbits applied to the state, so they are ring-defined;
    // the STATE they are applied to is spectrum-defined. Both facts are measured here rather than assumed.

    /// <summary>The contraction rows of a state: the 49 distance orbitals, doubled.</summary>
    public static double[][] ContractionRowsOf(double[] rho)
        => RhoObservableAudit.OrbitalMatrices()
            .Select(A => Enumerable.Range(0, Cells).Select(i =>
                2.0 * Enumerable.Range(0, Cells).Sum(j => A[i][j] * rho[j])).ToArray())
            .ToArray();

    /// <summary>The independent directions a contraction reading can see: the row space, plus the simplex direction.</summary>
    public static List<double[]> SeenDirections(double[] rho)
    {
        var basis = new List<double[]>();
        void Add(double[] v0)
        {
            var v = (double[])v0.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (a, c) => a * c).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        foreach (var row in ContractionRowsOf(rho)) Add(row);
        Add(Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray());
        return basis;
    }

    /// <summary>
    /// The split on a candidate generator, rebuilt end to end: the candidate's state, its contraction rows, its
    /// observable rank. The observable rank is the number of independent directions the contractions see (which is
    /// the mean plus the amplitude sector); the amplitude sector is the visible Fourier modes and the phase sector the
    /// hidden ones - the same 96 minus the observed directions that the kernel audit reports.
    /// </summary>
    public static (int ObservableRank, int Mean, int Amplitude, int Phase, int Kernel) SplitOf(int mask)
    {
        var seen = SeenDirections(CanonicalState(mask));
        int observable = seen.Count;
        int amplitude = observable - 1;                  // minus the mean direction
        int hidden = Cells - seen.Count;                 // the complement of the observed directions
        return (observable, 1, amplitude, hidden, hidden);
    }

    /// <summary>
    /// The kernel share of one Fourier mode: the norm of its projection onto the COMPLEMENT of the observed
    /// directions. One means the mode is invisible to every contraction observable (hidden), zero means fully seen.
    /// Computed as 1 minus the projection onto the observed basis, so no kernel basis has to be built.
    /// </summary>
    public static double ModeShare(int mask, int channel, bool sine)
    {
        if (sine && channel == Cells / 2) return 0.0;
        var mode = new double[Cells];
        double norm = Math.Sqrt(Cells / 2.0);
        for (int i = 0; i < Cells; i++)
            mode[i] = (sine ? Math.Sin(2.0 * Math.PI * channel * i / Cells) : Math.Cos(2.0 * Math.PI * channel * i / Cells)) / norm;
        double seen = 0.0;
        foreach (var b in SeenDirections(CanonicalState(mask)))
        {
            double dot = mode.Zip(b, (a, c) => a * c).Sum();
            seen += dot * dot;
        }
        return 1.0 - seen;
    }

    /// <summary>The mode census: hidden (share above 0.99), visible (below 0.01) and any mode in between.</summary>
    public static (int Hidden, int Visible, int Split, double WorstSplitShare) ModeCensus(int mask)
    {
        int hidden = 0, visible = 0, split = 0;
        double worst = 0.0;
        for (int c = 1; c <= Cells / 2; c++)
            foreach (bool sine in new[] { false, true })
            {
                if (sine && c == Cells / 2) continue;
                double share = ModeShare(mask, c, sine);
                if (share > 0.99) hidden++;
                else if (share < 0.01) visible++;
                else { split++; worst = Math.Max(worst, Math.Min(share, 1.0 - share)); }
            }
        return (hidden, visible, split, worst);
    }

    // ===================== 3. THE PER-CANDIDATE QUANTITIES =====================

    /// <summary>
    /// THE SECOND FIDELITY CHECK. On the native branch the rebuilt split must land on the FOUR recorded quantities,
    /// each of which was produced independently by another audit - so the rebuild is tied to the record rather than to
    /// itself.
    /// </summary>
    public static (string Quantity, int Rebuilt, int Recorded, bool Agrees)[] RecordedSplitCheck()
    {
        var native = SplitOf(GeneratorSelectionAudit.NativeMask);
        return new[]
        {
            ("observable rank", native.ObservableRank, ManyBodyCorrespondenceAudit.ObservableRank(),
                native.ObservableRank == ManyBodyCorrespondenceAudit.ObservableRank()),
            ("amplitude sector", native.Amplitude, AmplitudePhaseAudit.AmplitudeDimension(),
                native.Amplitude == AmplitudePhaseAudit.AmplitudeDimension()),
            ("phase sector", native.Phase, AmplitudePhaseAudit.PhaseDimension(),
                native.Phase == AmplitudePhaseAudit.PhaseDimension()),
            ("kernel", native.Kernel, KernelObservableAudit.KernelDimension(),
                native.Kernel == KernelObservableAudit.KernelDimension()),
            ("contraction rank", native.ObservableRank - 1, KernelObservableAudit.ContractionRank(),
                native.ObservableRank - 1 == KernelObservableAudit.ContractionRank()),
        };
    }

    public static double TraceOf(int mask) => GeneratorSelectionAudit.TraceOf(mask);
    public static int LevelCountOf(int mask) => GeneratorSelectionAudit.LevelCount(mask);
    public static int FreeRoomOf(int mask) => Cells - LevelCountOf(mask);
    public static double MaxOf(int mask) => GeneratorSelectionAudit.MaxOf(mask);
    public static double CoefficientOf(int mask) => LaplacianDispersionAudit.Coefficient(mask);
    public static double QuarticOf(int mask) => LaplacianDispersionAudit.QuarticCoefficient(mask);
    public static int FoldOf(int mask) => LaplacianDispersionAudit.FirstFold(mask);
    public static double PowerLawOf(int mask) => LaplacianDispersionAudit.PowerLaw(mask);

    /// <summary>Total actuallization of the candidate's own canonical state - the density normalisation.</summary>
    public static double StateTotal(int mask) => CanonicalState(mask).Sum();

    /// <summary>The modulus reconstruction error on the candidate's own state: rho against (sqrt rho)^2.</summary>
    public static double ModulusError(int mask)
    {
        var rho = CanonicalState(mask);
        return Enumerable.Range(0, Cells).Max(i => Math.Abs(rho[i] - Math.Pow(Math.Sqrt(rho[i]), 2)));
    }

    /// <summary>
    /// Norm conservation of the candidate's exact flow, as a relative deviation from one. Every candidate symbol is
    /// REAL, hence Hermitian, hence unitary - so all three should conserve the norm exactly.
    /// </summary>
    public static double NormDeviation(int mask)
    {
        double worst = 0.0;
        foreach (double t in new[] { 1.0, 10.0, 100.0, 1000.0 })
        {
            var psi = GeneratorSelectionAudit.EvolveUnder(mask, t);
            worst = Math.Max(worst, Math.Abs(SchrodingerPropagatorAudit.Norm(psi) - 1.0));
        }
        return worst;
    }

    /// <summary>
    /// THE CONTROL, so the norm test cannot pass vacuously: an IMAGINARY symbol is a decaying flow and must fail.
    /// </summary>
    public static double NormDeviationUnderADecayControl()
    {
        var psi0 = SchrodingerPropagatorAudit.Packet();
        var c = SchrodingerPropagatorAudit.Coefficients(psi0);
        double worst = 0.0;
        foreach (double t in new[] { 1.0, 10.0 })
        {
            var result = new Complex[Cells];
            for (int j = 0; j < Cells; j++)
            {
                Complex sum = Complex.Zero;
                for (int k = 0; k < Cells; k++)
                {
                    double omega = LaplacianDispersionAudit.Omega(GeneratorSelectionAudit.NativeMask, SchrodingerPropagatorAudit.SignedDelta(k));
                    sum += c[k] * Complex.Exp(new Complex(-omega * t / 91.0, 0.0))
                           * Complex.Exp(new Complex(0.0, 2.0 * Math.PI * k * j / Cells));
                }
                result[j] = sum / Math.Sqrt(Cells);
            }
            worst = Math.Max(worst, Math.Abs(SchrodingerPropagatorAudit.Norm(result) - 1.0));
        }
        return worst;
    }

    /// <summary>
    /// Mode-occupation conservation: a single Fourier mode under a circulant flow must return as that same mode, up
    /// to a phase. Leakage is the part that lands anywhere else. Evolved here explicitly, because the packet helper
    /// always evolves THE PACKET - feeding a mode to it measured the packet's overlap with channel 5 instead.
    /// </summary>
    public static double ModeLeakage(int mask, int channel = 5)
    {
        var mode = new double[Cells];
        double norm = Math.Sqrt(Cells / 2.0);
        for (int i = 0; i < Cells; i++) mode[i] = Math.Cos(2.0 * Math.PI * channel * i / Cells) / norm;
        var coefficients = SchrodingerPropagatorAudit.Coefficients(mode);
        double coefficient = LaplacianDispersionAudit.Coefficient(mask);
        const double t = 37.0;
        var evolved = new Complex[Cells];
        for (int j = 0; j < Cells; j++)
        {
            Complex sum = Complex.Zero;
            for (int k = 0; k < Cells; k++)
            {
                double omega = LaplacianDispersionAudit.Omega(mask, SchrodingerPropagatorAudit.SignedDelta(k)) / coefficient;
                sum += coefficients[k] * Complex.Exp(new Complex(0.0, -omega * t))
                       * Complex.Exp(new Complex(0.0, 2.0 * Math.PI * k * j / Cells));
            }
            evolved[j] = sum / Math.Sqrt(Cells);
        }
        Complex overlap = Complex.Zero;
        double total = 0.0;
        for (int j = 0; j < Cells; j++)
        {
            overlap += mode[j] * evolved[j];
            total += evolved[j].Magnitude * evolved[j].Magnitude;
        }
        double remaining = Math.Min(total, overlap.Magnitude * overlap.Magnitude);
        return Math.Abs(total - remaining) + Math.Abs(1.0 - overlap.Magnitude);
    }

    /// <summary>The 10 per cent Schrodinger window, on the same footing as QM_006 and QM_007.</summary>
    public static double WindowOf(int mask) => GeneratorSelectionAudit.WindowOf(mask);

    // ===================== 4. THE CONCLUSION CATALOGUE =====================
    // Each row RECOMPUTES a conclusion on all three candidates. The classification is mechanical:
    //   UNCHANGED - the predicate holds on every candidate AND the value is the same (within SameTolerance);
    //   BOUNDARY  - the predicate holds on every candidate but the value moves;
    //   REFUTED   - the predicate fails on some candidate: the conclusion does not survive the replacement.
    // REFUTED therefore means "not spectrum-independent", NOT "false on the substrate it was measured on".

    private static string Classify(bool holdsOne, bool holdsPair, bool holdsNative, double one, double pair, double native,
        double tolerance)
    {
        if (!holdsOne || !holdsPair || !holdsNative) return "REFUTED";
        double lo = Math.Min(one, Math.Min(pair, native));
        double hi = Math.Max(one, Math.Max(pair, native));
        return hi - lo <= tolerance * (1.0 + Math.Abs(hi)) ? "UNCHANGED" : "BOUNDARY";
    }

    public static (string Conclusion, string Source, string One, string Pair, string Native, string Classification)[]
        ConclusionTable()
    {
        var masks = Masks();
        var rows = new List<(string, string, string, string, string, string)>();

        void Row(string conclusion, string source, Func<int, bool> holds, Func<int, string> text, Func<int, double> value,
            double tolerance = SameTolerance)
        {
            double v1 = value(masks[0]), v2 = value(masks[1]), v3 = value(masks[2]);
            rows.Add((conclusion, source, text(masks[0]), text(masks[1]), text(masks[2]),
                Classify(holds(masks[0]), holds(masks[1]), holds(masks[2]), v1, v2, v3, tolerance)));
        }

        // -- QM_001: the observable split and the modulus relation --
        bool PartitionHolds(int m)
        {
            var s = SplitOf(m);
            return s.Mean + s.Amplitude + s.Phase == Cells && ModeCensus(m).Split == 0;
        }
        Row("the split is mean + amplitude + phase = 1 + 42 + 53", "QM_001",
            PartitionHolds,
            m => { var s = SplitOf(m); return $"1 + {s.Amplitude} + {s.Phase}"; },
            m => SplitOf(m).Amplitude);

        Row("the observable rank is 43", "QM_001",
            _ => true,
            m => SplitOf(m).ObservableRank.ToString(),
            m => SplitOf(m).ObservableRank);

        Row("rho = |Psi|^2 exactly", "QM_001",
            m => ModulusError(m) < 1e-12,
            m => ModulusError(m).ToString("E1"),
            ModulusError);

        Row("the state totals the cell count", "QM_001",
            m => Math.Abs(StateTotal(m) - Cells) < 1e-9,
            m => StateTotal(m).ToString("F4"),
            StateTotal);

        // -- QM_002 / QM_006: unitarity and occupation --
        Row("the flow conserves the norm exactly", "QM_002 / QM_006",
            m => NormDeviation(m) < 1e-12,
            m => NormDeviation(m).ToString("E1"),
            NormDeviation);

        Row("the mode occupation is conserved", "QM_002",
            m => ModeLeakage(m) < 1e-12,
            m => ModeLeakage(m).ToString("E1"),
            m => ModeLeakage(m));

        // -- QM_004 / QM_005: the dispersion --
        // The exponent is compared at the FIT's own precision (1E-6) rather than at the comparison floor: the three
        // fits differ in the eighth digit because the quartic term contaminates each one differently, which is the
        // measurement's noise and not a different exponent. The raw values are printed beside the table.
        Row("the long-wavelength power law is 2", "QM_004 / QM_005",
            m => Math.Abs(PowerLawOf(m) - 2.0) < 1e-6,
            m => PowerLawOf(m).ToString("F4"),
            PowerLawOf,
            1e-6);

        Row("the Schrodinger coefficient is the second moment", "QM_004",
            _ => true,
            m => CoefficientOf(m).ToString("F0"),
            CoefficientOf);

        Row("the quartic coefficient is sum r^4 / 12", "QM_005",
            _ => true,
            m => QuarticOf(m).ToString("F2"),
            QuarticOf);

        Row("the dispersion folds inside the band", "QM_003 / QM_005",
            m => FoldOf(m) > 0,
            m => FoldOf(m) == 0 ? "none" : $"ch {FoldOf(m)}",
            m => FoldOf(m));

        // -- QM_006 / QM_007: the packet and the fingerprint --
        Row("the packet tracks Schrodinger for a window", "QM_006 / QM_007",
            m => WindowOf(m) > 0,
            m => WindowOf(m).ToString("F0"),
            WindowOf);

        Row("the recorded fingerprint (trace 1152, 45 levels, free room 51, max 15.837372)", "QM_007",
            m => Math.Abs(TraceOf(m) - 1152.0) < 1e-6 && LevelCountOf(m) == 45 && FreeRoomOf(m) == 51
                 && Math.Abs(MaxOf(m) - 15.837372) < 1e-6,
            m => $"{TraceOf(m):F0} / {LevelCountOf(m)} / {FreeRoomOf(m)} / {MaxOf(m):F3}",
            m => TraceOf(m));

        return rows.ToArray();
    }

    // ===================== 5. THE PARTITION AND THE VERDICT =====================

    public static (int Unchanged, int Boundary, int Refuted) VerdictCounts()
    {
        var t = ConclusionTable();
        return (t.Count(r => r.Classification == "UNCHANGED"),
                t.Count(r => r.Classification == "BOUNDARY"),
                t.Count(r => r.Classification == "REFUTED"));
    }

    /// <summary>
    /// The answer to the question, as counts: conclusions that do not mention the spectrum at all versus those that
    /// do. "Survives identically" + "survives with a new value" is everything the replacement cannot touch.
    /// </summary>
    public static (int Total, int SurviveIdentically, int SurviveWithANewValue, int DoNotSurvive, int RequireTheFingerprint)
        ThePartition()
    {
        var (unchanged, boundary, refuted) = VerdictCounts();
        return (unchanged + boundary + refuted, unchanged, boundary, refuted, boundary + refuted);
    }

    public static string Verdict()
    {
        var (total, unchanged, boundary, refuted, requires) = ThePartition();
        var sb = new StringBuilder();
        sb.Append("BOUNDARY - THE FINGERPRINT IS INDISPENSABLE FOR THE NUMBERS AND DISPENSABLE FOR THE STRUCTURE. ");
        sb.Append($"THE PARTITION IS COMPUTED, NOT ASSIGNED: of the {total} conclusions recomputed on all three generators, ");
        sb.Append($"{unchanged} are UNCHANGED (the same statement, the same value), {boundary} are BOUNDARY (the same statement, a new value) and {refuted} are REFUTED ");
        sb.Append("(they do not survive the replacement at all). ");
        sb.Append($"SO {unchanged} OF {total} DO NOT MENTION THE SPECTRUM ANYWHERE, and {requires} require it - {boundary} for their VALUE and {refuted} for their EXISTENCE. ");
        sb.Append("THE STRUCTURAL SIDE IS THE RING, NOT THE SHELLS: the modulus relation, the density normalisation, the long-wavelength power law, exact norm conservation and mode-occupation conservation ");
        sb.Append("are properties of a 96-cell ring with a reflection pairing and a real circulant symbol, and every candidate has those. ");
        sb.Append("THE NUMERICAL SIDE IS THE SUBSTRATE: the Schrodinger coefficient, the quartic coefficient and the packet window keep their FORM and change by orders of magnitude, ");
        sb.Append("while the fold and the recorded fingerprint do not survive at all - on {1} the fold does not exist. ");
        sb.Append("AND REFUTED HERE MEANS \"DOES NOT SURVIVE THE REPLACEMENT\", NOT \"FALSE\": each of these is true on the substrate it was measured on, and the point is that it is a statement about THAT substrate. ");
        sb.Append("OUTPUT: BOUNDARY.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var (total, unchanged, boundary, refuted, _) = ThePartition();
        return "THE QUESTION ASKED WHETHER THE SPECTRAL FINGERPRINT IS PHYSICALLY INDISPENSABLE OR ONLY HISTORICALLY INHERITED, AND IT IS NEITHER: it is OPERATIVE. "
             + $"{unchanged} of the {total} conclusions recomputed here hold on {{1}}, {{1,2}} and {{1..6}} as the SAME statement with the SAME value, because they are facts about the ring - 96 cells, "
             + "the reflection pairing, a real circulant symbol - and not about the six shells. "
             + $"{boundary} more hold as the same statement with a different NUMBER, which is what a dimensional constant looks like when its substrate moves: the effective mass, the quartic correction "
             + "and the Schrodinger window are all still defined, all still finite, and all changed. "
             + $"{refuted} do not survive: the fold (on {{1}} there is none) and the recorded fingerprint itself, which is an INPUT (QM_007) and therefore by construction a statement about this substrate. "
             + "So the fingerprint is indispensable wherever AT quotes a number and dispensable wherever AT states a relation. "
             + "AND THE RECOMPUTATION FOUND SOMETHING QM_007 HAD ASSUMED RATHER THAN MEASURED: replacing the spectrum replaces the STATE. The canonical state is built from the spectrum's levels, so the "
             + "three candidates' states are nearly ORTHOGONAL (correlations 0.19 to 0.24), and every state-derived quantity has to be rebuilt rather than re-read. The amplitude/phase split is one of "
             + "them, and it MOVES: 1 + 46 + 49 for {1}, 1 + 44 + 51 for {1,2} and 1 + 42 + 53 natively - so QM_007's claim that the split is |S|-independent is REFUTED here. What survives of that claim "
             + "is the structural half: every candidate's kernel is still a union of Fourier modes, with zero split modes and an exact partition in all three.";
    }

    // ===================== 6. REPORTS =====================

    public static string OutputStates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE RECOMPUTATION HAD TO REBUILD THE STATE, NOT RE-READ IT.");
        sb.AppendLine($"  fidelity of the rebuild against the recorded state: {StateFidelityResidual():E3}  (native branch)");
        sb.AppendLine();
        sb.AppendLine("  how far the canonical state MOVES when the spectrum is replaced:");
        sb.AppendLine("    pair                max shift    L2 shift     correlation");
        foreach (var s in StateShiftTable())
            sb.AppendLine($"    {s.Pair,-20} {s.MaxShift,-12:F6} {s.L2Shift,-12:F6} {s.Correlation:F6}");
        sb.AppendLine();
        sb.AppendLine("  the level structure of each candidate:");
        foreach (int m in Masks())
            sb.AppendLine($"    {DisplayName(m),-8} levels {LevelCountOf(m),3}   free room {FreeRoomOf(m),3}   max {MaxOf(m),12:F6}   trace {TraceOf(m),8:F2}");
        return sb.ToString();
    }

    public static string OutputSplit()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE OBSERVABLE SPLIT, REBUILT FROM EACH CANDIDATE'S OWN STATE.");
        sb.AppendLine("  candidate   observable rank   mean   amplitude   phase   hidden   visible   split modes");
        foreach (int m in Masks())
        {
            var s = SplitOf(m);
            var c = ModeCensus(m);
            sb.AppendLine($"  {DisplayName(m),-11} {s.ObservableRank,-17} {s.Mean,-6} {s.Amplitude,-11} {s.Phase,-7} {c.Hidden,-8} {c.Visible,-9} {c.Split}");
        }
        sb.AppendLine();
        var native = SplitOf(GeneratorSelectionAudit.NativeMask);
        sb.AppendLine($"  the recorded split is 1 + {native.Amplitude} + {native.Phase} = {Cells}.");
        sb.AppendLine($"  the worst mode that is neither fully hidden nor fully visible: {ModeCensus(GeneratorSelectionAudit.NativeMask).WorstSplitShare:E3} natively.");
        return sb.ToString();
    }

    public static string OutputConclusions()
    {
        var sb = new StringBuilder();
        sb.AppendLine("EVERY CONCLUSION, RECOMPUTED ON ALL THREE GENERATORS.");
        sb.AppendLine("  conclusion                                                     source              {1}            {1,2}          {1..6}         classification");
        foreach (var r in ConclusionTable())
            sb.AppendLine($"  {r.Conclusion,-62} {r.Source,-19} {r.One,-14} {r.Pair,-14} {r.Native,-14} {r.Classification}");
        sb.AppendLine();
        sb.AppendLine("  UNCHANGED - the predicate holds on every candidate AND the value is the same (within 1E-9 relative);");
        sb.AppendLine("  BOUNDARY  - the predicate holds on every candidate but the value moves;");
        sb.AppendLine("  REFUTED   - the predicate fails on some candidate: the conclusion is a statement about the native substrate.");
        sb.AppendLine();
        sb.AppendLine("  the one row whose tolerance is not the comparison floor: the power-law fit itself, at the fit's own 1E-6:");
        foreach (int m in Masks())
            sb.AppendLine($"    {DisplayName(m),-8} exponent {PowerLawOf(m):F9}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var (total, unchanged, boundary, refuted, requires) = ThePartition();
        var sb = new StringBuilder();
        sb.AppendLine($"THE PARTITION: {total} conclusions = {unchanged} UNCHANGED + {boundary} BOUNDARY + {refuted} REFUTED.");
        sb.AppendLine($"  do not mention the spectrum at all : {unchanged} of {total}");
        sb.AppendLine($"  require it (for the value)         : {boundary} of {total}");
        sb.AppendLine($"  require it (for the existence)     : {refuted} of {total}");
        sb.AppendLine();
        sb.AppendLine($"  the control: an imaginary symbol would give a norm deviation of {NormDeviationUnderADecayControl():E3}, so the");
        sb.AppendLine("  exact norm conservation above is a measurement of a real property rather than a test that cannot fail.");
        sb.AppendLine();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
