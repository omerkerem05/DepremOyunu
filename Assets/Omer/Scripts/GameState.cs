using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    private HashSet<string> flags = new HashSet<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetFlag(string flag)
    {
        flags.Add(flag);
    }

    public bool HasFlag(string flag)
    {
        return flags.Contains(flag);
    }
}
