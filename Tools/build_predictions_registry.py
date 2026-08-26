# -*- coding: utf-8 -*-
"""Generate Docs/ATQG_Predictions.md and Docs/ATQG_Predictions.json.

The IMMUTABLE registry of the three pre-registered predictions (QG190/191/192).
Run:  python Tools/build_predictions_registry.py
Rule: no future phase may modify a registered prediction. Only CONFIRMED,
DISFAVORED, FALSIFIED may be added later (as the "outcome" field).
"""
import json, os, sys

sys.stdout.reconfigure(encoding='utf-8')
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MD = os.path.join(ROOT, "Docs", "ATQG_Predictions.md")
JSON = os.path.join(ROOT, "Docs", "ATQG_Predictions.json")

REGISTRY = [
    {
        "id": "P1",
        "name": "106 GeV resonance",
        "derivation_phase": "QG132 (derived) / QG190 (frozen)",
        "formula": "M_106 = 7·MZ/6 = 7·15.198 GeV; window = M_106 ± spacing/2, spacing = MZ/6 = 15.20 GeV",
        "inputs": "D96 ladder radii 6.0–17.333 (QG121/128), Z-anchor calibration MZ/6 (QG130), missing-rung rule (QG132)",
        "frozen_value": "106.39 GeV (central); window 98.79–113.99 GeV (stated 99–114 GeV)",
        "uncertainty": "±7.60 GeV (half the mean rung spacing); boson-anchor family agrees within 0.74% (QG133)",
        "falsification": "No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED)",
        "outcome": None,
    },
    {
        "id": "P2",
        "name": "0νββ m_ββ",
        "derivation_phase": "QG179 (derived) / QG191 (frozen)",
        "formula": "m_ββ = |Σ U_ei²·m_i| = |m1·c12²·c13² + m2·s12²·c13² + m3·s13²·e^(−2iδ)|",
        "inputs": "QG167 PMNS (s12 = √(#d/(Σm+#g)) = 0.5497, s13 = √(occ0/(2Σm)) = 0.1451, δ_ν = 66.4°), QG172 masses (m1=0, m2=8.72e-3, m3=4.94e-2 eV, normal ordering), QG179 Majorana (α2=α3=0)",
        "frozen_value": "m_ββ = 2.02 meV (computed 2.0222 meV)",
        "uncertainty": "±10% (1.8–2.2 meV range); dominated by m2·s12²·c13² = 2.52 meV, robust to CP phase",
        "falsification": "Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES)",
        "outcome": None,
    },
    {
        "id": "P3",
        "name": "Sector-ladder spectrum",
        "derivation_phase": "QG128-132 (derived) / QG192 (frozen)",
        "formula": "E_rung = radius·(MZ/6); unit quantum ΔE = MZ/6 = 15.20 GeV, top quantum = 1.333·15.20 = 20.26 GeV",
        "inputs": "D96 ladder radii (QG121/128), 8 thresholds (QG127), Z-anchor scale (QG130), missing-rung rule (QG132)",
        "frozen_value": "9 resonances: 106.39 (primary) → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities unit ×10 (0.909) + top ×1; width scale 15.20 GeV",
        "uncertainty": "±5% per rung; boson-anchor family agrees within 0.74% (QG133)",
        "falsification": "A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES)",
        "outcome": None,
    },
]


def outcome_text(outcome):
    if outcome is None:
        return "PENDING (no outcome yet)"
    return str(outcome)


# ── JSON (machine-readable) ───────────────────────────────────────────────────────
with open(JSON, "w", encoding="utf-8") as f:
    json.dump({
        "title": "AT-QG Prediction Registry",
        "immutable": True,
        "rule": "No future phase may modify a registered prediction. Only CONFIRMED, DISFAVORED, FALSIFIED may be added later.",
        "locked_by": "AT-QG Phase 193",
        "predictions": REGISTRY,
    }, f, ensure_ascii=False, indent=2)
    f.write("\n")

# ── Markdown (human-readable) ────────────────────────────────────────────────────
with open(MD, "w", encoding="utf-8") as f:
    f.write("# AT-QG Prediction Registry\n\n")
    f.write("**Immutable.** Locked by AT-QG Phase 193 (Prediction Registry Lock).\n\n")
    f.write("> **Rule:** No future phase may modify a registered prediction. Only **CONFIRMED**, "
            "**DISFAVORED**, **FALSIFIED** may be added later (as the outcome).\n\n")
    f.write("Machine-readable twin: `Docs/ATQG_Predictions.json`.\n\n")
    f.write("---\n\n")
    for p in REGISTRY:
        f.write(f"## {p['id']} — {p['name']}\n\n")
        f.write(f"| Field | Value |\n|-------|-------|\n")
        f.write(f"| Derivation phase | {p['derivation_phase']} |\n")
        f.write(f"| Formula | `{p['formula']}` |\n")
        f.write(f"| Inputs | {p['inputs']} |\n")
        f.write(f"| Frozen value | **{p['frozen_value']}** |\n")
        f.write(f"| Uncertainty | {p['uncertainty']} |\n")
        f.write(f"| Falsification condition | {p['falsification']} |\n")
        f.write(f"| Outcome | {outcome_text(p['outcome'])} |\n\n")
        f.write("---\n\n")

print(f"Wrote {MD}")
print(f"Wrote {JSON}")
