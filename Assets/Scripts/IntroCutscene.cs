using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class IntroCutscene : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] phrases = {
        "Чертаново - тихий, почти мёртвый городок, где время остановилось.",
        "Из последних работающих - только архив с пыльными тайнами да одно-единственное кафе.",
        "И ты - разнорабочий Кирилл, готовый взяться за любую работу..."
    };

    public float letterDelay = 0.05f;
    public float phraseDelay = 3f;
    public string nextScene = "Scenes/chertanovo";

    void Start()
    {
        StartCoroutine(RunCutscene());
    }

    IEnumerator RunCutscene()
    {
        foreach (string phrase in phrases)
        {
            // Печатаем фразу
            dialogueText.text = "";
            foreach (char letter in phrase)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(letterDelay);
            }

            // Ждем перед следующей фразой
            yield return new WaitForSeconds(phraseDelay);
        }

        // Загружаем игровую сцену
        SceneManager.LoadScene(nextScene);
    }

    void Update()
    {
        // Пропуск по любой клавише
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(nextScene);
        }
    }
}