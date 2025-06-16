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

    public void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f); 
    }

    public void SetEffectVolume(float value)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f); 
    }

    public void ClickSound()
    {
        if (clickAndioSource != null && clickAndioSource.clip != null)
        {
            clickAndioSource.PlayOneShot(clickAndioSource.clip);
        }
    }
}
