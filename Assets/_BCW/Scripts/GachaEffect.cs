using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaEffect : MonoBehaviour
{
    [Header("백그라운드 배경")]
    public SpriteRenderer darkPanel; // 배경 SpriteRenderer
    public float fadeDuration = 0.5f;

    [Header("등급별 이펙트")]
    public GameObject commonEffect;
    public GameObject rareEffect;
    public GameObject epicEffect;
    public GameObject legendaryEffect;
    public GameObject mysteryEffect;

    public bool isPlaying { get; private set; }

    private Dictionary<PotGrade, GameObject> gradeToEffect;
    private Color originalColor;
    private PotGrade potGrades = PotGrade.Common;

    private void Awake()
    {
        
        originalColor = darkPanel.color;

        gradeToEffect = new Dictionary<PotGrade, GameObject>
        {
            { PotGrade.Common, commonEffect },
            { PotGrade.Rare, rareEffect },
            { PotGrade.Epic, epicEffect },
            { PotGrade.Legendary, legendaryEffect },
            { PotGrade.Mystery, mysteryEffect }
        };

        foreach (var eff in gradeToEffect.Values)
            eff.SetActive(false);

        // 시작 시 배경은 원래 색으로 유지
        darkPanel.color = originalColor;
    }

    public void PlayEffect(PotGrade grade)
    {
        if (isPlaying) return;
        StartCoroutine(GachaSequence(grade));
    }

    private IEnumerator GachaSequence(PotGrade finalGrade)
    {
        isPlaying = true;

        // 1. 원래 색상 → 검정색 페이드
        yield return StartCoroutine(FadePanelColor(originalColor, new Color(0f, 0f, 0f, 1.0f)));

        // 2. 랜덤 등급 이펙트 셔플
        int shuffleCount = 6;
        float delay = 1.0f;

        for (int i = 0; i < shuffleCount; i++)
        {
            //HideAllEffects();
            // PotGrade random = GetRandomGrade();
            // ShowEffect(random);       
            // yield return new WaitForSeconds(delay);          
            PotGrade random = GetRandomGrade();
            ShowEffect(random);
            yield return new WaitForSeconds(delay);
            HideAllEffects(); 
          
        }

        // 3. 최종 등급 이펙트 보여주기
        HideAllEffects();
        Debug.Log("1");
        ShowEffect(finalGrade);
        yield return new WaitForSeconds(5f);
        Debug.Log("2");
        HideAllEffects();

        // 4. 검정 → 원래 색상 복구
        yield return StartCoroutine(FadePanelColor(darkPanel.color, originalColor));

        isPlaying = false;
    }

    private void ShowEffect(PotGrade grade)
    {
        if (gradeToEffect.TryGetValue(grade, out var prefab))
            prefab.SetActive(true);
    }

    private void HideAllEffects()
    {
        foreach (var eff in gradeToEffect.Values)
            eff.SetActive(false);
    }

    private IEnumerator FadePanelColor(Color from, Color to)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            darkPanel.color = Color.Lerp(from, to, t);
            yield return null;
        }

        darkPanel.color = to;
    }

    private PotGrade GetRandomGrade()
    {
        PotGrade[] grades = {
            PotGrade.Common, PotGrade.Rare, PotGrade.Epic, PotGrade.Legendary, PotGrade.Mystery
        };
        PotGrade potGrade;
        do
        {        
                potGrade = grades[Random.Range(0, grades.Length)];
        } while (potGrade == potGrades);

       potGrades = potGrade;
        return potGrades;
        
    }
}
