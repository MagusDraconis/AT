# ResearchY — Hierarchical Index

**Program:** ResearchY — Wave Geometry Program
**Index version:** 1.0 (2026-08-28)
**Maintenance rule:** every investigation receives a permanent identifier on creation;
numbers within a group are never reused; new investigations extend the sequence within
their group. Groups are topics, not chronological order.

## Identifier Format

```
ResearchY-G_###        where G ∈ {A, B, C, D} and ### is the sequence in the group
```

## Group Registry

| Group | Folder | Topic | Planned investigations |
|---|---|---|---|
| A | `A_WaveFoundations/` | Wave Foundations | A_001 Wave Origin Audit · A_002 Difference Disturbance · A_003 Actualization Propagation |
| B | `B_CircularGeometry/` | Circular Geometry | B_001 Circular Closure · B_002 Origin of π · B_003 Origin of 2π |
| C | `C_SourceGeometry/` | Source Geometry | C_001 Center Audit · C_002 Radial Propagation |
| D | `D_ResonanceStructure/` | Resonance Structure | D_001 D96 Resonance Audit · D_002 Standing Wave Model |

## Investigation Registry

| ID | Title | Status | File | Depends on | Notes |
|---|---|---|---|---|---|
| ResearchY-A_001 | Wave Origin Audit | COMPLETE | `A_WaveFoundations/ResearchY-A_001.md` | — | Can Difference be read as a localized disturbance; do propagation, resonance, radius, circumference, π, 2π emerge naturally? |
| ResearchY-A_002 | Difference Disturbance Audit | COMPLETE | `A_WaveFoundations/ResearchY-A_002.md` | ResearchY-A_001 | What constitutes a Difference; which disturbance interpretation (perturbation / phase displacement / graph defect / occupancy disturbance / mode excitation) is best; can a single localized Difference generate propagation across C96? Verdict: C5 mode excitation best (|ψ_k|² = ρ_k exact); zero mode = undisturbed background. |
| ResearchY-A_003 | Actualization Propagation Audit (rev. 2) | COMPLETE | `A_WaveFoundations/ResearchY-A_003.md` | ResearchY-A_001, ResearchY-A_002, ResearchY-A_004 | Rev. 2 after A_004: what propagates (count ρ); carrier (tree + ring); local generation + global readout; μ^k = depth; branching NOT rewritable as wave (phase separate DOF); Z2/octaves/locking are spectral (graph), not propagation. Preferred: branching + spectral projection. |
| ResearchY-A_004 | Propagation Falsification Audit | COMPLETE | `A_WaveFoundations/ResearchY-A_004.md` | ResearchY-A_003 | Falsification attempt: is "Actualization = branching + spectral projection" unique or merely preferred? Tests alternatives A (branching), B (diffusion), C (wave), D (hybrid) for reproduction of [4,4,87], λ structure, and moments — no fitting, no targets, no new assumptions. Verdict: FALSIFICATION FAILED — conclusion UNIQUE (not merely preferred). |
| ResearchY-A_005 | Spectral Projection Origin | COMPLETE | `A_WaveFoundations/ResearchY-A_005.md` | ResearchY-A_003 rev2, ResearchY-A_004 | Why does branching project onto spectral modes? Verdict: spectral projection is DERIVED, not primitive — minimal origin is the actualization attractor (D): closure → graph C96 → Laplacian → unique eigenbasis → readout. A fundamental FAILS (3rd primitive); B closure PARTIAL (size ≠ structure); C resonance CIRCULAR. |
| ResearchY-B_001 | Circular Closure Audit | COMPLETE | `B_CircularGeometry/ResearchY-B_001.md` | ResearchY-A_001, A_002, A_004, A_005 | Is circular closure a necessary consequence of Difference → Actualization → Attractor → Graph → Laplacian → Eigenbasis? Do π and 2π emerge? Verdict: closure EMERGES (bounded dynamics → ring); 2π EMERGES (θ_N=2π minimal phase closure); π emerges in ROLE (C/D=π) but value remains boundary (QG291/QG196 unchanged). |
| ResearchY-B_002 | Origin of π Value Audit | COMPLETE | `B_CircularGeometry/ResearchY-B_002.md` | ResearchY-B_001 | Can the numerical value π=3.14159... emerge from the canonical framework, or only its role? Verdict: BOUNDARY — L is an integer matrix → spectrum is algebraic; π is transcendental → no finite canonical construction outputs π's value. Role emerges (C/D=π); value is irreducible boundary. QG291/QG196 confirmed + strengthened. |
| ResearchY-C_001 | Center Audit | COMPLETE | `C_SourceGeometry/ResearchY-C_001.md` | ResearchY-A_001…A_005, B_001, B_002 | Is a unique center/source present in Difference→Actualization? Verdict: center ABSENT in space (circulant symmetry eliminates any preferred site); EMERGENT as the branching root (generation-space source, ρ₀=1/S); zero mode is a DERIVED reference state (not a source). Propagation not radial (tree-local + global readout). |
| ResearchY-C_002 | Radial Propagation Audit | COMPLETE | `C_SourceGeometry/ResearchY-C_002.md` | ResearchY-C_001 | Can propagation in C96 be genuinely radial? Verdict: FAIL (not canonically radial). Radial propagation requires a derived origin; C96 is D96 vertex-transitive (identical shell profiles from every node; diameter 8 = N/(2K); shells 12/…/11; reflection symmetry). Shortest-path = radial shells (identical). Canonical spreading = tree-local (branching) + global (spectral readout), a non-radial hybrid. Radial structure is a gauge/coordinate choice, not invariant content. |
| ResearchY-D_001 | Standing Wave Audit | COMPLETE | `D_ResonanceStructure/ResearchY-D_001.md` | ResearchY-C_001, C_002 | Can standing waves exist on C96 without center-based geometry? Verdict: YES — Fourier modes are time-harmonic eigenfunctions of L (ψ=φ(n)cos ωt, ω=√λ), translation-invariant (node positions depend only on k). Geometric (pattern) vs spectral (frequency) are centerless faces of one object. Zero mode ω₀=0 uniform rest state; 47 Z2 pairs (42 doublets + 5 + 6 groups). Classification: HYBRID, center-free. |
| ResearchY-B_003 | Origin of 2π Audit | COMPLETE | `B_CircularGeometry/ResearchY-B_003.md` | ResearchY-B_002 | Can 2π emerge as a closure invariant without deriving π itself? Verdict: YES in role, NO in value. The full-cycle period is forced by the finite closed ring: R^N=identity, z_k^N=1 (roots of unity), θ_{k+N}≡θ_k — all algebraic, no π value. Classification: cycle closure DERIVED; phase periodicity EMERGENT; eigenmode rotations DERIVED; N=96 symmetry DERIVED; 2π role EMERGENT; 2π value BOUNDARY (transcendental, radian convention). Consistent with B_001 (role) + B_002 (value). |
| ResearchY-D_002 | Standing Wave Model | COMPLETE | `D_ResonanceStructure/ResearchY-D_002.md` | ResearchY-C_001, C_002, D_001 | Canonical standing wave model of C96: Ψ=Σ[a cos+b sin]cos(ωt), 95 positive Fourier modes + 1 zero mode; 47 Z2 pairs (94 real modes) + self-conjugate k=48 (λ=12) = 95; zero mode ω₀=0 uniform rest state; hybrid (spatial harmonics × spectral eigenvalues); closure-consistent (R^N=id, θ_{k+N}≡θ_k, z^N=1, algebraic spectrum). Classification: HYBRID (center-free). |
| ResearchY-D_003 | Resonance Observables Audit | COMPLETE | `D_ResonanceStructure/ResearchY-D_003.md` | ResearchY-D_002 | Can resonance alone generate physical observables? Verdict: resonance generates the SPECTRAL observables (DERIVED: mode occupation [4,4,87], occMom=1900.25, moments, span, Z2 pairs, zero-mode role, invariants) but NOT the PHYSICAL observables — sector mapping is EMERGENT (correspondence), dimensional values are BOUNDARY (calibration anchors v/m_e, fit 1/α_em). Resonance is the spectral source, not the complete generator. |
| ResearchY-D_004 | Sector Mapping Origin Audit | COMPLETE | `D_ResonanceStructure/ResearchY-D_004.md` | ResearchY-D_003 | Why do spectral quantities map to physical sectors? Verdict: three-layer origin. DERIVED structure (occupancies, moments, gaps, Z2 pairs are exact); EMERGENT sector assignment (supported correspondence, not unique); BOUNDARY dimensional values (calibration v/m_e, fit 1/α_em). Families are the DERIVED exception (octave bands = families, floor(log₂ span)+1=3, QG210). Correspondence is "supported, not unique". |

## Rules

1. IDs are permanent. A retired investigation keeps its ID; a new investigation never
   reuses a number.
2. An investigation must be registered here (new row) **before** its content file is
   finalized.
3. Dependencies are recorded in the "Depends on" column; the dependency graph must remain
   acyclic.
4. Status values: PLANNED · IN_PROGRESS · COMPLETE · SUPERSEDED · RETIRED.
5. Every investigation checks consistency against the canonical hierarchy
   Difference → Actualization → Inevitable Spectrum → Physics.
