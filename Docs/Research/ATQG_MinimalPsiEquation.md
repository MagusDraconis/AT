# AT-QG Phase 44 — Minimal ψ Field Equation

**Program:** AT-QG (Unification)
**Phase:** 44 — the simplest dynamics consistent with observed ψ effects
**Status:** COMPLETED — 3/3 xUnit tests pass (135/135 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

Determine the simplest dynamics for the ψ (spin-2) primitive. Classify: DERIVED / PREFERRED / POSTULATED.

---

## 2. The minimal form (ATQG440)

The simplest ψ dynamics is the **massless spin-2 wave equation** (Fierz-Pauli / linearized Einstein):

□ψ_μν = 0,   ∂^μ ψ_μν = 0 (transverse),   ψ^μ_μ = 0 (traceless)   →   **2 helicities**,

propagating at light speed, reducing to linearized GR in the weak-field limit — exactly matching the observed
two-polarization gravitational waves.

The generating action is the quadratic (Weyl) action S = ∫ ψ_μν □ ψ^μν.

---

## 3. Classification (ATQG441)

| question | answer |
|---|---|
| DERIVED from AT? | **no** (ψ is a new primitive) |
| form PREFERRED? | **yes** (unique massless spin-2) |
| equation POSTULATED? | **yes** (new input for the new primitive) |

**Classification: POSTULATED** — with a PREFERRED form.

---

## 4. Two-layer status (ATQG442)

- **PREFERRED (form):** the massless spin-2 wave equation is the **unique** ghost-free, Lorentz-invariant,
  massless spin-2 theory, matching the observed light-speed, two-polarization gravitational waves.
- **POSTULATED (status):** ψ is a new primitive (QG23/24/37), so its equation of motion is a new input, not
  derived from AT's scalar sector.

---

## 5. Conclusion

The minimal tensor extension of AT is a **massless spin-2 field with the Fierz-Pauli wave equation** — **one new
primitive, one new equation, uniquely fixed by observation**. This is the final step of the QG arc: ψ's dynamics
cannot be derived (it is a new primitive), but among all possible dynamics its form is uniquely preferred by
Lorentz invariance, masslessness, and spin-2 (the observed GWs).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG440 `ATQG440_MinimalForm` | PASS (2 helicities, light speed, linearized GR) |
| ATQG441 `ATQG441_Classification` | PASS (POSTULATED, preferred form) |
| ATQG442 `ATQG442_TwoLayerConclusion` | PASS (one primitive, one equation) |

Code: `AT.Core/ResearchXH/MinimalPsiEquation.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase44_MinimalPsiEquationTests.cs`.
