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

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Flower"))
            {
                PlayerStat stat = GameManager.Instance.player.stat;
                float power = stat.plantMastery * (1 + (stat.pmLevel * 0.2f));

                hit.collider.GetComponent<Plant>().GrowPlant(power);
            }
            else if (hit.collider.gameObject.CompareTag("Bug"))
            {
                BugController bug = hit.collider.GetComponent<BugController>();
                
                if (bug != null)
                {
                    PlayerStat stat = GameManager.Instance.player.stat;
                    float damage = stat.attack;
                    bug.TakeDamage((int)damage);
                }
            }
        }
    }
}
