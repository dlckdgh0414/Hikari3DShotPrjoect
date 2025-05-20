using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
//½Ã°£ ¾ø´Ù¤¿¤¿¤¿¤¿¤¿¤¿¤¿¤¿ÀÌ³«¤¾‘§¹Â¤Ó¤¤´Ý·ùÇÏ´ö¤¸µð»Ø¤Ó¤©
{
    public string sceneName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(sceneName);
    }
}
