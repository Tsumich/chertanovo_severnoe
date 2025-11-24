// DialogueSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Game/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public bool isReaded;

    [System.Serializable]
    public class DialogueLine
    {
        public NPCDataSO speaker; 
        [TextArea(3, 5)] public string text; 

    }

    public DialogueLine[] dialogueLines;
}