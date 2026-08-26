using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 233 — Remaining Parameter Closure Audit. Re-adjudicate the 8 PARTIAL parameters from QG232
/// as DERIVED / BOUNDARY / ACTUALLY OPEN, separating true missing physics from documented boundaries.
/// Audit only — no new physics.
/// </summary>
public class ATQG_Phase233_ParameterClosureAuditTests : ResearchTestBase
{
    public ATQG_Phase233_ParameterClosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2330_EightParametersReAdjudicated()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2330: the 8 partial parameters re-adjudicated");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each of the 8 PARTIAL parameters from QG232 is re-classified as DERIVED / BOUNDARY");
        sb.AppendLine("    / ACTUALLY OPEN, separating true missing physics from documented boundaries.");
        sb.AppendLine();

        sb.AppendLine("RE-ADJUDICATION:");
        foreach (var a in ParameterClosureAudit.Adjudications())
        {
            sb.AppendLine($"  {a.Name}: {a.Status}");
            sb.AppendLine($"      {a.Reason}");
        }
        sb.AppendLine();

        sb.AppendLine($"By status: {string.Join(", ", ParameterClosureAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(8, ParameterClosureAudit.Adjudications().Length);
        var sc = ParameterClosureAudit.StatusCounts();
        Assert.Equal(3, sc[ParameterClosureAudit.Status.Derived]);
        Assert.Equal(3, sc[ParameterClosureAudit.Status.Boundary]);
        Assert.Equal(2, sc[ParameterClosureAudit.Status.ActuallyOpen]);
    }

    [Fact]
    public void ATQG2331_TrueMissingPhysicsSeparated()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2331: true missing physics vs documented boundaries");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - ACTUALLY OPEN = genuine missing physics; BOUNDARY = a documented impossibility or");
        sb.AppendLine("    scale/fraction input with a stated reason; DERIVED = resolved by a later phase.");
        sb.AppendLine();

        sb.AppendLine("ACTUALLY OPEN (true missing physics):");
        foreach (var a in ParameterClosureAudit.Adjudications().Where(a => a.Status == ParameterClosureAudit.Status.ActuallyOpen))
            sb.AppendLine($"  • {a.Name} — {a.Reason}");
        sb.AppendLine();
        sb.AppendLine("BOUNDARY (documented):");
        foreach (var a in ParameterClosureAudit.Adjudications().Where(a => a.Status == ParameterClosureAudit.Status.Boundary))
            sb.AppendLine($"  • {a.Name} — {a.Reason}");
        sb.AppendLine();
        sb.AppendLine("DERIVED (resolved):");
        foreach (var a in ParameterClosureAudit.Adjudications().Where(a => a.Status == ParameterClosureAudit.Status.Derived))
            sb.AppendLine($"  • {a.Name} — {a.Reason}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, ParameterClosureAudit.ActuallyOpen().Length);
        Assert.Contains("Ω_Λ (vacuum fraction)", ParameterClosureAudit.ActuallyOpen());
        Assert.Contains("Ω_m (matter fraction)", ParameterClosureAudit.ActuallyOpen());
        Assert.Equal(3, ParameterClosureAudit.Boundaries().Length);
        Assert.Equal(3, ParameterClosureAudit.Derived().Length);
    }

    [Fact]
    public void ATQG2332_Verdict()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2332: verdict — remaining exact gaps are Ω_Λ and Ω_m");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The parameter sector is complete iff no parameter is ACTUALLY OPEN.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Actually open: {string.Join(", ", ParameterClosureAudit.ActuallyOpen())}");
        sb.AppendLine($"  Boundaries: {string.Join(", ", ParameterClosureAudit.Boundaries())}");
        sb.AppendLine($"  Derived: {string.Join(", ", ParameterClosureAudit.Derived())}");
        sb.AppendLine($"  Parameter complete? {ParameterClosureAudit.ParameterComplete()}");
        sb.AppendLine($"  VERDICT = {ParameterClosureAudit.Verdict()}");
        sb.AppendLine($"  Summary: {ParameterClosureAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - 6 of the 8 partial parameters are resolved: 3 DERIVED (Majorana phases via the");
        sb.AppendLine("    real mass matrix QG174/179; quark hierarchy law via QG173; calibration ladder via");
        sb.AppendLine("    the Z-anchor QG130/168) and 3 BOUNDARY (Bekenstein 1/4 impossibility; H as a");
        sb.AppendLine("    current-epoch scale input; golden-ratio as a de-emphasized secondary consequence).");
        sb.AppendLine("  - The ONLY true missing physics is the pair of cosmological density fractions");
        sb.AppendLine("    Ω_Λ and Ω_m: their ratio to the critical density is not uniquely derived.");
        sb.AppendLine($"  ⇒ {ParameterClosureAudit.Verdict()}");

        Output.WriteLine(sb.ToString());

        Assert.False(ParameterClosureAudit.ParameterComplete(), "with Ω_Λ and Ω_m open, the sector is not fully complete");
        Assert.Equal("remaining exact gaps: Ω_Λ (vacuum fraction), Ω_m (matter fraction)", ParameterClosureAudit.Verdict());
    }
}
