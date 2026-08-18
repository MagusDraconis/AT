using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 82 — Origin of flavor mixing. Determines whether CKM/PMNS mixing can emerge from network family
/// indices. Classify: DERIVED / COMPATIBLE / NEW SECTOR.
///
/// Tests: TQMQG820 (family-index dynamics + link-state mixing), TQMQG821 (oscillations + rotations + CKM/PMNS),
/// TQMQG822 (classification).
/// </summary>
public class TQMQG_Phase82_FlavorMixingTests : ResearchTestBase
{
    public TQMQG_Phase82_FlavorMixingTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG820: family-index dynamics, link-state mixing ─────────────────────────

    [Fact]
    public void TQMQG820_FamilyIndexDynamics()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG820: can family indices mix via off-diagonal couplings?");

        bool offDiag = FlavorMixing.FamilyIndexCarriesOffDiagonal();
        bool linkMixes = FlavorMixing.LinkMixesFamilyIndices();

        sb.AppendLine($"family index carries OFF-DIAGONAL (mixing) couplings: {offDiag}");
        sb.AppendLine($"link mixes family indices via off-diagonal terms: {linkMixes}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: once the family index exists (QG81), off-diagonal couplings between indices are");
        sb.AppendLine("representable on the link — family-index dynamics is the mechanism that hosts mixing.");
        Output.WriteLine(sb.ToString());

        Assert.True(offDiag, "family index carries off-diagonal couplings");
        Assert.True(linkMixes, "link can mix family indices");
    }

    // ── TQMQG821: oscillations, rotations, CKM/PMNS interpretation ─────────────────

    [Fact]
    public void TQMQG821_OscillationsAndRotations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG821: flavor oscillations, mass rotations, CKM/PMNS parameters");

        bool oscillate = FlavorMixing.MixingGivesOscillations();
        bool unitary = FlavorMixing.MixingIsUnitaryRotation();
        bool representable = FlavorMixing.CkmPmnsRepresentable();
        bool derived = FlavorMixing.MixingEntriesDerived();
        int ckm = FlavorMixing.CkmParameterCount();
        int pmns = FlavorMixing.PmnsDiracParameterCount();
        int majorana = FlavorMixing.PmnsMajoranaPhases();

        sb.AppendLine($"mixing gives flavor oscillations: {oscillate}");
        sb.AppendLine($"mixing is a unitary rotation (flavor ↔ mass basis): {unitary}");
        sb.AppendLine($"CKM/PMNS representable on the family index: {representable}");
        sb.AppendLine($"specific CKM/PMNS entries DERIVED: {derived}");
        sb.AppendLine($"CKM real parameters (3 angles + 1 CP phase) = {ckm}");
        sb.AppendLine($"PMNS Dirac parameters (3 angles + 1 phase) = {pmns}");
        sb.AppendLine($"PMNS additional Majorana phases = {majorana}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: CKM/PMNS are representable as unitary rotations on the family index, and oscillations");
        sb.AppendLine("follow. But the specific angles and CP phase are FREE inputs, not network outputs.");
        Output.WriteLine(sb.ToString());

        Assert.True(oscillate, "mixing gives oscillations");
        Assert.True(unitary, "mixing is a unitary rotation");
        Assert.True(representable, "CKM/PMNS are representable");
        Assert.False(derived, "entries are not derived");
        Assert.Equal(4, ckm);
        Assert.Equal(4, pmns);
        Assert.Equal(2, majorana);
    }

    // ── TQMQG822: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG822_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG822: DERIVED / COMPATIBLE / NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {FlavorMixing.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the specific CKM/PMNS entries (angles, CP phase) are free empirical inputs.");
        sb.AppendLine("  • COMPATIBLE: mixing is representable as a unitary rotation on the family index (off-diagonal link");
        sb.AppendLine("    couplings); oscillations follow. No new sector is required.");
        sb.AppendLine("  • NOT NEW SECTOR: mixing needs no additional link content beyond the family index of QG81.");
        sb.AppendLine();
        sb.AppendLine("So flavor mixing is COMPATIBLE (representable) with the network, but not DERIVED from it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("COMPATIBLE", FlavorMixing.Classify());
        Assert.True(FlavorMixing.CkmPmnsRepresentable());
        Assert.False(FlavorMixing.MixingEntriesDerived());
    }
}
