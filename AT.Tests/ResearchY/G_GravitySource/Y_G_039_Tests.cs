using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_039 — Rho Realization Audit.
///
/// NOTE ON THE ID. This audit was requested as "G_036", but G_036 is already taken by the Temporal Core Test
/// Audit delivered earlier in the same session. The group-G ID space is permanent and is keyed by the index and
/// by the classification registry, so the next free number was used.
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
/// ANSWER: **BOUNDARY — and the filter reduces to ONE binding requirement.**
///
///  (1) The spectrum is RECOMPUTED, so every quoted figure is reproduced rather than trusted: 45 distinct levels,
///      the multiplicity histogram {1:1, 2:42, 5:1, 6:1}, free room 51, state dimension 95, trace 1152.
///  (2) NOT ENERGY — DERIVED. Energy is one number on a 95-dimensional space, so its kernel has dimension 94,
///      splitting EXACTLY as 51 (within-multiplet, where lambda is constant) + 43 (level mixing, 45 - 2). The
///      computed witness on the m = 6 multiplet moves NO energy while shifting the clock by 86 277 s/day.
///  (3) NOT INFORMATION — DERIVED, the same argument: the entropy is one number on the same space, so it too is
///      lossy by 94. The computed witness pair shares H = ln 2 exactly and differs by 0.77 in L1.
///  (4) NOT PHASE — DERIVED, sectorially: rho counts SITES, the phase lives on LINKS (E_003).
///  (5) PRESERVES THE CLOCK LAW — DERIVED, reproduced to G_016b's own figure.
///  (1) MEASURABLE — **THE BINDING ONE, and the reason the verdict is BOUNDARY.** G_018 could say DERIVED for the
///      IDENTITY of rho, because for that question G_017 removed an identification rather than the quantity. A
///      REALIZATION asks for an observable, and the natural laboratory realization is what G_017 excluded.
/// </summary>
public class Y_G_039_Tests : ResearchTestBase
{
    public Y_G_039_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_039_TheSpectrumIsRecomputedNotQuoted()
    {
        // Every figure the constraint audits quote, recomputed from the circulant.
        Assert.Equal(96, RhoRealizationAudit.D96Cells);
        Assert.Equal(45, RhoRealizationAudit.DistinctLevels());        // G_016's A0
        Assert.Equal(51, RhoRealizationAudit.FreeRoom());              // G_016/G_018
        Assert.Equal(95, RhoRealizationAudit.StateDimension());
        Assert.Equal(1152.0, RhoRealizationAudit.LaplacianTrace(), 6);
        // The trace is 2 x the link count; the circulant has degree 12, so 96 * 12 / 2 = 576 links.
        Assert.Equal(576, 96 * 12 / 2);
        Assert.Equal(1152, (int)Math.Round(RhoRealizationAudit.LaplacianTrace()));
        Assert.Equal(2 * (96 * 12 / 2), (int)Math.Round(RhoRealizationAudit.LaplacianTrace()));

        var hist = RhoRealizationAudit.MultiplicityHistogram();
        Assert.Equal(4, hist.Length);
        Assert.Equal((1, 1), hist[0]);
        Assert.Equal((2, 42), hist[1]);
        Assert.Equal((5, 1), hist[2]);
        Assert.Equal((6, 1), hist[3]);
        Assert.Equal(96, hist.Sum(h => h.Multiplicity * h.Count));

        Assert.True(RhoRealizationAudit.SpectrumReproducesTheRecord());
    }

    [Fact]
    public void Y_G_039_EnergyIsBlindTo94Of95Dimensions()
    {
        // Energy is ONE number on a 95-dimensional space.
        Assert.Equal(95, RhoRealizationAudit.StateDimension());
        Assert.Equal(94, RhoRealizationAudit.EnergyBlindDimension());

        // The kernel splits exactly, and both parts match the constraint audits.
        Assert.Equal(51, RhoRealizationAudit.WithinMultipletRoom());
        Assert.Equal(43, RhoRealizationAudit.LevelMixingRoom());
        Assert.True(RhoRealizationAudit.EnergyKernelSplitsExactly());
        Assert.Equal(94, RhoRealizationAudit.WithinMultipletRoom() + RhoRealizationAudit.LevelMixingRoom());

        // The witness: no energy at all, nearly a day per day of clock shift.
        var w = RhoRealizationAudit.WithinMultipletWitness();
        Assert.Equal(6, w.M);
        Assert.Equal(0.0, w.EnergyChange, 12);
        Assert.Equal(-2.0, w.Level, 9);
        Assert.True(w.RhoL1 > 1.0);
        // G_016b's own figure, to the precision it quotes: (1/3) ln 20 = 0.9985774245179969
        Assert.Equal(Math.Log(20.0) / 3.0, w.ClockShift, 12);
        Assert.Equal(0.9985774245, w.ClockShift, 8);
        Assert.Equal(86277.089, w.SecondsPerDay, 2);
    }

    [Fact]
    public void Y_G_039_InformationIsLossyByTheSame94Dimensions()
    {
        Assert.Equal(94, RhoRealizationAudit.InformationBlindDimension());
        Assert.Equal(RhoRealizationAudit.EnergyBlindDimension(), RhoRealizationAudit.InformationBlindDimension());

        // Two DIFFERENT occupancy vectors with the SAME entropy, computed by bisection.
        var (a, b, ha, hb) = RhoRealizationAudit.EqualEntropyDistinctStates();
        Assert.Equal(Math.Log(2.0), ha, 10);
        Assert.Equal(ha, hb, 10);
        Assert.Equal(a[0], a[1], 12);                       // A = (0.5, 0.5)
        Assert.NotEqual(b[0], b[1]);
        Assert.True(RhoRealizationAudit.EqualEntropySeparation() > 0.5);
    }

    [Fact]
    public void Y_G_039_PhaseIsSeparatedSectoriallyAndTheClockLawHolds()
    {
        // rho is a SITE count; the phase is a LINK object, so a phase rotation leaves rho untouched.
        var phase = RhoRealizationAudit.PhaseRotationTest();
        Assert.Equal(0.0, phase.RhoChange, 12);
        Assert.True(phase.HolonomyChange > 1e-9);

        // The clock law, to G_016b's own figure.
        Assert.Equal(Math.Log(20.0) / 3.0, RhoRealizationAudit.ClockShiftFromContrast(20.0), 12);
        Assert.Equal(0.9985774245, RhoRealizationAudit.ClockShiftFromContrast(20.0), 8);
        Assert.Equal(86277.089, RhoRealizationAudit.SecondsPerDay(20.0), 2);
        Assert.True(RhoRealizationAudit.ClockLawReproduced());
    }

    [Fact]
    public void Y_G_039_TheCandidatesAreDecidedByLoss()
    {
        var candidates = RhoRealizationAudit.Candidates();
        Assert.Equal(5, candidates.Length);

        // Zero loss: the full occupancy, the attractor's occupancy, the surviving set.
        var keep = RhoRealizationAudit.CandidatesThatDetermineRho();
        Assert.Equal(3, keep.Length);
        Assert.Contains("occupancy distributions", keep);
        Assert.Contains("attractor occupation", keep);
        Assert.Contains("survivor distributions", keep);

        // Lossy: anything averaged over a multiplet discards the free room.
        var gone = RhoRealizationAudit.CandidatesRefutedAsIdentity();
        Assert.Equal(2, gone.Length);
        Assert.Contains("state populations", gone);
        Assert.Contains("degeneracy occupation", gone);
        foreach (var c in candidates.Where(c => !c.ZeroLoss))
            Assert.Equal(51, c.DiscardedDimensions);

        Assert.Contains("ONE carrier", RhoRealizationAudit.TheSurvivorsCoincide());
    }

    [Fact]
    public void Y_G_039_TheVerdictIsBoundaryOnMeasurability()
    {
        var requirements = RhoRealizationAudit.Requirements();
        Assert.Equal(5, requirements.Length);
        Assert.Equal(4, RhoRealizationAudit.RequirementsMet());
        Assert.False(requirements.Single(r => r.Requirement.StartsWith("1.")).Met);   // measurable
        Assert.True(requirements.Single(r => r.Requirement.StartsWith("2.")).Met);    // not energy
        Assert.True(requirements.Single(r => r.Requirement.StartsWith("3.")).Met);    // not phase
        Assert.True(requirements.Single(r => r.Requirement.StartsWith("4.")).Met);    // not information
        Assert.True(requirements.Single(r => r.Requirement.StartsWith("5.")).Met);    // clock law

        Assert.Contains("UNMET IN THE LABORATORY", RhoRealizationAudit.Measurability().Status);
        Assert.Equal("BOUNDARY", RhoRealizationAudit.Verdict());
    }

    [Fact]
    public void Y_G_039_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_039 — Rho Realization Audit: which observable carries rho?");

        sb.AppendLine("QUESTION. What physical observable can carry rho while remaining compatible with G_016,");
        sb.AppendLine("G_016b, G_017, G_018 and G_035?");
        sb.AppendLine();
        sb.AppendLine("CANDIDATES  occupancy distributions, state populations, degeneracy occupation,");
        sb.AppendLine("            attractor occupation, survivor distributions");
        sb.AppendLine("REQUIREMENTS  1 measurable  2 not energy  3 not phase  4 not information");
        sb.AppendLine("              5 preserves the clock law");
        sb.AppendLine();
        sb.AppendLine("ID NOTE. Requested as G_036, which is already taken by the Temporal Core Test Audit delivered");
        sb.AppendLine("earlier in this session; the next free group-G number is used, because the ID space is");
        sb.AppendLine("permanent and is keyed by the index and the classification registry.");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The substrate is the 96-cell circulant C96(1..6) of G_016/G_018.");
        sb.AppendLine("  2. Every figure the constraint audits quote is RECOMPUTED here rather than cited.");
        sb.AppendLine("  3. Energy is the pairing E = <lambda, rho> (G_016); information is the Shannon entropy of the");
        sb.AppendLine("     same occupancy vector.");
        sb.AppendLine("  4. The clock law is dtau/dt = rho^(1/d) with d = 3 (G_028, G_016b).");
        sb.AppendLine();

        PrintHeader(RhoRealizationAudit.OutputSpectrum());
        PrintHeader(RhoRealizationAudit.OutputRequirements());
        PrintHeader(RhoRealizationAudit.OutputCandidates());

        PrintHeader("4. VERDICT");
        sb.AppendLine($"  {RhoRealizationAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("  " + RhoRealizationAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  THE FILTER, REDUCED");
        sb.AppendLine("   · not energy      MET — energy is one number on 95 dimensions, so it is blind to 94 of them");
        sb.AppendLine("                        (51 within-multiplet + 43 level mixing), computed exactly.");
        sb.AppendLine("   · not information MET — the entropy is one number on the same space, lossy by the same 94.");
        sb.AppendLine("   · not phase       MET — rho counts sites, the phase lives on links; sectorially orthogonal.");
        sb.AppendLine("   · clock law       MET — reproduced to G_016b's own figure, 86 277.089 s/day.");
        sb.AppendLine("   · measurable      UNMET — the natural realization (an optical intensity) is what G_017 excluded.");
        sb.AppendLine("   ⇒ FOUR OF FIVE, ONE CARRIER, AND THE BINDING CONSTRAINT NAMED. BOUNDARY.");

        Output.WriteLine(sb.ToString());
    }
}
