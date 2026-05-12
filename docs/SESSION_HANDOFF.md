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

## Current state (as of session 1, 2026-05-11)

- `.csproj` is a minimal class library targeting `net8.0`. No game references yet.
- No source files yet — the project compiles to an empty DLL.
- `.gitignore` excludes `bin/`, `obj/`, `/packages/`, `riderModule.iml`, `_ReSharper.Caches/`, `.idea/`, `.claude/`, and `*.DotSettings.user`.
- First commit: `Initial commit` (`3bf23a6`), pushed to `origin/main`.

## Sequenced plan

Numbered so the order is unambiguous. ☑ = done.

1. ☑ Fix `.csproj` so it builds as a class library (remove `<OutputType>Exe</OutputType>`).
2. ☑ Delete the `Program.cs` Console placeholder.
3. ☑ Commit clean baseline + push to GitHub.
4. ☐ **Windows: run the in-game Modding Toolchain.** CS2 → Options → Modding. Installs .NET 8, Unity 2022.3.7f1, and the official CSII NuGet project template.
5. ☐ **Windows: capture two paths** and record them in this doc (see "Open questions" below):
   - The full path to `Cities Skylines II/Cities2_Data/Managed/` (contains `Game.dll` and Colossal Order assemblies)
   - The toolchain's NuGet cache path (typically `%LOCALAPPDATA%\Cities Skylines II\.cache\Modding\` or similar)
6. ☐ **Decide how to share game references across machines:** either copy `Managed/*.dll` back to Mac, or set up the `.csproj` to read from a path property (e.g., `$(CS2GamePath)`) that resolves differently per machine.
7. ☐ **Add `<Reference>` entries to `.csproj`** for `Game.dll` and the Colossal Order / Unity assemblies needed by mods. The canonical list comes from inspecting the official CSII NuGet template's `.csproj` — don't fabricate it.
8. ☐ **Write a minimal `IMod`** implementation: a class with empty `OnLoad`/`OnDispose` and a single log line confirming the mod loaded.
9. ☐ **End-to-end smoke test:** build on Mac → push → pull on Windows → drop DLL into the CS2 mod folder → launch CS2 → confirm the log line.
10. ☐ Only after #9 succeeds: start work on the first real policy (pick one category, build it end-to-end, no premature abstraction across categories).

## Open questions (fill in as we learn)

- [ ] Windows `Managed/` path: `...`
- [ ] Windows toolchain NuGet cache path: `...`
- [ ] Final approach for sharing game references across Mac/Windows: `...`
- [ ] Exact list of game assemblies a hello-world `IMod` needs: `...`

## Notes for whoever picks this up next

- Fin is learning C# (background: C++, Python, JS). It's fine to explain idiomatic C#/.NET concepts when they come up; don't assume familiarity with NuGet, MSBuild, or Unity DOTS.
- Keep early scope tight. Don't build abstractions across the 5 policy categories before a single policy works end-to-end.
- Don't download `Game.dll` from third-party sources. Get it from the legitimate Windows install.
- Update this doc at the end of each session — that's the whole point of it.
