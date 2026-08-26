using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-081 Model Dependence Audit. Separates direct observations from FLRW-dependent
/// inferences, enumerates where the expanding-metric assumption is introduced, builds a
/// dependency graph (redshift → a(t) → H(z) → ΩΛ → dark energy), and evaluates whether a
/// "static space + evolving time" reconstruction (QG-080) changes any accepted conclusion.
/// </summary>
public static class ModelDependenceAnalyzer
{
    public static ModelDependenceReport Run(string outDir)
    {
        Directory.CreateDirectory(outDir);

        var observables = Observables();
        var inferences = Inferences();
        var assumptions = AssumptionMap();

        WriteObservablesCsv(Path.Combine(outDir, "Observables_vs_Inferences.csv"), observables, inferences);
        WriteAssumptionsCsv(Path.Combine(outDir, "FLRW_Assumption_Map.csv"), assumptions);

        return new ModelDependenceReport(
            BuildA(observables, inferences),
            BuildB(),
            BuildC(assumptions),
            BuildD(),
            BuildE(),
            BuildF(),
            BuildG(),
            observables, inferences, assumptions, outDir);
    }

    // ---------------------------------------------------------------------
    // Data
    // ---------------------------------------------------------------------

    public sealed record ObservabilityRow(string Quantity, string Type, bool RequiresFLRW, string Probe, string Notes);

    public sealed record AssumptionRow(string DerivedQuantity, string AssumptionChain, string FirstFLRWIntroduction);

    private static ObservabilityRow[] Observables() => new[]
    {
        new ObservabilityRow("redshift z", "Observable", false, "all", "frequency ratio of spectral features"),
        new ObservabilityRow("apparent flux / magnitude", "Observable", false, "all", "energy per area per time"),
        new ObservabilityRow("angular size θ", "Observable", false, "all", "measured sky angle"),
        new ObservabilityRow("time dilation (SN stretch)", "Observable", false, "SN Ia", "light-curve width ∝ (1+z)^b, b=1 measured"),
        new ObservabilityRow("CMB temperature T0", "Observable", false, "CMB", "2.725 K blackbody"),
        new ObservabilityRow("CMB angular spectrum C_l", "Observable", false, "CMB", "peak positions (raw multipoles)"),
        new ObservabilityRow("surface brightness", "Observable", false, "all", "flux / solid angle"),
        new ObservabilityRow("source counts N(S)", "Observable", false, "surveys", "number per flux bin"),
        new ObservabilityRow("line ratios / abundances", "Observable", false, "spectra", "physics of emitting gas"),
    };

    private static ObservabilityRow[] Inferences() => new[]
    {
        new ObservabilityRow("scale factor a(t)", "Inferred", true, "all", "a = 1/(1+z) under FLRW"),
        new ObservabilityRow("H(z)", "Inferred", true, "all", "chronometers nearly free; BAO/SNe assume FLRW"),
        new ObservabilityRow("luminosity distance D_L", "Inferred", true, "SN Ia", "D_L = (1+z)·χ"),
        new ObservabilityRow("angular diameter distance D_A", "Inferred", true, "BAO/CMB", "D_A = χ/(1+z)"),
        new ObservabilityRow("comoving distance χ", "Inferred", true, "all", "χ = ∫ c dz / H(z)"),
        new ObservabilityRow("sound horizon r_s", "Inferred", true, "BAO/CMB", "early-universe + recombination model"),
        new ObservabilityRow("Ωm, ΩΛ", "Inferred", true, "all", "Friedmann-equation fit"),
        new ObservabilityRow("dark energy density", "Inferred", true, "SN Ia", "the ΩΛ term itself"),
        new ObservabilityRow("proper distance", "Inferred", true, "all", "a(t)·χ"),
        new ObservabilityRow("comoving volume", "Inferred", true, "surveys", "dV = χ²dχ/H(z)"),
    };

    private static AssumptionRow[] AssumptionMap() => new[]
    {
        new AssumptionRow("flux → luminosity", "F = L/(4π D_L²)", "metric surface area via D_L"),
        new AssumptionRow("magnitude → D_L(z)", "m − M = 5 log₁₀(D_L/10 pc)", "D_L = (1+z)·χ (comoving χ)"),
        new AssumptionRow("angular size → D_A(z)", "θ = s/D_A", "D_A = χ/(1+z)"),
        new AssumptionRow("D_A + r_s → H(z)", "θ_BAO = r_s/D_A", "r_s (early universe) + comoving χ"),
        new AssumptionRow("D_L(z) → ΩΛ", "χ² fit to Friedmann D_L", "Friedmann eqn + ΩΛ functional form"),
        new AssumptionRow("redshift → a(t)", "1+z = a0/a", "a(t) exists (metric expansion)"),
        new AssumptionRow("growth → fσ8", "δ̈ + 2H δ̇ = 4πG ρ δ", "FLRW background H(t)"),
    };

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(ObservabilityRow[] obs, ObservabilityRow[] inf)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Column A = direct observables; Column B = FLRW-derived quantities.");
        sb.AppendLine();
        sb.AppendLine("  DIRECTLY OBSERVED (model-light):");
        foreach (var o in obs) sb.AppendLine($"    - {o.Quantity,-32} [{o.Probe}]");
        sb.AppendLine();
        sb.AppendLine("  INFERRED (require FLRW geometry):");
        foreach (var i in inf) sb.AppendLine($"    - {i.Quantity,-32} [{i.Probe}]");
        return sb.ToString();
    }

    private static string BuildB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Probe-by-probe audit of what is measured vs assumed.");
        sb.AppendLine();
        sb.AppendLine("  SN Ia:   MEASURED z, flux, color, light-curve stretch (time dilation b=1).");
        sb.AppendLine("           ASSUMED FLRW to turn flux into D_L(z), then Friedmann to get Ωm/ΩΛ.");
        sb.AppendLine("  BAO:     MEASURED angular correlation scale θ and radial Δz scale.");
        sb.AppendLine("           ASSUMED FLRW (D_A = χ/(1+z)) + an early-universe r_s to get H(z).");
        sb.AppendLine("  CMB:     MEASURED T0, blackbody spectrum, angular power spectrum C_l.");
        sb.AppendLine("           ASSUMED FLRW + perturbations + recombination to get θ* = r_s/D_A, Ωm h².");
        sb.AppendLine("  Chronometers: MEASURED Δt (galaxy ages) and Δz. H(z) = −1/(1+z) dz/dt is the");
        sb.AppendLine("           LEAST model-dependent H(z) — no metric, only stellar-population ages.");
        sb.AppendLine("  Surveys: MEASURED positions, redshifts, number counts, clustering.");
        sb.AppendLine("           ASSUMED FLRW (comoving volume, growth eqn) for P(k), fσ8, matter density.");
        return sb.ToString();
    }

    private static string BuildC(AssumptionRow[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Where the expanding metric is introduced (hidden FLRW assumptions).");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-24} {1,-26} {2}", "derived", "assumption chain", "first FLRW step"));
        foreach (var a in assumptions)
            sb.AppendLine(string.Format("  {0,-24} {1,-26} {2}", a.DerivedQuantity, a.AssumptionChain, a.FirstFLRWIntroduction));
        sb.AppendLine();
        sb.AppendLine("  KEY: expansion enters at (1) D_L = (1+z)χ, (2) D_A = χ/(1+z), (3) the Friedmann");
        sb.AppendLine("  equation for ΩΛ. Redshift, flux, angular size and time dilation themselves are");
        sb.AppendLine("  expansion-free; only their CONVERSION to distances needs the metric.");
        return sb.ToString();
    }

    private static string BuildD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Dependency graph (every assumption step explicit).");
        sb.AppendLine();
        sb.AppendLine("  redshift z (observed)");
        sb.AppendLine("    └─ [assume FLRW: 1+z = a0/a] ──> scale factor a(t)");
        sb.AppendLine("         └─ [assume H = ȧ/a] ──> expansion history H(z)");
        sb.AppendLine("              └─ [assume D_L = (1+z)∫dz/H] ──> luminosity distance");
        sb.AppendLine("                   └─ [assume Friedmann eqn] ──> Ωm, ΩΛ");
        sb.AppendLine("                        └─ [ΩΛ ≠ 0] ──> 'dark energy'");
        sb.AppendLine();
        sb.AppendLine("  time dilation (observed, b=1)");
        sb.AppendLine("    └─ consistent with FLRW AND with weak TSC (γ=a); excludes strong TSC (b=−1).");
        sb.AppendLine();
        sb.AppendLine("  angular size (observed)");
        sb.AppendLine("    └─ [assume D_A = χ/(1+z) + r_s] ──> BAO/CMB distances → H(z).");
        sb.AppendLine();
        sb.AppendLine("  The 'dark energy' conclusion is the DEEPEST inference (4 nested assumptions).");
        return sb.ToString();
    }

    private static string BuildE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Static-space + evolving-time reconstruction (from QG-080).");
        sb.AppendLine();
        sb.AppendLine("  Using ONLY redshift + time dilation + angular scales, one can build a static");
        sb.AppendLine("  space with clock rate γ(t) = dτ/dt = a(t). This reproduces redshift (1+z =");
        sb.AppendLine("  γ_obs/γ_emit), time dilation ((1+z)), and distances (D_L = χ(1+z)) — i.e. it");
        sb.AppendLine("  is conformal-time FLRW, observationally equivalent. So a VIABLE reconstruction");
        sb.AppendLine("  exists, but it is not distinguishable from ΛCDM (QG-080 Level 5 FAIL).");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Invariant vs model-dependent quantities.");
        sb.AppendLine();
        sb.AppendLine("  REINTERPRETATION-INVARIANT (robust, model-free):");
        sb.AppendLine("    - redshift z, time dilation b=1, CMB blackbody T0, Etherington D_L=(1+z)²D_A,");
        sb.AppendLine("      the ACCELERATION of the clock/expansion (SNe fainter than decelerating).");
        sb.AppendLine("  STRONGLY MODEL-DEPENDENT (FLRW-specific):");
        sb.AppendLine("    - scale factor a(t), comoving distance χ, ΩΛ ≈ 0.685, 'dark energy', proper");
        sb.AppendLine("      distance, comoving volume. In time-first terms ΩΛ becomes 'clock acceleration'.");
        sb.AppendLine();
        sb.AppendLine("  ⇒ The accepted conclusion 'dark energy density ΩΛ ≈ 0.685' is strongly model");
        sb.AppendLine("    dependent: the same data, re-read as clock evolution, no longer contains 'dark");
        sb.AppendLine("    energy' — only the (model-independent) acceleration.");
        return sb.ToString();
    }

    private static string BuildG()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (observation vs inference separated) : PASS");
        sb.AppendLine("  Level 2 (hidden FLRW assumptions identified) : PASS");
        sb.AppendLine("  Level 3 (viable static-space + evolving-time): PASS (γ=a, = conformal FLRW)");
        sb.AppendLine("  Level 4 (strongly model-dependent quantity) : PASS (ΩΛ/'dark energy', a(t), χ)");
        sb.AppendLine("  Level 5 (genuinely new prediction)          : FAIL (reinterpretation is sterile)");
        sb.AppendLine();
        sb.AppendLine("  CENTRAL QUESTION ANSWERED: the DIRECTLY observed core is redshift + flux + angular");
        sb.AppendLine("  size + time dilation + CMB blackbody. Everything geometric — a(t), H(z), distances,");
        sb.AppendLine("  ΩΛ, dark energy — is INFERRED by assuming expanding space. That inference CAN be");
        sb.AppendLine("  replaced by evolving time (QG-080), so expansion is an interpretation, not an");
        sb.AppendLine("  observation. But the reinterpretation yields no new prediction: it relabels, not");
        sb.AppendLine("  replaces, the FLRW structure.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // CSV writers
    // ---------------------------------------------------------------------

    private static void WriteObservablesCsv(string path, ObservabilityRow[] obs, ObservabilityRow[] inf)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Quantity,Type,RequiresFLRW,Probe,Notes");
        foreach (var o in obs)
            sb.AppendLine($"{o.Quantity},{o.Type},{o.RequiresFLRW},{o.Probe},{Escape(o.Notes)}");
        foreach (var i in inf)
            sb.AppendLine($"{i.Quantity},{i.Type},{i.RequiresFLRW},{i.Probe},{Escape(i.Notes)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteAssumptionsCsv(string path, AssumptionRow[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("DerivedQuantity,AssumptionChain,FirstFLRWIntroduction");
        foreach (var a in assumptions)
            sb.AppendLine($"{a.DerivedQuantity},{Escape(a.AssumptionChain)},{Escape(a.FirstFLRWIntroduction)}");
        File.WriteAllText(path, sb.ToString());
    }

    private static string Escape(string s) => s.Replace(",", ";");
}

public sealed record ModelDependenceReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    ModelDependenceAnalyzer.ObservabilityRow[] Observables,
    ModelDependenceAnalyzer.ObservabilityRow[] Inferences,
    ModelDependenceAnalyzer.AssumptionRow[] Assumptions,
    string OutDir);
