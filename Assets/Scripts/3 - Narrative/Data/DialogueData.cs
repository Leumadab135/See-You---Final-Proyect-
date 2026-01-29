using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Dialogue",
    menuName = "Narrative/Dialogue"
)]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> Lines;
}

