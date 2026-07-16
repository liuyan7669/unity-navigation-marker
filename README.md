# Navigation Marker

Draws an animated navigation line along a Unity NavMesh path.

## Usage

1. Add `NavigationMarker` to a GameObject with a `LineRenderer`.
2. Assign the player and destination transforms.
3. Call `Initialization()` before `EnableNavigation()`.
4. Call `DisableNavigation()` to clear the path.

The package includes a ready-to-use prefab, material, and arrow texture.

## Requirements

- Unity 2021.3 or newer
- Unity AI module
