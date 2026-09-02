# Y_NP_033_Result.md — ResearchY-NP_033 D96 Ensemble Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_033_Tests.cs`
**Run:** 2026-09-02
**Result:** ✅ 9/9 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_033"`

---

## Summary

**Question:** Can thermodynamic behavior emerge from an ENSEMBLE of D96 systems even
though a single D96 system has no temperature? Do Temperature, Boltzmann weights, or
Bose-like occupations emerge statistically?

**Verdict: PARTIAL — the ensemble DOES generate statistical temperature, Boltzmann
weights, and Bose-like occupations** (occupation exchange + entropy maximization over
the D96 mode set), but the observed blackbody radiation does NOT emerge (mode-set
obstruction persists). The hypothesis "structure single-D96 / thermodynamics
ensemble-D96" is CONFIRMED in its statistical part.

## Step 1 — Single D96 ring: no temperature (NP_030)

One fixed configuration has no statistical temperature. Canonical branching μ = 2 is
anti-thermal (growth).

## Step 2 — Two coupled D96 rings: zeroth law

Total entropy peaks at equal split (equal β):
S(50/50) = 35.7306 > S(35/65) = 35.3600 > S(20/80) = 34.1459. Two D96 systems in
occupation contact equilibrate to a common statistical temperature.

## Steps 3–4 — Occupation exchange + entropy maximization → Bose occupations

Over the D96 mode set with conserved total energy, the max-entropy occupation is the
Bose distribution n_k = 1/(e^(βω_k) − 1):

- S_Bose(1) = 17.8653 beats the uniform (15.7097), linear (15.2101), and bottom-heavy
  (4.03) alternatives at the same energy.
- Boltzmann identity ln(n/(1+n)) = −βω holds EXACTLY over the D96 modes.
- Microcanonical occupation-exchange marginal is geometric:
  P(n+1)/P(n) = Q/(Q+M−2) (verified M=5,Q=3 → 0.5; M=10,Q=10 → 0.5556; M=100,Q=100 →
  0.5051).

Energy-temperature relation (monotone): E(0.5) = 73.05, E(1.0) = 12.59, E(2.0) = 1.13.

## Step 5 — Mode-set obstruction: radiation still fails

The ensemble thermalizes OCCUPATION over the FIXED D96 mode set ([4,4,87] in
[0.622, 3.98]) — it cannot change the frequencies:

- Octave energy is bimodal at every T: cold (T=0.3) → 94% in the low octave; hot
  (T=10) → 91% in the top octave. No T gives a broad mid-band Planck shape.
- Σω³/(e^ω−1) over D96 modes = 120.70 ≠ π⁴/15 = 6.494.
- No modes above ω_max = 3.98 → no Wien tail.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_033_SingleRingNoTemperature` | single D96 ring has no T (NP_030) | ✅ |
| `Y_NP_033_ZerothLaw` | two-ring contact peaks at equal β | ✅ |
| `Y_NP_033_BoseMaximizesEntropy` | Bose beats uniform/linear/bottom-heavy at fixed E | ✅ |
| `Y_NP_033_BoltzmannWeightIdentity` | ln(n/(1+n)) = −βω exact over D96 modes | ✅ |
| `Y_NP_033_MicrocanonicalGeometricMarginal` | P(n+1)/P(n) = Q/(Q+M−2) geometric | ✅ |
| `Y_NP_033_ModeSetObstructionPersists` | Σω³/(e^ω−1) = 120.7 ≠ π⁴/15; no tail | ✅ |
| `Y_NP_033_OctaveEnergyBimodal` | octave energy bimodal at every T | ✅ |
| `Y_NP_033_Classification` | EMERGENT / REFUTED / FALSIFIED flags | ✅ |
| `Y_NP_033_Run` | research report | ✅ |

## Conclusion

An ensemble of D96 systems DOES generate thermodynamic behavior statistically —
temperature (β = ∂S/∂E), Boltzmann weights (e^(−βω_k)), and Bose occupations
(n_k = 1/(e^(βω_k) − 1)) all emerge from occupation exchange plus entropy maximization,
exactly as in standard statistical mechanics. This confirms the statistical half of the
hypothesis: single D96 has no temperature, but ensemble-D96 develops one. However, the
observed blackbody radiation is NOT reproduced: the ensemble thermalizes the occupation
but cannot change the D96 mode set (top-heavy [4,4,87], capped at 3.98, Σω³/(e^ω−1) =
120.70 ≠ π⁴/15, no Wien tail). So "thermodynamics is ensemble-D96" is confirmed for the
occupation statistics and refuted for the radiation spectrum. No new primitive;
canonical AT unchanged.
