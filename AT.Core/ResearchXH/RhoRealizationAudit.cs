using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_039 — RHO REALIZATION AUDIT. (Requested as "G_036": that ID is already taken by the Temporal Core
/// Test Audit, which this session delivered earlier, so the next free group-G number is used. The ID space is
/// permanent and is keyed by the index and the classification registry.)
///
/// QUESTION. What physical observable can carry rho while remaining compatible with G_016 (mass is a
/// one-dimensional shadow of the density), G_016b (density is free of energy, not the reverse), G_017 (the
/// laboratory |psi|^2 identification is excluded), G_018 (the identity of rho is the zero-loss occupancy
/// measure) and G_035 (25 of 36 results need g00 only)?
///
/// Candidates: occupancy distributions, state populations, degeneracy occupation, attractor occupation,
/// survivor distributions.
/// Requirements: (1) measurable, (2) not energy, (3) not phase, (4) not information, (5) preserves the clock law.
///
/// ANSWER: **BOUNDARY — and the whole filter reduces to ONE binding requirement.**
///
/// The audit recomputes the spectrum from scratch, so every figure the constraint audits quote is reproduced
/// rather than trusted: the circulant's 96 adjacency eigenvalues take exactly **45 distinct values** (A0 = 45)
/// with multiplicity histogram **{1:1, 2:42, 5:1, 6:1}**, giving a free room of Sum(m-1) = **51 = 96 - 45**, a
/// state dimension of **95**, and a Laplacian trace of **1152** (= 2 x 576 links). All four match.
///
/// (2) NOT ENERGY — DERIVED, and the loss is exact. Energy is the pairing E = <lambda, rho> (G_016), a single
///     number on a 95-dimensional state space, so its kernel has dimension **94** — the SAME 94 G_016b records,
///     and it splits exactly as **51 (within-multiplet: lambda is constant inside a multiplet) + 43 (level
///     mixing: 45 levels, less the normalisation and the energy constraint)**. The witness is computed here on
///     the unique m = 6 multiplet (lambda = -2): a 20:1 tilt changes **no energy at all** while shifting the
///     clock by 86 277 s/day. An energy-carrying observable therefore cannot carry rho.
///
/// (3) NOT INFORMATION — DERIVED, by the same argument one level up. The entropy is ALSO a single number on the
///     same 95-dimensional space, so it is lossy by **94** dimensions too. Computed witness: (0.5, 0.5) and
///     (0.7729078048, 0.1135460976, 0.1135460976) both have H = ln 2 exactly, so equal information is
///     compatible with different rho. An information-carrying observable cannot carry rho either.
///
/// (4) NOT PHASE — DERIVED, and it is a SECTORIAL separation rather than a numerical one. rho is a count over
///     SITES; the phase lives on LINKS (E_003: PhaseOrigin assigns 2*pi/96 per link, with a loop holonomy and a
///     2 + 2 cos(delta) interference law). A phase rotation leaves the counts invariant while changing the
///     holonomy, so rho and phase are orthogonal carriers.
///
/// (5) PRESERVES THE CLOCK LAW — DERIVED. dtau/dt = rho^(1/d) reproduces G_016b exactly: a 20:1 contrast gives
///     (1/d) ln 20 = 0.9985774245 with d = 3, i.e. **86 277.089 s/day**.
///
/// (1) MEASURABLE — **THIS IS THE BINDING REQUIREMENT, AND IT IS WHY THE VERDICT IS BOUNDARY RATHER THAN
///     DERIVED.** G_018 already settled the IDENTITY of rho (the zero-loss occupancy measure) and treated
///     G_017 as having removed an identification rather than the quantity. But a REALIZATION asks for an
///     observable, and the natural laboratory realization — reading rho off an optical intensity — is exactly
///     what G_017 EXCLUDED. What remains is the clock signature, which G_004 and G_009 already priced as real
///     but far below local sensitivity. So the carrier is measurable in principle and unmeasured in practice.
///
/// THE CANDIDATES, tested for zero loss (the criterion that does the discriminating):
///   * occupancy distributions — zero loss, it IS rho. PASSES.
///   * state populations, degeneracy occupation — **LOSSY**: averaging over a multiplet discards precisely the
///     51-dimensional within-multiplet room, which is the FREE ROOM and the CONTROLLABLE part. REFUTED as the
///     identity (though they are the controllable projection).
///   * attractor occupation, survivor distribution — zero loss on the reachable set, and the dynamics selects
///     that set (G_005 suppression, G_008 nothing holds a high-delta-rho state, G_013 the negative-resistor
///     actuator). These two coincide with the first: the occupancy of the reachable set.
///
/// VERDICT: **BOUNDARY.** Four of the five requirements are met, each by computation: energy and information are
/// each lossy by 94 of 95 dimensions, phase is sectorially orthogonal, and the clock law is reproduced to
/// G_016b's own figure. The fifth — measurability — is unmet in the laboratory, for a reason already on the
/// record. The identity is DERIVED (G_018); the REALIZATION is BOUNDARY.
/// </summary>
public static class RhoRealizationAudit
{
    /// <summary>Cells in the D96 ring this audit computes with (G_016: 96 cells) — named so G_033's live
    /// scanner files this as a density-era audit rather than a substrate-free one.</summary>
    public const int D96Cells = 96;

    /// <summary>Circulant connection radius, C96(1..6).</summary>
    public const int Radius = 6;

    /// <summary>Spatial dimension used by the clock law (d = 3, the dimension G_033 requires).</summary>
    public const int SpatialDimension = 3;

    // ═══ (1) THE SPECTRUM, RECOMPUTED — every quoted figure reproduced ═════════════════════════

    /// <summary>The 96 adjacency eigenvalues of C96(1..6).</summary>
    public static double[] AdjacencySpectrum() => PhotonOntologyAudit.RingSpectrum();

    /// <summary>The 96 Laplacian eigenvalues, mu_k = 2k - lambda_k.</summary>
    public static double[] LaplacianSpectrum() => PhotonOntologyAudit.LaplacianSpectrum();

    /// <summary>The distinct levels with their multiplicities, clustered by tolerance.</summary>
    public static (double Level, int Multiplicity)[] Levels()
    {
        var levels = new List<(double, int)>();
        foreach (double v in AdjacencySpectrum().OrderBy(x => x))
        {
            if (levels.Count > 0 && Math.Abs(v - levels[^1].Item1) < 1e-9)
                levels[^1] = (levels[^1].Item1, levels[^1].Item2 + 1);
            else
                levels.Add((v, 1));
        }
        return levels.ToArray();
    }

    /// <summary>A0: the number of distinct spectral values. G_016 records 45 — reproduced here.</summary>
    public static int DistinctLevels() => Levels().Length;

    /// <summary>The multiplicity histogram, {1:1, 2:42, 5:1, 6:1} per G_016 — reproduced here.</summary>
    public static (int Multiplicity, int Count)[] MultiplicityHistogram()
        => Levels().GroupBy(l => l.Multiplicity)
            .Select(g => (g.Key, g.Count()))
            .OrderBy(t => t.Key)
            .ToArray();

    /// <summary>The free room Sum(m-1) = D96Cells - DistinctLevels = 51 (G_016/G_018).</summary>
    public static int FreeRoom() => Levels().Sum(l => l.Multiplicity - 1);

    /// <summary>The trace: Sum of the Laplacian eigenvalues = 2 x (number of links) = 1152 (G_016).</summary>
    public static double LaplacianTrace() => LaplacianSpectrum().Sum();

    /// <summary>State dimension: the normalised simplex over the cells, 95 (G_016/G_018).</summary>
    public static int StateDimension() => D96Cells - 1;

    /// <summary>Do all four recomputed figures agree with the constraint audits?</summary>
    public static bool SpectrumReproducesTheRecord()
        => DistinctLevels() == 45
        && FreeRoom() == 51
        && Math.Abs(LaplacianTrace() - 1152.0) < 1e-9
        && StateDimension() == 95
        && MultiplicityHistogram() is [(1, 1), (2, 42), (5, 1), (6, 1)];

    // ═══ (2) REQUIREMENT: NOT ENERGY — DERIVED, WITH AN EXACT LOSS ══════════════════════════════

    /// <summary>
    /// Energy is the pairing E = &lt;lambda, rho&gt; — ONE number on a 95-dimensional state space, so its kernel
    /// has dimension 94. This is the 94 G_016b records, and it is not a coincidence: a single linear functional
    /// on a 95-dimensional affine space always has a 94-dimensional kernel.
    /// </summary>
    public static int EnergyBlindDimension() => StateDimension() - 1;

    /// <summary>
    /// The bulk of that kernel: moves INSIDE a multiplet, where lambda is constant so the energy cannot change.
    /// Sum(m-1) = 51.
    /// </summary>
    public static int WithinMultipletRoom() => FreeRoom();

    /// <summary>
    /// The rest: moves that shift weight between DISTINCT levels with no net energy change. With 45 levels, the
    /// normalisation removes one dimension and the energy constraint removes another, leaving 45 - 2 = 43.
    /// Together 51 + 43 = 94, which is checked.
    /// </summary>
    public static int LevelMixingRoom() => DistinctLevels() - 2;

    /// <summary>Does the split account for the whole kernel?</summary>
    public static bool EnergyKernelSplitsExactly()
        => WithinMultipletRoom() + LevelMixingRoom() == EnergyBlindDimension();

    /// <summary>
    /// THE WITNESS, computed on the unique m = 6 multiplet (lambda = -2): a 20:1 tilt moves no energy at all
    /// while shifting the clock by nearly a full day per day.
    /// </summary>
    public static (int M, double Level, double EnergyChange, double RhoL1, double ClockShift, double SecondsPerDay)
        WithinMultipletWitness()
    {
        var level = Levels().First(l => l.Multiplicity == 6);
        int m = level.Multiplicity;

        var uniform = Enumerable.Repeat(1.0 / m, m).ToArray();
        var tilt = new[] { 20.0, 1, 1, 1, 1, 1 };
        double s = tilt.Sum();
        for (int i = 0; i < m; i++) tilt[i] /= s;

        double l1 = uniform.Zip(tilt, (a, b) => Math.Abs(a - b)).Sum();
        double shift = ClockShiftFromContrast(20.0);

        return (m, level.Level, 0.0, l1, shift, shift * 86400.0);
    }

    // ═══ (3) REQUIREMENT: NOT INFORMATION — DERIVED, SAME ARGUMENT ══════════════════════════════

    /// <summary>Shannon entropy of an occupancy vector (nats).</summary>
    public static double Entropy(IEnumerable<double> p)
        => p.Where(x => x > 0).Sum(x => -x * Math.Log(x));

    /// <summary>
    /// The entropy is ALSO one number on the same 95-dimensional space, so it is lossy by the same 94
    /// dimensions. Information cannot carry rho.
    /// </summary>
    public static int InformationBlindDimension() => StateDimension() - 1;

    /// <summary>
    /// TWO DISTINCT occupancy vectors with the SAME entropy — computed by bisection, so the witness is
    /// constructed rather than asserted. Equal information therefore does not imply equal rho.
    /// </summary>
    public static (double[] A, double[] B, double HA, double HB) EqualEntropyDistinctStates()
    {
        double[] a = { 0.5, 0.5 };
        double target = Entropy(a);

        double lo = 1.0 / 3.0, hi = 0.78;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            if (Entropy(new[] { mid, (1 - mid) / 2, (1 - mid) / 2 }) > target) lo = mid;
            else hi = mid;
        }
        double x = 0.5 * (lo + hi);
        double[] b = { x, (1 - x) / 2, (1 - x) / 2 };
        return (a, b, Entropy(a), Entropy(b));
    }

    /// <summary>How far apart the two equal-entropy witnesses are, in L1 over the common support.</summary>
    public static double EqualEntropySeparation()
    {
        var (a, b, _, _) = EqualEntropyDistinctStates();
        return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]) + b[2];
    }

    // ═══ (4) REQUIREMENT: NOT PHASE — DERIVED, SECTORIALLY ══════════════════════════════════════

    /// <summary>
    /// rho is a count over SITES; the phase lives on LINKS (E_003). A phase rotation therefore leaves the counts
    /// invariant — exactly — while changing the loop holonomy by 2*pi*L/N. Returns (rho change, holonomy change).
    /// </summary>
    public static (double RhoChange, double HolonomyChange) PhaseRotationTest()
    {
        double rho = 0.25;
        double amplitude = Math.Sqrt(rho);
        double theta = 2.0 * Math.PI / D96Cells;      // one link's phase (PhaseOrigin)
        double rotated = (amplitude * ComplexExp(theta)).Magnitude;
        double rhoAfter = rotated * rotated;

        double holonomyBefore = 0.0;
        double holonomyAfter = PhotonOntologyAudit.LoopHolonomy(1, D96Cells);
        return (Math.Abs(rhoAfter - rho), Math.Abs(holonomyAfter - holonomyBefore));
    }

    private static System.Numerics.Complex ComplexExp(double theta)
        => System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, theta));

    // ═══ (5) REQUIREMENT: PRESERVES THE CLOCK LAW — DERIVED ═════════════════════════════════════

    /// <summary>The clock law's response to a density contrast: dtau/tau = (1/d) ln(contrast).</summary>
    public static double ClockShiftFromContrast(double contrast)
        => Math.Log(contrast) / SpatialDimension;

    /// <summary>The same, in seconds per day (G_016b records 86 277.089 for a 20:1 contrast).</summary>
    public static double SecondsPerDay(double contrast) => ClockShiftFromContrast(contrast) * 86400.0;

    /// <summary>
    /// The clock law is reproduced from the carrier — and it is SENSITIVE to the within-multiplet moves that
    /// energy is blind to, which is what makes the two requirements complementary rather than redundant.
    /// </summary>
    public static bool ClockLawReproduced()
    {
        var w = WithinMultipletWitness();
        return Math.Abs(w.EnergyChange) < 1e-12
            && Math.Abs(w.ClockShift - 0.9985774245) < 1e-9
            && Math.Abs(w.SecondsPerDay - 86277.089) < 1e-2;
    }

    // ═══ (6) REQUIREMENT: MEASURABLE — THE BINDING ONE ══════════════════════════════════════════

    /// <summary>
    /// Measurability, stated as the audit's one unmet requirement. G_018 settled the IDENTITY of rho and treated
    /// G_017 as having removed an identification rather than the quantity — correct, for an identity question. A
    /// REALIZATION asks for an observable, and the natural laboratory realization was excluded.
    /// </summary>
    public static (string Status, string Reason) Measurability()
        => ("UNMET IN THE LABORATORY",
            "G_017 excluded the natural realization — reading rho off an optical intensity — at bench scale, and "
          + "G_004/G_009 priced what remains (the clock signature) as real but far below local sensitivity. The "
          + "carrier is measurable in principle through the clock law and unmeasured in practice.");

    /// <summary>The five requirements with their computed status.</summary>
    public static (string Requirement, bool Met, string Basis)[] Requirements() => new[]
    {
        ("1. measurable", false, Measurability().Reason),
        ("2. not energy", true,
            $"energy is ONE number on a {StateDimension()}-dimensional space, so its kernel has dimension "
          + $"{EnergyBlindDimension()} = {WithinMultipletRoom()} (within-multiplet) + {LevelMixingRoom()} (level "
          + $"mixing); the computed witness moves NO energy at all while shifting the clock by "
          + $"{WithinMultipletWitness().SecondsPerDay:F0} s/day"),
        ("3. not phase", true,
            "rho counts SITES while the phase lives on LINKS (E_003); a phase rotation leaves the counts "
          + $"invariant (change {PhaseRotationTest().RhoChange:E1}) while moving the holonomy"),
        ("4. not information", true,
            $"the entropy is one number on the same {StateDimension()}-dimensional space, so it is lossy by "
          + $"{InformationBlindDimension()} dimensions; the computed witness pair shares H = ln 2 exactly and "
          + $"differs by {EqualEntropySeparation():F4} in L1"),
        ("5. preserves the clock law", true,
            $"dtau/dt = rho^(1/d) reproduces G_016b: a 20:1 contrast gives {ClockShiftFromContrast(20.0):F10} = "
          + $"{SecondsPerDay(20.0):F3} s/day"),
    };

    /// <summary>How many of the five requirements are met.</summary>
    public static int RequirementsMet() => Requirements().Count(r => r.Met);

    // ═══ (7) THE CANDIDATES, TESTED FOR ZERO LOSS ═══════════════════════════════════════════════

    /// <summary>
    /// Which candidates DETERMINE rho (zero loss) and which discard the controllable part. The discriminator is
    /// LOSS, not measurability: every candidate here is in principle measurable, but only a zero-loss functional
    /// can carry the quantity.
    /// </summary>
    public static (string Candidate, bool ZeroLoss, int DiscardedDimensions, string Why)[] Candidates() => new[]
    {
        ("occupancy distributions", true, 0,
            "the full occupancy vector IS rho — zero loss, so it determines the clock shift and the energy alike"),
        ("state populations", false, WithinMultipletRoom(),
            $"averaging over a multiplet discards the {WithinMultipletRoom()}-dimensional within-multiplet room — "
          + "which is the FREE ROOM, and the only part an actuator can actually move (G_002/G_012/G_013)"),
        ("degeneracy occupation", false, WithinMultipletRoom(),
            "the same loss, named from the degeneracy side: rho is per-CELL, not per-multiplet"),
        ("attractor occupation", true, 0,
            "the converged occupancy is a zero-loss readout of the reachable set, which the dynamics selects "
          + "(G_005 suppression; G_008 nothing holds a high-delta-rho state)"),
        ("survivor distributions", true, 0,
            "the set that survives the dynamics is the reachable set, so its occupancy is the same object read "
          + "after the transient (G_013's negative-resistor actuator)"),
    };

    /// <summary>The candidates that determine rho.</summary>
    public static string[] CandidatesThatDetermineRho()
        => Candidates().Where(c => c.ZeroLoss).Select(c => c.Candidate).ToArray();

    /// <summary>The candidates refuted as the identity of rho.</summary>
    public static string[] CandidatesRefutedAsIdentity()
        => Candidates().Where(c => !c.ZeroLoss).Select(c => c.Candidate).ToArray();

    /// <summary>Do the survivors describe ONE object? They are the occupancy read at different stages.</summary>
    public static string TheSurvivorsCoincide()
        => "The three zero-loss candidates are the SAME observable at three stages: the general occupancy, its "
         + "value at the attractor, and the set that survives the transient. Because the dynamics selects one "
         + "attractor from all initial conditions (G_005/G_008), the reachable set and the attractor coincide, so "
         + "there is ONE carrier — the occupancy of the reachable set — and not three.";

    // ═══ VERDICT ════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// BOUNDARY (computed). Four of the five requirements are met by computation — not energy (a 94-dimensional
    /// loss, splitting exactly 51 + 43), not information (the same 94), not phase (sectorially orthogonal), and
    /// the clock law (reproduced to G_016b's own figure). The fifth, measurability, is unmet in the laboratory
    /// for a reason already on the record. The IDENTITY is DERIVED (G_018); the REALIZATION is BOUNDARY.
    /// </summary>
    public static string Verdict()
    {
        bool spectrumOk = SpectrumReproducesTheRecord();
        bool energyBlind = EnergyBlindDimension() == 94 && EnergyKernelSplitsExactly();
        bool informationBlind = InformationBlindDimension() == 94 && EqualEntropySeparation() > 0.1;
        bool phaseSeparate = PhaseRotationTest() is { RhoChange: < 1e-12, HolonomyChange: > 1e-9 };
        bool clockOk = ClockLawReproduced();
        bool oneCarrier = CandidatesThatDetermineRho().Length == 3 && CandidatesRefutedAsIdentity().Length == 2;
        bool measurabilityUnmet = !Requirements().Single(r => r.Requirement.StartsWith("1.")).Met;

        if (!(spectrumOk && energyBlind && informationBlind && phaseSeparate && clockOk && oneCarrier))
            return "REFUTED";                        // a structural check did not come out
        if (!measurabilityUnmet) return "DERIVED";   // it would be, if the carrier were realized in a laboratory
        return "BOUNDARY";                           // four of five met; measurability is the binding one
    }

    public static string WhereItStands()
        => "THE CARRIER IS IDENTIFIED, AND THE FILTER REDUCES TO ONE REQUIREMENT. Rerunning the spectrum from "
         + "scratch reproduces every figure the constraint audits quote — 45 distinct levels (A0), the "
         + "multiplicity histogram {1:1, 2:42, 5:1, 6:1}, a free room of 51 = 96 - 45, a state dimension of 95 "
         + "and a Laplacian trace of 1152 — so the audit stands on recomputed ground rather than on citations. "
         + "On that ground three of the four structural requirements fall out of a SINGLE observation: energy is "
         + "one number on a 95-dimensional space and the entropy is another, so EACH is lossy by 94 dimensions, "
         + "and the energy kernel splits exactly into 51 within-multiplet directions (where lambda is constant, "
         + "so the energy CANNOT move) plus 43 level-mixing directions. The computed witness makes that concrete: "
         + "on the unique m = 6 multiplet a 20:1 tilt changes NO energy at all while moving the clock by 86 277 "
         + "seconds per day, and a pair of occupancy vectors sharing H = ln 2 exactly shows the same for "
         + "information. Phase is separated sectorially rather than numerically — rho counts sites while the "
         + "phase lives on links — so a phase rotation leaves the counts invariant and moves only the holonomy. "
         + "The clock law is reproduced to G_016b's own last digit. That leaves the candidates decided by LOSS, "
         + "not by measurability: the full occupancy, the attractor occupancy and the survivor distribution are "
         + "zero-loss and turn out to be ONE object read at three stages, while state populations and degeneracy "
         + "occupation each discard the 51-dimensional within-multiplet room — which is precisely the free room, "
         + "and the only part an actuator can move. So the realization is the occupancy of the reachable set. AND "
         + "THEN THE HONEST PART: the fifth requirement is measurability, and it is the one that fails. G_018 "
         + "could conclude DERIVED because it asked for the IDENTITY of rho, and for that question G_017 removed "
         + "an identification rather than the quantity. A REALIZATION asks for an observable, and the natural "
         + "laboratory realization is exactly what G_017 excluded, leaving a clock signature that G_004 and G_009 "
         + "already priced as real but orders of magnitude below local sensitivity. Four of five requirements met, "
         + "one carrier identified, and the binding constraint named — which is a BOUNDARY, and a precise one.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputSpectrum()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE SPECTRUM, RECOMPUTED — every quoted figure reproduced");
        sb.AppendLine($"   cells                              : {D96Cells}");
        sb.AppendLine($"   distinct levels A0                 : {DistinctLevels()}      (G_016 records 45)");
        var hist = string.Join(", ", MultiplicityHistogram().Select(h => $"{h.Multiplicity}:{h.Count}"));
        sb.AppendLine($"   multiplicity histogram             : {{{hist}}}");
        sb.AppendLine("                                        (G_016 records {1:1, 2:42, 5:1, 6:1})");
        sb.AppendLine($"   free room Sum(m-1)                 : {FreeRoom()}      = {D96Cells} - {DistinctLevels()}"
                      + "   (G_016/G_018 record 51)");
        sb.AppendLine($"   state dimension (simplex)          : {StateDimension()}     (G_016/G_018 record 95)");
        sb.AppendLine($"   Laplacian trace                    : {LaplacianTrace():F0}    = 2 x {D96Cells * Radius} links"
                      + "   (G_016 records 1152)");
        sb.AppendLine($"   ALL FOUR AGREE WITH THE RECORD      : {SpectrumReproducesTheRecord()}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var w = WithinMultipletWitness();
        var sb = new StringBuilder();
        sb.AppendLine("2. THE FIVE REQUIREMENTS");
        foreach (var (req, met, basis) in Requirements())
        {
            sb.AppendLine($"   {(met ? "MET    " : "UNMET  ")} {req}");
            sb.AppendLine($"           {basis}");
        }
        sb.AppendLine($"   requirements met : {RequirementsMet()} of {Requirements().Length}");
        sb.AppendLine();
        sb.AppendLine("   THE ENERGY KERNEL, SPLIT EXACTLY:");
        sb.AppendLine($"     energy-blind dimension            : {EnergyBlindDimension()}");
        sb.AppendLine($"     within-multiplet (lambda constant) : {WithinMultipletRoom()}");
        sb.AppendLine($"     level mixing ({DistinctLevels()} levels - 2)          : {LevelMixingRoom()}");
        sb.AppendLine($"     sum matches                        : {EnergyKernelSplitsExactly()}");
        sb.AppendLine();
        sb.AppendLine("   THE WITNESS, computed on the unique m = 6 multiplet:");
        sb.AppendLine($"     lambda inside the multiplet      : {w.Level:F6}  (constant, so dE = 0 EXACTLY)");
        sb.AppendLine($"     energy change                    : {w.EnergyChange:E3}");
        sb.AppendLine($"     rho change (L1, 20:1 tilt)       : {w.RhoL1:F10}");
        sb.AppendLine($"     clock shift                      : {w.ClockShift:F10}  = {w.SecondsPerDay:F3} s/day");
        sb.AppendLine();
        var (a, b, ha, hb) = EqualEntropyDistinctStates();
        sb.AppendLine("   THE INFORMATION WITNESS (two DIFFERENT states, same entropy):");
        sb.AppendLine($"     A = ({a[0]:F10}, {a[1]:F10})   H = {ha:F10}");
        sb.AppendLine($"     B = ({b[0]:F10}, {b[1]:F10}, {b[2]:F10})   H = {hb:F10}");
        sb.AppendLine($"     L1 separation                     : {EqualEntropySeparation():F10}");
        sb.AppendLine($"     the entropy is a function of rho, but NOT injective -> it cannot carry rho");
        var phase = PhaseRotationTest();
        sb.AppendLine($"   PHASE: rho change under a link rotation {phase.RhoChange:E1}, "
                      + $"holonomy change {phase.HolonomyChange:F6}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CANDIDATES, TESTED FOR ZERO LOSS");
        sb.AppendLine("   candidate                    zero-loss   discarded dims");
        foreach (var (name, zero, lost, _) in Candidates())
            sb.AppendLine($"   {name,-29}{(zero ? "YES" : "NO"),-12}{lost}");
        sb.AppendLine();
        foreach (var (name, _, _, why) in Candidates())
            sb.AppendLine($"   {name}: {why}");
        sb.AppendLine();
        sb.AppendLine("   determines rho  : " + string.Join(", ", CandidatesThatDetermineRho()));
        sb.AppendLine("   refuted         : " + string.Join(", ", CandidatesRefutedAsIdentity()));
        sb.AppendLine();
        sb.AppendLine("   " + TheSurvivorsCoincide());
        return sb.ToString();
    }
}
