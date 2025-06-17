using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public GameObject[] bugPrefabs;            // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ 
    public Transform plantTransform;        // ï¿½ï¿½Ç¥ ï¿½Ä¹ï¿½
    public Transform plantShelfTransform; //ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡

    private Plant currentPlant;

    private List<BugController> spawnedBugs = new List<BugController>();

    private int surviveDays = 0; //ï¿½ï¿½Â¥ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ìµï¿½ ï¿½ï¿½ï¿½ï¿½
    public float difficultyIncreaseInterval = 10f; //ï¿½ï¿½Æ³ï¿½ï¿½ï¿½ ï¿½Ï¼ï¿½ ï¿½ß°ï¿½ï¿½ï¿½ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½, ï¿½ï¿½ï¿½Ìµï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    private float dayTimer = 0f; //ï¿½ï¿½Â¥ï¿½ï¿½ ï¿½Ñ¾î°¡ï¿½ï¿½ ï¿½Ã°ï¿½

    public float spawnInterval = 5f; //ï¿½ï¿½ï¿½ï¿½ ï¿½Ã°ï¿½ ï¿½ï¿½
    private float spawnTimer = 0f;

    private bool isWaitingNextRound = true;
    private float roundWaitTimer = 0f;
    public float roundWaitDuration = 10f; // ï¿½ï¿½ï¿½ï¿½ ï¿½Ñ¾î°¥ ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½Ã°ï¿½

    public int totalBugStack = 0;
    public int pollutionLv = 10;

    private bool isProcessingBugCheck = false;

    private void Start()
    {
        surviveDays = GameManager.Instance.GetSaveData().surviveDays;   //ë°ì´í„° ë¡œë“œ
    }

    private void Update()
    {
        //ï¿½ï¿½ï¿½ï¿½ ï¿½ß°ï¿½
        //Å¸ï¿½Ì¸Óºï¿½ï¿½ï¿½ Å¸ï¿½Ì¸Ó°ï¿½ ï¿½ï¿½ï¿½Ê°ï¿½ ï¿½Ç¸ï¿½ SpawnBug() ï¿½ï¿½ï¿½ï¿½
        //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        currentPlant = GameManager.Instance.plantManager.pot.GetPlant();

        if(currentPlant == null || currentPlant.plantData == null)
        {
            return;
        }

        GetSurviveDays();
        GetBugStack();

        if (isWaitingNextRound)
        {
            roundWaitTimer += Time.deltaTime;
            if (roundWaitTimer >= roundWaitDuration)
            {
                isWaitingNextRound = false;
                roundWaitTimer = 0f;

            }
            return; // ï¿½ï¿½ï¿½ ï¿½ß¿ï¿½ï¿½ï¿½ ï¿½Æ·ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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
            GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //ë°ì´í„° ì €ì¥
            
            // ï¿½ï¿½ï¿½Ìµï¿½ ï¿½ï¿½ï¿½ï¿½ Ã³ï¿½ï¿½(ï¿½ï¿½ï¿½ï¿½ï¿½Ã°ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Â°ï¿½,ï¿½ï¿½ï¿½Çµï¿½ ï¿½Ã¾î³²)
            spawnInterval = Mathf.Max(1f, spawnInterval - 0.1f); // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½       

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
                    spawnedBugs.RemoveAt(i); // ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
                    bug.Die();               // ï¿½ï¿½ï¿½ï¿½ Ã³ï¿½ï¿½
                }
            }

            spawnedBugs.Clear();
            totalBugStack = 0;
            surviveDays--;
            isWaitingNextRound = true;
            GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //ë°ì´í„° ì €ì¥
            GameManager.Instance.uiManager.PlantDestroyClearUI();
        }

        isProcessingBugCheck = false;
    }

    Vector3 GetRandomSpawnPosition(PestType pestType)
    {
        if (pestType == PestType.OxygenLooter)
        {
            float spawnX = plantShelfTransform.position.x - 1f; // ï¿½ï¿½ï¿½İºï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
            float spawnY = plantShelfTransform.position.y + Random.Range(-2f, 2f); // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Æ·ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡
            return new Vector3(spawnX, spawnY, 0f);
        }

        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float x = Random.Range(bottomLeft.x, topRight.x);
        float y;

        // ï¿½ï¿½ï¿½ï¿½ ï¿½Ç´ï¿½ ï¿½Æ·ï¿½ï¿½Ê¿ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ (50% È®ï¿½ï¿½)
        bool spawnAbove = Random.value > 0.5f;
        float offset = 1f; // Ä«ï¿½Ş¶ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ó¸¶³ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½

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

    private void OxygenLooterWarning()
    {
        //»ê¼Ò °­Å»ÀÚ°¡ ÀÖÀ¸¸é ¿ŞÂÊ¾ÆÀÌÄÜ¿¡ »ê¼Ò ¹ú·¹ ¶¹´Ù´Â °æ°í½ÇÇà
        //¾ê¸¦ ¾îµğ´Ù ³ö¾ß Àß³ù´Ù´Â ¼Ò¸®¸¦ µéÀ»±î
    }
}
//surviveDays
// surviveDays = GameManager.Instance.GetSaveData().surviveDays;   //ë°ì´í„° ë¡œë“œ
//GameManager.Instance.saveLoadManager.SetSaveData("SurviveDays", surviveDays);  //ë°ì´í„° ì €ì¥