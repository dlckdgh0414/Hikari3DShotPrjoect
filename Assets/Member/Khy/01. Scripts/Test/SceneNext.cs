using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
//�ð� ���٤����������������̳������¤Ӥ��ݷ��ϴ�����ؤӤ�
{
    public string sceneName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(sceneName);
    }
}
