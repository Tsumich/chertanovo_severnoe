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

    // ДОБАВЬ ссылку на PlayerMovement
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

    public void showDialogWindow(string text)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);
        dialogueMessageText.text = text;

        LockPlayerMovement(); // БЛОКИРУЕМ движение
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

        UnlockPlayerMovement(); // РАЗБЛОКИРУЕМ движение
    }

    private void UnlockPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // ВКЛЮЧАЕМ скрипт движения
        }
    }

    public void clickYes()
    {
        panel.SetActive(false);
        isButtonPressed = true;
        UnlockPlayerMovement(); // РАЗБЛОКИРУЕМ при нажатии Yes
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