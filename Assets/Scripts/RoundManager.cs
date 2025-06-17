using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public GameObject[] bugPrefabs;            // ���� ������ 
    public Transform plantTransform;        // ��ǥ �Ĺ�
    public Transform plantShelfTransform; //���� ��ġ

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

    public int totalBugStack = 0;
    public int pollutionLv = 10;

    private bool isProcessingBugCheck = false;

    private void Start()
    {
        surviveDays = GameManager.Instance.GetSaveData().surviveDays;   //데이터 로드
    }

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
        GetBugStack();

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
            GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //데이터 저장
            
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

        PestType pestType = bugPrefabs[index].GetComponent<BugController>().entity.bugData.pestType;
        Vector3 spawnPos = GetRandomSpawnPosition(pestType);

        GameObject bugObj = Instantiate(bugPrefabs[index], spawnPos, Quaternion.identity);
        BugController bugCtrl = bugObj.GetComponent<BugController>();
        
        BugScriptObject pestData = bugCtrl.entity.bugData;
        bugCtrl.Setup(pestData, plantTransform);
        
        spawnedBugs.Add(bugCtrl);
    }

    private void CheckSpawnedBugs()
    {
        isProcessingBugCheck = true;

        totalBugStack = 0;
        foreach (var bug in spawnedBugs)
        {
            if (bug == null) continue;
            totalBugStack += bug.entity.bugData.bugStack;
        }

        if (totalBugStack >= pollutionLv && GameManager.Instance.plantManager.pot.GetPlant().GrowthStage != 3)
        {
            if (currentPlant != null)
                currentPlant.RemovePlant();

            for (int i = spawnedBugs.Count - 1; i >= 0; i--)
            {
                if (spawnedBugs[i] != null)
                {
                    var bug = spawnedBugs[i];
                    spawnedBugs.RemoveAt(i); // ����Ʈ���� ����
                    bug.Die();               // ���� ó��
                }
            }

            spawnedBugs.Clear();
            totalBugStack = 0;
            surviveDays--;
            isWaitingNextRound = true;
            GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //데이터 저장
            GameManager.Instance.uiManager.PlantDestroyClearUI();
        }

        isProcessingBugCheck = false;
    }

    Vector3 GetRandomSpawnPosition(PestType pestType)
    {
        if (pestType == PestType.OxygenLooter)
        {
            float spawnX = plantShelfTransform.position.x - 1f; // ���ݺ��� ���� �� ����
            float spawnY = plantShelfTransform.position.y + Random.Range(-2f, 2f); // ������ �������� ���Ʒ��� ���� ��ġ
            return new Vector3(spawnX, spawnY, 0f);
        }

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

    private void GetBugStack()
    {
        GameManager.Instance.uiManager.DisPlayBugStack(totalBugStack,pollutionLv);
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