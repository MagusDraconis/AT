using System.Globalization;

namespace AT.Core.ResearchQG;

public static class GenerationSpaceGeometryAnalyzer
{
    public static GGRResult RunFullAnalysis()
    {
        var geoms = BuildGeometries();
        return new GGRResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(geoms),BuildH(),BuildI(),geoms);
    }

    static GGeometry[] BuildGeometries()
    {
        return new GGeometry[]
        {
            new GGeometry("Euclidean R^3","3 (real)","SO(3) (orthogonal)","3 angles, NO complex phase","45° = angle with (1,1,1)","NO (real space has no phase)","B: explains Koide angle, FAILS CP violation."),
            new GGeometry("Complex C^3","3 (complex)","U(3) (unitary)","3 angles + 1 CP phase","45° = angle in real eigenvalue subspace","YES (unitary has the phase)","C: STRONG. The CP phase REQUIRES complex. This is the natural G."),
            new GGeometry("2-simplex (triangle)","2 (real, normalized)","S3 (permutation of vertices)","Mixing = relabeling, not rotation","Q = 2/3 = participation ratio (sum p_i^2)","NO (simplex is real)","B: elegant (S3 = triangle symmetry) but real, no CP phase."),
            new GGeometry("Sphere S^2","2 (real, direction)","O(3)","Rotation of amplitude direction","45° = latitude circle","NO (real)","B: the amplitude direction lives on S^2, but no CP phase."),
            new GGeometry("Information (Fisher)","3 (simplex)","Statistical manifold","Mixing as geodesic? (speculative)","Q = sum p_i^2 = participation ratio","NO (probabilities are real)","B: Koide = participation ratio is elegant, but no CP phase."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHY THE GEOMETRY OF G MATTERS");
        sb.AppendLine();
        sb.AppendLine("  QG-052/053/054 established: G is a real, unavoidable, 3D");
        sb.AppendLine("  internal space. But WHAT is its geometry?");
        sb.AppendLine();
        sb.AppendLine("  THE GEOMETRY MUST EXPLAIN (simultaneously):");
        sb.AppendLine("    1. Mixing (CKM/PMNS) = rotations in G.");
        sb.AppendLine("    2. CP violation = a complex phase in the mixing.");
        sb.AppendLine("    3. S3 = permutation symmetry of the 3 generations.");
        sb.AppendLine("    4. Koide 45° = a geometric direction in G.");
        sb.AppendLine("    5. Masses = eigenvalues (distances in G).");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The CP-VIOLATING PHASE is the key discriminator.");
        sb.AppendLine("    A real space (R³) has SO(3) rotations with NO phase.");
        sb.AppendLine("    A complex space (C³) has U(3) rotations WITH a phase.");
        sb.AppendLine("    Since CP violation is OBSERVED, G must be COMPLEX (C³).");
        sb.AppendLine("    This is a clean, decisive geometric result.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("EUCLIDEAN R³ ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Model G as real R³ (3D Euclidean space).");
        sb.AppendLine("    - Rotations: SO(3), parameterized by 3 angles.");
        sb.AppendLine("    - No complex phase (real matrices have real entries).");
        sb.AppendLine();
        sb.AppendLine("  THE CP-VIOLATION PROBLEM:");
        sb.AppendLine("    CKM is 3x3 UNITARY: 3 angles + 1 CP phase (6 physical");
        sb.AppendLine("    params after phase redefinitions).");
        sb.AppendLine("    A REAL orthogonal 3x3 (SO(3)) has only 3 angles, NO phase.");
        sb.AppendLine("    So R³ with SO(3) CANNOT produce the CP-violating phase.");
        sb.AppendLine("    The CP phase is the EXPERIMENTAL smoking gun that G is");
        sb.AppendLine("    NOT a real space.");
        sb.AppendLine();
        sb.AppendLine("  WHAT R³ DOES EXPLAIN (the Koide 45°):");
        sb.AppendLine("    The mass EIGENVALUES are real, so the amplitude vector");
        sb.AppendLine("    (sqrt(m_e), sqrt(m_mu), sqrt(m_tau)) lives in R³_+ (positive");
        sb.AppendLine("    octant). The 45° angle with (1,1,1) is a REAL-geometric fact.");
        sb.AppendLine("    So the MASSES live in R³_+, even though the full G is complex.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: R³ explains the mass geometry (Koide) but FAILS");
        sb.AppendLine("  the CP phase. G cannot be purely real.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SIMPLEX ANALYSIS: GENERATIONS AS TRIANGLE VERTICES");
        sb.AppendLine();
        sb.AppendLine("  Model the 3 generations as vertices of a 2-simplex (triangle).");
        sb.AppendLine("    - S3 = the symmetry of the triangle (permutation of vertices).");
        sb.AppendLine("    - The democratic direction (1,1,1) = the triangle CENTER.");
        sb.AppendLine("    - The Koide 45° = a direction relative to the center.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS ELEGANT:");
        sb.AppendLine("    S3 is LITERALLY the symmetry of a triangle. The 3 generations");
        sb.AppendLine("    as 3 vertices give S3 automatically, with a natural geometric");
        sb.AppendLine("    picture. The 'balanced' Koide (singlet = doublet) is the");
        sb.AppendLine("    midpoint between center (democratic) and vertex (hierarchical).");
        sb.AppendLine();
        sb.AppendLine("  THE PARTICIPATION-RATIO RESULT (information content):");
        sb.AppendLine("    Normalize the amplitudes: p_i = sqrt(m_i) / (sum sqrt(m)).");
        sb.AppendLine("    Then Q = sum p_i^2 = the PARTICIPATION RATIO (Simpson index).");
        sb.AppendLine("    Koide Q = 2/3 means the generation probability distribution");
        sb.AppendLine("    (p_e, p_mu, p_tau) = (0.013, 0.193, 0.793) has participation");
        sb.AppendLine("    ratio exactly 2/3. The 'effective number of generations'");
        sb.AppendLine("    (1/Q) = 3/2 = 1.5 (the tau dominates).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The simplex picture is ELEGANT (S3 = triangle symmetry,");
        sb.AppendLine("  Koide = participation ratio 2/3). But it is REAL, so it CANNOT");
        sb.AppendLine("  produce the CP phase. It describes the MASS geometry, not the");
        sb.AppendLine("  full complex G.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SPHERICAL S² ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Model the amplitude DIRECTION on the sphere S² (unit sphere).");
        sb.AppendLine("    - The amplitude vector A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau))");
        sb.AppendLine("      normalizes to a point on S².");
        sb.AppendLine("    - Koide 45° = a 'latitude circle' on S² (QG-047: x+y+z = sqrt(3/2)).");
        sb.AppendLine();
        sb.AppendLine("  WHY S² IS NATURAL FOR KOIDE:");
        sb.AppendLine("    Q = (sum m)/(sum sqrt m)^2 depends ONLY on the DIRECTION of");
        sb.AppendLine("    the amplitude vector, not its magnitude. So the Koide");
        sb.AppendLine("    constraint is a condition on the DIRECTION — a curve on S².");
        sb.AppendLine("    The 45° is a specific latitude on S².");
        sb.AppendLine();
        sb.AppendLine("  THE LIMITATION:");
        sb.AppendLine("    S² is REAL (2D), so it CANNOT host the CP phase. It describes");
        sb.AppendLine("    the mass direction, not the full mixing (which needs C³).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S² is the natural space for the Koide GEOMETRY (the");
        sb.AppendLine("  amplitude direction), but it is a SUBSpace of the full G (C³).");
        sb.AppendLine("  The masses live on S² (direction); the mixing lives in C³.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MIXING INTERPRETATION: U(3) ROTATIONS");
        sb.AppendLine();
        sb.AppendLine("  CKM and PMNS are UNITARY 3x3 matrices (U(3)).");
        sb.AppendLine();
        sb.AppendLine("  THE PARAMETER COUNT (the geometric content):");
        sb.AppendLine("    - A general U(3) matrix: 9 real parameters (3 angles + 6 phases).");
        sb.AppendLine("    - 5 phases absorbed by fermion field redefinitions.");
        sb.AppendLine("    - Physical content: 3 MIXING ANGLES + 1 CP PHASE.");
        sb.AppendLine("    - SO(3) (real rotations): 3 angles, NO phase.");
        sb.AppendLine("    - U(3) (unitary): 3 angles + 1 phase (the CP violation).");
        sb.AppendLine();
        sb.AppendLine("  THE GEOMETRIC CONCLUSION:");
        sb.AppendLine("    Mixing = U(3) rotations in a COMPLEX space (C³).");
        sb.AppendLine("    The CP phase is the 'complex' part of the rotation —");
        sb.AppendLine("    impossible in a real space, natural in a complex one.");
        sb.AppendLine();
        sb.AppendLine("  SO THE FULL G = C³ (COMPLEX), with:");
        sb.AppendLine("    - Mass eigenvalues: REAL (live in R³_+).");
        sb.AppendLine("    - Mixing: COMPLEX (U(3), lives in C³).");
        sb.AppendLine("    - CP phase: the complex angle of the mixing rotation.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G = C³. The masses are the real part; the mixing");
        sb.AppendLine("  (with CP) is the complex part. This is the SM's flavor space,");
        sb.AppendLine("  now identified as the AT generation space.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE INTERPRETATION: 45° AND PARTICIPATION RATIO");
        sb.AppendLine();
        sb.AppendLine("  Koide has TWO geometric readings:");
        sb.AppendLine();
        sb.AppendLine("  1. ANGLE (45°):");
        sb.AppendLine("     The amplitude vector A = (sqrt(m_e), sqrt(m_mu), sqrt(m_tau))");
        sb.AppendLine("     sits at 45° to the democratic direction (1,1,1).");
        sb.AppendLine("     Equivalent to: balanced S3 singlet/doublet (QG-046).");
        sb.AppendLine();
        sb.AppendLine("  2. PARTICIPATION RATIO (2/3):");
        sb.AppendLine("     Normalize: p_i = sqrt(m_i)/(sum sqrt m). Then");
        sb.AppendLine("     Q = sum p_i^2 = (sum m)/(sum sqrt m)^2 = 2/3.");
        sb.AppendLine("     Q = 2/3 means the generation amplitude distribution has");
        sb.AppendLine("     participation ratio 2/3 (effective generations = 3/2).");
        sb.AppendLine();
        sb.AppendLine("  NUMERICAL CHECK:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    p_e = {0:F4}, p_mu = {1:F4}, p_tau = {2:F4}",
            0.7148/53.147, 10.279/53.147, 42.153/53.147));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    sum p_i^2 = {0:F4} (Koide: 2/3 = 0.6667)",
            0.01345*0.01345 + 0.1934*0.1934 + 0.7932*0.7932));
        sb.AppendLine();
        sb.AppendLine("  THE TWO READINGS ARE EQUIVALENT:");
        sb.AppendLine("    Q = 2/3 ⟺ theta = 45° ⟺ participation ratio 2/3.");
        sb.AppendLine("    All are the SAME geometric fact about the amplitude vector.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is a GEOMETRIC constraint on the real");
        sb.AppendLine("  amplitude direction (in R³_+ / S² / simplex). It does NOT");
        sb.AppendLine("  involve the complex phase (CP) — it is purely real-geometric.");
        return sb.ToString();
    }

    static string BuildG(GGeometry[] geoms)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GEOMETRY COMPARISON MATRIX");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-22} {1,-10} {2,-16} {3,-22} {4}", "Geometry", "Dim", "Symmetry", "CP phase?", "Score"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var g in geoms)
        {
            string cp = g.CPPhase == "YES (unitary has the phase)" ? "YES" : "NO";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-22} {1,-10} {2,-16} {3,-22} {4}", g.Candidate, g.Dimension, g.Symmetry, cp, g.Score));
        }
        sb.AppendLine();
        sb.AppendLine("  THE RANKING (by explanatory power):");
        sb.AppendLine("    1. COMPLEX C³ (U(3)): explains mixing + CP + Koide. STRONGEST.");
        sb.AppendLine("    2. 2-simplex (S3): elegant S3 + Koide participation ratio.");
        sb.AppendLine("       But real (no CP). Describes the mass geometry.");
        sb.AppendLine("    3. Sphere S²: the mass direction. Real (no CP).");
        sb.AppendLine("    4. Euclidean R³ (SO(3)): explains Koide angle only.");
        sb.AppendLine("    5. Information (Fisher): Koide = participation ratio, real.");
        sb.AppendLine();
        sb.AppendLine("  THE SYNTHESIS:");
        sb.AppendLine("    G is C³ (complex, for mixing+CP). The masses (real) live in");
        sb.AppendLine("    R³_+ ⊂ C³, and their direction on S² / the simplex carries");
        sb.AppendLine("    the Koide geometry. So: G = C³, with a real mass subsector");
        sb.AppendLine("    (R³_+/S²/simplex) where Koide lives.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE CP-PHASE ARGUMENT IS DECISIVE AND CORRECT':");
        sb.AppendLine("     AGREED. SO(3) (real) has no phase; U(3) (complex) has one.");
        sb.AppendLine("     CP violation is OBSERVED. Therefore G is complex (C³).");
        sb.AppendLine("     This is a clean, rigorous geometric result.");
        sb.AppendLine();
        sb.AppendLine("  2. 'BUT THE MASS GEOMETRY IS REAL, NOT COMPLEX':");
        sb.AppendLine("     CORRECT. The mass EIGENVALUES are real (eigenvalues of");
        sb.AppendLine("     Y†Y, a Hermitian matrix). So the mass subsector is R³_+.");
        sb.AppendLine("     G is C³ for mixing, but the mass direction is real.");
        sb.AppendLine("     The full picture: C³ ⊃ R³_+ (masses) and U(3) ⊃ SO(3) (real part).");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE SIMPLEX/INFORMATION INTERPRETATION IS ELEGANT BUT REAL':");
        sb.AppendLine("     AGREED. Koide = participation ratio 2/3 is a NICE result,");
        sb.AppendLine("     but it lives in the REAL mass subsector. It does not");
        sb.AppendLine("     explain CP. It is COMPLEMENTARY, not competing, with C³.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ESTABLISHED':");
        sb.AppendLine("     - G is COMPLEX (C³): the CP phase proves it.");
        sb.AppendLine("     - Masses are REAL eigenvalues (R³_+ / S² / simplex).");
        sb.AppendLine("     - Koide = participation ratio 2/3 (real geometry).");
        sb.AppendLine("     - Mixing = U(3) rotations (complex geometry).");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     G has GENUINE geometry (mixing, CP, Koide are geometric).");
        sb.AppendLine("     But no UNIQUE geometry is forced — C³ (complex) is needed");
        sb.AppendLine("     for CP, while R³/S²/simplex describe the mass direction.");
        sb.AppendLine("     Classification: C (strong geometric structure), not D");
        sb.AppendLine("     (unique geometry).");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  G IS A COMPLEX SPACE C³ (WITH A REAL MASS SUBSECTOR)");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: G is best described as a COMPLEX 3D space (C³) with U(3)");
        sb.AppendLine("      symmetry — the CP phase forces this.");
        sb.AppendLine("  Q2: YES — Koide = a geometric constraint (45°) in the REAL");
        sb.AppendLine("      mass subsector R³_+ of G.");
        sb.AppendLine("  Q3: YES — 45° is intrinsic to the real mass direction (the");
        sb.AppendLine("      balanced S3 decomposition), a specific latitude on S².");
        sb.AppendLine("  Q4: Generations = the 3 eigenvectors of the Yukawa matrix in G");
        sb.AppendLine("      (basis vectors of the mass subsector).");
        sb.AppendLine("  Q5: YES — CKM/PMNS = U(3) rotations between different sectors'");
        sb.AppendLine("      mass bases in G.");
        sb.AppendLine("  Q6: G has a metric (Hermitian inner product), connection (gauge),");
        sb.AppendLine("      and geodesics (mixing trajectories). It is a complex manifold.");
        sb.AppendLine("  Q7: Masses = real eigenvalues (distances along the mass axes);");
        sb.AppendLine("      Koide = the direction of the eigenvalue vector.");
        sb.AppendLine("  Q8: S3 emerges from the real subsector (permutation of the 3");
        sb.AppendLine("      mass axes), automatic once dim=3.");
        sb.AppendLine("  Q9: Complex C³ explains mixing+CP+Koide better than any real");
        sb.AppendLine("      geometry (which cannot host the CP phase).");
        sb.AppendLine("  Q10: No UNIQUE geometry is forced — C³ is REQUIRED (CP phase),");
        sb.AppendLine("      but R³/S²/simplex are valid descriptions of the mass subsector.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C — STRONG GEOMETRIC STRUCTURE");
        sb.AppendLine();
        sb.AppendLine("    G has GENUINE, OBSERVABLE geometry:");
        sb.AppendLine("      - Mixing angles (CKM/PMNS) = rotation angles.");
        sb.AppendLine("      - CP phase = the complex phase of the U(3) rotation.");
        sb.AppendLine("      - Koide 45° = the real amplitude direction.");
        sb.AppendLine("      - Participation ratio 2/3 = the mass distribution shape.");
        sb.AppendLine();
        sb.AppendLine("    THE KEY RESULT: G is COMPLEX (C³), forced by CP violation.");
        sb.AppendLine("    The masses live in the real subsector (R³_+), where Koide");
        sb.AppendLine("    and S3 are geometric. This is the SM's flavor space, now");
        sb.AppendLine("    with a precise AT geometric characterization.");
        sb.AppendLine();
        sb.AppendLine("    NOT D (unique geometry): multiple models (C³, simplex, S²)");
        sb.AppendLine("    all fit, describing different ASPECTS (mixing vs mass).");
        sb.AppendLine("    The geometry is real but layered: complex (mixing) over");
        sb.AppendLine("    real (mass).");
        sb.AppendLine();
        sb.AppendLine("  QG program: 55 experiments.");
        return sb.ToString();
    }
}
