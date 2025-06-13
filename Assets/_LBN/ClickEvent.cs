using UnityEngine;

public class ClickEvent : MonoBehaviour
{
  
    public static bool IsGamePaused = false;

   

    void Update()
    {
        if (IsGamePaused) return;

        
        //if (input.touchcount > 0 && input.gettouch(0).phase == touchphase.began)
        //{
        //    onclick();
        //}

        
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
                PlayerStat stat = GameManager.Instance.player.stat;
                float power = stat.plantMastery * ( 1 + (stat.pmLevel * 0.2f));

                hit.collider.GetComponent<Plant>().GrowPlant(power);
            }
            else
            {
                return;
            }
        }
    }
}
