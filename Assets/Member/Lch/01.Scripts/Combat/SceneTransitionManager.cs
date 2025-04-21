using Member.Ysc._01_Code.Agent;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private EntityFinderSO playerFinder;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "player does not exist in this scene");

        playerFinder.SetPlayer(player.GetComponent<Entity>());
    }
}
