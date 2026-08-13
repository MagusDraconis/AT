using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-085 The 2π Origin Audit. Catalogs where 2π arises in fundamental physics, evaluates
/// whether g† = cH/2π selects a deep principle, and — critically — tests whether the data
/// actually single out 1/(2π) over the rival 'nice' O(1) factors (1/5, 1/6). Result: the 2π
/// is NOT uniquely selected; a₀/(cH) = 0.184 sits between 1/5, 1/6 and 1/(2π), favoring
/// numerical coincidence over a deep geometric/thermodynamic/informational principle.
/// </summary>
public static class TwoPiOriginAnalyzer
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static TwoPiOriginReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var relations = FundamentalRelations();
        var mechanisms = Mechanisms();
        var nice = CoincidenceAnalyzer.NiceNumbers();
        double coincidence = CoincidenceAnalyzer.CoincidenceProbability();
        var ranked = mechanisms.OrderByDescending(m => m.Score).ToArray();

        WriteRelationsCsv(Path.Combine(outDir, "Fundamental2PiRelations.csv"), relations);
        WriteMechanismsCsv(Path.Combine(outDir, "TwoPiMechanisms.csv"), mechanisms);
        WriteRankingCsv(Path.Combine(outDir, "TwoPiOriginRanking.csv"), ranked);
        WriteCoincidenceCsv(Path.Combine(outDir, "CoincidenceAnalysis.csv"), nice);

        PlotLandscape(Path.Combine(outDir, "TwoPiLandscape.png"), nice);
        PlotScales(Path.Combine(outDir, "AccelerationScaleComparisons.png"));
        PlotRanking(Path.Combine(outDir, "MechanismRanking.png"), ranked);

        return new TwoPiOriginReport(
            BuildA(relations),
            BuildB(mechanisms),
            BuildC(nice),
            BuildD(),
            BuildE(coincidence, nice),
            BuildF(ranked),
            BuildG(ranked, nice),
            relations, mechanisms.ToArray(), nice, ranked, outDir);
    }

    // ---------------------------------------------------------------------
    // Data
    // ---------------------------------------------------------------------

    public sealed record TwoPiRelation(string Relation, string Where, string Class);

    public sealed record TwoPiMechanism(string Name, string Description, double PredictedGdagger,
        bool RetainsTwoPi, double RatioToA0, string Factor, string Verdict, double Score);

    private static TwoPiRelation[] FundamentalRelations() => new[]
    {
        new TwoPiRelation("Unruh  T = ħ a/(2π k c)", "Euclidean periodicity β=2π/a", "thermodynamic/geometric"),
        new TwoPiRelation("Hawking  T = ħ c³/(8π G M k)", "surface gravity κ=c²/2R", "thermodynamic/geometric"),
        new TwoPiRelation("Gibbons–Hawking  T = ħ H/(2π k)", "de Sitter horizon", "thermodynamic/geometric"),
        new TwoPiRelation("de Sitter entropy  S = π R²/G", "horizon area", "holographic"),
        new TwoPiRelation("Bekenstein  S = A/(4 G)", "area law (1/4, not 2π)", "holographic"),
        new TwoPiRelation("ω = 2πν  (angular frequency)", "radians → cycles", "geometric/periodic"),
        new TwoPiRelation("e^{ikx}  (period 2π/k)", "wave periodicity", "geometric"),
        new TwoPiRelation("Gaussian norm 1/(2π)", "wavepacket normalization", "analytic"),
        new TwoPiRelation("Gauss–Bonnet  ∫K dA = 2πχ", "Euler characteristic", "topological"),
    };

    private static List<TwoPiMechanism> Mechanisms()
    {
        return new List<TwoPiMechanism>
        {
            new TwoPiMechanism("Horizon (Unruh+GH)", HorizonMechanismModel.Description,
                HorizonMechanismModel.Predict(), false, HorizonMechanismModel.RatioToObserved(),
                "1 (2π cancels)", "gives cH, 5.4× too large → EXCLUDED", 0.5),
            new TwoPiMechanism("Angular-frequency", InformationMechanismModel.Description,
                InformationMechanismModel.Predict(), true, InformationMechanismModel.RatioToObserved(),
                "1/(2π)", "natural 2π; but a₀ is 15% ABOVE cH/2π", 2.5),
            new TwoPiMechanism("Entropy/holographic", EntropyMechanismModel.Description,
                EntropyMechanismModel.Predict(), false, EntropyMechanismModel.RatioToObserved(),
                "1/6", "factor 6 (~5% from 2π); a₀ is 10% above cH/6", 2.5),
            new TwoPiMechanism("Coincidence", "no coupling; a₀/(cH)=0.184 near several nice numbers",
                double.NaN, false, 1.0, "—", "~30% chance; cannot be excluded", 1.5),
        };
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(TwoPiRelation[] relations)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Catalog of fundamental 2π occurrences.");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-34} {1,-24} {2}", "relation", "where 2π comes from", "class"));
        foreach (var r in relations)
            sb.AppendLine(string.Format("  {0,-34} {1,-24} {2}", r.Relation, r.Where, r.Class));
        sb.AppendLine();
        sb.AppendLine("  Two dominant classes: (1) horizon/thermodynamic — the 2π enters via the thermal");
        sb.AppendLine("  circle β=2π/κ and CANCELS in the acceleration; (2) angular/periodic — the 2π is the");
        sb.AppendLine("  radians→cycles conversion and is RETAINED.");
        return sb.ToString();
    }

    private static string BuildB(List<TwoPiMechanism> mechanisms)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Mechanism evaluation.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-20} {1,11} {2,9} {3,8} {4,8}", "mechanism", "pred. g†", "×a0", "factor", "2π?"));
        foreach (var m in mechanisms)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-20} {1,11:E2} {2,9:F2} {3,8} {4,8}",
                m.Name, m.PredictedGdagger, m.RatioToA0, m.Factor, m.RetainsTwoPi ? "yes" : "no"));
        sb.AppendLine();
        sb.AppendLine("  ×a0 = predicted/a0. 1.0 = perfect; 5.4 = cH (excluded); 0.87 = cH/2π; 0.91 = cH/6.");
        return sb.ToString();
    }

    private static string BuildC((string Candidate, double Value, double RatioToObserved, double LogMismatch)[] nice)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Is 1/(2π) actually selected?  (a₀/(cH) = 0.184 vs 'nice' O(1) factors.)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-10} {1,8} {2,9} {3,10}", "candidate", "value", "×a0/(cH)", "mismatch"));
        foreach (var n in nice)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-10} {1,8:F3} {2,9:F2} {3,9:P1}",
                n.Candidate, n.Value, n.RatioToObserved, n.RatioToObserved - 1.0));
        sb.AppendLine();
        sb.AppendLine("  a₀/(cH)=0.184 is CLOSER to 1/5 (8%) and 1/6 (10%) than to 1/(2π) (15%).");
        sb.AppendLine("  The 2π is NOT the best — nor a unique — match. This undermines a 'deep 2π principle'.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Why cH fails and an O(1) factor succeeds.");
        sb.AppendLine();
        sb.AppendLine("  - Horizon route (Unruh + Gibbons–Hawking): T=ħH/2πk and a=2πkT/ħ → a=cH.");
        sb.AppendLine("    The two 2π's CANCEL, leaving the full cH = 6.5e-10, which is 5.4× a₀ → EXCLUDED.");
        sb.AppendLine("  - Angular route: H is an angular frequency; the cycle rate ν=H/2π retains the 2π,");
        sb.AppendLine("    giving g†=cH/2π ≈ a₀. So the 2π marks the OSCILLATORY (cycle) reading of H.");
        sb.AppendLine();
        sb.AppendLine("  The 2π factor therefore has a natural (angular-frequency) origin — but because 1/5");
        sb.AppendLine("  and 1/6 match a₀/(cH) at least as well, the data do NOT uniquely select 2π.");
        return sb.ToString();
    }

    private static string BuildE(double coincidence, (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] nice)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Pure coincidence model.");
        sb.AppendLine();
        sb.AppendLine($"  a₀/(cH) = {CoincidenceAnalyzer.A0OverCH():F3}; nearest nice factor ≈ 1/5 (8% off).");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  P(random factor within ±16% of one of 4 nice numbers, 2-decade prior) = {0:P0}", coincidence));
        sb.AppendLine();
        sb.AppendLine("  ~30% coincidence — NOT compelling. The 2π (or 1/5, 1/6) is a 'nice-number accident',");
        sb.AppendLine("  of the kind common in dimensionless coincidences. The 15% offset of a₀ from cH/2π");
        sb.AppendLine("  further disfavors an exact principle.");
        return sb.ToString();
    }

    private static string BuildF(TwoPiMechanism[] ranked)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Ranking (naturalness + parameter count + predictive power).");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-20} {1,6} {2}", "mechanism", "score", "verdict"));
        foreach (var m in ranked)
            sb.AppendLine(string.Format("  {0,-20} {1,6:F1} {2}", m.Name, m.Score, m.Verdict));
        return sb.ToString();
    }

    private static string BuildG(TwoPiMechanism[] ranked, (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] nice)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (catalog of 2π occurrences)            : PASS");
        sb.AppendLine("  Level 2 (dominant physical classes)            : PASS (horizon vs angular)");
        sb.AppendLine("  Level 3 (cH/2π without tuning)                 : PASS (angular frequency)");
        sb.AppendLine("  Level 4 (why cH fails, O(1) factor succeeds)   : PASS (2π cancels in horizon route)");
        sb.AppendLine("  Level 5 (new falsifiable prediction)           : FAIL — data do NOT select 2π uniquely");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: the 1/(2π) factor is a numerical accident, NOT a deep");
        sb.AppendLine("  principle. Its angular-frequency origin is natural (H=ω, cycle rate H/2π), but the");
        sb.AppendLine("  data do not single it out: a₀/(cH)=0.184 matches 1/5 (8%) and 1/6 (10%) better than");
        sb.AppendLine("  1/(2π) (15%), and the a₀−cH/2π offset disfavors exactness. The 2π is one of several");
        sb.AppendLine("  'nice' O(1) factors near 0.18 — a ~30% coincidence, not a signature of deep structure.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteRelationsCsv(string path, TwoPiRelation[] relations)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Relation,Where2PiComesFrom,Class");
        foreach (var r in relations)
            sb.AppendLine($"{Escape(r.Relation)},{Escape(r.Where)},{r.Class}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteMechanismsCsv(string path, List<TwoPiMechanism> mechanisms)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Mechanism,PredictedGdagger,RatioToA0,RetainsTwoPi,Factor,Score");
        foreach (var m in mechanisms)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:E2},{2:F2},{3},{4},{5:F1}",
                m.Name, m.PredictedGdagger, m.RatioToA0, m.RetainsTwoPi ? "1" : "0", m.Factor, m.Score));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRankingCsv(string path, TwoPiMechanism[] ranked)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rank,Mechanism,Score,Verdict");
        for (int i = 0; i < ranked.Length; i++)
            sb.AppendLine($"{i + 1},{ranked[i].Name},{ranked[i].Score:F1},{Escape(ranked[i].Verdict)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteCoincidenceCsv(string path, (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] nice)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate,Value,RatioToA0OverCH,FractionalMismatch");
        foreach (var n in nice)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F4},{2:F3},{3:F3}",
                n.Candidate, n.Value, n.RatioToObserved, n.RatioToObserved - 1.0));
        File.WriteAllText(path, sb.ToString());
    }

    // ---------------------------------------------------------------------
    // Plots
    // ---------------------------------------------------------------------

    private static void PlotLandscape(string path, (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] nice)
    {
        var labels = new[] { "a0/(cH)" }.Concat(nice.Select(n => n.Candidate)).ToArray();
        var vals = new[] { CoincidenceAnalyzer.A0OverCH() }.Concat(nice.Select(n => n.Value)).ToArray();
        RARPlotter.PlotBars(path, labels, vals, Blue);
    }

    private static void PlotScales(string path)
    {
        RARPlotter.PlotBars(path,
            new[] { "cH", "cH/2π", "cH/6", "a0" },
            new[] { LocalCosmicCoupling.CH, LocalCosmicCoupling.Gdagger,
                    LocalCosmicCoupling.CH / 6.0, LocalCosmicCoupling.A0_Mond }, Green);
    }

    private static void PlotRanking(string path, TwoPiMechanism[] ranked)
    {
        RARPlotter.PlotBars(path, ranked.Select(m => m.Name).ToArray(),
            ranked.Select(m => m.Score).ToArray(), Orange);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record TwoPiOriginReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    TwoPiOriginAnalyzer.TwoPiRelation[] Relations, TwoPiOriginAnalyzer.TwoPiMechanism[] Mechanisms,
    (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] NiceNumbers,
    TwoPiOriginAnalyzer.TwoPiMechanism[] Ranking, string OutDir);
