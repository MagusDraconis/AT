namespace TQM.Core.Research;

/// <summary>
/// Evaluates alternative mathematical foundations for TQM
/// and determines which framework components survive.
///
/// TQM-X001: Alternative Foundations Audit
/// </summary>
public static class AlternativeFoundation
{
    public static List<AssumptionRegistry.AlternativeOperator> EvaluateAlternatives()
    {
        return new List<AssumptionRegistry.AlternativeOperator>
        {
            new("Normalized Laplacian", "L_norm = I - D^(-1/2)·A·D^(-1/2)",
                "Eigenvalues in [0,2], scale-invariant",
                true, "Scaling of λ_1 changes (no longer ∝1/Q²)", "Species still exist as eigenmodes"),

            new("Signless Laplacian", "L_abs = D + A",
                "All eigenvalues positive, no zero mode",
                false, "No zero eigenvalue → no uniform mode → species A fails", "Spectral structure partially preserved"),

            new("Directed Laplacian", "L_dir = D_out - A (asymmetric)",
                "Complex eigenvalues possible, non-orthogonal eigenvectors",
                false, "L_Q symmetric is fundamental — complex spectrum changes everything", "None of Schrodinger derivation survives (needs symmetric L_Q)"),

            new("Magnetic Laplacian", "L_mag = D - A∘exp(iθ_ij)",
                "Complex entries, Peierls substitution, Hofstadter butterfly",
                true, "Adds magnetic field → new phenomena", "Spectrum enriched, species become complex-valued"),

            new("Hypergraph Laplacian", "L_hyper for 3-body+ interactions",
                "Higher-order interactions, beyond pairwise",
                true, "Pairwise assumption relaxed → richer species", "Framework generalizes; new species from 3-body terms"),

            new("Fractional Laplacian", "L^α for α∈(0,1)",
                "Nonlocal operator, Lévy flights, anomalous diffusion",
                false, "Scaling m_eff∝Q² changes to m_eff∝Q^(2α)", "Spectral structure changes, species count may differ"),

            new("Nonlinear Operator", "L(ψ) = L_Q + β·diag(|ψ|²)",
                "State-dependent, Gross-Pitaevskii-like",
                true, "Linear superposition fails → no Hilbert space", "New phenomena: solitons, nonlinear eigenmodes, possibly open-ended innovation"),
        };
    }
}
