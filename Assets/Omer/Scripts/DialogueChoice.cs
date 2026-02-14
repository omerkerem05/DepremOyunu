using System;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextNodeIndex;   // Hangi node’a geçilecek
    public string flagToSet;    // Seçilince set edilecek flag
}
