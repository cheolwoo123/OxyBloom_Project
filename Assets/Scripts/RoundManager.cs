using System.Collections.Generic;
using UnityEngine;

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

    public float spawnInterval = 8f; //���� �ð� ��
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = true;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // ���� �Ѿ �� ��� �ð�

    public int totalBugStack = 0;
    public int pollutionLv = 10;

    private bool isProcessingBugCheck = false;
    private bool isRoundInitialized = false;

    public void InitRound() // ���̺� �ε� ���Ŀ� ȣ��
    {
        currentPlant = GameManager.Instance.plantManager.pot.GetPlant();
        isWaitingNextRound = false;
        dayTimer = 0f;
        spawnTimer = 0f;
    }

    private void Update()
    {
        currentPlant = GameManager.Instance.plantManager.pot.GetPlant();

        if (!isRoundInitialized)
        {
            currentPlant = GameManager.Instance.plantManager.pot.GetPlant();
            if (currentPlant != null && currentPlant.plantData != null)
            {
                InitRound();
                isRoundInitialized = true;
            }
            else
            {
                return; // ���� �÷�Ʈ ������ �ƹ��͵� �� ��
            }
        }

        GetSurviveDays();
        //GetBugStack();

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
            spawnInterval = Mathf.Max(2f, spawnInterval - 0.05f); // ������ ������       

            if (surviveDays % 5 == 0) //5������ �ѹ���
            {
                UpgradeSpawnedBugs();
            }

            isWaitingNextRound = true;
        }


        CheckSpawnedBugs();
        //OxygenLooterWarning(); ��ҹ��� ���� ��� ���� �߰�
    }
    private void SpawnBug()
    {

        if (currentPlant == null || currentPlant.plantData == null)
        {
            return;
        }

        int index;
        if (surviveDays < 1)
            index = 0;
        else if (surviveDays < 3)
            index = Random.Range(0, 1);
        else
            index = Random.Range(0, bugPrefabs.Length);

        Debug.Log(index+"-�ε��� ��ȣ");
        PestType pestType = bugPrefabs[index].GetComponent<BugController>().entity.bugData.pestType;
        Vector3 spawnPos = GetRandomSpawnPosition(pestType);

        GameObject bugObj = Instantiate(bugPrefabs[index], spawnPos, Quaternion.identity);
        BugController bugCtrl = bugObj.GetComponent<BugController>();
        
        BugScriptObject pestData = bugCtrl.entity.bugData;
        bugCtrl.Setup(pestData, plantTransform);

        // ���̵��� ���� �ɷ�ġ ����
        int levelMulti = surviveDays / 5;
        float hpMulti = 1f + (levelMulti * 0.1f);    // surviveDays �� ü�� 10% ����
        float speedMulti = 1f + (levelMulti * 0.05f); // surviveDays �� �ӵ� 5% ����

        bugCtrl.entity.SetHP(pestData.maxHP * hpMulti);
        bugCtrl.entity.SetSpeed(pestData.speed * speedMulti);

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


    //private void GetBugStack()
    //{
    //    GameManager.Instance.uiManager.DisPlayBugStack(totalBugStack,pollutionLv);
    //}

   
    public void StartNextRound()
    {
        isWaitingNextRound = false;
        dayTimer = 0;
        spawnTimer = 0f;
    }

    private void OxygenLooterWarning()
    {
        // ��� ��Ż�ڰ� �ϳ��� �����ϸ� true
        bool hasOxygenLooter = false;

        foreach (var bug in spawnedBugs)
        {
            if (bug == null || bug.entity == null) continue;

            if (bug.entity.bugData.pestType == PestType.OxygenLooter)
            {
                hasOxygenLooter = true;
                break;
            }
        }

        // UIManager�� OxygenLooter ��� �����ִ� �Լ� ȣ�� (�������� ���߿� ����)
        //GameManager.Instance.uiManager.
    }


    private void UpgradeSpawnedBugs()
    {
        float hpIncreaseRate = 0.1f;   // ü�� 10% ����
        float speedIncreaseRate = 0.05f; // �ӵ� 5% ����

        foreach (var bug in spawnedBugs)
        {
            if (bug == null) continue;

            BugEntity entity = bug.entity;
            if (entity == null) continue;

            // ���� HP ��� ���� (�⺻ maxHP ��� ������ ����)
            float newHP = entity.GetHP() * (1 + hpIncreaseRate);
            entity.SetHP(newHP);

            float newSpeed = entity.GetSpeed() * (1 + speedIncreaseRate);
            entity.SetSpeed(newSpeed);
        }
    }

}
