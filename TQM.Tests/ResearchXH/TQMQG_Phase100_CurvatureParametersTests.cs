using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 100 — Parameter origin from network curvature. Determines whether local curvature/deficit patterns
/// can determine physical parameters. Classify: NO RELATION / PARTIAL RELATION / CURVATURE ORIGIN.
///
/// Tests: TQMQG1000 (deficit + defect angles), TQMQG1001 (invariants + analogs + derived), TQMQG1002 (classification).
/// </summary>
public class TQMQG_Phase100_CurvatureParametersTests : ResearchTestBase
{
    public TQMQG_Phase100_CurvatureParametersTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG1000: deficit distributions, triangle defect angles ───────────────────

    [Fact]
    public void TQMQG1000_DeficitAndDefect()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1000: do deficit/defect-angle patterns exist?");

        bool deficit = CurvatureParameters.DeficitDistributionsExist();
        bool defect = CurvatureParameters.TriangleDefectAnglesExist();

        sb.AppendLine($"deficit-angle distributions exist: {deficit}");
        sb.AppendLine($"triangle defect angles are curvature invariants: {defect}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: discrete curvature (deficit angle = 2π − sum of face angles) is real and derived — the same");
        sb.AppendLine("object the G4 program used to extract curvature from spectra.");
        Output.WriteLine(sb.ToString());

        Assert.True(deficit, "deficit distributions exist");
        Assert.True(defect, "defect angles exist");
    }

    // ── TQMQG1001: curvature invariants, mass/mixing analogs, derived ──────────────

    [Fact]
    public void TQMQG1001_InvariantsAndAnalogs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1001: curvature invariants and parameter analogs");

        bool invariants = CurvatureParameters.LocalCurvatureInvariantsExist();
        bool mass = CurvatureParameters.MassHierarchyCurvatureAnalog();
        bool mixing = CurvatureParameters.MixingAngleCurvatureAnalog();
        bool derived = CurvatureParameters.CurvatureIsDerivedFromMetric();
        bool determines = CurvatureParameters.CurvatureDeterminesValues();

        sb.AppendLine($"local curvature invariants (Ricci analogue) exist: {invariants}");
        sb.AppendLine($"mass-hierarchy curvature analog (suggestive): {mass}");
        sb.AppendLine($"mixing-angle deficit-angle analog (suggestive): {mixing}");
        sb.AppendLine($"curvature DERIVED from the metric (ρ, ψ): {derived}");
        sb.AppendLine($"curvature DETERMINES specific parameter values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: curvature is a real geometric observable but DERIVED from the metric (no independent dof), and");
        sb.AppendLine("SM parameters are INTERNAL — the deficit-angle analogies are suggestive, not determinative.");
        Output.WriteLine(sb.ToString());

        Assert.True(invariants, "curvature invariants exist");
        Assert.True(mass, "mass analog exists");
        Assert.True(mixing, "mixing analog exists");
        Assert.True(derived, "curvature is derived");
        Assert.False(determines, "curvature does not determine values");
    }

    // ── TQMQG1002: classification ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG1002_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1002: NO RELATION / PARTIAL RELATION / CURVATURE ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {CurvatureParameters.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: deficit angles and curvature invariants are real, derived network observables.");
        sb.AppendLine("  • NOT CURVATURE ORIGIN: curvature is derived from the metric (no independent dof), and SM parameters are");
        sb.AppendLine("    INTERNAL — no native mapping identifies a specific deficit with a specific parameter.");
        sb.AppendLine("  • PARTIAL RELATION: real derived curvature + suggestive analogy, without value determination.");
        sb.AppendLine();
        sb.AppendLine("So network curvature gives a PARTIAL RELATION to parameters (derived observable, not curvature origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", CurvatureParameters.Classify());
        Assert.True(CurvatureParameters.DeficitDistributionsExist());
        Assert.False(CurvatureParameters.CurvatureDeterminesValues());
    }
}
