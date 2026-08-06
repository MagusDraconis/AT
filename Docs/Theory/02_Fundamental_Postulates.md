# 2. Fundamental Postulates

## The Irreducible Foundation of TQM

After TQM-154, the TQM framework rests on **4 irreducible postulates**.

---

## Postulate 1: Q Exists

**Statement**: Topological charge quanta Q exist. Each Q has a spatial position x_i and a phase θ_i. Q charges interact pairwise with coupling strength J_ij = f(|x_i - x_j|). Q is quantized, conserved, and indivisible.

**Status**: ASSUMED (irreducible).

**What it derives**:
- Interaction graph: nodes = Q charges, edges = interacting pairs
- Graph Laplacian L_Q = D - A
- Hilbert space: eigenvectors of L_Q form an orthonormal basis
- Eigenmodes: v_k with eigenvalues λ_k
- Information species: stable eigenmodes
- Darwinian evolution: species reproduce, compete, evolve on L_Q
- Physical observables: m_eff, E, Δ, ξ, D, C

**Derivation path**: Q → interaction graph → L_Q → Hilbert space → everything else.

**Confidence**: HIGH. L_Q is a mathematical identity given Q and interaction topology.

**Limitations**: Q must form a locally connected graph for the full hierarchy to emerge. Random and scale-free graphs lack discrete eigenmodes.

**Source**: TQM-117 through TQM-122 (Q theory), TQM-142 (Q → L_Q), TQM-143 (geometry universality).

---

## Postulate 2: Reversible Dynamics

**Statement**: The dynamics of the Q-network state are reversible. The norm of the state vector ||ψ||² is conserved in time.

**Status**: ASSUMED (mathematically equivalent to unitarity).

**What it derives**:
- Antisymmetric coupling J = [[0,1],[-1,0]] (from M^T = -M)
- Complex structure: J² = -I → J acts as the imaginary unit i
- Schrödinger equation: i∂ψ/∂t = L_Q ψ
- Unitary evolution: ψ(t) = exp(-iL_Q t) ψ(0)
- Stationary states: ψ_k(t) = exp(-iλ_k t) v_k
- Norm conservation: d/dt ||ψ||² = 0

**Derivation**:
For linear evolution dψ/dt = M·ψ:
d/dt ||ψ||² = ψ†(M + M†)ψ = 0 ∀ψ ⇒ M† = -M (anti-Hermitian).

For real M: M^T = -M (antisymmetric). The simplest 2×2 antisymmetric matrix is J. Combined with L_Q: M = J ⊗ L_Q gives ∂u/∂t = L_Q v, ∂v/∂t = -L_Q u, which with ψ = u + iv gives i∂ψ/∂t = L_Q ψ.

**Confidence**: HIGH. Mathematical identity: reversibility ⇔ anti-Hermitian ⇔ unitary.

**Limitations**: Does NOT determine which of the mathematically equivalent dynamics (diffusion ∂u/∂t = -L_Q u vs. Schrödinger i∂ψ/∂t = L_Q ψ) nature chooses. The choice is the postulate.

**Source**: TQM-149 (Schrödinger), TQM-150 (origin of i), TQM-151 (origin of J), TQM-152 (norm irreducibility).

---

## Postulate 3: Born Rule

**Statement**: The probability of observing a measurement outcome corresponding to state |φ⟩ when the system is in state |ψ⟩ is P = |⟨φ|ψ⟩|².

**Status**: ASSUMED. Uniquely selected by additivity (Gleason's theorem, 1957), but additivity itself is an assumption.

**What it derives**:
- Probability interpretation of the wavefunction
- Measurement statistics
- Expectation values ⟨A⟩ = ⟨ψ|A|ψ⟩

**Uniqueness proof** (Gleason, 1957):
Any probability measure on a Hilbert space of dimension ≥ 3 that is additive for orthogonal projectors must be of the form P = Tr(ρ·E). For pure states: P = |⟨φ|ψ⟩|².

Only exponent 2 satisfies P_0 + P_1 = 1 for all superpositions.

**Confidence**: HIGH for uniqueness (Gleason's theorem). MEDIUM for physical necessity (additivity is a classical probability axiom applied to quantum systems).

**Limitations**: Gleason's theorem requires additivity for orthogonal projectors. Why should quantum probabilities be additive in this way? The Born rule is mathematically unique given additivity, but additivity is not derived.

**Source**: TQM-153.

---

## Postulate 4: Measurement

**Statement**: A measurement on a quantum system produces a single definite outcome. The post-measurement state is the eigenstate corresponding to the observed eigenvalue.

**Status**: ASSUMED. This is the measurement problem — unsolved in all formulations of quantum mechanics since 1926.

**What is partially explained**:
- Decoherence: off-diagonal terms of ρ_S decay when coupled to environment
- Pointer states: eigenstates of the interaction Hamiltonian become stable
- Born statistics emerge on the diagonal of ρ_S

**What is NOT explained**:
- Why ONE outcome occurs (the "and" → "or" transition)
- How a particular outcome is selected
- The mechanism of wavefunction collapse

**Confidence**: LOW. This is the deepest unsolved problem in quantum foundations. No theory (TQM, Copenhagen, many-worlds, Bohmian, etc.) has provided a universally accepted solution.

**Source**: TQM-154.

---

## Postulate Count Comparison

| Framework | Postulates | Notes |
|-----------|-----------|-------|
| Standard QM | ~5 | Hilbert space, operators, Schrödinger, Born, measurement |
| TQM | 4 | Q exists, reversible dynamics, Born rule, measurement |
| TQM advantage | Hilbert space + Schrödinger DERIVED from postulates 1-2 | |

---

## What Is NOT Postulated (Derived)

| Structure | Derivation |
|-----------|-----------|
| Hilbert space | L_Q eigenvector basis |
| Graph Laplacian L_Q | Q interaction topology |
| Eigenmodes v_k | L_Q · v_k = λ_k · v_k |
| Complex structure J | Norm conservation → M^T = -M |
| Imaginary unit i | J² = -I → J acts as i |
| Schrödinger equation | J ⊗ L_Q → i∂ψ/∂t = L_Q ψ |
| Physical observables | λ_k → m_eff, E, Δ, ξ, D, C |
| Information species | Stable eigenmodes of L_Q |
| Darwinian evolution | Species + resources + asymmetry |
| Fitness law w = r/c | Resource-constrained growth |

---

*TQM-155: Fundamental Postulates. August 2026.*
