# Y_QM_002 - Result

**Audit:** ResearchY-QM_002 - Unitary Correspondence Audit
**Verdict:** **PARTIAL**
**Tests:** 8/8 PASSED

## Answer

**PARTIAL - no AT flow has both properties Schrodinger evolution needs.** Schrodinger needs (a) `|m| = 1` for every
mode and (b) a phase advance **linear** in the mode momentum. Of the four flows carried, **1 is unitary** (the Cayley
form) and **1 has the clean dispersion** (the exact flow) - **and they are different flows**, measured.

## The three measures

| measure | verdict | basis |
|---|---|---|
| **norm conservation** | **ANALOGOUS** | Cayley `|m| = 1` on all 48 channels to **2.220E-016** |
| **mode occupation** | **ANALOGOUS** | circulant, hence diagonal; constant under the unitary flow; equivalent to the first measure |
| **phase evolution** | **REFUTED** | the symbol is **sin δ**, not δ; zone edge stationary; **21 of 42** occupied modes past the fold |

## Norm conservation (worst &#124;&#124;m&#124; − 1&#124;)

| flow | worst deviation | at ε | channel |
|---|---|---|---|
| **unitary (Cayley)** | **2.220E-016** | 1E-002 | 15 |
| centred (skew) | 4.988E-003 | 1E-001 | 24 |
| forward difference | 2.000E-001 | 1E-001 | 48 |
| exact flow | 1.813E-001 | 1E-001 | 48 |

## The three deviations of the unitary flow

| source | magnitude | ε-order | removable by smaller steps |
|---|---|---|---|
| **Cayley generator convention** | **1.999999** (→ 2) | none | **NO** |
| **time discretisation** | **3.333331E-007** at ε = 1E-3 | **2** (coefficient 1/3) | yes |
| **lattice symbol sin δ vs δ** | **0.363380** = 1 − 2/π at channel 24 | none | **NO** |

| ε | discretisation gap | lattice gap | zone edge stationary |
|---|---|---|---|
| 1E-003 | 3.333331E-007 | **0.363380** | True |
| 1E-002 | 3.333133E-005 | **0.363380** | True |
| 1E-001 | 3.313475E-003 | **0.363380** | True |

## The fold

| finding | value |
|---|---|
| folding (peak) channel | **24** (δ = π/2) |
| zone-edge advance | **2E-019** - the highest mode does not advance at all |
| adjacent channel pairs | **23 ordered, 24 inverted** (of 47) |
| **occupied modes at or above the fold** | **21 of 42** (channels 25–31, 33–39, 41–47) |

## The sector mixing (2000 steps, ε = 1E-3)

| flow | phase before | phase after | amplitude before | amplitude after |
|---|---|---|---|---|
| **unitary (Cayley)** | **9.246E-015** | **0.6392** | 1.005011 | 0.775567 |
| centred | 9.246E-015 | 0.8510 | 1.005011 | 0.535627 |
| forward difference | 9.246E-015 | 0.3414 | 1.005011 | 0.351792 |
| exact flow | 9.246E-015 | 0.3414 | 1.005011 | 0.351771 |

## Notes

**The factor two is a measurement rather than an error, and it is the sharpest single number here.** The Cayley form
`(1 + iεs)/(1 − iεs)` equals `exp(2i arctan(εs))`, so the repository's Cayley update **unitarises twice** the skew
generator the other flows advance - its phase advance is `2ε sin δ` against their `ε sin δ`. Since G_066 and G_067
compared flows at equal ε, that factor is a **convention in the flow's definition with a measurable consequence**, and
it is now on the record.

**Two of the audit's own first assertions were wrong and are recorded.** The dispersion table's first version compared
the Cayley advance against the symbol *without* dividing out the flow's own generator convention, which reported a
100 % deviation at every channel and hid the ε² behaviour underneath; and the discretisation order's first version had
its log-ratio inverted and returned **−2.0**. Both are fixed in the core and the corrected values are the ones quoted.

**No group-G change.** The group-G consistency counts and the `TemporalIndependenceAudit` registry are unchanged; the
G_027 (literal-verdict), G_033 (substrate) and G_035 (registry) scanners were re-run after the new core was added.
