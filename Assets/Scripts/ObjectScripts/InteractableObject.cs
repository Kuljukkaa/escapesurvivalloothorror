using UnityEngine;

public class InteractableObject : MonoBehaviour
{
// Array of interactions (e.g., "Examine", "Pick Up", "Use")
  public string[] interactions;

  public void Interact(string action){
    Debug.Log("Interacting with " + gameObject.name + " using action: " + action);
  }
}
