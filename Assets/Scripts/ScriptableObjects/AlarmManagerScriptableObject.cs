using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AlarmManagerScriptableObject", order = 1)]
public class AlarmManagerScriptableObject : ScriptableObject
{
    public int nbWordsRequired;
    public float lowerAlarmSpeed;
    public float guideAlarmSpeed;
    public float visitorsAlarmSpeedTier1;
    public float visitorsAlarmSpeedTier2;
    public float visitorsAlarmSpeedTier3;
    public int visitorsTier2;
    public int visitorsTier3;
    public float alertThreeshold;
}