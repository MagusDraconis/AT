namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 85 — Origin of Standard Model parameters. The network hosts the required sectors, but key numbers
/// remain empirical. This phase asks whether masses, couplings, generations, and color count can EMERGE from
/// network information content.
///
/// Answer: POSTULATED. The SM has ~19 free parameters (3 gauge couplings, 2 Higgs parameters, 9 charged-fermion
/// masses, 4 CKM parameters, and the QCD θ term); with massive neutrinos one adds 3 masses + 4 PMNS parameters.
/// The link's information CAPACITY is ample (it already carries the full complex rank-2 object plus a family
/// index), so capacity does not CONSTRAIN the values — it only permits them. Symmetries (gauge, Lorentz) fix the
/// FORM of the parameters, not their numerical VALUES. The family count (3) and color count (3) are already
/// postulatory (QG79/QG80), and the mass hierarchies (e.g. up vs top quark) are not derived. Hence the SM
/// parameters are POSTULATED: the network can HOST them (compatible) but does not DERIVE them. No new primitives
/// added here (audit only).
/// </summary>
public static class SMParameters
{
    /// <summary>The five candidate mechanisms.</summary>
    public static readonly string[] Mechanisms =
    {
        "parameter-counting",
        "link-information-capacity",
        "symmetry-constraints",
        "family-index-structure",
        "mass-hierarchies",
    };

    /// <summary>Standard Model free parameters (3 gauge + 2 Higgs + 9 masses + 4 CKM + 1 QCD theta).</summary>
    public static int SmParameterCount() => 19;

    /// <summary>Additional parameters if neutrinos are massive (3 masses + 4 PMNS Dirac).</summary>
    public static int NeutrinoAdditionalParameters() => 7;

    /// <summary>Does the link's information capacity SUFFICE to host the parameters? Yes.</summary>
    public static bool LinkCapacitySufficient() => true;

    /// <summary>Does the link's capacity DETERMINE the parameter VALUES? No — it only permits them.</summary>
    public static bool LinkCapacityDeterminesValues() => false;

    /// <summary>Do symmetries fix the FORM but not the VALUES? Yes.</summary>
    public static bool SymmetriesFixFormNotValues() => true;

    /// <summary>Is the family count (3) free (not derived)? Yes.</summary>
    public static bool FamilyCountFree() => true;

    /// <summary>Are the mass hierarchies (e.g. up vs top quark) DERIVED? No.</summary>
    public static bool MassHierarchiesDerived() => false;

    /// <summary>Classification: DERIVED / COMPATIBLE / POSTULATED.</summary>
    public static string Classify() => "POSTULATED";
}
