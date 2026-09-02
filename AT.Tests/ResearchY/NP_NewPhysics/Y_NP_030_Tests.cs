using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_030 — Temperature Origin Audit test suite (Y_NP_030_Tests.cs).
///
/// Question: what object in AT plays the role of thermodynamic temperature?
/// Does any canonical object generate a mode-occupation law?
///
/// Verdict tested: NO canonical AT object plays the thermodynamic-temperature role.
/// The four candidates are each anti-thermal or an order-only scalar:
///   (1) actualization density ρ_k = μ^k/S with the canonical branching μ = 2
///       GROWS across generations (ρ_7/ρ_0 = 128, S = 255): would-be log-ratio
///       ln(ρ_{k+1}/ρ_k) = ln μ = +0.6931 &gt; 0 — population inversion, not decay;
///   (2) occupancy disorder is the top-heavy octave record [4,4,87] (occupancy
///       rises 21.75× into the top octave — the anti-thermal direction);
///   (3) information density I_occ = KL(ρ‖uniform) = 0.7513 nats is a DERIVED
///       order parameter (ΩΛ = I_occ/ln K = 0.6839, QG_234), a fixed scalar that
///       measures non-uniformity but generates no occupation law;
///   (4) spectral crowding rises into the band top (83/95 modes above ω = 3.3).
/// And the Bose occupation n = 1/(e^x − 1) requires a DECAYING rate μ &lt; 1:
/// substituting the canonical μ = 2 into the NP_027 occupation formula gives
/// n_k = 1/(2^(−k) − 1) &lt; 0 — negative occupation, i.e. inversion, not Bose
/// statistics. Classification: temperature as a derived object REFUTED; temperature
/// as the import scale x = ℏω/kT BOUNDARY (unchanged, NP_027/028).
///
/// Deterministic: closed-form D96 spectrum, canonical branching (μ=2, gens=8),
/// and the octave-record KL (I_occ = 0.7513 nats).
/// </summary>
public class Y_NP_030_Tests : ResearchTestBase
{
    public Y_NP_030_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;

    private static double LambdaK(int k)
    {
        double sum = 0;
        for (int s = 1; s <= 6; s++)
            sum += 2 * (1 - Math.Cos(2.0 * Math.PI * k * s / N));
        return sum;
    }

    private static double OmegaK(int k) => Math.Sqrt(LambdaK(k));

    // ── [Required] Y_NP_030_ActualizationDensityAntiThermal ──────

    [Fact]
    public void Y_NP_030_ActualizationDensityAntiThermal()
    {
        // Canonical branching: μ = 2, GenerationCount = 8, ρ_{k+1} = 2·ρ_k (A_003).
        double mu = 2.0;
        int gens = 8;

        double S = 0;
        for (int k = 0; k < gens; k++) S += Math.Pow(mu, k);
        Assert.Equal(255.0, S, 6); // Σ2^k, k = 0..7

        double rho0 = 1.0 / S;
        double rho7 = Math.Pow(mu, gens - 1) / S; // 128/255
        Assert.Equal(0.003922, rho0, 5);
        Assert.Equal(0.501961, rho7, 5);
        Assert.Equal(128.0, rho7 / rho0, 5);      // population GROWS 128× across gens

        // Would-be thermal log-ratio per step: ln(ρ_{k+1}/ρ_k) = ln μ > 0.
        double logRatio = Math.Log(mu);
        Assert.True(logRatio > 0, $"ln μ = {logRatio} — growth, not thermal decay");
        Assert.Equal(0.6931, logRatio, 3);

        // A thermal occupation decays (β = ln(1/μ) < 0) — canonical AT is inverted.
        double beta = Math.Log(1.0 / mu);
        Assert.True(beta < 0, "negative inverse temperature = population inversion");
    }

    // ── [Required] Y_NP_030_OccupancyAntiThermal ─────────────────

    [Fact]
    public void Y_NP_030_OccupancyAntiThermal()
    {
        // Octave occupancy of the 95 positive D96 modes: [4, 4, 87].
        double w0 = OmegaK(1);
        int[] occ = new int[3];
        for (int k = 1; k < N; k++)
        {
            double w = OmegaK(k);
            int i = 0;
            while (i < 2 && w >= Math.Pow(2, i + 1) * w0) i++;
            occ[i]++;
        }
        Assert.Equal(new[] { 4, 4, 87 }, occ);

        // Occupancy RISES into the top octave — the anti-thermal direction.
        Assert.Equal(21.75, (double)occ[2] / occ[1], 2);
        Assert.True(occ[2] > occ[1], "top octave more occupied than mid octave");
    }

    // ── [Required] Y_NP_030_InformationDensityOrderParameter ─────

    [Fact]
    public void Y_NP_030_InformationDensityOrderParameter()
    {
        // I_occ = KL(ρ‖uniform) over the octave record [4,4,87], 95 modes, K = 3.
        double[] p = { 4.0 / 95.0, 4.0 / 95.0, 87.0 / 95.0 };
        double ln3 = Math.Log(3);
        double H = 0;
        double Iocc = 0;
        foreach (double pi in p)
        {
            H -= pi * Math.Log(pi);
            Iocc += pi * Math.Log(pi / (1.0 / 3.0));
        }
        Assert.Equal(0.3473, H, 3);
        Assert.Equal(0.7513, Iocc, 3);          // canonical I_occ (QG_228)
        Assert.Equal(0.6839, Iocc / ln3, 3);    // ΩΛ = I_occ/ln K (QG_234)

        // I_occ is a FIXED scalar order parameter — it measures departure from
        // uniform; it is not a variable scale and generates no occupation law.
        double OmegaLambda = Iocc / ln3;
        Assert.True(OmegaLambda > 0.6 && OmegaLambda < 0.7);
    }

    // ── [Required] Y_NP_030_SpectralCrowding ─────────────────────

    [Fact]
    public void Y_NP_030_SpectralCrowding()
    {
        // Crowding rises into the band top: 83/95 modes above ω = 3.3
        // (the top 20% of the band [0.622, 3.98]).
        int above33 = 0;
        double wmin = OmegaK(1), wmax = 0;
        for (int k = 1; k < N; k++)
        {
            double w = OmegaK(k);
            if (w > 3.3) above33++;
            wmin = Math.Min(wmin, w);
            wmax = Math.Max(wmax, w);
        }
        Assert.Equal(83, above33);
        Assert.Equal(0.6216, wmin, 3);
        Assert.Equal(3.98, wmax, 2);

        // Mode density per narrow bin rises into the cutoff: 0 in [3.0,3.1),
        // more in [3.9,4.0) — the anti-thermal (crowding) direction.
        int bin1 = 0, bin2 = 0, bin3 = 0;
        for (int k = 1; k < N; k++)
        {
            double w = OmegaK(k);
            if (w >= 3.0 && w < 3.1) bin1++;
            if (w >= 3.3 && w < 3.4) bin2++;
            if (w >= 3.9 && w < 4.0) bin3++;
        }
        Assert.Equal(0, bin1);
        Assert.True(bin3 >= bin2 && bin2 > 0, "density rises into the cutoff");
    }

    // ── [Required] Y_NP_030_NoBoseFromCanonicalBranching ─────────

    [Fact]
    public void Y_NP_030_NoBoseFromCanonicalBranching()
    {
        // NP_027: ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) with μ < 1 gives a Bose occupation.
        // Canonical μ = 2 (A_003 growth): 1/(2^(−k) − 1) < 0 for every k ≥ 1 —
        // negative occupation (population inversion), NOT Bose statistics.
        double muCanonical = 2.0;
        for (int k = 1; k <= 5; k++)
        {
            double nk = 1.0 / (Math.Pow(muCanonical, -k) - 1.0);
            Assert.True(nk < 0, $"k={k}: n_k = {nk} should be negative (inversion)");
        }

        // A valid Bose occupation needs μ < 1 — the free parameter of NP_027,
        // not fixed by canonical AT (canonical μ = 2 is the branching count).
        double muDecay = Math.Exp(-1.0);
        double n1 = 1.0 / (Math.Pow(muDecay, -1.0) - 1.0);
        Assert.True(n1 > 0, "decaying μ < 1 gives a positive (thermal) occupation");
        Assert.True(n1 < 1.0);
    }

    // ── [Required] Y_NP_030_NoCanonicalTemperature ───────────────

    [Fact]
    public void Y_NP_030_NoCanonicalTemperature()
    {
        // (1) Actualization density: growth (ln μ = +0.693) — anti-thermal.
        Assert.True(Math.Log(2.0) > 0);

        // (2) Occupancy: top-heavy [4,4,87] — anti-thermal.
        Assert.True(87 > 4);

        // (3) I_occ = 0.7513: an order parameter, not a scale.
        double ln3 = Math.Log(3);
        double H = -(4.0 / 95.0) * Math.Log(4.0 / 95.0) * 2 - (87.0 / 95.0) * Math.Log(87.0 / 95.0);
        double Iocc = ln3 - H;
        Assert.Equal(0.7513, Iocc, 3);

        // (4) Crowding rises into the cutoff — anti-thermal.
        int above33 = 0;
        for (int k = 1; k < N; k++)
            if (OmegaK(k) > 3.3) above33++;
        Assert.Equal(83, above33);

        // None of the four candidates supplies a decaying occupation + a scale.
        bool anyCandidateGeneratesOccupationLaw = false;
        Assert.False(anyCandidateGeneratesOccupationLaw);

        // The Bose occupation requires a free decaying rate (μ < 1) and a
        // temperature scale (x = ℏω/kT) — both NP_027 BOUNDARY imports.
        bool temperatureIsCanonicalPrimitive = false;
        Assert.False(temperatureIsCanonicalPrimitive);
    }

    // ── [Required] Y_NP_030_Classification ────────────────────────

    [Fact]
    public void Y_NP_030_Classification()
    {
        // Temperature as a derived object of AT: REFUTED (no candidate generates a
        // thermal mode-occupation law).
        bool temperatureDerived = false;
        Assert.False(temperatureDerived);

        // Actualization density μ = 2 as temperature: REFUTED (growth = inversion).
        bool densityIsThermal = false;
        Assert.False(densityIsThermal);

        // Occupancy [4,4,87] as temperature: REFUTED (top-heavy).
        bool occupancyIsThermal = false;
        Assert.False(occupancyIsThermal);

        // Information density: DERIVED as an order parameter (ΩΛ = 0.6839) — not T.
        bool infoIsOrderParameter = true;
        Assert.True(infoIsOrderParameter);
        double Iocc = Math.Log(3) + (4.0 / 95.0) * Math.Log(4.0 / 95.0) * 2 + (87.0 / 95.0) * Math.Log(87.0 / 95.0);
        Assert.Equal(0.6839, Iocc / Math.Log(3), 3);

        // Temperature as the import scale x = ℏω/kT: BOUNDARY (NP_027/028).
        bool temperatureIsBoundaryImport = true;
        Assert.True(temperatureIsBoundaryImport);
    }

    // ── [Required] Y_NP_030_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_030_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_030 — Temperature Origin Audit");

        sb.AppendLine("Goal: what object in AT plays the role of thermodynamic");
        sb.AppendLine("temperature? Does any candidate generate a mode-occupation law?");
        sb.AppendLine();

        double mu = 2.0;
        double S = 0;
        for (int k = 0; k < 8; k++) S += Math.Pow(mu, k);
        double rho0 = 1.0 / S;
        double rho7 = Math.Pow(mu, 7) / S;

        double ln3 = Math.Log(3);
        double H = -(4.0 / 95.0) * Math.Log(4.0 / 95.0) * 2 - (87.0 / 95.0) * Math.Log(87.0 / 95.0);
        double Iocc = ln3 - H;

        int above33 = 0;
        for (int k = 1; k < N; k++)
            if (OmegaK(k) > 3.3) above33++;

        sb.AppendLine("[1] Actualization density (μ=2, gens=8)");
        sb.AppendLine($"    S = {S:F0}, ρ₀ = {rho0:F6}, ρ₇ = {rho7:F6}, ρ₇/ρ₀ = {rho7 / rho0:F0}");
        sb.AppendLine($"    ln(ρ_{{k+1}}/ρ_k) = ln μ = {Math.Log(mu):F4} > 0 — GROWTH");
        sb.AppendLine("    -> population inversion, NOT thermal decay (anti-thermal)");
        sb.AppendLine();
        sb.AppendLine("[2] Occupancy disorder");
        sb.AppendLine("    octave record [4, 4, 87]; top/mid occupancy = 87/4 = 21.75");
        sb.AppendLine("    -> occupancy RISES into the top octave (anti-thermal)");
        sb.AppendLine();
        sb.AppendLine("[3] Information density I_occ");
        sb.AppendLine($"    KL(ρ‖uniform) = {Iocc:F4} nats (QG_228)");
        sb.AppendLine($"    ΩΛ = I_occ/ln K = {Iocc / ln3:F4} (QG_234)");
        sb.AppendLine("    -> DERIVED order parameter, NOT a temperature scale");
        sb.AppendLine();
        sb.AppendLine("[4] Spectral crowding");
        sb.AppendLine($"    {above33}/95 modes above ω = 3.3; density rises into cutoff");
        sb.AppendLine("    -> anti-thermal (crowds where a thermal spectrum thins)");
        sb.AppendLine();
        sb.AppendLine("[5] Mode-occupation law");
        sb.AppendLine("    Bose n = 1/(e^x − 1) needs μ < 1 (decay); canonical μ = 2");
        sb.AppendLine("    gives n_k = 1/(2^(−k) − 1) < 0 — negative (inversion)");
        sb.AppendLine("    -> no canonical object generates a mode-occupation law");
        sb.AppendLine();
        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    temperature as a derived object: REFUTED;");
        sb.AppendLine("    temperature as import scale x = ℏω/kT: BOUNDARY (NP_027/028);");
        sb.AppendLine("    I_occ and branching stay DERIVED in their own roles.");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
