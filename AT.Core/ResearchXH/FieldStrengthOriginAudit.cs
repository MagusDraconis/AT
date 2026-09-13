using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_007 - FIELD STRENGTH ORIGIN AUDIT.
///
/// QUESTION. Can the D96^3 edge connection that E_006 derived produce a NON-ZERO field strength? Construct closed
/// plaquette loops, a discrete curvature and a discrete curl; measure F != 0; compare a pure gauge configuration
/// against a non-trivial loop; and detect the FIRST non-trivial field quantity.
///
/// ANSWER: **BOUNDARY - and the two halves of the answer are exact.**
///
///  (1) THE CONNECTION ALONE PRODUCES ZERO. E_006's edge connection is a product of lattice DIFFERENCES, and
///      differences commute on a uniform lattice: the commutator [D_mu, D_nu] vanishes exactly (measured to
///      roundoff), and the same statement in link language is that a gradient link field A_mu = D_mu lambda has an
///      exactly vanishing plaquette holonomy, because the four contributions telescope. So the edge connection is
///      FLAT. It does not produce a field strength by itself.
///
///  (2) BUT THE CUBE IS THE FIRST SUBSTRATE THAT CAN CARRY ONE AT ALL. A field strength needs a CLOSED TWO-
///      dimensional loop. The ring has ZERO elementary plaquettes (it is one-dimensional, whatever its link count),
///      while the cube has one plaquette per site and per orientation pair: 3L^3 = 2654208 at L = 96, inside an
///      independent cycle space of E - V + 1 = 1769473. THE FIRST NON-TRIVIAL FIELD QUANTITY THEREFORE EXISTS ONLY
///      ON THE CUBE. (This audit also CORRECTS E_005, which reported 3L^2 = 27648 elementary plaquettes while its
///      own cycle count already used the correct 3L^3 edges - the two numbers differed by a factor of L.)
///
///  (3) THE FIRST NON-TRIVIAL FIELD QUANTITY IS THE QUANTISED PLAQUETTE FLUX. A linear link field A_1 = f y gives
///      F = -f, and the requirement that the link be periodic on the torus forces f L to be a whole turn, so
///      f = 2 pi n / L and the SMALLEST NON-ZERO FIELD STRENGTH THE SUBSTRATE CAN CARRY IS 2 pi / 96 = 0.065449847.
///      That is exactly the phase quantum AT already has - the same number that E_005 found to be pure gauge on the
///      ring is, on the cube, the minimal real field strength.
///
///  (4) THE APPARATUS IS DERIVED AND EXACT. For an Abelian connection the discrete curvature IS the antisymmetrised
///      difference - not to leading order, exactly - so the curl and the holonomy agree to roundoff; the curvature
///      is gauge invariant (measured under a site-dependent gauge transformation); and the discrete Bianchi
///      identity holds exactly, because the six faces of an elementary cube telescope.
///
///  (5) SO THE VERDICT IS BOUNDARY. The detector, the curvature, the curl, the quantisation, gauge invariance and
///      Bianchi are all computed here, and the first non-trivial quantity is DETECTED - but the connection does not
///      produce it: it takes a chosen fluctuation. What is derived is the field-strength sector; what is still
///      missing is the dynamics that would select the fluctuation (E_005, E_006).
/// </summary>
public static class FieldStrengthOriginAudit
{
    public const int L = 96;

    // ===================== 1. WHAT THE CONNECTION ITSELF DOES =====================

    /// <summary>Modular reduction onto a torus of side <paramref name="l"/>.</summary>
    private static int Mod(int v, int l) => ((v % l) + l) % l;

    /// <summary>A deterministic scalar on the torus, used as the gauge function and as the gradient's potential.</summary>
    public static double Lambda(int x, int y, int z, int l)
        => 0.31 * Math.Sin(2.0 * Math.PI * x / l)
         + 0.17 * Math.Cos(2.0 * Math.PI * y / l)
         + 0.11 * Math.Sin(2.0 * Math.PI * z / l)
         + 0.07 * Math.Sin(2.0 * Math.PI * (x + y) / l);

    /// <summary>Forward difference of a scalar along one direction, on the torus.</summary>
    public static double Difference(Func<int, int, int, double> f, int mu, int x, int y, int z, int l)
        => mu switch
        {
            1 => f(Mod(x + 1, l), y, z) - f(x, y, z),
            2 => f(x, Mod(y + 1, l), z) - f(x, y, z),
            _ => f(x, y, Mod(z + 1, l)) - f(x, y, z),
        };

    /// <summary>A link field on the torus: A_mu(x, y, z).</summary>
    public delegate double LinkField(int x, int y, int z, int mu);

    /// <summary>Second difference D_mu D_nu of a scalar on the torus.</summary>
    public static double SecondDifference(Func<int, int, int, double> f, int mu, int nu, int x, int y, int z, int l)
    {
        var q = new[] { x, y, z };
        q[mu - 1] = Mod(q[mu - 1] + 1, l);
        return Difference(f, nu, q[0], q[1], q[2], l) - Difference(f, nu, x, y, z, l);
    }

    /// <summary>
    /// The commutator of two differences acting on a scalar - the curvature of the edge connection read as a
    /// derivative. Measured exhaustively on a small torus and on samples of the full one.
    /// </summary>
    public static double CommutatorResidual(int l, int stride = 1)
    {
        Func<int, int, int, double> f = (x, y, z) => Lambda(x, y, z, l);
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                    for (int mu = 1; mu <= 3; mu++)
                        for (int nu = mu + 1; nu <= 3; nu++)
                        {
                            double a = SecondDifference(f, mu, nu, x, y, z, l);
                            double b = SecondDifference(f, nu, mu, x, y, z, l);
                            worst = Math.Max(worst, Math.Abs(a - b));
                        }
        return worst;
    }

    public static bool DifferencesCommute() => CommutatorResidual(8) < 1e-12 && CommutatorResidual(L, 13) < 1e-12;

    /// <summary>The gradient link field A_mu = D_mu lambda - the connection's own content, in link language.</summary>
    public static LinkField PureGaugeField(int l)
        => (x, y, z, mu) => Difference((a, b, c) => Lambda(a, b, c, l), mu, x, y, z, l);

    // ===================== 2. CLOSED PLAQUETTE LOOPS, CURVATURE AND CURL =====================

    /// <summary>
    /// The closed plaquette loop's holonomy phase - the discrete CURVATURE. For an Abelian connection the four
    /// phases simply add, which is why the curvature below is exact rather than a leading-order approximation.
    /// </summary>
    public static double PlaquettePhase(LinkField a, int mu, int nu, int x, int y, int z, int l)
    {
        var afterMu = new[] { x, y, z }; afterMu[mu - 1] = Mod(afterMu[mu - 1] + 1, l);
        var afterNu = new[] { x, y, z }; afterNu[nu - 1] = Mod(afterNu[nu - 1] + 1, l);
        return a(x, y, z, mu) + a(afterMu[0], afterMu[1], afterMu[2], nu)
             - a(afterNu[0], afterNu[1], afterNu[2], mu) - a(x, y, z, nu);
    }

    /// <summary>The discrete CURL: the antisymmetrised difference of the link field.</summary>
    public static double DiscreteCurl(LinkField a, int mu, int nu, int x, int y, int z, int l)
    {
        var afterNu = new[] { x, y, z }; afterNu[nu - 1] = Mod(afterNu[nu - 1] + 1, l);
        var afterMu = new[] { x, y, z }; afterMu[mu - 1] = Mod(afterMu[mu - 1] + 1, l);
        double dMuANu = a(afterMu[0], afterMu[1], afterMu[2], nu) - a(x, y, z, nu);
        double dNuAMu = a(afterNu[0], afterNu[1], afterNu[2], mu) - a(x, y, z, mu);
        return dMuANu - dNuAMu;
    }

    public static double CurlVersusHolonomyResidual(LinkField a, int l, int stride = 1)
    {
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                    foreach (var (mu, nu) in Orientations())
                        worst = Math.Max(worst, Math.Abs(
                            PlaquettePhase(a, mu, nu, x, y, z, l) - DiscreteCurl(a, mu, nu, x, y, z, l)));
        return worst;
    }

    public static (int Mu, int Nu)[] Orientations() => new[] { (1, 2), (1, 3), (2, 3) };

    public static double MaxFieldStrength(LinkField a, int l, int stride = 1)
    {
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                    foreach (var (mu, nu) in Orientations())
                        worst = Math.Max(worst, Math.Abs(PlaquettePhase(a, mu, nu, x, y, z, l)));
        return worst;
    }

    // ===================== 3. PURE GAUGE VERSUS A NON-TRIVIAL LOOP =====================

    public static double PureGaugeFieldStrength() => MaxFieldStrength(PureGaugeField(8), 8);

    /// <summary>A linear link field: F_12 = -f everywhere, and nothing else.</summary>
    public static LinkField FluxField(int n, int l)
        => (x, y, z, mu) => mu == 1 ? 2.0 * Math.PI * n * y / l : 0.0;

    /// <summary>The flux a single plaquette carries.</summary>
    public static double FluxPerPlaquette(int n, int l) => 2.0 * Math.PI * n / l;

    public static double MinimalNonZeroFlux() => 2.0 * Math.PI / L;

    /// <summary>The link field must be periodic on the torus: exp(i A(L)) = exp(i A(0)).</summary>
    public static double PeriodicityResidual(int n, int l)
    {
        var a = FluxField(n, l);
        double theta0 = a(0, 0, 0, 1), thetaL = a(0, l, 0, 1);
        var e0 = System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, theta0));
        var eL = System.Numerics.Complex.Exp(new System.Numerics.Complex(0.0, thetaL));
        return System.Numerics.Complex.Abs(eL - e0);
    }

    /// <summary>Gauge invariance: A -> A + D chi must leave every plaquette phase unchanged.</summary>
    public static double GaugeInvarianceResidual()
    {
        var a = FluxField(1, 8);
        LinkField transformed = (x, y, z, mu) => a(x, y, z, mu) + Difference((p, q, r) => Lambda(p, q, r, 8), mu, x, y, z, 8);
        double worst = 0.0;
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                for (int z = 0; z < 8; z++)
                    foreach (var (mu, nu) in Orientations())
                        worst = Math.Max(worst, Math.Abs(
                            PlaquettePhase(a, mu, nu, x, y, z, 8) - PlaquettePhase(transformed, mu, nu, x, y, z, 8)));
        return worst;
    }

    /// <summary>
    /// The discrete BIANCHI identity: the oriented sum over the six faces of an elementary cube is zero, because the
    /// twelve link contributions cancel in pairs. It holds for every link field, so it tests the apparatus, not the
    /// configuration.
    /// </summary>
    public static double BianchiResidual(LinkField a, int l, int stride = 1)
    {
        double worst = 0.0;
        for (int x = 0; x < l; x += stride)
            for (int y = 0; y < l; y += stride)
                for (int z = 0; z < l; z += stride)
                {
                    double sum = PlaquettePhase(a, 1, 2, x, y, z, l)
                               - PlaquettePhase(a, 1, 2, x, y, Mod(z + 1, l), l)
                               - PlaquettePhase(a, 1, 3, x, y, z, l)
                               + PlaquettePhase(a, 1, 3, x, Mod(y + 1, l), z, l)
                               + PlaquettePhase(a, 2, 3, x, y, z, l)
                               - PlaquettePhase(a, 2, 3, Mod(x + 1, l), y, z, l);
                    worst = Math.Max(worst, Math.Abs(sum));
                }
        return worst;
    }

    public static double BianchiResidualOnFlux() => BianchiResidual(FluxField(1, 8), 8);
    public static double BianchiResidualOnPureGauge() => BianchiResidual(PureGaugeField(8), 8);

    // ===================== 4. THE CYCLE STRUCTURE =====================

    public static long Sites(int l) => (long)l * l * l;
    public static long Edges(int l) => 3L * Sites(l);

    /// <summary>One elementary plaquette per site and per orientation pair.</summary>
    public static long ElementaryPlaquettes(int l) => 3L * Sites(l);

    public static long IndependentLoops(int l) => Edges(l) - Sites(l) + 1;

    /// <summary>A one-dimensional substrate has no 2-cycles, so a field strength has nowhere to live.</summary>
    public static long RingElementaryPlaquettes() => 0;

    public static bool TheCubeIsTheFirstSubstrateWithPlaquettes()
        => RingElementaryPlaquettes() == 0 && ElementaryPlaquettes(L) == 2654208L;

    // ===================== 5. MEASUREMENTS AND VERDICT =====================

    public static (string Quantity, string Value, string Status)[] Measurements() => new[]
    {
        ("commutator of the edge connection's differences", $"{CommutatorResidual(L, 13):E2}", "ZERO - the connection is flat"),
        ("field strength of a pure gauge link field", $"{PureGaugeFieldStrength():E2}", "ZERO exactly (telescoping)"),
        ("curl versus holonomy (Abelian)", $"{CurlVersusHolonomyResidual(FluxField(1, 8), 8):E2}", "EQUAL - the curvature IS the antisymmetrised difference"),
        ("field strength of the minimal non-trivial loop", $"{FluxPerPlaquette(1, L):F9}", "NON-ZERO - the first non-trivial quantity"),
        ("gauge invariance of the curvature", $"{GaugeInvarianceResidual():E2}", "INVARIANT"),
        ("discrete Bianchi identity (flux field)", $"{BianchiResidualOnFlux():E2}", "EXACT"),
        ("discrete Bianchi identity (pure gauge)", $"{BianchiResidualOnPureGauge():E2}", "EXACT"),
        ("link periodicity for the minimal flux", $"{PeriodicityResidual(1, L):E2}", "PERIODIC - hence the quantisation"),
    };

    public static string FirstNonTrivialQuantity()
        => $"the quantised plaquette flux F = 2 pi n / L, smallest non-zero value {MinimalNonZeroFlux():F9}";

    public static string Verdict()
    {
        bool flat = DifferencesCommute() && PureGaugeFieldStrength() < 1e-12;
        bool apparatus = CurlVersusHolonomyResidual(FluxField(1, 8), 8) < 1e-12
                      && GaugeInvarianceResidual() < 1e-12
                      && BianchiResidualOnFlux() < 1e-12
                      && BianchiResidualOnPureGauge() < 1e-12;
        bool firstQuantity = TheCubeIsTheFirstSubstrateWithPlaquettes()
                          && Math.Abs(FluxPerPlaquette(1, L) - MinimalNonZeroFlux()) < 1e-12
                          && PeriodicityResidual(1, L) < 1e-12
                          && MaxFieldStrength(FluxField(1, L), L, 7) > 0.06;
        if (!flat || !apparatus || !firstQuantity) return "REFUTED";
        return "BOUNDARY";                 // the detector is derived; the fluctuation is not
    }

    public static string WhereItStands()
        => "THE CONNECTION ALONE PRODUCES NOTHING, AND THE CUBE IS THE FIRST PLACE WHERE ANYTHING CAN EXIST - BOTH "
         + "HALVES OF THAT SENTENCE ARE EXACT, AND THE SECOND ONE YIELDS THE FIRST NON-TRIVIAL FIELD QUANTITY. "
         + "E_006 derived the edge connection as a product of lattice differences, so the audit starts by asking "
         + "what such a thing does on its own. Differences COMMUTE on a uniform lattice: the commutator of any two "
         + $"of them vanishes, measured exhaustively at small size and on samples of the full torus at "
         + $"{CommutatorResidual(L, 13):E2}. The same statement in link language is that the connection's own "
         + $"content is a GRADIENT - and a gradient link field has an exactly vanishing plaquette holonomy, "
         + $"{PureGaugeFieldStrength():E2}, because the four contributions to the closed loop telescope. So the "
         + "edge connection is FLAT, and no amount of rearranging it will produce a field strength. THAT IS ONLY "
         + "HALF THE ANSWER, because the question is not whether the connection is curved but whether the "
         + "substrate can carry a curvature at all - and here the census decides it. A field strength needs a "
         + "CLOSED TWO-DIMENSIONAL LOOP. The ring has NONE: it is one-dimensional whatever its link count, so F "
         + "has nowhere to live and E_005's finding that the antisymmetric square vanishes at one direction is "
         + "the same statement. The cube has one elementary plaquette per site and per orientation pair, "
         + $"{ElementaryPlaquettes(L):N0} of them at L = {L}, sitting inside an independent cycle space of "
         + $"{IndependentLoops(L):N0}. (THIS AUDIT ALSO CORRECTS E_005, which reported 3L^2 = 27648 elementary "
         + "plaquettes while its own cycle count already used the correct 3L^3 edges: the two numbers in that "
         + "audit differed by a factor of L, and the correct count is three times the site count.) THE FIRST "
         + "NON-TRIVIAL FIELD QUANTITY IS THEREFORE THE QUANTISED PLAQUETTE FLUX. Take a link field linear in the "
         + "transverse coordinate, A_1 = f y: every (1,2) plaquette then carries the same curvature -f, and the "
         + "requirement that the link be a phase on a closed torus forces f L to be a whole turn, so f = 2 pi n / "
         + $"L. The smallest non-zero field strength the substrate can carry is therefore {MinimalNonZeroFlux():F9}"
         + " - which is EXACTLY the phase quantum AT already has. The number E_005 found to be pure gauge on the "
         + "ring is, on the cube, the minimal real field strength. And the apparatus around it is exact rather "
         + "than approximate: for an Abelian connection the four link phases of a closed loop simply add, so the "
         + "discrete CURVATURE and the discrete CURL agree to roundoff rather than to leading order; the curvature "
         + "is gauge invariant under a site-dependent gauge transformation; and the discrete BIANCHI identity "
         + "holds exactly, because the twelve link contributions to the six faces of an elementary cube cancel in "
         + "pairs. THE VERDICT IS BOUNDARY, and for a specific reason rather than a general caution: everything "
         + "that DETECTS a field strength is derived here - the plaquette, the curvature, the curl, the "
         + "quantisation, gauge invariance and Bianchi - and the first non-trivial quantity is exhibited with its "
         + "value, but the CONNECTION DOES NOT PRODUCE IT. It takes a chosen fluctuation, and nothing in AT yet "
         + "selects one: that is the dynamics layer E_005 located and E_006 left open.";

    // ===================== REPORT SECTIONS =====================

    public static string OutputConstruction()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE CONSTRUCTION, AND WHAT THE CONNECTION DOES BY ITSELF");
        sb.AppendLine($"   ring elementary plaquettes (2-cycles)      : {RingElementaryPlaquettes()}");
        sb.AppendLine($"   cube sites / edges                          : {Sites(L):N0} / {Edges(L):N0}");
        sb.AppendLine($"   cube elementary plaquettes (one per site, per orientation) : {ElementaryPlaquettes(L):N0}");
        sb.AppendLine($"   cube independent cycles E - V + 1           : {IndependentLoops(L):N0}");
        sb.AppendLine($"   commutator of differences                   : {CommutatorResidual(L, 13):E3}  (differences commute: {DifferencesCommute()})");
        sb.AppendLine($"   pure gauge field strength                   : {PureGaugeFieldStrength():E3}  (telescoping)");
        return sb.ToString();
    }

    public static string OutputMeasurements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. F != 0 ?  PURE GAUGE VERSUS A NON-TRIVIAL LOOP");
        sb.AppendLine("   quantity                                          | value        | status");
        foreach (var (quantity, value, status) in Measurements())
            sb.AppendLine($"   {quantity,-49} | {value,-12} | {status}");
        sb.AppendLine();
        sb.AppendLine($"   smallest non-zero flux 2 pi / L : {MinimalNonZeroFlux():F9}");
        sb.AppendLine($"   flux per plaquette for n = 1    : {FluxPerPlaquette(1, L):F9}");
        sb.AppendLine($"   link periodicity residual       : {PeriodicityResidual(1, L):E3}");
        sb.AppendLine($"   the first non-trivial quantity  : {FirstNonTrivialQuantity()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
