using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 32 — TRM compatibility audit. Audits which existing AT derivations break if the TRM (ψ) kernel is
/// added. Classify each: UNCHANGED / MODIFIED / BROKEN.
///
/// Tests: ATQG320 (compatibility matrix), ATQG321 (metric origin preserved), ATQG322 (unification readout).
/// </summary>
public class ATQG_Phase32_TRMCompatibilityAuditTests : ResearchTestBase
{
    public ATQG_Phase32_TRMCompatibilityAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG320: compatibility matrix ────────────────────────────────────────────────

    [Fact]
    public void ATQG320_CompatibilityMatrix()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG320: which derivations survive the added TRM (ψ) kernel?");

        int unchanged = 0, modified = 0, broken = 0;
        foreach (var d in TRMCompatibilityAudit.Derivations)
        {
            string c = TRMCompatibilityAudit.Classify(d);
            sb.AppendLine($"{d,-22} -> {c}");
            switch (c)
            {
                case "UNCHANGED": unchanged++; break;
                case "MODIFIED": modified++; break;
                case "BROKEN": broken++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"UNCHANGED : {unchanged}");
        sb.AppendLine($"MODIFIED  : {modified}");
        sb.AppendLine($"BROKEN    : {broken}");
        sb.AppendLine();
        sb.AppendLine("COMPATIBILITY MATRIX:");
        sb.AppendLine("  counting measure / matter deficit / α=0 attractor / critical branching → UNCHANGED");
        sb.AppendLine("  metric origin → MODIFIED (CORRECTED by G_025: ψ changes √(det g_ij) = ρ·e^(−dψ/(d−1)))");
        sb.AppendLine("  Einstein structure → MODIFIED (gains the ψ/Weyl terms — the whole point of the extension)");
        sb.AppendLine("  nothing is BROKEN.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, unchanged);      // CORRECTED (G_025): metric-origin is no longer UNCHANGED
        Assert.Equal(2, modified);       // metric-origin + einstein-structure
        Assert.Equal(0, broken);
    }

    // ── ATQG321: metric origin √(−g)=ρ preserved ─────────────────────────────────────

    [Fact]
    public void ATQG321_MetricOriginPreserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG321: does √(−g)=ρ survive the ψ-perturbation? CORRECTED (G_025): NO");

        int d = 3;
        double[] xs = { -1.0, -0.5, 0.0, 0.5, 1.0 };
        double b = 0.3;
        bool allPreserved = true, allBroken = true, psi0Preserved = true;
        // NOTE: at x = 0 the perturbation vanishes (ψ = b·x = 0), so that point is the measure-preserving
        // member itself — the break is asserted only where ψ ≠ 0.
        foreach (var x in xs)
        {
            double err = TRMCompatibilityAudit.PerturbedVolumeError(d, x, b);
            bool ok = TRMCompatibilityAudit.MetricOriginPreserved(d, x, b);
            bool ok0 = TRMCompatibilityAudit.MetricOriginPreserved(d, x, 0.0);
            sb.AppendLine($"x = {x,5:F2}  ψ≠0: √(det g_ij) = {TRMCompatibilityAudit.PerturbedVolumeElement(d, x, b):F6}  ρ = {TRMCompatibilityAudit.Profile(x):F6}  err = {err:E3}  preserved = {ok}");
            sb.AppendLine($"          ψ=0: √(det g_ij) = {TRMCompatibilityAudit.PerturbedVolumeElement(d, x, 0.0):F6}  ρ = {TRMCompatibilityAudit.Profile(x):F6}  preserved = {ok0}");
            allPreserved &= ok;
            if (x != 0.0) allBroken &= !ok;   // ψ = 0 at x = 0 by construction
            psi0Preserved &= ok0;
        }

        sb.AppendLine();
        sb.AppendLine($"√(−g) = ρ holds at ALL sample points under ψ≠0: {allPreserved}   (CORRECTED: false)");
        sb.AppendLine($"√(−g) = ρ FAILS at ALL sample points where ψ≠0 (x≠0): {allBroken}");
        sb.AppendLine($"√(−g) = ρ holds at ALL sample points under ψ = 0: {psi0Preserved}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION (CORRECTED by ResearchY-G_025): the former claim \"det = −ρ²\" came from raising the");
        sb.AppendLine("spatial block to the power d−1 instead of d. With the correct count there are d spatial factors, so");
        sb.AppendLine("  det g = −ρ^(2(d+1)/d)·e^(−2ψ/(d−1))   and   √(det g_ij) = ρ·e^(−dψ/(d−1)).");
        sb.AppendLine("The metric origin is therefore MODIFIED by the ψ extension, and ψ = 0 is the ONLY measure-preserving member.");
        Output.WriteLine(sb.ToString());

        Assert.True(allBroken, "CORRECTED (G_025): √(−g)=ρ must FAIL wherever ψ≠0 (x≠0)");
        Assert.True(psi0Preserved, "√(−g)=ρ must hold at ψ = 0 (the measure-preserving member)");
        Assert.False(allPreserved, "the ψ-perturbation is NOT volume-preserving");
        Assert.Equal("MODIFIED", TRMCompatibilityAudit.Classify("metric-origin"));
    }

    // ── ATQG322: unification readout ──────────────────────────────────────────────────

    [Fact]
    public void ATQG322_UnificationReadout()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG322: what a AT/TRM unification must preserve");

        int unchanged = 0, modified = 0, broken = 0;
        foreach (var d in TRMCompatibilityAudit.Derivations)
        {
            switch (TRMCompatibilityAudit.Classify(d))
            {
                case "UNCHANGED": unchanged++; break;
                case "MODIFIED": modified++; break;
                case "BROKEN": broken++; break;
            }
        }

        bool nothingBroken = broken == 0;
        bool einsteinModified = TRMCompatibilityAudit.Classify("einstein-structure") == "MODIFIED";
        bool metricOriginModified = TRMCompatibilityAudit.Classify("metric-origin") == "MODIFIED";   // CORRECTED (G_025)
        bool onlyEinsteinModified = modified == 2 && einsteinModified && metricOriginModified;

        sb.AppendLine($"nothing broken by the ψ kernel: {nothingBroken}");
        sb.AppendLine($"Einstein structure modified: {einsteinModified} ; metric origin modified: {metricOriginModified}");
        sb.AppendLine($"exactly those two are modified: {onlyEinsteinModified}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION (CORRECTED by ResearchY-G_025): the TRM (ψ) kernel leaves the scalar backbone intact — counting");
        sb.AppendLine("measure, matter=deficit, α=0 attractor and critical branching are UNCHANGED — but the METRIC ORIGIN is");
        sb.AppendLine("MODIFIED, not unchanged: ψ changes √(det g_ij) = ρ·e^(−dψ/(d−1)). Its effect is twofold:");
        sb.AppendLine("effect is to enrich the Einstein tensor with the ψ/Weyl (tensor) terms — which is exactly the non-conformal");
        sb.AppendLine("degree of freedom needed to restore lensing/GWs/horizon thermodynamics (QG22–24). A AT/TRM unification can");
        sb.AppendLine("proceed on this compatibility matrix: add ψ, keep the scalar derivations, replace only the Einstein sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(nothingBroken, "no derivation should be broken");
        Assert.True(onlyEinsteinModified, "only the Einstein structure should be modified");
    }
}
