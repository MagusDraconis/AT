namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 120 — Horizon suppression of families. QG119 showed local observers see FEWER octave
/// families than exist globally (a fixed horizon-24 window saturates at 2 families while the total grows to
/// 4 at N=192). This phase asks: does a FINITE HORIZON NATURALLY suppress higher-family modes?
///
/// Method (computational, fully deterministic): for a fixed global converged network (N=192, radius-2 class),
/// extract local window patches (induced subgraphs) at a grid of horizon sizes and measure (1) HORIZON SIZE —
/// observable octave-family count as a function of horizon; (2) MODE LOCALIZATION — mean inverse participation
/// ratio (IPR) of the global eigenmodes grouped by octave family (do higher families carry more localized
/// modes?); (3) FAMILY VISIBILITY — how many of the global families a given horizon resolves; (4) SPECTRAL
/// SUPPRESSION — the suppressed family count (total − visible) as the horizon shrinks, and whether the
/// observable count follows the log₂(h) spectral-resolution law of a windowed spectrum; (5) OBSERVABLE FAMILY
/// COUNT — the full observable-vs-horizon curve.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class HorizonFamilies
{
    /// <summary>Default dynamics parameters (matching QG115–119).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>Global network size for the suppression study.</summary>
    public const int GlobalN = 192;

    /// <summary>Horizon sizes swept (powers/semis roughly geometric, all ≤ GlobalN).</summary>
    public static readonly int[] HorizonGrid = { 8, 12, 16, 24, 32, 48, 64, 96, 128, 192 };

    // ── Global network + spectral machinery ────────────────────────────────────────

    /// <summary>Converged global network at the default class parameters (radius-2 plateau).</summary>
    public static double[,] GlobalNetwork(int n = GlobalN, double feedback = 0.7, double damping = 0.3,
        int K = DefaultK)
        => UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K, damping,
            feedback, 120);

    /// <summary>Total (whole-network) octave-family count.</summary>
    public static int TotalFamilies(int n = GlobalN, double feedback = 0.7, double damping = 0.3,
        int K = DefaultK)
        => StructureFromContent.FamilyCount(GlobalNetwork(n, feedback, damping, K));

    // ── 1. Horizon size → observable family count ─────────────────────────────────

    /// <summary>
    /// Observable octave-family count as a function of horizon size: the family count of the induced
    /// subgraph window of size h embedded in the global network.
    /// </summary>
    public static (int Horizon, int Families)[] ObservableFamiliesVsHorizon(double feedback = 0.7,
        double damping = 0.3, int K = DefaultK)
    {
        var net = GlobalNetwork(GlobalN, feedback, damping, K);
        var result = new List<(int, int)>();
        foreach (int h in HorizonGrid)
        {
            var patch = LocalVsGlobalAttractors.LocalWindowPatch(net, 0, h);
            result.Add((h, StructureFromContent.FamilyCount(patch)));
        }
        return result.ToArray();
    }

    // ── 2. Mode localization per family ────────────────────────────────────────────

    /// <summary>
    /// Mean inverse participation ratio (IPR) of the global eigenmodes grouped by octave family. IPR near 1
    /// = concentrated/localized; near 0 = delocalized. Higher families with higher IPR carry more localized
    /// modes — the natural reason a finite window fails to resolve them.
    /// </summary>
    public static (int Family, double MeanIPR)[] ModeLocalizationByFamily(double feedback = 0.7,
        double damping = 0.3, int K = DefaultK)
    {
        var laplacian = SpectrumRobustness.LaplacianOf(GlobalNetwork(GlobalN, feedback, damping, K));
        var (evals, vec) = SectorBoundaryPhysics.MatrixDecomposition(laplacian);
        int n = laplacian.GetLength(0);

        // stable frequencies (ascending, positive)
        var freqs = evals.Where(e => e > 1e-10).Select(e => Math.Sqrt(e)).OrderBy(e => e).ToArray();
        if (freqs.Length == 0) return Array.Empty<(int, double)>();

        // octave-family membership per stable frequency index
        double w0 = freqs[0];
        var familyIndex = new int[freqs.Length];
        int octave = 0;
        for (int i = 0; i < freqs.Length; i++)
        {
            while (freqs[i] >= w0 * Math.Pow(2.0, octave + 1) && octave < 40) octave++;
            familyIndex[i] = octave;
        }
        int nFamilies = (familyIndex.Length > 0 ? familyIndex.Max() : 0) + 1;

        // map each positive eigenvalue to its eigenvector index (same ordering as evals)
        var result = new List<(int, double)>();
        for (int f = 0; f < nFamilies; f++)
        {
            var members = Enumerable.Range(0, freqs.Length)
                .Where(i => familyIndex[i] == f)
                .ToArray();
            if (members.Length == 0) continue;
            double sumIpr = 0.0;
            foreach (int mi in members)
            {
                double ipr = 0.0;
                for (int i = 0; i < n; i++) ipr += vec[i, mi] * vec[i, mi] * vec[i, mi] * vec[i, mi];
                sumIpr += ipr;
            }
            result.Add((f + 1, sumIpr / members.Length));
        }
        return result.ToArray();
    }

    // ── 3. Family visibility ───────────────────────────────────────────────────────

    /// <summary>Visible families at a given horizon (the observable count).</summary>
    public static int VisibleFamilies(int horizon, double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var net = GlobalNetwork(GlobalN, feedback, damping, K);
        var patch = LocalVsGlobalAttractors.LocalWindowPatch(net, 0, Math.Min(horizon, GlobalN));
        return StructureFromContent.FamilyCount(patch);
    }

    // ── 4. Spectral suppression ────────────────────────────────────────────────────

    /// <summary>
    /// Suppression profile: for each horizon, total families, visible (observable) families, and suppressed
    /// count (total − visible).
    /// </summary>
    public static (int Horizon, int Total, int Visible, int Suppressed)[] SuppressionProfile(
        double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        int total = TotalFamilies(GlobalN, feedback, damping, K);
        var result = new List<(int, int, int, int)>();
        foreach (int h in HorizonGrid)
        {
            int visible = VisibleFamilies(h, feedback, damping, K);
            result.Add((h, total, visible, total - visible));
        }
        return result.ToArray();
    }

    /// <summary>
    /// Spectral-resolution law: does the observable family count grow like log₂(horizon) (the natural
    /// resolution limit of a windowed spectrum)? Checks monotone non-decreasing growth with horizon.
    /// </summary>
    public static bool ObservableCountGrowsWithHorizon(double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var curve = ObservableFamiliesVsHorizon(feedback, damping, K);
        for (int i = 1; i < curve.Length; i++)
            if (curve[i].Families < curve[i - 1].Families) return false;
        // must actually increase at least once (strict growth where the spectrum has more families to reveal)
        return curve.Any(c => c.Families > curve[0].Families);
    }

    /// <summary>Does the observable count saturate to the TOTAL at the full horizon (h = GlobalN)?</summary>
    public static bool SaturationAtFullHorizon(double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        int total = TotalFamilies(GlobalN, feedback, damping, K);
        int atFull = VisibleFamilies(GlobalN, feedback, damping, K);
        return atFull == total;
    }

    /// <summary>Is suppression strictly monotone (each smaller horizon sees no MORE families than the next)?</summary>
    public static bool SuppressionIsMonotone(double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var profile = SuppressionProfile(feedback, damping, K);
        for (int i = 1; i < profile.Length; i++)
            if (profile[i].Visible > profile[i - 1].Visible) return false;
        return profile[0].Suppressed >= profile[^1].Suppressed;
    }

    // ── 5. Observable family count ─────────────────────────────────────────────────

    /// <summary>Observable family count at a specific horizon.</summary>
    public static int ObservableFamilyCount(int horizon, double feedback = 0.7, double damping = 0.3,
        int K = DefaultK)
        => VisibleFamilies(horizon, feedback, damping, K);

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO SUPPRESSION     — the observable family count equals the total at EVERY horizon (a finite
    ///                        horizon changes nothing);
    ///   PARTIAL SUPPRESSION — suppression exists but is NOT systematic in the horizon (non-monotone curve,
    ///                        or the observable count does not track the window's spectral resolution);
    ///   HORIZON ORIGIN     — a finite horizon NATURALLY suppresses higher-family modes: the observable
    ///                        family count grows monotonically with horizon (the log₂ spectral-resolution
    ///                        law), saturating at the total for the full network — suppression is entirely
    ///                        attributable to the finite horizon.
    /// </summary>
    public static string Classify(double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var curve = ObservableFamiliesVsHorizon(feedback, damping, K);
        bool allEqual = curve.All(c => c.Families == curve[0].Families);
        if (allEqual) return "NO SUPPRESSION";

        bool grows = ObservableCountGrowsWithHorizon(feedback, damping, K);
        bool saturates = SaturationAtFullHorizon(feedback, damping, K);
        bool monotone = SuppressionIsMonotone(feedback, damping, K);

        if (grows && saturates && monotone) return "HORIZON ORIGIN";
        return "PARTIAL SUPPRESSION";
    }
}
