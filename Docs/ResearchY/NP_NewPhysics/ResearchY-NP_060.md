# ResearchY-NP_060 — Dark Energy Resource Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_060 (permanent)
**Title:** Dark Energy Resource Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_060.md`
**Depends on:** ResearchY-NP_055–NP_059 (the dark-energy arc), AT-QG QG89 (energy =
actualization rate), QG230 (Λ origin), QG234 (ΩΛ = I_occ/ln K), QG194 (matter = deficit),
NP_030 (no canonical temperature), NP_059 (energy = actualization rate BOUNDARY)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_060_Tests.cs`

---

## Purpose

The prior audits (NP_055–059) established ΩΛ = I_occ/ln K = 0.6839 is a DERIVED
information quantity whose energy reading is hosted (QG89 bridge). NP_060 asks the decisive
physical question: **does ΩΛ represent a physically extractable resource (can it perform
work), or only an informational state descriptor?** Program: (1) assume ΩΛ is an energy
reservoir; (2) define a hypothetical extraction process; (3) track energy / information /
entropy / actualization count; (4) test A) extractable work, B) state descriptor, C)
bookkeeping, D) hidden conserved resource; (5) compare with vacuum energy, entropy, free
energy, information measures. **Success criterion:** determine whether ΩΛ can perform work.
No new primitives; canonical AT unchanged.

---

## 1. Assume ΩΛ is an energy reservoir — what must be true?

For ΩΛ to be extractable (able to do work), it must possess **three** things:

| Requirement | ΩΛ = I_occ/ln K satisfies? |
|---|---|
| **an energy scale** (a dimensionful reservoir) | **NO** — ΩΛ is a dimensionless fraction of two nats |
| **a gradient** (something to extract *across*) | **NO** — it is a single snapshot number, no spatial/thermal gradient |
| **a temperature / free-energy channel** | **NO** — AT has no canonical temperature (NP_030) |

Work W = ∫ P dV (or −ΔF) has units of energy; a dimensionless fraction cannot do work
without a mechanism to convert nats → Joules — which is exactly the hosted QG89 bridge
(NP_058/059) plus the dimensionful anchors v, m_e and ħ, c.

---

## 2. A hypothetical extraction process

Suppose one tries to extract work from ΩΛ. The only channels available are:

```
(a) vacuum-energy channel   : dU = −p dV  (needs p = w·ρ, i.e. an energy density + EoS)
(b) information channel     : W = k_B T ln 2 per bit (Landauer/Szilard — needs a temperature)
(c) free-energy channel     : W = −ΔF,  F = U − TS  (needs U and T)
```

- **(a) is hosted.** AT does not derive p or ρ_Λ (NP_056: no equation of state). The
  vacuum-energy work channel is ΛCDM content, imported via QG230's Λ = 8πG·ρ_Λ.
- **(b) is absent.** AT has no derived temperature (NP_030: every candidate is anti-thermal
  or an order-only scalar). Without T, the Landauer/Szilard conversion nats → work is
  undefined.
- **(c) is undefinable.** F = U − TS requires U (energy — needs the QG89 bridge) and T
  (absent). AT derives neither, so no free energy exists to be minimized.

**Tracking the four quantities under a hypothetical extraction:**

| Quantity | Under extraction | Status |
|---|---|---|
| energy | unchanged — there is no energy reservoir to draw from | hosted/absent |
| information | I_occ = 0.7513 is a *function of the fixed ρ*, invariant | no flow |
| entropy | H = 0.3473 is also a function of the fixed ρ | no flow |
| actualization count | Σρ = 1 conserved; the deficit Σm = 0 conserved | conserved (a count) |

**Nothing flows.** ΩΛ has no reservoir to deplete, no gradient to exploit, no temperature
to convert information into work.

---

## 3. Test the four readings

| Test | Verdict |
|---|---|
| **A) extractable work** | **REFUTED.** No energy scale, no gradient, no temperature; nothing to extract. |
| **B) state descriptor only** | **YES.** ΩΛ is a snapshot order parameter of the count density ρ (NP_057). |
| **C) bookkeeping quantity** | **YES — identical to B** (the information partition I_occ + H = ln K). |
| **D) hidden conserved resource** | **NO.** The conserved quantities are Σρ = 1 and Σm = 0 — *counts*, not energy resources. A resource must be extractable; a conserved count has no energy content. |

**Determination: B = C. ΩΛ is a state descriptor / bookkeeping quantity, not a resource.**

---

## 4. The inversion — the extractable side is Ωm (matter), not ΩΛ

A key clarification from QG194: **matter is the deficit**, and the *deficit* is the
physical substance that gravitates and can do work:

```
ΩΛ = I_occ/ln K = (ln K − H)/ln K   ← the information SURPLUS (state descriptor)
Ωm = H/ln K     = realized entropy   ← the DEFICIT side (matter = ρ̄ − ρ, QG194)
```

The physically load-bearing (extractable, gravitating) side is **Ωm (the deficit)**, while
**ΩΛ (the surplus)** is the complementary information descriptor. So even *within* AT's own
ontology, ΩΛ is not the "resource" — matter is.

---

## 5. Compare with vacuum energy / entropy / free energy / information

| Quantity | Can it do work? | In AT? |
|---|---|---|
| **vacuum energy** (ρ_Λ, w = −1) | **YES** — d(ρV) = −p dV = +ρ dV: energy *grows* as it expands (the "free lunch") | HOSTED (QG230 Λ = 8πG·ρ_Λ, no derived EoS) |
| **entropy** (S) | **indirectly** — ΔS across a process → work, but needs T and a reservoir | DERIVED as a *count* (H = 0.3473), but no T to convert it |
| **free energy** (F = U − TS) | **YES, by definition** — W_max = −ΔF | UNDEFINABLE (no U, no T) |
| **information** (KL divergence) | **indirectly** — via Landauer/Szilard, needs T + mechanism | DERIVED as a number (0.7513), no conversion channel |

**ΩΛ belongs to the information column.** It is a DERIVED information measure that — unlike
vacuum energy or free energy — has no derived channel to perform work.

---

## Theorem

> **Theorem (NP_060).** ΩΛ = I_occ/ln K = 0.6839 cannot perform work; it is an
> informational STATE DESCRIPTOR (and bookkeeping quantity, B = C), not a physical resource.
> Proof: (1) Resource requirements (Section 1, verified): ΩΛ has no energy scale
> (dimensionless), no gradient (single snapshot number), and no temperature channel
> (NP_030). (2) Extraction channels (Section 2): vacuum work (a) is hosted (no derived p/ρ_Λ,
> NP_056); information work (b) needs T (absent); free-energy work (c) needs U and T (neither
> derived). (3) Tracking (Section 2, verified): under any hypothetical extraction, energy
> (none), information I_occ = 0.7513 (invariant function of ρ), entropy H = 0.3473
> (invariant), count Σρ = 1 and Σm = 0 (conserved counts) — nothing flows. (4) Readings
> (Section 3): A extractable work REFUTED; B state descriptor YES; C bookkeeping YES (= B);
> D hidden conserved resource NO (the conserved quantities are counts, not energy).
> (5) Inversion (Section 4): the extractable side is Ωm (matter = deficit, QG194), not ΩΛ
> (the surplus). (6) Comparison (Section 5): vacuum energy does work (hosted), free energy is
> undefinable, ΩΛ is a pure information measure with no conversion channel. Classification:
> ΩΛ as an information state descriptor DERIVED (NP_057); ΩΛ as a physical energy resource
> REFUTED; the vacuum-energy work channel CORRESPONDENCE (hosted, QG230); the conserved
> counts Σρ = 1 / Σm = 0 DERIVED but not resources. **Success criterion: ΩΛ cannot perform
> work — it merely describes the state of the system.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Enumerate resource requirements. (2) Construct extraction channels.
> (3) Track the four quantities. (4) Test A–D. (5) Locate the extractable side. (6) Compare.
> ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "ΩΛ does work like vacuum energy" | vacuum work needs an energy density + EoS (hosted); ΩΛ is a dimensionless fraction |
| "ΩΛ can be converted to work via information" | Landauer/Szilard needs a temperature; AT has none (NP_030) |
| "ΩΛ is free energy" | F = U − TS needs U (QG89 bridge) and T (absent) — undefinable |
| "ΩΛ is a hidden conserved resource" | the conserved quantities are Σρ = 1 and Σm = 0 — counts, not energy |
| "ΩΛ is the extractable side" | the extractable/gravitating side is Ωm = deficit (matter, QG194); ΩΛ is the surplus |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ΩΛ cannot perform work | a derived energy scale, gradient, or temperature channel that lets ΩΛ do work |
| ΩΛ is a state descriptor, not a resource | an observable that ΩΛ *depletes* or *drives* (a work process) |
| no information→work channel | a derived temperature (or free energy) converting I_occ → work |
| the extractable side is Ωm | a demonstration that ΩΛ (surplus), not Ωm (deficit), carries the work |

---

## 8. Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 as an information state descriptor | **DERIVED** (NP_057) |
| ΩΛ as a physically extractable resource | **REFUTED** (no scale/gradient/temperature) |
| vacuum-energy work channel | **CORRESPONDENCE** (hosted, QG230 Λ = 8πG·ρ_Λ) |
| conserved counts Σρ = 1 / Σm = 0 | **DERIVED** (but not energy resources) |
| the extractable/gravitating side | **Ωm** (matter = deficit, QG194), not ΩΛ |
| temperature / free energy | **BOUNDARY** (absent, NP_030) |

**Conclusion.** ΩΛ = I_occ/ln K = 0.6839 is an informational state descriptor (and
bookkeeping quantity) — it **cannot perform work**. It has no energy scale (dimensionless),
no gradient, and no temperature channel (NP_030), so none of the extraction channels
(vacuum work, information work, free energy) is available to it. The physically extractable
side of AT's ontology is Ωm (matter = deficit), not ΩΛ (the information surplus). The
vacuum-energy work channel is hosted ΛCDM content, imported via the QG89/Λ = 8πG·ρ_Λ bridge.
**ΩΛ merely describes the state of the system; it is not a resource.** No new primitive;
canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_060_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_060_ResourceRequirements` | ΩΛ dimensionless, no gradient, no temperature | ✅ |
| `Y_NP_060_VacuumWorkIsHosted` | d(ρV) = −p dV, w=−1 grows; no derived EoS | ✅ |
| `Y_NP_060_InformationWorkNeedsT` | Landauer W = k_B T ln 2; no T in AT | ✅ |
| `Y_NP_060_FreeEnergyUndefinable` | F = U − TS needs U and T | ✅ |
| `Y_NP_060_Tracking` | I_occ, H invariant; Σρ = 1, Σm = 0 conserved | ✅ |
| `Y_NP_060_Readings` | A refuted; B = C yes; D no | ✅ |
| `Y_NP_060_ExtractableSideIsMatter` | Ωm = deficit (matter), ΩΛ = surplus | ✅ |
| `Y_NP_060_Classification` | descriptor DERIVED; resource REFUTED; work hosted | ✅ |
| `Y_NP_060_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_060"`

---

## References

- ResearchY-NP_055–NP_059 (dark-energy arc), NP_030 (no canonical temperature), NP_059
  (energy = actualization rate BOUNDARY).
- AT-QG: QG89 (energy = actualization rate), QG230 (Λ origin, Λ = 8πG·ρ_Λ), QG234
  (ΩΛ = I_occ/ln K), QG194 (matter = deficit, Σm = 0), QG216 (Σρ = 1), QG228
  (I_occ = KL(ρ‖uniform)).
