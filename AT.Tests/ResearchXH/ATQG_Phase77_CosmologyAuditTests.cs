using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 77 — cosmology compatibility audit. Audits six cosmological features.
/// Classify: DERIVED / COMPATIBLE / UNKNOWN / MISSING.
///
/// Tests: ATQG770 (classification), ATQG771 (derived vs compatible), ATQG772 (gaps).
/// </summary>
public class ATQG_Phase77_CosmologyAuditTests : ResearchTestBase
{
    public ATQG_Phase77_CosmologyAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG770: classification ────────────────────────────────────────────────────

    [Fact]
    public void ATQG770_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG770: audit six cosmological features");

        int derived = 0, compatible = 0, unknown = 0, missing = 0;
        foreach (var f in CosmologyAudit.Features)
        {
            string c = CosmologyAudit.Classify(f);
            sb.AppendLine($"{f,-20} -> {c}");
            switch (c)
            {
                case "DERIVED": derived++; break;
                case "COMPATIBLE": compatible++; break;
                case "UNKNOWN": unknown++; break;
                case "MISSING": missing++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"DERIVED    : {derived}");
        sb.AppendLine($"COMPATIBLE : {compatible}");
        sb.AppendLine($"UNKNOWN    : {unknown}");
        sb.AppendLine($"MISSING    : {missing}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, derived);
        Assert.Equal(3, compatible);
        Assert.Equal(2, unknown);
        Assert.Equal(0, missing);
    }

    // ── ATQG771: derived vs compatible ─────────────────────────────────────────────

    [Fact]
    public void ATQG771_DerivedVsCompatible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG771: expansion derived; FRW/CMB/dark-matter compatible");

        sb.AppendLine("DERIVED:");
        sb.AppendLine("  • expansion — gravitational redshift (QG26) + scale-free ρ evolution (G4-RHO).");
        sb.AppendLine();
        sb.AppendLine("COMPATIBLE:");
        sb.AppendLine("  • FRW geometry — the conformal metric hosts FRW (a = ρ^(1/d));");
        sb.AppendLine("  • CMB isotropy — conformal isotropy;");
        sb.AppendLine("  • dark matter (effect) — the log-deficit gives flat rotation curves (G4-ME), not the particle.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", CosmologyAudit.Classify("expansion"));
        Assert.Equal("COMPATIBLE", CosmologyAudit.Classify("frw-geometry"));
        Assert.Equal("COMPATIBLE", CosmologyAudit.Classify("dark-matter"));
    }

    // ── ATQG772: gaps ──────────────────────────────────────────────────────────────

    [Fact]
    public void ATQG772_Gaps()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG772: the remaining cosmology gaps");

        foreach (var g in CosmologyAudit.Gaps)
            sb.AppendLine($"  • {g}");

        sb.AppendLine();
        sb.AppendLine("REMAINING GAPS:");
        sb.AppendLine("  1. Structure formation — density-perturbation growth and galaxy clustering are not derived;");
        sb.AppendLine("  2. Dark energy — Λ (accelerating expansion) is empirical.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network derives expansion and compatibly hosts FRW/CMB/dark-matter effects, but structure");
        sb.AppendLine("formation and dark energy remain UNKNOWN — the same open problems of standard cosmology.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, CosmologyAudit.Gaps.Length);
        Assert.Equal("UNKNOWN", CosmologyAudit.Classify("structure-formation"));
        Assert.Equal("UNKNOWN", CosmologyAudit.Classify("dark-energy"));
    }
}
