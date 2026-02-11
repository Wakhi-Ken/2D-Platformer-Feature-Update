using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    public Transform target;   // 👈 drag player here in Inspector

    private float offsetZ;
    private Vector3 currentVelocity;
    private bool followsPlayer;

    void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCol.bounds;
    }

    void Start()
    {
        // Safety check so Unity doesn't crash again
        if (target == null)
        {
            Debug.LogError("CameraFollow: No Player assigned!");
            return;
        }

        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    void FixedUpdate()
    {
        if (!followsPlayer || target == null) return;

        Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;

        if (aheadTargetPos.x >= transform.position.x)
        {
            Vector3 newCameraPosition = Vector3.SmoothDamp(
                transform.position,
                aheadTargetPos,
                ref currentVelocity,
                cameraSpeed
            );

            transform.position = new Vector3(
                newCameraPosition.x,
                transform.position.y,
                newCameraPosition.z
            );
        }
    }
}
