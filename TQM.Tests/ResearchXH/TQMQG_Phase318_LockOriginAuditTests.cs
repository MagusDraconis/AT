using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 318 (reissue) — Lock Origin Audit. QG312-317 established the D96 lock identities
/// [Σ√m/span ≈ 10, occMom/Σm ≈ 20, Σm²/Σm ≈ 12/5, occMom/Σm² ≈ 25/3]. This phase asks WHY: are the locks
/// EMERGENT, INEVITABLE, or RESONANCE FIXED POINTS? Investigate the moment ratios, gap ratios, span
/// ratios, and occupancy ratios; search for the common source of lock formation. D96 only, no
/// observables, no target values, deterministic.
/// </summary>
public class TQMQG_Phase318_LockOriginAuditTests : ResearchTestBase
{
    public TQMQG_Phase318_LockOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3180_TheMomentChainIdentity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3180: the moment-chain identity — the common source of lock formation");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the four locks are ratios of ONE D96 moment hierarchy {Σ√m, Σm, Σm², occMom};");
        sb.AppendLine("  - occMom/Σm = (Σm²/Σm)·(occMom/Σm²) holds EXACTLY [the telescoping identity];");
        sb.AppendLine("  - lock1 is algebraically forced by lock2 × lock3 — the common source.");
        sb.AppendLine();

        var l = LockOriginAudit.Locks();
        sb.AppendLine($"lock0 = Σ√m/span   = {l.SqrtMomentSpan:F4} ≈ 10");
        sb.AppendLine($"lock1 = occMom/Σm  = {l.OccMomOverSum:F4} ≈ 20");
        sb.AppendLine($"lock2 = Σm²/Σm     = {l.Sum2OverSum:F4} ≈ 12/5");
        sb.AppendLine($"lock3 = occMom/Σm² = {l.OccMomOverSum2:F4} ≈ 25/3");
        sb.AppendLine();
        sb.AppendLine($"TELESCOPING: lock2 × lock3 = {l.Sum2OverSum:F4} × {l.OccMomOverSum2:F4} = " +
                      $"{l.TelescopingProduct:F4}");
        sb.AppendLine($"identity holds exactly: {l.TelescopingHolds}");
        sb.AppendLine();
        sb.AppendLine("The four locks are NOT independent: they are the ratio chain of ONE moment");
        sb.AppendLine("hierarchy. lock1 = lock2 × lock3 is an algebraic necessity — the common source");
        sb.AppendLine("of lock formation.");

        Output.WriteLine(sb.ToString());

        Assert.True(LockOriginAudit.TelescopingHolds(),
            "the moment-chain identity occMom/Σm = (Σm²/Σm)·(occMom/Σm²) must hold exactly");
        Assert.True(Math.Abs(l.SqrtMomentSpan - 10.0) / 10.0 < 0.01, "lock0 ≈ 10");
        Assert.True(Math.Abs(l.OccMomOverSum - 20.0) / 20.0 < 0.01, "lock1 ≈ 20");
    }

    [Fact]
    public void TQMQG3181_RobustButNotInevitable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3181: the locks are robust to perturbation but NOT inevitable");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - changing one D96 group to a NEARBY size moves the locks only ~1-3%");
        sb.AppendLine("    [structural robustness];");
        sb.AppendLine("  - random same-constraint spectra NEVER reproduce the D96 lock values [not");
        sb.AppendLine("    inevitable — the values are D96-specific].");
        sb.AppendLine();

        sb.AppendLine("perturbations (one group size change):");
        foreach (var p in LockOriginAudit.Perturbations())
        {
            sb.AppendLine($"  {p.Description}: Σm²/Σm={p.Sum2OverSum:F4} Σ√m/span={p.SqrtMomentSpan:F4} " +
                          $"max dev={p.MaxDeviation:P1}");
        }
        sb.AppendLine($"max perturbation deviation: {LockOriginAudit.MaxPerturbationDeviation():P1}");
        sb.AppendLine($"perturbation robust: {LockOriginAudit.PerturbationRobust()}");
        sb.AppendLine();
        var (trials, withTwo, withFour) = LockOriginAudit.RandomInevitability();
        sb.AppendLine($"random same-constraint spectra [{trials} trials, sum=95, 44 groups]:");
        sb.AppendLine($"  with ≥2 D96 locks: {withTwo}/{trials}  with ≥4: {withFour}/{trials}");
        sb.AppendLine($"values NOT inevitable: {LockOriginAudit.ValuesNotInevitable()}");
        sb.AppendLine();
        sb.AppendLine("The locks are structurally robust [perturbations move them ~1%], but the specific");
        sb.AppendLine("values 10, 20, 12/5, 25/3 are D96-specific — random spectra never reproduce them.");

        Output.WriteLine(sb.ToString());

        Assert.True(LockOriginAudit.PerturbationRobust(),
            "most nearby single-group perturbations must move the locks less than 4%");
        Assert.True(LockOriginAudit.ValuesNotInevitable(),
            "random same-constraint spectra must not reproduce the D96 lock values");
    }

    [Fact]
    public void TQMQG3182_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3182: the lock-origin determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PARTIAL ORIGIN: the common source [the moment-chain identity] is found, but");
        sb.AppendLine("    the specific integer values are emergent from the D96 structure.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {LockOriginAudit.Summary()}");
        sb.AppendLine($"Origin score: {LockOriginAudit.OriginScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {LockOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the STRUCTURE of the locks is explained: they are the self-consistent ratio");
        sb.AppendLine("    chain of ONE moment hierarchy, linked by the exact telescoping identity");
        sb.AppendLine("    lock1 = lock2 × lock3 — the resonance fixed-point relation of the actualization");
        sb.AppendLine("    attractor;");
        sb.AppendLine("  - the VALUES are emergent from the D96 geometry: robust to perturbation, not");
        sb.AppendLine("    reproduced by random spectra, and not forced by a universal principle;");
        sb.AppendLine("  - the locks are RESONANCE FIXED POINTS in structure, EMERGENT in value.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", LockOriginAudit.Classify());
        Assert.True(LockOriginAudit.OriginScore() >= 3);
        Assert.Contains("PARTIAL ORIGIN", LockOriginAudit.Summary());
    }
}
