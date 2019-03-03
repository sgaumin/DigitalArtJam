using UnityEngine;

public class CorridorCollisionTriggerSoundState : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("Salles", "Renferme");
        AkSoundEngine.PostEvent("Set_Filter_On_Rtpc", gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("Salles", "Ouvert");
        AkSoundEngine.PostEvent("Reset_Game_Parameter_Filter_On_Rtpc", gameObject);
    }
}
