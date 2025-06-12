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
            if (!IsPointerOverUIObject(Input.GetTouch(0).position))
            {
                OnClick();
            }
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        Debug.Log("화면 클릭됨 (UI 제외, 일시정지 아님)");
        // 여기에 클릭 시 실행할 로직 추가
    }

    bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
