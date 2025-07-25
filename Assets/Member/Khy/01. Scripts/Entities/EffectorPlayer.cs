using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectorPlayer : MonoBehaviour, IEntityComponent
{
    private Dictionary<string, ParticleSystem> _effectDictionary;
    private Player _player;
    public ParticleSystem CurrentAttackEffect { get; private set; }

    [SerializeField]
    private float DespawnEffSec;

    [TextArea]
    public string Descript;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _effectDictionary = new Dictionary<string, ParticleSystem>();
        GetComponentsInChildren<ParticleSystem>(true).ToList().ForEach(effect => _effectDictionary.Add(effect.name, effect));
    }

    public void PlayEffect(string effectName, bool ChildInPlayer = true)
    {
        ParticleSystem effect = _effectDictionary.GetValueOrDefault(effectName);
        Debug.Assert(effect != null, $"request attack data is not exist : {effect}");
        CurrentAttackEffect = effect;

        if (!ChildInPlayer)
        {
            GameObject effectObj = Instantiate(effect, _player.transform).gameObject;
            effectObj.transform.SetParent(null);
            effectObj.GetComponent<ParticleSystem>().Play();
            StartCoroutine(RemoveEffectRoutine(effectObj));
        }
        else
            effect.Play();
    }

    public void StopEffect(string effectName)
    {
        ParticleSystem effect = _effectDictionary.GetValueOrDefault(effectName);
        Debug.Assert(effect != null, $"request attack data is not exist : {effect}");
        effect.Stop();
    }

    private IEnumerator RemoveEffectRoutine(GameObject instanceEffect)
    {
        yield return new WaitForSeconds(DespawnEffSec);
        Destroy(instanceEffect);
    }
}
