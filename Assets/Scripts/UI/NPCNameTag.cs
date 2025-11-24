using UnityEngine;
using TMPro;

public class NPCNameTag : MonoBehaviour
{
    [Header("Тексты над NPC")]
    public string npcName = "Калина"; // Меняйте в инспекторе
    public Vector3 offset = new Vector3(0, 2.2f, 0); // Над головой

    void Start()
    {
        CreateNameTags();
    }

    void CreateNameTags(bool hasDialogue = false)
    {
        // Создаем контейнер для текстов
        GameObject textContainer = new GameObject("NPCTexts");
        textContainer.transform.SetParent(transform);
        textContainer.transform.localPosition = offset;

        // 1. Имя NPC (повыше)
        GameObject nameTag = new GameObject("Name");
        nameTag.transform.SetParent(textContainer.transform);
        nameTag.transform.localPosition = new Vector3(0, 2, 0);

        TextMeshPro nameText = nameTag.AddComponent<TextMeshPro>();
        nameText.text = npcName;
        nameText.color = Color.white;
        nameText.fontSize = 3;
        nameText.alignment = TextAlignmentOptions.Center;


        GameObject test = new GameObject("Has Dialog");
        test.transform.SetParent(textContainer.transform);
        test.transform.localPosition = new Vector3(0, -0.3f, 0); // ОПУСКАЕМ ниже

        TextMeshPro testtext = test.AddComponent<TextMeshPro>();
        testtext.text = "!";
        testtext.color = Color.white;
        testtext.fontSize = 5;
        testtext.alignment = TextAlignmentOptions.Center;


        // Добавляем поворот к камере
        textContainer.AddComponent<SimpleBillboard>();
    }
}

// Простой поворот к камере
public class SimpleBillboard : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}