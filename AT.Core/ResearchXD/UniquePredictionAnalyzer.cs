namespace AT.Core.ResearchXD;

/// <summary>
/// Catalogs unique predictions of zero-parameter AT.
/// ResearchXD-001: Unique Predictions
/// </summary>
public static class UniquePredictionAnalyzer
{
    public enum Falsifiability { AlreadyRuledOut, TestableNow, TestableWithin5yr, TestableWithin10yr, Untestable }

    public sealed record ATPrediction(
        string Name, string Statement, string StandardModelPrediction,
        bool IsUnique, Falsifiability Falsifiability,
        string Experiment, string KillShot);

    public static List<ATPrediction> CatalogPredictions()
    {
        return new List<ATPrediction>
        {
            new("Time-varying dark energy w(z) ≠ -1",
                "Λ(t) = α/√V(t) → w(z) ≈ -1 + 0.015·(1+z)^(3/2).\n"
                + "Deviation ~1-3% at z~0.5-2.",
                "ΛCDM: w = -1 (exact constant).",
                true, Falsifiability.TestableWithin5yr,
                "Euclid (2024+), Roman (2027+), DESI",
                "Euclid measures w = -1.00 ± 0.01 → AT FALSIFIED at >3σ."),

            new("Derived acceleration scale a₀ ≈ cH₀",
                "a₀ = c²/ξ_cosmo = c·H₀/(2π) ≈ 10⁻¹⁰ m/s².\n"
                + "Matches MOND scale from Λ (X046).",
                "ΛCDM: a₀ is a coincidence (no derivation).",
                true, Falsifiability.TestableNow,
                "SPARC galaxy rotation curves",
                "If a₀ is shown to vary with cosmic time (not follow H₀), AT's Λ→a₀ link is broken."),

            new("DM = neutral topological defects (~TeV)",
                "Dark matter = stable neutral vortices/moduli excitations.\n"
                + "Mass ~TeV. No U(1) charge. Collisionless.",
                "ΛCDM: DM is a new particle (WIMP, axion, etc.).",
                false, Falsifiability.TestableWithin10yr,
                "Direct detection (XENON, LZ), indirect (Fermi, CTA)",
                "Null result from multi-ton direct detection + LHC excludes WIMP window → TeV-scale DM models constrained."),

            new("No spacetime singularities",
                "Maximum curvature = 1/ℓ_P². Black holes have regular cores.\n"
                + "Hawking radiation modified at Planck temperatures.",
                "GR: Schwarzschild singularity at r=0.",
                true, Falsifiability.Untestable,
                "None (Planck-scale effects).",
                "Untestable for astrophysical BHs. Requires primordial BHs at Planck mass."),

            new("Log-normal abundance distributions",
                "ALL abundance quantities (α, m_e, Ω_DM, ...) are log-normal\n"
                + "draws from multiplicative actualization cascades (XB002).",
                "Standard Model: parameters are fixed constants.",
                true, Falsifiability.TestableWithin10yr,
                "Universe ensemble: compare α in distant galaxies (VLT/ELT)",
                "If α is shown to be CONSTANT to higher precision than log-normal width allows → AT's statistical interpretation constrained."),

            new("Correlation-induced MOND-like gravity",
                "Galaxy rotation curves flattened by correlation geometry\n"
                + "without particle DM at galaxy scales (X063).",
                "ΛCDM: DM halos (NFW profile) explain rotation curves.",
                false, Falsifiability.TestableNow,
                "SPARC, weak lensing (Euclid, Rubin)",
                "If correlation gravity fails to fit diverse rotation curves better than NFW → model constrained."),

            new("M² = ⟨k⟩ ≈ 5 (connectivity prediction)",
                "Nonlinearity M² ≈ 5 is the average causal degree\n"
                + "in 3+1D. Depends only on dimensionality (XC003-5).",
                "Standard Model: no prediction for M².",
                true, Falsifiability.Untestable,
                "Indirect: mass hierarchy steepness",
                "Hard to falsify directly. Would require measuring ⟨k⟩ in causal set structure."),

            new("Neutrino normal ordering (m₁<m₂<m₃)",
                "Attractive self-interaction → higher excitations\n"
                + "slightly more localized → heavier (X060).",
                "Standard Model: ordering is a free parameter.",
                false, Falsifiability.TestableWithin10yr,
                "JUNO, DUNE, Hyper-K",
                "Inverted ordering confirmed at >5σ → Model A (X060) wrong."),
        };
    }

    public static string FalsificationRanking(List<ATPrediction> predictions)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FALSIFICATION RANKING — Fastest AT Kill Shots");
        sb.AppendLine();
        sb.AppendLine("  Rank  Prediction                           Unique?  Testable    Kill Shot");
        sb.AppendLine("  " + new string('-', 85));

        var ranked = predictions
            .OrderBy(p => p.Falsifiability)
            .ThenByDescending(p => p.IsUnique)
            .ToList();

        for (int i = 0; i < ranked.Count; i++)
        {
            var p = ranked[i];
            string uniq = p.IsUnique ? "✓ AT" : "~ shared";
            string test = p.Falsifiability switch
            {
                Falsifiability.TestableNow => "NOW",
                Falsifiability.TestableWithin5yr => "≤5yr",
                Falsifiability.TestableWithin10yr => "≤10yr",
                _ => "future"
            };
            sb.AppendLine($"  {i + 1,3}. {p.Name,-37} {uniq,-7}  {test,-6}  {p.KillShot.Split('\n')[0]}");
        }
        return sb.ToString();
    }

    public static string TheFalsificationTree()
    {
        return @"
AT FALSIFICATION TREE — DECISION FLOW

┌─────────────────────────────────────────────┐
│  EXPERIMENT 1: Euclid measures w(z)         │
│  ┌─ w = -1.00 ± 0.01 → AT FALSIFIED        │
│  └─ w ≠ -1 at >3σ → AT SURVIVES            │
├─────────────────────────────────────────────┤
│  EXPERIMENT 2: JUNO/DUNE neutrino ordering  │
│  ┌─ INVERTED at >5σ → Model A wrong         │
│  └─ NORMAL → AT consistent                  │
├─────────────────────────────────────────────┤
│  EXPERIMENT 3: SPARC rotation curves         │
│  ┌─ a₀ does NOT track H₀ → Λ→a₀ link broken │
│  └─ a₀ ∝ H₀ → AT a₀ derivation holds       │
├─────────────────────────────────────────────┤
│  EXPERIMENT 4: Direct DM detection           │
│  ┌─ WIMP detected (not defect) → AT DM wrong│
│  └─ Null → AT consistent (no WIMP miracle)  │
└─────────────────────────────────────────────┘

FASTEST KILL: Euclid w(z) measurement (by 2030).
DEEPEST KILL: Neutrino ordering (by 2035).
HARDEST KILL: Direct DM detection (defect ≠ WIMP).
";
    }
}
