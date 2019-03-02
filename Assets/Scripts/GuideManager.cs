using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public static GuideManager instance {get; private set;}

    public float timeToChange;
    public int numberDestinationsPerRoom;
    public Transform entry;
    public Transform exit;

    public Transform[] destinationsFirstRoom;
    public Transform[] destinastionsSecondRoom;
    public Transform[] destinationsThirdRoom;

    private void Awake()
    {
        instance = this;
    }
}
