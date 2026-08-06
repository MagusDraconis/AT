namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Evaluates candidate origins of the antisymmetric coupling J = [[0,1],[-1,0]].
///
/// TQM-151: Origin of the Antisymmetric Coupling
/// </summary>
public static class RotationOriginModel
{
    public static List<SymplecticStructure.CouplingOrigin> EvaluateOrigins()
    {
        return new List<SymplecticStructure.CouplingOrigin>
        {
            // H1: Norm conservation → antisymmetry → J
            new SymplecticStructure.CouplingOrigin(
                "Norm Conservation",
                "d/dt(u²+v²)=0 ⇒ M^T=-M. Simplest 2×2 antisymmetric = J.",
                true, true,
                "BEST: Norm conservation FORCES antisymmetry. J is the unique (up to scale) 2×2 antisymmetric matrix."),

            // H2: Energy conservation → Hamiltonian structure → symplectic J
            new SymplecticStructure.CouplingOrigin(
                "Energy Conservation",
                "dE/dt=0 with E=½(u·L_Q·u+v·L_Q·v) ⇒ Hamiltonian form requires J.",
                true, true,
                "Equivalent to norm conservation for quadratic energy. J is the canonical symplectic form."),

            // H3: Rotational SO(2) symmetry
            new SymplecticStructure.CouplingOrigin(
                "SO(2) Rotational Symmetry",
                "Invariance under (u,v)→(u·cosθ-v·sinθ, u·sinθ+v·cosθ) ⇒ generator J.",
                true, true,
                "J is the Lie algebra generator of SO(2). Rotational symmetry ⇔ J."),

            // H4: Graph topology → J
            new SymplecticStructure.CouplingOrigin(
                "Graph Topology",
                "J does NOT follow from graph adjacency structure alone.",
                false, false,
                "FAILS: Graph topology determines L_Q (symmetric), not M (antisymmetric part)."),

            // H5: Amplitude-phase duality
            new SymplecticStructure.CouplingOrigin(
                "Amplitude-Phase Duality",
                "Writing complex amplitude A·e^{iθ} naturally produces J in the equations of motion.",
                true, true,
                "Equivalent to complex representation. Circular motion in (u,v) = J·[u,v]."),

            // H6: J is fundamental
            new SymplecticStructure.CouplingOrigin(
                "J is Fundamental",
                "J cannot be derived from L_Q alone. It is an independent postulate.",
                false, false,
                "PARTIALLY TRUE: J requires an additional principle (norm conservation). L_Q alone is insufficient."),
        };
    }

    /// <summary>
    /// Demonstrate that norm conservation forces antisymmetry.
    /// </summary>
    public static (bool conserved, double drift) VerifyNormConservation(int Q = 10, int steps = 100)
    {
        var L = new double[Q, Q];
        for (int i = 0; i < Q; i++)
        {
            L[i, i] = 2;
            if (i > 0) L[i, i - 1] = -1;
            if (i < Q - 1) L[i, i + 1] = -1;
        }

        var rng = new Random(42);
        var u = new double[Q]; var v = new double[Q];
        for (int i = 0; i < Q; i++) { u[i] = rng.NextDouble() - 0.5; v[i] = rng.NextDouble() - 0.5; }

        double n0 = u.Sum(x => x * x) + v.Sum(x => x * x);
        double dt = 0.01;

        for (int t = 0; t < steps; t++)
        {
            var du = new double[Q]; var dv = new double[Q];
            for (int i = 0; i < Q; i++)
            {
                double sumV = 0, sumU = 0;
                for (int j = 0; j < Q; j++) { sumV += L[i, j] * v[j]; sumU += L[i, j] * u[j]; }
                du[i] = sumV; dv[i] = -sumU;
            }
            for (int i = 0; i < Q; i++) { u[i] += dt * du[i]; v[i] += dt * dv[i]; }
        }

        double nf = u.Sum(x => x * x) + v.Sum(x => x * x);
        double drift = Math.Abs(nf - n0) / Math.Max(n0, 1e-10);
        return (drift < 0.01, drift);
    }
}
