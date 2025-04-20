using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestSkillTree : MonoBehaviour
{
    [SerializeField] private List<Fruits> fruitsList;
    [SerializeField] private SkillTreeSO skillTreeSO;
    [SerializeField] private GameEventChannelSO eventChannelSO;
    
    private SkillTreeEvent _skillTreeEvent = SkillTreeEventChannel.SkillTreeEvent;

    private void Awake()
    {
        fruitsList.ForEach(f => f.Initialize());

        foreach (Fruits f in fruitsList)
        {
            f.FruitsButton.onClick.AddListener(() => SelectFruits(f.GetFruitsSO()));
        }
        
        eventChannelSO.AddListener<SkillTreePurchaseEvent>(HandleFruitsPurchase);
    }

    private void HandleFruitsPurchase(SkillTreePurchaseEvent skillTreeEvent)
    {
        StartCoroutine(TestConnect(skillTreeEvent.fruits));
    }

    public void SelectFruits(FruitsSO selectedFruits)
    {
        _skillTreeEvent.fruitsSO = selectedFruits;
        eventChannelSO.RaiseEvent(_skillTreeEvent);
    }

    private IEnumerator TestConnect(Fruits f)
    {
        f.transform.SetSiblingIndex(f.transform.parent.childCount - 2);
        for (int i = 0; i < 3; i++)
        {
            DOTween.To(() => 0, amount => f.FillNode[i].fillAmount = amount, 1f, 1f);
            yield return new WaitUntil(() => f.FillNode[i].fillAmount == 1);
        }
    }
}
