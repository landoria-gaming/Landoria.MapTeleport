# MapTeleport

Teleport to explored locations from Valheim's large map with a modifier-click.
Shared exploration counts, and the map closes after teleporting.

## Controls

| Control | Action |
| --- | --- |
| `Left Alt + left-click` on the large map | Teleport to the clicked explored location. |

The modifier key can be changed to `Left Ctrl` or `Left Shift` in the configuration.
Clicking unexplored terrain keeps the usual map behavior. Teleporting has no
moderator check, so use it only where the server's rules permit it.

## Configuration

After the first launch, edit `BepInEx/config/Landoria.MapTeleport.cfg` to set
`Controls > TeleportKey` to `LeftAlt`, `LeftCtrl`, or `LeftShift`. `LeftAlt` is
the default. Saving the file reloads the setting automatically while Valheim
is running.

## Contact

Report bugs through [GitHub Issues](https://github.com/landoria-gaming/Landoria.MapTeleport/issues).
