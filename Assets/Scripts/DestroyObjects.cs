using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GuideMovement guide = other.GetComponent<GuideMovement>();
        Visitor visitor = other.GetComponent<Visitor>();

        if (guide != null)
        {
            guide.DestroyGuide();
        }
        else if(visitor != null)
        {
            Destroy(other.gameObject);
        }
    }
}
