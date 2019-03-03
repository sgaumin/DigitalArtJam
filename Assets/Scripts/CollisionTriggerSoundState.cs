using UnityEngine;

public class CollisionTriggerSoundState : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("Salles", "Renferme");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        AkSoundEngine.SetState("Salles", "Ouvert");
    }
}