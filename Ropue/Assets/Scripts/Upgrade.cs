using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [TextArea(5,5)]
    public string Explain;
    public Sprite sprite;
    public int cost;
    public UpgradeState state;
}
