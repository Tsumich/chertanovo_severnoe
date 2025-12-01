using UnityEngine;
using TMPro;
using System.Linq;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Диалог NPC")]
    public DialogueSO dialogue;
    public string npcName;
    public int id;
    public Sprite avatar;
    public bool hasDialog = false;
    public DialogueSO lastDialogue;

    [Header("Настройки индикатора")]
    public Vector3 exclamationOffset = new Vector3(0, 3.5f, 0);
    public Color exclamationColor = Color.yellow;
    public float exclamationSize = 7f;

    private bool playerInRange = false;
    private GameObject exclamationMark; // Будет создаваться автоматически


    public void Interact()
    {
        // Добавляем проверку, что диалог действительно завершен
        if (!DialogueManager.Instance.isInDialogue && DialogueManager.Instance.isDialogueEnding == false)
            {
                TryStartDialogue();
                Debug.Log($"клавиша нажата: E");
            }
            else
            {
                DialogueManager.Instance.isDialogueEnding = false;
            }        
    }



    void TryStartDialogue()
    {
        
        if (!DialogueManager.Instance.isInDialogue )
        {
            if (QuestManager.Instance.activeQuest)
            {
                Debug.Log("has active");
                if (this.id == 1) DialogueManager.Instance.StartDialogue(QuestManager.Instance.activeQuest.KalinaLine);
                else 
                {
                    QuestSO active_quest = QuestManager.Instance.activeQuest;
                    QuestObjective firstUncompleted = active_quest.objectives
                        .FirstOrDefault(objective => 
                            !objective.isCompleted);
                    Debug.Log(firstUncompleted.objectiveType);
                    if (firstUncompleted.objectiveType != ObjectiveType.TalkToNPC) return;
                    if (firstUncompleted != null)
                    {
                        DialogueManager.Instance.StartDialogue(firstUncompleted.startDialogue);
                        if (firstUncompleted.relatedNPCs == this.id)
                        {
                            QuestManager.Instance.Complete_objective(active_quest, firstUncompleted);
                        }    
                    }
                    else
                    {
                        DialogueManager.Instance.StartDialogue(dialogue);
                    }
                }
            }
            else
            {
                Debug.Log("121");
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }
        
    }

    public string GetHintToInteract()
    {
        return "Нажмите E, чтобы поговорить с <color=yellow>" + npcName + " </color>";
    }
}