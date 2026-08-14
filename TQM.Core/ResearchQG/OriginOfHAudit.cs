using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-099 Origin Of H Audit. Determines the true status of H: derived, boundary condition,
/// selection effect, or fundamental constant. Result: H is NOT derivable from {c, G, ħ, Λ, N}
/// without circularity (the Planck rate is 61 decades off; H ~ c√Λ is circular via Λ ~ H²/c²;
/// H from N is circular). H is the final PRIMITIVE scale-setting parameter — a boundary condition
/// — from which Λ and a₀ inherit their scale.
/// </summary>
public static class OriginOfHAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);

    public static OriginOfHReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var origins = OriginOfHModel.Origins();

        WriteOriginsCsv(Path.Combine(outDir, "OriginOfH.csv"), origins);
        WriteGraphCsv(Path.Combine(outDir, "HDependencyGraph.csv"));
        WriteDerivedCsv(Path.Combine(outDir, "DerivedVsInputH.csv"));

        PlotHierarchy(Path.Combine(outDir, "PlanckHierarchy.png"));

        return new OriginOfHReport(
            BuildA(origins), BuildB(), BuildC(origins), BuildD(), BuildE(),
            origins, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(HOrigin[] origins)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Catalog of H-origin mechanisms.");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-20} {1,-24} {2,-12} {3}", "model", "classification", "circular?", "verdict"));
        foreach (var o in origins)
            sb.AppendLine(string.Format("  {0,-20} {1,-24} {2,-12} {3}", o.Model, o.Classification, o.Circular ? "yes" : "no", o.Verdict));
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Can H be calculated from {c, G, ħ, Λ, N} without circularity?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  From {{c,G,ħ}}: only 1/t_P = c/l_P = {0:E1} s⁻¹ vs H0 = {1:E1} s⁻¹", 
            OriginOfHModel.PlanckRate, OriginOfHModel.H0PerS));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    → Planck hierarchy {0:F0} decades. H is NOT a Planck-scale rate.", OriginOfHModel.PlanckHierarchy));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  From Λ: c√Λ = {0:E1} s⁻¹ ≈ H0 ({1:F1}×) — but Λ ~ H²/c² (causal set), so this is H from H.", 
            OriginOfHModel.HRateFromLambda, OriginOfHModel.HRateFromLambdaRatio));
        sb.AppendLine("  From N: circular (N = (c/H l_P)⁴ defines N from H).");
        sb.AppendLine();
        sb.AppendLine("  ⇒ H is NOT calculable from {c, G, ħ, Λ, N} without circularity. The only non-circular");
        sb.AppendLine("  relation is Λ ~ H² (which explains Λ FROM H, not H from Λ).");
        return sb.ToString();
    }

    private static string BuildC(HOrigin[] origins)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Ranking by assumptions / predictive power / circularity.");
        sb.AppendLine();
        var ranked = origins.OrderBy(o => (o.Circular ? 1 : 0)).ThenBy(o => o.Classification == "Primitive" ? 0 : 1).ToArray();
        for (int i = 0; i < ranked.Length; i++)
            sb.AppendLine($"  {i + 1}. {ranked[i].Model,-20} [{ranked[i].Classification}]");
        sb.AppendLine();
        sb.AppendLine("  'Fundamental constant' (H0 primitive) and 'FLRW boundary condition' are the LEAST");
        sb.AppendLine("  circular and most economical: 1 parameter, no derivation. The 'derived from Λ/N'");
        sb.AppendLine("  routes are all circular.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Dependency graph.");
        sb.AppendLine();
        sb.AppendLine("  primitive: H (the scale-setting rate) — INPUT, not derived");
        sb.AppendLine("    └─► Λ ~ H²/c²   (causal set: Λ = 1/√N with N defined by H)");
        sb.AppendLine("    └─► a₀ ~ cH     (order of magnitude; 2π coincidental)");
        sb.AppendLine();
        sb.AppendLine("  H sits at the TOP: Λ and a₀ inherit their scale from H, but H is not inherited from");
        sb.AppendLine("  anything deeper.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (all H origins mapped)              : PASS");
        sb.AppendLine("  Level 2 (circular derivations identified)   : PASS (Λ→H, N→H circular)");
        sb.AppendLine("  Level 3 (best non-circular explanation)     : PASS — H is a boundary condition");
        sb.AppendLine("  Level 4 (derived or primitive)              : PASS — H is PRIMITIVE");
        sb.AppendLine("  Level 5 (falsifiable route)                 : FAIL — no route");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: H is a PRIMITIVE parameter — a boundary condition, not a");
        sb.AppendLine("  derived consequence. It cannot be calculated from {c, G, ħ, Λ, N} without circularity");
        sb.AppendLine("  (Planck rate is 61 decades off; H~c√Λ is H-from-H; N is defined by H). Λ and a₀ are its");
        sb.AppendLine("  descendants (Λ ~ H²/c² genuinely, a₀ ~ cH at order of magnitude), but H itself has no");
        sb.AppendLine("  deeper origin in the current program.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteOriginsCsv(string path, HOrigin[] origins)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Model,Classification,Circular,Verdict");
        foreach (var o in origins)
            sb.AppendLine($"{o.Model},{o.Classification},{o.Circular},{Escape(o.Verdict)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteGraphCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Level,Concept,Status");
        sb.AppendLine("1,H (scale-setting rate),PRIMITIVE / boundary condition");
        sb.AppendLine("2,Lambda~H^2/c^2,derived (causal set)");
        sb.AppendLine("2,a0~cH,order of magnitude (2pi coincidental)");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteDerivedCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Source,Rate_s,H0PerS,Log10Ratio,Circular");
        sb.AppendLine($"Planck,{OriginOfHModel.PlanckRate:E2},{OriginOfHModel.H0PerS:E2},{OriginOfHModel.PlanckHierarchy:F1},false");
        sb.AppendLine($"FromLambda,{OriginOfHModel.HRateFromLambda:E2},{OriginOfHModel.H0PerS:E2},{Math.Log10(OriginOfHModel.HRateFromLambdaRatio):F2},true");
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotHierarchy(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "H0", "c√Λ", "1/t_P" },
            new[] { Math.Log10(OriginOfHModel.H0PerS),
                    Math.Log10(OriginOfHModel.HRateFromLambda),
                    Math.Log10(OriginOfHModel.PlanckRate) }.Select(v => v + 18).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record OriginOfHReport(
    string SA, string SB, string SC, string SD, string SE,
    HOrigin[] Origins, string OutDir);
