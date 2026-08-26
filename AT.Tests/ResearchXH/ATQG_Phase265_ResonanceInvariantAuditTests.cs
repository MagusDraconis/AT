using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 265 — Resonance Invariant Audit. What is the actual conserved quantity behind the four
/// operators? D96 only, no observables.
/// </summary>
public class ATQG_Phase265_ResonanceInvariantAuditTests : ResearchTestBase
{
    public ATQG_Phase265_ResonanceInvariantAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2650_ConservedQuantity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2650: the conserved quantity — the total spectral weight");

        sb.AppendLine("HYPOTHESIS: the invariant is Σλ = Σω² over the 95 positive modes — the trace of the");
        sb.AppendLine("D96 Laplacian, which is basis-independent (Σλ = Σ degrees = 2·edges) and therefore");
        sb.AppendLine("CONSERVED under the N=96 resonance dynamics.");
        sb.AppendLine();

        sb.AppendLine($"Σλ = Σω² = {ResonanceInvariantAudit.TotalSpectralWeight():F8}");
        sb.AppendLine($"exactly integer: {ResonanceInvariantAudit.InvariantIsInteger()}");
        sb.AppendLine($"exactly 1152: {ResonanceInvariantAudit.SpectralWeightIs1152()}");
        sb.AppendLine($"Σλ = 12 × 96 = gauge degree (1+3+8) × cycle N: {ResonanceInvariantAudit.FactorsAsGaugeTimesCycle()}");
        sb.AppendLine($"Σλ = 2·24² (alternate): {ResonanceInvariantAudit.FactorsAsTwice24Squared()}");
        sb.AppendLine();
        sb.AppendLine("The trace is a graph invariant: Σλ = Σ degrees = 2·(number of edges) = 2·576.");
        sb.AppendLine("It cannot change under any diagonalization — it is a conserved quantity of the");
        sb.AppendLine("network whose spectrum the operators read.");

        Output.WriteLine(sb.ToString());

        Assert.True(ResonanceInvariantAudit.InvariantIsInteger(), "Σλ must be an exact integer");
        Assert.True(ResonanceInvariantAudit.SpectralWeightIs1152(), "Σλ = 1152 exactly");
        Assert.True(ResonanceInvariantAudit.FactorsAsGaugeTimesCycle(), "Σλ = 12×96 (gauge degree × cycle)");
    }

    [Fact]
    public void ATQG2651_OperatorsReadSameInvariant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2651: all operators are measurements of the one invariant");

        sb.AppendLine("HYPOTHESIS: each operator is a deterministic read of the SAME 95-mode list whose");
        sb.AppendLine("total weight is conserved — so they are different measurements of one invariant.");
        sb.AppendLine();

        sb.AppendLine("CROWDING     = degeneracy read: multiset [42×2,5,6] → Σm, Σ√m, Σm²");
        sb.AppendLine("COMPRESSION  = octave-band read: occupancies [4,4,87] → occMom");
        sb.AppendLine("BEAT         = extent read: span = ω_max/ω_min");
        sb.AppendLine("LOCKING      = gap read: λ₂ = ω_min²");
        sb.AppendLine();
        sb.AppendLine($"All operators read the same spectrum: {ResonanceInvariantAudit.AllOperatorsReadSameSpectrum()}");
        sb.AppendLine($"All sectors read the same invariant: {ResonanceInvariantAudit.AllSectorsReadSameInvariant()}");
        sb.AppendLine();
        sb.AppendLine("THE BEAT IDENTITIES (the reads are coupled — consistent with one invariant):");
        sb.AppendLine($"  Σ√m/span = {ResonanceInvariantAudit.SqrtMOverSpan():F6} ≈ 10");
        sb.AppendLine($"  occMom/Σm = {ResonanceInvariantAudit.OccMomOverSigmaM():F6} ≈ 20");
        sb.AppendLine($"  within 2%: {ResonanceInvariantAudit.BeatIdentitiesWithin2Percent()} identities");

        Output.WriteLine(sb.ToString());

        Assert.True(ResonanceInvariantAudit.AllOperatorsReadSameSpectrum());
        Assert.True(ResonanceInvariantAudit.AllSectorsReadSameInvariant());
        Assert.True(ResonanceInvariantAudit.BeatIdentitiesWithin2Percent() >= 2);
    }

    [Fact]
    public void ATQG2652_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2652: the invariant determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - NO INVARIANT (score ≤ 2), PARTIAL INVARIANT (3-4),");
        sb.AppendLine("    UNIVERSAL RESONANCE INVARIANT (5-6);");
        sb.AppendLine("  - the invariant candidate: the total spectral weight Σλ = Σω² (the conserved trace).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ResonanceInvariantAudit.Summary()}");
        sb.AppendLine($"Invariant score: {ResonanceInvariantAudit.InvariantScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {ResonanceInvariantAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The conserved quantity is the total spectral weight Σλ = Σω² = 1152 = 12×96");
        sb.AppendLine("    (gauge degree 1+3+8 × cycle N) — an EXACT structural identity.");
        sb.AppendLine("  - It is a genuine invariant: the trace of the Laplacian is basis-independent");
        sb.AppendLine("    (Σλ = Σ degrees = 2·edges), so it is conserved under the resonance dynamics");
        sb.AppendLine("    that generates the spectrum.");
        sb.AppendLine("  - CROWDING, COMPRESSION, BEAT and LOCKING are the four measurements (reads) of");
        sb.AppendLine("    this one list: degeneracy read, octave-band read, extent read, gap read.");
        sb.AppendLine("  - All five sectors (masses, couplings, mixings, cosmology, gravity) consume these");
        sb.AppendLine("    reads — they are different measurements of the one invariant.");
        sb.AppendLine("  - The beat identities (Σ√m/span ≈ 10, occMom/Σm ≈ 20) couple the reads, consistent");
        sb.AppendLine("    with a single conserved quantity.");
        sb.AppendLine("  - Honest caveat: the operator-to-sector assignment retains QG149-157-era target");
        sb.AppendLine("    information (QG261-264); the conserved quantity itself is D96-only and exact.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL RESONANCE INVARIANT", ResonanceInvariantAudit.Classify());
        Assert.True(ResonanceInvariantAudit.InvariantScore() >= 5);
        Assert.Contains("UNIVERSAL RESONANCE INVARIANT", ResonanceInvariantAudit.Summary());
    }
}
