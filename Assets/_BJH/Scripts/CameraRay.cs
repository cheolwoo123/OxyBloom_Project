using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenCenter);

            Vector2 direction = Vector2.up;

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, direction, rayDistance);

        if (hit.collider != null)
        {
        }
        else
        {
        }
    }
}
