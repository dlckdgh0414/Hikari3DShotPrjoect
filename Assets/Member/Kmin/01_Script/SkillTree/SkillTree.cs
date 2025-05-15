using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Member.Kmin._01_Script.SkillTree;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public SkillTreeSO skillTreeSO;
    [SerializeField] private GameEventChannelSO eventChannelSO;
    [SerializeField] private EntityStat statCompo;
    //[SerializeField] private EntitySkillCompo skillCompo;
    
    private List<SkillTreeNode> _fruitsList;
    private SkillTreeNode _selectedNode;
    private SkillTreeSelectEvent _skillTreeSelectEvent = SkillTreeEventChannel.SkillTreeSelectEvent;

    private void Awake()
    {
        _fruitsList = transform.GetComponentsInChildren<SkillTreeNode>(true).ToList();
        _fruitsList.ForEach(f =>
        {
            f.Initialize();

            if (f.IsRootNode)
                ChangeNodeColor(f);
        });
        
        _fruitsList.ForEach(f => f.NodeButton.onClick.AddListener(() => SelectFruits(f)));
        eventChannelSO.AddListener<SkillTreePurchaseEvent>(HandleNodePurchase);
        eventChannelSO.AddListener<SkillTreeActiveEvent>(HandleNodeActive);
    }

    private void HandleNodeActive(SkillTreeActiveEvent evt) => ActiveNodeColor(_selectedNode, evt.isActive);

    private void HandleNodePurchase(SkillTreePurchaseEvent evt)
    {
        NodeSO nodeSO = evt.node.GetNodeSO();
        nodeSO.isPurchase = true;
        StatSO targetStat = statCompo.GetStat(nodeSO.statSO);
        targetStat.AddModifier(this, nodeSO.upgradeValue);
        
        ConnectColor(evt.node);
    }

    private void SelectFruits(SkillTreeNode selectedNode)
    {
        _selectedNode = selectedNode;
        _skillTreeSelectEvent.node = selectedNode;
        eventChannelSO.RaiseEvent(_skillTreeSelectEvent);
    }


    private void ConnectColor(SkillTreeNode f)
    {
        f.transform.SetSiblingIndex(f.ParentNode.transform.GetSiblingIndex() - 1);

        Sequence seq = DOTween.Sequence();
        
        for (int i = 0; i < 3; i++) {
            int idx = i;
            seq.Append(DOTween.To(() => 0f, amount
                    => f.FillBranch[idx].fillAmount = amount, 1f, 0.2f));
        }

        seq.OnComplete(() => ChangeNodeColor(f));
    }

    public void ChangeNodeColor(SkillTreeNode f)
    {
        Sequence seq = DOTween.Sequence();
        Outline outline = f.GetComponentInChildren<Outline>();

        seq.Join(f.NodeOutline.DOColor(f.branchColor, 1f))
            .Join(f.NodeIcon.DOColor(f.branchColor, 1f))
            .Join(f.NodeIcon.DOFade(1f, 1f));

        seq.OnComplete(() => {
            f.ConnectedNodes.ForEach(n => {
                n.NodeIcon.DOColor(Color.white, 1f);
                n.NodeIcon.DOFade(1f, 1f);
                n.NodeButton.interactable = true;
            });
        });
    }

    private void ActiveNodeColor(SkillTreeNode node, bool isActive)
    {
        Color targetColor = isActive ? node.branchColor : Color.white;

        node.NodeOutline.DOColor(targetColor, 0.2f);
        node.NodeIcon.DOColor(targetColor, 0.2f);
    }
}
