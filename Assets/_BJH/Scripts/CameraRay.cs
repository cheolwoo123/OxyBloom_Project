using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public float rayDistance = 10f;
    public Canvas mainCanvas;
    public Canvas shopCanvas;
    public Canvas showcaseCanvas;
    private enum UIMove { main, showcase, shop }
    private UIMove currentState = UIMove.main;


    void Update()
    {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenCenter);

           


        bool showcaseLeft = false;
        bool shopRight = false;

            RaycastHit2D hitshop = Physics2D.Raycast(worldPoint, Vector2.right, rayDistance);
        if (hitshop.collider != null && hitshop.collider.CompareTag("Shop"))
        {
            shopCanvas.gameObject.SetActive(true);
            shopRight = true;
        }
        else
        {
            shopCanvas.gameObject.SetActive(false);
        }

        RaycastHit2D hitshowcase = Physics2D.Raycast(worldPoint, Vector2.left, rayDistance);
        if(hitshowcase.collider !=null && hitshowcase.collider.CompareTag("Showcase"))
        {
            showcaseCanvas.gameObject.SetActive(true);
            showcaseLeft = true;
        }
        else
        {
            showcaseCanvas.gameObject.SetActive(false);
        }

        
        if (showcaseLeft && currentState != UIMove.showcase)
        {
            SwitchUI(UIMove.showcase);
        }
        else if (shopRight && currentState != UIMove.shop)
        {
            SwitchUI(UIMove.shop);
        }
        else if (!showcaseLeft && !shopRight && currentState != UIMove.main)
        {
            SwitchUI(UIMove.main);
        }


    }

    private void SwitchUI(UIMove newState)
    {
        mainCanvas.gameObject.SetActive(newState == UIMove.main);
        showcaseCanvas.gameObject.SetActive(newState == UIMove.showcase);
        shopCanvas.gameObject.SetActive(newState == UIMove.shop);

        currentState = newState;
    }
}
