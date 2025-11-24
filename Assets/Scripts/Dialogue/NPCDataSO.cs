// NPCDataSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Data", menuName = "Game/NPC Data")]
public class NPCDataSO : ScriptableObject
{
    public string npcName;        // "Ирен", "Калина"
    public Sprite avatar;         // Аватар для диалогов
    public string npсDescription;  // Описание персонажа
    // Можно добавить: голос, анимации, характер и т.д.
}