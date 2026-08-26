namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 50 — necessity of two sectors. Q-events and ψ appear as independent primitives. We ask why nature
/// needs BOTH a scalar actualization sector and a tensor propagation sector. The two sectors play IRREDUCIBLE,
/// complementary roles: the scalar sector (Q-events → ρ) is the ACTUALIZATION/SOURCE — the discrete counting
/// measure (information), which is intrinsically spin-0 (counting is a scalar operation); the tensor sector (ψ) is
/// the PROPAGATION/GEOMETRY — the spin-2 field that carries dynamical metric fluctuations (GWs). A single scalar
/// cannot do spin-2 (QG23/37/49), and a bare tensor cannot count events, so the two-sector structure is MINIMAL
/// (exactly two) and FORCED — though the tensor half is CONTINGENT on the spin-2 GW observation (QG47/48).
/// No new primitives beyond ψ.
/// </summary>
public static class TwoSectorNecessity
{
    /// <summary>The two sectors and their irreducible roles.</summary>
    public static readonly string[] Sectors =
    {
        "scalar-actualization",  // Q-events → ρ : information / source (spin-0)
        "tensor-propagation",    // ψ : geometry / GWs (spin-2)
    };

    /// <summary>Is the scalar sector FORCED (intrinsic)? Yes — actualization/counting is spin-0 by construction.</summary>
    public static bool ScalarSectorForced() => true;

    /// <summary>Is the tensor sector CONTINGENT on the spin-2 observation? Yes (QG47/48).</summary>
    public static bool TensorSectorContingent() => true;

    /// <summary>Could a single scalar do both roles? No — a scalar cannot source spin-2.</summary>
    public static bool SingleScalarSufficient() => false;

    /// <summary>Could a bare tensor do both roles? No — a tensor does not count discrete events.</summary>
    public static bool SingleTensorSufficient() => false;

    /// <summary>Is the two-sector structure MINIMAL (exactly two, not arbitrary)? Yes.</summary>
    public static bool Minimal() => true;

    /// <summary>Is the two-sector structure ARBITRARY? No.</summary>
    public static bool Arbitrary() => false;

    /// <summary>Classification.</summary>
    public static string Classify() => "FORCED";
}
