using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    public Transform guideToFollow;
    [SerializeField] private float distanceWithGuide;
    [SerializeField] private float timeToMove;
    [SerializeField] private float moveSpeed;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(MoveVisitor());
    }

    IEnumerator MoveVisitor()
    {
        while (true)
        {
            Vector3 visitorPosition = new Vector3(transform.position.x, guideToFollow.position.y, transform.position.z);
            float distanceToGuide = Vector3.Distance(guideToFollow.position, visitorPosition);


            Vector3 direction = (guideToFollow.position - transform.position).normalized;
            Vector3 nextPos = guideToFollow.position - direction * moveSpeed;

            agent.SetDestination(nextPos);


            yield return new WaitForSeconds(timeToMove);
        }
    }
}
