using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_047_IdentityEnergyCoupling : ResearchTestBase
{
    // ── Experimental parameters ──────────────────────────────────────

    private static readonly string[] Histories = { "A", "B", "AB", "BA", "ABC", "CBA" };
    private static readonly double[] Injections = { 0.00, 0.10, 0.25, 0.50, 1.00, 2.00 };
    private static readonly double[] Betas = { 0.0, 0.1, 0.2, 0.5, 1.0 };
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int N = 200;
    private const int Seeds = 4;
    private const int Iterations = 4000;
    private const int BaseSeed = 701408733;

    public AT_047_IdentityEnergyCoupling(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_047_Run()
    {
        SlowResearchGate.SkipUnlessSlowRequested("AT-047 heavy Kuramoto simulation");
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-047 Identity\u2013Energy Coupling Analysis");

        report.AppendLine("AT-047: Are Resonance Identity and Energy the Same Property?");
        report.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  Determine whether resonance identity and resonance energy");
        report.AppendLine("  represent the same degree of freedom or two independent");
        report.AppendLine("  properties of a condensate.");
        report.AppendLine();
        report.AppendLine("  H0: Identity is fully determined by energy.");
        report.AppendLine("  H1: Identity and energy are independent.");
        report.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        int total = Histories.Length * Injections.Length * Betas.Length * Seeds;

        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Histories:   [{string.Join(", ", Histories)}]");
        report.AppendLine($"  Injections:  [{string.Join(", ", Injections)}]");
        report.AppendLine($"  \u03b2 (memory): [{string.Join(", ", Betas)}]");
        report.AppendLine($"  Seeds: {Seeds} per combination");
        report.AppendLine($"  N = {N}, K = {K}, \u03bb = {Lambda}");
        report.AppendLine($"  Total runs: {total}");
        report.AppendLine($"  Iterations/run: {Iterations}");
        report.AppendLine();
        report.AppendLine("  Assumptions:");
        report.AppendLine("    A1. Identity fingerprint = (FinalR, MeanFreq, PhaseVariance)");
        report.AppendLine("    A2. Energy proxy = FinalR \u00d7 MeanFreq (consistent with AT-038/039)");
        report.AppendLine("    A3. Identity distance = normalized Euclidean distance in fingerprint space");
        report.AppendLine("    A4. Phases injected by history mimic distinct experiential paths");
        report.AppendLine("    A5. Frequency scaling is a valid energy injection mechanism");
        report.AppendLine();

        // ── Run experiments ──────────────────────────────────────────
        var bag = new ConcurrentBag<IdentityEnergyAnalyzer.IdentityEnergyState>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int hi = idx % Histories.Length;
            int rem = idx / Histories.Length;
            int ii = rem % Injections.Length;
            rem /= Injections.Length;
            int bi = rem % Betas.Length;
            int si = rem / Betas.Length;

            int combinedSeed = BaseSeed + idx * 7919;
            bag.Add(IdentityEnergyAnalyzer.Analyze(
                Histories[hi], Betas[bi], Injections[ii],
                K, Lambda, N, combinedSeed, Iterations));
        });

        sw.Stop();
        var states = bag.ToList();
        report.AppendLine($"  Completed {states.Count} runs in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── Compute correlations ─────────────────────────────────────
        var corr = IdentityEnergyAnalyzer.ComputeCorrelations(states);

        // ── Section 3: Identity-Energy Correlation ───────────────────
        AppendSection(report, "3. Identity\u2013Energy Correlation");

        report.AppendLine($"  Pearson r  = {corr.PearsonR,10:F4}");
        report.AppendLine($"  |r|        = {Math.Abs(corr.PearsonR),10:F4}");
        report.AppendLine($"  Mutual I   = {corr.MutualInformationBits,10:F4} bits");
        report.AppendLine();

        string rInterpretation = Math.Abs(corr.PearsonR) switch
        {
            >= 0.7 => "STRONG correlation \u2014 identity tracks energy closely",
            >= 0.4 => "MODERATE correlation \u2014 identity partially tracks energy",
            >= 0.2 => "WEAK correlation \u2014 identity weakly tracks energy",
            _ => "NEGLIGIBLE correlation \u2014 identity and energy are largely decoupled"
        };
        report.AppendLine($"  Interpretation: {rInterpretation}");
        report.AppendLine();

        // ── Section 4: Same-Energy Comparison ────────────────────────
        AppendSection(report, "4. Same-Energy Comparison (Q1: Different identities, same energy?)");

        report.AppendLine($"  Mean identity distance (same energy):    {corr.MeanIdentityDistanceSameEnergy,10:F4}");
        report.AppendLine($"  Mean identity distance (different energy): {corr.MeanIdentityDistanceDiffEnergy,10:F4}");
        report.AppendLine();

        bool sameEnergyDiffId = corr.MeanIdentityDistanceSameEnergy > 0.05;
        report.AppendLine($"  Q1 ANSWER: {(sameEnergyDiffId ? "YES \u2014 Different identities CAN occupy the same energy" : "NO \u2014 Identities at the same energy are nearly identical")}");
        report.AppendLine($"    \u2192 Identity distance at fixed energy: {corr.MeanIdentityDistanceSameEnergy:F4}");
        report.AppendLine();

        // ── Section 5: Same-Identity Comparison ──────────────────────
        AppendSection(report, "5. Same-Identity Comparison (Q2: Same identity, different energies?)");

        report.AppendLine($"  Mean energy distance (same identity):      {corr.MeanEnergyDistanceSameIdentity,10:F4}");
        report.AppendLine($"  Mean energy distance (different identity):  {corr.MeanEnergyDistanceDiffIdentity,10:F4}");
        report.AppendLine();

        bool sameIdDiffEnergy = corr.MeanEnergyDistanceSameIdentity > 0.1;
        report.AppendLine($"  Q2 ANSWER: {(sameIdDiffEnergy ? "YES \u2014 The same identity CAN occupy different energies" : "NO \u2014 Same identity implies same energy")}");
        report.AppendLine($"    \u2192 Energy spread at fixed identity: {corr.MeanEnergyDistanceSameIdentity:F4}");
        report.AppendLine();

        // ── Section 6: Memory Dependence ─────────────────────────────
        AppendSection(report, "6. Memory Dependence (Q4: Does \u03b2 modify energy levels?)");

        report.AppendLine("  \u03b2     | Mean Energy | Mean R   | Identity Var | Memory Score");
        report.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (double beta in Betas)
        {
            var sub = states.Where(s => Math.Abs(s.Beta - beta) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double meanE = sub.Average(s => s.Energy);
            double meanR = sub.Average(s => s.FinalR);
            double idVar = Variance(sub.Select(s => s.FinalR));
            double memScore = sub.Average(s => s.MemoryScore);
            report.AppendLine($"  {beta,4:F1}   | {meanE,10:F4}  | {meanR,8:F4} | {idVar,12:F6} | {memScore,12:F6}");
        }

        report.AppendLine();

        // Check Q4: does beta systematically shift energy?
        var beta0 = states.Where(s => s.Beta < 0.01).ToList();
        var beta1 = states.Where(s => s.Beta > 0.99).ToList();
        double e0 = beta0.Count > 0 ? beta0.Average(s => s.Energy) : 0;
        double e1 = beta1.Count > 0 ? beta1.Average(s => s.Energy) : 0;
        bool betaShiftsEnergy = Math.Abs(e1 - e0) > 0.05;

        report.AppendLine($"  Q4 ANSWER: {(betaShiftsEnergy ? "YES \u2014 Memory strength \u03b2 modifies energy levels" : "NO \u2014 Energy is \u03b2-independent")}");
        report.AppendLine($"    \u2192 \u0394E(\u03b2=0 \u2192 \u03b2=1): {e1 - e0,8:F4}");
        report.AppendLine();

        // ── Section 6b: Stability metrics ────────────────────────────
        report.AppendLine("  Stability Metrics:");
        report.AppendLine($"    Cross-energy identity stability:  {corr.CrossEnergyIdentityStability,8:F4}");
        report.AppendLine($"    Cross-identity energy stability:  {corr.CrossIdentityEnergyStability,8:F4}");
        report.AppendLine();

        // ── Section 7: Interpretation ────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine($"  Classification: {corr.RelationshipClassification}");
        report.AppendLine();

        // Q5: Does energy determine identity?
        bool energyDeterminesId = Math.Abs(corr.PearsonR) > 0.7;
        report.AppendLine($"  Q5: Does energy determine identity?");
        report.AppendLine($"    {(energyDeterminesId ? "YES \u2014 Energy is the dominant determinant of identity" : "NO \u2014 Energy alone does not determine identity")}");
        report.AppendLine($"    Evidence: |r| = {Math.Abs(corr.PearsonR):F4}, MI = {corr.MutualInformationBits:F4} bits");
        report.AppendLine();

        // Q6: Independence test
        bool areIndependent = corr.RelationshipClassification.StartsWith("D:");
        bool areWeaklyDependent = corr.RelationshipClassification.StartsWith("C:");
        report.AppendLine($"  Q6: Are identity and energy independent state variables?");
        if (areIndependent)
        {
            report.AppendLine("    YES \u2014 Identity and energy are independent properties of a condensate.");
        }
        else if (areWeaklyDependent)
        {
            report.AppendLine("    PARTIALLY \u2014 Identity and energy are weakly coupled but");
            report.AppendLine("    distinguishable as separate state variables.");
        }
        else
        {
            report.AppendLine("    NO \u2014 Identity and energy are strongly coupled and may be");
            report.AppendLine("    different manifestations of the same underlying property.");
        }
        report.AppendLine();

        // Supporting evidence table
        report.AppendLine("  Evidence Summary:");
        report.AppendLine($"    Correlation |r|:                 {Math.Abs(corr.PearsonR),8:F4}");
        report.AppendLine($"    Mutual Information:              {corr.MutualInformationBits,8:F4} bits");
        report.AppendLine($"    Same-E identity stability:       {corr.CrossEnergyIdentityStability,8:F4}");
        report.AppendLine($"    Same-I energy stability:         {corr.CrossIdentityEnergyStability,8:F4}");
        report.AppendLine($"    \u0394Id(same E) / \u0394Id(diff E):    {corr.MeanIdentityDistanceSameEnergy / Math.Max(corr.MeanIdentityDistanceDiffEnergy, 1e-10),8:F4}");
        report.AppendLine($"    \u0394E(same Id) / \u0394E(diff Id):    {corr.MeanEnergyDistanceSameIdentity / Math.Max(corr.MeanEnergyDistanceDiffIdentity, 1e-10),8:F4}");
        report.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Relationship: {corr.RelationshipClassification}");
        report.AppendLine();

        string primaryConclusion;
        if (areIndependent)
        {
            primaryConclusion = "Identity and energy are INDEPENDENT properties.";
            report.AppendLine("  C2. Two condensates can possess identical energy with different identity,");
            report.AppendLine("      and two condensates can share identity at different energies.");
            report.AppendLine("  C3. Identity carries information beyond the energy level \u2014");
            report.AppendLine("      historical path, memory, and phase structure encode");
            report.AppendLine("      distinguishable identity signatures.");
        }
        else if (areWeaklyDependent)
        {
            primaryConclusion = "Identity and energy are WEAKLY COUPLED but distinguishable.";
            report.AppendLine("  C2. Energy is a partial determinant of identity but does not");
            report.AppendLine("      fully specify it. Historical and memory effects persist");
            report.AppendLine("      as distinguishable identity components.");
            report.AppendLine("  C3. Identity and energy may represent different projections");
            report.AppendLine("      of an underlying state manifold rather than being identical.");
        }
        else
        {
            primaryConclusion = "Identity and energy are STRONGLY COUPLED.";
            report.AppendLine("  C2. Energy is the primary determinant of resonance identity.");
            report.AppendLine("      Different identities are largely different energy states.");
            report.AppendLine("  C3. Identity and energy may be two names for the same");
            report.AppendLine("      underlying condensate property.");
        }

        report.AppendLine();
        report.AppendLine($"  Primary conclusion: {primaryConclusion}");
        report.AppendLine();

        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-047 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────

    private static void AppendSection(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }

    private static double Variance(IEnumerable<double> values)
    {
        var list = values.ToList();
        if (list.Count < 2) return 0;
        double mean = list.Average();
        return list.Sum(v => (v - mean) * (v - mean)) / (list.Count - 1);
    }
}
