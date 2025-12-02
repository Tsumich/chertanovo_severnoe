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

    public IDialogueSO currentDialogue;

    public int currentLineIndex;
    public bool isInDialogue = false;

    public bool isDialogueEnding = false;

    void Awake()
    {
      
        dialoguePanel.SetActive(false);
        
        Instance = this;
    }

    public void StartDialogue(IDialogueSO dialogue)
    {
        PlayerMovement.Instance.LockPlayerMovement();
        Debug.Log($"StartDialogue вызван");

        if (dialogue == null)
        {
            Debug.Log($"Переданный dialogue пустой!");
            EndDialogue();
            return;
        }

        if (isInDialogue)
        {
            ShowNextLine();
            Debug.Log("Мы уже в диалоге и показываем след реплику");
            return;
        }
        Debug.Log("Запущен новый диалог");
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        currentLineIndex = 0;
        currentDialogue = dialogue;
        isInDialogue = true;
 
        if (currentDialogue.GetLenght() == 0) return;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex == currentDialogue.GetLenght())
        {
            EndDialogue();
            return;
        }
        Debug.Log($"ShowCurrentLine вызван для реплики {currentLineIndex}");
        if(currentDialogue == null)
        {
            Debug.Log("Current dialog == null");
            return;
        }
        DialogueLine line = currentDialogue.GetDialogueLine(currentLineIndex);

        if (line.speaker != null)
        {
            speakerText.text = line.speaker.npcName; // Берем имя из NPCDataSO

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
            speakerAvatar.gameObject.SetActive(false);
        }

        dialogueText.text = line.text;
        currentLineIndex++;
    }

    void EndDialogue()
    {
        isInDialogue = false;
        dialoguePanel.SetActive(false);
        currentDialogue = null;
        Debug.Log("Диалог завершён");
        currentLineIndex = 0;
        PlayerMovement.Instance.UnlockPlayerMovement();
    }
}