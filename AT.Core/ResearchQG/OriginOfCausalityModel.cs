namespace AT.Core.ResearchQG;

/// <summary>QG-092 origin of causality: the irreducible axioms of a causal partial order and
/// whether each can be derived from a deeper primitive.</summary>
public sealed record CausalityAxiom(string Name, string Statement, string Status, string Derivation);

public static class OriginOfCausalityModel
{
    /// <summary>Axioms of a causal partial order (≺), with derivability status.</summary>
    public static CausalityAxiom[] Axioms() => new[]
    {
        new CausalityAxiom("Transitivity", "A≺B ∧ B≺C ⇒ A≺C",
            "Axiom (but follows from consistency: an intransitive order admits paradoxes)", "derivable from consistency"),
        new CausalityAxiom("Antisymmetry", "A≺B ⇒ ¬(B≺A)",
            "Axiom (no closed cycles)", "derivable from consistency (no time loops)"),
        new CausalityAxiom("Acyclicity", "no chain A≺...≺A",
            "Axiom", "equivalent to antisymmetry on chains"),
        new CausalityAxiom("Local finiteness", "finitely many elements between A and B",
            "Axiom (physical discreteness)", "postulated (causal sets)"),
    };

    /// <summary>Candidate deeper primitives and whether they can reconstruct causality without
    /// already assuming it.</summary>
    public static (string Candidate, string CanReconstruct, string Circularity)[] DeeperPrimitives() => new[]
    {
        ("Information", "NO — 'information' already presupposes distinctions (a relation)",
            "distinctions are relations; order must be assumed"),
        ("Computation", "NO — computation is a causal sequence of steps",
            "computation needs a before/after"),
        ("Logic", "NO — implication (→) is itself a partial order",
            "logic already contains →"),
        ("Consistency", "PARTIAL — rules out cycles but not the order itself",
            "consistency forbids paradoxes, does not generate order"),
        ("Mathematical relations", "NO — a relation IS the primitive being derived",
            "circular: relation to explain relation"),
        ("Quantum correlations", "PARTIAL — entanglement gives correlations, not order",
            "order (causal) vs correlation (acausal) distinct"),
    };
}
