using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideMovement : MonoBehaviour
{
    [SerializeField] private Transform entry;
    [SerializeField] private Transform[] destinations;
    [SerializeField] private float timeToChange;

    private Queue<Transform> path;
    private NavMeshAgent agent;

    // TO DO: Create a array of external destinations
    // TO DO: Link to Guide State
    // TO DO: Start waiting time when arrived at the destination

    void Start()
    {
        path = new Queue<Transform>();

        agent = GetComponent<NavMeshAgent>();

        InitializePath();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (path.Count > 0)
        {
            Transform nextPosition = path.Dequeue();

            agent.SetDestination(nextPosition.position);

            yield return new WaitForSeconds(timeToChange);
        }

        yield break;
    }

    void InitializePath()
    {
        // Set Entry level as first destination
        path.Enqueue(entry);

        // Randomize destiantions array
        destinations = Shuffle(destinations);

        // Adding all destinations
        for (int i = 0; i < destinations.Length; i++)
        {
            path.Enqueue(destinations[i]);
        }

        // Set Entry level as last destination
        path.Enqueue(entry);
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
