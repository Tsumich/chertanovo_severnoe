using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IInteractable

{
    void Interact();
    string GetHintToInteract();
}