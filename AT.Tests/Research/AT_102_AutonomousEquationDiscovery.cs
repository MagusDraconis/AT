using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_102_AutonomousEquationDiscovery : ResearchTestBase
{
    private const int BaseSeed = 102_000_001;

    public AT_102_AutonomousEquationDiscovery(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_102_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-102 Autonomous Equation Discovery");

        sb.AppendLine("AT-102: Discovering governing equations directly from data.");
        sb.AppendLine("         No assumptions about functional form.");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-083: Identified state variables {R, M}.");
        sb.AppendLine("  AT-100: Rejected linear equation dR/dt = α₀+α₁·R+α₂·M.");
        sb.AppendLine("  AT-101: Partial repair, 4/8 attacks still fail.");
        sb.AppendLine();
        sb.AppendLine("  This experiment: STOP ASSUMING. DISCOVER.");
        sb.AppendLine("  Use sparse symbolic regression over 24 basis functions");
        sb.AppendLine("  to infer dR/dt = F(R, M, N, K, λ) directly from data.");
        sb.AppendLine();

        // ── Section 2: Basis Function Library ────────────────────────
        Sec(sb, "2. Basis Function Library (24 candidate terms)");
        foreach (var bf in EquationDiscoveryAnalyzer.BasisLibrary)
            sb.AppendLine($"  {bf.Name,-15} = {bf.Expression}");
        sb.AppendLine();

        // ── Section 3: Data Generation ───────────────────────────────
        Sec(sb, "3. Discovery Data");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var data = EquationDiscoveryAnalyzer.GenerateDiscoveryData(BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Generated {data.Count} data points in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine($"  Coverage: N∈[10..1000], K∈[0.01..10], λ∈[0.005..0.5]");
        sb.AppendLine($"           6 topologies, random + extreme R samples");
        sb.AppendLine();
        sb.AppendLine($"  R range:  [{data.Min(d => d.R):F4}, {data.Max(d => d.R):F4}]");
        sb.AppendLine($"  M range:  [{data.Min(d => d.M):F6}, {data.Max(d => d.M):F4}]");
        sb.AppendLine($"  dR/dt:    [{data.Min(d => d.dRdt):F6}, {data.Max(d => d.dRdt):F6}]");
        sb.AppendLine();

        // ── Section 4: Equation Discovery ────────────────────────────
        Sec(sb, "4. Forward Stepwise Symbolic Regression");

        var theory = EquationDiscoveryAnalyzer.DiscoverEquation(data);

        sb.AppendLine($"  Search path: {theory.SearchPath}");
        sb.AppendLine();
        sb.AppendLine("  Step │ Terms │ Train R² │ AICc       │ Equation (first 60 chars)");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var cand in theory.AllDRdtCandidates)
        {
            string eqShort = cand.Equation.Length > 55
                ? cand.Equation[..55] + "..."
                : cand.Equation;
            sb.AppendLine($"  {cand.Name,-4} │ {cand.NumTerms,4}  │ {cand.TrainR2,7:F4} │ {cand.TrainAICc,9:F1} │ {eqShort}");
        }
        sb.AppendLine();

        sb.AppendLine("  ── Discovered Equation ──");
        sb.AppendLine($"  dR/dt = {theory.DRdtEquation.Equation}");
        sb.AppendLine($"  Terms: {theory.DRdtEquation.NumTerms}");
        sb.AppendLine($"  Train R²: {theory.DRdtEquation.TrainR2:F4}");
        sb.AppendLine($"  AICc: {theory.DRdtEquation.TrainAICc:F1}");
        sb.AppendLine();

        // ── Section 5: Validation ────────────────────────────────────
        Sec(sb, "5. Hostile Validation (AT-100 Attack Vectors)");

        var validation = EquationDiscoveryAnalyzer.ValidateDiscoveredTheory(theory, BaseSeed);

        sb.AppendLine("  Attack                  │ R²       │ Pass?");
        sb.AppendLine("  " + new string('─', 50));
        foreach (var (name, (r2, passedTest)) in validation)
            sb.AppendLine($"  {name,-22} │ {r2,7:F4} │ {(passedTest ? "✓" : "✗ FAIL")}");
        sb.AppendLine();

        int nPassed = validation.Count(v => v.Value.Passed);
        double survival = (double)nPassed / validation.Count;

        sb.AppendLine($"  Survival: {nPassed}/{validation.Count} ({survival:P0})");
        sb.AppendLine();

        // Compare with AT-101 best.
        sb.AppendLine("  ── Comparison: AT-101 vs AT-102 ──");
        sb.AppendLine("  Attack                  │ AT-101(D) │ AT-102   │ Δ");
        sb.AppendLine("  " + new string('─', 65));
        // AT-101 Model D scores from earlier run.
        var at101 = new Dictionary<string, double>
        {
            ["Extreme Coherence"] = 0.279, ["Extreme M"] = 0.035,
            ["Mixed Topologies"] = 0.478, ["Coupling Laws"] = 0.903,
            ["Phase Noise"] = -0.154, ["Large-N N=500"] = -3.825,
            ["Small-N N=10"] = 0.114, ["Out-of-Distribution"] = -4.003
        };
        foreach (var (name, (r2, _)) in validation)
        {
            double t101 = at101.GetValueOrDefault(name, 0);
            double delta = r2 - t101;
            string mark = delta > 0.05 ? "↑ BETTER" : delta < -0.05 ? "↓ WORSE" : "≈ same";
            sb.AppendLine($"  {name,-22} │ {t101,8:F3}   │ {r2,7:F3}  │ {delta,8:F3} {mark}");
        }
        sb.AppendLine();

        // ── Section 6: Research Questions ────────────────────────────
        Sec(sb, "6. Research Questions");

        sb.AppendLine("  Q1: What is the simplest equation for dR/dt?");
        sb.AppendLine($"    {theory.DRdtEquation.Equation}");
        sb.AppendLine($"    ({theory.DRdtEquation.NumTerms} terms, R²={theory.DRdtEquation.TrainR2:F3})");
        sb.AppendLine();

        sb.AppendLine("  Q2: What is the simplest equation for dM/dt?");
        sb.AppendLine("    Not discovered — dM/dt requires temporal profile data.");
        sb.AppendLine("    AT-082 best fit: dM/dt = f(M,R,M²,R²,MR) with Adj R²=0.299");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does N appear fundamentally?");
        bool hasN = theory.DRdtEquation.Equation.Contains("N");
        sb.AppendLine($"    {(hasN ? "YES — N appears in the discovered equation." : "NO — N was not selected by the regression.")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does K appear fundamentally?");
        bool hasK = theory.DRdtEquation.Equation.Contains("K");
        sb.AppendLine($"    {(hasK ? "YES — K appears in the discovered equation." : "NO — K was not selected.")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Can the discovered equations survive hostile review?");
        sb.AppendLine($"    Survival: {nPassed}/{validation.Count} ({survival:P0})");
        string cls = survival >= 0.875 ? "YES — robust against all attacks" :
                     survival >= 0.625 ? "MOSTLY — minor gaps remain" :
                     survival >= 0.375 ? "PARTIALLY — significant gaps" : "NO — major failures";
        sb.AppendLine($"    {cls}");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can a scale-invariant form be found?");
        sb.AppendLine($"    Discovered equation contains: {theory.DRdtEquation.Equation}");
        sb.AppendLine("    Scale invariance means the equation should work at all N,K,λ.");
        sb.AppendLine($"    {(survival >= 0.75 ? "PARTIALLY scale-invariant — works across most regimes." : "NOT scale-invariant — parameter-dependent terms needed.")}");
        sb.AppendLine();

        sb.AppendLine("  Q7: What is the final minimal state-space theory?");
        sb.AppendLine($"    State = {{R, M}}");
        sb.AppendLine($"    dR/dt = {theory.DRdtEquation.Equation}");
        sb.AppendLine($"    ({theory.DRdtEquation.NumTerms} discovered terms)");
        sb.AppendLine();

        // ── Section 7: Classification ────────────────────────────────
        Sec(sb, "7. Classification");

        string classification = survival >= 0.875 ? "D: Candidate Emergent Physics" :
                                survival >= 0.625 ? "C: Robust Effective Field Theory" :
                                survival >= 0.375 ? "B: Improved Empirical Model" :
                                "A: No Equation Found";

        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Survival: {survival:P0} ({nPassed}/{validation.Count})");
        sb.AppendLine();

        // ── Section 8: Discovered Theory ─────────────────────────────
        Sec(sb, "8. Discovered Theory");

        sb.AppendLine("  ┌─────────────────────────────────────────────────────┐");
        sb.AppendLine("  │  DISCOVERED GOVERNING EQUATION                      │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        string eqDisplay = theory.DRdtEquation.Equation;
        if (eqDisplay.Length <= 48)
            sb.AppendLine($"  │  dR/dt = {eqDisplay,-42} │");
        else
        {
            for (int i = 0; i < eqDisplay.Length; i += 48)
                sb.AppendLine($"  │  {(i == 0 ? "dR/dt =" : "        ")}{eqDisplay.Substring(i, Math.Min(48, eqDisplay.Length - i)),-42} │");
        }
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  Terms: {theory.DRdtEquation.NumTerms,-44} │");
        sb.AppendLine($"  │  Train R²: {theory.DRdtEquation.TrainR2,43:F4} │");
        sb.AppendLine($"  │  Validation: {nPassed}/{validation.Count} passed ({survival:P0}){"",-21} │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        sb.AppendLine("  │  SELECTED BASIS FUNCTIONS                           │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        for (int i = 1; i < theory.DRdtEquation.Terms.Count; i++) // skip intercept
        {
            var term = theory.DRdtEquation.Terms[i];
            double coeff = i < theory.DRdtEquation.Coefficients.Length
                ? theory.DRdtEquation.Coefficients[i] : 0;
            sb.AppendLine($"  │  {coeff,10:F4} · {term.Expression,-32} │");
        }
        sb.AppendLine("  └─────────────────────────────────────────────────────┘");
        sb.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        Sec(sb, "9. Conclusion");
        sb.AppendLine($"  C1.  Discovered: dR/dt = {theory.DRdtEquation.Equation}");
        sb.AppendLine($"  C2.  Terms: {theory.DRdtEquation.NumTerms}");
        sb.AppendLine($"  C3.  Train R²: {theory.DRdtEquation.TrainR2:F4}");
        sb.AppendLine($"  C4.  Validation: {nPassed}/{validation.Count} ({survival:P0})");
        sb.AppendLine($"  C5.  Classification: {classification}");
        sb.AppendLine($"  C6.  Basis library: {EquationDiscoveryAnalyzer.BasisLibrary.Count} functions");
        sb.AppendLine($"  C7.  Data: {data.Count} points across N∈[10..1000], K∈[0.01..10], λ∈[0.005..0.5]");
        sb.AppendLine();
        sb.AppendLine($"  C8.  The symbolic regression {(survival >= 0.625 ? "DISCOVERED a robust equation." : "found an improved but imperfect equation.")}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-102 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
