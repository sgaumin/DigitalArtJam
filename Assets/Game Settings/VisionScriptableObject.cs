using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VisionScriptableObject", order = 1)]
public class VisionScriptableObject : ScriptableObject
{
    public float sightDistance;
    public float visionHeight;
    public float visionAngle;
    public float checkDelay;
}
