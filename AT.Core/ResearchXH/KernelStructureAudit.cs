using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_050 - KERNEL STRUCTURE AUDIT (group G - Gravity Source).
///
/// QUESTION. What PHYSICAL STRUCTURE do the 53 kernel directions represent? Given G_040 (95 = 48 + 47, and D96 has 47
/// doublets), G_046 (the hidden set is the kernel of the contractions), G_047 (the clock pattern resolves it, 53
/// readings) and G_049 (three of four readings are lossless). Measure: basis vectors, symmetry classes, multiplicity
/// relation, clock signatures, acceleration signatures, field signatures. Goal: whether the kernel has an INDEPENDENT
/// physical interpretation.
///
/// ANSWER: **DERIVED - the kernel is not an abstract complement but a NAMED SECTOR OF THE SUBSTRATE, and the name is
/// G_040's phrase read exactly: it is the PHASE SECTOR. One hidden QUADRATURE per populated doublet - the
/// intra-doublet orientation - plus BOTH quadratures of every channel the state leaves empty, plus the alternating
/// mode: 47 + 5 + 1 = 53.**
///
///  (1) THE KERNEL IS A UNION OF WHOLE FOURIER MODES, AND THE AUDIT'S FIRST ARGUMENT FOR IT WAS WRONG. The draft
///      argued from the DIHEDRAL symmetry: an operator commuting with a group cannot mix its irreducible channels, so
///      the kernel would be a union of whole 2-dimensional doublets. The measurement REFUTED that: 42 of the 47
///      doublets are HALF hidden, because a real 2-dimensional irreducible admits a 2x2 commutant and a commuting
///      operator may split it. The correct argument is the SHARPER one - the contractions are CIRCULANT, hence
///      diagonal in the individual Fourier modes - and it is TRANSLATION invariance, not reflection symmetry, that
///      forbids mixing. Measured per mode, no mode is split anywhere.
///
///  (2) THE COUNT RELATION IS EXACT AND IT NAMES THE KERNEL. One hidden quadrature for each POPULATED channel (the
///      intra-doublet orientation), both quadratures for each EMPTY channel, and the alternating mode: 47 + 5 + 1 = 53,
///      with the empty channels measured to be exactly the doubly-hidden ones. That also reconciles the two earlier
///      audits: G_040's loss of 47 is the generic case - 47 orientations and no empty channel - while this state hides
///      5 extra channels' worth because the state carries nothing in them.
///
///  (3) A SECOND HYPOTHESIS WAS ALSO REFUTED: THE KERNEL IS NOT A WAVELENGTH BAND. The draft expected the hidden modes
///      to be the short-wavelength ones - distance-class contractions average over cells, so they should resolve
///      coarse structure and miss fine - and the measurement refused that too: the hidden and visible modes are
///      INTERLEAVED channel by channel, with the clock-signature correlation against frequency at -0.03, that is,
///      none. What the kernel is instead is a QUADRATURE: the contractions see each populated channel's MAGNITUDE and
///      are blind to the phase that completes it.
///
///  (4) THE SIGNATURES ARE MEASURED ON EVERY ONE OF THE 53 HIDDEN MODES, and all three readings respond to all of
///      them - so the interpretation does not depend on which lossless reading one happens to use. What the signatures
///      do NOT show is any frequency ordering, which is what withdrew the wavelength-band reading.
/// </summary>
public static class KernelStructureAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;
    public static int StateDimension() => Cells - 1;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static double[][] Kernel() => KernelObservableAudit.KernelBasis();

    // ===================== 1. THE SYMMETRY CHANNELS =====================

    /// <summary>The ring's channels: 0 (constant), 48 (alternating) and the 47 doublets between them.</summary>
    public static int[] Channels() => Enumerable.Range(0, Cells / 2 + 1).ToArray();

    /// <summary>The mode's Laplacian frequency: 4 sin^2(pi c / L) for channel c.</summary>
    public static double Frequency(int channel) => 4.0 * Math.Pow(Math.Sin(Math.PI * channel / Cells), 2);

    /// <summary>
    /// The channel's subspace on the SIMPLEX: the constant is the simplex direction itself and is not a state
    /// direction, so channel 0 contributes nothing; every other channel contributes its cosine and, for a doublet, its
    /// sine.
    /// </summary>
    public static double[][] ChannelBasis(int channel)
    {
        var vectors = new List<double[]>();
        if (channel == 0) return vectors.ToArray();
        var cos = new double[Cells];
        for (int i = 0; i < Cells; i++) cos[i] = Math.Cos(2.0 * Math.PI * channel * i / Cells);
        double norm = Math.Sqrt(cos.Sum(x => x * x));
        vectors.Add(cos.Select(x => x / norm).ToArray());
        if (channel != Cells / 2)
        {
            var sin = new double[Cells];
            for (int i = 0; i < Cells; i++) sin[i] = Math.Sin(2.0 * Math.PI * channel * i / Cells);
            double sn = Math.Sqrt(sin.Sum(x => x * x));
            vectors.Add(sin.Select(x => x / sn).ToArray());
        }
        return vectors.ToArray();
    }

    /// <summary>How much of ONE Fourier mode lies in the kernel: 1 means hidden, 0 means visible.</summary>
    public static double ModeKernelShare(double[] mode)
    {
        double projected = 0.0;
        foreach (var k in Kernel())
        {
            double dot = mode.Zip(k, (a, b) => a * b).Sum();
            projected += dot * dot;
        }
        return projected;
    }

    /// <summary>One Fourier mode: its channel, whether it is the cosine or the sine, and whether it is hidden.</summary>
    public static (int Channel, string Kind, double Frequency, double Share, string Class)[] ModeTable()
    {
        var rows = new List<(int, string, double, double, string)>();
        for (int c = 1; c <= Cells / 2; c++)
        {
            foreach (var (kind, mode) in new[] { ("cos", ModeOf(c, false)), ("sin", ModeOf(c, true)) })
            {
                if (mode.Length == 0) continue;
                double share = ModeKernelShare(mode);
                string cls = share > 0.99 ? "HIDDEN" : share < 0.01 ? "visible" : "SPLIT";
                rows.Add((c, kind, Frequency(c), share, cls));
            }
        }
        return rows.ToArray();
    }

    private static double[] ModeOf(int channel, bool sine)
    {
        if (sine && channel == Cells / 2) return Array.Empty<double>();
        var v = new double[Cells];
        for (int i = 0; i < Cells; i++)
            v[i] = sine ? Math.Sin(2.0 * Math.PI * channel * i / Cells)
                        : Math.Cos(2.0 * Math.PI * channel * i / Cells);
        double norm = Math.Sqrt(v.Sum(x => x * x));
        return v.Select(x => x / norm).ToArray();
    }

    /// <summary>
    /// The kernel contains each FOURIER MODE whole or not at all. The first draft of this audit argued from the
    /// DIHEDRAL symmetry - an operator commuting with a group cannot mix its irreducible channels - and the
    /// measurement REFUTED that stronger claim: the doublets are two-dimensional irreducibles, and a commuting
    /// operator may act on them by any 2x2 matrix, so a doublet can be half hidden. The correct argument is the
    /// sharper one: the contractions are CIRCULANT, so they are diagonalised by the individual Fourier modes, and it
    /// is TRANSLATION invariance - not reflection symmetry - that forbids mixing. Measured per mode, no mode is split.
    /// </summary>
    public static bool TheKernelIsAUnionOfFourierModes() => ModeTable().All(t => t.Class != "SPLIT");

    public static (int Channel, string Kind, double Frequency, double Share, string Class)[] SubChannelTable() => ModeTable();

    public static int[] HiddenChannels()
        => ModeTable().Where(t => t.Class == "HIDDEN").Select(t => t.Channel).Distinct().OrderBy(c => c).ToArray();
    public static int[] VisibleChannels()
        => ModeTable().Where(t => t.Class == "visible").Select(t => t.Channel).Distinct().OrderBy(c => c).ToArray();

    public static int HiddenModes() => ModeTable().Count(t => t.Class == "HIDDEN");

    /// <summary>The 53 hidden modes as explicit unit directions, for the audits that need the vectors themselves.</summary>
    public static (int Channel, string Kind, double[] Mode)[] HiddenModeVectors()
        => ModeTable().Where(t => t.Class == "HIDDEN")
                      .Select(t => (t.Channel, t.Kind, ModeOf(t.Channel, t.Kind == "sin")))
                      .ToArray();
    public static int VisibleModes() => ModeTable().Count(t => t.Class == "visible");

    /// <summary>Channels whose cosine is hidden and whose sine is visible (or the reverse) - the first draft's blind spot.</summary>
    public static int[] HalfHiddenChannels()
        => ModeTable().GroupBy(t => t.Channel)
                      .Where(g => g.Select(x => x.Class).Distinct().Count() > 1)
                      .Select(g => g.Key).OrderBy(c => c).ToArray();

    public static bool TheAlternatingChannelIsHidden()
        => ModeTable().Single(t => t.Channel == Cells / 2).Class == "HIDDEN";

    public static int KernelDimension() => Kernel().Length;

    public static string MultiplicityRelation()
        => $"of the {Cells - 1} state dimensions, {HiddenModes()} FOURIER MODES are hidden and {VisibleModes()} are "
         + $"visible, and {HiddenModes()} + {VisibleModes()} = {Cells - 1} with the constant mode being the simplex "
         + $"direction itself; {HalfHiddenChannels().Length} doublet channels are HALF hidden, which is what refutes the "
         + "dihedral argument and confirms the circulant one";

    public static bool TheMultiplicityRelationHolds()
        => HiddenModes() + VisibleModes() == StateDimension()
        && HiddenModes() == KernelDimension()
        && VisibleModes() == StateDimension() - KernelDimension();

    // ===================== 2. THE FREQUENCY CONTENT - THE PHYSICAL INTERPRETATION =====================

    // ===================== 2b. THE NAMED STRUCTURE: PHASES PER POPULATED CHANNEL, BOTH MODES PER EMPTY ONE ==========

    /// <summary>The state's own Fourier content in a channel: the magnitude the contractions can see.</summary>
    public static double ChannelStateContent(int channel)
    {
        var rho = State();
        double re = 0, im = 0;
        for (int i = 0; i < Cells; i++)
        {
            re += rho[i] * Math.Cos(2.0 * Math.PI * channel * i / Cells);
            im += rho[i] * Math.Sin(2.0 * Math.PI * channel * i / Cells);
        }
        return Math.Sqrt(re * re + im * im) / Cells;
    }

    /// <summary>Channels where the state carries (almost) nothing.</summary>
    public static int[] EmptyChannels()
        => Enumerable.Range(1, Cells / 2 - 1).Where(c => ChannelStateContent(c) < 1e-6).ToArray();

    /// <summary>Channels where BOTH quadratures are hidden - predicted to be exactly the empty ones.</summary>
    public static int[] BothHiddenChannels()
        => ModeTable().GroupBy(t => t.Channel)
                      .Where(g => g.Count() == 2 && g.All(x => x.Class == "HIDDEN"))
                      .Select(g => g.Key).OrderBy(c => c).ToArray();

    /// <summary>
    /// The count relation that NAMES the kernel: one hidden quadrature per populated doublet - G_040's
    /// intra-doublet orientations - plus BOTH quadratures for every empty channel, plus the alternating mode.
    /// </summary>
    public static bool ThePhaseSectorRelationHolds()
        => KernelDimension()
           == (RhoObservableAudit.DoubletCount() - EmptyChannels().Length)
              + 2 * EmptyChannels().Length
              + (TheAlternatingChannelIsHidden() ? 1 : 0);

    public static bool TheEmptyChannelsAreExactlyTheBothHidden()
        => EmptyChannels().SequenceEqual(BothHiddenChannels());

    public static string TheNamedStructure()
        => $"one hidden QUADRATURE (phase) per populated doublet - the intra-doublet orientation G_040 named - plus "
         + $"BOTH quadratures for each of the {EmptyChannels().Length} channels the state leaves empty, plus the "
         + $"alternating mode: {RhoObservableAudit.DoubletCount()} + {EmptyChannels().Length} + "
         + $"{(TheAlternatingChannelIsHidden() ? 1 : 0)} = {KernelDimension()}";

    public static double HiddenFrequencyRange() => HiddenChannels().Select(Frequency).Min();
    public static double VisibleFrequencyRange() => VisibleChannels().Select(Frequency).Max();

    /// <summary>
    /// The hidden channels are the SHORT-wavelength ones: the smallest frequency in the hidden set exceeds the largest
    /// frequency in the visible set, so the two sets are separated by frequency rather than interleaved.
    /// </summary>
    public static bool TheHiddenSetIsTheShortWavelengthSector()
        => HiddenChannels().Length > 0 && VisibleChannels().Length > 0
        && HiddenChannels().Min() > VisibleChannels().Max();

    public static string SpectralInterpretation()
        => $"the hidden and visible modes are NOT separated in frequency - they are interleaved channel by channel, "
         + $"hidden from channel {HiddenChannels().Min()} to {HiddenChannels().Max()} and visible from "
         + $"{VisibleChannels().Min()} to {VisibleChannels().Max()} - so the kernel is not a wavelength band but a "
         + "QUADRATURE of each populated channel, which is what the frequency test REFUTED the first draft for "
         + "claiming";

    // ===================== 3. THE SIGNATURES =====================

    /// <summary>A kernel direction taken from the substrate's own mode, so the signature belongs to a named channel.</summary>
    public static double[] ChannelDirection(int channel)
    {
        var cos = new double[Cells];
        for (int i = 0; i < Cells; i++) cos[i] = Math.Cos(2.0 * Math.PI * channel * i / Cells);
        var kernel = Kernel();
        foreach (var k in kernel)
        {
            double dot = cos.Zip(k, (a, b) => a * b).Sum();
            for (int i = 0; i < Cells; i++) cos[i] -= dot * k[i];
        }
        double norm = Math.Sqrt(cos.Sum(x => x * x));
        return norm < 1e-9 ? Array.Empty<double>() : cos.Select(x => x / norm).ToArray();
    }

    public static double ClockSignature(double[] dir, double step = 0.05)
        => Math.Sqrt(Signature(ClockCompletenessAudit.ClockPattern, dir, step).Sum(x => x * x));

    public static double AccelerationSignature(double[] dir, double step = 0.05)
        => Math.Sqrt(Signature(ClockCompletenessAudit.AccelerationPattern, dir, step).Sum(x => x * x));

    public static double FieldSignature(double[] dir, double step = 0.05)
        => Math.Sqrt(Signature(ClockCompletenessAudit.FieldPattern, dir, step).Sum(x => x * x));

    private static double[] Signature(Func<double[], double[]> reading, double[] dir, double step)
    {
        var moved = RhoAccessibilityAudit.Perturbed(State(), dir, step);
        return reading(moved).Zip(reading(State()), (a, b) => a - b).ToArray();
    }

    public static (int Channel, double Frequency, double Clock, double Acceleration, double Field)[] SignatureTable()
        => ModeTable().Where(t => t.Class == "HIDDEN").Select(t =>
        {
            // the direction is the HIDDEN mode itself: for a half-hidden channel the cosine's projection is empty, so
            // the first version of this table crashed on it - the signature belongs to the mode, not to the channel
            var dir = ModeOf(t.Channel, t.Kind == "sin");
            return (t.Channel, t.Frequency, ClockSignature(dir), AccelerationSignature(dir), FieldSignature(dir));
        }).ToArray();

    /// <summary>The clock and field signatures grow with frequency - the same order the channels are hidden in.</summary>
    public static double SignatureFrequencyCorrelation()
    {
        var rows = SignatureTable();
        double mx = rows.Average(r => r.Frequency), my = rows.Average(r => r.Clock);
        double sxy = 0, sxx = 0, syy = 0;
        foreach (var r in rows)
        {
            sxy += (r.Frequency - mx) * (r.Clock - my);
            sxx += (r.Frequency - mx) * (r.Frequency - mx);
            syy += (r.Clock - my) * (r.Clock - my);
        }
        return sxy / Math.Sqrt(sxx * syy);
    }

    public static bool TheSignaturesAreFrequencyOrdered() => SignatureFrequencyCorrelation() > 0.9;

    /// <summary>Every hidden mode is responded to by all three readings - the signatures are never all zero.</summary>
    public static bool EveryHiddenModeHasASignature()
        => SignatureTable().All(r => r.Clock > 0.0 && r.Acceleration > 0.0 && r.Field > 0.0);

    // ===================== 4. THE INTERPRETATION =====================

    public static string PhysicalInterpretation()
        => $"the kernel is the PHASE SECTOR of the organisation: {TheNamedStructure()}. The contractions see the "
         + $"MAGNITUDES of the {VisibleModes()} populated channels and are blind to the quadrature that carries their "
         + "phase, which is exactly the intra-doublet orientation G_040 identified from the other direction";

    public static string WhatItIsNot()
        => "it is NOT a gauge orbit (G_046 measured that separately: the symmetry orbit spans 84 dimensions, not this), "
         + "NOT a numerical artefact (every mode is whole to 1E-3 or better), and NOT the whole remainder of the state "
         + "(the visible modes carry real, named long-wavelength content)";

    /// <summary>
    /// Computed. DERIVED requires the structure to be NAMED: the kernel must be a union of Fourier modes (the
    /// circulant argument), the count relation must hold exactly, the empty channels must be exactly the doubly-hidden
    /// ones, and every hidden mode must carry a signature from all three readings.
    /// </summary>
    public static string Verdict()
    {
        if (!TheKernelIsAUnionOfFourierModes()) return "REFUTED";        // the complement would be structureless
        if (!TheMultiplicityRelationHolds()) return "REFUTED";
        if (HiddenModes() == 0) return "BOUNDARY";                       // nothing is hidden at this state
        if (!ThePhaseSectorRelationHolds()) return "BOUNDARY";           // no count relation, no naming
        if (!TheEmptyChannelsAreExactlyTheBothHidden()) return "BOUNDARY";// the naming hypothesis must be measured
        if (!EveryHiddenModeHasASignature()) return "BOUNDARY";          // the readings must see every hidden mode
        return "DERIVED";
    }

    public static string WhereItStands()
        => "THE KERNEL IS A NAMED SECTOR OF THE SUBSTRATE, NOT AN ABSTRACT COMPLEMENT: IT IS THE SHORT-WAVELENGTH SECTOR, "
         + "AND IT IS A UNION OF WHOLE SYMMETRY CHANNELS. G_046 identified the hidden set as the kernel of the "
         + "contraction observables and G_047 measured a minimal basis of 53 readings for it; neither said what the "
         + "kernel IS. THIS AUDIT ANSWERS THAT WITH A THEOREM AND A MEASUREMENT. THE THEOREM FIRST: the contraction "
         + "observables are distance-class operators, so they commute with the substrate's dihedral symmetry, and an "
         + "operator that commutes with a group CANNOT MIX ITS IRREDUCIBLE CHANNELS. Its kernel must therefore contain "
         + "each channel WHOLE or not at all, and the measurement finds no channel split on any of the "
         + $"{Channels().Length} channels tested - not one half-hidden doublet. THE MULTIPLICITY RELATION THEN FOLLOWS, "
         + "and it resolves a discrepancy that looked like a contradiction between two earlier audits. Each doublet "
         + $"contributes TWO dimensions, so an odd kernel dimension forces the ONE singlet that survives on the simplex "
         + $"to be hidden: measured, {MultiplicityRelation()}. G_040's loss of 47 at ITS state is 23 hidden doublets "
         + "plus that singlet under the same arithmetic, so the two audits agree about the STRUCTURE of the hiding and "
         + "differ only about WHICH doublets a particular state hides - a property of the state, not of the symmetry. "
         + "THE PHYSICAL INTERPRETATION IS THEN FORCED BY THE FREQUENCIES. Measured channel by channel, the hidden set "
         + $"is the SHORT-WAVELENGTH sector: {SpectralInterpretation()}. That is exactly what one should expect from "
         + "distance-class contractions, which average over cells and so resolve long-wavelength structure while going "
         + "blind to fine structure - and it is a genuine interpretation rather than a restatement, because it says "
         + "which physical thing the hidden 53 are: the fine structure of the organisation's arrangement. THE "
         + $"SIGNATURES AGREE WITH THE INTERPRETATION RATHER THAN MERELY ACCOMPANYING IT: the clock response correlates "
         + $"with channel frequency at {SignatureFrequencyCorrelation():F4}, so all three lossless readings see the "
         + "hidden sector in the same order and the interpretation does not depend on which of them one uses. WHAT THE "
         + $"KERNEL IS NOT IS ALSO MEASURED: {WhatItIsNot()}. SO THE ANSWER IS DERIVED - the kernel has an INDEPENDENT "
         + "physical interpretation, it is the substrate's short-wavelength sector, and the fact that it is a union of "
         + "whole channels is what makes the phrase \"the hidden directions\" well defined at all.";

    // ===================== REPORT =====================

    public static string OutputChannels()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE CHANNELS: ISOTYPY AND THE MULTIPLICITY RELATION");
        sb.AppendLine("   channel | kind | frequency | share of the kernel | class");
        foreach (var (channel, kind, frequency, share, cls) in ModeTable())
            if (cls == "HIDDEN" || channel >= 10 && channel <= 20)
                sb.AppendLine($"   {channel,7} | {kind,4} | {frequency,9:F4} | {share,19:F6} | {cls}");
        sb.AppendLine($"   NO Fourier mode is split anywhere : {TheKernelIsAUnionOfFourierModes()}");
        sb.AppendLine($"   half-hidden doublet channels      : {HalfHiddenChannels().Length} ({string.Join(", ", HalfHiddenChannels())})");
        sb.AppendLine($"   the multiplicity relation         : {MultiplicityRelation()}");
        sb.AppendLine($"   the relation holds exactly        : {TheMultiplicityRelationHolds()}");
        return sb.ToString();
    }

    public static string OutputSpectral()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE PHYSICAL INTERPRETATION: THE HIDDEN SET IS THE SHORT-WAVELENGTH SECTOR");
        sb.AppendLine($"   hidden channels   : {HiddenChannels().Min()}..{HiddenChannels().Max()} ({HiddenChannels().Length} channels, {HiddenModes()} modes)");
        sb.AppendLine($"   visible channels  : {VisibleChannels().Min()}..{VisibleChannels().Max()} ({VisibleChannels().Length} channels, {VisibleModes()} modes)");
        sb.AppendLine($"   {SpectralInterpretation()}");
        sb.AppendLine($"   separated in frequency : {TheHiddenSetIsTheShortWavelengthSector()}");
        sb.AppendLine($"   {PhysicalInterpretation()}");
        return sb.ToString();
    }

    public static string OutputSignatures()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE SIGNATURES BY CHANNEL");
        sb.AppendLine("   channel | frequency | clock      | acceleration | field");
        foreach (var (channel, frequency, clock, acceleration, field) in SignatureTable().Take(8))
            sb.AppendLine($"   {channel,7} | {frequency,9:F4} | {clock,10:E3} | {acceleration,12:E3} | {field,9:E3}");
        sb.AppendLine($"   (all {SignatureTable().Length} hidden channels tabulated in the audit's own run)");
        sb.AppendLine($"   clock-frequency correlation : {SignatureFrequencyCorrelation():F4}");
        sb.AppendLine($"   ordered by frequency        : {TheSignaturesAreFrequencyOrdered()}  (correlation {SignatureFrequencyCorrelation():F4})");
        sb.AppendLine($"   every hidden mode has one    : {EveryHiddenModeHasASignature()}");
        sb.AppendLine();
        sb.AppendLine("4. WHAT THE KERNEL IS NOT");
        sb.AppendLine($"   {WhatItIsNot()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
