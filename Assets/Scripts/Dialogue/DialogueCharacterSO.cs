using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Dialogue/Character")]
public class DialogueCharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite characterPortrait;
    public AudioClip characterVoice;
}
