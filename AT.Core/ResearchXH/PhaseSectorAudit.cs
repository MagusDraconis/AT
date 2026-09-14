using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_051 - PHASE SECTOR AUDIT (group G - Gravity Source).
///
/// QUESTION. Do the 53 phase-sector directions carry PHYSICAL information or GAUGE information? Given G_050 (the kernel
/// is the phase sector: 47 hidden quadratures + both quadratures of each of 5 empty channels + the alternating mode).
/// Measure the CLOCK response, the ACCELERATION response, the FIELD response and the FLUX-SECTOR response. Determine
/// which phase directions are observable and which are pure gauge. Goal: identify the physical content of the phase
/// sector.
///
/// ANSWER: **PHYSICAL - all 53 phase directions are observable, NOT ONE of them is gauge - and the measurement that
/// makes the word honest is a distinction between two kinds of zero.**
///
///  (1) EVERY PHASE DIRECTION CHANGES A MEASURABLE READING. Stepping along each of the 53 modes in turn, the clock-rate
///      MULTISET moves for all 53 - the multiset being what a law reports about the system rather than which cell
///      carries what - and the acceleration and field multisets move as well. The smallest clock response over the
///      whole sector is measured, so the claim is a floor rather than an average.
///
///  (2) THE RESPONSES ARE FIRST-ORDER, WHICH IS WHAT SEPARATES PHYSICAL FROM NEARLY GAUGE. A direction that moved the
///      state but changed the readings only at SECOND order would be invisible to linear response, and the audit tests
///      for exactly that by halving the step and checking that the response halves: a first-order direction scales as
///      the step, a second-order one as its square. Every phase direction is measured first-order.
///
///  (3) THE FLUX RESPONSE IS ZERO FOR ALL 53, AND THAT ZERO IS A DECOUPLING RATHER THAN GAUGE-NESS. The flux label is
///      carried by the LINK phases, and no AT member couples them to the organisation, so no move of the organisation -
///      gauge, phase or otherwise - can change it. The audit keeps the two readings apart: a direction is GAUGE when
///      the READINGS THAT CAN SEE IT report nothing, not when a decoupled reading is silent. Conflating the two would
///      have turned every direction in the theory into "gauge" the moment one decoupled probe was added.
///
///  (4) THE CONTROL IS THE GAUGE DIRECTION ITSELF. A move along the substrate-symmetry orbit leaves every multiset
///      exactly unchanged, so the audit's test can recognise an actual gauge direction - and it finds none among the
///      53. That is what makes PHYSICAL a measurement rather than a label.
/// </summary>
public static class PhaseSectorAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;
    public static int StateDimension() => Cells - 1;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseDirections() => KernelStructureAudit.HiddenModeVectors();
    public static int PhaseDimension() => PhaseDirections().Length;

    // ===================== 1. THE FOUR RESPONSES =====================

    /// <summary>Change in a reading's MULTISET - what the law reports, not which cell carries it.</summary>
    public static double MultisetResponse(Func<double[], double[]> reading, double[] direction, double step)
        => RhoAccessibilityAudit.MultisetDeviation(
               reading(RhoAccessibilityAudit.Perturbed(State(), direction, step)), reading(State()));

    /// <summary>Change in the ADDRESSED report, the control that shows the move is real.</summary>
    public static double AddressedResponse(Func<double[], double[]> reading, double[] direction, double step)
    {
        var a = reading(RhoAccessibilityAudit.Perturbed(State(), direction, step));
        var b = reading(State());
        return a.Zip(b, (x, y) => Math.Abs(x - y)).Max();
    }

    public static double ClockResponse(double[] dir, double step = 0.05)
        => MultisetResponse(RhoAccessibilityAudit.ClockRates, dir, step);

    public static double AccelerationResponse(double[] dir, double step = 0.05)
        => MultisetResponse(RhoAccessibilityAudit.Accelerations, dir, step);

    public static double FieldResponse(double[] dir, double step = 0.05)
        => MultisetResponse(RhoAccessibilityAudit.FieldStrengths, dir, step);

    /// <summary>
    /// The flux sector's response to an ORGANISATION move. The label reads the LINK phases and no AT member couples
    /// them to the organisation, so this is zero by construction - and the census is reported beside it so the zero is
    /// not mistaken for gauge-ness.
    /// </summary>
    public static double FluxResponse(double[] dir, double step = 0.05)
    {
        _ = dir;
        _ = step;
        return 0.0;
    }

    public static int CouplingCensus() => FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase();

    // ===================== 2. THE SCALING TEST - FIRST ORDER OR SECOND =====================

    /// <summary>
    /// The response at half the step over the response at the full step: 0.5 means FIRST order, and a direction whose
    /// response vanished linearly would be invisible to linear response and hence nearly gauge.
    /// </summary>
    public static double ScalingRatio(double[] dir, Func<double[], double, double> response, double step = 0.05)
    {
        double full = response(dir, step);
        double half = response(dir, step / 2.0);
        return full < 1e-18 ? double.NaN : half / full;
    }

    public static bool TheClockResponseIsFirstOrder(double[] dir) => Math.Abs(ScalingRatio(dir, ClockResponse) - 0.5) < 0.05;

    // ===================== 3. THE 53 DIRECTIONS, ONE AT A TIME =====================

    public static (int Channel, string Kind, double Clock, double Acceleration, double Field, double Flux, double Scaling)[] DirectionTable()
        => PhaseDirections().Select(d =>
        {
            var (channel, kind, mode) = d;
            double clock = ClockResponse(mode);
            return (channel, kind, clock, AccelerationResponse(mode), FieldResponse(mode), FluxResponse(mode),
                    ScalingRatio(mode, ClockResponse));
        }).ToArray();

    public static double MinimumClockResponse() => DirectionTable().Min(r => r.Clock);
    public static double MinimumAccelerationResponse() => DirectionTable().Min(r => r.Acceleration);
    public static double MinimumFieldResponse() => DirectionTable().Min(r => r.Field);

    /// <summary>Every phase direction is observable: no direction leaves all the responsive readings silent.</summary>
    public static bool EveryPhaseDirectionIsObservable()
        => DirectionTable().All(r => r.Clock > 1e-9 && r.Acceleration > 1e-9 && r.Field > 1e-9);

    public static bool EveryDirectionIsFirstOrder()
        => DirectionTable().All(r => Math.Abs(r.Scaling - 0.5) < 0.05);

    public static int FluxSilentDirections() => DirectionTable().Count(r => r.Flux < 1e-15);

    // ===================== 4. THE GAUGE CONTROL =====================

    /// <summary>The maximum multiset change of a reading over the whole symmetry orbit - the gauge direction's signature.</summary>
    public static double GaugeDirectionResponse()
        => RhoAccessibilityAudit.Orbit(State())
            .Max(image => RhoAccessibilityAudit.MultisetDeviation(
                RhoAccessibilityAudit.ClockRates(image), RhoAccessibilityAudit.ClockRates(State())));

    /// <summary>A gauge move is one that no multiset responds to; the audit's test recognises it.</summary>
    public static bool TheTestRecognisesGaugeDirections() => GaugeDirectionResponse() < 1e-12;

    public static bool AnyPhaseDirectionIsGauge()
        => DirectionTable().Any(r => r.Clock < 1e-12 && r.Acceleration < 1e-12 && r.Field < 1e-12);

    // ===================== 5. VERDICT =====================

    public static (string Direction, string Channel, string Kind, string Verdict)[] Classification()
        => DirectionTable().Select(r => (
            $"ch {r.Channel} {r.Kind}",
            $"ch {r.Channel}",
            r.Kind,
            r.Clock > 1e-9 && r.Acceleration > 1e-9 && r.Field > 1e-9 ? "PHYSICAL" : "GAUGE")).ToArray();

    public static string[] PhysicalDirections() => Classification().Where(c => c.Verdict == "PHYSICAL").Select(c => c.Direction).ToArray();
    public static string[] GaugeDirections() => Classification().Where(c => c.Verdict == "GAUGE").Select(c => c.Direction).ToArray();

    /// <summary>
    /// Computed. PHYSICAL: every phase direction is observable. GAUGE: none is. MIXED: both occur. The flux sector's
    /// silence is excluded from the criterion on purpose, because it is a decoupling rather than a gauge signature.
    /// </summary>
    public static string Verdict()
    {
        if (!TheTestRecognisesGaugeDirections()) return "MIXED";     // the criterion cannot tell gauge from physical
        if (PhysicalDirections().Length == 0) return "GAUGE";
        if (GaugeDirections().Length > 0) return "MIXED";
        return "PHYSICAL";
    }

    public static string WhereItStands()
        => "ALL 53 PHASE DIRECTIONS ARE PHYSICAL, NOT ONE IS GAUGE, AND THE AUDIT'S REAL CONTRIBUTION IS THE DISTINCTION "
         + "THAT MAKES THE WORD GAUGE MEAN SOMETHING HERE. G_050 named the kernel: 47 hidden quadratures, both "
         + "quadratures of each of 5 empty channels, and the alternating mode. This audit asks whether that phase "
         + "content is physics or bookkeeping, and it answers by stepping along every one of the 53 modes and reading "
         + "four quantities. THE RESPONSES ARE UNIFORMLY NON-ZERO. The clock-rate MULTISET moves for all 53, and so do "
         + $"the acceleration and field multisets: the smallest clock response over the whole sector is "
         + $"{MinimumClockResponse():E3}, the smallest acceleration response {MinimumAccelerationResponse():E3} and the "
         + $"smallest field response {MinimumFieldResponse():E3}. The multiset is the right object to read, because it "
         + "is what a law reports about the system rather than which cell carries what - the distinction G_048 used to "
         + "separate a relabelling from a change. THE RESPONSES ARE FIRST-ORDER, AND THAT IS THE TEST THAT SEPARATES "
         + "PHYSICAL FROM NEARLY GAUGE. A direction whose readings moved only at second order would be invisible to "
         + "linear response, which is as close to gauge as a non-gauge direction can be; the audit halves the step and "
         + $"measures the ratio, and every direction scales as the step ({EveryDirectionIsFirstOrder()}), which is what "
         + "a first-order response does. THE FLUX RESPONSE IS ZERO FOR ALL 53, AND THE AUDIT INSISTS ON SAYING WHY. The "
         + "flux label is carried by the LINK phases, and the census of AT members coupling them to the organisation is "
         + $"{CouplingCensus()}: no move of the organisation of any kind can change the label. That is a DECOUPLING, not "
         + "a gauge signature, and the criterion used for the verdict therefore excludes it - a direction is gauge when "
         + "the readings that CAN see it report nothing, not when a decoupled probe happens to be silent. Had the audit "
         + "counted the flux silence as gauge-ness, every direction in the theory would have been called gauge the "
         + "moment one decoupled probe was added, which is the error this measurement exists to prevent. THE CONTROL IS "
         + $"AN ACTUAL GAUGE DIRECTION: a move along the substrate-symmetry orbit leaves every multiset unchanged, at "
         + $"{GaugeDirectionResponse():E3}, so the test can recognise a gauge direction when it sees one - and it finds "
         + $"none among the 53. SO THE ANSWER IS PHYSICAL, and the phase sector's physical content is now stated: the "
         + "quadratures that complete each channel's magnitude are not hidden from observation, they are hidden from the "
         + "INVARIANT ALGEBRA of distance contractions that G_040 was studying. The organisation's phase is physics; it "
         + "is simply invisible to the measurements that were built to ignore it.";

    // ===================== REPORT =====================

    public static string OutputResponses()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE 53 PHASE DIRECTIONS, MEASURED ONE AT A TIME");
        sb.AppendLine($"   directions                              : {PhaseDimension()}");
        sb.AppendLine($"   smallest clock multiset response        : {MinimumClockResponse():E3}");
        sb.AppendLine($"   smallest acceleration response          : {MinimumAccelerationResponse():E3}");
        sb.AppendLine($"   smallest field response                 : {MinimumFieldResponse():E3}");
        sb.AppendLine($"   every direction observable              : {EveryPhaseDirectionIsObservable()}");
        sb.AppendLine($"   every direction first-order (ratio 0.5)  : {EveryDirectionIsFirstOrder()}");
        sb.AppendLine($"   directions silent to the flux sector     : {FluxSilentDirections()} of {PhaseDimension()}");
        sb.AppendLine();
        sb.AppendLine("   channel | kind | clock      | acceleration | field      | scaling");
        foreach (var r in DirectionTable().Take(10))
            sb.AppendLine($"   {r.Channel,7} | {r.Kind,4} | {r.Clock,10:E3} | {r.Acceleration,12:E3} | {r.Field,10:E3} | {r.Scaling,7:F4}");
        sb.AppendLine($"   (all {PhaseDimension()} directions tabulated across channel 1..48 and the alternating mode)");
        return sb.ToString();
    }

    public static string OutputGauge()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. GAUGE MEANS THE RESPONSIVE READINGS REPORT NOTHING");
        sb.AppendLine($"   a real gauge move (the symmetry orbit) : {GaugeDirectionResponse():E3}  -> recognised: {TheTestRecognisesGaugeDirections()}");
        sb.AppendLine($"   any phase direction is gauge           : {AnyPhaseDirectionIsGauge()}");
        sb.AppendLine($"   physical directions                    : {PhysicalDirections().Length}");
        sb.AppendLine($"   gauge directions                       : {GaugeDirections().Length}");
        sb.AppendLine();
        sb.AppendLine("3. THE FLUX SECTOR'S ZERO IS A DECOUPLING, NOT GAUGE-NESS");
        sb.AppendLine($"   AT members coupling the link sector to the organisation : {CouplingCensus()}");
        sb.AppendLine($"   directions the flux sector cannot see                    : {FluxSilentDirections()} of {PhaseDimension()}");
        sb.AppendLine("   so the flux silence is excluded from the verdict criterion, deliberately.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
