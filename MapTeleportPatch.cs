using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace Landoria.MapTeleport
{
    // Teleports the local player when they modifier-click the large map.
    [HarmonyPatch(typeof(Minimap), nameof(Minimap.OnMapLeftClick))]
    internal static class MapTeleportPatch
    {
        private static readonly MethodInfo IsExploredMethod = AccessTools.Method(
            typeof(Minimap), "IsExplored", new[] { typeof(Vector3) });

        // Handles the teleport and suppresses the normal map click.
        private static bool Prefix(Minimap __instance)
        {
            if (Player.m_localPlayer == null || WorldGenerator.instance == null ||
                !IsTeleportKeyHeld() ||
                !TryGetMapPosition(__instance, out Vector3 destination) ||
                !IsExplored(__instance, destination))
            {
                return true;
            }

            destination.y = WorldGenerator.instance.GetHeight(destination.x, destination.z);
            Player player = Player.m_localPlayer;
            player.TeleportTo(destination, player.transform.rotation, distantTeleport: true);
            __instance.SetMapMode(Minimap.MapMode.Small);
            return false;
        }

        // Uses Valheim's own exploration check, including shared map progress.
        private static bool IsExplored(Minimap map, Vector3 destination)
        {
            return IsExploredMethod != null &&
                   IsExploredMethod.Invoke(map, new object[] { destination }) is bool explored &&
                   explored;
        }

        // Checks only the three supported left-side modifier keys.
        private static bool IsTeleportKeyHeld()
        {
            switch (Plugin.TeleportKey.Value)
            {
                case ModifierKey.LeftAlt:
                    return ZInput.GetKey(KeyCode.LeftAlt);
                case ModifierKey.LeftCtrl:
                    return ZInput.GetKey(KeyCode.LeftControl);
                case ModifierKey.LeftShift:
                    return ZInput.GetKey(KeyCode.LeftShift);
                default:
                    return false;
            }
        }

        // Converts the pointer on the visible map into world coordinates.
        private static bool TryGetMapPosition(Minimap map, out Vector3 position)
        {
            RectTransform rect = map.m_mapImageLarge.transform as RectTransform;
            if (rect == null || !RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rect, ZInput.pointerPosition, null, out Vector2 localPoint))
            {
                position = Vector3.zero;
                return false;
            }

            Vector2 normalized = Rect.PointToNormalized(rect.rect, localPoint);
            Rect visibleMap = map.m_mapImageLarge.uvRect;
            float mapX = visibleMap.xMin + normalized.x * visibleMap.width;
            float mapY = visibleMap.yMin + normalized.y * visibleMap.height;
            float halfTexture = map.m_textureSize / 2f;
            position = new Vector3((mapX * map.m_textureSize - halfTexture) * map.m_pixelSize,
                0f, (mapY * map.m_textureSize - halfTexture) * map.m_pixelSize);
            return true;
        }
    }
}
