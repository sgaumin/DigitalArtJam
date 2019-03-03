using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GuideManagerScriptableObject", order = 1)]
public class GuideManagerScriptableObject : ScriptableObject
{
    public float talkRange;
    public float captureSpeed;
    public float resetSpeed;
    public float chanceToGoToCorridor;
}
