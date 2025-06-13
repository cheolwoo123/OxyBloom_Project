using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject bugPrefab;            // 버그 프리팹 연결
    public BugScriptObject bugData;         // 생성할 버그 데이터
    public Transform plantTransform;        // 목표 식물

    void Start()
    {
        SpawnBug();
    }

    void SpawnBug()
    {
        GameObject bugObj = Instantiate(bugPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        BugController bugCtrl = bugObj.GetComponent<BugController>();
        bugCtrl.Setup(bugData, plantTransform);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float x = Random.Range(bottomLeft.x, topRight.x); 
        float y;

        // 위쪽 또는 아래쪽에서 생성 (50% 확률)
        bool spawnAbove = Random.value > 0.5f;
        float offset = 2f; // 카메라 밖으로 얼마나 떨어져서 생성할지

        if (spawnAbove)
        {
            y = topRight.y + offset;
        }
        else
        {
            y = bottomLeft.y - offset;
        }

        return new Vector3(x, y, 0f);
    }
}
