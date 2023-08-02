using UnityEngine;
using StageSystem;

public class CameraFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float zoomSpeed = 5f; // ÉYÅ[ÉÄÇÃë¨ìx
    public float zoomValue;

    private Camera cam;
    private CameraMove cameramove;
    private void Start()
    {
        cam = GetComponent<Camera>();
        cameramove = GetComponent<CameraMove>();
    }

    void Update()
    {
        if (cameramove.AllMap) return;

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            cam.orthographicSize = zoomValue;
        }
    }
}