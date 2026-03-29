# Godot C# Starter Template

This project starter is designed to eliminate the boilerplate of starting a new game by providing a minimalist architecture, essential managers, a modular UI system, and an in-game developer console right out of the box.

## Features Included

### Core Systems & Autoloads

* **GameManager:** Handles core game states and high-level logic.
* **SceneManager:** Smooth scene transitions and loading.
* **UIManager:** Centralized control over UI layers, windows, and menus.
* **Session & Logging:** Basic session tracking and formatted logging utilities.
* **Localization:** Ready-to-use setup for multi-language support.

### Developer Tools

* **In-Game Console:** A drop-down console (`ConsoleManager`, `UI_Console`) with easy-to-add custom commands (includes `help` and `clear` by default).
* **Debug HUD:** On-screen performance and variable tracking.

### Modular UI System
* **Base Classes:** `UI_BaseWindow` for easy window management (open, close, focus).
* **Pre-built Menus:** Fully functional Pause Menu and Options Menu.
* **UI Components:** Reusable components like Sliders, Number Edits, and Settings Tabs.
* **Settings System:** Attribute-based settings logic to easily save/load player preferences.

### Assets (Kenney & Fonts)
* **Prototyping Textures:** Includes Kenney's prototype textures in various colors (Dark, Green, Light, Orange, Purple, Red) for quick level blocking.
* **Fonts:** `Hack-Regular.ttf` included for a clean, readable developer UI.
* **Audio & Icons:** Bundled `.zip` packs of Kenney's UI sounds, impact sounds, and game icons ready to be extracted and used.

## Project Structure
* `assets/`: Raw assets like fonts, textures (Kenney grids), and audio.
* `scenes/`: Main game levels and environment setups.
* `scripts/`: All C# logic, separated by domain (`Autoloads/`, `Console/`, `Settings/`, `UI/`, `Utils/`).
* `ui/`: UI scenes (`.tscn`) and raw UI assets (icons). Scripts for these are linked from the `scripts/UI/` directory.

## Getting Started

### Prerequisites
* **Godot 4.x .NET Edition** (Required for C# support).
* An IDE with C# support (Visual Studio, VS Code, or Rider).

### Installation

1. **Clone or Download the Template**
```bash
git clone https://github.com/tmenant/gd-starter my-new-game
cd my-new-game
```

2. **Open in Godot**
* Open the Godot Project Manager.
* Click **Import** and select the `project.godot` file in your cloned folder.

3. **Build the C# Solution (Crucial)**
* Before running the game, you **must** build the C# project so Godot recognizes all the custom classes.
* Click the **Build** button at the top right of the Godot Editor, or open the `.sln` in your IDE and build it there.

4. **Verify Autoloads**
* Go to `Project > Project Settings > Globals/Autoload`.
* Ensure that `GameManager`, `SceneManager`, `UIManager`, `ConsoleManager`, and `DebugHUD` are listed and enabled. (They should be, but it's good practice to check on a fresh clone).

*Template built for Godot 4.x .NET.*