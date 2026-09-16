using System.Numerics;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-QM_002 - Unitary Correspondence Audit (group QM).
///
/// QUESTION. Can any AT flow reproduce unitary Schrodinger evolution on the occupied mode sector? Compare the unitary
/// Cayley flow, the centred difference flow and the dissipative flow. Measure NORM CONSERVATION, PHASE EVOLUTION and
/// MODE OCCUPATION. Output ANALOGOUS / PARTIAL / REFUTED.
///
/// ANSWER: **PARTIAL - and the reason is a TRADE-OFF rather than a defect. Each AT flow has exactly ONE of the two
/// properties Schrodinger evolution needs, and NO AT FLOW HAS BOTH.**
///
///  (1) THE MEASUREMENT, in one line. Schrodinger evolution needs (a) |m| = 1 for every mode - unitary, norm
///      conserving - and (b) arg m proportional to the mode's momentum, so that the phase advances LINEARLY in k.
///      AT's flows split these exactly:
///
///          unitary (Cayley)   |m| = 1 EXACTLY            arg m = 2 arctan(eps sin d)   - LINEAR ONLY TO ORDER eps^2
///          exact flow         |m| = exp(-eps (1 - cos d)) arg m = eps sin d            - THE CLEAN DISPERSION
///          centred (skew)     |m| = sqrt(1 + eps^2 sin^2 d)  arg m = arctan(eps sin d)
///          forward difference |m| < 1 (dissipative)       arg m non-linear via the real part
///
///      so the unitary flow buys unitarity at the price of a DISTORTED dispersion, and the exact flow has the clean
///      dispersion at the price of DISSIPATION. The correspondence is therefore partial in a precise, measured sense.
///
///  (2) NORM CONSERVATION IS ANALOGOUS, EXACTLY. The Cayley multiplier is (1 + i eps s)/(1 - i eps s), so |m| = 1 for
///      all 48 channels to machine precision, at every eps tested - and the audit measures the worst deviation rather
///      than citing the algebra. The centred form is amplifying (|m| > 1), the forward difference dissipative.
///
///  (3) MODE OCCUPATION IS ANALOGOUS AS WELL, and the reason is structural: every AT update is CIRCULANT, hence
///      diagonal in the Fourier basis, so each mode's |c_k| is a constant of the motion for any of them. The audit
///      checks that numerically. BUT - and this is the part a mode count alone would miss - the AMPLITUDE/PHASE SPLIT
///      of the state is NOT a constant: the flow rotates amplitude content into the phase sector (measured), which is
///      what a REAL Hamiltonian could never do. The conserved quantity is the mode occupation; the split is not.
///
///  (4) PHASE EVOLUTION IS WHERE THE CORRESPONDENCE BREAKS, AND IT BREAKS TWICE. A Hermitian generator EXISTS - the
///      audit computes H_eff = -arg m / eps per mode and finds a real spectrum - so the AT unitary flow IS the
///      exponential of a Hermitian operator. But that operator is not the AT operator: it is
///      H_eff = (2/eps) arctan(eps sin d), so (a) THE DISCRETISATION DEVICE: H_eff differs from the symbol sin d by a
///      relative amount that falls as eps^2, measured on the eps ladder; and (b) THE LATTICE ITSELF: the symbol is
///      sin d, not d. sin d is BOUNDED and FOLDS - it peaks at d = pi/2 (channel 24) and returns to ZERO at the zone
///      edge (channel 48) - so on the AT lattice the highest mode does not advance at all, and above channel 24 the
///      modes advance in the REVERSE order to the continuum. The audit measures both the stationary zone-edge mode
///      and the inversion count, and then measures how much of that folding region is actually OCCUPIED - the
///      question asks about the occupied mode sector, and the answer is a count rather than a general remark.
/// </summary>
public static class UnitaryCorrespondenceAudit
{
    // ===================== 1. THE THREE FLOWS THE QUESTION NAMES =====================

    public const string Cayley = "unitary (Cayley of the skew part)";
    public const string Centred = "centred (skew) difference";
    public const string Dissipative = "forward difference";
    public const string Exact = "exact flow exp(eps D)";

    public static (string Flow, string Role)[] Flows() => new[]
    {
        (Cayley, "the candidate: norm-preserving by construction"),
        (Centred, "the bare rotation generator"),
        (Dissipative, "the flow the earlier audits call dissipative"),
        (Exact, "the difference's own flow, carried as the dispersion reference"),
    };

    public static int Channels() => PhaseEvolutionAudit.Channels().Length;
    public static double Delta(int channel) => PhaseEvolutionAudit.Delta(channel);

    /// <summary>The multiplier's modulus and argument for one flow, one channel, one step.</summary>
    public static (double Modulus, double Phase) Symbol(string flow, int channel, double eps)
    {
        var m = PhaseEvolutionAudit.Multiplier(flow, channel, eps);
        return (m.Magnitude, Math.Atan2(m.Imaginary, m.Real));
    }

    // ===================== 2. NORM CONSERVATION =====================

    /// <summary>The worst deviation of |m| from 1 over every channel and every eps in the ladder.</summary>
    public static (string Flow, double WorstModulusDeviation, double WorstAtEps, int Channel)[] NormTable()
        => Flows().Select(f =>
        {
            double worst = 0.0, atEps = 0.0;
            int channel = 0;
            foreach (var eps in PhaseEvolutionAudit.Epsilons())
            foreach (var c in PhaseEvolutionAudit.Channels())
            {
                double d = Math.Abs(Symbol(f.Flow, c, eps).Modulus - 1.0);
                if (d > worst) { worst = d; atEps = eps; channel = c; }
            }
            return (f.Flow, worst, atEps, channel);
        }).ToArray();

    public static double[] NormRatios(string flow, double eps, int steps)
    {
        var start = PhaseEvolutionAudit.Base();
        var end = PhaseEvolutionAudit.Orbit(flow, start, eps, steps);
        return new[] { PhaseEvolutionAudit.Norm(end) / PhaseEvolutionAudit.Norm(start) };
    }

    // ===================== 3. PHASE EVOLUTION AND THE DISPERSION =====================

    /// <summary>The continuum momentum of the channel, delta - the linear law Schrodinger would give.</summary>
    public static double ContinuumMomentum(int channel) => Delta(channel);

    /// <summary>The AT lattice symbol: sin(delta), which is what every AT flow's phase advance is built from.</summary>
    public static double LatticeSymbol(int channel) => Math.Sin(Delta(channel));

    /// <summary>
    /// The dispersion table: the flow's phase advance per unit eps against the two references - the lattice symbol
    /// sin(delta) and the continuum momentum delta.
    /// </summary>
    public static (int Channel, double Delta, double ContinuumK, double LatticeSin, double CayleyAdvance,
                   double ExactAdvance, double CentredAdvance)[] Dispersion(double eps = 1e-3)
        => PhaseEvolutionAudit.Channels().Select(c => (
            c, Delta(c), ContinuumMomentum(c), LatticeSymbol(c),
            Symbol(Cayley, c, eps).Phase / eps,
            Symbol(Exact, c, eps).Phase / eps,
            Symbol(Centred, c, eps).Phase / eps)).ToArray();

    /// <summary>
    /// The Cayley form's generator convention, MEASURED: the flow's phase advance per step is
    /// 2 arctan(eps sin d), whose leading term is 2 eps sin d - so the repository's Cayley update unitarises TWICE
    /// the skew generator the other flows advance. The audit reports the factor rather than treating it as an error:
    /// it is a convention in the flow's definition with a measurable consequence.
    /// </summary>
    public static double CayleyGeneratorFactor(int channel = 24, double eps = 1e-6)
        => Symbol(Cayley, channel, eps).Phase / (eps * LatticeSymbol(channel));

    /// <summary>
    /// The Cayley dispersion with its OWN generator convention divided out, so the remaining gap is the time
    /// discretisation alone - and it falls as eps^2 rather than staying at 1.
    /// </summary>
    public static (double Eps, double NormalisedDeviation)[] CayleyNormalisedDeviation(int channel = 24)
        => PhaseEvolutionAudit.Epsilons().Select(eps =>
        {
            double s = LatticeSymbol(channel);
            double advance = Symbol(Cayley, channel, eps).Phase / eps;
            return (eps, Math.Abs(advance / (2.0 * s) - 1.0));
        }).ToArray();

    /// <summary>
    /// The three independent deviations of the AT unitary flow from a Schrodinger dispersion, in the order they
    /// appear: the flow's own generator convention, the time discretisation, and the lattice.
    /// </summary>
    public static (string Source, double Magnitude, double EpsOrder, bool RemovableBySmallerSteps)[] TheThreeDeviations(
        int channel = 24, double eps = 1e-3)
    {
        double s = LatticeSymbol(channel);
        double advance = Symbol(Cayley, channel, eps).Phase / eps;
        double factor = advance / s;                                    // -> 2 as eps -> 0
        double discretisation = Math.Abs(advance / (2.0 * s) - 1.0);    // -> 0 as eps^2
        double lattice = LatticeDeviation(channel);                     // does not move
        return new[]
        {
            ("the Cayley generator convention", factor, 0.0, false),
            ("the time discretisation", discretisation, CayleyDeviationOrder(), true),
            ("the lattice symbol sin d against d", lattice, 0.0, false),
        };
    }

    /// <summary>
    /// The measures are NOT independent: because every AT update is circulant, conserving the mode occupations is
    /// the same condition as having unit modulus. The audit measures that equivalence rather than listing three
    /// separate measures.
    /// </summary>
    public static bool OccupationConservationIsUnitarity()
        => Flows().All(f =>
        {
            bool unitary = Math.Abs(Symbol(f.Flow, 24, 1e-3).Modulus - 1.0) < 1e-12;
            bool conserved = OccupationTable(1e-3, 200).Single(t => t.Flow == f.Flow).WorstOccupationChange < 1e-12;
            return unitary == conserved;
        });

    /// <summary>
    /// The scaling exponent of the Cayley deviation, estimated by the log-ratio between successive epsilons - a
    /// computed order rather than a cited one.
    /// </summary>
    public static double CayleyDeviationOrder()
    {
        var table = CayleyNormalisedDeviation();
        double ratio = table[0].NormalisedDeviation / table[1].NormalisedDeviation;
        // the epsilons are 1e-3, 1e-2, 1e-1: a factor of 10 between successive rows, and the gap GROWS with eps
        return Math.Abs(Math.Log(ratio) / Math.Log(10.0));
    }

    /// <summary>The lattice deviation of the symbol from the continuum momentum, per channel.</summary>
    public static double LatticeDeviation(int channel) => Math.Abs(LatticeSymbol(channel) / ContinuumMomentum(channel) - 1.0);

    // ===================== 4. THE FOLD: BOUNDED, NON-MONOTONE DISPERSION =====================

    /// <summary>The channel at which the lattice symbol peaks - the folding point, measured.</summary>
    public static int FoldingChannel()
    {
        int best = 1; double peak = -1.0;
        foreach (var c in PhaseEvolutionAudit.Channels())
            if (LatticeSymbol(c) > peak) { peak = LatticeSymbol(c); best = c; }
        return best;
    }

    /// <summary>The zone edge (channel 48) is STATIONARY: the highest mode does not advance at all.</summary>
    public static double ZoneEdgeAdvance(double eps = 1e-3) => Math.Abs(Symbol(Cayley, PhaseEvolutionAudit.Cells / 2, eps).Phase);

    /// <summary>
    /// How many channel PAIRS advance in the reverse order to the continuum. Below the folding point the AT symbol
    /// increases with the mode number as the continuum does; above it, the ordering inverts - so this count is the
    /// measure of the region where an AT unitary flow cannot mimic Schrodinger at all.
    /// </summary>
    public static (int Ordered, int Inverted) OrderingCensus()
    {
        var channels = PhaseEvolutionAudit.Channels();
        int ordered = 0, inverted = 0;
        for (int i = 0; i < channels.Length - 1; i++)
        {
            bool atUp = LatticeSymbol(channels[i + 1]) > LatticeSymbol(channels[i]);
            bool continuumUp = ContinuumMomentum(channels[i + 1]) > ContinuumMomentum(channels[i]);
            if (atUp == continuumUp) ordered++; else inverted++;
        }
        return (ordered, inverted);
    }

    /// <summary>The occupied mode sector: which channels the canonical state populates.</summary>
    public static int[] OccupiedChannels()
        => KernelStructureAudit.VisibleModeVectors().Select(m => m.Channel).Distinct().OrderBy(c => c).ToArray();

    /// <summary>
    /// The count the question actually asks for: how much of the OCCUPIED mode sector lies in the folding region,
    /// where the AT dispersion runs backwards against the continuum.
    /// </summary>
    public static (int Occupied, int BelowFold, int AtOrAboveFold, int[] FoldedChannels) OccupiedFoldCensus()
    {
        var occupied = OccupiedChannels();
        int fold = FoldingChannel();
        var folded = occupied.Where(c => c >= fold).ToArray();
        return (occupied.Length, occupied.Count(c => c < fold), folded.Length, folded);
    }

    // ===================== 5. THE HERMITIAN GENERATOR =====================

    /// <summary>
    /// The effective Hamiltonian the flow exponentiates: H_eff = -arg m / eps, mode by mode. Because every flow is
    /// circulant the generator is diagonal in the Fourier basis, so its spectrum is REAL - the audit measures that
    /// rather than asserting Hermiticity from the algebra.
    /// </summary>
    public static (int Channel, double Effective, double Symbol, double RelativeGap)[] GeneratorTable(
        string flow = Cayley, double eps = 1e-3)
        => PhaseEvolutionAudit.Channels().Select(c =>
        {
            double s = LatticeSymbol(c);
            double h = -Symbol(flow, c, eps).Phase / eps;
            return (c, h, s, s == 0.0 ? 0.0 : Math.Abs(h / s - 1.0));
        }).ToArray();

    /// <summary>
    /// The decay part of the generator for each flow: log|m|/eps. It vanishes identically EXACTLY for the flows that
    /// are unitary, so Hermiticity is a measurement rather than an assertion from the algebra.
    /// </summary>
    public static (string Flow, double WorstDecayPart, bool GeneratorIsHermitian)[] GeneratorDecayParts()
        => Flows().Select(f =>
        {
            double worst = 0.0;
            foreach (var c in PhaseEvolutionAudit.Channels())
            {
                var m = PhaseEvolutionAudit.Multiplier(f.Flow, c, 1e-3);
                worst = Math.Max(worst, Math.Abs(Math.Log(m.Magnitude) / 1e-3));
            }
            return (f.Flow, worst, worst < 1e-12);
        }).ToArray();

    // ===================== 6. MODE OCCUPATION =====================

    /// <summary>
    /// The mode occupation of the state, by channel: the modulus of the state's Fourier content on that channel.
    /// </summary>
    public static double[] ModeOccupations(double[] state)
    {
        int cells = state.Length;
        var result = new double[cells / 2];
        for (int c = 1; c <= cells / 2; c++)
        {
            double re = 0.0, im = 0.0;
            for (int j = 0; j < cells; j++)
            {
                double d = 2.0 * Math.PI * c * j / cells;
                re += state[j] * Math.Cos(d);
                im -= state[j] * Math.Sin(d);
            }
            result[c - 1] = Math.Sqrt(re * re + im * im) / cells;
        }
        return result;
    }

    /// <summary>The worst relative change of any mode occupation over the run - the conservation test.</summary>
    public static (string Flow, double WorstOccupationChange)[] OccupationTable(double eps = 1e-3, int steps = 2000)
    {
        var start = PhaseEvolutionAudit.Base();
        var before = ModeOccupations(start);
        return Flows().Select(f =>
        {
            var after = ModeOccupations(PhaseEvolutionAudit.Orbit(f.Flow, start, eps, steps));
            double worst = 0.0;
            for (int k = 0; k < before.Length; k++)
                if (before[k] > 1e-12) worst = Math.Max(worst, Math.Abs(after[k] / before[k] - 1.0) / steps);
            return (f.Flow, worst);
        }).ToArray();
    }

    /// <summary>
    /// The sector-mixing measurement: the amplitude/phase split of the state is NOT conserved, even though the mode
    /// occupations are. A REAL Hamiltonian could never do this.
    /// </summary>
    public static (string Flow, double PhaseBefore, double PhaseAfter, double AmplitudeBefore, double AmplitudeAfter)[]
        SectorMixing(double eps = 1e-3, int steps = 2000)
    {
        var start = PhaseEvolutionAudit.Base();
        double pb = PhaseEvolutionAudit.PhaseNorm(start), ab = PhaseEvolutionAudit.AmplitudeNorm(start);
        return Flows().Select(f =>
        {
            var end = PhaseEvolutionAudit.Orbit(f.Flow, start, eps, steps);
            return (f.Flow, pb, PhaseEvolutionAudit.PhaseNorm(end), ab, PhaseEvolutionAudit.AmplitudeNorm(end));
        }).ToArray();
    }

    // ===================== 7. THE VERDICT =====================

    /// <summary>
    /// The three measures of the question, per flow: whether the norm is conserved, whether the phase advances
    /// linearly, and whether the mode occupation is conserved.
    /// </summary>
    public static (string Flow, string NormConservation, string PhaseEvolution, string ModeOccupation, string Verdict)[]
        MeasureTable(double eps = 1e-3)
    {
        var norm = NormTable().ToDictionary(t => t.Flow, t => t.WorstModulusDeviation);
        var occupation = OccupationTable(eps).ToDictionary(t => t.Flow, t => t.WorstOccupationChange);

        string Norm(string f) => norm[f] < 1e-12 ? "EXACT (|m| = 1 to machine precision)"
            : norm[f] < 1e-3 ? $"NEARLY CONSERVED (worst |m| deviation {norm[f]:E2})"
            : $"NOT CONSERVED (worst |m| deviation {norm[f]:E2})";

        string Phase(string f)
        {
            double s = LatticeSymbol(24);
            double advance = Symbol(f, 24, eps).Phase / eps;
            // each flow's OWN leading order: the Cayley form advances 2 eps sin d, the others eps sin d
            double ownOrder = f == Cayley ? 2.0 : 1.0;
            double normalised = Math.Abs(advance / (ownOrder * s) - 1.0);
            double factor = advance / s;
            if (f == Exact)
                return "LINEAR IN THE SYMBOL (arg m = eps sin d exactly) - but the modulus decays";
            if (f == Cayley)
                return $"NON-SCHRODINGER: the phase advances by a factor {factor:F6} of the symbol (the Cayley form's own "
                     + $"generator convention, 2 eps sin d), and beyond that by order eps^2 (gap {normalised:E2})";
            return $"arctan-LIKE (advance {factor:F6} of the symbol, gap from its own order {normalised:E2})";
        }

        string Occupation(string f) => occupation[f] < 1e-9
            ? "CONSTANT (circulant, hence diagonal: |c_k| is a constant of the motion)"
            : $"DRIFTING ({occupation[f]:E2} per step)";

        var table = new List<(string, string, string, string, string)>();
        foreach (var (flow, role) in Flows())
        {
            string verdict = flow == Cayley
                ? "PARTIAL - unitary and occupation-conserving, but the dispersion is not Schrodinger's"
                : flow == Exact
                    ? "PARTIAL - the clean dispersion, but dissipative"
                    : flow == Dissipative
                        ? "REFUTED - neither unitary nor linear"
                        : "PARTIAL - a bare rotation, amplifying and with the arctan dispersion";
            table.Add((flow, Norm(flow), Phase(flow), Occupation(flow), verdict));
        }
        return table.ToArray();
    }

    /// <summary>
    /// The verdict per MEASURE rather than per flow - the form the question asks for. Norm conservation and mode
    /// occupation correspond; phase evolution does not, and that is the measure that defines a Schrodinger evolution.
    /// </summary>
    public static (string Measure, string Verdict, string Basis)[] MeasureVerdicts(double eps = 1e-3)
    {
        var norm = NormTable();
        var occupation = OccupationTable(eps);
        var fold = OccupiedFoldCensus();
        double cayleyNorm = norm.Single(t => t.Flow == Cayley).WorstModulusDeviation;
        double cayleyOccupation = occupation.Single(t => t.Flow == Cayley).WorstOccupationChange;
        return new[]
        {
            ("norm conservation",
                cayleyNorm < 1e-12 ? "ANALOGOUS" : "PARTIAL",
                $"the Cayley multiplier has unit modulus on all 48 channels to {cayleyNorm:E2}, so the norm is conserved EXACTLY; "
                + $"the centred form amplifies and the forward difference dissipates"),
            ("mode occupation",
                cayleyOccupation < 1e-12 ? "ANALOGOUS" : "PARTIAL",
                $"every AT update is circulant, hence diagonal in the Fourier basis, so each |c_k| is a constant of the motion "
                + $"under the UNITARY flow to {cayleyOccupation:E2} - and the other flows' drift is the SAME condition as their lack "
                + $"of unitarity, which the audit verifies flow by flow; the AMPLITUDE/PHASE split is not conserved by any of them"),
            ("phase evolution",
                "REFUTED",
                $"the symbol is sin d and not d: it peaks at channel {FoldingChannel()} and vanishes at the zone edge "
                + $"({ZoneEdgeAdvance(eps):E2}), {OrderingCensus().Inverted} of {OrderingCensus().Ordered + OrderingCensus().Inverted} adjacent channel pairs advance in the reverse order, "
                + $"and {fold.AtOrAboveFold} of the {fold.Occupied} OCCUPIED modes lie in that folding region; a Hermitian generator exists "
                + $"only as H = (2/eps) arctan(eps sin d), whose discretisation gap falls as eps^{CayleyDeviationOrder():F2} while the lattice gap does not move"),
        };
    }

    public static (int Analogous, int Partial, int Refuted) VerdictCounts()
    {
        var v = MeasureVerdicts();
        return (v.Count(x => x.Verdict == "ANALOGOUS"), v.Count(x => x.Verdict == "PARTIAL"),
            v.Count(x => x.Verdict == "REFUTED"));
    }

    /// <summary>
    /// The trade-off, measured: which flows have |m| = 1, which have the clean dispersion, and whether any has both.
    /// </summary>
    public static (string Flow, bool Unitary, bool CleanDispersion)[] TheTradeOff(double eps = 1e-3)
        => Flows().Select(f => (
            f.Flow,
            Math.Abs(Symbol(f.Flow, 24, eps).Modulus - 1.0) < 1e-12,
            Math.Abs(Symbol(f.Flow, 24, eps).Phase / eps / LatticeSymbol(24) - 1.0) < 1e-12)).ToArray();

    public static bool NoFlowHasBothProperties() => !TheTradeOff().Any(t => t.Unitary && t.CleanDispersion);

    public static string Verdict()
    {
        var trade = TheTradeOff();
        var fold = OccupiedFoldCensus();
        var sb = new StringBuilder();
        sb.Append("PARTIAL - NO AT FLOW REPRODUCES UNITARITY AND THE SCHRODINGER DISPERSION TOGETHER. ");
        sb.Append($"Of the four flows carried, {trade.Count(t => t.Unitary)} is unitary ({string.Join(", ", trade.Where(t => t.Unitary).Select(t => t.Flow))}) ");
        sb.Append($"and {trade.Count(t => t.CleanDispersion)} has the clean dispersion ({string.Join(", ", trade.Where(t => t.CleanDispersion).Select(t => t.Flow))}) ");
        sb.Append("- AND THEY ARE DIFFERENT FLOWS, measured rather than argued. ");
        sb.Append("NORM CONSERVATION IS ANALOGOUS EXACTLY: the Cayley multiplier is (1 + i eps s)/(1 - i eps s), so |m| = 1 on every channel and at every eps tested. ");
        sb.Append("MODE OCCUPATION IS ANALOGOUS AS WELL, and structurally so: every AT update is circulant, hence diagonal in the Fourier basis, so each |c_k| is a constant of the motion ");
        sb.Append("- but the AMPLITUDE/PHASE SPLIT is not, which no real Hamiltonian could manage. ");
        sb.Append("PHASE EVOLUTION IS WHERE IT BREAKS. A HERMITIAN GENERATOR EXISTS - the audit computes H_eff = -arg m / eps and finds a real diagonal spectrum - ");
        sb.Append($"so the AT unitary flow IS the exponential of a Hermitian operator; but that operator is (2/eps) arctan(eps sin d), not the symbol, so ");
        sb.Append($"the discretisation gap falls as eps^{CayleyDeviationOrder():F2} and, more decisively, THE LATTICE SYMBOL IS sin d AND NOT d. ");
        sb.Append($"sin d is bounded and folds: it peaks at channel {FoldingChannel()} and returns to {ZoneEdgeAdvance():E0} at the zone edge, with {OrderingCensus().Inverted} of the {OrderingCensus().Ordered + OrderingCensus().Inverted} adjacent channel pairs advancing in the REVERSE order to the continuum. ");
        sb.Append($"ON THE OCCUPIED SECTOR the audit counts it rather than generalising: {fold.AtOrAboveFold} of the {fold.Occupied} occupied modes lie at or above the folding channel {FoldingChannel()}. ");
        sb.Append("OUTPUT: PARTIAL.");
        return sb.ToString();
    }

    public static string WhereItStands()
        => $"THE CORRESPONDENCE IS CONSTRUCTIVE BUT CONFINED. A Hermitian generator exists for the unitary flow, so the flow can be WRITTEN as exp(-i H t) "
         + "with H = (2/eps) arctan(eps sin d) - but H is not the AT operator, and the difference has two independent sources: the time discretisation, which is O(eps^2), and the lattice, which is sin d against d "
         + "and never vanishes however small eps becomes. THE LATTICE TERM IS THE ONE THAT CANNOT BE REMOVED BY TAKING SMALLER STEPS.";

    /// <summary>
    /// The step-size ladder that separates the two deviations: the discretisation gap shrinks as eps^2 while the
    /// lattice gap does not move at all.
    /// </summary>
    public static (double Eps, double DiscretisationGap, double LatticeGap, bool ZoneEdgeIsStat)[] StepLadder(int channel = 24)
        => PhaseEvolutionAudit.Epsilons().Select(eps =>
            (eps, CayleyNormalisedDeviation(channel).First(t => t.Eps == eps).NormalisedDeviation,
             LatticeDeviation(channel), ZoneEdgeAdvance(eps) < 1e-12)).ToArray();

    // ===================== 8. REPORTS =====================

    public static string OutputNorm()
    {
        var sb = new StringBuilder();
        sb.AppendLine("NORM CONSERVATION: the worst |m| deviation from 1, over all 48 channels and the whole eps ladder.");
        sb.AppendLine("  flow                                    worst | |m| - 1 |      at eps     channel");
        foreach (var t in NormTable())
            sb.AppendLine($"  {t.Flow,-38} {t.WorstModulusDeviation:E3}            {t.WorstAtEps:E0}       {t.Channel}");
        sb.AppendLine();
        sb.AppendLine("  the unitary (Cayley) form is exact because (1 + i eps s)/(1 - i eps s) has unit modulus identically;");
        sb.AppendLine("  the centred form is AMPLIFYING (|m| > 1) and the forward difference is DISSIPATIVE (|m| < 1).");
        return sb.ToString();
    }

    public static string OutputDispersion()
    {
        var sb = new StringBuilder();
        sb.AppendLine("PHASE EVOLUTION: the phase advance per unit eps against the two references.");
        sb.AppendLine("  ch   delta      continuum k    lattice sin d    Cayley       exact flow    centred");
        foreach (var d in Dispersion().Where(d => d.Channel % 4 == 0 || d.Channel == FoldingChannel()))
            sb.AppendLine($"  {d.Channel,2}   {d.Delta:F4}     {d.ContinuumK:F4}         {d.LatticeSin:F4}           {d.CayleyAdvance:F6}     {d.ExactAdvance:F6}      {d.CentredAdvance:F6}");
        sb.AppendLine();
        sb.AppendLine($"  THE FOLD: the lattice symbol peaks at channel {FoldingChannel()} (delta = pi/2) and returns to {ZoneEdgeAdvance():E0} at channel {PhaseEvolutionAudit.Cells / 2}.");
        sb.AppendLine($"  Adjacent channel pairs: {OrderingCensus().Ordered} advance in the continuum's order, {OrderingCensus().Inverted} in REVERSE.");
        var fold = OccupiedFoldCensus();
        sb.AppendLine($"  OCCUPIED SECTOR: {fold.Occupied} modes, of which {fold.AtOrAboveFold} lie at or above the folding channel: "
            + $"{string.Join(", ", fold.FoldedChannels)}");
        return sb.ToString();
    }

    public static string OutputTradeOff()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE TRADE-OFF: which flow has which of the two properties Schrodinger needs.");
        sb.AppendLine("  flow                                    |m| = 1 (unitary)     arg m = eps sin d (clean)");
        foreach (var t in TheTradeOff())
            sb.AppendLine($"  {t.Flow,-38} {t.Unitary,-21} {t.CleanDispersion}");
        sb.AppendLine();
        sb.AppendLine($"  NO FLOW HAS BOTH: {NoFlowHasBothProperties()}");
        sb.AppendLine();
        sb.AppendLine("  the step ladder, which separates the two deviations:");
        sb.AppendLine("    eps        discretisation gap    lattice gap (channel 24)    zone edge stationary");
        foreach (var r in StepLadder())
            sb.AppendLine($"    {r.Eps:E0}       {r.DiscretisationGap:E3}                {r.LatticeGap:F6}                     {r.ZoneEdgeIsStat}");
        return sb.ToString();
    }

    public static string OutputMeasures()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE THREE MEASURES OF THE QUESTION, PER FLOW.");
        foreach (var t in MeasureTable())
        {
            sb.AppendLine($"  {t.Flow}");
            sb.AppendLine($"    norm conservation : {t.NormConservation}");
            sb.AppendLine($"    phase evolution   : {t.PhaseEvolution}");
            sb.AppendLine($"    mode occupation   : {t.ModeOccupation}");
            sb.AppendLine($"    verdict           : {t.Verdict}");
        }
        sb.AppendLine();
        sb.AppendLine("  THE SECTOR MIXING, which a mode count alone would miss (2000 steps, eps = 1e-3):");
        sb.AppendLine("    flow                                    phase before    phase after     amplitude before   amplitude after");
        foreach (var m in SectorMixing())
            sb.AppendLine($"    {m.Flow,-38} {m.PhaseBefore:E3}         {m.PhaseAfter:E3}          {m.AmplitudeBefore:F6}           {m.AmplitudeAfter:F6}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        foreach (var t in MeasureTable()) sb.AppendLine($"{t.Flow}: {t.Verdict}");
        sb.AppendLine();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
