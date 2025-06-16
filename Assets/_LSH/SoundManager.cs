using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource clickAndioSource;
    public Slider bgmSlider;
    public Slider effectSlider;
    
    void Start()
    {
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
        SetBGMVolume(0.03f);
        SetEffectVolume(0.5f);
    }

    public void SetBGMVolume(float value)  //배경음악 볼륨조절
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f); 
    }

    public void SetEffectVolume(float value)  //효과음 볼륨조절
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f); 
    }

    public void ClickSound()   //클릭 사운드 내기
    {
        if (clickAndioSource != null && clickAndioSource.clip != null)
        {
            clickAndioSource.PlayOneShot(clickAndioSource.clip);
        }
    }
}
