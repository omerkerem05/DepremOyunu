using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AFAD/Case")]
public class CaseData : ScriptableObject
{
    public string caseID;
    public NPCData npc;

    public List<DialogueNode> dialogueNodes;

    public bool isPreEarthquake;

    public string requiredFlag;   // Gelmesi için gerekli flag
    public string flagToSet;      // Bitince set edilecek flag

    public bool hasChecklist;
}
