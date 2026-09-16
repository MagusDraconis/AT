using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_001 - ManyBody Correspondence Audit (new group QM). Does the AT decomposition
/// rho = mean + amplitude + phase have an analogue in many-body quantum states? Compare wavefunction amplitudes,
/// wavefunction phases, the density matrix, the reduced density matrix and occupation numbers, measuring DIMENSION,
/// KERNEL and OBSERVABLE RANK. Output ANALOGOUS / PARTIAL / REFUTED.
/// </summary>
public sealed class Y_QM_001_Tests : ResearchTestBase
{
    public Y_QM_001_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_QM_001_TheDecompositionIsACompleteOrthogonalPartition()
    {
        PrintHeader("QM_001 - the AT side: 1 + 42 + 53 = 96");

        int mean = ManyBodyCorrespondenceAudit.MeanDimension();
        int amplitude = ManyBodyCorrespondenceAudit.AmplitudeDimension();
        int phase = ManyBodyCorrespondenceAudit.PhaseDimension();

        var sb = new StringBuilder();
        sb.AppendLine($"  mean      {mean,3}   (the constant mode)");
        sb.AppendLine($"  amplitude {amplitude,3}   (G_052's amplitude sector)");
        sb.AppendLine($"  phase     {phase,3}   (G_052's phase sector)");
        sb.AppendLine($"  total     {mean + amplitude + phase,3}   against {ManyBodyCorrespondenceAudit.Cells} cells");
        sb.AppendLine();
        sb.AppendLine($"  the sectors are orthogonal, and the amplitude side is the contraction observables' row space:");
        sb.AppendLine($"  observable rank {ManyBodyCorrespondenceAudit.ObservableRank()} = mean + amplitude; kernel {ManyBodyCorrespondenceAudit.Kernel()} = the phase sector");
        sb.AppendLine($"  the canonical state's phase content: {ManyBodyCorrespondenceAudit.CanonicalPhaseContent():E3} (phase-free)");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, mean);
        Assert.Equal(42, amplitude);
        Assert.Equal(53, phase);
        Assert.True(ManyBodyCorrespondenceAudit.TheDecompositionIsACompletePartition());
        Assert.Equal(ManyBodyCorrespondenceAudit.Cells, mean + amplitude + phase);

        // The observable rank agrees with the decomposition audit's own count, so the two audits share the measure.
        Assert.Equal(AmplitudePhaseAudit.AmplitudePlusMean(), ManyBodyCorrespondenceAudit.ObservableRank());
        Assert.Equal(ManyBodyCorrespondenceAudit.PhaseDimension(), ManyBodyCorrespondenceAudit.Kernel());

        // The canonical state is phase-free: rho is a real modulus.
        Assert.True(ManyBodyCorrespondenceAudit.CanonicalPhaseContent() < 1e-12);
    }

    [Fact]
    public void Y_QM_001_RhoIsAModulusSquaredOfARealWavefunction()
    {
        PrintHeader("QM_001 - the deciding measurement: rho is a modulus, not an operator");

        var rho = RhoAccessibilityAudit.BaseState();
        double error = ManyBodyCorrespondenceAudit.ModulusReconstructionError();
        var census = ManyBodyCorrespondenceAudit.OccupancyCensus();

        var sb = new StringBuilder();
        sb.AppendLine($"  rho: {rho.Length} cells, total {ManyBodyCorrespondenceAudit.OccupancyTotal():F0}, mean {ManyBodyCorrespondenceAudit.OccupancyTotal() / rho.Length:F3}");
        sb.AppendLine($"  census: {census.NonNegative} non-negative, {census.StrictlyPositive} strictly positive, {census.Zero} zero");
        sb.AppendLine($"  reconstruction rho = |Psi|^2 with Psi = sqrt(rho): worst error {error:E3}");
        sb.AppendLine($"  the state's rank as an operator |rho><rho|: {ManyBodyCorrespondenceAudit.StateRank()}");
        sb.AppendLine();
        sb.AppendLine("  SO RHO IS THE MODULUS SQUARED OF A REAL WAVEFUNCTION ON THE SAME 96 SITES, AND ITS OPERATOR FORM IS");
        sb.AppendLine("  RANK ONE - THE RANK OF |Psi><Psi|. That is the |Psi|^2 answer to the goal, measured rather than argued.");
        Output.WriteLine(sb.ToString());

        Assert.True(ManyBodyCorrespondenceAudit.RhoIsAModulusSquared());
        Assert.True(error < 1e-12);
        Assert.Equal(1, ManyBodyCorrespondenceAudit.StateRank());

        // The total is the cell count, not 1: a density normalisation rather than a trace.
        Assert.Equal(96.0, ManyBodyCorrespondenceAudit.OccupancyTotal(), 9);
        Assert.Equal(96, census.NonNegative);
        Assert.Equal(0, census.Zero);
    }

    [Fact]
    public void Y_QM_001_TheDimensionMatchedManyBodyObjectNeedsNinetySixTimesTheParameters()
    {
        PrintHeader("QM_001 - dimension: the modulus against the operator");

        var table = ManyBodyCorrespondenceAudit.DimensionTable();
        var sb = new StringBuilder();
        sb.AppendLine("  object                       state dimension   matrix parameters   ratio");
        foreach (var d in table)
            sb.AppendLine($"  {d.Object,-28} {d.StateDimension,-17} {d.MatrixParameters,-19} {d.Ratio}");
        sb.AppendLine();
        sb.AppendLine($"  ONE PARTICLE ON {ManyBodyCorrespondenceAudit.Cells} MODES HAS FOCK DIMENSION {table[1].StateDimension} = AT's STATE DIMENSION,");
        sb.AppendLine($"  WHILE ITS DENSITY MATRIX NEEDS {table[1].MatrixParameters} REAL PARAMETERS: AT SUPPLIES {ManyBodyCorrespondenceAudit.Cells}, A FACTOR OF {table[1].Ratio} FEWER.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, table.Length);
        Assert.Equal(96L, table[1].StateDimension);
        Assert.Equal(9216L, table[1].MatrixParameters);
        Assert.Equal(96L, table[1].Ratio);
        // The ratio is always the dimension itself, which is the structural statement.
        foreach (var d in table.Skip(1)) Assert.Equal(d.StateDimension, d.Ratio);
    }

    [Fact]
    public void Y_QM_001_TheThreeRanksSeparateTheStateFromTheObservableAlgebra()
    {
        PrintHeader("QM_001 - the three ranks, which decide the goal");

        var ranks = ManyBodyCorrespondenceAudit.TheThreeRanks();
        var sb = new StringBuilder();
        foreach (var r in ranks)
            sb.AppendLine($"  {r.Rank,-16} {r.Value,4}   {r.WhatItIs}").AppendLine($"                     quantum counterpart: {r.QuantumCounterpart}");
        sb.AppendLine();
        sb.AppendLine($"  reading AT's kernel ({ManyBodyCorrespondenceAudit.Kernel()}) as a density-matrix kernel demands rank "
            + $"{ManyBodyCorrespondenceAudit.RankImpliedByTheKernel()}, which is {ManyBodyCorrespondenceAudit.RankImpliedByTheKernel()}x the rank of the state itself.");
        sb.AppendLine("  A DENSITY MATRIX CONFLATES THE TWO RANKS; AT SEPARATES THEM - which is why the kernel relation");
        sb.AppendLine("  hidden <-> zero occupancy is the natural-orbital relation and not a rank deficiency.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, ranks.Length);
        Assert.Equal(1, ranks[0].Value);     // state rank: a modulus
        Assert.Equal(43, ranks[1].Value);    // observable rank: 1 + 42
        Assert.Equal(53, ranks[2].Value);    // kernel: 96 - 43
        Assert.Equal(43L, ManyBodyCorrespondenceAudit.RankImpliedByTheKernel());
        Assert.Equal(ManyBodyCorrespondenceAudit.Cells,
            ManyBodyCorrespondenceAudit.ObservableRank() + ManyBodyCorrespondenceAudit.Kernel());
    }

    [Fact]
    public void Y_QM_001_TheOneBodyReducedDensityMatrixCarriesTheParticleNumber()
    {
        PrintHeader("QM_001 - the one-body reduced density matrix, computed exactly");

        var sb = new StringBuilder();
        foreach (var (modes, particles) in new[] { (3, 2), (4, 2), (6, 3), (6, 4) })
        {
            var state = ManyBodyCorrespondenceAudit.CanonicalState(modes, particles);
            var occ = ManyBodyCorrespondenceAudit.OccupationNumbers(state, modes);
            double trace = occ.Sum();
            sb.AppendLine($"  {particles} bosons on {modes} modes (Fock dim {ManyBodyCorrespondenceAudit.BosonFockDimension(modes, particles)}): "
                + $"occupations {string.Join(", ", occ.Select(x => x.ToString("F6")))}");
            sb.AppendLine($"    trace {trace:F12} = the particle number; rank {ManyBodyCorrespondenceAudit.OccupationRank(state, modes)}");

            // The conservation law: the 1-RDM's trace IS the particle number. A test that fails if it drifts.
            Assert.Equal(particles, trace, 9);
            Assert.True(occ.All(n => n >= -1e-12));
            // The 1-RDM is a genuine operator here, with more than one non-zero occupation.
            Assert.True(ManyBodyCorrespondenceAudit.OccupationRank(state, modes) >= 2);
        }
        sb.AppendLine();
        sb.AppendLine("  A DENSITY MATRIX'S OCCUPATIONS ARE REAL, NON-NEGATIVE AND SUM TO THE PARTICLE NUMBER -");
        sb.AppendLine($"  WHILE AT's OCCUPANCIES SUM TO {ManyBodyCorrespondenceAudit.OccupancyTotal():F0}, ITS STATE DIMENSION.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_QM_001_TheReducedDensityMatrixIsEntangledOnlyWhenThePairStraddlesTheCut()
    {
        PrintHeader("QM_001 - the reduced density matrix and its entanglement");

        var sb = new StringBuilder();
        // A superposition confined to one side of the cut is a PRODUCT state: pure rDM, zero entropy.
        var inside = ManyBodyCorrespondenceAudit.ReducedDensityMatrix(
            ManyBodyCorrespondenceAudit.CanonicalState(6, 3), 6, 3);
        // A superposition straddling the cut is ENTANGLED: Schmidt rank 2, entropy log 2.
        var across = ManyBodyCorrespondenceAudit.ReducedDensityMatrix(
            ManyBodyCorrespondenceAudit.SplitPair(6, 3), 6, 3);
        // A first version of this audit used the Fock enumeration's first two configurations, which both left the
        // kept block empty and returned rank 1 and zero entropy for EVERY system - a probe defect, recorded here.
        sb.AppendLine($"  superposition inside the kept block : dim {inside.Dimension}, rank {inside.Rank}, entropy {inside.Entropy:F6}, trace {inside.Trace:F6}");
        sb.AppendLine($"  superposition straddling the cut    : dim {across.Dimension}, rank {across.Rank}, entropy {across.Entropy:F6}, trace {across.Trace:F6}");
        sb.AppendLine($"  log 2 = {Math.Log(2.0):F6}");
        sb.AppendLine();
        sb.AppendLine($"  THE PARTIAL TRACE IS A GENUINE OPERATION HERE - AND AT HAS NOTHING FOR IT TO ACT ON:");
        sb.AppendLine("  the 96 cells (or the 96-dim state space) carry no tensor factor. The nearest AT object is the cubic");
        sb.AppendLine("  substrate D96^3, whose whole is 96^3 = 884736 and whose one-factor reduced object is 96^2 = 9216.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(20, inside.Dimension);                    // every N_A over 3 of the 6 modes
        Assert.Equal(1, inside.Rank);                          // a product state
        Assert.True(inside.Entropy < 1e-12);
        Assert.Equal(1.0, inside.Trace, 9);

        Assert.Equal(2, across.Rank);                          // Schmidt rank 2
        Assert.Equal(Math.Log(2.0), across.Entropy, 9);
        Assert.Equal(1.0, across.Trace, 9);
    }

    [Fact]
    public void Y_QM_001_TheOccupationsOfAFermionicStateObeyThePauliBound()
    {
        PrintHeader("QM_001 - fermions: the Pauli bound, which AT's multiplicities do not obey");

        var sb = new StringBuilder();
        foreach (var (modes, particles) in new[] { (6, 3), (8, 4) })
        {
            var occ = ManyBodyCorrespondenceAudit.FermionOccupationNumbers(modes, particles);
            sb.AppendLine($"  {particles} fermions on {modes} modes (Fock dim {ManyBodyCorrespondenceAudit.FermionFockDimension(modes, particles)}):");
            sb.AppendLine($"    occupations {string.Join(", ", occ.Select(x => x.ToString("F6")))}   trace {occ.Sum():F12}");
            Assert.Equal(particles, occ.Sum(), 9);
            Assert.True(occ.All(n => n >= -1e-12 && n <= 1.0 + 1e-12));
        }
        var mult = ManyBodyCorrespondenceAudit.Multiplicities();
        sb.AppendLine();
        sb.AppendLine($"  AT's own numbers: {mult.Length} distinct spectral levels, multiplicities summing to {ManyBodyCorrespondenceAudit.MultiplicitySum()}");
        sb.AppendLine($"    multiplicity range {mult.Min(m => m.Multiplicity)}..{mult.Max(m => m.Multiplicity)} - DECOMPOSED VALUES, many above 1");
        sb.AppendLine("  SO AT's 'OCCUPATION NUMBERS' ARE OPERATOR DEGENERACIES, NOT OCCUPATIONS: they are integers summing to the STATE");
        sb.AppendLine("  DIMENSION rather than reals summing to a particle number, and they are not bounded by 1.");
        Output.WriteLine(sb.ToString());

        Assert.True(ManyBodyCorrespondenceAudit.EveryFermionicOccupationLiesInThePauliInterval());
        Assert.Equal(45, mult.Length);
        Assert.Equal(96, ManyBodyCorrespondenceAudit.MultiplicitySum());
        Assert.True(mult.Max(m => m.Multiplicity) > 1);         // AT's numbers exceed the Pauli bound
        Assert.Equal(ManyBodyCorrespondenceAudit.Cells, ManyBodyCorrespondenceAudit.MultiplicitySum());
    }

    [Fact]
    public void Y_QM_001_TheVerdictIsPartialWithTheGoalAnswered()
    {
        PrintHeader("QM_001 - the verdict");

        var verdicts = ManyBodyCorrespondenceAudit.StructureVerdicts();
        var counts = ManyBodyCorrespondenceAudit.VerdictCounts();
        var verdict = ManyBodyCorrespondenceAudit.Verdict();

        var sb = new StringBuilder();
        foreach (var v in verdicts) sb.AppendLine($"  {v.Structure,-28} {v.Verdict}");
        sb.AppendLine($"  -> {counts.Analogous} analogous, {counts.Partial} partial, {counts.Refuted} refuted");
        sb.AppendLine();
        sb.AppendLine(verdict);
        sb.AppendLine();
        sb.AppendLine(ManyBodyCorrespondenceAudit.TheComparisonInOneLine());
        sb.AppendLine();
        sb.AppendLine(ManyBodyCorrespondenceAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, verdicts.Length);
        Assert.Equal(2, counts.Analogous);
        Assert.Equal(0, counts.Partial);
        Assert.Equal(3, counts.Refuted);
        Assert.Contains("PARTIAL", verdict);
        Assert.Contains("|Psi|^2", verdict);
    }
}
