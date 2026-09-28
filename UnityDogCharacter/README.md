# Unity Dog Character

Two drop-in scripts for a dog character, since Unity characters are built from a model +
Animator + script rather than a single asset:

## Scripts

- **`Scripts/DogController.cs`** — player-controlled dog. WASD/arrows to move, Shift to run,
  Space to jump, `C` to sit/stand, `B` to bark. Attach to a GameObject with a
  `CharacterController` component.
- **`Scripts/DogCompanionAI.cs`** — NPC dog that follows an owner `Transform` and sits near
  them, or wanders when it has no owner. Attach to a GameObject with a `NavMeshAgent`
  (bake a NavMesh in the scene first).

## Setup

1. Get a dog model (e.g. from the Unity Asset Store, Mixamo, or Sketchfab — model files
   themselves aren't something I can generate here).
2. Add a `CharacterController` (for `DogController`) or `NavMeshAgent` (for
   `DogCompanionAI`) to the dog's root GameObject.
3. Add an `Animator` component with an Animator Controller that has these parameters:
   - `Speed` (float)
   - `IsGrounded` (bool) — `DogController` only
   - `Jump` (trigger) — `DogController` only
   - `Bark` (trigger) — `DogController` only
   - `Sit` (bool)
4. For `DogController`: add an empty child GameObject near the feet named `GroundCheck`,
   assign it to the `Ground Check` field, and set `Ground Mask` to your ground's layer.
5. Drop the script onto the GameObject and assign the fields in the Inspector.
