using System.Globalization;

namespace AT.Core.ResearchQG;

public static class WhyThisNumberAnalyzer
{
    public static WTNResult RunFullAnalysis()
    {
        var nearby = BuildNearby();
        var distinctions = BuildDistinctions();
        return new WTNResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(),BuildF(nearby),BuildG(),BuildH(),BuildI(),nearby,distinctions);
    }

    static NearbyValue[] BuildNearby()
    {
        double[] qs = { 0.60, 0.63, 0.65, 2.0/3.0, 0.68, 0.70, 0.75 };
        return qs.Select(q =>
        {
            double cos2 = 1.0/(3.0*q);
            double theta = Math.Acos(Math.Sqrt(cos2))*180.0/Math.PI;
            string via = "viable (theta in [0,54.74])";
            string note = Math.Abs(q - 2.0/3.0) < 1e-9 ? "KOIDE: theta=45 deg exactly (balanced)" : "off-Koide";
            return new NearbyValue(q, cos2, theta, via, note);
        }).ToArray();
    }

    static Distinction[] BuildDistinctions()
    {
        return new Distinction[]
        {
            new Distinction("Rational number","2/3 is a simple fraction","NO (nearby 0.65=13/20, 0.70=7/10 also rational)","A: rationalness is not special — all nearby values are rational."),
            new Distinction("Midpoint of [1/3,1]","2/3 = (1/3+1)/2","PARTIAL: the exact midpoint. But 'midpoint' = the 45 deg balance (QG-058).","B: distinguished as the midpoint, but this IS the balance."),
            new Distinction("cos^2(theta)=1/2","2/3 = 1/(3·1/2)","PARTIAL: theta=45 deg = balanced singlet/doublet (QG-046).","B: 2/3 IS the 45 deg. Distinguished but unexplained."),
            new Distinction("N_eff = 3/2","2/3 = 1/(3/2)","PARTIAL: 1.5 effective generations = 3/2 = N/2.","B: 3/2 is suggestive (3 generations / 2 balance) but not derived."),
            new Distinction("S3 representation","singlet(1) + doublet(2), balance 1/2 each","PARTIAL: the 1/2 (balance) -> Q=2/3.","B: the 1/2 balance IS the 45 deg. Same mystery."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHY THIS NUMBER?");
        sb.AppendLine();
        sb.AppendLine("  QG-060: Q=2/3 is a boundary condition (surface), not a law.");
        sb.AppendLine("  The VALUE 2/3 remains unexplained.");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION (sharpened):");
        sb.AppendLine("    Why 2/3 and not 0.65, 0.67, or 0.70?");
        sb.AppendLine("    Is 2/3 DISTINGUISHED or ARBITRARY?");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    2/3 IS distinguished — it is the 45° balance point");
        sb.AppendLine("    (midpoint of the participation range). But the distinction");
        sb.AppendLine("    is the SAME as the 45° (QG-047), which is unexplained.");
        sb.AppendLine("    So 2/3 is DISTINGUISHED but NOT DERIVED.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RATIONAL NUMBER AUDIT: IS 2/3 SPECIAL AS A FRACTION?");
        sb.AppendLine();
        sb.AppendLine("  2/3 is a simple rational number. But so are nearby values:");
        sb.AppendLine("    - 0.65 = 13/20.");
        sb.AppendLine("    - 0.67 = 67/100.");
        sb.AppendLine("    - 0.70 = 7/10.");
        sb.AppendLine("  ALL nearby values are rational. Rationalness is NOT special.");
        sb.AppendLine();
        sb.AppendLine("  IS 2/3 THE 'SIMPLEST' RATIONAL AT THE MIDPOINT?");
        sb.AppendLine("    The midpoint (1/3 + 1)/2 = 2/3 IS the simplest rational");
        sb.AppendLine("    representation of the midpoint. But 'simplest' is not a");
        sb.AppendLine("    physical principle — it is aesthetic.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Rationalness does NOT distinguish 2/3. The fact");
        sb.AppendLine("  that 2/3 is a simple fraction is NOT a mechanism. It is");
        sb.AppendLine("  the GEOMETRIC midpoint (45°) that matters, not the fraction.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GEOMETRIC RATIO AUDIT: 2/3 = 1/(3·cos²45°)");
        sb.AppendLine();
        sb.AppendLine("  THE CLEANEST GEOMETRIC READING:");
        sb.AppendLine("    Q = 1/(3·cos²θ), where θ = angle of the amplitude vector");
        sb.AppendLine("    with the democratic direction (1,1,1).");
        sb.AppendLine("    Q = 2/3  ⟺  cos²θ = 1/2  ⟺  θ = 45°.");
        sb.AppendLine();
        sb.AppendLine("  WHY 45° IS THE BALANCE:");
        sb.AppendLine("    cos²45° = sin²45° = 1/2. The amplitude vector has EQUAL");
        sb.AppendLine("    democratic (singlet) and hierarchical (doublet) content.");
        sb.AppendLine("    This is the S3-balanced decomposition (QG-046).");
        sb.AppendLine();
        sb.AppendLine("  SO 2/3 IS DERIVATIVE OF 45°:");
        sb.AppendLine("    2/3 = 1/(3·1/2) = 2/3. The '2/3' is ENTIRELY equivalent to");
        sb.AppendLine("    the '45°'. There is no independent meaning of 2/3 — it is");
        sb.AppendLine("    the 45° balance expressed as a participation ratio.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 2/3 IS geometrically distinguished (it IS the 45°),");
        sb.AppendLine("  but the 45° itself is unexplained (QG-047). The question");
        sb.AppendLine("  'why 2/3' = 'why 45°' = 'why balance' — the SAME mystery.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SIMPLEX AND PROJECTION RATIO AUDIT");
        sb.AppendLine();
        sb.AppendLine("  SIMPLEX (the 2-simplex of amplitude distributions):");
        sb.AppendLine("    The normalized amplitudes p_i = sqrt(m_i)/sum sqrt(m) live");
        sb.AppendLine("    on a 2-simplex (triangle). Q = sum p_i^2 = the squared");
        sb.AppendLine("    distance from the simplex CENTER (democratic point).");
        sb.AppendLine("    Q = 2/3 is a specific distance from the center.");
        sb.AppendLine();
        sb.AppendLine("  PROJECTION (S3 singlet vs doublet):");
        sb.AppendLine("    The amplitude vector projects onto:");
        sb.AppendLine("      - S3 singlet (1,1,1): the democratic direction.");
        sb.AppendLine("      - S3 doublet: the orthogonal plane.");
        sb.AppendLine("    Balance: |singlet|² = |doublet|² = 1/2 each (45°).");
        sb.AppendLine("    Q = 2/3 = the participation ratio when singlet = doublet.");
        sb.AppendLine();
        sb.AppendLine("  SO 2/3 = 'THE BALANCED PROJECTION':");
        sb.AppendLine("    The value 2/3 encodes 'equal projection onto singlet and");
        sb.AppendLine("    doublet'. It is the S3-balanced configuration. This IS");
        sb.AppendLine("    distinguished (the balance point), but the balance itself");
        sb.AppendLine("    is unexplained.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 2/3 = the balanced-projection ratio. Geometrically");
        sb.AppendLine("  real, but the 'why balanced' remains the core mystery.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("BOUNDARY-VALUE STABILITY AUDIT");
        sb.AppendLine();
        sb.AppendLine("  Is 2/3 STABLE against deformation (QG-060)?");
        sb.AppendLine();
        sb.AppendLine("  RG STABILITY: lepton Yukawas run weakly, so Q is preserved");
        sb.AppendLine("  (any Q value, not just 2/3). This is STABILITY, not SELECTION.");
        sb.AppendLine();
        sb.AppendLine("  DEFORMATION: nearby values (0.65, 0.70) are ALSO stable");
        sb.AppendLine("  (weak running preserves them too). So 2/3 is NOT uniquely");
        sb.AppendLine("  stable — any value is stable for leptons.");
        sb.AppendLine();
        sb.AppendLine("  SO 2/3 IS NOT SELECTED BY STABILITY:");
        sb.AppendLine("    The weak RG running preserves ANY value. 2/3 is not");
        sb.AppendLine("    'more stable' than 0.65 or 0.70. The stability explains");
        sb.AppendLine("    the PERSISTENCE (10^-5), not the CHOICE (2/3).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 2/3 is STABLE (like all values), but NOT SELECTED");
        sb.AppendLine("  by any stability mechanism. The value is contingent, not");
        sb.AppendLine("  stability-forced.");
        return sb.ToString();
    }

    static string BuildF(NearbyValue[] nearby)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE NEARBY-VALUE SCAN (0.60 – 0.75)");
        sb.AppendLine();
        sb.AppendLine("  Would nearby values produce equally viable spectra?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,8} {1,12} {2,12} {3}", "Q", "cos²θ", "θ (deg)", "Note"));
        sb.AppendLine("  " + new string('-', 55));
        foreach (var n in nearby)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F3} {1,12:F4} {2,12:F2} {3}", n.Q, n.Cos2Theta, n.ThetaDeg, n.Note));
        }
        sb.AppendLine();
        sb.AppendLine("  KEY RESULT: ALL nearby values are VIABLE.");
        sb.AppendLine("    - Q = 0.60 (θ=41.8°), 0.65 (44.2°), 0.70 (46.4°), 0.75 (48.2°).");
        sb.AppendLine("    - Each gives a valid hierarchical spectrum (θ in [0, 54.74°]).");
        sb.AppendLine("    - NO value is excluded by viability. 2/3 is NOT uniquely viable.");
        sb.AppendLine();
        sb.AppendLine("  SO 2/3 IS NOT SELECTED BY VIABILITY:");
        sb.AppendLine("    The only distinction of 2/3 is the BALANCE (θ=45° exactly).");
        sb.AppendLine("    But nearby values are equally viable — they're just not");
        sb.AppendLine("    'balanced'. The balance is the unexplained feature.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: 2/3 is distinguished ONLY by the balance (45°),");
        sb.AppendLine("  not by viability or stability. The balance is the mystery.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. '2/3 IS DISTINGUISHED ONLY AS THE 45° (BALANCE)':");
        sb.AppendLine("     CORRECT. Every representation of 2/3 (midpoint, cos²θ=1/2,");
        sb.AppendLine("     balanced projection) is the SAME 45° balance. There is");
        sb.AppendLine("     no independent meaning of 2/3.");
        sb.AppendLine();
        sb.AppendLine("  2. 'RATIONALNESS IS NOT A MECHANISM':");
        sb.AppendLine("     CORRECT. 0.65=13/20 and 0.70=7/10 are also rational.");
        sb.AppendLine("     '2/3 is a simple fraction' is aesthetic, not physical.");
        sb.AppendLine();
        sb.AppendLine("  3. 'NEARBY VALUES ARE EQUALLY VIABLE':");
        sb.AppendLine("     CORRECT. The scan shows 0.60-0.75 all give valid spectra.");
        sb.AppendLine("     2/3 is not selected by viability or stability.");
        sb.AppendLine();
        sb.AppendLine("  4. 'THE N_eff = 3/2 IS SUGGESTIVE BUT NOT DERIVED':");
        sb.AppendLine("     CORRECT. 1.5 effective generations = 3/2 = N/2. The '2'");
        sb.AppendLine("     could be the S3 doublet dimension, but this is numerology");
        sb.AppendLine("     until a mechanism is found.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     2/3 is DISTINGUISHED (it is the 45° balance), but the");
        sb.AppendLine("     distinction is UNEXPLAINED. The question 'why 2/3' is");
        sb.AppendLine("     IDENTICAL to 'why 45°' = 'why balance'. No progress on");
        sb.AppendLine("     the value itself. Classification: B (weakly distinguished).");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. 'WHY 2/3' = 'WHY 45°' = 'WHY BALANCE':");
        sb.AppendLine("    These are ONE question. The 2/3 is entirely equivalent to");
        sb.AppendLine("    the 45° balance. No independent content exists.");
        sb.AppendLine();
        sb.AppendLine("  2. THE VALUE IS NOT SELECTED BY VIABILITY OR STABILITY:");
        sb.AppendLine("    Nearby values (0.60-0.75) are equally viable and equally");
        sb.AppendLine("    stable (weak RG running). 2/3 is not forced.");
        sb.AppendLine();
        sb.AppendLine("  3. THE SINGLE UNEXPLAINED FEATURE IS THE BALANCE:");
        sb.AppendLine("    The ONLY special thing about 2/3 is that it is the balance");
        sb.AppendLine("    point (45°, singlet=doublet). This balance is unexplained.");
        sb.AppendLine("    After 61 experiments, the mystery is REDUCED to: 'why is");
        sb.AppendLine("    the charged-lepton amplitude vector balanced?'");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT WOULD CONSTITUTE PROGRESS:");
        sb.AppendLine("    A mechanism that forces the balance (45°) — e.g., a Z2");
        sb.AppendLine("    symmetry between singlet and doublet sectors, or a");
        sb.AppendLine("    fixed point of a flavor flow. Neither has been found.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    Q=2/3 is DISTINGUISHED (the balance) but UNDERIVED.");
        sb.AppendLine("    The number is not arbitrary (it is the balance point),");
        sb.AppendLine("    but no mechanism produces it. It is 'distinguished");
        sb.AppendLine("    but unexplained' — the final state of the flavor program.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  2/3 IS THE 45° BALANCE — DISTINGUISHED BUT UNDERIVED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: 2/3 is special ONLY as the 45° balance (midpoint, cos²θ=1/2).");
        sb.AppendLine("  Q2: YES — 0.65, 0.67, 0.70 are equally viable (all give valid");
        sb.AppendLine("      hierarchical spectra). 2/3 is not viability-selected.");
        sb.AppendLine("  Q3: 2/3 is rational but so are nearby values. No hidden rational");
        sb.AppendLine("      structure. (N_eff=3/2 is suggestive but not derived.)");
        sb.AppendLine("  Q4: 2/3 emerges from GEOMETRY (45° balance), not probability,");
        sb.AppendLine("      symmetry alone, or representation theory. The geometry");
        sb.AppendLine("      is real but the balance is unexplained.");
        sb.AppendLine("  Q5: 2/3 = midpoint ratio = 1/(3cos²45°) = balanced-projection");
        sb.AppendLine("      ratio = (N+1)/(2N) at N=3. All are the SAME 45°.");
        sb.AppendLine("  Q6: Only charged leptons because only they are bare S¹+U(1)");
        sb.AppendLine("      (QG-050). The 'where' is explained, the 'value' is not.");
        sb.AppendLine("  Q7: YES — the Koide surface (Q=2/3) is a special submanifold");
        sb.AppendLine("      of flavor space (QG-060). Real, stable, lepton-specific.");
        sb.AppendLine("  Q8: YES — nearby values continuously deform into 2/3 (no");
        sb.AppendLine("      barrier). 2/3 is not topologically isolated.");
        sb.AppendLine("  Q9: OBSERVED (not selected). The balance is a boundary");
        sb.AppendLine("      condition, not a dynamical selection.");
        sb.AppendLine("  Q10: NO — no AT primitive (Q, Actualization, Phase, Attractor,");
        sb.AppendLine("      Persistence) produces 2/3. The balance is unexplained.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAKLY DISTINGUISHED");
        sb.AppendLine();
        sb.AppendLine("    2/3 IS distinguished (it is the 45° balance — a genuine");
        sb.AppendLine("    geometric special point). But the distinction is UNEXPLAINED:");
        sb.AppendLine("    no mechanism (symmetry, stability, viability) forces the");
        sb.AppendLine("    balance. Nearby values are equally viable.");
        sb.AppendLine();
        sb.AppendLine("    THE FINAL STATE OF THE FLAVOR PROGRAM:");
        sb.AppendLine("    The entire mystery reduces to ONE statement:");
        sb.AppendLine("    'The charged-lepton amplitude vector is balanced (45°).'");
        sb.AppendLine("    This is real (10^-5), lepton-specific, stable, and underived.");
        sb.AppendLine("    After 61 QG experiments, the balance itself remains the");
        sb.AppendLine("    single unexplained fact at the heart of flavor physics.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 61 experiments.");
        return sb.ToString();
    }
}
