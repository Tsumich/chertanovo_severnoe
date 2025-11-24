using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractableObject : MonoBehaviour
{
    public string objectName = "Вентиляция";
    private bool playerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(objectName == "Вентиляция")
            {
                SceneManager.LoadScene("VentilationMiniGame");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
}
