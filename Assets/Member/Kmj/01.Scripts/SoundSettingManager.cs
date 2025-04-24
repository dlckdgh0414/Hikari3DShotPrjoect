using Ami.BroAudio;
using Ami.BroAudio.Data;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettingManager : MonoBehaviour
{

    [SerializeField] private BroAudioType _bgm;
    [SerializeField] private BroAudioType _vfx;
    [SerializeField] private BroAudioType _main;

    private void Awake()
    { 
    }

    public void BGM(float volume)
    {
        BroAudio.SetVolume(_bgm, volume);
    }

    public void VFX(float volume)
    {
        BroAudio.SetVolume(_vfx,volume);
    }

    public void Master(float volume)
    {
        BroAudio.SetVolume(_main, volume);
    }
}
