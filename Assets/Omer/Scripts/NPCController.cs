using UnityEngine;

public class NPCController : MonoBehaviour
{
    private CaseData currentCase;
    private int currentNodeIndex = 0;

    public void Init(CaseData caseData)
    {
        currentCase = caseData;
        currentNodeIndex = 0;
        ShowCurrentNode();
    }

    void ShowCurrentNode()
    {
        DialogueNode node = currentCase.dialogueNodes[currentNodeIndex];
        UIManager.Instance.ShowDialogueNode(node, OnChoiceSelected);
    }

    void OnChoiceSelected(DialogueChoice choice)
    {
        if (!string.IsNullOrEmpty(choice.flagToSet))
            GameState.Instance.SetFlag(choice.flagToSet);

        if (choice.nextNodeIndex < 0)
        {
            EndDialogue();
        }
        else
        {
            currentNodeIndex = choice.nextNodeIndex;
            ShowCurrentNode();
        }
    }

    void EndDialogue()
    {
        if (currentCase.hasChecklist)
        {
            ChecklistManager.Instance.StartChecklist(OnChecklistFinished);
        }
        else
        {
            Exit();
        }
    }

    void OnChecklistFinished(bool success)
    {
        if (success)
            GameState.Instance.SetFlag("checklist_success");
        else
            GameState.Instance.SetFlag("checklist_fail");

        Exit();
    }

    void Exit()
    {
        FindObjectOfType<NPCQueueManager>().ShowNextNPC();
        Destroy(gameObject);
    }
}
