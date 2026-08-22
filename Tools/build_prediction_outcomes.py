# -*- coding: utf-8 -*-
"""Generate Docs/TQMQG_PredictionOutcomes.md and Docs/TQMQG_PredictionOutcomes.json.

The prediction outcome dashboard — a single source of truth for the external
validation of the three registered predictions (QG202). Folds the immutable
registry (TQMQG_Predictions.json) together with the evidence audits
(QG188A/199/200/201).

Run:  python Tools/build_prediction_outcomes.py
"""
import json, os, sys

sys.stdout.reconfigure(encoding='utf-8')
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MD = os.path.join(ROOT, "Docs", "TQMQG_PredictionOutcomes.md")
JSON = os.path.join(ROOT, "Docs", "TQMQG_PredictionOutcomes.json")

OUTCOMES = [
    {
        "id": "P1",
        "name": "106 GeV resonance",
        "state": "PENDING",
        "frozen_value": "106.39 GeV central; window 98.79–113.99 GeV (stated 99–114 GeV)",
        "current_evidence": (
            "No confirmed signal in the 99–114 GeV window (QG188A INCONCLUSIVE, QG199). "
            "Classic low-mass scalar excesses persist at ~95 GeV (combined γγ 3.1σ = the 91.19 rung, "
            "not P1). CMS γγ 15–73 fb and ATLAS γγ 19–102 fb limits do not exclude P1; "
            "LEP2 114.4 GeV bound is SM-coupling only."
        ),
        "support_level": "NONE (inside window) — no excess at 106.39 GeV; window open",
        "last_audit": "QG199 (P1 evidence update)",
        "next_experiment": "HL-LHC 3000 fb⁻¹ diphoton (projected 1–3 fb sensitivity in 100–106 GeV)",
        "falsification": "No signal in statistically sensitive searches of the 99–114 GeV window (DISFAVORED/FALSIFIED)",
    },
    {
        "id": "P2",
        "name": "0νββ m_ββ = 2.02 meV",
        "state": "PENDING",
        "frozen_value": "m_ββ = 2.02 meV (computed 2.0222 meV); ±10% (1.8–2.2 meV)",
        "current_evidence": (
            "No experiment has reached the 2.02 meV sensitivity (current limits 0.036–0.156 eV, QG179). "
            "The prediction is below all existing 0νββ limits."
        ),
        "support_level": "NONE — below current experimental reach",
        "last_audit": "QG191 (pre-registration)",
        "next_experiment": "nEXO / LEGEND-1000 ton-scale (0νββ half-life sensitivity ~10²⁸ yr)",
        "falsification": "Significant exclusion below 2.02 meV (a measured upper limit < 2.02 meV FALSIFIES)",
    },
    {
        "id": "P3",
        "name": "Sector-ladder spectrum",
        "state": "SUPPORTED",
        "frozen_value": "9 resonances: 106.39 → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV; multiplicities ×10 + ×1; width 15.20 GeV",
        "current_evidence": (
            "The 151.98 rung matches the combined CMS+ATLAS ~152 GeV diphoton excess (local 3.6σ, "
            "global up to 5.4σ, arXiv:2503.16245). Alignment MODERATE SUPPORT: 0.0132% deviation, "
            "p(any rung) = 0.26% (1 in 386), z = 2.80σ (QG201). SM anchors Z/H/t confirm the ladder "
            "scale (QG200)."
        ),
        "support_level": "MODERATE — 151.98 rung aligns with the ~152 GeV excess (2.80σ)",
        "last_audit": "QG200 (sector ladder evidence audit) / QG201 (statistics audit)",
        "next_experiment": "HL-LHC diphoton confirmation of the 152 GeV excess; full Run-3 searches",
        "falsification": "A sensitive search excludes any frozen rung (limit below the rung energy FALSIFIES)",
    },
]

META = {
    "title": "TQM-QG Prediction Outcome Dashboard",
    "purpose": "single source of truth for the external validation of the registered predictions",
    "states": ["PENDING", "SUPPORTED", "CONFIRMED", "DISFAVORED", "FALSIFIED"],
    "registry_rule": "No future phase may modify a registered prediction. Frozen values never change; only the state may advance forward.",
    "locked_by": "TQM-QG Phase 193 (registry) / Phase 202 (dashboard)",
    "last_updated": "2026-08-22",
}


def write_json():
    data = {"meta": META, "outcomes": OUTCOMES}
    with open(JSON, "w", encoding="utf-8") as f:
        json.dump(data, f, ensure_ascii=False, indent=2)
        f.write("\n")
    print("Wrote", JSON)


def write_md():
    lines = []
    lines.append("# TQM-QG Prediction Outcome Dashboard")
    lines.append("")
    lines.append("**Single source of truth for the external validation of the registered predictions.**")
    lines.append("")
    lines.append(f"- Purpose: {META['purpose']}")
    lines.append(f"- Registry rule: {META['registry_rule']}")
    lines.append(f"- Locked by: {META['locked_by']}")
    lines.append(f"- Last updated: {META['last_updated']}")
    lines.append(f"- States: {' | '.join(META['states'])}")
    lines.append("")
    lines.append("## Outcome Table")
    lines.append("")
    lines.append("| ID | Prediction | State | Frozen value | Support level | Last audit | Next experiment |")
    lines.append("|----|-----------|-------|--------------|---------------|------------|-----------------|")
    for o in OUTCOMES:
        lines.append(
            f"| {o['id']} | {o['name']} | **{o['state']}** | {o['frozen_value']} | "
            f"{o['support_level']} | {o['last_audit']} | {o['next_experiment']} |"
        )
    lines.append("")
    for o in OUTCOMES:
        lines.append(f"## {o['id']} — {o['name']}")
        lines.append("")
        lines.append(f"**State:** `{o['state']}`")
        lines.append("")
        lines.append(f"- **Frozen value:** {o['frozen_value']}")
        lines.append(f"- **Current evidence:** {o['current_evidence']}")
        lines.append(f"- **Support level:** {o['support_level']}")
        lines.append(f"- **Last audit:** {o['last_audit']}")
        lines.append(f"- **Next experiment:** {o['next_experiment']}")
        lines.append(f"- **Falsification:** {o['falsification']}")
        lines.append("")
    lines.append("## State Transitions")
    lines.append("")
    lines.append("```")
    lines.append("PENDING -> SUPPORTED -> CONFIRMED")
    lines.append("PENDING -> DISFAVORED -> FALSIFIED")
    lines.append("```")
    lines.append("")
    lines.append("A state may only advance forward; frozen values never change (QG193).")
    lines.append("")
    with open(MD, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))
    print("Wrote", MD)


if __name__ == "__main__":
    write_json()
    write_md()
