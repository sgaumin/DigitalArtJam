using System;
using UnityEngine;


[ExecuteInEditMode]
public class RaycastVision : MonoBehaviour
{
    public Transform PlayerTransform;

    [SerializeField] private float sightDistance = 2f;
    [SerializeField] private float visionHeight = 1.5f;
    [SerializeField] private float visionAngle = 20f;
    
    public event Action OnSeePlayer = delegate { };
    
    private void Update()
    {
        if (CanSeePlayer())
            OnSeePlayer();
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

        int layerMask = 1 << 10;
        if ((Vector3.Angle(rayDirection, startVecFwd)) < visionAngle &&
            Physics.Raycast(startVec, rayDirection, out hit, sightDistance, layerMask))
        {
            // Detect if player is within the field of view
                Debug.Log("Can see player");
                return true;
        }

        return false;
    }
}