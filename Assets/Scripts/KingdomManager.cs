using UnityEngine;

public class KingdomManager : MonoBehaviour
{
    public static KingdomManager instance;
    public Kingdom k;
    //Ȥ�� ���� �����صа�
    private void Awake()
    {
        if (instance = null)
        {
           instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Clear(Kingdom kingdom)
    {
        
    }
}