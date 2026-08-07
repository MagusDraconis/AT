namespace TQM.Core.Research;

/// <summary>
/// Evaluates species→graph feedback mechanisms for niche construction.
/// TQM-X020: Niche Construction Principle
/// </summary>
public static class SpeciesGraphFeedback
{
    public static List<NicheConstructionMetrics.FeedbackResult> EvaluateMechanisms()
    {
        return new List<NicheConstructionMetrics.FeedbackResult>
        {
            new(
                "Local density → edge creation",
                false, false, false,
                "Same operator type (graph Laplacian). Changing edge count doesn't change the operator CLASS — just its spectrum.",
                "Increases SPECIES diversity (more eigenmodes) but not CARRIER CLASS diversity. Same sinusoidal family."
            ),

            new(
                "Local density → edge removal",
                false, false, false,
                "Graph fragmentation can create DISCONNECTED components, each with its own eigenmode family. But modes are still sinusoidal.",
                "Creates ISOLATED subgraphs with independent spectra — same carrier class, different instances."
            ),

            new(
                "Species → new node creation",
                false, false, false,
                "Node addition = trivial spectrum expansion (X004). More eigenmodes, same type.",
                "Same as X004: more sinusoidal modes, no new carrier classes."
            ),

            new(
                "Species → coupling strength modulation",
                false, false, false,
                "Changing J_ij changes eigenvalue VALUES but not eigenvector TYPES. Spectrum shifts, not restructures.",
                "Rescaling eigenvalues doesn't create new mode families."
            ),

            new(
                "Species → topological defect creation",
                true, true, false,
                "Topological defects (vortices, domain walls) ARE new carrier classes! But they're created by the SPECIES activity, not by graph modification per se.",
                "PROMISING: Topological defects ARE qualitatively new carriers. But saturation expected — finite number of distinct topological sectors."
            ),

            new(
                "Species → dimension change (1D→2D)",
                true, true, true,
                "Changing graph dimension fundamentally changes the operator type. 1D sinusoidal → 2D sinusoidal → different mode families. Potentially unbounded if dimensions can keep increasing.",
                "MOST PROMISING: Dimensional expansion creates genuinely new carrier classes. But requires mechanism for dimension increase."
            ),

            new(
                "Species → operator type change (L_Q→magnetic L)",
                true, true, true,
                "Changing the operator TYPE (graph Laplacian → magnetic Laplacian) creates new carrier classes (Landau levels). Potentially unbounded if operator space is infinite.",
                "THEORETICALLY PROMISING: Operator space may be unbounded. But no physical mechanism for species to change the fundamental operator."
            ),
        };
    }
}
