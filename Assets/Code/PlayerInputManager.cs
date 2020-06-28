using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] BeltManager beltManager = null;
    [SerializeField] Button dragButton = null;
    [SerializeField] Button beltButton = null;

    public static PlayerMode PlayerMode;

    void Awake()
    {
        PlayerMode = PlayerMode.Dragging;

        dragButton.onClick.AddListener(() => PlayerMode = PlayerMode.Dragging);
        beltButton.onClick.AddListener(() => PlayerMode = PlayerMode.Belts);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }




        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerMode = PlayerMode.Belts;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && PlayerMode == PlayerMode.Belts)
        {
            PlayerMode = PlayerMode.Dragging;
        }


        dragButton.interactable = !(Draggable.IsDragEnabled = PlayerMode == PlayerMode.Dragging);
        beltButton.interactable = !(beltManager.enabled = PlayerMode == PlayerMode.Belts);
    }
}

public enum PlayerMode
{
    None,
    Dragging,
    Belts,
    Building,
}
