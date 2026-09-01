using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 254 — Formula Selection Principle. Derive a D96-only, target-free, deterministic rule
/// that selects a formula BEFORE any comparison.
/// </summary>
public class ATQG_Phase254_FormulaSelectionPrincipleTests : ResearchTestBase
{
    public ATQG_Phase254_FormulaSelectionPrincipleTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2540_OctavePreservationPredicate()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2540: the octave-preservation predicate");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A formula isolates a single octave band occ₀/occ₁/occ₃ iff it uses the band alone,");
        sb.AppendLine("    not as a two-band octave ratio occᵢ/occⱼ and not via the full aggregate occMom;");
        sb.AppendLine("  - Octave ratios, occMom, and the spectral aggregates are octave-preserving.");
        sb.AppendLine();

        sb.AppendLine("PUBLISHED FORMULAS (must be octave-preserving):");
        sb.AppendLine($"  Σm²/√occMom        → {FormulaSelectionPrinciple.IsOctavePreserving("Σm²/√occMom")}  (m_μ/me)");
        sb.AppendLine($"  √occMom·λ₂         → {FormulaSelectionPrinciple.IsOctavePreserving("√occMom·λ₂")}  (m_τ/m_μ)");
        sb.AppendLine($"  ln(span)/(Σm−#d)   → {FormulaSelectionPrinciple.IsOctavePreserving("ln(span)/(Σm−#d)")}  (1−n_s)");
        sb.AppendLine($"  (Σm−#d)·occ₁/occ₃  → {FormulaSelectionPrinciple.IsOctavePreserving("(Σm−#d)·occ₁/occ₃")}  (r₂₁ — octave ratio)");
        sb.AppendLine($"  span/√3            → {FormulaSelectionPrinciple.IsOctavePreserving("span/√3")}  (r₃₁)");
        sb.AppendLine($"  2Σm/(Σ√m·√(span·#g)) → {FormulaSelectionPrinciple.IsOctavePreserving("2Σm/(Σ√m·√(span·#g))")}  (m₂/m₃)");
        sb.AppendLine();
        sb.AppendLine("NON-NATIVE QG253 ALTERNATIVES (must violate octave preservation):");
        sb.AppendLine($"  √Σm/occ₀           → {FormulaSelectionPrinciple.IsOctavePreserving("√Σm/occ₀")}  (r₂₁ alternative)");
        sb.AppendLine($"  1/(span·ln occ₃)   → {FormulaSelectionPrinciple.IsOctavePreserving("1/(span·ln occ₃)")}  (1−n_s alternative)");
        sb.AppendLine($"  1/(occ₀√2)         → {FormulaSelectionPrinciple.IsOctavePreserving("1/(occ₀√2)")}  (m₂/m₃ alternative)");
        sb.AppendLine($"  occ₀²/λ₂           → {FormulaSelectionPrinciple.IsOctavePreserving("occ₀²/λ₂")}  (y_t/y_b alternative)");
        sb.AppendLine($"  #g²/√occ₃          → {FormulaSelectionPrinciple.IsOctavePreserving("#g²/√occ₃")}  (m_μ/me alternative)");

        Output.WriteLine(sb.ToString());

        Assert.True(FormulaSelectionPrinciple.PublishedFormulasOctavePreserving(), "all published formulas must be octave-preserving");
        Assert.True(FormulaSelectionPrinciple.NonNativeAlternativesExcluded(), "all five non-native alternatives must be excluded");
    }

    [Fact]
    public void ATQG2541_PoolFiltering()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2541: applying the rule to the QG253 candidate pool");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The rule is applied to the SAME candidate pool as QG253 (no targets consulted);");
        sb.AppendLine("  - Octave preservation is the stated D96 symmetry projection (Noether-consistent).");
        sb.AppendLine();

        var pool = FormulaSelectionPrinciple.OctavePreservingPool();
        sb.AppendLine($"QG253 pool size:            {FormulaUniquenessAudit.Pool.Length}");
        sb.AppendLine($"Octave-preserving subset:   {pool.Length}");
        sb.AppendLine($"Excluded alternatives:      {FormulaSelectionPrinciple.ExcludedAlternatives()}");
        sb.AppendLine($"Surviving octave ties:      {FormulaSelectionPrinciple.SurvivingTies()}");
        sb.AppendLine($"Target-free?               {FormulaSelectionPrinciple.TargetFree()}");
        sb.AppendLine($"Deterministic?             {FormulaSelectionPrinciple.Deterministic()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FormulaSelectionPrinciple.TargetFree(), "the rule must not consult any target value");
        Assert.True(FormulaSelectionPrinciple.Deterministic());
        Assert.True(pool.Length > 0 && pool.Length < FormulaUniquenessAudit.Pool.Length, "the filter must reduce the pool");
        Assert.Equal(5, FormulaSelectionPrinciple.ExcludedAlternatives());
        Assert.Equal(3, FormulaSelectionPrinciple.SurvivingTies());
    }

    [Fact]
    public void ATQG2542_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2542: summary — SELECTION PRINCIPLE");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The rule is a derivation-choice rule: a stated D96 symmetry (octave preservation),");
        sb.AppendLine("    applied before comparison, with no target values and no observables.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FormulaSelectionPrinciple.Summary()}");
        sb.AppendLine($"CLASSIFICATION = {FormulaSelectionPrinciple.Classify()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The octave-preservation rule removes 5 of the 8 QG253 minimal-complexity");
        sb.AppendLine("    alternatives — all the ones that isolate a single octave band (non-native);");
        sb.AppendLine("  - the published formulas all satisfy it (they use occMom or octave ratios, never");
        sb.AppendLine("    an isolated band);");
        sb.AppendLine("  - the residual 3 ties (√3·√Σm, λ₂³·Σ√m, 5/4·Σ√m/λ₂) are themselves octave-preserving,");
        sb.AppendLine("    so the principle narrows to the octave-preserving class (a strong prior) but does");
        sb.AppendLine("    not uniquely fix every formula without additional symmetry selection.");
        sb.AppendLine("  - This is the derivation-choice rule QG253 asked for: it selects before comparison.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SELECTION PRINCIPLE", FormulaSelectionPrinciple.Classify());
        Assert.Contains("SELECTION PRINCIPLE", FormulaSelectionPrinciple.Summary());
    }
}
