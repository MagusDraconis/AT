# Copilot Instructions for TQM

## xUnit Test Categories

There are two categories of xUnit tests in this project.

### Category 1: Software Validation Tests
Used to validate classes, services, methods, edge cases, and correctness. These are conventional unit/integration tests with standard Arrange-Act-Assert structure.

### Category 2: Research Tests
Used as executable scientific experiments. Research tests are first-class citizens of the TQM project and should be treated as executable research papers.

Research tests **must**:

- Inherit from `ResearchTestBase` (in `TQM.Tests.Shared`)
- Use `ITestOutputHelper` (provided by the base class as `Output`)
- Generate detailed scientific reports
- Use `StringBuilder` for composing multi-section output
- Output **assumptions** at the beginning of the report
- Output **intermediate calculations** with labeled values
- Output **final conclusions** summarizing findings
- Save all important numerical results in the output
- Be **deterministic** and **reproducible** — no randomness, no external dependencies that can change results between runs

Research tests should be placed under `TQM.Tests/Research/` and follow the naming convention `TQM_###_Tests.cs`.

Use `PrintHeader(string title)` from `ResearchTestBase` to demarcate major sections of the research report.

## Test Performance & Optimization

All xUnit tests should be **optimized for speed**:

- Use `StringBuilder` instead of string concatenation for composing output — this avoids intermediate string allocations that slow down test execution
- Use **`Parallel.For`**, **`Parallel.ForEach`**, or **`Task.WhenAll`** for independent computations that can run concurrently, especially in research tests performing many independent calculations
- Avoid `Thread.Sleep` or unnecessary delays — prefer `Task.Delay` only when genuinely required
- Keep test setup minimal — only initialize what the test actually needs

## Shared Code & DRY Principle

- Extract common logic into **shared base classes** (like `ResearchTestBase`) or **static utility methods** in `TQM.Tests.Shared`
- If the same helper method appears in two or more test files, move it into a shared class — do not duplicate
- Shared helpers should be placed under `TQM.Tests/Shared/`

# TQM Project Memory Rules

## Persistent Project Memory

This repository contains a persistent cross-chat memory document:

Docs/NewChat_Start.md

This file is the authoritative project context.

Before implementing any feature, experiment, simulation, theory extension, or research test:

1. Read Docs/NewChat_Start.md
2. Treat it as the current state of the TQM project
3. Preserve all documented decisions and hypotheses
4. Ensure all new work remains consistent with the documented research direction

## Mandatory Maintenance

Whenever one of the following occurs:

- a research experiment is completed
- a hypothesis is strengthened
- a hypothesis is weakened
- a hypothesis is rejected
- a new research direction is adopted
- a major architectural decision is made
- a significant scientific insight is discovered

the file Docs/NewChat_Start.md MUST be updated.

## Update Philosophy

Keep NewChat_Start.md concise.

It should contain only:

- mission
- current core hypothesis
- current research path
- important decisions
- completed experiments and conclusions
- current working hypothesis
- next open question

Do NOT turn it into a research log or full documentation.

## Research Documentation

Detailed experiment results belong in:

Docs/TQM_LabBook.md

Only major conclusions should be copied into:

Docs/NewChat_Start.md

## Prompt Requirement

For every new TQM experiment, simulation, theory extension, or research task:

- Read Docs/NewChat_Start.md first
- Perform the requested work
- Determine whether the project knowledge changed
- If knowledge changed, update Docs/NewChat_Start.md
- Mention the update in the final summary

NewChat_Start.md is considered mandatory project memory and must remain current.
