# MapTeleport

Teleport to explored locations from Valheim's large map with a modifier-click.
Shared exploration counts, and the map closes after teleporting.

## Controls

| Control | Action |
| --- | --- |
| `Left Alt + left-click` on the large map | Teleport to the clicked location. |

The modifier key can be changed to `Left Ctrl` or `Left Shift` in the configuration.

## Teleport restrictions

- The destination must be explored. Clicking unexplored terrain keeps the usual map behavior.
- The destination must be at least 200 m away by default. Set the minimum from 100 to 1000 m in the configuration.
- Items blocked by portals also block map teleportation unless the world allows all items.
- Teleportation is blocked while an enemy sees or hears the player.

## Contact

Report bugs through [GitHub Issues](https://github.com/landoria-gaming/Landoria.MapTeleport/issues).
