using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class VentilationMiniGame : MonoBehaviour
{
    public static VentilationMiniGame Instance;

    [Header("Настройки")]
    public int targetClicks = 15;
    public float timeLimit = 55f;

    [Header("UI Elements")]
    public TextMeshProUGUI clickCounterText;
    public TextMeshProUGUI timerText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int currentClicks = 0;
    private float currentTime = 0f;
    private bool gameActive = false;

    private GameObject player;

    void Awake()
    {
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    void Start()
    {
        gameActive = true;
        currentTime = timeLimit;
        UpdateUI();
    }

    void Update()
    {
        if (gameActive)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();

            if (currentTime <= 0)
            {
                EndGame(false); // Время вышло
            }
        }
    }

    public void RegisterClick()
    {
        if (!gameActive) return;

        currentClicks++;
        UpdateUI();

        if (currentClicks >= targetClicks)
        {
            EndGame(true); // Победа!
        }
    }

    void UpdateUI()
    {
        // Клики
        if (clickCounterText != null)
            clickCounterText.text = $"Кликов: {currentClicks}/{targetClicks}";

        // Таймер
        if (timerText != null)
            timerText.text = $"Время: {Mathf.CeilToInt(currentTime)} сек";
    }

    void EndGame(bool success)
    {
        gameActive = false;
        resultPanel.SetActive(true);
        resultText.text = success ? "Вентиляция прочищена!" : "Время вышло!";
        Invoke("ReturnToMainScene", 3f);
        player.SetActive(true);

        if (success) {
            if (QuestManager.Instance.activeQuest)
            { 
                QuestObjective firstUncompleted = QuestManager.Instance.activeQuest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);

                if (firstUncompleted.objectiveType == ObjectiveType.UseMiniGame)
                {
                    firstUncompleted.isCompleted = true;
                    //QuestHUDManager.Instance.CheckActiveQuests();
                }

            }
        }
    }

    void ReturnToMainScene()
    {
        SceneManager.LoadScene("Library");
    }
}
