using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip;
    public Vector3 minSize = new Vector2(0,0);
    public Vector3 MaxSize = new Vector2(0, 0);
    [Range(64, 8192)]
    public int visualizerSimples = 64;
    protected AudioClip AudioClip => audioClip;

    private AudioSource audioSource;
    private AudioListener audioLisner;
    private void Awake()
    {
        if (!audioClip)
            return;
        audioSource = new GameObject("AudioManager").AddComponent<AudioSource>();
        audioLisner = GetComponent<AudioListener>();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

   
}