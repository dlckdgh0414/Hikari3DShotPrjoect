using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Member.Kmin._01_Script.SkillTree;
using Member.Kmin._01_Script.SO;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public SkillTreeSO skillTreeSO;
    [SerializeField] private GameEventChannelSO eventChannelSO;
    [SerializeField] private NodeSOList nodeSOList;
    [SerializeField] private SaveNodeStat saveNodeStat;
    [SerializeField] private GameObject background;
    
    private List<SkillTreeNode> _nodes;
    private SkillTreeNode _selectedNode;
    private Dictionary<SkillTreeNode, NodeSO> _nodesDic;
    
    private SkillTreeSelectEvent _skillTreeSelectEvent = SkillTreeEventChannel.SkillTreeSelectEvent;

    public static SkillTree Instance { get; private set; }

    private void Awake()
    {
        _nodesDic = new Dictionary<SkillTreeNode, NodeSO>();
        _nodes = transform.GetComponentsInChildren<SkillTreeNode>(true).ToList();
        
        _nodes.ForEach(f =>
        {
            f.Initialize();
            _nodesDic.Add(f, nodeSOList.nodeSOList.Find(n => n.name == f.GetNodeSO().name));

            if (f.IsRootNode)
                ChangeNodeColor(f);
            
            if (f.GetNodeSO().isPurchase)
                ConnectColor(f, true);
        });
        
        _nodes.ForEach(f => f.NodeButton.onClick.AddListener(() => SelectNode(f)));
        eventChannelSO.AddListener<SkillTreePurchaseEvent>(HandleNodePurchase);
        eventChannelSO.AddListener<SkillTreeActiveEvent>(HandleNodeActive);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void OnDestroy()
    {
        eventChannelSO.RemoveListener<SkillTreePurchaseEvent>(HandleNodePurchase);
        eventChannelSO.RemoveListener<SkillTreeActiveEvent>(HandleNodeActive);
    }

    private void HandleNodeActive(SkillTreeActiveEvent evt) => ActiveNodeColor(_selectedNode, evt.isActive);

    private void HandleNodePurchase(SkillTreePurchaseEvent evt)
    {
        NodeSO nodeSO = _selectedNode.GetNodeSO();
        nodeSO.isPurchase = true;

        saveNodeStat.AddStat(nodeSO.statSO, nodeSO.upgradeValue);

        if (!string.IsNullOrEmpty(nodeSO.passiveSkill))
        {
            //PassiveSkill skill = skillCompo.transform.Find(nodeSO.passiveSkill).GetComponent<PassiveSkill>();
            saveNodeStat.skillData.Add(nodeSO.passiveSkill);
        }
        
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, -nodeSO.price);
        nodeSOList.Save();
        ConnectColor(evt.node);
    }

    private void SelectNode(SkillTreeNode selectedNode)
    {
        _selectedNode = selectedNode;
        _skillTreeSelectEvent.node = selectedNode;
        eventChannelSO.RaiseEvent(_skillTreeSelectEvent);
    }


    private void ConnectColor(SkillTreeNode f, bool isInstance = false)
    {
        if (f.IsRootNode) return;
        
        f.transform.SetSiblingIndex(f.ParentNode.transform.GetSiblingIndex() - 1);

        Sequence seq = DOTween.Sequence();
        
        for (int i = 0; i < 3; i++) {
            int idx = i;
            seq.Append(DOTween.To(() => 0f, amount
                    => f.FillBranch[idx].fillAmount = amount, 1f, 
                isInstance ? 0f : 0.2f));
        }

        seq.OnComplete(() => ChangeNodeColor(f, isInstance ? true : false));
    }

    public void ChangeNodeColor(SkillTreeNode f, bool isInstance = false)
    {
        Sequence seq = DOTween.Sequence();
        Outline outline = f.GetComponentInChildren<Outline>();

        seq.Join(f.NodeOutline.DOColor(f.branchColor, isInstance ? 0f : 1f))
            .Join(f.NodeIcon.DOColor(f.branchColor, isInstance ? 0f : 1f))
            .Join(f.NodeIcon.DOFade(1f, isInstance ? 0f : 1f));

        seq.OnComplete(() => {
            f.ConnectedNodes.ForEach(n => {
                n.NodeIcon.DOColor(Color.white, isInstance ? 0f : 1f);
                n.NodeIcon.DOFade(1f, isInstance ? 0f : 1f);
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
    
    public void ChangeActive() => background.SetActive(!background.activeSelf);
}
