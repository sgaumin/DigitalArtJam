using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideMovement : MonoBehaviour
{
    private GuideState guideState = GuideState.Talk;

    [SerializeField] private float timeToChange;
    [SerializeField] private int numberDestinationsPerRoom;
    [SerializeField] private Transform entry;
    [SerializeField] private Transform exit;

    [SerializeField] private Transform[] destinationsFirstRoom;
    [SerializeField] private Transform[] destinationsSecondRoom;
    [SerializeField] private Transform[] destinationsThirdRoom;

    private Queue<Transform> path;
    private NavMeshAgent agent;

    // TO DO: Create a array of external destinations

    void Start()
    {
        path = new Queue<Transform>();

        agent = GetComponent<NavMeshAgent>();

        InitializePath();

        StartCoroutine(GuideMove());
    }

    IEnumerator GuideMove()
    {
        while (path.Count > 0)
        {
            // Retrieve next destination
            Transform nextPosition = path.Dequeue();

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

                Debug.Log(isArrivedToDestination(nextPosition.position));

                // Update Guide State
                guideState = GuideState.Talk;

                yield return new WaitForSeconds(timeToChange);
            }
        }
    }

    // Method which detects if the Guide is arrived to destination
    bool isArrivedToDestination(Vector3 target)
    {
        Vector3 playerPosition = new Vector3(transform.position.x, target.y, transform.position.z);
        float distanceToTarget = Vector2.Distance(target, playerPosition);

        if (distanceToTarget < Mathf.Epsilon)
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
        path.Enqueue(entry);

        // Randomize destiantions array
        destinationsFirstRoom = Shuffle(destinationsFirstRoom);
        destinationsSecondRoom = Shuffle(destinationsSecondRoom);
        destinationsThirdRoom = Shuffle(destinationsThirdRoom);

        // Adding all destinations

        // Room 1
        for (int i = 0; i < numberDestinationsPerRoom; i++)
        {
            path.Enqueue(destinationsFirstRoom[i]);
        }

        // Room 2
        for (int i = 0; i < numberDestinationsPerRoom; i++)
        {
            path.Enqueue(destinationsSecondRoom[i]);
        }

        // Room 3
        for (int i = 0; i < numberDestinationsPerRoom; i++)
        {
            path.Enqueue(destinationsThirdRoom[i]);
        }

        // Set Entry level as last destination
        path.Enqueue(exit);
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
}
