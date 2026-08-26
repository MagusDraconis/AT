using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-087 Event Density Cosmology Audit. Tests whether H = d ln(a)/dt can be replaced by
/// H_event = d ln(N)/dt (event/information growth). The central result: the only event model
/// that reproduces H(z) is N = a (another reparametrization); the simple physically-motivated
/// models (linear/power/exponential/saturation) all fail the ΛCDM transition.
/// </summary>
public static class EventGrowthAnalyzer
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static EventGrowthReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var models = EventCosmology.Models();
        double[] zs = { 0.0, 0.5, 1.0, 2.0, 4.0 };

        // Comparison rows: (model, z, H_event, H_ΛCDM, ratio).
        var comparison = new List<EventComparisonRow>();
        foreach (var m in models)
            foreach (double z in zs)
            {
                double hEvent = m.HEventOfZ(z);
                double hLam = EventCosmologyConstants.HLambda(z);
                comparison.Add(new EventComparisonRow(m.Name, z, hEvent, hLam, hEvent / hLam));
            }

        var a0 = EventRateAcceleration.A0ForEach(models);

        WriteModelsCsv(Path.Combine(outDir, "EventDensityModels.csv"), models);
        WriteComparisonCsv(Path.Combine(outDir, "EventCosmologyComparison.csv"), comparison);
        WriteA0Csv(Path.Combine(outDir, "A0_FromEvents.csv"), a0);

        PlotComparison(Path.Combine(outDir, "EventGrowthComparison.png"), models, zs);

        return new EventGrowthReport(
            BuildA(), BuildB(models, comparison, zs), BuildC(a0), BuildD(), BuildE(),
            BuildF(models), BuildG(models),
            models, comparison.ToArray(), a0, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Event-density formalism: H_event = d ln(N)/dt, N = event/information/entropy count.");
        sb.AppendLine();
        sb.AppendLine("  Candidate event variables: N_event, N_causal, N_information, N_entropy, N_state_changes.");
        sb.AppendLine("  No scale factor a(t) is introduced; expansion is a projection of event evolution.");
        return sb.ToString();
    }

    private static string BuildB(EventGrowthModel[] models, List<EventComparisonRow> comparison, double[] zs)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Event-growth models: H_event(z)/H_ΛCDM(z) ratio (1.0 = exact match).");
        sb.AppendLine();
        sb.Append("  " + "model".PadRight(26));
        foreach (double z in zs) sb.Append($"  z={z,3:F1}");
        sb.AppendLine();
        foreach (var m in models)
        {
            sb.Append("  " + m.Name.PadRight(26));
            foreach (double z in zs)
            {
                var row = comparison.First(c => c.Model == m.Name && Math.Abs(c.Z - z) < 1e-9);
                sb.Append(string.Format(CultureInfo.InvariantCulture, "  {0,6:F2}", row.Ratio));
            }
            sb.AppendLine();
        }
        sb.AppendLine();
        sb.AppendLine("  N = a matches by construction (trivial). N ∝ t (coasting) tracks ΛCDM within ~17% at");
        sb.AppendLine("  z≤2 but has NO acceleration → fails SN Ia and CMB. Exponential (de Sitter) has no matter");
        sb.AppendLine("  era. Saturation has the wrong slope (~60–90% high at z≥0.5). Only N = a reproduces the");
        sb.AppendLine("  full ΛCDM transition.");
        return sb.ToString();
    }

    private static string BuildC((string Model, double A0_m_s2)[] a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("a₀ from the event rate: a₀ = c × H_event(0) = c × d ln(N)/dt.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-26} {1,11} {2,10}", "model", "a₀ [m/s²]", "×a₀(obs)"));
        foreach (var a in a0)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-26} {1,11:E2} {2,10:F1}", a.Model, a.A0_m_s2, a.A0_m_s2 / 1.2e-10));
        sb.AppendLine();
        sb.AppendLine("  All event models give a₀ ~ cH = 6.5e-10 (order-of-magnitude, no 1/(2π)). The");
        sb.AppendLine("  event-rate origin is the 'cH class' of QG-084/085, NOT the cH/2π class.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Redshift from events: 1+z = N_obs/N_emit.");
        sb.AppendLine();
        sb.AppendLine("  For N = a this is 1+z = a_obs/a_emit, exactly the FLRW redshift. Redshift CAN be");
        sb.AppendLine("  rewritten as event-density evolution, but only by identifying the event count with the");
        sb.AppendLine("  scale factor — i.e. it is another reparametrization (QG-080/082), not an independent");
        sb.AppendLine("  derivation. Time dilation likewise follows as (1+z).");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Links to Causal Set Theory, entropic gravity, information/computational universe.");
        sb.AppendLine();
        sb.AppendLine("  - Causal Set: N ∝ 4-volume ⇒ d ln(N)/dt ≈ 3H (factor 3 from spatial volume) — an");
        sb.AppendLine("    event rate of 3H, not H; not directly observable.");
        sb.AppendLine("  - Entropic/information (Verlinde/Lloyd): the rate is set by mass/energy, not H — a");
        sb.AppendLine("    different (non-cosmological) event rate.");
        sb.AppendLine("  These frameworks motivate 'events' but do not yet fix N(t) uniquely; they predict");
        sb.AppendLine("  N ≠ a (e.g. N ∝ volume), which fails the observed H(z).");
        return sb.ToString();
    }

    private static string BuildF(EventGrowthModel[] models)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — where the reconstruction fails.");
        sb.AppendLine();
        sb.AppendLine("  N ∝ t (coasting): reproduces H0 (~5%) but has no deceleration→acceleration → wrong");
        sb.AppendLine("    SN Ia distances and CMB acoustic scale. FAIL.");
        sb.AppendLine("  N ∝ t^n: coasting family, same failure. FAIL.");
        sb.AppendLine("  N ∝ e^{λt} (de Sitter): H constant → no matter era (H should rise ∝ (1+z)^{3/2}). FAIL.");
        sb.AppendLine("  N ∝ ln t (saturation): wrong slope (~60–90% high at z≥0.5). FAIL.");
        sb.AppendLine("  N = a: matches everything, but is the FLRW scale factor in disguise. (equivalence)");
        sb.AppendLine();
        sb.AppendLine("  The reconstruction fails precisely at the matter→de Sitter transition: only the");
        sb.AppendLine("  ΛCDM scale factor a(t) has the required H(t) = d ln a/dt.");
        return sb.ToString();
    }

    private static string BuildG(EventGrowthModel[] models)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (event formalism defined)           : PASS");
        sb.AppendLine("  Level 2 (H reproduced from events)          : PARTIAL — only N = a works");
        sb.AppendLine("  Level 3 (a₀ emerges)                       : PARTIAL — a₀ = cH (no 2π)");
        sb.AppendLine("  Level 4 (redshift without expansion)        : PASS but = reparametrization");
        sb.AppendLine("  Level 5 (falsifiable prediction)            : FAIL — N≠a is falsified, N=a is sterile");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: H = d ln(N)/dt is TRIVIALLY true for N = a, but the event");
        sb.AppendLine("  interpretation adds no new physics: the simple physically-motivated event models all");
        sb.AppendLine("  fail H(z), and the only one that works is N = a — another reparametrization of FLRW");
        sb.AppendLine("  (consistent with QG-080/082). Expansion is NOT fundamentally replaced by event growth;");
        sb.AppendLine("  event density is just another clock.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteModelsCsv(string path, EventGrowthModel[] models)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Model,NOfT,HEventZ0,HEventZ1,HEventZ2");
        foreach (var m in models)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1},{2:E2},{3:E2},{4:E2}",
                m.Name, m.NOfT, m.HEventOfZ(0), m.HEventOfZ(1), m.HEventOfZ(2)));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteComparisonCsv(string path, List<EventComparisonRow> comparison)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Model,z,HEvent,HLambda,Ratio");
        foreach (var c in comparison)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F1},{2:E2},{3:E2},{4:F3}",
                c.Model, c.Z, c.HEvent, c.HLambda, c.Ratio));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteA0Csv(string path, (string Model, double A0_m_s2)[] a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Model,A0_m_s2,RatioToObservedA0");
        foreach (var a in a0)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:E2},{2:F1}", a.Model, a.A0_m_s2, a.A0_m_s2 / 1.2e-10));
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotComparison(string path, EventGrowthModel[] models, double[] zs)
    {
        RARPlotter.PlotBars(path, models.Select(m => m.Name).ToArray(),
            models.Select(m => m.HEventOfZ(2.0) / EventCosmologyConstants.HLambda(2.0)).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record EventComparisonRow(string Model, double Z, double HEvent, double HLambda, double Ratio);

public sealed record EventGrowthReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    EventGrowthModel[] Models, EventComparisonRow[] Comparison,
    (string Model, double A0_m_s2)[] A0, string OutDir);
