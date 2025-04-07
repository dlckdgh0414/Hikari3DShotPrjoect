using UnityEngine;

public class KingdomManager : MonoBehaviour
{
    public static KingdomManager instance;
    public Kingdom k;
    //È¤½Ã ¸ô¶ó¼­ ±¸ÇöÇØµÐ°Å
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