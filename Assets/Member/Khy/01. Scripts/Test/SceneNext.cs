using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
//�ð� ���٤����������������̳������¤Ӥ��ݷ��ϴ�����ؤӤ�
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
