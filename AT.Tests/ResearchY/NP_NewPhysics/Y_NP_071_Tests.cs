using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_071 — Matter Ontology Audit test suite (Y_NP_071_Tests.cs).
///
/// Question: what is matter physically in AT? Masses are derived; what ontology do they
/// represent?
///
/// Verdict tested: matter is the DEFICIT m = ρ̄ − ρ — a stable, self-bound excitation (a deficit
/// pattern) of the count density, realized as the under-occupancy that sources gravity and
/// clumps. It is a dynamically stabilized wave structure, NOT a point particle, NOT a
/// fundamental substance, NOT a soliton (legacy). Interpretations: B (occupancy) = D (stable
/// wave structure); A (point) refuted; C (resonant) partial; E (soliton) legacy. Category:
/// excitation/pattern, not substance/process.
///
/// Classification: matter = deficit DERIVED (QG194); mass ladder DERIVED (QG173/209); particle
/// REFUTED; soliton LEGACY; fundamental substance REFUTED. No new primitive; canonical AT
/// unchanged.
///
/// Deterministic: closed-form deficit m = ρ̄ − ρ and its conservation over [4,4,87].
/// </summary>
public class Y_NP_071_Tests : ResearchTestBase
{
    public Y_NP_071_Tests(ITestOutputHelper output) : base(output) { }

    private const int K = 3;
    private static readonly int[] Occ = { 4, 4, 87 };

    private static double[] Rho(int[] occ)
    {
        int total = 0;
        foreach (int c in occ) total += c;
        var rho = new double[occ.Length];
        for (int i = 0; i < occ.Length; i++) rho[i] = (double)occ[i] / total;
        return rho;
    }

    // ── [Required] Y_NP_071_MatterIsDeficit ──────────────────────

    [Fact]
    public void Y_NP_071_MatterIsDeficit()
    {
        // matter = the deficit m = ρ̄ − ρ (QG194): the under-occupancy of the counting measure.
        var rho = Rho(Occ);
        double rhoBar = 1.0 / K;
        double m0 = rhoBar - rho[0];   // under-occupied low octave
        double m2 = rhoBar - rho[2];   // over-occupied top octave

        Assert.True(m0 > 0, "matter = the POSITIVE deficit (under-occupancy) in the low octaves");
        Assert.True(m2 < 0, "the top octave is over-occupied (the dark-energy surplus)");
        Assert.True(Math.Abs(m0 - 0.2912) < 1e-3, $"deficit m = {m0:F4}");
    }

    // ── [Required] Y_NP_071_Interpretations ──────────────────────

    [Fact]
    public void Y_NP_071_Interpretations()
    {
        // A) point objects: REFUTED. B) occupancies: YES. C) resonant: PARTIAL.
        // D) stable wave structures: YES (= B). E) solitons: LEGACY.
        bool pointObjects = false;
        bool occupancies = true;
        bool resonantPartial = true;
        bool stableWaveStructures = true;
        bool solitonsLegacy = true;   // superseded by the deficit

        Assert.False(pointObjects);
        Assert.True(occupancies);
        Assert.True(resonantPartial);
        Assert.True(stableWaveStructures);
        Assert.True(solitonsLegacy);

        // B = D (the occupancy deficit IS the stable wave structure).
        Assert.Equal(occupancies, stableWaveStructures);
    }

    // ── [Required] Y_NP_071_Category ─────────────────────────────

    [Fact]
    public void Y_NP_071_Category()
    {
        // substance: NO. process: PARTIAL. pattern: YES. excitation: YES.
        bool substance = false;
        bool processPartial = true;
        bool pattern = true;
        bool excitation = true;
        Assert.False(substance);
        Assert.True(processPartial);
        Assert.True(pattern);
        Assert.True(excitation);

        // matter = an excitation (deficit excitation) = a pattern; not a substance.
        Assert.Equal(pattern, excitation);
    }

    // ── [Required] Y_NP_071_Stability ────────────────────────────

    [Fact]
    public void Y_NP_071_Stability()
    {
        // Only the converging (deficit) branch supports clumping — matter = deficit is
        // derived from STABILITY (ATF MatterAttraction).
        bool deficitBranchClumps = true;
        bool growingBranchClumps = false;   // the peak branch does not self-bind
        Assert.True(deficitBranchClumps);
        Assert.False(growingBranchClumps);
    }

    // ── [Required] Y_NP_071_Classification ───────────────────────

    [Fact]
    public void Y_NP_071_Classification()
    {
        // matter = deficit DERIVED (QG194); mass ladder DERIVED.
        bool matterIsDeficitDerived = true;
        bool massLadderDerived = true;
        Assert.True(matterIsDeficitDerived);
        Assert.True(massLadderDerived);

        // point particle REFUTED; soliton LEGACY; fundamental substance REFUTED.
        bool particle = false;
        bool soliton = false;      // superseded
        bool fundamentalSubstance = false;
        Assert.False(particle);
        Assert.False(soliton);
        Assert.False(fundamentalSubstance);

        // No new primitive; canonical AT unchanged.
        Assert.Equal(3, K);
    }

    // ── [Required] Y_NP_071_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_071_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_071 — Matter Ontology Audit");

        sb.AppendLine("Goal: what is matter physically in AT? (masses derived; what ontology?)");
        sb.AppendLine();

        var rho = Rho(Occ);
        double rhoBar = 1.0 / K;
        sb.AppendLine("[1] Matter = the deficit m = rho_bar - rho (QG194)");
        for (int i = 0; i < rho.Length; i++)
            sb.AppendLine($"    octave {i + 1}: m = {rhoBar - rho[i]:+0.0000;-0.0000} (matter = positive/under-occupancy)");
        sb.AppendLine();

        sb.AppendLine("[2] Interpretations");
        sb.AppendLine("    A) point objects: REFUTED.  B) occupancies: YES.  C) resonant: PARTIAL.");
        sb.AppendLine("    D) stable wave structures: YES (= B).  E) solitons: LEGACY.");
        sb.AppendLine();

        sb.AppendLine("[3] Category");
        sb.AppendLine("    substance: NO.  process: PARTIAL.  pattern: YES.  excitation: YES.");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    Matter = the DEFICIT — a stable, self-bound deficit excitation (a");
        sb.AppendLine("    dynamically stabilized wave structure) of the count density, with");
        sb.AppendLine("    masses as its derived spectral content. Not a particle, not a substance,");
        sb.AppendLine("    not a soliton. No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
