using UnityEngine;

public class Script3 : MonoBehaviour
{
    [SerializeField] private script1 tableArea;
    [SerializeField] private Script2[] draggableItems;

    private void Awake()
    {
        if (tableArea == null)
        {
            Debug.LogError("Script3: tableArea atanmadi.");
            return;
        }

        if (draggableItems == null || draggableItems.Length == 0)
        {
            Debug.LogWarning("Script3: draggableItems listesi bos.");
            return;
        }

        foreach (Script2 item in draggableItems)
        {
            if (item != null)
            {
                item.SetTable(tableArea);
            }
        }
    }
}
