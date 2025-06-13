using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject bugPrefab;            // ���� ������ ����
    public BugScriptObject bugData;         // ������ ���� ������
    public Transform plantTransform;        // ��ǥ �Ĺ�

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

        // ���� �Ǵ� �Ʒ��ʿ��� ���� (50% Ȯ��)
        bool spawnAbove = Random.value > 0.5f;
        float offset = 2f; // ī�޶� ������ �󸶳� �������� ��������

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
