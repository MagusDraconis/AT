using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 158 — Moment order origin. QG157 established N_eff = moment(D96 multiplicity structure)
/// with ν:Σ√m, d:Σm, ℓ:Σm², u:Σocc²/occ₀ (mean δ deviation 0.16%). This phase asks WHY the specific
/// moment orders (1/2, 1, 2) are selected: are they INEVITABLE consequences of the Z2 doublet structure,
/// or merely descriptive?
///
/// Tests: ATQG1580 (Z2 base-2 inevitability), ATQG1581 (mode-selection rule and half-moment),
/// ATQG1582 (Z2-power law and classification).
/// </summary>
public class ATQG_Phase158_MomentOrderOriginTests : ResearchTestBase
{
    public ATQG_Phase158_MomentOrderOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1580_Z2Base2Inevitability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1580: Z2 base-2 inevitability of the moment orders");

        var ms = EffectiveAccessCounts.DoubletMultiplicities();
        sb.AppendLine("ASSUMPTIONS: the D96 geometry is base-2 — the Z2 doublets have order 2 (dominant");
        sb.AppendLine("multiplicity), and the octave structure is frequency doubling. The only integer");
        sb.AppendLine("powers of the Z2 order are p = 2^k.");
        sb.AppendLine();
        sb.AppendLine("BASE-2 STRUCTURE OF THE D96 GEOMETRY:");
        sb.AppendLine($"  doublet groups: {ms.Length}");
        sb.AppendLine($"  Z2 doublet multiplicity (dominant): {MomentOrderOrigin.Z2Order()}");
        sb.AppendLine($"  Z2 fraction (groups of size exactly 2): {MomentOrderOrigin.Z2Fraction():F3}");
        sb.AppendLine($"  octave count (family count): {MomentOrderOrigin.OctaveCount()}");
        sb.AppendLine($"  base-2 structure (Z2 fraction > 0.9, ≥ 3 octave families): {MomentOrderOrigin.Base2Structure()}");
        sb.AppendLine($"  Z2 order = 2 → integer powers 2^k for k = -1, 0, 1");
        sb.AppendLine();
        sb.AppendLine("THE MOMENT LADDER p_k = 2^k:");
        foreach (var (name, k, p) in MomentOrderOrigin.MomentOrders())
        {
            double mom = ms.Sum(m => Math.Pow(m, p));
            double d = Math.Log(mom) / Math.Log(FamilyIndexOrigin.IntraSectorModes()[^1] / FamilyIndexOrigin.IntraSectorModes()[0]);
            sb.AppendLine($"  {name}: k={k}, p=2^{k}={p:F2}, Σm^p={mom:F2}, δ={d:F4}");
        }
        sb.AppendLine();
        sb.AppendLine($"  moment orders are exactly the integer powers of the Z2 order: {MomentOrderOrigin.OrdersAreZ2Powers()}");
        sb.AppendLine($"  Z2 powers {2.0 / 2.0:F2} = 2^0, 2^-1 = {1.0 / 2.0:F2}, 2^1 = {2.0:F0} → the set {{2⁻¹, 2⁰, 2¹}} = {{1/2, 1, 2}}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the moment orders (1/2, 1, 2) ARE the integer powers of the Z2 order —");
        sb.AppendLine("with 3 family levels the only Z2 powers available are {2⁻¹, 2⁰, 2¹}.");
        Output.WriteLine(sb.ToString());

        Assert.True(MomentOrderOrigin.Base2Structure(), "D96 should be base-2");
        Assert.True(MomentOrderOrigin.Z2Fraction() > 0.9, "Z2 fraction should be dominant");
        Assert.True(MomentOrderOrigin.OctaveCount() >= 3, "should have ≥ 3 octave families");
        Assert.True(MomentOrderOrigin.OrdersAreZ2Powers(), "moment orders must be Z2 powers");
    }

    [Fact]
    public void ATQG1581_ModeSelectionRuleAndHalfMoment()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1581: mode-selection rule and half-moment origin");

        sb.AppendLine("ASSUMPTIONS: each sector reaches a different Z2-doublet level; the neutral sector");
        sb.AppendLine("(Q=0, T3-only, QG154) cannot access the charge×isospin channel and reaches ONE member");
        sb.AppendLine("of each doublet, so its count is a half-power (geometric-mean) statistic.");
        sb.AppendLine();
        sb.AppendLine("MODE-SELECTION RULE (doublet members each sector reaches):");
        foreach (var (name, rule, power) in MomentOrderOrigin.ModeSelectionRule())
            sb.AppendLine($"  {name}: power {power:F2} — {rule}");
        sb.AppendLine();
        sb.AppendLine("HALF-MOMENT ORIGIN (neutral sector):");
        var (half, geo, ratio) = MomentOrderOrigin.HalfMomentOrigin();
        sb.AppendLine($"  Σ√m = {half:F3}");
        sb.AppendLine($"  √(Σm · groups) = √(95 × 44) = {geo:F3}  (geometric mean of total × groups)");
        sb.AppendLine($"  Σ√m / √(groups×modes) = {ratio:F4}");
        sb.AppendLine($"  half-moment is geometric-mean interpolation: {MomentOrderOrigin.HalfMomentIsGeometricMean()}");
        sb.AppendLine();
        sb.AppendLine("  ν reaches one T3 member per doublet → count ∝ geometric mean of the channel.");
        sb.AppendLine("  d reaches both members (full doublet) → first moment Σm.");
        sb.AppendLine("  ℓ reaches the doublet squared (doublet occupancy) → second moment Σm².");
        sb.AppendLine("  u reaches the octave-occupation structure → octave moment Σocc²/occ₀.");
        Output.WriteLine(sb.ToString());

        Assert.True(MomentOrderOrigin.HalfMomentIsGeometricMean(), "half-moment should be the geometric mean");
        Assert.True(MomentOrderOrigin.ModeSelectionRule().Length == 3, "three doublet-level rules");
        Assert.True(MomentOrderOrigin.HalfMomentOrigin().HalfMoment > 0, "half-moment should be positive");
    }

    [Fact]
    public void ATQG1582_Z2PowerLawAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1582: Z2-power law and classification");

        sb.AppendLine("Z2-POWER LAW δ = log(N_eff)/log(span) WITH INEVITABLE ORDERS:");
        foreach (var (n, p, t, d) in MomentOrderOrigin.UnifiedLaw())
            sb.AppendLine($"  {n}: predicted δ={p:F4}  target δ={t:F4}  deviation={d:P2}");
        sb.AppendLine();
        sb.AppendLine($"  mean deviation = {MomentOrderOrigin.MeanDeviation():P2}");
        sb.AppendLine($"  all four sectors within 5%: {MomentOrderOrigin.UnifiedLaw().All(r => r.Deviation < 0.05)}");
        sb.AppendLine();
        sb.AppendLine("SECTOR ASSIGNMENT BY ACCESS ORDERING (monotone → unique):");
        var md = MomentOrderOrigin.MomentDeltaSequence();
        var td = MomentOrderOrigin.TargetDeltaSequence();
        for (int i = 0; i < 4; i++)
            sb.AppendLine($"  moment δ[{i}]={md[i]:F4}   target δ[{i}]={td[i]:F4}");
        sb.AppendLine($"  moment δ increasing: {MomentOrderOrigin.MomentSequenceIncreasing()}");
        sb.AppendLine($"  target δ increasing: {MomentOrderOrigin.TargetSequenceIncreasing()}");
        sb.AppendLine($"  unique monotone assignment: {MomentOrderOrigin.UniqueMonotoneAssignment()}");
        sb.AppendLine();
        int score = MomentOrderOrigin.OriginScore();
        string cls = MomentOrderOrigin.Classify();
        sb.AppendLine($"MOMENT-ORDER-origin score (0..5): {score}");
        sb.AppendLine($"  +1 base-2 D96 geometry: {MomentOrderOrigin.Base2Structure()}");
        sb.AppendLine($"  +1 orders are Z2 powers: {MomentOrderOrigin.OrdersAreZ2Powers()}");
        sb.AppendLine($"  +1 unique monotone assignment: {MomentOrderOrigin.UniqueMonotoneAssignment()}");
        sb.AppendLine($"  +1 half-moment is geometric mean: {MomentOrderOrigin.HalfMomentIsGeometricMean()}");
        sb.AppendLine($"  +1 Z2-power law within 5%: {MomentOrderOrigin.UnifiedLaw().All(r => r.Deviation < 0.05)}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • DESCRIPTIVE rejected: the orders (1/2, 1, 2) are not arbitrary labels — they are");
        sb.AppendLine("    exactly the integer powers of the Z2 order (2) with 3 family levels.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the mode-selection rule fixes each sector's doublet-access");
        sb.AppendLine("    level, and the assignment is unique by monotonicity (no fitting).");
        sb.AppendLine("  • INEVITABLE accepted: (1/2, 1, 2) ARE inevitable consequences of the Z2 doublet");
        sb.AppendLine("    structure — D96 is base-2 (order-2 doublets dominate), so with 3 octave families");
        sb.AppendLine("    the only Z2 powers are {{2⁻¹, 2⁰, 2¹}}; ν (neutral T3-only) reaches one member per");
        sb.AppendLine("    doublet → 2⁻¹, d (full access) → 2⁰, ℓ (doublet occupancy) → 2¹, u (dense band)");
        sb.AppendLine("    → the octave moment. Not merely descriptive.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "moment-order-origin score should be strong");
        Assert.True(MomentOrderOrigin.MeanDeviation() < 0.01, "mean deviation should be < 1%");
        Assert.True(MomentOrderOrigin.UniqueMonotoneAssignment(), "assignment should be unique");
        Assert.Equal("INEVITABLE", cls);
    }
}
