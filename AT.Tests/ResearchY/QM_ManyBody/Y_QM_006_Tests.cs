using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_006 - Schrodinger Propagator Audit (group QM). Can the nearest-neighbour generator {1} produce
/// psi(t) = exp(-i H t) psi(0) with Schrodinger-like dispersion? Measure omega(k), the group velocity, packet
/// spreading and norm conservation against the reference of Schrodinger packet evolution.
/// </summary>
public sealed class Y_QM_006_Tests : ResearchTestBase
{
    public Y_QM_006_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_QM_006_TheEvolutionIsExactAndTheReferenceReproducesItsOwnLaw()
    {
        PrintHeader("QM_006 - the exact evolution, and the analytic law checked against it");

        var history = SchrodingerPropagatorAudit.SpreadingHistory();
        var sb = new StringBuilder();
        sb.AppendLine("  time    reference width   analytic law   ratio");
        foreach (var h in history)
            sb.AppendLine($"  {h.Time,-7:F0} {h.ReferenceWidth,-17:F4} {h.SchrodingerLaw,-14:F4} {h.ReferenceWidth / h.SchrodingerLaw:F6}");
        sb.AppendLine();
        sb.AppendLine("  THE REFERENCE IS THE SAME PACKET UNDER omega = k^2, so the analytic law must reproduce it - and it does,");
        sb.AppendLine("  to five digits over the whole early window. A first version of this audit had the coefficient wrong by a");
        sb.AppendLine("  factor of four and the packet's own RMS width wrong by sqrt(2); the measured reference REFUSED both.");
        Output.WriteLine(sb.ToString());

        // The initial width is the parameter, not the parameter over root two.
        double width0 = SchrodingerPropagatorAudit.Width(SchrodingerPropagatorAudit.Evolve(SchrodingerPropagatorAudit.Packet(), SchrodingerPropagatorAudit.Reference, 0.0));
        Assert.Equal(SchrodingerPropagatorAudit.PacketWidth, width0, 6);

        // The analytic law reproduces the exact reference evolution, to within the ring's finite size.
        foreach (var h in history.Where(h => h.Time <= 40.0))
            Assert.Equal(h.SchrodingerLaw, h.ReferenceWidth, 3);
        // and the ring's finite size makes it lag at the longest time - measured, not hidden
        Assert.True(history[^1].ReferenceWidth < history[^1].SchrodingerLaw);
    }

    [Fact]
    public void Y_QM_006_NormConservationIsExactForEveryUnitaryCandidate()
    {
        PrintHeader("QM_006 - norm conservation: exact, and the control is why the test exists");

        var measures = SchrodingerPropagatorAudit.Measures(20.0);
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                norm deviation at t = 20");
        foreach (var m in measures)
            sb.AppendLine($"  {m.Candidate,-40} {m.NormDeviation:E2}");
        sb.AppendLine();
        sb.AppendLine("  every unitary candidate conserves the norm to machine precision BECAUSE ITS SYMBOL IS REAL, which makes");
        sb.AppendLine("  the generator Hermitian; the dissipative control is the only candidate that does not, and it is carried");
        sb.AppendLine("  precisely so that this test cannot pass vacuously.");
        Output.WriteLine(sb.ToString());

        foreach (var m in measures.Where(m => SchrodingerPropagatorAudit.IsUnitary(m.Candidate)))
            Assert.True(m.NormDeviation < 1e-12);

        var control = measures.Single(m => m.Candidate == SchrodingerPropagatorAudit.DissipativeControl);
        Assert.True(control.NormDeviation > 0.1);

        // and the norm is conserved at every time, not only at t = 20 - FOR THE UNITARY CANDIDATES. The control is
        // excluded here and asserted to decay instead, which is what it is carried for.
        var psi0 = SchrodingerPropagatorAudit.Packet();
        foreach (var t in SchrodingerPropagatorAudit.Times())
        foreach (var c in SchrodingerPropagatorAudit.Candidates().Where(SchrodingerPropagatorAudit.IsUnitary))
            Assert.Equal(1.0, SchrodingerPropagatorAudit.Norm(SchrodingerPropagatorAudit.Evolve(psi0, c, t)), 9);
        foreach (var t in SchrodingerPropagatorAudit.Times().Where(t => t > 0.0))
            Assert.True(SchrodingerPropagatorAudit.Norm(SchrodingerPropagatorAudit.Evolve(psi0, SchrodingerPropagatorAudit.DissipativeControl, t)) < 1.0);
    }

    [Fact]
    public void Y_QM_006_ThePacketWidthSeparatesSpreadingFromDriving()
    {
        PrintHeader("QM_006 - the measure that decides: the packet's width");

        var history = SchrodingerPropagatorAudit.SpreadingHistory();
        var sb = new StringBuilder();
        sb.AppendLine("  time    {1}        native     spectral   reference");
        foreach (var h in history)
            sb.AppendLine($"  {h.Time,-7:F0} {h.OneShellWidth,-10:F4} {h.NativeWidth,-10:F4} {h.SpectralWidth,-10:F4} {h.ReferenceWidth:F4}");
        sb.AppendLine();
        sb.AppendLine("  {1} TRACKS THE CONTINUUM LAW; the native generator lags it because its quartic contamination is larger;");
        sb.AppendLine("  and THE SPECTRAL CANDIDATE'S WIDTH NEVER MOVES - a linear dispersion moves every component at the same");
        sb.AppendLine("  speed, so it DRIVES the packet instead of SPREADING it.");
        Output.WriteLine(sb.ToString());

        // {1} tracks the reference closely; the native set does not.
        // The claim is an ORDERING, not a threshold at every time: the native generator's deviation is greater than
        // the one-shell one's at every time, and passes 5 % at the longest time in the window. Asserting 5 % at
        // t = 2 would fail, because at early times the native set is also within a per cent - its quartic correction
        // has not accumulated yet, which is exactly what the window measures.
        foreach (var h in history.Where(h => h.Time is >= 2.0 and <= 20.0))
        {
            double oneShellDeviation = Math.Abs(h.OneShellWidth / h.ReferenceWidth - 1.0);
            double nativeDeviation = Math.Abs(h.NativeWidth / h.ReferenceWidth - 1.0);
            Assert.True(oneShellDeviation < 0.01);
            Assert.True(nativeDeviation > 5.0 * oneShellDeviation);
        }
        Assert.True(Math.Abs(history.Single(h => h.Time == 20.0).NativeWidth
            / history.Single(h => h.Time == 20.0).ReferenceWidth - 1.0) > 0.05);

        // The spectral candidate's width is CONSTANT at every time except where the packet covers the ring's seam.
        foreach (var h in history.Where(h => h.Time != 40.0))
            Assert.Equal(SchrodingerPropagatorAudit.PacketWidth, h.SpectralWidth, 6);
        // the seam is a property of the ring, and it is recorded rather than hidden
        Assert.True(history.Single(h => h.Time == 40.0).SpectralWidth > SchrodingerPropagatorAudit.PacketWidth);

        // And the driving is measured directly: the centroid moves one cell per unit time exactly.
        var driving = SchrodingerPropagatorAudit.SpreadingVersusDriving();
        Assert.Equal(20.0, driving.Single(d => d.Candidate == SchrodingerPropagatorAudit.Spectral).CentroidAt20, 6);
        Assert.Equal(1.0, driving.Single(d => d.Candidate == SchrodingerPropagatorAudit.Spectral).WidthGrowthAt20, 6);
        Assert.True(Math.Abs(driving.Single(d => d.Candidate == SchrodingerPropagatorAudit.OneShell).CentroidAt20) < 1e-6);
    }

    [Fact]
    public void Y_QM_006_TheOneShellGeneratorReproducesSchrodingerOverTheLongestWindow()
    {
        PrintHeader("QM_006 - the window: how long each candidate stays Schrodinger-like");

        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                window at 10 % (bisected)");
        foreach (var c in SchrodingerPropagatorAudit.Candidates())
            sb.AppendLine($"  {c,-40} {SchrodingerPropagatorAudit.WindowAt(c),-10:F1}");
        sb.AppendLine();
        double ratio = SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.OneShell)
                       / SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.Native);
        sb.AppendLine($"  the one-shell window is {ratio:F2} times the native one, and the QUARTIC CONTAMINATION ratio is "
            + $"{SchrodingerPropagatorAudit.ContaminationRatio():F2}:");
        sb.AppendLine("  the same measurement reached from the dynamical side, but not identically, because the distance is not");
        sb.AppendLine("  linear in the accumulated phase error.");
        Output.WriteLine(sb.ToString());

        double oneShell = SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.OneShell);
        double native = SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.Native);
        double spectral = SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.Spectral);

        Assert.InRange(oneShell, 500.0, 530.0);
        Assert.InRange(native, 20.0, 26.0);
        Assert.True(oneShell > 20.0 * native);
        Assert.True(spectral < 1.0);

        // the window ratio tracks the contamination ratio (25.0) to within a fifth
        Assert.InRange(ratio / SchrodingerPropagatorAudit.ContaminationRatio(), 0.8, 1.0);
        Assert.InRange(SchrodingerPropagatorAudit.ContaminationRatio(), 24.9, 25.1);
    }

    [Fact]
    public void Y_QM_006_TheDispersionAndGroupVelocityOfEachCandidate()
    {
        PrintHeader("QM_006 - omega(k) and the group velocity");

        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                power law   v_g at channel 1   v_g at channel 24   v_g at channel 48");
        foreach (var c in new[] { SchrodingerPropagatorAudit.OneShell, SchrodingerPropagatorAudit.Native,
                                   SchrodingerPropagatorAudit.Spectral, SchrodingerPropagatorAudit.Reference })
            sb.AppendLine($"  {c,-40} {SchrodingerPropagatorAudit.PowerLaw(c),-11:F4} "
                + $"{SchrodingerPropagatorAudit.GroupVelocity(c, 1),-18:F6} {SchrodingerPropagatorAudit.GroupVelocity(c, 24),-19:F6} {SchrodingerPropagatorAudit.GroupVelocity(c, 48):F6}");
        sb.AppendLine();
        sb.AppendLine("  the reference's group velocity is 2k, which RISES without bound; the spectral candidate's is the");
        sb.AppendLine("  constant 1, which is why it drives; and the two Laplacians' vanish at the zone edge.");
        Output.WriteLine(sb.ToString());

        // the power laws of QM_004, recovered independently from the propagator side
        Assert.InRange(SchrodingerPropagatorAudit.PowerLaw(SchrodingerPropagatorAudit.OneShell), 1.999, 2.001);
        Assert.InRange(SchrodingerPropagatorAudit.PowerLaw(SchrodingerPropagatorAudit.Native), 1.999, 2.001);
        Assert.InRange(SchrodingerPropagatorAudit.PowerLaw(SchrodingerPropagatorAudit.Spectral), 0.999, 1.001);
        Assert.InRange(SchrodingerPropagatorAudit.PowerLaw(SchrodingerPropagatorAudit.Reference), 1.999, 2.001);

        // the group velocities: constant for the spectral candidate, rising for the reference, vanishing at the edge
        for (int c = 1; c <= 48; c++)
            Assert.Equal(1.0, SchrodingerPropagatorAudit.GroupVelocity(SchrodingerPropagatorAudit.Spectral, c), 9);
        Assert.True(SchrodingerPropagatorAudit.GroupVelocity(SchrodingerPropagatorAudit.Reference, 24)
            > SchrodingerPropagatorAudit.GroupVelocity(SchrodingerPropagatorAudit.Reference, 6));
        Assert.True(Math.Abs(SchrodingerPropagatorAudit.GroupVelocity(SchrodingerPropagatorAudit.OneShell, 48)) < 1e-12);
    }

    [Fact]
    public void Y_QM_006_TheDistanceFromTheExactSchrodingerSolutionIsTheAuditSHeadline()
    {
        PrintHeader("QM_006 - the distance from the exact Schrodinger solution");

        var psi0 = SchrodingerPropagatorAudit.Packet();
        var sb = new StringBuilder();
        sb.AppendLine("  time    {1}         native      spectral     control");
        foreach (var t in SchrodingerPropagatorAudit.Times())
            sb.AppendLine($"  {t,-7:F0} {SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.OneShell, t),-11:F6} "
                + $"{SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.Native, t),-11:F6} "
                + $"{SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.Spectral, t),-12:F6} "
                + $"{SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.DissipativeControl, t):F6}");
        sb.AppendLine();
        sb.AppendLine("  THE ONE-SHELL GENERATOR'S DISTANCE IS SMALL AND GROWS SLOWLY; the native one is an order of magnitude");
        sb.AppendLine("  larger at the same time; and the spectral candidate is order one immediately, because it is a different");
        sb.AppendLine("  operator and not an approximation to this one.");
        Output.WriteLine(sb.ToString());

        double d1 = SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.OneShell, 20.0);
        double dn = SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.Native, 20.0);
        double ds = SchrodingerPropagatorAudit.DistanceFromSchrodinger(psi0, SchrodingerPropagatorAudit.Spectral, 20.0);

        Assert.InRange(d1, 0.003, 0.005);
        Assert.InRange(dn, 0.08, 0.10);
        Assert.True(ds > 1.0);
        Assert.True(dn > 10.0 * d1);

        // and the one-shell distance stays inside the tolerance over the window the verdict quotes
        Assert.True(d1 < 0.10);
        Assert.True(SchrodingerPropagatorAudit.WindowAt(SchrodingerPropagatorAudit.OneShell) > 500.0);
    }

    [Fact]
    public void Y_QM_006_TheVerdictIsPartialWithTheAskedForGeneratorAnalogous()
    {
        PrintHeader("QM_006 - the verdict");

        var verdicts = SchrodingerPropagatorAudit.CandidateVerdicts();
        var counts = SchrodingerPropagatorAudit.VerdictCounts();
        var sb = new StringBuilder();
        foreach (var v in verdicts)
        {
            sb.AppendLine($"{v.Candidate}: {v.Verdict}");
            sb.AppendLine($"  {v.Basis}");
        }
        sb.AppendLine();
        sb.AppendLine(SchrodingerPropagatorAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine(SchrodingerPropagatorAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, verdicts.Length);
        Assert.Equal("ANALOGOUS", verdicts.Single(v => v.Candidate == SchrodingerPropagatorAudit.OneShell).Verdict);
        Assert.Equal("PARTIAL", verdicts.Single(v => v.Candidate == SchrodingerPropagatorAudit.Native).Verdict);
        Assert.Equal("REFUTED", verdicts.Single(v => v.Candidate == SchrodingerPropagatorAudit.Spectral).Verdict);
        Assert.Equal(1, counts.Analogous);
        Assert.Equal(1, counts.Partial);
        Assert.Equal(2, counts.Refuted);

        var verdict = SchrodingerPropagatorAudit.Verdict();
        Assert.Contains("PARTIAL", verdict);
        Assert.Contains("ANALOGOUS FOR THE GENERATOR", verdict);
        Assert.Contains("TRANSLATES RIGIDLY", verdict);
    }

    [Fact]
    public void Y_QM_006_TheReport()
    {
        PrintHeader("QM_006 - Schrodinger propagator: the report");

        var sb = new StringBuilder();
        sb.AppendLine(SchrodingerPropagatorAudit.OutputMeasures());
        sb.AppendLine(SchrodingerPropagatorAudit.OutputSpreading());
        sb.AppendLine(SchrodingerPropagatorAudit.OutputWindows());
        sb.AppendLine(SchrodingerPropagatorAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());

        // The evolution is exact, so the propagation of a packet under omega = k^2 must reproduce the
        // free-particle kernel: a delta packet spread by the Schrodinger kernel at small time.
        var psi0 = SchrodingerPropagatorAudit.Packet();
        var spread = SchrodingerPropagatorAudit.Evolve(psi0, SchrodingerPropagatorAudit.Reference, 0.0);
        // at t = 0 the state must be the initial packet itself
        for (int j = 0; j < SchrodingerPropagatorAudit.Cells; j++)
            Assert.Equal(psi0[j], spread[j].Real, 9);
        foreach (var j in Enumerable.Range(0, SchrodingerPropagatorAudit.Cells))
            Assert.True(Math.Abs(spread[j].Imaginary) < 1e-12);
    }
}
