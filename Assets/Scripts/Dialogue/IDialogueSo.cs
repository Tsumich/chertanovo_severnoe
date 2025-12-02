using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueSO : ScriptableObject, IDialogueSO
{
    public bool isReaded;

    public DialogueLine[] dialogueLines;

    public DialogueLine GetDialogueLine(int line_number)
    {
        return this.dialogueLines[line_number];
    }

    void Start()
    {
        void OnEnable() // или в Awake() если это MonoBehaviour
        {
            Debug.Log($"[{Time.time}] {name} загружен, тип: {GetType().Name}");
            Debug.Log($"  dialogueLines: {(dialogueLines == null ? "NULL" : $"Length={dialogueLines.Length}")}");
        }
    }

    public int GetLenght()
    {
        if (dialogueLines == null)
            return 0;

        return dialogueLines.Length;
    }
}