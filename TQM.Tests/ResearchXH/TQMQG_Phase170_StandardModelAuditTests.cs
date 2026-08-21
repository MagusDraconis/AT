using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 170 — Standard Model audit. QG138-169 reproduce many SM structures and parameters.
/// This phase audits all major measured SM quantities against the TQM-QG derivation record,
/// classifying each as TESTED / PARTIALLY TESTED / UNTESTED, computing the coverage percentage and a
/// ranked list of remaining tests.
///
/// Tests: TQMQG1700 (fermion masses + CKM + PMNS + CP), TQMQG1701 (couplings, running, boson masses,
/// g-2, neutrinos, EW), TQMQG1702 (coverage percentage + ranked remaining tests).
/// </summary>
public class TQMQG_Phase170_StandardModelAuditTests : ResearchTestBase
{
    public TQMQG_Phase170_StandardModelAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1700_FermionCKMAndPMNS()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1700: fermion masses, CKM, PMNS, CP audit");

        sb.AppendLine("ASSUMPTIONS: the TQM-QG derivation record through QG169 is the authoritative");
        sb.AppendLine("coverage catalog; each major measured SM quantity is classified as TESTED");
        sb.AppendLine("(quantitative D96 derivation), PARTIAL (structural/directional only), or UNTESTED.");
        sb.AppendLine();
        sb.AppendLine("FERMION MASSES:");
        foreach (var q in StandardModelAudit.Catalog().Where(q => q.Phase.StartsWith("QG14")))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-22} {q.Result,-22} vs {q.Physical,-16} {q.Phase}  {q.Note}");
        sb.AppendLine();
        sb.AppendLine("CKM:");
        foreach (var q in StandardModelAudit.Catalog().Where(q => q.Name.StartsWith("CKM")))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-26} {q.Result,-26} vs {q.Physical,-12} {q.Phase}");
        sb.AppendLine();
        sb.AppendLine("PMNS:");
        foreach (var q in StandardModelAudit.Catalog().Where(q => q.Name.StartsWith("PMNS")))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-10} {q.Result,-34} vs {q.Physical,-14} {q.Phase}");
        Output.WriteLine(sb.ToString());

        var (t, p, u) = StandardModelAudit.Counts();
        Assert.True(t > 20, "should have >20 tested quantities");
        Assert.Contains("CKM |Vus|", StandardModelAudit.Catalog().Select(q => q.Name));
        Assert.Contains("PMNS θ13", StandardModelAudit.Catalog().Select(q => q.Name));
    }

    [Fact]
    public void TQMQG1701_CouplingsBosonsAndUntested()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1701: couplings, running, boson masses, g-2, neutrinos, EW audit");

        sb.AppendLine("ASSUMPTIONS: the audit distinguishes TESTED (couplings, running, MW/MZ/MH),");
        sb.AppendLine("PARTIAL (quark absolute scale, 106 GeV prediction), and UNTESTED (g-2,");
        sb.AppendLine("neutrino mass values, precision-EW observables).");
        sb.AppendLine();
        sb.AppendLine("COUPLINGS AND RUNNING:");
        foreach (var q in StandardModelAudit.Catalog().Where(q => q.Name.StartsWith("α") || q.Name.StartsWith("1/") || q.Name.StartsWith("sin") || q.Name.StartsWith("unif")))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-18} {q.Result,-28} vs {q.Physical,-14} {q.Phase}");
        sb.AppendLine();
        sb.AppendLine("BOSON MASSES:");
        foreach (var q in StandardModelAudit.Catalog().Where(q => new[] { "MW", "MZ", "MH", "ρ", "MW/MZ" }.Contains(q.Name)))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-8} {q.Result,-28} vs {q.Physical,-14} {q.Phase}");
        sb.AppendLine();
        sb.AppendLine("g-2, NEUTRINO MASSES, PRECISION EW (all UNTESTED unless noted):");
        foreach (var q in StandardModelAudit.Catalog().Where(q =>
            q.Name.Contains("g-2") || q.Name.Contains("neutrino") || q.Name.Contains("Δm") ||
            q.Name.Contains("ordering") || q.Name.Contains("Majorana") || q.Name.Contains("Γ") ||
            q.Name.Contains("S, T, U") || q.Name.Contains("R_b") || q.Name.Contains("A_FB") ||
            q.Name.Contains("θ_QCD") || q.Name.Contains("sin²θ_eff")))
            sb.AppendLine($"  [{q.Status,-8}] {q.Name,-28} {q.Phase}  {q.Note}");
        Output.WriteLine(sb.ToString());

        var untested = StandardModelAudit.Catalog().Where(q => q.Status == StandardModelAudit.Coverage.Untested);
        Assert.Contains("muon g-2 (a_μ)", untested.Select(q => q.Name));
        Assert.Contains("S, T, U oblique", untested.Select(q => q.Name));
        Assert.Contains("neutrino masses ν1,ν2,ν3", untested.Select(q => q.Name));
    }

    [Fact]
    public void TQMQG1702_CoverageAndRankedRemainingTests()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1702: coverage percentage and ranked remaining tests");

        var (t, p, u) = StandardModelAudit.Counts();
        double testedCov = StandardModelAudit.TestedCoverage();
        double weightedCov = StandardModelAudit.WeightedCoverage();
        double massCov = StandardModelAudit.MassCoverage();

        sb.AppendLine("ASSUMPTIONS: TESTED = quantitative D96 derivation; PARTIAL = structural/");
        sb.AppendLine("directional only; UNTESTED = no TQM-QG derivation. Coverage is computed both");
        sb.AppendLine("as the TESTED-only fraction of the tested+untested space and as a weighted");
        sb.AppendLine("fraction over all catalogued quantities (TESTED=1.0, PARTIAL=0.5, UNTESTED=0).");
        sb.AppendLine();
        sb.AppendLine($"CATALOG SIZE: {t + p + u} major measured SM quantities");
        sb.AppendLine($"  TESTED:   {t}");
        sb.AppendLine($"  PARTIAL:  {p}");
        sb.AppendLine($"  UNTESTED: {u}");
        sb.AppendLine();
        sb.AppendLine("COVERAGE:");
        sb.AppendLine($"  tested-only coverage (tested/(tested+untested)) = {testedCov:P0}");
        sb.AppendLine($"  weighted coverage (1.0/0.5/0.0) = {weightedCov:P1}");
        sb.AppendLine($"  mass-observable weighted coverage = {massCov:P1}");
        sb.AppendLine();
        sb.AppendLine("RANKED REMAINING TESTS (by importance):");
        int rank = 1;
        foreach (var (name, status, _, why) in StandardModelAudit.RemainingTests())
            sb.AppendLine($"  {rank++,2}. [{status,-8}] {name}: {why}");
        sb.AppendLine();
        sb.AppendLine($"AUDIT SUMMARY: {StandardModelAudit.Summary()}");
        sb.AppendLine();
        sb.AppendLine("  • The electroweak sector is essentially fully covered: 1/α_em = 137 (0.03%),");
        sb.AppendLine("    α_weak, α_strong, sin²θ_W, MW/MZ/MH, ρ = 1, CKM + CP, PMNS + δ_ν.");
        sb.AppendLine("  • The largest remaining gaps are muon g-2 (no derivation at all) and the");
        sb.AppendLine("    absolute neutrino mass scale / ordering / splittings (structural origin only).");
        sb.AppendLine("  • Precision-EW observables (Γ_Z, Γ_W, Γ_H, S/T/U, R_b, A_FB, sin²θ_eff) are");
        sb.AppendLine("    entirely untested; quark absolute masses are partial (ratios only).");
        Output.WriteLine(sb.ToString());

        Assert.True(testedCov > 0.5, "tested-only coverage should exceed 50%");
        Assert.True(weightedCov > 0.6, "weighted coverage should exceed 60%");
        Assert.True(u > 5, "there should remain a substantial untested set");
        Assert.Equal(1, StandardModelAudit.RemainingTests()[0].Rank);
    }
}
