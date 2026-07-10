# Running to The Lighthouse

Running to The Lighthouse is a 3D first-person shooter game built in Unity 6. The player must survive waves of zombies, manage ammunition, and reach the lighthouse before night falls. 
Play for free:
https://marcelozuza.itch.io/runningtothelighthouse

## Overview

- Genre: First-person shooter / survival
- Main goals: fight zombies, use multiple weapons, and reach the lighthouse before darkness arrives
- Main scene: `Assets/Scenes/Level1.unity`
- Main menu: `Assets/Scenes/MainMenu.unity`
- Unity version: 6.0.3.14f1

## Key Features

- First-person movement with jump and mouse/gamepad look
- Shooting mechanics with raycast hit detection, impact effects, and blood effects
- Reloading and ammo UI for current and reserve ammunition
- Zombie AI with detection, pursuit, attack, and death animations
- Player health system with death panel and restart option
- Finish trigger that pauses the game and shows the victory screen
- Nightfall effect using directional light rotation

## Project Structure

- `Assets/Scripts/Player/`
  - `Movement.cs` - player movement and jump logic
  - `MouseLook.cs` - first-person camera control
  - `PlayerHealth.cs` - player health, death, and restart
  - `PlayerInputHandler.cs` - input binding and action handling
  - `ShootingSystem.cs` - shooting, ammo decrement, and hit detection
  - `RealoadSystem.cs` - reload handling and ammo refill logic
  - `WeaponManager.cs` - equip and swap weapons, update ammo HUD
  - `NewWeapon.cs` - weapon data definition via `ScriptableObject`
- `Assets/Scripts/Enemies/`
  - `ZombieAI.cs` - zombie pathfinding, detection, and attack behavior
  - `ZombieHealth.cs` - zombie health, damage reaction, and death
- `Assets/Scripts/GameManager/`
  - `FinishGame.cs` - finish line trigger and victory panel
  - `DarknessTrigger.cs` - transition to night by rotating the directional light
- `Assets/Scenes/`
  - `MainMenu.unity`
  - `Level1.unity`
  - `TestScene.unity`
- `Assets/InputSystem_Actions.inputactions` - player input setup for Unity Input System
- `Packages/manifest.json` - Unity package dependencies
- `WebGL Builds/` - exported WebGL build folder

## Controls

- Move: `W`, `A`, `S`, `D` or arrow keys
- Jump: `Space`
- Aim: mouse movement or right joystick
- Shoot: left mouse button or gamepad fire button
- Reload: `R`
- Next weapon: `E` or gamepad next weapon button
- Previous weapon: `Q` or gamepad previous weapon button
- Restart after death: shoot again

## Dependencies

- Unity 6.0.3.14f1
- `com.unity.inputsystem`
- `com.unity.ai.navigation`
- `com.unity.render-pipelines.universal`
- `com.unity.visualscripting`
- `com.unity.ugui`
- `TextMeshPro` (included with Unity)

## How to run

1. Open the project in Unity Editor 6.0.3.14f1.
2. Ensure packages in `Packages/manifest.json` are installed.
3. Open `Assets/Scenes/MainMenu.unity`.
4. Press Play to start the main menu.
5. Use the menu to start the game and load `Level1`.

## Notes

- `MainMenuManager.cs` loads the game scene with `SceneManager.LoadScene(1)`.
- `PlayerHealth.cs` pauses the game on death and shows a game over panel.
- `FinishGame.cs` pauses the game when the player reaches the finish trigger.
- `DarknessTrigger.cs` rotates the directional light to simulate nightfall.
- `deathByWater.cs` currently initializes a death panel but does not contain full water kill logic; water death is handled in `PlayerHealth.cs`.

## Suggestions for Improvement

- Add a visible timer or objective progress UI
- Support multiple levels and checkpoints
- Clean up debug logs before release
- Improve weapon swap and reload animation handling
- Add audio and visual polish for victory and defeat screens
