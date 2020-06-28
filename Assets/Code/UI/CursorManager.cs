using UnityEngine;
using System.Collections;
using System;

public class CursorManager : MonoBehaviour
{
    [SerializeField] CursorSetting normalCursor = default;
    [SerializeField] CursorSetting dragCursor = default;
    [SerializeField] CursorSetting draggingCursor = default;
    [SerializeField] CursorSetting beltCursor = default;

    void Update()
    {
        var setting = GetCursor();
        Cursor.SetCursor(setting.texture, setting.hotspot, CursorMode.Auto);
    }

    CursorSetting GetCursor()
    {
        switch (PlayerInputManager.PlayerMode)
        {
            case PlayerMode.Dragging:
                return Draggable.IsAnythingDragged ? draggingCursor : dragCursor;
            case PlayerMode.Belts:
                return beltCursor;
            case PlayerMode.Building:
                return default; //todo
            default:
                return normalCursor;
        }
    }

    [Serializable]
    struct CursorSetting
    {
        public Texture2D texture;
        public Vector2 hotspot;
    }
}
