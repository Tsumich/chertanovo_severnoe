using UnityEngine;

public class ForceCursor : MonoBehaviour
{
    void Start()
    {
        // ѕринудительно показываем курсор в миниигре
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // ѕосто€нно провер€ем, что курсор видим
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}