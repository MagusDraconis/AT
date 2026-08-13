namespace TQM.Core.ResearchQG;

/// <summary>QG-088 causal-graph cosmology: the toy network universes and their structural
/// scaling relations (analytic, from well-known random-graph / small-world / scale-free /
/// causal-set results).</summary>
public static class CausalGraphCosmology
{
    /// <summary>Structural variables with their scale-factor scaling S ∝ a^p.</summary>
    public static StructuralVariable[] Variables() => new[]
    {
        // Causal set: N ∝ 4-volume ∝ a³ (per unit time) → p = 3.
        new StructuralVariable("node count N", "a³ (4-volume)", 3.0, "causal set"),
        new StructuralVariable("link count E", "a³ (E ∝ N)", 3.0, "causal set"),
        // Average degree k̄ = 2E/N = const → p = 0.
        new StructuralVariable("average degree k̄", "const", 0.0, "all growing graphs"),
        // Scale-free clustering C ∝ N^{-3/4} ∝ a^{-9/4} → p = -9/4.
        new StructuralVariable("clustering C", "N^-3/4 = a^-9/4", -9.0 / 4.0, "scale-free (BA)"),
        // Path length L ∝ ln N ∝ ln a → d ln L/dt ≈ 0 (p ≈ 0).
        new StructuralVariable("path length L", "ln a", 0.0, "small-world"),
        // Network dimension d = 4 = const → p = 0.
        new StructuralVariable("network dimension d", "const (d=4)", 0.0, "causal set"),
        // Causal density (linking fraction) = const → p = 0.
        new StructuralVariable("causal density ρ_c", "const", 0.0, "causal set"),
        // The scale factor itself (the reparametrization) → p = 1.
        new StructuralVariable("scale factor S = a", "a", 1.0, "(reparametrization)"),
    };
}
