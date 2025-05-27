using Ami.BroAudio;
using UnityEngine;

public class MainmenuLogic : MonoBehaviour
{
    [SerializeField]
    private SoundID mainmenuBGM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BroAudio.Play(mainmenuBGM);
    }

    public void StopSound()
    {
        BroAudio.Stop(mainmenuBGM);
    }
}
