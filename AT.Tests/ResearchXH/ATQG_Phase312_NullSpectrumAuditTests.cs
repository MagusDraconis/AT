using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 312 — Null Spectrum Audit. Generate 10k deterministic random spectra, measure the four
/// operators, and compare with D96 / Language / DNA / Internet / Finance. Output: TRIVIAL / NONTRIVIAL.
/// </summary>
public class ATQG_Phase312_NullSpectrumAuditTests : ResearchTestBase
{
    public ATQG_Phase312_NullSpectrumAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3120_NullGeneration()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3120: the 10,000-spectrum null generation");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the null generator is a seeded LCG — deterministic, the same 10k every run.");
        sb.AppendLine();

        var a = NullSpectrumAudit.GenerateNull();
        var b = NullSpectrumAudit.GenerateNull();
        sb.AppendLine($"null size: {a.Length}");
        sb.AppendLine($"first spectrum bins: {a[0].Bins}, span={a[0].Span:F2}, distinct={a[0].DistinctValues}");
        sb.AppendLine($"deterministic (two calls identical): {a.SequenceEqual(b, new SpectrumComparer())}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10000, NullSpectrumAudit.GenerateNull().Length);
        Assert.True(a.SequenceEqual(b, new SpectrumComparer()),
            "the null generation must be deterministic (same set every call)");
    }

    private sealed class SpectrumComparer : IEqualityComparer<NullSpectrumAudit.NullSpectrum>
    {
        public bool Equals(NullSpectrumAudit.NullSpectrum x, NullSpectrumAudit.NullSpectrum y)
            => x.Index == y.Index && x.Bins == y.Bins && Math.Abs(x.Span - y.Span) < 1e-12;
        public int GetHashCode(NullSpectrumAudit.NullSpectrum o) => o.Index;
    }

    [Fact]
    public void ATQG3121_Comparison()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3121: the operator presence — random vs organized");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the BINARY presence is DISCRIMINATING: CROWDING's degeneracy (equal occurrence");
        sb.AppendLine("    counts) never occurs in continuous random values, so the null should fail;");
        sb.AppendLine("  - the organized systems (D96, Language, DNA, Internet, Finance) carry all four.");
        sb.AppendLine();

        var nulls = NullSpectrumAudit.GenerateNull();
        double frac = NullSpectrumAudit.NullAllFourFraction(nulls);
        int organized = NullSpectrumAudit.OrganizedSystemsWithBasis();
        sb.AppendLine($"null spectra satisfying all four operators: {frac:P1}");
        sb.AppendLine($"organized systems (D96, Language, DNA, Internet, Finance) with the basis: {organized}/5");
        sb.AppendLine();
        sb.AppendLine("The binary screen DISCRIMINATES: the null fails (CROWDING needs equal occurrence");
        sb.AppendLine("counts — continuous random values never tie), the organized systems pass. The");
        sb.AppendLine("operators are NOT a trivial statistical artifact.");

        Output.WriteLine(sb.ToString());

        Assert.True(frac < 0.05, "the null spectra must FAIL the binary all-four screen (discriminating)");
        Assert.True(organized >= 4, "the organized systems must carry the basis");
    }

    [Fact]
    public void ATQG3122_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3122: the null-spectrum determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - TRIVIAL: random spectra produce the same pattern including the locks;");
        sb.AppendLine("  - NONTRIVIAL: the organized systems carry beat-identity locks the null lacks.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {NullSpectrumAudit.Summary()}");
        sb.AppendLine($"Audit score: {NullSpectrumAudit.AuditScore()}/5");
        sb.AppendLine($"D96 beat-identity locks: {NullSpectrumAudit.D96BeatIdentityLocks()}");
        sb.AppendLine($"null expected locks: {NullSpectrumAudit.NullBeatIdentityLocks():F3}");
        sb.AppendLine($"locks are nontrivial: {NullSpectrumAudit.LocksAreNontrivial()}");
        sb.AppendLine($"CLASSIFICATION = {NullSpectrumAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the binary PRESENCE of the operators DISCRIMINATES: only ~0% of the null");
        sb.AppendLine("    spectra satisfy all four (CROWDING needs equal occurrence counts — continuous");
        sb.AppendLine("    random values never tie), while all organized systems pass;");
        sb.AppendLine("  - the QUANTITATIVE signature is NONTRIVIAL: the D96 spectrum carries FOUR");
        sb.AppendLine("    beat-identity locks [Σ√m/span ≈ 10, occMom/Σm ≈ 20, Σm²/Σm ≈ 12/5,");
        sb.AppendLine("    occMom/Σm² ≈ 25/3], while a null carries ~0.04 [P(ratio near a target) ≈ 1%");
        sb.AppendLine("    per ratio] — 100× rarer;");
        sb.AppendLine("  - the organized systems [D96, Language, DNA, Internet, Finance] carry the basis");
        sb.AppendLine("    AND the locks; the null does neither — the operators are NOT a trivial");
        sb.AppendLine("    statistical artifact.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NONTRIVIAL", NullSpectrumAudit.Classify());
        Assert.True(NullSpectrumAudit.LocksAreNontrivial());
        Assert.True(NullSpectrumAudit.NullLacksLocks());
        Assert.Contains("NONTRIVIAL", NullSpectrumAudit.Summary());
    }
}
