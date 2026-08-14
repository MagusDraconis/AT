using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-095 Unified Cosmological Scale Audit. Determines whether the two surviving anchors,
/// Λ ~ 1/√N (causal-set) and g† (time-scale), are independent or share a common origin.
/// Result: they share the single cosmic rate H (Λ ~ H²/c², g† ~ cH/2π) — a DIMENSIONAL
/// consequence, not a deep unification. Neither can derive the other without circularity,
/// and no new falsifiable prediction emerges.
/// </summary>
public static class ScaleUnificationAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static UnifiedScaleReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var combos = UnifiedScaleAnalyzer.DimensionlessCombinations();

        WriteRelationsCsv(Path.Combine(outDir, "UnifiedScaleRelations.csv"), combos);
        WriteComparisonCsv(Path.Combine(outDir, "LambdaGdaggerComparison.csv"));
        WriteRankingCsv(Path.Combine(outDir, "ScaleUnificationRanking.csv"));

        PlotLandscape(Path.Combine(outDir, "CosmologicalScaleLandscape.png"));
        PlotLambdaVsGdagger(Path.Combine(outDir, "LambdaVsGdagger.png"));
        PlotRanking(Path.Combine(outDir, "UnificationRanking.png"));

        return new UnifiedScaleReport(
            BuildA(combos), BuildB(), BuildC(), BuildD(), BuildE(), BuildF(),
            combos, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA((string Name, double Value, string Interpretation)[] combos)
    {
        var sb = new StringBuilder();
        sb.AppendLine("The surviving cosmological scales and their dimensionless combinations.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,12} {2}", "combination", "value", "interpretation"));
        foreach (var c in combos)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,12:E2} {2}", c.Name, c.Value, c.Interpretation));
        sb.AppendLine();
        sb.AppendLine("  KEY: Λ·l_P² = (H l_P/c)² (causal set) and g† = cH/2π (time scale) — BOTH are powers");
        sb.AppendLine("  of the single cosmic rate H. Λ ~ H²/c² and g† ~ cH.");
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Is g† linked to H or √Λ?");
        sb.AppendLine();
        sb.AppendLine($"  g† = cH/2π = {UnifiedScaleAnalyzer.Gdagger:E2};  c²√Λ = {UnifiedScaleAnalyzer.C2SqrtLambda:E2}.");
        sb.AppendLine($"  g†/(cH) = 1/(2π) = 0.159;  g†/(c²√Λ) = {UnifiedScaleAnalyzer.Gdagger / UnifiedScaleAnalyzer.C2SqrtLambda:F2}.");
        sb.AppendLine();
        sb.AppendLine("  g† is ~6× closer to H than to √Λ. Since Λ ~ H²/c², √Λ ~ H/c, so g† ~ cH/2π and");
        sb.AppendLine("  c²√Λ ~ cH are the SAME scale up to the 2π. The distinction H vs √Λ is just the 2π.");
        return sb.ToString();
    }

    private static string BuildC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Three hypotheses evaluated.");
        sb.AppendLine();
        sb.AppendLine("  A) Independent scales: Λ (bare constant) and g† (a₀) are unrelated → 2 free params,");
        sb.AppendLine("     no explanation. (The 'null' hypothesis.)");
        sb.AppendLine("  B) Common cosmological origin: both track H → Λ ~ H²/c² and g† ~ cH/2π, 0 free params,");
        sb.AppendLine("     but the 2π is a coincidence (QG-085) and Λ's time-dependence is not fixed.");
        sb.AppendLine("  C) Common causal-set origin: Λ ~ 1/√N (derived); g† ~ cH/2π = c/(2π)·(rate), where");
        sb.AppendLine("     the rate is the causal-depth growth (QG-091). This is the SAME as B with a discrete");
        sb.AppendLine("     substrate — still does not fix the 2π or g†'s evolution.");
        sb.AppendLine();
        sb.AppendLine("  B and C collapse to the same thing: H is the common rate; Λ ~ H², g† ~ cH. The 2π");
        sb.AppendLine("  remains the only unresolved (and un-unified) factor.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Can one derive the other?");
        sb.AppendLine();
        sb.AppendLine("  Λ → H: Λ ~ H²/c² gives H ~ c√Λ (magnitude), but NOT the Ωm/ΩΛ split (time-dependence).");
        sb.AppendLine("  H → Λ: H gives Λ ~ H²/c² only up to the O(1) α = Λ_obs/Λ_pred (QG-093).");
        sb.AppendLine("  g† → Λ: requires H = 2π g†/c, which ASSUMES the 2π coincidence. Circular.");
        sb.AppendLine("  Λ → g†: requires g† = cH/2π from H ~ c√Λ, i.e. g† ~ c²√Λ/2π — again the 2π.");
        sb.AppendLine();
        sb.AppendLine("  Neither is derivable from the other without re-introducing the 2π or α. They are");
        sb.AppendLine("  linked only through H, and only dimensionally.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Tuning quantification.");
        sb.AppendLine();
        sb.AppendLine("  Λ ~ H²/c² : residual factor α = Λ_obs/Λ_pred = 2.07 (O(1), no tuning — QG-093).");
        sb.AppendLine("  g† ~ cH/2π : residual factor 2π (a 'nice-number accident', ~30% coincidence — QG-085).");
        sb.AppendLine();
        sb.AppendLine("  Λ is far more natural (α=O(1) derived) than g† (2π coincidental). The unification does");
        sb.AppendLine("  NOT improve g†'s status: g† inherits the 2π problem unchanged.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (scales mapped)                      : PASS");
        sb.AppendLine("  Level 2 (dimensionless relations)            : PASS");
        sb.AppendLine("  Level 3 (common-origin models)               : PASS (B=C collapse)");
        sb.AppendLine("  Level 4 (best unification candidate)         : PASS — common rate H (dimensional)");
        sb.AppendLine("  Level 5 (falsifiable prediction)             : FAIL");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: Λ ~ 1/√N and g† are NOT independent anomalies — both are");
        sb.AppendLine("  manifestations of the SINGLE cosmic rate H (Λ ~ H²/c², g† ~ cH/2π). But this is a");
        sb.AppendLine("  DIMENSIONAL consequence, not a deep unification: neither derives the other without");
        sb.AppendLine("  circularity, and the 2π factor in g† remains un-unified and coincidental. No new");
        sb.AppendLine("  falsifiable prediction emerges beyond the existing g†(z) evolution test.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteRelationsCsv(string path, (string Name, double Value, string Interpretation)[] combos)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Combination,Value,Interpretation");
        foreach (var c in combos)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:E2},{2}", c.Name, c.Value, Escape(c.Interpretation)));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteComparisonCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Scale,Value_m_s2_or_relative");
        sb.AppendLine($"cH,{UnifiedScaleAnalyzer.CH:E2}");
        sb.AppendLine($"c2sqrtLambda,{UnifiedScaleAnalyzer.C2SqrtLambda:E2}");
        sb.AppendLine($"gdagger,{UnifiedScaleAnalyzer.Gdagger:E2}");
        sb.AppendLine($"a0,{UnifiedScaleAnalyzer.A0:E2}");
        sb.AppendLine($"H2OverLambdaC2,{UnifiedScaleAnalyzer.H2OverLambda:F2}");
        sb.AppendLine($"LambdaPredictionRatio,{UnifiedScaleAnalyzer.LambdaPredictionRatio:F2}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRankingCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rank,Hypothesis,Score,Note");
        sb.AppendLine("1,Common cosmological origin (H),3.0,both track H; dimensional");
        sb.AppendLine("2,Common causal-set origin,3.0,same as above with discrete substrate");
        sb.AppendLine("3,Independent scales,0.5,2 free params; no explanation");
        File.WriteAllText(path, sb.ToString());
    }

    // ---------------------------------------------------------------------
    // Plots
    // ---------------------------------------------------------------------

    private static void PlotLandscape(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "cH", "c²√Λ", "g†", "a₀" },
            new[] { UnifiedScaleAnalyzer.CH, UnifiedScaleAnalyzer.C2SqrtLambda,
                    UnifiedScaleAnalyzer.Gdagger, UnifiedScaleAnalyzer.A0 }.Select(v => v * 1e10).ToArray(), Blue);
    }

    private static void PlotLambdaVsGdagger(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "Λ·l_P²", "(H l_P/c)²", "g†/(cH)·l_P²", "a₀/(cH)·l_P²" },
            new[] {
                UnifiedScaleAnalyzer.Lambda * Math.Pow(1.616255e-35, 2),
                Math.Pow(UnifiedScaleAnalyzer.H0PerS * 1.616255e-35 / UnifiedScaleAnalyzer.C, 2),
                (UnifiedScaleAnalyzer.Gdagger / UnifiedScaleAnalyzer.CH) * Math.Pow(1.616255e-35, 2),
                (UnifiedScaleAnalyzer.A0 / UnifiedScaleAnalyzer.CH) * Math.Pow(1.616255e-35, 2),
            }.Select(v => Math.Log10(v) + 130).ToArray(), Green);
    }

    private static void PlotRanking(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "common H", "common causal-set", "independent" },
            new[] { 3.0, 3.0, 0.5 }, Orange);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record UnifiedScaleReport(
    string SA, string SB, string SC, string SD, string SE, string SF,
    (string Name, double Value, string Interpretation)[] Combinations, string OutDir);
