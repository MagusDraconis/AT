using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 96 — Stable State Selection. Determines whether the network possesses preferred stable states whose
/// spectra could select physical parameters. Classify: NO SELECTION / PARTIAL SELECTION / STATE SELECTION.
///
/// Tests: ATQG960 (energy minima + resonance modes), ATQG961 (attractors + spectrum selection + metastable),
/// ATQG962 (classification).
/// </summary>
public class ATQG_Phase96_StableStateSelectionTests : ResearchTestBase
{
    public ATQG_Phase96_StableStateSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG960: network energy minima, stable resonance modes ────────────────────

    [Fact]
    public void ATQG960_EnergyMinimaAndModes()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG960: native energy minima? stable modes?");

        bool energy = StableStateSelection.NativeEnergyFunctional();
        bool modes = StableStateSelection.StableModesExist();

        sb.AppendLine($"NATIVE energy functional with minima: {energy}");
        sb.AppendLine($"stable resonance modes exist: {modes}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: stable modes exist (QG95), but there is NO native energy functional whose minima select a");
        sb.AppendLine("preferred state — energy is derived as a concept (QG89), not as a native selection functional.");
        Output.WriteLine(sb.ToString());

        Assert.False(energy, "no native energy functional");
        Assert.True(modes, "stable modes exist");
    }

    // ── ATQG961: attractor states, discrete spectrum selection, metastable ────────

    [Fact]
    public void ATQG961_AttractorsSpectrumMetastable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG961: attractors, spectrum selection, metastable states");

        bool attractors = StableStateSelection.AttractorStatesNative();
        bool spectrumSelection = StableStateSelection.DiscreteSpectrumSelectionNative();
        bool metastable = StableStateSelection.MetastableStatesRepresentable();
        bool partial = StableStateSelection.PartialSelectionAchieved();
        bool full = StableStateSelection.FullStateSelectionAchieved();

        sb.AppendLine($"RG attractor states native (asymptotic freedom): {attractors}");
        sb.AppendLine($"native selection of WHICH eigenvalues are physical: {spectrumSelection}");
        sb.AppendLine($"metastable configurations representable: {metastable}");
        sb.AppendLine($"PARTIAL selection achieved (stability + attractors): {partial}");
        sb.AppendLine($"FULL state selection (unique preferred state) achieved: {full}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: stability and RG attractors PARTIALLY select/narrow the region, but nothing selects a unique");
        sb.AppendLine("preferred state whose spectrum equals the SM parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(attractors, "attractors native");
        Assert.False(spectrumSelection, "no native spectrum selection");
        Assert.True(metastable, "metastable representable");
        Assert.True(partial, "partial selection achieved");
        Assert.False(full, "full state selection not achieved");
    }

    // ── ATQG962: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG962_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG962: NO SELECTION / PARTIAL SELECTION / STATE SELECTION?");

        sb.AppendLine($"CLASSIFICATION: {StableStateSelection.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO SELECTION: stability + RG attractors do narrow the allowed region.");
        sb.AppendLine("  • NOT STATE SELECTION: no unique preferred state is selected whose spectrum equals the SM parameters.");
        sb.AppendLine("  • PARTIAL SELECTION: stability/attractors partially select; full state selection is absent.");
        sb.AppendLine();
        sb.AppendLine("So the network gives a PARTIAL SELECTION of parameter values (not a unique state selection).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL SELECTION", StableStateSelection.Classify());
        Assert.True(StableStateSelection.PartialSelectionAchieved());
        Assert.False(StableStateSelection.FullStateSelectionAchieved());
    }
}
