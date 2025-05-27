using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
//½Ã°£ ¾ø´Ù¤¿¤¿¤¿¤¿¤¿¤¿¤¿¤¿ÀÌ³«¤¾‘§¹Â¤Ó¤¤´Ý·ùÇÏ´ö¤¸µð»Ø¤Ó¤©
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
