using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource[] audioSource;
    public AudioClip[] BGM;
    public AudioClip[] SFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }
    public void PlayBGM(int n, float scale = 1.0f)
    {
        audioSource[0].Stop();
        audioSource[0].PlayOneShot(BGM[n], scale);
    }
    public void PlaySFX(int n, float scale = 1.0f)
    {
        audioSource[1].PlayOneShot(SFX[n], scale);
    }

    public void StopAllSFX()
    {
        audioSource[1].Stop();
    }
}
