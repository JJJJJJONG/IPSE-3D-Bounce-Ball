using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip bounce;
    public AudioClip getStar;
    public AudioClip button;
    public AudioClip itemBlock;

    AudioSource  audio;

    void Awake()
    {
       audio = gameObject.GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audio = GetComponent<AudioSource>();
        LoadAudioClips();
    }

    public void LoadAudioClips()
    {
        bounce = Resources.Load<AudioClip>("Sound/bounce");
        getStar = Resources.Load<AudioClip>("Sound/getStar");
        itemBlock = Resources.Load<AudioClip>("Sound/itemBlock");
        button = Resources.Load<AudioClip>("Sound/button");
    }

    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "Bounce":
                audio.clip = bounce;
                break;
            case "GetStar":
                audio.clip = getStar;
                break;
            case "ItemBlock":
                audio.clip = itemBlock;
                break;
            case "Button":
                audio.clip = button;
                break;
        }
        audio.Play();
    }
}
