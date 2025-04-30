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
    [SerializeField] private Color connectColor = Color.magenta;
    
    [HideInInspector]
    [field:SerializeField] public List<Image> ConnectedNode { get; private set; }
    [field:SerializeField] public List<Image> FillNode { get; private set; }
    public List<Image> BackgroundNode { get; private set; }
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
            if (f.ConnectedNode.Count > 0)
            {
                f.ConnectedNode.ForEach(n => {if (n != null) DestroyImmediate(n.gameObject); });
                f.FillNode.ForEach(n => { if (n != null) DestroyImmediate(n.gameObject); });
                f.BackgroundNode.ForEach(n => { if (n != null) DestroyImmediate(n.gameObject); });
                f.ConnectedNode.Clear();
                f.FillNode.Clear();
                f.BackgroundNode.Clear();
            }

            Transform root = f.transform.Find("Nodes");
            GameObject[] obj = new GameObject[3];
            Image[] nodes = new Image[3];

            for (int i = 0; i < 3; i++)
            {
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

            int origin = 0;

            if (node1Pos == Vector2.zero)
            {
                node1Pos = new Vector2(selfPos.x, (fruitsPos.y + selfPos.y) / 2);
                origin = selfPos.y < node1Pos.y ? 0 : 1;
                ConnectNode(selfPos, node1Pos, nodes[0], true);
                ConnectFillNode(f.ConnectedNode[0], root, f, origin);
            }

            Vector3 node2Pos = new Vector2(fruitsPos.x, node1Pos.y);
            origin = node1Pos.x < node2Pos.x ? 0 : 1;
            ConnectNode(node1Pos, node2Pos, nodes[1], false);
            ConnectFillNode(f.ConnectedNode[1], root, f, origin);

            origin = node2Pos.y < fruitsPos.y ? 0 : 1;
            ConnectNode(node2Pos, fruitsPos, nodes[2], true);
            ConnectFillNode(f.ConnectedNode[2], root, f, origin);
        }
    }

    private void ConnectFillNode(Image target, Transform root, Fruits parent, int origin)
    {
        Image fillImg = new GameObject($"FillNode{parent.FillNode.Count}").AddComponent<Image>();
        fillImg.transform.SetParent(root, false);
        fillImg.rectTransform.anchoredPosition = target.rectTransform.anchoredPosition;
        fillImg.rectTransform.sizeDelta = target.rectTransform.sizeDelta;
        fillImg.type = Image.Type.Filled;
        fillImg.fillAmount = 0;
        fillImg.sprite = fillNodeImage;
        fillImg.transform.SetSiblingIndex(root.childCount);

        if (fillImg.rectTransform.sizeDelta.x > fillImg.rectTransform.sizeDelta.y)
            fillImg.fillMethod = Image.FillMethod.Horizontal;
        else
            fillImg.fillMethod = Image.FillMethod.Vertical;

        fillImg.fillOrigin = origin;
        parent.FillNode.Add(fillImg);
    }
    
    private void ConnectBGNode(Image target, Transform root, Fruits parent)
    {
        Image fillImg = new GameObject($"BackgroundNode{parent.FillNode.Count}").AddComponent<Image>();
        fillImg.transform.SetParent(root, false);
        fillImg.rectTransform.anchoredPosition = target.rectTransform.anchoredPosition;
        fillImg.rectTransform.sizeDelta = new Vector2(target.rectTransform.sizeDelta.x * 2, target.rectTransform.sizeDelta.y);
        fillImg.sprite = fillNodeImage;
        fillImg.transform.SetSiblingIndex(0);

        parent.BackgroundNode.Add(fillImg);
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

    private void OnValidate()
    {
        if (FillNode != null)
        {
            foreach (var node in FillNode)
            {
                node.color = connectColor;
            }
        }
    }
}
