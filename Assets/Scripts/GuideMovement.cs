using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideMovement : MonoBehaviour
{
    private GuideState guideState = GuideState.Talk;

    [HideInInspector] public Transform nextPosition;

    private Transform[] destinationsFirstRoomTemp;
    private Transform[] destinastionsSecondRoomTemp;
    private Transform[] destinationsThirdRoomTemp;

    public bool goToCorridor;

    private Queue<Transform> path;
    private NavMeshAgent agent;

    void Start()
    {
        path = new Queue<Transform>();

        agent = GetComponent<NavMeshAgent>();

        goToCorridor = Random.value < 0.5;

        InitializePath();

        StartCoroutine(GuideMove());
    }

    IEnumerator GuideMove()
    {
        while (path.Count > 0)
        {
            // Retrieve next destination
            nextPosition = path.Dequeue();

            // Set the next destination
            if (guideState == GuideState.Talk)
            {
                agent.SetDestination(nextPosition.position);
                guideState = GuideState.Walking;
            }

            if (guideState == GuideState.Walking)
            {
                // Check if Player is arrived to his next destination
                while (!isArrivedToDestination(nextPosition.position))
                {
                    yield return null;
                }

                // Update Guide State
                guideState = GuideState.Talk;

                yield return new WaitForSeconds(GuideManager.instance.timeToChange);
            }
        }
    }

    // Method which detects if the Guide is arrived to destination
    bool isArrivedToDestination(Vector3 target)
    {
        Vector3 guidePosition = new Vector3(transform.position.x, target.y, transform.position.z);
        float distanceToTarget = Vector2.Distance(target, guidePosition);

        if (distanceToTarget < 0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void InitializePath()
    {
        // Set Entry level as first destination
        path.Enqueue(GuideManager.instance.entry);

        // Randomize destiantions array
        destinationsFirstRoomTemp = Shuffle(GuideManager.instance.destinationsFirstRoom);
        destinastionsSecondRoomTemp = Shuffle(GuideManager.instance.destinationsSecondRoom);
        destinationsThirdRoomTemp = Shuffle(GuideManager.instance.destinationsThirdRoom);

        // --- Adding all destinations
        // Room 1
        for (int i = 0; i < GuideManager.instance.numberDestinationsPerRoom; i++)
        {
            path.Enqueue(destinationsFirstRoomTemp[i]);
        }

        if (!goToCorridor)
        {
            // Room 2
            for (int i = 0; i < GuideManager.instance.numberDestinationsPerRoom; i++)
            {
                path.Enqueue(destinastionsSecondRoomTemp[i]);
            }
        }
        else
        {
            for (int i = 0; i < GuideManager.instance.numberDestinationsPerRoom; i++)
            {
                path.Enqueue(GuideManager.instance.destinationsCorridor[i]);
            }
        }

        // Room 3
        for (int i = 0; i < GuideManager.instance.numberDestinationsPerRoom; i++)
        {
            path.Enqueue(destinationsThirdRoomTemp[i]);
        }

        // Set Entry level as last destination
        path.Enqueue(GuideManager.instance.exit);
    }

    // Method in order to Shuffle detinations array
    Transform[] Shuffle(Transform[] transformArray)
    {
        for (int t = 0; t < transformArray.Length; t++)
        {
            Transform tmp = transformArray[t];
            int r = Random.Range(t, transformArray.Length);
            transformArray[t] = transformArray[r];
            transformArray[r] = tmp;
        }

        return transformArray;
    }

    // Destroy
    public void DestroyGuide()
    {
        Destroy(gameObject, 4f);
    }
}
