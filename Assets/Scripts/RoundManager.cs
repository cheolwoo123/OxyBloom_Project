using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] bugPrefabs;            // 버그 프리팹 
    public Transform plantTransform;        // 목표 식물

    private Plant currentPlant;

    private int surviveDays = 0; //날짜에 따라 난이도 변경
    public float difficultyIncreaseInterval = 10f; //살아남은 일수 추가해주는 조건, 난이도 증가 조건
    private float dayTimer = 0f; //날짜가 넘어가는 시간

    public float spawnInterval = 5f;
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = false;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // 라운드 넘어갈 때 대기 시간

    

    void Update()
    {
        //조건 추가
        //타이머변수 타이머가 몇초가 되면 SpawnBug() 실행
        //일차마다 배경 변경
        currentPlant = GameManager.Instance.plantManager.pot.GetPlant();

        if(currentPlant == null)
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

            // 난이도 증가 처리(스폰시간 빨라지는거,스피드 늘어남, 개채수 다양하게 등장)
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f); // 스폰이 빨라짐

            isWaitingNextRound = true;
        }

    }
    void SpawnBug()
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

    private void GetSurviveDays()
    {
        GameManager.Instance.uiManager.DisplayDays(surviveDays);
    }

    private void GetRoundTime()
    {
        GameManager.Instance.uiManager.DisplayWaveTime(difficultyIncreaseInterval - dayTimer);
    }
}
