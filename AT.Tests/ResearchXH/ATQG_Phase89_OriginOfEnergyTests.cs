using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 89 — Origin of energy. Determines what energy is in the network.
/// Classify: DERIVED / COMPATIBLE / NEW SECTOR.
///
/// Tests: ATQG890 (actualization rate + link-update activity), ATQG891 (excitation + mass-energy + conservation),
/// ATQG892 (classification).
/// </summary>
public class ATQG_Phase89_OriginOfEnergyTests : ResearchTestBase
{
    public ATQG_Phase89_OriginOfEnergyTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG890: actualization rate, link-update activity ─────────────────────────

    [Fact]
    public void ATQG890_ActualizationAndActivity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG890: energy = conjugate of causal-order evolution");

        bool time = OriginOfEnergy.TimeIsCausalOrder();
        bool conjugate = OriginOfEnergy.EnergyIsConjugateToTime();
        bool actualization = OriginOfEnergy.EnergyIsActualizationActivity();
        bool flux = OriginOfEnergy.LinkUpdateCarriesEnergy();

        sb.AppendLine($"network time = causal order (from Q-events): {time}");
        sb.AppendLine($"energy = conjugate/generator of time translation: {conjugate}");
        sb.AppendLine($"energy measured as actualization rate (Q-event activity): {actualization}");
        sb.AppendLine($"link-update activity carries energy flux: {flux}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: energy is the conserved generator of causal-order evolution — a structural identification,");
        sb.AppendLine("not an extra postulate. Actualization rate is its network expression; link updates carry its flux.");
        Output.WriteLine(sb.ToString());

        Assert.True(time, "time is causal order");
        Assert.True(conjugate, "energy is conjugate to time");
        Assert.True(actualization, "energy is actualization activity");
        Assert.True(flux, "link updates carry energy");
    }

    // ── ATQG891: excitation, mass-energy equivalence, conservation ────────────────

    [Fact]
    public void ATQG891_ExcitationEquivalenceConservation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG891: excitation, E = mc^2, conservation");

        bool store = OriginOfEnergy.ExcitationStoresEnergy();
        bool emc2 = OriginOfEnergy.MassEnergyEquivalenceRepresentable();
        bool noether = OriginOfEnergy.EnergyConservationViaNoether();
        bool concept = OriginOfEnergy.EnergyConceptDerived();
        bool values = OriginOfEnergy.EnergyValuesDerived();

        sb.AppendLine($"stored ψ/ρ excitation holds energy: {store}");
        sb.AppendLine($"mass-energy equivalence E = mc^2 representable: {emc2}");
        sb.AppendLine($"conservation via time-translation symmetry (Noether): {noether}");
        sb.AppendLine($"CONCEPT of energy DERIVED: {concept}");
        sb.AppendLine($"specific energy VALUES DERIVED: {values}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: energy is stored in ψ/ρ excitation, E = mc² links the Higgs condensate (rest mass) to energy,");
        sb.AppendLine("and conservation follows from Noether. The concept is derived; the specific values remain empirical.");
        Output.WriteLine(sb.ToString());

        Assert.True(store, "excitation stores energy");
        Assert.True(emc2, "mass-energy equivalence representable");
        Assert.True(noether, "conservation via Noether");
        Assert.True(concept, "concept derived");
        Assert.False(values, "values not derived");
    }

    // ── ATQG892: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG892_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG892: DERIVED / COMPATIBLE / NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {OriginOfEnergy.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • DERIVED (concept): energy is the conserved generator of causal-order evolution (Noether), not an extra");
        sb.AppendLine("    postulate — actualization rate, link-update flux, and ψ/ρ excitation are its carriers.");
        sb.AppendLine("  • NOT NEW SECTOR: no new representation is required.");
        sb.AppendLine("  • NUANCE: the specific energy VALUES (Hamiltonian, masses) remain empirical (QG85).");
        sb.AppendLine();
        sb.AppendLine("So the CONCEPT of energy is DERIVED; its values are postulatory.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", OriginOfEnergy.Classify());
        Assert.True(OriginOfEnergy.EnergyConceptDerived());
        Assert.False(OriginOfEnergy.EnergyValuesDerived());
    }
}
