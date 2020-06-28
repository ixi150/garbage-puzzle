using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] BeltManager beltManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        var ctrlPressed = Input.GetKey(KeyCode.LeftControl);

        beltManager.enabled = ctrlPressed;
        Draggable.IsDragEnabled = !ctrlPressed;
    }
}
