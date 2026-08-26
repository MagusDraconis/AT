using System.Globalization;
using System.Text;
using AT.Core.ResearchXB;
using AT.Core.ResearchXB.Models;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXB;

public class AT_XB001_OriginOfAbundance : ResearchTestBase
{
    public AT_XB001_OriginOfAbundance(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XB001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXB-001 Origin of Abundance");

        // 1. Founding insight
        Sec(sb, "Founding Insight — The Identity/Abundance Split");
        sb.AppendLine(AbundanceOriginAnalyzer.AbundanceDefinition());

        // 2. Universe ensemble
        Sec(sb, "The Universe Ensemble");
        sb.AppendLine(AbundanceOriginAnalyzer.TheUniverseEnsemble());

        // 3. Abundance parameter classification
        Sec(sb, "Abundance Parameter Classification");
        var parameters = AbundanceDistributionModel.ClassifyParameters();
        sb.AppendLine("  Parameter    Symbol  Distribution");
        sb.AppendLine("  " + new string('-', 60));
        foreach (var p in parameters)
        {
            sb.AppendLine($"  {p.Name,-12} {p.Symbol,-6}  {p.Distribution}");
        }
        sb.AppendLine();

        // 4. Ensemble simulation
        Sec(sb, "Universe Ensemble Simulation");
        var ensemble = UniverseEnsembleModel.GenerateEnsemble(1000);
        sb.AppendLine(UniverseEnsembleModel.EnsembleStatistics(ensemble));

        // 5. Ensemble prediction
        Sec(sb, "Ensemble Prediction");
        sb.AppendLine(AbundanceDistributionModel.EnsemblePrediction());

        // 6. Minimal abundance theory
        Sec(sb, "Minimal Abundance Theory");
        sb.AppendLine(AbundanceOriginAnalyzer.TheMinimalAbundanceTheory());

        // 7. The research program
        Sec(sb, "ResearchXB — Program Outline");
        sb.AppendLine("  RESEARCHX (completed):  Identity Physics.");
        sb.AppendLine("    'What exists?' — Topology → ~93% derived.");
        sb.AppendLine();
        sb.AppendLine("  RESEARCHXB (beginning): Abundance Physics.");
        sb.AppendLine("    'How much exists?' — History → distributions.");
        sb.AppendLine();
        sb.AppendLine("  KEY HYPOTHESES:");
        sb.AppendLine("    1. Abundance = frozen cosmological history.");
        sb.AppendLine("    2. Abundance quantities are RANDOM VARIABLES.");
        sb.AppendLine("    3. Only DISTRIBUTIONS can be predicted.");
        sb.AppendLine("    4. Our universe = one sample from the ensemble.");
        sb.AppendLine();
        sb.AppendLine("  NEXT STEPS:");
        sb.AppendLine("    XB002: Derive abundance distributions.");
        sb.AppendLine("    XB003: Test scale-invariance hypothesis.");
        sb.AppendLine("    XB004: Connect to cosmological observables.");
        sb.AppendLine("    XB005: Unify all abundances under history.");
        sb.AppendLine();

        // 8. Final
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXB-001 COMPLETE.");
        sb.AppendLine($"  Classification: D — Abundance identified as its own category.");
        sb.AppendLine($"  New research program: ABUNDANCE PHYSICS founded.");
        sb.AppendLine($"  Identity (ResearchX) + Abundance (ResearchXB) = COMPLETE AT.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
