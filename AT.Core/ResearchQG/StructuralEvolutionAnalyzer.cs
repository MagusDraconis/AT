using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-088 Event Structure Cosmology Audit. Tests whether cosmology can be driven by evolving
/// event STRUCTURE (connectivity, complexity, causal density, network dimension) rather than
/// event count or metric expansion. Result: no structural quantity evolves like H (they give
/// 3H, 0, −2.25H, ~0); only S = a (the reparametrization) gives H. Structure is another clock.
/// </summary>
public static class StructuralEvolutionAnalyzer
{
    static readonly Rgb24 Blue = new(30, 100, 220);
    static readonly Rgb24 Red = new(220, 40, 40);
    static readonly Rgb24 Green = new(40, 160, 60);
    static readonly Rgb24 Orange = new(230, 140, 20);

    public static StructuralReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var vars = CausalGraphCosmology.Variables();
        var a0 = StructureAccelerationScale.A0ForEach(vars);

        WriteVariablesCsv(Path.Combine(outDir, "EventStructureModels.csv"), vars);
        WriteRatesCsv(Path.Combine(outDir, "StructuralEvolutionRates.csv"), vars);
        WriteComparisonCsv(Path.Combine(outDir, "EmergentCosmologyComparison.csv"), vars);
        WriteA0Csv(Path.Combine(outDir, "A0_FromStructure.csv"), a0);

        PlotRates(Path.Combine(outDir, "StructuralEvolutionRates.png"), vars);

        return new StructuralReport(
            BuildA(vars),
            BuildB(vars),
            BuildC(vars),
            BuildD(vars),
            BuildE(a0),
            BuildF(),
            BuildG(vars),
            vars, a0, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Structural variables: S_connectivity, S_complexity, S_causal_density, S_information,");
        sb.AppendLine("S_network_dimension. Evolution law: H_S = d ln(S)/dt, with S ∝ a^p ⇒ H_S = p·H.");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-24} {1,-20} {2,8} {3,10}", "variable", "scaling", "p", "H_S/H"));
        foreach (var v in vars)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  {0,-24} {1,-20} {2,8:F2} {3,10:F2}",
                v.Name, v.ScalingWithA, v.PowerP, v.PowerP));
        sb.AppendLine();
        sb.AppendLine("  H_S/H = p. The observed H requires p = 1 (H_S = H). Only S = a has p = 1.");
        return sb.ToString();
    }

    private static string BuildB(StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Toy universes (analytic scaling):");
        sb.AppendLine("  A) Growing random graph (Erdős-Rényi): N∝t, k̄=const, C∝1/N, L∝ln N.");
        sb.AppendLine("  B) Small-world (Watts-Strogatz): high C, short L.");
        sb.AppendLine("  C) Scale-free (Barabási-Albert): P(k)∝k⁻³, C∝N^{-3/4}, diameter∝ln ln N.");
        sb.AppendLine("  D) Causal set: N∝4-volume∝a³, dimension d=4, linking fraction const.");
        sb.AppendLine("  E) Information-flow: edge weights∝entropy∝area.");
        sb.AppendLine();
        sb.AppendLine("  None of their structural quantities has p = 1 (see Section A).");
        return sb.ToString();
    }

    private static string BuildC(StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Structural evolution rates d ln(S)/dt = p·H (H_S/H).");
        sb.AppendLine();
        foreach (var v in vars)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} {1,6:F2}·H  {2}", v.Name, v.PowerP, v.PowerP == 1 ? "← matches H" : ""));
        sb.AppendLine();
        sb.AppendLine("  Only S = a gives p = 1 (d ln S/dt = H). Node/link count give 3H; degree/dimension/");
        sb.AppendLine("  causal density give 0; clustering gives −2.25H. No structural quantity evolves like H.");
        return sb.ToString();
    }

    private static string BuildD(StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Redshift from structure: 1+z = S_obs/S_emit = (1+z)^p.");
        sb.AppendLine();
        foreach (var v in vars)
        {
            double zPred = StructureDrivenRedshift.RedshiftFromStructure(1.0, v.PowerP);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} p={1,5:F2} → 1+z at z=1 = {2,8:F3} {3}",
                v.Name, v.PowerP, 1.0 + zPred,
                StructureDrivenRedshift.ReproducesRedshift(v.PowerP) ? "(= FLRW)" : "(≠ FLRW)"));
        }
        sb.AppendLine();
        sb.AppendLine("  Only p = 1 reproduces 1+z; p = 3 (node count) gives (1+z)³, excluded.");
        return sb.ToString();
    }

    private static string BuildE((string Name, double A0_m_s2)[] a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("a₀ from structure: a₀ = c × d ln(S)/dt = c·p·H.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-24} {1,11} {2,10}", "variable", "a₀ [m/s²]", "×a₀(obs)"));
        foreach (var a in a0)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} {1,11:E2} {2,10:F1}", a.Name, a.A0_m_s2, a.A0_m_s2 / 1.2e-10));
        sb.AppendLine();
        sb.AppendLine("  All give a₀ ~ cH (or 3cH) — the 'cH class', no 1/(2π). Order only (QG-084/087).");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Links to Causal Set Theory, Quantum Graphity, Holographic Networks, Computational");
        sb.AppendLine("Universe, Emergent Space-Time.");
        sb.AppendLine();
        sb.AppendLine("  - Causal set: dimension d=4 is FIXED (not evolving); N ∝ a³ gives 3H, not H.");
        sb.AppendLine("  - Quantum Graphity: a fixed lattice; structure is static, not evolving.");
        sb.AppendLine("  - Holographic: entropy ∝ area → structural rate tied to area, not H.");
        sb.AppendLine("  These frameworks motivate STRUCTURE, but their structural quantities do not evolve");
        sb.AppendLine("  like H — so structure does not replace expansion; it reparametrizes it (S = a).");
        return sb.ToString();
    }

    private static string BuildG(StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (structural variables defined)         : PASS");
        sb.AppendLine("  Level 2 (structure-driven H)                   : FAIL — no S gives H (only S=a)");
        sb.AppendLine("  Level 3 (a₀ emerges)                          : PARTIAL — a₀ = cH (no 2π)");
        sb.AppendLine("  Level 4 (redshift without expansion)           : PASS but = reparametrization");
        sb.AppendLine("  Level 5 (falsifiable prediction)               : FAIL — no distinct prediction");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: the true cosmological degree of freedom is NOT structure.");
        sb.AppendLine("  Every structural variable S(t) either collapses to S = a (the reparametrization) or");
        sb.AppendLine("  gives the wrong H(z) (node count 3H, degree 0, clustering −2.25H). The causal-information");
        sb.AppendLine("  network picture is rich but observationally equivalent to FLRW (or falsified), consistent");
        sb.AppendLine("  with the whole QG-080–087 reinterpretation program.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteVariablesCsv(string path, StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Variable,ScalingWithA,PowerP,SourceNetwork");
        foreach (var v in vars)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1},{2:F2},{3}", v.Name, Escape(v.ScalingWithA), v.PowerP, v.SourceNetwork));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRatesCsv(string path, StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Variable,EvolutionRateInUnitsOfH");
        foreach (var v in vars)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F2}", v.Name, v.PowerP));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteComparisonCsv(string path, StructuralVariable[] vars)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Variable,PowerP,MatchesH,RedshiftAtZ1");
        foreach (var v in vars)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F2},{2},{3:F3}",
                v.Name, v.PowerP, Math.Abs(v.PowerP - 1) < 1e-3 ? "1" : "0",
                1.0 + StructureDrivenRedshift.RedshiftFromStructure(1.0, v.PowerP)));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteA0Csv(string path, (string Name, double A0_m_s2)[] a0)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Variable,A0_m_s2,RatioToObservedA0");
        foreach (var a in a0)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:E2},{2:F1}", a.Name, a.A0_m_s2, a.A0_m_s2 / 1.2e-10));
        File.WriteAllText(path, sb.ToString());
    }

    private static void PlotRates(string path, StructuralVariable[] vars)
    {
        RARPlotter.PlotBars(path, vars.Select(v => v.Name).ToArray(),
            vars.Select(v => Math.Abs(v.PowerP)).ToArray(), Blue);
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record StructuralReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    StructuralVariable[] Variables, (string Name, double A0_m_s2)[] A0, string OutDir);
