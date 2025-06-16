using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 2f;
    public float maxDistance = 10f; // 이동 가능한 최대 거리
    public Vector3 originPosition = Vector3.zero;
    private Vector3 dragOrigin;

    void Start()
    {
        originPosition = transform.position;
    }

void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 worldBefore = Camera.main.ScreenToWorldPoint(new Vector3(dragOrigin.x, dragOrigin.y, Camera.main.transform.position.y));
            Vector3 worldNow = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

            Vector3 difference = worldBefore - worldNow;
            Vector3 move = new Vector3(difference.x, 0, difference.z); // Y축 고정

            Vector3 nextPosition = transform.position + move;

            Vector3 flatOrigin = new Vector3(originPosition.x, 0, originPosition.z);
            Vector3 flatNext = new Vector3(nextPosition.x, 0, nextPosition.z);

            if (Vector3.Distance(flatOrigin, flatNext) <= maxDistance)
            {
                transform.position = nextPosition;
            }

            dragOrigin = Input.mousePosition;
        }
    }
}
