using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-D Phase 0 — does Lc = ρ⁻¹ L ρ⁻¹ generate curvature dynamics?
/// Evolves the conformal density ρ(x,t) = 1 + A(t)·x² (analytic R(0,t) = −4·A(t)) and checks
/// whether the reconstructed curvature (CurvatureReconstruction.Score of Lc) follows the
/// operator's own evolution: sign tracking, dR/dt consistency, and spectral-observable
/// continuity.
///
/// Tests: G4-D00 (sign tracking over a full oscillation), G4-D01 (dR/dt consistency),
///        G4-D02 (spectral-observable continuity + reconstructed-vs-expected tracking).
/// </summary>
public class G4D_Phase0_CurvatureDynamicsTests : ResearchTestBase
{
    public G4D_Phase0_CurvatureDynamicsTests(ITestOutputHelper o) : base(o) { }

    private const double Amplitude = 0.8;   // |R(0)| = 3.2
    private const int Steps = 16;           // 17 frames per trajectory

    // ── G4-D00: reconstructed sign follows the operator through a full oscillation ──────

    [Fact]
    public void G4_D00_SignTracksOperatorEvolutionOverFullCycle()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D00: reconstructed curvature sign tracks ρ(t) through a full oscillation");

        var frames = CurvatureDynamics.Evolve(CurvatureDynamics.Oscillation(Steps, Amplitude));

        sb.AppendLine($"Trajectory: A(t) = {Amplitude:F1}·cos(2π t/{Steps}) ⇒ R(0,t) = −4·A(t).");
        sb.AppendLine("Crosses flat twice (t=4, t=12) ⇒ two sign flips. Sign of score must equal sign of R.");
        sb.AppendLine();
        sb.AppendLine($"{"t",3} {"A",7} {"R(0)",7} {"score",10} {"sign(score)",10} {"sign(R)",8}  match");

        bool allMatch = true;
        for (int t = 0; t < frames.Length; t++)
        {
            var f = frames[t];
            int ss = Math.Sign(f.Score), sr = Math.Sign(f.ExpectedR);
            bool match = ss == sr;
            if (!match) allMatch = false;
            sb.AppendLine($"{t,3} {f.A,7:F3} {f.ExpectedR,7:F2} {f.Score,10:F3} {ss,10} {sr,8}  {match}");
        }

        sb.AppendLine();
        sb.AppendLine($"Sign of reconstructed curvature matches sign of R(0) at {frames.Length}/{frames.Length} frames: {allMatch}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: changes in ρ produce predictable changes in reconstructed curvature —");
        sb.AppendLine("the sign of R̂ follows the operator's evolution through two sign flips.");
        Output.WriteLine(sb.ToString());

        foreach (var f in frames)
            Assert.Equal(Math.Sign(f.ExpectedR), Math.Sign(f.Score));
    }

    // ── G4-D01: dR̂/dt follows dR/dt (reconstruction evolves with the operator) ──────────

    [Fact]
    public void G4_D01_CurvatureRateIsConsistentWithOperatorEvolution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D01: dR̂/dt is sign-consistent with dR/dt along a monotonic sweep");

        var frames = CurvatureDynamics.Evolve(CurvatureDynamics.LinearSweep(Steps, -Amplitude, +Amplitude));

        sb.AppendLine($"Trajectory: A(t) linear −{Amplitude:F1} → +{Amplitude:F1} ⇒ R(0,t) linear +3.2 → −3.2.");
        sb.AppendLine("dR/dt = −4·ΔA &lt; 0 ⇒ dR̂/dt must also be &lt; 0 at every step.");
        sb.AppendLine();
        sb.AppendLine($"{"t",3} {"A",7} {"R",7} {"score",10} {"ΔR",7} {"Δscore",9}  sign-match");

        bool consistent = true;
        for (int t = 0; t < frames.Length - 1; t++)
        {
            double dR = frames[t + 1].ExpectedR - frames[t].ExpectedR;
            double dS = frames[t + 1].Score - frames[t].Score;
            bool match = Math.Sign(dR) == Math.Sign(dS) && Math.Sign(dS) != 0;
            if (!match) consistent = false;
            sb.AppendLine($"{t,3} {frames[t].A,7:F3} {frames[t].ExpectedR,7:F2} {frames[t].Score,10:F3} " +
                          $"{dR,7:F2} {dS,9:F3}  {match}");
        }

        sb.AppendLine();
        sb.AppendLine($"dR̂/dt follows dR/dt in sign at {Steps}/{Steps} steps: {consistent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the reconstructed curvature evolves monotonically and in the same");
        sb.AppendLine("direction as the analytic curvature — the reconstruction is a continuous,");
        sb.AppendLine("predictable function of the operator, not a static classification.");
        Output.WriteLine(sb.ToString());

        for (int t = 0; t < frames.Length - 1; t++)
        {
            double dR = frames[t + 1].ExpectedR - frames[t].ExpectedR;
            double dS = frames[t + 1].Score - frames[t].Score;
            Assert.True(Math.Sign(dR) == Math.Sign(dS) && Math.Sign(dS) != 0,
                $"step {t}: ΔR={dR:F3}, Δscore={dS:F3} disagree in sign");
        }
    }

    // ── G4-D02: spectral observables evolve continuously + track expected R ─────────────

    [Fact]
    public void G4_D02_SpectralObservablesEvolveContinuously()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D02: spectral observables evolve continuously and track expected R");

        var frames = CurvatureDynamics.Evolve(CurvatureDynamics.LinearSweep(Steps, -Amplitude, +Amplitude));

        var obs = new (string Name, double[] Vals)[]
        {
            ("gap",        frames.Select(f => f.Gap).ToArray()),
            ("heatTrace",  frames.Select(f => f.HeatTrace).ToArray()),
            ("ζ(2)",       frames.Select(f => f.Zeta).ToArray()),
            ("entropy",    frames.Select(f => f.Entropy).ToArray()),
        };

        sb.AppendLine("ASSUMPTIONS: each Lc spectral observable is monotonic in curvature (Phase 1 SC3),");
        sb.AppendLine("so along a monotonic A-sweep every observable must be monotonic (no reversal).");
        sb.AppendLine();

        foreach (var (name, vals) in obs)
        {
            bool mono = IsMonotonic(vals);
            sb.AppendLine($"{name,-10}  monotonic={mono}  range=[{vals.Min():F3}, {vals.Max():F3}]");
            Assert.True(mono, $"{name}: observable reversed direction along the sweep");
        }

        var scores = frames.Select(f => f.Score).ToArray();
        var rs = frames.Select(f => f.ExpectedR).ToArray();
        bool scoreMono = IsMonotonic(scores);
        double r = CurvatureDynamics.Pearson(scores, rs);

        sb.AppendLine();
        sb.AppendLine($"score       monotonic={scoreMono}  Pearson(score, R) = {r:F4}");
        sb.AppendLine();
        sb.AppendLine($"Tracking strength: reconstructed-vs-expected correlation |r| = {Math.Abs(r):F4}.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: every spectral observable evolves continuously (monotonically) with the");
        sb.AppendLine("operator, and the reconstructed curvature tracks the analytic R(t) strongly —");
        sb.AppendLine("curvature evolution follows operator evolution consistently.");
        Output.WriteLine(sb.ToString());

        Assert.True(scoreMono, "score reversed direction along the sweep");
        Assert.True(Math.Abs(r) > 0.9, $"tracking correlation |r| = {Math.Abs(r):F4} not > 0.9");
    }

    /// <summary>True if the series never reverses direction (all consecutive differences share a sign).</summary>
    private static bool IsMonotonic(double[] v)
    {
        int sign = 0;
        for (int i = 1; i < v.Length; i++)
        {
            double d = v[i] - v[i - 1];
            if (Math.Abs(d) < 1e-12) continue; // tolerate exact-flat equality
            int s = Math.Sign(d);
            if (sign == 0) sign = s;
            else if (s != sign) return false;
        }
        return true;
    }
}
