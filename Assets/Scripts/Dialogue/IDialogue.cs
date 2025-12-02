using UnityEngine;

public interface IDialogueSO
{
    DialogueLine GetDialogueLine(int line);
    int GetLenght();
}

[System.Serializable]
public class DialogueLine
{
    public NPCDataSO speaker;

    [TextArea(3, 5)] public string text;

}

