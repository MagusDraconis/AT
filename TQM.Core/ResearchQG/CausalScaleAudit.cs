using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-096 Can Causal Discreteness Generate g† Audit. Determines whether causal-set structure
/// produces a preferred acceleration scale ~1e-10 m/s² the way it produces Λ ~ 1/√N. Result:
/// causal discreteness generates the cH SCALE (via causal-depth growth d ln depth/dt = H), but
/// NOT the 1/(2π) factor — so g† = cH/2π is only half-predicted: the magnitude (cH) is natural,
/// the 2π is inserted. Λ and g† are therefore NOT unified by causal discreteness.
/// </summary>
public static class CausalScaleAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static CausalScaleReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var scales = CausalSetAccelerationModel.Scales();
        double a0 = CausalSetAccelerationModel.A0;

        WriteScalesCsv(Path.Combine(outDir, "CausalSetAccelerationScales.csv"), scales, a0);
        WriteGdaggerCsv(Path.Combine(outDir, "GdaggerFromCausalSets.csv"), a0);
        WriteComparisonCsv(Path.Combine(outDir, "LambdaVsGdaggerOrigin.csv"));

        PlotScales(Path.Combine(outDir, "CausalSetAccelerationScales.png"), scales);

        return new CausalScaleReport(
            BuildA(scales, a0), BuildB(), BuildC(), BuildD(), BuildE(), BuildF(),
            scales, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(CausalAccelerationScale[] scales, double a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Acceleration scales constructible from {c, l_P, N, Λ, H} (no new parameters).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-26} {1,11} {2,9}", "scale", "value", "×a₀"));
        foreach (var s in scales)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-26} {1,11:E2} {2,9:E1}", s.Name, s.Value_m_s2, s.Value_m_s2 / a0));
        sb.AppendLine();
        sb.AppendLine("  a₀ = 1.2e-10 sits at ×0.87 of cH/2π and ×5.5 of cH. The causal candidates are all");
        sb.AppendLine("  ~cH (except Planck, which is 61 decades off). Only cH/2π matches a₀ — and it needs the 2π.");
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate derivations.");
        sb.AppendLine();
        sb.AppendLine("  A) Causal-depth growth: d ln(depth)/dt = H ⇒ a_eff = cH (no 2π).");
        sb.AppendLine("  B) Poisson fluctuations: δN ~ √N gives Λ ~ 1/√N, but no galactic acceleration scale.");
        sb.AppendLine("  C) Ever-present Λ: c²√Λ ~ cH (since Λ ~ H²/c²) — again cH, no 2π.");
        sb.AppendLine("  D) Causal horizon: c²/R_H = cH — again cH, no 2π.");
        sb.AppendLine();
        sb.AppendLine("  ALL candidate derivations give a_eff ~ cH. NONE produces the 1/(2π) factor. The 2π is");
        sb.AppendLine("  an angular-frequency (radians→cycles) geometric factor, not a counting/order quantity.");
        return sb.ToString();
    }

    private static string BuildC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Is g† predicted or inserted?");
        sb.AppendLine();
        sb.AppendLine("  The cH SCALE (≈ 6.5e-10) is PREDICTED by causal-depth growth. The 1/(2π) factor that");
        sb.AppendLine("  turns cH into a₀ ≈ 1.2e-10 is INSERTED (it is the angular-frequency convention, not a");
        sb.AppendLine("  causal-set consequence).");
        sb.AppendLine();
        sb.AppendLine("  ⇒ g† = cH/2π is HALF-predicted: the magnitude is natural, the 2π is not generated.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Λ derivation vs g† derivation.");
        sb.AppendLine();
        sb.AppendLine("  Λ ~ 1/√N : 0 free params; exponent −1/2 DERIVED (counting/order); α = O(1). FULLY predicted.");
        sb.AppendLine("  g† = cH/2π : 0 free params for cH, but the 2π is a free dimensionless factor (INSERTED).");
        sb.AppendLine("               HALF predicted.");
        sb.AppendLine();
        sb.AppendLine("  Λ is fully derived from discreteness; g† is only the cH scale from discreteness — the 2π");
        sb.AppendLine("  is the same 'nice-number accident' as in QG-085, and causal sets do not resolve it.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — assume g† has nothing to do with causal discreteness.");
        sb.AppendLine();
        sb.AppendLine("  g† = cH/2π is explained equally well by (a) the time-scale/clock interpretation (QG-080),");
        sb.AppendLine("  (b) a bare MOND a₀ constant, or (c) coincidence. Causal discreteness adds nothing: it");
        sb.AppendLine("  reproduces the cH scale that ANY H-based construction gives, and leaves the 2π untouched.");
        sb.AppendLine("  No observation prefers the causal-set origin of g† over these alternatives.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (acceleration scales derived)        : PASS");
        sb.AppendLine("  Level 2 (causal-set candidate identified)    : PASS (causal-depth → cH)");
        sb.AppendLine("  Level 3 (does g† emerge naturally?)          : FAIL — cH yes, the 2π NO");
        sb.AppendLine("  Level 4 (compare Λ and g† origins)           : PASS (Λ fully derived, g† half)");
        sb.AppendLine("  Level 5 (falsifiable consequence)            : FAIL — no new prediction");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: causal discreteness does NOT generate g† = cH/2π the way it");
        sb.AppendLine("  generates Λ ~ 1/√N. It generates the cH SCALE (via causal-depth growth), but the 1/(2π)");
        sb.AppendLine("  is an angular-frequency factor that counting/order does not fix. So Λ and g† are NOT");
        sb.AppendLine("  unified by causal discreteness: Λ is fully derived, g† is half-derived, and the 2π");
        sb.AppendLine("  remains the un-unified coincidence (QG-085).");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteScalesCsv(string path, CausalAccelerationScale[] scales, double a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Scale,Construction,Value_m_s2,RatioToA0,HasTwoPi");
        foreach (var s in scales)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1},{2:E2},{3:F1},{4}",
                s.Name, Escape(s.Construction), s.Value_m_s2, s.Value_m_s2 / a0, s.HasTwoPi ? "1" : "0"));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteGdaggerCsv(string path, double a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Question,Answer");
        sb.AppendLine($"Generates cH scale,{CausalGdaggerOrigin.AEffFromCausalDepth():E2} m/s²");
        sb.AppendLine($"Generates 2pi,{CausalGdaggerOrigin.GeneratesTwoPi}");
        sb.AppendLine($"PredictionStatus,{Escape(CausalGdaggerOrigin.PredictionStatus)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteComparisonCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Quantity,FreeParams,DerivationStatus,Tuning");
        sb.AppendLine("Lambda~1/sqrt(N),0,fully derived (counting/order),alpha=O(1)");
        sb.AppendLine("gdagger=cH/2pi,0 for cH but 2pi inserted,half derived,2pi coincidental");
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotScales(string path, CausalAccelerationScale[] scales)
    {
        RARPlotter.PlotBars(path, scales.Select(s => s.Name).ToArray(),
            scales.Select(s => Math.Log10(s.Value_m_s2) + 12).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record CausalScaleReport(
    string SA, string SB, string SC, string SD, string SE, string SF,
    CausalAccelerationScale[] Scales, string OutDir);
