using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Dialogue Elements")]
    public GameObject questInfo;
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questObjectiveText;

    [Header("Last actions or events (yellow panel)")]
    public TextMeshProUGUI yellowPanelText;

    [Header("Show current FPS")]
    public TextMeshProUGUI fpsText;
    private float deltaTime = 0.0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Проверяем PlayerInteraction.Instance
        if (PlayerInteraction.Instance != null)
        {
            locationText.text = "Чертаново Северное: Кафе " + PlayerInteraction.Instance.lastLocation;
        }
        else
        {
            locationText.text = "Чертаново Северное: Кафе";
        }

        // Проверяем Inventory.Instance
        if (Inventory.Instance != null)
        {
            Inventory.Instance.OnCoinsValueEvent += OnCoinsValue;
            Inventory.Instance.OnTakenItemEvent += OnNewItem;
        }
        else
        {
            Debug.LogError("Inventory.Instance is null in HUDManager!");
        }
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        if (fpsText != null)
        {
            fpsText.text = string.Format("FPS: {0:0.}", fps);
        }
    }

    private void OnCoinsValue(int value)
    {
        yellowPanelText.text = "Получено монет: " + value;
    }

    private void OnNewItem(string item_name)
    {
        yellowPanelText.text = "Вы взяли предмет: " + item_name;
    }

    public void ShowQuest(QuestSO quest)
    {
        questInfo.SetActive(true);
        QuestObjective next_objective = quest.objectives
                .FirstOrDefault(objective => !objective.isCompleted);
        if (next_objective == null)
        {
            Debug.Log("Квест завершен - все задачи выполнены");
            HUDManager.Instance.ClearHUD();
            return;
        }
        if (questTitleText != null)
        {
            questTitleText.text = quest.questName;
            questTitleText.gameObject.SetActive(true);
        }

        
        questObjectiveText.text = next_objective.description;
        questObjectiveText.gameObject.SetActive(true);

        Debug.Log($"HUD: Показан квест '{quest.questName}'");
        Debug.Log($"HUD: Показан квест '{next_objective.description}'");
    }

    public void ClearHUD()
    {
        if (questTitleText != null)
        {
            questInfo.SetActive(false);
            //questTitleText.text = "";
            //questTitleText.gameObject.SetActive(false);
        }

        if (questObjectiveText != null)
        {
            questObjectiveText.text = "";
            questObjectiveText.gameObject.SetActive(false);
        }
    }

    public void SetLocation(string locationName)
    {
        if (locationText != null)
            locationText.text = locationName;
    }
} 