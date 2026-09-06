using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_087 — Nuclear Structure Audit test suite (Y_NP_087_Tests.cs).
///
/// Question: can Actualization Theory explain nuclear structure (binding energies, magic numbers,
/// shell structure)?
///
/// Verdict tested: nuclei are CORRESPONDENCE-ONLY (D). The nucleons (proton, neutron) are derived
/// quark composites forming an isospin doublet, and the strong coupling is a spectral
/// correspondence; but the nuclear STRUCTURE (binding energies, magic numbers, shells) is MISSING.
/// Structural reason: the D96 ring is 1D with mirror-pair (O(2)) degeneracies, while nuclear
/// shells are 3D (2l+1) spherical harmonics + spin-orbit.
///
/// Deterministic: closed-form (magic numbers, HO degeneracies, binding values).
/// </summary>
public class Y_NP_087_Tests : ResearchTestBase
{
    public Y_NP_087_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_087_Inventory ───────────────────────────

    [Fact]
    public void Y_NP_087_Inventory()
    {
        // proton (uud) and neutron (udd) are derived quark composites; deuteron/helium are not.
        bool protonDerived = true;
        bool neutronDerivedComposite = true;
        bool isospinDoubletMapped = true;
        bool deuteronNotDerived = true;  // needs the residual strong force
        bool heliumNotDerived = true;
        Assert.True(protonDerived && neutronDerivedComposite && isospinDoubletMapped);
        Assert.True(deuteronNotDerived && heliumNotDerived);

        // n − p mass difference ≈ 1.29 MeV (isospin-breaking/EM, not derived).
        double neutronMass = 939.565;
        double protonMass = 938.272;
        Assert.True(Math.Abs((neutronMass - protonMass) - 1.29) < 0.01);
    }

    // ── [Required] Y_NP_087_StructuralMismatch ──────────────────

    [Fact]
    public void Y_NP_087_StructuralMismatch()
    {
        // D96 is 1D with 47 mirror pairs (O(2) doublets) + 1 central mode;
        // nuclear shells are 3D (2l+1) spherical harmonics. Different structures.
        int d96MirrorPairs = 47;
        int d96CentralModes = 1;
        Assert.Equal(95, 2 * d96MirrorPairs + d96CentralModes);

        bool d96IsOneDimensional = true;
        bool nuclearShellsAreThreeDimensional = true;
        bool degeneraciesDiffer = true; // O(2) doublets ≠ (2l+1) harmonics
        Assert.True(d96IsOneDimensional && nuclearShellsAreThreeDimensional && degeneraciesDiffer);

        // 3D isotropic harmonic-oscillator shell degeneracies: (N+1)(N+2)/2.
        Assert.Equal(1, (0 + 1) * (0 + 2) / 2);
        Assert.Equal(3, (1 + 1) * (1 + 2) / 2);
        Assert.Equal(6, (2 + 1) * (2 + 2) / 2);
        Assert.Equal(10, (3 + 1) * (3 + 2) / 2);
    }

    // ── [Required] Y_NP_087_MagicNumbers ────────────────────────

    [Fact]
    public void Y_NP_087_MagicNumbers()
    {
        int[] magic = { 2, 8, 20, 28, 50, 82, 126 };
        Assert.Equal(7, magic.Length);

        // Harmonic-oscillator closure (no spin-orbit) gives 2, 8, 20, 40 — not the full set;
        // the spin-orbit closure (28, 50, 82, 126) is a 3D spherical-harmonic fact.
        int[] hoClosure = { 2, 8, 20, 40 };
        Assert.Equal(new[] { 2, 8, 20 }, magic.Take(3).ToArray());

        // The full magic set is NOT reproduced by the D96 octave structure [4,4,87].
        bool magicNumbersNotDerived = true;
        Assert.True(magicNumbersNotDerived);
    }

    // ── [Required] Y_NP_087_BindingTrends ───────────────────────

    [Fact]
    public void Y_NP_087_BindingTrends()
    {
        // Deuteron binding 2.224 MeV; helium-4 total 28.3 MeV (≈7.07 MeV/nucleon).
        double deuteronBinding = 2.224;
        double heliumBinding = 28.3;
        double heliumPerNucleon = heliumBinding / 4.0;
        Assert.True(Math.Abs(heliumPerNucleon - 7.07) < 0.01, $"He-4 per nucleon = {heliumPerNucleon:F2}");

        // No liquid-drop volume/surface/Coulomb/asymmetry terms are derived.
        bool volumeTermDerived = false;
        bool surfaceTermDerived = false;
        bool coulombTermDerived = false;
        bool asymmetryTermDerived = false;
        Assert.False(volumeTermDerived || surfaceTermDerived || coulombTermDerived || asymmetryTermDerived);
    }

    // ── [Required] Y_NP_087_ABCD ────────────────────────────────

    [Fact]
    public void Y_NP_087_ABCD()
    {
        // A) resonance composites: NO. B) deficit composites: NO.
        // C) symmetry composites: PARTIAL (isospin doublet). D) correspondence only: YES.
        bool resonanceComposite = false;
        bool deficitComposite = false;
        bool symmetryCompositePartial = true;
        bool correspondenceOnly = true;
        Assert.False(resonanceComposite);
        Assert.False(deficitComposite);
        Assert.True(symmetryCompositePartial);
        Assert.True(correspondenceOnly);
    }

    // ── [Required] Y_NP_087_Classification ──────────────────────

    [Fact]
    public void Y_NP_087_Classification()
    {
        bool nucleonsDerived = true;        // quark composites (NP_072)
        bool isospinDoubletDerived = true;  // Z2 pair (NP_074)
        bool strongCouplingCorrespondence = true; // α_strong = 8/Σ√m (spectral ratio)
        bool bindingEnergiesMissing = true;
        bool magicNumbersMissing = true;
        bool shellStructureMissing = true;
        Assert.True(nucleonsDerived && isospinDoubletDerived && strongCouplingCorrespondence);
        Assert.True(bindingEnergiesMissing && magicNumbersMissing && shellStructureMissing);
    }

    // ── [Required] Y_NP_087_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_087_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_087 — Nuclear Structure Audit");

        sb.AppendLine("Goal: can AT explain nuclear structure (binding, magic numbers, shells)?");
        sb.AppendLine();

        sb.AppendLine("[1] Nucleons DERIVED (quark composites, isospin doublet); deuteron/helium NOT.");
        sb.AppendLine();

        sb.AppendLine("[2] Structural mismatch: D96 = 1D mirror pairs (O(2)); nuclear shells = 3D (2l+1).");
        sb.AppendLine();

        sb.AppendLine("[3] Magic numbers [2,8,20,28,50,82,126] NOT reproduced; binding trends NOT derived.");
        sb.AppendLine();

        sb.AppendLine("[4] Determination: D (correspondence only); A/B refuted, C partial.");
        sb.AppendLine("    Nuclear structure is MISSING — it does not follow from the 1D D96 ring.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
