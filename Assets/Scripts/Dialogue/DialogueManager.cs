using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Image speakerAvatar;

    private DialogueSO currentDialogue;
    private int currentLineIndex;
    public bool isInDialogue = false;
    private bool canProceedToNextLine = false;

    public bool isDialogueEnding = false;

    void Awake()
    {
      
        dialoguePanel.SetActive(false);
        
        Instance = this;
    }

    void Update()
    {
        if (isInDialogue && canProceedToNextLine &&
           (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            ShowNextLine();
        }
        if (isDialogueEnding && isInDialogue)
        {
            EndDialogue();
        }
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        Debug.Log($"StartDialogue вызван");
        if (isInDialogue) return;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isInDialogue = true;
        canProceedToNextLine = true;

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        Debug.Log($"ShowCurrentLine вызван для реплики {currentLineIndex}");
        var line = currentDialogue.dialogueLines[currentLineIndex];

        // ИСПРАВЛЕННАЯ ЧАСТЬ - используем NPCDataSO вместо строки
        if (line.speaker != null)
        {
            speakerText.text = line.speaker.npcName; // Берем имя из NPCDataSO

            // Устанавливаем аватар если есть
            if (speakerAvatar != null && line.speaker.avatar != null)
            {
                speakerAvatar.sprite = line.speaker.avatar;
                speakerAvatar.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Speaker не назначен в диалоге!");
            speakerText.text = "???";
        }

        dialogueText.text = line.text;
        canProceedToNextLine = false;
        Invoke(nameof(EnableLineProgression), 0.1f);
    }

    void EnableLineProgression()
    {
        canProceedToNextLine = true;
    }

    void ShowNextLine()
    {
        if (!canProceedToNextLine) return;

        canProceedToNextLine = false;
        currentLineIndex++;

        // ИСПРАВЛЕНИЕ: проверяем, достигли ли мы конца массива
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isInDialogue = false;
        canProceedToNextLine = false;
        dialoguePanel.SetActive(false);
        // Скрываем аватар при завершении диалога
        if (speakerAvatar != null)
            speakerAvatar.gameObject.SetActive(false);
        isDialogueEnding = true;
        Debug.Log("Диалог завершён");

    }
}