using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using System.Diagnostics.Tracing;

public class Fruits : MonoBehaviour, IFruits
{
    [SerializeField] private GameEventChannelSO eventChannelSO;
    [SerializeField] private FruitsSO fruitsSO;
    [SerializeField] private List<Fruits> _connectedFruits;
    [SerializeField] private bool isRootFruits;
    
    [HideInInspector]
    [field:SerializeField] public List<Image> ConnectedNode { get; private set; }
    public Button FruitsButton { get; private set; } = null;
    public bool IsActive { get; set; }
    public bool CanPurchase { get; private set; } = false;

    private SkillTreeEvent _skillTreeEvent = SkillTreeEventChannel.SkillTreeEvent;

    public void Initialize()
    {
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Set, 10000);
        FruitsButton = GetComponentInChildren<Button>();
        FruitsButton.onClick.AddListener(SelectFruits);
        fruitsSO.Fruits = this;

        if(isRootFruits) _connectedFruits.ForEach(f => f.CanPurchase = true);
    }

    public void SelectFruits()
    {
        Debug.Log("FruitsSelect");
        _skillTreeEvent.fruitsSO = fruitsSO;
        eventChannelSO.RaiseEvent(_skillTreeEvent);
    }

    public void PurchaseFruits()
    {
        if (fruitsSO.price <= CurrencyManager.Instance.GetCurrency(CurrencyType.Eon) && !IsActive && CanPurchase)
        {
            CurrencyManager.Instance.ModifyCurrency
                (CurrencyType.Eon, ModifyType.Substract, fruitsSO.price);
            _connectedFruits.ForEach(f => f.CanPurchase = true);
            IsActive = true;

            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        ConnectedNode.ForEach(line => line.color = Color.red);
        //여기에 연출 추가 예정  
    }

    #region ConnectLineOnEditor
    [ContextMenu("ConnectLine")]
    private void ConnectLine()
    {
        foreach (Fruits f in _connectedFruits)
        {
/*            if (f.ConnectedNode != null)
                f.ConnectedNode.Clear();*/

            Transform root = f.transform.Find("Nodes");
            GameObject[] obj = new GameObject[3];
            Image[] nodes = new Image[3];

            for (int i = 0; i < 3; i++) {
                obj[i] = new GameObject($"Node{i}");
                nodes[i] = obj[i].AddComponent<Image>();
                nodes[i].transform.SetParent(root, false);

                f.ConnectedNode.Add(nodes[i]);
            }

            var rect = transform as RectTransform;
            Vector2 node1Pos = Vector2.zero;
            Vector2 selfPos = rect.position;
            Vector2 fruitsPos = f.GetComponentInChildren<Image>().rectTransform.position;

            for (int i = 0; i < 2; i++)
            {
                if (node1Pos == Vector2.zero) {
                    node1Pos = new Vector2(selfPos.x, (fruitsPos.y + selfPos.y) / 2);
                    ConnectNode(selfPos, node1Pos, nodes[0], true);
                }

                Vector3 node2Pos = new Vector2(fruitsPos.x, node1Pos.y);
                ConnectNode(node1Pos, node2Pos, nodes[1], false);
                ConnectNode(node2Pos, fruitsPos, nodes[2], true);
            }
        }
    }

    [ContextMenu("ClearAllNode")]
    private void ClearAllNode()
    {
        foreach(var fruits in _connectedFruits)
        {
            fruits.ConnectedNode.ForEach(n => DestroyImmediate(n.gameObject));
            fruits.ConnectedNode.Clear();
        }
    }

    [ContextMenu("ClearNode")]
    private void ClearNode()
    {
        ConnectedNode.ForEach(n => DestroyImmediate(n.gameObject));
        ConnectedNode.Clear();
    }

    private void ConnectNode(Vector3 pos1, Vector3 pos2, Image node, bool isVert)
    {
        Vector3 centerPos = (pos1 + pos2) / 2f;
        float distance = Vector3.Distance(pos1, pos2);

        node.rectTransform.position = centerPos;

        if (isVert)
            node.rectTransform.sizeDelta = new Vector2(10, distance);
        else
            node.rectTransform.sizeDelta = new Vector2(distance, 10);
    }
    #endregion
}
