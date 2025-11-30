using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Messages : MonoBehaviour
{
    public static Messages Instance;

    public GameObject panel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI dialogueMessageText;
    public bool isButtonPressed = false;

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

        LockPlayerMovement();
    }

    private void LockPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // ВЫКЛЮЧАЕМ скрипт движения
        }
    }

    public void CloseDialogWindow()
    {
        Cursor.visible = false;
        isButtonPressed = false;
        panel.SetActive(false);

        UnlockPlayerMovement(); 
    }

    private void UnlockPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true; 
        }
    }

    public void clickYes()
    {
        panel.SetActive(false);
        isButtonPressed = true;
        UnlockPlayerMovement();
        onYesCallback?.Invoke();
    }

    public void clickNo()
    {
        panel.SetActive(false);
        isButtonPressed = false;
        UnlockPlayerMovement(); // РАЗБЛОКИРУЕМ при нажатии No
    }

    public bool getBtnsState()
    {
        if (isButtonPressed)
        {
            Debug.Log(isButtonPressed);
        }
        return isButtonPressed;
    }
}