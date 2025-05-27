using Ami.BroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipManager : MonoBehaviour
//�ð� ���٤����������������̳������¤Ӥ��ݷ��ϴ�����ؤӤ�
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
