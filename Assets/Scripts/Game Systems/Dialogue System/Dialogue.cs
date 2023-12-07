using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

[System.Serializable]
public class DialogueCharacter
{
    public Sprite characterIcon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(5, 10)]
    public string dialogueLine;
}