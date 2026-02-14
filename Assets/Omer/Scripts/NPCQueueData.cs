using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AFAD/NPC Queue")]
public class NPCQueueData : ScriptableObject
{
    public List<CaseData> preEarthquakeCases;
    public List<CaseData> postEarthquakeCases;
}
