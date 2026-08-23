using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 255 — Secondary Selection Principle. Resolve the QG254 octave-preserving ties with one
/// target-free, D96-only, deterministic rule. Apply to the QG253 tie cases.
/// </summary>
public class TQMQG_Phase255_SecondarySelectionPrincipleTests : ResearchTestBase
{
    public TQMQG_Phase255_SecondarySelectionPrincipleTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2550_RuleComponents()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2550: the secondary rule components");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The rule operates on the QG254 octave-preserving candidate sets;");
        sb.AppendLine("  - (1) minimal complexity, (2) Noether consistency (no free constant),");
        sb.AppendLine("    (3) moment closure (max full-spectrum moment content).");
        sb.AppendLine();

        sb.AppendLine("FREE-CONSTANT CHECK (Noether consistency):");
        sb.AppendLine($"  5/4·Σ√m/λ₂   → free constant? {SecondarySelectionPrinciple.HasFreeConstant("5/4·Σ√m/λ₂")}");
        sb.AppendLine($"  √3·√Σm        → free constant? {SecondarySelectionPrinciple.HasFreeConstant("√3·√Σm")}");
        sb.AppendLine($"  Σm²/√occMom   → free constant? {SecondarySelectionPrinciple.HasFreeConstant("Σm²/√occMom")}");
        sb.AppendLine($"  √occMom·λ₂    → free constant? {SecondarySelectionPrinciple.HasFreeConstant("√occMom·λ₂")}");
        sb.AppendLine($"  span/√3       → free constant? {SecondarySelectionPrinciple.HasFreeConstant("span/√3")}  (√3 = √#families, D96-native)");
        sb.AppendLine();
        sb.AppendLine("MOMENT-CLOSURE SCORES (full-spectrum usage):");
        sb.AppendLine($"  Σm²/√occMom   → {SecondarySelectionPrinciple.MomentClosureScore("Σm²/√occMom"):F1}");
        sb.AppendLine($"  √occMom·λ₂    → {SecondarySelectionPrinciple.MomentClosureScore("√occMom·λ₂"):F1}");
        sb.AppendLine($"  √#d/λ₂        → {SecondarySelectionPrinciple.MomentClosureScore("√#d/λ₂"):F1}");
        sb.AppendLine($"  √3·√Σm        → {SecondarySelectionPrinciple.MomentClosureScore("√3·√Σm"):F1}");
        sb.AppendLine($"  span/√3       → {SecondarySelectionPrinciple.MomentClosureScore("span/√3"):F1}");

        Output.WriteLine(sb.ToString());

        Assert.True(SecondarySelectionPrinciple.HasFreeConstant("5/4·Σ√m/λ₂"));
        Assert.True(SecondarySelectionPrinciple.HasFreeConstant("√3·√Σm") == false, "√3 is D96-native (√#families)");
        Assert.False(SecondarySelectionPrinciple.HasFreeConstant("Σm²/√occMom"));
        Assert.False(SecondarySelectionPrinciple.HasFreeConstant("√occMom·λ₂"));
        Assert.True(SecondarySelectionPrinciple.MomentClosureScore("√occMom·λ₂") > SecondarySelectionPrinciple.MomentClosureScore("√#d/λ₂"),
            "occMom (2nd moment) beats #d (count)");
    }

    [Fact]
    public void TQMQG2551_ApplyToTieCases()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2551: applying the rule to the QG254 tie cases");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The rule reads only the formula structure (complexity, constants, moment order);");
        sb.AppendLine("  - No observable value or target enters the selection.");
        sb.AppendLine();

        sb.AppendLine("THE QG254 TIE CASES:");
        foreach (var (obs, candidates) in SecondarySelectionPrinciple.TieCases())
        {
            sb.AppendLine($"  {obs}: [{string.Join(" | ", candidates.Select(c => $"{c.Name} (c={c.Complexity})"))}]");
        }
        sb.AppendLine();
        sb.AppendLine("APPLYING THE RULE (min complexity → Noether → moment closure):");
        foreach (var (obs, selected, unique) in SecondarySelectionPrinciple.Apply())
            sb.AppendLine($"  {obs} → {selected}  (unique: {unique})");

        Output.WriteLine(sb.ToString());

        var applied = SecondarySelectionPrinciple.Apply();
        Assert.All(applied, r => Assert.True(r.Unique, $"{r.Observable} must resolve uniquely"));
        Assert.Equal("Σm²/√occMom", applied[0].Selected);
        Assert.Equal("√occMom·λ₂", applied[1].Selected);
        Assert.Equal("span/√3", applied[2].Selected);
    }

    [Fact]
    public void TQMQG2552_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2552: summary — UNIQUE SELECTION PRINCIPLE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Octave preservation (QG254) + moment-closure MDL (this phase) must uniquely select");
        sb.AppendLine("    a formula for every QG253/QG254 tie case, with no target information.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {SecondarySelectionPrinciple.Summary()}");
        sb.AppendLine($"CLASSIFICATION = {SecondarySelectionPrinciple.Classify()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - m_μ/me: Σm²/√occMom (no free constant, occMom 2nd moment) beats 5/4·Σ√m/λ₂ (free 5/4);");
        sb.AppendLine("  - m_τ/m_μ: √occMom·λ₂ (occMom 2nd moment + λ₂ invariant) beats √3·√Σm (Σ√m half-moment)");
        sb.AppendLine("    and √#d/λ₂ (#d is a count, not a moment);");
        sb.AppendLine("  - r₃₁: span/√3 at minimal complexity beats λ₂³·Σ√m (higher operator count);");
        sb.AppendLine("  - all selections are target-free (structure-only) and deterministic.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIQUE SELECTION PRINCIPLE", SecondarySelectionPrinciple.Classify());
        Assert.Contains("UNIQUE SELECTION PRINCIPLE", SecondarySelectionPrinciple.Summary());
        Assert.True(SecondarySelectionPrinciple.AllResolved());
        Assert.True(SecondarySelectionPrinciple.TargetFree());
        Assert.True(SecondarySelectionPrinciple.Deterministic());
    }
}
