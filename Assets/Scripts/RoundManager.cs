using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] bugPrefabs;            // 버그 프리팹 
    public Transform plantTransform;        // 목표 식물
    public Transform plantShelfTransform; //선반 위치

    private Plant currentPlant;

    private List<BugController> spawnedBugs = new List<BugController>();

    private int surviveDays = 0; //날짜에 따라 난이도 변경
    public float difficultyIncreaseInterval = 10f; //살아남은 일수 추가해주는 조건, 난이도 증가 조건
    private float dayTimer = 0f; //날짜가 넘어가는 시간

    public float spawnInterval = 8f; //스폰 시간 텀
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = true;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // 라운드 넘어갈 때 대기 시간

    public int totalBugStack = 0;
    public int pollutionLv = 10;

    private bool isProcessingBugCheck = false;
    private bool isRoundInitialized = false;

    public void InitRound() // 세이브 로드 이후에 호출
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
                return; // 아직 플랜트 없으면 아무것도 안 함
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
            return; // 대기 중에는 아래 실행하지 않음
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

            // 난이도 증가 처리(스폰시간 빨라지는거,스피드 늘어남)
            spawnInterval = Mathf.Max(2f, spawnInterval - 0.05f); // 스폰이 빨라짐       

            if (surviveDays % 5 == 0) //5일차에 한번씩
            {
                UpgradeSpawnedBugs();
            }

            isWaitingNextRound = true;
        }


        CheckSpawnedBugs();
        //OxygenLooterWarning(); 산소벌레 출현 경고 추후 추가
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

        Debug.Log(index+"-인덱스 번호");
        PestType pestType = bugPrefabs[index].GetComponent<BugController>().entity.bugData.pestType;
        Vector3 spawnPos = GetRandomSpawnPosition(pestType);

        GameObject bugObj = Instantiate(bugPrefabs[index], spawnPos, Quaternion.identity);
        BugController bugCtrl = bugObj.GetComponent<BugController>();
        
        BugScriptObject pestData = bugCtrl.entity.bugData;
        bugCtrl.Setup(pestData, plantTransform);

        // 난이도에 따라 능력치 보정
        int levelMulti = surviveDays / 5;
        float hpMulti = 1f + (levelMulti * 0.1f);    // surviveDays 당 체력 10% 증가
        float speedMulti = 1f + (levelMulti * 0.05f); // surviveDays 당 속도 5% 증가

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
                    spawnedBugs.RemoveAt(i); // 리스트에서 제거
                    bug.Die();               // 죽음 처리
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
            float spawnX = plantShelfTransform.position.x - 1f; // 선반보다 조금 더 왼쪽
            float spawnY = plantShelfTransform.position.y + Random.Range(-2f, 2f); // 선반을 기준으로 위아래로 랜덤 위치
            return new Vector3(spawnX, spawnY, 0f);
        }

        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float x = Random.Range(bottomLeft.x, topRight.x);
        float y;

        // 위쪽 또는 아래쪽에서 생성 (50% 확률)
        bool spawnAbove = Random.value > 0.5f;
        float offset = 1f; // 카메라 밖으로 얼마나 떨어져서 생성할지

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
        // 산소 강탈자가 하나라도 존재하면 true
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

        // UIManager에 OxygenLooter 경고 보여주는 함수 호출 (아이콘은 나중에 연결)
        //GameManager.Instance.uiManager.
    }


    private void UpgradeSpawnedBugs()
    {
        float hpIncreaseRate = 0.1f;   // 체력 10% 증가
        float speedIncreaseRate = 0.05f; // 속도 5% 증가

        foreach (var bug in spawnedBugs)
        {
            if (bug == null) continue;

            BugEntity entity = bug.entity;
            if (entity == null) continue;

            // 현재 HP 대비 증가 (기본 maxHP 대비 증가도 가능)
            float newHP = entity.GetHP() * (1 + hpIncreaseRate);
            entity.SetHP(newHP);

            float newSpeed = entity.GetSpeed() * (1 + speedIncreaseRate);
            entity.SetSpeed(newSpeed);
        }
    }

}
