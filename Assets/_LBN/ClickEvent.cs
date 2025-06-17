using UnityEngine;

public class ClickEvent : MonoBehaviour
{
  
    public static bool IsGamePaused = false;
    public GameObject clickEffect;


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
        if (clickEffect != null)
        {
            Instantiate(clickEffect, worldpos, Quaternion.identity);
        }
        RaycastHit2D hit = Physics2D.Raycast(worldpos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Flower"))
            {
                PlayerStat stat = GameManager.Instance.player.stat;
                float power = 10 + (stat.pmLevel * 0.2f);

                Debug.Log($"PM Level: {stat.pmLevel}, Power: {power}");

                hit.collider.GetComponent<Plant>().GrowPlant(power);
            }
            else if (hit.collider.gameObject.CompareTag("Bug"))
            {
                BugController bug = hit.collider.GetComponent<BugController>();
                
                if (bug != null)
                {
                    PlayerStat stat = GameManager.Instance.player.stat;
                    float damage = stat.atkLevel;
                    bug.TakeDamage((int)damage);
                }
            }
        }
    }
}
