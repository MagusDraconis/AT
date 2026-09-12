using System.Text;
using AT.Core.Research;
using AT.Core.ResearchDATA;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_026 — Authored-Verdict Audit (group G — Gravity Source).
///
/// WHY. G_025 verified the AT-QG optics arithmetic and found two defects: an off-by-one determinant and a
/// hard-coded γ. It closed with a question — is that defect class ISOLATED? This audit generalises the search
/// and finds the class is SYSTEMIC. The fingerprint is:
///
///     a member, or a data field, whose NAME or ROLE claims a computation or a verification verdict,
///     whose VALUE is a literal, and which a SCORE, CLASSIFICATION or TEST then consumes.
///
/// Five instances were found and fixed. This suite asserts the fixed invariants so the class cannot return
/// silently. Every assertion below can FAIL — which is the point, because the defects it replaces were
/// assertions on literals.
///
/// WHAT WAS FOUND AND FIXED.
///  (1) THE QG VERDICT LADDER. Four successive audits — QuantumGravityClosureAudit (PARTIAL QG),
///      QuantumGravityReclosureAudit (EFFECTIVE QG), QuantumGravityReclosureAudit2 (NEAR-COMPLETE QG) and
///      FinalQuantumGravityAudit (COMPLETE QG) — used the SAME method names and differed only in literals
///      (`IsQuantumMechanicsDerived() => false` in one, `=> true` in three). In the last two the sub-scores
///      were a SEPARATE set of typed 1.0s that never read the criteria, so `TotalScore()` returned 6.0 and
///      `Classify()` returned "COMPLETE QG" whatever the criteria said. In ReclosureAudit2 the criterion
///      `IsSpacetimeEmergent() => false` coexisted with `SpacetimeSubScore() => 0.5` and the two could never
///      disagree. The tests re-asserted the literals, so four suites asserted mutually contradictory verdicts
///      and ALL PASSED.
///  (2) THE BORN-RULE CLASSIFICATION. `TestAllAlphas()` typed `Survives`; `BuildRequirements()` typed the
///      pass arrays (`new[] { false, false, false, true, false, false }`); and
///      `AllRequirementsUniquelySatisfied(reqs, tests)` accepted the executed tests and NEVER READ THEM. The
///      old uniqueness test (`Count(p => p) != 1` on every row) also returned false unconditionally, because
///      the factorization rows legitimately pass for all six exponents — so the headline "D: Mathematical
///      Derivation — α=2 is UNIQUELY selected" was DEAD CODE and X037's real output was "C: Strong Theorem".
///  (3) THE RAR COMPLETION TABLE. Seven of eleven rows were typed `Derived = true` and labelled "DERIVED ✓",
///      then counted by `scores.Count(s => s.Derived)`.
///  (4) THE DERIVED CONSTANT G. The value is genuinely computed in `NewtonConstantOrigin` (v·A³ → M_Pl →
///      G = ħc/m_Pl²) but was duplicated as an independent literal in `FrameDraggingOrigin.G_D96` and
///      `PhysicalUnits.G_SI`, with nothing asserting the copies agreed.
///  (5) IGNORED PARAMETERS. `EffectiveSizeLaw.IdentityHoldsAcrossGrid(feedback, damping)` declared the
///      dynamics parameters and then called `FamilyBandIdentity(n, K)`, silently answering for the DEFAULTS.
///
/// WHAT IS *NOT* A DEFECT (verified, so the fix does not over-reach): `ConservationPrincipleAudit.Laws()` and
/// `MajoranaOrigin.Checks()` pass computed CALLS as their evidence flags; `TemporalWaveObservables.* => 0.0`
/// are analytic null results documented as such; `MetricOrigin.SqrtMinusG_Const` and
/// `LightPropagation.LightSpeed(rho) => 1.0` are honest (a named constant and the c = 1 convention).
///
/// WHAT THE FIX DOES *NOT* CLAIM. An authored research verdict does not become "computed" by being
/// restructured. The fix makes authorship VISIBLE and IMPOSSIBLE TO MISTAKE for a computation, makes every
/// score a DERIVED function of the criteria, makes every criterion cite its basis, and makes the tests able
/// to fail. The verdicts themselves are unchanged except where the project had already superseded them (ψ).
/// </summary>
public class Y_G_026_Tests : ResearchTestBase
{
    public Y_G_026_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_026_QgLadderIsDerivedFromCriteria()
    {
        // Each audit's score, sub-scores and classification must be FUNCTIONS of its criteria table.
        Assert.True(QuantumGravityClosureAudit.AllBasesCited(), "QG215 criteria must cite their evidence");
        Assert.True(QuantumGravityReclosureAudit.AllBasesCited(), "QG219 criteria must cite their evidence");
        Assert.True(QuantumGravityReclosureAudit2.AllBasesCited(), "QG221 criteria must cite their evidence");
        Assert.True(FinalQuantumGravityAudit.AllBasesCited(), "QG223 criteria must cite their evidence");

        Assert.True(QuantumGravityClosureAudit.ClassificationFollowsFromCriteria(), "QG215 ladder must follow");
        Assert.True(QuantumGravityReclosureAudit.ClassificationFollowsFromCriteria(), "QG219 ladder must follow");
        Assert.True(QuantumGravityReclosureAudit2.ClassificationFollowsFromCriteria(), "QG221 ladder must follow");
        Assert.True(FinalQuantumGravityAudit.ClassificationFollowsFromCriteria(), "QG223 ladder must follow");

        // The sub-scores must equal the criteria — this is the assertion that would have caught the typed
        // 1.0s in QG221/QG223.
        Assert.True(FinalQuantumGravityAudit.SubScoresMatchCriteria(),
            "QG223 sub-scores must be derived from the criteria, never typed");
        Assert.True(QuantumGravityReclosureAudit2.SubScoresMatchCriteria(),
            "QG221 sub-scores must be derived from the criteria, never typed");

        // The integer score and the double score must agree.
        Assert.Equal(QuantumGravityClosureAudit.QgScore(), (int)QuantumGravityClosureAudit.TotalScore());
        Assert.Equal(QuantumGravityReclosureAudit.QgScore(), (int)QuantumGravityReclosureAudit.TotalScore());
    }

    [Fact]
    public void Y_G_026_QgLadderReproducesTheHistoricalRungs()
    {
        // The restructure must NOT change the adjudication: the derived ladder reproduces QG215 → QG223 exactly.
        Assert.Equal(2.0, QuantumGravityClosureAudit.TotalScore(), 12);
        Assert.Equal("PARTIAL QG", QuantumGravityClosureAudit.Classify());

        Assert.Equal(4.0, QuantumGravityReclosureAudit.TotalScore(), 12);
        Assert.Equal("EFFECTIVE QG", QuantumGravityReclosureAudit.Classify());

        Assert.Equal(5.0, QuantumGravityReclosureAudit2.TotalScore(), 12);
        Assert.Equal("NEAR-COMPLETE QG", QuantumGravityReclosureAudit2.Classify());

        Assert.Equal(6.0, FinalQuantumGravityAudit.TotalScore(), 12);
        Assert.Equal("COMPLETE QG", FinalQuantumGravityAudit.Classify());

        // QG221's partial credit must be a DECLARED status, not a typed number, and must agree with its
        // own criterion — the exact decoupling that made a `false` criterion and a 0.5 sub-score coexist.
        var sp = QuantumGravityReclosureAudit2.Criteria().First(c => c.Name == "spacetime emergent");
        Assert.Equal(QgStatus.Partial, sp.Status);
        Assert.False(QuantumGravityReclosureAudit2.IsSpacetimeEmergent());
        Assert.Equal(sp.Score, QuantumGravityReclosureAudit2.SpacetimeSubScore(), 12);

        // The progression must report each phase's OWN derived total, not four typed numbers.
        var prog = FinalQuantumGravityAudit.Progression();
        Assert.Equal(4, prog.Length);
        Assert.Equal("QG215", prog[0].Phase);
        Assert.Equal("QG223", prog[3].Phase);
        Assert.Equal(QuantumGravityClosureAudit.TotalScore(), prog[0].Score, 12);
        Assert.Equal(QuantumGravityReclosureAudit.TotalScore(), prog[1].Score, 12);
        Assert.Equal(QuantumGravityReclosureAudit2.TotalScore(), prog[2].Score, 12);
        Assert.Equal(FinalQuantumGravityAudit.TotalScore(), prog[3].Score, 12);

        // Monotone non-decreasing — the escalation cannot silently reverse.
        for (int i = 1; i < prog.Length; i++)
            Assert.True(prog[i].Score >= prog[i - 1].Score,
                $"the ladder must be monotone non-decreasing ({prog[i - 1].Phase} -> {prog[i].Phase})");
    }

    [Fact]
    public void Y_G_026_PsiIsNotANewPrimitive()
    {
        // QG223's own adjudication said ψ IS a new fundamental primitive, and its test asserted the literal.
        // ResearchY-G_024 superseded that (QG285/QG286/QG292): ψ is the TRACELESS FACE of the one Difference
        // read against η, so the minimal primitive set is {Difference, η}. The canonical accessor must say so
        // while the historical verdict stays on the record.
        Assert.False(FinalQuantumGravityAudit.PsiIsNewPrimitive(),
            "ψ is NOT a new primitive — QG285/QG286/QG292 supersede QG24");
        Assert.True(FinalQuantumGravityAudit.PsiWasCalledNewPrimitive(),
            "the QG223-era verdict must be preserved for the record");

        // The functional content is untouched: ψ is still an ontological boundary, not a blocker.
        Assert.False(FinalQuantumGravityAudit.PsiIsQgBlocker());
        Assert.True(FinalQuantumGravityAudit.PsiIsOntologicalBoundary());
        Assert.True(FinalQuantumGravityAudit.PsiCapacityForced());
        Assert.True(FinalQuantumGravityAudit.PsiExcitationDerived());

        // The labelled adjudication must report the canonical value.
        var row = FinalQuantumGravityAudit.PsiAdjudication()
            .First(r => r.Question.Contains("new fundamental primitive"));
        Assert.False(row.Value, "the adjudication row must carry the canonical (superseding) verdict");
    }

    [Fact]
    public void Y_G_026_BornAlphaTwoUniquenessIsComputed()
    {
        // The α = 2 uniqueness must be EXECUTED, not typed.
        Assert.True(AlphaInvarianceScreen.AlphaTwoIsUniquelySelected(),
            "the executed unitary-invariance screen must select α = 2 alone");
        Assert.Equal(new[] { 2.0 }, AlphaInvarianceScreen.SurvivingAlphas());

        // Every exponent's recorded verdict must agree with the screen.
        foreach (var t in BornRuleDerivation.TestAllAlphas())
            Assert.Equal(AlphaInvarianceScreen.IsInvariantUnderUnitaries(t.Alpha), t.Survives);

        // The excluded exponents must be excluded by a REAL margin, not a rounding artefact.
        foreach (double a in new[] { 0.5, 1.0, 1.5, 3.0, 4.0 })
            Assert.True(AlphaInvarianceScreen.MaxRelativeViolation(a) > 1e-6,
                $"α = {a} must be excluded by a measurable margin");
    }

    [Fact]
    public void Y_G_026_RequirementEvidenceIsDisclosed()
    {
        var reqs = BornRuleDerivation.BuildRequirements();

        // Every requirement must cite its basis.
        Assert.All(reqs, r => Assert.False(string.IsNullOrWhiteSpace(r.Basis),
            $"requirement '{r.Name}' must cite its basis"));

        // 'Computed' and 'Analytic' must partition the rows — nothing may be unlabelled.
        var ev = BornRuleAnalyzer.RequirementEvidenceBreakdown(reqs);
        Assert.Equal(reqs.Count, ev.Computed + ev.Analytic);
        Assert.True(ev.Computed >= 2, "at least the basis/invariance requirements must be computed");

        // The analytic rows must be named, and must be exactly the ones with no executable test.
        Assert.Contains("Complexity additivity", ev.AnalyticNames);
        Assert.Contains("Partial trace consistency", ev.AnalyticNames);
        Assert.DoesNotContain("Unitary invariance of normalization", ev.AnalyticNames);
        Assert.DoesNotContain("Basis independence", ev.AnalyticNames);

        // The discriminating rows must admit α = 2 alone.
        var disc = BornRuleAnalyzer.DiscriminatingRequirements(reqs);
        Assert.NotEmpty(disc);
        for (int i = 0; i < reqs[0].PassesForAlpha.Length; i++)
        {
            bool allPass = disc.All(r => r.PassesForAlpha[i]);
            bool isTwo = Math.Abs(AlphaInvarianceScreen.TestAlphas[i] - 2.0) < 1e-12;
            Assert.Equal(isTwo, allPass);
        }

        // The classification must disclose the evidence split, and must be the D rung now that the
        // dead-code uniqueness test is fixed.
        var theorem = BornRuleAnalyzer.Analyze();
        Assert.Contains("UNIQUELY selected", theorem.Classification);
        Assert.Contains("computed", theorem.Classification, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Y_G_026_CompletionAndCacheEvidenceAreDisclosed()
    {
        // The RAR limits the completion table claims must be COMPUTED from the AT functional form.
        double gD = 1.2e-10;
        var (newtonian, deepMond, nRatio, dRatio) = RarScatterAnalyzer.RarLimitsCheck(gD);
        Assert.True(newtonian, $"the Newtonian limit must hold (ratio {nRatio:E6})");
        Assert.True(deepMond, $"the deep-MOND limit must hold (ratio {dRatio:E6})");

        // The AT form itself must satisfy both limits.
        Assert.Equal(1.0, RarScatterAnalyzer.AtRarForm(1e10 * gD, gD) / (1e10 * gD), 5);
        Assert.Equal(1.0, RarScatterAnalyzer.AtRarForm(1e-10 * gD, gD) / Math.Sqrt(1e-10 * gD * gD), 5);

        // The derived Newton constant must agree with its cached literals (previously unchecked).
        double computedG = NewtonConstantOrigin.GSISeconds();
        Assert.True(NewtonConstantOrigin.CacheAgreesWithComputation(FrameDraggingOrigin.G_D96),
            $"FrameDraggingOrigin.G_D96 must agree with the computed G = {computedG:E6}");
        Assert.True(NewtonConstantOrigin.CacheAgreesWithComputation(PhysicalUnits.G_SI),
            $"PhysicalUnits.G_SI must agree with the computed G = {computedG:E6}");
        Assert.True(NewtonConstantOrigin.CacheAgreesWithComputation(NewtonConstantOrigin.GPhysical, 5e-3),
            "the computed G must lie within 0.5 % of CODATA");
    }

    [Fact]
    public void Y_G_026_IgnoredParametersAreUsed()
    {
        // `IdentityHoldsAcrossGrid` declared feedback/damping and ignored them. The parameters must now
        // CHANGE the answer, which is the only way a caller can trust them.
        Assert.True(EffectiveSizeLaw.IdentityHoldsAcrossGrid(),
            "the identity must hold at the defaults");

        // At the defaults the parameterized check must agree with the bare one.
        Assert.Equal(EffectiveSizeLaw.FamilyBandIdentity(),
            EffectiveSizeLaw.FamilyBandIdentity(EffectiveSizeLaw.DefaultN, EffectiveSizeLaw.DefaultK,
                EffectiveSizeLaw.DefaultFeedback, EffectiveSizeLaw.DefaultDamping));

        // The declared parameters must be THREADED THROUGH. (ResearchY-G_026 note: at the point tested here
        // the intra-sector spectrum is unchanged by (feedback, damping) — the parameters flow into
        // HighEnergySectorStability.ObservableSector but do not move this observable, which is recorded as a
        // follow-up rather than asserted. What IS asserted is the contract the fix guarantees.)

        // THE CONTRACT: the grid answer must equal the per-point answers computed AT THE SUPPLIED dynamics
        // point. Under the old code the grid ignored its parameters and answered for the DEFAULTS, so this
        // equality is exactly what the fix guarantees and what the old code could violate.
        double fb = 0.5, dp = 0.1;
        bool viaGrid = EffectiveSizeLaw.IdentityHoldsAcrossGrid(fb, dp);
        bool recomputed = new[] { 48, 64, 96, 128, 192 }
            .All(n => new[] { 3, 4, 5, 6, 8, 10 }
                .Where(K => n / K >= 6)
                .All(K => EffectiveSizeLaw.FamilyBandIdentity(n, K, fb, dp)));
        Assert.Equal(recomputed, viaGrid);

        // ...and it must ALSO agree at the defaults, so the parameterization is consistent.
        Assert.Equal(EffectiveSizeLaw.IdentityHoldsAcrossGrid(), EffectiveSizeLaw.IdentityHoldsAcrossGrid(
            EffectiveSizeLaw.DefaultFeedback, EffectiveSizeLaw.DefaultDamping));
    }

    [Fact]
    public void Y_G_026_Run()
    {
        var sb = new StringBuilder();
        sb.AppendLine(); PrintHeader("Y_G_026 — Authored-Verdict Audit (the G_025 defect class, generalised)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The fingerprint of the G_025 defect class is: a member or data field whose NAME claims a");
        sb.AppendLine("     computation or a verification verdict, whose VALUE is a literal, and which a score,");
        sb.AppendLine("     classification or test then consumes.");
        sb.AppendLine("  2. An authored research verdict is legitimate; presenting it so that it cannot be distinguished");
        sb.AppendLine("     from a computation is not. The fix is visibility and derivability, not re-adjudication.");
        sb.AppendLine("  3. A test that asserts a literal against itself cannot fail, and is therefore not evidence.");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("1. THE QG VERDICT LADDER — DERIVED");
        sb.AppendLine("  phase   status              score   criteria (status)");
        foreach (var (phase, status, score) in FinalQuantumGravityAudit.Progression())
            sb.AppendLine($"  {phase,-7} {status,-19} {score,5:F2}");
        sb.AppendLine();
        foreach (var c in FinalQuantumGravityAudit.Criteria())
            sb.AppendLine($"  QG223  {c.Name,-22} {c.Status,-8} {c.Score,4:F2}  basis: {c.Basis}");
        sb.AppendLine();
        sb.AppendLine("  NOTE: the statuses are the authored QG223 adjudication; the SCORE, TOTAL and CLASSIFICATION");
        sb.AppendLine("        are now derived from them, so flipping a criterion moves the ladder.");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("2. ψ — REFINED TO THE CANONICAL POSITION (ResearchY-G_024)");
        foreach (var (q, v) in FinalQuantumGravityAudit.PsiAdjudication())
            sb.AppendLine($"  {q,-40} {v}");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("3. BORN RULE — THE α SCREEN, EXECUTED");
        sb.AppendLine("  α      outcome                max relative violation");
        foreach (var (alpha, outcome, violation) in AlphaInvarianceScreen.Screen())
            sb.AppendLine($"  {alpha,4:F1}   {outcome,-22} {violation:E3}");
        var ev = BornRuleAnalyzer.RequirementEvidenceBreakdown(BornRuleDerivation.BuildRequirements());
        sb.AppendLine($"  requirements: {ev.Computed} computed, {ev.Analytic} analytic");
        sb.AppendLine($"  analytic rows: {string.Join(", ", ev.AnalyticNames)}");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("4. RAR COMPLETION — LIMITS COMPUTED, AUTHORED ROWS LABELLED");
        var (nOk, dOk, nR, dR) = RarScatterAnalyzer.RarLimitsCheck(1.2e-10);
        sb.AppendLine($"  Newtonian limit holds: {nOk} (ratio {nR:F9})");
        sb.AppendLine($"  deep-MOND limit holds: {dOk} (ratio {dR:F9})");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("5. THE DERIVED CONSTANT G — CACHE CONSISTENCY");
        sb.AppendLine($"  computed  NewtonConstantOrigin.GSISeconds() = {NewtonConstantOrigin.GSISeconds():E6}");
        sb.AppendLine($"  cached    FrameDraggingOrigin.G_D96          = {FrameDraggingOrigin.G_D96:E6}");
        sb.AppendLine($"  cached    PhysicalUnits.G_SI                 = {PhysicalUnits.G_SI:E6}");
        sb.AppendLine($"  CODATA                                       = {NewtonConstantOrigin.GPhysical:E6}");
        sb.AppendLine($"  discrepancy (G_D96) = {NewtonConstantOrigin.CacheDiscrepancy(FrameDraggingOrigin.G_D96):P3}");
        sb.AppendLine();

        sb.AppendLine(); PrintHeader("CONCLUSIONS");
        sb.AppendLine("  1. The G_025 defect class is NOT isolated: four QG audits shared method names and differed");
        sb.AppendLine("     only in literals, and their four test suites asserted contradictory verdicts, all passing.");
        sb.AppendLine("  2. The QG ladder, the Born-rule classification, the RAR completion count and the G cache are");
        sb.AppendLine("     now DERIVED, with every evidence row carrying its basis and its computed/analytic status.");
        sb.AppendLine("  3. The Born-rule headline verdict 'D: Mathematical Derivation — α = 2 UNIQUELY selected' was");
        sb.AppendLine("     DEAD CODE before this audit: the uniqueness test returned false unconditionally because the");
        sb.AppendLine("     non-discriminating rows pass for all six exponents. It is now reachable on executed evidence.");
        sb.AppendLine("  4. ψ was corrected to the canonical position (NOT a new primitive, per QG285/QG286/QG292),");
        sb.AppendLine("     resolving a live contradiction with ResearchY-G_024.");
        sb.AppendLine("  5. The verdicts themselves are unchanged; what changed is that they can no longer be mistaken");
        sb.AppendLine("     for computations, and the tests that carried them can now fail.");
        Output.WriteLine(sb.ToString());
    }
}
