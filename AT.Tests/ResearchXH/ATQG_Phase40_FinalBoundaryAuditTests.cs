using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 40 — final quantum-gravity boundary audit. Settles what is DERIVED, NEW PRIMITIVE, IMPORTED, and
/// whether anything is EMERGENT across the full QG arc.
///
/// Tests: ATQG400 (boundary census), ATQG401 (the two primitives + derived chain), ATQG402 (final boundary).
/// </summary>
public class ATQG_Phase40_FinalBoundaryAuditTests : ResearchTestBase
{
    public ATQG_Phase40_FinalBoundaryAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG400: boundary census ──────────────────────────────────────────────────────

    [Fact]
    public void ATQG400_BoundaryCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG400: DERIVED / EMERGENT / NEW PRIMITIVE / IMPORTED across 11 items");

        int derived = 0, emergent = 0, primitive = 0, imported = 0;
        foreach (var item in FinalBoundaryAudit.Items)
        {
            string c = FinalBoundaryAudit.Classify(item);
            sb.AppendLine($"{item,-22} -> {c}");
            switch (c)
            {
                case "DERIVED": derived++; break;
                case "EMERGENT": emergent++; break;
                case "NEW PRIMITIVE": primitive++; break;
                case "IMPORTED": imported++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"DERIVED       : {derived}");
        sb.AppendLine($"EMERGENT      : {emergent}");
        sb.AppendLine($"NEW PRIMITIVE : {primitive}");
        sb.AppendLine($"IMPORTED      : {imported}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(7, derived);
        Assert.Equal(0, emergent);
        Assert.Equal(2, primitive);
        Assert.Equal(2, imported);
    }

    // ── ATQG401: the two primitives + the derived chain ──────────────────────────────

    [Fact]
    public void ATQG401_TwoPrimitivesAndDerivedChain()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG401: two primitives, seven derived, two imported");

        int primitives = FinalBoundaryAudit.Primitives();
        bool nothingEmergent = !FinalBoundaryAudit.AnythingEmergent();

        sb.AppendLine($"primitives: {primitives}  (Q-events + ψ)");
        sb.AppendLine($"anything EMERGENT: {!nothingEmergent}");
        sb.AppendLine();
        sb.AppendLine("THE TWO PRIMITIVES:");
        sb.AppendLine("  1. Q-events (REAL-UNDERIVED actualization substrate, QG29)");
        sb.AppendLine("  2. ψ (the spin-2 tensor field, NEW PRIMITIVE, QG23/24/37)");
        sb.AppendLine();
        sb.AppendLine("THE DERIVED CHAIN (7 items, all from Q-events + principles):");
        sb.AppendLine("  counting measure → causal order → geometry → Einstein structure → matter → scalar gravity → saturation");
        sb.AppendLine();
        sb.AppendLine("THE IMPORTED (observationally supplied, force ψ):");
        sb.AppendLine("  GW observables (spin-2) and lensing observables (non-conformal)");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, primitives);
        Assert.True(nothingEmergent, "nothing should be emergent");
        Assert.Equal("NEW PRIMITIVE", FinalBoundaryAudit.Classify("q-events"));
        Assert.Equal("NEW PRIMITIVE", FinalBoundaryAudit.Classify("tensor-sector"));
    }

    // ── ATQG402: final boundary ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG402_FinalBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG402: the final quantum-gravity boundary");

        sb.AppendLine("THE FINAL BOUNDARY:");
        sb.AppendLine();
        sb.AppendLine("  PRIMITIVE (underived, 2):   Q-events, ψ (tensor)");
        sb.AppendLine("  DERIVED (7):                counting measure, causal order, geometry, Einstein structure,");
        sb.AppendLine("                              matter, scalar gravity, saturation physics");
        sb.AppendLine("  IMPORTED (2):               GW observables, lensing observables  [the observational demand for ψ]");
        sb.AppendLine("  EMERGENT (0):               nothing arises from collective behavior without being derived");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: AT's quantum-gravity boundary is TWO primitives (Q-events + ψ) and NOTHING else. The entire");
        sb.AppendLine("scalar backbone — from the counting measure through causal order, geometry, matter, and gravity to the");
        sb.AppendLine("regular-core saturation — is DERIVED from Q-events alone. The only underived additions are the tensor ψ,");
        sb.AppendLine("demanded by exactly two imported observables (lensing and gravitational waves). There is no emergent sector.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, FinalBoundaryAudit.Primitives());
        Assert.True(!FinalBoundaryAudit.AnythingEmergent());
        Assert.Equal("IMPORTED", FinalBoundaryAudit.Classify("gw-observables"));
        Assert.Equal("IMPORTED", FinalBoundaryAudit.Classify("lensing-observables"));
    }
}
