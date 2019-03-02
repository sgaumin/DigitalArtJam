using System;
using System.Collections;
using UnityEngine;


[ExecuteInEditMode]
public class RaycastVision : MonoBehaviour
{
    public Transform PlayerTransform;

    [SerializeField] private float sightDistance = 2f;
    [SerializeField] private float visionHeight = 1.5f;
    [SerializeField] private float visionAngle = 20f;
    [SerializeField] private float checkDelay = 0.1f;

    private Guid guid;
    
    private void Start()
    {
        guid = Guid.NewGuid();
        StartCoroutine(PlayerCheckRoutine());
    }

    private IEnumerator PlayerCheckRoutine()
    {
        while (GameSystem.instance.gameState == GameState.Playing)
        {
            if (CanSeePlayer())
            {
                PlayerManager.instance.OnSeenPlayer(tag, guid);
            }
            else
            {
                PlayerManager.instance.OnPlayerLost(tag, guid);
            }
            yield return new WaitForSeconds(checkDelay);
        }
    }

    private bool CanSeePlayer()
    {
        Transform tr = transform;
        Vector3 startVec = tr.position;
        startVec.y += visionHeight;
        Vector3 startVecFwd = tr.forward;
        // startVecFwd.y += visionHeight;

        RaycastHit hit;
        Vector3 rayDirection = PlayerTransform.position - startVec;
        rayDirection.y = 0f;

        float rotationOffset = tr.eulerAngles.y;

        Vector3 frontLineVec = startVec + startVecFwd * sightDistance;

        Vector3 leftLineVec = new Vector3(
            Mathf.Cos((visionAngle + 90f - rotationOffset) * Mathf.Deg2Rad),
            0f,
            Mathf.Sin((visionAngle + 90f - rotationOffset) * Mathf.Deg2Rad));
        leftLineVec *= sightDistance;
        leftLineVec += startVec;

        Vector3 rightLineVec = new Vector3(
            Mathf.Cos((-visionAngle + 90f - rotationOffset) * Mathf.Deg2Rad),
            0f,
            Mathf.Sin((-visionAngle + 90f - rotationOffset) * Mathf.Deg2Rad));
        rightLineVec *= sightDistance;
        rightLineVec += startVec;

        Debug.DrawLine(startVec, leftLineVec, Color.red);
        Debug.DrawLine(startVec, frontLineVec, Color.green);
        Debug.DrawLine(startVec, rightLineVec, Color.blue);

        // Bit shift the index of the layer (2) to get a bit mask
        int layerMask = 1 << 2;

        // This would cast rays only against colliders in layer 2.
        // But instead we want to collide against everything except layer 2. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if ((Vector3.Angle(rayDirection, startVecFwd)) < visionAngle &&
            Physics.Raycast(startVec, rayDirection, out hit, sightDistance, layerMask))
        {
            // Detect if player is within the field of view
            return hit.transform.CompareTag("Player");
        }

        return false;
    }
}