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
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        StartCoroutine(MoveVisitor());
    }

    IEnumerator MoveVisitor()
    {
        while (true)
        {
            anim.SetTrigger("idle");
            Vector3 visitorPosition = new Vector3(transform.position.x, guideToFollow.position.y, transform.position.z);
            float distanceToGuide = Vector3.Distance(guideToFollow.position, visitorPosition);

            if (distanceToGuide > distanceWithGuide)
            {
                Vector3 direction = (guideToFollow.position - transform.position).normalized;
                Vector3 nextPos = guideToFollow.position - direction * moveSpeed;
                anim.SetTrigger("walking");
                agent.SetDestination(nextPos);
            }

            yield return new WaitForSeconds(timeToMove);
        }
    }
}
