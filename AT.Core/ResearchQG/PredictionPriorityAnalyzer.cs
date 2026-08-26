using System.Globalization;

namespace AT.Core.ResearchQG;

public static class PredictionPriorityAnalyzer
{
    public static PPRResult RunFullAnalysis()
    {
        var ranking = BuildRanking();
        return new PPRResult(BuildA(),BuildB(ranking),BuildC(ranking),BuildD(),BuildE(ranking),BuildF(),BuildG(),BuildH(),BuildI(),ranking);
    }

    // Score = falsification power * feasibility (0-1 scale). Higher = test first.
    static RankedPrediction[] BuildRanking()
    {
        return new RankedPrediction[]
        {
            new RankedPrediction("Evolving RAR: g†(z) = c·H(z)/(2π)","KMOS3D archival data","<1 year (archival)",0.90,0.95,0.86,"#1 — FASTEST + high falsification"),
            new RankedPrediction("Anti-matter gravity (GBAR precision)","GBAR/AEGIS free-fall","1-3 years (running)",0.85,0.80,0.68,"#2 — sharp falsification, underway"),
            new RankedPrediction("Neutrino-Koide: Q_nu = 2/3","DUNE/Hyper-K + cosmology","5-10 years",0.75,0.50,0.38,"#3 — clean but needs precise nu masses"),
            new RankedPrediction("Dark energy w(z) = -1 + 0.015(1+z)^(3/2)","Euclid/DESI","5-10 years",0.70,0.45,0.32,"#4 — slow, needs high statistics"),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CURRENT PREDICTION INVENTORY (QG-068)");
        sb.AppendLine();
        sb.AppendLine("  CONFIRMED (2):");
        sb.AppendLine("    - g† = c·H₀/(2π) (SPARC).");
        sb.AppendLine("    - Anti-matter falls down (ALPHA-g 2023).");
        sb.AppendLine();
        sb.AppendLine("  UNTESTED / PENDING (the 4 major ones):");
        sb.AppendLine("    - Evolving RAR: g†(z) = c·H(z)/(2π).");
        sb.AppendLine("    - Anti-matter gravity at higher precision (GBAR).");
        sb.AppendLine("    - Neutrino-Koide: Q_nu = 2/3.");
        sb.AppendLine("    - Dark energy: w(z) = -1 + 0.015(1+z)^(3/2).");
        sb.AppendLine();
        sb.AppendLine("  THE MISSION:");
        sb.AppendLine("    Rank these by FALSIFIABILITY × FEASIBILITY × RISK.");
        sb.AppendLine("    A prediction is valuable ONLY if it can genuinely fail.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The evolving RAR (g†(z)) is the SINGLE most important");
        sb.AppendLine("    experiment: fastest (archival data), highest falsification");
        sb.AppendLine("    power, and uniquely distinguishes AT from MOND and ΛCDM.");
        return sb.ToString();
    }

    static string BuildB(RankedPrediction[] ranking)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SCIENTIFIC-RISK ANALYSIS (FALSIFICATION POWER)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-38} {1,10} {2,10} {3}", "Prediction", "Falsif.", "Feasib.", "Rank"));
        sb.AppendLine("  " + new string('-', 75));
        foreach (var r in ranking)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-38} {1,10:F2} {2,10:F2} {3}", r.Prediction, r.FalsificationPower, r.Feasibility, r.Rank));
        }
        sb.AppendLine();
        sb.AppendLine("  WHAT FALSIFICATION POWER MEANS:");
        sb.AppendLine("    - 0.90 (g†(z)): if g† does NOT evolve as c·H(z)/2π, AT's");
        sb.AppendLine("      central RAR derivation (DATA-004) is WRONG. Devastating.");
        sb.AppendLine("    - 0.85 (anti-matter): if anti-H falls up, AT + GR + EP all");
        sb.AppendLine("      collapse. Decisive but currently consistent.");
        sb.AppendLine("    - 0.75 (neutrino-Koide): if Q_nu ≠ 2/3, Koide is charged-");
        sb.AppendLine("      lepton-specific (a refinement, not a falsification).");
        sb.AppendLine("    - 0.70 (w(z)): if η=0 exactly, the AT dark-energy signal");
        sb.AppendLine("      is absent (but 0.015 is below current sensitivity).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: g†(z) has the highest falsification power because");
        sb.AppendLine("  it uniquely tests a DERIVED number (g†), not a reinterpretation.");
        return sb.ToString();
    }

    static string BuildC(RankedPrediction[] ranking)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ACCESSIBILITY ANALYSIS (FEASIBILITY × TIMELINE)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-38} {1,-16} {2}", "Prediction", "Timeline", "Experiment"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var r in ranking)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-38} {1,-16} {2}", r.Prediction, r.Timeline, r.Experiment));
        }
        sb.AppendLine();
        sb.AppendLine("  THE ACCESSIBILITY SPECTRUM:");
        sb.AppendLine("    1. g†(z): ARCHIVAL data (KMOS3D). ~12 months. Nearly free.");
        sb.AppendLine("       This is the BEST accessibility — no new telescope needed.");
        sb.AppendLine("    2. Anti-matter: GBAR/AEGIS already RUNNING. 1-3 years.");
        sb.AppendLine("    3. Neutrino-Koide: needs precise neutrino masses (5-10 yr).");
        sb.AppendLine("    4. w(z): needs Euclid/DESI statistics (5-10 yr).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: g†(z) is the MOST ACCESSIBLE (archival, 12 months),");
        sb.AppendLine("  followed by anti-matter (running). The neutrino and dark");
        sb.AppendLine("  energy tests are years away.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DEPENDENCY ANALYSIS: WHAT BREAKS IF EACH FAILS?");
        sb.AppendLine();
        sb.AppendLine("  g†(z) FAILS (does NOT evolve as c·H(z)/2π):");
        sb.AppendLine("    - DATA-004 (g† = cH₀/2π) is WRONG. The RAR derivation");
        sb.AppendLine("      collapses. AT's STRONGEST prediction falsified.");
        sb.AppendLine("    - DAMAGE: CATASTROPHIC (core result destroyed).");
        sb.AppendLine();
        sb.AppendLine("  ANTI-MATTER FALLS UP:");
        sb.AppendLine("    - QG-035 (winding-sign blindness) is WRONG. T_μν ~ (∂θ)²");
        sb.AppendLine("      fails. GR + EP + AT all collapse.");
        sb.AppendLine("    - DAMAGE: CATASTROPHIC (but currently consistent).");
        sb.AppendLine();
        sb.AppendLine("  NEUTRINO-KOIDE FAILS (Q_nu ≠ 2/3):");
        sb.AppendLine("    - QG-046/050 (S3 all-leptons) is refined to charged-lepton-only.");
        sb.AppendLine("    - DAMAGE: MODERATE (a refinement, not a collapse).");
        sb.AppendLine();
        sb.AppendLine("  w(z) FAILS (η=0):");
        sb.AppendLine("    - QG-004 (Λ(t) from N(t)) is wrong. Dark energy not from AT.");
        sb.AppendLine("    - DAMAGE: MODERATE (QG-004 already partial, QG-004 was B).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: g†(z) and anti-matter have CATASTROPHIC damage on");
        sb.AppendLine("  failure (they test CORE derivations). Neutrino-Koide and w(z)");
        sb.AppendLine("  have MODERATE damage (refinements, not collapses).");
        return sb.ToString();
    }

    static string BuildE(RankedPrediction[] ranking)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PREDICTION RANKING (PRIORITY = FALSIFIABILITY × FEASIBILITY)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,4} {1,-40} {2,10} {3}", "#", "Prediction", "Priority", "Why"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var r in ranking)
        {
            string why = r.Rank.Length > 30 ? r.Rank[..27]+"..." : r.Rank;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,4} {1,-40} {2,10:F2} {3}", r.Rank.Split(' ')[0].Trim('#'), r.Prediction, r.PriorityScore, why));
        }
        sb.AppendLine();
        sb.AppendLine("  THE RANKING (final):");
        sb.AppendLine("    #1: Evolving RAR g†(z) — fastest + highest falsification.");
        sb.AppendLine("    #2: Anti-matter gravity (GBAR) — decisive, running.");
        sb.AppendLine("    #3: Neutrino-Koide — clean but slow.");
        sb.AppendLine("    #4: Dark energy w(z) — slow, low sensitivity.");
        sb.AppendLine();
        sb.AppendLine("  WHY g†(z) IS #1:");
        sb.AppendLine("    - Archival data (12 months, ~free).");
        sb.AppendLine("    - Tests a DERIVED number (g†), not a reinterpretation.");
        sb.AppendLine("    - Uniquely distinguishes AT from MOND (constant g†) and");
        sb.AppendLine("      ΛCDM (no RAR at all). It is the SHARPEST discriminator.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OPTIMAL TESTING ROADMAP");
        sb.AppendLine();
        sb.AppendLine("  PHASE 1 (0-12 months): Evolving RAR via KMOS3D archival data.");
        sb.AppendLine("    - Analyze existing high-z galaxy rotation curves.");
        sb.AppendLine("    - Test g†(z) = c·H(z)/(2π). Cost ~$0 (archival).");
        sb.AppendLine("    - OUTCOME: confirms/refutes AT's STRONGEST prediction.");
        sb.AppendLine();
        sb.AppendLine("  PHASE 2 (1-3 years): Anti-matter gravity at precision (GBAR).");
        sb.AppendLine("    - Measure g for anti-H to ~1%.");
        sb.AppendLine("    - OUTCOME: sharpens the already-confirmed 'falls down'.");
        sb.AppendLine();
        sb.AppendLine("  PHASE 3 (5-10 years): Neutrino-Koide (DUNE/Hyper-K).");
        sb.AppendLine("    - Precise neutrino mass hierarchy + absolute scale.");
        sb.AppendLine("    - OUTCOME: tests whether S3 extends to neutrinos.");
        sb.AppendLine();
        sb.AppendLine("  PHASE 4 (5-10 years): Dark energy w(z) (Euclid/DESI).");
        sb.AppendLine("    - High-precision w(z) measurement.");
        sb.AppendLine("    - OUTCOME: tests the AT Λ(t) form.");
        sb.AppendLine();
        sb.AppendLine("  THE OPTIMAL PATH:");
        sb.AppendLine("    Do g†(z) FIRST (fastest, cheapest, strongest). Then let");
        sb.AppendLine("    anti-matter and neutrino/dark-energy experiments mature.");
        sb.AppendLine("    The g†(z) test is the single highest-value experiment.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: ASSUME AT IS WRONG");
        sb.AppendLine();
        sb.AppendLine("  Which experiment reveals AT is wrong FASTEST?");
        sb.AppendLine();
        sb.AppendLine("  ANSWER: the evolving RAR (g†(z)).");
        sb.AppendLine("    - If AT's RAR derivation is wrong, g† does NOT evolve as");
        sb.AppendLine("      c·H(z)/2π. The archival data would show it immediately.");
        sb.AppendLine("    - 12 months, ~free, decisive. This is the fastest falsifier.");
        sb.AppendLine();
        sb.AppendLine("  WHY NOT THE OTHERS?");
        sb.AppendLine("    - Anti-matter: already consistent (falls down). GBAR only");
        sb.AppendLine("      sharpens; unlikely to reverse.");
        sb.AppendLine("    - Neutrino-Koide: slow (5-10 yr), and a 'no' only refines.");
        sb.AppendLine("    - w(z): slow, and 0.015 is below sensitivity (weak test).");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST WEAKNESS:");
        sb.AppendLine("    AT's core (Q, Randomness, triple) is UNFALSIFIABLE (they");
        sb.AppendLine("    are primitives). The FALSIFIABLE content is the DERIVED");
        sb.AppendLine("    predictions (g†, anti-matter, w(z), Koide). So AT is");
        sb.AppendLine("    falsifiable ONLY through its derived predictions, and");
        sb.AppendLine("    g†(z) is the sharpest of those.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: g†(z) is the fastest, strongest falsifier.");
        sb.AppendLine("  Fund that first. It is the single most important experiment.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. THE SHORTEST PATH TO VALIDATION/FALSIFICATION:");
        sb.AppendLine("    The evolving RAR (g†(z)) via archival KMOS3D data. 12");
        sb.AppendLine("    months, ~free, decisive. This is the #1 priority.");
        sb.AppendLine();
        sb.AppendLine("  2. AT'S FALSIFIABILITY IS REAL BUT CONCENTRATED:");
        sb.AppendLine("    The CORE (Q, Randomness, triple) is unfalsifiable (primitives).");
        sb.AppendLine("    The FALSIFIABLE content is the DERIVED predictions (g†, anti-");
        sb.AppendLine("    matter, w(z), Koide). So AT stands or falls on its derived");
        sb.AppendLine("    predictions, with g†(z) as the sharpest.");
        sb.AppendLine();
        sb.AppendLine("  3. THE DISCIPLINE OF PRIORITIZATION:");
        sb.AppendLine("    Ranking by falsifiability × feasibility (NOT elegance) is");
        sb.AppendLine("    the correct scientific discipline. g†(z) wins on BOTH.");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT THIS AUDIT ACHIEVES:");
        sb.AppendLine("    - Identifies the single most important experiment (g†(z)).");
        sb.AppendLine("    - Provides an optimal roadmap (4 phases).");
        sb.AppendLine("    - Honestly notes the core's unfalsifiability.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    AT is MODERATELY testable (B): its derived predictions are");
        sb.AppendLine("    testable, but its core is not. The g†(z) test is the");
        sb.AppendLine("    highest-value experiment — do it first.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  THE SINGLE MOST IMPORTANT EXPERIMENT: EVOLVING RAR (g†(z))");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: 4 predictions remain untested (g†(z), GBAR anti-matter,");
        sb.AppendLine("      neutrino-Koide, w(z)).");
        sb.AppendLine("  Q2: g†(z) has the HIGHEST falsification power (0.90) — it");
        sb.AppendLine("      tests a DERIVED number (g†), not a reinterpretation.");
        sb.AppendLine("  Q3: g†(z): <1 yr. Anti-matter: 1-3 yr. Neutrino/w(z): 5-10 yr.");
        sb.AppendLine("  Q4: AT depends on: g†(z) (CORE), anti-matter (CORE), neutrino");
        sb.AppendLine("      Koide (refinement), w(z) (QG-004, already partial).");
        sb.AppendLine("  Q5: Confirming g†(z) would STRONGLY validate AT (a derived");
        sb.AppendLine("      number, uniquely distinguishing from MOND and ΛCDM).");
        sb.AppendLine("  Q6: g†(z) failing would MOST STRONGLY damage AT (core RAR).");
        sb.AppendLine("  Q7: YES — ranked: g†(z) > anti-matter > neutrino > w(z).");
        sb.AppendLine("  Q8: Already collected by: KMOS3D (archival), GBAR/AEGIS (running),");
        sb.AppendLine("      Euclid/DESI (running), DUNE/Hyper-K (building).");
        sb.AppendLine("  Q9: Neutrino-Koide outcomes are mutually exclusive (charged-");
        sb.AppendLine("      lepton-only vs all-lepton S3).");
        sb.AppendLine("  Q10: Shortest path: analyze KMOS3D archival data for g†(z).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — MODERATELY TESTABLE");
        sb.AppendLine();
        sb.AppendLine("    AT is MODERATELY testable: its DERIVED predictions are");
        sb.AppendLine("    testable (4 pending), but its CORE (Q, Randomness, triple)");
        sb.AppendLine("    is unfalsifiable (primitives).");
        sb.AppendLine();
        sb.AppendLine("    THE SINGLE MOST IMPORTANT EXPERIMENT:");
        sb.AppendLine("    EVOLVING RAR g†(z) = c·H(z)/(2π) via KMOS3D archival data.");
        sb.AppendLine("    - Fastest (<1 year), cheapest (~free), strongest (tests a");
        sb.AppendLine("      derived number, uniquely distinguishes AT from MOND/ΛCDM).");
        sb.AppendLine("    - If funded: this ONE experiment provides the strongest and");
        sb.AppendLine("      fastest test of AT.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 69 experiments.");
        return sb.ToString();
    }
}
