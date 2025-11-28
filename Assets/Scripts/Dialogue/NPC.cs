using UnityEngine;
using TMPro;
using System.Linq;

public class NPC : MonoBehaviour
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

    void Start()
    {
        // Автоматически создаем восклицательный знак
        CreateExclamationMark();
        UpdateExclamationMark();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
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
            UpdateExclamationMark();
        }
    }

    void CreateExclamationMark()
    {
        // Если уже есть - выходим
        if (exclamationMark != null) return;

        // Создаем объект для "!"
        exclamationMark = new GameObject("ExclamationMark");
        exclamationMark.transform.SetParent(transform);
        exclamationMark.transform.localPosition = exclamationOffset;

        // Добавляем TextMeshPro
        TextMeshPro tmp = exclamationMark.AddComponent<TextMeshPro>();
        tmp.text = "!";
        tmp.color = exclamationColor;
        tmp.fontSize = exclamationSize;
        tmp.alignment = TextAlignmentOptions.Center;

        // Важные настройки для видимости
        tmp.rectTransform.sizeDelta = new Vector2(2, 2);
        tmp.enableWordWrapping = false;

        // Добавляем поворот к камере
        exclamationMark.AddComponent<SimpleBillboard>();

        // По умолчанию скрываем
        exclamationMark.SetActive(true);
    }

    // Метод для обновления восклицательного знака
    public void UpdateExclamationMark()
    {
        if (exclamationMark != null)
        {
            // Показываем "!" только если hasDialog = true
            exclamationMark.SetActive(hasDialog);
        }
    }

    void TryStartDialogue()
    {
        
        if (dialogue != null && !DialogueManager.Instance.isInDialogue )
        {
            if (QuestManager.Instance.activeQuest)
            {
                if (this.id == 1) DialogueManager.Instance.StartDialogue(QuestManager.Instance.activeQuest.KalinaLine);
                else 
                {
                    QuestSO active_quest = QuestManager.Instance.activeQuest;
                    QuestObjective firstUncompleted = active_quest.objectives
                        .FirstOrDefault(objective => 
                            !objective.isCompleted && objective.relatedNPCs == this.id);
                    if (firstUncompleted != null)
                    {
                        DialogueManager.Instance.StartDialogue(firstUncompleted.startDialogue);
                        if (firstUncompleted.objectiveType == ObjectiveType.TalkToNPC)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log($"Подойди к {npcName} - нажми E");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (DialogueManager.Instance.isInDialogue)
            {
                DialogueManager.Instance.isDialogueEnding = true;
            }
            //Debug.Log($"Отошли от {npcName}");
        }
    }
}