using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class GenerationSpectrumAnalyzer
{
    public static GSResult RunFullAnalysis()
    {
        var levels = BuildLeptonLevels();
        var mechs = BuildMechanisms();
        return new GSResult(BuildA(),BuildB(levels),BuildC(levels),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),levels,mechs);
    }

    static GenLevel[] BuildLeptonLevels()
    {
        return new GenLevel[]
        {
            new GenLevel("Electron (e)",0.511,1.0,"n=1 vortex (ground state)","Base frequency band ω_e. Lowest stable architecture.","STABLE: Ground state. Cannot decay (n=1, topological protection)."),
            new GenLevel("Muon (mu)",105.66,206.8,"n=1 vortex (EXCITED, same topology)","Frequency band ω_mu ~ 207×ω_e. Same winding, higher oscillation energy.","METASTABLE: Same topology, higher frequency. Decays to electron (τ_mu = 2.2 μs) via frequency de-excitation + weak interaction."),
            new GenLevel("Tau (tau)",1776.86,16.82,"n=1 vortex (EXCITED, same topology)","Frequency band ω_tau ~ 3478×ω_e. Highest lepton excitation.","METASTABLE: Decays to muon/electron (τ_tau = 2.9e-13 s). Fast de-excitation."),
        };
    }

    static GenMechanism[] BuildMechanisms()
    {
        return new GenMechanism[]
        {
            new GenMechanism("Topological sectors","Generations = distinct winding numbers (n=1,2,3).","NO: e, μ, τ all have n=1 (same charge, same topology). Winding distinguishes TYPE (e vs ν vs quark), not generation.","REJECTED: Generations are NOT distinct topologies. They share n=1."),
            new GenMechanism("Frequency excitation levels","Generations = excitation modes of the SAME topology. e = ground state, μ = 1st excited, τ = 2nd excited. Like electron orbitals: 1s, 2s, 3s.","PARTIALLY: Explains WHY e/μ/τ share topology but differ in mass (frequency). Does NOT explain why exactly 3 levels.","PROMISING: Correct qualitative picture. Quantization mechanism missing."),
            new GenMechanism("Architectural resonance quantization","Frequency bands are quantized by resonance conditions of the n=1 architecture. Only certain discrete ω values are self-consistent (like standing waves in a cavity).","PARTIALLY: Explains discreteness of masses. Does NOT derive the specific 3 allowed bands.","SPECULATIVE: Resonance quantization plausible but unproven."),
            new GenMechanism("Stability cutoff (4th gen)","Higher generations become so massive/unstable that they decay instantly or are forbidden by EW constraints. 4th gen ruled out by Z-width (N_ν=3).","YES (empirically): LEP measured N_ν = 2.984±0.008. 4th LIGHT generation EXCLUDED. 4th HEAVY generation disfavored by Higgs production.","EMPIRICAL: Explains why we see 3 (4th excluded), but doesn't DERIVE 3."),
            new GenMechanism("Higgs coupling pattern","Yukawa couplings y_e : y_μ : y_τ determine mass hierarchy. If y_4 >> y_3, 4th gen is too heavy to be stable matter.","PARTIALLY: Explains mass hierarchy via coupling. Couplings themselves unexplained.","INCOMPLETE: Yukawa hierarchy is the thing to be explained."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE THREE-GENERATION MYSTERY");
        sb.AppendLine();
        sb.AppendLine("  Observed fermions come in EXACTLY THREE generations:");
        sb.AppendLine("    Leptons:  (e, mu, tau),  (nu_e, nu_mu, nu_tau)");
        sb.AppendLine("    Quarks:   (u, c, t),     (d, s, b)");
        sb.AppendLine();
        sb.AppendLine("  What is KNOWN (empirically):");
        sb.AppendLine("    - Exactly 3 light generations (LEP, 1989: N_nu = 2.984 ± 0.008).");
        sb.AppendLine("    - Each generation repeats the same quantum numbers.");
        sb.AppendLine("    - Mass increases dramatically between generations.");
        sb.AppendLine();
        sb.AppendLine("  What is UNKNOWN (even in the SM):");
        sb.AppendLine("    - WHY exactly 3 (not 2, 4, or infinitely many).");
        sb.AppendLine("    - WHY masses are what they are (Yukawa couplings are free).");
        sb.AppendLine("    - WHY the hierarchy m_e : m_mu : m_tau = 1 : 207 : 3478.");
        sb.AppendLine();
        sb.AppendLine("  TQM'S STARTING POINT (from QG-034):");
        sb.AppendLine("    All three charged leptons are n=1 vortices — SAME topology.");
        sb.AppendLine("    They differ ONLY in frequency (architecture, QG-028).");
        sb.AppendLine("    Generations are therefore NOT distinct topological sectors.");
        sb.AppendLine("    They are EXCITATION LEVELS of one topology.");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION THIS EXPERIMENT ASKS:");
        sb.AppendLine("    Can TQM derive 'exactly 3' from the frequency architecture");
        sb.AppendLine("    of the n=1 vortex? Or does '3' remain an empirical input?");
        return sb.ToString();
    }

    static string BuildB(GenLevel[] levels)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LEPTON EXCITATION SPECTRUM");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,10} {2,12} {3}", "Level", "Mass(MeV)", "Ratio m/m_prev", "Interpretation"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var l in levels)
        {
            string ratio = l.RatioToPrev > 1 ? "×"+l.RatioToPrev.ToString("F1", CultureInfo.InvariantCulture) : "ground";
            string interp = l.FrequencyBand.Length > 55 ? l.FrequencyBand[..52]+"..." : l.FrequencyBand;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,10:F2} {2,12} {3}", l.Name, l.Mass_MeV, ratio, interp));
        }
        sb.AppendLine();
        sb.AppendLine("  QUANTITATIVE ANALYSIS:");
        sb.AppendLine("    m_mu/m_e   = 206.77  (not 100, not 1000 — irregular)");
        sb.AppendLine("    m_tau/m_mu = 16.82   (not 207 again — irregular)");
        sb.AppendLine("    m_tau/m_e  = 3477.5  (total span)");
        sb.AppendLine();
        sb.AppendLine("  THE RATIOS ARE IRREGULAR:");
        sb.AppendLine("    No clean geometric progression (206.8 ≠ 16.8).");
        sb.AppendLine("    No simple integer ratio (207 ≈ 13×16, but not exact).");
        sb.AppendLine("    No power law (16.8 ≠ 206.8^0.5 = 14.4).");
        sb.AppendLine("    This suggests the masses are set by a COMPLICATED");
        sb.AppendLine("    architectural resonance, not a simple ladder.");
        sb.AppendLine();
        sb.AppendLine("  NEAR-MISS PATTERNS (all fail):");
        sb.AppendLine("    - Geometric: 207 vs 16.8 — NOT geometric.");
        sb.AppendLine("    - Harmonic: ratios not simple fractions.");
        sb.AppendLine("    - Koide: m_e+m_mu+m_tau = (2/3)(sqrt(m_e)+sqrt(m_mu)+sqrt(m_tau))^2");
        sb.AppendLine("      — holds to 10^-5! But WHY? Unknown. (Koide formula, 1981).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Generation masses form an IRREGULAR frequency ladder.");
        sb.AppendLine("  The Koide relation is a tantalizing hint of underlying");
        sb.AppendLine("  architecture but has no TQM derivation yet.");
        return sb.ToString();
    }

    static string BuildC(GenLevel[] levels)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FREQUENCY QUANTIZATION");
        sb.AppendLine();
        sb.AppendLine("  ARE GENERATIONS QUANTIZED FREQUENCY LEVELS?");
        sb.AppendLine();
        sb.AppendLine("  ANALOGY: ELECTRON ORBITALS IN AN ATOM");
        sb.AppendLine("    Hydrogen energy levels: E_n = -13.6 eV / n^2.");
        sb.AppendLine("    Discrete, quantized, derivable from the Coulomb potential.");
        sb.AppendLine("    n = 1, 2, 3, ... infinite but increasingly unstable.");
        sb.AppendLine();
        sb.AppendLine("  TQM ANALOGY: n=1 VORTEX EXCITATION LEVELS");
        sb.AppendLine("    The n=1 vortex has a frequency architecture (QG-027/028).");
        sb.AppendLine("    Excited states = higher frequency bands of the SAME topology.");
        sb.AppendLine("    e = ground state (ω_e). μ = 1st excited (ω_μ ~ 207 ω_e).");
        sb.AppendLine("    τ = 2nd excited (ω_τ ~ 3478 ω_e).");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS ANALOGY IS GOOD:");
        sb.AppendLine("    - Explains shared topology (same charge, spin).");
        sb.AppendLine("    - Explains mass difference (different frequency).");
        sb.AppendLine("    - Explains decay (de-excitation: μ→e, τ→μ/e).");
        sb.AppendLine("    - Explains hierarchy (higher excitation = higher mass).");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS ANALOGY IS INCOMPLETE:");
        sb.AppendLine("    - Atomic levels derive from a KNOWN potential (Coulomb).");
        sb.AppendLine("    - Vortex excitation levels would derive from the ARCHITECTURAL");
        sb.AppendLine("      potential of the n=1 winding — WHICH TQM HAS NOT DERIVED.");
        sb.AppendLine("    - The specific frequencies ω_e, ω_μ, ω_τ are NOT predicted.");
        sb.AppendLine("    - WHY 3 stable levels (not 2 or 4)? NOT derived.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Frequency quantization is the RIGHT FRAMEWORK");
        sb.AppendLine("  (generations = excitation levels) but the quantizing");
        sb.AppendLine("  potential is NOT derived. The picture is qualitative.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FOURTH GENERATION: EXCLUDED");
        sb.AppendLine();
        sb.AppendLine("  WOULD A 4TH GENERATION EXIST?");
        sb.AppendLine("    If generations are excitation levels, a 4th level (t', b', tau', nu')");
        sb.AppendLine("    would be the next frequency band above tau.");
        sb.AppendLine("    Why don't we see it?");
        sb.AppendLine();
        sb.AppendLine("  EMPIRICAL CONSTRAINTS:");
        sb.AppendLine("  1. LEP Z-WIDTH (1989):");
        sb.AppendLine("     Z boson decays to all fermions with m < m_Z/2.");
        sb.AppendLine("     Measured N_nu = 2.984 ± 0.008 (light neutrino species).");
        sb.AppendLine("     A 4th LIGHT neutrino (m_4 < 45 GeV) is EXCLUDED at 5+ sigma.");
        sb.AppendLine("     → 4th generation, if it exists, must have HEAVY neutrino.");
        sb.AppendLine();
        sb.AppendLine("  2. HIGGS PRODUCTION (2012):");
        sb.AppendLine("     Higgs production via gluon fusion is enhanced by a factor ~9");
        sb.AppendLine("     with a 4th generation of heavy quarks (gg→H loop amplitude).");
        sb.AppendLine("     LHC measured Higgs production rate consistent with 3 generations.");
        sb.AppendLine("     → 4th HEAVY generation strongly DISFAVORED (excluded for most masses).");
        sb.AppendLine();
        sb.AppendLine("  3. ELECTROWEAK PRECISION:");
        sb.AppendLine("     S and T parameters. 4th generation alters loop corrections.");
        sb.AppendLine("     Measurements consistent with 3 generations only.");
        sb.AppendLine();
        sb.AppendLine("  TQM INTERPRETATION:");
        sb.AppendLine("    A 4th generation would be a 4th excitation level of n=1.");
        sb.AppendLine("    Empirically: NOT OBSERVED. Either:");
        sb.AppendLine("      (a) The architecture has exactly 3 stable excitation levels.");
        sb.AppendLine("      (b) The 4th level is so massive it decays instantly.");
        sb.AppendLine("      (c) The 4th level is forbidden by an (unknown) selection rule.");
        sb.AppendLine();
        sb.AppendLine("  TQM CANNOT YET DISTINGUISH THESE.");
        sb.AppendLine("  The absence of a 4th generation is OBSERVED, not DERIVED.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NEUTRINO GENERATION MAPPING");
        sb.AppendLine();
        sb.AppendLine("  NEUTRINOS: THE SAME THREE-GENERATION STRUCTURE");
        sb.AppendLine("    nu_e (m < 0.8 eV), nu_mu (< 0.19 MeV), nu_tau (< 18.2 MeV).");
        sb.AppendLine("    Direct mass limits only. Oscillations give mass DIFFERENCES:");
        sb.AppendLine("      Delta m_21^2 = 7.5e-5 eV^2 (solar).");
        sb.AppendLine("      |Delta m_32^2| = 2.5e-3 eV^2 (atmospheric).");
        sb.AppendLine("    Sum of masses: < 0.12 eV (cosmology).");
        sb.AppendLine();
        sb.AppendLine("  NEUTRINOS vs CHARGED LEPTONS:");
        sb.AppendLine("    Charged leptons: HUGE mass hierarchy (1 : 207 : 3478).");
        sb.AppendLine("    Neutrinos: TINY masses, nearly degenerate or mildly hierarchical.");
        sb.AppendLine("    m_nu / m_e < 10^-6 — neutrinos are ~10^6 times lighter!");
        sb.AppendLine();
        sb.AppendLine("  TQM INTERPRETATION:");
        sb.AppendLine("    Neutrinos are n=1 vortices with NEARLY-DECOUPLED cores");
        sb.AppendLine("    (QG-034). Their architecture barely interacts with the");
        sb.AppendLine("    amplitude mode (Higgs VEV, QG-037). Hence tiny masses.");
        sb.AppendLine();
        sb.AppendLine("    WHY SO DIFFERENT FROM CHARGED LEPTONS?");
        sb.AppendLine("    Charged lepton couples to phase (charge, U(1)) AND amplitude.");
        sb.AppendLine("    Neutrino couples ONLY to amplitude (no charge) — weakly.");
        sb.AppendLine("    The charge interaction 'anchors' the architecture to the");
        sb.AppendLine("    amplitude mode, giving larger mass. Neutrino lacks this anchor.");
        sb.AppendLine();
        sb.AppendLine("  SPECULATIVE: The neutrino mass hierarchy may be INVERTED or");
        sb.AppendLine("  normal, and nearly degenerate. This differs qualitatively");
        sb.AppendLine("  from the charged lepton hierarchy. TQM does NOT yet explain");
        sb.AppendLine("  this asymmetry. It remains an OPEN PROBLEM.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE SPECTRUM AUDIT");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: Generate 0, 1, 2, 4, 5+ stable generations.");
        sb.AppendLine();
        sb.AppendLine("  0 GENERATIONS: Impossible. Without fermions, no matter. Void.");
        sb.AppendLine();
        sb.AppendLine("  1 GENERATION: Impossible? e only. No CP violation from CKM");
        sb.AppendLine("    (needs 3+). No matter/anti-matter asymmetry (Sakharov).");
        sb.AppendLine("    Universe would be empty (matter annihilates).");
        sb.AppendLine();
        sb.AppendLine("  2 GENERATIONS: e, μ only. No tau. CKM matrix 2×2 — real, no");
        sb.AppendLine("    CP violation. Insufficient for baryogenesis. Likely no matter.");
        sb.AppendLine();
        sb.AppendLine("  3 GENERATIONS: OBSERVED. CP violation possible (CKM phase).");
        sb.AppendLine("    Baryogenesis possible. Matter survives. US WORKS.");
        sb.AppendLine();
        sb.AppendLine("  4 GENERATIONS: EXCLUDED by Z-width (light) and Higgs production");
        sb.AppendLine("    (heavy). Would exist only if 4th neutrino is heavy (>45 GeV)");
        sb.AppendLine("    and 4th quarks very heavy. Not observed.");
        sb.AppendLine();
        sb.AppendLine("  5+ GENERATIONS: Even more excluded. No evidence.");
        sb.AppendLine();
        sb.AppendLine("  THE ANTHROPIC ARGUMENT (unfalsifiable but suggestive):");
        sb.AppendLine("    - 3 generations: CP violation + stable matter. WORKS.");
        sb.AppendLine("    - <3 generations: no CP violation, no matter. FAILS.");
        sb.AppendLine("    - >3 generations: empirically excluded. ABSENT.");
        sb.AppendLine("    3 is the MINIMUM number enabling CP violation");
        sb.AppendLine("    (baryogenesis) while remaining observationally consistent.");
        sb.AppendLine();
        sb.AppendLine("  BUT THIS IS SELECTION, NOT DERIVATION:");
        sb.AppendLine("    '3 is what works' ≠ '3 is what must be'.");
        sb.AppendLine("    TQM does NOT derive 3. It OBSERVES 3 and notes");
        sb.AppendLine("    3 is the minimum for CP violation.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'TQM HAS NOT DERIVED THREE GENERATIONS':");
        sb.AppendLine("     CORRECT. The honest assessment is that TQM describes");
        sb.AppendLine("     generations (excitation levels of n=1) but does NOT");
        sb.AppendLine("     derive the count '3'. This experiment FAILS the");
        sb.AppendLine("     'derive 3' goal — and says so plainly.");
        sb.AppendLine();
        sb.AppendLine("  2. 'THE ORBITAL ANALOGY IS A METAPHOR, NOT A DERIVATION':");
        sb.AppendLine("     CORRECT. Atomic orbitals derive from Coulomb potential.");
        sb.AppendLine("     Vortex excitation levels would derive from an architectural");
        sb.AppendLine("     potential that TQM has NOT specified. The analogy is");
        sb.AppendLine("     ILLUSTRATIVE, not DERIVATIVE.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE KOIDE RELATION IS OUTSIDE SCOPE':");
        sb.AppendLine("     The Koide formula holds to 10^-5 and hints at deep");
        sb.AppendLine("     architecture. TQM does NOT explain it. This is a");
        sb.AppendLine("     FAILURE to address the most promising numerical clue.");
        sb.AppendLine();
        sb.AppendLine("  4. 'MASS RATIOS ARE IRREGULAR = NO PATTERN = NO DERIVATION':");
        sb.AppendLine("     CORRECT. 206.8 and 16.8 are not a clean ladder.");
        sb.AppendLine("     Any claim of 'derivation' would be numerology.");
        sb.AppendLine("     TQM correctly REFUSES to fabricate a pattern.");
        sb.AppendLine();
        sb.AppendLine("  5. 'WHAT IS GENUINELY ACHIEVED':");
        sb.AppendLine("     - Generations = excitation levels, NOT distinct topologies.");
        sb.AppendLine("       (This resolves a common confusion.)");
        sb.AppendLine("     - Decay chains (τ→μ→e) = frequency de-excitation.");
        sb.AppendLine("     - Neutrino/charged-lepton asymmetry = charge anchoring.");
        sb.AppendLine("     - 3 = minimum for CP violation (anthropic/selection).");
        sb.AppendLine("     These are QUALITATIVE insights, not quantitative derivations.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  THREE GENERATIONS: DESCRIBED, NOT DERIVED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Generations = EXCITATION LEVELS of the SAME topology (n=1),");
        sb.AppendLine("      NOT distinct topological sectors.");
        sb.AppendLine("  Q2: e/μ/τ = frequency levels of n=1 winding. QUALITATIVE.");
        sb.AppendLine("  Q3: Why exactly 3? NOT DERIVED. Empirical (LEP N_ν=3).");
        sb.AppendLine("  Q4: m_τ > m_μ > m_e = increasing frequency band. Direction");
        sb.AppendLine("      correct, magnitudes NOT derived.");
        sb.AppendLine("  Q5: 4th generation EXCLUDED (Z-width + Higgs production).");
        sb.AppendLine("  Q6: Higher generations disfavored by instability/mass. Empirical.");
        sb.AppendLine("  Q7: Resonance quantization PLAUSIBLE but unproven.");
        sb.AppendLine("  Q8: Finite spectrum (3 observed) but TQM doesn't derive finiteness.");
        sb.AppendLine("  Q9: Neutrino generations: same mechanism, decoupled cores. Partial.");
        sb.AppendLine("  Q10: Generation count NOT derived. Remains empirical input.");
        sb.AppendLine();
        sb.AppendLine("  MASS RATIO ANALYSIS (the hard numbers):");
        sb.AppendLine("    m_mu/m_e = 206.8, m_tau/m_mu = 16.8, m_tau/m_e = 3477.5.");
        sb.AppendLine("    IRREGULAR. No clean ladder. Koide relation holds (10^-5)");
        sb.AppendLine("    but is UNEXPLAINED by TQM (and by the SM).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — QUALITATIVE EXPLANATION");
        sb.AppendLine();
        sb.AppendLine("    TQM provides the CORRECT FRAMEWORK:");
        sb.AppendLine("      Generations = excitation levels of the same topology.");
        sb.AppendLine("    But does NOT derive:");
        sb.AppendLine("      - The number 3.");
        sb.AppendLine("      - The specific masses.");
        sb.AppendLine("      - The irregular mass ratios.");
        sb.AppendLine();
        sb.AppendLine("    'Exactly three' remains an EMPIRICAL INPUT to TQM,");
        sb.AppendLine("    exactly as it is to the Standard Model.");
        sb.AppendLine("    The most promising clue (Koide relation) is UNEXPLAINED.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 39 experiments.");
        return sb.ToString();
    }
}
