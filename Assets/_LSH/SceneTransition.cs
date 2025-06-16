using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour  // 씬 전환용 클래스
{
    public Image fadePanel;  //페이드인, 아웃 연출용 이미지

    private void Awake()
    {
        fadePanel.color = new Color(0, 0, 0, 1);
    }

    private void Start()
    {
        fadePanel.DOFade(0, 1f).OnComplete(() =>   //fadePanel가 사라진 후 fadePanel - false
        {
            fadePanel.gameObject.SetActive(false);
        });
    }

    public void FadeAndLoadScene(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, 1f).OnComplete(() =>
        {
            GameManager.Instance.gameObject.SetActive(false);
            SceneManager.LoadScene(sceneName);
        });
    }
}