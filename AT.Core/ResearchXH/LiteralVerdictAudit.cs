using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>How a literal-to-verdict path was adjudicated (ResearchY-G_027).</summary>
public enum PathDisposition
{
    /// <summary>The path is a real computation now — the literal is gone, or it is an honest constant whose
    /// name says so (a documented null result, a unit convention) and the verdict does not overstate it.</summary>
    Safe,

    /// <summary>The path is an AUTHORED judgement or taxonomy with no executable test behind it. Permitted
    /// ONLY with a cited reason, and the verdict must be presented as authored, never as derived.</summary>
    Boundary,

    /// <summary>The path had a computation available and bypassed it. REFUTED and must be FIXED.</summary>
    Refuted,
}

/// <summary>One literal-to-verdict path: a verdict-vocabulary boolean supplied as a literal at a construction
/// site, in a file that also consumes that flag from a score / classification / verdict function.</summary>
/// <param name="File">Repository-relative source path.</param>
/// <param name="Type">The constructed type.</param>
/// <param name="Field">The verdict-vocabulary boolean field.</param>
/// <param name="Value">The literal supplied.</param>
public sealed record LiteralVerdictPath(string File, string Type, string Field, string Value)
{
    /// <summary>The registry key: file, type and field (the line number is not part of the identity).</summary>
    public string Key => $"{File}::{Type}::{Field}";
}

/// <summary>An adjudicated entry in the triage registry (ResearchY-G_027).</summary>
/// <param name="File">Repository-relative source path.</param>
/// <param name="Type">The constructed type.</param>
/// <param name="Field">The verdict-vocabulary boolean field.</param>
/// <param name="Disposition">Safe / Boundary / Refuted.</param>
/// <param name="Reason">Why. Must not be empty — an unbacked disposition is itself a defect.</param>
public sealed record TriageEntry(string File, string Type, string Field, PathDisposition Disposition,
    string Reason)
{
    /// <summary>The registry key, matching <see cref="LiteralVerdictPath.Key"/>.</summary>
    public string Key => $"{File}::{Type}::{Field}";
}

/// <summary>
/// ResearchY-G_027 — THE LITERAL VERDICT AUDIT.
///
/// THE RULE. No result path may let a classification depend on a literal. Every verdict must be traceable to
/// computed evidence. A path of the form
///
///     literal boolean (verdict-vocabulary name)  ->  score / classification / verdict function
///
/// is REFUTED unless it is adjudicated in <see cref="Registry"/> as Safe (the literal is gone, or it is an
/// honest constant) or Boundary (a documented authored judgement with a cited reason).
///
/// WHY A SOURCE SCAN. G_025 and G_026 fixed seven instances of this class by hand. Fixing instances does not
/// prevent the class recurring — and it recurred four times in the QG ladder alone. This class therefore
/// ENFORCES the rule mechanically: it re-reads the source at test time, so a NEW literal-to-verdict path
/// fails the build until it is either computed or explicitly adjudicated.
///
/// WHAT IT DOES NOT DO. It does not decide whether a research verdict is CORRECT. It decides whether a
/// verdict is COMPUTED — and, when it is not, it forces that fact to be recorded in the open rather than
/// implied by a method name.
/// </summary>
public static class LiteralVerdictAudit
{
    /// <summary>Names that make a boolean a VERDICT rather than a parameter: supplying one as a literal means
    /// the verdict was typed in.</summary>
    public static readonly string[] VerdictVocabulary =
    {
        "Holds", "Ok", "Survives", "Derived", "Derivable", "Passes", "Passed", "Valid", "Satisfied",
        "Verified", "Supported", "Consistent", "Broken", "Correct", "Agreed", "Matched", "Matches",
        "Confirmed", "Preserved", "Recovered", "Exact", "Unique", "Adopted", "Solved", "Refuted",
        "Excluded", "Allowed", "Preferred", "Succeeded", "Found", "HasExactTwoPi", "StructurallyUnique",
        "IsTrueInput", "SupportsDerivedFromDifference", "IsValidCharge", "resourceNotFound", "True",
    };

    /// <summary>Names of the functions that CONSUME a verdict — a score, a classification or a verdict.</summary>
    public static readonly string[] ConsumerVocabulary =
    {
        "Score", "Classify", "Classification", "Verdict", "Grade", "Assess", "Rank", "Count(",
    };

    private static readonly Regex RecordRe =
        new(@"\brecord\s+(?:class\s+|struct\s+)?(\w+)\s*(?:<[^>]*>)?\s*\(([^;{]*?)\)\s*", RegexOptions.Singleline);

    private static readonly Regex CtorRe = new(@"\bnew\s+(\w+)\s*\(");

    private static string Clean(string src)
    {
        src = Regex.Replace(src, "@\"(?:[^\"]|\"\")*\"",
            m => "\"\"" + new string(' ', m.Value.Length - 2), RegexOptions.Singleline);
        src = Regex.Replace(src, "\"(?:\\\\.|[^\"\\\\])*\"",
            m => "\"\"" + new string(' ', m.Value.Length - 2));
        src = Regex.Replace(src, "'(?:\\\\.|[^'\\\\])'",
            m => "''" + new string(' ', m.Value.Length - 2));
        src = Regex.Replace(src, @"/\*.*?\*/", m => new string(' ', m.Value.Length), RegexOptions.Singleline);
        src = Regex.Replace(src, @"//[^\n]*", m => new string(' ', m.Value.Length));
        return src;
    }

    private static int LineOf(string src, int idx) => src.Take(idx).Count(c => c == '\n') + 1;

    /// <summary>Find every literal-to-verdict path in one source file.</summary>
    public static List<LiteralVerdictPath> ScanFile(string root, string fullPath)
    {
        var raw = File.ReadAllText(fullPath);
        var src = Clean(raw);
        var rel = Path.GetRelativePath(root, fullPath).Replace('\\', '/');

        var found = new List<LiteralVerdictPath>();

        // Verdict-vocabulary boolean parameters declared by the records in this file, keyed by POSITION as
        // well as by name — the codebase writes both `Holds: true` and plain positional `..., true, ...`.
        var verdictFields = new HashSet<string>(StringComparer.Ordinal);
        var positional = new Dictionary<string, List<(int Index, string Name)>>(StringComparer.Ordinal);
        foreach (Match m in RecordRe.Matches(src))
        {
            var type = m.Groups[1].Value;
            var parts = SplitArgs(m.Groups[2].Value);
            var bools = new List<(int, string)>();
            for (int i = 0; i < parts.Count; i++)
            {
                var pm = Regex.Match(parts[i].Trim(), @"^bool\s+(\w+)");
                if (!pm.Success) continue;
                var name = pm.Groups[1].Value;
                if (!VerdictVocabulary.Contains(name, StringComparer.Ordinal)) continue;
                verdictFields.Add(name);
                bools.Add((i, name));
            }
            positional[type] = bools;
        }

        foreach (Match m in CtorRe.Matches(src))
        {
            var type = m.Groups[1].Value;
            var lp = src.IndexOf('(', m.Index);
            if (lp < 0) continue;
            var (args, _) = CtorArgs(src, lp);
            var argList = SplitArgs(args);

            for (int i = 0; i < argList.Count; i++)
            {
                var a = argList[i].Trim();

                // (a) a NAMED verdict boolean
                var named = Regex.Match(a, @"^(\w+)\s*:\s*(true|false)$");
                if (named.Success)
                {
                    var field = named.Groups[1].Value;
                    if (verdictFields.Contains(field) && VerdictVocabulary.Contains(field, StringComparer.Ordinal))
                        found.Add(new LiteralVerdictPath(rel, type, field, named.Groups[2].Value));
                    continue;
                }

                // (b) a POSITIONAL bare boolean whose declared parameter is a verdict boolean
                var pos = Regex.Match(a, @"^(true|false)$");
                if (pos.Success && positional.TryGetValue(type, out var bools))
                {
                    var hit = bools.FirstOrDefault(b => b.Index == i);
                    if (hit.Name is not null)
                        found.Add(new LiteralVerdictPath(rel, type, hit.Name, pos.Groups[1].Value));
                }
            }
        }

        // Only files that CONSUME a verdict elsewhere are a real path (a flag nobody scores is inert).
        if (!found.Any()) return found;
        var consumes = ConsumerVocabulary.Any(v =>
            src.Contains(v, StringComparison.Ordinal) &&
            found.Any(f => Regex.IsMatch(src, @"\b" + Regex.Escape(f.Field) + @"\b")));
        if (!consumes) return new List<LiteralVerdictPath>();

        return found.GroupBy(f => f.Key).Select(g => g.First()).ToList();
    }

    private static (string, int) CtorArgs(string src, int lparen)
    {
        int depth = 0;
        for (int i = lparen; i < src.Length; i++)
        {
            if (src[i] == '(') depth++;
            else if (src[i] == ')')
            {
                depth--;
                if (depth == 0) return (src[(lparen + 1)..i], i);
            }
        }
        return (src[(lparen + 1)..], src.Length - 1);
    }

    /// <summary>Split a top-level argument list on commas, respecting nesting.</summary>
    public static List<string> SplitArgs(string s)
    {
        var outp = new List<string>();
        int depth = 0;
        var cur = new System.Text.StringBuilder();
        foreach (var c in s)
        {
            if (c is '(' or '[' or '{' or '<') depth++;
            else if (c is ')' or ']' or '}' or '>') depth--;
            if (c == ',' && depth == 0) { outp.Add(cur.ToString()); cur.Clear(); }
            else cur.Append(c);
        }
        if (cur.ToString().Trim().Length > 0) outp.Add(cur.ToString());
        return outp;
    }

    /// <summary>Every <c>.cs</c> file under a source root (repo-relative paths in the result).</summary>
    public static List<string> SourceFiles(string root)
        => Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories)
            .Where(p => !p.Contains(@"\obj\", StringComparison.Ordinal)
                     && !p.Contains(@"\bin\", StringComparison.Ordinal))
            .OrderBy(p => p, StringComparer.Ordinal)
            .ToList();

    /// <summary>Scan a whole source root for literal-to-verdict paths.</summary>
    public static List<LiteralVerdictPath> Scan(string root)
    {
        var all = new List<LiteralVerdictPath>();
        foreach (var f in SourceFiles(root)) all.AddRange(ScanFile(root, f));
        return all.OrderBy(p => p.Key, StringComparer.Ordinal).ToList();
    }

    /// <summary>
    /// THE TRIAGE REGISTRY. Every literal-to-verdict path found by the scan that is not COMPUTED must appear
    /// here with a disposition and a cited reason. An entry absent from this table FAILS the audit; an entry
    /// that no longer matches a path also FAILS (stale triage). Paths are relative to the AT.Core root.
    /// </summary>
    public static IReadOnlyList<TriageEntry> Registry { get; } = new[]
    {
        // ── BOUNDARY: authored judgements and input taxonomies, each with its reason cited. These are NOT
        //    computations; the audit's requirement is that they not be PRESENTED as such.
        new TriageEntry("ResearchXH/SelectionPrincipleAudit.cs", "Rule", "Derivable",
            PathDisposition.Boundary,
            "A META-LEVEL judgement about whether a selection rule is grounded in D96. The ConsistencyNote "
            + "carries the reasoning, and the audit's own summary states that neither rule is FORCED and that "
            + "the rules were selected AFTER QG253 revealed the non-uniqueness. Declaring it derived would "
            + "itself be the defect, so it is presented as authored (BOUNDARY), not as a result."),
        new TriageEntry("ResearchXH/AnchorInventoryAudit.cs", "Anchor", "IsTrueInput",
            PathDisposition.Boundary,
            "An INPUT TAXONOMY: whether a constant is a true theory input. Each row states its reason in "
            + "place (e.g. 'a TRUE THEORY INPUT: the conformal reference metric'), so authorship is "
            + "disclosed. Not computable without a definition of 'input', which is the audit's own subject."),
        new TriageEntry("ResearchXH/PrincipleCompetitionAudit.cs", "Principle", "Consistent",
            PathDisposition.Boundary,
            "An authored consistency flag per principle. The audit records that the Noether row IS "
            + "inconsistent (false) and explains why (the published QG238 l1 uses the 5/4 it excludes). It "
            + "encodes a hand-checked exception list, not a computation."),
        new TriageEntry("ResearchXH/MeasurementClassAudit.cs", "MeasurementClass", "StructurallyUnique",
            PathDisposition.Boundary,
            "An authored claim of structural uniqueness per measurement class ('the only value read'). The "
            + "note states the distinguishing property. Making it computed needs an exhaustive enumeration "
            + "of reads, which the codebase does not have (recorded as an open problem)."),
        new TriageEntry("ResearchXH/ActualizationOriginAudit.cs", "DependencyFact",
            "SupportsDerivedFromDifference", PathDisposition.Boundary,
            "An authored dependency finding per evidence item, each citing its phase (QG292/QG293/QG284/"
            + "QG294/QG295/QG288). The cited phases carry the analysis; the flag records its conclusion."),
        new TriageEntry("ResearchXH/ReorganizationPrediction.cs", "ReorgMember", "Correct",
            PathDisposition.Boundary,
            "An authored per-member prediction-correctness flag. What it compares against (the "
            + "pre-registered values) is recorded in the same audit, so the basis is in place."),
        new TriageEntry("Resonance/Theory/MicroscopicChargeProfile.cs", "FragmentationAttempt",
            "IsValidCharge", PathDisposition.Boundary,
            "An authored validity flag per fragmentation attempt; the accompanying note states the reason in "
            + "place ('Isolated kink detected' / 'Morse maxima ... > condensate count'), so the basis "
            + "travels with the flag."),
    };

    /// <summary>
    /// Paths that WERE literal-to-verdict, are now COMPUTED, and therefore no longer appear in the scan
    /// (ResearchY-G_025/G_026/G_027). Kept as a record so the fix is auditable and so a regression — a
    /// literal reappearing — is caught by the scan and cannot be excused by the registry.
    /// </summary>
    public static IReadOnlyList<(string File, string Type, string Field, string Fix)> FixedByComputation { get; } = new[]
    {
        ("ResearchXH/MetricAnsatzAudit.cs", "volume element", "PerturbedVolumeElement",
            "G_025: the determinant is now COMPUTED (the d-vs-(d-1) off-by-one is gone)."),
        ("ResearchXH/ConformalOpticsResolution.cs", "ppn", "GammaPsiZero/GammaPsiNonZero",
            "G_025: gamma is now COMPUTED from the metric via GammaFromPsiMetric."),
        ("ResearchXH/MetricAnsatzUniqueness.cs", "measure", "PsiPerturbationPreservesMeasure",
            "G_025: now returns the computed false instead of the constructed true."),
        ("ResearchXH/QuantumGravityClosureAudit.cs", "criteria", "IsQuantumMechanicsDerived",
            "G_026: DERIVED from the QgCriterion table (six bare literals removed)."),
        ("ResearchXH/QuantumGravityReclosureAudit.cs", "criteria", "IsQuantumMechanicsDerived",
            "G_026: DERIVED from the QgCriterion table."),
        ("ResearchXH/QuantumGravityReclosureAudit2.cs", "criteria", "IsSpacetimeEmergent",
            "G_026: DERIVED from the QgCriterion table; the typed sub-scores are gone."),
        ("ResearchXH/FinalQuantumGravityAudit.cs", "criteria", "SubScores",
            "G_026: DERIVED from the QgCriterion table; PsiIsNewPrimitive corrected to the canonical false."),
        ("Research/BornRuleDerivation.cs", "AlphaTest", "Survives",
            "G_026: COMPUTED from AlphaInvarianceScreen.IsInvariantUnderUnitaries."),
        ("Research/BornRuleDerivation.cs", "ConsistencyRequirement", "PassesForAlpha",
            "G_026: COMPUTED from the executed screen, or labelled RequirementEvidence.Analytic."),
        ("ResearchDATA/RarScatterAnalyzer.cs", "CompletionScore", "Derived",
            "G_026: the three limit rows are evaluated from the AT functional form; the rest are labelled "
            + "CompletionEvidence.Authored."),
        ("ResearchQG/GdaggerOriginAnalyzer.cs", "CouplingMechanism", "HasExactTwoPi / Matches / RatioToA0 / Score",
            "G_027: all four are DERIVED from the predicted g† (the Coincidence row's RatioToA0 typed 1.0 "
            + "with a NaN numerator is gone)."),
        ("ResearchXH/EffectiveSizeLaw.cs", "grid", "IdentityHoldsAcrossGrid",
            "G_026: the declared feedback/damping are now threaded through."),
        ("ResearchXH/NewtonConstantOrigin.cs", "cache", "cached G literals",
            "G_026: CacheAgreesWithComputation checks the caches against the computation."),
    };

    /// <summary>Paths found in the source that are NOT adjudicated in the registry — each one FAILS the audit.</summary>
    public static List<LiteralVerdictPath> UnTriaged(string root)
    {
        var known = Registry.Select(r => r.Key).ToHashSet(StringComparer.Ordinal);
        return Scan(root).Where(p => !known.Contains(p.Key)).ToList();
    }

    /// <summary>Registry entries no longer present in the source — stale triage, which also FAILS the audit.</summary>
    public static List<TriageEntry> StaleEntries(string root)
    {
        var present = Scan(root).Select(p => p.Key).ToHashSet(StringComparer.Ordinal);
        return Registry.Where(r => !present.Contains(r.Key)).ToList();
    }

    /// <summary>Refuted dispositions. This list MUST be empty: a refuted path is fixed, not tolerated.</summary>
    public static IReadOnlyList<TriageEntry> RefutedEntries
        => Registry.Where(r => r.Disposition == PathDisposition.Refuted).ToList();

    /// <summary>Registry entries that fail to cite a reason — an unbacked disposition.</summary>
    public static IReadOnlyList<TriageEntry> UnbackedEntries
        => Registry.Where(r => string.IsNullOrWhiteSpace(r.Reason) || r.Reason.Length < 40).ToList();

    /// <summary>The inventory, by disposition, for reporting.</summary>
    public static (PathDisposition Disposition, int Count)[] Inventory()
        => Registry.GroupBy(r => r.Disposition)
            .Select(g => (g.Key, g.Count()))
            .OrderBy(t => t.Key)
            .ToArray();
}
