using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Awake()
    {
       // _player = GameObject.FindWithTag("Player").GetComponent<Player>();

       // Sensitivity(2.5f);
    }

    public void ExitGame()
    {   
        Application.Quit();
    }


}
