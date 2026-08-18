using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 87 — Role of higher-dimensional network structure. Determines whether unresolved SM structure can
/// live on faces/volumes rather than nodes/links. Classify: IRRELEVANT / COMPATIBLE / PREFERRED.
///
/// Tests: TQMQG870 (faces + volumes are derived), TQMQG871 (flux vs family/color/mass homes),
/// TQMQG872 (classification).
/// </summary>
public class TQMQG_Phase87_FacesAndVolumesTests : ResearchTestBase
{
    public TQMQG_Phase87_FacesAndVolumesTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG870: faces (2-cells), volumes (3-cells) ───────────────────────────────

    [Fact]
    public void TQMQG870_FacesAndVolumesAreDerived()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG870: are faces/volumes independent primitives?");

        bool face = FacesAndVolumes.FaceIsCycleOfLinks();
        bool volume = FacesAndVolumes.VolumeIsComposite();
        bool independent = FacesAndVolumes.HigherCellsAddIndependentDof();

        sb.AppendLine($"face (2-cell) is a closed cycle of links: {face}");
        sb.AppendLine($"volume (3-cell) is a composite of faces: {volume}");
        sb.AppendLine($"higher cells add INDEPENDENT degrees of freedom: {independent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: faces and volumes are DERIVED composites. Any structure on a face reduces to structure on");
        sb.AppendLine("its boundary links — higher cells add no independent degrees of freedom.");
        Output.WriteLine(sb.ToString());

        Assert.True(face, "face is a cycle of links");
        Assert.True(volume, "volume is a composite");
        Assert.False(independent, "no independent dof from higher cells");
    }

    // ── TQMQG871: flux variables, family/color/mass structure ──────────────────────

    [Fact]
    public void TQMQG871_FluxAndStructureHomes()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG871: where do flux, family, color, and mass live?");

        bool curvature = FacesAndVolumes.CurvatureLivesOnFaces();
        bool family = FacesAndVolumes.FamilyLivesOnNodesOrLinks();
        bool color = FacesAndVolumes.ColorLivesOnLinks();
        bool mass = FacesAndVolumes.MassLivesOnNodes();

        sb.AppendLine($"gauge curvature / magnetic flux lives on faces: {curvature}");
        sb.AppendLine($"family index lives on nodes/links (QG81): {family}");
        sb.AppendLine($"color connection lives on links (QG78): {color}");
        sb.AppendLine($"Higgs scalar ρ lives on nodes (QG84): {mass}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: curvature legitimately lives on faces (derived from link holonomies), but the unresolved");
        sb.AppendLine("structure — family index, color connection, Higgs — already has homes on nodes/links.");
        Output.WriteLine(sb.ToString());

        Assert.True(curvature, "curvature lives on faces");
        Assert.True(family, "family lives on nodes/links");
        Assert.True(color, "color lives on links");
        Assert.True(mass, "mass lives on nodes");
    }

    // ── TQMQG872: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG872_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG872: IRRELEVANT / COMPATIBLE / PREFERRED?");

        sb.AppendLine($"CLASSIFICATION: {FacesAndVolumes.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • IRRELEVANT: faces/volumes are DERIVED from links (no independent dof), so they cannot resolve");
        sb.AppendLine("    structure that already has homes on nodes/links.");
        sb.AppendLine("  • NOT PREFERRED: there is no reason to move family/color/mass onto higher cells — they already live on");
        sb.AppendLine("    nodes/links (QG78/QG81/QG84).");
        sb.AppendLine("  • COMPATIBLE (subordinate): faces legitimately host DERIVED curvature/flux, but not new SM structure.");
        sb.AppendLine();
        sb.AppendLine("So higher-dimensional cells are IRRELEVANT for the unresolved SM structure.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("IRRELEVANT", FacesAndVolumes.Classify());
        Assert.False(FacesAndVolumes.HigherCellsAddIndependentDof());
        Assert.True(FacesAndVolumes.ColorLivesOnLinks());
    }
}
