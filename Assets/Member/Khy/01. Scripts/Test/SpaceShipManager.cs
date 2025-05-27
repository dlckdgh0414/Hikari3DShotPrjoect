using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipManager : MonoBehaviour
//½Ã°£ ¾ø´Ù¤¿¤¿¤¿¤¿¤¿¤¿¤¿¤¿ÀÌ³«¤¾‘§¹Â¤Ó¤¤´Ý·ùÇÏ´ö¤¸µð»Ø¤Ó¤©
{
    [SerializeField]
    private SoundID spaceShipBGM;
    private void Start()
    {
        BroAudio.Play(spaceShipBGM);
    }

    public void PrevScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ButtonEvent()
    {

    }

    public void HumanEvent()
    {

    }
}
