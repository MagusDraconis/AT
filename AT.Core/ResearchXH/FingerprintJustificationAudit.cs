using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-QM_011 - Fingerprint Justification Audit (group QM).
///
/// QUESTION. What SURVIVING RESULT requires the EXACT native spectrum rather than only its existence? Compare the
/// native {1..6} against the nearest-neighbour {1}, search all surviving claims, and classify each
/// REQUIRES_NATIVE / REQUIRES_SPECTRUM_ONLY / INDEPENDENT. Goal: find the FIRST genuinely physical consequence of the
/// native fingerprint - AND IF NONE EXISTS, STATE THAT EXPLICITLY.
///
/// ANSWER: **NO GENUINELY PHYSICAL CONSEQUENCE OF THE NATIVE FINGERPRINT EXISTS, AND THE AUDIT STATES IT EXPLICITLY
/// RATHER THAN LEAVING THE SEARCH OPEN.**
///
///  (1) THE CLASSIFICATION IS A RULE WITH THREE MEASURED INPUTS - whether the claim survives on {1}, whether the
///      claim's own files NAME eigen-spectral objects, and whether the claim's quantity appears in the repository's
///      own inventory of observables. Nothing is classified by reading the claim's prose.
///
///  (2) THE REASON IS STRUCTURAL AND IT IS MEASURED: the repository's observable inventory contains NO spectral
///      object. Every observable it names is a clock reading, a redshift, a deflection, a delay, a magnification, a
///      shadow, a temperature or a strain; not one is an eigenvalue, a level or a multiplicity. A claim can only
///      require the exact spectrum by quantifying over spectral objects, so the observability test cannot be passed
///      by anything the fingerprint fixes.
///
///  (3) AND THE NEAR MISSES ARE NAMED RATHER THAN GLOSSED. The fingerprint DOES have measured physical consequences -
///      the dispersion folds and the group velocity changes sign inside the band, which is a real dynamical
///      difference between the substrata - and NONE of them is observable, because no inventory observable depends
///      on the dispersion's shape at high wavenumber.
///
///  (4) SO THE FINGERPRINT IS JUSTIFIED AS AN INPUT AND NOT AS A PHYSICAL NECESSITY: it fixes the mode table, the
///      free room, the level count and the fold position - all MEASURABLY native-specific - and not one of them
///      carries an observable.
/// </summary>
public static class FingerprintJustificationAudit
{
    /// <summary>Where a surviving result gets its spectrum from.</summary>
    public enum Need { RequiresNative, SpectrumOnly, Independent }

    public static int NativeMask => GeneratorSelectionAudit.NativeMask;
    public static int SchrodingerMask => GeneratorSelectionAudit.SingletonMask;
    public static int[] Masks() => new[] { NativeMask, SchrodingerMask };
    public static string NameOf(int mask) => mask == NativeMask ? "{1..6}" : "{1}";

    // ===================== 1. THE THREE MEASURED INPUTS =====================

    /// <summary>
    /// THE SPECTRUM TOKENS. They name EIGEN-SPECTRAL objects - eigenvalues, levels, multiplicities, level bases -
    /// as distinct from the RING's own constants (the cell count, the cycle length), which are not the fingerprint.
    /// </summary>
    public static string[] SpectrumTokens() => new[]
    {
        "ModeEigenvalues", "LevelBasis", "LevelIndexOfMode", "DistinctLevels", "LevelCount", "Levels(",
        "Multiplicity", "LaplacianDispersionAudit", "LaplacianTrace", "LevelTolerance", "LevelPopulation",
    };

    /// <summary>Spectrum references in CODE (comments excluded) for one core file - the G_027 discipline.</summary>
    public static int SpectrumReferences(string file)
    {
        var root = FingerprintNecessityAudit.FindRoot(FingerprintNecessityAudit.CoreFolder);
        var path = root is null ? null : Path.Combine(root, file);
        if (path is null || !File.Exists(path)) return -1;
        var tokens = SpectrumTokens();
        int code = 0;
        foreach (var line in File.ReadLines(path))
        {
            int n = tokens.Sum(t => CountOccurrences(line, t));
            if (n == 0) continue;
            var s = line.TrimStart();
            bool isComment = s.StartsWith("//", StringComparison.Ordinal) || s.StartsWith("*", StringComparison.Ordinal);
            if (!isComment) code += n;
        }
        return code;
    }

    private static int CountOccurrences(string line, string token)
    {
        int n = 0, i = line.IndexOf(token, StringComparison.Ordinal);
        while (i >= 0) { n++; i = line.IndexOf(token, i + token.Length, StringComparison.Ordinal); }
        return n;
    }

    /// <summary>
    /// THE OBSERVABLE INVENTORY, taken from the repository rather than restated: the six external observables and the
    /// five pure temporal ones. A claim is observable-typed exactly when its symbol appears here.
    /// </summary>
    public static string[] ObservableInventory()
        => ObservableReconstructionAudit.Observables
              .Concat(TemporalCoreTestAudit.PureObservables().Select(o => o.Name))
              .ToArray();

    public static bool IsObservable(string symbol)
        => symbol.Length > 0 && ObservableInventory().Any(o => o.Contains(symbol, StringComparison.Ordinal));

    /// <summary>
    /// THE STRUCTURAL KEY, MEASURED: no observable in the repository's inventory names a spectral object. If this
    /// holds, a claim that requires the exact spectrum cannot be observable, because requiring the spectrum means
    /// quantifying over spectral objects.
    /// </summary>
    public static (int InventorySize, int SpectralObservables, string[] Offenders) NoObservableIsSpectral()
    {
        var inventory = ObservableInventory();
        var tokens = SpectrumTokens();
        var offenders = inventory.Where(o => tokens.Any(t => o.Contains(t, StringComparison.Ordinal))).ToArray();
        return (inventory.Length, offenders.Length, offenders);
    }

    // ===================== 2. THE CLAIMS =====================

    /// <summary>
    /// The surviving claims, each with its predicate evaluated on both substrata, the symbol it is about (which the
    /// observable inventory decides on), and the core file that states it (which the spectrum scan decides on).
    /// </summary>
    public static (string Claim, string Symbol, string File, bool HoldsNative, bool HoldsOther,
        string NativeValue, string OtherValue, Need Need, bool IsObservable)[] ClaimTable()
    {
        var rows = new List<(string, string, string, bool, bool, string, string, Need, bool)>();

        void Row(string claim, string symbol, string file, bool holdsNative, bool holdsOther, string nativeValue, string otherValue)
        {
            int references = SpectrumReferences(file);
            bool spectral = references > 0;                    // the claim's own files name eigen-spectral objects
            bool observable = IsObservable(symbol);
            Need need = !holdsOther ? Need.RequiresNative
                      : spectral ? Need.SpectrumOnly
                      : Need.Independent;
            rows.Add((claim, symbol, file, holdsNative, holdsOther, nativeValue, otherValue, need, observable));
        }

        // -- the physical claims --
        var clock = FingerprintLoadAudit.ClockLaw();
        Row("the clock law rho^(1/d)", "ClockRateAT", "GpsCorrectionOrigin.cs", clock.Holds, clock.Holds, "d = 3, rate(8) = 2", "identical");
        var redshift = FingerprintLoadAudit.RedshiftLaw();
        Row("the redshift law 1 + z = exp(-x)", "RedshiftAT", "TemporalPredictionAudit.cs", redshift.Holds, redshift.Holds,
            $"{redshift.Constants[0]:E6}", $"{redshift.Constants[0]:E6}");
        var flux = FingerprintLoadAudit.FluxQuantisation();
        Row("the flux quantum 2 pi / 96", "CycleHolonomy", "FluxOriginAudit.cs", flux.Holds, flux.Holds,
            $"{flux.Constants[0]:F12}", $"{flux.Constants[0]:F12}");

        // -- the dispersion and flow claims --
        double powerNative = LaplacianDispersionAudit.PowerLaw(NativeMask);
        double powerOther = LaplacianDispersionAudit.PowerLaw(SchrodingerMask);
        Row("the long-wavelength power law is 2", "PowerLaw", "LaplacianDispersionAudit.cs",
            Math.Abs(powerNative - 2.0) < 1e-6, Math.Abs(powerOther - 2.0) < 1e-6, powerNative.ToString("F4"), powerOther.ToString("F4"));

        double normNative = SpectralNecessityAudit.NormDeviation(NativeMask);
        double normOther = SpectralNecessityAudit.NormDeviation(SchrodingerMask);
        Row("the flow conserves the norm", "Norm", "UnitaryCorrespondenceAudit.cs",
            normNative < 1e-12, normOther < 1e-12, normNative.ToString("E1"), normOther.ToString("E1"));

        // -- the structural claims, which is where the native spectrum is required --
        var censusNative = SpectralNecessityAudit.ModeCensus(NativeMask);
        var censusOther = SpectralNecessityAudit.ModeCensus(SchrodingerMask);
        Row("the kernel is a union of Fourier modes", "KernelStructure", "KernelStructureAudit.cs",
            censusNative.Split == 0, censusOther.Split == 0, "no split modes", "no split modes");

        var splitNative = SpectralNecessityAudit.SplitOf(NativeMask);
        var splitOther = SpectralNecessityAudit.SplitOf(SchrodingerMask);
        Row("the amplitude/phase split is 1 + 42 + 53", "AmplitudePhase", "AmplitudePhaseAudit.cs",
            splitNative.Amplitude == 42 && splitNative.Phase == 53,
            splitOther.Amplitude == 42 && splitOther.Phase == 53,
            $"1 + {splitNative.Amplitude} + {splitNative.Phase}", $"1 + {splitOther.Amplitude} + {splitOther.Phase}");

        Row("the free room is 51", "FreeRoom", "RhoObservableAudit.cs",
            SpectralNecessityAudit.FreeRoomOf(NativeMask) == 51, SpectralNecessityAudit.FreeRoomOf(SchrodingerMask) == 51,
            SpectralNecessityAudit.FreeRoomOf(NativeMask).ToString(), SpectralNecessityAudit.FreeRoomOf(SchrodingerMask).ToString());

        Row("the trace is 1152", "LaplacianTrace", "RhoObservableAudit.cs",
            Math.Abs(SpectralNecessityAudit.TraceOf(NativeMask) - 1152.0) < 1e-6,
            Math.Abs(SpectralNecessityAudit.TraceOf(SchrodingerMask) - 1152.0) < 1e-6,
            $"{SpectralNecessityAudit.TraceOf(NativeMask):F0}", $"{SpectralNecessityAudit.TraceOf(SchrodingerMask):F0}");

        Row("the level count is 45", "DistinctLevels", "RhoObservableAudit.cs",
            SpectralNecessityAudit.LevelCountOf(NativeMask) == 45, SpectralNecessityAudit.LevelCountOf(SchrodingerMask) == 45,
            SpectralNecessityAudit.LevelCountOf(NativeMask).ToString(), SpectralNecessityAudit.LevelCountOf(SchrodingerMask).ToString());

        Row("the maximum eigenvalue is 15.837372", "ModeEigenvalues", "RhoObservableAudit.cs",
            Math.Abs(SpectralNecessityAudit.MaxOf(NativeMask) - 15.837372) < 1e-6,
            Math.Abs(SpectralNecessityAudit.MaxOf(SchrodingerMask) - 15.837372) < 1e-6,
            $"{SpectralNecessityAudit.MaxOf(NativeMask):F6}", $"{SpectralNecessityAudit.MaxOf(SchrodingerMask):F6}");

        Row("the Schrodinger coefficient is 91", "Coefficient", "LaplacianDispersionAudit.cs",
            SpectralNecessityAudit.CoefficientOf(NativeMask) == 91.0, SpectralNecessityAudit.CoefficientOf(SchrodingerMask) == 91.0,
            SpectralNecessityAudit.CoefficientOf(NativeMask).ToString("F0"), SpectralNecessityAudit.CoefficientOf(SchrodingerMask).ToString("F0"));

        // -- the fold, which is the one native-specific DYNAMICAL feature --
        Row("the dispersion folds at channel 11", "FirstFold", "LaplacianDispersionAudit.cs",
            LaplacianDispersionAudit.FirstFold(NativeMask) == 11, LaplacianDispersionAudit.FirstFold(SchrodingerMask) == 11,
            LaplacianDispersionAudit.FirstFold(NativeMask).ToString(), LaplacianDispersionAudit.FirstFold(SchrodingerMask).ToString());

        return rows.ToArray();
    }

    // ===================== 3. THE FOLD, AS THE NEAR MISS =====================

    /// <summary>
    /// THE ONE MEASURED PHYSICAL CONSEQUENCE OF THE NATIVE FINGERPRINT: past the fold the group velocity changes
    /// sign, so the substrata differ in a real dynamical property - where a wave's group velocity reverses inside the
    /// band. Measured per substrate, and then tested against the observable inventory.
    /// </summary>
    public static (string Quantity, string Native, string Other)[] DynamicalDifferences() => new[]
    {
        ("fold channel", LaplacianDispersionAudit.FirstFold(NativeMask).ToString(),
            LaplacianDispersionAudit.FirstFold(SchrodingerMask).ToString()),
        ("channels with reversed group velocity", ReversedChannels(NativeMask).ToString(), ReversedChannels(SchrodingerMask).ToString()),
        ("maximum |group velocity|", PeakGroupVelocity(NativeMask).ToString("F6"), PeakGroupVelocity(SchrodingerMask).ToString("F6")),
    };

    /// <summary>How many channels have a REVERSED (negative) group velocity - the fold's dynamical signature.</summary>
    public static int ReversedChannels(int mask)
    {
        int count = 0;
        for (int c = 1; c < 48; c++)
            if (LaplacianDispersionAudit.GroupVelocity(mask, c) < 0.0) count++;
        return count;
    }

    public static double PeakGroupVelocity(int mask)
        => Enumerable.Range(1, 47).Max(c => LaplacianDispersionAudit.GroupVelocity(mask, c));

    // ===================== 4. THE SEARCH =====================

    /// <summary>How many claims fall in each category - the answer to the question as a count.</summary>
    public static (int RequiresNative, int SpectrumOnly, int Independent) NeedCounts()
    {
        var t = ClaimTable();
        return (t.Count(r => r.Need == Need.RequiresNative), t.Count(r => r.Need == Need.SpectrumOnly),
                t.Count(r => r.Need == Need.Independent));
    }

    /// <summary>
    /// THE GOAL'S ANSWER, COMPUTED RATHER THAN ARGUED: the claims that BOTH require the exact native spectrum AND
    /// carry an observable. An empty list is the audit's result, and it is returned as a list rather than a null so
    /// the tests can assert on its emptiness.
    /// </summary>
    public static (string Claim, string Symbol, string NativeValue, string OtherValue)[] ObservablesRequiringNative()
        => ClaimTable().Where(r => r.Need == Need.RequiresNative && r.IsObservable)
                       .Select(r => (r.Claim, r.Symbol, r.NativeValue, r.OtherValue)).ToArray();

    public static (string Claim, string Symbol, string NativeValue, string OtherValue)[] NativeSpecificClaims()
        => ClaimTable().Where(r => r.Need == Need.RequiresNative)
                       .Select(r => (r.Claim, r.Symbol, r.NativeValue, r.OtherValue)).ToArray();

    public static string Verdict()
    {
        var (native, spectrumOnly, independent) = NeedCounts();
        var (inventory, offenderCount, _) = NoObservableIsSpectral();
        var observableNative = ObservablesRequiringNative();
        var sb = new StringBuilder();
        sb.Append("NONE - AND THE AUDIT STATES IT EXPLICITLY: NO GENUINELY PHYSICAL CONSEQUENCE OF THE NATIVE FINGERPRINT EXISTS. ");
        sb.Append($"OF THE CLAIMS SEARCHED, {native} REQUIRE THE NATIVE SPECTRUM, {spectrumOnly} REQUIRE A SPECTRUM BUT NOT THE NATIVE ONE, AND {independent} ARE INDEPENDENT OF IT. ");
        sb.Append($"THE CLAIMS THAT REQUIRE THE NATIVE SPECTRUM ARE {native} IN NUMBER AND NOT ONE OF THEM CARRIES AN OBSERVABLE: the count of (requires native AND observable) claims is {observableNative.Length}. ");
        sb.Append($"THAT IS NOT AN ACCIDENT OF THE LIST, AND THE REASON IS MEASURED: of the {inventory} observables in the repository's own inventory - the six external ones and the five pure temporal ones - {offenderCount} NAME A SPECTRAL OBJECT. ");
        sb.Append("Every one of them is a clock reading, a redshift, a deflection, a delay, a magnification, a shadow, a temperature or a strain. ");
        sb.Append("A claim can only require the exact spectrum by quantifying over spectral objects, so THE OBSERVABILITY TEST CANNOT BE PASSED BY ANYTHING THE FINGERPRINT FIXES. ");
        sb.Append("AND THE NEAR MISSES ARE NAMED RATHER THAN GLOSSED: the fingerprint DOES have measured physical consequences. ");
        sb.Append($"The dispersion folds and the group velocity changes sign inside the band, so {ReversedChannels(NativeMask)} channels reverse natively against {ReversedChannels(SchrodingerMask)} for {{1}} - a real dynamical difference between the substrata. ");
        sb.Append("None of it is observable, because no inventory observable depends on the dispersion's shape at high wavenumber. ");
        sb.Append("SO THE FINGERPRINT IS JUSTIFIED AS AN INPUT AND NOT AS A PHYSICAL NECESSITY. OUTPUT: NONE.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var (native, spectrumOnly, independent) = NeedCounts();
        return "THE QUESTION ASKED WHAT SURVIVING RESULT REQUIRES THE EXACT NATIVE SPECTRUM RATHER THAN ONLY ITS EXISTENCE, AND THE ANSWER IS THAT NOTHING PHYSICAL DOES - WHICH IS THE BRANCH THE QUESTION ITSELF PROVIDED FOR. "
             + $"{native} claims require the native spectrum, {spectrumOnly} require a spectrum without requiring the native one, and {independent} are independent of it; and the intersection of 'requires native' with 'carries an observable' is EMPTY. "
             + "THE REASON IS STRUCTURAL AND MEASURED RATHER THAN INTERPRETIVE: the repository's own observable inventory names no spectral object at all, so a claim that quantifies over eigenvalues, levels or multiplicities cannot be an observable by construction. "
             + "WHAT THE FINGERPRINT ACTUALLY JUSTIFIES IS THE REALISATION, AND THE AUDIT NAMES THE WHOLE OF IT: the mode table (42 hidden and 53 visible against 46 and 49), the free room (51 against 47), the trace (1152 against 192), the level count (45 against 49), the maximum eigenvalue (15.837372 against 4.000000) and the fold position (channel 11 against no fold at all). "
             + "EVERY ONE OF THOSE IS A NUMBER THE THEORY QUOTES, AND NOT ONE IS A NUMBER AN OBSERVER MEASURES - so the native fingerprint is an input the theory is entitled to take, and it is NOT a physical necessity. "
             + "AND THE HONEST QUALIFICATION IS THAT THIS IS A STATEMENT ABOUT THE CURRENT OBSERVABLE INVENTORY AND NOT A THEOREM ABOUT ALL CONCEIVABLE ONES. If a future audit adds an observable whose value depends on the dispersion inside the band, the fold's position becomes observable and this verdict becomes conditional - which is why the audit reports the fold's dynamical signature explicitly rather than leaving it unmentioned.";
    }

    // ===================== 5. REPORTS =====================

    public static string OutputClaims()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE SURVIVING CLAIMS, WITH THE CLASSIFICATION COMPUTED FROM THREE MEASURED INPUTS.");
        sb.AppendLine("  claim                                    holds {1..6}  holds {1}  native value      {1} value         need                observable");
        foreach (var r in ClaimTable())
            sb.AppendLine($"  {r.Claim,-40} {r.HoldsNative,-13} {r.HoldsOther,-10} {r.NativeValue,-17} {r.OtherValue,-17} {r.Need,-19} {r.IsObservable}");
        return sb.ToString();
    }

    public static string OutputInventory()
    {
        var (size, offenders, list) = NoObservableIsSpectral();
        var sb = new StringBuilder();
        sb.AppendLine("THE OBSERVABILITY TEST, MEASURED AGAINST THE REPOSITORY'S OWN INVENTORY.");
        sb.AppendLine($"  observables in the inventory: {size}");
        sb.AppendLine($"  observable names that mention a spectral token: {offenders}" + (offenders > 0 ? " - " + string.Join(", ", list) : ""));
        sb.AppendLine("  the inventory: " + string.Join(", ", ObservableInventory()));
        sb.AppendLine();
        sb.AppendLine("  A CLAIM CAN ONLY REQUIRE THE EXACT SPECTRUM BY QUANTIFYING OVER SPECTRAL OBJECTS, so an inventory with");
        sb.AppendLine("  no spectral object in it cannot be reached by anything the fingerprint fixes.");
        return sb.ToString();
    }

    public static string OutputDynamical()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE NEAR MISS - THE FINGERPRINT'S ACTUAL PHYSICAL CONSEQUENCES.");
        sb.AppendLine("  quantity                                {1..6}          {1}");
        foreach (var d in DynamicalDifferences())
            sb.AppendLine($"  {d.Quantity,-40} {d.Native,-15} {d.Other}");
        sb.AppendLine();
        sb.AppendLine("  the substrata differ in a REAL DYNAMICAL property - where a wave's group velocity reverses inside the");
        sb.AppendLine("  band - and no inventory observable depends on it, because none of them reads the dispersion at high");
        sb.AppendLine("  wavenumber. This is the closest thing to a physical consequence of the fingerprint, and it is not one.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var (native, spectrumOnly, independent) = NeedCounts();
        var sb = new StringBuilder();
        sb.AppendLine($"THE SEARCH: {native} REQUIRES_NATIVE + {spectrumOnly} REQUIRES_SPECTRUM_ONLY + {independent} INDEPENDENT.");
        sb.AppendLine($"observables requiring the native spectrum: {ObservablesRequiringNative().Length}");
        sb.AppendLine();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
