namespace AT.Core.Research;

/// <summary>
/// Data types for X060e U(1) Irreducibility.
/// </summary>
public static class U1IrreducibilityMetrics
{
    public enum U1Status { Independent, WeaklyPreferred, StronglyPreferred, FullyDerived }

    public sealed record U1Argument(
        string Name, string Logic,
        bool ProvesU1Inevitable, string Gap, bool Survives);

    public sealed record U1FreeEcology(
        string Name, string Structure,
        double Fitness, bool IsViable,
        string WhyFails);

    public sealed record U1Report(
        List<U1Argument> Arguments,
        List<U1FreeEcology> Counterexamples,
        int SurvivingArgs, int ViableCounterexamples,
        U1Status Status, string Verdict);
}
