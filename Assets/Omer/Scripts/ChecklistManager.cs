using UnityEngine;
using UnityEngine.UI;
using System;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance;

    public Toggle su;
    public Toggle fener;
    public Toggle pil;
    public Toggle ilkyardim;

    private Action<bool> onComplete;

    private void Awake()
    {
        Instance = this;
    }

    public void StartChecklist(Action<bool> callback)
    {
        gameObject.SetActive(true);
        onComplete = callback;
    }

    public void OnSubmit()
    {
        bool correct =
            su.isOn &&
            fener.isOn &&
            pil.isOn &&
            ilkyardim.isOn;

        gameObject.SetActive(false);

        onComplete?.Invoke(correct);
    }
}
