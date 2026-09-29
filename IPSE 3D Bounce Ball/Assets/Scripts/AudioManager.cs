using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip bounce;
    public AudioClip getStar;
    public AudioClip button;
    public AudioClip itemBlock;

    AudioSource audioSource;

    void Awake()
    {
        //씬마다 AudioManager가 있으므로 처음 것만 남기고 나머지는 바로 종료
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
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
        AudioClip clip = null;
        switch (clipName)
        {
            case "Bounce":
                clip = bounce;
                break;
            case "GetStar":
                clip = getStar;
                break;
            case "ItemBlock":
                clip = itemBlock;
                break;
            case "Button":
                clip = button;
                break;
        }
        //clip을 갈아끼우면 이전 소리가 끊기므로 겹쳐서 재생
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
