# Session Handoff

Living doc that captures the current state of the project and what to do next.
Update it at the end of each work session so a fresh reader (human or Claude) can pick up cold.

---

## Project overview

**Goal:** A Cities: Skylines 2 mod that adds new city and district policies across five categories:
1. Economic / Tax
2. Zoning / Land Use
3. Environmental
4. Traffic / Transport
5. Social / Happiness

**Distribution target:** Paradox Mods via the official PDX SDK toolchain.

**Project name on disk:** `CS2PolicyExtension` (note: not `CS2PolicyExpansion` — earlier notes had it spelled differently).

## Tech stack

- **Target framework:** `net8.0` (per CS2 wiki: net6 minimum, net8 recommended)
- **SDK:** .NET 8 SDK
- **Project type:** Class library (`.dll` loaded by CS2 at runtime — no `Main()`, implements `IMod`)
- **IDE:** JetBrains Rider (free for non-commercial use)
- **Repo:** https://github.com/fahinr/CS2PolicyExtension (public)

## Two-machine workflow

- **Mac (Rider)** — primary dev: write/build/commit here.
- **Windows** — runs CS2; used for testing built DLLs and for anything that requires the in-game Modding Toolchain.
- Sync via git. Push from Mac, pull on Windows; same in reverse.

On the **Mac**, the .NET 8 SDK is installed but **not on PATH**. Invoke it as `~/.dotnet/dotnet`.

## Current state (as of session 2, 2026-05-12)

- Windows toolchain has been run. Scaffolding lives in `CS2PolicyExtension/` (subdir), produced by the official CSII NuGet template.
- `CS2PolicyExtension/CS2PolicyExtension.csproj` references `Game`, `Colossal.Core`, `Colossal.Logging`, `Colossal.IO.AssetDatabase`, `UnityEngine.CoreModule`, `Unity.Burst`, `Unity.Collections`, `Unity.Entities`, `Unity.Mathematics`. Game references resolved via `CSII_TOOLPATH` env var on the Windows machine.
- `CS2PolicyExtension/Mod.cs` implements `Game.Modding.IMod` with `OnLoad`/`OnDispose` and a logger. Hello-world shape — no policy code yet.
- `Properties/PublishConfiguration.xml` + publish profiles present (PDX Mods publishing pipeline).
- The Mac build of the policy work has NOT been run yet against the toolchain references — those resolve from a Windows-only env var. Mac builds will currently fail without a cross-platform reference strategy.
- The hello-world Mod has NOT been smoke-tested in CS2 yet.

## Sequenced plan

Numbered so the order is unambiguous. ☑ = done.

1. ☑ Fix `.csproj` so it builds as a class library (remove `<OutputType>Exe</OutputType>`).
2. ☑ Delete the `Program.cs` Console placeholder.
3. ☑ Commit clean baseline + push to GitHub.
4. ☑ Windows: run the in-game Modding Toolchain. Scaffolding generated.
5. ☑ `<Reference>` entries are wired via the CSII template (`Mod.props`/`Mod.targets` imported via `CSII_TOOLPATH`).
6. ☑ Minimal `IMod` exists (`CS2PolicyExtension/Mod.cs`).
7. ☐ **Cross-platform reference strategy** — decide how (or whether) the Mac build resolves the toolchain references. Options: (a) only build on Windows; (b) copy toolchain reference DLLs to Mac and override `CSII_TOOLPATH` locally; (c) Mac builds skip the policy code paths via conditional compilation. Lowest-friction option is (a) for now.
8. ☐ **End-to-end smoke test of hello-world Mod:** build on Windows → publish locally via toolchain → launch CS2 → confirm `OnLoad` log line in `Player.log`. **Blocks all policy work.**
9. ☐ Begin policy work (see "Policy work — v1 plan" below).

## Open questions (fill in as we learn)

- [ ] Windows `Managed/` path: `...`
- [ ] Windows toolchain NuGet cache path (where `CSII_TOOLPATH` points): `...`
- [ ] Final approach for sharing game references across Mac/Windows: `...`
- [ ] Does Mac `dotnet build` work without `CSII_TOOLPATH`? If not, decision per Step 7.

---

## Policy work — v1 plan (codex-approved 2026-05-12)

The plan below was produced via the multi-stage codex-orchestrated workflow defined in `.claude/CLAUDE.local.md`: initial plan → adversarial review → revision → validation. Final verdict: **APPROVED** by codex with one non-blocking caution (noted under "Residual risks").

### Catalog (feasibility-tiered)

**Tier A — likely feasible as v1-style data-only single-effect policy:**
- Smoking Ban (district happiness modifier)
- Noise Ordinance (district happiness modifier)
- Public Wi-Fi (district happiness modifier — happiness-only variant of the original concept)

**Tier B — plausible after Tier A pattern is verified, may need light system hooks:**
- Tourism Tax, Vacancy Tax, Free Public Transit, Truck Curfew, Small Business Tax Break, Youth Program Funding

**Tier C — likely require new ECS systems or aren't policy-expressible; deferred indefinitely:**
- All Zoning/Land Use entries (Mixed-Use Mandate, Maximum Building Height, ADUs, Transit-Oriented Density Bonus, Historic Preservation)
- All Environmental entries (Mandatory Solar Panels, Tree Canopy Requirement, Single-Use Plastics Ban, Industrial Emissions Cap, Composting Mandate)
- Most Traffic entries (Congestion Charge, Low-Emission Zone, Car-Free Sunday)
- Social: Community Policing
- Economic: Land Value Tax, Vacancy Speculation Penalty

Tier C remains in the brainstorm doc but the mod does not promise these. Re-evaluate after Tier A+B work.

### First-policy pick — **DEFERRED until after decompile**

Choose whichever of {Smoking Ban, Noise Ordinance, Public Wi-Fi (happiness-only)} maps **most cleanly** onto the cleanest vanilla district policy analogue. "Most cleanly" = fewest non-modifier components, clearest district scoping. If none map well, surface that finding and re-plan — do not force-fit.

### Implementation plan

**Step 0 — Prereq (blocking):** Smoke-test the hello-world `Mod` in CS2. Confirm the `OnLoad` log line in `Player.log`. No policy work begins until this passes.

**Step 1 — Decompile one vanilla district policy end-to-end. Answer five facts before any pick:**
1. How is the policy prefab defined? (component shape, asset vs. code authoring)
2. Where and when is it registered? (which system, which world, which update phase, vanilla call site)
3. How is **district scope** represented? (vs. city scope — same component with a flag, or a different component?)
4. Are effects **data-only** (modifier components consumed by an existing system) or **system-backed** (the policy requires its own system to do anything)?
5. **Can a mod-created policy prefab be registered at runtime the same way vanilla does, or does vanilla rely on asset-pipeline behavior that mod code does not have access to?**

Output: `docs/decompile_notes/policy_pattern.md` with the five answers and a code snippet of the cleanest vanilla example.

**STOP CONDITION:** If the cleanest analogue is system-backed, OR if question 5's answer is "no" (registration depends on non-mod-accessible initialization) — STOP. Re-scope: v1 becomes "prove custom policy registration is possible from mod code." Do not continue to Step 2 with a candidate policy until registration is proven feasible.

**Step 2 — Pick first policy from Tier A** based on the cleanest vanilla analogue. Record the choice and reasoning in this doc under "First pick decision".

**Step 3 — Files to add (minimal v1):**
- `CS2PolicyExtension/Policies/<ChosenPolicy>Policy.cs` — builds the prefab, attaches one happiness `ModifierData`. Nothing else.
- Localization entry (English only): `Policies.<ChosenPolicy>.Name`, `Policies.<ChosenPolicy>.Description`. Format verified from `Properties/PublishConfiguration.xml` and the CSII template's localization conventions.
- `assets/icons/<chosen_policy>.png` — placeholder.
- `Mod.cs` extension: resolve `PrefabSystem` from `updateSystem.World`, call `<ChosenPolicy>Policy.Build(prefabSystem)`, register. Wrap in try/catch; failure logs and continues.

**Step 4 — Modifier value:** Use the smallest non-zero value that is observable in UI/debug behavior, ideally matching the magnitude used by the vanilla analogue policy. Exact number follows decompile findings, not a guess. **No upkeep. No secondary modifier.** Both deferred to a follow-up after toggle works.

**Step 5 — Smoke-test (the only test). Layered:**

*5a. Pre-smoke-test gate (do BEFORE first smoke-test launch):*
- Prefab naming/identity strategy: namespaced prefab ID (e.g., `CS2PolicyExtension.<PolicyName>`). No collision with any vanilla policy prefab name. No collision under fresh install vs. mod re-enable. Validate this strategy before flying it into CS2.

*5b. Runtime verification:*
- Mod loads cleanly (no exceptions in `Player.log`).
- Custom policy appears in the **district policy panel exactly once**.
- **Correct scope only**: appears in district panel, NOT city-wide policy panel (and not both).
- Toggle on / toggle off works without errors.
- Save/load preserves toggle state.
- Disabling and re-enabling the mod in the same session does not duplicate registration.

*5c. Behavioral verification:*
- Effect applies only inside the target district.
- Neighboring districts unaffected.
- Citywide values don't change unless expected.

*5d. Failure-mode verification (required for v1):*
- Save with the policy active, then uninstall the mod, then load the save — does CS2 handle the missing prefab reference?
- Game restart after enabling the mod (fresh process, not just session toggle).
- New map vs. established save.

*5e. Exploratory only (NOT v1 acceptance criteria — observe and document):*
- Missing icon / missing localization — does the panel degrade gracefully?

*5f. Logging discipline:*
- One log line at successful registration (with prefab ID + name).
- One log line on any binding/registration failure.
- **No** per-tick / per-apply log spam.

**Step 6 — Edge-case backlog (tracked but NOT v1 blockers):**
- Pre-existing saves vs. new saves.
- Iterative dev: reloading the mod in-session — duplicate prefab IDs (validate via the Step 5a naming strategy).
- District deletion / merge / split while policy is active.
- Applying to zero-population / undeveloped districts.
- Target subsystem removed/renamed in a future game patch.
- v1→v2 prefab structure change in our own mod.

**Step 7 — Out of scope for v1:**
- All Tier B and Tier C policies.
- Upkeep / cost system integration.
- Secondary modifiers (education effectiveness, etc.).
- Final icon art / localization beyond English.
- Numeric balancing against vanilla.
- Abstractions like `IPolicyDefinition` or `PolicyRegistrar` — revisit when 2–3 policies exist.

**Step 8 — Assumptions to validate via decompile (Step 1):**
- A vanilla district policy exists whose only effect is a single happiness modifier. (If not, switch to the simplest data-only district policy that does exist.)
- `PrefabSystem` is reachable from `Mod.OnLoad(UpdateSystem)` via `updateSystem.World`.
- Mods can ship policy prefabs without a separate asset bundle.
- **Highest-risk:** mods can introduce policy prefabs in a way the game UI and simulation fully recognize. Step 1 question #5 is designed to surface this before any v1 code is written.

### Residual risks (codex final NIT, non-blocking)

- "Save with policy active, then mod removed, then load save" behavior may be partly outside mod control. Test it, but **do not let it silently expand v1 into save-migration work** unless the decompile surfaces a clean mitigation path. If no clean mitigation, this check downgrades from required to "observe and document."

### First pick decision

_To be filled in after Step 1 (decompile) completes._

- Chosen policy: `...`
- Cleanest vanilla analogue: `...`
- Reason for pick: `...`

---

## Notes for whoever picks this up next

- Fin is learning C# (background: C++, Python, JS). It's fine to explain idiomatic C#/.NET concepts when they come up; don't assume familiarity with NuGet, MSBuild, or Unity DOTS.
- Keep early scope tight. Don't build abstractions across the 5 policy categories before a single policy works end-to-end.
- Don't download `Game.dll` from third-party sources. Get it from the legitimate Windows install.
- Update this doc at the end of each session — that's the whole point of it.
- `.claude/CLAUDE.local.md` defines the codex-orchestrated multi-stage workflow. For non-trivial design work, follow it.
