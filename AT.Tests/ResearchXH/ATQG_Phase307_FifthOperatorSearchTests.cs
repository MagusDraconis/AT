using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 307 — Fifth Operator Search. Attempt to discover a new operator beyond {CROWDING,
/// COMPRESSION, BEAT, LOCKING} by searching all unexplored domains. No observables, no target values,
/// D96 only, deterministic.
/// </summary>
public class ATQG_Phase307_FifthOperatorSearchTests : ResearchTestBase
{
    public ATQG_Phase307_FifthOperatorSearchTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3070_CandidatesReducible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3070: the candidate fifth operators reduce to the existing basis");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - phase δ_CP = occ_top/Σm is a COMPRESSION read;");
        sb.AppendLine("  - I_occ is a COMPRESSION functional of the occupancies;");
        sb.AppendLine("  - the spectral shape (kurtosis) is a MOMENT read.");
        sb.AppendLine();

        sb.AppendLine($"phase is a compression read: {FifthOperatorSearch.PhaseIsCompressionRead()}");
        sb.AppendLine($"information is a compression read: {FifthOperatorSearch.InformationIsCompressionRead()}");
        sb.AppendLine($"shape is a moment read: {FifthOperatorSearch.ShapeIsMomentRead()}");
        sb.AppendLine();
        sb.AppendLine("CANDIDATES:");
        foreach (var c in FifthOperatorSearch.Candidates())
        {
            sb.AppendLine($"  {c.Name} ({c.Domain}) → reduces to {c.ReducesTo}");
            sb.AppendLine($"      {c.Evidence}");
        }

        Output.WriteLine(sb.ToString());

        Assert.True(FifthOperatorSearch.PhaseIsCompressionRead(),
            "the phase must be a COMPRESSION read (occ_top/Σm)");
        Assert.True(FifthOperatorSearch.InformationIsCompressionRead(),
            "the information must be a COMPRESSION functional");
        Assert.True(FifthOperatorSearch.ShapeIsMomentRead(),
            "the spectral shape must be a MOMENT read");
    }

    [Fact]
    public void ATQG3071_ZeroModeAndSynchronization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3071: the zero-mode is the boundary; synchronization is the source");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the zero mode is the Laplacian kernel (the background, QG270);");
        sb.AppendLine("  - SYNCHRONIZATION (the N=96 cycle) is the SOURCE of the spectrum, not a read.");
        sb.AppendLine();

        sb.AppendLine($"zero mode is the Laplacian kernel (boundary): {FifthOperatorSearch.ZeroModeIsBoundary()}");
        sb.AppendLine($"synchronization is the source (N=96 cycle): {FifthOperatorSearch.SynchronizationIsSource()}");
        sb.AppendLine($"four-operator basis complete: {FifthOperatorSearch.FourOperatorBasisComplete()}");
        sb.AppendLine();
        sb.AppendLine("The zero mode (constant vector in ker L) is the background — the boundary structure,");
        sb.AppendLine("the SYNCHRONIZATION cycle. SYNCHRONIZATION UNDERLIES all four operators but is not");
        sb.AppendLine("a projection of the spectrum — it is the generator, not a fifth read.");

        Output.WriteLine(sb.ToString());

        Assert.True(FifthOperatorSearch.ZeroModeIsBoundary(),
            "the zero mode must be the Laplacian kernel (the background)");
        Assert.True(FifthOperatorSearch.SynchronizationIsSource(),
            "SYNCHRONIZATION must be the source of the spectrum");
        Assert.True(FifthOperatorSearch.FourOperatorBasisComplete(),
            "the four-operator basis must be complete (QG261/262/263)");
    }

    [Fact]
    public void ATQG3072_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3072: the fifth-operator determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - FIFTH OPERATOR FOUND: an independent fifth operator exists;");
        sb.AppendLine("  - NO FIFTH OPERATOR: every candidate reduces to the existing basis.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FifthOperatorSearch.Summary()}");
        sb.AppendLine($"Search score: {FifthOperatorSearch.SearchScore()}/5");
        sb.AppendLine($"independent candidates: {FifthOperatorSearch.IndependentCount()}");
        sb.AppendLine($"no independent fifth: {FifthOperatorSearch.NoIndependentFifth()}");
        sb.AppendLine($"CLASSIFICATION = {FifthOperatorSearch.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - every candidate from the unexplored domains reduces to the existing basis:");
        sb.AppendLine("    · phase/orientation → COMPRESSION (sinδ_CP = occ_top/Σm = 87/95);");
        sb.AppendLine("    · information I_occ → COMPRESSION (the KL of the octave occupancies);");
        sb.AppendLine("    · spectral shape → MOMENT (the higher-order moments);");
        sb.AppendLine("    · zero-mode/boundary → the SYNCHRONIZATION source (the Laplacian kernel);");
        sb.AppendLine("    · synchronization → the generator of the spectrum (the N=96 cycle);");
        sb.AppendLine("  - no candidate is an independent fifth spectral operator — the basis");
        sb.AppendLine("    {CROWDING, COMPRESSION, BEAT, LOCKING} + the MOMENT read-out is COMPLETE.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NO FIFTH OPERATOR", FifthOperatorSearch.Classify());
        Assert.True(FifthOperatorSearch.SearchScore() >= 5);
        Assert.True(FifthOperatorSearch.NoIndependentFifth());
        Assert.Contains("NO FIFTH OPERATOR", FifthOperatorSearch.Summary());
    }
}
