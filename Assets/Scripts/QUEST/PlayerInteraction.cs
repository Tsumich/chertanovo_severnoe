using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    private BulletinBoard currentBoard;
    public GameObject hintPanel;
    public TextMeshProUGUI messageText;

    public bool itemIsTaken = false;

    public string lastLocation = "Cafe";

    public static PlayerInteraction Instance;


    void Awake()
    {
        // ИСПРАВЬ для предотвращения дубликатов
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
           Destroy(gameObject);
        }

        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("HintPanel не назначен в инспекторе!");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Скрываем панель при загрузке любой новой сцены
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }

        currentBoard = null;

    }

    void Update()
    {

        if (currentBoard != null && Input.GetKeyDown(interactKey))
        {
            currentBoard.Interact();
        }
        if (DialogueManager.Instance != null && DialogueManager.Instance.isInDialogue)
        {
            //Debug.Log("скрыли плашку");
            hintPanel.SetActive(false);
            return;
        }
        if (itemIsTaken)
        {
            itemIsTaken = false;
            hintPanel.SetActive(false);
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger) return;
        Debug.Log($"Триггер: {other.name} (родитель: {other.transform.parent?.name})");

        if (hintPanel != null && hintPanel.activeSelf) return;

        else if (other.CompareTag("BulletinBoard"))
        {
            currentBoard = other.GetComponent<BulletinBoard>();
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            messageText.text = "Нажмите " + interactKey + " чтобы взаимодействовать с доской";
        }
        else if (other.CompareTag("NPC"))
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            Debug.Log("Нажми E для разговора");
            messageText.text = "Нажмите " + interactKey + " чтобы поговорить";
        }
        else if (other.CompareTag("Item"))
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            messageText.text = "Нажми E чтобы подобрать предмет";
        }
        else if (other.CompareTag("Enter"))
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            messageText.text = "Нажмите Е чтобы зайти";
        }
        else if (other.CompareTag("InteractableObject"))
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(true);
            }
            messageText.text = "Нажмите Е чтобы взаимодействовать";
        }
    }


    // ДОБАВЬ этот метод чтобы скрывать панель при уходе
    void OnTriggerExit(Collider other)
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }

        if (other.CompareTag("BulletinBoard"))
        {
            currentBoard = null;
            // СКРЫВАЕМ панель при уходе

            Debug.Log("Отошли от доски");

        }
    }
}