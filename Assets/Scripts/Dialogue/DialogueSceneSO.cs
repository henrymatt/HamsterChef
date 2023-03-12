using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "Dialogue/Scene")]
public class DialogueSceneSO : ScriptableObject
{
    public DialogueCharacterSO speakingCharacter;
    public string dialogue;
}
