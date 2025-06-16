using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenCenter);

            Vector2 direction = Vector2.up; // 또는 Vector2.up, Vector2.left 등

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, direction, rayDistance);

        if (hit.collider != null)
        {
            Debug.Log("Hit 2D: " + hit.collider.name);
        }
        else
        {
            Debug.Log("No 2D hit");
        }
    }
}
