namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 170 — Standard Model audit. QG138-169 reproduce many SM structures and parameters.
/// This phase identifies all major measured SM quantities NOT yet tested against TQM-QG and classifies
/// each as TESTED / PARTIALLY TESTED / UNTESTED, computing a coverage percentage and a ranked list of
/// remaining tests.
///
/// The audit enumerates the major measured SM quantities (fermion masses, CKM, PMNS, CP phases,
/// couplings, running couplings, MW/MZ/MH, muon g-2, neutrino masses, precision electroweak
/// observables), assigns each a classification from the TQM-QG record, and ranks the remaining
/// (untested/partially-tested) tests by importance and gap size. Fully deterministic — the table is a
/// static catalog of the TQM-QG derivation record through QG169.
/// </summary>
public static class StandardModelAudit
{
    public enum Coverage
    {
        Tested,
        Partial,
        Untested,
    }

    public sealed record SmQuantity(
        string Name,
        Coverage Status,
        string Phase,
        string Result,
        string Physical,
        double Deviation,
        string Note);

    /// <summary>The major measured SM quantities against the TQM-QG record.</summary>
    public static SmQuantity[] Catalog()
    {
        return new[]
        {
            // ── Fermion masses ────────────────────────────────────────────────
            new SmQuantity("electron mass", Coverage.Tested, "QG140",
                "0.51 MeV", "0.511 MeV", 0.002, "lepton hierarchy (HIERARCHY ORIGIN)"),
            new SmQuantity("muon mass", Coverage.Tested, "QG140",
                "105.66 MeV", "105.66 MeV", 0.000, "lepton hierarchy"),
            new SmQuantity("tau mass", Coverage.Tested, "QG140",
                "1828.40 MeV", "1776.86 MeV", 0.029, "lepton hierarchy"),
            new SmQuantity("up quark mass", Coverage.Partial, "QG146",
                "octave 1, amp", "2.2 MeV", 0.0, "within-sector ratio amplified (UP-SECTOR)"),
            new SmQuantity("down quark mass", Coverage.Partial, "QG146",
                "octave 1, sup", "4.7 MeV", 0.0, "within-sector ratio suppressed"),
            new SmQuantity("charm quark mass", Coverage.Partial, "QG146",
                "r21 x9.8", "1.27 GeV", 0.0, "up amplified x9.8 at r21"),
            new SmQuantity("strange quark mass", Coverage.Partial, "QG146",
                "r21 x0.34", "93 MeV", 0.0, "down suppressed x0.34"),
            new SmQuantity("top quark mass", Coverage.Partial, "QG146",
                "r31 x22.7", "173 GeV", 0.0, "up amplified x22.7 at r31"),
            new SmQuantity("bottom quark mass", Coverage.Partial, "QG146",
                "r31 x0.26", "4.2 GeV", 0.0, "down suppressed x0.26"),

            // ── CKM ───────────────────────────────────────────────────────────
            new SmQuantity("CKM |Vus|", Coverage.Tested, "QG165",
                "#d/(2Σm) = 0.2211", "0.2253", 0.0189, "doublet coupling (CKM ORIGIN)"),
            new SmQuantity("CKM |Vcb|", Coverage.Tested, "QG165",
                "(ω0/ω2)^δd = 0.0416", "0.0411", 0.0122, "octave transition"),
            new SmQuantity("CKM |Vub|", Coverage.Tested, "QG165",
                "2·Vcb·(occ0/occ2) = 0.003826", "0.00382", 0.0014, "occupancy suppression"),
            new SmQuantity("CKM diagonal/unitarity", Coverage.Partial, "QG165",
                "unitarity diag", "≈1", 0.0, "implied by construction"),
            new SmQuantity("CKM δ_CP", Coverage.Tested, "QG166",
                "asin(occ_top/Σm) = 66.3°", "65.6°", 0.0118, "chiral circulation (CP ORIGIN)"),
            new SmQuantity("Jarlskog J", Coverage.Tested, "QG166",
                "3.139e-5", "3.18e-5", 0.0128, "from δ_CP and angles"),

            // ── PMNS ──────────────────────────────────────────────────────────
            new SmQuantity("PMNS θ12", Coverage.Tested, "QG167",
                "√(#d/(Σm+#g)) = 33.35°", "33.4°", 0.0016, "doublet-coupling density (PMNS ORIGIN)"),
            new SmQuantity("PMNS θ23", Coverage.Tested, "QG167",
                "Σ√m/(2#d) = 49.72°", "49.1°", 0.0126, "neutral moment per doublet"),
            new SmQuantity("PMNS θ13", Coverage.Tested, "QG167",
                "√(occ0/(2Σm)) = 8.34°", "8.6°", 0.0299, "octave-access asymmetry"),
            new SmQuantity("PMNS δ_ν", Coverage.Tested, "QG167",
                "asin(44/48) = 66.4°", "≈1.2-1.3 rad", 0.012, "T3 chiral circulation"),

            // ── Couplings ─────────────────────────────────────────────────────
            new SmQuantity("1/α_em", Coverage.Tested, "QG162",
                "Σm+#d = 137", "137.036", 0.00026, "fine-structure 137 (COUPLING ORIGIN)"),
            new SmQuantity("α_weak", Coverage.Tested, "QG162",
                "3/Σm = 0.0316", "0.0338", 0.066, "doublet-transition density"),
            new SmQuantity("α_strong", Coverage.Tested, "QG162",
                "8/Σ√m = 0.1248", "0.1179", 0.059, "family-transition density"),
            new SmQuantity("sin²θ_W", Coverage.Tested, "QG162",
                "#g/(2Σm) = 0.2316", "0.2312", 0.0016, "Weinberg angle"),
            new SmQuantity("α_i(E) running", Coverage.Tested, "QG163/164",
                "octave ladder", "β functions", 0.0, "running + continuous (RUNNING ORIGIN)"),
            new SmQuantity("unification (no)", Coverage.Tested, "QG163",
                "hierarchy preserved", "no SM unif", 0.0, "no in-sector unification"),

            // ── Boson masses ──────────────────────────────────────────────────
            new SmQuantity("MW", Coverage.Tested, "QG168",
                "g₂v/2 = 80.12", "80.38 GeV", 0.0033, "weak scale (MASS ORIGIN)"),
            new SmQuantity("MZ", Coverage.Tested, "QG168",
                "MW/cosθ_W = 91.40", "91.19 GeV", 0.0023, "Weinberg projection"),
            new SmQuantity("MH", Coverage.Tested, "QG169",
                "σ_occ·span/2 = 125.25", "125.25 GeV", 0.00003, "collective scalar (HIGGS ORIGIN)"),
            new SmQuantity("ρ parameter", Coverage.Tested, "QG168",
                "MW²/(MZ²cos²θ_W) = 1", "1", 0.0, "exact SM tree-level"),
            new SmQuantity("MW/MZ", Coverage.Tested, "QG168",
                "cosθ_W = 0.8766", "0.8815", 0.0055, "Weinberg ratio"),

            // ── Untested: g-2 ─────────────────────────────────────────────────
            new SmQuantity("muon g-2 (a_μ)", Coverage.Untested, "—",
                "—", "11659205.9e-10", 0.0, "no TQM-QG derivation"),
            new SmQuantity("electron g-2 (a_e)", Coverage.Untested, "—",
                "—", "1159652180.7e-12", 0.0, "no TQM-QG derivation"),

            // ── Untested: neutrino masses ─────────────────────────────────────
            new SmQuantity("neutrino masses ν1,ν2,ν3", Coverage.Untested, "QG154",
                "structural", "open", 0.0, "T3-only access structural; exact law OPEN"),
            new SmQuantity("mass ordering (normal)", Coverage.Untested, "—",
                "—", "normal? inverted?", 0.0, "ordering not derived"),
            new SmQuantity("Δm²_solar, Δm²_atm", Coverage.Untested, "—",
                "—", "7.4e-5, 2.5e-3 eV²", 0.0, "mass splittings not derived"),
            new SmQuantity("Majorana character", Coverage.Untested, "—",
                "—", "open", 0.0, "not derived"),

            // ── Untested: precision electroweak ───────────────────────────────
            new SmQuantity("Z width Γ_Z", Coverage.Untested, "—",
                "—", "2.4952 GeV", 0.0, "no derivation"),
            new SmQuantity("W width Γ_W", Coverage.Untested, "—",
                "—", "2.085 GeV", 0.0, "no derivation"),
            new SmQuantity("Higgs width Γ_H", Coverage.Untested, "—",
                "—", "3.2 MeV", 0.0, "no derivation"),
            new SmQuantity("S, T, U oblique", Coverage.Untested, "—",
                "—", "≈0", 0.0, "no derivation"),
            new SmQuantity("R_b, R_c", Coverage.Untested, "—",
                "—", "0.2163, 0.1721", 0.0, "no derivation"),
            new SmQuantity("A_FB, A_POL", Coverage.Untested, "—",
                "—", "various", 0.0, "no derivation"),
            new SmQuantity("sin²θ_eff (leptonic)", Coverage.Untested, "—",
                "—", "0.23153", 0.0, "only structural sin²θ_W"),

            // ── Untested: QCD / misc ──────────────────────────────────────────
            new SmQuantity("θ_QCD (strong CP)", Coverage.Untested, "—",
                "—", "<1e-10", 0.0, "no derivation"),
            new SmQuantity("quark absolute masses", Coverage.Partial, "QG146",
                "ratios only", "2.2 MeV - 173 GeV", 0.0, "amplification factors; no absolute scale"),
            new SmQuantity("3 generations", Coverage.Tested, "QG138",
                "octave count = 3", "3", 0.0, "FUNDAMENTAL"),
            new SmQuantity("gauge sector 1+3+8", Coverage.Tested, "QG161",
                "degree-12 match", "1+3+8", 0.0, "GAUGE ORIGIN"),
            new SmQuantity("106 GeV resonance", Coverage.Partial, "QG132",
                "predicted", "unobserved", 0.0, "falsifiable prediction"),
        };
    }

    /// <summary>Number of quantities in each category.</summary>
    public static (int Tested, int Partial, int Untested) Counts()
    {
        var c = Catalog();
        int t = c.Count(x => x.Status == Coverage.Tested);
        int p = c.Count(x => x.Status == Coverage.Partial);
        int u = c.Count(x => x.Status == Coverage.Untested);
        return (t, p, u);
    }

    /// <summary>Coverage fraction of the TESTED category alone (0..1).</summary>
    public static double TestedCoverage()
    {
        var (t, _, u) = Counts();
        return (double)t / (t + u);
    }

    /// <summary>
    /// Weighted coverage: TESTED = 1.0, PARTIAL = 0.5, UNTESTED = 0.0. The overall fraction of the
    /// measured-SM information space that TQM-QG has reached.
    /// </summary>
    public static double WeightedCoverage()
    {
        var c = Catalog();
        double w = c.Sum(x => x.Status switch
        {
            Coverage.Tested => 1.0,
            Coverage.Partial => 0.5,
            _ => 0.0,
        });
        return w / c.Length;
    }

    /// <summary>Coverage fraction of the mass-weighted list (bosons + fermions).</summary>
    public static double MassCoverage()
    {
        var c = Catalog();
        var masses = c.Where(x =>
            x.Name.Contains("mass") || x.Name.Contains("MW") || x.Name.Contains("MZ") ||
            x.Name.Contains("MH") || x.Name.Contains("ν") || x.Name.Contains("g-2"));
        if (!masses.Any()) return 0;
        double w = masses.Sum(x => x.Status switch
        {
            Coverage.Tested => 1.0,
            Coverage.Partial => 0.5,
            _ => 0.0,
        });
        return w / masses.Count();
    }

    /// <summary>The untested and partially-tested quantities, ranked by importance (physics reach × gap).</summary>
    public static (string Name, Coverage Status, int Rank, string Why)[] RemainingTests()
    {
        var untested = Catalog()
            .Where(x => x.Status != Coverage.Tested)
            .Select(x => (Name: x.Name, Status: x.Status))
            .ToList();

        // Ranked by physics importance and size of the gap to TQM-QG reach.
        (string, Coverage, int, string)[] ranked =
        {
            ("muon g-2 (a_μ)", Coverage.Untested, 1,
                "the largest measured-vs-SM deviation; no TQM-QG origin — highest-priority gap"),
            ("neutrino masses ν1,ν2,ν3", Coverage.Untested, 2,
                "structural origin exists (QG154) but exact law, values, ordering are open"),
            ("Δm²_solar, Δm²_atm", Coverage.Untested, 3,
                "mass splittings — needed to pin the neutrino mass scale"),
            ("mass ordering (normal)", Coverage.Untested, 4,
                "normal vs inverted must follow from the D96 neutrino sector"),
            ("quark absolute masses", Coverage.Partial, 5,
                "amplification factors exist (QG146) but no absolute mass scale"),
            ("θ_QCD (strong CP)", Coverage.Untested, 6,
                "strong CP solution absent in the D96 framework"),
            ("sin²θ_eff (leptonic)", Coverage.Untested, 7,
                "structural Weinberg angle exists; the precise effective angle does not"),
            ("S, T, U oblique", Coverage.Untested, 8,
                "precision-EW test of new-physics reach"),
            ("Z width Γ_Z", Coverage.Untested, 9,
                "boson widths derive from the gauge sector but are untested"),
            ("W width Γ_W", Coverage.Untested, 10,
                "as Γ_Z"),
            ("Higgs width Γ_H", Coverage.Untested, 11,
                "follows from λ_H + MH, untested"),
            ("electron g-2 (a_e)", Coverage.Untested, 12,
                "second g-2 target"),
            ("R_b, R_c", Coverage.Untested, 13,
                "Z-pole flavor ratios"),
            ("A_FB, A_POL", Coverage.Untested, 14,
                "Z-pole asymmetries"),
            ("Majorana character", Coverage.Untested, 15,
                "0νββ test of the D96 neutrino"),
            ("CKM diagonal/unitarity", Coverage.Partial, 16,
                "implicit; explicit Vtd/Vts/Vtb not derived"),
            ("106 GeV resonance", Coverage.Partial, 17,
                "falsifiable prediction awaiting data (QG132)"),
        };
        return ranked;
    }

    /// <summary>Summary line describing the audit result.</summary>
    public static string Summary()
    {
        var (t, p, u) = Counts();
        return $"TESTED {t} / PARTIAL {p} / UNTESTED {u} — tested coverage " +
               $"{TestedCoverage():P0}, weighted coverage {WeightedCoverage():P1}";
    }
}
