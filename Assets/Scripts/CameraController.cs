using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;

    [SerializeField] private float moveTime = 0.5f;

    private Vector2 currVel;
    private float currZoomSpeed;

    private Vector2 targetPos;
    private float targetZoom = 5;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Vector2 target = Vector2.SmoothDamp(transform.position, targetPos, ref currVel, moveTime);
        transform.position = new Vector3(target.x, target.y, transform.position.z);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref currZoomSpeed, moveTime);
    }

    public static void SetTarget(Vector2 pos, float zoom)
    {
        instance.targetPos = pos;
        instance.targetZoom = zoom;
    }
}
