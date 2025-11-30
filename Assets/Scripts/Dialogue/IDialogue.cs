using UnityEngine;
using static ThroughtsSO;

public interface IDialogueSO {
    DialogueLine GetDialogueLine(int line);
    int GetLenght();
}

[System.Serializable]
public class DialogueLine
{
    public NPCDataSO speaker;

    [TextArea(3, 5)] public string text;

}

[CreateAssetMenu(fileName = "New inspect information", menuName = "Dialogue/Throughts")]
public class ThroughtsSO : ScriptableObject, IDialogueSO
{
    public DialogueLine[] dialogueLines;

    public DialogueLine GetDialogueLine(int line_number)
    {
        return this.dialogueLines[line_number];
    }

    public int GetLenght() { 
        return dialogueLines.Length;
    }
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueSO : ScriptableObject, IDialogueSO
{
    public bool isReaded;

    public DialogueLine[] dialogueLines;

    public DialogueLine GetDialogueLine(int line_number)
    {
        return this.dialogueLines[line_number];
    }

    public int GetLenght()
    {
        return dialogueLines.Length;
    }
}