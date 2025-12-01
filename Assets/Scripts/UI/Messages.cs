using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Messages : MonoBehaviour
{
    public static Messages Instance;

    public GameObject panel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI dialogueMessageText;

    private System.Action onYesCallback;

    private PlayerMovement playerMovement;

    void Awake()
    {
        panel.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Обнаружен дубликат {GetType().Name}! Уничтожаю: {gameObject.name}");
            Destroy(gameObject);
            return;
        }
        Instance = this;
       // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // НАЙДИ PlayerMovement при старте
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void showDialogWindow(string text, System.Action onYesCallback = null)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);
        dialogueMessageText.text = text;

        this.onYesCallback = onYesCallback;
        DialogueManager.Instance.isInDialogue = true;
        PlayerMovement.Instance.LockPlayerMovement();
    }



    public void CloseDialogWindow()
    {
        Cursor.visible = false;
        panel.SetActive(false);
        DialogueManager.Instance.isInDialogue = false;
        PlayerMovement.Instance.UnlockPlayerMovement();
    }

    public void clickYes()
    {
        onYesCallback?.Invoke();
        CloseDialogWindow();
    }

    public void clickNo()
    {
        CloseDialogWindow();
    }
}