using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 287 — Post-Duality Invariance Audit. The QG286 reinterpretation
/// (Difference → {ρ, ψ}) changes meaning but not numbers. Recomputed every frozen prediction
/// through the new hierarchy and compared to the pre-duality values. No new formulas, no retuning.
/// </summary>
public class ATQG_Phase287_PostDualityInvarianceAuditTests : ResearchTestBase
{
    public ATQG_Phase287_PostDualityInvarianceAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2870_PredictionRegistryInvariant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2870: the frozen registry (P1/P2/P3) is reproduced exactly");

        sb.AppendLine("HYPOTHESIS:");
        sb.AppendLine("  - the QG286 reinterpretation is meaning-only — it changes no number;");
        sb.AppendLine("  - P1/P2/P3 reproduce their frozen values through the new hierarchy.");
        sb.AppendLine();

        sb.AppendLine($"P1 106 GeV resonance: frozen 106.39 GeV, post-duality {PreRegistered106GeV.CentralMassGeV():F5} GeV");
        sb.AppendLine($"P2 0νββ m_ββ:        frozen 2.02 meV,     post-duality {PreRegisteredMbb.MbbMeV():F5} meV");
        sb.AppendLine($"P3 ladder rungs:      frozen 9,           post-duality {PreRegisteredLadderSpectrum.PredictedResonancesGeV().Length}");
        sb.AppendLine($"P3 first rungs (GeV): {string.Join(", ", PreRegisteredLadderSpectrum.PredictedResonancesGeV().Take(3).Select(x => x.ToString("F2", CultureInfo.InvariantCulture)))} ...");
        sb.AppendLine();
        sb.AppendLine($"registry lock still holds: {PostDualityInvarianceAudit.RegistryStillLocked()}");
        sb.AppendLine($"no new formulas / no retuning: {PostDualityInvarianceAudit.NoNewFormulasNoRetuning()}");

        Output.WriteLine(sb.ToString());

        Assert.True(PostDualityInvarianceAudit.RegistryStillLocked(),
            "the registry lock (QG193) must hold");
        Assert.True(PostDualityInvarianceAudit.NoNewFormulasNoRetuning(),
            "the reinterpretation must not add formulas or retune constants");
        Assert.True(PostDualityInvarianceAudit
                .AllEntries().Where(e => e.Category == "prediction")
                .All(e => e.Deviation < 0.005),
            "P1/P2/P3 must reproduce their frozen values within 0.5%");
    }

    [Fact]
    public void ATQG2871_MassesCouplingsMixingsInvariant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2871: masses, couplings, and mixings are numerically invariant");

        sb.AppendLine("HYPOTHESIS:");
        sb.AppendLine("  - every mass/coupling/mixing prediction is a ρ-face (count) read of Difference;");
        sb.AppendLine("  - the post-duality recomputation reproduces every frozen value.");
        sb.AppendLine();

        sb.AppendLine("MASSES:");
        sb.AppendLine($"  m_μ = {LeptonHierarchyExactLaw.MuonMass():F3} MeV  (frozen 105.79, dev {Dev(105.79, LeptonHierarchyExactLaw.MuonMass()):E2})");
        sb.AppendLine($"  m_τ = {LeptonHierarchyExactLaw.TauMass():F2} MeV  (frozen 1781.76, dev {Dev(1781.76, LeptonHierarchyExactLaw.TauMass()):E2})");
        sb.AppendLine($"  m_u = {QuarkMassOrigin.UpMass():F4} MeV  (frozen 2.164)");
        sb.AppendLine($"  m_t = {QuarkMassOrigin.TopMass():F1} MeV  (frozen 172704)");
        sb.AppendLine($"  m_ν2 = {NeutrinoMassLaw.M2():F6} eV  (frozen 8.72e-3)");
        sb.AppendLine("COUPLINGS:");
        sb.AppendLine($"  y_τ/y_μ = {YukawaOrigin.TauMuonRatio():F6}  (frozen 16.842)");
        sb.AppendLine($"  y_μ/y_e = {YukawaOrigin.MuonElectronRatio():F6}  (frozen 207.03)");
        sb.AppendLine($"  y_t/y_b = {YukawaOrigin.TopBottomRatio():F6}  (frozen 41.26)");
        sb.AppendLine($"  sin²θ_W = {WeakBosonMassOrigin.Sin2ThetaW():F6}  (frozen 0.2316)");
        sb.AppendLine("MIXINGS:");
        sb.AppendLine($"  Vus = {CKMOrigin.Vus():F6}  Vcb = {CKMOrigin.Vcb():F6}  Vub = {CKMOrigin.Vub():F6}");
        sb.AppendLine($"  θ12 = {PMNSOrigin.Theta12Deg():F3}°  θ23 = {PMNSOrigin.Theta23Deg():F3}°  θ13 = {PMNSOrigin.Theta13Deg():F3}°  δ = {PMNSOrigin.DeltaNuDeg():F2}°");
        sb.AppendLine();
        sb.AppendLine($"ψ enters no scalar prediction: {PostDualityInvarianceAudit.PsiEntersNoScalarPrediction()}");
        sb.AppendLine($"all inputs are ρ-face D96 primitives: {PostDualityInvarianceAudit.AllInputsAreRhoFace()}");

        Output.WriteLine(sb.ToString());

        Assert.True(PostDualityInvarianceAudit.PsiEntersNoScalarPrediction(),
            "ψ (tensor/orientation) must not enter any scalar prediction");
        Assert.True(PostDualityInvarianceAudit.AllInputsAreRhoFace(),
            "every prediction must be a read of the ρ-face D96 primitives");
        Assert.True(PostDualityInvarianceAudit
                .AllEntries().Where(e => e.Category is "mass" or "coupling" or "mixing")
                .All(e => e.Deviation < 0.005),
            "masses, couplings, and mixings must reproduce their frozen values within 0.5%");
    }

    [Fact]
    public void ATQG2872_CosmologyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2872: cosmology invariance and the final determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - INVARIANT (score 5-6): all frozen predictions reproduced, registry lock holds;");
        sb.AppendLine("  - PARTIAL SHIFT (3-4): some predictions drift;");
        sb.AppendLine("  - THEORY SHIFT (≤2): the reinterpretation changed the predictions.");
        sb.AppendLine();

        sb.AppendLine("COSMOLOGY:");
        sb.AppendLine($"  Ω_Λ = {CosmologicalFractionsOrigin.VacuumFraction():F6}  (frozen 0.6839)");
        sb.AppendLine($"  Ω_m = {CosmologicalFractionsOrigin.MatterFraction():F6}  (frozen 0.3161)");
        sb.AppendLine($"  n_s = {CmbSpectrumOrigin.SpectralIndex():F6}  (frozen 0.9650)");
        sb.AppendLine($"  ℓ₁ = {AcousticPeakOrigin.FirstPeak():F3}  ℓ₂ = {AcousticPeakOrigin.SecondPeak():F3}  ℓ₃ = {AcousticPeakOrigin.ThirdPeak():F3}");
        sb.AppendLine($"  ℓ₂/ℓ₁ = {AcousticPeakOrigin.SecondToFirstRatio():F6}  ℓ₃/ℓ₁ = {AcousticPeakOrigin.ThirdToFirstRatio():F6}");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {PostDualityInvarianceAudit.Summary()}");
        sb.AppendLine($"Invariance score: {PostDualityInvarianceAudit.InvarianceScore()}/6");
        sb.AppendLine($"Max deviation across {PostDualityInvarianceAudit.AllEntries().Length} predictions: {PostDualityInvarianceAudit.MaxDeviation():F6}");
        sb.AppendLine($"Mean deviation: {PostDualityInvarianceAudit.MeanDeviation():F6}");
        sb.AppendLine($"CLASSIFICATION = {PostDualityInvarianceAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the QG286 reinterpretation changed the ONTOLOGY (ρ and ψ are now dual faces of");
        sb.AppendLine("    one Difference, not independent primitives) but left the NUMBERS untouched;");
        sb.AppendLine("  - the duality lives at the level of the primitives' MEANING, not their values;");
        sb.AppendLine("  - every prediction is a function of the same ρ-face D96 constants, so every");
        sb.AppendLine("    prediction is numerically invariant;");
        sb.AppendLine("  - no new formula, no retuning — the theory's CONTENT is unchanged, only its");
        sb.AppendLine("    self-interpretation. The registry lock (QG193) still holds.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INVARIANT", PostDualityInvarianceAudit.Classify());
        Assert.True(PostDualityInvarianceAudit.InvarianceScore() >= 5);
        Assert.True(PostDualityInvarianceAudit.AllInvariant(), "max deviation must be below 0.5%");
        Assert.Contains("INVARIANT", PostDualityInvarianceAudit.Summary());
    }

    private static double Dev(double oldValue, double postValue)
        => Math.Abs(postValue / oldValue - 1.0);
}
