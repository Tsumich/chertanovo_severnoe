using UnityEngine;
using TMPro;

public class BulletinBoard : MonoBehaviour
{
    [Header("Ссылка на квест")]
    public QuestSO questData;

    [Header("Ссылка на текст")]
    public TextMeshPro notificationText;
    public GameObject particle;

    [Header("Настройки индикатора")]
    public string availableIcon = "!";
    public string completedIcon = "✓";

    void Start()
    {
        UpdateNotification();
    }

    void UpdateNotification()
    {
        if (notificationText == null || questData == null) return;

        if (questData.currentState == QuestState.NotStarted)
        {
            notificationText.text = availableIcon;
            notificationText.color = Color.red;
            notificationText.gameObject.SetActive(true);
            particle.SetActive(true);
        }
        else if (questData.currentState == QuestState.InProgress)
        {
            notificationText.text = completedIcon;
            notificationText.color = Color.blue;
            notificationText.gameObject.SetActive(true);
            particle.SetActive(false);
        }
        else
        {
            notificationText.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (questData == null) return;

        if (questData.currentState == QuestState.NotStarted)
        {

            QuestManager.Instance.AcceptQuest(questData);
            // Обновляем визуал доски
            UpdateNotification();

            // ПОКАЗЫВАЕМ КВЕСТ В HUD!
            QuestHUDManager.Instance.ShowQuest(questData);

            Debug.Log($"Принят квест: {questData.questName}");
        }
    }
}