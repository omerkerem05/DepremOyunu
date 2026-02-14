using UnityEngine;

[CreateAssetMenu(menuName = "AFAD/NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcID;
    public string displayName;
    public Sprite portrait;
}
