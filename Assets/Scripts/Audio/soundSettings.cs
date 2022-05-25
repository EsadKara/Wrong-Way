using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class soundSettings : MonoBehaviour
{
    AudioSource playerAudio;
    Slider slider;
    [SerializeField] Text soundTxt;

    private void Awake()
    {
        playerAudio = GameObject.Find("GameManager").GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        LoadAudio();
        soundTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }
    void Start()
    {
    }

    void Update()
    {
        soundTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }

    public void Sound(float value)
    {
        playerAudio.volume = value;
        PlayerPrefs.SetFloat("Sound", playerAudio.volume);
        soundTxt.text = ((int)(playerAudio.volume * 100)).ToString();
    }

    void LoadAudio()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            playerAudio.volume = PlayerPrefs.GetFloat("Sound");
            slider.value = playerAudio.volume;
        }
        else
        {
            PlayerPrefs.SetFloat("Sound", 0.5f);
            playerAudio.volume = PlayerPrefs.GetFloat("Sound");
            slider.value = playerAudio.volume;
        }
    }
}
