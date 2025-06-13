using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
  
    public static bool IsGamePaused = false;

   

    void Update()
    {
        if (IsGamePaused) return;

        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnClick();
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }


    void OnClick()
    {
        
       Vector2 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       RaycastHit2D hit = Physics2D.Raycast(worldpos, Vector2.zero);

        if (hit.collider.gameObject != null)
        {
            if(hit.collider.gameObject.tag == "Flower")
            {
                hit.collider.GetComponent<Plant>().GrowPlant(100);
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}
