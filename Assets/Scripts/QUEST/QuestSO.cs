using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    [Header("Основная информация")]                    // Заголовок в инспекторе
    public string questID;                             // Уникальный ID для поиска ("library_cleanup")
    public string questName;                           // Отображаемое имя ("Уборка в библиотеке")
    [TextArea] public string description;              // Многострочное поле для описания
    public DialogueSO KalinaLine;

    [Header("Статус (меняется в runtime)")]
    public QuestState currentState = QuestState.NotStarted; // Текущее состояние квеста

    [Header("Цели квеста")]
    public QuestObjective[] objectives;
}

public enum QuestState
{
    NotStarted,    
    InProgress,    
    Completed,    
    Failed         
}

public enum ObjectiveType
{
    TalkToNPC,      // Поговорить с NPC
    GoToLocation,   // Прийти в локацию
    CollectItem,    // Найти предмет
    UseMiniGame     // Пройти мини-игру
}

[System.Serializable]                                  
public class QuestObjective
{
    public ObjectiveType objectiveType;

    public int relatedNPCs;
    public int triggerLocation;

    public string objectiveID;                       
    public string description;     
    
    public bool isCompleted;

    public DialogueSO startDialogue;
}