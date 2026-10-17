# Eclipse Run

A Unity/C# prototype for a mobile endless runner with three-lane movement, swipe controls, procedural track segments, obstacles, coins, gems, and run HUD.

> **Current status:** this repository contains gameplay scripts, not a complete Unity project yet. Scenes, prefabs, materials, animations, audio, menus, monetization SDKs, and an Android build still need to be created and tested in Unity. Do not treat this as a tested or ready-to-publish game.

## Requirements

- Unity 6 (Unity 6000.x) recommended.
- For Android builds, install **Android Build Support**, **Android SDK & NDK Tools**, and **OpenJDK** through Unity Hub.
- Classic Unity Input Manager is used by the current keyboard/swipe scripts. If the project is configured for the new Input System only, set **Active Input Handling** to **Both** or **Input Manager (Old)** in Player Settings.

## Scripts included

- `GameManager`: shared coin/gem balances and run state.
- `RunnerController`: three-lane movement, jump, keyboard controls, obstacle collision.
- `MobileSwipeInput`: swipe left/right/up on touch screens.
- `ProceduralTrackSpawner` + `TrackSegment`: repeat track-segment prefabs ahead of the player.
- `TrackObstacleSpawner`: optional randomized obstacles on each segment.
- `CoinCollectible` / `GemCollectible`: trigger-based pickups.
- `CoinHUD` / `RunStatusHUD`: currency, distance, and game-over UI.
- `RunDifficulty`: gradual forward-speed increase.
- `FollowCamera`: third-person follow camera.
- `SceneBootstrap`: resets time scale and run state when a scene starts.

## First playable scene setup

1. Create a **3D Core** Unity project and save a scene as `Assets/Scenes/Gameplay.unity`.
2. Create a ground/track segment about **6 units wide and 30 units long**, with its origin at the segment's front/start edge. Make it a prefab and add `TrackSegment` and `TrackObstacleSpawner`. Set `TrackObstacleSpawner.segmentLength` to match the segment length. For a first test, leave obstacle prefabs empty; add them after basic movement works.
3. Create a Player capsule at approximately `(0, 1, 2)`. Add `CharacterController`, `RunnerController`, `MobileSwipeInput`, and optionally `RunDifficulty`. Set its tag to **Player**. The three lanes are at X = -2.2, 0, +2.2 by default.
4. Create an empty GameObject named `GameManager`, add `GameManager.cs`, and ensure there is only one in the starting scene.
5. Create an empty GameObject named `TrackSpawner`, add `ProceduralTrackSpawner.cs`, and assign the Player transform and track-segment prefab. Set segment length to the same value used by the prefab.
6. Put a Camera behind and above the player, add `FollowCamera.cs`, and assign the Player transform as Target. Example offset: `(0, 5, -7)`.
7. Add a directional light. Add a Canvas with UI Text labels for coins, gems, and distance. Attach `CoinHUD.cs` to a UI object and assign the labels. Attach `RunStatusHUD.cs` to another UI object and assign Player and distance label.
8. Make obstacle prefabs with visible meshes and colliders, tag them **Obstacle**, and assign them to each segment's `TrackObstacleSpawner`. Make coin/gem prefabs with trigger colliders and the relevant collectible script. Ensure the Player has a CharacterController and is tagged **Player**.
9. Press Play. Test keyboard controls with **A/D** or arrow keys, and jump with **Space/W/Up**. On a touchscreen, swipe left/right/up.
10. For the game-over panel, create a UI Panel, leave it inactive by default, assign it to `RunStatusHUD`, and connect its Restart button to `RunStatusHUD.RestartRun()`.

## Important integration notes

- Track-segment meshes/colliders must match the configured segment length and align with the segment prefab's origin.
- The current obstacle spawner uses randomized positions within each segment. Check obstacle spacing and ensure a safe route exists before tuning difficulty.
- `CoinHUD` uses Unity's built-in `UnityEngine.UI.Text`; TextMeshPro labels require a small code change or separate adapter.
- Coins and gems are in-memory run totals at this prototype stage; persistent saving, shop spending, ads, in-app purchases, analytics, privacy consent, and cloud saves are not implemented.
- No scene or prefab assets have been committed yet, and scripts have not been compiled or play-tested in Unity. Build and test before relying on the project.

## Android build

In Unity Hub, install Android support modules. In Unity 6, use **File > Build Profiles**, add/select Android, switch to that profile, then Build. For an APK intended only for testing, Unity's debug signing is sufficient. Google Play releases generally require an Android App Bundle (AAB), signing configuration, store assets, and policy compliance.

Official Unity references:
- [Android environment setup](https://docs.unity.com/en-us/engine/6000.0/manual/platform-specific/android/getting-started/sdksetup)
- [Build your application for Android](https://docs.unity.com/en-us/engine/6000.0/manual/platform-specific/android/building-and-delivering/build-process)
