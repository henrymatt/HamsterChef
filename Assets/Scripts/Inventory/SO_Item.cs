using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class SO_Item : ScriptableObject
{
    public string itemName;
    public Image image;
    public GameObject worldGO;
}
