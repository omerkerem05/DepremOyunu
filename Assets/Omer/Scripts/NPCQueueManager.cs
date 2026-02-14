using System.Collections.Generic;
using UnityEngine;

public class NPCQueueManager : MonoBehaviour
{
    public NPCQueueData queueData;

    private Queue<CaseData> activeQueue = new Queue<CaseData>();
    private bool isPostEarthquake = false;

    private void Start()
    {
        LoadPreEarthquakeQueue();
        ShowNextNPC();
    }

    void LoadPreEarthquakeQueue()
    {
        activeQueue.Clear();
        foreach (var c in queueData.preEarthquakeCases)
        {
            activeQueue.Enqueue(c);
        }
    }

    void LoadPostEarthquakeQueue()
    {
        activeQueue.Clear();
        foreach (var c in queueData.postEarthquakeCases)
        {
            // Flag kontrolü
            if (string.IsNullOrEmpty(c.requiredFlag) || 
                GameState.Instance.HasFlag(c.requiredFlag))
            {
                activeQueue.Enqueue(c);
            }
        }
    }

    public void TriggerEarthquake()
    {
        isPostEarthquake = true;
        LoadPostEarthquakeQueue();
        ShowNextNPC();
    }

    public void ShowNextNPC()
    {
        if (activeQueue.Count == 0)
        {
            Debug.Log("Queue finished");
            return;
        }

        CaseData currentCase = activeQueue.Dequeue();

        // Flag kontrolü
        if (!string.IsNullOrEmpty(currentCase.requiredFlag) &&
            !GameState.Instance.HasFlag(currentCase.requiredFlag))
        {
            ShowNextNPC();
            return;
        }

        ShowCase(currentCase);
    }

    void ShowCase(CaseData caseData)
    {
        Debug.Log("NPC: " + caseData.npc.displayName);
        Debug.Log("Dialogue: " + caseData.dialogueText);

        if (!string.IsNullOrEmpty(caseData.flagToSet))
        {
            GameState.Instance.SetFlag(caseData.flagToSet);
        }
    }
}
