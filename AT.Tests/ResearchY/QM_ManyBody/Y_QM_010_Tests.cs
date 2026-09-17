using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_010 - Fingerprint Load Audit (group QM). Which surviving AT claims actually depend on the native
/// {1..6} spectrum? Strip the fingerprint-specific constants, compare theorem content only, and classify each claim
/// UNCHANGED / NUMERICALLY_CHANGED / STRUCTURALLY_CHANGED / REFUTED, with a load label CORE / FINGERPRINT / ARTEFACT.
/// </summary>
public sealed class Y_QM_010_Tests : ResearchTestBase
{
    public Y_QM_010_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private const int Native = 0b111111;
    private const int Schrodinger = 1;

    [Fact]
    public void Y_QM_010_EveryClaimIsCoveredAndClassifiedByTheRule()
    {
        PrintHeader("QM_010 - the eight claims, constants stripped, theorem content only");

        var sb = new StringBuilder();
        sb.AppendLine("  claim                    holds {1..6}  holds {1}  constants  objects   classification        load");
        foreach (var r in FingerprintLoadAudit.LoadTable())
            sb.AppendLine($"  {r.Claim,-24} {r.HoldsNative,-13} {r.HoldsOther,-10} {r.ConstantsMove,-10} {r.ObjectsMove,-9} {r.Classification,-21} {r.Load}");
        Output.WriteLine(sb.ToString());

        // the table cannot silently drop a claim of the question
        Assert.True(FingerprintLoadAudit.EveryClaimIsCovered());
        Assert.Equal(8, FingerprintLoadAudit.LoadTable().Length);

        // the classification is a function of the measured booleans: exercised on every branch so the rule itself is tested
        Assert.Equal("REFUTED", FingerprintLoadAudit.Classify(true, false, false, false));
        Assert.Equal("STRUCTURALLY_CHANGED", FingerprintLoadAudit.Classify(true, true, true, true));
        Assert.Equal("NUMERICALLY_CHANGED", FingerprintLoadAudit.Classify(true, true, true, false));
        Assert.Equal("UNCHANGED", FingerprintLoadAudit.Classify(true, true, false, false));

        // and the load label likewise
        Assert.Equal(FingerprintLoadAudit.Load.Fingerprint, FingerprintLoadAudit.LoadOf(false, false, false));
        Assert.Equal(FingerprintLoadAudit.Load.Fingerprint, FingerprintLoadAudit.LoadOf(true, true, false));
        Assert.Equal(FingerprintLoadAudit.Load.Core, FingerprintLoadAudit.LoadOf(true, false, false));

        // no claim is refuted on the replacement
        Assert.Equal(0, FingerprintLoadAudit.ClassificationCounts().Refuted);
        Assert.All(FingerprintLoadAudit.LoadTable(), r => Assert.True(r.HoldsOther, r.Claim));
    }

    [Fact]
    public void Y_QM_010_TheCoreClaimsAreSubstrateFree()
    {
        PrintHeader("QM_010 - the CORE block: the physical content the fingerprint cannot reach");

        var sb = new StringBuilder();
        foreach (var r in FingerprintLoadAudit.LoadTable().Where(r => r.Load == FingerprintLoadAudit.Load.Core))
            sb.AppendLine($"  {r.Claim,-24} {r.Classification,-21} {r.Constants}");
        Output.WriteLine(sb.ToString());

        var table = FingerprintLoadAudit.LoadTable();

        // the clock law: the law and the redshift identity that follows from it
        var clock = FingerprintLoadAudit.ClockLaw();
        Assert.True(clock.Holds);
        Assert.Equal(FingerprintLoadAudit.Load.Core, Row(table, "clock law").Load);
        Assert.Equal("UNCHANGED", Row(table, "clock law").Classification);

        // the redshift law: AT is always the smaller, and the inequality is substrate-free
        Assert.True(FingerprintLoadAudit.RedshiftLaw().Holds);
        Assert.Equal("UNCHANGED", Row(table, "redshift law").Classification);

        // the flux quantisation: the quantum is the INVERSE CYCLE LENGTH, not a shell count
        var flux = FingerprintLoadAudit.FluxQuantisation();
        Assert.True(flux.Holds);
        Assert.Equal(2.0 * Math.PI / 96.0, flux.Constants[0], 12);
        Assert.Equal(2.0 * Math.PI, flux.Constants[1], 12);            // a unit winding is one whole turn
        Assert.Equal("UNCHANGED", Row(table, "flux quantisation").Classification);

        // THE OBSERVABILITY THEOREM IS NOT CORE, AND MY DRAFT PREDICTED THAT IT WAS. Its predicate is
        // substrate-free (no invisible direction is silent to the clock, on either substratum, and every response is
        // first order), but the kernel it quantifies over has a fingerprint-dependent size - so the claim as stated
        // is NUMERICALLY_CHANGED and its load is FINGERPRINT. The measurement refused the prediction.
        var obsNative = FingerprintLoadAudit.ObservabilityOf(Native);
        var obsOther = FingerprintLoadAudit.ObservabilityOf(Schrodinger);
        Assert.Equal(0, obsNative.SilentDirections);
        Assert.Equal(0, obsOther.SilentDirections);
        Assert.Equal(obsNative.KernelDimension, obsNative.FirstOrderDirections);
        Assert.Equal(obsOther.KernelDimension, obsOther.FirstOrderDirections);
        Assert.NotEqual(obsNative.KernelDimension, obsOther.KernelDimension);
        Assert.Equal("NUMERICALLY_CHANGED", Row(table, "observability theorem").Classification);
        Assert.Equal(FingerprintLoadAudit.Load.Fingerprint, Row(table, "observability theorem").Load);

        // so the CORE block is THREE claims and not four
        Assert.Equal(3, FingerprintLoadAudit.LoadCounts().Core);

        // the recorded counts, so the rebuild is tied to the repository rather than to itself
        Assert.Equal(KernelObservableAudit.KernelDimension(), obsNative.KernelDimension);
        Assert.True(KernelObservableAudit.TheClockResolvesTheKernel());
    }

    [Fact]
    public void Y_QM_010_TheFingerprintClaimsCarryTheRealisationAndNotTheTheorem()
    {
        PrintHeader("QM_010 - the FINGERPRINT block: true theorems, substrate-fixed realisations");

        var sb = new StringBuilder();
        foreach (var r in FingerprintLoadAudit.LoadTable().Where(r => r.Load == FingerprintLoadAudit.Load.Fingerprint))
            sb.AppendLine($"  {r.Claim,-24} {r.Classification,-21} {r.Constants}");
        Output.WriteLine(sb.ToString());

        var table = FingerprintLoadAudit.LoadTable();

        // the split: the partition holds, the membership and the dimensions move
        Assert.Equal("STRUCTURALLY_CHANGED", Row(table, "amplitude/phase split").Classification);
        var splitNative = FingerprintLoadAudit.Split(Native);
        var splitOther = FingerprintLoadAudit.Split(Schrodinger);
        Assert.True(splitNative.Holds && splitOther.Holds);
        Assert.Equal(42.0, splitNative.Constants[0], 9);
        Assert.Equal(53.0, splitNative.Constants[1], 9);
        Assert.Equal(46.0, splitOther.Constants[0], 9);
        Assert.Equal(49.0, splitOther.Constants[1], 9);

        // the kernel theorem: the identity holds by construction and the dimension moves
        var kNative = FingerprintLoadAudit.KernelTheorem(Native);
        var kOther = FingerprintLoadAudit.KernelTheorem(Schrodinger);
        Assert.True(kNative.Holds && kOther.Holds);
        Assert.Equal(53.0, kNative.Constants[0], 9);
        Assert.Equal(49.0, kOther.Constants[0], 9);
        Assert.Equal("NUMERICALLY_CHANGED", Row(table, "kernel theorem").Classification);

        // the source law and the phase accessibility: the ranking and the count move
        Assert.Equal("STRUCTURALLY_CHANGED", Row(table, "source law").Classification);
        Assert.Equal("NUMERICALLY_CHANGED", Row(table, "phase accessibility").Classification);
        Assert.NotEqual(FingerprintLoadAudit.ObservabilityOf(Native).KernelDimension,
                        FingerprintLoadAudit.ObservabilityOf(Schrodinger).KernelDimension);

        // every fingerprint claim is TRUE on both substrata: the load is realisation, not failure
        Assert.All(table.Where(r => r.Load == FingerprintLoadAudit.Load.Fingerprint), r => Assert.True(r.HoldsOther, r.Claim));
        Assert.Equal(5, FingerprintLoadAudit.LoadCounts().Fingerprint);
        Assert.Equal(3, FingerprintLoadAudit.LoadCounts().Core);
    }

    [Fact]
    public void Y_QM_010_NoNamedClaimIsAnArtefactAndTheControlsShowTheLabel()
    {
        PrintHeader("QM_010 - the ARTEFACT label: defined mechanically, and honestly empty for the named claims");

        var sb = new StringBuilder();
        sb.AppendLine("  claim                     vacuous over 63   {1..6}      {1}         distinct   load");
        foreach (var c in FingerprintLoadAudit.Controls())
            sb.AppendLine($"  {c.Claim,-25} {c.PredicateHoldsOnEverySubset,-17} {c.Native,-11:F1} {c.Schrodinger,-11:F1} {c.DistinctValues,-10} "
                + $"{FingerprintLoadAudit.ControlLoad(c.PredicateHoldsOnEverySubset, c.Native != c.Schrodinger)}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, FingerprintLoadAudit.Controls().Length);

        // every control is VACUOUS over the 63 subsets and its number MOVES - the mechanical shape of an artefact
        Assert.All(FingerprintLoadAudit.Controls(), c => Assert.True(c.PredicateHoldsOnEverySubset, c.Claim));
        Assert.All(FingerprintLoadAudit.Controls(), c => Assert.True(Math.Abs(c.Native - c.Schrodinger) > 0.0, c.Claim));
        Assert.All(FingerprintLoadAudit.Controls(), c => Assert.True(c.DistinctValues > 1, c.Claim));

        // so the classifier produces the label
        Assert.All(FingerprintLoadAudit.Controls(), c =>
            Assert.Equal(FingerprintLoadAudit.Load.Artefact,
                FingerprintLoadAudit.ControlLoad(c.PredicateHoldsOnEverySubset, c.Native != c.Schrodinger)));

        // and the label is not free: a predicate that CAN fail is not an artefact, whatever its number does
        Assert.Equal(FingerprintLoadAudit.Load.Core, FingerprintLoadAudit.ControlLoad(false, true));

        // NO NAMED CLAIM IS AN ARTEFACT, and the audit says so rather than filling the category
        Assert.Equal(0, FingerprintLoadAudit.LoadCounts().Artefact);
        Assert.All(FingerprintLoadAudit.LoadTable(), r => Assert.NotEqual(FingerprintLoadAudit.Load.Artefact, r.Load));
    }

    [Fact]
    public void Y_QM_010_TheVerdictIsFingerprintLoad()
    {
        PrintHeader("QM_010 - the load and the verdict");

        var (core, fingerprint, artefact) = FingerprintLoadAudit.LoadCounts();
        var (unchanged, numerically, structurally, refuted) = FingerprintLoadAudit.ClassificationCounts();
        var sb = new StringBuilder();
        sb.AppendLine($"  load      : {core} CORE + {fingerprint} FINGERPRINT + {artefact} ARTEFACT");
        sb.AppendLine($"  theorems  : {unchanged} UNCHANGED + {numerically} NUMERICALLY_CHANGED + {structurally} STRUCTURALLY_CHANGED + {refuted} REFUTED");
        sb.AppendLine();
        sb.AppendLine(FingerprintLoadAudit.Verdict());
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, core);
        Assert.Equal(5, fingerprint);
        Assert.Equal(0, artefact);
        Assert.Equal(8, core + fingerprint + artefact);
        Assert.Equal(3, unchanged);
        Assert.Equal(3, numerically);
        Assert.Equal(2, structurally);
        Assert.Equal(8, unchanged + numerically + structurally + refuted);
        Assert.Equal(0, refuted);
        Assert.StartsWith("FINGERPRINT", FingerprintLoadAudit.Verdict());
    }

    private static (string Claim, bool HoldsNative, bool HoldsOther, bool ConstantsMove, bool ObjectsMove,
        string Classification, FingerprintLoadAudit.Load Load, string Constants)
        Row((string Claim, bool HoldsNative, bool HoldsOther, bool ConstantsMove, bool ObjectsMove,
            string Classification, FingerprintLoadAudit.Load Load, string Constants)[] table, string claim)
        => table.Single(r => r.Claim == claim);

    [Fact]
    public void Y_QM_010_Diag()
    {
        PrintHeader("QM_010 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(FingerprintLoadAudit.OutputTable());
        Output.WriteLine(FingerprintLoadAudit.OutputReadings());
        Output.WriteLine(FingerprintLoadAudit.OutputControls());
        Output.WriteLine(FingerprintLoadAudit.OutputVerdict());
    }
}
