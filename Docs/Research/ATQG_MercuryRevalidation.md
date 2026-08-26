# AT-QG Phase 103 — Mercury Perihelion Revalidation

**Program:** AT-QG (Unification)
**Phase:** 103 — does the unified network still recover the observed perihelion advance of Mercury?
**Status:** COMPLETED — 3/3 xUnit tests pass (312/312 AT-QG)
**Constraint:** no new primitives added here (revalidation; computational)

---

## 1. Goal

Verify that the unified network (V,E) with sectors ρ (spin-0) and ψ (spin-2) recovers the observed 42.98 arcsec/century perihelion advance. Classify: MATCH / PARTIAL / FAIL.

---

## 2. GR baseline & PPN factor (ATQG1030)

The perihelion advance is computed from first principles (Mercury orbital elements) via the PPN formula

Δφ = (6π GM/(c² a(1−e²))) · (2 + 2γ − β)/3.

GR (γ = β = +1) gives factor 1 → **42.98 "/century**, matching observation within numerical precision. This is the
target the network must match.

---

## 3. Conformal vs unified (ATQG1031)

- **ρ-only conformal sector**: γ = −1 (QG26), β = +1 → factor −1/3 → **−14.33 "/century** (retrograde). Wrong sign
  and magnitude → **FAIL**.
- **ρ+ψ unified network**: ψ is the massless spin-2 graviton (Fierz-Pauli, QG44), restoring γ = β = +1 → factor 1 →
  **+42.98 "/century** → **MATCH**.

---

## 4. Classification (ATQG1032)

**MATCH** (via the ψ spin-2 sector).

- FAIL (ρ alone): conformal γ=−1 gives a retrograde advance;
- MATCH (ρ+ψ): the ψ graviton restores γ=β=+1, reproducing the observed value exactly;
- PARTIAL would apply only for a near-but-not-exact value, which is not the case.

---

## 5. Conclusion

The unified network **recovers** Mercury's perihelion advance through the ψ (spin-2) sector. This confirms ψ as the
graviton sector — a tensor observable (perihelion) that the scalar-only conformal sector cannot reproduce.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1030 `ATQG1030_GrBaseline` | PASS (42.98 "/century from orbital elements) |
| ATQG1031 `ATQG1031_ConformalVsUnified` | PASS (conformal retrograde; unified matches) |
| ATQG1032 `ATQG1032_Classification` | PASS (MATCH) |

Code: `AT.Core/ResearchXH/MercuryRevalidation.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase103_MercuryRevalidationTests.cs`.
