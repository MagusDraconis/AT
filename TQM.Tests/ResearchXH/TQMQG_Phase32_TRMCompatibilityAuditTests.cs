using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 32 — TRM compatibility audit. Audits which existing TQM derivations break if the TRM (ψ) kernel is
/// added. Classify each: UNCHANGED / MODIFIED / BROKEN.
///
/// Tests: TQMQG320 (compatibility matrix), TQMQG321 (metric origin preserved), TQMQG322 (unification readout).
/// </summary>
public class TQMQG_Phase32_TRMCompatibilityAuditTests : ResearchTestBase
{
    public TQMQG_Phase32_TRMCompatibilityAuditTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG320: compatibility matrix ────────────────────────────────────────────────

    [Fact]
    public void TQMQG320_CompatibilityMatrix()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG320: which derivations survive the added TRM (ψ) kernel?");

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
        sb.AppendLine("  counting measure / metric origin / matter deficit / α=0 attractor / critical branching → UNCHANGED");
        sb.AppendLine("  Einstein structure → MODIFIED (gains the ψ/Weyl terms — the whole point of the extension)");
        sb.AppendLine("  nothing is BROKEN.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, unchanged);
        Assert.Equal(1, modified);
        Assert.Equal(0, broken);
    }

    // ── TQMQG321: metric origin √(−g)=ρ preserved ─────────────────────────────────────

    [Fact]
    public void TQMQG321_MetricOriginPreserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG321: √(−g)=ρ survives the ψ-perturbation (volume-preserving)");

        int d = 3;
        double[] xs = { -1.0, -0.5, 0.0, 0.5, 1.0 };
        double b = 0.3;
        bool allPreserved = true;
        foreach (var x in xs)
        {
            double err = TRMCompatibilityAudit.PerturbedVolumeError(d, x, b);
            bool ok = TRMCompatibilityAudit.MetricOriginPreserved(d, x, b);
            sb.AppendLine($"x = {x,5:F2}  √(−g) = {TRMCompatibilityAudit.PerturbedVolumeElement(d, x, b):F6}  ρ = {TRMCompatibilityAudit.Profile(x):F6}  err = {err:E1}  ok = {ok}");
            allPreserved &= ok;
        }

        sb.AppendLine();
        sb.AppendLine($"√(−g) = ρ holds at all sample points under ψ: {allPreserved}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ψ-perturbation g_00=−ρ^(2/d)e^{2ψ}, g_ii=ρ^(2/d)e^{−2ψ/(d−1)} has det = −ρ², so √(−g)=ρ");
        sb.AppendLine("is UNCHANGED. The metric-origin derivation (√(−g)=ρ → k=2/d) survives the ψ-extension unchanged.");
        Output.WriteLine(sb.ToString());

        Assert.True(allPreserved, "√(−g)=ρ should be preserved at all sample points");
        Assert.Equal("UNCHANGED", TRMCompatibilityAudit.Classify("metric-origin"));
    }

    // ── TQMQG322: unification readout ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG322_UnificationReadout()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG322: what a TQM/TRM unification must preserve");

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
        bool onlyEinsteinModified = modified == 1 && TRMCompatibilityAudit.Classify("einstein-structure") == "MODIFIED";

        sb.AppendLine($"nothing broken by the ψ kernel: {nothingBroken}");
        sb.AppendLine($"only the Einstein structure is modified: {onlyEinsteinModified}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the TRM (ψ) kernel is a CLEAN extension. It leaves the scalar backbone intact — counting");
        sb.AppendLine("measure, metric origin √(−g)=ρ, matter=deficit, α=0 attractor, critical branching all UNCHANGED. Its only");
        sb.AppendLine("effect is to enrich the Einstein tensor with the ψ/Weyl (tensor) terms — which is exactly the non-conformal");
        sb.AppendLine("degree of freedom needed to restore lensing/GWs/horizon thermodynamics (QG22–24). A TQM/TRM unification can");
        sb.AppendLine("proceed on this compatibility matrix: add ψ, keep the scalar derivations, replace only the Einstein sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(nothingBroken, "no derivation should be broken");
        Assert.True(onlyEinsteinModified, "only the Einstein structure should be modified");
    }
}
