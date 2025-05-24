using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelChanger : MonoBehaviour,IEntityComponent
{
    public PlayerSkinSO skinSO;
    private Player _player;
    private Model _currentSkin;
    private Dictionary<string, Model> models = new();

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        GetComponentsInChildren<Model>(true).ToList().ForEach(compo => models.Add(compo.gameObject.name, compo));
        string skillSOName = PlayerSendInfo.Instance.ThisSkill != null ? PlayerSendInfo.Instance.ThisSkill.name : skinSO.name;


        _currentSkin = models[skillSOName];
        _currentSkin.gameObject.SetActive(true);
        _player.ModelChange(_currentSkin.gameObject);
    }
}
