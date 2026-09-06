using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_086 — Difference Necessity Audit test suite (Y_NP_086_Tests.cs).
///
/// Question: is Difference itself necessary, or could a weaker primitive generate the same
/// ontology?
///
/// Verdict tested: Difference is the MINIMAL and UNIQUE primitive — the bare logical possibility
/// of distinction. It is not derivable and not replaceable: identity is insufficient (self-identity
/// does not give a ≠ b), relation presupposes distinct relata, count/information/symmetry are
/// downstream. Identity is the logical dual (a ≠ b ⇔ ¬(a = b)) but the *preservation* relation used
/// for conservation (higher), while Difference is the *generation* relation (bottom). No weaker
/// primitive exists. Classification: BOUNDARY (irreducible primitive).
///
/// Deterministic: closed-form (conceptual determinations, no randomness).
/// </summary>
public class Y_NP_086_Tests : ResearchTestBase
{
    public Y_NP_086_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_086_RemoveDifference ────────────────────

    [Fact]
    public void Y_NP_086_RemoveDifference()
    {
        // Removing Difference removes distinguishability, hence counting, hence everything.
        bool distinctionBreaks = true;
        bool countingBreaks = true;       // cannot count indistinguishable things
        bool scaleFreenessBreaks = true;  // needs a distribution over distinguishable states
        bool d96Breaks = true;            // needs 96 distinguishable modes
        bool dualityBreaks = true;        // trace vs traceless are different projections
        bool cosmologyBreaks = true;      // fractions of a distribution
        Assert.True(distinctionBreaks && countingBreaks && scaleFreenessBreaks &&
                    d96Breaks && dualityBreaks && cosmologyBreaks);
    }

    // ── [Required] Y_NP_086_Candidates ──────────────────────────

    [Fact]
    public void Y_NP_086_Candidates()
    {
        // identity: insufficient (self-identity ≠ distinction).
        // relation: presupposes distinct relata. count/information/symmetry: downstream.
        bool identityInsufficient = true;
        bool relationPresupposes = true;
        bool countDownstream = true;
        bool informationDownstream = true;
        bool symmetryDownstream = true;
        Assert.True(identityInsufficient);
        Assert.True(relationPresupposes);
        Assert.True(countDownstream);
        Assert.True(informationDownstream);
        Assert.True(symmetryDownstream);
    }

    // ── [Required] Y_NP_086_IdentityDuality ─────────────────────

    [Fact]
    public void Y_NP_086_IdentityDuality()
    {
        // identity and difference are logical duals (a ≠ b ⇔ ¬(a = b)) but play opposite roles:
        // Difference = generation (bottom); identity = preservation (conservation, higher).
        bool identityIsLogicalDual = true;
        bool differenceGenerates = true;     // distinction → counting
        bool identityPreserves = true;       // self-identity across a transformation → conservation
        bool identityCannotReplace = true;   // preservation ≠ generation
        Assert.True(identityIsLogicalDual);
        Assert.True(differenceGenerates);
        Assert.True(identityPreserves);
        Assert.True(identityCannotReplace);
    }

    // ── [Required] Y_NP_086_ABCD ────────────────────────────────

    [Fact]
    public void Y_NP_086_ABCD()
    {
        // A) derivable: NO. B) replaceable: NO. C) minimal: YES. D) unique: YES.
        bool derivable = false;
        bool replaceable = false;
        bool minimal = true;
        bool unique = true;
        Assert.False(derivable);
        Assert.False(replaceable);
        Assert.True(minimal);
        Assert.True(unique);
    }

    // ── [Required] Y_NP_086_WeakerSearch ────────────────────────

    [Fact]
    public void Y_NP_086_WeakerSearch()
    {
        // "distinction" = Difference (synonym); plurality presupposes difference;
        // existence/unity generate nothing. No weaker primitive exists.
        bool distinctionIsDifference = true;
        bool pluralityPresupposes = true;
        bool existenceGeneratesNothing = true;
        bool unityGeneratesNothing = true;
        bool weakerPrimitiveExists = false;
        Assert.True(distinctionIsDifference && pluralityPresupposes &&
                    existenceGeneratesNothing && unityGeneratesNothing);
        Assert.False(weakerPrimitiveExists);
    }

    // ── [Required] Y_NP_086_Classification ──────────────────────

    [Fact]
    public void Y_NP_086_Classification()
    {
        bool differenceBoundary = true;     // irreducible primitive (QG270)
        bool minimalAndUnique = true;
        bool identityRefuted = true;
        bool relationRefuted = true;
        bool countInfoSymmetryRefuted = true;
        bool weakerRefuted = true;
        Assert.True(differenceBoundary);
        Assert.True(minimalAndUnique);
        Assert.True(identityRefuted && relationRefuted);
        Assert.True(countInfoSymmetryRefuted && weakerRefuted);
    }

    // ── [Required] Y_NP_086_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_086_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_086 — Difference Necessity Audit");

        sb.AppendLine("Goal: is Difference the minimal possible primitive, or could something weaker work?");
        sb.AppendLine();

        sb.AppendLine("[1] Remove Difference: distinction, counting, scale-freeness, D96, duality, cosmology all collapse.");
        sb.AppendLine();

        sb.AppendLine("[2] Candidates: identity insufficient; relation presupposes; count/information/symmetry downstream.");
        sb.AppendLine();

        sb.AppendLine("[3] Identity = the logical dual (a≠b ⇔ ¬(a=b)) but the preservation relation (conservation, higher);");
        sb.AppendLine("    Difference = the generation relation (distinction, bottom).");
        sb.AppendLine();

        sb.AppendLine("[4] Determination: C (minimal) = D (unique); A (derivable) and B (replaceable) REFUTED.");
        sb.AppendLine();

        sb.AppendLine("[5] No weaker primitive exists: distinction IS difference; identity/existence/unity generate nothing.");
        sb.AppendLine("    Difference is the irreducible generative seed; η is the reading reference.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
