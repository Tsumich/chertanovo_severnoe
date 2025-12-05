using UnityEngine;
using TMPro;
using System.Linq;

public class BananaCat : MonoBehaviour, IInteractable
{
    [Header("Ссылка на квест")]
    public QuestSO questData;

    [Header("Диалог NPC")]
    public DialogueSO dialogue;
    public string npcName;
    public int id;
    public Sprite avatar;
    public bool hasDialog = false;
    public DialogueSO lastDialogue;

    public void Interact()
    {
        TryStartDialogue();
    }

    void TryStartDialogue()
    {
        if (!QuestManager.Instance.activeQuest && !DialogueManager.Instance.isInDialogue)
        {
            Debug.Log("Активных квестов нет, начинаем квест бананового кота");
            QuestManager.Instance.AcceptQuest(questData);
            DialogueManager.Instance.StartDialogue(questData.objectives[0].startDialogue, () =>
            {
                Debug.Log("Завершаем задание по чтение реплики");
                QuestManager.Instance.Complete_objective(questData, questData.objectives[0]);
            });
            
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogue);

        }


    }

    public string GetHintToInteract()
    {
        return "Нажмите E, чтобы поговорить с <color=yellow>" + npcName + " </color>";
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager.Instance is null!");
            return;
        }

        if (dialogue == null)
        {
            Debug.LogError($"DialogueSO is null for NPC: {gameObject.name}");
            return;
        }
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}