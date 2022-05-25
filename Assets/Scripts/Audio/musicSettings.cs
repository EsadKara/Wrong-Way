using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class musicSettings : MonoBehaviour
{
    AudioSource playerAudio;
    Slider slider;
    [SerializeField] Text musicTxt;
    void Start()
    {
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        LoadAudio();
        musicTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }

    void Update()
    {
        musicTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }

    public void Music(float value)
    {
        playerAudio.volume = value;
        PlayerPrefs.SetFloat("Music", playerAudio.volume);
        musicTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }

    void LoadAudio()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            playerAudio.volume = PlayerPrefs.GetFloat("Music");
            slider.value = playerAudio.volume;
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            playerAudio.volume = PlayerPrefs.GetFloat("Music");
            slider.value = playerAudio.volume;
        }
    }
}
