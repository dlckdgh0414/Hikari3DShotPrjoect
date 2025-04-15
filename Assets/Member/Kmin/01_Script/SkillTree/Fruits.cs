using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fruits : MonoBehaviour, IFruits
{
    [SerializeField] private FruitsSO fruitsSO;
    [SerializeField] private Sprite fillNodeImage;
    [SerializeField] private List<Fruits> connectedFruits;
    [SerializeField] private bool isRootFruits;
    [SerializeField] private float width = 10;
    
    [HideInInspector]
    [field:SerializeField] public List<Image> ConnectedNode { get; private set; }
    [field:SerializeField] public List<Image> FillNode { get; private set; }
    public Button FruitsButton { get; private set; } = null;
    public bool IsActive { get; set; }
    public bool CanPurchase { get; private set; } = false;

    public void Initialize()
    {
        FruitsButton = GetComponentInChildren<Button>();
        if(isRootFruits) connectedFruits.ForEach(f => f.CanPurchase = true);
        fruitsSO.Fruits = this;
    }

    public void PurchaseFruits()
    {
        if (fruitsSO.price <= CurrencyManager.Instance.GetCurrency(CurrencyType.Eon) && !IsActive && CanPurchase)
        { 
            CurrencyManager.Instance.ModifyCurrency
                (CurrencyType.Eon, ModifyType.Substract, fruitsSO.price);
            connectedFruits.ForEach(f => f.CanPurchase = true);
            IsActive = true;
        }
    }

    public FruitsSO GetFruitsSO() => fruitsSO;

    #region ConnectLineOnEditor
    [ContextMenu("ConnectLine")]
    private void ConnectLine()
    {
        foreach (Fruits f in connectedFruits)
        {
            for (int i = 0; i < f.ConnectedNode.Count; i++)
            {
                if(f.ConnectedNode[i] == null)
                    f.ConnectedNode.RemoveAt(i);
                
                if(f.FillNode[i] == null)
                    f.FillNode.RemoveAt(i);
            }

            if (f.ConnectedNode.Count > 0)
            {
                for (int i = 0; i < f.ConnectedNode.Count; i++)
                {
                    if (f.ConnectedNode[i] != null)
                        DestroyImmediate(f.ConnectedNode[i].gameObject);
                    
                    if (f.FillNode[i] != null)
                        DestroyImmediate(f.FillNode[i].gameObject);
                }
                
                f.ConnectedNode.Clear();
                f.FillNode.Clear();
            }

            Transform root = f.transform.Find("Nodes");
            GameObject[] obj = new GameObject[3];
            Image[] nodes = new Image[3];

            for (int i = 0; i < 3; i++) {
                obj[i] = new GameObject($"Node{i}");
                nodes[i] = obj[i].AddComponent<Image>();
                nodes[i].transform.SetParent(root, false);
                nodes[i].transform.SetSiblingIndex(0);
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

            for (int i = 0; i < 3; i++)
            {
                Image fillImg = new GameObject($"FillNode{i}").AddComponent<Image>();
                fillImg.transform.SetParent(root, false);
                fillImg.rectTransform.anchoredPosition = f.ConnectedNode[i].rectTransform.anchoredPosition;
                fillImg.rectTransform.sizeDelta = f.ConnectedNode[i].rectTransform.sizeDelta;
                fillImg.color = Color.cyan;
                fillImg.type = Image.Type.Filled;
                fillImg.fillAmount = 0;
                fillImg.sprite = fillNodeImage;
                fillImg.transform.SetSiblingIndex(root.childCount);
                
                if(fillImg.rectTransform.sizeDelta.x > fillImg.rectTransform.sizeDelta.y)
                    fillImg.fillMethod = Image.FillMethod.Horizontal;
                else
                    fillImg.fillMethod = Image.FillMethod.Vertical;
                
                f.FillNode.Add(fillImg);
            }
        }
    }

    [ContextMenu("ClearAllNode")]
    private void ClearAllNode()
    {
        foreach(var fruits in connectedFruits)
        {
            fruits.ConnectedNode.ForEach(n => DestroyImmediate(n.gameObject));
            fruits.FillNode.ForEach(n => DestroyImmediate(n.gameObject));
            fruits.ConnectedNode.Clear();
            fruits.FillNode.Clear();
        }
    }

    [ContextMenu("ClearNode")]
    private void ClearNode()
    {
        ConnectedNode.ForEach(n => DestroyImmediate(n.gameObject));
        FillNode.ForEach(n => DestroyImmediate(n.gameObject));
        ConnectedNode.Clear();
        FillNode.Clear();
    }

    private void ConnectNode(Vector3 pos1, Vector3 pos2, Image node, bool isVert)
    {
        Vector3 centerPos = (pos1 + pos2) / 2f;
        float distance = Vector3.Distance(pos1, pos2);

        node.rectTransform.position = centerPos;

        if (isVert)
            node.rectTransform.sizeDelta = new Vector2(width, distance + width);
        else
            node.rectTransform.sizeDelta = new Vector2(distance + width, width);
    }
    #endregion
}
