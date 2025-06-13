using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject bugPrefab;            // ���� ������ ����
    public BugScriptObject bugData;         // ������ ���� ������
    public Transform plantTransform;        // ��ǥ �Ĺ�

    private int surviveDays = 0; //��¥�� ���� ���̵� ����
    public float difficultyIncreaseInterval = 10f; //��Ƴ��� �ϼ� �߰����ִ� ����, ���̵� ���� ����
    private float dayTimer = 0f; //��¥�� �Ѿ�� �ð�

    public float spawnInterval = 3.5f;
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = false;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // ���� �Ѿ �� ��� �ð�

    void Update()
    {
        //���� �߰�
        //Ÿ�̸Ӻ��� Ÿ�̸Ӱ� ���ʰ� �Ǹ� SpawnBug() ����
        //�������� ��� ����

        if (isWaitingNextRound)
        {
            roundWaitTimer += Time.deltaTime;
            if (roundWaitTimer >= roundWaitDuration)
            {
                isWaitingNextRound = false;
                roundWaitTimer = 0f;

            }
            return; // ��� �߿��� �Ʒ� �������� ����
        }

        dayTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnBug();
        }

        if (dayTimer >= difficultyIncreaseInterval)
        {
            dayTimer = 0f;
            surviveDays++;

            // ���̵� ���� ó��(�����ð� �������°�,���ǵ� �þ, ��ä�� �پ��ϰ� ����)
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.5f); // ������ ������

            isWaitingNextRound = true;
        }

    }
    void SpawnBug()
    {
        Plant currentPlant = GameManager.Instance.plantManager.pot.GetPlant();
        if (currentPlant == null || currentPlant.plantData == null)
        {
            return;
        }

        plantTransform = currentPlant.transform;

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

    public int GetSurviveDays()
    {
        return surviveDays;
    }
}
