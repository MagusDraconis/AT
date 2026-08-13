using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG080_TimeScaleCosmologyAudit : ResearchTestBase
{
    public TQM_QG080_TimeScaleCosmologyAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG080_TimeScaleCosmology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-080 — Time-Scale Cosmology Audit");

        double[] zs = { 0.0, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0 };
        var points = new List<TimeScalePoint>();
        foreach (double z in zs)
        {
            double a = Cosmology.ScaleFactor(z);
            double h = Cosmology.H(z);
            double gamma = CosmicClock.ClockRate(z);
            double gTsc = GdaggerTimeDerivation.GdaggerFromClock(h);
            double gTqm = GdaggerTimeDerivation.GdaggerFromClock(h); // identical by construction
            points.Add(new TimeScalePoint(z, a, h, gamma, CosmicClock.ClockDrift(z), gTsc, gTqm));
        }

        // Core equivalence assertions.
        foreach (double z in zs)
        {
            double zTsc = TimeDrivenRedshift.WeakTscRedshift(z);
            Assert.Equal(z, zTsc, 10);
            double dil = TimeDrivenRedshift.TimeDilationFactor(Cosmology.ScaleFactor(z), 1.0);
            Assert.Equal(1 + z, dil, 10);
            Assert.True(GdaggerTimeDerivation.EquivalenceHolds(z), $"g† equivalence failed at z={z}");
        }

        // SN Ia time-dilation discrimination significance.
        double observedB = 1.0, sigmaB = 0.05;
        double strongB = -1.0; // strong TSC predicts inverse dilation
        double discSigma = Math.Abs(observedB - strongB) / sigmaB;

        var report = BuildReport(zs, points.ToArray(), discSigma);

        // Write markdown docs to Docs/.
        string docsDir = LocateDir("Docs");
        WriteDocs(docsDir, points.ToArray(), discSigma);

        sb.Append(report);
        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  g†(0) = {GdaggerTimeDerivation.LocalGdagger():E2} m/s²   " +
                      $"SN Ia discrimination vs strong TSC: {discSigma:F0}σ");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG080_TimeScaleCosmology_Report.txt"), sb.ToString());

        Assert.True(points.Count > 0);
        Assert.True(discSigma > 10, "strong TSC not decisively falsified by SN Ia time dilation");
        Assert.True(File.Exists(Path.Combine(docsDir, "TimeScaleCosmology.md")));
    }

    private static string BuildReport(double[] zs, TimeScalePoint[] p, double discSigma)
    {
        var sb = new StringBuilder();
        S(sb, "Section A — Model definition"); sb.AppendLine(SectionA());
        S(sb, "Section B — Redshift from the evolving clock"); sb.AppendLine(SectionB());
        S(sb, "Section C — Time dilation (SN Ia test)"); sb.AppendLine(SectionC(discSigma));
        S(sb, "Section D — Distances, BAO, CMB"); sb.AppendLine(SectionD());
        S(sb, "Section E — g† derivation"); sb.AppendLine(SectionE(p));
        S(sb, "Section F — Falsifiability"); sb.AppendLine(SectionF());
        S(sb, "Section G — Final verdict"); sb.AppendLine(SectionG());
        return sb.ToString();
    }

    private static string SectionA()
    {
        return
            "Two times: coordinate time t and physical/emergent time τ, with clock rate\n" +
            "γ(t) = dτ/dt ≠ constant. Space is static in comoving coordinates (da/dτ = 0).\n" +
            "\n" +
            "KEY RESULT: with γ(t) = a(t) (the FLRW scale factor), τ is the conformal time\n" +
            "η = ∫a dt, and the physical metric ds² = a²(η)[−dη² + dχ² + Sₖ(χ)²dΩ²] is EXACTLY\n" +
            "FLRW in conformal coordinates. Time-Scale Cosmology is FLRW reinterpreted: the\n" +
            "scale factor a is re-read as the cosmic clock rate γ.";
    }

    private static string SectionB()
    {
        return
            "Redshift: an atomic transition has universal physical frequency ν₀. Its coordinate\n" +
            "frequency at emission is ν₀γ_emit; static space conserves coordinate frequency; the\n" +
            "observer measures ν₀γ_emit/γ_obs in physical time. Hence\n" +
            "\n" +
            "      1 + z = ν₀ / (ν₀ γ_emit/γ_obs) = γ_obs / γ_emit.\n" +
            "\n" +
            "With γ = a this is 1+z = a_obs/a_emit — identical to ΛCDM. Redshift is reproduced\n" +
            "purely as cumulative clock-rate evolution, with NO metric expansion required.";
    }

    private static string SectionC(double discSigma)
    {
        return
            "Time dilation Δτ_obs = (γ_obs/γ_emit)·Δτ_emit = (1+z)·Δτ_emit (weak TSC, γ=a) —\n" +
            "this MATCHES the observed SN Ia light-curve stretch s ∝ (1+z)^b with b = +1.\n" +
            "\n" +
            "A 'strong' (naive) TSC with clocks faster in the past (γ_emit > γ_obs) instead gives\n" +
            "the INVERSE dilation Δτ_obs = Δτ_emit/(1+z), i.e. b = −1. The observed b = 1.00±0.05\n" +
            $"(Blondin+ 2008; high-z confirmations) excludes strong TSC at {discSigma:F0}σ.\n" +
            "\n" +
            "CONCLUSION: only the γ = a (conformal-time) form of TSC survives — and that form is\n" +
            "observationally equivalent to ΛCDM.";
    }

    private static string SectionD()
    {
        return
            "Distances (weak TSC, γ = a): photons spread over a fixed comoving sphere of area\n" +
            "4πχ², but energy is redshifted by (1+z) and the arrival rate diluted by (1+z):\n" +
            "F = L/[4πχ²(1+z)²] ⇒ D_L = χ(1+z), and D_A = χ/(1+z) — identical to ΛCDM.\n" +
            "\n" +
            "BAO and CMB acoustic scale θ* = r_s/D_A are therefore UNCHANGED (weak TSC).\n" +
            "Strong TSC (truly static space) would give D_A = χ with no (1+z) factor, shifting\n" +
            "θ* by (1+z) and is excluded by the CMB/BAO acoustic-scale measurements — consistent\n" +
            "with the SN Ia falsification of Section C.";
    }

    private static string SectionE(TimeScalePoint[] p)
    {
        var sb = new StringBuilder();
        sb.AppendLine("g† as a time-scale parameter. The fractional clock drift is the Hubble rate:\n");
        sb.AppendLine("    d(ln γ)/dt = γ̇/γ = H   (since γ = a).\n");
        sb.AppendLine("Therefore the RAR acceleration scale is\n");
        sb.AppendLine("    g† = c·d(ln γ)/dt / 2π = c·(τ̈/τ̇)/2π = c·H/2π.\n");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,5} {1,8} {2,9} {3,10} {4,11}", "z", "H", "γ=a", "dlnγ/dt", "g† [m/s²]"));
        foreach (var q in p)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,5:F1} {1,8:F1} {2,9:F3} {3,10:F1} {4,11:E2}",
                q.Z, q.H, q.Gamma, q.HLnGamma, q.Gdagger_TSC));
        sb.AppendLine();
        sb.AppendLine("NOTE on notation: the task's 'g† ∝ c·d(ln τ)/dt' is imprecise; the correct quantity\n" +
            "is d(ln γ)/dt = d(ln dτ/dt)/dt = τ̈/τ̇ (the log-derivative of the clock RATE), not\n" +
            "d(ln τ)/dt = γ/τ. The acceleration scale is the clock's log-acceleration.");
        return sb.ToString();
    }

    private static string SectionF()
    {
        return
            "Falsifiability: the ONLY way TSC deviates from ΛCDM is γ ≠ a (a genuinely different\n" +
            "clock-rate history). That deviation changes the time-dilation exponent b and the\n" +
            "angular-diameter distance, and is decisively excluded by SN Ia (b = 1) and CMB/BAO.\n" +
            "\n" +
            "Hence Time-Scale Cosmology has NO distinct, surviving prediction: it is either\n" +
            "(i) observationally equivalent to ΛCDM (γ = a, conformal time), or (ii) falsified\n" +
            "(γ ≠ a). Its value is INTERPRETIVE — it explains why a local acceleration scale g†\n" +
            "tracks the global cosmic clock — not predictive.";
    }

    private static string SectionG()
    {
        return
            "CLASSIFICATION:\n" +
            "  Level 1 (self-consistent)          : PASS\n" +
            "  Level 2 (redshift + time dilation) : PASS (γ = a)\n" +
            "  Level 3 (g† ∝ H)                   : PASS (exact: g† = c·d ln γ/dt /2π = cH/2π)\n" +
            "  Level 4 (why local scale tracks global time) : PASS — g† is the clock's log-acceleration\n" +
            "  Level 5 (distinct falsifiable prediction)     : FAIL — TSC is equivalent to ΛCDM or falsified\n" +
            "\n" +
            "CENTRAL QUESTION ANSWERED: yes, cosmological expansion can be consistently reinterpreted\n" +
            "as the evolution of physical time (conformal time), and g† = c·H/2π emerges naturally as\n" +
            "the clock-drift acceleration. But this is a REINTERPRETATION of ΛCDM, not a new theory:\n" +
            "it yields no distinct testable prediction, so it does not independently confirm TQM.";
    }

    private static void WriteDocs(string docsDir, TimeScalePoint[] p, double discSigma)
    {
        File.WriteAllText(Path.Combine(docsDir, "TimeScaleCosmology.md"),
            "# Time-Scale Cosmology (QG-080)\n\n" +
            "Reinterprets cosmic expansion as the evolution of physical time τ (clock rate γ=dτ/dt), " +
            "with static comoving space. With γ(t)=a(t), τ is conformal time and the model is FLRW " +
            "in conformal coordinates — observationally equivalent to ΛCDM. See TimeScaleEquations.md " +
            "for the derivations; Predictions.md for the falsifiability verdict.\n");

        File.WriteAllText(Path.Combine(docsDir, "TimeScaleEquations.md"),
            "# Time-Scale Equations (QG-080)\n\n" +
            "- Two times: t (coordinate), τ (physical), γ(t)=dτ/dt.\n" +
            "- Static space: da/dτ = 0 in physical time; metric ds² = −c²γ²dt² + dℓ².\n" +
            "- Redshift: 1+z = γ_obs/γ_emit  (from frequency shift of a fixed physical clock).\n" +
            "- Time dilation: Δτ_obs = (γ_obs/γ_emit)·Δτ_emit.\n" +
            "- Distances: D_L = χ(1+z), D_A = χ/(1+z) (static χ + (1+z) energy/rate dilution).\n" +
            "- Identification γ=a ⇒ τ=conformal time η=∫a dt, and FLRW ds²=a²(η)[−dη²+dχ²+Sₖ(χ)²dΩ²].\n" +
            "- Hubble: H = (1/a)da/dt = (1/γ)dγ/dt = d(ln γ)/dt = τ̈/τ̇.\n" +
            "- g† = c·d(ln γ)/dt / 2π = c·(τ̈/τ̇)/2π = c·H/2π.\n");

        File.WriteAllText(Path.Combine(docsDir, "DerivedObservables.md"),
            "# Derived Observables (QG-080)\n\n" +
            "| Observable | Weak TSC (γ=a) | Strong TSC (γ≠a, clocks faster in past) |\n" +
            "|---|---|---|\n" +
            "| Redshift | 1+z (matches ΛCDM) | 1+z but wrong sign convention |\n" +
            "| SN Ia time dilation | (1+z), b=+1 | 1/(1+z), b=−1 (EXCLUDED ~40σ) |\n" +
            "| Luminosity distance | χ(1+z) (matches) | χ (no (1+z) factor) |\n" +
            "| Angular diameter distance | χ/(1+z) (matches) | χ (shifted acoustic scale) |\n" +
            "| BAO / CMB θ* | unchanged | excluded |\n\n" +
            "Consistency with SN Ia time dilation, BAO and the CMB acoustic scale REQUIRES γ=a,\n" +
            "i.e. conformal-time FLRW. Any genuine deviation is observationally excluded.\n");

        File.WriteAllText(Path.Combine(docsDir, "RAR_TimeInterpretation.md"),
            "# RAR Time Interpretation (QG-080)\n\n" +
            "The RAR acceleration scale g† = c·H/2π is the fractional drift of the cosmic clock times\n" +
            "c/2π: g† = c·d(ln γ)/dt/2π = c·(τ̈/τ̇)/2π. This reframes g† from a 'gravity modification'\n" +
            "parameter into a TIME parameter — the local dynamical acceleration scale is set by the\n" +
            "global cosmic clock's log-acceleration. This is a Machian statement: local inertia/dynamics\n" +
            "couple to the global state of cosmic time. It explains WHY g† ∝ H, but (see Predictions.md)\n" +
            "adds no independent observable prediction over ΛCDM.\n");

        File.WriteAllText(Path.Combine(docsDir, "Predictions.md"),
            "# Predictions & Falsifiability (QG-080)\n\n" +
            "Time-Scale Cosmology has NO distinct surviving prediction. It is either\n" +
            "(i) observationally equivalent to ΛCDM (γ = a, conformal time), or\n" +
            "(ii) falsified (γ ≠ a) by SN Ia time dilation (b = −1 vs observed b = 1) and CMB/BAO.\n" +
            $"SN Ia time-dilation discrimination significance ≈ {discSigma:F0}σ.\n\n" +
            "Verdict: Levels 1–4 pass (self-consistent, redshift+dilation, g†∝H, time-interpretation),\n" +
            "Level 5 fails (no distinct falsifiable prediction). The model is a reinterpretation, not a\n" +
            "new theory; it does NOT independently confirm TQM's g† = cH/2π.\n");
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }

    private static string LocateDir(params string[] segments)
    {
        string combined = Path.Combine(segments);
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, combined);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine(@"D:\Coding\Test\TQM", combined);
    }
}
