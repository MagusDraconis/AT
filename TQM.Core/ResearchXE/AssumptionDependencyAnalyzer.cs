namespace TQM.Core.ResearchXE;

using System.Globalization;

/// <summary>
/// Assumption-dependency structural audit of TQM.
/// ResearchXE-002: Assumption Dependency Audit
/// </summary>
public static class AssumptionDependencyAnalyzer
{
    public enum ImpactClass { Minimal, Moderate, Critical, Catastrophic }

    public sealed record Assumption(
        string Id, string Name, int Tier,
        string Description, bool IsExplicit,
        string FragilityNote);

    public sealed record ResultNode(
        string Id, string Name, string Experiment,
        string[] RequiredAssumptions, int Tier,
        string FragilityFromXE001);

    public sealed record DependencyEdge(
        string AssumptionId, string ResultId,
        ImpactClass Impact, string Notes);

    public static List<Assumption> BuildInventory()
    {
        return new List<Assumption>
        {
            new("A1", "Q exists (individuation)", 0,
                "Distinguishable entities exist. Q is the principle of individuation.",
                true, "IRREDUCIBLE. 10 reduction attempts, 0 successes (X035)."),

            new("A2", "Randomness is ontological", 0,
                "Outcome selection is genuinely random, not deterministic chaos.",
                true, "IRREDUCIBLE. Chaos cannot reproduce quantum correlations (Bell)."),

            new("A3", "Complexity maximization principle", 1,
                "Physical structures realize the configuration maximizing distinguishable states.",
                true, "HEURISTIC. 'Complexity' definition has hand-crafted weights (X060g)."),

            new("A4", "Born rule (P_i = |ψ_i|²)", 1,
                "Probability rule from Hilbert geometry. Derived in X037 from unitary invariance.",
                true, "RIGOROUS. Unitary invariance ⇔ α=2. Works in all dimensions."),

            new("A5", "3+1 spacetime dimensions", 1,
                "Derived from complexity maximization (X042). Required by all subsequent tiers.",
                true, "STRONG. d=3 uniquely maximizes complexity. Physics confirmation (Bertrand, GR d.o.f., knots)."),

            new("A6", "Causal set interpretation of Q-events", 1,
                "Q-events and their partial order form a causal set. External BDG action → GR.",
                true, "EXTERNAL DEPENDENCY. Causal set → GR is the largest external gap (X060g)."),

            new("A7", "Particles = topological defects", 2,
                "Stable localized structures in Q-event correlation field are particles.",
                true, "STRONG. Defects are solutions of the effective PDE. Topologically protected."),

            new("A8", "Gauge symmetry = Aut(moduli space)", 2,
                "Gauge groups are automorphisms of defect moduli spaces. U(1) = Aut(S¹).",
                true, "RIGOROUS. U(1) = Aut(S¹) is a theorem (X050, X060e)."),

            new("A9", "Stability cutoff α ≈ 1.5", 2,
                "Excitation level stability decays as exp(-α·n). α ≈ 1.5 gives 3 observable generations.",
                true, "FRAGILE (XE001). Small α changes → different generation count."),

            new("A10", "Anharmonicity from defect potential", 2,
                "Mass hierarchy m_n = m_0·exp(n·π·a) from WKB quantization of φ⁴ potential.",
                true, "STRONG. Pattern is robust. Exact a₀,γ values not derived."),

            new("A11", "Defect wavefunction overlap → mixing", 2,
                "|V_ij| ∝ exp(-β·|i-j|) from overlap of defect excitation wavefunctions.",
                true, "STRONG. Pattern correct. Exact β values depend on localization length."),

            new("A12", "Multiplicative actualization cascades", 3,
                "Each actualization multiplies abundance by exp(ε). CLT → log-normal.",
                true, "HIGHLY ROBUST (XE001). CLT guarantee — only fails with very few steps."),

            new("A13", "Born volatility identity σ₀² = Var[-log(p)]", 3,
                "Per-step volatility from Born rule variance. σ₀² ≈ 0.09 for p ≈ 1/2.",
                true, "ROBUST (XE001). Wide range of p produces σ₀² in plausible range."),

            new("A14", "Freezeout criterion Γ(T_f) = H(T_f)", 3,
                "Abundance variable freezes when its actualization rate < Hubble rate.",
                true, "STRONG. Universal criterion. T_f depends on the physics of each variable."),

            new("A15", "Λ(t) = α/√V(t) (Poisson fluctuations)", 4,
                "Λ from Q-event count fluctuations in causal diamonds. Produces w(z) ≠ -1.",
                true, "EXPOSED. Most falsifiable prediction. Coefficient uncomputed."),

            new("A16", "M² = ⟨k⟩_interact (causal degree)", 4,
                "The nonlinearity parameter is the average causal degree in 3+1D.",
                true, "ROBUST qualitatively (XE001). Exact value fragile — depends on degree definition."),

            new("A17", "Defect DM identity (neutral vortices)", 4,
                "Dark matter = stable neutral topological defects. No U(1) charge. Mass ~TeV.",
                true, "UNFALSIFIABLE in practice. Null results consistent but never confirm."),

            new("A18", "Neutrino = delocalized defect (no U(1))", 2,
                "Neutrinos are neutral → no gauge localization → tiny masses + large PMNS.",
                true, "STRONG. One mechanism → tiny mass + large mixing + Majorana possible."),
        };
    }

    public static List<ResultNode> BuildResults()
    {
        return new List<ResultNode>
        {
            new("R1", "Quantum mechanics (Hilbert, Schrödinger, Born)", "X036-X037",
                new[] { "A1", "A2", "A3", "A4" }, 1, "ROBUST"),

            new("R2", "Time from actualization order", "X040",
                new[] { "A1", "A2" }, 1, "ROBUST"),

            new("R3", "3+1 spacetime dimensions", "X042",
                new[] { "A1", "A2", "A3" }, 1, "ROBUST"),

            new("R4", "General Relativity (causal set → GR)", "X041",
                new[] { "A1", "A2", "A5", "A6" }, 1, "EXTERNAL GAP"),

            new("R5", "Particle existence (topological defects)", "X047",
                new[] { "A1", "A2", "A5", "A7" }, 2, "ROBUST"),

            new("R6", "Gauge symmetry structure", "X048-X050",
                new[] { "A1", "A5", "A7", "A8" }, 2, "ROBUST"),

            new("R7", "U(1) existence (theorem)", "X050-X060e",
                new[] { "A1", "A5", "A7", "A8" }, 2, "RIGOROUS"),

            new("R8", "Three generations", "X051",
                new[] { "A1", "A2", "A5", "A7", "A9" }, 2, "FRAGILE (XE001)"),

            new("R9", "Mass hierarchy pattern", "X052-X053",
                new[] { "A1", "A5", "A7", "A10" }, 2, "ROBUST"),

            new("R10", "Mixing structure (CKM/PMNS)", "X054",
                new[] { "A1", "A5", "A7", "A11" }, 2, "ROBUST"),

            new("R11", "Log-normal abundance law", "XB002",
                new[] { "A1", "A2", "A12" }, 3, "HIGHLY ROBUST"),

            new("R12", "Abundance volatility σ² = N·σ₀²", "XB003-XB004",
                new[] { "A1", "A2", "A12", "A13" }, 3, "ROBUST"),

            new("R13", "Abundance mean μ = log(N_f/N_i)", "XB005",
                new[] { "A1", "A2", "A12" }, 3, "ROBUST"),

            new("R14", "Freezeout epochs", "XB007",
                new[] { "A1", "A2", "A5", "A14" }, 3, "STRONG"),

            new("R15", "Λ(t) = α/√V → w(z) ≠ -1", "X046-X062",
                new[] { "A1", "A2", "A5", "A6", "A15" }, 4, "EXPOSED"),

            new("R16", "M² = ⟨k⟩ ≈ 5", "XC002-XC005",
                new[] { "A1", "A5", "A6", "A16" }, 4, "FRAGILE numerically"),

            new("R17", "DM = neutral defects", "X064",
                new[] { "A1", "A5", "A7", "A17" }, 4, "UNFALSIFIABLE"),

            new("R18", "Neutrino: delocalized + normal ordering", "X059-X060",
                new[] { "A1", "A5", "A7", "A18" }, 2, "STRONG (identity), FRAGILE (ordering)"),
        };
    }

    public static Dictionary<string, int> ComputeAssumptionImpact(
        List<Assumption> assumptions, List<ResultNode> results)
    {
        var impact = new Dictionary<string, int>();
        foreach (var a in assumptions)
        {
            int count = results.Count(r => r.RequiredAssumptions.Contains(a.Id));
            impact[a.Id] = count;
        }
        return impact;
    }

    public static string ImpactRanking(List<Assumption> assumptions, Dictionary<string, int> impact, int totalResults)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ASSUMPTION IMPACT — RESULTS SUPPORTED");
        sb.AppendLine();
        sb.AppendLine("  Rank  Assumption                          Tier  Results  Impact %");
        sb.AppendLine("  " + new string('-', 70));

        var ranked = assumptions.OrderByDescending(a => impact[a.Id]).ToList();
        for (int i = 0; i < ranked.Count; i++)
        {
            var a = ranked[i];
            int count = impact[a.Id];
            double pct = 100.0 * count / totalResults;
            string cls = pct > 50 ? "CRITICAL" : pct > 25 ? "HIGH" : pct > 10 ? "MEDIUM" : "LOW";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3}. {1,-35} {2,4}  {3,5}    {4,5:F0}%  {5}",
                i + 1, a.Name, a.Tier, count, pct, cls));
        }
        return sb.ToString();
    }

    public static string SinglePointsOfFailure(List<Assumption> assumptions, Dictionary<string, int> impact, int total)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SINGLE POINTS OF FAILURE");
        sb.AppendLine();

        var critical = assumptions.Where(a => impact[a.Id] > total * 0.25).OrderByDescending(a => impact[a.Id]);
        foreach (var a in critical)
        {
            double pct = 100.0 * impact[a.Id] / total;
            string severity = pct > 75 ? "CATASTROPHIC — >75% of framework" :
                              pct > 50 ? "CRITICAL — >50% of framework" :
                              "HIGH — >25% of framework";
            sb.AppendLine($"  [{severity}] {a.Name} (A{a.Id[1..]})");
            sb.AppendLine($"    Tier {a.Tier}. Supports {impact[a.Id]}/{total} results ({pct:F0}%).");
            sb.AppendLine($"    {a.FragilityNote}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string FragilityCorrelation(List<ResultNode> results)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FRAGILITY CORRELATION — XE001 + XE002");
        sb.AppendLine();
        sb.AppendLine("  Result                        XE001 Class    Root Assumption");
        sb.AppendLine("  " + new string('-', 65));

        foreach (var r in results)
        {
            string rootCause = r.FragilityFromXE001 switch
            {
                "FRAGILE (XE001)" => "A9 (stability cutoff α ≈ 1.5)",
                "FRAGILE numerically" => "A16 (degree definition)",
                "EXPOSED" => "A15 (uncomputed coefficient)",
                "UNFALSIFIABLE" => "A17 (no experimental access)",
                "EXTERNAL GAP" => "A6 (BDG action dependency)",
                "ROBUST" or "HIGHLY ROBUST" or "STRONG" or "RIGOROUS" => "CLT / topology / theorem (intrinsic robustness)",
                _ => "—"
            };
            string cls = r.FragilityFromXE001 switch
            {
                "FRAGILE (XE001)" or "FRAGILE numerically" => "FRAGILE",
                "EXPOSED" => "EXPOSED",
                _ => "STABLE"
            };
            sb.AppendLine($"  {r.Name,-30} {cls,-10} {rootCause}");
        }

        sb.AppendLine();
        sb.AppendLine("  PATTERN: Fragility traces to specific numerical assumptions (α, degree def).");
        sb.AppendLine("  Qualitative conclusions are protected by CLT / topology / theorems.");
        return sb.ToString();
    }

    public static string MinimalCore(List<Assumption> assumptions, List<ResultNode> results)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MINIMAL TQM CORE — TIER BY TIER");
        sb.AppendLine();

        // Tier 0: axioms
        var tier0 = assumptions.Where(a => a.Tier == 0).ToList();
        sb.AppendLine("  TIER 0 — AXIOMS (cannot remove):");
        foreach (var a in tier0)
            sb.AppendLine($"    {a.Id}: {a.Name}");
        sb.AppendLine($"    Supports: ALL results. Without these, nothing exists.");
        sb.AppendLine();

        // Tier 1: structural — needed for QM + spacetime
        var tier1 = assumptions.Where(a => a.Tier == 1).ToList();
        sb.AppendLine("  TIER 1 — STRUCTURAL (needed for QM, time, spacetime, GR):");
        foreach (var a in tier1)
            sb.AppendLine($"    {a.Id}: {a.Name}");
        sb.AppendLine($"    Removing A3 (complexity) kills R1, R3. Removing A6 (causal set) kills R4, R15, R16.");
        sb.AppendLine();

        // Tier 2: identity — needed for particle physics
        var tier2 = assumptions.Where(a => a.Tier == 2).ToList();
        sb.AppendLine("  TIER 2 — IDENTITY (needed for particles, gauge, generations, masses):");
        foreach (var a in tier2)
        {
            int supporters = results.Count(r => r.RequiredAssumptions.Contains(a.Id));
            sb.AppendLine($"    {a.Id}: {a.Name} — supports {supporters} results");
        }
        sb.AppendLine();

        // Minimal survival
        sb.AppendLine("  MINIMAL SURVIVING FRAMEWORK (if all Tier 3-4 fail):");
        sb.AppendLine("    Tier 0-2: QM, time, spacetime, GR, particles, gauge, generations, masses, mixing.");
        sb.AppendLine("    LOST: Abundance distributions, cosmology, DM identity, M² elimination.");
        sb.AppendLine("    Surviving parameter count: ~1 (M²).");
        sb.AppendLine("    This is STILL a ~95% reduction from SM.");
        return sb.ToString();
    }
}
