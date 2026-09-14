# Y_G_050 - Result

**Audit:** ResearchY-G_050 - Kernel Structure Audit
**Verdict:** **DERIVED**
**Tests:** 6/6 PASSED

## Answer

The 53 kernel directions are the **phase sector** of the organisation: one hidden **quadrature** per populated doublet,
**both** quadratures of each **empty** channel, and the alternating mode - **47 + 5 + 1 = 53**.

## Measurements

| quantity | value |
|---|---|
| hidden Fourier modes / visible | **53 / 42** (= 95) |
| half-hidden doublet channels | **42** |
| empty channels = doubly-hidden channels | **5**, identical sets (**True**) |
| no Fourier mode split anywhere | **True** |
| separated in frequency | **False** |
| clock-signature vs frequency correlation | **-0.0279** |
| every hidden mode has all three signatures | **True** |

## Two refused hypotheses

1. **"Whole doublets hidden"** (the dihedral argument) - refuted by the **42 half-hidden** doublets; the correct
   argument is the **circulant** one (translation invariance forbids mixing).
2. **"Short-wavelength sector"** - refuted: hidden and visible modes are **interleaved**, correlation ≈ 0.

## Reconciliation with G_040

G_040's **47** is the generic loss (47 orientations, no empty channel). This state adds **5** empty channels' worth:
**47 + 5 + 1 = 53**. Same structure, different state.
