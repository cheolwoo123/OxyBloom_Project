using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] bugPrefabs;            // ���� ������ 
    public Transform plantTransform;        // ��ǥ �Ĺ�

    private Plant currentPlant;

    private List<BugController> spawnedBugs = new List<BugController>();

    private int surviveDays = 0; //��¥�� ���� ���̵� ����
    public float difficultyIncreaseInterval = 10f; //��Ƴ��� �ϼ� �߰����ִ� ����, ���̵� ���� ����
    private float dayTimer = 0f; //��¥�� �Ѿ�� �ð�

    public float spawnInterval = 5f; //���� �ð� ��
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = true;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // ���� �Ѿ �� ��� �ð�

    public int totalBug = 0;

    private void Update()
    {
        //���� �߰�
        //Ÿ�̸Ӻ��� Ÿ�̸Ӱ� ���ʰ� �Ǹ� SpawnBug() ����
        //�������� ��� ����
        currentPlant = GameManager.Instance.plantManager.pot.GetPlant();

        if(currentPlant == null || currentPlant.plantData == null)
        {
            return;
        }

        GetSurviveDays();
        GetRoundTime();

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

            // ���̵� ���� ó��(�����ð� �������°�,���ǵ� �þ)
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f); // ������ ������       

            //isWaitingNextRound = true;
        }


        CheckSpawnedBugs();
    }
    private void SpawnBug()
    {
        
        if (currentPlant == null || currentPlant.plantData == null)
        {
            return;
        }

        int index = 0;
        if (surviveDays < 1)
            index = 0;
        else if (surviveDays < 3)
            index = Random.Range(0, 1);
        else
            index = Random.Range(0, bugPrefabs.Length);

        GameObject bugObj = Instantiate(bugPrefabs[index], GetRandomSpawnPosition(), Quaternion.identity);
        BugController bugCtrl = bugObj.GetComponent<BugController>();
        
        BugScriptObject pestData = bugCtrl.entity.bugData;
        bugCtrl.Setup(pestData, plantTransform);
        
        spawnedBugs.Add(bugCtrl);
    }

    private void CheckSpawnedBugs()
    {
        totalBug = 0;
        foreach (var bug in spawnedBugs)
        {
            if(bug == null) continue;

            totalBug += bug.entity.bugData.bugStack;
        }

        if(totalBug >= 10 && GameManager.Instance.plantManager.pot.GetPlant().GrowthStage != 3) //�� ������ 10�̰� ����ܰ谡 3�� �ƴϸ� ����
        {
            if(currentPlant !=  null)
            {
                currentPlant.RemovePlant(); //�Ĺ� �ı�
            }

            foreach (var bug in spawnedBugs)
            {
                if(bug != null)
                {
                    bug.Die();
                }
            }

            spawnedBugs.Clear(); //������ ���� ����Ʈ �ʱ�ȭ
            surviveDays--;
            //roundWaitTimer = 0;
            //difficultyIncreaseInterval = 10f;
            //GetRoundTime();
            //GetSurviveDays();
            isWaitingNextRound = true;
        }
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
        float offset = 1f; // ī�޶� ������ �󸶳� �������� ��������

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

    private void GetSurviveDays()
    {
        GameManager.Instance.uiManager.DisplayDays(surviveDays);
    }

    private void GetRoundTime()
    {
        GameManager.Instance.uiManager.DisplayWaveTime(difficultyIncreaseInterval - dayTimer);
    }

    public void RemoveBug(BugController bug)
    {
        if(spawnedBugs.Contains(bug))
        {
            spawnedBugs.Remove(bug);
        }
    }

    public void StartNextRound()
    {
        isWaitingNextRound = false;
        dayTimer = 0;
        spawnTimer = 0f;
    }
}
//surviveDays
// surviveDays = GameManager.Instance.GetSaveData().surviveDays;   //데이터 로드
//GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //데이터 저장