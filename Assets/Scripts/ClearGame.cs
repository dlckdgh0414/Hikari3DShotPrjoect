
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearGame : MonoBehaviour
{
    public static int CLEARIDX = 0;
    public static bool IsCLEAR = false;
    public static ClearGame instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ClearMethod()
    {
        IsCLEAR = true;
    }
}
