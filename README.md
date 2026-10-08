# MapTeleport

Teleport to an already explored location by holding Left Alt and clicking the
large Valheim map. Exploration shared through Valheim's map sharing counts.
The map closes after a teleport. Clicking unexplored terrain does not teleport;
the click keeps its usual map behavior.

## Configuration

After the first launch, edit `BepInEx/config/Landoria.MapTeleport.cfg` to set
`Controls > TeleportKey` to `LeftAlt`, `LeftCtrl`, or `LeftShift`. `LeftAlt` is
the default. Saving the file reloads the setting automatically while Valheim
is running.

## Installation

Install BepInEx 5 and place `Landoria.MapTeleport.dll` in
`BepInEx/plugins/Landoria.MapTeleport/` in your client profile. The mod only runs
on the client; it does not need to be installed on the server.

The teleport has no moderator or administrator check. Use it only where the
server's rules permit teleportation.
