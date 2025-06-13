using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public Image fadePanel;

    private void Awake()
    {
        fadePanel.color = new Color(0, 0, 0, 1);
    }

    private void Start()
    {
        fadePanel.DOFade(0, 1f).OnComplete(() =>
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