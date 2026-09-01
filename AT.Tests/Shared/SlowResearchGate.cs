namespace AT.Tests.Shared;

/// <summary>
/// Skip-by-default gate for long-running legacy research simulations
/// (e.g. AT-011, AT-041, AT-046, AT-047 heavy Kuramoto sweeps that take
/// many minutes each). Skipped unless AT_RUN_SLOW=1 is set.
/// </summary>
public static class SlowResearchGate
{
    public const string EnvVar = "AT_RUN_SLOW";

    /// <summary>Skips the test unless AT_RUN_SLOW=1 is set.</summary>
    public static void SkipUnlessSlowRequested(string reason)
    {
        Assert.SkipUnless(
            Environment.GetEnvironmentVariable(EnvVar) == "1",
            $"{reason} (set {EnvVar}=1 to run).");
    }
}
