using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
//獣娃 蒸陥たたたたたたたた戚開ぞ�Ч造咾ご欸�馬幾じ巨指びぉ
{
    public string sceneName;

    [SerializeField]
    private SoundID spaceShipBGM;
    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
            BroAudio.Stop(spaceShipBGM);
        }
    }
}
