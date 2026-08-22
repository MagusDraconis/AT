using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 193 — Prediction Registry Lock. Creates the immutable registry of the three pre-registered
/// predictions (P1 106 GeV, P2 0νββ m_ββ, P3 sector-ladder spectrum). No future phase may modify a registered
/// prediction — only CONFIRMED / DISFAVORED / FALSIFIED may be added later. Deterministic.
/// </summary>
public class TQMQG_Phase193_PredictionRegistryTests : ResearchTestBase
{
    public TQMQG_Phase193_PredictionRegistryTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1930_ImmutableRegistryContents()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1930: immutable prediction registry (P1, P2, P3)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The registry is the permanent record of the three pre-registered predictions.");
        sb.AppendLine("  - Every entry records: derivation phase, formula, inputs, frozen value, uncertainty,");
        sb.AppendLine("    falsification condition.");
        sb.AppendLine();

        var reg = PredictionRegistry.Registry;
        sb.AppendLine("REGISTRY (Id | Name | Derivation | Frozen value | Falsification):");
        foreach (var p in reg)
            sb.AppendLine($"  {p.Id} | {p.Name} | {p.DerivationPhase} | {p.FrozenValue} | {p.FalsificationCondition}");
        sb.AppendLine();
        sb.AppendLine($"  registry is locked (3 entries, no outcome yet)? {PredictionRegistry.RegistryIsLocked()}");
        sb.AppendLine($"  all frozen values intact (match QG190/191/192)?  {PredictionRegistry.AllValuesIntact()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, reg.Length);
        Assert.Equal(new[] { "P1", "P2", "P3" }, reg.Select(p => p.Id).ToArray());
        Assert.True(PredictionRegistry.RegistryIsLocked(), "registry must start locked with no outcome");
        Assert.True(PredictionRegistry.AllValuesIntact(), "frozen values must match the pre-registration phases");
    }

    [Fact]
    public void TQMQG1931_FormulasAndInputsComplete()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1931: formulas, inputs, uncertainty recorded for every prediction");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each entry must have a formula, inputs, frozen value, uncertainty, falsification.");
        sb.AppendLine();

        var reg = PredictionRegistry.Registry;
        sb.AppendLine("FORMULA / INPUTS / UNCERTAINTY:");
        foreach (var p in reg)
        {
            sb.AppendLine($"  {p.Id} formula: {p.Formula}");
            sb.AppendLine($"       inputs: {p.Inputs}");
            sb.AppendLine($"       uncertainty: {p.Uncertainty}");
        }
        sb.AppendLine();
        sb.AppendLine($"  P1 central mass from registry:      {PredictionRegistry.Get(PredictionRegistry.PredictionId.P1).FrozenValue}");
        sb.AppendLine($"  P2 m_ββ from registry:              {PredictionRegistry.Get(PredictionRegistry.PredictionId.P2).FrozenValue}");
        sb.AppendLine($"  P3 spectrum from registry:          {PredictionRegistry.Get(PredictionRegistry.PredictionId.P3).FrozenValue}");

        Output.WriteLine(sb.ToString());

        Assert.All(reg, p =>
        {
            Assert.False(string.IsNullOrWhiteSpace(p.Formula));
            Assert.False(string.IsNullOrWhiteSpace(p.Inputs));
            Assert.False(string.IsNullOrWhiteSpace(p.FrozenValue));
            Assert.False(string.IsNullOrWhiteSpace(p.Uncertainty));
            Assert.False(string.IsNullOrWhiteSpace(p.FalsificationCondition));
        });
        Assert.Contains("106.39", reg[0].FrozenValue);
        Assert.Contains("2.02", reg[1].FrozenValue);
        Assert.Contains("263.43", reg[2].FrozenValue);
    }

    [Fact]
    public void TQMQG1932_ImmutabilityLockHolds()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1932: immutability lock — only outcomes may be added, never values edited");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A registered prediction may never be modified; only CONFIRMED / DISFAVORED /");
        sb.AppendLine("    FALSIFIED status may be added later.");
        sb.AppendLine();

        // The registry field is `readonly` (cannot be reassigned) and the records are init-only (no setters).
        var reg = PredictionRegistry.Registry;
        double p1MassBefore = 106.39;
        double p2MbbBefore = 2.02;

        // Record an outcome (the ONLY allowed transition) and verify the frozen value is unchanged.
        var updated = PredictionRegistry.RecordOutcome(PredictionRegistry.PredictionId.P1,
            PredictionRegistry.Outcome.Confirmed);
        var afterP1 = PredictionRegistry.Get(PredictionRegistry.PredictionId.P1);

        sb.AppendLine("IMMUTABILITY CHECKS:");
        sb.AppendLine($"  registry field is readonly (no reassignment): {IsRegistryFieldReadonly()}");
        sb.AppendLine($"  records are init-only (no property setter):     {IsRegistryRecordInitOnly()}");
        sb.AppendLine($"  P1 frozen central mass:              {p1MassBefore:F2} GeV (constant)");
        sb.AppendLine($"  P2 frozen m_ββ:                      {p2MbbBefore:F2} meV (constant)");
        sb.AppendLine($"  recorded outcome (P1):               {updated.Status}");
        sb.AppendLine($"  P1 value unchanged after outcome:     {PredictionRegistry.ValuesUnchanged(afterP1)}");
        sb.AppendLine($"  all values still intact:             {PredictionRegistry.AllValuesIntact()}");
        sb.AppendLine($"  classification:                      {PredictionRegistry.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.True(IsRegistryFieldReadonly(), "the registry field must be readonly (cannot be reassigned)");
        Assert.True(IsRegistryRecordInitOnly(), "the registry records must be init-only (no setters)");
        Assert.Equal(PredictionRegistry.Outcome.Confirmed, updated.Status);
        Assert.True(PredictionRegistry.ValuesUnchanged(afterP1),
            "recording an outcome must never change the frozen value");
        Assert.True(PredictionRegistry.AllValuesIntact(), "all frozen values must remain intact");
        Assert.Equal("REGISTRY LOCK", PredictionRegistry.Classify());

        // Recording an outcome on the ORIGINAL registry must not mutate it (records are immutable).
        Assert.Equal(PredictionRegistry.Outcome.None,
            PredictionRegistry.Get(PredictionRegistry.PredictionId.P1).Status);
    }

    private static bool IsRegistryFieldReadonly()
    {
        var f = typeof(PredictionRegistry).GetField("Registry",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        return f != null && f.IsInitOnly;   // readonly field ⇒ IsInitOnly true
    }

    private static bool IsRegistryRecordInitOnly()
    {
        // Init-only setters carry the IsExternalInit modifier on the setter's return parameter.
        var t = typeof(PredictionRegistry).GetNestedType("RegisteredPrediction");
        var isExternalInit = typeof(System.Runtime.CompilerServices.IsExternalInit);
        return t.GetProperties().All(p =>
            p.SetMethod == null   // some may be get-only
            || p.SetMethod.ReturnParameter.GetRequiredCustomModifiers().Contains(isExternalInit));
    }
}
