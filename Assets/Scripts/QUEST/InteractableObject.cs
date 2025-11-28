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
            if (objectName == "Вентиляция")
            {
                Debug.Log("enterLocation");
                if(Inventory.Instance.HasItem("mop"))
                {
                    Debug.Log("Швабра есть ");
                    Messages.Instance.showDialogWindow("Почистить вентиляцию?");
                    if(Messages.Instance.isButtonPressed)
                    {
                        SceneManager.LoadScene("VentilationMiniGame");
                        Messages.Instance.CloseDialogWindow();
                    }
                }
                else
                {
                    Messages.Instance.messageText.text = "Нечем воспользоваться для чистки";
                    Debug.Log("Швабры нет");
                }
            }

            else if (objectName == "Автомат")
            {
                Debug.Log("вы подошли к автомату с едой");
                Messages.Instance.showDialogWindow("Потратить деньги на коке-коку зеро?");
                if (playerInRange && Messages.Instance.isButtonPressed)
                {
                    if(Inventory.Instance.coins > 15)
                    {
                        Inventory.Instance.coins -= 15;
                        Messages.Instance.messageText.text = "Вы купили коке-коку зеро";
                    }
                    else
                    {
                        Messages.Instance.messageText.text = "Недостаточно денег на коке-колу зеро";
                    }
                    PlayerInteraction.Instance.hintPanel.SetActive(false);
                    Messages.Instance.CloseDialogWindow();
                }
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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
