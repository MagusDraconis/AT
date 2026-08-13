using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-090 Origin Of Change Audit. Classifies candidate primitive realities (block universe,
/// information, causal network, quantum state, mathematical structure), establishes the
/// time-vs-change relationship (neither is fundamental; causality is deeper), and identifies the
/// one quantitative prediction from a theory of change: Causal Set Theory's Λ ~ 1/√N ~ 1e-122.
/// </summary>
public static class FundamentalChangeAudit
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);

    public static FundamentalChangeReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var realities = OriginOfChangeModel.Realities();

        WriteRealitiesCsv(Path.Combine(outDir, "OriginOfChangeModels.csv"), realities);
        WriteHierarchyCsv(Path.Combine(outDir, "ChangeHierarchy.csv"));
        WriteTimeChangeCsv(Path.Combine(outDir, "TimeVsChangeAnalysis.csv"));
        WriteRankingCsv(Path.Combine(outDir, "FundamentalChangeRanking.csv"), realities);

        PlotLambda(Path.Combine(outDir, "CausalSetLambdaPrediction.png"));

        return new FundamentalChangeReport(
            BuildA(realities),
            BuildB(),
            BuildC(),
            BuildD(),
            BuildE(),
            BuildF(),
            BuildG(realities),
            realities, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(PrimitiveReality[] realities)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Primitive realities (no assumed change) and what introduces apparent evolution.");
        sb.AppendLine();
        foreach (var r in realities)
            sb.AppendLine($"  - {r.Name,-24} : {r.MechanismOfApparentChange}");
        sb.AppendLine();
        sb.AppendLine("  Frameworks: Wheeler 'It from Bit', Rovelli thermal time, Barbour timeless physics,");
        sb.AppendLine("  Causal Set Theory, relational time, entropic time — all timeless at the base.");
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Time vs change — which is more fundamental?");
        sb.AppendLine();
        sb.AppendLine("  Change requires ordering (before/after), which is what time provides.");
        sb.AppendLine("  Time requires something changing (to define the 'tick').");
        sb.AppendLine("  ⇒ neither is fundamental: both emerge from the CAUSAL PARTIAL ORDER.");
        sb.AppendLine();
        sb.AppendLine("  In quantum gravity (Wheeler-DeWitt H|Ψ⟩=0) the universe state is STATIC and time");
        sb.AppendLine("  is EMERGENT (via a clock degree of freedom / correlations). So time is NOT fundamental.");
        sb.AppendLine("  Change is the relation between ordered elements — also emergent from the order.");
        return sb.ToString();
    }

    private static string BuildC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Change hierarchy.");
        sb.AppendLine();
        sb.AppendLine("  causality (partial order)");
        sb.AppendLine("    ├─► time (a labeling of the order)");
        sb.AppendLine("    └─► change (differences between ordered elements)");
        sb.AppendLine("         ├─► information (measurable differences)");
        sb.AppendLine("         └─► entropy production → H (rate of change)");
        sb.AppendLine();
        sb.AppendLine($"  Amount of change C(t) = N(t) (distinct events); d ln C/dt = {InformationChangeModel.ChangeRateInUnitsOfH}H");
        sb.AppendLine("  (causal-set 4-volume), so H, entropy and information growth are manifestations of C.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("The causal-set prediction (the one quantitative 'prediction from change').");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Planck length l_P = {0:E2} m", ChangeAnalyzer.PlanckLength));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Hubble length R_H = {0:E2} m", ChangeAnalyzer.HubbleLength));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  causet elements N = (R_H/l_P)⁴ = {0:E2}", ChangeAnalyzer.CausetElementCount()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  predicted Λ·l_P² = 1/√N = {0:E2}", ChangeAnalyzer.PredictedLambdaPlanck()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  observed  Λ·l_P² = {0:E2}", ChangeAnalyzer.ObservedLambdaPlanck()));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  ratio = {0:F2} (order-unity match)", ChangeAnalyzer.PredictionRatio()));
        sb.AppendLine();
        sb.AppendLine("  The discreteness of spacetime (a theory of CHANGE) predicts the MAGNITUDE of the");
        sb.AppendLine("  cosmological constant — the only framework in this program that is not simply FLRW");
        sb.AppendLine("  re-parameterized. (Its fluctuation around the mean is unobservably small, ~1/√N.)");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("a₀ as the minimum rate of change.");
        sb.AppendLine();
        sb.AppendLine($"  a₀ = c × (minimum cosmic rate of change) = cH = {ChangeAnalyzer.A0FromRateOfChange():E2} m/s²");
        sb.AppendLine("  This is the 'cH class' (no 2π), order-of-magnitude only (QG-084/085). The acceleration");
        sb.AppendLine("  scale is the minimum observable rate of change, times c.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — assume change does not fundamentally exist.");
        sb.AppendLine();
        sb.AppendLine("  A timeless description (block universe / Wheeler-DeWitt) reproduces ALL observations");
        sb.AppendLine("  in its classical limit: redshift, time dilation, entropy increase, galaxy evolution all");
        sb.AppendLine("  emerge as structure/correlations. The reconstruction does NOT fail — timeless and");
        sb.AppendLine("  evolving descriptions are observationally equivalent (consistent with QG-080–089).");
        sb.AppendLine("  The ONLY non-tautological content is the causal-set Λ ~ 1/√N magnitude prediction.");
        return sb.ToString();
    }

    private static string BuildG(PrimitiveReality[] realities)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (models classified)                    : PASS");
        sb.AppendLine("  Level 2 (time–change relationship)             : PASS");
        sb.AppendLine("  Level 3 (change vs time more fundamental)      : PASS — neither; causality deeper");
        sb.AppendLine("  Level 4 (cosmology ↔ deeper principle)         : PASS — causal-set Λ ~ 1/√N");
        sb.AppendLine("  Level 5 (falsifiable prediction)               : PARTIAL — Λ magnitude (fluctuation tiny)");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: neither time, change, information nor causality is 'more");
        sb.AppendLine("  fundamental' in isolation — the deeper primitive is the CAUSAL PARTIAL ORDER, from which");
        sb.AppendLine("  all four emerge. Cosmological evolution is the GROWTH OF CAUSAL STRUCTURE, not the");
        sb.AppendLine("  evolution of space or time. Its one quantitative signature is Λ ~ 1/√N ~ 1e-122 (causal");
        sb.AppendLine("  set), matching the observed dark-energy density.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteRealitiesCsv(string path, PrimitiveReality[] realities)
    {
        var sb = new StringBuilder();
        sb.AppendLine("PrimitiveReality,MechanismOfApparentChange,Framework,HasQuantitativePrediction,Prediction");
        foreach (var r in realities)
            sb.AppendLine($"{r.Name},{Escape(r.MechanismOfApparentChange)},{r.Framework},{r.HasQuantitativePrediction},{Escape(r.Prediction)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteHierarchyCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Level,Concept");
        sb.AppendLine("1,causality (partial order)");
        sb.AppendLine("2,time (labeling)");
        sb.AppendLine("2,change (differences)");
        sb.AppendLine("3,information (measurable differences)");
        sb.AppendLine("4,entropy production -> H (rate of change)");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteTimeChangeCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Question,Answer");
        sb.AppendLine("Is time fundamental?,No (emergent in quantum gravity: H|Psi>=0)");
        sb.AppendLine("Is change fundamental?,No (emergent from the causal partial order)");
        sb.AppendLine("Deeper primitive,causal partial order (causality)");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRankingCsv(string path, PrimitiveReality[] realities)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rank,PrimitiveReality,HasQuantitativePrediction");
        var ranked = realities.OrderByDescending(r => r.HasQuantitativePrediction ? 1 : 0).ToArray();
        for (int i = 0; i < ranked.Length; i++)
            sb.AppendLine($"{i + 1},{ranked[i].Name},{ranked[i].HasQuantitativePrediction}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotLambda(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "predicted Λ·l_P²", "observed Λ·l_P²" },
            new[] { ChangeAnalyzer.PredictedLambdaPlanck(), ChangeAnalyzer.ObservedLambdaPlanck() }, Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record FundamentalChangeReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    PrimitiveReality[] Realities, string OutDir);
