# TQM-QG Phase 38 — Origin of Finite-Density Saturation

**Program:** TQM-QG (Unification)
**Phase:** 38 — why do Q-events saturate at a critical density?
**Status:** COMPLETED — 3/3 xUnit tests pass (117/117 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

QG36 derived the TRM profile from Poisson saturation. Here we ask WHY Q-events saturate at a critical density.
Classify: DERIVED / PREFERRED / IMPORTED.

---

## 2. Mechanism census (TQMQG380)

The five candidate mechanisms — occupancy limits, update conflicts, exclusion principles, branching congestion,
temporal tick capacity — all reduce to **one root**: a Q-event is a **discrete tick** (QG29), and a discrete
counting measure ρ cannot be subdivided. So there is necessarily a maximal density. **5/5 mechanisms are the same
discrete fact; no new primitive is needed.**

---

## 3. Existence vs value (TQMQG381)

| aspect | status |
|---|---|
| EXISTENCE of a critical density | **DERIVED** (discreteness ⇒ max density) |
| VALUE of ρ_c (equivalently r_c) | **IMPORTED/supplied** (QG14: bounds, no native cutoff) |

The discreteness of Q-events FORCES a maximal density — saturation exists by construction. But the numerical
ρ_c is not derivable; it is a supplied parameter, exactly as QG14 concluded for the Planck cutoff.

---

## 4. Classification (TQMQG382)

**DERIVED** (with an imported scale).

- **DERIVED (existence)**: a discrete counting measure cannot be subdivided, so a maximal density is forced.
- **IMPORTED (value)**: ρ_c is supplied, consistent with QG14 (bounds but no native cutoff value).
- The MECHANISM is derived; only the SCALE is imported. Saturation is not a hand-inserted assumption.

---

## 5. Conclusion

Finite-density saturation is **DERIVED** from the discrete nature of Q-events — it is the statement that a
counting measure of pointlike ticks has a maximal density, an occupancy limit built into the primitives. Only the
numerical scale ρ_c (equivalently r_c) is imported. This completes the saturation chain: Q-event discreteness →
finite-density saturation (QG38) → Poisson regular-core profile (QG36) → regular black holes, while the tensor ψ
remains the one genuinely new primitive (QG37).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG380 `TQMQG380_MechanismCensus` | PASS (5/5 discrete root) |
| TQMQG381 `TQMQG381_ExistenceVsValue` | PASS (existence derived, value imported) |
| TQMQG382 `TQMQG382_Classification` | PASS (DERIVED) |

Code: `TQM.Core/ResearchXH/SaturationOrigin.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase38_SaturationOriginTests.cs`.
