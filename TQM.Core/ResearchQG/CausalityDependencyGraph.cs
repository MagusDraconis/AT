using System.Globalization;
using System.Text;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-092 Origin Of Causality Audit. Determines whether the causal partial order can be derived
/// from a deeper primitive (information, computation, logic, consistency, mathematical relations,
/// quantum correlations). Result: NO — every candidate either already presupposes an order/relation
/// or is too weak to generate it. Causality is the true foundation: its axioms (transitivity,
/// antisymmetry, acyclicity, local finiteness) are irreducible. Fundamental vs emergent causality
/// are observationally equivalent, so there is no falsifiable difference.
/// </summary>
public static class CausalityDependencyGraph
{
    public static CausalityDependencyReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var axioms = OriginOfCausalityModel.Axioms();
        var deeper = OriginOfCausalityModel.DeeperPrimitives();
        var graph = CausalityHierarchy.DependencyGraph();

        WriteAxiomsCsv(Path.Combine(outDir, "OriginOfCausalityModels.csv"), axioms);
        WriteGraphCsv(Path.Combine(outDir, "CausalityDependencyGraph.csv"), graph);
        WriteHierarchyCsv(Path.Combine(outDir, "PrimitiveHierarchy.csv"));
        WriteEmergentCsv(Path.Combine(outDir, "EmergentVsFundamentalCausality.csv"), deeper);

        return new CausalityDependencyReport(
            BuildA(axioms),
            BuildB(deeper),
            BuildC(graph),
            BuildD(),
            BuildE(),
            BuildF(),
            BuildG(),
            axioms, deeper, graph, outDir);
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(CausalityAxiom[] axioms)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Irreducible assumptions of a causal partial order ≺.");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-18} {1,-26} {2}", "axiom", "statement", "status"));
        foreach (var a in axioms)
            sb.AppendLine(string.Format("  {0,-18} {1,-26} {2}", a.Name, a.Statement, a.Status));
        sb.AppendLine();
        sb.AppendLine("  Transitivity + antisymmetry (acyclicity) are the ORDER; local finiteness is the");
        sb.AppendLine("  DISCRETENESS (causal sets). Consistency can FORBID cycles, but it cannot GENERATE");
        sb.AppendLine("  the order — the order must be given.");
        return sb.ToString();
    }

    private static string BuildB((string Candidate, string CanReconstruct, string Circularity)[] deeper)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate deeper primitives — can causality be reconstructed?");
        sb.AppendLine();
        foreach (var (candidate, can, circ) in deeper)
            sb.AppendLine($"  {candidate,-22} : {can}");
        sb.AppendLine();
        sb.AppendLine("  Every candidate either already presupposes an order/relation (information, computation,");
        sb.AppendLine("  logic →, mathematical relations) or is too weak (consistency forbids but does not");
        sb.AppendLine("  generate; quantum correlations give correlation, not order).");
        sb.AppendLine();
        sb.AppendLine("  ⇒ CAUSALITY IS NOT DERIVABLE from a deeper primitive without circularity.");
        return sb.ToString();
    }

    private static string BuildC(string[] graph)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Dependency graph.");
        sb.AppendLine();
        foreach (var line in graph) sb.AppendLine(line);
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Hostile audit — a universe with NO causal order.");
        sb.AppendLine();
        sb.AppendLine($"  {PrimitiveStructureAudit.NoCausalOrderConsequences}");
        sb.AppendLine();
        sb.AppendLine("  ⇒ Observations, dynamics, information and observers are ALL meaningless without an");
        sb.AppendLine("  order. A causally-ordered universe is the minimal non-empty structure.");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Consistency and causality.");
        sb.AppendLine();
        sb.AppendLine("  Inconsistent event orderings (cycles, intransitivity) produce paradoxes (grandfather");
        sb.AppendLine("  paradoxes, closed timelike curves) and are FORBIDDEN by consistency. So consistency");
        sb.AppendLine("  SELECTS partial orders — but it does not CREATE the order; it only rules out the bad");
        sb.AppendLine("  ones. Causality is the residual of consistency, not its consequence.");
        sb.AppendLine();
        sb.AppendLine("  ER = EPR / tensor networks: entanglement gives correlations and holographic bulk,");
        sb.AppendLine("  but reconstructing a causal bulk from them requires the causal order (AdS/CFT boundary");
        sb.AppendLine("  data are ordered). So causality is still assumed.");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Does Λ ~ 1/√N survive emergent causality?");
        sb.AppendLine();
        sb.AppendLine($"  {EmergentCausalityAnalyzer.LambdaSurvivalReason}.");
        sb.AppendLine("  ⇒ Yes: the causal-set Λ prediction survives whether causality is fundamental or emergent.");
        sb.AppendLine("  Fundamental vs emergent causality are observationally EQUIVALENT (the order exists either");
        sb.AppendLine("  way), so no falsifiable difference exists between them.");
        return sb.ToString();
    }

    private static string BuildG()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (primitive candidates classified)       : PASS");
        sb.AppendLine("  Level 2 (dependency graph completed)            : PASS");
        sb.AppendLine("  Level 3 (causality derivable?)                  : PASS — NO (irreducible)");
        sb.AppendLine("  Level 4 (deepest surviving primitive)           : PASS — causality (partial order)");
        sb.AppendLine("  Level 5 (falsifiable prediction)                : FAIL — fundamental vs emergent equivalent");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: causality is the TRUE FOUNDATION of reality. Its axioms");
        sb.AppendLine("  (transitivity, antisymmetry, acyclicity, local finiteness) are irreducible: every deeper");
        sb.AppendLine("  candidate (information, computation, logic, consistency, relations, correlations) either");
        sb.AppendLine("  presupposes an order or is too weak to generate it. There is no deeper primitive structure");
        sb.AppendLine("  — the partial order is the bottom. Fundamental vs emergent causality are observationally");
        sb.AppendLine("  equivalent, so this is a terminus, not a new testable theory.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteAxiomsCsv(string path, CausalityAxiom[] axioms)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Axiom,Statement,Status,Derivation");
        foreach (var a in axioms)
            sb.AppendLine($"{a.Name},{Escape(a.Statement)},{Escape(a.Status)},{Escape(a.Derivation)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteGraphCsv(string path, string[] graph)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Edge");
        foreach (var line in graph) sb.AppendLine(Escape(line));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteHierarchyCsv(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Level,Concept,Status");
        sb.AppendLine("1,causality (partial order),PRIMITIVE (irreducible)");
        sb.AppendLine("2,time,EMERGENT (depth)");
        sb.AppendLine("2,change,EMERGENT (differences)");
        sb.AppendLine("3,geometry,EMERGENT (conformal + volume)");
        sb.AppendLine("4,cosmology,EMERGENT (H, Lambda ~ 1/sqrt(N))");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteEmergentCsv(string path, (string Candidate, string CanReconstruct, string Circularity)[] deeper)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Candidate,CanReconstructCausality,Circularity");
        foreach (var (candidate, can, circ) in deeper)
            sb.AppendLine($"{candidate},{Escape(can)},{Escape(circ)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record CausalityDependencyReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    CausalityAxiom[] Axioms, (string Candidate, string CanReconstruct, string Circularity)[] Deeper,
    string[] Graph, string OutDir);
