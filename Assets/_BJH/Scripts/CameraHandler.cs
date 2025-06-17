using UnityEngine;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    public float SlideSpeed;

    [Header("화면 이동 버튼")]
    public Button LeftBtn;
    public Button RightBtn;

    [Header("이동할 좌표")]
    public Vector3 PlantPosition;
    public Vector3 SheifPosition;
    public Vector3 GachaPosition;

    [Header("이동한 화면 캔버스")]
    public Canvas PlantCanvas;
    public Canvas SheifCanvas;
    public Canvas GachaCanvas;

    private Vector3 targetPosition;

    private bool isSliding = false;

    void Start()
    {
        LeftBtn.onClick.AddListener(LeftSliding);
        RightBtn.onClick.AddListener(RiightSliding);
    }

    void Update()
    {
        if (isSliding)
        {
            transform.position = Vector3.MoveTowards
            (transform.position, targetPosition, SlideSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isSliding = false;
            }
        }
    }

    public void LeftSliding()
    {
        isSliding = true;

        if (transform.position == PlantPosition)
        {
            targetPosition = SheifPosition;
            LeftBtn.gameObject.SetActive(false);
            RightBtn.gameObject.SetActive(true);

            PlantCanvas.gameObject.SetActive(false);
            GachaCanvas.gameObject.SetActive(false);
            SheifCanvas.gameObject.SetActive(true);

        }
        else if (transform.position == GachaPosition)
        {
            targetPosition = PlantPosition;
            RightBtn.gameObject.SetActive(true);
           
            PlantCanvas.gameObject.SetActive(true);
            GachaCanvas.gameObject.SetActive(false);
            SheifCanvas.gameObject.SetActive(false);
        }
    }

    public void RiightSliding()
    {
        isSliding = true;

        if (transform.position == PlantPosition)
        {
            targetPosition = GachaPosition;
            RightBtn.gameObject.SetActive(false);
            LeftBtn.gameObject.SetActive(true);

            PlantCanvas.gameObject.SetActive(false);
            GachaCanvas.gameObject.SetActive(true);
            SheifCanvas.gameObject.SetActive(false);
        }
        else if (transform.position == SheifPosition)
        {
            targetPosition = PlantPosition;
            LeftBtn.gameObject.SetActive(true);

            PlantCanvas.gameObject.SetActive(true);
            GachaCanvas.gameObject.SetActive(false);
            SheifCanvas.gameObject.SetActive(false);
        }
    }
}