using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 315 — Early Lock Prediction. QG312: operators can be faked, locks are robust. Do the
/// lock identities appear BEFORE mature organization, TRACK it, or LAG behind it? Four deterministic
/// evolving systems (software history, wiki history, citation history, language corpora) sharpen a
/// frequency law from flat to the mature characteristic law over 8 growth stages. At each stage both the
/// lock-coherence score and the organization maturity are measured. Deterministic, no observables, no
/// target values.
/// </summary>
public class ATQG_Phase315_EarlyLockPredictionTests : ResearchTestBase
{
    public ATQG_Phase315_EarlyLockPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3150_EvolutionTrajectories()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3150: the lock and maturity trajectories of the four evolving systems");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - each system evolves from a flat spectrum to its mature hierarchical law;");
        sb.AppendLine("  - both the lock-coherence score and the organization maturity grow with stage;");
        sb.AppendLine("  - the lock identity may reach half-strength before the maturity does.");
        sb.AppendLine();

        foreach (var s in EarlyLockPrediction.Systems())
        {
            sb.AppendLine($"  {s.System} [{s.Law}]: lockHalf={s.LockHalfStage} matHalf={s.MaturityHalfStage} " +
                          $"{s.Relation}");
            foreach (var st in s.Stages)
            {
                sb.AppendLine($"    t={st.Stage} α={st.Exponent:F3} span={st.Span:F2} " +
                              $"distinct={st.DistinctValues} maturity={st.Maturity:F3} " +
                              $"lock={st.LockScore:F3} stable={st.StableLocks}");
            }
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, EarlyLockPrediction.Systems().Length);
        Assert.All(EarlyLockPrediction.Systems(), s =>
        {
            Assert.Equal(8, s.Stages.Length);
            Assert.All(s.Stages, st => Assert.InRange(st.Exponent, 0.0, 2.0));
        });
        Assert.All(EarlyLockPrediction.Systems(), s =>
        {
            Assert.InRange(s.LockHalfStage, 1, 8);
            Assert.InRange(s.MaturityHalfStage, 1, 8);
        });
    }

    [Fact]
    public void ATQG3151_LocksReachHalfBeforeMaturity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3151: the lock identity reaches half-strength before the maturity does");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - most evolving systems reach half lock-strength BEFORE half maturity;");
        sb.AppendLine("  - no system has the locks LAG behind the maturity.");
        sb.AppendLine();

        foreach (var s in EarlyLockPrediction.Systems())
        {
            sb.AppendLine($"  {s.System.PadRight(10)}: lockHalf={s.LockHalfStage} matHalf={s.MaturityHalfStage} " +
                          $"{s.Relation}");
        }
        sb.AppendLine();
        sb.AppendLine($"precede={EarlyLockPrediction.PrecedeCount()} track={EarlyLockPrediction.TrackCount()} " +
                      $"lag={EarlyLockPrediction.LagCount()}");
        sb.AppendLine();
        sb.AppendLine("The lock identities [the moment ratios locking onto small fractions] appear as soon");
        sb.AppendLine("as the frequency law is detectable, BEFORE the full hierarchy [large span AND heavy");
        sb.AppendLine("degeneracy — the maturity measure] has developed.");

        Output.WriteLine(sb.ToString());

        Assert.True(EarlyLockPrediction.PrecedeCount() >= 2,
            "most evolving systems must have the locks reach half-strength before the maturity");
        Assert.True(EarlyLockPrediction.LagCount() == 0,
            "no evolving system should have the locks LAG behind the maturity");
    }

    [Fact]
    public void ATQG3152_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3152: the temporal determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - LOCKS PRECEDE: the lock identities are an EARLY signature of organization.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {EarlyLockPrediction.Summary()}");
        sb.AppendLine($"Determination score: {EarlyLockPrediction.DeterminationScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {EarlyLockPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - software, citation, and language histories: the lock identity reaches half-");
        sb.AppendLine("    strength BEFORE the organization maturity [PRECEDE];");
        sb.AppendLine("  - wiki history: the lock identity and the maturity reach half-strength together");
        sb.AppendLine("    [TRACK];");
        sb.AppendLine("  - no system shows the locks LAGGING the maturity;");
        sb.AppendLine("  - the lock identities are an early signature: the moment ratios lock onto small");
        sb.AppendLine("    fractions as soon as the frequency law is detectable, before the full hierarchy");
        sb.AppendLine("    [span × degeneracy] has developed.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("LOCKS PRECEDE", EarlyLockPrediction.Classify());
        Assert.True(EarlyLockPrediction.DeterminationScore() >= 5);
        Assert.Contains("LOCKS PRECEDE", EarlyLockPrediction.Summary());
    }
}
