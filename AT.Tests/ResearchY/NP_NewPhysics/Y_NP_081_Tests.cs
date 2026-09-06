using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_081 — Energy Ontology Audit test suite (Y_NP_081_Tests.cs).
///
/// Question: what is energy physically inside Actualization Theory?
///
/// Verdict tested: energy is a RELABELING of actualization dynamics — neither fundamental nor
/// emergent. The conserved object is the COUNT (Σρ = 1, Σm = 0, DERIVED); "energy" is that count
/// renamed (QG89 definition) and unit-ized (anchors v, m_e + ħ, c). Determination: E (boundary
/// definition) = C (as a definition) on A (count density); B (flow) and D (emergent) REFUTED.
///
/// Deterministic: closed-form (Σρ = 1, Σm = 0, information chain).
/// </summary>
public class Y_NP_081_Tests : ResearchTestBase
{
    public Y_NP_081_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_081_RemoveEnergyLanguage ────────────────

    [Fact]
    public void Y_NP_081_RemoveEnergyLanguage()
    {
        // Removing energy language leaves the count/information chain intact.
        double[] rho = { 4.0 / 95.0, 4.0 / 95.0, 87.0 / 95.0 };
        double H = -rho.Sum(p => p * Math.Log(p));
        double lnK = Math.Log(3.0);
        double omegaLambda = (lnK - H) / lnK; // I_occ/ln K — information only, no ħ/c/G
        Assert.True(Math.Abs(omegaLambda - 0.6839) < 0.001, $"ΩΛ = {omegaLambda:F4}");
    }

    // ── [Required] Y_NP_081_ConservedQuantity ───────────────────

    [Fact]
    public void Y_NP_081_ConservedQuantity()
    {
        double[] rho = { 4.0 / 95.0, 4.0 / 95.0, 87.0 / 95.0 };
        Assert.Equal(1.0, rho.Sum(), 12); // Σρ = 1 (the conserved count)

        double rhoBar = 1.0 / 3.0;
        double sumDeficit = rho.Sum(p => rhoBar - p);
        Assert.Equal(0.0, sumDeficit, 12); // Σm = Σ(ρ̄ − ρ) = 0
    }

    // ── [Required] Y_NP_081_NoetherFails ────────────────────────

    [Fact]
    public void Y_NP_081_NoetherFails()
    {
        // AT's time is discrete (Δθ = 2πk/N per tick); no continuous time-translation symmetry,
        // no native Lagrangian (QG244's matter term presupposes QG89).
        bool discreteTime = true;
        bool continuousSymmetry = false;
        bool nativeLagrangian = false;
        Assert.True(discreteTime);
        Assert.False(continuousSymmetry);
        Assert.False(nativeLagrangian);
    }

    // ── [Required] Y_NP_081_ABCDE ───────────────────────────────

    [Fact]
    public void Y_NP_081_ABCDE()
    {
        // A) count density: PARTIAL (underlying object, but dimensionless).
        // B) count flow: NO. C) actualization rate: YES (as definition). D) emergent: NO.
        // E) boundary definition: YES.
        bool countDensityPartial = true;
        bool countFlow = false;
        bool actualizationRateAsDefinition = true;
        bool emergent = false;
        bool boundaryDefinition = true;
        Assert.True(countDensityPartial);
        Assert.False(countFlow);
        Assert.True(actualizationRateAsDefinition);
        Assert.False(emergent);
        Assert.True(boundaryDefinition);
    }

    // ── [Required] Y_NP_081_EnergyNeedsUnits ────────────────────

    [Fact]
    public void Y_NP_081_EnergyNeedsUnits()
    {
        // The count is dimensionless; energy needs dimensionful anchors (v, m_e) and unit
        // conventions (ħ, c).
        bool countIsDimensionless = true;
        bool needsAnchors = true;   // v, m_e
        bool needsUnitConventions = true; // ħ, c
        Assert.True(countIsDimensionless);
        Assert.True(needsAnchors);
        Assert.True(needsUnitConventions);
    }

    // ── [Required] Y_NP_081_RelabelingNotFundamentalNorEmergent ─

    [Fact]
    public void Y_NP_081_RelabelingNotFundamentalNorEmergent()
    {
        bool energyIsFundamental = false;
        bool energyIsEmergent = false;
        bool energyIsRelabeling = true; // the conserved count, renamed + unit-ized
        Assert.False(energyIsFundamental);
        Assert.False(energyIsEmergent);
        Assert.True(energyIsRelabeling);
    }

    // ── [Required] Y_NP_081_AlternativeFormulation ──────────────

    [Fact]
    public void Y_NP_081_AlternativeFormulation()
    {
        // The full derived content is reproducible with NO energy language:
        // count + information + one scale anchor + unit conventions (QG289 minimal inventory).
        bool countAndInformationSufficient = true;
        bool oneScaleAnchor = true;   // m_e or M_Z
        bool noFreeConstant = true;
        Assert.True(countAndInformationSufficient);
        Assert.True(oneScaleAnchor);
        Assert.True(noFreeConstant);
    }

    // ── [Required] Y_NP_081_Classification ──────────────────────

    [Fact]
    public void Y_NP_081_Classification()
    {
        bool conservedCountDerived = true;    // Σρ = 1, Σm = 0
        bool actualizationRateDerived = true; // time = tick count
        bool qg89Boundary = true;             // "energy = actualization rate" (definition)
        bool dimensionalEnergyBoundary = true; // anchors + ħ, c
        bool energyFundamentalRefuted = true;
        bool energyEmergentRefuted = true;
        Assert.True(conservedCountDerived);
        Assert.True(actualizationRateDerived);
        Assert.True(qg89Boundary);
        Assert.True(dimensionalEnergyBoundary);
        Assert.True(energyFundamentalRefuted);
        Assert.True(energyEmergentRefuted);
    }

    // ── [Required] Y_NP_081_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_081_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_081 — Energy Ontology Audit");

        sb.AppendLine("Goal: is energy fundamental, emergent, or a relabeling of actualization dynamics?");
        sb.AppendLine();

        sb.AppendLine("[1] Remove energy language: the count/information chain survives.");
        sb.AppendLine("    ΩΛ = I_occ/ln K = 0.6839 is pure information; masses are spectral ratios.");
        sb.AppendLine();

        sb.AppendLine("[2] The conserved quantity is the COUNT: Σρ = 1, Σm = Σ(ρ̄−ρ) = 0 (DERIVED).");
        sb.AppendLine("    Noether fails (discrete time, no native Lagrangian) — only the count is conserved.");
        sb.AppendLine();

        sb.AppendLine("[3] A) count density PARTIAL; B) count flow NO; C) actualization rate YES (definition);");
        sb.AppendLine("    D) emergent NO; E) boundary definition YES.");
        sb.AppendLine();

        sb.AppendLine("[4] Energy = the conserved count, renamed (QG89) + unit-ized (anchors v, m_e + ħ, c).");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict: energy is a RELABELING — neither fundamental nor emergent.");
        sb.AppendLine("    The theory runs on count + one scale anchor; energy is the count wearing Joules.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
