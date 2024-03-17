using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // You may also want to provide a way for the player to unlock the cursor, such as pressing the Escape key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Unlock the cursor and show it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}