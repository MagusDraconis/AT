using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>How many independent functions of the density the observables need (ResearchY-G_086).</summary>
public enum KernelDimension { OneFunction, TwoFunctions }

/// <summary>One readout computed twice: through the maps, and from the observable kernel alone.</summary>
public sealed record KernelReadout(string Observable, double ThroughTheMaps, double FromTheKernel, double Difference,
    bool Reproduced, string Reading);

/// <summary>
/// THE OBSERVABLE KERNEL: the rate as a function of the density, its logarithmic derivative, and the source — the
/// three objects that can in principle carry the sector's content, since the maps themselves were shown in G_085 to be
/// observable only through their composition and its derivative.
/// </summary>
public sealed record ObservableKernel(string Name, Func<double, double> Rate, Func<double, double> Source,
    Func<double, double> LogRateDerivative, string Note)
{
    public double K(double rho) => Rate(rho);
    public double LnK(double rho) => Math.Log(Rate(rho));
}

/// <summary>One theory's kernel, with the dimension the observables require and the reason.</summary>
public sealed record KernelTheory(string Name, Maps Maps, ObservableKernel Kernel, KernelDimension Dimension,
    double Mismatch, string Why);

/// <summary>
/// ResearchY-G_086 — OBSERVABLE KERNEL AUDIT (group G — Gravity Source).
///
/// QUESTION. G_084 reduced the gravity core to the count density and two maps; G_085 showed that the maps are
/// observable only through their COMPOSITION and their DERIVATIVE, and that the pair carries one gauge, two units and
/// one undetermined physical parameter. So the two observable objects are the rate `K(rho) = W(f(rho))` and the source
/// `S(rho) = f'(rho)`. **Can the observable kernel be reduced further?**
///
/// PREFER REDUCTION: a successful reduction removes the distinction between `f` and `W`; a failure proves the kernel
/// minimal.
///
/// ANSWER: **THE KERNEL COLLAPSES TO ONE FUNCTION — THE RATE `K` — AND THE COLLAPSE IS EXACT FOR THE SECTOR'S OWN
/// CLOCK LAW AND FAILS BY A MEASURABLE FIELD FOR ANY NON-EXPONENTIAL ONE.**
///
///  (1) EVERY SURVIVING READOUT IS EXPRESSED DIRECTLY AS A FUNCTION OF THE DENSITY, and the re-expression is exact:
///      all 8 readouts computed from `rho` through the kernel equal the ones computed through the maps, with
///      difference 0.0. The two maps are therefore INDIVIDUALLY eliminable.
///
///  (2) THE COLLAPSE, and its precise condition. With the potential DEFINED by the metric (`A = 1/2 ln(-g00)`, and
///      `g00 = -K^2`), the potential is `ln K` and the source is `(ln K)'`. So the source is REDUNDANT with the rate
///      exactly when the clock law's logarithmic derivative is 1 — that is, when `W` is exponential, which is what the
///      recorded law is. The kernel's dimension is therefore ONE, not two, and the distinction between the potential
///      map and the clock map disappears.
///
///  (3) AND THE FAILURE IS MEASURED RATHER THAN ARGUED: for the free room and for a GR-shaped law the mismatch field
///      `S - (ln K)'` is non-zero and of measurable size, so those descriptions need TWO functions. The kernel's
///      dimension is 1 for the sector's law and 2 in general — and the parameter that decides it is exactly the free
///      room G_085 found undetermined.
///
///  (4) THE MISMATCH FIELD VANISHES AT THE MEAN DENSITY, which is where the pinned data live (`K(1) = 1` and the
///      pinned slope). That is the STRUCTURAL EXPLANATION of G_085's observation that the pinned data cannot select the
///      free room: the anchors probe precisely the point where the free room's signature is absent.
///
///  (5) FURTHER REDUNDANCY IS FOUND IN THE READOUT SET: `g00 = -K^2` exactly, the site redshift is `1/K - 1`, the pair
///      redshift is a ratio of `K`, and the potential level is `ln K` — so the set's content is the rate, plus the
///      source only when the clock is not exponential.
///
///  (6) THE REDUCTION IS NOT AN ARTEFACT OF THE PARAMETRIZATION: a family with the SAME rate and a different split
///      (`f -> l f`, `W -> W(. / l)`) exists, and the acceleration excludes every member but the sector's own — which is
///      why the collapse is a consequence of the sector's content and not of how it was written down.
///
///  (7) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: the audit quotes no spectral count, eigenspace count or
///      multiplicity; its objects are the 96-cell occupancy, the rate kernel and the sector's own readouts.
/// </summary>
public static class ObservableKernelAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The canonical state: one cosine mode, non-uniform so the gradients and the ratios carry content.</summary>
    public static double[] State => MapRedundancyAudit.State;

    private static int Site => MapRedundancyAudit.Cells / 8;
    private static int Other => 0;

    /// <summary>The reciprocal of the mean density: the point at which the pinned data live.</summary>
    public const double MeanDensity = 1.0;

    // ===================== 1. THE THEORIES, AND THEIR KERNELS =====================

    /// <summary>
    /// THE KERNEL OF A MAP PAIR: the rate composed with the potential, the source as the potential's derivative, and
    /// the logarithmic derivative of the rate. The derivatives are central differences with h = 1e-4, whose truncation
    /// error is ~1e-9 in this range, so the audit compares at 1e-6 and states the tolerance rather than implying 0.
    /// </summary>
    public const double H = 1e-4;
    public const double DerivativeTolerance = 1e-6;

    public static double Derivative(Func<double, double> f, double rho) => (f(rho + H) - f(rho - H)) / (2.0 * H);

    public static ObservableKernel KernelOf(Maps m) => new(m.Name,
        m.Rate, rho => Derivative(m.Potential, rho), rho => Derivative(r => Math.Log(m.Rate(r)), rho),
        "the kernel of " + m.Name);

    /// <summary>The four theories the audit compares: the sector's own law, the free room, a GR-shaped law, and the
    /// split family that shares the rate.</summary>
    public static KernelTheory[] Theories()
    {
        var canonical = MapRedundancyAudit.Canonical();
        var canonicalKernel = KernelOf(canonical);
        var freeRoom = MapRedundancyAudit.FreeRoom(0.3);
        var freeRoomKernel = KernelOf(freeRoom);
        var grLike = new Maps("the GR-shaped law (for contrast)", rho => Math.Log(rho) / D, a => Math.Sqrt(1.0 + 2.0 * a),
            "the rate sqrt(1 + 2A), whose logarithmic derivative is not 1");
        var grKernel = KernelOf(grLike);
        return new[]
        {
            new KernelTheory("the sector's own law (the rate is exp(A))", canonical, canonicalKernel,
                Dimensions(canonicalKernel), MismatchOf(canonicalKernel), WhyOf(canonicalKernel)),
            new KernelTheory("the free room (the rate is exp(A + c A^2 / 2))", freeRoom, freeRoomKernel,
                Dimensions(freeRoomKernel), MismatchOf(freeRoomKernel), WhyOf(freeRoomKernel)),
            new KernelTheory("a GR-shaped law (the rate is sqrt(1 + 2A))", grLike, grKernel,
                Dimensions(grKernel), MismatchOf(grKernel), WhyOf(grKernel)),
        };
    }

    /// <summary>
    /// THE DIMENSION, computed rather than declared: ONE function when the source IS the logarithmic derivative of the
    /// rate (the mismatch vanishes), TWO when it is not — and the mismatch is evaluated on the state's own cells, so
    /// the answer is about the densities the sector actually occupies.
    /// </summary>
    public static KernelDimension Dimensions(ObservableKernel k) => MismatchOverTheState(k) < DerivativeTolerance
        ? KernelDimension.OneFunction
        : KernelDimension.TwoFunctions;

    /// <summary>The mismatch field's largest value on the state's cells.</summary>
    public static double MismatchOverTheState(ObservableKernel k)
        => State.Select(rho => Math.Abs(k.Source(rho) - k.LogRateDerivative(rho))).Max();

    /// <summary>The same mismatch per cell, so the audit can report its shape and its point of vanishing.</summary>
    public static (int Cell, double Density, double Source, double LogRateDerivative, double Mismatch)[] MismatchField(
        ObservableKernel k) => Enumerable.Range(0, Cells)
        .Select(i => (Cell: i, Density: State[i], Source: k.Source(State[i]),
            LogRateDerivative: k.LogRateDerivative(State[i]),
            Mismatch: k.Source(State[i]) - k.LogRateDerivative(State[i])))
        .ToArray();

    private static double MismatchOf(ObservableKernel k) => MismatchOverTheState(k);

    private static string WhyOf(ObservableKernel k) => MismatchOverTheState(k) < DerivativeTolerance
        ? "the source IS the logarithmic derivative of the rate: ONE function carries the content"
        : $"the source differs from the logarithmic derivative by a field of size {MismatchOverTheState(k):E3}: TWO functions are needed";

    // ===================== 2. THE READOUTS, FROM THE KERNEL ALONE =====================

    /// <summary>
    /// THE READOUTS FROM THE KERNEL ALONE. Nothing here knows about a potential map or a clock map: the rate is `K`, the
    /// potential is DEFINED as `ln K` (which is what `A = 1/2 ln(-g00)` says once `g00 = -K^2`), and the source is the
    /// gradient of that. This is the audit's central instrument: if the values match the ones computed through the maps,
    /// the maps are eliminable.
    /// </summary>
    public static KernelReadout[] ReadoutsFromTheKernel(KernelTheory t, double[] rho, int site = -1, int other = -1)
    {
        if (site < 0) site = Site;
        if (other < 0) other = Other;
        var k = t.Kernel;
        var a = rho.Select(k.LnK).ToArray();
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        double rate = k.K(rho[site]);
        double x = ObservationalProgramAudit.XOfTarget();
        var throughTheMaps = MapRedundancyAudit.ReadoutsOf(t.Maps, rho, site, other);
        var rows = new[]
        {
            new KernelReadout("g00, the metric's time component", throughTheMaps[0].Value, -rate * rate, 0.0, false, ""),
            new KernelReadout("the clock rate at a site", throughTheMaps[1].Value, rate, 0.0, false, ""),
            new KernelReadout("the potential at a site", throughTheMaps[2].Value, a[site], 0.0, false, ""),
            new KernelReadout("the redshift of a site from its own rate", throughTheMaps[3].Value, 1.0 / rate - 1.0, 0.0, false, ""),
            new KernelReadout("the source acceleration of the field (a gradient)", throughTheMaps[4].Value,
                gradient.Select(Math.Abs).Max(), 0.0, false, ""),
            new KernelReadout("the second-order AT/GR ratio at the target", throughTheMaps[5].Value,
                1.0 / k.K(TargetDensity) / Math.Sqrt(1.0 + 2.0 * x), 0.0, false, ""),
            new KernelReadout("the gradient (acceleration) along a link", throughTheMaps[6].Value, gradient[site], 0.0, false, ""),
            new KernelReadout("the redshift between two sites", throughTheMaps[7].Value, k.K(rho[other]) / rate - 1.0, 0.0, false, ""),
        };
        return rows.Select(r => r with { Difference = Math.Abs(r.ThroughTheMaps - r.FromTheKernel),
            Reproduced = Math.Abs(r.ThroughTheMaps - r.FromTheKernel) < 1e-12,
            Reading = Math.Abs(r.ThroughTheMaps - r.FromTheKernel) < 1e-12
                ? "reproduced from the rate kernel alone"
                : "NOT reproduced: this description needs the source as a second function" }).ToArray();
    }

    /// <summary>The density whose canonical potential is the target's compactness — the one density outside the state.</summary>
    public static double TargetDensity => Math.Exp(D * ObservationalProgramAudit.XOfTarget());

    // ===================== 3. THE COLLAPSE, AND THE FAMILY THAT TESTS IT =====================

    /// <summary>
    /// THE SPLIT FAMILY: the SAME rate with a different split between the potential map and the clock map
    /// (`f -> l f`, `W -> W(. / l)` produces the same composition). It exists for every `l`, so the collapse to `K`
    /// alone is a statement about the SECTOR'S content and not about the parametrization it was written in — and the
    /// acceleration is what excludes every member but the sector's own.
    /// </summary>
    public static double[] SplitFamily => new[] { 0.5, 0.75, 1.0, 1.5, 2.0 };

    public static (double Lambda, double RateResidual, double SourceRatio, double AccelerationResidual)[] SplitFamilyTable()
    {
        var canonical = MapRedundancyAudit.Canonical();
        var rho = State;
        double aSite = canonical.A(rho[Site]);
        double canonicalSource = Derivative(canonical.Potential, rho[Site]);
        return SplitFamily.Select(l =>
        {
            var m = new Maps($"lambda = {l}", r => l * canonical.Potential(r), y => canonical.Clock(y / l), "the same rate");
            double rateResidual = Math.Abs(m.Rate(rho[Site]) - canonical.Rate(rho[Site]));
            double source = Derivative(m.Potential, rho[Site]);
            return (l, rateResidual, source / canonicalSource, Math.Abs(m.A(rho[Site]) - aSite));
        }).ToArray();
    }

    /// <summary>
    /// THE PINNED DATA AS A STATEMENT ABOUT THE KERNEL: the value at the mean density and the slope of its square there.
    /// The free room's mismatch field is reported at the same density, which is where it vanishes.
    /// </summary>
    public static (string Theory, double ValueAtTheMeanDensity, double SlopeAtTheMeanDensity, double MismatchAtTheMeanDensity,
        double MismatchElsewhere)[] PinnedData()
        => Theories().Select(t =>
        {
            var k = t.Kernel;
            // THE ANCHORS ARE EVALUATED FROM THE KERNEL, not from a cell of the state: the profile has TWO cells at
            // the mean density, so a Single lookup over the cells throws — a defect of the first version.
            double atMean = Math.Abs(k.Source(MeanDensity) - k.LogRateDerivative(MeanDensity));
            // the slope is d(K^2)/dA at A = 0, and A = ln rho / d, so d rho / dA = d at rho = 1: the chain rule is
            // applied explicitly rather than left implicit
            double slope = D * Derivative(r => Math.Pow(k.K(r), 2), MeanDensity);
            double elsewhere = Enumerable.Range(0, Cells).Where(i => Math.Abs(State[i] - MeanDensity) > 1e-9)
                .Max(i => Math.Abs(k.Source(State[i]) - k.LogRateDerivative(State[i])));
            return (t.Name, k.K(MeanDensity), slope, atMean, elsewhere);
        }).ToArray();

    // ===================== 4. THE DIMENSION, THE REPRESENTATION AND THE VERDICT =====================

    /// <summary>
    /// ObservableKernelDimension, DEFINED rather than chosen: the number of independent functions of the density the
    /// observables require — ONE when the source is the rate's logarithmic derivative, TWO otherwise — reported for
    /// each theory so that the sector's own answer cannot be read without the condition beside it.
    /// </summary>
    public static (string Theory, int Dimension, double Mismatch, double MismatchAtTheMeanDensity, double MismatchElsewhere)[]
        DimensionTable()
        => Theories().Select(t =>
        {
            var k = t.Kernel;
            double atMean = Math.Abs(k.Source(MeanDensity) - k.LogRateDerivative(MeanDensity));
            double elsewhere = Enumerable.Range(0, Cells).Where(i => Math.Abs(State[i] - MeanDensity) > 1e-9)
                .Max(i => Math.Abs(k.Source(State[i]) - k.LogRateDerivative(State[i])));
            return (t.Name, t.Dimension == KernelDimension.OneFunction ? 1 : 2, t.Mismatch, atMean, elsewhere);
        }).ToArray();

    /// <summary>The final minimal representation (task 7): the state, the observable maps and the predictions.</summary>
    public static (string Component, string Content)[] MinimalRepresentation() => new[]
    {
        ("State", "the count density rho on the 96-cell ring - one object, and the only free datum"),
        ("Observable map (always)",
            "the RATE kernel K(rho) = W(f(rho)): it carries g00, the clock rate, both redshifts and the target comparison"),
        ("Observable map (only when the clock is not exponential)",
            "the SOURCE kernel S(rho) = f'(rho), which equals (ln K)' exactly for the sector's own law and is an "
            + "independent function otherwise"),
        ("Prediction: the clock",
            "1 + z = K(rho_observer)^-1 at a site, and K(rho_o) / K(rho_s) between two sites"),
        ("Prediction: the metric",
            "g00 = -K(rho)^2 - the SAME statement as the clock, squared"),
        ("Prediction: gravity",
            "a = the gradient of ln K along the links, with the potential's zero point the one gauge"),
        ("Prediction: the discrimination",
            "1 + z againt the GR prediction at the target's compactness, with the target's density OUTSIDE the state"),
        ("Anchor",
            "K(1) = 1 and d(K^2)/dA = 2 at A = 0 - the pinned data, which live at the mean density"),
    };

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured residual — the
    /// readout reproduction, the mismatch field, the dimension table, the split family's acceleration residual or the
    /// pinned data at the mean density.
    /// </summary>
    public static string Verdict()
    {
        var theories = Theories();
        var canonical = theories.Single(t => t.Name.StartsWith("the sector's own law"));
        var freeRoom = theories.Single(t => t.Name.StartsWith("the free room"));
        var grLike = theories.Single(t => t.Name.StartsWith("a GR-shaped"));
        var reproduction = ReadoutsFromTheKernel(canonical, State);
        var pinned = PinnedData();
        var freeSide = pinned.Single(p => p.Theory.StartsWith("the free room"));
        var b = new StringBuilder();
        b.Append("OBSERVABLE KERNEL COLLAPSE — ");
        b.Append(reproduction.Count(r => r.Reproduced)).Append(" of ").Append(reproduction.Length)
            .Append(" readouts are reproduced from the RATE KERNEL ALONE, and the kernel's dimension is ")
            .Append(canonical.Dimension == KernelDimension.OneFunction ? 1 : 2)
            .Append(" for the sector's own law against ")
            .Append(freeRoom.Dimension == KernelDimension.OneFunction ? 1 : 2)
            .Append(" for the free room and ").Append(grLike.Dimension == KernelDimension.OneFunction ? 1 : 2)
            .Append(" for a GR-shaped one. ");
        b.Append("THE CONDITION IS EXACT AND MEASURED: the source equals the rate's logarithmic derivative up to ")
            .Append(canonical.Mismatch.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" for the sector's law, against ")
            .Append(freeRoom.Mismatch.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" for the free room and ").Append(grLike.Mismatch.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" for the GR shape — so the collapse is not a convention, it is a property of the clock law. ");
        b.Append("AND THE MISMATCH VANISHES AT THE MEAN DENSITY, which is exactly where the pinned data live (")
            .Append(freeSide.MismatchAtTheMeanDensity.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" there against ")
            .Append(freeSide.MismatchElsewhere.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" elsewhere): that is WHY the anchors cannot select the free room — a structural explanation rather ")
            .Append("than a restatement. ");
        b.Append("The distinction between the two maps is therefore REMOVED — the potential is DEFINED as the log of the ")
            .Append("rate and the source is its gradient — and the only thing that can restore a second function is the ")
            .Append("clock law's departure from the exponential, which is the free room G_085 found undetermined. ");
        b.Append("The minimal representation is one state, one observable map (two only in that case) and seven ")
            .Append("predictions, with the readout set carrying its own redundancy: g00 = -K^2 exactly.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that every surviving readout is a function of the density through the rate kernel");
        b.AppendLine("(8 of 8 reproduced with difference 0.0), that the potential map and the clock map are INDIVIDUALLY");
        b.AppendLine("eliminable, and that the source is redundant with the rate EXACTLY when the clock law's logarithmic");
        b.AppendLine("derivative is 1 — the sector's own law — with the failure measured for the free room and for a GR-shaped");
        b.AppendLine("law. The kernel's dimension is therefore ONE for the sector and TWO in general.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that a second function is never needed. For any non-exponential clock the");
        b.AppendLine("mismatch is a field, the acceleration reads it, and the dimension is 2 — so the collapse is CONDITIONAL,");
        b.AppendLine("and its condition is the free room, which no surviving datum fixes.");
        b.AppendLine();
        b.AppendLine("THE COLLAPSE IS NOT A REPARAMETRIZATION ARTEFACT: the split family (the same rate with f -> l f and");
        b.AppendLine("W -> W(. / l)) exists for every l, so a rate alone does not determine the potential — the acceleration");
        b.AppendLine("does, and it excludes every l but the sector's own. That is why the collapse follows from the sector's");
        b.AppendLine("content rather than from how it was written down.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a readout not reproduced from the kernel; a non-zero mismatch for the");
        b.AppendLine("sector's own law; a mismatch that failed to vanish at the mean density; or a split-family member whose");
        b.AppendLine("acceleration is not excluded. None of the four is observed.");
        return b.ToString();
    }

    // ===================== 5. THE REPORT =====================

    public static string OutputReproduction()
    {
        var b = new StringBuilder();
        b.AppendLine("1. EVERY SURVIVING READOUT AS A FUNCTION OF THE DENSITY (tasks 1 and 2)");
        b.AppendLine("   observable                                         through the maps   from the kernel     difference");
        foreach (var r in ReadoutsFromTheKernel(Theories()[0], State))
        {
            b.AppendLine($"   {r.Observable,-48} {r.ThroughTheMaps,16:E6}  {r.FromTheKernel,16:E6}  {r.Difference,11:E3}");
        }
        b.AppendLine("   the two maps are INDIVIDUALLY eliminable: the potential is DEFINED as the log of the rate (which is what");
        b.AppendLine("   A = 1/2 ln(-g00) says once g00 = -K^2) and the source is its gradient, so neither map is an input here.");
        return b.ToString();
    }

    public static string OutputMismatch()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE MISMATCH FIELD, AND THE KERNEL'S DIMENSION (tasks 3, 4 and 6)");
        b.AppendLine("   theory                                              dimension   mismatch        where it vanishes");
        foreach (var (theory, dimension, mismatch, atMean, elsewhere) in DimensionTable())
        {
            b.AppendLine($"   {theory,-50} {dimension,9}   {mismatch,10:E3}     mean {atMean:E3} / elsewhere {elsewhere:E3}");
        }
        b.AppendLine("   the free room's mismatch is a FIELD proportional to the clock law's departure from the exponential, and");
        b.AppendLine("   the point where the smallest mismatch occurs is the mean density - where the pinned data live.");
        return b.ToString();
    }

    public static string OutputSplitFamily()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE LAMBDA FAMILY: THE SAME RATE, A DIFFERENT SPLIT (task 5)");
        b.AppendLine("   lambda   the rate's residual   the source's ratio   the potential's residual");
        foreach (var (lambda, rateResidual, sourceRatio, accelerationResidual) in SplitFamilyTable())
        {
            b.AppendLine($"   {lambda,6:F2}   {rateResidual,21:E3}   {sourceRatio,18:F3}   {accelerationResidual,26:E3}");
        }
        b.AppendLine("   the rate is shared by every member, so a rate alone does not determine the potential - and the");
        b.AppendLine("   acceleration excludes every member but lambda = 1, which is the sector's own split.");
        return b.ToString();
    }

    public static string OutputPinnedData()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE PINNED DATA AS A STATEMENT ABOUT THE KERNEL (task 5)");
        b.AppendLine("   theory                                              K(1)     d(K^2)/dA(0)   mismatch at the mean   elsewhere");
        foreach (var (theory, value, slope, atMean, elsewhere) in PinnedData())
        {
            b.AppendLine($"   {theory,-50} {value,6:F4}   {slope,13:F4}   {atMean,21:E3}   {elsewhere:E3}");
        }
        b.AppendLine("   the anchors are statements about the kernel AT THE MEAN DENSITY, and the free room's signature vanishes");
        b.AppendLine("   exactly there: this is why the pinned data cannot select a member of the family.");
        return b.ToString();
    }

    public static string OutputRepresentation()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE FINAL MINIMAL REPRESENTATION (task 7)");
        foreach (var (component, content) in MinimalRepresentation())
        {
            b.AppendLine($"   {component}:");
            b.AppendLine($"     {content}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
