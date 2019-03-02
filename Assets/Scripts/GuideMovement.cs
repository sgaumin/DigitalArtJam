using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideMovement : MonoBehaviour
{
    [SerializeField] private Transform[] destinations; // Use Queue instead ?
    [SerializeField] private float timeToChange;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Transform nextPosition = destinations[Random.Range(0, destinations.Length)];

            agent.SetDestination(nextPosition.position);

            yield return new WaitForSeconds(timeToChange);
        }
    }
}
