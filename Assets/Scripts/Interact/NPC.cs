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

    public void Interact()
    {
        TryStartDialogue();          
    }

    void TryStartDialogue()
    {
            if (QuestManager.Instance.activeQuest)
            {
                Debug.Log("has active");
                if (this.id == 1 && QuestManager.Instance.activeQuest.KalinaLine != null) DialogueManager.Instance.StartDialogue(QuestManager.Instance.activeQuest.KalinaLine);
                else 
                {
                    QuestSO active_quest = QuestManager.Instance.activeQuest;
                    QuestObjective firstUncompleted = active_quest.objectives
                        .FirstOrDefault(objective => 
                            !objective.isCompleted);
                    if (firstUncompleted.objectiveType != ObjectiveType.TalkToNPC) return;
                    if (firstUncompleted != null)
                    {
                        DialogueManager.Instance.StartDialogue(firstUncompleted.startDialogue, () =>
                        {
                            if (firstUncompleted.relatedNPCs == this.id)
                            {
                                QuestManager.Instance.Complete_objective(active_quest, firstUncompleted);
                            }
                        });
                          
                    }
                    else
                    {
                        DialogueManager.Instance.StartDialogue(dialogue);
                    }
                }
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