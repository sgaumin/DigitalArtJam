using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GuideMovement guide = other.GetComponent<GuideMovement>();

        if (guide != null)
        {
            guide.DestroyGuide();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
