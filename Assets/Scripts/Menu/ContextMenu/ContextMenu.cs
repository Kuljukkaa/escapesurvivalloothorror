using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this for TextMeshPro

public class ContextMenu : MonoBehaviour
{
    public GameObject buttonPrefab; //prefab for the dynamic buttons
    public GameObject menuPanelPrefab; //prefab for the panel where buttons go
    private GameObject currentMenu; //currently displayed menu
    private InteractableObject currentObject; // The object being interacted with
    public Transform canvasTransform; //Reference to the canvas

    public void ShowMenu(InteractableObject interactable, Vector3 position){
        Debug.Log("menuPanelPrefab: " + menuPanelPrefab);
        Debug.Log("buttonPrefab: " + buttonPrefab);
        Debug.Log("canvasTransform: " + canvasTransform);
        //destroy any existing menu
        if(currentMenu != null){
            Destroy(currentMenu);
        }
        currentObject = interactable; //set the current object being interacted with

        currentMenu = Instantiate(menuPanelPrefab, position, Quaternion.identity, canvasTransform); //instantiate the menu panel where clicked

        //clear any previous buttons
        foreach(Transform child in currentMenu.transform){
            Destroy(child.gameObject);
        }

        //dynamically create buttons for each interaction
        foreach(var interaction in interactable.interactions){
            GameObject button = Instantiate(buttonPrefab, currentMenu.transform); //Create new btn from prefab
            button.GetComponentInChildren<TextMeshProUGUI>().text = interaction; //set button text
            button.GetComponent<Button>().onClick.AddListener(() => OnMenuSelect(interaction)); //Add a listener to the button to handle clicks ?????? mitä vittua???
        }
    }

    public void OnMenuSelect(string interaction){
        currentObject.Interact(interaction); //trigger the interaction on obj
        Destroy(currentMenu);
    }
}
