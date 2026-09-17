using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_011 - Fingerprint Justification Audit (group QM). What surviving result requires the EXACT native
/// spectrum rather than only its existence? Compare {1..6} against {1}, classify each claim
/// REQUIRES_NATIVE / REQUIRES_SPECTRUM_ONLY / INDEPENDENT, and find the first genuinely physical consequence of the
/// native fingerprint - stating explicitly that none exists if none does.
/// </summary>
public sealed class Y_QM_011_Tests : ResearchTestBase
{
    public Y_QM_011_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private static (string Claim, string Symbol, string File, bool HoldsNative, bool HoldsOther,
        string NativeValue, string OtherValue, FingerprintJustificationAudit.Need Need, bool IsObservable)[] Table
        => FingerprintJustificationAudit.ClaimTable();

    [Fact]
    public void Y_QM_011_TheSearchClassifiesEveryClaimFromThreeMeasuredInputs()
    {
        PrintHeader("QM_011 - the search: every surviving claim, classified from measured inputs");

        var sb = new StringBuilder();
        sb.AppendLine("  claim                                    holds {1..6}  holds {1}  native value      {1} value         need                observable");
        foreach (var r in Table)
            sb.AppendLine($"  {r.Claim,-40} {r.HoldsNative,-13} {r.HoldsOther,-10} {r.NativeValue,-17} {r.OtherValue,-17} {r.Need,-19} {r.IsObservable}");
        Output.WriteLine(sb.ToString());

        // the table is not empty, every claim has a file that was FOUND, and the three categories are all populated
        Assert.NotEmpty(Table);
        Assert.All(Table, r => Assert.True(FingerprintJustificationAudit.SpectrumReferences(r.File) >= 0, r.File));
        var (native, spectrumOnly, independent) = FingerprintJustificationAudit.NeedCounts();
        Assert.True(native > 0, "some claim must require the native spectrum, or the search is vacuous");
        Assert.True(spectrumOnly > 0);
        Assert.True(independent > 0);
        Assert.Equal(Table.Length, native + spectrumOnly + independent);

        // and the claims that require the native spectrum are exactly the ones whose predicate FAILS on {1}
        Assert.All(Table.Where(r => r.Need == FingerprintJustificationAudit.Need.RequiresNative),
            r => Assert.False(r.HoldsOther, r.Claim));
        Assert.All(Table.Where(r => r.Need != FingerprintJustificationAudit.Need.RequiresNative),
            r => Assert.True(r.HoldsOther, r.Claim));
    }

    [Fact]
    public void Y_QM_011_NoObservableInTheRepositoryIsSpectral()
    {
        PrintHeader("QM_011 - the structural key: the observable inventory names no spectral object");

        var (size, offenders, list) = FingerprintJustificationAudit.NoObservableIsSpectral();
        var sb = new StringBuilder();
        sb.AppendLine($"  observables in the repository's inventory: {size}");
        sb.AppendLine($"  of those naming a spectral object: {offenders}" + (offenders > 0 ? " - " + string.Join(", ", list) : ""));
        sb.AppendLine("  the inventory: " + string.Join(", ", FingerprintJustificationAudit.ObservableInventory()));
        Output.WriteLine(sb.ToString());

        // the inventory is the repository's own, and it is non-empty
        Assert.True(size >= 11, $"the inventory has {size} entries, fewer than the eleven recorded");
        Assert.Equal(0, offenders);

        // the positives: the clock and the redshift ARE observables, and the clock is checkable by name
        Assert.True(FingerprintJustificationAudit.IsObservable("ClockRateAT"));
        Assert.True(FingerprintJustificationAudit.IsObservable("RedshiftAT"));
        Assert.Contains("lensing-deflection", FingerprintJustificationAudit.ObservableInventory());

        // the negatives: a spectral object is not
        Assert.False(FingerprintJustificationAudit.IsObservable("ModeEigenvalues"));
        Assert.False(FingerprintJustificationAudit.IsObservable("DistinctLevels"));
        Assert.False(FingerprintJustificationAudit.IsObservable(""));
    }

    [Fact]
    public void Y_QM_011_NoPhysicalConsequenceOfTheFingerprintExists()
    {
        PrintHeader("QM_011 - the answer: the search for a physical consequence comes back empty");

        var nativeClaims = FingerprintJustificationAudit.NativeSpecificClaims();
        var sb = new StringBuilder();
        sb.AppendLine("  claims requiring the exact native spectrum:");
        foreach (var r in nativeClaims)
            sb.AppendLine($"    {r.Claim,-40} {r.NativeValue,-17} against {r.OtherValue}");
        sb.AppendLine();
        sb.AppendLine($"  of those carrying an observable: {FingerprintJustificationAudit.ObservablesRequiringNative().Length}");
        Output.WriteLine(sb.ToString());

        // THE GOAL'S ANSWER: the intersection is EMPTY, asserted as a list rather than argued in prose
        Assert.NotEmpty(nativeClaims);
        Assert.Empty(FingerprintJustificationAudit.ObservablesRequiringNative());

        // and every claim that requires the native spectrum is non-observable, which is the reason
        Assert.All(Table.Where(r => r.Need == FingerprintJustificationAudit.Need.RequiresNative),
            r => Assert.False(r.IsObservable, r.Claim));

        // while the physical claims are independent of the fingerprint
        Assert.Equal(FingerprintJustificationAudit.Need.Independent, Table.Single(r => r.Symbol == "ClockRateAT").Need);
        Assert.Equal(FingerprintJustificationAudit.Need.Independent, Table.Single(r => r.Symbol == "RedshiftAT").Need);
        Assert.Equal(FingerprintJustificationAudit.Need.Independent, Table.Single(r => r.Symbol == "CycleHolonomy").Need);
    }

    [Fact]
    public void Y_QM_011_TheNearMissIsADynamicalDifferenceWithNoObservable()
    {
        PrintHeader("QM_011 - the near miss: the fold is a real dynamical difference and not an observable");

        var sb = new StringBuilder();
        sb.AppendLine("  quantity                                {1..6}          {1}");
        foreach (var d in FingerprintJustificationAudit.DynamicalDifferences())
            sb.AppendLine($"  {d.Quantity,-40} {d.Native,-15} {d.Other}");
        Output.WriteLine(sb.ToString());

        // the group velocity REVERSES inside the band natively and never for {1} - a measured physical difference
        int reversedNative = FingerprintJustificationAudit.ReversedChannels(0b111111);
        int reversedOther = FingerprintJustificationAudit.ReversedChannels(1);
        Assert.True(reversedNative > 0, "the native dispersion must reverse somewhere, or the fold is not dynamical");
        Assert.Equal(0, reversedOther);

        // AND MY FIRST REASONING ABOUT HOW MANY CHANNELS REVERSE WAS WRONG: I expected every channel from the fold to
        // the zone edge (48 - 11 = 37). The measurement gives 23, because the symbol is a sum of SIX sinusoids and the
        // group velocity therefore oscillates back above zero rather than staying negative - a single fold position
        // does not bound the reversed region. The measured value is asserted instead of the reasoning.
        Assert.Equal(23, reversedNative);
        Assert.NotEqual(48 - 11, reversedNative);

        // and the fold's position is the native claim that carries it
        var foldRow = Table.Single(r => r.Symbol == "FirstFold");
        Assert.Equal(FingerprintJustificationAudit.Need.RequiresNative, foldRow.Need);
        Assert.Equal("11", foldRow.NativeValue);
        Assert.Equal("0", foldRow.OtherValue);
        Assert.False(foldRow.IsObservable);

        // the peak group velocity differs too, so the difference is not only a sign change
        Assert.NotEqual(FingerprintJustificationAudit.PeakGroupVelocity(0b111111),
                        FingerprintJustificationAudit.PeakGroupVelocity(1), 6);
    }

    [Fact]
    public void Y_QM_011_TheVerdictStatesExplicitlyThatNoneExists()
    {
        PrintHeader("QM_011 - the verdict");

        var (native, spectrumOnly, independent) = FingerprintJustificationAudit.NeedCounts();
        var sb = new StringBuilder();
        sb.AppendLine($"  search    : {native} REQUIRES_NATIVE + {spectrumOnly} REQUIRES_SPECTRUM_ONLY + {independent} INDEPENDENT");
        sb.AppendLine($"  physical  : {FingerprintJustificationAudit.ObservablesRequiringNative().Length} observables require the native spectrum");
        sb.AppendLine();
        sb.AppendLine(FingerprintJustificationAudit.Verdict());
        Output.WriteLine(sb.ToString());

        // the verdict's first word is the audit's answer, and it is not hedged
        Assert.StartsWith("NONE", FingerprintJustificationAudit.Verdict());
        Assert.Contains("NO GENUINELY PHYSICAL CONSEQUENCE", FingerprintJustificationAudit.Verdict());
        Assert.Contains("EXPLICITLY", FingerprintJustificationAudit.Verdict());
        Assert.Equal(0, FingerprintJustificationAudit.ObservablesRequiringNative().Length);
    }

    [Fact]
    public void Y_QM_011_Diag()
    {
        PrintHeader("QM_011 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(FingerprintJustificationAudit.OutputClaims());
        Output.WriteLine(FingerprintJustificationAudit.OutputInventory());
        Output.WriteLine(FingerprintJustificationAudit.OutputDynamical());
        Output.WriteLine(FingerprintJustificationAudit.OutputVerdict());
    }
}
