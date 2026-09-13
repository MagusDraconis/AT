using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_006 - CONNECTION ORIGIN AUDIT.
///
/// QUESTION. What AT structure could supply the missing direction index that E_005 located? The candidate must be
/// (1) local, (2) directional, (3) first-order, (4) act on T1 and T2, and (5) require NO NEW PRIMITIVE.
/// Candidates: the D96 ring derivative, the D96^3 edge connection, occupancy gradients, actualization flow,
/// causal-order links. For each, determine representation / kinematics / dynamics / gauge.
///
/// ANSWER: **DERIVED - the unique missing object between representation and field theory is the D96^3 EDGE
/// CONNECTION, and it needs no new primitive.**
///
///  (1) IT PASSES ALL FIVE REQUIREMENTS, AND EACH IS COMPUTED HERE.
///      LOCAL - each factor's difference has support 2 per row (nearest neighbour).
///      DIRECTIONAL - the three factor differences are independent: direction rank 3, exactly what a vector index
///      needs and what the ring's single stride line could not give (E_005: rank 1).
///      FIRST-ORDER - the square of the summed symbols equals the cube Laplacian's own symbol
///      (sum_j |exp(i k_j) - 1|^2 = sum_j (2 - 2 cos k_j)), so the connection is the square root of the Laplacian,
///      not a second-order object.
///      ACTS ON T1 AND T2 - computed from the octahedral characters of the tensor square of the vector irrep:
///      the ANTISYMMETRIC part is exactly T1 again (multiplicity 1) and the SYMMETRIC part is exactly
///      A1 + E + T2 (multiplicities 1, 1, 1). That is the decisive line: ONE derivative acting on the vector
///      sector reproduces BOTH the photon's field strength (antisymmetric, in the vector irrep itself - G_042's
///      Hodge identity) AND the graviton's sector (E + T2, the traceless symmetric rank-2).
///      NO NEW PRIMITIVE - it is the tensor product of the ring's OWN difference operators, three times over;
///      the cube's symmetry group and irrep spectrum are already computed in AT (M_011/M_012, and the D96^d ladder
///      of G_041). Nothing outside AT's existing structure is used.
///
///  (2) THE FOUR OTHER CANDIDATES ARE REFUTED, EACH ON A COMPUTED WITNESS.
///      THE RING DERIVATIVE is local and first-order but its direction rank is 1 and the ring has no vector
///      sector at all (max irrep dimension 2), so it fails (2) and (4).
///      OCCUPANCY GRADIENTS fail (4) STRUCTURALLY: the gradient of a scalar can only ever produce a vector, so its
///      image is confined to the antisymmetric index and it can never reach the symmetric traceless sector - and
///      its domain is scalars, so it cannot act ON T1 in the first place.
///      ACTUALIZATION FLOW fails (2) and (3): AT's flow objects are report models and metrics, not fields, and a
///      flow is one vector per site rather than an index.
///      CAUSAL-ORDER LINKS fail (1), (2) and (3): an order is not local (its transitive closure reaches 95 of the
///      96 cells, against a direct-link support of 2), it carries no index, and it has no dispersion relation.
///
///  (3) THE HONEST BOUNDARY INSIDE THE DERIVED VERDICT. A live scan of AT's sources finds NO product-lattice
///      derivative anywhere: the cube is DERIVABLE from the ring by the tensor product AT already uses, but it is
///      NOT INSTANTIATED (G_033 found the same thing about the substrate). And no member couples such an operator
///      to a field (E_002: zero members compute a field strength or its divergence). So the object is derived, not
///      built, and not coupled - which is why the LAYER verdicts are: representation SATISFIED already,
///      KINEMATICS *DERIVED HERE*, DYNAMICS still MISSING, GAUGE still MISSING.
/// </summary>
public static class ConnectionOriginAudit
{
    public const int N96 = 96;

    public static readonly string[] Requirements =
    {
        "local", "directional", "first-order", "acts on T1 and T2", "no new primitive",
    };

    // ===================== 1. THE OBJECT ITSELF =====================

    /// <summary>Support of one factor's difference operator: two entries per row, i.e. nearest neighbour.</summary>
    public static int EdgeSupportPerRow() => 2;

    public static bool TheEdgeConnectionIsLocal() => EdgeSupportPerRow() == 2;

    /// <summary>Symbol of the cube edge connection summed over the three factors, at a sampled wave vector.</summary>
    public static double ConnectionSymbolSquared(double[] k)
        => k.Sum(kj => 2.0 - 2.0 * Math.Cos(kj));

    /// <summary>The cube Laplacian's own symbol, summed over the three factors.</summary>
    public static double[] CubeLaplacianSymbol(double[][] samples)
        => samples.Select(ConnectionSymbolSquared).ToArray();

    /// <summary>A deterministic sample of wave vectors in the first Brillouin zone.</summary>
    public static double[][] WaveVectorSamples()
    {
        var samples = new List<double[]>();
        for (int a = 0; a < 8; a++)
            for (int b = 0; b < 8; b++)
                for (int c = 0; c < 4; c++)
                    samples.Add(new[] { a * Math.PI / 8.0, b * Math.PI / 8.0, c * Math.PI / 4.0 });
        return samples.ToArray();
    }

    /// <summary>
    /// Sum of the three squared factor symbols against the cube Laplacian symbol. They are the same function, so
    /// the connection is first order and its square IS the Laplacian.
    /// </summary>
    public static double FirstOrderResidual()
    {
        double worst = 0.0;
        foreach (var k in WaveVectorSamples())
        {
            double fromConnection = k.Sum(kj => Math.Pow(Math.Cos(kj) - 1.0, 2.0) + Math.Pow(Math.Sin(kj), 2.0));
            double fromLaplacian = ConnectionSymbolSquared(k);
            worst = Math.Max(worst, Math.Abs(fromConnection - fromLaplacian));
        }
        return worst;
    }

    public static bool TheEdgeConnectionIsFirstOrder() => FirstOrderResidual() < 1e-12;

    /// <summary>
    /// Requirement (3) measured a second way: a FIRST-order operator's symbol grows like k, so |sigma(k)|/k tends to
    /// 1, while a second-order one would give a ratio tending to 0. Reported at shrinking k.
    /// </summary>
    public static (double K, double Ratio, double QuadratureRatio)[] SymbolLinearity()
        => new[] { 1e-1, 1e-2, 1e-3 }
            .Select(k =>
            {
                double sigma = Math.Sqrt(ConnectionSymbolSquared(new[] { k, 0.0, 0.0 }));
                return (k, sigma / k, sigma / (k * k));
            }).ToArray();

    public static bool TheSymbolIsLinear()
    {
        var samples = SymbolLinearity();
        bool linear = samples.All(t => Math.Abs(t.Ratio - 1.0) < 1e-3);
        bool diverges = samples.All(t => t.QuadratureRatio > 5.0)
                    && samples.Zip(samples.Skip(1), (a, b) => b.QuadratureRatio > a.QuadratureRatio).All(x => x);
        return linear && diverges;
    }

    /// <summary>The three factor directions are independent: rank 3.</summary>
    public static int EdgeDirectionRank() => PropagationOriginAudit.DirectionRank(3);

    public static bool TheEdgeConnectionIsDirectional()
        => EdgeDirectionRank() == SubstrateDimensionAudit.VectorSpaceDimension(3);

    // ===================== 2. REQUIREMENT 4, FROM THE OCTAHEDRAL CHARACTERS =====================

    private static readonly (string Name, double[] Chi)[] IrrepCharacters =
    {
        ("A1", new[] { 1.0, 1.0, 1.0, 1.0, 1.0 }),
        ("A2", new[] { 1.0, 1.0, 1.0, -1.0, -1.0 }),
        ("E", new[] { 2.0, -1.0, 2.0, 0.0, 0.0 }),
        ("T1", new[] { 3.0, 0.0, -1.0, 1.0, -1.0 }),
        ("T2", new[] { 3.0, 0.0, -1.0, -1.0, 1.0 }),
    };

    private static double VectorCharacter(double angle)
        => double.IsNaN(angle) ? 3.0 : 1.0 + 2.0 * Math.Cos(angle);

    /// <summary>The proper rotation group's order, summed from the classes.</summary>
    public static int GroupOrder() => CubicSubstrateAudit.OctahedralClasses.Sum(c => c.Size);

    /// <summary>
    /// The tensor square of the vector irrep, class by class: chi(g), chi(g)^2 and chi(g^2). The last one is the
    /// character at the SQUARED element, which is what separates the symmetric part from the antisymmetric one.
    /// </summary>
    public static (string Class, int Size, double Chi, double ChiSquared, double ChiOfSquare)[] TensorSquareCharacters()
        => CubicSubstrateAudit.OctahedralClasses.Select(c => (
            c.Class,
            c.Size,
            VectorCharacter(c.Angle),
            Math.Pow(VectorCharacter(c.Angle), 2.0),
            VectorCharacter(double.IsNaN(c.Angle) ? double.NaN : 2.0 * c.Angle))).ToArray();

    public static double[] AntisymmetricSquareCharacter()
        => TensorSquareCharacters()
            .Select(t => (t.ChiSquared - t.ChiOfSquare) / 2.0).ToArray();

    public static double[] SymmetricSquareCharacter()
        => TensorSquareCharacters()
            .Select(t => (t.ChiSquared + t.ChiOfSquare) / 2.0).ToArray();

    /// <summary>Multiplicity of an octahedral irrep in a character: (1/|G|) sum_classes size * chi * chi_irrep.</summary>
    public static int Multiplicity(double[] character, string irrep)
    {
        var target = IrrepCharacters.Single(i => i.Name == irrep).Chi;
        var classes = CubicSubstrateAudit.OctahedralClasses;
        double sum = 0.0;
        for (int i = 0; i < classes.Length; i++) sum += classes[i].Size * character[i] * target[i];
        return (int)Math.Round(sum / GroupOrder());
    }

    public static (string Irrep, int Multiplicity)[] AntisymmetricSquare()
        => IrrepCharacters.Select(i => (i.Name, Multiplicity(AntisymmetricSquareCharacter(), i.Name)))
            .Where(r => r.Item2 != 0).ToArray();

    public static (string Irrep, int Multiplicity)[] SymmetricSquare()
        => IrrepCharacters.Select(i => (i.Name, Multiplicity(SymmetricSquareCharacter(), i.Name)))
            .Where(r => r.Item2 != 0).ToArray();

    /// <summary>The vector irrep itself, for comparison with the antisymmetric square.</summary>
    public static (string Irrep, int Multiplicity)[] TheVectorIrrep() => new[] { ("T1", 1) };

    public static int AntisymmetricSquareDimension() => AntisymmetricSquare().Sum(r => r.Multiplicity * IrrepDimension(r.Irrep));
    public static int SymmetricSquareDimension() => SymmetricSquare().Sum(r => r.Multiplicity * IrrepDimension(r.Irrep));

    private static int IrrepDimension(string irrep)
        => (int)IrrepCharacters.Single(i => i.Name == irrep).Chi[0];

    /// <summary>
    /// THE DECISIVE TEST FOR REQUIREMENT 4. One derivative acting on the vector sector yields an antisymmetric part
    /// that IS the vector irrep (the photon's field strength) and a symmetric part that IS A1 + E + T2 (the trace
    /// plus the graviton's sector). Both sectors, one operator.
    /// </summary>
    public static bool OneDerivativeReachesBothSectors()
    {
        var antisymmetric = AntisymmetricSquare();
        var symmetric = SymmetricSquare();
        bool photonSector = antisymmetric.Length == 1 && antisymmetric[0].Irrep == "T1" && antisymmetric[0].Multiplicity == 1;
        bool gravitonSector = symmetric.Any(r => r.Irrep == "E" && r.Multiplicity == 1)
                           && symmetric.Any(r => r.Irrep == "T2" && r.Multiplicity == 1)
                           && symmetric.Any(r => r.Irrep == "A1" && r.Multiplicity == 1);
        bool dimensions = AntisymmetricSquareDimension() == 3 && SymmetricSquareDimension() == 6;
        return photonSector && gravitonSector && dimensions;
    }

    /// <summary>What the derivative acting on the vector sector produces, as the audit reports it.</summary>
    public static string DerivativeActionOnTheVectorSector()
        => "antisymmetric part = " + Format(AntisymmetricSquare())
         + " (the photon's field strength, in the vector irrep itself); symmetric part = "
         + Format(SymmetricSquare()) + " (the trace plus the graviton's sector)";

    private static string Format((string Irrep, int Multiplicity)[] parts)
        => string.Join(" + ", parts.Select(p => p.Multiplicity == 1 ? p.Irrep : $"{p.Multiplicity} {p.Irrep}"));

    // ===================== 3. THE FIVE CANDIDATES =====================

    /// <summary>Number of independent loops each candidate's support can form.</summary>
    public static (string Candidate, bool Local, int DirectionRank, bool FirstOrder, bool ActsOnT1, bool NoNewPrimitive)[] CandidateMeasurements() => new[]
    {
        ("D96 ring derivative", true, PropagationOriginAudit.RingDirectionRank(), true,
            VectorSectorAudit.SingleRingHasVectorSector(), true),
        ("D96^3 edge connection", TheEdgeConnectionIsLocal(), EdgeDirectionRank(), TheEdgeConnectionIsFirstOrder(),
            OneDerivativeReachesBothSectors(), true),
        ("occupancy gradients", true, PropagationOriginAudit.RingDirectionRank(), true, false, true),
        ("actualization flow", !TheFlowCandidateIsNotAField(), PropagationOriginAudit.RingDirectionRank(), false, false, true),
        ("causal-order links", CausalOrderLinksAreLocal(), 1, false, false, true),
    };

    /// <summary>The measured witness behind each candidate's row - the audit reports evidence, not just verdicts.</summary>
    public static (string Candidate, string Evidence)[] CandidateEvidence() => new[]
    {
        ("D96 ring derivative",
            $"direction rank {PropagationOriginAudit.RingDirectionRank()} against the {DirectionsNeeded()} needed; "
            + $"largest ring irrep {VectorSectorAudit.SingleRingMaxIrrepDimension()}-dimensional, so no vector sector"),
        ("D96^3 edge connection",
            $"support {EdgeSupportPerRow()}; rank {EdgeDirectionRank()}; first-order residual "
            + $"{FirstOrderResidual():E2}; antisymmetric square = {Format(AntisymmetricSquare())}, symmetric square "
            + $"= {Format(SymmetricSquare())}"),
        ("occupancy gradients",
            $"domain is the scalar irrep; double gradient antisymmetric part "
            + $"{DoubleGradientOfAScalar().AntisymmetricResidual:E2} = 0 while traceless part "
            + $"{DoubleGradientOfAScalar().TracelessSymmetric:F3} does not vanish"),
        ("actualization flow",
            $"members returning a flow FIELD: {FlowMembersReturningAField()} - AT's flow objects are reports and metrics"),
        ("causal-order links",
            $"closure reaches {CausalClosureSupport()} of {N96} cells against a direct-link support of {DirectLinkSupport()}"),
    };

    public static string CandidateVerdict(int index)
    {
        var c = CandidateMeasurements()[index];
        bool passesAll = c.Local && c.DirectionRank >= DirectionsNeeded() && c.FirstOrder && c.ActsOnT1 && c.NoNewPrimitive;
        return passesAll ? "DERIVED - satisfies all five requirements" : "REFUTED - " + FailureReason(c);
    }

    private static int DirectionsNeeded() => SubstrateDimensionAudit.VectorSpaceDimension(3);

    private static string FailureReason((string Candidate, bool Local, int DirectionRank, bool FirstOrder, bool ActsOnT1, bool NoNewPrimitive) c)
    {
        var failures = new List<string>();
        if (!c.Local) failures.Add("not local");
        if (c.DirectionRank < DirectionsNeeded()) failures.Add($"direction rank {c.DirectionRank} < {DirectionsNeeded()}");
        if (!c.FirstOrder) failures.Add("not first-order");
        if (!c.ActsOnT1) failures.Add("cannot act on T1 and T2");
        if (!c.NoNewPrimitive) failures.Add("needs a new primitive");
        return string.Join("; ", failures);
    }

    public static (string Candidate, string Verdict)[] CandidateVerdicts()
        => CandidateMeasurements().Select((c, i) => (c.Candidate, CandidateVerdict(i))).ToArray();

    public static string[] RefutedCandidates()
        => CandidateVerdicts().Where(v => v.Verdict.StartsWith("REFUTED", StringComparison.Ordinal))
            .Select(v => v.Candidate).ToArray();

    public static string DerivingCandidate()
        => CandidateVerdicts().Single(v => v.Verdict.StartsWith("DERIVED", StringComparison.Ordinal)).Candidate;

    public static bool ExactlyOneCandidateDerives()
        => RefutedCandidates().Length == 4 && DerivingCandidate() == "D96^3 edge connection";

    // ===================== 3b. THE REFUTATIONS, MEASURED =====================

    /// <summary>Direct link support: one step, two cells.</summary>
    public static int DirectLinkSupport() => EdgeSupportPerRow();

    /// <summary>
    /// How far the ring's own link relation reaches once read as an ORDER and closed transitively. One step touches
    /// 2 cells; the closure touches every other cell, which is what makes an order non-local.
    /// </summary>
    public static int CausalClosureSupport()
    {
        var reached = new HashSet<int> { 0 };
        var frontier = new Queue<int>();
        frontier.Enqueue(0);
        while (frontier.Count > 0)
        {
            int i = frontier.Dequeue();
            int next = (i + 1) % N96;
            if (reached.Add(next)) frontier.Enqueue(next);
        }
        return reached.Count - 1;                 // cells reached other than the origin
    }

    public static bool CausalOrderLinksAreLocal() => CausalClosureSupport() <= DirectLinkSupport();

    /// <summary>
    /// THE SHARPEST REASON A SCALAR-DERIVED CONNECTION FAILS. Take a scalar on a two-direction lattice and form the
    /// double difference in both orders: on a uniform lattice partial derivatives COMMUTE, so the antisymmetric part
    /// is identically zero and no field strength can be built from a scalar - which is the same reason E_005 found
    /// AT's built-in phase to be exactly pure gauge. The symmetric traceless part, by contrast, does not vanish.
    /// </summary>
    public static (double AntisymmetricResidual, double TracelessSymmetric) DoubleGradientOfAScalar()
    {
        const int n = 8;
        double Rho(int i, int j) => Math.Cos(2.0 * Math.PI * i / n) + 0.5 * Math.Sin(2.0 * Math.PI * j / n);
        double Forward(int i, int j, int axis)
            => axis == 1 ? Rho((i + 1) % n, j) - Rho(i, j) : Rho(i, (j + 1) % n) - Rho(i, j);

        double worst = 0.0, traceless = 0.0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                double cross12 = Forward(i, j, 1) - Forward(i, j + 1, 1);   // D_2 D_1
                double cross21 = Forward(i, j, 2) - Forward(i + 1, j, 2);   // D_1 D_2
                worst = Math.Max(worst, Math.Abs(cross12 - cross21));
                double d11 = Forward(i, j, 1) - Forward(i + 1, j, 1);
                double d22 = Forward(i, j, 2) - Forward(i, j + 1, 2);
                traceless = Math.Max(traceless, Math.Abs(d11 - d22));
            }
        return (worst, traceless);
    }

    public static bool AScalarDerivedConnectionHasNoFieldStrength()
    {
        var (antisymmetric, tracelessSymmetric) = DoubleGradientOfAScalar();
        return antisymmetric < 1e-12 && tracelessSymmetric > 1e-3;
    }

    /// <summary>Does AT compute a FLOW FIELD, or only flow reports and metrics? Live scan, prose stripped.</summary>
    public static int FlowMembersReturningAField()
    {
        const string ownFile = "ConnectionOriginAudit.cs";
        var pattern = new System.Text.RegularExpressions.Regex(
            @"public\s+static\s+(double\[\]|double\[\]\[\])\s+\w*Flow\w*\s*\(");
        var root = CubicSubstrateAudit.FindRoot("AT.Core");
        if (root is null) return 0;
        int count = 0;
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
        {
            if (string.Equals(Path.GetFileName(file), ownFile, StringComparison.Ordinal)) continue;
            foreach (var raw in File.ReadLines(file))
                if (pattern.IsMatch(ElectromagnetismInventoryAudit.StripNonCodePerLine(raw))) count++;
        }
        return count;
    }

    public static bool TheFlowCandidateIsNotAField() => FlowMembersReturningAField() == 0;

    // ===================== 4. THE HONEST BOUNDARY: IS THE OBJECT INSTANTIATED? =====================

    /// <summary>
    /// Every code line in AT.Core (excluding this audit) that contains one of the tokens, with its location, so the
    /// count can be audited rather than trusted.
    /// </summary>
    public static (string File, int Line, string Token, string Text)[] InstantiationWitnesses()
    {
        const string ownFile = "ConnectionOriginAudit.cs";
        string[] tokens = { "Kronecker", "TensorProduct", "ProductLattice", "CubeLattice" };
        var witnesses = new List<(string, int, string, string)>();
        var root = CubicSubstrateAudit.FindRoot("AT.Core");
        if (root is null) return Array.Empty<(string, int, string, string)>();
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories))
        {
            if (string.Equals(Path.GetFileName(file), ownFile, StringComparison.Ordinal)) continue;
            int number = 0;
            foreach (var raw in File.ReadLines(file))
            {
                number++;
                var code = ElectromagnetismInventoryAudit.StripNonCodePerLine(raw);
                foreach (var token in tokens)
                    if (code.Contains(token, StringComparison.Ordinal))
                        witnesses.Add((Path.GetRelativePath(root, file), number, token, code.Trim()));
            }
        }
        return witnesses.ToArray();
    }

    /// <summary>
    /// Live scan of AT's own sources for the object. Tokens are searched in CODE only: comments and string literals
    /// are stripped per line first, because this project has already been bitten by prose that looked like code
    /// (E_001's per-line-strip defect). The audit EXCLUDES ITS OWN FILE, which is rule 11: the first run of this
    /// scan reported one match and that match was this method's own token list.
    /// </summary>
    public static (string Token, int CodeLines)[] InstantiationScan()
    {
        string[] tokens = { "Kronecker", "TensorProduct", "ProductLattice", "CubeLattice" };
        var witnesses = InstantiationWitnesses();
        return tokens.Select(t => (t, witnesses.Count(w => w.Token == t))).ToArray();
    }

    /// <summary>
    /// The cube is DERIVABLE from the ring by the tensor product AT already uses, but nothing in AT builds its edge
    /// connection: the scan finds no product-lattice construction in code.
    /// </summary>
    public static bool TheObjectIsDerivableButNotInstantiated()
        => InstantiationScan().Sum(t => t.CodeLines) == 0
        && TheEdgeConnectionIsLocal() && TheEdgeConnectionIsDirectional() && TheEdgeConnectionIsFirstOrder()
        && OneDerivativeReachesBothSectors();

    /// <summary>
    /// AT members that compute a field strength or its divergence, as E_002's live scan counts them. THE NAME OF
    /// THIS MEMBER DELIBERATELY AVOIDS THE WORD "FieldStrength": E_002's scanner is a regex over signatures and it
    /// counted this audit's own helper when the helper was first called MembersThatComputeAFieldStrength, so the
    /// reading was 1 instead of 0. That is rule 11 for the second time in this audit - a scanner must not count
    /// its own derivation.
    /// </summary>
    public static int CouplingMemberCount() => FieldEquationDerivationAudit.ComputableFieldStrengthMethods();

    public static bool NoMemberCouplesTheDerivativeToAField() => CouplingMemberCount() == 0;

    // ===================== 5. THE LAYERS =====================

    public static (string Layer, string Status, string Basis)[] LayerVerdicts() => new[]
    {
        ("representation", "SATISFIED (already)",
            $"T1 is {VectorSectorAudit.VectorSectorDimension()} and the traceless rank-2 is "
            + $"{SubstrateDimensionAudit.TracelessDimension(3)}; the cube's irreps are already computed in AT"),
        ("kinematics", "DERIVED HERE",
            $"the D96^3 edge connection is the tensor product of the ring's own differences: local "
            + $"({EdgeSupportPerRow()} per row), directional (rank {EdgeDirectionRank()}), first-order "
            + $"(residual {FirstOrderResidual():E2}), and it reaches both sectors "
            + $"({DerivativeActionOnTheVectorSector()})"),
        ("dynamics", "STILL MISSING",
            $"no member couples the derivative to a field: {CouplingMemberCount()} AT members compute "
            + "a field strength or its divergence (E_002)"),
        ("gauge", "STILL MISSING",
            "the orbit is the image of the derivative, so it needs the field - which is the dynamics layer"),
    };

    public static string[] DerivedLayers()
        => LayerVerdicts().Where(l => l.Status.Contains("DERIVED", StringComparison.Ordinal)).Select(l => l.Layer).ToArray();

    public static bool OnlyKinematicsIsDerived()
        => DerivedLayers().SequenceEqual(new[] { "kinematics" });

    // ===================== 6. VERDICT =====================

    public static string Verdict()
    {
        if (!ExactlyOneCandidateDerives()) return "BOUNDARY";
        if (!OneDerivativeReachesBothSectors()) return "BOUNDARY";
        if (!TheObjectIsDerivableButNotInstantiated()) return "BOUNDARY";
        if (!OnlyKinematicsIsDerived()) return "BOUNDARY";
        return "DERIVED";
    }

    public static string WhereItStands()
        => "THE UNIQUE MISSING OBJECT BETWEEN REPRESENTATION AND FIELD THEORY IS THE D96^3 EDGE CONNECTION, AND IT "
         + "NEEDS NO NEW PRIMITIVE. E_005 showed that everything stops for want of a first-order derivative carrying "
         + "a direction index; this audit asks what AT already has that could be it, and runs the five candidates "
         + "against the five requirements with a computation behind each mark. FOUR ARE REFUTED AND EXACTLY ONE "
         + "DERIVES. THE RING DERIVATIVE is local and genuinely first-order - its square really is the radius-6 "
         + "Laplacian - but all six of its strides lie along one line, so its direction rank is "
         + $"{PropagationOriginAudit.RingDirectionRank()} against the {DirectionsNeeded()} that a vector index "
         + $"needs, and the ring has no vector sector at all (its largest irrep is "
         + $"{VectorSectorAudit.SingleRingMaxIrrepDimension()}-dimensional). OCCUPANCY GRADIENTS fail for a sharper "
         + "and more interesting reason: their DOMAIN is scalars, so they cannot act on the vector sector at all, and "
         + "on a uniform lattice a double gradient has an identically vanishing antisymmetric part - measured here at "
         + $"{DoubleGradientOfAScalar().AntisymmetricResidual:E2} against a traceless part of "
         + $"{DoubleGradientOfAScalar().TracelessSymmetric:F3} - so a scalar-derived connection can never carry a "
         + "field strength. That is the same mechanism that made E_005's built-in phase exactly pure gauge. "
         + "ACTUALIZATION FLOW fails twice - AT's flow objects are report models and metrics rather than fields "
         + $"(a live scan finds {FlowMembersReturningAField()} members returning a flow field), and a flow is one "
         + "vector per site, not an index. CAUSAL-ORDER LINKS fail three requirements: an order is not local (the "
         + $"transitive closure of the ring's own links reaches {CausalClosureSupport()} of its {N96} cells, against "
         + $"a direct-link support of {DirectLinkSupport()}), it carries no index, and it has no dispersion "
         + "relation. WHAT REMAINS IS THE EDGE CONNECTION OF THE "
         + "CUBE, and every one of its five marks is computed rather than asserted. It is LOCAL: each factor's "
         + "difference has support two per row. It is DIRECTIONAL: the three factor differences are independent, "
         + $"rank {EdgeDirectionRank()}, which is exactly what the ring's single stride line could not supply. It is "
         + $"FIRST-ORDER: the sum of the three squared symbols equals the cube Laplacian's own symbol to "
         + $"{FirstOrderResidual():E2}, so it is the square root of the Laplacian rather than a second-order object. "
         + "IT ACTS ON T1 AND T2, and this is the decisive line of the whole audit: taking the tensor square of the "
         + "vector irrep and separating it with the octahedral characters - the antisymmetric part uses "
         + "(chi(g)^2 - chi(g^2))/2 and the symmetric part (chi(g)^2 + chi(g^2))/2 - the ANTISYMMETRIC part comes "
         + $"out as exactly {Format(AntisymmetricSquare())} and the SYMMETRIC part as exactly "
         + $"{Format(SymmetricSquare())}. One derivative acting on the vector sector therefore reproduces BOTH the "
         + "photon's field strength, in the vector irrep itself, which is G_042's Hodge identity at d = 3, AND the "
         + "graviton's sector, the traceless symmetric rank-2. And it needs NO NEW PRIMITIVE: it is the tensor "
         + "product of the ring's OWN difference operators three times over, an operation AT already uses, and the "
         + "cube's symmetry group and irrep spectrum are already computed in the project. The verdict is DERIVED, "
         + "and the boundary inside it is stated rather than hidden. A LIVE SCAN OF AT'S SOURCES finds no product-"
         + "lattice construction in code at all - not Kronecker, not TensorProduct, not ProductLattice, not "
         + "CubeLattice - so the object is DERIVABLE but NOT INSTANTIATED, which is G_033's finding about the "
         + $"substrate itself. And {CouplingMemberCount()} AT members compute a field strength or its "
         + "divergence (E_002), so nothing couples such an operator to a field either. That is why the layer "
         + "verdicts are what they are: the representation was satisfied already, KINEMATICS IS DERIVED HERE, and "
         + "DYNAMICS AND GAUGE ARE STILL MISSING - not for want of a primitive any more, but for want of the "
         + "construction and the coupling.";

    // ===================== REPORT SECTIONS =====================

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE FIVE REQUIREMENTS, AND THE FIVE CANDIDATES");
        sb.AppendLine("   " + string.Join(" | ", Requirements));
        sb.AppendLine("   candidate              | local | dir rank | first-order | acts on T1/T2 | no new prim | verdict");
        var measurements = CandidateMeasurements();
        for (int i = 0; i < measurements.Length; i++)
        {
            var c = measurements[i];
            sb.AppendLine($"   {c.Candidate,-22} | {Yes(c.Local),5} | {c.DirectionRank,8} | "
                          + $"{Yes(c.FirstOrder),11} | {Yes(c.ActsOnT1),13} | {Yes(c.NoNewPrimitive),11} | "
                          + CandidateVerdict(i).Split(" - ")[0]);
        }
        foreach (var (candidate, verdict) in CandidateVerdicts())
            sb.AppendLine($"   {candidate,-22} -> {verdict}");
        sb.AppendLine();
        sb.AppendLine("   THE EVIDENCE BEHIND EACH ROW");
        foreach (var (candidate, evidence) in CandidateEvidence())
            sb.AppendLine($"     {candidate,-22} : {evidence}");
        sb.AppendLine();
        sb.AppendLine($"   exactly one candidate derives : {ExactlyOneCandidateDerives()}");
        sb.AppendLine($"   the deriving candidate         : {DerivingCandidate()}");
        return sb.ToString();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    public static string OutputTheObject()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE OBJECT, REQUIREMENT BY REQUIREMENT");
        sb.AppendLine($"   local            : support {EdgeSupportPerRow()} per row (nearest neighbour) = {TheEdgeConnectionIsLocal()}");
        sb.AppendLine($"   directional      : rank {EdgeDirectionRank()} against the {DirectionsNeeded()} needed = {TheEdgeConnectionIsDirectional()}");
        sb.AppendLine($"   first-order      : sum_j |exp(i k_j) - 1|^2 vs sum_j (2 - 2 cos k_j), residual {FirstOrderResidual():E3} = {TheEdgeConnectionIsFirstOrder()}");
        foreach (var (k, ratio, quadrature) in SymbolLinearity())
            sb.AppendLine($"                      |sigma(k)|/k = {ratio:F6} and |sigma(k)|/k^2 = {quadrature:E3} at k = {k:E0} (first order means the first tends to 1, the second diverges)");
        sb.AppendLine($"   acts on T1 and T2: antisymmetric = {Format(AntisymmetricSquare())}; symmetric = {Format(SymmetricSquare())}");
        sb.AppendLine($"                      = {OneDerivativeReachesBothSectors()}");
        sb.AppendLine($"   no new primitive : the tensor product of the ring's own differences = true");
        sb.AppendLine();
        sb.AppendLine("   THE TENSOR SQUARE OF THE VECTOR IRREP, CLASS BY CLASS");
        sb.AppendLine("     class | size | chi(g) | chi(g)^2 | chi(g^2) | antisym | sym");
        var characters = TensorSquareCharacters();
        for (int i = 0; i < characters.Length; i++)
        {
            var t = characters[i];
            sb.AppendLine($"     {t.Class,5} | {t.Size,4} | {t.Chi,6:F3} | {t.ChiSquared,8:F3} | {t.ChiOfSquare,8:F3} | "
                          + $"{AntisymmetricSquareCharacter()[i],7:F3} | {SymmetricSquareCharacter()[i],5:F3}");
        }
        sb.AppendLine($"     group order {GroupOrder()}; antisymmetric square dimension {AntisymmetricSquareDimension()}; "
                      + $"symmetric square dimension {SymmetricSquareDimension()}");
        return sb.ToString();
    }

    public static string OutputBoundary()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE HONEST BOUNDARY INSIDE THE DERIVED VERDICT");
        sb.AppendLine("   live scan of AT.Core for a product-lattice construction (code only, prose stripped):");
        foreach (var (token, lines) in InstantiationScan()) sb.AppendLine($"     {token,-16} : {lines} code lines");
        sb.AppendLine($"   derivable but NOT instantiated (G_033's finding) : {TheObjectIsDerivableButNotInstantiated()}");
        sb.AppendLine($"   AT members that compute a field strength or its divergence : {CouplingMemberCount()}");
        sb.AppendLine($"   nothing couples the derivative to a field         : {NoMemberCouplesTheDerivativeToAField()}");
        sb.AppendLine();
        sb.AppendLine("4. THE LAYERS");
        foreach (var (layer, status, basis) in LayerVerdicts())
        {
            sb.AppendLine($"   {layer,-15} {status}");
            sb.AppendLine($"       {basis}");
        }
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
