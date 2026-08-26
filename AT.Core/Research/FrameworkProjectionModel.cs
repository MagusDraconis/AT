namespace AT.Core.Research;

/// <summary>
/// Models how each framework projects from the deeper theory.
/// AT-X033: Emergence Gap Audit
/// </summary>
public static class FrameworkProjectionModel
{
    public static ConceptMappingMatrix.FrameworkProjection MainAT => new(
        Framework: "Main AT (117-154)",
        StartingQuestion: "\"What is the mathematical structure of persistent reality?\"",
        MathematicalCore: "L_Q = graph Laplacian → Hilbert → J → i → Schrödinger",
        PrimaryDiscovery: "Quantum mechanics emerges from Q + reversibility",
        BlindSpot: "Complexity hierarchy — Main AT never asked how many levels exist",
        NaturalConcepts: new[]
        {
            "L_Q (explicit operator form)",
            "Hilbert space (natural from L_Q eigenbasis)",
            "Schrödinger equation (derived via J → i)",
            "Born rule (Gleason, measurement boundary)",
            "Species = eigenmodes (natural from spectral decomposition)"
        },
        HiddenConcepts: new[]
        {
            "Complexity staircase (L0-L6) — present implicitly in species hierarchy but never formalized",
            "Finite/infinite boundary — never asked; always assumed finite graph",
            "Quantum necessity — quantum mechanics emerged but necessity wasn't proven",
            "Self-consistency as explicit principle — present as eigenmode condition L·v=λv"
        });

    public static ConceptMappingMatrix.FrameworkProjection ResearchX => new(
        Framework: "ResearchX (X001-X031)",
        StartingQuestion: "\"What are the minimal conditions for complex, evolving reality?\"",
        MathematicalCore: "R (reversibility) + S (self-consistency F(x)=x) → reality structures",
        PrimaryDiscovery: "Quantum reality (R=1,S=1) is the necessary optimum for finite complexity",
        BlindSpot: "Explicit operator form — ResearchX never derives L_Q or the specific Hamiltonian",
        NaturalConcepts: new[]
        {
            "R+S as independent foundations (X011, X014)",
            "Complexity staircase L0-L6 (X018)",
            "Carrier class taxonomy (X008)",
            "Finite → saturation proof (X027)",
            "Quantum necessity (X031) — proves QM is forced, not accidental"
        },
        HiddenConcepts: new[]
        {
            "L_Q explicit form — (R,S) is operator-independent; any operator satisfying both works",
            "J, i, specific Hamiltonian — not needed; ResearchX works at the principle level",
            "Eigenmode computation — species are identified by stability, not diagonalization"
        });

    public static (ConceptMappingMatrix.FrameworkProjection main, ConceptMappingMatrix.FrameworkProjection researchX) Both()
        => (MainAT, ResearchX);
}
