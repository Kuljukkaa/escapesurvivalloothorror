using UnityEngine;

public class ContextMenuRayCast : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){ // Right-click detection
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    
        if(Physics.Raycast(ray, out hit)){
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if(interactable != null){
                //Show the dynamic context menu for this obj
                ContextMenu contextMenu = FindFirstObjectByType<ContextMenu>();
                contextMenu.ShowMenu(interactable, Input.mousePosition); //pass clicked objaects interactions
                Debug.Log("Interacted with " + interactable.name);
            }
        }
        

        }
    }
}
