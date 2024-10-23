using UnityEngine;
using UnityEngine.EventSystems;

public class ContextMenuMouseCheck : MonoBehaviour
{
    public RectTransform menuPanel;  // Reference to the RectTransform of the menu panel
    public float delay = 0.1f;       // Delay before starting to check for mouse position

    private bool canCheck = false;

    void Start()
    {
        // Start a delay before checking for mouse exit
        Invoke(nameof(EnableMouseCheck), delay);
    }

    void EnableMouseCheck()
    {
        canCheck = true;  // Enable the mouse checking after the delay
    }

    void Update()
    {
        if (canCheck)
        {
            // Check if the mouse is not over any UI element (including buttons or menu)
            if (!IsPointerOverUI())
            {
                Destroy(gameObject);  // Destroy the menu if the mouse is not over a UI element
            }
        }
    }

    // This method checks if the mouse is over any UI element
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();  // Check if the mouse is over any UI element
    }
}
